/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 21-02-2012
 * Time: 00:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
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
