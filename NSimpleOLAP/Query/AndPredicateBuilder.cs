using System;
using System.Collections.Generic;
using NSimpleOLAP.Query.Predicates;
using System.Linq;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of AndPredicateBuilder.
	/// </summary>
	public class AndPredicateBuilder<T> : IPredicateBuilder<T>
		where T: struct, IComparable
	{
		private IPredicateBuilder<T> _root;
		private List<IPredicateBuilder<T>> _predicates;
		
		public AndPredicateBuilder(IPredicateBuilder<T> root)
		{
			_root = root;
			_predicates = new List<IPredicateBuilder<T>>();
		}
		
		public AndPredicateBuilder<T> Add(IPredicateBuilder<T> builder)
		{
			_predicates.Add(builder);
			return this;
		}
		
		public IPredicateBuilder<T> Root
		{
			get { return _root; }
		}
		
		public IPredicate<T> Build()
		{
			var builders = from item in _predicates
				select item.Build();
			var predicate = new AndPredicate<T>();
			
			predicate.AddPredicate(builders.ToArray());
			
			return predicate;
		}
	}
}
