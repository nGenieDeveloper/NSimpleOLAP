/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 20-02-2012
 * Time: 23:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
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
