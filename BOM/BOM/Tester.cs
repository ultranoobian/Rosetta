using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices; 

namespace BOM {
    public partial class Tester : Form {
        public Tester() {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e) {
            //

            Word.Application oWord = null;
            Word.Documents oDocs = null;
            Word.Document oDoc = null;
            Word.Paragraphs oParas = null;
            Word.Paragraph oPara = null;
            Word.Range oParaRng = null;
            Word.Font oFont = null; 

            //
            try {
                oWord = new Word.Application();
                oWord.Visible = true;
                Console.WriteLine("Word.Application is started");

                oDoc = oWord.Documents.Open(@"C:\Test.docx");
                Console.WriteLine("Word.Document is opened");

            }
            catch (Exception ex) {
                Console.WriteLine("Error:{0}", ex.Message);
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
