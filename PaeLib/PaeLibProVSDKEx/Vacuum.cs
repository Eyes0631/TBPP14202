using PaeLibGeneral;
using ProVLib;
using System;

namespace PaeLibProVSDKEx
{
    /// <summary>
    /// 真空產生器元件類別 Vacuum Component (必須搭配 ProVLib 的 InBit, OutBit 物件使用)
    /// </summary>
    public class Vacuum : IDisposable
    {
        #region 私有變數

        private bool m_Simulation = false;
        private bool m_DryRun = false;

        private DigitalInput DI_Vacuum = null;     //真空檢知
        private OutBit OB_Vacuum = null;            //真空建立
        private OutBit OB_Destroy = null;           //破壞產生

        private JTimer TM_VacuumCheckDelay = null;
        private JTimer TM_VacuumOnAndCheckDelay = null;
        private JTimer TM_VacuumOnDelay = null;
        private JTimer TM_VacuumOffDelay = null;
        private JTimer TM_DestoryOnDelay = null;
        private JTimer TM_DestoryOffDelay = null;

        #endregion 私有變數

        #region JVacuum 建構子

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="ibVac">真空 Sensor</param>
        /// <param name="obVacuum">真空開關</param>
        /// <param name="obDestory">破壞開關</param>
        public Vacuum(InBit ibVac, OutBit obVacuum, OutBit obDestory)
        {
            m_Simulation = false;
            m_DryRun = false;

            DI_Vacuum = new DigitalInput(ibVac);
            OB_Vacuum = obVacuum;
            OB_Destroy = obDestory;

            TM_VacuumCheckDelay = new JTimer();
            TM_VacuumOnAndCheckDelay = new JTimer();
            TM_VacuumOnDelay = new JTimer();
            TM_VacuumOffDelay = new JTimer();
            TM_DestoryOnDelay = new JTimer();
            TM_DestoryOffDelay = new JTimer();

            //設定計時器自動重置
            //TM_VacuumCheckDelay.AutoReset = true;
            //TM_VacuumOnAndCheckDelay.AutoReset = true;
            //TM_VacuumOnDelay.AutoReset = true;
            //TM_VacuumOffDelay.AutoReset = true;
            //TM_DestoryOnDelay.AutoReset = true;
            //TM_DestoryOffDelay.AutoReset = true;
        }

        #endregion JVacuum 建構子

        ~Vacuum()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (DI_Vacuum != null) DI_Vacuum.Dispose();
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
            set
            {
                m_Simulation = value;
                DI_Vacuum.Simulation = value;
            }
        }

        /// <summary>
        /// 是否開啟空跑模式
        /// </summary>
        /// <value>
        ///   <c>true</c> if DryRun; otherwise, <c>false</c>.
        /// </value>
        public bool DryRun
        {
            get { return m_DryRun; }
            set
            {
                m_DryRun = value;
                //DI_Vacuum.DryRun = value;
            }
        }

        /// <summary>
        /// 取得真空檢知ON的結果 (Read-Only)
        /// </summary>
        /// <value>
        ///   <c>true</c> if ON; otherwise, <c>false</c>.
        /// </value>
        public bool VacuumValueOn
        {
            get
            {
                if (m_DryRun)
                {
                    return true;
                }
                return DI_Vacuum.ValueOn;
            }
        }

        /// <summary>
        /// 取得真空檢知OFF的結果 (Read-Only)
        /// </summary>
        /// <value>
        ///   <c>true</c> if ON; otherwise, <c>false</c>.
        /// </value>
        public bool VacuumValueOff
        {
            get { return DI_Vacuum.ValueOff; }
        }

        /// <summary>
        /// 取得與設定真空建立開關的 ON/OFF
        /// </summary>
        /// <value>
        ///   <c>true</c> if ON; otherwise, <c>false</c>.
        /// </value>
        public bool VacuumCtrlValue
        {
            get
            {
                if (!m_Simulation && (OB_Vacuum != null))
                {
                    return OB_Vacuum.Value;
                }
                return false;
            }
            set
            {
                if (!m_Simulation && (OB_Vacuum != null))
                {
                    OB_Vacuum.Value = value;
                }
                //若真空建立被開啟，則自動關閉破壞產生
                if (!m_Simulation && (OB_Destroy != null) && value)
                {
                    OB_Destroy.Value = false;
                }
            }
        }

        /// <summary>
        /// 取得與設定破壞產生開關的 ON/OFF
        /// </summary>
        /// <value>
        ///   <c>true</c> if ON; otherwise, <c>false</c>.
        /// </value>
        public bool DestoryCtrlValue
        {
            get
            {
                if (!m_Simulation && (OB_Destroy != null))
                {
                    return OB_Destroy.Value;
                }
                return false;
            }
            set
            {
                if (!m_Simulation && (OB_Destroy != null))
                {
                    OB_Destroy.Value = value;
                }
                //開破壞時自動關閉真空
                if (!m_Simulation && (OB_Vacuum != null) && value)
                {
                    OB_Vacuum.Value = false;
                }
            }
        }

