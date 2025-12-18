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

namespace CSM
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
    public partial class CSM : BaseModuleInterface
    {
        private ThreeValued CheckHaveBoard_A = ThreeValued.UNKNOWN;
        private ThreeValued CheckHaveBoard_B = ThreeValued.UNKNOWN;
        private bool bNotUseLeftCassette = false;
        private bool bNotUseRightCassette = false;
        private bool bRightInLeftOut = false;
        bool SystemDryRun = false;

        public CSM()
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
        public override void AlwaysRun()
        {
            if (IB_BIBSafeDetect_A.Value)
            {
                //Alarm
            }
            if (IB_BIBSafeDetect_A.Value)
            {
                //Alarm
            }

        } //持續掃描
        public override int ScanIO() { return 0; } //掃描硬體按鈕IO

        //歸零相關函數
        public override void HomeReset()
        {
            DataReset();
        } //歸零前的重置
        public override bool Home() { return true; } //歸零

        //運轉相關函數
        public override void ServoOn() { } //Motor Servo On
        public override void ServoOff() { } //Motor Servo Off
        public override void RunReset() { } //運轉前初始化
        public override void Run() { } //運轉
        public override void SetSpeed(bool bHome = false) { } //速度設定

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

        #endregion

        #region 公用函數
        public void DataReset()
        {
            bNotUseLeftCassette = SReadValue("DoNotUseLeftCassette").ToBoolean();
            bNotUseRightCassette = SReadValue("DoNotUseRightCassette").ToBoolean();
            bRightInLeftOut = SReadValue("RightInLeftOut").ToBoolean();
            SystemDryRun = OReadValue("DryRun").ToBoolean();
        }

        public CassetteID GetActionCassette(bool bGet)
        {
            CassetteID id = CassetteID.NONE;
            if (bGet)
            {
                if (bRightInLeftOut)
                {
                    //優先取右邊, 右邊沒開取左邊，左邊也沒開那就不用動了
                    id = CassetteID.RIGHT;
                    if (bNotUseRightCassette) id = CassetteID.LEFT;
                    else if (bNotUseLeftCassette) id = CassetteID.NONE;
                }
                else
                {
                    //Sensor優先
                    if (bNotUseRightCassette && bNotUseLeftCassette)
                    {
                        id = CassetteID.NONE;
                    }
                    else if (!bNotUseLeftCassette && bNotUseRightCassette)
                    {
                        id = CassetteID.LEFT;
                    }
                    else if (bNotUseLeftCassette && !bNotUseRightCassette)
                    {
                        id = CassetteID.RIGHT;
                    }
                    else
                    {
                        if (IB_HaveBIBDetect_B.Value && IB_BIBSafeDetect_B.Value)
                        {
                            id = CassetteID.RIGHT;
                        }
                        else if (IB_HaveBIBDetect_A.Value && IB_BIBSafeDetect_A.Value)
                        {
                            id = CassetteID.LEFT;
                        }
                        else//兩邊都沒板 優先使用右邊
                        {
                            id = CassetteID.RIGHT;
                        }
                    }
                }
                if (id.Equals(CassetteID.RIGHT) && (!IB_HaveBIBDetect_B.Value || !IB_BIBSafeDetect_B.Value) && IsSimulation() == 0)
                {
                    id = CassetteID.NONE;
                }
                if (id.Equals(CassetteID.LEFT) && (!IB_HaveBIBDetect_A.Value || !IB_BIBSafeDetect_A.Value) && IsSimulation() == 0)
                {
                    id = CassetteID.NONE;
                }
            }
            else
            {
                if (bRightInLeftOut)
                {
                    //優先放置左邊，左邊沒開放右邊，右邊也沒開那...
                    id = CassetteID.LEFT;
                    if (bNotUseLeftCassette) id = CassetteID.RIGHT;
                    else if (bNotUseRightCassette) id = CassetteID.NONE;
                }
                else
                {
                    //Sensor優先
                    if (bNotUseRightCassette && bNotUseLeftCassette)    //兩邊都沒開
                    {
                        id = CassetteID.NONE;
                    }
                    else if (!bNotUseLeftCassette && bNotUseRightCassette)  //只開左邊
                    {
                        id = CassetteID.LEFT;
                    }
                    else if (bNotUseLeftCassette && !bNotUseRightCassette)  //只開右邊
                    {
                        id = CassetteID.RIGHT;
                    }
                    else//兩邊都開 Sensor優先
                    {
                        if (!IB_HaveBIBDetect_A.Value)
                        {
                            id = CassetteID.LEFT;
                        }
                        else if (!IB_HaveBIBDetect_B.Value)
                        {
                            id = CassetteID.RIGHT;
                        }
                        else//兩邊都沒板 優先使用右邊
                        {
                            id = CassetteID.LEFT;
                        }
                    }
                }
                if (id.Equals(CassetteID.RIGHT) && IB_HaveBIBDetect_B.Value)
                {
                    id = CassetteID.NONE;
                }
                if (id.Equals(CassetteID.LEFT) && IB_HaveBIBDetect_A.Value)
                {
                    id = CassetteID.NONE;
                }
            }
            return id;
        }

        public ThreeValued CheckHaveBoard(CassetteID id)
        {
            ThreeValued T = ThreeValued.UNKNOWN;
            switch (id)
            {
                case CassetteID.LEFT:
                    {
                        if (bNotUseLeftCassette) break;
                        if (IB_HaveBIBDetect_A.Value && IB_BIBSafeDetect_A.Value)
                        {
                            T = ThreeValued.TRUE;
                        }
                        else
                        {
                            T = ThreeValued.FALSE;
                        }
                        if (IsSimulation() != 0 || SystemDryRun)
                        {
                            T = ThreeValued.FALSE;
                        }
                    }
                    break;
                case CassetteID.RIGHT:
                    {
                        if (bNotUseRightCassette) break;
                        if ((IB_HaveBIBDetect_B.Value && IB_BIBSafeDetect_B.Value )|| IsSimulation() != 0 || SystemDryRun)
                        {
                            T = ThreeValued.TRUE;
                        }
                        else
                        {
                            T = ThreeValued.FALSE;
                        }
                    }
                    break;
                default:
                    {
                        //
                    }
                    break;
            }
            return T;
        }
        #endregion

    }
}

