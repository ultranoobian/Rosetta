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
                        qtyClassifierValues.Add(input);
                        break;
                    case ColumnType.Manufacturer:
                        qtyClassifierValues.Add(input);
                        break;
                    case ColumnType.PartNumber:
                        qtyClassifierValues.Add(input);
                        break;
                    case ColumnType.Designator:
                        qtyClassifierValues.Add(input);
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

        public Dictionary<ColumnType, double> Classify(IEnumerable<string> input)
        {
            Dictionary<CharacterFrequencyAnalyzer.Category, double> classifyingInput = CharacterFrequencyAnalyzer.CalculateRelativeAggregateFrequency(input);
            Dictionary<ColumnType, double> calculatedDelta = new Dictionary<ColumnType, double>();

            try
            {
                foreach (List<double> trainingData in qtyClassifierValues)
                {
                    calculatedDelta.Add(ColumnType.Quantity, CalculateHeuristicValue(classifyingInput, trainingData));
                }
                foreach (List<double> trainingData in designatorClassifierValues)
                {
                    calculatedDelta.Add(ColumnType.Designator, CalculateHeuristicValue(classifyingInput, trainingData));
                }
                foreach (List<double> trainingData in descriptionClassifierValues)
                {
                    calculatedDelta.Add(ColumnType.Description, CalculateHeuristicValue(classifyingInput, trainingData));
                }
                foreach (List<double> trainingData in mfgClassifierValues)
                {
                    calculatedDelta.Add(ColumnType.Manufacturer, CalculateHeuristicValue(classifyingInput, trainingData));
                }
                foreach (List<double> trainingData in mpnClassifierValues)
                {
                    calculatedDelta.Add(ColumnType.PartNumber, CalculateHeuristicValue(classifyingInput, trainingData));
                }
            }
            catch (Exception)
            {
            }
            return calculatedDelta;
        }

        private static double CalculateHeuristicValue(Dictionary<CharacterFrequencyAnalyzer.Category, double> a, List<double> n)
        {
            return a.Zip(CharacterFrequencyAnalyzer.CalculateRelativeAggregateFrequency(n), (b, c) =>
            {
                return (Math.Pow((b.Value - c.Value), 2) / c.Value);
            }).Sum();
        }
    }
}

