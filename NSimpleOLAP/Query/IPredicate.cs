using System;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of IPredicate.
	/// </summary>
	public interface IPredicate<T>
		where T: struct, IComparable
	{
		
	}
}
