using System;
using System.Collections.Generic;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Storage.Interfaces;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Storage.Molap;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Data;


namespace NSimpleOLAP.Storage.Molap.Graph
{
	internal enum CoordsCase { Point = 0, PointAll = 1, ALL = 2}
	
	/// <summary>
	/// Description of Graph.
	/// </summary>
	internal class Graph<T, U> : IDisposable
		where T: struct, IComparable
		where U: class, ICell<T>
	{
		private MolapKeyHandler<T> _keyHandler;
		private  MolapCellValuesHelper<T, U> _cellValueHelper;

		
		public Graph(T root, StorageConfig config, MolapCellValuesHelper<T, U> cellValueHelper)
		{
			this.Root = new ImpNode(new KeyValuePair<T, T>[] { new KeyValuePair<T,T>(default(T), root)}) { IsRootDim = true };
			_cellValueHelper = cellValueHelper;
			_keyHandler = new MolapKeyHandler<T>(config.MolapConfig);
			this.Root.Key = _keyHandler.GetKey(this.Root.Coords);
		}
		
		#region public members
				
		public Node<T, U> Root
		{
			get;
			private set;
		}
		
		public void AddRowInfo(MeasureValuesCollection<T> vardata, KeyValuePair<T,T>[] coords)
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
		
		public Node<T,U> GetNode(KeyValuePair<T,T>[] coords)
		{
			Node<T,U> currnode = this.Root.GetNode(GetHashPoints(coords));
			
			return currnode;
		}
		
		public IEnumerable<Node<T,U>> NodesEnumerator()
		{
			foreach (Node<T,U> item in this.Root.NodesEnumerator())
				yield return item;
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
			
			if (coords.Length > 0 && !coords[0].Equals(this.Root.Coords[0]))
				pairs.Add(this.Root.Coords[0]);
			
			for (int i = 0; i < coords.Length; i++)
			{
				pairs.Add(coords[i]);
				hslist.Add(_keyHandler.GetKey(pairs.ToArray()));
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
		
		private void CreateNodes(KeyValuePair<T,T>[] coords, Node<T,U> rootnode,  Node<T,U> connode, int index, MeasureValuesCollection<T> vardata)
		{
			for (int i = index; i < coords.Length; i++)
			{
				KeyValuePair<T, T> pair = coords[i];
				Node<T,U> dimnode = CreateNDimNode(rootnode, new KeyValuePair<T, T>(pair.Key, default(T)), vardata);
				Node<T,U> cellnode = null;
				
				if (connode != null)
					cellnode = CreateNDimNode(connode, pair, vardata);
				else
				{
					cellnode = CreateNDimNode(dimnode, pair, vardata);
					dimnode.InsertNode(cellnode);
				}
				
				this.CreateNodes(coords, dimnode, cellnode, i+1, vardata);
			}
		}
		
		private Node<T,U> CreateNDimNode(Node<T,U> rootnode, KeyValuePair<T, T> pair, MeasureValuesCollection<T> vardata)
		{
			KeyValuePair<T,T>[] coords = Node<T,U>.GetCoords(rootnode.Coords, pair);
			T hashkey = _keyHandler.GetKey(coords);
			Node<T,U> rnode = rootnode.InsertChildNodeIfNotExists(hashkey, coords);
			_cellValueHelper.UpdateMeasures(rnode.Container, vardata);
			
			return rnode;
		}
		
		
		
		#endregion
		
		#region Node<T,U> implementation
		
		private class ImpNode : Node<T, U>
		{
			public ImpNode(KeyValuePair<T, T>[] coords)
			{
				this.Container = (U)(object)(new MolapCell<T>(coords));
				this.Coords = coords;
				this.Adjacent = new NodeCollection<T, U>();
			}
			
			protected override Node<T, U> Create(T childkey, KeyValuePair<T, T>[] coords)
			{
				ImpNode node = new ImpNode(coords) { Key = childkey, IsRootDim = SetRootDim(coords) };
				
				return node;
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
