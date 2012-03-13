/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 19-02-2012
 * Time: 22:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Storage.Molap.Graph;
using NSimpleOLAP.Storage.Interfaces;
using NSimpleOLAP.Configuration.Interfaces;

namespace NSimpleOLAP.Storage.Molap
{
	/// <summary>
	/// Description of MolapStorage.
	/// </summary>
	public class MolapStorage<T,U> : IStorage<T,U>
		where T: struct, IComparable
		where U: class, ICell<T>, new()
	{
		private Graph<T,U> _graph;
		
		public MolapStorage(T cubeid, IStoreConfig<T> config)
		{
			_graph = new Graph<T, U>(cubeid, config);
		}
		
		public IEnumerable<U> GetCells(KeyValuePair<T, T>[] pairs)
		{
			foreach (var item in _graph.GetNodes(pairs))
				yield return item.Container;
		}
		
		public void AddRowData(KeyValuePair<T, T>[] pairs, IVarData<T> data)
		{
			_graph.AddRowInfo(data, pairs);
		}
		
		public void Dispose()
		{
			_graph.Dispose();
		}
	}
}
