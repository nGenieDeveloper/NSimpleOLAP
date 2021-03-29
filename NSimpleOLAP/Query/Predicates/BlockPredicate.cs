using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Data;

namespace NSimpleOLAP.Query.Predicates
{
	/// <summary>
	/// Description of BlockPredicate.
	/// </summary>
	internal class BlockPredicate<T>: IPredicate<T>
		where T: struct, IComparable
	{
		private List<IPredicate<T>> _predicates;
		
		public BlockPredicate()
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
			get { return PredicateType.BLOCK; }
		}

		public bool Execute(KeyValuePair<T, T>[] pairs, MeasureValuesCollection<T> data)
		{
			throw new NotImplementedException();
		}

		public bool FiltersOnFacts()
		{
			foreach (var item in _predicates)
			{
				if (item.FiltersOnFacts())
					return true;
			}

			return false;
		}

		public bool FiltersOnAggregation()
		{
			foreach (var item in _predicates)
			{
				if (item.FiltersOnAggregation())
					return true;
			}

			return false;
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
