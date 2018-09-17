using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace GPC_BOM {
    class WordExtraction {

        public void convertWordToExcel(frmMain mainApp, string wordFile, string outFile) {
            // Initialise MS Word variables
            Word.Application oWord = null;
            Word.Documents oDocs = null;
            Word.Document oDoc = null;

            // Initialise MS Excel variables
            Excel.Application oExcel = null;
            Excel.Workbook oWorkbook = null;
            Excel.Worksheet oWorksheet = null;

            // Try to run word and open document
            try {
                // Start Word
                oWord = new Word.Application();
                oWord.Visible = false;
                oWord.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
                if (oWord == null) {
                    Debug.WriteLine("Word could not be started. Check that compiled app reference matches Office version installed.");
                    return;
                }
                else {
                    Debug.WriteLine("Word.Application is started");
                }

                // Open Word document
                oDoc = oWord.Documents.Open(@wordFile);
                Debug.WriteLine("Word.Document is opened");
                mainApp.pbUpdate(20, frmMain.pbModeIncrement);

                // Check if there are any tables in our document
                if (oDoc.Tables.Count == 0) {
                    Debug.WriteLine("This document has no tables");
                    return;
                }

                // Start Excel
                oExcel = new Excel.Application();
                oExcel.Visible = false;
                oExcel.DisplayAlerts = false;
                oWorkbook = oExcel.Workbooks.Add(1);
                oWorksheet = oWorkbook.Sheets[1];
                //oExcel.CreateNewFile();
                //oExcel.SaveAs(inputPath);
                if (oWorksheet == null) {
                    Debug.WriteLine("Problem creating worksheet or opening Excel. Check that compiled app reference matches Office version installed.");
                    return;
                }
                else {
                    Debug.WriteLine("Excel.Application started.");
                }

                Debug.WriteLine("Processing file...");
                mainApp.pbUpdate(20, frmMain.pbModeIncrement);

                // Copy each Word doc table into a new Excel worksheet
                foreach (Word.Table table in oDoc.Tables) {
                    for (int row = 1; row <= table.Rows.Count; row++) {
                        for (int col = 1; col <= table.Columns.Count; col++) {
                            try {
                                // Problem: if cells are merged, this will throw an error
                                oWorksheet.Cells[row, col] = oExcel.WorksheetFunction.Clean(table.Cell(row, col).Range.Text);
                            }
                            catch (System.Runtime.InteropServices.COMException) {
                                // Ignore errors
                                continue;
                            }
                        }
                    }
                    oWorksheet = oWorkbook.Worksheets.Add();
                }
                // Delete excess sheet created by loop
                oWorksheet.Delete();
                Debug.WriteLine("Data copied. Attempting to save file...");
                mainApp.pbUpdate(20, frmMain.pbModeIncrement);

                // Save the Excel workbook (should probably be careful about overwriting stuff if it exists)
                // Save as Excel 97-2003 .xls file for backwards compatibility
                oWorkbook.SaveAs(outFile, Excel.XlFileFormat.xlExcel8, Type.Missing, Type.Missing, false, false, Excel.XlSaveAsAccessMode.xlNoChange, Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                mainApp.pbUpdate(20, frmMain.pbModeIncrement);

                // Clean up and close apps
                // TODO: Need to test if this also closes user's other windows!!!
                Debug.WriteLine("Cleaning up...");
                mainApp.pbUpdate(20, frmMain.pbModeIncrement);
                oExcel.Workbooks.Close();
                oExcel.Quit();
                oWord.Documents.Close();
                oWord.Quit(false);
            }
            catch (Exception ex) {
                Debug.WriteLine("Error:{0}", ex.Message);
            }
            finally {
                // Clean up the unmanaged Word/Excel COM resources by explicitly calling Marshal.FinalReleaseComObject on all accessor objects.  
                if (oDoc != null) {
                    Marshal.FinalReleaseComObject(oDoc);
                    oDoc = null;
                }
                if (oDocs != null) {
                    Marshal.FinalReleaseComObject(oDocs);
                    oDocs = null;
                }
                if (oWord != null) {
                    Marshal.FinalReleaseComObject(oWord);
                    oWord = null;
                }
                if (oWorksheet != null) {
                    Marshal.FinalReleaseComObject(oWorksheet);
                    oWorkbook = null;
                }
                if (oWorkbook != null) {
                    Marshal.FinalReleaseComObject(oWorkbook);
                    oWorkbook = null;
                }
                if (oExcel != null) {
                    Marshal.FinalReleaseComObject(oExcel);
                    oExcel = null;
                }
            }
        }
    }
}
