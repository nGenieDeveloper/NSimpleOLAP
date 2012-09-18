using System;
using NSimpleOLAP.Common;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Query.Predicates;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of MeasureSlicerBuilder.
	/// </summary>
	public class MeasureSlicerBuilder<T> : IPredicateBuilder<T>
		where T: struct, IComparable
	{
		private DataSchema<T> _schema;
		private T _measure;
		private object _value;
		private LogicalOperators _operator;
		private DataValueType _valueType;
		private MeasureReferenceTranslator<T> _translator;
		
		public MeasureSlicerBuilder(DataSchema<T> schema, MeasureReferenceTranslator<T> translator)
		{
			_schema = schema;
			_translator = translator;
		}
		
		#region fluent interface
		
		public MeasureSlicerBuilder<T> Set(string measure, DataValueType valueType,LogicalOperators loperator, object value)
		{
			_measure = _translator.Translate(measure);
			_valueType = valueType;
			_operator = loperator;
			_value = value;
			
			return this;
		}
		
		public MeasureSlicerBuilder<T> Set(T measureKey, DataValueType valueType, LogicalOperators loperator, object value)
		{
			_measure = measureKey;
			_valueType = valueType;
			_operator = loperator;
			_value = value;
			
			return this;
		}
		
		#endregion
		
		public IPredicate<T> Build()
		{
			return new SliceByMeasure<T>(_measure, _valueType, _operator, _value);
		}
	}
}
