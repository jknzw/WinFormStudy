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
using System.Reflection;

namespace Sample.Service
{
    public class FormKakeiboSqlService : Base.BaseService
    {
        private readonly string dataSource = "localhost";
        private readonly string dataBase = "SampleDb";
        private readonly string userId = "SampleUser";
        private readonly string password = "1234SampleUser";

        private readonly Logger logger = Logger.GetInstance(nameof(FormKakeiboSqlService));

        private static readonly object lockobj = new object();
        private enum ErrorCode : int
        {
            ZankinFileWriteError = -1,

        }

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

        private FormKakeiboSqlService() : base()
        {
            // コンストラクタの直接呼出しを禁止する
        }

        private FormKakeiboSqlService(Form form) : base(form)
        {
            // コンストラクタの直接呼出しを禁止する
        }

        public static FormKakeiboSqlService GetInstance()
        {
            return new FormKakeiboSqlService();
        }

        public static FormKakeiboSqlService GetInstance(Form form)
        {
            return new FormKakeiboSqlService(form);
        }

        public int GetZankin()
        {
            using (SQLManager manager = SQLManager.GetInstance(dataSource, dataBase, userId, password))
            {
                return GetZankin(manager);
            }
        }

        private int GetZankin(SQLManager manager)
        {
            string sql = @"
                SELECT * FROM kakeibo_zankin
                ";

            DataTable dt = manager.Select(sql);

            if (dt.Rows.Count > 0)
            {
                string zankin = dt.Rows[0]["zankin"].ToString();
                int.TryParse(zankin, out int result);
                return result;
            }
            else
            {
                return 0;
            }
        }

        public int WriteZankin(SQLManager manager, int zankin)
        {
            int count = -1;

            string sql = @"
                update kakeibo_zankin 
                set 
                    zankin = @zankin,
                    upduser = @upduser,
                    updtime = @updtime
                ";

            Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>();

            DateTime now = DateTime.Now;
            parameters.Add("zankin", zankin);
            parameters.Add("upduser", nameof(FormKakeiboSqlService));
            parameters.Add("updtime", now);

            count = manager.Update(sql, parameters);

            if (count == 0)
            {
                sql = @"
                    insert into kakeibo_zankin (
                        zankin,
                        insuser,
                        instime,
                        upduser,
                        updtime
                    ) values (
                        @zankin,
                        @insuser,
                        @instime,
                        @upduser,
                        @updtime
                    )
                    ";

                parameters.Add("insuser", nameof(FormKakeiboSqlService));
                parameters.Add("instime", now);

                count = manager.Insert(sql, parameters);
            }

            return count;
        }

        //public IEnumerable<string> GetRirekiFiles()
        //{
        //    // 履歴ファイルリスト
        //    return Directory.EnumerateFiles(rirekiFolderPath, "rireki*.csv");
        //}

        public async Task<int> GetKakeiboData(ModelKakeibo model)
        {
            using (SQLManager manager = SQLManager.GetInstance(dataSource, dataBase, userId, password))
            {
                return await GetKakeiboData(manager, model);
            }
        }

        private async Task<int> GetKakeiboData(SQLManager manager, ModelKakeibo model)
        {
            return await Task.Run(() =>
            {
                string sql = @"
                    SELECT 
                        seq
                        , hiduke
                        , naiyou
                        , nyukin
                        , shukkin
                        , zankin
                        , bikou
                    FROM kakeibo_rireki
                    WHERE hiduke BETWEEN @from AND @to
                    ";

                Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>
                {
                    { "from", ControlValuesDictionary["dtpFrom"].ToString("yyyy/MM/dd") },
                    { "to", ControlValuesDictionary["dtpTo"].ToString("yyyy/MM/dd") },
                };

                DataTable dtRireki = manager.Select(sql, parameters);

                model.Zankin = GetZankin(manager);

                model.RirekiTable = dtRireki;

                SetShukeiData(model, dtRireki);

                return dtRireki.Rows.Count;
            });
        }

