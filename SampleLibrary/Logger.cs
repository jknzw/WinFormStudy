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
        /// インスタンス辞書
        /// </summary>
        private static readonly Dictionary<string, Logger> dicLog = new Dictionary<string, Logger>();

        /// <summary>
        /// ログ出力ループ継続フラグ
        /// </summary>
        private bool loopWriteLog = true;

        /// <summary>
        /// キュー
        /// </summary>
        private BlockingCollection<string> Que = null;

        /// <summary>
        /// タスク
        /// </summary>
        private Task task = null;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private Logger()
        {
            // コンストラクタの使用を禁止する
            Que = new BlockingCollection<string>(new ConcurrentQueue<string>(), BoundedCapacity);
        }

        /// <summary>
        /// インスタンス取得
        /// </summary>
        /// <param name="logFilePath">ログファイルパス</param>
        /// <returns>Logger</returns>
        public static Logger GetInstance(string logFilePath, int taskTimeout = 0)
        {
            if (dicLog.ContainsKey(logFilePath))
            {
                // 生成済の場合
                return dicLog[logFilePath];
            }
            else
            {
                // 未生成の場合
                Logger log = new Logger()
                {
                    LogFilePath = logFilePath,
                    TaskTimeout = taskTimeout,
                };
                dicLog.Add(logFilePath, log);

                // タスクの開始
                log.task = Task.Run(() => log.AsyncWriteLog());

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
                ret = Que.TryAdd(GetLogText(log));
            }
            else
            {
                ret = Que.TryAdd(GetLogText(log), timeout);
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
            while (loopWriteLog)
            {
                using (StreamWriter sw = new StreamWriter(LogFilePath, true))
                {
                    try
                    {
                        while (Que.Count > 0)
                        {
                            if (Que.TryTake(out string item, 1 * 1000))
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
                    loopWriteLog = false;
                    if (TaskTimeout == 0)
                    {
                        task.Wait();
                    }
                    else
                    {
                        task.Wait(TaskTimeout);
                    }

                    try
                    {
                        task.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                    finally
                    {

                    }

                    Que.Dispose();

                    dicLog.Remove(LogFilePath);
                }

                // null
                task = null;
                Que = null;

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
