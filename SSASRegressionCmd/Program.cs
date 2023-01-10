using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSASRegressionCL;
using System.Xml.Linq;
using System.Xml;


namespace SSASRegressionCmd
{
    class Program
    {

        public enum ExitCode : int
        {
            Success = 0,
            CompareFailed = 1,
            TestFailed = 2,
            INVALID_COMMAND_LINE = 99,
            UnknownError = 10
        }
        static string lastError = string.Empty;

        static ExitCode exitCode = ExitCode.INVALID_COMMAND_LINE;


        public enum  ArgOption { Test,  Result, Server, Catalog, Parallel ,EffectiveUserName ,Result1,   Result2 }
        
        static int Main(string[] args)
        {
            /*Args 
             * File of Test Mdx - TestFile.xml  
             * Server
             * Catalog
             * OutputFile 
             */

            try
            {
                if (args.Length == 0)
                    ShowHelp();
                else
                {
                    bool RunCompare = false;
                    bool RunTest = false;
                    bool ArgError = false;
                    Dictionary<ArgOption, Option> options = GetOptionList();

                    if (args.Length <= options.Count)
                    {
                        foreach (ArgOption option in options.Keys)
                        {
                            var param1 = GetOptionValue(args, option);
                            options[option].Value = param1;
                            if (param1 != null)
                            {
                                switch (options[option].Type)
                                {
                                    case Option.OptionType.Test:
                                        RunTest = true;
                                        break;
                                    case Option.OptionType.Compare:
                                        RunCompare = true;
                                        break;
                                    case Option.OptionType.Both:
                                        RunTest = true;
                                        RunCompare = true;
                                        break;
                                }
                            }
                        }
                    }
                    else
                        ArgError = true;

                    ValueComparitor comparitor = ValueComparitor.FormattedValue;

                    if (ArgError || (!RunTest && !RunCompare))
                        ShowHelp(options);
                    else
                    {
                        if (RunTest)
                        {
                            string testFile = options[ArgOption.Test].Value;
                            string resultsFile = options[ArgOption.Result].Value;
                            string Server = options[ArgOption.Server].Value;
                            string CatalogName = options[ArgOption.Catalog].Value;
                            string ParallelThreads = options[ArgOption.Parallel].Value;
                            string effectiveUserName = options[ArgOption.EffectiveUserName].Value;
                           

                            int MaxThreads = 0;
                            if (!Int32.TryParse(ParallelThreads, out MaxThreads))
                                MaxThreads = 1;
                            Console.WriteLine("Running Tests on Server='{2}'; Catalog='{3}' with {4} threads... \r\n\tTest File: {0}\r\n\tResults File: {1}", testFile, resultsFile, Server, CatalogName, MaxThreads);

                            RunTests(testFile, resultsFile, Server, CatalogName, MaxThreads, effectiveUserName);
                        }
                        if (RunCompare)
                        {
                            
                            string resultsFile1 = options[ArgOption.Result1].Value;
                            string resultsFile2 = options[ArgOption.Result2].Value;

                            Console.WriteLine("Running Compare ... \r\n\tResults File 1: {0}\r\n\tResults File 2: {1}", resultsFile1, resultsFile2);
             
                            CompareResults(resultsFile1, resultsFile2, comparitor);
                          
                        }
                    }
                }
            }
            catch (Exception ex)
            { Console.WriteLine("Error: {0}", ex.Message);}

            Console.WriteLine(string.Format("\r\nExitCode: {0}", exitCode));

            Environment.Exit((int)exitCode);
            return (int)exitCode;
        }


        private static Dictionary<ArgOption, Option> GetOptionList()
        {
            Dictionary<ArgOption, Option> result = new Dictionary<ArgOption, Option>();
            result.Add(ArgOption.Test, new Option(Option.OptionType.Test, "filepath"));
            result.Add(ArgOption.Result, new Option(Option.OptionType.Test, "filepath"));
            result.Add(ArgOption.Server, new Option(Option.OptionType.Test, "ServerName"));
            result.Add(ArgOption.Catalog, new Option(Option.OptionType.Test, "CatalogName"));
            result.Add(ArgOption.Parallel, new Option(Option.OptionType.Test, "Number of threads"));
            result.Add(ArgOption.EffectiveUserName, new Option(Option.OptionType.Test, "EffectiveUserName"));

            result.Add(ArgOption.Result1, new Option(Option.OptionType.Compare, "filepath"));
            result.Add(ArgOption.Result2, new Option(Option.OptionType.Compare, "filepath"));
           
            return result;

        }

