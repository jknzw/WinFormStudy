using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleLibrary
{
    public interface ISQLService
    {
        DataTable Search(string sql, List<(string key, dynamic value)> param);
        int Insert(string sql, List<(string key, dynamic value)> param);

        DataTable Lock(string sql, List<(string key, dynamic value)> param);
        int Update(string sql, List<(string key, dynamic value)> param);
        int Delete(string sql, List<(string key, dynamic value)> param);
    }
}
