using System;
using System.Collections.Generic;
using System.Globalization;

namespace NSimpleOLAP.Common.Utils
{
  internal static class DateTimeMemberGenerator
  {
    public static IEnumerable<T> GetLevelIds<T>(this DateTime date, DateTimeLevels[] levels)
      where T : struct, IComparable
    {
      foreach (var level in levels)
        yield return TransformToDateId<T>(date, level);
    }

    public static IEnumerable<string> GetLevelNames(this DateTime date, DateTimeLevels[] levels)
    {
      foreach (var level in levels)
        yield return GetLevelName(date, level);
    }

    public static T TransformToDateId<T>(DateTime date, DateTimeLevels level)
      where T : struct, IComparable
    {
      switch (level)
      {
        case DateTimeLevels.DAY:
          return TransformToDay<T>(date);

        case DateTimeLevels.DATE:
          return TransformToDate<T>(date);

        case DateTimeLevels.MONTH_WITH_YEAR:
          return TransformToMonth<T>(date);

        case DateTimeLevels.YEAR:
          return TransformToYear<T>(date);

        case DateTimeLevels.QUARTER:
          return TransformToQuarter<T>(date);

        case DateTimeLevels.WEEK:
          return TransformToWeek<T>(date);

        case DateTimeLevels.MONTH:
          return TransformToMonthOfYear<T>(date);

        default:
          throw new Exception("Type not supported.");
      }
    }

    public static IEnumerable<Tuple<T,string>> GetAllMonthsInYear<T>()
      where T : struct, IComparable
    {
      for (var i = 1; i <= 12; i++)
      {
        var tempDate = new DateTime(2000, i, 1);

        yield return new Tuple<T, string>((T)Convert.ChangeType(i, typeof(T)), tempDate.ToString("MMMM"));
      }
    }

    public static IEnumerable<Tuple<T, string>> GetAllMonthsInYear<T>(DateTime value)
      where T : struct, IComparable
    {
      for (var i = 1; i <= 12; i++)
      {
        var tempDate = new DateTime(value.Year, i, 1);

        yield return new Tuple<T, string>(TransformToDateId<T>(tempDate, DateTimeLevels.MONTH_WITH_YEAR), 
          GetLevelName(tempDate, DateTimeLevels.MONTH_WITH_YEAR));
      }
    }

    public static IEnumerable<Tuple<T, string>> GetAllDays<T>()
      where T : struct, IComparable
    {
      for (var i = 1; i <= 31; i++)
      {
        yield return new Tuple<T, string>((T)Convert.ChangeType(i, typeof(T)), i.ToString());
      }
    }

    public static IEnumerable<Tuple<T, string>> GetAllWeeksInYear<T>()
      where T : struct, IComparable
    {
      for (var i = 1; i <= 52; i++)
      {
        yield return new Tuple<T, string>((T)Convert.ChangeType(i, typeof(T)), i.ToString());
      }
    }

    public static T TransformToDate<T>(DateTime date)
      where T : struct, IComparable
    {
      var value = date.Year * 10000
        + date.Month * 100
        + date.Day;

      return value.SetOutput<T>();
    }

    public static T TransformToDayOfMonth<T>(DateTime date)
      where T : struct, IComparable
    {
      var value = date.Day;

      return value.SetOutput<T>();
    }

    public static string GetLevelName(DateTime date, DateTimeLevels level)
    {
      switch (level)
      {
        case DateTimeLevels.DAY:
          return date.ToString("dd");
        case DateTimeLevels.DATE:
          return date.ToString("yyyy-MM-dd");
        case DateTimeLevels.YEAR:
          return date.ToString("yyyy");
        case DateTimeLevels.MONTH:
          return date.ToString("MMMM");
        case DateTimeLevels.MONTH_WITH_YEAR:
          return date.ToString("yyyy MMMM");
        case DateTimeLevels.WEEK:
          return string.Format("{0} Week {1}", date.ToString("yyyy"), DateToWeek(date));
        case DateTimeLevels.QUARTER:
          return string.Format("{0} Q{1}", date.ToString("yyyy"), DateToQuarter(date));
        default:
          throw new Exception("Level not supported.");
      }
    }

    public static int DateToWeek(DateTime date)
    {
      var weekNumber = CultureInfo
        .CurrentCulture.Calendar
        .GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

      return weekNumber;
    }

    public static T TransformToDay<T>(DateTime date)
      where T : struct, IComparable
    {
      var value = date.Day;

      return value.SetOutput<T>();
    }

    public static T TransformToWeek<T>(DateTime date)
      where T : struct, IComparable
    {
      var value = date.Year * 100
        + DateToWeek(date);

      return value.SetOutput<T>();
    }

    public static T TransformToMonth<T>(DateTime date)
      where T : struct, IComparable
    {
      var value = date.Year * 100
        + date.Month;

      return value.SetOutput<T>();
    }

    public static T TransformToMonthOfYear<T>(DateTime date)
      where T : struct, IComparable
    {
      var value = date.Month;

      return value.SetOutput<T>();
    }

    public static int DateToQuarter(DateTime date)
    {
      return (date.Month + 2) / 3;
    }

    public static T TransformToQuarter<T>(DateTime date)
      where T : struct, IComparable
    {
      var value = date.Year * 10
        + DateToQuarter(date);

      // (date.AddMonths(-3).Month + 2)/3;

      return value.SetOutput<T>();
    }

    public static T TransformToQuarterOfYear<T>(DateTime date)
      where T : struct, IComparable
    {
      // (date.AddMonths(-3).Month + 2)/3;

      return DateToQuarter(date).SetOutput<T>();
    }

    public static T TransformToYear<T>(DateTime date)
      where T : struct, IComparable
    {
      var value = date.Year;

      return value.SetOutput<T>();
    }

    public static T SetOutput<T>(this int value)
    {
      var ovalue = default(T);

      switch (ovalue)
      {
        case int i:
          return (T)(object)value;

        case long i:
          return (T)(object)Convert.ToInt64(value);

        case uint i:
          return (T)(object)Convert.ToUInt32(value);

        case ulong i:
          return (T)(object)Convert.ToUInt64(value);

        default:
          throw new Exception("Type not supported.");
      }
    }
  }
}