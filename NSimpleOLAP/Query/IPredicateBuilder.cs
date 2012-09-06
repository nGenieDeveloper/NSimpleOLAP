using System;
using System.Collections.Generic;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of IPredicateBuilder.
	/// </summary>
	public interface IPredicateBuilder<T>
		where T: struct, IComparable
	{
		IPredicate<T> Build();
	}
}
