using System;
using System.Collections.Generic;
using NSimpleOLAP;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Storage.Interfaces;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of QueryBuilder.
	/// </summary>
	public abstract class QueryBuilder<T>
		where T:struct, IComparable
	{
		protected WhereBuilder<T> _wherebuilder;
		protected Cube<T> _innerCube;
		protected ReferenceTranslator<T> _translator;
		
		public void Init()
		{
			_wherebuilder = new WhereBuilder<T>();
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
				_translator.ToString(item);
			
			return this;
		}
		
		/// <summary>
		/// Assign tuples to appear in row
		/// </summary>
		/// <param name="tuples">Tuples are inserted in KeyValuePairs</param>
		/// <returns></returns>
		public QueryBuilder<T> OnRows(params KeyValuePair<T,T>[] tuples)
		{
			return this;
		}
		
		/// <summary>
		/// Assign tuples to appear in column
		/// </summary>
		/// <param name="tuples">Tuples are inserted in "dimension.member" form</param>
		/// <returns></returns>
		public QueryBuilder<T> OnColumns(params string[] tuples)
		{
			return this;
		}
		
		/// <summary>
		/// Assign tuples to appear in column
		/// </summary>
		/// <param name="tuples">Tuples are inserted in KeyValuePairs</param>
		/// <returns></returns>
		public QueryBuilder<T> OnColumns(params KeyValuePair<T,T>[] tuples)
		{
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
		
		#endregion
		
		#region
		
		internal class QueryBuilderImpl: QueryBuilder<T>
		{
			public QueryBuilderImpl(Cube<T> cube)
			{
				this._innerCube = cube;
			}
		}
		
		#endregion
	}
}
