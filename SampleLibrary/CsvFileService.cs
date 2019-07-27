using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SampleLibrary
{
    /// <summary>
    /// csvファイル読み書き用クラス
    /// </summary>
    public class CsvFileService : FileManager
    {
        private CsvFileService()
        {
            // コンストラクタの禁止
        }

        public static CsvFileService GetInstance()
        {
            return new CsvFileService();
        }

        /// <summary>
        /// ファイル読み込み
        /// 基底クラスのFileReadを隠蔽し、振る舞いを変える
        /// （戻り値を配列に変更している）
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public new IEnumerable<string[]> FileRead(string filePath, string encoding = "UTF-8")
        {
            foreach (string text in base.FileRead(filePath, encoding))
            {
                yield return text.Split(',');
            }
        }

        public new int FileWrite(string filePath, string encoding = "UTF-8", params string[] text)
        {
            return base.FileWrite(filePath, encoding, string.Join(",", text));
        }

        public int FileWrite(string filePath, string encoding = "UTF-8", List<string> text = null)
        {
            if (text == null)
            {
                return -1;
            }
            return base.FileWrite(filePath, encoding, text.ToArray());
        }

        public int FileWrite(string filePath, string encoding = "UTF-8", List<string[]> text = null)
        {
            if (text == null)
            {
                return -1;
            }

            List<string> texts = new List<string>();
            foreach (string[] t in text)
            {
                texts.Add(string.Join(",", t));
            }

            return FileWrite(filePath, encoding, texts);
        }
    }
}
