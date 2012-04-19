using System;
using System.Collections.Generic;
using NSimpleOLAP;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Storage.Interfaces;

namespace NSimpleOLAP.Query
{
	/// <summary>
	/// Description of WhereBuilder.
	/// </summary>
	public class WhereBuilder<T>
		where T: struct, IComparable
	{
		public WhereBuilder()
		{
		}
		
		#region fluent interface
		
		public WhereBuilder<T> SetEquals(string tuple, object value)
		{
		
			return this;
		}
		
		public WhereBuilder<T> SetEquals(KeyValuePair<T,T> tuple, object value)
		{
		
			return this;
		}
		
		public WhereBuilder<T> SetGreater(string tuple, object value)
		{
		
			return this;
		}
		
		public WhereBuilder<T> SetGreater(KeyValuePair<T,T> tuple, object value)
		{
		
			return this;
		}
		
		public WhereBuilder<T> SetLower(string tuple, object value)
		{
		
			return this;
		}
		
		public WhereBuilder<T> SetLower(KeyValuePair<T,T> tuple, object value)
		{
		
			return this;
		}
		
		public WhereBuilder<T> SetGreaterOrEqual(string tuple, object value)
		{
		
			return this;
		}
		
		public WhereBuilder<T> SetGreaterOrEqual(KeyValuePair<T,T> tuple, object value)
		{
		
			return this;
		}
		
		public WhereBuilder<T> SetLowerOrEqual(string tuple, object value)
		{
		
			return this;
		}
		
		public WhereBuilder<T> SetLowerOrEqual(KeyValuePair<T,T> tuple, object value)
		{
		
			return this;
		}
		
		public WhereBuilder<T> And()
		{
		
			return this;
		}
		
		
		public WhereBuilder<T> Or()
		{
		
			return this;
		}
		
		public WhereBuilder<T> Not()
		{
		
			return this;
		}
		
		
		
		#endregion
	}
}
