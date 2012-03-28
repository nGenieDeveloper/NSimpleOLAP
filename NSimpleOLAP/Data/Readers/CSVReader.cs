using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Text;
using NSimpleOLAP.Common;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Configuration.Extensions;

namespace NSimpleOLAP.Data.Readers
{
	/// <summary>
	/// Description of CVSReader.
	/// </summary>
	public class CSVReader: AbsReader
	{
		private Row _row;
		private TextReader _reader;

		public CSVReader(DataSourceElement config)
		{
			this.Config = config;
			this.Init();
		}
		
		public override bool Next()
		{
			bool ret = false;
			
			string line = _reader.ReadLine();
			
			if (line != null)
			{
				string[] strs = line.Split(this.Config.CSVConfig.FieldDelimiter);

				_row.SetData(this.GetValues(strs));
				this.Current = _row;
			}
			else
				this.Current = null;
			
			return ret;
		}
		
		public override void Dispose()
		{
			this._reader.Close();
			this._reader.Dispose();
		}
		
		#region private members
		
		private void Init()
		{
			_row = new CSVReader.Row(this.Config.Fields.GetFieldIndexes());
			FileStream file_stream = File.OpenRead(this.Config.CSVConfig.FilePath);
			
			if (this.Config.CSVConfig.Encoding == string.Empty)
				this._reader = new StreamReader(file_stream, true);
			else
				this._reader = new StreamReader(file_stream, Encoding.GetEncoding(this.Config.CSVConfig.Encoding));
		}
		
		private object[] GetValues(string[] strs)
		{
			object[] values = new object[this.Config.Fields.Count];
			
			for (int i = 0; i < this.Config.Fields.Count; i++)
			{
				if (strs[i].Trim() != string.Empty)
					values[i] = Convert.ChangeType(strs[i], this.Config.Fields[i].FieldType);
				else
					values[i] = null;
			}
			
			return values;
		}
		
		#endregion
		
		#region private class
		
		private class Row : AbsRowData
		{
			public Row(Dictionary<string, int> fields)
			{
				this._indexes = fields;
			}
			
			public void SetData(object[] values)
			{
				_values = values;
			}
		}
		
		#endregion
	}
}
