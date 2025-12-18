using PaeLibGeneral;
using PaeLibProVSDKEx;
using ProVLib;
using System;

namespace PaeLibBibExchangerModule
{
    public class BemTransfer : JModuleBase, IDisposable
    {
        #region I/O & Motion 元件

        private DigitalInput DI_FrontTrackBIBDetector   = null;     //前軌道板偵測(B接點，靠近推車端)
        private DigitalInput DI_BackTrackBIBDetector    = null;      //後軌道板偵測(B接點，靠近主機端)
        private DigitalInput DI_HookBIBDetector         = null;           //勾爪處有無板偵測
        private DigitalInput DI_CarrierBIBDetector = null;        //推車有無板偵測
        private OutBit OB_AphaStepMotorAlarmReset = null;          //重置東方馬達的Alarm
        private Cylinder CY_FrontHook = null;                     //前勾爪氣缸(靠近推車端)
        private Cylinder CY_BackHook = null;                      //後勾爪氣缸(靠近主機端)
        private Cylinder CY_Pressure = null;                       //部分供板機含有板固定氣缸  Mars 20201231 add
        private Cylinder CY_CarrierGate = null;                    //閘門氣缸                  Mars 20201231 add
        private Motor MO_AxisY = null;                             //Y 軸馬達

        #endregion I/O & Motion 元件

        #region 私有變數

        //Barcode Reader
        private JBarcodeReader BarcodeReader = new JBarcodeReader();

        //Task
        private JActionTask AutoTask = new JActionTask();

        private JActionTask HomeTask = new JActionTask();
        private JActionTask ReadBIDTask = new JActionTask();

        //勾板氣缸上升 Delay Time
        private int iHookUpDelayTime = 300;

        //勾板氣缸下降 Delay Time
        private int iHookDownDelayTime = 300;

        //板固定氣缸上升 Delay Time
        private int iPressureUpDelayTime = 300;

        //板固定氣缸下降 Delay Time
        private int iPressureDownDelayTime = 300;

        //Y 軸點位補償值
        private int iYPosOffset = 0;

        #endregion 私有變數

        protected override void SimulationSetting(bool simulation)
        {
            //設定各 I/O 元件 Simulation
            
            if (DI_FrontTrackBIBDetector != null) DI_FrontTrackBIBDetector.Simulation = simulation;
            if (DI_BackTrackBIBDetector != null) DI_BackTrackBIBDetector.Simulation = simulation;
            if (DI_HookBIBDetector != null) DI_HookBIBDetector.Simulation = simulation;
            if (DI_CarrierBIBDetector != null) DI_CarrierBIBDetector.Simulation = simulation;
            if (CY_FrontHook != null) CY_FrontHook.Simulation = simulation;
            if (CY_BackHook != null) CY_BackHook.Simulation = simulation;
            if (CY_Pressure != null) CY_Pressure.Simulation = simulation;
            if (CY_CarrierGate != null) CY_CarrierGate.Simulation = simulation;
        }

        #region 屬性

        /// <summary>
        /// 前後勾爪距離
        /// </summary>
        public int DistanceOfHook { get; set; }

        /// <summary>
        /// 主機端送板點
        /// </summary>
        public int SendBIBPosition { get; set; }

        /// <summary>
        /// 主機端收板點
        /// </summary>
        public int ReceiveBIBPosition { get; set; }

        /// <summary>
        /// 安全等待點
        /// </summary>
        public int WaitingPosition { get; set; }

        /// <summary>
        /// 讀板號位置
        /// </summary>
        public int ReadBarCodePosition { get; set; }

        /// <summary>
        /// 推車有無板偵測點
        /// </summary>
        public int DetectCarrierBIBPosition { get; set; }

        /// <summary>
        /// 推車進板點(從推車拉板進來)
        /// </summary>
        public int PullInBIBPosition { get; set; }

        /// <summary>
        /// 推車退板點(退板回推車)
        /// </summary>
        public int PushOutBIBPosition { get; set; }

        /// <summary>
        /// Y 軸速度 - 高速(無勾板時)
        /// </summary>
        public int AxisYSpeedHigh { get; set; }

        /// <summary>
        /// Y 軸速度 - 低速(有勾板時)
        /// </summary>
        public int AxisYSpeedLow { get; set; }

        /// <summary>
        /// Barcode Reader ComPort
        /// </summary>
        public byte BarcodeReaderComPort { get; set; }

        /// <summary>
        /// Barcode Reader Type
        /// </summary>
        public byte BarcodeReaderType { get; set; }

        /// <summary>
        /// 設定是否讀取板號
        /// </summary>
        public bool ReadBID { get; set; }

