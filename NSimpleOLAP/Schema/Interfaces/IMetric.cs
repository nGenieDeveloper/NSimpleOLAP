using System;
using System.Collections.Generic;
using NSimpleOLAP.Configuration;
using System.Linq.Expressions;

namespace NSimpleOLAP.Schema.Interfaces
{
    public interface IMetric<T> : IDataItem<T>
        where T : struct, IComparable
    {
        Expression MetricExpression { get; set; }
        MetricConfig Config { get; set; }
        Type DataType { get; set; }
    }
}
