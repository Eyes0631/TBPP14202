using PaeLibGeneral;
using PaeLibProVSDKEx;
using ProVLib;

namespace PaeLibVTrayModule
{
    /// <summary>
    /// 直盤收盤模組
    /// 使用說明：
    /// (o) --> IsNeedToUnloadTray() + IsLotEnd() -|
    ///     |-- No, Not LotEnd, but need to unloas tray    -NEXT--> Action_ResetUnLoadTray() --> Action_UnLoadTray() --> (o)
    ///     |-- No, Not LotEnd, do NOT need to unload tray -IDLE--! (Back to IsNeedToUnloadTray() + IsLotEnd())
    ///     |-- Yes, It's NotEnd and Trolleys are LotEnded -CASE1-> LotEnd().
    /// </summary>
    public class VtmUnLoader : VTrayStacker
    {
        #region 程序變數

        private JActionTask Task_Home = new JActionTask();
        private JActionTask Task_UnloadTray = new JActionTask();
        private bool IsTrayReady = false;   //v1.3.1 - Jay Cao Added : 判斷是否有 Tray 在軌道上等待收盤

        #endregion 程序變數

        #region 元件

        //Input

        //Cylinder
        private Cylinder CY_UnloadTray = null;     //收盤氣缸

        //Timer
        private MyTimer OverTime_Home = new MyTimer(true);

        private MyTimer OverTime_UnLoadTray = new MyTimer(true);

        #endregion 元件

        /// <summary>
        /// Initializes a new instance of the <see cref="VtmUnLoader" /> class.
        /// </summary>
        /// <param name="ibTrayFull">收料區滿盤偵測</param>
        /// <param name="ibTrayExist">收料區有無盤偵測</param>
        /// <param name="ibTrayOnTrack">收料區軌道有無盤偵測</param>
        /// <param name="ibTrayInPosition">收料區軌道盤定位偵測</param>
        /// <param name="obUnloadTray">收盤氣缸</param>
        /// <param name="ibUnloadTrayCyOn">The ib unload tray cy on.</param>
        /// <param name="ibUnloadTrayCyOff">The ib unload tray cy off.</param>
        public VtmUnLoader(
            AlarmCallback alarm,
            InBit ibTrayFull, InBit ibTrayExist, InBit ibTrayOnTrack,
            InBit ibTrayInPosition1, InBit ibTrayInPosition2, OutBit obUnloadTray, InBit ibUnloadTrayCyOn,
            InBit ibUnloadTrayCyOff)
            : base(alarm)
        {
            DI_TrayFull = new DigitalInput(ibTrayFull);
            DI_TrayExist = new DigitalInput(ibTrayExist);
            DI_TrayOnTrack = new DigitalInput(ibTrayOnTrack);
            DI_TrayInPosition1 = new DigitalInput(ibTrayInPosition1);
            DI_TrayInPosition2 = new DigitalInput(ibTrayInPosition2);
            CY_UnloadTray = new Cylinder(obUnloadTray, ibUnloadTrayCyOn, ibUnloadTrayCyOff);

            //設定計時器自動重置
            OverTime_Home.AutoReset = true;
            OverTime_UnLoadTray.AutoReset = true;
        }

        ~VtmUnLoader()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (DI_TrayFull != null) DI_TrayFull.Dispose();
            if (DI_TrayExist != null) DI_TrayExist.Dispose();
            if (DI_TrayOnTrack != null) DI_TrayOnTrack.Dispose();
            if (DI_TrayInPosition1 != null) DI_TrayInPosition1.Dispose();
            if (DI_TrayInPosition2 != null) DI_TrayInPosition2.Dispose();
            if (CY_UnloadTray != null) CY_UnloadTray.Dispose();
        }

        protected override void SimulationSetting2(bool simulation)
        {
            //覆寫父類別虛擬函式內容
            CY_UnloadTray.Simulation = simulation;
        }

        #region 私有function

