using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleLibrary
{
    public interface ISQLManager : IDisposable
    {
        DataTable Search(string sql, Dictionary<string, dynamic> parameters);
        int Insert(string sql, Dictionary<string, dynamic> parameters);

        DataTable Lock(string sql, Dictionary<string, dynamic> parameters);
        int Update(string sql, Dictionary<string, dynamic> parameters);
        int Delete(string sql, Dictionary<string, dynamic> parameters);

        void Commit();
        void RollBack();
    }
}
