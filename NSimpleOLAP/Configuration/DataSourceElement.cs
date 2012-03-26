using System;   
using System.Configuration;
using NSimpleOLAP.Common;

namespace NSimpleOLAP.Configuration
{
	/// <summary>
	/// Represents a single XML tag inside a ConfigurationSection
	/// or a ConfigurationElementCollection.
	/// </summary>
	public sealed class DataSourceElement : ConfigurationElement
	{
		/// <summary>
		/// The attribute <c>name</c> of a <c>DataSourceConfigElement</c>.
		/// </summary>
		[ConfigurationProperty("name", IsKey = true, IsRequired = true)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}
	
	
		/// <summary>
		/// 
		/// </summary>
		[ConfigurationProperty("type")]
		public DataSourceType SourceType {
			get { return (DataSourceType)this["type"]; }
			set { this["type"] = value; }
		}
		
		[ConfigurationProperty("Fields")]
		public FieldElementCollection Fields {
			get { return (FieldElementCollection)this["Fields"]; }
			set { this["type"] = value; }
		}
		
		[ConfigurationProperty("connection")]
		public string Connection
		{
			get { return (string)this["connection"]; }
			set { this["connection"] = value; }
		}
		
		[ConfigurationProperty("query")]
		public string Query
		{
			get { return (string)this["query"]; }
			set { this["query"] = value; }
		}
		
		[ConfigurationProperty("path")]
		public string FilePath
		{
			get { return (string)this["path"]; }
			set { this["path"] = value; }
		}
	}
	
}

