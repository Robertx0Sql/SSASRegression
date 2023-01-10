using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using SSASRegressionCL;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace SSASRegressionUI
{
    public partial class frmMain : Form
    {
        private BackgroundWorker bw = new BackgroundWorker();
        public frmMain()
        {
            InitializeComponent();

            
           this.txtConnectionString.Text= string.Format("Provider={0};data source={1};Initial Catalog={2};", "MSOLAP.4", "localhost", "");
           StringCollection cboServerValues = Properties.Settings.Default.cboServerCollection;
           if (cboServerValues != null)
               foreach (var item in cboServerValues)
               {
                   cboServer.Items.Add(item);
               }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadWindowUserSettings();

            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.btnResultsView1, "View Results");
            ToolTip1.SetToolTip(this.btnResultsView2, "View Results");
            ToolTip1.SetToolTip(this.btnResultsViewLatest, "View Results of Test");

            ToolTip1.SetToolTip(this.btnTestFilePath, "Open Test file");

            ToolTip1.SetToolTip(this.btnCompareResultFilePath1, "Open Results of Test for compare");
            ToolTip1.SetToolTip(this.btnCompareResultFilePath2, "Open Results of Test for compare");
            ToolTip1.SetToolTip(this.btnResultFilePath, "Result File Path");
            ToolTip1.SetToolTip(this.txtEffectiveUserName, @"Use when an end user identity must be impersonated on the server. Specify the account in a domain\user format. \r\nTo use this property, the caller must have administrative permissions in Analysis Services");

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.Cancel)
            {

                if (WindowState == FormWindowState.Maximized)
                {
                    Properties.Settings.Default.frmMain_Location = RestoreBounds.Location;
                    Properties.Settings.Default.frmMain_Size = RestoreBounds.Size;
                    Properties.Settings.Default.frmMain_Maximised = true;

                }
                else
                {
                    Properties.Settings.Default.frmMain_Location = Location;
                    Properties.Settings.Default.frmMain_Size = Size;
                    Properties.Settings.Default.frmMain_Maximised = false;

                }
                Properties.Settings.Default.Save();
            }

        }
        private void SetNewResultFilePathWhenBlank()
        {
            if (string.IsNullOrEmpty(txtResultFilePath.Text) && File.Exists(txtTestFilePath.Text))
            {
                string server =cboServer.Text;
                string catalog = cboCatalogs.Text;
                if (rbConnectionString.Checked)
                {
                    Dictionary<string, string> connProperties = SSASRegressionCL.Utils.ConnectionStringProperties(this.txtConnectionString.Text);
                    catalog = "";
                    connProperties.TryGetValue("DataSource".ToLower(),out server);
                    connProperties.TryGetValue("InitialCatalog".ToLower(), out catalog);
                    if (catalog =="")
                        connProperties.TryGetValue("Catalog".ToLower(), out catalog);
                }

                txtResultFilePath.Text = String.Format(@"{0}\{1}_{2}_{3}_{4:yyyyMMddHHmm}", Path.GetDirectoryName(txtTestFilePath.Text), Path.GetFileNameWithoutExtension(txtTestFilePath.Text), server.Replace( "\\" , "-" ), catalog.Replace("\\", "-"), DateTime.UtcNow);
            }
        }
    
        private void btnRunTests_Click(object sender, EventArgs e)
        {
            RunTests();
        }

        private void EditTestFile()
        {
            try
            {
                string testFile = txtTestFilePath.Text;
                if ((testFile).Trim().Length == 0)
                {
                    if (GetTestFile())
                        EditTestFile();
                    
                }
                else
                    if (!File.Exists(testFile))
                        throw new FileNotFoundException(string.Format("The Test File '{0}' does not exist", testFile));
                    else
                    {
                        ShowTestEditor(testFile);

                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnEditTestFile_Click(object sender, EventArgs e)
        {
            EditTestFile();
        }

        private static void ShowTestEditor(string testFile)
        {
            frmEditTests2 f = ShowTestEditor();
            f.LoadTestsFile(testFile);
        }
        private static  frmEditTests2 ShowTestEditor()
        {
            frmEditTests2 f = new frmEditTests2();
            f.Show();
            return f;
        }
        private static void CreateNewTests()
        {
            try
            {
                ShowTestEditor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCreateNewTests_Click(object sender, EventArgs e)
        {
            CreateNewTests();
        }

        private void btnCompareResultFilePath1_Click(object sender, EventArgs e)
        {
            try
            {
                string filepath = null;
                if (FileUtil.GetOpenFilePath((string)"1st Results", ref filepath))
                    txtCompareResultFile1.Text = filepath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCompareResultFilePath2_Click(object sender, EventArgs e)
        {
            try
            {
                string filepath = null;
                if (FileUtil.GetOpenFilePath((string)"2nd Results", ref filepath))
                    txtCompareResultFile2.Text = filepath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Applying EventHandler
        public void OnResultProcess(object sender, ResultEventArgs ee)
        {
            SetStatusMessage(ee.message,ee.result);
        }
   
        private void SetStatusMessage(string message)
        {
           SetStatusMessage(message, ResultEventArgs.EventResult.Information);
        }
        delegate void SetTextCallback(string message, ResultEventArgs.EventResult eventResult);
        private void SetStatusMessage(string message, ResultEventArgs.EventResult eventResult)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (statusStrip1.InvokeRequired)
            {
                SetTextCallback d = SetStatusMessage;
                Invoke(d, new object[] { message, eventResult });
            }
            else
            {
                ListViewItem item = new ListViewItem(new[] { DateTime.Now.ToShortTimeString(), message });
                listView1.Items.Add(item);
                listView1.EnsureVisible(listView1.Items.Count-1);
                listView1.Refresh();
                if (eventResult != ResultEventArgs.EventResult.Error)
                {
                    toolStripStatusLabel1.Text = message;
                    statusStrip1.Refresh();
                }
            }

        }



        
        
        private void btnConnectServer_Click(object sender, EventArgs e)
        {
            ConnectToServer();
        }

        private void ConnectToServer()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                    if (LoadCatalogs())
                    {
                        if (!cboServer.Items.Contains(cboServer.Text))
                        {
                            //Add new Server to the Item List
                            cboServer.Items.Add(cboServer.Text);
                            // Add to Settings
                            SaveServerListtoSettings(cboServer.Text);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private static void SaveServerListtoSettings(string newServer)
        {
            if (!Properties.Settings.Default.cboServerCollection.Contains (newServer))
                Properties.Settings.Default.cboServerCollection.Add(newServer);
            Properties.Settings.Default.Save();
        }

        string _Server
        {
            get
            {
                return cboServer.Text;
            }
        }

        private bool LoadCatalogs()
        {
            try
            {
                MdxQuery q = new MdxQuery(_Server, "");

                using (DataTable dt = q.GetCatalogs())
                {
                    cboCatalogs.Items.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        string sCatalogName = row[0].ToString();
                        cboCatalogs.Items.Add(sCatalogName);
                    }
                }

                return true;
            }
            catch (Exception ex){
                throw new Exception("Error Loading Catalog List. "+ ex.Message, ex);
            }
        }

        private bool GetTestFile()
        {
            string filepath = null;
            if (FileUtil.GetOpenFilePath((string)"Tests", ref filepath))
            {
                txtTestFilePath.Text = filepath;
                return true;
            }
            else
                return false;

        }
        private void btnTestFilePath_Click(object sender, EventArgs e)
        {
            GetTestFile();
        }



        private void btnResultFilePath_Click(object sender, EventArgs e)
        {
            
            try
            { //Save Dialog 

                SetNewResultFilePathWhenBlank();

                SaveFileDialog saveFileDialog1 = new SaveFileDialog() { FileName = txtResultFilePath.Text, Filter = "xml file (*.xml)|*.xml", Title = "Save Results" };
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    txtResultFilePath.Text = saveFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboCatalogs_DropDown(object sender, EventArgs e)
        {
            try
            {
                ComboBoxUtils.ResizeComboBoxDropDown((ComboBox)sender);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        

    
        private void RunTests()
        {
            try
            {
                SetNewResultFilePathWhenBlank();
                int MaxParallel = (int)numParallelQueries.Value;

                string testFile = txtTestFilePath.Text;
                string resultsFile = txtResultFilePath.Text;

                    string Server = _Server;
                    string CatalogName = cboCatalogs.Text;
                    string EffectiveUserName = txtEffectiveUserName.Text;
                    string ConnectionString = txtConnectionString.Text;

                
                RunTestsDelegate rt = RunTests;



                rt.BeginInvoke(rbConnProperties.Checked, ConnectionString, Server, CatalogName, testFile, resultsFile, MaxParallel, EffectiveUserName, null, null);
                
            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
                MessageBox.Show(ex.Message, "Run Tests", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public delegate void RunTestsDelegate(bool ConnProperties,  string ConnectionString, string Server, string CatalogName, string testFile, string resultsFile, int MaxParallel, string EffectiveUserName);
        

        delegate void EnableControlsDelegate(bool enable);
        private void EnableControls(bool enable)
        {


            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (groupBoxTests.InvokeRequired)
            {
                EnableControlsDelegate d = EnableControls;
                Invoke(d, new object[] { enable });
            }
            else
            {
                groupBoxTests.Enabled = enable;
                groupBoxCompare.Enabled = enable;

                Cursor = (enable) ? Cursors.Default : Cursors.WaitCursor;
            }


        }


        private void RunTests(bool ConnProperties,  string ConnectionString, string Server, string CatalogName, string testFile, string resultsFile, int MaxParallel, string EffectiveUserName)
        {

            try
            {

                EnableControls(false);

                SetStatusMessage("====================================================================================");
                
                TestResults r ;
                if (ConnProperties)
                    r = new TestResults(Server, CatalogName, MaxParallel, EffectiveUserName);
                else
                    r = new TestResults(ConnectionString, MaxParallel);

                r.StatusEvent += OnResultProcess;

                //Results => file
                if (r.RunTeststoFile(testFile, resultsFile))
                    SetStatusMessage(string.Format("Successfully saved Results to {0}", resultsFile));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Run Tests", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableControls(true);

            }
        }


        private void btnCompareData_Click(object sender, EventArgs e)
        {
            CompareData();
        }

        private void CompareData()
        {
            ValueComparitor comparitor = ValueComparitor.FormattedValue;

            try
            {//  GetFileData

                string FilePath1 = txtCompareResultFile1.Text;
                string FilePath2 = txtCompareResultFile2.Text;
                SetStatusMessage("====================================================================================");
                ResultsComparator rc = new ResultsComparator();
                rc.StatusEvent += OnResultProcess;
                rc.Compare(FilePath1, FilePath2, comparitor);
              

                frmCompareResults f = new frmCompareResults() { resultsComparator = rc };
                f.Show();
            
            }
            catch (Exception ex)
            {
            //    MessageBox.Show(ex.Message);
                MessageBox.Show(ex.Message, "Error Comparing Results", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResultsView1_Click(object sender, EventArgs e)
        {
            string ResultsFilePath = txtCompareResultFile1.Text;
            ShowResults(ResultsFilePath);
        }

        private static void ShowResults(string ResultsFilePath)
        {
            try
            {

                if (Utils.ValidateExistingResultFilePath (ResultsFilePath ))
                {
                    frmResults f = new frmResults() ;
                    
                    f.Show();
                    f.ResultsFilePath = ResultsFilePath;
                }
                //else
                //    throw new FileNotFoundException(string.Format("Results file not specified/found"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Showing Results", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResultsView2_Click(object sender, EventArgs e)
        {
            string ResultsFilePath = txtCompareResultFile2.Text;
            ShowResults(ResultsFilePath);
        }

        private void btnResultsViewLatest_Click(object sender, EventArgs e)
        {
            string ResultsFilePath = txtResultFilePath.Text;
            ShowResults(ResultsFilePath);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAboutBox f = new frmAboutBox();
            f.ShowDialog();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void editTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditTestFile();
        }

        private void newTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewTests();
        }

        private void runTesttoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RunTests();
        }

        private void comparetoolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CompareData();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_DragDrop( object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                ((TextBox)sender).Text = files[0];
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_DragEnter(object sender, DragEventArgs e)
        {
            // Check if the Dataformat of the data can be accepted
            // (we only accept file drops from Explorer, etc.)
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it
        }

        private void groupBoxTests_Enter(object sender, EventArgs e)
        {

        }

       

        private void LoadWindowUserSettings()
        {
            System.Drawing.Point loc = Properties.Settings.Default.frmMain_Location;
            System.Drawing.Size size = Properties.Settings.Default.frmMain_Size;
            if (loc.X >= 0 && loc.Y >= 0)
            {
                this.Location = loc;
            }
            //http://stackoverflow.com/questions/847752/net-wpf-remember-window-size-between-sessions

            if (size.Height >= 100 && size.Width >= 200)
            {
                this.Size = size;
            }
        }

        private void rbConnectionString_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void rbConnProperties_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
          
        }
        

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool chk = (tabControl1.SelectedIndex == 0);
            rbConnProperties.Checked = chk;
            rbConnectionString.Checked = !chk;
          
        }

      
    }
}
