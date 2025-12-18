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
using System.Threading;

namespace HDT
{
    [Flags]
    public enum PP_FAILURE_ACTION_TYPE
    {
        ppNone = 0,
        ppRetry = 1,
        ppIgnore = 2,
        ppSearch = 4,
        ppALL = ppNone | ppRetry | ppIgnore | ppSearch
    }

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
    public partial class HDT : BaseModuleInterface, DHT_interface
    {
        private enum NozzleTask
        {
            CyAct,
            VacuumOn,
            VacuunOff,
            VacuumValueOn,
            VacuunValueOff,
            DestoryOn,
            DestoryOff,
            DetectResidue,
        }

        public struct HeadTargetPos
        {
            public int X;
            public int U;
            public int Z;
            public int CCDZ;
        }

        public struct OffsetInfo
        {
            public int BIBStageA_X;
            public int BIBStageA_Pick_Z;
            public int BIBStageA_Place_Z;

            public int BIBStageB_X;
            public int BIBStageB_Pick_Z;
            public int BIBStageB_Place_Z;

            public int TransferShuttleA_X;
            public int TransferShuttleA_Pick_Z;
            public int TransferShuttleA_Place_Z;

            public int TransferShuttleB_X;
            public int TransferShuttleB_Pick_Z;
            public int TransferShuttleB_Place_Z;
        }


        private enum HDT_ALARM_CODE
        {
            ER_HDT_Unknow = 0,
            ER_NozzleDetectResidue = 1,                //手臂{0}吸嘴偵測到殘料 {1}
            ER_VacuumValueOn = 2,                      //手臂{0}吸取時真空建立異常 {1}  
            ER_ICLost = 3,                             //手臂{0}放置前判斷真空建立異常，材料遺失 {0}
            ER_Protection_AxisZNotInSafePos = 4,       //手臂{0}高度安全保護啟動，請確認Z軸高度位於安全位置

            Warning_CheckByManual = 90,                //請選擇執行動作

            //CCDAlarm
            ER_CCD_ConnectionFailure = 30,             //E 手臂{0} CCD 連線異常

            ER_CCD_VisionResetFail = 31,               //E 手臂{0} CCD 通訊失敗：Reset Vision
            ER_CCD_VisionResetTimeout = 32,            //E 手臂{0} CCD 通訊逾時：Reset Vision
            ER_CCD_RunInspectFail_Position = 33,       //E 手臂{0} CCD 通訊失敗：Run Inspect (Position)
            ER_CCD_RunInspectTimeout_Position = 34,    //E 手臂{0} CCD 通訊逾時：Run Inspect (Position)
            ER_CCD_RunInspectFail_IC = 35,             //E 手臂{0} CCD 通訊失敗：Run Inspect (IC)
            ER_CCD_RunInspectTimeout_IC = 36,          //E 手臂{0} CCD 通訊逾時：Run Inspect (IC)
            ER_CCD_GetResultFail_Position = 37,        //E 手臂{0} CCD 通訊失敗：Get Result (Position)
            ER_CCD_GetResultTimeout_Position = 38,     //E 手臂{0} CCD 通訊逾時：Get Result (Position)
            ER_CCD_GetResultFail_IC = 39,              //E 手臂{0} CCD 通訊失敗：Get Result (IC)
            ER_CCD_GetResultTimeout_IC = 40,           //E 手臂{0} CCD 通訊逾時：Get Result (IC)
            ER_CCD_GetResultFail_2D = 41,              //E 手臂{0} CCD 通訊失敗：Get Result (2D)
            ER_CCD_GetResultTimeout_2D = 42,           //E 手臂{0} CCD 通訊逾時：Get Result (2D)
            ER_CCD_RunInspectFail_2D = 43,             //E 手臂{0} CCD 通訊失敗：Run Inspect (2D)
            ER_CCD_RunInspectTimeout_2D = 44,          //E 手臂{0} CCD 通訊逾時：Run Inspect (2D)
        }

        #region 流程參數
        public bool ZHomeDone = false;
        private JActionFlag HDTA_flag_ACTION;
        private JActionFlag HDTB_flag_ACTION;
        private ACTIONMODE HDTA_ActionMode;
        private ACTIONMODE HDTB_ActionMode;
        private HeadTargetPos HDTA_TargetPos;
        private HeadTargetPos HDTB_TargetPos;
        private PnPStation HDTA_PnPStation;
        private PnPStation HDTB_PnPStation;
        private ZOffset Pick;
        //視覺用
        private int BoardCount = 0;
        private int RowIndex = 0;
        private int ColIndex = 0;

        //private MyTimer HOMETimer;     //歸零流程使用Timer
        private MyTimer HDTA_AutoTimer;//手臂A自動流程使用Timer
        private MyTimer HDTB_AutoTimer;//手臂B自動流程使用Timer
        private MyTimer CCDA_AutoTimer;//CCDA自動流程使用Timer
        private MyTimer CCDB_AutoTimer;//CCDB自動流程使用Timer

        private bool bNotUseTopHead = false;
        private bool bNotUseBottomHead = false;
        private bool bEnableTopHeadCCD = false;
        private bool bEnableBottomHeadCCD = false;
        #endregion

        #region TrayDataSheet

        public struct TrayPackage
        {
            public int XN { get; set; }                 //X 數量
            public int YN { get; set; }                 //Y 數量
            public int XPitch { get; set; }             //X Pitch
            public int YPitch { get; set; }             //Y Pitch
            public int XOffset { get; set; }            //X Offset
            public int YOffset { get; set; }            //Y Offset
            public int Stack_Thickness { get; set; }    //盤厚度(不包含擋牆, 堆疊單盤高度)
            //public int Real_Thickness { get; set; }     //實際盤厚度
            //public int Depth { get; set; }              //盤深度
            //public int DeviceThickness { get; set; }    //材料厚度
            public int DeviceWidth { get; set; }        //IC寬
            public int DeviceHeight { get; set; }       //IC長
            public int Tray_Dir { get; set; }           //Pin 1 方向
            public int Tray_Notch { get; set; }         //
        }

        public struct TrayZInfo
        {
            public int Thickness { get; set; }          //盤厚度
            public int Depth { get; set; }              //盤深度
            public int DeviceThickness { get; set; }    //材料厚度
        }

        public struct ZOffset
        {
            public int HDTA_ZOffset;
            public int HDTB_ZOffset;
        }
        #endregion

        #region Package、Option、Basic Value
        public struct PackageValue
        {//產品參數待確認
            #region 盤資訊
            public TrayPackage TrayDataSheet;
            public TrayZInfo Shuttle;
            public TrayZInfo Board;
            #endregion

            public int Pos_CCD_Focus_Z; //CCD Z軸對焦位置 //!兩隻手臂用同一個點位嗎
            public int Pos_CCD_Focus_Z_HDTA_StageA;
            public int Pos_CCD_Focus_Z_HDTA_StageB;
            public int Pos_CCD_Focus_Z_HDTB_StageA;
            public int Pos_CCD_Focus_Z_HDTB_StageB;

            #region 吸嘴動作參數
            public int VacuumActionTime;
            public int BeforeDestoryDelay;
            public int DestoryActionTime;
            public int AfterDestoryDelay;
            #endregion

            #region 位置補償
            public int offset_HDTA_BIBStageA_X;
            public int offset_HDTA_BIBStageA_Pick_Z;
            public int offset_HDTA_BIBStageA_Place_Z;
            public int offset_HDTA_BIBStageB_X;
            public int offset_HDTA_BIBStageB_Pick_Z;
            public int offset_HDTA_BIBStageB_Place_Z;

            public int offset_HDTA_TransferShuttleA_X;
            public int offset_HDTA_TransferShuttleA_Pick_Z;
            public int offset_HDTA_TransferShuttleA_Place_Z;
            public int offset_HDTA_TransferShuttleB_X;
            public int offset_HDTA_TransferShuttleB_Pick_Z;
            public int offset_HDTA_TransferShuttleB_Place_Z;


            public int offset_HDTB_BIBStageA_X;
            public int offset_HDTB_BIBStageA_Pick_Z;
            public int offset_HDTB_BIBStageA_Place_Z;
            public int offset_HDTB_BIBStageB_X;
            public int offset_HDTB_BIBStageB_Pick_Z;
            public int offset_HDTB_BIBStageB_Place_Z;

            public int offset_HDTB_TransferShuttleA_X;
            public int offset_HDTB_TransferShuttleA_Pick_Z;
            public int offset_HDTB_TransferShuttleA_Place_Z;
            public int offset_HDTB_TransferShuttleB_X;
            public int offset_HDTB_TransferShuttleB_Pick_Z;
            public int offset_HDTB_TransferShuttleB_Place_Z;

            #endregion

            #region 2段速
            public int Distance_DT_HDT_BIB_SlowSpeed_Dis_KSM;
            public int Speed_DT_HDT_BIB_SlowSpeed_KSM;

            public int Distance_DT_HDT_BIB_SlowSpeed_Dis_Pick_BSM;
            public int Distance_DT_HDT_ST_SlowSpeed_Dis_Place_BSM;
            public int Speed_DT_HDT_BIB_SlowSpeed_BSM;


            #endregion

            #region Shuttle 資訊
            public int ShuttleWidth;
            #endregion

        }
        public PackageValue PValue;

        public struct OptionValue
        {
            public int MachineRate;
            public bool DryRun;
            //public bool DryRunWithoutDeiPak;
            //public bool DryRunWithoutThinChuck;
            //public bool DryRunWithoutTray;
            public bool DryRunWithoutDevice;
            public bool DryRunWithoutBoard;
            //public bool EnableTraceLog;
            //public bool EnableDebugLog;
        }
        public OptionValue OValue;

        public struct BasicValue
        {
            #region Setting


            //Accelere/Speed/Decelerate
            public int Speed_HDTA_X;
            public int Speed_HDTA_U;
            public int Speed_HDTA_Z;
            public int Speed_HDTA_CCDZ;
            public int Acc_HDTA_X;
            public int Acc_HDTA_U;
            public int Acc_HDTA_Z;
            public int Acc_HDTA_CCDZ;

            public int Speed_HDTB_X;
            public int Speed_HDTB_U;
            public int Speed_HDTB_Z;
            public int Speed_HDTB_CCDZ;
            public int Acc_HDTB_X;
            public int Acc_HDTB_U;
            public int Acc_HDTB_Z;
            public int Acc_HDTB_CCDZ;

            #endregion

            #region 點位

            //Base Pos
            public int Pos_HDTA_BoardStageA_X;
            public int Pos_HDTA_BoardStageA_Z;
            public int Pos_HDTA_BoardStageB_X;
            public int Pos_HDTA_BoardStageB_Z;
            public int Pos_HDTA_TransferShuttleA_X;
            public int Pos_HDTA_TransferShuttleA_Z;
            public int Pos_HDTA_TransferShuttleB_X;
            public int Pos_HDTA_TransferShuttleB_Z;
            public int Pos_HDTA_SortBox_Pass_X;
            public int Pos_HDTA_SortBox_Fail_X;
            public int Pos_HDTA_SortBox_Z;

            public int Pos_HDTB_BoardStageA_X;
            public int Pos_HDTB_BoardStageA_Z;
            public int Pos_HDTB_BoardStageB_X;
            public int Pos_HDTB_BoardStageB_Z;
            public int Pos_HDTB_TransferShuttleA_X;
            public int Pos_HDTB_TransferShuttleA_Z;
            public int Pos_HDTB_TransferShuttleB_X;
            public int Pos_HDTB_TransferShuttleB_Z;
            public int Pos_HDTB_SortBox_Pass_X;
            public int Pos_HDTB_SortBox_Fail_X;
            public int Pos_HDTB_SortBox_Z;


            //Bound
            public int Pos_HDTA_BoardStageA_LeftBound;
            public int Pos_HDTA_BoardStageA_RightBound;
            public int Pos_HDTA_BoardStageB_LeftBound;
            public int Pos_HDTA_BoardStageB_RightBound;
            public int Pos_HDTA_TransferShuttleA_LeftBound;
            public int Pos_HDTA_TransferShuttleA_RightBound;
            public int Pos_HDTA_TransferShuttleB_LeftBound;
            public int Pos_HDTA_TransferShuttleB_RightBound;

            public int Pos_HDTB_BoardStageA_LeftBound;
            public int Pos_HDTB_BoardStageA_RightBound;
            public int Pos_HDTB_BoardStageB_LeftBound;
            public int Pos_HDTB_BoardStageB_RightBound;
            public int Pos_HDTB_TransferShuttleA_LeftBound;
            public int Pos_HDTB_TransferShuttleA_RightBound;
            public int Pos_HDTB_TransferShuttleB_LeftBound;
            public int Pos_HDTB_TransferShuttleB_RightBound;



            public int Pos_HDTA_Safe_Z;
            public int Pos_HDTA_Nozzle_CCD_Offset;
            public int Pos_HDTA_Nozzle_CCD_OffsetY;
            public int Pos_HDTA_RotateResolution;

            public int Pos_HDTB_Safe_Z;
            public int Pos_HDTB_Nozzle_CCD_Offset;
            public int Pos_HDTB_Nozzle_CCD_OffsetY;
            public int Pos_HDTB_RotateResolution;

            #endregion
        }
        public BasicValue SValue;
        #endregion

        #region 模組固定參數
        private const int NOZZLE_NUM_X = 1;
        private const int NOZZLE_NUM_Y = 1;
        #endregion

        #region 吸嘴元件

        private NozzleArray HDTA_Nozzles = null;
        private NozzleArray HDTB_Nozzles = null;

        #endregion

        #region 錯誤處理頁面

        private Thread th_ShowErrorSelectForm;
        private bool StopShowErrorSelectForm;
        private bool RunShowErrorSelectForm;
        private PP_FAILURE_ACTION_TYPE PPFAReturn = PP_FAILURE_ACTION_TYPE.ppNone;      //吸嘴真空異常處置方式回傳值
        private PP_FAILURE_ACTION_TYPE PPFAFormType = PP_FAILURE_ACTION_TYPE.ppNone;    //吸嘴真空異常處置方式選擇表單類型

        #endregion

        #region 初始化用
        private bool ModuleInitiated = true;
        #endregion


        public HDT()
        {
            InitializeComponent();
            CreateComponentList();

            HDTA_Nozzles = new NozzleArray(NOZZLE_NUM_X, NOZZLE_NUM_Y);
            HDTB_Nozzles = new NozzleArray(NOZZLE_NUM_X, NOZZLE_NUM_Y);
            PValue.Shuttle = new TrayZInfo();
            PValue.Board = new TrayZInfo();

            HDTA_flag_ACTION = new JActionFlag();
            HDTB_flag_ACTION = new JActionFlag();
            HDTA_ActionMode = new ACTIONMODE();
            HDTB_ActionMode = new ACTIONMODE();
            HDTA_TargetPos = new HeadTargetPos();
            HDTB_TargetPos = new HeadTargetPos();
            HDTA_PnPStation = new PnPStation();
            HDTB_PnPStation = new PnPStation();

            HDTA_AutoTimer = new MyTimer();
            HDTB_AutoTimer = new MyTimer();
            HDTA_AutoTimer.AutoReset = true;
            HDTB_AutoTimer.AutoReset = true;


            th_ShowErrorSelectForm = new Thread(ShowErrorSelectForm);
            th_ShowErrorSelectForm.Start();

        }


        #region 繼承函數

        //模組解構使用
        public override void DisposeTH()
        {
            StopShowErrorSelectForm = true;
            th_ShowErrorSelectForm.Join();
            base.DisposeTH();
        }

