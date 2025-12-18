using PaeLibGeneral;
using PaeLibProVSDKEx;
using ProVLib;
using System;

//public enum CTRAYUNLOADER //TrayUnloader_使用Alarm範圍36~50
//{
//    crER_CTM_TrayUnloadTimeout = 36,    //收盤區收盤氣缸動作逾時 {0}
//    crER_CTM_TrayRemain = 37,           //收盤區盤殘留 {0}
//    crER_CTM_LockedByTrolley = 38,      //收盤區已被台車鎖定 {0}
//    crER_CTM_TrollryIDAssignError = 39, //收盤區台車編號指定錯誤 {0}
//    crER_CTM_TrayNonExist = 40,         //收盤區台車盤未至定位 {0}
//    crER_CTM_TrayInWrongPosition = 41,  //收盤區台車盤定位不良 {0}
//    crER_CTM_TrayFull = 42,             //收盤區盤堆疊高度過高 {0}
//};
namespace PaeLibCTrayModule
{
    /// <summary>
    /// 十字台車收盤區模組類別
    /// </summary>
    /// <remarks>
    /// 2020-12-04 v1.3 Jay Tsao : 增加收盤馬達(Z軸)功能
    /// </remarks>
    /// <seealso cref="ComponentSet.CTrayStacker"/>
    public class CTrayUnloader : CTrayStacker, IDisposable
    {
        #region 程序變數

        private int Pos_UnloadTrayZ = 0; //收盤馬達Z軸 - 收盤高度//2020-12-04 v1.3 Jay Tsao Added

        private JActionTask Task_Home = new JActionTask();
        private JActionTask Task_UnloadTray = new JActionTask();

        #endregion 程序變數

        #region 元件

        //Output
        private Cylinder CY_UnloadTray = null;     //收盤氣缸

        //Motor
        private Motor MT_UnloadTrayZ = null;               //收盤馬達Z軸 //2020-12-04 v1.3 Jay Tsao Added

        #endregion 元件

