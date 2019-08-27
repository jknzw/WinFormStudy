using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleLibrary;

namespace UnitTestProjectSample
{
    [TestClass]
    public class UnitTestSqlServerManager
    {
        [TestMethod]
        public void TestSQLServerConnect()
        {
            using (SQLServerManager mgr = new SQLServerManager())
            {
                mgr.Connect();
                mgr.BeginTransaction();
            }
        }
    }
}
