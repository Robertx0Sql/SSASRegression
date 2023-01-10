using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VirtualDataGridView
{
    public partial class CellPropertiesWindow : Form
    {
        public CellPropertiesWindow()
        {
            InitializeComponent();
        }
        

        public void AddProperty(string name, string value)
        {
            ListViewItem listViewItem = new ListViewItem(name);
            listViewItem.SubItems.Add(value);
            this.listViewProperties.Items.Add(listViewItem);

           // this.propertyGrid1.
        }
    }
}
