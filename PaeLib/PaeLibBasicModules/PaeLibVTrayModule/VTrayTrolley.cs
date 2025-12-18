using PaeLibGeneral;
using PaeLibProVSDKEx;
using ProVLib;
using System;

namespace PaeLibVTrayModule
{
    /// <summary>
    /// <c>直盤台車</c>類別
    /// </summary>
    /// <remarks>
    /// 1. 使用說明：
    /// <para>(o) --> ActionReset_BeforeTakeTray() --> Action_BeforeTakeTray() + IsLotEnd() -|</para>
    /// <para>    |-- No, Not LotEnd, but can take tray            -NEXT--> ActionReset_TakeTray() --> Action_TakeTray() --> Goto() --> ActionReset_GiveTray() --> Action_GiveTray() --> (o)</para>
    /// <para>    |-- No, Not LotEnd and can NOT take tray         -IDLE--! (Back to Action_BeforeTakeTray() + IsLotEnd())</para>
    /// <para>    |-- Yes, it's LotEnd and Tray Loader is LotEnded -CASE1-> LotEnd().</para>
    /// 2. 其他功能：
    /// <para>   a. 盤固定：Action_ResetFixTray() --> Action_FixTray()</para>
    /// <para>   b. 盤釋放：Action_ResetReleaseTray() --> Action_ReleaseTray()</para>
    /// 3. 注意事項：
    /// <para>此台車模組雖有防撞保護，但前提是台車只能往前移動，不可後退，否則有可能會發生吸嘴取放中，台車移動的情況。(因為高優先權的台車若後退，低優先權的台車要讓開)</para>
    /// </remarks>
    public class VtmTrolley : JModuleBase
    {
        #region 程序變數

        private JActionTask Task_Home = new JActionTask();
        private JActionTask Task_TrayScan = new JActionTask();
        private JActionTask Task_FixTray = new JActionTask();
        private JActionTask Task_ReleaseTray = new JActionTask();
        private JActionTask Task_BeforeLoadTray = new JActionTask();    //v1.3 - Jay Cao Added
        private JActionTask Task_LoadTray = new JActionTask();
        private JActionTask Task_UnloadTray = new JActionTask();
        private JActionTask Task_UnloadTrayByLoader = new JActionTask();

        private int LastLoadTrayPos = 0;    //v1.3 - Jay Cao Added : 最後一次載盤位置

        #endregion 程序變數

        #region 元件

        //Input
        private DigitalInput DI_TrayDectet = null;     //有無盤偵測

        private DigitalInput DI_TrolleyUnsafe = null;    //台車是否在不安全區偵測(B接點, ON:安全, OFF:不安全)，此 sensor 僅有在直盤模組的台車與搬盤機構會有行程干涉的情況下才會安裝，目的是要讓搬盤機構判斷台車是否在安全位置，避免與台車干涉
        private DigitalInput DI_JumpIC = null;
        private DigitalInput DI_TrayOnTrack_L = null;
        private DigitalInput DI_TrayOnTrack_M = null;
        private DigitalInput DI_TrayOnTrack_R = null;

        //Cylinder
        private Cylinder CY_BasePlane = null;      //基準邊氣缸

        private Cylinder CY_Clamp = null;          //夾盤氣缸
        private Cylinder CY_SidePush = null;       //側推氣缸

        //Motor
        private Motor MT_Trolley = null;

        private VTrayTrolleyControlCenter VTTCtrl = null;
        private bool VirtualState = false;

        //Timer
        private MyTimer OverTime_Home = new MyTimer(true);

        private MyTimer OverTime_ScanTray = new MyTimer(true);
        private MyTimer OverTime_FixTray = new MyTimer(true);
        private MyTimer OverTime_ReleaseTray = new MyTimer(true);
        private MyTimer OverTime_LoadTray = new MyTimer(true);
        private MyTimer OverTime_UnLoadTray = new MyTimer(true);
        private MyTimer OverTime_UnloadTrayByLoader = new MyTimer(true);

        //VtmLoader
        private VtmLoader TraySource = null;    //v1.3.1 - Jay Cao Added : Tray 來源物件

        //VtmUnLoader
        private VtmUnLoader TrayDestination = null;  //v1.3.1 - Jay Cao Added : Tray 目的物件

        #endregion 元件

