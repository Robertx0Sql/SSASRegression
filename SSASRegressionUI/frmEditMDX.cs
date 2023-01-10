using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//using MDXParser;

namespace SSASRegressionUI
{
    public partial class frmEditMDX : Form
    {
   //     private FormatOptions m_formatOptions = new FormatOptions();
        
        public frmEditMDX()
        {
            InitializeComponent();
        }


        public string Mdx
        {
            get
            {
                return MdxEditor.Text;
            }
            set
            {
                MdxEditor.Text = value;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CancelAndExit();

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAndExit();
        }

        private void cancelToolStripButton_Click(object sender, EventArgs e)
        {
            CancelAndExit();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxEditor.SelectAll(); 
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxEditor.Cut();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Determine if last operation can be undone in text box.    
            if (MdxEditor.CanUndo == true)
            {
                // Undo the last operation.
                MdxEditor.Undo();
                // Clear the undo buffer to prevent last action from being redone.
                //mdxQueryEditor1.ClearUndo();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxEditor.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxEditor.Paste();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveAndExit();
        }

        private void validateMdxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxEditor.ParseClick(true);
        }
        void SaveAndExit()
        {

            DialogResult = DialogResult.OK;

            Hide();
        }
        void CancelAndExit()
        {
            if (saveToolStripButton.Enabled)
            {
                if (MessageBox.Show("Cancel Changes?", "MDX Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
            DialogResult = DialogResult.Cancel ;

            Hide();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxEditor.ShowOptions();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxEditor.Redo();
        }

        private void formatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxEditor.FormatClick(true);
        }

        private void commentSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxEditor.CommentSelection();
        }

        private void uncommentSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxEditor.UncommentSelection();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            MdxEditor.Cut();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            MdxEditor.Copy();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            MdxEditor.Paste();
        }

        private void toolStripButtonParseMdx_Click(object sender, EventArgs e)
        {
            MdxEditor.ParseClick(true);
        }

        private void toolStripButtonCommentMDX_Click(object sender, EventArgs e)
        {
            MdxEditor.CommentSelection();
        }

        private void toolStripButtonUnCommentMdx_Click(object sender, EventArgs e)
        {
            MdxEditor.UncommentSelection();
        }

        private void toolStripButtonFormatMDX_Click(object sender, EventArgs e)
        {
            MdxEditor.FormatClick(true);
        }

        private void optionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MdxEditor.ShowOptions();
        }

        private void showHideMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
       //     MdxEditor.MessageView = !MdxEditor.MessageView;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            MdxEditor.ShowOptions();
        }

        private void frmEditMDX_Load(object sender, EventArgs e)
        {
            System.Drawing.Point loc = Properties.Settings.Default.frmEditMdx_Location;
            System.Drawing.Size size = Properties.Settings.Default.frmEditMdx_Size;

            //multiple monitors and location > current single monitor 
            //http://stackoverflow.com/questions/847752/net-wpf-remember-window-size-between-sessions
            if (loc.X >= 0 && loc.Y >= 0)
            {
                this.Location = loc;
            }
            if (size.Height >= 100 && size.Width >= 200)
            {
                this.Size = size;
            }
        }

        private void frmEditMDX_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.Cancel && saveToolStripButton.Enabled)
            {

                if (WindowState == FormWindowState.Maximized)
                {
                    Properties.Settings.Default.frmEditMdx_Location = RestoreBounds.Location;
                    Properties.Settings.Default.frmEditMdx_Size = RestoreBounds.Size;
                    Properties.Settings.Default.frmEditMdx_Maximised = true;

                }
                else
                {
                    Properties.Settings.Default.frmEditMdx_Location = Location;
                    Properties.Settings.Default.frmEditMdx_Size = Size;
                    Properties.Settings.Default.frmEditMdx_Maximised = false;

                }
                Properties.Settings.Default.Save();
            }
        }

        public bool ReadOnly
        {
            set
            {
                saveToolStripButton.Enabled = !value;
                saveToolStripMenuItem.Enabled = !value;
            }
        }
    }
}
