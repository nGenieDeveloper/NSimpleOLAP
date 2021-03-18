using System;
using System.Linq;
using System.Collections.Generic;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Storage.Molap.Graph;
using NSimpleOLAP.Storage.Interfaces;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Common;
using NSimpleOLAP.Data;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Schema.Interfaces;

namespace NSimpleOLAP.Storage.Molap
{
	/// <summary>
	/// Description of MolapStorage.
	/// </summary>
	public class MolapStorage<T,U> : IStorage<T,U>
		where T: struct, IComparable
		where U: class, ICell<T>
	{
		private Graph<T,U> _graph;
		private T _cubeid;
		private CanonicFormater<T> _canonicFormater;
		
		public MolapStorage(T cubeid, StorageConfig config)
		{
			_cubeid = cubeid;
			this.Config = config;
			_canonicFormater = new CanonicFormater<T>();
			this.Init();
		}
		
		#region private methods
		
		private void Init()
		{
			this.NameSpace = new ImpNameSpace(AbsIdentityKey<T>.Create());
			this.Dimensions = new MembersCollection<Dimension<T>>(ItemType.Dimension, 
			                                                      (dimension)=> { 
			                                                      	this.NameSpace.Add(dimension);
			                                                      	dimension.SetMembersStorage(new DimensionMembersCollection());
			                                                      },
			                                                      (storage)=> { 
			                                                      		this.NameSpace.Clear(ItemType.Dimension);
			                                                      		
			                                                      		foreach (Dimension<T> item in storage)
			                                                      			item.Dispose();
			                                                      });
			this.Measures = new MembersCollection<Measure<T>>(ItemType.Measure, 
			                                                  (measure)=>{ this.NameSpace.Add(measure); }, 
			                                                  (storage)=> this.NameSpace.Clear(ItemType.Measure));
			this.Metrics = new MembersCollection<Metric<T>>(ItemType.Metric, 
			                                                (metric)=>{ this.NameSpace.Add(metric); }, 
			                                                (storage)=> this.NameSpace.Clear(ItemType.Metric));
			_graph = new Graph<T, U>(_cubeid, this.Config, new CellValuesHelper(this.Measures));
		}
		
		#endregion
		
		#region IStorage<T,U> implementation
		
		public IEnumerable<U> GetCells(KeyValuePair<T, T>[] pairs)
		{
			KeyValuePair<T,T>[] cpairs = _canonicFormater.Format(pairs);
			
			foreach (var item in _graph.GetNodes(cpairs))
				yield return item.Container;
		}
		
		public IEnumerable<U> CellEnumerator()
		{
			foreach (Node<T,U> item in _graph.NodesEnumerator())
				yield return item.Container;
		}
		
		public U GetCell(KeyValuePair<T,T>[] pairs)
		{
			KeyValuePair<T,T>[] cpairs = _canonicFormater.Format(pairs);
			Node<T,U> node = _graph.GetNode(cpairs);
			
			if (node != null)
				return node.Container;
			else
				return null;
		}
		
		public void AddRowData(KeyValuePair<T, T>[] pairs, MeasureValuesCollection<T> data)
		{
			_graph.AddRowInfo(data, pairs);
		}
		
		public int GetCellCount()
		{
			int count = 0;
			IEnumerable<int> cellscounts = from item in _graph.NodesEnumerator()
											select item.GetNodeCount();
			count = cellscounts.Sum();
			
			return count;
		}
		
		public void Dispose()
		{
			_graph.Dispose();

			NameSpace.Dispose();
		}
		
		public StorageType StorageType { get { return StorageType.Molap; } }
		
		public INamespace<T> NameSpace { 
			get;
			private set;			
		}
		
		public IMemberStorage<T, Dimension<T>> Dimensions {
			get;
			private set;
		}
		
		public IMemberStorage<T, Measure<T>> Measures {
			get;
			private set;
		}
		
		public IMemberStorage<T, Metric<T>> Metrics {
			get;
			private set;
		}
		
		public StorageConfig Config { 
			get;
			private set;
		}
		
		#endregion
		
		#region private classes
		
		private class MembersCollection<TMember> : AbsMolapMemberCollection<T,TMember>
			where TMember: IDataItem<T>
		{
			public MembersCollection(ItemType type, Action<TMember> onaddmember, Action<IMemberStorage<T,TMember>> onclear)
			{
				_type = type;
				this.memberOnAdd = onaddmember;
				this.onClear = onclear;
				this.Init();
			}
		}
		
		private class DimensionMembersCollection : AbsMolapMemberCollection<T, Member<T>>
		{
			private AbsIdentityKey<T> _keybuilder;
				
			public DimensionMembersCollection()
			{
				_keybuilder = AbsIdentityKey<T>.Create();
				_type = ItemType.Member;
				this.memberOnAdd = (item) => {
												if (item.ID.Equals(default(T)))
													item.ID = _keybuilder.GetNextKey();
											};
				this.Init();
			}
		}
		
		private class ImpNameSpace: AbsMolapNameSpace<T>
		{
			public ImpNameSpace(AbsIdentityKey<T> keybuilder)
			{
				this._keybuilder = keybuilder;
				this.Init();
			}
		}
		
		private class CellValuesHelper : MolapCellValuesHelper<T, U>
		{
			private IMemberStorage<T, Measure<T>> _measures;
			
			public CellValuesHelper(IMemberStorage<T, Measure<T>> measures)
			{
				_measures = measures;
			}
			
			public override void UpdateMeasures(U cell, MeasureValuesCollection<T> measures)
			{
				MolapCell<T> mcell = (MolapCell<T>)(object)cell;
				
				mcell.IncrementOcurrences();
				
				foreach (KeyValuePair<T, object> item in measures)
				{
					if (item.Value != null)
					{
						ValueType nvalue = (ValueType)item.Value;
						
						if (mcell.Values.ContainsKey(item.Key))
						{
							Type measuretype = this._measures[item.Key].DataType;
							ValueType ovalue = mcell.Values[item.Key];
							Func<ValueType, ValueType, ValueType> functor = null;
						
							if (measuretype == typeof(int))
								functor = (x,y) => (int)x + (int)y;
							else if (measuretype == typeof(long))
								functor = (x,y) => (long)x + (long)y;
							else if (measuretype == typeof(uint))
								functor = (x,y) => (uint)x + (uint)y;
							else if (measuretype == typeof(ulong))
								functor = (x,y) => (ulong)x + (ulong)y;
							else if (measuretype == typeof(decimal))
								functor = (x,y) => (decimal)x + (decimal)y;
							else if (measuretype == typeof(float))
								functor = (x,y) => (float)x + (float)y;
							else if (measuretype == typeof(double))
								functor = (x,y) => (double)x + (double)y;
							
							if (functor != null)
								mcell.Values[item.Key] = this.Add(ovalue, nvalue, functor);
						}
						else
							mcell.Values.Add(item.Key, nvalue);
					}
				}
			}
			
			public override void ClearCell(U cell)
			{
				((MolapCell<T>)(object)cell).Reset();
			}
		}
		
		#endregion
	}
}