        #endregion 公用屬性

        #region 公有函式

        /// <summary>
        /// 真空檢知(單純判斷真空值，不做真空建立動作)
        /// </summary>
        /// <param name="on_ms">The on ms.</param>
        /// <param name="timeout_ms">The timeout ms.</param>
        /// <returns>
        /// ThreeValued.TRUE : 真空偵測 Sensor On，且持續 on_ms 時間
        /// ThreeValued.FALSE : 真空偵測 Sensor Off，且持續超過 timeout_ms 時間
        /// ThreeValued.UNKNOW : 真空偵測 Sensor On，但持續時間未達 on_ms，或真空偵測 Sensor Off，但持續時間未超過 timeout_ms
        /// </returns>
        public ThreeValued VacuumCheck(int on_ms = 0, int timeout_ms = 0)
        {
            if (m_DryRun)
            {
                return ThreeValued.TRUE;
            }
            ThreeValued tRet = this.DI_Vacuum.On(on_ms, timeout_ms);
            return tRet;
        }

        /// <summary>
        /// 開真空並檢知
        /// </summary>
        /// <param name="on_ms">The on ms.</param>
        /// <param name="timeout_ms">The timeout ms.</param>
        /// <returns>
        /// ThreeValued.TRUE : 真空偵測 Sensor On，且持續 on_ms 時間
        /// ThreeValued.FALSE : 真空偵測 Sensor Off，且持續超過 timeout_ms 時間
        /// ThreeValued.UNKNOW : 真空偵測 Sensor On，但持續時間未達 on_ms，或真空偵測 Sensor Off，但持續時間未超過 timeout_ms
        /// </returns>
        public ThreeValued VacuumOnAndCheck(int on_ms = 50, int timeout_ms = 500)
        {
            this.VacuumCtrlValue = true;
            if (m_DryRun)
            {
                return ThreeValued.TRUE;
            }
            ThreeValued tRet = this.DI_Vacuum.On(on_ms, timeout_ms);
            return tRet;
        }

        /// <summary>
        /// 開真空
        /// </summary>
        /// <param name="on_ms">The on ms.</param>
        /// <param name="timeout_ms">The timeout ms.</param>
        /// <returns>
        /// true : 開真空且延遲 on_ms 時間
        /// false : 開真空但延遲時間未達 on_ms
        /// </returns>
        public bool VacuumOn(int on_ms = 0)
        {
            bool bRet = false;
            this.VacuumCtrlValue = true;
            if ((on_ms <= 0) || TM_VacuumOnDelay.Count(on_ms))
            {
                bRet = true;
            }
            return bRet;
        }

        /// <summary>
        /// 關真空
        /// </summary>
        /// <param name="off_ms">The off ms.</param>
        /// <returns>
        /// true : 關真空且延遲 off_ms 時間
        /// false : 關真空但延遲時間未達 off_ms
        /// </returns>
        public bool VacuumOff(int off_ms = 0)
        {
            bool bRet = false;
            this.VacuumCtrlValue = false;
            if ((off_ms <= 0) || TM_VacuumOffDelay.Count(off_ms))
            {
                bRet = true;
            }
            return bRet;
        }

        /// <summary>
        /// 開破壞，並延遲 on_ms 時間後關閉
        /// </summary>
        /// <param name="on_ms">The on ms.</param>
        /// <returns>
        /// true : 開破壞且延遲 on_ms 時間
        /// false : 開破壞但延遲時間未達 on_ms
        /// </returns>
        public bool DestoryOn(int on_ms = 100)
        {
            bool bRet = false;
            this.DestoryCtrlValue = true;
            if ((on_ms <= 0) || TM_DestoryOnDelay.Count(on_ms))
            {
                DestoryCtrlValue = false;
                bRet = true;
            }
            return bRet;
        }

        /// <summary>
        /// 關破壞
        /// </summary>
        /// <param name="off_ms">The off ms.</param>
        /// <returns>
        /// true : 關破壞且延遲 off_ms 時間
        /// false : 關破壞但延遲時間未達 off_ms
        /// </returns>
        public bool DestoryOff(int off_ms = 0)
        {
            bool bRet = false;
            this.DestoryCtrlValue = false;
            if ((off_ms <= 0) || TM_DestoryOffDelay.Count(off_ms))
            {
                bRet = true;
            }
            return bRet;
        }

        #endregion 公有函式
    }
}