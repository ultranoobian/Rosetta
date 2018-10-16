using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net;
using System.Reflection;

namespace GPC_BOM {
    class ExcelConversion {

        #region Declare statics and constants

        /*
         * xxx_column: used at runtime to indicate column index where xxx was found
         * xxx_row: used at runtime to indicate row index where xxx was found
         * xxx_sap_column: set at compile time to indicate column index of xxx in SAP format
         * xxx_sap_row: set at compile time to indicate row index of xxx in SAP format
         * xxx_quotewin_column: set at compile time to indicate column index of xxx in Quotewin format
         * xxx_quotewin_row: set at compile time to indicate row index of xxx in Quotewin format
         * */

        public static int? level_column = null;
        public static int? level_row = null;
        public const int level_sap_column = 1;
        public const int level_sap_row = 5;
        public const int level_quotewin_multi_column = 1;
        public const int level_quotewin_multi_row = 2;
        // Quotewin_single doesn't have level field

        public static int? cpn_column = null;
        public static int? cpn_row = null;
        public const int cpn_sap_column = 2;
        public const int cpn_sap_row = 5;
        public const int cpn_quotewin_multi_column = 2;
        public const int cpn_quotewin_multi_row = 2;
        public const int cpn_quotewin_single_column = 3;
        public const int cpn_quotewin_single_row = 2;

        public static int? description_column = null;
        public static int? description_row = null;
        public const int description_sap_column = 3;
        public const int description_sap_row = 5;
        public const int description_quotewin_multi_column = 7;
        public const int description_quotewin_multi_row = 2;
        public const int description_quotewin_single_column = 8;
        public const int description_quotewin_single_row = 2;

        public static int? quantity_column = null;
        public static int? quantity_row = null;
        public const int quantity_sap_column = 4;
        public const int quantity_sap_row = 5;
        public const int quantity_quotewin_multi_column = 5;
        public const int quantity_quotewin_multi_row = 2;
        public const int quantity_quotewin_single_column = 6;
        public const int quantity_quotewin_single_row = 2;

        public static int? designator_column = null;
        public static int? designator_row = null;
        public const int designator_sap_column = 5;
        public const int designator_sap_row = 5;
        public const int designator_quotewin_multi_column = 6;
        public const int designator_quotewin_multi_row = 2;
        public const int designator_quotewin_single_column = 7;
        public const int designator_quotewin_single_row = 2;

        public static int? manufacturer_column = null;
        public static int? manufacturer_row = null;
        public const int manufacturer_sap_column = 6;
        public const int manufacturer_sap_row = 5;
        public const int manufacturer_quotewin_multi_column = 16;
        public const int manufacturer_quotewin_multi_row = 2;
        public const int manufacturer_quotewin_single_column = 17;
        public const int manufacturer_quotewin_single_row = 2;

        public static int? mpn_column = null;
        public static int? mpn_row = null;
        public const int mpn_sap_column = 7;
        public const int mpn_sap_row = 5;
        public const int mpn_quotewin_multi_column = 18;
        public const int mpn_quotewin_multi_row = 2;
        public const int mpn_quotewin_single_column = 19;
        public const int mpn_quotewin_single_row = 2;

        public static int? process_column = null;
        public static int? process_row = null;
        public const int process_sap_column = 8;
        public const int process_sap_row = 5;
        // Quotewin format does not have process field
        //public const int process_quotewin_column = 1;
        //public const int process_quotewin_row = 2;

        public static int? notes_column = null;
        public static int? notes_row = null;
        public const int notes_sap_column = 9;
        public const int notes_sap_row = 5;
        // Just a guess here that 'notes' corresponds to 'long comment'
        public const int notes_quotewin_multi_column = 24;
        public const int notes_quotewin_multi_row = 2;
        public const int notes_quotewin_single_column = 25;
        public const int notes_quotewin_single_row = 2;

        public const string dataMissingMessage = "<Missing Data>";

        #endregion

