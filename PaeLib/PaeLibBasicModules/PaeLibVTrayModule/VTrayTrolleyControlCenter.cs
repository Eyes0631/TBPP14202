using PaeLibGeneral;
using System;
using System.Collections.Generic;

namespace PaeLibVTrayModule
{
    //設定狀態權限
    internal class TrolleyStatusPriority
    {
        public TROLLEY_STATE Status { get; set; }
        public int priority { get; set; }

        /// <summary>
        /// TrolleyStatusPriority 建構子
        /// </summary>
        /// <param name="st">狀態</param>
        /// <param name="po">權限值(數字越大，權限越高)</param>
        public TrolleyStatusPriority(TROLLEY_STATE st, int po)
        {
            Status = st;
            priority = po;
        }

        /// <summary>
        /// TrolleyStatusPriority 建構子
        /// </summary>
        /// <remarks>
        /// 2020-08-04 Jay Tsao Added. 直接以列舉值作為權限值
        /// </remarks>
        /// <param name="st">狀態</param>
        public TrolleyStatusPriority(TROLLEY_STATE st)
            : this(st, (int)st)
        {
        }
    }

    /// <summary>
    /// 直盤台車控制中心
    /// </summary>
    internal class VTrayTrolleyControlCenter
    {
        private string DebugLogMessage = "";

        #region 元件

        //台車狀態與優先權對照表
        private static List<TrolleyStatusPriority> TrolleyStatusPriorityTable = null;

        //台車物件清單
        private static List<VtmTrolley> Trolleys = null;

        #endregion 元件

