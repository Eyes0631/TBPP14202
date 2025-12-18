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
using CommonObj;
using PaeLibGeneral;
using PaeLibProVSDKEx;


namespace TRM
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
    public partial class TRM : BaseModuleInterface,TRM_interface
    {
        class BasicPosInfo
        {
            public int Top_Z { get; set; }
            public int Down_Z { get; set; }
            public int Check_Z { get; set; }
            public int Safe_X { get; set; }
            public int Station_X { get; set; }
            public int Safe_Y { get; set; }
            public int BIB_Y { get; set; }
            public int Station_U { get; set; }
            public int Detect_Y { get; set; }
            public int Read_Y { get; set; }
        }

        public enum TRM_ALARM_CODE
        {
            ER_TRM_UNKNOW = 0,
            ER_TRM_BOARDDETECT = 1,
            ER_TRM_SCANDETECT = 2,
            ER_TRM_BOARDCYLINDER = 3,
            ER_TRM_AXISU_NOSAFESTOP = 4,

        }

        public TRM()
        {
            InitializeComponent();
            CreateComponentList();            
        }

        #region 繼承函數

        //模組解構使用
        public override void DisposeTH()
        {
            if (br != null)
            {
                br.Close();
                br = null;
            }
            base.DisposeTH();
        }

        //程式初始化 
        public override void Initial() 
        {
            Board_Cylinder = new Cylinder(OB_Board_Cylinder, IB_Board_Cylinder_On, IB_Board_Cylinder_Off);
            DI_Detect_Board = new DigitalInput(IB_BoardDetect);
            DI_Detect_Scan = new DigitalInput(IB_Scan_BoardDetect);

            MT_AxisX = MT_TRM_AxisX;
            MT_AxisY = MT_TRM_AxisY;
            MT_AxisZ = MT_TRM_AxisZ;
            MT_AxisU = MT_TRM_AxisU;

            BT_BasicPos = new BasicPosInfo();

            Now_Station = TRMStation.NONE;
            Last_Station = TRMStation.NONE;
            Station_Dir = TRMStation.NONE;
            TRM_State = TRMState.NONE;
            TRM_ActionResult = ThreeValued.UNKNOWN;
            iBarCodeReaderComPort = SReadValue("iBarCodeReaderComPort").ToInt(); //讀BarCodeReader_COMPORT
            br = new JBarcodeReader();
            bOpenBar = br.Open(2, Convert.ToByte(iBarCodeReaderComPort));

        }

        //持續偵測函數
        public override void AlwaysRun() 
        {
            bool b1 = (MT_AxisU != null && MT_AxisU.Busy());
            if (b1)
            {
                if (MT_AxisY.IsHomeOn.Equals(false) && IsSimulation().Equals(0))
                {
                    StopMotor();
                    ShowAlarmMessage("E", (int)TRM_ALARM_CODE.ER_TRM_AXISU_NOSAFESTOP);
                }
            }
        } //持續掃描
        public override int ScanIO() { return 0; } //掃描硬體按鈕IO

        //歸零相關函數
        public override void HomeReset() 
        {
            mLotend = false;
            mLotendOk = false;
            mCanHome = false;
            mHomeOk = false;
            Last_Station = TRMStation.NONE;
            Station_Dir = TRMStation.NONE;
            FC_TRM_HOME.TaskReset();
        } //歸零前的重置
        public override bool Home() 
        {
            FC_TRM_HOME.MainRun();
            return mHomeOk; 
        } //歸零

        //運轉相關函數
        public override void ServoOn() 
        {
            if (MT_TRM_AxisX != null) { MT_TRM_AxisX.ServoOn(true); }
            if (MT_TRM_AxisY != null) { MT_TRM_AxisY.ServoOn(true); }
            if (MT_TRM_AxisZ != null) { MT_TRM_AxisZ.ServoOn(true); }
            if (MT_TRM_AxisU != null) { MT_TRM_AxisU.ServoOn(true); }
        } //Motor Servo On
        public override void ServoOff() 
        {
            if (MT_TRM_AxisX != null) { MT_TRM_AxisX.ServoOn(false); }
            if (MT_TRM_AxisY != null) { MT_TRM_AxisY.ServoOn(false); }
            if (MT_TRM_AxisZ != null) { MT_TRM_AxisZ.ServoOn(false); }
            if (MT_TRM_AxisU != null) { MT_TRM_AxisU.ServoOn(false); }
        } //Motor Servo Off
        public override void RunReset() 
        {
            FC_TRM_GET.TaskReset();
            FC_TRM_PUT.TaskReset();
            FC_TRM_MOVE.TaskReset();
            FC_TRM_READ.TaskReset();
            
        } //運轉前初始化
        public override void Run() 
        {
            FC_TRM_GET.MainRun();
            FC_TRM_PUT.MainRun();
            FC_TRM_MOVE.MainRun();
            FC_TRM_READ.MainRun();
        } //運轉
        public override void SetSpeed(bool bHome = false) 
        {
            int SPD = 10000;
            int ACC_MULTIPLE = 100000;
            int ACC_DEC = 100000;
            int SPEED_RATE = OReadValue("機台速率").ToInt();

            if (MT_TRM_AxisX != null)
            {
                if (!bHome)
                {
                    SPD = (SReadValue("MT_TRM_X_SPEED").ToInt() * SPEED_RATE) / 100;
                    ACC_MULTIPLE = SReadValue("MT_TRM_X_ACC_MULTIPLE").ToInt();
                    ACC_DEC = (SPD * ACC_MULTIPLE);
                }
                MT_TRM_AxisX.SetSpeed(SPD);
                MT_TRM_AxisX.SetAcceleration(ACC_DEC);
                MT_TRM_AxisX.SetDeceleration(ACC_DEC);
            }

            if (MT_TRM_AxisY != null)
            {
                if (!bHome)
                {
                    SPD = (SReadValue("MT_TRM_Y_SPEED").ToInt() * SPEED_RATE) / 100;
                    ACC_MULTIPLE = SReadValue("MT_TRM_Y_ACC_MULTIPLE").ToInt();
                    ACC_DEC = (SPD * ACC_MULTIPLE);
                }
                MT_TRM_AxisY.SetSpeed(SPD);
                MT_TRM_AxisY.SetAcceleration(ACC_DEC);
                MT_TRM_AxisY.SetDeceleration(ACC_DEC);
            }

            if (MT_TRM_AxisZ != null)
            {
                if (!bHome)
                {
                    SPD = (SReadValue("MT_TRM_Z_SPEED").ToInt() * SPEED_RATE) / 100;
                    ACC_MULTIPLE = SReadValue("MT_TRM_Z_ACC_MULTIPLE").ToInt();
                    ACC_DEC = (SPD * ACC_MULTIPLE);
                }
                MT_TRM_AxisZ.SetSpeed(SPD);
                MT_TRM_AxisZ.SetAcceleration(ACC_DEC);
                MT_TRM_AxisZ.SetDeceleration(ACC_DEC);
            }

            if (MT_TRM_AxisU != null)
            {
                if (!bHome)
                {
                    SPD = (SReadValue("MT_TRM_U_SPEED").ToInt() * SPEED_RATE) / 100;
                    ACC_MULTIPLE = SReadValue("MT_TRM_U_ACC_MULTIPLE").ToInt();
                    ACC_DEC = (SPD * ACC_MULTIPLE);
                }
                MT_TRM_AxisU.SetSpeed(SPD);
                MT_TRM_AxisU.SetAcceleration(ACC_DEC);
                MT_TRM_AxisU.SetDeceleration(ACC_DEC);
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
        private int DegreeToPluse(int Degree)
        {
            double Resolution = SReadValue("Pos_TRM_RotateResolution").ToInt();//SSet->FindField("Roar_Resolution")->AsInteger;
            double Pluse = ((double)Degree / 360) * Resolution * -1;
            return (int)Pluse;
        }

        private void LogDebug(string module, string msg)
        {
            bool EnableDebugLog = true;
            if (EnableDebugLog && GetUseModule())
            {
                JLogger.LogDebug("Debug", module + " | " + msg);
            }
        }

        /// <summary>
        /// Loads the basic position data.
        /// </summary>
        private void LoadBasicData(TRMStation Station)
        {
            #region 【USER_TODO : 載入基準點位】
            switch (Station)
            {
                case TRMStation.STAGE_A:
                    {
                        BT_BasicPos.Top_Z = SReadValue("Pos_StageA_Top_Z").ToInt();
                        BT_BasicPos.Down_Z = SReadValue("Pos_StageA_Down_Z").ToInt();
                        BT_BasicPos.Check_Z = SReadValue("Pos_StageA_Check_Z").ToInt();
                        BT_BasicPos.Station_X = SReadValue("Pos_StageA_Station_X").ToInt();
                        BT_BasicPos.Safe_Y = SReadValue("Pos_Safe_Y").ToInt();
                        BT_BasicPos.BIB_Y = SReadValue("Pos_StageA_BIB_Y").ToInt();
                        BT_BasicPos.Detect_Y = SReadValue("Pos_StageA_BIBDetect_Y").ToInt();
                        BT_BasicPos.Station_U = SReadValue("Pos_StageA_Station_U").ToInt();
                    }
                    break;
                case TRMStation.STAGE_B:
                    {
                        BT_BasicPos.Top_Z = SReadValue("Pos_StageB_Top_Z").ToInt();
                        BT_BasicPos.Down_Z = SReadValue("Pos_StageB_Down_Z").ToInt();
                        BT_BasicPos.Check_Z = SReadValue("Pos_StageB_Check_Z").ToInt();
                        BT_BasicPos.Station_X = SReadValue("Pos_StageB_Station_X").ToInt();
                        BT_BasicPos.Safe_Y = SReadValue("Pos_Safe_Y").ToInt();
                        BT_BasicPos.BIB_Y = SReadValue("Pos_StageB_BIB_Y").ToInt();
                        BT_BasicPos.Detect_Y = SReadValue("Pos_StageB_BIBDetect_Y").ToInt();
                        BT_BasicPos.Station_U = SReadValue("Pos_StageB_Station_U").ToInt();
                    }
                    break;
                case TRMStation.STAGE_CH:
                    {
                        BT_BasicPos.Top_Z = SReadValue("Pos_ClamSheel_Top_Z").ToInt();
                        BT_BasicPos.Down_Z = SReadValue("Pos_ClamSheel_Down_Z").ToInt();
                        BT_BasicPos.Check_Z = SReadValue("Pos_ClamSheel_Check_Z").ToInt();
                        BT_BasicPos.Station_X = SReadValue("Pos_ClamSheel_Station_X").ToInt();
                        BT_BasicPos.Safe_Y = SReadValue("Pos_Safe_Y").ToInt();
                        BT_BasicPos.BIB_Y = SReadValue("Pos_ClamSheel_BIB_Y").ToInt();
                        BT_BasicPos.Detect_Y = SReadValue("Pos_ClamSheel_BIBDetect_Y").ToInt();
                        BT_BasicPos.Station_U = SReadValue("Pos_Clamsheel_Station_U").ToInt();
                    }
                    break;
                case TRMStation.LOAD_A:
                    {
                        BT_BasicPos.Top_Z = SReadValue("Pos_LoadA_Top_Z").ToInt();
                        BT_BasicPos.Down_Z = SReadValue("Pos_LoadA_Down_Z").ToInt();
                        BT_BasicPos.Check_Z = SReadValue("Pos_LoadA_Check_Z").ToInt();
                        BT_BasicPos.Station_X = SReadValue("Pos_LoadA_Station_X").ToInt();
                        BT_BasicPos.Safe_Y = SReadValue("Pos_Safe_Y").ToInt();
                        BT_BasicPos.BIB_Y = SReadValue("Pos_LoadA_BIB_Y").ToInt();
                        BT_BasicPos.Detect_Y = SReadValue("Pos_LoadA_BIBDetect_Y").ToInt();
                        BT_BasicPos.Station_U = SReadValue("Pos_LoadA_Station_U").ToInt();
                    }
                    break;
                case TRMStation.LOAD_B:
                    {
                        BT_BasicPos.Top_Z = SReadValue("Pos_LoadB_Top_Z").ToInt();
                        BT_BasicPos.Down_Z = SReadValue("Pos_LoadB_Down_Z").ToInt();
                        BT_BasicPos.Check_Z = SReadValue("Pos_LoadB_Check_Z").ToInt();
                        BT_BasicPos.Station_X = SReadValue("Pos_LoadB_Station_X").ToInt();
                        BT_BasicPos.Safe_Y = SReadValue("Pos_Safe_Y").ToInt();
                        BT_BasicPos.BIB_Y = SReadValue("Pos_LoadB_BIB_Y").ToInt();
                        BT_BasicPos.Detect_Y = SReadValue("Pos_LoadB_BIBDetect_Y").ToInt();
                        BT_BasicPos.Station_U = SReadValue("Pos_LoadB_Station_U").ToInt();
                    }
                    break;
                case TRMStation.READBOARCODE:
                    {
                        BT_BasicPos.Check_Z = SReadValue("Pos_BARCODE_Check_Z").ToInt();
                        BT_BasicPos.Station_X = SReadValue("Pos_BARCODE_Station_X").ToInt();
                        BT_BasicPos.Station_U = SReadValue("Pos_BARCODE_Station_U").ToInt();
                        BT_BasicPos.Read_Y = SReadValue("Pos_BARCODE_Station_Y").ToInt();
                    }
                    break;
                case TRMStation.WAITING:
                    {
                        BT_BasicPos.Station_X = SReadValue("Pos_Waiting_X").ToInt();
                    }
                    break;

            }
            BT_BasicPos.Safe_X = SReadValue("Pos_Safe_X").ToInt();
            

            #endregion
        }

        private void ShowAlarmMessage(string AlarmLevel, int ErrorCode, params object[] args)   //Alarm顯示
        {
            ShowAlarm(AlarmLevel, ErrorCode, args);
        }


        private bool MoveToX(int iX)
        {
            if (!GetUseModule() || (IsSimulation() != 0))
            {
                return true;
            }

            bool B1 = true;
            if (MT_AxisX != null)
            {
                B1 = MT_AxisX.G00(iX);
            }
            if (B1)
            {
                return true;
            }
            return false;
        }

        private bool MoveToY(int iY)
        {
            if (!GetUseModule() || (IsSimulation() != 0))
            {
                return true;
            }

            bool B1 = true;
            if (MT_AxisY != null)
            {
                B1 = MT_AxisY.G00(iY);
            }
            if (B1)
            {
                return true;
            }

            return false;
        }

        private bool MoveToZ(int iZ)
        {
            if (!GetUseModule() || (IsSimulation() != 0))
            {
                return true;
            }

            bool B1 = true;
            if (MT_AxisZ != null)
            {
                B1 = MT_AxisZ.G00(iZ);
            }
            if (B1)
            {
                return true;
            }

            return false;
        }

        private bool MoveToU(int iU)
        {
            if (!GetUseModule() || (IsSimulation() != 0))
            {
                return true;
            }

            bool B1 = true;
            if (MT_AxisU != null)
            {
                B1 = MT_AxisU.G00(iU);
            }
            if (B1)
            {
                return true;
            }
            return false;
        }

        private bool BoardCyControl(bool state, int delay = 100, int timeOut = 5000)
        {
            ThreeValued r1;

            if (!GetUseModule() || (IsSimulation() != 0)) //|| SystemDryRun)
            {
                return true;
            }

            if (state)
            {
                r1 = Board_Cylinder.On(delay, timeOut);
            }
            else
            {
                r1 = Board_Cylinder.Off(delay, timeOut);
            }

            if (r1.Equals(ThreeValued.TRUE))
            {
                return true;
            }
            else if (r1.Equals(ThreeValued.FALSE))
            {
                //alarm
                //ShowAlarmMessage("E", (int)Board_Cylinder.ER_CHM_STAGECYLINDER_TIMEOUT);
                return false;
            }

            return false;
        }

        private bool WorkBoardDetect(bool bSensor)
        {
            if (IsSimulation() != 0)
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
                    return DI_Detect_Board.ValueOn;
                }
                else
                {
                    return DI_Detect_Board.ValueOff;
                }
            }
        }

        private bool WorkScanDetect(bool bSensor)
        {
            if (IsSimulation() != 0)
            {
                if (bSensor)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            else
            {
                if (bSensor)
                {
                    return DI_Detect_Scan.ValueOn;
                }
                else
                {
                    return DI_Detect_Scan.ValueOff;
                }
            }
        }
       
        #endregion

        #region 公用函數
        public string ReaderBarcode()
        {
            string sRet = "";
            if (br != null)
            {
                sRet = br.ReadBarcode();
                if (sRet != "#BUSY#")
                {
                    br.ReadBarcodeTaskReset();
                    if (sRet == "!TIMEOUT!" && sRet == "!ERROR!")
                    {
                        sRet = "";
                    }
                }
            }
            return sRet;
                 //if (br1 != null)
                 //           {
                 //               sRet = br1.ReadBarcode();
                 //               if (sRet != "#BUSY#")
                 //               {
                 //                   br1.ReadBarcodeTaskReset();
                 //                   if (sRet != "!TIMEOUT!" && sRet != "!ERROR!" && CheckBoardType(sRet))
                 //                   {
                 //                       DiePakExchanger_Top.SetDiePakInfoID(sRet);
                 //                       DiePakExchanger_Elevator_Z.SetSpeed(axis_z_apeed);


                 //                       int index = CarrierList.FindIndex(c => c.Level == DiePakExchange_iMoveBackSlot);
                 //                       DiePakInfo TempInfo = new DiePakInfo();
                 //                       TempInfo = CarrierList[index].Clone();
                 //                       TempInfo.BID = sRet;
                 //                       CarrierList[index] = TempInfo.Clone();
        }

        public void BarCodeReaderReset()
        {
            if (br != null)
            {
                br.ReadBarcodeTaskReset();
            }
        }

        public void SetCanRunHome()
        {
            mCanHome = true;
        }

        public void DataReset()
        {
            SetSpeed();
            SystemDryRun = OReadValue("DryRun").ToBoolean();
            SystemDryRun_NoBoard = OReadValue("DryRun_NoBoard").ToBoolean();
            SystemDryRun_NoDevice = OReadValue("DryRun_NoDevice").ToBoolean();
        }

        public bool IsTRMSafety()
        {
            if (IsSimulation() != 0)
            {
                return true;
            }

            return !MT_AxisX.IsHomeOn && MT_AxisY.IsHomeOn;
            //if (MT_AxisX.IsHomeOn)
            //{
            //    return ThreeValued.TRUE;
            //}
            //else
            //{
            //    return ThreeValued.FALSE;
            //}
        }

        public TRMState GetTransferState()
        {
            return TRM_State;
        }

        public ThreeValued SetTransferState(TRMState state)
        {
            TRM_State = state;
            return ThreeValued.TRUE;
        }

        public string GetBarcodeValue()
        {
            return BarcodeValue;
        }

        //Mars modifyed 20220223
        //public ThreeValued SetActionCommand_TRM(TRMStation Station, ACTIONMODE Mode, BasePosInfo Pos)
        public ThreeValued SetActionCommand_TRM(TRMStation Station, ACTIONMODE Mode)
        {
            TRM_Command = ThreeValued.FALSE;

            if (this.GetUseModule().Equals(false))
            {
                return ThreeValued.TRUE;
            }

            //Mars add 其他動作執行時不可接受任何動作
            if (Flag_GetAction.IsDoing() || Flag_PutAction.IsDoing() || Flag_MoveAction.IsDoing() || Flag_ReadAction.IsDoing())
            {
                return ThreeValued.FALSE;
            }

            if (Mode == ACTIONMODE.GET)
            {
                Flag_GetAction.DoIt();
                TRM_Command = ThreeValued.TRUE;
            }

            if (Mode == ACTIONMODE.PUT)
            {
                Flag_PutAction.DoIt();
                TRM_Command = ThreeValued.TRUE;
            }

            if (Mode == ACTIONMODE.MOVE)
            {
                Flag_MoveAction.DoIt();
                TRM_Command = ThreeValued.TRUE;
            }

            if (Mode == ACTIONMODE.READ)
            {
                Flag_ReadAction.DoIt();
                TRM_Command = ThreeValued.TRUE;
            }

            Now_Station = Station;
            LoadBasicData(Station);
            if (TRM_Command.Equals(ThreeValued.TRUE))
            {
                LogDebug("TRM", string.Format("SetActionCommand_TRM({0},{1}) return true.", Station.ToString(), Mode.ToString()));
            }
            return TRM_Command;
        }

        public ThreeValued GetActionResult_TRM()
        {
            if (GetUseModule().Equals(false))
            {
                LogDebug("TRM", string.Format("GetResut_TRM return true."));
                return ThreeValued.TRUE;
            }

            return TRM_ActionResult;
        }

        #endregion

        #region 流程相關變數

        private JActionFlag Flag_GetAction = new JActionFlag();
        private JActionFlag Flag_PutAction = new JActionFlag();
        private JActionFlag Flag_MoveAction = new JActionFlag();
        private JActionFlag Flag_ReadAction = new JActionFlag();
        private BasicPosInfo BT_BasicPos = new BasicPosInfo();
        private TRMStation Now_Station;
        private TRMStation Last_Station;
        private TRMStation Station_Dir;
        private ThreeValued TRM_Command;
        private ThreeValued TRM_ActionResult;
        private TRMState TRM_State;

        private BasePosInfo nowPos = new BasePosInfo();

        private JBarcodeReader br;
        private int iBarCodeReaderComPort = 0;      //讀BarCodeReader_COMPORT
        private bool bOpenBar = true;
        #endregion

        #region 氣缸控制
        private Cylinder Board_Cylinder = null;

        #endregion

        #region 元件
        private Motor MT_AxisX = null;  //移載台車X軸馬達
        private Motor MT_AxisY = null;  //移載台車Y軸馬達
        private Motor MT_AxisZ = null;  //移載台車Z軸馬達
        private Motor MT_AxisU = null;  //移載台車U軸旋轉台

        private DigitalInput DI_Detect_Board = null;
        private DigitalInput DI_Detect_Scan = null;
        bool SystemDryRun = false;
        bool SystemDryRun_NoBoard = false;
        bool SystemDryRun_NoDevice = false;

        #endregion

        #region TRM 歸零流程
        private FlowChart.FCRESULT FC_TRM_HOME_Run()
        {
            //TRM 歸零流程 - Transfer Shuttle Start to Home

            if (MT_AxisX != null) { MT_AxisX.HomeReset(); }
            if (MT_AxisY != null) { MT_AxisY.HomeReset(); }
            if (MT_AxisZ != null) { MT_AxisZ.HomeReset(); }
            if (MT_AxisU != null) { MT_AxisU.HomeReset(); }
            return FlowChart.FCRESULT.NEXT;
        }   

        private FlowChart.FCRESULT flowChart1_Run()
        {
            //TRM 歸零流程 - can Home?
            if (mCanHome || IsSimulation() != 0)
            {
                MT_AxisX.HomeReset();
                MT_AxisY.HomeReset();
                MT_AxisZ.HomeReset();
                MT_AxisU.HomeReset();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }        

        private FlowChart.FCRESULT flowChart2_Run()
        {
            //TRM 歸零流程 - Cylinder Control ON
            Board_Cylinder.Simulation = (IsSimulation() != 0);
            Board_Cylinder.On();
            return FlowChart.FCRESULT.NEXT;
            //if (!Board_Cylinder.OffSensorValue)
            //if (Board_Cylinder.OnSensorValue)

            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart3_Run()
        {
            //TRM 歸零流程 - Axis Y Home
            if (MT_AxisY == null)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            if (MT_AxisY.Home())
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart4_Run()
        {
            //TRM 歸零流程 - Axis X Home
            //if (MT_AxisX == null)
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //if (MT_AxisX.Home())
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //return FlowChart.FCRESULT.IDLE;
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart5_Run()
        {

            //TRM 歸零流程 - Axis Z&U Home
            bool b1 = true;
            bool b2 = true;
            bool b3 = true;
            if (MT_AxisZ != null) { b1 = MT_AxisZ.Home(); }
            if (MT_AxisU != null) { b2 = MT_AxisU.Home(); }
            if (MT_AxisX != null) { b3 = MT_AxisX.Home(); }

            if (b1 && b2 && b3)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart6_Run()
        {
            //TRM 歸零流程 - Check Have BIB
            bool b1 = WorkBoardDetect(false);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            else
            {
                ShowAlarmMessage("E", (int)TRM_ALARM_CODE.ER_TRM_BOARDDETECT);
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart7_Run()
        {
            //TRM 歸零流程 - Cylinder Control Off
            bool b1 = BoardCyControl(false);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart8_Run()
        {
            //TRM 歸零流程 - Done
            mHomeOk = true;
            return FlowChart.FCRESULT.IDLE;
        }
        #endregion

        #region TRM Get Action
        private FlowChart.FCRESULT FC_TRM_GET_Run()
        {
            //TRM Get Action - Get Start
            Flag_GetAction.Reset();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart9_Run()
        {
            //TRM Get Action - Need Get?
            if (Flag_GetAction.IsDoIt())
            {
                TRM_ActionResult = ThreeValued.UNKNOWN;
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart10_Run()
        {
            //TRM Get Action - Axis Z Move and Check have BIB?
            if (!Station_Dir.Equals(TRMStation.DIRECT_DOWN)) return FlowChart.FCRESULT.NEXT;

            bool b1 = MoveToZ(BT_BasicPos.Check_Z);
            bool b2 = MoveToY(BT_BasicPos.Detect_Y);
            //if (MoveToZ(BT_BasicPos.Check_Z))
            if (b1 && b2)
            {
                if (WorkScanDetect(true) || (SystemDryRun && SystemDryRun_NoBoard))
                {  
                    //Mars Check Have BIB Is Need move Y?
                    LogDebug("TRM", string.Format("Get Action Z move to check have BIB: " + BT_BasicPos.Check_Z.ToString()));
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart11_Run()
        {
            //TRM Get Action - Axis Z Move to Down Position
            if (MoveToZ(BT_BasicPos.Down_Z))
            {
                LogDebug("TRM", string.Format("Get Action Z move to Down Position: " + BT_BasicPos.Down_Z.ToString()));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart12_Run()
        {
            //TRM Get Action - Axis Y Move to BIB Position
            if (MoveToY(BT_BasicPos.BIB_Y))
            {
                SetSpeed(true);
                LogDebug("TRM", string.Format("Get Action Y move to BIB Position: " + BT_BasicPos.BIB_Y.ToString()));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart13_Run()
        {
            //TRM Get Action - Axis Z Move to Top Position
            ContinueRun = true;
            if (MoveToZ(BT_BasicPos.Top_Z))
            {
                SetSpeed(false);
                ContinueRun = false;
                LogDebug("TRM", string.Format("Get Action Z move to Top Position: " + BT_BasicPos.Top_Z.ToString()));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart14_Run()
        {
            //TRM Get Action - Fix Cylinder On Flag_HaveBIB
            bool b1 = BoardCyControl(true);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart15_Run()
        {
            //TRM Get Action - Axis Y Move to Safe Position
            ContinueRun = true;
            if (MoveToY(BT_BasicPos.Safe_Y))
            {
                ContinueRun = false;
                LogDebug("TRM", string.Format("Get Action Y move to Safe Position: " + BT_BasicPos.Safe_Y.ToString()));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart16_Run()
        {
            //TRM Get Action - Done
            Flag_GetAction.Done();
            TRM_ActionResult = ThreeValued.TRUE;
            LogDebug("TRM", string.Format("Get Action Done "));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart29_Run()
        {
            //TRM Get Action - NEXT
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion

        #region TRM Put Action
        private FlowChart.FCRESULT FC_TRM_PUT_Run()
        {
            //TRM Put Action - Put Start
            Flag_PutAction.Reset();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart17_Run()
        {
            //TRM Put Action - Need Put?
            if (Flag_PutAction.IsDoIt())
            {
                TRM_ActionResult = ThreeValued.UNKNOWN;
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart18_Run()
        {
            //TRM Put Action - Axis Z Move and Check have BIB?
            if (!Station_Dir.Equals(TRMStation.DIRECT_DOWN)) return FlowChart.FCRESULT.NEXT;

            ContinueRun = true;
            bool b1 = MoveToZ(BT_BasicPos.Check_Z);
            bool b2 = MoveToY(BT_BasicPos.Detect_Y);
            //if (MoveToZ(BT_BasicPos.Check_Z)
            if (b1 && b2)
            {
                ContinueRun = false;
                if (!IB_Scan_BoardDetect.Value || (SystemDryRun && SystemDryRun_NoBoard))
                {
                    LogDebug("TRM", string.Format("Put Action Z move to Safe Position: " + BT_BasicPos.Check_Z.ToString()));
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart19_Run()
        {
            //TRM Put Action - Axis Z Move to Top Position
            ContinueRun = true;
            if (MoveToZ(BT_BasicPos.Top_Z))
            {
                ContinueRun = false;
                LogDebug("TRM", string.Format("Put Action Z move to Top Position: " + BT_BasicPos.Top_Z.ToString()));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart20_Run()
        {
            //TRM Put Action - Axis Y Move to BIB Position
            ContinueRun = true;
            if (MoveToY(BT_BasicPos.BIB_Y - 3000))
            {
                ContinueRun = false;
                LogDebug("TRM", string.Format("Put Action Y move to BIB Position: " + BT_BasicPos.BIB_Y.ToString()));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart21_Run()
        {
            //TRM Put Action - Fix Cylinder Off Flag_HaveBIB
            bool b1 = BoardCyControl(false);
            if (b1)
            {
                SetSpeed(true);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart22_Run()
        {
            //TRM Put Action - Axis Z Move to Down Position
            if (MoveToZ(BT_BasicPos.Down_Z))
            {
                SetSpeed(false);
                LogDebug("TRM", string.Format("Put Action Z move to Down Position: " + BT_BasicPos.Down_Z.ToString()));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart23_Run()
        {
            //TRM Put Action - Axis Y Move to Safe Position
            if (MoveToY(BT_BasicPos.Safe_Y))
            {
                LogDebug("TRM", string.Format("Put Action Y move to Safe Position: " + BT_BasicPos.Safe_Y.ToString()));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart24_Run()
        {
            //TRM Put Action - Done
            Flag_PutAction.Done();
            TRM_ActionResult = ThreeValued.TRUE;
            LogDebug("TRM", string.Format("Put Action Done "));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart30_Run()
        {
            //TRM Put Action - NEXT
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion

        #region TRM Move Action
        private FlowChart.FCRESULT FC_TRM_MOVE_Run()
        {
            //Move Action - Move Start
            Flag_MoveAction.Reset();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart25_Run()
        {
            //Move Action - Need Move?
            if (Flag_MoveAction.IsDoIt())
            {
                TRM_ActionResult = ThreeValued.UNKNOWN;
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart26_Run()
        {
            //Move Action - Move to Postiton
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart27_Run()
        {
            //Move Action - Move Done
            Flag_MoveAction.Done();
            TRM_ActionResult = ThreeValued.TRUE;
            LogDebug("TRM", string.Format("Move Action Done "));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart28_Run()
        {
            //Move Action - NEXT
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT FC_MOVE_TO_POSITION_Run()
        {
            //Move to Position - Start Move to Posititon
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart31_Run()
        {
            //CASE1 不用旋轉
            //NEXT 需要旋轉，但須先一動X軸到安全位置後XU一起做動  CH to other
            //CASE2 需要旋轉，XU可一起動做    loadA StageA to CH and stage to load 
            //Move to Position - Need Change Station?
            if (Station_Dir.HasFlag(Now_Station))//|| Station_Dir == TRMStation.NONE)
            {
                return FlowChart.FCRESULT.CASE1;
            }
            else
            {
                if (Now_Station.Equals(TRMStation.LOAD_A) || Now_Station.Equals(TRMStation.LOAD_B) || Now_Station.Equals(TRMStation.READBOARCODE))
                {
                    Station_Dir = TRMStation.DIRECT_DOWN;
                }
                else if (Now_Station.Equals(TRMStation.STAGE_A) || Now_Station.Equals(TRMStation.STAGE_B))
                {
                    Station_Dir = TRMStation.DIRECT_TOP;
                }
                else
                {
                    Station_Dir = TRMStation.DORECT_RIGHT;
                }

                if (Last_Station.Equals(TRMStation.STAGE_CH))
                {
                    return FlowChart.FCRESULT.NEXT;
                }
                if (Last_Station.Equals(TRMStation.LOAD_A) || Last_Station.Equals(TRMStation.STAGE_A))
                {
                    return FlowChart.FCRESULT.CASE3;
                }
                if (Last_Station.Equals(TRMStation.LOAD_B) || Last_Station.Equals(TRMStation.STAGE_B) || Last_Station.Equals(TRMStation.READBOARCODE))
                {
                    if (Now_Station.Equals(TRMStation.STAGE_CH))
                    {
                        return FlowChart.FCRESULT.CASE2;
                    }
                    else
                    {
                        return FlowChart.FCRESULT.CASE3;
                    }
                }
            }
            //laststation = waitting
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart32_Run()
        {
            //Move to Position - Is Axis X Safe?
            if (!MT_AxisX.IsHomeOn)
            {
                LogDebug("TRM", string.Format("Move Action X Safe = false "));
                return FlowChart.FCRESULT.CASE1;
            }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart33_Run()
        {
            //Move to Position - Move X to Safe Position
            if (MoveToX(BT_BasicPos.Safe_X))
            {
                LogDebug("TRM", string.Format("Move Action X move to Safe Position: " + BT_BasicPos.Safe_X.ToString()));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart34_Run()
        {
            //Move to Position - Move U to Position
            int Pos = DegreeToPluse(BT_BasicPos.Station_U);
            //if (MoveToU(BT_BasicPos.Station_U))
            if (MoveToU(Pos))
            {
                LogDebug("TRM", string.Format("Move Action U move to Position: " + BT_BasicPos.Station_U.ToString()));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart35_Run()
        {
            //Move to Position - Move X to Position
            if (MoveToX(BT_BasicPos.Station_X))
            {
                LogDebug("TRM", string.Format("Move Action X move to work Position: " + BT_BasicPos.Station_X.ToString()));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart36_Run()
        {
            //Move to Position - Move Z to Position
            bool b1 = true;
            bool b2 = true;
            //b1 = MoveToZ(BT_BasicPos.Check_Z);
            if (Now_Station.Equals(TRMStation.READBOARCODE))
            {
                b2 = MoveToY(BT_BasicPos.Read_Y);
            }
            //if (MoveToZ(BT_BasicPos.Check_Z))
            if (b1 && b2)
            {
                LogDebug("TRM", string.Format("Move Action Z move to CheckZ Position: " + BT_BasicPos.Check_Z.ToString()));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart37_Run()
        {
            //Move to Position - Done
            Last_Station = Now_Station;
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart38_Run()
        {
            //Move to Position - NEXT
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            if (test.Enabled == false)
            {
                FC_TRM_GET.TaskReset();
                FC_TRM_PUT.TaskReset();
                FC_TRM_MOVE.TaskReset();

                test.Enabled = true;
            }
            else
            {
                test.Enabled = false;
            }
        }

        private void test_Tick(object sender, EventArgs e)
        {
            FC_TRM_GET.MainRun();
            FC_TRM_PUT.MainRun();
            FC_TRM_MOVE.MainRun();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //SetActionCommand_TRM(TRMStation.LOAD_A, ACTIONMODE.GET, nowPos);
            SetActionCommand_TRM(TRMStation.LOAD_A, ACTIONMODE.GET);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //SetActionCommand_TRM(TRMStation.LOAD_A, ACTIONMODE.PUT, nowPos);
            SetActionCommand_TRM(TRMStation.LOAD_A, ACTIONMODE.PUT);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //SetActionCommand_TRM(TRMStation.STAGE_CH, ACTIONMODE.MOVE, nowPos);
            SetActionCommand_TRM(TRMStation.STAGE_CH, ACTIONMODE.MOVE);
        }

        private FlowChart.FCRESULT flowChart39_Run()
        {
            int iPos = SReadValue("Pos_TRM_RotateBase").ToInt();
            bool b1 = MT_AxisU.G00(iPos);
            if (b1)
            {
                MT_AxisU.SetPos(0);
                MT_AxisU.SetEncoderPos(0);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KCSDK.MotorJogForm.MotorJog.Run(groupBox9);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ServoOn();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ServoOff();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            text_BarCodeReader.Text = "";
            br.ReadBarcodeTaskReset();
            timer1.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (br != null)
            {
                text_BarCodeReader.Text = "Scaning...";
                string sRet = br.ReadBarcode();
                if (sRet != "#BUSY#")
                {
                    text_BarCodeReader.Text = sRet;
                    br.ReadBarcodeTaskReset();
                    timer1.Enabled = false;
                    //Timer_BarCodeReader.Enabled = false;
                }
            }
        }

        private FlowChart.FCRESULT flowChart40_Run()
        {
            Flag_ReadAction.Reset();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart41_Run()
        {
            if (Flag_ReadAction.IsDoIt())
            {
                BarcodeValue = "";
                Flag_ReadAction.Doing();
                TRM_ActionResult = ThreeValued.UNKNOWN;
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private string BarcodeValue = "";
        private FlowChart.FCRESULT flowChart42_Run()
        {
            if (IsSimulation() != 0) return FlowChart.FCRESULT.NEXT;
            if (SystemDryRun && SystemDryRun_NoBoard) return FlowChart.FCRESULT.NEXT;
            if (br != null)
            {
                string sRet = br.ReadBarcode();
                if (sRet != "#BUSY#")
                {
                    br.ReadBarcodeTaskReset();
                    if (sRet != "!TIMEOUT!" && sRet != "!ERROR!")// && CheckBoardType(sRet))
                    {
                        BarcodeValue = sRet;
                    }

                    return FlowChart.FCRESULT.NEXT;
                }
                    //Timer_BarCodeReader.Enabled = false;
             }
             return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart43_Run()
        {
            Flag_ReadAction.Done();
            TRM_ActionResult = ThreeValued.TRUE;
            LogDebug("TRM", string.Format("Move Action Done  " + BarcodeValue));
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart40_Run_1()
        {
            if (MoveToY(BT_BasicPos.Safe_Y))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart44_Run()
        {
            int Pos = DegreeToPluse(BT_BasicPos.Station_U);
            bool b1 = MoveToX(BT_BasicPos.Station_X);
            bool b2 = MoveToU(Pos);
            if (b1 && b2)
            {
                LogDebug("TRM", string.Format("Move Action X move to work Position: " + BT_BasicPos.Station_X.ToString()));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        
        
    }
}

