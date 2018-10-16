using Microsoft.VisualStudio.TestTools.UnitTesting;
using GPC_BOM.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace GPC_BOM.Heuristics.Tests
{
    [TestClass()]
    public class ClassifierTests
    {

        public static void LoadFrequencyValues(Classifier classifier)
        {

            using (StreamReader reader = new StreamReader("Resources/training_combined.txt"))
            {
                string content = reader.ReadToEnd();
                List<string> lines = content.Split('\n').ToList();
                List<List<string>> subLines = new List<List<string>>();
                lines.ForEach(s => subLines.Add(s.Split(',').ToList()));

                foreach (List<string> items in subLines)
                {
                    string category = items[0];
                    List<double> frequencyValues = new List<double>();

                    items.GetRange(1, items.Count - 1).ForEach(str => frequencyValues.Add(Convert.ToDouble(str)));

                    switch (category)
                    {
                        case "designator":
                            classifier.AddFrequencyValue(Classifier.ColumnType.Designator, frequencyValues);
                            break;
                        case "manufactuer":
                            classifier.AddFrequencyValue(Classifier.ColumnType.Manufacturer, frequencyValues);
                            break;
                        case "mpn":
                            classifier.AddFrequencyValue(Classifier.ColumnType.PartNumber, frequencyValues);
                            break;
                        case "qty":
                            classifier.AddFrequencyValue(Classifier.ColumnType.Quantity, frequencyValues);
                            break;
                        case "description":
                            classifier.AddFrequencyValue(Classifier.ColumnType.Description, frequencyValues);
                            break;
                        default:
                            continue;
                    }
                }


            }

        }

        [TestMethod()]
        public void GetInstance_NewInstance()
        {
            Classifier clf = Classifier.GetInstance();
            Assert.IsNotNull(clf);

            Classifier clf2 = Classifier.GetInstance();
            Assert.IsNotNull(clf);
            Assert.AreSame(clf, clf2);
        }

        [TestMethod()]
        public void ClassifyAnalyze_EmptySetZero()
        {
            Classifier clf = Classifier.GetInstance();
            PrivateObject privateObject = new PrivateObject(clf);
            var a = (List<List<double>>)privateObject.GetField("qtyClassifierValues");
            int i = a.Count;
        }

        [TestMethod()]
        public void ClassificationTest_Classify_Quantity()
        {
            const Classifier.ColumnType testColumn = Classifier.ColumnType.Quantity;
            Classifier clf = Classifier.GetInstance();
            Assert.IsNotNull(clf);

            LoadFrequencyValues(clf);

            var result = clf.Classify(new List<string>()
            {
                "1", "2", "3", "4", "5" ,"6"
            });

            Assert.AreEqual(testColumn, result);
        }
    }
}