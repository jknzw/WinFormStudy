using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sample
{
    public partial class Form1 : Sample.BaseForm
    {
        private string CsvFilePath { get; set; }

        public Form1()
        {
            InitializeComponent();
            SetButtonEnabled(ActionMode.Init);
        }

        private enum ActionMode
        {
            Init = 0,
            CsvOpen = 1,
        }

        private void SetButtonEnabled(ActionMode mode)
        {
            switch (mode)
            {
                case ActionMode.CsvOpen:
                    buttonClear.Enabled = true;
                    buttonSearch.Enabled = true;
                    buttonUpdate.Enabled = true;
                    buttonDelete.Enabled = true;
                    break;
                case ActionMode.Init:
                default:
                    buttonClear.Enabled = false;
                    buttonSearch.Enabled = false;
                    buttonUpdate.Enabled = false;
                    buttonDelete.Enabled = false;
                    break;
            }
        }

        public void SelectButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result.Equals(DialogResult.OK))
            {
                CsvFilePath = openFileDialog1.FileName;
                this.Text = CsvFilePath;

                DataTable dt = Form1Service.GetInstance().GetDataTable(CsvFilePath, "UTF-8");

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

                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                //bs.Sort = sort.Remove(sort.Length - 1);

                dataGridView1.DataSource = bs;

                for (int colIdx = 0; colIdx < dataGridView1.Columns.Count; colIdx++)
                {
                    dataGridView1.Columns[colIdx].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                }

                SetButtonEnabled(ActionMode.CsvOpen);
            }
            else
            {
                SetButtonEnabled(ActionMode.Init);
            }
        }

        public override void SearchButton_Click(object sender, EventArgs e)
        {
            base.SearchButton_Click(sender, e);

            dataGridView1.DataSource = Form1Service.GetInstance()
                .Search(dataGridView1.DataSource as BindingSource, comboBoxSelect.Text, textBoxSelect.Text);
        }

        public override void ClearButton_Click(object sender, EventArgs e)
        {
            base.ClearButton_Click(sender, e);

            // フィルタクリア
            dataGridView1.DataSource = Form1Service.GetInstance()
                .Clear(dataGridView1.DataSource as BindingSource);

        }
        public override void DeleteButton_Click(object sender, EventArgs e)
        {
            base.DeleteButton_Click(sender, e);

            DialogResult result = MessageBox.Show("選択行を削除します。よろしいですか？", "削除確認", MessageBoxButtons.OKCancel);
            if (DialogResult.OK.Equals(result))
            {
                if (dataGridView1.SelectedRows.Count <= 0)
                {
                    MessageBox.Show("選択行がありません");
                    return;
                }

                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.Remove(row);
                }

                MessageBox.Show("選択行を削除しました");
            }
        }

        public override void UpdateButton_Click(object sender, EventArgs e)
        {
            base.UpdateButton_Click(sender, e);

            DialogResult result = MessageBox.Show("保存します。よろしいですか？", "保存確認", MessageBoxButtons.OKCancel);
            if (DialogResult.OK.Equals(result))
            {
                int writeCount = Form1Service.GetInstance().Update(CsvFilePath, "UTF-8", (dataGridView1.DataSource as BindingSource).DataSource as DataTable);

                MessageBox.Show("保存しました");
            }
        }
    }
}
