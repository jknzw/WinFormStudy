using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SampleLibrary
{
    class DataBaseService : IDataBaseService
    {
        public int Delete(string sql, List<(string key, dynamic value)> param)
        {
            throw new NotImplementedException();
        }

        public int Insert(string sql, List<(string key, dynamic value)> param)
        {
            throw new NotImplementedException();
        }

        public DataTable Lock(string sql, List<(string key, dynamic value)> param)
        {
            throw new NotImplementedException();
        }

        public DataTable Search(string sql, List<(string key, dynamic value)> param)
        {
            throw new NotImplementedException();
        }

        public int Update(string sql, List<(string key, dynamic value)> param)
        {
            throw new NotImplementedException();
        }
    }
}
