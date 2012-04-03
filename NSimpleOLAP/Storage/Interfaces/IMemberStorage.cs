using System;
using System.Collections.Generic;
using NSimpleOLAP.Schema.Interfaces;

namespace NSimpleOLAP.Storage.Interfaces
{
	/// <summary>
	/// Description of IMemberStorage.
	/// </summary>
	public interface IMemberStorage<T, TMember>: ICollection<TMember>, IDisposable
		where T: struct, IComparable
		where TMember: IDataItem<T>
	{
		TMember this[T key] { get; }
		TMember this[string name ] { get; }
	}
}
