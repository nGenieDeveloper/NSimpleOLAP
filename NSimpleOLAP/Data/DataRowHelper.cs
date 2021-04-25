using NSimpleOLAP.Configuration;
using NSimpleOLAP.Data.Readers;
using NSimpleOLAP.Schema;
using System;
using System.Collections.Generic;
using NSimpleOLAP.Common.Utils;
using NSimpleOLAP.Common;

namespace NSimpleOLAP.Data
{
  /// <summary>
  /// Description of DataRowHelper.
  /// </summary>
  internal class DataRowHelper<T>
    where T : struct, IComparable
  {
    private DataSchema<T> _schema;
    private CubeSourceConfig _config;

    public DataRowHelper(DataSchema<T> schema, CubeSourceConfig config)
    {
      _schema = schema;
      _config = config;
    }

    public KeyValuePair<T, T>[] GetDimensions(AbsRowData rowdata)
    {
      List<KeyValuePair<T, T>> retlist = new List<KeyValuePair<T, T>>();

      foreach (SourceMappingsElement item in _config.Fields)
      {
        if (rowdata[item.Field] != null)
        {
          if (!string.IsNullOrEmpty(item.Dimension))
          {
            Dimension<T> dimension = _schema.Dimensions[item.Dimension];
            T segment = (T)Convert.ChangeType(rowdata[item.Field], typeof(T));

            if (dimension.Members.ContainsKey(segment))
            {
              KeyValuePair<T, T> pair = new KeyValuePair<T, T>(dimension.ID, segment);
              retlist.Add(pair);
            }
          }
          else if (item.Labels?.Length > 0)
          {
            for (var i = 0; i < item.Labels.Length; i++)
            {
              var dimension = _schema.Dimensions[item.Labels[i]];

              if (dimension.TypeOf == DimensionType.Date)
              {
                if (rowdata[item.Field] != null)
                {
                  var value = ((DateTime?)rowdata[item.Field]).Value;
                  T segment = DateTimeMemberGenerator.TransformToDateId<T>(value, ((DimensionDateTime<T>)dimension).DateLevel);

                  KeyValuePair<T, T> pair = new KeyValuePair<T, T>(dimension.ID, segment);
                  retlist.Add(pair);

                  if (!dimension.Members.ContainsKey(segment))
                  {
                    dimension.Members.Add(new Member<T> 
                    { 
                      ID = segment, 
                      Name = DateTimeMemberGenerator.GetLevelName(value,((DimensionDateTime<T>)dimension).DateLevel) 
                    });
                  }
                }
              }
            }
          }
        }
      }

      retlist.Sort(ComparePairs);

      return retlist.ToArray();
    }

    public MeasureValuesCollection<T> GetMeasureData(AbsRowData rowdata)
    {
      MeasureValuesCollection<T> vars = new MeasureValuesCollection<T>();

      foreach (var item in _schema.Measures)
      {
        if (rowdata[item.Config.ValueFieldName] != null)
          vars.Add(item.ID, rowdata[item.Config.ValueFieldName]);
      }

      return vars;
    }

    private int ComparePairs(KeyValuePair<T, T> a, KeyValuePair<T, T> b)
    {
      return a.Key.CompareTo(b.Key);
    }
  }
}