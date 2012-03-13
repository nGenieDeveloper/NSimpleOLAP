/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 22-02-2012
 * Time: 16:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NSimpleOLAP.Configuration;

namespace NSimpleOLAP.Configuration.Interfaces
{
	/// <summary>
	/// Description of IMetaDataConfig.
	/// </summary>
	public interface IMetaDataConfig<T>
		where T: struct, IComparable
	{
		DimensionElemCollection Dimensions { get; }
		MeasureElemCollection Measures { get; }
		MetricElemCollection Metrics { get; }
	}
}
