using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCR;
using System.Drawing;


namespace OCR_Test
{
    [TestClass]
    public class ImageEngineTest
    {
        [TestMethod]
        public void BasicTable_Test()
        {
            Image testImage = Bitmap.FromFile("Test Resources/EX-01-8000w.png");
            Assert.AreEqual(1, ImageEngine.ExtractTables(testImage));
        }

        [TestMethod]
        public void BasicTable_Test2()
        {
            Image testImage = Bitmap.FromFile("Test Resources/EX-02-8000w.png");
            Assert.AreEqual(1, ImageEngine.ExtractTables(testImage));
        }

    }
}
