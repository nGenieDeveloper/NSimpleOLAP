using System;
using System.Collections.Generic;
using NSimpleOLAP.Query.Predicates;
using System.Linq;
using NSimpleOLAP.Query.Interfaces;


namespace NSimpleOLAP.Query.Builder
{
	/// <summary>
	/// Description of BlockPredicateBuilder.
	/// </summary>
	public class BlockPredicateBuilder<T> : IPredicateBuilder<T>
		where T: struct, IComparable
	{
		private BlockPredicateBuilder<T> _root;
		private IPredicateBuilder<T> _innerPredicate;
		
		internal BlockPredicateBuilder()
		{
		}
		
		public BlockPredicateBuilder(BlockPredicateBuilder<T> root) : this()
		{
			_root = root;
		}
		
		#region Fluent interface
		
		public BlockPredicateBuilder<T> Set(IPredicateBuilder<T> builder)
		{
			_innerPredicate = builder;
			return this;
		}
		
		public BlockPredicateBuilder<T> CloseBlock()
		{
			return _root;
		}

		public AndPredicateBuilder<T> And(params Func<BlockPredicateBuilder<T>, IPredicateBuilder<T>>[] andPreds)
		{

			return null;
		}


		public OrPredicateBuilder<T> Or(params Func<BlockPredicateBuilder<T>, IPredicateBuilder<T>>[] orPreds)
		{

			return null;
		}

		public NotPredicateBuilder<T> Not(Func<WhereBuilder<T>, IPredicateBuilder<T>> notPred)
		{

			return null;
		}

		#endregion

		public IPredicate<T> Build()
		{
			var predicate =  new BlockPredicate<T>(_innerPredicate.Build());

			return predicate;
		}
	}
}
