using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SSASRegressionCL;
using Microsoft.AnalysisServices.AdomdClient;
using System.Diagnostics;

namespace SSASRegressionUI
{
    public partial class DataGridViewCellSetAdvanced : UserControl, IDataGridViewCellSetAdvanced
    {
        private string _GridName = string.Empty;
        private const string cstdateformat = "dd-MM-yy HH:mm:ss";
        public const string CellPropertyDataTable = "DataTable";
        private const string STR_GridCellData = "CellData";
        private const string STR_GridDataTable = "DataTable";

        public event EventHandler PropertyListChanged;
        public event EventHandler<TextArgs> DataGridDataChanged;
        public event CellEnterHandler DataGrid_CellEnterEvent;
        public DataGridViewCellSetAdvanced()
        {
            InitializeComponent();
            this.gridDataTable.Dock = DockStyle.Fill;
            this.gridDataTable.Visible = false;
            cellSetDataGridView1.Visible = true;            
            //subscribe to event
            CellEnterEventEnabled(true);
        }

        // Invoke the Changed event; called whenever list changes
        void OnPropertyListChanged(EventArgs e)
        {
            if (PropertyListChanged != null)
                PropertyListChanged(this, e);
        }
        // Invoke the Changed event; called whenever DataGrid Data changes
        void RaiseDataGridDataChanged(string message)
        {
          //  EventHandler<TextArgs> handler = DataGridDataChanged;
            if (DataGridDataChanged != null)
                DataGridDataChanged(this, new TextArgs(message));
        }



        private bool _ResultCellError;

        private int _RowIndex;
        private int _ColumnIndex;
        public int ColumnIndex
        {
            get
            {
                return _ColumnIndex;
            }
        }
        public int RowIndex
        {
            get
            {
                return _RowIndex;
            }
        }
        public bool IsCellError
        {
            get
            {
                return _ResultCellError;
            }
        }
        public string GridName
        {
            get
            {
                return (_GridName + " ").TrimEnd();
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _GridName = value;
            }
        }
        private void SetResultCellErrorFlag(bool IsError)
        {
            _ResultCellError = IsError;
            //raise event
            lblGridCellErrors.Visible = IsError;
        }
        
        private void RaiseEventCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            _ColumnIndex = e.ColumnIndex;
            _RowIndex = e.RowIndex;
            if (DataGrid_CellEnterEvent != null)
            {
                DataGrid_CellEnterEvent(this, e);
            }
        }

        public void HighlightCell(int rowindex, int columnindex)
        {
            string sGrid = "";
            VirtualDataGridView.ICellSetDataGridView vdataGrid = CurrentDataGridView(ref sGrid);

            vdataGrid.HighlightCell(rowindex, columnindex);
        }
        
        private TestResult _result;
        public string FileName
        {
            get { return txtFileName.Text; }
            set { txtFileName.Text = value; }
        }
        public TestResult result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;

