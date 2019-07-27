using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleLibrary
{
    /// <summary>
    /// ファイル操作インタフェース
    /// ファイル読込・ファイル書込処理のメソッドの実装規約
    /// </summary>
    interface IFileManager
    {
        /// <summary>
        /// ファイル読込
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="encoding">文字コード</param>
        /// <returns>読込データ</returns>
        IEnumerable<string> FileRead(string filePath, string encoding);

        /// <summary>
        /// ファイル書込
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="encoding">文字コード</param>
        /// <param name="text">書込データ</param>
        /// <returns>0:正常</returns>
        int FileWrite(string filePath, string encoding, params string[] text);
    }
}