        public void transformExcel(frmMain mainApp, string excelFile, string outFile, int headerRow, string fileType) {

            /* TODO
             * This function simply takes the first sheet that it sees and runs with it.
             * Probably need an encapsulting foreach loop per sheet to cover certain BOM types.
             * */

            // Initialise MS Excel variables
            Excel.Application oExcel = null;
            Excel.Workbook oWorkbook = null;
            Excel.Workbook oWorkbook2 = null;
            Excel.Worksheet oWorksheet = null;
            Excel.Worksheet oWorksheet2 = null;

            try {
                // Start Excel
                mainApp.statusUpdate2("(Starting Excel)");
                oExcel = new Excel.Application();
                oExcel.Visible = false;
                oExcel.DisplayAlerts = false;
                if (oExcel == null) {
                    MessageBox.Show("Excel could not be started. Please check that it is installed correctly and allows automation.");
                    Debug.WriteLine("Excel could not be started. Check that compiled app reference matches Office version installed.");
                    return;
                }
                else {
                    Debug.WriteLine("Excel.Application is started");
                }

                // Open Excel spreadsheet
                mainApp.statusUpdate2("(Opening Excel document)");
                oWorkbook = oExcel.Workbooks.Open(excelFile);
                // This sets the variable as the active sheet - this may need to change if iterating through multiple sheets
                // Heuristics!!!
                oWorksheet = oWorkbook.ActiveSheet;
                Debug.WriteLine("Processing file...");

                #region Heuristics - Initialisation

                // Step 1: Get/Create an instance of the classifier
                Heuristics.Classifier heuristics_classifier = Heuristics.Classifier.GetInstance();

                // Step 2: Load a training file containing columns with the frequency values
                // For this to work, ensure that the Build Action is set to 'Embedded Resource'
                var assembly = Assembly.GetExecutingAssembly();
                var trainingFile = "GPC_BOM.Resources.training_combined.txt";
                string line;
                List<double> designator_training = new List<double>();
                List<double> manufacturer_training = new List<double>();
                List<double> mpn_training = new List<double>();
                List<double> qty_training = new List<double>();
                List<double> description_training = new List<double>();

                using (Stream stream = assembly.GetManifestResourceStream(trainingFile)) {
                    using (StreamReader reader = new StreamReader(stream)) {
                        while ((line = reader.ReadLine()) != null) {
                            // Debug.WriteLine(line);
                            var elements = line.Split(new[] {','}, System.StringSplitOptions.RemoveEmptyEntries);
                            int count = 0;
                            string trainingType = "";

                            foreach (string item in elements) {
                                if (count == 0) {
                                    // First item tell us which column the data is for
                                    trainingType = item.ToString();
                                    count = count + 1;
                                    continue;
                                }
                                count = count + 1;
                                switch (trainingType) {
                                    case "designator":
                                        designator_training.Add(Convert.ToDouble(item));
                                        break;
                                    case "manufacturer":
                                        manufacturer_training.Add(Convert.ToDouble(item));
                                        break;
                                    case "mpn":
                                        mpn_training.Add(Convert.ToDouble(item));
                                        break;
                                    case "qty":
                                        qty_training.Add(Convert.ToDouble(item));
                                        break;
                                    case "description":
                                        description_training.Add(Convert.ToDouble(item));
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }

                // Step 3: Add Quantity classifier values
                heuristics_classifier.AddFrequencyValue(Heuristics.Classifier.ColumnType.Quantity, qty_training);
                // Step 4: Add Designator classifier values
                heuristics_classifier.AddFrequencyValue(Heuristics.Classifier.ColumnType.Designator, designator_training);
                // Step 5: Add Description classifier values
                heuristics_classifier.AddFrequencyValue(Heuristics.Classifier.ColumnType.Description, description_training);
                // Step 6: Add Manufacturer classifier values
                heuristics_classifier.AddFrequencyValue(Heuristics.Classifier.ColumnType.Manufacturer, manufacturer_training);
                // Step 7: Add MPN classifier values
                heuristics_classifier.AddFrequencyValue(Heuristics.Classifier.ColumnType.PartNumber, mpn_training);

                #endregion

                mainApp.pbUpdate(20, frmMain.pbModeIncrement);

                // Do a basic search first to fill in the non-heuristics row/col numbers
                #region Basic Search - Find column headers
                
                mainApp.statusUpdate2("(Searching for column headers)");
                Excel.Range searchRange = oExcel.get_Range(headerRow.ToString() + ":" + headerRow.ToString(), Type.Missing);

                // Find Level
                foreach (string item in Properties.Settings.Default.cLevel) {
                    Excel.Range locationFound = searchRange.Find(item);
                    if (locationFound != null) {
                        // Once we get a match, break out of loop
                        level_column = locationFound.Column;
                        // Only get the data
                        level_row = locationFound.Row + 1;
                        break;
                    }
                }

                // Find CPN
                foreach (string item in Properties.Settings.Default.cCPN) {
                    Excel.Range locationFound = searchRange.Find(item);
                    if (locationFound != null) {
                        // Once we get a match, break out of loop
                        cpn_column = locationFound.Column;
                        cpn_row = locationFound.Row + 1;
                    }
                }

                // Find Description
                foreach (string item in Properties.Settings.Default.cDescription) {
                    Excel.Range locationFound = searchRange.Find(item);
                    if (locationFound != null) {
                        // Once we get a match, break out of loop
                        description_column = locationFound.Column;
                        description_row = locationFound.Row + 1;
                        break;
                    }
                }

                // Find Quantity
                foreach (string item in Properties.Settings.Default.cQuantity) {
                    Excel.Range locationFound = searchRange.Find(item);
                    if (locationFound != null) {
                        // Once we get a match, break out of loop
                        quantity_column = locationFound.Column;
                        quantity_row = locationFound.Row + 1;
                        break;
                    }
                }

                // Find Designator
                foreach (string item in Properties.Settings.Default.cDesignator) {
                    Excel.Range locationFound = searchRange.Find(item);
                    if (locationFound != null) {
                        // Once we get a match, break out of loop
                        designator_column = locationFound.Column;
                        designator_row = locationFound.Row + 1;
                        break;
                    }
                }

                // Find Manufacturer
                foreach (string item in Properties.Settings.Default.cManufacturer) {
                    Excel.Range locationFound = searchRange.Find(item);
                    if (locationFound != null) {
                        // Once we get a match, break out of loop
                        manufacturer_column = locationFound.Column;
                        manufacturer_row = locationFound.Row + 1;
                        break;
                    }
                }

                // Find MPN
                foreach (string item in Properties.Settings.Default.cMPN) {
                    Excel.Range locationFound = searchRange.Find(item);
                    if (locationFound != null) {
                        // Once we get a match, break out of loop
                        mpn_column = locationFound.Column;
                        mpn_row = locationFound.Row + 1;
                        break;
                    }
                }

                // Find Process
                foreach (string item in Properties.Settings.Default.cProcess) {
                    Excel.Range locationFound = searchRange.Find(item);
                    if (locationFound != null) {
                        // Once we get a match, break out of loop
                        process_column = locationFound.Column;
                        process_row = locationFound.Row + 1;
                        break;
                    }
                }

                // Find Notes
                foreach (string item in Properties.Settings.Default.cNotes) {
                    Excel.Range locationFound = searchRange.Find(item);
                    if (locationFound != null) {
                        // Once we get a match, break out of loop
                        notes_column = locationFound.Column;
                        notes_row = locationFound.Row + 1;
                        break;
                    }
                }

                #endregion
                // Overwrite col numbers with heuristics
                #region Heuristics - Find Column Headers

                mainApp.statusUpdate2("(Searching for column headers intelligently)");

                // Use these to store the raw lowest heuristic values
                double? quantityHeuristic = null;
                double? descriptionHeuristic = null;
                double? manufacturerHeuristic = null;
                double? mpnHeuristic = null;
                double? designatorHeuristic = null;

                try {
                    for (int i = 1; i < oWorksheet.UsedRange.Columns.Count + 1; i++) {
                        // Copy every column sequentially into a temporary array until we find everything we're looking for
                        Excel.Range temp_start = oWorksheet.Cells[1, i];
                        Excel.Range temp_end = oWorksheet.Cells[oWorksheet.UsedRange.Rows.Count, i];
                        Excel.Range temp_range = (Excel.Range)oWorksheet.get_Range(temp_start, temp_end);
                        System.Array genArray = (System.Array)temp_range.Cells.Value2;
                        string[] tempArray = genArray.OfType<object>().Select(o => o.ToString()).ToArray();

                        // Classify the current column
                        Dictionary<Heuristics.Classifier.ColumnType, double> heuristicsColumnType = heuristics_classifier.Classify(tempArray);

                        // Determine whether a column belongs to a specific type
                        switch (heuristicsColumnType.Min().Key) {
                            case Heuristics.Classifier.ColumnType.Quantity:
                                if (!quantityHeuristic.HasValue) {
                                    // First time a column has been predicted to be 'quantity'
                                    Debug.WriteLine(heuristicsColumnType.Min().Value.ToString());
                                    quantityHeuristic = Convert.ToDouble(heuristicsColumnType.Min().Value.ToString());
                                    quantity_column = i;
                                }
                                else if (heuristicsColumnType.Min().Value < quantityHeuristic) {
                                    // Not the first time, but this one seems more likely
                                    quantityHeuristic = heuristicsColumnType.Min().Value;
                                    quantity_column = i;
                                }
                                if (!quantity_row.HasValue) {
                                    // If the row hasn't already been set, use a default
                                    quantity_row = 2;
                                }
                                break;
                            case Heuristics.Classifier.ColumnType.Description:
                                if (!descriptionHeuristic.HasValue) {
                                    // First time a column has been predicted to be 'description'
                                    descriptionHeuristic = (double)heuristicsColumnType.Min().Value;
                                    description_column = i;
                                }
                                else if (heuristicsColumnType.Min().Value < descriptionHeuristic) {
                                    // Not the first time, but this one seems more likely
                                    descriptionHeuristic = heuristicsColumnType.Min().Value;
                                    description_column = i;
                                }
                                if (!description_row.HasValue) {
                                    // If the row hasn't already been set, use a default
                                    description_row = 2;
                                }
                                break;
                            case Heuristics.Classifier.ColumnType.PartNumber:
                                if (!mpnHeuristic.HasValue) {
                                    // First time a column has been predicted to be 'mpn'
                                    mpnHeuristic = (double)heuristicsColumnType.Min().Value;
                                    mpn_column = i;
                                }
                                else if (heuristicsColumnType.Min().Value < mpnHeuristic) {
                                    // Not the first time, but this one seems more likely
                                    mpnHeuristic = heuristicsColumnType.Min().Value;
                                    mpn_column = i;
                                }
                                if (!mpn_row.HasValue) {
                                    // If the row hasn't already been set, use a default
                                    mpn_row = 2;
                                }
                                break;
                            case Heuristics.Classifier.ColumnType.Designator:
                                if (!designatorHeuristic.HasValue) {
                                    // First time a column has been predicted to be 'designator'
                                    designatorHeuristic = (double)heuristicsColumnType.Min().Value;
                                    designator_column = i;
                                }
                                else if (heuristicsColumnType.Min().Value < designatorHeuristic) {
                                    // Not the first time, but this one seems more likely
                                    designatorHeuristic = heuristicsColumnType.Min().Value;
                                    designator_column = i;
                                }
                                if (!designator_row.HasValue) {
                                    // If the row hasn't already been set, use a default
                                    designator_row = 2;
                                }
                                break;
                            case Heuristics.Classifier.ColumnType.Manufacturer:
                                if (!manufacturerHeuristic.HasValue) {
                                    // First time a column has been predicted to be 'manufacturer'
                                    manufacturerHeuristic = (double)heuristicsColumnType.Min().Value;
                                    manufacturer_column = i;
                                }
                                else if (heuristicsColumnType.Min().Value < manufacturerHeuristic) {
                                    // Not the first time, but this one seems more likely
                                    manufacturerHeuristic = heuristicsColumnType.Min().Value;
                                    manufacturer_column = i;
                                }
                                if (!manufacturer_row.HasValue) {
                                    // If the row hasn't already been set, use a default
                                    manufacturer_row = 2;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception) {
                }
                finally {
                    Debug.WriteLine("Quantity Heuristic and column:");
                    Debug.WriteLine(quantityHeuristic);
                    Debug.WriteLine(quantity_column);
                    Debug.WriteLine("Description Heuristic and column:");
                    Debug.WriteLine(descriptionHeuristic);
                    Debug.WriteLine(description_column);
                    Debug.WriteLine("MPN Heuristic and column:");
                    Debug.WriteLine(mpnHeuristic);
                    Debug.WriteLine(mpn_column);
                    Debug.WriteLine("Designator Heuristic and column:");
                    Debug.WriteLine(designatorHeuristic);
                    Debug.WriteLine(designator_column);
                    Debug.WriteLine("Manufacturer Heuristic and column:");
                    Debug.WriteLine(manufacturerHeuristic);
                    Debug.WriteLine(manufacturer_column);
                }

                #endregion

                mainApp.pbUpdate(20, frmMain.pbModeIncrement);

                #region Extract column data

                mainApp.statusUpdate2("(Extracting data from original file)");
                // Get Level
                System.Array levelData = null;
                if (level_column == (int?)null || level_row == (int?)null) {
                    Debug.WriteLine("No Level column found.");
                }
                else {
                    Excel.Range startCell = oWorksheet.Cells[level_row, level_column];
                    Excel.Range endCell = oWorksheet.Cells[oWorksheet.UsedRange.Rows.Count, level_column];
                    Excel.Range range = (Excel.Range)oWorksheet.get_Range(startCell, endCell);
                    levelData = (System.Array)range.Cells.Value2;
                }

                // Get CPN
                System.Array cpnData = null;
                if (cpn_column == (int?)null || cpn_row == (int?)null) {
                    Debug.WriteLine("No CPN column found.");
                }
                else {
                    Excel.Range startCell = oWorksheet.Cells[cpn_row, cpn_column];
                    Excel.Range endCell = oWorksheet.Cells[oWorksheet.UsedRange.Rows.Count, cpn_column];
                    Excel.Range range = (Excel.Range)oWorksheet.get_Range(startCell, endCell);
                    cpnData = (System.Array)range.Cells.Value2;
                }

                // Get Description
                System.Array descriptionData = null;
                if (description_column == (int?)null || description_row == (int?)null) {
                    Debug.WriteLine("No Description column found.");
                }
                else {
                    Excel.Range startCell = oWorksheet.Cells[description_row, description_column];
                    Excel.Range endCell = oWorksheet.Cells[oWorksheet.UsedRange.Rows.Count, description_column];
                    Excel.Range range = (Excel.Range)oWorksheet.get_Range(startCell, endCell);
                    descriptionData = (System.Array)range.Cells.Value2;
                }

                // Get Quantity
                System.Array quantityData = null;
                if (quantity_column == (int?)null || quantity_row == (int?)null) {
                    Debug.WriteLine("No Quantity column found.");
                }
                else {
                    Excel.Range startCell = oWorksheet.Cells[quantity_row, quantity_column];
                    Excel.Range endCell = oWorksheet.Cells[oWorksheet.UsedRange.Rows.Count, quantity_column];
                    Excel.Range range = (Excel.Range)oWorksheet.get_Range(startCell, endCell);
                    quantityData = (System.Array)range.Cells.Value2;
                }

                // Get Designator
                System.Array designatorData = null;
                if (designator_column == (int?)null || designator_row == (int?)null) {
                    Debug.WriteLine("No Designator column found.");
                }
                else {
                    Excel.Range startCell = oWorksheet.Cells[designator_row, designator_column];
                    Excel.Range endCell = oWorksheet.Cells[oWorksheet.UsedRange.Rows.Count, designator_column];
                    Excel.Range range = (Excel.Range)oWorksheet.get_Range(startCell, endCell);
                    designatorData = (System.Array)range.Cells.Value2;
                }

                // Get Manufacturer
                System.Array manufacturerData = null;
                if (manufacturer_column == (int?)null || manufacturer_row == (int?)null) {
                    Debug.WriteLine("No Manufacturer column found.");
                }
                else {
                    Excel.Range startCell = oWorksheet.Cells[manufacturer_row, manufacturer_column];
                    Excel.Range endCell = oWorksheet.Cells[oWorksheet.UsedRange.Rows.Count, manufacturer_column];
                    Excel.Range range = (Excel.Range)oWorksheet.get_Range(startCell, endCell);
                    manufacturerData = (System.Array)range.Cells.Value2;
                }

                // Get MPN
                System.Array mpnData = null;
                if (mpn_column == (int?)null || mpn_row == (int?)null) {
                    Debug.WriteLine("No MPN column found.");
                }
                else {
                    Excel.Range startCell = oWorksheet.Cells[mpn_row, mpn_column];
                    Excel.Range endCell = oWorksheet.Cells[oWorksheet.UsedRange.Rows.Count, mpn_column];
                    Excel.Range range = (Excel.Range)oWorksheet.get_Range(startCell, endCell);
                    mpnData = (System.Array)range.Cells.Value2;
                }

                // Get Process
                System.Array processData = null;
                if (process_column == (int?)null || process_row == (int?)null) {
                    Debug.WriteLine("No Process column found.");
                }
                else {
                    Excel.Range startCell = oWorksheet.Cells[process_row, process_column];
                    Excel.Range endCell = oWorksheet.Cells[oWorksheet.UsedRange.Rows.Count, process_column];
                    Excel.Range range = (Excel.Range)oWorksheet.get_Range(startCell, endCell);
                    processData = (System.Array)range.Cells.Value2;
                }

                // Get Notes
                System.Array notesData = null;
                if (notes_column == (int?)null || notes_row == (int?)null) {
                    Debug.WriteLine("No Notes column found.");
                }
                else {
                    Excel.Range startCell = oWorksheet.Cells[notes_row, notes_column];
                    Excel.Range endCell = oWorksheet.Cells[oWorksheet.UsedRange.Rows.Count, notes_column];
                    Excel.Range range = (Excel.Range)oWorksheet.get_Range(startCell, endCell);
                    notesData = (System.Array)range.Cells.Value2;
                }

                #endregion

                mainApp.pbUpdate(20, frmMain.pbModeIncrement);

                // Copy template from resources
                mainApp.statusUpdate2("(Creating and opening output file)");
                if (fileType.Equals("quotewin_single")) {
                    File.WriteAllBytes(@outFile, Properties.Resources.Quotewin_single);
                } else if (fileType.Equals("quotewin_multi")) {
                    File.WriteAllBytes(@outFile, Properties.Resources.Quotewin_multi);
                }
                else if (fileType.Equals("sap")) {
                    File.WriteAllBytes(@outFile, Properties.Resources.SAP);
                }

                // Open output Excel workbook
                oWorkbook2 = oExcel.Workbooks.Open(outFile);
                oWorksheet2 = oWorkbook2.ActiveSheet;

                #region Write data to output file

                mainApp.statusUpdate2("(Writing data to output file)");
                // Write Level column
                if (levelData != null) {
                    int rowCount = levelData.GetLength(0);
                    int columnCount = levelData.GetLength(1);
                    Excel.Range dataRange = null;
                    // Quotewin_single does not have level
                    if (fileType.Equals("quotewin_multi")) {
                        dataRange = oWorksheet2.Cells[level_quotewin_multi_row, level_quotewin_multi_column];
                    }
                    else if (fileType.Equals("sap")) {
                        dataRange = oWorksheet2.Cells[level_sap_row, level_sap_column];
                    }
                    if (dataRange != null) {
                        // Null check in case of quotewin_single
                        dataRange = dataRange.get_Resize(rowCount, columnCount);
                        dataRange.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, levelData);
                    }
                }
                else {
                    // We need to mark any missing data
                    if (fileType.Equals("quotewin_multi")) {
                        writeCell(oWorksheet2, level_quotewin_multi_row, level_quotewin_multi_column, dataMissingMessage);
                    }
                    else if (fileType.Equals("sap")) {
                        writeCell(oWorksheet2, level_sap_row, level_sap_column, dataMissingMessage);
                        // Highlighting is only possible with the Excel file format, not CSV
                        highlightCell(oWorksheet2, level_sap_row, level_sap_column, Excel.XlRgbColor.rgbYellow);
                    }
                }

                // Write CPN column
                if (cpnData != null) {
                    int rowCount = cpnData.GetLength(0);
                    int columnCount = cpnData.GetLength(1);
                    Excel.Range dataRange = null;
                    if (fileType.Equals("quotewin_single")) {
                        dataRange = oWorksheet2.Cells[cpn_quotewin_single_row, cpn_quotewin_single_column];
                    }
                    else if (fileType.Equals("quotewin_multi")) {
                        dataRange = oWorksheet2.Cells[cpn_quotewin_multi_row, cpn_quotewin_multi_column];
                    }
                    else if (fileType.Equals("sap")) {
                        dataRange = oWorksheet2.Cells[cpn_sap_row, cpn_sap_column];
                    }
                    dataRange = dataRange.get_Resize(rowCount, columnCount);
                    dataRange.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, cpnData);
                }
                else {
                    // We need to mark any missing data
                    if (fileType.Equals("quotewin_single")) {
                        writeCell(oWorksheet2, cpn_quotewin_single_row, cpn_quotewin_single_column, dataMissingMessage);
                    }
                    else if (fileType.Equals("quotewin_multi")) {
                        writeCell(oWorksheet2, cpn_quotewin_multi_row, cpn_quotewin_multi_column, dataMissingMessage);
                    }
                    else if (fileType.Equals("sap")) {
                        writeCell(oWorksheet2, cpn_sap_row, cpn_sap_column, dataMissingMessage);
                        // Highlighting is only possible with the Excel file format, not CSV
                        highlightCell(oWorksheet2, cpn_sap_row, cpn_sap_column, Excel.XlRgbColor.rgbYellow);
                    }
                }

                // Write Description column
                if (descriptionData != null) {
                    int rowCount = descriptionData.GetLength(0);
                    int columnCount = descriptionData.GetLength(1);
                    Excel.Range dataRange = null;
                    if (fileType.Equals("quotewin_single")) {
                        dataRange = oWorksheet2.Cells[description_quotewin_single_row, description_quotewin_single_column];
                    }
                    else if (fileType.Equals("quotewin_multi")) {
                        dataRange = oWorksheet2.Cells[description_quotewin_multi_row, description_quotewin_multi_column];
                    }
                    else if (fileType.Equals("sap")) {
                        dataRange = oWorksheet2.Cells[description_sap_row, description_sap_column];
                    }
                    dataRange = dataRange.get_Resize(rowCount, columnCount);
                    dataRange.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, descriptionData);
                }
                else {
                    // We need to mark any missing data
                    if (fileType.Equals("quotewin_single")) {
                        writeCell(oWorksheet2, description_quotewin_single_row, description_quotewin_single_column, dataMissingMessage);
                    }
                    else if (fileType.Equals("quotewin_multi")) {
                        writeCell(oWorksheet2, description_quotewin_multi_row, description_quotewin_multi_column, dataMissingMessage);
                    }
                    else if (fileType.Equals("sap")) {
                        writeCell(oWorksheet2, description_sap_row, description_sap_column, dataMissingMessage);
                        // Highlighting is only possible with the Excel file format, not CSV
                        highlightCell(oWorksheet2, description_sap_row, description_sap_column, Excel.XlRgbColor.rgbYellow);
                    }
                }

                // Write Quantity column
                if (quantityData != null) {
                    int rowCount = quantityData.GetLength(0);
                    int columnCount = quantityData.GetLength(1);
                    Excel.Range dataRange = null;
                    if (fileType.Equals("quotewin_single")) {
                        dataRange = oWorksheet2.Cells[quantity_quotewin_single_row, quantity_quotewin_single_column];
                    }
                    else if (fileType.Equals("quotewin_multi")) {
                        dataRange = oWorksheet2.Cells[quantity_quotewin_multi_row, quantity_quotewin_multi_column];
                    }
                    else if (fileType.Equals("sap")) {
                        dataRange = oWorksheet2.Cells[quantity_sap_row, quantity_sap_column];
                    }
                    dataRange = dataRange.get_Resize(rowCount, columnCount);
                    dataRange.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, quantityData);
                }
                else {
                    // We need to mark any missing data
                    if (fileType.Equals("quotewin_single")) {
                        writeCell(oWorksheet2, quantity_quotewin_single_row, quantity_quotewin_single_column, dataMissingMessage);
                    }
                    else if (fileType.Equals("quotewin_multi")) {
                        writeCell(oWorksheet2, quantity_quotewin_multi_row, quantity_quotewin_multi_column, dataMissingMessage);
                    }
                    else if (fileType.Equals("sap")) {
                        writeCell(oWorksheet2, quantity_sap_row, quantity_sap_column, dataMissingMessage);
                        // Highlighting is only possible with the Excel file format, not CSV
                        highlightCell(oWorksheet2, quantity_sap_row, quantity_sap_column, Excel.XlRgbColor.rgbYellow);
                    }
                }

                // Write Designator column
                if (designatorData != null) {
                    int rowCount = designatorData.GetLength(0);
                    int columnCount = designatorData.GetLength(1);
                    Excel.Range dataRange = null;
                    if (fileType.Equals("quotewin_single")) {
                        dataRange = oWorksheet2.Cells[designator_quotewin_single_row, designator_quotewin_single_column];
                    }
                    else if (fileType.Equals("quotewin_multi")) {
                        dataRange = oWorksheet2.Cells[designator_quotewin_multi_row, designator_quotewin_multi_column];
                    }
                    else if (fileType.Equals("sap")) {
                        dataRange = oWorksheet2.Cells[designator_sap_row, designator_sap_column];
                    }
                    dataRange = dataRange.get_Resize(rowCount, columnCount);
                    dataRange.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, designatorData);
                }
                else {
                    // We need to mark any missing data
                    if (fileType.Equals("quotewin_single")) {
                        writeCell(oWorksheet2, designator_quotewin_single_row, designator_quotewin_single_column, dataMissingMessage);
                    }
                    else if (fileType.Equals("quotewin_multi")) {
                        writeCell(oWorksheet2, designator_quotewin_multi_row, designator_quotewin_multi_column, dataMissingMessage);
                    }
                    else if (fileType.Equals("sap")) {
                        writeCell(oWorksheet2, designator_sap_row, designator_sap_column, dataMissingMessage);
                        // Highlighting is only possible with the Excel file format, not CSV
                        highlightCell(oWorksheet2, designator_sap_row, designator_sap_column, Excel.XlRgbColor.rgbYellow);
                    }
                }

                // Write Manufacturer column
                if (manufacturerData != null) {
                    int rowCount = manufacturerData.GetLength(0);
                    int columnCount = manufacturerData.GetLength(1);
                    Excel.Range dataRange = null;
                    if (fileType.Equals("quotewin_single")) {
                        dataRange = oWorksheet2.Cells[manufacturer_quotewin_single_row, manufacturer_quotewin_single_column];
                    }
                    else if (fileType.Equals("quotewin_multi")) {
                        dataRange = oWorksheet2.Cells[manufacturer_quotewin_multi_row, manufacturer_quotewin_multi_column];
                    }
                    else if (fileType.Equals("sap")) {
                        dataRange = oWorksheet2.Cells[manufacturer_sap_row, manufacturer_sap_column];
                    }
                    dataRange = dataRange.get_Resize(rowCount, columnCount);
                    dataRange.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, manufacturerData);
                }
                else {
                    // We need to mark any missing data
                    if (fileType.Equals("quotewin_single")) {
                        writeCell(oWorksheet2, manufacturer_quotewin_single_row, manufacturer_quotewin_single_column, dataMissingMessage);
                    }
                    else if (fileType.Equals("quotewin_multi")) {
                        writeCell(oWorksheet2, manufacturer_quotewin_multi_row, manufacturer_quotewin_multi_column, dataMissingMessage);
                    }
                    else if (fileType.Equals("sap")) {
                        writeCell(oWorksheet2, manufacturer_sap_row, manufacturer_sap_column, dataMissingMessage);
                        // Highlighting is only possible with the Excel file format, not CSV
                        highlightCell(oWorksheet2, manufacturer_sap_row, manufacturer_sap_column, Excel.XlRgbColor.rgbYellow);
                    }
                }

                // Write MPN column
                if (mpnData != null) {
                    int rowCount = mpnData.GetLength(0);
                    int columnCount = mpnData.GetLength(1);
                    Excel.Range dataRange = null;
                    if (fileType.Equals("quotewin_single")) {
                        dataRange = oWorksheet2.Cells[mpn_quotewin_single_row, mpn_quotewin_single_column];
                    }
                    else if (fileType.Equals("quotewin_multi")) {
                        dataRange = oWorksheet2.Cells[mpn_quotewin_multi_row, mpn_quotewin_multi_column];
                    }
                    else if (fileType.Equals("sap")) {
                        dataRange = oWorksheet2.Cells[mpn_sap_row, mpn_sap_column];
                    }
                    dataRange = dataRange.get_Resize(rowCount, columnCount);
                    dataRange.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, mpnData);
                }
                else {
                    // We need to mark any missing data
                    if (fileType.Equals("quotewin_single")) {
                        writeCell(oWorksheet2, mpn_quotewin_single_row, mpn_quotewin_single_column, dataMissingMessage);
                    }
                    else if (fileType.Equals("quotewin_multi")) {
                        writeCell(oWorksheet2, mpn_quotewin_multi_row, mpn_quotewin_multi_column, dataMissingMessage);
                    }
                    else if (fileType.Equals("sap")) {
                        writeCell(oWorksheet2, mpn_sap_row, mpn_sap_column, dataMissingMessage);
                        // Highlighting is only possible with the Excel file format, not CSV
                        highlightCell(oWorksheet2, mpn_sap_row, mpn_sap_column, Excel.XlRgbColor.rgbYellow);
                    }
                }

                // Write Process column
                if (processData != null) {
                    int rowCount = processData.GetLength(0);
                    int columnCount = processData.GetLength(1);
                    Excel.Range dataRange = null;
                    if (fileType.Equals("sap")) {
                        dataRange = oWorksheet2.Cells[process_sap_row, process_sap_column];
                    }
                    if (dataRange != null) {
                        dataRange = dataRange.get_Resize(rowCount, columnCount);
                        dataRange.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, processData);
                    }
                }
                else {
                    // We need to mark any missing data
                    if (fileType.Equals("sap")) {
                        writeCell(oWorksheet2, process_sap_row, process_sap_column, dataMissingMessage);
                        // Highlighting is only possible with the Excel file format, not CSV
                        highlightCell(oWorksheet2, process_sap_row, process_sap_column, Excel.XlRgbColor.rgbYellow);
                    }
                }

                // Write Notes column
                if (notesData != null) {
                    int rowCount = notesData.GetLength(0);
                    int columnCount = notesData.GetLength(1);
                    Excel.Range dataRange = null;
                    if (fileType.Equals("quotewin_single") ) {
                        dataRange = oWorksheet2.Cells[notes_quotewin_single_row, notes_quotewin_single_column];
                    }
                    else if (fileType.Equals("quotewin_multi")) {
                        dataRange = oWorksheet2.Cells[notes_quotewin_multi_row, notes_quotewin_multi_column];
                    }
                    else if (fileType.Equals("sap")) {
                        dataRange = oWorksheet2.Cells[notes_sap_row, notes_sap_column];
                    }
                    dataRange = dataRange.get_Resize(rowCount, columnCount);
                    dataRange.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, notesData);
                }
                else {
                    // We need to mark any missing data
                    if (fileType.Equals("quotewin_single")) {
                        writeCell(oWorksheet2, notes_quotewin_single_row, notes_quotewin_single_column, dataMissingMessage);
                    }
                    else if (fileType.Equals("quotewin_multi")) {
                        writeCell(oWorksheet2, notes_quotewin_multi_row, notes_quotewin_multi_column, dataMissingMessage);
                    }
                    else if (fileType.Equals("sap")) {
                        writeCell(oWorksheet2, notes_sap_row, notes_sap_column, dataMissingMessage);
                        // Highlighting is only possible with the Excel file format, not CSV
                        highlightCell(oWorksheet2, notes_sap_row, notes_sap_column, Excel.XlRgbColor.rgbYellow);
                    }
                }

