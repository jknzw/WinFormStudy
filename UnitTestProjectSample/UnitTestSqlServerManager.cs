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
            using (SQLServerUtility mgr = new SQLServerUtility())
            {
                mgr.Connect();
                mgr.BeginTransaction();
            }
        }
    }
}
