using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ORMLib;

using Linq;

public abstract class Table<T>
    where T : class, new()
{
    private bool exist = false;
    public async Task Save()
    {
        var obj = this as T;
        if (this.exist)
            await Access.Instance.Update(obj);
        else await Access.Instance.Insert(obj);
        this.exist = true;
    }
    public async Task Delete()
    {
        var obj = this as T;
        await Access.Instance.Delete(obj);
        this.exist = false;
    }
    public static IQueryable<T> All
    {
        get
        {
            var provider = ObjectRelationalMappingConfig.Config.QueryProvider;
            var empty = provider.CreateQuery<T>(null);
            return provider.CreateQuery<T>(Expression.Constant(empty));
        }
    }
}