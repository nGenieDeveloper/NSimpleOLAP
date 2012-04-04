using System;
using System.Collections.Generic;
using System.Linq;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Storage.Interfaces;
using NSimpleOLAP.Common;
using NSimpleOLAP.Common.Collections;	

namespace NSimpleOLAP.Storage.Molap
{
	/// <summary>
	/// Description of AbsMolapMemberCollection.
	/// </summary>
	public abstract class AbsMolapMemberCollection<T, TMember> : IMemberStorage<T,TMember>
		where T: struct, IComparable
		where TMember: IDataItem<T>
	{
		private TSDictionary<T,TMember> _innerDictionary;
		private TSDictionary<string, T> _mapName;
		protected ItemType _type;
		protected Action<TMember> memberOnAdd;
		protected Action onClear;
		
		protected virtual void Init()
		{
			_innerDictionary = new TSDictionary<T, TMember>();
			_mapName = new TSDictionary<string, T>();
		}
		              
		#region IMemberStorage<T,TMember> implementation
		
		
		public TMember this[T key] {
			get {
				return _innerDictionary[key];
			}
		}
		
		public TMember this[string name ] {
			get {
				return _innerDictionary[_mapName[name]];
			}
		}
		
		public int Count {
			get {
				return _innerDictionary.Count;
			}
		}
		
		public bool IsReadOnly {
			get {
				return false;
			}
		}
		
		public void Add(TMember item)
		{
			if (memberOnAdd != null)
				memberOnAdd(item);

			_mapName.Add(item.Name, item.ID);
			_innerDictionary.Add(item.ID, item);
		}
		
		public void Clear()
		{
			_mapName.Clear();
			_innerDictionary.Clear();
			
			if (onClear !=null)
				onClear();
		}
		
		public bool Contains(TMember item)
		{
			return _innerDictionary.ContainsKey(item.ID);
		}
		
		public void CopyTo(TMember[] array, int arrayIndex)
		{
			_innerDictionary.Values.CopyTo(array,arrayIndex);
		}
		
		public bool Remove(TMember item)
		{
			return _innerDictionary.Remove(item.ID);
		}
		
		public IEnumerator<TMember> GetEnumerator()
		{
			foreach (var item in _innerDictionary.Values)
				yield return item;
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (var item in _innerDictionary.Values)
				yield return item;
		}
		
		public void Dispose()
		{
			this.Clear();
			_innerDictionary.Dispose();
			_mapName.Dispose();
		}
		
		#endregion
	}
}
