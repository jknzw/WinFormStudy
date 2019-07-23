using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SampleLibrary
{
    public class Logger : IDisposable
    {
        #region プロパティ
        /// <summary>
        /// ログファイルパス
        /// 変更は禁止し、参照は許可する
        /// </summary>
        public string LogFilePath { get; private set; } = null;

        /// <summary>
        /// キューの上限
        /// </summary>
        public int BoundedCapacity { get; set; } = 10000;

        /// <summary>
        /// ログ出力ディレイ(ms)
        /// </summary>
        public int WriteDelay { get; set; } = 5 * 1000;

        /// <summary>
        /// 終了時のWait時間(ms)
        /// 0は無制限
        /// </summary>
        public int TaskTimeout { get; set; } = 0;
        #endregion

        #region フィールド
        /// <summary>
        /// AsyncLock
        /// https://www.hanselman.com/blog/ComparingTwoTechniquesInNETAsynchronousCoordinationPrimitives.aspx
        /// </summary>
        private sealed class AsyncLock
        {
            private readonly System.Threading.SemaphoreSlim m_semaphore
              = new System.Threading.SemaphoreSlim(1, 1);
            private readonly Task<IDisposable> m_releaser;

            public AsyncLock()
            {
                m_releaser = Task.FromResult((IDisposable)new Releaser(this));
            }

            public Task<IDisposable> LockAsync()
            {
                var wait = m_semaphore.WaitAsync();
                return wait.IsCompleted ?
                        m_releaser :
                        wait.ContinueWith(
                          (_, state) => (IDisposable)state,
                          m_releaser.Result,
                          System.Threading.CancellationToken.None,
                          TaskContinuationOptions.ExecuteSynchronously,
                          TaskScheduler.Default
                        );
            }
            private sealed class Releaser : IDisposable
            {
                private readonly AsyncLock m_toRelease;
                internal Releaser(AsyncLock toRelease) { m_toRelease = toRelease; }
                public void Dispose() { m_toRelease.m_semaphore.Release(); }
            }
        }

        /// <summary>
        /// インスタンス辞書
        /// </summary>
        private static readonly Dictionary<string, Logger> _dicLog = new Dictionary<string, Logger>();

        private static readonly AsyncLock _asyncLock = new AsyncLock();

        /// <summary>
        /// ログ出力ループ継続フラグ
        /// </summary>
        private bool _loopWriteLog = true;

        /// <summary>
        /// キュー
        /// </summary>
        private BlockingCollection<string> _que = null;

        /// <summary>
        /// タスク
        /// </summary>
        private Task _task = null;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private Logger()
        {
            // コンストラクタの使用を禁止する
            _que = new BlockingCollection<string>(new ConcurrentQueue<string>(), BoundedCapacity);
        }

        /// <summary>
        /// インスタンス取得
        /// </summary>
        /// <param name="logFilePath">ログファイルパス</param>
        /// <returns>Logger</returns>
        public static Logger GetInstance(string logFilePath, int taskTimeout = 0)
        {
            if (_dicLog.ContainsKey(logFilePath))
            {
                // 生成済の場合
                return _dicLog[logFilePath];
            }
            else
            {
                // 未生成の場合
                Logger log = new Logger()
                {
                    LogFilePath = logFilePath,
                    TaskTimeout = taskTimeout,
                };
                _dicLog.Add(logFilePath, log);

                // タスクの開始
                log._task = Task.Run(() => log.AsyncWriteLog());

                return log;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool WriteLineWait(string log)
        {
            return WriteLine(log, System.Threading.Timeout.Infinite);
        }

        public bool WriteLine(string log, int timeout = 0)
        {
            bool ret;
            if (timeout == 0)
            {
                ret = _que.TryAdd(GetLogText(log));
            }
            else
            {
                ret = _que.TryAdd(GetLogText(log), timeout);
            }
            if (!ret)
            {
                Debug.WriteLine($"Que TryAdd false [{log}]");
            }
            return ret;
        }

        private string GetLogText(string log)
        {
            return $"[{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fffffff")}]{log}";
        }

        private async Task AsyncWriteLog()
        {
            while (_loopWriteLog)
            {
                using (await _asyncLock.LockAsync())
                {
                    using (StreamWriter sw = new StreamWriter(LogFilePath, true))
                    {
                        try
                        {
                            while (_que.Count > 0)
                            {
                                if (_que.TryTake(out string item, 1 * 1000))
                                {
                                    Debug.WriteLine($"write[{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fffffff")}]append{item}");
                                    await sw.WriteLineAsync(item);
                                }
                                else
                                {
                                    Debug.WriteLine("TryTake Error");
                                    break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }
                    }
                }
                await Task.Delay(WriteDelay);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // 破棄処理

                    // 出力の終了を待つ
                    _loopWriteLog = false;
                    if (TaskTimeout == 0)
                    {
                        _task.Wait();
                    }
                    else
                    {
                        _task.Wait(TaskTimeout);
                    }

                    try
                    {
                        _task.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                    finally
                    {

                    }

                    _que.Dispose();

                    _dicLog.Remove(LogFilePath);
                }

                // null
                _task = null;
                _que = null;

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
