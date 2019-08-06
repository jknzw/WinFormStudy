using Sample.Utility;
using SampleLibrary;
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
    public class FormKakeiboService : Base.BaseService
    {
        //private readonly string zankinFilePath = "./zankin.txt";
        private readonly string rirekiFilePath = "./rireki.csv";

        public class BeanKakeibo
        {
            public int Zankin { get; set; } = 0;
            public int SumShunyu { get; set; } = 0;
            public int SumShishutsu { get; set; } = 0;
            public DataTable DtRireki { get; set; } = new DataTable();
        }

        private FormKakeiboService() : base()
        {
            // コンストラクタの直接呼出しを禁止する
        }

        private FormKakeiboService(Form form) : base(form)
        {
            // コンストラクタの直接呼出しを禁止する
        }

        public static FormKakeiboService GetInstance()
        {
            return new FormKakeiboService();
        }

        public static FormKakeiboService GetInstance(Form form)
        {
            return new FormKakeiboService(form);
        }

        //public int GetZankin()
        //{
        //    FileService fs = new FileService();
        //    string zankin = fs.FileRead(zankinFilePath).First();
        //    int.TryParse(zankin, out int result);
        //    return result;
        //}

        //public int WriteZankin(int zankin)
        //{
        //    FileService fs = new FileService();
        //    return fs.FileWrite(zankinFilePath, zankin.ToString());
        //}

        public BeanKakeibo GetRireki()
        {
            string encoding = "UTF-8";
            BeanKakeibo bean = new BeanKakeibo();
            DataTable dt = new DataTable();
            try
            {
                // csvファイル読込
                // ToListで読み込み処理を確定させる
                CsvFileService csv = CsvFileService.GetInstance();
                List<string[]> list = csv.CsvFileRead(rirekiFilePath, encoding)?.ToList();

                if (list.First() == null)
                {
                    // ヘッダがない場合、ファイルが存在しない
                    // ヘッダを生成して出力
                    WriteRirekiHeader(csv);

                    // 空のテーブルを返す
                    return bean;
                }

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

                    // 収入計算
                    int.TryParse(row[EnumRireki.Shunyu.GetInt()].ToString(), out int shunyu);
                    bean.SumShunyu += shunyu;

                    // 支出計算
                    int.TryParse(row[EnumRireki.Shishutsu.GetInt()].ToString(), out int shishutsu);
                    bean.SumShishutsu += shishutsu;

                    // 行追加
                    dt.Rows.Add(row);
                }

                // 残金
                int.TryParse(list.Last()[EnumRireki.Zankin.GetInt()], out int zankin);
                bean.Zankin = zankin;

                // データテーブルを設定
                bean.DtRireki = dt;
            }
            catch (Exception ex)
            {
                // csvファイルのデータが0件・1件の場合 ArgumentNullException
                Debug.WriteLine(ex.Message);
            }
            return bean;
        }

        private void WriteRirekiHeader(CsvFileService csv)
        {
            csv.FileWrite(rirekiFilePath,
                EnumRirekiExtension.RirekiDic[EnumRireki.Ymd],
                EnumRirekiExtension.RirekiDic[EnumRireki.Youto],
                EnumRirekiExtension.RirekiDic[EnumRireki.Shunyu],
                EnumRirekiExtension.RirekiDic[EnumRireki.Shishutsu],
                EnumRirekiExtension.RirekiDic[EnumRireki.Zankin],
                EnumRirekiExtension.RirekiDic[EnumRireki.Biko]);
        }

        public int Touroku()
        {
            CsvFileService csv = CsvFileService.GetInstance();

            DateTime dt = ControlValuesDictionary["dateTimePicker1"];
            string youto = ControlValuesDictionary["comboBoxYouto"];

            string type = ControlValuesDictionary["comboBoxType"];
            int.TryParse(ControlValuesDictionary["customTextBoxKingaku"], out int kingaku);
            int.TryParse(ControlValuesDictionary["customReadOnlyTextBoxZankin"], out int zankin);
            string shunyu = string.Empty;
            string sishutsu = string.Empty;
            if (type.Equals("支出"))
            {
                sishutsu = kingaku.ToString();
                zankin -= kingaku;
            }
            else
            {
                // 収入
                shunyu = kingaku.ToString();
                zankin += kingaku;
            }

            // csv.FileAppend(rirekiFilePath, "年月日", "用途", "収入","支出",  "残金", "備考");
            string biko = ControlValuesDictionary["customTextBoxBiko"];

            if (1 == csv.FileAppend(rirekiFilePath, dt.ToString("yyyy/MM/dd"), youto, shunyu, sishutsu, zankin.ToString(), biko))
            {
                // 正常
                return 0;

                //if (1 == WriteZankin(zankin))
                //{
                //    //正常
                //    return 0;
                //}
                //else
                //{
                //    // 残金の書き込みに失敗
                //    return -2;
                //}
            }
            else
            {
                // 履歴の書き込みに失敗
                return -1;
            }
        }

        public int Delete(DataGridView dataGridView, out BeanKakeibo val)
        {
            val = null;

            int count = 0;
            // 選択行がある場合
            if (dataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected) > 0)
            {
                // 選択行を削除
                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                {
                    dataGridView.Rows.Remove(row);
                    count++;
                }

                // ファイル更新
                if (0 <= UpdateAll(dataGridView, out val))
                {
                    // 正常
                }
                else
                {
                    // 更新失敗
                    count = -1;
                }
            }
            else
            {
                // 選択0件
            }
            return count;
        }

        /// <summary>
        /// CSVファイル更新
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="count"></param>
        /// <returns>書き込み数</returns>
        public int UpdateAll(DataGridView dataGridView, out BeanKakeibo value)
        {
            int count = 0;
            // 履歴ファイルにDataGridViewの内容を書き込む
            // ソート前情報(DataTable)を取得
            DataTable dt = (dataGridView.DataSource as BindingSource).DataSource as DataTable;

            CsvFileService csv = CsvFileService.GetInstance();

            WriteRirekiHeader(csv);

            bool first = true;
            value = new BeanKakeibo();
            foreach (DataRow row in dt.Rows)
            {
                // 収入合計
                int.TryParse(row[EnumRireki.Shunyu.GetInt()].ToString(), out int shunyu);
                value.SumShunyu += shunyu;

                // 支出合計
                int.TryParse(row[EnumRireki.Shishutsu.GetInt()].ToString(), out int shishutsu);
                value.SumShishutsu += shishutsu;

                // 残金
                if (first)
                {
                    // 最初のレコード
                    // 残金は変更しない
                    int.TryParse(row[EnumRireki.Zankin.GetInt()].ToString(), out int zankin);
                    value.Zankin = zankin;
                    first = false;
                }
                else
                {
                    // 2番目以降
                    // 残金を再設定する
                    value.Zankin = value.Zankin + shunyu - shishutsu;
                    row[EnumRireki.Zankin.GetInt()] = value.Zankin;
                }

                // ファイル書き込み
                count += csv.FileAppend(rirekiFilePath,
                    row[EnumRireki.Ymd.GetInt()].ToString(),
                    row[EnumRireki.Youto.GetInt()].ToString(),
                    row[EnumRireki.Shunyu.GetInt()].ToString(),
                    row[EnumRireki.Shishutsu.GetInt()].ToString(),
                    row[EnumRireki.Zankin.GetInt()].ToString(),
                    row[EnumRireki.Biko.GetInt()].ToString());
            }

            // 履歴
            value.DtRireki = dt;

            return count;
        }
    }
}
