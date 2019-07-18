using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }
        protected void BaseButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
            switch (((Button)sender).Text)
            {
                case "検索":
                    SearchButton_Click(sender, e);
                    break;
                case "クリア":
                    ClearButton_Click(sender, e);
                    break;
                case "追加":
                    InsertButton_Click(sender, e);
                    break;
                case "更新":
                    UpdateButton_Click(sender, e);
                    break;
                case "削除":
                    DeleteButton_Click(sender, e);
                    break;
                case "終了":
                    ExitButton_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        public virtual void SearchButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        public virtual void ClearButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        public virtual void InsertButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        public virtual void UpdateButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        public virtual void DeleteButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        public virtual void ExitButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
            Close();
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {

        }

        private void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
