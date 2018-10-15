using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelDataReader;

namespace GPC_BOM {
    public partial class frmMain : Form {

        public static string pbModeSet = "set";
        public static string pbModeIncrement = "increment";
        public static string pbModeReset = "reset";

        public frmMain() {
            InitializeComponent();
        }

        #region Menu Strip Code
        private void msiOpen_Click(object sender, EventArgs e) {
            int queueSize = 0;
            if (this.openFileDialog.ShowDialog() == DialogResult.OK) {
                foreach (string file in this.openFileDialog.FileNames) {
                    if (file != null) {
                        string[] arr = new string[4];
                        arr[0] = Path.GetFileNameWithoutExtension(file); // Name
                        arr[1] = Path.GetExtension(file); // Type
                        arr[2] = file; // Location
                        arr[3] = "Ready"; // Status
                        ListViewItem lvi = new ListViewItem(arr);
                        lvFiles.Items.Add(lvi);
                    }
                    queueSize = queueSize + 1;
                }
                this.lblDragDrop.Hide();
                this.picWatermark.Hide();
            }
            statusUpdate(String.Format("{0} files added to queue", queueSize));
        }
        private void msiExit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void msiSettings_Click(object sender, EventArgs e) {
            Settings settingsForm = new Settings();
            settingsForm.Show();
        }

        private void msiDocumentation_Click(object sender, EventArgs e) {
            try {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Resources\\SoftwareGuide.pdf");
                Process.Start(path);
            }
            catch (Exception ex) {
                MessageBox.Show("The help file seems to be missing, inaccessible or damaged.");
                Debug.WriteLine(ex.ToString());
            }
        }
        private void msiAbout_Click(object sender, EventArgs e) {
            AboutBox about = new AboutBox();
            about.Show();
        }
        #endregion

        public void pbUpdate(int value, string mode) {

            // Check input
            if (value < 0 || value > 100) {
                Debug.WriteLine("Value out of range.");
                return;
            }
            if (mode.Equals(pbModeIncrement) && tsProgressBar.Value > 80) {
                // It's over 80!
                this.tsProgressBar.Value = 100;
                return;
            }

            // Update the progress bar according to mode and value
            if (mode.Equals(pbModeSet)) {
                //this.backgroundWorker.ReportProgress(value);
                this.tsProgressBar.Value = value;
                this.tsProgressBar.Invalidate();
            }
            else if (mode.Equals(pbModeIncrement)) {
                // The step has been set to 20 by design
                this.tsProgressBar.PerformStep();
                this.tsProgressBar.Invalidate();
            }
            else if (mode.Equals(pbModeReset)) {
                //this.backgroundWorker.ReportProgress(0);
                this.tsProgressBar.Value = 0;
                this.tsProgressBar.Invalidate();
            }
            else {
                Debug.WriteLine("Received an unrecognised command to pbUpdate()");
            }
        }

        public void statusUpdate(string message) {
            // Set the status message
            this.tsStatus.Text = message;
        }

        public void statusUpdate2(string message) {
            // Set the secondary status message
            this.tsStatus2.Text = message;
        }

        public static bool isFolderWritable(string tmpPath) {
            // Test if current context allows write to specified directory
            try {
                System.IO.File.Create(tmpPath + "\\" + "0.tmp").Close();
                System.IO.File.Delete(tmpPath + "\\" + "0.tmp");
            }
            catch (System.UnauthorizedAccessException ex) {
                Debug.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }

        private void frmMain_Load(object sender, EventArgs e) {
            // Initialise background worker for progress bar sync
            //this.backgroundWorker.RunWorkerAsync();
            statusUpdate("Application started");
            this.Icon = Properties.Resources.gpc_icon;
        }

        private void btnBrowse_Click(object sender, EventArgs e) {
            // Button action to open browse dialog
            DialogResult result = this.folderBrowser.ShowDialog();
            if (result == DialogResult.OK) {
                tbPath.Text = this.folderBrowser.SelectedPath;
                // Check write permissions on folder
                if (isFolderWritable(tbPath.Text) == false) {
                    MessageBox.Show("You don't appear to have permissions to write to that folder.");
                    tbPath.Clear();
                    this.folderBrowser.ShowDialog();
                    return;
                }
            }
        }

        private void lvFiles_DragEnter(object sender, DragEventArgs e) {
            // Drag and drop effect in windows
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true) {
                e.Effect = DragDropEffects.All;
            }
        }

