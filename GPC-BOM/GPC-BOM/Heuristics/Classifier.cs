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
                return new Classifier();
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
            //var a = CharacterFrequencyAnalyzer.AggregateCharacterFrequency(input.ToList());
            var a = CharacterFrequencyAnalyzer.CalculateRelativeAggregateFrequency(input);
            Dictionary<ColumnType, double> calculatedDelta = new Dictionary<ColumnType, double>();

            foreach (List<double> n in qtyClassifierValues)
            {
                //var result = a.Zip(CharacterFrequencyAnalyzer.AggregateCharacterFrequency(n), (b, c) => c - b);
                double result = a.Zip(CharacterFrequencyAnalyzer.CalculateRelativeAggregateFrequency(n), (b, c) =>
                {
                    return (Math.Pow((b.Value - c.Value), 2) / c.Value);
                }).Sum();

                calculatedDelta.Add(ColumnType.Quantity, result);

            }
            foreach (List<double> n in designatorClassifierValues)
            {
                double result = a.Zip(CharacterFrequencyAnalyzer.CalculateRelativeAggregateFrequency(n), (b, c) =>
                {
                    return (Math.Pow((b.Value - c.Value), 2) / c.Value);
                }).Sum();

                calculatedDelta.Add(ColumnType.Designator, result);
            }
            foreach (List<double> n in descriptionClassifierValues)
            {
                double result = a.Zip(CharacterFrequencyAnalyzer.CalculateRelativeAggregateFrequency(n), (b, c) =>
                {
                    return (Math.Pow((b.Value - c.Value), 2) / c.Value);
                }).Sum();

                calculatedDelta.Add(ColumnType.Description, result);
            }
            foreach (List<double> n in mfgClassifierValues)
            {
                double result = a.Zip(CharacterFrequencyAnalyzer.CalculateRelativeAggregateFrequency(n), (b, c) =>
                {
                    return (Math.Pow((b.Value - c.Value), 2) / c.Value);
                }).Sum();

                calculatedDelta.Add(ColumnType.Manufacturer, result);
            }
            foreach (List<double> n in mpnClassifierValues)
            {
                double result = a.Zip(CharacterFrequencyAnalyzer.CalculateRelativeAggregateFrequency(n), (b, c) =>
                {
                    return (Math.Pow((b.Value - c.Value), 2) / c.Value);
                }).Sum();

                calculatedDelta.Add(ColumnType.PartNumber, result);
            }

            return calculatedDelta;
        }
    }
}

