using System;
namespace SSASRegressionCL
{
    interface IResult
    {
        string Data { get; set; }
        string Error { get; set; }
        bool IsError { get; }
        DateTime EndDate { get; set; }
        DateTime StartDate { get; set; }
    }
}
