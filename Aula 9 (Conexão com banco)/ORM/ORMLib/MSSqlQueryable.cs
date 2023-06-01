using System;
using System.Linq.Expressions;
using System.Collections.Generic;
namespace ORMLib.MSSql;
using Providers;
using Linq;
public class MSSqlQueryable<T> : IQueryable<T>
{
    public MSSqlQueryable(Expression exp, MSSqlQueryProvider provider)
    {
        this.ElementType = typeof(IEnumerable<T>);
        this.Expression = exp;
        this.Provider = provider;
    }
    public Type ElementType { get; private set; }
    public Expression Expression { get; private set; }
    public IQueryProvider Provider { get; private set; }
}