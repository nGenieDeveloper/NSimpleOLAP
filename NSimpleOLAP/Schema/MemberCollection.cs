using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Schema.Interfaces;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of MemberCollection.
	/// </summary>
	public class MemberCollection<T> : BaseDataMemberCollection<T, Member<T>>
		where T: struct, IComparable
	{
		public MemberCollection()
		{
			base.Init();
		}
	}
}
