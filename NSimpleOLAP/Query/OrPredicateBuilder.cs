using System;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of OrPredicateBuilder.
	/// </summary>
	public class OrPredicateBuilder<T> : IPredicateBuilder<T>
		where T: struct, IComparable
	{
		public OrPredicateBuilder()
		{
		}
		
		public IPredicate<T> Build()
		{
			throw new NotImplementedException();
		}
	}
}
