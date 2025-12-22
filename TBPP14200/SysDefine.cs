using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProVIFM;
using KCSDK;
using ProVTool;
using CommonObj;
using IsClientIntf;
using System.Data;
using ProVLib;
using PaeLibProVSDKEx;
using CommonObj;

namespace TBPP14200
{
    #region 架構使用 (全域共用結構定義)

    public enum ControlButtonType
    {
        None = 0,
        Initial,
    }

    //登入人員類別
    public enum UserType
    {
        utNone = 0,
        utOperator,
        utEngineer,
        utAdministrator,
        utProV,
    };

    //運行模式
    public enum RunModeDT
    {
        IDLE = 0,
        HOME,
        AUTO,
        MANUAL,
        TEACH,
        MAINTENANCE,
    }

    //設備狀態
    public enum StateMode
    {
        SM_IDLE, //閒置狀態 (起始狀態)
        SM_INIT_RESET, //進入初始化流程
        SM_INIT_RUN, //初始化運行
        SM_PACKAGELOAD_RESET, //進入載入產品設定流程
        SM_PACKAGELOAD_OK, //載入產品設定完成
        SM_AUTO_RESET, //進入自動運行流程
        SM_AUTO_RUN, //自動運行
        SM_MANUAL_RESET, //手動流程前置設定
        SM_MANUAL_RUN, //手動流程運行
        SM_TEACH_RESET, //調機流程前置設定
        SM_TEACH_RUN, //調機流程運行

        SM_ALARM, //錯誤警報中
        SM_PAUSE, //運行暫停
        SM_ABORT, //運行強制中止
        SM_CANCEL, //運行取消

        SM_MAINTENANCE_RESET, //維護模式前置設定
        SM_MAINTENANCE_RUN, //維護模式運行
    };

    //log類型
    public enum EnLoggerType
    {
        EnLog_ARM = 0,
        EnLog_OP,
        EnLog_SPC,
        EnLog_COMM,
    };

    ////使用者權限管理
    //public struct UserDT
    //{
    //    public string UserID; //員工編號
    //    public string UserPW; //員工密碼
    //    public UserType UserRight; //權限
    //}

    #endregion

    #region 使用者修改 (全域共用結構定義)


    public struct TRAY_INFO
    {

        public int Dir;     //Pin1 方向, 0:Left_Top, 1:Left_Bottom, 2:Right_Top, 3:Right_Bottom.
        public int Length;  //長
        public int Width;   //寬

        public int XN;      //X 數量(依盤方向而定)
        public int YN;      //Y 數量(依盤方向而定)
        public int XP;      //X 間距(依盤方向而定)
        public int YP;      //Y 間距(依盤方向而定)
        public int XO;      //X 偏移量(依盤方向而定)
        public int YO;      //Y 偏移量(依盤方向而定)
        public int Height;  //高(厚度)
        public int Depth;   //深度

        public int BLK_XN;  //Block X 數量(依盤方向而定)
        public int BLK_YN;  //Block Y 數量(依盤方向而定)
        public int BLK_XP;  //Block X 間距(依盤方向而定)
        public int BLK_YP;  //Block Y 間距(依盤方向而定)
        public int BLK_XO;  //Block X 偏移量(依盤方向而定)
        public int BLK_YO;  //Block Y 偏移量(依盤方向而定)
        public int BLK_Height;  //高(厚度)
        public int BLK_Depth;   //深度


        public int JedecTrayEar;  //JEDEC TRAY 耳朵寬度
        public int DeviceHeight;    //材料厚度
        public int DeviceWidth;
        public int DeviceLength;
        public int LID_Height;      //Lid 厚度
        public int LID_Dir;         //開蓋方向

        public int TrayThickness;
        public int Notch;
    }

    public class LOT_INFO
    {
        public string LotNumber { get; set; }   //目前生產批號
        public string OperatorID { get; set; }  //目前生產工號
        public string DiePakType { get; set; }  //板Type
        public string DeviceType { get; set; }  //材料Type
        public string TestProgram { get; set; } //測試程式名稱
        public int LotQuantity { get; set; }    //開批輸入數量
        public int BIBXN { get; set; }
        public int BIBYN { get; set; }

