using NSimpleOLAP.Interfaces;
using System;

namespace NSimpleOLAP.CubeExpressions.Interfaces
{
  public interface IExpressionContext<T, U>
    where T : struct, IComparable
    where U : class, ICell<T>
  {
    U CurrentCell { get; }

    U RootCell { get; }
  }
}