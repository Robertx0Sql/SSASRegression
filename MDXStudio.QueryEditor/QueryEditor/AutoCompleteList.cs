using System;
using System.Drawing;
using System.Windows.Forms;

namespace MDXStudio
{
    internal class AutoCompleteList : ListBox
    {
        private ImageList m_ImageList;

        public ImageList ImageList
        {
            get
            {
                return this.m_ImageList;
            }
            set
            {
                this.m_ImageList = value;
            }
        }

        public AutoCompleteList()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            Rectangle bounds = e.Bounds;
            if (this.m_ImageList != null)
            {
                Size imageSize = this.m_ImageList.ImageSize;
                if (base.Items.Count > 0)
                {
                    AutoCompleteListItem item = (AutoCompleteListItem)base.Items[e.Index];
                    if (item.ImageIndex == -1)
                    {
                        e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor), (float)bounds.Left, (float)bounds.Top);
                    }
                    else
                    {
                        this.m_ImageList.Draw(e.Graphics, bounds.Left, bounds.Top, item.ImageIndex);
                        e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor), (float)(bounds.Left + imageSize.Width), (float)bounds.Top);
                    }
                }
            }
            base.OnDrawItem(e);
        }
    }
}