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
            OCR.PDFiumEngine pdfEngine;
            pdfEngine = new PDFiumEngine("Test Resources/EX-01.pdf");
            pdfEngine = new PDFiumEngine("Test Resources/EX-02.pdf");
        }

        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public void InitializationTest_ExceptException()
        {
            OCR.PDFiumEngine pdfEngine = new PDFiumEngine("Test Resources/testPDF_nonexistent.pdf");

        }

        [TestMethod]
        public void PageCountTest()
        {
            OCR.PDFiumEngine pdfEngine = new PDFiumEngine("Test Resources/EX-01.pdf");
            Assert.AreEqual(3, pdfEngine.PageCount);
        }

        [TestMethod]
        public void PageCountTest_2()
        {
            OCR.PDFiumEngine pdfEngine = new PDFiumEngine("Test Resources/EX-02.pdf");
            Assert.AreEqual(11, pdfEngine.PageCount);
        }

        [TestMethod]
        public void RenderTest_default()
        {
            OCR.PDFiumEngine pdfEngine = new PDFiumEngine("Test Resources/EX-01.pdf");
            using (Image img = pdfEngine.GetImageFromPageNumber(0))
            {
                Assert.AreEqual(500, img.Height);
            }
        }

        [TestMethod]
        public void RenderTest_8000_width()
        {
            OCR.PDFiumEngine pdfEngine = new PDFiumEngine("Test Resources/EX-02.pdf");
            using (Image img = pdfEngine.GetImageFromPageNumber(0, 8000, 0))
            {
                Assert.AreEqual(8000, img.Width);
            }

        }


        [TestMethod]
        public void RenderTest_4000_width()
        {
            OCR.PDFiumEngine pdfEngine = new PDFiumEngine("Test Resources/EX-01.pdf");
            using (Image img = pdfEngine.GetImageFromPageNumber(0, 4000, 0))
            {
                Assert.AreEqual(4000, img.Width);
            }

        }

        [TestMethod]
        public void RenderTest_8000_width_2()
        {
            OCR.PDFiumEngine pdfEngine = new PDFiumEngine("Test Resources/EX-01.pdf");
            using (Image img = pdfEngine.GetImageFromPageNumber(0, 8000, 0))
            {
                Assert.AreEqual(8000, img.Width);
            }

        }


        [TestMethod]
        public void RenderTest_4000_width_2()
        {
            OCR.PDFiumEngine pdfEngine = new PDFiumEngine("Test Resources/EX-02.pdf");
            using (Image img = pdfEngine.GetImageFromPageNumber(0, 4000, 0))
            {
                Assert.AreEqual(4000, img.Width);
            }

        }

        [TestMethod]
        public void RenderTest_4000_width_4000_height()
        {
            OCR.PDFiumEngine pdfEngine = new PDFiumEngine("Test Resources/EX-01.pdf");
            using (Image img = pdfEngine.GetImageFromPageNumber(0, 4000, 4000))
            {
                Assert.AreEqual(4000, img.Width);
                Assert.AreEqual(4000, img.Height);
            }

        }

        [TestMethod]
        public void RenderTest_4000_width_4000_height_2()
        {
            OCR.PDFiumEngine pdfEngine = new PDFiumEngine("Test Resources/EX-02.pdf");
            using (Image img = pdfEngine.GetImageFromPageNumber(0, 4000, 4000))
            {
                Assert.AreEqual(4000, img.Width);
                Assert.AreEqual(4000, img.Height);
            }

        }
    }
}

