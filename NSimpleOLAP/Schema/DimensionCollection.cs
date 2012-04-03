using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Storage.Interfaces;


namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of DimensionsCollection.
	/// </summary>
	public class DimensionCollection<T> : BaseDataMemberCollection<T, Dimension<T>>
		where T: struct, IComparable
	{
		public DimensionCollection(IMemberStorage<T, Dimension<T>> storage)
		{
			_storage = storage;
			base.Init();
		}
	}
}
