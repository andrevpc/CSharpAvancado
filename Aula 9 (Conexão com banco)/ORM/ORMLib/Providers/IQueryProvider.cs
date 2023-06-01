using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ORMLib.Providers;

using Linq;

public interface IQueryProvider
{
    IQueryable<T> CreateQuery<T>(Expression exp);
    Task<T> Execute<T>(Expression exp);
}