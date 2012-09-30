using System;
using NSimpleOLAP.Query.Predicates;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of NotPredicateBuilder.
	/// </summary>
	public class NotPredicateBuilder<T> : IPredicateBuilder<T>
		where T: struct, IComparable
	{
		private IPredicateBuilder<T> _root;
		private IPredicateBuilder<T> _predicate;
		
		public NotPredicateBuilder(IPredicateBuilder<T> root)
		{
			_root = root;
		}
		
		#region Fluent interface
		
		public NotPredicateBuilder<T> Add(IPredicateBuilder<T> builder)
		{
			_predicate = builder;
			return this;
		}
		
		public IPredicateBuilder<T> Root
		{
			get { return _root; }
		}
		
		#endregion
		
		public IPredicate<T> Build()
		{
			var predicate = new NotPredicate<T>(_predicate.Build());
			
			return predicate;
		}
	}
}
