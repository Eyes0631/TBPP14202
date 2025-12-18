using PaeLibGeneral;
using PaeLibProVSDKEx;
using ProVLib;
using System;

namespace PaeLibBibExchangerModule
{
    /// <summary>
    /// 升降裝置類別
    /// </summary>
    public class BemElevator : JModuleBase, IDisposable
    {
        #region I/O & Motion 元件

        private DigitalInput DI_FrontBIBPositionDetector = null;        //前板定位偵測(B接點)靠近手推台車
        private DigitalInput DI_BackBIBPositionDetector = null;         //後板定位偵測(B接點)靠近主機
        private Motor MO_AxisZ = null;                                      //Z軸馬達

        #endregion I/O & Motion 元件

        #region 私有變數

        //Task
        private JActionTask AutoTask = new JActionTask();

        private JActionTask HomeTask = new JActionTask();

        #endregion 私有變數

        protected override void SimulationSetting(bool simulation)
        {
            //設定各 I/O 元件 Simulation
            if (DI_FrontBIBPositionDetector != null)
            DI_FrontBIBPositionDetector.Simulation = simulation;
            if (DI_BackBIBPositionDetector != null)
            DI_BackBIBPositionDetector.Simulation = simulation;
        }

        #region 屬性

        /// <summary>
        /// 手推台車基準高度(上層平台對準推車第一層(最下層)的高度)
        /// </summary>
        public int CarrierBaseHeight { get; set; }

        /// <summary>
        /// 主機板機構基準高度(上層平台對準主機板機構的高度)
        /// </summary>
        public int StageBaseHeight { get; set; }

        /// <summary>
        /// 上下層平台間距
        /// </summary>
        public int TransferPitch { get; set; }

        /// <summary>
        /// 偵測推車板子高度偏移量(相對於進出板高度而言)
        /// </summary>
        public int CarrierBIBDetectHeightOffset { get; set; }

        #endregion 屬性

        #region 建構與解構

        /// <summary>
        /// 升降裝置類別建構子
        /// </summary>
        /// <param name="alarm">Alarm的委派</param>
        /// <param name="ib_FrontBIBPositionDetector">前板定位偵測(B接點)靠近手推台車</param>
        /// <param name="ib_BackBIBPositionDetector">後板定位偵測(B接點)靠近主機</param>
        /// <param name="mo_AxisZ">Z軸馬達</param>
        public BemElevator(
            AlarmCallback alarm,
            InBit ib_FrontBIBPositionDetector, InBit ib_BackBIBPositionDetector,
            Motor mo_AxisZ)
            : base(alarm)
        {
            DI_FrontBIBPositionDetector = new DigitalInput(ib_FrontBIBPositionDetector);
            DI_BackBIBPositionDetector = new DigitalInput(ib_BackBIBPositionDetector);
            MO_AxisZ = mo_AxisZ;

            Simulation = false;
            DryRun = false;
            CarrierBaseHeight = 0;
            StageBaseHeight = 0;
            TransferPitch = 0;
            CarrierBIBDetectHeightOffset = 0;
        }

        ~BemElevator()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (DI_FrontBIBPositionDetector != null) DI_FrontBIBPositionDetector.Dispose();
            if (DI_BackBIBPositionDetector != null) DI_BackBIBPositionDetector.Dispose();
            if (MO_AxisZ != null) MO_AxisZ.Dispose();
        }

        #endregion 建構與解構

        #region 私有函式

        private void ShowModuleAlarm(int ret)//模組Alarm
        {
            this.ShowAlarm(ret);
        }

        private void ClearModuleAlarm(int ret)
        {
            this.ClearAlarm(ret);
        }

        /// <summary>
        /// 判斷狀態是否安全可讓 Z 軸移動
        /// </summary>
        /// <returns>true : 安全，false : 不安全</returns>
        private bool IsSafeForAxisZMoving()
        {
            bool bRet = true;
            if (!this.Simulation)
            {
                if (DI_FrontBIBPositionDetector != null && DI_FrontBIBPositionDetector.ValueOff)
                {
                    //Alarm : 前板定位不良
                    ShowModuleAlarm((int)BEMAlarmCode.ArmBemElevatorFrontBIBPositionError);
                    bRet = false;
                }
                else if (DI_BackBIBPositionDetector != null && DI_BackBIBPositionDetector.ValueOff)
                {
                    //Alarm : 後板定位不良
                    ShowModuleAlarm((int)BEMAlarmCode.ArmBemElevatorBackBIBPositionError);
                    bRet = false;
                }
            }
            return bRet;
        }

        #endregion 私有函式

        #region 公有函式

        /// <summary>
        /// Z 軸安全防護 - 於 AlwaysRun 中呼叫
        /// </summary>
        /// <param name="safe">true : 安全，false : 不安全 (由外部觸發安全防護使用)</param>
        public void SafetyProtection(bool safe)
        {
            if (MO_AxisZ != null)
            {
                if (MO_AxisZ.Busy())
                {
                    if (!safe || !IsSafeForAxisZMoving())
                    {
                        MO_AxisZ.FastStop();
                        //Alarm : 觸發安全防護，Z 軸緊急停止!!
                        ShowModuleAlarm((int)BEMAlarmCode.ArmBemElevatorSafetyProtection);
                    }
                }
            }
        }

        #endregion 公有函式

        #region 動作流程

