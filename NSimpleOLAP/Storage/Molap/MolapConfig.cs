/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 19-02-2012
 * Time: 22:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Storage.Interfaces;
using NSimpleOLAP.Configuration.Interfaces;

namespace NSimpleOLAP.Storage.Molap
{
	/// <summary>
	/// Description of MolapConfig.
	/// </summary>
	public abstract class MolapConfig<T> : IStoreConfig<T>
		where T: struct, IComparable
	{
		public Func<KeyValuePair<T, T>[], T> HashingFunction {
			get;
			private set;
		}
		
		public Action<object, IVarData<T>> VarMergeFunction {
			get;
			private set;
		}
		
		public StorageType StoreType 
		{
			get;
			private set;
		}
	}
}
