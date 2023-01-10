using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SSASRegressionCL;

namespace SSASRegressionUI
{
    public interface IDataGridViewCellSetAdvanced
    {
        event EventHandler PropertyListChanged;
        event CellEnterHandler DataGrid_CellEnterEvent;
        int ColumnIndex { get; }
        int RowIndex { get; }
        void HighlightCell(int rowindex, int columnindex);
        TestResult result { get; set; }
        List<string> CellPropertiesList { get; }
        bool AllowUserToAddRows { get; set; }
        bool AllowUserToDeleteRows { get; set; }
        DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode { get; set; }
        string CellProperty { get; set; }
    }
}