        public void Reset()
        {
            LotNumber = "";
            OperatorID = "";
            DiePakType = "";
            DeviceType = "";
            TestProgram = "";
            LotQuantity = -1;
            BIBYN = 1;
            BIBXN = 1;
        }
        public LOT_INFO Clone()
        {
            LOT_INFO info = new LOT_INFO();
            info.LotNumber = this.LotNumber;
            info.OperatorID = this.OperatorID;
            info.DiePakType = this.DiePakType;
            info.DeviceType = this.DeviceType;
            info.TestProgram = this.TestProgram;
            info.LotQuantity = this.LotQuantity;
            info.BIBXN = this.BIBXN;
            info.BIBYN = this.BIBYN;
            return info;
        }
    }

    public class PnPInfo
    {
        private PnPStation m_SourceStation;
        private PnPStation m_TargetStation;
        private TrayDataEx m_SourceTray;
        private TrayDataEx m_TargetTray;
        private TrayDataEx m_HDTTray;
        private byte[] m_TargetBin;
        private byte[] m_SourceBin;
        private int PnP_X;
        private int PnP_Y;

        public int WorkingPos_X;
        public int WorkingPos_Y;
        public int WorkingPos_U;
        public int WorkingPos_Z;
        public VehicleState Current_VechicleState;

        public PnPInfo()
        { }

        public PnPInfo(TrayDataEx HDTTray, PnPStation SourceStation, TrayDataEx SourceTray, byte[] SourceBin, PnPStation TargetStation, TrayDataEx TargetTray, byte[] TargetBin, int PnP_X, int PnP_Y, VehicleState Current_VechicleState)
        {
            this.m_SourceStation = SourceStation;
            this.m_SourceTray = SourceTray;
            this.m_TargetStation = TargetStation;
            this.m_TargetTray = TargetTray;
            this.m_TargetBin = TargetBin;
            this.m_SourceBin = SourceBin;
            this.PnP_X = PnP_X;
            this.PnP_Y = PnP_Y;
            this.Current_VechicleState = Current_VechicleState;
            this.m_HDTTray = HDTTray;
        }

        public PnPStation SourceStation
        {
            get
            {
                return m_SourceStation;
            }
        }

        public PnPStation TargetStation
        {
            get
            {
                return m_TargetStation;
            }
            set
            {
                m_TargetStation = value;
            }
        }

        public TrayDataEx SourceTray
        {
            get
            {
                return this.m_SourceTray;
            }
        }

        public TrayDataEx TargetTray
        {
            get
            {
                return this.m_TargetTray;
            }
        }

        public TrayDataEx HDTTray
        {
            get
            {
                return this.m_HDTTray;
            }
        }

        public bool HeadIsFull
        {
            get
            {
                bool b1 = m_HDTTray.IsEmpty(GlobalDefine.EmptyBin);
                //bool b2 = m_HDTTray.IsEmpty(new byte[] {(byte)BinDefine.SortEmpty});
                //if (b1 && b2)
                if (b1)
                {
                    return true;
                }
                return false;
            }
        }

        public int PickICCount
        {
            get
            {
                return m_HDTTray.BinCount(SourceBin);
            }
        }
        public int HEAD_X
        {
            get
            {
                return PnP_X;
            }
        }

        public int HEAD_Y
        {
            get
            {
                return PnP_Y;
            }
        }

        public bool HeadIsEmpty
        {
            get
            {
                bool b1 = m_HDTTray.IsEmpty(GlobalDefine.AllBin);
                //bool b2 = m_HDTTray.IsEmpty(GlobalDefine.SortBin);
                //if (b1 && b2)
                if (b1)
                {
                    return true;
                }
                return false;
            }
        }

        public bool TargetIsDone
        {
            get
            {
                if (m_TargetTray.IsEmpty(m_TargetBin))
                {
                    return true;
                }
                return false;
            }
        }

        public bool SourceIsDone
        {
            get
            {
                if (m_SourceTray.IsEmpty(m_SourceBin))
                {
                    return true;
                }
                return false;
            }
        }
        public bool SourceBlockIsDone(int Block_X, int Block_Y)
        {
            bool SourceIsDone = true;
            for (int i = 0; i < m_SourceTray.Cols; i++)
            {
                for (int j = 0; j < m_SourceTray.Rows; j++)
                {
                    byte bin = m_SourceTray.GetBin(Block_X, Block_Y, i, j);
                    if (m_SourceBin.Contains(bin))
                    {
                        SourceIsDone = false;
                    }

                }

            }
            return SourceIsDone;
        }

        public int TargetBinCount
        {
            get
            {
                return m_TargetTray.BinCount(m_TargetBin);
            }
        }

        public int SourceBinCount
        {
            get
            {
                return m_SourceTray.BinCount(m_SourceBin);
            }
        }

        public byte[] SourceBin
        {
            get
            {
                return m_SourceBin;
            }
        }

