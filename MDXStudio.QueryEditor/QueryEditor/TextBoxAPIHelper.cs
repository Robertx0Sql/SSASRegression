using System;
using System.Drawing;
using System.Windows.Forms;

namespace MDXStudio.QueryEditor
{
	internal class TextBoxAPIHelper
	{
		private const double anInch = 14.4;

		private TextBoxAPIHelper()
		{
		}

		internal static int CharFromPos(TextBoxBase txt, Point pt)
		{
			int x = (pt.X & 65535) + ((pt.Y & 65535) << 16);
			IntPtr intPtr = MDXStudio.QueryEditor.NativeMethods.SendMessageInt(txt.Handle, 215, IntPtr.Zero, new IntPtr(x));
			int num = intPtr.ToInt32();
			int num1 = (num & 65535) >> 16;
			int num2 = num & 65535;
			IntPtr intPtr1 = MDXStudio.QueryEditor.NativeMethods.SendMessageInt(txt.Handle, 187, new IntPtr(num1), IntPtr.Zero);
			return intPtr1.ToInt32() + num2;
		}

		internal static int GetBaselineOffsetAtCharIndex(TextBoxBase tb, int index)
		{
			MDXStudio.QueryEditor.NativeMethods.CHARRANGE cHARRANGE = new MDXStudio.QueryEditor.NativeMethods.CHARRANGE();
			MDXStudio.QueryEditor.NativeMethods.RECT rECT = new MDXStudio.QueryEditor.NativeMethods.RECT();
			MDXStudio.QueryEditor.NativeMethods.RECT rECT1 = new MDXStudio.QueryEditor.NativeMethods.RECT();
			MDXStudio.QueryEditor.NativeMethods.FORMATRANGE fORMATRANGE = new MDXStudio.QueryEditor.NativeMethods.FORMATRANGE();
			RichTextBox richTextBox = tb as RichTextBox;
			if (richTextBox == null)
			{
				return tb.Font.Height;
			}
			int lineFromCharIndex = richTextBox.GetLineFromCharIndex(index);
			IntPtr intPtr = MDXStudio.QueryEditor.NativeMethods.SendMessageInt(richTextBox.Handle, 187, new IntPtr(lineFromCharIndex), IntPtr.Zero);
			int num = intPtr.ToInt32();
			IntPtr intPtr1 = MDXStudio.QueryEditor.NativeMethods.SendMessageInt(richTextBox.Handle, 193, new IntPtr(lineFromCharIndex), IntPtr.Zero);
			int num1 = intPtr1.ToInt32();
			cHARRANGE.cpMin = num;
			cHARRANGE.cpMax = num + num1;
			rECT.Top = 0;
			rECT.Bottom = 14;
			rECT.Left = 0;
			rECT.Right = 10000000;
			rECT1.Top = 0;
			rECT1.Bottom = 14;
			rECT1.Left = 0;
			rECT1.Right = 10000000;
			Graphics graphic = Graphics.FromHwnd(richTextBox.Handle);
			IntPtr hdc = graphic.GetHdc();
			fORMATRANGE.chrg = cHARRANGE;
			fORMATRANGE.hdc = hdc;
			fORMATRANGE.hdcTarget = hdc;
			fORMATRANGE.rc = rECT;
			fORMATRANGE.rcPage = rECT1;
			IntPtr intPtr2 = MDXStudio.QueryEditor.NativeMethods.SendMessage(richTextBox.Handle, 1081, IntPtr.Zero, ref fORMATRANGE);
			intPtr2.ToInt32();
			graphic.ReleaseHdc(hdc);
			graphic.Dispose();
			return (int)((double)(fORMATRANGE.rc.Bottom - fORMATRANGE.rc.Top) / 14.4);
		}

		internal static int GetFirstVisibleLine(TextBoxBase txt)
		{
			IntPtr intPtr = MDXStudio.QueryEditor.NativeMethods.SendMessageInt(txt.Handle, 206, IntPtr.Zero, IntPtr.Zero);
			return intPtr.ToInt32();
		}

		internal static int GetLineIndex(TextBoxBase txt, int line)
		{
			IntPtr intPtr = MDXStudio.QueryEditor.NativeMethods.SendMessageInt(txt.Handle, 187, new IntPtr(line), IntPtr.Zero);
			return intPtr.ToInt32();
		}

		internal static int GetTextWidthAtCharIndex(TextBoxBase tb, int index, int length)
		{
			MDXStudio.QueryEditor.NativeMethods.CHARRANGE cHARRANGE = new MDXStudio.QueryEditor.NativeMethods.CHARRANGE();
			MDXStudio.QueryEditor.NativeMethods.RECT width = new MDXStudio.QueryEditor.NativeMethods.RECT();
			MDXStudio.QueryEditor.NativeMethods.RECT rECT = new MDXStudio.QueryEditor.NativeMethods.RECT();
			MDXStudio.QueryEditor.NativeMethods.FORMATRANGE fORMATRANGE = new MDXStudio.QueryEditor.NativeMethods.FORMATRANGE();
			RichTextBox richTextBox = tb as RichTextBox;
			if (richTextBox == null)
			{
				return tb.Font.Height;
			}
			cHARRANGE.cpMin = index;
			cHARRANGE.cpMax = index + length;
			width.Top = 0;
			width.Bottom = 14;
			width.Left = 0;
			Size clientSize = richTextBox.ClientSize;
			width.Right = (int)((double)clientSize.Width * 14.4);
			rECT.Top = 0;
			rECT.Bottom = 14;
			rECT.Left = 0;
			Size size = richTextBox.ClientSize;
			rECT.Right = (int)((double)size.Width * 14.4);
			Graphics graphic = Graphics.FromHwnd(richTextBox.Handle);
			IntPtr hdc = graphic.GetHdc();
			fORMATRANGE.chrg = cHARRANGE;
			fORMATRANGE.hdc = hdc;
			fORMATRANGE.hdcTarget = hdc;
			fORMATRANGE.rc = width;
			fORMATRANGE.rcPage = rECT;
			MDXStudio.QueryEditor.NativeMethods.SendMessage(richTextBox.Handle, 1081, IntPtr.Zero, ref fORMATRANGE);
			graphic.ReleaseHdc(hdc);
			graphic.Dispose();
			return (int)((double)(fORMATRANGE.rc.Right - fORMATRANGE.rc.Left) / 14.4);
		}

		internal static Point PosFromChar(TextBoxBase txt, int charIndex)
		{
			IntPtr intPtr = MDXStudio.QueryEditor.NativeMethods.SendMessageInt(txt.Handle, 214, new IntPtr(charIndex), IntPtr.Zero);
			return new Point(intPtr.ToInt32());
		}

		internal static void ScrollLineDown(TextBoxBase txt)
		{
			IntPtr intPtr = MDXStudio.QueryEditor.NativeMethods.SendMessageInt(txt.Handle, 181, (IntPtr)1, IntPtr.Zero);
			intPtr.ToInt32();
		}

		internal static void ScrollLineUp(TextBoxBase txt)
		{
			IntPtr intPtr = MDXStudio.QueryEditor.NativeMethods.SendMessageInt(txt.Handle, 181, (IntPtr)0, IntPtr.Zero);
			intPtr.ToInt32();
		}

		internal static void ShowCaret(TextBox txt)
		{
			bool flag = false;
			for (int i = 0; !flag && i < 10; i++)
			{
				flag = MDXStudio.QueryEditor.NativeMethods.ShowCaretAPI(txt.Handle);
			}
		}
	}
}