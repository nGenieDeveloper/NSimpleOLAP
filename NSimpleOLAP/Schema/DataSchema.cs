using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Data;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Common.Interfaces;
using NSimpleOLAP.Storage.Interfaces;


namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of DataSchema.
	/// </summary>
	public class DataSchema<T> : IDisposable, IProcess
		where T: struct, IComparable
	{
		private DataSourceCollection _datasources;
		
		public DataSchema(IMemberStorage<T, Dimension<T>> dimstorage, 
		                  IMemberStorage<T, Measure<T>> messtorage,
		                 IMemberStorage<T, Metric<T>> metstorage)
		{
			Dimensions = new DimensionCollection<T>(dimstorage);
			Measures = new MeasuresCollection<T>(messtorage);
			Metrics = new MetricsCollection<T>(metstorage);
		}
		
		public DataSchema(CubeConfig config, 
		                  DataSourceCollection datasources, 
		                  IMemberStorage<T, Dimension<T>> dimstorage, 
		                  IMemberStorage<T, Measure<T>> messtorage,
		                  IMemberStorage<T, Metric<T>> metstorage):this(dimstorage, messtorage, metstorage)
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
				Dimension<T> ndim = new Dimension<T>(item, _datasources[item.Source]) { Name = item.Name };
				this.Dimensions.Add(ndim);
			}
		}
		
		private void InitializeMeasures()
		{
			foreach (MeasureConfig item in this.Config.Measures)
			{
				Measure<T> nmes = new Measure<T>(item);
				this.Measures.Add(nmes);
			}
		}
		
		private void InitializeMetrics()
		{
			foreach (MetricConfig item in this.Config.Metrics)
			{
				Metric<T> nmes = new Metric<T>(item);
				this.Metrics.Add(nmes);
			}
		}
		
		private void ProcessDimensions()
		{
			foreach (Dimension<T> item in this.Dimensions)
				item.Process();
		}
		
		#endregion
		
		#region IProcess implementation
		
		public void Process()
		{
			this.ProcessDimensions();
		}
		
		public void Refresh()
		{
			throw new NotImplementedException();
		}
		
		#endregion
		
		
	}
}
