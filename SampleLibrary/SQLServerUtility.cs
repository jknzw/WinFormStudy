using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace SampleLibrary
{
    public class SQLServerUtility : IDataBaseUtility
    {
        private readonly string dataSource = "localhost";
        private readonly string dataBase = "SampleDb";
        private readonly string userId = "SampleUser";
        private readonly string password = "1234SampleUser";

        private SqlConnection con;
        private SqlTransaction tran;

        private readonly Logger logger;

        public SQLServerUtility()
        {
            logger = Logger.GetInstance(nameof(SQLServerUtility));
        }

        public void BeginTransaction()
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            tran = con.BeginTransaction();
        }

        public void Commit()
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            tran.Commit();
        }

        public void Connect()
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            string conStr = $@"Data Source={dataSource};"
                + $@"Initial Catalog={dataBase};"
                + $@"Connect Timeout=60;Persist Security Info=True;"
                + $@"User ID={userId};Password={password}";

            con = new SqlConnection(conStr);

            con.Open();
        }

        public int Execute(string sql, Dictionary<string, dynamic> parameters)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            // SQLの実行
            SqlCommand command = new SqlCommand(sql, con);

            foreach (string key in parameters.Keys)
            {
                SqlParameter parameter = new SqlParameter($"@{key}", parameters[key]);
                command.Parameters.Add(parameter);
            }

            return command.ExecuteNonQuery();
        }

        public DataTable Fill()
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            throw new NotImplementedException();
        }

        public void RollBack()
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            tran.Rollback();
        }
        public void Close()
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            tran.Rollback();
            tran.Dispose();
            con.Close();
            con.Dispose();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name, disposing.ToString());

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
