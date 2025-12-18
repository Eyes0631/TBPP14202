using PaeLibGeneral;
using PaeLibProVSDKEx;
using ProVLib;

namespace PaeLibVTrayModule
{
    /// <summary>
    /// 直盤載盤模組
    /// 使用說明：
    /// (o) --> ActionReset_BeforeLoadTray() --> Action_BeforeLoadTray() + IsLotEnd() -|
    ///     |-- No, Not LotEnd, but can load tray    -NEXT--> ActionReset_LoadTray() --> Action_LoadTray() --> (o)
    ///     |-- No, Not LotEnd and can NOT load tray -IDLE--! (Back to Action_BeforeLoadTray() + IsLotEnd())
    ///     |-- Yes, it's LotEnd                     -CASE1-> LotEnd().
    /// 其他功能：
    /// 1. 收盤：ActionReset_UnloadTray() --> Action_UnloadTray().
    /// </summary>
    public class VtmLoader : VTrayStacker
    {
        #region 程序變數

        private JActionTask Task_Home = new JActionTask();
        private JActionTask Task_BeforeLoadTray = new JActionTask();    //v1.3 - Jay Cao Added
        private JActionTask Task_LoadTray = new JActionTask();
        private JActionTask Task_UnloadTray = new JActionTask();
        private bool IsTrayReady = false;   //v1.3 - Jay Cao Added : 判斷 Tray 是否已載入在軌道上等待

        #endregion 程序變數

        #region 元件

        //Input
        private DigitalInput DI_TrayInSeparatePos = null;      //入料區盤分離位置有無盤偵測(B接點)

        //Output
        private Cylinder CY_TraySeparate = null;           //入料區盤分離氣缸

        //Motor
        private Motor MT_LoadTrayZ = null;               //入料區接盤馬達

        //Timer
        private MyTimer OverTime_Home = new MyTimer(true);

        private MyTimer OverTime_LoadTray = new MyTimer(true);
        private MyTimer OverTime_UnloadTray = new MyTimer(true);

        #endregion 元件

        /// <summary>
        /// Initializes a new instance of the <see cref="VtmLoader" /> class.
        /// </summary>
        /// <param name="ibTrayFull">入料區滿盤偵測</param>
        /// <param name="ibTrayExist">入料區有無盤偵測</param>
        /// <param name="ibTrayInSeparatePos">The ib tray in separate position.</param>
        /// <param name="ibTrayOnTrack">入料區軌道有無盤偵測</param>
        /// <param name="ibTrayInPosition">入料區軌道盤定位偵測</param>
        /// <param name="obTraySeparate">入料區盤分離氣缸</param>
        /// <param name="moLoadTrayZ">入料區接盤馬達</param>
        public VtmLoader(
            AlarmCallback alarm,
            InBit ibTrayFull, InBit ibTrayExist, InBit ibTrayInSeparatePos,
            InBit ibTrayOnTrack, InBit ibTrayInPosition1, InBit ibTrayInPosition2,
            OutBit obTraySeparate, Motor moLoadTrayZ)
            : base(alarm)
        {
            DI_TrayFull = new DigitalInput(ibTrayFull);
            DI_TrayExist = new DigitalInput(ibTrayExist);
            DI_TrayInSeparatePos = new DigitalInput(ibTrayInSeparatePos);
            DI_TrayOnTrack = new DigitalInput(ibTrayOnTrack);
            DI_TrayInPosition1 = new DigitalInput(ibTrayInPosition1);
            DI_TrayInPosition2 = new DigitalInput(ibTrayInPosition2);
            CY_TraySeparate = new Cylinder(obTraySeparate, null, null);
            MT_LoadTrayZ = moLoadTrayZ;

            //設定計時器自動重置
            OverTime_Home.AutoReset = true;
            OverTime_LoadTray.AutoReset = true;
            OverTime_UnloadTray.AutoReset = true;
        }

