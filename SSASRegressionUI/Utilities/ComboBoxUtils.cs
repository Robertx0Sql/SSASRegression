using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
namespace SSASRegressionUI
{
    internal class ComboBoxUtils
    {
        public static void ResizeComboBoxDropDown(ComboBox senderComboBox)
        {
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
            (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
            ? SystemInformation.VerticalScrollBarWidth : 0;
            int newWidth;

            foreach (string s in (senderComboBox).Items)
            {
                newWidth = (int)g.MeasureString(s, font).Width
                + vertScrollBarWidth;

                if (width < newWidth)
                {
                    width = newWidth;
                }
            }

            senderComboBox.DropDownWidth = width;
        }
    }
}
