﻿using System;
using System.Collections.Generic;

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

      metadata.Dimensions = new DimensionConfigCollection();
      metadata.Measures = new MeasureConfigCollection();
      metadata.Metrics = new MetricConfigCollection();

      foreach (var item in this.GetDimensions())
        metadata.Dimensions.Add(item);

      foreach (var item in this.GetMeasures())
        metadata.Measures.Add(item);

      foreach (var item in this.GetMetrics())
        metadata.Metrics.Add(item);

      return metadata;
    }

    #endregion public methods

    #region private members

    private IEnumerable<DimensionConfig> GetDimensions()
    {
      foreach (var item in _dimensions)
      {
        DimensionBuilder builder = new DimensionBuilder().SetName(item.Key);
        item.Value(builder);
        yield return builder.Create();
      }
    }

    private IEnumerable<MeasureConfig> GetMeasures()
    {
      foreach (var item in _measures)
      {
        MeasureBuilder builder = new MeasureBuilder().SetName(item.Key);
        item.Value(builder);
        yield return builder.Create();
      }
    }

    private IEnumerable<MetricConfig> GetMetrics()
    {
      foreach (var item in _metrics)
      {
        MetricBuilder builder = new MetricBuilder().SetName(item.Key);
        item.Value(builder);
        yield return builder.Create();
      }
    }

    #endregion private members
  }
}