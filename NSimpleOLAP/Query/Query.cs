using NSimpleOLAP.Query.Interfaces;
using System;

namespace NSimpleOLAP.Query
{
  /// <summary>
  /// Description of Query.
  /// </summary>
  public abstract class Query<T>
    where T : struct, IComparable
  {
    protected Axis<T> axis;

    protected IPredicate<T> predicates;

    internal Axis<T> Axis
    {
      get { return axis; }
    }

    internal IPredicate<T> PredicateTree
    {
      get { return predicates; }
    }
  }
}