        public byte[] TargetBin
        {
            get
            {
                return m_TargetBin;
            }
        }

        public NozzleState[,] GetBeforePickHeadAction()
        {
            int YN = PnP_Y;
            int XN = PnP_X;
            NozzleState[,] Action = new NozzleState[XN, YN];

            for (int y = 0; y < YN; y++)
            {
                for (int x = 0; x < XN; x++)
                {
                    byte bin = 0;
                    byte state = 0;
                    state = m_HDTTray.GetState(0, 0, x, y);
                    bin = m_HDTTray.GetBin(0, 0, x, y);
                    if (state.Equals(1))
                    {
                        Action[x, y] = NozzleState.InUsing;
                    }
                    else
                    {
                        Action[x, y] = NozzleState.Unused;
                    }
                }
            }
            return Action;
        }

        public NozzleState[,] GetBeforePlaceHeadAction()
        {
            //this.m_HDTTray = HDTTray;
            int YN = PnP_Y;
            int XN = PnP_X;
            NozzleState[,] Action = new NozzleState[XN, YN];

            for (int y = 0; y < YN; y++)
            {
                for (int x = 0; x < XN; x++)
                {
                    byte bin = 0;
                    byte state = 0;
                    state = m_HDTTray.GetState(0, 0, x, y);//need modify
                    bin = m_HDTTray.GetBin(0, 0, x, y);//need modify
                    if (state.Equals(1))
                    {
                        Action[x, y] = NozzleState.InUsing;
                    }
                    else
                    {
                        Action[x, y] = NozzleState.Unused;
                    }
                }
            }
            return Action;
        }


    }

public class BoardBinInfo//用於主畫面顯示
    {
        private object Lock = null;
        private string m_id;
        private string m_type;
        private string m_map;
        private List<BinDefine> m_bininfo;
        bool StopUpdate = true;
        public BoardBinInfo()
        {
            Lock = new object();
            m_id = "";
            m_type = "";
            m_map = "";
            m_bininfo = new List<BinDefine>();
            StopUpdate = true;
        }
        public void SetBoardInfo(string id = "", string type = "", string map = "")
        {
            StopUpdate = true;
            lock (Lock)
            {
                this.m_id = id;
                this.m_type = type;
                this.m_map = map;
                this.m_bininfo = new List<BinDefine>();
            }
        }

        private byte StrToBin(string symbol)
        {
            switch (symbol.ToUpper())
            {
                case "-":
                    {
                        return (byte)BinDefine.Disabled;
                    }
                    break;
                case "X":
                    {
                        return (byte)BinDefine.BadSocket;
                    }
                    break;
                case "M":
                    {
                        return (byte)BinDefine.MaskedSocket;
                    }
                    break;
                case "0":
                    {
                        return (byte)BinDefine.Empty;
                    }
                    break;
                case "1":
                    {
                        return (byte)BinDefine.Bin1;
                    }
                    break;
                case "2":
                    {
                        return (byte)BinDefine.Bin2;
                    }
                    break;
                case "3":
                    {
                        return (byte)BinDefine.Bin3;
                    }
                    break;
                case "4":
                    {

                        return (byte)BinDefine.Bin4;
                    }
                    break;
                case "5":
                    {

                        return (byte)BinDefine.Bin5;
                    }
                    break;
                case "6":
                    {

                        return (byte)BinDefine.Bin6;
                    }
                    break;
                case "7":
                    {

                        return (byte)BinDefine.Bin7;
                    }
                    break;
                case "8":
                    {

                        return (byte)BinDefine.Bin8;
                    }
                    break;
                case "9":
                    {

                        return (byte)BinDefine.Bin9;
                    }
                    break;
                case "10":
                    {

                        return (byte)BinDefine.Bin10;
                    }
                    break;
                case "11":
                    {

                        return (byte)BinDefine.Bin11;
                    }
                    break;
                case "12":
                    {

                        return (byte)BinDefine.Bin12;
                    }
                    break;
                case "13":
                    {

                        return (byte)BinDefine.Bin13;
                    }
                    break;
                case "14":
                    {

                        return (byte)BinDefine.Bin14;
                    }
                    break;
                case "15":
                    {

                        return (byte)BinDefine.Bin15;
                    }
                    break;
                default:
                    {
                        return (byte)BinDefine.Bin2;
                    }
                    break;

            }
            return (byte)BinDefine.Disabled;
        }



