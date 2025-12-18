using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProVIFM;
using ProVLib;
using KCSDK;
using PaeLibComponent;
using PaeLibGeneral;
using PaeLibProVSDKEx;
using CommonObj;

namespace BSM
{
    /// <summary>
    /// 資料讀取的方式: 
    /// 1. PReadValue => 產品相關資料的讀取
    /// 2. OReadValue => 通用設定資料的讀取
    /// 3. SReadValue => 模組設定資料的讀取
    /// 
    /// Log輸出的方式:
    /// 1. public void LogSay(EnLoggerType mType, params string[] msg);
    ///    LogMode=0 msg固定取用陣列第一項目值
    ///    LogMode=1 msg依類型不同取用項目值
    ///    Type="OP" 取用陣列第一項目值
    ///    Type="Alarm" [0]:模組名稱 [1]:錯誤代碼 [2]:錯誤內容 [3]:持續時間 (*由ShowAlarm函式處理)
    ///    Type="Comm" [0]:模組名稱 [1]:記錄內容
    ///    Type="SPC" [0~N]:欄位值
    /// 
    /// Alarm輸出的方式: 
    /// 1. public void ShowAlarm(string AlarmLevel, int ErrorCode);
    /// 2. public void ShowAlarm(string AlarmLevel, int ErrorCode, params object[] args);
    /// AlarmLevel: "E/e","W/w","I/i"
    /// 
    /// 常用判斷:
    /// 1. IsSimulation() => 是否為模擬狀態 0:實機 1:亂數模擬 2:純模擬
    /// 2. mCanHome => 是否可歸零
    /// 3. mHomeOk => 歸零完畢時，需設定此變數為 true
    /// 4. mLotend => 是否可結批
    /// 5. mLotendOk => 結批完畢時，需設定此變數為 true
    /// 
    /// 常用函式:
    /// 1. SetCanLotEnd: 通知模組可以進行結批
    /// 2. GetLotEndOk: 取得模組是否結批完成
    /// 3. GetLotend: 取得模組是否正在進行結批
    /// 4. SetLotEndOk: 設定模組結批完成
    /// 5. IsSimulation: 取得模組是否在模擬狀態
    /// 6. SetCanHome: 通知模組可以進行歸零
    /// 7. GetHomeOk: 取得模組歸零結果
    /// 8. SReadValue: 取得模組內部指定欄位資料
    /// 9. SSetValue: 設定模組內部指定欄位資料
    /// 10. SaveFile: 通知模組進行資料存檔
    /// 11. PReadValue: 取得產品設定指定表格/欄位資料
    /// 12. OReadValue: 取得通用設定指定欄位資料
    /// 13. GetUseModule: 取得模組開關設定
    /// 14. GetModuleID: 取得模組編號設定
    /// 
    /// 常用資料變數:
    /// 1. PackageName: 目前模組正在使用的產品名稱
    /// 2. IsAutoMode: 模組是否處理Auto模式
    /// 3. RunTM: MyTimer型別，可用於Auto流程
    /// 4. HomeTM: MyTimer型別，可用於Home流程
    /// </summary>
    /// 
    public partial class BSM : BaseModuleInterface, BSM_interface
    {
        private struct ActionPos//點位結構
        {
            public int iPos_BSM_A_Waiting;
            public int iPos_BSM_A_Work_HeadA;
            public int iPos_BSM_A_Work_HeadB;

            public int iPos_BSM_B_Waiting;
            public int iPos_BSM_B_Work_HeadA;
            public int iPos_BSM_B_Work_HeadB;
        }

        private enum BSM_ALARM_CODE
        {
            ER_BSM_Unknow = 0,
            ER_BSM_StageA_HoldCy_Timeout = 1,       //夾板氣缸逾時
            ER_BSM_StageA_FrontCy_Timeout = 2,      //前推氣缸逾時
            ER_BSM_StageA_SideCy_Timeout = 3,       //側定氣缸逾時
            ER_BSM_StageA_SupportCy_Timeout = 4,    //支撐氣缸逾時
            ER_BSM_StageB_HoldCy_Timeout = 5,
            ER_BSM_StageB_FrontCy_Timeout = 6,
            ER_BSM_StageB_SideCy_Timeout = 7,
            ER_BSM_StageB_SupportCy_Timeout = 8,

            ER_BSM_StageA_BoardRemain = 9,          //歸零時，偵測到StageA有板殘留
            ER_BSM_StageB_BoardRemain = 10,         //歸零時，偵測到StageB有板殘留
            ER_BSM_NoBoardInStageA = 11,            //StageA夾板前未偵測到板
            ER_BSM_NoBoardInStageB = 12,            //StageB夾板前未偵測到板

        }

        private class BIBStageInfo
        {           
            private BIBStageID m_id;
            public BIBStageID ID { get { return m_id; } }
            public BIBStageSate State { get; set; }
            public BIBStageModuleOwner Owner { get; set; }
            public int Position { get; set; }
            public int ViberationCount { get; set; }
            public JActionFlag Flag_Moving = new JActionFlag();
            public ACTIONMODE Action_Mode { get; set; }

            public BIBStageInfo(BIBStageID id, BIBStageSate state,
                BIBStageModuleOwner owner)
            {
                m_id = id;
                State = state;
                Owner = owner;
                Position = 0;
                ViberationCount = 0;
                Flag_Moving.Reset();
                Action_Mode = ACTIONMODE.NONE;
            }

            public void Reset()
            {               
                State = BIBStageSate.NONE;
                Owner = BIBStageModuleOwner.NONE;
                Position = 0;
                ViberationCount = 0;
                Flag_Moving.Reset();
                Action_Mode = ACTIONMODE.NONE;
            }
        }

        //左台車A
        private BIBStageInfo LTS = new BIBStageInfo(BIBStageID.BIBStageA,
            BIBStageSate.NONE, BIBStageModuleOwner.NONE);
        //右台車B
        private BIBStageInfo RTS = new BIBStageInfo(BIBStageID.BIBStageB,
            BIBStageSate.NONE, BIBStageModuleOwner.NONE);

