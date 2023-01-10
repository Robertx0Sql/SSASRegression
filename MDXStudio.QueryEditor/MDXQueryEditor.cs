using MDXStudio.QueryEditor;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
//using WeifenLuo.WinFormsUI.Docking;
using MDXParser;

namespace MDXStudio.QueryEditor
{
    public partial class MDXQueryEditor : UserControl
    {
        public MDXQueryEditor()
        {
            InitializeComponent();
            this.m_fChanged = false;
            WigglyLinesPainter wigglyLinesPainter = new WigglyLinesPainter(mdxEditor);
            mdxEditor.Tag = wigglyLinesPainter;
            m_Connection = new Connection();
            this.m_Connection.AMOConnection = null;
            this.m_MessagesGrid = new MessageLog(this.messagesControl1.DataGridView);

        }
        MessageLog m_MessagesGrid;
        string m_TabText;
        private Intellisense m_Intellisense;
        private bool m_fChanged;
        internal Connection m_Connection;
        private FormatOptions m_formatOptions = new FormatOptions();
        /// <summary>
        /// 
        /// </summary>
        public FormatOptions formatOptions
        {
            get { return m_formatOptions; }
            set
            {
                m_formatOptions = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Changed
        {
            get
            {
                return this.m_fChanged;
            }
            set
            {
                if (this.m_fChanged != value)
                {
                    this.m_fChanged = value;
                    if (this.m_fChanged)
                    {
                        m_TabText = string.Format("{0}*", this.Text);
                        return;
                    }
                    m_TabText = this.Text;
                }
            }
        }

        private void mdxEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                //(base.ParentForm as MainForm).toolStripButtonExecute_Click(null, null);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                //(base.ParentForm as MainForm).toolStripButtonExecute_Click(null, null);
                e.Handled = true;
            }
            else if (!e.Control || e.KeyCode != Keys.P)
            {
                this.m_Intellisense.ProcessKeyDownEvent(sender, e);
            }
            else
            {
                //(base.ParentForm as MainForm).toolStripButtonParse_Click(null, null);
                e.Handled = true;
            }
            //base.OnKeyDown(e);
        }

        private void mdxEditor_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.m_Intellisense.ProcessKeyPressEvent(sender, e, this.m_Connection);
            base.OnKeyPress(e);

        }

        private void mdxEditor_LostFocus(object sender, EventArgs e)
        {
        }


        private void mdxEditor_MouseDown(object sender, MouseEventArgs e)
        {
            this.m_Intellisense.ProcessMouseDownEvent(sender, e);
        }

        private void mdxEditor_TextChanged(object sender, EventArgs e)
        {
            this.Changed = true;
        }

