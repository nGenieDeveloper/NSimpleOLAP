using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Storage.Interfaces;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of MeasuresCollection.
	/// </summary>
	public class MeasuresCollection<T> : BaseDataMemberCollection<T, Measure<T>>
		where T: struct, IComparable
	{
		public MeasuresCollection(IMemberStorage<T, Measure<T>> storage)
		{
			_storage = storage;
			base.Init();
		}

    public override Measure<T> Next(T key)
    {
      throw new NotImplementedException();
    }

    public override Measure<T> Previous(T key)
    {
      throw new NotImplementedException();
    }
  }
}
