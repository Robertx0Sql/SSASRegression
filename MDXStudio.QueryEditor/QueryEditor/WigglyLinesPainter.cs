using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MDXStudio.QueryEditor
{
	public class WigglyLinesPainter : NativeWindow
	{
		private MdxEditor parentTextBox;

		private Bitmap bitmap;

		private Graphics textBoxGraphics;

		private Graphics bufferGraphics;

		private MDXStudio.QueryEditor.WigglyLines m_WigglyLines;

		public MDXStudio.QueryEditor.WigglyLines WigglyLines
		{
			get
			{
				return this.m_WigglyLines;
			}
			set
			{
				this.m_WigglyLines = value;
			}
		}

		internal WigglyLinesPainter(MdxEditor textBox)
		{
			this.m_WigglyLines = new MDXStudio.QueryEditor.WigglyLines();
			this.parentTextBox = textBox;
			this.bitmap = new Bitmap(textBox.Width, textBox.Height);
			this.bufferGraphics = Graphics.FromImage(this.bitmap);
			this.bufferGraphics.Clip = new Region(textBox.ClientRectangle);
			this.textBoxGraphics = Graphics.FromHwnd(textBox.Handle);
			base.AssignHandle(this.parentTextBox.Handle);
		}

		internal void AddWigglyLine(WigglyLine wl)
		{
			if (this.m_WigglyLines != null)
			{
				this.m_WigglyLines.Add(wl);
			}
		}

		internal void ClearWigglyLines()
		{
			if (this.m_WigglyLines != null)
			{
				this.m_WigglyLines.Clear();
			}
		}

		private void CustomPaint()
		{
			int firstVisibleLine = TextBoxAPIHelper.GetFirstVisibleLine(this.parentTextBox);
			firstVisibleLine = TextBoxAPIHelper.GetLineIndex(this.parentTextBox, firstVisibleLine);
			this.bufferGraphics.Clear(Color.Transparent);
			foreach (WigglyLine mWigglyLine in this.m_WigglyLines)
			{
				Point y = TextBoxAPIHelper.PosFromChar(this.parentTextBox, mWigglyLine.StartChar);
				Point x = TextBoxAPIHelper.PosFromChar(this.parentTextBox, mWigglyLine.EndChar);
				x.X = x.X + 1;
				y.Y = y.Y + TextBoxAPIHelper.GetBaselineOffsetAtCharIndex(this.parentTextBox, mWigglyLine.StartChar);
				x.Y = x.Y + TextBoxAPIHelper.GetBaselineOffsetAtCharIndex(this.parentTextBox, mWigglyLine.EndChar);
				this.DrawWave(y, x);
			}
			this.textBoxGraphics.DrawImageUnscaled(this.bitmap, 0, 0);
		}

		private void DrawWave(Point start, Point end)
		{
			Pen red = Pens.Red;
			if (end.X - start.X <= 4)
			{
				this.bufferGraphics.DrawLine(red, start, end);
				return;
			}
			ArrayList arrayLists = new ArrayList();
			for (int i = start.X; i <= end.X - 2; i = i + 4)
			{
				arrayLists.Add(new Point(i, start.Y));
				arrayLists.Add(new Point(i + 2, start.Y + 2));
			}
			Point[] array = (Point[])arrayLists.ToArray(typeof(Point));
			this.bufferGraphics.DrawLines(red, array);
		}

		protected override void WndProc(ref Message m)
		{
			if (this.m_WigglyLines == null || this.m_WigglyLines.Count == 0)
			{
				base.WndProc(ref m);
				return;
			}
			if (m.Msg != 15)
			{
				base.WndProc(ref m);
				return;
			}
			this.parentTextBox.Invalidate();
			base.WndProc(ref m);
			this.CustomPaint();
		}
	}
}