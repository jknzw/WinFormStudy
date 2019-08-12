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
    public partial class FormKakeibo : Sample.Base.BaseForm
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly Logger logger = Logger.GetInstance("kakeibo");

        /// <summary>
        /// Model
        /// Serviceからの値を受け取るために使う
        /// </summary>
        private FormKakeiboService.ModelKakeibo Model { get; set; } = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormKakeibo()
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
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);
            base.ButtonF1_Click(sender, e);

            // ▼▼▼ 業務処理 ▼▼▼
            FormKakeiboService sv = FormKakeiboService.GetInstance(this);

            // 登録
            int ret = sv.Touroku();
            if (ret != 0)
            {
                logger.WriteLine($"登録処理でエラーが発生しました。ErrorCode[{ret}]");
            }

            // 検索
            ret = Search(sv);
            if (ret != 0)
            {
                logger.WriteLine($"検索処理でエラーが発生しました。ErrorCode[{ret}]");
            }

            // スクロールを最終に移動
            gridRireki.ScrollToLast();
            // 選択を解除
            gridRireki.ClearSelection();
            // 最終行を選択
            gridRireki.Rows[gridRireki.Rows.GetLastRow(DataGridViewElementStates.None)].Selected = true;

            // ▲▲▲ 業務処理 ▲▲▲
        }

        /// <summary>
        /// クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF2_Click(object sender, EventArgs e)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            base.ButtonF2_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼
            customTextBoxKingaku.Text = string.Empty;
            customTextBoxBiko.Text = string.Empty;

            dateTimePicker1.Focus();
            // ▲▲▲ 業務処理 ▲▲▲
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
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            base.ButtonF6_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            if (DialogResult.OK == MessageBox.Show("削除します。よろしいですか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                FormKakeiboService sv = FormKakeiboService.GetInstance(this);
                int ret = sv.Delete(Model.RirekiFile, gridRireki, out FormKakeiboService.ModelKakeibo value);
                if (ret > 0)
                {
                    // 画面更新
                    SetScreenValues(value);

                    MessageBox.Show($"{ret}件削除しました。");
                }
                else
                {
                    Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} Error:{ret}");
                }
            }
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
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

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
            logger.WriteLine($"{MethodBase.GetCurrentMethod().Name}");

            FormKakeiboService sv = FormKakeiboService.GetInstance(this);
            Search(sv);
        }

        private int Search(FormKakeiboService sv)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            gridRireki.DataSource = new BindingSource();
            gridShukei.DataSource = new BindingSource();

            // 新規Model生成
            FormKakeiboService.ModelKakeibo model = new FormKakeiboService.ModelKakeibo
            {
                // 履歴ファイルパス取得
                RirekiFile = sv.GetRirekiFilePath(),

                // 過去履歴取得
                RirekiFiles = sv.GetRirekiFiles()
            };

            // 履歴ファイルを読み込む
            int ret = sv.GetRireki(model);
            if (ret == 0)
            {
                // 正常
                Model = model;
            }
            else if (ret == -1)
            {
                // 履歴ファイルが無い場合
                if (DialogResult.OK == MessageBox.Show("月が変わりました。履歴ファイルを更新しますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    // 新しいファイルに変更する
                    sv.CreateNewRirekiFile(model.RirekiFile);

                    // 再読込
                    ret = sv.GetRireki(model);
                    if (ret == 0)
                    {
                        // 正常
                        Model = model;
                    }
                    else
                    {
                        // 履歴ファイル取得エラー
                        logger.WriteError($"履歴取得に失敗しました。", ret);
                        return -2;
                    }
                }
                else
                {
                    // 過去の最新のファイルを使う
                    model.RirekiFile = model.RirekiFiles.OrderByDescending(x => x).First();

                    // 再読込
                    ret = sv.GetRireki(model);
                    if (ret == 0)
                    {
                        // 正常
                        Model = model;
                    }
                    else
                    {
                        // 履歴ファイル取得エラー
                        logger.WriteError($"履歴取得に失敗しました。", ret);
                        return -3;
                    }
                }
            }
            else
            {
                // 履歴ファイル取得エラー
                logger.WriteError($"履歴取得に失敗しました。", ret);
                return -1;
            }

            // コンボボックスの生成
            SortedDictionary<string, string> comboSource = new SortedDictionary<string, string>();
            foreach (string rirekiFile in Model.RirekiFiles)
            {
                string filename = Path.GetFileNameWithoutExtension(rirekiFile);
                if (!rirekiFile.Equals(Model.RirekiFile))
                {
                    comboSource.Add(filename, rirekiFile);
                }
            }
            if (comboSource.Count > 0)
            {
                // 対象が1件以上の場合バインド
                cmbRirekiFiles.DataSource = new BindingSource(comboSource.Reverse(), null);
                cmbRirekiFiles.DisplayMember = "Key";
                cmbRirekiFiles.ValueMember = "Value";

                if (cmbRirekiFiles.Items.Count > 0)
                {
                    cmbRirekiFiles.SelectedIndex = 0;
                }
            }

            // 検索結果を画面に設定
            SetScreenValues(Model);
            // 残金を編集不可にする
            gridRireki.Columns["残金"].ReadOnly = true;

            return 0;
        }

        #region DataGridViewRireki
        /// <summary>
        /// 削除キー押下時
        /// </summary>
        private void DataGridViewRireki_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            // Deleteイベントをキャンセルし、削除ボタン処理を行う
            buttonF6.PerformClick();
            e.Cancel = true;
        }

        private void DataGridViewRireki_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            FormKakeiboService sv = FormKakeiboService.GetInstance(this);
            int ret = sv.UpdateAll(Model.RirekiFile, gridRireki, out FormKakeiboService.ModelKakeibo value);
            if (ret >= 0)
            {
                // 正常
                SetScreenValues(value);
            }
            else
            {
                logger.WriteLine($"{MethodBase.GetCurrentMethod().Name} Error:{ret}");
            }
        }

        private void SetScreenValues(FormKakeiboService.ModelKakeibo model)
        {
            logger.WriteLine($"{MethodBase.GetCurrentMethod().Name}");

            // 残金・収入・支出を更新
            customReadOnlyTextBoxZankin.Text = model.Zankin.ToString();
            customReadOnlyTextBoxShunyu.Text = model.SumShunyu.ToString();
            customReadOnlyTextBoxShishutsu.Text = model.SumShishutsu.ToString();

            (gridRireki.DataSource as BindingSource).DataSource = model.RirekiTable;
            (gridShukei.DataSource as BindingSource).DataSource = model.ShukeiTable;
        }
        #endregion

        private void ComboBoxShukeiMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            logger.WriteLine($"{MethodBase.GetCurrentMethod().Name}");

            FormKakeiboService sv = FormKakeiboService.GetInstance(this);
            DataTable rireki = (gridRireki.DataSource as BindingSource).DataSource as DataTable;
            DataTable shukei = sv.GetShukeiTable(rireki);

            gridShukei.Columns.Clear();
            (gridShukei.DataSource as BindingSource).DataSource = shukei;

        }

        private void CmbRirekiFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            logger.WriteLine($"{MethodBase.GetCurrentMethod().Name}");

            FormKakeiboService sv = FormKakeiboService.GetInstance();
            ChangeKakoRireki(sv);
        }

        private void CmbRirekiMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            logger.WriteLine($"{MethodBase.GetCurrentMethod().Name}");

            FormKakeiboService sv = FormKakeiboService.GetInstance();
            ChangeKakoRireki(sv);
        }

        private void ChangeKakoRireki(FormKakeiboService sv)
        {
            logger.WriteLine($"{MethodBase.GetCurrentMethod().Name}");

            if (cmbRirekiFiles.SelectedIndex >= 0)
            {
                // 選択している項目がある場合
                // 履歴ファイルを読み込む
                string rirekiFile = ((KeyValuePair<string, string>)cmbRirekiFiles.SelectedItem).Value;
                DataTable dt = sv.GetKakoRirekiTable(rirekiFile, cmbRirekiMode.Text);
                gridKakoRireki.DataSource = new BindingSource(dt, null);
            }
        }
    }
}
