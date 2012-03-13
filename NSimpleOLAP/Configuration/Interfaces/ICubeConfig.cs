/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 22-02-2012
 * Time: 00:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace NSimpleOLAP.Configuration.Interfaces
{
	/// <summary>
	/// Description of ICubeConfig.
	/// </summary>
	public interface ICubeConfig<T>
		where T: struct, IComparable
	{
		IStoreConfig<T> StoreConfig { get; }
		IDataSourceConfig<T> DataSourceConfig { get; }
		IMetaDataConfig<T> MetaDataConfig { get; }
	}
}
