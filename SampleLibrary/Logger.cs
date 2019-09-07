using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SampleLibrary
{
    public class Logger : IDisposable
    {
        #region プロパティ

        /// <summary>
        /// ベースファイル名
        /// </summary>
        private string BaseFileName { get; set; }

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
        public int WriteDelay { get; set; } = (int)(0.5 * 1000);

        /// <summary>
        /// 終了時のWait時間(ms)
        /// 0は無制限
        /// </summary>
        public int TaskTimeout { get; set; } = 0;
        #endregion

        #region フィールド
        #region AsyncLock
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

        ///// <summary>
        ///// ロックオブジェクト
        ///// </summary>
        //private static readonly AsyncLock _asyncLock = new AsyncLock();
        #endregion

        /// <summary>
        /// ロックオブジェクト
        /// </summary>
        private static readonly Dictionary<string, AsyncLock> dicLockObj = new Dictionary<string, AsyncLock>();

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
        public static Logger GetInstance(string baseFileName, int taskTimeout = 0)
        {
            // ログファイルパスの生成
            string logFilePath = $"{baseFileName}{DateTime.Now.ToString("yyyyMMdd")}.log";

            Logger log = new Logger()
            {
                BaseFileName = baseFileName,
                LogFilePath = logFilePath,
                TaskTimeout = taskTimeout,
            };

            // LockObjectを静的領域に保持(プログラム終了まで保持)
            if (!dicLockObj.ContainsKey(logFilePath))
            {
                dicLockObj.Add(logFilePath, new AsyncLock());
            }

            // タスクの開始
            log._task = Task.Run(() => log.AsyncWriteLog());

            return log;
        }

        /// <summary>
        /// ログ出力(Wait)
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool WriteLineWait(string log)
        {
            return WriteLine(log, System.Threading.Timeout.Infinite);
        }

        #region WriteLine
        public bool StartMethod(string methodName, params string[] values)
        {
            if (values.Length > 0)
            {
                return WriteLine($"[{methodName}] Method Start [{string.Join("][", values)}]");
            }
            else
            {
                return WriteLine($"[{methodName}] Method Start");
            }
        }

        public bool EndMethod(string methodName, params string[] values)
        {
            if (values.Length > 0)
            {
                return WriteLine($"[{methodName}] Method End [{string.Join("][", values)}]");
            }
            else
            {
                return WriteLine($"[{methodName}] Method End");
            }
        }
        public bool WriteException(string methodName, Exception ex, int timeout = 0)
        {
            return WriteLine($"[{methodName}] Exception[{ex}]", timeout);
        }

        public bool WriteExceptionMessage(string methodName, Exception ex, int timeout = 0)
        {
            return WriteLine($"[{methodName}] Message[{ex.Message}]", timeout);
        }

        public bool WriteLog(string methodName, params dynamic[] values)
        {
            if (values.Length > 0)
            {
                List<string> valueList = new List<string>();
                foreach (dynamic value in values)
                {
                    valueList.Add(value?.ToString());
                }
                return WriteLine($"[{methodName}] [{string.Join("][", values)}]");
            }
            else
            {
                return WriteLine($"[{methodName}]");
            }
        }

        public bool WriteLine(string methodName, string log, int timeout = 0)
        {
            return WriteLine($"[{methodName}]{log}", timeout);
        }

        /// <summary>
        /// ログ出力(非同期) 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool WriteLine(string log, int timeout = 0)
        {
            bool ret = false;

            try
            {
                Debug.WriteLine($"[{BaseFileName}][{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fffffff")}]{log}");

                // キューに追加
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
                    // キューの追加に失敗
                    Debug.WriteLine($"Que TryAdd false [{log}]");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[{MethodBase.GetCurrentMethod().Name}][{ex.Message}]");
            }

            return ret;
        }
        #endregion

        /// <summary>
        /// ログ出力用文字列の取得
        /// 年月日日付を付加する
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        private string GetLogText(string log)
        {
            return $"[{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fffffff")}]{log}";
        }

        /// <summary>
        /// ログ出力
        /// </summary>
        /// <returns></returns>
        private async Task AsyncWriteLog()
        {
            while (_loopWriteLog)
            {
                if (_que.Count != 0)
                {
                    // キューがある場合
                    using (await dicLockObj[LogFilePath].LockAsync())
                    {
                        // ファイルを開く
                        using (StreamWriter sw = new StreamWriter(LogFilePath, true))
                        {
                            try
                            {
                                // キューが無くなるまで書き込む
                                while (_que.Count > 0)
                                {
                                    if (_que.TryTake(out string item, 1 * 1000))
                                    {
                                        // 書き終わるまで処理を待つ
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
                                Debug.WriteLine($"[{MethodBase.GetCurrentMethod().Name}][{ex.Message}]");
                            }
                        }
                    }
                }
                // キューが無い場合 or 書き込み終わったら待機
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

                    //dicLockObj.Remove(LogFilePath);
                }

                // null
                _task = null;
                _que = null;

                disposedValue = true;
            }
        }

        //~Logger()
        //{
        //    Dispose(false);
        //}

        public void Dispose()
        {
            Dispose(true);

            //GC.SuppressFinalize(this);
        }
        #endregion
    }
}
