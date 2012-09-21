using System;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of BlockPredicateBuilder.
	/// </summary>
	public class BlockPredicateBuilder<T> : IPredicateBuilder<T>
		where T: struct, IComparable
	{
		public BlockPredicateBuilder()
		{
		}
		
		public IPredicate<T> Build()
		{
			throw new NotImplementedException();
		}
	}
}
