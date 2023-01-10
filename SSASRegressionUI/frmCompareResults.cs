using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSASRegressionCL;
using System.Threading;
using System.Data;
using Microsoft.AnalysisServices.AdomdClient;


namespace SSASRegressionUI
{
    public partial class frmCompareResults : Form, IfrmCompareResults
    {
        public frmCompareResults()
        {
            InitializeComponent();
            dataGridViewCellSetLeft.PropertyListChanged += toolStripInitShowCellPropList;
            dataGridViewCellSetLeft.DataGrid_CellEnterEvent += DataGridCellEnter;
            dataGridViewCellSetRight.DataGrid_CellEnterEvent += DataGridCellEnter;
            dataGridViewCellSetRight.GridName = "Right";
            dataGridViewCellSetLeft.GridName = "Left";
            
            SetupToolStripComboBoxDrawItemEventHandler((ComboBox)toolStripComboBoxResults.Control);
            //this.toolStripComboBox1.ComboBox.DrawItem += new DrawItemEventHandler(comboBox1_DrawItem);
            dataGridViewCellSetRight.DataGridDataChanged += new EventHandler<TextArgs>(DataGridDataChanged);
            dataGridViewCellSetLeft.DataGridDataChanged += new EventHandler<TextArgs>(DataGridDataChanged);
            InitShowCellPropList(); 
            SetStatusMessage(string.Empty);

        }


        private void SetupToolStripComboBoxDrawItemEventHandler(ComboBox box)
        {
            box.DrawMode = DrawMode.OwnerDrawVariable;
            // box.MeasureItem += new MeasureItemEventHandler(box_MeasureItem);
            box.DrawItem += ComboBoxResults_DrawItem; //DrawItemEventHandler(ComboBoxResults_DrawItem);
        }

        private void DataGridCellEnter(object sender, DataGridViewCellEventArgs e)
        {
           // DataGridViewCellSetAdvanced dg = sender as DataGridViewCellSetAdvanced;

            SetStatusMessage(string.Format("Left Grid ({0},{1}) ; Right Grid ({2},{3}) ", dataGridViewCellSetLeft.ColumnIndex, dataGridViewCellSetLeft.RowIndex, dataGridViewCellSetRight.ColumnIndex, dataGridViewCellSetRight.RowIndex));
        }

        private ResultsComparator _resultsComparator;

        public ResultsComparator resultsComparator
        {
            get { return _resultsComparator; }
            set { _resultsComparator = value;
                LoadData();
            }
        }

        Dictionary<string, string> ResultItemsMapping = new Dictionary<string, string>();
       