        /// <summary>
        /// 收盤氣缸作動
        /// </summary>
        /// <param name="val">if set to <c>true</c> [收盤氣缸上升]</param>
        /// <param name="delay_ms">收盤氣缸作動後的延遲時間</param>
        /// <param name="timeout_ms">等待收盤氣缸作動的逾時時間</param>
        /// <returns>
        ///     <c>crDONE</c> 收盤氣缸作動且持續了 on_ms 的時間.
        ///     <c>crBUSY</c> 等待收盤氣缸作動且等待時間未超過 timeout_ms.
        ///     <c>crER_TIMEOUT</c> 等待時間超過 timeout_ms，但收盤氣缸仍未作動.
        ///     <c>crER_NULL</c> 收盤氣缸物件為 NULL.
        /// </returns>
        private ThreeValued UnloadTrayCtrl(bool val, int delay_ms, int timeout_ms)
        {
            ThreeValued tRet = ThreeValued.UNKNOWN;
            if (CY_UnloadTray != null)
            {
                if (val)
                {
                    tRet = CY_UnloadTray.On(delay_ms, timeout_ms);
                }
                else
                {
                    tRet = CY_UnloadTray.Off(delay_ms, timeout_ms);
                }
                if (tRet == ThreeValued.FALSE)
                {
                    if (val)
                    {
                        //Alarm : 收料區收盤氣缸上升動作逾時
                        this.ShowAlarm((int)VTMAlarmCode.TrayUnloaderUnloadTrayCylinderUpTimeout);
                    }
                    else
                    {
                        //Alarm : 收料區收盤氣缸下降動作逾時
                        this.ShowAlarm((int)VTMAlarmCode.TrayUnloaderUnloadTrayCylinderDownTimeout);
                    }
                }
            }
            else
            {
                //氣缸為空物件，直接回傳完成
                tRet = ThreeValued.TRUE;
            }
            return tRet;
        }

        #endregion 私有function

        #region 共用function

        /// <summary>
        /// 通知收盤
        /// v1.3.1 - Jay Cao Added
        /// ！！注意！！
        /// ！！僅供 VtmTrolley - Action_GiveTray() 使用！！
        /// ！！注意！！
        /// </summary>
        /// <param name="locker"></param>
        internal void InformToUnloadTray(VtmTrolley locker)
        {
            if ((locker != null) && (Var_Locker == locker) && !IsTrayReady)
            {
                IsTrayReady = true; //通知可進行收盤流程
            }
        }

