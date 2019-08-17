namespace Sample
{
    partial class FormTcpServer
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
            this.tboxMessage = new System.Windows.Forms.TextBox();
            this.cboxTarget = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonF1
            // 
            this.buttonF1.Text = "Listen";
            // 
            // buttonF2
            // 
            this.buttonF2.Text = "Stop";
            // 
            // buttonF5
            // 
            this.buttonF5.Text = "Send";
            // 
            // tboxIP
            // 
            this.tboxIP.Location = new System.Drawing.Point(34, 12);
            this.tboxIP.Name = "tboxIP";
            this.tboxIP.Size = new System.Drawing.Size(100, 19);
            this.tboxIP.TabIndex = 102;
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
            this.tboxPort.TabIndex = 102;
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
            this.listBoxUser.FormattingEnabled = true;
            this.listBoxUser.ItemHeight = 12;
            this.listBoxUser.Location = new System.Drawing.Point(822, 36);
            this.listBoxUser.Name = "listBoxUser";
            this.listBoxUser.Size = new System.Drawing.Size(120, 448);
            this.listBoxUser.TabIndex = 104;
            // 
            // listViewLog
            // 
            this.listViewLog.Location = new System.Drawing.Point(13, 36);
            this.listViewLog.Name = "listViewLog";
            this.listViewLog.Size = new System.Drawing.Size(803, 423);
            this.listViewLog.TabIndex = 105;
            this.listViewLog.UseCompatibleStateImageBehavior = false;
            this.listViewLog.View = System.Windows.Forms.View.Details;
            // 
            // tboxMessage
            // 
            this.tboxMessage.Location = new System.Drawing.Point(140, 465);
            this.tboxMessage.Name = "tboxMessage";
            this.tboxMessage.Size = new System.Drawing.Size(676, 19);
            this.tboxMessage.TabIndex = 106;
            // 
            // cboxTarget
            // 
            this.cboxTarget.FormattingEnabled = true;
            this.cboxTarget.Location = new System.Drawing.Point(13, 465);
            this.cboxTarget.Name = "cboxTarget";
            this.cboxTarget.Size = new System.Drawing.Size(121, 20);
            this.cboxTarget.TabIndex = 107;
            // 
            // FormTcpServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.cboxTarget);
            this.Controls.Add(this.tboxMessage);
            this.Controls.Add(this.listViewLog);
            this.Controls.Add(this.listBoxUser);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tboxPort);
            this.Controls.Add(this.tboxIP);
            this.Name = "FormTcpServer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTcpServer_FormClosing);
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
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.label14, 0);
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
    }
}
