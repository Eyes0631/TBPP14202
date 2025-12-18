using PaeLibGeneral;
using ProVLib;
using System;
using System.Collections.Generic;

namespace PaeLibProVSDKEx
{
    /// <summary>
    /// 搭載防撞系統的馬達元件 (必須搭配 ProVLib 的 Motor 物件使用)
    /// </summary>
    /// <example>
    /// 使用說明 :
    /// <code>
    /// //1. 初始化 JMotor 物件 mt (注意!!同路線之優先權不可重複，否則會無法移動)
    /// JMotor mt = new JMotor(馬達元件, 路線編號, 優先權);
    /// //2. 設定起始位置
    /// mt.InitialPosition = 同一路線(line)上絕對座標的起始位置
    /// //3. 設定尺寸
    /// mt.MoverSize = 移動物件的尺寸大小
    /// //4. 設定安全距離
    /// mt.SafeDistance = 移動物件之間的安全距離
    /// //【以上 2~4 項，於初始化後設定，設定後若需變更，建議重新歸零，否則可能會導致保護失效】
    /// //5. 馬達歸零重置
    /// mt.HomeReset();
    /// //6. 馬達歸零
    /// mt.Home();  //回傳值型態 bool
    /// //7. 馬達移動
    /// mt.Goto(要移動的相對位置);  //回傳值型態 bool
    /// </code>
    /// </example>
    public class MotorEx : IDisposable
    {
        //馬達清單
        private static List<MotorEx> Movers = null;

        private Motor MT_Mover = null;    //ProV Motor 元件
        private uint LineID;    //移動路線編號，同一線的才需做防撞保護
        private uint PriorityH;   //優先權高值
        private uint PriorityL;   //優先權低值
        private uint Priority;   //優先權(比較時使用) - 數字越大，優先權越高

        private bool m_Simulation = false;      //是否開啟模擬功能
        private int m_InitialPosition;          //起始位置
        private uint m_MoverSize;                //尺寸大小
        private uint m_SafeDistance;             //安全距離

        //----------------------------------------
        // 防撞機制使用
        //----------------------------------------
        private int m_RelCurrentPos;     //台車目前相對座標位置 (即馬達回朔位置，不包含 Para_InitialPos 值)

        private int m_RelTargetPos;      //台車目標相對座標位置 (即使用者指定的目標位置，不包含 Para_InitialPos 值)

        private int m_AbsCurrentPos;     //台車目前絕對座標位置 (Para_InitialPos + 馬達回朔位置)
        private int m_AbsTargetPos;      //台車目標絕對座標位置 (Para_InitialPos + 使用者指定的目標位置)

        private int m_AbsLockedPos;      //台車鎖定絕對座標位置 (防撞機制演算後可鎖定的絕對座標位置，已包含 Para_InitialPos 值)
        //----------------------------------------

        private JTimer TM_Busy = null;     //Motor busy timer

        /// <summary>
        /// JMotor 靜態建構子
        /// 靜態建構函式可以用來初始化任何靜態資料，或執行只需執行一次的特定動作。
        /// 在建立第一個執行個體或參考任何靜態成員之前，會自動呼叫靜態建構函式。
        /// </summary>
        static MotorEx()
        {
            Movers = new List<MotorEx>();
        }

        /// <summary>
        /// JMotor 建構子
        /// </summary>
        /// <param name="mt">Motor物件</param>
        /// <param name="line_id">路線編號</param>
        /// <param name="priority_l">低優先權設定值(正整數，數值越大，優先權越高)</param>
        /// <param name="priority_h">高優先權設定值(正整數，數值越大，優先權越高)</param>
        public MotorEx(Motor mt, uint line_id, uint priority_l, uint priority_h)
        {
            MT_Mover = mt;
            LineID = line_id;
            PriorityL = Math.Min(priority_l, priority_h);    //取最小值
            PriorityH = Math.Max(priority_l, priority_h);   //取最大值
            Priority = PriorityH;   //預設優先權為高

            m_Simulation = false;
            m_InitialPosition = 0;
            m_MoverSize = 0;
            m_SafeDistance = 0;
            m_RelCurrentPos = 0;

            TM_Busy = new JTimer();
            //TM_Busy.AutoReset = true;
            TM_Busy.Reset();

            if ((Movers != null) && (Priority > 0))
            {
                Movers.Add(this);   //將馬達加至防撞保護清單中
            }
        }

