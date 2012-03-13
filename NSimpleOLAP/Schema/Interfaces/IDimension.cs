using System;
using System.Collections.Generic;
using NSimpleOLAP.Schema;

namespace NSimpleOLAP.Schema.Interfaces
{
    public interface IDimension<T>: IDataItem<T>
        where T: struct, IComparable
    {
        MemberCollection<T> Members { get; }
    }
}
