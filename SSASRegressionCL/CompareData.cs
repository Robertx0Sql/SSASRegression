using System;
using System.Collections.Generic;
using System.Linq;

namespace SSASRegressionCL
{
    public class CompareData
    {
        public CompareData(string ID, string Description, TestResult result1, TestResult result2)
        {
            //setup(ID, Description, result1, result2);
            this.ID = ID;
            this.Description = Description;
            this.resultLeft = result1;
            this.resultRight = result2;
        }

        public CompareData(string ID, string Description, TestResult result1, TestResult result2, CompareResultType compareType, List<ComparisonDifference> compareErrors)
        {
            //  setup(ID, Description, result1, result2);
            this.ID = ID;
            this.Description = Description;
            this.resultLeft = result1;
            this.resultRight = result2;
            this.CompareResult = compareType;
            this.CompareErrors = compareErrors;
        }
        private void setup(string ID, string Description, TestResult result1, TestResult result2)
        {
            this.ID = ID;
            this.Description = Description;
            this.resultLeft = result1;
            this.resultRight = result2;
        }

        public override string ToString()
        {
            return String.Format("{0} [{1}]", Description, ID);
        }


        public CompareResultType CompareResult;
        public List<ComparisonDifference> CompareErrors;
        public string ID;
        public string Description;
        public TestResult resultLeft;
        public TestResult resultRight;
    }
}
