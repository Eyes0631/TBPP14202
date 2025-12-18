using System;

namespace CommonObj
{
    /// <summary>
    /// 治具台車車號
    /// </summary>
    public enum KitShuttleID
    {
        NONE = 0,
        //左台車
        TransferShuttleA = 1,
        //右台車
        TransferShuttleB = 2,
        //Unknow
        Unknow = 4,
    }

    /// <summary>
    /// 治具台車使用者
    /// </summary>
    public enum KitShuttleOwner
    {
        NONE = 0,
        //Board Head A
        HDT_BIB_A,
        //Board Head B
        HDT_BIB_B,
        //Bowl Feeder A
        HDT_BF_A,
        //Bowl Feeder B
        HDT_BF_B,
    }

    /// <summary>
    /// 治具台車狀態
    /// </summary>
    [Flags]
    public enum TransferShuttleState
    {
        NONE = 0,
        //承載材料：待測料，承載狀態：空車 (等待 CCD 檢查有無殘料 >>> 等待 HDT_DP 設定預約位置)
        EMPTY = 1,
        //承載材料：待測料，承載狀態：空車(已預約) (Tray -> Transfer)
        ORDER = 2,
        //承載材料：待測料，承載狀態：滿料 (等待 CCD 讀取 2D Code >>> Transfer -> DiePak)
        FULL = 4,

        //承載材料：待測料，承載狀態：空車 (等待 HDT_BIB 設定預約位置)
        EMPTY_CHECKED = 8,
    }
}
