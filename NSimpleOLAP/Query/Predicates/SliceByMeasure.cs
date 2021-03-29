using NSimpleOLAP.Common;
using NSimpleOLAP.Data;
using System;
using System.Collections.Generic;

namespace NSimpleOLAP.Query.Predicates
{
  /// <summary>
  /// Description of SliceByMeasure.
  /// </summary>
  internal class SliceByMeasure<T> : IPredicate<T>
    where T : struct, IComparable
  {
    private T _measure;
    private LogicalOperators _operator;
    private object _value;
    private DataValueType _dataValueType;

    public SliceByMeasure(T measureKey, DataValueType valueType,
                          LogicalOperators loperator, object value)
    {
      _measure = measureKey;
      _operator = loperator;
      _dataValueType = valueType;
      _value = value;
    }

    public T MeasureKey
    {
      get { return _measure; }
    }

    public LogicalOperators Operator
    {
      get { return _operator; }
    }

    public DataValueType DataValueType
    {
      get { return _dataValueType; }
    }

    public object Value
    {
      get { return _value; }
    }

    public PredicateType TypeOf
    {
      get { return PredicateType.MEASURE; }
    }

    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    public bool Execute(KeyValuePair<T, T>[] pairs, MeasureValuesCollection<T> data)
    {
      if (data.ContainsKey(MeasureKey))
      {
        var value = data[MeasureKey];

        switch (_operator)
        {
          case LogicalOperators.EQUALS:
            return _value.Equals(value);

          case LogicalOperators.NOTEQUALS:
            return !_value.Equals(value);

          case LogicalOperators.GREATERTHAN:
            return _value.GreaterThan(value);

          case LogicalOperators.GREATEROREQUALS:
            return _value.GreaterOrEquals(value);

          case LogicalOperators.LOWERTHAN:
            return _value.LowerThan(value);

          case LogicalOperators.LOWEROREQUALS:
            return _value.LowerOrEquals(value);
        }
      }

      return false;
    }

    public bool FiltersOnAggregation()
    {
      return false;
    }

    public bool FiltersOnFacts()
    {
      return true;
    }

    public override int GetHashCode()
    {
      var result = TypeOf.GetHashCode()
        ^ Operator.GetHashCode()
        ^ _value.GetHashCode();

      return result;
    }
  }
}