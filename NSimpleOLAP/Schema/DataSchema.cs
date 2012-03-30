using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Data;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Common.Interfaces;


namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of DataSchema.
	/// </summary>
	public class DataSchema<T> : IDisposable, IProcess
		where T: struct, IComparable
	{
		private DataSourceCollection _datasources;
		
		public DataSchema()
		{
			Dimensions = new DimensionCollection<T>(AbsIdentityKey<T>.Create());
			Measures = new MeasuresCollection<T>(AbsIdentityKey<T>.Create());
			Metrics = new MetricsCollection<T>(AbsIdentityKey<T>.Create());
		}
		
		public DataSchema(CubeConfig config, DataSourceCollection datasources):this()
		{
			_datasources = datasources;
			this.Config = config.MetaData;
			this.Initialize();
		}
		
		public MetaDataConfig Config
		{
			get;
			set;
		}
		
		public DimensionCollection<T> Dimensions 
		{
			get;
			private set;
		}
		
		public MeasuresCollection<T> Measures 
		{
			get;
			private set;
		}
		
		public MetricsCollection<T> Metrics 
		{
			get;
			private set;
		}
		
		#region IDisposable implementation
		
		public void Dispose()
		{
			this.Dimensions.Dispose();
			this.Measures.Dispose();
			this.Metrics.Dispose();
		}
		
		#endregion
		
		#region private members
		
		private void Initialize()
		{
			this.InitializeDimensions();
			this.InitializeMeasures();
			this.InitializeMetrics();
		}
		
		private void InitializeDimensions()
		{
			foreach (DimensionConfig item in this.Config.Dimensions)
			{
				Dimension<T> ndim = new Dimension<T>(item, _datasources[item.Source]) 
									{ 
										Name = item.Name
											//falta ID
									};
				this.Dimensions.Add(ndim);
			}
		}
		
		private void InitializeMeasures()
		{
			foreach (MeasureConfig item in this.Config.Measures)
			{
				Measure<T> nmes = new Measure<T>(item)
									{ 
										Name = item.Name
											//falta ID
									};
				this.Measures.Add(nmes);
			}
		}
		
		private void InitializeMetrics()
		{
			foreach (MetricConfig item in this.Config.Metrics)
			{
				Metric<T> nmes = new Metric<T>(item)
									{ 
										Name = item.Name
											//falta ID
									};
				this.Metrics.Add(nmes);
			}
		}
		
		
		#endregion
		
		#region IProcess implementation
		#endregion
		
		public void Process()
		{
			throw new NotImplementedException();
		}
		
		public void Refresh()
		{
			throw new NotImplementedException();
		}
	}
}
