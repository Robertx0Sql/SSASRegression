using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Microsoft.AnalysisServices.AdomdClient;
using SSASRegressionCL;


namespace SSASRegressionUI
{
    public partial class frmResults : Form
    {
        public frmResults()
        {
            InitializeComponent();
            dataGridViewCellSet1.PropertyListChanged += toolStripInitShowCellPropList;
            dataGridViewCellSet1.DataGridDataChanged += new EventHandler<TextArgs>(DataGridDataChanged);
            InitShowCellPropList();            
        }
        TestResults _testResults;
       
        private List<TestResult> _ResultList;
        private string _ResultsFilePath;
        public string ResultsFilePath
        {
            get
            {
                return _ResultsFilePath;
            }
            set
            {
                _ResultsFilePath = value;
                dataGridViewCellSet1.FileName = value;
                LoadData();
            }
        }

        private void LoadData()
        {
            try
            {
                _testResults = new TestResults();
                _testResults.LoadFile(_ResultsFilePath);
                _ResultList = _testResults.listOfResults;


                /*
                    r.Server
                    r.CatalogName
                    r.StartDate 
                    r.EndDate 
                */
                toolStripComboBoxResults.Items.Clear();
                testsToolStripButton.DropDownItems.Clear();
                
                foreach (TestResult r in _ResultList)
                {
                    toolStripComboBoxResults.Items.Add(r.ToString());
                    testsToolStripButton.DropDownItems.Add(CreateTestResultMenuItem(r.ID, r.ToString()));
                }
                //show the 1st item in the lists
                if (toolStripComboBoxResults.Items.Count != 0)
                {
                    toolStripComboBoxResults.SelectedIndex = 0;
                }
                if (!_testResults.ValidResultFile)
                    MessageBox.Show("No Results in File. \nHave You loaded a Test File?", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
             //   MessageBox.Show(ex.Message);
                MessageBox.Show(ex.Message, "Error Loading Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private ToolStripMenuItem CreateTestResultMenuItem(string id, string Description )
        {
            ToolStripMenuItem item = new ToolStripMenuItem() { Name = Guid.NewGuid().ToString(), Tag = id, Text = Description };
            item.Click += MenuItemClickHandler;

            return item;
        }
   
        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            // Take some action based on the data in clickedItem
            //remove checks from existing items
            
            //clickedItem.Checked = true;
            toolStripComboBoxResults.SelectedIndex = toolStripComboBoxResults.FindStringExact(clickedItem.Text);

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
            List<string> CellPropertiesList = dataGridViewCellSet1.CellPropertiesList;
            
            foreach (string item in CellPropertiesList)
            {
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(item);
                toolStripMenuItem.Click += toolStripMenuShow_Click;
                toolStripDropDownButtonShowCellProp.DropDownItems.Add(toolStripMenuItem);
            }
            SetCurrentCellPropertyChecked();
        }

        private void SetCurrentCellPropertyChecked()
        {
            string CurrentCellProperty = dataGridViewCellSet1.CellProperty;
            if (CurrentCellProperty == null)
                CurrentCellProperty = "FORMATTED_VALUE";
            foreach (ToolStripMenuItem item in toolStripDropDownButtonShowCellProp.DropDownItems)
            {
                item.Checked = (item.Text == CurrentCellProperty);
                
            }
        }

        private void toolStripMenuShow_Click(object sender, EventArgs e)
        {
            dataGridViewCellSet1.CellProperty = (sender as ToolStripMenuItem).Text;
            SetCurrentCellPropertyChecked();
        }





        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

   





        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (toolStripComboBoxResults.SelectedIndex >= 0)
                {
                    string key = toolStripComboBoxResults.Items[toolStripComboBoxResults.SelectedIndex].ToString();

                   
                    this.Text = "Results " + key;
                    
                    txtTestDescription.Text = "";
                    txtTestID.Text = "";
                    DataGridViewCellSetAdvanced _dgwleft = dataGridViewCellSet1;
                    _dgwleft.result = null;
                    
                    //Stopwatch stopwatch = new Stopwatch();

                    // Begin timing
                    //stopwatch.Start();
                    //string sGrid = "Results";
                    
                    //string Gridsize="";
                    TestResult r = _ResultList.Find(x => x.ToString() == key);
                    if (r != null)
                    {
                        txtTestDescription.Text = r.Description;
                        txtTestID.Text = r.ID;
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
                        if (r.isResultExecuted)
                            _dgwleft.result = r;
                        else
                            SetStatusMessage(string.Format("{0} is not a valid Result", r.ToString()));
                    }
                   
                    //stopwatch.Stop();
                    //Console.WriteLine(stopwatch.ElapsedMilliseconds);
                    //SetStatusMessage(String.Format("Loaded {0} Grid {1} in {2}ms", sGrid, Gridsize, stopwatch.ElapsedMilliseconds));
                    

                    
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Loading Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void toolStripComboBox1_DropDown(object sender, EventArgs e)
        {
            try
            {
                ToolStripComboBox senderToolStripComboBox = (ToolStripComboBox)sender;
                ComboBoxUtils.ResizeComboBoxDropDown((ComboBox)senderToolStripComboBox.ComboBox);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Loading Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            DataTable QueryStats = _testResults.ResultStatistics();
            FrmQueryStats f = new FrmQueryStats() { QueryStats = QueryStats };
            f.Show();
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

        private void openToolStripButton_Click(object sender, EventArgs e)
        {

            FileOpen();
        }

        private void FileOpen()
        {
            try
            {
                string filepath = null;
                if (FileUtil.GetOpenFilePath((string)"1st Results", ref filepath))



                    if (Utils.ValidateExistingResultFilePath(filepath))
                    {
                        this.ResultsFilePath = filepath;

                    }
                //else
                //    throw new FileNotFoundException(string.Format("Results file not specified/found"));
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error Showing Results", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileOpen();
        }

    }
}
