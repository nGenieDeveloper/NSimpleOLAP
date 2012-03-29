using System;
using System.Configuration;
using System.Collections.Generic;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Data.Readers;
using NSimpleOLAP.Data.Interfaces;


namespace NSimpleOLAP.Data
{
	/// <summary>
	/// Description of DefaultDataSource.
	/// </summary>
	public class DefaultDataSource : IDataSource
	{
		public DefaultDataSource()
		{
		}
		
		public DefaultDataSource(DataSourceConfig config)
		{
			this.Config = config;
			this.Name = config.Name;
		}
		
		public string Name {
			get;
			set;
		}
		
		public DataSourceConfig Config {
			get;
			set;
		}
		
		public AbsReader GetReader()
		{
			return AbsReader.Create(this.Config);
		}
	}
}