        public bool StrConvertToDiePakMap(TrayDataEx diepak)
        {
            bool ret = true;
            try
            {
                lock (Lock)
                {
                    string[] SplitMap1 = m_map.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (SplitMap1.Length <= 0)
                    {
                        return false;
                    }
                    int index1 = 0;

                    for (int j = 0; j < diepak.BlockCols; j++)
                    {
                        for (int i = 0; i < diepak.BlockRows; i++)
                        {
                            string[] SplitMap2 = SplitMap1[index1++].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            int index2 = 0;
                            
                             for (int l = 0; l < diepak.Cols; l++)
                            {
                                for (int k = 0; k < diepak.Rows; k++)    
                                {
                                    byte bin = StrToBin(SplitMap2[index2++]);
                                    UpdateBin((BinDefine)bin);
                                    diepak.SetBin(j, i, l, k, bin, false);
                                }
                            }
                        }
                    }

                }
                diepak.Refresh();
            }
            catch (Exception ex)
            {
                return false;
            }
            StopUpdate = false;
            return ret;

        }
        private void UpdateBin(BinDefine bin)
        {
            // 
            // {
            m_bininfo.Add(bin);
            //  }
        }
        public int GetBinCount(byte[] bin)
        {
            if (StopUpdate)
            {
                return 0;
            }
            lock (Lock)
            {
                int num = 0;
                for (int i = 0; i < bin.Length; i++)
                {
                    num += m_bininfo.Count(item => item == (BinDefine)bin[i]);
                }
                return num;
            }
        }

        public string ID
        {
            get
            {
                return m_id;
            }
        }
        public string TYPE
        {
            get
            {
                return m_type;
            }
        }
        public string MAP
        {
            get
            {
                return m_map;

            }
        }
    }

    public struct HEAD_INFO
    {
        public int Max_Pitch_X;
        public int Max_Pitch_Y;
        public int Min_Pitch_X;
        public int Min_Pitch_Y;
        public bool HasX;
        public bool HasY;
        public int Num_X;
        public int Num_Y;
    }
    #endregion

    //系統參數定義
    public class SYSPara
    {
        #region 架構使用 (全域共用參數定義)

        //前置設定參數 - setup
        public static string SysDir = ""; //系統路徑
        public static int Simulation; //是否開啟模擬
        public static string EQID; //設備代號
        public static int LightMode; //燈號顯示模式 0:依設備狀態顯示 1:依Alarm項目而定
        public static int ScreenMode; //0:自動縮放 1:1440x900 2:1024x768 3:1366x768
        public static int LoginMode; //使用者登入模式 0:通用登入 1:特定使用者登入
        public static string RunStartUpTM; //軟體開啟時間
        public static bool DebugMode; //Debug模式
        public static string VisionEXEFilePath; //視覺程式路徑
        public static bool UseDoubleScreen; //使用雙螢幕切換互鎖
        public static int LogMaxLine; //歷史記錄最大行數
        public static string LogPath; //歷史記綠路徑

        //時間記錄參數
        public static UInt64 OperationSecond; //機台開批後總花費時間
        public static UInt64 RunSecond; //機台實際運行時間
        public static UInt64 StopSecond; //機台暫停時間
        public static UInt64 AlarmSecond; //機台警報時間 
        public static UInt64 IdleTM; //機台開機後計時 IDLE 時間
        public static UInt64 ManualTM; //機台開機後計時 Manual 時間
        public static UInt64 MaintenanceTM; //機台維修累計時間
        public static UInt64 HomeTM; //機台開機後計時 Home 時間

        public static double ScanTimeEx; //MSS流程ScanTime
        public static long ScanTime; //機台每秒掃描次數
        public static double MaxScanTime; //記錄MSS流程最久執行時間

        //系統參數
        public static int LicenseMode; //授權模式
        public static string Licenser; //授權者
        public static string LicenseDate; //授權日期
        public static int LicenseDays; //授權天數

        public static bool InitialOk; //判斷 Initial 是否完成動作
        public static bool Lotend; //結批開關
        public static bool LotendOk; //結批完成
        public static bool ErrorStop; //錯誤停機
        public static bool HMIReady; //HMI啟動完成
        public static bool HMIClose; //HMI關閉

        private static bool mSysRun; //系統運行中
        public static bool SysRun //系統運行中
        {
            get { return mSysRun; }
            set
            {
                mSysRun = value;
                foreach (BaseModuleInterface module in FormSet.ModuleList)
                    module.SysRun = value;
            }
        }

