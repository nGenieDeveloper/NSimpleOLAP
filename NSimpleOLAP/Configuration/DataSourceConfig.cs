using System;   
using System.Configuration;
using NSimpleOLAP.Common;

namespace NSimpleOLAP.Configuration
{
	/// <summary>
	/// Represents a single XML tag inside a ConfigurationSection
	/// or a ConfigurationElementCollection.
	/// </summary>
	public sealed class DataSourceConfig : ConfigurationElement
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
		
		[ConfigurationProperty("DBConfig")]
		public DBConfigElement DBConfig
		{
			get { return (DBConfigElement)this["DBConfig"]; }
			set { this["DBConfig"] = value; }
		}
		
		[ConfigurationProperty("CSVConfig")]
		public CSVElement CSVConfig
		{
			get { return (CSVElement)this["CSVConfig"]; }
			set { this["CSVConfig"] = value; }
		}
		
		[ConfigurationProperty("DTableConfig")]
		internal DataTableConfig DTableConfig
		{
			get { return (DataTableConfig)this["DTableConfig"]; }
			set { this["DTableConfig"] = value; }
		}
	}
	
}

