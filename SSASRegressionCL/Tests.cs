using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace SSASRegressionCL
{
    [XmlRoot("Tests")]
    public class Tests
    {

        //private const string cstrTests = "Tests";
        public delegate void GetResultsHandler(object sender, ResultEventArgs e);
        public event GetResultsHandler GetResultsEvent;
        //const string cstrTest = "test";

        [XmlElement("test")]
        public List<Test> TestList  { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>List of Test items</returns>
        public List<Test> LoadFile(string filename)
        {
            List<Test> tests = null;
            Utils.ValidateTestFilePath(filename);
            try
            {
                RaiseEvent(string.Format("Load tests '{0}'", filename)); 
                XmlSerializer serializer = new XmlSerializer(typeof(Tests));
                Tests ts;
                // Set the reader settings.
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;
                settings.IgnoreProcessingInstructions = true;
                settings.IgnoreWhitespace = true;

                // A FileStream is needed to read the XML document.
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                   XmlReader reader = XmlReader.Create(fs);
                  
                    ts = (Tests)serializer.Deserialize(reader);
                }
                if (ts != null)
                {
                    tests = ts.TestList;
                }

                //Load xml
                //
                
                
                
                //var docTests = XDocument.Load(filePath);

                //// Query the data and write out a subset of contacts
                //var _Tests = from t in docTests.Descendants(cstrTest)
                //             select new Test { MDX = t.Element("mdx").Value.Replace("\n", Environment.NewLine).Replace("\r\r\n", Environment.NewLine),
                //             ID = t.Attribute("id").Value,
                //             Description = t.Element("description").Value };
                //tests = _Tests.ToList<Test>();
            }
            catch (Exception ex)
            {
                throw new Exception("LoadTest File: " + ex.Message, ex);
            }
            return tests;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tests"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>

        public static bool SaveFile(List<Test> tests, string filename)
        {
            bool result = false;
            Utils.ValidateNewResultFilePath(filename);
            Tests ts = new Tests();
            ts.TestList = tests;
            try
            {
                //preserve line endings(\r\n) and tabs(\t) https://msdn.microsoft.com/en-us/library/system.xml.xmlwritersettings.newlinehandling%28v=vs.110%29.aspx
                XmlWriterSettings ws = new XmlWriterSettings();
                ws.NewLineHandling = NewLineHandling.Replace;
                ws.Indent = true;
                
                using (XmlWriter writer = XmlWriter.Create(filename, ws)) 
                {
                    //TextWriter writer = new StreamWriter(filename);
                    XmlSerializer serializer = new XmlSerializer(typeof(Tests));

                    serializer.Serialize(writer, ts);
                    //   writer.Close();
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Save Tests file '{0}'", filename), ex);
            }
            return result;
        }


        /// <summary>
        /// ImportQueries
        /// </summary>
        /// <param name="FileNames"></param>
        /// <returns>List of Test Items</returns>
        public static List<Test> Import(string[] FileNames)
        {
            List<Test> tests = new List<Test>();
            // Read the files
            foreach (string filepath in FileNames)
            {

                Test t = new Test();
                {
                    t.Description = Path.GetFileNameWithoutExtension(filepath);
                    using (StreamReader fs = new System.IO.StreamReader(filepath))
                    {
                        t.MDX = fs.ReadToEnd();
                    }
                }
                tests.Add(t);

            }
            return tests;
        }

        /// <summary>
        /// Export Tests to Folder
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="tests"></param>
        public static void Export(string folderName, List<Test> tests)
        {

            foreach (Test t in tests)
            {
                string filepath = String.Format(@"{0}\{1}.mdx", folderName, t);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filepath))
                {
                    file.WriteLine(t.MDX);
                }
            }
        }
        private void RaiseEvent(string message)
        {
            if (GetResultsEvent != null)
            {
                ResultEventArgs e = new ResultEventArgs() { message = message };

                GetResultsEvent(this, e);
            }
        }
    }
}