using System;
using System.Collections.Generic;
using NSimpleOLAP;
using NSimpleOLAP.Configuration;

namespace NSimpleOLAP.Configuration.Fluent
{
	/// <summary>
	/// Description of ConfigBuilder.
	/// </summary>
	public class CubeBuilder
	{
		private string _name = string.Empty;
		private string _source = string.Empty;
		private StorageConfigBuilder _storeconfig;
		private List<DataSourceBuilder> _datasourceconfigs;
		private MetaDataBuilder _metadataconfig;
		
		public CubeBuilder()
		{
			_storeconfig = new StorageConfigBuilder();
			_datasourceconfigs = new List<DataSourceBuilder>();
			_metadataconfig = new MetaDataBuilder();
		}
		
		#region public methods
		
		public CubeBuilder SetName(string name)
		{
			_name = name;
			return this;
		}
		
		public CubeBuilder SetSource(string source)
		{
			_source = source;
			return this;
		}
		
		public CubeBuilder Storage(Action<StorageConfigBuilder> storeconfig)
		{
			storeconfig(_storeconfig);
			return this;
		}
		
		public CubeBuilder AddDataSource(Action<DataSourceBuilder> datasourceconfig)
		{
			DataSourceBuilder builder = new DataSourceBuilder();
			
			datasourceconfig(builder);
			_datasourceconfigs.Add(builder);
			
			return this;
		}
		
		public CubeBuilder MetaData(Action<MetaDataBuilder> medataconfig)
		{
			medataconfig(_metadataconfig);
			return this;
		}
		
		public CubeConfig Create()
		{
			CubeConfig cube = new CubeConfig();
			
			cube.Name = _name;
			cube.Storage = _storeconfig.Create();
			cube.MetaData = _metadataconfig.Create();
			cube.DataSources = new DataSourceConfigCollection();
			
			foreach (var item in _datasourceconfigs)
				cube.DataSources.Add(item.Create());
			
			return cube;
		}
		
		#endregion
	}
}
