using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace OCR
{
    public class Tesseract : IDisposable
    {
        private static TesseractEngine engine;

        public static TesseractEngine getInstance()
        {
            if (engine != null)
            {
                return engine;
            }
            else
            {
                engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
                return engine;
            }
        }

        public void Dispose()
        {
            if (engine != null)
            {
                engine.Dispose();
            }
        }
    }

    public static class Utility
    {
        public static String Consume(String path)
        {
            string path_extension = Path.GetExtension(path);
            if (path_extension != ".pdf")
            {
                throw new ArgumentException("Path does not lead to a PDF file type", path);
            }
            using (PDFiumEngine pdfEngine = new PDFiumEngine(path))
            using (StreamWriter sw = new StreamWriter(String.Format("output_{0}.txt", Path.GetFileNameWithoutExtension(path), false)))
            {
                for (int i = 0; i < pdfEngine.PageCount; i++)
                {
                    Image img = pdfEngine.GetImageFromPageNumber(i, 4000, 0);
                    using (TesseractEngine engine = Tesseract.getInstance())
                    {
                        Bitmap bit = new Bitmap(img);
                        using (Page p = engine.Process(bit, PageSegMode.AutoOsd))
                        {
                            string text = p.GetText();
                            sw.WriteLine(text);
                            sw.Flush();
                        }
                    }
                }
                sw.Close();
            }

            return "";
        }

        public static String Consume_Image(String path)
        {
            using (StreamWriter sw = new StreamWriter(String.Format("output_{0}.txt", Path.GetFileNameWithoutExtension(path), false)))
            {
                using (TesseractEngine engine = Tesseract.getInstance())
                {
                    Bitmap bit = new Bitmap(path);
                    using (Page p = engine.Process(bit, PageSegMode.AutoOsd))
                    {
                        string text = p.GetText();
                        sw.WriteLine(text);
                        sw.Flush();
                    }
                }

                sw.Close();
            }
            return "";
        }
    }

}

