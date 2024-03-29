﻿using NSimpleOLAP.Common;
using System.Linq;
using System;

namespace NSimpleOLAP.Configuration.Fluent
{
  /// <summary>
  /// Description of DimensionBuilder.
  /// </summary>
  public class DimensionBuilder
  {
    private DimensionConfig _element;

    public DimensionBuilder()
    {
      _element = new DimensionConfig();
    }

    #region public methods

    public DimensionBuilder SetName(string name)
    {
      _element.Name = name;
      return this;
    }

    public DimensionBuilder DescField(string name)
    {
      _element.DesFieldName = name;
      Validate();
      return this;
    }

    public DimensionBuilder DescField(int index)
    {
      _element.ValueFieldIndex = index;
      Validate();
      return this;
    }

    public DimensionBuilder ValueField(string name)
    {
      _element.ValueFieldName = name;
      Validate();
      return this;
    }

    public DimensionBuilder ValueField(int index)
    {
      _element.ValueFieldIndex = index;
      Validate();
      return this;
    }

    public DimensionBuilder Source(string name)
    {
      _element.Source = name;
      Validate();
      return this;
    }

    public DimensionBuilder AllowsMembersWithSameName()
    {
      _element.AllowsMembersWithSameName = true;
      Validate();
      return this;
    }

    public DimensionBuilder SetToDateSource(params DateTimeLevels[] levels)
    {
      _element.DimensionType = DimensionType.Date;
      _element.Levels = levels.ToList();
      Validate();
      return this;
    }

    public DimensionBuilder SetLevelDimensions(params string[] levels)
    {
      if (_element.DimensionType != DimensionType.Date)
        _element.DimensionType = DimensionType.Levels;
      
      _element.LevelLabels = levels;
      Validate();
      return this;
    }

    private void Validate()
    {
      if (_element.DimensionType == DimensionType.Date)
      {
        if (!string.IsNullOrEmpty(_element.Source))
          throw new Exception("Date dimensions don't have a table source.");
        if (!string.IsNullOrEmpty(_element.DesFieldName) ||
            !string.IsNullOrEmpty(_element.ValueFieldName))
          throw new Exception("Date dimensions don't need a descriptor table mappings.");
        if (_element.LevelLabels?.Length > 0 && 
           _element.DimensionType == DimensionType.Date &&
            _element.Levels.Count != _element.LevelLabels.Length)
          throw new Exception("The number of Date Time Levels don\'t match the number of Level Labels.");
      }

      if (_element.DimensionType == DimensionType.Numeric)
      {
        if (_element.Levels?.Count > 0)
          throw new Exception("Non Date dimensions can\'t have date time levels.");
      }
    }

    public DimensionConfig Create()
    {
      return _element;
    }

    #endregion public methods
  }
}