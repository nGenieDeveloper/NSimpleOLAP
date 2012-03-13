/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 20-02-2012
 * Time: 00:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace NSimpleOLAP.Interfaces
{
	/// <summary>
	/// Description of ICellCollection.
	/// </summary>
	public interface ICellCollection<T, U>: ICollection<U>
		where T: struct, IComparable
		where U: class, ICell<T>, new()
	{
		U this[T[] keys] { get; }
		bool ContainsKey(T[] keys);
	}
}
