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
        private readonly List<Form> formList = new List<Form>();

        public FormMenu()
        {
            InitializeComponent();

            SetTabIndex();
        }

        private void SetTabIndex()
        {
            foreach (Control ctrl in this.Controls)
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
            formList.Add(f);
            //モードレスでフォームを表示
            f.Show();
        }

        private void CustomButton3_Click(object sender, EventArgs e)
        {
            ShowDialog(new TestForm());
        }

        private void CustomButton4_Click(object sender, EventArgs e)
        {
            ShowDialog(new FormKakeibo());
        }

        private void CustomButton5_Click(object sender, EventArgs e)
        {
            Form f = new FormTcpClient();
            formList.Add(f);
            //モードレスでフォームを表示
            f.Show();
        }

        private void CustomButton6_Click(object sender, EventArgs e)
        {
            Form formTcpServer = new FormTcpServer();
            formList.Add(formTcpServer);
            formTcpServer.Show();
        }

        private void FormMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form form in formList)
            {
                form.Dispose();
            }
        }

        private void CustomButton7_Click(object sender, EventArgs e)
        {
            ShowDialog(new TestSqlForm());
        }
    }
}
