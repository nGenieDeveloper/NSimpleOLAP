using System;
using System.Collections.Generic;
using System.Configuration;
using NSimpleOLAP;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Configuration.Fluent;

namespace NSimpleOLAP.Configuration.Extensions
{
	/// <summary>
	/// Description of ConfigExtensions.
	/// </summary>
	internal static class ConfigExtensions
	{
		public static Dictionary<string, int> GetFieldIndexes(this FieldElementCollection fields)
		{
			Dictionary<string, int> dict = new Dictionary<string, int>();
			
			for (int i = 0; i < fields.Count; i++)
				dict.Add(fields[i].Name, i);
			
			return dict;
		}
		
		public static CubeBuilder SetupConfig<T>(this Cube<T> cube)
			where T: struct, IComparable
		{
			cube.Config = new CubeConfig();
			CubeBuilder builder = new CubeBuilder(cube.Config);
				
			return builder;
		}
		
		public static Cube<T> Create<T>(this CubeBuilder cubebuilder)
			where T: struct, IComparable
		{
			CubeConfig cube = cubebuilder.CreateConfig();
		
			return new Cube<T>(cube);
		}
	}
}
