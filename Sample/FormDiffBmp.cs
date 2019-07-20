using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sample
{
    public partial class FormDiffBmp : Sample.BaseForm
    {
        public FormDiffBmp()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                textBox1.Text = openFileDialog1.FileName;
            }
            else
            {
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog().Equals(DialogResult.OK))
            {
                textBox2.Text = openFileDialog2.FileName;
            }
            else
            {
            }
        }

        protected override void ButtonF1_Click(object sender, EventArgs e)
        {
            base.ButtonF1_Click(sender, e);

            Bitmap bmp1 = new Bitmap(textBox1.Text);
            Bitmap bmp2 = new Bitmap(textBox2.Text);

            int.TryParse(textBoxX1.Text, out int marginX1);
            int.TryParse(textBoxY1.Text, out int marginY1);
            int.TryParse(textBoxX2.Text, out int marginX2);
            int.TryParse(textBoxY2.Text, out int marginY2);

            //int.TryParse(textBox3.Text, out int hosei);

            int width = Math.Max(bmp1.Width + marginX1, bmp2.Width + marginX2);
            int height = Math.Max(bmp1.Height + marginY1, bmp2.Height + marginY2);

            Bitmap diffBmp = new Bitmap(width, height);         // 返却する差分の画像。
            Color diffColor = Color.Red;                        // 画像の差分に付ける色。

            toolStripProgressBar1.Maximum = width;

            // 全ピクセルを総当りで比較し、違う部分があればfalseを返す。
            for (int i = 0; i < width - marginX1; i++)
            {
                for (int j = 0; j < height - marginY1; j++)
                {
                    if (i < marginX1 || j < marginY1)
                    {
                        // マージン
                        diffBmp.SetPixel(i, j, diffColor);
                    }
                    else if (i + marginX1 >= bmp1.Width || i + marginX2 >= bmp2.Width
                        || j + marginY1 >= bmp1.Height || j + marginY2 >= bmp2.Height)
                    {
                        // 縦横オーバー
                        diffBmp.SetPixel(i + marginX1, j + marginY1, diffColor);
                    }
                    else
                    {
                        //Color color1 = bmp1.GetPixel(i + marginX1, j + marginY1);
                        //if (color1 == bmp2.GetPixel(i + marginX2, j + marginY2))
                        if (CompareBmp(bmp1, bmp2, i + marginX1, j + marginY1, i + marginX2, j + marginY2))
                        {
                            diffBmp.SetPixel(i + marginX1, j + marginY1, bmp1.GetPixel(i + marginX1, j + marginY1));
                        }
                        else
                        {
                            diffBmp.SetPixel(i + marginX1, j + marginY1, diffColor);
                        }
                    }
                }

                toolStripProgressBar1.Value = i;
            }
            //diffBmp.Save(path, ImageFormat.Png);
            pictureBox1.Image = diffBmp;

            toolStripProgressBar1.Value = 0;
        }

        private bool CompareBmp(Bitmap bmp1, Bitmap bmp2, int x1, int y1, int x2, int y2, int correction = 0)
        {
            Color color1 = bmp1.GetPixel(x1, y1);
            if (color1 == bmp2.GetPixel(x2, y2))
            {
                return true;
            }
            else
            {
                for (int i = 0; i < correction; i++)
                {
                    //　□□□
                    //　□　□
                    //　□□□

                }
                return false;
            }
        }
    }
}