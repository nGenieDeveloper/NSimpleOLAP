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
  public class ExpressionElementsBuilder<T>
   where T : struct, IComparable
  {
    private DimensionReferenceTranslator<T> _dimTranslator;
    private MeasureReferenceTranslator<T> _measTranslator;
    private ExpressionNodeBuilder<T> _node;

    public ExpressionElementsBuilder(DimensionReferenceTranslator<T> dimTranslator, MeasureReferenceTranslator<T> measTranslator)
    {
      _dimTranslator = dimTranslator;
      _measTranslator = measTranslator;
    }

    public ExpressionNodeBuilder<T> Set(string measure)
    {
      var picker = new ExpressionElementPickerBuilder<T>(_dimTranslator, _measTranslator);

      _node = new ExpressionNodeBuilder<T>(picker.Set(measure), _dimTranslator, _measTranslator);

      return _node;
    }

    public ExpressionNodeBuilder<T> Set(string measure, params string[] tuples)
    {
      var picker = new ExpressionElementPickerBuilder<T>(_dimTranslator, _measTranslator);

      _node = new ExpressionNodeBuilder<T>(picker.Set(measure, tuples), _dimTranslator, _measTranslator);

      return _node;
    }

    public void Create()
    {
      _node.Create();
    }
  }
}
