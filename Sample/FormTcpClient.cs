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

                listViewLog.Items.Add($"接続しました。{tcpClientUtil.GetClientIpAndPort()}->{tcpClientUtil.GetServerIpAndPort()}");

                // 名前を送信
                string name = tboxName.Text;
                TcpMessageManager mgr = new TcpMessageManager(TcpMessageManager.HeaderConnect, name, TcpMessageManager.TargetAll, name);
                tcpClientUtil.Send(mgr.GetSendMessage());
            }
            catch (SocketException ex)
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
                    string texts = await tcpClientUtil.ReadAsync(cToken);

                    if (texts != null)
                    {
                        foreach (string text in texts.Split('\n'))
                        {
                            logger.WriteLine(text);

                            TcpMessageManager mgr = new TcpMessageManager(text);
                            switch (mgr.Header)
                            {
                                case TcpMessageManager.HeaderConnect:
                                    // 申請した名前が登録できたか判定する
                                    // Connect,IP:Port,送信した名前,登録した名前
                                    if (tcpClientUtil.GetClientIpAndPort().Equals(mgr.SendFromTarget))
                                    {
                                        // 自分が送信したNAMEの返信
                                        if (!mgr.SendToTarget.Equals(mgr.Value))
                                        {
                                            // 別名に変更された場合、名前を変更する
                                            Invoke((Action)(() =>
                                            {
                                                tboxName.Text = mgr.Value;
                                                listViewLog.Items.Add($"既に名前が使われていたため、名前を{mgr.Value}に変更しました。");
                                            }));
                                        }
                                    }
                                    else
                                    {
                                        // 他の人が送信したNAMEの受信
                                        Invoke((Action)(() =>
                                        {
                                            listViewLog.Items.Add($"{mgr.Value}が接続しました。");
                                        }));
                                    }

                                    // リストに追加する
                                    Invoke((Action)(() =>
                                    {
                                        listBoxUser.Items.Add(mgr.Value);
                                        cboxTarget.Items.Add(mgr.Value);
                                    }));
                                    break;
                                case TcpMessageManager.HeaderName:
                                    // リストに追加する
                                    Invoke((Action)(() =>
                                    {
                                        listBoxUser.Items.Add(mgr.Value);
                                        cboxTarget.Items.Add(mgr.Value);
                                    }));
                                    break;
                                case TcpMessageManager.HeaderTargetMsg:
                                    Invoke((Action)(() =>
                                    {
                                        listViewLog.Items.Add(mgr.GetRecvTargetMessage());
                                    }));
                                    break;
                                case TcpMessageManager.HeaderAllMsg:
                                default:
                                    Invoke((Action)(() =>
                                    {
                                        listViewLog.Items.Add(mgr.GetRecvMessage());
                                    }));
                                    break;
                            }
                        }
                    }
                    else
                    {
                        logger.WriteLine("切断されました。");

                        // ToDo:破棄処理
                        // ToDo:コントロール初期化

                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    Invoke((Action)(() =>
                    {
                        listViewLog.Items.Add(ex.Message);
                    }));
                }
                catch (Exception ex2)
                {
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex2.Message);
                }
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
            // メッセージ送信
            string sendMsg = tboxMessage.Text;
            string toName = cboxTarget.Text;
            string fromName = tboxName.Text;
            TcpMessageManager mgr;
            if (toName.Equals("全員"))
            {
                mgr = new TcpMessageManager(TcpMessageManager.HeaderAllMsg, fromName, TcpMessageManager.TargetAll, sendMsg);
            }
            else
            {
                mgr = new TcpMessageManager(TcpMessageManager.HeaderTargetMsg, fromName, toName, sendMsg);
            }

            tcpClientUtil.Send(mgr.GetSendMessage());

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
