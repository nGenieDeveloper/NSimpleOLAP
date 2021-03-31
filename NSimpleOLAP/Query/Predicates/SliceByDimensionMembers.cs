﻿using System;
using System.Linq;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Data;
using NSimpleOLAP.Query.Interfaces;

namespace NSimpleOLAP.Query.Predicates
{
  /// <summary>
  /// Description of SliceByDimensionMember.
  /// </summary>
  internal class SliceByDimensionMembers<T> : IPredicate<T>
    where T: struct, IComparable
  {
    private T _dimension;
    private LogicalOperators _operator;
    private List<T> _values;
    
    public SliceByDimensionMembers(T dimensionKey, LogicalOperators loperator, params T[] values)
    {
      _values = new List<T>();
      _dimension = dimensionKey;
      _operator = loperator;
      _values.AddRange(values);
    }
    
    public T Dimension
    {
      get { return _dimension; }
    }
    
    public LogicalOperators Operator
    {
      get { return _operator; }
    }
    
    public IEnumerable<T> Values
    {
      get { return _values; }
    }
    
    public PredicateType TypeOf 
    {
      get { return PredicateType.DIMENSION; }
    }

    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    public bool Execute(KeyValuePair<T, T>[] pairs, MeasureValuesCollection<T> data)
    {
      var results = pairs
        .Where(x => x.Key.Equals(Dimension))
        .ToArray();

      if (results.Length > 0)
      {
        return results
          .Join(Values, x => x.Value, y => y, (x, y) => x)
          .Any(); // to do change this
      }

      return false;
    }

    public bool FiltersOnAggregation()
    {
      return true;
    }

    public bool FiltersOnFacts()
    {
      return false;
    }

    public override int GetHashCode()
    {
      var result = TypeOf.GetHashCode()
        ^ Operator.GetHashCode();

      foreach (var item in _values)
        result ^= item.GetHashCode();

      return result;
    }
  }
}
