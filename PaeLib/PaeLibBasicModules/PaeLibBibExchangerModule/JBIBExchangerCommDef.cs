using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaeLibBibExchangerModule
{
    /// <summary>
    /// BEM Alarm Code
    /// Handler 的 Alarm.xls 代碼需與此一致
    /// </summary>
    public enum BEMAlarmCode
    {
        //---------------------------------------------------------------------
        //Carrier Alarm Code Range : 1~30
        //---------------------------------------------------------------------
        //Alarm : 推車夾持氣缸按鈕偵測元件未初始化
        ArmBemCarrierClasperButtonNull = 1,

        //Alarm : 閘門氣缸按鈕偵測元件未初始化
        ArmBemCarrierGateButtonNull = 2,

        //Alarm : 推車夾持氣缸元件未初始化
        ArmBemCarrierClasperNull = 3,

        //Alarm : 閘門氣缸元件未初始化
        ArmBemCarrierGateNull = 4,

        //Alarm : 夾持推車動作逾時
        ArmBemCarrierClaspTimeout = 6,

        //Alarm : 釋放推車動作逾時
        ArmBemCarrierReleaseTimeout = 7,

        //Alarm : 關閉閘門動作逾時
        ArmBemCarrierCloseGateTimeout = 8,

        //Alarm : 開啟閘門動作逾時
        ArmBemCarrierOpenGateTimeout = 9,
        
        //Alarm : 推車未推至定位
        ArmBemCarrierPositionError = 11,

        //Alarm : 推車板定位不良
        ArmBemCarrierBIBPositionError = 12,

        //Alarm : 閘門板定位不良
        ArmBemCarrierGateBIBPositionError = 13,

        //Alarm : 手推台車樣式錯誤
        ArmBemCarrierTypeError = 14,

        //---------------------------------------------------------------------
        //Elevator Alarm Code Range : 31~50
        //---------------------------------------------------------------------
        //Alarm : Z軸馬達元件未初始化
        ArmBemElevatorAxisZNull = 31,

        //Alarm : 基準高度點位設定錯誤，無法計算 Z 軸工作高度
        ArmBemElevatorBaseHeightDataError = 36,

        //Alarm : 推車工作樓層高度設定錯誤，無法計算 Z 軸工作高度
        ArmBemElevatorSlotHeightDataError = 37,

        //Alarm : 上下層工作平台設定錯誤，無法計算 Z 軸工作高度化
        ArmBemElevatorStageIDDataError = 38,

        //Alarm : 觸發安全防護，Z 軸緊急停止!!
        ArmBemElevatorSafetyProtection = 41,

        //Alarm : 前板定位不良
        ArmBemElevatorFrontBIBPositionError = 42,

        //Alarm : 後板定位不良
        ArmBemElevatorBackBIBPositionError = 43,

        //Alarm : 觸發光閘安全防護
        ArmBemElevatorGratingBIBPositionError = 44,

        //---------------------------------------------------------------------
        //Transfer Alarm Code Range : 51~70
        //---------------------------------------------------------------------
        //Alarm : 前勾板氣缸元件未初始化
        ArmBemTransferFrontHookNull = 51,

        //Alarm : 後勾板氣缸元件未初始化
        ArmBemTransferBackHookNull = 52,

        //Alarm : Y軸馬達元件未初始化
        ArmBemTransferAxisYNull = 53,

        //Alarm : 板固定氣缸上升動作逾時
        ArmBemTransferPressureUpTimeout = 54,

        //Alarm : 板固定氣缸上升動作逾時
        ArmBemTransferPressureDownTimeout = 55,

        //Alarm : 前勾板氣缸上升動作逾時
        ArmBemTransferFrontHookUpTimeout = 56,

        //Alarm : 後勾板氣缸上升動作逾時
        ArmBemTransferBackHookUpTimeout = 57,

        //Alarm : 前勾板氣缸下降動作逾時
        ArmBemTransferFrontHookDownTimeout = 58,

        //Alarm : 後勾板氣缸下降動作逾時
        ArmBemTransferBackHookDownTimeout = 59,

        //Alarm : 從推車進板流程偵測到平台內板異常，請確認是否卡板
        ArmBemTransferTrackBIBDetectErrorCT = 61,

        //Alarm : 退板至推車流程偵測到平台內板異常，請確認是否卡板
        ArmBemTransferTrackBIBDetectErrorTC = 62,

        //Alarm : 送板至主機流程偵測到平台內板異常，請確認是否卡板
        ArmBemTransferTrackBIBDetectErrorTS = 63,

        //Alarm : 從主機收板流程偵測到平台內板異常，請確認是否卡板
        ArmBemTransferTrackBIBDetectErrorST = 64,

        //Alarm : 歸零流程偵測到平台內有板，請手動將板移除
        ArmBemTransferBIBRemainInHoming = 65,

        //Alarm : 條碼槍通訊埠開啟失敗 (請確認埠號設定是否正確)
        ArmBemTransferBarcodeReaderComPortError = 70,

        //...
        ArmBemUnknow = 99,
    }
}