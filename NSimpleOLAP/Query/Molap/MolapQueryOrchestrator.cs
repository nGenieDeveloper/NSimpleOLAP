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

namespace NSimpleOLAP.Query.Molap
{
  internal class MolapQueryOrchestrator<T> : IQueryOrchestrator<T, Cell<T>>
    where T : struct, IComparable
  {
    private Cube<T> _cube;

    public MolapQueryOrchestrator(Cube<T> cube)
    {
      _cube = cube;
    }

    public IEnumerable<Cell<T>> Run(Query<T> query)
    {
      if (query.PredicateTree.FiltersOnFacts())
      {
        var id = CreateNewOrReuseAggregation(query);

        return GetCells(id, query);
      }

      return GetCells(query);
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

    private IEnumerable<Cell<T>> GetCells(T aggregationId, Query<T> query)
    {
      var tuples = query.Axis.UnionAxisTuples;

      foreach (var tuple in tuples)
      {
        var cells = _cube.Storage.GetCells(aggregationId, tuple);

        foreach (var cell in cells)
          yield return cell;
      }
    }

    private IEnumerable<Cell<T>> GetCells(Query<T> query)
    {
      var tuples = query.Axis.UnionAxisTuples;

      foreach (var tuple in tuples)
      {
        var cells = _cube.Storage.GetCells(tuple);

        foreach (var cell in cells)
          yield return cell;
      }
    }
  }
}
