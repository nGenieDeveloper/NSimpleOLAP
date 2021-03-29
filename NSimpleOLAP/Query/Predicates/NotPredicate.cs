﻿using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Data;


namespace NSimpleOLAP.Query.Predicates
{
	/// <summary>
	/// Description of NotPredicate.
	/// </summary>
	internal class NotPredicate<T> : IPredicate<T>
		where T: struct, IComparable		
	{
		private IPredicate<T> _predicate;
		
		public NotPredicate(IPredicate<T> predicate)
		{
			_predicate = predicate;
		}
		
		public IPredicate<T> Predicate
		{
			get { return _predicate; }
		}
		
		public PredicateType TypeOf 
		{
			get { return PredicateType.NOT; }
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			var result = TypeOf.GetHashCode()
				^ _predicate.GetHashCode();

			return result;
		}

		public bool Execute(KeyValuePair<T, T>[] pairs, MeasureValuesCollection<T> data)
		{
			throw new NotImplementedException();
		}

		public bool FiltersOnFacts()
		{
			return _predicate.FiltersOnFacts();
		}

		public bool FiltersOnAggregation()
		{
			return _predicate.FiltersOnAggregation();
		}
	}
}
