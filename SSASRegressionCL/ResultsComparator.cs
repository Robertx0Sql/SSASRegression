using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.AdomdClient;
using System.Data;
using System.IO;

namespace SSASRegressionCL
{
    public enum ValueComparitor {  FormattedValue, RawValue }
     public class ResultsComparator : IResultsComparator
    {
      //  public ValueComparitor valueComparitor = ValueComparitor.FormattedValue;

        public event ComparatorHandler ComparatorResultsEvent;

        public event GetStatusHandler StatusEvent;

        TestResults _testResultsLeft = new TestResults();
        TestResults _testResultsRight = new TestResults();

        private string _FilePathLeft;

        private string _FilePathRight;

        public string FilePathLeft
        {
            get { return _FilePathLeft; }
        }

        public string FilePathRight
        {
            get { return _FilePathRight; }
           
        }
        public string FileNameLeft
        {
            get { return Path.GetFileName (_FilePathLeft); }
        }

        public string FileNameRight
        {
            get { return Path.GetFileName (_FilePathRight); }

        }
        private Dictionary<string, CompareData> _ComparisonResults;

        public Dictionary<string, CompareData> ComparedResults
        {
            get { return _ComparisonResults; }
        }


        public bool Compare(string filePath1, string filePath2, ValueComparitor comparitor)
        {
            if (filePath1 == filePath2)
            {
                throw new Exception(string.Format("The Results files cannot be the same file"));
            }
            _FilePathLeft  = filePath1;
            _FilePathRight = filePath2;
            
            _testResultsLeft.StatusEvent += new GetStatusHandler(OnResultProcess);
            _testResultsRight.StatusEvent += new GetStatusHandler(OnResultProcess);

            _testResultsLeft.LoadFile(filePath1);
            _testResultsRight.LoadFile(filePath2);
            Dictionary<string, TestResult> dresults1 = _testResultsLeft.ResultDictionary;
            Dictionary<string, TestResult> dresults2 = _testResultsRight.ResultDictionary;

            return Compare( dresults1, dresults2, comparitor);
        }

      
        public bool Compare(Dictionary<string, TestResult> dresults1, Dictionary<string, TestResult> dresults2, ValueComparitor comparitor)
        {
            Dictionary<string, CompareData> cd = new Dictionary<string, CompareData>();
            _ComparisonResults=new Dictionary<string, CompareData>();
            int compareFailCount = 0;
            int comparePassCount = 0;
            int compareUntestedCount = 0;

            try
            {
                foreach (TestResult r1 in dresults1.Values)
                {
                    TestResult r2 = null;
                    dresults2.TryGetValue(r1.ID, out r2);
                    
                    if (r2 != null)
                    {
                        RaiseStatusMessage(string.Format("Comparing Results of Test '{0}'", r1.ToString()));

                        if (r1.IsError || r2.IsError)
                        {
                            List<ComparisonDifference> compareErrors = new List<ComparisonDifference>();
                            if (r1.IsError)
                                compareErrors.Add(new ComparisonDifference(ComparisonDifference.CompareSide.Left, r1.Error));
                            if (r2.IsError)
                                compareErrors.Add(new ComparisonDifference(ComparisonDifference.CompareSide.Right, r2.Error));

                            compareUntestedCount++; //CompareResultsExtracted(dresults1, cd, new ComparisonDifference(ComparisonDifference.CompareSide.Both, "Failed to run Test. - Cannot compare"));
                            cd.Add(r1.ID/*.ToString()*/, new CompareData(r1.ID, r1.Description, r1, r2, CompareResultType.Untested, compareErrors));
                        }
                        else
                        {

                            List<ComparisonDifference> compareErrors = CompareCellSets(r1, r2, comparitor);

                            //need to test Compare here!
                            CompareResultType CompareType;

                            if (compareErrors.Count == 0)
                            {
                                comparePassCount++;
                                CompareType = CompareResultType.Pass;
                            }
                            else
                            {
                                compareFailCount++;
                                CompareType = CompareResultType.Fail;
                            }
                            cd.Add(r1.ID/*.ToString()*/, new CompareData(r1.ID, r1.Description, r1, r2, CompareType, compareErrors));
                        }
                    }
                }             
 
                //check/mark the items where there was no match
                compareUntestedCount += ResultNotinOtherFile(dresults1, cd, new ComparisonDifference(ComparisonDifference.CompareSide.Left, "Test/Result only exists in Left File"));
                compareUntestedCount += ResultNotinOtherFile(dresults2, cd, new ComparisonDifference(ComparisonDifference.CompareSide.Right, "Test/Result only exists in Right File"));

                RaiseStatusMessage(string.Format("Finished Compare of {0} Results: {1} Passed, {2} Failed and {3} Untested/Unique.", comparePassCount + compareFailCount + compareUntestedCount, comparePassCount, compareFailCount, compareUntestedCount));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            _ComparisonResults= cd;
            return true;
        }

      
        private int ResultNotinOtherFile(Dictionary<string, TestResult> dresults, Dictionary<string, CompareData> cd, ComparisonDifference compareError)
        {
            List<ComparisonDifference> compareErrorList = new List<ComparisonDifference>();
            compareErrorList.Add(compareError);
            
            int result = 0;
            foreach (TestResult r in dresults.Values)
            {
                if (!cd.ContainsKey(r.ID/*.ToString()*/))
                {
                    result++;
                    RaiseStatusMessage(string.Format("Unable to Compare Test Results '{0}'; Test only Exists in 1 File.", r.ToString()));
                    cd.Add(r.ID/*.ToString()*/, new CompareData(r.ID, r.Description, (compareError.Side == ComparisonDifference.CompareSide.Left) ? r : null, (compareError.Side == ComparisonDifference.CompareSide.Left) ? null : r, CompareResultType.Untested, compareErrorList));
                }
            }
            return result;
        }
        
        private List<ComparisonDifference> CompareCellSets( TestResult r1, TestResult r2, ValueComparitor valueComparitor)
        {
            List<ComparisonDifference> ErrorResults = new List<ComparisonDifference>();
            CellSet c1 = r1.cellSet;
            CellSet c2 = r2.cellSet;
            string CellSetErrors1;
            string CellSetErrors2;

            if (r1.Data == r2.Data) // compare data strings then they are equal 
                return ErrorResults;

            int cs1RowCount, cs2RowCount;
            int cs1ColumnCount, cs2ColumnCount;

            Utils.GetCellSetCounts(c1, out cs1RowCount, out cs1ColumnCount);
            Utils.GetCellSetCounts(c2, out cs2RowCount, out cs2ColumnCount);

            if (cs1RowCount ==0 && cs1ColumnCount ==0 && cs2RowCount ==0 && cs2ColumnCount ==0 && c1!=null && c2!=null)
                return ErrorResults;

            HashSet<string> ErrorHashSet = new HashSet<string>();

            Dictionary<string, CellData> x1 = GetCellSetDictionary(c1, out CellSetErrors1, valueComparitor);
            Dictionary<string, CellData> x2 = GetCellSetDictionary(c2, out CellSetErrors2, valueComparitor);
            
            if (x1 == null || x2 == null)
            {
                if (x1 == null)
                {
                    ErrorResults.Add(new ComparisonDifference(ComparisonDifference.CompareSide.Left, CellSetErrors1));

                }
                if (x2 == null)
                {
                    ErrorResults.Add(new ComparisonDifference(ComparisonDifference.CompareSide.Right, CellSetErrors2));
                }
            }
            else
            {
                if (x1.Keys.Count != x2.Keys.Count)
                {
                   

                    ErrorResults.Add(new ComparisonDifference(ComparisonDifference.CompareSide.Both, string.Format("Row/Column Count Does not Match: Rows, Cols: {0},{1} vs {2},{3}", cs1RowCount, cs1ColumnCount, cs2RowCount, cs2ColumnCount)));
                }

                List<string> found = new List<string>();
                foreach (string Key in x1.Keys)
                {
                    CellData cdLeft = null;
                    CellData cdRight = null;
                    if (!x1.TryGetValue(Key, out cdLeft))
                    {
                        ErrorResults.Add(new ComparisonDifference(ComparisonDifference.CompareSide.Left, String.Format("Missing Left Data for Left Key {0}", Key)));
                    }
                    else
                    {
                        if (!x2.TryGetValue(Key, out cdRight))
                        {
                            ErrorResults.Add(new ComparisonDifference(ComparisonDifference.CompareSide.Left, String.Format("Missing Right Data for Left Key/Data:: '{0}'; '{1}'", Key, cdLeft.value), cdLeft.Row, cdLeft.Column, cdLeft.value));
                        }
                    }
                    if (cdRight != null && cdLeft != null)
                    {
                        if (cdLeft.value == cdRight.value)
                        {
                            found.Add(Key);
                        }
                        else
                        {
                            string description = string.Format("Data Diff for Key '{0}'; values '{1}','{2}' ", Key, cdLeft.value, cdRight.value);
                            //if (ErrorResults.Find(x => x.Description == description) == null)
                            if (!ErrorHashSet.Contains (description))
                            {
                                ErrorHashSet.Add(description);
                                ErrorResults.Add(new ComparisonDifference(ComparisonDifference.CompareSide.Both, description, cdLeft.Row, cdLeft.Column, cdLeft.value));
                            }
                        }
                    }
                    //ErrorResults.Add(string.Format("Could not match Data for Key {0}; values '{1}','{2}' ", Key, cd1.value, cd2.FormattedValue));

                }
                if (found.Count != x1.Keys.Count)
                {
                    // should already be caught
                }

                found.Clear();
                foreach (string Key in x2.Keys)
                {
                    CellData cdLeft = null;
                    CellData cdRight = null;

                    if (!x2.TryGetValue(Key, out cdRight))
                    {
                        ErrorResults.Add(new ComparisonDifference(ComparisonDifference.CompareSide.Right, "Missing Right Data for Right Key " + Key));
                    }
                    else
                    {
                        if (!x1.TryGetValue(Key, out cdLeft))
                        {
                            ErrorResults.Add(new ComparisonDifference(ComparisonDifference.CompareSide.Right, String.Format("Missing Left Data for Right Key/Data:: '{0}'; '{1}'", Key, cdRight.value), cdRight.Row, cdRight.Column, cdRight.value));
                        }
                    }
                    if (cdRight != null && cdLeft != null)
                    {
                        if (cdLeft.value == cdRight.value)
                        {
                            found.Add(Key);
                        }
                        else
                        {
                            string description = string.Format("Data Diff for Key '{0}'; values '{1}','{2}' ", Key, cdLeft.value, cdRight.value);
                         //   if (ErrorResults.Find(x => x.Description == description) == null)
                            if (!ErrorHashSet.Contains(description))
                            {
                                ErrorHashSet.Add(description);
                                ErrorResults.Add(new ComparisonDifference(ComparisonDifference.CompareSide.Both, description, cdRight.Row, cdRight.Column, cdRight.value));
                            }
                        }
                    }
                }
            }



            return ErrorResults;
        }

        private bool CompareCellSets(string ID, string Description, CellSet c1, CellSet c2, ValueComparitor comparitor)
        {
            bool result = false;

            if (c1.Equals(c2))
            {
                RaiseComparatorResultsEvent(ID, Description, "objects are Equal", "Both", true);
                return true;
            }
            else
            {
                if (c1.Cells.Count == c2.Cells.Count)
                {
                    GetCellSetDictionary(c1, comparitor);
                }
                else
                {
                    RaiseComparatorResultsEvent(ID, Description, "objects not equal", "Both", false);
                }
            }
            return result;
        }

        private Dictionary<string, CellData> GetCellSetDictionary(CellSet cst, ValueComparitor comparitor)
        { 
            string CellSetErrors ;
           return  GetCellSetDictionary(cst, out CellSetErrors, comparitor);
        }

        private Dictionary<string, CellData> GetCellSetDictionary(CellSet cst, out string errors, ValueComparitor comparitor)
        {
            Dictionary<string, CellData> d = new Dictionary<string, CellData>();
            int currentRow = 0;
            int currentColumn = 0;
            int colCount = 0;
            int rowCount = 0;
            int colDimCount = 0;
            int rowDimCount = 0;
            errors = "";
            Cell cell = null;
            Member member = null;
            string key = "";
            string lblText = "null";
            try
            {
                //check if any axes were returned else throw error.
                int axes_count = cst.Axes.Count;
                if (axes_count == 0)
                {
                    throw new Exception("No data returned for the selection");
                } //if axes count is not 2
                if (axes_count != 2)
                {
                    throw new Exception(string.Format("The Comparitor only supports queries with two axes. {0} axis specified. ", axes_count));
                } //if no position on either row or column throw error
                if (!(cst.Axes[0].Positions.Count > 0) && !(cst.Axes[1].Positions.Count > 0))
                {
                    throw new Exception("No data returned for the selection - no position on either row or column");
                }
            }
            catch (Exception ex)
            {
                errors = string.Format("Error: {0} ", ex.Message);
            }

            try
            {
                //Number of dimensions on the column
                if (cst.Axes[0].Positions.Count > 0)
                    colDimCount = cst.Axes[0].Positions[0].Members.Count;

                //Number of dimensions on the row
                if (cst.Axes[1].Positions.Count > 0)
                {
                    if (cst.Axes[1].Positions[0].Members.Count > 0)
                    {
                        rowDimCount = cst.Axes[1].Positions[0].Members.Count;
                    }
                }

                //Total rows and columns
                rowCount = cst.Axes[1].Positions.Count + colDimCount; //number of rows + rows for column headers
                colCount = cst.Axes[0].Positions.Count + rowDimCount; //number of columns + columns for row headers

                for (currentRow = 0; currentRow < rowCount; currentRow++)
                {
                    for (currentColumn = 0; currentColumn < colCount; currentColumn++)
                    {
                        cell = null;
                        member = null;
                        lblText = "null";
                        //check if we are writing to a ROW having column header
                        if (currentRow < colDimCount)
                        {
                            //check if we are writing to a cell having row header
                            if (currentColumn < rowDimCount)
                            {
                                //this should be empty cell -- it's on top left of the grid.
                            }
                            else
                            {
                                //this is a column header cell -- use member caption for header
                                 member = cst.Axes[0].Positions[currentColumn - rowDimCount].Members[currentRow];
                                key = CreateComparitorDictionaryKey("Column Member  ", (currentColumn - rowDimCount), currentRow);
                                lblText = GetMemberCaption(member);
                                AddCellSetComparitorDictionaryItem(d, currentRow, currentColumn, colDimCount, rowDimCount, key, lblText);
                                //if (!d.ContainsKey(key))
                                //{
                                //    d.Add(key, new CellData { value = lblText, Column = currentColumn, ColumnOffSet = rowDimCount, Row = currentRow, RowOffSet = colDimCount });
                                //}
                            }
                        }
                        else
                        {
                            //row having data (not column headers)
                            //check if we are writing to a cell having row header
                            if (currentColumn < rowDimCount)
                            {
                                //this is a row header cell -- use member caption for header
                                 member = cst.Axes[1].Positions[currentRow - colDimCount].Members[currentColumn];
                                 key = CreateComparitorDictionaryKey("Row Member ", currentColumn, (currentRow - colDimCount));
                                 lblText = GetMemberCaption(member);
                                 AddCellSetComparitorDictionaryItem(d, currentRow, currentColumn, colDimCount, rowDimCount, key, lblText);
                            }
                            else
                            {
                                //this is data cell.. 
                                int col_index = currentColumn - rowDimCount;
                                int row_index = currentRow - colDimCount;
                               
                                 cell = cst[currentColumn - rowDimCount, currentRow - colDimCount];
                                 key = CreateComparitorDictionaryKey("Cell ", col_index, row_index);
                                 lblText = GetCellValue(cell, comparitor);
                                 AddCellSetComparitorDictionaryItem(d, currentRow, currentColumn, colDimCount, rowDimCount, key, lblText);
                                //if (!d.ContainsKey(key))
                                //{
                                //    d.Add(key, new CellData { value = lblText, Column = currentColumn, ColumnOffSet = rowDimCount, Row = currentRow, RowOffSet = colDimCount });
                                //}
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errors = string.Format("Error @{2} {0} after {1} comparison points ", ex.Message, d.Count, key);
                d = null;
            }
            return d;
        }

        private static void AddCellSetComparitorDictionaryItem(Dictionary<string, CellData> d, int currentRow, int currentColumn, int colDimCount, int rowDimCount, string key, string lblText)
        {
            if (!d.ContainsKey(key))
            {
                d.Add(key, new CellData { value = lblText, Column = currentColumn, ColumnOffSet = rowDimCount, Row = currentRow, RowOffSet = colDimCount });
            }
        }

        private static string GetMemberCaption(Member member)
        {
            string lblText = "null";
            try
            {
                if (member != null)
                {
                    lblText = member.Caption;
                }
            }
            catch (Exception ex)
            {
                lblText = "Get Member Error " + ex.Message;
            }
            return lblText;
        }

        private static string GetCellValue(Cell cell, ValueComparitor valueComparitor )
        {
            string lblText = string.Empty;
            try
            {
                if (cell != null)
                {
                    object obj = null;
                    if (valueComparitor == ValueComparitor.RawValue)
                    {
                        obj = cell.Value;
                    }
                    else
                    {

                        obj = cell.FormattedValue;
                    }
                    if (obj != null)
                    {
                        lblText = obj.ToString();
                    }
                    else
                    {
                        lblText = "null";
                    }
                }
                else
                {
                    lblText = "null object";
                }
            }
            catch (AdomdErrorResponseException ex)
            {
                lblText = "#Error :: "+ ex.Message ;
            }
            catch (Exception ex)
            {
                lblText = "Get CellValue Error " + ex.Message;
            }
            return lblText;
        }

        private static string CreateComparitorDictionaryKey(string fieldtype, int col_index, int row_index)
        {
            return fieldtype + row_index.ToString() + ", " + col_index.ToString();
        }

        private void RaiseComparatorResultsEvent(string id, string description, string message, string file, bool CompareResult)
        {
            if (ComparatorResultsEvent != null)
            {
                CompareEventArgs e = new CompareEventArgs();
                e.message = message;
                e.id = id;
                e.description = description;
                e.file = file;
                e.CompareResult = CompareResult;
                ComparatorResultsEvent(this, e);
            }
        }
        public void OnResultProcess(object sender, ResultEventArgs ee)
        {
            RaiseStatusMessage(ee.message, ee.result);
        }
        private void RaiseStatusMessage(string message)
        {
            RaiseStatusMessage(message, ResultEventArgs.EventResult.Information);
        }

        private void RaiseStatusError(string message)
        {
            RaiseStatusMessage(message, ResultEventArgs.EventResult.Error);
        }

        private void RaiseStatusMessage(string message, ResultEventArgs.EventResult result)
        {
            if (StatusEvent != null)
            {
                ResultEventArgs e = new ResultEventArgs();
                e.message = message;
                e.result = result;

                StatusEvent(this, e);
            }
        }

        public DataTable ResultStatistics()
        {

            DataTable table = new DataTable();
            table.Columns.Add("Test ID", typeof(string));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("Compare Result", typeof(string));
            table.Columns.Add("Performance (L-R)", typeof(string)); 
            table.Columns.Add("Left Start Date Time", typeof(DateTime));
            table.Columns.Add("Left End Date Time", typeof(DateTime));
            table.Columns.Add("Left Test Time", typeof(string));
            table.Columns.Add("Left Error", typeof(string));
            table.Columns.Add("Right Start Date Time", typeof(DateTime));
            table.Columns.Add("Right End Date Time", typeof(DateTime));
            table.Columns.Add("Right Test Time", typeof(string));
            table.Columns.Add("Right Error", typeof(string));
            

          
             TimeSpan TotalTimeLeft = TimeSpan.Zero; //_testResultsLeft.ResultTimeSpan;

            TimeSpan TotalTimeRight = TimeSpan.Zero;// _testResultsRight.ResultTimeSpan;


            foreach (string id in _ComparisonResults.Keys)
            {
               // string CompareTypeCode = "-";
                CompareData cd;
                if (_ComparisonResults.TryGetValue(id, out cd))
                {
                    //    switch (cd.CompareResult)
                    //    {
                    //        case CompareResultType.Pass:
                    //            CompareTypeCode = "P";
                    //            break;
                    //        case CompareResultType.Fail:
                    //            CompareTypeCode = "F";
                    //            break;
                    //        case CompareResultType.Untested:
                    //            CompareTypeCode = "?";
                    //            break;
                    //    }

                    TestResult rsLeft = cd.resultLeft;
                    TestResult rsRight = cd.resultRight;

                    DateTime? StartDateLeft = null;
                    DateTime? EndDateLeft = null;
                    string QueryTimeFormatedLeft = null;
                    string ErrorLeft = null;
                    TimeSpan QueryLeft   = TimeSpan.Zero;

                    DateTime? StartDateRight = null;
                    DateTime? EndDateRight = null;
                    string QueryTimeFormatedRight = null;
                    string ErrorRight = null;
                    TimeSpan QueryRight = TimeSpan.Zero;
                    
                    if (rsLeft != null)
                    {
                        StartDateLeft = rsLeft.StartDate;
                        EndDateLeft = rsLeft.EndDate;
                        QueryTimeFormatedLeft = string.Format("{0:c}", rsLeft.QueryTimeSpan);
                        ErrorLeft = rsLeft.Error;
                        QueryLeft= rsLeft.QueryTimeSpan;
                        if (cd.CompareResult != CompareResultType.Untested) 
                            TotalTimeLeft += rsLeft.QueryTimeSpan;
                    }
                    if (rsRight != null)
                    {
                        
                        StartDateRight = rsRight.StartDate;
                        EndDateRight = rsRight.EndDate;
                        QueryTimeFormatedRight = string.Format("{0:c}", rsRight.QueryTimeSpan);
                        ErrorRight = rsRight.Error;
                        QueryRight = rsRight.QueryTimeSpan;
                        if (cd.CompareResult != CompareResultType.Untested) 
                            TotalTimeRight += rsRight.QueryTimeSpan;
                    }
                    
                    table.Rows.Add(cd.ID, cd.Description, cd.CompareResult.ToString()
                        , (cd.CompareResult == CompareResultType.Untested)?null:string.Format("{0:p}", (QueryLeft - QueryRight).TotalMilliseconds / QueryLeft.TotalMilliseconds)
                        , StartDateLeft, EndDateLeft, QueryTimeFormatedLeft, ErrorLeft
                        , StartDateRight, EndDateRight, QueryTimeFormatedRight, ErrorRight
                        );
                }
            }
            table.Rows.Add("TOTALS::", "", "", string.Format("{0:p}", (TotalTimeLeft - TotalTimeRight).TotalMilliseconds / TotalTimeLeft.TotalMilliseconds)
                    , _testResultsLeft.StartDate, _testResultsLeft.EndDate, string.Format("{0:c}", TotalTimeLeft), ""
                    , _testResultsRight.StartDate, _testResultsRight.EndDate, string.Format("{0:c}", TotalTimeRight), ""
                    );
            return table;
        }
       
    }
}
