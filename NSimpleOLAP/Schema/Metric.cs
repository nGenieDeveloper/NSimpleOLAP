/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 20-02-2012
 * Time: 23:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NSimpleOLAP.Schema.Interfaces;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of Metric.
	/// </summary>
	public class Metric<T> : IMetric<T>
		where T: struct, IComparable
	{
		public Metric()
		{
		}
		
		public object Expression {
			get;
			set;
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
