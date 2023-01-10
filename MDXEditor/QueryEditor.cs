using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MdxQueryEditor
{
    public partial class QueryEditor : UserControl, IQueryEditor
    {
        public QueryEditor()
        {
            InitializeComponent();
            m_formatOptions.ColorFunctionNames = true;
            m_formatOptions.CommaBeforeNewLine = false;
            m_formatOptions.Indent = 2;
            m_formatOptions.LineWidth = 80;
            m_formatOptions.Output = MDXParser.OutputFormat.Text;
           
        }
        MDXParser.FormatOptions m_formatOptions = new MDXParser.FormatOptions();
        private bool m_fChanged;
        public bool CanUndo
        {
            get
            {
                return richTextBox1.CanUndo;
            }
        }

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
                        return;
                    }
                }
            }
        }

        public void CommentSelection()
        {
       
            int lineFromCharIndex = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);
            int num = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart + richTextBox1.SelectionLength);
            richTextBox1.SelectionLength = 0;
            for (int i = lineFromCharIndex; i <= num; i++)
            {
                richTextBox1.SelectionStart = richTextBox1.GetFirstCharIndexFromLine(i);
                richTextBox1.SelectedText = "//";
                richTextBox1.SelectionLength = 0;
            }
        }

        public void Copy()
        {
            if (!string.IsNullOrEmpty(SelectedText))
            {
                Clipboard.SetText(SelectedText);
                return;
            }
        }

        public void Cut()
        {

            Copy();
            SelectedText = "";
        }

        public void FormatClick(bool IsExpression)
        {

            if (richTextBox1.Text.Length == 0)
                return;

            //Log(richTextBox1.Text, "Format");

            MDXParser.MDXParser parser = DoParse(richTextBox1.Text, false);
            if (parser != null)
            {
              
                string formatedmdx =parser.FormatMDX(m_formatOptions);
                richTextBox1.Text = formatedmdx;
            }
        }
        
        internal MDXParser.FormatOptions formatOptions
        {
            
            get { return m_formatOptions; }
            set
            {
                m_formatOptions = value;
            }
        }
        public bool FormatOptionsColorFunctionNames
        {
            get { return m_formatOptions.ColorFunctionNames; }
            set { m_formatOptions.ColorFunctionNames = value; }
        }
        public bool FormatOptionsCommaBeforeNewLine
        {
            get { return m_formatOptions.CommaBeforeNewLine; }
            set { m_formatOptions.CommaBeforeNewLine = value; }
        }
        
        public int FormatOptionsIndent
        {
            get { return m_formatOptions.Indent; }
            set { m_formatOptions.Indent = value; }
        }
        public int FormatOptionsLineWidth
        {
            get { return m_formatOptions.LineWidth; }
            set { m_formatOptions.LineWidth = value; }
        }



        public void ParseClick(bool IsExpression)
        {
            MDXParser.MDXParser parser = DoParse(richTextBox1.Text, false);
            
        }

        public void Paste()
        {
            if (!string.IsNullOrEmpty(Clipboard.GetText()))
            {
                SelectedText = Clipboard.GetText();
            }

        }

        public void Redo()
        {
            richTextBox1.Redo();
        }

        public void SelectAll()
        {
            richTextBox1.SelectAll();
        }

        public string SelectedText
        {
            get
            {
                return richTextBox1.SelectedText;
            }
            set
            {
                richTextBox1.SelectedText = value;
            }
        }

        public void UncommentSelection()
        {
            int lineFromCharIndex = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);
            int num = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart + richTextBox1.SelectionLength);
            richTextBox1.SelectionLength = 0;
            for (int i = lineFromCharIndex; i <= num; i++)
            {
                int firstCharIndexFromLine = richTextBox1.GetFirstCharIndexFromLine(i);
                if (firstCharIndexFromLine + 2 >= richTextBox1.TextLength)
                {
                    return;
                }
                if (richTextBox1.Text[firstCharIndexFromLine] == '/' && richTextBox1.Text[firstCharIndexFromLine + 1] == '/')
                {
                    richTextBox1.SelectionStart = firstCharIndexFromLine;
                    richTextBox1.SelectionLength = 2;
                    richTextBox1.SelectedText = "";
                    richTextBox1.SelectionLength = 0;
                }
            }
        }

        public void Undo()
        {
            
            this.richTextBox1.Undo();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.Changed = true;
        }

        public string Text
        {
            get
            {
                return richTextBox1.Text;
            }
            set
            {
                 richTextBox1.Text=value;
            }
        }
        private MDXParser.MDXParser DoParse(string mdx, bool ExprFirst)
        {


            MDXParser.MDXParser parser = new MDXParser.MDXParser(
                mdx, new MDXParser.Source(), new MDXParser.CubeInfo());

            try
            {
                if (ExprFirst)
                    parser.ParseExpression();
                else
                    parser.Parse();
            }

            catch (MDXParser.MDXParserException ex)
            {
                try
                {
                    if (ExprFirst)
                        parser.Parse();
                    else
                        parser.ParseExpression();
                }

                catch (Exception)
                {
                    LogParsingError(ex);
                    return null;
                }
            }

            catch (Exception ex)
            {
                LogGeneralError(ex);
                return null;
            }

            return parser;
        }

        private void MessageGridPopulate(Exception exception, MDXParser.MessageCollection messages)
        {
            string exception_Message = exception.Message;
            foreach (MDXParser.Message item in messages)
            {
                exception_Message += "\r\n" + item.Text;
            }

            MessageBox.Show(exception_Message);
        }
        private void LogParsingError(MDXParser.MDXParserException ex)
        {
            MDXParser.Message m = ex.Messages[0];// &#91;0&#93;;
         //   Log(m.Text, "Error");
            MessageBox.Show(m.Text, "Parsing Error", MessageBoxButtons.OK ,MessageBoxIcon.Error);
        }

        private void LogGeneralError(Exception ex)
        {
            MessageBox.Show(ex.Message, "General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // Log(ex.Message, "Failure");
        }
        protected void Log(string mdx, string action)
        {
            string logfile = "";
        }
        public void ShowOptions()
        {
            (new MDXParserOptions(this.m_formatOptions)).ShowDialog();
        }

    }
}