        /// <summary>
        /// Initializes a new instance of the <see cref="CTrayLoader" /> class.
        /// </summary>
        /// <param name="name">此物件名稱</param>
        /// <param name="alarm">模組 Alarm 的回呼函式</param>
        /// <param name="ibTrayFullV">盤堆疊區滿盤偵測-直</param>
        /// <param name="ibTrayFullH">盤堆疊區滿盤偵測-橫</param>
        /// <param name="ibTrayExistV">盤堆疊區有無盤偵測-直</param>
        /// <param name="ibTrayExistH">盤堆疊區有無盤偵測-橫</param>
        /// <param name="ibTopTrolleyTrayExist">上台車有無盤偵測</param>
        /// <param name="ibBotTrolleyTrayExist">下台車有無盤偵測</param>
        /// <param name="obUnloadTray">收盤氣缸</param>
        /// <param name="ibUnloadTrayCyOn">收盤氣缸 On Sensor</param>
        /// <param name="ibUnloadTrayCyOff">收盤氣缸 Off Sensor</param>
        public CTrayUnloader(string name, AlarmCallback alarm,
            InBit ibTrayFullV, InBit ibTrayFullH, InBit ibTrayExistV, InBit ibTrayExistH,
            InBit ibTopTrolleyTrayExist, InBit ibBotTrolleyTrayExist,
            OutBit obUnloadTray, InBit ibUnloadTrayCyOn, InBit ibUnloadTrayCyOff)
            : base(name, alarm)
        {
            DI_StackerTrayFullV = new DigitalInput(ibTrayFullV);
            DI_StackerTrayFullH = new DigitalInput(ibTrayFullH);
            DI_StackerTrayExistV = new DigitalInput(ibTrayExistV);
            DI_StackerTrayExistH = new DigitalInput(ibTrayExistH);
            DI_TopShuttleTrayExist = new DigitalInput(ibTopTrolleyTrayExist);
            DI_BottomShuttleTrayExist = new DigitalInput(ibBotTrolleyTrayExist);
            CY_UnloadTray = new Cylinder(obUnloadTray, ibUnloadTrayCyOn, ibUnloadTrayCyOff);

            // 以下為未使用
            DI_StackerTrayInPositionX1 = new DigitalInput(null);
            DI_StackerTrayInPositionX2 = new DigitalInput(null);
            DI_TopShuttleTrayInPositionX1 = new DigitalInput(null);
            DI_TopShuttleTrayInPositionX2 = new DigitalInput(null);
            DI_BottomShuttleTrayInPositionX1 = new DigitalInput(null);
            DI_BottomShuttleTrayInPositionX2 = new DigitalInput(null);
            MT_UnloadTrayZ = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CTrayLoader" /> class.
        /// <para>2020-12-04 v1.3 Jay Tsao Added, for Unload Tray Motor.</para>
        /// </summary>
        /// <param name="name">此物件名稱</param>
        /// <param name="alarm">模組 Alarm 的回呼函式</param>
        /// <param name="ibTrayFullV">盤堆疊區滿盤偵測-直</param>
        /// <param name="ibTrayFullH">盤堆疊區滿盤偵測-橫</param>
        /// <param name="ibTrayExistV">盤堆疊區有無盤偵測-直</param>
        /// <param name="ibTrayExistH">盤堆疊區有無盤偵測-橫</param>
        /// <param name="ibTopTrolleyTrayExist">上台車有無盤偵測</param>
        /// <param name="ibBotTrolleyTrayExist">下台車有無盤偵測</param>
        /// <param name="mtLoadTrayZ">收盤馬達Z軸</param>
        public CTrayUnloader(string name, AlarmCallback alarm,
           InBit ibTrayFullV, InBit ibTrayFullH, InBit ibTrayExistV, InBit ibTrayExistH,
           InBit ibTopTrolleyTrayExist, InBit ibBotTrolleyTrayExist, Motor mtUnloadTrayZ)
            : base(name, alarm)
        {
            DI_StackerTrayFullV = new DigitalInput(ibTrayFullV);
            DI_StackerTrayFullH = new DigitalInput(ibTrayFullH);
            DI_StackerTrayExistV = new DigitalInput(ibTrayExistV);
            DI_StackerTrayExistH = new DigitalInput(ibTrayExistH);
            DI_TopShuttleTrayExist = new DigitalInput(ibTopTrolleyTrayExist);
            DI_BottomShuttleTrayExist = new DigitalInput(ibBotTrolleyTrayExist);
            MT_UnloadTrayZ = mtUnloadTrayZ;

            // 以下為未使用
            DI_StackerTrayInPositionX1 = new DigitalInput(null);
            DI_StackerTrayInPositionX2 = new DigitalInput(null);
            DI_TopShuttleTrayInPositionX1 = new DigitalInput(null);
            DI_TopShuttleTrayInPositionX2 = new DigitalInput(null);
            DI_BottomShuttleTrayInPositionX1 = new DigitalInput(null);
            DI_BottomShuttleTrayInPositionX2 = new DigitalInput(null);
            CY_UnloadTray = new Cylinder(null,null,null);
        }

        ~CTrayUnloader()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (DI_StackerTrayFullV != null) DI_StackerTrayFullV.Dispose();
            if (DI_StackerTrayFullH != null) DI_StackerTrayFullH.Dispose();
            if (DI_StackerTrayExistV != null) DI_StackerTrayExistV.Dispose();
            if (DI_StackerTrayExistH != null) DI_StackerTrayExistH.Dispose();
            if (DI_StackerTrayInPositionX1 != null) DI_StackerTrayInPositionX1.Dispose();
            if (DI_StackerTrayInPositionX2 != null) DI_StackerTrayInPositionX2.Dispose();
            if (DI_TopShuttleTrayExist != null) DI_TopShuttleTrayExist.Dispose();
            if (DI_TopShuttleTrayInPositionX1 != null) DI_TopShuttleTrayInPositionX1.Dispose();
            if (DI_TopShuttleTrayInPositionX1 != null) DI_TopShuttleTrayInPositionX2.Dispose();
            if (DI_BottomShuttleTrayExist != null) DI_BottomShuttleTrayExist.Dispose();
            if (DI_BottomShuttleTrayInPositionX1 != null) DI_BottomShuttleTrayInPositionX1.Dispose();
            if (DI_BottomShuttleTrayInPositionX1 != null) DI_BottomShuttleTrayInPositionX2.Dispose();
            if (CY_UnloadTray != null) CY_UnloadTray.Dispose();
        }

        #region 公開屬性

        ///// <summary>
        ///// 是否使用空跑功能
        ///// </summary>
        ///// <value>
        /////   <c>true</c> 使用空跑功能; otherwise, <c>false</c>.
        ///// </value>
        //public bool DryRun
        //{
        //    get { return Para_DryRun; }
        //    set
        //    {
        //        base.DryRun = value;
        //        Para_DryRun = value;
        //    }
        //}

        ///// <summary>
        ///// 是否使用模擬功能
        ///// </summary>
        ///// <value>
        /////   <c>true</c> 使用模擬功能; otherwise, <c>false</c>.
        ///// </value>
        //public bool Simulation
        //{
        //    get { return Para_Simulation; }
        //    set
        //    {
        //        base.Simulation = value;
        //        Para_Simulation = value;

