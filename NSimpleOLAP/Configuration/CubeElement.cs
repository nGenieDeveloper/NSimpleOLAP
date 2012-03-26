using System;   
using System.Configuration;

namespace NSimpleOLAP.Configuration
{
	/// <summary>
	/// Represents a single XML tag inside a ConfigurationSection
	/// or a ConfigurationElementCollection.
	/// </summary>
	public sealed class CubeElement : ConfigurationElement
	{
		/// <summary>
		/// The attribute <c>name</c> of a <c>CubeElement</c>.
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
		[ConfigurationProperty("DataSources")]
		public DataSourceElementCollection DataSources {
			get { return (DataSourceElementCollection)this["DataSources"]; }
			set { this["DataSources"] = value; }
		}
		
		[ConfigurationProperty("MetaData")]
		public MetaDataElement MetaData {
			get { return (MetaDataElement)this["MetaData"]; }
			set { this["MetaData"] = value; }
		}
		
		[ConfigurationProperty("Storage")]
		public StorageElement Storage {
			get { return (StorageElement)this["Storage"]; }
			set { this["Storage"] = value; }
		}
	}
	
}

