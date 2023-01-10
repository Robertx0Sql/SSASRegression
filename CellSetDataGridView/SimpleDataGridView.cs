using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSASRegressionCL ;
namespace VirtualDataGridView
{


    public partial class SimpleDataGridView : UserControl, ICellSetDataGridView
    {
        private bool _IsCellError;
        DataGridViewCell SelectedDataGridViewCell = null;

        Color RowsbackColor;
        Color ColumnsbackColor;
        
        public SimpleDataGridView()
        {
            InitializeComponent();
        }

  

        public void HighlightCell(int rowindex, int columnindex)
        {
            if (SelectedDataGridViewCell != null)
            {
                SelectedDataGridViewCell.Style.SelectionBackColor = gridDataTable.ColumnHeadersDefaultCellStyle.BackColor;
                if (SelectedDataGridViewCell.RowIndex >= 0)
                    gridDataTable.Rows[SelectedDataGridViewCell.RowIndex].DefaultCellStyle.BackColor = RowsbackColor;
                if (SelectedDataGridViewCell.ColumnIndex >= 0)
                    gridDataTable.Columns[SelectedDataGridViewCell.ColumnIndex].DefaultCellStyle.BackColor = ColumnsbackColor;

            }
            if ((rowindex <= gridDataTable.RowCount) && (columnindex <= gridDataTable.ColumnCount) && columnindex >= 0 && rowindex >= 0)
            {
                gridDataTable.ClearSelection();

                DataGridViewCell dgvc = gridDataTable.Rows[rowindex].Cells[columnindex];

                dgvc.Selected = true;
                RowsbackColor = gridDataTable.Rows[rowindex].DefaultCellStyle.BackColor;
                ColumnsbackColor = gridDataTable.Columns[columnindex].DefaultCellStyle.BackColor;
                gridDataTable.Rows[rowindex].DefaultCellStyle.BackColor = Color.AliceBlue;
                gridDataTable.Columns[columnindex].DefaultCellStyle.BackColor = Color.AliceBlue;
                dgvc.Style.SelectionBackColor = Color.Red;

                gridDataTable.FirstDisplayedCell = dgvc;
                SelectedDataGridViewCell = dgvc;

                RaiseEventCellEnter(this, new DataGridViewCellEventArgs(columnindex, rowindex));
            }
            else
                gridDataTable.ClearSelection();

        }
        public bool DataSourceValid
        {
            get
            {
                return (gridDataTable.DataSource != null);
            }
        }
        
        public string CellProperty
        {
            get;
            set;
        }

        public List<string> CellPropertiesList
        {
            get
            {
                List<string> result = new List<string>();
                if (result.Count == 0)
                {
                    result.Add("CellOrdinal");
                    result.Add("VALUE");
                    result.Add("FORMATTED_VALUE");
                }
                return result;
            }
        }

        public event CellEnterHandler DataGrid_CellEnterEvent;
       
        private void RaiseEventCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGrid_CellEnterEvent != null)
            {
                DataGrid_CellEnterEvent(sender, e);
            }
        }
        public void DrawCellSetInGrid(Microsoft.AnalysisServices.AdomdClient.CellSet cs)
        {
            if (cs != null)
            {
                CellSetDataTable dt = Utils.Cellset2Datatable(cs);
                if (dt.IsCellError)
                    _IsCellError = true;
                gridDataTable.DataSource = dt;

            }
            else gridDataTable.DataSource = null;
        }

        public void CopyData()
        {
            
        }

        public void Redraw()
        {
            
        }

        
        public bool IsCellError
        {
            get { return _IsCellError; }
        }
    }
}
