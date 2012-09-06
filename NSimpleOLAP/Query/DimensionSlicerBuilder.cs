using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Schema;

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
		
		public DimensionSlicerBuilder(DataSchema<T> schema)
		{
			_schema = schema;
			_members = new List<T>();
		}
		
		#region fluent interface
		
		public DimensionSlicerBuilder<T> Set(string dimension, LogicalOperators loperator, params string[] member)
		{
			return this;
		}
		
		public DimensionSlicerBuilder<T> Set(T dimensionKey, LogicalOperators loperator, params T[] memberKeys)
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
