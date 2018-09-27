using System;
using System.Security.Permissions;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Word = Microsoft.Office.Interop.Word;
using System.Diagnostics;

namespace GPC_BOM {
    class PDFExtraction {
        public void convertPDFToWord(frmMain mainApp, string inFile, string outFile) {
            // Initialise MS Word variables
            Word.Application oWord = null;
            Word.Documents oDocs = null;
            Word.Document oDoc = null;

            // Try to run word and open document
            try {
                // Start Word
                mainApp.statusUpdate2("(Starting Word)");
                oWord = new Word.Application();
                oWord.Visible = false;
                oWord.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
                if (oWord == null) {
                    MessageBox.Show("Word could not be started. Please check that it is installed correctly and allows automation.");
                    Debug.WriteLine("Word could not be started. Check that compiled app reference matches Office version installed.");
                    return;
                }
                else {
                    Debug.WriteLine("Word.Application is started");
                }
                mainApp.pbUpdate(20, frmMain.pbModeIncrement);

                /*
                 * Check MS Word interop version (Interop version is not always the only installed version, but close enough)
                 * Incompatible:
                 * Office 2003: 11.0
                 * Office 2007: 12.0
                 * Office 2010: 14.0
                 * Native PDF conversion available starting from:
                 * Office 2013: 15.0
                 * Office 2016: 16.0
                 * */
                mainApp.statusUpdate2("(Checking Word Compatibility)");
                string msWordVersion = oWord.Application.Version;
                Debug.WriteLine("MS Word Version: " + msWordVersion);
                if (Convert.ToDouble(msWordVersion) > 14) {
                    Debug.WriteLine("MS Word Version is newer than 2013. Native PDF conversion available.");
                }
                else {
                    MessageBox.Show("It appears that the installed version of Microsoft Word does not support native PDF conversion. Word 2013 or greater is required.");
                }

                // Call subroutine to check/set registry value for MS Word silent PDF conversion
                // TODO: Test before uncommenting for production
                this.setWordPDFConversionSilent(msWordVersion);
                mainApp.pbUpdate(20, frmMain.pbModeIncrement);

                // Open Word document
                mainApp.statusUpdate2("(Opening PDF and converting to Word)");
                oDoc = oWord.Documents.Open(inFile, false, true);
                Debug.WriteLine("Word.Document is opened");
                mainApp.pbUpdate(20, frmMain.pbModeIncrement);
                
                // PDF automatic converts to Word format on open
                // We can now save the document
                mainApp.statusUpdate2("(Saving as Word document)");
                oDoc.SaveAs2(outFile, Word.WdSaveFormat.wdFormatDocumentDefault);
                mainApp.pbUpdate(20, frmMain.pbModeIncrement);

                // Clean up and close apps
                // TODO: Need to test if this also closes user's other windows!!!
                mainApp.statusUpdate2("(Closing Word)");
                Debug.WriteLine("Cleaning up...");
                mainApp.pbUpdate(20, frmMain.pbModeIncrement);
                oDoc.Close(false, Type.Missing, Type.Missing);
                //oWord.Documents.Close(false, Type.Missing, Type.Missing);
                oWord.Quit(false, Type.Missing, Type.Missing);
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
            }
        }

        private void setWordPDFConversionSilent(string msWordVersion) {
            // Normally, opening a PDF document in word generates a conversion prompt
            // This prompt can be disabled via a registry key
            // Enclose in try-catch as user may not have permissions to read/write registry
            try {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Office\\" + msWordVersion.Trim() + "\\Word\\Options", true);
                if (key != null) {
                    int value = Convert.ToInt16(key.GetValue("DisableConvertPdfWarning", -1, RegistryValueOptions.None).ToString());
                    if (value == 1) {
                        Debug.WriteLine("Registry key already set.");
                    }
                    else {
                        // Write the value regardless of whether it already exists or not
                        key.SetValue("DisableConvertPdfWarning", 1, RegistryValueKind.DWord);
                        key.Close();
                    }
                }
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.ToString());
            }
        }

    }
}
