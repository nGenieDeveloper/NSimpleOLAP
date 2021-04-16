using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSimpleOLAP.Interfaces;

namespace NSimpleOLAP.CubeExpressions.Interfaces
{
  public interface IExpressionContext<T, U, C>
    where T : struct, IComparable
    where U : class, ICell<T>
    where C : ICube<T, U>
  {
    U CurrentCell { get; }

    C Cube { get; }
  }
}
