using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSimpleOLAP.Query.Interfaces;
using NSimpleOLAP.Common.Utils;
using NSimpleOLAP.Common;


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

    public void Create()
    {
      _expressionBuilder.Create();
    }
  }
}
