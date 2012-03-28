﻿using System;
using System.Configuration;
using NSimpleOLAP.Common;
using NSimpleOLAP.Configuration;

namespace NSimpleOLAP.Data.Readers
{
	/// <summary>
	/// Description of AbsReader.
	/// </summary>
	public abstract class AbsReader: IDisposable
	{
		
		protected DataSourceElement Config
		{
			get;
			set;
		}
		
		#region public members
		
		public AbsRowData Current
		{
			get;
			protected set;
		}
		
		public abstract bool Next();
		
		public static AbsReader Create(DataSourceElement config)
		{
			AbsReader reader = null;
			
			switch (config.SourceType)
			{
				case DataSourceType.CSV: 
					reader = new CSVReader(config);
					break;
				case DataSourceType.DataBase: 
					reader = new DBReader(config);
					break;
				case DataSourceType.DataSet:
					reader = new DTableReader(config);
					break;
			}
			 
			return reader;
		}
		
		#endregion
		
		public abstract void Dispose();
	}
}
