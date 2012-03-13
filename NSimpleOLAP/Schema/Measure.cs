/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 20-02-2012
 * Time: 23:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NSimpleOLAP.Schema.Interfaces;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of Measure.
	/// </summary>
	public class Measure<T> : IMeasure<T>
		where T: struct, IComparable
	{
		public Measure()
		{
		}
		
		public Type DataType {
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
