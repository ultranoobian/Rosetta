using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCR;

namespace OCR_Test
{
    [TestClass]
    public class TesseractTest
    {
        [TestMethod]
        public void TestConsume()
        {
            OCR.Utility.Consume("Test Resources/EX-01.pdf");
        }

        [TestMethod]
        public void TestConsume_image()
        {
            OCR.Utility.Consume_Image("Test Resources/EX3.png");
        }
    }
}
