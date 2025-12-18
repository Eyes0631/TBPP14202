using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaeLibVTrayModule
{
    /// <summary>
    /// <c>台車編號</c>
    /// </summary>
    public enum TROLLEY_ID
    {
        /// <summary>
        /// A 台車
        /// </summary>
        TrolleyA,

        /// <summary>
        /// B 台車
        /// </summary>
        TrolleyB,

        /// <summary>
        /// 虛擬台車
        /// </summary>
        Virtual,

        /// <summary>
        /// 無台車
        /// </summary>
        NONE
    }

    /// <summary>
    /// <c>台車狀態</c>
    /// <para>IDLE : 0</para>
    /// <para>FINISH : 10~29</para>
    /// <para>READY : 30~49</para>
    /// <para>WORKING : 50~69</para>
    /// <para>WAITUNLOADING : 97</para>
    /// <para>UNLOADING : 98</para>
    /// <para>LOADING : 99</para>
    /// <para>UNKNOW : 100</para>
    /// </summary>
    /// <remarks>
    /// <para>2020-08-04 Jay Tsao 重新定義 WAITUNLOADING 的值(60 改為 97)</para>
    /// </remarks>
    public enum TROLLEY_STATE
    {
        /// <summary>
        /// 台車無盤
        /// </summary>
        IDLE = 0,

        /// <summary>
        /// 台車作業完成(準備一般收盤)
        /// </summary>
        FINISH_01 = 10,

        /// <summary>
        /// 台車作業完成(準備特殊收盤)
        /// </summary>
        FINISH_02 = 11,

        /// <summary>
        /// 台車準備工作01
        /// </summary>
        READY_01 = 30,

        /// <summary>
        /// 台車準備工作02
        /// </summary>
        READY_02 = 31,

        /// <summary>
        /// 台車準備工作03
        /// </summary>
        READY_03 = 32,

        /// <summary>
        /// 台車準備工作04
        /// </summary>
        READY_04 = 33,

        /// <summary>
        /// 台車準備工作05
        /// </summary>
        READY_05 = 34,

        /// <summary>
        /// 台車工作中01
        /// </summary>
        WORKING_01 = 50,

        /// <summary>
        /// 台車工作中02
        /// </summary>
        WORKING_02 = 51,

        /// <summary>
        /// 台車工作中03
        /// </summary>
        WORKING_03 = 52,

        /// <summary>
        /// 台車工作中04
        /// </summary>
        WORKING_04 = 53,

        /// <summary>
        /// 台車工作中05
        /// </summary>
        WORKING_05 = 54,

        /// <summary>
        /// 台車等待收盤(權限高於FINISH，所以不會讓開)
        /// </summary>
        WAITUNLOADING = 97,

        /// <summary>
        /// 台車收盤中
        /// </summary>
        UNLOADING = 98,

        /// <summary>
        /// 台車載盤中
        /// </summary>
        LOADING = 99,

        /// <summary>
        /// 台車無狀態
        /// </summary>
        UNKNOW = 100
    }

    /// <summary>
    /// <c>VTM Alarm Code</c>
    /// </summary>
    /// <remarks>
    /// Handler 的 Alarm.xls 代碼需與此一致
    /// </remarks>
    public enum VTMAlarmCode
    {
        //---------------------------------------------------------------------
        // Loader Alarm : 01~20
        //---------------------------------------------------------------------

        /// <summary>
        /// Alarm : 入料區已被其他台車鎖定
        /// </summary>
        TrayLoaderLocked = 1,

        /// <summary>
        /// Alarm : 入料區點位資料設定異常
        /// </summary>
        TrayLoaderPositionDataError = 2,

        /// <summary>
        /// Alarm : 入料區歸零動作逾時
        /// </summary>
        TrayLoaderHomeTimeout = 3,

        /// <summary>
        /// Alarm : 入料區載盤流程動作逾時
        /// </summary>
        TrayLoaderLoadTrayTimeout = 4,

        /// <summary>
        /// Alarm : 入料區收盤流程動作逾時
        /// </summary>
        TrayLoaderUnloadTrayTimeout = 5,

        /// <summary>
        /// Alarm : 入料區盤分離氣缸開啟動作逾時
        /// </summary>
        TrayLoaderSeparateTrayCylinderOpenTimeout = 6,

        /// <summary>
        /// Alarm : 入料區盤分離氣缸關閉動作逾時
        /// </summary>
        TrayLoaderSeparateTrayCylinderCloseTimeout = 7,

        /// <summary>
        /// Alarm : 入料區有盤殘留
        /// </summary>
        TrayLoaderTrayRemain = 11,

        /// <summary>
        /// Alarm : 入料區無盤
        /// </summary>
        TrayLoaderTrayEmpty = 12,

        /// <summary>
        /// Alarm : 入料區滿盤
        /// </summary>
        TrayLoaderTrayFull = 13,

        /// <summary>
        /// Alarm : 入料區盤分離異常，盤未確實分離
        /// </summary>
        TrayLoaderTraySeparateFailure = 14,

        /// <summary>
        /// Alarm : 入料區軌道上有盤殘留
        /// </summary>
        TrayLoaderTrackTrayRemain = 15,

        /// <summary>
        /// Alarm : 入料區軌道盤位置不良
        /// </summary>
        TrayLoaderTrackTrayPositionError = 16,

        //---------------------------------------------------------------------
        // Unloader Alarm : 21~40
        //---------------------------------------------------------------------

        /// <summary>
        /// Alarm : 收料區未被台車鎖定
        /// </summary>
        TrayUnloaderLocked = 21,

        /// <summary>
        /// Alarm : 收料區歸零動作逾時
        /// </summary>
        TrayUnloaderHomeTimeout = 22,

        /// <summary>
        /// Alarm : 收料區收盤流程動作逾時
        /// </summary>
        TrayUnloaderUnloadTrayTimeout = 23,

        /// <summary>
        /// Alarm : 收料區收盤氣缸上升動作逾時
        /// </summary>
        TrayUnloaderUnloadTrayCylinderUpTimeout = 24,

        /// <summary>
        /// Alarm : 收料區收盤氣缸下降動作逾時
        /// </summary>
        TrayUnloaderUnloadTrayCylinderDownTimeout = 25,

        /// <summary>
        /// Alarm : 收料區有盤殘留
        /// </summary>
        TrayUnloaderTrayRemain = 31,

        /// <summary>
        /// Alarm : 收料區滿盤
        /// </summary>
        TrayUnloaderTrayFull = 32,

        /// <summary>
        /// Alarm : 收料區軌道上有盤殘留
        /// </summary>
        TrayUnloaderTrackTrayRemain = 33,

        /// <summary>
        /// Alarm : 收料區軌道盤位置不良
        /// </summary>
        TrayUnloaderTrackTrayPositionError = 34,

        //---------------------------------------------------------------------
        // Trolley Alarm : 41~60
        //---------------------------------------------------------------------

        /// <summary>
        /// Alarm : 盤台車基準邊氣缸上升動作逾時
        /// </summary>
        TrayTrolleyBasePlaneCylinderUpTimeout = 41,

        /// <summary>
        /// Alarm : 盤台車基準邊氣缸下降動作逾時
        /// </summary>
        TrayTrolleyBasePlaneCylinderDownTimeout = 42,

        /// <summary>
        /// Alarm : 盤台車夾盤氣缸夾盤動作逾時
        /// </summary>
        TrayTrolleyClampCylinderFixTimeout = 43,

        /// <summary>
        /// Alarm : 盤台車夾盤氣缸放開動作逾時
        /// </summary>
        TrayTrolleyClampCylinderReleaseTimeout = 44,

        /// <summary>
        /// Alarm : 盤側推氣缸推出動作逾時
        /// </summary>
        TrayTrolleySidePushCylinderOutTimeout = 45,

        /// <summary>
        /// Alarm : 盤側推氣缸放收回作逾時
        /// </summary>
        TrayTrolleySidePushCylinderBackTimeout = 46,

        /// <summary>
        /// Alarm : 盤台車歸零流程動作逾時
        /// </summary>
        TrayTrolleyHomeTimeout = 47,

        /// <summary>
        /// Alarm : 盤台車盤固定流程動作逾時
        /// </summary>
        TrayTrolleyFixTrayTimeout = 48,

        /// <summary>
        /// Alarm : 盤台車盤釋放流程動作逾時
        /// </summary>
        TrayTrolleyReleaseTrayTimeout = 49,

        /// <summary>
        /// Alarm : 軌道盤掃描流程動作逾時
        /// </summary>
        TrayTrolleyScanTrackTrayTimeout = 50,

        /// <summary>
        /// Alarm : 盤台車載盤流程動作逾時
        /// </summary>
        TrayTrolleyLoadTrayTimeout = 51,

        /// <summary>
        /// Alarm : 盤台車退盤流程動作逾時
        /// </summary>
        TrayTrolleyUnloadTrayTimeout = 52,

        /// <summary>
        /// Alarm : 盤台車狀態錯誤，無法進行盤掃描
        /// </summary>
        TrayTrolleyScanTrackTrayStatusError = 53,

        /// <summary>
        /// Alarm : 盤台車狀態錯誤，無法進行載盤
        /// </summary>
        TrayTrolleyLoadTrayStatusError = 54,

        /// <summary>
        /// Alarm : 盤台車狀態錯誤，無法進行退盤
        /// </summary>
        TrayTrolleyUnloadTrayStatusError = 55,

        /// <summary>
        /// Alarm : 盤台車基準邊氣缸或夾盤氣缸未釋放，盤掃描流程暫停
        /// </summary>
        TrayTrolleyBasePlaneOrClampNotRelease = 56,

        /// <summary>
        /// Alarm : 軌道有盤殘留，請手動移除
        /// </summary>
        TrayTrolleyTrackTrayRemain = 57,
    }
}