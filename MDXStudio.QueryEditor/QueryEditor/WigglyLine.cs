using System;

namespace MDXStudio.QueryEditor
{
	public class WigglyLine
	{
		internal int StartChar;

		internal int EndChar;

		public WigglyLine(int pos, int len)
		{
			this.StartChar = pos;
			this.EndChar = this.StartChar + len;
		}
	}
}