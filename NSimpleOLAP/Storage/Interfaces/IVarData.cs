/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 17-02-2012
 * Time: 23:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace NSimpleOLAP.Storage.Interfaces
{
	/// <summary>
	/// Description of IVarData.
	/// </summary>
	public interface IVarData<T> : IDictionary<T, object>
		where T: struct, IComparable
	{
		
	}
}
