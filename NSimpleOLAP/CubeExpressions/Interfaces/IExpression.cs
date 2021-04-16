using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSimpleOLAP.Interfaces;

namespace NSimpleOLAP.CubeExpressions.Interfaces
{
  public interface IExpression<T, U, C>
    where T: struct, IComparable
    where U : class, ICell<T>
    where C : ICube<T, U>
  {

    T ID { get; }

    Type ReturnType { get; }

    object Evaluate(IExpressionContext<T, U, C> context);

    V Evaluate<V>(IExpressionContext<T, U, C> context);
  }
}
