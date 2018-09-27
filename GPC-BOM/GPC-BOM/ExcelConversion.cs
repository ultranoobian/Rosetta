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
        public const int level_quotewin_column = 1;
        public const int level_quotewin_row = 2;

        public static int? cpn_column = null;
        public static int? cpn_row = null;
        public const int cpn_sap_column = 2;
        public const int cpn_sap_row = 5;
        public const int cpn_quotewin_column = 2;
        public const int cpn_quotewin_row = 2;

        public static int? description_column = null;
        public static int? description_row = null;
        public const int description_sap_column = 3;
        public const int description_sap_row = 5;
        public const int description_quotewin_column = 7;
        public const int description_quotewin_row = 2;

        public static int? quantity_column = null;
        public static int? quantity_row = null;
        public const int quantity_sap_column = 4;
        public const int quantity_sap_row = 5;
        public const int quantity_quotewin_column = 5;
        public const int quantity_quotewin_row = 2;

        public static int? designator_column = null;
        public static int? designator_row = null;
        public const int designator_sap_column = 5;
        public const int designator_sap_row = 5;
        public const int designator_quotewin_column = 6;
        public const int designator_quotewin_row = 2;

        public static int? manufacturer_column = null;
        public static int? manufacturer_row = null;
        public const int manufacturer_sap_column = 6;
        public const int manufacturer_sap_row = 5;
        public const int manufacturer_quotewin_column = 30;
        public const int manufacturer_quotewin_row = 2;

        public static int? mpn_column = null;
        public static int? mpn_row = null;
        public const int mpn_sap_column = 7;
        public const int mpn_sap_row = 5;
        public const int mpn_quotewin_column = 18;
        public const int mpn_quotewin_row = 2;

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
        public const int notes_quotewin_column = 24;
        public const int notes_quotewin_row = 2;

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
                mainApp.pbUpdate(20, frmMain.pbModeIncrement);

                #region Find column headers

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
                if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                    File.WriteAllBytes(@outFile, Properties.Resources.Quotewin);
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
                    if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                        dataRange = oWorksheet2.Cells[level_quotewin_row, level_quotewin_column];
                    }
                    else if (fileType.Equals("sap")) {
                        dataRange = oWorksheet2.Cells[level_sap_row, level_sap_column];
                    }
                    dataRange = dataRange.get_Resize(rowCount, columnCount);
                    dataRange.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, levelData);
                }
                else {
                    // We need to mark any missing data
                    if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                        writeCell(oWorksheet2, level_quotewin_row, level_quotewin_column, dataMissingMessage);
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
                    if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                        dataRange = oWorksheet2.Cells[cpn_quotewin_row, cpn_quotewin_column];
                    }
                    else if (fileType.Equals("sap")) {
                        dataRange = oWorksheet2.Cells[cpn_sap_row, cpn_sap_column];
                    }
                    dataRange = dataRange.get_Resize(rowCount, columnCount);
                    dataRange.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, cpnData);
                }
                else {
                    // We need to mark any missing data
                    if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                        writeCell(oWorksheet2, cpn_quotewin_row, cpn_quotewin_column, dataMissingMessage);
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
                    if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                        dataRange = oWorksheet2.Cells[description_quotewin_row, description_quotewin_column];
                    }
                    else if (fileType.Equals("sap")) {
                        dataRange = oWorksheet2.Cells[description_sap_row, description_sap_column];
                    }
                    dataRange = dataRange.get_Resize(rowCount, columnCount);
                    dataRange.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, descriptionData);
                }
                else {
                    // We need to mark any missing data
                    if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                        writeCell(oWorksheet2, description_quotewin_row, description_quotewin_column, dataMissingMessage);
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
                    if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                        dataRange = oWorksheet2.Cells[quantity_quotewin_row, quantity_quotewin_column];
                    }
                    else if (fileType.Equals("sap")) {
                        dataRange = oWorksheet2.Cells[quantity_sap_row, quantity_sap_column];
                    }
                    dataRange = dataRange.get_Resize(rowCount, columnCount);
                    dataRange.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, quantityData);
                }
                else {
                    // We need to mark any missing data
                    if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                        writeCell(oWorksheet2, quantity_quotewin_row, quantity_quotewin_column, dataMissingMessage);
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
                    if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                        dataRange = oWorksheet2.Cells[designator_quotewin_row, designator_quotewin_column];
                    }
                    else if (fileType.Equals("sap")) {
                        dataRange = oWorksheet2.Cells[designator_sap_row, designator_sap_column];
                    }
                    dataRange = dataRange.get_Resize(rowCount, columnCount);
                    dataRange.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, designatorData);
                }
                else {
                    // We need to mark any missing data
                    if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                        writeCell(oWorksheet2, designator_quotewin_row, designator_quotewin_column, dataMissingMessage);
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
                    if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                        dataRange = oWorksheet2.Cells[manufacturer_quotewin_row, manufacturer_quotewin_column];
                    }
                    else if (fileType.Equals("sap")) {
                        dataRange = oWorksheet2.Cells[manufacturer_sap_row, manufacturer_sap_column];
                    }
                    dataRange = dataRange.get_Resize(rowCount, columnCount);
                    dataRange.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, manufacturerData);
                }
                else {
                    // We need to mark any missing data
                    if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                        writeCell(oWorksheet2, manufacturer_quotewin_row, manufacturer_quotewin_column, dataMissingMessage);
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
                    if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                        dataRange = oWorksheet2.Cells[mpn_quotewin_row, mpn_quotewin_column];
                    }
                    else if (fileType.Equals("sap")) {
                        dataRange = oWorksheet2.Cells[mpn_sap_row, mpn_sap_column];
                    }
                    dataRange = dataRange.get_Resize(rowCount, columnCount);
                    dataRange.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, mpnData);
                }
                else {
                    // We need to mark any missing data
                    if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                        writeCell(oWorksheet2, mpn_quotewin_row, mpn_quotewin_column, dataMissingMessage);
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
                    if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                        // Quotewin format does not have process field
                        //dataRange = oWorksheet2.Cells[process_quotewin_row, process_quotewin_column];
                    }
                    else if (fileType.Equals("sap")) {
                        dataRange = oWorksheet2.Cells[process_sap_row, process_sap_column];
                    }
                    if (dataRange != null) {
                        dataRange = dataRange.get_Resize(rowCount, columnCount);
                        dataRange.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, processData);
                    }
                }
                else {
                    // We need to mark any missing data
                    if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                        // Quotewin format does not have process field
                        //writeCell(oWorksheet2, process_quotewin_row, process_quotewin_column, dataMissingMessage);
                    }
                    else if (fileType.Equals("sap")) {
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
                    if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                        dataRange = oWorksheet2.Cells[notes_quotewin_row, notes_quotewin_column];
                    }
                    else if (fileType.Equals("sap")) {
                        dataRange = oWorksheet2.Cells[notes_sap_row, notes_sap_column];
                    }
                    dataRange = dataRange.get_Resize(rowCount, columnCount);
                    dataRange.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, notesData);
                }
                else {
                    // We need to mark any missing data
                    if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                        writeCell(oWorksheet2, notes_quotewin_row, notes_quotewin_column, dataMissingMessage);
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
                if (fileType.Equals("quotewin_single") || fileType.Equals("quotewin_multi")) {
                    // Iterate through every row of the sheet
                    for (int i = 2; i <= oWorksheet2.UsedRange.Rows.Count; i++) {
                        // Check if description is filled or not
                        string descriptionValue = Convert.ToString(oWorksheet2.Cells[i, description_quotewin_column].Value2);
                        if (string.IsNullOrEmpty(descriptionValue) || string.IsNullOrWhiteSpace(descriptionValue)) {
                            // Check if MPN is available
                            string mpnValue = Convert.ToString(oWorksheet2.Cells[i, mpn_quotewin_column].Value2);
                            if (string.IsNullOrEmpty(mpnValue) || string.IsNullOrWhiteSpace(mpnValue)) {
                                // Can't do anything if MPN is missing as well
                                writeCell(oWorksheet2, i, description_quotewin_column, dataMissingMessage);
                                writeCell(oWorksheet2, i, mpn_quotewin_column, dataMissingMessage);
                                continue;
                            }
                            else {
                                // Web scraping time!
                                string result = tryWebScrape(mpnValue);
                                if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result)) {
                                    // Couldn't find anything via web search
                                    writeCell(oWorksheet2, i, description_quotewin_column, dataMissingMessage);
                                    continue;
                                }
                                else {
                                    // Write data to cell
                                    writeCell(oWorksheet2, i, description_quotewin_column, result);
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

        private string tryWebScrape(string searchTerm) {
            // Blank string as default
            string result = "";
                // Try DigiKey first
                result = Webscraper.NaiveDigikey(searchTerm);
                if (string.IsNullOrEmpty(result)) {
                    // Try Element14 second
                    result = Webscraper.NaiveElement14(searchTerm);
                    if (string.IsNullOrEmpty(result)) {
                        // Try Mouser third
                        result = Webscraper.NaiveMouser(searchTerm);
                        if (string.IsNullOrEmpty(result)) {
                            // Try RS fourth
                            result = Webscraper.NaiveRS(searchTerm);
                        }
                    }
                }
            return result;
        }
    }
}
