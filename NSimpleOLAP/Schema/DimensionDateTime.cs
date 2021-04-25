using NSimpleOLAP.Common;
using NSimpleOLAP.Configuration;
using System;
using System.Collections.Generic;
using NSimpleOLAP.Common.Utils;

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
      LevelDimensions = new List<DimensionDateTime<T>>();
      _members = new MemberDateTimeCollection<T>();
      Members = _members;
    }

    public DateTimeLevels DateLevel { get; private set; }

    public new DimensionType TypeOf { get { return DimensionType.Date; } }

    public new int LevelPosition { get { return _level; } }

    public new bool HasLevels { get { return LevelDimensions.Count > 0; } }

    public IList<DimensionDateTime<T>> LevelDimensions
    {
      get;
      private set;
    }

    public new MemberCollection<T> Members
    {
      get;
      private set;
    }

    public override void Process()
    {
      foreach (var item in PrePopulate())
      {
        this.Members.Add(new Member<T>()
        {
          ID = item.Item1,
          Name = item.Item2
        });
      }
    }

    
    private IEnumerable<Tuple<T, string>> PrePopulate()
    {
      var tempDate = DateTime.Now;

      switch (DateLevel)
      {
        case DateTimeLevels.YEAR:
          return new Tuple<T, string>[] { 
            new Tuple<T, string>(DateTimeMemberGenerator.TransformToDateId<T>(tempDate, DateLevel), 
            DateTimeMemberGenerator.GetLevelName(tempDate, DateLevel)) 
          };
        case DateTimeLevels.MONTH:
          return DateTimeMemberGenerator.GetAllMonthsInYear<T>(tempDate);
        case DateTimeLevels.MONTH_SOLO:
          return DateTimeMemberGenerator.GetAllMonthsInYear<T>();
        case DateTimeLevels.DAY:
          return DateTimeMemberGenerator.GetAllDays<T>();
        default:
          return new Tuple<T, string>[] { };
      }
    }


    // Change Members
  }
}