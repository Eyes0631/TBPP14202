using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaeLibGeneral
{
    //=============================================================================================

    /// <summary>
    /// 委派 - 模組 Alarm 的回呼函式
    /// </summary>
    /// <param name="ret">錯誤代碼</param>
    public delegate void AlarmCallback(int ret, bool clear = false);

    //=============================================================================================

    /// <summary>
    /// 模組基礎抽象類別
    /// </summary>
    public abstract class JModuleBase
    {
        private AlarmCallback mModuleAlarmCallback = null;   //Alarm 委派
        private bool mSimulation = false;   //模擬跑

        public JModuleBase(AlarmCallback alarm)
        {
            mModuleAlarmCallback = alarm;
            this.DryRun = false;
            this.Simulation = false;
        }

        #region 公開屬性

        /// <summary>
        /// 是否使用空跑功能
        /// </summary>
        /// <value>
        ///   <c>true</c> 使用空跑功能; otherwise, <c>false</c>.
        /// </value>
        public bool DryRun { get; set; }

        /// <summary>
        /// 是否使用模擬功能
        /// </summary>
        /// <value>
        ///   <c>true</c> 使用模擬功能; otherwise, <c>false</c>.
        /// </value>
        public bool Simulation
        {
            get { return this.mSimulation; }
            set
            {
                this.mSimulation = value;
                SimulationSetting(this.mSimulation);
            }
        }

        #endregion 公開屬性

        #region 保護抽象函式

        protected abstract void SimulationSetting(bool simulation); //由子類別實作設定 simulation 的內容

        #endregion 保護抽象函式

        #region 公開方法

        /// <summary>
        /// 顯示 Alarm 的函式
        /// </summary>
        /// <param name="ret"></param>
        public void ShowAlarm(int ret)//模組Alarm
        {
            if (mModuleAlarmCallback != null)
            {
                mModuleAlarmCallback(ret, false);
            }
        }

        /// <summary>
        /// 清除 Alarm 的函式
        /// </summary>
        /// <param name="ret"></param>
        public void ClearAlarm(int ret)
        {
            if (mModuleAlarmCallback != null)
            {
                mModuleAlarmCallback(ret, true);
            }
        }

        #endregion 公開方法
    }

    //=============================================================================================
}