        //        CY_UnloadTray.Simulation = Para_Simulation;
        //    }
        //}

        //收盤馬達Z軸 - 收盤高度
        public int UnloadTrayZ
        {
            get { return Pos_UnloadTrayZ; }
            set { Pos_UnloadTrayZ = value; }
        }

        #endregion 公開屬性

        #region 私有函式

        //private void ShowModuleAlarm(int ret)
        //{
        //    if (ModuleAlarmCallback != null)
        //    {
        //        ModuleAlarmCallback(ret, false);
        //    }
        //}

        //private void ClearModuleAlarm(int ret)
        //{
        //    if (ModuleAlarmCallback != null)
        //    {
        //        ModuleAlarmCallback(ret, true);
        //    }
        //}

        protected override void SimulationSetting2(bool simulation)
        {
            if (CY_UnloadTray!=null)
            CY_UnloadTray.Simulation = simulation;
        }

        /// <summary>
        /// 收盤氣缸作動
        /// </summary>
        /// <param name="val">if set to <c>true</c> [盤分離氣缸開啟].</param>
        /// <param name="delay_ms">盤分離氣缸作動後的延遲時間</param>
        /// <param name="timeout_ms">等待盤分離氣缸作動的逾時時間</param>
        /// <returns></returns>
        private ThreeValued UnloadTrayCtrl(bool val, int delay_ms, int timeout_ms)
        {
            ThreeValued tRet = ThreeValued.UNKNOWN;
            if (val)
            {
                if (CY_UnloadTray != null)
                {
                    tRet = CY_UnloadTray.On(delay_ms, timeout_ms);
                    if (tRet.Equals(ThreeValued.FALSE))
                    {
                        //Alarm : {0} 收盤氣缸上升動作逾時({1})
                        this.ShowAlarm((int)CTMAlarmCode.ER_TLU_TrayUnloadCyOnTimeout);
                    }
                }
                // 2020-12-04 v1.3 Jay Tsao Added, for Unload Tray Motor.
                else if (MT_UnloadTrayZ != null)
                {
                    // 收盤馬達上升
                    if (MT_UnloadTrayZ.G00(Pos_UnloadTrayZ))
                    {
                        tRet = ThreeValued.TRUE;
                    }
                }
            }
            else
            {
                if (CY_UnloadTray != null)
                {
                    tRet = CY_UnloadTray.Off(delay_ms, timeout_ms);
                    if (tRet.Equals(ThreeValued.FALSE))
                    {
                        //Alarm : {0} 收盤氣缸下降動作逾時({1})
                        this.ShowAlarm((int)CTMAlarmCode.ER_TLU_TrayUnloadCyOffTimeout);
                    }
                }
                // 2020-12-04 v1.3 Jay Tsao Added, for Unload Tray Motor.
                else if (MT_UnloadTrayZ != null)
                {
                    // 收盤馬達下降
                    if (MT_UnloadTrayZ.G00(1000))
                    {
                        tRet = ThreeValued.TRUE;
                    }
                }
            }
            return tRet;
        }

        #endregion 私有函式

        #region 公有函式

        /// <summary>
        /// 歸零流程重置
        /// </summary>
        /// <returns></returns>
        public void Action_HomeReset()
        {
            base.Reset();
            Task_Home.Reset();
            if (MT_UnloadTrayZ != null)
            {
                MT_UnloadTrayZ.HomeReset();
            }
        }

        /// <summary>
        /// 模組歸零流程
        /// </summary>
        /// <returns></returns>
        public bool Action_Home()
        {
            bool bRet = false;
            JActionTask Task = Task_Home;
            switch (Task.Value)
            {
                case 0:
                    {
                        // 判斷使用 "收盤氣缸" 或 "收盤馬達"
                        if (MT_UnloadTrayZ == null)
                        {
                            // 收盤氣缸關閉
                            ThreeValued crRet = UnloadTrayCtrl(false, 300, 10000);
                            if (crRet.Equals(ThreeValued.TRUE))
                            {
                                this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TrayUnloadCyOffTimeout);
                                Task.Next(10);
                            }
                        }
                        else
                        {
                            // 收盤馬達歸零
                            if (MT_UnloadTrayZ.Home())
                            {
                                Task.Next(10);
                            }
                        }
                    }
                    break;

                case 10:     //檢查盤堆疊區是否無盤(有盤Alarm)
                    {
                        if (StackerTrayNonExist())
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TrayRemain);
                            Task.Next(1000);
                        }
                        else
                        {
                            //Alarm : 收盤區盤殘留 {0}
                            this.ShowAlarm((int)CTMAlarmCode.ER_TLU_TrayRemain);
                        }
                    }
                    break;

