using Microsoft.VisualStudio.TestTools.UnitTesting;
using GPC_BOM.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPC_BOM.Heuristics.Tests
{
    [TestClass()]
    public class ClassifierTests
    {

        public static void LoadFrequencyValues(Classifier classifier)
        {
            throw new NotImplementedException();
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