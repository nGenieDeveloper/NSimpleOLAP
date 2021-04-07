using System;
using System.Collections.Generic;

namespace NSimpleOLAP.Common
{
  internal class KeyEqualityComparer<T> : IEqualityComparer<KeyValuePair<T, T>>
    where T : struct, IComparable
  {
    public bool Equals(KeyValuePair<T, T> x, KeyValuePair<T, T> y)
    {
      return x.Equals(y);
    }

    public int GetHashCode(KeyValuePair<T, T> obj)
    {
      return obj.Key.GetHashCode()
        ^ obj.Value.GetHashCode();
    }
  }

  internal class KeyComparer<T> : IComparer<KeyValuePair<T, T>>
    where T : struct, IComparable
  {
    public int Compare(KeyValuePair<T, T> x, KeyValuePair<T, T> y)
    {
      var keyCompare = x.Key.CompareTo(y.Key);

      if (keyCompare != 0)
        return keyCompare;

      return x.Value.CompareTo(y.Value);
    }
  }

  internal class AllKeyComparer<T> : IComparer<KeyValuePair<T, T>>
    where T : struct, IComparable
  {
    public int Compare(KeyValuePair<T, T> x, KeyValuePair<T, T> y)
    {
      var keyCompare = x.Key.CompareTo(y.Key);

      return keyCompare;
    }
  }
}