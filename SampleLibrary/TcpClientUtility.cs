using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleLibrary
{
    public class TcpClientUtility : IDisposable
    {
        private readonly Logger _logger;

        private readonly Encoding _enc;

        private TcpClient _client = null;

        private Task _readLoopTask = null;

        private CancellationTokenSource _cancelTokenSource = null;

        /// <summary>
        /// キュー
        /// </summary>
        private BlockingCollection<string> _que = new BlockingCollection<string>();

        public TcpClientUtility(string ipString = null, int port = 2001, string encoding = "UTF-8")
        {
            _logger = Logger.GetInstance(GetType().Name);
            _logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            _enc = Encoding.GetEncoding(encoding);

            // 接続
            Connect(ipString, port);

            // 読み込みループ
            ReadTaskStart();
        }

        private void Connect(string ipString, int port)
        {
            _logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            _client = new TcpClient(ipString, port);
        }

        private void ReadTaskStart()
        {
            _logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            if (_cancelTokenSource != null)
            {
                // 既にRead中の場合キャンセル
                _cancelTokenSource.Cancel();
                _cancelTokenSource.Dispose();
            }

            if (_readLoopTask != null)
            {
                // 既にタスクがある場合終了を待つ
                _readLoopTask.Wait(10 * 1000);
                _readLoopTask.Dispose();
            }

            // Task停止用のトークン発行
            _cancelTokenSource = new CancellationTokenSource();

            // 非同期Read
            _readLoopTask = Task.Run(() => ReadLoopAsync(_cancelTokenSource.Token), _cancelTokenSource.Token);
        }

        private async void ReadLoopAsync(CancellationToken token)
        {
            _logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            while (!token.IsCancellationRequested)
            {
                string resMsg = await Task.Run(() => ReadAsync(), token);
                if (!_que.TryAdd(resMsg))
                {
                    _logger.WriteLine($"TryAdd Error[{resMsg}]");
                }
            }
        }

        private async Task<string> ReadAsync()
        {
            _logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            NetworkStream ns = _client.GetStream();

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
                        _logger.WriteLine("切断されました。");
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

            _logger.WriteLine($"受信MSG[{resMsg}]");

            return resMsg;
        }

        /// <summary>
        /// サーバーにデータを送信する
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sendMsg"></param>
        public void Send(string sendMsg)
        {
            _logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            NetworkStream ns = _client.GetStream();

            // 10秒でタイムアウト
            ns.WriteTimeout = 1000 * 10;

            //文字列をByte型配列に変換
            byte[] sendBytes = _enc.GetBytes(sendMsg + '\n');

            //データを送信する
            ns.Write(sendBytes, 0, sendBytes.Length);

            _logger.WriteLine($"送信MSG[{sendMsg}]");
        }

        public string Read()
        {
            if (_que.Count > 0)
            {
                _logger.WriteLine(MethodBase.GetCurrentMethod().Name);

                if (_que.TryTake(out string retMsg, 1 * 1000))
                {
                    return retMsg;
                }
                else
                {
                    _logger.WriteLine("TryTake Error");
                    return null;
                }
            }
            return null;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            _logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            if (!disposedValue)
            {
                if (disposing)
                {
                    // マネージ状態を破棄します (マネージ オブジェクト)。

                    // ClientのClose
                    _client.GetStream().Close();
                    _client.Close();
                    _client.Dispose();

                    _logger.Dispose();
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
            _logger.WriteLine(MethodBase.GetCurrentMethod().Name);

            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);

            // 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
