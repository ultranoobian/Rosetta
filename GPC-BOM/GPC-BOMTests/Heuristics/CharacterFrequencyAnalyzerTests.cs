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
        public void AnalyzeTest_SingleSet_UpperCase()
        {
            Dictionary<int, int> result = CharacterFrequencyAnalyzer.Analyze(new List<string> { "ABC" });

            Assert.AreEqual(1, result['A']);
            Assert.AreEqual(1, result['B']);
            Assert.AreEqual(1, result['C']);

            // Select where element(s) e are NOT(A,B or C)
            IEnumerable<int> allOtherCharacters = result.Where(e => !(e.Key == 'A' || e.Key == 'B' || e.Key == 'C')).Select(e => e.Value);
            Assert.IsTrue(allOtherCharacters.All(e => e == 0));
        }

        [TestMethod()]
        public void AnalyzeTest_SingleSet_LowerCase()
        {
            Dictionary<int, int> result = CharacterFrequencyAnalyzer.Analyze(new List<string> { "abc" });

            Assert.AreEqual(1, result['a']);
            Assert.AreEqual(1, result['b']);
            Assert.AreEqual(1, result['c']);

            // Select where element(s) e are NOT(a,b or c)
            IEnumerable<int> allOtherCharacters = result.Where(e => !(e.Key == 'a' || e.Key == 'b' || e.Key == 'c')).Select(e => e.Value);
            Assert.IsTrue(allOtherCharacters.All(e => e == 0));
        }

        [TestMethod()]
        public void AnalyzeTest_SingleSet_Digits()
        {
            Dictionary<int, int> result = CharacterFrequencyAnalyzer.Analyze(new List<string> { "123" });

            Assert.AreEqual(1, result['1']);
            Assert.AreEqual(1, result['2']);
            Assert.AreEqual(1, result['3']);

            // Select where element(s) e are NOT(a,b or c)
            IEnumerable<int> allOtherCharacters = result.Where(e => !(e.Key == '1' || e.Key == '2' || e.Key == '3')).Select(e => e.Value);
            Assert.IsTrue(allOtherCharacters.All(e => e == 0));
        }

        [TestMethod()]
        public void AnalyzeTest_SingleSet_Symbols()
        {
            List<string> input = new List<string> { ",./;'", "a!" };
            Dictionary<int, int> result = CharacterFrequencyAnalyzer.Analyze(input);

            Assert.AreEqual(1, result[',']);
            Assert.AreEqual(1, result['.']);
            Assert.AreEqual(1, result['/']);
            Assert.AreEqual(1, result[';']);
            Assert.AreEqual(1, result['\'']);


            // Select where element(s) e are NOT(a,b or c)
            IEnumerable<int> allOtherCharacters = result.Where(e => input.Any(s => s.Contains(e.Key.ToString())))
                .Select(e => e.Value);
            Assert.IsTrue(allOtherCharacters.All(e => e == 0));
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
            Assert.AreEqual(5, result[CharacterFrequencyAnalyzer.Category.Uppercase]);
        }

        [TestMethod()]
        public void CalculateRelativeAggregateFrequencyAnalyze_Double_EmptySetZero()
        {
            var result = Heuristics.CharacterFrequencyAnalyzer.CalculateRelativeAggregateFrequency(new List<double>());
            Assert.IsTrue(result.All(e => e.Value == 0));
        }

        [TestMethod()]
        public void CalculateRelativeAggregateFrequencyAnalyze_Double_SingleSetZero()
        {
            List<double> input = new List<double>();
            for (int i = 0; i < 128; i++)
            {
                input.Add(0.0d);
            }
            var result = Heuristics.CharacterFrequencyAnalyzer.CalculateRelativeAggregateFrequency(input);
            Assert.IsTrue(result.All(e => e.Value == 0));
        }

    
        [TestMethod()]
        public void CalculateRelativeAggregateFrequencyAnalyze_Double_SingleSet_Digits()
        {
            List<double> input = new List<double>();
            for (int i = 0; i < 128; i++)
            {
                input.Add(0.0d);
            }
            for(int i = 48; i <= 57; i++)
            {
                input[i] = 1.0d;
            }

            var result = Heuristics.CharacterFrequencyAnalyzer.CalculateRelativeAggregateFrequency(input);
            Assert.AreEqual(1, result[CharacterFrequencyAnalyzer.Category.Numbers]);
        }

        [TestMethod()]
        public void CalculateRelativeAggregateFrequencyAnalyze_String_EmptySetZero()
        {
            var result = Heuristics.CharacterFrequencyAnalyzer.CalculateRelativeAggregateFrequency(new List<string>());
            Assert.IsTrue(result.All(e => e.Value == 0));
        }

        [TestMethod()]
        public void CalculateRelativeAggregateFrequencyAnalyze_String_SingleSetZero()
        {
            List<string> input = new List<string>() { "" };
            var result = Heuristics.CharacterFrequencyAnalyzer.CalculateRelativeAggregateFrequency(input);
            Assert.IsTrue(result.All(e => e.Value == 0));
        }

        [TestMethod()]
        public void CalculateRelativeAggregateFrequencyAnalyze_String_SingleSet_Digits()
        {
            List<string> input = new List<string>() { "0123456789" };
            var result = Heuristics.CharacterFrequencyAnalyzer.CalculateRelativeAggregateFrequency(input);
            Assert.AreEqual(1, result[CharacterFrequencyAnalyzer.Category.Numbers]);
        }

    }
}