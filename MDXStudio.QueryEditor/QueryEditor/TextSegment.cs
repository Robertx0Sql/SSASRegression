using System;

namespace MDXStudio.QueryEditor
{
	public class TextSegment
	{
		private eHighlightType mHighlightType;

		private int mStartPosition;

		private int mLenth;

		public eHighlightType HighlightType
		{
			get
			{
				return this.mHighlightType;
			}
			set
			{
				this.mHighlightType = value;
			}
		}

		public int Lenth
		{
			get
			{
				return this.mLenth;
			}
			set
			{
				this.mLenth = value;
			}
		}

		public int StartPosition
		{
			get
			{
				return this.mStartPosition;
			}
			set
			{
				this.mStartPosition = value;
			}
		}

		public TextSegment(eHighlightType pHighlightType, int pStartPosition, int pLength)
		{
			this.mHighlightType = pHighlightType;
			this.mStartPosition = pStartPosition;
			this.mLenth = pLength;
		}

		public override bool Equals(object pObj)
		{
			if (pObj is TextSegment)
			{
				TextSegment textSegment = (TextSegment)pObj;
				if (this.mStartPosition == textSegment.mStartPosition && this.mLenth == textSegment.mLenth && this.mHighlightType == textSegment.mHighlightType)
				{
					return true;
				}
			}
			return false;
		}

		public override int GetHashCode()
		{
			return this.mStartPosition;
		}

		public bool StartIsIn(TextSegment pTextSegment)
		{
			bool flag = false;
			if (this.mStartPosition - pTextSegment.StartPosition > 0 & this.mStartPosition - pTextSegment.StartPosition < pTextSegment.Lenth)
			{
				flag = true;
			}
			return flag;
		}
	}
}