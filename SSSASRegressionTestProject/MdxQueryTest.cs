using SSASRegressionCL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.AnalysisServices.AdomdClient;
using System.Data;

namespace SSSASRegressionTestProject
{
    
    
    /// <summary>
    ///This is a test class for MdxQueryTest and is intended
    ///to contain all MdxQueryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MdxQueryTest
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
        ///A test for MdxQuery Constructor
        ///</summary>
        [TestMethod()]
        public void MdxQueryConstructorTest()
        {
            string server = string.Empty; // TODO: Initialize to an appropriate value
            string catalogName = string.Empty; // TODO: Initialize to an appropriate value
            MdxQuery target = new MdxQuery(server, catalogName);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for ConnectToDatabase
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void ConnectToDatabaseTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            MdxQuery_Accessor target = new MdxQuery_Accessor(param0); // TODO: Initialize to an appropriate value
            target.ConnectToDatabase();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        public void DisposeTest()
        {
            string server = string.Empty; // TODO: Initialize to an appropriate value
            string catalogName = string.Empty; // TODO: Initialize to an appropriate value
            MdxQuery target = new MdxQuery(server, catalogName); // TODO: Initialize to an appropriate value
            target.Dispose();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void DisposeTest1()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            MdxQuery_Accessor target = new MdxQuery_Accessor(param0); // TODO: Initialize to an appropriate value
            bool disposing = false; // TODO: Initialize to an appropriate value
            target.Dispose(disposing);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ExecuteCommandCellSet
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void ExecuteCommandCellSetTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            MdxQuery_Accessor target = new MdxQuery_Accessor(param0); // TODO: Initialize to an appropriate value
            CellSet expected = null; // TODO: Initialize to an appropriate value
            CellSet actual;
            actual = target.ExecuteCommandCellSet();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ExecuteCommandCellSet
        ///</summary>
        [TestMethod()]
        public void ExecuteCommandCellSetTest1()
        {
            string server = string.Empty; // TODO: Initialize to an appropriate value
            string catalogName = string.Empty; // TODO: Initialize to an appropriate value
            MdxQuery target = new MdxQuery(server, catalogName); // TODO: Initialize to an appropriate value
            string Mdx = string.Empty; // TODO: Initialize to an appropriate value
            CellSet expected = null; // TODO: Initialize to an appropriate value
            CellSet actual;
            actual = target.ExecuteCommandCellSet(Mdx);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ExecuteCommandDataSet
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void ExecuteCommandDataSetTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            MdxQuery_Accessor target = new MdxQuery_Accessor(param0); // TODO: Initialize to an appropriate value
            DataTable expected = null; // TODO: Initialize to an appropriate value
            DataTable actual;
            actual = target.ExecuteCommandDataSet();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ExecuteCommandDataSet
        ///</summary>
        [TestMethod()]
        public void ExecuteCommandDataSetTest1()
        {
            string server = string.Empty; // TODO: Initialize to an appropriate value
            string catalogName = string.Empty; // TODO: Initialize to an appropriate value
            MdxQuery target = new MdxQuery(server, catalogName); // TODO: Initialize to an appropriate value
            string Mdx = string.Empty; // TODO: Initialize to an appropriate value
            DataTable expected = null; // TODO: Initialize to an appropriate value
            DataTable actual;
            actual = target.ExecuteCommandDataSet(Mdx);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ExecuteCommandReader
        ///</summary>
        [TestMethod()]
        public void ExecuteCommandReaderTest()
        {
            string server = string.Empty; // TODO: Initialize to an appropriate value
            string catalogName = string.Empty; // TODO: Initialize to an appropriate value
            MdxQuery target = new MdxQuery(server, catalogName); // TODO: Initialize to an appropriate value
            string Mdx = string.Empty; // TODO: Initialize to an appropriate value
            AdomdDataReader expected = null; // TODO: Initialize to an appropriate value
            AdomdDataReader actual;
            actual = target.ExecuteCommandReader(Mdx);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ExecuteCommandReader
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void ExecuteCommandReaderTest1()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            MdxQuery_Accessor target = new MdxQuery_Accessor(param0); // TODO: Initialize to an appropriate value
            AdomdDataReader expected = null; // TODO: Initialize to an appropriate value
            AdomdDataReader actual;
            actual = target.ExecuteCommandReader();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ExecuteXmlReader
        ///</summary>
        [TestMethod()]
        public void ExecuteXmlReaderTest()
        {
            string server = string.Empty; // TODO: Initialize to an appropriate value
            string catalogName = string.Empty; // TODO: Initialize to an appropriate value
            MdxQuery target = new MdxQuery(server, catalogName); // TODO: Initialize to an appropriate value
            string Mdx = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ExecuteXmlReader(Mdx);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Finalize
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void FinalizeTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            MdxQuery_Accessor target = new MdxQuery_Accessor(param0); // TODO: Initialize to an appropriate value
         //   target.Finalize();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetAdomdCommand
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void GetAdomdCommandTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            MdxQuery_Accessor target = new MdxQuery_Accessor(param0); // TODO: Initialize to an appropriate value
            AdomdCommand expected = null; // TODO: Initialize to an appropriate value
            AdomdCommand actual;
            actual = target.GetAdomdCommand();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetCatalogs
        ///</summary>
        [TestMethod()]
        public void GetCatalogsTest()
        {
            string server = string.Empty; // TODO: Initialize to an appropriate value
            string catalogName = string.Empty; // TODO: Initialize to an appropriate value
            MdxQuery target = new MdxQuery(server, catalogName); // TODO: Initialize to an appropriate value
            DataTable expected = null; // TODO: Initialize to an appropriate value
            DataTable actual;
            actual = target.GetCatalogs();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CatalogName
        ///</summary>
        [TestMethod()]
        public void CatalogNameTest()
        {
            string server = string.Empty; // TODO: Initialize to an appropriate value
            string catalogName = string.Empty; // TODO: Initialize to an appropriate value
            MdxQuery target = new MdxQuery(server, catalogName); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.CatalogName = expected;
            actual = target.CatalogName;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for MDXCommand
        ///</summary>
        [TestMethod()]
        public void MDXCommandTest()
        {
            string server = string.Empty; // TODO: Initialize to an appropriate value
            string catalogName = string.Empty; // TODO: Initialize to an appropriate value
            MdxQuery target = new MdxQuery(server, catalogName); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.MDXCommand = expected;
            actual = target.MDXCommand;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Provider
        ///</summary>
        [TestMethod()]
        public void ProviderTest()
        {
            string server = string.Empty; // TODO: Initialize to an appropriate value
            string catalogName = string.Empty; // TODO: Initialize to an appropriate value
            MdxQuery target = new MdxQuery(server, catalogName); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Provider;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Server
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void ServerTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            MdxQuery_Accessor target = new MdxQuery_Accessor(param0); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Server;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
