using SampleLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    class Form1Service : CsvFileService
    {
        public DataTable GetDataTable(string filePath,string encoding)
        {
            List<string[]> list = CsvFileRead(filePath,encoding);

            DataTable dt = new DataTable();

            // ヘッダ取得
            if (list.Count > 0)
            {
                foreach (string text in list[0])
                {
                    dt.Columns.Add(text);
                }
            }

            // データ設定
            for (int rowIdx = 1; rowIdx < list.Count; rowIdx++)
            {
                DataRow row = dt.NewRow();
                for (int colIdx = 0; colIdx < dt.Columns.Count; colIdx++)
                {
                    row[colIdx] = list[rowIdx][colIdx];
                }
                dt.Rows.Add(row);
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
