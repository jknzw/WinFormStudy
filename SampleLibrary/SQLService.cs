using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace SampleLibrary
{
    public class SQLService : ISQLService
    {
        public enum DataBaseType
        {
            SQLServer,
        }

        private IDataBaseManager mgr;
        private Logger logger;

        private SQLService()
        {
            logger = Logger.GetInstance(GetType().Name);
        }
        public static SQLService GetInstance(DataBaseType type)
        {
            SQLService service = new SQLService();
            service.logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            switch (type)
            {
                case DataBaseType.SQLServer:
                default:
                    service.mgr = new SQLServerManager();
                    break;
            }

            service.mgr.Connect();
            service.mgr.BeginTransaction();

            service.logger.EndMethod(MethodBase.GetCurrentMethod().Name);
            return service;
        }

        public int Delete(string sql, List<(string key, dynamic value)> param)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            throw new NotImplementedException();
        }

        public int Insert(string sql, List<(string key, dynamic value)> param)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            throw new NotImplementedException();
        }

        public DataTable Lock(string sql, List<(string key, dynamic value)> param)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            throw new NotImplementedException();
        }

        public DataTable Search(string sql, List<(string key, dynamic value)> param)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            throw new NotImplementedException();
        }

        public int Update(string sql, List<(string key, dynamic value)> param)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            throw new NotImplementedException();
        }

        public int Call(string sql, List<(string key, dynamic value)> param)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            return 0;
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
