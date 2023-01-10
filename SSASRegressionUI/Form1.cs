using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SSASRegressionUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void userControl21_FormatMdx(object sender, EventArgs e)
        {
            
        }

        private void formatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxQueryEditor.FormatClick(false);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDXParser.FormatOptions formatOptions = this.MdxQueryEditor.formatOptions;
            (new Options(formatOptions)).ShowDialog();
            (new Options(formatOptions)).ShowDialog();
        }

        private void parseToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // MdxQueryEditor.ParseClick(false);
        }

        private void formatToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //MdxQueryEditor.FormatClick(false);
        }

        private void parseExpressionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxQueryEditor.ParseClick(true);
        }

        private void formatExpressionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxQueryEditor.FormatClick(true);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxQueryEditor.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxQueryEditor.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MdxQueryEditor.SelectedText))
            {
                Clipboard.SetText(MdxQueryEditor.SelectedText);
            }
            MdxQueryEditor.SelectedText = "";
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MdxQueryEditor.SelectedText))
            {
                Clipboard.SetText(MdxQueryEditor.SelectedText);
                return;
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Clipboard.GetText()))
            {
                MdxQueryEditor.SelectedText = Clipboard.GetText();
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxQueryEditor.SelectAll();
        }

        private void commentSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxQueryEditor.CommentSelection();
        }

        private void uncommentSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxQueryEditor.UncommentSelection();
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdxQueryEditor.MessageView = !MdxQueryEditor.MessageView;
        }
    }
}
