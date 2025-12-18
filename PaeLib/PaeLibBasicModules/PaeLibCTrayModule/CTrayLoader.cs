using PaeLibGeneral;
using PaeLibProVSDKEx;
using ProVLib;
using System;

//public enum CTRAYLOADER //TrayLoader_使用Alarm範圍21~30
//{
//    crER_CTM_TraySeparateTimeout = 21,  //載盤區盤分離氣缸動作逾時 {0}
//    crER_CTM_TrayRemain = 22,           //載盤區盤殘留 {0}
//    crER_CTM_LockedByTrolley = 23,      //載盤區已被台車鎖定 {0}
//    crER_CTM_TrollryIDAssignError = 24, //台車編號指定錯誤 {0}
//    crER_CTM_TrayFull = 25,             //載盤區盤堆疊高度過高 {0}
//    crER_CTM_TrayEmpty = 26,            //載盤區無盤 {0}
//    crER_CTM_TrayNonExist = 27,         //載盤區台車盤未至定位 {0}
//    crER_CTM_TrayInWrongPosition = 28,  //載盤區台車盤定位不良 {0}
//};

namespace PaeLibCTrayModule
{
    /// <summary>
    /// 十字台車載盤區模組類別
    /// </summary>
    /// <seealso cref="ComponentSet.CTrayStacker" />
    public class CTrayLoader : CTrayStacker, IDisposable
    {
        ///// <summary>
        ///// The module alarm callback
        ///// </summary>
        //private AlarmCallback ModuleAlarmCallback = null;

        //#region 參數設定

        //private bool Para_DryRun = false;//空跑
        //private bool Para_Simulation = false;//模擬跑

        //#endregion 參數設定

        #region 程序變數

        private JActionTask Task_Home = new JActionTask();
        private JActionTask Task_LoadTray = new JActionTask();
        private JActionTask Task_UnloadTray = new JActionTask();

        #endregion 程序變數

        #region 點位變數

        private int Pos_LoadTrayZ;              //接盤馬達Z軸 - 接盤高度
        private int Pos_SepTrayZ;               //接盤馬達Z軸 - 盤分離高度
        private int Pos_TrayOnTopTrolleyZ;      //接盤馬達Z軸 - 上台車盤高度
        private int Pos_TrayOnBotTrolleyZ;      //接盤馬達Z軸 - 下台車盤高度
        private int Pos_WaitingZ;               //接盤馬達Z軸 - 安全等待高度

        #endregion 點位變數

        #region 元件

        //Output
        private Cylinder CY_TraySeparate = null;           //盤分離氣缸

        //Motor
        private Motor MT_LoadTrayZ = null;               //接盤馬達Z軸

        #endregion 元件

