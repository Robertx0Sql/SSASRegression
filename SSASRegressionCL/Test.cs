using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SSASRegressionCL
{

    [Serializable]
    [XmlType("test")]
    public class Test : ITest
    {
        public Test()
        {
            ID = null;
        }

        public Test(string id, string description, string mdx)
        {
            this.ID = id;
            this.MDX = mdx;
            this.Description = description;
        }

        
        private string _id;
        private string _description;
        private string _mdx;

        public override string ToString()
        {
            return String.Format("{0} [{1}]", Description, ID);
        }

        [XmlAttribute(AttributeName = "id")]
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (value == string.Empty || value == null)
                    _id = Guid.NewGuid().ToString().ToUpper();
                else
                    _id = value;
            }
        }
        
        [XmlElement(ElementName = "description")]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        [XmlElement(ElementName = "mdx", DataType = "normalizedString")]
        public string MDX
        {
            get
            {
                return _mdx;
            }
            set
            {//fix mdx to set lineEnding as \r\n instead of \n as set by XML loader
                if (_mdx==null)
                    _mdx=value.Replace("\n", Environment.NewLine).Replace("\r\r\n", Environment.NewLine); 
                else
                _mdx = value;
            }
        }
        
    }
}
