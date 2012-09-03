using System;
using System.Collections.Generic;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of AxisBuilder.
	/// </summary>
	public class AxisBuilder<T>
	{
		private List<KeyValuePair<T,T>[]> _rowTuples;
		private List<KeyValuePair<T,T>[]> _columnTuples;
		private List<KeyValuePair<T,T>> _filterTuples;
		
		public AxisBuilder()
		{
			_rowTuples =  new List<KeyValuePair<T, T>[]>();
			_columnTuples = new List<KeyValuePair<T, T>[]>();
			_filterTuples = new List<KeyValuePair<T, T>>();
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
			_filterTuples.Add(tuple);
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
			return null;
		}
		
		#endregion
	}
}
