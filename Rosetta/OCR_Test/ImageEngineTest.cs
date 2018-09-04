using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCR;
using System.Drawing;


namespace OCR_Test
{
    [TestClass]
    public class ImageEngineTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //using(Stream imageStream)
            //System.Drawing.Bitmap testImage = new System.Drawing.Bitmap("testImage.jpg");
            ImageEngine ie = new ImageEngine();
            PrivateObject privateImageEngine = new PrivateObject(ie);

            //var returnValue = privateImageEngine.Invoke("ExtractTables", testImage);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Image testImage = Bitmap.FromFile("c.bmp");
            ImageEngine.ExtractTables(testImage);
        }
    }
}
