using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Schema.Interfaces;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of MetricsCollection.
	/// </summary>
	public class MetricsCollection<T> : BaseDataMemberCollection<T, Metric<T>>
		where T: struct, IComparable
	{
		public MetricsCollection(INamespace<T> nameSpace)
		{
			_namespace = nameSpace;
			base.Init();
		}
	}
}
