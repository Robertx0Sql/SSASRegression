using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SSASRegressionUI
{
    public static class FileUtil
    {
        public static bool GetOpenFilePath(string FileType, ref string filepath)
        {
            bool result = false;
            //Open Dialog - get file
            try
            {

                OpenFileDialog openFileDialog1 = new OpenFileDialog() { Filter = "xml file (*.xml)|*.xml", Title = String.Format("Open {0} File", FileType.Trim()) };
                openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName != "")
                {
                    filepath = openFileDialog1.FileName;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Open File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }
    }
}