        private void SetShukeiData(ModelKakeibo model, DataTable dtRireki)
        {
            model.ShukeiTable = GetShukeiTable(dtRireki);
            int.TryParse(dtRireki.Compute("Sum(nyukin)", null).ToString(), out int sumShunyu);
            int.TryParse(dtRireki.Compute("Sum(shukkin)", null).ToString(), out int sumShishutsu);
            model.SumShunyu = sumShunyu;
            model.SumShishutsu = sumShishutsu;
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
                string ymd = Convert.ToDateTime(row["hiduke"]).ToString("yyyy/MM/dd");
                int shunyu = row["nyukin"].GetInt();
                int shishutsu = row["shukkin"].GetInt();

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
                string youto = row["naiyou"].ToString();
                int shunyu = row["nyukin"].GetInt();
                int shishutsu = row["shukkin"].GetInt();

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
                string ymd = Convert.ToDateTime(row["hiduke"]).ToString("yyyy/MM/dd");
                string youto = row["naiyou"].ToString();
                int shunyu = row["nyukin"].GetInt();
                int shishutsu = row["shukkin"].GetInt();

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

        /// <summary>
        /// 登録
        /// </summary>
        /// <returns>登録件数</returns>
        public int Touroku()
        {
            DateTime inputDateTime = ControlValuesDictionary["dateTimePicker1"];
            string youto = ControlValuesDictionary["comboBoxYouto"];

            string type = ControlValuesDictionary["comboBoxType"];
            int.TryParse(ControlValuesDictionary["customTextBoxKingaku"], out int kingaku);
            dynamic shunyu = DBNull.Value;
            dynamic shishutsu = DBNull.Value;

            string biko = ControlValuesDictionary["customTextBoxBiko"];

            int count = -1;

            lock (lockobj)
            {
                using (SQLManager manager = SQLManager.GetInstance(dataSource, dataBase, userId, password))
                {
                    // 残金の計算
                    int zankin = GetZankin(manager);
                    if (type.Equals("支出"))
                    {
                        shishutsu = kingaku.ToString();
                        zankin -= kingaku;
                    }
                    else
                    {
                        // 収入
                        shunyu = kingaku.ToString();
                        zankin += kingaku;
                    }

                    // 残金の更新
                    int ret = WriteZankin(manager, zankin);
                    if (ret != 1)
                    {
                        // 残金の更新に失敗
                        return -1;
                    }

                    Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>
                {
                    { "hiduke", inputDateTime },
                    { "naiyou", youto },
                    { "nyukin", shunyu },
                    { "shukkin", shishutsu },
                    { "zankin", zankin },
                    { "bikou", biko },
                    { "insuser", nameof(Touroku) },
                    { "upduser", nameof(Touroku) }
                };

                    string sql = @"
                    INSERT INTO kakeibo_rireki (
                        hiduke
                        , naiyou
                        , nyukin
                        , shukkin
                        , zankin
                        , bikou
                        , insuser
                        , upduser
                    ) VALUES (
                        @hiduke
                        , @naiyou
                        , @nyukin
                        , @shukkin
                        , @zankin
                        , @bikou
                        , @insuser
                        , @upduser
                    )
                    ";

                    count = manager.Insert(sql, parameters);
                    if (count == 1)
                    {
                        manager.Commit();
                    }
                }
            }
            return count;
        }

        public int UpdateAll()
        {
            DataTable dtRireki = ControlValuesDictionary["gridRireki"];
            foreach (DataRow row in dtRireki.Rows)
            {
                // 編集を確定(DataRowVersion Proposed→Current)
                row.EndEdit();
            }

            // 更新対象を抽出
            DataTable dtModified = dtRireki.GetChanges(DataRowState.Modified);
            DataTable dtDeleted = dtRireki.GetChanges(DataRowState.Deleted);
            int updcount = dtModified?.Rows.Count ?? 0;
            updcount += dtDeleted?.Rows.Count ?? 0;
            if (updcount == 0)
            {
                // 更新対象が無い場合はreturn
                return 0;
            }

            int count = 0;
            using (SQLManager manager = SQLManager.GetInstance(dataSource, dataBase, userId, password))
            {
                if (dtModified != null)
                {
                    string sql = @"
                    UPDATE kakeibo_rireki
                    SET
                        hiduke = @hiduke,
                        naiyou = @naiyou,
                        nyukin = @nyukin,
                        shukkin = @shukkin,
                        zankin = @zankin,
                        bikou = @bikou,
                        upduser = @upduser,
                        updtime = GETDATE()
                    WHERE
                        seq = @seq
                    ";
                    foreach (DataRow row in dtModified.Rows)
                    {
                        Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>
                        {
                            { "seq", row["seq"] },
                            { "hiduke", row["hiduke"] },
                            { "naiyou", row["naiyou"] },
                            { "nyukin", row["nyukin"] },
                            { "shukkin", row["shukkin"] },
                            { "zankin", row["zankin"] },
                            { "bikou", row["bikou"] },
                            { "upduser", nameof(UpdateAll) }
                        };

                        count += manager.Update(sql, parameters);
                    }
                }

                if (dtDeleted != null)
                {
                    foreach (DataRow row in dtDeleted.Rows)
                    {
                        count += Delete(manager, row["seq"]);
                    }
                }

                manager.Commit();
            }

            // 変更を確定(modefied → unchanged)
            dtRireki.AcceptChanges();

            return count;
        }

        private int Delete(SQLManager manager, dynamic seq)
        {
            string sql = @"
                DELETE FROM kakeibo_rireki
                WHERE
                    seq = @seq
                ";

            Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>
            {
                { "seq", seq }
            };

            int count = manager.Delete(sql, parameters);
            return count;
        }

        public int Delete(DataGridView dataGridView)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            // 選択行がある場合
            int deletecount = 0;
            int selectcount = 0;
            if (dataGridView.Rows.GetRowCount(DataGridViewElementStates.Selected) > 0)
            {
                using (SQLManager manager = SQLManager.GetInstance(dataSource, dataBase, userId, password))
                {
                    // 選択行をデータベースから削除
                    foreach (DataGridViewRow row in dataGridView.SelectedRows)
                    {
                        int.TryParse(row.Cells["seq"].Value.ToString(), out int seq);
                        deletecount += Delete(manager, seq);
                    }
                    // コミット
                    manager.Commit();
                }
            }
            else
            {
                // 選択0件
            }

            logger.EndMethod(MethodBase.GetCurrentMethod().Name, $"delete:{deletecount}", $"select:{selectcount}");

            return deletecount;
        }
    }
}
