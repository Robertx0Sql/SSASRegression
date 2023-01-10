using System;

namespace MDXStudio.MdSchema
{
	internal enum eMeasureAggregator
	{
		Unknown = 0,
		Sum = 1,
		Count = 2,
		Min = 3,
		Max = 4,
		Avg = 5,
		Var = 6,
		Std = 7,
		Dst = 8,
		None = 9,
		AvgChildren = 10,
		FirstChild = 11,
		LastChild = 12,
		FirstNonEmpty = 13,
		LastNonEmpty = 14,
		ByAccount = 15,
		Calculated = 255
	}
}