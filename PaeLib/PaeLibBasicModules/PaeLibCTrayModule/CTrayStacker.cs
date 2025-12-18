using PaeLibGeneral;
using PaeLibProVSDKEx;
using System.Collections.Generic;

namespace PaeLibCTrayModule
{
    /// <summary>
    /// 十字台車盤堆疊區抽象類別
    /// </summary>
    public abstract class CTrayStacker : JModuleBase
    {
        //private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        #region 參數設定

        //private bool Para_DryRun = false;//空跑
        //private bool Para_Simulation = false;//模擬跑

        #endregion 參數設定

        #region 變數設定

        private string HeirName = "";                   //繼承者名稱
        protected object Var_Locker = null;            //鎖定者(only one, Var_User 與 locker 相同才可鎖定)
        protected object Var_User = null;              //使用者(only one)
        protected List<object> Var_InsideList = null;   //路過者清單

        #endregion 變數設定

        #region 元件

        protected DigitalInput DI_StackerTrayFullV = null;                 //盤堆疊區滿盤偵測-直盤 (A接點)
        protected DigitalInput DI_StackerTrayFullH = null;                 //盤堆疊區滿盤偵測-橫盤 (A接點)
        protected DigitalInput DI_StackerTrayExistV = null;                //盤堆疊區有無盤偵測-直盤 (A接點)
        protected DigitalInput DI_StackerTrayExistH = null;                //盤堆疊區有無盤偵測-橫盤 (A接點)
        protected DigitalInput DI_StackerTrayInPositionX1 = null;          //盤堆疊區盤定位偵測(盤分離下方)-X1 (B接點) //2020-12-04 v1.3 Jay Tsao Added.
        protected DigitalInput DI_StackerTrayInPositionX2 = null;          //盤堆疊區盤定位偵測(盤分離下方)-X2 (B接點) //2020-12-04 v1.3 Jay Tsao Added.
        protected DigitalInput DI_TopShuttleTrayExist = null;              //上台車有無盤偵測 (B接點)
        protected DigitalInput DI_TopShuttleTrayInPositionX1 = null;       //上台車盤定位偵測-X1 (B接點) //2020-12-04 v1.3 Jay Tsao Modified Name from V to X1.
        protected DigitalInput DI_TopShuttleTrayInPositionX2 = null;       //上台車盤定位偵測-X2 (B接點) //2020-12-04 v1.3 Jay Tsao Modified Name from H to X2.
        protected DigitalInput DI_BottomShuttleTrayExist = null;           //下台車有無盤偵測 (B接點)
        protected DigitalInput DI_BottomShuttleTrayInPositionX1 = null;    //下台車盤定位偵測-X1 (B接點) //2020-12-04 v1.3 Jay Tsao Modified Name from V to X1.
        protected DigitalInput DI_BottomShuttleTrayInPositionX2 = null;    //下台車盤定位偵測-X2 (B接點) //2020-12-04 v1.3 Jay Tsao Modified Name from V to X1.

        #endregion 元件

        public CTrayStacker(string name, AlarmCallback alarm)
            : base(alarm)
        {
            HeirName = name;
            Var_InsideList = new List<object>();
        }

        ~CTrayStacker()
        {
            if (Var_InsideList != null)
            {
                Var_InsideList.Clear();
                Var_InsideList = null;
            }
        }

        ///// <summary>
        ///// 是否使用空跑功能
        ///// </summary>
        ///// <value>
        /////   <c>true</c> 使用空跑功能; otherwise, <c>false</c>.
        ///// </value>
        //protected bool DryRun
        //{
        //    get { return Para_DryRun; }
        //    set { Para_DryRun = value; }
        //}

        ///// <summary>
        ///// 是否使用模擬功能
        ///// </summary>
        ///// <value>
        /////   <c>true</c> 使用模擬功能; otherwise, <c>false</c>.
        ///// </value>
        //protected bool Simulation
        //{
        //    get { return Para_Simulation; }
        //    set
        //    {
        //        Para_Simulation = value;

