using System;
using System.Data;

namespace NSimpleOLAP.Data.Providers
{
	/// <summary>
	/// Description of DBFactory.
	/// </summary>
	public abstract class DBFactory
	{
		public static IDbConnection CreateConnection(string providername)
		{
			return null;
		}
	}
}
