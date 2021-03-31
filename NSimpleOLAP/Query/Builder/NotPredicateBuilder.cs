using System;
using NSimpleOLAP.Query.Predicates;
using NSimpleOLAP.Query.Interfaces;

namespace NSimpleOLAP.Query.Builder
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

		public AndPredicateBuilder<T> And(params Func<WhereBuilder<T>, IPredicateBuilder<T>>[] andPreds)
		{

			return null;
		}


		public OrPredicateBuilder<T> Or(params Func<WhereBuilder<T>, IPredicateBuilder<T>>[] orPreds)
		{

			return null;
		}

		public MeasureSlicerBuilder<T> Dimension(string dimension)
		{

			return null;
		}

		public DimensionSlicerBuilder<T> Measure(string measure)
		{

			return null;
		}

		public BlockPredicateBuilder<T> Block(Func<WhereBuilder<T>, IPredicateBuilder<T>> blockPred)
		{
			return null;
		}

		#endregion

		public IPredicate<T> Build()
		{
			var predicate = new NotPredicate<T>(_predicate.Build());
			
			return predicate;
		}
	}
}
