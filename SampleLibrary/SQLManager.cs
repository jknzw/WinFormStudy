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

        private IDataBaseUtility mgr;
        private readonly Logger logger;

        private SQLManager()
        {
            logger = Logger.GetInstance(GetType().Name);
        }
        public static SQLManager GetInstance(DataBaseType type)
        {
            SQLManager service = new SQLManager();
            service.logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            switch (type)
            {
                case DataBaseType.SQLServer:
                default:
                    service.mgr = new SQLServerUtility();
                    break;
            }

            service.mgr.Connect();
            service.mgr.BeginTransaction();

            service.logger.EndMethod(MethodBase.GetCurrentMethod().Name);
            return service;
        }

        public int Insert(string sql, Dictionary<string, dynamic> parameters)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            return Update(sql, parameters);
        }

        public DataTable Search(string sql, Dictionary<string, dynamic> parameters)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            throw new NotImplementedException();
        }
        public DataTable Lock(string sql, Dictionary<string, dynamic> parameters)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            throw new NotImplementedException();
        }

        public int Update(string sql, Dictionary<string, dynamic> parameters)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            return mgr.Execute(sql, parameters);
        }

        public int Delete(string sql, Dictionary<string, dynamic> parameters)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            return Update(sql, parameters);
        }

        public int Call(string sql, Dictionary<string, dynamic> parameters)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            return Update(sql, parameters);
        }

        public void Commit()
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            mgr.Commit();
            mgr.BeginTransaction();
        }

        public void RollBack()
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            mgr.RollBack();
            mgr.BeginTransaction();
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
                    mgr.Dispose();

                    logger.Dispose();
                }

                // アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // 大きなフィールドを null に設定します。
                mgr = null;

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