        /// <summary>
        /// Initializes a new instance of the <see cref="VtmTrolley" /> class. (建立<c>實體</c>直盤台車物件使用)
        /// </summary>
        /// <param name="alarm">Alarm委派</param>
        /// <param name="line">台車路線編號(同一Tray模組的左右台車為同一編號)</param>
        /// <param name="id">台車編號</param>
        /// <param name="initPos">台車初始位置(絕對座標)</param>
        /// <param name="len">台車長度(um)</param>
        /// <param name="safeDist">台車間的安全距離(um)</param>
        /// <param name="mtTRR">台車馬達</param>
        /// <param name="obBasePlane">基準邊氣缸</param>
        /// <param name="ibBasePlaneOn">基準邊氣缸 ON Sensor</param>
        /// <param name="ibBasePlaneOff">基準邊氣缸 OFF Sensor</param>
        /// <param name="obClamp">夾盤氣缸</param>
        /// <param name="ibClampOn">夾盤氣缸 ON Sesnor</param>
        /// <param name="ibClampOff">夾盤氣缸 OFF Sensor</param>
        /// <param name="obSidePush">側推氣缸</param>
        /// <param name="ibSidePushOn">側推氣缸 ON Sensor</param>
        /// <param name="ibSidePushOff">側推氣缸 OFF Sensor</param>
        /// <param name="ibTrayDetect">有無盤偵測</param>
        /// <param name="ibTrolleyUnsafe">台車是否在不安全區偵測</param>
        /// <param name="ibTRRjumpic">跳料偵測</param>
        /// <param name="ibTrayOnTrack_L">軌道盤定位偵測(左,前)</param>
        /// <param name="ibTrayOnTrack_M">軌道盤定位偵測(中)</param>
        /// <param name="ibTrayOnTrack_R">軌道盤定位偵測(右,後)</param>
        /// <param name="IsVirtual">是否為虛擬台車(用以Lock某區域防止台車進入)</param>
        public VtmTrolley(
            AlarmCallback alarm,
            int line, TROLLEY_ID id, int initPos, uint len, uint safeDist, Motor mtTRR,
            OutBit obBasePlane, InBit ibBasePlaneOn, InBit ibBasePlaneOff,
            OutBit obClamp, InBit ibClampOn, InBit ibClampOff,
            OutBit obSidePush, InBit ibSidePushOn, InBit ibSidePushOff,
            InBit ibTrayDetect, InBit ibTrolleyUnsafe, InBit ibTRRjumpic,
            InBit ibTrayOnTrack_L, InBit ibTrayOnTrack_M, InBit ibTrayOnTrack_R,
            bool IsVirtual = false)
            : base(alarm)
        {
            this.LineID = line;
            this.ID = id;
            this.Length = len;
            this.SafeDistance = safeDist;
            this.InitialPos = initPos;
            this.LockedPos = initPos;
            VirtualState = IsVirtual;

            //控制元件初始化
            MT_Trolley = mtTRR;
            CY_BasePlane = new Cylinder(obBasePlane, ibBasePlaneOn, ibBasePlaneOff);
            CY_Clamp = new Cylinder(obClamp, ibClampOn, ibClampOff);
            CY_SidePush = new Cylinder(obSidePush, ibSidePushOn, ibSidePushOff);
            DI_TrayDectet = new DigitalInput(ibTrayDetect);
            DI_TrolleyUnsafe = new DigitalInput(ibTrolleyUnsafe);
            DI_JumpIC = new DigitalInput(ibTRRjumpic);
            DI_TrayOnTrack_L = new DigitalInput(ibTrayOnTrack_L);
            DI_TrayOnTrack_M = new DigitalInput(ibTrayOnTrack_M);
            DI_TrayOnTrack_R = new DigitalInput(ibTrayOnTrack_R);
            //設定計時器自動重置
            OverTime_Home.AutoReset = true;
            OverTime_ScanTray.AutoReset = true;
            OverTime_FixTray.AutoReset = true;
            OverTime_ReleaseTray.AutoReset = true;
            OverTime_UnloadTrayByLoader.AutoReset = true;

            //將台車加入防撞系統監控清單中
            VTTCtrl = new VTrayTrolleyControlCenter();
            VTTCtrl.AddTrolley(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VtmTrolley" /> class. (建立<c>虛擬</c>直盤台車物件使用)
        /// </summary>
        /// <param name="alarm">Alarm委派</param>
        /// <param name="line">台車路線編號(同一Tray模組的左右台車為同一編號)</param>
        /// <param name="id">台車編號</param>
        /// <param name="initPos">台車初始位置(絕對座標)</param>
        /// <param name="len">台車長度(um)</param>
        /// <param name="safeDist">台車間的安全距離(um)</param>
        public VtmTrolley(
            AlarmCallback alarm, int line, TROLLEY_ID id, int initPos, uint len, uint safeDist)
            : this(alarm, line, id, initPos, len, safeDist, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, true)
        {
        }

        ~VtmTrolley()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (DI_TrolleyUnsafe != null) DI_TrolleyUnsafe.Dispose();
            if (DI_TrayDectet != null) DI_TrayDectet.Dispose();
            if (CY_SidePush != null) CY_SidePush.Dispose();
            if (CY_Clamp != null) CY_Clamp.Dispose();
            if (CY_BasePlane != null) CY_BasePlane.Dispose();
        }

        protected override void SimulationSetting(bool simulation)
        {
            //實作父類別抽象函式內容
            CY_BasePlane.Simulation = simulation;
            CY_Clamp.Simulation = simulation;
            CY_SidePush.Simulation = simulation;
            DI_TrayDectet.Simulation = simulation;
            DI_TrolleyUnsafe.Simulation = simulation;
        }

        #region 公開屬性

        /// <summary>
        /// 台車是否移動中 (Read-Only)
        /// </summary>
        /// <value>
        ///   <c>true</c> if busy; otherwise, <c>false</c>.
        /// </value>
        public bool Busy
        {
            get { return MT_Trolley.Busy(); }
        }

        /// <summary>
        /// 台車移動路線編號 (Read-Only)
        /// </summary>
        public int LineID { get; private set; }

        /// <summary>
        /// Trolley ID (Read-Only)
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public TROLLEY_ID ID { get; private set; }

        /// <summary>
        /// 台車長度 (Read-Only)
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public uint Length { get; private set; }

        /// <summary>
        /// 台車安全間距 (Read-Only)
        /// </summary>
        /// <value>
        /// The safe distance.
        /// </value>
        public uint SafeDistance { get; private set; }

        /// <summary>
        /// 台車初始位置 (Read-Only)
        /// </summary>
        /// <value>
        /// The initial position.
        /// </value>
        public int InitialPos { get; private set; }

        private int mRelativeCurrentPos = 0;

        /// <summary>
        /// 目前位置 (相對座標) (Read-Only)
        /// </summary>
        /// <value>
        /// The relative current position.
        /// </value>
        public int RelativeCurrentPos
        {
            get
            {
                //有馬達則以馬達回朔位置為準，若無馬達則以使用者指定值為準
                if (MT_Trolley != null)
                {
                    mRelativeCurrentPos = MT_Trolley.ReadEncPos();
                }
                return mRelativeCurrentPos;
            }
            private set
            {
                mRelativeCurrentPos = value;
            }
        }

        /// <summary>
        /// 使用者指定目標位置 (相對座標) (Read-Only)
        /// </summary>
        /// <value>
        /// The relative target position.
        /// </value>
        public int RelativeTargetPos { get; internal set; }

        /// <summary>
        /// 目前位置(絕對座標) (Read-Only)
        /// </summary>
        /// <value>
        /// The current position.
        /// </value>
        public int CurrentPos
        {
            get
            {
                int pos = this.InitialPos + this.RelativeCurrentPos;
                return pos;
            }
        }

        /// <summary>
        /// 目標位置(絕對座標) (Read-Only)
        /// </summary>
        /// <value>
        /// The target position.
        /// </value>
        public int TargetPos
        {
            get
            {
                int pos = this.InitialPos + this.RelativeTargetPos;
                return pos;
            }
        }

        /// <summary>
        /// 鎖定位置(絕對座標) (不開放給使用者直接設定此值，使用者僅可設定 RelativeTargetPos)
        /// </summary>
        /// <value>
        /// The locked position.
        /// </value>
        internal int LockedPos { get; set; }

        /// <summary>
        /// 台車狀態 (Read-Only)
        /// <list type="bullet">
        /// <item><term>IDLE : </term><description>無盤(0)</description></item>
        /// <item><term>LOADING : </term><description>載盤中(99)</description></item>
        /// <item><term>READY : </term><description>準備去工作(30~49)</description></item>
        /// <item><term>WORKING : </term><description>工作中(50~59)</description></item>
        /// <item><term>FINISH : </term><description>工作完成(10~29)</description></item>
        /// <item><term>WAITUNLOADING : </term><description>等待收盤(97)</description></item>
        /// <item><term>UNLOADING : </term><description>收盤中(98)</description></item>
        /// </list>
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public TROLLEY_STATE Status { get; private set; }

        #endregion 公開屬性

        #region 私有function

        /// <summary>
        /// 台車基準邊氣缸升降控制
        /// </summary>
        /// <param name="val">true : 上升，false : 下降</param>
        /// <param name="delay_ms">作動後的延遲時間</param>
        /// <param name="timeout_ms">等待作動的逾時時間</param>
        /// <returns></returns>
        private ThreeValued BasePlaneCtrl(bool val, int delay_ms, int timeout_ms)
        {
            ThreeValued tRet = ThreeValued.UNKNOWN;
            if (CY_BasePlane != null)
            {
                if (val)
                {
                    tRet = CY_BasePlane.On(delay_ms, timeout_ms);
                }
                else
                {
                    tRet = CY_BasePlane.Off(delay_ms, timeout_ms);
                }
                if (tRet == ThreeValued.FALSE)
                {
                    if (val)
                    {
                        //Alarm : 盤台車基準邊氣缸上升動作逾時
                        this.ShowAlarm((int)VTMAlarmCode.TrayTrolleyBasePlaneCylinderUpTimeout);
                    }
                    else
                    {
                        //Alarm : 盤台車基準邊氣缸下降動作逾時
                        this.ShowAlarm((int)VTMAlarmCode.TrayTrolleyBasePlaneCylinderDownTimeout);
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

        /// <summary>
        /// 台車夾盤氣缸控制
        /// </summary>
        /// <param name="val">true : 夾盤，false : 放開</param>
        /// <param name="delay_ms">作動後的延遲時間</param>
        /// <param name="timeout_ms">等待作動的逾時時間</param>
        /// <returns></returns>
        private ThreeValued ClampCtrl(bool val, int delay_ms, int timeout_ms)
        {
            ThreeValued tRet = ThreeValued.UNKNOWN;
            if (CY_Clamp != null)
            {
                if (val)
                {
                    tRet = CY_Clamp.On(delay_ms, timeout_ms);
                }
                else
                {
                    tRet = CY_Clamp.Off(delay_ms, timeout_ms);
                }
                if (tRet == ThreeValued.FALSE)
                {
                    if (val)
                    {
                        //Alarm : 盤台車夾盤氣缸夾盤動作逾時
                        this.ShowAlarm((int)VTMAlarmCode.TrayTrolleyClampCylinderFixTimeout);
                    }
                    else
                    {
                        //Alarm : 盤台車夾盤氣缸放開動作逾時
                        this.ShowAlarm((int)VTMAlarmCode.TrayTrolleyClampCylinderReleaseTimeout);
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

        /// <summary>
        /// 盤側推氣缸控制
        /// </summary>
        /// <param name="val"></param>
        /// <param name="delay_ms"></param>
        /// <param name="timeout_ms"></param>
        /// <returns></returns>
        private ThreeValued SidePushCtrl(bool val, int delay_ms, int timeout_ms)
        {
            ThreeValued tRet = ThreeValued.UNKNOWN;
            if (CY_SidePush != null)
            {
                if (val)
                {
                    tRet = CY_SidePush.On(delay_ms, timeout_ms);
                }
                else
                {
                    tRet = CY_SidePush.Off(delay_ms, timeout_ms);
                }
                if (tRet == ThreeValued.FALSE)
                {
                    if (val)
                    {
                        //Alarm : 盤側推氣缸推出動作逾時
                        this.ShowAlarm((int)VTMAlarmCode.TrayTrolleySidePushCylinderOutTimeout);
                    }
                    else
                    {
                        //Alarm : 盤側推氣缸放收回作逾時
                        this.ShowAlarm((int)VTMAlarmCode.TrayTrolleySidePushCylinderBackTimeout);
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

        /// <summary>
        /// 判斷台車是否離開 Tray Loader Area
        /// !!此函式僅供 VtmLoader 物件使用!!
        /// v1.3 - Jay Cao Added
        /// ！！注意！！
        /// ！！此機制尚無法應付多個 Tray Loader Area 的模組！！
        /// ！！注意！！
        /// </summary>
        /// <returns></returns>
        private bool IsTrolleyOutOfTrayLoaderArea()
        {
            int dist = Math.Abs(this.LastLoadTrayPos - this.RelativeCurrentPos);
            if (dist > (this.Length + this.SafeDistance))
            {
                return true;
            }
            return false;
        }

        #endregion 私有function

        #region 共用function

        /// <summary>
        /// <c>切換台車狀態</c>
        /// <para>僅可設定 READY, WORKING, FINISH 等狀態，其他狀態為程式內部使用</para>
        /// <para>虛擬台車可設定任何狀態</para>
        /// </summary>
        /// <param name="ts">The trolley status want to set.</param>
        /// <returns></returns>
        public bool SetStatus(TROLLEY_STATE ts)
        {
            /*
             * IDLE = 0, (不可設定)
             *
             * READY = 30~49, (可設定)
             * WORKING = 50~69, (可設定)
             * FINISH = 10~29, (可設定)
             *
             * LOADING = 99, (不可設定)
             * UNLOADING = 98, (不可設定)
             * UNKNOW = 100 (不可設定)
            */
            //虛擬台車設定
            if (VirtualState)
            {
                this.Status = ts;
                return true;
            }
            //檢查目前台車狀態是否為 READY 且要設定的狀態為 WORKING, READY or FINISH
            if ((((int)this.Status >= 30) && ((int)this.Status <= 49)) &&
                ((((int)ts >= 50) && ((int)ts <= 69)) ||
                (((int)ts >= 30) && ((int)ts <= 49)) ||
                (((int)ts >= 10) && ((int)ts <= 29))))
            {
                this.Status = ts;
                return true;
            }
            //檢查目前台車狀態是否為 WORKING 且要設定的狀態為 READY or FINISH
            if ((((int)this.Status >= 50) && ((int)this.Status <= 69)) &&
                ((((int)ts >= 10) && ((int)ts <= 29)) || (((int)ts >= 30) && ((int)ts <= 49))))
            {
                this.Status = ts;
                return true;
            }
            //若不符合上述條件，不允許修改台車狀態
            return false;
        }

        /// <summary>
        /// <c>強制設定台車狀態</c>
        /// <para>注意！！僅限使用於模擬模式，且模組停用時使用。</para>
        /// </summary>
        /// <param name="ts">The ts.</param>
        public void ForceSetStatus(TROLLEY_STATE ts)
        {
            if (this.Simulation)
            {
                this.Status = ts;
            }
        }

        /// <summary>
        /// <c>判斷台車是否在安全區間</c>
        /// <para>此 sensor 僅有在直盤模組的台車與搬盤機構會有行程干涉的情況下才會安裝，目的是要讓搬盤機構判斷台車是否在安全位置，避免與台車干涉</para>
        /// </summary>
        /// <returns></returns>
        public bool InSaftArea()
        {
            if (this.Simulation)
            {
                return true;    //安全
            }
            return DI_TrolleyUnsafe.ValueOn;      //B接點, ON:安全, OFF:不安全
        }

        /// <summary>
        /// <c>台車移動</c>
        /// </summary>
        /// <param name="Pos">目標位置(相對座標)</param>
        /// <returns>
        /// ThreeValued.TRUE : 移動完成
        /// ThreeValued.FALSE : 移動失敗
        /// ThreeValued.UNKNOWN : 移動中
        /// </returns>
        public ThreeValued Goto(int Pos)
        {
            //判斷是否可 unlock tray loader
            //v1.3.1 - Jay Cao Added
            if ((TraySource != null) && (this.Status != TROLLEY_STATE.IDLE))
            {
                if (IsTrolleyOutOfTrayLoaderArea())
                {
                    TraySource.TrayHasBeenTaken(this);
                    TraySource = null;
                }
            }

            //移動到 Pos
            bool b1 = (Math.Abs(Pos) - Math.Abs(this.RelativeCurrentPos)) > 0;  //2020-08-08 Jay Cao Added : 限制移動位置只能往前(遠離原點)，不可回頭
            bool b2 = VTTCtrl.MoveTo(this.LineID, this.ID, Pos);
            if (b1 && b2)
            {
                if (Simulation)
                {
                    this.RelativeCurrentPos = (this.LockedPos - this.InitialPos);
                    return ThreeValued.TRUE;
                }
                bool B1 = MT_Trolley.G00(this.LockedPos - this.InitialPos);
                bool B2 = this.TargetPos.Equals(this.LockedPos);
                if (B1 && B2)
                {
                    return ThreeValued.TRUE;
                }
                return ThreeValued.UNKNOWN;
            }
            return ThreeValued.FALSE;
        }

        /// <summary>
        /// <c>軌道盤掃描流程重置</c>
        /// </summary>
        public void Action_TrayScanReset()
        {
            Task_TrayScan.Reset();
            OverTime_ScanTray.Restart();
        }

        /// <summary>
        /// <c>軌道盤掃描流程</c>
        /// </summary>
        /// <param name="StartPos">掃描起點</param>
        /// <param name="EndPos">掃描終點</param>
        /// <returns>
        /// ThreeValued.TRUE : 掃描完成
        /// ThreeValued.FALSE : 掃描失敗
        /// ThreeValued.UNKNOWN : 掃描中
        /// </returns>
        public ThreeValued Action_TrayScan(int StartPos, int EndPos)
        {
            //檢查掃描總時間是否超過 90 秒
            if (OverTime_ScanTray.On(90000))
            {
                OverTime_ScanTray.Restart();
                //Alarm : 軌道盤掃描流程動作動作逾時
                this.ShowAlarm((int)VTMAlarmCode.TrayTrolleyScanTrackTrayTimeout);
                return ThreeValued.FALSE;   //動作逾時
            }

            ThreeValued tRet = ThreeValued.UNKNOWN; //掃描中
            JActionTask Task = Task_TrayScan;
            switch (Task.Value)
            {
                case 0:     //檢查台車狀態是否為 IDLE
                    {
                        if (this.Status.Equals(TROLLEY_STATE.IDLE))
                        {
                            Task.Next(10);
                        }
                        else
                        {
                            //Alarm : 盤台車狀態錯誤，無法進行盤掃描
                            this.ShowAlarm((int)VTMAlarmCode.TrayTrolleyScanTrackTrayStatusError);
                            tRet = ThreeValued.FALSE;
                        }
                    }
                    break;

                case 10:     //台車移動至起點 (基準邊氣缸與夾盤氣缸皆須 OFF)
                    {
                        bool B1 = CY_BasePlane.OffSensorValue.Equals(true);
                        bool B2 = CY_Clamp.OffSensorValue.Equals(true);
                        //判斷基準邊氣缸與夾盤氣缸是否皆 OFF
                        if (B1 && B2)
                        {
                            //台車移動至起點
                            ThreeValued tRetTemp = Goto(StartPos);
                            if (tRetTemp == ThreeValued.TRUE)
                            {
                                Task.Next(20);
                            }
                            else
                            {
                                tRet = tRetTemp;
                            }
                        }
                        else
                        {
                            MT_Trolley.Stop();
                            tRet = ThreeValued.FALSE;
                            //Alarm : 盤台車基準邊氣缸或夾盤氣缸未釋放，盤掃描流程暫停
                            this.ShowAlarm((int)VTMAlarmCode.TrayTrolleyBasePlaneOrClampNotRelease);
                        }
                    }
                    break;

                case 20:     //台車移動至終點，同時掃描軌道有無盤 (基準邊氣缸與夾盤氣缸皆須 OFF)
                    {
                        bool B1 = CY_BasePlane.OffSensorValue.Equals(true);
                        bool B2 = CY_Clamp.OffSensorValue.Equals(true);
                        //判斷基準邊氣缸與夾盤氣缸是否皆 OFF
                        if (B1 && B2)
                        {
                            //掃描軌道有無盤
                            if (DI_TrayDectet.ValueOn)
                            {
                                MT_Trolley.Stop();
                                tRet = ThreeValued.FALSE;
                                //Alarm : 軌道有盤殘留，請手動移除
                                this.ShowAlarm((int)VTMAlarmCode.TrayTrolleyTrackTrayRemain);
                            }
                            else
                            {
                                //台車移動至終點
                                ThreeValued tRetTemp = Goto(EndPos);
                                if (tRetTemp == ThreeValued.TRUE)
                                {
                                    Task.Next(30);
                                }
                                else
                                {
                                    tRet = tRetTemp;
                                }
                            }
                        }
                        else
                        {
                            MT_Trolley.Stop();
                            tRet = ThreeValued.FALSE;
                            //Alarm : 盤台車基準邊氣缸或夾盤氣缸未釋放，盤掃描流程暫停
                            this.ShowAlarm((int)VTMAlarmCode.TrayTrolleyBasePlaneOrClampNotRelease);
                        }
                    }
                    break;

                case 30:     //台車移回起點 (基準邊氣缸與夾盤氣缸皆須 OFF)
                    {
                        bool B1 = CY_BasePlane.OffSensorValue.Equals(true);
                        bool B2 = CY_Clamp.OffSensorValue.Equals(true);
                        //判斷基準邊氣缸與夾盤氣缸是否皆 OFF
                        if (B1 && B2)
                        {
                            //台車移動至起點
                            ThreeValued tRetTemp = Goto(0);
                            if (tRetTemp == ThreeValued.TRUE)
                            {
                                Task.Next(1000);
                            }
                            else
                            {
                                tRet = tRetTemp;
                            }
                        }
                        else
                        {
                            MT_Trolley.Stop();
                            tRet = ThreeValued.FALSE;
                            //Alarm : 盤台車基準邊氣缸或夾盤氣缸未釋放，盤掃描流程暫停
                            this.ShowAlarm((int)VTMAlarmCode.TrayTrolleyBasePlaneOrClampNotRelease);
                        }
                    }
                    break;

                case 1000:     //完成
                    {
                        tRet = ThreeValued.TRUE;
                    }
                    break;
            }
            return tRet;
        }

        /// <summary>
        /// <c>盤台車歸零流程重置</c>
        /// </summary>
        /// <returns></returns>
        public void Action_ResetHome()
        {
            Task_Home.Reset();
            MT_Trolley.HomeReset();
            OverTime_Home.Restart();
            LastLoadTrayPos = 0;    //v1.3 - Jay Cao Added
            TraySource = null;      //v1.3.1 - Jay Cao Added
            TrayDestination = null;     //v1.3.1 - Jay Cao Added
        }

        /// <summary>
        /// <c>盤台車歸零流程</c>
        /// </summary>
        /// <returns></returns>
        public ThreeValued Action_Home()
        {
            //檢查流程總時間是否超過 90 秒
            if (OverTime_Home.On(90000))
            {
                OverTime_Home.Restart();
                //Alarm : 盤台車歸零流程動作逾時
                this.ShowAlarm((int)VTMAlarmCode.TrayTrolleyHomeTimeout);
                return ThreeValued.FALSE;   //歸零逾時
            }

            ThreeValued tRet = ThreeValued.UNKNOWN;
            JActionTask Task = Task_Home;
            switch (Task.Value)
            {
                case 0:     //盤側推氣缸 OFF
                    {
                        ThreeValued tRetTemp = SidePushCtrl(false, 50, 3000);
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

                case 10:     //盤夾持氣缸 OFF
                    {
                        ThreeValued tRetTemp = ClampCtrl(false, 200, 3000);
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

                case 20:     //盤基準邊氣缸 OFF
                    {
                        ThreeValued tRetTemp = BasePlaneCtrl(false, 50, 3000);
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

                case 30:     //盤台車歸零
                    {
                        if (MT_Trolley.Home())
                        {
                            this.Status = TROLLEY_STATE.IDLE;
                            Task.Next(1000);
                        }
                    }
                    break;

                case 1000:     //完成
                    {
                        tRet = ThreeValued.TRUE;
                    }
                    break;
            }
            return tRet;
        }

        /// <summary>
        /// <c>盤台車盤固定流程重置</c>
        /// </summary>
        /// <returns></returns>
        public void Action_ResetFixTray()
        {
            Task_FixTray.Reset();
            OverTime_FixTray.Restart();
        }

        /// <summary>
        /// <c>盤台車盤固定流程</c>
        /// </summary>
        /// <param name="TraySidePushPos">盤側推點</param>
        /// <param name="TrayWaitingPos">完成盤固定後等待點</param>
        /// <returns></returns>
        public ThreeValued Action_FixTray(int TraySidePushPos, int TrayWaitingPos)
        {
            //檢查流程總時間是否超過 90 秒
            if (OverTime_FixTray.On(90000))
            {
                OverTime_FixTray.Restart();
                //Alarm : 盤台車盤固定流程動作逾時
                this.ShowAlarm((int)VTMAlarmCode.TrayTrolleyFixTrayTimeout);
                return ThreeValued.FALSE;   //動作逾時
            }

            ThreeValued tRet = ThreeValued.UNKNOWN;
            JActionTask Task = Task_FixTray;
            switch (Task.Value)
            {
                case 0:     //基準邊上升
                    {
                        ThreeValued tRetTemp = BasePlaneCtrl(true, 50, 3000);
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

                case 10:    //夾盤 ON
                    {
                        ThreeValued tRetTemp = ClampCtrl(true, 50, 3000);
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

                case 20:    //台車移至側推點
                    {
                        ThreeValued tRetTemp = Goto(TraySidePushPos);
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

                case 30:    //盤側推 ON
                    {
                        ThreeValued tRetTemp = SidePushCtrl(true, 50, 3000);
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

                case 40:    //夾盤 OFF
                    {
                        ThreeValued tRetTemp = ClampCtrl(false, 50, 3000);
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

                case 50:    //夾盤 ON
                    {
                        ThreeValued tRetTemp = ClampCtrl(true, 100, 3000);
                        if (tRetTemp == ThreeValued.TRUE)
                        {
                            Task.Next(60);
                        }
                        else
                        {
                            tRet = tRetTemp;
                        }
                    }
                    break;

                case 60:    //盤側推 OFF
                    {
                        ThreeValued tRetTemp = SidePushCtrl(false, 50, 3000);
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

                case 70:    //台車移至等待點
                    {
                        ThreeValued tRetTemp = Goto(TrayWaitingPos);
                        if (tRetTemp == ThreeValued.TRUE)
                        {
                            Task.Next(1000);
                        }
                        else
                        {
                            tRet = tRetTemp;
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
        /// <c>盤台車盤釋放流程重置</c>
        /// </summary>
        /// <returns></returns>
        public void Action_ResetReleaseTray()
        {
            Task_ReleaseTray.Reset();
            OverTime_ReleaseTray.Restart();
        }

        /// <summary>
        /// <c>盤台車盤釋放流程</c>
        /// </summary>
        /// <returns></returns>
        public ThreeValued Action_ReleaseTray()
        {
            //檢查流程總時間是否超過 90 秒
            if (OverTime_ReleaseTray.On(90000))
            {
                OverTime_ReleaseTray.Restart();
                //Alarm : 盤台車盤釋放流程動作逾時
                this.ShowAlarm((int)VTMAlarmCode.TrayTrolleyReleaseTrayTimeout);
                return ThreeValued.FALSE;   //動作逾時
            }

            ThreeValued tRet = ThreeValued.UNKNOWN;
            JActionTask Task = Task_ReleaseTray;
            switch (Task.Value)
            {
                case 0:     //盤側推 OFF
                    {
                        ThreeValued tRetTemp = SidePushCtrl(false, 50, 3000);
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

                case 10:    //夾盤 OFF
                    {
                        ThreeValued tRetTemp = ClampCtrl(false, 50, 3000);
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

                case 20:    //基準邊 OFF
                    {
                        ThreeValued tRetTemp = BasePlaneCtrl(false, 500, 3000);
                        if (tRetTemp == ThreeValued.TRUE)
                        {
                            Task.Next(1000);
                        }
                        else
                        {
                            tRet = tRetTemp;
                        }
                    }
                    break;

                case 1000:    //完成
                    {
                        tRet = ThreeValued.TRUE;
                    }
                    break;
            }
            return tRet;
        }

        /// <summary>
        /// <c>台車取盤前置流程重置</c>
        /// <para>v1.3 - Jay Cao Added</para>
        /// </summary>
        public void ActionReset_BeforeTakeTray()
        {
            Task_BeforeLoadTray.Reset();
        }

        /// <summary>
        /// 台車取盤前置流程
        /// <para>v1.3 - Jay Cao Added</para>
        /// <para>將台車移回載盤區，並判斷 VTrayLoader 是否已完成載盤</para>
        /// </summary>
        /// <param name="TraySource">取盤區 VtmLoader 物件</param>
        /// <param name="LoadTrayPos">取盤位置</param>
        /// <returns>
        /// UNKNOWN : 流程進行中
        /// TRUE : 流程完成
        /// FALSE : 流程失敗(異常)
        /// </returns>
        public ThreeValued Action_BeforeTakeTray(VtmLoader TrayLoader, int LoadTrayPos)
        {
            if (TrayLoader == null)
            {
                //Alarm : 未指定 Tray Loader
                return ThreeValued.FALSE;
            }

            ThreeValued tRet = ThreeValued.UNKNOWN;
            JActionTask Task = Task_BeforeLoadTray;
            switch (Task.Value)
            {
                case 0:     //檢查台車狀態是否為 IDLE
                    {
                        if (this.Status.Equals(TROLLEY_STATE.IDLE))
                        {
                            Task.Next(10);
                        }
                        else
                        {
                            tRet = ThreeValued.FALSE;
                            //Alarm : 盤台車狀態錯誤，無法進行載盤
                            this.ShowAlarm((int)VTMAlarmCode.TrayTrolleyLoadTrayStatusError);
                        }
                    }
                    break;

                case 10:    //台車盤釋放 - Reset
                    {
                        Action_ResetReleaseTray();
                        Task.Next(20);
                    }
                    break;

                case 20:    //台車盤釋放
                    {
                        ThreeValued tRetTemp = Action_ReleaseTray();
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

                case 30:    //移至載盤位置
                    {
                        ThreeValued tRetTemp = Goto(LoadTrayPos);
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

                case 40:
                    {
                        //判斷 Tray Loader 是否完成載盤且可鎖定
                        if (TrayLoader.IsTrayReadyToTake(this))
                        {
                            LastLoadTrayPos = LoadTrayPos;
                            TraySource = TrayLoader;    //紀錄 Tray 來源(用以後續 unlock tray loader)
                            Task.Next(100);
                        }
                    }
                    break;

                case 100:
                    {
                        tRet = ThreeValued.TRUE;
                    }
                    break;
            }
            return tRet;
        }

        /// <summary>
        /// <c>盤台車載盤流程流程</c>
        /// </summary>
        public void ActionReset_TakeTray()
        {
            Task_LoadTray.Reset();
            OverTime_LoadTray.Restart();
        }

        /// <summary>
        /// <c>盤台車載盤流程</c>
        /// <para>v1.3 - Jay Cao Changed</para>
        /// <para>台車完成夾盤+盤定位動作，並移動至等待位置</para>
        /// </summary>
        /// <param name="TraySidePushPos">盤台車側推位置(通常同載盤位置)</param>
        /// <param name="WaitingPos">盤台車等待位置(完成載盤後)</param>
        /// <returns></returns>
        public ThreeValued Action_TakeTray(int TraySidePushPos, int WaitingPos)
        {
            //檢查動作總時間是否超過 90 秒
            if (OverTime_LoadTray.On(90000))
            {
                OverTime_LoadTray.Restart();
                //Alarm : 盤台車載盤流程動作逾時
                this.ShowAlarm((int)VTMAlarmCode.TrayTrolleyLoadTrayTimeout);
                return ThreeValued.FALSE;   //動作逾時
            }

            ThreeValued tRet = ThreeValued.UNKNOWN;
            JActionTask Task = Task_LoadTray;
            switch (Task.Value)
            {
                case 0:    //設定台車狀態為 LOADING
                    {
                        this.Status = TROLLEY_STATE.LOADING;
                        Task.Next(10);
                    }
                    break;

                case 10:    //台車執行夾盤作動 - Reset
                    {
                        Action_ResetFixTray();
                        Task.Next(20);
                    }
                    break;

                case 20:    //台車執行夾盤作動
                    {
                        ThreeValued tRetTemp = Action_FixTray(TraySidePushPos, TraySidePushPos);
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

                case 30:    //台車移動到等待位置
                    {
                        ThreeValued tRetTemp = Goto(WaitingPos);
                        if (tRetTemp == ThreeValued.TRUE)
                        {
                            //！！注意！！
                            //！！原本計畫在此呼叫 VtmLoader 的 TrayHasBeenTaken()，但是...！！
                            //！！因為大部分直盤模組的 WaitingPos 都還沒完全離開 Tray Loader 區！！
                            //！！甚至有些模組的 WaitingPos、TraySidePushPos 與 LoadTrayPos 必須一樣！！
                            //！！所以，非常無奈的，只好將呼叫 TrayHasBeenTaken() 的工作交給 User 了！！
                            //！！注意！！

                            this.Status = TROLLEY_STATE.READY_01;   //設定台車狀態為 READY_01
                            Task.Next(100);
                        }
                        else
                        {
                            tRet = tRetTemp;
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
        /// <c>盤台車退盤流程重置</c>
        /// </summary>
        public void ActionReset_GiveTray()
        {
            Task_UnloadTray.Reset();
            OverTime_UnLoadTray.Restart();
        }

        /// <summary>
        /// <c>盤台車退盤流程</c>
        /// </summary>
        /// <param name="UnLoadTrayPos">收盤點</param>
        /// <param name="WaitingPos">收盤前等待點</param>
        /// <returns></returns>
        public ThreeValued Action_GiveTray(VtmUnLoader TrayUnloader, int UnLoadTrayPos, int WaitingPos)
        {
            if (TrayUnloader == null)
            {
                //Alarm : 未指定 Tray Unloader
                return ThreeValued.FALSE;
            }

            //檢查動作總時間是否超過 90 秒
            if (OverTime_UnLoadTray.On(90000))
            {
                OverTime_UnLoadTray.Restart();
                //Alarm : 盤台車退盤流程動作逾時
                this.ShowAlarm((int)VTMAlarmCode.TrayTrolleyUnloadTrayTimeout);
                return ThreeValued.FALSE;   //動作逾時
            }

            ThreeValued tRet = ThreeValued.UNKNOWN;
            JActionTask Task = Task_UnloadTray;
            switch (Task.Value)
            {
                case 0:     //檢查台車狀態是否為 FINISH
                    {
                        if (this.Status.Equals(TROLLEY_STATE.FINISH_01) ||
                            this.Status.Equals(TROLLEY_STATE.FINISH_02) || this.Status.Equals(TROLLEY_STATE.WAITUNLOADING))
                        {
                            this.Status = TROLLEY_STATE.UNLOADING;  //設定台車狀態為 UNLOADING
                            Task.Next(10);
                        }
                        else
                        {
                            tRet = ThreeValued.FALSE;
                            //Alarm : 盤台車狀態錯誤，無法進行退盤
                            this.ShowAlarm((int)VTMAlarmCode.TrayTrolleyUnloadTrayStatusError);
                        }
                    }
                    break;

                case 10:    //判斷是否可鎖定 tray unloader
                    {
                        if (TrayUnloader.Lock(this))    //v1.3.1 - Jay Cao Added
                        {
                            Task.Next(20);
                        }
                        else
                        {
                            this.Goto(WaitingPos);  //v1.3.1 - Jay Cao Added : 移動到收盤前等待點
                        }
                    }
                    break;

                case 20:    //移至收盤位置
                    {
                        ThreeValued tRetTemp = Goto(UnLoadTrayPos);
                        {
                            if (DI_JumpIC.ValueOff && !this.DryRun)
                            {
                                //偵測到IC跳料
                                tRet = ThreeValued.FALSE;
                                break;
                            }
                            else
                            {
                                if (tRetTemp == ThreeValued.TRUE)
                                {
                                    Task.Next(30);
                                }
                                else
                                {
                                    tRet = tRetTemp;
                                }
                            }
                        }
                    }
                    break;

                case 30:    //台車盤釋放 - Reset
                    {
                        Action_ResetReleaseTray();
                        Task.Next(40);
                    }
                    break;

                case 40:    //台車盤釋放
                    {
                        ThreeValued tRetTemp = Action_ReleaseTray();
                        if (tRetTemp == ThreeValued.TRUE)
                        {
                            this.Status = TROLLEY_STATE.IDLE;   //設定台車狀態為 IDLE
                            TrayUnloader.InformToUnloadTray(this);  //通知收盤
                            Task.Next(100);
                        }
                        else
                        {
                            tRet = tRetTemp;
                        }
                    }
                    break;

                case 100:
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