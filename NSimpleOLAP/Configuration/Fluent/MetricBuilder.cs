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
		private MetricConfig _element;
		
		public MetricBuilder()
		{
			_element = new MetricConfig();
		}
		
		#region public methods
		
		public MetricBuilder SetName(string name)
		{
			_element.Name = name;
			return this;
		}
		
		public MetricBuilder SetType(Type type)
		{
			_element.DataType = type;
			return this;
		}
		
		public MetricBuilder SetExpression(Expression expression)
		{
			_element.MetricFunction = expression;
			return this;
		}
		
		internal MetricConfig Create()
		{
			return _element;
		}
		
		#endregion
	}
}
