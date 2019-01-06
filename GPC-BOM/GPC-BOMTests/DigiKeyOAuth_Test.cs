using GPC_BOM;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GPC_BOM.Tests
{
    [TestClass()]
    public class DigiKeyOAuth_Test
    {
        [TestMethod()]
        public void authProcess_test()
        {
            DigiKeyOAuth auth = new DigiKeyOAuth();
            auth.authProcess();
            //throw new NotImplementedException();
        }
    }
}
