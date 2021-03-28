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
  public class MolapQueryOrchestrator<T, U> : IQueryOrchestrator<T, U>
    where T : struct, IComparable
    where U : class, ICell<T>
  {
    private MolapStorage<T, U> _storage;

    MolapQueryOrchestrator(MolapStorage<T, U> storage)
    {
      _storage = storage;

      
    }

    public IEnumerable<U> Run(Query<T> query)
    {
      throw new NotImplementedException();
    }
  }
}
