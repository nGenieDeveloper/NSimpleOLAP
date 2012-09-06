using System;
using System.Collections.Generic;
using NSimpleOLAP;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Storage.Interfaces;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of WhereBuilder.
	/// </summary>
	public class WhereBuilder<T>
		where T: struct, IComparable
	{
		private DataSchema<T> _schema;
		private List<IPredicateBuilder<T>> _predicates;
		
		public WhereBuilder(DataSchema<T> schema)
		{
			_schema = schema;
			_predicates = new List<IPredicateBuilder<T>>();
			PredicateFactory = new PredicateFactory<T>(schema);
		}
		
		public PredicateFactory<T> PredicateFactory
		{
			get;
			private set;
		}
		
		#region fluent interface
		
		public WhereBuilder<T> AddPredicate(IPredicateBuilder<T> builder)
		{
			_predicates.Add(builder);
		
			return this;
		}
		
		public WhereBuilder<T> And()
		{
		
			return this;
		}
		
		
		public WhereBuilder<T> Or()
		{
		
			return this;
		}
		
		public WhereBuilder<T> Not()
		{
		
			return this;
		}
		
		
		
		#endregion
	}
}
