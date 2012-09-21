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
		private List<IPredicateBuilder<T>> _predicates;
		
		public AndPredicate(params IPredicateBuilder<T>[] predicates)
		{
			_predicates = new List<IPredicateBuilder<T>>(predicates);
		}
		
		public IEnumerable<IPredicateBuilder<T>> Predicates
		{
			get { return _predicates; }
		}
	}
}
