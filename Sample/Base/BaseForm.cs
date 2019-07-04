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
            switch (((Button)sender).Name)
            {
                case nameof(buttonSearch):
                    SearchButton_Click(sender, e);
                    break;
                case nameof(buttonClear):
                    ClearButton_Click(sender, e);
                    break;
                case nameof(buttonUpdate):
                    UpdateButton_Click(sender, e);
                    break;
                case nameof(buttonDelete):
                    DeleteButton_Click(sender, e);
                    break;
                case nameof(buttonEnd):
                default:
                    ExitButton_Click(sender, e);
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
    }
}
