using System;
using System.Collections.Generic;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Data.Interfaces;
using NSimpleOLAP.Common;
using NSimpleOLAP.Common.Interfaces;
using NSimpleOLAP.Storage.Interfaces;

namespace NSimpleOLAP.Schema
{
	/// <summary>
	/// Description of Dimension.
	/// </summary>
	public class Dimension<T> : IDimension<T>, IProcess
		where T: struct, IComparable
	{
		public Dimension()
		{
			
		}
		
		public Dimension(DimensionConfig dimconfig, IDataSource datasource)
		{
			this.Config = dimconfig;
			this.DataSource = datasource;
		}
		
		public string Name {
			get;
			set;
		}
		
		public T ID {
			get;
			set;
		}
		
		public ItemType ItemType { 
			get { return ItemType.Dimension; }
		}
		
		public MemberCollection<T> Members {
			get;
			private set;
		}
		
		public DimensionConfig Config {
			get;
			set;
		}
		
		public IDataSource DataSource {
			get;
			private set;
		}
		
		public void Process()
		{
			throw new NotImplementedException();
		}
		
		public void Refresh()
		{
			throw new NotImplementedException();
		}
		
		#region
		
		internal void SetMembersStorage(IMemberStorage<T, Member<T>> storage)
		{
			this.Members = new MemberCollection<T>(storage);
		}
		
		#endregion
		
		#region IDisposable
		
		public void Dispose()
		{
			this.Members.Dispose();
		}
		
		#endregion
	}
}
