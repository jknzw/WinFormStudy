namespace Sample
{
    partial class FormSample
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
                logger?.Dispose();
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
            this.SuspendLayout();
            // 
            // buttonF1
            // 
            this.buttonF1.Location = new System.Drawing.Point(7, 513);
            // 
            // buttonF2
            // 
            this.buttonF2.Location = new System.Drawing.Point(88, 513);
            // 
            // buttonF3
            // 
            this.buttonF3.Location = new System.Drawing.Point(169, 513);
            // 
            // buttonF12
            // 
            this.buttonF12.Location = new System.Drawing.Point(903, 513);
            // 
            // buttonF11
            // 
            this.buttonF11.Location = new System.Drawing.Point(822, 513);
            // 
            // buttonF4
            // 
            this.buttonF4.Location = new System.Drawing.Point(250, 513);
            // 
            // buttonF5
            // 
            this.buttonF5.Location = new System.Drawing.Point(334, 513);
            // 
            // buttonF6
            // 
            this.buttonF6.Location = new System.Drawing.Point(415, 513);
            // 
            // buttonF7
            // 
            this.buttonF7.Location = new System.Drawing.Point(496, 513);
            // 
            // buttonF8
            // 
            this.buttonF8.Location = new System.Drawing.Point(577, 513);
            // 
            // buttonF9
            // 
            this.buttonF9.Location = new System.Drawing.Point(660, 513);
            // 
            // buttonF10
            // 
            this.buttonF10.Location = new System.Drawing.Point(741, 513);
            // 
            // FormSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Name = "FormSample";
            this.ResumeLayout(false);

        }

        #endregion
    }
}
