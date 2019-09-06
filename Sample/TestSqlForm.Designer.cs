namespace Sample
{
    partial class TestSqlForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                TestSqlFormDispose();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label13 = new System.Windows.Forms.Label();
            this.tBoxDataSource = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tBoxDataBase = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tBoxUserId = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tBoxPassword = new System.Windows.Forms.TextBox();
            this.tBoxSql = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.dgv = new WindowsFormsControlLibrary.CustomDataGridView();
            this.label18 = new System.Windows.Forms.Label();
            this.dgvParams = new WindowsFormsControlLibrary.CustomDataGridView();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listBoxSql = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonF1
            // 
            this.buttonF1.Text = "Connect";
            // 
            // buttonF2
            // 
            this.buttonF2.Text = "Insert";
            // 
            // buttonF3
            // 
            this.buttonF3.Text = "Update";
            // 
            // buttonF4
            // 
            this.buttonF4.Text = "Delete";
            // 
            // buttonF5
            // 
            this.buttonF5.Text = "Select";
            // 
            // buttonF6
            // 
            this.buttonF6.Text = "F6";
            // 
            // buttonF8
            // 
            this.buttonF8.Text = "Commit";
            // 
            // buttonF9
            // 
            this.buttonF9.Text = "RollBack";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 13);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 12);
            this.label13.TabIndex = 101;
            this.label13.Text = "DataSource";
            // 
            // tBoxDataSource
            // 
            this.tBoxDataSource.Location = new System.Drawing.Point(112, 10);
            this.tBoxDataSource.Name = "tBoxDataSource";
            this.tBoxDataSource.Size = new System.Drawing.Size(100, 19);
            this.tBoxDataSource.TabIndex = 102;
            this.tBoxDataSource.Text = "localhost";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 38);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(55, 12);
            this.label14.TabIndex = 101;
            this.label14.Text = "DataBase";
            // 
            // tBoxDataBase
            // 
            this.tBoxDataBase.Location = new System.Drawing.Point(112, 35);
            this.tBoxDataBase.Name = "tBoxDataBase";
            this.tBoxDataBase.Size = new System.Drawing.Size(100, 19);
            this.tBoxDataBase.TabIndex = 102;
            this.tBoxDataBase.Text = "SampleDb";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 65);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(38, 12);
            this.label15.TabIndex = 101;
            this.label15.Text = "UserId";
            // 
            // tBoxUserId
            // 
            this.tBoxUserId.Location = new System.Drawing.Point(112, 62);
            this.tBoxUserId.Name = "tBoxUserId";
            this.tBoxUserId.Size = new System.Drawing.Size(100, 19);
            this.tBoxUserId.TabIndex = 102;
            this.tBoxUserId.Text = "SampleUser";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 90);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(54, 12);
            this.label16.TabIndex = 101;
            this.label16.Text = "Password";
            // 
            // tBoxPassword
            // 
            this.tBoxPassword.Location = new System.Drawing.Point(112, 87);
            this.tBoxPassword.Name = "tBoxPassword";
            this.tBoxPassword.Size = new System.Drawing.Size(100, 19);
            this.tBoxPassword.TabIndex = 102;
            this.tBoxPassword.Text = "1234SampleUser";
            // 
            // tBoxSql
            // 
            this.tBoxSql.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tBoxSql.Location = new System.Drawing.Point(334, 10);
            this.tBoxSql.Multiline = true;
            this.tBoxSql.Name = "tBoxSql";
            this.tBoxSql.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tBoxSql.Size = new System.Drawing.Size(638, 111);
            this.tBoxSql.TabIndex = 103;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(218, 13);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(26, 12);
            this.label17.TabIndex = 101;
            this.label17.Text = "SQL";
            // 
            // dgv
            // 
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(250, 127);
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 21;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(722, 368);
            this.dgv.TabIndex = 104;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(12, 112);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(62, 12);
            this.label18.TabIndex = 101;
            this.label18.Text = "parameters";
            // 
            // dgvParams
            // 
            this.dgvParams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvParams.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvParams.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Key,
            this.Value});
            this.dgvParams.Location = new System.Drawing.Point(14, 127);
            this.dgvParams.Name = "dgvParams";
            this.dgvParams.RowTemplate.Height = 21;
            this.dgvParams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvParams.Size = new System.Drawing.Size(230, 368);
            this.dgvParams.TabIndex = 104;
            // 
            // Key
            // 
            this.Key.HeaderText = "Key";
            this.Key.Name = "Key";
            this.Key.Width = 49;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.Width = 59;
            // 
            // listBoxSql
            // 
            this.listBoxSql.FormattingEnabled = true;
            this.listBoxSql.ItemHeight = 12;
            this.listBoxSql.Items.AddRange(new object[] {
            "select",
            "insert",
            "update",
            "delete",
            "createtable",
            "droptable"});
            this.listBoxSql.Location = new System.Drawing.Point(250, 10);
            this.listBoxSql.Name = "listBoxSql";
            this.listBoxSql.Size = new System.Drawing.Size(75, 112);
            this.listBoxSql.TabIndex = 105;
            this.listBoxSql.SelectedIndexChanged += new System.EventHandler(this.ListBoxSql_SelectedIndexChanged);
            // 
            // TestSqlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.listBoxSql);
            this.Controls.Add(this.dgvParams);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.tBoxSql);
            this.Controls.Add(this.tBoxPassword);
            this.Controls.Add(this.tBoxUserId);
            this.Controls.Add(this.tBoxDataBase);
            this.Controls.Add(this.tBoxDataSource);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label13);
            this.Name = "TestSqlForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestForm_FormClosing);
            this.Controls.SetChildIndex(this.buttonF1, 0);
            this.Controls.SetChildIndex(this.buttonF11, 0);
            this.Controls.SetChildIndex(this.buttonF4, 0);
            this.Controls.SetChildIndex(this.buttonF7, 0);
            this.Controls.SetChildIndex(this.buttonF10, 0);
            this.Controls.SetChildIndex(this.buttonF2, 0);
            this.Controls.SetChildIndex(this.buttonF3, 0);
            this.Controls.SetChildIndex(this.buttonF5, 0);
            this.Controls.SetChildIndex(this.buttonF8, 0);
            this.Controls.SetChildIndex(this.buttonF6, 0);
            this.Controls.SetChildIndex(this.buttonF9, 0);
            this.Controls.SetChildIndex(this.buttonF12, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.label17, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.label18, 0);
            this.Controls.SetChildIndex(this.tBoxDataSource, 0);
            this.Controls.SetChildIndex(this.tBoxDataBase, 0);
            this.Controls.SetChildIndex(this.tBoxUserId, 0);
            this.Controls.SetChildIndex(this.tBoxPassword, 0);
            this.Controls.SetChildIndex(this.tBoxSql, 0);
            this.Controls.SetChildIndex(this.dgv, 0);
            this.Controls.SetChildIndex(this.dgvParams, 0);
            this.Controls.SetChildIndex(this.listBoxSql, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParams)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tBoxDataSource;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tBoxDataBase;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tBoxUserId;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tBoxPassword;
        private System.Windows.Forms.TextBox tBoxSql;
        private System.Windows.Forms.Label label17;
        private WindowsFormsControlLibrary.CustomDataGridView dgv;
        private System.Windows.Forms.Label label18;
        private WindowsFormsControlLibrary.CustomDataGridView dgvParams;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.ListBox listBoxSql;
    }
}
