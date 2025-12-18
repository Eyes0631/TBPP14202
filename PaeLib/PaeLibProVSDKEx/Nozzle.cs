using PaeLibGeneral;
using ProVLib;
using System;

namespace PaeLibProVSDKEx
{
    /// <summary>
    /// 吸嘴狀態列舉
    /// </summary>
    [Flags]
    public enum NozzleState
    {
        /// <summary>
        /// 未使用(預設值)
        /// </summary>
        Unused = 1,

        /// <summary>
        /// 使用中
        /// </summary>
        InUsing = 2,

        /// <summary>
        /// 取放失敗(真空檢查Failure)
        /// </summary>
        PnPFailure = 4,

        ICLost = 8,
    }

    /// <summary>
    /// 吸嘴元件類別 Nozzle Component (必須搭配 ProVLib 的 InBit, OutBit 物件使用)
    /// </summary>
    public class Nozzle : IDisposable
    {
        #region 私有變數

        private bool m_Simulation = false;
        private bool m_DryRun = false;

        private Cylinder CY_Nozzle = null;
        private Vacuum VC_Nozzle = null;

        #endregion 私有變數

        #region JNozzle 建構子

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="obCy">吸嘴氣缸開關</param>
        /// <param name="ibCyOn">吸嘴氣缸 On Sensor</param>
        /// <param name="ibCyOff">吸嘴氣缸 Off Sensor</param>
        /// <param name="ibVac">吸嘴真空偵測 Sensor</param>
        /// <param name="obVac">吸嘴真空開關</param>
        /// <param name="obDes">吸嘴破壞開關</param>
        public Nozzle(OutBit obCy, InBit ibCyOn, InBit ibCyOff,
            InBit ibVac, OutBit obVac, OutBit obDes)
        {
            m_Simulation = false;
            m_DryRun = false;
            this.State = NozzleState.Unused;
            //this.ActionState = NozzleActionState.None;

            CY_Nozzle = new Cylinder(obCy, ibCyOn, ibCyOff);
            VC_Nozzle = new Vacuum(ibVac, obVac, obDes);
        }

        #endregion JNozzle 建構子

        ~Nozzle()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (CY_Nozzle != null) CY_Nozzle.Dispose();
            if (VC_Nozzle != null) VC_Nozzle.Dispose();
        }

        #region 公用屬性

        /// <summary>
        /// 是否開啟模擬功能
        /// </summary>
        public bool Simulation
        {
            get { return m_Simulation; }
            set
            {
                m_Simulation = value;
                CY_Nozzle.Simulation = value;
                VC_Nozzle.Simulation = value;
            }
        }

        /// <summary>
        /// 是否開啟空跑模式
        /// </summary>
        public bool DryRun
        {
            get { return m_DryRun; }
            set
            {
                m_DryRun = value;
                //CY_Nozzle.DryRun = value;
                VC_Nozzle.DryRun = value;
            }
        }

        /// <summary>
        /// 吸嘴狀態
        /// </summary>
        public NozzleState State { get; set; }

        /// <summary>
        /// 吸嘴動作狀態
        /// </summary>
        //private NozzleActionState _ActionState = NozzleActionState.None;

        //public NozzleActionState ActionState
        //{
        //    get
        //    {
        //        if (this.State != NozzleState.None || this.State != NozzleState.Disable)
        //        {
        //            return this._ActionState;
        //        }
        //        return NozzleActionState.None;
        //    }
        //    set
        //    {
        //        if (this.State != NozzleState.None || this.State != NozzleState.Disable)
        //        {
        //            this._ActionState = value;
        //        }
        //        else
        //        {
        //            this._ActionState = NozzleActionState.None;
        //        }
        //    }
        //}

        /// <summary>
        /// 取得吸嘴氣缸 ON Sensor 的輸入訊號 ON/OFF 值
        /// </summary>
        /// <value>
        ///   <c>true</c> if [on sensor value is ON]; otherwise, <c>false</c>.
        /// </value>
        public bool CyOnSensorValue
        {
            get { return CY_Nozzle.OnSensorValue; }
        }

        /// <summary>
        /// 取得吸嘴氣缸 OFF Sensor 的輸入訊號 ON/OFF 值
        /// </summary>
        /// <value>
        ///   <c>true</c> if [off sensor value is ON]; otherwise, <c>false</c>.
        /// </value>
        public bool CyOffSensorValue
        {
            get { return CY_Nozzle.OffSensorValue; }
        }

        /// <summary>
        /// 取得與設定吸嘴氣缸開關輸出訊號 ON/OFF 值
        /// </summary>
        /// <value>
        ///   <c>true</c> if [cilinder is ON]; otherwise, <c>false</c>.
        /// </value>
        public bool CyCtrlValue
        {
            get { return CY_Nozzle.OnCtrlValue; }
            set { CY_Nozzle.OnCtrlValue = value; }
        }

