using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace ORMLib.MSSql;

using System.Data.SqlClient;
using Linq;
using Providers;

public class MSSqlQueryProvider : IQueryProvider
{
    public IQueryable<T> CreateQuery<T>(Expression expression)
        => new MSSqlQueryable<T>(expression, this);
    public async Task<T> Execute<T>(Expression expression)
    {
        SqlAccess access = new SqlAccess();
        List<SqlParameter> list = new List<SqlParameter>();
        var query = buildQuery<T>(expression, list);
        // System.Console.WriteLine(query);
        var data = await access.RunQuery<T>(query, list.ToArray());
        return data;
    }
    private string buildQuery<T>(Expression expression, List<SqlParameter> parameters)
    {
        string query = "";
        if (expression is MethodCallExpression call)
        {
            if (call.Method.Name == "Select" && call.Method.DeclaringType == typeof(Queryable))
            {
                query = buildQuery<T>(call.Arguments[0], parameters);
                if (call.Arguments[1] is UnaryExpression unary)
                {
                    var exp = unary.Operand;
                    var paramter = exp.ToString().Split(' ')[0];
                    var str = string.Concat(
                    exp.ToString()
                    .SkipWhile(c => c != '>')
                    .Skip(1)
                    );
                    query = query.Replace("*", str) + " " + paramter;
                }
            }
            if (call.Method.Name == "Where" && call.Method.DeclaringType == typeof(Queryable))
            {
                query = buildQuery<T>(call.Arguments[0], parameters);
                query = buildQuery<T>(call.Arguments[0], parameters);
                if (call.Arguments[1] is UnaryExpression unary)
                {
                    var exp = unary.Operand;
                    var paramter = exp.ToString().Split(' ')[0];
                    var str = string.Concat(
                    exp.ToString()
                    .SkipWhile(c => c != '>')
                    .Skip(1)
                    );
                    str = str.Replace("==", "=");
                    str = str.Replace("\"", "'");
                    query = $"{query} {paramter} where {str}";
                }
            }
        }
        else if (expression is ConstantExpression constExp)
        {
            var type = constExp.Type;
            if (typeof(IQueryable).IsAssignableFrom(type))
            {
                var queryType = type.GenericTypeArguments[0];
                query = $"select * from {queryType.Name}";
            }
        }
        return query;
    }
}