        /// <summary>
        /// Initializes a new instance of the <see cref="CTrayLoader" /> class.
        /// </summary>
        //public CTrayLoader()
        //{
        //    ModuleAlarmCallback = null;
        //    DI_StackerTrayFull = new JDigitalInput();
        //    DI_StackerTrayExist = new JDigitalInput();
        //    DI_TopTrolleyTrayExist = new JDigitalInput();
        //    DI_TopTrolleyTrayInPositionV = new JDigitalInput();
        //    DI_TopTrolleyTrayInPositionH = new JDigitalInput();
        //    DI_BottomTrolleyTrayExist = new JDigitalInput();
        //    DI_BottomTrolleyTrayInPositionV = new JDigitalInput();
        //    DI_BottomTrolleyTrayInPositionH = new JDigitalInput();
        //    CY_TraySeparate = new JCylinder();
        //    MT_LoadTrayZ = new Motor();
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="CTrayLoader" /> class.
        /// </summary>
        /// <param name="name">此物件名稱</param>
        /// <param name="alarm">模組 Alarm 的回呼函式</param>
        /// <param name="ibTrayFullV">盤堆疊區滿盤偵測-直</param>
        /// <param name="ibTrayFullH">盤堆疊區滿盤偵測-橫</param>
        /// <param name="ibTrayExistV">盤堆疊區有無盤偵測-直</param>
        /// <param name="ibTrayExistH">盤堆疊區有無盤偵測-橫</param>
        /// <param name="ibStackerTrayInPosX1">盤分離區盤定位偵測-X1</param>
        /// <param name="ibStackerTrayInPosX2">盤分離區盤定位偵測-X2</param>
        /// <param name="ibTopTrolleyTrayExist">上台車有無盤偵測</param>
        /// <param name="ibTopTrolleyTrayInPosX1">上台車盤定位偵測-X1</param>
        /// <param name="ibTopTrolleyTrayInPosX2">上台車盤定位偵測-X2</param>
        /// <param name="ibBotTrolleyTrayExist">下台車有無盤偵測</param>
        /// <param name="ibBotTrolleyTrayInPosX1">下台車盤定位偵測-X1</param>
        /// <param name="ibBotTrolleyTrayInPosX2">下台車盤定位偵測-X2</param>
        /// <param name="obTraySeparate">盤分離氣缸</param>
        /// <param name="mtLoadTrayZ">接盤馬達Z軸</param>
        public CTrayLoader(string name, AlarmCallback alarm,
            InBit ibTrayFullV, InBit ibTrayFullH, InBit ibTrayExistV, InBit ibTrayExistH,
            InBit ibStackerTrayInPosX1, InBit ibStackerTrayInPosX2,
            InBit ibTopTrolleyTrayExist, InBit ibTopTrolleyTrayInPosX1, InBit ibTopTrolleyTrayInPosX2,
            InBit ibBotTrolleyTrayExist, InBit ibBotTrolleyTrayInPosX1, InBit ibBotTrolleyTrayInPosX2,
            OutBit obTraySeparate, Motor mtLoadTrayZ)
            : base(name, alarm)
        {
            DI_StackerTrayFullV = new DigitalInput(ibTrayFullV);
            DI_StackerTrayFullH = new DigitalInput(ibTrayFullH);
            DI_StackerTrayExistV = new DigitalInput(ibTrayExistV);
            DI_StackerTrayExistH = new DigitalInput(ibTrayExistH);
            DI_StackerTrayInPositionX1 = new DigitalInput(ibStackerTrayInPosX1);
            DI_StackerTrayInPositionX2 = new DigitalInput(ibStackerTrayInPosX2);
            DI_TopShuttleTrayExist = new DigitalInput(ibTopTrolleyTrayExist);
            DI_TopShuttleTrayInPositionX1 = new DigitalInput(ibTopTrolleyTrayInPosX1);
            DI_TopShuttleTrayInPositionX2 = new DigitalInput(ibTopTrolleyTrayInPosX2);
            DI_BottomShuttleTrayExist = new DigitalInput(ibBotTrolleyTrayExist);
            DI_BottomShuttleTrayInPositionX1 = new DigitalInput(ibBotTrolleyTrayInPosX1);
            DI_BottomShuttleTrayInPositionX2 = new DigitalInput(ibBotTrolleyTrayInPosX2);
            CY_TraySeparate = new Cylinder(obTraySeparate, null, null);
            MT_LoadTrayZ = mtLoadTrayZ;
        }

        ~CTrayLoader()
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
            if (DI_TopShuttleTrayInPositionX2 != null) DI_TopShuttleTrayInPositionX2.Dispose();
            if (DI_BottomShuttleTrayExist != null) DI_BottomShuttleTrayExist.Dispose();
            if (DI_BottomShuttleTrayInPositionX1 != null) DI_BottomShuttleTrayInPositionX1.Dispose();
            if (DI_BottomShuttleTrayInPositionX2 != null) DI_BottomShuttleTrayInPositionX2.Dispose();
            if (CY_TraySeparate != null) CY_TraySeparate.Dispose();
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

        //        CY_TraySeparate.Simulation = Para_Simulation;
        //    }
        //}

        //接盤馬達Z軸 - 接盤高度
        public int LoadTrayZ
        {
            get { return Pos_LoadTrayZ; }
            set { Pos_LoadTrayZ = value; }
        }

        //接盤馬達Z軸 - 盤分離高度
        public int SepTrayZ
        {
            get { return Pos_SepTrayZ; }
            set { Pos_SepTrayZ = value; }
        }

        //接盤馬達Z軸 - 上台車盤高度
        public int TrayOnTopTrolleyZ
        {
            get { return Pos_TrayOnTopTrolleyZ; }
            set { Pos_TrayOnTopTrolleyZ = value; }
        }

        //接盤馬達Z軸 - 下台車盤高度
        public int TrayOnBotTrolleyZ
        {
            get { return Pos_TrayOnBotTrolleyZ; }
            set { Pos_TrayOnBotTrolleyZ = value; }
        }

