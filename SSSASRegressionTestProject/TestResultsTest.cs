using SSASRegressionCL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SSSASRegressionTestProject
{
    
    
    /// <summary>
    ///This is a test class for TestResultsTest and is intended
    ///to contain all TestResultsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TestResultsTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for TestResults Constructor
        ///</summary>
        [TestMethod()]
        public void TestResults_SavePassedResults()
        {
            string Server = "Server"; // TODO: Initialize to an appropriate value
            string CatalogName = "CatalogName"; // TODO: Initialize to an appropriate value
            int maxThreads = 0; // TODO: Initialize to an appropriate value
            TestResults target = new TestResults(Server, CatalogName, maxThreads);

            List<TestResult> results = new List<TestResult> ();
            Test t = new Test("1","failing","select {}" );
        //    results.Add(new TestResult(t, DateTime.UtcNow,"error description"));
            results.Add(new TestResult(t, "",DateTime.UtcNow,DateTime.UtcNow));
            target.listOfResults = results;
            target.SaveFile(@"c:\temp\testresult.xml");
            //Assert.Inconclusive("TODO: Implement code to verify target");
        }


        [TestMethod()]
        public void TestResults_SaveFailedResults()
        {
            string Server = "Server"; // TODO: Initialize to an appropriate value
            string CatalogName = "CatalogName"; // TODO: Initialize to an appropriate value
            int maxThreads = 0; // TODO: Initialize to an appropriate value
            TestResults target = new TestResults(Server, CatalogName, maxThreads);

            List<TestResult> results = new List<TestResult>();
            Test t = new Test("1", "failing", "select {}");
            results.Add(new TestResult(t, DateTime.UtcNow, DateTime.UtcNow, "error description"));
            //results.Add(new TestResult(t, "", DateTime.UtcNow, DateTime.UtcNow));
            target.listOfResults = results; 
            target.SaveFile(@"c:\temp\testresult_fail.xml");
            //Assert.Inconclusive("TODO: Implement code to verify target");
        }
        /// <summary>
        ///A test for TestResults Constructor
        ///</summary>
        [TestMethod()]
        public void TestResultsConstructorTest1()
        {
            string Server = string.Empty; // TODO: Initialize to an appropriate value
            string CatalogName = string.Empty; // TODO: Initialize to an appropriate value
            TestResults target = new TestResults(Server, CatalogName);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for TestResults Constructor
        ///</summary>
        [TestMethod()]
        public void TestResultsConstructorTest2()
        {
            TestResults target = new TestResults();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for LoadFile
        ///</summary>
        [TestMethod()]
        public void LoadFileTest()
        {
            TestResults target = new TestResults(); // TODO: Initialize to an appropriate value
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            List<TestResult> expected = null; // TODO: Initialize to an appropriate value
            List<TestResult> actual;
            bool targetLoadFile =  target.LoadFile(filePath);;
            actual = target.listOfResults;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ProcessSemaphoreItem
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void ProcessSemaphoreItemTest()
        {
            TestResults_Accessor target = new TestResults_Accessor(); // TODO: Initialize to an appropriate value
            object item = null; // TODO: Initialize to an appropriate value
            target.ProcessSemaphoreItem(item);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RaiseError
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void RaiseErrorTest()
        {
            TestResults_Accessor target = new TestResults_Accessor(); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            target.RaiseError(message, new Exception());
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RaiseEvent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void RaiseEventTest()
        {
            TestResults_Accessor target = new TestResults_Accessor(); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            ResultEventArgs.EventResult result = new ResultEventArgs.EventResult(); // TODO: Initialize to an appropriate value
            target.RaiseEvent(message, result);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RaiseMessage
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void RaiseMessageTest()
        {
            TestResults_Accessor target = new TestResults_Accessor(); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            target.RaiseMessage(message);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RunParallelTests
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void RunParallelTestsTest()
        {
            TestResults_Accessor target = new TestResults_Accessor(); // TODO: Initialize to an appropriate value
            List<Test> tests = null; // TODO: Initialize to an appropriate value
            target.RunParallelTests(tests);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RunTest
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void RunTestTest()
        {
            TestResults_Accessor target = new TestResults_Accessor(); // TODO: Initialize to an appropriate value
            Test test = null; // TODO: Initialize to an appropriate value
            MdxQuery query = null; // TODO: Initialize to an appropriate value
            TestResult expected = null; // TODO: Initialize to an appropriate value
            TestResult actual;
            actual = target.RunTest(test, query);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RunTests
        ///</summary>
        [TestMethod()]
        public void RunTestsTest()
        {
            TestResults target = new TestResults(); // TODO: Initialize to an appropriate value
            string testFile = string.Empty; // TODO: Initialize to an appropriate value
            List<TestResult> expected = null; // TODO: Initialize to an appropriate value
            List<TestResult> actual;
            target.RunTests(testFile);
            actual = target.listOfResults;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RunTests
        ///</summary>
        [TestMethod()]
        public void RunTestsTest1()
        {
            TestResults target = new TestResults(); // TODO: Initialize to an appropriate value
            List<Test> tests = null; // TODO: Initialize to an appropriate value
            List<TestResult> expected = null; // TODO: Initialize to an appropriate value
            List<TestResult> actual;
            target.RunTests(tests);
            actual = target.listOfResults;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RunTests
        ///</summary>
        [TestMethod()]
        public void RunTestsTest2()
        {
            TestResults target = new TestResults(); // TODO: Initialize to an appropriate value
            string testFile = string.Empty; // TODO: Initialize to an appropriate value
            string server = string.Empty; // TODO: Initialize to an appropriate value
            string catalogName = string.Empty; // TODO: Initialize to an appropriate value
            List<TestResult> expected = null; // TODO: Initialize to an appropriate value
            List<TestResult> actual;
            target.RunTests(testFile, server, catalogName);
            actual = target.listOfResults;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RunTests
        ///</summary>
        [TestMethod()]
        public void RunTestsTest3()
        {
            TestResults target = new TestResults(); // TODO: Initialize to an appropriate value
            List<Test> tests = null; // TODO: Initialize to an appropriate value
            string server = string.Empty; // TODO: Initialize to an appropriate value
            string catalogName = string.Empty; // TODO: Initialize to an appropriate value
            List<TestResult> expected = null; // TODO: Initialize to an appropriate value
            List<TestResult> actual;
            target.RunTests(tests, server, catalogName);
            actual = target.listOfResults;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RunTeststoFile
        ///</summary>
        [TestMethod()]
        public void RunTeststoFileTest()
        {
            TestResults target = new TestResults(); // TODO: Initialize to an appropriate value
            string testFile = string.Empty; // TODO: Initialize to an appropriate value
            string resultsFile = string.Empty; // TODO: Initialize to an appropriate value
            string Server = string.Empty; // TODO: Initialize to an appropriate value
            string CatalogName = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.RunTeststoFile(testFile, resultsFile, Server, CatalogName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RunTeststoFile
        ///</summary>
        [TestMethod()]
        public void RunTeststoFileTest1()
        {
            TestResults target = new TestResults(); // TODO: Initialize to an appropriate value
            string testFile = string.Empty; // TODO: Initialize to an appropriate value
            string resultsFile = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.RunTeststoFile(testFile, resultsFile);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SaveFile
        ///</summary>
        [TestMethod()]
        public void SaveFileTest()
        {
            TestResults target = new TestResults(); // TODO: Initialize to an appropriate value
            List<TestResult> results = null; // TODO: Initialize to an appropriate value
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.SaveFile(filePath);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CatalogName
        ///</summary>
        [TestMethod()]
        public void CatalogNameTest()
        {
            TestResults target = new TestResults(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.CatalogName = expected;
            actual = target.CatalogName;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EndDate
        ///</summary>
        [TestMethod()]
        public void EndDateTest()
        {
            TestResults target = new TestResults(); // TODO: Initialize to an appropriate value
            DateTime actual;
            actual = target.EndDate;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for MaxThreads
        ///</summary>
        [TestMethod()]
        public void MaxThreadsTest()
        {
            TestResults target = new TestResults(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.MaxThreads = expected;
            actual = target.MaxThreads;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Server
        ///</summary>
        [TestMethod()]
        public void ServerTest()
        {
            TestResults target = new TestResults(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Server = expected;
            actual = target.Server;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for StartDate
        ///</summary>
        [TestMethod()]
        public void StartDateTest()
        {
            TestResults target = new TestResults(); // TODO: Initialize to an appropriate value
            DateTime actual;
            actual = target.StartDate;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SaveFile
        ///</summary>
        [TestMethod()]
        public void SaveFileTest1()
        {
            TestResults target = new TestResults(); // TODO: Initialize to an appropriate value
            List<TestResult> results = null; // TODO: Initialize to an appropriate value
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.SaveFile( filePath);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
