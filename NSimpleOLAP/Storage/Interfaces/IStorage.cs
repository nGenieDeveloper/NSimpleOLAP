using System;
using System.Collections.Generic;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Schema.Interfaces;

namespace NSimpleOLAP.Storage.Interfaces
{
	/// <summary>
	/// Description of IStorage.
	/// </summary>
	public interface IStorage<T,U> : IDisposable
		where T: struct, IComparable
		where U: class, new()
	{
		IEnumerable<U> GetCells(KeyValuePair<T,T>[] pairs);
		void AddRowData(KeyValuePair<T,T>[] pairs, IVarData<T> data);
		
		INamespace<T> NameSpace { get; }
		IMemberStorage<T, Dimension<T>> Dimensions { get; }
		IMemberStorage<T, Measure<T>> Measures { get; }
		IMemberStorage<T, Metric<T>> Metrics { get; }
	}
}
