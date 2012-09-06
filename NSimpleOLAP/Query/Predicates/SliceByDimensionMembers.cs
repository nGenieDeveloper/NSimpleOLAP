using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;

namespace NSimpleOLAP.Query.Predicates
{
	/// <summary>
	/// Description of SliceByDimensionMember.
	/// </summary>
	public class SliceByDimensionMembers<T> : IPredicate<T>
		where T: struct, IComparable
	{
		private T _dimension;
		private LogicalOperators _operator;
		private List<T> _values;
		
		public SliceByDimensionMembers(T dimensionKey, LogicalOperators loperator, params T[] values)
		{
			_values = new List<T>();
			_dimension = dimensionKey;
			_operator = loperator;
			_values.AddRange(values);
		}
		
		public T Dimension
		{
			get { return _dimension; }
		}
		
		public LogicalOperators Operator
		{
			get { return _operator; }
		}
		
		public IEnumerable<T> Values
		{
			get { return _values; }
		}
	}
}
