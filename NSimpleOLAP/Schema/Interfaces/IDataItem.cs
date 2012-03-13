/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 20-02-2012
 * Time: 00:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace NSimpleOLAP.Schema.Interfaces
{
	/// <summary>
	/// Description of IDataItem.
	/// </summary>
	public interface IDataItem<T>
		where T: struct, IComparable
	{
		string Name { get; set; }
        T ID { get; set; }
	}
}
