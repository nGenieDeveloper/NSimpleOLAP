using NSimpleOLAP.Configuration;
using NSimpleOLAP.Data.Readers;
using NSimpleOLAP.Schema;
using System;
using System.Collections.Generic;

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
          T segment = (T)Convert.ChangeType(rowdata[item.Field], typeof(T));
          Dimension<T> dimension = _schema.Dimensions[item.Dimension];

          if (dimension.Members.ContainsKey(segment))
          {
            KeyValuePair<T, T> pair = new KeyValuePair<T, T>(dimension.ID, segment);
            retlist.Add(pair);
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