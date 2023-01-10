using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace VirtualDataGridView
{
	internal class VirtualGrid
	{
		private readonly DataGridView DisplayGrid;

		//private readonly Context AxesContext;

		internal CellSet CellSetBinding;

		private int colCount;

		private int colDepth;

		private int rowCount;

		private int rowDepth;

		private int rowCountCurrent;

		private bool m_fRowsIncludeHierarchyName;

		private bool m_fShowFormattedValue;

		private readonly ContextMenuStrip mContextMenu;

		internal string CellProperty;

        private bool _IsCellError;
        
        public VirtualGrid(DataGridView GridControl)//, Context in_AxesContext)
		{
			this.DisplayGrid = GridControl;
			//this.AxesContext = in_AxesContext;
			this.mContextMenu = this.BuildContextMenu();
			this.DisplayGrid.ContextMenuStrip = this.mContextMenu;
		}



        public bool IsCellError
        {
            get
            {
                return _IsCellError;
            }
        }
        private ContextMenuStrip BuildContextMenu()
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            //ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Coordinates", null, new EventHandler(this.OnContextMenuClick), "mItemCoordinates");
            //contextMenuStrip.Items.Add(toolStripMenuItem);
            //toolStripMenuItem = new ToolStripMenuItem("Clear watches", null, new EventHandler(this.OnContextMenuClick), "mItemClearWatches");
            //contextMenuStrip.Items.Add(toolStripMenuItem);
            //toolStripMenuItem = new ToolStripMenuItem("Debug", null, new EventHandler(this.OnContextMenuClick), "mItemDebug");
            //contextMenuStrip.Items.Add(toolStripMenuItem);
            //toolStripMenuItem = new ToolStripMenuItem("Show Calculation", null, new EventHandler(this.OnContextMenuClick), "mItemShowCalculation");
            //contextMenuStrip.Items.Add(toolStripMenuItem);
            //contextMenuStrip.Opening += new CancelEventHandler(this.mContextMenuResultGrid_Opening);

            //ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("TEST1", null, new EventHandler(this.OnContextMenuClick), "mItemTest1");
            //contextMenuStrip.Items.Add(toolStripMenuItem);
            //toolStripMenuItem = new ToolStripMenuItem("TEST2", null, new EventHandler(this.OnContextMenuClick), "mItemTest2");
            //contextMenuStrip.Items.Add(toolStripMenuItem);

             ToolStripMenuItem toolStripMenuItem;
                        ToolStripSeparator toolStripSeparator;
                        
            toolStripMenuItem = new ToolStripMenuItem("Select All", null, new EventHandler(this.OnContextMenuClick), "mItemDataSelectAll");
            toolStripMenuItem.Enabled = true;
            toolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+A";//= Keys.C && Keys.Control;
            contextMenuStrip.Items.Add(toolStripMenuItem);

            toolStripSeparator = new ToolStripSeparator();
            contextMenuStrip.Items.Add(toolStripSeparator);

            toolStripMenuItem = new ToolStripMenuItem("Cut", Resource1.CutHS, new EventHandler(this.OnContextMenuClick), "mItemDataCut");
            toolStripMenuItem.Enabled = false;
            toolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+X";//= Keys.C && Keys.Control;
            contextMenuStrip.Items.Add(toolStripMenuItem);
            toolStripMenuItem = new ToolStripMenuItem("Copy", Resource1.CopyHS, new EventHandler(this.OnContextMenuClick), "mItemDataCopy");
            toolStripMenuItem.Enabled = true;
            toolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
            contextMenuStrip.Items.Add(toolStripMenuItem);
            toolStripMenuItem = new ToolStripMenuItem("Paste", Resource1.PasteHS, new EventHandler(this.OnContextMenuClick), "mItemDataPaste");
            toolStripMenuItem.Enabled = false;
            toolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+V";
            contextMenuStrip.Items.Add(toolStripMenuItem);


            return contextMenuStrip;
		}

		public void CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
            try
            {
                if (e.ColumnIndex >= this.colCount + this.rowDepth || e.RowIndex >= this.rowCount + this.colDepth)
                {
                    return;
                }
                if (e.ColumnIndex < this.rowDepth && e.RowIndex >= this.colDepth)
                {
                    Member item = this.CellSetBinding.Axes[1].Positions[e.RowIndex - this.colDepth].Members[e.ColumnIndex];
                    this.ShowMemberProperties(item);
                    return;
                }
                if (e.ColumnIndex < this.rowDepth || e.RowIndex >= this.colDepth)
                {
                    if (e.ColumnIndex >= this.rowDepth && e.RowIndex >= this.colDepth)
                    {
                        int rowIndex = (e.RowIndex - this.colDepth) * this.colCount + e.ColumnIndex - this.rowDepth;
                        this.ShowCellProperties(this.CellSetBinding.Cells[rowIndex]);
                    }
                    return;
                }
                int columnIndex = e.ColumnIndex - this.rowDepth;
                bool flag = true;
                if (this.m_fRowsIncludeHierarchyName)
                {
                    if (columnIndex % 2 == 0)
                    {
                        flag = false;
                    }
                    columnIndex = columnIndex / 2;
                }
                if (flag)
                {
                    Member member = this.CellSetBinding.Axes[0].Positions[columnIndex].Members[e.RowIndex];
                    this.ShowMemberProperties(member);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
		}
    
		public void CellEnter(object sender, DataGridViewCellEventArgs e, AdomdConnection Connection, string cube)
		{
    
			if (e.ColumnIndex >= this.colCount + this.rowDepth || e.RowIndex >= this.rowCount + this.colDepth)
			{
				return;
			}
			//this.AxesContext.Clear();
			if (e.ColumnIndex >= this.rowDepth && e.RowIndex < this.colDepth)
			{
				int columnIndex = e.ColumnIndex - this.rowDepth;
				bool flag = true;
				if (this.m_fRowsIncludeHierarchyName)
				{
					if (columnIndex % 2 == 0)
					{
						flag = false;
					}
					columnIndex = columnIndex / 2;
				}
				if (flag)
				{
				//	this.AxesContext.StartContext(Connection, cube);
					//this.AxesContext.UpdateCoordinates(this.CellSetBinding.Axes[0].Positions[columnIndex), this.CellSetBinding.OlapInfo.AxesInfo .Axes[0));
				}
				return;
			}
			if (e.ColumnIndex < this.rowDepth && e.RowIndex >= this.colDepth)
			{
				//this.AxesContext.StartContext(Connection, cube);
				//this.AxesContext.UpdateCoordinates(this.CellSetBinding.Axes[1].Positions[e.RowIndex - this.colDepth), this.CellSetBinding.OlapInfo.AxesInfo .Axes[1));
				return;
			}
			if (e.ColumnIndex >= this.rowDepth && e.RowIndex >= this.colDepth)
			{
				int rowIndex = e.RowIndex - this.colDepth;
				int num = e.ColumnIndex - this.rowDepth;
				//this.AxesContext.StartContext(Connection, cube);
				if (this.CellSetBinding.Axes.Count > 0)
				{
					//this.AxesContext.UpdateCoordinates(this.CellSetBinding.Axes[0].Positions[num), this.CellSetBinding.OlapInfo.AxesInfo .Axes[0));
				}
                if (this.CellSetBinding.Axes.Count > 1)
				{
					//this.AxesContext.UpdateCoordinates(this.CellSetBinding.Axes[1].Positions[rowIndex), this.CellSetBinding.OlapInfo.AxesInfo .Axes[1));
				}
			}
		}

		public void CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
		{
			if (e.ColumnIndex >= this.colCount + this.rowDepth || e.RowIndex >= this.rowCount + this.colDepth)
			{
				return;
			}
			if (e.ColumnIndex < this.rowDepth && e.RowIndex >= this.colDepth)
			{
				Member item = this.CellSetBinding.Axes[1].Positions[e.RowIndex - this.colDepth].Members[e.ColumnIndex];
				e.Value = item.Caption;
			}
			if (e.ColumnIndex >= this.rowDepth && e.RowIndex < this.colDepth)
			{
				int columnIndex = e.ColumnIndex - this.rowDepth;
				bool flag = true;
				if (this.m_fRowsIncludeHierarchyName)
				{
					if (columnIndex % 2 == 0)
					{
						flag = false;
					}
					columnIndex = columnIndex / 2;
				}
				if (!flag)
				{
					e.Value = this.CellSetBinding.OlapInfo.AxesInfo .Axes[0].Hierarchies[e.RowIndex].Name;
				}
				else
				{
					Member member = this.CellSetBinding.Axes[0].Positions[columnIndex].Members[e.RowIndex];
					e.Value = member.Caption;
				}
			}
			if (e.ColumnIndex >= this.rowDepth && e.RowIndex >= this.colDepth)
			{
				int rowIndex = (e.RowIndex - this.colDepth) * this.colCount + e.ColumnIndex - this.rowDepth;
				Cell cell = this.CellSetBinding.Cells[rowIndex];
				try
				{
					if (this.CellProperty != null)
					{
						e.Value = cell.CellProperties[this.CellProperty].Value;
					}
					else if (!this.m_fShowFormattedValue)
					{
						e.Value = cell.Value;
					}
					else
					{
						e.Value = cell.FormattedValue;
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					e.Value = "#Error";
                    _IsCellError = true;
					DataGridViewCell message = this.DisplayGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
					message.ToolTipText = exception.Message;
				}
			}
		}
        


		public void DrawCellSetInGrid(CellSet csResult, MessageLog MessageLog, bool fRowsIncludeHierarchyName)
		{
            _IsCellError = false;
			int num;
			try
			{
				this.DisplayGrid.SuspendLayout();
				this.DisplayGrid.Columns.Clear();
				this.DisplayGrid.Rows.Clear();
				this.CellSetBinding = csResult;
                if (csResult != null)
                {
                    this.m_fShowFormattedValue = null != this.CellSetBinding.OlapInfo.CellInfo.CellProperties.Find("FORMATTED_VALUE");
                    this.m_fRowsIncludeHierarchyName = fRowsIncludeHierarchyName;
                    if (csResult.Axes.Count > 2)
                    {
                        MessageLog.WriteErrorMessage("Too many axes in the result.");
                    }
                    else if (csResult.Axes.Count != 0)
                    {
                        this.colCount = csResult.Axes[0].Positions.Count;
                        this.colDepth = 0;
                        if (!this.DisplayGrid.ColumnHeadersVisible)
                        {
                            this.colDepth = csResult.OlapInfo.AxesInfo.Axes[0].Hierarchies.Count;
                        }
                        this.rowCount = 0;
                        this.rowDepth = 0;
                        if (csResult.Axes.Count <= 1)
                        {
                            this.rowCount = 1;
                            this.rowDepth = 0;
                        }
                        else
                        {
                            this.rowCount = csResult.Axes[1].Positions.Count;
                            this.rowDepth = csResult.OlapInfo.AxesInfo.Axes[1].Hierarchies.Count;
                        }
                        for (int i = 0; i < this.rowDepth; i++)
                        {
                            this.DisplayGrid.Columns.Add(string.Concat("rowColumn", i + 1), csResult.OlapInfo.AxesInfo.Axes[1].Hierarchies[i].Name);
                            this.DisplayGrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        }
                        for (int j = 0; j < this.colCount; j++)
                        {
                            if (fRowsIncludeHierarchyName)
                            {
                                this.DisplayGrid.Columns.Add(string.Concat("columnHierarchy", j + 1), "");
                                this.DisplayGrid.Columns[this.rowDepth + j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            }
                            if (this.DisplayGrid.Columns.Count <= 0)
                            {
                                this.DisplayGrid.Columns.Add(".", csResult.Axes[0].Positions[j].Members[0].Caption);
                            }
                            else
                            {
                                DataGridViewColumn dataGridViewColumn = new DataGridViewColumn();
                                dataGridViewColumn.FillWeight = 1f;
                                if (!this.DisplayGrid.ColumnHeadersVisible)
                                {
                                    dataGridViewColumn.Name = ".";
                                }
                                else
                                {
                                    dataGridViewColumn.Name = csResult.Axes[0].Positions[j].Members[0].Caption;
                                }
                                dataGridViewColumn.CellTemplate = this.DisplayGrid.Columns[0].CellTemplate;
                                this.DisplayGrid.Columns.Add(dataGridViewColumn);
                            }
                            this.DisplayGrid.Columns[this.rowDepth + j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        }
                        this.rowCountCurrent = this.rowCount;
                        this.DisplayGrid.RowCount = this.colDepth + this.rowCountCurrent;
                        for (int k = 0; k < this.colDepth; k++)
                        {
                            string caption = null;
                            for (int l = 0; l < this.colCount; l++)
                            {
                                num = (fRowsIncludeHierarchyName ? 1 : 0);
                                int num1 = num;
                                DataGridViewCell item = this.DisplayGrid.Rows[k].Cells[this.rowDepth + l + num1];
                                Member member = csResult.Axes[0].Positions[l].Members[k];
                                if (member.Caption != caption)
                                {
                                    item.Value = member.Caption;
                                    caption = member.Caption;
                                }
                                if (fRowsIncludeHierarchyName)
                                {
                                    item = this.DisplayGrid.Rows[k].Cells[this.rowDepth + l];
                                    item.Value = csResult.OlapInfo.AxesInfo.Axes[0].Hierarchies[k].Name;
                                }
                            }
                        }
                        if (fRowsIncludeHierarchyName)
                        {
                            VirtualGrid virtualGrid = this;
                            virtualGrid.colCount = virtualGrid.colCount * 2;
                        }
                        if (!fRowsIncludeHierarchyName)
                        {
                            //object[] objArray = new object[] { "Displaying ", this.colCount, " columns and ", this.rowCount, " rows." };
                            //MessageLog.WriteMessage(string.Concat(objArray));
                        }
                    }
                    else
                    {
                        MessageLog.WriteMessage("Query result is empty.");
                    }
                }
			}
			finally
			{
				this.DisplayGrid.AutoResizeColumns();
				this.DisplayGrid.ResumeLayout();
			}
		}

		private VirtualGrid.eArea GetArea(int pColIndex, int pRowIndex)
		{
			if (pColIndex >= this.rowDepth && pColIndex < this.rowDepth + this.colCount && pRowIndex >= this.colDepth && pRowIndex < this.colDepth + this.rowCount)
			{
				return VirtualGrid.eArea.Cells;
			}
			if (pColIndex >= this.rowDepth && pColIndex < this.rowDepth + this.colCount && pRowIndex < this.colDepth)
			{
				return VirtualGrid.eArea.Cols;
			}
			if (pColIndex < this.rowDepth && pRowIndex >= this.colDepth && pRowIndex < this.colDepth + this.rowCount)
			{
				return VirtualGrid.eArea.Rows;
			}
			return VirtualGrid.eArea.None;
		}

		private void mContextMenuResultGrid_Opening(object sender, CancelEventArgs e)
		{
			foreach (ToolStripMenuItem item in this.mContextMenu.Items)
			{
			//	item.Visible = false;
			}
			DataGridViewCell currentCell = this.DisplayGrid.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			switch (this.GetArea(currentCell.ColumnIndex, currentCell.RowIndex))
			{
				case VirtualGrid.eArea.None:
				{
					e.Cancel = true;
					return;
				}
				case VirtualGrid.eArea.Rows:
				case VirtualGrid.eArea.Cols:
				{
				//	this.mContextMenu.Items["mItemClearWatches"].Visible = true;
					return;
				}
				case VirtualGrid.eArea.Cells:
				{
					e.Cancel = true;
					return;
				}
				default:
				{
					return;
				}
			}
		}

		private void OnContextMenuClick(object sender, EventArgs e)
		{
			string name = ((ToolStripMenuItem)sender).Name;
			string str = name;
			if (name != null)
			{
                if (str == "mItemDataSelectAll")
                {
                    DisplayGrid.SelectAll();
                }
                if (str == "mItemDataCopy")
                {
                    //Program.m_MainForm.ClearDebugWatch();
                    try
                    {
                        if (this.DisplayGrid.RowCount > 0)
                        {
                            DisplayGrid.SelectAll();

                            Clipboard.SetDataObject(
                                 this.DisplayGrid.GetClipboardContent());
                        }
                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {
                        //throw new Exception(                             "The Clipboard could not be accessed.");
                    }
                }
                else if (!(str == "mItemDebug") && !(str == "mItemShowCalculation"))
                {
                    return;
                }
			}
		}

		public void Redraw()
		{
			this.DisplayGrid.Invalidate();
		}

		public void Scroll(ScrollEventArgs e)
		{
			if (this.rowCountCurrent >= this.rowCount)
			{
				return;
			}
			if (e.ScrollOrientation != ScrollOrientation.VerticalScroll)
			{
				return;
			}
			if (e.NewValue <= e.OldValue)
			{
				return;
			}
			int newValue = e.NewValue + 256;
			if (newValue > this.rowCount)
			{
				newValue = this.rowCount;
			}
			if (newValue <= this.rowCountCurrent)
			{
				return;
			}
			DataGridView displayGrid = this.DisplayGrid;
			displayGrid.RowCount = displayGrid.RowCount + newValue - this.rowCountCurrent;
			this.rowCountCurrent = newValue;
		}

		public void SetContextMenu()
		{
			DataGridViewCell currentCell = this.DisplayGrid.CurrentCell;
		}

		private void ShowCellProperties(Cell cl)
		{
            CellPropertiesWindow cellPropertiesWindow = new CellPropertiesWindow();
			CellPropertyCollection.Enumerator enumerator = cl.CellProperties.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Microsoft.AnalysisServices.AdomdClient.CellProperty current = enumerator.Current;
				string str = null;
				try
				{
					str = Convert.ToString(current.Value);
				}
				catch (Exception exception)
				{
					str = exception.Message;
				}
				cellPropertiesWindow.AddProperty(current.Name, str);
			}
			cellPropertiesWindow.Show();
		}

		private void ShowMemberProperties(Member member)
		{
			CellPropertiesWindow cellPropertiesWindow = new CellPropertiesWindow();
			cellPropertiesWindow.Text = "Member Properties";
			cellPropertiesWindow.AddProperty("Caption", member.Caption);
			cellPropertiesWindow.AddProperty("Name", member.Name);
			cellPropertiesWindow.AddProperty("Unique Name", member.UniqueName);
			cellPropertiesWindow.AddProperty("Level Name", member.LevelName);
			int levelDepth = member.LevelDepth;
			cellPropertiesWindow.AddProperty("Level Depth", levelDepth.ToString());
			long childCount = member.ChildCount;
			cellPropertiesWindow.AddProperty("Children Count", childCount.ToString());
            try
            {
                if (member.Parent != null)
                {
                    cellPropertiesWindow.AddProperty("Parent Caption", member.Parent.Caption);
                    cellPropertiesWindow.AddProperty("Parent Unique Name", member.Parent.UniqueName);
                }
            }
            catch { }
			bool drilledDown = member.DrilledDown;
			cellPropertiesWindow.AddProperty("Drilled Down", drilledDown.ToString());
			bool parentSameAsPrevious = member.ParentSameAsPrevious;
			cellPropertiesWindow.AddProperty("Parent Same as Previous", parentSameAsPrevious.ToString());
			MemberPropertyCollection.Enumerator enumerator = member.MemberProperties.GetEnumerator();
			while (enumerator.MoveNext())
			{
				MemberProperty current = enumerator.Current;
				string str = null;
				try
				{
					str = Convert.ToString(current.Value);
				}
				catch (Exception exception)
				{
					str = exception.Message;
				}
				cellPropertiesWindow.AddProperty(current.Name, str);
			}
			cellPropertiesWindow.Show();
		}

		private enum eArea
		{
			None,
			Rows,
			Cols,
			Cells
		}
	}
}