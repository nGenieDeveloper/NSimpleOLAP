using System;
using System.Collections.Generic;
using System.Linq;
using NSimpleOLAP.Data.Interfaces;
using NSimpleOLAP.Common.Collections;

namespace NSimpleOLAP.Data
{
	/// <summary>
	/// Description of DataSourceCollection.
	/// </summary>
	public class DataSourceCollection: ICollection<IDataSource>
	{
		private TSDictionary<string,IDataSource> _innerdict;
		
		public DataSourceCollection()
		{
			_innerdict = new TSDictionary<string, IDataSource>();
		}
		
		public IDataSource this[string key]
		{
			get { return _innerdict[key]; }
		}
		
		#region ICollection<IDataSource> implementation
		
		public int Count {
			get {
				return _innerdict.Count;
			}
		}
		
		public bool IsReadOnly {
			get {
				return false;
			}
		}
		
		public void Add(IDataSource item)
		{
			_innerdict.Add(item.Name, item);
		}
		
		public void Clear()
		{
			_innerdict.Clear();
		}
		
		public bool Contains(IDataSource item)
		{
			return _innerdict.ContainsKey(item.Name);
		}
		
		public void CopyTo(IDataSource[] array, int arrayIndex)
		{
			_innerdict.Values.CopyTo(array, arrayIndex);
		}
		
		public bool Remove(IDataSource item)
		{
			return _innerdict.Remove(item.Name);
		}
		
		public IEnumerator<IDataSource> GetEnumerator()
		{
			foreach (var item in _innerdict.Values)
				yield return item;
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (var item in _innerdict.Values)
				yield return item;
		}
		
		#endregion
	}
}
