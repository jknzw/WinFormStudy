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
            this.customButton1 = new WindowsFormsControlLibrary.CustomButton();
            this.customButton2 = new WindowsFormsControlLibrary.CustomButton();
            this.SuspendLayout();
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
            // customButton2
            // 
            this.customButton2.Location = new System.Drawing.Point(12, 41);
            this.customButton2.Name = "customButton2";
            this.customButton2.Size = new System.Drawing.Size(99, 23);
            this.customButton2.TabIndex = 0;
            this.customButton2.Text = "FormDiffBmp";
            this.customButton2.UseVisualStyleBackColor = true;
            this.customButton2.Click += new System.EventHandler(this.CustomButton2_Click);
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.customButton2);
            this.Controls.Add(this.customButton1);
            this.Name = "FormMenu";
            this.Text = "FormMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private WindowsFormsControlLibrary.CustomButton customButton1;
        private WindowsFormsControlLibrary.CustomButton customButton2;
    }
}