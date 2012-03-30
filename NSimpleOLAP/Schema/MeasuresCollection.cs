using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Schema.Interfaces;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of MeasuresCollection.
	/// </summary>
	public class MeasuresCollection<T> : BaseDataMemberCollection<T, Measure<T>>
		where T: struct, IComparable
	{
		public MeasuresCollection(AbsIdentityKey<T> keybuilder)
		{
			_keyBuilder = keybuilder;
			base.Init();
		}
	}
}
