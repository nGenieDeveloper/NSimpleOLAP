using NSimpleOLAP.Common.Utils;
using System;
using System.Collections.Generic;

namespace NSimpleOLAP.CubeExpressions.Builder
{
  public class ExpressionElementPickerBuilder<T>
    where T : struct, IComparable
  {
    private T _measure;
    private List<KeyValuePair<T, T>[]> _tuples;
    private DimensionReferenceTranslator<T> _dimTranslator;
    private MeasureReferenceTranslator<T> _measTranslator;

    public ExpressionElementPickerBuilder(DimensionReferenceTranslator<T> dimTranslator, MeasureReferenceTranslator<T> measTranslator)
    {
      _dimTranslator = dimTranslator;
      _measTranslator = measTranslator;
    }

    internal T Measure
    {
      get
      {
        return _measure;
      }
    }

    internal List<KeyValuePair<T, T>[]> Tuples
    {
      get
      {
        return _tuples;
      }
    }

    public ExpressionElementPickerBuilder<T> Set(string measure)
    {
      _measure = _measTranslator.Translate(measure);

      return this;
    }

    internal ExpressionElementPickerBuilder<T> Set(T measure)
    {
      _measure = measure;

      return this;
    }

    public ExpressionElementPickerBuilder<T> Set(string measure, params string[] tuples)
    {
      Set(measure);

      _tuples = new List<KeyValuePair<T, T>[]>();

      foreach (var item in tuples)
      {
        _tuples.Add(_dimTranslator.Translate(item));
      }

      return this;
    }

    internal ExpressionElementPickerBuilder<T> Set(T measure, IEnumerable<KeyValuePair<T, T>[]> tuples)
    {
      Set(measure);

      _tuples = new List<KeyValuePair<T, T>[]>();

      _tuples.AddRange(tuples);

      return this;
    }

    internal Tuple<T, List<KeyValuePair<T, T>[]>> Create()
    {
      return new Tuple<T, List<KeyValuePair<T, T>[]>>(Measure, Tuples);
    }
  }
}