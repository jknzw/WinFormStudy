using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SampleLibrary
{
    interface IDataBaseManager : IDisposable
    {
        void Connect();

        void BeginTransaction();

        DataTable Fill();

        int Execute();

        void Commit();

        void RollBack();

        void Close();
    }
}
