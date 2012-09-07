using System;
using System.Collections.Generic;
using NSimpleOLAP;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Interfaces;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of ReferenceTranslator.
	/// </summary>
	public class DimensionReferenceTranslator<T>
		where T: struct, IComparable
	{
		private DataSchema<T> _schema;
		
		public DimensionReferenceTranslator(DataSchema<T> schema)
		{
			_schema = schema;
		}
		
		public KeyValuePair<T,T>[] Translate(string value)
		{
			var values =  this.GetStreamValues(value);
			int index = 0;
			List<KeyValuePair<T,T>> tuples = new List<KeyValuePair<T, T>>();
			
			while (index < values.Length)
			{
				T key = default(T);
				T member = default(T);
				
				if (TryGetDimension(values[index], out key))
				{
					index++;
					
					if (TryGetDimensionMember(key,values[index], out member))
						index++;
					
					tuples.Add(new KeyValuePair<T,T>(key, member));
				}
				else
					throw new Exception();
			}
			
			return tuples.ToArray();
		}
		
		public T GetDimension(string value)
		{
			T key = default(T);
			
			if (TryGetDimension(value, out key))
				return key;
			else
				throw new Exception();
		}
		
		public T GetDimensionMember(T dimKey, string value)
		{
			T key = default(T);
			
			if (TryGetDimensionMember(dimKey, value, out key))
				return key;
			else
				throw new Exception();
		}
		
		#region private members
		
		private string[] GetStreamValues(string value)
		{
			return value.Split('.');
		}
		
		private bool TryGetDimension(string value, out T key)
		{
			bool ret = false;
			var dimension = _schema.Dimensions[value];
			
			if (dimension != null)
			{
				key = dimension.ID;
				ret = true;
			}
			else
				key = default(T);
			
			return ret;
		}
		
		private bool TryGetDimensionMember(T dimKey, string value, out T key)
		{
			bool ret = false;
			var member = _schema.Dimensions[dimKey].Members[value];
			
			if (member != null)
			{
				key = member.ID;
				ret = true;
			}
			else
				key = default(T);
			
			return ret;
		}
		
		#endregion
			
	}
}
