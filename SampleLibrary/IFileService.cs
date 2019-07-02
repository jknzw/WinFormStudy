using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleLibrary
{
    interface IFileService
    {
        List<string> FileRead(string filePath, string encoding);

        int FileWrite(string filePath,  List<string> text, string encoding);
    }
}
