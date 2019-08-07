using Sample.DataSet;
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
using SampleLibrary.Utility;

namespace Sample.Service
{
    public class FormKakeiboService : Base.BaseService
    {
        //private readonly string zankinFilePath = "./zankin.txt";
        private readonly string rirekiFilePath = "./rireki.csv";

        public class ModelKakeibo
        {
            public int Zankin { get; set; } = 0;
            public int SumShunyu { get; set; } = 0;
            public int SumShishutsu { get; set; } = 0;
            public DataTable RirekiTable { get; set; } = new DataTable();
            public DataTable ShukeiTable { get; set; } = new DataTable();
        }

        private FormKakeiboService() : base()
        {
            // コンストラクタの直接呼出しを禁止する
        }

        private FormKakeiboService(Form form) : base(form)
        {
            // コンストラクタの直接呼出しを禁止する
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

        public ModelKakeibo GetRireki()
        {
            string encoding = "UTF-8";
            ModelKakeibo bean = new ModelKakeibo();
            DataTable dtRireki = new DataTable();
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
                    dtRireki.Columns.Add(text);
                }

                // データ設定
                foreach (string[] texts in list.Skip(1))
                {
                    DataRow row = dtRireki.NewRow();
                    for (int colIdx = 0; colIdx < dtRireki.Columns.Count; colIdx++)
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
                    dtRireki.Rows.Add(row);
                }

                // 残金
                int.TryParse(list.Last()[EnumRireki.Zankin.GetInt()], out int zankin);
                bean.Zankin = zankin;

                // データテーブルを設定
                bean.RirekiTable = dtRireki;
                bean.ShukeiTable = GetShukeiTable(dtRireki);
            }
            catch (Exception ex)
            {
                // csvファイルのデータが0件・1件の場合 ArgumentNullException
                Debug.WriteLine(ex.Message);
            }
            return bean;
        }

        public DataTable GetShukeiTable(DataTable dtRireki)
        {
            DataTable dt;
            switch (ControlValuesDictionary["cmbShukeiMode"])
            {
                case "年月日＋内容":
                    dt = GetShukeiYmdYouto(dtRireki);
                    break;
                case "内容":
                    dt = GetShukeiYouto(dtRireki);
                    break;
                case "年月日":
                default:
                    dt = GetShukeiYmd(dtRireki);
                    break;
            }
            return dt;
        }

        private DataTable GetShukeiYmd(DataTable rirekiTable)
        {
            DataTable dtShukei = new DataSetKakeibo.ShukeiDataTable();

            SortedDictionary<string, (int shunyu, int shishutsu)> dic = new SortedDictionary<string, (int, int)>();

            foreach (DataRow row in rirekiTable.Rows)
            {
                string ymd = row[EnumRireki.Ymd.GetInt()].ToString();
                int shunyu = row[EnumRireki.Shunyu.GetInt()].GetInt();
                int shishutsu = row[EnumRireki.Shishutsu.GetInt()].GetInt();

                if (dic.ContainsKey(ymd))
                {
                    shunyu = dic[ymd].shunyu + shunyu;
                    shishutsu = dic[ymd].shishutsu + shishutsu;
                    dic[ymd] = (shunyu, shishutsu);
                }
                else
                {
                    dic.Add(ymd, (shunyu, shishutsu));
                }
            }

            foreach (string ymd in dic.Keys)
            {
                DataRow row = dtShukei.NewRow();

                row[EnumRirekiExtension.RirekiDic[EnumRireki.Ymd]] = ymd;
                row[EnumRirekiExtension.RirekiDic[EnumRireki.Shunyu]] = dic[ymd].shunyu;
                row[EnumRirekiExtension.RirekiDic[EnumRireki.Shishutsu]] = dic[ymd].shishutsu;

                dtShukei.Rows.Add(row);
            }

            return dtShukei;
        }

