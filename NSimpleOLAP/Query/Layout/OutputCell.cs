using NSimpleOLAP.Query.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NSimpleOLAP.Query.Layout
{
  public class OutputCell<T> : IOutputCell<T>
    where T : struct, IComparable
  {
    private readonly Dictionary<string, object> _values;

    public OutputCell(KeyValuePair<T, T>[] coords, KeyValuePair<T, T>[] xcoords, KeyValuePair<T, T>[] ycoords)
    {
      _values = new Dictionary<string, object>();
      Coords = coords;
      XCoords = xcoords;
      YCoords = ycoords;
    }

    public object this[string key] => _values[key];

    public object this[int key]
    {
      get
      {
        return _values.ToArray()[key];
      }
    }

    public KeyValuePair<T, T>[] Coords
    {
      get;
      private set;
    }

    public KeyValuePair<T, T>[] XCoords
    {
      get;
      private set;
    }

    public KeyValuePair<T, T>[] YCoords
    {
      get;
      private set;
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      foreach (var item in _values)
        yield return item;
    }

    internal void Add(string measure, object value)
    {
      _values.Add(measure, value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      foreach (var item in _values)
        yield return item;
    }
  }
}