/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 20-02-2012
 * Time: 20:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NSimpleOLAP.Schema.Interfaces;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of Member.
	/// </summary>
	public class Member<T> : IMember<T>
		where T: struct, IComparable
	{
		public Member()
		{
		}
		
		public string Name {
			get;
			set;
		}
		
		public T ID {
			get;
			set;
		}
	}
}
