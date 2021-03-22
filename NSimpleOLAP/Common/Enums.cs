using System;

namespace NSimpleOLAP.Common
{
	public enum DimensionType { Numeric = 0, Date = 1, DayHour = 2 }
	
	public enum StorageType { Molap = 0, Rolap = 1 }
	
	public enum DataSourceType { CSV = 0, DataSet = 1, DataBase = 2 }
	
	public enum ItemType { Dimension = 0, Measure = 1, Metric = 2 , Member = 3 }
	
	public enum MolapHashTypes { FNV, FNV1A, MURMUR2, CITY }
	
	public enum LogicalOperators { EQUALS, GREATER, LOWER, GREATERTHAN, LOWERTHAN, NOTEQUALS, IN }
	
	public enum DataValueType { AGGREGATED, FACT }
	
	public enum PredicateType { BLOCK = 0, AND = 1, OR = 2, NOT = 3, DIMENSION = 4, MEASURE = 5 }

	public enum OperationMode { OnDemand, PreAggregate }

}
