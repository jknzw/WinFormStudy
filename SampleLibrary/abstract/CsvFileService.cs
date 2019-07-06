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
    public abstract class CsvFileService : FileService
    {
        public IEnumerable<string[]> CsvFileRead(string filePath, string encoding = "UTF-8")
        {
            foreach (string text in FileRead(filePath, encoding))
            {
                yield return text.Split(',');
            }
        }
        public int CsvFileWrite(string filePath, List<string> text, string encoding = "UTF-8")
        {
            return FileWrite(filePath, encoding, text.ToArray());
        }
    }
}
