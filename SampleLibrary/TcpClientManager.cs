using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleLibrary
{
    public class TcpClientManager : IDisposable
    {
        private readonly Logger logger;

        private readonly Encoding encoding;

        private TcpClient tcpClient = null;

        //private Task _readLoopTask = null;

        //private CancellationTokenSource _cancelTokenSource = null;

        ///// <summary>
        ///// キュー
        ///// </summary>
        //private BlockingCollection<string> _que = new BlockingCollection<string>();

        public TcpClientManager(string ipString = null, int port = 2001, string encoding = "UTF-8")
        {
            logger = Logger.GetInstance(GetType().Name);
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            this.encoding = Encoding.GetEncoding(encoding);

            // 接続
            Connect(ipString, port);

            //// 読み込みループ
            //ReadTaskStart();
        }

        private void Connect(string ipString, int port)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            tcpClient = new TcpClient(ipString, port);

            // 送信タイムアウト設定
            tcpClient.GetStream().WriteTimeout = 10 * 1000;

            // 受信タイムアウト設定
            tcpClient.GetStream().ReadTimeout = 10 * 1000;
        }

        public string GetClientIp()
        {
            return ((IPEndPoint)tcpClient.Client.LocalEndPoint).Address.MapToIPv6().ToString();
        }

        public int GetClientPort()
        {
            return ((IPEndPoint)tcpClient.Client.LocalEndPoint).Port;
        }

        public string GetClientIpAndPort()
        {
            return $"{GetClientIp()}:{GetClientPort()}";
        }

        public string GetServerIp()
        {
            return ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.MapToIPv6().ToString();
        }

        public int GetServerPort()
        {
            return ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Port;
        }

        public string GetServerIpAndPort()
        {
            return $"{GetServerIp()}:{GetServerPort()}";
        }

        //private void ReadTaskStart()
        //{
        //    _logger.WriteLine(MethodBase.GetCurrentMethod().Name);

        //    if (_cancelTokenSource != null)
        //    {
        //        // 既にRead中の場合キャンセル
        //        _cancelTokenSource.Cancel();
        //        _cancelTokenSource.Dispose();
        //    }

        //    if (_readLoopTask != null)
        //    {
        //        // 既にタスクがある場合終了を待つ
        //        _readLoopTask.Wait(10 * 1000);
        //        _readLoopTask.Dispose();
        //    }

        //    // Task停止用のトークン発行
        //    _cancelTokenSource = new CancellationTokenSource();

        //    // 非同期Read
        //    _readLoopTask = Task.Run(() => ReadLoopAsync(_cancelTokenSource.Token), _cancelTokenSource.Token);
        //}

        //private async void ReadLoopAsync(CancellationToken token)
        //{
        //    _logger.WriteLine(MethodBase.GetCurrentMethod().Name);

        //    while (!token.IsCancellationRequested)
        //    {
        //        string resMsg = await Task.Run(() => ReadAsync(), token);

        //        foreach (string msg in resMsg.Split('\n'))
        //        {
        //            if (!_que.TryAdd(msg))
        //            {
        //                _logger.WriteLine($"TryAdd Error[{msg}]");
        //            }
        //        }
        //    }
        //}

        //public string Read()
        //{
        //    if (_que.Count > 0)
        //    {
        //        _logger.WriteLine(MethodBase.GetCurrentMethod().Name);

        //        if (_que.TryTake(out string retMsg, 1 * 1000))
        //        {
        //            return retMsg;
        //        }
        //        else
        //        {
        //            _logger.WriteLine("TryTake Error");
        //            return null;
        //        }
        //    }
        //    return null;
        //}


        public async Task<string> ReadAsync(CancellationToken token)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            NetworkStream ns = tcpClient.GetStream();

            string resMsg = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] resBytes = new byte[256];
                int resSize;
                do
                {
                    //データの一部を受信する
                    resSize = await ns.ReadAsync(resBytes, 0, resBytes.Length, token);

                    //Readが0を返した時はクライアントが切断したと判断
                    if (resSize == 0)
                    {
                        logger.WriteLine("切断されました。");
                        return null;
                    }

                    //受信したデータを蓄積する
                    ms.Write(resBytes, 0, resSize);

                    //まだ読み取れるデータがあるか、データの最後が\nでない時は、
                    //受信を続ける
                } while (ns.DataAvailable || resBytes[resSize - 1] != '\n');

                //受信したデータを文字列に変換
                resMsg = encoding.GetString(ms.GetBuffer(), 0, (int)ms.Length);

                // クローズ
                ms.Close();
            }
            //末尾の\nを削除
            resMsg = resMsg.TrimEnd('\n');

            logger.WriteLine($"受信MSG[{resMsg}]");

            return resMsg;
        }

        /// <summary>
        /// サーバーにデータを送信する
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sendMsg"></param>
        public bool Send(string sendMsg)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            bool ret = true;
            try
            {
                NetworkStream ns = tcpClient.GetStream();

                // 10秒でタイムアウト
                ns.WriteTimeout = 1000 * 10;

                //文字列をByte型配列に変換
                byte[] sendBytes = encoding.GetBytes(sendMsg + '\n');

                //データを送信する
                ns.Write(sendBytes, 0, sendBytes.Length);

                logger.WriteLine($"送信MSG[{sendMsg}]");
            }
            catch (SocketException ex)
            {
                ret = false;
                Debug.Print(ex.ToString());
                logger.WriteLine(ex.ToString());
            }
            return ret;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            if (!disposedValue)
            {
                if (disposing)
                {
                    // マネージ状態を破棄します (マネージ オブジェクト)。

                    // ClientのClose
                    tcpClient.GetStream().Close();
                    tcpClient.Close();
                    tcpClient.Dispose();

                    logger.Dispose();
                }

                // アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // 大きなフィールドを null に設定します。

                disposedValue = true;
            }
        }

        // 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        // ~TcpServer()
        // {
        //   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
        //   Dispose(false);
        // }

        public void Dispose()
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);

            // 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
