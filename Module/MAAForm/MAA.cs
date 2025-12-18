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
using ProVTool;
using KCSDK;

namespace MAA
{
    public partial class MAA : BaseModuleInterface
    {
        bool bUseSafeDoor = true;

        public MAA()
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

        public override void Initial()
        {
            base.Initial();
            bUseSafeDoor = SetupReadValue("安全門機制設定").ToInt() != 0;
        }

        //持續偵測函數
        //持續掃描
        public override void AlwaysRun()
        {
            if (IsSimulation() != 0)
                return;
            
            //B接
            //檢查緊停
            ChackEMG();

            //B接
            //空壓偵測
            if (AirSensor.Off(20) == true)
                ShowAlarm("E", 2);
        
            //B接
            //檢查安全門
            ChackSafeDoor();

            //檢查光閘
            CheckGate();

            //檢查料盒
            CheckBinBox();

            ChackIonBar();
        }

        //掃描硬體按鈕IO
        //bit0=StartButton / Bit1=StopButton / Bit2=AlarmResetButton 
        bool bArmRstIsSet = false;
        bool bStartIsSet = false;
        bool bSuppDevice = false;
        bool bSuppIsSet = false;
        public override int ScanIO()
        {
            OB_StartButton.Value = SysRun;
            OB_StopButton.Value = !SysRun;
            OB_AlarmResetButton.Value = IB_AlarmResetButton.Value;

            byte result = 0;
            //if (IB_SuppDevice.Value)
            //{
            //    if (bSuppDevice)
            //        result |= 0x04;

            //    bSuppDevice = !bSuppDevice;
            //    if (bSuppDevice)
            //    {
            //        ShowAlarm("I", 4);
            //    }
            //}

            if (IB_SuppDevice.Value)
                bSuppIsSet = true;
            if (!IB_SuppDevice.Value && bSuppIsSet)
            {
                bSuppIsSet = false;

                if (bSuppDevice)
                    result |= 0x04;

                bSuppDevice = !bSuppDevice;

                if (bSuppDevice)
                {
                    ShowAlarm("I", 4);
                }
            }

            if (IB_StartButton.Value)
                bStartIsSet = true;
            if (!IB_StartButton.Value && bStartIsSet) //放開時再設定Start Bit
            {
                bStartIsSet = false;
                result |= 0x01;
            }

            if (IB_StopButton.Value)
                result |= 0x02;

            if (IB_AlarmResetButton.Value)
                bArmRstIsSet = true;
            if (!IB_AlarmResetButton.Value && bArmRstIsSet) //放開時再設定Alarm Reset Bit
            {
                bArmRstIsSet = false;
                result |= 0x04;
            }
            return result;
        }

        //歸零初始
        public override void HomeReset()
        {
            mLotendOk = true;
            mHomeOk = true;
        }

        //歸零
        public override bool Home() 
        {
            return mHomeOk; 
        } 

        //手動相關函數
        //手動運行前置設定
        public override void ManualReset() 
        {
        }

        //手動模式運行
        public override void ManualRun() 
        {
        }
      
        //停止所有馬達
        public override void StopMotor()
        {
            base.StopMotor();
        }

        public override void InitialSubPackage(Form EditForm)
        {
            #region 使用者修改 (關聯式資料庫，將相關表格加入這個主表格內)
            //tFieldCB1.EditForm = EditForm;
            //EditForm.Tag = this;
            #endregion
        }

        #endregion

        #region 私有函數
        //離子棒檢查
        private bool ChackIonBar()
        {
            //  if (IsSimulation() != 0)
            //  {
            //      return true;
            //   }
            if (ob_IonizerBarCtl.Value != IsAutoMode)
                ob_IonizerBarCtl.Value = IsAutoMode;

            bool UseIonBar = SReadValue("UseIonBarDetected").ToBoolean();
            if (!UseIonBar)
            {
                return true;
            }
            if (!IsAutoMode)
            {
                return true;
            }
            InBit[] IonBar = { IB_IonizerFan1, IB_IonizerFan2 };
            bool ret = true;
            for (int i = 0; i < IonBar.Count(); i++)
            {
                InBit obj = IonBar[i];
                if (obj.Off(2000))
                {
                    ret = false;
                    ShowAlarm("E", 4, i+1);
                }
            }
            return ret;
        }

