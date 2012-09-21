using System;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of AndPredicateBuilder.
	/// </summary>
	public class AndPredicateBuilder<T> : IPredicateBuilder<T>
		where T: struct, IComparable
	{
		public AndPredicateBuilder()
		{
		}
		
		public IPredicate<T> Build()
		{
			throw new NotImplementedException();
		}
	}
}
