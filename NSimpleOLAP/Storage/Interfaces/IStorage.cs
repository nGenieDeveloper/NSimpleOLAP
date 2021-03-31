using System;
using System.Collections.Generic;
using System.Configuration;
using NSimpleOLAP.Common;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Data;
using NSimpleOLAP.Query;
using NSimpleOLAP.Query.Interfaces;

namespace NSimpleOLAP.Storage.Interfaces
{
	/// <summary>
	/// Description of IStorage.
	/// </summary>
	public interface IStorage<T,U> : IDisposable
		where T: struct, IComparable
		where U: class
	{
		IEnumerable<U> GetCells(KeyValuePair<T,T>[] pairs);
		U GetCell(KeyValuePair<T,T>[] pairs);

		IEnumerable<U> GetCells(T key, KeyValuePair<T, T>[] pairs);
		U GetCell(T key, KeyValuePair<T, T>[] pairs);

		IEnumerable<U> CellEnumerator();
		void AddRowData(KeyValuePair<T,T>[] pairs, MeasureValuesCollection<T> data);
		int GetCellCount();

		T CreateAggregation(KeyValuePair<T, T>[] axisPairs, IPredicate<T> predicateRoot);

		bool AggregationExists(KeyValuePair<T, T>[] axisPairs, IPredicate<T> predicateRoot);

		void PopulateNewAggregation(T key, IPredicate<T> predicateRoot);

		StorageType StorageType { get; }
		INamespace<T> NameSpace { get; }
		IMemberStorage<T, Dimension<T>> Dimensions { get; }
		IMemberStorage<T, Measure<T>> Measures { get; }
		IMemberStorage<T, Metric<T>> Metrics { get; }
		StorageConfig Config { get; }
	}
}
