using PaeLibGeneral;
using ProVLib;
using System;

namespace PaeLibCTrayModule
{
    /// <summary>
    /// 十字台車模組類別
    /// </summary>
    public class CTrayShuttle : JModuleBase, IDisposable
    {
        #region 參數設定

        private CTrayShuttleID Para_ID;             //台車編號

        #endregion 參數設定

        #region 程序變數

        private CTrayShuttleState Var_State;       //台車狀態
        private CTrayShuttleOperationMode Var_OperationMode;      //台車作業模式
        private int Var_Speed;      //台車速度

        private JActionTask Task_Home = new JActionTask();
        private JActionTask Task_LoadTray = new JActionTask();
        private JActionTask Task_UnloadTray = new JActionTask();
        private JActionTask Task_UnloadTrayByLoader = new JActionTask();

        #endregion 程序變數

        //#region 點位變數

        //private int Pos_LoadTray;               //接盤位置
        //private int Pos_UnloadTray;             //收盤位置
        //private int Pos_Working;                //工作位置
        //private int Pos_WaitingAfterLoad;       //接盤後等待位置
        //private int Pos_WaitingBeforeUnload;    //收盤前等待位置

        //#endregion 點位變數

        #region 元件

        //Motor
        private Motor MT_Shuttle = null;               //台車馬達

        #endregion 元件

        /// <summary>
        /// Initializes a new instance of the <see cref="CTrayShuttle" /> class.
        /// </summary>
        /// <param name="alarm">模組 Alarm 的回呼函式</param>
        /// <param name="id">台車編號</param>
        /// <param name="mtTRR">台車馬達參考</param>
        public CTrayShuttle(AlarmCallback alarm, CTrayShuttleID id, Motor mtTRR)
            : base(alarm)
        {
            Para_ID = id;
            MT_Shuttle = mtTRR;
            Var_Speed = mtTRR.Speed;
            Var_State = CTrayShuttleState.IDLE;
        }

        ~CTrayShuttle()
        {
            this.Dispose();
        }

        public void Dispose()
        {
        }

        #region 公開屬性

        ///// <summary>
        ///// 是否使用空跑功能
        ///// </summary>
        ///// <value>
        /////   <c>true</c> 使用空跑功能; otherwise, <c>false</c>.
        ///// </value>
        //public bool DryRun
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
        //public bool Simulation
        //{
        //    get { return Para_Simulation; }
        //    set { Para_Simulation = value; }
        //}

        /// <summary>
        /// 台車是否移動中 (Read-Only)
        /// </summary>
        /// <value>
        ///   <c>true</c> if busy; otherwise, <c>false</c>.
        /// </value>
        public bool Busy
        {
            get
            {
                if (MT_Shuttle != null)
                {
                    return MT_Shuttle.Busy();
                }
                return false;
            }
        }

        /// <summary>
        /// Shuttle ID (Read-Only)
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public CTrayShuttleID ID
        {
            get { return Para_ID; }
        }

        /// <summary>
        /// 台車狀態
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public CTrayShuttleState State
        {
            get { return Var_State; }
        }

        public CTrayShuttleOperationMode OperationMode
        {
            get { return Var_OperationMode; }
            set { Var_OperationMode = value; }
        }

        public int Speed
        {
            get { return Var_Speed; }
            set { Var_Speed = value; }
        }

        #endregion 公開屬性

        #region 私有函式

        //private void ShowModuleAlarm(ComResult ret)
        //{
        //    if (ModuleAlarmCallback != null)
        //    {
        //        ModuleAlarmCallback((int)ret, false);
        //    }
        //}

        //private void ClearModuleAlarm(int ret)
        //{
        //    if (ModuleAlarmCallback != null)
        //    {
        //        ModuleAlarmCallback(ret, true);
        //    }
        //}

        protected override void SimulationSetting(bool simulation)
        {
        }

        #endregion 私有函式

        #region 公有函式

        /// <summary>
        /// 重置台車狀態
        /// </summary>
        public void ResetSatate()
        {
            this.Var_State = CTrayShuttleState.IDLE;
        }

        /// <summary>
        /// Sets the trolley status.
        /// </summary>
        /// <param name="ts">The trolley status want to set.</param>
        /// <returns></returns>
        public bool SetState(CTrayShuttleState ts)
        {
            //IDLE -> READY -> WORKING -> FINISH -> IDLE ->...

            //檢查目前台車狀態是否為 IDLE 且要設定的狀態為 READY
            if (this.Var_State.Equals(CTrayShuttleState.IDLE) &&
                ts.Equals(CTrayShuttleState.READY))
            {
                this.Var_State = ts;
                return true;
            }
            //檢查目前台車狀態是否為 READY 且要設定的狀態為 WORKING or FINISH
            if (this.Var_State.Equals(CTrayShuttleState.READY) &&
                (ts.Equals(CTrayShuttleState.WORKING) ||
                ts.Equals(CTrayShuttleState.FINISH)))
            {
                this.Var_State = ts;
                return true;
            }
            //檢查目前台車狀態是否為 WORKING 且要設定的狀態為 FINISH
            if (this.Var_State.Equals(CTrayShuttleState.WORKING) &&
                ts.Equals(CTrayShuttleState.FINISH))
            {
                this.Var_State = ts;
                return true;
            }
            //檢查目前台車狀態是否為 FINISH 且要設定的狀態為 IDLE
            if (this.Var_State.Equals(CTrayShuttleState.FINISH) &&
                ts.Equals(CTrayShuttleState.IDLE))
            {
                this.Var_State = ts;
                return true;
            }
            //若不符合上述條件，不允許修改台車狀態
            return false;
        }

        public void SetSpeedRate(double speed_rate)
        {
            if (MT_Shuttle != null)
            {
                speed_rate = Math.Min(speed_rate, 1.0);     //限制最大值為 1.0
                speed_rate = Math.Max(speed_rate, 0.1);     //限制最小值為 0.1
                int speed = (int)(Var_Speed * speed_rate);
                MT_Shuttle.SetSpeed(speed);
            }
        }

        public bool Goto(int Pos)
        {
            if (MT_Shuttle != null)
            {
                bool bRet = MT_Shuttle.G00(Pos);
                return bRet;
            }
            return true;
        }

        public void Action_HomeReset()
        {
            this.Var_State = CTrayShuttleState.IDLE;
            Task_Home.Reset();
            if (MT_Shuttle != null)
            {
                MT_Shuttle.HomeReset();
            }
        }

        public bool Action_Home()
        {
            bool bRet = false;
            JActionTask Task = Task_Home;
            switch (Task.Value)
            {
                case 0:     //台車 Y 軸歸零
                    {
                        if (MT_Shuttle == null)
                        {
                            Task.Next(1000);
                        }
                        else if (MT_Shuttle.Home())
                        {
                            Task.Next(1000);
                        }
                    }
                    break;

                case 1000:
                    {
                        bRet = true;
                    }
                    break;
            }
            return bRet;
        }

        #endregion 公有函式
    }
}