using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSimpleOLAP.Query.Interfaces;
using NSimpleOLAP.Common.Utils;
using NSimpleOLAP.Common;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.CubeExpressions.Interfaces;

namespace NSimpleOLAP.CubeExpressions.Builder
{
  public class ExpressionNodeBuilder<T>
   where T : struct, IComparable
  {
    private DimensionReferenceTranslator<T> _dimTranslator;
    private MeasureReferenceTranslator<T> _measTranslator;
    private ExpressionElementPickerBuilder<T> _picker;
    private OperationType _operation;
    private ValueType _value;
    private ExpressionElementsBuilder<T> _leftNodeBuilder;

    public ExpressionNodeBuilder(ExpressionElementPickerBuilder<T> picker, DimensionReferenceTranslator<T> dimTranslator, MeasureReferenceTranslator<T> measTranslator)
    {
      _dimTranslator = dimTranslator;
      _measTranslator = measTranslator;
      _picker = picker;
      _leftNodeBuilder = new ExpressionElementsBuilder<T>(_dimTranslator, _measTranslator);
    }

    public void Sum<V>(V value)
      where V : struct
    {
      _operation = OperationType.SUM;
      _value = value;
    }

    public void Sum(Action<ExpressionElementsBuilder<T>> builder)
    {
      _operation = OperationType.SUM;

      builder(_leftNodeBuilder);
    }

    public void Subtract<V>(V value)
      where V : struct
    {
      _operation = OperationType.SUBTRACTION;
      _value = value;
    }

    public void Subtract(Action<ExpressionElementsBuilder<T>> builder)
    {
      _operation = OperationType.SUBTRACTION;

      builder(_leftNodeBuilder);
    }

    public void Multiply<V>(V value)
      where V : struct
    {
      _operation = OperationType.MULTIPLICATION;
      _value = value;
    }

    public void Multiply(Action<ExpressionElementsBuilder<T>> builder)
    {
      _operation = OperationType.MULTIPLICATION;

      builder(_leftNodeBuilder);
    }

    public void Divide<V>(V value)
      where V : struct
    {
      V defvalue = default(V);

      if (defvalue.Equals(value))
        throw new Exception("Division by 0 is not allowed!");

      _operation = OperationType.DIVISION;
      _value = value;
    }

    public void Divide(Action<ExpressionElementsBuilder<T>> builder)
    {
      _operation = OperationType.DIVISION;

      builder(_leftNodeBuilder);
    }

    public void Average()
    {
      _operation = OperationType.AVERAGE;
    }

    public void Value()
    {
      _operation = OperationType.VALUE;
    }

    public void Max()
    {
      _operation = OperationType.MAX;
    }

    public void Min()
    {
      _operation = OperationType.MAX;
    }

    internal Func<IExpressionContext<T, ICell<T>>, IExpressionContext<T, ICell<T>>> Create()
    {
      return null;
    }


  }
}
