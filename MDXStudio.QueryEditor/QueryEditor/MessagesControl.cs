using System.Collections.Generic;
using System.Linq;
using MDXParser;
using MDXStudio.QueryEditor;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MDXStudio
{
    public partial class MessagesControl : UserControl
    {
        public MessagesControl()
        {
            InitializeComponent();
        }
        internal System.Windows.Forms.DataGridView DataGridView
        {
            get
            {
                return this.dgvMessages;
            }
        }
        private void dgvMessages_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            System.Windows.Forms.DataGridView dataGridView = sender as System.Windows.Forms.DataGridView;
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            DataGridViewRow item = dataGridView.Rows[e.RowIndex];
            if (item.Cells[e.ColumnIndex].Tag != null)
            {
                return;
            }
            object tag = item.Tag;
            if (tag == null)
            {
                return;
            }
            if (tag.GetType() == typeof(MDXParser.Message))
            {
                MDXParser.Message message = tag as MDXParser.Message;
                MDXStudio.TextBoxSource source = message.Source as MDXStudio.TextBoxSource;
                if (source == null && message.Node != null)
                {
                    source = message.Node.Source as TextBoxSource;
                }
                if (source == null)
                {
                    return;
                }
                MdxEditor textBox = source.TextBox;
                textBox.SelectAll();
                textBox.SelectionBackColor = SystemColors.Window;
                textBox.SelectionStart = message.Location.Position;
                textBox.SelectionLength = message.Location.Length;
                textBox.SelectionBackColor = (message.Type == MessageType.Error ? Color.Red : Color.Yellow);
                textBox.Focus();
                textBox.ScrollToCaret();
                textBox.SelectionLength = 0;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
