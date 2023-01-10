using System;
using System.Collections.Generic;
using System.Linq;

namespace SSASRegressionCL
{
    public interface IResultsComparator
    {
        event ComparatorHandler ComparatorResultsEvent;
        event GetStatusHandler StatusEvent;
        Dictionary<string, CompareData> ComparedResults { get; }
        bool Compare(string filePath1, string filePath2, ValueComparitor omparitor);
        bool Compare(Dictionary<string, TestResult> dresults1, Dictionary<string, TestResult> dresults2, ValueComparitor comparitor);
        void OnResultProcess(object sender, ResultEventArgs ee);
        


    }
}
