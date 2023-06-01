using System.Data.SqlClient;

namespace ORMLib;

using Providers;

public class ObjectRelationalMappingConfigBuilder
{
    private DataBaseSystem dataBaseSystem;
    private SqlConnectionStringBuilder stringConnectionBuilder = new SqlConnectionStringBuilder();
    private IQueryProvider queryProvider;
    private IAccessProvider accessProvider;
    public ObjectRelationalMappingConfigBuilder SetDataBaseSystem(DataBaseSystem sys)
    {
        this.dataBaseSystem = sys;
        return this;
    }
    public ObjectRelationalMappingConfigBuilder SetDataSource(string serverName)
    {
        stringConnectionBuilder.DataSource = serverName;
        return this;
    }
    public ObjectRelationalMappingConfigBuilder SetInitialCatalog(string initialCatalog)
    {
        stringConnectionBuilder.InitialCatalog = initialCatalog;
        return this;
    }
    public ObjectRelationalMappingConfigBuilder SetIntegratedSecurity(bool integratedSecurity)
    {
        stringConnectionBuilder.IntegratedSecurity = integratedSecurity;
        return this;
    }
    public ObjectRelationalMappingConfigBuilder SetStringConnection(string strConn)
    {
        stringConnectionBuilder.ConnectionString = strConn;
        return this;
    }
    public ObjectRelationalMappingConfigBuilder SetQueryProvider(IQueryProvider provider)
    {
        this.queryProvider = provider;
        return this;
    }
    public ObjectRelationalMappingConfigBuilder SetAccessProvider(IAccessProvider provider) 
    {
        this.accessProvider = provider;
        return this;
    }
    public ObjectRelationalMappingConfig Build()
    {
        ObjectRelationalMappingConfig config = new ObjectRelationalMappingConfig(
        this.dataBaseSystem,
        this.stringConnectionBuilder.ConnectionString,
        this.stringConnectionBuilder.InitialCatalog,
        this.queryProvider,
        this.accessProvider
        );
        return config;
    }
}