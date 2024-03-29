﻿using NSimpleOLAP.Common;
using System.Linq;
using System;

namespace NSimpleOLAP.Configuration.Fluent
{
  /// <summary>
  /// Description of DataSourceBuilder.
  /// </summary>
  public class DataSourceBuilder
  {
    private DataSourceConfig _element;
    private Action<CSVConfigBuilder> _csvconfig;
    private Action<DBConfigBuilder> _dbconfig;
    private Action<DataTableConfigBuilder> _dtconfig;

    public DataSourceBuilder()
    {
      _element = new DataSourceConfig();
      _element.Fields = new FieldConfigCollection();
    }

    #region public methods

    public DataSourceBuilder SetName(string name)
    {
      _element.Name = name;
      return this;
    }

    public DataSourceBuilder SetCSVConfig(Action<CSVConfigBuilder> config)
    {
      _csvconfig = config;
      return this;
    }

    public DataSourceBuilder SetDBConfig(Action<DBConfigBuilder> config)
    {
      _dbconfig = config;
      return this;
    }

    public DataSourceBuilder SetDBConfig(Action<DataTableConfigBuilder> config)
    {
      _dtconfig = config;
      return this;
    }

    public DataSourceBuilder SetSourceType(DataSourceType sourcetype)
    {
      _element.SourceType = sourcetype;
      return this;
    }

    public DataSourceBuilder AddField(string name, Type type)
    {
      FieldConfig field = new FieldConfig() { Name = name, FieldType = type };
      _element.Fields.Add(field);
      return this;
    }

    public DataSourceBuilder AddDateField(string name, string format)
    {
      FieldConfig field = new FieldConfig() {
        Name = name,
        FieldType = typeof(DateTime),
        Format = format,
      };
      _element.Fields.Add(field);
      return this;
    }

    public DataSourceBuilder AddDateField(string name, int index, string format)
    {
      FieldConfig field = new FieldConfig() { 
        Name = name, 
        FieldType = typeof(DateTime),
        Index = index,
        Format = format,
      };
      _element.Fields.Add(field);
      return this;
    }

    public DataSourceBuilder AddField(string name, int index, Type type)
    {
      FieldConfig field = new FieldConfig() { Name = name, FieldType = type, Index = index };
      _element.Fields.Add(field);
      return this;
    }

    internal DataSourceConfig Create()
    {
      if (_element.SourceType == DataSourceType.CSV)
      {
        CSVConfigBuilder csvbuilder = new CSVConfigBuilder();
        _csvconfig(csvbuilder);
        _element.CSVConfig = csvbuilder.Create();
      }
      else if (_element.SourceType == DataSourceType.DataBase)
      {
        DBConfigBuilder dbbuilder = new DBConfigBuilder();
        _dbconfig(dbbuilder);
        _element.DBConfig = dbbuilder.Create();
      }
      else if (_element.SourceType == DataSourceType.DataSet)
      {
        DataTableConfigBuilder dtbuilder = new DataTableConfigBuilder();
        _dtconfig(dtbuilder);
        _element.DTableConfig = dtbuilder.Create();
      }

      return _element;
    }

    #endregion public methods
  }
}