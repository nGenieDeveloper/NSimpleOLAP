using System;
using System.Collections.Generic;
using NSimpleOLAP.Interfaces;

namespace NSimpleOLAP
{
	/// <summary>
	/// Description of Cell.
	/// </summary>
	public class Cell<T> : ICell<T>
		where T: struct, IComparable
	{
		public Cell()
		{
			this.Values = new ValuesCollection<T>();
		}
		
		public T[] HashedKeys {
			get;
			internal set;
		}
		
		public KeyValuePair<T, T>[] Coords {
			get;
			internal set;
		}
		
		public uint Occurrences {
			get;
			set;
		}
		
		public IValueCollection<T> Values {
			get;
			private set;
		}
	}
}
