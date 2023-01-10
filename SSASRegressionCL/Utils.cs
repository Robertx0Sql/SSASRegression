using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using Microsoft.AnalysisServices.AdomdClient;
using System.IO;
using System.Data;
using System.Text;

namespace SSASRegressionCL
{
    public static class Utils
    {
        public static string GetElementValue(XElement xElement, string elementName)
        {
            if (xElement == null)
                return null;
            else
            {
                XElement odt = xElement.Element(elementName);
                if (odt != null)
                    return odt.Value;
                else
                    return null;
            }
        }


        public static string GetAttributeValue(XElement xElement, string attributeName)
        {
            if (xElement == null)
                return null;
            else
            {
                XAttribute odt = xElement.Attribute(attributeName);
                if (odt != null)
                    return odt.Value;
                else
                    return null;
            }
        }


        public static DateTime GetAttributeDate(XElement xElement, string attributeName)
        {
            DateTime dt = new DateTime();
            string value = GetAttributeValue(xElement, attributeName);
            if (value != null)
                dt = Convert.ToDateTime(value);
            return dt;
        }

        public static CellSet GetCellSetfromXML(string xml)
        {
            if (xml == null)
                return null;
            else
            {
                XmlReader xmlReader2 = XmlReader.Create(new StringReader(xml));
                return CellSet.LoadXml(xmlReader2);
            }
        }

        public static int CellSetColumns(CellSet cst)
        {
            int columns = 0;
            if (cst != null)
            {
                if (cst.Axes.Count > 1)
                {
                    //Number of dimensions on the row
                    int rowDimCount = 0;
                    if (cst.Axes[1].Positions.Count > 0)
                    {
                        rowDimCount = cst.Axes[1].Positions[0].Members.Count;
                    }

                    //Total columns
                    columns = cst.Axes[0].Positions.Count + rowDimCount; //number of columns + columns for row headers
                }
            }
            return columns;
        }
        public static int CellSetRows(CellSet cst)
        {
            int rows = 0;
            if (cst != null)
            {
                if (cst.Axes.Count > 1)
                {
                    //Number of dimensions on the column
                    int columnDimCount = 0;
                    if (cst.Axes[0].Positions.Count > 0)
                    {
                        columnDimCount = cst.Axes[0].Positions[0].Members.Count;
                    }
                    rows = cst.Axes[1].Positions.Count + columnDimCount; //number of rows + rows for column headers
                }
            }
            return rows;
        }

        public static bool GetCellSetCounts(CellSet cst, out int rows, out int columns)
        {
            rows = CellSetRows(cst);
            columns = CellSetColumns(cst);
            if (cst != null)


                return true;
            else
                return false;
        }

        internal static bool ValidateExistingFilePath(string filetype, string filepath)
        {

            if (filepath.Trim() == "")
                throw new FileNotFoundException(String.Format("{0} file not specified", filetype));

            if (!File.Exists(filepath))
                throw new FileNotFoundException(string.Format("The {1} file '{0}' does not exist", filepath, filetype));
            return true;
        }
        internal static bool ValidateNewFilePath(string FileType, string filepath)
        {
            if (filepath.Trim() == "")
                throw new FileNotFoundException(String.Format("{0} file not specified", FileType));

            if (!Directory.Exists(Path.GetDirectoryName(filepath)) && Path.GetDirectoryName(filepath) != "")
                throw new FileNotFoundException(string.Format("The Directory '{1}' for {2} file '{0}' does not exist",
                                                    filepath,
                                                    Path.GetDirectoryName(filepath),
                                                    FileType));
            return true;
        }

        public static bool ValidateTestFilePath(string testFile)
        {
            string FileType = "Tests";
            return Utils.ValidateExistingFilePath(FileType, testFile);
        }
        public static bool ValidateExistingResultFilePath(string resultsFile)
        {
            string FileType = "Results";
            return Utils.ValidateExistingFilePath(FileType, resultsFile);
        }
        public static bool ValidateNewResultFilePath(string resultsFile)
        {
            string FileType = "Results";
            return Utils.ValidateNewFilePath(FileType, resultsFile);
        }



        //Code : utility code for converting cellset to a data table



