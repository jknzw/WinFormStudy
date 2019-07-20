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

        #region Buttonイベント
        private void BaseButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
            switch (((Button)sender).Name)
            {
                case nameof(buttonF1):
                    ButtonF1_Click(sender, e);
                    break;
                case nameof(buttonF2):
                    ButtonF2_Click(sender, e);
                    break;
                case nameof(buttonF3):
                    ButtonF3_Click(sender, e);
                    break;
                case nameof(buttonF4):
                    ButtonF4_Click(sender, e);
                    break;
                case nameof(buttonF5):
                    ButtonF5_Click(sender, e);
                    break;
                case nameof(buttonF6):
                    ButtonF6_Click(sender, e);
                    break;
                case nameof(buttonF7):
                    ButtonF7_Click(sender, e);
                    break;
                case nameof(buttonF8):
                    ButtonF8_Click(sender, e);
                    break;
                case nameof(buttonF9):
                    ButtonF9_Click(sender, e);
                    break;
                case nameof(buttonF10):
                    ButtonF10_Click(sender, e);
                    break;
                case nameof(buttonF11):
                    ButtonF11_Click(sender, e);
                    break;
                case nameof(buttonF12):
                    ButtonF12_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        protected virtual void ButtonF1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF2_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF3_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF4_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF5_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF6_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF7_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF8_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF9_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF10_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF11_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF12_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);

            // 終了
            Close();
        }
        #endregion

        #region Formイベント
        private void BaseForm_Load(object sender, EventArgs e)
        {

        }

        private void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        #endregion

        protected void SetAllBaseButtonEnabled(bool enabled)
        {
            buttonF1.Enabled = enabled;
            buttonF2.Enabled = enabled;
            buttonF3.Enabled = enabled;
            buttonF4.Enabled = enabled;
            buttonF5.Enabled = enabled;
            buttonF6.Enabled = enabled;
            buttonF7.Enabled = enabled;
            buttonF8.Enabled = enabled;
            buttonF9.Enabled = enabled;
            buttonF10.Enabled = enabled;
            buttonF11.Enabled = enabled;
            buttonF12.Enabled = enabled;
        }
    }
}
