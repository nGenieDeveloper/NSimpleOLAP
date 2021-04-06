using NSimpleOLAP.Common;
using NSimpleOLAP.Common.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NSimpleOLAP.Query
{
  /// <summary>
  /// Description of Axis.
  /// </summary>
  public class Axis<T>
    where T : struct, IComparable
  {
    private Hasher<T> _hasher;
    private List<KeyValuePair<T, T>[]> _rowsAxis;
    private List<KeyValuePair<T, T>[]> _columnsAxis;
    private List<T> _rowHashes;
    private List<T> _columnHashes;

    public Axis(MolapHashTypes hashingtype)
    {
      _rowsAxis = new List<KeyValuePair<T, T>[]>();
      _columnsAxis = new List<KeyValuePair<T, T>[]>();
      _rowHashes = new List<T>();
      _columnHashes = new List<T>();
      _hasher = Hasher<T>.Create(hashingtype);
    }

    #region props

    public IEnumerable<KeyValuePair<T, T>[]> RowAxis
    {
      get { return _rowsAxis; }
    }

    public IEnumerable<KeyValuePair<T, T>[]> ColumnAxis
    {
      get { return _columnsAxis; }
    }

    public IEnumerable<KeyValuePair<T, T>> UnionAxis
    {
      get { return GetAllUniquePairs(); }
    }

    public IEnumerable<KeyTuplePairs<T>> UnionAxisTuples
    {
      get { return GetAllUniquePairs(GetTuplePairs()); }
    }

    #endregion props

    #region public methods

    public void AddRowTuples(params KeyValuePair<T, T>[] tuples)
    {
      T hash = _hasher.HashTuples(tuples);

      if (!_rowHashes.Contains(hash))
      {
        _rowsAxis.Add(tuples);
        _rowHashes.Add(hash);
      }
    }

    public void AddColumnTuples(params KeyValuePair<T, T>[] tuples)
    {
      T hash = _hasher.HashTuples(tuples);

      if (!_columnHashes.Contains(hash))
      {
        _columnsAxis.Add(tuples);
        _columnHashes.Add(hash);
      }
    }

    #endregion public methods

    #region private methods

    private bool ContainsDimension(KeyValuePair<T, T>[] tuples)
    {
      return tuples.Any(x => IsDimention(x));
    }

    private IEnumerable<KeyValuePair<T, T>> GetAllUniquePairs()
    {
      var query = GetAllPairs()
        .Where(x => !x.IsReservedValue())
        .Distinct(new KeyEqualityComparer<T>())
        .OrderBy(x => x, new KeyComparer<T>());

      foreach (var item in query)
        yield return item;
    }

    private IEnumerable<KeyTuplePairs<T>> GetAllUniquePairs(IEnumerable<KeyValuePair<T, T>[]> tuples)
    {
      foreach (var item in tuples)
      {
        var query = item
        .Where(x => !x.IsReservedValue())
        .Distinct(new KeyEqualityComparer<T>())
        .OrderBy(x => x, new KeyComparer<T>());
        var selectors = item
          .Select((x, index) => new { Pair = x, Index = index })
          .Where(x => x.Pair.IsReservedValue())
          .ToArray();
        var list = query.ToList();
        var anchor = list.ToArray();
        // to do optimize this
        foreach (var selector in selectors)
        {
          var value = item[selector.Index - 1];
          var index = list
            .FindIndex(x => value.Key.Equals(x.Key) && value.Value.Equals(x.Value));

          list.Insert(index + 1, selector.Pair);
        }

        yield return new KeyTuplePairs<T>(anchor, list.ToArray());
      }
    }

    private IEnumerable<KeyValuePair<T, T>> GetAllPairs()
    {
      foreach (var item in _rowsAxis)
      {
        foreach (var pair in item)
          yield return pair;
      }

      foreach (var item in _columnsAxis)
      {
        foreach (var pair in item)
          yield return pair;
      }
    }

    private IEnumerable<KeyValuePair<T, T>[]> GetTuplePairs()
    {
      foreach (var col in _columnsAxis)
      {
        foreach (var row in _rowsAxis)
        {
          var result = new KeyValuePair<T, T>[col.Length + row.Length];

          col.CopyTo(result, 0);
          row.CopyTo(result, col.Length);

          yield return result;
        }
      }
    }

    private bool IsDimention(KeyValuePair<T, T> dim)
    {
      var dvalue = default(T);

      return !dim.Key.Equals(dvalue)
        && dim.Value.Equals(dvalue);
    }

    #endregion private methods
  }
}