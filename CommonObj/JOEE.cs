using PaeLibGeneral;
using PaeLibProVSDKEx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;

namespace CommonObj
{
    // public enum JBinType { Untested = 0, Tested, Bin1, Bin2, Bin3, Bin4, Bin5, Bin6, Bin7, Bin8, Bin9, Bin10, Bin11, Bin12, Bin13, Bin14, Bin15, Bin16, Bin17 };
    public enum JStatusType { Process, Completed, Missing };

    public class JOEESYS
    {
        private object OEE_LOCK = null;
        public string OEEDirectory { get; set; }   //儲存 OEE Data 的檔案路徑
        public string LotReportDirectory { get; set; }   //儲存 Lot Report 的檔案路徑

        //目前已生產板子數量
        public int BoardCount
        {
            get
            {
                lock (OEE_LOCK)
                {
                    return OEE_DATA.DiePakList.Count;
                }
            }
        }

        private JOEE OEE_DATA = null;
        private double CurrentCarrierStartTime;
        private string CurrentCarrierID;

        /// <summary>
        /// JOEESYS 建構子
        /// </summary>
        public JOEESYS()
        {
            OEE_LOCK = new object();
            OEE_DATA = new JOEE();
            //CurrentCarrier = new JCAR();
            Reset();
        }

        /// <summary>
        /// 取得指定 Bin 別的數量資訊
        /// </summary>
        /// <param name="bin">指定 Bin 別</param>
        /// <returns>數量資訊</returns>
        public JBINQTY GetBinQty(BinDefine bin)
        {
            lock (OEE_LOCK)
            {
                JBIN BinQty = OEE_DATA.LotInfo.BinQtyList.Find(x => x.Type.Equals(bin));
                if (BinQty == null)
                {
                    return new JBINQTY() { Process = 0, Completed = 0, Missing = 0 };
                }
                else
                {
                    return BinQty.Qty;
                }
            }
        }

        public void Reset()
        {
            lock (OEE_LOCK)
            {
                OEE_DATA.Reset();
                OEEDirectory = string.Empty;
                LotReportDirectory = string.Empty;
                CurrentCarrierStartTime = 0;
                CurrentCarrierID = string.Empty;
            }
        }
        public List<JCARRIER> DiepakList()
        {
            return OEE_DATA.DiePakList;
        }
        /// <summary>
        /// 開批
        /// </summary>
        /// <param name="lot_no">批號</param>
        /// <param name="opid">工號</param>
        /// <param name="carrier_type">板 Type</param>
        /// <param name="device_type">材料Type</param>
        /// <param name="test_program">測試程式名稱</param>
        /// <param name="recipe">料號</param>
        /// <param name="mode">模式</param>
        /// <param name="equiment_id">機台編號</param>
        /// <param name="version">版次</param>
        /// <param name="lot_qty">開批輸入數量</param>
        public void NewLot(string lot_no, string opid, string carrier_type, string device_type, string test_program, string recipe, string mode,
            string equiment_id, string version, int lot_qty)
        {
            OEE_DATA.LotInfo.Index = DateTime.Now.TimeOfDay.TotalSeconds;
            OEE_DATA.LotInfo.LotNo = lot_no;
            OEE_DATA.LotInfo.OPID = opid;
            OEE_DATA.LotInfo.CarrierType = carrier_type;
            OEE_DATA.LotInfo.DeviceType = device_type;
            OEE_DATA.LotInfo.TestProgram = test_program;
            OEE_DATA.LotInfo.Recipe = recipe;
            OEE_DATA.LotInfo.Mode = mode;
            OEE_DATA.LotInfo.EquimentID = equiment_id;
            OEE_DATA.LotInfo.Version = version;
            OEE_DATA.LotInfo.StartTime = DateTime.Now;
            OEE_DATA.LotInfo.LotQty = lot_qty;

            UpdateOEEData();  //Update lot report
        }

        /// <summary>
        /// 更新 Bin 數量
        /// </summary>
        /// <param name="type"></param>
        /// <param name="qty"></param>
        public void UpdateBinQty(BinDefine type, JStatusType status, int qty)
        {
            if (OEE_DATA.LotInfo.Index.Equals(0))
            {
                return;
            }

            lock (OEE_LOCK)
            {
                int idx = OEE_DATA.LotInfo.BinQtyList.FindIndex(x => x.Type.Equals(type));
                if (idx < 0)
                {
                    switch (status)
                    {
                        case JStatusType.Process:
                            {
                                OEE_DATA.LotInfo.BinQtyList.Add(new JBIN() { Type = type, Qty = new JBINQTY() { Process = qty, Completed = 0, Missing = 0 } });
                            }
                            break;
                        case JStatusType.Completed:
                            {
                                OEE_DATA.LotInfo.BinQtyList.Add(new JBIN() { Type = type, Qty = new JBINQTY() { Process = 0, Completed = qty, Missing = 0 } });
                            }
                            break;
                        case JStatusType.Missing:
                            {
                                OEE_DATA.LotInfo.BinQtyList.Add(new JBIN() { Type = type, Qty = new JBINQTY() { Process = 0, Completed = 0, Missing = qty } });
                            }
                            break;

                    }
                }
                else
                {
                    switch (status)
                    {
                        case JStatusType.Process:
                            {
                                OEE_DATA.LotInfo.BinQtyList[idx].Qty.Process += qty;
                            }
                            break;
                        case JStatusType.Completed:
                            {
                                OEE_DATA.LotInfo.BinQtyList[idx].Qty.Completed += qty;
                            }
                            break;
                        case JStatusType.Missing:
                            {
                                OEE_DATA.LotInfo.BinQtyList[idx].Qty.Missing += qty;
                            }
                            break;

                    }
                }
            }

            UpdateOEEData();    //Update OEE Data
        }

        /// <summary>
        /// 結批
        /// </summary>
        /// <param name="run_sec">運轉秒數</param>
        /// <param name="stop_sec">停止秒數</param>
        /// <param name="alarm_sec">警報秒數</param>
        public void EndLot(int run_sec, int stop_sec, int alarm_sec)
        {
            if (OEE_DATA.LotInfo.Index.Equals(0))
            {
                return;
            }

            OEE_DATA.LotInfo.EndTime = DateTime.Now;
            OEE_DATA.LotInfo.RunTime = run_sec;
            OEE_DATA.LotInfo.StopTime = stop_sec;
            OEE_DATA.LotInfo.AlarmTime = alarm_sec;

            lock (OEE_LOCK)
            {
                //將無結束時間的 Alarm 移除
                OEE_DATA.AlarmList.RemoveAll(x => x.EndTime.Equals(DateTime.MinValue));
            }

            UpdateOEEData();    //Update OEE Data
            GenLotReport();     //Generate Lot Report
            //GenLotReportForExcel();     //Generate Lot Report Excel
            GenLotReportForExcel2();
            OEE_DATA.LotInfo.Index = 0;     //Reset
        }

        /// <summary>
        /// 載入新板
        /// </summary>
        /// <param name="id">板號</param>
        /// <param name="map">板 Map 資料</param>
        public void NewCarrier(string id, string map)
        {
            if (OEE_DATA.LotInfo.Index.Equals(0))
            {
                return;
            }

            lock (OEE_LOCK)
            {
                JCARRIER carrier = new JCARRIER()
                {
                    Index = DateTime.Now.TimeOfDay.TotalSeconds,
                    LotCreateDateTime = OEE_DATA.LotInfo.Index,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.MinValue,
                    ID = id,
                    ReadMap = map
                };
                OEE_DATA.DiePakList.Add(carrier);
                CurrentCarrierStartTime = carrier.Index;
                CurrentCarrierID = carrier.ID;
            }

            UpdateOEEData();  //Update lot report
        }

