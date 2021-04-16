using NSimpleOLAP.CubeExpressions.Interfaces;
using System;
using NSimpleOLAP.Interfaces;

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