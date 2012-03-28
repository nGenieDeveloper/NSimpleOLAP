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
		private StorageElement _element;
		
		public StorageConfigBuilder()
		{
			_element = new StorageElement();
		}
		
		#region public methods
		
		public StorageConfigBuilder SetStoreType(StorageType storetype)
		{
			_element.StoreType = storetype;
			return this;
		}
		
		internal StorageElement Create()
		{
			return _element;
		}
		
		#endregion
	}
}