        //目前運行狀態
        public static StateMode mSysState;
        public static StateMode SysState
        {
            get { return mSysState; }
            set
            {
                mSysState = value;
                //+ By Max For SECS
                int idx = SYSPara.OReadValue("CommProtocol").ToInt();
                if (idx == 1) //SECS
                {
                    switch (mSysState)
                    {
                        case StateMode.SM_INIT_RUN:
                            {
                                if (SYSPara.PackageName != "")
                                {
                                    FormSet.mGemF.ChangeProcessState(ProVGEMProcessFSM.EventID.EVT_INITIAL);
                                }
                                else
                                {
                                    FormSet.mGemF.ChangeProcessState(ProVGEMProcessFSM.EventID.EVT_HOME);
                                }
                            }
                            break;
                        case StateMode.SM_IDLE:
                            {
                                if (SYSPara.LotendOk)
                                {
                                    SYSPara.LotendOk = false;
                                    FormSet.mGemF.ChangeProcessState(ProVGEMProcessFSM.EventID.EVT_LOTEND);
                                }
                                else
                                {
                                    FormSet.mGemF.ChangeProcessState(ProVGEMProcessFSM.EventID.EVT_HOMEDONE);
                                }
                            }
                            break;
                        case StateMode.SM_AUTO_RUN:
                            {
                                if (SYSPara.SysRun)
                                {
                                    FormSet.mGemF.ChangeProcessState(ProVGEMProcessFSM.EventID.EVT_START);
                                }
                                else
                                {
                                    FormSet.mGemF.ChangeProcessState(ProVGEMProcessFSM.EventID.EVT_PAUSE);
                                }
                            }
                            break;
                        case StateMode.SM_PACKAGELOAD_RESET:
                            {
                                FormSet.mGemF.ChangeProcessState(ProVGEMProcessFSM.EventID.EVT_SETUP);
                            }
                            break;
                        case StateMode.SM_AUTO_RESET:
                            {
                                FormSet.mGemF.ChangeProcessState(ProVGEMProcessFSM.EventID.EVT_START);
                            }
                            break;
                        case StateMode.SM_PAUSE:
                            {
                                FormSet.mGemF.ChangeProcessState(ProVGEMProcessFSM.EventID.EVT_PAUSE);
                            }
                            break;
                        case StateMode.SM_ABORT:
                            {
                                FormSet.mGemF.ChangeProcessState(ProVGEMProcessFSM.EventID.EVT_ABORT);
                            }
                            break;
                        case StateMode.SM_MANUAL_RUN:
                            {
                                FormSet.mGemF.ChangeProcessState(ProVGEMProcessFSM.EventID.EVT_PAUSE);
                            }
                            break;
                    }
                }
                foreach (BaseModuleInterface module in FormSet.ModuleList)
                    module.SysState = (ProVIFM.StateMode)value;
            }
        }

        //運行模式
        public static RunModeDT mPreRunMode;
        public static RunModeDT mRunMode;
        public static RunModeDT RunMode
        {
            get { return mRunMode; }
            set
            {
                mPreRunMode = mRunMode;
                mRunMode = value;
                foreach (BaseModuleInterface module in FormSet.ModuleList)
                    module.RunMode = (ProVIFM.RunModeDT)value;
            }
        }

        //是否在生產模式
        private static bool mIsAutoMode = false;
        public static bool IsAutoMode
        {
            get { return mIsAutoMode; }
            set
            {
                mIsAutoMode = value;
                foreach (BaseModuleInterface module in FormSet.ModuleList)
                    module.IsAutoMode = value;
            }
        }

        //目前載入的產品名稱
        private static string mPackageName = "";
        public static string PackageName
        {
            get { return mPackageName; }
            set
            {
                mPackageName = value;
                foreach (BaseModuleInterface module in FormSet.ModuleList)
                    module.PackageName = value;
            }
        }

        //目前載入的生產批號
        public static string 生產批號 = " ";

        //+ By Max 20200204 For LogAnalyzer
        public static LogAnalyzer.LogDb logDB = new LogAnalyzer.LogDb();

