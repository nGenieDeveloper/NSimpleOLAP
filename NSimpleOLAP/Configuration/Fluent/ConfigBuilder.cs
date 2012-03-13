/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 22-02-2012
 * Time: 16:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NSimpleOLAP.Configuration.Interfaces;

namespace NSimpleOLAP.Configuration.Fluent
{
	/// <summary>
	/// Description of ConfigBuilder.
	/// </summary>
	public class ConfigBuilder
	{
		private StorageConfigBuilder _storeconfig;
		private DataSourceBuilder _datasourceconfig;
		private MetaDataBuilder _metadataconfig;
		
		public ConfigBuilder()
		{
			_storeconfig = new StorageConfigBuilder();
			_datasourceconfig = new DataSourceBuilder();
			_metadataconfig = new MetaDataBuilder();
		}
		
		#region public methods
		
		public ConfigBuilder Storage(Action<StorageConfigBuilder> storeconfig)
		{
			storeconfig(_storeconfig);
			return this;
		}
		
		public ConfigBuilder DataSource(Action<DataSourceBuilder> datasourceconfig)
		{
			datasourceconfig(_datasourceconfig);
			return this;
		}
		
		public ConfigBuilder MetaData(Action<MetaDataBuilder> medataconfig)
		{
			medataconfig(_metadataconfig);
			return this;
		}
		
		public ICubeConfig<T> Create<T>()
			where T: struct, IComparable
		{
			return null;
		}
		
		#endregion
	}
}
