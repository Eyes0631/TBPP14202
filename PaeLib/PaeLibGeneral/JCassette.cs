using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaeLibGeneral
{
    /// <summary>
    /// Slot Status Enum (插槽狀態列舉)
    /// </summary>
    public enum SlotStatus
    {
        /// <summary>
        /// 初始值(未掃描)
        /// </summary>
        NULL = 0,

        /// <summary>
        /// 未知有無板(掃描不到板子，但不代表真的無板)
        /// </summary>
        UNKNOW = 1,

        /// <summary>
        /// 待處理(未進板)
        /// </summary>
        TODO = 2,

        /// <summary>
        /// 空的(已進板)
        /// </summary>
        EMPTY = 3,

        /// <summary>
        /// 已完成(已退板)
        /// </summary>
        DONE = 4,

        /// <summary>
        /// 停用
        /// </summary>
        DISABLED = 5,

        /// <summary>
        /// 異常
        /// </summary>
        ERROR = 6,
    };

    /// <summary>
    /// Cassette Slot 資訊資料結構
    /// </summary>
    internal class SlotInfo
    {
        //slot 狀態
        public SlotStatus Status { get; set; }

        //與下一層 Slot 的距離 (Read Only)
        public int Pitch { get; private set; }

        //建構子
        public SlotInfo(SlotStatus status, int pitch)
        {
            this.Status = status;
            this.Pitch = pitch;
        }
    }

    /// <summary>
    /// 卡匣類別，通用於 Rack、Magazine、Cassette 等應用
    /// </summary>
    /// <example>
    /// 以手推台車應用為案例，分為上下兩個區塊，每一區塊有16層，每層Pitch為45000，兩區塊的Gap為150000：
    /// <code>
    /// JCassette trolley = new JCassette();
    /// trolley.AddSlotInfo(16, 45000, 0);  //設定第一區塊的 slot 資訊 (手推台車的下層)
    /// trolley.AddSlotInfo(16, 45000, 150000);  //設定第二區塊的 slot 資訊 (手推台車的上層)
    /// trolley.ResetStatus(SlotStatus.UNKNOW); //重置每一層的狀態為未知
    /// </code>
    /// </example>
    public class JCassette
    {
        /// <summary>
        /// Cassette Slot 資訊清單
        /// </summary>
        private List<SlotInfo> Slot = new List<SlotInfo>();

        /// <summary>
        /// 建構子
        /// </summary>
        public JCassette()
        {
            Reset();
        }

        /// <summary>
        /// 重置 Cassette 所有資訊
        /// </summary>
        public void Reset()
        {
            Slot.Clear();
        }

        /// <summary>
        /// 取得 Cassette Slot 總數
        /// </summary>
        /// <returns>回傳 Cassette Slot 總數</returns>
        public int GetTotalSlots()
        {
            return Slot.Count;
        }

        /// <summary>
        /// 重置所有 Slot 的狀態
        /// </summary>
        /// <param name="setstatus">指定所有 Slot 要重置的狀態</param>
        public void ResetStatus(SlotStatus status)
        {
            foreach (SlotInfo si in Slot)
            {
                si.Status = status;
            }
        }

        /// <summary>
        /// 取得指定 Slot 的狀態
        /// </summary>
        /// <param name="n">指定 Slot，第一層(最底下)為 1，第二層為 2，...以此類推</param>
        /// <returns>回傳指定 Slot 狀態，若輸入的 Slot 錯誤，則回傳 ssERROR</returns>
        public SlotStatus GetStatus(int n)
        {
            if (n > 0 && n <= Slot.Count)
            {
                return Slot[n - 1].Status;
            }
            return SlotStatus.ERROR;
        }

        /// <summary>
        /// 設定指定 Slot 的狀態
        /// </summary>
        /// <param name="n">指定 Slot，第一層(最底下)為 1，第二層為 2，...以此類推</param>
        /// <param name="status">要設定的 Slot 狀態</param>
        /// <returns>true : 指定 Slot 的狀態設定成功，false : 指定 Slot 的狀態設定失敗，可能是輸入參數錯誤</returns>
        public bool SetStatus(int n, SlotStatus status)
        {
            bool bRet = false;
            if (n > 0 && n <= Slot.Count)
            {
                Slot[n - 1].Status = status;
                bRet = true;
            }
            return bRet;
        }

        /// <summary>
        /// 加入 Cassette Slot 資訊
        /// </summary>
        /// <param name="count">加入的 Slot 數量</param>
        /// <param name="pitch">Slot 的間距</param>
        /// <param name="gap">加入的第一個 Slot (最下一層) 與前一 Slot (最上一層) 的間距</param>
        /// <returns>true : 加入 Slot 成功，false : 加入 Slot 失敗</returns>
        public bool AddSlotInfo(int count, int pitch, int gap)
        {
            bool bRet = false;
            //2021-02-24 Jay Tsao Fixed : gap >= 0, NOT gap > 0.
            if (count > 0 && pitch > 0 && gap >= 0)
            {
                Slot.Add(new SlotInfo(SlotStatus.NULL, gap));  //第 1 層
                for (int i = 2; i <= count; ++i)
                {
                    //第 2 到 count 層
                    Slot.Add(new SlotInfo(SlotStatus.NULL, pitch));	//新加入的第二層以後則以 pitch 作為間距
                }
                bRet = true;
            }
            return bRet;
        }

        /// <summary>
        /// 取得指定 Slot 的高度
        /// </summary>
        /// <param name="n">指定 Slot，第一層(最底下)為 1，第二層為 2，...以此類推</param>
        /// <returns>回傳指定 Slot 高度，第一層高度為 0，-1 代表高度計算失敗</returns>
        public int GetSlotHeight(int n)
        {
            int iRet = -1;
            if (n > 0 && n <= Slot.Count)
            {
                iRet = 0;
                //將 1 ~ n 層的 Pitch 加起來，就是第 n 層的高度
                for (int i = 1; i <= n; ++i)
                {
                    iRet += Slot[i - 1].Pitch;
                }
            }
            return iRet;
        }
    }
}