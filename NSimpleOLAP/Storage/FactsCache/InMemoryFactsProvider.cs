using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSimpleOLAP.Storage.Interfaces;
using NSimpleOLAP.Data;
using NSimpleOLAP.Common;
using NSimpleOLAP.Common.Hashing;
using NSimpleOLAP.Common.Collections;

namespace NSimpleOLAP.Storage.FactsCache
{
  public class InMemoryFactsProvider<T> : IFactsProviderCache<T>
    where T : struct, IComparable
  {
    private Hasher<T> _hasher;
    private MolapHashTypes _hashingtype;

    public InMemoryFactsProvider(MolapHashTypes hashingType)
    {
      _hashingtype = hashingType;
      this.Init();
    }

    public int Count => throw new NotImplementedException();

    #region private methods

    private void Init()
    {
      _hasher = Hasher<T>.Create(this._hashingtype);
    }

    #endregion

    public void AddFRow(KeyValuePair<T, T>[] pairs, MeasureValuesCollection<T> data)
    {
      throw new NotImplementedException();
    }

    public void Clear()
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Tuple<KeyValuePair<T, T>[], MeasureValuesCollection<T>>> EnumerateFacts()
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Tuple<KeyValuePair<T, T>[], MeasureValuesCollection<T>>> FetchFacts(KeyValuePair<T, T>[] pairs)
    {
      throw new NotImplementedException();
    }

    public bool RemoveRows(KeyValuePair<T, T>[] pairs)
    {
      throw new NotImplementedException();
    }

    public bool RemoveRows(params int[] indexes)
    {
      throw new NotImplementedException();
    }
  }
}