        /// <summary>
        /// 板完成
        /// </summary>
        /// <param name="id">板號</param>
        public void CarrierFinish(string map)
        {
            if (OEE_DATA.LotInfo.Index.Equals(0))
            {
                return;
            }

            lock (OEE_LOCK)
            {
                ///判斷 Carrier 是否存在於 List 中
                int idx = OEE_DATA.DiePakList.FindIndex(x => (x.ID.Equals(CurrentCarrierID) && x.EndTime.Equals(DateTime.MinValue)));
                if (idx >= 0)
                {
                    OEE_DATA.DiePakList[idx].EndTime = DateTime.Now;
                    OEE_DATA.DiePakList[idx].WriteMap = map;
                    CurrentCarrierStartTime = 0;
                    CurrentCarrierID = string.Empty;
                }
            }

            UpdateOEEData();  //Update lot report
        }

        /// <summary>
        /// 發生 Alarm (Alarm 計時開始)
        /// </summary>
        /// <param name="alarm_code"></param>
        /// <param name="alarm_module"></param>
        /// <param name="alarm_msg"></param>
        public void AppearAlarm(string alarm_code, string alarm_module, string alarm_msg)
        {
            if (OEE_DATA.LotInfo.Index.Equals(0))
            {
                return;
            }
            if (!(alarm_code.IndexOf("E") >= 0))
            {
                return;
            }
            if (alarm_code.IndexOf("1403") >= 0)//PIN1 FAIL不列入計算
            {
                return;
            }
            lock (OEE_LOCK)
            {
                ///判斷 Alarm 是否存在於 List 中
                int idx = OEE_DATA.AlarmList.FindIndex(x => (x.AlarmCode.Equals(alarm_code) && x.AlarmMessage == alarm_msg && x.EndTime.Equals(DateTime.MinValue)));
                if (idx >= 0)
                {
                    ///此 Alarm 已存在於 List 中，所以只要直接開始計時即可
                    OEE_DATA.AlarmList[idx].Start();
                }
                else
                {
                    ///此 Alarm 不存在 List 中，所以要新增 Alarm 並開始計時
                    JALM alm = new JALM()
                    {
                        Index = DateTime.Now.TimeOfDay.TotalSeconds,
                        LotCreateDateTime = OEE_DATA.LotInfo.Index,
                        CarrierStartDateTime = CurrentCarrierStartTime,
                        StartTime = DateTime.Now,
                        EndTime = DateTime.MinValue,
                        AlarmCode = alarm_code,
                        AlarmModule = alarm_module,
                        AlarmMessage = alarm_msg
                    };
                    OEE_DATA.AlarmList.Add(alm);
                }
            }

            UpdateOEEData();  //Update lot report
        }

