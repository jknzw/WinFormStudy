using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SampleLibrary
{
    public abstract class CsvFileService : IFileService
    {
        public int FileWrite(string filePath, List<string> text, string encoding = "UTF-8")
        {
            File.WriteAllLines(filePath, text, Encoding.GetEncoding(encoding));

            return 0;
        }

        public List<string> FileRead(string filePath, string encoding = "UTF-8")
        {
            List<string> list = new List<string>();

            list.AddRange(File.ReadAllLines(filePath, Encoding.GetEncoding(encoding)));

            return list;
        }

        public List<string[]> CsvFileRead(string filePath, string encoding = "UTF-8")
        {
            List<string> list = FileRead(filePath, encoding);

            List<string[]> csvlist = new List<string[]>();
            foreach (string text in list)
            {
                csvlist.Add(text.Split(','));
            }

            return csvlist;
        }
    }
}
