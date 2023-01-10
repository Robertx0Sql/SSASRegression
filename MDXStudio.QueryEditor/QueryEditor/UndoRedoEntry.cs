using System;

namespace MDXStudio.QueryEditor
{
	internal class UndoRedoEntry
	{
		private readonly string mRtfText;

		private readonly int mSelectionStart;

		private readonly int mSelectionLength;

		private readonly MDXStudio.QueryEditor.WigglyLines mWigglyLines;

		public string RtfText
		{
			get
			{
				return this.mRtfText;
			}
		}

		public int SelectionLength
		{
			get
			{
				return this.mSelectionLength;
			}
		}

		public int SelectionStart
		{
			get
			{
				return this.mSelectionStart;
			}
		}

		public MDXStudio.QueryEditor.WigglyLines WigglyLines
		{
			get
			{
				return this.mWigglyLines;
			}
		}

		public UndoRedoEntry(string pRtfText, int pSelectionStart, int pSelectionLength, MDXStudio.QueryEditor.WigglyLines wl)
		{
			this.mRtfText = pRtfText;
			this.mSelectionStart = pSelectionStart;
			this.mSelectionLength = pSelectionLength;
			this.mWigglyLines = wl;
		}
	}
}