using System;
using System.Collections.Generic;
using SSASRegressionCL;
using PowerArgs;

namespace SSASRegressionCmd
{
    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    class ProgramNxt
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


        static ExitCode exitCode = ExitCode.Success;

        static int Main(string[] args)
        {
            exitCode = ExitCode.INVALID_COMMAND_LINE;
            
            Args.InvokeAction<ProgramNxt>(args);

            Console.WriteLine(string.Format("\r\nExitCode: {0}", exitCode));

            Environment.Exit((int)exitCode);
            return (int)exitCode;
        }


        [HelpHook, ArgShortcut("-?"), ArgDescription("Shows this help")]
        public bool Help { get; set; }

        [ArgActionMethod, ArgDescription("Run Tests against a SSAS Database and generate an output file"), ArgShortcut("T")]
        public void Test(RunTestArgs args)
        {
            Console.WriteLine("Running Tests on Server='{2}'; Catalog='{3}' with {4} threads... \r\n\tTest File: {0}\r\n\tResults File: {1}", args.TestFile, args.ResultFile, args.Server, args.Catalog, args.Parallel, args.UserName);
            try
            {
                RunTests(args.TestFile, args.ResultFile, args.Server, args.Catalog, args.Parallel, args.UserName);
            }
            catch (Exception ex)
            {
                ConsoleColor origConsoleColor = Console.ForegroundColor; //save this for later reset
                Console.ForegroundColor = ConsoleColor.Red;
                if (!lastError.EndsWith(ex.Message))
                    Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }
        }

        [ArgActionMethod, ArgDescription("Run Tests against a SSAS Database and generate an output file"), ArgShortcut("TCS")]
        public void TestConnString(RunTestConnStringArgs args)
        {
            string Server="";
            string Catalog="";
            
            
            Console.WriteLine("Running Tests on Server='{2}'; Catalog='{3}' with {4} threads... \r\n\tTest File: {0}\r\n\tResults File: {1}", args.TestFile, args.ResultFile, Server, Catalog, args.Parallel );
            try
            {
                exitCode = ExitCode.TestFailed;


                SSASRegressionCL.TestResults r = new SSASRegressionCL.TestResults(args.ConnectionString,args.Parallel );
                r.StatusEvent += new GetStatusHandler(OnResultProcess);
                if (r.RunTeststoFile(args.TestFile ,args.ResultFile))
                {
                    Console.WriteLine(string.Format("Successfully saved Results to {0}", args.ResultFile));
                    exitCode = ExitCode.Success;
                }
                
            }
            catch (Exception ex)
            {
                ConsoleColor origConsoleColor = Console.ForegroundColor; //save this for later reset
                Console.ForegroundColor = ConsoleColor.Red;
                if (!lastError .EndsWith (ex.Message))
                    Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }
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

        [ArgActionMethod, ArgDescription("Compares 2 Result files & show Differences"), ArgShortcut("C")]
        public void Compare(RunCompareArgs args)
        {
            //Console.WriteLine("RUN Compare... ");
            string resultsFile1 = args.Result1;
            string resultsFile2 = args.Result2;
            bool detail = args.Verbose;

            ValueComparitor comparitor = ValueComparitor.FormattedValue;

            Console.WriteLine("Running Compare ... \r\n\tResults File 1 (Left): {0}\r\n\tResults File 2 (Right): {1}\r\n\tVerbose {2}", resultsFile1, resultsFile2,detail);
            
            bool result = true;

            Console.WriteLine();
            ConsoleColor origConsoleColor = Console.ForegroundColor; //save this for later reset

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
                                        //Console.WriteLine("\t {0}", cd.description);
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("\tCompare PASSED: '{0}'", cd.ToString ());
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

           if(result)
               exitCode = ExitCode.Success;
           else
               exitCode = ExitCode.CompareFailed ;
        }


        public static void OnResultProcess(object sender, ResultEventArgs ee)
        {
            ConsoleColor origConsoleColor = Console.ForegroundColor; //save this for later reset
            if (ee.result == ResultEventArgs.EventResult.Error)
            {
                lastError = ee.message;
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(ee.message);

            Console.ForegroundColor = origConsoleColor;
        }



    }
    //internal class Option
    //{
    //    public enum ArgOption { Test, Result, Server, Catalog, Parallel, EffectiveUserName, Result1, Result2 }

    //    public enum OptionType { None = 0, Test , Compare , Both};
    //    public string Value {get;set;}
    //    public string Description { get; set; }
    //    public OptionType Type { get; set; }
        
    //    public Option(OptionType type)
    //    {
    //        this.Type = type;
    //    }
    //    public Option(OptionType type, string description)
    //    {
    //        this.Type = type;
    //        this.Description = description;
    //    }
    //    public Option(OptionType type, string description, string value)
    //    {
    //        this.Type = type;
    //        this.Description = description;
    //        this.Value = value;
    //    }
    //}

    //[ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    //public class CalculatorProgram
    //{
      
    //}

    public class RunTestArgs
    {
        [ArgRequired, ArgDescription("Input file for the Test")]
        public string TestFile { get; set; }

        [ArgRequired, ArgDescription("Output file of the Result")]
        public string ResultFile { get; set; }

        [ArgRequired, ArgDescription("Name of SSAS Server")]
        public string Server { get; set; }

        [ArgRequired, ArgDescription("Name of SSAS Catalog/Database")]
        public string Catalog { get; set; }

        [ArgDescription("Number of threads to run in Parallel"), ArgDefaultValue(1)]
        public int Parallel { get; set; }

        [ArgDescription("User identity must be impersonated on the server. Specify the account in a domain\\user format. To use this property, the caller must have administrative permissions in Analysis Services")]
        public string UserName { get; set; }
    }

    public class RunTestConnStringArgs
    {
        [ArgRequired, ArgDescription("Input file for the Test")]
        public string TestFile { get; set; }

        [ArgRequired, ArgDescription("Output file of the Result")]
        public string ResultFile { get; set; }

        [ArgRequired, ArgDescription("Connection String"), ArgShortcut("cs")]
        public string ConnectionString { get; set; }

        [ArgDescription("Number of threads to run in Parallel"), ArgDefaultValue(1)]
        public int Parallel { get; set; }

        
    }
    public class RunCompareArgs
    {
        [ArgRequired, ArgDescription("filePath of the 1st Result"), ArgShortcut("R1")]
        public string Result1 { get; set; }
        
        [ArgRequired, ArgDescription("filePath of the 2nd Result"), ArgShortcut("R2")]
        public string Result2 { get; set; }
       
        [ArgDescription("Show verbose details of Compare"), ArgDefaultValue(false)]
        public bool Verbose { get; set; }

    }
    
}
