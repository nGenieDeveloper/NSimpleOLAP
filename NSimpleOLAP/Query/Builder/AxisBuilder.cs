using System;
using System.Collections.Generic;
using System.Linq;
using NSimpleOLAP.Common;
using NSimpleOLAP.Query.Interfaces;

namespace NSimpleOLAP.Query.Builder
{
	/// <summary>
	/// Description of AxisBuilder.
	/// </summary>
	public class AxisBuilder<T>
		where T : struct, IComparable
	{
		private Axis<T> _axis;
		private List<T> _filterDimensions;
		
		public AxisBuilder(MolapHashTypes hashingtype)
		{
			_axis = new Axis<T>(hashingtype);
			_filterDimensions = new List<T>();
		}
		
		public void AddRowTuples(KeyValuePair<T, T>[] tuples)
		{
			_axis.AddRowTuples(tuples);
		}
		
		public void AddColumnTuples(KeyValuePair<T, T>[] tuples)
		{
			_axis.AddColumnTuples(tuples);
		}
		
		public void AddFilterTuples(KeyValuePair<T, T> tuple)
		{
			if (!_filterDimensions.Contains(tuple.Key))
				_filterDimensions.Add(tuple.Key);
		}
		
		public IEnumerable<KeyValuePair<T,T>[]> Rows()
		{
			foreach(var item in _axis.RowAxis)
				yield return AppendFilterDimensionality(item);
		}
		
		public IEnumerable<KeyValuePair<T,T>[]> Columns()
		{
			foreach(var item in _axis.ColumnAxis)
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