        //        DI_StackerTrayFullV.Simulation = Para_Simulation;
        //        DI_StackerTrayFullH.Simulation = Para_Simulation;
        //        DI_StackerTrayExistV.Simulation = Para_Simulation;
        //        DI_StackerTrayExistH.Simulation = Para_Simulation;
        //        DI_TopShuttleTrayExist.Simulation = Para_Simulation;
        //        DI_BottomShuttleTrayExist.Simulation = Para_Simulation;
        //        DI_TopShuttleTrayInPositionV.Simulation = Para_Simulation;
        //        DI_TopShuttleTrayInPositionH.Simulation = Para_Simulation;
        //        DI_BottomShuttleTrayInPositionV.Simulation = Para_Simulation;
        //        DI_BottomShuttleTrayInPositionH.Simulation = Para_Simulation;

        //        //2017/05/29 Jay Tsao Remark
        //        //載盤區偵測台車有無盤Sensor位置錯誤，對照式Sensor光線與台車定位塊干涉，造成誤判有盤。(軟體先忽略不判斷)
        //        //DI_TopShuttleTrayExist.Simulation = true;
        //        //DI_BottomShuttleTrayExist.Simulation = true;
        //    }
        //}

        protected override void SimulationSetting(bool simulation)
        {
            if (DI_StackerTrayFullV!=null)
            DI_StackerTrayFullV.Simulation = simulation;
            if (DI_StackerTrayFullH != null)
            DI_StackerTrayFullH.Simulation = simulation;
            if (DI_StackerTrayExistV != null)
            DI_StackerTrayExistV.Simulation = simulation;
            if (DI_StackerTrayExistH != null)
            DI_StackerTrayExistH.Simulation = simulation;
            if (DI_TopShuttleTrayExist != null)
            DI_TopShuttleTrayExist.Simulation = simulation;
            if (DI_BottomShuttleTrayExist != null)
            DI_BottomShuttleTrayExist.Simulation = simulation;
            if (DI_TopShuttleTrayInPositionX1 != null)
            DI_TopShuttleTrayInPositionX1.Simulation = simulation;
            if (DI_TopShuttleTrayInPositionX2 != null)
            DI_TopShuttleTrayInPositionX2.Simulation = simulation;
            if (DI_BottomShuttleTrayInPositionX1 != null)
            DI_BottomShuttleTrayInPositionX1.Simulation = simulation;
            if (DI_BottomShuttleTrayInPositionX2 != null)
            DI_BottomShuttleTrayInPositionX2.Simulation = simulation;

            SimulationSetting2(simulation);
        }

        protected virtual void SimulationSetting2(bool simulation)
        {
            //由子類別實作設定 simulation 的內容
        }

        protected void Reset()
        {
            Var_Locker = null;
            Var_User = null;
            Var_InsideList.Clear();
        }

        public bool IsUser(object user)
        {
            return Var_InsideList.Contains(user);
        }

        /// <summary>
        /// 進入 (會先判斷是否被鎖定，無被鎖定才可進入)
        /// </summary>
        /// <param name="user">使用者</param>
        /// <param name="through">是否只是經過<c>true</c> [through].</param>
        /// <returns></returns>
        public bool Enter(object user, bool through)
        {
            if (Var_Locker != null)
            {
                //如果已被鎖定，禁止進入
                return false;
            }

            //加入路過者清單
            if (!Var_InsideList.Contains(user))
            {
                Var_InsideList.Add(user);
                JLogger.LogDebug("CTrayStacker", string.Format("{0}:Add through - {1}", HeirName, ((CTrayShuttle)user).ID.ToString()));
            }

            //未被鎖定，則判斷是否只是經過
            if (through)
            {
                return true;
            }
            else
            {
                //判斷是否已有人使用
                if (Var_User == null)
                {
                    //無人使用
                    Var_User = user;
                    JLogger.LogDebug("CTrayStacker", string.Format("{0}:Add user - {1}", HeirName, ((CTrayShuttle)user).ID.ToString()));
                    return true;
                }
                if (Var_User == user)
                {
                    JLogger.LogDebug("CTrayStacker", string.Format("{0}:Same user - {1}", HeirName, ((CTrayShuttle)user).ID.ToString()));
                    return true;
                }
                //不只是經過，是要使用此區域，則要先判斷是否已有其他使用者
                //if (Var_InsideList.Count.Equals(0))
                //{
                //    //無其他使用者，將使用者加入使用者清單
                //    Var_InsideList.Add(user);
                //    return true;
                //}
                //if (Var_InsideList.Count.Equals(1))
                //{
                //    //只有自己一個使用者
                //    if (Var_InsideList.Contains(user))
                //    {
                //        return true;
                //    }
                //}
            }
            //已有使用者要使用，禁止進入
            return false;
        }

