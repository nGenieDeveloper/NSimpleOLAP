using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Query.Predicates;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of DimensionSlicerBuilder.
	/// </summary>
	public class DimensionSlicerBuilder<T> : IPredicateBuilder<T>
		where T: struct, IComparable
	{
		private DataSchema<T> _schema;
		private T _dimension;
		private List<T> _members;
		private LogicalOperators _operator;
		private DimensionReferenceTranslator<T> _translator;
		
		public DimensionSlicerBuilder(DataSchema<T> schema, DimensionReferenceTranslator<T> translator)
		{
			_schema = schema;
			_members = new List<T>();
			_translator = translator;
		}
		
		#region fluent interface
		
		public DimensionSlicerBuilder<T> Set(string dimension, LogicalOperators loperator, params string[] members)
		{
			_dimension = _translator.GetDimension(dimension);
			_operator = loperator;
			
			foreach (var item in members)
				_members.Add(_translator.GetDimensionMember(_dimension, item));
			
			return this;
		}
		
		public DimensionSlicerBuilder<T> Set(T dimensionKey, LogicalOperators loperator, params T[] memberKeys)
		{
			_dimension = dimensionKey;
			_operator = loperator;
			_members.AddRange(memberKeys);
			
			return this;
		}
		
		#endregion
		
		public IPredicate<T> Build()
		{
			return new SliceByDimensionMembers<T>(_dimension, _operator, _members.ToArray());
		}
	}
}