                case 1000:    //歸零完成
                    {
                        bRet = true;
                    }
                    break;
            }
            return bRet;
        }

        /// <summary>
        /// 收盤流程重置
        /// </summary>
        /// <param name="locker">鎖定者物件</param>
        /// <returns></returns>
        public bool Action_UnloadTrayReset(object locker)
        {
            if (this.Lock(locker))
            {
                this.ClearAlarm((int)CTMAlarmCode.ER_TLU_AlreadyLocked);
                Task_UnloadTray.Reset();
                return true;
            }
            //Alarm : 收盤區已被台車鎖定 {0}
            this.ShowAlarm((int)CTMAlarmCode.ER_TLU_AlreadyLocked);
            return false;
        }

        /// <summary>
        /// 收盤流程
        /// </summary>
        /// <param name="locker">鎖定者物件</param>
        /// <param name="tid">台車編號(用以判斷是哪一台車)</param>
        /// <returns></returns>
        public bool Action_UnloadTray(object locker, CTrayShuttleID tid)
        {
            if (!this.Lock(locker))
            {
                //Alarm : 收盤區已被台車鎖定 {0}
                this.ShowAlarm((int)CTMAlarmCode.ER_TLU_AlreadyLocked);
                return false;
            }

            if (!tid.Equals(CTrayShuttleID.TopCTrayShuttle) && !tid.Equals(CTrayShuttleID.BottomCTrayShuttle))
            {
                //Alarm : 收盤區台車編號指定錯誤 {0}
                this.ShowAlarm((int)CTMAlarmCode.ER_TLU_AssignWrongShuttleID);
                return false;
            }

            bool bRet = false;
            JActionTask Task = Task_UnloadTray;
            switch (Task.Value)
            {
                case 0:     //收盤氣缸關閉
                    {
                        ThreeValued crRet = UnloadTrayCtrl(false, 300, 10000);
                        if (crRet.Equals(ThreeValued.TRUE))
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TrayUnloadCyOffTimeout);
                            Task.Next(10);
                        }
                    }
                    break;

                case 10:    //檢查台車上是否有盤(無盤則Alarm : 台車無盤)
                    {
                        if (ShuttleTrayExist(tid))
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TrayNotOnShuttle);
                            Task.Next(20);
                        }
                        else
                        {
                            //Alarm : 收盤區台車盤未至定位 {0}
                            this.ShowAlarm((int)CTMAlarmCode.ER_TLU_TrayNotOnShuttle);
                        }
                    }
                    break;

                case 20:   //檢查台車盤是否定位良好(定位不良則發Alarm)
                    {
                        if (ShuttleTrayInPosition(tid))
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TrayInWrongPosition);
                            Task.Next(30);
                        }
                        else
                        {
                            //Alarm : 收盤區台車盤定位不良 {0}
                            this.ShowAlarm((int)CTMAlarmCode.ER_TLU_TrayInWrongPosition);
                        }
                    }
                    break;

                case 30:    //收盤氣缸開啟
                    {
                        ThreeValued crRet = UnloadTrayCtrl(true, 800, 10000);
                        if (crRet.Equals(ThreeValued.TRUE))
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TrayUnloadCyOnTimeout);
                            Task.Next(40);
                        }
                    }
                    break;

                case 40:    //收盤氣缸關閉
                    {
                        ThreeValued crRet = UnloadTrayCtrl(false, 300, 10000);
                        if (crRet.Equals(ThreeValued.TRUE))
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TrayUnloadCyOffTimeout);
                            Task.Next(50);
                        }
                    }
                    break;

                case 50:   //檢查台車是否無盤(有盤則發Alarm)
                    {
                        if (ShuttleTrayNonExist(tid))
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TrayRemainOnShuttle);
                            Task.Next(60);
                        }
                        else
                        {
                            //Alarm : 收盤區盤殘留 {0}
                            this.ShowAlarm((int)CTMAlarmCode.ER_TLU_TrayRemainOnShuttle);
                        }
                    }
                    break;

                case 60:    //判斷盤堆疊區是否未滿盤(滿盤則Alarm : 盤堆疊高度過高)
                    {
                        if (IsStackerNotFullTray())
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TrayFull);
                            Task.Next(1000);
                        }
                        else
                        {
                            //Alarm : 收盤區盤堆疊高度過高 {0}
                            this.ShowAlarm((int)CTMAlarmCode.ER_TLU_TrayFull);
                        }
                    }
                    break;

                case 1000:  //收盤完成
                    {
                        bRet = true;
                    }
                    break;
            }
            return bRet;
        }

        #endregion 公有函式
    }
}