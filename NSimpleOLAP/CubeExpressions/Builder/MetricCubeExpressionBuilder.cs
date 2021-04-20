﻿using NSimpleOLAP.Common.Utils;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Schema;
using System;
using System.Collections.Generic;

namespace NSimpleOLAP.CubeExpressions.Builder
{
  public abstract class MetricCubeExpressionBuilder<T>
    where T : struct, IComparable
  {
    protected Cube<T> _innerCube;
    protected DimensionReferenceTranslator<T> _dimTranslator;
    protected MeasureReferenceTranslator<T> _measTranslator;
    protected Dictionary<string, MetricExpressionBuilder<T>> _expressionBuilders;

    protected void Init()
    {
      _dimTranslator = new DimensionReferenceTranslator<T>(_innerCube.Schema);
      _measTranslator = new MeasureReferenceTranslator<T>(_innerCube.Schema);
      _expressionBuilders = new Dictionary<string, MetricExpressionBuilder<T>>();
    }

    public MetricCubeExpressionBuilder<T> Add(string name, Action<ExpressionBuilder<T>> abuilder)
    {
      var builder = new MetricExpressionBuilder<T>(_dimTranslator, _measTranslator);
      var expressionBuilder = builder.Metric(name);

      abuilder(expressionBuilder);
      _expressionBuilders.Add(name, builder);

      return this;
    }

    public void Create()
    {
      foreach (var item in _expressionBuilders)
      {
        var expression = item.Value.Create();

        _innerCube.Schema.Metrics.Add(new Metric<T>
        {
          MetricExpression = expression,
          Name = item.Key,
          DataType = expression.ReturnType,
          Config = new MetricConfig
          {
            DataType = expression.ReturnType,
            Name = item.Key
          }
        });
      }
    }
  }
}