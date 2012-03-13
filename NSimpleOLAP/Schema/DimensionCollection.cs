/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 20-02-2012
 * Time: 00:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using NSimpleOLAP.Schema.Interfaces;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of DimensionsCollection.
	/// </summary>
	public class DimensionCollection<T> : BaseDataMemberCollection<T, Dimension<T>>
		where T: struct, IComparable
	{
		public DimensionCollection()
		{
			base.Init();
		}
	}
}
