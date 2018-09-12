using System;
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
        public Form1()
		{
			InitializeComponent();
		}

        private void Form1_Load_1(object sender, EventArgs e)
		{

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
			Excel ex = new Excel(@"input", 1);
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
                Excel input = new Excel(@"input", 1);
                string[,] read = input.ReadRange(Excel.lvl_x + 1, Excel.lvl_y, Excel.lastRow, Excel.lvl_y);
                input.Close();
                input.Quit();

                Excel output = new Excel(@"output", 1);
                output.WriteRange(5, 1, Excel.lastRow + 3, 1, read);
                output.Save();
                output.Close();
                output.Quit();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Debug.Print("We can't save 'output.xlsx' because the file is read-only.");
            }
        }
        // scan "CPN"
        public void WriteCPN()
        {
            try
            {
                Excel input = new Excel(@"input", 1);
                string[,] read = input.ReadRange(Excel.cpn_x + 1, Excel.cpn_y, Excel.lastRow, Excel.cpn_y);
                input.Close();
                input.Quit();

                Excel output = new Excel(@"output", 1);
                output.WriteRange(5, 2, Excel.lastRow + 3, 2, read);
                output.Save();
                output.Close();
                output.Quit();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Debug.Print("We can't save 'output.xlsx' because the file is read-only.");
            }
        }
        // scan "Description"
        public void WriteDescription()
        {
            try
            {
                Excel input = new Excel(@"input", 1);
                // MessageBox.Show("desc_x: " + Excel.desc_x + "desc_y: " + Excel.desc_y);
                string[,] read = input.ReadRange(Excel.desc_x + 1, Excel.desc_y, Excel.lastRow, Excel.desc_y);
                input.Close();
                input.Quit();

                Excel output = new Excel(@"output", 1);
                output.WriteRange(5, 3, Excel.lastRow + 3, 3, read);
                output.Save();
                output.Close();
                output.Quit();
            }
            catch(System.Runtime.InteropServices.COMException ex)
            {
                Debug.Print("We can't save 'output.xlsx' because the file is read-only.");
            }
            
        }
        // scan "Quantity"
        public void WriteQuantity()
        {
            try
            {
                Excel input = new Excel(@"input", 1);
                string[,] read = input.ReadRange(Excel.qty_x + 1, Excel.qty_y, Excel.lastRow, Excel.qty_y);
                input.Close();
                input.Quit();

                Excel output = new Excel(@"output", 1);
                output.WriteRange(5, 4, Excel.lastRow + 3, 4, read);
                output.Save();
                output.Close();
                output.Quit();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Debug.Print("We can't save 'output.xlsx' because the file is read-only.");
            }
            
        }
        // scan "Designator"
        public void WriteDesignator()
        {
            try
            {
                Excel input = new Excel(@"input", 1);
                string[,] read = input.ReadRange(Excel.desi_x + 1, Excel.desi_y, Excel.lastRow, Excel.desi_y);
                input.Close();
                input.Quit();

                Excel output = new Excel(@"output", 1);
                output.WriteRange(5, 5, Excel.lastRow + 3, 5, read);
                output.Save();
                output.Close();
                output.Quit();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Debug.Print("We can't save 'output.xlsx' because the file is read-only.");
            }
            
        }
        //Scan "Manufacturer"
        public void WriteManufacturer()
        {
            try
            {
                Excel input = new Excel(@"input", 1);
                string[,] read = input.ReadRange(Excel.manf_x + 1, Excel.manf_y, Excel.lastRow, Excel.manf_y);
                input.Close();
                input.Quit();

                Excel output = new Excel(@"output", 1);
                output.WriteRange(5, 6, Excel.lastRow + 3, 6, read);
                output.Save();
                output.Close();
                output.Quit();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Debug.Print("We can't save 'output.xlsx' because the file is read-only.");
            }         
        }
        // scan "MPN"
        public void WriteMPN()
        {
            try
            {
                Excel input = new Excel(@"input", 1);
                string[,] read = input.ReadRange(Excel.mpn_x + 1, Excel.mpn_y, Excel.lastRow, Excel.mpn_y);
                input.Close();
                input.Quit();

                Excel output = new Excel(@"output", 1);
                output.WriteRange(5, 7, Excel.lastRow + 3, 7, read);
                output.Save();
                output.Close();
                output.Quit();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Debug.Print("We can't save 'output.xlsx' because the file is read-only.");
            }
        }
        // scan "Process"
        public void WriteProcess()
        {
            try
            {
                Excel input = new Excel(@"input", 1);
                string[,] read = input.ReadRange(Excel.prcs_x + 1, Excel.prcs_y, Excel.lastRow, Excel.prcs_y);
                input.Close();
                input.Quit();

                Excel output = new Excel(@"output", 1);
                output.WriteRange(5, 8, Excel.lastRow + 3, 8, read);
                output.Save();
                output.Close();
                output.Quit();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Debug.Print("We can't save 'output.xlsx' because the file is read-only.");
            }
        }
        // scan "CPN"
        public void WriteNotes()
        {
            try
            {
                Excel input = new Excel(@"input", 1);
                string[,] read = input.ReadRange(Excel.note_x + 1, Excel.note_y, Excel.lastRow, Excel.note_y);
                input.Close();
                input.Quit();

                Excel output = new Excel(@"output", 1);
                output.WriteRange(5, 9, Excel.lastRow + 3, 9, read);
                output.Save();
                output.Close();
                output.Quit();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Debug.Print("We can't save 'output.xlsx' because the file is read-only.");
            }
        }

        // create output file
        public void CreateOutput()
		{
            Excel excel = new Excel(@"output.xlsx", 1);
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
			Excel ex = new Excel();
			ex.CreateNewFile();
			ex.SaveAs(@"output");
			ex.Close();
            ex.Quit();
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
            timer1.Start();
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
            MessageBox.Show("Saved");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(20);
            label1.Text = progressBar1.Value.ToString() + "%";
        }
    }
}
