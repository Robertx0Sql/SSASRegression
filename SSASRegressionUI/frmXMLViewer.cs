using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Xml.Xsl;
using System.Reflection;


namespace SSASRegressionUI
{
    public partial class frmXMLViewer : Form
    {
        private string _xml;
        
        public frmXMLViewer()
        {
            InitializeComponent();
        }
        public string Xml
        {
            get
            {
                return _xml;
            }
            set
            {
                _xml = value;
                if (_xml != string.Empty && _xml != null)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(_xml);
                    DocumentXml = doc;
                }
                else
                {
                    MessageBox.Show("Data Element is empty, nothing to show");
                }
            }
        }
        private XmlDocument DocumentXml
        {
            set
            {
                //var assembly = Assembly.GetExecutingAssembly();
                var resourceName = @"Resources\defaultss.xsl";
                try
                {
                    string resourcePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + resourceName;
                    if (File.Exists(resourcePath))
                    {
                        Stream s = File.OpenRead(resourcePath);
                        //using (Stream s = )
                        //using (Stream s = assembly.GetManifestResourceStream(resourceName))
                        { //  Stream s = null;

                            XmlReader xr = XmlReader.Create(s);
                            XslCompiledTransform xct = new XslCompiledTransform();
                            xct.Load(xr);

                            StringBuilder sb = new StringBuilder();
                            XmlWriter xw = XmlWriter.Create(sb);
                            xct.Transform(value, xw);

                            webBrowser1.DocumentText = sb.ToString();
                        }
                    }
                    else
                        MessageBox.Show("Could not load XSLT Resource from " + resourcePath, "Error loading Data into Browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error loading Data into Browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
