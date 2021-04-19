using NSimpleOLAP.Common.Utils;
using NSimpleOLAP.CubeExpressions.Interfaces;
using NSimpleOLAP.Interfaces;
using System;

namespace NSimpleOLAP.CubeExpressions.Builder
{
  public class MetricExpressionBuilder<T>
    where T : struct, IComparable
  {
    private string _name;
    private DimensionReferenceTranslator<T> _dimTranslator;
    private MeasureReferenceTranslator<T> _measTranslator;
    private ExpressionBuilder<T> _expressionBuilder;

    public MetricExpressionBuilder(DimensionReferenceTranslator<T> dimTranslator, MeasureReferenceTranslator<T> measTranslator)
    {
      _dimTranslator = dimTranslator;
      _measTranslator = measTranslator;
    }

    public ExpressionBuilder<T> Metric(string name)
    {
      _name = name;

      _expressionBuilder = new ExpressionBuilder<T>(_dimTranslator, _measTranslator);

      return _expressionBuilder;
    }

    public Func<IExpressionContext<T, ICell<T>>, object> Create()
    {
      var functor = _expressionBuilder.Create();
      Func<IExpressionContext<T, ICell<T>>, object> capsule = x =>
      {
        IExpressionContext<T, ICell<T>> result = functor(x);

        return null; // todo
      };

      return capsule;
    }
  }
}