        //接盤馬達Z軸 - 安全等待高度
        public int WaitingZ
        {
            get { return Pos_WaitingZ; }
            set { Pos_WaitingZ = value; }
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
            if (CY_TraySeparate!=null)
            CY_TraySeparate.Simulation = simulation;
        }

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
            if (val)
            {
                tRet = CY_TraySeparate.On(delay_ms, timeout_ms);
                if (tRet.Equals(ThreeValued.FALSE))
                {
                    //Alarm : {0} 盤分離氣缸打開動作逾時({1})
                    this.ShowAlarm((int)CTMAlarmCode.ER_TLU_TraySeparateCyOnTimeout);
                }
            }
            else
            {
                tRet = CY_TraySeparate.Off(delay_ms, timeout_ms);
                if (tRet.Equals(ThreeValued.FALSE))
                {
                    //Alarm : {0} 盤分離氣缸關閉動作逾時({1})
                    this.ShowAlarm((int)CTMAlarmCode.ER_TLU_TraySeparateCyOffTimeout);
                }
            }
            return tRet;
        }

        private bool LoadTrayZGoto(int pos)
        {
            if (MT_LoadTrayZ != null)
            {
                bool bRet = MT_LoadTrayZ.G00(pos);
                return bRet;
            }
            return true;
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
            if (MT_LoadTrayZ != null)
            {
                MT_LoadTrayZ.HomeReset();
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
                case 0:     //盤分離氣缸關閉
                    {
                        ThreeValued crRet = TraySeparateCtrl(false, 300, 3000);
                        if (crRet.Equals(ThreeValued.TRUE))
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TraySeparateCyOffTimeout);
                            Task.Next(10);
                        }
                    }
                    break;

                case 10:     //接盤馬達歸零
                    {
                        if (MT_LoadTrayZ == null)
                        {
                            Task.Next(20);
                        }
                        else if (MT_LoadTrayZ.Home())
                        {
                            Task.Next(20);
                        }
                    }
                    break;

