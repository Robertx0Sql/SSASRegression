using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.AnalysisServices.AdomdClient;


namespace VirtualDataGridView
{

    public partial class CellSetDataGridView : UserControl, ICellSetDataGridView
    {
    
        public CellSetDataGridView()
        {
            InitializeComponent();
            virtualGrid = new VirtualGrid(dataGridView1);
      
        }
        DataGridViewCell SelectedDataGridViewCell=null;

        Color RowsbackColor ;
        Color ColumnsbackColor ;
        
        public void HighlightCell(int rowindex, int columnindex)
        {
            if (SelectedDataGridViewCell != null)
            {
                SelectedDataGridViewCell.Style.SelectionBackColor = dataGridView1.ColumnHeadersDefaultCellStyle.BackColor;
                if (SelectedDataGridViewCell.RowIndex >= 0 )
                dataGridView1.Rows[SelectedDataGridViewCell.RowIndex].DefaultCellStyle.BackColor = RowsbackColor;
                if (SelectedDataGridViewCell.ColumnIndex>=0)
                dataGridView1.Columns[SelectedDataGridViewCell.ColumnIndex].DefaultCellStyle.BackColor = ColumnsbackColor;
            
            }   
            if ((rowindex <= dataGridView1.RowCount) && (columnindex <= dataGridView1.ColumnCount) && columnindex >= 0 && rowindex >= 0 && dataGridView1.RowCount > 0 && dataGridView1.ColumnCount>0)
            {
                dataGridView1.ClearSelection();

                DataGridViewCell dgvc = dataGridView1.Rows[rowindex].Cells[columnindex];

                dgvc.Selected = true;
                 RowsbackColor = dataGridView1.Rows[rowindex].DefaultCellStyle.BackColor;
                ColumnsbackColor = dataGridView1.Columns[columnindex].DefaultCellStyle.BackColor;
                dataGridView1.Rows[rowindex].DefaultCellStyle.BackColor = Color.AliceBlue;
                dataGridView1.Columns[columnindex].DefaultCellStyle.BackColor = Color.AliceBlue;
                dgvc.Style.SelectionBackColor = Color.Red;
                
                dataGridView1.FirstDisplayedCell = dgvc;
                SelectedDataGridViewCell = dgvc;

                RaiseEventCellEnter(this, new DataGridViewCellEventArgs (columnindex,rowindex));
            }
            else 
                dataGridView1.ClearSelection();
               
        }

        public string CellProperty
        {
            get { return virtualGrid.CellProperty; }
            set { 
                virtualGrid.CellProperty = value;
                this.virtualGrid.Redraw();
            }
        }

        public List<string> CellPropertiesList
        {
            get { 
                List<string> result = new List<string>();
                try
                {
                    //should test for nulls but just exit 
                    CellSet cellSetBinding = this.virtualGrid.CellSetBinding;
                    if (cellSetBinding != null)
                    {
                        OlapInfoPropertyCollection.Enumerator enumerator = cellSetBinding.OlapInfo.CellInfo.CellProperties.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            OlapInfoProperty current = enumerator.Current;
                            result.Add(current.Name);
                            //  Console.WriteLine(current.Name);
                        }
                    }
                   
                }
                catch { }
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


        private void dataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                this.virtualGrid.CellValueNeeded(sender, e);
            }
            catch (Exception exception)
            {
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.virtualGrid.CellDoubleClick(sender, e);
        }

        private bool _DataSourceValid;

        public bool DataSourceValid
        {
            get
            {
                return _DataSourceValid;
            }
        }
        public bool IsCellError
        {
            get
            {
                return virtualGrid.IsCellError;
            }
        }
        public void DrawCellSetInGrid(CellSet cs)
        {
            
            SelectedDataGridViewCell = null;//reset selectedDataGridViewCell
            MessageLog msglog = new MessageLog();
            virtualGrid.DrawCellSetInGrid(cs, msglog, false);
            _DataSourceValid = (cs != null);
        }
      public  void CopyData()
        {
            try
            {
                if (this.dataGridView1.RowCount > 0)
                {
                    dataGridView1.SelectAll();

                    Clipboard.SetDataObject(
                         this.dataGridView1.GetClipboardContent());
                }
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
               throw new Exception(
                    "The Clipboard could not be accessed.");
            }
       
        }

      public void Redraw()
      {
          this.virtualGrid.Redraw();
      }

      private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
      {
          RaiseEventCellEnter(sender, e);
      }

    }
}
