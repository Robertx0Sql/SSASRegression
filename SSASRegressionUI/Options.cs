using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MDXParser;
namespace SSASRegressionUI
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private FormatOptions m_FormatOptions;


        public Options(FormatOptions fo)
        {
            this.InitializeComponent();
            this.m_FormatOptions = fo;
            this.radioButtonNLBeforeComma.Checked = this.m_FormatOptions.CommaBeforeNewLine;
            this.radioButtonNLAfterComma.Checked = !this.m_FormatOptions.CommaBeforeNewLine;
            this.numericUpDownIndent.Value = this.m_FormatOptions.Indent;
            this.checkBoxColorFunction.Checked = this.m_FormatOptions.ColorFunctionNames;
            this.treeViewOptions.ExpandAll();
        }

        private void checkBoxColorFunction_CheckedChanged(object sender, EventArgs e)
        {
            this.m_FormatOptions.ColorFunctionNames = (sender as CheckBox).Checked;
        }
        private void numericUpDownIndent_ValueChanged(object sender, EventArgs e)
        {
            this.m_FormatOptions.Indent = (int)(sender as NumericUpDown).Value;
        }

        private void Options_Load(object sender, EventArgs e)
        {
            this.treeViewOptions.ExpandAll();
        }

        private void radioButtonNLAfterComma_CheckedChanged(object sender, EventArgs e)
        {
            this.m_FormatOptions.CommaBeforeNewLine = !(sender as RadioButton).Checked;
        }

        private void radioButtonNLBeforeComma_CheckedChanged(object sender, EventArgs e)
        {
            this.m_FormatOptions.CommaBeforeNewLine = (sender as RadioButton).Checked;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}
