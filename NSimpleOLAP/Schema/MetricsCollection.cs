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
		public MetricsCollection(AbsIdentityKey<T> keybuilder)
		{
			_keyBuilder = keybuilder;
			base.Init();
		}
	}
}
