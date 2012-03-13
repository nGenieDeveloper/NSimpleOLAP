/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 23-02-2012
 * Time: 01:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Configuration;
using System.Linq.Expressions;


namespace NSimpleOLAP.Configuration
{
	/// <summary>
	/// Description of MetricElement.
	/// </summary>
	public class MetricElement
	{
		public MetricElement()
		{
		}
		
		public string Name
		{
			get;
			set;
		}
		
		public ValueType ID
		{
			get;
			set;
		}
		
		public Type DataType
		{
			get;
			set;
		}
		
		public Expression MetricFunction
		{
			get;
			set;
		}
		
	}
}
