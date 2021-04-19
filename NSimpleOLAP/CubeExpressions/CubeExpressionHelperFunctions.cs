using System;

namespace NSimpleOLAP.CubeExpressions
{
  internal static class CubeExpressionHelperFunctions
  {
    internal static ValueType Sum(this ValueType x, ValueType y)
    {
      switch (x)
      {
        case int i when y is int:
          return ((int)x).Sum((int)x);

        case double i when y is int:
          return ((double)x).Sum((int)x);

        case double i when y is double:
          return ((double)x).Sum((double)x);

        case decimal i when y is int:
          return ((decimal)x).Sum((int)x);

        case decimal i when y is decimal:
          return ((decimal)x).Sum((decimal)x);

        case float i when y is int:
          return ((float)x).Sum((int)x);

        case float i when y is float:
          return ((float)x).Sum((float)x);

        default:
          throw new Exception("Type is not supported.");
      }
    }

    internal static ValueType Subtraction(this ValueType x, ValueType y)
    {
      switch (x)
      {
        case int i when y is int:
          return ((int)x).Subtraction((int)x);

        case double i when y is int:
          return ((double)x).Subtraction((int)x);

        case double i when y is double:
          return ((double)x).Subtraction((double)x);

        case decimal i when y is int:
          return ((decimal)x).Subtraction((int)x);

        case decimal i when y is decimal:
          return ((decimal)x).Subtraction((decimal)x);

        case float i when y is int:
          return ((float)x).Subtraction((int)x);

        case float i when y is float:
          return ((float)x).Subtraction((float)x);

        default:
          throw new Exception("Type is not supported.");
      }
    }

    internal static ValueType Multiplication(this ValueType x, ValueType y)
    {
      switch (x)
      {
        case int i when y is int:
          return ((int)x).Multiplication((int)x);

        case double i when y is int:
          return ((double)x).Multiplication((int)x);

        case double i when y is double:
          return ((double)x).Multiplication((double)x);

        case decimal i when y is int:
          return ((decimal)x).Multiplication((int)x);

        case decimal i when y is decimal:
          return ((decimal)x).Multiplication((decimal)x);

        case float i when y is int:
          return ((float)x).Multiplication((int)x);

        case float i when y is float:
          return ((float)x).Multiplication((float)x);

        default:
          throw new Exception("Type is not supported.");
      }
    }

    internal static ValueType Division(this ValueType x, ValueType y)
    {
      switch (x)
      {
        case int i when y is int:
          return ((int)x).Division((int)x);

        case double i when y is int:
          return ((double)x).Division((int)x);

        case double i when y is double:
          return ((double)x).Division((double)x);

        case decimal i when y is int:
          return ((decimal)x).Division((int)x);

        case decimal i when y is decimal:
          return ((decimal)x).Division((decimal)x);

        case float i when y is int:
          return ((float)x).Division((int)x);

        case float i when y is float:
          return ((float)x).Division((float)x);

        default:
          throw new Exception("Type is not supported.");
      }
    }

    internal static ValueType Minimum(this ValueType x, ValueType y)
    {
      switch (x)
      {
        case int i:
          return ((int)x).Min(x);

        case double i:
          return ((double)x).Min(x);

        case decimal i:
          return ((decimal)x).Min(x);

        case float i:
          return ((float)x).Min(x);

        default:
          throw new Exception("Type is not supported.");
      }
    }

    internal static ValueType Maximum(this ValueType x, ValueType y)
    {
      switch (x)
      {
        case int i:
          return ((int)x).Max(x);

        case double i:
          return ((double)x).Max(x);

        case decimal i:
          return ((decimal)x).Max(x);

        case float i:
          return ((float)x).Max(x);

        default:
          throw new Exception("Type is not supported.");
      }
    }

    internal static bool IsZero(this ValueType x)
    {
      switch (x)
      {
        case int i:
          return (int)x == 0;

        case double i:
          return (double)x == 0.00d;

        case decimal i:
          return (decimal)x == 0.00m;

        case float i:
          return (float)x == 0.00f;

        default:
          return false;
      }
    }

    private static int Max(this int value, object value2)
    {
      return (value > (int)value2) ? value : (int)value2;
    }

    private static double Max(this double value, object value2)
    {
      return (value > (double)value2) ? value : (double)value2;
    }

    private static decimal Max(this decimal value, object value2)
    {
      return (value > (decimal)value2) ? value : (decimal)value2;
    }

    private static float Max(this float value, object value2)
    {
      return (value > (float)value2) ? value : (float)value2;
    }

    private static int Min(this int value, object value2)
    {
      return (value < (int)value2) ? value : (int)value2;
    }

    private static double Min(this double value, object value2)
    {
      return (value < (double)value2) ? value : (double)value2;
    }

    private static decimal Min(this decimal value, object value2)
    {
      return (value < (decimal)value2) ? value : (decimal)value2;
    }

    private static float Min(this float value, object value2)
    {
      return (value < (float)value2) ? value : (float)value2;
    }

    private static float? Division(this float value, int value2)
    {
      return value2 > 0 ? (float?)(value / value2) : null;
    }

    private static double? Division(this double value, int value2)
    {
      return value2 > 0 ? (double?)(value / value2) : null;
    }

    private static decimal? Division(this decimal value, int value2)
    {
      return value2 > 0 ? (decimal?)(value / value2) : null;
    }

    private static double? Division(this int value, int value2)
    {
      return value2 > 0 ? (double?)(value / value2) : null;
    }

    private static double? Division(this double value, double value2)
    {
      return value2 > 0 ? (double?)(value / value2) : null;
    }

    private static decimal? Division(this decimal value, decimal value2)
    {
      return value2 > 0 ? (decimal?)(value / value2) : null;
    }

    private static float Multiplication(this float value, int value2)
    {
      return value * value2;
    }

    private static double Multiplication(this double value, int value2)
    {
      return value * value2;
    }

    private static decimal Multiplication(this decimal value, int value2)
    {
      return value * value2;
    }

    private static int Multiplication(this int value, int value2)
    {
      return value * value2;
    }

    private static double Multiplication(this double value, double value2)
    {
      return value * value2;
    }

    private static decimal Multiplication(this decimal value, decimal value2)
    {
      return value * value2;
    }

    private static float Subtraction(this float value, int value2)
    {
      return value - value2;
    }

    private static double Subtraction(this double value, int value2)
    {
      return value - value2;
    }

    private static decimal Subtraction(this decimal value, int value2)
    {
      return value - value2;
    }

    private static int Subtraction(this int value, int value2)
    {
      return value - value2;
    }

    private static double Subtraction(this double value, double value2)
    {
      return value - value2;
    }

    private static decimal Subtraction(this decimal value, decimal value2)
    {
      return value - value2;
    }

    private static float Sum(this float value, int value2)
    {
      return value + value2;
    }

    private static double Sum(this double value, int value2)
    {
      return value + value2;
    }

    private static decimal Sum(this decimal value, int value2)
    {
      return value + value2;
    }

    private static int Sum(this int value, int value2)
    {
      return value + value2;
    }

    private static double Sum(this double value, double value2)
    {
      return value + value2;
    }

    private static decimal Sum(this decimal value, decimal value2)
    {
      return value + value2;
    }
  }
}