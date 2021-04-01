using NSimpleOLAP.Common;
using System;

namespace NSimpleOLAP.Query.Builder
{
  public static class PredicateBuilderExtensions
  {
    public static MeasureSlicerBuilder<T> Equals<T>(this MeasureSlicerBuilder<T> builder, object value)
      where T : struct, IComparable
    {
      return builder.SetOperationValuePair(LogicalOperators.EQUALS, value);
    }

    public static MeasureSlicerBuilder<T> NotEquals<T>(this MeasureSlicerBuilder<T> builder, object value)
      where T : struct, IComparable
    {
      return builder.SetOperationValuePair(LogicalOperators.NOTEQUALS, value);
    }

    public static MeasureSlicerBuilder<T> GreaterThan<T>(this MeasureSlicerBuilder<T> builder, object value)
      where T : struct, IComparable
    {
      return builder.SetOperationValuePair(LogicalOperators.GREATERTHAN, value);
    }

    public static MeasureSlicerBuilder<T> GreaterOrEquals<T>(this MeasureSlicerBuilder<T> builder, object value)
      where T : struct, IComparable
    {
      return builder.SetOperationValuePair(LogicalOperators.GREATEROREQUALS, value);
    }

    public static MeasureSlicerBuilder<T> LowerThan<T>(this MeasureSlicerBuilder<T> builder, object value)
      where T : struct, IComparable
    {
      return builder.SetOperationValuePair(LogicalOperators.LOWERTHAN, value);
    }

    public static MeasureSlicerBuilder<T> LowerOrEquals<T>(this MeasureSlicerBuilder<T> builder, object value)
      where T : struct, IComparable
    {
      return builder.SetOperationValuePair(LogicalOperators.LOWEROREQUALS, value);
    }

    public static DimensionSlicerBuilder<T> Equals<T>(this DimensionSlicerBuilder<T> builder, string member)
      where T : struct, IComparable
    {
      return builder.SetOperationSegments(LogicalOperators.EQUALS, member);
    }

    public static DimensionSlicerBuilder<T> NotEquals<T>(this DimensionSlicerBuilder<T> builder, string member)
      where T : struct, IComparable
    {
      return builder.SetOperationSegments(LogicalOperators.NOTEQUALS, member);
    }

    public static DimensionSlicerBuilder<T> In<T>(this DimensionSlicerBuilder<T> builder, params string[] members)
      where T : struct, IComparable
    {
      return builder.SetOperationSegments(LogicalOperators.IN, members);
    }
  }
}