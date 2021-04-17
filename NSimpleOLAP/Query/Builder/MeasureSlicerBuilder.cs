using NSimpleOLAP.Common;
using NSimpleOLAP.Query.Interfaces;
using NSimpleOLAP.Query.Predicates;
using NSimpleOLAP.Schema;
using System;
using NSimpleOLAP.Common.Utils;

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
    private string _measureName;

    public MeasureSlicerBuilder(DataSchema<T> schema, MeasureReferenceTranslator<T> translator)
    {
      _schema = schema;
      _translator = translator;
    }

    #region fluent interface

    internal MeasureSlicerBuilder<T> SetMeasure(string measure)
    {
      _measureName = measure;
      _measure = _translator.Translate(measure);
      _valueType = _translator.MeasureType(_measure);

      return this;
    }

    internal MeasureSlicerBuilder<T> SetMeasure(T measureKey)
    {
      _measure = measureKey;
      _valueType = _translator.MeasureType(_measure);
      _measureName = _translator.MeasureName(measureKey);

      return this;
    }

    internal MeasureSlicerBuilder<T> SetOperationValuePair(LogicalOperators loperator, object value)
    {
      if (!value.CompatibleType(_valueType))
        throw new Exception($"Attempting to make operation with incompatible value type on Measure {_measureName}.");

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