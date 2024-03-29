﻿using SampleLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample.Service
{
    public class Form1Service : Base.BaseService
    {
        private Form1Service(Form form) : base(form)
        {
            // コンストラクタの直接呼出しを禁止する
        }

        public static Form1Service GetInstance(Form form)
        {
            return new Form1Service(form);
        }

        public DataTable GetDataTable(string filePath, string encoding)
        {
            DataTable dt = new DataTable();
            try
            {
                // csvファイル読込
                // ToListで読み込み処理を確定させる
                CsvFileService csv = CsvFileService.GetInstance();
                List<string[]> list = csv.CsvFileRead(filePath, encoding).ToList();

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

        public BindingSource Search(BindingSource bs, string colName, string searchText)
        {
            if (string.IsNullOrEmpty(colName) || string.IsNullOrEmpty(searchText))
            {
                bs = Clear(bs);
            }
            else
            {
                bs.SetFilter(colName, searchText);
            }
            return bs;
        }

        public BindingSource Clear(BindingSource bs)
        {
            bs.Filter = string.Empty;
            return bs;
        }

        public int Update(string filePath, string encoding, DataTable dt)
        {
            List<string> writeData = new List<string>();
            // header
            string header = string.Empty;
            foreach (DataColumn col in dt.Columns)
            {
                header += col + ",";
            }
            writeData.Add(header.Remove(header.Length - 1));

            // data
            foreach (DataRow row in dt.Rows)
            {
                string data = string.Empty;
                for (int colIdx = 0; colIdx < dt.Columns.Count; colIdx++)
                {
                    data += row[colIdx]?.ToString() + ",";
                }
                writeData.Add(data.Remove(data.Length - 1));
            }

            CsvFileService csv = CsvFileService.GetInstance();
            return csv.FileWrite(filePath, encoding, false, writeData);
        }
    }
}
