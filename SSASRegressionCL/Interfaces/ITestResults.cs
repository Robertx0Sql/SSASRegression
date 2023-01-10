using System;
using System.Collections.Generic;
using System.Linq;

namespace SSASRegressionCL
{
    public interface ITestResults
    {
        event GetStatusHandler StatusEvent;
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        string Server { get; set; }
        string CatalogName { get; set; }
        int MaxThreads { get; set; }

        List<TestResult> listOfResults { get; set; }
        Dictionary<string, TestResult> ResultDictionary { get; }
        bool RunTests(string testFile, string server, string catalogName);
        bool RunTests(string testFile);
        bool RunTests(List<Test> tests, string Server, string catalogName);
        bool RunTests(List<Test> tests);
        bool RunTeststoFile(string testFile, string resultsFile, string server, string catalogName);
        bool RunTeststoFile(string testFile, string resultsFile);
        bool LoadFile(string filePath);
        bool SaveFile(string filePath);
    }
}