        #region 控制元件
        //各馬達
        private Motor MT_AxisY_StageA = null;               //板台車A      MT_BSM_StageA_AxisY
        private Motor MT_AxisY_StageB = null;               //板台車B      MT_BSM_StageB_AxisY
        //各Sensor
        protected DigitalInput DI_HaveBIBDetect_A = null;   //有無板偵側A  IB_HaveBIBDetect_A
        protected DigitalInput DI_HaveBIBDetect_B = null;   //有無板偵側B  IB_HaveBIBDetect_B
        //各氣缸
        private Cylinder CY_HoldA = null;                   //夾板氣缸A    OB_Hold_Cy_A
        private Cylinder CY_FrontA = null;                  //前推氣缸A    OB_Front_Cy_A
        private Cylinder CY_SideA = null;                   //側定氣缸A    OB_Side_Cy_A
        private Cylinder CY_SupportA = null;                //支撐氣缸A    OB_Support_Cy_A
        private Cylinder CY_HoldB = null;                   //夾板氣缸B    OB_Hold_Cy_B
        private Cylinder CY_FrontB = null;                  //前推氣缸B    OB_Front_Cy_B
        private Cylinder CY_SideB = null;                   //側定氣缸B    OB_Side_Cy_B
        private Cylinder CY_SupportB = null;                //支撐氣缸B    OB_Support_Cy_B

        #endregion

        #region 流程控制變數
        bool SystemDryRun = false;//空跑

        //private BIBStageModuleOwner StageA_Owner;   //StageA使用者.
        //private BIBStageModuleOwner StageB_Owner;   //StageB使用者.
        //private ACTIONMODE StageA_ActionMode;       //StageA動作模式.
        //private ACTIONMODE StageB_ActionMode;       //StageB動作模式.
        //private int StageA_ActionPosition;          //StageA移動位置.
        //private int StageB_ActionPosition;          //StageB移動位置.
        //private JActionFlag StageA_Flag_Action;     //StageA動作旗標.
        //private JActionFlag StageB_Flag_Action;     //StageB動作旗標.

        #endregion

        #region 參數變數
        private bool Para_DryRun = false;           //空跑
        private bool Para_Simulation = false;       //桌上模擬
        private bool Para_DryRun_NoBoard = false;
        private bool Para_DryRun_NoDevice = false;

        private static int iTMCYDelay = 6000;       //氣缸流程動作逾時 超過6秒當成動作逾時狀態

        private ActionPos APos;                     //模組流程點位

        private bool bNotUseLeftStage = false;
        private bool bNotUseRightStage = false;

        #endregion


        public BSM()
        {
            InitializeComponent();
            CreateComponentList();

            //StageA_Flag_Action = new JActionFlag();
            //StageB_Flag_Action = new JActionFlag();
        }


        #region 繼承函數

        //模組解構使用
        public override void DisposeTH()
        {
            base.DisposeTH();
        }

        //程式初始化
        public override void Initial()
        {
            #region 控制元件 初始化
            //各馬達
            MT_AxisY_StageA = MT_BSM_StageA_AxisY;
            MT_AxisY_StageB = MT_BSM_StageB_AxisY;
            //各Sensor
            DI_HaveBIBDetect_A = new DigitalInput(IB_HaveBIBDetect_A);
            DI_HaveBIBDetect_B = new DigitalInput(IB_HaveBIBDetect_B);
            //各氣缸
            CY_HoldA = new Cylinder(OB_Hold_Cy_A, IB_Hold_Cy_A_On, IB_Hold_Cy_A_Off);
            CY_FrontA = new Cylinder(OB_Front_Cy_A, IB_Front_Cy_A_On, IB_Front_Cy_A_Off);
            CY_SideA = new Cylinder(OB_Side_Cy_A, IB_Side_Cy_A_On, IB_Side_Cy_A_Off);
            CY_SupportA = new Cylinder(OB_Support_Cy_A, IB_Support_Cy_A_On, IB_Support_Cy_A_Off);
            CY_HoldB = new Cylinder(OB_Hold_Cy_B, IB_Hold_Cy_B_On, IB_Hold_Cy_B_Off);
            CY_FrontB = new Cylinder(OB_Front_Cy_B, IB_Front_Cy_B_On, IB_Front_Cy_B_Off);
            CY_SideB = new Cylinder(OB_Side_Cy_B, IB_Side_Cy_B_On, IB_Side_Cy_B_Off);
            CY_SupportB = new Cylinder(OB_Support_Cy_B, IB_Support_Cy_B_On, IB_Support_Cy_B_Off);

            #endregion



        }

        //持續偵測函數
        public override void AlwaysRun() //持續掃描
        {

        }
        public override int ScanIO() { return 0; } //掃描硬體按鈕IO

        //歸零相關函數
        public override void HomeReset() //歸零前的重置
        {
            DataReset();
            mLotend = false;
            mLotendOk = false;
            mCanHome = false;
            mHomeOk = false;

            LTS.Reset();
            RTS.Reset();

            FC_BSM_HOME.TaskReset();
        }
        public override bool Home() //歸零
        {
            FC_BSM_HOME.MainRun();
            return mHomeOk;
        }

        //運轉相關函數
        public override void ServoOn() //Motor Servo On
        {
            MT_BSM_StageA_AxisY.ServoOn(true);
            MT_BSM_StageB_AxisY.ServoOn(true);
        }
        public override void ServoOff() //Motor Servo Off
        {
            MT_BSM_StageA_AxisY.ServoOn(false);
            MT_BSM_StageB_AxisY.ServoOn(false);
        }
        public override void RunReset() //運轉前初始化
        {
            FC_BSM_StageA_AUTO_LOCK.TaskReset();
            FC_BSM_StageA_AUTO_UNLOCK.TaskReset();
            FC_BSM_StageA_AUTO_MOVE.TaskReset();
            FC_BSM_StageB_AUTO_LOCK.TaskReset();
            FC_BSM_StageB_AUTO_UNLOCK.TaskReset();
            FC_BSM_StageB_AUTO_MOVE.TaskReset();


        }
        public override void Run() //運轉
        {
            FC_BSM_StageA_AUTO_LOCK.MainRun();
            FC_BSM_StageA_AUTO_UNLOCK.MainRun();
            FC_BSM_StageA_AUTO_MOVE.MainRun();
            FC_BSM_StageB_AUTO_LOCK.MainRun();
            FC_BSM_StageB_AUTO_UNLOCK.MainRun();
            FC_BSM_StageB_AUTO_MOVE.MainRun();


        }
        public override void SetSpeed(bool bHome = false) //速度設定
        {
            int SPD = 10000;//! 速度要再確認
            int ACC_MULTIPLE = 100000;
            int ACC_DEC = 100000;
            int SPEED_RATE = OReadValue("機台速率").ToInt();

            if (MT_BSM_StageA_AxisY != null)
            {
                if (!bHome)
                {
                    SPD = (SReadValue("MT_BSM_StageA_Y_SPEED").ToInt() * SPEED_RATE) / 100;
                    ACC_MULTIPLE = SReadValue("MT_BSM_StageA_Y_ACC_MULTIPLE").ToInt();
                    ACC_DEC = (SPD * ACC_MULTIPLE);
                }
                MT_BSM_StageA_AxisY.SetSpeed(SPD);
                MT_BSM_StageA_AxisY.SetAcceleration(ACC_DEC);
                MT_BSM_StageA_AxisY.SetDeceleration(ACC_DEC);
            }

            if (MT_BSM_StageB_AxisY != null)
            {
                if (!bHome)
                {
                    SPD = (SReadValue("MT_BSM_StageB_Y_SPEED").ToInt() * SPEED_RATE) / 100;
                    ACC_MULTIPLE = SReadValue("MT_BSM_StageB_Y_ACC_MULTIPLE").ToInt();
                    ACC_DEC = (SPD * ACC_MULTIPLE);
                }
                MT_BSM_StageB_AxisY.SetSpeed(SPD);
                MT_BSM_StageB_AxisY.SetAcceleration(ACC_DEC);
                MT_BSM_StageB_AxisY.SetDeceleration(ACC_DEC);
            }

        }

