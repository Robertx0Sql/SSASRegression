using SSASRegressionCL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SSSASRegressionTestProject
{
    
    
    /// <summary>
    ///This is a test class for TestsTest and is intended
    ///to contain all TestsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TestsTest
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
        ///A test for Tests Constructor
        ///</summary>
        [TestMethod()]
        public void TestsConstructorTest()
        {
            Tests target = new Tests();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for LoadFile
        ///</summary>
        [TestMethod()]
        public void LoadFileTest()
        {
            Tests target = new Tests(); // TODO: Initialize to an appropriate value
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            List<Test> expected = null; // TODO: Initialize to an appropriate value
            List<Test> actual;
            actual = target.LoadFile(filePath);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RaiseEvent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SSASRegressionCL.dll")]
        public void RaiseEventTest()
        {
            Tests_Accessor target = new Tests_Accessor(); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            target.RaiseEvent(message);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SaveFile
        ///</summary>
        [TestMethod()]
        public void SaveFileTest()
        {
             // TODO: Initialize to an appropriate value
            List<Test> tests = null; // TODO: Initialize to an appropriate value
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Tests.SaveFile(tests, filePath);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
