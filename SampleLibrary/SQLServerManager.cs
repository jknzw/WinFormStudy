using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace SampleLibrary
{
    public class SQLServerManager : IDataBaseManager
    {
        private readonly string dataSource = "localhost";
        private readonly string dataBase = "SampleDb";
        private readonly string userId = "SampleUser";
        private readonly string password = "1234SampleUser";

        private SqlConnection con;
        private SqlTransaction tran;

        private readonly Logger logger = Logger.GetInstance(nameof(SQLServerManager));
        public SQLServerManager()
        {
        }

        public void BeginTransaction()
        {
            tran = con.BeginTransaction();
        }

        public void Commit()
        {
            tran.Commit();
        }

        public void Connect()
        {
            string conStr = $@"Data Source={dataSource};"
                + $@"Initial Catalog={dataBase};"
                + $@"Connect Timeout=60;Persist Security Info=True;"
                + $@"User ID={userId};Password={password}";

            con = new SqlConnection(conStr);

            con.Open();
        }

        public int Execute()
        {
            throw new NotImplementedException();
        }

        public DataTable Fill()
        {
            throw new NotImplementedException();
        }

        public void RollBack()
        {
            tran.Rollback();
        }
        public void Close()
        {
            tran.Rollback();
            tran.Dispose();
            con.Close();
            con.Dispose();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Close();

                    logger.Dispose();
                }

                // 大きなフィールドを null に設定します。
                tran = null;
                con = null;

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
