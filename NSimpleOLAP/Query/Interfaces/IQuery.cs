using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSimpleOLAP.Interfaces;

namespace NSimpleOLAP.Query.Interfaces
{
  public interface IQuery<T, U>
    where T : struct, IComparable
    where U : class, ICell<T>
  {
    IEnumerable<U> Run();
  }
}