        /// <summary>
        /// 離開
        /// </summary>
        /// <param name="user">The user.</param>
        public bool Leave(object user)
        {
            //判斷是否為路過，是路過才從路過者清單移除
            if (Var_InsideList.Contains(user))
            {
                Var_InsideList.Remove(user);
                JLogger.LogDebug("CTrayStacker", string.Format("{0}:Remove through - {1}", HeirName, ((CTrayShuttle)user).ID.ToString()));
            }
            //移除使用者
            if (Var_User != null)
            {
                if (Var_User.Equals(user))
                {
                    Var_User = null;
                    JLogger.LogDebug("CTrayStacker", string.Format("{0}:Remove user - {1}", HeirName, ((CTrayShuttle)user).ID.ToString()));
                }
            }
            return true;
        }

        /// <summary>
        /// 鎖定者
        /// </summary>
        /// <value>
        /// The locker.
        /// </value>
        public object Locker
        {
            get { return Var_Locker; }
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
                    //JLogger.DebugLog("CTrayStacker", string.Format("{0}:Already locked by - {1}", HeirName, ((CTrayShuttle)locker).ID.ToString()));
                    return true;
                }
                //已被鎖定，所以鎖定失敗，須等解鎖後，才可再被鎖定
                return false;
            }
            //判斷是否無路過者
            if (Var_InsideList.Count > 0 && !Var_InsideList.Contains(locker))
            {
                //有路過者，且不是目前使用者，所以無法鎖定，等路過者離開後，才可鎖定
                return false;
            }
            //判斷使用者是否與鎖定者相同
            if (Var_User != null)
            {
                if (Var_User.Equals(locker))
                {
                    //鎖定成功，紀錄鎖定者
                    Var_Locker = locker;
                    JLogger.LogDebug("CTrayStacker", string.Format("{0}:Lock by - {1}", HeirName, ((CTrayShuttle)locker).ID.ToString()));
                    return true;
                }
            }
            //判斷使用者是否為 1 個(超過 1 個，代表同時有多個使用者在此區域，不可鎖定)
            //if (Var_InsideList.Count.Equals(1))
            //{
            //    //判斷使用者與鎖定者是否相同(要鎖定前，務必先進入)
            //    if (Var_InsideList.Contains(locker))
            //    {
            //        //鎖定成功，紀錄鎖定者
            //        Var_Locker = locker;
            //        return true;
            //    }
            //    // locker 未進入 (使用程序錯誤，鎖定前須先進入)
            //    return false;
            //}
            //使用者超過 1 個(須等其他使用者離開)
            return false;
        }

        /// <summary>
        /// 解除鎖定 (但解鈴還需繫鈴人，所以會檢查解鎖者的身分是否與當初鎖定者相同)
        /// </summary>
        /// <param name="unlocker">The unlocker.</param>
        /// <returns></returns>
        public bool Unlock(object unlocker)
        {
            //判斷是否無人鎖定
            if (Var_Locker == null)
            {
                return true;    //無人鎖定，無須解鎖
            }

            //判斷解鎖者是否為鎖定者
            if (Var_Locker != null)
            {
                if (Var_Locker.Equals(unlocker))
                {
                    //清除使用者
                  //  if (Var_User.Equals(unlocker))
                   // {
                  //      Var_User = null;
                  //      JLogger.LogDebug("CTrayStacker", string.Format("{0}:Remove user - {1}", HeirName, ((CTrayShuttle)unlocker).ID.ToString()));
                  //  }
                    //是，解鎖成功
                    Var_Locker = null;
                    JLogger.LogDebug("CTrayStacker", string.Format("{0}:Unlock by - {1}", HeirName, ((CTrayShuttle)unlocker).ID.ToString()));
                    return true;
                }
            }
            //否，解鎖失敗
            return false;
        }

        /// <summary>
        /// 判斷盤堆疊區是否有盤
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [有盤]; otherwise, <c>false</c>.
        /// </returns>
        public bool StackerTrayExist()
        {
            if (this.DryRun || this.Simulation)
            {
                return true;    //空跑模式時，不做實際偵測，回傳有盤
            }
            bool b1 = DI_StackerTrayExistV.ValueOn;
            bool b2 = DI_StackerTrayExistH.ValueOn;
            return (b1 || b2);
        }

        /// <summary>
        /// 判斷盤堆疊區是否無盤
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [有盤]; otherwise, <c>false</c>.
        /// </returns>
        public bool StackerTrayNonExist()
        {
            if (this.Simulation)
            {
                return true;   //空跑模式時，不做實際偵測，回傳無盤
            }
            bool b1 = DI_StackerTrayExistV.ValueOff;
            bool b2 = DI_StackerTrayExistH.ValueOff;
            return (b1 && b2);
        }

        /// <summary>
        /// 判斷盤堆疊區是否滿盤
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [滿盤]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsStackerFullTray()
        {
            if (this.Simulation)
            {
                return true;   //模擬模式時，不做實際偵測，回傳滿盤
            }
            bool b1 = DI_StackerTrayFullV.ValueOn;
            bool b2 = DI_StackerTrayFullH.ValueOn;
            return (b1 || b2);
        }

        /// <summary>
        /// 判斷盤堆疊區是否未滿盤
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [滿盤]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsStackerNotFullTray()
        {
            if (this.Simulation)
            {
                return true;   //模擬模式時，不做實際偵測，回傳不滿盤
            }
            bool b1 = DI_StackerTrayFullV.ValueOff;
            bool b2 = DI_StackerTrayFullH.ValueOff;
            return (b1 && b2);
        }

        /// <summary>
        /// 判斷盤分離是否卡盤
        /// <para>2020-12-04 v1.3 Jay Tsao Added.</para>
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [位置良好]; otherwise, <c>false</c>.
        /// </returns>
        private bool StackerTrayInPosition()
        {
            if (this.Simulation)
            {
                return true;   //模擬模式時，不做實際偵測，回傳不滿盤
            }
            bool b1 = DI_StackerTrayInPositionX1.ValueOff;
            bool b2 = DI_StackerTrayInPositionX2.ValueOff;
            return (b1 && b2);
        }

        /// <summary>
        /// 判斷台車是否有盤
        /// </summary>
        /// <returns></returns>
        public bool ShuttleTrayExist(CTrayShuttleID tid)
        {
            if (this.DryRun || this.Simulation)
            {
                return true;   //空跑模式時，不做實際偵測，回傳軌道上有盤
            }

            switch (tid)
            {
                case CTrayShuttleID.TopCTrayShuttle:
                    return DI_TopShuttleTrayExist.ValueOff;

                case CTrayShuttleID.BottomCTrayShuttle:
                    return DI_BottomShuttleTrayExist.ValueOff;
            }
            return false;
        }

        /// <summary>
        /// 判斷台車是否無盤
        /// </summary>
        /// <param name="defVal">if set to <c>true</c> [回傳預設值為軌道上有盤].</param>
        /// <returns></returns>
        public bool ShuttleTrayNonExist(CTrayShuttleID tid)
        {
            if (this.DryRun || this.Simulation)
            {
                return true;   //空跑模式時，不做實際偵測，回傳軌道上有盤
            }

            switch (tid)
            {
                case CTrayShuttleID.TopCTrayShuttle:
                    return DI_TopShuttleTrayExist.ValueOn;

                case CTrayShuttleID.BottomCTrayShuttle:
                    return DI_BottomShuttleTrayExist.ValueOn;
            }
            return false;
        }

        /// <summary>
        /// 判斷台車盤位置是否良好
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [位置良好]; otherwise, <c>false</c>.
        /// </returns>
        public bool ShuttleTrayInPosition(CTrayShuttleID tid)
        {
            bool b1 = false;
            bool b2 = false;
            bool b3 = StackerTrayInPosition();  //2020-12-04 v1.3 Jay Tsao Added. (判斷盤分離是否良好無卡盤)
            switch (tid)
            {
                case CTrayShuttleID.TopCTrayShuttle:
                    {
                        b1 = DI_TopShuttleTrayInPositionX1.ValueOn;
                        b2 = DI_TopShuttleTrayInPositionX2.ValueOn;
                    }
                    break;

                case CTrayShuttleID.BottomCTrayShuttle:
                    {
                        b1 = DI_BottomShuttleTrayInPositionX1.ValueOn;
                        b2 = DI_BottomShuttleTrayInPositionX2.ValueOn;
                    }
                    break;
            }
            return (b1 && b2);
        }
    }
}