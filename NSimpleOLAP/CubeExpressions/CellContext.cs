using NSimpleOLAP.CubeExpressions.Interfaces;
using NSimpleOLAP.Interfaces;
using System;
using System.Collections.Generic;

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

    public object Result { get; set; }

    public IDictionary<T, ValueType> PreviousValues { get; private set; }

    public IDictionary<T, ValueType> NewValues { get; private set; }
  }
}