/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 20-02-2012
 * Time: 00:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Storage.Interfaces;
using NSimpleOLAP.Configuration.Interfaces;

namespace NSimpleOLAP.Interfaces
{
	/// <summary>
	/// Description of ICube.
	/// </summary>
	public interface ICube<T,U>: IDisposable
		where T: struct, IComparable
		where U: class, ICell<T>, new()
	{
		T Key { get; set; }
		string Name { get; set; }
		DataSchema<T> Schema { get; }
		IStoreConfig<T> StorageConfig { get; set; }
		IStorage<T,U> Storage { get; }
		ICellCollection<T, U> Cells { get; }
		object DataSource { get; set; }
		bool IsProcessing { get; }
		
		void Process();
		void Refresh();
	}
}
