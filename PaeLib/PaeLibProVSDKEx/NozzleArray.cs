using PaeLibGeneral;
using ProVLib;
using System;

namespace PaeLibProVSDKEx
{
    /// <summary>
    /// <c>吸嘴元件組(二維陣列)</c>類別
    /// <para>主要應用於 Module 控制吸嘴動作使用，包含吸嘴真空、破壞與氣缸控制，以及真空判斷的結果回傳 Module</para>
    /// </summary>
    public class NozzleArray : IDisposable
    {
        #region 私有變數

        private bool m_Simulation = false;
        private bool m_DryRun = false;
        private uint NOZZLE_NUM_X = 0;
        private uint NOZZLE_NUM_Y = 0;
        private Nozzle[,] Nozzles = null;

        /* TODO : 此處應不須另外使用 JTimer 來計時，而應使用元件(Cylinder & Vacuum)本身的計時器 (Jay Tsao 2020-08-30) */
        private JTimer DT_NozzleUp = null;
        private JTimer TO_NozzleUp = null;
        private JTimer DT_NozzleDown = null;
        private JTimer TO_NozzleDown = null;
        private JTimer DT_VacuumCheck = null;
        private JTimer TO_VacuumCheck = null;
        private JTimer DT_VacuumOn = null;
        private JTimer DT_VacuumOff = null;
        private JTimer DT_DestroyOn = null;
        private JTimer DT_DestroyOff = null;
        /* TODO : 此處應不須另外使用 JTimer 來計時，而應使用元件(Cylinder & Vacuum)本身的計時器 (Jay Tsao 2020-08-30) */

        #endregion 私有變數

        #region JNozzleSet 建構子

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="xn">吸嘴陣列行數(Col)</param>
        /// <param name="yn">吸嘴陣列列數(Row)</param>
        public NozzleArray(uint xn, uint yn)
        {
            m_Simulation = false;
            m_DryRun = false;

            NOZZLE_NUM_X = xn;
            NOZZLE_NUM_Y = yn;

            Nozzles = new Nozzle[NOZZLE_NUM_X, NOZZLE_NUM_Y];

            //Reset Nozzles Object to null
            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    Nozzles[x, y] = null;
                }
            }

