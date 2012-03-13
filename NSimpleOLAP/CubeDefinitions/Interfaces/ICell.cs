/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 20-02-2012
 * Time: 00:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace NSimpleOLAP.Interfaces
{
	/// <summary>
	/// Description of ICell.
	/// </summary>
	public interface ICell<T>
		where T: struct, IComparable
	{
		T[] HashedKeys { get; }
		
		KeyValuePair<T,T>[] Coords { get; }
		
        uint Occurrences { get; set; }

        IValueCollection<T> Values { get; }
	}
}
