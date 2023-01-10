using SSASRegressionCL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SSSASRegressionTestProject
{
    
    
    /// <summary>
    ///This is a test class for TestTest and is intended
    ///to contain all TestTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TestTest
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
        ///A test for Test Constructor
        ///</summary>
        [TestMethod()]
        public void TestConstructorTest()
        {
            string id = string.Empty; // TODO: Initialize to an appropriate value
            string description = string.Empty; // TODO: Initialize to an appropriate value
            string mdx = string.Empty; // TODO: Initialize to an appropriate value
            Test target = new Test(id, description, mdx);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Test Constructor
        ///</summary>
        [TestMethod()]
        public void TestConstructorTest1()
        {
            Test target = new Test();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
