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

namespace KSM
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
    public partial class KSM : BaseModuleInterface, KSM_interface
    {
        private enum KSM_ALARM_CODE
        {
            ER_KSM_Unknow = 0,
            ER_KSM_SetActionTaskError = 1,//任務指定錯誤

        }
       
        private class TransferShuttleInfo
        {
            private KitShuttleID m_id;
            public KitShuttleID ID { get { return m_id; } }
            public VehicleState State { get; set; }
            public KitShuttleOwner Owner { get; set; }
            public int Position { get; set; }
            public int ViberationCount { get; set; }
            public JActionFlag Flag_Moving = new JActionFlag();

            public TransferShuttleInfo(KitShuttleID id, VehicleState state,
                KitShuttleOwner owner)
            {
                m_id = id;
                State = state;
                Owner = owner;
                Position = 0;
                ViberationCount = 0;

                Flag_Moving.Reset();
            }

            public void Reset()
            {
                State = VehicleState.NONE;
                Owner = KitShuttleOwner.NONE;
                Position = 0;
                ViberationCount = 0;
                Flag_Moving.Reset();
            }
        }

        //左台車A
        private TransferShuttleInfo LTS = new TransferShuttleInfo(KitShuttleID.TransferShuttleA,
            VehicleState.NONE, KitShuttleOwner.NONE);
        //右台車B
        private TransferShuttleInfo RTS = new TransferShuttleInfo(KitShuttleID.TransferShuttleB,
            VehicleState.NONE, KitShuttleOwner.NONE);


        private bool bNotUseLeftShuttle = false;
        private bool bNotUseRightShuttle = false;

        public KSM()
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
        public override void Initial() { }

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

            FC_KSM_HOME.TaskReset();
        }
        public override bool Home() //歸零
        {
            FC_KSM_HOME.MainRun();
            return mHomeOk;
        }

        //運轉相關函數
        public override void ServoOn() //Motor Servo On
        {
            MT_KSM_ShuttleA.ServoOn(true);
            MT_KSM_ShuttleB.ServoOn(true);
        }
        public override void ServoOff() //Motor Servo Off
        {
            MT_KSM_ShuttleA.ServoOn(false);
            MT_KSM_ShuttleB.ServoOn(false);
        }
        public override void RunReset() //運轉前初始化
        {
            FC_KSM_ShuttleA_AUTORUN.TaskReset();
            FC_KSM_ShuttleB_AUTORUN.TaskReset();
        }
        public override void Run() //運轉
        {
            FC_KSM_ShuttleA_AUTORUN.MainRun();
            FC_KSM_ShuttleB_AUTORUN.MainRun();
        }
        public override void SetSpeed(bool bHome = false) //速度設定
        {
            int SPD = 10000;//! 速度要再確認
            int ACC_MULTIPLE = 100000;
            int ACC_DEC = 100000;
            int SPEED_RATE = OReadValue("機台速率").ToInt();

            if (MT_KSM_ShuttleA != null)
            {
                if (!bHome)
                {
                    SPD = (SReadValue("MT_KSM_ShuttleA_Y_SPEED").ToInt() * SPEED_RATE) / 100;
                    ACC_MULTIPLE = SReadValue("MT_KSM_ShuttleA_Y_ACC_MULTIPLE").ToInt();
                    ACC_DEC = (SPD * ACC_MULTIPLE);
                }
                MT_KSM_ShuttleA.SetSpeed(SPD);
                MT_KSM_ShuttleA.SetAcceleration(ACC_DEC);
                MT_KSM_ShuttleA.SetDeceleration(ACC_DEC);
            }

            if (MT_KSM_ShuttleB != null)
            {
                if (!bHome)
                {
                    SPD = (SReadValue("MT_KSM_ShuttleB_Y_SPEED").ToInt() * SPEED_RATE) / 100;
                    ACC_MULTIPLE = SReadValue("MT_KSM_ShuttleB_Y_ACC_MULTIPLE").ToInt();
                    ACC_DEC = (SPD * ACC_MULTIPLE);
                }
                MT_KSM_ShuttleB.SetSpeed(SPD);
                MT_KSM_ShuttleB.SetAcceleration(ACC_DEC);
                MT_KSM_ShuttleB.SetDeceleration(ACC_DEC);
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
                //LTS.Owner = KitShuttleOwner.NONE;
                //LTS.State = VehicleState.NONE;
                //LTS.Position = 0;
                //LTS.Flag_Moving.Reset();

                //RTS.Owner = KitShuttleOwner.NONE;
                //RTS.State = VehicleState.NONE;
                //RTS.Position = 0;
                //RTS.Flag_Moving.Reset();

            bNotUseLeftShuttle = SReadValue("DoNotUseLeftShuttle").ToBoolean();
            bNotUseRightShuttle = SReadValue("DoNotUseRightShuttle").ToBoolean();
        }

        public bool IsUseShuttle(KitShuttleID id)
        {
            bool bRet = false;
            switch (id)
            {
                case KitShuttleID.TransferShuttleA:
                    {
                        bRet = !bNotUseLeftShuttle;
                    }
                    break;
                case KitShuttleID.TransferShuttleB:
                    {
                        bRet = !bNotUseRightShuttle;
                    }
                    break;
            }
            return bRet;
        }

        /// <summary>
        /// 取得指定狀態的台車車號
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public KitShuttleID IsTransferShuttleReady(VehicleState state)
        {
            if (LTS.State.HasFlag(state))
            {
                return LTS.ID;
            }
            if (RTS.State.HasFlag(state))
            {
                return RTS.ID;
            }

            return KitShuttleID.NONE;
        }



        /// <summary>
        /// 鎖定台車
        /// </summary>
        /// <param name="owner">台車使用者</param>
        /// <param name="state">台車狀態</param>
        /// <returns></returns>
        public ThreeValued LockTransferShuttle(KitShuttleOwner owner, KitShuttleID id)
        {
            ThreeValued bRet = ThreeValued.UNKNOWN;
            TransferShuttleInfo TS = null;
            switch (id)
            {
                case KitShuttleID.TransferShuttleA:
                    {
                        TS = LTS;
                    }
                    break;
                case KitShuttleID.TransferShuttleB:
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
                else if (TS.Owner.Equals(KitShuttleOwner.NONE))
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

        /// <summary>
        /// 解除台車鎖定
        /// </summary>
        /// <param name="owner">台車使用者</param>
        /// <param name="id">台車編號</param>
        /// <returns></returns>
        public ThreeValued UnlockTransferShuttle(KitShuttleOwner owner, KitShuttleID id)
        {
            ThreeValued bRet = ThreeValued.UNKNOWN;
            TransferShuttleInfo TS = null;
            switch (id)
            {
                case KitShuttleID.TransferShuttleA:
                    {
                        TS = LTS;
                    }
                    break;
                case KitShuttleID.TransferShuttleB:
                    {
                        TS = RTS;
                    }
                    break;
            }
            if (TS != null)
            {
                if (TS.Owner.Equals(KitShuttleOwner.NONE))
                {
                    bRet = ThreeValued.TRUE;
                }
                else if (TS.Owner.Equals(owner))
                {
                    // if (TransferShuttleMoveToUnlockPos(id, owner))
                    // {
                    TS.Owner = KitShuttleOwner.NONE;
                    bRet = ThreeValued.TRUE;
                    //  }
                }
                else
                {
                    bRet = ThreeValued.FALSE;
                }
            }
            return bRet;
        }

        /// <summary>
        /// 判斷台車是否鎖定
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is transfer shuttle locked] [the specified identifier]; otherwise, <c>false</c>.
        /// </returns>
        public ThreeValued IsTransferShuttleLocked(KitShuttleID id)
        {
            ThreeValued bRet = ThreeValued.UNKNOWN;
            TransferShuttleInfo TS = null;
            switch (id)
            {
                case KitShuttleID.TransferShuttleA:
                    {
                        TS = LTS;
                    }
                    break;
                case KitShuttleID.TransferShuttleB:
                    {
                        TS = RTS;
                    }
                    break;
            }
            if (TS != null)
            {
                //bRet = !TS.Owner.Equals(KitShuttleOwner.NONE);

                if (TS.Owner.Equals(KitShuttleOwner.NONE))
                {
                    bRet = ThreeValued.FALSE;
                }
                else if (!TS.Owner.Equals(KitShuttleOwner.NONE))
                {
                    bRet = ThreeValued.TRUE;
                }               
                
            }
            return bRet;
        }

        /// <summary>
        /// 設定台車狀態
        /// </summary>
        /// <param name="id">台車編號</param>
        /// <param name="state">台車狀態</param>
        /// <returns></returns>
        public ThreeValued SetTransferShuttleState(KitShuttleID id, VehicleState state)
        {
            ThreeValued bRet = ThreeValued.UNKNOWN;
            TransferShuttleInfo TS = null;
            switch (id)
            {
                case KitShuttleID.TransferShuttleA:
                    {
                        TS = LTS;
                    }
                    break;
                case KitShuttleID.TransferShuttleB:
                    {
                        TS = RTS;
                    }
                    break;
            }
            if (TS != null)
            {
                if (TS.Owner.Equals(KitShuttleOwner.NONE))
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

        /// <summary>
        /// 設定台車移動位置
        /// </summary>
        /// <param name="owner">台車使用者</param>
        /// <param name="id">台車編號</param>
        /// <param name="pos">移動位置</param>
        /// <returns></returns>
        public ThreeValued SetActionCommand_KSM(KitShuttleOwner owner, KitShuttleID id, ACTIONMODE mode, BasePosInfo pos)
        {
            ThreeValued bRet = ThreeValued.UNKNOWN;
            TransferShuttleInfo TS = null;

            if (owner.Equals(KitShuttleOwner.NONE) || id.Equals(KitShuttleID.NONE) || mode.Equals(KitShuttleID.NONE))
            {
                ShowAlarm("E", (int)KSM_ALARM_CODE.ER_KSM_SetActionTaskError);
                return ThreeValued.UNKNOWN;
            }

            switch (id)
            {
                case KitShuttleID.TransferShuttleA:
                    {
                        TS = LTS;
                    }
                    break;
                case KitShuttleID.TransferShuttleB:
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
                        TS.Position = pos.Y;
                        TS.Flag_Moving.DoIt();
                        SetSpeed();
                        bRet = ThreeValued.TRUE;
                        LogDebug("KSM", string.Format("SetActionCommand_KSM({0},{1},{2}) return true.", owner.ToString(), id.ToString(), pos.Y));
                    }
                    else
                    {
                        bRet = ThreeValued.FALSE;
                    }
                }
            }
            return bRet;
        }

        /// <summary>
        /// 台車移動是否完成
        /// </summary>
        /// <param name="owner">台車使用者</param>
        /// <param name="id">台車編號</param>
        /// <returns>
        ///   <c>true</c>if [移動完成]; otherwise, <c>false</c>.
        /// </returns>       
        public ThreeValued GetActionResult_KSM(KitShuttleOwner owner, KitShuttleID id)
        {
            ThreeValued bRet = ThreeValued.UNKNOWN;
            TransferShuttleInfo TS = null;
            switch (id)
            {
                case KitShuttleID.TransferShuttleA:
                    {
                        TS = LTS;
                    }
                    break;
                case KitShuttleID.TransferShuttleB:
                    {
                        TS = RTS;
                    }
                    break;
            }
            if (TS != null)
            {
                if (TS.Owner.Equals(owner))
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
            }
            if (bRet == ThreeValued.TRUE)
            {
                LogDebug("KSM", string.Format("GetActionResult_KSM({0},{1}) return true.", owner.ToString(), id.ToString()));
            }
            return bRet;
        }

        public BasePosInfo GetBasicInfo(KitShuttleOwner station, KitShuttleID id)
        {
            BasePosInfo pos = new BasePosInfo();

            switch (station)
            {
                case KitShuttleOwner.HDT_BF_A:
                    {
                        pos.X = 0;
                        pos.Y = id.Equals(KitShuttleID.TransferShuttleA) ? SReadValue("Pos_ShuttleA_BFM_A_Y").ToInt() : SReadValue("Pos_ShuttleB_BFM_A_Y").ToInt();
                        pos.Z = 0;
                    }
                    break;
                case KitShuttleOwner.HDT_BF_B:
                    {
                        pos.X = 0;
                        pos.Y = id.Equals(KitShuttleID.TransferShuttleA) ? SReadValue("Pos_ShuttleA_BFM_B_Y").ToInt() : SReadValue("Pos_ShuttleB_BFM_B_Y").ToInt();
                        pos.Z = 0;
                    }
                    break;
                case KitShuttleOwner.HDT_BIB_A:
                    {
                        pos.X = 0;
                        pos.Y = id.Equals(KitShuttleID.TransferShuttleA) ? SReadValue("Pos_ShuttleA_HDT_A_Y").ToInt() : SReadValue("Pos_ShuttleB_HDT_A_Y").ToInt();
                        pos.Z = 0;
                    }
                    break;
                case KitShuttleOwner.HDT_BIB_B:
                    {
                        pos.X = 0;
                        pos.Y = id.Equals(KitShuttleID.TransferShuttleA) ? SReadValue("Pos_ShuttleA_HDT_B_Y").ToInt() : SReadValue("Pos_ShuttleB_HDT_B_Y").ToInt();
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

        /// <summary>
        /// 取得基準點
        /// </summary>
        /// <param name="TS">TransferShuttleInfo</param>
        /// <param name="BasicPosY">The basic position y.</param>
        /// <returns></returns>
        private bool GetBasicPosY(TransferShuttleInfo TS, ref int BasicPosY)
        {
            bool bRet = false;
            if (TS != null)
            {
                switch (TS.Owner)
                {
                    case KitShuttleOwner.HDT_BF_A:
                        {
                            if (TS.ID.Equals(KitShuttleID.TransferShuttleA))
                            {
                                BasicPosY = SReadValue("Pos_ShuttleA_HDT_A_Y").ToInt();
                                bRet = true;
                            }
                            else if (TS.ID.Equals(KitShuttleID.TransferShuttleB))
                            {
                                BasicPosY = SReadValue("Pos_ShuttleB_HDT_A_Y").ToInt();
                                bRet = true;
                            }
                        }
                        break;
                    case KitShuttleOwner.HDT_BF_B:
                        {
                            if (TS.ID.Equals(KitShuttleID.TransferShuttleA))
                            {
                                BasicPosY = SReadValue("Pos_ShuttleA_HDT_B_Y").ToInt();
                                bRet = true;
                            }
                            else if (TS.ID.Equals(KitShuttleID.TransferShuttleB))
                            {
                                BasicPosY = SReadValue("Pos_ShuttleB_HDT_B_Y").ToInt();
                                bRet = true;
                            }
                        }
                        break;
                    case KitShuttleOwner.HDT_BIB_A:
                        {
                            if (TS.ID.Equals(KitShuttleID.TransferShuttleA))
                            {
                                BasicPosY = SReadValue("Pos_ShuttleA_BFM_A_Y").ToInt();
                                bRet = true;
                            }
                            else if (TS.ID.Equals(KitShuttleID.TransferShuttleB))
                            {
                                BasicPosY = SReadValue("Pos_ShuttleB_BFM_A_Y").ToInt();
                                bRet = true;
                            }
                        }
                        break;
                    case KitShuttleOwner.HDT_BIB_B:
                        {
                            if (TS.ID.Equals(KitShuttleID.TransferShuttleA))
                            {
                                BasicPosY = SReadValue("Pos_ShuttleA_BFM_B_Y").ToInt();
                                bRet = true;
                            }
                            else if (TS.ID.Equals(KitShuttleID.TransferShuttleB))
                            {
                                BasicPosY = SReadValue("Pos_ShuttleB_BFM_B_Y").ToInt();
                                bRet = true;
                            }
                        }
                        break;
                }
            }
            return bRet;
        }
        private int GetOffsetValue(TransferShuttleInfo TS)
        {
            int offset_value = 0;
            if (TS != null)
            {
                switch (TS.Owner)
                {
                    case KitShuttleOwner.HDT_BF_A:
                        {
                            if (TS.ID.Equals(KitShuttleID.TransferShuttleA))
                            {
                                offset_value = PReadValue("Offset_HDTA_BIB_A_KIT_Y").ToInt();
                            }
                            else if (TS.ID.Equals(KitShuttleID.TransferShuttleB))
                            {
                                offset_value = PReadValue("Offset_HDTA_BIB_B_KIT_Y").ToInt();
                            }
                        }
                        break;
                    case KitShuttleOwner.HDT_BF_B:
                        {
                            if (TS.ID.Equals(KitShuttleID.TransferShuttleA))
                            {
                                offset_value = PReadValue("Offset_HDTB_BIB_A_KIT_Y").ToInt();
                            }
                            else if (TS.ID.Equals(KitShuttleID.TransferShuttleB))
                            {
                                offset_value = PReadValue("Offset_HDTB_BIB_B_KIT_Y").ToInt();
                            }
                        }
                        break;
                    case KitShuttleOwner.HDT_BIB_A:
                        {
                            if (TS.ID.Equals(KitShuttleID.TransferShuttleA))
                            {
                                offset_value = PReadValue("Offset_HDT_BFA__KITA_Y").ToInt();
                            }
                            else if (TS.ID.Equals(KitShuttleID.TransferShuttleB))
                            {
                                offset_value = PReadValue("Offset_HDT_BFA_KITB_Y").ToInt();
                            }
                        }
                        break;
                    case KitShuttleOwner.HDT_BIB_B:
                        {
                            if (TS.ID.Equals(KitShuttleID.TransferShuttleA))
                            {
                                offset_value = PReadValue("Offset_HDT_BFB_KITA_Y").ToInt();
                            }
                            else if (TS.ID.Equals(KitShuttleID.TransferShuttleB))
                            {
                                offset_value = PReadValue("Offset_HDT_BFB_KITB_Y").ToInt();
                            }
                        }
                        break;
                }
            }
            return offset_value;
        }

        private void ShowAlarmMessage(string AlarmLevel, int ErrorCode, params object[] args)//Alarm顯示
        {
            ShowAlarm(AlarmLevel, ErrorCode, args);
        }
        private void ClearAlarmMessage(string AlarmLevel, int ErrorCode)
        {
            return;
        }


        #endregion


        #region 公用函數
        public void Protection_KSM(KitShuttleOwner owner, KitShuttleID id)
        {
            switch (owner)
            {
                case KitShuttleOwner.HDT_BF_A:
                    {
                        switch (id)
                        {
                            case KitShuttleID.TransferShuttleA:
                                {
                                    if (MT_KSM_ShuttleA.Busy()) MT_KSM_ShuttleA.FastStop();
                                }
                                break;
                            case KitShuttleID.TransferShuttleB:
                                {
                                    if (MT_KSM_ShuttleB.Busy()) MT_KSM_ShuttleB.FastStop();
                                }
                                break;
                        }
                    }
                    break;
                case KitShuttleOwner.HDT_BF_B:
                    {
                        switch (id)
                        {
                            case KitShuttleID.TransferShuttleA:
                                {
                                    if (MT_KSM_ShuttleA.Busy()) MT_KSM_ShuttleA.FastStop();
                                }
                                break;
                            case KitShuttleID.TransferShuttleB:
                                {
                                    if (MT_KSM_ShuttleB.Busy()) MT_KSM_ShuttleB.FastStop();
                                }
                                break;
                        }
                    }
                    break;
                case KitShuttleOwner.HDT_BIB_A:
                    {
                        switch (id)
                        {
                            case KitShuttleID.TransferShuttleA:
                                {
                                    if (MT_KSM_ShuttleA.Busy()) MT_KSM_ShuttleA.FastStop();
                                }
                                break;
                            case KitShuttleID.TransferShuttleB:
                                {
                                    if (MT_KSM_ShuttleB.Busy()) 
                                        MT_KSM_ShuttleB.FastStop();
                                }
                                break;
                        }
                    }
                    break;
                case KitShuttleOwner.HDT_BIB_B:
                    {
                        switch (id)
                        {
                            case KitShuttleID.TransferShuttleA:
                                {
                                    if (MT_KSM_ShuttleA.Busy()) MT_KSM_ShuttleA.FastStop();
                                }
                                break;
                            case KitShuttleID.TransferShuttleB:
                                {
                                    if (MT_KSM_ShuttleB.Busy()) 
                                        MT_KSM_ShuttleB.FastStop();
                                }
                                break;
                        }
                    }
                    break;
            }
        }
        #endregion



        #region 歸零流程
        private FlowChart.FCRESULT FC_KSM_HOME_Run()//Start Home
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart1_Run()//Can Home ?
        {
            if (mCanHome)
            {
                MT_KSM_ShuttleA.HomeReset();
                MT_KSM_ShuttleB.HomeReset();

                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart2_Run()//Shuttle A、B Home
        {
            bool b1 = MT_KSM_ShuttleA.Home();
            bool b2 = MT_KSM_ShuttleB.Home();
            if (b1 && b2)
            {
                SetSpeed();
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart3_Run()//Done
        {
            mHomeOk = true;
            return FlowChart.FCRESULT.IDLE;
        }

        #endregion


        #region 自動流程 ShuttleA
        private FlowChart.FCRESULT FC_KSM_ShuttleA_AUTORUN_Run()//Start Shuttle A Move
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart4_Run()//Dose Shuttle Need Move?
        {
            if (LTS.Flag_Moving.IsDoIt())
            {
                LTS.Flag_Moving.Doing();
                LogDebug("KSM", "LeftTransferShuttle(Shuttle A) moving process - START.");
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart5_Run()//Shuttle Move
        {
            int BasicPosY = 0;
            if (GetBasicPosY(LTS, ref BasicPosY))
            {
                int OffsetY = GetOffsetValue(LTS);
                //int pos = BasicPosY + OffsetY + LTS.Position;
                int pos = LTS.Position;//由主流程運算完提供

                bool bRet = MT_KSM_ShuttleA.G00(pos);
                if (bRet)
                {
                    LogDebug("KSM", string.Format("【{0}】 ask 【{1}】 move to 【{2}】", LTS.Owner.ToString(), LTS.ID.ToString(), pos));
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE;       
        }

        private FlowChart.FCRESULT flowChart6_Run()//Done
        {
            LTS.Flag_Moving.Done();
            LogDebug("KSM", "LeftTransferShuttle(Shuttle A) moving process - END.");
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart10_Run()//Next
        {
            return FlowChart.FCRESULT.NEXT;
        }

        #endregion


        #region 自動流程 ShuttleB
        private FlowChart.FCRESULT FC_KSM_ShuttleB_AUTORUN_Run()//Start Shuttle B Move
        {
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart7_Run()//Dose Shuttle Need Move?
        {
            if (RTS.Flag_Moving.IsDoIt())
            {
                RTS.Flag_Moving.Doing();
                LogDebug("KSM", "RightTransferShuttle(Shuttle B) moving process - START.");
                return FlowChart.FCRESULT.NEXT;
            }
            return FlowChart.FCRESULT.IDLE;
        }

        private FlowChart.FCRESULT flowChart8_Run()//Shuttle Move
        {
            int BasicPosY = 0;
            if (GetBasicPosY(RTS, ref BasicPosY))
            {
                int OffsetY = GetOffsetValue(RTS);
                //int pos = BasicPosY + OffsetY + RTS.Position;
                int pos = RTS.Position;//由主流程運算完提供

                bool bRet = MT_KSM_ShuttleB.G00(pos);
                if (bRet)
                {
                    LogDebug("KSM", string.Format("【{0}】 ask 【{1}】 move to 【{2}】", RTS.Owner.ToString(), RTS.ID.ToString(), pos));
                    return FlowChart.FCRESULT.NEXT;
                }
            }
            return FlowChart.FCRESULT.IDLE; 
        }

        private FlowChart.FCRESULT flowChart9_Run()//Done
        {
            RTS.Flag_Moving.Done();
            LogDebug("KSM", "RightTransferShuttle(Shuttle B) moving process - END.");
            return FlowChart.FCRESULT.NEXT;
        }

        private FlowChart.FCRESULT flowChart11_Run()//Next
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

        private void button4_Click(object sender, EventArgs e)
        {
            ServoOn();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ServoOff();
        }
        


    }
}

