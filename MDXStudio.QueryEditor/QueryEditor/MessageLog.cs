using MDXParser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace MDXStudio
{
    internal class MessageLog
    {
        private DataGridView m_GridMessages;


        private bool m_Enabled;

        public bool Enabled
        {
            get
            {
                return this.m_Enabled;
            }
            set
            {
                this.m_Enabled = value;
            }
        }

        public MessageLog(DataGridView d)
        {
            this.m_GridMessages = d;
            
            this.m_Enabled = true;
        }

        public void Clear()
        {
            if (!this.m_Enabled)
            {
                return;
            }
            this.m_GridMessages.Rows.Clear();
        }

        private void OnMessagesContextMenuClick(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
            string name = toolStripMenuItem.Name;
            string str = name;
            if (name != null)
            {
                if (str == "mItemHide")
                {
                    DataGridViewRow tag = toolStripMenuItem.Tag as DataGridViewRow;
                    DataGridView dataGridView = tag.DataGridView;
                    if (dataGridView.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow selectedRow in dataGridView.SelectedRows)
                        {
                            if (selectedRow.Tag == null)
                            {
                                continue;
                            }
                            (selectedRow.Tag as MDXParser.Message).Hide = true;
                        }
                    }
                    else if (tag.Tag != null)
                    {
                        (tag.Tag as MDXParser.Message).Hide = true;
                    }
                    this.Populate(dataGridView.Tag as MessageCollection);
                    return;
                }
                if (str == "mItemHideAll")
                {
                    DataGridViewRow dataGridViewRow = toolStripMenuItem.Tag as DataGridViewRow;
                    DataGridView dataGridView1 = dataGridViewRow.DataGridView;
                    if (dataGridViewRow.Tag != null)
                    {
                        int id = (dataGridViewRow.Tag as MDXParser.Message).Id;
                        foreach (DataGridViewRow row in (IEnumerable)dataGridView1.Rows)
                        {
                            if (row.Tag == null)
                            {
                                continue;
                            }
                            MDXParser.Message message = row.Tag as MDXParser.Message;
                            if (message.Id != id)
                            {
                                continue;
                            }
                            message.Hide = true;
                        }
                        this.Populate(dataGridView1.Tag as MessageCollection);
                        return;
                    }
                }
                else
                {
                    if (str != "mItemHelp")
                    {
                        return;
                    }
                    Process.Start(toolStripMenuItem.Tag.ToString());
                }
            }
        }

        public void Populate(MessageCollection messages)
        {
            if (!this.m_Enabled)
            {
                return;
            }
            DataGridViewColumn sortedColumn = this.m_GridMessages.SortedColumn;
            SortOrder sortOrder = this.m_GridMessages.SortOrder;
            this.Clear();
            this.m_GridMessages.Tag = messages;
            foreach (MDXParser.Message message in messages)
            {
                if (message.Hide)
                {
                    continue;
                }
                DataGridViewRow dataGridViewRow = new DataGridViewRow();
                dataGridViewRow.CreateCells(this.m_GridMessages);
                dataGridViewRow.Cells[0].Value = message.Severity;
                if (message.Node != null)
                {
                    dataGridViewRow.Cells[1].Value = message.Node.Locator.Line;
                    dataGridViewRow.Cells[2].Value = message.Node.Locator.Column;
                }
                dataGridViewRow.Cells[3].Value = message.Text;
                if (message.URL != null)
                {
                    dataGridViewRow.Cells[4].Value = "More info";
                    dataGridViewRow.Cells[4].Tag = message.URL;
                }
                dataGridViewRow.Tag = message;
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Hide this message", null, new EventHandler(this.OnMessagesContextMenuClick), "mItemHide")
                {
                    Tag = dataGridViewRow
                };
                contextMenuStrip.Items.Add(toolStripMenuItem);
                toolStripMenuItem = new ToolStripMenuItem("Hide all messages of this type", null, new EventHandler(this.OnMessagesContextMenuClick), "mItemHideAll")
                {
                    Tag = dataGridViewRow
                };
                contextMenuStrip.Items.Add(toolStripMenuItem);
                if (message.URL != null)
                {
                    toolStripMenuItem = new ToolStripMenuItem("Show help", null, new EventHandler(this.OnMessagesContextMenuClick), "mItemHelp")
                    {
                        Tag = message.URL
                    };
                    contextMenuStrip.Items.Add(toolStripMenuItem);
                }
                dataGridViewRow.ContextMenuStrip = contextMenuStrip;
                this.m_GridMessages.Rows.Add(dataGridViewRow);
            }
            if (sortOrder != SortOrder.None)
            {
                ListSortDirection listSortDirection = ListSortDirection.Ascending;
                if (sortOrder == SortOrder.Descending)
                {
                    listSortDirection = ListSortDirection.Descending;
                }
                this.m_GridMessages.Sort(sortedColumn, listSortDirection);
            }
           // this.Show();
        }

        public void WriteErrorMessage(string message)
        {
            if (!this.m_Enabled)
            {
                return;
            }
            this.WriteMessage(message);
           // this.m_DC.Show();
        }

        public void WriteMessage(string message)
        {
            if (!this.m_Enabled)
            {
                return;
            }
            DataGridViewRow dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.CreateCells(this.m_GridMessages);
            dataGridViewRow.Cells[3].Value = message;
            this.m_GridMessages.Rows.Add(dataGridViewRow);
        }

        public void WriteMessage(int line, int col, string message)
        {
            if (!this.m_Enabled)
            {
                return;
            }
            DataGridViewRow dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.CreateCells(this.m_GridMessages);
            dataGridViewRow.Cells[1].Value = line;
            dataGridViewRow.Cells[2].Value = col;
            dataGridViewRow.Cells[3].Value = message;
            this.m_GridMessages.Rows.Add(dataGridViewRow);
        }

        public void WriteMessage(MDXParser.Message m)
        {
            if (!this.m_Enabled)
            {
                return;
            }
            DataGridViewRow dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.CreateCells(this.m_GridMessages);
            dataGridViewRow.Cells[1].Value = m.Location.Line;
            dataGridViewRow.Cells[2].Value = m.Location.Column;
            dataGridViewRow.Cells[3].Value = m.Text;
            dataGridViewRow.Tag = m;
            this.m_GridMessages.Rows.Add(dataGridViewRow);
        }
    }
}