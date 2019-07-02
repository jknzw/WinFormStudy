using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleLibrary
{
    public interface IDataBaseService
    {
        int Search();
        int Insert();
        int Update();
        int Delete();
    }
}
