namespace Sample
{
    partial class FormTcpClient
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
            this.tboxIP = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tboxPort = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.listBoxUser = new System.Windows.Forms.ListBox();
            this.listViewLog = new System.Windows.Forms.ListView();
            this.colHeaderLog = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tboxMessage = new System.Windows.Forms.TextBox();
            this.cboxTarget = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tboxName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonF1
            // 
            this.buttonF1.TabIndex = 7;
            this.buttonF1.Text = "Connect";
            // 
            // buttonF2
            // 
            this.buttonF2.TabIndex = 8;
            this.buttonF2.Text = "DisConnect";
            // 
            // buttonF3
            // 
            this.buttonF3.TabIndex = 9;
            // 
            // buttonF12
            // 
            this.buttonF12.TabIndex = 18;
            this.buttonF12.Text = "Close";
            // 
            // buttonF11
            // 
            this.buttonF11.TabIndex = 17;
            this.buttonF11.Text = "Clear";
            // 
            // buttonF4
            // 
            this.buttonF4.TabIndex = 10;
            // 
            // buttonF5
            // 
            this.buttonF5.TabIndex = 11;
            this.buttonF5.Text = "Send";
            // 
            // buttonF6
            // 
            this.buttonF6.TabIndex = 12;
            // 
            // buttonF7
            // 
            this.buttonF7.TabIndex = 13;
            // 
            // buttonF8
            // 
            this.buttonF8.TabIndex = 14;
            // 
            // buttonF9
            // 
            this.buttonF9.TabIndex = 15;
            // 
            // buttonF10
            // 
            this.buttonF10.TabIndex = 16;
            // 
            // tboxIP
            // 
            this.tboxIP.Location = new System.Drawing.Point(34, 12);
            this.tboxIP.Name = "tboxIP";
            this.tboxIP.Size = new System.Drawing.Size(100, 19);
            this.tboxIP.TabIndex = 0;
            this.tboxIP.Text = "127.0.0.1";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 15);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 12);
            this.label13.TabIndex = 103;
            this.label13.Text = "IP";
            // 
            // tboxPort
            // 
            this.tboxPort.Location = new System.Drawing.Point(174, 12);
            this.tboxPort.Name = "tboxPort";
            this.tboxPort.Size = new System.Drawing.Size(100, 19);
            this.tboxPort.TabIndex = 1;
            this.tboxPort.Text = "2001";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(140, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(26, 12);
            this.label14.TabIndex = 103;
            this.label14.Text = "Port";
            // 
            // listBoxUser
            // 
            this.listBoxUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxUser.FormattingEnabled = true;
            this.listBoxUser.ItemHeight = 12;
            this.listBoxUser.Location = new System.Drawing.Point(822, 36);
            this.listBoxUser.Name = "listBoxUser";
            this.listBoxUser.Size = new System.Drawing.Size(120, 448);
            this.listBoxUser.TabIndex = 4;
            // 
            // listViewLog
            // 
            this.listViewLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHeaderLog});
            this.listViewLog.HideSelection = false;
            this.listViewLog.Location = new System.Drawing.Point(13, 36);
            this.listViewLog.Name = "listViewLog";
            this.listViewLog.Size = new System.Drawing.Size(803, 423);
            this.listViewLog.TabIndex = 3;
            this.listViewLog.UseCompatibleStateImageBehavior = false;
            this.listViewLog.View = System.Windows.Forms.View.Details;
            // 
            // colHeaderLog
            // 
            this.colHeaderLog.Text = "Log";
            this.colHeaderLog.Width = 600;
            // 
            // tboxMessage
            // 
            this.tboxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxMessage.Location = new System.Drawing.Point(140, 465);
            this.tboxMessage.Name = "tboxMessage";
            this.tboxMessage.Size = new System.Drawing.Size(676, 19);
            this.tboxMessage.TabIndex = 6;
            // 
            // cboxTarget
            // 
            this.cboxTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboxTarget.FormattingEnabled = true;
            this.cboxTarget.Items.AddRange(new object[] {
            "全員"});
            this.cboxTarget.Location = new System.Drawing.Point(13, 465);
            this.cboxTarget.Name = "cboxTarget";
            this.cboxTarget.Size = new System.Drawing.Size(121, 20);
            this.cboxTarget.TabIndex = 5;
            this.cboxTarget.Text = "全員";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(280, 15);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(34, 12);
            this.label15.TabIndex = 103;
            this.label15.Text = "Name";
            // 
            // tboxName
            // 
            this.tboxName.Location = new System.Drawing.Point(320, 11);
            this.tboxName.Name = "tboxName";
            this.tboxName.Size = new System.Drawing.Size(100, 19);
            this.tboxName.TabIndex = 2;
            this.tboxName.Text = "テストユーザー";
            // 
            // FormTcpClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.cboxTarget);
            this.Controls.Add(this.tboxMessage);
            this.Controls.Add(this.listViewLog);
            this.Controls.Add(this.listBoxUser);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tboxName);
            this.Controls.Add(this.tboxPort);
            this.Controls.Add(this.tboxIP);
            this.Name = "FormTcpClient";
            this.Text = "TcpClient";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTcpClient_FormClosing);
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
            this.Controls.SetChildIndex(this.tboxIP, 0);
            this.Controls.SetChildIndex(this.tboxPort, 0);
            this.Controls.SetChildIndex(this.tboxName, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.listBoxUser, 0);
            this.Controls.SetChildIndex(this.listViewLog, 0);
            this.Controls.SetChildIndex(this.tboxMessage, 0);
            this.Controls.SetChildIndex(this.cboxTarget, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tboxIP;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tboxPort;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ListBox listBoxUser;
        private System.Windows.Forms.ListView listViewLog;
        private System.Windows.Forms.TextBox tboxMessage;
        private System.Windows.Forms.ComboBox cboxTarget;
        private System.Windows.Forms.ColumnHeader colHeaderLog;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tboxName;
    }
}
