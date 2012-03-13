/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 23-02-2012
 * Time: 00:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NSimpleOLAP.Configuration;

namespace NSimpleOLAP.Configuration.Fluent
{
	/// <summary>
	/// Description of DimensionBuilder.
	/// </summary>
	public class DimensionBuilder
	{
		private DimensionElement _element;
		
		public DimensionBuilder()
		{
			_element = new DimensionElement();
		}
		
		#region public methods
		
		public DimensionBuilder SetName(string name)
		{
			_element.Name = name;
			return this;
		}
		
		public DimensionBuilder SetID<T>(T id)
			where T: struct, IComparable
		{
			_element.ID = id;
			return this;
		}
		
		public DimensionBuilder DescField(string name)
		{
			_element.DesFieldName = name;
			return this;
		}
		
		public DimensionBuilder DescField(int index)
		{
			_element.ValueFieldIndex = index;
			return this;
		}
		
		public DimensionBuilder ValueField(string name)
		{
			_element.ValueFieldName = name;
			return this;
		}
		
		public DimensionBuilder ValueField(int index)
		{
			_element.ValueFieldIndex = index;
			return this;
		}
		
		public DimensionBuilder Source(string name)
		{
			_element.Source = name;
			return this;
		}
		
		public DimensionBuilder AllowsMembersWithSameName()
		{
			_element.AllowsMembersWithSameName = true;
			return this;
		}
		
		public DimensionElement Create()
		{
			return _element;
		}
		
		#endregion
	}
}