        /// <summary>
        /// 判斷是否需要收盤
        /// </summary>
        /// <returns></returns>
        public bool IsNeedToUnloadTray()
        {
            //v1.3.1 Jay Cao Added : 檢查 tray unloader 是否有被鎖定 (避免 user 流程錯誤導致誤動作)
            if ((Var_Locker != null) && IsTrayReady)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 歸零流程重置
        /// </summary>
        /// <returns></returns>
        public void Action_ResetHome()
        {
            Var_Locker = null;
            IsTrayReady = false;    //v1.3.1 - Jay Cao Added
            Task_Home.Reset();
            OverTime_Home.Restart();
        }

        /// <summary>
        /// 歸零流程
        /// </summary>
        /// <returns></returns>
        public ThreeValued Action_Home()
        {
            //檢查動作總時間是否超過 30 秒
            if (OverTime_Home.On(30000))
            {
                OverTime_Home.Restart();
                //Alarm : 收料區歸零動作動作逾時
                this.ShowAlarm((int)VTMAlarmCode.TrayUnloaderHomeTimeout);
                return ThreeValued.FALSE;      //動作逾時
            }

            ThreeValued tRet = ThreeValued.UNKNOWN;
            JActionTask Task = Task_Home;
            switch (Task.Value)
            {
                case 0:     //收盤氣缸 OFF (下降)
                    {
                        ThreeValued tRetTemp = UnloadTrayCtrl(false, 100, 5000);
                        if (tRetTemp == ThreeValued.TRUE)
                        {
                            Task.Next(10);
                        }
                        else
                        {
                            tRet = tRetTemp;
                        }
                    }
                    break;

                case 10:     //檢查軌道上是否有盤殘留(有盤Alarm)
                    {
                        if (this.DryRun)
                        {
                            Task.Next(20);
                        }
                        else
                        {
                            bool b1 = TrayInPosition();
                            bool b2 = NothingOnTrack();
                            if (b1 && b2)
                            {
                                Task.Next(20);
                            }
                            else
                            {
                                //Alarm : 收料區軌道上有盤殘留
                                this.ShowAlarm((int)VTMAlarmCode.TrayUnloaderTrackTrayRemain);
                                tRet = ThreeValued.FALSE;
                            }
                        }
                    }
                    break;

                case 20:     //檢查收料區是否有盤殘留
                    {
                        if (!CheckInitialTrayExist || TrayNonExist())
                        {
                            Task.Next(1000);
                        }
                        else
                        {
                            //Alarm : 收料區有盤殘留
                            this.ShowAlarm((int)VTMAlarmCode.TrayUnloaderTrayRemain);
                            tRet = ThreeValued.FALSE;
                        }
                    }
                    break;

                case 1000:  //完成
                    {
                        tRet = ThreeValued.TRUE;
                    }
                    break;
            }
            return tRet;
        }

        /// <summary>
        /// 收盤流程重置
        /// </summary>
        /// <returns></returns>
        public bool Action_ResetUnLoadTray()
        {
            Task_UnloadTray.Reset();
            OverTime_UnLoadTray.Restart();
            return true;
        }

        /// <summary>
        /// 收盤流程
        /// </summary>
        /// <returns></returns>
        public ThreeValued Action_UnLoadTray()
        {
            //檢查動作總時間是否超過 90 秒
            if (OverTime_UnLoadTray.On(90000))
            {
                OverTime_UnLoadTray.Restart();
                //Alarm : 收料區收盤流程動作逾時
                this.ShowAlarm((int)VTMAlarmCode.TrayTrolleyUnloadTrayTimeout);
                return ThreeValued.FALSE;      //動作逾時
            }

            ThreeValued tRet = ThreeValued.UNKNOWN;
            JActionTask Task = Task_UnloadTray;
            switch (Task.Value)
            {
                case 0:     //判斷軌道上有無盤與檢查軌道盤定位是否良好
                    {
                        bool b1 = TrayInPosition();
                        bool b2 = TrayOnTrack();
                        if (b1 && b2)
                        {
                            Task.Next(10);
                        }
                        else
                        {
                            if (!b1 || !b2)
                            {
                                //Alarm : 收料區軌道盤位置不良
                                this.ShowAlarm((int)VTMAlarmCode.TrayUnloaderTrackTrayPositionError);
                            }
                            tRet = ThreeValued.FALSE;
                        }
                    }
                    break;

                case 10:     //檢查是否滿盤
                    {
                        if (IsFullTray().Equals(false))
                        {
                            Task.Next(20);
                        }
                        else
                        {
                            //Alarm : 收料區滿盤
                            this.ShowAlarm((int)VTMAlarmCode.TrayUnloaderTrayFull);
                            tRet = ThreeValued.FALSE;
                        }
                    }
                    break;

                case 20:     //收盤氣缸上升
                    {
                        ThreeValued tRetTemp = UnloadTrayCtrl(true, 500, 5000);
                        if (tRetTemp == ThreeValued.TRUE)
                        {
                            Task.Next(30);
                        }
                        else
                        {
                            tRet = tRetTemp;
                        }
                    }
                    break;

                case 30:     //收盤氣缸下降
                    {
                        ThreeValued tRetTemp = UnloadTrayCtrl(false, 100, 5000);
                        if (tRetTemp == ThreeValued.TRUE)
                        {
                            Task.Next(40);
                        }
                        else
                        {
                            tRet = tRetTemp;
                        }
                    }
                    break;

                case 40:     //判斷軌道上有無盤與檢查軌道盤定位是否良好
                    {
                        bool b1 = TrayInPosition();
                        bool b2 = NothingOnTrack();
                        if (b1 && b2)
                        {
                            Task.Next(50);
                        }
                        else
                        {
                            //Alarm : 收料區軌道上有盤殘留
                            this.ShowAlarm((int)VTMAlarmCode.TrayUnloaderTrackTrayRemain);
                            tRet = ThreeValued.FALSE;
                        }
                    }
                    break;

                case 50:     //檢查是否滿盤
                    {
                        if (IsFullTray().Equals(false))
                        {
                            IsTrayReady = false;    //v1.3.1 - Jay Cao Added
                            Var_Locker = null;  //v1.3.1 - Jay Cao Added : force unlock tray unloader
                            Task.Next(1000);
                        }
                        else
                        {
                            //Alarm : 收料區滿盤
                            this.ShowAlarm((int)VTMAlarmCode.TrayUnloaderTrayFull);
                            tRet = ThreeValued.FALSE;
                        }
                    }
                    break;

                case 1000:  //完成
                    {
                        tRet = ThreeValued.TRUE;
                    }
                    break;
            }
            return tRet;
        }

        #endregion 共用function
    }
}