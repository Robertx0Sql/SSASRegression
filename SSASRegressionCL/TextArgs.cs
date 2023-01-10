using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSASRegressionCL
{
    public class TextArgs : EventArgs
    {
        #region Fields
        private string _Message;
        #endregion Fields

        #region ConstructorsH
        public TextArgs(string TextMessage)
        {
            _Message = TextMessage;
        }
        #endregion Constructors

        #region Properties
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        #endregion Properties
    }

}
