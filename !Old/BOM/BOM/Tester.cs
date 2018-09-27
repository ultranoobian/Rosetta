using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices; 
using System.Diagnostics;

namespace BOM {
    public partial class Tester : Form {
        public Tester() {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e) {

            // Initialise MS Word variables
            Word.Application oWord = null;
            Word.Documents oDocs = null;
            Word.Document oDoc = null;
            Word.Paragraphs oParas = null;
            Word.Paragraph oPara = null;
            Word.Range oParaRng = null;
            Word.Font oFont = null;

            // Initialise MS Excel variables
            Excel.Application oExcel = null;
            Excel.Workbook oWorkbook = null;
            Excel.Worksheet oWorksheet = null;

            // Try to run word and open document
            try {
                // Start Word
                oWord = new Word.Application();
                oWord.Visible = true;
                if (oWord == null) {
                    Debug.WriteLine("Word could not be started. Check that compiled app reference matches Office version installed.");
                    return;
                }
                else {
                    Debug.WriteLine("Word.Application is started");
                }

                // Open Word document
                //oDoc = oWord.Documents.Open(@"C:\Test.docx");
                oDoc = oWord.Documents.Open(@"C:\Test2.doc");
                Debug.WriteLine("Word.Document is opened");

                // Check if there are any tables in our document
                if (oDoc.Tables.Count == 0) {
                    Debug.WriteLine("This document has no tables");
                    return;
                }

                // Start Excel
                oExcel = new Excel.Application();
                oExcel.Visible = true;
                oWorkbook = oExcel.Workbooks.Add(1);
                oWorksheet = oWorkbook.Sheets[1];
                if (oWorksheet == null) {
                    Debug.WriteLine("Problem creating worksheet or opening Excel. Check that compiled app reference matches Office version installed.");
                    return;
                }
                else {
                    Debug.WriteLine("Excel.Application started.");
                }

                // Copy each Word doc table into a new Excel worksheet
                foreach (Word.Table table in oDoc.Tables) {
                    for (int row = 1; row <= table.Rows.Count; row++) {
                        for (int col = 1; col <= table.Columns.Count; col++) {
                            try {
                                // Problem: if cells are merged, this will throw an error
                                oWorksheet.Cells[row, col] = oExcel.WorksheetFunction.Clean(table.Cell(row, col).Range.Text);
                            }
                            catch (System.Runtime.InteropServices.COMException COMex) {
                                continue;
                            }

                            
                        }
                    }
                    oWorksheet = oWorkbook.Worksheets.Add();
                }
                // Delete excess sheet created by loop
                oWorksheet.Delete();

                // Save the Excel workbook (should probably be careful about overwriting stuff if it exists)
                oWorkbook.SaveAs("C:\\Test.xlsx", Excel.XlFileFormat.xlWorkbookDefault);

                // Clean up and close apps
                // !!! Need to test if this also closes user's other windows !!!
                oExcel.Workbooks.Close();
                oExcel.Quit();
                oWord.Documents.Close();
                oWord.Quit();
            }
            catch (Exception ex) {
                Debug.WriteLine("Error:{0}", ex.Message);
            }
            finally {
                // Clean up the unmanaged Word COM resources by explicitly calling Marshal.FinalReleaseComObject on all accessor objects.  
                if (oFont != null) {
                    Marshal.FinalReleaseComObject(oFont);
                    oFont = null;
                }
                if (oParaRng != null) {
                    Marshal.FinalReleaseComObject(oParaRng);
                    oParaRng = null;
                }
                if (oPara != null) {
                    Marshal.FinalReleaseComObject(oPara);
                    oPara = null;
                }
                if (oParas != null) {
                    Marshal.FinalReleaseComObject(oParas);
                    oParas = null;
                }
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
            }
        }
    }
}
