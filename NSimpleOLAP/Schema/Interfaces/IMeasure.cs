using System;

namespace NSimpleOLAP.Schema.Interfaces
{
    public interface IMeasure<T> : IDataItem<T>
        where T: struct, IComparable
    {
        Type DataType { get; set; }
    }
}
