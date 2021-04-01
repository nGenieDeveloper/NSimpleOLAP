using NSimpleOLAP.Query.Builder;
using NSimpleOLAP.Query.Interfaces;
using NSimpleOLAP.Schema;
using System;

namespace NSimpleOLAP.Query.Predicates
{
  /// <summary>
  /// Description of PredicateFactory.
  /// </summary>
  public class PredicateBuilderFactory<T>
    where T : struct, IComparable
  {
    private DataSchema<T> _schema;
    private DimensionReferenceTranslator<T> _dimTranslator;
    private MeasureReferenceTranslator<T> _mesTranslator;

    public PredicateBuilderFactory(DataSchema<T> schema,
                            DimensionReferenceTranslator<T> dimTranslator,
                            MeasureReferenceTranslator<T> mesTranslator)
    {
      _schema = schema;
      _dimTranslator = dimTranslator;
      _mesTranslator = mesTranslator;
    }

    #region Create Predicates

    public IPredicateBuilder<T> CreateDimensionSlicer()
    {
      return new DimensionSlicerBuilder<T>(_schema, _dimTranslator);
    }

    public IPredicateBuilder<T> CreateMeasureSlicer()
    {
      return new MeasureSlicerBuilder<T>(_schema, _mesTranslator);
    }

    internal IPredicateBuilder<T> CreateAndPredicate()
    {
      return new AndPredicateBuilder<T>(this);
    }

    internal IPredicateBuilder<T> CreateOrPredicate()
    {
      return new OrPredicateBuilder<T>(this);
    }

    internal IPredicateBuilder<T> CreateBlockPredicate()
    {
      return new BlockPredicateBuilder<T>(this);
    }

    internal IPredicateBuilder<T> CreateNotPredicate()
    {
      return new NotPredicateBuilder<T>(this);
    }

    #endregion Create Predicates
  }
}