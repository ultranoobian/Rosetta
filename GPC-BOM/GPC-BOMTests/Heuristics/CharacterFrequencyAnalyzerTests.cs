using Microsoft.VisualStudio.TestTools.UnitTesting;
using GPC_BOM.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPC_BOM.Heuristics.Tests
{
    [TestClass()]
    public class CharacterFrequencyAnalyzerTests
    {
        [TestMethod()]
        public void AnalyzeTest_EmptyTestZero()
        {
            Dictionary<int, int> result = CharacterFrequencyAnalyzer.Analyze(new List<string> { "" });

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any(e => e.Value > 0));
        }

        [TestMethod()]
        public void AnalyzeTest_EmptySetNull()
        {
            Dictionary<int, int> result = GPC_BOM.Heuristics.CharacterFrequencyAnalyzer.Analyze(null);
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any(e => e.Value > 0));
        }

        [TestMethod()]
        public void AnalyzeTest_SingleSet()
        {
            Dictionary<int, int> result = CharacterFrequencyAnalyzer.Analyze(new List<string> { "ABC" });

            Assert.AreEqual(1, result['A']);
            Assert.AreEqual(1, result['B']);
            Assert.AreEqual(1, result['C']);
        }



        [TestMethod()]
        public void AggregateCharacterFrequency()
        {
            //throw new NotImplementedException();
        }

        [TestMethod()]
        public void AggregateCharacterFrequency_EmptyInput()
        {
            var result = Heuristics.CharacterFrequencyAnalyzer.AggregateCharacterFrequency(new List<string> { });
            Assert.IsNotNull(result);
            Assert.IsTrue(result.All(e => e.Value == 0));
        }

        [TestMethod()]
        public void AggregateCharacterFrequency_SingleInput()
        {
            var result = Heuristics.CharacterFrequencyAnalyzer.AggregateCharacterFrequency(new List<string> { "JARDO" });

            Assert.IsNotNull(result);
            Assert.AreEqual(5, result[CharacterFrequencyAnalyzer.Category.UPPERCASE]);
        }
    }
}