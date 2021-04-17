using NSimpleOLAP.Common;
using NSimpleOLAP.Schema.Interfaces;
using System;

namespace NSimpleOLAP.Schema
{
  /// <summary>
  /// Description of Member.
  /// </summary>
  public class Member<T> : IMember<T>
    where T : struct, IComparable
  {
    public Member()
    {
    }

    public string Name
    {
      get;
      set;
    }

    public T ID
    {
      get;
      set;
    }

    public ItemType ItemType
    {
      get { return ItemType.Member; }
    }
  }
}