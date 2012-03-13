/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 20-02-2012
 * Time: 00:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
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
	}
}