            DT_NozzleUp = new JTimer();
            TO_NozzleUp = new JTimer();
            DT_NozzleDown = new JTimer();
            TO_NozzleDown = new JTimer();
            DT_VacuumCheck = new JTimer();
            TO_VacuumCheck = new JTimer();
            DT_VacuumOn = new JTimer();
            DT_VacuumOff = new JTimer();
            DT_DestroyOn = new JTimer();
            DT_DestroyOff = new JTimer();
        }

        #endregion JNozzleSet 建構子

        ~NozzleArray()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            //Reset Nozzles Object to null
            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    Nozzles[x, y] = null;
                }
            }
        }

        #region 公用屬性

        /// <summary>
        /// 是否開啟模擬功能
        /// </summary>
        public bool Simulation
        {
            get { return m_Simulation; }
            set
            {
                m_Simulation = value;
                for (int x = 0; x < NOZZLE_NUM_X; x++)
                {
                    for (int y = 0; y < NOZZLE_NUM_Y; y++)
                    {
                        if (Nozzles[x, y] != null)
                        {
                            Nozzles[x, y].Simulation = m_Simulation;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 是否開啟空跑模式
        /// </summary>
        public bool DryRun
        {
            get { return m_DryRun; }
            set
            {
                m_DryRun = value;
                for (int x = 0; x < NOZZLE_NUM_X; x++)
                {
                    for (int y = 0; y < NOZZLE_NUM_Y; y++)
                    {
                        if (Nozzles[x, y] != null)
                        {
                            Nozzles[x, y].DryRun = m_DryRun;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 取得吸嘴陣列中指定[x,y]的吸嘴 JNozzle 物件
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>JNozzle 物件</returns>
        public Nozzle this[int x, int y]
        {
            get
            {
                if ((x < NOZZLE_NUM_X) && (y < NOZZLE_NUM_Y))
                {
                    if (Nozzles[x, y] != null)
                    {
                        return Nozzles[x, y];
                    }
                }
                return null;
            }
        }

        #endregion 公用屬性

        #region 公用函式

        /// <summary>
        /// 初始化吸嘴陣列中指定[x,y]的吸嘴 JNozzle 物件
        /// </summary>
        /// <param name="x">指定的 x 索引值(Col)</param>
        /// <param name="y">指定的 y 索引值(Row)</param>
        /// <param name="obCy">吸嘴氣缸</param>
        /// <param name="ibCyOn">吸嘴氣缸 On Sensor</param>
        /// <param name="ibCyOff">吸嘴氣缸 Off Sensor</param>
        /// <param name="ibVac">吸嘴真空偵測 Sensor</param>
        /// <param name="obVac">吸嘴真空開關</param>
        /// <param name="obDes">吸嘴破壞開關</param>
        /// <returns><c>true</c> 如果建立成功; 否則, <c>false</c>.</returns>
        public bool InitialNozzle(uint x, uint y, OutBit obCy, InBit ibCyOn, InBit ibCyOff,
            InBit ibVac, OutBit obVac, OutBit obDes)
        {
            if ((x < NOZZLE_NUM_X) && (y < NOZZLE_NUM_Y))
            {
                Nozzles[x, y] = new Nozzle(obCy, ibCyOn, ibCyOff, ibVac, obVac, obDes);
                if (Nozzles[x, y] != null)
                {
                    Nozzles[x, y].State = NozzleState.InUsing;
                    //Nozzles[x, y].ActionState = NozzleActionState.ActionSuccess;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 設定吸嘴陣列的吸嘴狀態
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>
        /// true : 設定成功
        /// false : 設定失敗
        /// </returns>
        public bool SetNozzlesState(NozzleState[,] state)
        {
            if (state.Rank.Equals(2))
            {
                int xn = state.GetUpperBound(0) + 1;
                int yn = state.GetUpperBound(1) + 1;
                if (xn.Equals((int)NOZZLE_NUM_X) && yn.Equals((int)NOZZLE_NUM_Y))
                {
                    for (int x = 0; x < xn; x++)
                    {
                        for (int y = 0; y < yn; y++)
                        {
                            if (this[x, y] != null)
                            {
                                this[x, y].State = state[x, y];
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 吸嘴陣列中符合指定狀態的吸嘴氣缸上升，其它放下
        /// </summary>
        /// <param name="crRet">結果陣列</param>
        /// <param name="state">指定吸嘴狀態(符合此狀態的吸嘴才會有作用)</param>
        /// <param name="delay_ms">The delay ms.</param>
        /// <param name="timeout_ms">The timeout ms.</param>
        /// <returns>
        /// ThreeValued.TRUE : 符合指定狀態的吸嘴氣缸已全數上升
        /// ThreeValued.FALSE : 符合指定狀態的吸嘴氣缸有部分上升失敗
        /// ThreeValued.UNKNOWN : 符合指定狀態的吸嘴氣缸未全數上升，且未超過 timeout_ms 時間
        /// </returns>
        public ThreeValued NozzleUp(ref ThreeValued[,] tRet, NozzleState state, int delay_ms = 0, int timeout_ms = 0)
        {
            bool bRet = ((delay_ms <= 0) || DT_NozzleUp.Count(delay_ms));
            if (tRet == null)
            {
                tRet = new ThreeValued[NOZZLE_NUM_X, NOZZLE_NUM_Y];
            }
            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    if (Nozzles[x, y] != null)
                    {
                        if (state.HasFlag(this[x, y].State))
                        {
                            tRet[x, y] = Nozzles[x, y].CylinderOn();    //吸嘴上升(ON)
                        }
                        else
                        {
                            tRet[x, y] = Nozzles[x, y].CylinderOff();     //吸嘴放下(OFF)
                        }
                    }
                    else
                    {
                        //未初始化之吸嘴物件視為不使用
                        tRet[x, y] = ThreeValued.TRUE;
                    }
                    bRet &= (tRet[x, y].Equals(ThreeValued.TRUE));
                    //if (!tRet[x, y].Equals(ComResult.crDONE))
                    //{
                    //    bRet = false;
                    //}
                }
            }
            if (bRet)
            {
                return ThreeValued.TRUE;
            }
            else
            {
                if ((timeout_ms > 0) && TO_NozzleUp.Count(timeout_ms))
                {
                    return ThreeValued.FALSE;
                }
            }
            return ThreeValued.UNKNOWN;
        }

        /// <summary>
        /// 吸嘴陣列中符合指定狀態的吸嘴氣缸放下，其他上升
        /// </summary>
        /// <param name="crRet">結果陣列</param>
        /// <param name="state">指定吸嘴狀態(符合此狀態的吸嘴才會有作用)</param>
        /// <param name="delay_ms">The delay ms.</param>
        /// <param name="timeout_ms">The timeout ms.</param>
        /// <returns>
        /// ThreeValued.TRUE : 符合指定狀態的吸嘴氣缸已全數放下
        /// ThreeValued.FALSE : 符合指定狀態的吸嘴氣缸有部分放下失敗
        /// ThreeValued.UNKNOWN : 符合指定狀態的吸嘴氣缸未全數放下，且未超過 timeout_ms 時間
        /// </returns>
        public ThreeValued NozzleDown(ref ThreeValued[,] tRet, NozzleState state, int delay_ms = 0, int timeout_ms = 0)
        {
            bool bRet = ((delay_ms <= 0) || DT_NozzleDown.Count(delay_ms));
            if (tRet == null)
            {
                tRet = new ThreeValued[NOZZLE_NUM_X, NOZZLE_NUM_Y];
            }
            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    if (Nozzles[x, y] != null)
                    {
                        if (state.HasFlag(this[x, y].State))
                        {
                            tRet[x, y] = Nozzles[x, y].CylinderOff();    //吸嘴放下(OFF)
                        }
                        else
                        {
                            tRet[x, y] = Nozzles[x, y].CylinderOn();     //吸嘴上升(ON)
                        }
                    }
                    else
                    {
                        //未初始化之吸嘴物件視為不使用
                        tRet[x, y] = ThreeValued.TRUE;
                    }
                    bRet &= (tRet[x, y].Equals(ThreeValued.TRUE));
                    //if (!crRet[x, y].Equals(ComResult.crDONE))
                    //{
                    //    bRet = false;
                    //}
                }
            }
            if (bRet)
            {
                return ThreeValued.TRUE;
            }
            else
            {
                if ((timeout_ms > 0) && TO_NozzleDown.Count(timeout_ms))
                {
                    return ThreeValued.FALSE;
                }
            }
            return ThreeValued.UNKNOWN;
        }

        /// <summary>
        /// 吸嘴陣列中符合指定狀態的吸嘴真空ON狀態(真空判斷)
        /// </summary>
        /// <param name="state">指定的吸嘴狀態</param>
        /// <returns>
        /// ThreeValued.TRUE : 真空偵測 Senosr On
        /// ThreeValued.FALSE : 真空偵測 Sensor Off
        /// ThreeValued.UNKNOWN : 不使用
        /// </returns>
        public ThreeValued[,] NozzleVacuumValueOn(NozzleState state)
        {
            ThreeValued[,] iRet = new ThreeValued[NOZZLE_NUM_X, NOZZLE_NUM_Y];
            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    if (Nozzles[x, y] != null)
                    {
                        if (state.HasFlag(this[x, y].State))
                        {
                            if (Nozzles[x, y].VacuumValueOn)
                            {
                                iRet[x, y] = ThreeValued.TRUE;     //真空偵測 Senosr On
                            }
                            else
                            {
                                iRet[x, y] = ThreeValued.FALSE;     //真空偵測 Sensor Off
                            }
                        }
                        else
                        {
                            iRet[x, y] = ThreeValued.UNKNOWN;       //不使用
                        }
                    }
                    else
                    {
                        //未初始化之吸嘴物件視為不使用
                        iRet[x, y] = ThreeValued.UNKNOWN;
                    }
                }
            }
            return iRet;
        }

        /// <summary>
        /// 吸嘴陣列中符合指定狀態的吸嘴真空OFF狀態(真空判斷)
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>
        /// ThreeValued.TRUE : 真空偵測 Senosr Off
        /// ThreeValued.FALSE : 真空偵測 Sensor On
        /// ThreeValued.UNKNOWN : 不使用
        /// </returns>
        public ThreeValued[,] NozzleVacuumValueOff(NozzleState state)
        {
            ThreeValued[,] iRet = new ThreeValued[NOZZLE_NUM_X, NOZZLE_NUM_Y];
            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    if (Nozzles[x, y] != null)
                    {
                        if (state.HasFlag(this[x, y].State))
                        {
                            if (!Nozzles[x, y].VacuumValueOn)
                            {
                                iRet[x, y] = ThreeValued.TRUE;     //真空偵測 Senosr Off
                            }
                            else
                            {
                                iRet[x, y] = ThreeValued.FALSE;     //真空偵測 Sensor On
                            }
                        }
                        else
                        {
                            iRet[x, y] = ThreeValued.UNKNOWN;       //不使用
                        }
                    }
                    else
                    {
                        //未初始化之吸嘴物件視為不使用
                        iRet[x, y] = ThreeValued.UNKNOWN;
                    }
                }
            }
            return iRet;
        }

        /// <summary>
        /// 吸嘴陣列中符合指定狀態的吸嘴開真空
        /// </summary>
        /// <param name="crRet">結果陣列</param>
        /// <param name="state">指定吸嘴狀態(符合此狀態的吸嘴才會有作用)</param>
        /// <param name="delay_ms">The delay ms.</param>
        /// <returns>
        /// </returns>
        public bool NozzleVacuumOn(NozzleState state, int delay_ms = 0)
        {
            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    if (Nozzles[x, y] != null)
                    {
                        if (state.HasFlag(this[x, y].State))
                        {
                            Nozzles[x, y].VacuumCtrlValue = true;   //開真空
                        }
                    }
                }
            }
            bool bRet = DT_VacuumOn.Count(delay_ms);
            return bRet;
        }

        /// <summary>
        /// 吸嘴陣列中符合指定狀態的吸嘴關真空
        /// </summary>
        /// <param name="crRet">結果陣列</param>
        /// <param name="state">指定吸嘴狀態(符合此狀態的吸嘴才會有作用)</param>
        /// <param name="delay_ms">The delay ms.</param>
        /// <returns>
        /// </returns>
        public bool NozzleVacuumOff(NozzleState state, int delay_ms = 0)
        {
            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    if (Nozzles[x, y] != null)
                    {
                        if (state.HasFlag(this[x, y].State))
                        {
                            Nozzles[x, y].VacuumCtrlValue = false;    //關真空
                        }
                    }
                }
            }
            bool bRet = DT_VacuumOff.Count(delay_ms);
            return bRet;
        }

        /// <summary>
        /// 吸嘴陣列中符合指定狀態的吸嘴開破壞
        /// </summary>
        /// <param name="crRet">結果陣列</param>
        /// <param name="state">指定吸嘴狀態(符合此狀態的吸嘴才會有作用)</param>
        /// <param name="delay_ms">The delay ms.</param>
        /// <returns>
        /// </returns>
        public bool NozzleDestroyOn(NozzleState state, int delay_ms = 100)
        {
            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    if (Nozzles[x, y] != null)
                    {
                        if (state.HasFlag(this[x, y].State))
                        {
                            Nozzles[x, y].DestoryCtrlValue = true;    //開破壞
                        }
                    }
                }
            }
            if (DT_DestroyOn.Count(delay_ms))
            {
                NozzleDestroyOff(state);    //關破壞
                return true;
            }
            return false;
        }

        /// <summary>
        /// 吸嘴陣列中符合指定狀態的吸嘴關破壞
        /// </summary>
        /// <param name="crRet">結果陣列</param>
        /// <param name="state">指定吸嘴狀態(符合此狀態的吸嘴才會有作用)</param>
        /// <param name="delay_ms">The delay ms.</param>
        /// <returns>
        /// </returns>
        public bool NozzleDestroyOff(NozzleState state, int delay_ms = 0)
        {
            for (int x = 0; x < NOZZLE_NUM_X; x++)
            {
                for (int y = 0; y < NOZZLE_NUM_Y; y++)
                {
                    if (Nozzles[x, y] != null)
                    {
                        if (state.HasFlag(this[x, y].State))
                        {
                            Nozzles[x, y].DestoryCtrlValue = false;    //關破壞
                        }
                    }
                }
            }
            bool bRet = DT_DestroyOff.Count(delay_ms);
            return bRet;
        }

        #endregion 公用函式
    }
}