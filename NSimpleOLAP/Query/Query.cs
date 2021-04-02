using NSimpleOLAP.Query.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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

    protected Cube<T> cube;

    internal Cube<T> Cube
    {
      get { return cube; }
    }

    internal Axis<T> Axis
    {
      get { return axis; }
    }

    internal IPredicate<T> PredicateTree
    {
      get { return predicates; }
    }

    public IEnumerable<Cell<T>> Run()
    {
      return null;
    }
  }
}