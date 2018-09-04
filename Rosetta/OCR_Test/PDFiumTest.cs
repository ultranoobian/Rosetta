using OCR;
using System.Drawing;



using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace OCR_Test
{
    [TestClass]
    public class PDFiumTest
    {
        [TestMethod]
        public void InitializationTest_NoErrors()
        {
            OCR.PDFiumEngine pdfEngine = new PDFiumEngine("Test Resources/testPDF.pdf");
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void InitializationTest_ExceptException()
        {
            OCR.PDFiumEngine pdfEngine = new PDFiumEngine("Test Resources/testPDF_nonexistent.pdf");
            
        }

        [TestMethod]
        public void PageCountTest()
        {
            OCR.PDFiumEngine pdfEngine = new PDFiumEngine("Test Resources/testPDF.pdf");
            //Interesting format....
            //Assert.AreEqual(expected: 3, actual: pdfEngine.Count());
            Assert.AreEqual(3, pdfEngine.PageCount);
        }

        [TestMethod]
        public void RenderTest()
        {
            OCR.PDFiumEngine pdfEngine = new PDFiumEngine("Test Resources/testPDF.pdf");
            var img = pdfEngine.GetImageFromPageNumber(0);
            Bitmap bit = new Bitmap(img);
            bit.Save("a.jpg", ImageFormat.Jpeg);
                        
        }

        [TestMethod]
        public void Render2Test()
        {
            OCR.PDFiumEngine pdfEngine = new PDFiumEngine("Test Resources/testPDF.pdf");
            var img = pdfEngine.GetImageFromPageNumber(0, 8000,0);
            Bitmap bit = new Bitmap(img);
            bit.Save("b.jpg", ImageFormat.Jpeg);
            bit.Dispose();

        }


        [TestMethod]
        public void Render3Test()
        {
            OCR.PDFiumEngine pdfEngine = new PDFiumEngine("Test Resources/testPDF.pdf");
            var img = pdfEngine.GetImageFromPageNumber(0, 8000, 0);
            Bitmap bit = new Bitmap(img);
            bit.Save("c.bmp", ImageFormat.Bmp);
            bit.Dispose();

        }

        [TestMethod]
        public void Render4Test()
        {
            OCR.PDFiumEngine pdfEngine = new PDFiumEngine("Test Resources/testPDF3.pdf");
            //List<Image> images = new List<Image>();
            //for(int i = 0; i < pdfEngine.PageCount; i++)
            //{
            //    images.Add(pdfEngine.GetImageFromPageNumber(i, 8000, 0));
            //}
            
            Bitmap bit = new Bitmap(pdfEngine.GetImageFromPageNumber(0, 8000, 0));
            bit.Save("c.jpg", ImageFormat.Jpeg);
            bit.Dispose();

        }

        [TestMethod]
        public void Render5Test()
        {
            OCR.PDFiumEngine pdfEngine = new PDFiumEngine("Test Resources/testPDF4.pdf");

            Bitmap bit = new Bitmap(pdfEngine.GetImageFromPageNumber(0, 3000, 0));
            bit.Save("c.jpg", ImageFormat.Jpeg);
            bit.Dispose();

        }

    }
}

