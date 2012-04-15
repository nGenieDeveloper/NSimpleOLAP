using System;
using System.Collections.Generic;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Storage.Interfaces;

namespace NSimpleOLAP
{
	/// <summary>
	/// Description of CellCollection.
	/// </summary>
	public class CellCollection<T> : ICellCollection<T, Cell<T>>
		where T: struct, IComparable
	{
		private IStorage<T,Cell<T>> _storage;
			
		public CellCollection(IStorage<T,Cell<T>> storage)
		{
			_storage = storage;
		}
		
		public Cell<T> this[T[] keys] {
			get {
				throw new NotImplementedException();
			}
		}
		
		public int Count {
			get {
				throw new NotImplementedException();
			}
		}
		
		public bool IsReadOnly {
			get {
				throw new NotImplementedException();
			}
		}
		
		public bool ContainsKey(T[] keys)
		{
			throw new NotImplementedException();
		}
		
		public void Add(Cell<T> item)
		{
			throw new NotImplementedException();
		}
		
		public void Clear()
		{
			throw new NotImplementedException();
		}
		
		public bool Contains(Cell<T> item)
		{
			throw new NotImplementedException();
		}
		
		public void CopyTo(Cell<T>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}
		
		public bool Remove(Cell<T> item)
		{
			throw new NotImplementedException();
		}
		
		public IEnumerator<Cell<T>> GetEnumerator()
		{
			throw new NotImplementedException();
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
