using System;
using System.Linq.Expressions;

namespace ORMLib.Linq;

using Providers;

public interface IQueryable
{
    Type ElementType { get; }
    Expression Expression { get; }
    IQueryProvider Provider { get; }
}
public interface IQueryable<out T> : IQueryable
{
}
