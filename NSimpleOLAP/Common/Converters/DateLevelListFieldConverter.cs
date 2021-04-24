using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace NSimpleOLAP.Common.Converters
{
  public class DateLevelListFieldConverter : ConfigurationConverterBase
  {
    public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo culture, object value)
    {
      if (value == null)
        return new List<DateTimeLevels>();

      var str = (string)value;
      var list = new List<DateTimeLevels>();

      foreach (var item in str.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
      {
        DateTimeLevels result;

        if (!Enum.TryParse(item.ToUpper().Trim(), true, out result))
        {
          throw new InvalidOperationException("Invalid DateTime Level value");
        }

        list.Add(result);
      }

      return list;
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
      if (value is List<DateTimeLevels>)
      {
        var arr = (List<DateTimeLevels>)value;

        return string.Join(",", arr.Select(x => x.ToString()));
      }

      return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}