using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SSASRegressionUI
{
    public partial class FrmQueryStats : Form
    {
        public FrmQueryStats()
        {
            InitializeComponent();
        }
        private DataTable _QueryStats;

        public DataTable QueryStats
        {
            get { return _QueryStats; }
            set { _QueryStats = value;

            UpdateGrid();
            }
        }

        private void UpdateGrid()
        {
            dataGridView1.DataSource = _QueryStats;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].ValueType == typeof(DateTime))
            { LongFormDateTimeFormat(e);
            }
        }
        private static void LongFormDateTimeFormat(DataGridViewCellFormattingEventArgs formatting)
        {
            if (formatting.Value != null)
            {
                try
                {
                    DateTime theDate = DateTime.Parse(formatting.Value.ToString());

                    formatting.Value = String.Format("{0} {1}", theDate.ToShortDateString(), theDate.ToLongTimeString());// .ToString("dd-MM-yy hh:mm:ss");
                    formatting.FormattingApplied = true;
                }
                catch (FormatException)
                {
                    // Set to false in case there are other handlers interested trying to 
                    // format this DataGridViewCellFormattingEventArgs instance.
                    formatting.FormattingApplied = false;
                }
            }
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if (dataGridView1.Columns[e.Column.Index].ValueType == typeof(DateTime))
            {
                e.Column.Width = 125;
            }
            if (dataGridView1.Columns[e.Column.Index].Name=="Test ID")
            {
              //  e.Column.Width = 230;
            }
            if (dataGridView1.Columns[e.Column.Index].Name == "Description")
            {
                e.Column.Width = 230;
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectAll();

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(
                             dataGridView1.GetClipboardContent());
        }
    }
}
