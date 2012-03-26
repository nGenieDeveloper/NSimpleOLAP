/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 24-03-2012
 * Time: 22:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;   
using System.Configuration;

namespace NSimpleOLAP.Configuration
{
	/// <summary>
	/// Represents a single XML tag inside a ConfigurationSection
	/// or a ConfigurationElementCollection.
	/// </summary>
	public sealed class FieldElement : ConfigurationElement
	{
		/// <summary>
		/// The attribute <c>name</c> of a <c>FieldElement</c>.
		/// </summary>
		[ConfigurationProperty("name", IsKey = true, IsRequired = true)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}
	
	
		/// <summary>
		/// 
		/// </summary>
		[ConfigurationProperty("type")]
		public Type FieldType {
			get { return (Type)this["type"]; }
			set { this["type"] = value; }
		}
	}
	
}

