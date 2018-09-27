using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Tesseract;

namespace OCR
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                //var testImagePath = "./phototest.tif";
                var testImagePath = "./c.jpg";
                if (args.Length > 0)
                {
                    testImagePath = args[0];
                }

                try
                {
                    // "eng" refers to english
                    // EngineMode.Default refers to OCR engine
                    using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
                    {
                        using (var img = Pix.LoadFromFile(testImagePath))
                        {
                            using (var page = engine.Process(img))
                            {
                                var text = page.GetText();
                                var hocrtext = page.GetHOCRText(0, false);
                                Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());

                                Console.WriteLine("Text (GetText): \r\n{0}", text);
                                Console.WriteLine("Text (iterator):");
                                using (var iter = page.GetIterator())
                                {
                                    iter.Begin();

                                    do
                                    {
                                        do
                                        {
                                            do
                                            {
                                                do
                                                {
                                                    if (iter.IsAtBeginningOf(PageIteratorLevel.Block))
                                                    {
                                                        Console.WriteLine("<BLOCK>");
                                                    }

                                                    Console.Write(iter.GetText(PageIteratorLevel.Word));
                                                    Console.Write(" ");

                                                    if (iter.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word))
                                                    {
                                                        Console.WriteLine();
                                                    }
                                                } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                                                if (iter.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
                                                {
                                                    Console.WriteLine();
                                                }
                                            } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                                        } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                                    } while (iter.Next(PageIteratorLevel.Block));
                                }
                            }

                            // Segregated Processing within rectangle coordinates
                            using (var page = engine.Process(img, Rect.FromCoords(0, 0, 100, 100)))
                            {
                                // do something
                            }


                        }
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceError(e.ToString());
                    Console.WriteLine("Unexpected Error: " + e.Message);
                    Console.WriteLine("Details: ");
                    Console.WriteLine(e.ToString());
                }
                Console.Write("Press any key to continue . . . ");
                Console.ReadKey(true);
            }
        }
    }
}
