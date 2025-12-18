using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFM
{
    public enum EBLM_ALARM_CODE
    {
        ER_BLM1_NozzleVacuumCheckTimeout,                           // BLM 1 吸嘴真空檢查超時警報
        ER_BLM2_NozzleVacuumCheckTimeout,                           // BLM 2 吸嘴真空檢查超時警報
        ER_BLM1_BowlFeederNoPartsTimeout,                           // BLM 1 震動盤無料超時報警
        ER_BLM2_BowlFeederNoPartsTimeout,                           // BLM 2 震動盤無料超時報警
        ER_BLM1_BowlFeederPickFailure,                              // BLM 1 Bowl Feeder取料失敗
        ER_BLM2_BowlFeederPickFailure,                              // BLM 2 Bowl Feeder取料失敗
        ER_BLM1_NozzleAbnormalityDetectedBeforePlacement,           // BLM 1 放置前偵測到吸嘴狀態異常
        ER_BLM2_NozzleAbnormalityDetectedBeforePlacement,           // BLM 2 放置前偵測到吸嘴狀態異常
        ER_BLM1_CCDConnectionFailure,                               // BLM 1 CCD連線異常
        ER_BLM2_CCDConnectionFailure,                               // BLM 2 CCD連線異常
        ER_BLM1_InspectionOverTimes,                                // BLM 1 連續辨識失敗超過設定次數
        ER_BLM2_InspectionOverTimes,                                // BLM 2 連續辨識失敗超過設定次數
        ER_BLM1_ZAixsNotSafePosButXAxisMove,                        // BLM 1 偵測到Z軸不在安全高度而X軸要移動
        ER_BLM2_ZAixsNotSafePosButXAxisMove,                        // BLM 2 偵測到Z軸不在安全高度而X軸要移動
        ER_BLM1_CheckNozzleHaveProductAfterPlace,                   // BLM 1 放置後檢查到吸嘴上仍有產品
        ER_BLM2_CheckNozzleHaveProductAfterPlace,                   // BLM 2 放置後檢查到吸嘴上仍有產品
        ER_BLM1_CheckNozzleFindProductAfterGarbageCan,              // BLM 1 丟垃极桶後檢查吸嘴發現有產品
        ER_BLM2_CheckNozzleFindProductAfterGarbageCan,              // BLM 2 丟垃极桶後檢查吸嘴發現有產品
        ER_BLM1_WaitCcdResultOverTime,                              // BLM 1 等待CCD回覆超時
        ER_BLM2_WaitCcdResultOverTime,                              // BLM 2 等待CCD回覆超時
        ER_BLM1_sofewareError,                                      // BLM 1 軟體異常請重新開啟並連線
        ER_BLM2_sofewareError,                                      // BLM 2 軟體異常請重新開啟並連線
    }       
}
