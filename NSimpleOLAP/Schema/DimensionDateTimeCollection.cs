using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSimpleOLAP.Storage.Interfaces;

namespace NSimpleOLAP.Schema
{
  public class DimensionDateTimeCollection<T> : BaseDataMemberCollection<T, DimensionDateTime<T>>
    where T : struct, IComparable
  {

    public override DimensionDateTime<T> Next(T key)
    {
      var linkedList = new LinkedList<T>(this.Select(x => x.ID));
      var node = linkedList.Find(key);

      if (node != null && node.Next != null)
      {
        return this[node.Next.Value];
      }

      return this[linkedList.First.Value];
    }

    public override DimensionDateTime<T> Previous(T key)
    {
      var linkedList = new LinkedList<T>(this.Select(x => x.ID));
      var node = linkedList.Find(key);

      if (node != null && node.Previous != null)
      {
        return this[node.Previous.Value];
      }

      return this[linkedList.Last.Value];
    }
  }
}
