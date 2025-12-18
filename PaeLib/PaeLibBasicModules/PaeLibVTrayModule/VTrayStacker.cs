using PaeLibGeneral;
using PaeLibProVSDKEx;

namespace PaeLibVTrayModule
{
    /// <summary>
    /// 直盤堆疊區抽象類別
    /// </summary>
    public abstract class VTrayStacker : JModuleBase
    {
        #region 參數設定

        protected object Var_Locker = null;

        #endregion 參數設定

        #region 元件

        //Input
        protected DigitalInput DI_TrayFull = null;               //入料區滿盤偵測 (A接點)

        protected DigitalInput DI_TrayExist = null;              //入料區有無盤偵測 (A接點)
        protected DigitalInput DI_TrayOnTrack = null;            //入料區軌道有無盤偵測 (B接點)
        protected DigitalInput DI_TrayInPosition1 = null;         //入料區軌道盤定位偵測1 (B接點)
        protected DigitalInput DI_TrayInPosition2 = null;         //入料區軌道盤定位偵測2 (B接點)

        #endregion 元件

        public VTrayStacker(AlarmCallback alarm)
            : base(alarm)
        {
            this.CheckInitialTrayExist = false;
        }

        /// <summary>
        /// 歸零時是否檢查盤堆疊區有無盤(預設值：不檢查)
        /// </summary>
        public bool CheckInitialTrayExist { get; set; }

        protected override void SimulationSetting(bool simulation)
        {
            //實作父類別抽象函式內容
            DI_TrayFull.Simulation = simulation;
            DI_TrayExist.Simulation = simulation;
            DI_TrayOnTrack.Simulation = simulation;
            DI_TrayInPosition1.Simulation = simulation;
            DI_TrayInPosition2.Simulation = simulation;
            this.SimulationSetting(simulation);
        }

        protected virtual void SimulationSetting2(bool simulation)
        {
            //由子類別實作設定 simulation 的內容
        }

        /// <summary>
        /// 鎖定 (但會先判斷是否無人鎖定，且須紀錄鎖定者)
        /// </summary>
        /// <param name="locker">The locker.</param>
        /// <returns></returns>
        public bool Lock(object locker)
        {
            //判斷是否已有被 locker 以外鎖定
            if (Var_Locker != null)
            {
                //判斷是否已被 locker 鎖定
                if (Var_Locker.Equals(locker))
                {
                    return true;
                }
                //已被鎖定，所以鎖定失敗，須等解鎖後，才可再被鎖定
                return false;
            }
            //鎖定成功，紀錄鎖定者
            Var_Locker = locker;
            return true;
        }

        /// <summary>
        /// 解除鎖定 (但解鈴還需繫鈴人，所以會檢查解鎖者的身分是否與當初鎖定者相同)
        /// </summary>
        /// <param name="unlocker">The unlocker.</param>
        /// <returns></returns>
        public bool Unlock(object unlocker)
        {
            //判斷解鎖者是否為鎖定者
            if (Var_Locker != null)
            {
                if (Var_Locker.Equals(unlocker))
                {
                    //是，解鎖成功
                    Var_Locker = null;
                    return true;
                }
            }
            //否，解鎖失敗
            return false;
        }

        /// <summary>
        /// 判斷入料區是否有盤
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [有盤]; otherwise, <c>false</c>.
        /// </returns>
        public bool TrayExist()
        {
            if (this.DryRun)
            {
                return true;    //空跑模式時，不做實際偵測，回傳有盤
            }
            if (DI_TrayExist != null)
            {
                //Sensor 為 A 接點(反射式)，若有 Tray 時 Sensor 會被觸發(遮斷)，訊號為 ON
                return DI_TrayExist.ValueOn;
            }
            return true;    //不偵測(預設值：有盤)
        }

        /// <summary>
        /// 判斷入料區是否無盤
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [有盤]; otherwise, <c>false</c>.
        /// </returns>
        public bool TrayNonExist()
        {
            if (DI_TrayExist != null)
            {
                //Sensor 為 A 接點(反射式)，若無 Tray 時 Sensor 不會被觸發(遮斷)，訊號為 OFF
                return DI_TrayExist.ValueOff;
            }
            return true;    //不偵測(預設值：無盤)
        }

        /// <summary>
        /// 判斷盤堆疊區是否滿盤
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [滿盤]; otherwise, <c>false</c>.
        /// </returns>
        protected bool IsFullTray()
        {
            if (this.Simulation)
            {
                return false;   //模擬模式時，不做實際偵測，回傳不滿盤
            }
            if (DI_TrayFull != null)
            {
                //Sensor 為 A 接點(反射式)，若有 Tray 時 Sensor 會被觸發(遮斷)，訊號為 ON
                return DI_TrayFull.ValueOn;
            }
            return false;   //不偵測(預設回傳值：不滿盤)
        }

        /// <summary>
        /// 判斷軌道是否有盤
        /// </summary>
        /// <returns></returns>
        protected bool TrayOnTrack()
        {
            if (this.DryRun)
            {
                return true;   //空跑模式時，不做實際偵測，回傳軌道上有盤
            }
            if (DI_TrayOnTrack != null)
            {
                //Sensor 為 B 接點(對照式)，若有 Tray 時 Sensor 會被觸發(遮斷)，訊號為 OFF
                return DI_TrayOnTrack.ValueOff;
            }
            return true;    //不偵測(預設回傳值：軌道有盤)
        }

        /// <summary>
        /// 判斷軌道是否無盤
        /// </summary>
        /// <param name="defVal">if set to <c>true</c> [回傳預設值為軌道上有盤].</param>
        /// <returns></returns>
        protected bool NothingOnTrack()
        {
            if (this.DryRun)
            {
                return true;   //空跑模式時，不做實際偵測，回傳軌道上無盤
            }
            if (DI_TrayOnTrack != null)
            {
                //Sensor 為 B 接點(對照式)，若無 Tray 時 Sensor 不會被觸發(遮斷)，訊號為 ON
                return DI_TrayOnTrack.ValueOn;
            }
            return true;    //不偵測(預設回傳值：軌道無盤)
        }

        /// <summary>
        /// 判斷軌道盤位置是否良好
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [位置良好]; otherwise, <c>false</c>.
        ///   </returns>
        protected bool TrayInPosition()
        {
            //Sensor 為 B 接點(對照式)，當 Tray 位置良好時，Sensor 不會被觸發(遮斷)，訊號為 ON，所以不用判斷是否為 DryRun
            bool b1 = (DI_TrayInPosition1 != null ? DI_TrayInPosition1.ValueOn : true);
            bool b2 = (DI_TrayInPosition2 != null ? DI_TrayInPosition2.ValueOn : true);
            return (b1 && b2);
        }
    }
}