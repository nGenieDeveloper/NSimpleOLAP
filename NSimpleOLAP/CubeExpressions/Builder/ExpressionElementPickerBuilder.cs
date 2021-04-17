using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSimpleOLAP.CubeExpressions.Builder
{
  public class ExpressionElementPickerBuilder<T>
    where T : struct, IComparable
  {
    private T _measure;
    private List<KeyValuePair<T, T>[]> _tuples;

    public ExpressionElementPickerBuilder<T> Set(string measure)
    {

      return this;
    }

    internal ExpressionElementPickerBuilder<T> Set(T measure)
    {

      return this;
    }

    public ExpressionElementPickerBuilder<T> Set(string measure, params string[] tuples)
    {

      return this;
    }

    internal ExpressionElementPickerBuilder<T> Set(T measure, IEnumerable<KeyValuePair<T, T>[]> tuples)
    {

      return this;
    }
  }
}
