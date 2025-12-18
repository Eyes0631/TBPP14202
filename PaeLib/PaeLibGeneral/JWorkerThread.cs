using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaeLibGeneral
{
    /// <summary>
    /// 工作執行緒類別
    /// </summary>
    /// <example>
    /// <code>
    /// //執行緒要執行的內容
    /// public void DoSomething()
    /// {
    ///     ...
    /// }
    /// ...
    /// JWorkerThread wt = new JWorkerThread(DoSomething);
    /// wt.Start(); //啟動執行緒
    /// ...
    /// wt.Stop();  //停止執行緒(通常在解構子中呼叫)，記得要停止，否則 App 可能會無法正常關閉
    /// </code>
    /// </example>
    public class JWorkerThread
    {
        /* Initializes a new instance of the ManualResetEvent class
        * with a Boolean value indicating whether to set the initial state to signaled.
        */
        private ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        private ManualResetEvent _pauseEvent = new ManualResetEvent(true);
        private ThreadStart _doJob;
        private Thread _thread;
        //bool StopThread = false;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="job">工作函式名稱</param>
        public JWorkerThread(ThreadStart job)
        {
            _doJob = job;
        }

        private void Job()
        {
            //int cnt = 0;
            //while (!StopThread)
            while (true)
            {
                /* 封鎖目前執行緒, 直到waitHandle收到通知,
                * Timeout.Infinite表示無限期等候
                */
                _pauseEvent.WaitOne(Timeout.Infinite);
                /* return true if the current instance receives a signal.
                * If the current instance is never signaled, WaitOne never returns
                */
                if (_shutdownEvent.WaitOne(0))
                    break;
                /* if (_shutdownEvent.WaitOne(Timeout.Infinite))
                * 因為沒有收到signal, 所以會停在if()這一行, 造成cnt無法累加
                */

                _doJob();
                //Console.WriteLine("{0}", cnt++);

                Thread.Sleep(1);
            }
        }

        /// <summary>
        /// 啟動
        /// <para>呼叫 Stop() 停止</para>
        /// </summary>
        public void Start()
        {
            _thread = new Thread(Job);
            _thread.Start();
            //StopThread = false;
            //Console.WriteLine("Thread started running");
        }

        /// <summary>
        /// 暫停
        /// <para>呼叫 Resume() 繼續</para>
        /// </summary>
        public void Pause()
        {
            /* Sets the state of the event to nonsignaled,
            * causing threads to block.
            */
            _pauseEvent.Reset();
            //Console.WriteLine("Thread paused");
        }

        /// <summary>
        /// 繼續
        /// </summary>
        public void Resume()
        {
            /* Sets the state of the event to signaled,
            * allowing one or more waiting threads to proceed.
            */
            _pauseEvent.Set();
            //Console.WriteLine("Thread resuming ");
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            // Signal the shutdown event
            _shutdownEvent.Set();
            //Console.WriteLine("Thread Stopped ");

            // Make sure to resume any paused threads
            _pauseEvent.Set();

            // Wait for the thread to exit
            if (_thread != null)
            {
                //StopThread = true;
                _thread.Join();
            }
        }
    }
}