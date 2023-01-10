using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSASRegressionCL;
using System.Xml.Linq;
using System.Xml;

namespace SSASRegressionCmd
{
    internal class Option
    {
        public enum OptionType { None = 0, Test, Compare, Both };
        public string Value { get; set; }
        public string Description { get; set; }
        public OptionType Type { get; set; }

        public Option(OptionType type)
        {
            this.Type = type;
        }
        public Option(OptionType type, string description)
        {
            this.Type = type;
            this.Description = description;
        }
        public Option(OptionType type, string description, string value)
        {
            this.Type = type;
            this.Description = description;
            this.Value = value;
        }
    }
}
