using System;
using System.Collections.Generic;

namespace NSimpleOLAP.Query.Interfaces
{
  public interface IOutputCell<T> : IEnumerable<KeyValuePair<string, object>>
    where T : struct, IComparable
  {
    object this[string key] { get; }

    object this[int key] { get; }

    KeyValuePair<T, T>[] Coords { get; }

    KeyValuePair<T, T>[] XCoords { get; }

    KeyValuePair<T, T>[] YCoords { get; }
  }
}