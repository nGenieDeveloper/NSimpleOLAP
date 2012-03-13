/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 15-02-2012
 * Time: 23:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Storage.Interfaces;
using NSimpleOLAP.Configuration.Interfaces;

namespace NSimpleOLAP.Storage.Molap.Graph
{
	internal enum CoordsCase { Point = 0, PointAll = 1, ALL = 2}
	
	/// <summary>
	/// Description of Graph.
	/// </summary>
	internal class Graph<T, U> : IDisposable
		where T: struct, IComparable
		where U: class, ICell<T>, new()
	{
		private Func<KeyValuePair<T,T>[], T> _hashPairsFunction;
		private Action<object, IVarData<T>> _varDataMergeFunc;
		
		public Graph(T root, IStoreConfig<T> config)
		{
			this.Root = new ImpNode() { 
				Coords = new KeyValuePair<T, T>[] { new KeyValuePair<T,T>(root, default(T))},
				IsRootDim = true
			};
			_hashPairsFunction = config.HashingFunction;
			_varDataMergeFunc = config.VarMergeFunction;
			this.Root.Key = _hashPairsFunction(this.Root.Coords);
		}
		
		#region public members
				
		public Node<T, U> Root
		{
			get;
			private set;
		}
		
		public void AddRowInfo(IVarData<T> vardata, KeyValuePair<T,T>[] coords)
		{	
			CreateNodes(coords,this.Root, null, 0, vardata);
		}
		
		public IEnumerable<Node<T,U>> GetNodes(KeyValuePair<T,T>[] coords)
		{
			CoordsCase coordstype = CoordsType(coords);
			IEnumerable<Node<T,U>> nodes = null;
			
			switch (coordstype)
			{
				case CoordsCase.Point:
					nodes = GetSingleNode(coords);
					break;
				case CoordsCase.PointAll:
					nodes = GetPointAllNodes(coords);
					break;
				case CoordsCase.ALL: 
					nodes = GetAllNodes(coords);
					break;
			}
			
			foreach (var item in nodes)
				yield return item;
				// (1.1, 2.0, 3.1) : (1.0.2.0,3.0) -> (1.1,2.n,3.1)
				// (1.0,2.0,3.0)
		}
		
		#endregion
		
		#region private methods
		
		private IEnumerable<Node<T,U>> GetSingleNode(KeyValuePair<T,T>[] coords)
		{
			Node<T,U> currnode = this.Root.GetNode(GetHashPoints(coords));
			
			if (currnode == null)
				yield break;
			
			yield return currnode;
		}
		
		private IEnumerable<Node<T,U>> GetPointAllNodes(KeyValuePair<T,T>[] coords)
		{
			Node<T,U> currnode = this.Root;
			List<KeyValuePair<T,T>> pairs = new List<KeyValuePair<T, T>>();
			KeyValuePair<T,T>[] scoords = Array.FindAll(coords, x => !x.Value.Equals(default(T)));
			
			foreach (var coord in coords)
				pairs.Add(new KeyValuePair<T,T>(coord.Key, default(T)));
			
			currnode = currnode.GetNode(GetHashPoints(pairs.ToArray()));
			
			if (currnode == null)
				yield break;
			
			foreach (var item in currnode.Adjacent)
			{
				if (!item.IsRootDim && FilterNode(item, scoords))
					yield return item;
			}
		}
		
		private IEnumerable<Node<T,U>> GetAllNodes(KeyValuePair<T,T>[] coords)
		{
			Node<T,U> currnode = this.Root.GetNode(GetHashPoints(coords));
					
			if (currnode == null)
				yield break;
			
			foreach (var item in currnode.Adjacent)
			{
				if (!item.IsRootDim)
					yield return item;
			}
		}
		
		private bool FilterNode(Node<T,U> node, KeyValuePair<T,T>[] scoords)
		{
			bool ret = true;
			
			foreach (var item in scoords)
			{
				int val = Array.BinarySearch<KeyValuePair<T,T>>(node.Coords,item);
				
				if (val < 0)
				{
					ret = false;
					break;
				}
			}
			
			return ret;
		}
		
		private T[] GetHashPoints(KeyValuePair<T,T>[] coords)
		{
			List<KeyValuePair<T,T>> pairs = new List<KeyValuePair<T, T>>();
			List<T> hslist = new List<T>();
			
			for (int i = 0; i < coords.Length; i++)
			{
				pairs.Add(coords[i]);
				hslist.Add(_hashPairsFunction(pairs.ToArray()));
			}
			
			return hslist.ToArray();
		}
		
		private CoordsCase CoordsType(KeyValuePair<T,T>[] coords)
		{
			KeyValuePair<T,T>[] bcoords = Array.FindAll(coords, x => x.Value.Equals(default(T)));
			
			if (coords.Length == bcoords.Length)
				return CoordsCase.ALL;
			else if (bcoords.Length > 0)
				return CoordsCase.PointAll;
			else
				return CoordsCase.Point;
		}
		
		private void CreateNodes(KeyValuePair<T,T>[] coords, Node<T,U> rootnode,  Node<T,U> connode, int index, IVarData<T> vardata)
		{
			for (int i = index; i < coords.Length; i++)
			{
				KeyValuePair<T, T> pair = coords[i];
				Node<T,U> rnode = CreateNDimNode(rootnode, new KeyValuePair<T, T>(pair.Key, default(T)), vardata);
				Node<T,U> childnode = CreateNDimNode(rnode, pair, vardata);
				
				if (connode != null)
					connode.InsertNode(childnode);
				
				this.CreateNodes(coords, rnode, childnode, i+1, vardata);
			}
		}
		
		private Node<T,U> CreateNDimNode(Node<T,U> rootnode, KeyValuePair<T, T> pair, IVarData<T> vardata)
		{
			KeyValuePair<T,T>[] coords = Node<T,U>.GetCoords(rootnode.Coords, pair);
			T hashkey = _hashPairsFunction(coords);
			Node<T,U> rnode = rootnode.InsertChildNodeIfNotExists(hashkey, coords);
			_varDataMergeFunc(rnode, vardata);
			
			return rnode;
		}
		
		#endregion
		
		#region Node<T,U> implementation
		
		private class ImpNode : Node<T, U>
		{
			public ImpNode()
			{
				this.Container = new U();
				this.Adjacent = new NodeCollection<T, U>();
			}
			
			protected override Node<T, U> Create(T childkey, KeyValuePair<T, T>[] coords)
			{
				return new ImpNode() { Key = childkey, Coords = coords, IsRootDim = SetRootDim(coords) };
			}
			
			private bool SetRootDim(KeyValuePair<T, T>[] coords)
			{
				bool ret = false;
				
				if (coords[coords.Length-1].Value.Equals(default(T)))
					ret = true;
				
				return ret;
			}
		}
		
		#endregion
		
		#region IDisposable implementation
		
		public void Dispose()
		{
			Root.Dispose();
		}
		
		#endregion
	}
}
