namespace Sample
{
    partial class FormMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.customButton2 = new WindowsFormsControlLibrary.CustomButton();
            this.customButton7 = new WindowsFormsControlLibrary.CustomButton();
            this.customButton3 = new WindowsFormsControlLibrary.CustomButton();
            this.customButton4 = new WindowsFormsControlLibrary.CustomButton();
            this.customButton6 = new WindowsFormsControlLibrary.CustomButton();
            this.customButton5 = new WindowsFormsControlLibrary.CustomButton();
            this.customButton1 = new WindowsFormsControlLibrary.CustomButton();
            this.customButton8 = new WindowsFormsControlLibrary.CustomButton();
            this.SuspendLayout();
            // 
            // customButton2
            // 
            this.customButton2.Location = new System.Drawing.Point(12, 70);
            this.customButton2.Name = "customButton2";
            this.customButton2.Size = new System.Drawing.Size(99, 23);
            this.customButton2.TabIndex = 2;
            this.customButton2.Text = "FormDiffBmp";
            this.customButton2.UseVisualStyleBackColor = true;
            this.customButton2.Click += new System.EventHandler(this.CustomButton2_Click);
            // 
            // customButton7
            // 
            this.customButton7.Location = new System.Drawing.Point(117, 99);
            this.customButton7.Name = "customButton7";
            this.customButton7.Size = new System.Drawing.Size(99, 23);
            this.customButton7.TabIndex = 7;
            this.customButton7.Text = "TestSqlForm";
            this.customButton7.UseVisualStyleBackColor = true;
            this.customButton7.Click += new System.EventHandler(this.CustomButton7_Click);
            // 
            // customButton3
            // 
            this.customButton3.Location = new System.Drawing.Point(12, 99);
            this.customButton3.Name = "customButton3";
            this.customButton3.Size = new System.Drawing.Size(99, 23);
            this.customButton3.TabIndex = 3;
            this.customButton3.Text = "TestForm";
            this.customButton3.UseVisualStyleBackColor = true;
            this.customButton3.Click += new System.EventHandler(this.CustomButton3_Click);
            // 
            // customButton4
            // 
            this.customButton4.Location = new System.Drawing.Point(12, 41);
            this.customButton4.Name = "customButton4";
            this.customButton4.Size = new System.Drawing.Size(99, 23);
            this.customButton4.TabIndex = 1;
            this.customButton4.Text = "FormKakeibo";
            this.customButton4.UseVisualStyleBackColor = true;
            this.customButton4.Click += new System.EventHandler(this.CustomButton4_Click);
            // 
            // customButton6
            // 
            this.customButton6.Location = new System.Drawing.Point(117, 41);
            this.customButton6.Name = "customButton6";
            this.customButton6.Size = new System.Drawing.Size(99, 23);
            this.customButton6.TabIndex = 5;
            this.customButton6.Text = "FormTcpServer";
            this.customButton6.UseVisualStyleBackColor = true;
            this.customButton6.Click += new System.EventHandler(this.CustomButton6_Click);
            // 
            // customButton5
            // 
            this.customButton5.Location = new System.Drawing.Point(117, 12);
            this.customButton5.Name = "customButton5";
            this.customButton5.Size = new System.Drawing.Size(99, 23);
            this.customButton5.TabIndex = 4;
            this.customButton5.Text = "FormTcpClient";
            this.customButton5.UseVisualStyleBackColor = true;
            this.customButton5.Click += new System.EventHandler(this.CustomButton5_Click);
            // 
            // customButton1
            // 
            this.customButton1.Location = new System.Drawing.Point(12, 12);
            this.customButton1.Name = "customButton1";
            this.customButton1.Size = new System.Drawing.Size(99, 23);
            this.customButton1.TabIndex = 0;
            this.customButton1.Text = "Form1";
            this.customButton1.UseVisualStyleBackColor = true;
            this.customButton1.Click += new System.EventHandler(this.CustomButton1_Click);
            // 
            // customButton8
            // 
            this.customButton8.Location = new System.Drawing.Point(12, 128);
            this.customButton8.Name = "customButton8";
            this.customButton8.Size = new System.Drawing.Size(99, 23);
            this.customButton8.TabIndex = 8;
            this.customButton8.Text = "FormKakeiboS";
            this.customButton8.UseVisualStyleBackColor = true;
            this.customButton8.Click += new System.EventHandler(this.CustomButton8_Click);
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 161);
            this.Controls.Add(this.customButton2);
            this.Controls.Add(this.customButton7);
            this.Controls.Add(this.customButton3);
            this.Controls.Add(this.customButton8);
            this.Controls.Add(this.customButton4);
            this.Controls.Add(this.customButton6);
            this.Controls.Add(this.customButton5);
            this.Controls.Add(this.customButton1);
            this.Name = "FormMenu";
            this.Text = "メニュー";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMenu_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private WindowsFormsControlLibrary.CustomButton customButton1;
        private WindowsFormsControlLibrary.CustomButton customButton2;
        private WindowsFormsControlLibrary.CustomButton customButton3;
        private WindowsFormsControlLibrary.CustomButton customButton4;
        private WindowsFormsControlLibrary.CustomButton customButton5;
        private WindowsFormsControlLibrary.CustomButton customButton6;
        private WindowsFormsControlLibrary.CustomButton customButton7;
        private WindowsFormsControlLibrary.CustomButton customButton8;
    }
}