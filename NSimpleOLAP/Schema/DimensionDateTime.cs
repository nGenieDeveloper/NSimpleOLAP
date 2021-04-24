using NSimpleOLAP.Common;
using NSimpleOLAP.Configuration;
using System;
using System.Collections.Generic;

namespace NSimpleOLAP.Schema
{
  public class DimensionDateTime<T> : Dimension<T>
    where T : struct, IComparable
  {
    private int _level;
    private MemberDateTimeCollection<T> _members;

    public DimensionDateTime(DimensionConfig dimconfig, DateTimeLevels level, int levelIndex)
    {
      this.Config = dimconfig;
      DateLevel = level;
      _level = levelIndex;
      DateDimensions = new List<DimensionDateTime<T>>();
      _members = new MemberDateTimeCollection<T>();
      Members = _members;
    }

    public DateTimeLevels DateLevel { get; private set; }

    public new DimensionType TypeOf { get { return DimensionType.Date; } }

    public new int LevelPosition { get { return _level; } }

    public new bool HasLevels { get { return DateDimensions.Count > 0; } }

    public IList<DimensionDateTime<T>> DateDimensions
    {
      get;
      private set;
    }

    public new MemberCollection<T> Members
    {
      get;
      private set;
    }



    // Change Members
  }
}