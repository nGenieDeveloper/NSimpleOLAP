using System;
using System.Collections.Generic;

namespace NSimpleOLAP.Common
{
  internal static class ReservedAndSpecialValues
  {

    public const string ALL = "ALL";

    private static KeyValuePair<int, int> All_Int = new KeyValuePair<int, int>(0, 0);

    private static KeyValuePair<uint, uint> All_UInt = new KeyValuePair<uint, uint>(0, 0);

    private static KeyValuePair<long, long> All_Long = new KeyValuePair<long, long>(0, 0);

    private static KeyValuePair<UInt64, UInt64> All_ULong = new KeyValuePair<UInt64, UInt64>(0, 0);

    public static KeyValuePair<T, T> GetAllValue<T>()
    {
      var value = default(T);

      switch (value)
      {
        case int i:
          return (KeyValuePair<T, T>)(object)All_Int;

        case uint i:
          return (KeyValuePair<T, T>)(object)All_UInt;

        case long i:
          return (KeyValuePair<T, T>)(object)All_Long;

        case UInt64 i:
          return (KeyValuePair<T, T>)(object)All_ULong;

        default:
          throw new Exception("Type is not supported.");
      }
    }
  }
}