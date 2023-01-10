using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.AdomdClient;
using System.Xml;
using System.Text;
using System.Xml.Serialization;

namespace SSASRegressionCL
{
    /// <summary>
    /// implements a Test with additional Attributes and a Sub Result.
    /// </summary>
    [Serializable]
    [XmlType("test")]
    public class TestResult : ITest, ITestResult
    {
        private const string cstdateformat = "dd-MM-yy HH:mm:ss"; 
        
        public TestResult() { }
        /// <summary>
        /// Implements a basic Test 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <param name="mdx"></param>
        private TestResult(string id, string description, string mdx)
        {
            ID = id;
            MDX = mdx;
            Description = description;
        }
        private TestResult(string id, string description, string mdx, string data, DateTime startDate, DateTime endDate, string error)
            : this(id, description, mdx)
        {
            Data = data;
            StartDate = startDate;
            EndDate = endDate;
            Error = error;
        }

        public TestResult(Test test, string data, DateTime startDate, DateTime endDate)
            : this(test.ID, test.Description, test.MDX, data, startDate, endDate, null)
        {
            //Data = data;
            //this.StartDate = StartDate;
            //this.EndDate = EndDate;
        }

        public TestResult(string id, string description, string mdx, string data, DateTime startDate, DateTime endDate)
            : this(id, description, mdx, data, startDate, endDate, null)
        {
            //Data = data;
            //StartDate = startDate;
            //EndDate = endDate;
        }
        public TestResult(Test test, DateTime startDate, DateTime endDate, string error)
            : this(test.ID, test.Description, test.MDX, null, startDate, endDate, error)
        {
            //Data = null;
            //StartDate = startDate;
            //EndDate = endDate;
            //Error = error;
        }


        private string _id;
        private string _description;
        private string _mdx;
        private Result _result;
     
        public override string ToString()
        {
            return String.Format("{0} [{1}]", Description, ID);
        }
     
        [XmlAttribute(AttributeName = "id")]
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (value == string.Empty || value == null)
                    _id = Guid.NewGuid().ToString().ToUpper();
                else
                    _id = value;
            }
        }

        [XmlElement(ElementName = "description")]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        [XmlElement(ElementName = "mdx")]
        public string MDX
        {
            get
            {
                return _mdx;
            }
            set
            {//fix old files where NewLines are not saved correctly as \r\n but as \n only
                if (_mdx == null)
                    _mdx = value.Replace("\n", Environment.NewLine).Replace("\r\r\n", Environment.NewLine);
                else
                    _mdx = value;
            }
        }

        [XmlElement(ElementName = "result")]
        public Result result
        {
            get
            {
                if (_result == null)
                    _result = new Result();
                return _result;
            }
            set
            {
                if (_result == null)
                    _result = new Result();
                _result = value;
            }
        }
        
        [XmlIgnore]
        public bool isResultExecuted
        {
            get
            {
                return (_result != null);
            }
        }
        [XmlIgnore]
        public string Data
        {
            get
            {
                if (result != null) return result.Data;
                else
                    return null;
            }
            set {  if (result != null) result.Data = value; }
        }

        [XmlIgnore]
        public DateTime EndDate
        {
            get
            { 
                if (result != null)
                    return result.EndDate;
                else
                    return DateTime.MinValue;
            }
            set
            {
                if (result != null)
                    result.EndDate = value;
            }
        }
        
        [XmlIgnore]
        public DateTime StartDate
        {
            get
            {
                if (result != null)
                    return result.StartDate; 
                else
                    return DateTime.MinValue;
            }
            set
            {
                if (result != null) 
                    result.StartDate = value;
            }
        }

        [XmlIgnore]
        public string Error
        {
            get
            {
                if (result != null)
                    return result.Error;
                else return null;
            }
            set
            {
                if (result != null) result.Error = value;
            }
        }

        [XmlIgnore]
        public bool IsError
        {
            get
            {
                if (result != null)
                    return result.IsError;
                else
                    return false;
            }
        }

        [XmlIgnore]
        public string ServerName { get; set; }

        [XmlIgnore]
        public string CatalogName { get; set; }

        [XmlIgnore]
        public CellSet cellSet
        {
            get
            {
                if (Data == null || Data == string.Empty)
                    return null;
                else
                {
                    CellSet c = null;
                    try
                    {
                        c = Utils.GetCellSetfromXML(Data);
                    }
                    catch (Exception ex)
                    {
                        string message = String.Format("Error De-Serializing CellSet from Data Element for Test '{0}' \r\nServer= {1}\r\nCatalog= {2}\r\n\r\nCellSet Error= {3}", this.Description, this.ServerName, this.CatalogName, ex.Message);
                        throw new Exception(message, ex);
                    }
                    return c;
                }
            }
        }

        [XmlIgnore]
        public TimeSpan QueryTimeSpan
        {
            get
            {
                TimeSpan ts=new TimeSpan();
                ts= ((DateTime) EndDate - (DateTime) StartDate);
                return ts;
            }

        }

        [XmlIgnore]
        public string QueryTimeFormated
        {
            get
            {
                TimeSpan ts = QueryTimeSpan;

                string ms = (ts.Milliseconds).ToString();
                ms = (ms.Length > 3) ? ms.Substring(0, 3) : ms;

                string seconds = ((int)ts.TotalSeconds).ToString();

                // return (ts.Hours).ToString() + ":" + (ts.Minutes).ToString() + ":" + (ts.Seconds).ToString() + "." + ms;
                return String.Format("{0}.{1} (s)", seconds, ms);
            }
        }
    }

    /// <summary>
    /// This is the Result of the Test. it contains the Execution DateTimes and the result Data.
    /// </summary>
    [Serializable]
    [XmlType("result")]
    public class Result : SSASRegressionCL.IResult
    {
        private string _error;
        private bool _dataErrorChecked = false;

        [XmlAttribute(AttributeName = "EndDate")]
        public DateTime EndDate { get; set; }

        [XmlAttribute(AttributeName = "StartDate")]
        public DateTime StartDate { get; set; }

        [XmlAttribute(AttributeName = "error")]
        public string Error
        {
            get
            {
                GetErrorfromData();
                if (_error == null)
                    return string.Empty;
                else
                    return _error;

                ;
            }
            set { _error = value; }
        }

        [XmlElement(ElementName = "data", IsNullable = true)]
        public string Data { get; set; }

        public bool IsError
        {
            get
            {
                GetErrorfromData();
                if (_error == null || _error == string.Empty)
                    return false;
                else
                    return true;

            }
        }

        /// <summary>
        /// Check the Data for Errors if the error is not already set
        /// </summary>
        void GetErrorfromData()
        {
            if (_dataErrorChecked)
                return;
            else
            {
                if ((_error == null || _error == string.Empty) && !(Data == null || Data == string.Empty))
                {
                    try
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(Data);
                        XmlNode root = doc.DocumentElement;

                        XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);

                        nsmgr.AddNamespace("ex", "urn:schemas-microsoft-com:xml-analysis:exception");

                        XmlNode errornode = root.SelectSingleNode("ex:Messages/ex:Error", nsmgr);

                        if (errornode != null)
                        {
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < errornode.Attributes.Count; i++)
                            {
                                sb.AppendFormat("{0} {1}", errornode.Attributes[i].Name, errornode.Attributes[i].Value);
                                sb.Append(", ");
                            }
                            _error = sb.ToString();
                        }

                        doc = null;
                    }
                    catch
                    { //swallow any error as this is not THAT important
                    }
                    _dataErrorChecked = true;
                }
            }
        }
    }
}
