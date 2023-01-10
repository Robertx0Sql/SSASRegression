using MDXParser;
using MDXStudio;
using SearchableControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MDXStudio.QueryEditor
{
    internal sealed class MdxEditor : SearchableRichTextBox
    {
        private bool mChangingFlag;

        private string mPrevMDX;

        private bool mInOnUndo;

        private Stack<UndoRedoEntry> mUndoStack;

        private Stack<UndoRedoEntry> mRedoStack;

        private ToolTip mToolTip;

        private MDXParser.ColorCoding m_ColorCoding;

        //	private IContainer components;

        public new bool CanUndo
        {
            get
            {
                return true;
            }
        }

        public MDXParser.ColorCoding ColorCoding
        {
            get
            {
                return this.m_ColorCoding;
            }
        }

        public bool RedoEnabled
        {
            get
            {
                return this.mRedoStack.Count > 0;
            }
        }

        public bool UndoEnabled
        {
            get
            {
                return this.mUndoStack.Count > 1;
            }
        }

        public MdxEditor()
        {
            this.InitializeComponent();
            this.AllowDrop = true;
            this.InitUndoRedoBuffer();
        }

        public MdxEditor(IContainer container)
        {
            container.Add(this);
            this.InitializeComponent();
            this.AllowDrop = true;
            base.AcceptsTab = true;
            this.InitUndoRedoBuffer();
            this.mToolTip = new ToolTip(base.Container);
        }

        private void AdjustTextColors(IList<TextSegment> pList, int pStart, int pEnd)
        {
            Color black = Color.Black;
            for (int i = pStart; i <= pEnd; i++)
            {
                switch (pList[i].HighlightType)
                {
                    case eHighlightType.BlockComment:
                        {
                            black = Color.Green;
                            break;
                        }
                    case eHighlightType.KeyWord:
                        {
                            black = Color.Blue;
                            break;
                        }
                    case eHighlightType.LineComment:
                        {
                            black = Color.Green;
                            break;
                        }
                    case eHighlightType.Number:
                        {
                            black = Color.Magenta;
                            break;
                        }
                    case eHighlightType.Operator:
                        {
                            black = Color.Gray;
                            break;
                        }
                    case eHighlightType.PlainText:
                        {
                            black = Color.Black;
                            break;
                        }
                    case eHighlightType.String:
                        {
                            black = Color.Red;
                            break;
                        }
                    case eHighlightType.SystemFunction:
                        {
                            black = Color.DarkRed;
                            break;
                        }
                }
                this.AdjustTextSegment(pList[i].StartPosition, pList[i].Lenth, black);
                if (i == pEnd)
                {
                    MdxEditor selectionStart = this;
                    selectionStart.SelectionStart = selectionStart.SelectionStart + pList[i].Lenth;
                    this.SelectionLength = 0;
                }
            }
        }

        private void AdjustTextSegment(int pStartPoint, int pLenth, Color pColor)
        {
            base.SelectionStart = pStartPoint;
            this.SelectionLength = pLenth;
            base.SelectionColor = pColor;
        }

        private void CompleteList(IList<TextSegment> pList, string pText)
        {
            int i;
            int startPosition;
            int num;
            if (pList.Count <= 0)
            {
                pList.Add(new TextSegment(eHighlightType.PlainText, 0, pText.Length));
            }
            else
            {
                if (pList[0].StartPosition > 0)
                {
                    pList.Insert(0, new TextSegment(eHighlightType.PlainText, 0, pList[0].StartPosition));
                }
                for (i = 1; i < pList.Count; i++)
                {
                    num = pList[i - 1].StartPosition + pList[i - 1].Lenth;
                    if (pList[i].StartPosition > num)
                    {
                        int num1 = num;
                        startPosition = pList[i].StartPosition - num1;
                        pList.Insert(i, new TextSegment(eHighlightType.PlainText, num1, startPosition));
                        i++;
                    }
                }
                num = pList[i - 1].StartPosition + pList[i - 1].Lenth;
                startPosition = pText.Length - num;
                if (num <= pText.Length)
                {
                    pList.Add(new TextSegment(eHighlightType.PlainText, num, startPosition));
                    return;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing && this.components != null)
            //{
            //    this.components.Dispose();
            //}
            base.Dispose(disposing);
        }

        private void FilterList(List<TextSegment> pList)
        {
            pList.Sort(new MyTextSegmentComparer());
            for (int i = 0; i < pList.Count; i++)
            {
                int num = i + 1;
                while (num < pList.Count && pList[num].StartIsIn(pList[i]))
                {
                    pList.RemoveAt(num);
                }
            }
        }

        private void FindChangedText(IList<TextSegment> pTextListPrev, IList<TextSegment> pTextList, out int oChangedStart, out int oChangedEnd)
        {
            bool flag = false;
            oChangedStart = 0;
            oChangedEnd = pTextList.Count - 1;
            int num = Math.Min(pTextListPrev.Count, pTextList.Count);
            int num1 = 0;
            while (num1 < num)
            {
                if (pTextListPrev[num1].Equals(pTextList[num1]))
                {
                    num1++;
                }
                else
                {
                    oChangedStart = num1;
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                oChangedStart = num;
                return;
            }
            int count = pTextListPrev.Count - 1;
            for (int i = pTextList.Count - 1; count >= oChangedStart && i >= oChangedStart; i--)
            {
                TextSegment item = pTextListPrev[count];
                TextSegment textSegment = pTextList[i];
                oChangedEnd = i;
                if (item.Lenth != textSegment.Lenth)
                {
                    break;
                }
                if (item.HighlightType != textSegment.HighlightType)
                {
                    return;
                }
                count--;
            }
        }

        public int FindMatch(char c1, char c2)
        {
            int selectionStart = base.SelectionStart - 1;
            int num = 1;
            while (selectionStart >= 0)
            {
                if (this.Text[selectionStart] == c2)
                {
                    num--;
                    if (num == 0)
                    {
                        return selectionStart;
                    }
                }
                else if (this.Text[selectionStart] == c1)
                {
                    num++;
                }
                selectionStart--;
            }
            return -1;
        }

        public string GetCurrentWord()
        {
            return this.GetWord(1, true, false);
        }

        public string GetPrecedingWord()
        {
            return this.GetPrecedingWord(1);
        }

        public string GetPrecedingWord(int cwords)
        {
            return this.GetWord(cwords, false, true);
        }

        private string GetWord(int cwords, bool stopatdot, bool skiptrailingwhitespaces)
        {
            int num;
            int selectionStart = base.SelectionStart - 1;
            while (true)
            {
                if (skiptrailingwhitespaces)
                {
                    while (selectionStart >= 0 && char.IsWhiteSpace(this.Text, selectionStart))
                    {
                        selectionStart--;
                    }
                }
                if (selectionStart < 0)
                {
                    return null;
                }
                num = selectionStart;
                bool flag = false;
                while (num >= 0)
                {
                    char text = this.Text[num];
                    if (flag)
                    {
                        if (text == '[')
                        {
                            flag = false;
                        }
                    }
                    else if (text == ']')
                    {
                        flag = true;
                    }
                    else if (stopatdot && text == '.' || text != '.' && text != '\u005F' && !char.IsLetterOrDigit(text))
                    {
                        break;
                    }
                    num--;
                }
                cwords--;
                if (cwords == 0)
                {
                    break;
                }
                selectionStart = num;
            }
            num++;
            return this.Text.Substring(num, selectionStart - num + 1);
        }

        public void HideToolTip()
        {
            this.mToolTip.Hide(this);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            this.Font = FontUtils.FontCreate("Courier New", 8f, FontStyle.Regular);
            base.WordWrap = false;
            base.ResumeLayout(false);
        }

        private void InitUndoRedoBuffer()
        {
            this.mUndoStack = new Stack<UndoRedoEntry>();
            this.mRedoStack = new Stack<UndoRedoEntry>();
            this.mUndoStack.Push(new UndoRedoEntry(base.Rtf, base.SelectionStart, this.SelectionLength, null));
        }

        private void InsertTextFromClipboard()
        {
            IDataObject dataObject = Clipboard.GetDataObject();
            if (dataObject.GetDataPresent(DataFormats.UnicodeText))
            {
                this.SelectedText = dataObject.GetData(DataFormats.UnicodeText).ToString();
                return;
            }
            if (dataObject.GetDataPresent(DataFormats.Text))
            {
                this.SelectedText = dataObject.GetData(DataFormats.Text).ToString();
            }
        }

        public new void LoadFile(string pFilePath)
        {
            base.LoadFile(pFilePath);
            this.InitUndoRedoBuffer();
        }

        public new void LoadFile(string pFilePath, RichTextBoxStreamType pFileType)
        {
            base.LoadFile(pFilePath, pFileType);
            this.InitUndoRedoBuffer();
        }

        public new void LoadFile(Stream pData, RichTextBoxStreamType pFileType)
        {
            base.LoadFile(pData, pFileType);
            this.InitUndoRedoBuffer();
        }

        public void MakeRedo()
        {
            if (this.mRedoStack.Count > 0)
            {
                try
                {
                    MDXStudio.QueryEditor.NativeMethods.NestedLockWindowUpdate(base.Handle);
                    this.mInOnUndo = true;
                    UndoRedoEntry undoRedoEntry = this.mRedoStack.Pop();
                    this.mUndoStack.Push(undoRedoEntry);
                    base.Rtf = undoRedoEntry.RtfText;
                    base.SelectionStart = undoRedoEntry.SelectionStart;
                    this.SelectionLength = 0;
                }
                finally
                {
                    this.mInOnUndo = false;
                    MDXStudio.QueryEditor.NativeMethods.NestedUnlockWindowUpdate();
                    this.Refresh();
                }
            }
        }

        public void MakeUndo()
        {
            if (this.mUndoStack.Count > 1)
            {
                try
                {
                    MDXStudio.QueryEditor.NativeMethods.NestedLockWindowUpdate(base.Handle);
                    this.mInOnUndo = true;
                    this.mRedoStack.Push(this.mUndoStack.Pop());
                    UndoRedoEntry undoRedoEntry = this.mUndoStack.Peek();
                    base.Rtf = undoRedoEntry.RtfText;
                    base.SelectionStart = undoRedoEntry.SelectionStart;
                    this.SelectionLength = 0;
                    if (base.Tag != null && base.Tag.GetType() == typeof(WigglyLinesPainter))
                    {
                        (base.Tag as WigglyLinesPainter).WigglyLines = undoRedoEntry.WigglyLines;
                    }
                }
                finally
                {
                    this.mInOnUndo = false;
                    MDXStudio.QueryEditor.NativeMethods.NestedUnlockWindowUpdate();
                    this.Refresh();
                }
            }
        }

        protected override void OnDragDrop(DragEventArgs pArgs)
        {
            string data = (string)pArgs.Data.GetData(typeof(string));
            this.SelectedText = data;
            base.OnDragDrop(pArgs);
        }

        protected override void OnDragEnter(DragEventArgs pArgs)
        {
            pArgs.Effect = DragDropEffects.Move;
            base.OnDragEnter(pArgs);
        }

        protected override void OnDragOver(DragEventArgs pArgs)
        {
            pArgs.Effect = DragDropEffects.Move;
            base.OnDragOver(pArgs);
        }

        protected override void OnKeyDown(KeyEventArgs pArgs)
        {
            try
            {
                if (pArgs.KeyCode == Keys.V && pArgs.Modifiers == Keys.Control)
                {
                    this.InsertTextFromClipboard();
                    pArgs.Handled = true;
                }
                if (pArgs.KeyCode == Keys.E && pArgs.Modifiers == Keys.Control)
                {
                    pArgs.Handled = true;
                }
                if (pArgs.KeyCode == Keys.L && pArgs.Modifiers == Keys.Control)
                {
                    pArgs.Handled = true;
                }
                if (pArgs.KeyCode == Keys.R && pArgs.Modifiers == Keys.Control)
                {
                    pArgs.Handled = true;
                }
                if (pArgs.KeyCode == Keys.Back && pArgs.Modifiers == Keys.Alt || pArgs.KeyCode == Keys.Z && pArgs.Modifiers == Keys.Control)
                {
                    this.Undo();
                    pArgs.Handled = true;
                }
                if (pArgs.KeyCode == Keys.Y && pArgs.Modifiers == Keys.Control)
                {
                    this.Redo();
                    pArgs.Handled = true;
                }
                base.OnKeyDown(pArgs);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MessageBox.Show(base.FindForm(), string.Concat(exception.Message, "\n\n", exception.StackTrace), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        protected override void OnTextChanged(EventArgs pArgs)
        {
            try
            {
                base.OnTextChanged(pArgs);
                if (!this.mInOnUndo)
                {
                    this.Process();
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MessageBox.Show(base.FindForm(), string.Concat(exception.Message, "\n\n", exception.StackTrace), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Process()
        {
            if (this.mChangingFlag)
            {
                return;
            }
            try
            {
                this.mChangingFlag = true;
                string text = this.Text;
                int selectionStart = base.SelectionStart;
                bool flag = true;
                if (this.mPrevMDX != null)
                {
                    flag = !text.Equals(this.mPrevMDX, StringComparison.InvariantCulture);
                }
                this.mPrevMDX = text;
                WigglyLinesPainter tag = base.Tag as WigglyLinesPainter;
                if (flag)
                {
                    if (tag != null)
                    {
                        tag.ClearWigglyLines();
                    }
                    TextBoxSource textBoxSource = new TextBoxSource(this, new Locator());
                    MDXParser.MDXParser mDXParser = new MDXParser.MDXParser(text, textBoxSource, new CubeInfo());
                    mDXParser.ColorCode();
                    if (mDXParser.HasColorCoding())
                    {
                        this.m_ColorCoding = mDXParser.GetColorCoding();
                        string rTF = this.m_ColorCoding.ConvertToRTF(text);
                        try
                        {
                            MDXStudio.QueryEditor.NativeMethods.NestedLockWindowUpdate(base.Handle);
                            int firstVisibleLine = TextBoxAPIHelper.GetFirstVisibleLine(this);
                            if (rTF == null)
                            {
                                this.ForeColor = Color.Black;
                                this.BackColor = Color.White;
                                base.SelectionBackColor = Color.White;
                                this.Text = text;
                            }
                            else
                            {
                                base.Rtf = rTF;
                            }
                            base.SelectionStart = selectionStart;
                            int num = TextBoxAPIHelper.GetFirstVisibleLine(this);
                            if (num <= firstVisibleLine)
                            {
                                for (int i = 0; i < firstVisibleLine - num; i++)
                                {
                                    TextBoxAPIHelper.ScrollLineDown(this);
                                }
                            }
                            else
                            {
                                for (int j = 0; j < num - firstVisibleLine; j++)
                                {
                                    TextBoxAPIHelper.ScrollLineUp(this);
                                }
                            }
                        }
                        finally
                        {
                            MDXStudio.QueryEditor.NativeMethods.NestedUnlockWindowUpdate();
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                WigglyLines wigglyLines = null;
                if (tag != null)
                {
                    wigglyLines = tag.WigglyLines;
                }
                this.mUndoStack.Push(new UndoRedoEntry(base.Rtf, selectionStart, 0, wigglyLines));
                this.mRedoStack.Clear();
            }
            finally
            {
                this.mChangingFlag = false;
            }
        }

        public new void Redo()
        {
            this.MakeRedo();
        }

        public void ShowToolTip(string text, int duration)
        {
            Point y = TextBoxAPIHelper.PosFromChar(this, base.SelectionStart);
            y.Y = y.Y + this.Font.Height;
            this.mToolTip.Show(text, this, y, duration);
        }

        public void ShowToolTip(string text)
        {
            Point y = TextBoxAPIHelper.PosFromChar(this, base.SelectionStart);
            y.Y = y.Y + this.Font.Height + 4;
            this.mToolTip.Show(text, this, y);
        }

        private void SyntaxAnalysis(List<TextSegment> pList)
        {
            ParserUtil.CheckRegExsLoadList(this.Text, pList);
            if (pList.Count > 1)
            {
                this.FilterList(pList);
            }
            this.CompleteList(pList, this.Text);
        }

        public new void Undo()
        {
            this.MakeUndo();
        }
    }
}