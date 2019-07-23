namespace Sample
{
    partial class TestForm
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
            this.textBoxLogFilePath = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxBoundedCapacity = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxWriteDelay = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBoxTaskTimeout = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonF1
            // 
            this.buttonF1.Text = "実行";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 13);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 101;
            this.label13.Text = "LogFilePath";
            // 
            // textBoxLogFilePath
            // 
            this.textBoxLogFilePath.Location = new System.Drawing.Point(112, 10);
            this.textBoxLogFilePath.Name = "textBoxLogFilePath";
            this.textBoxLogFilePath.Size = new System.Drawing.Size(100, 19);
            this.textBoxLogFilePath.TabIndex = 102;
            this.textBoxLogFilePath.Text = "./log.txt";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 38);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 12);
            this.label14.TabIndex = 101;
            this.label14.Text = "BoundedCapacity";
            // 
            // textBoxBoundedCapacity
            // 
            this.textBoxBoundedCapacity.Location = new System.Drawing.Point(112, 35);
            this.textBoxBoundedCapacity.Name = "textBoxBoundedCapacity";
            this.textBoxBoundedCapacity.Size = new System.Drawing.Size(100, 19);
            this.textBoxBoundedCapacity.TabIndex = 102;
            this.textBoxBoundedCapacity.Text = "10000";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 65);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(83, 12);
            this.label15.TabIndex = 101;
            this.label15.Text = "WriteDelay(ms)";
            // 
            // textBoxWriteDelay
            // 
            this.textBoxWriteDelay.Location = new System.Drawing.Point(112, 62);
            this.textBoxWriteDelay.Name = "textBoxWriteDelay";
            this.textBoxWriteDelay.Size = new System.Drawing.Size(100, 19);
            this.textBoxWriteDelay.TabIndex = 102;
            this.textBoxWriteDelay.Text = "5000";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 90);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(94, 12);
            this.label16.TabIndex = 101;
            this.label16.Text = "TaskTimeout(ms)";
            // 
            // textBoxTaskTimeout
            // 
            this.textBoxTaskTimeout.Location = new System.Drawing.Point(112, 87);
            this.textBoxTaskTimeout.Name = "textBoxTaskTimeout";
            this.textBoxTaskTimeout.Size = new System.Drawing.Size(100, 19);
            this.textBoxTaskTimeout.TabIndex = 102;
            this.textBoxTaskTimeout.Text = "0";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.textBoxTaskTimeout);
            this.Controls.Add(this.textBoxWriteDelay);
            this.Controls.Add(this.textBoxBoundedCapacity);
            this.Controls.Add(this.textBoxLogFilePath);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Name = "TestForm";
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
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.textBoxLogFilePath, 0);
            this.Controls.SetChildIndex(this.textBoxBoundedCapacity, 0);
            this.Controls.SetChildIndex(this.textBoxWriteDelay, 0);
            this.Controls.SetChildIndex(this.textBoxTaskTimeout, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxLogFilePath;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxBoundedCapacity;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxWriteDelay;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxTaskTimeout;
    }
}
