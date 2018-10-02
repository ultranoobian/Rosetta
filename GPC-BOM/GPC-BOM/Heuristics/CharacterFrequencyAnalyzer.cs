using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPC_BOM.Heuristics
{
    public static class CharacterFrequencyAnalyzer
    {
        private enum Category { SPACE, COMMA, PUNCTUATION, DIGITS, UPPERCASE, LOWERCASE};

        public static Dictionary<int, int> Analyze(List<string> input)
        {
            Dictionary<int, int> charFrequencyObservations = new Dictionary<int, int>();
            for (int i = 0; i < 128; i++)
            {
                charFrequencyObservations.Add(i, 0);
            }

            foreach (string s in input)
            {
                foreach (char c in s)
                {
                    if (charFrequencyObservations.TryGetValue(c, out int foundValue))
                    {
                        charFrequencyObservations[c]++;
                    }
                }
            }

            return charFrequencyObservations;
        }

        public static Dictionary<int, decimal> CalculateHueristicValue()
        {
            throw new NotImplementedException();
        }

        private static Dictionary<Category, int> AggregateCharacterFrequency(List<string> input)
        {
            throw new NotImplementedException();
        }

    }

}
