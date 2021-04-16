﻿using NSimpleOLAP.CubeExpressions.Interfaces;
using System;
using NSimpleOLAP.Interfaces;

namespace NSimpleOLAP.CubeExpressions
{
  public abstract class MetricsExpression<T> : IExpression<T, ICell<T>>
    where T : struct, IComparable
  {
    protected T id;
    protected Type returnType;
    protected Func<IExpressionContext<T, ICell<T>>, object> expression;

    public T ID
    {
      get
      {
        return id;
      }
    }

    public Type ReturnType
    {
      get
      {
        return returnType;
      }
    }

    public object Evaluate(IExpressionContext<T, ICell<T>> context)
    {
      return expression(context);
    }

    public V Evaluate<V>(IExpressionContext<T, ICell<T>> context)
    {
      return (V)expression(context);
    }
  }
}