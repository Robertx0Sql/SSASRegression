using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.AdomdClient;
using System.Windows.Forms;

namespace VirtualDataGridView
{
    public delegate void CellEnterHandler(object sender, DataGridViewCellEventArgs e);

    public interface ICellSetDataGridView
    {
        event CellEnterHandler DataGrid_CellEnterEvent;

        string CellProperty { get; set; }
        bool IsCellError { get; }
        bool DataSourceValid {get;}
        List<string> CellPropertiesList { get; }
        void HighlightCell(int rowindex, int columnindex);
        void DrawCellSetInGrid(CellSet cs);
        void CopyData();
        void Redraw();
    }
}