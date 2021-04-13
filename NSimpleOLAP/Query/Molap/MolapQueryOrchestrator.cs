using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSimpleOLAP.Common;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Data;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Storage.Interfaces;
using NSimpleOLAP.Query.Interfaces;
using NSimpleOLAP.Storage.Molap;
using NSimpleOLAP.Query.Layout;

namespace NSimpleOLAP.Query.Molap
{
  internal class MolapQueryOrchestrator<T> : IQueryOrchestrator<T, IOutputCell<T>>
    where T : struct, IComparable
  {
    private Cube<T> _cube;
    private AllKeyComparer<T> _allKeyComparer;
    private KeysBaseEqualityComparer<T> _pairsEqualityComparer;

    public MolapQueryOrchestrator(Cube<T> cube)
    {
      _cube = cube;
      _allKeyComparer = new AllKeyComparer<T>();
      _pairsEqualityComparer = new KeysBaseEqualityComparer<T>();
    }

    public IEnumerable<IOutputCell<T>> Run(Query<T> query)
    {
      if (query.PredicateTree.FiltersOnFacts())
      {
        var id = CreateNewOrReuseAggregation(query);

        return GetCells(id, query);
      }

      return GetCells(query);
    }

    public IEnumerable<IOutputCell<T>[]> Run2(Query<T> query)
    {
      if (query.PredicateTree.FiltersOnFacts())
      {
        var id = CreateNewOrReuseAggregation(query);

        return LayerByRow(GetCells(id, query), query);
      }

      return LayerByRow(GetCells(query), query);
    }

    private T CreateNewOrReuseAggregation(Query<T> query)
    {
      var tuples = query.Axis.UnionAxis.ToArray();
      T cubeId;

      if (!_cube.Storage.AggregationExists(tuples, query.PredicateTree))
        cubeId = _cube.Storage.CreateAggregation(tuples, query.PredicateTree);
      else
        cubeId = _cube.Storage.GetAggregationId(tuples, query.PredicateTree);

      return cubeId;
    }

    private IEnumerable<IOutputCell<T>> GetCells(T aggregationId, Query<T> query)
    {
      var tuples = query.Axis.UnionAxisTuples;

      foreach (var tuple in tuples)
      {
        var cells = _cube.Storage.GetCells(aggregationId, tuple);

        foreach (var cell in cells)
          yield return Map(cell, tuple, query);
      }
    }

    private IEnumerable<IOutputCell<T>> GetCells(Query<T> query)
    {
      var tuples = query.Axis.UnionAxisTuples;

      foreach (var tuple in tuples)
      {
        var cells = _cube.Storage.GetCells(tuple);

        foreach (var cell in cells)
          yield return Map(cell, tuple, query);
      }
    }

    private IOutputCell<T> Map(Cell<T> cell, KeyTuplePairs<T> tuples, Query<T> query)
    {
      var ocell = new OutputCell<T>(cell.Coords, tuples.XAnchorTuple, tuples.YAnchorTuple);

      foreach (var item in query.Measures)
      {
        var measure = _cube.Schema.Measures[item];
        var value = cell.Values[item];

        ocell.Add(measure.Name, value);
      }

      return ocell;
    }

    private IEnumerable<IOutputCell<T>[]> LayerByRow(IEnumerable<IOutputCell<T>> cells, Query<T> query)
    {
      var cols = (from item in query.Axis.ColumnAxis
                 let result = item.Where(x => !x.IsWildcard<T>()).ToArray()
                 select result).ToArray();
      var rows = (from item in query.Axis.RowAxis
                 let result = item.Where(x => !x.IsWildcard<T>()).ToArray()
                 select result).ToArray();
      var ocells = cells.OrderBy(x => x.Coords, new AllKeysComparer<T>()).ToArray();
      var colsSegments = ocells.Select(x => x.XCoords).Distinct(_pairsEqualityComparer).ToArray();
      var rowSegments = ocells.Select(x => x.YCoords).Distinct(_pairsEqualityComparer).ToArray();

      var index = 0;

      if (cols.Length > 0 && rows.Length > 0)
      {
        foreach (var row in rowSegments)
        {
          var values = new IOutputCell<T>[colsSegments.Length];

          for (var i = 0; i< colsSegments.Length; i++)
          {
            var col = colsSegments[i];
            var cell = ocells[index];

            if (index < ocells.Length
              && _pairsEqualityComparer.Equals(cell.XCoords, col)
              && _pairsEqualityComparer.Equals(cell.YCoords, row))
            {
              values[i] = cell;
              index++;
            }
          }

          yield return values;
        }
      }
    }

    private bool FilterCell(KeyValuePair<T, T>[] cellCoords, KeyValuePair<T, T>[] scoords)
    {
      bool ret = true;

      foreach (var item in scoords)
      {
        int val = Array.BinarySearch(cellCoords, item, _allKeyComparer);

        if (val < 0)
        {
          ret = false;
          break;
        }
      }

      return ret;
    }
  }
}
