using NSimpleOLAP.Common;
using NSimpleOLAP.Common.Interfaces;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Data.Interfaces;
using NSimpleOLAP.Data.Readers;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Storage.Interfaces;
using System;

namespace NSimpleOLAP.Schema
{
  /// <summary>
  /// Description of Dimension.
  /// </summary>
  public class Dimension<T> : IDimension<T>, IProcess
    where T : struct, IComparable
  {
    public Dimension()
    {
    }

    public Dimension(DimensionConfig dimconfig, IDataSource datasource)
    {
      this.Config = dimconfig;
      this.DataSource = datasource;
    }

    public string Name
    {
      get;
      set;
    }

    public T ID
    {
      get;
      set;
    }

    public ItemType ItemType
    {
      get { return ItemType.Dimension; }
    }

    public virtual DimensionType TypeOf { get { return DimensionType.Numeric; } }

    public MemberCollection<T> Members
    {
      get;
      private set;
    }

    public DimensionConfig Config
    {
      get;
      set;
    }

    public IDataSource DataSource
    {
      get;
      private set;
    }

    public virtual bool HasLevels { get { return false; } }

    public virtual int LevelPosition { get { return 0; } }

    public virtual void Process()
    {
      using (AbsReader reader = this.DataSource.GetReader())
      {
        while (reader.Next())
        {
          this.Members.Add(new Member<T>()
          {
            ID = (T)reader.Current[this.Config.ValueFieldName],
            Name = reader.Current[this.Config.DesFieldName].ToString()
          });
        }
      }
    }

    public virtual void Refresh()
    {
      throw new NotImplementedException();
    }

    #region

    internal void SetMembersStorage(IMemberStorage<T, Member<T>> storage)
    {
      this.Members = new MemberCollection<T>(storage);
    }

    #endregion

    #region IDisposable

    public void Dispose()
    {
      this.Members.Dispose();
    }

    #endregion
  }
}