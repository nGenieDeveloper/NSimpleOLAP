using System;
using System.Collections.Generic;
using System.Linq;
using NSimpleOLAP.Common;
using NSimpleOLAP.Common.Collections;
using NSimpleOLAP.Schema.Interfaces;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of NameSpace.
	/// </summary>
	public class NameSpace<T> : INamespace<T>
		where T: struct, IComparable
	{
		private TSDictionary<T, IDataItem<T>> _items;
		private TSDictionary<string, T> _index;
		private AbsIdentityKey<T> _keybuilder;
		
		public NameSpace(AbsIdentityKey<T> keybuilder)
		{
			_keybuilder = keybuilder;
			_index = new TSDictionary<string, T>();
			_items = new TSDictionary<T, IDataItem<T>>();
		}
		
		public IDataItem<T> this[T key] {
			get {
				return _items[key];
			}
		}
		
		public IDataItem<T> this[string name] {
			get {
				return _items[_index[name]];
			}
		}
		
		public int Count {
			get {
				return _items.Count;
			}
		}
		
		public bool IsReadOnly {
			get {
				return false;
			}
		}
		
		public void Add(IDataItem<T> item)
		{
			if (item.ID.Equals(default(T)))
				item.ID = _keybuilder.GetNextKey();
			
			_items.Add(item.ID, item);
			_index.Add(item.Name,item.ID);
		}
		
		public void Clear()
		{
			_items.Clear();
			_index.Clear();
		}
		
		public bool Contains(IDataItem<T> item)
		{
			return _items.ContainsKey(item.ID) || _index.ContainsKey(item.Name);
		}
		
		public void CopyTo(IDataItem<T>[] array, int arrayIndex)
		{
			_items.Values.CopyTo(array, arrayIndex);
		}
		
		public bool Remove(IDataItem<T> item)
		{
			return _items.Remove(item.ID) && _index.Remove(item.Name);
		}
		
		public IEnumerator<IDataItem<T>> GetEnumerator()
		{
			foreach (var item in _items.Values)
				yield return item;
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (var item in _items.Values)
				yield return item;
		}
		
		public void Dispose()
		{
			_items.Dispose();
			_index.Dispose();
		}
	}
}
