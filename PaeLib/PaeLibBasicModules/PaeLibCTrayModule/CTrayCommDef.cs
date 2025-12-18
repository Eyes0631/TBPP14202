using System;

namespace PaeLibCTrayModule
{
    /// <summary>
    /// 生產模式
    /// </summary>
    [Flags]
    public enum PRODUCTION_MODE
    {
        NONE = 0,

        //上板
        LOAD = 1,

        //卸板
        UNLOAD = 2,

        //同時上卸板
        //LOAD_UNLOAD = (LOAD | UNLOAD),
        //全部
        //ALL = (NONE | LOAD | UNLOAD)
    }

    /// <summary>
    /// 台車編號
    /// </summary>
    public enum CTrayShuttleID
    {
        NONE = 0,

        //上台車
        TopCTrayShuttle = 1,

        //下台車
        BottomCTrayShuttle = 2
    }

    public enum CTrayShuttleStation
    {
        //工作區
        WORKING = 0,

        //等待區
        WAITING = 1,

        //已測料收盤區
        UL_TESTED = 2,

        //待測料載盤區
        LD_UNTESTED = 3,

        //空盤載盤區
        LD_EMPTY = 4
    }

    /// <summary>
    /// 台車動作模式
    /// </summary>
    public enum CTrayShuttleAction
    {
        //移動至工作點
        WORKING,

        //載盤
        LOAD_TRAY,

        //收盤
        UNLOAD_TRAY,

        //移動至等待點
        WAITING,

        //無
        NONE,
    }

    /// <summary>
    /// 台車狀態
    /// </summary>
    public enum CTrayShuttleState
    {
        IDLE = 0,

        FINISH = 10, //FINISH_01 = 11, FINISH_02 = 12, FINISH_03 = 13, FINISH_04 = 14,
        //FINISH_05 = 15, FINISH_06 = 16, FINISH_07 = 17, FINISH_08 = 18, FINISH_09 = 19,
        //FINISH_10 = 20, FINISH_11 = 21, FINISH_12 = 22, FINISH_13 = 13, FINISH_14 = 14,
        //FINISH_15 = 25, FINISH_16 = 26, FINISH_17 = 27, FINISH_18 = 28, FINISH_19 = 29,

        READY = 30, //READY_01 = 31, READY_02 = 32, READY_03 = 33, READY_04 = 34,
        //READY_05 = 35, READY_06 = 36, READY_07 = 37, READY_08 = 38, READY_09 = 39,
        //READY_10 = 40, READY_11 = 41, READY_12 = 42, READY_13 = 43, READY_14 = 44,
        //READY_15 = 45, READY_16 = 46, READY_17 = 47, READY_18 = 48, READY_19 = 49,

        WORKING = 50, //WORKING_01 = 51, WORKING_02 = 52, WORKING_03 = 53, WORKING_04 = 54,
        //WORKING_05 = 55, WORKING_06 = 56, WORKING_07 = 57, WORKING_08 = 58, WORKING_09 = 59,
        //WORKING_10 = 60, WORKING_11 = 61, WORKING_12 = 62, WORKING_13 = 63, WORKING_14 = 64,
        //WORKING_15 = 65, WORKING_16 = 66, WORKING_17 = 67, WORKING_18 = 68, WORKING_19 = 69,

        LOADING = 99, UNLOADING = 98,

        UNKNOW = 100
    }

    //十字盤模組的錯誤訊息代碼列舉(列舉值與 AlarmTable 的錯誤代碼必須相同)
    public enum CTMAlarmCode
    {
        //For CTM
        ER_CTM_UNKNOW = 0,

        ER_CTM_WrongStationOrAction = 1,         //{0} 台車目地站別或台車動作模式設定錯誤({1})

        //ER_CTM_AlreadyLocked = 2,                //{0} 已被其他台車鎖定({1})
        ER_CTM_AssignWrongShuttleID = 3,         //{0} 台車編號指定錯誤({1})

        ER_CTM_TrayShuttleProtection = 4,        //{0} 台車 Y 軸安全保護({1})
        ER_CTM_TrayShuttleICJump = 5,            //{0} 台車 IC 跳料，請手動確認({1})

        //For Tray Loader/Unloader
        ER_TLU_TraySeparateCyOnTimeout = 20,     //{0} 盤分離氣缸打開動作逾時({1})

        ER_TLU_TraySeparateCyOffTimeout = 21,    //{0} 盤分離氣缸關閉動作逾時({1})
        ER_TLU_TrayUnloadCyOnTimeout = 22,       //{0} 收盤氣缸上升動作逾時({1})
        ER_TLU_TrayUnloadCyOffTimeout = 23,      //{0} 收盤氣缸下降動作逾時({1})
        ER_TLU_TrayRemain = 24,                  //{0} 盤殘留({1})
        ER_TLU_TrayFull = 25,                    //{0} 滿盤({1})
        ER_TLU_TrayEmpty = 26,                   //{0} 無盤({1})
        ER_TLU_TrayInWrongPosition = 27,         //{0} 台車盤定位不良({1})
        ER_TLU_TrayNotOnShuttle = 28,            //{0} 盤未在台車上({1})
        ER_TLU_TrayRemainOnShuttle = 29,         //{0} 台車盤殘留({1})
        ER_TLU_AlreadyLocked = 30,                //{0} 已被其他台車鎖定({1})
        ER_TLU_AssignWrongShuttleID = 31,         //{0} 台車編號指定錯誤({1})
    }
}