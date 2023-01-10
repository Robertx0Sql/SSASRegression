using SSASRegressionCL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.AnalysisServices.AdomdClient;

namespace SSSASRegressionTestProject
{
    
    
    /// <summary>
    ///This is a test class for TestResultTest and is intended
    ///to contain all TestResultTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TestResultTest
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
        ///A test for TestResult Constructor
        ///</summary>
        [TestMethod()]
        public void TestResultConstructorTest()
        {
            string id = string.Empty; // TODO: Initialize to an appropriate value
            string description = string.Empty; // TODO: Initialize to an appropriate value
            string data = string.Empty; // TODO: Initialize to an appropriate value
            string mdx = string.Empty; // TODO: Initialize to an appropriate value
            DateTime startDate = new DateTime(); // TODO: Initialize to an appropriate value
            DateTime endDate = new DateTime(); // TODO: Initialize to an appropriate value
            TestResult target = new TestResult(id, description, data, mdx, startDate, endDate);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for TestResult Constructor
        ///</summary>
        [TestMethod()]
        public void TestResultConstructorTest1()
        {
            string id = string.Empty; // TODO: Initialize to an appropriate value
            string description = string.Empty; // TODO: Initialize to an appropriate value
            string mdx = string.Empty; // TODO: Initialize to an appropriate value
          //  TestResult target = new TestResult(id, description, mdx);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for TestResult Constructor
        ///</summary>
        [TestMethod()]
        public void TestResultConstructorTest2()
        {
            TestResult target = new TestResult();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for TestResult Constructor
        ///</summary>
        [TestMethod()]
        public void TestResultConstructorTest3()
        {
            Test test = null; // TODO: Initialize to an appropriate value
            string data = string.Empty; // TODO: Initialize to an appropriate value
            DateTime StartDate = new DateTime(); // TODO: Initialize to an appropriate value
            DateTime EndDate = new DateTime(); // TODO: Initialize to an appropriate value
            TestResult target = new TestResult(test, data, StartDate, EndDate);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            TestResult target = new TestResult(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for QueryTimeFormated
        ///</summary>
        [TestMethod()]
        public void QueryTimeFormatedTest()
        {
            TestResult target = new TestResult(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.QueryTimeFormated;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for QueryTimeSpan
        ///</summary>
        [TestMethod()]
        public void QueryTimeSpanTest()
        {
            TestResult target = new TestResult(); // TODO: Initialize to an appropriate value
            TimeSpan actual;
            actual = target.QueryTimeSpan;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for cellSet
        ///</summary>
        [TestMethod()]
        public void cellSetTest()
        {
            TestResult target = new TestResult(); // TODO: Initialize to an appropriate value
            CellSet actual;
            actual = target.cellSet;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
