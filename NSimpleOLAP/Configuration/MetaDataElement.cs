using System;   
using System.Configuration;

namespace NSimpleOLAP.Configuration
{
	/// <summary>
	/// Represents a single XML tag inside a ConfigurationSection
	/// or a ConfigurationElementCollection.
	/// </summary>
	public sealed class MetaDataElement : ConfigurationElement
	{
		/// <summary>
		/// The attribute <c>name</c> of a <c>MetaDataConfigElement</c>.
		/// </summary>
		[ConfigurationProperty("Dimensions", IsRequired = true)]
		public DimensionElementCollection  Dimensions
		{
			get { return (DimensionElementCollection)this["Dimensions"]; }
			set { this["Dimensions"] = value; }
		}
		
		[ConfigurationProperty("Measures")]
		public MeasureElementCollection  Measures
		{
			get { return (MeasureElementCollection)this["Measures"]; }
			set { this["Measures"] = value; }
		}
		
		[ConfigurationProperty("Metrics")]
		public MetricElementCollection  Metrics
		{
			get { return (MetricElementCollection)this["Metrics"]; }
			set { this["Metrics"] = value; }
		}
	}
	
}

