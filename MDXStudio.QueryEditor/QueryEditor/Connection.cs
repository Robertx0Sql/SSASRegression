using Microsoft.AnalysisServices;
using Microsoft.AnalysisServices.AdomdClient;
using System;

namespace MDXStudio
{
    internal class Connection
    {
        internal string ServerName;

        internal AdomdConnection QueryConnection;

        internal AdomdConnection MetadataConnection;

        internal Microsoft.AnalysisServices.Server AMOConnection;

        internal Microsoft.AnalysisServices.Server TraceConnection;

        internal string CurrentCatalog;

        internal string CurrentCube;

        public Connection()
        {
        }
    }
}