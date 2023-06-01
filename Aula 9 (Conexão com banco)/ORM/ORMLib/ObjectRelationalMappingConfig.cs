using System.Threading.Tasks;

namespace ORMLib;

using Exceptions;
using Providers;

public class ObjectRelationalMappingConfig
{
    private static ObjectRelationalMappingConfig config = null;
    public static ObjectRelationalMappingConfig Config
    {
        get
        {
            if (config == null)
                throw new ConfigNotInitializedException();
            return config;
        }
    }
    public static ObjectRelationalMappingConfigBuilder GetBuilder()
        => new ObjectRelationalMappingConfigBuilder();
    public virtual DataBaseSystem DataBaseSystem { get; set; }
    public virtual string StringConnection { get; set; }
    public virtual string InitialCatalog { get; set; }
    public IQueryProvider QueryProvider { get; set; }
    public IAccessProvider AccessProvider { get; set; }
    public ObjectRelationalMappingConfig(
        DataBaseSystem dataBaseSystem,
        string stringConnection,
        string initialCatalog,
        IQueryProvider queryProvider,
        IAccessProvider accessProvider
    )
    {
        this.DataBaseSystem = dataBaseSystem;
        this.StringConnection = stringConnection;
        this.InitialCatalog = initialCatalog;
        this.QueryProvider = queryProvider;

        this.AccessProvider = accessProvider;
    }
    public void Use()
        => config = this;
    public virtual async Task UseAsync()
        => Use();
}