using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace ORMLib.Linq;

public static class Queryable
{
    public static IQueryable<R> Select<T, R>(this IQueryable<T> source, Expression<Func<T, R>> selector)
    where T : new()
    {
        if (source is null)
            throw new ArgumentNullException("source");
        if (selector is null)
            throw new ArgumentNullException("selector");
        var provider = source.Provider;
        var self = new Func<IQueryable<T>, Expression<Func<T, R>>, IQueryable<R>>(Select);
        var query = provider.CreateQuery<R>(
        Expression.Call(
        null,
        self.Method,
        source.Expression,
        Expression.Quote(selector)
        )
        );
        return query;
    }
    public static IQueryable<T> Where<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate)
    where T : new()
    {
        if (source is null)
            throw new ArgumentNullException("source");
        if (predicate is null)
            throw new ArgumentNullException("predicate");
        var provider = source.Provider;
        var self = new Func<IQueryable<T>, Expression<Func<T, bool>>, IQueryable<T>>(Where);
        var query = provider.CreateQuery<T>(
        Expression.Call(
        null,
        self.Method,
        source.Expression,
        Expression.Quote(predicate)
        )
        );
        return query;
    }
    public static async Task<List<T>> ToListAsync<T>(this IQueryable<T> source)
    {
        if (source is null)
            throw new ArgumentNullException("source");
        var exp = source.Expression;
        var provider = source.Provider;
        var collection = await provider.Execute<IEnumerable<T>>(exp);
        return collection.ToList();
    }
}