                #endregion

                Debug.WriteLine("Data copied.");
                mainApp.pbUpdate(20, frmMain.pbModeIncrement);

                #region Web Scraping

                mainApp.statusUpdate2("(Checking for missing fields and searching the web)");
                // At the time of writing, only missing descriptions can be retrieved
                // MPN is usually the only unique identifier that can be searched
                if (fileType.Equals("quotewin_single")) {
                    // Iterate through every row of the sheet
                    for (int i = 2; i <= oWorksheet2.UsedRange.Rows.Count; i++) {
                        // Check if description is filled or not
                        string descriptionValue = Convert.ToString(oWorksheet2.Cells[i, description_quotewin_single_column].Value2);
                        if (string.IsNullOrEmpty(descriptionValue) || string.IsNullOrWhiteSpace(descriptionValue)) {
                            // Check if MPN is available
                            string mpnValue = Convert.ToString(oWorksheet2.Cells[i, mpn_quotewin_single_column].Value2);
                            if (string.IsNullOrEmpty(mpnValue) || string.IsNullOrWhiteSpace(mpnValue)) {
                                // Can't do anything if MPN is missing as well
                                writeCell(oWorksheet2, i, description_quotewin_single_column, dataMissingMessage);
                                writeCell(oWorksheet2, i, mpn_quotewin_single_column, dataMissingMessage);
                                continue;
                            }
                            else {
                                // Web scraping time!
                                string result = tryWebScrape(mpnValue);
                                if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result)) {
                                    // Couldn't find anything via web search
                                    writeCell(oWorksheet2, i, description_quotewin_single_column, dataMissingMessage);
                                    continue;
                                }
                                else {
                                    // Write data to cell
                                    writeCell(oWorksheet2, i, description_quotewin_single_column, result);
                                }
                            }
                        }
                        else {
                            // Next loop if cell is already filled
                            continue;
                        }
                    }
                }
                else if (fileType.Equals("quotewin_multi")) {
                    // Iterate through every row of the sheet
                    for (int i = 2; i <= oWorksheet2.UsedRange.Rows.Count; i++) {
                        // Check if description is filled or not
                        string descriptionValue = Convert.ToString(oWorksheet2.Cells[i, description_quotewin_multi_column].Value2);
                        if (string.IsNullOrEmpty(descriptionValue) || string.IsNullOrWhiteSpace(descriptionValue)) {
                            // Check if MPN is available
                            string mpnValue = Convert.ToString(oWorksheet2.Cells[i, mpn_quotewin_multi_column].Value2);
                            if (string.IsNullOrEmpty(mpnValue) || string.IsNullOrWhiteSpace(mpnValue)) {
                                // Can't do anything if MPN is missing as well
                                writeCell(oWorksheet2, i, description_quotewin_multi_column, dataMissingMessage);
                                writeCell(oWorksheet2, i, mpn_quotewin_multi_column, dataMissingMessage);
                                continue;
                            }
                            else {
                                // Web scraping time!
                                string result = tryWebScrape(mpnValue);
                                if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result)) {
                                    // Couldn't find anything via web search
                                    writeCell(oWorksheet2, i, description_quotewin_multi_column, dataMissingMessage);
                                    continue;
                                }
                                else {
                                    // Write data to cell
                                    writeCell(oWorksheet2, i, description_quotewin_multi_column, result);
                                }
                            }
                        }
                        else {
                            // Next loop if cell is already filled
                            continue;
                        }
                    }
                }
                else if (fileType.Equals("sap")) {
                    // Iterate through every row of the sheet
                    for (int i = 5; i <= oWorksheet2.UsedRange.Rows.Count; i++) {
                        // Check if description is filled or not
                        string descriptionValue = Convert.ToString(oWorksheet2.Cells[i, description_sap_column].Value2);
                        if (string.IsNullOrEmpty(descriptionValue) || string.IsNullOrWhiteSpace(descriptionValue)) {
                            // Check if MPN is available
                            string mpnValue = Convert.ToString(oWorksheet2.Cells[i, mpn_sap_column].Value2);
                            if (string.IsNullOrEmpty(mpnValue) || string.IsNullOrWhiteSpace(mpnValue)) {
                                // Can't do anything if MPN is missing as well
                                writeCell(oWorksheet2, i, description_sap_column, dataMissingMessage);
                                highlightCell(oWorksheet2, i, description_sap_column, Excel.XlRgbColor.rgbYellow);
                                writeCell(oWorksheet2, i, mpn_sap_column, dataMissingMessage);
                                highlightCell(oWorksheet2, i, mpn_sap_column, Excel.XlRgbColor.rgbYellow);
                                continue;
                            }
                            else {
                                // Web scraping time!
                                string result = tryWebScrape(mpnValue);
                                if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result)) {
                                    // Couldn't find anything via web search
                                    writeCell(oWorksheet2, i, description_sap_column, dataMissingMessage);
                                    highlightCell(oWorksheet2, i, description_sap_column, Excel.XlRgbColor.rgbYellow);
                                    continue;
                                }
                                else {
                                    // Write data to cell and use different highlight colour
                                    writeCell(oWorksheet2, i, description_sap_column, result);
                                    highlightCell(oWorksheet2, i, description_sap_column, Excel.XlRgbColor.rgbAqua);
                                }
                            }
                        }
                        else {
                            // Next loop if cell is already filled
                            continue;
                        }
                    }
                }

                #endregion

                Debug.WriteLine("Attempting to save file.");
                mainApp.statusUpdate2("(Saving the output file)");
                mainApp.pbUpdate(20, frmMain.pbModeIncrement);
                // Save the Excel workbook (should probably be careful about overwriting stuff if it exists)
                if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                    // Save as Windows CSV file
                    oWorkbook2.SaveAs(outFile, Excel.XlFileFormat.xlCSV, Type.Missing, Type.Missing, false, false, Excel.XlSaveAsAccessMode.xlNoChange, Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                }
                else if (fileType.Equals("sap")) {
                    // Save as Excel 97-2003 .xls file for backwards compatibility
                    oWorkbook2.SaveAs(outFile, Excel.XlFileFormat.xlExcel8, Type.Missing, Type.Missing, false, false, Excel.XlSaveAsAccessMode.xlNoChange, Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                }
                
                // Clean up and close apps
                // TODO: Need to test if this also closes user's other windows!!!
                Debug.WriteLine("Cleaning up...");
                mainApp.statusUpdate2("(Closing Excel)");
                oExcel.Workbooks.Close();
                oExcel.Quit();
            }
            catch (Exception ex) {
                Debug.WriteLine("Error:{0}", ex.Message);
            }
            finally {
                // Clean up the unmanaged Excel COM resources by explicitly calling Marshal.FinalReleaseComObject on all accessor objects.  
                if (oExcel != null) {
                    Marshal.FinalReleaseComObject(oExcel);
                    oExcel = null;
                }
                if (oWorkbook != null) {
                    Marshal.FinalReleaseComObject(oWorkbook);
                    oWorkbook = null;
                }
                if (oWorkbook2 != null) {
                    Marshal.FinalReleaseComObject(oWorkbook2);
                    oWorkbook2 = null;
                }
                if (oWorksheet != null) {
                    Marshal.FinalReleaseComObject(oWorksheet);
                    oWorksheet = null;
                }
                if (oWorksheet2 != null) {
                    Marshal.FinalReleaseComObject(oWorksheet2);
                    oWorksheet2 = null;
                }
            }
        }

        private void writeCell(Excel.Worksheet oWorksheet, int row, int column, string data) {
            oWorksheet.Cells[row, column].Value2 = data;
        }

        private void highlightCell(Excel.Worksheet oWorksheet, int row, int column, Excel.XlRgbColor colourValue) {
            oWorksheet.Cells[row, column].Interior.Color = colourValue;
        }

        public static bool CheckForInternetConnection() {
            // https://stackoverflow.com/a/2031831/2553699
            try {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204")) {
                    return true;
                }
            }
            catch {
                return false;
            }
        }

        private string tryWebScrape(string searchTerm) {
            // Blank string as default
            string result = "";

            // Check if we can connect to the Internet
            if (CheckForInternetConnection() == false) {
                MessageBox.Show("Tried to access Google to check Internet connection, but failed. Aborting web scrape operation.");
                return result;
            }

            // Retrieve current setting values for web scraping order
            List<string> webOrder = new List<string>();
            webOrder.Add(Properties.Settings.Default.webOrder1);
            webOrder.Add(Properties.Settings.Default.webOrder2);
            webOrder.Add(Properties.Settings.Default.webOrder3);
            webOrder.Add(Properties.Settings.Default.webOrder4);
            for (int i = 0; i < webOrder.Count; i++) {
                if (string.IsNullOrEmpty(result)) {
                    switch (webOrder[i]) {
                        case "NaiveDigikey":
                            result = NaiveDigikey(searchTerm);
                            break;
                        case "NaiveMouser":
                            result = NaiveMouser(searchTerm);
                            break;
                        case "NaiveElement14":
                            result = NaiveElement14(searchTerm);
                            break;
                        case "NaiveRS":
                            result = NaiveRS(searchTerm);
                            break;
                    }
                }
                else {
                    break;
                }
            }
            return result;
        }

        private string NaiveDigikey(string searchTerm) {
            string retVal = "";
            retVal = Webscraper.NaiveDigikey(searchTerm);
            return retVal;
        }
        private string NaiveMouser(string searchTerm) {
            string retVal = "";
            retVal = Webscraper.NaiveMouser(searchTerm);
            return retVal;
        }
        private string NaiveElement14(string searchTerm) {
            string retVal = "";
            retVal = Webscraper.NaiveElement14(searchTerm);
            return retVal;
        }
        private string NaiveRS(string searchTerm) {
            string retVal = "";
            retVal = Webscraper.NaiveRS(searchTerm);
            return retVal;
        }
    }
}
