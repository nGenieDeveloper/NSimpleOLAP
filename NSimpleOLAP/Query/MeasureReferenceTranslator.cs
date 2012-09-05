using System;
using System.Collections.Generic;
using NSimpleOLAP;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Interfaces;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of MeasureReferenceTranslator.
	/// </summary>
	public class MeasureReferenceTranslator<T>
		where T: struct, IComparable
	{
		private DataSchema<T> _schema;
		
		public MeasureReferenceTranslator(DataSchema<T> schema)
		{
			_schema = schema;
		}
		
		public T Translate(string value)
		{
			var measure = _schema.Measures[value];
			
			if (measure != null)
				return measure.ID;
			else
				throw new Exception();
		}
	}
}
