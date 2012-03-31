using System;
using System.Collections.Generic;
using System.Linq;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Common;
using NSimpleOLAP.Common.Collections;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of BaseDataMemberCollection.
	/// </summary>
	public abstract class BaseDataMemberCollection<T,D> : IDataItemCollection<T, D>
		where T: struct, IComparable
		where D: class, IDataItem<T>
	{
		private TSDictionary<T,D> _innerDictionary;
		private TSDictionary<string, T> _mapName;
		protected INamespace<T> _namespace;
		
		protected void Init()
		{
			_innerDictionary = new TSDictionary<T, D>();
			_mapName = new TSDictionary<string, T>();
		}
		
		public D this[T key] {
			get {
				return _innerDictionary[key];
			}
			set {
				_innerDictionary[key] = value;
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
		
		public D GetByName(string name)
		{
			if (_mapName.ContainsKey(name))
				return _innerDictionary[_mapName[name]];
			else
				return null;
		}
		
		public void Add(D item)
		{
			_namespace.Add(item);
			_mapName.Add(item.Name, item.ID);
			_innerDictionary.Add(item.ID, item);
		}
		
		public void Clear()
		{
			_mapName.Clear();
			_innerDictionary.Clear();
		}
		
		public bool Contains(D item)
		{
			return _innerDictionary.ContainsKey(item.ID);
		}
		
		public void CopyTo(D[] array, int arrayIndex)
		{
			_innerDictionary.Values.CopyTo(array,arrayIndex);
		}
		
		public bool Remove(D item)
		{
			return _innerDictionary.Remove(item.ID);
		}
		
		public IEnumerator<D> GetEnumerator()
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
	}
}
