using System;
using System.Collections.Generic;
using NSimpleOLAP.Query.Predicates;
using System.Linq;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of OrPredicateBuilder.
	/// </summary>
	public class OrPredicateBuilder<T> : IPredicateBuilder<T>
		where T: struct, IComparable
	{
		private IPredicateBuilder<T> _root;
		private List<IPredicateBuilder<T>> _predicates;
		
		public OrPredicateBuilder(IPredicateBuilder<T> root)
		{
			_root = root;
			_predicates = new List<IPredicateBuilder<T>>();
		}
		
		#region Fluent interface
		
		public OrPredicateBuilder<T> Add(IPredicateBuilder<T> builder)
		{
			_predicates.Add(builder);
			return this;
		}
		
		public IPredicateBuilder<T> Root
		{
			get { return _root; }
		}
		
		#endregion
		
		public IPredicate<T> Build()
		{
			var builders = from item in _predicates
				select item.Build();
			var predicate = new OrPredicate<T>();
			
			predicate.AddPredicate(builders.ToArray());
			
			return predicate;
		}
	}
}
