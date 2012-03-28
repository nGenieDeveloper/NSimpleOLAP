using System;
using System.Collections.Generic;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Storage.Interfaces;
using NSimpleOLAP.Configuration;

namespace NSimpleOLAP
{
	/// <summary>
	/// Description of Cube.
	/// </summary>
	public class Cube<T> : ICube<T, Cell<T>>
		where T: struct, IComparable
	{
		public Cube()
		{
			
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
		
		public object DataSource {
			get;
			set;
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
