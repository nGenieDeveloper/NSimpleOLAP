using System;
using System.Collections.Generic;
using System.Linq;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of AxisBuilder.
	/// </summary>
	public class AxisBuilder<T>
	{
		private List<KeyValuePair<T,T>[]> _rowTuples;
		private List<KeyValuePair<T,T>[]> _columnTuples;
		private List<T> _filterDimensions;
		
		public AxisBuilder()
		{
			_rowTuples =  new List<KeyValuePair<T, T>[]>();
			_columnTuples = new List<KeyValuePair<T, T>[]>();
			_filterDimensions = new List<T>();
		}
		
		public void AddRowTuples(KeyValuePair<T, T>[] tuples)
		{
			_rowTuples.Add(tuples);
		}
		
		public void AddColumnTuples(KeyValuePair<T, T>[] tuples)
		{
			_columnTuples.Add(tuples);
		}
		
		public void AddFilterTuples(KeyValuePair<T, T> tuple)
		{
			if (!_filterDimensions.Contains(tuple.Key))
				_filterDimensions.Add(tuple.Key);
		}
		
		public IEnumerable<KeyValuePair<T,T>[]> Rows()
		{
			foreach(var item in _rowTuples)
				yield return AppendFilterDimensionality(item);
		}
		
		public IEnumerable<KeyValuePair<T,T>[]> Columns()
		{
			foreach(var item in _columnTuples)
				yield return AppendFilterDimensionality(item);
		}
		
		#region private members
		
		private KeyValuePair<T,T>[] AppendFilterDimensionality(KeyValuePair<T,T>[] tuples)
		{
			var list = (from pair in tuples
			            select pair.Key).ToList();
			var query = from item in _filterDimensions
						where !list.Contains(item)
						select new KeyValuePair<T,T>(item, default(T));
					
			return tuples.Concat(query).ToArray();
		}
		
		#endregion
	}
}
