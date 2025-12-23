/////////////////////////////////////////////////////////////////////////////////
///TBPP14202 有預留BowlFeed，但因時程問題，軟體目前未實作 (後須如果有改機軟體需檢查)
///Note:
///1.因為舌板只有單Arm所以一次只能一板
///2.TODO:TrayHead Unload 放料後要做OEEUpdate
/////////////////////////////////////////////////////////////////////////////////
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
using System.IO;
using CommonObj;
using PaeLibProVSDKEx;
using PaeLibComponent;
using PaeLibGeneral;
using System.Threading;
using System.Reflection;
using System.Drawing.Printing;
using System.Diagnostics;

namespace TBPP14200
{
    //使用者修改 (主控流程)
    public partial class MainFlowF : Form
    {
        #region 使用者修改 (定義各模組指標，方便使用)

        private BaseModuleInterface mMAA;
        private BaseModuleInterface mTRM;
        private BaseModuleInterface mCHM;
        private BaseModuleInterface mBSM;
        private BaseModuleInterface mHDT;
        private BaseModuleInterface mKSM;
        private BaseModuleInterface mBFM;
        private BaseModuleInterface mCSM;
        //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
        private BaseModuleInterface mRTR;
        private BaseModuleInterface mLTR;
        private BaseModuleInterface mHDT_TR;

       
        #endregion

        public List<FlowChart> FlowChartList = new List<FlowChart>();
        public bool mHomeOk;

        #region Const

        public const string ModuleName_MAA = "MAA";
        public const string ModuleName_TRM = "TRM";
        public const string ModuleName_CHM = "CHM";
        public const string ModuleName_BSM = "BSM";
        public const string ModuleName_HDT = "HDT";
        public const string ModuleName_KSM = "KSM";
        public const string ModuleName_BFM = "BFM";
        public const string ModuleName_CSM = "CSM";
        //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
        public const string ModuleName_LTR = "LTR";
        public const string ModuleName_RTR = "RTR";
        public const string ModuleName_HDT_TR = "HDT_TR";

        #endregion

        #region enum
        public enum AlarmCode
        {
            Err_CassetteHasBoard = 801,     
            Err_AddNewJobFail = 902,
            Err_SuppleDevie = 1010,         //W1010
            Err_ClamShellHasBoard = 1011,   //E1011,    開關蓋站有板,    Clamshell has board
            Err_ClamShellHasNoBoard = 1012, //E1012,    開關蓋站無板,    Clamshell has no board
            Err_StageA_HasBoard = 1013,     //E1013,    板台車A有板,     BoardStage A has board
            Err_StageA_HasNoBoard = 1014,   //E1014,    板台車A無板,     BoardStage A has no board
            Err_StageB_HasBoard = 1015,     //E1015,    板台車B有板,     BoardStage B has board
            Err_StageB_HasNoBoard= 1016,    //E1016,    板台車B無板,     BoardStage B has no board
        }

        #endregion
        #region 產品基本資料相關結構變數

        private TRAY_INFO BoardInfo = new TRAY_INFO();  //Board info
        private TRAY_INFO KitInfo = new TRAY_INFO();    //Kit Shuttle Info
        //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
        private TRAY_INFO TrayInfo = new TRAY_INFO();   //Tray info

        #endregion

        #region 主流程用相關變數
        bool Flag_Booking_A_Work = false;
        bool Flag_Booking_B_Work = false;
        bool Flag_Booking_A_Error = false;
        bool Flag_Booking_B_Error = false;
        int BoardSeq = 0;
        bool Flag_SuppleDeviceSafe = false;

        bool MainFlowLotEndControlStart = false;
        //timer
        private MyTimer TM_Delay_MAA = null;
        private MyTimer TM_Delay_ModuleLotEnd = null;
        private MyTimer TM_Delay_TRM = null;
        private MyTimer TM_Delay_CHM = null;
        private MyTimer TM_Delay_BSM = null;
        private MyTimer TM_Delay_HDT = null;
        private MyTimer TM_Delay_KSM = null;
        private MyTimer TM_Delay_BFM = null;
        private MyTimer TM_Delay_CSM = null;
        //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
        private MyTimer TM_Delay_HDT_TR = null;
        private MyTimer TM_Delay_RTR = null;
        private MyTimer TM_Delay_LTR = null;

        private MyTimer TM_Homing = null;

        private GetTickCountEx m_TRM1tick = null;
        private GetTickCountEx m_TRM2tick = null;
        private GetTickCountEx m_Headtick = null;
        //報表用
        private Container componentsprinter;
        private Button printButton;
        private Font printFont;
        private StreamReader streamToPrint;
        //目前使用的模組
        //private KitShuttleID HDT_BF_KitShuttleID = KitShuttleID.NONE;
        private KitShuttleID HDT_BIB_A_KitShuttleID = KitShuttleID.NONE;
        private KitShuttleID HDT_BIB_B_KitShuttleID = KitShuttleID.NONE;
        private BIBStageID HDT_BIB_BIBStageID = BIBStageID.NONE;
        private KitShuttleID HDT_BIB_Booking_KitShuttleID = KitShuttleID.NONE;
        //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
        private TrayID _WorkTray = TrayID.NONE;
       

        //private TRMStation WorkStation = TRMStation.NONE;
        private CassetteID WorkID = CassetteID.NONE;

        //Board Head 取放資訊
        //private PnPCalculator HDT_BIB_A_PnP = null;
        //private PnPCalculator HDT_BIB_B_PnP = null;
        private HEAD_INFO HDT_BIB_A_INFO;
        private HEAD_INFO HDT_BIB_B_INFO;
        //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
        private HEAD_INFO HDT_TR_INFO;

        private PnPInfo HDT_BIB_A_PnPInfo = null;
        private PnPInfo HDT_BIB_B_PnPInfo = null;
        //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
        private PnPInfo HDT_TR_PnPInfo = null;

        BookingInfo HDT_BIB_A_BookingInfo = null;
        BookingInfo HDT_BIB_B_BookingInfo = null;

        //Lock Kit MainFlow 使用
        private KitShuttleOwner LeftKitUser;
        private KitShuttleOwner RightKitUser;

        //Reject Param
        bool NeedRejectLeftBoard = false;
        bool NeedRejectRightBoard = false;

        //Tray Data
        private TrayDataEx TDEx_Unload = null;
        private TrayDataEx TDEx_Load = null;

        private TrayDataEx TDEx_TRM = null;

        private TrayDataEx TDEx_CHM = null;

        private TrayDataEx TDEx_Left_Board = null;
        private TrayDataEx TDEx_Right_Board = null;

        private TrayDataEx TDEx_HDT_BIB_A = null;
        private TrayDataEx TDEx_HDT_BIB_B = null;

        private TrayDataEx TDEx_Left_KitShuttle = null;
        private TrayDataEx TDEx_Right_KitShuttle = null;

        private TrayDataEx TDEx_Top_BFM = null;
        private TrayDataEx TDEx_Bottom_BFM = null;

        private TrayDataEx TDEx_PassBox = null;
        private TrayDataEx TDEx_FailBox = null;

        //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
        private TrayDataEx TDEx_LeftTray = null;
        private TrayDataEx TDEx_RightTray = null;
        private TrayDataEx TDEx_HDT_TR = null;

        //Board 預約系統
        private BoardBookingSystem Board_BKS = null;

        private BoardHeadPnPNavigator HDT_BIB_A_PnP = null;
        private BoardHeadPnPNavigator HDT_BIB_B_PnP = null;

        private BoardHeadPnPNavigator HDT_BF_A_PnP = null;
        private BoardHeadPnPNavigator HDT_BF_B_PnP = null;

        //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
        private PnPCalculator HDT_TR_PnP = null;

        //HDT BIB Flag
        private JActionFlag Flag_HDT_BIB_A_PnP;
        private JActionFlag Flag_HDT_BIB_B_PnP;
        private JActionFlag Flag_HDT_BIB_A_Action;
        private JActionFlag Flag_HDT_BIB_B_Action;
        //BIB Head CCD In advance Flag
        private JActionFlag Flag_HDT_BIB_A_Vision_InAdvance;
        private JActionFlag Flag_HDT_BIB_B_Vision_InAdvance;
        //BIB Vision Flag
        private JActionFlag Flag_HDT_BIB_A_Vision;
        private JActionFlag Flag_HDT_BIB_B_Vision;

        //CHM action
        private JActionFlag Flag_CHM_Action;
        private ACTIONMODE CHM_ACTION = ACTIONMODE.NONE;

        //Booking Flag
        //private JActionFlag Flag_Booking;
        private JActionFlag Flag_Booking_A;
        private JActionFlag Flag_Booking_B;

        //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
        private JActionFlag Flag_HDT_TR_PnP;
        private JActionFlag Flag_Load_ScanAction;
        private JActionFlag Flag_Load_WorkAction;
        private JActionFlag Flag_SuppleDeviceAction;

        //Kit Shuttle Control
        private KitShuttleStateControl LeftKitStateControl;
        private KitShuttleStateControl RightKitStateControl;

        //BF 目前使用的台車
        private KitShuttleID HDT_BF_A_KitShuttleID = KitShuttleID.NONE;
        private KitShuttleID HDT_BF_B_KitShuttleID = KitShuttleID.NONE;

        //TRM目前對哪一台車工作
        BIBStageID TRM_WorkStage = BIBStageID.NONE;

        private TrayDataEx TDEx_HDT_BF_A = null;
        private TrayDataEx TDEx_HDT_BF_B = null;
        //private PnPCalculator HDT_BF_A_PnP = null;
        //private PnPCalculator HDT_BF_B_PnP = null;
        private HEAD_INFO HDT_BF_A_INFO;
        private HEAD_INFO HDT_BF_B_INFO;
        private PnPInfo HDT_BF_A_PnPInfo;
        private PnPInfo HDT_BF_B_PnPInfo;

        private BDLinkSMCtrl BDSMC = new BDLinkSMCtrl();
        private LotInformation_SM lotinfo_sm = new LotInformation_SM();

        //Mainflow lot end flag
        private JActionFlag Flag_TRM_LotEnd;
        private JActionFlag Flag_CHM_LotEnd;
        private JActionFlag Flag_LeftBSM_LotEnd;
        private JActionFlag Flag_RightBSM_LotEnd;
        private JActionFlag Flag_TopHDT_LotEnd;
        private JActionFlag Flag_BottomHDT_LotEnd;
        private JActionFlag Flag_LeftKitShuttle_LotEnd;
        private JActionFlag Flag_RightKitShuttle_LotEnd;
        private JActionFlag Flag_TopBowlFeeder_LotEnd;
        private JActionFlag Flag_BottomBowlFeeder_LotEnd;
        //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
        private JActionFlag Flag_VTray_LotEnd;
        private JActionFlag Flag_HDT_TR_LotEnd;

        //MainFlow Lot End Task
        private JActionTask Task_MainFLotEnd;
        //JActionTask ExportLotEndTask = new JActionTask();


        private bool bLoadToBox_A = false;
        private bool bLoadToBox_B = false;

        public bool HDTA_NozzleStateIsFail;
        public bool HDTB_NozzleStateIsFail;

        //Board Info
        //CassetteInfo LeftCassette;
        //CassetteInfo RightCassette;
        List<BoardInfo> AllSlots = new List<BoardInfo>();
        BoardInfo WorkingBoard;
        BoardInfo StageA_Board;
        BoardInfo StageB_Board;
        #endregion

        #region 其它表單參數
        private Thread DialogFormThread = null; //其它表單處理專用執行緒
        private bool StopDialogFormThread = true;   //其它表單處理專用執行緒運行旗標
        private bool ShowDialogForm = false;
        private DialogFormType MainFlow_DialogFormType = DialogFormType.None;
        private string MainFlow_DialogFormMessage = string.Empty;
        private DialogFormReturn MainFLow_DialogFormReturn = DialogFormReturn.RETURN_NONE;
        private string MainFlow_DialogFormInput = string.Empty;
        #endregion

        public MainFlowF()
        {
            InitializeComponent();

            TopLevel = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            //找出所有的主控FlowChart
            GetControls(this);

            #region 使用者修改 (定義各模組指標，方便使用)

            mMAA = (BaseModuleInterface)FormSet.mMSS.GetModule(ModuleName_MAA);
            mTRM = (BaseModuleInterface)FormSet.mMSS.GetModule(ModuleName_TRM);
            mCHM = (BaseModuleInterface)FormSet.mMSS.GetModule(ModuleName_CHM);
            mBSM  = (BaseModuleInterface)FormSet.mMSS.GetModule(ModuleName_BSM);
            mHDT = (BaseModuleInterface)FormSet.mMSS.GetModule(ModuleName_HDT);
            mKSM = (BaseModuleInterface)FormSet.mMSS.GetModule(ModuleName_KSM);
            mBFM = (BaseModuleInterface)FormSet.mMSS.GetModule(ModuleName_BFM);
            mCSM = (BaseModuleInterface)FormSet.mMSS.GetModule(ModuleName_CSM);
            //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
            mRTR = (BaseModuleInterface)FormSet.mMSS.GetModule(ModuleName_RTR);
            mLTR = (BaseModuleInterface)FormSet.mMSS.GetModule(ModuleName_LTR);
            mHDT_TR = (BaseModuleInterface)FormSet.mMSS.GetModule(ModuleName_HDT_TR);

            DialogFormThread = new Thread(ShowDialogFormProcess);
            StopDialogFormThread = false;
            DialogFormThread.Start();

            //Timer
            TM_Delay_BFM = new MyTimer();
            TM_Delay_BFM.AutoReset = true;
            TM_Delay_BSM = new MyTimer();
            TM_Delay_BSM.AutoReset = true;
            TM_Delay_CHM = new MyTimer();
            TM_Delay_CHM.AutoReset = true;
            TM_Delay_HDT = new MyTimer();
            TM_Delay_HDT.AutoReset = true;
            TM_Delay_KSM = new MyTimer();
            TM_Delay_KSM.AutoReset = true;
            TM_Delay_MAA = new MyTimer();
            TM_Delay_MAA.AutoReset = true;
            TM_Delay_TRM = new MyTimer();
            TM_Delay_TRM.AutoReset = true;
            TM_Homing = new MyTimer();
            TM_Homing.AutoReset = true;
            //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
            TM_Delay_HDT_TR = new MyTimer();
            TM_Delay_HDT_TR.AutoReset = true;
            TM_Delay_RTR = new MyTimer();
            TM_Delay_RTR.AutoReset = true;
            TM_Delay_LTR = new MyTimer();
            TM_Delay_LTR.AutoReset = true;

            TM_Delay_ModuleLotEnd = new MyTimer();
            TM_Delay_ModuleLotEnd.AutoReset = true;

            //HDT
            //HDT_BF_A_PnP = new PnPCalculator();
            //HDT_BF_B_PnP = new PnPCalculator();
            //HDT_BIB_A_PnP = new PnPCalculator();
            //HDT_BIB_B_PnP = new PnPCalculator();

            //Board booking system
            Board_BKS = new BoardBookingSystem();
            HDT_BIB_A_PnP = new BoardHeadPnPNavigator();
            HDT_BIB_B_PnP = new BoardHeadPnPNavigator();
            HDT_BF_A_PnP = new BoardHeadPnPNavigator();
            HDT_BF_B_PnP = new BoardHeadPnPNavigator();
            //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
            HDT_TR_PnP = new PnPCalculator();

            HDT_BIB_A_PnPInfo = new PnPInfo();
            HDT_BIB_B_PnPInfo = new PnPInfo();
            //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
            HDT_TR_PnPInfo = new PnPInfo();

            //Kit Shuttle Control
            LeftKitStateControl = new KitShuttleStateControl();
            RightKitStateControl = new KitShuttleStateControl();
            Flag_HDT_BIB_A_PnP = new JActionFlag();
            Flag_HDT_BIB_A_Vision = new JActionFlag();
            Flag_HDT_BIB_A_Vision_InAdvance = new JActionFlag();
            Flag_HDT_BIB_A_Action = new JActionFlag();
            Flag_HDT_BIB_B_PnP = new JActionFlag();
            Flag_HDT_BIB_B_Vision = new JActionFlag();
            Flag_HDT_BIB_B_Vision_InAdvance = new JActionFlag();
            Flag_HDT_BIB_B_Action = new JActionFlag();
            Flag_CHM_Action = new JActionFlag();
            //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
            Flag_HDT_TR_PnP = new JActionFlag();
            Flag_Load_ScanAction = new JActionFlag();
            Flag_Load_WorkAction = new JActionFlag();
            Flag_SuppleDeviceAction = new JActionFlag();
            //Flag_Booking = new JActionFlag();
            Flag_Booking_A = new JActionFlag();
            Flag_Booking_B = new JActionFlag();
            Flag_LeftBSM_LotEnd = new JActionFlag();
            Flag_RightBSM_LotEnd = new JActionFlag();
            Flag_LeftKitShuttle_LotEnd = new JActionFlag();
            Flag_RightKitShuttle_LotEnd = new JActionFlag();
            Flag_TopBowlFeeder_LotEnd = new JActionFlag();
            Flag_BottomBowlFeeder_LotEnd = new JActionFlag();
            Flag_TopHDT_LotEnd = new JActionFlag();
            Flag_BottomHDT_LotEnd = new JActionFlag();
            Flag_TRM_LotEnd = new JActionFlag();
            Flag_CHM_LotEnd = new JActionFlag();
            //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
            Flag_VTray_LotEnd = new JActionFlag();
            Flag_HDT_TR_LotEnd = new JActionFlag();

            Task_MainFLotEnd = new JActionTask();

            //LeftCassette = new CassetteInfo(CassetteID.LEFT);
            //RightCassette = new CassetteInfo(CassetteID.RIGHT);
            WorkingBoard = new CommonObj.BoardInfo();
            StageA_Board = new CommonObj.BoardInfo();
            StageB_Board = new CommonObj.BoardInfo();
            
            //baord info
            SYSPara.CurrentBoard = new BoardBinInfo();
            //IndexTime 計算
            GlobalDefine.IndexBF_MoveIndexTime = new IndexTime();
            GlobalDefine.IndexBF_PickIndexTime = new IndexTime();
            GlobalDefine.IndexBF_PlaceIndexTime = new IndexTime();
            GlobalDefine.IndexBIB_HDT_MoveIndexTime = new IndexTime();
            GlobalDefine.IndexBIB_HDT_PickIndexTime = new IndexTime();
            GlobalDefine.IndexBIB_HDT_PlaceIndexTime = new IndexTime();

            //委派
            GlobalDefine.m_ShowAlarm = new GlobalDefine.ShowAlarmDelegate(ShowAlarm);
            GlobalDefine.m_LotEndDelegate = new GlobalDefine.LotEndDelegate(ExportLotEndReport);
            GlobalDefine.m_LotStartDelegate = new GlobalDefine.LotStartDelegate(ExportLotStartReport);

            m_TRM1tick = new GetTickCountEx();
            m_TRM2tick = new GetTickCountEx();
            m_Headtick = new GetTickCountEx();
            #endregion
        }

        #region 動作函數

        //初始化函數
        public void Initial()
        {
        }

        //持續掃描
        public void AlwaysRun()
        {
            #region 手動安全保護
            if (SYSPara.RunMode.Equals(RunModeDT.MANUAL))
            {
                bool IsSafety = false;
                #region Top Board Head Protection
                IsSafety = (bool)SYSPara.CallProc(ModuleName_HDT, "IsAxisZSafety", BoardHeadID.HDT_A);
                if (!IsSafety.Equals(true))
                {
                    //板手臂不在安全高度，啟動其它模組安全防護
                    HDTLocation HDTLocation = (HDTLocation)SYSPara.CallProc(ModuleName_HDT, "GetHDTLocation", BoardHeadID.HDT_A);
                    {
                        switch (HDTLocation)
                        {
                            case CommonObj.HDTLocation.LEFTBOARD:
                                {
                                    //TODO:啟用LEFTBOARD 安全防護機制
                                    SYSPara.CallProc(ModuleName_BSM, "Protection_BSM", BIBStageModuleOwner.HDT_BIB_A, BIBStageID.BIBStageA);
                                }
                                break;
                            case CommonObj.HDTLocation.RIGHTBOARD:
                                {
                                    //TODO:啟用RIGHTBOARD 安全防護機制
                                    SYSPara.CallProc(ModuleName_BSM, "Protection_BSM", BIBStageModuleOwner.HDT_BIB_A, BIBStageID.BIBStageB);
                                }
                                break;
                            case CommonObj.HDTLocation.LEFTKIT:
                                {
                                    //TODO:啟用LeftKitShuttle 安全防護機制
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleA);
                                }
                                break;
                            case CommonObj.HDTLocation.RIGHTKIT:
                                {
                                    //TODO:啟用RightKitShuttle 安全防護機制
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleB);
                                }
                                break;
                            case CommonObj.HDTLocation.MIDDLEKIT:
                                {
                                    //TODO:啟用KitShuttle 安全防護機制
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleA);
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleB);
                                }
                                break;
                        }
                    }
                }
                #endregion
                #region Bottom Board Head Protection
                IsSafety = (bool)SYSPara.CallProc(ModuleName_HDT, "IsAxisZSafety", BoardHeadID.HDT_B);
                if (!IsSafety.Equals(true))
                {
                    //板手臂不在安全高度，啟動其它模組安全防護
                    HDTLocation HDTLocation = (HDTLocation)SYSPara.CallProc(ModuleName_HDT, "GetHDTLocation", BoardHeadID.HDT_B);
                    {
                        switch (HDTLocation)
                        {
                            case CommonObj.HDTLocation.LEFTBOARD:
                                {
                                    //TODO:啟用LEFTBOARD 安全防護機制
                                    SYSPara.CallProc(ModuleName_BSM, "Protection_BSM", BIBStageModuleOwner.HDT_BIB_B, BIBStageID.BIBStageA);
                                }
                                break;
                            case CommonObj.HDTLocation.RIGHTBOARD:
                                {
                                    //TODO:啟用RIGHTBOARD 安全防護機制
                                    SYSPara.CallProc(ModuleName_BSM, "Protection_BSM", BIBStageModuleOwner.HDT_BIB_B, BIBStageID.BIBStageB);
                                }
                                break;
                            case CommonObj.HDTLocation.LEFTKIT:
                                {
                                    //TODO:啟用LeftKitShuttle 安全防護機制
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleA);
                                }
                                break;
                            case CommonObj.HDTLocation.RIGHTKIT:
                                {
                                    //TODO:啟用RightKitShuttle 安全防護機制
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleB);
                                }
                                break;
                            case CommonObj.HDTLocation.MIDDLEKIT:
                                {
                                    //TODO:啟用KitShuttle 安全防護機制
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleA);
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleB);
                                }
                                break;
                        }
                    }
                }
                #endregion
                #region Top BowlFeeder Head Protection
                IsSafety = (bool)SYSPara.CallProc(ModuleName_BFM, "IsAxisZSafety", BowlFeederID.BF_A);
                if (!IsSafety.Equals(true))
                {
                    //板手臂不在安全高度，啟動其它模組安全防護
                    //TODO:Module GetHDTLcation
                    //HDTLocation HDTLocation = (HDTLocation)SYSPara.CallProc(ModuleName_BFM, "GetHDTLocation", BowlFeederID.BF_A);
                    //{
                    //    switch (HDTLocation)
                    //    {
                    //        case CommonObj.HDTLocation.LEFTKIT:
                    //            {
                    //                //TODO:啟用LeftKitShuttle 安全防護機制
                    //                SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleA);
                    //            }
                    //            break;
                    //        case CommonObj.HDTLocation.RIGHTKIT:
                    //            {
                    //                //TODO:啟用RightKitShuttle 安全防護機制
                    //                SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleB);
                    //            }
                    //            break;
                    //        case CommonObj.HDTLocation.MIDDLEKIT:
                    //            {
                    //                //TODO:啟用KitShuttle 安全防護機制
                    //                SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleA);
                    //                SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleB);
                    //            }
                    //            break;
                    //    }
                    //}
                }
                #endregion
                #region Bottom BowlFeeder Head Protection
                IsSafety = (bool)SYSPara.CallProc(ModuleName_BFM, "IsAxisZSafety", BowlFeederID.BF_B);
                if (!IsSafety.Equals(true))
                {
                    //板手臂不在安全高度，啟動其它模組安全防護
                    //TODO:Module GetHDTLcation
                    //HDTLocation HDTLocation = (HDTLocation)SYSPara.CallProc(ModuleName_BFM, "GetHDTLocation", BowlFeederID.BF_B);
                    //{
                    //    switch (HDTLocation)
                    //    {
                    //        case CommonObj.HDTLocation.LEFTKIT:
                    //            {
                    //                //TODO:啟用LeftKitShuttle 安全防護機制
                    //                SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleA);
                    //            }
                    //            break;
                    //        case CommonObj.HDTLocation.RIGHTKIT:
                    //            {
                    //                //TODO:啟用RightKitShuttle 安全防護機制
                    //                SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleB);
                    //            }
                    //            break;
                    //        case CommonObj.HDTLocation.MIDDLEKIT:
                    //            {
                    //                //TODO:啟用KitShuttle 安全防護機制
                    //                SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleA);
                    //                SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleB);
                    //            }
                    //            break;
                    //    }
                    //}
                }
                #endregion
                #region Tray Head Protection
                IsSafety = (bool)SYSPara.CallProc(ModuleName_HDT_TR, "IsAxisZSafety");
                if (!IsSafety.Equals(true))
                {
                    //板手臂不在安全高度，啟動其它模組安全防護
                    HDTLocation HDTLocation = (HDTLocation)SYSPara.CallProc(ModuleName_HDT, "GetHDTLocation");
                    {
                        switch (HDTLocation)
                        {
                            case CommonObj.HDTLocation.LEFTKIT:
                                {
                                    //TODO:啟用LeftKitShuttle 安全防護機制
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleA);
                                }
                                break;
                            case CommonObj.HDTLocation.RIGHTKIT:
                                {
                                    //TODO:啟用RightKitShuttle 安全防護機制
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleB);
                                }
                                break;
                            case CommonObj.HDTLocation.MIDDLEKIT:
                                {
                                    //TODO:啟用KitShuttle 安全防護機制
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleA);
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleB);
                                }
                                break;
                            case CommonObj.HDTLocation.LEFTTRAY:
                                {
                                    SYSPara.CallProc(ModuleName_LTR, "Protection_LTR", KitShuttleOwner.HDT_TR);
                                }
                                break;
                            case CommonObj.HDTLocation.RIGHTTRAY:
                                {
                                    SYSPara.CallProc(ModuleName_RTR, "Protection_RTR", KitShuttleOwner.HDT_TR);
                                }
                                break;
                        }
                    }
                }
                #endregion Tray Head Protection
                #region Transerfer 是否在安全可旋轉點位
                IsSafety = (bool)SYSPara.CallProc(ModuleName_TRM, "IsTRMSafety");
                SYSPara.CallProc(ModuleName_CHM, "Protection_CHM", IsSafety);
                #endregion
            }
            else if (SYSPara.RunMode.Equals(RunModeDT.AUTO) || SYSPara.RunMode.Equals(RunModeDT.HOME))
            {
                bool IsSafety = false;
                #region Top Board Head Protection
                IsSafety = (bool)SYSPara.CallProc(ModuleName_HDT, "IsAxisZSafety", BoardHeadID.HDT_A);
                if (!IsSafety.Equals(true))
                {
                    //板手臂不在安全高度，啟動其它模組安全防護
                    HDTLocation HDTLocation = (HDTLocation)SYSPara.CallProc(ModuleName_HDT, "GetHDTLocation", BoardHeadID.HDT_A);
                    {
                        switch (HDTLocation)
                        {
                            case CommonObj.HDTLocation.LEFTBOARD:
                                {
                                    //TODO:啟用LEFTBOARD 安全防護機制
                                    SYSPara.CallProc(ModuleName_BSM, "Protection_BSM", BIBStageModuleOwner.HDT_BIB_A, BIBStageID.BIBStageA);
                                }
                                break;
                            case CommonObj.HDTLocation.RIGHTBOARD:
                                {
                                    //TODO:啟用RIGHTBOARD 安全防護機制
                                    SYSPara.CallProc(ModuleName_BSM, "Protection_BSM", BIBStageModuleOwner.HDT_BIB_A, BIBStageID.BIBStageB);
                                }
                                break;
                            case CommonObj.HDTLocation.LEFTKIT:
                                {
                                    //TODO:啟用LeftKitShuttle 安全防護機制
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleA);
                                }
                                break;
                            case CommonObj.HDTLocation.RIGHTKIT:
                                {
                                    //TODO:啟用RightKitShuttle 安全防護機制
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleB);
                                }
                                break;
                            case CommonObj.HDTLocation.MIDDLEKIT:
                                {
                                    //TODO:啟用KitShuttle 安全防護機制
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleA);
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleB);
                                }
                                break;
                        }
                    }
                }
                #endregion
                #region Bottom Board Head Protection
                IsSafety = (bool)SYSPara.CallProc(ModuleName_HDT, "IsAxisZSafety", BoardHeadID.HDT_B);
                if (!IsSafety.Equals(true))
                {
                    //板手臂不在安全高度，啟動其它模組安全防護
                    HDTLocation HDTLocation = (HDTLocation)SYSPara.CallProc(ModuleName_HDT, "GetHDTLocation", BoardHeadID.HDT_B);
                    {
                        switch (HDTLocation)
                        {
                            case CommonObj.HDTLocation.LEFTBOARD:
                                {
                                    //TODO:啟用LEFTBOARD 安全防護機制
                                    SYSPara.CallProc(ModuleName_BSM, "Protection_BSM", BIBStageModuleOwner.HDT_BIB_B, BIBStageID.BIBStageA);
                                }
                                break;
                            case CommonObj.HDTLocation.RIGHTBOARD:
                                {
                                    //TODO:啟用RIGHTBOARD 安全防護機制
                                    SYSPara.CallProc(ModuleName_BSM, "Protection_BSM", BIBStageModuleOwner.HDT_BIB_B, BIBStageID.BIBStageB);
                                }
                                break;
                            case CommonObj.HDTLocation.LEFTKIT:
                                {
                                    //TODO:啟用LeftKitShuttle 安全防護機制
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleA);
                                }
                                break;
                            case CommonObj.HDTLocation.RIGHTKIT:
                                {
                                    //TODO:啟用RightKitShuttle 安全防護機制
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleB);
                                }
                                break;
                            case CommonObj.HDTLocation.MIDDLEKIT:
                                {
                                    //TODO:啟用KitShuttle 安全防護機制
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleA);
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleB);
                                }
                                break;
                        }
                    }
                }
                #endregion
                #region Top BowlFeeder Head Protection
                IsSafety = (bool)SYSPara.CallProc(ModuleName_BFM, "IsAxisZSafety", BowlFeederID.BF_A);
                if (!IsSafety.Equals(true))
                {
                    //板手臂不在安全高度，啟動其它模組安全防護
                    //TODO:Module GetHDTLcation
                    //HDTLocation HDTLocation = (HDTLocation)SYSPara.CallProc(ModuleName_BFM, "GetHDTLocation", BowlFeederID.BF_A);
                    //{
                    //    switch (HDTLocation)
                    //    {
                    //        case CommonObj.HDTLocation.LEFTKIT:
                    //            {
                    //                //TODO:啟用LeftKitShuttle 安全防護機制
                    //                SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleA);
                    //            }
                    //            break;
                    //        case CommonObj.HDTLocation.RIGHTKIT:
                    //            {
                    //                //TODO:啟用RightKitShuttle 安全防護機制
                    //                SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleB);
                    //            }
                    //            break;
                    //        case CommonObj.HDTLocation.MIDDLEKIT:
                    //            {
                    //                //TODO:啟用KitShuttle 安全防護機制
                    //                SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleA);
                    //                SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleB);
                    //            }
                    //            break;
                    //    }
                    //}
                }
                #endregion
                #region Bottom BowlFeeder Head Protection
                IsSafety = (bool)SYSPara.CallProc(ModuleName_BFM, "IsAxisZSafety", BowlFeederID.BF_B);
                if (!IsSafety.Equals(true))
                {
                    //板手臂不在安全高度，啟動其它模組安全防護
                    //TODO:Module GetHDTLcation
                    //HDTLocation HDTLocation = (HDTLocation)SYSPara.CallProc(ModuleName_BFM, "GetHDTLocation", BowlFeederID.BF_B);
                    //{
                    //    switch (HDTLocation)
                    //    {
                    //        case CommonObj.HDTLocation.LEFTKIT:
                    //            {
                    //                //TODO:啟用LeftKitShuttle 安全防護機制
                    //                SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleA);
                    //            }
                    //            break;
                    //        case CommonObj.HDTLocation.RIGHTKIT:
                    //            {
                    //                //TODO:啟用RightKitShuttle 安全防護機制
                    //                SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleB);
                    //            }
                    //            break;
                    //        case CommonObj.HDTLocation.MIDDLEKIT:
                    //            {
                    //                //TODO:啟用KitShuttle 安全防護機制
                    //                SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleA);
                    //                SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleB);
                    //            }
                    //            break;
                    //    }
                    //}
                }
                #endregion
                #region Tray Head Protection
                IsSafety = (bool)SYSPara.CallProc(ModuleName_HDT_TR, "IsAxisZSafety");
                if (!IsSafety.Equals(true))
                {
                    //板手臂不在安全高度，啟動其它模組安全防護
                    HDTLocation HDTLocation = (HDTLocation)SYSPara.CallProc(ModuleName_HDT, "GetHDTLocation");
                    {
                        switch (HDTLocation)
                        {
                            case CommonObj.HDTLocation.LEFTKIT:
                                {
                                    //TODO:啟用LeftKitShuttle 安全防護機制
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleA);
                                }
                                break;
                            case CommonObj.HDTLocation.RIGHTKIT:
                                {
                                    //TODO:啟用RightKitShuttle 安全防護機制
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleB);
                                }
                                break;
                            case CommonObj.HDTLocation.MIDDLEKIT:
                                {
                                    //TODO:啟用KitShuttle 安全防護機制
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleA);
                                    SYSPara.CallProc(ModuleName_KSM, "Protection_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleB);
                                }
                                break;
                            case CommonObj.HDTLocation.LEFTTRAY:
                                {
                                    SYSPara.CallProc(ModuleName_LTR, "Protection_LTR", KitShuttleOwner.HDT_TR);
                                }
                                break;
                            case CommonObj.HDTLocation.RIGHTTRAY:
                                {
                                    SYSPara.CallProc(ModuleName_RTR, "Protection_RTR", KitShuttleOwner.HDT_TR);
                                }
                                break;
                        }
                    }
                }
                #endregion Tray Head Protection
                #region Transerfer 是否在安全可旋轉點位
                IsSafety = (bool)SYSPara.CallProc(ModuleName_TRM, "IsTRMSafety");
                SYSPara.CallProc(ModuleName_CHM, "Protection_CHM", IsSafety);
                #endregion

                Flag_SuppleDeviceSafe = (bool)SYSPara.CallProc(ModuleName_MAA, "SuppleDeviceSensor");
                if (Flag_SuppleDeviceAction.IsDoIt() && Flag_SuppleDeviceSafe)
                {
                    Flag_SuppleDeviceAction.Doing();
                }
                if (Flag_SuppleDeviceAction.IsDoing() && Flag_SuppleDeviceSafe == false)
                {
                    Flag_SuppleDeviceAction.Done();
                }

                SYSPara.CallProc(ModuleName_MAA, "SetLoadPortWork", Flag_Load_WorkAction.IsDoing());

                #region LOAD/UNLOAD DataView
                //v2.0.0.1 改Rack取消此段落
                //if (SYSPara.RunMode.Equals(RunModeDT.AUTO))
                //{
                //    ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_CSM, "CheckHaveBoard", CassetteID.LEFT);
                //    ThreeValued T2 = (ThreeValued)SYSPara.CallProc(ModuleName_CSM, "CheckHaveBoard", CassetteID.RIGHT);
                //    byte by1 = (byte)BinDefine.Empty;
                //    byte by2 = (byte)BinDefine.Empty;
                //    if (T1.Equals(ThreeValued.TRUE)) by1 = (byte)BinDefine.Bin1;
                //    if (T2.Equals(ThreeValued.TRUE)) by2 = (byte)BinDefine.Bin1;
                //    for (uint i = 0; i < td_Unload.XN; i++)
                //    {
                //        for (uint j = 0; j < td_Unload.YN; j++)
                //        {
                //            byte bin = 0;
                //            byte state = 0;
                //            td_Unload.CellGet(0, 0, (int)i, (int)j, ref bin, ref state);
                //            td_Unload.CellSet(0, 0, (int)i, (int)j, by1, state);
                //        }
                //    }
                    
                //    for (uint i = 0; i < td_Load.XN; i++)
                //    {
                //        for (uint j = 0; j < td_Load.YN; j++)
                //        {
                //            byte bin = 0;
                //            byte state = 0;
                //            td_Load.CellGet(0, 0, (int)i, (int)j, ref bin, ref state);
                //            td_Load.CellSet(0, 0, (int)i, (int)j, by2, state);
                //        }
                //    }
                //}
                #endregion
            }
            #endregion
        }

        public void DisposeTH()
        {
            StopDialogFormThread = true;
            DialogFormThread.Join();
            BDSMC.Dispose();
        }

        //掃描硬體按鈕IO
        public void ScanIO()
        {
            #region 架構使用 (處理MAA的硬體按鈕IO對應的動作)

            if ((SYSPara.ManualControlIO)&&(SYSPara.RunMode != RunModeDT.MANUAL))
                return;

            int result = ((BaseModuleInterface)mMAA).ScanIO();

            //Start Button
            if ((result & 0x01) == 1)
            {
                if (!SYSPara.SysRun)
                    SYSPara.LogSay(EnLoggerType.EnLog_OP, "User Click「Start」");

                SYSPara.ErrorStop = false;
                SYSPara.Alarm.ClearAll();

                //SYSPara.CallProc(ModuleName_TRM, "DataReset");
                //SYSPara.CallProc(ModuleName_CHM, "DataReset");
                //SYSPara.CallProc(ModuleName_BSM, "DataReset");
                //SYSPara.CallProc(ModuleName_HDT, "DataReset");
                //SYSPara.CallProc(ModuleName_KSM, "DataReset");
                //SYSPara.CallProc(ModuleName_BFM, "DataReset");
                //SYSPara.CallProc(ModuleName_MAA, "DataReset");
                //SYSPara.CallProc(ModuleName_CSM, "DataReset");
                //SYSPara.CallProc(ModuleName_RTR, "DataReset");
                //SYSPara.CallProc(ModuleName_LTR, "DataReset");
                //SYSPara.CallProc(ModuleName_HDT_TR, "DataReset");

                SYSPara.MusicOn = true;
                SYSPara.SysRun = true;
            }
            //Stop Button
            if (((result >> 1) & 0x01) == 1)
            {
                if (SYSPara.SysRun)
                    SYSPara.LogSay(EnLoggerType.EnLog_OP, "User Click「Stop」");

                if (SYSPara.SysState == StateMode.SM_ALARM)
                {
                    SYSPara.ErrorStop = false;
                    SYSPara.SysState = StateMode.SM_PAUSE;
                }

                SYSPara.MusicOn = false;
                SYSPara.SysRun = false;
                FormSet.mMSS.M_Stop();
            }
            //Alarm Result Button
            if (((result >> 2) & 0x01) == 1)
            {
                SYSPara.Alarm.ClearAll();
                if (SYSPara.SysState == StateMode.SM_ALARM || SYSPara.SysState == StateMode.SM_PAUSE)
                {
                    SYSPara.ErrorStop = false;
                    SYSPara.SysState = StateMode.SM_PAUSE;
                }
                SYSPara.MusicOn = false;                
            }

            #endregion
        }

        //歸零前的重置
        public void HomeReset()
        {
            #region 使用者修改 (針對各模式的歸零前置設定)
            FC_Initial.TaskReset();
            DataReset();
            BoardSeq = 0;
            BoardCount = 0;
            bLoadToBox_A = false;
            bLoadToBox_A = false;

            HDTA_NozzleStateIsFail = false;
            HDTB_NozzleStateIsFail = false;

            HDT_BIB_BIBStageID = BIBStageID.NONE;

            if (BDSMC != null)
                BDSMC.ReleaseCommandProcess("");
            #endregion

            mHomeOk = false;
        }

        //歸零
        public bool Home()
        {
            FC_Initial.MainRun();
            //return true;
            return mHomeOk;
        }

        //運轉前初始化
        public void RunReset()
        {
            FC_TransferRobot.TaskReset();
            FC_Clamshell.TaskReset();
            FC_BoardStageA.TaskReset();
            FC_BoardStageB.TaskReset();
            FC_Booking.TaskReset();
            FC_BoardHeadA.TaskReset();
            FC_BoardHeadAPnP.TaskReset();
            FC_BoardHeadB.TaskReset();
            FC_BoardHeadBPnP.TaskReset();
            FC_ShuttleA.TaskReset();
            FC_ShuttleB.TaskReset();
            if (mBFM.GetUseModule())
            {
                FC_BowFeederA.TaskReset();
                FC_BowFeederB.TaskReset();
            }
        }

        //運轉
        public void Run()
        {
            FC_TransferRobot.MainRun();
            FC_Clamshell.MainRun();
            FC_BoardStageA.MainRun();
            FC_BoardStageB.MainRun();
            FC_Booking.MainRun();
            FC_BoardHeadA.MainRun();
            FC_BoardHeadAPnP.MainRun();
            FC_BoardHeadB.MainRun();
            FC_BoardHeadBPnP.MainRun();
            FC_ShuttleA.MainRun();
            FC_ShuttleB.MainRun();
            if (mBFM.GetUseModule())
            {
                FC_BowFeederA.MainRun();
                FC_BowFeederB.MainRun();
            }
            ////是否進行結批流程
            //if (SYSPara.Lotend)
            //{
            //    //設定MainFlow模組結批完成
            //    SYSPara.LotendOk = true;
            //}

            JBINQTY LoadQty = GlobalDefine.OEE.GetBinQty(BinDefine.Untested);
            JBINQTY UnLoadQty = GlobalDefine.OEE.GetBinQty(BinDefine.Tested);

            if (LoadQty.Process >= SYSPara.NewLotInfo.LotQuantity && SYSPara.NewLotInfo.LotQuantity > 0 && SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD))
            {
                SYSPara.LotendOk = false;
                SYSPara.Lotend = true;
                SYSPara.Alarm.Say("I0504"); //結批開始
            }
            if (UnLoadQty.Process >= SYSPara.NewLotInfo.LotQuantity && SYSPara.NewLotInfo.LotQuantity > 0 && SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.UNLOAD))
            {
                SYSPara.LotendOk = false;
                SYSPara.Lotend = true;
                SYSPara.Alarm.Say("I0504"); //結批開始
            }

            if (SYSPara.Lotend)
            {
                SYSPara.LotendOk = Flag_TRM_LotEnd.IsDone() && Flag_CHM_LotEnd.IsDone() && Flag_LeftBSM_LotEnd.IsDone() && Flag_RightBSM_LotEnd.IsDone() &&
                Flag_TopHDT_LotEnd.IsDone() && Flag_BottomHDT_LotEnd.IsDone() && Flag_LeftKitShuttle_LotEnd.IsDone() && Flag_RightKitShuttle_LotEnd.IsDone() &&
                Flag_TopBowlFeeder_LotEnd.IsDone() && Flag_BottomBowlFeeder_LotEnd.IsDone();
                //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
                SYSPara.LotendOk = SYSPara.LotendOk && Flag_HDT_TR_LotEnd.IsDone() && Flag_VTray_LotEnd.IsDone();
            }

            if (SYSPara.LotendOk)
            {
                ExportLotEndReport();
            }
        }

        //手動運行前置設定
        public void ManualReset()
        {
        }

        //手動模式運行
        public void ManualRun()
        {
        }

        //維修模式前置設定
        public void MaintenanceReset()
        {
        }

        //維修模式運行
        public void MaintenanceRun()
        {
        }
        #endregion

        #region 公開函數
        //public bool SetBDLStartLot(LotInformation_SM LotInfo)
        //{
        //    CommandReturn CRR = CommandReturn.CR_Waitting;
        //    int iErrorCode = 0;
        //    string sErrorMsg = "";

        //    while (CRR == CommandReturn.CR_Waitting)
        //    {
        //        CRR = BDSMC.SetLotData("LotStartSetLotData", LotInfo, 20000);
        //        BDSMC.GetErrorMessage(ref iErrorCode, ref sErrorMsg);
        //    }
        //    BDSMC.ReleaseCommandProcess("LotStartSetLotData");
        //    if (CRR != CommandReturn.CR_OK)
        //    {
        //        int Alarm = (iErrorCode + 1400);
        //        //ShowAlarmMessage("E", (int)Alarm, "DataLink", sErrorMsg);
        //        SYSPara.ShowAlarm("E", (int)Alarm, "DataLink", sErrorMsg);
        //        return false;
        //    }
        //    return true;
        //}
         

        public bool CreatBoardDataLinkerInfo()
        {
            bool ret = true;
            //LotInformation_SM lotinfo_sm = new LotInformation_SM();
            //客戶編號(ProV/...)
            lotinfo_sm.CustomerID = 0;
            //連線樣式(Aher Link)
            lotinfo_sm.LinkType = 8;
            //生產模式(None/Load/Unload)
            lotinfo_sm.ProductionMode = (int)SYSPara.SystemProductionMode;
            //批號
            lotinfo_sm.LotNo = SYSPara.NewLotInfo.LotNumber;
            //材料數量
            lotinfo_sm.Quantity = SYSPara.NewLotInfo.LotQuantity;
            //產品名稱
            lotinfo_sm.ProductName = SYSPara.NewLotInfo.DeviceType; ;
            //板樣式
            //lotinfo_sm.BoardType = SYSPara.NewLotInfo.DiePakType;
            //Block Col 數量 (BXN)
            lotinfo_sm.BlockColCount = SYSPara.PReadValue("BoardTable", "Board_XN").ToInt();
            //Block Row 數量 (BYN)
            lotinfo_sm.BlockRowCount = SYSPara.PReadValue("BoardTable", "Board_YN").ToInt();
            //Dut Col 數量 (DXN)
            lotinfo_sm.DutColCount = 1;// SYSPara.PReadValue("BoardTable", "Socket_XN").ToInt();
            //Dut Row 數量 (DYN)
            lotinfo_sm.DutRowCount = 1;// SYSPara.PReadValue("BoardTable", "Socket_YN").ToInt();
            //OP ID
            lotinfo_sm.OpID = SYSPara.NewLotInfo.OperatorID;
            //Machine ID
            lotinfo_sm.MachineNo = SYSPara.OReadValue("EquipmentID").ToString();

            lotinfo_sm.Reserve2 = SYSPara.NewLotInfo.TestProgram;//Aher Setting TestProgram Name

            //ret = SetBDLStartLot(lotinfo_sm);

            return ret;
        }

        public void RejectLeftBoard()
        {
            NeedRejectLeftBoard = true;
        }

        public void RejectRightBoard()
        {
            NeedRejectRightBoard = true;
        }

        public bool ExportLotEndReport()
        {
            bool ret = false;
            if (GlobalDefine.OEE.NeedExportLotEndReport())
            {
                JBINQTY LoadQty = GlobalDefine.OEE.GetBinQty(BinDefine.Untested);
                JBINQTY UnLoadQty = GlobalDefine.OEE.GetBinQty(BinDefine.Tested);
                GlobalDefine.OEE.EndLot((int)SYSPara.RunSecond, (int)SYSPara.StopSecond, (int)SYSPara.AlarmSecond);
                double uph = ((double)(LoadQty.Completed + UnLoadQty.Completed) / (double)SYSPara.RunSecond) * 3.6;
                SYSPara.logDB.SpcSayDb(GlobalDefine.OEE.StartTime, FormSet.mPackage.FileName_Auto, SYSPara.NewLotInfo.LotNumber, (float)(LoadQty.Completed + UnLoadQty.Completed), (float)uph);
            }
            ret = true;
            return ret;
        }

        public void ExportLotStartReport(string LotNo, int LotQuantity)
        {
            SYSPara.RunSecond = 0;
            SYSPara.StopSecond = 0;
            SYSPara.AlarmSecond = 0;
            //ExportLotEndTask.Reset();
            SYSPara.NewLotInfo.LotNumber = LotNo;
            SYSPara.NewLotInfo.LotQuantity = LotQuantity;
            GlobalDefine.OEE.Reset();
            GlobalDefine.OEE.LotReportDirectory = SYSPara.OReadValue("LotReportPath").ToString();
            GlobalDefine.OEE.NewLot(SYSPara.NewLotInfo.LotNumber, SYSPara.NewLotInfo.OperatorID, SYSPara.NewLotInfo.DiePakType, SYSPara.NewLotInfo.DeviceType,
            SYSPara.NewLotInfo.TestProgram, SYSPara.PackageName, SYSPara.SystemProductionMode.ToString(), SYSPara.OReadValue("EquipmentID").ToString(),
            FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.ToString(), SYSPara.NewLotInfo.LotQuantity);

            FormSet.mMainFlow.CreatBoardDataLinkerInfo();
            return;
        }

        #endregion

        #region 私有函數
        /// <summary>
        /// 資料設定
        /// </summary>
        private void DataReset()
        {
            SYSPara.CallProc(ModuleName_TRM, "DataReset");
            SYSPara.CallProc(ModuleName_CHM, "DataReset");
            SYSPara.CallProc(ModuleName_BSM, "DataReset");
            SYSPara.CallProc(ModuleName_HDT, "DataReset");
            SYSPara.CallProc(ModuleName_KSM, "DataReset");
            SYSPara.CallProc(ModuleName_BFM, "DataReset");
            SYSPara.CallProc(ModuleName_MAA, "DataReset");
            SYSPara.CallProc(ModuleName_CSM, "DataReset");
            SYSPara.CallProc(ModuleName_LTR, "DataReset");
            SYSPara.CallProc(ModuleName_RTR, "DataReset");
            SYSPara.CallProc(ModuleName_HDT_TR, "DataReset");

            LoadMachineData();
            LoadPackageData();

            SYSPara.CurrentBoard = new BoardBinInfo();

            //預約系統
            Board_BKS = new BoardBookingSystem();
            HDT_BIB_A_PnP = new BoardHeadPnPNavigator();
            HDT_BIB_B_PnP = new BoardHeadPnPNavigator();
            HDT_BIB_A_PnP.SocketXPitch = BoardInfo.XP;
            HDT_BIB_A_PnP.SocketYPitch = BoardInfo.YP;
            HDT_BIB_A_PnP.BlockYPitch = 0;
            HDT_BIB_B_PnP.SocketXPitch = BoardInfo.XP;
            HDT_BIB_B_PnP.SocketYPitch = BoardInfo.YP;
            HDT_BIB_B_PnP.BlockYPitch = 0;

            //TrayData
            TDEx_Bottom_BFM = new TrayDataEx(td_Bottom_BFM);
            TDEx_Top_BFM = new TrayDataEx(td_Top_BFM);
            TDEx_HDT_BF_A = new TrayDataEx(td_HDT_BF_A);
            TDEx_HDT_BF_B = new TrayDataEx(td_HDT_BF_B);

            TDEx_TRM = new TrayDataEx(td_TRM);
            TDEx_CHM = new TrayDataEx(td_CHM);

            TDEx_Left_KitShuttle = new TrayDataEx(td_Left_KitShuttle);
            TDEx_Right_KitShuttle = new TrayDataEx(td_Right_KitShuttle);

            TDEx_Left_Board = new TrayDataEx(td_Left_Board);
            TDEx_Right_Board = new TrayDataEx(td_Right_Board);

            TDEx_HDT_BIB_A = new TrayDataEx(td_HDT_BIB_A);
            TDEx_HDT_BIB_B = new TrayDataEx(td_HDT_BIB_B);

            TDEx_Load = new TrayDataEx(td_Load);
            TDEx_Unload = new TrayDataEx(td_Unload);

            TDEx_PassBox = new TrayDataEx(td_PassBox);
            TDEx_FailBox = new TrayDataEx(td_FailBox);

            TDEx_LeftTray = new TrayDataEx(td_LeftTray);
            TDEx_RightTray = new TrayDataEx(td_RightTray);
            TDEx_HDT_TR = new TrayDataEx(td_HDT_TR);

            //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
            TDEx_LeftTray.SetInfo(1, 1, 1, 1, TrayInfo.XN, TrayInfo.YN, TrayInfo.XP, TrayInfo.YP, TrayInfo.XO, TrayInfo.YO, 0, 0);
            TDEx_LeftTray.ResetBin(GlobalDefine.EmptyBin, 1, false);
            TDEx_RightTray.SetInfo(1, 1, 1, 1, TrayInfo.XN, TrayInfo.YN, TrayInfo.XP, TrayInfo.YP, TrayInfo.XO, TrayInfo.YO, 0, 0);
            TDEx_RightTray.ResetBin(GlobalDefine.EmptyBin, 1, false);

            TDEx_HDT_TR.SetInfo(1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 0, 0);
            TDEx_HDT_TR.ResetBin(GlobalDefine.EmptyBin, 1, false);

            TDEx_Right_Board.SetInfo(1, 1, 1, 1, BoardInfo.XN, BoardInfo.YN, BoardInfo.XP, BoardInfo.YP, BoardInfo.XO, BoardInfo.YO, 0, 0);
            TDEx_Right_Board.ResetBin(GlobalDefine.EmptyBin, 1, false);
            TDEx_Left_Board.SetInfo(1, 1, 1, 1, BoardInfo.XN, BoardInfo.YN, BoardInfo.XP, BoardInfo.YP, BoardInfo.XO, BoardInfo.YO, 0, 0);
            TDEx_Left_Board.ResetBin(GlobalDefine.EmptyBin, 1, false);

            TDEx_Left_KitShuttle.SetInfo(1, 1, 1, 1, KitInfo.XN, KitInfo.YN, KitInfo.XP, KitInfo.YP, KitInfo.XO, KitInfo.YO, 0, 0);
            TDEx_Left_KitShuttle.ResetBin(GlobalDefine.EmptyBin, 1, false);
            TDEx_Right_KitShuttle.SetInfo(1, 1, 1, 1, KitInfo.XN, KitInfo.YN, KitInfo.XP, KitInfo.YP, KitInfo.XO, KitInfo.YO, 0, 0);
            TDEx_Right_KitShuttle.ResetBin(GlobalDefine.EmptyBin, 1, false);

            //TDEx_Load.SetInfo(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0);
            //v2.0.0.1 20251222 改成RACK
            TDEx_Load.SetInfo(1, 1, 1, 1, 1, SYSPara.PReadValue("CassetteTable", "Cassette_Num").ToInt(), 1, 1, 1, 1, 0, 0);
            TDEx_Load.ResetBin(GlobalDefine.EmptyBin, 1, false);

            //TDEx_HDT_BIB_A.SetInfo(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0);
            //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
            TDEx_HDT_BIB_A.SetInfo(1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 0, 0);
            TDEx_HDT_BIB_A.ResetBin(GlobalDefine.EmptyBin, 1, false);

            //TDEx_HDT_BIB_B.SetInfo(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0);
            //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
            TDEx_HDT_BIB_B.SetInfo(1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 0, 0);
            TDEx_HDT_BIB_B.ResetBin(GlobalDefine.EmptyBin, 1, false);

            //TDEx_Unload.SetInfo(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0);
            //v2.0.0.1 20251222 改成RACK
            TDEx_Unload.SetInfo(1, 1, 1, 1, 1, SYSPara.PReadValue("CassetteTable", "Cassette_Num").ToInt(), 1, 1, 1, 1, 0, 0);
            TDEx_Unload.ResetBin(GlobalDefine.EmptyBin, 1, false);

            TDEx_CHM.SetInfo(1, 1, 1, 1, BoardInfo.XN, BoardInfo.YN, BoardInfo.XP, BoardInfo.YP, BoardInfo.XO, BoardInfo.YO, 0, 0);
            TDEx_CHM.ResetBin(GlobalDefine.EmptyBin, 1, false);


            TDEx_HDT_BF_A.SetInfo(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0);
            TDEx_HDT_BF_A.ResetBin(GlobalDefine.EmptyBin, 1, false);

            TDEx_HDT_BF_B.SetInfo(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0);
            TDEx_HDT_BF_B.ResetBin(GlobalDefine.EmptyBin, 1, false);

            TDEx_Top_BFM.SetInfo(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0);
            TDEx_Top_BFM.ResetBin(GlobalDefine.EmptyBin, 1, false);

            TDEx_Bottom_BFM.SetInfo(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0);
            TDEx_Bottom_BFM.ResetBin(GlobalDefine.EmptyBin, 1, false);

            TDEx_TRM.SetInfo(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0);
            TDEx_TRM.ResetBin(GlobalDefine.EmptyBin, 1, false);

            TDEx_PassBox.SetInfo(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0);
            TDEx_PassBox.ResetBin(GlobalDefine.EmptyBin, 1, false);

            TDEx_FailBox.SetInfo(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0);
            TDEx_FailBox.ResetBin(GlobalDefine.EmptyBin, 1, false);

            HDT_BF_A_KitShuttleID = KitShuttleID.NONE;
            HDT_BF_B_KitShuttleID = KitShuttleID.NONE;

            HDT_BIB_A_KitShuttleID = KitShuttleID.NONE;
            HDT_BIB_B_KitShuttleID = KitShuttleID.NONE;

            LeftKitStateControl.Reset();
            RightKitStateControl.Reset();

            Flag_BottomBowlFeeder_LotEnd.Reset();
            Flag_TopBowlFeeder_LotEnd.Reset();
            Flag_RightKitShuttle_LotEnd.Reset();
            Flag_LeftKitShuttle_LotEnd.Reset();
            Flag_RightBSM_LotEnd.Reset();
            Flag_LeftBSM_LotEnd.Reset();
            Flag_BottomHDT_LotEnd.Reset();
            Flag_TopHDT_LotEnd.Reset();
            Flag_TRM_LotEnd.Reset();
            Flag_CHM_LotEnd.Reset();
            //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
            Flag_VTray_LotEnd.Reset();
            Flag_HDT_TR_LotEnd.Reset();
            
            Flag_HDT_BIB_A_PnP.Reset();
            Flag_HDT_BIB_B_PnP.Reset();

            Flag_HDT_BIB_A_Action.Reset();
            Flag_HDT_BIB_B_Action.Reset();

            Flag_HDT_BIB_A_Vision.Reset();
            Flag_HDT_BIB_A_Vision_InAdvance.Reset();
            Flag_HDT_BIB_B_Vision.Reset();
            Flag_HDT_BIB_B_Vision_InAdvance.Reset();

            //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
            Flag_HDT_TR_PnP.Reset();
            Flag_Load_ScanAction.Reset();
            Flag_Load_WorkAction.Reset();
            Flag_SuppleDeviceAction.Reset();

            //LeftCassette.Reset();
            //RightCassette.Reset();
            WorkingBoard.Reset();
            StageA_Board.Reset();
            StageB_Board.Reset();

            Flag_CHM_Action.Reset();
            //Flag_Booking.Reset();
            Flag_Booking_A.Reset();
            Flag_Booking_B.Reset();

            //PnP calc reset
            //HDT_BF_A_PnP = new PnPCalculator(TDEx_HDT_BF_A, HDT_BF_A_INFO.Max_Pitch_X, HDT_BF_A_INFO.Min_Pitch_X, HDT_BF_A_INFO.Max_Pitch_Y, HDT_BF_A_INFO.Min_Pitch_Y, HDT_BF_A_INFO.HasX, HDT_BF_A_INFO.HasY);
            //HDT_BF_B_PnP = new PnPCalculator(TDEx_HDT_BF_B, HDT_BF_B_INFO.Max_Pitch_X, HDT_BF_B_INFO.Min_Pitch_X, HDT_BF_B_INFO.Max_Pitch_Y, HDT_BF_B_INFO.Min_Pitch_Y, HDT_BF_B_INFO.HasX, HDT_BF_B_INFO.HasY);
            //HDT_BIB_A_PnP = new PnPCalculator(TDEx_HDT_BIB_A, HDT_BIB_A_INFO.Max_Pitch_X, HDT_BIB_A_INFO.Min_Pitch_X, HDT_BIB_A_INFO.Max_Pitch_Y, HDT_BIB_A_INFO.Min_Pitch_Y, HDT_BIB_A_INFO.HasX, HDT_BIB_A_INFO.HasY);
            //HDT_BIB_B_PnP = new PnPCalculator(TDEx_HDT_BIB_B, HDT_BIB_B_INFO.Max_Pitch_X, HDT_BIB_B_INFO.Min_Pitch_X, HDT_BIB_B_INFO.Max_Pitch_Y, HDT_BIB_B_INFO.Min_Pitch_Y, HDT_BIB_B_INFO.HasX, HDT_BIB_B_INFO.HasY);

            //Lock
            LeftKitUser = KitShuttleOwner.NONE;
            RightKitUser = KitShuttleOwner.NONE;

            //Module Data Reset (unknow)
            MainFlow_DialogFormType = DialogFormType.None;

            MainFlowModuleLotEndControl(true);
            bool bUseTRM = mTRM.GetUseModule();
            bool bUseCHM = mCHM.GetUseModule();
            bool bUseBSM = mBSM.GetUseModule();
            bool bUseHDT = mHDT.GetUseModule();
            bool bUseKSM = mKSM.GetUseModule();
            bool bUseBFM = mBFM.GetUseModule();
            //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
            bool bUseHDT_TR = mHDT_TR.GetUseModule();
            bool bUseLeftTray = mLTR.GetUseModule();
            bool bUseRightTray = mRTR.GetUseModule();
            //TODO:need add log for use list
            string SystemEquipmentID = SYSPara.OReadValue("EquipmentID").ToString();
            int SystemMachineRate = SYSPara.OReadValue("機台速率").ToInt();
            bool SystemDryRun = SYSPara.OReadValue("DryRun").ToBoolean();
            bool SystemDryRunWithoutIC = SYSPara.OReadValue("DryRunWithoutIC").ToBoolean();
            bool SystemDryRunWithoutTopCCD = SYSPara.OReadValue("DryRunWithoutTopCCD").ToBoolean();
            bool SystemDryRunWithoutBottomCCD = SYSPara.OReadValue("DryRunWithoutBottomCCD").ToBoolean();
            bool SystemDryRunWithoutBFM = SYSPara.OReadValue("DryRunWithoutBFM").ToBoolean();
            bool SystemDryRunWithoutHDT = SYSPara.OReadValue("DryRunWithoutHDT").ToBoolean();
            if (SystemDryRunWithoutHDT) SystemDryRunWithoutBFM = true;
            int LinkMode = SYSPara.PReadValue("LinkType").ToInt();
            SYSPara.ProductionLinkMode = (LinkMode)SYSPara.PReadValue("LinkType").ToInt();
            MainFlowModuleLotEndControl(false);
            NeedRejectLeftBoard = false;
            NeedRejectRightBoard = false;

            Flag_Booking_A_Work = false;
            Flag_Booking_B_Work = false;

            InitRackSlots();
        }

        private void MainFlowModuleLotEndControl(bool reset = true)
        {
            if (reset)
            {
                MainFlowLotEndControlStart = false;
                //Lot End Flag Reset
                SYSPara.Lotend = false;
                Task_MainFLotEnd.Reset();
                Flag_TRM_LotEnd.Reset();
                Flag_CHM_LotEnd.Reset();
                Flag_TopHDT_LotEnd.Reset();
                Flag_BottomHDT_LotEnd.Reset();
                Flag_LeftBSM_LotEnd.Reset();
                Flag_RightBSM_LotEnd.Reset();
                Flag_LeftKitShuttle_LotEnd.Reset();
                Flag_RightKitShuttle_LotEnd.Reset();
                Flag_TopBowlFeeder_LotEnd.Reset();
                Flag_BottomBowlFeeder_LotEnd.Reset();
                //v2.0.0.1 20251219 Mars TBPP14202 新增TrayModule & Tray Head
                Flag_HDT_TR_LotEnd.Reset();
                Flag_VTray_LotEnd.Reset();
            }
            else
            {
                MainFlowLotEndControlStart = true;
            }
        }

        /// <summary>
        /// 讀取機台設定
        /// </summary>
        private void LoadMachineData()
        {
            //因為只有一根吸嘴 所以不需設定
            HDT_BF_A_INFO.Max_Pitch_X = 1;
            HDT_BF_A_INFO.Min_Pitch_X = 1;
            HDT_BF_A_INFO.Max_Pitch_Y = 1;
            HDT_BF_A_INFO.Min_Pitch_Y = 1;
            HDT_BF_A_INFO.HasY = false;
            HDT_BF_A_INFO.HasX = false;
            HDT_BF_A_INFO.Num_X = 1;
            HDT_BF_A_INFO.Num_Y = 1;
            HDT_BF_B_INFO = HDT_BF_A_INFO;

            //HDT_BIB_A_INFO.Max_Pitch_X = 1;
            //HDT_BIB_A_INFO.Min_Pitch_X = 1;
            //HDT_BIB_A_INFO.Max_Pitch_Y = 1;
            //HDT_BIB_A_INFO.Min_Pitch_Y = 1;
            //HDT_BIB_A_INFO.HasY = false;
            //HDT_BIB_A_INFO.HasX = false;
            //HDT_BIB_A_INFO.Num_X = 1;
            //HDT_BIB_A_INFO.Num_Y = 1;
            //HDT_BIB_B_INFO = HDT_BIB_A_INFO;
            HDT_BIB_A_INFO = (HEAD_INFO)SYSPara.CallProc(ModuleName_HDT, "GetHeadInfo");
            HDT_BIB_B_INFO = HDT_BIB_A_INFO;
        }

        /// <summary>
        /// 讀取產品設定
        /// </summary>
        private void LoadPackageData()
        {
            BoardInfo.XN = SYSPara.PReadValue("BoardTable", "Board_XN").ToInt();
            BoardInfo.YN = SYSPara.PReadValue("BoardTable", "Board_YN").ToInt();
            BoardInfo.XP = SYSPara.PReadValue("BoardTable", "Board_XPitch").ToInt();
            BoardInfo.YP = SYSPara.PReadValue("BoardTable", "Board_YPitch").ToInt();
            BoardInfo.XO = SYSPara.PReadValue("BoardTable", "Board_XShift").ToInt();
            BoardInfo.YO = SYSPara.PReadValue("BoardTable", "Board_YShift").ToInt();
            BoardInfo.BLK_Height = SYSPara.PReadValue("BoardTable", "Board_Thickness").ToInt();
            //BoardInfo.XN = SYSPara.PReadValue("BoardTable", "Socket_XN").ToInt();
            //BoardInfo.YN = SYSPara.PReadValue("BoardTable", "Socket_YN").ToInt();
            //BoardInfo.XP = SYSPara.PReadValue("BoardTable", "Socket_XPitch").ToInt();
            //BoardInfo.YP = SYSPara.PReadValue("BoardTable", "Socket_YPitch").ToInt();
            //BoardInfo.XO = SYSPara.PReadValue("BoardTable", "Socket_XShift").ToInt();
            //BoardInfo.YO = SYSPara.PReadValue("BoardTable", "Socket_YShift").ToInt();
            BoardInfo.Length = SYSPara.PReadValue("BoardTable", "Board_Length").ToInt();  //Size X
            BoardInfo.Width = SYSPara.PReadValue("BoardTable", "Board_Width").ToInt();    //Size Y
            BoardInfo.Height = SYSPara.PReadValue("BoardTable", "Board_Thickness").ToInt();
            BoardInfo.Depth = SYSPara.PReadValue("BoardTable", "Board_Depth").ToInt();
            BoardInfo.DeviceHeight = SYSPara.PReadValue("DeviceTable", "Device_Thickness").ToInt();
            BoardInfo.DeviceLength = SYSPara.PReadValue("DeviceTable", "Device_Length").ToInt();
            BoardInfo.DeviceWidth = SYSPara.PReadValue("DeviceTable", "Device_Width").ToInt();
            BoardInfo.LID_Dir = SYSPara.PReadValue("BoardTable", "LID_Dir").ToInt();
            BoardInfo.Dir = SYSPara.PReadValue("BoardTable", "Pin1_Dir").ToInt();

            KitInfo.BLK_XN = 1;
            KitInfo.BLK_YN = 1;
            KitInfo.XN = SYSPara.PReadValue("KitTable", "Kit_XN").ToInt();
            KitInfo.YN = SYSPara.PReadValue("KitTable", "Kit_YN").ToInt() / KitInfo.BLK_YN;
            KitInfo.XP = SYSPara.PReadValue("KitTable", "Kit_XPitch").ToInt();
            KitInfo.YP = SYSPara.PReadValue("KitTable", "Kit_YPitch").ToInt();
            KitInfo.XO = SYSPara.PReadValue("KitTable", "Kit_XShift").ToInt();
            KitInfo.YO = SYSPara.PReadValue("KitTable", "Kit_YShift").ToInt();
            KitInfo.Length = SYSPara.PReadValue("KitTable", "Kit_Length").ToInt();
            KitInfo.Width = SYSPara.PReadValue("KitTable", "Kit_Width").ToInt();
            KitInfo.Height = SYSPara.PReadValue("KitTable", "Kit_Thickness").ToInt();
            KitInfo.Depth = SYSPara.PReadValue("KitTable", "Kit_Depth").ToInt();
            KitInfo.DeviceHeight = SYSPara.PReadValue("DeviceTable", "Device_Thickness").ToInt();

            TrayInfo.XN = SYSPara.PReadValue("TrayTable", "JEDECTray_XN").ToInt();
            TrayInfo.YN = SYSPara.PReadValue("TrayTable", "JEDECTray_YN").ToInt();
            TrayInfo.XP = SYSPara.PReadValue("TrayTable", "JEDECTray_XPitch").ToInt();
            TrayInfo.YP = SYSPara.PReadValue("TrayTable", "JEDECTray_YPitch").ToInt();
            TrayInfo.XO = SYSPara.PReadValue("TrayTable", "JEDECTray_XShift").ToInt();
            TrayInfo.YO = SYSPara.PReadValue("TrayTable", "JEDECTray_YShift").ToInt();
            TrayInfo.Length = SYSPara.PReadValue("TrayTable", "JEDECTray_Length").ToInt();
            TrayInfo.Width = SYSPara.PReadValue("TrayTable", "JEDECTray_Width").ToInt();
            TrayInfo.Depth = SYSPara.PReadValue("TrayTable", "JEDECTray_Depth").ToInt();
            TrayInfo.Height = SYSPara.PReadValue("TrayTable", "JEDECTray_Height").ToInt();
            TrayInfo.Notch = SYSPara.PReadValue("TrayTable", "JEDECTray_Notch").ToInt();
            TrayInfo.TrayThickness = SYSPara.PReadValue("TrayTable", "JEDECTray_Thickness").ToInt();
        }

        private void GetControls(Control ctr)
        {
            foreach (Control myctr in ctr.Controls)
            {
                //如果傳進來的控制項有子控制項的話
                if (myctr.HasChildren == true)
                {
                    //就遞迴呼叫自己
                    GetControls(myctr);
                }

                if (myctr is FlowChart)
                    FlowChartList.Add((FlowChart)myctr);
            }
        }

        private void ShowDialogFormProcess()
        {
            while (!StopDialogFormThread)
            {
                while (ShowDialogForm)
                {
                    if (FormSet.mMainFlowDialogForm != null)
                    {
                        FormSet.mMainFlowDialogForm.ShowDialogForm(MainFlow_DialogFormType, MainFlow_DialogFormMessage);
                        MainFLow_DialogFormReturn = FormSet.mMainFlowDialogForm.ActionResult;
                        MainFlow_DialogFormInput = FormSet.mMainFlowDialogForm.InputResult;
                    }
                    MainFlow_DialogFormType = DialogFormType.None;
                    ShowDialogForm = false;
                }
                Thread.Sleep(20);
            }
        }

        private void ShowAlarm(string AlarmLevel, int ErrorCode, params object[] args)
        {
            string code = string.Format("{0}{1:0000}", AlarmLevel, ErrorCode);
            SYSPara.Alarm.Say(code, args);
        }

        private void CallDialogForm(DialogFormType type, string message)
        {
            if (MainFlow_DialogFormType.Equals(DialogFormType.None))
            {
                MainFlow_DialogFormType = type;
                MainFlow_DialogFormMessage = message;
                ShowDialogForm = true;
            }
        }

        private bool LockKit(KitShuttleOwner user, KitShuttleID id)
        {
            bool bRet = false;
            switch (id)
            {
                case KitShuttleID.TransferShuttleA:
                    {
                        if (LeftKitUser.Equals(KitShuttleOwner.NONE) || LeftKitUser.Equals(user))
                        {
                            LeftKitUser = user;
                            bRet = true;
                        }
                    }
                    break;
                case KitShuttleID.TransferShuttleB:
                    {
                        if (RightKitUser.Equals(KitShuttleOwner.NONE) || RightKitUser.Equals(user))
                        {
                            RightKitUser = user;
                            bRet = true;
                        }
                    }
                    break;
                default:
                    {
                        bRet = false;
                    }
                    break;
            }
            return bRet;
        }

        private bool UnLockKit(KitShuttleStateControl user, KitShuttleID id)
        {
            bool bRet = false;
            switch (id)
            {
                case KitShuttleID.TransferShuttleA:
                    {
                        if (LeftKitUser.Equals(user) || LeftKitUser.Equals(KitShuttleID.NONE))
                        {
                            LeftKitUser = KitShuttleOwner.NONE;
                            bRet = true;
                        }
                    }
                    break;
                case KitShuttleID.TransferShuttleB:
                    {
                        if (RightKitUser.Equals(user) || RightKitUser.Equals(KitShuttleID.NONE))
                        {
                            RightKitUser = KitShuttleOwner.NONE;
                            bRet = true;
                        }
                    }
                    break;
                default:
                    {
                        bRet = false;
                    }
                    break;
            }
            return bRet;
        }

        private void InitRackSlots()
        {
            AllSlots.Clear();
            for (int i = 0; i < SYSPara.PReadValue("Cassette_Num").ToInt(); i++) AllSlots.Add(new BoardInfo(CassetteID.LEFT, i));
            for (int i = 0; i < SYSPara.PReadValue("Cassette_Num").ToInt(); i++) AllSlots.Add(new BoardInfo(CassetteID.RIGHT, i));
        }

        private BoardInfo GetFirstToDoBoard(CassetteID cid)
        {
            foreach (BoardInfo board in AllSlots)
            {
                if (board.CID == cid && board.State.IsDoIt())
                {
                    return board;
                }
            }
            return null;
        }

        private void SetBoardWorkDone(BoardInfo workingboard)
        {
            CassetteID homeID = workingboard.CID;
            int homeSlot = workingboard.Slot;

            foreach (var b in AllSlots)
            {
                if (b.CID == homeID && b.Slot == homeSlot)
                {
                    b.BID = workingboard.BID;
                    b.BeforeMap = workingboard.BeforeMap;
                    b.AfterMap = workingboard.AfterMap;
                    b.NowStation = workingboard.CID == CassetteID.LEFT ? TRMStation.LOAD_A : TRMStation.LOAD_B;
                    b.State.Done();
                    break;
                }
            }
        }

        private int GetDoItCount(CassetteID cid)
        {
            int count = 0;
            foreach (BoardInfo board in AllSlots)
            {
                if (board.CID == cid && board.State.IsDoIt())
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// 封裝更新邏輯
        /// </summary>
        private void UpdateStationData(List<string> mapList, TrayDataEx tray, CassetteID cid)
        {
            //TrayData update
            for (int i = 0; i < mapList.Count; i++)
            {
                byte binValue = (mapList[i] == "1") ? (byte)BinDefine.Bin1 : (byte)BinDefine.Empty;
                tray.SetBin(0, 0, 0, i, binValue);

                UpdateSlotsState(cid, i, BoardState.ToDo);
            }
        }

        private void UpdateSlotsState(CassetteID cid, int slot, BoardState newState)
        {
            foreach (BoardInfo board in AllSlots)
            {
                if (board.CID == cid && board.Slot == slot)
                {
                    switch (newState)
                    {
                        case BoardState.ToDo: board.State.DoIt(); break;
                        case BoardState.Processing: board.State.Doing(); break;
                        case BoardState.Finished: board.State.Done(); break;
                        case BoardState.Idle: board.State.Reset(); break;
                    }
                    return;
                }
            }
        }

        private void SetMappingData(string sMap, TRMStation station)
        {
            if (string.IsNullOrEmpty(sMap)) return;

            List<string> list = sMap.TrimEnd(',').Split(',').ToList();
            switch (station)
            {
                case TRMStation.LOAD_A:
                    UpdateStationData(list, TDEx_Load, CassetteID.LEFT);
                    break;
                case TRMStation.LOAD_B:
                    UpdateStationData(list, TDEx_Unload, CassetteID.RIGHT);
                    break;
                default:
                    //非Rack TrayData不動作
                    break;
            }
        }

        private BoardInfo TryGetNextBoard(out CassetteID selectedID)
        {
            CassetteID[] priorityList = { CassetteID.LEFT, CassetteID.RIGHT };

            foreach (CassetteID cid in priorityList)
            {
                if (GetDoItCount(cid) > 0)
                {
                    selectedID = cid;
                    BoardInfo board = GetFirstToDoBoard(cid);

                    if (board != null)
                    {
                        UpdateSlotsState(board.CID, board.Slot, BoardState.Processing);
                        return board;
                    }
                }
            }

            // 若都沒貨
            selectedID = CassetteID.NONE;
            return null;
        }

        private string GetBoardMap(TrayDataEx TDEx_Temp, bool original = false)
        {
            string sMap = string.Empty;
            string sBlockMap = string.Empty;
            for (int cx = 0; cx < TDEx_Temp.Cols; ++cx)
            {
                for (int cy = 0; cy < TDEx_Temp.Rows; ++cy)
                {
                    byte bin = TDEx_Temp.GetBin(0, 0, cx, cy, original);
                    string binstr = GlobalDefine.BinToStr(bin);
                    if (string.IsNullOrEmpty(sBlockMap))
                    {
                        sBlockMap = string.Format("{0}", binstr);
                    }
                    else
                    {
                        sBlockMap = string.Format("{0},{1}", sBlockMap, binstr);
                    }
                }
            }
            sMap += sBlockMap;
            return sMap;
        }

        private String GetBoardMap(String BID)
        {
            string smap;
            if (SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD))
            {
                smap = "0,0,0,0,0,0,0,0," +
                            "0,0,0,0,0,0,0,0," +
                            "0,0,0,0,0,0,0,0," +
                            "0,0,0,0,0,0,0,0," +
                            "0,0,0,0,0,0,0,0," +
                            "0,0,0,0,1,0,0,0," +
                            "0,0,0,0,0,0,0,0," +
                            "0,0,0,0,0,0,0,1,";
            }
            else
            {
                smap = "1,1,1,1,1,1,1,1," +
                            "1,1,1,1,1,1,1,1," +
                            "1,1,1,1,1,1,1,1," +
                            "1,1,1,1,1,1,1,1," +
                            "1,1,1,1,1,1,1,1," +
                            "1,1,1,1,1,1,1,1," +
                            "1,1,1,1,1,1,1,1," +
                            "1,1,1,1,1,1,1,1,";
            }
            return smap;
        }
        
        private BinDefine GetBinDefine(string bin)
        {
            switch (bin)
            {
                case "2": { return BinDefine.Bin2; }
                case "3": { return BinDefine.Bin3; }
                case "4": { return BinDefine.Bin4; }
                case "5": { return BinDefine.Bin5; }
                case "6": { return BinDefine.Bin6; }
                case "7": { return BinDefine.Bin7; }
                case "8": { return BinDefine.Bin8; }
                case "9": { return BinDefine.Bin9; }
                case "10": { return BinDefine.Bin10; }
                case "11": { return BinDefine.Bin11; }
                case "12": { return BinDefine.Bin12; }
                case "13": { return BinDefine.Bin13; }
                case "14": { return BinDefine.Bin14; }
                case "15": { return BinDefine.Bin15; }
                case "16": { return BinDefine.Bin16; }
                case "17": { return BinDefine.Bin17; }
                case "18": { return BinDefine.Bin18; }
                case "19": { return BinDefine.Bin19; }
                case "20": { return BinDefine.Bin20; }
                case "21": { return BinDefine.Bin21; }
                case "22": { return BinDefine.Bin22; }
                case "23": { return BinDefine.Bin23; }
                case "24": { return BinDefine.Bin24; }
                case "25": { return BinDefine.Bin25; }
                case "26": { return BinDefine.Bin26; }
                case "27": { return BinDefine.Bin27; }
                case "28": { return BinDefine.Bin28; }
                case "29": { return BinDefine.Bin29; }
                case "30": { return BinDefine.Bin30; }
                case "31": { return BinDefine.Bin31; }
                case "32": { return BinDefine.Bin32; }
            }
            return BinDefine.Bin2;
        }

        #endregion

        #region Flow
        #region Home Flow
        private FlowChart.FCRESULT FC_Initial_Run()
        {
            bool OpenLotHome = !string.IsNullOrEmpty(SYSPara.NewLotInfo.LotNumber);
            if (!SYSPara.ProductionLinkMode.Equals(LinkMode.OFFLINE) && OpenLotHome)
            {
                GlobalDefine.RunFile(GlobalDefine.BoardDataLinkPath, "");
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart327_Run()
        {
            bool OpenLotHome = !string.IsNullOrEmpty(SYSPara.NewLotInfo.LotNumber);
            if (SYSPara.ProductionLinkMode.Equals(LinkMode.Saunders) && OpenLotHome)
            {
                //BoardDataLink 
                bool b1 = FormSet.mMainFlow.CreatBoardDataLinkerInfo();
                if (!b1)
                {
                    SYSPara.Alarm.Say("w1001");
                    return FlowChart.FCRESULT.IDLE;
                }
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart328_Run()
        {
            bool OpenLotHome = !string.IsNullOrEmpty(SYSPara.NewLotInfo.LotNumber);
            if (SYSPara.ProductionLinkMode.Equals(LinkMode.Saunders) && OpenLotHome)
            {
                //BoardDataLink 

                int iErrorCode = 0;
                string sErrorMsg = "";

                CommandReturn CRR = CommandReturn.CR_Waitting;
                CRR = BDSMC.SetLotData("LotStartSetLotData", lotinfo_sm, 20000);
                if (!CRR.Equals(CommandReturn.CR_Waitting))
                {
                    BDSMC.ReleaseCommandProcess("LotStartSetLotData");

                    if (CRR.Equals(CommandReturn.CR_Error))
                    {
                        BDSMC.GetErrorMessage(ref iErrorCode, ref sErrorMsg);
                        int Alarm = (iErrorCode + 1400);
                        SYSPara.ShowAlarm("E", (int)Alarm, "DataLink", sErrorMsg);
                    }
                    else
                    {
                        return FlowChart.FCRESULT.NEXT;
                    }
                }
                return FlowChart.FCRESULT.IDLE;
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart336_Run()
        {
            bool OpenLotHome = !string.IsNullOrEmpty(SYSPara.NewLotInfo.LotNumber);
            if (SYSPara.ProductionLinkMode.Equals(LinkMode.Saunders) && OpenLotHome)
            {
                //BoardDataLink 

                int iErrorCode = 0;
                string sErrorMsg = "";

                CommandReturn CRR = CommandReturn.CR_Waitting;
                CRR = BDSMC.StartLot("LotStartProcess");
                if (!CRR.Equals(CommandReturn.CR_Waitting))
                {
                    BDSMC.ReleaseCommandProcess("LotStartProcess");

                    if (CRR.Equals(CommandReturn.CR_Error))
                    {
                        BDSMC.GetErrorMessage(ref iErrorCode, ref sErrorMsg);
                        int Alarm = (iErrorCode + 1400);
                        SYSPara.ShowAlarm("E", (int)Alarm, "DataLink", sErrorMsg);
                    }
                    else
                    {
                        return FlowChart.FCRESULT.NEXT;
                    }
                }
                return FlowChart.FCRESULT.IDLE;
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart2_Run()
        {
            SYSPara.CallProc(ModuleName_CHM, "SetCanRunHome");
            SYSPara.CallProc(ModuleName_BFM, "SetCanRunHome");
            SYSPara.CallProc(ModuleName_HDT, "SetCanRunHome");
            SYSPara.CallProc(ModuleName_HDT_TR, "SetCanRunHome");

            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart3_Run()
        {
            ThreeValued T1 = ThreeValued.TRUE;//(ThreeValued)SYSPara.CallProc(ModuleName_CHM, "IsAxisZHomeOK");
            ThreeValued T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BFM, "IsAxisZHomeOK", BowlFeederID.BF_A);
            ThreeValued T3 = (ThreeValued)SYSPara.CallProc(ModuleName_BFM, "IsAxisZHomeOK", BowlFeederID.BF_B);
            ThreeValued T4 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "IsAxisZHomeOK");
            ThreeValued T5 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT_TR, "IsAxisZHomeOK");

            if (T1.Equals(ThreeValued.TRUE) &&
                T2.Equals(ThreeValued.TRUE)&&
                T3.Equals(ThreeValued.TRUE) &&
                T4.Equals(ThreeValued.TRUE) &&
                T5.Equals(ThreeValued.TRUE))
            {
                SYSPara.CallProc(ModuleName_TRM, "SetCanRunHome");
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart4_Run()
        {
            //ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "IsTRMSafety");
            bool T1 = (bool)SYSPara.CallProc(ModuleName_TRM, "IsTRMSafety");

            //if (T1.Equals(ThreeValued.TRUE))
            if (T1.Equals(true))
            {
                SYSPara.CallProc(ModuleName_CHM, "SetRobotSafety");
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart5_Run()
        {
            SYSPara.CallProc(ModuleName_KSM, "SetCanRunHome");
            SYSPara.CallProc(ModuleName_BSM, "SetCanRunHome");
            SYSPara.CallProc(ModuleName_RTR, "SetCanRunHome");
            SYSPara.CallProc(ModuleName_LTR, "SetCanRunHome");

            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart6_Run()
        {
            bool b1 = (!mBFM.GetUseModule() || mBFM.mHomeOk) ;
            bool b2 = (!mBSM.GetUseModule() || mBSM.mHomeOk);
            bool b3 = (!mCHM.GetUseModule() || mCHM.mHomeOk);
            bool b4 = (!mTRM.GetUseModule() || mTRM.mHomeOk);
            bool b5 = (!mHDT.GetUseModule() || mHDT.mHomeOk);
            bool b6 = (!mKSM.GetUseModule() || mKSM.mHomeOk);
            bool b7 = (!mHDT_TR.GetUseModule() || mHDT_TR.mHomeOk);
            bool b8 = (!mLTR.GetUseModule() || mLTR.mHomeOk);
            bool b9 = (!mRTR.GetUseModule() || mRTR.mHomeOk);

            if (b1 && b2 && b3 && b4 && b5 && b6 && b7 && b8 && b9)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart323_Run()
        {
            string Msg = SYSPara.PReadValue("VisionJobName").ToString() + "," +
                               BoardInfo.YN.ToString() + "," +
                               BoardInfo.YN.ToString() + "," +
                               BoardInfo.YN.ToString() + "," +
                               BoardInfo.YN.ToString() + "," +
                /*BoardInfo.YN.ToString() +*/ "1," +
                /*BoardInfo.XN.ToString() + */"1," +
                /*BBoardInfo.YP.ToString() + */"1," +
                /*BBoardInfo.XP.ToString() + */"1," +
                               BoardInfo.DeviceWidth.ToString() + "," +
                               BoardInfo.DeviceLength.ToString() + "," +
                               BoardInfo.DeviceHeight.ToString();
            SYSPara.CallProc(ModuleName_BFM, "AddNewJob", Msg);
            TM_Homing.Restart();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart324_Run()
        {
            int iRet = (int)SYSPara.CallProc(ModuleName_BFM, "AddNewJobResult");
            if (iRet.Equals(1))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            else if (iRet.Equals(0))
            {
                return FlowChart.FCRESULT.CASE1;
            }
            if (TM_Homing.On(5000))
            {
                //SYSPara.ShowAlarm("E", 902);
                SYSPara.ShowAlarm("E", (int)AlarmCode.Err_AddNewJobFail);
                TM_Homing.Restart();
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart325_Run()
        {
            string Msg = SYSPara.PReadValue("VisionJobName").ToString();
            SYSPara.CallProc(ModuleName_BFM, "ChangeJob", Msg);
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart326_Run()
        {
            int iRet = (int)SYSPara.CallProc(ModuleName_BFM, "ChangeJobResult");
            if (iRet.Equals(1))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            else if (iRet.Equals(0))
            {
                return FlowChart.FCRESULT.CASE1;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart104_Run()
        {
            mHomeOk = true;
            return FlowChart.FCRESULT.IDLE;
        }

        #endregion 

        #region TRM Flow
        private FlowChart.FCRESULT FC_TransferRobot_Run()
        {
            SYSPara.CallProc(ModuleName_TRM, "SetTransferState", TRMState.NONE);
            SYSPara.CallProc(ModuleName_BSM, "SetBoardStageState", BIBStageID.BIBStageA, BIBStageSate.IDLE);
            SYSPara.CallProc(ModuleName_BSM, "SetBoardStageState", BIBStageID.BIBStageB, BIBStageSate.IDLE);
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart9_Run()
        {
            //結批
            if (SYSPara.Lotend)
            {
                if (/*mBSM.GetLotEndOk() && */Flag_RightBSM_LotEnd.IsDone() && Flag_LeftBSM_LotEnd.IsDone())
                {
                    mTRM.SetCanLotEnd();
                    Flag_TRM_LotEnd.DoIt();
                    return FlowChart.FCRESULT.CASE2;
                }
                return FlowChart.FCRESULT.CASE1;
            }
            //判斷TRM狀態是否可工作
            TRMState state = (TRMState)SYSPara.CallProc(ModuleName_TRM, "GetTransferState");
            if (state.Equals(TRMState.WORKING)) //單執行續不太可能出現這個狀況，但還是判斷
            {
                return FlowChart.FCRESULT.CASE1;
            }

            //v2.0.0.1 先判斷是否需要Scan Rack
            if (Flag_Load_ScanAction.IsDone() == false)
            {
                if (Flag_Load_ScanAction.IsDoing() == false)
                {
                    Flag_Load_ScanAction.DoIt();
                }
                return FlowChart.FCRESULT.IDLE;
            }
            

            //判斷是否有Stage需要取板
            BIBStageID Stage = BIBStageID.NONE;
            Stage = (BIBStageID)SYSPara.CallProc(ModuleName_BSM, "IsBIBStageReady", BIBStageSate.IDLE);

            if (Stage.Equals(BIBStageID.BIBStageA) || Stage.Equals(BIBStageID.BIBStageB))
            {
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetBoardStageState", (BIBStageID)Stage, BIBStageSate.TRANSFER);
                if (T.Equals(ThreeValued.TRUE))
                {
                    TRM_WorkStage = Stage;
                    SYSPara.CallProc(ModuleName_TRM, "SetTransferState", TRMState.WORKING);
                    return FlowChart.FCRESULT.NEXT;
                }
            }

            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart10_Run()
        {
            //結批
            if (SYSPara.Lotend)
            {
                if (/*mBSM.GetLotEndOk() && */Flag_RightBSM_LotEnd.IsDone() && Flag_LeftBSM_LotEnd.IsDone())
                {
                    mTRM.SetCanLotEnd();
                    Flag_TRM_LotEnd.DoIt();
                    return FlowChart.FCRESULT.CASE2;
                }
            }
            //判斷TRM狀態是否可工作
            TRMState state = (TRMState)SYSPara.CallProc(ModuleName_TRM, "GetTransferState");
            if (state.Equals(TRMState.WORKING)) //單執行續不太可能出現這個狀況，但還是判斷
            {
                return FlowChart.FCRESULT.CASE1;
            }

            //v2.0.0.1 先判斷是否需要Scan Rack
            if (Flag_Load_ScanAction.IsDone() == false)
            {
                if (Flag_Load_ScanAction.IsDoing() == false)
                {
                    Flag_Load_ScanAction.DoIt();
                }
                return FlowChart.FCRESULT.IDLE;
            }

            //判斷是否有Stage需要退板
            BIBStageID Stage = BIBStageID.NONE;
            Stage = (BIBStageID)SYSPara.CallProc(ModuleName_BSM, "IsBIBStageReady", BIBStageSate.TRANSFER);

            if (Stage.Equals(BIBStageID.BIBStageA) || Stage.Equals(BIBStageID.BIBStageB))
            {
               // ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetBoardStageState", (BIBStageID)Stage, BIBStageSate.TRANSFER);
                //if (T.Equals(ThreeValued.TRUE))
                {
                    TRM_WorkStage = Stage;
                    SYSPara.CallProc(ModuleName_TRM, "SetTransferState", TRMState.WORKING);
                    return FlowChart.FCRESULT.NEXT;
                }
            }            

            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart339_Run()
        {
            //ThreeValued T = ThreeValued.UNKNOWN;
            //BasePosInfo Pos = new BasePosInfo();

            //if (TRM_WorkStage == BIBStageID.BIBStageA)
            //    T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.NONE, BIBStageID.BIBStageA, ACTIONMODE.UNLOCK, Pos);
            //else if (TRM_WorkStage == BIBStageID.BIBStageB)
            //    T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.NONE, BIBStageID.BIBStageB, ACTIONMODE.UNLOCK, Pos);
            BasePosInfo Pos = new BasePosInfo();
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.NONE, TRM_WorkStage, ACTIONMODE.UNLOCK, Pos);

            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart338_Run()
        {
            //ThreeValued T = ThreeValued.UNKNOWN;

            //if (TRM_WorkStage == BIBStageID.BIBStageA)
            //    T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageA);
            //else if (TRM_WorkStage == BIBStageID.BIBStageB)
            //    T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageB);

            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", TRM_WorkStage);

            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }
        private FlowChart.FCRESULT flowChart8_Run()
        {
            //TRM_至LOAD 取板
            if (GetDoItCount(CassetteID.LEFT) > 0)
            {
                WorkID = CassetteID.LEFT;
                WorkingBoard = GetFirstToDoBoard(CassetteID.LEFT);
                UpdateSlotsState(WorkingBoard.CID, WorkingBoard.Slot, BoardState.Processing);
                WorkingBoard.NowStation = TRMStation.LOAD_A;
            }
            else if (GetDoItCount(CassetteID.RIGHT) > 0)
            {
                WorkID = CassetteID.RIGHT;
                WorkingBoard = GetFirstToDoBoard(CassetteID.RIGHT);
                UpdateSlotsState(WorkingBoard.CID, WorkingBoard.Slot, BoardState.Processing);
                WorkingBoard.NowStation = TRMStation.LOAD_B;
            }
            else
            {
                WorkID = CassetteID.NONE;
            }
            //工作區改Rack不再使用Sensor判斷改用Cassette State
            //WorkID = (CassetteID)SYSPara.CallProc(ModuleName_CSM, "GetActionCassette", true);
            TRMStation WorkStation = TRMStation.NONE;
            if (WorkID.Equals(CassetteID.NONE))
            {
                ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetBoardStageState", TRM_WorkStage, BIBStageSate.IDLE);
                if (T1.Equals(ThreeValued.TRUE))
                {
                    TRM_WorkStage = BIBStageID.NONE;
                    SYSPara.CallProc(ModuleName_TRM, "SetTransferState", TRMState.IDLE);
                    return FlowChart.FCRESULT.CASE1;
                }
                return FlowChart.FCRESULT.IDLE;
            }
            else if (WorkID.Equals(CassetteID.LEFT)) WorkStation = TRMStation.LOAD_A;
            else WorkStation = TRMStation.LOAD_B;

            //ThreeValued T = (ThreeValued) SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", TRMStation.LOAD_B, ACTIONMODE.MOVE);
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", WorkStation, ACTIONMODE.MOVE, 0);
            if (T.Equals(ThreeValued.TRUE))
            {
                WorkingBoard.State.Doing();
                SYSPara.CallProc(ModuleName_BSM, "LockBIBStage", (BIBStageModuleOwner)BIBStageModuleOwner.TRANSFER, (BIBStageID)TRM_WorkStage);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart139_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart12_Run()
        {
            //ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_CSM, "CheckHaveBoard", CassetteID.RIGHT);
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_CSM, "CheckHaveBoard", WorkID);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            else if (T.Equals(ThreeValued.FALSE))
            {
                //TODO:Need Alarm
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart13_Run()
        {
            //TRM_取板
            if (Flag_SuppleDeviceSafe) return FlowChart.FCRESULT.IDLE;

            //ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", TRMStation.LOAD_B, ACTIONMODE.GET);
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", WorkingBoard.NowStation, ACTIONMODE.GET);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart152_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (T.Equals(ThreeValued.TRUE))
            {
                //Change Data 
                TrayDataEx sourceTray = (WorkingBoard.NowStation == TRMStation.LOAD_A) ? TDEx_Load : TDEx_Unload;

                if (WorkingBoard.NowStation == TRMStation.LOAD_A || WorkingBoard.NowStation == TRMStation.LOAD_B)
                {
                    sourceTray.SetBin(0, 0, 0, WorkingBoard.Slot, (byte)BinDefine.Empty);
                }
                TDEx_TRM.BinReplace((byte)BinDefine.Empty, (byte)BinDefine.Bin1);
                WorkingBoard.NowStation = TRMStation.SELF;
                
                //if (WorkID.Equals(CassetteID.RIGHT)) TDEx_Load.BinReplace((byte)BinDefine.Bin1, (byte)BinDefine.Empty);
                //else if (WorkID.Equals(CassetteID.LEFT)) TDEx_Unload.BinReplace((byte)BinDefine.Bin1, (byte)BinDefine.Empty);
                //TDEx_TRM.BinReplace((byte)BinDefine.Empty, (byte)BinDefine.Bin1);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart14_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", TRMStation.READBOARCODE, ACTIONMODE.MOVE);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart161_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart272_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", TRMStation.READBOARCODE, ACTIONMODE.READ);
            if (T.Equals(ThreeValued.TRUE))
            {
                SYSPara.CurrentBIBID = "";
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart271_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (T.Equals(ThreeValued.TRUE))
            {
                SYSPara.CurrentBIBID = (string)SYSPara.CallProc(ModuleName_TRM, "GetBarcodeValue");
                BoardSeq++;
                WorkingBoard.BID = SYSPara.CurrentBIBID;
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }
         
        private FlowChart.FCRESULT flowChart15_Run()
        {
            //TODO: Need check barcode reader GlobalDefine.OEE.NewCarrier(BoardID, BoardMap);
            
            string BarcodeID = string.Format("TEST{0:0000}", BoardSeq);

            if (SYSPara.CurrentBIBID == "" && SYSPara.OReadValue("UseBoardIDKeyin").ToBoolean())
            {
                if (SYSPara.SysState == StateMode.SM_ALARM)
                {
                    SYSPara.ErrorStop = false;
                    SYSPara.SysState = StateMode.SM_PAUSE;
                }
                SYSPara.MusicOn = false;
                FormSet.mMSS.M_Stop();

                SYSPara.SysRun = false;
                using (BoardIDForm BoardID = new BoardIDForm())
                {
                    DialogResult ret = BoardID.ShowDialog();
                }
            }

            if (SYSPara.CurrentBIBID == "")
            {
                if (SYSPara.OReadValue("NoUseBoardID").ToBoolean())
                {
                    SYSPara.CurrentBIBID = BarcodeID;
                }
                else
                {
                    if (TRM_WorkStage == BIBStageID.BIBStageA)
                        SYSPara.CallProc(ModuleName_BSM, "UnlockBIBStage", (BIBStageModuleOwner)BIBStageModuleOwner.TRANSFER, (BIBStageID)BIBStageID.BIBStageA);
                    else if (TRM_WorkStage == BIBStageID.BIBStageB)
                        SYSPara.CallProc(ModuleName_BSM, "UnlockBIBStage", (BIBStageModuleOwner)BIBStageModuleOwner.TRANSFER, (BIBStageID)BIBStageID.BIBStageB);
                    SYSPara.CallProc(ModuleName_BSM, "SetBoardStageState", TRM_WorkStage, BIBStageSate.IDLE);
                    return FlowChart.FCRESULT.CASE1;    //退板
                }
            }

            //讀取板資料
            string strMap = "";
            string BoardLotNo = "";
            int iErrorCode = 0;
            string sErrorMsg = "";

            CommandReturn CRR = CommandReturn.CR_Waitting;

            if (SYSPara.SystemProductionMode.HasFlag(PRODUCTION_MODE.UNLOAD))
            {
                CRR = BDSMC.BeforeRemove("GetBoardMap", SYSPara.CurrentBIBID, ref strMap, ref BoardLotNo, 60000);
            }
            else
            {
                CRR = BDSMC.BeforeInsert("GetBoardMap", SYSPara.CurrentBIBID, ref strMap, ref BoardLotNo, 60000);
            }

            if (!CRR.Equals(CommandReturn.CR_Waitting))
            {
                BDSMC.ReleaseCommandProcess("GetBoardMap");
                if (CRR.Equals(CommandReturn.CR_Error))
                {
                    BDSMC.GetErrorMessage(ref iErrorCode, ref sErrorMsg);
                    int Alarm = (iErrorCode + 1400);
                    SYSPara.ShowAlarm("E", (int)Alarm, "DataLink", sErrorMsg);

                    if (TRM_WorkStage == BIBStageID.BIBStageA)
                        SYSPara.CallProc(ModuleName_BSM, "UnlockBIBStage", (BIBStageModuleOwner)BIBStageModuleOwner.TRANSFER, (BIBStageID)BIBStageID.BIBStageA);
                    else if (TRM_WorkStage == BIBStageID.BIBStageB)
                        SYSPara.CallProc(ModuleName_BSM, "UnlockBIBStage", (BIBStageModuleOwner)BIBStageModuleOwner.TRANSFER, (BIBStageID)BIBStageID.BIBStageB);

                    SYSPara.CallProc(ModuleName_BSM, "SetBoardStageState", TRM_WorkStage, BIBStageSate.IDLE);
                    return FlowChart.FCRESULT.CASE1;    //退板
                }
            }
            else
            {
                return FlowChart.FCRESULT.IDLE;
            }

            strMap = strMap.Replace(';', ',').Trim();
            //int count = strMap.LastIndexOf(',');
            //strMap = strMap.Substring(0, count);
            strMap = strMap.TrimEnd(',');
            //string[] aMap = strMap.Split(',').ToArray();
            //strMap = "";
            //for (int y = 0; y < BoardInfo.YN; y++)
            //{
            //    for (int x = 0; x < BoardInfo.XN; x++)
            //    {
            //        strMap += aMap[x * BoardInfo.YN + y] + ",";
            //    }
            //}

            if (SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD))
            {
                strMap = strMap.Replace("1", "0").Trim();
            }
            //v2.0.0.1
            WorkingBoard.BeforeMap = strMap;

            SYSPara.CurrentBoard.SetBoardInfo(SYSPara.CurrentBIBID, "", strMap);
            switch (TRM_WorkStage)
            {
                case BIBStageID.BIBStageA:
                    {
                        SYSPara.StageA_BIBID = SYSPara.CurrentBIBID;
                        SYSPara.StageA_Map = strMap;
                        SYSPara.CurrentBoard.StrConvertToDiePakMap(TDEx_Left_Board);

                        BoardMapInfo BoardMap = new BoardMapInfo(BoardInfo.BLK_XN, BoardInfo.BLK_YN, BoardInfo.XN, BoardInfo.YN, SYSPara.CurrentBoard.MAP);

                        Board_BKS.CreateBoardBookingList(SYSPara.SystemProductionMode, BoardMap, BIBStageID.BIBStageA);

                        if (SYSPara.CurrentBoard.StrConvertToDiePakMap(TDEx_Left_Board))
                        {
                            GlobalDefine.OEE.NewCarrier(SYSPara.StageA_BIBID, SYSPara.StageA_Map);
                            Flag_Booking_A.DoIt();
                            return FlowChart.FCRESULT.NEXT;
                        }
                    }
                    break;
                case BIBStageID.BIBStageB:
                    {
                        SYSPara.StageB_BIBID = SYSPara.CurrentBIBID;
                        SYSPara.StageB_Map = strMap;
                        SYSPara.CurrentBoard.StrConvertToDiePakMap(TDEx_Right_Board);

                        BoardMapInfo BoardMap = new BoardMapInfo(BoardInfo.BLK_XN, BoardInfo.BLK_YN, BoardInfo.XN, BoardInfo.YN, SYSPara.CurrentBoard.MAP);

                        Board_BKS.CreateBoardBookingList(SYSPara.SystemProductionMode, BoardMap, BIBStageID.BIBStageB);

                        if (SYSPara.CurrentBoard.StrConvertToDiePakMap(TDEx_Right_Board))
                        {
                            GlobalDefine.OEE.NewCarrier(SYSPara.StageB_BIBID, SYSPara.StageB_Map);
                            Flag_Booking_B.DoIt();
                            return FlowChart.FCRESULT.NEXT;
                        }
                    }
                    break;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart16_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", TRMStation.STAGE_CH, ACTIONMODE.MOVE, 0);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart162_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart17_Run()
        {
            bool  b1 = (bool)SYSPara.CallProc(ModuleName_CHM, "CheckHaveBoard", false);
            if (b1.Equals(true))
            {
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", TRMStation.STAGE_CH, ACTIONMODE.PUT, 0);
                if (T.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else
            {
                SYSPara.ShowAlarm("E", (int)AlarmCode.Err_ClamShellHasBoard);
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart163_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (T.Equals(ThreeValued.TRUE))
            {
                //v2.0.0.1 change data
                WorkingBoard.NowStation = TRMStation.STAGE_CH;
                //if (WorkID.Equals(CassetteID.RIGHT)) TDEx_Load.BinReplace((byte)BinDefine.Bin1, (byte)BinDefine.Empty);
                //else if (WorkID.Equals(CassetteID.LEFT)) TDEx_Unload.BinReplace((byte)BinDefine.Bin1, (byte)BinDefine.Empty);
                TDEx_TRM.BinReplace((byte)BinDefine.Bin1, (byte)BinDefine.Empty);
                TDEx_CHM.BinReplace((byte)BinDefine.Empty, (byte)BinDefine.Bin1);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart18_Run()
        {
            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", TRMStation.WAITING, ACTIONMODE.MOVE, 0);
            if (T1.Equals(ThreeValued.TRUE))
            {
                Flag_CHM_Action.Reset();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart164_Run()
        {
            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            
            if (T1.Equals(ThreeValued.TRUE))
            {
                CHM_ACTION = ACTIONMODE.OPEN;
                Flag_CHM_Action.DoIt();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart19_Run()
        {
            if (Flag_CHM_Action.IsDone())
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart20_Run()
        {
            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", TRMStation.STAGE_CH, ACTIONMODE.MOVE, 0);
            //ThreeValued T2 = (ThreeValued)SYSPara.CallProc(ModuleName_CHM, "SetActionCommand_CHM", ACTIONMODE.UNLOCK);
            //ThreeValued T2 = ThreeValued.TRUE;
            //if (T1.Equals(ThreeValued.TRUE) && T2.Equals(ThreeValued.TRUE))
            if (T1.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart165_Run()
        {
            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            //ThreeValued T2 = (ThreeValued)SYSPara.CallProc(ModuleName_CHM, "GetActionResult_CHM");
            //if (T1.Equals(ThreeValued.TRUE) && T2.Equals(ThreeValued.TRUE))
            if (T1.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart21_Run()
        {
            bool b1 = (bool)SYSPara.CallProc(ModuleName_CHM, "CheckHaveBoard", true);
            if (b1)
            {
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", TRMStation.STAGE_CH, ACTIONMODE.GET, 0);
                if (T.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else
            {
                SYSPara.ShowAlarm("E", (int)AlarmCode.Err_ClamShellHasNoBoard);
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart166_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (T.Equals(ThreeValued.TRUE))
            {
                WorkingBoard.NowStation = TRMStation.SELF;
                TDEx_CHM.BinReplace((byte)BinDefine.Bin1, (byte)BinDefine.Empty);
                TDEx_TRM.BinReplace((byte)BinDefine.Empty, (byte)BinDefine.Bin1);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart22_Run()
        {
            TRMStation station = TRMStation.NONE;
            if (TRM_WorkStage == BIBStageID.BIBStageA) station = TRMStation.STAGE_A;
            else if (TRM_WorkStage == BIBStageID.BIBStageB) station = TRMStation.STAGE_B;

            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", (TRMStation)station, ACTIONMODE.MOVE, 0);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart167_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart23_Run()
        {
            TRMStation station = TRMStation.NONE;
            if (TRM_WorkStage == BIBStageID.BIBStageA) station = TRMStation.STAGE_A;
            else if (TRM_WorkStage == BIBStageID.BIBStageB) station = TRMStation.STAGE_B;

            bool b1 = (bool)SYSPara.CallProc(ModuleName_BSM, "CheckHaveBoard", TRM_WorkStage, false);
            if (b1)
            {
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", (TRMStation)station, ACTIONMODE.PUT, 0);
                if (T.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else
            { 
                int _ErrCode = TRM_WorkStage == BIBStageID.BIBStageA ? (int)AlarmCode.Err_StageA_HasBoard : (int)AlarmCode.Err_StageB_HasBoard;
                if (TRM_WorkStage == BIBStageID.BIBStageA || TRM_WorkStage == BIBStageID.BIBStageB)
                {
                    SYSPara.ShowAlarm("E", _ErrCode);
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart168_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (T.Equals(ThreeValued.TRUE))
            {
                TDEx_TRM.BinReplace((byte)BinDefine.Bin1, (byte)BinDefine.Empty);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart335_Run()
        {
            //ThreeValued T = ThreeValued.UNKNOWN;
            //BasePosInfo Pos = new BasePosInfo();

            //if (TRM_WorkStage == BIBStageID.BIBStageA)
            //    T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.TRANSFER, BIBStageID.BIBStageA, ACTIONMODE.LOCK, Pos);
            //else if (TRM_WorkStage == BIBStageID.BIBStageB)
            //    T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.TRANSFER, BIBStageID.BIBStageB, ACTIONMODE.LOCK, Pos);
            BasePosInfo Pos = new BasePosInfo();
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.TRANSFER, TRM_WorkStage, ACTIONMODE.LOCK, Pos);

            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart337_Run()
        {
            //ThreeValued T = ThreeValued.UNKNOWN;

            //if (TRM_WorkStage == BIBStageID.BIBStageA)
            //    T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageA);
            //else if (TRM_WorkStage == BIBStageID.BIBStageB)
            //    T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageB);

            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", TRM_WorkStage);

            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart11_Run()
        {
            TRMStation station = TRMStation.NONE;
            if (TRM_WorkStage == BIBStageID.BIBStageA) station = TRMStation.STAGE_A;
            else if (TRM_WorkStage == BIBStageID.BIBStageB) station = TRMStation.STAGE_B;

            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", (TRMStation)station, ACTIONMODE.MOVE, 0);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart169_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart31_Run()
        {
            TRMStation station = TRMStation.NONE;
            if (TRM_WorkStage == BIBStageID.BIBStageA) station = TRMStation.STAGE_A;
            else if (TRM_WorkStage == BIBStageID.BIBStageB) station = TRMStation.STAGE_B;

            bool b1 = (bool)SYSPara.CallProc(ModuleName_BSM, "CheckHaveBoard", TRM_WorkStage, true);
            if (b1)
            {
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", (TRMStation)station, ACTIONMODE.GET, 0);
                if (T.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else
            { 
                int _ErrCode = TRM_WorkStage == BIBStageID.BIBStageA ? (int)AlarmCode.Err_StageA_HasNoBoard : (int)AlarmCode.Err_StageB_HasNoBoard;
                SYSPara.ShowAlarm("E", _ErrCode);
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart170_Run()
        {
            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (T1.Equals(ThreeValued.TRUE))
            {
                ThreeValued T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetBoardStageState", (BIBStageID)TRM_WorkStage, BIBStageSate.IDLE);
                if (T2.Equals(ThreeValued.TRUE))
                {
                    if (TRM_WorkStage.Equals(BIBStageID.BIBStageA))
                    {
                        SYSPara.StageA_BIBID = "";
                        TDEx_Left_Board.ResetBin(GlobalDefine.EmptyBin);
                        WorkingBoard = StageA_Board.Clone();
                        StageA_Board.Reset();
                    }
                    else if (TRM_WorkStage.Equals(BIBStageID.BIBStageB))
                    {
                        SYSPara.StageB_BIBID = "";
                        TDEx_Right_Board.ResetBin(GlobalDefine.EmptyBin);
                        WorkingBoard = StageB_Board.Clone();
                        StageB_Board.Reset();
                    }
                    WorkingBoard.NowStation = TRMStation.SELF;
                    TDEx_TRM.ResetBin(GlobalDefine.PassBin);
                    
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart30_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", TRMStation.STAGE_CH, ACTIONMODE.MOVE, 0);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart171_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }
        
        private FlowChart.FCRESULT flowChart29_Run()
        {
            bool b1 = (bool)SYSPara.CallProc(ModuleName_CHM, "CheckHaveBoard", false);
            if (b1)
            {
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", TRMStation.STAGE_CH, ACTIONMODE.PUT, 0);
                if (T.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else
            {
                SYSPara.ShowAlarm("E", (int)AlarmCode.Err_ClamShellHasBoard);
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart172_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (T.Equals(ThreeValued.TRUE))
            {
                WorkingBoard.NowStation = TRMStation.STAGE_CH;
                TDEx_TRM.ResetBin(GlobalDefine.EmptyBin);
                TDEx_CHM.ResetBin(GlobalDefine.PassBin);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart28_Run()
        {
            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", TRMStation.WAITING, ACTIONMODE.MOVE, 0);
            if (T1.Equals(ThreeValued.TRUE))
            {
                Flag_CHM_Action.Reset();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart173_Run()
        {
            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (T1.Equals(ThreeValued.TRUE))
            {
                CHM_ACTION = ACTIONMODE.CLOSE;
                Flag_CHM_Action.DoIt();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart27_Run()
        {
            if (Flag_CHM_Action.IsDone())
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart26_Run()
        {
            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", TRMStation.STAGE_CH, ACTIONMODE.MOVE, 0);
            if (T1.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart174_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart25_Run()
        {
            bool b1 = (bool)SYSPara.CallProc(ModuleName_CHM, "CheckHaveBoard", true);
            if (b1)
            {
                ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", TRMStation.STAGE_CH, ACTIONMODE.GET, 0);
                if (T1.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else
            {
                SYSPara.ShowAlarm("E", (int)AlarmCode.Err_ClamShellHasNoBoard);
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart175_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (T.Equals(ThreeValued.TRUE))
            {
                WorkingBoard.NowStation = TRMStation.SELF;
                TDEx_CHM.ResetBin(GlobalDefine.EmptyBin);
                TDEx_TRM.ResetBin(GlobalDefine.PassBin);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart24_Run()
        {
            WorkID = WorkingBoard.CID;
            //WorkID = (CassetteID)SYSPara.CallProc(ModuleName_CSM, "GetActionCassette", false);
            TRMStation WorkStation = TRMStation.NONE;
            if (WorkID.Equals(CassetteID.NONE))
            {
                //SYSPara.ShowAlarm("E", 801, "UNLOAD");
                //SYSPara.ShowAlarm("E", (int)AlarmCode.Err_CassetteHasBoard, "UNLOAD");
                //不應該出現這個狀況，退板有板由Module Alarm
                return FlowChart.FCRESULT.IDLE;
            }
            else if (WorkID.Equals(CassetteID.LEFT)) WorkStation = TRMStation.LOAD_A;
            else WorkStation = TRMStation.LOAD_B;

            //ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", TRMStation.LOAD_A, ACTIONMODE.MOVE);
            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", WorkStation, ACTIONMODE.MOVE, 0);
            if (T1.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart176_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart32_Run()
        {
            //v2.0.0.1 有無板由TRM取放板的時候自己判斷
            return FlowChart.FCRESULT.NEXT;
            //ThreeValued T = (ThreeValued)SYSPara.CallProc("ModuleName_Rack", "RackCheckHaveBIB", "RackID.UNLOAD");
            //ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_CSM, "CheckHaveBoard", WorkID);
            //if (T.Equals(ThreeValued.FALSE))
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //else if (T.Equals(ThreeValued.TRUE))
            //{
            //    //TODO:Need Alarm
            //}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart33_Run()
        {
            if (Flag_SuppleDeviceSafe) return FlowChart.FCRESULT.IDLE;
            //WorkID = (CassetteID)SYSPara.CallProc(ModuleName_CSM, "GetActionCassette", false);
            WorkID = WorkingBoard.CID;
            TRMStation WorkStation = TRMStation.NONE;
            if (WorkID.Equals(CassetteID.NONE)) return FlowChart.FCRESULT.IDLE;
            else if (WorkID.Equals(CassetteID.LEFT)) WorkStation = TRMStation.LOAD_A;
            else WorkStation = TRMStation.LOAD_B;
            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", WorkStation, ACTIONMODE.PUT, 0);
            if (T1.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart177_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (T.Equals(ThreeValued.TRUE))
            {
                //TODO:v2.0.0.1
                TDEx_TRM.ResetBin(GlobalDefine.EmptyBin);
                if (WorkingBoard.CID == CassetteID.LEFT)
                {
                    TDEx_Load.SetBin(0, 0, 0, WorkingBoard.Slot, (byte)BinDefine.Tested);
                }
                else if (WorkingBoard.CID == CassetteID.RIGHT)
                {
                    TDEx_Unload.SetBin(0, 0, 0, WorkingBoard.Slot, (byte)BinDefine.Tested);
                }
                //TDEx_TRM.ResetBin(GlobalDefine.EmptyBin);
                //if (WorkID.Equals(CassetteID.LEFT)) TDEx_Unload.ResetBin(GlobalDefine.PassBin);
                //else if (WorkID.Equals(CassetteID.RIGHT)) TDEx_Load.ResetBin(GlobalDefine.PassBin);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart36_Run()
        {
            //if (mTRM.mLotendOk || !mTRM.GetUseModule() && Flag_TRM_LotEnd.IsDoIt())
            //{
            //    Flag_CHM_LotEnd.DoIt();
            //    return FlowChart.FCRESULT.NEXT;
            //}
            if (Flag_TRM_LotEnd.IsDoIt() || !mTRM.GetUseModule())
            {
                Flag_CHM_LotEnd.DoIt();
                Flag_TRM_LotEnd.Doing();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart37_Run()
        {
            Flag_TRM_LotEnd.Done();
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart116_Run()
        {
            //if (TRM_WorkStage == BIBStageID.BIBStageA)
            //    SYSPara.CallProc(ModuleName_BSM, "UnlockBIBStage", (BIBStageModuleOwner)BIBStageModuleOwner.TRANSFER, (BIBStageID)BIBStageID.BIBStageA);
            //else if (TRM_WorkStage == BIBStageID.BIBStageB)
            //    SYSPara.CallProc(ModuleName_BSM, "UnlockBIBStage", (BIBStageModuleOwner)BIBStageModuleOwner.TRANSFER, (BIBStageID)BIBStageID.BIBStageB);
            SYSPara.CallProc(ModuleName_BSM, "UnlockBIBStage", BIBStageModuleOwner.TRANSFER, TRM_WorkStage);
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetBoardStageState", TRM_WorkStage, BIBStageSate.READY);
            if (T.Equals(ThreeValued.TRUE))
            {
                if (TRM_WorkStage == BIBStageID.BIBStageA)
                {
                    StageA_Board = WorkingBoard.Clone();
                    StageA_Board.NowStation = TRMStation.STAGE_A;
                }
                else if (TRM_WorkStage == BIBStageID.BIBStageB)
                {
                    StageB_Board = WorkingBoard.Clone();
                    StageB_Board.NowStation = TRMStation.STAGE_B;
                }
                WorkingBoard.Reset();
                TRM_WorkStage = BIBStageID.NONE;
                SYSPara.CallProc(ModuleName_TRM, "SetTransferState", TRMState.IDLE);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart120_Run()
        {
            SetBoardWorkDone(WorkingBoard);
            WorkingBoard.Reset();
            TRM_WorkStage = BIBStageID.NONE;
            SYSPara.CallProc(ModuleName_TRM, "SetTransferState", TRMState.IDLE);
            
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion

        #region CHM FLOW
        private FlowChart.FCRESULT FC_Clamshell_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart35_Run()
        {
            if (SYSPara.Lotend)
            if (Flag_CHM_LotEnd.IsDoIt())
            {
                mCHM.SetCanLotEnd();
                Flag_CHM_LotEnd.Doing();
                return FlowChart.FCRESULT.CASE1;
            }

            if (Flag_CHM_Action.IsDoIt())
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart40_Run()
        {
            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_CHM, "SetActionCommand_CHM", ACTIONMODE.LOCK);
            if (T1.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart41_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_CHM, "GetActionResult_CHM");
            if (T.Equals(ThreeValued.TRUE))
            {
                if (CHM_ACTION.Equals(ACTIONMODE.OPEN))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
                else
                {
                    return FlowChart.FCRESULT.CASE1;
                }
                
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart47_Run()
        {
            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_CHM, "SetActionCommand_CHM", ACTIONMODE.OPEN);
            if (T1.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart42_Run()
        {
            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_CHM, "SetActionCommand_CHM", ACTIONMODE.CLOSE);
            if (T1.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart43_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_CHM, "GetActionResult_CHM");
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart46_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_CHM, "GetActionResult_CHM");
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE; 
        }

        private FlowChart.FCRESULT flowChart44_Run()
        {
            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_CHM, "SetActionCommand_CHM", ACTIONMODE.UNLOCK);
            if (T1.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart48_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_CHM, "GetActionResult_CHM");
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE; 
        }

        private FlowChart.FCRESULT flowChart45_Run()
        {
            Flag_CHM_Action.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart34_Run()
        {
            //if (mCHM.mLotendOk || !mCHM.GetUseModule())
            if (Flag_CHM_LotEnd.IsDoing() || !mCHM.GetUseModule())
            {
                Flag_CHM_LotEnd.Done();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart39_Run()
        {
            return FlowChart.FCRESULT.IDLE;
        }
        #endregion CHM FLOW

        #region BIBStage FLOW
        private FlowChart.FCRESULT FC_BoardStageA_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart50_Run()
        {
            BIBStageSate StageState = (BIBStageSate)SYSPara.CallProc(ModuleName_BSM, "GetBoardStageState", BIBStageID.BIBStageA);
            if (StageState.Equals(BIBStageSate.READY))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart57_Run()
        {
            if (SYSPara.Lotend)
            {
                if ((SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD) && Flag_TopHDT_LotEnd.IsDone() && Flag_BottomHDT_LotEnd.IsDone()) ||
                    (SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.UNLOAD)))
                {
                    BIBStageSate StageState = (BIBStageSate)SYSPara.CallProc(ModuleName_BSM, "GetBoardStageState", BIBStageID.BIBStageA);
                    if (!StageState.Equals(BIBStageSate.WORKING))
                    {
                        if (StageState.Equals(BIBStageSate.READY))
                        {
                            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetBoardStageState", BIBStageID.BIBStageA, BIBStageSate.TRANSFER);
                        }
                        if (StageState.Equals(BIBStageSate.IDLE))
                        {
                            Flag_LeftBSM_LotEnd.DoIt();
                            return FlowChart.FCRESULT.CASE1;
                        }
                        return FlowChart.FCRESULT.IDLE;
                    }
                    //if (!StageState.Equals(BIBStageSate.IDLE) && !StageState.Equals(BIBStageSate.WORKING))
                    //if (!StageState.Equals(BIBStageSate.TRANSFER) && !StageState.Equals(BIBStageSate.WORKING))
                    //{
                    //    ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetBoardStageState", BIBStageID.BIBStageA, BIBStageSate.TRANSFER);
                    //    if (T.Equals(ThreeValued.TRUE))
                    //    {
                    //        Flag_RightBSM_LotEnd.DoIt();
                    //        return FlowChart.FCRESULT.CASE1;
                    //    }
                    //    return FlowChart.FCRESULT.IDLE;
                    //}
                }
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart51_Run()
        {
            return FlowChart.FCRESULT.NEXT;
            //BasePosInfo Pos = new BasePosInfo();
            //ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.NONE, BIBStageID.BIBStageA, ACTIONMODE.LOCK, Pos);
            //if (T.Equals(ThreeValued.TRUE))
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart178_Run()
        {
            return FlowChart.FCRESULT.NEXT;
            //ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageA);
            //if (T.Equals(ThreeValued.TRUE))
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart52_Run()
        {
            //GetBoardMap(SYSPara.StageA_BIBID);
            //SYSPara.SystemProductionMode = PRODUCTION_MODE.LOAD;
            //string strMap = GetBoardMap(SYSPara.StageA_BIBID);

            //string strMap = "";
            //string BoardLotNo = "";
            //int iErrorCode = 0;
            //string sErrorMsg = "";

            //CommandReturn CRR = CommandReturn.CR_Waitting;

            //if (SYSPara.SystemProductionMode.HasFlag(PRODUCTION_MODE.UNLOAD))
            //{
            //    CRR = BDSMC.BeforeRemove("GetBoardMap", SYSPara.StageA_BIBID, ref strMap, ref BoardLotNo, 60000);
            //}
            //else
            //{
            //    CRR = BDSMC.BeforeInsert("GetBoardMap", SYSPara.StageA_BIBID, ref strMap, ref BoardLotNo, 60000);
            //}

            //if (!CRR.Equals(CommandReturn.CR_Waitting))
            //{
            //    BDSMC.ReleaseCommandProcess("GetBoardMap");
            //    if (CRR.Equals(CommandReturn.CR_Error))
            //    {
            //        BDSMC.GetErrorMessage(ref iErrorCode, ref sErrorMsg);
            //        int Alarm = (iErrorCode + 1400);
            //        SYSPara.ShowAlarm("E", (int)Alarm, "DataLink", sErrorMsg);

            //        return FlowChart.FCRESULT.CASE1;    //退板
            //    }
            //}
            //else
            //{
            //    return FlowChart.FCRESULT.IDLE;
            //}

            //strMap = strMap.Replace(';', ',').Trim();
            //int count = strMap.LastIndexOf(',');
            //strMap = strMap.Substring(0, count);

            //if (SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD))
            //{
            //    strMap = strMap.Replace("1", "0").Trim();
            //}

            //SYSPara.StageA_BIBID = SYSPara.CurrentBIBID;
            //SYSPara.StageA_Map = strMap;

            //SYSPara.CurrentBoard.SetBoardInfo(SYSPara.StageA_BIBID, "", SYSPara.StageA_Map);
            //SYSPara.CurrentBoard.StrConvertToDiePakMap(TDEx_Left_Board);

            //GlobalDefine.OEE.NewCarrier(SYSPara.StageA_BIBID, SYSPara.StageA_Map);

            return FlowChart.FCRESULT.NEXT;
        }

        int BoardCount = 0;
        private FlowChart.FCRESULT flowChart53_Run()
        {
            return FlowChart.FCRESULT.NEXT;
            //if (!Flag_Booking.IsDoing() && !Flag_Booking.IsDoIt() && !Flag_Booking_B_Work) 
            //if (!Flag_Booking_A.IsDoing() && !Flag_Booking_A.IsDoIt())
            //{
            //    Flag_Booking_A_Work = true;
            //    BoardMapInfo BoardMap = new BoardMapInfo(BoardInfo.BLK_XN, BoardInfo.BLK_YN, BoardInfo.XN, BoardInfo.YN, SYSPara.CurrentBoard.MAP);

            //    Board_BKS.CreateBoardBookingList(SYSPara.SystemProductionMode, BoardMap, BIBStageID.BIBStageA);

            //    if (SYSPara.CurrentBoard.StrConvertToDiePakMap(TDEx_Left_Board))
            //    {
            //        //Flag_Booking.DoIt();
            //        Flag_Booking_A.DoIt();
            //        //HDT_BIB_BIBStageID = BIBStageID.BIBStageA;
            //        return FlowChart.FCRESULT.NEXT;
            //    }
            //}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart54_Run()
        {
            //if (Flag_Booking.IsDone())
            if (Flag_Booking_A.IsDone())
            {
                BoardCount++;
                Flag_Booking_A_Work = false;

                return FlowChart.FCRESULT.NEXT;


                int iErrorCode = 0;
                string sErrorMsg = "";

                string strMap = GetBoardMap(TDEx_Left_Board);
                string sBID = SYSPara.StageA_BIBID;
                StageA_Board.AfterMap = strMap;
                CommandReturn CRR = CommandReturn.CR_Waitting;
                if (string.IsNullOrEmpty(strMap))
                {
                    return FlowChart.FCRESULT.NEXT;
                }

                if (SYSPara.SystemProductionMode.HasFlag(PRODUCTION_MODE.LOAD))
                {
                    //AfterInsert                    
                    CRR = BDSMC.AfterInsert("SetFinishBoard", sBID, ref strMap);
                }
                else
                {
                    //AfterRemove
                    CRR = BDSMC.AfterRemove("SetFinishBoard", sBID, ref strMap);
                }

                if (!CRR.Equals(CommandReturn.CR_Waitting))
                {
                    BDSMC.ReleaseCommandProcess("SetFinishBoard");

                    //SaveBIBMap(CurrentBoardID, strMap);

                    if (CRR.Equals(CommandReturn.CR_Error))
                    {
                        BDSMC.GetErrorMessage(ref iErrorCode, ref sErrorMsg);
                        int Alarm = (iErrorCode + 1400);
                        SYSPara.ShowAlarm("E", (int)Alarm, "DataLink", sErrorMsg);
                    }
                    else
                    {
                        return FlowChart.FCRESULT.NEXT;
                    }
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart55_Run()
        {
            return FlowChart.FCRESULT.NEXT;
            //BasePosInfo Pos = new BasePosInfo();
            //ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.NONE, BIBStageID.BIBStageA, ACTIONMODE.UNLOCK, Pos);
            //if (T.Equals(ThreeValued.TRUE))
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart179_Run()
        {
            return FlowChart.FCRESULT.NEXT;
            //ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageA);
            //if (T.Equals(ThreeValued.TRUE))
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart56_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetBoardStageState", BIBStageID.BIBStageA, BIBStageSate.TRANSFER);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart58_Run()
        {
            if (Flag_LeftBSM_LotEnd.IsDoIt() && Flag_RightBSM_LotEnd.IsDoIt())
            {
                mBSM.SetCanLotEnd();

                int iErrorCode = 0;
                string sErrorMsg = "";
                CommandReturn CRR = CommandReturn.CR_Waitting;
                CRR = BDSMC.StartLot("LotEndProcess");
                if (!CRR.Equals(CommandReturn.CR_Waitting))
                {
                    BDSMC.ReleaseCommandProcess("LotEndProcess");

                    if (CRR.Equals(CommandReturn.CR_Error))
                    {
                        BDSMC.GetErrorMessage(ref iErrorCode, ref sErrorMsg);
                        int Alarm = (iErrorCode + 1400);
                        SYSPara.ShowAlarm("E", (int)Alarm, "DataLink", sErrorMsg);
                    }
                    else
                    {
                        return FlowChart.FCRESULT.NEXT;
                    }
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart180_Run()
        {
            //if (!mBSM.GetUseModule())
            {
                Flag_RightBSM_LotEnd.Done();
                Flag_LeftBSM_LotEnd.Done();
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_BoardStageB_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart67_Run()
        {
            BIBStageSate StageState = (BIBStageSate)SYSPara.CallProc(ModuleName_BSM, "GetBoardStageState", BIBStageID.BIBStageB);
            if (StageState.Equals(BIBStageSate.READY))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart66_Run()
        {
            return FlowChart.FCRESULT.NEXT;
            //BasePosInfo Pos = new BasePosInfo();
            //ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.NONE, BIBStageID.BIBStageB, ACTIONMODE.LOCK, Pos);
            //if (T.Equals(ThreeValued.TRUE))
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart182_Run()
        {
            return FlowChart.FCRESULT.NEXT;
            //ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageB);
            //if (T.Equals(ThreeValued.TRUE))
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart65_Run()
        {
            //GetBoardMap(SYSPara.StageB_BIBID);
            //SYSPara.SystemProductionMode = PRODUCTION_MODE.LOAD;
            //string strMap = GetBoardMap(SYSPara.StageB_BIBID);
            //string strMap = "";
            //string BoardLotNo = "";
            //int iErrorCode = 0;
            //string sErrorMsg = "";
            ////string strMap = GetBoardMap(SYSPara.StageA_BIBID);

            //CommandReturn CRR = CommandReturn.CR_Waitting;

            //if (SYSPara.SystemProductionMode.HasFlag(PRODUCTION_MODE.UNLOAD))
            //{
            //    CRR = BDSMC.BeforeRemove("GetBoardMap", SYSPara.StageB_BIBID, ref strMap, ref BoardLotNo, 60000);
            //}
            //else
            //{
            //    CRR = BDSMC.BeforeInsert("GetBoardMap", SYSPara.StageB_BIBID, ref strMap, ref BoardLotNo, 60000);
            //}

            //if (!CRR.Equals(CommandReturn.CR_Waitting))
            //{
            //    BDSMC.ReleaseCommandProcess("GetBoardMap");
            //    if (CRR.Equals(CommandReturn.CR_Error))
            //    {
            //        BDSMC.GetErrorMessage(ref iErrorCode, ref sErrorMsg);
            //        int Alarm = (iErrorCode + 1400);
            //        SYSPara.ShowAlarm("E", (int)Alarm, "DataLink", sErrorMsg);

            //        return FlowChart.FCRESULT.CASE1;    //退板
            //    }
            //}
            //else
            //{
            //    return FlowChart.FCRESULT.IDLE;
            //}

            //strMap = strMap.Replace(';', ',').Trim();
            //int count = strMap.LastIndexOf(',');
            //strMap = strMap.Substring(0, count);

            //if (SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD))
            //{
            //    strMap = strMap.Replace("1", "0").Trim();
            //}

            //SYSPara.StageB_BIBID = SYSPara.CurrentBIBID;
            //SYSPara.StageB_Map = strMap;

            //SYSPara.CurrentBoard.SetBoardInfo(SYSPara.StageB_BIBID, "", strMap);
            //SYSPara.CurrentBoard.StrConvertToDiePakMap(TDEx_Right_Board);

            //GlobalDefine.OEE.NewCarrier(SYSPara.StageB_BIBID, SYSPara.StageB_Map);
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart64_Run()
        {
            return FlowChart.FCRESULT.NEXT;
            //if (!Flag_Booking.IsDoing() && !Flag_Booking.IsDoIt() && !Flag_Booking_A_Work) 
            //if (!Flag_Booking_B.IsDoing() && !Flag_Booking_B.IsDoIt())
            //{
            //    Flag_Booking_B_Work = true;
            //    //Flag_Booking.DoIt();
            //    BoardMapInfo BoardMap = new BoardMapInfo(BoardInfo.BLK_XN, BoardInfo.BLK_YN, BoardInfo.XN, BoardInfo.YN, SYSPara.CurrentBoard.MAP);

            //    Board_BKS.CreateBoardBookingList(SYSPara.SystemProductionMode, BoardMap, BIBStageID.BIBStageB);

            //    if (SYSPara.CurrentBoard.StrConvertToDiePakMap(TDEx_Right_Board))
            //    {
            //        //Flag_Booking.DoIt();
            //        Flag_Booking_B.DoIt();
            //        //HDT_BIB_BIBStageID = BIBStageID.BIBStageB;
            //        return FlowChart.FCRESULT.NEXT;
            //    }
            //}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart63_Run()
        {
            if (Flag_Booking_B.IsDone())
            {
                BoardCount++;
                Flag_Booking_B_Work = false;

                return FlowChart.FCRESULT.NEXT;

                int iErrorCode = 0;
                string sErrorMsg = "";

                 string strMap = GetBoardMap(TDEx_Right_Board);
                string sBID = SYSPara.StageB_BIBID;
                StageB_Board.AfterMap = strMap;
                CommandReturn CRR = CommandReturn.CR_Waitting;
                if (string.IsNullOrEmpty(strMap))
                {
                    return FlowChart.FCRESULT.NEXT;
                }

                if (SYSPara.SystemProductionMode.HasFlag(PRODUCTION_MODE.LOAD))
                {
                    //AfterInsert                    
                    CRR = BDSMC.AfterInsert("SetFinishBoard", sBID, ref strMap);
                }
                else
                {
                    //AfterRemove
                    CRR = BDSMC.AfterRemove("SetFinishBoard", sBID, ref strMap);
                }

                if (!CRR.Equals(CommandReturn.CR_Waitting))
                {
                    BDSMC.ReleaseCommandProcess("SetFinishBoard");

                    //SaveBIBMap(CurrentBoardID, strMap);

                    if (CRR.Equals(CommandReturn.CR_Error))
                    {
                        BDSMC.GetErrorMessage(ref iErrorCode, ref sErrorMsg);
                        int Alarm = (iErrorCode + 1400);
                        SYSPara.ShowAlarm("E", (int)Alarm, "DataLink", sErrorMsg);
                    }
                    else
                    {
                        return FlowChart.FCRESULT.NEXT;
                    }
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart62_Run()
        {
            return FlowChart.FCRESULT.NEXT;
            //BasePosInfo Pos = new BasePosInfo();
            //ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.NONE, BIBStageID.BIBStageB, ACTIONMODE.UNLOCK, Pos);
            //if (T.Equals(ThreeValued.TRUE))
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart181_Run()
        {
            return FlowChart.FCRESULT.NEXT;
            //ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageB);
            //if (T.Equals(ThreeValued.TRUE))
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart61_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetBoardStageState", BIBStageID.BIBStageB, BIBStageSate.TRANSFER);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart60_Run()
        {
            if (SYSPara.Lotend)
            {
                if ((SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD) && Flag_TopHDT_LotEnd.IsDone() && Flag_BottomHDT_LotEnd.IsDone()) ||
                    (SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.UNLOAD)))
                {
                    BIBStageSate StageState = (BIBStageSate)SYSPara.CallProc(ModuleName_BSM, "GetBoardStageState", BIBStageID.BIBStageB);
                    if (!StageState.Equals(BIBStageSate.WORKING))
                    {
                        if (StageState.Equals(BIBStageSate.READY))
                        {
                            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetBoardStageState", BIBStageID.BIBStageB, BIBStageSate.TRANSFER);
                        }
                        if (StageState.Equals(BIBStageSate.IDLE))
                        {
                            Flag_RightBSM_LotEnd.DoIt();
                            return FlowChart.FCRESULT.CASE1;
                        }
                        return FlowChart.FCRESULT.IDLE;
                    }
                    //if (!StageState.Equals(BIBStageSate.TRANSFER) && !StageState.Equals(BIBStageSate.WORKING))
                    //{
                    //    ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetBoardStageState", BIBStageID.BIBStageB, BIBStageSate.TRANSFER);
                    //    if (T.Equals(ThreeValued.TRUE))
                    //    {
                    //        Flag_LeftBSM_LotEnd.DoIt();
                    //        return FlowChart.FCRESULT.CASE1;
                    //    }
                    //    return FlowChart.FCRESULT.IDLE;
                    //}
                }
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart59_Run()
        {
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_Booking_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart71_Run()
        {
            //if (Flag_Booking.IsDoIt())
            if (Flag_Booking_A.IsDoIt() || Flag_Booking_B.IsDoIt())
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart79_Run()
        {
            //Flag_Booking.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart80_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart73_Run()
        {
            if (SYSPara.Lotend)
            {
                bool b1 = TDEx_Left_KitShuttle.IsEmpty(GlobalDefine.PassBin);
                bool b2 = TDEx_Right_KitShuttle.IsEmpty(GlobalDefine.PassBin);
                
                if (b1 && b2)
                    Board_BKS.CancelBoardBookingList();
            }

            if (Flag_Booking_A.IsDoIt())
            {
                if (HDT_BIB_BIBStageID.Equals(BIBStageID.NONE))
                {
                    HDT_BIB_BIBStageID = BIBStageID.BIBStageA;
                }
                if (Board_BKS.BoardBookingListCount(BIBStageID.BIBStageA).Equals(0))
                {
                    Flag_Booking_A.Done();
                    HDT_BIB_BIBStageID = BIBStageID.NONE;
                }
            }

            if (Flag_Booking_B.IsDoIt())
            {
                if (HDT_BIB_BIBStageID.Equals(BIBStageID.NONE))
                {
                    HDT_BIB_BIBStageID = BIBStageID.BIBStageB;
                }

                if (Board_BKS.BoardBookingListCount(BIBStageID.BIBStageB).Equals(0))
                {
                    Flag_Booking_B.Done();
                    HDT_BIB_BIBStageID = BIBStageID.NONE;
                }
            }
            //if (Flag_Booking_A.IsDoIt())
            //{
            //    if (Board_BKS.BoardBookingListCount(BIBStageID.BIBStageA).Equals(0))
            //    {
            //        HDT_BIB_BIBStageID = BIBStageID.NONE;
            //        Flag_Booking_A.Done();
            //    }
            //    else
            //    {
            //        if (HDT_BIB_BIBStageID.Equals(BIBStageID.NONE))
            //            HDT_BIB_BIBStageID = BIBStageID.BIBStageA;
            //    }
            //}
            //if (Flag_Booking_B.IsDoIt())
            //{
            //    if (Board_BKS.BoardBookingListCount(BIBStageID.BIBStageB).Equals(0))
            //    {
            //        HDT_BIB_BIBStageID = BIBStageID.NONE;
            //        Flag_Booking_B.Done();
            //    }
            //    else
            //    {
            //        if (HDT_BIB_BIBStageID.Equals(BIBStageID.NONE))
            //            HDT_BIB_BIBStageID = BIBStageID.BIBStageB;
            //    }
            //}
            if (Board_BKS.BoardBookingListCount().Equals(0))
            {
                //TODO:回寫上板MAP
                return FlowChart.FCRESULT.CASE1;
            }

            if (HDT_BIB_BIBStageID.Equals(BIBStageID.NONE))
                return FlowChart.FCRESULT.IDLE;

            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart74_Run()
        {
            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "IsHeadUse", BoardHeadID.HDT_A);
            ThreeValued T2 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "IsHeadUse", BoardHeadID.HDT_B);
            if (T1.Equals(ThreeValued.TRUE))
                Flag_HDT_BIB_A_Action.DoIt();
            if (T2.Equals(ThreeValued.TRUE))
                Flag_HDT_BIB_B_Action.DoIt();

            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart76_Run()
        {
            if (Flag_HDT_BIB_A_Action.IsDone() && Flag_HDT_BIB_B_Action.IsDone())
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.NEXT;
            //if (SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD))
            //{
            //    return FlowChart.FCRESULT.CASE1;
            //}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT pr_Run()
        {
            if (SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD))
            {
                return FlowChart.FCRESULT.CASE1;
            }
            return FlowChart.FCRESULT.NEXT;
            //if (Board_BKS.SetKitShuttleBookingInfo(KitShuttleID.TransferShuttleB, VehicleState.UNTESTED_EMPTY, TDEx_Right_KitShuttle)) 
            //if (!Board_BKS.SetStageBookingInfo())
            //{
            //    return FlowChart.FCRESULT.IDLE;
            //}
            //return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart77_Run()
        {
            if (SYSPara.Lotend)
            {
                return FlowChart.FCRESULT.CASE1;
            //    Board_BKS.CancelBoardBookingList();
            }
            bool b1 = LeftKitStateControl.IsStateDoIt(VehicleState.UNTESTED_EMPTY);
            bool b2 = RightKitStateControl.IsStateDoIt(VehicleState.UNTESTED_EMPTY);
            if (b1)
            {
               // HDT_BIB_A_KitShuttleID = KitShuttleID.TransferShuttleA;
                HDT_BIB_Booking_KitShuttleID = KitShuttleID.TransferShuttleA;
                if (Board_BKS.SetKitShuttleBookingInfo(KitShuttleID.TransferShuttleA, VehicleState.UNTESTED_EMPTY, TDEx_Left_KitShuttle))
                    return FlowChart.FCRESULT.NEXT;
            }
            else if (b2)
            {
                //HDT_BIB_A_KitShuttleID = KitShuttleID.TransferShuttleB;
                HDT_BIB_Booking_KitShuttleID = KitShuttleID.TransferShuttleB;
                if (Board_BKS.SetKitShuttleBookingInfo(KitShuttleID.TransferShuttleB, VehicleState.UNTESTED_EMPTY, TDEx_Right_KitShuttle)) 
                    return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.CASE1;

            //HDT_BIB_Booking_KitShuttleID = (KitShuttleID)SYSPara.CallProc(ModuleName_KSM, "IsTransferShuttleReady", (VehicleState)VehicleState.UNTESTED_EMPTY);

            //if (!HDT_BIB_Booking_KitShuttleID.Equals(KitShuttleID.NONE))
            //{ 
            //    //Set Kit Shuttle Bin Data
            //    TrayDataEx td_KitShuttle = null;
            //    switch (HDT_BIB_Booking_KitShuttleID)
            //    {
            //        case KitShuttleID.TransferShuttleA:
            //            {
            //                td_KitShuttle = TDEx_Right_KitShuttle;
            //            }
            //            break;
            //        case KitShuttleID.TransferShuttleB:
            //            {
            //                td_KitShuttle = TDEx_Left_KitShuttle;
            //            }
            //            break;
            //    }

            //    if (Board_BKS.SetKitShuttleBookingInfo(HDT_BIB_A_KitShuttleID, VehicleState.UNTESTED_EMPTY, td_KitShuttle))
            //    {
            //        return FlowChart.FCRESULT.NEXT;
            //    }

            //    //能否結批
            //}
            //return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart78_Run()
        {
            //通知換台車
            //ThreeValued T1 = ThreeValued.UNKNOWN;
            bool b1 = false;
            bool b2 = false;
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "IsTransferShuttleLocked", (KitShuttleID)HDT_BIB_Booking_KitShuttleID);
            if (T.Equals(ThreeValued.FALSE))
            {
                //ThreeValued SetTransferShuttleState(KitShuttleID id, TransferShuttleState state);
                //T1 = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "SetTransferShuttleState", (KitShuttleID)HDT_BIB_Booking_KitShuttleID, (VehicleState)VehicleState.UNTESTED_EMPTY_BOOKED);
                if (HDT_BIB_Booking_KitShuttleID.Equals(KitShuttleID.TransferShuttleA))
                {
                    b1 = LeftKitStateControl.StateDone(VehicleState.UNTESTED_EMPTY);
                }
                else if (HDT_BIB_Booking_KitShuttleID.Equals(KitShuttleID.TransferShuttleB))
                {
                    b2 = RightKitStateControl.StateDone(VehicleState.UNTESTED_EMPTY);
                }
            }
           
            //if (T1.Equals(ThreeValued.TRUE))
            if (b1 || b2)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart376_Run()
        {
            if (SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.UNLOAD))
            {
                return FlowChart.FCRESULT.CASE1;
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart378_Run()
        {
            if (SYSPara.Lotend)
            {
                return FlowChart.FCRESULT.CASE1;
                //    Board_BKS.CancelBoardBookingList();
            }
            bool b1 = LeftKitStateControl.IsStateDoIt(VehicleState.TESTED_EMPTY);
            bool b2 = RightKitStateControl.IsStateDoIt(VehicleState.TESTED_EMPTY);
            if (b1)
            {
                // HDT_BIB_A_KitShuttleID = KitShuttleID.TransferShuttleA;
                HDT_BIB_Booking_KitShuttleID = KitShuttleID.TransferShuttleA;
                if (Board_BKS.SetKitShuttleBookingInfo(KitShuttleID.TransferShuttleA, VehicleState.TESTED_EMPTY, TDEx_Left_KitShuttle))
                    return FlowChart.FCRESULT.NEXT;
            }
            else if (b2)
            {
                //HDT_BIB_A_KitShuttleID = KitShuttleID.TransferShuttleB;
                HDT_BIB_Booking_KitShuttleID = KitShuttleID.TransferShuttleB;
                if (Board_BKS.SetKitShuttleBookingInfo(KitShuttleID.TransferShuttleB, VehicleState.TESTED_EMPTY, TDEx_Right_KitShuttle))
                    return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart377_Run()
        {
            bool b1 = false;
            bool b2 = false;
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "IsTransferShuttleLocked", (KitShuttleID)HDT_BIB_Booking_KitShuttleID);
            if (T.Equals(ThreeValued.FALSE))
            {
                if (HDT_BIB_Booking_KitShuttleID.Equals(KitShuttleID.TransferShuttleA))
                {
                    b1 = LeftKitStateControl.StateDone(VehicleState.TESTED_EMPTY);
                }
                else if (HDT_BIB_Booking_KitShuttleID.Equals(KitShuttleID.TransferShuttleB))
                {
                    b2 = RightKitStateControl.StateDone(VehicleState.TESTED_EMPTY);
                }
            }

            if (b1 || b2)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart70_Run()
        {
            if (SYSPara.Lotend)
            {
                return FlowChart.FCRESULT.CASE1;
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart72_Run()
        {
            return FlowChart.FCRESULT.IDLE;
        }
        
        
        #region Board Head A
        private FlowChart.FCRESULT FC_BoardHeadA_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart83_Run()
        {
            if (Flag_HDT_BIB_A_Action.IsDoIt())
            {
                Flag_HDT_BIB_A_Action.Doing();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart86_Run()
        {
            if (SYSPara.SystemProductionMode.HasFlag(PRODUCTION_MODE.LOAD))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart89_Run()
        {
            Flag_HDT_BIB_A_Action.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart84_Run()
        {
            //if (SYSPara.Lotend)// && mBSM.GetLotEndOk())
            if (SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.UNLOAD))
            {
                if (SYSPara.Lotend && Flag_LeftBSM_LotEnd.IsDone() && Flag_RightBSM_LotEnd.IsDone())
                {
                    Flag_TopHDT_LotEnd.DoIt();
                    return FlowChart.FCRESULT.CASE1;
                }
            }
            else
            {
                if (SYSPara.Lotend && Flag_TopBowlFeeder_LotEnd.IsDone() && Flag_BottomBowlFeeder_LotEnd.IsDone())
                {
                    if (Board_BKS.BoardBookingListCount() <= 0)
                    {
                        Flag_TopHDT_LotEnd.DoIt();
                        return FlowChart.FCRESULT.CASE1;
                    }
                }
            }

            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart85_Run()
        {
            if (Flag_TopHDT_LotEnd.IsDoIt() && Flag_BottomHDT_LotEnd.IsDoIt())
            {
                mHDT.SetCanLotEnd();
                Flag_TopHDT_LotEnd.Doing();
                Flag_BottomHDT_LotEnd.Doing();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart267_Run()
        {
            //if (mHDT.mLotendOk)
            {
                Flag_BottomHDT_LotEnd.Done();
                Flag_TopHDT_LotEnd.Done();
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart184_Run()
        {
            bool b1 = LeftKitStateControl.IsStateDoIt(VehicleState.UNTESTED_FULL);
            bool b2 = RightKitStateControl.IsStateDoIt(VehicleState.UNTESTED_FULL);
            if (b1)
            {
                HDT_BIB_A_KitShuttleID = KitShuttleID.TransferShuttleA;
                return FlowChart.FCRESULT.NEXT;
            }
            else if (b2)
            {
                HDT_BIB_A_KitShuttleID = KitShuttleID.TransferShuttleB;
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart185_Run()
        {
            PnPStation WorkStage = PnPStation.BOARD_A;
            TrayDataEx TDEx_Temp = TDEx_Left_Board;
            if (HDT_BIB_BIBStageID.Equals(BIBStageID.BIBStageB))
            {
                WorkStage = PnPStation.BOARD_B;
                TDEx_Temp = TDEx_Right_Board;
            }
            switch (HDT_BIB_A_KitShuttleID)
            {
                case KitShuttleID.TransferShuttleA:
                    {
                        HDT_BIB_A_PnPInfo = new PnPInfo(TDEx_HDT_BIB_A, PnPStation.KIT_SHUTTLE_A, TDEx_Left_KitShuttle, GlobalDefine.PassBin, (PnPStation)WorkStage, TDEx_Temp, GlobalDefine.EmptyBin, 1, 1, VehicleState.UNTESTED_FULL);
                    }
                    break;
                case KitShuttleID.TransferShuttleB:
                    {
                        HDT_BIB_A_PnPInfo = new PnPInfo(TDEx_HDT_BIB_A, PnPStation.KIT_SHUTTLE_B, TDEx_Right_KitShuttle, GlobalDefine.PassBin, (PnPStation)WorkStage, TDEx_Temp, GlobalDefine.EmptyBin, 1, 1, VehicleState.UNTESTED_FULL);
                    }
                    break;

                default:
                    {
                        //alarm
                        return FlowChart.FCRESULT.IDLE;
                    }
            }

            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart186_Run()
        {
            Flag_HDT_BIB_A_PnP.DoIt();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart187_Run()
        {
            if (Flag_HDT_BIB_A_PnP.IsDone() && Flag_HDT_BIB_B_PnP.IsDone())
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart188_Run()
        {
            HDT_BIB_A_KitShuttleID = KitShuttleID.NONE;
            bool b1 = true;
            switch (HDT_BIB_A_PnPInfo.SourceStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        if (LeftKitStateControl.IsStateDoIt(VehicleState.UNTESTED_FULL))
                            b1 = LeftKitStateControl.StateDone(LeftKitStateControl.State);

                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        if (RightKitStateControl.IsStateDoIt(VehicleState.UNTESTED_FULL))
                            b1 = RightKitStateControl.StateDone(RightKitStateControl.State);
                    }
                    break;
            }

            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart190_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart195_Run()
        {
            bool b1 = LeftKitStateControl.IsStateDoIt(VehicleState.TESTED_EMPTY_BOOKED);
            bool b2 = RightKitStateControl.IsStateDoIt(VehicleState.TESTED_EMPTY_BOOKED);
            if (b1)
            {
                HDT_BIB_A_KitShuttleID = KitShuttleID.TransferShuttleA;
                return FlowChart.FCRESULT.NEXT;
            }
            else if (b2)
            {
                HDT_BIB_A_KitShuttleID = KitShuttleID.TransferShuttleB;
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart191_Run()
        {
            TrayDataEx TDEx_Temp = HDT_BIB_A_KitShuttleID == KitShuttleID.TransferShuttleA ? TDEx_Left_KitShuttle : TDEx_Right_KitShuttle;

            if (HDT_BIB_A_KitShuttleID == KitShuttleID.TransferShuttleA || HDT_BIB_A_KitShuttleID == KitShuttleID.TransferShuttleB)
            {
                switch (HDT_BIB_BIBStageID)
                {
                    case BIBStageID.BIBStageA:
                        {
                            HDT_BIB_A_PnPInfo = new PnPInfo(TDEx_HDT_BIB_A, PnPStation.BOARD_A, TDEx_Left_Board, GlobalDefine.AllBin, PnPStation.PassBox, TDEx_Temp, GlobalDefine.EmptyBin, 1, 1, VehicleState.TESTED_EMPTY_BOOKED);
                        }
                        break;
                    case BIBStageID.BIBStageB:
                        {
                            HDT_BIB_A_PnPInfo = new PnPInfo(TDEx_HDT_BIB_A, PnPStation.BOARD_B, TDEx_Right_Board, GlobalDefine.AllBin, PnPStation.PassBox, TDEx_Temp, GlobalDefine.EmptyBin, 1, 1, VehicleState.TEST_EMPTY_BOOKED);
                        }
                        break;
                    default:
                        {
                            //alarm
                            return FlowChart.FCRESULT.IDLE;
                        }
                        break;
                }
            }

            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart192_Run()
        {
            Flag_HDT_BIB_A_PnP.DoIt();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart193_Run()
        {
            if (Flag_HDT_BIB_A_PnP.IsDone())
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart379_Run()
        {
            HDT_BIB_A_KitShuttleID = KitShuttleID.NONE;
            bool b1 = true;
            switch (HDT_BIB_A_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        if (LeftKitStateControl.IsStateDoIt(VehicleState.TESTED_EMPTY_BOOKED))
                            b1 = LeftKitStateControl.StateDone(LeftKitStateControl.State);

                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        if (RightKitStateControl.IsStateDoIt(VehicleState.TESTED_EMPTY_BOOKED))
                            b1 = RightKitStateControl.StateDone(RightKitStateControl.State);
                    }
                    break;
            }

            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        #endregion
        #region BoardHeadPnP
        private FlowChart.FCRESULT FC_BoardHeadAPnP_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart96_Run()
        {
            bool b1 = Flag_HDT_BIB_A_PnP.IsDoIt();
            if (b1)
            {
                Flag_HDT_BIB_A_PnP.Doing();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart97_Run()
        {
            bool bDone = HDT_BIB_A_PnPInfo.SourceIsDone;
            if (bDone)
            {
                return FlowChart.FCRESULT.CASE1;
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart98_Run()
        {
            bool bPnPFinish = HDT_BIB_A_PnPInfo.SourceIsDone;
            if (bPnPFinish)
            {
                //bool b1 = true;
                //switch (HDT_BIB_A_PnPInfo.SourceStation)
                //{
                //    case PnPStation.KIT_SHUTTLE_A:
                //        {
                //            if (LeftKitStateControl.IsStateDoIt(VehicleState.UNTESTED_FULL))
                //                b1 = LeftKitStateControl.StateDone(LeftKitStateControl.State);

                //        }
                //        break;
                //    case PnPStation.KIT_SHUTTLE_B:
                //        {
                //            if (RightKitStateControl.IsStateDoIt(VehicleState.UNTESTED_FULL))
                //                b1 = RightKitStateControl.StateDone(RightKitStateControl.State);
                //        }
                //        break;
                //}

                //if (b1)
                {
                    return FlowChart.FCRESULT.CASE1;
                }
                return FlowChart.FCRESULT.IDLE;
            }

            return FlowChart.FCRESULT.NEXT;
        }
        private FlowChart.FCRESULT flowChart268_Run()
        {
            if (Flag_Booking_A_Error || HDTA_NozzleStateIsFail)
            {
                Flag_Booking_A_Error = false;
                HDTA_NozzleStateIsFail = false;
                return FlowChart.FCRESULT.CASE1;
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart102_Run()
        {
            //HDT_BIB_A_BookingInfo = null;
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart101_Run()
        {
            //bool b1 = false;
            //switch (HDT_BIB_A_KitShuttleID)
            //{
            //    case KitShuttleID.TransferShuttleA:
            //        {
            //            b1 = LeftKitStateControl.StateDoIt(VehicleState.UNTESTED_EMPTY);

            //        }
            //        break;
            //    case KitShuttleID.TransferShuttleB:
            //        {
            //            b1 = RightKitStateControl.StateDoIt(VehicleState.UNTESTED_EMPTY);
            //        }
            //        break;
            //}
            //if (b1)
            {
                Flag_HDT_BIB_A_PnP.Done();

                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT FC_HDT_DP_PICKUP_START_Run()
        {
            //BIB_A_PnP Pickup Start
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart196_Run()
        {
            //Pick up-lock
            if (SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD))
            {
                KitShuttleID KitID = KitShuttleID.NONE;
                if (HDT_BIB_A_PnPInfo.SourceStation.Equals(PnPStation.KIT_SHUTTLE_A)) KitID = KitShuttleID.TransferShuttleA;
                if (HDT_BIB_A_PnPInfo.SourceStation.Equals(PnPStation.KIT_SHUTTLE_B)) KitID = KitShuttleID.TransferShuttleB;
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "LockTransferShuttle", (KitShuttleOwner)KitShuttleOwner.HDT_BIB_A, (KitShuttleID)KitID);
                if (T.Equals(ThreeValued.TRUE))
                {
                    bool b2 = Board_BKS.GetKitBlockInfo(KitID, HDT_BIB_A_PnPInfo.Current_VechicleState);
                    if (!b2)
                    {
                        //Error
                        Flag_Booking_A_Error = true;
                        return FlowChart.FCRESULT.CASE1;
                    }
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else
            {
                KitShuttleID KitID = KitShuttleID.NONE;
                if (HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_A)) KitID = KitShuttleID.TransferShuttleA;
                if (HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_B)) KitID = KitShuttleID.TransferShuttleB;
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "LockBIBStage", (BIBStageModuleOwner)BIBStageModuleOwner.HDT_BIB_A, (BIBStageID)HDT_BIB_BIBStageID);
                if (T.Equals(ThreeValued.TRUE))
                {
                    //bool b2 = Board_BKS.GetWorkStageBlockInfo();
                    bool b2 = Board_BKS.GetKitBlockInfo(KitID, HDT_BIB_A_PnPInfo.Current_VechicleState);
                    if (!b2)
                    {
                        //
                        Flag_Booking_A_Error = true;
                        return FlowChart.FCRESULT.CASE1;
                    }
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private BasePosInfo GetOffset(BoardHeadID id, PnPStation station, bool bPick = false)
        {
            BasePosInfo Pos = new BasePosInfo();
            switch (id)
            {
                case BoardHeadID.HDT_A:
                    {
                        switch (station)
                        {
                            case PnPStation.BOARD_A:
                                {
                                    Pos.X = SYSPara.PReadValue("Offset_HDTA_BIB_A_X").ToInt();
                                    Pos.Y = SYSPara.PReadValue("Offset_HDTA_BIB_A_Y").ToInt();
                                    Pos.Z = SYSPara.PReadValue("Offset_HDTA_BIB_A_Z_PLACE").ToInt() + BoardInfo.Height + BoardInfo.DeviceHeight;
                                    if (bPick)
                                    {
                                        //Pos.Z = SYSPara.PReadValue("Offset_HDTA_BIB_A_Z_PICKUP").ToInt() + BoardInfo.Height + BoardInfo.DeviceHeight - BoardInfo.Depth;
                                        SYSPara.CallProc(ModuleName_HDT, "SetHeadAStation", station);
                                        Pos.Z = BoardInfo.Height + BoardInfo.DeviceHeight - BoardInfo.Depth;
                                    }
                                }
                                break;
                            case PnPStation.BOARD_B:
                                {
                                    Pos.X = SYSPara.PReadValue("Offset_HDTA_BIB_B_X").ToInt();
                                    Pos.Y = SYSPara.PReadValue("Offset_HDTA_BIB_B_Y").ToInt();
                                    Pos.Z = SYSPara.PReadValue("Offset_HDTA_BIB_B_Z_PLACE").ToInt() + BoardInfo.Height + BoardInfo.DeviceHeight;
                                    if (bPick)
                                    {
                                        //Pos.Z = SYSPara.PReadValue("Offset_HDTA_BIB_B_Z_PICKUP").ToInt() + BoardInfo.Height + BoardInfo.DeviceHeight - BoardInfo.Depth;
                                        SYSPara.CallProc(ModuleName_HDT, "SetHeadAStation", station);
                                        Pos.Z = BoardInfo.Height + BoardInfo.DeviceHeight - BoardInfo.Depth;
                                    }
                                }
                                break;
                            case PnPStation.KIT_SHUTTLE_A:
                                {
                                    Pos.X = SYSPara.PReadValue("Offset_HDTA_KITA_X").ToInt();
                                    Pos.Y = SYSPara.PReadValue("Offset_HDTA_KITA_Y").ToInt();
                                    Pos.Z = SYSPara.PReadValue("Offset_HDTA_KITA_Z_PLACE").ToInt() + KitInfo.Height + KitInfo.DeviceHeight;
                                    if (bPick)
                                    {
                                        //Pos.Z = SYSPara.PReadValue("Offset_HDTA_KITA_Z_PICKUP").ToInt() + KitInfo.Height - KitInfo.Depth + KitInfo.DeviceHeight;
                                        SYSPara.CallProc(ModuleName_HDT, "SetHeadAStation", station);
                                        Pos.Z = KitInfo.Height - KitInfo.Depth + KitInfo.DeviceHeight;
                                    }
                                }
                                break;
                            case PnPStation.KIT_SHUTTLE_B:
                                {
                                    Pos.X = SYSPara.PReadValue("Offset_HDTA_KITB_X").ToInt();
                                    Pos.Y = SYSPara.PReadValue("Offset_HDTA_KITB_Y").ToInt();
                                    Pos.Z = SYSPara.PReadValue("Offset_HDTA_KITB_Z_PLACE").ToInt() + KitInfo.Height + KitInfo.DeviceHeight;
                                    if (bPick)
                                    {
                                        //Pos.Z = SYSPara.PReadValue("Offset_HDTA_KITB_Z_PICKUP").ToInt() + KitInfo.Height - KitInfo.Depth + KitInfo.DeviceHeight;
                                        SYSPara.CallProc(ModuleName_HDT, "SetHeadAStation", station);
                                        Pos.Z = KitInfo.Height - KitInfo.Depth + KitInfo.DeviceHeight;
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case BoardHeadID.HDT_B:
                    {
                        switch (station)
                        {
                            case PnPStation.BOARD_A:
                                {
                                    Pos.X = SYSPara.PReadValue("Offset_HDTB_BIB_A_X").ToInt();
                                    Pos.Y = SYSPara.PReadValue("Offset_HDTB_BIB_A_Y").ToInt();
                                    Pos.Z = SYSPara.PReadValue("Offset_HDTB_BIB_A_Z_PLACE").ToInt() + BoardInfo.Height + BoardInfo.DeviceHeight;
                                    if (bPick)
                                    {
                                        //Pos.Z = SYSPara.PReadValue("Offset_HDTB_BIB_A_Z_PICKUP").ToInt() + BoardInfo.Height + BoardInfo.DeviceHeight - BoardInfo.Depth;
                                        SYSPara.CallProc(ModuleName_HDT, "SetHeadBStation", station);
                                        Pos.Z = BoardInfo.Height + BoardInfo.DeviceHeight - BoardInfo.Depth;

                                    }
                                }
                                break;
                            case PnPStation.BOARD_B:
                                {
                                    Pos.X = SYSPara.PReadValue("Offset_HDTB_BIB_B_X").ToInt();
                                    Pos.Y = SYSPara.PReadValue("Offset_HDTB_BIB_B_Y").ToInt();
                                    Pos.Z = SYSPara.PReadValue("Offset_HDTB_BIB_B_Z_PLACE").ToInt() + BoardInfo.Height + BoardInfo.DeviceHeight;
                                    if (bPick)
                                    {
                                        //Pos.Z = SYSPara.PReadValue("Offset_HDTB_BIB_B_Z_PICKUP").ToInt() + BoardInfo.Height + BoardInfo.DeviceHeight - BoardInfo.Depth;
                                        SYSPara.CallProc(ModuleName_HDT, "SetHeadBStation", station);
                                        Pos.Z = BoardInfo.Height + BoardInfo.DeviceHeight - BoardInfo.Depth;

                                    }
                                }
                                break;
                            case PnPStation.KIT_SHUTTLE_A:
                                {
                                    Pos.X = SYSPara.PReadValue("Offset_HDTB_KITA_X").ToInt();
                                    Pos.Y = SYSPara.PReadValue("Offset_HDTB_KITA_Y").ToInt();
                                    Pos.Z = SYSPara.PReadValue("Offset_HDTB_KITA_Z_PLACE").ToInt() + KitInfo.Height + KitInfo.DeviceHeight;
                                    if (bPick)
                                    {
                                        //Pos.Z = SYSPara.PReadValue("Offset_HDTB_KITA_Z_PICKUP").ToInt() + KitInfo.Height - KitInfo.Depth + KitInfo.DeviceHeight;
                                        SYSPara.CallProc(ModuleName_HDT, "SetHeadBStation", station);
                                        Pos.Z = KitInfo.Height - KitInfo.Depth + KitInfo.DeviceHeight;
                                    }
                                }
                                break;
                            case PnPStation.KIT_SHUTTLE_B:
                                {
                                    Pos.X = SYSPara.PReadValue("Offset_HDTB_KITB_X").ToInt();
                                    Pos.Y = SYSPara.PReadValue("Offset_HDTB_KITB_Y").ToInt();
                                    Pos.Z = SYSPara.PReadValue("Offset_HDTB_KITB_Z_PLACE").ToInt() + KitInfo.Height + KitInfo.DeviceHeight;
                                    if (bPick)
                                    {
                                        //Pos.Z = SYSPara.PReadValue("Offset_HDTB_KITB_Z_PICKUP").ToInt() + KitInfo.Height - KitInfo.Depth + KitInfo.DeviceHeight - 5000;
                                        SYSPara.CallProc(ModuleName_HDT, "SetHeadBStation", station);
                                        Pos.Z = KitInfo.Height - KitInfo.Depth + KitInfo.DeviceHeight - 5000;
                                    }
                                }
                                break;
                        }
                    }
                    break;
            }
            return Pos;
        }

        private FlowChart.FCRESULT flowChart197_Run()
        {
            Point pos = new Point();
            //Point PickOffset = new Point();
            BasePosInfo Offset = new BasePosInfo();
            BasePosInfo HDT_BIB_A_Basic = new BasePosInfo();
            BasePosInfo BSMBasic = new BasePosInfo();
            BasePosInfo KSMBasic = new BasePosInfo();

            switch (HDT_BIB_A_PnPInfo.SourceStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        Offset = GetOffset(BoardHeadID.HDT_A, PnPStation.KIT_SHUTTLE_A, true);
                        pos = HDT_BIB_A_PnP.CalcKitPnPPos(HDT_BIB_A_PnPInfo.HDTTray, HDT_BIB_A_PnPInfo.SourceTray, Board_BKS.CurrentKitBlockInfo);  //治具位置
                        HDT_BIB_A_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", HDT_BIB_A_PnPInfo.SourceStation, BoardHeadID.HDT_A);
                        KSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleA);
                        HDT_BIB_A_PnPInfo.WorkingPos_X = HDT_BIB_A_Basic.X - (KitInfo.Width / 2) + KitInfo.XO + pos.X * KitInfo.XP + Offset.X;
                        HDT_BIB_A_PnPInfo.WorkingPos_Y = KSMBasic.Y - (KitInfo.Length / 2) + KitInfo.YO + pos.Y * KitInfo.YP + Offset.Y;
                        HDT_BIB_A_PnPInfo.WorkingPos_Z = HDT_BIB_A_Basic.Z + Offset.Z;

                        //HDT_BIB_A_PnPInfo.WorkingPos_X = -(int)((float)(KitInfo.XN - 1) / 2 * KitInfo.XP);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        Offset = GetOffset(BoardHeadID.HDT_A, PnPStation.KIT_SHUTTLE_B, true);
                        pos = HDT_BIB_A_PnP.CalcKitPnPPos(HDT_BIB_A_PnPInfo.HDTTray, HDT_BIB_A_PnPInfo.SourceTray, Board_BKS.CurrentKitBlockInfo);
                        HDT_BIB_A_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", HDT_BIB_A_PnPInfo.SourceStation, BoardHeadID.HDT_A);
                        KSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleB);
                        HDT_BIB_A_PnPInfo.WorkingPos_X = HDT_BIB_A_Basic.X - (KitInfo.Width / 2) + KitInfo.XO + pos.X * KitInfo.XP + Offset.X;
                        HDT_BIB_A_PnPInfo.WorkingPos_Y = KSMBasic.Y - (KitInfo.Length / 2) + KitInfo.YO + pos.Y * KitInfo.YP + Offset.Y;
                        HDT_BIB_A_PnPInfo.WorkingPos_Z = HDT_BIB_A_Basic.Z + Offset.Z;
                    }
                    break;
                case PnPStation.BOARD_A:
                    {
                        Offset = GetOffset(BoardHeadID.HDT_A, PnPStation.BOARD_A, true);
                        pos = HDT_BIB_A_PnP.CalcBoardPnPPos(HDT_BIB_A_PnPInfo.HDTTray, HDT_BIB_A_PnPInfo.SourceTray, Board_BKS.CurrentWorkInfo);
                        HDT_BIB_A_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", PnPStation.BOARD_A, BIBStageID.BIBStageA);
                        BSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_BSM, "GetBasicInfo", BIBStageModuleOwner.HDT_BIB_A, BIBStageID.BIBStageA);
                        HDT_BIB_A_PnPInfo.WorkingPos_X = (pos.X) + (HDT_BIB_A_Basic.X) + BoardInfo.XO - BoardInfo.Width + Offset.X;
                        HDT_BIB_A_PnPInfo.WorkingPos_Y = (pos.Y) + (BSMBasic.Y) + BoardInfo.YO + Offset.Y;
                        HDT_BIB_A_PnPInfo.WorkingPos_Z = HDT_BIB_A_Basic.Z + Offset.Z;
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        Offset = GetOffset(BoardHeadID.HDT_A, PnPStation.BOARD_B, true);
                        pos = HDT_BIB_A_PnP.CalcBoardPnPPos(HDT_BIB_A_PnPInfo.HDTTray, HDT_BIB_A_PnPInfo.SourceTray, Board_BKS.CurrentWorkInfo);
                        HDT_BIB_A_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", PnPStation.BOARD_B, BoardHeadID.HDT_A);
                        BSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_BSM, "GetBasicInfo", BIBStageModuleOwner.HDT_BIB_A, BIBStageID.BIBStageB);
                        HDT_BIB_A_PnPInfo.WorkingPos_X = (pos.X) + (HDT_BIB_A_Basic.X) + BoardInfo.XO - BoardInfo.Width + Offset.X;
                        HDT_BIB_A_PnPInfo.WorkingPos_Y = (pos.Y) + (BSMBasic.Y) + BoardInfo.YO + Offset.Y;
                        HDT_BIB_A_PnPInfo.WorkingPos_Z = HDT_BIB_A_Basic.Z + Offset.Z;
                    }
                    break;
                case PnPStation.PassBox:
                    {
                        HDT_BIB_A_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", PnPStation.PassBox, BoardHeadID.HDT_A);
                        HDT_BIB_A_PnPInfo.WorkingPos_X = HDT_BIB_A_Basic.X;
                        HDT_BIB_A_PnPInfo.WorkingPos_Z = HDT_BIB_A_Basic.Z;
                    }
                    break;
                case PnPStation.BinBox:
                    {
                        HDT_BIB_A_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", PnPStation.BinBox, BoardHeadID.HDT_A);
                        HDT_BIB_A_PnPInfo.WorkingPos_X = HDT_BIB_A_Basic.X;
                        HDT_BIB_A_PnPInfo.WorkingPos_Z = HDT_BIB_A_Basic.Z;
                    }
                    break;

                default:
                    {
                        return FlowChart.FCRESULT.IDLE;
                    }
                    break;
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart198_Run()
        {
            NozzleState[,] states = HDT_BIB_A_PnPInfo.GetBeforePickHeadAction();

            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "SetNozzleState", states, BoardHeadID.HDT_A);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart199_Run()
        {
            //ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", HDT_BIB_A_PnPInfo.SourceStation, ACTIONMODE.MOVE, HDT_BIB_A_PnPInfo.WorkingPos_X, 0, 0);
            //if (T.Equals(ThreeValued.TRUE))
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //return FlowChart.FCRESULT.IDLE;
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart200_Run()
        {
            //BasePosInfo Pos = new BasePosInfo();
            BasePosInfo Pos = new BasePosInfo();
            Pos.X = HDT_BIB_A_PnPInfo.WorkingPos_X;
            Pos.Y = HDT_BIB_A_PnPInfo.WorkingPos_Y;
            Pos.Z = HDT_BIB_A_PnPInfo.WorkingPos_Z;
            Pos.U = HDT_BIB_A_PnPInfo.WorkingPos_U;

            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_A, ACTIONMODE.MOVE, Pos);

            ThreeValued T = ThreeValued.UNKNOWN;
            switch (HDT_BIB_A_PnPInfo.SourceStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        //Pos = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleA);
                        //Pos.Y = Pos.Y + HDT_BIB_A_PnPInfo.WorkingPos_Y;
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "SetActionCommand_KSM", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleA, ACTIONMODE.MOVE, Pos);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        //Pos = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleB);
                        //Pos.Y = Pos.Y + HDT_BIB_A_PnPInfo.WorkingPos_Y;
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "SetActionCommand_KSM", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleB, ACTIONMODE.MOVE, Pos);
                    }
                    break;
                case PnPStation.BOARD_A:
                    {
                        //public BasePosInfo GetBasicInfo(BIBStageModuleOwner station, BIBStageID id)
                        //Pos = (BasePosInfo)SYSPara.CallProc(ModuleName_BSM, "GetBasicInfo", BIBStageModuleOwner.HDT_BIB_A, BIBStageID.BIBStageA);
                        //Pos.Y = Pos.Y + HDT_BIB_A_PnPInfo.WorkingPos_Y;
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.HDT_BIB_A, BIBStageID.BIBStageA, ACTIONMODE.MOVE, Pos);// ACTIONMODE.MOVE, HDT_BIB_A_PnPInfo.WorkingPos_Y, HDT_BIB_BIBStageID, "", "");
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        //public BasePosInfo GetBasicInfo(BIBStageModuleOwner station, BIBStageID id)
                        //Pos = (BasePosInfo)SYSPara.CallProc(ModuleName_BSM, "GetBasicInfo", BIBStageModuleOwner.HDT_BIB_A, BIBStageID.BIBStageB);
                        //Pos.Y = Pos.Y + HDT_BIB_A_PnPInfo.WorkingPos_Y;
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.HDT_BIB_A, BIBStageID.BIBStageB, ACTIONMODE.MOVE, Pos);// ACTIONMODE.MOVE, HDT_BIB_A_PnPInfo.WorkingPos_Y, HDT_BIB_BIBStageID, "", "");
                    }
                    break;
            }
            if (T.Equals(ThreeValued.TRUE) && T1.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart201_Run()
        {
            ThreeValued T1 = ThreeValued.UNKNOWN;
            ThreeValued T2 = ThreeValued.UNKNOWN;
            T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_A);

            switch (HDT_BIB_A_PnPInfo.SourceStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "GetActionResult_KSM", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleA);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "GetActionResult_KSM", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleB);
                    }
                    break;
                case PnPStation.BOARD_A:
                    {

                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageA);
                    }
                    break;
                case PnPStation.BOARD_B:
                    {

                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageB);
                    }
                    break;
                case PnPStation.PassBox:
                case PnPStation.BinBox:
                    {
                        T2 = ThreeValued.TRUE;
                    }
                    break;
            }

            if (T1.Equals(ThreeValued.TRUE) && T2.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart202_Run()
        {
            BasePosInfo Pos = new BasePosInfo();
            Pos.X = HDT_BIB_A_PnPInfo.WorkingPos_X;
            Pos.Y = HDT_BIB_A_PnPInfo.WorkingPos_Y;
            Pos.Z = HDT_BIB_A_PnPInfo.WorkingPos_Z;
            Pos.U = HDT_BIB_A_PnPInfo.WorkingPos_U;

            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_A, ACTIONMODE.PICKUP, Pos);// HDT_BF_A_PnPInfo.SourceStation, ACTIONMODE.PICKUP, Pos);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart203_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_A);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart204_Run()
        {
            bool[,] AfterPickPnPState = new bool[HDT_BIB_A_PnPInfo.HEAD_X, HDT_BIB_A_PnPInfo.HEAD_Y];
            NozzleState[,] AfterPickNozzleState = (NozzleState[,])SYSPara.CallProc(ModuleName_HDT, "GetNozzleState", BoardHeadID.HDT_A);
            int CompleteCount = 0;
            int LostCount = 0;

            for (int y = 0; y < HDT_BIB_A_PnPInfo.HEAD_Y; y++)
            {
                for (int x = 0; x < HDT_BIB_A_PnPInfo.HEAD_X; x++)
                {
                    if (AfterPickNozzleState[x, y].Equals(NozzleState.InUsing))
                    {
                        AfterPickPnPState[x, y] = true;
                        CompleteCount++;
                    }
                    else if (AfterPickNozzleState[x, y].Equals(NozzleState.PnPFailure))
                    {
                        AfterPickPnPState[x, y] = false;
                        LostCount++;
                        if (HDT_BIB_A_PnPInfo.SourceStation.Equals(PnPStation.BOARD_A) ||
                            HDT_BIB_A_PnPInfo.SourceStation.Equals(PnPStation.BOARD_B))
                        {
                            HDTA_NozzleStateIsFail = true;
                        }
                    }
                    else
                    {
                        AfterPickPnPState[x, y] = false;
                    }
                }
            }
            bool bRet = false;
            switch (HDT_BIB_A_PnPInfo.SourceStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        bRet = HDT_BIB_A_PnP.KitDataExchange(TDEx_HDT_BIB_A, TDEx_Left_KitShuttle, Board_BKS.CurrentKitBlockInfo, AfterPickPnPState);
                        HDT_BIB_A_BookingInfo = Board_BKS.CurrentKitBlockInfo;
                        Board_BKS.SocketBlockDone(KitShuttleID.TransferShuttleA, HDT_BIB_A_PnPInfo.Current_VechicleState);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        bRet = HDT_BIB_A_PnP.KitDataExchange(TDEx_HDT_BIB_A, TDEx_Right_KitShuttle, Board_BKS.CurrentKitBlockInfo, AfterPickPnPState);
                        HDT_BIB_A_BookingInfo = Board_BKS.CurrentKitBlockInfo;
                        Board_BKS.SocketBlockDone(KitShuttleID.TransferShuttleB, HDT_BIB_A_PnPInfo.Current_VechicleState);
                    }
                    break;
                case PnPStation.BOARD_A:
                    {
                        HDT_BIB_A_PnP.DiePakDataExchange(TDEx_HDT_BIB_A, TDEx_Left_Board, Board_BKS.CurrentWorkInfo, AfterPickPnPState);
                        //HDT_BIB_A_BookingInfo = Board_BKS.CurrentWorkInfo;
                        //Board_BKS.WorkBlockDone();
                        HDT_BIB_A_BookingInfo = Board_BKS.CurrentKitBlockInfo;
                        KitShuttleID kitID = HDT_BIB_A_PnPInfo.TargetStation == PnPStation.KIT_SHUTTLE_A ? KitShuttleID.TransferShuttleA : KitShuttleID.TransferShuttleB;
                        Board_BKS.SocketBlockDone(kitID, HDT_BIB_A_PnPInfo.Current_VechicleState);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Tested, JStatusType.Process, CompleteCount);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Tested, JStatusType.Missing, LostCount);
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        HDT_BIB_A_PnP.DiePakDataExchange(TDEx_HDT_BIB_A, TDEx_Right_Board, Board_BKS.CurrentWorkInfo, AfterPickPnPState);
                        //HDT_BIB_A_BookingInfo = Board_BKS.CurrentWorkInfo;
                        //Board_BKS.WorkBlockDone();
                        KitShuttleID kitID = HDT_BIB_A_PnPInfo.TargetStation == PnPStation.KIT_SHUTTLE_A ? KitShuttleID.TransferShuttleA : KitShuttleID.TransferShuttleB;
                        Board_BKS.SocketBlockDone(kitID, HDT_BIB_A_PnPInfo.Current_VechicleState);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Tested, JStatusType.Process, CompleteCount);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Tested, JStatusType.Missing, LostCount);
                    }
                    break;

                default:
                    {
                        return FlowChart.FCRESULT.IDLE;
                    }
            }

            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart205_Run()
        {
            if (SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD))
            {
                KitShuttleID KitID = KitShuttleID.NONE;
                if (HDT_BIB_A_PnPInfo.SourceStation.Equals(PnPStation.KIT_SHUTTLE_A)) KitID = KitShuttleID.TransferShuttleA;
                if (HDT_BIB_A_PnPInfo.SourceStation.Equals(PnPStation.KIT_SHUTTLE_B)) KitID = KitShuttleID.TransferShuttleB;
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "UnlockTransferShuttle", (KitShuttleOwner)KitShuttleOwner.HDT_BIB_A, (KitShuttleID)KitID);
                if (T.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else
            {
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "UnlockBIBStage", (BIBStageModuleOwner)BIBStageModuleOwner.HDT_BIB_A, (BIBStageID)HDT_BIB_BIBStageID);
                if (T.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart207_Run()
        {
            //if (!SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD))
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            if (HDT_BIB_A_PnPInfo.TargetStation == PnPStation.KIT_SHUTTLE_A || HDT_BIB_A_PnPInfo.TargetStation == PnPStation.KIT_SHUTTLE_B)
            {
                KitShuttleID KitID = KitShuttleID.NONE;
                if (HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_A)) KitID = KitShuttleID.TransferShuttleA;
                if (HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_B)) KitID = KitShuttleID.TransferShuttleB;
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "LockTransferShuttle", (KitShuttleOwner)KitShuttleOwner.HDT_BIB_A, (KitShuttleID)KitID);
                if (T.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else
            {
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "LockBIBStage", (BIBStageModuleOwner)BIBStageModuleOwner.HDT_BIB_A, (BIBStageID)HDT_BIB_BIBStageID);
                if (T.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE; 
        }

        private FlowChart.FCRESULT flowChart208_Run()
        {
            BasePosInfo HDT_BIB_A_Basic = new BasePosInfo();
            BasePosInfo BSMBasic = new BasePosInfo();
            BasePosInfo KSMBasic = new BasePosInfo();
            //Point Placeoffset = new Point();
            BasePosInfo Offset = new BasePosInfo();
            Point pos = new Point();

            switch (HDT_BIB_A_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        Offset = GetOffset(BoardHeadID.HDT_A, PnPStation.KIT_SHUTTLE_A, false);
                        pos = HDT_BIB_A_PnP.CalcKitPnPPos(HDT_BIB_A_PnPInfo.HDTTray, HDT_BIB_A_PnPInfo.TargetTray, Board_BKS.CurrentKitBlockInfo);
                        HDT_BIB_A_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", PnPStation.KIT_SHUTTLE_A, BoardHeadID.HDT_A);
                        KSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleA);
                        float pitchy = (float)(KitInfo.YN) / 2;
                        KSMBasic.Y -= (int)(pitchy * KitInfo.YP);
                        HDT_BIB_A_PnPInfo.WorkingPos_X = pos.X + HDT_BIB_A_Basic.X + Offset.X;
                        HDT_BIB_A_PnPInfo.WorkingPos_Y = pos.Y + KSMBasic.Y + Offset.Y;
                        HDT_BIB_A_PnPInfo.WorkingPos_Z = HDT_BIB_A_Basic.Z + Offset.Z;
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        Offset = GetOffset(BoardHeadID.HDT_A, PnPStation.KIT_SHUTTLE_B, false);
                        pos = HDT_BIB_A_PnP.CalcKitPnPPos(HDT_BIB_A_PnPInfo.HDTTray, HDT_BIB_A_PnPInfo.TargetTray, Board_BKS.CurrentKitBlockInfo);
                        HDT_BIB_A_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", PnPStation.KIT_SHUTTLE_B, BoardHeadID.HDT_A);
                        KSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleB);
                        float pitchy = (float)(KitInfo.YN) / 2;
                        KSMBasic.Y -= (int)(pitchy * KitInfo.YP);
                        HDT_BIB_A_PnPInfo.WorkingPos_X = pos.X + HDT_BIB_A_Basic.X + Offset.X;
                        HDT_BIB_A_PnPInfo.WorkingPos_Y = pos.Y + KSMBasic.Y + Offset.Y;
                        HDT_BIB_A_PnPInfo.WorkingPos_Z = HDT_BIB_A_Basic.Z + Offset.Z;
                    }
                    break;
                case PnPStation.BOARD_A:
                    {
                        Offset = GetOffset(BoardHeadID.HDT_A, PnPStation.BOARD_A, false);
                        //pos = HDT_BIB_A_PnP.CalcBoardPnPPos(HDT_BIB_A_PnPInfo.HDTTray, HDT_BIB_A_PnPInfo.TargetTray, Board_BKS.CurrentKitBlockInfo);
                        pos = HDT_BIB_A_PnP.CalcBoardPnPPos(HDT_BIB_A_PnPInfo.HDTTray, HDT_BIB_A_PnPInfo.TargetTray, HDT_BIB_A_BookingInfo);
                        HDT_BIB_A_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", PnPStation.BOARD_A, BoardHeadID.HDT_A);
                        // DPMBasic.Y = HDT_DP_Basic.X;
                        BSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_BSM, "GetBasicInfo", BIBStageModuleOwner.HDT_BIB_A, BIBStageID.BIBStageA);
                        HDT_BIB_A_PnPInfo.WorkingPos_X = pos.X + HDT_BIB_A_Basic.X + Offset.X + BoardInfo.XO - BoardInfo.Width;
                        HDT_BIB_A_PnPInfo.WorkingPos_Y = pos.Y + BSMBasic.Y + Offset.Y + BoardInfo.YO;
                        HDT_BIB_A_PnPInfo.WorkingPos_Z = HDT_BIB_A_Basic.Z + Offset.Z;
                        //if (true)
                        //{
                        //    PnPStation TargetStation = PnPStation.PassBox;
                        //    //if (HDT_BIB_A_BookingInfo.Map.Equals("2")) TargetStation = PnPStation.BinBox;
                        //    pos = HDT_BIB_A_PnP.CalcBoxPnPPos(HDT_BIB_A_PnPInfo.HDTTray, HDT_BIB_A_PnPInfo.TargetTray);
                        //    HDT_BIB_A_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", TargetStation, BoardHeadID.HDT_A);
                        //    HDT_BIB_A_PnPInfo.WorkingPos_X = HDT_BIB_A_Basic.X;
                        //    HDT_BIB_A_PnPInfo.WorkingPos_Z = HDT_BIB_A_Basic.Z;
                        //}
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        Offset = GetOffset(BoardHeadID.HDT_A, PnPStation.BOARD_B, false);
                        //pos = HDT_BIB_A_PnP.CalcBoardPnPPos(HDT_BIB_A_PnPInfo.HDTTray, HDT_BIB_A_PnPInfo.TargetTray, Board_BKS.CurrentKitBlockInfo);
                        pos = HDT_BIB_A_PnP.CalcBoardPnPPos(HDT_BIB_A_PnPInfo.HDTTray, HDT_BIB_A_PnPInfo.TargetTray, HDT_BIB_A_BookingInfo);
                        HDT_BIB_A_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", PnPStation.BOARD_B, BoardHeadID.HDT_A);
                        // DPMBasic.Y = HDT_DP_Basic.X;
                        BSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_BSM, "GetBasicInfo", BIBStageModuleOwner.HDT_BIB_A, BIBStageID.BIBStageB);
                        HDT_BIB_A_PnPInfo.WorkingPos_X = pos.X + HDT_BIB_A_Basic.X + Offset.X + BoardInfo.XO - BoardInfo.Width;
                        HDT_BIB_A_PnPInfo.WorkingPos_Y = pos.Y + BSMBasic.Y + Offset.Y + BoardInfo.YO;
                        HDT_BIB_A_PnPInfo.WorkingPos_Z = HDT_BIB_A_Basic.Z + Offset.Z;
                        //if (true)
                        //{
                        //    PnPStation TargetStation = PnPStation.PassBox;
                        //    //if (HDT_BIB_A_BookingInfo.Map.Equals("2")) TargetStation = PnPStation.BinBox;
                        //    pos = HDT_BIB_A_PnP.CalcBoxPnPPos(HDT_BIB_A_PnPInfo.HDTTray, HDT_BIB_A_PnPInfo.TargetTray);
                        //    HDT_BIB_A_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", TargetStation, BoardHeadID.HDT_A);
                        //    HDT_BIB_A_PnPInfo.WorkingPos_X = HDT_BIB_A_Basic.X;
                        //    HDT_BIB_A_PnPInfo.WorkingPos_Z = HDT_BIB_A_Basic.Z;
                        //}
                    }
                    break;
                case PnPStation.PassBox:
                case PnPStation.BinBox:
                    {
                        PnPStation TargetStation = PnPStation.PassBox;
                        if (!HDT_BIB_A_BookingInfo.Map.Equals("1")) TargetStation = PnPStation.BinBox;
                        pos = HDT_BIB_A_PnP.CalcBoxPnPPos(HDT_BIB_A_PnPInfo.HDTTray, HDT_BIB_A_PnPInfo.TargetTray);
                        HDT_BIB_A_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", TargetStation, BoardHeadID.HDT_A);
                        HDT_BIB_A_PnPInfo.WorkingPos_X = HDT_BIB_A_Basic.X;
                        HDT_BIB_A_PnPInfo.WorkingPos_Z = HDT_BIB_A_Basic.Z;
                    }
                    break;

                default:
                    {
                        return FlowChart.FCRESULT.IDLE;
                    }
                    break;
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart209_Run()
        {
            NozzleState[,] PnPState = HDT_BIB_A_PnPInfo.GetBeforePlaceHeadAction();
            bool T = (bool)SYSPara.CallProc(ModuleName_HDT, "SetNozzleState", PnPState, BoardHeadID.HDT_A);
            if (T)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart295_Run()
        {
            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_A);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            BasePosInfo pos = new BasePosInfo();
            int Offset = (int)SYSPara.CallProc(ModuleName_HDT, "GetNozzleCCDOffset", BoardHeadID.HDT_A);
            int OffsetY = (int)SYSPara.CallProc(ModuleName_HDT, "GetNozzleCCDOffsetY", BoardHeadID.HDT_A);
            pos.X = HDT_BIB_A_PnPInfo.WorkingPos_X + Offset;
            pos.Y = HDT_BIB_A_PnPInfo.WorkingPos_Y + OffsetY;
            pos.Z = HDT_BIB_A_PnPInfo.WorkingPos_Z;
            pos.U = HDT_BIB_A_PnPInfo.WorkingPos_U;

            ThreeValued T1 = ThreeValued.TRUE;
            ThreeValued T2 = ThreeValued.TRUE;
            bool bFocus = true;
            switch (HDT_BIB_A_PnPInfo.TargetStation)
            {
                case PnPStation.BOARD_A:
                    {
                        T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_A, ACTIONMODE.MOVE, pos);
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.HDT_BIB_A, BIBStageID.BIBStageA, ACTIONMODE.MOVE, pos);
                        bFocus = (bool)SYSPara.CallProc(ModuleName_HDT, "SetVisionFocus", BoardHeadID.HDT_A, BIBStageID.BIBStageA);
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_A, ACTIONMODE.MOVE, pos);
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.HDT_BIB_A, BIBStageID.BIBStageB, ACTIONMODE.MOVE, pos);
                        bFocus = (bool)SYSPara.CallProc(ModuleName_HDT, "SetVisionFocus", BoardHeadID.HDT_A, BIBStageID.BIBStageB);
                    }
                    break;
            }
            if (T1.Equals(ThreeValued.TRUE) && T2.Equals(ThreeValued.TRUE) && bFocus)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart296_Run()
        {
            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_A);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            ThreeValued T1 = ThreeValued.TRUE;
            ThreeValued T2 = ThreeValued.TRUE;

            switch (HDT_BIB_A_PnPInfo.TargetStation)
            {
                case PnPStation.BOARD_A:
                    {
                        T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_A);
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageA);
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_A);
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageB);
                    }
                    break;
            }

            if (T1.Equals(ThreeValued.TRUE) && T2.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart297_Run()
        {
            if (!HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_A) && !HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_B))
                return FlowChart.FCRESULT.NEXT;

            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_A);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            BasePosInfo pos = new BasePosInfo();
            pos.X = HDT_BIB_A_PnPInfo.WorkingPos_X;
            pos.Y = HDT_BIB_A_PnPInfo.WorkingPos_Y;
            pos.Z = HDT_BIB_A_PnPInfo.WorkingPos_Z;
            pos.U = HDT_BIB_A_PnPInfo.WorkingPos_U;
            pos.ColIndex = HDT_BIB_A_BookingInfo.SocketX;
            pos.RowIndex = HDT_BIB_A_BookingInfo.SocketY;
            pos.BoardCount = BoardCount + 1;

            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_A, ACTIONMODE.GRAB_CCD, pos);
            if (T1.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart298_Run()
        {
            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_A);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            if (!HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_A) && !HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_B))
                return FlowChart.FCRESULT.NEXT;

            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_A);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }
        private FlowChart.FCRESULT flowChart303_Run()
        {
            bLoadToBox_A = false;
            if (!HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_A) && !HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_B))
                return FlowChart.FCRESULT.NEXT;

            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_A);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            InspectionState state = InspectionState.Unknow;
            if (HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_A) || HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_B))
            {
                state = (InspectionState)SYSPara.CallProc(ModuleName_HDT, "GetInspectState", BoardHeadID.HDT_A);
                switch (state)
                {
                    case InspectionState.Success:   //有IC 上板未放置有IC為異常
                        {
                            //Alarm
                            return FlowChart.FCRESULT.NEXT;
                        }
                        break;
                    case InspectionState.Fail:  //無IC
                        {
                            return FlowChart.FCRESULT.NEXT;
                        }
                        break;
                    case InspectionState.Error: //無Socket 丟到垃圾桶
                        {
                            Point pos = new Point();
                            BasePosInfo HDT_BIB_A_Basic = new BasePosInfo();
                            PnPStation TargetStation = PnPStation.BinBox;
                            pos = HDT_BIB_A_PnP.CalcBoxPnPPos(HDT_BIB_A_PnPInfo.HDTTray, HDT_BIB_A_PnPInfo.TargetTray);
                            HDT_BIB_A_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", TargetStation, BoardHeadID.HDT_A);
                            HDT_BIB_A_PnPInfo.WorkingPos_X = HDT_BIB_A_Basic.X;
                            HDT_BIB_A_PnPInfo.WorkingPos_Z = HDT_BIB_A_Basic.Z;
                            bLoadToBox_A = true;
                            return FlowChart.FCRESULT.NEXT;
                        }
                        break;
                }
                return FlowChart.FCRESULT.IDLE;
            }
            else
            {
                return FlowChart.FCRESULT.NEXT;
            }
        }

        private FlowChart.FCRESULT flowChart304_Run()
        {
            string code = string.Format("{0}{1:0000}", "E", "0901");
            string args = "Board Head A";
            SYSPara.Alarm.Say(code, args);
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart217_Run()
        {
            BasePosInfo pos = new BasePosInfo();
            BasePosInfo InspectOffset = new BasePosInfo();
            int OffsetY = 0;
            if (HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_B) || HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_A))
            {
                InspectOffset = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetInspectResult", BoardHeadID.HDT_A);
                bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_A);
                if (b1)
                {
                    OffsetY = (int)SYSPara.CallProc(ModuleName_HDT, "GetNozzleCCDOffsetY", BoardHeadID.HDT_A);
                    //InspectOffset.Y = InspectOffset.Y + OffsetY;
                }
            }
            pos.X = HDT_BIB_A_PnPInfo.WorkingPos_X + InspectOffset.X;
            pos.Y = HDT_BIB_A_PnPInfo.WorkingPos_Y - InspectOffset.Y - OffsetY;
            pos.Z = HDT_BIB_A_PnPInfo.WorkingPos_Z + InspectOffset.Z;
            pos.U = HDT_BIB_A_PnPInfo.WorkingPos_U + InspectOffset.U;
            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_A, ACTIONMODE.MOVE, pos);// HDT_BIB_A_PnPInfo.TargetStation, ACTIONMODE.PLACEMENT, Pos);

            ThreeValued T = ThreeValued.UNKNOWN;
            switch (HDT_BIB_A_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "SetActionCommand_KSM", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleA, ACTIONMODE.MOVE, pos);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "SetActionCommand_KSM", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleB, ACTIONMODE.MOVE, pos);
                    }
                    break;
                case PnPStation.BOARD_A:
                    {
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.HDT_BIB_A, BIBStageID.BIBStageA, ACTIONMODE.MOVE, pos);//ACTIONMODE.MOVE, HDT_BIB_A_PnPInfo.WorkingPos_Y, HDT_BIB_BIBStageID, "", "");
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.HDT_BIB_A, BIBStageID.BIBStageB, ACTIONMODE.MOVE, pos);//ACTIONMODE.MOVE, HDT_BIB_A_PnPInfo.WorkingPos_Y, HDT_BIB_BIBStageID, "", "");
                    }
                    break;
                case PnPStation.PassBox:
                    {
                        T = ThreeValued.TRUE;
                    }
                    break;
                case PnPStation.BinBox:
                    {
                        T = ThreeValued.TRUE;
                    }
                    break;
            }
            if (T.Equals(ThreeValued.TRUE) && T1.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;


        }

        private FlowChart.FCRESULT flowChart216_Run()
        {
            ThreeValued T1 = ThreeValued.UNKNOWN;
            ThreeValued T2 = ThreeValued.UNKNOWN;
            T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_A);

            switch (HDT_BIB_A_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "GetActionResult_KSM", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleA);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "GetActionResult_KSM", KitShuttleOwner.HDT_BIB_A, KitShuttleID.TransferShuttleB);
                    }
                    break;
                case PnPStation.BOARD_A:
                    {

                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageA);
                    }
                    break;
                case PnPStation.BOARD_B:
                    {

                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageB);
                    }
                    break;
                case PnPStation.PassBox:
                case PnPStation.BinBox:
                    {
                        T2 = ThreeValued.TRUE;
                    }
                    break;
            }

            if (T1.Equals(ThreeValued.TRUE) && T2.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart211_Run()
        {
            BasePosInfo Pos = new BasePosInfo();

            Pos.X = HDT_BIB_A_PnPInfo.WorkingPos_X;
            Pos.Y = HDT_BIB_A_PnPInfo.WorkingPos_Y;
            Pos.Z = HDT_BIB_A_PnPInfo.WorkingPos_Z;
            Pos.U = HDT_BIB_A_PnPInfo.WorkingPos_U;

            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_A, ACTIONMODE.PLACEMENT, Pos);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart212_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_A);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart213_Run()
        {
            bool[,] AfterPlacePnPState = new bool[HDT_BIB_A_PnPInfo.HEAD_X, HDT_BIB_A_PnPInfo.HEAD_Y];
            NozzleState[,] PnPState = (NozzleState[,])SYSPara.CallProc(ModuleName_HDT, "GetNozzleState", BoardHeadID.HDT_A);
            int CompleteCount = 0;
            int LostCount = 0;
            //BinDefine BinType = SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD) ? BinDefine.Untested : BinDefine.Tested;
            for (int y = 0; y < HDT_BIB_A_PnPInfo.HEAD_Y; y++)
            {
                for (int x = 0; x < HDT_BIB_A_PnPInfo.HEAD_X; x++)
                {
                    if (PnPState[x, y].Equals(NozzleState.InUsing))
                    {
                        AfterPlacePnPState[x, y] = true;
                        CompleteCount++;
                    }
                    else if (PnPState[x, y].Equals(NozzleState.ICLost))
                    {
                        AfterPlacePnPState[x, y] = false;
                        LostCount++;
                    }
                    else
                    {
                        AfterPlacePnPState[x, y] = false;
                        LostCount++;
                    }
                }
            }

            bool bRet = false;
            switch (HDT_BIB_A_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        bRet = HDT_BIB_A_PnP.KitDataExchange(TDEx_HDT_BIB_A, TDEx_Right_KitShuttle, Board_BKS.CurrentKitBlockInfo, AfterPlacePnPState);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        bRet = HDT_BIB_A_PnP.KitDataExchange(TDEx_HDT_BIB_A, TDEx_Left_KitShuttle, Board_BKS.CurrentKitBlockInfo, AfterPlacePnPState);
                    }
                    break;
                case PnPStation.BOARD_A:
                    {
                        bRet = HDT_BIB_A_PnP.DiePakDataExchange(TDEx_HDT_BIB_A, TDEx_Left_Board, HDT_BIB_A_BookingInfo, AfterPlacePnPState);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Untested, JStatusType.Completed, CompleteCount);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Untested, JStatusType.Missing, LostCount);
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        bRet = HDT_BIB_A_PnP.DiePakDataExchange(TDEx_HDT_BIB_A, TDEx_Right_Board, HDT_BIB_A_BookingInfo, AfterPlacePnPState);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Untested, JStatusType.Completed, CompleteCount);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Untested, JStatusType.Missing, LostCount);
                    }
                    break;
                case PnPStation.PassBox:
                case PnPStation.BinBox:
                    {
                        TrayDataEx TDEx_Target = TDEx_PassBox;
                        if (HDT_BIB_A_BookingInfo.Map.Equals("1"))
                        {
                            GlobalDefine.OEE.UpdateBinQty(BinDefine.Bin1, JStatusType.Completed, CompleteCount);
                        }
                        else
                        {
                            TDEx_Target = TDEx_FailBox;
                            BinDefine temp = GetBinDefine(HDT_BIB_A_BookingInfo.Map);

                            GlobalDefine.OEE.UpdateBinQty(temp, JStatusType.Completed, CompleteCount);
                        }

                        bRet = HDT_BIB_A_PnP.DiePakDataExchange(TDEx_HDT_BIB_A, TDEx_Target, HDT_BIB_A_BookingInfo, AfterPlacePnPState);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Tested, JStatusType.Completed, CompleteCount);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Tested, JStatusType.Missing, LostCount);

                        TDEx_PassBox.BinReplace((byte)BinDefine.Bin1, (byte)BinDefine.Empty);
                        TDEx_FailBox.BinReplace((byte)BinDefine.Bin1, (byte)BinDefine.Empty);

                    }
                    break;
                default:
                    {
                        return FlowChart.FCRESULT.IDLE;
                    }
                    break;
            }
            if (bRet)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart299_Run()
        {
            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_A);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            if (bLoadToBox_A) return FlowChart.FCRESULT.NEXT;

            BasePosInfo pos = new BasePosInfo();
            int Offset = (int)SYSPara.CallProc(ModuleName_HDT, "GetNozzleCCDOffset", BoardHeadID.HDT_A);
            int OffsetY = (int)SYSPara.CallProc(ModuleName_HDT, "GetNozzleCCDOffsetY", BoardHeadID.HDT_A);
            pos.X = HDT_BIB_A_PnPInfo.WorkingPos_X + Offset;
            pos.Y = HDT_BIB_A_PnPInfo.WorkingPos_Y + OffsetY;
            pos.Z = HDT_BIB_A_PnPInfo.WorkingPos_Z;
            pos.U = HDT_BIB_A_PnPInfo.WorkingPos_U;

            ThreeValued T1 = ThreeValued.TRUE;
            ThreeValued T2 = ThreeValued.TRUE;
            switch (HDT_BIB_A_PnPInfo.TargetStation)
            {
                case PnPStation.BOARD_A:
                    {
                        T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_A, ACTIONMODE.MOVE, pos);
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.HDT_BIB_A, BIBStageID.BIBStageA, ACTIONMODE.MOVE, pos);
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_A, ACTIONMODE.MOVE, pos);
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.HDT_BIB_A, BIBStageID.BIBStageB, ACTIONMODE.MOVE, pos);
                    }
                    break;
            }
            if (T1.Equals(ThreeValued.TRUE) && T2.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart300_Run()
        {
            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_A);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            if (bLoadToBox_A) return FlowChart.FCRESULT.NEXT;

            ThreeValued T1 = ThreeValued.TRUE;
            ThreeValued T2 = ThreeValued.TRUE;

            switch (HDT_BIB_A_PnPInfo.TargetStation)
            {
                case PnPStation.BOARD_A:
                    {
                        T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_A);
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageA);
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_A);
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageB);
                    }
                    break;
            }

            if (T1.Equals(ThreeValued.TRUE) && T2.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart301_Run()
        {
            if (!HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_A) && !HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_B))
                return FlowChart.FCRESULT.NEXT;

            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_A);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            if (bLoadToBox_A) return FlowChart.FCRESULT.NEXT;


            BasePosInfo pos = new BasePosInfo();
            pos.X = HDT_BIB_A_PnPInfo.WorkingPos_X;
            pos.Y = HDT_BIB_A_PnPInfo.WorkingPos_Y;
            pos.Z = HDT_BIB_A_PnPInfo.WorkingPos_Z;
            pos.U = HDT_BIB_A_PnPInfo.WorkingPos_U;
            pos.ColIndex = HDT_BIB_A_BookingInfo.SocketX;
            pos.RowIndex = HDT_BIB_A_BookingInfo.SocketY;
            pos.BoardCount = BoardCount + 1;

            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_A, ACTIONMODE.GRAB_CCD_AF, pos);
            if (T1.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart302_Run()
        {
            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_A);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            if (!HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_A) && !HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_B))
                return FlowChart.FCRESULT.NEXT;

            if (bLoadToBox_A) return FlowChart.FCRESULT.NEXT;

            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_A);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart306_Run()
        {
            if (!HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_A) && !HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_B))
                return FlowChart.FCRESULT.NEXT;

            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_A);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            if (bLoadToBox_A) return FlowChart.FCRESULT.NEXT;

            InspectionState state = InspectionState.Unknow;
            if (HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_A) || HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_B))
            {
                state = (InspectionState)SYSPara.CallProc(ModuleName_HDT, "GetInspectState", BoardHeadID.HDT_A);
                switch (state)
                {
                    case InspectionState.Success:   //上板後有IC正常
                        {
                            return FlowChart.FCRESULT.NEXT;
                        }
                        break;
                    case InspectionState.Fail:  //上板後無IC異常
                    case InspectionState.Error: //異常
                        {
                            //Alarm CCD 檢測異常
                            return FlowChart.FCRESULT.CASE1;    //重新拍攝
                        }
                        break;
                }
                return FlowChart.FCRESULT.IDLE;
            }
            else
            {
                return FlowChart.FCRESULT.NEXT;
            }
        }

        private FlowChart.FCRESULT flowChart307_Run()
        {
            string code = string.Format("{0}{1:0000}", "E", "0901");
            string args = "Board Head A";
            SYSPara.Alarm.Say(code, args);
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart214_Run()
        {
            //if (!SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD))
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            if (HDT_BIB_A_PnPInfo.TargetStation == PnPStation.KIT_SHUTTLE_A || HDT_BIB_A_PnPInfo.TargetStation == PnPStation.KIT_SHUTTLE_B)
            {
                KitShuttleID KitID = KitShuttleID.NONE;
                if (HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_A)) KitID = KitShuttleID.TransferShuttleA;
                if (HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_B)) KitID = KitShuttleID.TransferShuttleB;
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "UnlockTransferShuttle", (KitShuttleOwner)KitShuttleOwner.HDT_BIB_A, (KitShuttleID)KitID);
                if (T.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else
            {
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "UnlockBIBStage", (BIBStageModuleOwner)BIBStageModuleOwner.HDT_BIB_A, (BIBStageID)HDT_BIB_BIBStageID);
                if (T.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }
        #endregion
        #region BoardHead B
        private FlowChart.FCRESULT flowChart7_Run()
        {
            if (Flag_HDT_BIB_B_Action.IsDoIt())
            {
                Flag_HDT_BIB_B_Action.Doing();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart68_Run()
        {
            if (SYSPara.SystemProductionMode.HasFlag(PRODUCTION_MODE.LOAD))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart95_Run()
        {
            Flag_HDT_BIB_B_Action.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart38_Run()
        {
            //if (SYSPara.Lotend)// && mBSM.GetLotEndOk())
            if (SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.UNLOAD))
            {
                if (SYSPara.Lotend && Flag_LeftBSM_LotEnd.IsDone() && Flag_RightBSM_LotEnd.IsDone())
                {
                    Flag_BottomHDT_LotEnd.DoIt();
                    return FlowChart.FCRESULT.CASE1;
                }
            }
            else
            {
                if (SYSPara.Lotend && Flag_TopBowlFeeder_LotEnd.IsDone() && Flag_BottomBowlFeeder_LotEnd.IsDone())
                {
                    if (Board_BKS.BoardBookingListCount() <= 0)
                    {
                        Flag_BottomHDT_LotEnd.DoIt();
                        return FlowChart.FCRESULT.CASE1;
                    }
                }
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart49_Run()
        {
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart229_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart228_Run()
        {
            bool b1 = LeftKitStateControl.IsStateDoIt(VehicleState.UNTESTED_FULL);
            bool b2 = RightKitStateControl.IsStateDoIt(VehicleState.UNTESTED_FULL);
            if (b1)
            {
                HDT_BIB_B_KitShuttleID = KitShuttleID.TransferShuttleA;
                return FlowChart.FCRESULT.NEXT;
            }
            else if (b2)
            {
                HDT_BIB_B_KitShuttleID = KitShuttleID.TransferShuttleB;
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart227_Run()
        {
            PnPStation WorkStage = PnPStation.BOARD_A;
            TrayDataEx TDEx_Temp = TDEx_Left_Board;
            if (HDT_BIB_BIBStageID.Equals(BIBStageID.BIBStageB))
            {
                WorkStage = PnPStation.BOARD_B;
                TDEx_Temp = TDEx_Right_Board;
            }
            switch (HDT_BIB_B_KitShuttleID)
            {
                case KitShuttleID.TransferShuttleA:
                    {
                        HDT_BIB_B_PnPInfo = new PnPInfo(TDEx_HDT_BIB_B, PnPStation.KIT_SHUTTLE_A, TDEx_Left_KitShuttle, GlobalDefine.PassBin, (PnPStation)WorkStage, TDEx_Temp, GlobalDefine.EmptyBin, 1, 1, VehicleState.UNTESTED_FULL);
                    }
                    break;
                case KitShuttleID.TransferShuttleB:
                    {
                        HDT_BIB_B_PnPInfo = new PnPInfo(TDEx_HDT_BIB_B, PnPStation.KIT_SHUTTLE_B, TDEx_Right_KitShuttle, GlobalDefine.PassBin, (PnPStation)WorkStage, TDEx_Temp, GlobalDefine.EmptyBin, 1, 1, VehicleState.UNTESTED_FULL);
                    }
                    break;

                default:
                    {
                        //alarm
                        return FlowChart.FCRESULT.IDLE;
                    }

            }

            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart226_Run()
        {
            Flag_HDT_BIB_B_PnP.DoIt();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart225_Run()
        {
            if (Flag_HDT_BIB_B_PnP.IsDone() && Flag_HDT_BIB_A_PnP.IsDone())
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart224_Run()
        {
            HDT_BIB_B_KitShuttleID = KitShuttleID.NONE;
            bool b1 = true;
            switch (HDT_BIB_B_PnPInfo.SourceStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        if (LeftKitStateControl.IsStateDoIt(VehicleState.UNTESTED_FULL))
                            b1 = LeftKitStateControl.StateDone(LeftKitStateControl.State);

                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        if (RightKitStateControl.IsStateDoIt(VehicleState.UNTESTED_FULL))
                            b1 = RightKitStateControl.StateDone(RightKitStateControl.State);
                    }
                    break;
            }

            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart210_Run()
        {
            bool b1 = LeftKitStateControl.IsStateDoIt(VehicleState.TESTED_EMPTY_BOOKED);
            bool b2 = RightKitStateControl.IsStateDoIt(VehicleState.TESTED_EMPTY_BOOKED);
            if (b1)
            {
                HDT_BIB_B_KitShuttleID = KitShuttleID.TransferShuttleA;
                return FlowChart.FCRESULT.NEXT;
            }
            else if (b2)
            {
                HDT_BIB_B_KitShuttleID = KitShuttleID.TransferShuttleB;
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart218_Run()
        {
            TrayDataEx TDEx_Temp = HDT_BIB_B_KitShuttleID == KitShuttleID.TransferShuttleA ? TDEx_Left_KitShuttle : TDEx_Right_KitShuttle;

            if (HDT_BIB_B_KitShuttleID == KitShuttleID.TransferShuttleA || HDT_BIB_B_KitShuttleID == KitShuttleID.TransferShuttleB)
            {
                switch (HDT_BIB_BIBStageID)
                {
                    case BIBStageID.BIBStageA:
                        {
                            HDT_BIB_B_PnPInfo = new PnPInfo(TDEx_HDT_BIB_B, PnPStation.BOARD_A, TDEx_Left_Board, GlobalDefine.AllBin, PnPStation.BinBox, TDEx_Temp, GlobalDefine.EmptyBin, 1, 1, VehicleState.TEST_EMPTY_BOOKED);
                        }
                        break;
                    case BIBStageID.BIBStageB:
                        {
                            HDT_BIB_B_PnPInfo = new PnPInfo(TDEx_HDT_BIB_B, PnPStation.BOARD_B, TDEx_Right_Board, GlobalDefine.AllBin, PnPStation.BinBox, TDEx_Temp, GlobalDefine.EmptyBin, 1, 1, VehicleState.TEST_EMPTY_BOOKED);
                        }
                        break;
                    default:
                        {
                            //alarm
                            return FlowChart.FCRESULT.IDLE;
                        }
                        break;
                }
            }

            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart219_Run()
        {
            Flag_HDT_BIB_B_PnP.DoIt();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart220_Run()
        {
            if (Flag_HDT_BIB_B_PnP.IsDone())
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart380_Run()
        {
            HDT_BIB_B_KitShuttleID = KitShuttleID.NONE;
            bool b1 = true;
            switch (HDT_BIB_B_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        if (LeftKitStateControl.IsStateDoIt(VehicleState.TESTED_EMPTY_BOOKED))
                            b1 = LeftKitStateControl.StateDone(LeftKitStateControl.State);

                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        if (RightKitStateControl.IsStateDoIt(VehicleState.TESTED_EMPTY_BOOKED))
                            b1 = RightKitStateControl.StateDone(RightKitStateControl.State);
                    }
                    break;
            }

            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        #endregion
        #region BoardHeadB PnP
        private FlowChart.FCRESULT flowChart157_Run()
        {
            bool b1 = Flag_HDT_BIB_B_PnP.IsDoIt();
            if (b1)
            {
                Flag_HDT_BIB_B_PnP.Doing();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart155_Run()
        {
            bool bDone = HDT_BIB_B_PnPInfo.SourceIsDone;
            if (bDone)
            {
                return FlowChart.FCRESULT.CASE1;
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart159_Run()
        {
            bool bPnPFinish = HDT_BIB_B_PnPInfo.SourceIsDone;
            if (bPnPFinish)
            {
                //bool b1 = true;
                //switch (HDT_BIB_B_PnPInfo.SourceStation)
                //{
                //    case PnPStation.KIT_SHUTTLE_A:
                //        {
                //            if (LeftKitStateControl.IsStateDoIt(VehicleState.UNTESTED_FULL))
                //                b1 = LeftKitStateControl.StateDone(LeftKitStateControl.State);
                //        }
                //        break;
                //    case PnPStation.KIT_SHUTTLE_B:
                //        {
                //            if (RightKitStateControl.IsStateDoIt(VehicleState.UNTESTED_FULL))
                //                b1 = RightKitStateControl.StateDone(RightKitStateControl.State);
                //        }
                //        break;
                //}

                //if (b1)
                {
                    return FlowChart.FCRESULT.CASE1;
                }
                return FlowChart.FCRESULT.IDLE;
            }
            
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart269_Run()
        {
            if (Flag_Booking_B_Error || HDTB_NozzleStateIsFail)
            {
                Flag_Booking_B_Error = false;
                HDTB_NozzleStateIsFail = false;
                return FlowChart.FCRESULT.CASE1;
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart154_Run()
        {
            //HDT_BIB_B_BookingInfo = null;
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart156_Run()
        {
            //bool b1 = false;
            //switch (HDT_BIB_B_KitShuttleID)
            //{
            //    case KitShuttleID.TransferShuttleA:
            //        {
            //            b1 = LeftKitStateControl.StateDoIt(VehicleState.UNTESTED_EMPTY);

            //        }
            //        break;
            //    case KitShuttleID.TransferShuttleB:
            //        {
            //            b1 = RightKitStateControl.StateDoIt(VehicleState.UNTESTED_EMPTY);
            //        }
            //        break;
            //}
            //if (b1)
            {
                Flag_HDT_BIB_B_PnP.Done();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart239_Run()
        {
            //Pick up-lock
            //if (SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD))
            if (HDT_BIB_B_PnPInfo.SourceStation == PnPStation.KIT_SHUTTLE_A || HDT_BIB_B_PnPInfo.SourceStation == PnPStation.KIT_SHUTTLE_B)
            {
                KitShuttleID KitID = KitShuttleID.NONE;
                if (HDT_BIB_B_PnPInfo.SourceStation.Equals(PnPStation.KIT_SHUTTLE_A)) KitID = KitShuttleID.TransferShuttleA;
                if (HDT_BIB_B_PnPInfo.SourceStation.Equals(PnPStation.KIT_SHUTTLE_B)) KitID = KitShuttleID.TransferShuttleB;
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "LockTransferShuttle", (KitShuttleOwner)KitShuttleOwner.HDT_BIB_B, (KitShuttleID)KitID);
                if (T.Equals(ThreeValued.TRUE))
                {
                    bool b2 = Board_BKS.GetKitBlockInfo(KitID, HDT_BIB_B_PnPInfo.Current_VechicleState);
                    if (!b2)
                    {
                        Flag_Booking_B_Error = true;
                        return FlowChart.FCRESULT.CASE1;
                    }
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else
            {
                KitShuttleID KitID = KitShuttleID.NONE;
                if (HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_A)) KitID = KitShuttleID.TransferShuttleA;
                if (HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_B)) KitID = KitShuttleID.TransferShuttleB;
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "LockBIBStage", (BIBStageModuleOwner)BIBStageModuleOwner.HDT_BIB_B, (BIBStageID)HDT_BIB_BIBStageID);
                if (T.Equals(ThreeValued.TRUE))
                {
                    //bool b2 = Board_BKS.GetWorkStageBlockInfo();
                    bool b2 = Board_BKS.GetKitBlockInfo(KitID, HDT_BIB_B_PnPInfo.Current_VechicleState);
                    if (!b2)
                    {
                        Flag_Booking_B_Error = true;
                        return FlowChart.FCRESULT.CASE1;
                    }
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart240_Run()
        {
            Point pos = new Point();
            //Point PickOffset = new Point();
            BasePosInfo Offset = new BasePosInfo();
            BasePosInfo HDT_BIB_B_Basic = new BasePosInfo();
            BasePosInfo BSMBasic = new BasePosInfo();
            BasePosInfo KSMBasic = new BasePosInfo();

            switch (HDT_BIB_B_PnPInfo.SourceStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        Offset = GetOffset(BoardHeadID.HDT_B, PnPStation.KIT_SHUTTLE_A, true);
                        pos = HDT_BIB_B_PnP.CalcKitPnPPos(HDT_BIB_B_PnPInfo.HDTTray, HDT_BIB_B_PnPInfo.SourceTray, Board_BKS.CurrentKitBlockInfo);
                        HDT_BIB_B_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", HDT_BIB_B_PnPInfo.SourceStation, BoardHeadID.HDT_B);
                        KSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleA);
                        HDT_BIB_B_PnPInfo.WorkingPos_X = HDT_BIB_B_Basic .X - (KitInfo.Width / 2) + KitInfo.XO + pos.X * KitInfo.XP + Offset.X;
                        HDT_BIB_B_PnPInfo.WorkingPos_Y = KSMBasic .Y - (KitInfo.Length / 2) + KitInfo.YO + pos.Y * KitInfo.YP + Offset.Y;
                        HDT_BIB_B_PnPInfo.WorkingPos_Z = HDT_BIB_B_Basic.Z + Offset.Z;
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        Offset = GetOffset(BoardHeadID.HDT_B, PnPStation.KIT_SHUTTLE_B, true);
                        pos = HDT_BIB_B_PnP.CalcKitPnPPos(HDT_BIB_B_PnPInfo.HDTTray, HDT_BIB_B_PnPInfo.SourceTray, Board_BKS.CurrentKitBlockInfo);
                        HDT_BIB_B_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", HDT_BIB_B_PnPInfo.SourceStation, BoardHeadID.HDT_B);
                        KSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleB);
                        HDT_BIB_B_PnPInfo.WorkingPos_X = HDT_BIB_B_Basic.X - (KitInfo.Width / 2) + KitInfo.XO + pos.X * KitInfo.XP + Offset.X;
                        HDT_BIB_B_PnPInfo.WorkingPos_Y = KSMBasic.Y - (KitInfo.Length / 2) + KitInfo.YO + pos.Y * KitInfo.YP + Offset.Y;
                        HDT_BIB_B_PnPInfo.WorkingPos_Z = HDT_BIB_B_Basic.Z + Offset.Z;
                    }
                    break;
                case PnPStation.BOARD_A:
                    {
                        Offset = GetOffset(BoardHeadID.HDT_B, PnPStation.BOARD_A, true);
                        pos = HDT_BIB_B_PnP.CalcBoardPnPPos(HDT_BIB_B_PnPInfo.HDTTray, HDT_BIB_B_PnPInfo.SourceTray, Board_BKS.CurrentWorkInfo);
                        HDT_BIB_B_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", PnPStation.BOARD_A, BoardHeadID.HDT_B);
                        BSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_BSM, "GetBasicInfo", BIBStageModuleOwner.HDT_BIB_B, BIBStageID.BIBStageA);
                        HDT_BIB_B_PnPInfo.WorkingPos_X = (pos.X) + (HDT_BIB_B_Basic.X) + BoardInfo.XO - BoardInfo.Width + Offset.X;
                        HDT_BIB_B_PnPInfo.WorkingPos_Y = (pos.Y) + (BSMBasic.Y) + BoardInfo.YO + Offset.Y;
                        HDT_BIB_B_PnPInfo.WorkingPos_Z = HDT_BIB_B_Basic.Z + Offset.Z;
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        Offset = GetOffset(BoardHeadID.HDT_B, PnPStation.BOARD_B, true);
                        pos = HDT_BIB_B_PnP.CalcBoardPnPPos(HDT_BIB_B_PnPInfo.HDTTray, HDT_BIB_B_PnPInfo.SourceTray, Board_BKS.CurrentWorkInfo);
                        HDT_BIB_B_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", PnPStation.BOARD_B, BoardHeadID.HDT_B);
                        BSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_BSM, "GetBasicInfo", BIBStageModuleOwner.HDT_BIB_B, BIBStageID.BIBStageB);
                        HDT_BIB_B_PnPInfo.WorkingPos_X = (pos.X) + (HDT_BIB_B_Basic.X) + BoardInfo.XO - BoardInfo.Width + Offset.X;
                        HDT_BIB_B_PnPInfo.WorkingPos_Y = (pos.Y) + (BSMBasic.Y) + BoardInfo.YO + Offset.Y;
                        HDT_BIB_B_PnPInfo.WorkingPos_Z = HDT_BIB_B_Basic.Z + Offset.Z;
                    }
                    break;
                case PnPStation.PassBox:
                    {
                        HDT_BIB_B_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", PnPStation.PassBox, BoardHeadID.HDT_B);
                        HDT_BIB_B_PnPInfo.WorkingPos_X = HDT_BIB_B_Basic.X;
                        HDT_BIB_B_PnPInfo.WorkingPos_Z = HDT_BIB_B_Basic.Z;
                    }
                    break;
                case PnPStation.BinBox:
                    {
                        HDT_BIB_B_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", PnPStation.BinBox, BoardHeadID.HDT_B);
                        HDT_BIB_B_PnPInfo.WorkingPos_X = HDT_BIB_B_Basic.X;
                        HDT_BIB_B_PnPInfo.WorkingPos_Z = HDT_BIB_B_Basic.Z;
                    }
                    break;

                default:
                    {
                        return FlowChart.FCRESULT.IDLE;
                    }
                    break;
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart238_Run()
        {
            NozzleState[,] states = HDT_BIB_B_PnPInfo.GetBeforePickHeadAction();

            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "SetNozzleState", states, BoardHeadID.HDT_B);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart237_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart236_Run()
        {
            BasePosInfo Pos = new BasePosInfo();
            //Pos = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", HDT_BIB_B_PnPInfo.SourceStation, BoardHeadID.HDT_B);
            Pos.X =HDT_BIB_B_PnPInfo.WorkingPos_X;
            Pos.Y =HDT_BIB_B_PnPInfo.WorkingPos_Y;
            Pos.Z =HDT_BIB_B_PnPInfo.WorkingPos_Z;
            Pos.U =HDT_BIB_B_PnPInfo.WorkingPos_U;

            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_B, ACTIONMODE.MOVE, Pos);// HDT_BIB_B_PnPInfo.SourceStation, ACTIONMODE.MOVE, Pos);

            ThreeValued T = ThreeValued.UNKNOWN;
            switch (HDT_BIB_B_PnPInfo.SourceStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        //Pos = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BIB_B, HDT_BIB_B_KitShuttleID);
                        //Pos.Y = Pos.Y + HDT_BIB_A_PnPInfo.WorkingPos_Y;
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "SetActionCommand_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleA, ACTIONMODE.MOVE, Pos);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        //Pos = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BIB_B, HDT_BIB_B_KitShuttleID);
                        //Pos.Y = Pos.Y + HDT_BIB_A_PnPInfo.WorkingPos_Y;
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "SetActionCommand_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleB, ACTIONMODE.MOVE, Pos);
                    }
                    break;
                case PnPStation.BOARD_A:
                    {
                        //Pos = (BasePosInfo)SYSPara.CallProc(ModuleName_BSM, "GetBasicInfo", BIBStageModuleOwner.HDT_BIB_B, BIBStageID.BIBStageA);
                        //Pos.Y = Pos.Y + HDT_BIB_A_PnPInfo.WorkingPos_Y;
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.HDT_BIB_B, BIBStageID.BIBStageA, ACTIONMODE.MOVE, Pos);//ACTIONMODE.MOVE, HDT_BIB_B_PnPInfo.WorkingPos_Y, HDT_BIB_BIBStageID, "", "");
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        //Pos = (BasePosInfo)SYSPara.CallProc(ModuleName_BSM, "GetBasicInfo", BIBStageModuleOwner.HDT_BIB_B, BIBStageID.BIBStageB);
                        //Pos.Y = Pos.Y + HDT_BIB_A_PnPInfo.WorkingPos_Y;
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.HDT_BIB_B, BIBStageID.BIBStageB, ACTIONMODE.MOVE, Pos);//ACTIONMODE.MOVE, HDT_BIB_B_PnPInfo.WorkingPos_Y, HDT_BIB_BIBStageID, "", "");
                    }
                    break;
            }
            if (T.Equals(ThreeValued.TRUE) && T1.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart235_Run()
        {
            ThreeValued T1 = ThreeValued.UNKNOWN;
            ThreeValued T2 = ThreeValued.UNKNOWN;
            T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_B);

            switch (HDT_BIB_B_PnPInfo.SourceStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "GetActionResult_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleA);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "GetActionResult_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleB);
                    }
                    break;
                case PnPStation.BOARD_A:
                    {

                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageA);
                    }
                    break;
                case PnPStation.BOARD_B:
                    {

                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageB);
                    }
                    break;
                case PnPStation.PassBox:
                case PnPStation.BinBox:
                    {
                        T2 = ThreeValued.TRUE;
                    }
                    break;
            }

            if (T1.Equals(ThreeValued.TRUE) && T2.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart234_Run()
        {
            BasePosInfo Pos = new BasePosInfo();
            
            Pos.X = HDT_BIB_B_PnPInfo.WorkingPos_X;
            Pos.Y = HDT_BIB_B_PnPInfo.WorkingPos_Y;
            Pos.Z = HDT_BIB_B_PnPInfo.WorkingPos_Z;
            Pos.U = HDT_BIB_B_PnPInfo.WorkingPos_U;

            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_B, ACTIONMODE.PICKUP, Pos);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart233_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_B);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart232_Run()
        {
            bool[,] AfterPickPnPState = new bool[HDT_BIB_B_PnPInfo.HEAD_X, HDT_BIB_B_PnPInfo.HEAD_Y];
            NozzleState[,] AfterPickNozzleState = (NozzleState[,])SYSPara.CallProc(ModuleName_HDT, "GetNozzleState", BoardHeadID.HDT_B);
            int CompleteCount = 0;
            int LostCount = 0;

            for (int y = 0; y < HDT_BIB_B_PnPInfo.HEAD_Y; y++)
            {
                for (int x = 0; x < HDT_BIB_B_PnPInfo.HEAD_X; x++)
                {
                    if (AfterPickNozzleState[x, y].Equals(NozzleState.InUsing))
                    {
                        AfterPickPnPState[x, y] = true;
                        CompleteCount++;
                    }
                    else if (AfterPickNozzleState[x, y].Equals(NozzleState.PnPFailure))
                    {
                        AfterPickPnPState[x, y] = false;
                        LostCount++;
                        if (HDT_BIB_B_PnPInfo.SourceStation.Equals(PnPStation.BOARD_A) ||
                            HDT_BIB_B_PnPInfo.SourceStation.Equals(PnPStation.BOARD_B))
                        {
                            HDTB_NozzleStateIsFail = false;
                        }
                    }
                    else
                    {
                        AfterPickPnPState[x, y] = false;
                    }
                }
            }
            bool bRet = false;
            switch (HDT_BIB_B_PnPInfo.SourceStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        bRet = HDT_BIB_B_PnP.KitDataExchange(TDEx_HDT_BIB_B, TDEx_Left_KitShuttle, Board_BKS.CurrentKitBlockInfo, AfterPickPnPState);
                        HDT_BIB_B_BookingInfo = Board_BKS.CurrentKitBlockInfo;
                        Board_BKS.SocketBlockDone(KitShuttleID.TransferShuttleA, HDT_BIB_B_PnPInfo.Current_VechicleState);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        bRet = HDT_BIB_B_PnP.KitDataExchange(TDEx_HDT_BIB_B, TDEx_Right_KitShuttle, Board_BKS.CurrentKitBlockInfo, AfterPickPnPState);
                        HDT_BIB_B_BookingInfo = Board_BKS.CurrentKitBlockInfo;
                        Board_BKS.SocketBlockDone(KitShuttleID.TransferShuttleB, HDT_BIB_B_PnPInfo.Current_VechicleState);
                    }
                    break;
                case PnPStation.BOARD_A:
                    {
                        HDT_BIB_B_PnP.DiePakDataExchange(TDEx_HDT_BIB_B, TDEx_Left_Board, Board_BKS.CurrentWorkInfo, AfterPickPnPState);
                        //HDT_BIB_B_BookingInfo = Board_BKS.CurrentWorkInfo;
                        //Board_BKS.WorkBlockDone();
                        HDT_BIB_B_BookingInfo = Board_BKS.CurrentKitBlockInfo;
                        KitShuttleID kitID = HDT_BIB_B_PnPInfo.TargetStation == PnPStation.KIT_SHUTTLE_A ? KitShuttleID.TransferShuttleA : KitShuttleID.TransferShuttleB;
                        Board_BKS.SocketBlockDone(kitID, HDT_BIB_B_PnPInfo.Current_VechicleState);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Tested, JStatusType.Process, CompleteCount);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Tested, JStatusType.Missing, LostCount);
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        HDT_BIB_B_PnP.DiePakDataExchange(TDEx_HDT_BIB_B, TDEx_Right_Board, Board_BKS.CurrentWorkInfo, AfterPickPnPState);
                        //HDT_BIB_B_BookingInfo = Board_BKS.CurrentWorkInfo;
                        //Board_BKS.WorkBlockDone();
                        HDT_BIB_B_BookingInfo = Board_BKS.CurrentKitBlockInfo;
                        KitShuttleID kitID = HDT_BIB_B_PnPInfo.TargetStation == PnPStation.KIT_SHUTTLE_A ? KitShuttleID.TransferShuttleA : KitShuttleID.TransferShuttleB;
                        Board_BKS.SocketBlockDone(kitID, HDT_BIB_B_PnPInfo.Current_VechicleState);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Tested, JStatusType.Process, CompleteCount);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Tested, JStatusType.Missing, LostCount);
                    }
                    break;

                default:
                    {
                        return FlowChart.FCRESULT.IDLE;
                    }
            }
            
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart231_Run()
        {
            //if (SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD))
            if (HDT_BIB_B_PnPInfo.SourceStation == PnPStation.KIT_SHUTTLE_A || HDT_BIB_B_PnPInfo.SourceStation == PnPStation.KIT_SHUTTLE_B)
            {
                KitShuttleID KitID = KitShuttleID.NONE;
                if (HDT_BIB_B_PnPInfo.SourceStation.Equals(PnPStation.KIT_SHUTTLE_A)) KitID = KitShuttleID.TransferShuttleA;
                if (HDT_BIB_B_PnPInfo.SourceStation.Equals(PnPStation.KIT_SHUTTLE_B)) KitID = KitShuttleID.TransferShuttleB;
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "UnlockTransferShuttle", (KitShuttleOwner)KitShuttleOwner.HDT_BIB_B, (KitShuttleID)KitID);
                if (T.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else
            {
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "UnlockBIBStage", (BIBStageModuleOwner)BIBStageModuleOwner.HDT_BIB_B, (BIBStageID)HDT_BIB_BIBStageID);
                if (T.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart251_Run()
        {
            //if (!SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD))
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            if (HDT_BIB_B_PnPInfo.TargetStation == PnPStation.KIT_SHUTTLE_A || HDT_BIB_B_PnPInfo.TargetStation == PnPStation.KIT_SHUTTLE_B)
            {
                KitShuttleID KitID = KitShuttleID.NONE;
                if (HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_A)) KitID = KitShuttleID.TransferShuttleA;
                if (HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_B)) KitID = KitShuttleID.TransferShuttleB;
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "LockTransferShuttle", (KitShuttleOwner)KitShuttleOwner.HDT_BIB_B, (KitShuttleID)KitID);
                if (T.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else
            {
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "LockBIBStage", (BIBStageModuleOwner)BIBStageModuleOwner.HDT_BIB_B, (BIBStageID)HDT_BIB_BIBStageID);
                if (T.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart250_Run()
        {
            BasePosInfo HDT_BIB_B_Basic = new BasePosInfo();
            BasePosInfo BSMBasic = new BasePosInfo();
            BasePosInfo KSMBasic = new BasePosInfo();
            //Point Placeoffset = new Point();
            BasePosInfo Offset = new BasePosInfo();
            Point pos = new Point();

            switch (HDT_BIB_B_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        Offset = GetOffset(BoardHeadID.HDT_B, PnPStation.KIT_SHUTTLE_A, false);
                        pos = HDT_BIB_B_PnP.CalcKitPnPPos(HDT_BIB_B_PnPInfo.HDTTray, HDT_BIB_B_PnPInfo.TargetTray, Board_BKS.CurrentKitBlockInfo);
                        HDT_BIB_B_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", PnPStation.KIT_SHUTTLE_A, BoardHeadID.HDT_B);
                        KSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleA);
                        float pitchy = (float)(KitInfo.YN) / 2;
                        KSMBasic.Y -= (int)(pitchy * KitInfo.YP);
                        HDT_BIB_B_PnPInfo.WorkingPos_X = pos.X + HDT_BIB_B_Basic.X + Offset.X;
                        HDT_BIB_B_PnPInfo.WorkingPos_Y = pos.Y + KSMBasic.Y + Offset.Y;
                        HDT_BIB_B_PnPInfo.WorkingPos_Z = HDT_BIB_B_Basic.Z + Offset.Z; 
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        Offset = GetOffset(BoardHeadID.HDT_B, PnPStation.KIT_SHUTTLE_B, false);
                        pos = HDT_BIB_B_PnP.CalcKitPnPPos(HDT_BIB_B_PnPInfo.HDTTray, HDT_BIB_B_PnPInfo.TargetTray, Board_BKS.CurrentKitBlockInfo);
                        HDT_BIB_B_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", PnPStation.KIT_SHUTTLE_B, BoardHeadID.HDT_B);
                        KSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleB);
                        float pitchy = (float)(KitInfo.YN) / 2;
                        KSMBasic.Y -= (int)(pitchy * KitInfo.YP);
                        HDT_BIB_B_PnPInfo.WorkingPos_X = pos.X + HDT_BIB_B_Basic.X + Offset.X;
                        HDT_BIB_B_PnPInfo.WorkingPos_Y = pos.Y + KSMBasic.Y + Offset.Y;
                        HDT_BIB_B_PnPInfo.WorkingPos_Z = HDT_BIB_B_Basic.Z + Offset.Z;
                    }
                    break;
                case PnPStation.BOARD_A:
                    {
                        Offset = GetOffset(BoardHeadID.HDT_B, PnPStation.BOARD_A, false);
                        //pos = HDT_BIB_B_PnP.CalcBoardPnPPos(HDT_BIB_B_PnPInfo.HDTTray, HDT_BIB_B_PnPInfo.TargetTray, Board_BKS.CurrentKitBlockInfo);
                        pos = HDT_BIB_B_PnP.CalcBoardPnPPos(HDT_BIB_B_PnPInfo.HDTTray, HDT_BIB_B_PnPInfo.TargetTray, HDT_BIB_B_BookingInfo);
                        HDT_BIB_B_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", PnPStation.BOARD_A, BoardHeadID.HDT_B);
                        // DPMBasic.Y = HDT_DP_Basic.X;
                        BSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_BSM, "GetBasicInfo", BIBStageModuleOwner.HDT_BIB_B, BIBStageID.BIBStageA);
                        HDT_BIB_B_PnPInfo.WorkingPos_X = pos.X + HDT_BIB_B_Basic.X + Offset.X + BoardInfo.XO - BoardInfo.Width;
                        HDT_BIB_B_PnPInfo.WorkingPos_Y = pos.Y + BSMBasic.Y + Offset.Y + BoardInfo.YO;
                        HDT_BIB_B_PnPInfo.WorkingPos_Z = HDT_BIB_B_Basic.Z + Offset.Z;
                        //if (true)
                        //{
                        //    PnPStation TargetStation = PnPStation.PassBox;
                        //    //if (HDT_BIB_B_BookingInfo.Map.Equals("2")) TargetStation = PnPStation.BinBox;
                        //    pos = HDT_BIB_B_PnP.CalcBoxPnPPos(HDT_BIB_B_PnPInfo.HDTTray, HDT_BIB_B_PnPInfo.TargetTray);
                        //    HDT_BIB_B_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", TargetStation, BoardHeadID.HDT_B);
                        //    HDT_BIB_B_PnPInfo.WorkingPos_X = HDT_BIB_B_Basic.X;
                        //    HDT_BIB_B_PnPInfo.WorkingPos_Z = HDT_BIB_B_Basic.Z;
                        //}
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        Offset = GetOffset(BoardHeadID.HDT_B, PnPStation.BOARD_B, false);
                        //pos = HDT_BIB_B_PnP.CalcBoardPnPPos(HDT_BIB_B_PnPInfo.HDTTray, HDT_BIB_B_PnPInfo.TargetTray, Board_BKS.CurrentKitBlockInfo);
                        pos = HDT_BIB_B_PnP.CalcBoardPnPPos(HDT_BIB_B_PnPInfo.HDTTray, HDT_BIB_B_PnPInfo.TargetTray, HDT_BIB_B_BookingInfo);
                        HDT_BIB_B_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", PnPStation.BOARD_B, BoardHeadID.HDT_B);
                        // DPMBasic.Y = HDT_DP_Basic.X;
                        BSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_BSM, "GetBasicInfo", BIBStageModuleOwner.HDT_BIB_B, BIBStageID.BIBStageB);
                        HDT_BIB_B_PnPInfo.WorkingPos_X = pos.X + HDT_BIB_B_Basic.X + Offset.X + BoardInfo.XO - BoardInfo.Width;
                        HDT_BIB_B_PnPInfo.WorkingPos_Y = pos.Y + BSMBasic.Y + Offset.Y + BoardInfo.YO;
                        HDT_BIB_B_PnPInfo.WorkingPos_Z = HDT_BIB_B_Basic.Z + Offset.Z;
                        //if (true)
                        //{
                        //    PnPStation TargetStation = PnPStation.PassBox;
                        //    //if (HDT_BIB_B_BookingInfo.Map.Equals("2")) TargetStation = PnPStation.BinBox;
                        //    pos = HDT_BIB_B_PnP.CalcBoxPnPPos(HDT_BIB_B_PnPInfo.HDTTray, HDT_BIB_B_PnPInfo.TargetTray);
                        //    HDT_BIB_B_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", TargetStation, BoardHeadID.HDT_B);
                        //    HDT_BIB_B_PnPInfo.WorkingPos_X = HDT_BIB_B_Basic.X;
                        //    HDT_BIB_B_PnPInfo.WorkingPos_Z = HDT_BIB_B_Basic.Z;
                        //}
                    }
                    break;
                case PnPStation.PassBox:
                case PnPStation.BinBox:
                    {
                        PnPStation TargetStation = PnPStation.PassBox;
                        if (!HDT_BIB_B_BookingInfo.Map.Equals("1")) TargetStation = PnPStation.BinBox;
                        pos = HDT_BIB_B_PnP.CalcBoxPnPPos(HDT_BIB_B_PnPInfo.HDTTray, HDT_BIB_B_PnPInfo.TargetTray);
                        HDT_BIB_B_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", TargetStation, BoardHeadID.HDT_B);
                        HDT_BIB_B_PnPInfo.WorkingPos_X = HDT_BIB_B_Basic.X;
                        HDT_BIB_B_PnPInfo.WorkingPos_Z = HDT_BIB_B_Basic.Z;
                    }
                    break;

                default:
                    {
                        return FlowChart.FCRESULT.IDLE;
                    }
                    break;
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart249_Run()
        {
            NozzleState[,] PnPState = HDT_BIB_B_PnPInfo.GetBeforePlaceHeadAction();
            bool T = (bool)SYSPara.CallProc(ModuleName_HDT, "SetNozzleState", PnPState, BoardHeadID.HDT_B);
            if (T)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart322_Run()
        {
            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_B);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            BasePosInfo pos = new BasePosInfo();
            int Offset = (int)SYSPara.CallProc(ModuleName_HDT, "GetNozzleCCDOffset", BoardHeadID.HDT_B);
            int OffsetY = (int)SYSPara.CallProc(ModuleName_HDT, "GetNozzleCCDOffsetY", BoardHeadID.HDT_B);
            pos.X = HDT_BIB_B_PnPInfo.WorkingPos_X + Offset;
            pos.Y = HDT_BIB_B_PnPInfo.WorkingPos_Y + OffsetY;
            pos.Z = HDT_BIB_B_PnPInfo.WorkingPos_Z;
            pos.U = HDT_BIB_B_PnPInfo.WorkingPos_U;

            ThreeValued T1 = ThreeValued.TRUE;
            ThreeValued T2 = ThreeValued.TRUE;
            bool bFocus = true;
            switch (HDT_BIB_B_PnPInfo.TargetStation)
            {
                case PnPStation.BOARD_A:
                    {
                        T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_B, ACTIONMODE.MOVE, pos);
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.HDT_BIB_B, BIBStageID.BIBStageA, ACTIONMODE.MOVE, pos);
                        bFocus = (bool)SYSPara.CallProc(ModuleName_HDT, "SetVisionFocus", BoardHeadID.HDT_B, BIBStageID.BIBStageA);
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_B, ACTIONMODE.MOVE, pos);
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.HDT_BIB_B, BIBStageID.BIBStageB, ACTIONMODE.MOVE, pos);
                        bFocus = (bool)SYSPara.CallProc(ModuleName_HDT, "SetVisionFocus", BoardHeadID.HDT_B, BIBStageID.BIBStageB);
                    }
                    break;
            }
            if (T1.Equals(ThreeValued.TRUE) && T2.Equals(ThreeValued.TRUE) && bFocus)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart321_Run()
        {
            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_B);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            ThreeValued T1 = ThreeValued.TRUE;
            ThreeValued T2 = ThreeValued.TRUE;

            switch (HDT_BIB_B_PnPInfo.TargetStation)
            {
                case PnPStation.BOARD_A:
                    {
                        T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_B);
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageA);
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_B);
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageB);
                    }
                    break;
            }

            if (T1.Equals(ThreeValued.TRUE) && T2.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart315_Run()
        {
            if (!HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.BOARD_A) && !HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.BOARD_B))
                return FlowChart.FCRESULT.NEXT;

            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_B);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            BasePosInfo pos = new BasePosInfo();
            pos.X = HDT_BIB_B_PnPInfo.WorkingPos_X;
            pos.Y = HDT_BIB_B_PnPInfo.WorkingPos_Y;
            pos.Z = HDT_BIB_B_PnPInfo.WorkingPos_Z;
            pos.U = HDT_BIB_B_PnPInfo.WorkingPos_U;
            pos.ColIndex = HDT_BIB_B_BookingInfo.SocketX;
            pos.RowIndex = HDT_BIB_B_BookingInfo.SocketY;
            pos.BoardCount = BoardCount + 1;

            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_B, ACTIONMODE.GRAB_CCD, pos);
            if (T1.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart316_Run()
        {
            if (!HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.BOARD_A) && !HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.BOARD_B))
                return FlowChart.FCRESULT.NEXT;

            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_B);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_B);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart317_Run()
        {
            bLoadToBox_B = false;
            if (!HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.BOARD_A) && !HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.BOARD_B))
                return FlowChart.FCRESULT.NEXT;

            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_B);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            InspectionState state = InspectionState.Unknow;
            if (HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.BOARD_A) || HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.BOARD_B))
            {
                state = (InspectionState)SYSPara.CallProc(ModuleName_HDT, "GetInspectState", BoardHeadID.HDT_B);
                switch (state)
                {
                    case InspectionState.Success:   //有IC 上板未放置有IC為異常
                        {
                            //Alarm
                            return FlowChart.FCRESULT.CASE1;
                        }
                        break;
                    case InspectionState.Fail:  //無IC
                        {
                            return FlowChart.FCRESULT.NEXT;
                        }
                        break;
                    case InspectionState.Error: //無Socket 丟到垃圾桶
                        {
                            Point pos = new Point();
                            BasePosInfo HDT_BIB_B_Basic = new BasePosInfo();
                            PnPStation TargetStation = PnPStation.BinBox;
                            pos = HDT_BIB_B_PnP.CalcBoxPnPPos(HDT_BIB_B_PnPInfo.HDTTray, HDT_BIB_B_PnPInfo.TargetTray);
                            HDT_BIB_B_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetBasicInfo", TargetStation, BoardHeadID.HDT_B);
                            HDT_BIB_B_PnPInfo.WorkingPos_X = HDT_BIB_B_Basic.X;
                            HDT_BIB_B_PnPInfo.WorkingPos_Z = HDT_BIB_B_Basic.Z;
                            bLoadToBox_B = true;
                            return FlowChart.FCRESULT.NEXT;
                        }
                        break;
                }
                return FlowChart.FCRESULT.IDLE;
            }
            else
            {
                return FlowChart.FCRESULT.NEXT;
            }
        }

        private FlowChart.FCRESULT flowChart318_Run()
        {
            string code = string.Format("{0}{1:0000}", "E", "0901");
            string args = "Board Head B";
            SYSPara.Alarm.Say(code, args);
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart314_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart242_Run()
        {
            BasePosInfo pos = new BasePosInfo();
            BasePosInfo InspectOffset = new BasePosInfo();
            int OffsetY = 0;
            if (HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_B) || HDT_BIB_A_PnPInfo.TargetStation.Equals(PnPStation.BOARD_A))
            {
                InspectOffset = (BasePosInfo)SYSPara.CallProc(ModuleName_HDT, "GetInspectResult", BoardHeadID.HDT_B);
                
                bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_B);
                if (b1)
                {
                    OffsetY = (int)SYSPara.CallProc(ModuleName_HDT, "GetNozzleCCDOffsetY", BoardHeadID.HDT_B);
                    //InspectOffset.Y = InspectOffset.Y + OffsetY;
                }
            }
            pos.X = HDT_BIB_B_PnPInfo.WorkingPos_X + InspectOffset.X;
            pos.Y = HDT_BIB_B_PnPInfo.WorkingPos_Y - InspectOffset.Y - OffsetY;
            pos.Z = HDT_BIB_B_PnPInfo.WorkingPos_Z + InspectOffset.Z;
            pos.U = HDT_BIB_B_PnPInfo.WorkingPos_U + InspectOffset.U;
            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_B, ACTIONMODE.MOVE,pos);// HDT_BIB_B_PnPInfo.TargetStation, ACTIONMODE.PLACEMENT, Pos);

            ThreeValued T = ThreeValued.UNKNOWN;
            switch (HDT_BIB_B_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "SetActionCommand_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleA, ACTIONMODE.MOVE, pos);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "SetActionCommand_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleB, ACTIONMODE.MOVE, pos);
                    }
                    break;
                case PnPStation.BOARD_A:
                    {
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.HDT_BIB_B, BIBStageID.BIBStageA, ACTIONMODE.MOVE, pos);//ACTIONMODE.MOVE, HDT_BIB_B_PnPInfo.WorkingPos_Y, HDT_BIB_BIBStageID, "", "");
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.HDT_BIB_B, BIBStageID.BIBStageB, ACTIONMODE.MOVE, pos);//ACTIONMODE.MOVE, HDT_BIB_B_PnPInfo.WorkingPos_Y, HDT_BIB_BIBStageID, "", "");
                    }
                    break;
                case PnPStation.PassBox:
                    {
                        T = ThreeValued.TRUE;
                    }
                    break;
                case PnPStation.BinBox:
                    {
                        T = ThreeValued.TRUE;
                    }
                    break;
            }
            if (T.Equals(ThreeValued.TRUE) && T1.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE; 
        }

        private FlowChart.FCRESULT flowChart243_Run()
        {
            ThreeValued T1 = ThreeValued.UNKNOWN;
            ThreeValued T2 = ThreeValued.UNKNOWN;
            T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_B);

            switch (HDT_BIB_B_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "GetActionResult_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleA);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "GetActionResult_KSM", KitShuttleOwner.HDT_BIB_B, KitShuttleID.TransferShuttleB);
                    }
                    break;
                case PnPStation.BOARD_A:
                    {

                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageA);
                    }
                    break;
                case PnPStation.BOARD_B:
                    {

                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageB);
                    }
                    break;
                case PnPStation.PassBox:
                case PnPStation.BinBox:
                    {
                        T2 = ThreeValued.TRUE;
                    }
                    break;
            }

            if (T1.Equals(ThreeValued.TRUE) && T2.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
            //ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", HDT_BIB_BIBStageID);
            //if (T.Equals(ThreeValued.TRUE))
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart244_Run()
        {
            BasePosInfo Pos = new BasePosInfo();
            
            Pos.X = HDT_BIB_B_PnPInfo.WorkingPos_X;
            Pos.Y = HDT_BIB_B_PnPInfo.WorkingPos_Y;
            Pos.Z = HDT_BIB_B_PnPInfo.WorkingPos_Z;
            Pos.U = HDT_BIB_B_PnPInfo.WorkingPos_U;

            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_B, ACTIONMODE.PLACEMENT, Pos);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart245_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_B);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart246_Run()
        {
            bool[,] AfterPlacePnPState = new bool[HDT_BIB_B_PnPInfo.HEAD_X, HDT_BIB_B_PnPInfo.HEAD_Y];
            NozzleState[,] PnPState = (NozzleState[,])SYSPara.CallProc(ModuleName_HDT, "GetNozzleState", BoardHeadID.HDT_B);
            int CompleteCount = 0;
            int LostCount = 0;
            for (int y = 0; y < HDT_BIB_B_PnPInfo.HEAD_Y; y++)
            {
                for (int x = 0; x < HDT_BIB_B_PnPInfo.HEAD_X; x++)
                {
                    if (PnPState[x, y].Equals(NozzleState.InUsing))
                    {
                        
                        AfterPlacePnPState[x, y] = true;
                        CompleteCount++;
                        
                    }
                    else if (PnPState[x, y].Equals(NozzleState.ICLost))
                    {
                        AfterPlacePnPState[x, y] = false;
                        LostCount++;
                    }
                    else
                    {
                        AfterPlacePnPState[x, y] = false;
                        LostCount++;
                    }
                }
            }

            bool bRet = false;
            switch (HDT_BIB_B_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        bRet = HDT_BIB_B_PnP.KitDataExchange(TDEx_HDT_BIB_B, TDEx_Right_KitShuttle, Board_BKS.CurrentKitBlockInfo, AfterPlacePnPState);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        bRet = HDT_BIB_B_PnP.KitDataExchange(TDEx_HDT_BIB_B, TDEx_Left_KitShuttle, Board_BKS.CurrentKitBlockInfo, AfterPlacePnPState);
                    }
                    break;
                case PnPStation.BOARD_A:
                    {
                        //bRet = HDT_BIB_B_PnP.DiePakDataExchange(TDEx_HDT_BIB_B, TDEx_Left_Board, Board_BKS.CurrentKitBlockInfo, AfterPlacePnPState);
                        bRet = HDT_BIB_B_PnP.DiePakDataExchange(TDEx_HDT_BIB_B, TDEx_Left_Board, HDT_BIB_B_BookingInfo, AfterPlacePnPState);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Untested, JStatusType.Completed, CompleteCount);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Untested, JStatusType.Missing, LostCount);
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        bRet = HDT_BIB_B_PnP.DiePakDataExchange(TDEx_HDT_BIB_B, TDEx_Right_Board, HDT_BIB_B_BookingInfo, AfterPlacePnPState);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Untested, JStatusType.Completed, CompleteCount);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Untested, JStatusType.Completed, LostCount);
                    }
                    break;
                case PnPStation.PassBox:
                case PnPStation.BinBox:
                    {
                        TrayDataEx TDEx_Target = TDEx_PassBox;
                        if (HDT_BIB_B_BookingInfo.Map.Equals("1"))
                        {
                            GlobalDefine.OEE.UpdateBinQty(BinDefine.Bin1, JStatusType.Completed, CompleteCount);
                        }
                        else
                        {

                            //TDEx_Target = TDEx_FailBox;
                            //GlobalDefine.OEE.UpdateBinQty(BinDefine.Bin2, JStatusType.Completed, CompleteCount);
                            TDEx_Target = TDEx_FailBox;
                            BinDefine temp = GetBinDefine(HDT_BIB_B_BookingInfo.Map);

                            GlobalDefine.OEE.UpdateBinQty(temp, JStatusType.Completed, CompleteCount);
                        }
                        bRet = HDT_BIB_B_PnP.DiePakDataExchange(TDEx_HDT_BIB_B, TDEx_Target, HDT_BIB_B_BookingInfo, AfterPlacePnPState);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Tested, JStatusType.Completed, CompleteCount);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Tested, JStatusType.Missing, LostCount);

                        TDEx_PassBox.BinReplace((byte)BinDefine.Bin1, (byte)BinDefine.Empty);
                        TDEx_FailBox.BinReplace((byte)BinDefine.Bin1, (byte)BinDefine.Empty);
                    }
                    break;
                default:
                    {
                        return FlowChart.FCRESULT.IDLE;
                    }
                    break;
            }
            if (bRet)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart320_Run()
        {
            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_B);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            if (bLoadToBox_B) return FlowChart.FCRESULT.NEXT;

            BasePosInfo pos = new BasePosInfo();
            int Offset = (int)SYSPara.CallProc(ModuleName_HDT, "GetNozzleCCDOffset", BoardHeadID.HDT_B);
            int OffsetY = (int)SYSPara.CallProc(ModuleName_HDT, "GetNozzleCCDOffsetY", BoardHeadID.HDT_B);
            pos.X = HDT_BIB_B_PnPInfo.WorkingPos_X + Offset;
            pos.Y = HDT_BIB_B_PnPInfo.WorkingPos_Y + OffsetY;
            pos.Z = HDT_BIB_B_PnPInfo.WorkingPos_Z;
            pos.U = HDT_BIB_B_PnPInfo.WorkingPos_U;

            ThreeValued T1 = ThreeValued.TRUE;
            ThreeValued T2 = ThreeValued.TRUE;
            switch (HDT_BIB_B_PnPInfo.TargetStation)
            {
                case PnPStation.BOARD_A:
                    {
                        T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_B, ACTIONMODE.MOVE, pos);
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.HDT_BIB_B, BIBStageID.BIBStageA, ACTIONMODE.MOVE, pos);
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_B, ACTIONMODE.MOVE, pos);
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "SetActionCommand_BSM", BIBStageModuleOwner.HDT_BIB_B, BIBStageID.BIBStageB, ACTIONMODE.MOVE, pos);
                    }
                    break;
            }
            if (T1.Equals(ThreeValued.TRUE) && T2.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart319_Run()
        {
            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_B);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            if (bLoadToBox_B) return FlowChart.FCRESULT.NEXT;

            ThreeValued T1 = ThreeValued.TRUE;
            ThreeValued T2 = ThreeValued.TRUE;

            switch (HDT_BIB_B_PnPInfo.TargetStation)
            {
                case PnPStation.BOARD_A:
                    {
                        T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_B);
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageA);
                    }
                    break;
                case PnPStation.BOARD_B:
                    {
                        T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_B);
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "GetActionResult_BSM", BIBStageID.BIBStageB);
                    }
                    break;
            }

            if (T1.Equals(ThreeValued.TRUE) && T2.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart310_Run()
        {
            if (!HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.BOARD_A) && !HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.BOARD_B))
                return FlowChart.FCRESULT.NEXT;

            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_B);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            if (bLoadToBox_B) return FlowChart.FCRESULT.NEXT;

            BasePosInfo pos = new BasePosInfo();
            pos.X = HDT_BIB_B_PnPInfo.WorkingPos_X;
            pos.Y = HDT_BIB_B_PnPInfo.WorkingPos_Y;
            pos.Z = HDT_BIB_B_PnPInfo.WorkingPos_Z;
            pos.U = HDT_BIB_B_PnPInfo.WorkingPos_U;
            pos.ColIndex = HDT_BIB_B_BookingInfo.SocketX;
            pos.RowIndex = HDT_BIB_B_BookingInfo.SocketY;
            pos.BoardCount = BoardCount + 1;
            ThreeValued T1 = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "SetActionCommand_HDT", BoardHeadID.HDT_B, ACTIONMODE.GRAB_CCD_AF, pos);
            if (T1.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart311_Run()
        {
            if (!HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.BOARD_A) && !HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.BOARD_B))
                return FlowChart.FCRESULT.NEXT;

            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_B);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            if (bLoadToBox_B) return FlowChart.FCRESULT.NEXT;

            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_HDT, "GetActionResult_HDT", BoardHeadID.HDT_B);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart312_Run()
        {
            if (!HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.BOARD_A) && !HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.BOARD_B))
                return FlowChart.FCRESULT.NEXT;

            bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "bUseCCD", BoardHeadID.HDT_B);
            if (!b1) return FlowChart.FCRESULT.NEXT;

            if (bLoadToBox_B) return FlowChart.FCRESULT.NEXT;

            InspectionState state = InspectionState.Unknow;
            if (HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.BOARD_A) || HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.BOARD_B))
            {
                state = (InspectionState)SYSPara.CallProc(ModuleName_HDT, "GetInspectState", BoardHeadID.HDT_B);
                switch (state)
                {
                    case InspectionState.Success:   //上板後有IC正常
                        {
                            return FlowChart.FCRESULT.NEXT;
                        }
                        break;
                    case InspectionState.Fail:  //上板後無IC異常
                    case InspectionState.Error: //異常
                        {
                            //Alarm CCD 檢測異常
                            return FlowChart.FCRESULT.CASE1;    //重新拍攝
                        }
                        break;
                }
                return FlowChart.FCRESULT.IDLE;
            }
            else
            {
                return FlowChart.FCRESULT.NEXT;
            }
        }

        private FlowChart.FCRESULT flowChart313_Run()
        {
            string code = string.Format("{0}{1:0000}", "E", "0901");
            string args = "Board Head B";
            SYSPara.Alarm.Say(code, args);
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart247_Run()
        {
            //if (!SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD))
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            if (HDT_BIB_B_PnPInfo.TargetStation == PnPStation.KIT_SHUTTLE_A || HDT_BIB_B_PnPInfo.TargetStation == PnPStation.KIT_SHUTTLE_B)
            {
                KitShuttleID KitID = KitShuttleID.NONE;
                if (HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_A)) KitID = KitShuttleID.TransferShuttleA;
                if (HDT_BIB_B_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_B)) KitID = KitShuttleID.TransferShuttleB;
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "UnlockTransferShuttle", (KitShuttleOwner)KitShuttleOwner.HDT_BIB_B, (KitShuttleID)KitID);
                if (T.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else
            {
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BSM, "UnlockBIBStage", (BIBStageModuleOwner)BIBStageModuleOwner.HDT_BIB_B, (BIBStageID)HDT_BIB_BIBStageID);
                if (T.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }
        #endregion
        #endregion

        #region Shuttle A
        private FlowChart.FCRESULT FC_ShuttleA_Run()
        {
            return FlowChart.FCRESULT.NEXT;
            //if (SYSPara.Lotend)
            //{
            //    Flag_LeftKitShuttle_LotEnd.DoIt();
            //    return FlowChart.FCRESULT.CASE1;
            //}

            //bool b = (bool)SYSPara.CallProc(ModuleName_KSM, "IsUseShuttle", KitShuttleID.TransferShuttleA);
            //if (!b)
            //{
            //    return FlowChart.FCRESULT.IDLE;
            //}

            //bool b1 = LeftKitStateControl.StateDoIt(VehicleState.UNTESTED_EMPTY);
            //if (b1)
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart360_Run()
        {
            //系統結批且作業完成
            if (SYSPara.Lotend && Flag_LeftKitShuttle_LotEnd.IsDoIt())
            {
                int BookingCount = Board_BKS.BoardBookingListCount();
                if (BookingCount <= 0)
                {
                    return FlowChart.FCRESULT.CASE2;
                }
            }
            //未開啟模組
            bool b = (bool)SYSPara.CallProc(ModuleName_KSM, "IsUseShuttle", KitShuttleID.TransferShuttleA);
            if (!b)
            {
                Flag_LeftKitShuttle_LotEnd.DoIt();
                return FlowChart.FCRESULT.CASE2;
            }

            if (SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD))
            {
                if (LeftKitStateControl.IsStateDone(VehicleState.NONE))
                {
                    LeftKitStateControl.StateDoIt(VehicleState.UNTESTED_EMPTY);
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart105_Run()
        {

            //bool b1 = RightKitStateControl.StateDoIt(VehicleState.UNTESTED_EMPTY);
            bool b1 = LeftKitStateControl.IsStateDoIt(VehicleState.UNTESTED_EMPTY);
            //if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart253_Run()
        {
            if (SYSPara.Lotend && Flag_LeftKitShuttle_LotEnd.IsDoIt())
            {
                int BookingCount = Board_BKS.BoardBookingListCount();
                if (BookingCount <= 0)
                {
                    return FlowChart.FCRESULT.CASE1;
                }
            }

            bool b1 = LeftKitStateControl.IsStateDone(VehicleState.UNTESTED_EMPTY);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart106_Run()
        {
            bool b1 = LeftKitStateControl.StateDoIt(VehicleState.UNTESTED_EMPTY_BOOKED);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart254_Run()
        {
            bool b1 = LeftKitStateControl.IsStateDone(VehicleState.UNTESTED_EMPTY_BOOKED);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart107_Run()
        {
            bool b1 = LeftKitStateControl.StateDoIt(VehicleState.UNTESTED_FULL);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart255_Run()
        {
            bool b1 = LeftKitStateControl.IsStateDone(VehicleState.UNTESTED_FULL);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart111_Run()
        {
            //bool b1 = LeftKitStateControl.StateDoIt(VehicleState.UNTESTED_EMPTY);
            bool b1 = LeftKitStateControl.StateDoIt(VehicleState.NONE);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart361_Run()
        {
            //系統結批且作業完成
            if (SYSPara.Lotend && Flag_LeftKitShuttle_LotEnd.IsDoIt())
            {
                int BookingCount = Board_BKS.BoardBookingListCount();
                if (BookingCount <= 0)
                {
                    return FlowChart.FCRESULT.CASE2;
                }
            }
            //未開啟模組
            bool b = (bool)SYSPara.CallProc(ModuleName_KSM, "IsUseShuttle", KitShuttleID.TransferShuttleA);
            if (!b)
            {
                Flag_LeftKitShuttle_LotEnd.DoIt();
                return FlowChart.FCRESULT.CASE2;
            }

            if (SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.UNLOAD))
            {
                if (LeftKitStateControl.IsStateDone(VehicleState.NONE))
                {
                    LeftKitStateControl.StateDoIt(VehicleState.TESTED_EMPTY);
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart112_Run()
        {
            bool b1 = LeftKitStateControl.IsStateDoIt(VehicleState.TESTED_EMPTY);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart362_Run()
        {
            if (SYSPara.Lotend && Flag_LeftKitShuttle_LotEnd.IsDoIt())
            {
                int BookingCount = Board_BKS.BoardBookingListCount();
                if (BookingCount <= 0)
                {
                    return FlowChart.FCRESULT.CASE1;
                }
            }

            bool b1 = LeftKitStateControl.IsStateDone(VehicleState.TESTED_EMPTY);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart363_Run()
        {
            bool b1 = LeftKitStateControl.StateDoIt(VehicleState.TESTED_EMPTY_BOOKED);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart364_Run()
        {
            bool b1 = LeftKitStateControl.IsStateDone(VehicleState.TESTED_EMPTY_BOOKED);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart365_Run()
        {
            bool b1 = LeftKitStateControl.StateDoIt(VehicleState.TESTED_FULL);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart359_Run()
        {
            bool b1 = LeftKitStateControl.IsStateDone(VehicleState.TESTED_FULL);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart366_Run()
        {
            bool b1 = LeftKitStateControl.StateDoIt(VehicleState.NONE);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart108_Run()
        {
            //if (Flag_RightKitShuttle_LotEnd.IsDoIt() && Flag_LeftKitShuttle_LotEnd.IsDoIt())
            //{
            //    mKSM.SetCanLotEnd();
            //    return FlowChart.FCRESULT.NEXT;
            //}
            if (Flag_LeftKitShuttle_LotEnd.IsDoIt())
            {
                Flag_LeftKitShuttle_LotEnd.Doing();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart109_Run()
        {
            //if (mKSM.mLotendOk)
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}

            //會進來Flowchart109 表示Flag_LeftKitShuttle一定是Doing
            if (/*Flag_LeftKitShuttle_LotEnd.IsDoing() && */Flag_RightKitShuttle_LotEnd.IsDoing())
            {
                mKSM.SetCanLotEnd();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart110_Run()
        {
            //if (mKSM.mLotendOk)
            {
                Flag_LeftKitShuttle_LotEnd.Done();
                Flag_RightKitShuttle_LotEnd.Done();
            }
            return FlowChart.FCRESULT.IDLE;
        }
        #endregion Shuttle A
        #region Shuttle B
        private FlowChart.FCRESULT FC_ShuttleB_Run()
        {
            return FlowChart.FCRESULT.NEXT;
            //if (SYSPara.Lotend)
            //{
            //    Flag_RightKitShuttle_LotEnd.DoIt();
            //    return FlowChart.FCRESULT.CASE1;
            //}

            //bool b = (bool)SYSPara.CallProc(ModuleName_KSM, "IsUseShuttle", KitShuttleID.TransferShuttleB);
            //if (!b)
            //{
            //    return FlowChart.FCRESULT.IDLE;
            //}

            //bool b1 = RightKitStateControl.StateDoIt(VehicleState.UNTESTED_EMPTY);
            //if (b1)
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart367_Run()
        {
            //系統結批且作業完成

            if (SYSPara.Lotend && Flag_RightKitShuttle_LotEnd.IsDoIt())
            {
                int BookingCount = Board_BKS.BoardBookingListCount();
                if (BookingCount <= 0)
                {
                    return FlowChart.FCRESULT.CASE2;
                }
            }
            //未開啟模組
            bool b = (bool)SYSPara.CallProc(ModuleName_KSM, "IsUseShuttle", KitShuttleID.TransferShuttleB);
            if (!b)
            {
                Flag_RightKitShuttle_LotEnd.DoIt();
                return FlowChart.FCRESULT.CASE2;
            }

            if (SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.UNLOAD))
            {
                if (RightKitStateControl.IsStateDone(VehicleState.NONE))
                {
                    RightKitStateControl.StateDoIt(VehicleState.TESTED_EMPTY);
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart113_Run()
        {
            //系統結批且作業完成
            if (SYSPara.Lotend && Flag_RightKitShuttle_LotEnd.IsDoIt())
            {
                int BookingCount = Board_BKS.BoardBookingListCount();
                if (BookingCount <= 0)
                {
                    return FlowChart.FCRESULT.CASE2;
                }
            }
            //未開啟模組
            bool b = (bool)SYSPara.CallProc(ModuleName_KSM, "IsUseShuttle", KitShuttleID.TransferShuttleB);
            if (!b)
            {
                Flag_RightKitShuttle_LotEnd.DoIt();
                return FlowChart.FCRESULT.CASE2;
            }

            if (SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.LOAD))
            {
                if (RightKitStateControl.IsStateDone(VehicleState.NONE))
                {
                    RightKitStateControl.StateDoIt(VehicleState.UNTESTED_EMPTY);
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart114_Run()
        {
            //bool b1 = LeftKitStateControl.StateDoIt(VehicleState.UNTESTED_EMPTY);
            bool b1 = RightKitStateControl.IsStateDoIt(VehicleState.UNTESTED_EMPTY);
            //if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart258_Run()
        {
            //if (SYSPara.Lotend && SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.UNLOAD))
            //{
            //    Flag_RightKitShuttle_LotEnd.DoIt();
            //    return FlowChart.FCRESULT.CASE1;
            //}

            if (SYSPara.Lotend && Flag_RightKitShuttle_LotEnd.IsDoIt())
            {
                int bookingcount = Board_BKS.BoardBookingListCount();
                if (bookingcount <= 0)
                {
                    return FlowChart.FCRESULT.CASE1;
                }
            }

            bool b1 = RightKitStateControl.IsStateDone(VehicleState.UNTESTED_EMPTY);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart117_Run()
        {
            bool b1 = RightKitStateControl.StateDoIt(VehicleState.UNTESTED_EMPTY_BOOKED);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart257_Run()
        {
            bool b1 = RightKitStateControl.IsStateDone(VehicleState.UNTESTED_EMPTY_BOOKED);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart118_Run()
        {
            bool b1 = RightKitStateControl.StateDoIt(VehicleState.UNTESTED_FULL);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart256_Run()
        {
            bool b1 = RightKitStateControl.IsStateDone(VehicleState.UNTESTED_FULL);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart119_Run()
        {
            bool b1 = RightKitStateControl.StateDoIt(VehicleState.NONE);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart368_Run()
        {
            bool b1 = RightKitStateControl.IsStateDoIt(VehicleState.TESTED_EMPTY);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart369_Run()
        {
            if (SYSPara.Lotend && Flag_RightKitShuttle_LotEnd.IsDoIt())
            {
                int bookingcount = Board_BKS.BoardBookingListCount();
                if (bookingcount <= 0)
                {
                    return FlowChart.FCRESULT.CASE1;
                }
            }

            bool b1 = RightKitStateControl.IsStateDone(VehicleState.TESTED_EMPTY);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart370_Run()
        {
            bool b1 = RightKitStateControl.StateDoIt(VehicleState.TESTED_EMPTY_BOOKED);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart371_Run()
        {
            bool b1 = RightKitStateControl.IsStateDone(VehicleState.TESTED_EMPTY_BOOKED);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart372_Run()
        {
            bool b1 = RightKitStateControl.StateDoIt(VehicleState.TESTED_FULL);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart373_Run()
        {
            bool b1 = RightKitStateControl.IsStateDone(VehicleState.UNTESTED_FULL);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart374_Run()
        {
            bool b1 = RightKitStateControl.StateDoIt(VehicleState.NONE);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart115_Run()
        {
            if (Flag_RightKitShuttle_LotEnd.IsDoIt())
            {
                Flag_RightKitShuttle_LotEnd.Doing();
            }
            return FlowChart.FCRESULT.IDLE;
        }

        #endregion Shuttle B

        #region BowFeeder A
        private FlowChart.FCRESULT FC_BowFeederA_Run()
        {
            if (SYSPara.Lotend)
            {
                return FlowChart.FCRESULT.CASE1;
            }

            bool b = (bool)SYSPara.CallProc(ModuleName_BFM, "IsUseBFM", BowlFeederID.BF_A);
            if (b)
            {
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart121_Run()
        {
            if (SYSPara.Lotend && SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.UNLOAD))
            {
                Flag_TopBowlFeeder_LotEnd.DoIt();
                return FlowChart.FCRESULT.CASE1;
            }

            bool b2 = RightKitStateControl.IsStateDoIt(VehicleState.UNTESTED_EMPTY_BOOKED);
            bool b1 = LeftKitStateControl.IsStateDoIt(VehicleState.UNTESTED_EMPTY_BOOKED);

            if (HDT_BF_A_KitShuttleID.Equals(KitShuttleID.NONE))
            {
                if (b1 && HDT_BF_B_KitShuttleID != KitShuttleID.TransferShuttleA)
                {
                    if (SYSPara.Lotend)
                    {
                        bool b3 = LeftKitStateControl.StateDone(VehicleState.UNTESTED_EMPTY_BOOKED);
                        if (b3)
                        {
                            HDT_BF_A_KitShuttleID = KitShuttleID.NONE;
                            Flag_TopBowlFeeder_LotEnd.DoIt();
                            return FlowChart.FCRESULT.CASE1;
                        }
                    }
                    HDT_BF_A_KitShuttleID = KitShuttleID.TransferShuttleA;
                    return FlowChart.FCRESULT.NEXT;
                }
                else if (b2 && HDT_BF_B_KitShuttleID != KitShuttleID.TransferShuttleB)
                {
                    if (SYSPara.Lotend)
                    {
                        bool b4 = RightKitStateControl.StateDone(VehicleState.UNTESTED_EMPTY_BOOKED);
                        if (b4)
                        {
                            HDT_BF_A_KitShuttleID = KitShuttleID.NONE;
                            Flag_TopBowlFeeder_LotEnd.DoIt();
                            return FlowChart.FCRESULT.CASE1;
                        }
                    }
                    HDT_BF_A_KitShuttleID = KitShuttleID.TransferShuttleB;
                    return FlowChart.FCRESULT.NEXT;
                }
                else
                {
                    if (SYSPara.Lotend)
                    {
                        HDT_BF_A_KitShuttleID = KitShuttleID.NONE;
                        Flag_TopBowlFeeder_LotEnd.DoIt();
                        return FlowChart.FCRESULT.CASE1;
                    }
                }
            }
            else
            {
                if (HDT_BF_A_KitShuttleID.Equals(KitShuttleID.TransferShuttleA) && b1)
                {
                    if (SYSPara.Lotend)
                    {
                        bool b5 = LeftKitStateControl.StateDone(VehicleState.UNTESTED_EMPTY_BOOKED);
                        if (b5)
                        {
                            HDT_BF_A_KitShuttleID = KitShuttleID.NONE;
                            Flag_TopBowlFeeder_LotEnd.DoIt();
                            return FlowChart.FCRESULT.CASE1;
                        }
                    }
                    return FlowChart.FCRESULT.NEXT;
                }
                else if (HDT_BF_A_KitShuttleID.Equals(KitShuttleID.TransferShuttleB) && b2)
                {
                    if (SYSPara.Lotend)
                    {
                        bool b6 = RightKitStateControl.StateDone(VehicleState.UNTESTED_EMPTY_BOOKED);
                        if (b6)
                        {
                            HDT_BF_A_KitShuttleID = KitShuttleID.NONE;
                            Flag_TopBowlFeeder_LotEnd.DoIt();
                            return FlowChart.FCRESULT.CASE1;
                        }
                    }
                    return FlowChart.FCRESULT.NEXT;
                }
            }


            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart122_Run()
        {
            bool b1 = (bool)SYSPara.CallProc(ModuleName_BFM, "ChkBowlArrivalDetectionState", BowlFeederID.BF_A, EOnOffState.ON);
            if (b1.Equals(true))
            {
                TDEx_Top_BFM.BinReplace((byte)BinDefine.Empty, (byte)BinDefine.Bin1);
                return FlowChart.FCRESULT.NEXT;
            }
            else if (b1.Equals(false))
            {
                KitShuttleID KitID = KitShuttleID.NONE;
                if (HDT_BF_A_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_A)) KitID = KitShuttleID.TransferShuttleA;
                if (HDT_BF_A_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_B)) KitID = KitShuttleID.TransferShuttleB;
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "UnlockTransferShuttle", (KitShuttleOwner)KitShuttleOwner.HDT_BF_A, (KitShuttleID)KitID);
                if (T.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.CASE1;
                }
                return FlowChart.FCRESULT.IDLE;
                //return FlowChart.FCRESULT.CASE1;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart123_Run()
        {
            TrayDataEx Source;
            TrayDataEx Target;
            PnPStation SourceStation;
            PnPStation TargetStation;

            switch (HDT_BF_A_KitShuttleID)
            {
                case KitShuttleID.TransferShuttleA:
                    {
                        Target = TDEx_Left_KitShuttle;
                        TargetStation = PnPStation.KIT_SHUTTLE_A;
                    }
                    break;
                case KitShuttleID.TransferShuttleB:
                    {
                        Target = TDEx_Right_KitShuttle;
                        TargetStation = PnPStation.KIT_SHUTTLE_B;
                    }
                    break;
                default:
                    {
                        //Alarm
                        return FlowChart.FCRESULT.IDLE;
                    }
                    break;
            }
            Source = TDEx_Top_BFM;
            SourceStation = PnPStation.BOWLFEEDER_A;

            HDT_BF_A_PnPInfo = new PnPInfo(TDEx_Top_BFM, SourceStation, Source, GlobalDefine.PassBin, TargetStation, Target, GlobalDefine.EmptyBin, HDT_BF_A_INFO.Num_X, HDT_BF_A_INFO.Num_Y, VehicleState.UNTESTED_EMPTY_BOOKED);
            if (HDT_BF_A_PnPInfo.TargetIsDone)
            {
                return FlowChart.FCRESULT.CASE1;
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart292_Run()
        {
            KitShuttleID KitID = KitShuttleID.NONE;
            if (HDT_BF_A_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_A)) KitID = KitShuttleID.TransferShuttleA;
            if (HDT_BF_A_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_B)) KitID = KitShuttleID.TransferShuttleB;
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "LockTransferShuttle", (KitShuttleOwner)KitShuttleOwner.HDT_BF_A, (KitShuttleID)KitID);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart127_Run()
        {
            bool TargetFull = HDT_BF_A_PnPInfo.TargetIsDone;

            if (TargetFull || SYSPara.Lotend)
            {
                return FlowChart.FCRESULT.CASE1;
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart128_Run()
        {
            bool b1 = HDT_BF_A_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_A);
            bool b2 = HDT_BF_A_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_B);
            if (b1)
            {
                bool b3 = LeftKitStateControl.StateDone(VehicleState.UNTESTED_EMPTY_BOOKED);
                bool b5 = LeftKitStateControl.IsStateDoIt(VehicleState.UNTESTED_EMPTY);
                if (b3 || b5)
                {
                    HDT_BF_A_KitShuttleID = KitShuttleID.NONE;
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else if (b2)
            {
                bool b4 = RightKitStateControl.StateDone(VehicleState.UNTESTED_EMPTY_BOOKED);
                bool b6 = RightKitStateControl.IsStateDoIt(VehicleState.UNTESTED_EMPTY); //作業中節批
                if (b4 || b6)
                {
                    HDT_BF_A_KitShuttleID = KitShuttleID.NONE;
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart291_Run()
        {
            KitShuttleID KitID = KitShuttleID.NONE;
            if (HDT_BF_A_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_A)) KitID = KitShuttleID.TransferShuttleA;
            if (HDT_BF_A_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_B)) KitID = KitShuttleID.TransferShuttleB;
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "UnlockTransferShuttle", (KitShuttleOwner)KitShuttleOwner.HDT_BF_A, (KitShuttleID)KitID);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart126_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart125_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart129_Run()
        {
            if (Flag_TopBowlFeeder_LotEnd.IsDoIt())
            {
                Flag_TopBowlFeeder_LotEnd.Doing();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart140_Run()
        {
            if (Flag_TopBowlFeeder_LotEnd.IsDoing() && Flag_BottomBowlFeeder_LotEnd.IsDoing())
            {
                Flag_LeftKitShuttle_LotEnd.DoIt();
                Flag_RightKitShuttle_LotEnd.DoIt();
                mBFM.SetCanLotEnd();
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart141_Run()
        {
            //if (mBFM.mLotendOk)
            {
                Flag_TopBowlFeeder_LotEnd.Done();
                Flag_BottomBowlFeeder_LotEnd.Done();


            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart259_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart329_Run()
        {

            bool b1 = false;
            Point pos = new Point();
            BasePosInfo PlaceOffset = new BasePosInfo();
            BasePosInfo HDT_BF_A_Basic = new BasePosInfo();
            BasePosInfo KSMBasic = new BasePosInfo();

            switch (HDT_BF_A_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        PlaceOffset.X = SYSPara.PReadValue("Offset_HDT_BF_A_KITA_X").ToInt();
                        PlaceOffset.Y = SYSPara.PReadValue("Offset_HDT_BF_A_KITA_Y").ToInt();
                        PlaceOffset.Z = SYSPara.PReadValue("Offset_HDT_BF_A_KITA_Z_PLACE").ToInt() + KitInfo.Height + KitInfo.DeviceHeight;

                        pos = HDT_BF_A_PnP.PreCalcKitPnPPos(HDT_BF_A_PnPInfo.HDTTray, HDT_BF_A_PnPInfo.TargetTray);  //治具位置

                        HDT_BF_A_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_BFM, "GetBasicPos", BowlFeederID.BF_A, KitShuttleID.TransferShuttleA);
                        KSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleA);
                        //int InspectOffsetY = (int)SYSPara.CallProc(ModuleName_BFM, "GetInspectionResultY", BowlFeederID.BF_A);
                        HDT_BF_A_PnPInfo.WorkingPos_X = HDT_BF_A_Basic.X - (KitInfo.Width / 2) + KitInfo.XO + pos.X * KitInfo.XP + PlaceOffset.X;
                        HDT_BF_A_PnPInfo.WorkingPos_Y = KSMBasic.Y - (KitInfo.Length / 2) + KitInfo.YO + pos.Y * KitInfo.YP + PlaceOffset.Y;
                        HDT_BF_A_PnPInfo.WorkingPos_Z = HDT_BF_A_Basic.Z + PlaceOffset.Z;
                        b1 = true;
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        PlaceOffset.X = SYSPara.PReadValue("Offset_HDT_BF_A_KITB_X").ToInt();
                        PlaceOffset.Y = SYSPara.PReadValue("Offset_HDT_BF_A_KITB_Y").ToInt();
                        PlaceOffset.Z = SYSPara.PReadValue("Offset_HDT_BF_A_KITB_Z_PLACE").ToInt() + KitInfo.Height + KitInfo.DeviceHeight;

                        pos = HDT_BF_A_PnP.PreCalcKitPnPPos(HDT_BF_A_PnPInfo.HDTTray, HDT_BF_A_PnPInfo.TargetTray);  //治具位置
                        HDT_BF_A_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_BFM, "GetBasicPos", BowlFeederID.BF_A, KitShuttleID.TransferShuttleB);
                        KSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleB);
                        //int InspectOffsetY = (int)SYSPara.CallProc(ModuleName_BFM, "GetInspectionResultY", BowlFeederID.BF_A);
                        HDT_BF_A_PnPInfo.WorkingPos_X = HDT_BF_A_Basic.X - (KitInfo.Width / 2) + KitInfo.XO + pos.X * KitInfo.XP + PlaceOffset.X;
                        HDT_BF_A_PnPInfo.WorkingPos_Y = KSMBasic.Y - (KitInfo.Length / 2) + KitInfo.YO + pos.Y * KitInfo.YP + PlaceOffset.Y;
                        HDT_BF_A_PnPInfo.WorkingPos_Z = HDT_BF_A_Basic.Z + PlaceOffset.Z;
                        b1 = true;
                    }
                    break;
            }
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart332_Run()
        {
            BasePosInfo Pos = new BasePosInfo();
            Pos.X = HDT_BF_A_PnPInfo.WorkingPos_X;
            Pos.Y = HDT_BF_A_PnPInfo.WorkingPos_Y;
            Pos.Z = HDT_BF_A_PnPInfo.WorkingPos_Z;
            Pos.U = HDT_BF_A_PnPInfo.WorkingPos_U;

            ThreeValued T = ThreeValued.UNKNOWN;
            switch (HDT_BF_A_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "SetActionCommand_KSM", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleA, ACTIONMODE.MOVE, Pos);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "SetActionCommand_KSM", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleB, ACTIONMODE.MOVE, Pos);
                    }
                    break;
            }
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart331_Run()
        {
            ThreeValued T2 = ThreeValued.UNKNOWN;
            switch (HDT_BF_A_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "GetActionResult_KSM", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleA);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "GetActionResult_KSM", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleB);
                    }
                    break;
            }

            if (T2.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart273_Run()
        {
            BasePosInfo Pos = new BasePosInfo();
            Pos.X = HDT_BF_A_PnPInfo.WorkingPos_X;
            Pos.Y = HDT_BF_A_PnPInfo.WorkingPos_Y;
            Pos.Z = HDT_BF_A_PnPInfo.WorkingPos_Z;
            Pos.U = HDT_BF_A_PnPInfo.WorkingPos_U;

            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BFM, "SetActionCommand_BFM", BowlFeederID.BF_A, ACTIONMODE.PICKUP, Pos);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart274_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BFM, "GetActionResult_BFM", BowlFeederID.BF_A);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart275_Run()
        {

            bool b1 = false;
            Point pos = new Point();
            //Point PlaceOffset = new Point();
            BasePosInfo PlaceOffset = new BasePosInfo();
            BasePosInfo HDT_BF_A_Basic = new BasePosInfo();
            BasePosInfo KSMBasic = new BasePosInfo();

            switch (HDT_BF_A_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        PlaceOffset.X = SYSPara.PReadValue("Offset_HDT_BF_A_KITA_X").ToInt();
                        PlaceOffset.Y = SYSPara.PReadValue("Offset_HDT_BF_A_KITA_Y").ToInt();
                        PlaceOffset.Z = SYSPara.PReadValue("Offset_HDT_BF_A_KITA_Z_PLACE").ToInt() + KitInfo.Height + KitInfo.DeviceHeight;

                        pos = HDT_BF_A_PnP.CalcKitPnPPos(HDT_BF_A_PnPInfo.HDTTray, HDT_BF_A_PnPInfo.TargetTray);  //治具位置
                        //private BasePosInfo GetBasicPos(BowlFeederID ID, KitShuttleID ShuttleID)
                        HDT_BF_A_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_BFM, "GetBasicPos", BowlFeederID.BF_A, KitShuttleID.TransferShuttleA);
                        KSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleA);
                        int InspectOffsetY = (int)SYSPara.CallProc(ModuleName_BFM, "GetInspectionResultY", BowlFeederID.BF_A);
                        HDT_BF_A_PnPInfo.WorkingPos_X = HDT_BF_A_Basic.X - (KitInfo.Width / 2) + KitInfo.XO + pos.X * KitInfo.XP + PlaceOffset.X;
                        HDT_BF_A_PnPInfo.WorkingPos_Y = KSMBasic.Y - (KitInfo.Length / 2) + KitInfo.YO + pos.Y * KitInfo.YP + InspectOffsetY + PlaceOffset.Y;
                        HDT_BF_A_PnPInfo.WorkingPos_Z = HDT_BF_A_Basic.Z + PlaceOffset.Z;
                        b1 = true;
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        PlaceOffset.X = SYSPara.PReadValue("Offset_HDT_BF_A_KITB_X").ToInt();
                        PlaceOffset.Y = SYSPara.PReadValue("Offset_HDT_BF_A_KITB_Y").ToInt();
                        PlaceOffset.Z = SYSPara.PReadValue("Offset_HDT_BF_A_KITB_Z_PLACE").ToInt() + KitInfo.Height + KitInfo.DeviceHeight;

                        pos = HDT_BF_A_PnP.CalcKitPnPPos(HDT_BF_A_PnPInfo.HDTTray, HDT_BF_A_PnPInfo.TargetTray);  //治具位置
                        HDT_BF_A_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_BFM, "GetBasicPos", BowlFeederID.BF_A, KitShuttleID.TransferShuttleB);
                        KSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleB);
                        int InspectOffsetY = (int)SYSPara.CallProc(ModuleName_BFM, "GetInspectionResultY", BowlFeederID.BF_A);
                        HDT_BF_A_PnPInfo.WorkingPos_X = HDT_BF_A_Basic.X - (KitInfo.Width / 2) + KitInfo.XO + pos.X * KitInfo.XP + PlaceOffset.X;
                        HDT_BF_A_PnPInfo.WorkingPos_Y = KSMBasic.Y - (KitInfo.Length / 2) + KitInfo.YO + pos.Y * KitInfo.YP + InspectOffsetY + PlaceOffset.Y;
                        HDT_BF_A_PnPInfo.WorkingPos_Z = HDT_BF_A_Basic.Z + PlaceOffset.Z;
                        b1 = true;
                    }
                    break;
            }
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart276_Run()
        {
            NozzleState[,] states = HDT_BF_A_PnPInfo.GetBeforePickHeadAction();

            return FlowChart.FCRESULT.NEXT;
            //bool b1 = (bool)SYSPara.CallProc(ModuleName_HDT, "SetNozzleState", states, BoardHeadID.HDT_A);
            //if (b1)
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart281_Run()
        {
            BasePosInfo Pos = new BasePosInfo();
            Pos.X = HDT_BF_A_PnPInfo.WorkingPos_X;
            Pos.Y = HDT_BF_A_PnPInfo.WorkingPos_Y;
            Pos.Z = HDT_BF_A_PnPInfo.WorkingPos_Z;
            Pos.U = HDT_BF_A_PnPInfo.WorkingPos_U;

            ThreeValued T = ThreeValued.UNKNOWN;
            switch (HDT_BF_A_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "SetActionCommand_KSM", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleA, ACTIONMODE.MOVE, Pos);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "SetActionCommand_KSM", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleB, ACTIONMODE.MOVE, Pos);
                    }
                    break;
            }
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart280_Run()
        {
            ThreeValued T2 = ThreeValued.UNKNOWN;
            switch (HDT_BF_A_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "GetActionResult_KSM", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleA);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "GetActionResult_KSM", KitShuttleOwner.HDT_BF_A, KitShuttleID.TransferShuttleB);
                    }
                    break;
            }

            if (T2.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart277_Run()
        {
            BasePosInfo Pos = new BasePosInfo();
            Pos.X = HDT_BF_A_PnPInfo.WorkingPos_X;
            Pos.Y = HDT_BF_A_PnPInfo.WorkingPos_Y;
            Pos.Z = HDT_BF_A_PnPInfo.WorkingPos_Z;
            Pos.U = HDT_BF_A_PnPInfo.WorkingPos_U;

            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BFM, "SetActionCommand_BFM", BowlFeederID.BF_A, ACTIONMODE.PLACEMENT, Pos);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart278_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BFM, "GetActionResult_BFM", BowlFeederID.BF_A);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart279_Run()
        {
            bool[,] AfterPickPnPState = new bool[HDT_BF_A_PnPInfo.HEAD_X, HDT_BF_A_PnPInfo.HEAD_Y];
            for (int x = 0; x < HDT_BF_A_PnPInfo.HEAD_X; x++)
            {
                for (int y = 0; y < HDT_BF_A_PnPInfo.HEAD_Y; y++)
                {
                    AfterPickPnPState[x, y] = true;
                }
            }
            bool bRet = false;
            switch (HDT_BF_A_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        bRet = HDT_BF_A_PnP.KitDataExchange(TDEx_Top_BFM, TDEx_Left_KitShuttle, AfterPickPnPState);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Untested, JStatusType.Process, 1);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        bRet = HDT_BF_A_PnP.KitDataExchange(TDEx_Top_BFM, TDEx_Right_KitShuttle, AfterPickPnPState);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Untested, JStatusType.Process, 1);
                    }
                    break;
                default:
                    {
                        return FlowChart.FCRESULT.IDLE;
                    }
            }

            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart262_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart260_Run()
        {
            BasePosInfo Pos = new BasePosInfo();
            Pos.X = HDT_BF_A_PnPInfo.WorkingPos_X;
            Pos.Y = HDT_BF_A_PnPInfo.WorkingPos_Y;
            Pos.Z = HDT_BF_A_PnPInfo.WorkingPos_Z;
            Pos.U = HDT_BF_A_PnPInfo.WorkingPos_U;

            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BFM, "SetActionCommand_BFM", BowlFeederID.BF_A, ACTIONMODE.NONE, Pos);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart261_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BFM, "GetActionResult_BFM", BowlFeederID.BF_A);
            if (T.Equals(ThreeValued.TRUE))
            {

                //HDT_TR_PnP.Nozzles.SetState(0, 0, i, j, (byte)1, false);
                //HDT_BF_A_PnPInfo.SourceTray.SetBin(0, 0, 1, 1, (byte)BinDefine.Empty);
                TDEx_Top_BFM.BinReplace((byte)BinDefine.Bin1, (byte)BinDefine.Empty);
                for (int y = 0; y < KitInfo.YN; y++)
                {
                    for (int x = 0; x < KitInfo.XN; x++)
                    {
                        byte bin = HDT_BF_A_PnPInfo.TargetTray.GetBin(0, 0, x, y);
                        if (bin.Equals((byte)BinDefine.Empty))
                        {
                            HDT_BF_A_PnPInfo.TargetTray.SetBin(0, 0, x, y, (byte)BinDefine.Bin1);
                            GlobalDefine.OEE.UpdateBinQty(BinDefine.Untested, JStatusType.Process, 1);
                            break;
                        }
                    }
                }


                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }
        #endregion
        #region BowFeeder B
        private FlowChart.FCRESULT FC_BowFeederB_Run()
        {
            if (SYSPara.Lotend)
            {
                return FlowChart.FCRESULT.CASE1;
            }

            bool b = (bool)SYSPara.CallProc(ModuleName_BFM, "IsUseBFM", BowlFeederID.BF_B);
            if (b)
            {
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart132_Run()
        {
            if (SYSPara.Lotend && SYSPara.SystemProductionMode.Equals(PRODUCTION_MODE.UNLOAD))
            {
                Flag_BottomBowlFeeder_LotEnd.DoIt();
                return FlowChart.FCRESULT.CASE1;
            }

            bool b2 = RightKitStateControl.IsStateDoIt(VehicleState.UNTESTED_EMPTY_BOOKED);
            bool b1 = LeftKitStateControl.IsStateDoIt(VehicleState.UNTESTED_EMPTY_BOOKED);

            if (HDT_BF_B_KitShuttleID.Equals(KitShuttleID.NONE))
            {
                if (b1 && HDT_BF_A_KitShuttleID != KitShuttleID.TransferShuttleA)
                {
                    if (SYSPara.Lotend)
                    {
                        bool b3 = LeftKitStateControl.StateDone(VehicleState.UNTESTED_EMPTY_BOOKED);
                        if (b3)
                        {
                            HDT_BF_B_KitShuttleID = KitShuttleID.NONE;
                            Flag_BottomBowlFeeder_LotEnd.DoIt();
                            return FlowChart.FCRESULT.CASE1;
                        }
                    }
                    HDT_BF_B_KitShuttleID = KitShuttleID.TransferShuttleA;
                    return FlowChart.FCRESULT.NEXT;
                }
                else if (b2 && HDT_BF_A_KitShuttleID != KitShuttleID.TransferShuttleB)
                {
                    if (SYSPara.Lotend)
                    {
                        bool b4 = RightKitStateControl.StateDone(VehicleState.UNTESTED_EMPTY_BOOKED);
                        if (b4)
                        {
                            HDT_BF_B_KitShuttleID = KitShuttleID.NONE;
                            Flag_BottomBowlFeeder_LotEnd.DoIt();
                            return FlowChart.FCRESULT.CASE1;
                        }
                    }
                    HDT_BF_B_KitShuttleID = KitShuttleID.TransferShuttleB;
                    return FlowChart.FCRESULT.NEXT;
                }
                else
                {
                    if (SYSPara.Lotend)
                    {
                        HDT_BF_B_KitShuttleID = KitShuttleID.NONE;
                        Flag_BottomBowlFeeder_LotEnd.DoIt();
                        return FlowChart.FCRESULT.CASE1;
                    }
                }
            }
            else
            {
                if (HDT_BF_B_KitShuttleID.Equals(KitShuttleID.TransferShuttleA) && b1)
                {
                    if (SYSPara.Lotend)
                    {
                        bool b5 = RightKitStateControl.StateDone(VehicleState.UNTESTED_EMPTY_BOOKED);
                        if (b5)
                        {
                            HDT_BF_B_KitShuttleID = KitShuttleID.NONE;
                            Flag_BottomBowlFeeder_LotEnd.DoIt();
                            return FlowChart.FCRESULT.CASE1;
                        }
                    }
                    return FlowChart.FCRESULT.NEXT;
                }
                else if (HDT_BF_B_KitShuttleID.Equals(KitShuttleID.TransferShuttleB) && b2)
                {
                    if (SYSPara.Lotend)
                    {
                        bool b6 = RightKitStateControl.StateDone(VehicleState.UNTESTED_EMPTY_BOOKED);
                        if (b6)
                        {
                            HDT_BF_B_KitShuttleID = KitShuttleID.NONE;
                            Flag_BottomBowlFeeder_LotEnd.DoIt();
                            return FlowChart.FCRESULT.CASE1;
                        }
                    }
                    return FlowChart.FCRESULT.NEXT;
                }
            }


            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart133_Run()
        {
            bool b1 = (bool)SYSPara.CallProc(ModuleName_BFM, "ChkBowlArrivalDetectionState", BowlFeederID.BF_B, EOnOffState.ON);
            if (b1.Equals(true))
            {
                TDEx_Bottom_BFM.BinReplace((byte)BinDefine.Empty, (byte)BinDefine.Bin1);
                return FlowChart.FCRESULT.NEXT;
            }
            else if (b1.Equals(false))
            {
                KitShuttleID KitID = KitShuttleID.NONE;
                if (HDT_BF_B_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_A)) KitID = KitShuttleID.TransferShuttleA;
                if (HDT_BF_B_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_B)) KitID = KitShuttleID.TransferShuttleB;
                ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "UnlockTransferShuttle", (KitShuttleOwner)KitShuttleOwner.HDT_BF_B, (KitShuttleID)KitID);
                if (T.Equals(ThreeValued.TRUE))
                {
                    return FlowChart.FCRESULT.CASE1;
                }
                return FlowChart.FCRESULT.IDLE;
                //return FlowChart.FCRESULT.CASE1;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart134_Run()
        {
            TrayDataEx Source;
            TrayDataEx Target;
            PnPStation SourceStation;
            PnPStation TargetStation;

            switch (HDT_BF_B_KitShuttleID)
            {
                case KitShuttleID.TransferShuttleA:
                    {
                        Target = TDEx_Left_KitShuttle;
                        TargetStation = PnPStation.KIT_SHUTTLE_A;
                    }
                    break;
                case KitShuttleID.TransferShuttleB:
                    {
                        Target = TDEx_Right_KitShuttle;
                        TargetStation = PnPStation.KIT_SHUTTLE_B;
                    }
                    break;
                default:
                    {
                        //Alarm
                        return FlowChart.FCRESULT.IDLE;
                    }
                    break;
            }
            Source = TDEx_Bottom_BFM;
            SourceStation = PnPStation.BOWLFEEDER_B;

            HDT_BF_B_PnPInfo = new PnPInfo(TDEx_Bottom_BFM, SourceStation, Source, GlobalDefine.PassBin, TargetStation, Target, GlobalDefine.EmptyBin, HDT_BF_B_INFO.Num_X, HDT_BF_B_INFO.Num_Y, VehicleState.UNTESTED_EMPTY_BOOKED);
            if (HDT_BF_B_PnPInfo.TargetIsDone)
            {
                return FlowChart.FCRESULT.CASE1;
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart294_Run()
        {
            KitShuttleID KitID = KitShuttleID.NONE;
            if (HDT_BF_B_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_A)) KitID = KitShuttleID.TransferShuttleA;
            if (HDT_BF_B_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_B)) KitID = KitShuttleID.TransferShuttleB;
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "LockTransferShuttle", (KitShuttleOwner)KitShuttleOwner.HDT_BF_B, (KitShuttleID)KitID);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart136_Run()
        {
            bool TargetFull = HDT_BF_B_PnPInfo.TargetIsDone;
            if (TargetFull || SYSPara.Lotend)
            {
                return FlowChart.FCRESULT.CASE1;
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart137_Run()
        {
            bool b1 = HDT_BF_B_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_A);
            bool b2 = HDT_BF_B_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_B);
            if (b1)
            {
                bool b3 = LeftKitStateControl.StateDone(VehicleState.UNTESTED_EMPTY_BOOKED);
                bool b5 = LeftKitStateControl.IsStateDoIt(VehicleState.UNTESTED_EMPTY);
                if (b3 || b5)
                {
                    HDT_BF_B_KitShuttleID = KitShuttleID.NONE;
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            else if (b2)
            {
                bool b4 = RightKitStateControl.StateDone(VehicleState.UNTESTED_EMPTY_BOOKED);
                bool b6 = RightKitStateControl.IsStateDoIt(VehicleState.UNTESTED_EMPTY);
                if (b4 || b6)
                {
                    HDT_BF_B_KitShuttleID = KitShuttleID.NONE;
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart293_Run()
        {
            KitShuttleID KitID = KitShuttleID.NONE;
            if (HDT_BF_B_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_A)) KitID = KitShuttleID.TransferShuttleA;
            if (HDT_BF_B_PnPInfo.TargetStation.Equals(PnPStation.KIT_SHUTTLE_B)) KitID = KitShuttleID.TransferShuttleB;
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "UnlockTransferShuttle", (KitShuttleOwner)KitShuttleOwner.HDT_BF_B, (KitShuttleID)KitID);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart130_Run()
        {
            if (Flag_BottomBowlFeeder_LotEnd.IsDoIt())
            {
                Flag_BottomBowlFeeder_LotEnd.Doing();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart270_Run()
        {
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart266_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart330_Run()
        {
            bool b1 = false;
            Point pos = new Point();
            BasePosInfo PlaceOffset = new BasePosInfo();
            BasePosInfo HDT_BF_B_Basic = new BasePosInfo();
            BasePosInfo KSMBasic = new BasePosInfo();

            switch (HDT_BF_B_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        PlaceOffset.X = SYSPara.PReadValue("Offset_HDT_BF_B_KITA_X").ToInt();
                        PlaceOffset.Y = SYSPara.PReadValue("Offset_HDT_BF_B_KITA_Y").ToInt();
                        PlaceOffset.Z = SYSPara.PReadValue("Offset_HDT_BF_B_KITA_Z_PLACE").ToInt() + KitInfo.Height + KitInfo.DeviceHeight;

                        pos = HDT_BF_B_PnP.PreCalcKitPnPPos(HDT_BF_B_PnPInfo.HDTTray, HDT_BF_B_PnPInfo.TargetTray);  //治具位置
                        HDT_BF_B_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_BFM, "GetBasicPos", BowlFeederID.BF_B, KitShuttleID.TransferShuttleA);
                        KSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleA);
                        //int InspectOffsetY = (int)SYSPara.CallProc(ModuleName_BFM, "GetInspectionResultY", BowlFeederID.BF_B);
                        HDT_BF_B_PnPInfo.WorkingPos_X = HDT_BF_B_Basic.X - (KitInfo.Width / 2) + KitInfo.XO + pos.X * KitInfo.XP + PlaceOffset.X;
                        HDT_BF_B_PnPInfo.WorkingPos_Y = KSMBasic.Y - (KitInfo.Length / 2) + KitInfo.YO + pos.Y * KitInfo.YP + PlaceOffset.Y;
                        HDT_BF_B_PnPInfo.WorkingPos_Z = HDT_BF_B_Basic.Z + PlaceOffset.Z;
                        b1 = true;
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        PlaceOffset.X = SYSPara.PReadValue("Offset_HDT_BF_B_KITB_X").ToInt();
                        PlaceOffset.Y = SYSPara.PReadValue("Offset_HDT_BF_B_KITB_Y").ToInt();
                        PlaceOffset.Z = SYSPara.PReadValue("Offset_HDT_BF_B_KITB_Z_PLACE").ToInt() + KitInfo.Height + KitInfo.DeviceHeight;

                        pos = HDT_BF_B_PnP.CalcKitPnPPos(HDT_BF_B_PnPInfo.HDTTray, HDT_BF_B_PnPInfo.TargetTray);  //治具位置
                        HDT_BF_B_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_BFM, "GetBasicPos", BowlFeederID.BF_B, KitShuttleID.TransferShuttleB);
                        KSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleB);
                        //int InspectOffsetY = (int)SYSPara.CallProc(ModuleName_BFM, "GetInspectionResultY", BowlFeederID.BF_B);
                        HDT_BF_B_PnPInfo.WorkingPos_X = HDT_BF_B_Basic.X - (KitInfo.Width / 2) + KitInfo.XO + pos.X * KitInfo.XP + PlaceOffset.X;
                        HDT_BF_B_PnPInfo.WorkingPos_Y = KSMBasic.Y - (KitInfo.Length / 2) + KitInfo.YO + pos.Y * KitInfo.YP + PlaceOffset.Y;
                        HDT_BF_B_PnPInfo.WorkingPos_Z = HDT_BF_B_Basic.Z + PlaceOffset.Z;
                        b1 = true;
                    }
                    break;
            }
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart334_Run()
        {
            BasePosInfo Pos = new BasePosInfo();
            Pos.X = HDT_BF_B_PnPInfo.WorkingPos_X;
            Pos.Y = HDT_BF_B_PnPInfo.WorkingPos_Y;
            Pos.Z = HDT_BF_B_PnPInfo.WorkingPos_Z;
            Pos.U = HDT_BF_B_PnPInfo.WorkingPos_U;

            ThreeValued T = ThreeValued.UNKNOWN;
            switch (HDT_BF_B_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "SetActionCommand_KSM", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleA, ACTIONMODE.MOVE, Pos);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "SetActionCommand_KSM", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleB, ACTIONMODE.MOVE, Pos);
                    }
                    break;
            }
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart333_Run()
        {
            ThreeValued T2 = ThreeValued.UNKNOWN;
            switch (HDT_BF_B_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "GetActionResult_KSM", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleA);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "GetActionResult_KSM", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleB);
                    }
                    break;
            }

            if (T2.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart290_Run()
        {
            BasePosInfo Pos = new BasePosInfo();
            Pos.X = HDT_BF_B_PnPInfo.WorkingPos_X;
            Pos.Y = HDT_BF_B_PnPInfo.WorkingPos_Y;
            Pos.Z = HDT_BF_B_PnPInfo.WorkingPos_Z;
            Pos.U = HDT_BF_B_PnPInfo.WorkingPos_U;



            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BFM, "SetActionCommand_BFM", BowlFeederID.BF_B, ACTIONMODE.PICKUP, Pos);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart289_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BFM, "GetActionResult_BFM", BowlFeederID.BF_B);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart288_Run()
        {
            bool b1 = false;
            Point pos = new Point();
            //Point PickOffset = new Point();
            BasePosInfo PlaceOffset = new BasePosInfo();
            BasePosInfo HDT_BF_B_Basic = new BasePosInfo();
            BasePosInfo KSMBasic = new BasePosInfo();

            switch (HDT_BF_B_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        PlaceOffset.X = SYSPara.PReadValue("Offset_HDT_BF_B_KITA_X").ToInt();
                        PlaceOffset.Y = SYSPara.PReadValue("Offset_HDT_BF_B_KITA_Y").ToInt();
                        PlaceOffset.Z = SYSPara.PReadValue("Offset_HDT_BF_B_KITA_Z_PLACE").ToInt() + KitInfo.Height + KitInfo.DeviceHeight;

                        pos = HDT_BF_B_PnP.CalcKitPnPPos(HDT_BF_B_PnPInfo.HDTTray, HDT_BF_B_PnPInfo.TargetTray);  //治具位置
                        HDT_BF_B_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_BFM, "GetBasicPos", BowlFeederID.BF_B, KitShuttleID.TransferShuttleA);
                        KSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleA);
                        int InspectOffsetY = (int)SYSPara.CallProc(ModuleName_BFM, "GetInspectionResultY", BowlFeederID.BF_B);
                        HDT_BF_B_PnPInfo.WorkingPos_X = HDT_BF_B_Basic.X - (KitInfo.Width / 2) + KitInfo.XO + pos.X * KitInfo.XP + PlaceOffset.X;
                        HDT_BF_B_PnPInfo.WorkingPos_Y = KSMBasic.Y - (KitInfo.Length / 2) + KitInfo.YO + pos.Y * KitInfo.YP + InspectOffsetY + PlaceOffset.Y;
                        HDT_BF_B_PnPInfo.WorkingPos_Z = HDT_BF_B_Basic.Z + PlaceOffset.Z;
                        b1 = true;
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        PlaceOffset.X = SYSPara.PReadValue("Offset_HDT_BF_B_KITB_X").ToInt();
                        PlaceOffset.Y = SYSPara.PReadValue("Offset_HDT_BF_B_KITB_Y").ToInt();
                        PlaceOffset.Z = SYSPara.PReadValue("Offset_HDT_BF_B_KITB_Z_PLACE").ToInt() + KitInfo.Height + KitInfo.DeviceHeight;

                        pos = HDT_BF_B_PnP.CalcKitPnPPos(HDT_BF_B_PnPInfo.HDTTray, HDT_BF_B_PnPInfo.TargetTray);  //治具位置
                        HDT_BF_B_Basic = (BasePosInfo)SYSPara.CallProc(ModuleName_BFM, "GetBasicPos", BowlFeederID.BF_B, KitShuttleID.TransferShuttleB);
                        KSMBasic = (BasePosInfo)SYSPara.CallProc(ModuleName_KSM, "GetBasicInfo", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleB);
                        int InspectOffsetY = (int)SYSPara.CallProc(ModuleName_BFM, "GetInspectionResultY", BowlFeederID.BF_B);
                        HDT_BF_B_PnPInfo.WorkingPos_X = HDT_BF_B_Basic.X - (KitInfo.Width / 2) + KitInfo.XO + pos.X * KitInfo.XP + PlaceOffset.X;
                        HDT_BF_B_PnPInfo.WorkingPos_Y = KSMBasic.Y - (KitInfo.Length / 2) + KitInfo.YO + pos.Y * KitInfo.YP + InspectOffsetY + PlaceOffset.Y;
                        HDT_BF_B_PnPInfo.WorkingPos_Z = HDT_BF_B_Basic.Z + PlaceOffset.Z;
                        b1 = true;
                    }
                    break;
            }
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart287_Run()
        {
            NozzleState[,] states = HDT_BF_B_PnPInfo.GetBeforePickHeadAction();

            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart286_Run()
        {
            BasePosInfo Pos = new BasePosInfo();
            Pos.X = HDT_BF_B_PnPInfo.WorkingPos_X;
            Pos.Y = HDT_BF_B_PnPInfo.WorkingPos_Y;
            Pos.Z = HDT_BF_B_PnPInfo.WorkingPos_Z;
            Pos.U = HDT_BF_B_PnPInfo.WorkingPos_U;

            ThreeValued T = ThreeValued.UNKNOWN;
            switch (HDT_BF_B_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "SetActionCommand_KSM", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleA, ACTIONMODE.MOVE, Pos);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        T = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "SetActionCommand_KSM", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleB, ACTIONMODE.MOVE, Pos);
                    }
                    break;
            }
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart282_Run()
        {
            ThreeValued T2 = ThreeValued.UNKNOWN;
            switch (HDT_BF_B_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "GetActionResult_KSM", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleA);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        T2 = (ThreeValued)SYSPara.CallProc(ModuleName_KSM, "GetActionResult_KSM", KitShuttleOwner.HDT_BF_B, KitShuttleID.TransferShuttleB);
                    }
                    break;
            }

            if (T2.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart283_Run()
        {
            BasePosInfo Pos = new BasePosInfo();
            Pos.X = HDT_BF_B_PnPInfo.WorkingPos_X;
            Pos.Y = HDT_BF_B_PnPInfo.WorkingPos_Y;
            Pos.Z = HDT_BF_B_PnPInfo.WorkingPos_Z;
            Pos.U = HDT_BF_B_PnPInfo.WorkingPos_U;

            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BFM, "SetActionCommand_BFM", BowlFeederID.BF_B, ACTIONMODE.PLACEMENT, Pos);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart284_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BFM, "GetActionResult_BFM", BowlFeederID.BF_B);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart285_Run()
        {
            bool[,] AfterPickPnPState = new bool[HDT_BF_B_PnPInfo.HEAD_X, HDT_BF_B_PnPInfo.HEAD_Y];

            for (int x = 0; x < HDT_BF_B_PnPInfo.HEAD_X; x++)
            {
                for (int y = 0; y < HDT_BF_B_PnPInfo.HEAD_Y; y++)
                {
                    AfterPickPnPState[x, y] = true;
                }
            }

            bool bRet = false;
            switch (HDT_BF_B_PnPInfo.TargetStation)
            {
                case PnPStation.KIT_SHUTTLE_A:
                    {
                        bRet = HDT_BF_B_PnP.KitDataExchange(TDEx_Bottom_BFM, TDEx_Left_KitShuttle, AfterPickPnPState);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Untested, JStatusType.Process, 1);
                    }
                    break;
                case PnPStation.KIT_SHUTTLE_B:
                    {
                        bRet = HDT_BF_B_PnP.KitDataExchange(TDEx_Bottom_BFM, TDEx_Right_KitShuttle, AfterPickPnPState);
                        GlobalDefine.OEE.UpdateBinQty(BinDefine.Untested, JStatusType.Process, 1);
                    }
                    break;
                default:
                    {
                        return FlowChart.FCRESULT.IDLE;
                    }
            }

            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart265_Run()
        {
            BasePosInfo Pos = new BasePosInfo();
            Pos.X = HDT_BF_B_PnPInfo.WorkingPos_X;
            Pos.U = HDT_BF_B_PnPInfo.WorkingPos_U;
            Pos.Y = HDT_BF_B_PnPInfo.WorkingPos_Y;
            Pos.Z = HDT_BF_B_PnPInfo.WorkingPos_Z;

            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BFM, "SetActionCommand_BFM", BowlFeederID.BF_B, ACTIONMODE.NONE, Pos);
            if (T.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart264_Run()
        {
            ThreeValued T = (ThreeValued)SYSPara.CallProc(ModuleName_BFM, "GetActionResult_BFM", BowlFeederID.BF_B);
            if (T.Equals(ThreeValued.TRUE))
            {
                GlobalDefine.OEE.UpdateBinQty(BinDefine.Untested, JStatusType.Process, 1);
                //TDEx_Bottom_BFM.BinReplace((byte)BinDefine.Bin1, (byte)BinDefine.Empty);
                HDT_BF_B_PnPInfo.SourceTray.SetBin(0, 0, 0, 0, (byte)BinDefine.Empty);
                for (int y = 0; y < KitInfo.YN; y++)
                {
                    for (int x = 0; x < KitInfo.XN; x++)
                    {
                        byte bin = HDT_BF_B_PnPInfo.TargetTray.GetBin(0, 0, x, y);
                        if (bin.Equals((byte)BinDefine.Empty))
                        {
                            HDT_BF_B_PnPInfo.TargetTray.SetBin(0, 0, x, y, (byte)BinDefine.Bin1);
                            break;
                        }
                    }
                }
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }
        #endregion

        #region Scan Action
        private FlowChart.FCRESULT FC_Auto_ScanAction_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart351_Run()
        {
            if (Flag_Load_ScanAction.IsDoIt())
            {
                Flag_Load_ScanAction.Doing();
                Flag_Load_WorkAction.Done();
                Flag_SuppleDeviceAction.DoIt();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart352_Run()
        {
            if (Flag_SuppleDeviceAction.IsDone())
            {
                return FlowChart.FCRESULT.NEXT;
            }
            SYSPara.ShowAlarm("W", (int)AlarmCode.Err_SuppleDevie);
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart353_Run()
        {
            ThreeValued tRet = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", TRMStation.LOAD_A, ACTIONMODE.SCAN);
            if (tRet.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart354_Run()
        {
            ThreeValued tRet = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (tRet.Equals(ThreeValued.TRUE))
            {
                string sMap = (string)SYSPara.CallProc(ModuleName_TRM, "GetMappingResult", TRMStation.LOAD_A);
                SetMappingData(sMap, TRMStation.LOAD_A);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart355_Run()
        {
            ThreeValued tRet = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "SetActionCommand_TRM", TRMStation.LOAD_B, ACTIONMODE.SCAN);
            if (tRet.Equals(ThreeValued.TRUE))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart356_Run()
        {
            ThreeValued tRet = (ThreeValued)SYSPara.CallProc(ModuleName_TRM, "GetActionResult_TRM");
            if (tRet.Equals(ThreeValued.TRUE))
            {
                string sMap = (string)SYSPara.CallProc(ModuleName_TRM, "GetMappingResult", TRMStation.LOAD_B);
                SetMappingData(sMap, TRMStation.LOAD_B);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart357_Run()
        {
            Flag_Load_ScanAction.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart358_Run()
        {
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion ScanAction

        #endregion

    }
}
