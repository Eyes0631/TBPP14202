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
using PaeLibProVSDKEx;
using PaeLibGeneral;
using CommonObj;
using System.Threading;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace BFM
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

    /// <summary>
    /// 震動盤異常處置方式列舉
    /// </summary>
    /// [Flags]
    public enum BF_FAILURE_ACTION_TYPE
    {
        bfNone = 0,
        bfWait = 1,
        bfFinish = 2,
        bfALL = bfNone | bfWait | bfFinish
    }

    /// <summary>
    /// 吸嘴真空異常處置方式列舉
    /// </summary>
    [Flags]
    public enum PP_FAILURE_ACTION_TYPE
    {
        ppNone = 0,
        ppRetry = 1,
        ppIgnore = 2,
        ppSearch = 4,
        ppALL = ppNone | ppRetry | ppIgnore | ppSearch
    }

    public partial class BFM : BaseModuleInterface, BFM_interface
    {
        /// <summary>
        /// 三態 (使用於基礎元件, JDI, JCY, JVCC, JNOZ...etc)
        /// </summary>
        public enum TripletRestult
        {
            NONE_WAITING_BUSY = 0,
            TRUE_ON_DONE_OK = 1,
            FALSE_OFF_TIMEOUT_NG = 2,
        }

        #region 【USER_TODO : 依實際需求定義】

        const int NOZZLE_NUM_X = 1;     //吸嘴 X 數量
        const int NOZZLE_NUM_Y = 1;     //吸嘴 Y 數量

        #endregion

        #region 控制元件
        private Motor MT_BFM1_AxisX = null;                   // BFM1取放手臂 X 軸馬達
        private Motor MT_BFM1_AxisU = null;                   // BFM1取放手臂 U 軸馬達
        private Motor MT_BFM1_AxisZ = null;                   // BFM1取放手臂 Z 軸馬達        
        private Nozzle Nozzle_BFM1 = null;                     // BFM1吸嘴陣列元件
        private DigitalInput DI_BFM1ArrivalDetection = null;
        //private DigitalInput DI_BFM1IonizerFanSensor = null;


        private Motor MT_BFM2_AxisX = null;                   // BFM2取放手臂 X 軸馬達
        private Motor MT_BFM2_AxisU = null;                   // BFM2取放手臂 U 軸馬達
        private Motor MT_BFM2_AxisZ = null;                   // BFM2取放手臂 Z 軸馬達
        private Nozzle Nozzle_BFM2 = null;                      // BFM2吸嘴陣列元件
        private DigitalInput DI_BFM2ArrivalDetection = null;
        //private DigitalInput DI_BFM2IonizerFanSensor = null;

        #endregion

        #region 基準點位與 Package 資訊

        #endregion

        #region 取放流程相關變數
        bool SystemDryRun = false;
        bool DryRunWithoutDevice = false;
        private bool mBFM1AxisZHomeOK;                          // 確認BFM1 Z軸是否已經歸零完成
        private bool mBFM2AxisZHomeOK;                          // 確認BFM2 Z軸是否已經歸零完成
        private bool mBFM1HdtHomeOk;                            // 確認BFM1 HDT模組已經歸零完成
        private bool mBFM1BfHomeOk;                             // 確認BFM1 BF模組已經歸零完成
        private bool mBFM2HdtHomeOk;                            // 確認BFM2 HDT模組已經歸零完成
        private bool mBFM2BfHomeOk;                             // 確認BFM2 BF模組已經歸零完成
        private JActionFlag Flag_BowlFeederID1Action;           // Flag_BowlFeeder1動作旗標
        private ACTIONMODE BowlFeederID1ActionMode;             // BowlFeeder1動作模式(取料/放料)
        private JActionFlag Flag_BowlFeederID2Action;           // Flag_BowlFeeder2動作旗標
        private ACTIONMODE BowlFeederID2ActionMode;             // BowlFeeder1動作模式(取料/放料)
        private BasePosInfo BF_A_PnPPos;
        private BasePosInfo BF_B_PnPPos;
        private CVisionResult BF_A_InspectionResult;            // BF A模組CCD辨識結果
        private CVisionResult BF_B_InspectionResult;            // BF B模組CCD辨識結果

        private MyTimer T_BLM1Home = new MyTimer();             // BLM 1歸零時使用的Timer
        private MyTimer T_BLM2Home = new MyTimer();             // BLM 2歸零時使用的Timer
        private MyTimer T_BF1 = new MyTimer();                  // 震動盤1流程用的Timer
        private MyTimer T_BF2 = new MyTimer();                  // 震動盤2流程用的Timer
        private MyTimer T_BF1_Pick = new MyTimer();             // BF1模組pick流程使用的Timer
        private MyTimer T_BF1_Place = new MyTimer();            // BF1模組pick流程使用的Timer
        private MyTimer T_BF2_Pick = new MyTimer();             // BF2模組pick流程使用的Timer
        private MyTimer T_BF2_Place = new MyTimer();            // BF2模組pick流程使用的Timer

        private bool bBF1CanPick = false;                       // Bowl Feeder 1設定是否可供吸嘴取料
        private bool bBF2CanPick = false;                       // Bowl Feeder 2設定是否可供吸嘴取料
        private bool bBF1TakePartAway = false;                  // 通知Bowl Feeder 1已經取走Part了
        private bool bBF2TakePartAway = false;                  // 通知Bowl Feeder 2已經取走Part了
        private int iBF1_PickRetryTimes = 0;                    // 記錄目前BF1取料失敗累積次數
        private int iBF2_PickRetryTimes = 0;                    // 記錄目前BF2取料失敗累積次數
        private int iBF1_InspectionFailureTimes = 0;            // 記錄目前BF1 CCD失敗累積次數
        private int iBF2_InspectionFailureTimes = 0;            // 記錄目前BF2 CCD失敗累積次數

        private bool bNotUseBF1 = false;                            //是否不使用BF1
        private bool bNotUseBF2 = false;                            //是否不使用BF2

        //private MyTimer
        #endregion

        #region 其他表單處理相關變數
        private Thread DialogFormThread = null;                 // 其他表單處理專用執行緒
        private bool StopDialogFormThread = true;               // 其他表單處理專用執行緒運行旗標
        private bool ShowPPFAForm = false;                      // 吸嘴真空異常處置方式選擇表單顯示控制旗標
        private string BFM_PnPDialogMessage = "";                  // 顯示要出現在對話框的內容

        private PP_FAILURE_ACTION_TYPE PPFAReturn = PP_FAILURE_ACTION_TYPE.ppNone;      //吸嘴真空異常處置方式回傳值
        private PP_FAILURE_ACTION_TYPE PPFAFormType = PP_FAILURE_ACTION_TYPE.ppNone;    //吸嘴真空異常處置方式選擇表單類型

        private bool ShowBFAForm = false;
        private BF_FAILURE_ACTION_TYPE BFAReturn = BF_FAILURE_ACTION_TYPE.bfNone;
        private BF_FAILURE_ACTION_TYPE BFAFormType = BF_FAILURE_ACTION_TYPE.bfNone;
        private string BFM_BFDialogMessage = "";                  // 顯示要出現在對話框的內容


        #endregion

        public BFM()
        {
            InitializeComponent();

            CreateComponentList();

            DialogFormThread = new Thread(PPFAProcess);
            StopDialogFormThread = false;
            DialogFormThread.Start();

            Flag_BowlFeederID1Action = new JActionFlag();
            Flag_BowlFeederID2Action = new JActionFlag();

            BF_A_InspectionResult = new CVisionResult();
            BF_B_InspectionResult = new CVisionResult();
        }

        private void PPFAProcess()
        {
            while (!StopDialogFormThread)
            {
                while (ShowPPFAForm)
                {
                    PPFailureActionSelectForm PPFASelectForm = new PPFailureActionSelectForm();
                    PPFASelectForm.ShowPPFASelectForm(PPFAFormType, BFM_PnPDialogMessage);
                    PPFAReturn = PPFASelectForm.PPFailureActionResult;
                    PPFAFormType = PP_FAILURE_ACTION_TYPE.ppNone;
                    ShowPPFAForm = false;
                    PPFASelectForm.Close();
                }

                while (ShowBFAForm)
                {
                    BowlFeederActionSelectForm BFASelectForm = new BowlFeederActionSelectForm();
                    BFASelectForm.ShowBFASelectForm(BFAFormType, BFM_BFDialogMessage);
                    BFAReturn = BFASelectForm.BFFailureActionResult;
                    BFAFormType = BF_FAILURE_ACTION_TYPE.bfNone;
                    ShowBFAForm = false;
                    BFASelectForm.Close();
                }
                
                Thread.Sleep(10);
            }
        }

        #region 繼承函數

        //模組解構使用
        public override void DisposeTH()
        {
            StopDialogFormThread = true;
            DialogFormThread.Join();
            base.DisposeTH();
        }

        //程式初始化
        public override void Initial()
        {
            MT_BFM1_AxisX = MT_Bowl1_Head_AxisX;
            MT_BFM1_AxisU = MT_Bowl1_Head_AxisU;
            MT_BFM1_AxisZ = MT_Bowl1_Head_AxisZ;
            Nozzle_BFM1 = new Nozzle(null, null, null, ib_Bowl1_VaccumChk, ob_Bowl1_VaccumSW, ob_Bowl1_DestorySW);
            DI_BFM1ArrivalDetection = new DigitalInput(ib_Bowl1_ArrivalDetection);
            //DI_BFM1IonizerFanSensor = new DigitalInput(ib_Bowl1_IonizerFanSensor);

            MT_BFM2_AxisX = MT_Bowl2_Head_AxisX;
            MT_BFM2_AxisU = MT_Bowl2_Head_AxisU;
            MT_BFM2_AxisZ = MT_Bowl2_Head_AxisZ;
            Nozzle_BFM2 = new Nozzle(null, null, null, ib_Bowl2_VaccumChk, ob_Bowl2_VaccumSW, ob_Bowl2_DestorySW);
            DI_BFM2ArrivalDetection = new DigitalInput(ib_Bowl2_ArrivalDetection);
            //DI_BFM2IonizerFanSensor = new DigitalInput(ib_Bowl2_IonizerFanSensor);
        }

        //持續偵測函數
        public override void AlwaysRun() 
        {
            bool b1 = (MT_BFM1_AxisX != null && MT_BFM1_AxisX.Busy());
            if (b1)
            {
                if (IsAxisZSafety(BowlFeederID.BF_A).Equals(false))
                {
                    StopMotor();
                    //Alarm : 手臂馬達移動中，偵測Z軸不再安全高度狀態下，馬達停止
                    ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM1_ZAixsNotSafePosButXAxisMove);
                }                
            }

            bool b2 = (MT_BFM2_AxisX != null && MT_BFM2_AxisX.Busy());
            if (b2)
            {
                if (IsAxisZSafety(BowlFeederID.BF_B).Equals(false))
                {
                    StopMotor();
                    //Alarm : 手臂馬達移動中，偵測Z軸不再安全高度狀態下，馬達停止
                    ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM2_ZAixsNotSafePosButXAxisMove);
                }
            }


        } //持續掃描

        public override int ScanIO() { return 0; } //掃描硬體按鈕IO

        //歸零相關函數
        public override void HomeReset()
        {
            DataReset();
            mLotend = false;
            mLotendOk = false;
            mCanHome = false;
            mHomeOk = false;
            mBFM1AxisZHomeOK = false;
            mBFM2AxisZHomeOK = false;
            mBFM1HdtHomeOk = false;
            mBFM1BfHomeOk = false;
            mBFM2HdtHomeOk = false;
            mBFM2BfHomeOk = false;

            Flag_BowlFeederID1Action.Reset();
            Flag_BowlFeederID2Action.Reset();

            FC_Home_HDT1_Home_Start.TaskReset();
            FC_Home_BF1_Home_Start.TaskReset();
            FC_Home_HDT2_Home_Start.TaskReset();
            FC_Home_BF2_Home_Start.TaskReset();
        } //歸零前的重置

        public override bool Home()
        {
            if (mBFM1HdtHomeOk == true &&
                mBFM1BfHomeOk == true &&
                mBFM2HdtHomeOk == true &&
                mBFM2BfHomeOk == true)
            {
                mHomeOk = true;
            }
            else
            {
                FC_Home_HDT1_Home_Start.MainRun();
                FC_Home_BF1_Home_Start.MainRun();
                FC_Home_HDT2_Home_Start.MainRun();
                FC_Home_BF2_Home_Start.MainRun();
            }

            return mHomeOk;
        } //歸零

        //運轉相關函數
        public override void ServoOn()
        {
            if (MT_BFM1_AxisX != null) { MT_BFM1_AxisX.ServoOn(true); }
            if (MT_BFM1_AxisU != null) { MT_BFM1_AxisU.ServoOn(true); }
            if (MT_BFM1_AxisZ != null) { MT_BFM1_AxisZ.ServoOn(true); }

            if (MT_BFM2_AxisX != null) { MT_BFM2_AxisX.ServoOn(true); }
            if (MT_BFM2_AxisU != null) { MT_BFM2_AxisU.ServoOn(true); }
            if (MT_BFM2_AxisZ != null) { MT_BFM2_AxisZ.ServoOn(true); }
        } //Motor Servo On

        public override void ServoOff()
        {
            if (MT_BFM1_AxisX != null) { MT_BFM1_AxisX.ServoOn(false); }
            if (MT_BFM1_AxisU != null) { MT_BFM1_AxisU.ServoOn(false); }
            if (MT_BFM1_AxisZ != null) { MT_BFM1_AxisZ.ServoOn(false); }

            if (MT_BFM2_AxisX != null) { MT_BFM2_AxisX.ServoOn(false); }
            if (MT_BFM2_AxisU != null) { MT_BFM2_AxisU.ServoOn(false); }
            if (MT_BFM2_AxisZ != null) { MT_BFM2_AxisZ.ServoOn(false); }
        } //Motor Servo Off

        public override void RunReset() 
        {
            FC_BF1_Start.TaskReset();
            FC_BF2_Start.TaskReset();
            FC_Pick_StartPick1_Start.TaskReset();
            FC_Pick_StartPick2_Start.TaskReset();
            FC_Place_StartPlace1_Start.TaskReset();
            FC_Place_StartPlace2_Start.TaskReset();
        } //運轉前初始化

        public override void Run() 
        {
            if (SReadValue("DoNotUseBFM1Module").ToBoolean() == false)
            {
                FC_BF1_Start.MainRun();
                FC_Pick_StartPick1_Start.MainRun();
                FC_Place_StartPlace1_Start.MainRun();   
            }

            if (SReadValue("DoNotUseBFM2Module").ToBoolean() == false)
            {
                FC_BF2_Start.MainRun();
                FC_Pick_StartPick2_Start.MainRun();
                FC_Place_StartPlace2_Start.MainRun();
            }
        } //運轉

        public override void SetSpeed(bool bHome = false)
        {
            int SPD = 10000;
            int ACC_MULTIPLE = 100000;
            int ACC_DEC = 100000;
            int SPEED_RATE = OReadValue("機台速率").ToInt();

            if (MT_BFM1_AxisX != null)
            {
                if (!bHome)
                {
                    SPD = (SReadValue("MT_HDT_X_SPEED").ToInt() * SPEED_RATE) / 100;
                    ACC_MULTIPLE = SReadValue("MT_HDT_X_ACC_MULTIPLE").ToInt();
                    ACC_DEC = (SPD * ACC_MULTIPLE);
                }
                MT_BFM1_AxisX.SetSpeed(SPD);
                MT_BFM1_AxisX.SetAcceleration(ACC_DEC);
                MT_BFM1_AxisX.SetDeceleration(ACC_DEC);
            }

            if (MT_BFM2_AxisX != null)
            {
                MT_BFM2_AxisX.SetSpeed(SPD);
                MT_BFM2_AxisX.SetAcceleration(ACC_DEC);
                MT_BFM2_AxisX.SetDeceleration(ACC_DEC);
            }

            if (MT_BFM1_AxisZ != null)
            {
                if (!bHome)
                {
                    SPD = (SReadValue("MT_HDT_Z_SPEED").ToInt() * SPEED_RATE) / 100;
                    ACC_MULTIPLE = SReadValue("MT_HDT_Z_ACC_MULTIPLE").ToInt();
                    ACC_DEC = (SPD * ACC_MULTIPLE);
                }
                MT_BFM1_AxisZ.SetSpeed(SPD);
                MT_BFM1_AxisZ.SetAcceleration(ACC_DEC);
                MT_BFM1_AxisZ.SetDeceleration(ACC_DEC);
            }

            if (MT_BFM2_AxisZ != null)
            {
                MT_BFM2_AxisZ.SetSpeed(SPD);
                MT_BFM2_AxisZ.SetAcceleration(ACC_DEC);
                MT_BFM2_AxisZ.SetDeceleration(ACC_DEC);
            }

            if (MT_BFM1_AxisU != null)
            {
                if (!bHome)
                {
                    SPD = (SReadValue("MT_HDT_U_SPEED").ToInt() * SPEED_RATE) / 100;
                    ACC_MULTIPLE = SReadValue("MT_HDT_U_ACC_MULTIPLE").ToInt();
                    ACC_DEC = (SPD * ACC_MULTIPLE);
                }
                MT_BFM1_AxisU.SetSpeed(SPD);
                MT_BFM1_AxisU.SetAcceleration(ACC_DEC);
                MT_BFM1_AxisU.SetDeceleration(ACC_DEC);
            }

            if (MT_BFM2_AxisU != null)
            {
                MT_BFM2_AxisU.SetSpeed(SPD);
                MT_BFM2_AxisU.SetAcceleration(ACC_DEC);
                MT_BFM2_AxisU.SetDeceleration(ACC_DEC);
            }
        } //速度設定

        //手動相關函數
        public override void ManualReset() { } //手動運行前置設定
        public override void ManualRun() { } //手動模式運行

        //停止所有馬達
        public override void StopMotor()
        {
            base.StopMotor();
        }

        #endregion

        #region 私有函數
        private void ShowAlarmMessage(string AlarmLevel, int ErrorCode, params object[] args)   //Alarm顯示
        {
            ShowAlarm(AlarmLevel, ErrorCode, args);
        }
        private void ClearAlarmMessage(string AlarmLevel, int ErrorCode)
        {
            return;
        }

        /// <summary>
        /// Loads the basic position data.
        /// </summary>
        private void LoadBasicData()
        {
        }

        private void SocketLog(BowlFeederID ID, string msg)
        {
            switch (ID)
            {
                case BowlFeederID.BF_A:
                    {
                        JLogger.LogDebug("BF_A_Socket", msg);
                    }
                    break;

                case BowlFeederID.BF_B:
                    {
                        JLogger.LogDebug("BF_B_Socket", msg);
                    }
                    break;
            }

        }
        #endregion

        #region 公用函數
        public int GetInspectionResultY(BowlFeederID id)
        {
            int iRet = 0;
            switch(id)
            {
                case BowlFeederID.BF_A:
                    {
                        if (SReadValue("EnableBFM1CCDFunction").ToBoolean() == true)
                        {
                            iRet = BF_A_InspectionResult.GetOffsetY();
                        }
                    }
                    break;
                case BowlFeederID.BF_B:
                    {
                        if (SReadValue("EnableBFM2CCDFunction").ToBoolean() == true)
                        {
                            iRet = BF_B_InspectionResult.GetOffsetY();
                        }
                    }
                    break;
            }
            return iRet;
        }

        public void SetWorkSpeed(BowlFeederID ID)
        {
            int SPD = 10000;
            int ACC_MULTIPLE = 100000;
            int ACC_DEC = 100000;
            int SPEED_RATE = OReadValue("機台速率").ToInt();

            switch (ID)
            {
                case BowlFeederID.BF_A:
                    {
                        if (MT_BFM1_AxisX != null)
                        {
                            SPD = (SReadValue("MT_HDT_X_SPEED").ToInt() * SPEED_RATE) / 100;
                            ACC_MULTIPLE = SReadValue("MT_HDT_X_ACC_MULTIPLE").ToInt();
                            ACC_DEC = (SPD * ACC_MULTIPLE);
                            MT_BFM1_AxisX.SetSpeed(SPD);
                            MT_BFM1_AxisX.SetAcceleration(ACC_DEC);
                            MT_BFM1_AxisX.SetDeceleration(ACC_DEC);
                        }

                        if (MT_BFM1_AxisZ != null)
                        {
                            SPD = (SReadValue("MT_HDT_Z_SPEED").ToInt() * SPEED_RATE) / 100;
                            ACC_MULTIPLE = SReadValue("MT_HDT_Z_ACC_MULTIPLE").ToInt();
                            ACC_DEC = (SPD * ACC_MULTIPLE);
                            MT_BFM1_AxisZ.SetSpeed(SPD);
                            MT_BFM1_AxisZ.SetAcceleration(ACC_DEC);
                            MT_BFM1_AxisZ.SetDeceleration(ACC_DEC);
                        }

                        if (MT_BFM1_AxisU != null)
                        {
                            SPD = (SReadValue("MT_HDT_U_SPEED").ToInt() * SPEED_RATE) / 100;
                            ACC_MULTIPLE = SReadValue("MT_HDT_U_ACC_MULTIPLE").ToInt();
                            ACC_DEC = (SPD * ACC_MULTIPLE);
                            MT_BFM1_AxisU.SetSpeed(SPD);
                            MT_BFM1_AxisU.SetAcceleration(ACC_DEC);
                            MT_BFM1_AxisU.SetDeceleration(ACC_DEC);
                        }
                    }
                    break;

                case BowlFeederID.BF_B:
                    {
                        if (MT_BFM2_AxisX != null)
                        {
                            SPD = (SReadValue("MT_HDT_X_SPEED").ToInt() * SPEED_RATE) / 100;
                            ACC_MULTIPLE = SReadValue("MT_HDT_X_ACC_MULTIPLE").ToInt();
                            ACC_DEC = (SPD * ACC_MULTIPLE);
                            MT_BFM2_AxisX.SetSpeed(SPD);
                            MT_BFM2_AxisX.SetAcceleration(ACC_DEC);
                            MT_BFM2_AxisX.SetDeceleration(ACC_DEC);
                        }

                        if (MT_BFM2_AxisZ != null)
                        {
                            SPD = (SReadValue("MT_HDT_Z_SPEED").ToInt() * SPEED_RATE) / 100;
                            ACC_MULTIPLE = SReadValue("MT_HDT_Z_ACC_MULTIPLE").ToInt();
                            ACC_DEC = (SPD * ACC_MULTIPLE);
                            MT_BFM2_AxisZ.SetSpeed(SPD);
                            MT_BFM2_AxisZ.SetAcceleration(ACC_DEC);
                            MT_BFM2_AxisZ.SetDeceleration(ACC_DEC);
                        }

                        if (MT_BFM2_AxisU != null)
                        {
                            SPD = (SReadValue("MT_HDT_U_SPEED").ToInt() * SPEED_RATE) / 100;
                            ACC_MULTIPLE = SReadValue("MT_HDT_U_ACC_MULTIPLE").ToInt();
                            ACC_DEC = (SPD * ACC_MULTIPLE);
                            MT_BFM2_AxisU.SetSpeed(SPD);
                            MT_BFM2_AxisU.SetAcceleration(ACC_DEC);
                            MT_BFM2_AxisU.SetDeceleration(ACC_DEC);
                        }
                    }
                    break;
            }
        }

        private void SetHdtPnPLowSpeed(BowlFeederID ID, ACTIONMODE mode)
        {
            int SPD = 10000;
            int ACC_MULTIPLE = 7;
            int ACC_DEC = 100000;
            int SPEED_RATE = OReadValue("機台速率").ToInt();

            switch (mode)
            {
                case ACTIONMODE.PICKUP:
                    {
                        SPD = (PReadValue("DT_HDT_BF_SlowSpeed_BowlFeeder").ToInt() * SPEED_RATE) / 100;
                        ACC_MULTIPLE = SReadValue("MT_HDT_Z_ACC_MULTIPLE").ToInt();
                        ACC_DEC = (SPD * ACC_MULTIPLE);
                    }
                    break;

                case ACTIONMODE.PLACEMENT:
                    {
                        SPD = (PReadValue("DT_HDT_BF_SlowSpeed").ToInt() * SPEED_RATE) / 100;
                        ACC_MULTIPLE = SReadValue("MT_HDT_Z_ACC_MULTIPLE").ToInt();
                        ACC_DEC = (SPD * ACC_MULTIPLE);
                    }
                    break;
            }


            switch (ID)
            {
                case BowlFeederID.BF_A:
                    {
                        if (MT_BFM1_AxisZ != null)
                        {
                            MT_BFM1_AxisZ.SetSpeed(SPD);
                            MT_BFM1_AxisZ.SetAcceleration(ACC_DEC);
                            MT_BFM1_AxisZ.SetDeceleration(ACC_DEC);
                        }
                    }
                    break;

                case BowlFeederID.BF_B:
                    {
                        if (MT_BFM2_AxisZ != null)
                        {
                            MT_BFM2_AxisZ.SetSpeed(SPD);
                            MT_BFM2_AxisZ.SetAcceleration(ACC_DEC);
                            MT_BFM2_AxisZ.SetDeceleration(ACC_DEC);
                        }
                    }
                    break;
            }
        }

        private void SetTriggerSpeed(BowlFeederID ID)
        {
            int SPD = 10000;
            int ACC_MULTIPLE = 7;
            int ACC_DEC = 100000;
            int SPEED_RATE = OReadValue("機台速率").ToInt();
            SPD = (SReadValue("MT_HDT_X_TriggerSPEED").ToInt() * SPEED_RATE) / 100;
            ACC_MULTIPLE = SReadValue("MT_HDT_X_TriggerACC_MULTIPLE").ToInt();
            ACC_DEC = (SPD * ACC_MULTIPLE);

            switch (ID)
            {
                case BowlFeederID.BF_A:
                    {
                        if (MT_BFM1_AxisX != null)
                        {
                            MT_BFM1_AxisX.SetSpeed(SPD);
                            MT_BFM1_AxisX.SetAcceleration(ACC_DEC);
                            MT_BFM1_AxisX.SetDeceleration(ACC_DEC);
                        }
                    }
                    break;

                case BowlFeederID.BF_B:
                    {
                        if (MT_BFM2_AxisX != null)
                        {
                            MT_BFM2_AxisX.SetSpeed(SPD);
                            MT_BFM2_AxisX.SetAcceleration(ACC_DEC);
                            MT_BFM2_AxisX.SetDeceleration(ACC_DEC);
                        }
                    }
                    break;
            }
        }

        public BasePosInfo GetBasicPos(BowlFeederID ID, KitShuttleID ShuttleID)
        {
            BasePosInfo rt = new BasePosInfo();
            switch (ID)
            {
                case BowlFeederID.BF_A:
                    {
                        switch (ShuttleID)
                        {
                            case KitShuttleID.TransferShuttleA:
                                {
                                    rt.X = SReadValue("Pos_HDT1_ShuttleAPlace_X").ToInt();
                                    rt.Z = SReadValue("Pos_HDT1_Shuttle_A_Place_Z").ToInt();
                                }
                                break;

                            case KitShuttleID.TransferShuttleB:
                                {
                                    rt.X = SReadValue("Pos_HDT1_ShuttleBPlace_X").ToInt();
                                    rt.Z = SReadValue("Pos_HDT1_Shuttle_B_Place_Z").ToInt();
                                }
                                break;
                        }
                    }
                    break;

                case BowlFeederID.BF_B:
                    {
                        switch (ShuttleID)
                        {
                            case KitShuttleID.TransferShuttleA:
                                {
                                    rt.X = SReadValue("Pos_HDT2_ShuttleAPlace_X").ToInt();
                                    rt.Z = SReadValue("Pos_HDT2_Shuttle_A_Place_Z").ToInt();
                                }
                                break;

                            case KitShuttleID.TransferShuttleB:
                                {
                                    rt.X = SReadValue("Pos_HDT2_ShuttleBPlace_X").ToInt();
                                    rt.Z = SReadValue("Pos_HDT2_Shuttle_B_Place_Z").ToInt();
                                }
                                break;
                        }
                    }
                    break;

                default:
                    {
                        rt.X = 0;
                        rt.Y = 0;
                        rt.Z = 0;
                        rt.U = 0;
                    }
                    break;
            }

            return rt;
        }
        #endregion

        #region 使用者函數
        public void SetCanRunHome()
        {
            mCanHome = true;
        }

        public void DataReset()
        {
            #region 模組模擬與空跑設定
            //模組模擬與空跑設定
            Nozzle_BFM1.Simulation = (IsSimulation() != 0);
            Nozzle_BFM2.Simulation = (IsSimulation() != 0);
            DI_BFM1ArrivalDetection.Simulation = (IsSimulation() != 0);
            //DI_BFM1IonizerFanSensor.Simulation = (IsSimulation() != 0);
            DI_BFM2ArrivalDetection.Simulation = (IsSimulation() != 0);
            //DI_BFM2IonizerFanSensor.Simulation = (IsSimulation() != 0);

            //Flag_BowlFeederID1Action.Reset();
            //Flag_BowlFeederID2Action.Reset();

            SystemDryRun = OReadValue("DryRun").ToBoolean();
            //Nozzle_BFM1.DryRun = SystemDryRun;
            //Nozzle_BFM2.DryRun = SystemDryRun;
            DryRunWithoutDevice = OReadValue("DryRun").ToBoolean() && OReadValue("DryRun_NoDevice").ToBoolean();


            bNotUseBF1 = SReadValue("DoNotUseBFM1Module").ToBoolean();
            bNotUseBF2 = SReadValue("DoNotUseBFM2Module").ToBoolean();
            #endregion
        }

        public bool IsUseBFM(BowlFeederID id)
        {
            bool bRet = false;
            switch (id)
            {
                case BowlFeederID.BF_A:
                    {
                        bRet = !bNotUseBF1;
                    }
                    break;
                case BowlFeederID.BF_B:
                    {
                        bRet = !bNotUseBF2;
                    }
                    break;
            }
            return bRet;
        }

        public ThreeValued IsAxisZHomeOK(BowlFeederID ID)
        {
            if (GetUseModule() == false)
                return ThreeValued.TRUE;
            ThreeValued TRet = ThreeValued.UNKNOWN;
            switch (ID)
            {
                case BowlFeederID.BF_A:
                    {
                        if (!GetUseModule())
                        {
                            return ThreeValued.TRUE;
                        }
                        if (mBFM1AxisZHomeOK) TRet = ThreeValued.TRUE;
                        else if (!mBFM1AxisZHomeOK) TRet = ThreeValued.FALSE;
                        return TRet;
                    }
                    break;

                case BowlFeederID.BF_B:
                    {
                        if (!GetUseModule())
                        {
                            return ThreeValued.TRUE;
                        }
                        if (mBFM2AxisZHomeOK) TRet = ThreeValued.TRUE;
                        else if (!mBFM2AxisZHomeOK) TRet = ThreeValued.FALSE;
                        return TRet;
                    }
                    break;
            }
            return ThreeValued.FALSE;
        }

        /// <summary>
        /// 判斷Z軸是否安全
        /// /<returns></returns>
        public bool IsAxisZSafety(BowlFeederID ID)
        {
            if (GetUseModule() == false) return true;
            switch (ID)
            {
                case BowlFeederID.BF_A:
                    {
                        if (!this.IsSimulation().Equals(0))
                        {
                            return true;    //模擬模式不用保護
                        }

                        if (MT_BFM1_AxisZ != null)
                        {
                            //bool b1 = !MT_BFM1_AxisZ.IsHomeOn;
                            bool b1 = false;
                            int AxisZ_Pos = MT_BFM1_AxisZ.ReadEncPos();
                            int AxisZSaftyPosZ = SReadValue("Pos_HDT1_Safe_Z").ToInt();

                            if (AxisZ_Pos < (AxisZSaftyPosZ - 500) || b1)
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                    break;

                case BowlFeederID.BF_B:
                    {
                        if (!this.IsSimulation().Equals(0))
                        {
                            return true;    //模擬模式不用保護
                        }

                        if (MT_BFM2_AxisZ != null)
                        {
                            //bool b1 = !MT_BFM2_AxisZ.IsHomeOn;
                            bool b1 = false;
                            int AxisZ_Pos = MT_BFM2_AxisZ.ReadEncPos();
                            int AxisZSaftyPosZ = SReadValue("Pos_HDT1_Safe_Z").ToInt();

                            if (AxisZ_Pos < (AxisZSaftyPosZ - 500) || b1)
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                    break;
            }
            return false;
        }

        public ThreeValued SetActionCommand_BFM(BowlFeederID id, ACTIONMODE mode, BasePosInfo pos)
        {
            switch (id)
            {
                case BowlFeederID.BF_A:
                    {
                        if (!GetUseModule() || mode.Equals(ACTIONMODE.NONE))
                        {
                            Flag_BowlFeederID1Action.Done();
                            return ThreeValued.TRUE;
                        }

                        //判斷前次動作是否完成
                        if (!Flag_BowlFeederID1Action.IsDoing())
                        {
                            BF_A_PnPPos = pos;
                            BowlFeederID1ActionMode = mode;
                            Flag_BowlFeederID1Action.DoIt();
                            return ThreeValued.TRUE;
                        }
                        return ThreeValued.FALSE;
                    }
                    //break;

                case BowlFeederID.BF_B:
                    {
                        if (!GetUseModule() || mode.Equals(ACTIONMODE.NONE))
                        {
                            Flag_BowlFeederID2Action.Done();
                            return ThreeValued.TRUE;
                        }

                        //判斷前次動作是否完成
                        if (!Flag_BowlFeederID2Action.IsDoing())
                        {
                            BF_B_PnPPos = pos;
                            BowlFeederID2ActionMode = mode;
                            Flag_BowlFeederID2Action.DoIt();
                            return ThreeValued.TRUE;
                        }
                        return ThreeValued.FALSE;
                    }
                    //break;
            }
            return ThreeValued.FALSE;
        }

        public ThreeValued GetActionResult_BFM(BowlFeederID id)
        {
            switch (id)
            {
                case BowlFeederID.BF_A:
                    {
                        if (Flag_BowlFeederID1Action.IsDone())
                        {
                            return ThreeValued.TRUE;
                        }
                        else
                        {
                            return ThreeValued.FALSE;
                        }
                    }
                    //break;

                case BowlFeederID.BF_B:
                    {
                        if (Flag_BowlFeederID2Action.IsDone())
                        {
                            return ThreeValued.TRUE;
                        }
                        else
                        {
                            return ThreeValued.FALSE;
                        }
                    }
                    //break;
            }
            return ThreeValued.FALSE;
        }

        public bool ChkBowlArrivalDetectionState(BowlFeederID NO, EOnOffState state)
        {
            switch (NO)
            {
                case BowlFeederID.BF_A:
                    {
                        if (!this.IsSimulation().Equals(0) || DryRunWithoutDevice)
                        {
                            return true;    //模擬模式不用保護
                        }

                        if (DI_BFM1ArrivalDetection != null)
                        {
                            switch (state)
                            {
                                case EOnOffState.ON:
                                    {
                                        return DI_BFM1ArrivalDetection.ValueOn;
                                    }
                                    break;

                                case EOnOffState.OFF:
                                    {
                                        return DI_BFM1ArrivalDetection.ValueOff;
                                    }
                                    break;
                            }

                        }

                    }
                    break;

                case BowlFeederID.BF_B:
                    {
                        if (!this.IsSimulation().Equals(0) || DryRunWithoutDevice)
                        {
                            return true;    //模擬模式不用保護
                        }

                        if (DI_BFM1ArrivalDetection != null)
                        {
                            switch (state)
                            {
                                case EOnOffState.ON:
                                    {
                                        return DI_BFM2ArrivalDetection.ValueOn;
                                    }
                                    break;

                                case EOnOffState.OFF:
                                    {
                                        return DI_BFM2ArrivalDetection.ValueOff;
                                    }
                                    break;
                            }
                        }
                    }
                    break;
            }

            return false;
        }

        public void CtlBFMBowelFrontSuction(BowlFeederID NO, EOnOffState state)
        {
            switch (NO)
            {
                case BowlFeederID.BF_A:
                    {
                        if (ob_Bowl1_FrontSuction != null)
                        {
                            switch (state)
                            {
                                case EOnOffState.ON:
                                    {
                                        ob_Bowl1_FrontSuction.On();
                                        //ob_Bowl1_FrontSuction.Value = true;
                                    }
                                    break;

                                case EOnOffState.OFF:
                                    {
                                        ob_Bowl1_FrontSuction.Off();
                                        //ob_Bowl1_FrontSuction.Value = false;
                                    }
                                    break;
                            }
                        }
                    }
                    break;

                case BowlFeederID.BF_B:
                    {
                        if (ob_Bowl2_FrontSuction != null)
                        {
                            switch (state)
                            {
                                case EOnOffState.ON:
                                    {
                                        ob_Bowl2_FrontSuction.On();
                                        //ob_Bowl2_FrontSuction.Value = true;
                                    }
                                    break;

                                case EOnOffState.OFF:
                                    {
                                        ob_Bowl2_FrontSuction.Off();
                                        //ob_Bowl2_FrontSuction.Value = false;
                                    }
                                    break;
                            }
                        }
                    }
                    break;
            }
        }

        public void CtlBFMBowelDownSuction(BowlFeederID NO, EOnOffState state)
        {
            switch (NO)
            {
                case BowlFeederID.BF_A:
                    {
                        if (ob_Bowl1_DownSuction != null)
                        {
                            switch (state)
                            {
                                case EOnOffState.ON:
                                    {
                                        ob_Bowl1_DownSuction.On(100);
                                        //ob_Bowl1_DownSuction.Value = true;
                                    }
                                    break;

                                case EOnOffState.OFF:
                                    {
                                        ob_Bowl1_DownSuction.Off(100);
                                        //ob_Bowl1_DownSuction.Value = false;
                                    }
                                    break;
                            }
                        }
                    }
                    break;

                case BowlFeederID.BF_B:
                    {
                        if (ob_Bowl2_DownSuction != null)
                        {
                            switch (state)
                            {
                                case EOnOffState.ON:
                                    {
                                        ob_Bowl2_DownSuction.On();
                                        //ob_Bowl2_DownSuction.Value = true;
                                    }
                                    break;

                                case EOnOffState.OFF:
                                    {
                                        ob_Bowl2_DownSuction.Off();
                                        //ob_Bowl2_DownSuction.Value = false;
                                    }
                                    break;
                            }
                        }
                    }
                    break;
            }
        }

        public void CtlBFMBowelBackSuction(BowlFeederID NO, EOnOffState state)
        {
            switch (NO)
            {
                case BowlFeederID.BF_A:
                    {
                        if (ob_Bowl1_BackSuction != null)
                        {
                            switch (state)
                            {
                                case EOnOffState.ON:
                                    {
                                        ob_Bowl1_BackSuction.On(100);
                                        //ob_Bowl1_BackSuction.Value = true;
                                    }
                                    break;

                                case EOnOffState.OFF:
                                    {
                                        ob_Bowl1_BackSuction.Off(100);
                                        //ob_Bowl1_BackSuction.Value = false;
                                    }
                                    break;
                            }
                        }
                    }
                    break;

                case BowlFeederID.BF_B:
                    {
                        if (ob_Bowl2_BackSuction != null)
                        {
                            switch (state)
                            {
                                case EOnOffState.ON:
                                    {
                                        ob_Bowl2_BackSuction.On();
                                        //ob_Bowl2_BackSuction.Value = true;
                                    }
                                    break;

                                case EOnOffState.OFF:
                                    {
                                        ob_Bowl2_BackSuction.Off();
                                        //ob_Bowl2_BackSuction.Value = false;
                                    }
                                    break;
                            }
                        }
                    }
                    break;
            }
        }

        public void CtlBFMBowelVibratorSW(BowlFeederID NO, EOnOffState state)
        {
            switch (NO)
            {
                case BowlFeederID.BF_A:
                    {
                        if (ob_Bowl1_VibratorSW != null)
                        {
                            switch (state)
                            {
                                case EOnOffState.ON:
                                    {
                                        ob_Bowl1_VibratorSW.On();
                                        //ob_Bowl1_VibratorSW.Value = true;
                                    }
                                    break;

                                case EOnOffState.OFF:
                                    {
                                        ob_Bowl1_VibratorSW.Off();
                                        //ob_Bowl1_VibratorSW.Value = false;
                                    }
                                    break;
                            }
                        }
                    }
                    break;

                case BowlFeederID.BF_B:
                    {
                        if (ob_Bowl2_VibratorSW != null)
                        {
                            switch (state)
                            {
                                case EOnOffState.ON:
                                    {
                                        ob_Bowl2_VibratorSW.On();
                                        //ob_Bowl2_VibratorSW.Value = true;
                                    }
                                    break;

                                case EOnOffState.OFF:
                                    {
                                        ob_Bowl2_VibratorSW.Off();
                                        //ob_Bowl2_VibratorSW.Value = false;
                                    }
                                    break;
                            }
                        }
                    }
                    break;
            }
        }

        public void CtlIonizerBar(EOnOffState state)
        {
            switch (state)
            {
                case EOnOffState.ON:
                    {
                        ob_IonizerBarCtl.On();
                        //ob_IonizerBarCtl.Value = true;
                    }
                    break;

                case EOnOffState.OFF:
                    {
                        ob_IonizerBarCtl.Off();
                        //ob_IonizerBarCtl.Value = false;
                    }
                    break;
            }
        }

        public bool HdtAxisUMove(BowlFeederID ID, double deg)
        {
            bool b1 = false;
            switch (ID)
            {
                case BowlFeederID.BF_A:
                    {
                        if (MT_BFM1_AxisU != null)
                        {
                            b1 = MT_BFM1_AxisU.G00(CalAxisUMovePos(ID, deg));
                        }
                    }
                    break;

                case BowlFeederID.BF_B:
                    {
                        if (MT_BFM1_AxisU != null)
                        {
                            b1 = MT_BFM2_AxisU.G00(CalAxisUMovePos(ID, deg));
                        }
                    }
                    break;
            }
            return b1;
        }

        public int CalAxisUMovePos(BowlFeederID ID, double deg)
        {
            int CalPos = 0;
            try
            {
                switch (ID)
                {
                    case BowlFeederID.BF_A:
                        {                            
                            int BasePos = SReadValue("Pos_HDT1_Zero_U").ToInt();
                            if (deg != 0)
                            {
                                //int degToPos = deg / SReadValue("MT_HDT_U_RESOLUTION").ToInt();
                                double resolution = SReadValue("MT_HDT_U_RESOLUTION").ToDouble();
                                double degToPos = deg * (resolution / 360);
                                CalPos = BasePos + (int)degToPos;
                            }
                            else
                            {
                                CalPos = BasePos;
                            }
                        }
                        break;

                    case BowlFeederID.BF_B:
                        {
                            int BasePos = SReadValue("Pos_HDT2_Zero_U").ToInt();
                            if (deg != 0)
                            {
                                //int degToPos = deg / SReadValue("MT_HDT_U_RESOLUTION").ToInt();
                                double resolution = SReadValue("MT_HDT_U_RESOLUTION").ToDouble();
                                double degToPos = deg * (resolution / 360);
                                CalPos = BasePos + (int)degToPos;
                            }
                            else
                            {
                                CalPos = BasePos;
                            }
                            
                            
                        }
                        break;

                       
                }
                return CalPos;
                
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool HdtAxisXMove(BowlFeederID ID, int pos)
        {
            bool b1 = false;
            if (IsSimulation() != 0) return true;
            switch (ID)
            {
                case BowlFeederID.BF_A:
                    {
                        if (MT_BFM1_AxisX != null)
                        {
                            b1 = MT_BFM1_AxisX.G00(pos);
                        }
                    }
                    break;

                case BowlFeederID.BF_B:
                    {
                        if (MT_BFM2_AxisX != null)
                        {
                            b1 = MT_BFM2_AxisX.G00(pos);
                        }
                    }
                    break;
            }
            return b1;
        }

        private void CallPPFAForm(PP_FAILURE_ACTION_TYPE type, string message)
        {
            if (PPFAFormType.Equals(PP_FAILURE_ACTION_TYPE.ppNone))
            {
                BFM_PnPDialogMessage = message;
                PPFAFormType = type;
                ShowPPFAForm = true;
            }
        }

        private void CallBFAForm(BF_FAILURE_ACTION_TYPE type, string message)
        {
            if (BFAFormType.Equals(BF_FAILURE_ACTION_TYPE.bfNone))
            {
                BFM_BFDialogMessage = message;
                BFAFormType = type;
                ShowBFAForm = true;
            }
        }
        #endregion

        #region Home Flowchart
        private FlowChart.FCRESULT FC_Home_HDT_Home_Start_Run()
        {
            if (MT_BFM1_AxisZ != null)
            {
                MT_BFM1_AxisZ.HomeReset();
            }
            JLogger.LogDebug("BF_A", string.Format("Home - HDT1 START"));
            T_BLM1Home.Restart();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Home_HDT_Home_ZHome_Run()
        {
            bool b1 = false;
            if (MT_BFM1_AxisZ != null)
            {
                b1 = MT_BFM1_AxisZ.Home();
            }
            else
            {
                b1 = true;
            }

            if (b1 == true)
            {
                if (MT_BFM1_AxisX != null)
                {
                    MT_BFM1_AxisX.HomeReset();
                }

                if (MT_BFM1_AxisU != null)
                {
                    MT_BFM1_AxisU.HomeReset();
                }

                JLogger.LogDebug("BF_A", string.Format("Home - HDT1 Z Home"));
                mBFM1AxisZHomeOK = true;
                T_BLM1Home.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Home_HDT_Home_XUHome_Run()
        {
            bool b1 = false;
            if (MT_BFM1_AxisX != null)
            {
                b1 = MT_BFM1_AxisX.Home();
            }
            else
            {
                b1 = true;
            }

            bool b2 = false;
            if (MT_BFM1_AxisU != null)
            {
                b2 = MT_BFM1_AxisU.Home();
            }
            else
            {
                b2 = true;
            }

            if (b1 == true && b2 == true)
            {
                JLogger.LogDebug("BF_A", string.Format("Home - HDT1 X Home and U Home"));
                T_BLM1Home.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Home_HDT_Home_SetXUAxisReady_Run()
        {
            bool b1 = false;
            if (MT_BFM1_AxisX != null)
            {
                b1 = MT_BFM1_AxisX.G00(SReadValue("Pos_HDT1_Pick_X").ToInt());
            }
            else
            {
                b1 = true;
            }

            bool b2 = false;
            if (MT_BFM1_AxisU != null)
            {
                b2 = MT_BFM1_AxisU.G00(SReadValue("Pos_HDT1_Zero_U").ToInt());
            }
            else
            {
                b2 = true;
            }


            if (b1 == true && b2 == true)
            {
                JLogger.LogDebug("BF_A", string.Format("Home - HDT1 Set X  and U Ready"));
                T_BLM1Home.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Home_HDT_Home_VacOn_Run()
        {
            if (Nozzle_BFM1 != null)
            {
                Nozzle_BFM1.VacuumOn();
            }

            JLogger.LogDebug("BF_A", string.Format("Home - HDT1 Vac. On"));
            T_BLM1Home.Restart();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Home_HDT_Home_CheckVac_Run()
        {
            bool b1 = false;

            ThreeValued r1 = Nozzle_BFM1.VacuumOnAndCheck();


            if (r1.Equals(ThreeValued.TRUE))
            {// 真空有建立，吸嘴上有產品
                return FlowChart.FCRESULT.CASE1;
            }
            else if (r1.Equals(ThreeValued.FALSE))
            {
                b1 = true;
            }

            if (b1 == true)
            {
                Nozzle_BFM1.VacuumOff();
                JLogger.LogDebug("BF_A", string.Format("Home - HDT1 Check Vac."));
                T_BLM1Home.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Home_HDT_Home_DestoryOn_Run()
        {
            bool b1 = Nozzle_BFM1.DestoryOn(SReadValue("GarbageCanDestoryTime").ToInt());

            if (b1 == true)
            {
                Nozzle_BFM1.DestoryOff();
                JLogger.LogDebug("BF_A", string.Format("Home - HDT1 Destory On"));
                T_BLM1Home.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Home_HDT_Home_VisionSetJob_Run()
        {
            if (SReadValue("EnableBFM1CCDFunction").ToBoolean() == true)
            {
                BF_A_ClientSocket.IP = SReadValue("BFM1CcdIP").ToString();
                BF_A_ClientSocket.Port = SReadValue("BFM1CcdPortNo").ToInt();
                BF_A_ClientSocket.Connect();
                JLogger.LogDebug("BF_A", string.Format("Home - HDT1 Use CCD Connection"));
            }
            T_BLM1Home.Restart();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Home_HDT_Home_Done_Run()
        {
            if (mBFM1HdtHomeOk == false)
            {
                JLogger.LogDebug("BF_A", string.Format("Home - HDT1 Done"));
                mBFM1HdtHomeOk = true;
            }
            
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Home_HDT2_Home_Start_Run()
        {
            if (MT_BFM2_AxisZ != null)
            {
                MT_BFM2_AxisZ.HomeReset();
            }
            JLogger.LogDebug("BF_B", string.Format("Home - HDT2 START"));
            T_BLM2Home.Restart();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Home_HDT2_Home_ZHome_Run()
        {
            bool b1 = false;
            if (MT_BFM2_AxisZ != null)
            {
                b1 = MT_BFM2_AxisZ.Home();
            }
            else
            {
                b1 = true;
            }

            if (b1 == true)
            {
                if (MT_BFM2_AxisX != null)
                {
                    MT_BFM2_AxisX.HomeReset();
                }

                if (MT_BFM2_AxisU != null)
                {
                    MT_BFM2_AxisU.HomeReset();
                }

                JLogger.LogDebug("BF_B", string.Format("Home - HDT2 Z Home"));
                mBFM2AxisZHomeOK = true;
                T_BLM2Home.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Home_HDT2_Home_XUHome_Run()
        {
            bool b1 = false;
            if (MT_BFM2_AxisX != null)
            {
                b1 = MT_BFM2_AxisX.Home();
            }
            else
            {
                b1 = true;
            }

            bool b2 = false;
            if (MT_BFM2_AxisU != null)
            {
                b2 = MT_BFM2_AxisU.Home();
            }
            else
            {
                b2 = true;
            }

            if (b1 == true && b2 == true)
            {
                JLogger.LogDebug("BF_B", string.Format("Home - HDT2 X Home and U Home"));
                T_BLM2Home.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;

        }

        private FlowChart.FCRESULT FC_Home_HDT2_Home_SetXUAxisReady_Run()
        {
            bool b1 = false;
            if (MT_BFM2_AxisX != null)
            {
                b1 = MT_BFM2_AxisX.G00(SReadValue("Pos_HDT2_Pick_X").ToInt());
            }
            else
            {
                b1 = true;
            }

            bool b2 = false;
            if (MT_BFM2_AxisU != null)
            {
                b2 = MT_BFM2_AxisU.G00(SReadValue("Pos_HDT2_Zero_U").ToInt());
            }
            else
            {
                b2 = true;
            }

            if (b1 == true && b2 == true)
            {
                JLogger.LogDebug("BF_B", string.Format("Home - HDT2 Set X  and U Ready"));
                T_BLM2Home.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Home_HDT2_Home_VacOn_Run()
        {
            if (Nozzle_BFM2 != null)
            {
                Nozzle_BFM2.VacuumOn();
            }

            JLogger.LogDebug("BF_B", string.Format("Home - HDT2 Vac. On"));
            T_BLM2Home.Restart();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Home_HDT2_Home_CheckVac_Run()
        {
            bool b1 = false;
            ThreeValued r1 = Nozzle_BFM2.VacuumOnAndCheck();

            if (r1.Equals(ThreeValued.TRUE))
            {// 偵測到吸嘴上有產品，移至垃圾桶丟掉
                return FlowChart.FCRESULT.CASE1;
            }
            else if (r1.Equals(ThreeValued.FALSE))
            {
                b1 = true;
            }

            if (b1 == true)
            {
                JLogger.LogDebug("BF_B", string.Format("Home - HDT2 Check Vac."));
                Nozzle_BFM2.VacuumOff();
                T_BLM2Home.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Home_HDT1_Home_MoveGarbageCanPos_Run()
        {
            bool b1 = false;
            if (MT_BFM1_AxisX != null)
            {
                b1 = MT_BFM1_AxisX.G00(SReadValue("Pos_HDT1_GarbageCan_X").ToInt());
            }
            else
            {
                b1 = true;
            }

            if (b1 == true)
            {
                Nozzle_BFM1.VacuumOff();
                JLogger.LogDebug("BF_A", string.Format("Home - HDT1 Move Garbage Can Pos"));
                T_BLM1Home.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart2_Run()
        {
            bool b1 = false;
            if (MT_BFM2_AxisX != null)
            {
                b1 = MT_BFM2_AxisX.G00(SReadValue("Pos_HDT2_GarbageCan_X").ToInt());
            }
            else
            {
                b1 = true;
            }

            if (b1 == true)
            {
                JLogger.LogDebug("BF_B", string.Format("Home - HDT2 Move Garbage Can Pos"));
                Nozzle_BFM2.VacuumOff();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Home_HDT2_Home_DestoryOn_Run()
        {
            bool b1 = Nozzle_BFM2.DestoryOn(SReadValue("GarbageCanDestoryTime").ToInt());
            if (b1 == true)
            {
                JLogger.LogDebug("BF_B", string.Format("Home - HDT2 Destory On"));
                Nozzle_BFM2.DestoryOff();
                T_BLM2Home.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Home_HDT2_Home_VisionSetJob_Run()
        {
            if (SReadValue("EnableBFM2CCDFunction").ToBoolean() == true)
            {
                BF_B_ClientSocket.IP = SReadValue("BFM2CcdIP").ToString();
                BF_B_ClientSocket.Port = SReadValue("BFM2CcdPortNo").ToInt();
                BF_B_ClientSocket.Connect();
                JLogger.LogDebug("BF_B", string.Format("Home - HDT2 Use CCD Connection"));
            }
            T_BLM2Home.Restart();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Home_HDT2_Home_Done_Run()
        {
            if (mBFM2HdtHomeOk == false)
            {
                JLogger.LogDebug("BF_B", string.Format("Home - HDT2 Done"));
                mBFM2HdtHomeOk = true;
            }
            
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Home_BF1_Home_Start_Run()
        {
            JLogger.LogDebug("BF_A", string.Format("Home - BF1 START"));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Home_BF1_Home_ShakeOn_Run()
        {
            CtlBFMBowelVibratorSW(BowlFeederID.BF_A, EOnOffState.ON);
            JLogger.LogDebug("BF_A", string.Format("Home - BF1 Shake ON"));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Home_BF1_Home_VacOn_Run()
        {
            CtlBFMBowelFrontSuction(BowlFeederID.BF_A, EOnOffState.ON);
            CtlBFMBowelDownSuction(BowlFeederID.BF_A, EOnOffState.ON);
            CtlBFMBowelBackSuction(BowlFeederID.BF_A, EOnOffState.ON);
            JLogger.LogDebug("BF_A", string.Format("Home - BF1 Vac. ON"));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Home_BF1_Home_VacOff_Run()
        {
            ThreeValued r1 = DI_BFM1ArrivalDetection.On(1000, 5000);

            if (r1.Equals(ThreeValued.TRUE) || r1.Equals(ThreeValued.FALSE))
            {
                CtlBFMBowelFrontSuction(BowlFeederID.BF_A, EOnOffState.OFF);
                CtlBFMBowelDownSuction(BowlFeederID.BF_A, EOnOffState.OFF);
                CtlBFMBowelBackSuction(BowlFeederID.BF_A, EOnOffState.OFF);
                JLogger.LogDebug("BF_A", string.Format("Home - BF1 Vac. Off"));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Home_BF1_Home_ShakeOff_Run()
        {
            CtlBFMBowelVibratorSW(BowlFeederID.BF_A, EOnOffState.OFF);
            JLogger.LogDebug("BF_A", string.Format("Home - BF1 Shake Off"));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Home_BF1_Home_Done_Run()
        {
            if (mBFM1BfHomeOk == false)
            {
                JLogger.LogDebug("BF_A", string.Format("Home - BF1 Done"));
                mBFM1BfHomeOk = true;
            }
            
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Home_BF2_Home_Start_Run()
        {
            JLogger.LogDebug("BF_B", string.Format("Home - BF2 START"));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Home_BF2_Home_ShakeOn_Run()
        {
            CtlBFMBowelVibratorSW(BowlFeederID.BF_B, EOnOffState.ON);
            JLogger.LogDebug("BF_B", string.Format("Home - BF2 Shake ON"));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Home_BF2_Home_VacOn_Run()
        {
            CtlBFMBowelFrontSuction(BowlFeederID.BF_B, EOnOffState.ON);
            CtlBFMBowelDownSuction(BowlFeederID.BF_B, EOnOffState.ON);
            CtlBFMBowelBackSuction(BowlFeederID.BF_B, EOnOffState.ON);
            JLogger.LogDebug("BF_B", string.Format("Home - BF2 Vac. ON"));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Home_BF2_Home_VacOff_Run()
        {
            ThreeValued r1 = DI_BFM2ArrivalDetection.On(1000, 5000);

            if (r1.Equals(ThreeValued.TRUE) || r1.Equals(ThreeValued.FALSE))
            {
                CtlBFMBowelFrontSuction(BowlFeederID.BF_B, EOnOffState.OFF);
                CtlBFMBowelDownSuction(BowlFeederID.BF_B, EOnOffState.OFF);
                CtlBFMBowelBackSuction(BowlFeederID.BF_B, EOnOffState.OFF);
                JLogger.LogDebug("BF_B", string.Format("Home - BF2 Vac. Off"));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Home_BF2_Home_ShakeOff_Run()
        {
            CtlBFMBowelVibratorSW(BowlFeederID.BF_B, EOnOffState.OFF);
            JLogger.LogDebug("BF_B", string.Format("Home - BF2 Shake Off"));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Home_BF2_Home_Done_Run()
        {
            if (mBFM2BfHomeOk == false)
            {
                JLogger.LogDebug("BF_B", string.Format("Home - BF2 Done"));
                mBFM2BfHomeOk = true;
            }
            
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick_Start_Run()
        {
            if (Flag_BowlFeederID1Action.IsDoIt())
            {
                if (BowlFeederID1ActionMode.Equals(ACTIONMODE.PICKUP))
                {
                    Flag_BowlFeederID1Action.Doing();
                    JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 START"));
                    SetWorkSpeed(BowlFeederID.BF_A);
                    iBF1_PickRetryTimes = 0;
                    T_BF1_Pick.Restart();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick_CanPick_Run()
        {
            if (iBF1_InspectionFailureTimes >= SReadValue("InspectionNGOverTimes").ToInt())
            {
                JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Inspection NG Over Times"));
                T_BF1_Pick.Restart();
                return FlowChart.FCRESULT.CASE1;
            }

            bool b1 = ChkBowlArrivalDetectionState(BowlFeederID.BF_A, EOnOffState.ON);
            if (b1 == true && bBF1CanPick == true)
            {// Bowl feeder有產品
                JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Can Pick"));
                T_BF1_Pick.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick1_StartMove_Start_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Pick1_StartMove_NeedMove_Run()
        {

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick_MoveBowlFeederPos_Run()
        {
            int BasePosX = SReadValue("Pos_HDT1_Pick_X").ToInt();
            int OffsetPosX = PReadValue("Offset_HDT_BF_A_X").ToInt();
            int PosX = BasePosX + OffsetPosX;
            bool b1 = HdtAxisXMove(BowlFeederID.BF_A, PosX);
            bool b2 = HdtAxisUMove(BowlFeederID.BF_A, 0);
            if (b1 == true && b2 == true)
            {
                JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Move Bowl feeder Pos={0}", PosX));
                T_BF1_Pick.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_BF1_Start_Run()
        {
            bBF1CanPick = false;
            CtlBFMBowelVibratorSW(BowlFeederID.BF_A, EOnOffState.ON);
            T_BF1.Restart();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_BF1_HaveIC_Run()
        {
            if (mLotend == true)
            {// 收到結批命令
                return FlowChart.FCRESULT.CASE1;
            }
            
            ThreeValued rt = DI_BFM1ArrivalDetection.On(100, SReadValue("NoPartsAlarmDelayTime").ToInt());
            if (DryRunWithoutDevice) rt = ThreeValued.TRUE;

                if (rt.Equals(ThreeValued.TRUE))
            {// 有Part到達
                CtlBFMBowelFrontSuction(BowlFeederID.BF_A, EOnOffState.ON);
                T_BF1.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            else if (rt.Equals(ThreeValued.FALSE))
            {// 無Part到達且超時
                //*********************************************
                // BFAReturn 務必於 return 前做 Reset
                //*********************************************
                BFAReturn = BF_FAILURE_ACTION_TYPE.bfNone;
                string msg = "BF_A:It is detected that the vibrating plate is out of stock for a long time, please select the action.";
                CallBFAForm(BF_FAILURE_ACTION_TYPE.bfWait | BF_FAILURE_ACTION_TYPE.bfFinish, msg);
                return FlowChart.FCRESULT.CASE2;
            }
            else
            {// 無Part到達且尚未超時的條件下，讓產品流進來
                CtlBFMBowelFrontSuction(BowlFeederID.BF_A, EOnOffState.OFF);
                CtlBFMBowelDownSuction(BowlFeederID.BF_A, EOnOffState.OFF);
                CtlBFMBowelBackSuction(BowlFeederID.BF_A, EOnOffState.OFF);
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_BF1_KeepIC_Run()
        {
            bPick = false;
            bBF1CanPick = true;
            CtlBFMBowelDownSuction(BowlFeederID.BF_A, EOnOffState.ON);
            CtlBFMBowelFrontSuction(BowlFeederID.BF_A, EOnOffState.ON);
            T_BF1.Restart();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_BF1_WaittingPick_Run()
        {
            if (bBF1TakePartAway == true)
            {
                bBF1CanPick = false;
                bBF1TakePartAway = false;
                CtlBFMBowelFrontSuction(BowlFeederID.BF_A, EOnOffState.OFF);
                CtlBFMBowelDownSuction(BowlFeederID.BF_A, EOnOffState.OFF);
                CtlBFMBowelBackSuction(BowlFeederID.BF_A, EOnOffState.ON);
                
                T_BF1.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_BF1_Delay_Run()
        {
            if (T_BF1.On(SReadValue("NextPartDelayTime").ToInt()) && bPick)
            {
                T_BF1.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick_AxisZToPickPosPlusOffset_Run()
        {
            int BasePickPosZ = SReadValue("Pos_HDT1_Pick_Z").ToInt();
            int OffsetPickPosZ = PReadValue("Offset_HDT_BF_A_Z_PICKUP").ToInt();
            int SlowSpeedDistance = PReadValue("DT_HDT_BF_SlowSpeed_Dis_Pick_BowlFeeder").ToInt();
            int PickPosZ = BasePickPosZ + OffsetPickPosZ + SlowSpeedDistance;

            bool b1 = MT_BFM1_AxisZ.G00(PickPosZ);
            if (b1 == true)
            {
                SetHdtPnPLowSpeed(BowlFeederID.BF_A, ACTIONMODE.PICKUP);
                JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Axis Z to Pick Pos + Offset={0}", PickPosZ));
                T_BF1_Pick.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick_AxisZToPickPos_Run()
        {
            int BasePickPosZ = SReadValue("Pos_HDT1_Pick_Z").ToInt();
            int OffsetPickPosZ = PReadValue("Offset_HDT_BF_A_Z_PICKUP").ToInt();
            int PickPosZ = BasePickPosZ + OffsetPickPosZ;

            bool b1 = MT_BFM1_AxisZ.G00(PickPosZ);
            if (b1 == true)
            {
                JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Axis Z to Pick Pos={0}", PickPosZ));
                T_BF1_Pick.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick_UsingVaccumOn_Run()
        {
            bool b1 = Nozzle_BFM1.VacuumOn(PReadValue("DT_HDT_BF_VAC").ToInt());
            if (b1 == true || DryRunWithoutDevice)
            {
                SetWorkSpeed(BowlFeederID.BF_A);
                JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Using Vaccum On"));
                T_BF1_Pick.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        bool bPick = false;
        bool bPick2 = false;
        private FlowChart.FCRESULT FC_Pick_StartPick_AxisZToSafety_Run()
        {
            if (ob_Bowl1_BackSuction.Value == true && ob_Bowl1_DownSuction.Value == false && ob_Bowl1_FrontSuction.Value == false)
            {
                int Pos = SReadValue("Pos_HDT1_Safe_Z").ToInt();
                Pos += PReadValue("Device_Thickness").ToInt();
                //bool b1 = MT_BFM1_AxisZ.G00(SReadValue("Pos_HDT1_Safe_Z").ToInt());
                bool b1 = MT_BFM1_AxisZ.G00(Pos);
                if (b1 == true)
                {
                    bPick = true;
                    JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Axis Z to Safety"));
                    T_BF1_Pick.Restart();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick_CheckVaccumValue_Run()
        {
            ThreeValued rt = ThreeValued.UNKNOWN;
            if (DryRunWithoutDevice)
            {
                rt = ThreeValued.TRUE;
            }
            else
            {
                rt = Nozzle_BFM1.VacuumCheck();
            }

            if (rt.Equals(ThreeValued.TRUE))
            {// 真空建立成功，表示吸嘴上有產品
                Nozzle_BFM1.State = NozzleState.InUsing;
                bBF1TakePartAway = true;
                JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Check Vaccum Value"));
                T_BF1_Pick.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            else if (T_BF1_Pick.On(3000))
            {
                Nozzle_BFM1.State = NozzleState.PnPFailure;
                int OverTimes = SReadValue("BowlFeederPickRetryNGTimes").ToInt();

                if (OverTimes <= 0 || iBF1_PickRetryTimes >= OverTimes)
                {
                    iBF1_PickRetryTimes = 0;
                    //*********************************************
                    // PPFAReturn 務必於 return 前做 Reset
                    //*********************************************
                    PPFAReturn = PP_FAILURE_ACTION_TYPE.ppNone;
                    string msg = "BF_A Bowl feeder pickup failure, be check devices by manual before ignore.";
                    CallPPFAForm(PP_FAILURE_ACTION_TYPE.ppRetry | PP_FAILURE_ACTION_TYPE.ppIgnore, msg);
                    T_BF1_Pick.Restart();
                    return FlowChart.FCRESULT.CASE1;
                }
                else
                {// 吸嘴吸取失敗，進入自動重吸動作
                    Nozzle_BFM1.State = NozzleState.PnPFailure;
                    T_BF1_Pick.Restart();
                    return FlowChart.FCRESULT.CASE2;
                }

            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick_RetryOrIgnore_Run()
        {
            //取料流程 - 重試(N)或忽略(1)
            switch (PPFAReturn)
            {
                case PP_FAILURE_ACTION_TYPE.ppRetry:
                    JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Pickup process failure, Select action = [Retry]"));
                    return FlowChart.FCRESULT.NEXT;     //重試 (取料流程 - 作用中且真空建立失敗的吸嘴氣缸下降)
                //break;
                case PP_FAILURE_ACTION_TYPE.ppIgnore:
                    JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Pickup process failure, Select action = [Ignore]"));
                    return FlowChart.FCRESULT.CASE1;    //忽略 (取料流程 - 資料交換)
                //break;
                case PP_FAILURE_ACTION_TYPE.ppSearch:
                    JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Pickup process failure, Select action = [Search]"));
                    return FlowChart.FCRESULT.CASE1;    //搜尋 (取料流程 - 資料交換)
                //break;
            }
            ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM1_BowlFeederPickFailure);
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick_SetTriggerPos_Run()
        {
            ContinueRun = true;
            if (SReadValue("EnableBFM1CCDFunction").ToBoolean() == true)
            {
                SetTriggerSpeed(BowlFeederID.BF_A);
                int[] iTrigger = { SReadValue("Pos_HDT1_Grab_X").ToInt() };
                MT_BFM1_AxisX.SetTriggerByArray(iTrigger, iTrigger.Length);
                BF_A_InspectionResult.Reset();
                BF_A_ClientSocket.Socket.SendText("StartToInspectOnflyCmd");
                JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Set Trigger Pos"));
            }
            
            T_BF1_Pick.Restart();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick_MovePlaceStandbyPos_Run()
        {
            ContinueRun = true;
            int PosX = SReadValue("Pos_HDT1_WaitPlace_X").ToInt();

            bool b1 = HdtAxisXMove(BowlFeederID.BF_A, PosX);
            if (b1 == true)
            {
                MT_BFM1_AxisX.SetTriggerOff();
                SetWorkSpeed(BowlFeederID.BF_A);
                JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Move Place Standby  Pos={0}", PosX));
                T_BF1_Pick.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_BF1_PickFinished_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick_GetInspectionResult_Run()
        {
            ContinueRun = true;
            if (SReadValue("EnableBFM1CCDFunction").ToBoolean() == true)
            {
                InspectionState state = BF_A_InspectionResult.GetIsInspectionResultFinish();
                switch (state)
                {

                    case InspectionState.Success:
                    case InspectionState.Fail:
                        {
                            JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Get Inspection Result Success"));
                            T_BF1_Pick.Restart();
                            ContinueRun = false;
                            return FlowChart.FCRESULT.NEXT;
                        }
                        break;

                    case InspectionState.Error:
                        {
                            JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Get Inspection Result Faile"));
                            T_BF1_Pick.Restart();
                            ContinueRun = false;
                            return FlowChart.FCRESULT.CASE1;
                        }
                        break;
                }

                if (T_BF1.On(SReadValue("InspectionNGResponseTimes").ToInt()))
                {
                    ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM1_WaitCcdResultOverTime);
                    JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Wait Inspection Result Over Time"));
                    T_BF1_Pick.Restart();
                    ContinueRun = false;
                    return FlowChart.FCRESULT.CASE1;
                }
            }
            else
            {
                T_BF1_Pick.Restart();
                ContinueRun = false;
                return FlowChart.FCRESULT.NEXT;
            }
            
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick_UAxisRotation_Run()
        {
            return FlowChart.FCRESULT.NEXT;
            //int angle = BF_A_InspectionResult.GetAngle();
            //bool b1 = HdtAxisUMove(BowlFeederID.BF_A, angle);

            //if (b1 == true)
            //{
            //    JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 U Axis Rotation"));
            //    T_BF1_Pick.Restart();
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick_Done_Run()
        {

            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick_MoveGarbageCanPos_Run()
        {
            int PosX = SReadValue("Pos_HDT1_GarbageCan_X").ToInt();

            bool b1 = HdtAxisXMove(BowlFeederID.BF_A, PosX);
            if (b1 == true)
            {
                JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Move GarbageCan Pos"));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick_CheckNozzle_Run()
        {
            ThreeValued rt = Nozzle_BFM1.VacuumCheck(50, 100);

            if (rt.Equals(ThreeValued.FALSE))
            {
                Nozzle_BFM1.VacuumOff();
                JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Check Nozzle"));
                return FlowChart.FCRESULT.NEXT;
            }
            else if (rt.Equals(ThreeValued.TRUE))
            {
                ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM1_CheckNozzleFindProductAfterGarbageCan);
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick_NozzleVacON_Run()
        {
            bool b1 = Nozzle_BFM1.VacuumOn(PReadValue("DT_HDT_BF_VAC").ToInt());
            if (b1 == true)
            {
                JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Nozzle Vac ON"));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick_NozzleDestory_Run()
        {
            int DestoryDelayTime = SReadValue("GarbageCanDestoryTime").ToInt();
            if (Nozzle_BFM1.DestoryOn(DestoryDelayTime))
            {
                JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Nozzle Destory"));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart3_Run()
        {
            Flag_BowlFeederID1Action.Done();
            JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Done"));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace1_Start_Run()
        {
            if (Flag_BowlFeederID1Action.IsDoIt())
            {
                if (BowlFeederID1ActionMode.Equals(ACTIONMODE.PLACEMENT))
                {
                    Flag_BowlFeederID1Action.Doing();
                    JLogger.LogDebug("BF_A", string.Format("Place - Hdt1 START"));
                    SetWorkSpeed(BowlFeederID.BF_A);
                    T_BF1_Place.Restart();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace1_CheckUsingVac_Run()
        {
            ThreeValued rt = ThreeValued.UNKNOWN;
            if (DryRunWithoutDevice)
            {
                rt = ThreeValued.TRUE;
            }
            else
            {
                rt = Nozzle_BFM1.VacuumCheck();
            }

            if (rt.Equals(ThreeValued.TRUE))
            {
                JLogger.LogDebug("BF_A", string.Format("Place - Hdt1 Check Using Vac."));
                T_BF1_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            //else if (rt.Equals(ThreeValued.FALSE))
            else
            {
                //*********************************************
                // PPFAReturn 務必於 return 前做 Reset
                //*********************************************
                PPFAReturn = PP_FAILURE_ACTION_TYPE.ppNone;
                string msg = "BF_A shuttle place failure, be check devices by manual before ignore.";
                CallPPFAForm(PP_FAILURE_ACTION_TYPE.ppRetry | PP_FAILURE_ACTION_TYPE.ppIgnore, msg);
                T_BF1_Place.Restart();
                return FlowChart.FCRESULT.CASE1;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace1_RetryOrIgnore_Run()
        {
            //放料流程 - 重試(N)或忽略(1)
            switch (PPFAReturn)
            {
                case PP_FAILURE_ACTION_TYPE.ppRetry:
                    Nozzle_BFM1.State = NozzleState.InUsing;
                    JLogger.LogDebug("BF_A", string.Format("Place - Hdt1 Place process failure, Select action = [Retry]"));
                    return FlowChart.FCRESULT.NEXT;     //重試 (取料流程 - 作用中且真空建立失敗的吸嘴氣缸下降)
                //break;
                case PP_FAILURE_ACTION_TYPE.ppIgnore:
                    Nozzle_BFM1.State = NozzleState.ICLost;
                    JLogger.LogDebug("BF_A", string.Format("Place - Hdt1 Place process failure, Select action = [Ignore]"));
                    return FlowChart.FCRESULT.CASE1;    //忽略 (取料流程 - 資料交換)
                //break;
                case PP_FAILURE_ACTION_TYPE.ppSearch:
                    JLogger.LogDebug("BF_A", string.Format("Place - Hdt1 Place process failure, Select action = [Search]"));
                    return FlowChart.FCRESULT.CASE1;    //搜尋 (取料流程 - 資料交換)
                //break;
            }
            ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM1_NozzleAbnormalityDetectedBeforePlacement);
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace1_AxisXToPlacePos_Run()
        {
            int PosX = BF_A_PnPPos.X;
            int InspectionOffsetX = BF_A_InspectionResult.GetOffsetX();
            bool b1 = HdtAxisXMove(BowlFeederID.BF_A, PosX - InspectionOffsetX);

            double angle = BF_A_InspectionResult.GetAngle();
            bool b2 = HdtAxisUMove(BowlFeederID.BF_A, angle);

            if (b1 == true && b2)
            {
                JLogger.LogDebug("BF_A", string.Format("Place - Hdt1 Axis X to Place Pos={0}", PosX));
                T_BF1_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;

        }

        private FlowChart.FCRESULT FC_Place_StartPlace1_AxisZToPlaceAddOffset_Run()
        {
            int BasePlacePosZ = BF_A_PnPPos.Z;
            int SlowSpeedDistance = PReadValue("DT_HDT_BF_SlowSpeed_Dis").ToInt();
            int PlacePosZ = BasePlacePosZ + SlowSpeedDistance;

            bool b1 = MT_BFM1_AxisZ.G00(PlacePosZ);
            if (b1 == true || IsSimulation() != 0)
            {
                SetHdtPnPLowSpeed(BowlFeederID.BF_A, ACTIONMODE.PLACEMENT);
                JLogger.LogDebug("BF_A", string.Format("Place - Hdt1 Axis Z to Place+Offset={0}", PlacePosZ));
                T_BF1_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace1_AxisZToPlace_Run()
        {
            int BasePlacePosZ = BF_A_PnPPos.Z;
            int PlacePosZ = BasePlacePosZ;

            bool b1 = MT_BFM1_AxisZ.G00(PlacePosZ);
            if (b1 == true || IsSimulation() != 0)
            {
                JLogger.LogDebug("BF_A", string.Format("Place - Hdt1 Axis Z to Place+Offset={0}", PlacePosZ));
                T_BF1_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace1_BeforeDestoryDelay_Run()
        {
            //放料流程 - Before air purge delay
            int dt = PReadValue("DT_HDT_BF_DES_BF").ToInt();
            //Jay Tsao 2017-08-21 ###Added - 放 IC 前後延遲時間
            if ((dt <= 0) || T_BF1_Place.On(dt))
            {
                JLogger.LogDebug("BF_A", string.Format("Place - Hdt1 Before Destory Delay={0}", dt));
                T_BF1_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace1_Destory_Run()
        {
            //放料流程 - Air purge ON -> delay -> OFF (作用的吸嘴開破壞)
            int DestoryDelayTime = PReadValue("DT_HDT_BF_DES").ToInt();
            if (Nozzle_BFM1.DestoryOn(DestoryDelayTime))
            {
                JLogger.LogDebug("BF_A", string.Format("Place - Hdt1 Destory={0}", DestoryDelayTime));
                T_BF1_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace1_AfterDestoryDelay_Run()
        {
            //放料流程 - After air purge delay
            int dt = PReadValue("DT_HDT_BF_DES_AF").ToInt();
            //Jay Tsao 2017-08-21 ###Added - 放 IC 前後延遲時間
            if ((dt <= 0) || T_BF1_Place.On(dt))
            {
                Nozzle_BFM1.DestoryOff();
                JLogger.LogDebug("BF_A", string.Format("Place - Hdt1 After Destory Delay={0}", dt));
                T_BF1_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace1_AxisZUpPlaceAddOffset_Run()
        {
            int BasePlacePosZ = BF_A_PnPPos.Z;
            int SlowSpeedDistance = PReadValue("DT_HDT_BF_SlowSpeed_Dis").ToInt();
            int PlacePosZ = BasePlacePosZ + SlowSpeedDistance;

            bool b1 = MT_BFM1_AxisZ.G00(PlacePosZ);
            if (b1 == true || IsSimulation() != 0)
            {
                SetWorkSpeed(BowlFeederID.BF_A);
                JLogger.LogDebug("BF_A", string.Format("Place - Hdt1 Axis Z up Place+Offset={0}", PlacePosZ));
                T_BF1_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace1_AxisZToSafetyPos_Run()
        {
            bool b1 = MT_BFM1_AxisZ.G00(SReadValue("Pos_HDT1_Safe_Z").ToInt());
            if (b1 == true || IsSimulation() != 0)
            {
                JLogger.LogDebug("BF_A", string.Format("Place - Hdt1 Axis Z to Safety Pos"));
                T_BF1_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace1_OpenVac_Run()
        {
            bool b1 = Nozzle_BFM1.VacuumOn();
            if (b1 == true)
            {
                JLogger.LogDebug("BF_A", string.Format("Place - Hdt1 Open Vac."));
                T_BF1_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace1_CheckVac_Run()
        {
            ThreeValued rt = Nozzle_BFM1.VacuumCheck(50, 100);
            if (DryRunWithoutDevice || IsSimulation() != 0) rt = ThreeValued.FALSE;
            //rt = ThreeValued.FALSE;
            if (rt.Equals(ThreeValued.TRUE))
            {// 吸嘴上仍有產品
                ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM1_CheckNozzleHaveProductAfterPlace);
            }
            else if (rt.Equals(ThreeValued.FALSE))
            {// 吸嘴上沒有產品
                Nozzle_BFM1.State = NozzleState.InUsing;
                Nozzle_BFM1.VacuumOff();
                JLogger.LogDebug("BF_A", string.Format("Place - Hdt1 Check Vac."));
                T_BF1_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick_AutoRetryAgain_Run()
        {
            Nozzle_BFM1.VacuumOff();
            iBF1_PickRetryTimes++;
            JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Auto Retry Again = {0}", iBF1_PickRetryTimes));
            T_BF1_Pick.Restart();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Home_HDT1_Home_CheckConnectionState_Run()
        {
            if (SReadValue("EnableBFM1CCDFunction").ToBoolean() == true)
            {
                if (BF_A_ClientSocket.Socket != null)
                {
                    try
                    {
                        bool b1 = BF_A_ClientSocket.Socket.Connected;

                        if (b1)
                        {
                            BF_A_InspectionResult.Reset();
                            JLogger.LogDebug("BF_A", string.Format("Home - HDT1 CCD Connection Success"));
                            return FlowChart.FCRESULT.NEXT;
                        }
                        else if (T_BLM1Home.On(1000))
                        {
                            T_BLM1Home.Restart();
                            ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM1_CCDConnectionFailure);
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM1_CCDConnectionFailure);
                        T_BLM1Home.Restart();
                    }
                }
                return FlowChart.FCRESULT.IDLE;
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Home_HDT2_Home_CheckConnectionState_Run()
        {
            if (SReadValue("EnableBFM2CCDFunction").ToBoolean() == true)
            {
                if (BF_B_ClientSocket.Socket != null)
                {
                    try
                    {
                        bool b1 = BF_B_ClientSocket.Socket.Connected;

                        if (b1)
                        {
                            JLogger.LogDebug("BF_B", string.Format("Home - HDT2 CCD Connection Success"));
                            return FlowChart.FCRESULT.NEXT;
                        }
                        else if (T_BLM1Home.On(1000))
                        {
                            T_BLM2Home.Restart();
                            ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM2_CCDConnectionFailure);
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM2_CCDConnectionFailure);
                        T_BLM2Home.Restart();
                    }
                }
                return FlowChart.FCRESULT.IDLE;
            }

            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick_InspectionNGOverTimes_Run()
        {
            ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM1_InspectionOverTimes);
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_BF1_LotEnd_Run()
        {
            CtlBFMBowelVibratorSW(BowlFeederID.BF_A, EOnOffState.OFF);
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_Start_Run()
        {
            if (Flag_BowlFeederID2Action.IsDoIt())
            {
                if (BowlFeederID2ActionMode.Equals(ACTIONMODE.PICKUP))
                {
                    Flag_BowlFeederID2Action.Doing();
                    JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 START"));
                    SetWorkSpeed(BowlFeederID.BF_B);
                    iBF2_PickRetryTimes = 0;
                    T_BF2_Pick.Restart();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_CanPick_Run()
        {
            if (iBF2_InspectionFailureTimes >= SReadValue("InspectionNGOverTimes").ToInt())
            {
                JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Inspection NG Over Times"));
                T_BF2_Pick.Restart();
                return FlowChart.FCRESULT.CASE1;
            }

            bool b1 = ChkBowlArrivalDetectionState(BowlFeederID.BF_B, EOnOffState.ON);
            if (b1 == true && bBF2CanPick == true)
            {// Bowl feeder有產品
                JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Can Pick"));
                T_BF2_Pick.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_MoveBowlFeederPos_Run()
        {
            int BasePosX = SReadValue("Pos_HDT2_Pick_X").ToInt();
            int OffsetPosX = PReadValue("Offset_HDT_BF_B_X").ToInt();
            int PosX = BasePosX + OffsetPosX;
            bool b1 = HdtAxisXMove(BowlFeederID.BF_B, PosX);
            bool b2 = HdtAxisUMove(BowlFeederID.BF_B, 0);
            if (b1 == true && b2 == true)
            {
                JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Move Bowl feeder Pos={0}", PosX));
                T_BF2_Pick.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_AxisZToPickPosPlusOffset_Run()
        {
            int BasePickPosZ = SReadValue("Pos_HDT2_Pick_Z").ToInt();
            int OffsetPickPosZ = PReadValue("Offset_HDT_BF_B_Z_PICKUP").ToInt();
            int SlowSpeedDistance = PReadValue("DT_HDT_BF_SlowSpeed_Dis_Pick_BowlFeeder").ToInt();
            int PickPosZ = BasePickPosZ + OffsetPickPosZ + SlowSpeedDistance;

            bool b1 = MT_BFM2_AxisZ.G00(PickPosZ);
            if (b1 == true)
            {
                SetHdtPnPLowSpeed(BowlFeederID.BF_B, ACTIONMODE.PICKUP);
                JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Axis Z to Pick Pos + Offset={0}", PickPosZ));
                T_BF2_Pick.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_AxisZToPickPos_Run()
        {
            int BasePickPosZ = SReadValue("Pos_HDT2_Pick_Z").ToInt();
            int OffsetPickPosZ = PReadValue("Offset_HDT_BF_B_Z_PICKUP").ToInt();
            int PickPosZ = BasePickPosZ + OffsetPickPosZ;

            bool b1 = MT_BFM2_AxisZ.G00(PickPosZ);
            if (b1 == true)
            {
                JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Axis Z to Pick Pos={0}", PickPosZ));
                T_BF2_Pick.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_UsingVaccumOn_Run()
        {
            bool b1 = Nozzle_BFM2.VacuumOn(PReadValue("DT_HDT_BF_VAC").ToInt());
            if (b1 == true || DryRunWithoutDevice)
            {
                SetWorkSpeed(BowlFeederID.BF_B);
                JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Using Vaccum On"));
                T_BF2_Pick.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_AxisZToSafety_Run()
        {
            if (ob_Bowl2_BackSuction.Value == true && ob_Bowl2_DownSuction.Value == false && ob_Bowl2_FrontSuction.Value == false)
            {
                int Pos = SReadValue("Pos_HDT2_Safe_Z").ToInt();
                Pos += PReadValue("Device_Thickness").ToInt();
                bool b1 = MT_BFM2_AxisZ.G00(Pos);
                if (b1 == true)
                {
                    bPick2 = true;
                    JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Axis Z to Safety"));
                    T_BF2_Pick.Restart();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_CheckVaccumValue_Run()
        {
            ThreeValued rt = ThreeValued.UNKNOWN;
            if (DryRunWithoutDevice)
            {
                rt = ThreeValued.TRUE;
            }
            else
            {
               rt = Nozzle_BFM2.VacuumCheck();
            }

            if (rt.Equals(ThreeValued.TRUE))
            {// 真空建立成功，表示吸嘴上有產品
                Nozzle_BFM2.State = NozzleState.InUsing;
                bBF2TakePartAway = true;
                JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Check Vaccum Value"));
                T_BF2_Pick.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            else if (T_BF2_Pick.On(3000))
            {
                Nozzle_BFM2.State = NozzleState.PnPFailure;
                int OverTimes = SReadValue("BowlFeederPickRetryNGTimes").ToInt();

                if (OverTimes <= 0 || OverTimes >= iBF1_PickRetryTimes)
                {
                    iBF1_PickRetryTimes = 0;
                    //*********************************************
                    // PPFAReturn 務必於 return 前做 Reset
                    //*********************************************
                    PPFAReturn = PP_FAILURE_ACTION_TYPE.ppNone;
                    string msg = "BF_B Bowl feeder pickup failure, be check devices by manual before ignore.";
                    CallPPFAForm(PP_FAILURE_ACTION_TYPE.ppRetry | PP_FAILURE_ACTION_TYPE.ppIgnore, msg);
                    T_BF2_Pick.Restart();
                    return FlowChart.FCRESULT.CASE1;
                }
                else
                {// 吸嘴吸取失敗，進入自動重吸動作
                    Nozzle_BFM1.State = NozzleState.PnPFailure;
                    T_BF2_Pick.Restart();
                    return FlowChart.FCRESULT.CASE2;
                }
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_RetryOrIgnore_Run()
        {
            //取料流程 - 重試(N)或忽略(1)
            switch (PPFAReturn)
            {
                case PP_FAILURE_ACTION_TYPE.ppRetry:
                    JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Pickup process failure, Select action = [Retry]"));
                    return FlowChart.FCRESULT.NEXT;     //重試 (取料流程 - 作用中且真空建立失敗的吸嘴氣缸下降)
                //break;
                case PP_FAILURE_ACTION_TYPE.ppIgnore:
                    JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Pickup process failure, Select action = [Ignore]"));
                    return FlowChart.FCRESULT.CASE1;    //忽略 (取料流程 - 資料交換)
                //break;
                case PP_FAILURE_ACTION_TYPE.ppSearch:
                    JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Pickup process failure, Select action = [Search]"));
                    return FlowChart.FCRESULT.CASE1;    //搜尋 (取料流程 - 資料交換)
                //break;
            }
            ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM2_BowlFeederPickFailure);
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_SetTriggerPos_Run()
        {
            ContinueRun = true;
            if (SReadValue("EnableBFM2CCDFunction").ToBoolean() == true)
            {
                SetTriggerSpeed(BowlFeederID.BF_B);
                int[] iTrigger = { SReadValue("Pos_HDT2_Grab_X").ToInt() };
                MT_BFM2_AxisX.SetTriggerByArray(iTrigger, iTrigger.Length);
                BF_B_InspectionResult.Reset();
                BF_B_ClientSocket.Socket.SendText("StartToInspectOnflyCmd");
                JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Set Trigger Pos"));
            }
            T_BF2_Pick.Restart();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_MovePlaceStandbyPos_Run()
        {
            ContinueRun = true;
            int PosX = SReadValue("Pos_HDT2_WaitPlace_X").ToInt();

            bool b1 = HdtAxisXMove(BowlFeederID.BF_B, PosX);
            if (b1 == true)
            {
                MT_BFM2_AxisX.SetTriggerOff();
                SetWorkSpeed(BowlFeederID.BF_B);
                JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Move Place Standby  Pos={0}", PosX));
                T_BF2_Pick.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_GetInspectionResult_Run()
        {
            ContinueRun = true;
            if (SReadValue("EnableBFM2CCDFunction").ToBoolean() == true)
            {
                InspectionState state = BF_B_InspectionResult.GetIsInspectionResultFinish();
                switch (state)
                {

                    case InspectionState.Success:
                    case InspectionState.Fail:
                        {
                            JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Get Inspection Result Success"));
                            T_BF2_Pick.Restart();
                            ContinueRun = false;
                            return FlowChart.FCRESULT.NEXT;
                        }
                        break;

                    case InspectionState.Error:
                        {
                            JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Get Inspection Result Faile"));
                            T_BF2_Pick.Restart();
                            ContinueRun = false;
                            return FlowChart.FCRESULT.CASE1;
                        }
                        break;
                }

                if (T_BF2.On(SReadValue("InspectionNGResponseTimes").ToInt()))
                {
                    ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM2_WaitCcdResultOverTime);
                    JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Wait Inspection Result Over Time"));
                    T_BF2_Pick.Restart();
                    ContinueRun = false;
                    return FlowChart.FCRESULT.CASE1;
                }
            }
            else
            {
                T_BF2_Pick.Restart();
                ContinueRun = false;
                return FlowChart.FCRESULT.NEXT;
            }
            

            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_UAxisRotation_Run()
        {
            return FlowChart.FCRESULT.NEXT;
            //int angle = BF_B_InspectionResult.GetAngle();
            //bool b1 = HdtAxisUMove(BowlFeederID.BF_B, angle);

            //if (b1 == true)
            //{
            //    JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 U Axis Rotation"));
            //    T_BF2_Pick.Restart();
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_MoveGarbageCanPos_Run()
        {
            int PosX = SReadValue("Pos_HDT2_GarbageCan_X").ToInt();

            bool b1 = HdtAxisXMove(BowlFeederID.BF_B, PosX);
            if (b1 == true)
            {
                JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Move GarbageCan Pos"));
                T_BF2_Pick.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_NozzleDestory_Run()
        {
            int DestoryDelayTime = SReadValue("GarbageCanDestoryTime").ToInt();
            if (Nozzle_BFM2.DestoryOn(DestoryDelayTime))
            {
                JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Nozzle Destory"));
                T_BF2_Pick.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_NozzleVacON_Run()
        {
            bool b1 = Nozzle_BFM1.VacuumOn(PReadValue("DT_HDT_BF_VAC").ToInt());
            if (b1 == true)
            {
                JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Nozzle Vac ON"));
                T_BF2_Pick.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_CheckNozzle_Run()
        {
            ThreeValued rt = Nozzle_BFM2.VacuumCheck(50, 100);

            if (rt.Equals(ThreeValued.FALSE))
            {
                JLogger.LogDebug("BF_B", string.Format("Pick - Hdt1 Check Nozzle"));
                T_BF2_Pick.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            else if (rt.Equals(ThreeValued.TRUE))
            {
                ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM2_CheckNozzleFindProductAfterGarbageCan);
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_AutoRetryAgain_Run()
        {
            iBF2_PickRetryTimes++;
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_InspectionNGOverTimes_Run()
        {
            ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM2_InspectionOverTimes);
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Pick_StartPick2_Done_Run()
        {
            T_BF2_Pick.Restart();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace2_Start_Run()
        {
            if (Flag_BowlFeederID2Action.IsDoIt())
            {
                if (BowlFeederID2ActionMode.Equals(ACTIONMODE.PLACEMENT))
                {
                    Flag_BowlFeederID2Action.Doing();
                    JLogger.LogDebug("BF_B", string.Format("Place - Hdt2 START"));
                    SetWorkSpeed(BowlFeederID.BF_B);
                    T_BF2_Place.Restart();
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace2_CheckUsingVac_Run()
        {
            ThreeValued rt = ThreeValued.UNKNOWN;
            if (DryRunWithoutDevice)
            {
                rt = ThreeValued.TRUE;
            }
            else
            {
                rt = Nozzle_BFM2.VacuumCheck(100, 1000);
            }

            if (rt.Equals(ThreeValued.TRUE))
            {
                JLogger.LogDebug("BF_B", string.Format("Place - Hdt2 Check Using Vac."));
                T_BF2_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            else if (rt.Equals(ThreeValued.FALSE))
            {
                //*********************************************
                // PPFAReturn 務必於 return 前做 Reset
                //*********************************************
                PPFAReturn = PP_FAILURE_ACTION_TYPE.ppNone;
                string msg = "BF_B shuttle place failure, be check devices by manual before ignore.";
                CallPPFAForm(PP_FAILURE_ACTION_TYPE.ppRetry | PP_FAILURE_ACTION_TYPE.ppIgnore, msg);
                T_BF2_Place.Restart();
                return FlowChart.FCRESULT.CASE1;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace2_AxisXToPlacePos_Run()
        {
            int PosX = BF_B_PnPPos.X;
            int InspectionOffsetX = BF_B_InspectionResult.GetOffsetX();
            bool b1 = HdtAxisXMove(BowlFeederID.BF_B, PosX - InspectionOffsetX);

            double angle = BF_B_InspectionResult.GetAngle();
            bool b2 = HdtAxisUMove(BowlFeederID.BF_B, angle);

            if (b1 == true && b2)
            {
                JLogger.LogDebug("BF_B", string.Format("Place - Hdt2 Axis X to Place Pos={0}", PosX));
                T_BF2_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace2_AxisZToPlaceAddOffset_Run()
        {
            int BasePlacePosZ = BF_B_PnPPos.Z;
            int SlowSpeedDistance = PReadValue("DT_HDT_BF_SlowSpeed_Dis").ToInt();
            int PlacePosZ = BasePlacePosZ + SlowSpeedDistance;

            bool b1 = MT_BFM2_AxisZ.G00(PlacePosZ);
            if (b1 == true)
            {
                SetHdtPnPLowSpeed(BowlFeederID.BF_B, ACTIONMODE.PLACEMENT);
                JLogger.LogDebug("BF_B", string.Format("Place - Hdt2 Axis Z to Place+Offset={0}", PlacePosZ));
                T_BF2_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace2_AxisZToPlace_Run()
        {
            int BasePlacePosZ = BF_B_PnPPos.Z;
            int PlacePosZ = BasePlacePosZ;

            bool b1 = MT_BFM2_AxisZ.G00(PlacePosZ);
            if (b1 == true)
            {
                JLogger.LogDebug("BF_B", string.Format("Place - Hdt2 Axis Z to Place+Offset={0}", PlacePosZ));
                T_BF2_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace2_BeforeDestoryDelay_Run()
        {
            //放料流程 - Before air purge delay
            int dt = PReadValue("DT_HDT_BF_DES_BF").ToInt();

            if ((dt <= 0) || T_BF2_Place.On(dt))
            {
                JLogger.LogDebug("BF_B", string.Format("Place - Hdt2 Before Destory Delay={0}", dt));
                T_BF2_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace2_Destory_Run()
        {
            //放料流程 - Air purge ON -> delay -> OFF (作用的吸嘴開破壞)
            int DestoryDelayTime = PReadValue("DT_HDT_BF_DES").ToInt();
            if (Nozzle_BFM2.DestoryOn(DestoryDelayTime))
            {
                JLogger.LogDebug("BF_B", string.Format("Place - Hdt2 Destory={0}", DestoryDelayTime));
                T_BF2_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace2_AfterDestoryDelay_Run()
        {
            //放料流程 - After air purge delay
            int dt = PReadValue("DT_HDT_BF_DES_AF").ToInt();

            if ((dt <= 0) || T_BF2_Place.On(dt))
            {
                Nozzle_BFM2.DestoryOff();
                JLogger.LogDebug("BF_B", string.Format("Place - Hdt2 After Destory Delay={0}", dt));
                T_BF2_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace2_AxisZUpPlaceAddOffset_Run()
        {
            int BasePlacePosZ = BF_B_PnPPos.Z;
            int SlowSpeedDistance = PReadValue("DT_HDT_BF_SlowSpeed_Dis").ToInt();
            int PlacePosZ = BasePlacePosZ + SlowSpeedDistance;

            bool b1 = MT_BFM2_AxisZ.G00(PlacePosZ);
            if (b1 == true)
            {
                SetWorkSpeed(BowlFeederID.BF_B);
                JLogger.LogDebug("BF_B", string.Format("Place - Hdt2 Axis Z up Place+Offset={0}", PlacePosZ));
                T_BF2_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace2_AxisZToSafetyPos_Run()
        {
            bool b1 = MT_BFM2_AxisZ.G00(SReadValue("Pos_HDT2_Safe_Z").ToInt());
            if (b1 == true)
            {
                JLogger.LogDebug("BF_B", string.Format("Place - Hdt2 Axis Z to Safety Pos"));
                T_BF2_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace2_OpenVac_Run()
        {
            bool b1 = Nozzle_BFM2.VacuumOn();
            if (b1 == true)
            {
                JLogger.LogDebug("BF_B", string.Format("Place - Hdt2 Open Vac."));
                T_BF2_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace2_CheckVac_Run()
        {
            ThreeValued rt = Nozzle_BFM2.VacuumCheck(50,100);
            if (DryRunWithoutDevice) rt = ThreeValued.FALSE;

            if (rt.Equals(ThreeValued.TRUE))
            {// 吸嘴上仍有產品
                ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM2_CheckNozzleHaveProductAfterPlace);
            }
            else if (rt.Equals(ThreeValued.FALSE))
            {// 吸嘴上沒有產品
                Nozzle_BFM2.State = NozzleState.InUsing;
                Nozzle_BFM2.VacuumOff();
                JLogger.LogDebug("BF_B", string.Format("Place - Hdt2 Check Vac."));
                T_BF2_Place.Restart();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace1_Done_Run()
        {
            Flag_BowlFeederID1Action.Done();
            JLogger.LogDebug("BF_A", string.Format("Place - Hdt1 Done"));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart4_Run()
        {
            // 偷跑回震動盤位置
            int BasePosX = SReadValue("Pos_HDT1_Pick_X").ToInt();
            HdtAxisXMove(BowlFeederID.BF_A, BasePosX);
            T_BF1_Place.Restart();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart25_Run()
        {
            Flag_BowlFeederID2Action.Done();
            JLogger.LogDebug("BF_B", string.Format("Pick - Hdt2 Done"));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart24_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart6_Run()
        {
            Flag_BowlFeederID2Action.Done();
            JLogger.LogDebug("BF_B", string.Format("Place - Hdt2 Done"));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace1_Ignore_Run()
        {
            Flag_BowlFeederID1Action.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_Place_StartPlace2_RetryOrIgnore_Run()
        {
            //放料流程 - 重試(N)或忽略(1)
            switch (PPFAReturn)
            {
                case PP_FAILURE_ACTION_TYPE.ppRetry:
                    Nozzle_BFM2.State = NozzleState.InUsing;
                    JLogger.LogDebug("BF_B", string.Format("Place - Place process failure, Select action = [Retry]"));
                    return FlowChart.FCRESULT.NEXT;     //重試 (取料流程 - 作用中且真空建立失敗的吸嘴氣缸下降)
                //break;
                case PP_FAILURE_ACTION_TYPE.ppIgnore:
                    Nozzle_BFM2.State = NozzleState.ICLost;
                    JLogger.LogDebug("BF_B", string.Format("Place - Place process failure, Select action = [Ignore]"));
                    return FlowChart.FCRESULT.CASE1;    //忽略 (取料流程 - 資料交換)
                //break;
                case PP_FAILURE_ACTION_TYPE.ppSearch:
                    JLogger.LogDebug("BF_B", string.Format("Place - Place process failure, Select action = [Search]"));
                    return FlowChart.FCRESULT.CASE1;    //搜尋 (取料流程 - 資料交換)
                //break;
            }
            ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM2_NozzleAbnormalityDetectedBeforePlacement);
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_BF2_Start_Run()
        {
            bBF2CanPick = false;
            CtlBFMBowelVibratorSW(BowlFeederID.BF_B, EOnOffState.ON);
            T_BF2.Restart();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_BF2_HaveIC_Run()
        {
            if (mLotend == true)
            {// 收到結批命令
                T_BF2.Restart();
                return FlowChart.FCRESULT.CASE1;
            }

            ThreeValued rt = DI_BFM2ArrivalDetection.On(100, SReadValue("NoPartsAlarmDelayTime").ToInt());
            if (DryRunWithoutDevice) rt = ThreeValued.TRUE;
            if (rt.Equals(ThreeValued.TRUE))
            {// 有Part到達
                CtlBFMBowelFrontSuction(BowlFeederID.BF_B, EOnOffState.ON);
                T_BF2.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            else if (rt.Equals(ThreeValued.FALSE))
            {// 無Part到達且超時
                //ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM2_BowlFeederNoPartsTimeout);
                //T_BF2.Restart();
                //*********************************************
                // BFAReturn 務必於 return 前做 Reset
                //*********************************************
                BFAReturn = BF_FAILURE_ACTION_TYPE.bfNone;
                string msg = "BF_B:It is detected that the vibrating plate is out of stock for a long time, please select the action.";
                CallBFAForm(BF_FAILURE_ACTION_TYPE.bfWait | BF_FAILURE_ACTION_TYPE.bfFinish, msg);
                return FlowChart.FCRESULT.CASE2;
            }
            else
            {// 無Part到達且尚未超時的條件下，讓產品流進來
                CtlBFMBowelFrontSuction(BowlFeederID.BF_B, EOnOffState.OFF);
                CtlBFMBowelDownSuction(BowlFeederID.BF_B, EOnOffState.OFF);
                CtlBFMBowelBackSuction(BowlFeederID.BF_B, EOnOffState.OFF);
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_BF2_KeepIC_Run()
        {
            bPick2 = false;
            bBF2CanPick = true;
            CtlBFMBowelDownSuction(BowlFeederID.BF_B, EOnOffState.ON);
            CtlBFMBowelFrontSuction(BowlFeederID.BF_B, EOnOffState.ON);
            T_BF2.Restart();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_BF2_WaittingPick_Run()
        {
            if (bBF2TakePartAway == true)
            {
                bBF2CanPick = false;
                bBF2TakePartAway = false;
                CtlBFMBowelBackSuction(BowlFeederID.BF_B, EOnOffState.ON);
                CtlBFMBowelDownSuction(BowlFeederID.BF_B, EOnOffState.OFF);
                CtlBFMBowelFrontSuction(BowlFeederID.BF_B, EOnOffState.OFF);
                T_BF2.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart8_Run()
        {
            if (T_BF2.On(SReadValue("NextPartDelayTime").ToInt()) && bPick2)
            {
                T_BF2.Restart();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_BF2_LotEnd_Run()
        {
            CtlBFMBowelVibratorSW(BowlFeederID.BF_B, EOnOffState.OFF);
            return FlowChart.FCRESULT.IDLE;
        }


        private FlowChart.FCRESULT FC_BF1_WaitOrFinish_Run()
        {
            //震動盤流程 - 等待(N)或完成(1)
            switch (BFAReturn)
            {
                case BF_FAILURE_ACTION_TYPE.bfWait:
                    JLogger.LogDebug("BF_A", string.Format("Bowl Feeder - Long time have not parts, Select action = [Wait]"));
                    return FlowChart.FCRESULT.NEXT;     // 繼續等待
                //break;
                case BF_FAILURE_ACTION_TYPE.bfFinish:
                    JLogger.LogDebug("BF_A", string.Format("Bowl Feeder - Long time have not parts, Select action = [Finish]"));
                    return FlowChart.FCRESULT.CASE1;    // 此模組已供料完成
            }
            ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM1_BowlFeederNoPartsTimeout);
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_BF1_Finish_Run()
        {
            //if (mLotend == true)
            {// 收到結批命令
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_BF2_WaitOrFinish_Run()
        {
            //震動盤流程 - 等待(N)或完成(1)
            switch (BFAReturn)
            {
                case BF_FAILURE_ACTION_TYPE.bfWait:
                    JLogger.LogDebug("BF_B", string.Format("Bowl Feeder - Long time have not parts, Select action = [Wait]"));
                    return FlowChart.FCRESULT.NEXT;     // 繼續等待
                //break;
                case BF_FAILURE_ACTION_TYPE.bfFinish:
                    JLogger.LogDebug("BF_B", string.Format("Bowl Feeder - Long time have not parts, Select action = [Finish]"));
                    return FlowChart.FCRESULT.CASE1;    // 此模組已供料完成
            }
            ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM2_BowlFeederNoPartsTimeout);
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_BF2_Finish_Run()
        {
            //if (mLotend == true)
            {// 收到結批命令
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart5_Run()
        {
            // 偷跑回震動盤位置
            int BasePosX = SReadValue("Pos_HDT2_Pick_X").ToInt();
            HdtAxisXMove(BowlFeederID.BF_B, BasePosX);
            T_BF2_Place.Restart();
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion


        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                BF_A_ClientSocket.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void BF_A_ClientSocket_OnConnect(object sender)
        {
            SocketLog(BowlFeederID.BF_A, "CCD連線建立成功");
        }

        private void BF_A_ClientSocket_OnDisconnect(object sender, System.Net.Sockets.Socket socket)
        {
            SocketLog(BowlFeederID.BF_A, "CCD連線中斷");
        }

        private void BF_A_ClientSocket_OnError(object sender, int errorCode, string ErrMsg)
        {
            String msg = String.Format("Socket連線錯誤：Error Code:{0}, ErrMsg{1}", errorCode, ErrMsg);
            SocketLog(BowlFeederID.BF_A, msg);
        }

        private void BF_A_ClientSocket_OnRead(ProVTool.SocketClient sender)
        {
            string msg = sender.ReadText();
            SocketLog(BowlFeederID.BF_A, msg);
            try
            {
                BF_A_InspectionResult.ParseResultString(msg);                

            }
            catch (Exception ex)
            {
                SocketLog(BowlFeederID.BF_A, ex.ToString());
            }
        }

        private void BF_B_ClientSocket_OnConnect(object sender)
        {
            SocketLog(BowlFeederID.BF_B, "CCD連線建立成功");
        }

        private void BF_B_ClientSocket_OnDisconnect(object sender, System.Net.Sockets.Socket socket)
        {
            SocketLog(BowlFeederID.BF_B, "CCD連線中斷");
        }

        private void BF_B_ClientSocket_OnError(object sender, int errorCode, string ErrMsg)
        {
            String msg = String.Format("Socket連線錯誤：Error Code:{0}, ErrMsg{1}", errorCode, ErrMsg);
            SocketLog(BowlFeederID.BF_B, msg);
        }

        private void BF_B_ClientSocket_OnRead(ProVTool.SocketClient sender)
        {
            string msg = sender.ReadText();
            SocketLog(BowlFeederID.BF_B, msg);
            try
            {
                BF_B_InspectionResult.ParseResultString(msg);

            }
            catch (Exception ex)
            {
                SocketLog(BowlFeederID.BF_B, ex.ToString());
            }
            //string msg = sender.ReadText();
            //SocketLog(BowlFeederID.BF_A, msg);
            //try
            //{
            //    char[] Separators = new char[] { ',', ';', '=' };
            //    String[] sCommand = msg.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

            //}
            //catch (Exception ex)
            //{
            //    SocketLog(BowlFeederID.BF_B, ex.ToString());
            //}
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

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                BF_A_ClientSocket.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                BF_B_ClientSocket.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                BF_B_ClientSocket.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private FlowChart.FCRESULT FC_Place_StartPlace2_Ignore_Run()
        {
            Flag_BowlFeederID2Action.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ServoOn();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ServoOff();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ServoOn();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ServoOff();
        }

        private FlowChart.FCRESULT flowChart10_Run()
        {
            ContinueRun = true;
            if (SReadValue("EnableBFM2CCDFunction").ToBoolean() == false)
            {
                return FlowChart.FCRESULT.NEXT;
            }

            if (BF_B_InspectionResult.GetGrabSuccessValue())
            {
                BF_B_ClientSocket.Socket.SendText("GetInspectResultCmd");
                return FlowChart.FCRESULT.NEXT;
            }
            if (T_BF2_Pick.On(3000))
            {
                ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM2_sofewareError);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart11_Run()
        {
            ContinueRun = true;
            if (SReadValue("EnableBFM1CCDFunction").ToBoolean() == false)
            {
                return FlowChart.FCRESULT.NEXT;
            }

            if (BF_A_InspectionResult.GetGrabSuccessValue())
            {
                BF_A_ClientSocket.Socket.SendText("GetInspectResultCmd");
                return FlowChart.FCRESULT.NEXT;
            }
            if (T_BF1_Pick.On(3000))
            {
                ShowAlarmMessage("E", (int)EBLM_ALARM_CODE.ER_BLM1_sofewareError);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        public bool AddNewJob(string msg)
        {
            if (SReadValue("EnableBFM1CCDFunction").ToBoolean() == false)
            {
                return true;
            }
            string Msg = "AddNewJobCmd," + msg + ";";
            BF_A_ClientSocket.Socket.SendText(Msg);
            return true; ;
        }


        public int AddNewJobResult()
        {
            if (IsSimulation() != 0) return 1;
            return BF_A_InspectionResult.GetAddNewJobValue();
        }

        public bool ChangeJob(string job)
        {
            if (SReadValue("EnableBFM1CCDFunction").ToBoolean() == false)
            {
                return true;
            }
            string Msg = "ChangeJobCmd," + job + ";";
            BF_A_ClientSocket.Socket.SendText(Msg);
            return true;
        }

        public int ChangeJobResult()
        {
            if (IsSimulation() != 0) return 1;
            return BF_A_InspectionResult.GetChangeJobValue();
        }

        //#region Repeat Test
        bool Flag_Repeat_A = false;
        bool Flag_Repeat_B = false;
        JActionTask Repeat_TaskA = new JActionTask();
        JActionTask Repeat_TaskB = new JActionTask();
        private void button18_Click(object sender, EventArgs e)
        {
            InspectList = new List<InspectResult>();
            Repeat_TaskA.Reset();
            Flag_Repeat_A = true;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            InspectList2 = new List<InspectResult>();
            Repeat_TaskB.Reset();
            Flag_Repeat_B = true;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Flag_Repeat_B = false;
            WriteToXLS("BFB", InspectList2);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Flag_Repeat_A = false;
            WriteToXLS("BFA", InspectList);
        }

        public struct InspectResult
        {
            public string Result;
            public string XValue;
            public string YValue;
            public string AngValue;
            public string x2;
            public string y2;
        }
        
        private bool WriteToXLS(string title, List<InspectResult> List)
        {
            string fname = string.Format(@"D:\Repeat\{0}.xls", title);

            if (File.Exists(fname))
                File.Delete(fname);

            try
            {
                using (FileStream fs = new FileStream(fname, FileMode.CreateNew, FileAccess.Write))
                {
                    //建立Excel 2003檔案
                    IWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("使用者管理");

                    //寫入表頭
                    int LineCNT = 1;
                    sheet.CreateRow(LineCNT);
                    sheet.GetRow(LineCNT).CreateCell(1).SetCellValue("辨識結果");
                    sheet.GetRow(LineCNT).CreateCell(2).SetCellValue("X補償");
                    sheet.GetRow(LineCNT).CreateCell(3).SetCellValue("Y補償");
                    sheet.GetRow(LineCNT).CreateCell(4).SetCellValue("角度補償");
                    sheet.GetRow(LineCNT).CreateCell(5).SetCellValue("X補償");
                    sheet.GetRow(LineCNT).CreateCell(6).SetCellValue("Y補償");
                    LineCNT++;

                    foreach (InspectResult v in List)
                    {
                        sheet.CreateRow(LineCNT);

                        sheet.GetRow(LineCNT).CreateCell(1).SetCellValue(v.Result.ToString());
                        sheet.GetRow(LineCNT).CreateCell(2).SetCellValue(v.XValue.ToString());
                        sheet.GetRow(LineCNT).CreateCell(3).SetCellValue(v.YValue.ToString());
                        sheet.GetRow(LineCNT).CreateCell(4).SetCellValue(v.AngValue.ToString());
                        sheet.GetRow(LineCNT).CreateCell(5).SetCellValue(v.x2.ToString());
                        sheet.GetRow(LineCNT).CreateCell(6).SetCellValue(v.y2.ToString());
                        LineCNT++;
                    }

                    for (int i = 1; i <= 3; i++)
                        sheet.AutoSizeColumn(i);

                    workbook.Write(fs);
                    fs.Close();
                    workbook = null;
                }
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }

        private List<InspectResult> InspectList = null;
        private List<InspectResult> InspectList2 = null;
        private void TM_RepeatA_Tick(object sender, EventArgs e)
        {
            TM_RepeatA.Enabled = false;
            if (Flag_Repeat_A)
            {
                switch (Repeat_TaskA.Value)
                {
                    case 0:
                        {   //關閉BF真空
                            CtlBFMBowelFrontSuction(BowlFeederID.BF_A, EOnOffState.OFF);
                            CtlBFMBowelDownSuction(BowlFeederID.BF_A, EOnOffState.OFF);
                            CtlBFMBowelBackSuction(BowlFeederID.BF_A, EOnOffState.ON);
                            Repeat_TaskA.Next();
                        }
                        break;
                    case 1:
                        { //X軸移到取位置
                            int BasePosX = SReadValue("Pos_HDT1_Pick_X").ToInt();
                            int OffsetPosX = PReadValue("Offset_HDT_BF_A_X").ToInt();
                            int PosX = BasePosX + OffsetPosX;
                            bool b1 = HdtAxisXMove(BowlFeederID.BF_A, PosX);
                            bool b2 = HdtAxisUMove(BowlFeederID.BF_A, 0);
                            if (b1 == true && b2 == true)
                            {
                                Repeat_TaskA.Next();
                            }
                        }
                        break;
                    case 2:
                        {
                            int BasePickPosZ = SReadValue("Pos_HDT1_Pick_Z").ToInt();
                            int OffsetPickPosZ = PReadValue("Offset_HDT_BF_A_Z_PICKUP").ToInt();
                            int SlowSpeedDistance = PReadValue("DT_HDT_BF_SlowSpeed_Dis_Pick_BowlFeeder").ToInt();
                            int PickPosZ = BasePickPosZ + OffsetPickPosZ + SlowSpeedDistance;

                            bool b1 = MT_BFM1_AxisZ.G00(PickPosZ);
                            if (b1 == true)
                            {
                                SetHdtPnPLowSpeed(BowlFeederID.BF_A, ACTIONMODE.PICKUP);
                                Repeat_TaskA.Next();
                            }
                        }
                        break;
                    case 3:
                        {
                            int BasePickPosZ = SReadValue("Pos_HDT1_Pick_Z").ToInt();
                            int OffsetPickPosZ = PReadValue("Offset_HDT_BF_A_Z_PICKUP").ToInt();
                            int PickPosZ = BasePickPosZ + OffsetPickPosZ;

                            bool b1 = MT_BFM1_AxisZ.G00(PickPosZ);
                            if (b1 == true)
                            {
                                JLogger.LogDebug("BF_A", string.Format("Pick - Hdt1 Axis Z to Pick Pos={0}", PickPosZ));
                                Repeat_TaskA.Next();
                            }
                        }
                        break;
                    case 4:
                        {
                            bool b1 = Nozzle_BFM1.VacuumOn(PReadValue("DT_HDT_BF_VAC").ToInt());
                            if (b1 == true)
                            {
                                SetWorkSpeed(BowlFeederID.BF_A);
                                Repeat_TaskA.Next();
                            }
                        }
                        break;
                    case 5:
                        {
                            
                                int Pos = SReadValue("Pos_HDT1_Safe_Z").ToInt();
                                Pos += PReadValue("Device_Thickness").ToInt();
                                bool b1 = MT_BFM1_AxisZ.G00(Pos);
                                if (b1 == true)
                                {
                                    Repeat_TaskA.Next();
                                }
                        }
                        break;
                    case 6:
                        {
                            //bool b2 = HdtAxisUMove(BowlFeederID.BF_A, 270);
                            //if (b2)
                            {
                                if (SReadValue("EnableBFM1CCDFunction").ToBoolean() == true)
                                {
                                    SetTriggerSpeed(BowlFeederID.BF_A);
                                    int[] iTrigger = { SReadValue("Pos_HDT1_Grab_X").ToInt() };
                                    MT_BFM1_AxisX.SetTriggerByArray(iTrigger, iTrigger.Length);
                                    BF_A_InspectionResult.Reset();
                                    BF_A_ClientSocket.Socket.SendText("StartToInspectOnflyCmd");
                                }
                                Repeat_TaskA.Next();
                            }
                        }
                        break;
                    case 7:
                        {
                            int PosX = SReadValue("Pos_HDT1_WaitPlace_X").ToInt();
                            bool b1 = HdtAxisXMove(BowlFeederID.BF_A, PosX);
                            if (b1 == true)
                            {
                                MT_BFM1_AxisX.SetTriggerOff();
                                SetWorkSpeed(BowlFeederID.BF_A);
                                Repeat_TaskA.Next();    
                            }
                        }
                        break;
                    case 8:
                        {
                            if (SReadValue("EnableBFM1CCDFunction").ToBoolean() == false)
                            {
                                Repeat_TaskA.Next();    
                            }

                            if (BF_A_InspectionResult.GetGrabSuccessValue())
                            {
                                BF_A_ClientSocket.Socket.SendText("GetInspectResultCmd");
                                Repeat_TaskA.Next();    
                            }
                        }
                        break;
                    case 9:
                        {
                            InspectionState state = BF_A_InspectionResult.GetIsInspectionResultFinish();
                            switch (state)
                            {

                                case InspectionState.Success:
                                    {
                                        bool b2 = HdtAxisUMove(BowlFeederID.BF_A, 180);
                                        if (b2)
                                        {
                                            InspectResult value = new InspectResult();
                                            value.Result = "1";
                                            value.XValue = BF_A_InspectionResult.GetOffsetX().ToString();
                                            value.YValue = BF_A_InspectionResult.GetOffsetY().ToString();
                                            value.AngValue = BF_A_InspectionResult.GetAngle().ToString();
                                            value.x2 = BF_A_InspectionResult.GetX2().ToString();
                                            value.y2 = BF_A_InspectionResult.GetY2().ToString();

                                            InspectList.Add(value);
                                            //InspectList.Clear();
                                            Repeat_TaskA.Next();
                                        }
                                    }
                                    break;
                                case InspectionState.Fail:
                                    {
                                        bool b2 = HdtAxisUMove(BowlFeederID.BF_A, 0);
                                        if (b2)
                                        {
                                            InspectResult value = new InspectResult();
                                            value.Result = "0";
                                            value.XValue = BF_A_InspectionResult.GetOffsetX().ToString();
                                            value.YValue = BF_A_InspectionResult.GetOffsetY().ToString();
                                            value.AngValue = BF_A_InspectionResult.GetAngle().ToString();
                                            value.x2 = BF_A_InspectionResult.GetX2().ToString();
                                            value.y2 = BF_A_InspectionResult.GetY2().ToString();
                                            InspectList.Add(value);
                                            
                                            Repeat_TaskA.Next();
                                        }
                                    }
                                    break;
                                case InspectionState.Error:
                                    {
                                        InspectResult value = new InspectResult();
                                        value.Result = "-1";
                                        value.XValue = BF_A_InspectionResult.GetOffsetX().ToString();
                                        value.YValue = BF_A_InspectionResult.GetOffsetY().ToString();
                                        value.AngValue = BF_A_InspectionResult.GetAngle().ToString();
                                        value.x2 = BF_A_InspectionResult.GetX2().ToString();
                                        value.y2 = BF_A_InspectionResult.GetY2().ToString();
                                        InspectList.Add(value);
                                        Repeat_TaskA.Next();
                                    }
                                    break;
                            }
                        }
                        break;
                    case 10:
                        {
                            int BasePosX = SReadValue("Pos_HDT1_Pick_X").ToInt();
                            int OffsetPosX = PReadValue("Offset_HDT_BF_A_X").ToInt();
                            int PosX = BasePosX + OffsetPosX;
                            bool b1 = HdtAxisXMove(BowlFeederID.BF_A, PosX);
                            bool b2 = true;
                            //bool b2 = HdtAxisUMove(BowlFeederID.BF_A, 0);
                            if (b1 == true && b2 == true)
                            {
                                Repeat_TaskA.Jump(6);
                            }
                        }
                        break;
                }
            }
            TM_RepeatA.Enabled = true;
        }

        private void TM_RepeatB_Tick(object sender, EventArgs e)
        {
            TM_RepeatB.Enabled = false;
            if (Flag_Repeat_B)
            {
                switch (Repeat_TaskB.Value)
                {
                    case 0:
                        {   //關閉BF真空
                            CtlBFMBowelFrontSuction(BowlFeederID.BF_B, EOnOffState.OFF);
                            CtlBFMBowelDownSuction(BowlFeederID.BF_B, EOnOffState.OFF);
                            CtlBFMBowelBackSuction(BowlFeederID.BF_B, EOnOffState.ON);
                            Repeat_TaskB.Next();
                        }
                        break;
                    case 1:
                        { //X軸移到取位置
                            int BasePosX = SReadValue("Pos_HDT2_Pick_X").ToInt();
                            int OffsetPosX = PReadValue("Offset_HDT_BF_B_X").ToInt();
                            int PosX = BasePosX + OffsetPosX;
                            bool b1 = HdtAxisXMove(BowlFeederID.BF_B, PosX);
                            bool b2 = HdtAxisUMove(BowlFeederID.BF_B, 0);
                            if (b1 == true && b2 == true)
                            {
                                Repeat_TaskB.Next();
                            }
                        }
                        break;
                    case 2:
                        {
                            int BasePickPosZ = SReadValue("Pos_HDT2_Pick_Z").ToInt();
                            int OffsetPickPosZ = PReadValue("Offset_HDT_BF_B_Z_PICKUP").ToInt();
                            int SlowSpeedDistance = PReadValue("DT_HDT_BF_SlowSpeed_Dis_Pick_BowlFeeder").ToInt();
                            int PickPosZ = BasePickPosZ + OffsetPickPosZ + SlowSpeedDistance;

                            bool b1 = MT_BFM2_AxisZ.G00(PickPosZ);
                            if (b1 == true)
                            {
                                SetHdtPnPLowSpeed(BowlFeederID.BF_B, ACTIONMODE.PICKUP);
                                Repeat_TaskB.Next();
                            }
                        }
                        break;
                    case 3:
                        {
                            int BasePickPosZ = SReadValue("Pos_HDT2_Pick_Z").ToInt();
                            int OffsetPickPosZ = PReadValue("Offset_HDT_BF_B_Z_PICKUP").ToInt();
                            int PickPosZ = BasePickPosZ + OffsetPickPosZ;

                            bool b1 = MT_BFM2_AxisZ.G00(PickPosZ);
                            if (b1 == true)
                            {
                                Repeat_TaskB.Next();
                            }
                        }
                        break;
                    case 4:
                        {
                            bool b1 = Nozzle_BFM2.VacuumOn(PReadValue("DT_HDT_BF_VAC").ToInt());
                            if (b1 == true)
                            {
                                SetWorkSpeed(BowlFeederID.BF_B);
                                Repeat_TaskB.Next();
                            }
                        }
                        break;
                    case 5:
                        {

                            int Pos = SReadValue("Pos_HDT2_Safe_Z").ToInt();
                            Pos += PReadValue("Device_Thickness").ToInt();
                            bool b1 = MT_BFM2_AxisZ.G00(Pos);
                            if (b1 == true)
                            {
                                Repeat_TaskB.Next();
                            }
                        }
                        break;
                    case 6:
                        {
                            //bool b2 = HdtAxisUMove(BowlFeederID.BF_B, 0);
                            //if (b2)
                            {
                                if (SReadValue("EnableBFM2CCDFunction").ToBoolean() == true)
                                {
                                    SetTriggerSpeed(BowlFeederID.BF_B);
                                    int[] iTrigger = { SReadValue("Pos_HDT2_Grab_X").ToInt() };
                                    MT_BFM2_AxisX.SetTriggerByArray(iTrigger, iTrigger.Length);
                                    BF_B_InspectionResult.Reset();
                                    BF_B_ClientSocket.Socket.SendText("StartToInspectOnflyCmd");
                                }
                                Repeat_TaskB.Next();
                            }
                        }
                        break;
                    case 7:
                        {
                            int PosX = SReadValue("Pos_HDT2_WaitPlace_X").ToInt();
                            bool b1 = HdtAxisXMove(BowlFeederID.BF_B, PosX);
                            if (b1 == true)
                            {
                                MT_BFM2_AxisX.SetTriggerOff();
                                SetWorkSpeed(BowlFeederID.BF_B);
                                Repeat_TaskB.Next();
                            }
                        }
                        break;
                    case 8:
                        {
                            if (SReadValue("EnableBFM2CCDFunction").ToBoolean() == false)
                            {
                                Repeat_TaskB.Next();
                            }

                            if (BF_B_InspectionResult.GetGrabSuccessValue())
                            {
                                BF_B_ClientSocket.Socket.SendText("GetInspectResultCmd");
                                Repeat_TaskB.Next();
                            }
                        }
                        break;
                    case 9:
                        {
                            InspectionState state = BF_B_InspectionResult.GetIsInspectionResultFinish();
                            switch (state)
                            {

                                case InspectionState.Success:
                                    {
                                        InspectResult value = new InspectResult();
                                        value.Result = "1";
                                        value.XValue = BF_B_InspectionResult.GetOffsetX().ToString();
                                        value.YValue = BF_B_InspectionResult.GetOffsetY().ToString();
                                        value.AngValue = BF_B_InspectionResult.GetAngle().ToString();
                                        InspectList2.Add(value);
                                        Repeat_TaskB.Next();
                                    }
                                    break;
                                case InspectionState.Fail:
                                    {
                                        InspectResult value = new InspectResult();
                                        value.Result = "0";
                                        value.XValue = BF_B_InspectionResult.GetOffsetX().ToString();
                                        value.YValue = BF_B_InspectionResult.GetOffsetY().ToString();
                                        value.AngValue = BF_B_InspectionResult.GetAngle().ToString();
                                        InspectList2.Add(value);
                                        Repeat_TaskB.Next();
                                    }
                                    break;
                                case InspectionState.Error:
                                    {
                                        InspectResult value = new InspectResult();
                                        value.Result = "-1";
                                        value.XValue = BF_B_InspectionResult.GetOffsetX().ToString();
                                        value.YValue = BF_B_InspectionResult.GetOffsetY().ToString();
                                        value.AngValue = BF_B_InspectionResult.GetAngle().ToString();
                                        InspectList2.Add(value);
                                        Repeat_TaskB.Next();
                                    }
                                    break;
                            }
                        }
                        break;
                    case 10:
                        {
                            int BasePosX = SReadValue("Pos_HDT2_Pick_X").ToInt();
                            int OffsetPosX = PReadValue("Offset_HDT_BF_B_X").ToInt();
                            int PosX = BasePosX + OffsetPosX;
                            bool b1 = HdtAxisXMove(BowlFeederID.BF_B, PosX);
                            bool b2 = HdtAxisUMove(BowlFeederID.BF_B, 0);
                            if (b1 == true && b2 == true)
                            {
                                Repeat_TaskB.Jump(6);
                            }
                        }
                        break;
                }
            }
            TM_RepeatB.Enabled = true;
        }


    }
}

