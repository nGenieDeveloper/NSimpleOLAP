using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Storage.Interfaces;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of MemberCollection.
	/// </summary>
	public class MemberCollection<T> : BaseDataMemberCollection<T, Member<T>>
		where T: struct, IComparable
	{
		public MemberCollection(IMemberStorage<T, Member<T>> storage)
		{
			_storage = storage;
			base.Init();
		}
	}
}
