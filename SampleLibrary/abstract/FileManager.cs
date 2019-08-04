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
        protected const string DefaultEncoding = "UTF-8";

        public virtual int FileWrite(string filePath, params string[] texts)
        {
            return FileWrite(filePath, DefaultEncoding, false, texts);
        }

        public virtual int FileAppend(string filePath, params string[] texts)
        {
            return FileWrite(filePath, DefaultEncoding, true, texts);
        }

        public virtual int FileWrite(string filePath, string encoding = DefaultEncoding, bool append = false, params string[] texts)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i] = texts[i].Replace("\n", "<br>");
            }

            int count = 0;
            using (StreamWriter sw = new StreamWriter(filePath, append, Encoding.GetEncoding(encoding)))
            {
                foreach (string text in texts)
                {
                    sw.WriteLine(text);
                    count++;
                }
            }
            return count;
        }

        public virtual IEnumerable<string> FileRead(string filePath, string encoding = DefaultEncoding)
        {
            if (File.Exists(filePath))
            {
                foreach (string text in File.ReadAllLines(filePath, Encoding.GetEncoding(encoding)))
                {
                    Debug.WriteLine($"yield return [{text}]");
                    yield return text.Replace("<br>", "\n");
                }
            }
            else
            {
                yield return null;
            }
        }
    }
}
