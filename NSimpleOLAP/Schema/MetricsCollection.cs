/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 21-02-2012
 * Time: 00:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using NSimpleOLAP.Schema.Interfaces;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of MetricsCollection.
	/// </summary>
	public class MetricsCollection<T> : BaseDataMemberCollection<T, Metric<T>>
		where T: struct, IComparable
	{
		public MetricsCollection()
		{
			base.Init();
		}
	}
}
