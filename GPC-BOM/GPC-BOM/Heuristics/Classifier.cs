using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPC_BOM.Heuristics
{
    class Classifier
    {
        static Classifier instance = null;
        public enum ColumnType { Quanitity, Description, Manufacturer, PartNumber, Designator };

        List<List<double>> qtyClassifierValues = new List<List<double>>();
        List<List<double>> designatorClassifierValues = new List<List<double>>();
        List<List<double>> descriptionClassifierValues = new List<List<double>>();
        List<List<double>> mfgClassifierValues = new List<List<double>>();
        List<List<double>> mpnClassifierValues = new List<List<double>>();

        public Classifier()
        {
        }

        public Classifier getInstance()
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                return null;
            }
        }

        public void loadQuantityFrequency(List<double> input)
        {
            if (input.Count > 0)
            {
                qtyClassifierValues.Add(input);
            }
        }
    }
}

