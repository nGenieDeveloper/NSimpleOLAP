using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Configuration;

namespace NSimpleOLAP.Configuration.Fluent
{
	/// <summary>
	/// Description of MolapStorageBuilder.
	/// </summary>
	public class MolapStorageBuilder
	{
		private MolapStorageConfig _element;
		
		public MolapStorageBuilder()
		{
			_element = new MolapStorageConfig();
		}
		
		#region public methods
		
		public MolapStorageBuilder SetStoreType(MolapHashTypes hashtype)
		{
			_element.HashType = hashtype;
			return this;
		}
		
		internal MolapStorageConfig Create()
		{
			return _element;
		}
		
		#endregion
	}
}
