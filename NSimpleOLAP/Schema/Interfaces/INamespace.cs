using System;
using System.Collections.Generic;
using NSimpleOLAP.Common;

namespace NSimpleOLAP.Schema.Interfaces
{
	/// <summary>
	/// Description of INamespace.
	/// </summary>
	public interface INamespace<T> : ICollection<IDataItem<T>>, IDisposable
		where T: struct, IComparable
	{
		IDataItem<T> this[T key] { get; }
		IDataItem<T> this[string name] { get; }
		void Clear(ItemType type);
	}
}
