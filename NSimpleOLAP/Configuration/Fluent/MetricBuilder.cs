/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 23-02-2012
 * Time: 00:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Linq.Expressions;
using NSimpleOLAP.Configuration;

namespace NSimpleOLAP.Configuration.Fluent
{
	/// <summary>
	/// Description of MetricBuilder.
	/// </summary>
	public class MetricBuilder
	{
		private MetricElement _element;
		
		public MetricBuilder()
		{
			_element = new MetricElement();
		}
		
		#region public methods
		
		public MetricBuilder SetName(string name)
		{
			_element.Name = name;
			return this;
		}
		
		public MetricBuilder SetID<T>(T id)
			where T: struct, IComparable
		{
			_element.ID = id;
			return this;
		}
		
		internal MetricElement Create()
		{
			return _element;
		}
		
		#endregion
	}
}
