using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SSASRegressionCL;

namespace SSASRegressionUI
{
    public partial class TestEditItem : UserControl
    {
        public TestEditItem()
        {
            InitializeComponent();
        }

        private int index = -1;

      
        public int Index
        {
            get { return index; }
            set { index = value; }
        }


        public string IndexValue
        {
            get
            {
                return textBox1.Text;// +"(" + Index.ToString() + ")";
            }
            set
            {
                textBox1.Text += value + ", ";
            }
        }

        public string Id
        {
            get { return txtID.Text; }
            set { txtID.Text = value; }
        }

        public bool isChanged
        {
            get
            {

                bool result;

                Test t = TestItem; //this can not return a null .
                if (_test == null) //could be null;
                {
                    if (t.Description != "" || t.MDX != "" || t.ID != "")
                        return true;
                    else
                        return false;
                }


                if (t.Description != _test.Description)
                    result = true;
                else
                    result = false;
                if (t.MDX != _test.MDX)
                    result = true;
                if (t.ID != _test.ID)
                    result = true;

                return result;
            }
        }
        Test _test;
        public Test TestItem
        {
            get
            {
                string id = txtID.Text;
                string MDX = txtMDX.Text;
                string Description = txtDescription.Text;
                return new Test(id, Description, MDX);
            }
            set
            {
                _test = value;
                if (value != null)
                {
                    txtID.Text = value.ID;
                    txtMDX.Text = value.MDX;
                    txtDescription.Text = value.Description;
                }
                else
                {
                    txtID.Text = "";
                    txtMDX.Text = "";
                    txtDescription.Text = "";
                }
            }
        }

        private void btnEditMDX_Click(object sender, EventArgs e)
        {
            frmEditMDX f = new frmEditMDX() { Mdx = txtMDX.Text };

            if (f.ShowDialog() == DialogResult.OK)
            {
                txtMDX.Text = f.Mdx;
            }
        }

      
    }
}