        //核心參數
        public static bool 安全門開啟 = false; //安全門機制改善 2020/02/18 kent
        public static StateMode NowEQState; //設備目前的狀態
        public static StateMode PreEQState; //設備前一次的狀態
        public static StateThread thState; //狀態流程 Thread
        public static bool ManualControlIO; //手動操作模式
        public static bool MusicOn; //是否要控制音樂
        public static double CT; //Cycle Time
        public static AlarmSystem Alarm = new AlarmSystem(); //警報系統
        public static Language Lang; //語系系統
        public static Secret mSecret; //權限管理 //20210701 kent
        public static void LogSay(EnLoggerType LogType, params string[] Para) //Log記錄
        {
            if (Para == null)
                return;

            if (Para.Length <= 0)
                return;
            string str = string.Format("{1}\t{2}\t{0}", Para[0], SYSPara.PackageName, SYSPara.生產批號);
            FormSet.mLogF.LogSay(LogType, str);
        }
        public static void LogSay(string LogType, params string[] Para) //Log記錄
        {
            if (Para == null)
                return;

            if (Para.Length <= 0)
                return;

            string str = string.Format("{1}\t{2}\t{0}", Para[0], SYSPara.PackageName, SYSPara.生產批號);
            FormSet.mLogF.LogSay(LogType, str);
        }

        //呼叫指定模組內的指定函數功能
        public static object CallProc(string ModuleName, string FuncName, params object[] args)
        {
            foreach (BaseModuleInterface obj in FormSet.ModuleList)
            {
                if (obj.Name == ModuleName)
                {
                    try
                    {
                        object result = obj.GetType().GetMethod(FuncName).Invoke(obj, args);
                        return result;
                    }
                    catch (Exception)
                    {
                        SYSPara.Alarm.Say("E0506", string.Format("{0}.{1}", ModuleName, FuncName));
                        return null;
                    };
                }
            }
            return null;
        }

        //按鈕事件記錄
        public static string FindTabPageName(Control control)
        {
            Control temp = control;
            while (temp.Parent != null)
            {
                temp = temp.Parent;
                if (temp is TabPage)
                    return temp.Text;
            }
            return string.Empty;
        }
        public static string FindFormName(Control control)
        {
            Control temp = control;
            while (temp.Parent != null)
            {
                temp = temp.Parent;
                if (temp is Form)
                    return temp.Text;
            }
            return string.Empty;
        }
        public static void OPClickLog(object sender, EventArgs e)
        {
            if (sender is Button)
                LogSay(EnLoggerType.EnLog_OP, string.Format("User Click: {0}-{1}-{2}-{3}"
                    , FindFormName(((Button)sender))
                    , FindTabPageName(((Button)sender))
                    , ((Button)sender).Parent.Text
                    , ((Button)sender).Text));
            else if (sender is CheckBox)
                LogSay(EnLoggerType.EnLog_OP, string.Format("User Click: {0}-{1}-{2}-{3}"
                    , FindFormName(((CheckBox)sender))
                    , FindTabPageName(((CheckBox)sender))
                    , ((CheckBox)sender).Parent.Text
                    , ((CheckBox)sender).Text));
            else if (sender is OutBit)
                LogSay(EnLoggerType.EnLog_OP, string.Format("User Click: {0}-{1}-{2}-{3}, OldValue: {4}, NewValue: {5}"
                    , FindFormName(((OutBit)sender))
                    , FindTabPageName(((OutBit)sender))
                    , ((OutBit)sender).Parent.Text
                    , ((OutBit)sender).Text
                    , (!((OutBit)sender).Value).ToString()
                    , ((OutBit)sender).Value.ToString()));
        }
        public static void ScanButtonForLogCallBack(Control sender)
        {
            //處理 Stage Module
            foreach (Control control in sender.Controls)
            {
                if ((control is Button) || (control is CheckBox))
                    control.Click += OPClickLog;
                else if (control is OutBit)
                    ((OutBit)control).ProVOnClick += OPClickLog;

                if (control.HasChildren)
                    ScanButtonForLogCallBack(control);
            }
        }
        #endregion

        #region 資料表格讀寫
        //資料讀取
        public static DataTransfer OReadValue(string FieldName)
        {
            return FormSet.mOption.OptionDS.ReadValue(FieldName);
        }
        public static void OReadValue(string FieldName, ref object Value, ref string FolderName)
        {
            FormSet.mOption.OptionDS.ReadValue(FieldName, ref Value, ref FolderName);
        }
        public static DataTable OReadTable(string FieldName)
        {
            return FormSet.mOption.OptionDS.ReadTable(FieldName);
        }
        public static DataTransfer PReadValue(string FieldName)
        {
            return FormSet.mPackage.PackageDS.ReadValue(FieldName);
        }
        public static void PReadValue(string FieldName, ref object Value, ref string FolderName)
        {
            FormSet.mPackage.PackageDS.ReadValue(FieldName, ref Value, ref FolderName);
        }
        public static DataTable PReadTable(string FieldName)
        {
            return FormSet.mPackage.PackageDS.ReadTable(FieldName);
        }
        public static DataTransfer SetupReadValue(string FieldName)
        {
            return FormSet.m內參設定.OptionDS.ReadValue(FieldName);
        }
        public static void SetupReadValue(string FieldName, ref object Value, ref string FolderName)
        {
            FormSet.m內參設定.OptionDS.ReadValue(FieldName, ref Value, ref FolderName);
        }
        public static DataTable SetupReadTable(string FieldName)
        {
            return FormSet.m內參設定.OptionDS.ReadTable(FieldName);
        }

