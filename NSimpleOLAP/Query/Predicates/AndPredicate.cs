using System;
using System.Collections.Generic;

namespace NSimpleOLAP.Query.Predicates
{
	/// <summary>
	/// Description of AndPredicate.
	/// </summary>
	public class AndPredicate<T> : IPredicate<T>
		where T: struct, IComparable
	{
		private List<IPredicate<T>> _predicates;
		
		public AndPredicate(params IPredicate<T>[] predicates)
		{
			_predicates = new List<IPredicate<T>>(predicates);
		}
		
		public IEnumerable<IPredicate<T>> Predicates
		{
			get { return _predicates; }
		}
	}
}