        //手動相關函數
        public override void ManualReset() { } //手動運行前置設定
        public override void ManualRun() { } //手動模式運行

        //停止所有馬達
        public override void StopMotor()
        {
            foreach (Motor motor in MotorList)
            {
                motor.Stop();
            }
        }

        #endregion


        #region 使用者函數

        /// <summary>
        /// 設定是否可以開始進行歸零
        /// </summary>
        public void SetCanRunHome()
        {
            mCanHome = true;
        }

        /// <summary>
        /// 設定是否可以開始進行結批
        /// </summary>
        public void SetCanLotEnd()
        {
            //mLotend = true;
        }

        /// <summary>
        /// 模組資料重置 (於 MainFlow DataReset() 中呼叫)
        /// </summary>
        public void DataReset()
        {
            LoadSetData(); //載入功能選項
            LoadBaseData();//載入基準點位
     
            //LTS.Owner = BIBStageModuleOwner.NONE;
            //LTS.State = BIBStageSate.NONE;
            //LTS.Position = 0;
            //LTS.Action_Mode = ACTIONMODE.NONE;
            //LTS.Flag_Moving.Reset();
            
            //RTS.Owner = BIBStageModuleOwner.NONE;
            //RTS.State = BIBStageSate.NONE;
            //RTS.Position = 0;
            //RTS.Action_Mode = ACTIONMODE.NONE;
            //RTS.Flag_Moving.Reset();



            //StageA_Owner = BIBStageModuleOwner.NONE;
            //StageB_Owner = BIBStageModuleOwner.NONE;
            //StageA_ActionMode = ACTIONMODE.NONE;
            //StageB_ActionMode = ACTIONMODE.NONE;
            //StageA_ActionPosition = 0;
            //StageB_ActionPosition = 0;
            //StageA_Flag_Action.Reset();
            //StageB_Flag_Action.Reset();
        }


        public BIBStageID IsBIBStageReady(BIBStageSate state)
        {
            if (LTS.State.Equals(state))
            {
                return LTS.ID;
            }
            if (RTS.State.Equals(state))
            {
                return RTS.ID;
            }

            return BIBStageID.NONE;
        }

        public BIBStageSate GetBoardStageState(BIBStageID id)
        {
            if (id == BIBStageID.BIBStageA)
            {
                return LTS.State;
            }
            if (id == BIBStageID.BIBStageB)
            {
                return RTS.State;
            }
            return BIBStageSate.NONE;
        }

        public ThreeValued SetBoardStageState(BIBStageID id, BIBStageSate state)
        {
            ThreeValued bRet = ThreeValued.UNKNOWN;
            BIBStageInfo TS = null;
            switch (id)
            {
                case BIBStageID.BIBStageA:
                    {
                        if (!bNotUseLeftStage)
                            TS = LTS;
                    }
                    break;
                case BIBStageID.BIBStageB:
                    {
                        if (!bNotUseRightStage)
                            TS = RTS;
                    }
                    break;
            }

            if (TS != null)
            {
                if (TS.Owner.Equals(BIBStageModuleOwner.NONE))
                {
                    TS.State = state;
                    bRet = ThreeValued.TRUE;
                }
                else
                {
                    bRet = ThreeValued.FALSE;
                }
            }
            return bRet;
        }

        public ThreeValued LockBIBStage(BIBStageModuleOwner owner, BIBStageID id)
        {
            ThreeValued bRet = ThreeValued.UNKNOWN;
            BIBStageInfo TS = null;
            switch (id)
            {
                case BIBStageID.BIBStageA:
                    {
                        TS = LTS;
                    }
                    break;
                case BIBStageID.BIBStageB:
                    {
                        TS = RTS;
                    }
                    break;
            }
            if (TS != null)
            {
                if (TS.Owner.Equals(owner))
                {
                    bRet = ThreeValued.TRUE;
                }
                else if (TS.Owner.Equals(BIBStageModuleOwner.NONE))
                {
                    TS.Owner = owner;
                    bRet = ThreeValued.TRUE;
                }
                else
                {
                    bRet = ThreeValued.FALSE;
                }
            }
            return bRet;
        }

        public ThreeValued UnlockBIBStage(BIBStageModuleOwner owner, BIBStageID id)
        {
            ThreeValued bRet = ThreeValued.UNKNOWN;
            BIBStageInfo TS = null;
            switch (id)
            {
                case BIBStageID.BIBStageA:
                    {
                        TS = LTS;
                    }
                    break;
                case BIBStageID.BIBStageB:
                    {
                        TS = RTS;
                    }
                    break;
            }
            if (TS != null)
            {
                if (TS.Owner.Equals(BIBStageModuleOwner.NONE))
                {
                    bRet = ThreeValued.TRUE;
                }
                else if (TS.Owner.Equals(owner))
                {
                    TS.Owner = BIBStageModuleOwner.NONE;
                    bRet = ThreeValued.TRUE;
                }
                else
                {
                    bRet = ThreeValued.FALSE;
                }
            }
            return bRet;
        }

        public ThreeValued SetActionCommand_BSM(BIBStageModuleOwner owner, BIBStageID id, ACTIONMODE mode, BasePosInfo pos)
        {
            ThreeValued bRet = ThreeValued.UNKNOWN;
            BIBStageInfo TS = null;
            switch (id)
            {
                case BIBStageID.BIBStageA:
                    {
                        TS = LTS;
                    }
                    break;
                case BIBStageID.BIBStageB:
                    {
                        TS = RTS;
                    }
                    break;
            }
            if (TS != null)
            {
                if (TS.Owner.Equals(owner))
                {
                    if (!TS.Flag_Moving.IsDoing())
                    {
                        TS.Action_Mode = mode;
                        TS.Position = pos.Y;
                        TS.Flag_Moving.DoIt();
                        SetSpeed();
                        bRet = ThreeValued.TRUE;
                        LogDebug("BSM", string.Format("SetActionCommand_BSM({0},{1},{2}, {3}) return true.", owner.ToString(), id.ToString(), mode.ToString(), pos.Y));
                    }
                    else
                    {
                        bRet = ThreeValued.FALSE;
                    }
                }
            }
            return bRet;
        }

