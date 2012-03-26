using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Configuration;

namespace NSimpleOLAP.Configuration.Fluent
{
	/// <summary>
	/// Description of DataSourceBuilder.
	/// </summary>
	public class DataSourceBuilder
	{
		private DataSourceElement _element;
		
		public DataSourceBuilder()
		{
			_element = new DataSourceElement();
			_element.Fields = new FieldElementCollection();
		}
		
		#region public methods
		
		public DataSourceBuilder SetName(string name)
		{
			_element.Name = name;
			return this;
		}
		
		public DataSourceBuilder SetConnection(string connection)
		{
			_element.Connection = connection;
			return this;
		}
		
		public DataSourceBuilder SetQuery(string query)
		{
			_element.Connection = query;
			return this;
		}
		
		public DataSourceBuilder SetFilePath(string path)
		{
			_element.FilePath = path;
			return this;
		}
		
		public DataSourceBuilder SetSourceType(DataSourceType sourcetype)
		{
			_element.SourceType = sourcetype;
			return this;
		}
		
		public DataSourceBuilder AddField(string name, Type type)
		{
			FieldElement field = new FieldElement() { Name = name, FieldType = type };
			_element.Fields.Add(field);
			return this;
		}
		
		public DataSourceElement Create()
		{
			return _element;
		}
		
		#endregion
	}
}
