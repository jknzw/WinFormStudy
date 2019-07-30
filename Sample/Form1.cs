using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Sample.Service;

namespace Sample
{
    public partial class Form1 : Sample.Base.BaseForm
    {
        private string CsvFilePath { get; set; }

        public Form1()
        {
            InitializeComponent();

            // 使わないボタンを非表示にする
            buttonF3.Visible = false;
            buttonF4.Visible = false;
            buttonF7.Visible = false;
            buttonF8.Visible = false;
            buttonF9.Visible = false;
            buttonF10.Visible = false;

            // ボタンの有効無効を設定
            SetButtonEnabled(ActionMode.Init);
        }

        private enum ActionMode
        {
            Init = 0,
            CsvOpen = 1,
        }

        private void SetButtonEnabled(ActionMode mode)
        {
            // まず全てのボタンを無効にする
            SetAllBaseButtonEnabled(false);

            // 必要なボタンのみ有効にする
            switch (mode)
            {
                case ActionMode.CsvOpen:
                    buttonF1.Enabled = true;
                    buttonF2.Enabled = true;
                    buttonF5.Enabled = true;
                    buttonF6.Enabled = true;
                    buttonF11.Enabled = true;
                    buttonF12.Enabled = true;
                    break;
                case ActionMode.Init:
                default:
                    buttonF1.Enabled = true;
                    buttonF12.Enabled = true;
                    break;
            }
        }

        /// <summary>
        /// CSV選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result.Equals(DialogResult.OK))
            {
                CsvFilePath = openFileDialog1.FileName;
                this.Text = CsvFilePath;

                DataTable dt = Service.Form1Service.GetInstance(this).GetDataTable(CsvFilePath, "UTF-8");

                //string sort = string.Empty;
                if (dt.Columns.Count > 0)
                {
                    comboBoxSelect.Items.Clear();
                    for (int colIdx = 0; colIdx < dt.Columns.Count; colIdx++)
                    {
                        string colName = dt.Columns[colIdx].ColumnName;
                        comboBoxSelect.Items.Add(colName);
                        //sort += colName + ",";
                    }
                    comboBoxSelect.SelectedIndex = 0;
                }

                BindingSource bs = new BindingSource
                {
                    DataSource = dt
                };
                //bs.Sort = sort.Remove(sort.Length - 1);

                dataGridView1.DataSource = bs;

                for (int colIdx = 0; colIdx < dataGridView1.Columns.Count; colIdx++)
                {
                    // テキストを折り返して表示する
                    dataGridView1.Columns[colIdx].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                }

                SetButtonEnabled(ActionMode.CsvOpen);
            }
            else
            {
                SetButtonEnabled(ActionMode.Init);
            }
        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF2_Click(object sender, EventArgs e)
        {
            base.ButtonF1_Click(sender, e);

            //dataGridView1.DataSource = Form1Service.GetInstance()
            //    .Search(dataGridView1.DataSource as BindingSource, comboBoxSelect.Text, textBoxSelect.Text);

            // 試しコード
            Dictionary<string, dynamic> dicControls = this.GetControlDictionary();
            dataGridView1.DataSource = Form1Service.GetInstance(this)
                .Search(dataGridView1.DataSource as BindingSource, dicControls[nameof(comboBoxSelect)], dicControls[nameof(textBoxSelect)]);
        }

        /// <summary>
        /// クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF11_Click(object sender, EventArgs e)
        {
            base.ButtonF11_Click(sender, e);

            // テキストボックスクリア
            textBoxSelect.Text = string.Empty;

            // フィルタクリア
            dataGridView1.DataSource = Form1Service.GetInstance(this)
                .Clear(dataGridView1.DataSource as BindingSource);
        }

        /// <summary>
        /// 選択行削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF6_Click(object sender, EventArgs e)
        {
            base.ButtonF6_Click(sender, e);

            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);

            DialogResult result = MessageBox.Show("選択行を削除します。よろしいですか？", "削除確認", MessageBoxButtons.OKCancel);
            if (DialogResult.OK.Equals(result))
            {
                if (dataGridView1.SelectedRows.Count <= 0)
                {
                    MessageBox.Show("選択行がありません");
                    return;
                }

                int delCount = 0;
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (row.IsNewRow)
                    {
                        // 新規行は何もしない
                        continue;
                    }
                    else
                    {
                        // 新規行以外は削除する
                        dataGridView1.Rows.Remove(row);
                        delCount++;
                    }
                }

                if (delCount > 0)
                {
                    MessageBox.Show($"選択行を{delCount}行削除しました");
                }
                else
                {
                    // 新規行のみの場合
                    MessageBox.Show($"選択行は削除出来ません");
                }
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF5_Click(object sender, EventArgs e)
        {
            base.ButtonF5_Click(sender, e);

            DialogResult result = MessageBox.Show("保存します。よろしいですか？", "保存確認", MessageBoxButtons.OKCancel);
            if (DialogResult.OK.Equals(result))
            {
                int writeCount = Form1Service.GetInstance(this).Update(CsvFilePath, "UTF-8", (dataGridView1.DataSource as BindingSource).DataSource as DataTable);

                MessageBox.Show($"{writeCount}行保存しました");
            }
        }
    }
}
