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
  public class ExpressionElementsBuilder<T>
   where T : struct, IComparable
  {
    private DimensionReferenceTranslator<T> _dimTranslator;
    private MeasureReferenceTranslator<T> _measTranslator;
    private ExpressionNodeBuilder<T> _node;
    private Type _type;

    public ExpressionElementsBuilder(DimensionReferenceTranslator<T> dimTranslator, MeasureReferenceTranslator<T> measTranslator)
    {
      _dimTranslator = dimTranslator;
      _measTranslator = measTranslator;
    }

    public ExpressionNodeBuilder<T> Set(string measure)
    {
      var picker = new ExpressionElementPickerBuilder<T>(_dimTranslator, _measTranslator);

      _node = new ExpressionNodeBuilder<T>(picker.Set(measure), _dimTranslator, _measTranslator);
      _type = _node.ReturnType;

      return _node;
    }

    public ExpressionNodeBuilder<T> Set(string measure, params string[] tuples)
    {
      var picker = new ExpressionElementPickerBuilder<T>(_dimTranslator, _measTranslator);

      _node = new ExpressionNodeBuilder<T>(picker.Set(measure, tuples), _dimTranslator, _measTranslator);
      _type = _node.ReturnType;

      return _node;
    }

    internal Type ReturnType
    {
      get
      {
        return _type;
      }
    }

    internal Func<IExpressionContext<T, ICell<T>>, IExpressionContext<T, ICell<T>>> Create()
    {
      return _node.Create();
    }
  }
}
