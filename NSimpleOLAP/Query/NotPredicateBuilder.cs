using System;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of NotPredicateBuilder.
	/// </summary>
	public class NotPredicateBuilder<T> : IPredicateBuilder<T>
		where T: struct, IComparable
	{
		public NotPredicateBuilder()
		{
		}
		
		public IPredicate<T> Build()
		{
			throw new NotImplementedException();
		}
	}
}
