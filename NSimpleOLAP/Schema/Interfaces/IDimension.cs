using System;
using System.Configuration;
using System.Collections.Generic;
using NSimpleOLAP.Schema;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Data.Interfaces;

namespace NSimpleOLAP.Schema.Interfaces
{
    public interface IDimension<T>: IDataItem<T>, IDisposable
        where T: struct, IComparable
    {
        MemberCollection<T> Members { get; }
        DimensionConfig Config { get; set; }
        IDataSource DataSource { get; }
    }
}
