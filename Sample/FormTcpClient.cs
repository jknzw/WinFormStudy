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


        private Task readLoopTask = null;

        public TcpClientManager TcpClientUtil { get; set; } = null;
        public CancellationTokenSource ReadCancelTokenSource { get; set; } = null;

        public Logger Logger { get; } = null;

        #endregion

        #region コンストラクタ
        public FormTcpClient()
        {
            Logger = Logger.GetInstance(GetType().Name);
            Logger.WriteLine(MethodBase.GetCurrentMethod().Name);

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
            SetControlEnabled(ActionMode.Init);
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
        private void SetControlEnabled(ActionMode mode)
        {
            Logger.WriteLine($"{MethodBase.GetCurrentMethod().Name} ActionMode:{mode}");

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
            Logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            base.ButtonF1_Click(sender, e);
            // ▼▼▼ 業務処理 ▼▼▼

            if (TcpClientUtil != null)
            {
                // 既に接続している場合、再接続前に破棄する
                TcpClientUtil.Dispose();
            }

            // 接続
            string ip = tboxIP.Text;
            int.TryParse(tboxPort.Text, out int port);

            try
            {
                TcpClientUtil = new TcpClientManager(ip, port);

                listViewLog.Items.Add($"接続しました。{TcpClientUtil.GetClientIpAndPort()}->{TcpClientUtil.GetServerIpAndPort()}");

                // 名前を送信
                string name = tboxName.Text;
                TcpMessageUtility mgr = new TcpMessageUtility(TcpMessageUtility.HeaderConnect, name, TcpMessageUtility.TargetAll, name);
                TcpClientUtil.Send(mgr.GetSendMessage());
            }
            catch (SocketException ex)
            {
                listViewLog.Items.Add(ex.Message);
                return;
            }

            // 読み込みループ開始
            // Task停止用のトークン発行
            ReadCancelTokenSource = new CancellationTokenSource();
            CancellationToken cToken = ReadCancelTokenSource.Token;
            readLoopTask = Task.Run(() => ReadLoop(cToken), cToken);

            // ボタンの有効無効を設定
            SetControlEnabled(ActionMode.Connect);
            // ▲▲▲ 業務処理 ▲▲▲
        }

        private async void ReadLoop(CancellationToken cToken)
        {
            Logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            try
            {
                while (!cToken.IsCancellationRequested)
                {
                    string texts = await TcpClientUtil.ReadAsync(cToken);

                    if (texts != null)
                    {
                        foreach (string text in texts.Split('\n'))
                        {
                            Logger.WriteLine(text);

                            TcpMessageUtility mgr = new TcpMessageUtility(text);
                            switch (mgr.Header)
                            {
                                case TcpMessageUtility.HeaderConnect:
                                    // 申請した名前が登録できたか判定する
                                    // Connect,IP:Port,送信した名前,登録した名前
                                    if (TcpClientUtil.GetClientIpAndPort().Equals(mgr.SendFromTarget))
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
                                case TcpMessageUtility.HeaderName:
                                    // リストに追加する
                                    Invoke((Action)(() =>
                                    {
                                        listBoxUser.Items.Add(mgr.Value);
                                        cboxTarget.Items.Add(mgr.Value);
                                    }));
                                    break;
                                case TcpMessageUtility.HeaderTargetMsg:
                                    Invoke((Action)(() =>
                                    {
                                        listViewLog.Items.Add(mgr.GetRecvTargetMessage());
                                    }));
                                    break;
                                case TcpMessageUtility.HeaderAllMsg:
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
                        Logger.WriteLine("切断されました。");
                        Invoke((Action)(() =>
                        {
                            listViewLog.Items.Add("切断されました。");
                            CloseAndInit();
                        }));
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
            Logger.WriteLine(MethodBase.GetCurrentMethod().Name);
            base.ButtonF5_Click(sender, e);

            // ▼▼▼ 業務処理 ▼▼▼
            // メッセージ送信
            string sendMsg = tboxMessage.Text;
            string toName = cboxTarget.Text;
            string fromName = tboxName.Text;
            TcpMessageUtility mgr;
            if (toName.Equals("全員"))
            {
                mgr = new TcpMessageUtility(TcpMessageUtility.HeaderAllMsg, fromName, TcpMessageUtility.TargetAll, sendMsg);
            }
            else
            {
                mgr = new TcpMessageUtility(TcpMessageUtility.HeaderTargetMsg, fromName, toName, sendMsg);
            }

            if (!TcpClientUtil.Send(mgr.GetSendMessage()))
            {
                listViewLog.Items.Add("送信に失敗しました。再接続して下さい。");
                CloseAndInit();
            }

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
            Logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            CloseTcpClient();

            // Logger破棄
            Logger.Dispose();
        }

        private void CloseAndInit()
        {
            // 破棄処理
            CloseTcpClient();

            // コントロール初期化
            SetControlEnabled(ActionMode.Init);
        }

        private void CloseTcpClient()
        {
            // 受信キャンセル
            ReadCancelTokenSource.Cancel();
            readLoopTask?.Wait(1 * 1000);
            readLoopTask?.Dispose();
            readLoopTask = null;

            // TCP破棄
            TcpClientUtil?.Dispose();
            TcpClientUtil = null;
        }
        #endregion
    }
}
