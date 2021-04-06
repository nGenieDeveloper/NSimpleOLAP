using System;
using System.Collections.Generic;

namespace NSimpleOLAP.Common
{
  public class KeyTuplePairs<T>
    where T : struct, IComparable
  {
    public KeyTuplePairs(KeyValuePair<T, T>[] anchor, KeyValuePair<T, T>[] selector)
    {
      AnchorTuple = anchor;
      SelectorTuple = selector;
    }

    public KeyValuePair<T, T>[] AnchorTuple { get; private set; }

    public KeyValuePair<T, T>[] SelectorTuple { get; private set; }
  }
}