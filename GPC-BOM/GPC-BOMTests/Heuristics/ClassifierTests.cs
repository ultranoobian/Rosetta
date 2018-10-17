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
                        case "manufacturer":
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
        [ExpectedException(typeof(InvalidOperationException))]
        public void Classifier_NoData_ThrownException()
        {
            Classifier clf = Classifier.GetInstance();
            Assert.IsNotNull(clf);


            List<string> input = new List<string>()
            {
                "SomeDataHere"
            };
            clf.Classify(input);
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

            Assert.AreEqual(testColumn, result.Min().columnType);
            Assert.IsTrue(result.Count > 1);
        }

        [TestMethod()]
        public void ClassificationTest_Classify_MPN()
        {
            const Classifier.ColumnType testColumn = Classifier.ColumnType.PartNumber;
            Classifier clf = Classifier.GetInstance();
            Assert.IsNotNull(clf);

            LoadFrequencyValues(clf);

            var result = clf.Classify(new List<string>()
            {
                "C1608X7R1E105K080AB", "MC0805N100J201CT", "GRM2165C2A471JA01D", "VS-15MQ040NPBF", "EMVA250ADA101MF80G" ,"251R15S221JV4E"
            });

            Assert.AreEqual(testColumn, result.Min().columnType);
            Assert.IsTrue(result.Count > 1);
        }

        [TestMethod()]
        public void ClassificationTest_Classify_kNN_3_PartNumber()
        {
            const Classifier.ColumnType testColumn = Classifier.ColumnType.PartNumber;
            Classifier clf = Classifier.GetInstance();
            Assert.IsNotNull(clf);

            LoadFrequencyValues(clf);

            var result = clf.Classify_KNN(new List<string>()
            {
                "C1608X7R1E105K080AB", "MC0805N100J201CT", "GRM2165C2A471JA01D", "VS-15MQ040NPBF", "EMVA250ADA101MF80G" ,"251R15S221JV4E"
            }, 3);

            Assert.AreEqual(testColumn, result);
        }

        [TestMethod()]
        public void ClassificationTest_Classify_kNN_5_PartNumber()
        {
            const Classifier.ColumnType testColumn = Classifier.ColumnType.PartNumber;
            Classifier clf = Classifier.GetInstance();
            Assert.IsNotNull(clf);

            LoadFrequencyValues(clf);

            var result = clf.Classify_KNN(new List<string>()
            {
                "C1608X7R1E105K080AB", "MC0805N100J201CT", "GRM2165C2A471JA01D", "VS-15MQ040NPBF", "EMVA250ADA101MF80G" ,"251R15S221JV4E"
            }, 3);

            Assert.AreEqual(testColumn, result);
        }

        [TestMethod()]
        public void ClassificationTest_Classify_kNN_3_Manufacturer()
        {
            const Classifier.ColumnType testColumn = Classifier.ColumnType.Manufacturer;
            Classifier clf = Classifier.GetInstance();
            Assert.IsNotNull(clf);

            LoadFrequencyValues(clf);

            var result = clf.Classify_KNN(new List<string>()
            {
                "TI", "Murata", "AVX", "Keystone", "Microchip" ,"Crystek Corporation"
            }, 3);

            Assert.AreEqual(testColumn, result);
        }
        [TestMethod()]
        public void ClassificationTest_Classify_kNN_5_Manufacturer()
        {
            const Classifier.ColumnType testColumn = Classifier.ColumnType.Manufacturer;
            Classifier clf = Classifier.GetInstance();
            Assert.IsNotNull(clf);

            LoadFrequencyValues(clf);

            var result = clf.Classify_KNN(new List<string>()
            {
                "TI", "Murata", "AVX", "Keystone", "Microchip" ,"Crystek Corporation"
            }, 5);

            Assert.AreEqual(testColumn, result);
        }

        [TestMethod()]
        public void ClassificationTest_Classify_kNN_3_Designator()
        {
            const Classifier.ColumnType testColumn = Classifier.ColumnType.Designator;
            Classifier clf = Classifier.GetInstance();
            Assert.IsNotNull(clf);

            LoadFrequencyValues(clf);

            var result = clf.Classify_KNN(new List<string>()
            {
                "C2,C3,C14,C15,C22,C23,C28,C29,C48,C49,C61,C62,C74,C75,C86,C87,C94-C173,R22,R24,R36,R39,R65,R87,R90,R94",
                "T1,T2,T3,T4", "L19_QSFP0, L19_QSFP1, L22_QSFP0, L22_QSFP1, L23_QSFP0, L23_QSFP1"
            }, 3);

            Assert.AreEqual(testColumn, result);
        }
        [TestMethod()]
        public void ClassificationTest_Classify_kNN_5_Designator()
        {
            const Classifier.ColumnType testColumn = Classifier.ColumnType.Designator;
            Classifier clf = Classifier.GetInstance();
            Assert.IsNotNull(clf);

            LoadFrequencyValues(clf);

            var result = clf.Classify_KNN(new List<string>()
            {
                "C2,C3,C14,C15,C22,C23,C28,C29,C48,C49,C61,C62,C74,C75,C86,C87,C94-C173,R22,R24,R36,R39,R65,R87,R90,R94",
                "T1,T2,T3,T4", "L19_QSFP0, L19_QSFP1, L22_QSFP0, L22_QSFP1, L23_QSFP0, L23_QSFP1"
            }, 5);

            Assert.AreEqual(testColumn, result);
        }

        [TestMethod()]
        public void ClassificationTest_Classify_kNN_3_Description()
        {
            const Classifier.ColumnType testColumn = Classifier.ColumnType.Description;
            Classifier clf = Classifier.GetInstance();
            Assert.IsNotNull(clf);

            LoadFrequencyValues(clf);

            var result = clf.Classify_KNN(new List<string>()
            {
                "Ferrite Bead, 220R @ 100M", "Ferrite Bead, 220R @ 100M", "Header, 3 way, 1.25mm pitch", "Header, 14 way, 2mm pitch, SMT", "High Speed USB UART/FIFO"
            }, 3);

            Assert.AreEqual(testColumn, result);
        }
        [TestMethod()]
        public void ClassificationTest_Classify_kNN_5_Description()
        {
            const Classifier.ColumnType testColumn = Classifier.ColumnType.Description;
            Classifier clf = Classifier.GetInstance();
            Assert.IsNotNull(clf);

            LoadFrequencyValues(clf);

            var result = clf.Classify_KNN(new List<string>()
            {
                "Ferrite Bead, 220R @ 100M", "Ferrite Bead, 220R @ 100M", "Header, 3 way, 1.25mm pitch", "Header, 14 way, 2mm pitch, SMT", "High Speed USB UART/FIFO"
            }, 5);

            Assert.AreEqual(testColumn, result);
        }

        [TestMethod()]
        public void ClassificationTest_Classify_kNN_3_Quantity()
        {
            const Classifier.ColumnType testColumn = Classifier.ColumnType.Quantity;
            Classifier clf = Classifier.GetInstance();
            Assert.IsNotNull(clf);

            LoadFrequencyValues(clf);

            var result = clf.Classify_KNN(new List<string>()
            {
                "1","7","3","4","2","1","4","4","2","3","2","1"
            }, 3);

            Assert.AreEqual(testColumn, result);
        }
        [TestMethod()]
        public void ClassificationTest_Classify_kNN_5_Quantity()
        {
            const Classifier.ColumnType testColumn = Classifier.ColumnType.Quantity;
            Classifier clf = Classifier.GetInstance();
            Assert.IsNotNull(clf);

            LoadFrequencyValues(clf);

            var result = clf.Classify_KNN(new List<string>()
            {
                "1","7","3","4","2","1","4","4","2","3","2","1"
            }, 5);

            Assert.AreEqual(testColumn, result);
        }
    }
}