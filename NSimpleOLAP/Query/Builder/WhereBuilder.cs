using System;
using System.Collections.Generic;
using NSimpleOLAP;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Storage.Interfaces;
using NSimpleOLAP.Query.Interfaces;
using NSimpleOLAP.Query.Predicates;


namespace NSimpleOLAP.Query.Builder
{
	/// <summary>
	/// Description of WhereBuilder.
	/// </summary>
	public class WhereBuilder<T>
		where T: struct, IComparable
	{
		private DataSchema<T> _schema;
		private BlockPredicateBuilder<T> _rootBlock;
		private IPredicateBuilder<T> _currentBlock;
		
		
		private List<IPredicateBuilder<T>> _predicates;
		
		public WhereBuilder(DataSchema<T> schema, 
		                        DimensionReferenceTranslator<T> dimTranslator,
		                        MeasureReferenceTranslator<T> mesTranslator)
		{
			_schema = schema;
			_rootBlock = new BlockPredicateBuilder<T>();
			_currentBlock = _rootBlock;
			BuilderFactory = new PredicateBuilderFactory<T>(schema, dimTranslator, mesTranslator);
		}
		
		public PredicateBuilderFactory<T> BuilderFactory
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
		
		public WhereBuilder<T> BeginBlock()
		{
			return this;
		}
		
		public WhereBuilder<T> EndBlock()
		{
			return this;
		}
		
		#endregion
	}
}
