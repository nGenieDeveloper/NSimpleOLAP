using System;

namespace NSimpleOLAP.Common
{
	public enum DimensionType { Numeric = 0, Date = 1, DayHour = 2 }
	
	public enum StorageType { Molap = 0, Rolap = 1 }
	
	public enum DataSourceType { CSV = 0, DataSet = 1, DataBase = 2 }
	
	public enum ItemType { Dimension = 0, Measure = 1, Metric = 2 , Member = 3 }
	
}
