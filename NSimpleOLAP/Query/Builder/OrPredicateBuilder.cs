using System;
using System.Collections.Generic;
using NSimpleOLAP.Query.Predicates;
using System.Linq;
using NSimpleOLAP.Query.Interfaces;

namespace NSimpleOLAP.Query.Builder
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

		public NotPredicateBuilder<T> Not(Func<WhereBuilder<T>, IPredicateBuilder<T>> notPred)
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
			var builders = from item in _predicates
				select item.Build();
			var predicate = new OrPredicate<T>();
			
			predicate.AddPredicate(builders.ToArray());
			
			return predicate;
		}
	}
}
