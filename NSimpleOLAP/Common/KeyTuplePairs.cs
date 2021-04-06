using System;
using System.Collections.Generic;
using System.Linq;

namespace NSimpleOLAP.Common
{
  public class KeyTuplePairs<T>
    where T : struct, IComparable
  {
    private readonly bool _sameValue;
    public KeyTuplePairs(KeyValuePair<T, T>[] anchor, KeyValuePair<T, T>[] selector)
    {
      AnchorTuple = anchor;
      SelectorTuple = selector;

      _sameValue = anchor.Length == selector.Length;
    }

    public bool HasSelectors
    {
      get
      {
        return !_sameValue;
      }
    }

    public KeyValuePair<T, T>[] AnchorTuple { get; private set; }

    public KeyValuePair<T, T>[] SelectorTuple { get; private set; }

    public KeyValuePair<T, T>? GetSelector(KeyValuePair<T, T> value)
    {
      var query = SelectorTuple.ToList();
      var index = query
        .FindIndex(x => value.Key.Equals(x.Key) && value.Value.Equals(x.Value));

      if (index + 1 < SelectorTuple.Length)
      {
        var result = SelectorTuple[index + 1];

        if (result.IsReservedValue())
          return result;
      }

      return null;
    }

    public int SelectorCount()
    {
      var query = SelectorTuple
        .Where(x => x.IsReservedValue());

      return query.Count();
    }
  }
}