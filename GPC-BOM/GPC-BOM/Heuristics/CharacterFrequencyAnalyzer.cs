﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPC_BOM.Heuristics
{
    public static class CharacterFrequencyAnalyzer
    {
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
    }

}
