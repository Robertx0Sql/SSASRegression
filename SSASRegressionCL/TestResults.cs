using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Threading;
using System.Collections;
using System.Data;
using System.Xml.Serialization;
using System.Xml;

namespace SSASRegressionCL
{
    [XmlRoot("Tests")]
    public class TestResults : ITestResults
    {

        public event GetStatusHandler StatusEvent;

        private string _server;
        private string _catalogName;
        private DateTime _endDate;
        private DateTime _startDate;
        private int _maxThreads=1;
        private string _effectiveUserName;
        private string _ConnectionString;

        private static Semaphore semaphore;

        private List<TestResult> _listOfResults;

        private List<string> _errorList = new List<string>();

        public TestResults()
        { }

        public TestResults(string Server, string CatalogName)
        {
            _server = Server;
            _catalogName = CatalogName;
        }

        public TestResults(string connectionString, int maxThreads)
        {
            this._ConnectionString = connectionString;
            MaxThreads = maxThreads;
            SetServerCatalogfromConnectionString(connectionString);

            _endDate = DateTime.UtcNow;
            _startDate = DateTime.UtcNow;
        }

      
        public TestResults(string Server, string CatalogName, int maxThreads)
        {
            this.Server = Server;
            this.CatalogName = CatalogName;
            MaxThreads = maxThreads;

            _endDate = DateTime.UtcNow;
            _startDate = DateTime.UtcNow;
        }

        public TestResults(string Server, string CatalogName, int maxThreads, string EffectiveUserName)
        {
            this.Server = Server;
            this.CatalogName = CatalogName;
            MaxThreads = maxThreads;

            _endDate = DateTime.UtcNow;
            _startDate = DateTime.UtcNow;
            if (!EffectiveUserName.IsNullOrEmptyOrWhitespace())
            _effectiveUserName = EffectiveUserName;
        }
        private void SetServerCatalogfromConnectionString(string connectionString)
        {
            _catalogName = Utils.ConnectionStringCatalog(connectionString);
            _server = Utils.ConnectionStringServer(connectionString);
        }

        #region Fields
        [XmlElement("test")]
        public List<TestResult> listOfResults
        {
            get
            {
                if (_listOfResults == null)
                    _listOfResults = new List<TestResult>();
                return _listOfResults;
            }
            set
            {
                _listOfResults = value;
            }
        }
        [XmlAttribute(AttributeName = "StartDate")]
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        [XmlAttribute(AttributeName = "EndDate")]
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        [XmlAttribute(AttributeName = "Server")]
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }
        [XmlAttribute(AttributeName = "Database")]
        public string CatalogName
        {
            get { return _catalogName; }
            set { _catalogName = value; }
        }

        [XmlAttribute(AttributeName = "EffectiveUserName")]
        public string EffectiveUserName
        {
            get { return _effectiveUserName; }
            set { _effectiveUserName = value; }
        }



        [XmlIgnore]
        public bool ValidResultFile
        {
            get;
            private set;
        }

        [XmlIgnore]
        public int MaxThreads
        {
            get { return _maxThreads; }
            set
            {
                value = (value < 0) ? 0 : value;
                value = (value > 10) ? 10 : value;
                _maxThreads = value;
            }
        }
        [XmlIgnore]
        public TimeSpan ResultTimeSpan
        {
            get
            {
                TimeSpan ts = EndDate - StartDate;
                return ts;
            }

        }
        [XmlIgnore]
        public Dictionary<string, TestResult> ResultDictionary
        {
            get
            {

                Dictionary<string, TestResult> dicResult = listOfResults.ToDictionary(x => x.ID, x => x);
                return dicResult;
            }
        }
        #endregion




        #region Run Tests

        public bool RunTests(string testFile, string server, string catalogName)
        {
            _server = server;
            _catalogName = catalogName;

            return RunTests(testFile);
        }

        public bool RunTests(string testFile)
        {
            //Load xml
            Utils.ValidateTestFilePath(testFile);

            Tests t = new Tests();
            List<Test> tests = t.LoadFile(testFile);

            return RunTests(tests);
        }


        public bool RunTests(List<Test> tests, string server, string catalogName)
        {
            _server = server;
            _catalogName = catalogName;
            return RunTests(tests);
        }