        private void QueryWindowUC_Load(object sender, EventArgs e)
        {
            this.m_Intellisense = new Intellisense();
            this.m_Intellisense.m_AutoCompleteList = this.autoCompleteList;
            this.autoCompleteList.KeyDown += this.m_Intellisense.listBoxAutoComplete_KeyDown;
            this.autoCompleteList.Click += new EventHandler(this.m_Intellisense.listBoxAutoComplete_DoubleClick);
            this.autoCompleteList.DoubleClick += new EventHandler(this.m_Intellisense.listBoxAutoComplete_DoubleClick);
            this.autoCompleteList.SelectedIndexChanged += new EventHandler(this.m_Intellisense.listBoxAutoComplete_SelectedIndexChanged);
            this.splitContainer1.SplitterDistance = (int)(this.Size.Height * 0.75);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IsExpression"></param>
        public void FormatClick(bool IsExpression)
        {
            try
            {
                MdxEditor currentMdxEditor = this.GetCurrentMdxEditor();
                if (currentMdxEditor != null)
                {
                    int selectionLength = currentMdxEditor.SelectionLength;
                    this.ClearMessages();
                    MDXParser.MDXParser mDXParser = this.Parse(null, false, IsExpression, true);
                    if (mDXParser != null)
                    {
                        FormatOptions formatOption = new FormatOptions(this.m_formatOptions)
                        {
                            Output = OutputFormat.Text
                        };
                        string str = mDXParser.FormatMDX(formatOption);
                        if (selectionLength <= 0)
                        {
                            currentMdxEditor.Text = str;
                        }
                        else
                        {
                            currentMdxEditor.SelectedText = str;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessagesGridWriteErrorMessage(exception.Message);


            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IsExpression"></param>
        public void ParseClick(bool IsExpression)
        {
            try
            {
                this.ClearMessages();
                MDXParser.MDXParser mDXParser = this.Parse(null, true, IsExpression, true);
                if (mDXParser != null)
                {

                    //this.m_winDependencies.Show(this.dockPanel);
                    //this.m_winParseTree.Show(this.dockPanel);
                    //activeDocument.DockHandler.Show(this.dockPanel);
                    MDXNode node = mDXParser.GetNode();
                    DependencyGraph dependencyGraph = new DependencyGraph();
                    node.BuildDependencyGraph(dependencyGraph, null);
                    //this.m_winDependencies.DataGridView.Rows.Clear();
                    //dependencyGraph.UpdateStats(this.m_winDependencies.DataGridView);
                    //dependencyGraph.Visualize(this.m_winDependencies.NetMapControl, null, null);
                    MDXSelectNode select = mDXParser.GetSelect();
                    if (select != null)
                    {
                        /*if (this.m_Connection.QueryConnection != null && this.toolStripComboBoxCubes.SelectedIndex >= 0)
                        {
                            string name = ((MdCube)this.toolStripComboBoxCubes.Items[this.toolStripComboBoxCubes.SelectedIndex]).Name;
                            this.QueryContext.SetSelect(select, this.m_Connection.QueryConnection, name);
                        }
                         */
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);//		this.m_MessagesGrid.WriteErrorMessage(exception.Message);
            }
        }

        private void ClearMessages()
        {
            this.m_MessagesGrid.Clear();
        }

        private MDXParser.MDXParser Parse(string text, bool FillTree, bool IsExpression, bool TryAnother)
        {
            MDXParser.MDXParser mDXParser;
            MdxEditor currentMdxEditor = this.GetCurrentMdxEditor();
            if (currentMdxEditor == null)
            {
                return null;
            }
            string str = text ?? (currentMdxEditor.SelectionLength <= 0 ? currentMdxEditor.Text : currentMdxEditor.SelectedText);
            Locator locator = new Locator();
            if (currentMdxEditor.SelectionLength > 0)
            {
                locator.Position = currentMdxEditor.SelectionStart;
            }
            MDXParser.CubeInfo adomdClientCubeInfo = null;

            /*if (this.m_Connection.MetadataConnection != null && this.toolStripComboBoxCubes.SelectedIndex >= 0)
            {
                string name = ((MdCube)this.toolStripComboBoxCubes.Items[this.toolStripComboBoxCubes.SelectedIndex]).Name;
                if (!string.IsNullOrEmpty(name))
                {
                    adomdClientCubeInfo = new AdomdClientCubeInfo(name, this.m_Connection.MetadataConnection);
                }
              
            }
             * */
            if (adomdClientCubeInfo == null)
            {
                adomdClientCubeInfo = new MDXParser.CubeInfo();
            }

            MDXParser.MDXParser mDXParser1 = new MDXParser.MDXParser(str, new TextBoxSource(currentMdxEditor, locator), adomdClientCubeInfo);
            int selectionStart = currentMdxEditor.SelectionStart;
            currentMdxEditor.SelectAll();
            currentMdxEditor.SelectionBackColor = SystemColors.Window;
            currentMdxEditor.SelectionStart = selectionStart;
            currentMdxEditor.SelectionLength = 0;
            currentMdxEditor.Focus();
            int num = currentMdxEditor.SelectionStart;
            try
            {
                try
                {
                    if (str == string.Empty)
                        return null;
                    else
                        if (!IsExpression)
                        {
                            mDXParser1.Parse();
                        }
                        else
                        {
                            mDXParser1.ParseExpression();
                        }
                }
                catch (MDXParserException mDXParserException1)
                {
                    MDXParserException mDXParserException = mDXParserException1;
                    if (!TryAnother)
                    {
                        MessageGridPopulate(mDXParserException1, mDXParserException.Messages);
                    }
                    else
                    {
                        try
                        {
                            if (!IsExpression)
                            {
                                mDXParser1.ParseExpression();
                            }
                            else
                            {
                                mDXParser1.Parse();
                            }
                        }
                        catch (Exception exception)
                        {
                            MessageGridPopulate(exception, mDXParserException.Messages);
                            mDXParser = null;
                            return mDXParser;
                        }
                    }
                }
                /* if (FillTree)
                 {
                     System.Windows.Forms.TreeNode parseTreeNode = this.GetParseTreeNode((currentMdxEditor.ReadOnly ? "MDX Script" : "Queries"));
                     parseTreeNode.Nodes.Clear();
                     parseTreeNode.Tag = mDXParser1.GetNode();
                     mDXParser1.FillTree(parseTreeNode);
                     parseTreeNode.Expand();
                     parseTreeNode.TreeView.SelectedNode = parseTreeNode;
                 }
                 * */
                return mDXParser1;
            }
            finally
            {
                currentMdxEditor.SelectionStart = num;
                currentMdxEditor.SelectionLength = 0;
            }
            return mDXParser;
        }

        private MdxEditor GetCurrentMdxEditor()
        {
            return mdxEditor;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Undo()
        {
            MdxEditor currentMdxEditor = this.GetCurrentMdxEditor();
            if (currentMdxEditor != null)
                currentMdxEditor.Undo();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Redo()
        {
            MdxEditor currentMdxEditor = this.GetCurrentMdxEditor();
            if (currentMdxEditor != null)
                currentMdxEditor.MakeRedo();

        }

        public new bool CanUndo
        {
            get
            {
                MdxEditor currentMdxEditor = this.GetCurrentMdxEditor();
                if (currentMdxEditor != null)
                    return currentMdxEditor.CanUndo;
                else
                    return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SelectedText
        {
            get
            {
                MdxEditor currentMdxEditor = this.GetCurrentMdxEditor();
                if (currentMdxEditor != null)
                    return currentMdxEditor.SelectedText;
                else
                    return null;

            }
            set
            {
                MdxEditor currentMdxEditor = this.GetCurrentMdxEditor();
                if (currentMdxEditor != null)
                    currentMdxEditor.SelectedText = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get
            {
                MdxEditor currentMdxEditor = this.GetCurrentMdxEditor();
                if (currentMdxEditor != null)
                    return currentMdxEditor.Text;
                else
                    return null;

            }
            set
            {
                MdxEditor currentMdxEditor = this.GetCurrentMdxEditor();
                if (currentMdxEditor != null)
                    currentMdxEditor.Text = value;
            }
        }
        
        public void SelectAll()
        {
            MdxEditor currentMdxEditor = this.GetCurrentMdxEditor();
            if (currentMdxEditor != null)
            {
                currentMdxEditor.SelectAll();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Copy()
        {
            if (!string.IsNullOrEmpty(SelectedText))
            {
                Clipboard.SetText(SelectedText);
                return;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Cut()
        {
            Copy();
            SelectedText = "";
        }
        /// <summary>
        /// 
        /// </summary>
        public void Paste()
        {
            if (!string.IsNullOrEmpty(Clipboard.GetText()))
            {
                SelectedText = Clipboard.GetText();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public void CommentSelection()
        {
            MdxEditor currentMdxEditor = this.GetCurrentMdxEditor();
            if (currentMdxEditor == null)
            {
                return;
            }
            int lineFromCharIndex = currentMdxEditor.GetLineFromCharIndex(currentMdxEditor.SelectionStart);
            int num = currentMdxEditor.GetLineFromCharIndex(currentMdxEditor.SelectionStart + currentMdxEditor.SelectionLength);
            currentMdxEditor.SelectionLength = 0;
            for (int i = lineFromCharIndex; i <= num; i++)
            {
                currentMdxEditor.SelectionStart = currentMdxEditor.GetFirstCharIndexFromLine(i);
                currentMdxEditor.SelectedText = "//";
                currentMdxEditor.SelectionLength = 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void UncommentSelection()
        {
            MdxEditor currentMdxEditor = this.GetCurrentMdxEditor();
            if (currentMdxEditor == null)
            {
                return;
            }
            int lineFromCharIndex = currentMdxEditor.GetLineFromCharIndex(currentMdxEditor.SelectionStart);
            int num = currentMdxEditor.GetLineFromCharIndex(currentMdxEditor.SelectionStart + currentMdxEditor.SelectionLength);
            currentMdxEditor.SelectionLength = 0;
            for (int i = lineFromCharIndex; i <= num; i++)
            {
                int firstCharIndexFromLine = currentMdxEditor.GetFirstCharIndexFromLine(i);
                if (firstCharIndexFromLine + 2 >= currentMdxEditor.TextLength)
                {
                    return;
                }
                if (currentMdxEditor.Text[firstCharIndexFromLine] == '/' && currentMdxEditor.Text[firstCharIndexFromLine + 1] == '/')
                {
                    currentMdxEditor.SelectionStart = firstCharIndexFromLine;
                    currentMdxEditor.SelectionLength = 2;
                    currentMdxEditor.SelectedText = "";
                    currentMdxEditor.SelectionLength = 0;
                }
            }
        }
 /// <summary>
 /// 
 /// </summary>
        public bool MessageView
        {
            get
            {
                return !this.splitContainer1.Panel2Collapsed;
            }
            set
            {
                this.splitContainer1.Panel2Collapsed = !value;

            }
        }
        private void MessageGridPopulate(Exception exception, MessageCollection messages)
        {
            if (MessageView)
                this.m_MessagesGrid.Populate(messages);
            else
            {
                string exception_Message = exception.Message;
                foreach (MDXParser.Message item in messages)
                {
                    exception_Message += "\r\n" + item.Text;
                }

                MessageBox.Show(exception_Message);
            }
        }

        private void MessagesGridWriteErrorMessage(string errormessage)
        {
            if (MessageView)
                this.m_MessagesGrid.WriteErrorMessage(errormessage);
            else
                MessageBox.Show(errormessage);
        }
    }
}
