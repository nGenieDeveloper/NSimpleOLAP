using System;
using System.Collections.Generic;
using NSimpleOLAP;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Storage.Interfaces;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of Query.
	/// </summary>
	public class Query<T>
		where T: struct, IComparable
	{
		public Query()
		{
		}
	}
}