        //資料寫入
        public static void OSetValue(string FieldName, object value)
        {
            FormSet.mOption.OptionDS.SetData(FieldName, "String", value);
            FormSet.mOption.OptionDS.SaveFile();
        }
        public static void OSetSubValue(string FieldName, object value, string FolderName)
        {
            //如FolderName為空值，則取目前設定值
            if (string.IsNullOrEmpty(FolderName))
            {
                object valuetemp = null;
                OReadValue(FieldName, ref valuetemp, ref FolderName);
            }
            FormSet.mOption.OptionDS.SetSubData(FieldName, "SubPackage", value, FolderName);
            FormSet.mOption.OptionDS.SaveFile();
        }
        public static void OSetTable(string FieldName, object value)
        {
            FormSet.mOption.OptionDS.SetData(FieldName, "Table", value);
            FormSet.mOption.OptionDS.SaveFile();
        }
        public static void PSetValue(string FieldName, object value)
        {
            //+ By Max 20191121 For Package Operation in Auto Mode
            FormSet.mPackage.PackageDS.SetData(FieldName, "String", value);
            FormSet.mPackage.PackageDS.SaveFile();
        }
        public static void PSetSubValue(string FieldName, object value, string FolderName)
        {
            //如FolderName為空值，則取目前設定值
            if (string.IsNullOrEmpty(FolderName))
            {
                object valuetemp = null;
                PReadValue(FieldName, ref valuetemp, ref FolderName);
            }
            FormSet.mPackage.PackageDS.SetSubData(FieldName, "SubPackage", value, FolderName);
            FormSet.mPackage.PackageDS.SaveFile();
        }
        public static void PSetTable(string FieldName, object value)
        {
            FormSet.mPackage.PackageDS.SetData(FieldName, "Table", value);
            FormSet.mPackage.PackageDS.SaveFile();
        }
        public static void SetupSetValue(string FieldName, object value)
        {
            FormSet.m內參設定.OptionDS.SetData(FieldName, "String", value);
            FormSet.m內參設定.OptionDS.SaveFile();
        }
        public static void SetupSetSubValue(string FieldName, object value, string FolderName)
        {
            //如FolderName為空值，則取目前設定值
            if (string.IsNullOrEmpty(FolderName))
            {
                object valuetemp = null;
                SetupReadValue(FieldName, ref valuetemp, ref FolderName);
            }
            FormSet.m內參設定.OptionDS.SetSubData(FieldName, "SubPackage", value, FolderName);
            FormSet.m內參設定.OptionDS.SaveFile();
        }
        public static void SetupSetTable(string FieldName, object value)
        {
            FormSet.m內參設定.OptionDS.SetData(FieldName, "Table", value);
            FormSet.m內參設定.OptionDS.SaveFile();
        }

        //子表格讀取
        public static DataTransfer OReadValue(string FieldName, string SubFieldName)
        {
            return FormSet.mOption.OptionDS.ReadValue(FieldName, SubFieldName);
        }
        public static DataTable OReadTable(string FieldName, string SubFieldName)
        {
            return FormSet.mOption.OptionDS.ReadTable(FieldName, SubFieldName);
        }
        public static DataTransfer PReadValue(string FieldName, string SubFieldName)
        {
            return FormSet.mPackage.PackageDS.ReadValue(FieldName, SubFieldName);
        }
        public static DataTable PReadTable(string FieldName, string SubFieldName)
        {
            return FormSet.mPackage.PackageDS.ReadTable(FieldName, SubFieldName);
        }
        public static DataTransfer SetupReadValue(string FieldName, string SubFieldName)
        {
            return FormSet.m內參設定.OptionDS.ReadValue(FieldName, SubFieldName);
        }
        public static DataTable SetupReadTable(string FieldName, string SubFieldName)
        {
            return FormSet.m內參設定.OptionDS.ReadTable(FieldName, SubFieldName);
        }