        public ThreeValued GetActionResult_BSM(BIBStageID id)
        {
            ThreeValued bRet = ThreeValued.UNKNOWN;
            BIBStageInfo TS = null;
            switch (id)
            {
                case BIBStageID.BIBStageA:
                    {
                        TS = LTS;
                    }
                    break;
                case BIBStageID.BIBStageB:
                    {
                        TS = RTS;
                    }
                    break;
            }
            if (TS != null)
            {
                //判斷模組是否使用
                if (!GetUseModule())
                {
                    //模組不使用
                    TS.Flag_Moving.Done();
                    bRet = ThreeValued.TRUE;
                }
                else
                {
                    if (TS.Flag_Moving.IsDone())
                    {
                        bRet = ThreeValued.TRUE;
                    }
                    else
                    {
                        bRet = ThreeValued.FALSE;
                    }
                }
            }
            if (bRet == ThreeValued.TRUE)
            {
                LogDebug("BSM", string.Format("GetActionResult_BSM({0}) return true.", id.ToString()));
            }
            return bRet;
        }

        public BasePosInfo GetBasicInfo(BIBStageModuleOwner station, BIBStageID id)
        {
            BasePosInfo pos = new BasePosInfo();

            switch (station)
            {
                case BIBStageModuleOwner.HDT_BIB_A:
                    {
                        pos.X = 0;
                        pos.Y = id.Equals(BIBStageID.BIBStageA) ? SReadValue("Pos_BSM_A_Work_HeadA").ToInt() : SReadValue("Pos_BSM_B_Work_HeadA").ToInt();
                        pos.Z = 0;
                    }
                    break;
                case BIBStageModuleOwner.HDT_BIB_A_CCD://基準點: 手臂取放 與 CCD 共用
                    {
                        pos.X = 0;
                        pos.Y = id.Equals(BIBStageID.BIBStageA) ? SReadValue("Pos_BSM_A_Work_HeadA").ToInt() : SReadValue("Pos_BSM_B_Work_HeadA").ToInt();
                        pos.Z = 0;
                    }
                    break;
                case BIBStageModuleOwner.HDT_BIB_B:
                    {
                        pos.X = 0;
                        pos.Y = id.Equals(BIBStageID.BIBStageA) ? SReadValue("Pos_BSM_A_Work_HeadB").ToInt() : SReadValue("Pos_BSM_B_Work_HeadB").ToInt();
                        pos.Z = 0;
                    }
                    break;
                case BIBStageModuleOwner.HDT_BIB_B_CCD:
                    {
                        pos.X = 0;
                        pos.Y = id.Equals(BIBStageID.BIBStageA) ? SReadValue("Pos_BSM_A_Work_HeadB").ToInt() : SReadValue("Pos_BSM_B_Work_HeadB").ToInt();
                        pos.Z = 0;
                    }
                    break;
            }
            return pos;
        }

        #endregion


        #region 私有函數
        private void LogDebug(string module, string msg)
        {
            bool EnableDebugLog = true;
            if (EnableDebugLog && GetUseModule())
            {
                JLogger.LogDebug("Debug", module + " | " + msg);
            }
        }

        private void ShowAlarmMessage(string AlarmLevel, int ErrorCode, params object[] args)//Alarm顯示
        {
            ShowAlarm(AlarmLevel, ErrorCode, args);
        }
        private void ClearAlarmMessage(string AlarmLevel, int ErrorCode)
        {
            return;
        }

        private void LoadSetData()//載入功能選項
        {
            Para_Simulation = !IsSimulation().Equals(0);//是否為模擬模式

            //各Sensor
            DI_HaveBIBDetect_A.Simulation = Para_Simulation;
            DI_HaveBIBDetect_B.Simulation = Para_Simulation;
            //各氣缸
            CY_HoldA.Simulation = Para_Simulation;
            CY_FrontA.Simulation = Para_Simulation;
            CY_SideA.Simulation = Para_Simulation;
            CY_SupportA.Simulation = Para_Simulation;
            CY_HoldB.Simulation = Para_Simulation;
            CY_FrontB.Simulation = Para_Simulation;
            CY_SideB.Simulation = Para_Simulation;
            CY_SupportB.Simulation = Para_Simulation;

            SystemDryRun = OReadValue("DryRun").ToBoolean();
            Para_DryRun = SystemDryRun;
            Para_DryRun_NoBoard = OReadValue("DryRun_NoBoard").ToBoolean();
            Para_DryRun_NoDevice = OReadValue("DryRun_NoDevice").ToBoolean();
        }
        private void LoadBaseData()//載入基準點位
        {
            APos.iPos_BSM_A_Waiting = SReadValue("Pos_BSM_A_Waiting").ToInt();
            APos.iPos_BSM_A_Work_HeadA = SReadValue("Pos_BSM_A_Work_HeadA").ToInt();
            APos.iPos_BSM_A_Work_HeadB = SReadValue("Pos_BSM_A_Work_HeadB").ToInt();
            APos.iPos_BSM_B_Waiting = SReadValue("Pos_BSM_B_Waiting").ToInt();
            APos.iPos_BSM_B_Work_HeadA = SReadValue("Pos_BSM_B_Work_HeadA").ToInt();
            APos.iPos_BSM_B_Work_HeadB = SReadValue("Pos_BSM_B_Work_HeadB").ToInt();

            bNotUseLeftStage = SReadValue("DoNotUseLeftStage").ToBoolean();
            bNotUseRightStage = SReadValue("DoNotUseRightStage").ToBoolean();

        }