        /// <summary>
        /// 板號
        /// </summary>
        public string BID { get;  private set; }

        /// <summary>
        /// 讀取板號模式 
        /// 0:拉板時讀取
        /// 1:推車讀取
        /// 2:供板機固定高度讀取
        /// </summary>
        public int ReadBIDMode { get; set; }

        /// <summary>
        /// 記錄板子來源樓層
        /// </summary>
        public int PullSlotNo { get; set; }

        #endregion 屬性

        #region 建構與解構

        public BemTransfer(
            AlarmCallback alarm,
            InBit ib_FrontTrackBIBDetector, InBit ib_BackTrackBIBDetector,
            InBit ib_HookBIBDetector, InBit ib_CarrierBIBDetector,
            OutBit ob_AphaStepMotorAlarmReset,
            OutBit ob_FrontHook, InBit ib_FrontHookOn, InBit ib_FrontHookOff,
            OutBit ob_BackHook, InBit ib_BackHookOn, InBit ib_BackHookOff,
            OutBit ob_Presure, InBit ib_PresureOn, InBit ib_PresureOff,
            OutBit ob_CarrierGate, InBit ib_CarrierGateUp, InBit ib_CarrierGateDown,
            Motor mo_AxisY,
            byte barcodeReaderType, byte barcodeReaderComPort)
            : base(alarm)
        {
            DI_FrontTrackBIBDetector = new DigitalInput(ib_FrontTrackBIBDetector);
            DI_BackTrackBIBDetector = new DigitalInput(ib_BackTrackBIBDetector);
            DI_HookBIBDetector = new DigitalInput(ib_HookBIBDetector);
            DI_CarrierBIBDetector = new DigitalInput(ib_CarrierBIBDetector);
            OB_AphaStepMotorAlarmReset = ob_AphaStepMotorAlarmReset;
            CY_FrontHook = new Cylinder(ob_FrontHook, ib_FrontHookOn, ib_FrontHookOff);
            CY_BackHook = new Cylinder(ob_BackHook, ib_BackHookOn, ib_BackHookOff);
            CY_Pressure = new Cylinder(ob_Presure, ib_PresureOn, ib_PresureOff);
            CY_CarrierGate = new Cylinder(ob_CarrierGate, ib_CarrierGateUp, ib_CarrierGateDown);
            MO_AxisY = mo_AxisY;

            BarcodeReaderType = barcodeReaderType;
            BarcodeReaderComPort = barcodeReaderComPort;
        }

        ~BemTransfer()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (DI_FrontTrackBIBDetector != null) DI_FrontTrackBIBDetector.Dispose();
            if (DI_BackTrackBIBDetector != null) DI_BackTrackBIBDetector.Dispose();
            if (DI_HookBIBDetector != null) DI_HookBIBDetector.Dispose();
            if (DI_CarrierBIBDetector != null) DI_CarrierBIBDetector.Dispose();
            if (OB_AphaStepMotorAlarmReset != null) OB_AphaStepMotorAlarmReset.Dispose();
            if (CY_BackHook != null) CY_BackHook.Dispose();
            if (CY_FrontHook != null) CY_FrontHook.Dispose();
            if (CY_Pressure != null) CY_Pressure.Dispose();
            if (CY_CarrierGate != null) CY_CarrierGate.Dispose();
            if (MO_AxisY != null) MO_AxisY.Dispose();
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
        /// 前勾板氣缸上升
        /// </summary>
        /// <returns></returns>
        private bool FrontHookUp()
        {
            bool bRet = false;
            if (CY_FrontHook != null)
            {
                ThreeValued tRet = CY_FrontHook.On(iHookUpDelayTime, 6000);
                if (tRet.Equals(ThreeValued.TRUE) || Simulation)
                {
                    //設為低速
                    MO_AxisY.SetSpeed(Math.Min(AxisYSpeedHigh, AxisYSpeedLow));
                    bRet = true;
                }
                else if (tRet.Equals(ThreeValued.FALSE))
                {
                    //Alarm : 前勾板氣缸上升動作逾時
                    ShowModuleAlarm((int)BEMAlarmCode.ArmBemTransferFrontHookUpTimeout);
                }
            }
            else
            {
                //Alarm : 前勾板氣缸元件未初始化
                ShowModuleAlarm((int)BEMAlarmCode.ArmBemTransferFrontHookNull);
            }
            return bRet;
        }

