using NSimpleOLAP.Query.Interfaces;
using NSimpleOLAP.Query.Predicates;
using NSimpleOLAP.Schema;
using System;

namespace NSimpleOLAP.Query.Builder
{
  /// <summary>
  /// Description of WhereBuilder.
  /// </summary>
  public class WhereBuilder<T>
    where T : struct, IComparable
  {
    private DataSchema<T> _schema;
    private BlockPredicateBuilder<T> _rootBlock;
    private IPredicateBuilder<T> _currentBlock;

    public WhereBuilder(DataSchema<T> schema,
                        DimensionReferenceTranslator<T> dimTranslator,
                        MeasureReferenceTranslator<T> mesTranslator)
    {
      _schema = schema;
      BuilderFactory = new PredicateBuilderFactory<T>(schema, dimTranslator, mesTranslator);
      _rootBlock = new BlockPredicateBuilder<T>(BuilderFactory);
      _currentBlock = _rootBlock;
    }

    public PredicateBuilderFactory<T> BuilderFactory
    {
      get;
      private set;
    }

    #region fluent interface

    public WhereBuilder<T> Define(Func<BlockPredicateBuilder<T>, IPredicateBuilder<T>> blockBuilder)
    {
      _currentBlock = blockBuilder(_rootBlock);

      return this;
    }

    public IPredicate<T> Build()
    {
      return _rootBlock.Build();
    }

    #endregion fluent interface
  }
}