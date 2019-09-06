using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SampleLibrary
{
    public class MySQLManager : IDataBaseUtility
    {
        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public int Execute(string sql, Dictionary<string, dynamic> parameters)
        {
            throw new NotImplementedException();
        }

        public DataTable Fill(string sql, Dictionary<string, dynamic> parameters)
        {
            throw new NotImplementedException();
        }

        public void RollBack()
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    RollBack();
                    Close();
                }

                // TODO: 大きなフィールドを null に設定します。

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
