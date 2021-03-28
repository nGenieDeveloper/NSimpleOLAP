using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;

namespace NSimpleOLAP.Query.Predicates
{
	/// <summary>
	/// Description of AndPredicate.
	/// </summary>
	internal class AndPredicate<T> : IPredicate<T>
		where T: struct, IComparable
	{
		private List<IPredicate<T>> _predicates;
		
		public AndPredicate()
		{
			_predicates = new List<IPredicate<T>>();
		}
		
		public void AddPredicate(params IPredicate<T>[] predicates)
		{
			_predicates.AddRange(predicates);
		}
		
		public IEnumerable<IPredicate<T>> Predicates
		{
			get { return _predicates; }
		}
		
		public PredicateType TypeOf 
		{
			get { return PredicateType.AND; }
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			var result = TypeOf.GetHashCode();

			foreach (var item in _predicates)
				result ^= item.GetHashCode();

			return result;
		}
	}
}
