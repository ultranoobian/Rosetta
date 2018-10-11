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
        [TestMethod()]
        public void GetInstanceAnalyze_EmptySetZero()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void AddFrequencyValueAnalyze_EmptySetZero()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void AddFrequencyValueAnalyze_EmptySetZero1()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ClassifyAnalyze_EmptySetZero()
        {
            Classifier clf = Classifier.GetInstance();
            PrivateObject privateObject = new PrivateObject(clf);
            var a = (List<List<double>>)privateObject.GetField("qtyClassifierValues");
            int i = a.Count;
        }
    }
}