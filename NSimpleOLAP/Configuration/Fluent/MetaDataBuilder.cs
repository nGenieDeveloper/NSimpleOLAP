using System;
using System.Collections.Generic;
using NSimpleOLAP.Configuration;

namespace NSimpleOLAP.Configuration.Fluent
{
	/// <summary>
	/// Description of MetaDataBuilder.
	/// </summary>
	public class MetaDataBuilder
	{
		private Dictionary<string, Action<DimensionBuilder>> _dimensions;
		private Dictionary<string, Action<MeasureBuilder>> _measures;
		private Dictionary<string, Action<MetricBuilder>> _metrics;
		
		public MetaDataBuilder()
		{
			_dimensions = new Dictionary<string, Action<DimensionBuilder>>();
			_measures = new Dictionary<string, Action<MeasureBuilder>>();
			_metrics = new Dictionary<string, Action<MetricBuilder>>();
		}
		
		#region public methods
		
		public MetaDataBuilder AddDimension(string name, Action<DimensionBuilder> configdimension)
		{
			_dimensions.Add(name, configdimension);
			return this;
		}
		
		public MetaDataBuilder AddMeasure(string name, Action<MeasureBuilder> configmeasure)
		{
			_measures.Add(name, configmeasure);
			return this;
		}
		
		public MetaDataBuilder AddMetric(string name, Action<MetricBuilder> configmetric)
		{
			_metrics.Add(name, configmetric);
			return this;
		}
		
		internal MetaDataConfig Create() 
		{
			MetaDataConfig metadata = new MetaDataConfig();
			
			metadata.Dimensions = new DimensionElementCollection();
			metadata.Measures = new MeasureElementCollection();
			metadata.Metrics = new MetricElementCollection();
		
			foreach (var item in this.GetDimensions())
				metadata.Dimensions.Add(item);
			
			foreach (var item in this.GetMeasures())
				metadata.Measures.Add(item);
			
			foreach (var item in this.GetMetrics())
				metadata.Metrics.Add(item);
		
			return metadata;
		}
		
		#endregion
		
		#region private members
		
		private IEnumerable<DimensionElement> GetDimensions()
		{
			int c = 1;
			
			foreach (var item in _dimensions)
			{
				DimensionBuilder builder = new DimensionBuilder().SetName(item.Key).SetID<int>(c);
				item.Value(builder);
				yield return builder.Create();
				c++;
			}
		}
		
		private IEnumerable<MeasureElement> GetMeasures()
		{
			int c = 1;
			
			foreach (var item in _measures)
			{
				MeasureBuilder builder = new MeasureBuilder().SetName(item.Key).SetID<int>(c);
				item.Value(builder);
				yield return builder.Create();
				c++;
			}
		}
		
		private IEnumerable<MetricElement> GetMetrics()
		{
			int c = 1;
			
			foreach (var item in _metrics)
			{
				MetricBuilder builder = new MetricBuilder().SetName(item.Key).SetID<int>(c);
				item.Value(builder);
				yield return builder.Create();
				c++;
			}
		}
		
		#endregion
	}
}
