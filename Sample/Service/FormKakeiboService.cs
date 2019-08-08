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
using System.IO;

namespace Sample.Service
{
    public class FormKakeiboService : Base.BaseService
    {
        private readonly string zankinFilePath = "zankin.txt";
        private readonly string rirekiFolderPath = "rireki";
        private readonly string encoding = "UTF-8";

        public class ModelKakeibo
        {
            public int Zankin { get; set; } = 0;
            public int SumShunyu { get; set; } = 0;
            public int SumShishutsu { get; set; } = 0;
            public DataTable RirekiTable { get; set; } = new DataTable();
            public DataTable ShukeiTable { get; set; } = new DataTable();
            public IEnumerable<string> RirekiFiles { get; set; } = null;

            public string RirekiFile = null;
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

        public int GetZankin()
        {
            FileService fs = new FileService();
            string zankin = fs.FileRead(zankinFilePath).First();
            int.TryParse(zankin, out int result);
            return result;
        }

        public int WriteZankin(int zankin)
        {
            FileService fs = new FileService();
            return fs.FileWrite(zankinFilePath, zankin.ToString());
        }

        public ModelKakeibo GetRireki()
        {
            ModelKakeibo model = new ModelKakeibo();
            try
            {
                // csvファイル読込
                // ToListで読み込み処理を確定させる
                CsvFileService csv = CsvFileService.GetInstance();

                // 履歴ファイルリスト
                model.RirekiFiles = Directory.EnumerateFiles(rirekiFolderPath, "rireki*.csv");
                model.RirekiFile = GetRirekiFilePath();

                List<string[]> list = csv.CsvFileRead(model.RirekiFile, encoding)?.ToList();

                if (list.First() == null)
                {
                    // ヘッダがない場合、ファイルが存在しない
                    // ヘッダを生成して出力
                    WriteRirekiHeader(csv, model.RirekiFile);

                    // 残金を繰越金として書き込む
                    int kurikoshi = GetZankin();
                    csv.FileAppend(model.RirekiFile, DateTime.Now.ToString("yyyy/MM/01"), "繰り越し", kurikoshi.ToString(), string.Empty, kurikoshi.ToString(), string.Empty);

                    // 再読込
                    list = csv.CsvFileRead(model.RirekiFile, encoding)?.ToList();
                }

                (DataTable dtRireki, int shunyu, int shishutsu) = ConvertToRirekiDataTable(list);
                model.SumShunyu = shunyu;
                model.SumShishutsu = shishutsu;

                // 残金
                int.TryParse(list.Last()[EnumRireki.Zankin.GetInt()], out int zankin);
                model.Zankin = zankin;

                // データテーブルを設定
                model.RirekiTable = dtRireki;
                model.ShukeiTable = GetShukeiTable(dtRireki);
            }
            catch (Exception ex)
            {
                // csvファイルのデータが0件・1件の場合 ArgumentNullException
                Debug.WriteLine(ex.Message);
            }
            return model;
        }

        public DataTable GetKakoRirekiTable(string filePath, string shukeiMode)
        {
            CsvFileService csv = CsvFileService.GetInstance();
            List<string[]> list = csv.CsvFileRead(filePath, encoding)?.ToList();
            (DataTable dt, _, _) = ConvertToRirekiDataTable(list);
            dt = GetShukeiTable(dt, shukeiMode);
            return dt;
        }

        private (DataTable, int, int) ConvertToRirekiDataTable(List<string[]> list)
        {
            DataTable dtRireki = new DataTable();

            // ヘッダ設定
            foreach (string text in list.First())
            {
                Debug.WriteLine($"Columns.Add({text})");
                dtRireki.Columns.Add(text);
            }

            // データ設定
            int sumShunyu = 0;
            int sumShishutsu = 0;
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
                sumShunyu += shunyu;

                // 支出計算
                int.TryParse(row[EnumRireki.Shishutsu.GetInt()].ToString(), out int shishutsu);
                sumShishutsu += shishutsu;

                // 行追加
                dtRireki.Rows.Add(row);
            }

            return (dtRireki, sumShunyu, sumShishutsu);
        }

        private string GetRirekiFilePath()
        {

            // ファイルパス
            return Path.Combine(rirekiFolderPath, $"rireki{DateTime.Now.ToString("yyyyMM")}.csv");
        }

        public DataTable GetShukeiTable(DataTable dtRireki, string mode = null)
        {
            if (string.IsNullOrEmpty(mode))
            {
                mode = ControlValuesDictionary["cmbShukeiMode"];
            }

            DataTable dt;
            switch (mode)
            {
                case "年月日＋内容":
                    dt = GetShukeiYmdYouto(dtRireki);
                    break;
                case "内容":
                    dt = GetShukeiYouto(dtRireki);
                    break;
                case "年月日":
                    dt = GetShukeiYmd(dtRireki);
                    break;
                case "全て":
                default:
                    dt = dtRireki;
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


        private void WriteRirekiHeader(CsvFileService csv, string filePath)
        {
            csv.FileWrite(filePath,
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


            string filePath = GetRirekiFilePath();
            if (1 == csv.FileAppend(filePath, dt.ToString("yyyy/MM/dd"), youto, shunyu, sishutsu, zankin.ToString(), biko))
            {
                // 正常
                if (1 == WriteZankin(zankin))
                {
                    //正常
                    return 0;
                }
                else
                {
                    // 残金の書き込みに失敗
                    return -2;
                }
            }
            else
            {
                // 履歴の書き込みに失敗
                return -1;
            }
        }

        public (int, ModelKakeibo) Delete(string rirekiFilePath, DataGridView dataGridView)
        {
            ModelKakeibo val = null;
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
                (int ret, ModelKakeibo value) = UpdateAll(rirekiFilePath, dataGridView);
                if (0 <= ret)
                {
                    // 正常
                    val = value;
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
            return (count, val);
        }

        /// <summary>
        /// 履歴・残金ファイル更新
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="count"></param>
        /// <returns>書き込み数</returns>
        public (int, ModelKakeibo) UpdateAll(string rirekiFilePath, DataGridView dataGridView)
        {
            ModelKakeibo value = new ModelKakeibo();

            int count = 0;
            // 履歴ファイルにDataGridViewの内容を書き込む
            // ソート前情報(DataTable)を取得
            DataTable dt = (dataGridView.DataSource as BindingSource).DataSource as DataTable;

            CsvFileService csv = CsvFileService.GetInstance();

            WriteRirekiHeader(csv, rirekiFilePath);

            bool first = true;
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

            // 残金ファイル更新
            if (1 == WriteZankin(value.Zankin))
            {
                //正常
                return (count, value);
            }
            else
            {
                // 残金の書き込みに失敗
                return (-2, value);
            }
        }
    }
}