        ~VtmLoader()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (DI_TrayFull != null) DI_TrayFull.Dispose();
            if (DI_TrayExist != null) DI_TrayExist.Dispose();
            if (DI_TrayInSeparatePos != null) DI_TrayInSeparatePos.Dispose();
            if (DI_TrayOnTrack != null) DI_TrayOnTrack.Dispose();
            if (DI_TrayInPosition1 != null) DI_TrayInPosition1.Dispose();
            if (DI_TrayInPosition2 != null) DI_TrayInPosition2.Dispose();
            if (CY_TraySeparate != null) CY_TraySeparate.Dispose();
        }

        protected override void SimulationSetting2(bool simulation)
        {
            //覆寫父類別虛擬函式內容
            DI_TrayInSeparatePos.Simulation = simulation;
            CY_TraySeparate.Simulation = simulation;
        }

        #region 私有function

        /// <summary>
        /// 盤分離氣缸作動
        /// </summary>
        /// <param name="val">if set to <c>true</c> [盤分離氣缸開啟].</param>
        /// <param name="delay_ms">盤分離氣缸作動後的延遲時間</param>
        /// <param name="timeout_ms">等待盤分離氣缸作動的逾時時間</param>
        /// <returns></returns>
        private ThreeValued TraySeparateCtrl(bool val, int delay_ms, int timeout_ms)
        {
            ThreeValued tRet = ThreeValued.UNKNOWN;
            if (CY_TraySeparate != null)
            {
                if (val)
                {
                    tRet = CY_TraySeparate.On(delay_ms, timeout_ms);
                }
                else
                {
                    tRet = CY_TraySeparate.Off(delay_ms, timeout_ms);
                }
                if (tRet == ThreeValued.FALSE)
                {
                    if (val)
                    {
                        //Alarm : 入料區盤分離氣缸開啟動作逾時
                        this.ShowAlarm((int)VTMAlarmCode.TrayLoaderSeparateTrayCylinderOpenTimeout);
                    }
                    else
                    {
                        //Alarm : 入料區盤分離氣缸關閉動作逾時
                        this.ShowAlarm((int)VTMAlarmCode.TrayLoaderSeparateTrayCylinderCloseTimeout);
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
        /// 歸零流程重置
        /// </summary>
        /// <returns></returns>
        public void Action_ResetHome()
        {
            Var_Locker = null;
            IsTrayReady = false;    //v1.3 - Jay Cao Added
            Task_Home.Reset();
            MT_LoadTrayZ.HomeReset();
            OverTime_Home.Restart();
        }

        /// <summary>
        /// 歸零流程 ( 呼叫 Action_Home() 函式前，務必先呼叫 ActionReset_Home() )
        /// </summary>
        /// <returns></returns>
        public ThreeValued Action_Home()
        {
            //檢查動作總時間是否超過 30 秒
            if (OverTime_Home.On(30000))
            {
                OverTime_Home.Restart();
                //Alarm : 入料區歸零動作動作逾時
                this.ShowAlarm((int)VTMAlarmCode.TrayLoaderHomeTimeout);
                return ThreeValued.FALSE;
            }

            ThreeValued tRet = ThreeValued.UNKNOWN;
            JActionTask Task = Task_Home;
            switch (Task.Value)
            {
                case 0:     //盤分離氣缸關閉
                    {
                        ThreeValued tRetTemp = TraySeparateCtrl(false, 300, 3000);
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

                case 10:     //接盤馬達歸零
                    {
                        if (MT_LoadTrayZ.Home())
                        {
                            Task.Next(20);
                        }
                    }
                    break;

                case 20:     //檢查軌道上是否有盤殘留(有盤Alarm)
                    {
                        if (this.DryRun)
                        {
                            Task.Next(30);
                        }
                        else
                        {
                            bool b1 = TrayInPosition(); //判斷軌道盤位置是否良好
                            bool b2 = NothingOnTrack(); //判斷軌道是否無盤
                            if (b1 && b2)
                            {
                                Task.Next(30);
                            }
                            else
                            {
                                //Alarm : 入料區軌道上有盤殘留
                                this.ShowAlarm((int)VTMAlarmCode.TrayLoaderTrackTrayRemain);
                                tRet = ThreeValued.FALSE;
                            }
                        }
                    }
                    break;

                case 30:     //檢查入料區有無盤(有盤Alarm)
                    {
                        if (!CheckInitialTrayExist || TrayNonExist())
                        {
                            Task.Next(1000);
                        }
                        else
                        {
                            //Alarm : 入料區有盤殘留
                            this.ShowAlarm((int)VTMAlarmCode.TrayLoaderTrayRemain);
                            tRet = ThreeValued.FALSE;
                        }
                    }
                    break;

                case 1000:    //歸零完成
                    {
                        tRet = ThreeValued.TRUE;
                    }
                    break;
            }
            return tRet;
        }

        /// <summary>
        /// 判斷 Tray 是否已載入完成等待台車取盤
        /// v1.3 - Jay Cao Added
        /// ！！注意！！
        /// ！！僅供 VtmTrolley - Action_BeforeTakeTray() 使用！！
        /// ！！注意！！
        /// </summary>
        /// <param name="locker">要取盤的台車物件</param>
        /// <returns>
        /// true : Tray 已載入等待台車取盤
        /// false : Tray 未載入或已有其他台車在取盤
        /// </returns>
        internal bool IsTrayReadyToTake(VtmTrolley locker)
        {
            //v1.3.1 - Jay Cao Added "(locker != null)"
            if ((locker != null) && (Var_Locker == null) && IsTrayReady)
            {
                if (this.Lock(locker))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 通知 Tray 已被台車取走
        /// v1.3 - Jay Cao Added
        /// ！！注意！！
        /// ！！僅供 VtmTrolley - Goto() 使用！！
        /// ！！注意！！
        /// </summary>
        /// <param name="locker">已取盤的台車物件</param>
        internal void TrayHasBeenTaken(VtmTrolley locker)
        {
            //v1.3.1 - Jay Cao Added "(locker != null) && IsTrayReady"
            if ((locker != null) && (Var_Locker == locker) && IsTrayReady)
            {
                IsTrayReady = false;
                Var_Locker = null;  //force unlock tray loader
            }
        }

        /// <summary>
        /// 載盤前置流程(檢查動作)重置
        /// v1.3 - Jay Cao Added
        /// </summary>
        public void ActionReset_BeforeLoadTray()
        {
            Task_BeforeLoadTray.Reset();
        }

        /// <summary>
        /// 載盤前置流程(檢查動作)
        /// v1.3 - Jay Cao Added
        /// </summary>
        /// <returns>
        /// UNKNOWN : 流程進行中
        /// TRUE : 流程完成
        /// FALSE : 流程失敗(異常)
        /// </returns>
        public ThreeValued Action_BeforeLoadTray()
        {
            ThreeValued tRet = ThreeValued.UNKNOWN;
            JActionTask Task = Task_BeforeLoadTray;
            switch (Task.Value)
            {
                case 0: //判斷軌道 Tray 是否已被取走
                    {
                        if (!IsTrayReady)
                        {
                            Task.Next(10);
                        }
                    }
                    break;

                case 10:     //盤分離氣缸關閉
                    {
                        ThreeValued tRetTemp = TraySeparateCtrl(false, 300, 3000);
                        if (tRetTemp == ThreeValued.TRUE)
                        {
                            Task.Next(20);
                        }
                        else
                        {
                            tRet = tRetTemp;
                        }
                    }
                    break;

                case 20:     //判斷入料區是否滿盤(不滿盤下一步)
                    {
                        if (IsFullTray().Equals(false))
                        {
                            Task.Next(30);
                        }
                        else
                        {
                            //Alarm : 入料區滿盤
                            this.ShowAlarm((int)VTMAlarmCode.TrayLoaderTrayFull);
                            tRet = ThreeValued.FALSE;
                        }
                    }
                    break;

                case 30:     //判斷軌道區盤位置是否良好(良好下一步) - 利用此函示判斷軌道是否有盤殘留
                    {
                        bool b1 = TrayInPosition();
                        bool b2 = NothingOnTrack();
                        if (this.DryRun || (b1 && b2))
                        {
                            Task.Next(40);
                        }
                        else
                        {
                            //Alarm : 入料區軌道上有盤殘留
                            this.ShowAlarm((int)VTMAlarmCode.TrayLoaderTrackTrayRemain);
                            tRet = ThreeValued.FALSE;
                        }
                    }
                    break;

                case 40:     //判斷入料區有無盤(有盤下一步)
                    {
                        if (TrayExist())
                        {
                            Task.Next(100);
                        }
                        else
                        {
                            //Alarm : 入料區無盤
                            this.ShowAlarm((int)VTMAlarmCode.TrayLoaderTrayEmpty);
                            tRet = ThreeValued.FALSE;
                        }
                    }
                    break;

                case 100:  //完成
                    {
                        tRet = ThreeValued.TRUE;
                    }
                    break;
            }
            return tRet;
        }

        /// <summary>
        /// 載盤流程重置
        /// v1.3 - Jay Cao Added
        /// </summary>
        public void ActionReset_LoadTray()
        {
            Task_LoadTray.Reset();
            OverTime_LoadTray.Restart();
        }

        /// <summary>
        /// 載盤流程
        /// v1.3 - Jay Cao Added
        /// </summary>
        /// <param name="posLoadTrayZ">接盤高度</param>
        /// <param name="posSepTrayZ">盤分離高度</param>
        /// <param name="posSepTrayDetectZ">盤分離後偵測有無黏盤高度</param>
        /// <param name="posTrayOnTrackZ">軌道盤高度</param>
        /// <param name="posWaiting">安全等待高度</param>
        /// <param name="Thickness">盤厚度</param>
        /// <returns>
        /// UNKNOWN : 流程進行中
        /// TRUE : 流程完成
        /// FALSE : 流程失敗(異常)
        /// </returns>
        public ThreeValued Action_LoadTray(int posLoadTrayZ, int posSepTrayZ, int posSepTrayDetectZ,
            int posTrayOnTrackZ, int posWaiting, int Thickness)
        {
            //初步檢查輸入的參數值是否異常
            if (!Simulation && !((posLoadTrayZ > posSepTrayZ) && (posSepTrayZ > posSepTrayDetectZ) &&
                (posSepTrayDetectZ > posTrayOnTrackZ) && (posTrayOnTrackZ >= posWaiting)))
            {
                //Alarm : 入料區點位資料設定異常
                this.ShowAlarm((int)VTMAlarmCode.TrayLoaderPositionDataError);
                return ThreeValued.FALSE;      //資料設定錯誤
            }

            //檢查動作總時間是否超過 90 秒
            if (OverTime_LoadTray.On(90000))
            {
                OverTime_LoadTray.Restart();
                //Alarm : 入料區載盤流程動作逾時
                this.ShowAlarm((int)VTMAlarmCode.TrayLoaderLoadTrayTimeout);
                return ThreeValued.FALSE;      //動作逾時
            }

            ThreeValued tRet = ThreeValued.UNKNOWN;
            JActionTask Task = Task_LoadTray;
            switch (Task.Value)
            {
                case 0:     //接盤馬達至盤分離高度(兩段式移動，先移動至盤分離高度，在移動至接盤高度，避免因速度過快導致跳料)
                    {
                        if (MT_LoadTrayZ.G00(posSepTrayZ))
                        {
                            Task.Next(10);
                        }
                    }
                    break;

                case 10:     //接盤馬達至接盤高度
                    {
                        if (MT_LoadTrayZ.G00(posLoadTrayZ))
                        {
                            Task.Next(20);
                        }
                    }
                    break;

                case 20:     //盤分離氣缸開啟
                    {
                        ThreeValued tRetTemp = TraySeparateCtrl(true, 300, 3000);
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

                case 30:     //接盤馬達下降至盤分離高度
                    {
                        if (MT_LoadTrayZ.G00(posLoadTrayZ - (Thickness)))
                        {
                            Task.Next(40);
                        }
                    }
                    break;

                case 40:     //盤分離氣缸關閉
                    {
                        ThreeValued tRetTemp = TraySeparateCtrl(false, 300, 3000);
                        if (tRetTemp == ThreeValued.TRUE)
                        {
                            Task.Next(50);
                        }
                        else
                        {
                            tRet = tRetTemp;
                        }
                    }
                    break;

                case 50:    //接盤馬達至盤分離後偵測有無黏盤高度
                    {
                        if (MT_LoadTrayZ.G00(posLoadTrayZ - (int)(Thickness * 2.3)))
                        {
                            Task.Next(60);
                        }
                    }
                    break;

                case 60:     //判斷有無黏盤
                    {
                        if (this.DryRun)
                        {
                            Task.Next(70);
                        }
                        else
                        {
                            if (DI_TrayInSeparatePos.ValueOn)
                            {
                                Task.Next(70);
                            }
                            else
                            {
                                //Alarm : 入料區盤分離異常，盤未確實分離
                                this.ShowAlarm((int)VTMAlarmCode.TrayLoaderTraySeparateFailure);
                                tRet = ThreeValued.FALSE;
                            }
                        }
                    }
                    break;

                case 70:    //接盤馬達至軌道放盤高度(兩段式下降，故意將放盤高度增加 1mm，再移動至安全高度，避免因速度過快造成跳料)
                    {
                        if (MT_LoadTrayZ.G00(posTrayOnTrackZ + 1000))
                        {
                            Task.Next(80);
                        }
                    }
                    break;

                case 80:     //接盤馬達至安全等待高度
                    {
                        if (MT_LoadTrayZ.G00(posWaiting))
                        {
                            Task.Next(90);
                        }
                    }
                    break;

                case 90:    //檢查盤位置是否異常
                    {
                        if (this.DryRun)
                        {
                            Task.Next(1000);
                        }
                        else
                        {
                            bool b1 = TrayInPosition();
                            bool b2 = TrayOnTrack();
                            if (b1 && b2)
                            {
                                Task.Next(1000);
                            }
                            else
                            {
                                //Alarm : 入料區軌道盤位置不良
                                this.ShowAlarm((int)VTMAlarmCode.TrayLoaderTrackTrayPositionError);
                                tRet = ThreeValued.FALSE;
                            }
                        }
                    }
                    break;

                case 1000:  //完成
                    {
                        IsTrayReady = true;     //v1.3 - Jay Cao Added
                        tRet = ThreeValued.TRUE;
                    }
                    break;
            }
            return tRet;
        }

        /// <summary>
        /// 【已停用】載盤流程重置
        /// </summary>
        /// <returns></returns>
        //public bool Action_ResetLoadTray()
        //{
        //    //if (!Var_Locker.Equals(locker))
        //    //{
        //    //    //Alarm : 入料區已被其他台車鎖定
        //    //    this.ShowAlarm((int)VTMAlarmCode.TrayLoaderLocked);
        //    //    return false;   //不合法的使用者
        //    //}

        //    Task_LoadTray.Reset();
        //    OverTime_LoadTray.Restart();
        //    return true;
        //}

        /// <summary>
        /// 【已停用】載盤流程 ( 呼叫 Action_LoadTray() 函式前，務必先呼叫 Action_ResetLoadTray() )
        /// </summary>
        /// <param name="posLoadTrayZ">接盤高度</param>
        /// <param name="posSepTrayZ">盤分離高度</param>
        /// <param name="posSepTrayDetectZ">盤分離後偵測有無黏盤高度</param>
        /// <param name="posTrayOnTrackZ">軌道盤高度</param>
        /// <param name="posWaiting">安全等待高度</param>
        /// <returns></returns>
        //public ThreeValued Action_LoadTray(int posLoadTrayZ, int posSepTrayZ,
        //    int posSepTrayDetectZ, int posTrayOnTrackZ, int posWaiting, int Thickness)
        //{
        //    //if (!Var_Locker.Equals(locker))
        //    //{
        //    //    //Alarm : 入料區已被其他台車鎖定
        //    //    this.ShowAlarm((int)VTMAlarmCode.TrayLoaderLocked);
        //    //    return ThreeValued.FALSE;     //不合法的使用者
        //    //}

        //    //初步檢查輸入的參數值是否異常
        //    if (!Simulation && !((posLoadTrayZ > posSepTrayZ) && (posSepTrayZ > posSepTrayDetectZ) &&
        //        (posSepTrayDetectZ > posTrayOnTrackZ) && (posTrayOnTrackZ >= posWaiting)))
        //    {
        //        //Alarm : 入料區點位資料設定異常
        //        this.ShowAlarm((int)VTMAlarmCode.TrayLoaderPositionDataError);
        //        return ThreeValued.FALSE;      //資料設定錯誤
        //    }

        //    //檢查動作總時間是否超過 90 秒
        //    if (OverTime_LoadTray.On(90000))
        //    {
        //        OverTime_LoadTray.Restart();
        //        //Alarm : 入料區載盤流程動作逾時
        //        this.ShowAlarm((int)VTMAlarmCode.TrayLoaderLoadTrayTimeout);
        //        return ThreeValued.FALSE;      //動作逾時
        //    }

        //    ThreeValued tRet = ThreeValued.UNKNOWN;
        //    JActionTask Task = Task_LoadTray;
        //    switch (Task.Value)
        //    {
        //        case 0:     //盤分離氣缸關閉
        //            {
        //                ThreeValued tRetTemp = TraySeparateCtrl(false, 300, 3000);
        //                if (tRetTemp == ThreeValued.TRUE)
        //                {
        //                    Task.Next(10);
        //                }
        //                else
        //                {
        //                    tRet = tRetTemp;
        //                }
        //            }
        //            break;

        //        case 10:     //判斷入料區是否滿盤(不滿盤下一步)
        //            {
        //                if (IsFullTray().Equals(false))
        //                {
        //                    Task.Next(20);
        //                }
        //                else
        //                {
        //                    //Alarm : 入料區滿盤
        //                    this.ShowAlarm((int)VTMAlarmCode.TrayLoaderTrayFull);
        //                    tRet = ThreeValued.FALSE;
        //                }
        //            }
        //            break;

        //        case 20:     //判斷軌道區盤位置是否良好(良好下一步) - 利用此函示判斷軌道是否有盤殘留
        //            {
        //                if (this.DryRun)
        //                {
        //                    Task.Next(30);
        //                }
        //                else
        //                {
        //                    bool b1 = TrayInPosition();
        //                    bool b2 = NothingOnTrack();
        //                    if (b1 && b2)
        //                    {
        //                        Task.Next(30);
        //                    }
        //                    else
        //                    {
        //                        //Alarm : 入料區軌道上有盤殘留
        //                        this.ShowAlarm((int)VTMAlarmCode.TrayLoaderTrackTrayRemain);
        //                        tRet = ThreeValued.FALSE;
        //                    }
        //                }
        //            }
        //            break;

        //        case 30:     //判斷入料區有無盤(有盤下一步)
        //            {
        //                if (TrayExist())
        //                {
        //                    Task.Next(40);
        //                }
        //                else
        //                {
        //                    //Alarm : 入料區無盤
        //                    this.ShowAlarm((int)VTMAlarmCode.TrayLoaderTrayEmpty);
        //                    tRet = ThreeValued.FALSE;
        //                }
        //            }
        //            break;

        //        case 40:     //接盤馬達至盤分離高度(兩段式移動，先移動至盤分離高度，在移動至接盤高度，避免因速度過快導致跳料)
        //            {
        //                if (MT_LoadTrayZ.G00(posSepTrayZ))
        //                {
        //                    Task.Next(50);
        //                }
        //            }
        //            break;

        //        case 50:     //接盤馬達至接盤高度
        //            {
        //                if (MT_LoadTrayZ.G00(posLoadTrayZ))
        //                {
        //                    Task.Next(60);
        //                }
        //            }
        //            break;

        //        case 60:     //盤分離氣缸開啟
        //            {
        //                ThreeValued tRetTemp = TraySeparateCtrl(true, 300, 3000);
        //                if (tRetTemp == ThreeValued.TRUE)
        //                {
        //                    Task.Next(70);
        //                }
        //                else
        //                {
        //                    tRet = tRetTemp;
        //                }
        //            }
        //            break;

        //        case 70:     //接盤馬達下降至盤分離高度
        //            {
        //                if (MT_LoadTrayZ.G00(posLoadTrayZ - (Thickness)))
        //                {
        //                    Task.Next(80);
        //                }
        //            }
        //            break;

        //        case 80:     //盤分離氣缸關閉
        //            {
        //                ThreeValued tRetTemp = TraySeparateCtrl(false, 300, 3000);
        //                if (tRetTemp == ThreeValued.TRUE)
        //                {
        //                    Task.Next(90);
        //                }
        //                else
        //                {
        //                    tRet = tRetTemp;
        //                }
        //            }
        //            break;

        //        case 90:    //接盤馬達至盤分離後偵測有無黏盤高度
        //            {
        //                if (MT_LoadTrayZ.G00(posLoadTrayZ - (int)(Thickness * 2.3)))
        //                {
        //                    Task.Next(100);
        //                }
        //            }
        //            break;

        //        case 100:     //判斷有無黏盤
        //            {
        //                if (this.DryRun)
        //                {
        //                    Task.Next(110);
        //                }
        //                else
        //                {
        //                    if (DI_TrayInSeparatePos.ValueOn)
        //                    {
        //                        Task.Next(110);
        //                    }
        //                    else
        //                    {
        //                        //Alarm : 入料區盤分離異常，盤未確實分離
        //                        this.ShowAlarm((int)VTMAlarmCode.TrayLoaderTraySeparateFailure);
        //                        tRet = ThreeValued.FALSE;
        //                    }
        //                }
        //            }
        //            break;

        //        case 110:    //接盤馬達至軌道放盤高度(兩段式下降，故意將放盤高度增加 1mm，再移動至安全高度，避免因速度過快造成跳料)
        //            {
        //                if (MT_LoadTrayZ.G00(posTrayOnTrackZ + 1000))
        //                {
        //                    Task.Next(120);
        //                }
        //            }
        //            break;

        //        case 120:     //接盤馬達至安全等待高度
        //            {
        //                if (MT_LoadTrayZ.G00(posWaiting))
        //                {
        //                    Task.Next(130);
        //                }
        //            }
        //            break;

        //        case 130:    //檢查盤位置是否異常
        //            {
        //                if (this.DryRun)
        //                {
        //                    Task.Next(1000);
        //                }
        //                else
        //                {
        //                    bool b1 = TrayInPosition();
        //                    bool b2 = TrayOnTrack();
        //                    if (b1 && b2)
        //                    {
        //                        Task.Next(1000);
        //                    }
        //                    else
        //                    {
        //                        //Alarm : 入料區軌道盤位置不良
        //                        this.ShowAlarm((int)VTMAlarmCode.TrayLoaderTrackTrayPositionError);
        //                        tRet = ThreeValued.FALSE;
        //                    }
        //                }
        //            }
        //            break;

        //        case 1000:  //完成
        //            {
        //                tRet = ThreeValued.TRUE;
        //            }
        //            break;
        //    }
        //    return tRet;
        //}

        /// <summary>
        /// 收盤流程重置
        /// </summary>
        /// <returns></returns>
        public bool ActionReset_UnloadTray()
        {
            //if (!Var_Locker.Equals(locker))
            //{
            //    //Alarm : 入料區已被其他台車鎖定
            //    this.ShowAlarm((int)VTMAlarmCode.TrayLoaderLocked);
            //    return false;     //不合法的使用者
            //}

            Task_UnloadTray.Reset();
            OverTime_UnloadTray.Restart();
            return true;
        }

        /// <summary>
        /// 收盤流程 ( 呼叫 Action_LoadTray() 函式前，務必先呼叫 Action_ResetLoadTray() )
        /// </summary>
        /// <param name="posLoadTrayZ">接盤高度</param>
        /// <param name="posSepTrayZ">盤分離高度</param>
        /// <param name="posSepTrayDetectZ">盤分離後偵測有無黏盤高度</param>
        /// <param name="posTrayOnTrackZ">軌道盤高度</param>
        /// <param name="posWaiting">安全等待高度</param>
        /// <returns></returns>
        public ThreeValued Action_UnloadTray(int posLoadTrayZ, int posSepTrayZ,
            int posWaiting)
        {
            //if (!Var_Locker.Equals(locker))
            //{
            //    //Alarm : 入料區已被其他台車鎖定
            //    this.ShowAlarm((int)VTMAlarmCode.TrayLoaderLocked);
            //    return ThreeValued.FALSE;     //不合法的使用者
            //}

            //初步檢查輸入的參數值是否異常
            if (!Simulation && !((posLoadTrayZ > posSepTrayZ) && (posSepTrayZ > posWaiting)))
            {
                //Alarm : 入料區點位資料設定異常
                this.ShowAlarm((int)VTMAlarmCode.TrayLoaderPositionDataError);
                return ThreeValued.FALSE;      //資料設定錯誤
            }

            //檢查動作總時間是否超過 90 秒
            if (OverTime_UnloadTray.On(90000))
            {
                OverTime_UnloadTray.Restart();
                //Alarm : 入料區收盤流程動作逾時
                this.ShowAlarm((int)VTMAlarmCode.TrayLoaderUnloadTrayTimeout);
                return ThreeValued.FALSE;      //動作逾時
            }

            ThreeValued tRet = ThreeValued.UNKNOWN;
            JActionTask Task = Task_UnloadTray;
            switch (Task.Value)
            {
                case 0:     //盤分離氣缸關閉
                    {
                        ThreeValued tRetTemp = TraySeparateCtrl(false, 300, 3000);
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

                case 10:     //接盤馬達至安全等待高度
                    {
                        if (MT_LoadTrayZ.G00(posWaiting))
                        {
                            Task.Next(20);
                        }
                    }
                    break;

                case 20:    //檢查盤位置是否異常
                    {
                        if (this.DryRun)
                        {
                            Task.Next(30);
                        }
                        else
                        {
                            bool b1 = TrayInPosition();
                            bool b2 = TrayOnTrack();
                            if (b1 && b2)
                            {
                                Task.Next(30);
                            }
                            else
                            {
                                //Alarm : 入料區軌道盤位置不良
                                this.ShowAlarm((int)VTMAlarmCode.TrayLoaderTrackTrayPositionError);
                                tRet = ThreeValued.FALSE;
                            }
                        }
                    }
                    break;

                case 30:     //接盤馬達至盤分離高度
                    {
                        if (MT_LoadTrayZ.G00(posSepTrayZ))
                        {
                            Task.Next(40);
                        }
                    }
                    break;

                case 40:     //盤分離氣缸開啟
                    {
                        ThreeValued tRetTemp = TraySeparateCtrl(true, 300, 3000);
                        if (tRetTemp == ThreeValued.TRUE)
                        {
                            Task.Next(50);
                        }
                        else
                        {
                            tRet = tRetTemp;
                        }
                    }
                    break;

                case 50:     //接盤馬達至接盤高度
                    {
                        if (MT_LoadTrayZ.G00(posLoadTrayZ))
                        {
                            Task.Next(60);
                        }
                    }
                    break;

                case 60:     //盤分離氣缸關閉
                    {
                        ThreeValued tRetTemp = TraySeparateCtrl(false, 300, 3000);
                        if (tRetTemp == ThreeValued.TRUE)
                        {
                            Task.Next(70);
                        }
                        else
                        {
                            tRet = tRetTemp;
                        }
                    }
                    break;

                case 70:     //接盤馬達至安全等待高度
                    {
                        if (MT_LoadTrayZ.G00(posWaiting))
                        {
                            Task.Next(80);
                        }
                    }
                    break;

                case 80:     //判斷入料區是否滿盤(不滿盤下一步)
                    {
                        if (IsFullTray().Equals(false))
                        {
                            Task.Next(1000);
                        }
                        else
                        {
                            //Alarm : 入料區滿盤
                            this.ShowAlarm((int)VTMAlarmCode.TrayLoaderTrayFull);
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