        /// <summary>
        /// 後勾板氣缸上升
        /// </summary>
        /// <returns></returns>
        private bool BackHookUp()
        {
            bool bRet = false;
            if (CY_BackHook != null)
            {
                ThreeValued tRet = CY_BackHook.On(iHookUpDelayTime, 6000);
                if (tRet.Equals(ThreeValued.TRUE) || Simulation)
                {
                    //設為低速
                    MO_AxisY.SetSpeed(Math.Min(AxisYSpeedHigh, AxisYSpeedLow));
                    bRet = true;
                }
                else if (tRet.Equals(ThreeValued.FALSE))
                {
                    //Alarm : 後勾板氣缸上升動作逾時
                    ShowModuleAlarm((int)BEMAlarmCode.ArmBemTransferBackHookUpTimeout);
                }
            }
            else
            {
                //Alarm : 後勾板氣缸元件未初始化
                ShowModuleAlarm((int)BEMAlarmCode.ArmBemTransferBackHookNull);
            }
            return bRet;
        }

        /// <summary>
        /// 前勾板氣缸下降
        /// </summary>
        /// <returns></returns>
        private bool FrontHookDown()
        {
            bool bRet = false;
            if (CY_FrontHook != null)
            {
                ThreeValued tRet = CY_FrontHook.Off(iHookDownDelayTime, 6000);
                if (tRet.Equals(ThreeValued.TRUE) || this.Simulation)
                {
                    bRet = true;
                }
                else if (tRet.Equals(ThreeValued.FALSE))
                {
                    //Alarm : 前勾板氣缸下降動作逾時
                    ShowModuleAlarm((int)BEMAlarmCode.ArmBemTransferFrontHookDownTimeout);
                }
            }
            else
            {
                //Alarm : 前勾板氣缸元件未初始化
                ShowModuleAlarm((int)BEMAlarmCode.ArmBemTransferFrontHookNull);
            }
            return bRet;
        }

        /// <summary>
        /// 後勾板氣缸下降
        /// </summary>
        /// <returns></returns>
        private bool BackHookDown()
        {
            bool bRet = false;
            if (CY_BackHook != null)
            {
                ThreeValued tRet = CY_BackHook.Off(iHookDownDelayTime, 6000);
                if (tRet.Equals(ThreeValued.TRUE) || this.Simulation)
                {
                    bRet = true;
                }
                else if (tRet.Equals(ThreeValued.FALSE))
                {
                    //Alarm : 後勾板氣缸下降動作逾時
                    ShowModuleAlarm((int)BEMAlarmCode.ArmBemTransferBackHookDownTimeout);
                }
            }
            else
            {
                //Alarm : 後勾板氣缸元件未初始化
                ShowModuleAlarm((int)BEMAlarmCode.ArmBemTransferBackHookNull);
            }
            return bRet;
        }

        /// <summary>
        /// 板固定氣缸上升
        /// </summary>
        /// <returns></returns>
        private bool PressureUp()
        {
            bool bRet = false;
            if (CY_Pressure != null)
            {
                ThreeValued tRet = CY_Pressure.Off(iPressureUpDelayTime, 6000);
                if (tRet.Equals(ThreeValued.TRUE) || this.Simulation)
                {
                    bRet = true;
                }
                else if (tRet.Equals(ThreeValued.FALSE))
                {
                    //Alarm : 前勾板氣缸下降動作逾時
                    ShowModuleAlarm((int)BEMAlarmCode.ArmBemTransferPressureUpTimeout);
                }
            }
            else
            {
                //部分供板機無Pressure氣缸，直接回傳true
                bRet = true;
            }
            return bRet;
        }

        /// <summary>
        /// 板下壓氣缸下降
        /// </summary>
        /// <returns></returns>
        private bool PressureDown()
        {
            bool bRet = false;
            if (CY_Pressure.Simulation.Equals(true))
            {
                return true;
            }
            if (CY_Pressure != null)
            {
                ThreeValued tRet = CY_Pressure.On(iPressureDownDelayTime, 6000);
                if (tRet.Equals(ThreeValued.TRUE) || this.Simulation)
                {
                    bRet = true;
                }
                else if (tRet.Equals(ThreeValued.FALSE))
                {
                    //Alarm : 後勾板氣缸下降動作逾時
                    ShowModuleAlarm((int)BEMAlarmCode.ArmBemTransferPressureDownTimeout);
                }
            }
            else
            {
                //部分供板機無Pressure氣缸，直接回傳true
                bRet = true;
            }
            return bRet;
        }

