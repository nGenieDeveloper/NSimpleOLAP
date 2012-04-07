using System;   
using System.Configuration;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Storage.Interfaces;

namespace NSimpleOLAP.Configuration
{
	/// <summary>
	/// Represents a single XML tag inside a ConfigurationSection
	/// or a ConfigurationElementCollection.
	/// </summary>
	public sealed class StorageConfig : ConfigurationElement
	{
		/// <summary>
		/// 
		/// </summary>
		[ConfigurationProperty("type", IsRequired = true, DefaultValue = StorageType.Molap)]
		public StorageType StoreType {
			get { return (StorageType)this["type"]; }
			set { this["type"] = value; }
		}
		
		[ConfigurationProperty("MolapConfig")]
		public MolapStorageConfig MolapConfig {
			get { return (MolapStorageConfig)this["MolapConfig"]; }
			set { this["MolapConfig"] = value; }
		}
		
		internal Func<KeyValuePair<T,T>[], T> GetHashingFunction<T>()
			where T: struct, IComparable
		{
			return null;
		}
		
		internal Action<object, IVarData<T>> GetVarMergeFunction<T>()
			where T: struct, IComparable
		{
			return null;
		}
	}
	
}

