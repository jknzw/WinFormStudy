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
    public partial class FormTcpServer : Sample.Base.BaseForm
    {
        private Task acceptLoopTask = null;

        /// <summary>
        /// クライアント管理辞書
        /// [key]名前 or IP:Port
        /// [Value] TcpServerUtility.ClientManager
        /// </summary>
        private readonly Dictionary<string, TcpServerManager.ClientInfo> dicTcpClient = new Dictionary<string, TcpServerManager.ClientInfo>();
        private readonly int taskTimeOut = 1 * 1000;

        public TcpServerManager TcpServerUtil { get; set; } = null;

        public Logger Logger { get; } = null;
        public CancellationTokenSource CancelTokenSource { get; set; } = null;

        public FormTcpServer()
        {
            Logger = Logger.GetInstance(GetType().Name);
            Logger.StartMethod(nameof(FormTcpServer));

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

            Logger.EndMethod(nameof(FormTcpServer));
        }

        /// <summary>
        /// 動作モード
        /// </summary>
        private enum ActionMode
        {
            Init = 0,
            Listen,
        }

        /// <summary>
        /// ボタン状態の設定
        /// </summary>
        /// <param name="mode"></param>
        private void SetButtonEnabled(ActionMode mode)
        {
            Logger.StartMethod(MethodBase.GetCurrentMethod().Name, $"ActionMode:{mode}");

            // まず全てのボタンを無効にする
            SetAllBaseButtonEnabled(false);

            // 必要なボタンのみ有効にする
            buttonF11.Enabled = true;
            buttonF12.Enabled = true;
            switch (mode)
            {
                case ActionMode.Listen:
                    buttonF1.Enabled = false;
                    buttonF2.Enabled = true;
                    buttonF5.Enabled = true;
                    buttonF6.Enabled = true;
                    break;
                case ActionMode.Init:
                default:
                    buttonF1.Enabled = true;
                    buttonF2.Enabled = false;
                    buttonF5.Enabled = false;
                    buttonF6.Enabled = false;
                    break;
            }

            Logger.EndMethod(MethodBase.GetCurrentMethod().Name);
        }

        /// <summary>
        /// 開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF1_Click(object sender, EventArgs e)
        {
            Logger.StartMethod(MethodBase.GetCurrentMethod().Name);
            base.ButtonF1_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            if (TcpServerUtil != null)
            {
                // 既に接続している場合、再接続前に破棄する
                TcpServerUtil.Dispose();
            }

            // Listen
            string ip = tboxIP.Text;
            int.TryParse(tboxPort.Text, out int port);
            TcpServerUtil = new TcpServerManager(ip, port);

            listViewLog.Items.Add($"Listenを開始しました。IP[{ip}] Port[{port}]");

            // Task停止用のトークン発行
            CancelTokenSource = new CancellationTokenSource();
            CancellationToken cToken = CancelTokenSource.Token;

            // Accept Loop Start
            acceptLoopTask = AcceptLoop(cToken);

            // ボタンの有効無効を設定
            SetButtonEnabled(ActionMode.Listen);
            // ▲▲▲ 業務処理 ▲▲▲
            Logger.EndMethod(MethodBase.GetCurrentMethod().Name);
        }

        private async Task AcceptLoop(CancellationToken cToken)
        {
            Logger.StartMethod(nameof(AcceptLoop));
            try
            {
                while (!cToken.IsCancellationRequested)
                {
                    // Acceptを非同期待機
                    TcpServerManager.ClientInfo mgr = await TcpServerUtil.AcceptAsync().ConfigureAwait(false);
                    // Accept完了後、後続処理が動く

                    string acceptClientName = mgr.GetClientIpAndPort();

                    Invoke((Action)(() =>
                    {
                        listViewLog.Items.Add($"[{MethodBase.GetCurrentMethod().Name}]{acceptClientName}が接続しました。");
                    }));

                    // Read Loop Start
                    mgr.ReadTask = ReadLoop(mgr, cToken);

                    // 接続してきたクライアントに対し、既に接続している他のクライアントの情報を送信
                    foreach (string clientName in dicTcpClient.Keys)
                    {
                        TcpMessageUtility sendMsgMgr = new TcpMessageUtility(TcpMessageUtility.HeaderName, clientName, acceptClientName, clientName);
                        TcpServerUtil.SendTarget(mgr.GetClientIpAndPort(), sendMsgMgr.GetSendMessage());
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                Logger.WriteException(nameof(AcceptLoop), ex);
            }
            Logger.EndMethod(nameof(AcceptLoop));
        }

        private async Task ReadLoop(TcpServerManager.ClientInfo tcpClientMgr, CancellationToken cToken)
        {
            Logger.StartMethod(nameof(ReadLoop));

            try
            {
                TcpClient client = tcpClientMgr.Client;
                while (!cToken.IsCancellationRequested)
                {
                    // メッセージ受信
                    string texts = await TcpServerUtil.ReadAsync(client, cToken).ConfigureAwait(false);

                    if (texts != null)
                    {
                        // \nで分割
                        foreach (string text in texts.Split('\n'))
                        {
                            Logger.WriteLine(text);

                            Invoke((Action)(() =>
                            {
                                listViewLog.Items.Add($"[Read]{text}");
                            }));

                            TcpMessageUtility recvMsgMgr = new TcpMessageUtility(text);
                            TcpMessageUtility sendMsgMgr;
                            switch (recvMsgMgr.Header)
                            {
                                case TcpMessageUtility.HeaderConnect:
                                    // 名前設定
                                    // 辞書のキーの変更
                                    dicTcpClient.Remove(tcpClientMgr.GetClientIpAndPort());

                                    if (!dicTcpClient.ContainsKey(recvMsgMgr.Value))
                                    {
                                        // 受信した名前が未登録の場合
                                        tcpClientMgr.Name = recvMsgMgr.Value;
                                        dicTcpClient.Add(tcpClientMgr.Name, tcpClientMgr);
                                    }
                                    else
                                    {
                                        // 受信した名前が既に登録されている場合
                                        int count = 0;
                                        string newName;
                                        do
                                        {
                                            // 番号を付加する
                                            count++;
                                            newName = recvMsgMgr.Value + count.ToString();
                                        } while (dicTcpClient.ContainsKey(newName));
                                        tcpClientMgr.Name = newName;
                                        dicTcpClient.Add(newName, tcpClientMgr);
                                    }

                                    // List＆Comboに追加
                                    Invoke((Action)(() =>
                                    {
                                        listBoxUser.Items.Add(tcpClientMgr.Name);
                                        cboxTarget.Items.Add(tcpClientMgr.Name);
                                    }));

                                    // 全体に接続情報を送信
                                    // Connect,IP:Port,受信した名前,登録した名前
                                    sendMsgMgr = new TcpMessageUtility(TcpMessageUtility.HeaderConnect, tcpClientMgr.GetClientIpAndPort(), recvMsgMgr.Value, tcpClientMgr.Name);
                                    TcpServerUtil.SendAll(sendMsgMgr.GetSendMessage());
                                    break;
                                case TcpMessageUtility.HeaderTargetMsg:
                                    // 送信元を設定
                                    sendMsgMgr = new TcpMessageUtility(TcpMessageUtility.HeaderTargetMsg, tcpClientMgr.Name, recvMsgMgr.SendToTarget, recvMsgMgr.Value);

                                    Invoke((Action)(() =>
                                    {
                                        listViewLog.Items.Add(sendMsgMgr.GetRecvTargetMessage());
                                    }));

                                    // 受信したメッセージを対象に送信する
                                    // 名前→IP:Portへの変換
                                    string toTarget = dicTcpClient[recvMsgMgr.SendToTarget].GetClientIpAndPort();
                                    TcpServerUtil.SendTarget(toTarget, sendMsgMgr.GetSendMessage());

                                    // 受信したメッセージを送信元に送信する
                                    // 名前→IP:Portへの変換
                                    string fromTarget = tcpClientMgr.GetClientIpAndPort();
                                    TcpServerUtil.SendTarget(fromTarget, sendMsgMgr.GetSendMessage());
                                    break;
                                case TcpMessageUtility.HeaderAllMsg:
                                default:
                                    // 送信元を設定
                                    sendMsgMgr = new TcpMessageUtility(TcpMessageUtility.HeaderAllMsg, tcpClientMgr.Name, recvMsgMgr.SendToTarget, recvMsgMgr.Value);

                                    Invoke((Action)(() =>
                                    {
                                        listViewLog.Items.Add(sendMsgMgr.GetRecvTargetMessage());
                                    }));

                                    // 受信したメッセージを全体に送信する
                                    TcpServerUtil.SendAll(sendMsgMgr.GetSendMessage());
                                    break;
                            }
                        }
                    }
                    else
                    {
                        // 切断
                        Logger.WriteLine(nameof(ReadLoop), "切断されました。");
                        string deleteTargetIpAndPort = tcpClientMgr.GetClientIpAndPort();
                        _ = dicTcpClient.Remove(deleteTargetIpAndPort);
                        TcpServerUtil.Delete(deleteTargetIpAndPort);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                Logger.WriteException(nameof(ReadLoop), ex);
            }

            Logger.EndMethod(nameof(ReadLoop));
        }

        /// <summary>
        /// 
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
            base.ButtonF5_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            string sendMsg = tboxMessage.Text;
            string fromName = tboxName.Text;
            string toName = cboxTarget.Text;

            TcpMessageUtility sendMsgMgr;
            if (toName.Equals("全員"))
            {
                sendMsgMgr = new TcpMessageUtility(TcpMessageUtility.HeaderAllMsg, fromName, TcpMessageUtility.TargetAll, sendMsg);
            }
            else
            {
                sendMsgMgr = new TcpMessageUtility(TcpMessageUtility.HeaderTargetMsg, fromName, toName, sendMsg);
            }

            TcpServerUtil.SendAll(sendMsgMgr.GetSendMessage());
            // ▲▲▲ 業務処理 ▲▲▲
        }

        #region F6～F10 未使用
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
        #endregion

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

        #region Close
        private async void FormTcpServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            CancelTokenSource?.Cancel();

            // Lisner Stop And TcpClient Close
            TcpServerUtil?.Dispose();

            // Accept And Read Task Wait
            List<Task> waitTasks = new List<Task>();
            if (acceptLoopTask != null)
            {
                waitTasks.Add(acceptLoopTask);
            }
            foreach (TcpServerManager.ClientInfo mgr in dicTcpClient.Values)
            {
                waitTasks.Add(mgr.ReadTask);
            }
            await Task.WhenAll(waitTasks);

            // Logger Dispose
            Logger?.Dispose();
        }
        #endregion
    }
}