        //程式初始化
        public override void Initial()
        {
            InitialNozzles();
            HDTA_Nozzles.Simulation = IsSimulation() != 0;
            HDTB_Nozzles.Simulation = IsSimulation() != 0;

            MT_HDT_A_AxisX.IsSimulation = IsSimulation();
            MT_HDT_A_AxisZ.IsSimulation = IsSimulation();
            MT_HDT_A_AxisU.IsSimulation = IsSimulation();
            MT_CCD_A_AxisZ.IsSimulation = IsSimulation();

            MT_HDT_B_AxisX.IsSimulation = IsSimulation();
            MT_HDT_B_AxisZ.IsSimulation = IsSimulation();
            MT_HDT_B_AxisU.IsSimulation = IsSimulation();
            MT_CCD_B_AxisZ.IsSimulation = IsSimulation();
        }

        //持續偵測函數
        public override void AlwaysRun() //持續掃描
        {
            if (ModuleInitiated)
            {
                ModuleInitiated = false;
                LoadBasicData();
                LoadOptionData();
            }
            else //安全保護
            {
                //Z軸安全保護
                if (MT_HDT_A_AxisX.Busy() || MT_HDT_A_AxisU.Busy())
                {
                    if (IsAxisZSafety(BoardHeadID.HDT_A).Equals(false))
                    {
                        KCSDK.MotorJogForm.MotorJog.StopJog();
                        ShowAlarm("E", (int)HDT_ALARM_CODE.ER_Protection_AxisZNotInSafePos, "A");
                    }
                }
                if (MT_HDT_B_AxisX.Busy() || MT_HDT_B_AxisU.Busy())
                {
                    if (IsAxisZSafety(BoardHeadID.HDT_B).Equals(false))
                    {
                        KCSDK.MotorJogForm.MotorJog.StopJog();
                        ShowAlarm("E", (int)HDT_ALARM_CODE.ER_Protection_AxisZNotInSafePos, "B");
                    }
                }
            }
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

            ZHomeDone = false;

            HDTA_flag_ACTION.Reset();
            HDTB_flag_ACTION.Reset();
            HDTA_ActionMode = ACTIONMODE.NONE;
            HDTB_ActionMode = ACTIONMODE.NONE;
            HDTA_PnPStation = PnPStation.NONE;
            HDTB_PnPStation = PnPStation.NONE;

            BoardCount = 0;
            ColIndex = 0;
            RowIndex = 0;

            MT_HDT_A_AxisX.HomeReset();
            MT_HDT_A_AxisU.HomeReset();
            MT_HDT_A_AxisZ.HomeReset();
            MT_CCD_A_AxisZ.HomeReset();
            MT_HDT_B_AxisX.HomeReset();
            MT_HDT_B_AxisU.HomeReset();
            MT_HDT_B_AxisZ.HomeReset();
            MT_CCD_B_AxisZ.HomeReset();

            ServoOn();

            FC_HDT_HOME.TaskReset();
        }
        public override bool Home() //歸零
        {
            FC_HDT_HOME.MainRun();
            return mHomeOk;
        }

        //運轉相關函數
        public override void ServoOn()//Motor Servo On
        {
            MT_HDT_A_AxisX.ServoOn(true);
            MT_HDT_A_AxisU.ServoOn(true);
            MT_HDT_A_AxisZ.ServoOn(true);
            MT_CCD_A_AxisZ.ServoOn(true);

            MT_HDT_B_AxisX.ServoOn(true);
            MT_HDT_B_AxisU.ServoOn(true);
            MT_HDT_B_AxisZ.ServoOn(true);
            MT_CCD_B_AxisZ.ServoOn(true);
        }
        public override void ServoOff()//Motor Servo Off
        {
            MT_HDT_A_AxisX.ServoOn(false);
            MT_HDT_A_AxisU.ServoOn(false);
            MT_HDT_A_AxisZ.ServoOn(false);
            MT_CCD_A_AxisZ.ServoOn(false);

            MT_HDT_B_AxisX.ServoOn(false);
            MT_HDT_B_AxisU.ServoOn(false);
            MT_HDT_B_AxisZ.ServoOn(false);
            MT_CCD_B_AxisZ.ServoOn(false);
        }
        public override void RunReset() //運轉前初始化
        {
            FC_HDT_A_AUTO_Pick.TaskReset();
            FC_HDT_A_AUTO_Place.TaskReset();
            FC_HDT_A_AUTO_Move.TaskReset();
            FC_HDT_A_AUTO_Inspection.TaskReset();

            FC_HDT_B_AUTO_Pick.TaskReset();
            FC_HDT_B_AUTO_Place.TaskReset();
            FC_HDT_B_AUTO_Move.TaskReset();
            FC_HDT_B_AUTO_Inspection.TaskReset();
        }
        public override void Run() //運轉
        {
            FC_HDT_A_AUTO_Pick.MainRun();
            FC_HDT_A_AUTO_Place.MainRun();
            FC_HDT_A_AUTO_Move.MainRun();
            FC_HDT_A_AUTO_Inspection.MainRun();

            FC_HDT_B_AUTO_Pick.MainRun();
            FC_HDT_B_AUTO_Place.MainRun();
            FC_HDT_B_AUTO_Move.MainRun();
            FC_HDT_B_AUTO_Inspection.MainRun();
        }
        public override void SetSpeed(bool bHome = false) //速度設定
        {//!速度要再確認
            double rate = OValue.MachineRate;

            int speed = bHome ? 10000 : (int)(SValue.Speed_HDTA_X * rate) / 100;
            int acc = bHome ? 100000 : (int)(SValue.Acc_HDTA_X * speed);
            MT_HDT_A_AxisX.SetSpeed(speed);
            MT_HDT_A_AxisX.SetAcceleration(acc);
            MT_HDT_A_AxisX.SetDeceleration(acc);

            speed = bHome ? 10000 : (int)(SValue.Speed_HDTA_U * rate) / 100;
            acc = bHome ? 100000 : (int)(SValue.Acc_HDTA_U * speed);
            MT_HDT_A_AxisU.SetSpeed(speed);
            MT_HDT_A_AxisU.SetAcceleration(acc);
            MT_HDT_A_AxisU.SetDeceleration(acc);

            speed = bHome ? 10000 : (int)(SValue.Speed_HDTA_Z * rate) / 100;
            acc = bHome ? 100000 : (int)(SValue.Acc_HDTA_Z * speed);
            MT_HDT_A_AxisZ.SetSpeed(speed);
            MT_HDT_A_AxisZ.SetAcceleration(acc);
            MT_HDT_A_AxisZ.SetDeceleration(acc);

            speed = bHome ? 10000 : (int)(SValue.Speed_HDTA_CCDZ * rate) / 100;
            acc = bHome ? 100000 : (int)(SValue.Acc_HDTA_CCDZ * speed);
            MT_CCD_A_AxisZ.SetSpeed(speed);
            MT_CCD_A_AxisZ.SetAcceleration(acc);
            MT_CCD_A_AxisZ.SetDeceleration(acc);


            speed = bHome ? 10000 : (int)(SValue.Speed_HDTB_X * rate) / 100;
            acc = bHome ? 100000 : (int)(SValue.Acc_HDTB_X * speed);
            MT_HDT_B_AxisX.SetSpeed(speed);
            MT_HDT_B_AxisX.SetAcceleration(acc);
            MT_HDT_B_AxisX.SetDeceleration(acc);

            speed = bHome ? 10000 : (int)(SValue.Speed_HDTB_U * rate) / 100;
            acc = bHome ? 100000 : (int)(SValue.Acc_HDTB_U * speed);
            MT_HDT_B_AxisU.SetSpeed(speed);
            MT_HDT_B_AxisU.SetAcceleration(acc);
            MT_HDT_B_AxisU.SetDeceleration(acc);

            speed = bHome ? 10000 : (int)(SValue.Speed_HDTB_Z * rate) / 100;
            acc = bHome ? 100000 : (int)(SValue.Acc_HDTB_Z * speed);
            MT_HDT_B_AxisZ.SetSpeed(speed);
            MT_HDT_B_AxisZ.SetAcceleration(acc);
            MT_HDT_B_AxisZ.SetDeceleration(acc);

            speed = bHome ? 10000 : (int)(SValue.Speed_HDTB_CCDZ * rate) / 100;
            acc = bHome ? 100000 : (int)(SValue.Acc_HDTB_CCDZ * speed);
            MT_CCD_B_AxisZ.SetSpeed(speed);
            MT_CCD_B_AxisZ.SetAcceleration(acc);
            MT_CCD_B_AxisZ.SetDeceleration(acc);

        }

        //手動相關函數
        public override void ManualReset()//手動運行前置設定
        {

        }
        public override void ManualRun() //手動模式運行
        {

        }

        //停止所有馬達
        public override void StopMotor()
        {
            base.StopMotor();
            foreach (Motor motor in MotorList)
            {
                motor.Stop();
            }
        }

        public override void AfterSaveParam()
        {
            base.AfterSaveParam();
            LoadBasicData();
        }

        #endregion


        #region 使用者函數
        public ThreeValued IsHeadUse(BoardHeadID id)
        {
            ThreeValued T = ThreeValued.UNKNOWN;
            switch (id)
            {
                case BoardHeadID.HDT_A:
                    {
                        T = ThreeValued.TRUE;
                        if (bNotUseTopHead) T = ThreeValued.FALSE;
                    }
                    break;
                case BoardHeadID.HDT_B:
                    {
                        T = ThreeValued.TRUE;
                        if (bNotUseBottomHead) T = ThreeValued.FALSE;
                    }
                    break;
                default:
                    { 
                        //UNKNOWN
                    }
                    break;
            }
            return T;
        }
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
            LoadBasicData();
            LoadOptionData();
            LoadPackageData();

            //ZHomeDone = false;

            //HDTA_flag_ACTION.Reset();
            //HDTB_flag_ACTION.Reset();
            //HDTA_ActionMode = ACTIONMODE.NONE;
            //HDTB_ActionMode = ACTIONMODE.NONE;
            //HDTA_PnPStation = PnPStation.NONE;
            //HDTB_PnPStation = PnPStation.NONE;

            //BoardCount = 0;
            //ColIndex = 0;
            //RowIndex = 0;
        }

        public ThreeValued IsAxisZHomeOK()
        {
            ThreeValued tRet = ThreeValued.FALSE;
            if (GetUseModule().Equals(false))
                tRet = ThreeValued.TRUE;
                //return true;

            if (ZHomeDone) tRet = ThreeValued.TRUE;
            //return ZHomeDone;
            return tRet;
        }

        /// <summary>
        /// 判斷Z軸是否安全
        /// /<returns></returns>
        public bool IsAxisZSafety(BoardHeadID id)
        {
            if (GetUseModule().Equals(false) || IsSimulation() != 0)
                return true;

            if (id == BoardHeadID.HDT_A)
            {
                int AxisZ_Pos = MT_HDT_A_AxisZ.ReadEncPos();
                if (AxisZ_Pos < SValue.Pos_HDTA_Safe_Z - 100 || MT_HDT_A_AxisZ.IsHomeOn.Equals(true))
                {
                    return false;
                }
            }
            else if (id == BoardHeadID.HDT_B)
            {
                int AxisZ_Pos = MT_HDT_B_AxisZ.ReadEncPos();
                if (AxisZ_Pos < SValue.Pos_HDTB_Safe_Z - 100 || MT_HDT_B_AxisZ.IsHomeOn.Equals(true))
                {
                    return false;
                }
            }
            return true;
        }

        public ThreeValued SetActionCommand_HDT(BoardHeadID id, ACTIONMODE mode, BasePosInfo pos)
        {
            ThreeValued bRet = ThreeValued.UNKNOWN;

            if (this.GetUseModule().Equals(false))
            {
                return ThreeValued.TRUE;
            }

            if (id == BoardHeadID.HDT_A)
            {
                if (HDTA_flag_ACTION.IsDoing())
                {
                    return ThreeValued.FALSE;
                }
                SetSpeed_Normal(id);

                HDTA_ActionMode = mode;
                HDTA_TargetPos.X = pos.X;
                HDTA_TargetPos.U = pos.U;
                HDTA_TargetPos.Z = pos.Z;
                BoardCount = pos.BoardCount;
                ColIndex = pos.ColIndex;
                RowIndex = pos.RowIndex;

                HDTA_flag_ACTION.DoIt();

                bRet = ThreeValued.TRUE;
            }

            else if (id == BoardHeadID.HDT_B)
            {
                if (HDTB_flag_ACTION.IsDoing())
                {
                    return ThreeValued.FALSE;
                }
                SetSpeed_Normal(id);

                HDTB_ActionMode = mode;
                HDTB_TargetPos.X = pos.X;
                HDTB_TargetPos.U = pos.U;
                HDTB_TargetPos.Z = pos.Z;
                HDTB_flag_ACTION.DoIt();

                BoardCount = pos.BoardCount;
                ColIndex = pos.ColIndex;
                RowIndex = pos.RowIndex;

                bRet = ThreeValued.TRUE;
            }

            //string msg = string.Format("BoardHeadID:{0},PnPMode{1},TargetPos.X{2},TargetPos.U{3},TargetPos.Z{4}"
            //    , id.ToString(), mode.ToString(), pos.X.ToString(), pos.U.ToString(), pos.Z.ToString());
            //LogTrace(msg);

            if (bRet.Equals(ThreeValued.TRUE))
            {
                string msg = string.Format("BoardHeadID:{0},PnPMode{1},TargetPos.X{2},TargetPos.U{3},TargetPos.Z{4}"
                    , id.ToString(), mode.ToString(), pos.X.ToString(), pos.U.ToString(), pos.Z.ToString());
                LogDebug("SetCommand_HDT : " + msg);
            }

            return bRet;
        }

        public ThreeValued GetActionResult_HDT(BoardHeadID id)
        {
            ThreeValued bRet = ThreeValued.UNKNOWN;

            if (GetUseModule().Equals(false))
            {
                return ThreeValued.TRUE;
            }

            if (id == BoardHeadID.HDT_A)
            {
                if (HDTA_flag_ACTION.IsDone())
                {
                    bRet = ThreeValued.TRUE;
                }
                else
                {
                    bRet = ThreeValued.FALSE;
                }
            }

            else if (id == BoardHeadID.HDT_B)
            {
                if (HDTB_flag_ACTION.IsDone())
                {
                    bRet = ThreeValued.TRUE;
                }
                else
                {
                    bRet = ThreeValued.FALSE;
                }
            }

            if (bRet.Equals(ThreeValued.TRUE))
            {
                LogDebug(string.Format("GetResult HDT ({0})  is true: ", id.ToString()));
            }

            return bRet;
        }

