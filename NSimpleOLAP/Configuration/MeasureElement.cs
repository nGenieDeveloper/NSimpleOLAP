using System;
using System.Linq.Expressions;
using System.Configuration;

namespace NSimpleOLAP.Configuration
{
	/// <summary>
	/// Represents a single XML tag inside a ConfigurationSection
	/// or a ConfigurationElementCollection.
	/// </summary>
	public sealed class MeasureElement : ConfigurationElement
	{
		/// <summary>
		/// The attribute <c>name</c> of a <c>MeasureElement</c>.
		/// </summary>
		[ConfigurationProperty("name", IsKey = true, IsRequired = true)]
		[StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\.,", MinLength = 1, MaxLength = 120)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}
	
	
		[ConfigurationProperty("id")]
		public ValueType ID
		{
			get { return (ValueType)this["id"]; }
			set { this["id"] = value; }
		}
		
		[ConfigurationProperty("source", IsRequired = true)]
		[StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\")]
		public string Source
		{
			get { return (string)this["source"]; }
			set { this["source"] = value; }
		}
		
		[ConfigurationProperty("valueFieldName")]
		[StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\")]
		public string ValueFieldName
		{
			get { return (string)this["valueFieldName"]; }
			set { this["valueFieldName"] = value; }
		}
		
		[ConfigurationProperty("valueFieldIndex")]
		public int? ValueFieldIndex
		{
			get { return (int?)this["valueFieldIndex"]; }
			set { this["valueFieldIndex"] = value; }
		}
		
		public Type DataType
		{
			get;
			set;
		}
		
		public Expression MergeFunction
		{
			get;
			set;
		}
	}
	
}

