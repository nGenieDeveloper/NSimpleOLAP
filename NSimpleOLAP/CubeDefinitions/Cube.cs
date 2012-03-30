using System;
using System.Collections.Generic;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Storage.Interfaces;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Data;
using NSimpleOLAP.Common.Interfaces;

namespace NSimpleOLAP
{
	/// <summary>
	/// Description of Cube.
	/// </summary>
	public class Cube<T> : ICube<T, Cell<T>>, IProcess
		where T: struct, IComparable
	{
		public Cube()
		{
			this.DataSources = new DataSourceCollection();
		}
		
		public Cube(CubeConfig config): this()
		{
			this.Config = config;
		}
		
		public T Key {
			get;
			set;
		}
		
		public string Name {
			get;
			set;
		}
		
		public DataSchema<T> Schema {
			get;
			private set;
		}
		
		public IStorage<T, Cell<T>> Storage {
			get;
			private set;
		}
		
		public ICellCollection<T, Cell<T>> Cells { 
			get;
			private set;
		}
		
		public DataSourceCollection DataSources {
			get;
			private set;
		}
		
		public bool IsProcessing {
			get;
			private set;
		}
		
		public CubeConfig Config { 
			get; 
			internal set;
		}
		
		#region IDisposable implementations
		
		public void Dispose()
		{
			this.Schema.Dispose();
			this.Storage.Dispose();
			this.DataSources = null;
		}
		
		#endregion
		
		public void Init()
		{
			throw new NotImplementedException();
		}
		
		public void Process()
		{
			throw new NotImplementedException();
		}
		
		public void Refresh()
		{
			throw new NotImplementedException();
		}
	}
}
