using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
namespace SSASRegressionCL
{
    public class CellSetDataTable:DataTable
    {
        public CellSetDataTable()
        {
            
        }
        public CellSetDataTable(string tableName)
            : base(tableName)
        {
            
        }
        public CellSetDataTable(string tableName, string tableNamespace)
            : base(tableName, tableNamespace)
        {
            
        }
        protected CellSetDataTable(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            
        }
        public bool IsCellError;

    }
}
