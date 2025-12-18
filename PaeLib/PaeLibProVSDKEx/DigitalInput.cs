using PaeLibGeneral;
using ProVLib;
using System;

namespace PaeLibProVSDKEx
{
    /// <summary>
    /// 數位輸入訊號元件類別 Digital Input Component (必須搭配 ProVLib 的 InBit 物件使用)
    /// </summary>
    public class DigitalInput : IDisposable
    {
        #region 私有變數

        private bool m_Simulation = false;      //是否開啟模擬功能

        private InBit IB_Component = null;

        private JTimer TM_OnDelay = null;
        private JTimer TM_OffDelay = null;
        private JTimer TM_OnTimeout = null;
        private JTimer TM_OffTimeout = null;

        #endregion 私有變數

        #region JDigitalInput 建構子

        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalInput"/> class.
        /// </summary>
        /// <param name="ibComp">The ib comp.</param>
        public DigitalInput(InBit ibComp)
        {
            m_Simulation = false;

            IB_Component = ibComp;

            TM_OnDelay = new JTimer();
            TM_OffDelay = new JTimer();
            TM_OnTimeout = new JTimer();
            TM_OffTimeout = new JTimer();

            //設定計時器自動重置
            //TM_OnDelay.AutoReset = true;
            //TM_OffDelay.AutoReset = true;
            //TM_OnTimeout.AutoReset = true;
            //TM_OffTimeout.AutoReset = true;
        }

        #endregion JDigitalInput 建構子

        ~DigitalInput()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            //if (IB_Component != null) IB_Component.Dispose();
        }

        #region 公用屬性

        /// <summary>
        /// 是否開啟模擬功能
        /// </summary>
        /// <value>
        ///   <c>true</c> if simulation; otherwise, <c>false</c>.
        /// </value>
        public bool Simulation
        {
            get { return m_Simulation; }
            set { m_Simulation = value; }
        }

        /// <summary>
        /// 取得 DI 的 ON 訊號 (Read-Only)
        /// </summary>
        /// <value>
        ///   <c>true</c> if ON; otherwise, <c>false</c>.
        /// </value>
        public bool ValueOn
        {
            get
            {
                if (!m_Simulation && (IB_Component != null))
                {
                    return IB_Component.Value;
                }
                return true;
            }
        }

        /// <summary>
        /// 取得 DI 的 OFF 訊號 (Read-Only)
        /// </summary>
        /// <value>
        ///   <c>true</c> if OFF; otherwise, <c>false</c>.
        /// </value>
        public bool ValueOff
        {
            get
            {
                if (!m_Simulation && (IB_Component != null))
                {
                    return !IB_Component.Value;
                }
                return true;
            }
        }

        #endregion 公用屬性

        #region 公用函式

        /// <summary>
        /// 判斷 DI 的 ON 訊號
        /// </summary>
        /// <param name="on_ms">On delay time.</param>
        /// <param name="timeout_ms">等待 ON 的 timeout 時間.</param>
        /// <returns>
        ///     <c>ThreeValued.UNKNOWN</c> [訊號 Off 且等待時間未超過 timeout_ms ] 或 [訊號 On 但持續時間未達 on_ms ].
        ///     <c>ThreeValued.TRUE</c> 訊號 On 且持續了 on_ms 的時間.
        ///     <c>ThreeValued.FALSE</c> 訊號 Off 且等待時間已超過 timeout_ms .
        /// </returns>
        public ThreeValued On(int on_ms = 0, int timeout_ms = 0)
        {
            ThreeValued tRet = ThreeValued.UNKNOWN;
            //判斷是否有訊號輸入
            if (this.ValueOn)
            {
                //判斷是否有訊號輸入且維持了 on_ms 的時間
                if ((on_ms <= 0) || TM_OnDelay.Count(on_ms))
                {
                    tRet = ThreeValued.TRUE;
                }
            }
            else
            {
                //無訊號輸入，將計時器重置
                TM_OnDelay.Reset();       //Delay 計時器重置
                //判斷是否持續指定時間無訊號輸入
                if ((timeout_ms > 0) && TM_OnTimeout.Count(timeout_ms))
                {
                    tRet = ThreeValued.FALSE;
                }
            }
            return tRet;
        }

        /// <summary>
        /// 判斷 DI 的 OFF 訊號
        /// </summary>
        /// <param name="off_ms">Off delay time.</param>
        /// <param name="timeout_ms">等待 OFF 的 timeout 時間.</param>
        /// <returns>
        ///     <c>ThreeValued.UNKNOWN</c> [訊號 On 且等待時間未超過 timeout_ms ] 或 [訊號 Off 但持續時間未達 off_ms ].
        ///     <c>ThreeValued.TRUE</c> 訊號 Off 且持續了 off_ms 的時間.
        ///     <c>ThreeValued.FALSE</c> 訊號 On 且等待時間已超過 timeout_ms .
        /// </returns>
        public ThreeValued Off(int off_ms = 0, int timeout_ms = 0)
        {
            ThreeValued tRet = ThreeValued.UNKNOWN;
            //判斷是否無訊號輸入
            if (this.ValueOff)
            {
                //判斷是否無訊號輸入且維持了 off_ms 的時間
                if ((off_ms <= 0) || TM_OffDelay.Count(off_ms))
                {
                    tRet = ThreeValued.TRUE;
                }
            }
            else
            {
                //有訊號輸入，將計時器重置
                TM_OffDelay.Reset();      //Delay 計時器重置
                //判斷是否持續指定時間有訊號輸入
                if ((timeout_ms > 0) && TM_OffTimeout.Count(timeout_ms))
                {
                    tRet = ThreeValued.FALSE;
                }
            }
            return tRet;
        }

        #endregion 公用函式
    }
}