        //氣缸控制
        private bool Hold_Cy_Ctrl(BIBStageID id, bool val, int delay_ms = 100, int timeout_ms = 5000)   //夾板氣缸
        {
            ThreeValued crRet = ThreeValued.UNKNOWN;
            Cylinder CY = null;
            BSM_ALARM_CODE Alarm = BSM_ALARM_CODE.ER_BSM_Unknow;
            switch (id)
            {
                case BIBStageID.BIBStageA:
                    {
                        CY = CY_HoldA;
                        Alarm = BSM_ALARM_CODE.ER_BSM_StageA_HoldCy_Timeout;
                    }
                    break;
                case BIBStageID.BIBStageB:
                    {
                        CY = CY_HoldB;
                        Alarm = BSM_ALARM_CODE.ER_BSM_StageB_HoldCy_Timeout;
                    }
                    break;
            }

            if (CY != null)
            {
                if (val)
                {
                    crRet = CY.On(delay_ms, timeout_ms);
                }
                else
                {
                    crRet = CY.Off(delay_ms, timeout_ms);
                }

                if (crRet.Equals(ThreeValued.TRUE))
                {
                    ClearAlarmMessage("E", (int)Alarm);
                    return true;
                }
                else if (crRet.Equals(ThreeValued.FALSE))
                {
                    //Alarm : 夾板氣缸動作逾時
                    ShowAlarmMessage("E", (int)Alarm);
                }
            }
            return false;
        }
        private bool Front_Cy_Ctrl(BIBStageID id, bool val, int delay_ms = 100, int timeout_ms = 5000)  //前推氣缸
        {
            ThreeValued crRet = ThreeValued.UNKNOWN;
            Cylinder CY = null;
            BSM_ALARM_CODE Alarm = BSM_ALARM_CODE.ER_BSM_Unknow;
            switch (id)
            {
                case BIBStageID.BIBStageA:
                    {
                        CY = CY_FrontA;
                        Alarm = BSM_ALARM_CODE.ER_BSM_StageA_FrontCy_Timeout;
                    }
                    break;
                case BIBStageID.BIBStageB:
                    {
                        CY = CY_FrontB;
                        Alarm = BSM_ALARM_CODE.ER_BSM_StageB_FrontCy_Timeout;
                    }
                    break;
            }

            if (CY != null)
            {
                if (val)
                {
                    crRet = CY.On(delay_ms, timeout_ms);
                }
                else
                {
                    crRet = CY.Off(delay_ms, timeout_ms);
                }

                if (crRet.Equals(ThreeValued.TRUE))
                {
                    ClearAlarmMessage("E", (int)Alarm);
                    return true;
                }
                else if (crRet.Equals(ThreeValued.FALSE))
                {
                    //Alarm : 前推氣缸動作逾時
                    ShowAlarmMessage("E", (int)Alarm);
                }
            }
            return false;
        }
        private bool Side_Cy_Ctrl(BIBStageID id, bool val, int delay_ms = 100, int timeout_ms = 5000)   //側定氣缸
        {
            ThreeValued crRet = ThreeValued.UNKNOWN;
            Cylinder CY = null;
            BSM_ALARM_CODE Alarm = BSM_ALARM_CODE.ER_BSM_Unknow;
            switch (id)
            {
                case BIBStageID.BIBStageA:
                    {
                        CY = CY_SideA;
                        Alarm = BSM_ALARM_CODE.ER_BSM_StageA_SideCy_Timeout;
                    }
                    break;
                case BIBStageID.BIBStageB:
                    {
                        CY = CY_SideB;
                        Alarm = BSM_ALARM_CODE.ER_BSM_StageB_SideCy_Timeout;
                    }
                    break;
            }

            if (CY != null)
            {
                if (val)
                {
                    crRet = CY.On(delay_ms, timeout_ms);
                }
                else
                {
                    crRet = CY.Off(delay_ms, timeout_ms);
                }

                if (crRet.Equals(ThreeValued.TRUE))
                {
                    ClearAlarmMessage("E", (int)Alarm);
                    return true;
                }
                else if (crRet.Equals(ThreeValued.FALSE))
                {
                    //Alarm : 側定氣缸動作逾時
                    ShowAlarmMessage("E", (int)Alarm);
                }
            }
            return false;
        }
        private bool Support_Cy_Ctrl(BIBStageID id, bool val, int delay_ms = 100, int timeout_ms = 5000)//支撐氣缸
        {
            ThreeValued crRet = ThreeValued.UNKNOWN;
            Cylinder CY = null;
            BSM_ALARM_CODE Alarm = BSM_ALARM_CODE.ER_BSM_Unknow;
            switch (id)
            {
                case BIBStageID.BIBStageA:
                    {
                        CY = CY_SupportA;
                        Alarm = BSM_ALARM_CODE.ER_BSM_StageA_SupportCy_Timeout;
                    }
                    break;
                case BIBStageID.BIBStageB:
                    {
                        CY = CY_SupportB;
                        Alarm = BSM_ALARM_CODE.ER_BSM_StageB_SupportCy_Timeout;
                    }
                    break;
            }

            if (CY != null)
            {
                if (val)
                {
                    crRet = CY.On(delay_ms, timeout_ms);
                }
                else
                {
                    crRet = CY.Off(delay_ms, timeout_ms);
                }

                if (crRet.Equals(ThreeValued.TRUE))
                {
                    ClearAlarmMessage("E", (int)Alarm);
                    return true;
                }
                else if (crRet.Equals(ThreeValued.FALSE))
                {
                    //Alarm :支撐氣缸動作逾時
                    ShowAlarmMessage("E", (int)Alarm);
                }
            }
            return false;
        }
        //Sensor辨別狀態
        private bool HaveBIBDetector(BIBStageID id, bool bSensor)                          //有無板偵側
        {
            DigitalInput DI = null;
            switch (id)
            {
                case BIBStageID.BIBStageA:
                    {
                        DI = DI_HaveBIBDetect_A;
                    }
                    break;
                case BIBStageID.BIBStageB:
                    {
                        DI = DI_HaveBIBDetect_B;
                    }
                    break;
            }
            if (DI != null)
            {
                if ((Para_DryRun && Para_DryRun_NoBoard) || Para_Simulation || !GetUseModule())
                {
                    if (bSensor)
                    {
                        return true;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if (bSensor)
                    {
                        return DI.ValueOn;
                    }
                    else
                    {
                        return DI.ValueOff;
                    }
                }
            }
            return false;
        }
        //馬達移動流程控制
        private bool MT_AxisY_StageA_MoveToPos(int igopos)//板台車A移動至位置
        {
            bool bMotorMove = false;
            int iPos = 0;
            //int iBasePos = 0;
            //int OffsetY = 0;

            //switch (LTS.Owner)
            //{
            //    case BIBStageModuleOwner.HDT_BIB_A:
            //        APos.iPos_BSM_A_Work_HeadA = SReadValue("Pos_BSM_A_Work_HeadA").ToInt();//A手臂取放基準點
            //        iBasePos = APos.iPos_BSM_A_Work_HeadA;
            //        OffsetY = PReadValue("Offset_HDTA_BIB_A_Y").ToInt();
            //        break;
            //    case BIBStageModuleOwner.HDT_BIB_B:
            //        APos.iPos_BSM_A_Work_HeadB = SReadValue("Pos_BSM_A_Work_HeadB").ToInt();//B手臂取放基準點
            //        iBasePos = APos.iPos_BSM_A_Work_HeadB;
            //        OffsetY = PReadValue("Offset_HDTB_BIB_A_Y").ToInt();
            //        break;
            //    case BIBStageModuleOwner.HDT_BIB_A_CCD:
            //        APos.iPos_BSM_A_Work_HeadA = SReadValue("Pos_BSM_A_Work_HeadA").ToInt();//A手臂取放基準點(與CCD共用)
            //        iBasePos = APos.iPos_BSM_A_Work_HeadA;
            //        OffsetY = PReadValue("Offset_HDTA_BIB_A_Y").ToInt();
            //        break;
            //    case BIBStageModuleOwner.HDT_BIB_B_CCD:
            //        APos.iPos_BSM_A_Work_HeadB = SReadValue("Pos_BSM_A_Work_HeadB").ToInt();//B手臂取放基準點(與CCD共用)
            //        iBasePos = APos.iPos_BSM_A_Work_HeadB;
            //        OffsetY = PReadValue("Offset_HDTB_BIB_A_Y").ToInt();
            //        break;
            //    case BIBStageModuleOwner.TRANSFER:
            //        APos.iPos_BSM_A_Waiting = SReadValue("Pos_BSM_A_Waiting").ToInt();//台車等待點
            //        iBasePos = 0;
            //        OffsetY = 0;
            //        break;
            //}

            if (Para_Simulation || !GetUseModule())
            {
                return true;
            }
            if (MT_BSM_StageA_AxisY != null)
            {
                //iPos = igopos + iBasePos + OffsetY;
                iPos = igopos;//移動點由主流程運算完提供，所以不需再加基準點與offset

                bMotorMove = MT_AxisY_StageA.G00(iPos);
            }
            return bMotorMove;
        }
        private bool MT_AxisY_StageB_MoveToPos(int igopos)//板台車B移動至位置
        {
            bool bMotorMove = false;
            int iPos = 0;
            //int iBasePos = 0;
            //int OffsetY = 0;

            //switch (RTS.Owner)
            //{
            //    case BIBStageModuleOwner.HDT_BIB_A:
            //        APos.iPos_BSM_B_Work_HeadA = SReadValue("Pos_BSM_B_Work_HeadA").ToInt();//A手臂取放基準點
            //        iBasePos = APos.iPos_BSM_B_Work_HeadA;
            //        OffsetY = PReadValue("Offset_HDTA_BIB_B_Y").ToInt();
            //        break;
            //    case BIBStageModuleOwner.HDT_BIB_B:
            //        APos.iPos_BSM_B_Work_HeadB = SReadValue("Pos_BSM_B_Work_HeadB").ToInt();//B手臂取放基準點
            //        iBasePos = APos.iPos_BSM_B_Work_HeadB;
            //        OffsetY = PReadValue("Offset_HDTB_BIB_B_Y").ToInt();
            //        break;
            //    case BIBStageModuleOwner.HDT_BIB_A_CCD:
            //        APos.iPos_BSM_B_Work_HeadA = SReadValue("Pos_BSM_B_Work_HeadA").ToInt();//A手臂取放基準點(與CCD共用)
            //        iBasePos = APos.iPos_BSM_B_Work_HeadA;
            //        OffsetY = PReadValue("Offset_HDTA_BIB_B_Y").ToInt();
            //        break;
            //    case BIBStageModuleOwner.HDT_BIB_B_CCD:
            //        APos.iPos_BSM_B_Work_HeadB = SReadValue("Pos_BSM_B_Work_HeadB").ToInt();//B手臂取放基準點(與CCD共用)
            //        iBasePos = APos.iPos_BSM_B_Work_HeadB;
            //        OffsetY = PReadValue("Offset_HDTB_BIB_B_Y").ToInt();
            //        break;
            //    case BIBStageModuleOwner.TRANSFER:
            //        APos.iPos_BSM_B_Waiting = SReadValue("Pos_BSM_B_Waiting").ToInt();//台車等待點
            //        iBasePos = 0;
            //        OffsetY = 0;
            //        break;
            //}

            if (Para_Simulation || !GetUseModule())
            {
                return true;
            }
            if (MT_BSM_StageB_AxisY != null)
            {
                //iPos = igopos + iBasePos + OffsetY;
                iPos = igopos;//移動點由主流程運算完提供，所以不需再加基準點與offset

                bMotorMove = MT_AxisY_StageB.G00(iPos);
            }
            return bMotorMove;
        }


        #endregion


        #region 公用函數
        public void Protection_BSM(BIBStageModuleOwner owner, BIBStageID id)
        {
            switch (owner)
            {
                case BIBStageModuleOwner.HDT_BIB_A:
                    {
                        switch (id)
                        {
                            case BIBStageID.BIBStageA:
                                {
                                    if (MT_AxisY_StageA.Busy())
                                    {
                                        MT_AxisY_StageA.FastStop();
                                    }
                                }
                                break;
                            case BIBStageID.BIBStageB:
                                {
                                    if (MT_AxisY_StageB.Busy())
                                    {
                                        MT_AxisY_StageB.FastStop();
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case BIBStageModuleOwner.HDT_BIB_B:
                    {
                        switch (id)
                        {
                            case BIBStageID.BIBStageA:
                                {
                                    if (MT_AxisY_StageA.Busy())
                                    {
                                        MT_AxisY_StageA.FastStop();
                                    }
                                }
                                break;
                            case BIBStageID.BIBStageB:
                                {
                                    if (MT_BSM_StageB_AxisY.Busy())
                                    {
                                        MT_AxisY_StageB.FastStop();
                                    }
                                }
                                break;
                        }
                    }
                    break;
            }
        }

        public bool CheckHaveBoard(BIBStageID id, bool value = false)
        {
            return HaveBIBDetector(id, value);
        }
        #endregion



        #region 歸零流程
        private FlowChart.FCRESULT FC_BSM_HOME_Run()//Start Home
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart1_Run()//Can Home ?
        {
            if (mCanHome)
            {
                MT_AxisY_StageA.HomeReset();
                MT_AxisY_StageB.HomeReset();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart2_Run()//Hold Cylinder On
        {
            bool bHold_Cy_A_Ctrl = Hold_Cy_Ctrl(BIBStageID.BIBStageA, true);
            bool bHold_Cy_B_Ctrl = Hold_Cy_Ctrl(BIBStageID.BIBStageB, true);
            if (bHold_Cy_A_Ctrl && bHold_Cy_B_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart3_Run()//Stage A、B Home
        {
            bool b1 = MT_AxisY_StageA.Home();
            bool b2 = MT_AxisY_StageB.Home();
            if (b1 && b2)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart4_Run()//All Cylinder Off
        {
            bool bHold_Cy_A_Ctrl = Hold_Cy_Ctrl(BIBStageID.BIBStageA, false);
            bool bHold_Cy_B_Ctrl = Hold_Cy_Ctrl(BIBStageID.BIBStageB, false);
            bool bFront_Cy_A_Ctrl = Front_Cy_Ctrl(BIBStageID.BIBStageA, false);
            bool bFront_Cy_B_Ctrl = Front_Cy_Ctrl(BIBStageID.BIBStageB, false);
            bool bSide_Cy_A_Ctrl = Side_Cy_Ctrl(BIBStageID.BIBStageA, false);
            bool bSide_Cy_B_Ctrl = Side_Cy_Ctrl(BIBStageID.BIBStageB, false);
            bool bSupport_Cy_A_Ctrl = Support_Cy_Ctrl(BIBStageID.BIBStageA, false);
            bool bSupport_Cy_B_Ctrl = Support_Cy_Ctrl(BIBStageID.BIBStageB, false);

            if (bHold_Cy_A_Ctrl && bHold_Cy_B_Ctrl && bFront_Cy_A_Ctrl && bFront_Cy_B_Ctrl && bSide_Cy_A_Ctrl && bSide_Cy_B_Ctrl && bSupport_Cy_A_Ctrl && bSupport_Cy_B_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart5_Run()//Check Have BIB Remain
        {
            bool bHaveBIBDetector_A = HaveBIBDetector(BIBStageID.BIBStageA, false);//偵測StageA是否還有板
            bool bHaveBIBDetector_B = HaveBIBDetector(BIBStageID.BIBStageB, false);//偵測StageB是否還有板

            if ((bHaveBIBDetector_A && bHaveBIBDetector_B) || (Para_DryRun && Para_DryRun_NoBoard) || Para_Simulation)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            else
            {
                if (!bHaveBIBDetector_A)
                {
                    ShowAlarmMessage("E", (int)BSM_ALARM_CODE.ER_BSM_StageA_BoardRemain);
                }
                if (!bHaveBIBDetector_B)
                {
                    ShowAlarmMessage("E", (int)BSM_ALARM_CODE.ER_BSM_StageB_BoardRemain);
                }              
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart6_Run()//Done
        {
            mHomeOk = true;
            return FlowChart.FCRESULT.IDLE;
        }
        #endregion


        #region 自動流程 StageA Lock
        private FlowChart.FCRESULT FC_BSM_StageA_AUTO_LOCK_Run()//Start Lock
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart7_Run()//Need Lock ?
        {
            if (LTS.Flag_Moving.IsDoIt())
            {
                if (LTS.Action_Mode.Equals(ACTIONMODE.LOCK))
                {
                    LTS.Flag_Moving.Doing();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart33_Run()//Check Have BIB
        {
            bool bHaveBIBDetector = HaveBIBDetector(BIBStageID.BIBStageA, true);//偵測StageA是否有板

            if (bHaveBIBDetector || (Para_DryRun && Para_DryRun_NoBoard) || Para_Simulation)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            else
            {
                ShowAlarmMessage("E", (int)BSM_ALARM_CODE.ER_BSM_NoBoardInStageA);
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart8_Run()//Side Cylinder On
        {
            bool bCy_Ctrl = Side_Cy_Ctrl(BIBStageID.BIBStageA, true);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart9_Run()//Side Cylinder Off
        {
            bool bCy_Ctrl = Side_Cy_Ctrl(BIBStageID.BIBStageA, false);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart10_Run()//Front Cylinder On
        {
            bool bCy_Ctrl = Front_Cy_Ctrl(BIBStageID.BIBStageA, true);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart11_Run()//Side Cylinder On
        {
            bool bCy_Ctrl = Side_Cy_Ctrl(BIBStageID.BIBStageA, true);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart12_Run()//Hold Cylinder On
        {
            bool bCy_Ctrl = Hold_Cy_Ctrl(BIBStageID.BIBStageA, true);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart13_Run()//Stage Up(Support Cylinder On)
        {
            bool bCy_Ctrl = Support_Cy_Ctrl(BIBStageID.BIBStageA, true);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart14_Run()//Move to BasePos
        {
            APos.iPos_BSM_A_Work_HeadA = SReadValue("Pos_BSM_A_Work_HeadA").ToInt();//A手臂取放基準點
            bool b1 = MT_AxisY_StageA_MoveToPos(APos.iPos_BSM_A_Work_HeadA);
            if (b1)
            {
                LogDebug("BSM_A", "After Lock to BasePos:" + APos.iPos_BSM_A_Work_HeadA.ToString());
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart15_Run()//Done
        {
            LTS.Action_Mode = ACTIONMODE.NONE;
            LTS.Flag_Moving.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart16_Run()//Next
        {
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion


        #region 自動流程 StageA UnLock
        private FlowChart.FCRESULT FC_BSM_StageA_AUTO_UNLOCK_Run()//Start UnLock
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart18_Run()//Need UnLock ?
        {
            if (LTS.Flag_Moving.IsDoIt())
            {
                if (LTS.Action_Mode.Equals(ACTIONMODE.UNLOCK))
                {
                    LTS.Flag_Moving.Doing();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart19_Run()//Move to Reject BIB Pos
        {
            APos.iPos_BSM_A_Waiting = SReadValue("Pos_BSM_A_Waiting").ToInt();//等待點
            bool b1 = MT_AxisY_StageA_MoveToPos(APos.iPos_BSM_A_Waiting);
            if (b1)
            {
                LogDebug("BSM_A", "Before unlock move to reject pos :" + APos.iPos_BSM_A_Waiting.ToString());
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart20_Run()//Stage Down(Support Cylinder Off)
        {
            bool bCy_Ctrl = Support_Cy_Ctrl(BIBStageID.BIBStageA, false);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart21_Run()//Side Cylinder Off
        {
            bool bCy_Ctrl = Side_Cy_Ctrl(BIBStageID.BIBStageA, false);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart22_Run()//Front Cylinder Off
        {
            bool bCy_Ctrl = Front_Cy_Ctrl(BIBStageID.BIBStageA, false);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart23_Run()//Hold Cylinder Off
        {
            bool bCy_Ctrl = Hold_Cy_Ctrl(BIBStageID.BIBStageA, false);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart24_Run()//Done
        {
            LTS.Action_Mode = ACTIONMODE.NONE;
            LTS.Flag_Moving.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart25_Run()//Next
        {
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion


        #region 自動流程 StageA Move
        private FlowChart.FCRESULT FC_BSM_StageA_AUTO_MOVE_Run()//Start Move
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart26_Run()//Need Move?
        {
            if (LTS.Flag_Moving.IsDoIt())
            {
                if (LTS.Action_Mode.Equals(ACTIONMODE.MOVE))
                {
                    LTS.Flag_Moving.Doing();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart27_Run()//Move to Position
        {
            bool b1 = MT_AxisY_StageA_MoveToPos(LTS.Position);
            if (b1)
            {
                LogDebug("BSM_A", "Call to Move: " + LTS.Position.ToString());
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart28_Run()//Done
        {
            LTS.Action_Mode = ACTIONMODE.NONE;
            LTS.Flag_Moving.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart17_Run()//Next
        {
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion


        #region 自動流程 StageB Lock
        private FlowChart.FCRESULT FC_BSM_StageB_AUTO_LOCK_Run()//Start Lock
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart44_Run()//Need Lock ?
        {
            if (RTS.Flag_Moving.IsDoIt())
            {
                if (RTS.Action_Mode.Equals(ACTIONMODE.LOCK))
                {
                    RTS.Flag_Moving.Doing();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart42_Run()//Check Have BIB
        {
            bool bHaveBIBDetector = HaveBIBDetector(BIBStageID.BIBStageB, true);//偵測StageB是否有板

            if (bHaveBIBDetector || (Para_DryRun && Para_DryRun_NoBoard) || Para_Simulation)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            else
            {
                ShowAlarmMessage("E", (int)BSM_ALARM_CODE.ER_BSM_NoBoardInStageB);
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart45_Run()//Side Cylinder On
        {
            bool bCy_Ctrl = Side_Cy_Ctrl(BIBStageID.BIBStageB, true);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart46_Run()//Side Cylinder Off
        {
            bool bCy_Ctrl = Side_Cy_Ctrl(BIBStageID.BIBStageB, false);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart47_Run()//Front Cylinder On
        {
            bool bCy_Ctrl = Front_Cy_Ctrl(BIBStageID.BIBStageB, true);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart48_Run()//Side Cylinder On
        {
            bool bCy_Ctrl = Side_Cy_Ctrl(BIBStageID.BIBStageB, true);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart49_Run()//Hold Cylinder On
        {
            bool bCy_Ctrl = Hold_Cy_Ctrl(BIBStageID.BIBStageB, true);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart50_Run()//Stage Up(Support Cylinder On)
        {
            bool bCy_Ctrl = Support_Cy_Ctrl(BIBStageID.BIBStageB, true);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart51_Run()//Move to BasePos
        {
            APos.iPos_BSM_B_Work_HeadA = SReadValue("Pos_BSM_B_Work_HeadA").ToInt();//A手臂取放基準點
            bool b1 = MT_AxisY_StageB_MoveToPos(APos.iPos_BSM_B_Work_HeadA);
            if (b1)
            {
                LogDebug("BSM_B", "After Lock to BasePos:" + APos.iPos_BSM_A_Work_HeadA.ToString());
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart52_Run()//Done
        {
            RTS.Action_Mode = ACTIONMODE.NONE;
            RTS.Flag_Moving.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart43_Run()//Next
        {
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion


        #region 自動流程 StageB UnLock
        private FlowChart.FCRESULT FC_BSM_StageB_AUTO_UNLOCK_Run()//Start UnLock
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart35_Run()//Need UnLock ?
        {
            if (RTS.Flag_Moving.IsDoIt())
            {
                if (RTS.Action_Mode.Equals(ACTIONMODE.UNLOCK))
                {
                    RTS.Flag_Moving.Doing();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart36_Run()//Move to Reject BIB Pos
        {
            APos.iPos_BSM_B_Waiting = SReadValue("Pos_BSM_B_Waiting").ToInt();//等待點
            bool b1 = MT_AxisY_StageB_MoveToPos(APos.iPos_BSM_B_Waiting);
            if (b1)
            {
                LogDebug("BSM_B", "Before unlock move to reject pos :" + APos.iPos_BSM_A_Waiting.ToString());
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart37_Run()//Stage Down(Support Cylinder Off)
        {
            bool bCy_Ctrl = Support_Cy_Ctrl(BIBStageID.BIBStageB, false);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart38_Run()//Side Cylinder Off
        {
            bool bCy_Ctrl = Side_Cy_Ctrl(BIBStageID.BIBStageB, false);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart39_Run()//Front Cylinder Off
        {
            bool bCy_Ctrl = Front_Cy_Ctrl(BIBStageID.BIBStageB, false);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart40_Run()//Hold Cylinder Off
        {
            bool bCy_Ctrl = Hold_Cy_Ctrl(BIBStageID.BIBStageB, false);
            if (bCy_Ctrl)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart41_Run()//Done
        {
            RTS.Action_Mode = ACTIONMODE.NONE;
            RTS.Flag_Moving.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart34_Run()//Next
        {
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion


        #region 自動流程 StageB Move
        private FlowChart.FCRESULT FC_BSM_StageB_AUTO_MOVE_Run()//Start Move
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart30_Run()//Need Move?
        {
            if (RTS.Flag_Moving.IsDoIt())
            {
                if (RTS.Action_Mode.Equals(ACTIONMODE.MOVE))
                {
                    RTS.Flag_Moving.Doing();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart31_Run()//Move to Position
        {
            bool b1 = MT_AxisY_StageB_MoveToPos(RTS.Position);
            if (b1)
            {
                LogDebug("BSM_B", "Call to Move: " + LTS.Position.ToString());
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart32_Run()//Done
        {
            RTS.Action_Mode = ACTIONMODE.NONE;
            RTS.Flag_Moving.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart29_Run()//Next
        {
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion


        #region 馬達按鈕
        private void button1_Click(object sender, EventArgs e)
        {
            SetSpeed(true);
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            if (btn != null)
            {
                KCSDK.MotorJogForm.MotorJog.Run(btn.Parent);
            }
            SetSpeed();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetSpeed(true);
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            if (btn != null)
            {
                KCSDK.MotorJogForm.MotorJog.Run(btn.Parent);
            }
            SetSpeed();
        }
        #endregion

        private void tpHome_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ServoOn();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ServoOff();
        }

        private FlowChart.FCRESULT flowChart53_Run()
        {
            APos.iPos_BSM_A_Waiting = SReadValue("Pos_BSM_A_Waiting").ToInt();//等待點
            APos.iPos_BSM_B_Waiting = SReadValue("Pos_BSM_B_Waiting").ToInt();//等待點
            bool b1 = MT_AxisY_StageA_MoveToPos(APos.iPos_BSM_A_Waiting);
            bool b2 = MT_AxisY_StageB_MoveToPos(APos.iPos_BSM_B_Waiting);
            if (b1 && b2)
            {
                //LogDebug("BSM_A", "Before unlock move to reject pos :" + APos.iPos_BSM_A_Waiting.ToString());
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        



        


    }
}