        public bool RunTests(List<Test> tests)
        {
            listOfResults = new List<TestResult>();
            string currentTestDescription = "<Connect to Server/Catalog>";

            try
            {
                _errorList.Clear();
                RaiseMessage(string.Format("Connect to Server '{0}'; CatalogName '{1}'", Server, CatalogName));
                MdxQuery query = NewMdxQuery();

                _startDate = DateTime.UtcNow;
                if (MaxThreads <= 1)
                {
                    RaiseMessage(string.Format("Running {0} Tests Serially", tests.Count));

                    foreach (Test test in tests)
                    {
                        currentTestDescription = test.Description;
                        listOfResults.Add(RunTest(test, query));
                    }
                    RaiseMessage(string.Format("Completed {0} Tests Serially", tests.Count));
                }
                else
                {
                    RunParallelTests(tests);
                }
                _endDate = DateTime.UtcNow;
            }
            catch (ConnectionException ex)
            {
                string error = string.Format("Error connecting to Server/Catalog: {0}", ex.Message);
                RaiseError(error, ex);
            }
            catch (QueryException ex)
            {
                string error = string.Format("Error Running Test '{0}': {1}", currentTestDescription, ex.Message);
                RaiseError(error, ex);
            }
            catch (Exception ex)
            {
                string error = string.Format("Error Running Test '{0}': {1}", currentTestDescription, ex.Message);
                RaiseError(error, ex);

            }
            if (_errorList.Count > 0)
                throw new Exception("RunTests: \r\n" + string.Join(";\r\n", _errorList.ToArray()));

            return true;
        }

        private MdxQuery NewMdxQuery()
        {
            MdxQuery query;
            if(!_ConnectionString.IsNullOrEmptyOrWhitespace())
                query = new MdxQuery(_ConnectionString);
            else
                query=new MdxQuery(Server, CatalogName, EffectiveUserName);

            return query;
        }


        private TestResult RunTest(Test test, MdxQuery query)
        {
            // string mdx = test.MDX;
            //string id = test.ID;
            //string description = test.Description;
            RaiseMessage(string.Format("Start Test : {0}", test.Description));
            DateTime TestStartDate = DateTime.UtcNow;
            TestResult result;
            try
            {
             
                string dataresult = query.ExecuteXmlReader(test.MDX);
                DateTime TestEndDate = DateTime.UtcNow;

                RaiseMessage(string.Format("Completed Test : {0}", test.Description));
                result = new TestResult(test, dataresult, TestStartDate, TestEndDate);
            }
            catch (Exception ex)
            {
                DateTime TestEndDate = DateTime.UtcNow;
                RaiseMessage(string.Format("Failed to Run Test : {0}", test.Description));
                result = new TestResult(test, TestStartDate, TestEndDate, ex.Message);
            }
            return result;
        }

        #region Run Tests in Parallel
        private void RunParallelTests(List<Test> tests)
        {
            RaiseMessage(string.Format("Running {0} Tests in Parallel using {1} Threads", tests.Count, MaxThreads));
            semaphore = new Semaphore(MaxThreads, MaxThreads);

            Queue myQ = new Queue();

            foreach (Test test in tests)
            {
                myQ.Enqueue(test);
            }

            while (myQ.Count != 0)
            {
                semaphore.WaitOne();
                var item = myQ.Dequeue();
                ThreadPool.QueueUserWorkItem(ProcessSemaphoreItem, item); // see below
            }

            //// When the queue is empty, you have to wait for all processing
            //// threads to complete.
            //// If you can acquire the semaphore MaxThreads times, all workers are done
            int count = 0;
            while (count < MaxThreads)
            {
                semaphore.WaitOne();
                ++count;
            }

            if (_errorList.Count > 0)
                throw new Exception("RunTests: \r\n" + string.Join(";\r\n", _errorList.ToArray()));

            RaiseMessage(string.Format("Completed {0} Tests in Parallel using {1} Threads", tests.Count, MaxThreads));
        }


