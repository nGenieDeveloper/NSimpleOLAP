using System;
using NSimpleOLAP.Common;
using NSimpleOLAP.Schema;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of MeasureSlicerBuilder.
	/// </summary>
	public class MeasureSlicerBuilder<T> : IPredicateBuilder<T>
		where T: struct, IComparable
	{
		private DataSchema<T> _schema;
		private T _dimension;
		private object _value;
		private LogicalOperators _operator;
		private DataValueType _valueType;
		
		public MeasureSlicerBuilder(DataSchema<T> schema)
		{
			_schema = schema;
		}
		
		#region fluent interface
		
		public MeasureSlicerBuilder<T> Set(string measure, DataValueType valueType,LogicalOperators loperator, object value)
		{
			return this;
		}
		
		public MeasureSlicerBuilder<T> Set(T measureKey, DataValueType valueType, LogicalOperators loperator, object value)
		{
			return this;
		}
		
		#endregion
		
		public IPredicate<T> Build()
		{
			throw new NotImplementedException();
		}
	}
}
