using System;
using System.Collections.Generic;
using NSimpleOLAP.Configuration;

namespace NSimpleOLAP.Configuration.Fluent
{
	/// <summary>
	/// Description of ConfigBuilder.
	/// </summary>
	public class CubeConfigBuilder
	{
		private string _name = string.Empty;
		private StorageConfigBuilder _storeconfig;
		private List<DataSourceBuilder> _datasourceconfigs;
		private MetaDataBuilder _metadataconfig;
		
		public CubeConfigBuilder()
		{
			_storeconfig = new StorageConfigBuilder();
			_datasourceconfigs = new List<DataSourceBuilder>();
			_metadataconfig = new MetaDataBuilder();
		}
		
		#region public methods
		
		public CubeConfigBuilder SetName(string name)
		{
			_name = name;
			return this;
		}
		
		public CubeConfigBuilder Storage(Action<StorageConfigBuilder> storeconfig)
		{
			storeconfig(_storeconfig);
			return this;
		}
		
		public CubeConfigBuilder AddDataSource(Action<DataSourceBuilder> datasourceconfig)
		{
			DataSourceBuilder builder = new DataSourceBuilder();
			
			datasourceconfig(builder);
			_datasourceconfigs.Add(builder);
			
			return this;
		}
		
		public CubeConfigBuilder MetaData(Action<MetaDataBuilder> medataconfig)
		{
			medataconfig(_metadataconfig);
			return this;
		}
		
		public CubeElement Create()
		{
			CubeElement cube = new CubeElement();
			
			cube.Name = _name;
			cube.Storage = _storeconfig.Create();
			cube.MetaData = _metadataconfig.Create();
			cube.DataSources = new DataSourceElementCollection();
			
			foreach (var item in _datasourceconfigs)
				cube.DataSources.Add(item.Create());
			
			return cube;
		}
		
		#endregion
	}
}
