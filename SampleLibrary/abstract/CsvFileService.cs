using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SampleLibrary
{
    /// <summary>
    /// csvファイル読み書き用抽象クラス
    /// メソッドを定義すると共に最低限の機能を実装している
    /// 使うときは継承して使う
    /// </summary>
    public abstract class CsvFileService : IFileService
    {
        public int FileWrite(string filePath, string encoding = "UTF-8", params string[] text)
        {
            File.WriteAllLines(filePath, text, Encoding.GetEncoding(encoding));
            return 0;
        }

        public string[] FileRead(string filePath, string encoding = "UTF-8")
        {
            return File.ReadAllLines(filePath, Encoding.GetEncoding(encoding));
        }

        public IEnumerable<string[]> CsvFileRead(string filePath, string encoding = "UTF-8")
        {
            string[] texts = FileRead(filePath, encoding);
            foreach (string text in texts)
            {
                Debug.WriteLine($"yield return [{text}]");
                yield return text.Replace("<br>", "\n").Split(',');
            }
        }
        public int CsvFileWrite(string filePath, string encoding = "UTF-8", params string[] text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                text[i] = text[i].Replace("\n", "<br>");
            }
            File.WriteAllLines(filePath, text, Encoding.GetEncoding(encoding));
            return 0;
        }
    }
}
