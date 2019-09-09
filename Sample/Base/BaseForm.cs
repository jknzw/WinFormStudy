using SampleLibrary;
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

namespace Sample.Base
{
    public partial class BaseForm : Form
    {
        private readonly Logger logger = Logger.GetInstance(nameof(BaseForm));

        public BaseForm()
        {
            InitializeComponent();
        }

        #region Buttonイベント
        private void BaseButton_Click(object sender, EventArgs e)
        {
            try
            {
                logger.StartMethod(MethodBase.GetCurrentMethod().Name, $"Click:{((Button)sender).Name}");
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
            catch (Exception ex)
            {
                logger.WriteException(MethodBase.GetCurrentMethod().Name, ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                logger.EndMethod(MethodBase.GetCurrentMethod().Name, $"Click:{((Button)sender).Name}");
            }
        }

        protected virtual void ButtonF1_Click(object sender, EventArgs e)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF2_Click(object sender, EventArgs e)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF3_Click(object sender, EventArgs e)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF4_Click(object sender, EventArgs e)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF5_Click(object sender, EventArgs e)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF6_Click(object sender, EventArgs e)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF7_Click(object sender, EventArgs e)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF8_Click(object sender, EventArgs e)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF9_Click(object sender, EventArgs e)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF10_Click(object sender, EventArgs e)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF11_Click(object sender, EventArgs e)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        protected virtual void ButtonF12_Click(object sender, EventArgs e)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

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

        private void BaseForm_KeyDown(object sender, KeyEventArgs e)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);
            switch (e.KeyCode)
            {
                case Keys.F1:
                    buttonF1.PerformClick();
                    break;
                case Keys.F2:
                    buttonF2.PerformClick();
                    break;
                case Keys.F3:
                    buttonF3.PerformClick();
                    break;
                case Keys.F4:
                    buttonF4.PerformClick();
                    break;
                case Keys.F5:
                    buttonF5.PerformClick();
                    break;
                case Keys.F6:
                    buttonF6.PerformClick();
                    break;
                case Keys.F7:
                    buttonF7.PerformClick();
                    break;
                case Keys.F8:
                    buttonF8.PerformClick();
                    break;
                case Keys.F9:
                    buttonF9.PerformClick();
                    break;
                case Keys.F10:
                    buttonF10.PerformClick();
                    break;
                case Keys.F11:
                    buttonF11.PerformClick();
                    break;
                case Keys.F12:
                    buttonF12.PerformClick();
                    break;
                default:
                    break;
            }
        }
    }
}