        private bool CarrierGateCtrl(bool val)
        {
            if (CY_CarrierGate.Equals(null))
            {
                return true;
            }

            ThreeValued crRet = ThreeValued.UNKNOWN;
            if (val)
            {
                crRet = CY_CarrierGate.On(80, 6000);
            }
            else
            {
                crRet = CY_CarrierGate.Off(80, 6000);
            }

            if (crRet.Equals(ThreeValued.TRUE) || DryRun || Simulation)
            {
                return true;
            }
            else if (crRet.Equals(ThreeValued.FALSE))
            {
                //Alarm : 台車閘門氣缸動作逾時
                if (val)
                {
                    ShowModuleAlarm((int)BEMAlarmCode.ArmBemCarrierOpenGateTimeout);
                }
                else
                {
                    ShowModuleAlarm((int)BEMAlarmCode.ArmBemCarrierCloseGateTimeout);
                }
            }
            return false;
        }

        /// <summary>
        /// 前後勾板氣缸下降
        /// </summary>
        /// <returns></returns>
        private bool AllHookDown()
        {
            bool bRet1 = FrontHookDown();
            bool bRet2 = BackHookDown();
            if (bRet1 && bRet2)
            {
                //設為高速
                MO_AxisY.SetSpeed(Math.Max(AxisYSpeedHigh, AxisYSpeedLow));
                return true;
            }
            return false;
        }

        private bool AxisYGoto(int position)
        {
            bool bRet = false;
            if (MO_AxisY != null)
            {
                if (MO_AxisY.G00(position))
                {
                    bRet = true;
                }
            }
            else
            {
                //Alarm : Y 軸馬達元件未初始化
                ShowModuleAlarm((int)BEMAlarmCode.ArmBemTransferAxisYNull);
            }
            return bRet;
        }

        /// <summary>
        /// 勾板前檢查勾爪上方有板或無板 (Sensor A接點)
        /// </summary>
        /// <param name="bState">true : 判斷有板，false : 判斷無板</param>
        /// <returns></returns>
        private bool CheckCarrierBoardExists(bool bState)
        {
            if (DryRun)
            {
                return true;
            }
            bool B1 = false;
            if (bState)
            {
                B1 = DI_HookBIBDetector.ValueOn;
            }
            else
            {
                B1 = DI_HookBIBDetector.ValueOff;
            }
            return B1;
        }

        #endregion 私有函式

        #region 公有函式

        /// <summary>
        /// 判斷平台內有板或無板 (Sensor B接點)
        /// </summary>
        /// <param name="bState">true : 判斷有板，false : 判斷無板</param>
        /// <returns></returns>
        public bool StageBoardExists(bool bState)
        {
            if (DryRun)
            {
                return true;
            }
            bool B1 = false;
            bool B2 = false;
            if (bState)
            {
                //判斷是否有板，若有板則兩顆Sensor都被遮斷
                B1 = DI_FrontTrackBIBDetector.ValueOff;
                B2 = DI_BackTrackBIBDetector.ValueOff;
            }
            else
            {
                //判斷是否無板，若無板則兩顆Sensor都無被遮斷
                B1 = DI_FrontTrackBIBDetector.ValueOn;
                B2 = DI_BackTrackBIBDetector.ValueOn;
            }
            return (B1 && B2);
        }

