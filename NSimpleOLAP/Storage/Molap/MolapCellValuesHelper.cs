using System;
using System.Collections.Generic;
using NSimpleOLAP;
using NSimpleOLAP.Interfaces;
using NSimpleOLAP.Data;

namespace NSimpleOLAP.Storage.Molap
{
	/// <summary>
	/// Description of MolapCellValuesMergeHelper.
	/// </summary>
	internal abstract class MolapCellValuesHelper<T, U>
		where T: struct, IComparable
		where U: class, ICell<T>
	{
		public abstract void UpdateMeasures(U cell, MeasureValuesCollection<T> measures);
		
		public abstract void ClearCell(U cell);
		
		public ValueType Add(ValueType oldvalue, ValueType newvalue,Func<ValueType, ValueType, ValueType> functor)
		{
			return functor(oldvalue, newvalue);
		}
		
	}
}