                LoadResult();
            }
        }

        private void LoadResult()
        {

            //Reset
            
            txtServer.Text = string.Empty;
            txtDataBase.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            txtStartDate.Text = string.Empty;
            txtQueryInfo.Text = string.Empty;
            LblQueryInfo.Text = "Info";
            txtQueryInfo.BackColor = txtStartDate.BackColor;
            cellSetDataGridView1.DrawCellSetInGrid(null);
            gridDataTable.DrawCellSetInGrid(null); ;
            SetResultCellErrorFlag(false);
            if (_result != null)
            {
                //reset the current column / row info
                _RowIndex = 0;
                _ColumnIndex = 0;

                btnShowMdx.Enabled = true;
                txtServer.Text = _result.ServerName;
                txtDataBase.Text = _result.CatalogName;
                txtEndDate.Text = _result.EndDate.ToString(cstdateformat);
                txtStartDate.Text = _result.StartDate.ToString(cstdateformat);
                
                this.Refresh();
                if (result.IsError)
                {
                    LblQueryInfo.Text = "Error:";
                    txtQueryInfo.Text = _result.Error;
                    txtQueryInfo.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    LblQueryInfo.Text = "Query Time:";
                    txtQueryInfo.Text = _result.QueryTimeFormated;
                }

                DrawGrids();
                OnPropertyListChanged(EventArgs.Empty);
            }
            else
            {
                
                OnPropertyListChanged(EventArgs.Empty);
            }
        }

        private void DrawGrids()
        {
            try
            {
                Stopwatch stopwatch = null;
                string GridLogInfo = string.Empty;
                string sGrid = "";
                VirtualDataGridView.ICellSetDataGridView vdataGrid = CurrentDataGridView(ref sGrid);
                if (_result != null)
                {
                    if (!_result.IsError)
                    {
                        /*if (CellProperty == CellPropertyDataTable)
                        {
                           vdataGrid = gridDataTable;

                           sGrid = GridName + STR_GridDataTable;

                            //if (gridDataTable_old.DataSource == null)
                            //{
                            //    CellSet cs = GetGridCellSet(ref stopwatch, ref GridLogInfo, sGrid, _result);

                            //    CellSetDataTable dt = Utils.Cellset2Datatable(cs);
                            //    if (dt.IsCellError)
                            //        SetResultCellErrorFlag(true);
                            //    gridDataTable_old.DataSource = dt;
                            //    LogEndGridDraw(stopwatch, GridLogInfo, sGrid);
                            //}
                            //else
                            //    RaiseDataGridDataChanged(string.Format("Redraw {0} Grid", sGrid));
                        }
                        else
                        {
                            vdataGrid = cellSetDataGridView1;
                            sGrid = GridName + STR_GridCellData;

                            //if (!cellSetDataGridView1.DataSourceValid)
                            //{
                            //    CellSet cs = GetGridCellSet(ref stopwatch, ref GridLogInfo, sGrid, _result);
                            //    cellSetDataGridView1.DrawCellSetInGrid(cs);
                            //    SetResultCellErrorFlag(cellSetDataGridView1.IsCellError);
                            //    LogEndGridDraw(stopwatch, GridLogInfo, sGrid);
                            //}
                            //else
                            //{
                            //    string cellProperty = CellProperty;
                            //    if (cellProperty == null)
                            //        cellProperty = "FORMATED_VALUE";
                            //    RaiseDataGridDataChanged(string.Format("Redraw {0} Grid: {1}", sGrid, cellProperty));
                            //}
                        }*/

                        if (!vdataGrid.DataSourceValid)
                        {
                            CellSet cs = GetGridCellSet(ref stopwatch, ref GridLogInfo, sGrid, _result);
                            vdataGrid.DrawCellSetInGrid(cs);
                            SetResultCellErrorFlag(vdataGrid.IsCellError);
                            LogEndGridDraw(stopwatch, GridLogInfo, sGrid);
                        }
                        else
                        {
                            string cellProperty = CellProperty;

                            if (cellProperty == null && cellProperty != CellPropertyDataTable)
                                cellProperty = "FORMATED_VALUE";
                            RaiseDataGridDataChanged(string.Format("Redraw {0} Grid: {1}", sGrid, cellProperty));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in Setting Grid Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private VirtualDataGridView.ICellSetDataGridView CurrentDataGridView(ref string sGrid)
        {
            VirtualDataGridView.ICellSetDataGridView vdataGrid;
            if (CellProperty == CellPropertyDataTable)
            {
                vdataGrid = gridDataTable;

                sGrid = GridName + STR_GridDataTable;
            }
            else
            {
                vdataGrid = cellSetDataGridView1;
                sGrid = GridName + STR_GridCellData;
            }
            return vdataGrid;
        }

        
        private CellSet GetGridCellSet(ref Stopwatch stopwatch, ref string GridLogInfo, string sGrid, TestResult _result)
        {
            if (stopwatch == null)
                stopwatch = new Stopwatch();
            // Begin timing
            stopwatch.Start();
            CellSet cs = null;
            try
            {
                cs = _result.cellSet;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Getting CellSet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            int rows = Utils.CellSetRows(cs);
            int columns = Utils.CellSetColumns(cs);

            GridLogInfo = String.Format("with {0} rows and {1} columns", rows, columns);
            RaiseDataGridDataChanged(String.Format("Loading {0} Grid {1}", sGrid, GridLogInfo));
            return cs;
        }
        private void LogEndGridDraw(Stopwatch stopwatch, string GridLogInfo, string sGrid)
        {
            stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
            RaiseDataGridDataChanged(String.Format("Loaded {0} Grid {1} in {2}ms", sGrid, GridLogInfo, stopwatch.ElapsedMilliseconds));

        }

        //Stopwatch stopwatch = new Stopwatch();

                    // Begin timing
                    //stopwatch.Start();
                    //string sGrid = "Results";
                    
                    //string Gridsize="";
                    
        //CellSet cs = r.cellSet;
                        //try
                        //{

                        //    int rows = Utils.CellSetRows(cs);
                        //    int columns = Utils.CellSetColumns(cs);
                        //    cs = null;
                        //    Gridsize = String.Format("with {0} rows and {1} columns", rows, columns);
                        //}
                        //catch (Exception ex)
                        //{
                        //    Gridsize = "... Unknown Size " + ex.Message;
                        //}
                        //SetStatusMessage(String.Format("Loading {0} Grid {1}", sGrid, Gridsize));
                    
                    
        public List<string> CellPropertiesList
        {
            get
            {
                List<string> result = cellSetDataGridView1.CellPropertiesList;
                if (result.Count!=0) 
                    result.Add(CellPropertyDataTable);

                return result;
            }
        }

        public bool AllowUserToAddRows
        {
            get
            {
                return true;
            }
            set
            {
            }
        }
        public bool AllowUserToDeleteRows
        {
            get
            {
                return true;
            }
            set
            {
            }
        }
        public DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode
        {
            get
            {
                return DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            }
            set
            {
            }
        }

        private void btnViewMdx_Click(object sender, EventArgs e)
        {
            if (_result == null)
            {
                MessageBox.Show("No Result available/Loaded");
            }
            else
            {
                frmEditMDX f = new frmEditMDX() { Mdx =  _result.MDX,
                ReadOnly=true};
                f.Show();
            }
        }

        public string CellProperty
        {
            get
            {
                if (gridDataTable.Visible)
                    return CellPropertyDataTable;
                else
                    return cellSetDataGridView1.CellProperty;
            }
            set
            {
                if (value != CellPropertyDataTable)
                    cellSetDataGridView1.CellProperty = value;

                cellSetDataGridView1.Visible = !(value == CellPropertyDataTable);
                gridDataTable.Visible = (value == CellPropertyDataTable);
                DrawGrids();
            }
        }

        internal void CellEnterEventEnabled(bool enabled)
        {
            if (enabled)
            {
                cellSetDataGridView1.DataGrid_CellEnterEvent += RaiseEventCellEnter;
                gridDataTable.DataGrid_CellEnterEvent += RaiseEventCellEnter;
            }
            else
            {
                cellSetDataGridView1.DataGrid_CellEnterEvent -= RaiseEventCellEnter;
                gridDataTable.DataGrid_CellEnterEvent -= RaiseEventCellEnter;
            }
        }

        private void btnShowDataElement_Click(object sender, EventArgs e)
        {
            try
            {
                if (_result == null)
                {
                    MessageBox.Show("No Result available/Loaded");
                }
                else
                {
                    if (_result.Data == string.Empty || _result.Data == null)
                    {
                        MessageBox.Show("Data Element is empty, nothing to show");
                    }
                    else
                    {
                        frmXMLViewer f = new frmXMLViewer();
                        f.Text = string.Format("XML Data viewer for Test: {0}", _result.ToString());
                        f.Show();
                        f.Xml = _result.Data; 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading Data into Browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //testing only
            /*try
            {
                Form2 f = new Form2();
                f.Show();
                if (_result != null)
                    f.cellset = _result.cellSet;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading Data into simple Grid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
             */
        }

      
    }
}
