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


namespace NSimpleOLAP.Query.Interfaces
{
  public interface IQueryOrchestrator<T, U>
    where T : struct, IComparable
    where U : class, ICell<T>
  {
    IEnumerable<U> Run(Query<T> query);
  }
}
