using System;
using NSimpleOLAP.Common;

namespace NSimpleOLAP.Query.Predicates
{
	/// <summary>
	/// Description of SliceByMeasure.
	/// </summary>
	internal class SliceByMeasure<T> : IPredicate<T>
		where T: struct, IComparable
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

		public override int GetHashCode()
		{
			var result = TypeOf.GetHashCode()
				^ Operator.GetHashCode()
				^ _value.GetHashCode();

			return result;
		}
	}
}
