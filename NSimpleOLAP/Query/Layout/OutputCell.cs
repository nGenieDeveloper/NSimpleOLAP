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
      Init();
    }

    private void Init()
    {
      if (XCoords.Length > 0)
      {
        var xcoords2 = new KeyValuePair<T, T>[XCoords.Length];
        
        Array.Copy(XCoords, xcoords2, XCoords.Length);
        ReplaceDefaultSegments(Coords, xcoords2);
        XCoords = xcoords2;
      }

      if (YCoords.Length > 0)
      {
        var ycoords2 = new KeyValuePair<T, T>[YCoords.Length];

        Array.Copy(YCoords, ycoords2, YCoords.Length);
        ReplaceDefaultSegments(Coords, ycoords2);
        YCoords = ycoords2;
      }
    }

    private void ReplaceDefaultSegments(KeyValuePair<T, T>[] coords, KeyValuePair<T, T>[] destiny)
    {
      var query2 = destiny
        .Where(x => x.Value.Equals(default(T)))
        .Select((x, i) => new { Pair2 = x, Index2 = i })
        .ToArray();

      if (query2.Length > 0)
      {
        var query = coords
        .Select((x, i) => new { Pair1 = x, Index1 = i });
        var query3 = query
          .Join(query2, x => x.Pair1.Key, y => y.Pair2.Key, (x, y) => new { Source = x, Target = y })
          .ToArray();

        foreach (var item in query3)
          destiny[item.Target.Index2] = item.Source.Pair1;
      }
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