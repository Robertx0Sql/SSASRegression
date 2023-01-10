using System;
using System.Collections.Generic;

namespace MDXStudio.QueryEditor
{
	public class MyTextSegmentComparer : IComparer<TextSegment>
	{
		public MyTextSegmentComparer()
		{
		}

		public int Compare(TextSegment pTextSegmentA, TextSegment pTextSegmentB)
		{
			int startPosition = pTextSegmentA.StartPosition - pTextSegmentB.StartPosition;
			if (startPosition == 0)
			{
				startPosition = pTextSegmentA.Lenth - pTextSegmentB.Lenth;
			}
			return startPosition;
		}
	}
}