        private DataTable GetShukeiYouto(DataTable rirekiTable)
        {
            DataTable dtShukei = new DataSetKakeibo.ShukeiYoutoDataTable();

            SortedDictionary<string, (int shunyu, int shishutsu)> dic = new SortedDictionary<string, (int, int)>();

            foreach (DataRow row in rirekiTable.Rows)
            {
                string youto = row[EnumRireki.Youto.GetInt()].ToString();
                int shunyu = row[EnumRireki.Shunyu.GetInt()].GetInt();
                int shishutsu = row[EnumRireki.Shishutsu.GetInt()].GetInt();

                if (dic.ContainsKey(youto))
                {
                    shunyu = dic[youto].shunyu + shunyu;
                    shishutsu = dic[youto].shishutsu + shishutsu;
                    dic[youto] = (shunyu, shishutsu);
                }
                else
                {
                    dic.Add(youto, (shunyu, shishutsu));
                }
            }

            foreach (string youto in dic.Keys)
            {
                DataRow row = dtShukei.NewRow();

                row[EnumRirekiExtension.RirekiDic[EnumRireki.Youto]] = youto;
                row[EnumRirekiExtension.RirekiDic[EnumRireki.Shunyu]] = dic[youto].shunyu;
                row[EnumRirekiExtension.RirekiDic[EnumRireki.Shishutsu]] = dic[youto].shishutsu;

                dtShukei.Rows.Add(row);
            }

            return dtShukei;
        }

        private DataTable GetShukeiYmdYouto(DataTable rirekiTable)
        {
            DataTable dtShukei = new DataSetKakeibo.ShukeiYmdYoutoDataTable();

            SortedDictionary<(string ymd, string youto), (int shunyu, int shishutsu)> dic = new SortedDictionary<(string ymd, string youto), (int, int)>();

            foreach (DataRow row in rirekiTable.Rows)
            {
                string ymd = row[EnumRireki.Ymd.GetInt()].ToString();
                string youto = row[EnumRireki.Youto.GetInt()].ToString();
                int shunyu = row[EnumRireki.Shunyu.GetInt()].GetInt();
                int shishutsu = row[EnumRireki.Shishutsu.GetInt()].GetInt();

                if (dic.ContainsKey((ymd, youto)))
                {
                    shunyu = dic[(ymd, youto)].shunyu + shunyu;
                    shishutsu = dic[(ymd, youto)].shishutsu + shishutsu;
                    dic[(ymd, youto)] = (shunyu, shishutsu);
                }
                else
                {
                    dic.Add((ymd, youto), (shunyu, shishutsu));
                }
            }

            foreach ((string ymd, string youto) in dic.Keys)
            {
                DataRow row = dtShukei.NewRow();

                row[EnumRirekiExtension.RirekiDic[EnumRireki.Ymd]] = ymd;
                row[EnumRirekiExtension.RirekiDic[EnumRireki.Youto]] = youto;
                row[EnumRirekiExtension.RirekiDic[EnumRireki.Shunyu]] = dic[(ymd, youto)].shunyu;
                row[EnumRirekiExtension.RirekiDic[EnumRireki.Shishutsu]] = dic[(ymd, youto)].shishutsu;

                dtShukei.Rows.Add(row);
            }

            return dtShukei;
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

        public int Delete(DataGridView dataGridView, out ModelKakeibo val)
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
        public int UpdateAll(DataGridView dataGridView, out ModelKakeibo value)
        {
            int count = 0;
            // 履歴ファイルにDataGridViewの内容を書き込む
            // ソート前情報(DataTable)を取得
            DataTable dt = (dataGridView.DataSource as BindingSource).DataSource as DataTable;

            CsvFileService csv = CsvFileService.GetInstance();

            WriteRirekiHeader(csv);

            bool first = true;
            value = new ModelKakeibo();
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
            value.RirekiTable = dt;

            // 集計
            value.ShukeiTable = GetShukeiYmd(dt);

            return count;
        }
    }
}
