using MDXParser;
using MDXStudio.QueryEditor;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MDXStudio
{
    internal class TextBoxSource : Source
    {
        protected MdxEditor m_TextBox;

        internal MdxEditor TextBox
        {
            get
            {
                return this.m_TextBox;
            }
        }

        internal TextBoxSource(MdxEditor tb, Locator l)
        {
            this.m_TextBox = tb;
            base.StartLocation = l;
        }

        public override Source Clone()
        {
            return new TextBoxSource(this.m_TextBox, base.StartLocation);
        }

        public override void DrawWigglyLine(int pos, int len)
        {
            WigglyLinesPainter tag = this.m_TextBox.Tag as WigglyLinesPainter;
            tag.AddWigglyLine(new WigglyLine(pos, len));
        }

        public override void SetColor(int pos, int len, Color color)
        {
            this.m_TextBox.SelectionStart = base.StartLocation.Position + pos;
            this.m_TextBox.SelectionLength = len;
            this.m_TextBox.SelectionColor = color;
        }
    }
}