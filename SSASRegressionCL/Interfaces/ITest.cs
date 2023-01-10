using System;
using System.Collections.Generic;
using System.Linq;

namespace SSASRegressionCL
{
    public interface ITest
    {
        string ToString();
        string ID { get; set; }
        string Description { get; set; }
        string MDX { get; set; }
    }
}
