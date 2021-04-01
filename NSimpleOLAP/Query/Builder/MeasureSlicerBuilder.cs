﻿using NSimpleOLAP.Common;
using NSimpleOLAP.Query.Interfaces;
using NSimpleOLAP.Query.Predicates;
using NSimpleOLAP.Schema;
using System;

namespace NSimpleOLAP.Query.Builder
{
  /// <summary>
  /// Description of MeasureSlicerBuilder.
  /// </summary>
  public class MeasureSlicerBuilder<T> : IPredicateBuilder<T>
    where T : struct, IComparable
  {
    private DataSchema<T> _schema;
    private T _measure;
    private object _value;
    private LogicalOperators _operator;
    private Type _valueType;
    private MeasureReferenceTranslator<T> _translator;

    public MeasureSlicerBuilder(DataSchema<T> schema, MeasureReferenceTranslator<T> translator)
    {
      _schema = schema;
      _translator = translator;
    }

    #region fluent interface

    internal MeasureSlicerBuilder<T> SetMeasure(string measure)
    {
      _measure = _translator.Translate(measure);
      _valueType = _translator.MeasureType(_measure);

      return this;
    }

    internal MeasureSlicerBuilder<T> SetMeasure(T measureKey)
    {
      _measure = measureKey;
      _valueType = _translator.MeasureType(_measure);

      return this;
    }

    internal MeasureSlicerBuilder<T> SetOperationValuePair(LogicalOperators loperator, object value)
    {
      _operator = loperator;
      _value = value;

      return this;
    }

    #endregion fluent interface

    public IPredicate<T> Build()
    {
      return new SliceByMeasure<T>(_measure, _valueType, _operator, _value);
    }
  }
}