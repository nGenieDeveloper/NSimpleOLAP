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
      throw new NotImplementedException();
    }
  }
}
