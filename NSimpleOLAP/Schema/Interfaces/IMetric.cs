using System;
using System.Collections.Generic;

namespace NSimpleOLAP.Schema.Interfaces
{
    public interface IMetric<T> : IDataItem<T>
        where T : struct, IComparable
    {
        object Expression { get; set; }
    }
}