        /// <summary>
        /// 暫時清除 Alarm (Alarm 計時暫停)
        /// </summary>
        /// <param name="alarm_code"></param>
        public void DisappearAlarm(string alarm_code)
        {
            if (OEE_DATA.LotInfo.Index.Equals(0))
            {
                return;
            }

            lock (OEE_LOCK)
            {
                if (alarm_code.Equals("ClearAll"))
                {
                    //暫停所有 Alarm 計時(暫時清除)
                    foreach (JALM alm in OEE_DATA.AlarmList)
                    {
                        alm.Stop();
                    }
                }
                else
                {
                    ///判斷 Alarm 是否存在於 List 中
                    int idx = OEE_DATA.AlarmList.FindIndex(x => (x.AlarmCode.Equals(alarm_code) && x.EndTime.Equals(DateTime.MinValue)));
                    if (idx >= 0)
                    {
                        OEE_DATA.AlarmList[idx].Stop();
                    }
                }
            }

            UpdateOEEData();  //Update lot report
        }
        public bool NeedExportLotEndReport()
        {
            if (OEE_DATA.LotInfo.Index.Equals(0))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 確認 Alarm 解除(Alarm 計時停止)
        /// </summary>
        /// <param name="alarm_code"></param>
        public void DissolveAlarm(string alarm_code)
        {
            if (OEE_DATA.LotInfo.Index.Equals(0))
            {
                return;
            }

            lock (OEE_LOCK)
            {
                ///判斷 Alarm 是否存在於 List 中
                int idx = OEE_DATA.AlarmList.FindIndex(x => (x.AlarmCode.Equals(alarm_code) && x.EndTime.Equals(DateTime.MinValue)));
                if (idx >= 0)
                {
                    OEE_DATA.AlarmList[idx].Stop();
                    OEE_DATA.AlarmList[idx].EndTime = DateTime.Now;  ///紀錄 Alarm 結束時間
                }
            }

            UpdateOEEData();  //Update lot report            
        }
        public DateTime StartTime
        {
            get
            {
                return OEE_DATA.LotInfo.StartTime;
            }
        }
        public string StartTimeStr
        {
            get
            {
                return OEE_DATA.LotInfo.StartTimeStr;
            }
        }
        public string LotNo
        {
            get
            {
                return OEE_DATA.LotInfo.LotNo;
            }
        }
        /// <summary>
        /// 加入 Device ID
        /// </summary>
        /// <param name="id"></param>
        public void AddDeviceID(string id)
        {
            if (OEE_DATA.LotInfo.Index.Equals(0))
            {
                return;
            }

            lock (OEE_LOCK)
            {
                if (OEE_DATA.DeviceIDList.Exists(x => x.Equals(id)).Equals(false))
                {
                    OEE_DATA.DeviceIDList.Add(id);
                }
            }

            //UpdateOEEData();  //Update lot report  
        }

        public void AddDeviceID(IEnumerable<string> idList)
        {
            if (OEE_DATA.LotInfo.Index.Equals(0))
            {
                return;
            }

            foreach (string id in idList)
            {
                AddDeviceID(id);
            }
        }
        public string GetLotReportPath()
        {
            return GetFilePath(FILE_PATH_TYPE.LOT_REPORT);
        }
        public void AddDeviceID(string DiepakID, int blk_x, int blk_y, int xn, int yn, string bin, string id)
        {
            if (OEE_DATA.LotInfo.Index.Equals(0))
            {
                return;
            }

            lock (OEE_LOCK)
            {
                bool b1 = (OEE_DATA.ICIDList.FindIndex(x => x.BLK_X.Equals(blk_x) && x.BLK_Y.Equals(blk_y) && x.XN.Equals(xn) && x.YN.Equals(yn) && x.DieapkID.Equals(DiepakID))) < 0;
                if (b1)
                {
                    ICID IC_DATA = new ICID();
                    IC_DATA.SetData(DiepakID, blk_x, blk_y, xn, yn, bin, id);
                    OEE_DATA.ICIDList.Add(IC_DATA);
                }
                else
                {
                    int x = 0;
                }
            }


        }

        enum FILE_PATH_TYPE { OEE, LOT_REPORT, LOT_REPORT_EXCEL }

        private string GetFilePath(FILE_PATH_TYPE type)
        {
            string dir = string.Empty;
            string file_name = string.Empty;
            switch (type)
            {
                case FILE_PATH_TYPE.OEE:
                    {
                        if (OEEDirectory.Equals(string.Empty))
                        {
                            OEEDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "OEE_DATA");
                        }
                        dir = OEEDirectory;
                        file_name = string.Format("{0}_{1}.xml", OEE_DATA.LotInfo.LotNo, OEE_DATA.LotInfo.StartTime.ToString("yyyyMMddHHmmss"));
                    }
                    break;
                case FILE_PATH_TYPE.LOT_REPORT:
                    {
                        if (LotReportDirectory.Equals(string.Empty))
                        {
                            LotReportDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "LOT_REPORT");
                        }
                        dir = Path.Combine(LotReportDirectory, this.OEE_DATA.LotInfo.Mode);
                        file_name = string.Format("{0}_{1}.txt", OEE_DATA.LotInfo.LotNo, OEE_DATA.LotInfo.StartTime.ToString("yyyyMMddHHmmss"));
                    }
                    break;
                case FILE_PATH_TYPE.LOT_REPORT_EXCEL:
                    {
                        if (LotReportDirectory.Equals(string.Empty))
                        {
                            LotReportDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "LOT_REPORT");
                        }
                        dir = Path.Combine(LotReportDirectory, this.OEE_DATA.LotInfo.Mode);
                        file_name = string.Format("{0}_{1}.xls", OEE_DATA.LotInfo.LotNo, OEE_DATA.LotInfo.StartTime.ToString("yyyyMMddHHmmss"));
                    }
                    break;
            }
            if (Path.HasExtension(dir))
            {
                dir = Path.GetDirectoryName(dir);
            }
            dir = Path.GetFullPath(dir);
            if (Directory.Exists(dir).Equals(false))
            {
                Directory.CreateDirectory(dir);
            }
            string file_path = Path.Combine(dir, file_name);
            return file_path;
        }

        /// <summary>
        /// 更新 OEE DATA (XML File)
        /// </summary>
        private void UpdateOEEData()
        {
            lock (OEE_LOCK)
            {
                string file_path = GetFilePath(FILE_PATH_TYPE.OEE);
                ///Write to XML file
                string xml = XmlObjectSerializer.Serialize<JOEE>(OEE_DATA);
                File.WriteAllText(file_path, xml);
            }
        }

        /// <summary>
        /// 產生 Lot Report
        /// </summary>
        private void GenLotReport()
        {
            string file_path = GetFilePath(FILE_PATH_TYPE.LOT_REPORT);
            using (FileStream fs = new FileStream(file_path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    lock (OEE_LOCK)
                    {
                        #region 開始寫入 Lot Report 內容

                        //Machine Info
                        //sw.WriteLine("[Machine Info]");
                        //sw.WriteLine("Equiment_ID={0}", OEE_DATA.LotInfo.EquimentID);
                        //sw.WriteLine("Software_Version={0}", OEE_DATA.LotInfo.Version);

                        //sw.WriteLine("");
                        //Lot Info
                        sw.WriteLine("[Lot Info]");
                        sw.WriteLine("Lot_Number={0}", OEE_DATA.LotInfo.LotNo);
                        sw.WriteLine("OPID={0}", OEE_DATA.LotInfo.OPID);
                        sw.WriteLine("DiePak_Type={0}", OEE_DATA.LotInfo.CarrierType);
                        sw.WriteLine("Device_Type={0}", OEE_DATA.LotInfo.DeviceType);
                        sw.WriteLine("Test_Program={0}", OEE_DATA.LotInfo.TestProgram);
                        sw.WriteLine("Mode={0}", OEE_DATA.LotInfo.Mode);
                        sw.WriteLine("Recipe={0}", OEE_DATA.LotInfo.Recipe);
                        sw.WriteLine("Lot_StartTime={0:yyyy/MM/dd HH:mm:ss}", OEE_DATA.LotInfo.StartTime);
                        sw.WriteLine("Lot_EndTime={0:yyyy/MM/dd HH:mm:ss}", OEE_DATA.LotInfo.EndTime);
                        sw.WriteLine("Lot_Qty={0}", OEE_DATA.LotInfo.LotQty);

                        //計算 Load Qty
                        JBINQTY LoadBinQty = GetBinQty(BinDefine.Untested);
                        sw.WriteLine("LoadProcess_Qty={0}", LoadBinQty.Process);
                        sw.WriteLine("LoadCompleted_Qty={0}", LoadBinQty.Completed);
                        sw.WriteLine("LoadMissing_Qty={0}", LoadBinQty.Missing);

                        //計算 Unload Qty
                        JBINQTY UnloadBinQty = GetBinQty(BinDefine.Tested);
                        sw.WriteLine("UnloadProcess_Qty={0}", UnloadBinQty.Process);
                        sw.WriteLine("UnloadCompleted_Qty={0}", UnloadBinQty.Completed);
                        sw.WriteLine("UnloadMissing_Qty={0}", UnloadBinQty.Missing);

                        //計算 Bin1 Qty
                        JBINQTY Bin1Qty = GetBinQty(BinDefine.Bin1);
                        if (OEE_DATA.LotInfo.Mode.Equals("UNLOAD"))
                        {
                            sw.WriteLine("Bin1_Qty={0}", Bin1Qty.Completed);
                        }
                        else
                        {
                            sw.WriteLine("Bin1_Qty={0}", LoadBinQty.Completed);
                        }

                        //計算 Bin2 Qty
                        JBINQTY Bin2Qty = GetBinQty(BinDefine.Bin2);
                        sw.WriteLine("Bin2_Qty={0}", Bin2Qty.Completed);

                        //計算 Bin3 Qty
                        JBINQTY Bin3Qty = GetBinQty(BinDefine.Bin3);
                        sw.WriteLine("Bin3_Qty={0}", Bin3Qty.Completed);

                        //計算 Bin4 Qty
                        JBINQTY Bin4Qty = GetBinQty(BinDefine.Bin4);
                        sw.WriteLine("Bin4_Qty={0}", Bin4Qty.Completed);

                        //計算 Bin5 Qty
                        JBINQTY Bin5Qty = GetBinQty(BinDefine.Bin5);
                        sw.WriteLine("Bin5_Qty={0}", Bin5Qty.Completed);

                        ////計算 Bin6 Qty
                        //JBINQTY Bin6Qty = GetBinQty(BinDefine.Bin6);
                        //sw.WriteLine("Bin6_Qty={0}", Bin6Qty.Completed);

                        ////計算 Bin7 Qty
                        //JBINQTY Bin7Qty = GetBinQty(BinDefine.Bin7);
                        //sw.WriteLine("Bin7_Qty={0}", Bin7Qty.Completed);

                        ////計算 Bin8 Qty
                        //JBINQTY Bin8Qty = GetBinQty(BinDefine.Bin8);
                        //sw.WriteLine("Bin8_Qty={0}", Bin8Qty.Completed);
                        ////計算 Bin8 Qty
                        //JBINQTY Bin9Qty = GetBinQty(BinDefine.Bin9);
                        //sw.WriteLine("Bin9_Qty={0}", Bin9Qty.Completed);
                        //sw.WriteLine("");
                        sw.WriteLine("Total_DiePak={0}", OEE_DATA.DiePakList.Count);
                        //sw.WriteLine("");
                        //sw.WriteLine("Run_Time={0}", TimeSpan.FromSeconds(OEE_DATA.LotInfo.RunTime).ToString(@"hh\:mm\:ss"));
                        //sw.WriteLine("Stop_Time={0}", TimeSpan.FromSeconds(OEE_DATA.LotInfo.StopTime).ToString(@"hh\:mm\:ss"));
                        //sw.WriteLine("Alarm_Time={0}", TimeSpan.FromSeconds(OEE_DATA.LotInfo.AlarmTime).ToString(@"hh\:mm\:ss"));

                        ////計算故障排除時間
                        //int TotalTime = (int)((OEE_DATA.LotInfo.EndTime - OEE_DATA.LotInfo.StartTime).TotalSeconds);
                        //int RepairTime = (TotalTime - OEE_DATA.LotInfo.RunTime - OEE_DATA.LotInfo.StopTime - OEE_DATA.LotInfo.AlarmTime);
                        //sw.WriteLine("Repair_Time={0}", TimeSpan.FromSeconds(RepairTime).ToString(@"hh\:mm\:ss"));

                        //計算 UPH
                        //int CompletedQty = (LoadBinQty.Completed + UnloadBinQty.Completed);
                        //if (OEE_DATA.LotInfo.RunTime > 0)
                        //{
                        //    sw.WriteLine("UPH(K/H)={0:0.0}", ((CompletedQty * 3.6) / OEE_DATA.LotInfo.RunTime));
                        //}
                        //else
                        //{
                        //    sw.WriteLine("UPH(K/H)=0");
                        //}


                        //計算 MUBA (CompletedQty/AlarmCount)
                        //if (OEE_DATA.AlarmList.Count > 0)
                        //{
                        //    sw.WriteLine("MUBA(pcs)={0}", (CompletedQty / OEE_DATA.AlarmList.Count));
                        //}
                        //else
                        //{
                        //    sw.WriteLine("MUBA(pcs)={0}", CompletedQty);
                        //}

                        //機算 MTBA (RunTime/AlarmCount)
                        //if (OEE_DATA.AlarmList.Count > 0)
                        //{
                        //    sw.WriteLine("MTBA(sec)={0}", (OEE_DATA.LotInfo.RunTime / OEE_DATA.AlarmList.Count));
                        //}
                        //else
                        //{
                        //    sw.WriteLine("MTBA(sec)={0}", OEE_DATA.LotInfo.RunTime);
                        //}

                        sw.WriteLine("");

                        //DiePak Info
                        sw.WriteLine("[DiePak Info]");
                        foreach (JCARRIER DP in OEE_DATA.DiePakList)
                        {
                            sw.WriteLine("DiePak_ID={0}", DP.ID);
                            sw.WriteLine("DiePak_StartTime={0:yyyy/MM/dd HH:mm:ss}", DP.StartTime);
                            sw.WriteLine("DiePak_EndTime={0:yyyy/MM/dd HH:mm:ss}", DP.EndTime);
                            sw.WriteLine("DeiPak_CostTime={0}", TimeSpan.FromSeconds((int)((DP.EndTime - DP.StartTime).TotalSeconds)).ToString(@"hh\:mm\:ss"));
                            if (OEE_DATA.LotInfo.Mode.Equals("UNLOAD"))
                            {
                                sw.WriteLine("DiePak_Empty={0}", DP.ReadMap.Count(x => x.Equals('0')));
                                for (int i = 1; i <= 5; i++)
                                {
                                    sw.WriteLine("DiePak_Bin{0}={1}", i, DP.ReadMap.Count(x => x.Equals((char)(i + 48))));
                                }
                            }
                            else
                            {
                                sw.WriteLine("DiePak_Empty={0}", DP.WriteMap.Count(x => x.Equals('0')));
                                for (int i = 1; i <= 5; i++)
                                {
                                    sw.WriteLine("DiePak_Bin{0}={1}", i, DP.WriteMap.Count(x => x.Equals((char)(i + 48))));
                                }
                            }
                            //sw.WriteLine("DiePak_ReadMap=[{0}]", DP.ReadMap);
                            //sw.WriteLine("DiePak_WriteMap=[{0}]", DP.WriteMap);
                            //sw.WriteLine("");
                        }
                        //sw.WriteLine("");
                        //sw.WriteLine("Alarm_Count={0}", OEE_DATA.AlarmList.Count);
                        //////Alarm List
                        //sw.WriteLine("[Alarm List]");
                        //foreach (JALM alarm in OEE_DATA.AlarmList)
                        //{
                        //    sw.WriteLine("Alarm_Code={0}", alarm.AlarmCode);
                        //    sw.WriteLine("Alarm_Module={0}", alarm.AlarmModule);
                        //    sw.WriteLine("Alarm_Message={0}", alarm.AlarmMessage);
                        //    sw.WriteLine("Alarm_Time={0}", TimeSpan.FromSeconds((int)(alarm.AlarmTime / 1000)).ToString(@"hh\:mm\:ss"));
                        //    sw.WriteLine("Alarm_StartTime={0:yyyy/MM/dd HH:mm:ss}", alarm.StartTime);
                        //    sw.WriteLine("Alarm_EndTime={0:yyyy/MM/dd HH:mm:ss}", alarm.EndTime);
                        //    JCARRIER dp = OEE_DATA.DiePakList.Find(x => x.Index.Equals(alarm.CarrierStartDateTime));
                        //    sw.WriteLine("");
                        //    // sw.WriteLine("Alarm_DiePak_ID={0}", (dp != null ? dp.ID : ""));
                        //}
                        //sw.WriteLine("");

                        //Device ID
                        //sw.WriteLine("[Device ID List]");
                        //foreach (string id in OEE_DATA.DeviceIDList)
                        //{
                        //    sw.WriteLine(id);
                        //}
                        //sw.WriteLine("");

                        #endregion
                    }

                    //清空緩衝區
                    sw.Flush();
                    //關閉流
                    sw.Close();
                    fs.Close();
                }
            }
        }


        /// <summary>
        /// 產生 Excel Lot Report
        /// </summary>
        //private void GenLotReportForExcel()
        //{
        //    return;
        //    string file_path = GetFilePath(FILE_PATH_TYPE.LOT_REPORT_EXCEL);
        //    Excel.Application Excel_AP = new Excel.Application();
        //    /// Excel_AP.Worksheets.Add();
        //    Excel.Workbook Excel_WB = Excel_AP.Workbooks.Add();
        //    Excel.Worksheet Excel_WS1 = new Excel.Worksheet();
        //    Excel_WS1 = Excel_WB.Worksheets[1];
        //    Excel_WS1.Name = "LotInfo";
        //    //計算 Load Qty
        //    JBINQTY LoadBinQty = GetBinQty(BinDefine.Untested);


        //    //計算 Unload Qty
        //    JBINQTY UnloadBinQty = GetBinQty(BinDefine.Tested);

        //    //計算 Bin1 Qty
        //    JBINQTY Bin1Qty = GetBinQty(BinDefine.Bin1);


        //    //計算 Bin2 Qty
        //    JBINQTY Bin2Qty = GetBinQty(BinDefine.Bin2);

        //    //計算 Bin3 Qty
        //    JBINQTY Bin3Qty = GetBinQty(BinDefine.Bin3);


        //    //計算 Bin4 Qty
        //    JBINQTY Bin4Qty = GetBinQty(BinDefine.Bin4);

        //    //計算 Bin5 Qty
        //    JBINQTY Bin5Qty = GetBinQty(BinDefine.Bin5);

        //    //計算 Bin6 Qty
        //    JBINQTY Bin6Qty = GetBinQty(BinDefine.Bin6);

        //    //計算 Bin7 Qty
        //    JBINQTY Bin7Qty = GetBinQty(BinDefine.Bin7);

        //    //計算 Bin8 Qty
        //    JBINQTY Bin8Qty = GetBinQty(BinDefine.Bin8);
        //    //計算 Bin8 Qty
        //    JBINQTY Bin9Qty = GetBinQty(BinDefine.Bin9);
        //    string runtime = TimeSpan.FromSeconds(OEE_DATA.LotInfo.RunTime).ToString(@"hh\:mm\:ss");
        //    string stoptime = TimeSpan.FromSeconds(OEE_DATA.LotInfo.StopTime).ToString(@"hh\:mm\:ss");
        //    //sw.WriteLine("Alarm_Time={0}", TimeSpan.FromSeconds(OEE_DATA.LotInfo.AlarmTime).ToString(@"hh\:mm\:ss"));

        //    ////計算故障排除時間
        //    //int TotalTime = (int)((OEE_DATA.LotInfo.EndTime - OEE_DATA.LotInfo.StartTime).TotalSeconds);
        //    //int RepairTime = (TotalTime - OEE_DATA.LotInfo.RunTime - OEE_DATA.LotInfo.StopTime - OEE_DATA.LotInfo.AlarmTime);
        //    //sw.WriteLine("Repair_Time={0}", TimeSpan.FromSeconds(RepairTime).ToString(@"hh\:mm\:ss"));

        //    //計算 UPH
        //    int CompletedQty = (LoadBinQty.Completed + UnloadBinQty.Completed);
        //    string uph = "0";
        //    if (OEE_DATA.LotInfo.RunTime > 0)
        //    {
        //        uph = string.Format("{0:0.0}", ((CompletedQty * 3.6) / OEE_DATA.LotInfo.RunTime));
        //    }



        //    string title = "Equiment_ID,Software_Version,Lot_Number,OPID,DiePak_Type,Device_Type,Test_Program,Mode,Recipe,Lot_StartTime,Lot_EndTime,Lot_Qty,LoadProcess_Qty,LoadCompleted_Qty,LoadMissing_Qty,UnloadProcess_Qty,UnloadCompleted_Qty,UnloadMissing_Qty,Bin1_Qty,Bin2_Qty,Bin3_Qty,Bin4_Qty,Bin5_Qty,Bin6_Qty,Bin7_Qty,Bin8_Qty,Bin9_Qty,Total_DiePak,Run_Time,Stop_Time,UPH(K/H)";
        //    string col = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30}",
        //                                OEE_DATA.LotInfo.EquimentID, OEE_DATA.LotInfo.Version, OEE_DATA.LotInfo.LotNo, OEE_DATA.LotInfo.OPID, OEE_DATA.LotInfo.CarrierType, OEE_DATA.LotInfo.DeviceType, OEE_DATA.LotInfo.TestProgram, OEE_DATA.LotInfo.Mode,
        //                                OEE_DATA.LotInfo.Recipe, OEE_DATA.LotInfo.StartTime, OEE_DATA.LotInfo.EndTime, OEE_DATA.LotInfo.LotQty, LoadBinQty.Process, LoadBinQty.Completed, LoadBinQty.Missing, UnloadBinQty.Process, UnloadBinQty.Completed, UnloadBinQty.Missing, Bin1Qty.Completed, Bin2Qty.Completed, Bin3Qty.Completed, Bin4Qty.Completed, Bin5Qty.Completed, Bin6Qty.Completed, Bin7Qty.Completed, Bin8Qty.Completed, Bin9Qty.Completed
        //                                , OEE_DATA.DiePakList.Count, runtime, stoptime, uph);
        //    String[] linetitle = title.Split(',');
        //    for (int i = 0; i < linetitle.Count(); i++)
        //    {
        //        Excel_WS1.Cells[1, i + 1] = linetitle[i];
        //    }
        //    String[] lineCol = col.Split(',');
        //    for (int i = 0; i < lineCol.Count(); i++)
        //    {
        //        Excel_WS1.Cells[2, i + 1] = lineCol[i];
        //    }

        //    Excel.Range usedrange = Excel_WS1.UsedRange;
        //    usedrange.Columns.AutoFit();

        //    Excel.Worksheet Excel_WS2 = new Excel.Worksheet();
        //    Excel_WS2 = Excel_WB.Worksheets[2];
        //    Excel_WS2.Name = "DiepakInfo";
        //    string title2 = "DiePak_ID,DiePak_StartTime,DiePak_EndTime,DeiPak_CostTime,DiePak_Empty";
        //    for (int i = 1; i <= 9; i++)
        //    {
        //        title2 += string.Format(",DiePak_Bin{0}", i);
        //    }
        //    title2 += ",DiePak_ReadMap,DiePak_WriteMap";
        //    String[] linetitle2 = title2.Split(',');
        //    for (int i = 0; i < linetitle2.Count(); i++)
        //    {
        //        Excel_WS2.Cells[1, i + 1] = linetitle2[i];
        //    }
        //    int diepaklist = 1;
        //    foreach (JCARRIER DP in OEE_DATA.DiePakList)
        //    {
        //        string col2 = "";
        //        col2 += string.Format("{0}", DP.ID) + ",";
        //        col2 += string.Format("{0:yyyy/MM/dd HH:mm:ss}", DP.StartTime) + ",";
        //        col2 += string.Format("{0:yyyy/MM/dd HH:mm:ss}", DP.EndTime) + ",";
        //        col2 += string.Format("{0}", TimeSpan.FromSeconds((int)((DP.EndTime - DP.StartTime).TotalSeconds)).ToString(@"hh\:mm\:ss")) + ",";
        //        col2 += string.Format("{0}", DP.ReadMap.Count(x => x.Equals('0'))) + ",";
        //        for (int i = 1; i <= 9; i++)
        //        {
        //            col2 += string.Format("{0}", DP.ReadMap.Count(x => x.Equals((char)(i + 48))));
        //            col2 += ",";
        //        }
        //        String[] lineCol2 = col2.Split(',');
        //        for (int i = 0; i < lineCol2.Count(); i++)
        //        {
        //            Excel_WS2.Cells[diepaklist + 1, i + 1] = lineCol2[i];
        //        }
        //        Excel_WS2.Cells[diepaklist + 1, lineCol2.Count()] = string.Format("{0}", DP.ReadMap);
        //        Excel_WS2.Cells[diepaklist + 1, lineCol2.Count() + 1] = string.Format("{0}", DP.WriteMap);
        //        diepaklist++;
        //    }

        //    Excel.Range usedrange2 = Excel_WS2.UsedRange;
        //    usedrange2.Columns.AutoFit();


        //    Excel.Worksheet Excel_WS3 = new Excel.Worksheet();
        //    Excel_WS3 = Excel_WB.Worksheets[3];
        //    Excel_WS3.Name = "AlarmList";
        //    string title3 = "Alarm_Code,Alarm_Module,Alarm_Message,Alarm_Time,Alarm_StartTime,Alarm_EndTime";
        //    String[] linetitle3 = title3.Split(',');
        //    for (int i = 0; i < linetitle3.Count(); i++)
        //    {
        //        Excel_WS3.Cells[1, i + 1] = linetitle3[i];
        //    }
        //    int Alarmlist = 1;

        //    foreach (JALM alarm in OEE_DATA.AlarmList)
        //    {
        //        string col3 = "";
        //        col3 += string.Format("Alarm_Code={0}", alarm.AlarmCode) + ",";
        //        col3 += string.Format("Alarm_Module={0}", alarm.AlarmModule) + ",";
        //        col3 += string.Format("Alarm_Message={0}", alarm.AlarmMessage) + ",";
        //        col3 += string.Format("Alarm_Time={0}", TimeSpan.FromSeconds((int)(alarm.AlarmTime / 1000)).ToString(@"hh\:mm\:ss")) + ",";
        //        col3 += string.Format("Alarm_StartTime={0:yyyy/MM/dd HH:mm:ss}", alarm.StartTime) + ",";
        //        col3 += string.Format("Alarm_EndTime={0:yyyy/MM/dd HH:mm:ss}", alarm.EndTime);
        //        String[] lineCol3 = col3.Split(',');
        //        for (int i = 0; i < lineCol3.Count(); i++)
        //        {
        //            Excel_WS3.Cells[Alarmlist + 1, i + 1] = lineCol3[i];
        //        }
        //        Alarmlist++;
        //    }

        //    Excel.Range usedrange3 = Excel_WS3.UsedRange;
        //    usedrange3.Columns.AutoFit();

        //    int IDLIST = 1;
        //    Excel.Worksheet Excel_WS4 = new Excel.Worksheet();
        //    Excel_WS4 = Excel_WB.Worksheets.Add(After: Excel_WB.Worksheets[Excel_WB.Sheets.Count]);
        //    Excel_WS4.Name = "2DIDList";
        //    string title4 = "2DID";
        //    Excel_WS4.Cells[1, 1] = title4;
        //    foreach (string id in OEE_DATA.DeviceIDList)
        //    {
        //        Excel_WS4.Cells[IDLIST + 1, 1] = id;
        //        IDLIST++;
        //    }
        //    Excel.Range usedrange4 = Excel_WS4.UsedRange;
        //    usedrange4.Columns.AutoFit();

        //    Excel_WB.SaveAs(file_path);
        //    Excel_WS1 = null;
        //    Excel_WS2 = null;
        //    Excel_WS3 = null;
        //    Excel_WS4 = null;
        //    Excel_WB.Close();
        //    Excel_WB = null;
        //    Excel_AP.Quit();
        //    Excel_AP = null;
        //}
        /// <summary>
        /// 產生 Excel Lot Report
        /// </summary>
        private void GenLotReportForExcel2()
        {
            string file_path = GetFilePath(FILE_PATH_TYPE.LOT_REPORT_EXCEL);

            var workbook = new HSSFWorkbook();
            var sheetReportResult = workbook.CreateSheet("LotInfo");
            //計算 Load Qty
            JBINQTY LoadBinQty = GetBinQty(BinDefine.Untested);


            //計算 Unload Qty
            JBINQTY UnloadBinQty = GetBinQty(BinDefine.Tested);

            //計算 Bin1 Qty

            JBINQTY Bin1Qty = GetBinQty(BinDefine.Bin1);


            //計算 Bin2 Qty
            JBINQTY Bin2Qty = GetBinQty(BinDefine.Bin2);

            //計算 Bin3 Qty
            JBINQTY Bin3Qty = GetBinQty(BinDefine.Bin3);


            //計算 Bin4 Qty
            JBINQTY Bin4Qty = GetBinQty(BinDefine.Bin4);

            //計算 Bin5 Qty
            JBINQTY Bin5Qty = GetBinQty(BinDefine.Bin5);

            //計算 Bin6 Qty
            JBINQTY Bin6Qty = GetBinQty(BinDefine.Bin6);

            //計算 Bin7 Qty
            JBINQTY Bin7Qty = GetBinQty(BinDefine.Bin7);

            //計算 Bin8 Qty
            JBINQTY Bin8Qty = GetBinQty(BinDefine.Bin8);
            //計算 Bin8 Qty
            JBINQTY Bin9Qty = GetBinQty(BinDefine.Bin9);
            string runtime = TimeSpan.FromSeconds(OEE_DATA.LotInfo.RunTime).ToString(@"hh\:mm\:ss");
            string stoptime = TimeSpan.FromSeconds(OEE_DATA.LotInfo.StopTime).ToString(@"hh\:mm\:ss");
            //sw.WriteLine("Alarm_Time={0}", TimeSpan.FromSeconds(OEE_DATA.LotInfo.AlarmTime).ToString(@"hh\:mm\:ss"));

            ////計算故障排除時間
            //int TotalTime = (int)((OEE_DATA.LotInfo.EndTime - OEE_DATA.LotInfo.StartTime).TotalSeconds);
            //int RepairTime = (TotalTime - OEE_DATA.LotInfo.RunTime - OEE_DATA.LotInfo.StopTime - OEE_DATA.LotInfo.AlarmTime);
            //sw.WriteLine("Repair_Time={0}", TimeSpan.FromSeconds(RepairTime).ToString(@"hh\:mm\:ss"));

            //計算 UPH
            int CompletedQty = (LoadBinQty.Completed + UnloadBinQty.Completed);
            string uph = "0";
            if (OEE_DATA.LotInfo.RunTime > 0)
            {
                uph = string.Format("{0:0.00}", ((CompletedQty * 3.6) / OEE_DATA.LotInfo.RunTime));
            }
            // 計算 MUBA (CompletedQty/AlarmCount)
            int MUBA;
            if (OEE_DATA.AlarmList.Count > 0 && CompletedQty > 0)
            {
                MUBA = (CompletedQty / OEE_DATA.AlarmList.Count);
            }
            else
            {
                MUBA = CompletedQty;
            }

            // MTBA (RunTime/AlarmCount)
            int MTBA;

            if (OEE_DATA.AlarmList.Count > 0)
            {
                MTBA = (OEE_DATA.LotInfo.RunTime / OEE_DATA.AlarmList.Count);
            }
            else
            {
                MTBA = OEE_DATA.LotInfo.RunTime;
            }

            int Bin1 = 0;
            if (OEE_DATA.LotInfo.Mode.Equals("UNLOAD"))
            {
                Bin1 = Bin1Qty.Completed;
            }
            else
            {
                Bin1 = LoadBinQty.Completed;
            }
            double mins_MTBA = Math.Round((double)MTBA / 60, 2);
            string title = "Equiment_ID,Software_Version,Lot_Number,OPID,DiePak_Type,Device_Type,Test_Program,Mode,Recipe,Lot_StartTime,Lot_EndTime,Lot_Qty,LoadProcess_Qty,LoadCompleted_Qty,LoadMissing_Qty,UnloadProcess_Qty,UnloadCompleted_Qty,UnloadMissing_Qty,Bin1_Qty,Bin2_Qty,Bin3_Qty,Bin4_Qty,Bin5_Qty,Bin6_Qty,Bin7_Qty,Bin8_Qty,Bin9_Qty,Total_DiePak,Run_Time,Stop_Time,UPH(K/H),MUBA,MTBA";
            string col = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32}",
                                        OEE_DATA.LotInfo.EquimentID, OEE_DATA.LotInfo.Version, OEE_DATA.LotInfo.LotNo, OEE_DATA.LotInfo.OPID, OEE_DATA.LotInfo.CarrierType, OEE_DATA.LotInfo.DeviceType, OEE_DATA.LotInfo.TestProgram, OEE_DATA.LotInfo.Mode,
                                        OEE_DATA.LotInfo.Recipe, OEE_DATA.LotInfo.StartTime, OEE_DATA.LotInfo.EndTime, OEE_DATA.LotInfo.LotQty, LoadBinQty.Process, LoadBinQty.Completed, LoadBinQty.Missing, UnloadBinQty.Process, UnloadBinQty.Completed, UnloadBinQty.Missing, Bin1, Bin2Qty.Completed, Bin3Qty.Completed, Bin4Qty.Completed, Bin5Qty.Completed, Bin6Qty.Completed, Bin7Qty.Completed, Bin8Qty.Completed, Bin9Qty.Completed
                                        , OEE_DATA.DiePakList.Count, runtime, stoptime, uph, MUBA + "(pcs)", mins_MTBA + "(mins)");
            String[] linetitle = title.Split(',');
            int row_index = 0;
            sheetReportResult.CreateRow(row_index);
            for (int i = 0; i < linetitle.Count(); i++)
            {
                //產生第一個要用CreateRow 
                sheetReportResult.GetRow(row_index).CreateCell(i).SetCellValue(linetitle[i]);
            }
            row_index = 1;
            sheetReportResult.CreateRow(row_index);
            String[] lineCol = col.Split(',');
            for (int i = 0; i < lineCol.Count(); i++)
            {
                sheetReportResult.GetRow(row_index).CreateCell(i).SetCellValue(lineCol[i]);
                sheetReportResult.AutoSizeColumn(i);
            }

            var sheetReportResult2 = workbook.CreateSheet("DiepakInfo");
            row_index = 0;
            sheetReportResult2.CreateRow(row_index);
            string title2 = "DiePak_ID,DiePak_StartTime,DiePak_EndTime,DeiPak_CostTime,DiePak_Empty";
            for (int i = 1; i <= 9; i++)
            {
                title2 += string.Format(",DiePak_Bin{0}", i);
            }
            title2 += ",DiePak_ReadMap,DiePak_WriteMap";
            String[] linetitle2 = title2.Split(',');
            for (int i = 0; i < linetitle2.Count(); i++)
            {
                sheetReportResult2.GetRow(row_index).CreateCell(i).SetCellValue(linetitle2[i]);
            }
            int diepaklist = 1;
            foreach (JCARRIER DP in OEE_DATA.DiePakList)
            {
                string col2 = "";
                col2 += string.Format("{0}", DP.ID) + ",";
                col2 += string.Format("{0:yyyy/MM/dd HH:mm:ss}", DP.StartTime) + ",";
                col2 += string.Format("{0:yyyy/MM/dd HH:mm:ss}", DP.EndTime) + ",";
                col2 += string.Format("{0}", TimeSpan.FromSeconds((int)((DP.EndTime - DP.StartTime).TotalSeconds)).ToString(@"hh\:mm\:ss")) + ",";
                if (OEE_DATA.LotInfo.Mode.Equals("UNLOAD"))
                {
                    col2 += string.Format("{0}", DP.ReadMap.Count(x => x.Equals('0'))) + ",";
                    for (int i = 1; i <= 9; i++)
                    {
                        col2 += string.Format("{0}", DP.ReadMap.Count(x => x.Equals((char)(i + 48))));
                        col2 += ",";
                    }
                }
                else
                {
                    col2 += string.Format("{0}", DP.WriteMap.Count(x => x.Equals('0'))) + ",";
                    for (int i = 1; i <= 9; i++)
                    {
                        col2 += string.Format("{0}", DP.WriteMap.Count(x => x.Equals((char)(i + 48))));
                        col2 += ",";
                    }
                }
                sheetReportResult2.CreateRow(diepaklist);
                String[] lineCol2 = col2.Split(',');
                for (int i = 0; i < lineCol2.Count(); i++)
                {
                    sheetReportResult2.GetRow(diepaklist).CreateCell(i).SetCellValue(lineCol2[i]);
                    sheetReportResult2.AutoSizeColumn(i);
                }
                sheetReportResult2.GetRow(diepaklist).CreateCell(lineCol2.Count()).SetCellValue(string.Format("{0}", DP.ReadMap));
                sheetReportResult2.AutoSizeColumn(lineCol2.Count());
                sheetReportResult2.GetRow(diepaklist).CreateCell(lineCol2.Count() + 1).SetCellValue(string.Format("{0}", DP.WriteMap));
                sheetReportResult2.AutoSizeColumn(lineCol2.Count() + 1);
                diepaklist++;

            }

            var sheetReportResult3 = workbook.CreateSheet("AlarmList");
            string title3 = "Alarm_Code,Alarm_Module,Alarm_Message,Alarm_Time,Alarm_StartTime,Alarm_EndTime";
            String[] linetitle3 = title3.Split(',');
            row_index = 0;
            sheetReportResult3.CreateRow(row_index);
            for (int i = 0; i < linetitle3.Count(); i++)
            {
                sheetReportResult3.GetRow(row_index).CreateCell(i).SetCellValue(linetitle3[i]);
            }
            int Alarmlist = 1;
            foreach (JALM alarm in OEE_DATA.AlarmList)
            {
                sheetReportResult3.CreateRow(Alarmlist);
                string col3 = "";
                col3 += string.Format("{0}", alarm.AlarmCode) + ";";
                col3 += string.Format("{0}", alarm.AlarmModule) + ";";
                col3 += string.Format("{0}", alarm.AlarmMessage) + ";";
                col3 += string.Format("{0}", TimeSpan.FromSeconds((int)(alarm.AlarmTime / 1000)).ToString(@"hh\:mm\:ss")) + ";";
                col3 += string.Format("{0:yyyy/MM/dd HH:mm:ss}", alarm.StartTime) + ";";
                col3 += string.Format("{0:yyyy/MM/dd HH:mm:ss}", alarm.EndTime);
                String[] lineCol3 = col3.Split(';');
                for (int i = 0; i < lineCol3.Count(); i++)
                {
                    sheetReportResult3.GetRow(Alarmlist).CreateCell(i).SetCellValue(lineCol3[i]);
                    //sheetReportResult3.AutoSizeColumn(i);
                }
                Alarmlist++;
            }




            int IDLIST = 1;
            var sheetReportResult4 = workbook.CreateSheet("2DIDList");
            string title4 = "DiepakID,BlockX,BlockY,X,Y,BIN,ID";
            String[] linetitle4 = title4.Split(',');
            row_index = 0;
            sheetReportResult4.CreateRow(row_index);
            for (int i = 0; i < linetitle4.Count(); i++)
            {
                sheetReportResult4.GetRow(row_index).CreateCell(i).SetCellValue(linetitle4[i]);
            }
            //string title4 = "2DID";
            //row_index = 0;
            //sheetReportResult4.CreateRow(row_index);
            //sheetReportResult4.GetRow(row_index).CreateCell(0).SetCellValue(title4);
            for (int i = 0; i < OEE_DATA.ICIDList.Count; i++)
            {
                sheetReportResult4.CreateRow(IDLIST);
                string col4 = "";
                col4 += string.Format("{0}", OEE_DATA.ICIDList[i].DieapkID) + ",";
                col4 += string.Format("{0}", OEE_DATA.ICIDList[i].BLK_X) + ",";
                col4 += string.Format("{0}", OEE_DATA.ICIDList[i].BLK_Y) + ",";
                col4 += string.Format("{0}", OEE_DATA.ICIDList[i].XN) + ",";
                col4 += string.Format("{0}", OEE_DATA.ICIDList[i].YN) + ",";
                col4 += string.Format("{0}", OEE_DATA.ICIDList[i].Bin) + ",";
                col4 += string.Format("{0}", OEE_DATA.ICIDList[i].ID);
                String[] lineCol4 = col4.Split(',');
                for (int j = 0; j < lineCol4.Count(); j++)
                {
                    sheetReportResult4.GetRow(IDLIST).CreateCell(j).SetCellValue(lineCol4[j]);
                    // sheetReportResult4.AutoSizeColumn(j);
                }
                //sheetReportResult4.CreateRow(IDLIST);
                //sheetReportResult4.GetRow(IDLIST).CreateCell(0).SetCellValue(id);
                IDLIST++;
            }
            //foreach (string id in OEE_DATA.DeviceIDList)
            //{
            //    sheetReportResult4.CreateRow(IDLIST);
            //    sheetReportResult4.GetRow(IDLIST).CreateCell(0).SetCellValue(id);
            //    IDLIST++;
            //}

            var file = new FileStream(file_path, FileMode.Create);
            workbook.Write(file);
            file.Close();
        }
    }

    /// <summary>
    /// Overall Equipment Effectiveness class structure (OEE 整體設備效能統計資料 類別結構)
    /// </summary>
    public class JOEE
    {

        public JEAP LotInfo { get; set; }
        public List<JCARRIER> DiePakList { get; set; }
        public List<JALM> AlarmList { get; set; }
        public List<string> DeviceIDList { get; set; }
        public List<ICID> ICIDList { get; set; }
        public JOEE()
        {
            LotInfo = new JEAP();
            DiePakList = new List<JCARRIER>();
            AlarmList = new List<JALM>();
            DeviceIDList = new List<string>();
            ICIDList = new List<ICID>();
        }

        public void Reset()
        {
            LotInfo.Reset();
            DiePakList.Clear();
            AlarmList.Clear();
            DeviceIDList.Clear();
            ICIDList.Clear();
        }
    }

    /// <summary>
    /// Equipment Availability and Performance class (EAP 產能與稼動資料 類別)
    /// </summary>
    public class JEAP
    {
        public double Index { get; set; }      //CreateDateTime
        public string EquimentID { get; set; }
        public string LotNo { get; set; }
        public string OPID { get; set; }
        public string CarrierType { get; set; }
        public string DeviceType { get; set; }
        public string TestProgram { get; set; }
        public string Mode { get; set; }
        public string Recipe { get; set; }
        public string Version { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RunTime { get; set; }
        public int StopTime { get; set; }
        public int AlarmTime { get; set; }
        //public int RepairTime { get { return Math.Max(((EndTime - StartTime).Seconds - RunTime - StopTime - AlarmTime), 0); } }
        public int LotQty { get; set; }     //開批時輸入的數量
        //public int ProcessQty { get; set; }         //生產實際投料數量
        public List<JBIN> BinQtyList { get; set; }  //生產實際各Bin產出數量

        public JEAP()
        {
            BinQtyList = new List<JBIN>();
            Reset();
        }

        public void Reset()
        {
            Index = 0;
            EquimentID = "";
            LotNo = "";
            OPID = "";
            CarrierType = "";
            DeviceType = "";
            TestProgram = "";
            Mode = "";
            Recipe = "";
            Version = "";
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MinValue;
            RunTime = 0;
            StopTime = 0;
            AlarmTime = 0;
            LotQty = 0;
            BinQtyList.Clear();
        }
        public string StartTimeStr
        {
            get
            {
                return StartTime.ToString("yyyyMMddHHmmss");
            }
        }
    }

    public class JBIN
    {
        public BinDefine Type { get; set; }
        public JBINQTY Qty { get; set; }
    }

    public class JBINQTY
    {
        public int Process { get; set; }
        public int Completed { get; set; }
        public int Missing { get; set; }
    }

    /// <summary>
    /// 載具資料類別
    /// </summary>
    public class JCARRIER
    {
        public double Index { get; set; }      //StartDateTime
        public double LotCreateDateTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ID { get; set; }
        //public string Type { get; set; }
        //public int BlockXN { get; set; }
        //public int BlockYN { get; set; }
        //public int DutXN { get; set; }
        //public int DutYN { get; set; }
        /// <summary>
        /// Map Format : Block1 ; Block2 ; ... ; BlockN
        /// Block Format : Dut1 , Dut2 , ... , DutN
        /// Example : 0,1,...,2;3,X,...,4;...;X,3,...,2
        /// </summary>
        public string ReadMap { get; set; }

        public string WriteMap { get; set; }

        public JCARRIER()
        {
            Index = 0;
            LotCreateDateTime = 0;
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MinValue;
            ID = "";
            //Type = "";
            //BlockXN = 0;
            //BlockYN = 0;
            //DutXN = 0;
            //DutYN = 0;
            ReadMap = "";
            WriteMap = "";
        }
    }

    /// <summary>
    /// 錯誤訊息資料類別
    /// </summary>
    public class JALM
    {
        public double Index { get; set; }      //StartDateTime
        public double LotCreateDateTime { get; set; }
        public double CarrierStartDateTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string AlarmCode { get; set; }
        public string AlarmModule { get; set; }
        public string AlarmMessage { get; set; }
        public double AlarmTime { get; set; }

        private Stopwatch tmr = null;

        public JALM()
        {
            Index = 0;
            LotCreateDateTime = 0;
            CarrierStartDateTime = 0;
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MinValue;
            AlarmCode = "";
            AlarmModule = "";
            AlarmMessage = "";
            AlarmTime = 0;
            tmr = new Stopwatch();
            tmr.Stop();
            tmr.Reset();
            tmr.Start();
        }

        public void Start()
        {
            tmr.Start();
            AlarmTime = tmr.ElapsedMilliseconds;
        }

        public void Stop()
        {
            tmr.Stop();
            AlarmTime = tmr.ElapsedMilliseconds;
        }

    }


    /// <summary>
    /// 2D ID 類別
    /// </summary> 
    public class ICID
    {
        public string DieapkID { get; set; }
        public int BLK_X { get; set; }
        public int BLK_Y { get; set; }
        public int XN { get; set; }
        public int YN { get; set; }
        public string Bin { get; set; }
        public string ID { get; set; }
        public ICID()
        {
            this.DieapkID = "";
            this.BLK_X = 0;
            this.BLK_Y = 0;
            this.XN = 0;
            this.YN = 0;
            this.Bin = "";
            this.ID = "";
        }
        public void SetData(string DiepakID, int blk_x, int blk_y, int xn, int yn, string bin, string id)
        {
            this.DieapkID = DiepakID;
            this.BLK_X = blk_x;
            this.BLK_Y = blk_y;
            this.XN = xn;
            this.YN = yn;
            this.Bin = bin;
            this.ID = id;
        }

    }

}
