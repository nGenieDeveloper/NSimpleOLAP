using System;
using System.Collections.Generic;
using NSimpleOLAP.Query.Predicates;
using System.Linq;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of BlockPredicateBuilder.
	/// </summary>
	public class BlockPredicateBuilder<T> : IPredicateBuilder<T>
		where T: struct, IComparable
	{
		private BlockPredicateBuilder<T> _root;
		private List<IPredicateBuilder<T>> _predicates;
		
		internal BlockPredicateBuilder()
		{
			_predicates = new List<IPredicateBuilder<T>>();
		}
		
		public BlockPredicateBuilder(BlockPredicateBuilder<T> root) : this()
		{
			_root = root;
		}
		
		#region Fluent interface
		
		public BlockPredicateBuilder<T> Add(IPredicateBuilder<T> builder)
		{
			_predicates.Add(builder);
			return this;
		}
		
		public BlockPredicateBuilder<T> CloseBlock()
		{
			return _root;
		}
		
		#endregion
		
		public IPredicate<T> Build()
		{
			var builders = from item in _predicates
				select item.Build();
			var predicate = new BlockPredicate<T>();
			
			predicate.AddPredicate(builders.ToArray());
			
			return predicate;
		}
	}
}
