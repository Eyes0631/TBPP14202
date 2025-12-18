using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaeLibGeneral
{
    /// <summary>
    /// 定時器類別
    /// <para>具備自動重置的功能，重置的條件有二：</para>
    /// <para>1. 判斷呼叫者是否不同了：自動判斷呼叫端的"方法或屬性名稱+原始程式檔完整路徑+行號"是否有變，若有則代表呼叫者不同了。</para>
    /// <para>2. 若呼叫者相同，則判斷呼叫的間隔時間是否超過設定值，預設值為 200ms ，也是最小值。</para>
    /// </summary>
    /// <example>
    /// 使用方式有三：
    /// <code>
    /// JTimer tm = new JTimer();
    /// //方式一：定時計時 (以 500ms 為例)
    /// if(tm.Count(500))
    /// {
    ///     //時間到...
    /// }
    /// //方式二：倒數計時 (以 500ms 為例)
    /// long rt = tm.CountDown(500);    //距離到時還有 rt ms
    /// //方式三：碼表計時
    /// tm.Reset(); //重置碼表
    /// long et = tm.Start();     //碼表開始，並回傳已經過的時間(ms)
    /// tm.Pause(); //碼表暫停
    /// </code>
    /// </example>
    public class JTimer
    {
        private System.Diagnostics.Stopwatch sw = null;
        private int _autoResetInterval = 0; //自動重置的間隔時間
        private long _lastElapsedMS = 0;  //最後一次計時的時間總計
        private string _laserCallerInfo = string.Empty;
        private int _milliseconds = 0;
        private bool _reset = false;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="autoResetInterval">自動重置的間隔時間(預設值 200 ms)</param>
        public JTimer(int autoResetInterval = 200)
        {
            sw = new System.Diagnostics.Stopwatch();
            sw.Stop();
            sw.Reset();
            this.AutoResetInterval = autoResetInterval;
        }

        /// <summary>
        /// 自動重置的間隔時間
        /// </summary>
        public int AutoResetInterval
        {
            get
            {
                return _autoResetInterval;
            }
            set
            {
                //限制最小值為 200ms
                _autoResetInterval = Math.Min(value, 200);
            }
        }

        /// <summary>
        /// 重置計時
        /// </summary>
        /// <param name="ms">設定計時時間</param>
        public void Reset()
        {
            _reset = true;
        }

        /// <summary>
        /// 開始計時
        /// </summary>
        /// <param name="caller">呼叫端的方法或屬性名稱</param>
        /// <param name="source">呼叫端的原始程式檔完整路徑</param>
        /// <param name="callerLineNumber">呼叫端的方法之原始程式檔中的行號</param>
        /// <returns>回傳計時的時間總計(ms)</returns>
        public long Start(//string caller, string source, int callerLineNumber)
            [System.Runtime.CompilerServices.CallerMemberName] string caller = "",
            [System.Runtime.CompilerServices.CallerFilePath] string source = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0)
        {
            string _currentCallerInfo = string.Format("{0}:{1}:{2}", caller, source, lineNumber); //呼叫端資訊
            //判斷此次呼叫端與前次呼叫端是否相同
            if (_currentCallerInfo.Equals(_laserCallerInfo))
            {
                //同一呼叫端
                if ((false == _reset) && (_autoResetInterval > 0))
                {
                    long intervalMS = (sw.ElapsedMilliseconds - _lastElapsedMS);
                    if (intervalMS > _autoResetInterval)
                    {
                        //同一呼叫端且此次呼叫與前次呼叫間隔時間超過 intervalMS 設定值 => Timer Reset
                        _reset = true;
                        JLogger.LogDebug("JTimer", string.Format("JTimer auto reset. (Interval= {0} ms, caller : {1})", intervalMS, _currentCallerInfo));
                    }
                }
            }
            else
            {
                //不同呼叫端 => Timer Reset
                _reset = true;
            }

            if (_reset)
            {
                _reset = false;
                sw.Stop();
                sw.Reset();
            }
            sw.Start();
            _laserCallerInfo = string.Format("{0}:{1}:{2}", caller, source, lineNumber);
            _lastElapsedMS = sw.ElapsedMilliseconds;  //紀錄最後一次計時的時間總計
            return _lastElapsedMS;
        }

        /// <summary>
        /// 暫停計時
        /// </summary>
        public void Pause()
        {
            sw.Stop();
        }

        /// <summary>
        /// 倒數計時 (Timer mode)，回傳剩餘時間。
        /// </summary>
        /// <param name="ms">指定倒數毫秒數</param>
        /// <param name="memberName">呼叫端的方法或屬性名稱</param>
        /// <param name="sourceFilePath">呼叫端的原始程式檔完整路徑</param>
        /// <param name="sourceLineNumber">呼叫端的方法之原始程式檔中的行號</param>
        /// <returns>剩餘毫秒數</returns>
        public long CountDown(int ms = 0,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (ms > 0)
            {
                _milliseconds = ms;
            }
            long RemainMS = Math.Max(0, (_milliseconds - this.Start(memberName, sourceFilePath, sourceLineNumber)));
            return RemainMS;
        }

        /// <summary>
        /// 定時計時 (Stopwatch mode)，時間到則回傳true，否則回傳false。
        /// </summary>
        /// <param name="ms">指定計時毫秒數</param>
        /// <param name="memberName">呼叫端的方法或屬性名稱</param>
        /// <param name="sourceFilePath">呼叫端的原始程式檔完整路徑</param>
        /// <param name="sourceLineNumber">呼叫端的方法之原始程式檔中的行號</param>
        /// <returns>
        ///   <c>true</c> 時間到(當耗用的毫秒數大於等於指定計時毫秒數時); 否則, <c>false</c>.
        /// </returns>
        public bool Count(int ms = 0,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            if (ms >= 0)
            {
                _milliseconds = ms;
            }
            long RemainMS = Math.Max(0, (_milliseconds - this.Start(memberName, sourceFilePath, sourceLineNumber)));
            bool bRet = (RemainMS <= 0);
            return bRet;
        }
    }
}