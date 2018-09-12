using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;


namespace excel
{
	class Excel
	{
		string path = "";
		_Application excel = new _Excel.Application();
		Workbook wb;
		Worksheet ws;
        public static int fullRow;
        public static int lastRow;
        public static int lvl_x = 0;
        public static int lvl_y = 0;
        public static int cpn_x = 0;
        public static int cpn_y = 0;
        public static int desc_x = 0;
        public static int desc_y = 0;
        public static int qty_x = 0;
        public static int qty_y = 0;
        public static int desi_x = 0;
        public static int desi_y = 0;
        public static int manf_x = 0;
        public static int manf_y = 0;
        public static int mpn_x = 0;
        public static int mpn_y = 0;
        public static int prcs_x = 0;
        public static int prcs_y = 0;
        public static int note_x = 0;
        public static int note_y = 0;


        public Excel()
		{
		}

		public Excel(String path, int sheet)
		{
			this.path = path;
			wb = excel.Workbooks.Open(path);
			ws = wb.Worksheets[sheet];
		}

		public void CreateNewFile()
		{
			this.wb = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
		}

		public string ReadCell(int i, int j)
		{
			i++;
			j++;
			if (ws.Cells[i, j].Value2 != null)
				return ws.Cells[i, j].Value2;
			else
				return "";
		}

		public string[,] ReadRange(int starti, int starty, int endi, int endy)
		{
			Range range = (Range)ws.Range[ws.Cells[starti, starty], ws.Cells[endi, endy]];
			object[,] holder = range.Value2;
			string[,] returnString = new string[endi - starti + 1, endy - starty + 1];
			for (int p = 1; p <= endi - starti + 1; p++)
			{
				for (int q = 1; q <= endy - starty + 1; q++)
				{
                    try
                    {
                        returnString[p - 1, q - 1] = holder[p, q].ToString();
                    }
                    catch(NullReferenceException ex)
                    {
                        Debug.Print("Object reference not set to an instance of an object");
                    }
				}

			}
			return returnString;
		}

		public void WriteRange(int starti, int starty, int endi, int endy, string[,] writestring)
		{
			Range range = (Range)ws.Range[ws.Cells[starti, starty], ws.Cells[endi, endy]];
			range.Value2 = writestring;

		}

        public void findEnd()
        {
            fullRow = ws.Rows.Count;
            lastRow = ws.Cells[fullRow,1].End(Microsoft.Office.Interop.Excel.XlDirection.xlUp).Row;
        }
        
        public void findField()
        {
            for(int i = 1; i < 10; i++)
            {
                for(int j = 1; j < 10; j++)
                {
                    if(listLevel(ws.Cells[i, j].Text.ToString()).CompareTo("Level") == 0)
                    {
                        lvl_x = i;
                        lvl_y = j;
                    }
                    else if (listCPN(ws.Cells[i, j].Text.ToString()).CompareTo("CPN") == 0)
                    {
                        cpn_x = i;
                        cpn_y = j;
                    }
                    else if(listDescription(ws.Cells[i, j].Text.ToString()).CompareTo("Description")== 0)
                    {
                        desc_x = i;
                        desc_y = j;
                    }
                    else if(listQuantity(ws.Cells[i, j].Text.ToString()).CompareTo("Quantity") == 0)
                    {
                        qty_x = i;
                        qty_y = j;
                    }
                    else if(listDesignator(ws.Cells[i, j].Text.ToString()).CompareTo("Designator") == 0)
                    {
                        desi_x = i;
                        desi_y = j;
                    }
                    else if(listManufacturer(ws.Cells[i, j].Text.ToString()).CompareTo("Manufacturer") == 0)
                    {
                        manf_x = i;
                        manf_y = j;
                    }
                    else if(listMPN(ws.Cells[i, j].Text.ToString()).CompareTo("MPN") == 0)
                    {
                        mpn_x = i;
                        mpn_y = j;
                    }
                    else if (listProcess(ws.Cells[i, j].Text.ToString()).CompareTo("Process") == 0)
                    {
                        prcs_x = i;
                        prcs_y = j;
                    }
                    else if (listNotes(ws.Cells[i, j].Text.ToString()).CompareTo("Notes") == 0)
                    {
                        note_x = i;
                        note_y = j;
                    }
                }
            }
        }
        /*
        public void styleField()
        {
            for (int i = 1; i < 10; i++)
            {
                ws.Cells[4, i].Style.Font.Bold = true;
                if (i < 3 || i > 7)
                {
                    //ws.Cells[4, i].Style.BackColor = System.Drawing.Color.FromName("PowderBlue");
                }
                else if(i >= 3 || i <= 7)
                {
                    //ws.Cells[4, i].Style.BackColor = System.Drawing.Color.FromName("IndianRed");
                }
            }
            ws.Cells.Style.Font.Bold = false;
        }
        */
        /*
        private void XlPrintErrors(string v)
        {
            throw new NotImplementedException();
        }
        */

