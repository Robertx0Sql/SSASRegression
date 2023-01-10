using System;

namespace MDXStudio
{
    public class AutoCompleteListItem
    {
        private string m_Text;

        private int m_ImageIndex;

        private object m_Tag;

        public int ImageIndex
        {
            get
            {
                return this.m_ImageIndex;
            }
            set
            {
                this.m_ImageIndex = value;
            }
        }

        public object Tag
        {
            get
            {
                return this.m_Tag;
            }
            set
            {
                this.m_Tag = value;
            }
        }

        public string Text
        {
            get
            {
                return this.m_Text;
            }
            set
            {
                this.m_Text = value;
            }
        }

        public AutoCompleteListItem(string text, int index, object tag)
        {
            this.m_Text = text;
            this.m_ImageIndex = index;
            this.m_Tag = tag;
        }

        public AutoCompleteListItem(string text, int index)
            : this(text, index, null)
        {
        }

        public AutoCompleteListItem(string text)
            : this(text, -1)
        {
        }

        public AutoCompleteListItem()
            : this("")
        {
        }

        public override string ToString()
        {
            return this.m_Text;
        }
    }
}