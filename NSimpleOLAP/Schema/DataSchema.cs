﻿using System;
using System.Collections.Generic;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Schema.Interfaces;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of DataSchema.
	/// </summary>
	public class DataSchema<T> : IDisposable
		where T: struct, IComparable
	{
		public DataSchema()
		{
			Dimensions = new DimensionCollection<T>();
			Measures = new MeasuresCollection<T>();
			Metrics = new MetricsCollection<T>();
		}
		
		public DataSchema(CubeConfig config):this()
		{
			this.Initialize(config);
		}
		
		public DimensionCollection<T> Dimensions 
		{
			get;
			private set;
		}
		
		public MeasuresCollection<T> Measures 
		{
			get;
			private set;
		}
		
		public MetricsCollection<T> Metrics 
		{
			get;
			private set;
		}
		
		#region IDisposable implementation
		
		public void Dispose()
		{
			this.Dimensions.Dispose();
			this.Measures.Dispose();
			this.Metrics.Dispose();
		}
		
		#endregion
		
		#region private members
		
		private void Initialize(CubeConfig config)
		{
			
		}
		
		#endregion
	}
}
