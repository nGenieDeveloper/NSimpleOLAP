﻿using System;
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
    private NamespaceResolver<T> _resolver;
    private ExpressionNodeBuilder<T> _node;
    private Type _type;

    public ExpressionElementsBuilder(NamespaceResolver<T> resolver)
    {
      _resolver = resolver;
    }

    public ExpressionNodeBuilder<T> Set(string measure)
    {
      var picker = new ExpressionElementPickerBuilder<T>(_resolver);

      _node = new ExpressionNodeBuilder<T>(picker.Set(measure), _resolver);
      _type = _node.ReturnType;

      return _node;
    }

    public ExpressionNodeBuilder<T> Set(string measure, params string[] tuples)
    {
      var picker = new ExpressionElementPickerBuilder<T>(_resolver);

      _node = new ExpressionNodeBuilder<T>(picker.Set(measure, tuples), _resolver);
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
