using System;
using System.Configuration;
using NSimpleOLAP.Configuration;

namespace NSimpleOLAP.Schema.Interfaces
{
    public interface IMeasure<T> : IDataItem<T>
        where T: struct, IComparable
    {
        Type DataType { get; set; }
        MeasureConfig Config { get; set; }
    }
}
