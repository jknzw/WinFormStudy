namespace Sample
{
    partial class Form1
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.comboBoxSelect = new System.Windows.Forms.ComboBox();
            this.textBoxSelect = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Text = "保存";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(99, 429);
            this.buttonDelete.Size = new System.Drawing.Size(85, 23);
            this.buttonDelete.Text = "選択行削除";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonSelect
            // 
            this.buttonSelect.Location = new System.Drawing.Point(15, 12);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(75, 23);
            this.buttonSelect.TabIndex = 7;
            this.buttonSelect.Text = "CSV選択";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // comboBoxSelect
            // 
            this.comboBoxSelect.FormattingEnabled = true;
            this.comboBoxSelect.Location = new System.Drawing.Point(96, 14);
            this.comboBoxSelect.Name = "comboBoxSelect";
            this.comboBoxSelect.Size = new System.Drawing.Size(128, 20);
            this.comboBoxSelect.TabIndex = 8;
            // 
            // textBoxSelect
            // 
            this.textBoxSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSelect.Location = new System.Drawing.Point(230, 14);
            this.textBoxSelect.Name = "textBoxSelect";
            this.textBoxSelect.Size = new System.Drawing.Size(396, 19);
            this.textBoxSelect.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(800, 464);
            this.Controls.Add(this.comboBoxSelect);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.textBoxSelect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Controls.SetChildIndex(this.buttonClear, 0);
            this.Controls.SetChildIndex(this.buttonUpdate, 0);
            this.Controls.SetChildIndex(this.buttonDelete, 0);
            this.Controls.SetChildIndex(this.buttonEnd, 0);
            this.Controls.SetChildIndex(this.buttonSearch, 0);
            this.Controls.SetChildIndex(this.textBoxSelect, 0);
            this.Controls.SetChildIndex(this.buttonSelect, 0);
            this.Controls.SetChildIndex(this.comboBoxSelect, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.ComboBox comboBoxSelect;
        private System.Windows.Forms.TextBox textBoxSelect;
    }
}
