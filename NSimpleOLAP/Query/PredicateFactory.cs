using System;
using System.Collections.Generic;
using NSimpleOLAP;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Storage.Interfaces;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of PredicateFactory.
	/// </summary>
	public class PredicateFactory<T>
		where T: struct, IComparable
	{
		private DataSchema<T> _schema;
		
		public PredicateFactory(DataSchema<T> schema)
		{
			_schema = schema;
		}
		
		
	}
}
