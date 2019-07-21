using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();

            SetTabIndex();
        }

        private void SetTabIndex()
        {
            foreach(Control ctrl in this.Controls)
            {

            }
        }

        private void CustomButton1_Click(object sender, EventArgs e)
        {
            ShowDialog(new Form1());
        }

        private void ShowDialog(Form f)
        {
            this.Visible = false;

            //モーダルでフォームを表示
            f.ShowDialog(this);

            f.Dispose();
            this.Visible = true;
        }

        private void CustomButton2_Click(object sender, EventArgs e)
        {
            Form f = new FormDiffBmp();
            //モードレスでフォームを表示
            f.Show();
        }
    }
}