        /// <summary>
        /// 資料重置
        /// </summary>
        public void DataReset()
        {
            //重置變數

            TaskReset();
            HomeReset();
        }

        /// <summary>
        /// 動作流程重置
        /// </summary>
        public void TaskReset()// 重置 Task
        {
            AutoTask.Reset();   //Reset to zero.
        }

        public bool GotoPosition(int stageID, int iPos)      //移動至指定位置
        {
            if (MO_AxisZ != null)
            {
                if (IsSafeForAxisZMoving())
                {

                    int WP = iPos;
                    if (stageID.Equals(0))  //基準高度以上層平台為基準，固下層平台為要再加上下台車平台的間距
                    {
                        WP += TransferPitch;
                    }
                    if (MO_AxisZ.G00(WP))
                    {
                        return true;	//移動完成
                    }
                }
            }
            else
            {
                //Alarm : Z軸馬達元件未初始化
                ShowModuleAlarm((int)BEMAlarmCode.ArmBemElevatorAxisZNull);
            }
            return false;
        }

        public bool GotoStageWorkingPosition(int stageID)
        {
            if (MO_AxisZ != null)
            {
                if (IsSafeForAxisZMoving())
                {
                    if (CarrierBaseHeight < 0)
                    {
                        //Alarm : 基準高度點位設定錯誤，無法計算 Z 軸工作高度
                        ShowModuleAlarm((int)BEMAlarmCode.ArmBemElevatorBaseHeightDataError);
                        return false;
                    }
                    if (stageID < 0 || stageID > 1)
                    {
                        //Alarm : 上下層工作平台設定錯誤，無法計算 Z 軸工作高度
                        ShowModuleAlarm((int)BEMAlarmCode.ArmBemElevatorStageIDDataError);
                        return false;
                    }
                    int WP = StageBaseHeight;
                    if (stageID.Equals(0))  //基準高度以上層平台為基準，固下層平台為要再加上下台車平台的間距
                    {
                        WP += TransferPitch;
                    }
                    if (MO_AxisZ.G00(WP))
                    {
                        return true;	//移動完成
                    }
                }
            }
            else
            {
                //Alarm : Z軸馬達元件未初始化
                ShowModuleAlarm((int)BEMAlarmCode.ArmBemElevatorAxisZNull);
            }
            return false;
        }

        /// <summary>
        /// 移動至指定平台之指定工作高度
        /// </summary>
        /// <param name="stageID">指定平台ID : 下層平台為 0，上層平台為 1</param>
        /// <param name="SlotHeight">指定工作高度(相對於第一層)</param>
        /// <returns>true : 移動完成，false : 移動中...</returns>
        public bool GotoCarrierWorkingPosition(int stageID, int height)
        {
            if (MO_AxisZ != null)
            {
                if (IsSafeForAxisZMoving())
                {
                    if (CarrierBaseHeight < 0)
                    {
                        //Alarm : 基準高度點位設定錯誤，無法計算 Z 軸工作高度
                        ShowModuleAlarm((int)BEMAlarmCode.ArmBemElevatorBaseHeightDataError);
                        return false;
                    }
                    if (height < 0)
                    {
                        //Alarm : 推車工作樓層高度設定錯誤，無法計算 Z 軸工作高度
                        ShowModuleAlarm((int)BEMAlarmCode.ArmBemElevatorSlotHeightDataError);
                        return false;
                    }
                    if (stageID < 0 || stageID > 1)
                    {
                        //Alarm : 上下層工作平台設定錯誤，無法計算 Z 軸工作高度
                        ShowModuleAlarm((int)BEMAlarmCode.ArmBemElevatorStageIDDataError);
                        return false;
                    }
                    int WP = CarrierBaseHeight + height;
                    if (stageID.Equals(0))  //基準高度以上層平台為基準，固下層平台為要再加上下台車平台的間距
                    {
                        WP += TransferPitch;
                    }
                    if (MO_AxisZ.G00(WP))
                    {
                        return true;	//移動完成
                    }
                }
            }
            else
            {
                //Alarm : Z軸馬達元件未初始化
                ShowModuleAlarm((int)BEMAlarmCode.ArmBemElevatorAxisZNull);
            }
            return false;
        }

        /// <summary>
        /// 移動至指定平台之指定樓層高度進行推車板偵測
        /// </summary>
        /// <param name="stageID">指定平台ID : 下層平台為 0，上層平台為 1</param>
        /// <param name="slotHeight">指定樓層高度(相對於第一層)</param>
        /// <returns>true : 移動完成，false : 移動中...</returns>
        public bool GotoCarrierBIBDetectPosition(int stageID, int slotHeight)
        {
            int DetectBIBHeight = slotHeight + CarrierBIBDetectHeightOffset;
            bool bRet = GotoCarrierWorkingPosition(stageID, DetectBIBHeight);
            return bRet;
        }

        #endregion 動作流程

        #region 歸零流程

        /// <summary>
        /// 重置 HomeTask
        /// </summary>
        private void HomeReset()
        {
            HomeTask.Reset();   //Reset to zero.
            MO_AxisZ.HomeReset();
        }

        public bool Home()
        {
            bool bRet = false;
            if (IsSafeForAxisZMoving())
            {
                if (MO_AxisZ.Home())
                {
                    bRet = true;
                }
            }
            return bRet;
        }

        #endregion 歸零流程
    }
}