        /// <summary>
        /// VTrayTrolleyControlCenter 靜態建構子
        /// 靜態建構函式可以用來初始化任何靜態資料，或執行只需執行一次的特定動作。
        /// 在建立第一個執行個體或參考任何靜態成員之前，會自動呼叫靜態建構函式。
        /// </summary>
        static VTrayTrolleyControlCenter()
        {
            TrolleyStatusPriorityTable = new List<TrolleyStatusPriority>();
            Trolleys = new List<VtmTrolley>();

            //2020-08-04 Jay Tsao Added.
            //內定的台車狀態權限(數字越大，權限越高，低權限的台車要禮讓高權限台車)
            TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.IDLE));
            TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.LOADING));
            TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.UNLOADING));
            TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.WAITUNLOADING)); //等待收盤
            TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.FINISH_01));     //一般收盤
            TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.FINISH_02));     //特殊收盤(手臂搬盤之類的)
            //以下狀態，若有使用，須由使用者設定優先權，否則無法使用，台車將無法移動
            TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.READY_01));
            TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.WORKING_01));
            TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.READY_02));
            TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.WORKING_02));
            TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.READY_03));
            TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.WORKING_03));
            TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.READY_04));
            TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.WORKING_04));
            TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.READY_05));
            TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.WORKING_05));

            //2020-08-04 Jay Tsao Marked.
            ////內定的台車狀態權限(數字越大，權限越高，低權限的台車要禮讓高權限台車)
            //TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.IDLE, 0));
            //TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.LOADING, 99));
            //TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.UNLOADING, 98));
            //TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.WAITUNLOADING, 97)); //等待收盤
            //TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.FINISH_01, 10));     //一般收盤
            //TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.FINISH_02, 11));     //特殊收盤(手臂搬盤之類的)
            ////以下狀態，若有使用，須由使用者設定優先權，否則無法使用，台車將無法移動
            //TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.READY_01, 30));
            //TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.WORKING_01, 50));
            //TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.READY_02, 31));
            //TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.WORKING_02, 51));
            //TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.READY_03, 32));
            //TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.WORKING_03, 52));
            //TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.READY_04, 33));
            //TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.WORKING_04, 53));
            //TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.READY_05, 34));
            //TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(TROLLEY_STATE.WORKING_05, 54));

            //設定計時器自動重置
            //OverTime_Home.AutoReset = true;
            //OverTime_LoadTray.AutoReset = true;
            //OverTime_UnLoadTray.AutoReset = true;
        }

        /// <summary>
        /// VTrayTrolleyControlCenter 建構子
        /// </summary>
        public VTrayTrolleyControlCenter()
        {
            DebugLogMessage = "";
        }

        ~VTrayTrolleyControlCenter()
        {
        }

        #region 私有function

        //取得該狀態優先權號
        private int TrolleyPriority(TROLLEY_STATE ts)
        {
            int iRet = -1;
            TrolleyStatusPriority tsp = TrolleyStatusPriorityTable.Find(x => x.Status.Equals(ts));
            if (tsp != null)
            {
                iRet = tsp.priority;
            }
            return iRet;
        }

        private void DebugLog(string s)
        {
            if (DebugLogMessage != s)
            {
                DebugLogMessage = s;
                JLogger.LogDebug("VTrayTrolleyControlCenter", DebugLogMessage);
            }
        }

        #endregion 私有function

        #region 共用function

        //設定狀態權重
        public ThreeValued SetStatePriority(TROLLEY_STATE st, int po)
        {
            ThreeValued Result = ThreeValued.UNKNOWN;
            //檢查此優先權是否已存在(index < 0 代表不存在)
            if (TrolleyStatusPriorityTable.FindIndex(x => x.priority.Equals(po)) < 0)
            {
                //TROLLEYSTATUS.READY_#N
                bool B1 = ((((int)st >= 30) && ((int)st <= 49)) && ((po >= 30) && (po <= 49)));
                //TROLLEYSTATUS.WORKING_#N
                bool B2 = ((((int)st >= 50) && ((int)st <= 69)) && ((po >= 50) && (po <= 69)));

                if (B1 || B2)
                {
                    int idx = TrolleyStatusPriorityTable.FindIndex(x => x.Status.Equals(st));
                    if (idx >= 0)
                    {
                        TrolleyStatusPriorityTable[idx].priority = po;
                    }
                    else
                    {
                        TrolleyStatusPriorityTable.Add(new TrolleyStatusPriority(st, po));
                    }
                    Result = ThreeValued.TRUE;
                }
            }
            else
            {
                Result = ThreeValued.FALSE;
            }
            return Result;
        }

        //清除台車清單
        public void ClearTrolley()
        {
            Trolleys.Clear();
        }

        //增加台車,回傳台車數
        public int AddTrolley(VtmTrolley tly)
        {
            if (tly != null)
            {
                Trolleys.Add(tly);
            }
            return Trolleys.Count;
        }

        /// <summary>
        /// 計算台車可移動位置
        /// </summary>
        /// <param name="LineID">台車移動路線編號</param>
        /// <param name="ID">台車編號</param>
        /// <param name="ReleativePos">台車移動目的地的相對位置</param>
        /// <returns>
        /// true : 計算完成
        /// false : 計算失敗(台車狀態與權限設定異常，台車不可移動)
        /// </returns>
        public bool MoveTo(int LineID, TROLLEY_ID ID, int ReleativePos)
        {
            string sTRR_INFO = "【LineID : " + LineID.ToString() +
                ", TrolleyID : " + ID.ToString() +
                ", Position : " + ReleativePos.ToString() +
                "】";
            string sMsg = "";

            //取得指定編號的台車
            VtmTrolley tlyA = Trolleys.Find(x => (x.ID.Equals(ID) && x.LineID.Equals(LineID)));

            bool bRet = false;
            //ThreeValued tRet = ThreeValued.UNKNOWN;
            //有找到台車
            if (tlyA != null)
            {
                //設定目標位置(相對座標)
                tlyA.RelativeTargetPos = ReleativePos;

                //判斷台車優先權是否大於 0
                if (TrolleyPriority(tlyA.Status) > 0)
                {
                    //判斷台車是否移動中
                    if (tlyA.Busy.Equals(false))
                    {
                        int cp = tlyA.CurrentPos;       //台車目前位置
                        int tp = tlyA.TargetPos;        //台車目標位置
                        int lp = tp;                    //台車鎖定位置

                        int maxPos = Math.Max(tlyA.CurrentPos, tlyA.TargetPos);     //台車可移動位置最大值
                        int minPos = Math.Min(tlyA.CurrentPos, tlyA.TargetPos);     //台車可移動位置最小值

                        foreach (VtmTrolley tlyB in Trolleys)
                        {
                            //判斷是否為同一線的台車(不同線不需檢查)
                            if (tlyB.LineID.Equals(tlyA.LineID))
                            {
                                if (TrolleyPriority(tlyB.Status) > 0)
                                {
                                    if (!tlyB.ID.Equals(tlyA.ID))
                                    {
                                        //判斷A的位置是否小於B
                                        if (tlyA.CurrentPos < tlyB.CurrentPos)
                                        {
                                            //是，計算 A 的最大值 (maxPos)

                                            //判斷 A 與 B 的優先權
                                            if (TrolleyPriority(tlyA.Status) > TrolleyPriority(tlyB.Status))
                                            {
                                                //A 的優先權大於 B，以 B 的 currentPos 與 lockerPos 來決定
                                                maxPos = Math.Min(maxPos, (tlyB.CurrentPos - (int)tlyA.Length - (int)tlyA.SafeDistance));
                                                maxPos = Math.Min(maxPos, (tlyB.LockedPos - (int)tlyA.Length - (int)tlyA.SafeDistance));
                                            }
                                            else
                                            {
                                                //A 的優先權小於 B，以 B 的 targetPos, currentPos 與 lockerPos 來決定
                                                maxPos = Math.Min(maxPos, (tlyB.TargetPos - (int)tlyA.Length - (int)tlyA.SafeDistance));
                                                maxPos = Math.Min(maxPos, (tlyB.CurrentPos - (int)tlyA.Length - (int)tlyA.SafeDistance));
                                                maxPos = Math.Min(maxPos, (tlyB.LockedPos - (int)tlyA.Length - (int)tlyA.SafeDistance));
                                            }
                                        }
                                        else
                                        {
                                            //否，計算 A 的最小值 (minPos)
                                            if (TrolleyPriority(tlyA.Status) > TrolleyPriority(tlyB.Status))
                                            {
                                                //A 的優先權大於 B，以 B 的 currentPos 與 lockerPos 來決定
                                                minPos = Math.Max(minPos, (tlyB.CurrentPos + (int)tlyB.Length + (int)tlyA.SafeDistance));
                                                minPos = Math.Max(minPos, (tlyB.LockedPos + (int)tlyB.Length + (int)tlyA.SafeDistance));
                                            }
                                            else
                                            {
                                                //A 的優先權小於 B，以 B 的 targetPos, currentPos 與 lockerPos 來決定
                                                minPos = Math.Max(minPos, (tlyB.TargetPos + (int)tlyB.Length + (int)tlyA.SafeDistance));
                                                minPos = Math.Max(minPos, (tlyB.CurrentPos + (int)tlyB.Length + (int)tlyA.SafeDistance));
                                                minPos = Math.Max(minPos, (tlyB.LockedPos + (int)tlyB.Length + (int)tlyA.SafeDistance));
                                            }
                                        }
                                        sMsg = "(LineID : " + tlyB.LineID.ToString() +
                                            ", TrolleyID : " + tlyB.ID.ToString() +
                                            ", Status : " + tlyB.Status.ToString() +
                                            ", Length : " + tlyB.Length.ToString() +
                                            ", SafeDist : " + tlyB.SafeDistance.ToString() +
                                            ", TargetPos : " + tlyB.TargetPos.ToString() +
                                            ", CurrentPos : " + tlyB.CurrentPos.ToString() +
                                            ", LockedPos : " + tlyB.LockedPos.ToString() + ")";
                                        DebugLog(sTRR_INFO + sMsg);
                                    }
                                }
                                else if (TrolleyPriority(tlyB.Status) < 0)
                                {
                                    //台車狀態與權限設定異常，台車不可移動
                                    return false;
                                }
                            }
                        }

                        if (tlyA.TargetPos < minPos)
                        {
                            //target min max
                            tlyA.LockedPos = minPos;
                        }
                        else if (tlyA.TargetPos > maxPos)
                        {
                            //min max target
                            tlyA.LockedPos = maxPos;
                        }
                        else
                        {
                            //min target max
                            tlyA.LockedPos = tlyA.TargetPos;
                        }
                    }
                    bRet = true;
                    sMsg = "(Status : " + tlyA.Status.ToString() +
                        ", Length : " + tlyA.Length.ToString() +
                        ", SafeDist : " + tlyA.SafeDistance.ToString() +
                        ", TargetPos : " + tlyA.TargetPos.ToString() +
                        ", CurrentPos : " + tlyA.CurrentPos.ToString() +
                        ", LockedPos : " + tlyA.LockedPos.ToString() +
                        ")";
                    DebugLog(sTRR_INFO + sMsg);
                }
                else if (TrolleyPriority(tlyA.Status) < 0)
                {
                    //台車狀態與權限設定異常，台車不可移動
                    sMsg = "(Message : 台車狀態與權限設定異常，台車不可移動!!)";
                    DebugLog(sTRR_INFO + sMsg);
                }
                else
                {
                    //台車狀態為 IDLE，不需做防撞保護判斷，可直接移動到指定位置
                    tlyA.LockedPos = tlyA.TargetPos;
                    bRet = true;
                    sMsg = "(Status : " + tlyA.Status.ToString() +
                        ", Length : " + tlyA.Length.ToString() +
                        ", SafeDist : " + tlyA.SafeDistance.ToString() +
                        ", TargetPos : " + tlyA.TargetPos.ToString() +
                        ", CurrentPos : " + tlyA.CurrentPos.ToString() +
                        ", LockedPos : " + tlyA.LockedPos.ToString() +
                        ")";
                    DebugLog(sTRR_INFO + sMsg);
                }
            }
            else
            {
                //未發現此車號台車
                sMsg = "(Message : 未發現此車號台車!!)";
                DebugLog(sTRR_INFO + sMsg);
            }
            return bRet;
        }

        #endregion 共用function
    }
}