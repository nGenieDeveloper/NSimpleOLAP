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
		private bool _firstline = true;
		private TextReader _reader;

		public CSVReader(DataSourceConfig config)
		{
			this.Config = config;
			this.Init();
		}
		
		public override bool Next()
		{
			bool ret = false;
			
			string line = _reader.ReadLine();
			
			if (this._firstline && this.Config.CSVConfig.HasHeader)
				line = _reader.ReadLine();
			
			if (line != null)
			{
				string[] strs = line.Split(this.Config.CSVConfig.FieldDelimiter);

				_row.SetData(this.GetValues(strs));
				this.Current = _row;
				ret = true;
			}
			else
				this.Current = null;
			
			this._firstline = false;
			
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
				string value = strs[i].Trim();
				
				
				if (value != string.Empty && !value.Contains("."))
					values[i] = Convert.ChangeType(value, this.Config.Fields[i].FieldType);
				else if (value != string.Empty && value.Contains("."))
				{
					double val = 0;

					if (double.TryParse(value,NumberStyles.Float, CultureInfo.InvariantCulture,  out val))
						values[i] = Convert.ChangeType(val, this.Config.Fields[i].FieldType);
					else
						values[i] = null;
				}
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
