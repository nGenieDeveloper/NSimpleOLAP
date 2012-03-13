/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 19-02-2012
 * Time: 21:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace NSimpleOLAP.Storage.Interfaces
{
	/// <summary>
	/// Description of IStorage.
	/// </summary>
	public interface IStorage<T,U> : IDisposable
		where T: struct, IComparable
		where U: class, new()
	{
		IEnumerable<U> GetCells(KeyValuePair<T,T>[] pairs);
		void AddRowData(KeyValuePair<T,T>[] pairs, IVarData<T> data);
	}
}
