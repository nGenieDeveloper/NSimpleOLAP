using NSimpleOLAP.Query.Interfaces;
using NSimpleOLAP.Query.Molap;
using System;
using System.Collections.Generic;
using NSimpleOLAP.Common.Utils;

namespace NSimpleOLAP.Query.Builder
{
  /// <summary>
  /// Description of QueryBuilder.
  /// </summary>
  public abstract class QueryBuilder<T>
    where T : struct, IComparable
  {
    protected WhereBuilder<T> _wherebuilder;
    protected Cube<T> _innerCube;
    protected DimensionReferenceTranslator<T> _dimTranslator;
    protected MeasureReferenceTranslator<T> _measTranslator;
    protected AxisBuilder<T> _axisBuilder;
    protected List<T> _measureKeys;

    protected void Init()
    {
      _measureKeys = new List<T>();
      _dimTranslator = new DimensionReferenceTranslator<T>(_innerCube.Schema);
      _measTranslator = new MeasureReferenceTranslator<T>(_innerCube.Schema);
      _wherebuilder = new WhereBuilder<T>(_innerCube.Schema, _dimTranslator, _measTranslator);
      _axisBuilder = new AxisBuilder<T>(_innerCube.Config.Storage.MolapConfig.HashType, _innerCube.Schema);
    }

    #region fluent interface

    /// <summary>
    /// Assign tuples to appear in row
    /// </summary>
    /// <param name="tuples">Tuples are inserted in "dimension.member" form</param>
    /// <returns></returns>
    public QueryBuilder<T> OnRows(params string[] tuples)
    {
      foreach (var item in tuples)
        _axisBuilder.AddRowTuples(_dimTranslator.Translate(item));

      return this;
    }

    /// <summary>
    /// Assign tuples to appear in row
    /// </summary>
    /// <param name="tuples">Tuples are inserted in KeyValuePairs</param>
    /// <returns></returns>
    public QueryBuilder<T> OnRows(params KeyValuePair<T, T>[] tuples)
    {
      _axisBuilder.AddRowTuples(tuples);

      return this;
    }

    /// <summary>
    /// Assign tuples to appear in column
    /// </summary>
    /// <param name="tuples">Tuples are inserted in "dimension.member" form</param>
    /// <returns></returns>
    public QueryBuilder<T> OnColumns(params string[] tuples)
    {
      foreach (var item in tuples)
        _axisBuilder.AddColumnTuples(_dimTranslator.Translate(item));

      return this;
    }

    /// <summary>
    /// Assign tuples to appear in column
    /// </summary>
    /// <param name="tuples">Tuples are inserted in KeyValuePairs</param>
    /// <returns></returns>
    public QueryBuilder<T> OnColumns(params KeyValuePair<T, T>[] tuples)
    {
      _axisBuilder.AddColumnTuples(tuples);

      return this;
    }

    /// <summary>
    /// Assign measures
    /// </summary>
    /// <param name="measures">Measure's names</param>
    /// <returns></returns>
    public QueryBuilder<T> AddMeasures(params string[] measures)
    {
      foreach (var item in measures)
        _measureKeys.Add(_measTranslator.Translate(item));

      return this;
    }

    /// <summary>
    /// Assign measures
    /// </summary>
    /// <param name="measureKeys">Measure's keys</param>
    /// <returns></returns>
    public QueryBuilder<T> AddMeasures(params T[] measureKeys)
    {
      _measureKeys.AddRange(measureKeys);

      return this;
    }

    /// <summary>
    /// construct where cause
    /// </summary>
    /// <param name="whereBuild"></param>
    /// <returns></returns>
    public QueryBuilder<T> Where(Action<WhereBuilder<T>> whereBuild)
    {
      whereBuild(_wherebuilder);

      return this;
    }

    public Query<T> Create()
    {
      return new QueryImplementation(_innerCube, _axisBuilder.Build(), _measureKeys, _wherebuilder.Build());
    }

    #endregion fluent interface

    #region

    private class QueryImplementation : Query<T>
    {
      public QueryImplementation(Cube<T> cube, Axis<T> axis, List<T> measures, IPredicate<T> predicateTree)
      {
        this.cube = cube;
        this.axis = axis;
        this.measures = measures;
        this.predicates = predicateTree;
        this.queryOrchestrator = new MolapQueryOrchestrator<T>(this.cube);
      }
    }

    internal class QueryBuilderImpl : QueryBuilder<T>
    {
      public QueryBuilderImpl(Cube<T> cube)
      {
        this._innerCube = cube;
        this.Init();
      }
    }

    #endregion
  }
}