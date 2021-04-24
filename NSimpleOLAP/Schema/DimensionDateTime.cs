using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSimpleOLAP.Common;
using NSimpleOLAP.Configuration;
using NSimpleOLAP.Data.Interfaces;
using NSimpleOLAP.Schema.Interfaces;
using NSimpleOLAP.Storage.Interfaces;

using NSimpleOLAP.Common.Interfaces;


namespace NSimpleOLAP.Schema
{
  public class DimensionDateTime<T> : Dimension<T>
    where T : struct, IComparable
  {
    private int _level;
    private bool _hasLevels;

    public DimensionDateTime(DimensionConfig dimconfig)
    {
      this.Config = dimconfig;
    }

    public new DimensionType TypeOf { get { return DimensionType.Date; } }

    public new int LevelPosition { get { return _level; } }

    public new bool HasLevels { get { return _hasLevels; } }

    public DimensionDateTimeCollection<T> DateDimensions
    {
      get;
      private set;
    }
  }
}