                case 20:     //檢查盤堆疊區是否無盤(有盤Alarm)
                    {
                        if (StackerTrayNonExist())
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TrayRemain);
                            Task.Next(1000);
                        }
                        else
                        {
                            //Alarm : 載盤區盤殘留 {0}
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
        /// 載盤流程重置
        /// </summary>
        /// <param name="locker">鎖定者物件</param>
        /// <returns></returns>
        public bool Action_LoadTrayReset(object locker)
        {
            if (this.Lock(locker))
            {
                this.ClearAlarm((int)CTMAlarmCode.ER_TLU_AlreadyLocked);
                Task_LoadTray.Reset();
                return true;
            }
            //Alarm : 載盤區已被台車鎖定 {0}
            this.ShowAlarm((int)CTMAlarmCode.ER_TLU_AlreadyLocked);
            return false;
        }

        /// <summary>
        /// 載盤流程
        /// </summary>
        /// <param name="locker">鎖定者物件</param>
        /// <param name="tid">台車編號(用以判斷是哪一台車)</param>
        /// <returns></returns>
        public CTrayShuttleLoadUnloadResult Action_LoadTray(object locker, CTrayShuttleID tid)
        {
            if (!this.Lock(locker))
            {
                //Alarm : 載盤區已被台車鎖定 {0}
                this.ShowAlarm((int)CTMAlarmCode.ER_TLU_AlreadyLocked);
                return CTrayShuttleLoadUnloadResult.DATAERROR;
            }

            if (!tid.Equals(CTrayShuttleID.TopCTrayShuttle) && !tid.Equals(CTrayShuttleID.BottomCTrayShuttle))
            {
                //Alarm : 台車編號指定錯誤 {0}
                this.ShowAlarm((int)CTMAlarmCode.ER_TLU_AssignWrongShuttleID);
                return CTrayShuttleLoadUnloadResult.DATAERROR;
            }

            CTrayShuttleLoadUnloadResult cRet = CTrayShuttleLoadUnloadResult.WORKING;
            JActionTask Task = Task_LoadTray;
            switch (Task.Value)
            {
                case 0:     //盤分離氣缸關閉
                    {
                        ThreeValued crRet = TraySeparateCtrl(false, 300, 3000);
                        if (crRet.Equals(ThreeValued.TRUE))
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TraySeparateCyOffTimeout);
                            Task.Next(10);
                        }
                    }
                    break;

                case 10:   //接盤馬達下降至安全等待高度
                    {
                        if (LoadTrayZGoto(Pos_WaitingZ))
                        {
                            Task.Next(20);
                        }
                    }
                    break;

                case 20:    //判斷盤堆疊區是否未滿盤(滿盤則Alarm : 盤堆疊高度過高)
                    {
                        if (IsStackerNotFullTray())
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TrayFull);
                            Task.Next(30);
                        }
                        else
                        {
                            //Alarm : 載盤區盤堆疊高度過高 {0}
                            this.ShowAlarm((int)CTMAlarmCode.ER_TLU_TrayFull);
                        }
                    }
                    break;

                case 30:    //檢查台車上是否無盤(有盤則Alarm : 台車有盤殘留)
                    {
                        if (ShuttleTrayNonExist(tid))
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TrayRemainOnShuttle);
                            Task.Next(40);
                        }
                        else
                        {
                            //Alarm : 載盤區台車有盤殘留 {0}
                            this.ShowAlarm((int)CTMAlarmCode.ER_TLU_TrayRemainOnShuttle);
                        }
                    }
                    break;

                case 40:    //判斷盤堆疊區是否有盤(無盤則Alarm)
                    {
                        if (StackerTrayExist())
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TrayEmpty);
                            Task.Next(50);
                        }
                        else
                        {
                            //Alarm : 載盤區無盤 {0}
                            cRet = CTrayShuttleLoadUnloadResult.TRAYNONEXIST;
                            this.ShowAlarm((int)CTMAlarmCode.ER_TLU_TrayEmpty);
                        }
                    }
                    break;

                case 50:    //接盤馬達至盤分離高度
                    {
                        if (LoadTrayZGoto(Pos_SepTrayZ))
                        {
                            Task.Next(60);
                        }
                    }
                    break;

                case 60:    //接盤馬達至接盤高度
                    {
                        if (LoadTrayZGoto(Pos_LoadTrayZ))
                        {
                            Task.Next(70);
                        }
                    }
                    break;

                case 70:    //盤分離氣缸開啟
                    {
                        ThreeValued crRet = TraySeparateCtrl(true, 300, 3000);
                        if (crRet.Equals(ThreeValued.TRUE))
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TraySeparateCyOnTimeout);
                            Task.Next(80);
                        }
                    }
                    break;

                case 80:    //接盤馬達下降至盤分離高度
                    {
                        if (LoadTrayZGoto(Pos_SepTrayZ))
                        {
                            Task.Next(90);
                        }
                    }
                    break;

                case 90:    //盤分離氣缸關閉
                    {
                        ThreeValued crRet = TraySeparateCtrl(false, 300, 3000);
                        if (crRet.Equals(ThreeValued.TRUE))
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TraySeparateCyOffTimeout);
                            Task.Next(100);
                        }
                    }
                    break;

                case 100:    //接盤馬達下降至盤台車高度
                    {
                        int Pos_TrayOnTrolleyZ = Pos_WaitingZ;
                        switch (tid)
                        {
                            case CTrayShuttleID.TopCTrayShuttle:
                                Pos_TrayOnTrolleyZ = Pos_TrayOnTopTrolleyZ;
                                break;

                            case CTrayShuttleID.BottomCTrayShuttle:
                                Pos_TrayOnTrolleyZ = Pos_TrayOnBotTrolleyZ;
                                break;
                        }
                        if (LoadTrayZGoto(Pos_TrayOnTrolleyZ))
                        {
                            Task.Next(110);
                        }
                    }
                    break;

                case 110:   //接盤馬達下降至安全等待高度
                    {
                        if (LoadTrayZGoto(Pos_WaitingZ))
                        {
                            Task.Next(120);
                        }
                    }
                    break;

                case 120:   //檢查台車是否有盤(無盤則發Alarm) && 檢查台車盤是否定位良好(定位不良則發Alarm)
                    {
                        if (ShuttleTrayExist(tid))      //檢查台車是否有盤
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TrayNotOnShuttle);
                            //Task.Next(130);   //Jay Tsao 2017/10/19 ###Removed - 增加"檢查台車盤是否定位良好"於 Action_LoadTray() - Case 120
                            if (ShuttleTrayInPosition(tid))     //檢查台車盤是否定位良好
                            {
                                this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TrayInWrongPosition);
                                Task.Next(1000);
                            }
                            else
                            {
                                //Alarm : 載盤區台車盤定位不良 {0}
                                this.ShowAlarm((int)CTMAlarmCode.ER_TLU_TrayInWrongPosition);
                            }
                        }
                        else
                        {
                            //Alarm : 載盤區台車盤未至定位 {0}
                            this.ShowAlarm((int)CTMAlarmCode.ER_TLU_TrayNotOnShuttle);
                        }
                    }
                    break;

                case 1000:  //載盤完成
                    {
                        cRet = CTrayShuttleLoadUnloadResult.FINISH;
                    }
                    break;
            }
            return cRet;
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
            //Alarm : 載盤區已被台車鎖定 {0}
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
                //Alarm : 載盤區已被台車鎖定 {0}
                this.ShowAlarm((int)CTMAlarmCode.ER_TLU_AlreadyLocked);
                return false;
            }

            if (!tid.Equals(CTrayShuttleID.TopCTrayShuttle) && !tid.Equals(CTrayShuttleID.BottomCTrayShuttle))
            {
                //Alarm : 台車編號指定錯誤 {0}
                this.ShowAlarm((int)CTMAlarmCode.ER_TLU_AssignWrongShuttleID);
                return false;
            }

            bool bRet = false;
            JActionTask Task = Task_UnloadTray;
            switch (Task.Value)
            {
                case 0:     //盤分離氣缸關閉
                    {
                        ThreeValued crRet = TraySeparateCtrl(false, 300, 3000);
                        if (crRet.Equals(ThreeValued.TRUE))
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TraySeparateCyOffTimeout);
                            Task.Next(10);
                        }
                    }
                    break;

                case 10:   //接盤馬達下降至安全等待高度
                    {
                        if (LoadTrayZGoto(Pos_WaitingZ))
                        {
                            Task.Next(20);
                        }
                    }
                    break;

                case 20:    //檢查台車上是否有盤(無盤則Alarm : 台車無盤)
                    {
                        if (ShuttleTrayExist(tid))
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TrayNotOnShuttle);
                            Task.Next(30);
                        }
                        else
                        {
                            //Alarm : 載盤區台車盤未至定位 {0}
                            this.ShowAlarm((int)CTMAlarmCode.ER_TLU_TrayNotOnShuttle);
                        }
                    }
                    break;

                case 30:   //檢查台車盤是否定位良好(定位不良則發Alarm)
                    {
                        if (ShuttleTrayInPosition(tid))
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TrayInWrongPosition);
                            Task.Next(40);
                        }
                        else
                        {
                            //Alarm : 載盤區台車盤定位不良 {0}
                            this.ShowAlarm((int)CTMAlarmCode.ER_TLU_TrayInWrongPosition);
                        }
                    }
                    break;

                case 40:    //接盤馬達上升至盤台車高度
                    {
                        int Pos_TrayOnTrolleyZ = Pos_WaitingZ;
                        switch (tid)
                        {
                            case CTrayShuttleID.TopCTrayShuttle:
                                Pos_TrayOnTrolleyZ = Pos_TrayOnTopTrolleyZ;
                                break;

                            case CTrayShuttleID.BottomCTrayShuttle:
                                Pos_TrayOnTrolleyZ = Pos_TrayOnBotTrolleyZ;
                                break;
                        }
                        if (LoadTrayZGoto(Pos_TrayOnTrolleyZ))
                        {
                            Task.Next(50);
                        }
                    }
                    break;

                case 50:    //接盤馬達至盤分離高度
                    {
                        if (LoadTrayZGoto(Pos_SepTrayZ))
                        {
                            Task.Next(60);
                        }
                    }
                    break;

                case 60:    //盤分離氣缸開啟
                    {
                        ThreeValued crRet = TraySeparateCtrl(true, 300, 3000);
                        if (crRet.Equals(ThreeValued.TRUE))
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TraySeparateCyOnTimeout);
                            Task.Next(70);
                        }
                    }
                    break;

                case 70:    //接盤馬達至接盤高度
                    {
                        if (LoadTrayZGoto(Pos_LoadTrayZ))
                        {
                            Task.Next(80);
                        }
                    }
                    break;

                case 80:    //盤分離氣缸關閉
                    {
                        ThreeValued crRet = TraySeparateCtrl(false, 300, 3000);
                        if (crRet.Equals(ThreeValued.TRUE))
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TraySeparateCyOffTimeout);
                            Task.Next(90);
                        }
                    }
                    break;

                case 90:   //接盤馬達下降至安全等待高度
                    {
                        if (LoadTrayZGoto(Pos_WaitingZ))
                        {
                            Task.Next(100);
                        }
                    }
                    break;

                case 100:   //檢查台車是否無盤(有盤則發Alarm)
                    {
                        if (ShuttleTrayNonExist(tid))
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TrayRemainOnShuttle);
                            Task.Next(110);
                        }
                        else
                        {
                            //Alarm : 載盤區台車有盤殘留 {0}
                            this.ShowAlarm((int)CTMAlarmCode.ER_TLU_TrayRemainOnShuttle);
                        }
                    }
                    break;

                case 110:    //判斷盤堆疊區是否未滿盤(滿盤則Alarm : 盤堆疊高度過高)
                    {
                        if (IsStackerNotFullTray())
                        {
                            this.ClearAlarm((int)CTMAlarmCode.ER_TLU_TrayFull);
                            Task.Next(1000);
                        }
                        else
                        {
                            //Alarm : 載盤區盤堆疊高度過高 {0}
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