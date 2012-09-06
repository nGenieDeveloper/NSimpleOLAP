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
		public MeasureSlicerBuilder()
		{
		}
		
		#region fluent interface
		
		public MeasureSlicerBuilder<T> Set(string measure, LogicalOperators loperator, object value)
		{
			return this;
		}
		
		public MeasureSlicerBuilder<T> Set(T measureKey, LogicalOperators loperator, object value)
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
