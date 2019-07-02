using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sample
{
    public partial class Form1 : Sample.BaseForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        public override void SelectButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result.Equals(DialogResult.OK))
            {
                string filePath = openFileDialog1.FileName;
                Form1Service sv = new Form1Service();
                DataTable dt = sv.GetDataTable(filePath, "UTF-8");
                dataGridView1.DataSource = dt;

                textBox1.Text = filePath;
            }
        }
    }
}
