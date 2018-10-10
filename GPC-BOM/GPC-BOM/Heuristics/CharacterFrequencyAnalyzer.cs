using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPC_BOM.Heuristics
{
    public static class CharacterFrequencyAnalyzer
    {
        public enum Category { Space, Comma, Punctuation, Numbers, Uppercase, Lowercase };

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
        public static Dictionary<int, int> Analyze(IEnumerable<string> input)
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
                    categoricalFrequency[Category.Space] += keyPair.Value;
                }
                if (keyPair.Key == 44)
                {
                    categoricalFrequency[Category.Comma] += keyPair.Value;
                }
                if (keyPair.Key >= 32 && keyPair.Key <= 47)
                {
                    categoricalFrequency[Category.Punctuation]++;
                }

                // Numbers
                if (keyPair.Key >= 48 && keyPair.Key <= 57)
                {
                    categoricalFrequency[Category.Numbers] += keyPair.Value;
                }

                // Uppercase Characters && Lowercase Characters
                if (keyPair.Key >= 65)
                {
                    if (keyPair.Key <= 90)
                    {
                        categoricalFrequency[Category.Uppercase] += keyPair.Value;
                    }
                    else if (keyPair.Key <= 122)
                    {
                        categoricalFrequency[Category.Lowercase] += keyPair.Value;
                    }
                }

            }

            return categoricalFrequency;
        }

        public static Dictionary<Category, int> AggregateCharacterFrequency(Dictionary<int,int> input)
        {

            Dictionary<Category, int> categoricalFrequency = new Dictionary<Category, int>();
            foreach (Category c in Enum.GetValues(typeof(Category)))
            {
                categoricalFrequency.Add(c, 0);
            }

            foreach (KeyValuePair<int, int> keyPair in input)
            {
                // Punctuation & Punctuation Specific values
                if (keyPair.Key == 32)
                {
                    categoricalFrequency[Category.Space] += keyPair.Value;
                }
                if (keyPair.Key == 44)
                {
                    categoricalFrequency[Category.Comma] += keyPair.Value;
                }
                if (keyPair.Key >= 32 && keyPair.Key <= 47)
                {
                    categoricalFrequency[Category.Punctuation]++;
                }

                // Numbers
                if (keyPair.Key >= 48 && keyPair.Key <= 57)
                {
                    categoricalFrequency[Category.Numbers] += keyPair.Value;
                }

                // Uppercase Characters && Lowercase Characters
                if (keyPair.Key >= 65)
                {
                    if (keyPair.Key <= 90)
                    {
                        categoricalFrequency[Category.Uppercase] += keyPair.Value;
                    }
                    else if (keyPair.Key <= 122)
                    {
                        categoricalFrequency[Category.Lowercase] += keyPair.Value;
                    }
                }

            }

            return categoricalFrequency;
        }


        public static Dictionary<int, double> CalculateRelativeFrequency(IEnumerable<string> input)
        {
            Dictionary<int, double> frequency = new Dictionary<int, double>();
            Dictionary<int, int> f = Analyze(input);

            var totalCharCount = f.Sum(kp => kp.Value);
            foreach(KeyValuePair<int, int> kvp in f)
            {
                double relativeFrequency = (double)kvp.Value / totalCharCount;
                frequency.Add(kvp.Key, relativeFrequency);
            }

            return frequency;
        }

        public static Dictionary<Category, double> CalculateRelativeAggregateFrequency(IEnumerable<string> input)
        {
            Dictionary<Category, double> categoricalFrequency = new Dictionary<Category, double>();
            Dictionary<int, int> f = Analyze(input);
            var totalCharCount = f.Sum(kp => kp.Value);

            foreach (KeyValuePair<int, int> keyPair in f)
            {

                if (keyPair.Key == 32)
                {
                    categoricalFrequency[Category.Space] += keyPair.Value;
                }
                if (keyPair.Key == 44)
                {
                    categoricalFrequency[Category.Comma] += keyPair.Value;
                }
                if (keyPair.Key >= 32 && keyPair.Key <= 47)
                {
                    categoricalFrequency[Category.Punctuation]++;
                }

                // Numbers
                if (keyPair.Key >= 48 && keyPair.Key <= 57)
                {
                    categoricalFrequency[Category.Numbers] += keyPair.Value;
                }

                // Uppercase Characters && Lowercase Characters
                if (keyPair.Key >= 65)
                {
                    if (keyPair.Key <= 90)
                    {
                        categoricalFrequency[Category.Uppercase] += keyPair.Value;
                    }
                    else if (keyPair.Key <= 122)
                    {
                        categoricalFrequency[Category.Lowercase] += keyPair.Value;
                    }
                }
            }
            foreach(KeyValuePair<Category, double> keyPair in categoricalFrequency)
            {
                categoricalFrequency[keyPair.Key] = keyPair.Value / totalCharCount;
                
            }

            return categoricalFrequency;
        }

        public static Dictionary<Category, double> CalculateRelativeAggregateFrequency(IEnumerable<double> input)
        {
            Dictionary<Category, double> categoricalFrequency = new Dictionary<Category, double>();
            Dictionary<int, int> f = new Dictionary<int, int>();
            var totalCharCount = f.Sum(kp => kp.Value);

            for(int i = 0; i < input.Count(); i++)
            {

                if (i == 32)
                {
                    categoricalFrequency[Category.Space] += input.ElementAt(i);
                }
                if (i == 44)
                {
                    categoricalFrequency[Category.Comma] += input.ElementAt(i);
                }
                if (i >= 32 && i <= 47)
                {
                    categoricalFrequency[Category.Punctuation]++;
                }

                // Numbers
                if (i >= 48 && i <= 57)
                {
                    categoricalFrequency[Category.Numbers] += input.ElementAt(i);
                }

                // Uppercase Characters && Lowercase Characters
                if (i >= 65)
                {
                    if (i <= 90)
                    {
                        categoricalFrequency[Category.Uppercase] += input.ElementAt(i);
                    }
                    else if (i <= 122)
                    {
                        categoricalFrequency[Category.Lowercase] += input.ElementAt(i);
                    }
                }
            }
            foreach (KeyValuePair<Category, double> keyPair in categoricalFrequency)
            {
                categoricalFrequency[keyPair.Key] = keyPair.Value / totalCharCount;

            }

            return categoricalFrequency;
        }
    }

}
