﻿namespace NSimpleOLAP.Common
{
  public enum DimensionType { Numeric = 0, Date = 1, DayHour = 2, Levels = 3 }

  public enum StorageType { Molap = 0, Rolap = 1 }

  public enum DataSourceType { CSV = 0, DataSet = 1, DataBase = 2 }

  public enum ItemType { Dimension = 0, Measure = 1, Metric = 2, Member = 3 }

  public enum MolapHashTypes { FNV, FNV1A, MURMUR2, CITY }

  public enum LogicalOperators { EQUALS = 0, GREATERTHAN = 1, LOWERTHAN = 2, GREATEROREQUALS = 3, LOWEROREQUALS = 4, NOTEQUALS = 5, IN = 6 }

  public enum DataValueType { AGGREGATED, FACT }

  public enum PredicateType { BLOCK = 0, AND = 1, OR = 2, NOT = 3, DIMENSION = 4, MEASURE = 5, NULL = 6 }

  public enum OperationMode { OnDemand, PreAggregate }

  public enum OutputCellType { DATA, COLUMN_LABEL, ROW_LABEL }

  public enum OperationType { NONE = 0, SUM = 1, SUBTRACTION = 2, MULTIPLICATION = 3, DIVISION = 4, MIN = 5, MAX = 6, AVERAGE = 7, VALUE = 8 }

  public enum DateTimeLevels { DATE = 0, DAY = 1 , MONTH_WITH_YEAR = 2, QUARTER = 3 , YEAR = 4, WEEK = 5, MONTH = 6 }
}