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

    public WhereBuilder<T> Where(Func<BlockPredicateBuilder<T>, IPredicateBuilder<T>> blockBuilder)
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

  internal class teste
  {
    private void dosomeExample()
    {
      var wherebuil = new WhereBuilder<int>(null, null, null);

      wherebuil.Where(x => x.And(b => b.Measure("")
      , b => b.Measure("")
      , b => b.Not(n => n.And(a => a.Dimension("d"), a => a.Dimension("i")))));
    }
  }
}