        private bool CheckBinBox()
        {
            //if (!SysRun)
            if (!IsAutoMode)
                return true;

            bool err = true;
            if (IB_PassBox.On())
            {
                err = false;
                ShowAlarm("E", 5);
            }
            if (IB_FailBox.On())
            {
                err = false;
                ShowAlarm("E", 6);
            }
            return err;
        }

        private bool CheckGate()
        {
            if (!bUseSafeDoor)
                return true;

            if (!IsAutoMode)
                return true;

            if (bSuppDevice)
                return true;

            bool err = true;
            if (IB_Gate.Off(100))
            {
                err = false;
                ShowAlarm("E", 4);
            }
            return err;
        }

        //安全門機制改善
        //宣告一個委派與紀錄委派的公有函式以便發生安全門或警停發生時可透過此委派通知主程式
        Action<int> SaftyCheckDelegate;
        public void RecordSaftyCheckDelegate(Action<int> pSaftyCheck)
        {
            SaftyCheckDelegate = pSaftyCheck;
        }

        //安全門檢查
        private bool ChackSafeDoor()
        {
            if (!bUseSafeDoor)
                return true;

            InBit[] SafeDoor = { SafeDoor1, SafeDoor2, SafeDoor3, SafeDoor4/*, SafeDoor5, SafeDoor6, SafeDoor7, SafeDoor8 */};
            bool err = false;
            for (int i = 0; i < SafeDoor.Count(); i++)
            {
                InBit obj = SafeDoor[i];
                if (obj.Off(10))
                {
                    err = true;
                    ShowAlarm("E", 0, i+1);
                    if (SaftyCheckDelegate != null) //透過委派通知主程式偵測到安全門被打開了
                        SaftyCheckDelegate(1); //Safe Door
                }
                else
                {
                    if (SaftyCheckDelegate != null) //透過委派通知主程式偵測到安全門關閉了
                        SaftyCheckDelegate(0); //Relieve
                }
            }
            return err;
        }

        //緊停按鈕檢查
        private bool ChackEMG()
        {
            InBit[] EMG = { EMGA1, EMGA2, EMGA3, EMGA4 };
            bool err = false;
            for (int i = 0; i < EMG.Count(); i++)
            {
                InBit obj = EMG[i];
                if (obj.Off(10))
                {
                    err = true;
                    ShowAlarm("E", 1, i+1);
                    if (SaftyCheckDelegate != null) //透過委派通知主程式偵測到緊急停止開關被觸發了
                        SaftyCheckDelegate(2); //EMG
                }
                else
                {
                    if (SaftyCheckDelegate != null) //透過委派通知主程式偵測到緊急停止開關被解除了
                        SaftyCheckDelegate(0); //Safe Door
                }
            }
            return err;
        }

        #endregion

        #region 公有函數
        public bool SuppleDeviceSensor()
        {
            return bSuppDevice;
        }

        public void DataReset()
        {
            return;
        }

        //取得紅燈訊號
        public bool ScanRedLight()
        {
            return RedLight.Value;
        }

        //取得黃燈訊號
        public bool ScanYellowLight()
        {
            return YellowLight.Value;
        }

        //取得綠燈訊號
        public bool ScanGreenLight()
        {
            return GreenLight.Value;
        }

        //取得藍燈訊號
        public bool ScanBlueLight()
        {
            return BlueLight.Value;
        }

        //回傳燈號IO
        public List<object> GetLightIO()
        {
            List<object> lightio = new List<object>();
            lightio.Add(GreenLight);
            lightio.Add(YellowLight);
            lightio.Add(RedLight);
            lightio.Add(Music1);
            lightio.Add(Music2);
            lightio.Add(Music3);
            lightio.Add(Music4);
            lightio.Add(BlueLight);
            return lightio;
        }

        #endregion
    }
}

