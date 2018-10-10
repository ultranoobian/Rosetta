using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPC_BOM.Heuristics
{
    class Classifier
    {
        private static Classifier instance = null;
        public enum ColumnType { Description, Designator, Manufacturer, PartNumber, Quantity };

        private List<List<double>> qtyClassifierValues = new List<List<double>>();
        private List<List<double>> designatorClassifierValues = new List<List<double>>();
        private List<List<double>> descriptionClassifierValues = new List<List<double>>();
        private List<List<double>> mfgClassifierValues = new List<List<double>>();
        private List<List<double>> mpnClassifierValues = new List<List<double>>();

        public Classifier()
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

        public ColumnType Classify(IEnumerable<string> input)
        {
            throw new NotImplementedException();
        }
    }
}