        /// <summary>
        /// 取得吸嘴真空檢知 Sensor 的輸入訊號 ON/OFF 值
        /// </summary>
        /// <value>
        ///   <c>true</c> if [vacuum value is ON]; otherwise, <c>false</c>.
        /// </value>
        public bool VacuumValueOn
        {
            get { return VC_Nozzle.VacuumValueOn; }
        }

        ///// <summary>
        ///// 取得吸嘴真空檢知 Sensor 的輸入訊號 OFF 值
        ///// </summary>
        ///// <value>
        /////   <c>true</c> if [vacuum value is OFF]; otherwise, <c>false</c>.
        ///// </value>
        //public bool VacuumValueOff
        //{
        //    get { return VC_Nozzle.VacuumValueOff; }
        //}

        /// <summary>
        /// 取得與設定吸嘴真空開關輸出訊號 ON/OFF 值
        /// </summary>
        /// <value>
        ///   <c>true</c> if vacuum ON (destory OFF); otherwise, <c>false</c>.
        /// </value>
        public bool VacuumCtrlValue
        {
            get { return VC_Nozzle.VacuumCtrlValue; }
            set { VC_Nozzle.VacuumCtrlValue = value; }
        }

        /// <summary>
        /// 取得與設定吸嘴破壞開關輸出訊號 ON/OFF 值
        /// </summary>
        /// <value>
        ///   <c>true</c> if destory ON (vacuum OFF); otherwise, <c>false</c>.
        /// </value>
        public bool DestoryCtrlValue
        {
            get { return VC_Nozzle.DestoryCtrlValue; }
            set { VC_Nozzle.DestoryCtrlValue = value; }
        }

        #endregion 公用屬性

        #region 公用函式

        /// <summary>
        /// 吸嘴氣缸 ON
        /// </summary>
        /// <param name="on_ms">The on ms.</param>
        /// <param name="timeout_ms">The timeout ms.</param>
        /// <returns></returns>
        public ThreeValued CylinderOn(int on_ms = 0, int timeout_ms = 0)
        {
            ThreeValued tRet = CY_Nozzle.On(on_ms, timeout_ms);
            return tRet;
        }

        /// <summary>
        /// 吸嘴氣缸 OFF
        /// </summary>
        /// <param name="off_ms">The off ms.</param>
        /// <param name="timeout_ms">The timeout ms.</param>
        /// <returns></returns>
        public ThreeValued CylinderOff(int off_ms = 0, int timeout_ms = 0)
        {
            ThreeValued tRet = CY_Nozzle.Off(off_ms, timeout_ms);
            return tRet;
        }

        /// <summary>
        /// 吸嘴真空檢知(單純判斷真空值，不做真空建立動作)
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
            ThreeValued tRet = VC_Nozzle.VacuumCheck(on_ms, timeout_ms);
            return tRet;
        }

        /// <summary>
        /// 吸嘴真空建立，並做真空檢知
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
            ThreeValued tRet = VC_Nozzle.VacuumOnAndCheck(on_ms, timeout_ms);
            return tRet;
        }

        /// <summary>
        /// 吸嘴真空建立
        /// </summary>
        /// <param name="on_ms">The on ms.</param>
        /// <param name="timeout_ms">The timeout ms.</param>
        /// <returns>
        /// true : 開真空且延遲 on_ms 時間
        /// false : 開真空但延遲時間未達 on_ms
        /// </returns>
        public bool VacuumOn(int on_ms = 0)
        {
            bool bRet = VC_Nozzle.VacuumOn(on_ms);
            return bRet;
        }

        /// <summary>
        /// 吸嘴真空關閉
        /// </summary>
        /// <param name="off_ms">真空關閉後的延遲時間</param>
        /// <returns>
        /// true : 關真空且延遲 off_ms 時間
        /// false : 關真空但延遲時間未達 off_ms
        /// </returns>
        public bool VacuumOff(int off_ms = 0)
        {
            bool bRet = VC_Nozzle.VacuumOff(off_ms);
            return bRet;
        }

        /// <summary>
        /// 吸嘴破壞開啟，並於 on_ms 時間後關閉
        /// </summary>
        /// <param name="on_ms">破壞開啟時間，時間到即關閉破壞</param>
        /// <returns>
        /// true : 開破壞且延遲 on_ms 時間
        /// false : 開破壞但延遲時間未達 on_ms
        /// </returns>
        public bool DestoryOn(int on_ms = 100)
        {
            bool bRet = VC_Nozzle.DestoryOn(on_ms);
            return bRet;
        }

        /// <summary>
        /// 吸嘴破壞關閉
        /// </summary>
        /// <param name="off_ms">破壞關閉後的延遲時間</param>
        /// <returns>
        /// true : 關破壞且延遲 off_ms 時間
        /// false : 關破壞但延遲時間未達 off_ms
        /// </returns>
        public bool DestoryOff(int off_ms = 0)
        {
            bool bRet = VC_Nozzle.DestoryOff(off_ms);
            return bRet;
        }

        #endregion 公用函式
    }
}