using System;   
using System.Configuration;

namespace NSimpleOLAP.Configuration
{
	/// <summary>
	/// Represents a single XML tag inside a ConfigurationSection
	/// or a ConfigurationElementCollection.
	/// </summary>
	public sealed class CubeConfig : ConfigurationElement
	{
		/// <summary>
		/// The attribute <c>name</c> of a <c>CubeElement</c>.
		/// </summary>
		[ConfigurationProperty("name", IsKey = true, IsRequired = true)]
		[StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\")]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}
	
		/// <summary>
		/// 
		/// </summary>
		[ConfigurationProperty("source", IsRequired = true)]
		[StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\")]
		public string Source
		{
			get { return (string)this["source"]; }
			set { this["source"] = value; }
		}
	
		/// <summary>
		/// 
		/// </summary>
		[ConfigurationProperty("DataSources")]
		public DataSourceConfigCollection DataSources {
			get { return (DataSourceConfigCollection)this["DataSources"]; }
			set { this["DataSources"] = value; }
		}
		
		[ConfigurationProperty("MetaData")]
		public MetaDataConfig MetaData {
			get { return (MetaDataConfig)this["MetaData"]; }
			set { this["MetaData"] = value; }
		}
		
		[ConfigurationProperty("Storage")]
		public StorageElement Storage {
			get { return (StorageElement)this["Storage"]; }
			set { this["Storage"] = value; }
		}
	}
	
}