        ~MotorEx()
        {
            this.Dispose();
        }

        public void Dispose()
        {
        }

        /// <summary>
        /// 是否開啟模擬功能
        /// </summary>
        /// <value>
        ///   <c>true</c> if simulation; otherwise, <c>false</c>.
        /// </value>
        public bool Simulation
        {
            get { return m_Simulation; }
            set { m_Simulation = value; }
        }

        /// <summary>
        /// 同一路線(line)上絕對座標的起始位置
        /// </summary>
        public int InitialPosition
        {
            get { return m_InitialPosition; }
            set
            {
                m_InitialPosition = value;
                m_AbsLockedPos = m_InitialPosition;     //2017/05/29 Jay Tsao 以 m_InitialPosition 初始化
            }
        }

        /// <summary>
        /// 移動物件的尺寸大小
        /// </summary>
        public uint MoverSize
        {
            get { return m_MoverSize; }
            set { m_MoverSize = value; }
        }

        /// <summary>
        /// 移動物件之間的安全距離
        /// </summary>
        public uint SafeDistance
        {
            get { return m_SafeDistance; }
            set { m_SafeDistance = value; }
        }

        /// <summary>
        /// 是否移動中
        /// </summary>
        /// <param name="busy_ms">馬達 busy 訊號持續時間</param>
        /// <returns><c>true</c> if busy; otherwise, <c>false</c>.</returns>
        public bool Busy(int busy_ms = 0)
        {
            bool bRet = false;
            if (MT_Mover.Busy())
            {
                if (busy_ms.Equals(0) || TM_Busy.Count(busy_ms))
                {
                    TM_Busy.Reset();
                    bRet = true;
                }
            }
            else
            {
                TM_Busy.Reset();
            }
            return bRet;

            //return MT_Mover.Busy();
        }

        /// <summary>
        /// 台車目前相對座標位置 (即馬達回朔位置，不包含 Para_InitialPos 值)
        /// </summary>
        /// <value>
        /// The relative current position.
        /// </value>
        public int RelCurrentPos
        {
            get
            {
                //有馬達則以馬達回朔位置為準，若無馬達則以使用者指定值為準
                if (MT_Mover != null)
                {
                    m_RelCurrentPos = MT_Mover.ReadEncPos();
                }
                return m_RelCurrentPos;
            }
            set
            {
                m_RelCurrentPos = value;
            }
        }

        /// <summary>
        /// 台車目標相對座標位置 (即使用者指定的目標位置，不包含 Para_InitialPos 值)
        /// </summary>
        /// <value>
        /// The relative target position.
        /// </value>
        public int RelTargetPos
        {
            get { return m_RelTargetPos; }
            set { m_RelTargetPos = value; }
        }

        /// <summary>
        /// 台車目前絕對座標位置 (Para_InitialPos + 馬達回朔位置)
        /// </summary>
        /// <value>
        /// The current position.
        /// </value>
        public int AbsCurrentPos
        {
            get
            {
                m_AbsCurrentPos = InitialPosition + RelCurrentPos;
                return m_AbsCurrentPos;
            }
        }

        /// <summary>
        /// 台車目標絕對座標位置 (Para_InitialPos + 使用者指定的目標位置)
        /// </summary>
        /// <value>
        /// The target position.
        /// </value>
        public int AbsTargetPos
        {
            get
            {
                m_AbsTargetPos = InitialPosition + RelTargetPos;
                return m_AbsTargetPos;
            }
        }

        /// <summary>
        /// 台車鎖定絕對座標位置 (防撞機制演算後可鎖定的絕對座標位置，已包含 Para_InitialPos 值)
        /// <para>注意!!使用者不可直接設定此值，使用者僅可設定 RelativeTargetPos</para>
        /// </summary>
        /// <value>
        /// The locked position.
        /// </value>
        private int AbsLockedPos
        {
            get { return m_AbsLockedPos; }
            set { m_AbsLockedPos = value; }
        }

