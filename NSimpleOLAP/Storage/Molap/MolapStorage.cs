using System;
using System.Collections.Generic;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Storage.Molap.Graph;
using NSimpleOLAP.Storage.Interfaces;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Common;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Schema.Interfaces;

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
		
		public MolapStorage(T cubeid, StorageElement config)
		{
			this.Init();
			_graph = new Graph<T, U>(cubeid, config);
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
		}
		
		#endregion
		
		#region IStorage<T,U> implementation
		
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
			Metrics.Dispose();
			Measures.Dispose();
			Dimensions.Dispose();
			NameSpace.Dispose();
		}
		
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
		
		#endregion
	}
}
