using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SSASRegressionCL;
using System.IO;
using System.ComponentModel;
using System.Drawing;

namespace SSASRegressionUI
{
    public partial class frmEditTests2 : Form
    {
        
        
        public frmEditTests2()
        {
            InitializeComponent();

            //reset _isDirty => false; as seting the binding source on the objects sets it to true
            _isDirty = false;
            
        }


        private bool _isDirty = false;
        private string _filePath = "";
    

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (value == null) value = "";

                _filePath = value;
      

                this.Text= "Tests Editor - Editing File :" + _filePath;
                
            }
        }

        private int DataSourceCount
        {
            get
            {
                return testBindingSource.List.Count;
            }
        }

        private int CurrentPosition
        {
            get
            {
                return testBindingSource.Position;
            }
        }
        private Test CurrentTest
        {
            get
            {

                if (CurrentPosition >= 0)
                {
                    return testBindingSource.List.Cast<SSASRegressionCL.Test>().ToList()[CurrentPosition];
                }
                else
                    return null;
            }
        }
          
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            AddNewTest();
        }

        
        private void openToolStripButton_Click(object sender, EventArgs e)
        {

            OpenTestsFile();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveTests();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenTestsFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveTests();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DeleteCurrentTest();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTest();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteCurrentTest();
        }

        private void toolStripMenuItemImport_Click(object sender, EventArgs e)
        {
            ImportTests();
        }

        private void toolStripMenuItemExport_Click(object sender, EventArgs e)
        {
            Export();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }


        private void ReOrderUptoolStripButton_Click(object sender, EventArgs e)
        {
            ReOrderTests_MoveUp();
        }
        private void ReOrderDowntoolStripButton_Click(object sender, EventArgs e)
        {
            ReOrderTests_MoveDown();
        }


        private void textMdx_TextChanged(object sender, EventArgs e)
        {
            TextChangedUpdateBinding(sender);
        }

        private void textDescription_TextChanged(object sender, EventArgs e)
        {
            TextChangedUpdateBinding(sender);

        }

        private void textId_TextChanged(object sender, EventArgs e)
        {
            TextChangedUpdateBinding(sender);

        }

        private static void TextChangedUpdateBinding(object sender)
        {
            TextBox txt = (TextBox)sender;
            txt.DataBindings["Text"].WriteValue();
        }
        private void testBindingSource_PositionChanged(object sender, EventArgs e)
        {
            UpdateItemInfoLabel();
        }

        private void testBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            _isDirty = true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Exit()
        {
                Close();
        }



        private void frmEditTests2_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (_isDirty && this.DataSourceCount > 0)
            {
                string MessaageDescription = String.Format("Save changes ?");

                string MessageCaption = "Close Editor";
                DialogResult messageBoxShow = MessageBox.Show(MessaageDescription, MessageCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
                switch (messageBoxShow)
                {
                    case DialogResult.Yes:
                        if(!SaveTests())
                            e.Cancel = true;
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel://cancel exit and continue editing
                        e.Cancel=true;
                        break;
                }
            }
            if (!e.Cancel)
            {

                if (WindowState == FormWindowState.Maximized)
                {
                    Properties.Settings.Default.frmEditTests_Location = RestoreBounds.Location;
                    Properties.Settings.Default.frmEditTests_Size = RestoreBounds.Size;
                    Properties.Settings.Default.frmEditTests_Maximised = true;
            
        }
                else 
                {
                    Properties.Settings.Default.frmEditTests_Location = Location;
                    Properties.Settings.Default.frmEditTests_Size = Size;
                    Properties.Settings.Default.frmEditTests_Maximised = false;
                  
                }
                Properties.Settings.Default.Save();
            }
        }
        private void frmEditTests2_Load(object sender, EventArgs e)
        {

            Point loc = Properties.Settings.Default.frmEditTests_Location;
            System.Drawing.Size size = Properties.Settings.Default.frmEditTests_Size;
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
        /// <summary>
        /// 
        /// </summary>
        private void DeleteCurrentTest()
        {
            
            int index = CurrentPosition;
            if (index >= 0)
            {
                string CurrentTestDescription;

                if (CurrentTest != null)
                    CurrentTestDescription = CurrentTest.ToString();
                else
                    CurrentTestDescription = "";

                string MessaageDescription = String.Format("Delete Test {0}?", CurrentTestDescription.TrimStart());

                string MessageCaption = "Confirm Delete";
                if (DialogResult.OK == MessageBox.Show(MessaageDescription, MessageCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    testBindingSource.RemoveAt(index);
            }
        }
        public void LoadTestsFile(string file)
        {
            try
            {
                FilePath = file;
                Tests _tLoader = new Tests();
                List<Test> tests = _tLoader.LoadFile(FilePath);
                BindNewList(tests);
                _isDirty = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text);
            }
        }
        private void OpenTestsFile()
        {
            //Open Dialog - get file
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog() { Filter = "xml file (*.xml)|*.xml", Title = "Open Tests File" };
                openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName != "")
                {
                    LoadTestsFile(openFileDialog1.FileName); //setting the FilePath property calls the Load Test function
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool SaveAs()
        {
            bool result = false;
            try
            { //Save Dialog 
                SaveFileDialog saveFileDialog1 = new SaveFileDialog() { Filter = "xml file (*.xml)|*.xml", Title = "Save Tests" };
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    FilePath = saveFileDialog1.FileName;
                    SaveTestFile();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }

        private bool SaveTests()
        {
            bool result = false;
            try
            {
                if (FilePath.Trim() == "")
                {
                    result = SaveAs();
                }
                else
                {
                    result = SaveTestFile();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }

        private bool SaveTestFile()
        {
            bool result = false;
            try
            {
                if (FilePath.Trim() == "")
                    throw new FileNotFoundException("FilePath Not set");
                else
                {
                    
                    List<Test> ts = GetTests();
                    result = Tests.SaveFile(ts, FilePath);
                    if (result)
                    {
                        MessageBox.Show(string.Format("Successfully Saved file '{1}'", ts.Count, FilePath),"Save Tests", MessageBoxButtons.OK ,MessageBoxIcon.Information );
                        _isDirty = false;
                    }
                    else
                        MessageBox.Show(string.Format("Unable to Save file '{1}'", ts.Count, FilePath));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error while Saving file {0} \r\n{1}", FilePath, ex.Message));

            }

            return result;
        }

        private List<Test> GetTests()
        {
           
            testBindingSource.EndEdit();// testBindingSource. //check for unsaved text dat
            List<Test> tests = testBindingSource.List.Cast<SSASRegressionCL.Test>().ToList();
            return tests;
        }

    
        private void AddNewTest()
        {
            testBindingSource.Add(new SSASRegressionCL.Test());
//            UpdateItemInfoLabel();
            testBindingSource.MoveLast();
            
        }

     
        private void AddTests(List<Test> tests)
        {
            foreach (Test t in tests)
            {
                testBindingSource.Add(t);
                UpdateItemInfoLabel();
            }
            testBindingSource.MoveLast();
            
        }

        private void UpdateItemInfoLabel()
        {

            toolStripLabel1.Text = String.Format("Test {0} of {1}", testBindingSource.Position + 1, DataSourceCount);

        }
        
        private void Swap(System.Collections.IList list, int first, int second)
        {
            object temp = list[first];
            list[first] = list[second];
            list[second] = temp;
            testBindingSource.Position = second;
        }
        
        private void ReOrderTests_MoveUp()
        {
            try
            {
                    int oldIndex = testBindingSource.Position;
                    int newIndex = oldIndex + 1;
                    if (newIndex < DataSourceCount)
                    {
                        Swap(testBindingSource.List, oldIndex,newIndex);
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReOrderTests_MoveDown()
        {
            try
            {
                int oldIndex = testBindingSource.Position;
                    int newIndex = oldIndex - 1;


                    if (newIndex >= 0)// listView1.Items.Count)
                    {
                        Swap(testBindingSource.List, oldIndex,newIndex);
                    }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
        /// <summary>
        /// Imports tests from separate mdx files into 1 test suite as multiple tests
        /// </summary>
        private void ImportTests()
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.Filter = "mdx file (*.mdx)|*.mdx|" +
                    "All files (*.*)|*.*";
                openFileDialog1.Title = "Select query files";
                openFileDialog1.Multiselect = true;


                DialogResult dr = openFileDialog1.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    string[] FileNames = openFileDialog1.FileNames;

                    List<Test> tests = Tests.Import(FileNames);
                    AddTests(tests);
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Importing Queries", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void Export()
        {
            try
            {
                string dummyFileName = "Save Here";

                SaveFileDialog sf = new SaveFileDialog();
                // Feed the dummy name to the save dialog
                sf.FileName = dummyFileName;


                sf.Title = "Export Folder";

                if (sf.ShowDialog() == DialogResult.OK)
                {
                    // Now here's our save folder
                    string folderName = Path.GetDirectoryName(sf.FileName);
                    Tests.Export(folderName, GetTests());
                    MessageBox.Show(string.Format("Successfully Exported Tests to {0}", folderName), "Export Tests", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Exporting Tests", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
        private void BindNewList(List<SSASRegressionCL.Test> yourList)
        {
            testBindingSource.Clear();
            //this is not efficient but is simple
            foreach (SSASRegressionCL.Test t in yourList)
            {
                testBindingSource.Add(t);
                UpdateItemInfoLabel();
            }
        }

        private void btnEditMDX_Click(object sender, EventArgs e)
        {
            frmEditMDX f = new frmEditMDX() { Mdx = textMdx.Text };

            if (f.ShowDialog() == DialogResult.OK)
            {
                textMdx.Text = f.Mdx;
            }
        }

    

      
    }
}