        //子表格寫入
        public static void OSetValue(string FieldName, string SubFieldName, object value)
        {
            FormSet.mOption.OptionDS.SetData(FieldName, SubFieldName, "String", value);
        }
        public static void OSetTable(string FieldName, string SubFieldName, object value)
        {
            FormSet.mOption.OptionDS.SetData(FieldName, SubFieldName, "Table", value);
        }
        public static void PSetValue(string FieldName, string SubFieldName, object value)
        {
            FormSet.mPackage.PackageDS.SetData(FieldName, SubFieldName, "String", value);
            FormSet.mPackage.PackageDS.SaveFile();
        }
        public static void PSetTable(string FieldName, string SubFieldName, object value)
        {
            FormSet.mPackage.PackageDS.SetData(FieldName, SubFieldName, "Table", value);
            FormSet.mPackage.PackageDS.SaveFile();
        }
        public static void SetupSetValue(string FieldName, string SubFieldName, object value)
        {
            FormSet.m內參設定.OptionDS.SetData(FieldName, SubFieldName, "String", value);
        }
        public static void SetupSetTable(string FieldName, string SubFieldName, object value)
        {
            FormSet.m內參設定.OptionDS.SetData(FieldName, SubFieldName, "Table", value);
        }
        #endregion

        #region ShowAlarm
        public static void ShowAlarm(String ErrType, int ErrNo)
        {
            Alarm.Say(string.Format("{0}{1:0000}", ErrType, ErrNo));
        }
        public static void ShowAlarm(String ErrType, int ErrNo, Control Sender)
        {
            Alarm.Say(string.Format("{0}{1:0000}", ErrType, ErrNo), Sender);
        }
        public static void ShowAlarm(String ErrType, int ErrNo, params Object[] args)
        {
            Alarm.Say(string.Format("{0}{1:0000}", ErrType, ErrNo), args);
        }
        public static void ShowAlarm(String ErrType, int ErrNo, Control Sender, params Object[] args)
        {
            Alarm.Say(string.Format("{0}{1:0000}", ErrType, ErrNo), Sender, args);
        }
        public static void AlarmReset()
        {
            Alarm.ClearAll();
        }
        #endregion

        #region 使用者修改
        public static PRODUCTION_MODE SystemProductionMode = PRODUCTION_MODE.NONE;
        public static LinkMode ProductionLinkMode = LinkMode.OFFLINE;
        public static LOT_INFO NewLotInfo = new LOT_INFO();
        public static BoardBinInfo CurrentBoard = new BoardBinInfo();
        public static string CurrentBIBID { get; set; }
        public static string StageA_BIBID { get; set; }
        public static string StageB_BIBID { get; set; }
        public static string StageA_Map { get; set; }
        public static string StageB_Map { get; set; }

        #endregion
    }

    //通用視窗集合
    public class FormSet
    {
        #region 架構使用 (全域共用視窗定義)

        public static MainF1 mMainF1; //主視窗(寬螢幕使用)
        public static MainF2 mMainF2; //主視窗(觸控使用)
        public static MainF3 mMainF3; //主視窗(寬螢幕觸控使用)
        public static UserLoginF mUserLogin; //使用者登入視窗
        public static UserLoginEx mUserLoginEx; //特定的使用者登入視窗
        public static InitialForm mInitialF; //歸零視窗
        public static HistoryLogger mLogF; //歷史記錄視窗
        public static MSS mMSS; //中介層視窗
        public static OptionF mOption; //通用設定視窗
        public static 內參設定 m內參設定; //內參設定頁面
        public static PackageF mPackage; //產品設定視窗
        //public static PackageExF mPackage; //產品設定視窗(資料夾版本)
        public static ModuleContainerForm mModuleContainer; //模組容器
        public static PackageSelectF mPackageSelF; //產品規格選擇視窗
        //public static PackageSelectExF mPackageSelF; //產品規格選擇視窗(資料夾版本)
        public static BannerForm mBanner; //Splash Form
        public static List<object> ModuleList = new List<object>(); //模組視窗清單
        public static MainFlowF mMainFlow; //主控流程視窗
        public static KeyPad mKeyPad; //小鍵盤
        public static LotStartForm mLotStartF; //開批流程
        public static LotEndForm mLotEndF; //結批流程

        //+ By Max For SECS
        public static ProVGemF mGemF;

        #endregion

        #region 使用者修改 (關聯式資料庫/其他使用者定義的視窗)
        public static MainFlowSelectForm mMainFlowDialogForm;
        public static BoardTable mBoardTable;
        public static DeviceTable mDeviceTable;
        public static KitTable mKitTable;
        public static TrayTable mTrayTable;
        public static CassetteTable mCassetteTable;
        //public static ToolF mToolF;

        #endregion
    }
}
