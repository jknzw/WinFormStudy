using SampleLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample
{
    public partial class FormTcpClient : Sample.Base.BaseForm
    {
        #region フィールド
        private TcpClientUtility tcpClientUtil = null;

        private readonly Logger logger = null;

        private Task readLoopTask = null;
        private CancellationTokenSource readCancelTokenSource = null;
        #endregion

        #region コンストラクタ
        public FormTcpClient()
        {
            logger = Logger.GetInstance(GetType().Name);
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            InitializeComponent();

            // 使わないボタンを非表示にする
            //buttonF1.Visible = false;
            //buttonF2.Visible = false;
            buttonF3.Visible = false;
            buttonF4.Visible = false;
            //buttonF5.Visible = false;
            buttonF6.Visible = false;
            buttonF7.Visible = false;
            buttonF8.Visible = false;
            buttonF9.Visible = false;
            buttonF10.Visible = false;
            //buttonF11.Visible = false;

            // ボタンの有効無効を設定
            SetButtonEnabled(ActionMode.Init);
        }
        #endregion

        /// <summary>
        /// 動作モード
        /// </summary>
        private enum ActionMode
        {
            Init = 0,
            Connect,
        }

        /// <summary>
        /// ボタン状態の設定
        /// </summary>
        /// <param name="mode"></param>
        private void SetButtonEnabled(ActionMode mode)
        {
            logger.WriteLine($"{MethodBase.GetCurrentMethod().Name} ActionMode:{mode}");

            // まず全てのボタンを無効にする
            SetAllBaseButtonEnabled(false);

            // 必要なボタンのみ有効にする
            buttonF11.Enabled = true;
            buttonF12.Enabled = true;
            switch (mode)
            {
                case ActionMode.Connect:
                    buttonF1.Enabled = false;
                    buttonF2.Enabled = true;
                    buttonF5.Enabled = true;
                    break;
                case ActionMode.Init:
                default:
                    buttonF1.Enabled = true;
                    buttonF2.Enabled = false;
                    buttonF5.Enabled = false;
                    break;
            }
        }

        /// <summary>
        /// Connect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF1_Click(object sender, EventArgs e)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            base.ButtonF1_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            if (tcpClientUtil != null)
            {
                // 既に接続している場合、再接続前に破棄する
                tcpClientUtil.Dispose();
            }

            // 接続
            string ip = tboxIP.Text;
            int.TryParse(tboxPort.Text, out int port);

            try
            {
                tcpClientUtil = new TcpClientUtility(ip, port);
            }
            catch (Exception ex)
            {
                listViewLog.Items.Add(ex.Message);
                return;
            }

            // 読み込みループ開始
            // Task停止用のトークン発行
            readCancelTokenSource = new CancellationTokenSource();
            CancellationToken cToken = readCancelTokenSource.Token;
            readLoopTask = Task.Run(() => ReadLoop(cToken), cToken);

            // ボタンの有効無効を設定
            SetButtonEnabled(ActionMode.Connect);
            // ▲▲▲ 業務処理 ▲▲▲
        }

        private async void ReadLoop(CancellationToken cToken)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            try
            {
                while (!cToken.IsCancellationRequested)
                {
                    string s = await tcpClientUtil.ReadAsync();

                    if (!string.IsNullOrEmpty(s))
                    {
                        logger.WriteLine(s);

                        if (s.StartsWith("[MSG]"))
                        {
                            string msg = s.Remove(0, "[MSG]".Length);
                            Invoke((Action)(() =>
                            {
                                listViewLog.Items.Add(msg);
                            }));

                        }
                        else if (s.StartsWith("[CONNECT]"))
                        {
                            string msg = s.Remove(0, "[CONNECT]".Length);
                            Invoke((Action)(() =>
                            {
                                listBoxUser.Items.Add(msg);
                            }));
                        }
                        else
                        {
                            Invoke((Action)(() =>
                            {
                                listViewLog.Items.Add(s);
                            }));
                        }
                    }
                    else
                    {
                        logger.WriteLine("切断されました。");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Invoke((Action)(() =>
                {
                    listBoxUser.Items.Add(ex.Message);
                }));
            }
        }

        /// <summary>
        /// DisConnect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF2_Click(object sender, EventArgs e)
        {
            base.ButtonF2_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            // ▲▲▲ 業務処理 ▲▲▲
        }

        #region F3～F4 未使用
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF3_Click(object sender, EventArgs e)
        {
            base.ButtonF3_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            // ▲▲▲ 業務処理 ▲▲▲
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF4_Click(object sender, EventArgs e)
        {
            base.ButtonF4_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            // ▲▲▲ 業務処理 ▲▲▲
        }
        #endregion

        /// <summary>
        /// Send
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF5_Click(object sender, EventArgs e)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);
            base.ButtonF5_Click(sender, e);

            // ▼▼▼ 業務処理 ▼▼▼

            string sendMsg = tboxMessage.Text;
            tcpClientUtil.Send(sendMsg);

            // ▲▲▲ 業務処理 ▲▲▲
        }

        #region F6～F12 未使用
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF6_Click(object sender, EventArgs e)
        {
            base.ButtonF6_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            // ▲▲▲ 業務処理 ▲▲▲
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF7_Click(object sender, EventArgs e)
        {
            base.ButtonF7_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            // ▲▲▲ 業務処理 ▲▲▲
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF8_Click(object sender, EventArgs e)
        {
            base.ButtonF8_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            // ▲▲▲ 業務処理 ▲▲▲
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF9_Click(object sender, EventArgs e)
        {
            base.ButtonF9_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            // ▲▲▲ 業務処理 ▲▲▲
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF10_Click(object sender, EventArgs e)
        {
            base.ButtonF10_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            // ▲▲▲ 業務処理 ▲▲▲
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF11_Click(object sender, EventArgs e)
        {
            base.ButtonF11_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            // ▲▲▲ 業務処理 ▲▲▲
        }
        #endregion

        #region Close
        private void FormTcpClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            // 受信キャンセル
            readCancelTokenSource.Cancel();
            readLoopTask.Wait(10 * 1000);
            readLoopTask.Dispose();

            // TCP破棄
            tcpClientUtil.Dispose();

            // Logger破棄
            logger.Dispose();
        }
        #endregion
    }
}
