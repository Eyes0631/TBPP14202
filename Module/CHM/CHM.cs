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

namespace CHM
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
    public partial class CHM : BaseModuleInterface, CHM_interface
    {
        
        private enum CHM_ACTION
        {
            NONE,
            YtoFirstOpen,
            YtoFirstClose,
            ZtoSafety,
            YtoNextOpen,
            YtoNextClose,
            ZtoOpen,
            ZtoClosefirst,
            YtoClose,
            ZtoCloseSecond,
            NextLine,
            Delay,
            Delay_Open,
        }

        private enum CHM_ALARM_CODE
        {
            ER_CHM_UNKNOW = 0,
            ER_CHM_FONTCYLINDER_TIMEOUT = 1,
            ER_CHM_SIDECYLINDER_TIMEOUT = 2,
            ER_CHM_HOLDCYLINDER_TIMEOUT = 3,
            ER_CHM_STAGECYLINDER_TIMEOUT = 4,
            ER_CHM_ZCYLINDER_TIMEOUT = 5,
            ER_CHM_DETECT_BOARDERROR = 6,
            ER_CHM_SCAN_DETECTERROR = 7,
        }

        private enum DIRECTION
        { 
            NONE,
            POSITIVE,
            NEGATIVE
        }


     #region 流程相關變數

        private JActionFlag Flag_Action = new JActionFlag();
        private JActionFlag Flag_ClamSheelAction = new JActionFlag(); //開關蓋動作
        private JActionFlag Flag_OpenAction = new JActionFlag(); //開蓋流程
        private JActionFlag Flag_CloseAction = new JActionFlag();    //關蓋流程
        private JActionFlag Flag_DetectAction = new JActionFlag();   //偵測流程
        private JActionFlag Flag_MoveAction = new JActionFlag(); //Move
        private JActionFlag Flag_LockAction = new JActionFlag(); //Lock
        private JActionFlag Flag_UnlockAction = new JActionFlag();   //Unlock
        private JActionFlag Flag_CheckAction = new JActionFlag();    //整板偵測流程
        private ThreeValued CHM_ActionResult = ThreeValued.UNKNOWN;
        private ACTIONMODE CHM_Mode = ACTIONMODE.NONE;
        private CH_CHECKMODE Detect_Mode = CH_CHECKMODE.NONE;
        private Queue<CHM_ACTION> CHM_ActionQueue = new Queue<CHM_ACTION>();

        private bool bMT_AxisU_CanHome = false;

        private ThreeValued mAxisZHomeOK = ThreeValued.FALSE;
        private BasePosInfo nowPos = new BasePosInfo();

        private int iWorkIdx;
        private DIRECTION WorkDir = new DIRECTION();

        private int iXN;
        private int iYN;
        //private int iWorkLine;
        private bool Z_Detect;
        //Mars add
        private int SPos_Base_H_Y;
        private int SPos_Base_V_Y;
        private int SPos_Safe_Z;
        private int SPos_Rotato_H;
        private int SPos_Rotato_V;
        private int SRotateResolution;
        private int PClamSheetDir;
        private int PClamsSheelBaseYOffset;
        private int PBoard_YShift;
        private int PBoard_XShift;
        private int PBoard_YPitch;
        private int PBoard_XPitch;
        private int POpenAction_ZDown1;
        private int PCloseAction_ZDown1;
        private int PCloseAction_YDis1;
        private int PCloseAction_ZDown2;
        private int POpenAction_YOffset;
        private int PCloseAction_YOffset;
        private int PClamsSheelCloseDetectZPos;
        private int PClamsSheelOpenDetectZPos;
        private int PClamsSheelCloseDetectYPos;
        private int PClamsSheelOpenDetectYPos;
        private int PRotateOffset;

        //Action Delay
        private int POpen_DelayTm;
        private int PClose_DelayTm;

        bool SystemDryRun = false;
        bool SystemDryRun_NoBoard = false;
        bool SystemDryRun_NoDevice = false;




        #endregion

        public CHM()
        {
            InitializeComponent();
            CreateComponentList();
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
            Font_Cylinder = new Cylinder(OB_Font_Cylinder, IB_Font_Cylinder_On, IB_Font_Cylinder_Off);
            Side_Cylinder = new Cylinder(OB_Side_Cylinder, IB_Side_Cylinder_On, IB_Side_Cylinder_Off);
            Hold_Cylinder = new Cylinder(OB_Hold_Cylinder, IB_Hold_Cylinder_On, IB_Hold_Cylinder_Off);
            Stage_Cylinder = new Cylinder(OB_Stage_Cylinder, IB_Stage_Cylinder_On, IB_Stage_Cylinder_Off);
            Z_Cylinder = new Cylinder(OB_Z_Cylinder_B, OB_Z_Cylinder_A, IB_Z_Cylinder_On, IB_Z_Cylinder_Off);

            DI_Detect_Board = new DigitalInput(IB_BoardDetect);
            DI_Detect_Scan = new DigitalInput(IB_ScanDetect);

            MT_AxisY = MT_CHM_AxisY;
            MT_AxisZ = MT_CHM_AxisZ;
            MT_AxisU = MT_CHM_AxisU;
        }

        //持續偵測函數
        public override void AlwaysRun()
        {
            if (SysState == StateMode.SM_AUTO_RUN)
            {
                //int AxisZSaftyPosZ = SReadValue("Pos_Safe_Z").ToInt();
                //if (MT_AxisY.IsBusy && MT_AxisZ.ReadEncPos() < AxisZSaftyPosZ)
                if (MT_AxisY.Busy() && MT_AxisZ.ReadEncPos() < SPos_Safe_Z)
                {
                    //Set Alarm
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
            FC_CHM_HOME.TaskReset();

            WorkDir = DIRECTION.NEGATIVE;
            iWorkIdx = 0;

            Flag_Action.Reset();
            Flag_OpenAction.Reset();
            Flag_CloseAction.Reset();
            Flag_MoveAction.Reset();
            Flag_LockAction.Reset();
            Flag_UnlockAction.Reset();
            Flag_CheckAction.Reset();
            Flag_ClamSheelAction.Reset();
            Flag_DetectAction.Reset();

            bMT_AxisU_CanHome = false;
        } //歸零前的重置
        public override bool Home()
        {
            FC_CHM_HOME.MainRun();
            return mHomeOk;
        } //歸零

        //運轉相關函數
        public override void ServoOn()
        {
            if (MT_CHM_AxisY != null) { MT_CHM_AxisY.ServoOn(true); }
            if (MT_CHM_AxisZ != null) { MT_CHM_AxisZ.ServoOn(true); }
            if (MT_CHM_AxisU != null) { MT_CHM_AxisU.ServoOn(true); }
        } //Motor Servo On
        public override void ServoOff()
        {
            if (MT_CHM_AxisY != null) { MT_CHM_AxisY.ServoOn(false); }
            if (MT_CHM_AxisZ != null) { MT_CHM_AxisZ.ServoOn(false); }
            if (MT_CHM_AxisU != null) { MT_CHM_AxisU.ServoOn(false); }
        } //Motor Servo Off
        public override void RunReset()
        {
            FC_CHM_WORK.TaskReset();
            FC_CHM_CLAMSHELL.TaskReset();
            FC_CHM_CHECK.TaskReset();
            FC_CHM_DETECT.TaskReset();
            FC_CHM_MOVE.TaskReset();
            FC_CHM_LOCK.TaskReset();
            FC_CHM_UNLOCK.TaskReset();
        } //運轉前初始化
        public override void Run()
        {
            FC_CHM_WORK.MainRun();
            FC_CHM_CLAMSHELL.MainRun();
            FC_CHM_CHECK.MainRun();
            FC_CHM_DETECT.MainRun();
            FC_CHM_MOVE.MainRun();
            FC_CHM_LOCK.MainRun();
            FC_CHM_UNLOCK.MainRun();
            if (mLotend)
            {
                mLotendOk = true;
            }
        } //運轉
        public override void SetSpeed(bool bHome = false)
        {
            int SPD = 10000;
            int ACC_MULTIPLE = 100000;
            int ACC_DEC = 100000;
            int SPEED_RATE = OReadValue("機台速率").ToInt();

            if (MT_CHM_AxisY != null)
            {
                if (!bHome)
                {
                    SPD = (SReadValue("MT_CHM_Y_SPEED").ToInt() * SPEED_RATE) / 100;
                    ACC_MULTIPLE = SReadValue("MT_CHM_Y_ACC_MULTIPLE").ToInt();
                    ACC_DEC = (SPD * ACC_MULTIPLE);
                }
                MT_CHM_AxisY.SetSpeed(SPD);
                MT_CHM_AxisY.SetAcceleration(ACC_DEC);
                MT_CHM_AxisY.SetDeceleration(ACC_DEC);
            }

            if (MT_CHM_AxisZ != null)
            {
                if (!bHome)
                {
                    SPD = (SReadValue("MT_CHM_Z_SPEED").ToInt() * SPEED_RATE) / 100;
                    ACC_MULTIPLE = SReadValue("MT_CHM_Z_ACC_MULTIPLE").ToInt();
                    ACC_DEC = (SPD * ACC_MULTIPLE);
                }
                MT_CHM_AxisZ.SetSpeed(SPD);
                MT_CHM_AxisZ.SetAcceleration(ACC_DEC);
                MT_CHM_AxisZ.SetDeceleration(ACC_DEC);
            }

            if (MT_CHM_AxisU != null)
            {
                if (!bHome)
                {
                    SPD = (SReadValue("MT_CHM_U_SPEED").ToInt() * SPEED_RATE) / 100;
                    ACC_MULTIPLE = SReadValue("MT_CHM_U_ACC_MULTIPLE").ToInt();
                    ACC_DEC = (SPD * ACC_MULTIPLE);
                }
                MT_CHM_AxisU.SetSpeed(SPD);
                MT_CHM_AxisU.SetAcceleration(ACC_DEC);
                MT_CHM_AxisU.SetDeceleration(ACC_DEC);
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
            double Resolution = SRotateResolution;//SSet->FindField("Roar_Resolution")->AsInteger;
            double Pluse = ((double)Degree / 360) * Resolution;
            return (int)Pluse;
        }

        private bool GetNextIndex()
        {
            bool bRet = false;
            if (WorkDir.Equals(DIRECTION.NEGATIVE))
            {
                iWorkIdx++;
            }
            else if (WorkDir.Equals(DIRECTION.POSITIVE))
            {
                iWorkIdx--;
            }

            int MaxRow = (PClamSheetDir.Equals(2) || PClamSheetDir.Equals(3)) ? iXN : iYN;
            if (iWorkIdx < 0)
            {
                iWorkIdx = 0;
                WorkDir = DIRECTION.NEGATIVE;
                bRet = true;
            }
            else if (iWorkIdx > MaxRow -1)
            {
                iWorkIdx = MaxRow - 1;
                WorkDir = DIRECTION.POSITIVE;
                bRet = true;
            }
            if (bRet) LogDebug("CHM", "WorkDir (turn) " + WorkDir.ToString());
            LogDebug("CHM", "iWorkIdx = " + iWorkIdx.ToString());
            return bRet;
        }

        private void LogDebug(string module, string msg)
        {
            bool EnableDebugLog = true;
            if (EnableDebugLog && GetUseModule())
            {
                JLogger.LogDebug("Debug", module + " | " + msg);
            }
        }

        private void ShowAlarmMessage(string AlarmLevel, int ErrorCode, params object[] args)   //Alarm顯示
        {
            ShowAlarm(AlarmLevel, ErrorCode, args);
        }

        private bool AllCyControl(bool state)
        {
            if (state)
            {
                Font_Cylinder.On();
                Side_Cylinder.On();
                Hold_Cylinder.On();
                Stage_Cylinder.On();
                Z_Cylinder.On();

                bool b1 = Font_Cylinder.OnSensorValue;
                bool b2 = Side_Cylinder.OnSensorValue;
                bool b3 = Hold_Cylinder.OnSensorValue;
                bool b4 = Stage_Cylinder.OnSensorValue;
                bool b5 = Z_Cylinder.OnSensorValue;

                if ((b1 && b2 && b3 && b4 && b5) || IsSimulation() != 0)
                {
                    return true;
                }

            }
            else
            {
                Font_Cylinder.Off();
                Side_Cylinder.Off();
                Hold_Cylinder.Off();
                Stage_Cylinder.Off();
                Z_Cylinder.Off();

                bool b1 = Font_Cylinder.OffSensorValue;
                bool b2 = Side_Cylinder.OffSensorValue;
                bool b3 = Hold_Cylinder.OffSensorValue;
                bool b4 = Stage_Cylinder.OffSensorValue;
                bool b5 = Z_Cylinder.OffSensorValue;

                if ((b1 && b2 && b3 && b4 && b5) || IsSimulation() != 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool SideCyControl(bool state, int delay = 50, int timeOut = 5000)
        {
            ThreeValued r1;

            if (!GetUseModule() || (IsSimulation() != 0))
            {
                LogDebug("CHM", "SideCyControl " + state.ToString());
                return true;
            }

            if (state)
            {
                r1 = Side_Cylinder.On(delay, timeOut);
            }
            else
            {
                r1 = Side_Cylinder.Off(delay, timeOut);
            }

            if (r1.Equals(ThreeValued.TRUE))
            {
                return true;
            }
            else if (r1.Equals(ThreeValued.FALSE))
            {
                //alarm
                ShowAlarmMessage("E", (int)CHM_ALARM_CODE.ER_CHM_SIDECYLINDER_TIMEOUT);
                return false;
            }

            return false;
        }
        private bool FontCyControl(bool state, int delay = 50, int timeOut = 5000)
        {
            ThreeValued r1;

            if (!GetUseModule() || (IsSimulation() != 0))
            {
                LogDebug("CHM", "FontCyControl " + state.ToString());
                return true;
            }

            if (state)
            {
                r1 = Font_Cylinder.On(delay, timeOut);
            }
            else
            {
                r1 = Font_Cylinder.Off(delay, timeOut);
            }

            if (r1.Equals(ThreeValued.TRUE))
            {
                return true;
            }
            else if (r1.Equals(ThreeValued.FALSE))
            {
                //alarm
                ShowAlarmMessage("E", (int)CHM_ALARM_CODE.ER_CHM_FONTCYLINDER_TIMEOUT);
                return false;
            }

            return false;
        }
        private bool HoldCyControl(bool state, int delay = 50, int timeOut = 5000)
        {
            ThreeValued r1;

            if (!GetUseModule() || (IsSimulation() != 0))
            {
                LogDebug("CHM", "HoldCyControl " + state.ToString());
                return true;
            }

            if (state)
            {
                r1 = Hold_Cylinder.On(delay, timeOut);
            }
            else
            {
                r1 = Hold_Cylinder.Off(delay, timeOut);
            }

            if (r1.Equals(ThreeValued.TRUE))
            {
                return true;
            }
            else if (r1.Equals(ThreeValued.FALSE))
            {
                //alarm
                ShowAlarmMessage("E", (int)CHM_ALARM_CODE.ER_CHM_HOLDCYLINDER_TIMEOUT);
                return false;
            }

            return false;
        }
        private bool StageCyControl(bool state, int delay = 50, int timeOut = 5000)
        {
            ThreeValued r1;

            if (!GetUseModule() || (IsSimulation() != 0))
            {
                LogDebug("CHM", "StageCyControl " + state.ToString());
                return true;
            }

            if (state)
            {
                r1 = Stage_Cylinder.On(delay, timeOut);
            }
            else
            {
                r1 = Stage_Cylinder.Off(delay, timeOut);
            }

            if (r1.Equals(ThreeValued.TRUE))
            {
                return true;
            }
            else if (r1.Equals(ThreeValued.FALSE))
            {
                //alarm
                ShowAlarmMessage("E", (int)CHM_ALARM_CODE.ER_CHM_STAGECYLINDER_TIMEOUT);
                return false;
            }

            return false;
        }
        private bool ZCyControl(bool state, int delay = 50, int timeOut = 5000)
        {
            ThreeValued r1;

            if (!GetUseModule() || (IsSimulation() != 0))
            {
                LogDebug("CHM", "ZCyControl " + state.ToString());
                return true;
            }

            if (state)
            {
                r1 = Z_Cylinder.On(delay, timeOut);
            }
            else
            {
                r1 = Z_Cylinder.Off(delay, timeOut);
            }

            if (r1.Equals(ThreeValued.TRUE))
            {
                return true;
            }
            else if (r1.Equals(ThreeValued.FALSE))
            {
                //alarm
                ShowAlarmMessage("E", (int)CHM_ALARM_CODE.ER_CHM_ZCYLINDER_TIMEOUT);
                return false;
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

        #region Open & Close function
        private int GetBasePos()
        {
            int iBasePos;
            int iShift;
            int iOffset = PClamsSheelBaseYOffset;
            //if (dir.Equals(2) || dir.Equals(3))
            if (PClamSheetDir.Equals(2) || PClamSheetDir.Equals(3))
            {
                iBasePos = SPos_Base_H_Y;
                iShift = PBoard_XShift;
                //iBasePos = SReadValue("Pos_Base_H_Y").ToInt();
                //iOffset = PReadValue("ClamsSheelBaseYOffset").ToInt();
                //iShift = PReadValue("BoardTable", "Board_YShift").ToInt();

            }
            else
            {
                iBasePos = SPos_Base_V_Y;
                iShift = PBoard_YShift;
                //iBasePos = SReadValue("Pos_Base_V_Y").ToInt();
                //iOffset = PReadValue("ClamsSheelBaseYOffset").ToInt();
                //iShift = PReadValue("BoardTable", "Board_XShift").ToInt();
            }

            return iBasePos - iShift + iOffset;
        }

        //private bool MoveYToFirstPos()
        //{
        //    if (CDir.Equals(2) || CDir.Equals(3))
        //    {
        //        iBasePos = SReadValue("Pos_Base_H_Y").ToInt();
        //        iOffset = PReadValue("ClamsSheelBaseYOffset").ToInt();
        //        iShift = PReadValue("BoardTable", "Board_YShift").ToInt();

        //    }
        //    else
        //    {
        //        iBasePos = SReadValue("Pos_Base_V_Y").ToInt();
        //        iOffset = PReadValue("ClamsSheelBaseYOffset").ToInt();
        //        iShift = PReadValue("BoardTable", "Board_XShift").ToInt();
        //    }

        //    bool b1 = MoveToY(iBasePos - iShift + iOffset);
        //    if (b1)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        private int GetTypePitch()
        {
            int iRet = PClamSheetDir.Equals(2) || PClamSheetDir.Equals(3) ? PBoard_XPitch : PBoard_YPitch;
            //if (dir.Equals(2) || dir.Equals(3))
            //{
            //    iRet = PReadValue("BoardTable", "Board_YPitch").ToInt();
            //}
            //else
            //{
            //    iRet = PReadValue("BoardTable", "Board_XPitch").ToInt();
            //}

            return iRet;
        }

        private bool MoveYToWork(int offset = 0)
        {
            //int CDir = PReadValue("BoardTable", "ClamsheelDir").ToInt();
            int iBasePos = GetBasePos();
            int iPitch = GetTypePitch();
            bool b1 = MoveToY(iBasePos - (iPitch * iWorkIdx) + offset);
            if (b1)
            {
                LogDebug("CHM", "MoveYToWork :" + (iBasePos - (iPitch * iWorkIdx) + offset).ToString());
                return true;
            }
            return false;
        }

        //private bool MoveYToNextPitch(int line)
        //{
        //    int iYPos = MT_AxisY.ReadEncPos();
        //    int CDir = PReadValue("BoardTable", "ClamsheelDir").ToInt();
        //    if (CDir.Equals(2) || CDir.Equals(3))
        //    {
        //        iPitch = PReadValue("BoardTable", "PitchY").ToInt();
        //    }
        //    else
        //    {
        //        iPitch = PReadValue("BoardTable", "PitchX").ToInt();
        //    }

        //    //bool b1 = MoveToY(iYPos + iPitch);
        //    bool b1 = MoveToY(iBasePos = SReadValue("Pos_Base_V_Y").ToInt();
        //        iOffset = PReadValue( "OffSetX").ToInt();
        //        iPitch = PReadValue("BoardTable", "PitchX").ToInt(); DataGridLineStyle * 
        //    if (b1)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //private bool MoveYToNextClose()
        //{
        //    int iYPos = MT_AxisY.ReadEncPos();
        //    int iOffset;
        //    int iPitch;

        //    int CDir = PReadValue("BoardTable", "ClamsheelDir").ToInt();
        //    if (CDir.Equals(2) || CDir.Equals(3))
        //    {
        //        iPitch = PReadValue("BoardTable", "PitchY").ToInt();
        //        iOffset = PReadValue("offsetY").ToInt();
        //    }
        //    else
        //    {
        //        iPitch = PReadValue("BoardTable", "PitchX").ToInt();
        //        iOffset = PReadValue("offsetX").ToInt();
        //    }

        //    bool b1 = MoveToY(iYPos + iPitch - iOffset);
        //    if (b1)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        private bool MoveZToOpenPos()
        {
            //bool b1 = MoveToZ(PReadValue("OpenAction_ZDown1").ToInt());
            bool b1 = MoveToZ(POpenAction_ZDown1);
            if (b1)
            {
                LogDebug("CHM", "MoveZToOpenPos :" + POpenAction_ZDown1.ToString());
                return true;
            }
            return false;
        }

        private bool MoveZToSafe()
        {
            //bool b1 = MoveToZ(SReadValue("Pos_Safe_Z").ToInt());
            bool b1 = MoveToZ(SPos_Safe_Z);
            bool b2 = ZCyControl(false);
            if (b1 && b2)
            {
                LogDebug("CHM", "MoveZToSafe :" + SPos_Safe_Z.ToString());
                return true;
            }
            return false;
        }

        private bool MoveZToFirstClose()
        {
            //bool b1 = MoveToZ(PReadValue("CloseAction_ZDown1").ToInt());
            bool b1 = MoveToZ(PCloseAction_ZDown1);
            if (b1)
            {
                LogDebug("CHM", "MoveZToFirstClose :" + PCloseAction_ZDown1.ToString());
                return true;
            }
            return false;
        }

        //private bool MoveYToClose()
        //{
        //    int iYPos = MT_AxisY.ReadEncPos();
        //    int iOffset = PReadValue("offset").ToInt();
        //    bool b1 = MoveToY(iYPos + iOffset);
        //    if (b1)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        private bool MoveZToSecondClose()
        {
            //bool b1 = MoveToZ(PReadValue("CloseAction_ZDown2").ToInt());
            bool b1 = MoveToZ(PCloseAction_ZDown2);
            if (b1)
            {
                LogDebug("CHM", "MoveZToSecondClose :" + PCloseAction_ZDown2.ToString());
                return true;
            }
            return false;
        }

        private void ActioneOpen()
        {
            CHM_ActionQueue.Enqueue(CHM_ACTION.ZtoSafety);
            CHM_ActionQueue.Enqueue(CHM_ACTION.YtoFirstOpen);
            CHM_ActionQueue.Enqueue(CHM_ACTION.ZtoOpen);
            CHM_ActionQueue.Enqueue(CHM_ACTION.Delay_Open);
            CHM_ActionQueue.Enqueue(CHM_ACTION.ZtoSafety);
            CHM_ActionQueue.Enqueue(CHM_ACTION.NextLine);
        }

        private void ActionCLose()
        {

            CHM_ActionQueue.Enqueue(CHM_ACTION.ZtoSafety);
            CHM_ActionQueue.Enqueue(CHM_ACTION.YtoFirstClose);
            CHM_ActionQueue.Enqueue(CHM_ACTION.ZtoClosefirst);
            CHM_ActionQueue.Enqueue(CHM_ACTION.YtoClose);
            CHM_ActionQueue.Enqueue(CHM_ACTION.ZtoCloseSecond);
            CHM_ActionQueue.Enqueue(CHM_ACTION.Delay);
            CHM_ActionQueue.Enqueue(CHM_ACTION.ZtoSafety);
            CHM_ActionQueue.Enqueue(CHM_ACTION.NextLine);
        }
        #endregion

        private bool WorkBoardDetect(bool bSensor)
        {
            if (IsSimulation() != 0 || (SystemDryRun && SystemDryRun_NoBoard) || !GetUseModule())
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

        private void TM_Detect_Tick(object sender, EventArgs e)
        {
            TM_Detect.Enabled = false;
            Z_Detect = true;
            if (Z_Detect)
                Z_Detect = IB_ScanDetect.Value || (SystemDryRun && SystemDryRun_NoBoard);
            //if (!IB_ScanDetect.Value)
            //{
            //    Z_Detect = IB_ScanDetect.Value;
            //}
            TM_Detect.Enabled = true;
        }

        #endregion

        #region 公用函數
        public bool CheckHaveBoard(bool value = false)
        {
            return WorkBoardDetect(value);
        }

        /// <summary>
        /// 藉由MainFLow 傳回Transfer是否安全，如果不在安全位置不可旋轉
        /// </summary>
        /// <param name="Val"></param>
        public void Protection_CHM(bool Val)
        {
            if (Val.Equals(false))
            {
                if (MT_CHM_AxisU.Busy())
                {
                    MT_CHM_AxisU.FastStop();
                }
                if (MT_AxisZ.Busy())
                {
                    MT_AxisZ.FastStop();
                }
                if (MT_AxisY.Busy())
                {
                    MT_AxisY.FastStop();
                }
            }
        }

        public void SetCanRunHome()
        {
            mCanHome = true;
        }

        public void DataReset()
        {
            //iYN = PReadValue("YN").ToInt();
            //iXN = PReadValue("XN").ToInt();
            iYN = PReadValue("BoardTable", "Board_YN").ToInt();
            iXN = PReadValue("BoardTable", "Board_XN").ToInt();
            Boardstate = new string[iXN * iYN];


            SPos_Base_H_Y = SReadValue("Pos_Base_H_Y").ToInt();
            SPos_Base_V_Y = SReadValue("Pos_Base_V_Y").ToInt();
            SPos_Safe_Z = SReadValue("Pos_Safe_Z").ToInt();
            SPos_Rotato_H = SReadValue("Pos_Rotato_H").ToInt();
            SPos_Rotato_V = SReadValue("Pos_Rotato_V").ToInt();
            SRotateResolution = SReadValue("Pos_CHM_RotateResolution").ToInt();
            PClamSheetDir = PReadValue("BoardTable", "ClamsheelDir").ToInt();
            PClamsSheelBaseYOffset = PReadValue("ClamsSheelBaseYOffset").ToInt();
            PBoard_YShift = PReadValue("BoardTable", "Board_YShift").ToInt();
            PBoard_XShift = PReadValue("BoardTable", "Board_XShift").ToInt();
            PBoard_YPitch = PReadValue("BoardTable", "Board_YPitch").ToInt();
            PBoard_XPitch = PReadValue("BoardTable", "Board_XPitch").ToInt();
            POpenAction_ZDown1 = PReadValue("OpenAction_ZDown1").ToInt();
            PCloseAction_ZDown1 = PReadValue("CloseAction_ZDown1").ToInt();
            PCloseAction_YDis1 = PReadValue("CloseAction_YDis1").ToInt();
            PCloseAction_ZDown2 = PReadValue("CloseAction_ZDown2").ToInt();
            POpenAction_YOffset = PReadValue("OpenAction_YOffset").ToInt();
            PCloseAction_YOffset = PReadValue("CloseAction_YOffset").ToInt();
            PClamsSheelCloseDetectZPos = PReadValue("ClamsSheelCloseDetectZPos").ToInt();
            PClamsSheelOpenDetectZPos = PReadValue("ClamsSheelOpenDetectZPos").ToInt();
            PClamsSheelCloseDetectYPos = PReadValue("ClamsSheelCloseDetectYPos").ToInt();
            PClamsSheelOpenDetectYPos = PReadValue("ClamsSheelOpenDetectYPos").ToInt();
            PRotateOffset = PReadValue("RotateOffset").ToInt();

            POpen_DelayTm = PReadValue("DT_CHM_OPEN").ToInt();
            PClose_DelayTm = PReadValue("DT_CHM_CLOSE").ToInt();

            SystemDryRun = OReadValue("DryRun").ToBoolean();
            SystemDryRun_NoBoard = OReadValue("DryRun_NoBoard").ToBoolean();
            SystemDryRun_NoDevice = OReadValue("DryRun_NoDevice").ToBoolean();

            SetSpeed();
        }

        public ThreeValued IsAxisZHomeOK()
        {
            if (!GetUseModule())
            {
                return ThreeValued.TRUE;
            }
            return mAxisZHomeOK;
        }

        public ThreeValued SetRobotSafety()
        {
            bMT_AxisU_CanHome = true;
            return ThreeValued.TRUE;
        }

        public ThreeValued SetActionCommand_CHM(ACTIONMODE mode)//, CH_CHECKMODE CKMode, BasePosInfo pos)
        {
            //Mars modifyed 
            if (this.GetUseModule().Equals(false))
            {
                return ThreeValued.TRUE;
            }

            if (Flag_Action.IsDoing() || Flag_OpenAction.IsDoing() || Flag_CloseAction.IsDoing() ||
                Flag_LockAction.IsDoing() || Flag_UnlockAction.IsDoing() || Flag_MoveAction.IsDoing())
            {
                return ThreeValued.FALSE;
            }

            CHM_Mode = mode;
            if (mode == ACTIONMODE.OPEN)
            {
                Flag_Action.DoIt();
                Flag_OpenAction.DoIt();
                CHM_ActionResult = ThreeValued.UNKNOWN;
                return ThreeValued.TRUE;
            }

            if (mode == ACTIONMODE.CLOSE)
            {
                Flag_Action.DoIt();
                Flag_CloseAction.DoIt();
                CHM_ActionResult = ThreeValued.UNKNOWN;
                return ThreeValued.TRUE;
            }

            if (mode == ACTIONMODE.LOCK)
            {
                Flag_LockAction.DoIt();
                CHM_ActionResult = ThreeValued.UNKNOWN;
                return ThreeValued.TRUE;
            }

            if (mode == ACTIONMODE.UNLOCK)
            {
                Flag_UnlockAction.DoIt();
                CHM_ActionResult = ThreeValued.UNKNOWN;
                return ThreeValued.TRUE;
            }

            if (mode == ACTIONMODE.MOVE)
            {
                Flag_MoveAction.DoIt();
                //nowPos = pos;
                CHM_ActionResult = ThreeValued.UNKNOWN;
                return ThreeValued.TRUE;
            }
            return ThreeValued.FALSE;
        }

        public ThreeValued GetActionResult_CHM()
        {
            if (GetUseModule().Equals(false))
            {
                return ThreeValued.TRUE;
            }

            return CHM_ActionResult;
        }

        public string[] GetBoardState()
        {
            return Boardstate;
        }

        public void SetBoardState(string[] sBoard)
        {
            Boardstate = sBoard;
        }
        #endregion

        #region 公用變數
        public string[] Boardstate;
        #endregion

        #region 元件
        private Motor MT_AxisY = new Motor();  //移載台車Y軸馬達
        private Motor MT_AxisZ = new Motor();  //移載台車Z軸馬達
        private Motor MT_AxisU = new Motor();  //旋轉台U軸旋轉台

        private Cylinder Font_Cylinder = null;
        private Cylinder Side_Cylinder = null;
        private Cylinder Hold_Cylinder = null;
        private Cylinder Stage_Cylinder = null;
        private Cylinder Z_Cylinder = null;

        private DigitalInput DI_Detect_Board = null;
        private DigitalInput DI_Detect_Scan = null;

        #endregion

        #region 歸零流程
        private FlowChart.FCRESULT FC_CHM_HOME_Run()
        {
            //CHM 歸零流程 - Start Home
            if (MT_AxisY != null) { MT_AxisY.HomeReset(); }
            if (MT_AxisZ != null) { MT_AxisZ.HomeReset(); }
            if (MT_AxisU != null) { MT_AxisU.HomeReset(); }
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart1_Run()
        {
            //CHM 歸零流程 - Can Home?(TRM X Safe)
            if (mCanHome || IsSimulation() != 0)
            {
                MT_AxisY.HomeReset();
                MT_AxisZ.HomeReset();
                MT_AxisU.HomeReset();
                mAxisZHomeOK = ThreeValued.FALSE;
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart2_Run()
        {
            //CHM 歸零流程 - Axis Z Home
            if (MT_AxisZ == null)
            {
                mAxisZHomeOK = ThreeValued.TRUE;
                return FlowChart.FCRESULT.NEXT;
            }
            if (MT_AxisZ.Home())
            {
                mAxisZHomeOK = ThreeValued.TRUE;
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart3_Run()
        {
            //CHM 歸零流程 - Axis Y Home
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
            //CHM 歸零流程 - Axis U Home
            if (!bMT_AxisU_CanHome) return FlowChart.FCRESULT.IDLE;

            if (MT_AxisU == null)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            if (MT_AxisU.Home())
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart5_Run()
        {
            //CHM 歸零流程 - Axis U to Ready

            int iPos = SReadValue("Pos_CHM_RotateBase").ToInt();
            bool b1 = MT_AxisU.G00(iPos);
            if (b1)
            {
                MT_AxisU.SetPos(0);
                MT_AxisU.SetEncoderPos(0);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
            //return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart6_Run()
        {
            //CHM 歸零流程 - All Cylinder Off

            if (AllCyControl(false))
            {
                return FlowChart.FCRESULT.NEXT;
            }

            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart7_Run()
        {
            //CHM 歸零流程 - Check Have BIB Remain
            bool b1 = WorkBoardDetect(false);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            else
            {
                ShowAlarmMessage("E", (int)CHM_ALARM_CODE.ER_CHM_DETECT_BOARDERROR);
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart8_Run()
        {
            //CHM 歸零流程 - Done
            mHomeOk = true;
            return FlowChart.FCRESULT.IDLE;
        }
        #endregion

        #region ClamShell
        private FlowChart.FCRESULT FC_CHM_CLAMSHELL_Run()
        {
            //CHM ClamShell Action - Start Clamshell
            Flag_ClamSheelAction.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart9_Run()
        {
            //CHM Action - Need Work?
            if (Flag_ClamSheelAction.IsDoIt())
            {
                Flag_ClamSheelAction.Doing();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart10_Run()
        {
            //CHM ClamShell Action - Task Assignment
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart11_Run()
        {
            //CHM ClamShell Action - Task Process
            bool b1 = false;
            switch (CHM_ActionQueue.Peek())
            {
                case CHM_ACTION.ZtoSafety:
                    {
                        b1 = MoveZToSafe();
                    }
                    break;
                case CHM_ACTION.ZtoOpen:
                    {
                        b1 = MoveZToOpenPos();
                    }
                    break;
                case CHM_ACTION.ZtoClosefirst:
                    {
                        b1 = MoveZToFirstClose();
                    }
                    break;
                case CHM_ACTION.ZtoCloseSecond:
                    {
                        b1 = MoveZToSecondClose();
                    }
                    break;

                case CHM_ACTION.YtoFirstOpen:
                    {
                        int Dis = POpenAction_YOffset;
                        b1 = MoveYToWork(Dis);
                        //GetNextIndex();
                        //b1 = MoveYToFirstPos();
                    }
                    break;
                case CHM_ACTION.YtoFirstClose:
                    {
                        int Dis = PCloseAction_YOffset;//PReadValue("CloseAction_YOffset").ToInt();
                        b1 = MoveYToWork(Dis);
                        //GetNextIndex();
                    }
                    break;
                case CHM_ACTION.YtoNextOpen:
                    {
                        //GetNextIndex();
                        int Dis = POpenAction_YOffset;//PReadValue("OpenAction_YOffset").ToInt();
                        b1 = MoveYToWork(Dis);
                    }
                    break;
                case CHM_ACTION.YtoNextClose:
                    {
                        //GetNextIndex();
                        int Dis = PCloseAction_YOffset;//PReadValue("CloseAction_YOffset").ToInt();
                        b1 = MoveYToWork(Dis);
                    }
                    break;
                case CHM_ACTION.YtoClose:
                    {
                        int Dis = PCloseAction_YOffset;//PReadValue("CloseAction_YOffset").ToInt();
                        Dis += PCloseAction_YDis1;//PReadValue("CloseAction_YDis1").ToInt();
                        b1 = MoveYToWork(Dis);
                        //b1 = MoveYToClose();
                    }
                    break;
                case CHM_ACTION.NextLine:
                    {
                        GetNextIndex();
                        b1 = true;
                    }
                    break;
                case CHM_ACTION.Delay:
                    {
                        b1 = RunTM.On(PClose_DelayTm);
                    }
                    break;
                case CHM_ACTION.Delay_Open:
                    {
                        b1 = RunTM.On(POpen_DelayTm);
                    }
                    break;
            }
            if (b1)
            {
                CHM_ActionQueue.Dequeue();
                RunTM.Restart();
                if (CHM_ActionQueue.Count <= 0)
                    return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart12_Run()
        {
            //CHM ClamShell Action - Task finish
            Flag_ClamSheelAction.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart39_Run()
        {
            //CHM ClamShell Action - NEXT
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart40_Run()
        {
            //CHM ClamShell Action - NEXT
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion

        #region Open Action
        private FlowChart.FCRESULT FC_CHM_OPEN_Run()
        {
            //CHM Open Action - Start Open
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart13_Run()
        {
            //CHM Open Action - Need Open?
            if (Flag_OpenAction.IsDoIt())
            {
                LogDebug("CHM", "Start Open");
                Detect_Mode = CH_CHECKMODE.NONE;
                Flag_OpenAction.Doing();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart14_Run()
        {
            //if(true)
            //    return FlowChart.FCRESULT.NEXT;
            //CHM Open Action - Set Open Action
            CHM_ActionQueue.Clear();
            

            for (int iWork = 0; iWork < iYN; iWork++)
            {
                ActioneOpen();
                //if (iWork == 0)
                //{
                //    CHM_ActionQueue.Enqueue(CHM_ACTION.YtoFirstOpen);
                //    ActioneOpen();
                //}
                //else
                //{
                //    CHM_ActionQueue.Enqueue(CHM_ACTION.YtoNextOpen);
                //    ActioneOpen();
                //}
            }
            Flag_ClamSheelAction.DoIt();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart15_Run()
        {
            //if (true)
            //    return FlowChart.FCRESULT.NEXT;
            //CHM Open Action - Working
            bool b1 = Flag_ClamSheelAction.IsDone();
            if (b1)
            {
                LogDebug("CHM", "Start Open Done");
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart16_Run()
        {
            //CHM Open Action - Done
            LogDebug("CHM", "Start Detect_After Open");
            Flag_OpenAction.Done();
            Detect_Mode = CH_CHECKMODE.AFTEROPEN;
            Flag_CheckAction.DoIt();
            if (SReadValue("DoNotUseOpenDetect").ToBoolean())
            {
                Flag_CheckAction.Done();
            }
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion

        #region Close Action
        private FlowChart.FCRESULT FC_CHM_CLOSE_Run()
        {
            //CHM Close Action - Start Close
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart17_Run()
        {
            //CHM Close Action - Need Close?
            if (Flag_CloseAction.IsDoIt())
            {
                LogDebug("CHM", "Start Close");
                Detect_Mode = CH_CHECKMODE.NONE;
                Flag_CloseAction.Doing();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart18_Run()
        {
            //if (true)
            //    return FlowChart.FCRESULT.NEXT;
            //CHM Close Action - Set Close Action
            CHM_ActionQueue.Clear();

            for (int iWork = 0; iWork < iYN; iWork++)
            {
                ActionCLose();
                //if (iWork == 0)
                //{
                //    CHM_ActionQueue.Enqueue(CHM_ACTION.YtoFirstClose);
                //    ActionCLose();
                //}
                //else
                //{
                //    CHM_ActionQueue.Enqueue(CHM_ACTION.YtoNextClose);
                //    ActionCLose();
                //}
            }
            Flag_ClamSheelAction.DoIt();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart19_Run()
        {
            //if (true)
            //    return FlowChart.FCRESULT.NEXT;
            //CHM Close Action - Working
            bool b1 = Flag_ClamSheelAction.IsDone();
            if (b1)
            {
                LogDebug("CHM", "Start Close Done");
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart20_Run()
        {
            //CHM Close Action - Done
            LogDebug("CHM", "Start Detect_After Close");
            Flag_CloseAction.Done();
            Detect_Mode = CH_CHECKMODE.AFTERCLOSE;
            Flag_CheckAction.DoIt();
            if (SReadValue("DoNotUseCloseDetect").ToBoolean())
            {
                Flag_CheckAction.Done();
            }
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion

        #region Detect Action
        private FlowChart.FCRESULT FC_CHM_DETECT_Run()
        {
            //CHM Detect Action - Start Detect
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart21_Run()
        {
            //CHM Detect Action - Need Detect?
            if (Flag_DetectAction.IsDoIt())
            {
                Flag_DetectAction.Doing();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart22_Run()
        {
            //CHM Detect Action - Detect Cy On
            if (ZCyControl(true))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart23_Run()
        {
            //CHM Detect Action - Detect Action
            if (Detect_Mode == CH_CHECKMODE.AFTEROPEN || Detect_Mode == CH_CHECKMODE.BEFORECLOSE)
            {
                //bool b1 = MoveToZ(SReadValue("Pos_Safe_Z").ToInt());
                bool b1 = MoveToZ(SPos_Safe_Z);
                if (b1)
                {
                    Z_Detect = true;
                    TM_Detect.Start();
                    TM_Detect.Enabled = true;
                    
                    LogDebug("CHM", "MoveToZ SPos_Safe_Z " + SPos_Safe_Z.ToString());
                    return FlowChart.FCRESULT.CASE1;
                }
                return FlowChart.FCRESULT.IDLE;
            }

            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart24_Run()
        {
            //CHM Detect Action - Axis Z down to detect
            //if (MoveToY(PReadValue("ClamsSheelCloseDetectZPos").ToInt()))
            if (MoveToZ(PClamsSheelCloseDetectZPos))
            {
                LogDebug("CHM", "MoveToY PClamsSheelCloseDetectZPos " + PClamsSheelCloseDetectZPos.ToString());
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart25_Run()
        {
            //CHM Detect Action - Check Detect
            bool b1 = WorkScanDetect(true) || (SystemDryRun && SystemDryRun_NoBoard);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.CASE1;
        }

        private FlowChart.FCRESULT flowChart26_Run()
        {
            //CHM Detect Action - Axis Z to Safety and Alarm
            //bool b1 = MoveToZ(SReadValue("Pos_Safe_Z").ToInt());
            bool b1 = MoveToZ(SPos_Safe_Z);
            if (b1)
            {
                ShowAlarmMessage("E", (int)CHM_ALARM_CODE.ER_CHM_SCAN_DETECTERROR);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart27_Run()
        {
            //CHM Detect Action - Down
            Flag_DetectAction.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart28_Run()
        {
            //CHM Detect Action - Axis Z to end Detect pos and still Detect
            //Z_Detect = true;
            //TM_Detect.Enabled = true;
            //if (MoveToY(PReadValue("ClamsSheelOpenDetectZPos").ToInt()))
            if (MoveToZ(PClamsSheelOpenDetectZPos))
            {
                LogDebug("CHM", "MoveToY  PClamsSheelOpenDetectZPos " + PClamsSheelOpenDetectZPos.ToString());
                if (!Z_Detect)
                {
                    //TM_Detect.Enabled = false;
                    return FlowChart.FCRESULT.CASE1;
                }
                //TM_Detect.Enabled = false;
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart29_Run()
        {

            //CHM Detect Action - Axis Z to Safety
            //bool b1 = MoveToZ(SReadValue("Pos_Safe_Z").ToInt());
            bool b1 = MoveToZ(SPos_Safe_Z);
            if (b1)
            {
                LogDebug("CHM", "MoveToZ  SPos_Safe_Z " + SPos_Safe_Z.ToString());
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart30_Run()
        {
            //CHM Detect Action -  Axis Z to Safety and Alarm
            //bool b1 = MoveToZ(SReadValue("Pos_Safe_Z").ToInt());
            bool b1 = MoveToZ(SPos_Safe_Z);
            if (b1)
            {
                Z_Detect = true;
                TM_Detect.Enabled = true;
                ShowAlarmMessage("E",(int)CHM_ALARM_CODE.ER_CHM_SCAN_DETECTERROR);
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart31_Run()
        {
            //CHM Detect Action - NEXT
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart32_Run()
        {
            //CHM Detect Action - NEXT
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart33_Run()
        {
            //CHM Detect Action - NEXT
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart34_Run()
        {
            //CHM Detect Action - NEXT
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion

        #region Move Action
        private FlowChart.FCRESULT FC_CHM_MOVE_Run()
        {
            //CHM Move Action - Start Move
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart41_Run()
        {
            //CHM Move Action - Need Move?
            if (Flag_MoveAction.IsDoIt())
            {
                Flag_MoveAction.Doing();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart42_Run()
        {
            //CHM Move Action - Move to Position
            if (MoveToY(nowPos.Y))
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart43_Run()
        {
            //CHM Move Action - Done
            Flag_MoveAction.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart44_Run()
        {
            //CHM Move Action - NEXT
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart45_Run()
        {
            //CHM Move Action - NEXT
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion

        #region Lock Action
        private FlowChart.FCRESULT FC_CHM_LOCK_Run()
        {
            //CHM Lock Action - Start Lock
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart47_Run()
        {
            //CHM Lock Action - Need Lock?
            if (Flag_LockAction.IsDoIt())
            {
                LogDebug("CHM", "LOCK START");
                Flag_LockAction.Doing();
                CHM_ActionResult = ThreeValued.UNKNOWN;
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart48_Run()
        {
            //CHM Lock Action - Side Cy On
            bool b1 = SideCyControl(true);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart49_Run()
        {
            //CHM Lock Action - Side Cy Off
            bool b1 = SideCyControl(false);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart50_Run()
        {
            //CHM Lock Action - Font Cy On
            bool b1 = FontCyControl(true);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart51_Run()
        {
            //CHM Lock Action - Side Cy On
            bool b1 = SideCyControl(true);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart52_Run()
        {
            //CHM Lock Action - Hold Cy On
            bool b1 = HoldCyControl(true);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart53_Run()
        {
            //CHM Lock Action - Stage Up
            bool b1 = StageCyControl(true);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart54_Run()
        {
            //CHM Lock Action - rotato to work
            //int CDir = PReadValue("BoardTable", "ClamsheelDir").ToInt();
            
            int ang = (PClamSheetDir.Equals(2) || PClamSheetDir.Equals(3)) ? SPos_Rotato_H : SPos_Rotato_V;
            int Pos = DegreeToPluse(ang);
            //if (CDir.Equals(2) || CDir.Equals(3))
            //{
                //Pos = SReadValue("Pos_Rotato_H").ToInt();
                //bool b1 = MoveToU(SReadValue("Pos_Rotato_H").ToInt());
                //if (b1)
                //{
                //    return FlowChart.FCRESULT.NEXT;
                //}
           // }
            //else
            //{
                //Pos = SReadValue("Pos_Rotato_V").ToInt();
                //bool b1 = MoveToU(SReadValue("Pos_Rotato_V").ToInt());
                //if (b1)
                //{
                //    return FlowChart.FCRESULT.NEXT;
                //}
            //}
            bool b1 = MoveToU(Pos);
            if (b1)
            {
                LogDebug("CHM", string.Format("LOCK U move to {0}", Pos.ToString()));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
            //return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart55_Run()
        {
            //CHM Lock Action - Done
            Flag_LockAction.Done();
            CHM_ActionResult = ThreeValued.TRUE;
            LogDebug("CHM", "LOCK DONE");
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart56_Run()
        {
            //CHM Lock Action - NEXT
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion

        #region Unlock Action
        private FlowChart.FCRESULT FC_CHM_UNLOCK_Run()
        {
            //CHM Unlock Action - Start Unlock
            
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart57_Run()
        {
            //CHM Unlock Action - Need Unlock?
            if (Flag_UnlockAction.IsDoIt())
            {
                Flag_UnlockAction.Doing();
                CHM_ActionResult = ThreeValued.UNKNOWN;
                LogDebug("CHM", "UNLOCK START");
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart58_Run()
        {
            //CHM Unlock Action - rotato to Ready
            //int CDir = PReadValue("ClamsheelDir").ToInt();
            //if (CDir.Equals(2) || CDir.Equals(3))
            //{
            //    return FlowChart.FCRESULT.NEXT;
            //}
            //else
            //{
            //    bool b1 = MoveToU(SReadValue("Pos_Rotato_V").ToInt());
            //    if (b1)
            //    {
            //        return FlowChart.FCRESULT.NEXT;
            //    }
            //}
            //return FlowChart.FCRESULT.NEXT;
            //int Pos = SReadValue("Pos_Rotato_V").ToInt();
            int Pos = DegreeToPluse(SPos_Rotato_H);
            bool b1 = MoveToU(Pos);
            if (b1)
            {
                LogDebug("CHM", string.Format("UNLOCK U move ready {0}", Pos.ToString()));
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart59_Run()
        {
            //CHM Unlock Action - Stage Down
            bool b1 = StageCyControl(false);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart60_Run()
        {
            //CHM Unlock Action - Side Off
            bool b1 = SideCyControl(false);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart61_Run()
        {
            //CHM Unlock Action - Font Off
            bool b1 = FontCyControl(false);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart62_Run()
        {
            //CHM Unlock Action - Hold Off
            bool b1 = HoldCyControl(false);
            if (b1)
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart63_Run()
        {
            //CHM Unlock Action - Done
            Flag_UnlockAction.Done();
            CHM_ActionResult = ThreeValued.TRUE;
            LogDebug("CHM", "UNLOCK DONE");
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart64_Run()
        {
            //CHM Unlock Action - NEXT
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion

        #region Check Action
        private FlowChart.FCRESULT FC_CHM_CHECK_Run()
        {
            //CHM Check board - Start Check
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart65_Run()
        {
            //CHM Check board - Need Check?
            if (Flag_CheckAction.IsDoIt())
            {
                Flag_CheckAction.Doing();
                //iWorkLine = 0;
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart66_Run()
        {
            //CHM Check board - Move Action
            int dis = 0;
            if (Detect_Mode.Equals(CH_CHECKMODE.BEFOREOPEN) || Detect_Mode.Equals(CH_CHECKMODE.AFTERCLOSE))
            {
                dis = PClamsSheelCloseDetectYPos;//PReadValue("ClamsSheelCloseDetectYPos").ToInt();
            }
            else if (Detect_Mode.Equals(CH_CHECKMODE.BEFORECLOSE) || Detect_Mode.Equals(CH_CHECKMODE.AFTEROPEN))
            {
                dis = PClamsSheelOpenDetectYPos;//PReadValue("ClamsSheelOpenDetectYPos").ToInt();
            }
            bool b1 = MoveYToWork(dis);
           
            if (b1)
            {
                LogDebug("CHM", "MoveYToWork  BeforeCheck " + dis.ToString());
                Flag_DetectAction.DoIt();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
            //if (Detect_Mode == CH_CHECKMODE.BEFOREOPEN || Detect_Mode == CH_CHECKMODE.AFTERCLOSE)
            //{
            //    if (iWorkLine == 0)
            //    {
            //        b1 = MoveYToFirstPos(); //移動至第一排 偵測位置
            //    }
            //    else
            //    {
            //        b1 = MoveYToNextPitch();    //NEXT 偵測位置
            //    }
            //    if (b1)
            //    {
            //        Flag_DetectAction.DoIt();
            //        return FlowChart.FCRESULT.NEXT;
            //    }
            //}
            //else
            //{
            //    if (iWorkLine == 0)
            //    {
            //        b1 = MoveYToFirstPos(); //移動至第一排 偵測位置
            //    }
            //    else
            //    {
            //        b1 = MoveYToNextPitch();    //NEXT 偵測位置
            //    }
            //    if (b1)
            //    {
            //        Flag_DetectAction.DoIt();
            //        return FlowChart.FCRESULT.NEXT;
            //    }
            //}
           // return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart67_Run()
        {
            //CHM Check board - Detect Action
            bool b1 = Flag_DetectAction.IsDone();
            if (b1)
            {
                //iWorkLine++;
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart68_Run()
        {
            //CHM Check board - Check All Finish?
            if (GetNextIndex())
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.CASE1;
            
            
            //if (CDir.Equals(2) || CDir.Equals(3))
            //{
            //    if (iWorkLine < iYN)
            //    {
            //        return FlowChart.FCRESULT.CASE1;
            //    }
            //}
            //else
            //{
            //    if (iWorkLine < iXN)
            //    {
            //        return FlowChart.FCRESULT.CASE1;
            //    }
            //}

            //return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart69_Run()
        {
            //CHM Check board - Axis Z to Safe
            if (MoveZToSafe())
            {
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart70_Run()
        {
            //CHM Check board - Down
            Flag_CheckAction.Done();
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart71_Run()
        {
            //CHM Check board - NEXT
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart72_Run()
        {
            //CHM Check board - NEXT
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart73_Run()
        {
            //CHM Check board - NEXT
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart74_Run()
        {
            //CHM Check board - NEXT
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion

        #region Auto Action
        private FlowChart.FCRESULT FC_CHM_WORK_Run()
        {
            //Auto Action - Start Work
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart75_Run()
        {
            //CHM Close Action - Need Close?
            if (Flag_Action.IsDoIt())
            {
                Flag_Action.Doing();
                CHM_ActionResult = ThreeValued.UNKNOWN;
                if (CHM_Mode == ACTIONMODE.OPEN)
                {
                    LogDebug("CHM", "Start Detect_Before Open");
                    Detect_Mode = CH_CHECKMODE.BEFOREOPEN;
                    Flag_CheckAction.DoIt();
                    if (SReadValue("DoNotUseCloseDetect").ToBoolean())
                    {
                        Flag_CheckAction.Done();
                    }
                    return FlowChart.FCRESULT.NEXT;
                }
                else if (CHM_Mode == ACTIONMODE.CLOSE)
                {
                    LogDebug("CHM", "Start Detect_Before Close");
                    Detect_Mode = CH_CHECKMODE.BEFORECLOSE;
                    Flag_CheckAction.DoIt();
                    if (SReadValue("DoNotUseOpenDetect").ToBoolean())
                    {
                        Flag_CheckAction.Done();
                    }
                    return FlowChart.FCRESULT.CASE1;
                }
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart76_Run()
        {
            //CHM Close Action - Before Open Detect
            bool b1 = Flag_CheckAction.IsDone();
            if (b1)
            {
                LogDebug("CHM", "Start Detect_Before Open Done");
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart77_Run()
        {
            //CHM Close Action - Open
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart78_Run()
        {
            //CHM Close Action - After Open Detect
            bool b1 = Flag_CheckAction.IsDone();
            if (b1)
            {
                LogDebug("CHM", "Start Detect_After Open Done");
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart79_Run()
        {
            //CHM Close Action - Before Close Detect
            bool b1 = Flag_CheckAction.IsDone();
            if (b1)
            {
                LogDebug("CHM", "Start Detect_Before Close Done");
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart80_Run()
        {
            //CHM Close Action - Close
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart81_Run()
        {
            //CHM Close Action - After Close Detect
            bool b1 = Flag_CheckAction.IsDone();
            if (b1)
            {
                LogDebug("CHM", "Start Detect_After Close Done");
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart82_Run()
        {
            //Auto Action - Done
            Flag_Action.Done();
            CHM_ActionResult = ThreeValued.TRUE;
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart84_Run()
        {
            //Auto Action - NEXT
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart85_Run()
        {
            //Auto Action - NEXT
            return FlowChart.FCRESULT.NEXT;
        }
        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            BasePosInfo pPos = new BasePosInfo();
            SetActionCommand_CHM(ACTIONMODE.LOCK);//, CH_CHECKMODE.NONE, pPos);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            iXN = 3;
            iYN = 3;
            if (test.Enabled == false)
            {
                FC_CHM_WORK.TaskReset();
                FC_CHM_CLAMSHELL.TaskReset();
                FC_CHM_CHECK.TaskReset();
                FC_CHM_DETECT.TaskReset();
                FC_CHM_MOVE.TaskReset();
                FC_CHM_LOCK.TaskReset();
                FC_CHM_UNLOCK.TaskReset();
                
                test.Enabled = true;
            }
            else
            {
                test.Enabled = false;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            SetActionCommand_CHM(ACTIONMODE.UNLOCK);//, CH_CHECKMODE.NONE, nowPos);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SetActionCommand_CHM(ACTIONMODE.OPEN);//, CH_CHECKMODE.NONE, nowPos);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SetActionCommand_CHM(ACTIONMODE.CLOSE);//, CH_CHECKMODE.NONE, nowPos);
        }

        private void test_Tick(object sender, EventArgs e)
        {
            FC_CHM_WORK.MainRun();
            FC_CHM_CLAMSHELL.MainRun();
            FC_CHM_CHECK.MainRun();
            FC_CHM_DETECT.MainRun();
            FC_CHM_MOVE.MainRun();
            FC_CHM_LOCK.MainRun();
            FC_CHM_UNLOCK.MainRun();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SetActionCommand_CHM(ACTIONMODE.MOVE);//, CH_CHECKMODE.NONE, nowPos);
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

    }
}

