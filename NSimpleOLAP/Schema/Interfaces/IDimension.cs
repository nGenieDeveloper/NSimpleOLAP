using NSimpleOLAP.Configuration;
using NSimpleOLAP.Data.Interfaces;
using System;

namespace NSimpleOLAP.Schema.Interfaces
{
  public interface IDimension<T> : IDataItem<T>, IDisposable
      where T : struct, IComparable
  {
    MemberCollection<T> Members { get; }
    DimensionConfig Config { get; set; }
    IDataSource DataSource { get; }

    bool HasLevels { get; }

    int LevelPosition { get; }
  }
}