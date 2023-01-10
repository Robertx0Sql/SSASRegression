using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.AdomdClient;
using System.Data;

namespace SSASRegressionCL
{
    public interface IMdxQuery
    {
        string CatalogName { get; set; }
        DataTable GetCatalogs();
        string MDXCommand { get; set; }
        string Provider { get; }
        string EffectiveUserName { get; }
        string ConnectionString { get; set; }
        string ExecuteXmlReader(string Mdx);
        CellSet ExecuteCommandCellSet(string Mdx);

        /// <summary>
        ///
        /// </summary>
        /// <param name="Mdx"></param>
        /// <returns></returns>
        AdomdDataReader ExecuteCommandReader(string Mdx);
        /// <summary>
        ///
        /// </summary>
        /// <param name="Mdx"></param>
        /// <returns></returns>
        DataTable ExecuteCommandDataSet(string Mdx);
    }
}