        // the code to process an item
        void ProcessSemaphoreItem(object item)
        {
            // cast the item to type 
            Test test = item as Test;
            
            try
            {
                if (test != null)
                {
                    MdxQuery query = NewMdxQuery(); 

                    listOfResults.Add(RunTest(test, query));
                    query = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                RaiseError(string.Format("Error Running Test '{0}' : {1}", test.Description, ex.Message),ex);
            }
            finally
            {
                // when done processing, release the semaphore
                try
                {
                    semaphore.Release();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    RaiseError(string.Format("Error in Parallel Testing : {0}", ex.Message), ex);
                }
            }
        }
        #endregion

        #endregion

        #region Run Tests to File
        public bool RunTeststoFile(string testFile, string resultsFile, string connectionString)
        {
            _ConnectionString = connectionString;
            SetServerCatalogfromConnectionString(connectionString);
            return RunTeststoFile(testFile, resultsFile);
        }
        public bool RunTeststoFile(string testFile, string resultsFile, string Server, string CatalogName)
        {
            _server = Server;
            _catalogName = CatalogName;

            return RunTeststoFile(testFile, resultsFile);
        }

        public bool RunTeststoFile(string testFile, string resultsFile)
        {

            Utils.ValidateNewResultFilePath(resultsFile);

            if (testFile == resultsFile)
                throw new Exception(string.Format("The Results file cannot be the same as the Input file", testFile));


            if (RunTests(testFile))
                return SaveFile(resultsFile);
            else
                return false;
        }


        #endregion


        #region File IO

      
        public bool SaveFile(string filename)
        {
            bool result = false;
            Utils.ValidateNewResultFilePath(filename);

            try
            {
                //preserve line endings(\r\n) and tabs(\t) https://msdn.microsoft.com/en-us/library/system.xml.xmlwritersettings.newlinehandling%28v=vs.110%29.aspx
                XmlWriterSettings ws = new XmlWriterSettings();
                ws.NewLineHandling = NewLineHandling.Replace;
                ws.Indent = true;

                using (XmlWriter writer = XmlWriter.Create(filename, ws))
                {
                    //TextWriter writer = new StreamWriter(filename);
                    XmlSerializer serializer = new XmlSerializer(typeof(TestResults));

                    serializer.Serialize(writer, this);
                    //   writer.Close();
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Saving Result File " + filename, ex);
            }
            return result;
        }


        public bool LoadFile(string filename)
        {
            string STR_ErrorLoadingResultFile = "Error Loading Result File '{0}'; {1}";

            try
            {
                 

                // Create an instance of the XmlSerializer specifying type and namespace.
                XmlSerializer serializer = new XmlSerializer(typeof(TestResults));
               
                TestResults tr;
                // A FileStream is needed to read the XML document.
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    XmlReader reader = XmlReader.Create(fs);

                    tr = (TestResults)serializer.Deserialize(reader);
                }
                if (tr != null)
                {
                    this.listOfResults = tr.listOfResults;
                    this.StartDate = tr.StartDate;
                    this.EndDate = tr.EndDate;
                    this.Server = tr.Server;
                    this.CatalogName = tr.CatalogName;

                    int validResults = 0;
                    if (listOfResults != null)
                    {//copy the server info down to the result level
                        foreach (TestResult r in listOfResults)
                        {
                            if (r.isResultExecuted)
                            {
                                validResults++;
                                r.CatalogName = CatalogName;
                                r.ServerName = Server;
                            }
                        }
                    }
                    ValidResultFile = (validResults != 0);
                    
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new Exception(String.Format(STR_ErrorLoadingResultFile, filename, ex.Message), ex);
            }
            catch (FileLoadException ex)
            {
                throw new Exception(String.Format(STR_ErrorLoadingResultFile, filename, ex.Message), ex);

            }

            catch (FieldAccessException ex)
            {
                throw new Exception(String.Format(STR_ErrorLoadingResultFile, filename, ex.Message), ex);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format(STR_ErrorLoadingResultFile, filename, ex.Message), ex);
            }
            return true;
        }
        #endregion
        public DataTable ResultStatistics()
        {


            DataTable table = new DataTable();
            table.Columns.Add("Test ID", typeof(string));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("Start Date Time", typeof(DateTime));
            table.Columns.Add("End Date Time", typeof(DateTime));
            table.Columns.Add("Test Time (hh:mm:ss.ms)", typeof(string));
            // table.Columns.Add("Test Time (String)", typeof(string));
            table.Columns.Add("Error", typeof(string));

            //
            // Here we add five DataRows.
            //
            if (listOfResults.Count > 0)
            {
                TimeSpan totaltime = TimeSpan.Zero;


                foreach (TestResult result in listOfResults)
                {
                    table.Rows.Add(result.ID, result.Description, result.StartDate, result.EndDate, string.Format("{0:c}", result.QueryTimeSpan), result.Error);

                    totaltime += result.QueryTimeSpan;
                }

                table.Rows.Add("TOTALS::", "", StartDate, EndDate, string.Format("{0:c}", totaltime), "");
            }
            return table;
        }


        #region Raise Events
        private void RaiseMessage(string message)
        {
            RaiseEvent(message, ResultEventArgs.EventResult.Information);
        }

        private void RaiseError(string message, Exception ex)
        {
            _errorList.Add(message);
            RaiseEvent(message, ResultEventArgs.EventResult.Error);
            throw ex; //new Exception(message, ex);

        }

        private void RaiseEvent(string message, ResultEventArgs.EventResult result)
        {
            if (StatusEvent != null)
            {
                ResultEventArgs e = new ResultEventArgs() { message = message, result = result };

                StatusEvent(this, e);
            }
        }
        #endregion




    }


}
