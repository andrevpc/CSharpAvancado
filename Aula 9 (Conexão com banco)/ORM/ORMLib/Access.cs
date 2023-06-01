using System.Threading.Tasks;

namespace ORMLib;

public abstract class Access
{
    private static Access instance = null;
    public static Access Instance
    {
        get
        {
            if (instance is not null)
                return instance;
            var provider = ObjectRelationalMappingConfig.Config.AccessProvider;
            instance = provider.Provide();
            return instance;
        }
    }
    public abstract Task Insert<T>(T obj);
    public abstract Task Delete<T>(T obj);
    public abstract Task Update<T>(T obj);
}