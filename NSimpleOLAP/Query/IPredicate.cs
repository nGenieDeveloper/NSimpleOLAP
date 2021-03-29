using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Data;
using NSimpleOLAP.Query;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of IPredicate.
	/// </summary>
	public interface IPredicate<T>
		where T: struct, IComparable
	{
		PredicateType TypeOf { get; }

		bool Execute(KeyValuePair<T, T>[] pairs, MeasureValuesCollection<T> data);

		bool FiltersOnFacts();

		bool FiltersOnAggregation();
	}
}
