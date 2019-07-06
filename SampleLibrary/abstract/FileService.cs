using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SampleLibrary
{
    /// <summary>
    /// ファイル読み書き用抽象クラス
    /// メソッドを定義すると共に最低限の機能を実装している
    /// 使うときは継承して使う
    /// </summary>
    public abstract class FileService : IFileService
    {
        public int FileWrite(string filePath, string encoding = "UTF-8", params string[] text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                text[i] = text[i].Replace("\n", "<br>");
            }
            File.WriteAllLines(filePath, text, Encoding.GetEncoding(encoding));
            return 0;
        }

        public IEnumerable<string> FileRead(string filePath, string encoding = "UTF-8")
        {
            foreach (string text in File.ReadAllLines(filePath, Encoding.GetEncoding(encoding)))
            {
                Debug.WriteLine($"yield return [{text}]");
                yield return text.Replace("<br>", "\n");
            }
        }
    }
}