        private static string GetOptionValue(string[] args, ArgOption option)
        {
            string result =null;
            string ArgName = "-" + option.ToString() + ":";
            var ArgNameValue= args.SingleOrDefault(arg => arg.StartsWith(ArgName, StringComparison.InvariantCultureIgnoreCase));
            if (ArgNameValue!=null)
                result = ArgNameValue.Substring(ArgName.Length);

            Console.WriteLine(string.Format(" .... arg {0} , value {1}", option.ToString(), (result!=null) ? result : "{null}" ));

            return result;
        }
        private static void ShowHelp(Dictionary<ArgOption, Option> options)
        {
            Console.WriteLine("SSASRegressionCmd ....");
            Console.WriteLine("\tTest Parameters ....");
            foreach (ArgOption option in options.Keys)
            {
                if (options[option].Type==Option.OptionType.Test)
                    Console.WriteLine("\t   -{0}:<{1}>", option, options[option].Description);
            }
            Console.WriteLine("\t");
            Console.WriteLine("\tCompare Parameters ....");
            foreach (ArgOption option in options.Keys)
            {
                if (options[option].Type == Option.OptionType.Compare)
                    Console.WriteLine("\t   -{0}:<{1}>", option, options[option].Description);
            }
            
            //ShowHelp();
        }
        private static void ShowHelp()
        {
            Dictionary<ArgOption, Option> options = GetOptionList();
            ShowHelp(options);
        }
        private static bool CompareResults(string resultsFile1, string resultsFile2, ValueComparitor comparitor)
        {
           return CompareResults(resultsFile1, resultsFile2, true,  comparitor);
        }
        private static bool CompareResults(string resultsFile1, string resultsFile2, bool detail, ValueComparitor comparitor)
        {
            ConsoleColor origConsoleColor = Console.ForegroundColor; //save this for later reset
            bool result = false;
            try
            {//  GetFileData

                ResultsComparator c = new ResultsComparator();

                c.Compare(resultsFile1, resultsFile2, comparitor);
                Dictionary<string, CompareData> _DataCompare = c.ComparedResults;

                //run thru twice 1=> summary
                int FailedTests = 0;
                int Untested = 0;
                int PassedTests = 0;
                foreach (string key in _DataCompare.Keys)
                {
                    CompareData cd;
                    if (_DataCompare.TryGetValue(key, out cd))
                    {

                        switch (cd.CompareResult)
                        {
                            case CompareResultType.Pass:
                                PassedTests++;
                                break;
                            case CompareResultType.Fail:
                                FailedTests++;
                                break;
                            case CompareResultType.Untested:
                                Untested++;
                                break;
                        }
                    }
                }

                if (PassedTests == _DataCompare.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine("Compare Results PASSED: {0} of {1} Successfully Compared as identical.", PassedTests, _DataCompare.Count);
                    result = true;
                    if (detail)
                    {
                        foreach (string key in _DataCompare.Keys)
                        {
                            CompareData cd;
                            if (_DataCompare.TryGetValue(key, out cd))
                            {
                                switch (cd.CompareResult)
                                {
                                    case CompareResultType.Pass:
                                        Console.ForegroundColor = origConsoleColor;
                                        Console.WriteLine("\tCompare PASSED: '{0}'", cd.ToString());
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    //2nd time to show detail any errors 
                    exitCode = ExitCode.CompareFailed;

                    foreach (string key in _DataCompare.Keys)
                    {
                        CompareData cd;
                        if (_DataCompare.TryGetValue(key, out cd))
                        {

                            switch (cd.CompareResult)
                            {
                                case CompareResultType.Pass:
                                    //Console.WriteLine("\t {0}", cd.description);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Compare PASSED: '{0}'", cd.ToString());
                                    break;
                                case CompareResultType.Fail:

                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Compare FAILED: '{0}'", cd.ToString());
                                    Console.ForegroundColor = origConsoleColor; //reset the color to std;
                                    if (detail)
                                        foreach (ComparisonDifference item in cd.CompareErrors)
                                        {
                                            Console.WriteLine("\t {0}", item.Description);
                                        }
                                    result = false;
                                    break;
                                case CompareResultType.Untested:
                                    Console.ForegroundColor = ConsoleColor.Red;

                                    //origConsoleColor
                                    Console.Write("Compare UNTESTED: '{0}'", cd.ToString());
                                    if (cd.CompareErrors.Count > 0)
                                    {
                                        ComparisonDifference item = cd.CompareErrors[0];
                                        Console.ForegroundColor = origConsoleColor;
                                        Console.Write(" {1}", cd.ToString(), item.Description);
                                    }
                                    Console.Write("\r\n");


                                    //        strCompareErrors = "<UNKNOWN> NOT TESTED";
                                    result = false;
                                    break;
                            }

                        }
                    }
                }

            }

            catch (Exception ex)
            {
                result = false;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error while Comparing Results:");
                Console.ForegroundColor = origConsoleColor;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }

            if (result)
                exitCode = ExitCode.Success;
            else
                exitCode = ExitCode.CompareFailed;
            return result;
        }

        private static void RunTests(string testFile, string resultsFile, string Server, string CatalogName, int MaxThreads, string effectiveUserName)
        {
            exitCode = ExitCode.TestFailed;

            SSASRegressionCL.TestResults r = new SSASRegressionCL.TestResults(Server, CatalogName, MaxThreads, effectiveUserName);
            r.StatusEvent += new GetStatusHandler(OnResultProcess);

            //Results => file

            if (r.RunTeststoFile(testFile, resultsFile))
            {
                Console.WriteLine(string.Format("Successfully saved Results to {0}", resultsFile));
                exitCode = ExitCode.Success;
            }
        }
        public static void OnResultProcess(object sender, ResultEventArgs ee)
        {
            Console.WriteLine(ee.message);
        }




    }
    internal class Option 
    {
        public enum OptionType { None = 0, Test , Compare , Both};
        public string Value {get;set;}
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
