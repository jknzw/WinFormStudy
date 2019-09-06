using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace SampleLibrary
{
    public class SQLManager : ISQLManager
    {
        public enum DataBaseType
        {
            SQLServer,
        }

        private IDataBaseUtility dbUtil;
        private Logger logger;

        private SQLManager()
        {
            logger = Logger.GetInstance(GetType().Name);
        }

        public static SQLManager GetInstance(string dataSource, string dataBase, string userId, string password)
        {
            return GetInstance(DataBaseType.SQLServer, dataSource, dataBase, userId, password);
        }

        private static SQLManager GetInstance(DataBaseType type, string dataSource, string dataBase, string userId, string password)
        {
            SQLManager service = new SQLManager();
            service.logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            switch (type)
            {
                case DataBaseType.SQLServer:
                default:
                    service.dbUtil = new SQLServerUtility(dataSource, dataBase, userId, password);
                    break;
            }

            service.dbUtil.Connect();
            service.dbUtil.BeginTransaction();

            service.logger.EndMethod(MethodBase.GetCurrentMethod().Name);
            return service;
        }

        public int Insert(string sql, Dictionary<string, dynamic> parameters)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            return Update(sql, parameters);
        }

        public DataTable Select(string sql, Dictionary<string, dynamic> parameters)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            return dbUtil.Fill(sql, parameters);
        }
        public DataTable Lock(string sql, Dictionary<string, dynamic> parameters)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            return Select(sql, parameters);
        }

        public int Update(string sql, Dictionary<string, dynamic> parameters)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            return dbUtil.Execute(sql, parameters);
        }

        public int Delete(string sql, Dictionary<string, dynamic> parameters)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            return Update(sql, parameters);
        }

        public int Call(string sql, Dictionary<string, dynamic> parameters)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            // TODO:outパラメータを受け取る
            // TODO:outパラメータにDictionaryを追加する

            return Update(sql, parameters);
        }

        public void Commit()
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            dbUtil.Commit();
            dbUtil.BeginTransaction();
        }

        public void RollBack()
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            dbUtil.RollBack();
            dbUtil.BeginTransaction();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // マネージ状態を破棄します (マネージ オブジェクト)。
                    dbUtil?.RollBack();
                    dbUtil?.Dispose();

                    logger?.Dispose();
                }

                // アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // 大きなフィールドを null に設定します。
                dbUtil = null;
                logger = null;

                disposedValue = true;
            }
        }

        // 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        // ~SQLService()
        // {
        //   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
        //   Dispose(false);
        // }

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);
            // 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
