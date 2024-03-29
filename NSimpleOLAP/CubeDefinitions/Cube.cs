﻿using NSimpleOLAP.Configuration;
using NSimpleOLAP.Data;
using NSimpleOLAP.Data.Readers;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Storage;
using NSimpleOLAP.Storage.Interfaces;
using System;

namespace NSimpleOLAP
{
  /// <summary>
  /// Description of Cube.
  /// </summary>
  public class Cube<T> : ICube<T, Cell<T>>
    where T : struct, IComparable
  {
    private DataRowHelper<T> _rowHelper;

    public Cube()
    {
      this.Config = DefaultCubeConfiguration.GetConfig();
      this.Init();
    }

    public Cube(CubeConfig config)
    {
      this.Config = config;
      this.Init();
    }

    #region props

    public T Key
    {
      get;
      set;
    }

    public string Name
    {
      get;
      set;
    }

    public string Source
    {
      get;
      set;
    }

    public INamespace<T> NameSpace
    {
      get;
      private set;
    }

    public DataSchema<T> Schema
    {
      get;
      private set;
    }

    public IStorage<T, Cell<T>> Storage
    {
      get;
      private set;
    }

    public ICellCollection<T, Cell<T>> Cells
    {
      get;
      private set;
    }

    public DataSourceCollection DataSources
    {
      get;
      private set;
    }

    public bool IsProcessing
    {
      get;
      private set;
    }

    public CubeConfig Config
    {
      get;
      internal set;
    }

    internal bool Initialized
    {
      get;
      private set;
    }

    #endregion props

    #region IDisposable implementations

    public void Dispose()
    {
      if (Schema != null)
      {
        this.Schema.Dispose();
        this.Storage.Dispose();
        this.NameSpace.Dispose();
      }

      this.DataSources = null;
    }

    #endregion IDisposable implementations

    public void Initialize()
    {
      this.Storage = StorageFactory<T, Cell<T>>.Create(this.Key, this.Config.Storage);
      this.NameSpace = Storage.NameSpace;
      this.DataSources = new DataSourceCollection(this.Config);
      this.Cells = new CellCollection<T>(this.Storage);
      this.Schema = new DataSchema<T>(
        this.Config,
        this.DataSources,
        this.Storage.Dimensions,
        this.Storage.Measures,
        this.Storage.Metrics);
      _rowHelper = new DataRowHelper<T>(this.Schema, this.Config.Source);

      Initialized = true;
    }

    #region IProcess implementation

    public void Process()
    {
      this.IsProcessing = true;
      this.Schema.Process();
      this.ProcessDataSource();
      this.IsProcessing = false;
    }

    public void Refresh()
    {
      throw new NotImplementedException();
    }

    #endregion IProcess implementation

    #region private methods

    private void Init()
    {
      this.Name = this.Config.Name;
      this.Source = this.Config.Source.Name;
      this.IsProcessing = false;
    }

    private void ProcessDataSource()
    {
      using (AbsReader reader = this.DataSources[this.Source].GetReader())
      {
        while (reader.Next())
        {
          this.Storage.AddRowData(_rowHelper.GetDimensions(reader.Current),
            _rowHelper.GetMeasureData(reader.Current));
        }
      }
    }

    #endregion private methods
  }
}