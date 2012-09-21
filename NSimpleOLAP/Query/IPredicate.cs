using System;
using NSimpleOLAP.Common;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of IPredicate.
	/// </summary>
	public interface IPredicate<T>
		where T: struct, IComparable
	{
		PredicateType TypeOf { get; }
	}
}
