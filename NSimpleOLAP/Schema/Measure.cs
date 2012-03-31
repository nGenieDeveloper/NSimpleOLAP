using System;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Common;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of Measure.
	/// </summary>
	public class Measure<T> : IMeasure<T>
		where T: struct, IComparable
	{
		public Measure()
		{
		}
		
		public Measure(MeasureConfig config )
		{
			this.Config = config;
		}
		
		public Type DataType {
			get;
			set;
		}
		
		public string Name {
			get;
			set;
		}
		
		public T ID {
			get;
			set;
		}
		
		public ItemType ItemType { 
			get { return ItemType.Measure; }
		}
		
		public MeasureConfig Config { 
			get; 
			set; 
		}
	}
}
