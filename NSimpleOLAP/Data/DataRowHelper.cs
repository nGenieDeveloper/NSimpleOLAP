using System;
using System.Collections.Generic;
using System.Linq;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Data.Readers;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Storage;
using NSimpleOLAP.Storage.Interfaces;

namespace NSimpleOLAP.Data
{
	/// <summary>
	/// Description of DataRowHelper.
	/// </summary>
	internal class DataRowHelper<T>
		where T: struct, IComparable
	{
		private DataSchema<T> _schema;
		private CubeSourceConfig _config;
		
		public DataRowHelper(DataSchema<T> schema, CubeSourceConfig config)
		{
			_schema = schema;
			_config = config;
		}
		
		public KeyValuePair<T,T>[] GetDimensions(AbsRowData rowdata)
		{
			List<KeyValuePair<T,T>> retlist = new List<KeyValuePair<T, T>>();
			
			foreach (SourceMappingsElement item in _config.Fields)
			{
				if (rowdata[item.Field] != null)
				{
					T segment = (T)Convert.ChangeType(rowdata[item.Field], typeof(T));
					Dimension<T> dimension = _schema.Dimensions[item.Dimension];
					
					if (dimension.Members.ContainsKey(segment))
					{
						KeyValuePair<T,T> pair = new KeyValuePair<T, T>(dimension.ID, segment);
						retlist.Add(pair);
					}
				}
			}
			
			retlist.Sort(ComparePairs);
			
			return retlist.ToArray();
		}
		
		public IVarData<T> GetMeasureData(AbsRowData rowdata)
		{
			return new VarsCollection<T>();
		}
		
		private int ComparePairs(KeyValuePair<T, T> a, KeyValuePair<T, T> b)
	    {
			return a.Value.CompareTo(b.Value);
	    }
	}
}
