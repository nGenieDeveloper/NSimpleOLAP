using NSimpleOLAP.Data;
using System;
using System.Collections.Generic;

namespace NSimpleOLAP.Storage.Interfaces
{
  public interface IFactsProviderCache<T> : IDisposable
    where T : struct, IComparable
  {
    void AddFRow(KeyValuePair<T, T>[] pairs, MeasureValuesCollection<T> data);

    IEnumerable<Tuple<KeyValuePair<T, T>[], MeasureValuesCollection<T>>> FetchFacts(KeyValuePair<T, T>[] pairs);

    IEnumerable<Tuple<KeyValuePair<T, T>[], MeasureValuesCollection<T>>> EnumerateFacts();

    bool RemoveRows(KeyValuePair<T, T>[] pairs);

    bool RemoveRows(params int[] indexes);

    void Clear();
  }
}