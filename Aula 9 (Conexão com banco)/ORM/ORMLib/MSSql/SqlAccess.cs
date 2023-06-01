using System;
using System.Linq;
using System.Data;
using System.Reflection;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ORMLib.MSSql;

using Exceptions;
using DataAnnotations;
using System.Collections;

internal class SqlAccess : Access
{
    private SqlCommand comm;
    private SqlConnection conn;
    private bool loaded = false;
    private bool exist = false;

    public async Task CreateDataBaseIfNotExistAsync()
    {
        if (exist)
            return;
        exist = true;

        var config = ObjectRelationalMappingConfig.Config;
        var masterStrConn = config.StringConnection.Replace(
        config.InitialCatalog,
        "master"
        );
        var conn = new SqlConnection(masterStrConn);
        await conn.OpenAsync();

        var comm = new SqlCommand();
        comm.CommandText = $"select * from sys.databases where name = '{config.InitialCatalog}'";
        comm.Connection = conn;
        comm.CommandType = CommandType.Text;

        var reader = await comm.ExecuteReaderAsync();
        var dt = new DataTable();
        dt.Load(reader);

        if (dt.Rows.Count > 0)
        {
            await conn.CloseAsync();
            return;
        }

        comm.CommandText = $"create database {config.InitialCatalog}";
        await comm.ExecuteNonQueryAsync();
        await conn.CloseAsync();
    }
    public async Task LoadAsync()
    {
        if (loaded)
            return;

        await CreateDataBaseIfNotExistAsync();

        var config = ObjectRelationalMappingConfig.Config;
        conn = new SqlConnection(config.StringConnection);
        await conn.OpenAsync();
        comm = new SqlCommand();
        comm.Connection = conn;
        comm.CommandType = CommandType.Text;

        loaded = true;
    }
    public async Task CreateIfNotExistAsync(Type type)
    {
        await LoadAsync();

        if (await TestExistenceAsync(type))
            return;

        comm.CommandText = $"create table {type.Name} (";

        foreach (var prop in type.GetProperties())
        {
            string column = $"{prop.Name} {ConvertToSqlType(prop.PropertyType)}";
            
            if (prop.Name == "ID")
                column += " identity primary key";

            var foreignKeyAtt = prop.GetCustomAttribute<ForeignKeyAttribute>();
            if (foreignKeyAtt != null)
            {
                string temp = comm.CommandText;
                await CreateIfNotExistAsync(foreignKeyAtt.ForeignTable);
                comm.CommandText = temp;
                column += $" references {foreignKeyAtt.ForeignTable.Name}(ID)";
            }

            var notnullAtt = prop.GetCustomAttribute<NotNullAttribute>();
            
            if (notnullAtt != null)
                column += $" not null";

            comm.CommandText += $"{column}, ";
        }
        comm.CommandText = comm.CommandText.Substring(0, comm.CommandText.Length - 1) + " )";
        
        await comm.ExecuteNonQueryAsync();
    }
    public string ConvertToSqlType(Type type)
    {
        if (type == typeof(int))
            return "int";

        if (type == typeof(string))
            return "varchar(MAX)";

        if (type == typeof(byte[]))
            return "varbinary";

        if (type == typeof(decimal))
            return "decimal";

        if (type == typeof(long))
            return "bigint";

        if (type == typeof(DateTime))
            return "datetime";

        throw new InvalidColumnType(type);
    }
    public async Task<bool> TestExistenceAsync(Type type)
    {
        DataTable dt = await ReadTableAsync($"select * from sys.tables where name = '{type.Name}'");
        return dt.Rows.Count > 0;
    }
    public async Task<DataTable> ReadTableAsync(string query, params SqlParameter[] parameters)
    {
        await LoadAsync();
        comm.CommandText = query;
        comm.Parameters.Clear();
        comm.Parameters.AddRange(parameters);
        var reader = await comm.ExecuteReaderAsync();
        DataTable dt = new DataTable();
        dt.Load(reader);
        return dt;
    }
    public async Task ExecuteNonQueryAsync(string query, params SqlParameter[] parameters)
    {
        comm.CommandText = query;
        comm.Parameters.Clear();
        comm.Parameters.AddRange(parameters);

        await comm.ExecuteNonQueryAsync();
    }
    public async Task<T> RunQuery<T>(string query, params SqlParameter[] parameters)
    {
        var type = typeof(T);
        var dt = await ReadTableAsync(query, parameters);
        var isCollection = typeof(IEnumerable).IsAssignableFrom(type);

        if (isCollection)
        {
            var args = type.GetGenericArguments();
            if (args.Length > 0)
                type = args[0];
        }

        int i = 0;
        object[] data = new object[dt.Rows.Count];

        foreach (DataRow row in dt.Rows)
        {
            if (row.ItemArray.Length == 1)
            {
                data[i++] = row.ItemArray[0];
                continue;
            }

            var obj = Activator.CreateInstance(type);
            
            foreach (var prop in type.GetProperties())
                prop.SetValue(obj, row[prop.Name]);

            data[i++] = obj;
        }

        if (isCollection)
        {
            var listType = typeof(List<>).MakeGenericType(type);
            var list = (IList)Activator.CreateInstance(listType);

            foreach (var x in data)
                list.Add(Convert.ChangeType(x, type));
            return (T)list;
        }
        return (T)data[0];
    }
    public override async Task Insert<T>(T obj)
    {
        await CreateIfNotExistAsync(typeof(T));

        var id = getID<T>();

        List<SqlParameter> parameters = new List<SqlParameter>();
        string query = $"insert {typeof(T).Name} values (";
        
        foreach (var prop in typeof(T).GetProperties())
        {
            if (prop.Name == id?.Name)
                continue;

            var paramName = "@" + prop.Name;
            query += paramName + ",";
            parameters.Add(new SqlParameter(paramName, prop.GetValue(obj)));
        }
        query = query.Substring(0, query.Length - 1) + ")";
        await ExecuteNonQueryAsync(query, parameters.ToArray());
    }
    public override async Task Delete<T>(T obj)
    {
        await CreateIfNotExistAsync(typeof(T));

        var id = getID<T>();

        await ExecuteNonQueryAsync(
            $"delete {typeof(T).Name} where {id.Name} == @{id.Name}",
            new SqlParameter($"@{id.Name}", id.GetValue(obj))
        );
    }
    public override async Task Update<T>(T obj)
    {
        await CreateIfNotExistAsync(typeof(T));

        var id = getID<T>();
        
        List<SqlParameter> parameters = new List<SqlParameter>();
        string query = $"update {typeof(T).Name} set \n";

        foreach (var prop in typeof(T).GetProperties())
        {
            if (prop.Name == id?.Name)
                continue;

            var paramName = "@" + prop.Name;
            query += paramName + ",";
            parameters.Add(new SqlParameter(paramName, prop.GetValue(obj)));
        }
        query += $" where {id.Name} == @{id.Name}";
        parameters.Add(new SqlParameter($"@{id.Name}", id.GetValue(obj)));
        await ExecuteNonQueryAsync(query, parameters.ToArray());
    }
    private PropertyInfo getID<T>()
    {
        foreach (var prop in typeof(T).GetProperties())
        {
            if (prop.GetCustomAttribute<PrimaryKeyAttribute>() is null)
                continue;
            return prop;
        }
        return null;
    }
}