        public BasePosInfo GetBasicInfo(PnPStation station, BoardHeadID id)
        {
            BasePosInfo pos = new BasePosInfo();

            switch (station)
            {
                case PnPStation.BOARD_A:
                    {
                        pos.X = id.Equals(BoardHeadID.HDT_A) ? SValue.Pos_HDTA_BoardStageA_X : SValue.Pos_HDTB_BoardStageA_X;
                        pos.Y = 0;
                        pos.Z = id.Equals(BoardHeadID.HDT_A) ? SValue.Pos_HDTA_BoardStageA_Z : SValue.Pos_HDTB_BoardStageA_Z;
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        pos.X = id.Equals(BoardHeadID.HDT_A) ? SValue.Pos_HDTA_BoardStageB_X : SValue.Pos_HDTB_BoardStageB_X;
                        pos.Y = 0;
                        pos.Z = id.Equals(BoardHeadID.HDT_A) ? SValue.Pos_HDTA_BoardStageB_Z : SValue.Pos_HDTB_BoardStageB_Z;
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        pos.X = id.Equals(BoardHeadID.HDT_A) ? SValue.Pos_HDTA_TransferShuttleA_X : SValue.Pos_HDTB_TransferShuttleA_X;
                        pos.Y = 0;
                        pos.Z = id.Equals(BoardHeadID.HDT_A) ? SValue.Pos_HDTA_TransferShuttleA_Z : SValue.Pos_HDTB_TransferShuttleA_Z;
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        pos.X = id.Equals(BoardHeadID.HDT_A) ? SValue.Pos_HDTA_TransferShuttleB_X : SValue.Pos_HDTB_TransferShuttleB_X;
                        pos.Y = 0;
                        pos.Z = id.Equals(BoardHeadID.HDT_A) ? SValue.Pos_HDTA_TransferShuttleB_Z : SValue.Pos_HDTB_TransferShuttleB_Z;
                    }
                    break;
                case PnPStation.PassBox:
                    {
                        pos.X = id.Equals(BoardHeadID.HDT_A) ? SValue.Pos_HDTA_SortBox_Pass_X : SValue.Pos_HDTB_SortBox_Pass_X;
                        pos.Y = 0;
                        pos.Z = id.Equals(BoardHeadID.HDT_A) ? SValue.Pos_HDTA_SortBox_Z : SValue.Pos_HDTB_SortBox_Z;
                    }
                    break;
                case PnPStation.BinBox:
                    {
                        pos.X = id.Equals(BoardHeadID.HDT_A) ? SValue.Pos_HDTA_SortBox_Fail_X : SValue.Pos_HDTB_SortBox_Fail_X;
                        pos.Y = 0;
                        pos.Z = id.Equals(BoardHeadID.HDT_A) ? SValue.Pos_HDTA_SortBox_Z : SValue.Pos_HDTB_SortBox_Z;
                    }
                    break;
            }
            return pos;
        }

        public bool SetNozzleState(NozzleState[,] state, BoardHeadID id)
        {
            switch (id)
            {
                case BoardHeadID.HDT_A:
                    {
                        return HDTA_Nozzles.SetNozzlesState(state);
                    }
                    
                case BoardHeadID.HDT_B:
                    {
                        return HDTB_Nozzles.SetNozzlesState(state);
                    }
                    
            }
            return false;
        }

