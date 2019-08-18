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
        private TcpServerUtility tcpServerUtil = null;

        private readonly Logger logger = null;

        private Task acceptLoopTask = null;

        /// <summary>
        /// クライアント管理辞書
        /// [key]名前 or IP:Port
        /// [Value] TcpServerUtility.ClientManager
        /// </summary>
        private readonly Dictionary<string, TcpServerUtility.ClientManager> dicTcpClient = new Dictionary<string, TcpServerUtility.ClientManager>();

        private CancellationTokenSource cancelTokenSource = null;

        private readonly int taskTimeOut = 10 * 1000;

        public FormTcpServer()
        {
            logger = Logger.GetInstance(GetType().Name);
            logger.StartMethod(nameof(FormTcpServer));

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

            logger.EndMethod(nameof(FormTcpServer));
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
            logger.StartMethod(MethodBase.GetCurrentMethod().Name, $"ActionMode:{mode}");

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

            logger.EndMethod(MethodBase.GetCurrentMethod().Name);
        }

        /// <summary>
        /// 開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ButtonF1_Click(object sender, EventArgs e)
        {
            logger.StartMethod(MethodBase.GetCurrentMethod().Name);
            base.ButtonF1_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            if (tcpServerUtil != null)
            {
                // 既に接続している場合、再接続前に破棄する
                tcpServerUtil.Dispose();
            }

            // Listen
            string ip = tboxIP.Text;
            int.TryParse(tboxPort.Text, out int port);
            tcpServerUtil = new TcpServerUtility(ip, port);

            listViewLog.Items.Add($"Listenを開始しました。IP[{ip}] Port[{port}]");

            // Task停止用のトークン発行
            cancelTokenSource = new CancellationTokenSource();
            CancellationToken cToken = cancelTokenSource.Token;

            // Accept Loop Start
            acceptLoopTask = Task.Run(() => AcceptLoop(cToken), cToken);

            // ボタンの有効無効を設定
            SetButtonEnabled(ActionMode.Listen);
            // ▲▲▲ 業務処理 ▲▲▲
            logger.EndMethod(MethodBase.GetCurrentMethod().Name);
        }

        private async Task AcceptLoop(CancellationToken cToken)
        {
            logger.StartMethod(nameof(AcceptLoop));
            try
            {
                while (!cToken.IsCancellationRequested)
                {
                    TcpServerUtility.ClientManager mgr = await tcpServerUtil.Accept().ConfigureAwait(false);

                    string acceptClientName = mgr.GetClientIpAndPort();

                    Invoke((Action)(() =>
                    {
                        listViewLog.Items.Add($"[{MethodBase.GetCurrentMethod().Name}]{acceptClientName}が接続しました。");
                    }));

                    // Read Loop Start
                    mgr.ReadTask = Task.Run(() => ReadLoop(mgr, cToken), cToken);

                    // 既に接続してるメンバー情報を送信
                    foreach (string clientName in dicTcpClient.Keys)
                    {
                        TcpMessageManager sendMsgMgr = new TcpMessageManager(TcpMessageManager.HeaderName, clientName, acceptClientName, clientName);
                        tcpServerUtil.SendTarget(mgr.GetClientIpAndPort(), sendMsgMgr.GetSendMessage());
                    }

                    // 管理用辞書に追加
                    dicTcpClient.Add(acceptClientName, mgr);
                }
            }
            catch (Exception ex)
            {
                logger.WriteException(nameof(AcceptLoop), ex);
            }
            logger.EndMethod(nameof(AcceptLoop));
        }

        private async Task ReadLoop(TcpServerUtility.ClientManager tcpClientMgr, CancellationToken cToken)
        {
            logger.StartMethod(nameof(ReadLoop));

            try
            {
                TcpClient client = tcpClientMgr.Client;
                while (!cToken.IsCancellationRequested)
                {
                    // メッセージ受信
                    string texts = await tcpServerUtil.ReadAsync(client, cToken);

                    if (texts != null)
                    {
                        // \nで分割
                        foreach (string text in texts.Split('\n'))
                        {
                            logger.WriteLine(text);

                            Invoke((Action)(() =>
                            {
                                listViewLog.Items.Add($"[Read]{text}");
                            }));

                            TcpMessageManager recvMsgMgr = new TcpMessageManager(text);
                            TcpMessageManager sendMsgMgr;
                            switch (recvMsgMgr.Header)
                            {
                                case TcpMessageManager.HeaderConnect:
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
                                    sendMsgMgr = new TcpMessageManager(TcpMessageManager.HeaderConnect, tcpClientMgr.GetClientIpAndPort(), recvMsgMgr.Value, tcpClientMgr.Name);
                                    tcpServerUtil.SendAll(sendMsgMgr.GetSendMessage());
                                    break;
                                case TcpMessageManager.HeaderTargetMsg:
                                    // 送信元を設定
                                    sendMsgMgr = new TcpMessageManager(TcpMessageManager.HeaderTargetMsg, tcpClientMgr.Name, recvMsgMgr.SendToTarget, recvMsgMgr.Value);

                                    Invoke((Action)(() =>
                                    {
                                        listViewLog.Items.Add(sendMsgMgr.GetRecvTargetMessage());
                                    }));

                                    // 受信したメッセージを対象に送信する
                                    // 名前→IP:Portへの変換
                                    string toTarget = dicTcpClient[recvMsgMgr.SendToTarget].GetClientIpAndPort();
                                    tcpServerUtil.SendTarget(toTarget, sendMsgMgr.GetSendMessage());

                                    // 受信したメッセージを送信元に送信する
                                    // 名前→IP:Portへの変換
                                    string fromTarget = tcpClientMgr.GetClientIpAndPort();
                                    tcpServerUtil.SendTarget(fromTarget, sendMsgMgr.GetSendMessage());
                                    break;
                                case TcpMessageManager.HeaderAllMsg:
                                default:
                                    // 送信元を設定
                                    sendMsgMgr = new TcpMessageManager(TcpMessageManager.HeaderAllMsg, tcpClientMgr.Name, recvMsgMgr.SendToTarget, recvMsgMgr.Value);

                                    Invoke((Action)(() =>
                                    {
                                        listViewLog.Items.Add(sendMsgMgr.GetRecvTargetMessage());
                                    }));

                                    // 受信したメッセージを全体に送信する
                                    tcpServerUtil.SendAll(sendMsgMgr.GetSendMessage());
                                    break;
                            }
                        }
                    }
                    else
                    {
                        // 切断
                        logger.WriteLine(nameof(ReadLoop), "切断されました。");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.WriteException(nameof(ReadLoop), ex);
            }

            logger.EndMethod(nameof(ReadLoop));
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

            TcpMessageManager sendMsgMgr;
            if (toName.Equals("全員"))
            {
                sendMsgMgr = new TcpMessageManager(TcpMessageManager.HeaderAllMsg, fromName, TcpMessageManager.TargetAll, sendMsg);
            }
            else
            {
                sendMsgMgr = new TcpMessageManager(TcpMessageManager.HeaderTargetMsg, fromName, toName, sendMsg);
            }

            tcpServerUtil.SendAll(sendMsgMgr.GetSendMessage());
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
        private void FormTcpServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            cancelTokenSource?.Cancel();

            // Lisner Stop And TcpClient Close
            tcpServerUtil?.Dispose();

            // Accept And Read Task Wait
            List<Task> waitTasks = new List<Task>();
            if (acceptLoopTask != null)
            {
                waitTasks.Add(acceptLoopTask);
            }
            foreach (TcpServerUtility.ClientManager mgr in dicTcpClient.Values)
            {
                waitTasks.Add(mgr.ReadTask);
            }
            if (Task.WhenAll(waitTasks).Wait(taskTimeOut))
            {
                logger.WriteLine("Task Timeout");
            }

            // Logger Dispose
            logger?.Dispose();
        }
        #endregion
    }
}