        public static CellSetDataTable Cellset2Datatable(CellSet cs)
        {

            CellSetDataTable dt = new CellSetDataTable();
            if (cs == null)
                return dt;
            DataColumn dc;
            DataRow dr;
            int i;
            int j;
            int num = 0;
            int nNumberOfGroupingColumns = 0;

            int colDimCount = 0;
            int rowDimCount = 0;
            //Number of dimensions on the column
            if (cs.Axes[0].Positions.Count > 0)
                colDimCount = cs.Axes[0].Positions[0].Members.Count;

            //Number of dimensions on the row
            if (cs.Axes[1].Positions.Count > 0)
            {
                if (cs.Axes[1].Positions[0].Members.Count > 0)
                {
                    rowDimCount = cs.Axes[1].Positions[0].Members.Count;
                }
            }

            //Total rows and columns
            int rowCount = cs.Axes[1].Positions.Count + colDimCount; //number of rows + rows for column headers
            int colCount = cs.Axes[0].Positions.Count + rowDimCount; //number of columns + columns for row headers

            //  string[,] rowDimNames = new string[rowDimCount, colCount];

            if ((cs.Axes.Count > 1))
            {
                //add Column for row dimensions 
                foreach (Member m in cs.Axes[1].Positions[0].Members)
                {
                    num++;
                    dc = new DataColumn();
                    dc.ColumnName = "row dim" + num.ToString();//m.Caption; // GetColumnName(num);
                    dt.Columns.Add(dc);
                    nNumberOfGroupingColumns++;
                }
            }

            string sCaption;
            foreach (Position p in cs.Axes[0].Positions)
            {
                sCaption = "";
                foreach (Member m in p.Members)
                {
                    if (sCaption.Equals(""))
                    {
                        sCaption = string.Format("[{0}]", m.Caption.Trim());
                    }
                    else
                    {
                        sCaption = string.Format("{0} / [{1}]", sCaption, m.Caption.Trim());
                    }
                }
                num++;
                dc = new DataColumn();
                dc.ColumnName = sCaption + "p" + num.ToString();//  GetColumnName(num);
                //   dc.Caption = sCaption;
                dt.Columns.Add(dc);
            }
            for (int xr = 0; xr < colDimCount; xr++)
            {
                dr = dt.NewRow();
                for (int xc = 0; xc < colCount - rowDimCount; xc++)
                {
                    Member m = cs.Axes[0].Positions[xc].Members[xr];
                    dr[xc + rowDimCount] = m.Caption;




                }
                dt.Rows.Add(dr);
            }

            // import data
            int x;
            int y;
            Position py;
            if ((nNumberOfGroupingColumns > 0))
            {
                for (y = 0; (y
                            <= (cs.Axes[1].Positions.Count - 1)); y++)
                {
                    py = cs.Axes[1].Positions[y];
                    i = 0;
                    dr = dt.NewRow();
                    foreach (Member m in py.Members)
                    {
                        dr[i] = m.Caption;
                        i++;
                    }
                    for (x = 0; (x
                                <= (cs.Axes[0].Positions.Count - 1)); x++)
                    {
                        //       dr[i] = cs[x, y].FormattedValue;
                        try
                        {
                            dr[i] = cs[x, y].FormattedValue;
                            //other cells in the row
                        }
                        catch (Exception exception1)
                        {
                            Exception exception = exception1;
                            dr[i] = "#Error ";//+ "\r\n" + exception1.Message;
                            dt.IsCellError = true;
                            //   DataGridViewCell message = this.DisplayGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                            // message.ToolTipText = exception.Message;
                        }

                        i++;
                    }
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                dr = dt.NewRow();
                for (i = 0; (i
                            <= (cs.Axes[0].Positions.Count - 1)); i++)
                {
                    //                    dr[i] = cs[i].FormattedValue;
                    try
                    {
                        dr[i] = cs[i].FormattedValue;
                        //other cells in the row
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        dr[i] = "#Error ";// +"\r\n" + exception1.Message;
                        dt.IsCellError = true;
                        //   DataGridViewCell message = this.DisplayGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        // message.ToolTipText = exception.Message;
                    }

                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
   
        public static bool IsNullOrEmptyOrWhitespace(this string s)
        {
        
            return string.IsNullOrEmpty(s) || (s.Trim().Length == 0);

            // .Net 4.0 required:
            //return string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s);

        }        
        public static Dictionary<string, string> ConnectionStringProperties(string connectionString)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            try
            {
                string[] array = connectionString.Split(';');
                foreach (string item in array)
                {
                    string[] param = item.Split('=');
                    if (param.Length == 2)
                    {
                        result.Add(param[0].Replace(" ","").ToLower(), param[1]);
                    }
                }
               }
            catch { }
            return result;
        }
        public static string ConnectionStringCatalog(string connectionString)
        {
            string _catalogName="";
            Dictionary<string, string> connProperties = SSASRegressionCL.Utils.ConnectionStringProperties(connectionString);
           connProperties.TryGetValue("InitialCatalog".ToLower(), out _catalogName);
            if (_catalogName == "")
                connProperties.TryGetValue("Catalog".ToLower(), out _catalogName);
            return _catalogName;
         }
        public static string ConnectionStringServer(string connectionString)
        {
            string _server = "";
            Dictionary<string, string> connProperties = SSASRegressionCL.Utils.ConnectionStringProperties(connectionString);
            connProperties.TryGetValue("DataSource".ToLower(), out _server);
            return _server;
        }
    }
}
