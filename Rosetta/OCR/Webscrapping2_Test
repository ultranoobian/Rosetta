using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApp1;

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

    [TestClass]
    public class WebScrapperTest2
    {
        [TestMethod]
        public void NaiveRS_Test()
        {
            Assert.AreEqual("TE Connectivity CR2032 PCB Battery Holder, Leaf Spring Contact ", WebScrapper.NaiveRS("796136-1"));

        }
        [TestMethod]
        public void NaiveRS_Test2()
        {
            Assert.AreEqual("Murata Ferrite Bead (Chip Ferrite Bead), 1 x 0.5 x 0.5mm (0402 (1005M)), 220Ω impedance at 100 MHz", WebScrapper.NaiveRS("BLM15BD221SN1D"));
        }

    }

    [TestClass]
    public class WebScrapperTest3
    {
        [TestMethod]
        public void NaiveElement14_Test()
        {
            Assert.AreEqual("YAGEO - SMD Chip Resistor, 2.2 kohm, RC Series, 75 V, Thick Film, 0603 [1608 Metric], 100 mW ", WebScrapper.NaiveElement14("RC0603FR-072K2L"));

        }
        /*
        [TestMethod]
        public void NaiveElement14_Test2()
        {
            Assert.AreEqual("Murata Ferrite Bead (Chip Ferrite Bead), 1 x 0.5 x 0.5mm (0402 (1005M)), 220Ω impedance at 100 MHz", WebScrapper.NaiveElement14("BLM15BD221SN1D"));
        }
        */

    }
    
    [TestClass]
    public class WebScrapperTest4
    {
        [TestMethod]
        public void NaiveMouser_Test()
        {
            Assert.AreEqual("Multilayer Ceramic Capacitors MLCC - SMD/SMT 0402 4.7uF 6.3volts *Derate Voltage/Temp", WebScrapper.NaiveMouser("GRM155R60J475ME87D"));

        }
        /*
        [TestMethod]
        public void NaiveMouser_Test2()
        {
            Assert.AreEqual("Murata Ferrite Bead (Chip Ferrite Bead), 1 x 0.5 x 0.5mm (0402 (1005M)), 220Ω impedance at 100 MHz", WebScrapper.NaiveRS("BLM15BD221SN1D"));
        }
        */
    }
    

}
