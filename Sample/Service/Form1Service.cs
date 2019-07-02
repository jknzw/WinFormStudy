using SampleLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    class Form1Service : CsvFileService
    {
        public DataTable GetDataTable(string filePath, string encoding)
        {
            DataTable dt = new DataTable();
            try
            {
                // csvファイル読込
                IEnumerable<string[]> list = CsvFileRead(filePath, encoding);

                // ヘッダ設定
                foreach (string text in list.First())
                {
                    Debug.WriteLine($"Columns.Add({text})");
                    dt.Columns.Add(text);
                }

                // データ設定
                foreach (string[] texts in list.Skip(1))
                {
                    DataRow row = dt.NewRow();
                    for (int colIdx = 0; colIdx < dt.Columns.Count; colIdx++)
                    {
                        Debug.WriteLine($"Rows[{colIdx}].Add({texts[colIdx]})");
                        row[colIdx] = texts[colIdx];
                    }
                    dt.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                // csvファイルのデータが0件・1件の場合 ArgumentNullException
                Debug.WriteLine(ex.Message);
            }
            return dt;
        }
        public int Insert()
        {
            return 0;
        }
        public int Update()
        {
            return 0;
        }
        public int Delete()
        {
            return 0;
        }
    }
}
