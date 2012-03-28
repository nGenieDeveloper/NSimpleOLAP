using System;
using System.Data;
using System.Configuration;

namespace NSimpleOLAP.Configuration
{
	/// <summary>
	/// Represents a single XML tag inside a ConfigurationSection
	/// or a ConfigurationElementCollection.
	/// </summary>
	internal sealed class DataTableConfig : ConfigurationElement
	{
		/// <summary>
		/// The attribute <c>name</c> of a <c>DataTableConfigElement</c>.
		/// </summary>
		[ConfigurationProperty("datatable")]
		internal DataTable Table
		{
			get { return (DataTable)this["datatable"]; }
			set { this["datatable"] = value; }
		}
	}
}

