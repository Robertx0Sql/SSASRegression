using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.AdomdClient;

namespace SSASRegressionCL
{
    public interface ITestResult
    {
        string ToString();
        Microsoft.AnalysisServices.AdomdClient.CellSet cellSet { get; }
        TimeSpan QueryTimeSpan { get; }
        string QueryTimeFormated { get; }
        Result result { get; set; }
        DateTime EndDate { get; set; }
        DateTime StartDate { get; set; }
        string Data { get; set; }
        string Error { get; set; }
        bool IsError { get; }
        bool isResultExecuted { get; }
    }
}
