using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Schema.Interfaces;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of DimensionsCollection.
	/// </summary>
	public class DimensionCollection<T> : BaseDataMemberCollection<T, Dimension<T>>
		where T: struct, IComparable
	{
		public DimensionCollection(INamespace<T> nameSpace)
		{
			_namespace = nameSpace;
			base.Init();
		}
	}
}
