using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPC_BOM.Heuristics
{
    public static class CharacterFrequencyAnalyzer
    {
        public enum Category { SPACE, COMMA, PUNCTUATION, DIGITS, UPPERCASE, LOWERCASE };

        public static Dictionary<int, int> Analyze(List<string> input)
        {
            Dictionary<int, int> charFrequencyObservations = new Dictionary<int, int>();
            for (int i = 0; i < 128; i++)
            {
                charFrequencyObservations.Add(i, 0);
            }

            if (input != null)
            {
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
            }

            return charFrequencyObservations;
        }

        public static Dictionary<int, decimal> CalculateHueristicValue()
        {
            throw new NotImplementedException();
        }

        public static Dictionary<Category, int> AggregateCharacterFrequency(List<string> input)
        {
            var result = Analyze(input);
            Dictionary<Category, int> categoricalFrequency = new Dictionary<Category, int>();
            foreach (Category c in Enum.GetValues(typeof(Category)))
            {
                categoricalFrequency.Add(c, 0);
            }

            foreach (KeyValuePair<int, int> keyPair in result)
            {
                // Punctuation & Punctuation Specific values
                if (keyPair.Key == 32)
                {
                    categoricalFrequency[Category.SPACE] += keyPair.Value;
                }
                if (keyPair.Key == 44)
                {
                    categoricalFrequency[Category.COMMA] += keyPair.Value;
                }
                if (keyPair.Key >= 32 && keyPair.Key <= 47)
                {
                    categoricalFrequency[Category.PUNCTUATION]++;
                }

                // Uppercase Characters && Lowercase Characters
                if (keyPair.Key >= 65)
                {
                    if (keyPair.Key <= 90)
                    {
                        categoricalFrequency[Category.UPPERCASE] += keyPair.Value;
                    }
                    else if (keyPair.Key <= 122)
                    {
                        categoricalFrequency[Category.LOWERCASE] += keyPair.Value;
                    }
                }

            }

            return categoricalFrequency;
        }

    }

}
