using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSimpleOLAP.CubeExpressions
{
  internal static class CubeExpressionHelperFunctions
  {

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
