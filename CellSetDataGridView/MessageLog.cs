using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualDataGridView
{
    class MessageLog
    {
        internal static void WriteErrorMessage(string p)
        {
            Console.WriteLine("ERROR: " + p);
        }

        internal static void WriteMessage(string p)
        {
            Console.WriteLine(">>: " + p);
        }
    }
}
