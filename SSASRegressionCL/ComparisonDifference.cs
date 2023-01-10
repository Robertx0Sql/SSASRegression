using System;
using System.Collections.Generic;
using System.Linq;

namespace SSASRegressionCL
{
    public class ComparisonDifference
    {
        public enum CompareSide
        {
            Both,
            Left,
            Right
        }
        public CompareSide Side { get; set; }
        public string Description { get; set; }

        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public string Value { get; set; }
        internal ComparisonDifference(CompareSide side, string description)
        {
            this.Side = side;
            this.Description = description;
        }


        internal ComparisonDifference(CompareSide side, string description, int Row, int Column, string value)
        {
            this.Side = side;
            this.Description = description;
            this.RowIndex = Row;
            this.ColumnIndex = Column;
            this.Value = value;
        }
    }
}
