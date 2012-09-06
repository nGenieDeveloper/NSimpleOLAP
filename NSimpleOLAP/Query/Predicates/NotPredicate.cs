using System;

namespace NSimpleOLAP.Query.Predicates
{
	/// <summary>
	/// Description of NotPredicate.
	/// </summary>
	public class NotPredicate<T> : IPredicate<T>
		where T: struct, IComparable		
	{
		private IPredicate<T> _predicate;
		
		public NotPredicate(IPredicate<T> predicate)
		{
			_predicate = predicate;
		}
		
		public IPredicate<T> Predicate
		{
			get { return _predicate; }
		}
	}
}
