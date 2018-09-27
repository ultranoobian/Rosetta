using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace excel
{
	public partial class Form1 : Form
	{
		/*
        int desc_x = Excel.desc_x;
        int desc_y = Excel.desc_y;
        int qty_x = Excel.qty_x;
        int qty_y = Excel.qty_y;
        int desi_x = Excel.desi_x;
        int desi_y = Excel.desi_y;
        int manf_x = Excel.manf_x;
        int manf_y = Excel.manf_y;
        int mpn_x = Excel.mpn_x;
        int mpn_y = Excel.mpn_y;
        */
		string at = @"";
		string inputPath = @"";

		string outputPath = @"";

		string file = "";

		string fileName = "";

		string extension = "";

		string stdDetails = "{0, -10}{1, -10}{2, -15}{3, -20}";

		public Form1()
		{
			InitializeComponent();
		}

        private void Form1_Load_1(object sender, EventArgs e)
		{
			listBox1.Items.Add(String.Format(stdDetails, "Name", "Type", "Location", "Message"));
		}

		public void OpenFile()
		{
			Excel excel = new Excel(@"‪helloworld.xlsx", 1);
			MessageBox.Show(excel.ReadCell(0, 0));
		}
        // search for necessary field
        // store the coordinate of each field as static variable
		public void SearchField()
		{
			Excel ex = new Excel(inputPath, 1);
            ex.findEnd();
            ex.findField();
			ex.Close();
            ex.Quit();
        }
        // scan "Level"
        public void WriteLevel()
        {
            try
            {
                Excel input = new Excel(inputPath, 1);
                string[,] read = input.ReadRange(Excel.lvl_x + 1, Excel.lvl_y, Excel.lastRow, Excel.lvl_y);
                input.Close();
                input.Quit();

                Excel output = new Excel(outputPath, 1);
                output.WriteRange(5, 1, Excel.lastRow + 3, 1, read);
                output.Save();
                output.Close();
                output.Quit();
            }
			catch (COMException)
			{
                //Debug.Print("We can't save 'output.xlsx' because the file is read-only.");
            }
        }
        // scan "CPN"
        public void WriteCPN()
        {
            try
            {
                Excel input = new Excel(inputPath, 1);
                string[,] read = input.ReadRange(Excel.cpn_x + 1, Excel.cpn_y, Excel.lastRow, Excel.cpn_y);
                input.Close();
                input.Quit();

                Excel output = new Excel(outputPath, 1);
                output.WriteRange(5, 2, Excel.lastRow + 3, 2, read);
                output.Save();
                output.Close();
                output.Quit();
            }
			catch (COMException)
			{
                //Debug.Print("We can't save 'output.xlsx' because the file is read-only.");
            }
        }
        // scan "Description"
        public void WriteDescription()
        {
            try
            {
                Excel input = new Excel(inputPath, 1);
                // MessageBox.Show("desc_x: " + Excel.desc_x + "desc_y: " + Excel.desc_y);
                string[,] read = input.ReadRange(Excel.desc_x + 1, Excel.desc_y, Excel.lastRow, Excel.desc_y);
                input.Close();
                input.Quit();

                Excel output = new Excel(outputPath, 1);
                output.WriteRange(5, 3, Excel.lastRow + 3, 3, read);
                output.Save();
                output.Close();
                output.Quit();
            }
			catch (COMException)
			{
                //Debug.Print("We can't save 'output.xlsx' because the file is read-only.");
            }
            
        }
        // scan "Quantity"
        public void WriteQuantity()
        {
            try
            {
                Excel input = new Excel(inputPath, 1);
                string[,] read = input.ReadRange(Excel.qty_x + 1, Excel.qty_y, Excel.lastRow, Excel.qty_y);
                input.Close();
                input.Quit();

                Excel output = new Excel(outputPath, 1);
                output.WriteRange(5, 4, Excel.lastRow + 3, 4, read);
                output.Save();
                output.Close();
                output.Quit();
            }
			catch (COMException)
			{
                //Debug.Print("We can't save 'output.xlsx' because the file is read-only.");
            }
            
        }
        // scan "Designator"
        public void WriteDesignator()
        {
            try
            {
                Excel input = new Excel(inputPath, 1);
                string[,] read = input.ReadRange(Excel.desi_x + 1, Excel.desi_y, Excel.lastRow, Excel.desi_y);
                input.Close();
                input.Quit();

                Excel output = new Excel(outputPath, 1);
                output.WriteRange(5, 5, Excel.lastRow + 3, 5, read);
                output.Save();
                output.Close();
                output.Quit();
            }
			catch (COMException)
			{
                //Debug.Print("We can't save 'output.xlsx' because the file is read-only.");
            }
            
        }
        //Scan "Manufacturer"
        public void WriteManufacturer()
        {
            try
            {
                Excel input = new Excel(inputPath, 1);
                string[,] read = input.ReadRange(Excel.manf_x + 1, Excel.manf_y, Excel.lastRow, Excel.manf_y);
                input.Close();
                input.Quit();

                Excel output = new Excel(outputPath, 1);
                output.WriteRange(5, 6, Excel.lastRow + 3, 6, read);
                output.Save();
                output.Close();
                output.Quit();
            }
            catch (COMException)
            {
                //Debug.Print("We can't save 'output.xlsx' because the file is read-only.");
            }         
        }
        // scan "MPN"
        public void WriteMPN()
        {
            try
            {
                Excel input = new Excel(inputPath, 1);
                string[,] read = input.ReadRange(Excel.mpn_x + 1, Excel.mpn_y, Excel.lastRow, Excel.mpn_y);
                input.Close();
                input.Quit();

                Excel output = new Excel(outputPath, 1);
                output.WriteRange(5, 7, Excel.lastRow + 3, 7, read);
                output.Save();
                output.Close();
                output.Quit();
            }
			catch (COMException)
			{
                //Debug.Print("We can't save 'output.xlsx' because the file is read-only.");
            }
        }
        // scan "Process"
        public void WriteProcess()
        {
            try
            {
                Excel input = new Excel(inputPath, 1);
                string[,] read = input.ReadRange(Excel.prcs_x + 1, Excel.prcs_y, Excel.lastRow, Excel.prcs_y);
                input.Close();
                input.Quit();

                Excel output = new Excel(outputPath, 1);
                output.WriteRange(5, 8, Excel.lastRow + 3, 8, read);
                output.Save();
                output.Close();
                output.Quit();
            }
			catch (COMException)
			{
                //Debug.Print("We can't save 'output.xlsx' because the file is read-only.");
            }
        }
        // scan "CPN"
        public void WriteNotes()
        {
            try
            {
                Excel input = new Excel(inputPath, 1);
                string[,] read = input.ReadRange(Excel.note_x + 1, Excel.note_y, Excel.lastRow, Excel.note_y);
                input.Close();
                input.Quit();

                Excel output = new Excel(outputPath, 1);
                output.WriteRange(5, 9, Excel.lastRow + 3, 9, read);
                output.Save();
                output.Close();
                output.Quit();
            }
			catch (COMException)
			{
                //Debug.Print("We can't save 'output.xlsx' because the file is read-only.");
            }
        }

        // create output file
        public void CreateOutput()
		{
            Excel excel = new Excel(outputPath, 1);
            excel.WriteCell(1, 1, "P/N:");
            excel.WriteCell(1, 2, "Rev:");
            excel.WriteCell(1, 3, "Description");
            // blue color bold
            excel.WriteCell(4, 1, "Level");
            excel.WriteCell(4, 2, "CPN");
            // red color bold (mandatory field)
            excel.WriteCell(4, 3, "Description");
            excel.WriteCell(4, 4, "Quantity");
            excel.WriteCell(4, 5, "Designator");
            excel.WriteCell(4, 6, "Manufacturer");
            excel.WriteCell(4, 7, "MPN");
            // blue color bold
            excel.WriteCell(4, 8, "Process");
            excel.WriteCell(4, 9, "Notes");
            // excel.styleField();

            excel.Save();
			excel.Close();
            excel.Quit();
        }

		// create new file
		public void CreateBOMB()
		{
			getPath();
			Excel ex = new Excel();

			ex.CreateNewFile();
			
			ex.SaveAs(outputPath);
            ex.Quit();
		}

		public void getPath()
		{
			inputPath += listBox1.Text;
			file = Path.GetFileName(inputPath);
			fileName = Path.GetFileNameWithoutExtension(inputPath);
			extension = Path.GetExtension(inputPath);
			if (extension.CompareTo(".doc") == 0)
			{
				extension = ".xlsx";
				inputPath = Path.GetDirectoryName(inputPath) + "\\" + fileName + extension;
				convertWordToExcel();
			}
			fileName += "_SAP";
			file = fileName + extension;
			outputPath = Path.GetDirectoryName(inputPath) + "\\" + file;
			textBox1.Text = outputPath;
		}

		public void convertWordToExcel()
		{

			// Initialise MS Word variables
			Word.Application oWord = null;
			Word.Documents oDocs = null;
			Word.Document oDoc = null;
			Word.Paragraphs oParas = null;
			Word.Paragraph oPara = null;
			Word.Range oParaRng = null;
			Word.Font oFont = null;

			// Initialise MS Excel variables

			//Excel.Application oExcel = null;
			//Excel.Workbook oWorkbook = null;
			//Excel.Worksheet oWorksheet = null;
			_Excel._Application excel = new _Excel.Application();
			Workbook oWorkbook;
			Worksheet oWorksheet;

			// Try to run word and open document
			try
			{
				// Start Word
				oWord = new Word.Application();
				oWord.Visible = true;
				if (oWord == null)
				{
					Debug.WriteLine("Word could not be started. Check that compiled app reference matches Office version installed.");
					return;
				}
				else
				{
					Debug.WriteLine("Word.Application is started");
				}

				// Open Word document
				//oDoc = oWord.Documents.Open(@"C:\Test.docx");
				oDoc = oWord.Documents.Open(at + Path.GetDirectoryName(inputPath) + "\\" + fileName);
				MessageBox.Show("passed1");
				Debug.WriteLine("Word.Document is opened");

				// Check if there are any tables in our document
				if (oDoc.Tables.Count == 0)
				{
					Debug.WriteLine("This document has no tables");
					return;
				}

				// Start Excel
				Excel oExcel = new Excel();
			　　excel.Visible = true;
				oWorkbook =excel.Workbooks.Add(1);
				oWorksheet = oWorkbook.Sheets[1];
				oExcel.CreateNewFile();
				oExcel.SaveAs(inputPath);
				if (oWorksheet == null)
				{
					Debug.WriteLine("Problem creating worksheet or opening Excel. Check that compiled app reference matches Office version installed.");
					return;
				}
				else
				{
					Debug.WriteLine("Excel.Application started.");
				}

				// Copy each Word doc table into a new Excel worksheet
				foreach (Word.Table table in oDoc.Tables)
				{
					for (int row = 1; row <= table.Rows.Count; row++)
					{
						for (int col = 1; col <= table.Columns.Count; col++)
						{
							try
							{
								// Problem: if cells are merged, this will throw an error
								oWorksheet.Cells[row, col] = excel.WorksheetFunction.Clean(table.Cell(row, col).Range.Text);
							}
							catch (System.Runtime.InteropServices.COMException)
							{
								continue;
							}


						}
					}
					oWorksheet = oWorkbook.Worksheets.Add();
				}
				// Delete excess sheet created by loop
				oWorksheet.Delete();

				// Save the Excel workbook (should probably be careful about overwriting stuff if it exists)
				//oWorkbook.SaveAs(inputPath, XlFileFormat.xlWorkbookDefault);
				//excel.Save();
				//Workbooks.Save();
				oWorkbook.Save();
				// oExcel.Save();

				// Clean up and close apps
				// !!! Need to test if this also closes user's other windows !!!
				excel.Workbooks.Close();
				excel.Quit();
				oWord.Documents.Close();
				oWord.Quit();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error:{0}", ex.Message);
			}
			finally
			{
				// Clean up the unmanaged Word COM resources by explicitly calling Marshal.FinalReleaseComObject on all accessor objects.  
				if (oFont != null)
				{
					Marshal.FinalReleaseComObject(oFont);
					oFont = null;
				}
				if (oParaRng != null)
				{
					Marshal.FinalReleaseComObject(oParaRng);
					oParaRng = null;
				}
				if (oPara != null)
				{
					Marshal.FinalReleaseComObject(oPara);
					oPara = null;
				}
				if (oParas != null)
				{
					Marshal.FinalReleaseComObject(oParas);
					oParas = null;
				}
				if (oDoc != null)
				{
					Marshal.FinalReleaseComObject(oDoc);
					oDoc = null;
				}
				if (oDocs != null)
				{
					Marshal.FinalReleaseComObject(oDocs);
					oDocs = null;
				}
				if (oWord != null)
				{
					Marshal.FinalReleaseComObject(oWord);
					oWord = null;
				}
			}
		}

		// Execute CreateBOMB and output_file. This will be in button2 (Start (conversion)) eventually
		private void button1_Click(object sender, EventArgs e)
        {
            CreateBOMB();
            CreateOutput();
            MessageBox.Show("output file created");
        }

        // Execute field scanner
        private void button2_Click_1(object sender, EventArgs e)
		{
            //timer1.Start();
            SearchField();
			WriteLevel();
			WriteCPN();

			WriteDescription();

			WriteQuantity();

			WriteDesignator();

			WriteManufacturer();

			WriteMPN();

			WriteProcess();

			WriteNotes();

			MessageBox.Show("convert completed");
		}

        // Save (Preview?)
		private void button3_Click(object sender, EventArgs e)
		{
			using (Form2 fm2 = new Form2(CreateBOMB))
			{
				fm2.ShowDialog(this);
			}
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBox1_DragDrop(object sender, DragEventArgs e)
		{
			// This just allows you to drop a file into the textbox and display it's path.
			string[] files1 = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			foreach (string file1 in files1)
			listBox1.Text = file1;
			
			//MessageBox.Show(textBox1.Text);
		}

		private void textBox1_DragEnter(object sender, DragEventArgs e)
		{
			//Drag and drop effect in windows
			if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
			{
				e.Effect = DragDropEffects.All;
			}
		}

		private void listBox1_DragDrop(object sender, DragEventArgs e)
		{
			// This just allows you to drop a file into the textbox and display it's path.
			string[] files1 = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			foreach (string file1 in files1)
				listBox1.Text = file1;
			//textBox1.Text = outputPath;
			//MessageBox.Show(listBox1.Text);

			String Name, Type, Location, Message;

			Name = listBox1.Text; ;
			Type = "";
			Location = "";
			Message = "Good";

			listBox1.Items.Add(String.Format(stdDetails, Name, Type, Location, Message));
		}

		private void listBox1_DragEnter(object sender, DragEventArgs e)
		{
			//Drag and drop effect in windows
			if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
			{
				e.Effect = DragDropEffects.All;
			}
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			
		}

		private void progressBar1_Click(object sender, EventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void label3_Click(object sender, EventArgs e)
		{

		}
	}
}
