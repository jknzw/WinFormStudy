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
    public class TcpServerUtility : IDisposable
    {
        private readonly Logger logger;

        private TcpListener listener = null;

        private readonly Dictionary<string, ClientManager> _dicClient = new Dictionary<string, ClientManager>();

        private readonly Encoding _enc;

        /// <summary>
        /// キュー
        /// </summary>
        private BlockingCollection<string> _que = new BlockingCollection<string>();

        public class ClientManager
        {
            public string Name { get; set; } = null;
            public TcpClient Client { get; }
            public Task ReadTask { get; set; } = null;
            //private CancellationTokenSource _cancelTokenSource;

            public ClientManager(TcpClient client)
            {
                Client = client;
            }

            //public ClientManager(Task task, TcpClient client, CancellationTokenSource cancelTokenSource)
            //{
            //    _task = task;
            //    Client = client;
            //    _cancelTokenSource = cancelTokenSource;
            //}

            public string GetIp()
            {
                return ((IPEndPoint)Client.Client.LocalEndPoint).Address.ToString();
            }

            public int GetPort()
            {
                return ((IPEndPoint)Client.Client.LocalEndPoint).Port;
            }
        }

        public TcpServerUtility(string ipString = null, int port = 2001, string encoding = null)
        {
            logger = Logger.GetInstance(GetType().Name);

            if (string.IsNullOrEmpty(encoding))
            {
                // Default
                _enc = Encoding.UTF8;
            }
            else
            {
                _enc = Encoding.GetEncoding(encoding);
            }

            Listen(ipString, port);
        }

        private void Listen(string ipString, int port)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            if (listener == null)
            {
                if (string.IsNullOrEmpty(ipString))
                {
                    // IPv4とIPv6の全てのIPアドレスをListenする

                    // Lisnerを生成
                    listener = new TcpListener(System.Net.IPAddress.IPv6Any, port);

                    // IPv6Onlyを0にする
                    listener.Server.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, 0);
                }
                else
                {
                    IPAddress ipAdd;
                    if (!IPAddress.TryParse(ipString, out ipAdd))
                    {
                        // 変換失敗時
                        // ホスト名と判断し、ホスト名からIPアドレスを取得する

                        IPHostEntry ipentry = Dns.GetHostEntry(ipString);
                        foreach (IPAddress ip in ipentry.AddressList)
                        {
                            if (ip.AddressFamily == AddressFamily.InterNetwork)
                            {
                                // IPv4
                                ipString = ip.ToString();
                                break;
                            }
                        }
                        ipAdd = IPAddress.Parse(ipString);
                    }
                    listener = new TcpListener(ipAdd, port);
                }
            }

            //Listenを開始する
            logger.WriteLine($"Listen" +
                $"IP:{((IPEndPoint)listener.LocalEndpoint).Address} Port:{((System.Net.IPEndPoint)listener.LocalEndpoint).Port})。");
            listener.Start();
        }

        public async Task<ClientManager> Accept()
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            TcpClient client = await listener.AcceptTcpClientAsync();

            string ip = ((IPEndPoint)client.Client.LocalEndPoint).Address.ToString();
            string port = ((IPEndPoint)client.Client.LocalEndPoint).Port.ToString();

            logger.WriteLine($"{MethodBase.GetCurrentMethod().Name} " +
                $"Accept IP:{ip}" +
                $"Port:{port})。");

            //// Task停止用のトークン発行
            //CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            //Task task = Task.Run(() => ReadLoopAsync(client, cancelTokenSource.Token), cancelTokenSource.Token);

            //ClientManager mgr = new ClientManager(task, client, cancelTokenSource);

            ClientManager mgr = new ClientManager(client);

            string key = $"{ip}:{port}";
            if (_dicClient.ContainsKey(key))
            {
                // 接続済の場合、破棄して後勝ち
                _dicClient[key].Client.Dispose();
                _dicClient[key] = mgr;
            }
            else
            {
                // 初回
                _dicClient.Add(key, mgr);
            }

            return mgr;
        }

        //private async void ReadLoopAsync(TcpClient client, CancellationToken token)
        //{
        //    _logger.WriteLine(MethodBase.GetCurrentMethod().Name);

        //    while (!token.IsCancellationRequested)
        //    {
        //        string resMsg = await Task.Run(() => ReadAsync(client), token);

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

        public async Task<string> ReadAsync(TcpClient client)
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            NetworkStream ns = client.GetStream();

            // 10秒でタイムアウト
            ns.ReadTimeout = 1000 * 10;

            string resMsg = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] resBytes = new byte[256];
                int resSize;
                do
                {
                    //データの一部を受信する
                    resSize = await ns.ReadAsync(resBytes, 0, resBytes.Length);

                    //Readが0を返した時はクライアントが切断したと判断
                    if (resSize == 0)
                    {
                        logger.WriteLine("クライアントが切断しました。");
                        return null;
                    }

                    //受信したデータを蓄積する
                    ms.Write(resBytes, 0, resSize);

                    //まだ読み取れるデータがあるか、データの最後が\nでない時は、
                    //受信を続ける
                } while (ns.DataAvailable || resBytes[resSize - 1] != '\n');

                //受信したデータを文字列に変換
                resMsg = _enc.GetString(ms.GetBuffer(), 0, (int)ms.Length);

                // クローズ
                ms.Close();
            }
            //末尾の\nを削除
            resMsg = resMsg.TrimEnd('\n');

            logger.WriteLine($"受信MSG[{resMsg}]");

            return resMsg;
        }

        public void SendAll(string sendMsg, string header = "[MSG]")
        {
            foreach (ClientManager mgr in _dicClient.Values)
            {
                TcpClient tcpClient = mgr.Client;
                Send(tcpClient, sendMsg, header);
            }
        }


        /// <summary>
        /// クライアントにデータを送信する
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sendMsg"></param>
        public void Send(TcpClient client, string sendMsg, string header = "[MSG]")
        {
            logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            NetworkStream ns = client.GetStream();

            // 10秒でタイムアウト
            ns.WriteTimeout = 1000 * 10;

            //文字列をByte型配列に変換
            byte[] sendBytes = _enc.GetBytes(header + sendMsg + '\n');

            //データを送信する
            ns.Write(sendBytes, 0, sendBytes.Length);

            logger.WriteLine($"送信MSG[{sendMsg}]");
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

                    // Listenの停止
                    listener.Stop();

                    // ClientのClose
                    foreach (ClientManager mgr in _dicClient.Values)
                    {
                        TcpClient client = mgr.Client;
                        client.GetStream().Close();
                        client.Close();
                        client.Dispose();
                    }

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
