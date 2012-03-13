/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 17-02-2012
 * Time: 22:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;
using NSimpleOLAP.Storage.Interfaces;

namespace NSimpleOLAP.Configuration.Interfaces
{
	/// <summary>
	/// Description of IStoreConfig.
	/// </summary>
	public interface IStoreConfig<T>
		where T: struct, IComparable
	{
		Func<KeyValuePair<T,T>[], T> HashingFunction { get; }
		Action<object, IVarData<T>> VarMergeFunction { get; }
		
		StorageType StoreType { get; }
	}
}
