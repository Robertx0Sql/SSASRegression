using System;
using System.Drawing;

namespace MDXStudio.QueryEditor
{
	public static class FontUtils
	{
		private readonly static FontStyle[] SUBSTITUTION_SEQUENCE;

		static FontUtils()
		{
			FontStyle[] fontStyleArray = new FontStyle[] { FontStyle.Regular, FontStyle.Strikeout, FontStyle.Underline, FontStyle.Italic, FontStyle.Bold, FontStyle.Underline | FontStyle.Strikeout, FontStyle.Italic | FontStyle.Strikeout, FontStyle.Bold | FontStyle.Strikeout, FontStyle.Italic | FontStyle.Underline, FontStyle.Bold | FontStyle.Underline, FontStyle.Bold | FontStyle.Italic, FontStyle.Italic | FontStyle.Underline | FontStyle.Strikeout, FontStyle.Bold | FontStyle.Underline | FontStyle.Strikeout, FontStyle.Bold | FontStyle.Italic | FontStyle.Strikeout, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline | FontStyle.Strikeout };
			FontUtils.SUBSTITUTION_SEQUENCE = fontStyleArray;
		}

		public static Font FontCreate(FontFamily aFamily, float aSize)
		{
			return FontUtils.FontCreate(aFamily.Name, aSize, FontStyle.Regular);
		}

		public static Font FontCreate(string aFamilyName, float aSize)
		{
			return FontUtils.FontCreate(aFamilyName, aSize, FontStyle.Regular);
		}

		public static Font FontCreate(FontFamily aFamily, float aSize, FontStyle aStyle)
		{
			return FontUtils.FontCreate(aFamily.Name, aSize, aStyle);
		}

		public static Font FontCreate(string aFamilyName, float aSize, FontStyle aStyle)
		{
			try
			{
				FontFamily fontFamily = new FontFamily(aFamilyName);
				int num = 0;
				while (num <= 15)
				{
					FontStyle fontStyle = aStyle ^ FontUtils.SUBSTITUTION_SEQUENCE[num];
					if (!fontFamily.IsStyleAvailable(fontStyle))
					{
						num++;
					}
					else
					{
						Font font = new Font(fontFamily, aSize, fontStyle);
						return font;
					}
				}
			}
			catch
			{
			}
			return new Font("Verdana", aSize, aStyle);
		}
	}
}