        public NozzleState[,] GetNozzleState(BoardHeadID id)
        {
            NozzleState[,] nozzlestate = new NozzleState[NOZZLE_NUM_X, NOZZLE_NUM_Y];
            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                { 
                    if (id.Equals(BoardHeadID.HDT_A))
                    {
                        nozzlestate[x, y] = HDTA_Nozzles[x, y].State;
                    }
                    else if (id.Equals(BoardHeadID.HDT_B))
                    {
                        nozzlestate[x, y] = HDTB_Nozzles[x, y].State;
                    }
                }
            }
            return nozzlestate;
        }

        public NozzleState GetNozzleState2(BoardHeadID id, int NOZZLE_NUM_X, int NOZZLE_NUM_Y)//獲取單一吸嘴狀態
        {
            NozzleState nzl_state = new NozzleState();

            if (id == BoardHeadID.HDT_A)
            {
                nzl_state = HDTA_Nozzles[NOZZLE_NUM_X, NOZZLE_NUM_Y].State;
            }
            else if (id == BoardHeadID.HDT_B)
            {
                nzl_state = HDTB_Nozzles[NOZZLE_NUM_X, NOZZLE_NUM_Y].State;
            }
            return nzl_state;
        }

        public void SetHeadAStation(PnPStation station)
        {
            HDTA_PnPStation = station;
        }

        public void SetHeadBStation(PnPStation station)
        {
            HDTB_PnPStation = station;
        }

        public void SetPickOffsetZ(BoardHeadID id,PnPStation station)
        {
            if (id == BoardHeadID.HDT_A)
            {
                switch (station)
                {
                    case PnPStation.BOARD_A:
                        Pick.HDTA_ZOffset = PValue.offset_HDTA_BIBStageA_Pick_Z;
                        break;
                    case PnPStation.BOARD_B:
                        Pick.HDTA_ZOffset = PValue.offset_HDTA_BIBStageB_Pick_Z;
                        break;
                    case PnPStation.KIT_SHUTTLE_A:
                        Pick.HDTA_ZOffset = PValue.offset_HDTA_TransferShuttleA_Pick_Z;
                        break;
                    case PnPStation.KIT_SHUTTLE_B:
                        Pick.HDTA_ZOffset = PValue.offset_HDTA_TransferShuttleB_Pick_Z;
                        break;
                }
            }
            else if (id == BoardHeadID.HDT_B)
            {
                switch (station)
                {
                    case PnPStation.BOARD_A:
                        Pick.HDTB_ZOffset = PValue.offset_HDTB_BIBStageA_Pick_Z;
                        break;
                    case PnPStation.BOARD_B:
                        Pick.HDTB_ZOffset = PValue.offset_HDTB_BIBStageB_Pick_Z;
                        break;
                    case PnPStation.KIT_SHUTTLE_A:
                        Pick.HDTB_ZOffset = PValue.offset_HDTB_TransferShuttleA_Pick_Z;
                        break;
                    case PnPStation.KIT_SHUTTLE_B:
                        Pick.HDTB_ZOffset = PValue.offset_HDTB_TransferShuttleB_Pick_Z;
                        break;
                }
            }
        }

        #endregion

        

        #region 私有函數
        private void LogTrace(string msg)
        {
            if (GetUseModule())
                JLogger.LogTrace("Trace", "HDT | " + msg);
        }
        private void LogDebug(string msg)
        {
            if (GetUseModule())
                JLogger.LogDebug("Debug", "HDT | " + msg);
        }

        private void ShowErrorSelectForm()
        {
            while (StopShowErrorSelectForm.Equals(false))
            {
                while (RunShowErrorSelectForm)
                {
                    RunShowErrorSelectForm = false;
                    PPFailureActionSelectForm PPFASelectForm = new PPFailureActionSelectForm();
                    PPFASelectForm.ShowPPFASelectForm(PPFAFormType);
                    PPFAReturn = PPFASelectForm.PPFailureActionResult;
                    PPFAFormType = PP_FAILURE_ACTION_TYPE.ppNone;
                    PPFASelectForm.Close();
                }
                Thread.Sleep(20);
            }
        }
        private void CallPPFailureForm(PP_FAILURE_ACTION_TYPE type)
        {
            PPFAReturn = PP_FAILURE_ACTION_TYPE.ppNone;
            if (PPFAFormType.Equals(PP_FAILURE_ACTION_TYPE.ppNone))
            {
                PPFAFormType = type;
                RunShowErrorSelectForm = true;
            }
        }

        private void InitialNozzles()
        {
            HDTA_Nozzles.InitialNozzle(0, 0, null, null, null, IB_HDT_A_VacDetect_1, OB_HDT_A_Vacuum_1, OB_HDT_A_Destroy_1);
            HDTA_Nozzles.InitialNozzle(0, 1, null, null, null, IB_HDT_A_VacDetect_2, OB_HDT_A_Vacuum_2, OB_HDT_A_Destroy_2);
            HDTA_Nozzles.InitialNozzle(0, 2, null, null, null, IB_HDT_A_VacDetect_3, OB_HDT_A_Vacuum_3, OB_HDT_A_Destroy_3);
            HDTA_Nozzles.InitialNozzle(0, 3, null, null, null, IB_HDT_A_VacDetect_4, OB_HDT_A_Vacuum_4, OB_HDT_A_Destroy_4);

            HDTB_Nozzles.InitialNozzle(0, 0, null, null, null, IB_HDT_B_VacDetect_1, OB_HDT_B_Vacuum_1, OB_HDT_B_Destroy_1);
            HDTB_Nozzles.InitialNozzle(0, 1, null, null, null, IB_HDT_B_VacDetect_2, OB_HDT_B_Vacuum_2, OB_HDT_B_Destroy_2);
            HDTB_Nozzles.InitialNozzle(0, 2, null, null, null, IB_HDT_B_VacDetect_3, OB_HDT_B_Vacuum_3, OB_HDT_B_Destroy_3);
            HDTB_Nozzles.InitialNozzle(0, 3, null, null, null, IB_HDT_B_VacDetect_4, OB_HDT_B_Vacuum_4, OB_HDT_B_Destroy_4);
        }
        /// <summary>
        /// 重置吸嘴的激活狀態
        /// </summary>
        /// <param name="state">-1:停用, 0:不使用, 1:使用中</param>
        private bool ResetNozzleState(BoardHeadID id, NozzleState state)
        {
            NozzleState[,] nzl_state = new NozzleState[NOZZLE_NUM_X, NOZZLE_NUM_Y];
            for (uint x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (uint y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    nzl_state[x, y] = state;
                }
            }

            if (id == BoardHeadID.HDT_A)
            {
                return HDTA_Nozzles.SetNozzlesState(nzl_state);
            }
            else if (id == BoardHeadID.HDT_B)
            {
                return HDTB_Nozzles.SetNozzlesState(nzl_state);
            }
            return false;
        }
        /// <summary>
        /// 重置吸嘴的狀態
        /// </summary>
        /// <param name="ori_state">欲改變吸嘴狀態</param>
        /// <param name="target_state">改變後吸嘴狀態</param>
        /// <returns></returns>
        private bool SetNozzleStateToTarget(BoardHeadID id, NozzleState ori_state, NozzleState target_state)
        {
            bool bRet = true;
            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    if (id == BoardHeadID.HDT_A)
                    {
                        if (HDTA_Nozzles[x, y].State.Equals(ori_state))
                        {
                            HDTA_Nozzles[x, y].State = target_state;
                        }
                    }
                    else if (id == BoardHeadID.HDT_B)
                    {
                        if (HDTB_Nozzles[x, y].State.Equals(ori_state))
                        {
                            HDTB_Nozzles[x, y].State = target_state;
                        }
                    }
                }
            }
            return bRet;
        }

        
        private void LoadOptionData()
        {//!通用設定待確認
            OValue.MachineRate = OReadValue("機台速率").ToInt();
            OValue.DryRun = OReadValue("DryRun").ToBoolean();
            OValue.DryRunWithoutBoard = OReadValue("DryRun_NoBoard").ToBoolean();
            OValue.DryRunWithoutDevice = OReadValue("DryRun_NoDevice").ToBoolean();
            //OValue.DryRunWithoutTray = OReadValue("DryRun_NoBoard").ToBoolean() || OValue.DryRun;
            //OValue.DryRunWithoutDevice = OReadValue("DryRun_NoDevice").ToBoolean() || OValue.DryRunWithoutTray;

        }
        private void LoadBasicData()
        {
            #region Setting

            //Accelere/Speed/Decelerate
            SValue.Speed_HDTA_X = SReadValue("MT_HDT_HeadA_X_SPEED").ToInt();
            SValue.Speed_HDTA_U = SReadValue("MT_HDT_HeadA_U_SPEED").ToInt();
            SValue.Speed_HDTA_Z = SReadValue("MT_HDT_HeadA_Z_SPEED").ToInt();
            SValue.Speed_HDTA_CCDZ = SReadValue("MT_HDT_CCDA_Z_SPEED").ToInt();
            SValue.Acc_HDTA_X = SReadValue("MT_HDT_HeadA_X_ACC_MULTIPLE").ToInt();
            SValue.Acc_HDTA_U = SReadValue("MT_HDT_HeadA_U_ACC_MULTIPLE").ToInt();
            SValue.Acc_HDTA_Z = SReadValue("MT_HDT_HeadA_Z_ACC_MULTIPLE").ToInt();
            SValue.Acc_HDTA_CCDZ = SReadValue("MT_HDT_CCDA_Z_ACC_MULTIPLE").ToInt();

            SValue.Speed_HDTB_X = SReadValue("MT_HDT_HeadB_X_SPEED").ToInt();
            SValue.Speed_HDTB_U = SReadValue("MT_HDT_HeadB_U_SPEED").ToInt();
            SValue.Speed_HDTB_Z = SReadValue("MT_HDT_HeadB_Z_SPEED").ToInt();
            SValue.Speed_HDTB_CCDZ = SReadValue("MT_HDT_CCDB_Z_SPEED").ToInt();
            SValue.Acc_HDTB_X = SReadValue("MT_HDT_HeadB_X_ACC_MULTIPLE").ToInt();
            SValue.Acc_HDTB_U = SReadValue("MT_HDT_HeadB_U_ACC_MULTIPLE").ToInt();
            SValue.Acc_HDTB_Z = SReadValue("MT_HDT_HeadB_Z_ACC_MULTIPLE").ToInt();
            SValue.Acc_HDTB_CCDZ = SReadValue("MT_HDT_CCDB_Z_ACC_MULTIPLE").ToInt();

            #endregion

            #region 點位
            //Base Pos
            SValue.Pos_HDTA_BoardStageA_X = SReadValue("Pos_HDT_A_BoardStageA_X").ToInt();
            SValue.Pos_HDTA_BoardStageA_Z = SReadValue("Pos_HDT_A_BoardStageA_Z").ToInt();
            SValue.Pos_HDTA_BoardStageB_X = SReadValue("Pos_HDT_A_BoardStageB_X").ToInt();
            SValue.Pos_HDTA_BoardStageB_Z = SReadValue("Pos_HDT_A_BoardStageB_Z").ToInt();
            SValue.Pos_HDTA_TransferShuttleA_X = SReadValue("Pos_HDT_A_TransferShuttleA_X").ToInt();
            SValue.Pos_HDTA_TransferShuttleA_Z = SReadValue("Pos_HDT_A_TransferShuttleA_Z").ToInt();
            SValue.Pos_HDTA_TransferShuttleB_X = SReadValue("Pos_HDT_A_TransferShuttleB_X").ToInt();
            SValue.Pos_HDTA_TransferShuttleB_Z = SReadValue("Pos_HDT_A_TransferShuttleB_Z").ToInt();
            SValue.Pos_HDTA_SortBox_Pass_X = SReadValue("Pos_HDT_A_SortBox_Pass_X").ToInt();
            SValue.Pos_HDTA_SortBox_Fail_X = SReadValue("Pos_HDT_A_SortBox_Fail_X").ToInt();
            SValue.Pos_HDTA_SortBox_Z = SReadValue("Pos_HDT_A_SortBox_Z").ToInt();

            SValue.Pos_HDTB_BoardStageA_X = SReadValue("Pos_HDT_B_BoardStageA_X").ToInt();
            SValue.Pos_HDTB_BoardStageA_Z = SReadValue("Pos_HDT_B_BoardStageA_Z").ToInt();
            SValue.Pos_HDTB_BoardStageB_X = SReadValue("Pos_HDT_B_BoardStageB_X").ToInt();
            SValue.Pos_HDTB_BoardStageB_Z = SReadValue("Pos_HDT_B_BoardStageB_Z").ToInt();
            SValue.Pos_HDTB_TransferShuttleA_X = SReadValue("Pos_HDT_B_TransferShuttleA_X").ToInt();
            SValue.Pos_HDTB_TransferShuttleA_Z = SReadValue("Pos_HDT_B_TransferShuttleA_Z").ToInt();
            SValue.Pos_HDTB_TransferShuttleB_X = SReadValue("Pos_HDT_B_TransferShuttleB_X").ToInt();
            SValue.Pos_HDTB_TransferShuttleB_Z = SReadValue("Pos_HDT_B_TransferShuttleB_Z").ToInt();
            SValue.Pos_HDTB_SortBox_Pass_X = SReadValue("Pos_HDT_B_SortBox_Pass_X").ToInt();
            SValue.Pos_HDTB_SortBox_Fail_X = SReadValue("Pos_HDT_B_SortBox_Fail_X").ToInt();
            SValue.Pos_HDTB_SortBox_Z = SReadValue("Pos_HDT_B_SortBox_Z").ToInt();



            //Bound
            SValue.Pos_HDTA_BoardStageA_LeftBound = SReadValue("Pos_HDT_A_BoardStageA_LeftBound").ToInt();
            SValue.Pos_HDTA_BoardStageA_RightBound = SReadValue("Pos_HDT_A_BoardStageA_RightBound").ToInt();
            SValue.Pos_HDTA_BoardStageB_LeftBound = SReadValue("Pos_HDT_A_BoardStageB_LeftBound").ToInt();
            SValue.Pos_HDTA_BoardStageB_RightBound = SReadValue("Pos_HDT_A_BoardStageB_RightBound").ToInt();
            SValue.Pos_HDTA_TransferShuttleA_LeftBound = SReadValue("Pos_HDT_A_TransferShuttleA_LeftBound").ToInt();
            SValue.Pos_HDTA_TransferShuttleA_RightBound = SReadValue("Pos_HDT_A_TransferShuttleA_RightBound").ToInt();
            SValue.Pos_HDTA_TransferShuttleB_LeftBound = SReadValue("Pos_HDT_A_TransferShuttleB_LeftBound").ToInt();
            SValue.Pos_HDTA_TransferShuttleB_RightBound = SReadValue("Pos_HDT_A_TransferShuttleB_RightBound").ToInt();

            SValue.Pos_HDTB_BoardStageA_LeftBound = SReadValue("Pos_HDT_B_BoardStageA_LeftBound").ToInt();
            SValue.Pos_HDTB_BoardStageA_RightBound = SReadValue("Pos_HDT_B_BoardStageA_RightBound").ToInt();
            SValue.Pos_HDTB_BoardStageB_LeftBound = SReadValue("Pos_HDT_B_BoardStageB_LeftBound").ToInt();
            SValue.Pos_HDTB_BoardStageB_RightBound = SReadValue("Pos_HDT_B_BoardStageB_RightBound").ToInt();
            SValue.Pos_HDTB_TransferShuttleA_LeftBound = SReadValue("Pos_HDT_B_TransferShuttleA_LeftBound").ToInt();
            SValue.Pos_HDTB_TransferShuttleA_RightBound = SReadValue("Pos_HDT_B_TransferShuttleA_RightBound").ToInt();
            SValue.Pos_HDTB_TransferShuttleB_LeftBound = SReadValue("Pos_HDT_B_TransferShuttleB_LeftBound").ToInt();
            SValue.Pos_HDTB_TransferShuttleB_RightBound = SReadValue("Pos_HDT_B_TransferShuttleB_RightBound").ToInt();



            SValue.Pos_HDTA_Safe_Z = SReadValue("Pos_HDT_A_Safe_Z").ToInt();
            SValue.Pos_HDTA_Nozzle_CCD_Offset = SReadValue("Pos_HDT_A_Nozzle_CCD_Offset").ToInt();
            SValue.Pos_HDTA_Nozzle_CCD_OffsetY = SReadValue("Pos_HDT_A_Nozzle_CCD_OffsetY").ToInt();
            SValue.Pos_HDTA_RotateResolution = SReadValue("Pos_HDT_A_RotateResolution").ToInt();

            SValue.Pos_HDTB_Safe_Z = SReadValue("Pos_HDT_B_Safe_Z").ToInt();
            SValue.Pos_HDTB_Nozzle_CCD_Offset = SReadValue("Pos_HDT_B_Nozzle_CCD_Offset").ToInt();
            SValue.Pos_HDTB_Nozzle_CCD_OffsetY = SReadValue("Pos_HDT_B_Nozzle_CCD_OffsetY").ToInt();
            SValue.Pos_HDTB_RotateResolution = SReadValue("Pos_HDT_B_RotateResolution").ToInt();

            #endregion

            #region 設定
            bNotUseTopHead = SReadValue("DoNotUseTopHead").ToBoolean();
            bNotUseBottomHead = SReadValue("DoNotUseBottomHead").ToBoolean();
            bEnableTopHeadCCD = SReadValue("EnableTopHeadCCD").ToBoolean();
            bEnableBottomHeadCCD = SReadValue("EnableBottomHeadCCD").ToBoolean();

            #endregion 
        }
        private void LoadPackageData()
        {

            //CCD對焦
            PValue.Pos_CCD_Focus_Z = PReadValue("HDTA_STAGEA_CCD_Focus_Z").ToInt();
            PValue.Pos_CCD_Focus_Z_HDTA_StageA = PReadValue("HDTA_STAGEA_CCD_Focus_Z").ToInt();
            PValue.Pos_CCD_Focus_Z_HDTA_StageB = PReadValue("HDTA_STAGEB_CCD_Focus_Z").ToInt();
            PValue.Pos_CCD_Focus_Z_HDTB_StageA = PReadValue("HDTB_STAGEA_CCD_Focus_Z").ToInt();
            PValue.Pos_CCD_Focus_Z_HDTB_StageB = PReadValue("HDTB_STAGEB_CCD_Focus_Z").ToInt();

            //吸嘴動作參數
            PValue.VacuumActionTime = PReadValue("DT_HDT_BIB_VAC").ToInt();
            PValue.BeforeDestoryDelay = PReadValue("DT_HDT_BIB_DES_BF").ToInt();
            PValue.DestoryActionTime = PReadValue("DT_HDT_BIB_DES").ToInt();
            PValue.AfterDestoryDelay = PReadValue("DT_HDT_BIB_DES_AF").ToInt();

            //位置補償
            PValue.offset_HDTA_BIBStageA_X = PReadValue("Offset_HDTA_BIB_A_X").ToInt();
            PValue.offset_HDTA_BIBStageA_Pick_Z = PReadValue("Offset_HDTA_BIB_A_Z_PICKUP").ToInt();
            PValue.offset_HDTA_BIBStageA_Place_Z = PReadValue("Offset_HDTA_BIB_A_Z_PLACE").ToInt();
            PValue.offset_HDTA_BIBStageB_X = PReadValue("Offset_HDTA_BIB_B_X").ToInt();
            PValue.offset_HDTA_BIBStageB_Pick_Z = PReadValue("Offset_HDTA_BIB_B_Z_PICKUP").ToInt();
            PValue.offset_HDTA_BIBStageB_Place_Z = PReadValue("Offset_HDTA_BIB_B_Z_PLACE").ToInt();

            PValue.offset_HDTA_TransferShuttleA_X = PReadValue("Offset_HDTA_BIB_A_KIT_X").ToInt();
            PValue.offset_HDTA_TransferShuttleA_Pick_Z = PReadValue("Offset_HDTA_KITA_Z_PICKUP").ToInt();
            PValue.offset_HDTA_TransferShuttleA_Place_Z = PReadValue("Offset_HDTA_KITA_Z_PLACE").ToInt();
            PValue.offset_HDTA_TransferShuttleB_X = PReadValue("Offset_HDTA_BIB_B_KIT_X").ToInt();
            PValue.offset_HDTA_TransferShuttleB_Pick_Z = PReadValue("Offset_HDTA_KITB_Z_PICKUP").ToInt();
            PValue.offset_HDTA_TransferShuttleB_Place_Z = PReadValue("Offset_HDTA_KITB_Z_PLACE").ToInt();


            PValue.offset_HDTB_BIBStageA_X = PReadValue("Offset_HDTB_BIB_A_X").ToInt();
            PValue.offset_HDTB_BIBStageA_Pick_Z = PReadValue("Offset_HDTB_BIB_A_Z_PICKUP").ToInt();
            PValue.offset_HDTB_BIBStageA_Place_Z = PReadValue("Offset_HDTB_BIB_A_Z_PLACE").ToInt();
            PValue.offset_HDTB_BIBStageB_X = PReadValue("Offset_HDTB_BIB_B_X").ToInt();
            PValue.offset_HDTB_BIBStageB_Pick_Z = PReadValue("Offset_HDTB_BIB_B_Z_PICKUP").ToInt();
            PValue.offset_HDTB_BIBStageB_Place_Z = PReadValue("Offset_HDTB_BIB_B_Z_PLACE").ToInt();

            PValue.offset_HDTB_TransferShuttleA_X = PReadValue("Offset_HDTB_BIB_A_KIT_X").ToInt();
            PValue.offset_HDTB_TransferShuttleA_Pick_Z = PReadValue("Offset_HDTB_KITA_Z_PICKUP").ToInt();
            PValue.offset_HDTB_TransferShuttleA_Place_Z = PReadValue("Offset_HDTB_KITA_Z_PLACE").ToInt();
            PValue.offset_HDTB_TransferShuttleB_X = PReadValue("Offset_HDTB_BIB_B_KIT_X").ToInt();
            PValue.offset_HDTB_TransferShuttleB_Pick_Z = PReadValue("Offset_HDTB_KITB_Z_PICKUP").ToInt();
            PValue.offset_HDTB_TransferShuttleB_Place_Z = PReadValue("Offset_HDTB_KITB_Z_PLACE").ToInt();

            //2段速
            PValue.Distance_DT_HDT_BIB_SlowSpeed_Dis_KSM = PReadValue("DT_HDT_BIB_SlowSpeed_Dis_KSM").ToInt();
            PValue.Speed_DT_HDT_BIB_SlowSpeed_KSM = PReadValue("DT_HDT_BIB_SlowSpeed_KSM").ToInt();

            PValue.Distance_DT_HDT_BIB_SlowSpeed_Dis_Pick_BSM = PReadValue("DT_HDT_BIB_SlowSpeed_Dis_Pick_BSM").ToInt();
            PValue.Distance_DT_HDT_ST_SlowSpeed_Dis_Place_BSM = PReadValue("DT_HDT_ST_SlowSpeed_Dis_Place_BSM").ToInt();
            PValue.Speed_DT_HDT_BIB_SlowSpeed_BSM = PReadValue("DT_HDT_BIB_SlowSpeed_BSM").ToInt();
        }

        private bool HDTA_X_MoveToPos(int x)
        {
            if (HDTA_ActionMode.Equals(ACTIONMODE.MOVE))
            {
                if (IsAxisZSafety(BoardHeadID.HDT_A).Equals(true))
                {
                    if (IsSimulation() != 0) return true;
                    bool b1 = MT_HDT_A_AxisX.G00(x);
                    return b1;
                }
                else
                {
                    ShowAlarm("E", (int)HDT_ALARM_CODE.ER_Protection_AxisZNotInSafePos, "A");
                }
            }
            return false;
        }
        private bool HDTA_U_MoveToPos(int u)
        {
            //if (HDTA_ActionMode.Equals(ACTIONMODE.MOVE))
            {
                if (IsAxisZSafety(BoardHeadID.HDT_A).Equals(true))
                {
                    if (IsSimulation() != 0) return true;
                    bool b1 = MT_HDT_A_AxisU.G00(u);
                    return b1;
                }
                else
                {
                    ShowAlarm("E", (int)HDT_ALARM_CODE.ER_Protection_AxisZNotInSafePos, "A");
                }
            }
            return false;
        }
        private bool HDTA_Z_MoveToPos(int z)
        {
            if (HDTA_ActionMode.Equals(ACTIONMODE.PICKUP) || HDTA_ActionMode.Equals(ACTIONMODE.PLACEMENT))
            {
                if (IsSimulation() != 0) return true;
                bool b1 = MT_HDT_A_AxisZ.G00(z);
                return b1;
            }
            return false;
        }
        private bool HDTB_X_MoveToPos(int x)
        {
            //if (HDTB_ActionMode.Equals(ACTIONMODE.MOVE))
            {
                if (IsAxisZSafety(BoardHeadID.HDT_B).Equals(true))
                {
                    if (IsSimulation() != 0) return true;
                    bool b1 = MT_HDT_B_AxisX.G00(x);
                    return b1;
                }
                else
                {
                    ShowAlarm("E", (int)HDT_ALARM_CODE.ER_Protection_AxisZNotInSafePos, "B");
                }
            }
            return false;
        }
        private bool HDTB_U_MoveToPos(int u)
        {
            //if (HDTB_ActionMode.Equals(ACTIONMODE.MOVE))
            {
                if (IsAxisZSafety(BoardHeadID.HDT_B).Equals(true))
                {
                    if (IsSimulation() != 0) return true;
                    bool b1 = MT_HDT_B_AxisU.G00(u);
                    return b1;
                }
                else
                {
                    ShowAlarm("E", (int)HDT_ALARM_CODE.ER_Protection_AxisZNotInSafePos, "B");
                }
            }
            return false;
        }
        private bool HDTB_Z_MoveToPos(int z)
        {
            if (HDTB_ActionMode.Equals(ACTIONMODE.PICKUP) || HDTB_ActionMode.Equals(ACTIONMODE.PLACEMENT))
            {
                if (IsSimulation() != 0) return true;
                bool b1 = MT_HDT_B_AxisZ.G00(z);
                return b1;
            }
            return false;
        }

        private void SetSpeed_Normal(BoardHeadID id) //單一手臂 速度設定為常速
        {//!速度要再確認
            double rate = OValue.MachineRate;

            if (id == BoardHeadID.HDT_A)
            {
                int speed = (int)(SValue.Speed_HDTA_X * rate) / 100;
                int acc = (int)(SValue.Acc_HDTA_X * speed);
                MT_HDT_A_AxisX.SetSpeed(speed);
                MT_HDT_A_AxisX.SetAcceleration(acc);
                MT_HDT_A_AxisX.SetDeceleration(acc);

                speed = (int)(SValue.Speed_HDTA_U * rate) / 100;
                acc = (int)(SValue.Acc_HDTA_U * speed);
                MT_HDT_A_AxisU.SetSpeed(speed);
                MT_HDT_A_AxisU.SetAcceleration(acc);
                MT_HDT_A_AxisU.SetDeceleration(acc);

                speed = (int)(SValue.Speed_HDTA_Z * rate) / 100;
                acc = (int)(SValue.Acc_HDTA_Z * speed);
                MT_HDT_A_AxisZ.SetSpeed(speed);
                MT_HDT_A_AxisZ.SetAcceleration(acc);
                MT_HDT_A_AxisZ.SetDeceleration(acc);

                speed = (int)(SValue.Speed_HDTA_CCDZ * rate) / 100;
                acc = (int)(SValue.Acc_HDTA_CCDZ * speed);
                MT_CCD_A_AxisZ.SetSpeed(speed);
                MT_CCD_A_AxisZ.SetAcceleration(acc);
                MT_CCD_A_AxisZ.SetDeceleration(acc);
            }
            else if (id == BoardHeadID.HDT_B)
            {
                int speed = (int)(SValue.Speed_HDTB_X * rate) / 100;
                int acc = (int)(SValue.Acc_HDTB_X * speed);
                MT_HDT_B_AxisX.SetSpeed(speed);
                MT_HDT_B_AxisX.SetAcceleration(acc);
                MT_HDT_B_AxisX.SetDeceleration(acc);

                speed = (int)(SValue.Speed_HDTB_U * rate) / 100;
                acc = (int)(SValue.Acc_HDTB_U * speed);
                MT_HDT_B_AxisU.SetSpeed(speed);
                MT_HDT_B_AxisU.SetAcceleration(acc);
                MT_HDT_B_AxisU.SetDeceleration(acc);

                speed = (int)(SValue.Speed_HDTB_Z * rate) / 100;
                acc = (int)(SValue.Acc_HDTB_Z * speed);
                MT_HDT_B_AxisZ.SetSpeed(speed);
                MT_HDT_B_AxisZ.SetAcceleration(acc);
                MT_HDT_B_AxisZ.SetDeceleration(acc);

                speed = (int)(SValue.Speed_HDTB_CCDZ * rate) / 100;
                acc = (int)(SValue.Acc_HDTB_CCDZ * speed);
                MT_CCD_B_AxisZ.SetSpeed(speed);
                MT_CCD_B_AxisZ.SetAcceleration(acc);
                MT_CCD_B_AxisZ.SetDeceleration(acc);
            }
        }

        #endregion


        #region 公用函數
        public bool SetVisionFocus(BoardHeadID Hid, BIBStageID Bid)
        {
            bool bRet = false;

            if (!bUseCCD(Hid)) return true;

            switch (Hid)
            {
                case BoardHeadID.HDT_A:
                    {
                        switch (Bid)
                        {
                            case BIBStageID.BIBStageA:
                                {
                                    bRet = MT_CCD_A_AxisZ.G00(PValue.Pos_CCD_Focus_Z_HDTA_StageA);
                                }
                                break;
                            case BIBStageID.BIBStageB:
                                {
                                    bRet = MT_CCD_A_AxisZ.G00(PValue.Pos_CCD_Focus_Z_HDTA_StageB);
                                }
                                break;
                        }
                    }
                    break;
                case BoardHeadID.HDT_B:
                    {
                        switch (Bid)
                        {
                            case BIBStageID.BIBStageA:
                                {
                                    bRet = MT_CCD_A_AxisZ.G00(PValue.Pos_CCD_Focus_Z_HDTA_StageA);
                                }
                                break;
                            case BIBStageID.BIBStageB:
                                {
                                    bRet = MT_CCD_A_AxisZ.G00(PValue.Pos_CCD_Focus_Z_HDTA_StageB);
                                }
                                break;
                        }
                    }
                    break;
            }

            return bRet;
        }

        public HDTLocation GetHDTLocation(BoardHeadID id)
        {
            if (!GetUseModule() || IsSimulation() != 0)
            {
                return HDTLocation.SAFE;
            }

            HDTLocation Ret = HDTLocation.SAFE;
            switch (id)
            {
                case BoardHeadID.HDT_A:
                    {
                        int AxisXCurrentPos = MT_HDT_A_AxisX.ReadEncPos();
                        if (SValue.Pos_HDTA_BoardStageA_LeftBound <= AxisXCurrentPos && AxisXCurrentPos <= SValue.Pos_HDTA_BoardStageA_RightBound)
                        {
                            return HDTLocation.LEFTBOARD;
                        }
                        if (SValue.Pos_HDTA_BoardStageB_LeftBound <= AxisXCurrentPos && AxisXCurrentPos <= SValue.Pos_HDTA_BoardStageB_RightBound)
                        {
                            return HDTLocation.RIGHTBOARD;
                        }
                        if (SValue.Pos_HDTA_TransferShuttleA_LeftBound <= AxisXCurrentPos && AxisXCurrentPos <= SValue.Pos_HDTA_TransferShuttleA_RightBound)
                        {
                            return HDTLocation.LEFTKIT;
                        }
                        if (SValue.Pos_HDTA_TransferShuttleB_LeftBound <= AxisXCurrentPos && AxisXCurrentPos <= SValue.Pos_HDTA_TransferShuttleB_RightBound)
                        {
                            return HDTLocation.RIGHTKIT;
                        }
                        if (SValue.Pos_HDTA_TransferShuttleA_RightBound <= AxisXCurrentPos && AxisXCurrentPos <= SValue.Pos_HDTA_TransferShuttleB_LeftBound)
                        {
                            return HDTLocation.MIDDLEKIT;
                        }
                    }
                    break;
                case BoardHeadID.HDT_B:
                    {
                        int AxisXCurrentPos = MT_HDT_B_AxisX.ReadEncPos();
                        if (SValue.Pos_HDTB_BoardStageA_LeftBound <= AxisXCurrentPos && AxisXCurrentPos <= SValue.Pos_HDTB_BoardStageA_RightBound)
                        {
                            return HDTLocation.LEFTBOARD;
                        }
                        if (SValue.Pos_HDTB_BoardStageB_LeftBound <= AxisXCurrentPos && AxisXCurrentPos <= SValue.Pos_HDTB_BoardStageB_RightBound)
                        {
                            return HDTLocation.RIGHTBOARD;
                        }
                        if (SValue.Pos_HDTB_TransferShuttleA_LeftBound <= AxisXCurrentPos && AxisXCurrentPos <= SValue.Pos_HDTB_TransferShuttleA_RightBound)
                        {
                            return HDTLocation.LEFTKIT;
                        }
                        if (SValue.Pos_HDTB_TransferShuttleB_LeftBound <= AxisXCurrentPos && AxisXCurrentPos <= SValue.Pos_HDTB_TransferShuttleB_RightBound)
                        {
                            return HDTLocation.RIGHTKIT;
                        }
                        if (SValue.Pos_HDTB_TransferShuttleA_RightBound <= AxisXCurrentPos && AxisXCurrentPos <= SValue.Pos_HDTB_TransferShuttleB_LeftBound)
                        {
                            return HDTLocation.MIDDLEKIT;
                        }
                    }
                    break;
            }
            return Ret;
        }
        #endregion





        #region 歸零流程
        private FlowChart.FCRESULT FC_HDT_HOME_Run()//Start Home
        {
            if (mCanHome)
            {
                MT_HDT_A_AxisZ.HomeReset();
                MT_CCD_A_AxisZ.HomeReset();
                MT_HDT_B_AxisZ.HomeReset();
                MT_CCD_B_AxisZ.HomeReset();

                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart1_Run()//Z Home (Include CCD Z)
        {
            bool b1 = MT_HDT_A_AxisZ.Home();
            bool b2 = MT_CCD_A_AxisZ.Home();
            bool b3 = MT_HDT_B_AxisZ.Home();
            bool b4 = MT_CCD_B_AxisZ.Home();

            if (b1 && b2 && b3 && b4)
            {
                MT_HDT_A_AxisX.HomeReset();
                MT_HDT_A_AxisU.HomeReset();
                MT_HDT_B_AxisX.HomeReset();
                MT_HDT_B_AxisU.HomeReset();

                ZHomeDone = true;

                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart2_Run()//X & U Home
        {
            bool b1 = MT_HDT_A_AxisX.Home();
            bool b2 = MT_HDT_A_AxisU.Home();
            bool b3 = MT_HDT_B_AxisX.Home();
            bool b4 = MT_HDT_B_AxisU.Home();

            if (b1 && b2 && b3 && b4)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart3_Run()//Vacuum On
        {
            ResetNozzleState(BoardHeadID.HDT_A, NozzleState.InUsing);
            ResetNozzleState(BoardHeadID.HDT_B, NozzleState.InUsing);

            bool b1 = HDTA_Nozzles.NozzleVacuumOn(NozzleState.InUsing, PValue.VacuumActionTime);
            bool b2 = HDTB_Nozzles.NozzleVacuumOn(NozzleState.InUsing, PValue.VacuumActionTime);

            if (OValue.DryRunWithoutDevice || (b1 && b2))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart4_Run()//Detect IC Remain
        {
            ThreeValued[,] HDTA_tvRet = HDTA_Nozzles.NozzleVacuumValueOff(NozzleState.InUsing);
            ThreeValued[,] HDTB_tvRet = HDTB_Nozzles.NozzleVacuumValueOff(NozzleState.InUsing);

            bool bRet = true;
            if (IsSimulation() == 0 && OValue.DryRunWithoutDevice.Equals(false))
            {
                for (int x = 0; x < NOZZLE_NUM_X; x++)
                {
                    for (int y = 0; y < NOZZLE_NUM_Y; y++)
                    {
                        string errMsg = string.Format("[{0},{1}]", x + 1, y + 1);
                        if (HDTA_tvRet[x, y].Equals(ThreeValued.FALSE))
                        {
                            ShowAlarm("E", (int)HDT_ALARM_CODE.ER_NozzleDetectResidue, "A", errMsg);
                            bRet = false;
                        }
                        if (HDTB_tvRet[x, y].Equals(ThreeValued.FALSE))
                        {
                            ShowAlarm("E", (int)HDT_ALARM_CODE.ER_NozzleDetectResidue, "B", errMsg);
                            bRet = false;
                        }
                    }
                }
            }

            if (bRet)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart5_Run()//Vacuum Off
        {
            if (OValue.DryRunWithoutDevice)
            {
                return FlowChart.FCRESULT.NEXT;
            }

            bool b1 = HDTA_Nozzles.NozzleVacuumOff(NozzleState.InUsing, PValue.VacuumActionTime);
            bool b2 = HDTB_Nozzles.NozzleVacuumOff(NozzleState.InUsing, PValue.VacuumActionTime);

            if (b1 && b2)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart6_Run()//Destory On
        {
            if (OValue.DryRunWithoutDevice)
            {
                return FlowChart.FCRESULT.NEXT;
            }

            bool b1 = HDTA_Nozzles.NozzleDestroyOn(NozzleState.InUsing, PValue.DestoryActionTime);
            bool b2 = HDTB_Nozzles.NozzleDestroyOn(NozzleState.InUsing, PValue.DestoryActionTime);

            if (b1 && b2)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart7_Run()//Destory Off
        {
            if (OValue.DryRunWithoutDevice)
            {
                return FlowChart.FCRESULT.NEXT;
            }

            bool b1 = HDTA_Nozzles.NozzleDestroyOff(NozzleState.InUsing, PValue.DestoryActionTime);
            bool b2 = HDTB_Nozzles.NozzleDestroyOff(NozzleState.InUsing, PValue.DestoryActionTime);

            if (b1 && b2)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart8_Run()//Vision Set CCD goto ready
        {
            bool b1 = IsSimulation() != 0 || MT_CCD_A_AxisZ.G00(PValue.Pos_CCD_Focus_Z_HDTA_StageA);
            bool b2 = IsSimulation() != 0 || MT_CCD_B_AxisZ.G00(PValue.Pos_CCD_Focus_Z_HDTB_StageA);

            if (b1 && b2)
            {
                HDT_A_ClientSocket.IP = SReadValue("HDTA_CcdIP").ToString();
                HDT_A_ClientSocket.Port = SReadValue("HDTA_CcdPortNo").ToInt();
                HDT_A_ClientSocket.Connect();
                HDT_B_ClientSocket.IP = SReadValue("HDTB_CcdIP").ToString();
                HDT_B_ClientSocket.Port = SReadValue("HDTB_CcdPortNo").ToInt();
                HDT_B_ClientSocket.Connect();

                //HOMETimer.Restart();
                HomeTM.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart82_Run()//Check Connection state
        {
            if (IsSimulation() != 0)
            {
                return FlowChart.FCRESULT.NEXT;
            }

            if (HDT_A_ClientSocket.Socket != null && HDT_B_ClientSocket.Socket != null)
            {
                try
                {
                    bool b1 = true;
                    bool b2 =true;
                    if (bEnableTopHeadCCD)
                        b1 =  HDT_A_ClientSocket.Socket.Connected;
                    if (bEnableBottomHeadCCD)
                        b2 = HDT_B_ClientSocket.Socket.Connected;

                    if (b1 && b2)
                    {
                        JLogger.LogDebug("HDT", string.Format("Home - HDT CCD Connection Success"));
                        return FlowChart.FCRESULT.NEXT;
                    }
                    else if (HomeTM.On(1000))
                    {                      
                        if (!b1)
                        {                    
                            ShowAlarm("E", (int)HDT_ALARM_CODE.ER_CCD_ConnectionFailure, "A");
                        }
                        if (!b2)
                        {
                            ShowAlarm("E", (int)HDT_ALARM_CODE.ER_CCD_ConnectionFailure, "B");
                        }
                        HomeTM.Restart();
                    }
                }
                catch (Exception ex)
                {
                    ShowAlarm("E", (int)HDT_ALARM_CODE.ER_CCD_ConnectionFailure, "A、B");
                    HomeTM.Restart();
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart9_Run()//Done
        {
            mHomeOk = true;
            return FlowChart.FCRESULT.IDLE;
        }
        #endregion


        #region 自動流程 HDTA Pick
        private FlowChart.FCRESULT FC_HDT_A_AUTO_Pick_Run()//Start Pick
        {
            if (HDTA_flag_ACTION.IsDoIt())
            {
                if (HDTA_ActionMode.Equals(ACTIONMODE.PICKUP))
                {
                    LogDebug(string.Format("Head A Pick Start "));
                    HDTA_flag_ACTION.Doing();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart10_Run()//Axis Z to PickPos + Offset
        {
            //bool b1 = HDTA_Z_MoveToPos(HDTA_TargetPos.Z + PValue.Distance_DT_HDT_BIB_SlowSpeed_Dis_Pick_BSM);
            SetPickOffsetZ(BoardHeadID.HDT_A, HDTA_PnPStation);
            bool b1 = HDTA_Z_MoveToPos(HDTA_TargetPos.Z + PValue.Distance_DT_HDT_BIB_SlowSpeed_Dis_Pick_BSM + Pick.HDTA_ZOffset);

            if (b1)
            {
                MT_HDT_A_AxisZ.SetSpeed(PValue.Speed_DT_HDT_BIB_SlowSpeed_BSM);//轉成慢速
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart11_Run()//Axis Z to PickPos 
        {
            //bool b1 = HDTA_Z_MoveToPos(HDTA_TargetPos.Z);
            bool b1 = HDTA_Z_MoveToPos(HDTA_TargetPos.Z + Pick.HDTA_ZOffset);

            if (b1)
            {
                //SetSpeed_Normal(BoardHeadID.HDT_A);//轉回常速
                HDTA_AutoTimer.Restart();
                LogDebug(string.Format("Head A Pick Pos = {0}: ", HDTA_TargetPos.Z.ToString()));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart12_Run()//Using Vacuum On
        {
            if (OValue.DryRunWithoutDevice)
            {
                if (HDTA_AutoTimer.On(PValue.VacuumActionTime))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else if ((HDTA_Nozzles.NozzleVacuumOn((NozzleState.InUsing | NozzleState.PnPFailure), PValue.VacuumActionTime)))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart13_Run()//Axis Z to Safety
        {
            bool b1 = HDTA_Z_MoveToPos(SValue.Pos_HDTA_Safe_Z);

            if (b1)
            {
                //SetSpeed_Normal(BoardHeadID.HDT_A);//轉回常速
                LogDebug(string.Format("Head A Safe Pos = {0}: ", SValue.Pos_HDTA_Safe_Z.ToString()));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart14_Run()//Check Using Vacuum Value
        {
            bool bRet = true;
            bool[,] baRet = new bool[NOZZLE_NUM_X, NOZZLE_NUM_Y];
            List<string> alarmlist = new List<string>();

            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    if (HDTA_Nozzles[x, y].State.Equals(NozzleState.InUsing) || HDTA_Nozzles[x, y].State.Equals(NozzleState.PnPFailure))
                    {
                        baRet[x, y] = HDTA_Nozzles[x, y].VacuumValueOn;

                        if (OValue.DryRunWithoutDevice)
                        {
                            baRet[x, y] = true;
                        }

                        if (baRet[x, y])
                        {
                            HDTA_Nozzles[x, y].State = NozzleState.InUsing;
                        }
                        else
                        {
                            HDTA_Nozzles[x, y].State = NozzleState.PnPFailure;
                            bRet = bRet && false;
                            string errMsg = string.Format("[{0},{1}]", x + 1, y + 1);
                            alarmlist.Add(errMsg);
                        }
                    }
                }
            }

            if (bRet)
            {
                return FlowChart.FCRESULT.NEXT;
            }

            PP_FAILURE_ACTION_TYPE flag_failure = PP_FAILURE_ACTION_TYPE.ppNone;
            flag_failure = PP_FAILURE_ACTION_TYPE.ppIgnore | PP_FAILURE_ACTION_TYPE.ppRetry;
            CallPPFailureForm(flag_failure);

            for (int i = 0; i < alarmlist.Count; i++)
            {
                ShowAlarm("E", (int)HDT_ALARM_CODE.ER_VacuumValueOn, "A", alarmlist[i]);
            }
            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart15_Run()//Retry or Ignore
        {
            switch (PPFAReturn)
            {
                case PP_FAILURE_ACTION_TYPE.ppRetry:
                    LogTrace(string.Format("Pickup process failure. Select action = [Retry]"));
                    LogSay(EnLoggerType.EnLog_OP, string.Format("Board Head Pick Retry btn click"));
                    return FlowChart.FCRESULT.NEXT;     //重試 (取料流程 - 作用中且真空建立失敗的吸嘴氣缸下降)

                case PP_FAILURE_ACTION_TYPE.ppIgnore:
                    LogTrace("Pickup process failure. Select action = [Ignore]");
                    LogSay(EnLoggerType.EnLog_OP, string.Format("Board Head Pick Ignore btn click"));
                    return FlowChart.FCRESULT.CASE1;    //忽略 (取料流程 - 資料交換)

                //!暫時不使用search功能
                //case PP_FAILURE_ACTION_TYPE.ppSearch:

            }
            ShowAlarm("W", (int)HDT_ALARM_CODE.Warning_CheckByManual);   //Sort head pickup failure, be check devices by manual before ignore.
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart16_Run()//Vac. Failure Vac. Off
        {
            ThreeValued[,] tvRet = new ThreeValued[NOZZLE_NUM_X, NOZZLE_NUM_Y];
            if (OValue.DryRunWithoutDevice || HDTA_Nozzles.NozzleVacuumOff(NozzleState.PnPFailure))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart18_Run()//Pick Done
        {
            HDTA_ActionMode = ACTIONMODE.NONE;
            HDTA_flag_ACTION.Done();
            LogDebug(string.Format("Head A Pick Done"));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart37_Run()//Next
        {
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion


        #region 自動流程 HDTA Place
        private FlowChart.FCRESULT FC_HDT_A_AUTO_Place_Run()//Start Place
        {
            if (HDTA_flag_ACTION.IsDoIt())
            {
                if (HDTA_ActionMode.Equals(ACTIONMODE.PLACEMENT))
                {
                    HDTA_flag_ACTION.Doing();
                    LogDebug(string.Format("Head A Place Start"));
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart19_Run()//Check Using Vac.
        {
            ThreeValued[,] tvRet = HDTA_Nozzles.NozzleVacuumValueOn(NozzleState.InUsing);
            bool bRet = true;
            List<string> alarmlist = new List<string>();
            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    if (HDTA_Nozzles[x, y].State.Equals(NozzleState.InUsing))
                    {
                        if (OValue.DryRunWithoutDevice)
                        {
                            tvRet[x, y] = ThreeValued.TRUE;
                        }

                        if (tvRet[x, y].Equals(ThreeValued.FALSE))
                        {
                            HDTA_Nozzles[x, y].State = NozzleState.PnPFailure;
                            bRet = bRet && false;
                            string msg = string.Format("[{0},{1}]", x + 1, y + 1);
                            alarmlist.Add(msg);
                        }
                    }
                }
            }

            if (bRet)
            {
                return FlowChart.FCRESULT.NEXT;
            }

            CallPPFailureForm(PP_FAILURE_ACTION_TYPE.ppIgnore | PP_FAILURE_ACTION_TYPE.ppRetry);
            for (int i = 0; i < alarmlist.Count; i++)
            {
                ShowAlarm("E", (int)HDT_ALARM_CODE.ER_ICLost, "A", alarmlist[i]);
            }
            return FlowChart.FCRESULT.CASE1;

        }

        private FlowChart.FCRESULT flowChart20_Run()//Retry or Ignore
        {
            string msg = "";
            switch (PPFAReturn)
            {
                case PP_FAILURE_ACTION_TYPE.ppRetry:
                    msg = string.Format("IC Lost. Select action = [Retry]");
                    LogTrace("Place | " + msg);
                    LogSay(EnLoggerType.EnLog_OP, string.Format("Board Head Place Retry btn click"));
                    return FlowChart.FCRESULT.NEXT;     //重試 (放料流程 - 判斷作用的吸嘴是否真空建立成功,Y(N),N(1))
                //break;
                case PP_FAILURE_ACTION_TYPE.ppIgnore:
                case PP_FAILURE_ACTION_TYPE.ppSearch:
                    msg = string.Format("IC Lost. Select action = [Ignore]");
                    LogTrace("Place | " + msg);
                    LogSay(EnLoggerType.EnLog_OP, string.Format("Board Head Place Ignore btn click"));
                    return FlowChart.FCRESULT.CASE1;    //忽略 (放料流程 - IC 掉落資料交換)
                //break;
            }
            ShowAlarm("W", (int)HDT_ALARM_CODE.Warning_CheckByManual);   //Sort head pickup failure, be check devices by manual before ignore.
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart21_Run()//Set Nozzle state
        {
            SetNozzleStateToTarget(BoardHeadID.HDT_A, NozzleState.PnPFailure, NozzleState.ICLost);
            HDTA_Nozzles.NozzleVacuumOff(NozzleState.ICLost);
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart22_Run()//Retry
        {
            //放料流程 - 重試(N) - 將 ActionFailed 改為 InUsing
            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    if (HDTA_Nozzles[x, y].State.Equals(NozzleState.PnPFailure))
                    {
                        HDTA_Nozzles[x, y].State = NozzleState.InUsing;
                    }
                }
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart23_Run()//Axis Z to PlacePos + Offset
        {
            bool b1 = HDTA_Z_MoveToPos(HDTA_TargetPos.Z + PValue.Distance_DT_HDT_ST_SlowSpeed_Dis_Place_BSM);

            if (b1)
            {
                MT_HDT_A_AxisZ.SetSpeed(PValue.Speed_DT_HDT_BIB_SlowSpeed_BSM);//轉成慢速
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart24_Run()//Axis Z to PlacePos 
        {
            bool b1 = HDTA_Z_MoveToPos(HDTA_TargetPos.Z);

            if (b1)
            {
                LogDebug(string.Format("Head A Place Pos = {0}: ", HDTA_TargetPos.Z.ToString()));
                HDTA_AutoTimer.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart25_Run()//Before destory delay
        {
            if (PValue.BeforeDestoryDelay <= 0 || HDTA_AutoTimer.On(PValue.BeforeDestoryDelay))
            //if (HDTA_AutoTimer.On(500))
            {
                HDTA_AutoTimer.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart29_Run()//Destory (on -> delay -> off)
        {
            int DestoryTime = PValue.DestoryActionTime;

            if (OValue.DryRunWithoutDevice)
            {
                if (HDTA_AutoTimer.On(DestoryTime))
                {
                    HDTA_AutoTimer.Restart();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else if (HDTA_Nozzles.NozzleDestroyOn(NozzleState.InUsing, DestoryTime))
            {
                if (HDTA_Nozzles.NozzleDestroyOff(NozzleState.InUsing))
                {
                    HDTA_AutoTimer.Restart();
                    return FlowChart.FCRESULT.NEXT;
                }
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart30_Run()//After destory delay
        {
            if (PValue.AfterDestoryDelay <= 0 || HDTA_AutoTimer.On(PValue.AfterDestoryDelay))
            {
                HDTA_AutoTimer.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart31_Run()//Axis Z to PlacePos + Offset
        {
            bool b1 = HDTA_Z_MoveToPos(HDTA_TargetPos.Z + PValue.Distance_DT_HDT_ST_SlowSpeed_Dis_Place_BSM);

            if (b1)
            {
                SetSpeed_Normal(BoardHeadID.HDT_A);//轉回常速
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart32_Run()//Axis Z to Safety
        {
            bool b1 = HDTA_Z_MoveToPos(SValue.Pos_HDTA_Safe_Z);

            if (b1)
            {
                LogDebug(string.Format("Head A Place Safe Pos = {0}: ", SValue.Pos_HDTA_Safe_Z.ToString()));
                HDTA_AutoTimer.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart33_Run()//Open Vac.
        {
            if (OValue.DryRunWithoutDevice)
            {
                if (HDTA_AutoTimer.On(PValue.VacuumActionTime))
                {
                    HDTA_AutoTimer.Restart();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else if ((HDTA_Nozzles.NozzleVacuumOn((NozzleState.InUsing | NozzleState.PnPFailure | NozzleState.Unused | NozzleState.ICLost), PValue.VacuumActionTime)))
            {
                HDTA_AutoTimer.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart34_Run()//Check Vac.
        {
            bool bRet = true;
            bool[,] baRet = new bool[NOZZLE_NUM_X, NOZZLE_NUM_Y];
            List<string> alarmlist = new List<string>();
            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    baRet[x, y] = HDTA_Nozzles[x, y].VacuumValueOn;
                    if (OValue.DryRunWithoutDevice || IsSimulation() != 0)
                    {
                        baRet[x, y] = false;
                    }
                    if (baRet[x, y])
                    {
                        bRet = bRet && false;
                        string errMsg = string.Format("[{0},{1}]", x + 1, y + 1);
                        alarmlist.Add(errMsg);
                    }
                }
            }

            if (bRet)
            {
                HDTA_AutoTimer.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            for (int i = 0; i < alarmlist.Count; i++)
            {
                ShowAlarm("E", (int)HDT_ALARM_CODE.ER_NozzleDetectResidue, "A", alarmlist[i]);
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart35_Run()//Close Vac.
        {
            if (OValue.DryRunWithoutDevice)
            {
                if (HDTA_AutoTimer.On(PValue.VacuumActionTime))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else if ((HDTA_Nozzles.NozzleVacuumOff((NozzleState.InUsing | NozzleState.PnPFailure | NozzleState.Unused | NozzleState.ICLost), PValue.VacuumActionTime)))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart36_Run()//Place Done
        {
            HDTA_ActionMode = ACTIONMODE.NONE;
            HDTA_flag_ACTION.Done();
            LogDebug(string.Format("Head B Place Done"));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart38_Run()//Next
        {
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion


        #region 自動流程 HDTA Move
        private FlowChart.FCRESULT FC_HDT_A_AUTO_Move_Run()//Start Move
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart26_Run()//Need Move?
        {
            if (HDTA_flag_ACTION.IsDoIt())
            {
                if (HDTA_ActionMode.Equals(ACTIONMODE.MOVE))
                {
                    HDTA_flag_ACTION.Doing();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart27_Run()//Move to Position (X & U)
        {
            bool b1 = HDTA_X_MoveToPos(HDTA_TargetPos.X);
            bool b2 = HDTA_U_MoveToPos(HDTA_TargetPos.U);

            if (b1 && b2)
            {
                LogDebug(string.Format("Head A move X = {0},  U = {1}: ", HDTA_TargetPos.X.ToString(), HDTA_TargetPos.U.ToString()));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart28_Run()//Done
        {
            HDTA_flag_ACTION.Done();
            HDTA_ActionMode = ACTIONMODE.NONE;
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart17_Run()//Next
        {
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion


        #region 自動流程 HDTA Inspection
        private FlowChart.FCRESULT FC_HDT_A_AUTO_Inspection_Run()//Start Inspection
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart39_Run()//Wait for command
        {
            if (HDTA_flag_ACTION.IsDoIt())
            {
                if (HDTA_ActionMode.Equals(ACTIONMODE.GRAB_CCD) || HDTA_ActionMode.Equals(ACTIONMODE.GRAB_CCD_AF))
                {
                    if (bEnableTopHeadCCD.Equals(false))
                    {
                        HDTA_flag_ACTION.Done();
                        return FlowChart.FCRESULT.IDLE;
                    }
                    HDTA_flag_ACTION.Doing();
                    LogDebug(string.Format("Head A GRAB_CCD Start"));
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart40_Run()//Reset Status
        {
            mInspectionResult_A = InspectionState.Unknow;
            //mInspectionResult_A = InspectionState.Success;
            CCDA_Pos.X = 0;
            CCDA_Pos.Y = 0;
            CCDA_Pos.U = 0;
            bGrabSuccess_A = false;
            bGetResult_A = false;

            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart41_Run()//Set command
        {
            //string msg = "StartToInspectCmd";
            string msg = "StartToInspectBeforePlaceCmd";
            if ( HDTA_ActionMode.Equals(ACTIONMODE.GRAB_CCD_AF))
                msg = "StartToInspectAfterPlaceCmd";
            msg += "," + BoardCount.ToString() + "," + RowIndex.ToString() + "," + ColIndex.ToString();
            HDT_A_ClientSocket.Socket.SendText(msg);

            //CCDA_AutoTimer.Restart();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart42_Run()//Wait Status
        {
            if (bGrabSuccess_A)
            {
                bGrabSuccess_A = false;
                //string Msg = "GetInspectResultCmd," + BoardCount.ToString() + "," + RowIndex.ToString() + "," + ColIndex.ToString();
                //HDT_A_ClientSocket.Socket.SendText(Msg);
                return FlowChart.FCRESULT.NEXT;
            }
            bool b1 = false;
            if (b1)
            {
                return FlowChart.FCRESULT.CASE1;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart43_Run()//Get Result
        {
            //if (mInspectionResult_A == InspectionState.Success)
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //else if (mInspectionResult_A == InspectionState.Fail)
            //{
            //    ShowAlarm("E", (int)HDT_ALARM_CODE.ER_CCD_GetResultFail_Position, "A");
            //    return FlowChart.FCRESULT.CASE1;
            //}
            //return FlowChart.FCRESULT.NEXT;

            if (mInspectionResult_A != InspectionState.Unknow)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            bool b1 = false;
            if (b1) return FlowChart.FCRESULT.CASE1;

            //if (CCDA_AutoTimer.On(1000))
            //{
            //    ShowAlarm("E", (int)HDT_ALARM_CODE.ER_CCD_GetResultTimeout_Position, "A");
            //    CCDA_AutoTimer.Restart();
            //}
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart44_Run()//Inspection Finished
        {
            HDTA_ActionMode = ACTIONMODE.NONE;
            HDTA_flag_ACTION.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart45_Run()//Next
        {
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion


        #region 自動流程 HDTB Pick
        private FlowChart.FCRESULT FC_HDT_B_AUTO_Pick_Run()//Start Pick
        {
            if (HDTB_flag_ACTION.IsDoIt())
            {
                if (HDTB_ActionMode.Equals(ACTIONMODE.PICKUP))
                {
                    LogDebug(string.Format("Head B Pick Start "));
                    HDTB_flag_ACTION.Doing();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart72_Run()//Axis Z to PickPos + Offset
        {
            SetPickOffsetZ(BoardHeadID.HDT_B, HDTB_PnPStation);
            //bool b1 = HDTB_Z_MoveToPos(HDTB_TargetPos.Z + PValue.Distance_DT_HDT_BIB_SlowSpeed_Dis_Pick_BSM);
            bool b1 = HDTB_Z_MoveToPos(HDTB_TargetPos.Z + PValue.Distance_DT_HDT_BIB_SlowSpeed_Dis_Pick_BSM + Pick.HDTB_ZOffset);


            if (b1)
            {
                MT_HDT_B_AxisZ.SetSpeed(PValue.Speed_DT_HDT_BIB_SlowSpeed_BSM);//轉成慢速
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart73_Run()//Axis Z to PickPos 
        {
            //bool b1 = HDTB_Z_MoveToPos(HDTB_TargetPos.Z);
            bool b1 = HDTB_Z_MoveToPos(HDTB_TargetPos.Z + Pick.HDTB_ZOffset);

            if (b1)
            {
                //SetSpeed_Normal(BoardHeadID.HDT_B);//轉回常速
                LogDebug(string.Format("Head B Pick Pos = {0}: ", HDTB_TargetPos.Z.ToString()));
                HDTB_AutoTimer.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart74_Run()//Using Vacuum On
        {
            if (OValue.DryRunWithoutDevice)
            {
                if (HDTB_AutoTimer.On(PValue.VacuumActionTime))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else if ((HDTB_Nozzles.NozzleVacuumOn((NozzleState.InUsing | NozzleState.PnPFailure), PValue.VacuumActionTime)))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart75_Run()//Axis Z to Safety
        {
            bool b1 = HDTB_Z_MoveToPos(SValue.Pos_HDTB_Safe_Z);

            if (b1)
            {
                LogDebug(string.Format("Head B Safe Pos = {0}: ", SValue.Pos_HDTB_Safe_Z.ToString()));
                //SetSpeed_Normal(BoardHeadID.HDT_B);//轉回常速
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart76_Run()//Check Using Vacuum Value
        {
            bool bRet = true;
            bool[,] baRet = new bool[NOZZLE_NUM_X, NOZZLE_NUM_Y];
            List<string> alarmlist = new List<string>();

            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    if (HDTB_Nozzles[x, y].State.Equals(NozzleState.InUsing) || HDTB_Nozzles[x, y].State.Equals(NozzleState.PnPFailure))
                    {
                        baRet[x, y] = HDTB_Nozzles[x, y].VacuumValueOn;

                        if (OValue.DryRunWithoutDevice)
                        {
                            baRet[x, y] = true;
                        }

                        if (baRet[x, y])
                        {
                            HDTB_Nozzles[x, y].State = NozzleState.InUsing;
                        }
                        else
                        {
                            HDTB_Nozzles[x, y].State = NozzleState.PnPFailure;
                            bRet = bRet && false;
                            string errMsg = string.Format("[{0},{1}]", x + 1, y + 1);
                            alarmlist.Add(errMsg);
                        }
                    }
                }
            }

            if (bRet)
            {
                return FlowChart.FCRESULT.NEXT;
            }

            PP_FAILURE_ACTION_TYPE flag_failure = PP_FAILURE_ACTION_TYPE.ppNone;
            flag_failure = PP_FAILURE_ACTION_TYPE.ppIgnore | PP_FAILURE_ACTION_TYPE.ppRetry;
            CallPPFailureForm(flag_failure);

            for (int i = 0; i < alarmlist.Count; i++)
            {
                ShowAlarm("E", (int)HDT_ALARM_CODE.ER_VacuumValueOn, "B", alarmlist[i]);
            }
            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart77_Run()//Retry or Ignore
        {
            switch (PPFAReturn)
            {
                case PP_FAILURE_ACTION_TYPE.ppRetry:
                    LogTrace(string.Format("Pickup process failure. Select action = [Retry]"));
                    LogSay(EnLoggerType.EnLog_OP, string.Format("Board Head Pick Retry btn click"));
                    return FlowChart.FCRESULT.NEXT;     //重試 (取料流程 - 作用中且真空建立失敗的吸嘴氣缸下降)

                case PP_FAILURE_ACTION_TYPE.ppIgnore:
                    LogTrace("Pickup process failure. Select action = [Ignore]");
                    LogSay(EnLoggerType.EnLog_OP, string.Format("Board Head Pick Ignore btn click"));
                    return FlowChart.FCRESULT.CASE1;    //忽略 (取料流程 - 資料交換)

                //!暫時不使用search功能
                //case PP_FAILURE_ACTION_TYPE.ppSearch:

            }
            ShowAlarm("W", (int)HDT_ALARM_CODE.Warning_CheckByManual);   //Sort head pickup failure, be check devices by manual before ignore.
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart78_Run()//Vac. Failure Vac. Off
        {
            ThreeValued[,] tvRet = new ThreeValued[NOZZLE_NUM_X, NOZZLE_NUM_Y];
            if (OValue.DryRunWithoutDevice || HDTB_Nozzles.NozzleVacuumOff(NozzleState.PnPFailure))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart79_Run()//Pick Done
        {
            HDTB_ActionMode = ACTIONMODE.NONE;
            HDTB_flag_ACTION.Done();
            LogDebug(string.Format("Head B Pick Done"));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart70_Run()//Next
        {
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion


        #region 自動流程 HDTB Place
        private FlowChart.FCRESULT FC_HDT_B_AUTO_Place_Run()//Start Place
        {
            if (HDTB_flag_ACTION.IsDoIt())
            {
                if (HDTB_ActionMode.Equals(ACTIONMODE.PLACEMENT))
                {
                    LogDebug(string.Format("Head B Place Start"));
                    HDTB_flag_ACTION.Doing();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart55_Run()//Check Using Vac.
        {
            ThreeValued[,] tvRet = HDTB_Nozzles.NozzleVacuumValueOn(NozzleState.InUsing);
            bool bRet = true;
            List<string> alarmlist = new List<string>();
            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    if (HDTB_Nozzles[x, y].State.Equals(NozzleState.InUsing))
                    {
                        if (OValue.DryRunWithoutDevice)
                        {
                            tvRet[x, y] = ThreeValued.TRUE;
                        }

                        if (tvRet[x, y].Equals(ThreeValued.FALSE))
                        {
                            HDTB_Nozzles[x, y].State = NozzleState.PnPFailure;
                            bRet = bRet && false;
                            string msg = string.Format("[{0},{1}]", x + 1, y + 1);
                            alarmlist.Add(msg);
                        }
                    }
                }
            }

            if (bRet)
            {
                return FlowChart.FCRESULT.NEXT;
            }

            CallPPFailureForm(PP_FAILURE_ACTION_TYPE.ppIgnore | PP_FAILURE_ACTION_TYPE.ppRetry);
            for (int i = 0; i < alarmlist.Count; i++)
            {
                ShowAlarm("E", (int)HDT_ALARM_CODE.ER_ICLost, "B", alarmlist[i]);
            }
            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart56_Run()//Retry or Ignore
        {
            string msg = "";
            switch (PPFAReturn)
            {
                case PP_FAILURE_ACTION_TYPE.ppRetry:
                    msg = string.Format("IC Lost. Select action = [Retry]");
                    LogTrace("Place | " + msg);
                    LogSay(EnLoggerType.EnLog_OP, string.Format("Board Head Place Retry btn click"));
                    return FlowChart.FCRESULT.NEXT;     //重試 (放料流程 - 判斷作用的吸嘴是否真空建立成功,Y(N),N(1))
                //break;
                case PP_FAILURE_ACTION_TYPE.ppIgnore:
                case PP_FAILURE_ACTION_TYPE.ppSearch:
                    msg = string.Format("IC Lost. Select action = [Ignore]");
                    LogTrace("Place | " + msg);
                    LogSay(EnLoggerType.EnLog_OP, string.Format("Board Head Place Ignore btn click"));
                    return FlowChart.FCRESULT.CASE1;    //忽略 (放料流程 - IC 掉落資料交換)
                //break;
            }
            ShowAlarm("W", (int)HDT_ALARM_CODE.Warning_CheckByManual);   //Sort head pickup failure, be check devices by manual before ignore.
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart57_Run()//Set Nozzle state
        {
            SetNozzleStateToTarget(BoardHeadID.HDT_B, NozzleState.PnPFailure, NozzleState.ICLost);
            HDTB_Nozzles.NozzleVacuumOff(NozzleState.ICLost);
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart58_Run()//Retry
        {
            //放料流程 - 重試(N) - 將 ActionFailed 改為 InUsing
            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    if (HDTB_Nozzles[x, y].State.Equals(NozzleState.PnPFailure))
                    {
                        HDTB_Nozzles[x, y].State = NozzleState.InUsing;
                    }
                }
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart59_Run()//Axis Z to PlacePos + Offset
        {
            bool b1 = HDTB_Z_MoveToPos(HDTB_TargetPos.Z + PValue.Distance_DT_HDT_ST_SlowSpeed_Dis_Place_BSM);

            if (b1)
            {
                MT_HDT_B_AxisZ.SetSpeed(PValue.Speed_DT_HDT_BIB_SlowSpeed_BSM);//轉成慢速
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart60_Run()//Axis Z to PlacePos 
        {
            bool b1 = HDTB_Z_MoveToPos(HDTB_TargetPos.Z);

            if (b1)
            {
                LogDebug(string.Format("Head B Place Pos = {0}: ", HDTB_TargetPos.Z.ToString()));
                HDTB_AutoTimer.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart61_Run()//Before destory delay
        {
            if (PValue.BeforeDestoryDelay <= 0 || HDTB_AutoTimer.On(PValue.BeforeDestoryDelay))
            {
                HDTB_AutoTimer.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart62_Run()//Destory (on -> delay -> off)
        {
            int DestoryTime = PValue.DestoryActionTime;

            if (OValue.DryRunWithoutDevice)
            {
                if (HDTB_AutoTimer.On(DestoryTime))
                {
                    HDTB_AutoTimer.Restart();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else if (HDTB_Nozzles.NozzleDestroyOn(NozzleState.InUsing, DestoryTime))
            {
                if (HDTB_Nozzles.NozzleDestroyOff(NozzleState.InUsing))
                {
                    HDTB_AutoTimer.Restart();
                    return FlowChart.FCRESULT.NEXT;
                }
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart63_Run()//After destory delay
        {
            if (PValue.AfterDestoryDelay <= 0 || HDTB_AutoTimer.On(PValue.AfterDestoryDelay))
            {
                HDTB_AutoTimer.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart64_Run()//Axis Z to PlacePos + Offset
        {
            bool b1 = HDTB_Z_MoveToPos(HDTB_TargetPos.Z + PValue.Distance_DT_HDT_ST_SlowSpeed_Dis_Place_BSM);

            if (b1)
            {
                SetSpeed_Normal(BoardHeadID.HDT_B);//轉回常速
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart65_Run()//Axis Z to Safety
        {
            bool b1 = HDTB_Z_MoveToPos(SValue.Pos_HDTB_Safe_Z);

            if (b1)
            {
                LogDebug(string.Format("Head B Place Safe Pos = {0}: ", SValue.Pos_HDTB_Safe_Z.ToString()));
                HDTB_AutoTimer.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart66_Run()//Open Vac.
        {
            if (OValue.DryRunWithoutDevice)
            {
                if (HDTB_AutoTimer.On(PValue.VacuumActionTime))
                {
                    HDTB_AutoTimer.Restart();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else if ((HDTB_Nozzles.NozzleVacuumOn((NozzleState.InUsing | NozzleState.PnPFailure | NozzleState.Unused | NozzleState.ICLost), PValue.VacuumActionTime)))
            {
                HDTB_AutoTimer.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart67_Run()//Check Vac.
        {
            bool bRet = true;
            bool[,] baRet = new bool[NOZZLE_NUM_X, NOZZLE_NUM_Y];
            List<string> alarmlist = new List<string>();
            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    baRet[x, y] = HDTB_Nozzles[x, y].VacuumValueOn;
                    if (OValue.DryRunWithoutDevice || IsSimulation() != 0)
                    {
                        baRet[x, y] = false;
                    }
                    if (baRet[x, y])
                    {
                        bRet = bRet && false;
                        string errMsg = string.Format("[{0},{1}]", x + 1, y + 1);
                        alarmlist.Add(errMsg);
                    }
                }
            }

            if (bRet)
            {
                HDTB_AutoTimer.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            for (int i = 0; i < alarmlist.Count; i++)
            {
                ShowAlarm("E", (int)HDT_ALARM_CODE.ER_NozzleDetectResidue, "B", alarmlist[i]);
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart68_Run()//Close Vac.
        {
            if (OValue.DryRunWithoutDevice)
            {
                if (HDTB_AutoTimer.On(PValue.VacuumActionTime))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else if ((HDTB_Nozzles.NozzleVacuumOff((NozzleState.InUsing | NozzleState.PnPFailure | NozzleState.Unused | NozzleState.ICLost), PValue.VacuumActionTime)))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart69_Run()//Place Done
        {
            HDTB_ActionMode = ACTIONMODE.NONE;
            HDTB_flag_ACTION.Done();
            LogDebug(string.Format("Head B Place Done"));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart53_Run()//Next
        {
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion


        #region 自動流程 HDTB Move
        private FlowChart.FCRESULT FC_HDT_B_AUTO_Move_Run()//Start Move
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart54_Run()//Need Move?
        {
            if (HDTB_flag_ACTION.IsDoIt())
            {
                if (HDTB_ActionMode.Equals(ACTIONMODE.MOVE))
                {
                    HDTB_flag_ACTION.Doing();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart71_Run()//Move to Position (X & U)
        {
            bool b1 = HDTB_X_MoveToPos(HDTB_TargetPos.X);
            bool b2 = HDTB_U_MoveToPos(HDTB_TargetPos.U);

            if (b1 && b2)
            {
                LogDebug(string.Format("Head B move X = {0},  U = {1}: ", HDTB_TargetPos.X.ToString(), HDTB_TargetPos.U.ToString()));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart80_Run()//Done
        {
            HDTB_flag_ACTION.Done();
            HDTB_ActionMode = ACTIONMODE.NONE;
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart81_Run()//Next
        {
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion


        #region 自動流程 HDTB Inspection
        private FlowChart.FCRESULT FC_HDT_B_AUTO_Inspection_Run()//Start Inspection
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart47_Run()//Wait for command
        {
            if (HDTB_flag_ACTION.IsDoIt())
            {
                if (HDTB_ActionMode.Equals(ACTIONMODE.GRAB_CCD) || HDTB_ActionMode.Equals(ACTIONMODE.GRAB_CCD_AF))
                {
                    //if (SReadValue("EnableBottomHeadCCD").ToBoolean() == false)
                    if (bEnableBottomHeadCCD.Equals(false))
                    {
                        HDTB_flag_ACTION.Done();
                        return FlowChart.FCRESULT.IDLE;
                    }

                    LogDebug(string.Format("Head B GRAB_CCD Start"));
                    HDTB_flag_ACTION.Doing();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart48_Run()//Reset Status
        {
            mInspectionResult_B = InspectionState.Unknow;
            CCDB_Pos.X = 0;
            CCDB_Pos.Y = 0;
            CCDB_Pos.U = 0;
            bGrabSuccess_B = false;
            bGetResult_B = false;

            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart49_Run()//Set command
        {
            //string msg = "StartToInspectCmd";
            string msg = "StartToInspectBeforePlaceCmd";
            if (HDTA_ActionMode.Equals(ACTIONMODE.GRAB_CCD_AF))
                msg = "StartToInspectAfterPlaceCmd";

            msg += "," + BoardCount.ToString() + "," + RowIndex.ToString() + "," + ColIndex.ToString();
            HDT_B_ClientSocket.Socket.SendText(msg);

            //CCDB_AutoTimer.Restart();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart50_Run()//Wait Status
        {
            if (bGrabSuccess_B)
            {
                bGrabSuccess_B = false;
                //string Msg = "GetInspectResultCmd," + BoardCount.ToString() + "," + RowIndex.ToString() + "," + ColIndex.ToString();
                //HDT_B_ClientSocket.Socket.SendText(Msg);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart51_Run()//Get Result
        {
            //if (mInspectionResult_B == InspectionState.Success)
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //else if (mInspectionResult_B == InspectionState.Fail)
            //{
            //    ShowAlarm("E", (int)HDT_ALARM_CODE.ER_CCD_GetResultFail_Position, "B");
            //    return FlowChart.FCRESULT.CASE1;
            //}


            if (mInspectionResult_B != InspectionState.Unknow)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            bool b1 = false;
            if (b1) return FlowChart.FCRESULT.CASE1;

            //if (CCDA_AutoTimer.On(1000))
            //{
            //    ShowAlarm("E", (int)HDT_ALARM_CODE.ER_CCD_GetResultTimeout_Position, "A");
            //    CCDA_AutoTimer.Restart();
            //}
            return FlowChart.FCRESULT.IDLE;

            //if (mInspectionResult_B != InspectionState.Unknow)
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}

            ////if (CCDB_AutoTimer.On(1000))
            ////{
            ////    ShowAlarm("E", (int)HDT_ALARM_CODE.ER_CCD_GetResultTimeout_Position, "B");
            ////    CCDB_AutoTimer.Restart();
            ////}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart52_Run()//Inspection Finished
        {
            HDTB_ActionMode = ACTIONMODE.NONE;
            HDTB_flag_ACTION.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart46_Run()//Next
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

        private void button3_Click(object sender, EventArgs e)
        {
            SetSpeed(true);
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            if (btn != null)
            {
                KCSDK.MotorJogForm.MotorJog.Run(btn.Parent);
            }
            SetSpeed();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SetSpeed(true);
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            if (btn != null)
            {
                KCSDK.MotorJogForm.MotorJog.Run(btn.Parent);
            }
            SetSpeed();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SetSpeed(true);
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            if (btn != null)
            {
                KCSDK.MotorJogForm.MotorJog.Run(btn.Parent);
            }
            SetSpeed();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SetSpeed(true);
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            if (btn != null)
            {
                KCSDK.MotorJogForm.MotorJog.Run(btn.Parent);
            }
            SetSpeed();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SetSpeed(true);
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            if (btn != null)
            {
                KCSDK.MotorJogForm.MotorJog.Run(btn.Parent);
            }
            SetSpeed();
        }

        private void button5_Click(object sender, EventArgs e)
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

 
        #region CCD相關事件、按鈕
        //按鈕
        private void button10_Click(object sender, EventArgs e)//CCDA 連線
        {
            try
            {
                HDT_A_ClientSocket.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button9_Click(object sender, EventArgs e)//CCDA 斷線
        {
            try
            {
                HDT_A_ClientSocket.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button12_Click(object sender, EventArgs e)//CCDB 連線
        {
            try
            {
                HDT_B_ClientSocket.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button11_Click(object sender, EventArgs e)//CCDB 斷線
        {
            try
            {
                HDT_B_ClientSocket.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        //事件
        private void HDT_A_ClientSocket_OnConnect(object sender)
        {
            JLogger.LogDebug("HDTA_CCD_Socket", "CCD連線建立成功");
        }

        private void HDT_A_ClientSocket_OnDisconnect(object sender, System.Net.Sockets.Socket socket)
        {
            JLogger.LogDebug("HDTA_CCD_Socket", "CCD連線中斷");
        }

        private void HDT_A_ClientSocket_OnError(object sender, int errorCode, string ErrMsg)
        {
            String msg = String.Format("Socket連線錯誤：Error Code:{0}, ErrMsg{1}", errorCode, ErrMsg);
            JLogger.LogDebug("HDTA_CCD_Socket", msg);
        }

        private void HDT_A_ClientSocket_OnRead(ProVTool.SocketClient sender)
        {
            string msg = sender.ReadText();
            CCDA_ParseResultString(msg);
        }

        private void HDT_B_ClientSocket_OnConnect(object sender)
        {
            JLogger.LogDebug("HDTB_CCD_Socket", "CCD連線建立成功");
        }

        private void HDT_B_ClientSocket_OnDisconnect(object sender, System.Net.Sockets.Socket socket)
        {
            JLogger.LogDebug("HDTB_CCD_Socket", "CCD連線中斷");
        }

        private void HDT_B_ClientSocket_OnError(object sender, int errorCode, string ErrMsg)
        {
            String msg = String.Format("Socket連線錯誤：Error Code:{0}, ErrMsg{1}", errorCode, ErrMsg);
            JLogger.LogDebug("HDTB_CCD_Socket", msg);
        }

        private void HDT_B_ClientSocket_OnRead(ProVTool.SocketClient sender)
        {
            string msg = sender.ReadText();
            CCDB_ParseResultString(msg);
        }

        //public enum InspectionState
        //{
        //    Unknow,
        //    Success,
        //    Fail,
        //    Error,
        //    Wait,
        //}

        private InspectionState mInspectionResult_A = InspectionState.Unknow;//判斷是否成功
        private BasePosInfo CCDA_Pos;//視覺補償點位
        private bool bGrabSuccess_A = false;
        private bool bGetResult_A = false;
        private bool CCDA_ParseResultString(string result)
        {
            try
            {
                char[] Separators = new char[] { ',', ';', '=', '|' };
                String[] sCommand = result.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

                //if (sCommand[0] == "Pass")
                if (sCommand[0] == "1")
                {
                    mInspectionResult_A = InspectionState.Success;
                    double itemp = 0;
                    double.TryParse(sCommand[1], out itemp);
                    CCDA_Pos.X = (int)itemp;
                    double.TryParse(sCommand[2], out itemp);
                    CCDA_Pos.Y = (int)itemp;
                    double.TryParse(sCommand[3], out itemp);
                    CCDA_Pos.U = (int)itemp;
                    
                    CCDA_Pos.X = 0;
                    CCDA_Pos.Y = 0;
                    CCDA_Pos.U = 0;
                }
                //else if (sCommand[0] == "Fail")
                if (sCommand[0] == "0")
                {
                    mInspectionResult_A = InspectionState.Fail;
                    double itemp = 0;
                    double.TryParse(sCommand[1], out itemp);
                    CCDA_Pos.X = (int)itemp;
                    double.TryParse(sCommand[2], out itemp);
                    CCDA_Pos.Y = (int)itemp;
                    double.TryParse(sCommand[3], out itemp);
                    CCDA_Pos.U = (int)itemp;
                }
                if (sCommand[0] == "-1")
                {
                    mInspectionResult_A = InspectionState.Error;
                    CCDA_Pos.X = 0;
                    CCDA_Pos.Y = 0;
                    CCDA_Pos.U = 0;
                }
                if (sCommand[0] == "GrabSuccessCmd")
                {
                    bGrabSuccess_A = true;
                }

                if (sCommand[0] == "StartToInspectBeforePlaceCmd")
                {
                    bGetResult_A = true;
                }


                if (sCommand[0] == "StartToInspectAfterPlaceCmd")
                {
                    bGetResult_A = true;
                }
                return true;
            }
            catch (Exception ex)
            {
                mInspectionResult_A = InspectionState.Error;
                CCDA_Pos.X = 0;
                CCDA_Pos.Y = 0;
                CCDA_Pos.U = 0;
                bGrabSuccess_A = false;
                bGetResult_A = false;
                return false;
            }
        }


        private InspectionState mInspectionResult_B = InspectionState.Unknow;//判斷是否成功
        private BasePosInfo CCDB_Pos;//視覺補償點位
        private bool bGrabSuccess_B = false;
        private bool bGetResult_B = false;
        private bool CCDB_ParseResultString(string result)
        {
            try
            {
                char[] Separators = new char[] { ',', ';', '=', '|' };
                String[] sCommand = result.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

                //if (sCommand[0] == "Pass")
                if (sCommand[0] == "1") //有IC
                {
                    mInspectionResult_B = InspectionState.Success;
                    double itemp = 0;
                    double.TryParse(sCommand[1], out itemp);
                    CCDB_Pos.X = (int)itemp;
                    double.TryParse(sCommand[2], out itemp);
                    CCDB_Pos.Y = (int)itemp;
                    double.TryParse(sCommand[3], out itemp);
                    CCDB_Pos.U = (int)itemp;
                }
                //else if (sCommand[0] == "Fail")
                if (sCommand[0] == "0") //沒有IC
                {
                    mInspectionResult_B = InspectionState.Fail;
                    double itemp = 0;
                    double.TryParse(sCommand[1], out itemp);
                    CCDB_Pos.X = (int)itemp;
                    double.TryParse(sCommand[2], out itemp);
                    CCDB_Pos.Y = (int)itemp;
                    double.TryParse(sCommand[3], out itemp);
                    CCDB_Pos.U = (int)itemp;
                }
                if (sCommand[0] == "-1")    //異常
                {
                    mInspectionResult_B = InspectionState.Error;
                    CCDB_Pos.X = 0;
                    CCDB_Pos.Y = 0;
                    CCDB_Pos.U = 0;
                }
                if (sCommand[0] == "GrabSuccessCmd")
                {
                    bGrabSuccess_B = true;
                }

                if (sCommand[0] == "StartToInspectBeforePlaceCmd")
                {
                    bGetResult_B = true;
                }


                if (sCommand[0] == "StartToInspectAfterPlaceCmd")
                {
                    bGetResult_B = true;
                }
                return true;
            }
            catch (Exception ex)
            {
                mInspectionResult_B = InspectionState.Error;
                CCDB_Pos.X = 0;
                CCDB_Pos.Y = 0;
                CCDB_Pos.U = 0;
                bGrabSuccess_B = false;
                bGetResult_B = false;
                return false;
            }
        }

        public InspectionState GetInspectState(BoardHeadID id)
        {
            InspectionState state = InspectionState.Unknow;
            switch (id)
            {
                case BoardHeadID.HDT_A:
                    {
                        state = mInspectionResult_A;
                    }
                    break;
                case BoardHeadID.HDT_B:
                    {
                        state = mInspectionResult_B;
                    }
                    break;
            }
            return state;
        }

        public BasePosInfo GetInspectResult(BoardHeadID id)
        {
            BasePosInfo Pos = new BasePosInfo();
            switch (id)
            {
                case BoardHeadID.HDT_A:
                    {
                        Pos = CCDA_Pos;
                    }
                    break;
                case BoardHeadID.HDT_B:
                    {
                        Pos = CCDB_Pos;
                    }
                    break;
            }

            return Pos;
        }


        public int GetNozzleCCDOffsetY(BoardHeadID id)
        {
            int Offset = 0;
            switch (id)
            {
                case BoardHeadID.HDT_A:
                    {
                        Offset = SValue.Pos_HDTA_Nozzle_CCD_OffsetY;
                    }
                    break;
                case BoardHeadID.HDT_B:
                    {
                        Offset = SValue.Pos_HDTB_Nozzle_CCD_OffsetY;
                    }
                    break;
            }
            return Offset;
        }

        public int GetNozzleCCDOffset(BoardHeadID id)
        {
            int Offset = 0;
            switch (id)
            {
                case BoardHeadID.HDT_A:
                    {
                        Offset = SValue.Pos_HDTA_Nozzle_CCD_Offset;
                    }
                    break;
                case BoardHeadID.HDT_B:
                    {
                        Offset = SValue.Pos_HDTB_Nozzle_CCD_Offset;
                    }
                    break;
            }
            return Offset;
        }

        public bool bUseCCD(BoardHeadID id)
        {
            bool bRet = false;
            switch (id)
            {
                case BoardHeadID.HDT_A:
                    {
                        bRet = bEnableTopHeadCCD;
                    }
                    break;
                case BoardHeadID.HDT_B:
                    {
                        bRet = bEnableBottomHeadCCD;
                    }
                    break;
            }

            return bRet;
        }

        #endregion

        private void button14_Click(object sender, EventArgs e)
        {
            ServoOn();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ServoOff();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            ServoOn();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ServoOff();
        }

        private FlowChart.FCRESULT flowChart84_Run()
        {
            bool b1 = HDTA_U_MoveToPos(0);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart85_Run()
        {
            bool b1 = HDTB_U_MoveToPos(0);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart83_Run()
        {
            if (IsSimulation() != 0) return FlowChart.FCRESULT.NEXT;

            bool b1 = false;
            bool b2 = false;

            if (IsAxisZSafety(BoardHeadID.HDT_A).Equals(true))
            {
                int pos = SReadValue("Pos_HDT_A_U_Ready").ToInt();
                b1 = MT_HDT_A_AxisU.G00(pos);
            }
            else
            {
                ShowAlarm("E", (int)HDT_ALARM_CODE.ER_Protection_AxisZNotInSafePos, "A");
            }
            if (IsAxisZSafety(BoardHeadID.HDT_B).Equals(true))
            {
                int pos = SReadValue("Pos_HDT_B_U_Ready").ToInt();
                b2 = MT_HDT_B_AxisU.G00(pos);
            }
            else
            {
                ShowAlarm("E", (int)HDT_ALARM_CODE.ER_Protection_AxisZNotInSafePos, "B");
            }

            if (b1 && b2)
            {
                MT_HDT_A_AxisU.SetPos(0);
                MT_HDT_A_AxisU.SetEncoderPos(0);
                MT_HDT_B_AxisU.SetPos(0);
                MT_HDT_B_AxisU.SetEncoderPos(0);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart86_Run()
        {
            if (bGetResult_A)
            {
                bGetResult_A = false;
                string Msg = "GetInspectResultCmd," + BoardCount.ToString() + "," + RowIndex.ToString() + "," + ColIndex.ToString();
                HDT_A_ClientSocket.Socket.SendText(Msg);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart87_Run()
        {
            if (bGetResult_B)
            {
                bGetResult_B = false;
                string Msg = "GetInspectResultCmd," + BoardCount.ToString() + "," + RowIndex.ToString() + "," + ColIndex.ToString();
                HDT_B_ClientSocket.Socket.SendText(Msg);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart88_Run()
        {
            SetPickOffsetZ(BoardHeadID.HDT_A, HDTA_PnPStation);
            bool b1 = HDTA_Z_MoveToPos(HDTA_TargetPos.Z + PValue.Distance_DT_HDT_BIB_SlowSpeed_Dis_Pick_BSM + Pick.HDTA_ZOffset);

            if (b1)
            {
                SetSpeed_Normal(BoardHeadID.HDT_A);//轉回常速
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
            
        }

        private FlowChart.FCRESULT flowChart89_Run()
        {
            SetPickOffsetZ(BoardHeadID.HDT_B, HDTB_PnPStation);
            //bool b1 = HDTB_Z_MoveToPos(HDTB_TargetPos.Z + PValue.Distance_DT_HDT_BIB_SlowSpeed_Dis_Pick_BSM);
            bool b1 = HDTB_Z_MoveToPos(HDTB_TargetPos.Z + PValue.Distance_DT_HDT_BIB_SlowSpeed_Dis_Pick_BSM + Pick.HDTB_ZOffset);


            if (b1)
            {
                SetSpeed_Normal(BoardHeadID.HDT_B);//轉回常速
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        

        

        
        

    }
}

