using System;
using NSimpleOLAP.Common;

namespace NSimpleOLAP.Query.Predicates
{
	/// <summary>
	/// Description of SliceByMeasure.
	/// </summary>
	public class SliceByMeasure<T> : IPredicate<T>
		where T: struct, IComparable
	{
		private T _measure;
		private LogicalOperators _operator;
		private object _value;
		private DataValueType _dataValueType;
		
		public SliceByMeasure()
		{
		}
	}
}
