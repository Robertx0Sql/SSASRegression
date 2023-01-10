using SSASRegressionCL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.AnalysisServices.AdomdClient;
using System.Collections.Generic;

namespace SSSASRegressionTestProject
{
    
    
    /// <summary>
    ///This is a test class for ResultsComparatorTest and is intended
    ///to contain all ResultsComparatorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ResultsComparatorTest
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
        ///A test for ResultsComparator Constructor
        ///</summary>
        [TestMethod()]
        public void ResultsComparatorConstructorTest()
        {
            ResultsComparator target = new ResultsComparator();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CompareCellSets
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void CompareCellSetsTest()
        {
            ValueComparitor comparitor = ValueComparitor.FormattedValue;
            ResultsComparator_Accessor target = new ResultsComparator_Accessor(); // TODO: Initialize to an appropriate value
            string ID = string.Empty; // TODO: Initialize to an appropriate value
            string Description = string.Empty; // TODO: Initialize to an appropriate value
            CellSet c1 = null; // TODO: Initialize to an appropriate value
            CellSet c2 = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CompareCellSets(ID, Description, c1, c2, comparitor);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CompareCellSets
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void CompareCellSetsTest1()
        {
            ResultsComparator_Accessor target = new ResultsComparator_Accessor(); // TODO: Initialize to an appropriate value
            TestResult r1 = null; // TODO: Initialize to an appropriate value
            TestResult r2 = null; // TODO: Initialize to an appropriate value
            ValueComparitor comparitor = ValueComparitor.FormattedValue;
            List<ComparisonDifference> expected = null; // TODO: Initialize to an appropriate value
            List<ComparisonDifference> actual;
            actual = target.CompareCellSets(r1, r2, comparitor);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CompareFiles
        ///</summary>
        [TestMethod()]
        public void CompareFilesTest()
        {
            ResultsComparator target = new ResultsComparator(); // TODO: Initialize to an appropriate value
            string filePath1 = string.Empty; // TODO: Initialize to an appropriate value
            string filePath2 = string.Empty; // TODO: Initialize to an appropriate value
            Dictionary<string, CompareData> expected = null; // TODO: Initialize to an appropriate value
            Dictionary<string, CompareData> actual;
            ValueComparitor comparitor = ValueComparitor.FormattedValue;
            target.Compare(filePath1, filePath2, comparitor);
            actual = target.ComparedResults;

            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

       

       

        /// <summary>
        ///A test for GetCellSetDictionary
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void GetCellSetDictionaryTest()
        {
            ResultsComparator_Accessor target = new ResultsComparator_Accessor(); // TODO: Initialize to an appropriate value
            CellSet cst = null; // TODO: Initialize to an appropriate value
            Dictionary<string, CellData_Accessor> expected = null; // TODO: Initialize to an appropriate value
            Dictionary<string, CellData_Accessor> actual;
            ValueComparitor comparitor = ValueComparitor.FormattedValue;
            actual = target.GetCellSetDictionary(cst, comparitor);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetKey
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void GetKeyTest()
        {
            string fieldtype = string.Empty; // TODO: Initialize to an appropriate value
            int col_index = 0; // TODO: Initialize to an appropriate value
            int row_index = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = ResultsComparator_Accessor.CreateComparitorDictionaryKey(fieldtype, col_index, row_index);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OnResultProcess
        ///</summary>
        [TestMethod()]
        public void OnResultProcessTest()
        {
            ResultsComparator target = new ResultsComparator(); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            ResultEventArgs ee = null; // TODO: Initialize to an appropriate value
            target.OnResultProcess(sender, ee);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RaiseComparatorResultsEvent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void RaiseComparatorResultsEventTest()
        {
            ResultsComparator_Accessor target = new ResultsComparator_Accessor(); // TODO: Initialize to an appropriate value
            string id = string.Empty; // TODO: Initialize to an appropriate value
            string description = string.Empty; // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            string file = string.Empty; // TODO: Initialize to an appropriate value
            bool CompareResult = false; // TODO: Initialize to an appropriate value
            target.RaiseComparatorResultsEvent(id, description, message, file, CompareResult);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RaiseStatusError
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void RaiseStatusErrorTest()
        {
            ResultsComparator_Accessor target = new ResultsComparator_Accessor(); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            target.RaiseStatusError(message);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RaiseStatusMessage
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void RaiseStatusMessageTest()
        {
            ResultsComparator_Accessor target = new ResultsComparator_Accessor(); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            target.RaiseStatusMessage(message);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RaiseStatusMessage
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void RaiseStatusMessageTest1()
        {
            ResultsComparator_Accessor target = new ResultsComparator_Accessor(); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            ResultEventArgs.EventResult result = new ResultEventArgs.EventResult(); // TODO: Initialize to an appropriate value
            target.RaiseStatusMessage(message, result);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
