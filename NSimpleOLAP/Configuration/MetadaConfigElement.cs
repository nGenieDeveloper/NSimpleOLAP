/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 23-02-2012
 * Time: 22:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Configuration;
using NSimpleOLAP.Configuration.Interfaces;

namespace NSimpleOLAP.Configuration
{
	/// <summary>
	/// Description of MetadaConfigElement.
	/// </summary>
	public class MetadaConfigElement<T> : IMetaDataConfig<T>
		where T: struct, IComparable
	{
		public MetadaConfigElement()
		{
		}
		
		public DimensionElemCollection Dimensions {
			get;
			private set;
		}
		
		public MeasureElemCollection Measures {
			get;
			private set;
		}
		
		public MetricElemCollection Metrics {
			get;
			private set;
		}
	}
}
