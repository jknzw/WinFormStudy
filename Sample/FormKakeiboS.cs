using Sample.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Reflection;
using SampleLibrary;
using Sample.Utility;
using System.IO;

namespace Sample
{
    public partial class FormKakeiboS : Sample.Base.BaseForm
    {

        /// <summary>
        /// Model
        /// Serviceからの値を受け取るために使う
        /// </summary>
        private FormKakeiboSqlService.ModelKakeibo Model { get; set; } = null;

        public Logger Logger { get; } = Logger.GetInstance("kakeibo");

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormKakeiboS()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF1_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.WriteLine(MethodBase.GetCurrentMethod().Name);
                base.ButtonF1_Click(sender, e);

                // ▼▼▼ 業務処理 ▼▼▼
                FormKakeiboSqlService sv = FormKakeiboSqlService.GetInstance(this);

                // 登録
                int ret = sv.Touroku();
                if (ret != 0)
                {
                    Logger.WriteLine($"登録処理でエラーが発生しました。ErrorCode[{ret}]");
                }

                // 検索
                ret = Search(sv);
                if (ret < 0)
                {
                    Logger.WriteLine($"検索処理でエラーが発生しました。ErrorCode[{ret}]");
                }

                // スクロールを最終に移動
                gridRireki.ScrollToLast();
                // 選択を解除
                gridRireki.ClearSelection();
                // 最終行を選択
                gridRireki.Rows[gridRireki.Rows.GetLastRow(DataGridViewElementStates.None)].Selected = true;
                // ▲▲▲ 業務処理 ▲▲▲
            }
            catch (Exception ex)
            {
                Logger.WriteException(MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF2_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.WriteLine(MethodBase.GetCurrentMethod().Name);

                base.ButtonF2_Click(sender, e);
                // ▼▼▼ 業務処理 ▼▼▼
                customTextBoxKingaku.Text = string.Empty;
                customTextBoxBiko.Text = string.Empty;

                dateTimePicker1.Focus();
                // ▲▲▲ 業務処理 ▲▲▲
            }
            catch (Exception ex)
            {
                Logger.WriteException(MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message);
            }
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

            // ▲▲▲ 業務処理 ▲▲▲
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF6_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.WriteLine(MethodBase.GetCurrentMethod().Name);

                base.ButtonF6_Click(sender, e);
                // ▼▼▼ 業務処理 ▼▼▼

                if (DialogResult.OK == MessageBox.Show("削除します。よろしいですか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    FormKakeiboSqlService sv = FormKakeiboSqlService.GetInstance(this);
                    //int ret = sv.Delete(out FormKakeiboSqlService.ModelKakeibo value);
                    //if (ret > 0)
                    //{
                    //    // 画面更新
                    //    SetScreenValues(value);

                    //    MessageBox.Show($"{ret}件削除しました。");
                    //}
                    //else
                    //{
                    //    Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} Error:{ret}");
                    //}
                }
                // ▲▲▲ 業務処理 ▲▲▲
            }
            catch (Exception ex)
            {
                Logger.WriteException(MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message);
            }

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

            // ▲▲▲ 業務処理 ▲▲▲
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
        /// クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF11_Click(object sender, EventArgs e)
        {
            Logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            base.ButtonF11_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            // ▲▲▲ 業務処理 ▲▲▲
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormKakeibo_Load(object sender, EventArgs e)
        {
            try
            {
                Logger.WriteLine($"{MethodBase.GetCurrentMethod().Name}");

                FormKakeiboSqlService sv = FormKakeiboSqlService.GetInstance(this);
                Search(sv);
            }
            catch (Exception ex)
            {
                Logger.WriteException(MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sv"></param>
        /// <returns>取得件数</returns>
        private int Search(FormKakeiboSqlService sv)
        {
            Logger.StartMethod(MethodBase.GetCurrentMethod().Name);

            gridRireki.DataSource = new BindingSource();
            gridShukei.DataSource = new BindingSource();

            // 新規Model生成
            Model = new FormKakeiboSqlService.ModelKakeibo();

            // 履歴・残金を読み込む
            int ret = sv.GetKakeiboData(Model);

            // 検索結果を画面に設定
            SetScreenValues(Model);

            // 残金を編集不可にする
            gridRireki.Columns["zankin"].ReadOnly = true;

            Logger.EndMethod(MethodBase.GetCurrentMethod().Name, ret.ToString());
            return ret;
        }

        #region DataGridViewRireki
        /// <summary>
        /// 削除キー押下時
        /// </summary>
        private void DataGridViewRireki_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                Logger.WriteLine(MethodBase.GetCurrentMethod().Name);

                // Deleteイベントをキャンセルし、削除ボタン処理を行う
                buttonF6.PerformClick();
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                Logger.WriteException(MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void SetScreenValues(FormKakeiboSqlService.ModelKakeibo model)
        {
            Logger.WriteLine($"{MethodBase.GetCurrentMethod().Name}");

            // 残金・収入・支出を更新
            customReadOnlyTextBoxZankin.Text = model.Zankin.ToString();
            customReadOnlyTextBoxShunyu.Text = model.SumShunyu.ToString();
            customReadOnlyTextBoxShishutsu.Text = model.SumShishutsu.ToString();

            (gridRireki.DataSource as BindingSource).DataSource = model.RirekiTable;
            (gridShukei.DataSource as BindingSource).DataSource = model.ShukeiTable;

            // シーケンス列を非表示にする
            gridRireki.Columns["seq"].Visible = false;

            // ヘッダの表示名を変える
            gridRireki.Columns["hiduke"].HeaderText = "日付";
            gridRireki.Columns["naiyou"].HeaderText = "用途";
            gridRireki.Columns["nyukin"].HeaderText = "収入";
            gridRireki.Columns["shukkin"].HeaderText = "支出";
            gridRireki.Columns["zankin"].HeaderText = "残金";
            gridRireki.Columns["bikou"].HeaderText = "備考";
        }
        #endregion

        private void ComboBoxShukeiMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Logger.WriteLine($"{MethodBase.GetCurrentMethod().Name}");

                FormKakeiboSqlService sv = FormKakeiboSqlService.GetInstance(this);
                DataTable rireki = (gridRireki.DataSource as BindingSource).DataSource as DataTable;
                DataTable shukei = sv.GetShukeiTable(rireki);

                gridShukei.Columns.Clear();
                (gridShukei.DataSource as BindingSource).DataSource = shukei;
            }
            catch (Exception ex)
            {
                Logger.WriteException(MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void CmbRirekiFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Logger.WriteLine($"{MethodBase.GetCurrentMethod().Name}");

                FormKakeiboSqlService sv = FormKakeiboSqlService.GetInstance();
                //ChangeKakoRireki(sv);
            }
            catch (Exception ex)
            {
                Logger.WriteException(MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void CmbRirekiMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Logger.WriteLine($"{MethodBase.GetCurrentMethod().Name}");

                FormKakeiboSqlService sv = FormKakeiboSqlService.GetInstance();
                //ChangeKakoRireki(sv);
            }
            catch (Exception ex)
            {
                Logger.WriteException(MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void GridRireki_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // 更新処理
                FormKakeiboSqlService sv = FormKakeiboSqlService.GetInstance(this);
                int ret = sv.UpdateAll();
            }
            catch (Exception ex)
            {
                Logger.WriteException(MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
