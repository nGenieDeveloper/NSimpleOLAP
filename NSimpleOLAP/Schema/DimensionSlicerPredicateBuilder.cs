using System;
using System.Collections.Generic;
using NSimpleOLAP;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Storage.Interfaces;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of DimensionSlicerPredicate.
	/// </summary>
	public class DimensionSlicerPredicateBuilder<T>
		where T: struct, IComparable
	{
		private DataSchema<T> _schema;
		
		public DimensionSlicerPredicateBuilder(DataSchema<T> schema)
		{
			_schema = schema;
		}
	}
}
