/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 23-02-2012
 * Time: 00:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Configuration;

namespace NSimpleOLAP.Configuration
{
	/// <summary>
	/// Description of DimensionElement.
	/// </summary>
	public class DimensionElement
	{
		public DimensionElement()
		{
		}
		
		public string Name
		{
			get;
			set;
		}
		
		public ValueType ID
		{
			get;
			set;
		}
		
		public string Source
		{
			get;
			set;
		}
		
		public string DesFieldName
		{
			get;
			set;
		}
		
		public string ValueFieldName
		{
			get;
			set;
		}
		
		public int? DesFieldIndex
		{
			get;
			set;
		}
		
		public int? ValueFieldIndex
		{
			get;
			set;
		}
		
		public bool AllowsMembersWithSameName
		{
			get;
			set;
		}
	}
}