        private void lvFiles_DragDrop(object sender, DragEventArgs e) {
            // This just allows you to drop file(s) into the list view and show their details
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int queueSize = 0;
            foreach (string file in files) {
                string[] arr = new string[4];
                arr[0] = Path.GetFileNameWithoutExtension(file); // Name
                arr[1] = Path.GetExtension(file); // Type
                arr[2] = file; // Location
                arr[3] = "Ready"; // Status
                ListViewItem lvi = new ListViewItem(arr);
                lvFiles.Items.Add(lvi);
                queueSize = queueSize + 1;
            }
            // Hide the drag and drop label now
            this.lblDragDrop.Hide();
            this.picWatermark.Hide();
            statusUpdate(String.Format("{0} files added to queue", queueSize));
        }

        private void btnRemove_Click(object sender, EventArgs e) {
            // Removes only the highlighted item(s) from the list view
            // The multi-select property can be toggled via the designer
            if (lvFiles.SelectedItems.Count != 0) {
                lvFiles.SelectedItems[0].Remove();
                if (lvFiles.Items.Count == 0) {
                    this.lblDragDrop.Show();
                    this.dataGridView.DataSource = null;
                    this.dataGridView.Refresh();
                    this.cbSheet.Items.Clear();
                    this.picWatermark.Show();
                }
            }
            else {
                Debug.Print("Nothing was selected...");
            }
        }

        private void btnClearQueue_Click(object sender, EventArgs e) {
            // Removes all items from the list view
            lvFiles.Items.Clear();
            this.lblDragDrop.Show();
            this.dataGridView.DataSource = null;
            this.dataGridView.Refresh();
            this.cbSheet.Items.Clear();
            this.picWatermark.Show();
        }

