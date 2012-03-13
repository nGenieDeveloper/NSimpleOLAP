/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 20-02-2012
 * Time: 00:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace NSimpleOLAP.Interfaces
{
	/// <summary>
	/// Description of IValueCollection.
	/// </summary>
	public interface IValueCollection<T> : IDictionary<T, ValueType>
        where T : struct, IComparable
	{
		S GetValue<S>(T key);
        bool InsertOrUpdate(T key, int value);
        bool InsertOrUpdate(T key, long value);
        bool InsertOrUpdate(T key, decimal value);
        bool InsertOrUpdate(T key, float value);
        bool InsertOrUpdate(T key, double value);
	}
}
