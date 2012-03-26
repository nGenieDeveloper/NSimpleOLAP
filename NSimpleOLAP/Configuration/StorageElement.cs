/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 25-03-2012
 * Time: 03:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
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
	public sealed class StorageElement : ConfigurationElement
	{
		/// <summary>
		/// 
		/// </summary>
		[ConfigurationProperty("type", IsRequired = true, DefaultValue = StorageType.Molap)]
		public StorageType StoreType {
			get { return (StorageType)this["type"]; }
			set { this["special"] = value; }
		}
		
		internal Func<KeyValuePair<T,T>[], T> GetHashingFunction<T>()
			where T: struct, IComparable
		{
			throw new NotImplementedException();
		}
		
		internal Action<object, IVarData<T>> GetVarMergeFunction<T>()
			where T: struct, IComparable
		{
			throw new NotImplementedException();
		}
	}
	
}

