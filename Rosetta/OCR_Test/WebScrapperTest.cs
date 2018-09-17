using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCR;

namespace OCR_Test
{
    [TestClass]
    public class WebScrapperTest
    {
        [TestMethod]
        public void NaiveDigikey_Test()
        {
            Assert.AreEqual("CAP CER 1000PF 100V C0G 0603 ", WebScrapper.NaiveDigikey("C1608C0G2A102J080AA"));
        }

        [TestMethod]
        public void NaiveDigikey_Test2()
        {
            Assert.AreEqual("DIODE ARRAY SCHOTTKY 30V SOT23 ", WebScrapper.NaiveDigikey("BAT54S, 215"));
        }
    }
}
