using System;
using System.Collections.Generic;
using NSimpleOLAP;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Storage.Interfaces;
using NSimpleOLAP.Query.Interfaces;

namespace NSimpleOLAP.Query.Builder
{
	/// <summary>
	/// Description of QueryBuilder.
	/// </summary>
	public abstract class QueryBuilder<T>
		where T:struct, IComparable
	{
		protected WhereBuilder<T> _wherebuilder;
		protected Cube<T> _innerCube;
		protected DimensionReferenceTranslator<T> _dimTranslator;
		protected MeasureReferenceTranslator<T> _measTranslator;
		protected AxisBuilder<T> _axisBuilder;
		protected List<T> _measureKeys;
		
		protected void Init()
		{
			_dimTranslator = new DimensionReferenceTranslator<T>(_innerCube.Schema);
			_measTranslator = new MeasureReferenceTranslator<T>(_innerCube.Schema);
			_wherebuilder = new WhereBuilder<T>(_innerCube.Schema, _dimTranslator, _measTranslator);
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
		public QueryBuilder<T> OnRows(params KeyValuePair<T,T>[] tuples)
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
		public QueryBuilder<T> OnColumns(params KeyValuePair<T,T>[] tuples)
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
			return new QueryImplementation(null, _wherebuilder.Build());
		}

		#endregion

		#region

		private class QueryImplementation : Query<T>
		{
			public QueryImplementation(Axis<T> axis, IPredicate<T> predicateTree)
			{
				this.axis = axis;
				this.predicates = predicateTree;
			}
		}

		internal class QueryBuilderImpl: QueryBuilder<T>
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
