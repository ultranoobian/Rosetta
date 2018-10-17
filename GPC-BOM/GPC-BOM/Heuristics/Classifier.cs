using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPC_BOM.Heuristics
{
    public class Classifier
    {
        private static Classifier instance = null;
        public enum ColumnType { Description, Designator, Manufacturer, PartNumber, Quantity };

        private List<List<double>> qtyClassifierValues = new List<List<double>>();
        private List<List<double>> designatorClassifierValues = new List<List<double>>();
        private List<List<double>> descriptionClassifierValues = new List<List<double>>();
        private List<List<double>> mfgClassifierValues = new List<List<double>>();
        private List<List<double>> mpnClassifierValues = new List<List<double>>();

        private Classifier()
        {
        }

        public static Classifier GetInstance()
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                instance = new Classifier();
                return instance;
            }
        }

        public void AddFrequencyValue(ColumnType type, List<double> input)
        {
            if (input.Count > 0)
            {
                switch (type)
                {
                    case ColumnType.Quantity:
                        qtyClassifierValues.Add(input);
                        break;
                    case ColumnType.Description:
                        descriptionClassifierValues.Add(input);
                        break;
                    case ColumnType.Manufacturer:
                        mfgClassifierValues.Add(input);
                        break;
                    case ColumnType.PartNumber:
                        mpnClassifierValues.Add(input);
                        break;
                    case ColumnType.Designator:
                        designatorClassifierValues.Add(input);
                        break;
                    default:
                        break;
                }
            }
        }

        public void AddFrequencyValue(ColumnType type, List<List<double>> input)
        {
            foreach (List<double> sublist in input)
            {
                AddFrequencyValue(type, sublist);
            }
        }

        public List<HeuristicTuple> Classify(IEnumerable<string> input)
        {
            Dictionary<CharacterFrequencyAnalyzer.Category, double> classifyingInput = CharacterFrequencyAnalyzer.CalculateRelativeAggregateFrequency(input);
            List<HeuristicTuple> calculatedDeltas = new List<HeuristicTuple>();

            try
            {
                foreach (List<double> trainingData in qtyClassifierValues)
                {
                    calculatedDeltas.Add(new HeuristicTuple(ColumnType.Quantity, CalculateHeuristicValue(classifyingInput, trainingData)));
                }
                foreach (List<double> trainingData in designatorClassifierValues)
                {
                    calculatedDeltas.Add(new HeuristicTuple(ColumnType.Designator, CalculateHeuristicValue(classifyingInput, trainingData)));
                }
                foreach (List<double> trainingData in descriptionClassifierValues)
                {
                    calculatedDeltas.Add(new HeuristicTuple(ColumnType.Description, CalculateHeuristicValue(classifyingInput, trainingData)));
                }
                foreach (List<double> trainingData in mfgClassifierValues)
                {
                    calculatedDeltas.Add(new HeuristicTuple(ColumnType.Manufacturer, CalculateHeuristicValue(classifyingInput, trainingData)));
                }
                foreach (List<double> trainingData in mpnClassifierValues)
                {
                    calculatedDeltas.Add(new HeuristicTuple(ColumnType.PartNumber, CalculateHeuristicValue(classifyingInput, trainingData)));
                }

                calculatedDeltas.Sort();
            }

            catch (Exception)
            {
                throw;
            }
            return calculatedDeltas;
        }

        private static double CalculateHeuristicValue(Dictionary<CharacterFrequencyAnalyzer.Category, double> a, List<double> n)
        {
            return a.Zip(CharacterFrequencyAnalyzer.CalculateRelativeAggregateFrequency(n), (b, c) =>
            {
                return (Math.Pow((b.Value - c.Value), 2));
            }).Sum();
        }

        public class HeuristicTuple : IComparable<HeuristicTuple>
        {
            public readonly ColumnType columnType;
            public readonly double heuristicValue;

            internal HeuristicTuple(ColumnType columnType, double heuristicValue)
            {
                this.columnType = columnType;
                this.heuristicValue = heuristicValue;
            }

            public int CompareTo(HeuristicTuple other)
            {
                return this.heuristicValue.CompareTo(other.heuristicValue);
            }
        }
    }
}

