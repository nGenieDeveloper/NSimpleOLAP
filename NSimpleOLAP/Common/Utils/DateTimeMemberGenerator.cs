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
        case DateTimeLevels.DATE:
          return TransformToDate<T>(date);

        case DateTimeLevels.MONTH:
          return TransformToMonth<T>(date);

        case DateTimeLevels.YEAR:
          return TransformToYear<T>(date);

        case DateTimeLevels.QUARTER:
          return TransformToQuarter<T>(date);

        case DateTimeLevels.WEEK:
          return TransformToWeek<T>(date);

        case DateTimeLevels.MONTH_SOLO:
          return TransformToMonthOfYear<T>(date);

        default:
          throw new Exception("Type not supported.");
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
        case DateTimeLevels.DATE:
          return date.ToString("yyyy-MM-dd");
        case DateTimeLevels.YEAR:
          return date.ToString("yyyy");
        case DateTimeLevels.MONTH_SOLO:
          return date.ToString("MMMM");
        case DateTimeLevels.MONTH:
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