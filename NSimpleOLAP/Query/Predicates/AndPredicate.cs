using NSimpleOLAP.Common;
using NSimpleOLAP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using NSimpleOLAP.Query.Interfaces;

namespace NSimpleOLAP.Query.Predicates
{
  /// <summary>
  /// Description of AndPredicate.
  /// </summary>
  internal class AndPredicate<T> : IPredicate<T>
    where T : struct, IComparable
  {
    private List<IPredicate<T>> _predicates;

    public AndPredicate()
    {
      _predicates = new List<IPredicate<T>>();
    }

    public void AddPredicate(params IPredicate<T>[] predicates)
    {
      _predicates.AddRange(predicates);
    }

    public IEnumerable<IPredicate<T>> Predicates
    {
      get { return _predicates; }
    }

    public PredicateType TypeOf
    {
      get { return PredicateType.AND; }
    }

    public bool Execute(KeyValuePair<T, T>[] pairs, MeasureValuesCollection<T> data)
    {
      var result = false;

      if (_predicates.Count > 0)
      {
        var query = _predicates.Skip(1);

        result = _predicates[0].Execute(pairs, data);

        if (!result)
          return result;

        foreach (var item in query)
        {
          result = result && item.Execute(pairs, data);

          if (!result)
          {
            break;
          }
        }
      }

      return result;
    }

    public bool FiltersOnFacts()
    {
      foreach (var item in _predicates)
      {
        if (item.FiltersOnFacts())
          return true;
      }

      return false;
    }

    public bool FiltersOnAggregation()
    {
      foreach (var item in _predicates)
      {
        if (item.FiltersOnAggregation())
          return true;
      }

      return false;
    }

    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    public override int GetHashCode()
    {
      var result = TypeOf.GetHashCode();

      foreach (var item in _predicates)
        result ^= item.GetHashCode();

      return result;
    }
  }
}