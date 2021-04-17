using NSimpleOLAP.CubeExpressions.Interfaces;
using NSimpleOLAP.Interfaces;
using System;

namespace NSimpleOLAP.CubeExpressions
{
  public abstract class CellContext<T> : IExpressionContext<T, ICell<T>>
    where T : struct, IComparable
  {
    public ICell<T> CurrentCell
    {
      get;
      private set;
    }

    public ICell<T> RootCell { get; }
  }
}