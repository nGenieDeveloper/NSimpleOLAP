/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 21-02-2012
 * Time: 00:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using NSimpleOLAP.Interfaces;

namespace NSimpleOLAP
{
	/// <summary>
	/// Description of ValuesCollection.
	/// </summary>
	public class ValuesCollection<T> : IValueCollection<T>
		where T: struct, IComparable
	{
		public ValuesCollection()
		{
		}
		
		public ValueType this[T key] {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public ICollection<T> Keys {
			get {
				throw new NotImplementedException();
			}
		}
		
		public ICollection<ValueType> Values {
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
				return false;
			}
		}
		
		public S GetValue<S>(T key)
		{
			throw new NotImplementedException();
		}
		
		public bool InsertOrUpdate(T key, int value)
		{
			throw new NotImplementedException();
		}
		
		public bool InsertOrUpdate(T key, long value)
		{
			throw new NotImplementedException();
		}
		
		public bool InsertOrUpdate(T key, decimal value)
		{
			throw new NotImplementedException();
		}
		
		public bool InsertOrUpdate(T key, float value)
		{
			throw new NotImplementedException();
		}
		
		public bool InsertOrUpdate(T key, double value)
		{
			throw new NotImplementedException();
		}
		
		public bool ContainsKey(T key)
		{
			throw new NotImplementedException();
		}
		
		public void Add(T key, ValueType value)
		{
			throw new NotImplementedException();
		}
		
		public bool Remove(T key)
		{
			throw new NotImplementedException();
		}
		
		public bool TryGetValue(T key, out ValueType value)
		{
			throw new NotImplementedException();
		}
		
		public void Add(KeyValuePair<T, ValueType> item)
		{
			throw new NotImplementedException();
		}
		
		public void Clear()
		{
			throw new NotImplementedException();
		}
		
		public bool Contains(KeyValuePair<T, ValueType> item)
		{
			throw new NotImplementedException();
		}
		
		public void CopyTo(KeyValuePair<T, ValueType>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}
		
		public bool Remove(KeyValuePair<T, ValueType> item)
		{
			throw new NotImplementedException();
		}
		
		public IEnumerator<KeyValuePair<T, ValueType>> GetEnumerator()
		{
			throw new NotImplementedException();
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
