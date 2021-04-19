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
  public class ExpressionBuilder<T>
    where T : struct, IComparable
  {
    private ExpressionElementsBuilder<T> _expressionRoot;
    private DimensionReferenceTranslator<T> _dimTranslator;
    private MeasureReferenceTranslator<T> _measTranslator;

    public ExpressionBuilder(DimensionReferenceTranslator<T> dimTranslator, MeasureReferenceTranslator<T> measTranslator)
    {
      _dimTranslator = dimTranslator;
      _measTranslator = measTranslator;
    }

    public void Expression(Action<ExpressionElementsBuilder<T>> expression)
    {
      _expressionRoot = new ExpressionElementsBuilder<T>(_dimTranslator, _measTranslator);

      expression(_expressionRoot);
    }

    internal Func<IExpressionContext<T, ICell<T>>, IExpressionContext<T, ICell<T>>> Create()
    {
      return _expressionRoot.Create();
    }
  }
}
