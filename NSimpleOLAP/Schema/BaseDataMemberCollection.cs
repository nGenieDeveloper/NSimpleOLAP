using System;
using System.Collections.Generic;
using System.Linq;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Common;
using NSimpleOLAP.Common.Collections;
using NSimpleOLAP.Storage.Interfaces;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of BaseDataMemberCollection.
	/// </summary>
	public abstract class BaseDataMemberCollection<T,D> : IDataItemCollection<T, D>
		where T: struct, IComparable
		where D: class, IDataItem<T>
	{
		protected IMemberStorage<T, D> _storage;
		
		protected void Init()
		{
			
		}
		
		public D this[T key] {
			get {
				return _storage[key];
			}
		}
		
		public D this[string name] {
			get {
				return _storage[name];
			}
		}
		
		public int Count {
			get {
				return _storage.Count;
			}
		}
		
		public bool IsReadOnly {
			get {
				return _storage.IsReadOnly;
			}
		}
		
		public void Add(D item)
		{
			_storage.Add(item);
		}
		
		public void Clear()
		{
			_storage.Clear();
		}
		
		public bool Contains(D item)
		{
			return _storage.Contains(item);
		}

		public bool Contains(string item)
		{
			return _storage.Any(x => x.Name.Equals(item));
		}

		public bool ContainsKey(T key)
		{
			return _storage.ContainsKey(key);
		}
		
		public void CopyTo(D[] array, int arrayIndex)
		{
			_storage.CopyTo(array,arrayIndex);
		}
		
		public bool Remove(D item)
		{
			return _storage.Remove(item);
		}
		
		public IEnumerator<D> GetEnumerator()
		{
			foreach (var item in _storage)
				yield return item;
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (var item in _storage)
				yield return item;
		}
		
		public void Dispose()
		{
			_storage.Dispose();
		}
	}
}
