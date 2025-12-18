using PaeLibGeneral;
using ProVLib;
using System;

namespace PaeLibProVSDKEx
{
    /// <summary>
    /// 氣缸元件類別 Cylinder Component (必須搭配 ProVLib 的 InBit, OutBit 物件使用)
    /// </summary>
    public class Cylinder : IDisposable
    {
        #region 私有變數

        private bool m_Simulation = false;
        private DigitalInput DI_On = null;     //氣缸 On Sensor
        private DigitalInput DI_Off = null;    //氣缸 Off Sensor
        private OutBit OB_On = null;          //氣缸控制 ON DO
        private OutBit OB_Off = null;          //氣缸控制 OFF DO

        #endregion 私有變數

        #region JCylinder 建構子

        /// <summary>
        /// Initializes a new instance of the <see cref="Cylinder"/> class.
        /// </summary>
        /// <param name="obOn">The ob on.</param>
        /// <param name="obOff">The ob off.</param>
        /// <param name="ibOn">The ib on.</param>
        /// <param name="ibOff">The ib off.</param>
        public Cylinder(OutBit obOn, OutBit obOff, InBit ibOn, InBit ibOff)
        {
            m_Simulation = false;
            OB_On = obOn;
            OB_Off = obOff;
            DI_On = new DigitalInput(ibOn);
            DI_Off = new DigitalInput(ibOff);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cylinder"/> class.
        /// </summary>
        /// <param name="obOn">The ob on.</param>
        /// <param name="ibOn">The ib on.</param>
        /// <param name="ibOff">The ib off.</param>
        public Cylinder(OutBit obOn, InBit ibOn, InBit ibOff)
        {
            OB_On = obOn;
            OB_Off = null;
            DI_On = new DigitalInput(ibOn);
            DI_Off = new DigitalInput(ibOff);
        }

        #endregion JCylinder 建構子

        ~Cylinder()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (DI_On != null) DI_On.Dispose();
            if (DI_Off != null) DI_Off.Dispose();
            //if (OB_On != null) OB_On.Dispose();
            //if (DI_Off != null) DI_Off.Dispose();
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
                DI_On.Simulation = value;
                DI_Off.Simulation = value;
            }
        }

        /// <summary>
        /// 取得氣缸 On Sensor 的 ON 訊號 (Read-Only)
        /// </summary>
        /// <value>
        ///   <c>true</c> if ON; otherwise, <c>false</c>.
        /// </value>
        public bool OnSensorValue
        {
            get { return DI_On.ValueOn; }
        }

        /// <summary>
        /// 取得氣缸 Off Sensor 的 ON 訊號 (Read-Only)
        /// </summary>
        /// <value>
        ///   <c>true</c> if ON; otherwise, <c>false</c>.
        /// </value>
        public bool OffSensorValue
        {
            get { return DI_Off.ValueOn; }
        }

        /// <summary>
        /// 取得與設定控制氣缸 ON 的 DO 的 ON/OFF 訊號
        /// </summary>
        /// <value>
        ///   <c>true</c> if ON; otherwise, <c>false</c>.
        /// </value>
        public bool OnCtrlValue
        {
            get
            {
                if (!m_Simulation && (OB_On != null))
                {
                    return OB_On.Value;
                }
                return false;
            }
            set
            {
                if (!m_Simulation && (OB_On != null))
                {
                    OB_On.Value = value;
                }
                if (!m_Simulation && (OB_Off != null))
                {
                    OB_Off.Value = !value;
                }
            }
        }

        /// <summary>
        /// 取得與設定控制氣缸 OFF 的 DO 的 ON/OFF 訊號
        /// </summary>
        /// <value>
        ///   <c>true</c> if OFF; otherwise, <c>false</c>.
        /// </value>
        public bool OffCtrlValue
        {
            get
            {
                if (!m_Simulation && (OB_Off != null))
                {
                    return OB_Off.Value;
                }
                return false;
            }
            set
            {
                if (!m_Simulation && (OB_Off != null))
                {
                    OB_Off.Value = value;
                }
                if (!m_Simulation && (OB_On != null))
                {
                    OB_On.Value = !value;
                }
            }
        }

        #endregion 公用屬性

        #region 公用函式

        /// <summary>
        /// 設定氣缸控制的 DO 訊號為 ON
        /// </summary>
        /// <param name="on_ms">On delay time.</param>
        /// <param name="timeout_ms">等待 ON 的 timeout 時間.</param>
        /// <returns>
        /// ThreeValued.TRUE : 氣缸 On Sensor On，且持續 on_ms 時間
        /// ThreeValued.FALSE : 氣缸 On Sensor Off，且持續超過 timeout_ms 時間
        /// ThreeValued.UNKNOW : 氣缸 On Sensor On，但持續時間未達 on_ms，或氣缸 On Sensor Off，但持續時間未超過 timeout_ms
        /// </returns>
        public ThreeValued On(int on_ms = 0, int timeout_ms = 0)
        {
            this.OnCtrlValue = true;     //氣缸 ON
            ThreeValued tRet = this.DI_On.On(on_ms, timeout_ms);
            return tRet;
        }

        /// <summary>
        /// 設定氣缸控制的 DO 訊號為 OFF
        /// </summary>
        /// <param name="off_ms">Off delay time.</param>
        /// <param name="timeout_ms">等待 OFF 的 timeout 時間.</param>
        /// <returns>
        /// ThreeValued.TRUE : 氣缸 Off Sensor On，且持續 off_ms 時間
        /// ThreeValued.FALSE : 氣缸 Off Sensor Off，且持續超過 timeout_ms 時間
        /// ThreeValued.UNKNOW : 氣缸 Off Sensor On，但持續時間未達 off_ms，或氣缸 Off Sensor Off，但持續時間未超過 timeout_ms
        /// </returns>
        public ThreeValued Off(int off_ms = 0, int timeout_ms = 0)
        {
            this.OffCtrlValue = true;   //氣缸 ON
            ThreeValued tRet = DI_Off.On(off_ms, timeout_ms);
            return tRet;
        }

        /// <summary>
        /// 關閉氣缸 ON 與 OFF DO
        /// </summary>
        public void Free()
        {
            if (!m_Simulation)
            {
                if (OB_On != null)
                {
                    OB_On.Value = false;
                }
                if (OB_Off != null)
                {
                    OB_Off.Value = false;
                }
            }
        }

        #endregion 公用函式
    }
}