        public string listLevel(string word)
        {
            if (word.CompareTo("Level") == 0 || word.CompareTo("LEVEL") == 0 || word.CompareTo("level") == 0
                || word.CompareTo("Lvl") == 0 || word.CompareTo("Lv") == 0 || word.CompareTo("Lv.") == 0)
            {
                return "Level";
            }
            return word;
        }

        public string listCPN(string word)
        {
            if (word.CompareTo("CPN") == 0 || word.CompareTo("Cpn") == 0 || word.CompareTo("cpn") == 0)
            {
                return "CPN";
            }
            return word;
        }

        public string listDescription(string word)
        {
            if (word.CompareTo("Description")==0||word.CompareTo("description")== 0||word.CompareTo("DESCRIPTION")==0
                ||word.CompareTo("Item Description")== 0||word.CompareTo("MATERIAL DESCRIPTION")==0||word.CompareTo("General Description")==0)
            {
                return "Description";
            }
            return word;
        }

        public string listQuantity(string word)
        {
            if (word.CompareTo("Quantity")==0||word.CompareTo("Qty")==0||word.CompareTo("QTY")==0
                ||word.CompareTo("Qty/Assy")==0||word.CompareTo("Qty per Assy")==0||word.CompareTo("E-BOM Quantity")==0)
            {
                return "Quantity";
            }
            return word;
        }

        public string listDesignator(string word)
        {
            if (word.CompareTo("Designator")==0||word.CompareTo("DESIGNATOR")==0||word.CompareTo("DESIGNATION")==0
                ||word.CompareTo("designator")==0||word.CompareTo("Ref Des")==0||word.CompareTo("Reference Des")==0
                ||word.CompareTo("Reference Position / Notes")==0||word.CompareTo("REF")==0||word.CompareTo("Ref Designators")==0
                ||word.CompareTo("PART-REFS")==0||word.CompareTo("LOCATION No.")==0||word.CompareTo("Circuit Code(EE)")==0
                ||word.CompareTo("LOCATION")==0)
            {
                return "Designator";
            }
            return word;
        }

        public string listManufacturer(string word)
        {
            if (word.CompareTo("Manufacturer")==0||word.CompareTo("MANUFACTURER")==0||word.CompareTo("Mfgr.Name")==0
                ||word.CompareTo("20-Primary Manuf")==0||word.CompareTo("MAKER")==0||word.CompareTo("Vendor")==0
                || word.CompareTo("Mfr  PNs") == 0)
            {
                return "Manufacturer";
            }
            return word;
        }

        public string listMPN(string word)
        {
            if (word.CompareTo("Part Number")==0||word.CompareTo("Mfgr.Nbr")==0||word.CompareTo("Manufacturer Part #")==0
                ||word.CompareTo("Manufacturer P/N")==0||word.CompareTo("MANUFACTURERS PART NUMBER")==0||word.CompareTo("21-Primary Manuf PN") ==0
                ||word.CompareTo("P/N") == 0 || word.CompareTo("PARTS No.") == 0 || word.CompareTo("COMPONENT P/N") == 0
                ||word.CompareTo("MPN")==0|| word.CompareTo("'21-Primary Manuf PN")==0||word.CompareTo("'21 - Primary Manuf PN")==0)
            {
                return "MPN";
            }
            return word;
        }
        public string listProcess(string word)
        {
            if (word.CompareTo("Process") == 0 || word.CompareTo("PROCESS") == 0 || word.CompareTo("process") == 0)
            {
                return "Process";
            }
            return word;
        }
        public string listNotes(string word)
        {
            if (word.CompareTo("Notes") == 0 || word.CompareTo("NOTES") == 0 || word.CompareTo("notes") == 0
                ||word.CompareTo("BOM Notes") == 0)
            {
                return "Notes";
            }
            return word;
        }

        public void WriteCell(int i, int j, string s)
		{
			ws.Cells[i, j].Value2 = s;
		}

		public void Save()
		{
			wb.Save();
		}

		public void SaveAs(string path)
		{
			wb.SaveAs(path);
		}

        public void Quit()
        {
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
        }

		public void Close()
		{
			wb.Close();
		}
	}
}
