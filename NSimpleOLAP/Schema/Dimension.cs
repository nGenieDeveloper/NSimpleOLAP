/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 20-02-2012
 * Time: 20:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using NSimpleOLAP.Schema.Interfaces;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of Dimension.
	/// </summary>
	public class Dimension<T> : IDimension<T>
		where T: struct, IComparable
	{
		public Dimension()
		{
			Members = new MemberCollection<T>();
		}
		
		public string Name {
			get;
			set;
		}
		
		public T ID {
			get;
			set;
		}
		
		public MemberCollection<T> Members {
			get;
			private set;
		}
	}
}
