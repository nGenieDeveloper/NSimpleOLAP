using System;
using System.Collections.Generic;
using NSimpleOLAP;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Storage.Interfaces;
using NSimpleOLAP.Query.Interfaces;
using NSimpleOLAP.Query.Builder;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of QueryExtensions.
	/// </summary>
	public static class QueryExtensions
	{
		public static QueryBuilder<T> BuildQuery<T>(this Cube<T> cube)
			where T: struct, IComparable
		{
			return new QueryBuilder<T>.QueryBuilderImpl(cube);
		}
	}
}
