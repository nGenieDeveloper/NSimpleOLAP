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

		//Where(b => b.And(b1 => b1.Measure("x").Equals(7), b1 => b1.Dimension("W").NotEquals("ddd"))
		
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
		
		public AndPredicateBuilder<T> And(params Func<WhereBuilder<T>, IPredicateBuilder<T>>[] andPreds)
		{
		
			return null;
		}
		
		
		public OrPredicateBuilder<T> Or(params Func<WhereBuilder<T>, IPredicateBuilder<T>>[] orPreds)
		{
		
			return null;
		}
		
		public NotPredicateBuilder<T> Not(Func<WhereBuilder<T>, IPredicateBuilder<T>> notPred)
		{
		
			return null;
		}

		public MeasureSlicerBuilder<T> Dimension(string dimension)
		{

			return null;
		}

		public DimensionSlicerBuilder<T> Measure(string measure)
		{

			return null;
		}

		public BlockPredicateBuilder<T> Block(Func<WhereBuilder<T>, IPredicateBuilder<T>> blockPred)
		{
			return null;
		}
		
		
		#endregion
	}

	class teste
  {
		private void dosomeExample()
    {
			var wherebuil = new WhereBuilder<int>(null, null, null);

			wherebuil.And(b => b.Measure("")
			, b => b.Measure("")
			, b=> b.Not(n=> n.And(a => a.Dimension("d"), a => a.Dimension("i"))));

    }
  }
}
