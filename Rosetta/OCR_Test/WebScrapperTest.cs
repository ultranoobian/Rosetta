using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCR;

namespace OCR.Tests
{
    [TestClass()]
    public class WebscrapperTest
    {
        [TestMethod()]
        public void API_MouserTest()
        {
            Assert.AreEqual("Crystals +/-20ppm 12MHZ", WebScrapper.MouserAPI_GetDescription("ABM3B-12.000MHZ-B2-T"));
        }

        [TestMethod()]
        public void Element14API_GetDescriptionTest()
        {
            Assert.AreEqual("ABRACON - ABM3B-12.000MHZ-B2-T - CRYSTAL, 12MHZ, 18PF, 5 X 3.2MM", WebScrapper.Element14API_GetDescription("ABM3B-12.000MHZ-B2-T"));
        }
    }
}

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
            Assert.AreEqual("Murata Ferrite Bead (Chip Ferrite Bead), 1 x 0.5 x 0.5mm (0402 (1005M)), 220Î© impedance at 100 MHz ", WebScrapper.NaiveRS("BLM15BD221SN1D"));
        }

        [TestMethod]
        public void NaiveRS_Test3()
        {
            Assert.AreEqual("Murata 10nF Multilayer Ceramic Capacitor MLCC 6.3V dc Â±10% X7R Dielectric 0402 (1005M) SMD, Max. Temp. +125Â°C ", WebScrapper.NaiveRS("GRM155R70J103KA01D"));
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
        
        [TestMethod]
        public void NaiveElement14_Test2()
        {
            Assert.AreEqual("MURATA - Ferrite Bead, 0402 [1005 Metric], 220 ohm, 300 mA, BLM15B Series, 0.4 ohm, ± 25%", WebScrapper.NaiveElement14("BLM15BD221SN1D"));
        }
        

    }

    [TestClass]
    public class WebScrapperTest4
    {
        [TestMethod]
        public void NaiveMouser_Test()
        {
            Assert.AreEqual("Multilayer Ceramic Capacitors MLCC - SMD/SMT 0402 4.7uF 6.3volts *Derate Voltage/Temp Learn More", WebScrapper.NaiveMouser("GRM155R60J475ME87D"));

        }
        
        [TestMethod]
        public void NaiveMouser_Test2()
        {
            Assert.AreEqual("Ferrite Beads 220 OHM HIGH SPEED SIGNAL Learn More", WebScrapper.NaiveMouser("BLM15BD221SN1D"));
        }

        [TestMethod]
        public void NaiveMouser_Test3()
        {
            Assert.AreEqual("Thick Film Resistors - SMD 2.2K OHM 1% Learn More", WebScrapper.NaiveMouser("RC0603FR-072K2L"));
        }

        [TestMethod]
        public void NaiveMouser_Test4()
        {
            Assert.AreEqual("Crystals +/-20ppm 12MHZ", WebScrapper.NaiveMouser("ABM3B-12.000MHZ-B2-T"));
        }

    }


}
