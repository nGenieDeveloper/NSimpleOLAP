/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 21-02-2012
 * Time: 00:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using NSimpleOLAP.Schema.Interfaces;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of MeasuresCollection.
	/// </summary>
	public class MeasuresCollection<T> : BaseDataMemberCollection<T, Measure<T>>
		where T: struct, IComparable
	{
		public MeasuresCollection()
		{
			base.Init();
		}
	}
}
