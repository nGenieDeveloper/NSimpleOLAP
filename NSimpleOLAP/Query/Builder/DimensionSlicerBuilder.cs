using NSimpleOLAP.Common;
using NSimpleOLAP.Query.Interfaces;
using NSimpleOLAP.Query.Predicates;
using NSimpleOLAP.Schema;
using System;
using System.Collections.Generic;

namespace NSimpleOLAP.Query.Builder
{
  /// <summary>
  /// Description of DimensionSlicerBuilder.
  /// </summary>
  public class DimensionSlicerBuilder<T> : IPredicateBuilder<T>
    where T : struct, IComparable
  {
    private DataSchema<T> _schema;
    private T _dimension;
    private List<T> _members;
    private LogicalOperators _operator;
    private DimensionReferenceTranslator<T> _translator;

    public DimensionSlicerBuilder(DataSchema<T> schema, DimensionReferenceTranslator<T> translator)
    {
      _schema = schema;
      _members = new List<T>();
      _translator = translator;
    }

    #region fluent interface

    internal DimensionSlicerBuilder<T> SetDim(string dimension)
    {
      _dimension = _translator.GetDimension(dimension);

      return this;
    }

    internal DimensionSlicerBuilder<T> SetDim(T dimensionKey)
    {
      _dimension = dimensionKey;

      return this;
    }

    internal DimensionSlicerBuilder<T> SetOperationSegments(LogicalOperators loperator, params string[] members)
    {
      _operator = loperator;

      foreach (var item in members)
        _members.Add(_translator.GetDimensionMember(_dimension, item));

      return this;
    }

    internal DimensionSlicerBuilder<T> SetOperationSegments(LogicalOperators loperator, params T[] memberKeys)
    {
      _operator = loperator;
      _members.AddRange(memberKeys);

      return this;
    }

    #endregion fluent interface

    public IPredicate<T> Build()
    {
      return new SliceByDimensionMembers<T>(_dimension, _operator, _members.ToArray());
    }
  }
}