        private void LoadData()
        {
            try
            { 
                toolStripComboBoxResults.Items.Clear();
                //testsToolStripMenu.DropDownItems.Clear();

                testspassToolStripMenuItem.DropDownItems.Clear();
                testsfailToolStripMenuItem.DropDownItems.Clear();
                testsuniqueToolStripMenuItem.DropDownItems.Clear();


                foreach (string id in _resultsComparator.ComparedResults.Keys)
                {
                    string CompareTypeCode = "-";
                    string TestDescription = string.Empty;
                    CompareData cd;
                    if (_resultsComparator.ComparedResults.TryGetValue(id, out cd))
                    {
                        TestDescription = cd.ToString();

                        switch (cd.CompareResult)
                        {
                            case CompareResultType.Pass:
                                CompareTypeCode = "P";
                                testspassToolStripMenuItem.DropDownItems.Add(CreateTestResultMenuItem(id , TestDescription, CompareTypeCode));
                                break;
                            case CompareResultType.Fail:
                                CompareTypeCode = "F";
                                testsfailToolStripMenuItem.DropDownItems.Add(CreateTestResultMenuItem(id, TestDescription, CompareTypeCode));
                                break;
                            case CompareResultType.Untested:
                                CompareTypeCode = "?";
                                testsuniqueToolStripMenuItem.DropDownItems.Add(CreateTestResultMenuItem(id, TestDescription, CompareTypeCode));
                                break;
                        }
                    }
                    else
                    {
                        TestDescription = id + " Description not Found";
                        testsuniqueToolStripMenuItem.DropDownItems.Add(CreateTestResultMenuItem(id, TestDescription, CompareTypeCode));
                    }
                    string ItemText = ResultItemText(TestDescription, CompareTypeCode);
                    toolStripComboBoxResults.Items.Add(ItemText);

                    ResultItemsMapping.Add(ItemText, id);
                }
                //show the 1st item in the list
                if (toolStripComboBoxResults.Items.Count != 0)
                {
                    toolStripComboBoxResults.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string ResultItemText(string TestDescription, string CompareTypeCode)
        {
            return String.Format("{0} {1}", TestDescription, CompareTypeCode);
        }

      



        private ToolStripMenuItem CreateTestResultMenuItem(string id,string Description, string CompareTypeCode)
        {
            ToolStripMenuItem item = new ToolStripMenuItem() { Name = Guid.NewGuid().ToString(), Tag = id, Text = ResultItemText(Description, CompareTypeCode) };
            item.Click += MenuItemClickHandler;
            
            return item;
        }

        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            // Take some action based on the data in clickedItem
            //remove checks from existing items
            RemoveCheckedItems(testspassToolStripMenuItem.DropDownItems);
            RemoveCheckedItems(testsfailToolStripMenuItem.DropDownItems);
            RemoveCheckedItems(testsuniqueToolStripMenuItem.DropDownItems);
            clickedItem.Checked = true;
            toolStripComboBoxResults.SelectedIndex = toolStripComboBoxResults.FindStringExact(clickedItem.Text);
           
        }
        private void SetCurrentCellPropertyChecked()
        {
            string CurrentCellProperty = dataGridViewCellSetLeft.CellProperty;
            if (CurrentCellProperty == null)
                CurrentCellProperty = "FORMATTED_VALUE";
            foreach (ToolStripMenuItem item in toolStripDropDownButtonShowCellProp.DropDownItems)
            {
                item.Checked = (item.Text == CurrentCellProperty);

            }
        }
        private static void RemoveCheckedItems(ToolStripItemCollection toolStripItemCollection)
        {
            foreach (ToolStripMenuItem MenuItem in toolStripItemCollection)
            {
                MenuItem.Checked = false;
            }   
        }
        private void DataGridDataChanged(object sender, TextArgs e)
        {
            SetStatusMessage(e.Message);
        }

        void toolStripInitShowCellPropList(object sender, EventArgs e)
        {
            InitShowCellPropList();
        }


        internal void InitShowCellPropList()
        {
            toolStripDropDownButtonShowCellProp.DropDownItems.Clear();
            List<string> CellPropertiesList = dataGridViewCellSetLeft.CellPropertiesList;
            foreach (string item in CellPropertiesList)
            {
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(item);
                toolStripMenuItem.Click += toolStripMenuShow_Click;
                toolStripDropDownButtonShowCellProp.DropDownItems.Add(toolStripMenuItem);
            }
            SetCurrentCellPropertyChecked();
        }
        //   static bool SuspendGrid = false;
        private void toolStripMenuShow_Click(object sender, EventArgs e)
        {
            dataGridViewCellSetLeft.CellProperty = (sender as ToolStripMenuItem).Text;
            dataGridViewCellSetRight.CellProperty = (sender as ToolStripMenuItem).Text;
            SetCurrentCellPropertyChecked();
        }

        
        private void ShowCompareTestResult(CompareData cd)
        {
            try
            {

                //SuspendGrid = true;

                dataGridViewCellSetLeft.CellEnterEventEnabled(false);
                dataGridViewCellSetRight.CellEnterEventEnabled(false);
                dataGridViewCellSetLeft.result = null;
                dataGridViewCellSetRight.result = null;
                dataGridViewCellSetLeft.Refresh();
                dataGridViewCellSetRight.Refresh();
                SetStatusMessage("Load Compare Results");
                listView1.Items.Clear();


                Color bk = SystemColors.Control;

                switch (cd.CompareResult)
                {
                    case CompareResultType.Pass:
                        // txtCompareErrors.Text = "<NONE> TEST PASSED ";
                        bk = Color.Green;
                        break;
                    case CompareResultType.Fail:
                        bk = Color.Red;
                        //SetStatusMessage("Loaded " + errors.ToString() + " Compare Errors ");
                        break;
                    case CompareResultType.Untested:
                        bk = SystemColors.Control;
                        break;
                }
              splitContainer2.Panel1.BackColor = bk;

              txtTestDescription.Text = cd.Description;
              txtTestID.Text = cd.ID;


                Thread thread = new Thread( LoadCurrentCompare );
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start(cd);

                EnableControls(false);
                btnCompareResultstoClipBoard.Enabled = true;
                //SetStatusMessage("Compare Loaded ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        static int ActiveBackgroundThreads = 0;

        void LoadCurrentCompare(object data)
        {
            CompareData cd = data as CompareData;
            if (cd != null)
            {
                //code goes here
                ActiveBackgroundThreads = 0;

                //CompareData cd = GetCurrentCompareData();
               // string FileName = "";
                ActiveBackgroundThreads++;
                SetdataGridViewCellSetResultDelegate dgl = SetdataGridViewCellSetResult;
                dgl.BeginInvoke(dataGridViewCellSetLeft, cd.resultLeft, "Left", _resultsComparator.FileNameLeft , null, null);

                ActiveBackgroundThreads++;
                SetdataGridViewCellSetResultDelegate dgr = SetdataGridViewCellSetResult;
                dgr.BeginInvoke(dataGridViewCellSetRight, cd.resultRight, "Right", _resultsComparator.FileNameRight, null, null);

                ActiveBackgroundThreads++;
                ShowCompareErrorsDelegate se = ShowCompareErrors;
                se.BeginInvoke(cd, null, null);
            }
        }
        void EnableControls( bool enabled )
        {
            toolStrip1.Enabled = enabled;
            splitContainer2.Enabled = enabled;
        }
        void ReleaseSemaphore()
        {
            try
            {
                
                ActiveBackgroundThreads--;

                if (ActiveBackgroundThreads <= 0)
                {
                    ActiveBackgroundThreads = 0;
                    EnableControls(true);
                    dataGridViewCellSetLeft.CellEnterEventEnabled(true);
                    dataGridViewCellSetRight.CellEnterEventEnabled(true);
                    SetStatusMessage("Compare Loaded ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //  RaiseError(string.Format("Error in Parallel Testing : {0}", ex.Message));
            }
        }

        public delegate void SetdataGridViewCellSetResultDelegate(DataGridViewCellSetAdvanced _dataGridViewCellSet, TestResult result, string sGrid, string FileName);
        public delegate void ShowCompareErrorsDelegate(CompareData cd);

        private void SetdataGridViewCellSetResult(DataGridViewCellSetAdvanced _dataGridViewCellSet, TestResult result, string sGrid, string FileName)
        {
            if (_dataGridViewCellSet.InvokeRequired)
            {
                try
                {
                    SetdataGridViewCellSetResultDelegate d = SetdataGridViewCellSetResult;
                    Invoke(d, new object[] { _dataGridViewCellSet, result, sGrid, FileName });
                }
                catch
                {
                    ReleaseSemaphore();
                }
                finally
                {
                }
            }
            else
            {
                try
                {
                    if (result != null)
                    {
                       /* if (!result.IsError)
                        {
                            try
                            {
                                CellSet cs = result.cellSet;

                                int rows = Utils.CellSetRows(cs);
                                int columns = Utils.CellSetColumns(cs);
                                cs = null;
                                //int rows = Utils.CellSetRows(result.cellSet);
                                //int columns = Utils.CellSetColumns(result.cellSet);
                                SetStatusMessage(String.Format("Loading {0} Grid with {1} rows and {2} columns", sGrid, rows, columns));
                            }
                            catch (Exception ex)
                            {
                                SetStatusMessage(String.Format("Loading {0} Grid... Unknown Size", sGrid));
                            }
                        }else
                        {
                            SetStatusMessage(String.Format("Loading {0} Grid... Unknown Size", sGrid));
                        }*/
                        _dataGridViewCellSet.FileName = FileName; 
                        _dataGridViewCellSet.result = result;
                        
                    }
                }
                catch
                {
                }
                finally
                {
                    ReleaseSemaphore();
                }
            }
        }
        private void ShowCompareErrors(CompareData cd)
        {
            if (listView1.InvokeRequired)
            {
                try
                {
                    ShowCompareErrorsDelegate d = ShowCompareErrors;
                    Invoke(d, new object[] { cd });
                }
                catch
                {
                    ReleaseSemaphore();
                }
            }
            else
            {
                try
                {
                    listView1.Items.Clear();
                    int errors = cd.CompareErrors.Count;
                    foreach (ComparisonDifference item in cd.CompareErrors)
                    {
                        ListViewItem lvitem = new ListViewItem(new[] { item.Description, item.Side.ToString() }) { Tag = item };
                        listView1.Items.Add(lvitem);
                        if (listView1.Items.Count % 100 == 0)
                            SetStatusMessage(String.Format("Loaded {0} of {1} Compare Errors ", listView1.Items.Count, errors));
                        if (listView1.Items.Count >= 10000) // only load the 1st 10K errors

                        {
                            SetStatusMessage(String.Format("Loaded FIRST {0} of {1} Compare Errors ", listView1.Items.Count, errors));
                           break; 
                        }
                    }
                }
                catch
                {
                }
                finally
                {
                    ReleaseSemaphore();
                }
            }
        }


        delegate void SetTextCallback(string message);
        private void SetStatusMessage(string message)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (statusStrip1.InvokeRequired)
            {
                SetTextCallback d = SetStatusMessage;
                Invoke(d, new object[] { message });
            }
            else
            {
                toolStripStatusLabel1.Text = message;
                statusStrip1.Refresh();
            }
        }


        private void SetItemTemplateControlSize()
        {
            SplitContainer _drobj = splitContainer1;
            _drobj.SplitterDistance = _drobj.Width / 2;
        }





        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (toolStripComboBoxResults.SelectedIndex >= 0)
                {
                    CompareData cd = GetCurrentCompareData();
                    ShowCompareTestResult(cd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private CompareData GetCurrentCompareData()
        {
            CompareData cd = null;

            try
            {

                
                string ItemText = toolStripComboBoxResults.Items[toolStripComboBoxResults.SelectedIndex].ToString();
                string id;
                if (ResultItemsMapping.TryGetValue(ItemText, out id))
                {
                    //if (key.Length > 1)
                    //    key = key.Substring(0, key.Length - 2).Trim(); //Remove Pass/Fail tag

                    Text = "Compare Results " + ItemText;

                    _resultsComparator.ComparedResults.TryGetValue(id, out cd);
                }
            }
            catch(Exception ex)
            {
                //do nothing
                Console.WriteLine(ex.Message);
            }

            return cd;
        }
        private void ComboBoxResults_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the background 
            e.DrawBackground();

            // Get the item text    
            string text = ((ComboBox)sender).Items[e.Index].ToString();
            // Determine the forecolor based on whether or not the item is selected    
            Brush brush;
            
            if (text.EndsWith("P", StringComparison.InvariantCultureIgnoreCase))// compare  date with your list.  
            {
                brush = Brushes.Green;
            }
            else
            {
                if (text.EndsWith("?", StringComparison.InvariantCultureIgnoreCase))// compare  date with your list.  
                    brush = Brushes.Navy;
                else
                brush = Brushes.Red;
            }

            // Draw the text    
            e.Graphics.DrawString(text, ((Control)sender).Font, brush, e.Bounds.X, e.Bounds.Y);
        }


        private void toolStripComboBoxResults_DropDown(object sender, EventArgs e)
        {
            try
            {
               ToolStripComboBox senderToolStripComboBox = (ToolStripComboBox)sender;
               ComboBoxUtils.ResizeComboBoxDropDown( (ComboBox)senderToolStripComboBox.ComboBox);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmCompareResults_Resize(object sender, EventArgs e)
        {
            try
            {
                SetItemTemplateControlSize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            showCurrentCompareError();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            showCurrentCompareError();
        }
        void showCurrentCompareError()
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    ListViewItem lvi = listView1.SelectedItems[0];
                    ComparisonDifference ce = lvi.Tag as ComparisonDifference;
                    if (ce != null)
                    {
                        int rowindex = ce.RowIndex;
                        int columnindex = ce.ColumnIndex;
                        if (rowindex >= 0 && columnindex >= 0)
                        {
                            switch (ce.Side)
                            {
                                case ComparisonDifference.CompareSide.Left:
                                    dataGridViewCellSetLeft.HighlightCell(rowindex, columnindex);
                                    dataGridViewCellSetRight.HighlightCell(-1, -1);
                                    break;
                                case ComparisonDifference.CompareSide.Right:
                                    dataGridViewCellSetRight.HighlightCell(rowindex, columnindex);
                                    dataGridViewCellSetLeft.HighlightCell(-1, -1);
                                    break;
                                case ComparisonDifference.CompareSide.Both:
                                    dataGridViewCellSetLeft.HighlightCell(rowindex, columnindex);
                                    dataGridViewCellSetRight.HighlightCell(rowindex, columnindex);
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetStatusMessage(ex.Message);
            }
        }

        private void btnCompareResultstoClipBoard_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                CompareData cd = GetCurrentCompareData();
                if (cd != null)
                {
                    foreach (ComparisonDifference item in cd.CompareErrors)
                    {
                        sb.AppendLine(item.Description);
                    }
                    Clipboard.SetDataObject(sb.ToString());
                    SetStatusMessage(string.Format("Copied '{0}' Compare Errors to ClipBoard ", cd.Description));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Copy Compare Errors to ClipBoard " + ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int count = (int)(timer1.Tag) ;
            count++;
            if (count > 10)
                count = 1;
            timer1.Tag = count;
            SetStatusMessage("Loading Compare " + new String('.', count) );
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            DataTable QueryStats = _resultsComparator.ResultStatistics();
            FrmQueryStats f = new FrmQueryStats() { QueryStats = QueryStats };
            f.Show();
        }
    }
}
