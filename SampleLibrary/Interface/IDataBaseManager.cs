using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SampleLibrary
{
    interface IDataBaseManager : ICloneable
    {
        int Connect();

        int BeginTransaction();

        DataTable Fill();

        int Execute();

        int Commit();

        int RollBack();

        int Close();
    }
}
