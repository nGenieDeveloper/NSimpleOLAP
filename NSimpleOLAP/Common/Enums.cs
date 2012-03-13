/*
 * Created by SharpDevelop.
 * User: calex
 * Date: 22-02-2012
 * Time: 00:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace NSimpleOLAP.Common
{
	public enum DimensionType { Numeric = 0, Date = 1, DayHour = 2 }
	
	public enum StorageType { Molap = 0, Rolap = 1 }
	
	public enum DataSourceType { CSV = 0, DataSet = 1, DataBase = 2 }
}