        private void btnConvert_Click(object sender, EventArgs e) {
            statusUpdate("Processing queue...");

            #region Validate and check information prior to conversion

            // Check that the queue is not empty
            if (lvFiles.Items.Count == 0) {
                MessageBox.Show("Please add files to the queue.");
                return;
            }

            // Check that an output format has been selected
            if (cbConvertOption.SelectedIndex == -1) {
                MessageBox.Show("Please select an output format.");
                return;
            }

            // Check that an output location has been selected
            if (string.IsNullOrWhiteSpace(tbPath.Text) || string.IsNullOrEmpty(tbPath.Text)) {
                MessageBox.Show("Please select an output location.");
                return;
            }

            #endregion

            // Show wait cursor and block interaction
            this.Cursor = Cursors.WaitCursor;
            this.Enabled = false;

            string conversionFormat = null;
            if (cbConvertOption.SelectedIndex == 0) {
                conversionFormat = "quotewin_single";
            }
            else if (cbConvertOption.SelectedIndex == 1) {
                conversionFormat = "quotewin_multi";
            }
            else if (cbConvertOption.SelectedIndex == 2) {
                conversionFormat = "sap";
            }

            foreach (ListViewItem item in lvFiles.Items) {
                // Trigger the preview
                item.Selected = true;
                item.SubItems[3].Text = "Processing";
                // Get file type and process accordingly
                if (String.Equals(item.SubItems[1].Text, ".doc", StringComparison.OrdinalIgnoreCase) == true || String.Equals(item.SubItems[1].Text, ".docx", StringComparison.OrdinalIgnoreCase) == true) {
                    // Give the user a status update
                    statusUpdate(String.Format("Processing file: {0}", item.SubItems[0].Text + item.SubItems[1].Text));
                    // It's a Word document - convert to Excel first
                    var wordEx = new WordExtraction();
                    string inFile = item.SubItems[2].Text;
                    string outFile = this.tbPath.Text + "\\" + item.SubItems[0].Text + "_extract.xls";
                    wordEx.convertWordToExcel(this, inFile, outFile);
                    wordEx = null;
                    // Reset progress bar for file
                    pbUpdate(0, pbModeReset);
                    // Next step: process as Excel
                    statusUpdate(String.Format("Processing file: {0}", item.SubItems[0].Text + "_extract.xls"));
                    var excelConvert = new ExcelConversion();
                    string inFile2 = outFile;
                    string outFile2 = this.tbPath.Text + "\\" + item.SubItems[0].Text + "_" + conversionFormat;
                    if (conversionFormat.Equals("sap")) {
                        outFile2 = outFile2 + ".xls";
                    }
                    else {
                        outFile2 = outFile2 + ".csv";
                    }
                    // Ask user for row header
                    int headerRow = askRowHeader(item.SubItems[0].Text);
                    // Call Excel conversion function
                    excelConvert.transformExcel(this, inFile2, outFile2, headerRow, conversionFormat);
                    // Reset progress bar for file
                    pbUpdate(0, pbModeReset);
                }
                else if (String.Equals(item.SubItems[1].Text, ".pdf", StringComparison.OrdinalIgnoreCase) == true) {
                    statusUpdate(String.Format("Processing file: {0}", item.SubItems[0].Text + item.SubItems[1].Text));
                    // It's a PDF - convert to Word first
                    string inFile = item.SubItems[2].Text;
                    string outFile = this.tbPath.Text + "\\" + item.SubItems[0].Text + "_extract.doc";
                    var pdfEX = new PDFExtraction();
                    pdfEX.convertPDFToWord(this, inFile, outFile);
                    pdfEX = null;
                    // Reset progress bar for file
                    pbUpdate(0, pbModeReset);
                    // Next step: convert from Word to Excel
                    statusUpdate(String.Format("Processing file: {0}", item.SubItems[0].Text + "_extract.doc"));
                    var wordEx = new WordExtraction();
                    string inFile2 = outFile;
                    string outFile2 = this.tbPath.Text + "\\" + item.SubItems[0].Text + "_extract.xls";
                    wordEx.convertWordToExcel(this, inFile2, outFile2);
                    wordEx = null;
                    // Reset progress bar for file
                    pbUpdate(0, pbModeReset);
                    // Next step: process as Excel
                    statusUpdate(String.Format("Processing file: {0}", item.SubItems[0].Text + "_extract.xls"));
                    var excelConvert = new ExcelConversion();
                    string inFile3 = outFile2;
                    string outFile3 = this.tbPath.Text + "\\" + item.SubItems[0].Text + "_" + conversionFormat;
                    if (conversionFormat.Equals("sap")) {
                        outFile3 = outFile3 + ".xls";
                    }
                    else {
                        outFile3 = outFile3 + ".csv";
                    }
                    // Ask user for row header
                    int headerRow = askRowHeader(item.SubItems[0].Text);
                    // Call Excel conversion function
                    excelConvert.transformExcel(this, inFile3, outFile3, headerRow, conversionFormat);
                    // Reset progress bar for file
                    pbUpdate(0, pbModeReset);
                }
                else if (String.Equals(item.SubItems[1].Text, ".xls", StringComparison.OrdinalIgnoreCase) == true || String.Equals(item.SubItems[1].Text, ".xlsx", StringComparison.OrdinalIgnoreCase) == true) {
                    statusUpdate(String.Format("Processing file: {0}", item.SubItems[0].Text + item.SubItems[1].Text));
                    // It's a native Excel spreadsheet
                    var excelConvert = new ExcelConversion();
                    string inFile = item.SubItems[2].Text;
                    string outFile = this.tbPath.Text + "\\" + item.SubItems[0].Text + "_" + conversionFormat;
                    if (conversionFormat.Equals("sap")) {
                        outFile = outFile + ".xls";
                    }
                    else {
                        outFile = outFile + ".csv";
                    }
                    // Ask user for row header
                    int headerRow = askRowHeader(item.SubItems[0].Text);
                    excelConvert.transformExcel(this, inFile, outFile, headerRow, conversionFormat);
                    // Reset progress bar for file
                    pbUpdate(0, pbModeReset);
                }
                else {
                    // File extension unrecognised
                    Debug.WriteLine("File type unrecognised.");
                    statusUpdate(String.Format("Unrecognised file skipped: {0}", item.SubItems[0].Text + item.SubItems[1].Text));
                }
                // Update status in list
                item.SubItems[3].Text = "Complete";
            }
            // Queue has been completely processed
            statusUpdate("Ready");
            statusUpdate2(" ");
            this.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private int askRowHeader(string filename) {
            int value = 1;
            string input = Microsoft.VisualBasic.Interaction.InputBox(String.Format("Please set the row header for '{0}':", filename), "Input Required", "1", 0, 0);
            if (!Int32.TryParse(input, out value)) {
                value = askRowHeader(filename);
            }
            return value;
        }

        private void lvFiles_SelectedIndexChanged(object sender, EventArgs e) {
            IExcelDataReader excelReader = null;
            // Don't run when deselecting, clear instead
            if (lvFiles.SelectedItems.Count == 0) {
                this.dataGridView.DataSource = null;
                this.dataGridView.Refresh();
                this.cbSheet.Items.Clear();
                this.picWatermark.Show();
                return;
            }
            else {
                this.picWatermark.Hide();
                // Determine what kind of excel file it is
                string file = lvFiles.SelectedItems[0].SubItems[2].Text;

                try {
                    if (lvFiles.SelectedItems[0].SubItems[1].Text.ToLower().Equals(".xls")) {
                        // Reading from a binary Excel file ('97-2003 format; *.xls)
                        FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read);
                        excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (lvFiles.SelectedItems[0].SubItems[1].Text.ToLower().Equals(".xlsx")) {
                        // Reading from a OpenXml Excel file (2007 format; *.xlsx)
                        FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read);
                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else {
                        return;
                    }
                }
                catch (System.IO.IOException ex) {
                    MessageBox.Show("Encountered an error reading the file. Check that it is not open or in use, and that it still exists.");
                    Debug.WriteLine(ex.ToString());
                    return;
                }
            }
            // DataSet - The result of each spreadsheet will be created in the result.Tables
            DataSet result = excelReader.AsDataSet();
            // Populate combo box of sheets
            foreach (DataTable dt in result.Tables) {
                cbSheet.Items.Add(dt.TableName);
            }
            // Close file
            excelReader.Close();
            // Show data
            this.cbSheet.SelectedIndex = 0;
            this.dataGridView.DataSource = result.Tables[cbSheet.SelectedIndex];
            foreach (DataGridViewRow row in dataGridView.Rows) {
                row.HeaderCell.Value = (row.Index + 1).ToString();
            }
            this.dataGridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void cbSheet_SelectedIndexChanged(object sender, EventArgs e) {
            IExcelDataReader excelReader = null;
            // Determine what kind of excel file it is
            string file = lvFiles.SelectedItems[0].SubItems[2].Text;

            if (lvFiles.SelectedItems[0].SubItems[1].Text.ToLower().Equals(".xls")) {
                // Reading from a binary Excel file ('97-2003 format; *.xls)
                FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read);
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else if (lvFiles.SelectedItems[0].SubItems[1].Text.ToLower().Equals(".xlsx")) {
                // Reading from a OpenXml Excel file (2007 format; *.xlsx)
                FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read);
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            else {
                return;
            }
            // DataSet - The result of each spreadsheet will be created in the result.Tables
            DataSet result = excelReader.AsDataSet();
            // Close file
            excelReader.Close();
            // Show data
            dataGridView.DataSource = result.Tables[cbSheet.SelectedIndex];
            foreach (DataGridViewRow row in dataGridView.Rows) {
                row.HeaderCell.Value = (row.Index + 1).ToString();
            }
            dataGridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }
    }
}
