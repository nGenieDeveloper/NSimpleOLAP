using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Configuration;


namespace NSimpleOLAP.Configuration.Fluent
{
	/// <summary>
	/// Description of StorageConfigBuilder.
	/// </summary>
	public class StorageConfigBuilder
	{
		private StorageConfig _element;
		
		public StorageConfigBuilder()
		{
			_element = new StorageConfig();
		}
		
		#region public methods
		
		public StorageConfigBuilder SetStoreType(StorageType storetype)
		{
			_element.StoreType = storetype;
			return this;
		}
		
		internal StorageConfig Create()
		{
			return _element;
		}
		
		#endregion
	}
}
