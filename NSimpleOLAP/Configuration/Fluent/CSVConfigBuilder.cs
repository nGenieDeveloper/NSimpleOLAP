using System;
using System.Configuration;
using NSimpleOLAP.Configuration;

namespace NSimpleOLAP.Configuration.Fluent
{
	/// <summary>
	/// Description of CSVConfigBuilder.
	/// </summary>
	public class CSVConfigBuilder
	{
		private CSVElement _element;
		
		public CSVConfigBuilder()
		{
			_element = new CSVElement();
		}
		
		#region public methods
		
		public CSVConfigBuilder SetFilePath(string path)
		{
			_element.FilePath = path;
			return this;
		}
		
		public CSVConfigBuilder SetFieldDelimiter(char delimiter)
		{
			_element.FieldDelimiter = delimiter;
			return this;
		}
		
		public CSVConfigBuilder SetIsQuotedText()
		{
			_element.TextIsDoubleQuoted = true;
			return this;
		}
		
		public CSVConfigBuilder SetEncoding(string encoding)
		{
			_element.Encoding = encoding;
			return this;
		}
		
		internal CSVElement Create()
		{
			return _element;
		}
		#endregion
	}
}
