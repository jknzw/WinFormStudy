using SampleLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample
{
    public partial class TestSqlForm : Sample.Base.BaseForm
    {
        public TestSqlForm()
        {
            InitializeComponent();
        }

        private Logger log = Logger.GetInstance(nameof(TestSqlForm));

        private SQLManager sqlManager = null;

        private void TestSqlFormDispose()
        {
            sqlManager?.Dispose();
            sqlManager = null;

            log?.Dispose();
            log = null;
        }

        /// <summary>
        /// Connect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF1_Click(object sender, EventArgs e)
        {
            log.StartMethod(MethodBase.GetCurrentMethod().Name);

            base.ButtonF1_Click(sender, e);

            // ▼▼▼ 業務処理 ▼▼▼

            Connect();

            // ▲▲▲ 業務処理 ▲▲▲
        }

        private void Connect()
        {
            log.StartMethod(MethodBase.GetCurrentMethod().Name);

            string datasource = tBoxDataSource.Text;
            string database = tBoxDataBase.Text;
            string userid = tBoxUserId.Text;
            string password = tBoxPassword.Text;

            try
            {
                sqlManager = SQLManager.GetInstance(datasource, database, userid, password);
                MessageBox.Show("接続しました。");
            }
            catch (Exception ex)
            {
                log.WriteException(MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show($"接続に失敗しました。{ex.Message}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF2_Click(object sender, EventArgs e)
        {
            base.ButtonF2_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            try
            {
                Execute("Insert", sqlManager.Insert);
            }
            catch (Exception ex)
            {
                log.WriteException(MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show($"Exception[{ex.Message}]");
            }

            // ▲▲▲ 業務処理 ▲▲▲
        }

        private void Execute(string sqltype, Func<string, Dictionary<string, dynamic>, int> func)
        {
            string sql = tBoxSql.Text;
            Dictionary<string, dynamic> parameters = GetSqlParameters();

            if (sql.IndexOf(sqltype, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                int ret = func(sql, parameters);
                MessageBox.Show($"{ret}件{sqltype}しました。");
            }
            else
            {
                MessageBox.Show($"{sqltype}文ではありません。");
            }
        }

        private Dictionary<string, dynamic> GetSqlParameters()
        {
            Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>();

            foreach (DataGridViewRow row in dgvParams.Rows)
            {
                string key = row.Cells["Key"].Value?.ToString();
                string value = row.Cells["Value"].Value?.ToString();

                if (string.IsNullOrWhiteSpace(key))
                {
                    // keyが空の場合除外
                }
                else
                {
                    parameters.Add(key, value);
                }
            }

            return parameters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF3_Click(object sender, EventArgs e)
        {
            base.ButtonF3_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            try
            {
                Execute("Update", sqlManager.Update);
            }
            catch (Exception ex)
            {
                log.WriteException(MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show($"Exception[{ex.Message}]");
            }

            // ▲▲▲ 業務処理 ▲▲▲
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF4_Click(object sender, EventArgs e)
        {
            base.ButtonF4_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            try
            {
                Execute("Delete", sqlManager.Delete);
            }
            catch (Exception ex)
            {
                log.WriteException(MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show($"Exception[{ex.Message}]");
            }

            // ▲▲▲ 業務処理 ▲▲▲
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF5_Click(object sender, EventArgs e)
        {
            base.ButtonF5_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            try
            {
                Search();
            }
            catch (Exception ex)
            {
                log.WriteException(MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show($"Exception[{ex.Message}]");
            }

            // ▲▲▲ 業務処理 ▲▲▲
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF6_Click(object sender, EventArgs e)
        {
            base.ButtonF6_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            // ▲▲▲ 業務処理 ▲▲▲
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF7_Click(object sender, EventArgs e)
        {
            base.ButtonF7_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼



            // ▲▲▲ 業務処理 ▲▲▲
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF8_Click(object sender, EventArgs e)
        {
            base.ButtonF8_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            try
            {
                sqlManager.Commit();
                MessageBox.Show($"Commitしました。");
            }
            catch (Exception ex)
            {
                log.WriteException(MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show($"Exception[{ex.Message}]");
            }

            // ▲▲▲ 業務処理 ▲▲▲
        }

        private void Search()
        {
            string sql = tBoxSql.Text;
            Dictionary<string, dynamic> parameters = GetSqlParameters();

            if (sql.IndexOf("Select", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                DataTable dt = sqlManager.Select(sql, parameters);
                dgv.DataSource = dt;
                MessageBox.Show($"{dt.Rows.Count}件Selectしました。");
            }
            else
            {
                MessageBox.Show($"Select文ではありません。");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF9_Click(object sender, EventArgs e)
        {
            base.ButtonF9_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            try
            {
                sqlManager.RollBack();
                MessageBox.Show($"RollBackしました。");
            }
            catch (Exception ex)
            {
                log.WriteException(MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show($"Exception[{ex.Message}]");
            }

            // ▲▲▲ 業務処理 ▲▲▲
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF10_Click(object sender, EventArgs e)
        {
            base.ButtonF10_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            // ▲▲▲ 業務処理 ▲▲▲
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF11_Click(object sender, EventArgs e)
        {
            base.ButtonF11_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            try
            {
                tBoxSql.Text = string.Empty;

                dgv.DataSource = null;

                dgvParams.Rows.Clear();
            }
            catch (Exception ex)
            {
                log.WriteException(MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show($"Exception[{ex.Message}]");
            }

            // ▲▲▲ 業務処理 ▲▲▲
        }


        private void TestForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void ListBoxSql_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (listBoxSql.SelectedItem.ToString())
            {
                case "select":
                    tBoxSql.Text =

@"select 
    * 
from kakeibo_rireki";

                    break;
                case "insert":
                    tBoxSql.Text =
@"INSERT 
INTO kakeibo_rireki ( 
	hiduke
	, naiyou
	, nyukin
	, shukkin
	, bikou
	, insuser
	, instime
	, upduser
	, updtime
) 
VALUES ( 
	@hiduke
	, @naiyou
	, @nyukin
	, @shukkin
	, @bikou
	, @insuser
	, @instime
	, @upduser
	, @updtime
)";
                    dgvParams.Rows.Clear();
                    dgvParams.Rows.Add("hiduke", DateTime.Now.ToString("yyyy/MM/dd"));
                    dgvParams.Rows.Add("naiyou", "内容");
                    dgvParams.Rows.Add("nyukin", "10");
                    dgvParams.Rows.Add("shukkin", "100");
                    dgvParams.Rows.Add("bikou", "備考");
                    dgvParams.Rows.Add("insuser", "追加ユーザー");
                    dgvParams.Rows.Add("instime", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    dgvParams.Rows.Add("upduser", "更新ユーザー");
                    dgvParams.Rows.Add("updtime", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    break;
                case "createtable":
                    tBoxSql.Text =

@"create table kakeibo_rireki
(
	hiduke date,
	naiyou varchar(20),
	nyukin int,
	shukkin int,
	bikou varchar(200),
	insuser varchar(20),
	instime date,
	upduser varchar(20),
	updtime date,
)";

                    break;
                case "droptable":
                    tBoxSql.Text = @"drop table kakeibo_rireki";
                    break;
                default:
                    break;
            }
        }
    }
}
