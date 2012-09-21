using System;
using System.Collections.Generic;
using NSimpleOLAP;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Storage.Interfaces;
using NSimpleOLAP.Query.Predicates;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of PredicateFactory.
	/// </summary>
	public class PredicateBuilderFactory<T>
		where T: struct, IComparable
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
		
		/*
		internal IPredicateBuilder<T> CreateAndPredicate(params IPredicateBuilder<T>[] leftPredicates)
		{
			
		}
		
		internal IPredicateBuilder<T> CreateOrPredicate(params IPredicateBuilder<T>[] leftPredicates)
		{
		
		}
		*/
		
		
		#endregion
	}
}