        /// <summary>
        /// 切換為低優先權
        /// </summary>
        public void SetLowPriority()
        {
            Priority = PriorityL;
        }

        /// <summary>
        /// 切換為高優先權
        /// </summary>
        public void SetHeightPriority()
        {
            Priority = PriorityH;
        }

        /// <summary>
        /// 馬達移動
        /// </summary>
        /// <param name="Pos">The position.</param>
        /// <returns></returns>
        public bool Goto(int Pos)
        {
            int CanMoveRelPos = this.CanMoveTo(Pos);
            if (Simulation)
            {
                this.RelCurrentPos = CanMoveRelPos;
                return true;
            }

            bool B1 = this.MT_Mover.G00(CanMoveRelPos);
            bool B2 = this.AbsTargetPos.Equals(this.AbsLockedPos);
            if (B1 && B2)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 馬達 Servo On/Off
        /// </summary>
        /// <param name="bSon">true : Servo On, flase : Servo Off</param>
        public void ServoOn(bool bSon = true)
        {
            MT_Mover.ServoOn(bSon);
        }

        /// <summary>
        /// 馬達歸零重置
        /// </summary>
        public void HomeReset()
        {
            if (!Simulation)
            {
                this.MT_Mover.HomeReset();
            }
        }

        /// <summary>
        /// 馬達歸零
        /// </summary>
        /// <returns><c>true</c> if Home OK; otherwise, <c>false</c>.</returns>
        public bool Home()
        {
            if (Simulation)
            {
                return true;
            }
            return this.MT_Mover.Home();
        }

        /// <summary>
        /// 取得可移動的位置
        /// </summary>
        /// <param name="ReleativePos">The releative position.</param>
        /// <returns>回傳可移動的位置</returns>
        private int CanMoveTo(int ReleativePos)
        {
            string sMoverInfo = "【Motor : " + this.MT_Mover.Name +
                ", LineID : " + this.LineID.ToString() +
                ", Priority : " + this.Priority.ToString() +
                ", Position : " + ReleativePos.ToString() +
                "】";
            string sMsg = "";

            //bool bRet = false;

            //設定目標位置(相對座標)
            this.RelTargetPos = ReleativePos;
            //tlyA.RelativeTargetPos = ReleativePos;

            //判斷優先權是否大於 0 (小於等於 0 不做保護)
            if (this.Priority > 0)
            {
                //是否不在移動中
                if (this.Busy().Equals(false))
                {
                    int cp = this.AbsCurrentPos;       //目前位置
                    int tp = this.AbsTargetPos;        //目標位置
                    int lp = tp;                       //鎖定位置

                    int maxPos = Math.Max(this.AbsCurrentPos, this.AbsTargetPos);     //移動範圍最大值
                    int minPos = Math.Min(this.AbsCurrentPos, this.AbsTargetPos);     //移動範圍最小值

                    foreach (MotorEx mv in Movers)
                    {
                        //判斷是否為同一線(不同線不需檢查)
                        if (mv.LineID.Equals(this.LineID))
                        {
                            if (mv.Priority > 0)
                            {
                                if (!mv.Equals(this))
                                {
                                    int SafeDist = Math.Max((int)this.SafeDistance, (int)mv.SafeDistance);
                                    //判斷目前位置是否小於 mv
                                    if (this.AbsCurrentPos < mv.AbsCurrentPos)
                                    {
                                        //是，計算最大值 (maxPos)

                                        //判斷優先權
                                        if (this.Priority > mv.Priority)
                                        {
                                            //優先權大於 mv，以 mv 的 AbsCurrentPos 與 AbsLockerPos 來決定
                                            maxPos = Math.Min(maxPos, (mv.AbsCurrentPos - (int)this.MoverSize - SafeDist));
                                            maxPos = Math.Min(maxPos, (mv.AbsLockedPos - (int)this.MoverSize - SafeDist));
                                        }
                                        else if (this.Priority < mv.Priority)
                                        {
                                            //優先權小於 mv，以 mv 的 AbsTargetPos, AbsCrrentPos 與 AbsLockerPos 來決定
                                            maxPos = Math.Min(maxPos, (mv.AbsTargetPos - (int)this.MoverSize - SafeDist));
                                            maxPos = Math.Min(maxPos, (mv.AbsCurrentPos - (int)this.MoverSize - SafeDist));
                                            maxPos = Math.Min(maxPos, (mv.AbsLockedPos - (int)this.MoverSize - SafeDist));
                                        }
                                        else
                                        {
                                            //優先權相同
                                            maxPos = this.AbsCurrentPos;    //不得移動
                                        }
                                    }
                                    else
                                    {
                                        //否，計算最小值 (minPos)

                                        //判斷優先權
                                        if (this.Priority > mv.Priority)
                                        {
                                            //優先權大於 mv，以 mv 的 AbsCurrentPos 與 AbsLockedPos 來決定
                                            minPos = Math.Max(minPos, (mv.AbsCurrentPos + (int)mv.MoverSize + SafeDist));
                                            minPos = Math.Max(minPos, (mv.AbsLockedPos + (int)mv.MoverSize + SafeDist));
                                        }
                                        else if (this.Priority < mv.Priority)
                                        {
                                            //優先權小於 mv，以 mv 的 AbsTargetPos, AbsCurrentPos 與 AbsLockedPos 來決定
                                            minPos = Math.Max(minPos, (mv.AbsTargetPos + (int)mv.MoverSize + SafeDist));
                                            minPos = Math.Max(minPos, (mv.AbsCurrentPos + (int)mv.MoverSize + SafeDist));
                                            minPos = Math.Max(minPos, (mv.AbsLockedPos + (int)mv.MoverSize + SafeDist));
                                        }
                                        else
                                        {
                                            //優先權相同
                                            minPos = this.AbsCurrentPos;    //不得移動
                                        }
                                    }
                                    sMsg = "(LineID : " + mv.LineID.ToString() +
                                        ", Motor : " + mv.MT_Mover.Name +
                                        ", Size : " + mv.MoverSize.ToString() +
                                        ", SafeDist : " + SafeDist.ToString() +
                                        ", TargetPos : " + mv.AbsTargetPos.ToString() +
                                        ", CurrentPos : " + mv.AbsCurrentPos.ToString() +
                                        ", LockedPos : " + mv.AbsLockedPos.ToString() + ")";
                                    JLogger.LogDebug("JMotor", sMoverInfo + sMsg);
                                }
                            }
                        }
                    }

                    if (this.AbsTargetPos < minPos)
                    {
                        //target min max
                        this.AbsLockedPos = minPos;
                    }
                    else if (this.AbsTargetPos > maxPos)
                    {
                        //min max target
                        this.AbsLockedPos = maxPos;
                    }
                    else
                    {
                        //min target max
                        this.AbsLockedPos = this.AbsTargetPos;
                    }
                }
                //bRet = true;
                sMsg = "(Size : " + this.MoverSize.ToString() +
                    ", SafeDist : " + this.SafeDistance.ToString() +
                    ", AbsTargetPos : " + this.AbsTargetPos.ToString() +
                    ", AbsCurrentPos : " + this.AbsCurrentPos.ToString() +
                    ", AbsLockedPos : " + this.AbsLockedPos.ToString() +
                    ")";
                JLogger.LogDebug("JMotor", sMoverInfo + sMsg);
            }
            else
            {
                //優先權為 0 ，不需做防撞保護
                this.AbsLockedPos = this.AbsTargetPos;
                //bRet = true;
                sMsg = "(Size : " + this.MoverSize.ToString() +
                    ", SafeDist : " + this.SafeDistance.ToString() +
                    ", AbsTargetPos : " + this.AbsTargetPos.ToString() +
                    ", AbsCurrentPos : " + this.AbsCurrentPos.ToString() +
                    ", AbsLockedPos : " + this.AbsLockedPos.ToString() +
                    ")";
                JLogger.LogDebug("JMotor", sMoverInfo + sMsg);
            }
            return (this.AbsLockedPos - this.InitialPosition);
        }
    }
}