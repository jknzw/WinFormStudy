using SampleLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample
{
    public partial class TestForm : Sample.Base.BaseForm
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private int count = 0;

        private Logger log = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF1_Click(object sender, EventArgs e)
        {
            base.ButtonF1_Click(sender, e);

            // ▼▼▼ 業務処理 ▼▼▼
            if (log == null)
            {
                // 初回
                log = Logger.GetInstance(textBoxLogFilePath.Text);
            }
            else if (!log.LogFilePath.Equals(textBoxLogFilePath.Text))
            {
                // ログファイル変更
                log.Dispose();
                log = Logger.GetInstance(textBoxLogFilePath.Text);
            }
            else
            {
                // 変更無し
            }

            int.TryParse(textBoxBoundedCapacity.Text, out int cap);
            int.TryParse(textBoxTaskTimeout.Text, out int timeout);
            int.TryParse(textBoxWriteDelay.Text, out int delay);
            log.BoundedCapacity = cap;
            log.TaskTimeout = timeout;
            log.WriteDelay = delay;

            for (int i = 0; i < 1000; i++)
            {
                log.WriteLineWait(count++.ToString());
            }
            // ▲▲▲ 業務処理 ▲▲▲
        }

        private void TestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // インスタンス破棄
            log?.Dispose();
            log = null;
        }
    }
}
