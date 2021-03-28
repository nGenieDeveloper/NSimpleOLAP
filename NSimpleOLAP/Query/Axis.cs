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
      var query = GetAllPairs().Distinct().OrderBy(x => x);

      foreach (var item in query)
        yield return item;
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

    private bool IsDimention(KeyValuePair<T, T> dim)
    {
      var dvalue = default(T);

      return !dim.Key.Equals(dvalue)
        && dim.Value.Equals(dvalue);
    }

    #endregion private methods
  }
}