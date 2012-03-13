/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 19-02-2012
 * Time: 22:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using NSimpleOLAP.Storage.Interfaces;

namespace NSimpleOLAP.Storage
{
	/// <summary>
	/// Description of VarsCollection.
	/// </summary>
	public class VarsCollection<T> : IVarData<T>
		where T: struct, IComparable
	{
		private Dictionary<T,object> _innerdict;
			
		public VarsCollection()
		{
			_innerdict = new Dictionary<T, object>();
		}
		
		public object this[T key] {
			get {
				return _innerdict[key];
			}
			set {
				_innerdict[key] = value;
			}
		}
		
		public ICollection<T> Keys {
			get {
				return _innerdict.Keys;
			}
		}
		
		public ICollection<object> Values {
			get {
				return _innerdict.Values;
			}
		}
		
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
		
		public bool ContainsKey(T key)
		{
			return _innerdict.ContainsKey(key);
		}
		
		public void Add(T key, object value)
		{
			throw new NotImplementedException();
		}
		
		public bool Remove(T key)
		{
			return _innerdict.Remove(key);
		}
		
		public bool TryGetValue(T key, out object value)
		{
			return _innerdict.TryGetValue(key, out value);
		}
		
		public void Add(KeyValuePair<T, object> item)
		{
			_innerdict.Add(item.Key, item.Value);
		}
		
		public void Clear()
		{
			_innerdict.Clear();
		}
		
		public bool Contains(KeyValuePair<T, object> item)
		{
			return _innerdict.ContainsKey(item.Key) && _innerdict.ContainsValue(item.Value);
		}
		
		public void CopyTo(KeyValuePair<T, object>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}
		
		public bool Remove(KeyValuePair<T, object> item)
		{
			return _innerdict.Remove(item.Key);
		}
		
		public IEnumerator<KeyValuePair<T, object>> GetEnumerator()
		{
			foreach (var item in _innerdict)
				yield return item;
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (var item in _innerdict)
				yield return item;
		}
	}
}
