using System;

namespace MdxQueryEditor
{
    public interface IQueryEditor
    {
        bool CanUndo { get; }
        bool Changed { get; set; }
        void CommentSelection();
        void Copy();
        void Cut();
        void FormatClick(bool IsExpression);
       // MDXParser.FormatOptions formatOptions { get; set; }
        
        void ParseClick(bool IsExpression);
        void Paste();
        void Redo();
        void SelectAll();
        string SelectedText { get; set; }
        string Text { get; set; }
        void UncommentSelection();
        void Undo();
        void ShowOptions();
    }

}
