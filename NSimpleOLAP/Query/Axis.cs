using System;
using System.Collections.Generic;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of Axis.
	/// </summary>
	public class Axis<T>
		where T: struct, IComparable
	{
		private List<KeyValuePair<T,T>[]> _rowsAxis;
		private List<KeyValuePair<T,T>[]> _columnsAxis;
		private List<int> _rowHashes;
		private List<int> _columnHashes;
		
		public Axis()
		{
			_rowsAxis = new List<KeyValuePair<T, T>[]>();
			_columnsAxis = new List<KeyValuePair<T, T>[]>();
			_rowHashes = new List<int>();
			_columnHashes = new List<int>();
		}
		
		#region props
		
		public IEnumerable<KeyValuePair<T,T>[]> RowAxis
		{
			get;
			private set;
		}
		
		public IEnumerable<KeyValuePair<T,T>[]> ColumnAxis
		{
			get;
			private set;
		}
		
		public IEnumerable<KeyValuePair<T,T>[]> UnionAxis
		{
			get;
			private set;
		}
		
		#endregion
		
		#region public methods
		
		public void AddRowTuples(params KeyValuePair<T,T>[] tuples)
		{
		
		}
		
		public void AddColumnTuples(params KeyValuePair<T,T>[] tuples)
		{
		
		}
		
		#endregion
		
		#region private methods
		
		private bool IsDuplicatedRowTuple(KeyValuePair<T,T>[] tuples)
		{
			return false;
		}
		
		private bool IsDuplicatedColumnTuple(KeyValuePair<T,T>[] tuples)
		{
			return false;
		}
		
		private bool DulDimen(KeyValuePair<T,T> dim)
		{
			return false;
		}
		
		#endregion
	}
}
