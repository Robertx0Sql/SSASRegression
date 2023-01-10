using System;
using System.Runtime.InteropServices;

namespace MDXStudio.QueryEditor
{
	internal class NativeMethods
	{
		internal const int WM_USER = 1024;

		internal const int EM_FORMATRANGE = 1081;

		internal const uint EM_LINEINDEX = 187;

		internal const uint EM_LINELENGTH = 193;

		internal const uint EM_POSFROMCHAR = 214;

		internal const uint EM_CHARFROMPOS = 215;

		internal const uint EM_GETFIRSTVISIBLELINE = 206;

		internal const uint EM_SCROLL = 181;

		internal const uint WM_SETREDRAW = 11;

		internal const uint WM_KEYDOWN = 256;

		internal const int SB_LINEDOWN = 1;

		internal const int SB_LINEUP = 0;

		internal const int SB_PAGEDOWN = 3;

		internal const int SB_PAGEUP = 2;

		private static int m_LockedWindows;

		private NativeMethods()
		{
		}

		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=false)]
		private static extern bool LockWindowUpdate(IntPtr hWndLock);

		internal static void NestedLockWindowUpdate(IntPtr h)
		{
			if (NativeMethods.m_LockedWindows == 0)
			{
				NativeMethods.LockWindowUpdate(h);
			}
			NativeMethods.m_LockedWindows = NativeMethods.m_LockedWindows + 1;
		}

		internal static void NestedUnlockWindowUpdate()
		{
			NativeMethods.m_LockedWindows = NativeMethods.m_LockedWindows - 1;
			if (NativeMethods.m_LockedWindows == 0)
			{
				NativeMethods.LockWindowUpdate(IntPtr.Zero);
			}
		}

		internal static void SendKeyDownEvent(IntPtr h, char key)
		{
			IntPtr intPtr = NativeMethods.SendMessageInt(h, 256, (IntPtr)key, (IntPtr)1);
			intPtr.ToInt32();
		}

		[DllImport("user32", CharSet=CharSet.None, ExactSpelling=false)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wp, ref NativeMethods.FORMATRANGE lp);

		[DllImport("user32", CharSet=CharSet.Auto, EntryPoint="SendMessage", ExactSpelling=false)]
		internal static extern IntPtr SendMessageInt(IntPtr handle, uint msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32", CharSet=CharSet.None, EntryPoint="ShowCaret", ExactSpelling=false)]
		internal static extern bool ShowCaretAPI(IntPtr hwnd);

		internal struct CHARRANGE
		{
			public int cpMin;

			public int cpMax;
		}

		internal struct FORMATRANGE
		{
			public IntPtr hdc;

			public IntPtr hdcTarget;

			public NativeMethods.RECT rc;

			public NativeMethods.RECT rcPage;

			public NativeMethods.CHARRANGE chrg;
		}

		internal struct RECT
		{
			public int Left;

			public int Top;

			public int Right;

			public int Bottom;
		}
	}
}