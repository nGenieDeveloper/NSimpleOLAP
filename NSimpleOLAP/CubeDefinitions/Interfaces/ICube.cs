using System;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Storage.Interfaces;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Data;

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
		IStorage<T,U> Storage { get; }
		ICellCollection<T, U> Cells { get; }
		DataSourceCollection DataSources { get; }
		bool IsProcessing { get; }
		CubeConfig Config { get; }
		
		void Init();
		void Process();
		void Refresh();
	}
}
