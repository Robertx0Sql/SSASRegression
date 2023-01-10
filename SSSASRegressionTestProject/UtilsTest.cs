using SSASRegressionCL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.AnalysisServices.AdomdClient;
using System.Xml.Linq;

namespace SSSASRegressionTestProject
{
    
    
    /// <summary>
    ///This is a test class for UtilsTest and is intended
    ///to contain all UtilsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UtilsTest
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
        ///A test for CellSetColumns
        ///</summary>
        [TestMethod()]
        public void CellSetColumnsTest()
        {
            CellSet cst = null; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = Utils.CellSetColumns(cst);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CellSetRows
        ///</summary>
        [TestMethod()]
        public void CellSetRowsTest()
        {
            CellSet cst = null; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = Utils.CellSetRows(cst);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAttributeDate
        ///</summary>
        [TestMethod()]
        public void GetAttributeDateTest()
        {
            XElement xElement = null; // TODO: Initialize to an appropriate value
            string attributeName = string.Empty; // TODO: Initialize to an appropriate value
            DateTime expected = new DateTime(); // TODO: Initialize to an appropriate value
            DateTime actual;
            actual = Utils.GetAttributeDate(xElement, attributeName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAttributeValue
        ///</summary>
        [TestMethod()]
        public void GetAttributeValueTest()
        {
            XElement xElement = null; // TODO: Initialize to an appropriate value
            string attributeName = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = Utils.GetAttributeValue(xElement, attributeName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetCellSetCounts
        ///</summary>
        [TestMethod()]
        public void GetCellSetCountsTest()
        {
            CellSet cst = null; // TODO: Initialize to an appropriate value
            int rows = 0; // TODO: Initialize to an appropriate value
            int rowsExpected = 0; // TODO: Initialize to an appropriate value
            int columns = 0; // TODO: Initialize to an appropriate value
            int columnsExpected = 0; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Utils.GetCellSetCounts(cst, out rows, out columns);
            Assert.AreEqual(rowsExpected, rows);
            Assert.AreEqual(columnsExpected, columns);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetCellSetfromXML
        ///</summary>
        [TestMethod()]
        public void GetCellSetfromXMLTest()
        {
            string xml = string.Empty; // TODO: Initialize to an appropriate value
            CellSet expected = null; // TODO: Initialize to an appropriate value
            CellSet actual;
            actual = Utils.GetCellSetfromXML(xml);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetElementValue
        ///</summary>
        [TestMethod()]
        public void GetElementValueTest()
        {
            XElement xElement = null; // TODO: Initialize to an appropriate value
            string elementName = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = Utils.GetElementValue(xElement, elementName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
