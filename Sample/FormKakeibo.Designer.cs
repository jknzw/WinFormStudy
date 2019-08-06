namespace Sample
{
    partial class FormKakeibo
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
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.customReadOnlyTextBoxZankin = new WindowsFormsControlLibrary.CustomReadOnlyTextBox();
            this.customReadOnlyTextBoxShishutsu = new WindowsFormsControlLibrary.CustomReadOnlyTextBox();
            this.customReadOnlyTextBoxShunyu = new WindowsFormsControlLibrary.CustomReadOnlyTextBox();
            this.dataGridViewRireki = new System.Windows.Forms.DataGridView();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.comboBoxYouto = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.customTextBoxKingaku = new WindowsFormsControlLibrary.CustomTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.customTextBoxBiko = new WindowsFormsControlLibrary.CustomTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridViewShukei = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRireki)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShukei)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonF1
            // 
            this.buttonF1.Text = "登録";
            // 
            // buttonF2
            // 
            this.buttonF2.Text = "クリア";
            // 
            // buttonF3
            // 
            this.buttonF3.Visible = false;
            // 
            // buttonF11
            // 
            this.buttonF11.Visible = false;
            // 
            // buttonF4
            // 
            this.buttonF4.Visible = false;
            // 
            // buttonF5
            // 
            this.buttonF5.Visible = false;
            // 
            // buttonF7
            // 
            this.buttonF7.Visible = false;
            // 
            // buttonF8
            // 
            this.buttonF8.Visible = false;
            // 
            // buttonF9
            // 
            this.buttonF9.Visible = false;
            // 
            // buttonF10
            // 
            this.buttonF10.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(17, 13);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 101;
            this.label13.Text = "残金";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(171, 13);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 12);
            this.label14.TabIndex = 101;
            this.label14.Text = "支出";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(332, 13);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 101;
            this.label15.Text = "収入";
            // 
            // customReadOnlyTextBoxZankin
            // 
            this.customReadOnlyTextBoxZankin.Location = new System.Drawing.Point(52, 10);
            this.customReadOnlyTextBoxZankin.Name = "customReadOnlyTextBoxZankin";
            this.customReadOnlyTextBoxZankin.ReadOnly = true;
            this.customReadOnlyTextBoxZankin.Size = new System.Drawing.Size(113, 19);
            this.customReadOnlyTextBoxZankin.TabIndex = 102;
            this.customReadOnlyTextBoxZankin.TabStop = false;
            // 
            // customReadOnlyTextBoxShishutsu
            // 
            this.customReadOnlyTextBoxShishutsu.Location = new System.Drawing.Point(206, 10);
            this.customReadOnlyTextBoxShishutsu.Name = "customReadOnlyTextBoxShishutsu";
            this.customReadOnlyTextBoxShishutsu.ReadOnly = true;
            this.customReadOnlyTextBoxShishutsu.Size = new System.Drawing.Size(119, 19);
            this.customReadOnlyTextBoxShishutsu.TabIndex = 102;
            this.customReadOnlyTextBoxShishutsu.TabStop = false;
            // 
            // customReadOnlyTextBoxShunyu
            // 
            this.customReadOnlyTextBoxShunyu.Location = new System.Drawing.Point(367, 10);
            this.customReadOnlyTextBoxShunyu.Name = "customReadOnlyTextBoxShunyu";
            this.customReadOnlyTextBoxShunyu.ReadOnly = true;
            this.customReadOnlyTextBoxShunyu.Size = new System.Drawing.Size(123, 19);
            this.customReadOnlyTextBoxShunyu.TabIndex = 102;
            this.customReadOnlyTextBoxShunyu.TabStop = false;
            // 
            // dataGridViewRireki
            // 
            this.dataGridViewRireki.AllowUserToAddRows = false;
            this.dataGridViewRireki.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRireki.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewRireki.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewRireki.Name = "dataGridViewRireki";
            this.dataGridViewRireki.RowTemplate.Height = 21;
            this.dataGridViewRireki.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRireki.Size = new System.Drawing.Size(946, 400);
            this.dataGridViewRireki.TabIndex = 0;
            this.dataGridViewRireki.TabStop = false;
            this.dataGridViewRireki.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewRireki_CellEndEdit);
            this.dataGridViewRireki.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.DataGridViewRireki_UserDeletingRow);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.dateTimePicker1.Location = new System.Drawing.Point(16, 38);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(149, 19);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // comboBoxYouto
            // 
            this.comboBoxYouto.FormattingEnabled = true;
            this.comboBoxYouto.Items.AddRange(new object[] {
            "食費",
            "生活雑貨",
            "娯楽",
            "医療",
            "光熱費",
            "繰り越し",
            "収入",
            "その他"});
            this.comboBoxYouto.Location = new System.Drawing.Point(206, 37);
            this.comboBoxYouto.Name = "comboBoxYouto";
            this.comboBoxYouto.Size = new System.Drawing.Size(121, 20);
            this.comboBoxYouto.TabIndex = 1;
            this.comboBoxYouto.Text = "食費";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(171, 40);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 12);
            this.label16.TabIndex = 101;
            this.label16.Text = "内容";
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "支出",
            "収入"});
            this.comboBoxType.Location = new System.Drawing.Point(368, 36);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(54, 20);
            this.comboBoxType.TabIndex = 2;
            this.comboBoxType.Text = "支出";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(333, 40);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 12);
            this.label17.TabIndex = 101;
            this.label17.Text = "金額";
            // 
            // customTextBoxKingaku
            // 
            this.customTextBoxKingaku.Location = new System.Drawing.Point(428, 37);
            this.customTextBoxKingaku.Name = "customTextBoxKingaku";
            this.customTextBoxKingaku.Size = new System.Drawing.Size(100, 19);
            this.customTextBoxKingaku.TabIndex = 3;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(534, 40);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(29, 12);
            this.label18.TabIndex = 101;
            this.label18.Text = "備考";
            // 
            // customTextBoxBiko
            // 
            this.customTextBoxBiko.Location = new System.Drawing.Point(569, 37);
            this.customTextBoxBiko.Name = "customTextBoxBiko";
            this.customTextBoxBiko.Size = new System.Drawing.Size(403, 19);
            this.customTextBoxBiko.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 63);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(960, 432);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridViewRireki);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(952, 406);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "履歴";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridViewShukei);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(952, 406);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "集計";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridViewShukei
            // 
            this.dataGridViewShukei.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewShukei.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewShukei.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewShukei.Name = "dataGridViewShukei";
            this.dataGridViewShukei.RowTemplate.Height = 21;
            this.dataGridViewShukei.Size = new System.Drawing.Size(946, 400);
            this.dataGridViewShukei.TabIndex = 1;
            // 
            // FormKakeibo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.customTextBoxBiko);
            this.Controls.Add(this.customTextBoxKingaku);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.comboBoxYouto);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.customReadOnlyTextBoxShunyu);
            this.Controls.Add(this.customReadOnlyTextBoxShishutsu);
            this.Controls.Add(this.customReadOnlyTextBoxZankin);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Name = "FormKakeibo";
            this.Text = "家計簿";
            this.Load += new System.EventHandler(this.FormKakeibo_Load);
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
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.label17, 0);
            this.Controls.SetChildIndex(this.label18, 0);
            this.Controls.SetChildIndex(this.customReadOnlyTextBoxZankin, 0);
            this.Controls.SetChildIndex(this.customReadOnlyTextBoxShishutsu, 0);
            this.Controls.SetChildIndex(this.customReadOnlyTextBoxShunyu, 0);
            this.Controls.SetChildIndex(this.dateTimePicker1, 0);
            this.Controls.SetChildIndex(this.comboBoxYouto, 0);
            this.Controls.SetChildIndex(this.comboBoxType, 0);
            this.Controls.SetChildIndex(this.customTextBoxKingaku, 0);
            this.Controls.SetChildIndex(this.customTextBoxBiko, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRireki)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShukei)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private WindowsFormsControlLibrary.CustomReadOnlyTextBox customReadOnlyTextBoxZankin;
        private WindowsFormsControlLibrary.CustomReadOnlyTextBox customReadOnlyTextBoxShishutsu;
        private WindowsFormsControlLibrary.CustomReadOnlyTextBox customReadOnlyTextBoxShunyu;
        private System.Windows.Forms.DataGridView dataGridViewRireki;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox comboBoxYouto;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label label17;
        private WindowsFormsControlLibrary.CustomTextBox customTextBoxKingaku;
        private System.Windows.Forms.Label label18;
        private WindowsFormsControlLibrary.CustomTextBox customTextBoxBiko;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridViewShukei;
    }
}
