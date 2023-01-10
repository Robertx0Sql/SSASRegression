using System;
using System.Collections.Generic;
using System.Linq;

namespace SSASRegressionCL
{

    public delegate void ComparatorHandler(object sender, CompareEventArgs e);
    public delegate void GetStatusHandler(object sender, ResultEventArgs e);

    public class ResultEventArgs : EventArgs
    {
    public enum EventResult {Information=0, Error=1}
        
        public string message;
        public EventResult result;
    }
    public class CompareEventArgs : ResultEventArgs 
    {

        public string description;
        public string id;
        public string file;
        public bool CompareResult;
    }
    public enum CompareResultType
    {
        Fail = 2,
        Pass = 1,
        Untested = 0
    };
}