        /// <summary>
        /// 推車內有板或無板 (Sensor A接點)
        /// 為了因應空跑功能，所以必須在屬性註明是判斷有板或無板，因為DryRun時一律回傳true
        /// </summary>
        /// <param name="bState">true : 判斷有板，false : 判斷無板</param>
        /// <returns></returns>
        public bool CarrierBoardExists(bool bState)
        {
            if (DryRun || Simulation)
            {
                return true;
            }
            bool B1 = false;
            if (bState)
            {
                B1 = DI_CarrierBIBDetector.ValueOn;
            }
            else
            {
                B1 = DI_CarrierBIBDetector.ValueOff;
            }
            return B1;
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
        /// 讀取板號流程重置
        /// </summary>
        public void ReadBIDTaskReset()
        {
            BID = "";
            ReadBIDTask.Reset();
            BarcodeReader.ReadBarcodeTaskReset();
        }

        /// <summary>
        /// 讀取板號流程
        /// </summary>
        /// <returns>
        ///     <c>#BUSY#</c> : BUSY
        ///     <c>!ERROR!</c> : ERROR
        ///     <c>!TIMEOUT!</c> : TIMEOUT
        ///     <c>string</c> : barcode
        /// </returns>
        public bool ReadBIDProcess()
        {
            if (!ReadBID || BarcodeReaderType.Equals(0) || BarcodeReaderComPort.Equals(0))
            {
                BID = "";
                return true;
            }

            bool bRet = false;
            JActionTask Task = ReadBIDTask;
            switch (Task.Value)
            {
                case 0: //Open Com Port
                    {
                        if (BarcodeReader.Open(BarcodeReaderType, BarcodeReaderComPort))
                        {
                            BarcodeReader.ReadBarcodeTaskReset();
                            Task.Next();
                        }
                        else
                        {
                            //Alarm : 條碼槍通訊埠開啟失敗 (請確認埠號設定是否正確)
                            ShowModuleAlarm((int)BEMAlarmCode.ArmBemTransferBarcodeReaderComPortError);
                        }
                    }
                    break;

                case 1: //Barcode Reader - ReadBarcode()
                    {
                        string sRet = BarcodeReader.ReadBarcode();
                        if (sRet != "#BUSY#")
                        {
                            BID = sRet;
                            Task.Next();
                        }
                    }
                    break;

                case 2: //Close Com Port
                    {
                        BarcodeReader.Close();
                        Task.Next(99);
                    }
                    break;

                case 99:    //Done
                    {
                        bRet = true;
                    }
                    break;
            }
            return bRet;
        }

        /// <summary>
        /// 動作流程重置
        /// </summary>
        public void TaskReset()// 重置 Task
        {
            AutoTask.Reset();   //Reset to zero.
        }

        /// <summary>
        /// 移動到讀取BarCode點 (呼叫此函式前，記得先呼叫 TaskReset() 函式重置 Task)
        /// </summary>
        /// <returns></returns>
        public bool GotoDetectCarrierDiePakBarCodePosition()
        {
            bool bRet = false;
            JActionTask Task = AutoTask;
            switch (Task.Value)
            {
                case 0:
                    {   //DiePak下壓氣缸下降                            
                        if (PressureDown())
                        {
                            Task.Next();
                        }
                    } break;
                case 1:
                    {	//後勾板氣缸上升
                        if (BackHookUp())
                        {
                            Task.Next();
                        }
                    } break;
                case 2:
                    {   //DiePak下壓氣缸上升                            
                        if (PressureUp())
                        {
                            Task.Next();
                        }
                    } break;
                case 3:
                    {   //Y 軸移至讀取 BarCode點位
                        if (AxisYGoto(this.ReadBarCodePosition))
                        {
                            Task.Next();
                        }
                    } break;
                case 4:
                    {   //DiePak下壓氣缸下降                            
                        if (PressureDown())
                        {
                            Task.Next();
                        }
                    } break;
                case 5: //完成
                    {
                        bRet = true;
                    }
                    break;
            }

            return bRet;
        }

        /// <summary>
        /// 移動到讀取BarCode後回安全點點 (呼叫此函式前，記得先呼叫 TaskReset() 函式重置 Task)
        /// </summary>
        /// <returns></returns>
        public bool GotoDetectCarrierDiePakBarCodePositionSafe()
        {
            bool bRet = false;
            JActionTask Task = AutoTask;
            switch (Task.Value)
            {
                case 0:
                    {   //DiePak下壓氣缸上升                            
                        if (PressureUp())
                        {
                            Task.Next();
                        }
                    } break;
                case 1:
                    {   //Y 軸移至安全等待點
                        if (AxisYGoto(this.WaitingPosition))
                        {
                            Task.Next();
                        }
                    } break;
                case 2:
                    {   //DiePak下壓氣缸下降                            
                        if (PressureDown())
                        {
                            Task.Next();
                        }
                    } break;
                case 3:
                    {	//前後勾板氣缸下降
                        if (AllHookDown())
                        {
                            Task.Next();
                        }
                    } break;
                case 4: //完成
                    {
                        bRet = true;
                    }
                    break;
            }
            
            return bRet;
        }

        /// <summary>
        /// 移動至推車有無板偵測點 (呼叫此函式前，記得先呼叫 TaskReset() 函式重置 Task)
        /// </summary>
        /// <returns></returns>
        public bool GotoDetectCarrierBoardPosition(bool bState, out bool bResult)
        {
            bResult = false;
            bool bRet = false;
            JActionTask Task = AutoTask;
            switch (Task.Value)
            {
                case 0: //如有板下壓氣缸，先做下壓在做偵測(退板偵測時有板)
                    {
                        if (PressureDown())
                        {
                            Task.Next();
                        }
                    }
                    break;
                case 1: //前/後勾板氣缸下降
                    {
                        if (AllHookDown())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 2: //Y 軸移至推車有無板偵測點
                    {
                        if (AxisYGoto(this.DetectCarrierBIBPosition))
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 3: //判斷有無板
                    {
                        bResult = CarrierBoardExists(bState);
                        bRet = true;
                    }
                    break;

                //case 4: //Y 軸移至安全位置
                //    {
                //        if (AxisYGoto(this.WaitingPosition))
                //        {
                //            Task.Next();
                //        }
                //    }
                //    break;

                //case 5: //完成
                //    {
                        
                //        bRet = true;
                //    }
                //    break;
            }
            return bRet;
        }

        /// <summary>
        /// 從推車進板流程 (呼叫此函式前，記得先呼叫 TaskReset() 函式重置 Task)
        /// </summary>
        /// <returns></returns>
        public bool PullInBoardFromCarrier()
        {
            bool bRet = false;
            JActionTask Task = AutoTask;
            switch (Task.Value)
            {
                case 0: //前後勾板氣缸下降
                    {
                        if (AllHookDown())
                        {
                            iYPosOffset = 0;
                            Task.Next();
                        }
                    }
                    break;
                case 1:
                    {   //DiePak下壓氣缸上升                            
                        if (PressureUp())
                        {
                            iYPosOffset = 0;
                            Task.Next();
                        }
                    } break;
                case 2: //Y 軸移動至推車進板點
                    {
                        int pos = PullInBIBPosition + iYPosOffset;
                        if (AxisYGoto(pos))
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 3: //判斷有無板，無板則回 case 3 (Offset + 1000)
                    {
                        if (CheckCarrierBoardExists(true))
                        {
                            Task.Next();
                        }
                        else
                        {
                            if (iYPosOffset < 3000)
                            {
                                iYPosOffset += 1000;
                                Task.Next(1);
                            }
                            else
                            {
                                //推車無板，勾板失敗
                                Task.Next(14);   //Y 軸移至安全等待點
                            }
                        }
                    }
                    break;

                case 4: //前勾板氣缸上升
                    {
                        if (FrontHookUp())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 5 :    //推車閘門向上開啟
                    {
                        if (CarrierGateCtrl(true))
                        {
                            Task.Next();
                        }
                    }
                    break;
                case 6: //Y 軸移至主機端送板點 (暫時借用此點位，因為此點位最靠近極限)
                    {
                        if (AxisYGoto(SendBIBPosition))
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 7: //Diepak 下壓氣缸下壓
                    {
                        if (PressureDown())
                        {
                            Task.Next();
                        }
                    }
                    break;
                case 8: //前後勾板氣缸下降
                    {
                        if (AllHookDown())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 9: //Y 軸前進一個勾爪距離(即主機端送板點-勾爪距離-1000)
                    {
                        //多退 1000 避免勾爪與板框摩擦
                        int pos = SendBIBPosition - DistanceOfHook - 1000;
                        if (AxisYGoto(pos))
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 10: //後勾板氣缸上升
                    {
                        if (BackHookUp())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 11: //DiePak 下壓氣上升
                    {
                        if (PressureUp())
                        {
                            Task.Next();
                        }
                    }
                    break;
                case 12: //移動至讀取板號位置
                    {
                        if (ReadBIDMode.Equals(0) && !ReadBID)
                        {
                            if (AxisYGoto(ReadBarCodePosition))
                            {
                                ReadBIDTaskReset();
                                Task.Next();
                            }
                        }
                        else
                        {
                            Task.Next(14);
                        }
                        //if (!ReadBID || AxisYGoto(ReadBarCodePosition))
                        //{
                        //    ReadBIDTaskReset();
                        //    Task.Next();
                        //}
                    }
                    break;

                case 13: //讀取板號
                    {
                        if (ReadBIDProcess())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 14: //Y 軸移至安全等待點
                    {
                        if (AxisYGoto(WaitingPosition))
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 15: //推車閘門向下關閉
                    {
                        if (CarrierGateCtrl(false))
                        {
                            Task.Next();
                        }
                    }
                    break;
                case 16:    //DiePak 下壓氣缸下壓
                    {
                        if (PressureDown())
                        {
                            Task.Next();
                        }
                    }
                    break;
                case 17: //前後勾板氣缸下降
                    {
                        Task.Next();    //後續動作仍需後勾爪上升，此處不做下降
                        //if (AllHookDown())
                        //{
                        //    Task.Next();
                        //}
                    }
                    break;

                case 18: //判斷平台內有無板 (B接點)
                    {
                        if (StageBoardExists(true))
                        {
                            Task.Next(99);
                        }
                        else
                        {
                            //Alarm : 從推車進板流程偵測到平台內板異常，請確認是否卡板
                            ShowModuleAlarm((int)BEMAlarmCode.ArmBemTransferTrackBIBDetectErrorCT);
                        }
                    }
                    break;

                case 99:    //完成
                    {
                        bRet = true;
                    }
                    break;
            }
            return bRet;
        }

        /// <summary>
        /// 退板至推車流程 (呼叫此函式前，記得先呼叫 TaskReset() 函式重置 Task)
        /// </summary>
        /// <returns></returns>
        public bool PushOutBoardToCarrier()
        {
            bool bRet = false;
            JActionTask Task = AutoTask;
            switch (Task.Value)
            {
                case 0: //DiePak 下壓氣缸下壓
                    {
                        if (PressureDown())
                        {
                            Task.Next();
                        }
                    }
                    break;
                case 1: //前後勾板氣缸下降
                    {
                        if (AllHookDown())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 2: //Y 軸移至安全等待點
                    {
                        int pos = (WaitingPosition - 1000);
                        if (AxisYGoto(pos))
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 3: //後勾板氣缸上升
                    {
                        if (BackHookUp())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 4: //DiePak 下壓氣缸下壓
                    {
                        if (PressureUp())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 5: //推車閘門氣缸向上開啟
                    {
                        if (CarrierGateCtrl(true))
                        {
                            Task.Next();
                        }
                    }
                    break;
                case 6: //Y 軸移動至推車退板點
                    {
                        if (AxisYGoto(PushOutBIBPosition))
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 7: //DiePak下壓氣缸下壓
                    {
                        if (PressureDown())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 8: //前後勾板氣缸下降
                    {
                        if (AllHookDown())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 9: //Y 軸移動至推車退板點 + DistanceOfHook
                    {
                        int pos = (PushOutBIBPosition + DistanceOfHook - 1000);
                        if (AxisYGoto(pos))
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 10: //前勾板氣缸上升
                    {
                        if (FrontHookUp())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 11: //DiePak下壓氣缸上升
                    {
                        if (PressureUp())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 12: //Y 軸移動至推車退板點
                    {
                        if (AxisYGoto(PushOutBIBPosition))
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 13: //推車閘門向下關閉
                    {
                        if (CarrierGateCtrl(false))
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 14: //前後勾板氣缸下降
                    {
                        if (AllHookDown())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 15: //Y 軸移至安全等待點
                    {
                        if (AxisYGoto(WaitingPosition))
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 16:    //判斷平台內有無板
                    {
                        if (StageBoardExists(false))
                        {
                            //bBoardExists = false;
                            Task.Next(99);
                        }
                        else
                        {
                            //Alarm : 退板至推車流程偵測到平台內板異常，請確認是否卡板
                            ShowModuleAlarm((int)BEMAlarmCode.ArmBemTransferTrackBIBDetectErrorTC);
                        }
                    }
                    break;

                case 99:    //完成
                    {
                        bRet = true;
                    }
                    break;
            }
            return bRet;
        }

        /// <summary>
        /// 送板至主機流程 (呼叫此函式前，記得先呼叫 TaskReset() 函式重置 Task)
        /// </summary>
        /// <returns></returns>
        public bool SendBoardToBoardStage()
        {
            bool bRet = false;
            JActionTask Task = AutoTask;
            switch (Task.Value)
            {
                case 0: //DiePak下壓氣缸下降
                    {
                        if (PressureDown())
                        {
                            Task.Next();
                        }
                    }
                    break;
                case 1: //前後勾板氣缸下降
                    {
                        if (AllHookDown())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 2: //Y 軸移至安全等待點
                    {
                        if (AxisYGoto(WaitingPosition))
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 3: //後勾板氣缸上升
                    {
                        if (BackHookUp())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 4: //板下壓氣缸上升
                    {
                        if (PressureUp())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 5: //Y 軸移至主機端送板點
                    {
                        if (AxisYGoto(SendBIBPosition))
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 6: //DiePak下壓氣缸下降
                    {
                        if (PressureDown())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 7: //前後勾板氣缸下降
                    {
                        if (AllHookDown())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 8: //Y 軸移至安全等待點
                    {
                        if (AxisYGoto(WaitingPosition))
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 9:
                    {		//判斷平台內有無板
                        bool B1 = DI_FrontTrackBIBDetector.ValueOn;	    //無遮斷 靠近手推台車
                        bool B2 = DI_BackTrackBIBDetector.ValueOff;		//遮斷 靠近主機
                        if (DryRun || (B1 && B2))
                        {
                            //bBoardExists = false;
                            Task.Next(99);
                        }
                        else
                        {
                            //Alarm : 送板至主機流程偵測到平台內板異常，請確認是否卡板
                            ShowModuleAlarm((int)BEMAlarmCode.ArmBemTransferTrackBIBDetectErrorTS);
                        }
                    }
                    break;

                case 99:    //完成
                    {
                        bRet = true;
                    }
                    break;
            }
            return bRet;
        }

        public bool AfterSendBoardToBoardStage()
        {
            bool bRet = false;
            JActionTask Task = AutoTask;
            switch (Task.Value)
            {
                case 0: //DiePak下壓氣缸上升
                    {
                        if (PressureUp())
                        {
                            Task.Next();
                        }                       
                    }
                    break;
                case 1:
                    {
                        bRet =true;
                    }
                    break;
            }
            return bRet;
        }

        public bool BeforeReceiveBoardFromBoardStage()
        {
            bool bRet = false;
            JActionTask Task = AutoTask;
            switch(Task.Value)
            {
                case 0: //前後勾板氣缸下降
                    {
                        if (AllHookDown())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 1: //Y軸移至安全等待點
                    {
                        if (AxisYGoto(WaitingPosition))
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 2: //DiePak下壓氣缸下降
                    {
                        if (PressureDown())
                        {
                            Task.Next(99);
                        }
                    }
                    break;
                case 99:
                    {
                        bRet = true;
                    }
                    break;
            }
            return bRet;
        }

        /// <summary>
        /// 從主機收板流程 (呼叫此函式前，記得先呼叫 TaskReset() 函式重置 Task)
        /// </summary>
        /// <returns></returns>
        public bool ReceiveBoardFromBoardStage()
        {
            bool bRet = false;
            JActionTask Task = AutoTask;
            switch (Task.Value)
            {
                case 0: //前後勾板氣缸下降
                    {
                        if (AllHookDown())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 1: //Y 軸移至主機端收板點
                    {
                        if (AxisYGoto(ReceiveBIBPosition))
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 2: //後勾板氣缸上升
                    {
                        if (BackHookUp())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 3: //DiePak 下壓氣缸上升
                    {
                        if (PressureUp())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 4: //Y 軸移至安全等待點
                    {
                        if (AxisYGoto(WaitingPosition))
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 5: //DiePak下壓氣缸下降
                    {
                        if (PressureDown())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 6:
                    {	//前後勾板氣缸下降
                        if (AllHookDown())
                        {
                            Task.Next();
                        }
                    }
                    break;

                case 7: //判斷平台內有無板
                    {
                        if (StageBoardExists(true))
                        {
                            //bBoardExists = true;    //2016/07/11 Jay Tsao
                            Task.Next(99);
                        }
                        else
                        {
                            //Alarm : 從主機收板流程偵測到平台內板異常，請確認是否卡板
                            ShowModuleAlarm((int)BEMAlarmCode.ArmBemTransferTrackBIBDetectErrorST);
                        }
                    } break;
                case 99: //完成
                    {
                        bRet = true;
                    } break;
            }
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
            MO_AxisY.HomeReset();
        }

        /// <summary>
        /// 歸零 (呼叫此函式前，記得先呼叫 HomeReset() 函式)
        /// </summary>
        /// <returns></returns>
        public bool Home()
        {
            bool bRet = false;
            if (MO_AxisY != null)
            {
                JActionTask Task = HomeTask;
                switch (Task.Value)
                {
                    case 0:
                        {
                            Task.Next();
                        }
                        break;

                    case 1: //前後勾板氣缸下降
                        {
                            bool b1 = AllHookDown();
                            bool b2 = PressureDown();
                            bool b3 = CarrierGateCtrl(false);
                            //if (AllHookDown())
                            if (b1 && b2 && b3)
                            {
                                MO_AxisY.HomeReset();
                                Task.Next();
                            }
                        }
                        break;

                    case 2: //Y 軸歸零
                        {
                            if (MO_AxisY.Home())
                            {
                                Task.Next();
                            }
                        }
                        break;

                    case 3: //Y 軸移至安全等待點
                        {
                            if (AxisYGoto(WaitingPosition))
                            {
                                Task.Next();
                            }
                        }
                        break;

                    case 4:
                        {   //要確認下當有無板子此2個SENSOR偵測狀態    B偵測
                            if (StageBoardExists(false))
                            {
                                //bBoardExists = false;   //2016/07/11 Jay Tsao
                                Task.Next(99);
                            }
                            else
                            {
                                //Alarm : 歸零流程偵測到平台內有板，請手動將板移除
                                ShowModuleAlarm((int)BEMAlarmCode.ArmBemTransferBIBRemainInHoming);
                            }
                        }
                        break;

                    case 99: //完成
                        {
                            bRet = true;
                        }
                        break;
                }
            }
            else
            {
                //Alarm : Y 軸馬達元件未初始化
                ShowModuleAlarm((int)BEMAlarmCode.ArmBemTransferAxisYNull);
            }
            return bRet;
        }

        #endregion 歸零流程
    }
}