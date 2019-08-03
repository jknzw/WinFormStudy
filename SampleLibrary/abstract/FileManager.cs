using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SampleLibrary
{
    /// <summary>
    /// ファイル読み書き用抽象クラス
    /// メソッドと最低限の共通機能を実装している
    /// 使うときは継承して使う
    /// </summary>
    public abstract class FileManager : IFileManager
    {
        public virtual int FileWrite(string filePath, string encoding = "UTF-8", params string[] texts)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i] = texts[i].Replace("\n", "<br>");
            }

            int count = 0;
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.GetEncoding(encoding)))
            {
                foreach (string text in texts)
                {
                    sw.WriteLine(text);
                    count++;
                }
            }
            return count;
        }

        public virtual IEnumerable<string> FileRead(string filePath, string encoding = "UTF-8")
        {
            foreach (string text in File.ReadAllLines(filePath, Encoding.GetEncoding(encoding)))
            {
                Debug.WriteLine($"yield return [{text}]");
                yield return text.Replace("<br>", "\n");
            }
        }
    }
}
