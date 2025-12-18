using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaeLibGeneral;
using KCSDK;
using CommonObj;
using System.Threading;

namespace CommonObj
{
    public partial class BDLinkSMCtrl : Form
    {

        private struct TBoardData
        {
            public string BoardID;
            public string BoardMap;
            public string BoardIDMap;
            public string LotNo;
        }

        #region 變數
        private bool IsShareMemOpened;
        private string strCtrlName;
        private LotInformation_SM LotData;
        private TBoardData BoardData;
        private string sCmdExeCode;
        private int iCmdExeTask;
        private CommandReturn crCmdRetReturn;
        private int iCmdErrorCode;
        private string sCmdErrorMessage;
        private double dwCommStart;
        private int iCommTimeOutTickLimit;
        private JMemoryMappedManager BDSMC = null;
        //private GetTickCountEx tick = null;
        public JWorkerThread m_Work = null;
        private JTimer TM_LinkTimeOut = null;
        private bool bUpdate;
        #endregion


        public BDLinkSMCtrl()
        {
            InitializeComponent();

            //if (tick.Equals(null))
            //tick = new GetTickCountEx();

            sCmdExeCode = "";
            iCmdExeTask = -1;
            crCmdRetReturn = CommandReturn.CR_IDLE;
            strCtrlName = "";
            bUpdate = false;

            CreateBoardData();
        }

        public void Dispose()
        {
            Close();
        }

        private void Close()
        {
            if (BDSMC != null)
            {
                BDSMC.Close();
                BDSMC.Dispose();
            }
            if (m_Work != null)
            {
                m_Work.Stop();
            }
        }

        private void CreateBoardData()
        {
            IsShareMemOpened = false;
            BDSMC = new JMemoryMappedManager();
            if (!BDSMC.Equals(null))
            {
                BDSMC.AddField("CMD", 1);
                BDSMC.AddField("RET", 1);
                BDSMC.AddField("CustomerID", 1);
                BDSMC.AddField("LinkType", 1);
                BDSMC.AddField("ProductionMode", 1);
                BDSMC.AddField("LotNo", 64);
                BDSMC.AddField("MachineNo", 32);
                BDSMC.AddField("MachineState", 4);
                BDSMC.AddField("Opid", 32);
                BDSMC.AddField("Quantity", 4);
                BDSMC.AddField("ProductName", 64);
                BDSMC.AddField("BoardType", 64);
                BDSMC.AddField("BlockColCount", 4);
                BDSMC.AddField("BlockRowCount", 4);
                BDSMC.AddField("DutColCount", 4);
                BDSMC.AddField("DutRowCount", 4);
                BDSMC.AddField("BoardID", 16);
                BDSMC.AddField("SocketName", 64);
                BDSMC.AddField("BoardQty", 4);
                BDSMC.AddField("ICWorkQty", 5);
                BDSMC.AddField("GoodDeviceQty", 5);
                BDSMC.AddField("StartTime", 32);
                BDSMC.AddField("EndTime", 32);
                BDSMC.AddField("StopTime", 32);
                BDSMC.AddField("AlarmTime", 32);
                BDSMC.AddField("AlarmTimes", 32);
                BDSMC.AddField("RunningTime", 32);
                BDSMC.AddField("Running", 32);
                BDSMC.AddField("JamRate", 16);
                BDSMC.AddField("JamTotal", 16);
                BDSMC.AddField("MTBF", 16);
                BDSMC.AddField("MUBF", 16);
                BDSMC.AddField("MTTR", 16);
                BDSMC.AddField("MTBA", 16);
                BDSMC.AddField("Availability", 16);
                BDSMC.AddField("UPH", 8);
                BDSMC.AddField("SortBinQty", 512);
                BDSMC.AddField("ForceUnload", 1);
                BDSMC.AddField("ErrorCode", 4);
                BDSMC.AddField("ErrorMessage", 1024);
                BDSMC.AddField("AlarmList", 2048);
                BDSMC.AddField("BoardMap", 2048);
                BDSMC.AddField("BoardMap_InspectionResult", 2048);
                BDSMC.AddField("BoardMap_OCR", 2048);
                BDSMC.AddField("BoardMap_2DCode1", 4096);
                BDSMC.AddField("BoardMap_2DCode2", 4096);
                BDSMC.AddField("BoardMap_2DCode3", 4096);
                BDSMC.AddField("BoardMap_2DCode4", 4096);
                BDSMC.AddField("BoardMap_2DCode5", 4096);
                BDSMC.AddField("ConnectCk", 128);
                BDSMC.AddField("Reserve1", 128);
                BDSMC.AddField("Reserve2", 128);
                BDSMC.AddField("Reserve3", 128);
                BDSMC.AddField("Reserve4", 128);
                BDSMC.AddField("Reserve5", 128);
                BDSMC.AddField("Reserve6", 128);
                BDSMC.AddField("Reserve7", 128);
                BDSMC.AddField("Reserve8", 128);
                BDSMC.AddField("Reserve9", 128);
                BDSMC.AddField("Reserve10", 819200);
                IsShareMemOpened = BDSMC.Open("BOARD_DATA_LINK");
            }
            TM_LinkTimeOut = new JTimer();

            m_Work = new JWorkerThread(Act);
            m_Work.Start();

            //timer1.Enabled = true;
            //timer1.Start();
        }

        private bool GetProcessCtrl(string CtrlName)
        {
            if (strCtrlName == "" || strCtrlName == CtrlName)
            {
                strCtrlName = CtrlName;
                return true;
            }
            return false;
        }

        private bool ReleaseProcessCtrl(string CtrlName)
        {
            if (strCtrlName == CtrlName || CtrlName == "")
            { //Mars 強制release
                strCtrlName = "";
                return true;
            }
            return false;
        }

        private void Act()
        {
            if (IsShareMemOpened == false)
            {   //ptrShareMemory 作Write/Read 前須先檢查
                iCmdErrorCode = 2;
                sCmdErrorMessage = "連線異常(Err_Connect_Failure)";
                crCmdRetReturn = CommandReturn.CR_Error;
                return;
            }

            try
            {
                switch (iCmdExeTask)
                {
                    case 0:
                        //dwCommStart = tick.Value;
                        iCmdExeTask++;
                        TM_LinkTimeOut.Reset();
                        break;

                    case 1:
                        if (sCmdExeCode == "SetLotData")
                        {
                            BDSMC.WriteStr("CustomerID", LotData.CustomerID.ToString());   //1
                            BDSMC.WriteStr("LinkType", LotData.LinkType.ToString());   //1
                            BDSMC.WriteStr("ProductionMode", LotData.ProductionMode.ToString());   //1
                            BDSMC.WriteStr("LotNo", LotData.LotNo);   //32
                            BDSMC.WriteInt("Quantity", LotData.Quantity);   //4
                            BDSMC.WriteStr("ProductName", LotData.ProductName);   //64
                            BDSMC.WriteStr("BoardType", LotData.BoardType);   //64
                            BDSMC.WriteInt("BlockColCount", LotData.BlockColCount);   //4
                            BDSMC.WriteInt("BlockRowCount", LotData.BlockRowCount);   //4
                            BDSMC.WriteInt("DutColCount", LotData.DutColCount);   //4
                            BDSMC.WriteInt("DutRowCount", LotData.DutRowCount);   //4

                            //STK開批需要資訊
                            BDSMC.WriteStr("Opid", LotData.OpID);  //12
                            BDSMC.WriteStr("SocketName", LotData.SocketName);  //20
                            BDSMC.WriteStr("ForceUnload", LotData.ForceUnload);  //1
                            BDSMC.WriteStr("MachineNo", LotData.MachineNo);

                            //STK結批需要資訊
                            BDSMC.WriteStr("BoardQty", LotData.BoardQty.ToString());  //12
                            BDSMC.WriteStr("ICWorkQty", LotData.ICWorkQty.ToString());  //20
                            BDSMC.WriteStr("GoodDeviceQty", LotData.GoodDeviceQty.ToString());  //1
                            BDSMC.WriteStr("StartTime", LotData.StartTime.ToString());  //12
                            //BDSMC.WriteStr("EndTime", LotData.EndTime);  //12
                            BDSMC.WriteStr("RunningTime", LotData.RunningTime);  //20
                            BDSMC.WriteStr("Running", LotData.Running);  //1
                            BDSMC.WriteStr("JamRate", LotData.JamRate);  //12
                            BDSMC.WriteStr("JamTotal", LotData.JamTotal);  //20
                            BDSMC.WriteStr("SortBinQty", LotData.SortBinQty);  //1
                            //aher test program
                            //BDSMC.WriteStr("Reserve2", LotData.Reserve2);

                            iCmdExeTask = 101;   //回傳OK
                            break;
                        }
                        else
                        {
                            if (sCmdExeCode == "1")
                            {   //1: StartLot 開批
                                //
                            }
                            if (sCmdExeCode == "2")
                            {   //2: EndLot 結批
                                //
                            }
                            if (sCmdExeCode == "3")
                            {   //3: BeforeInsert 上板前
                                if (BoardData.BoardID.Length < 1)
                                {
                                    iCmdErrorCode = 11;
                                    sCmdErrorMessage = "板號不符(Err_BID_Not_Match)";
                                    iCmdExeTask = 666;   //回傳Error
                                    break;
                                }
                                BDSMC.WriteStr("BoardID", BoardData.BoardID);   //16
                                //ptrShareMemory->WriteStr(WideString("BoardMap"), WideString(""));   //2048
                            }
                            if (sCmdExeCode == "4")
                            {   //4: AfterInsert 上板後
                                if (BoardData.BoardID.Length < 1)
                                {
                                    iCmdErrorCode = 101;
                                    sCmdErrorMessage = "板號不符(Err_BID_Not_Match)";
                                    iCmdExeTask = 666;   //回傳Error
                                    break;
                                }

                                BDSMC.WriteStr("MachineState", bUpdate.ToString());   //16
                                BDSMC.WriteStr("BoardID", BoardData.BoardID);   //16
                                BDSMC.WriteStr("BoardMap", BoardData.BoardMap);   //2048
                                if (string.IsNullOrEmpty(BoardData.BoardIDMap))
                                {
                                    BDSMC.WriteStr("Reserve10", "");
                                }
                                else
                                {
                                    //string[] Size4096temp = GlobalDefine.StringSplitByLength(BoardData.BoardIDMap, 4096);
                                    //for (int i = 0; i < 5; i++)
                                    //{
                                    //    if (i < Size4096temp.Length)
                                    //    {
                                    //        BDSMC.WriteStr(string.Format("BoardMap_2DCode{0}", i + 1), Size4096temp[i]);
                                    //    }
                                    //    else
                                    //    {
                                    //        BDSMC.WriteStr(string.Format("BoardMap_2DCode{0}", i + 1), "");
                                    //    }
                                    //}
                                    //BoardData.BoardIDMap
                                    BDSMC.WriteStr("Reserve10", BoardData.BoardIDMap);
                                }
                            }
                            if (sCmdExeCode == "5")
                            {   //5: BeforeRemove 卸板前
                                if (BoardData.BoardID.Length < 1)
                                {
                                    iCmdErrorCode = 11;
                                    sCmdErrorMessage = "板號不符(Err_BID_Not_Match)";
                                    iCmdExeTask = 666;   //回傳Error
                                    break;
                                }
                                BDSMC.WriteStr("BoardID", BoardData.BoardID);   //16
                                //ptrShareMemory->WriteStr(WideString("BoardMap"), WideString(""));   //2048
                            }
                            if (sCmdExeCode == "6")
                            {   //6: AfterRemove 卸板後
                                if (BoardData.BoardID.Length < 1)
                                {
                                    iCmdErrorCode = 101;
                                    sCmdErrorMessage = "板號不符(Err_BID_Not_Match)";
                                    iCmdExeTask = 666;   //回傳Error
                                    break;
                                }
                                BDSMC.WriteStr("BoardID", BoardData.BoardID);   //16
                                BDSMC.WriteStr("BoardMap", BoardData.BoardMap);   //2048
                            }
                            BDSMC.WriteStr("RET", "0");   //1
                            BDSMC.WriteStr("CMD", sCmdExeCode);   //1

                            iCmdExeTask++;
                            break;
                        }
                        break;

                    case 2:
                        {
                            //if (Math.Abs(tick.Value - dwCommStart) > iCommTimeOutTickLimit)
                            if (TM_LinkTimeOut.Count(iCommTimeOutTickLimit))
                            {
                                iCmdErrorCode = 99;
                                sCmdErrorMessage = "Time Out.";
                                iCmdExeTask = 666;   //回傳Error
                                break;
                            }

                            string wsRet = "";
                            if (BDSMC.ReadStr("RET", ref wsRet))   //1
                            {
                                if (wsRet.Equals("0"))
                                {
                                    crCmdRetReturn = CommandReturn.CR_Waitting;
                                    break;
                                }
                                if (wsRet.Equals("1"))
                                {
                                    iCmdExeTask = 100;   //取得Board Map
                                    break;
                                }
                                if (wsRet.Equals("2"))
                                {
                                    iCmdExeTask = 665;   //取得Error Code
                                    break;
                                }
                            }
                        }
                        break;

                    //==================================================
                    case 100:   //取得Board Map
                        {
                            string wsRet = "";
                            string LotRet = "";
                            if (sCmdExeCode == "3" || sCmdExeCode == "5")
                            {   //3: BeforeInsert 上板前   //5: BeforeRemove 卸板前
                                if (BDSMC.ReadStr("BoardMap", ref wsRet) && BDSMC.ReadStr("Reserve3", ref LotRet))   //2048
                                {
                                    BoardData.BoardMap = wsRet;
                                    BoardData.LotNo = LotRet;
                                    iCmdExeTask = 101;   //回傳OK
                                    break;
                                }
                            }
                            else
                            {
                                iCmdExeTask = 101;   //回傳OK
                                break;
                            }
                        }
                        break;

                    case 101:   //回傳OK
                        crCmdRetReturn = CommandReturn.CR_OK;
                        break;

                    case 665:   //取得Error Code
                        {
                            int iRet = 0;
                            string wsRet = "";
                            int iDataCount = 0;
                            if (BDSMC.ReadInt("ErrorCode", ref iRet))
                            {   //4
                                iCmdErrorCode = iRet;
                                iDataCount++;
                            }
                            if (BDSMC.ReadStr("ErrorMessage", ref wsRet))
                            {   //1024
                                sCmdErrorMessage = wsRet;
                                iDataCount++;
                            }

                            if (iDataCount == 2)
                            {
                                iCmdExeTask = 666;   //回傳Error
                                break;
                            }
                        }
                        break;

                    case 666:   //回傳Error
                        crCmdRetReturn = CommandReturn.CR_Error;
                        break;

                    default:
                        //iCmdErrorCode = 2;
                        //sCmdErrorMessage = "連線異常(Err_Connect_Failure)";
                        //crCmdRetReturn = CR_Error;
                        break;
                }

                if (true)
                {
                    lblCtrlName.Text = strCtrlName;
                    string wstmp = "";
                    int itmp = 0;

                    BDSMC.ReadStr("CMD", ref wstmp);
                    lblCMD.Text = wstmp;
                    BDSMC.ReadStr("RET", ref wstmp);
                    lblRET.Text = wstmp;
                    BDSMC.ReadStr("CustomerID", ref wstmp);
                    lblCustomerID.Text = wstmp;
                    BDSMC.ReadStr("LinkType", ref wstmp);
                    lblLinkType.Text = wstmp;
                    BDSMC.ReadStr("ProductionMode", ref wstmp);
                    lblProductionMode.Text = wstmp;
                    BDSMC.ReadStr("LotNo", ref wstmp);
                    lblLotNo.Text = wstmp;
                    BDSMC.ReadStr("Quantity", ref wstmp);
                    lblQuantity.Text = wstmp;
                    BDSMC.ReadStr("ProductName", ref wstmp);
                    lblProductName.Text = wstmp;
                    BDSMC.ReadStr("BoardType", ref wstmp);
                    lblBoardType.Text = wstmp;

                    BDSMC.ReadInt("BlockColCount", ref itmp);
                    lblBlockColCount.Text = itmp.ToString();
                    BDSMC.ReadInt("BlockRowCount", ref itmp);
                    label12.Text = itmp.ToString();
                    BDSMC.ReadInt("DutColCount", ref itmp);
                    lblDutColCount.Text = itmp.ToString();
                    BDSMC.ReadInt("DutRowCount", ref itmp);
                    lblDutRowCount.Text = itmp.ToString();

                    BDSMC.ReadStr("BoardID", ref wstmp);
                    lblBoardID.Text = wstmp;

                    BDSMC.ReadInt("ErrorCode", ref itmp);
                    lblErrorCode.Text = itmp.ToString();

                    BDSMC.ReadStr("ErrorMessage", ref wstmp);
                    lblErrorMessage.Text = wstmp;
                    BDSMC.ReadStr("BoardMap", ref wstmp);
                    lblBoardMap.Text = wstmp;

                    //timer1.Interval = 1000;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Open() error: " + ex.Message);
            }
            Thread.Sleep(1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
            timer1.Stop();

            if (IsShareMemOpened == false)
            {   //ptrShareMemory 作Write/Read 前須先檢查
                iCmdErrorCode = 2;
                sCmdErrorMessage = "連線異常(Err_Connect_Failure)";
                crCmdRetReturn = CommandReturn.CR_Error;
                return;
            }

            try
            {
                switch (iCmdExeTask)
                {
                    case 0:
                        //dwCommStart = tick.Value;
                        iCmdExeTask++;
                        TM_LinkTimeOut.Reset();
                        break;

                    case 1:
                        if (sCmdExeCode == "SetLotData")
                        {
                            BDSMC.WriteStr("CustomerID", LotData.CustomerID.ToString());   //1
                            BDSMC.WriteStr("LinkType", LotData.LinkType.ToString());   //1
                            BDSMC.WriteStr("ProductionMode", LotData.ProductionMode.ToString());   //1
                            BDSMC.WriteStr("LotNo", LotData.LotNo);   //32
                            BDSMC.WriteStr("Quantity", LotData.Quantity.ToString());   //4
                            BDSMC.WriteStr("ProductName", LotData.ProductName);   //64
                            BDSMC.WriteStr("BoardType", LotData.BoardType);   //64
                            BDSMC.WriteInt("BlockColCount", LotData.BlockColCount);   //4
                            BDSMC.WriteInt("BlockRowCount", LotData.BlockRowCount);   //4
                            BDSMC.WriteInt("DutColCount", LotData.DutColCount);   //4
                            BDSMC.WriteInt("DutRowCount", LotData.DutRowCount);   //4

                            //STK開批需要資訊
                            BDSMC.WriteStr("Opid", LotData.OpID);  //12
                            BDSMC.WriteStr("SocketName", LotData.SocketName);  //20
                            BDSMC.WriteStr("ForceUnload", LotData.ForceUnload);  //1
                            BDSMC.WriteStr("MachineNo", LotData.MachineNo);

                            //STK結批需要資訊
                            BDSMC.WriteStr("BoardQty", LotData.BoardQty.ToString());  //12
                            BDSMC.WriteStr("ICWorkQty", LotData.ICWorkQty.ToString());  //20
                            BDSMC.WriteStr("GoodDeviceQty", LotData.GoodDeviceQty.ToString());  //1
                            BDSMC.WriteStr("StartTime", LotData.StartTime);  //12
                            //BDSMC.WriteStr("EndTime",        LotData.EndTime);  //12
                            BDSMC.WriteStr("RunningTime", LotData.RunningTime);  //20
                            BDSMC.WriteStr("Running", LotData.Running);  //1
                            BDSMC.WriteStr("JamRate", LotData.JamRate);  //12
                            BDSMC.WriteStr("JamTotal", LotData.JamTotal);  //20
                            BDSMC.WriteStr("SortBinQty", LotData.SortBinQty);  //1
                            //aher test program
                            BDSMC.WriteStr("Reserve2", LotData.Reserve2);

                            iCmdExeTask = 101;   //回傳OK
                            break;
                        }
                        else
                        {
                            if (sCmdExeCode == "1")
                            {   //1: StartLot 開批
                                //
                            }
                            if (sCmdExeCode == "2")
                            {   //2: EndLot 結批
                                //
                            }
                            if (sCmdExeCode == "3")
                            {   //3: BeforeInsert 上板前
                                if (BoardData.BoardID.Length < 1)
                                {
                                    iCmdErrorCode = 11;
                                    sCmdErrorMessage = "板號不符(Err_BID_Not_Match)";
                                    iCmdExeTask = 666;   //回傳Error
                                    break;
                                }
                                BDSMC.WriteStr("BoardID", BoardData.BoardID);   //16
                                //ptrShareMemory->WriteStr(WideString("BoardMap"), WideString(""));   //2048
                            }
                            if (sCmdExeCode == "4")
                            {   //4: AfterInsert 上板後
                                if (BoardData.BoardID.Length < 1)
                                {
                                    iCmdErrorCode = 101;
                                    sCmdErrorMessage = "板號不符(Err_BID_Not_Match)";
                                    iCmdExeTask = 666;   //回傳Error
                                    break;
                                }
                                BDSMC.WriteStr("BoardID", BoardData.BoardID);   //16
                                BDSMC.WriteStr("BoardMap", BoardData.BoardMap);   //2048
                            }
                            if (sCmdExeCode == "5")
                            {   //5: BeforeRemove 卸板前
                                if (BoardData.BoardID.Length < 1)
                                {
                                    iCmdErrorCode = 11;
                                    sCmdErrorMessage = "板號不符(Err_BID_Not_Match)";
                                    iCmdExeTask = 666;   //回傳Error
                                    break;
                                }
                                BDSMC.WriteStr("BoardID", BoardData.BoardID);   //16
                                //ptrShareMemory->WriteStr(WideString("BoardMap"), WideString(""));   //2048
                            }
                            if (sCmdExeCode == "6")
                            {   //6: AfterRemove 卸板後
                                if (BoardData.BoardID.Length < 1)
                                {
                                    iCmdErrorCode = 101;
                                    sCmdErrorMessage = "板號不符(Err_BID_Not_Match)";
                                    iCmdExeTask = 666;   //回傳Error
                                    break;
                                }
                                BDSMC.WriteStr("BoardID", BoardData.BoardID);   //16
                                BDSMC.WriteStr("BoardMap", BoardData.BoardMap);   //2048
                            }
                            BDSMC.WriteStr("RET", "0");   //1
                            BDSMC.WriteStr("CMD", sCmdExeCode);   //1

                            iCmdExeTask++;
                            break;
                        }
                        break;

                    case 2:
                        {
                            //if (Math.Abs(tick.Value - dwCommStart) > iCommTimeOutTickLimit){
                            if (TM_LinkTimeOut.Count(iCommTimeOutTickLimit))
                            {
                                iCmdErrorCode = 99;
                                sCmdErrorMessage = "Time Out.";
                                iCmdExeTask = 666;   //回傳Error
                                break;
                            }

                            string wsRet = "";
                            if (BDSMC.ReadStr("RET", ref wsRet))   //1
                            {
                                if (wsRet.Equals("0"))
                                {
                                    crCmdRetReturn = CommandReturn.CR_Waitting;
                                    break;
                                }
                                if (wsRet.Equals("1"))
                                {
                                    iCmdExeTask = 100;   //取得Board Map
                                    break;
                                }
                                if (wsRet.Equals("2"))
                                {
                                    iCmdExeTask = 665;   //取得Error Code
                                    break;
                                }
                            }
                        }
                        break;

                    //==================================================
                    case 100:   //取得Board Map
                        {
                            string wsRet = "";
                            if (sCmdExeCode == "3" || sCmdExeCode == "5")
                            {   //3: BeforeInsert 上板前   //5: BeforeRemove 卸板前
                                if (BDSMC.ReadStr("BoardMap", ref wsRet))   //2048
                                {
                                    BoardData.BoardMap = wsRet;
                                    iCmdExeTask = 101;   //回傳OK
                                    break;
                                }
                            }
                            else
                            {
                                iCmdExeTask = 101;   //回傳OK
                                break;
                            }
                        }
                        break;

                    case 101:   //回傳OK
                        crCmdRetReturn = CommandReturn.CR_OK;
                        break;

                    case 665:   //取得Error Code
                        {
                            int iRet = 0;
                            string wsRet = "";
                            int iDataCount = 0;
                            if (BDSMC.ReadInt("ErrorCode", ref iRet))
                            {   //4
                                iCmdErrorCode = iRet;
                                iDataCount++;
                            }
                            if (BDSMC.ReadStr("ErrorMessage", ref wsRet))
                            {   //1024
                                sCmdErrorMessage = wsRet;
                                iDataCount++;
                            }

                            if (iDataCount == 2)
                            {
                                iCmdExeTask = 666;   //回傳Error
                                break;
                            }
                        }
                        break;

                    case 666:   //回傳Error
                        crCmdRetReturn = CommandReturn.CR_Error;
                        break;

                    default:
                        //iCmdErrorCode = 2;
                        //sCmdErrorMessage = "連線異常(Err_Connect_Failure)";
                        //crCmdRetReturn = CR_Error;
                        break;
                }

                if (true)
                {
                    lblCtrlName.Text = strCtrlName;
                    string wstmp = "";
                    int itmp = 0;

                    BDSMC.ReadStr("CMD", ref wstmp);
                    lblCMD.Text = wstmp;
                    BDSMC.ReadStr("RET", ref wstmp);
                    lblRET.Text = wstmp;
                    BDSMC.ReadStr("CustomerID", ref wstmp);
                    lblCustomerID.Text = wstmp;
                    BDSMC.ReadStr("LinkType", ref wstmp);
                    lblLinkType.Text = wstmp;
                    BDSMC.ReadStr("ProductionMode", ref wstmp);
                    lblProductionMode.Text = wstmp;
                    BDSMC.ReadStr("LotNo", ref wstmp);
                    lblLotNo.Text = wstmp;
                    BDSMC.ReadStr("Quantity", ref wstmp);
                    lblQuantity.Text = wstmp;
                    BDSMC.ReadStr("ProductName", ref wstmp);
                    lblProductName.Text = wstmp;
                    BDSMC.ReadStr("BoardType", ref wstmp);
                    lblBoardType.Text = wstmp;

                    BDSMC.ReadInt("BlockColCount", ref itmp);
                    lblBlockColCount.Text = itmp.ToString();
                    BDSMC.ReadInt("BlockRowCount", ref itmp);
                    label12.Text = itmp.ToString();
                    BDSMC.ReadInt("DutColCount", ref itmp);
                    lblDutColCount.Text = itmp.ToString();
                    BDSMC.ReadInt("DutRowCount", ref itmp);
                    lblDutRowCount.Text = itmp.ToString();

                    BDSMC.ReadStr("BoardID", ref wstmp);
                    lblBoardID.Text = wstmp;

                    BDSMC.ReadInt("ErrorCode", ref itmp);
                    lblErrorCode.Text = itmp.ToString();

                    BDSMC.ReadStr("ErrorMessage", ref wstmp);
                    lblErrorMessage.Text = wstmp;
                    BDSMC.ReadStr("BoardMap", ref wstmp);
                    lblBoardMap.Text = wstmp;

                    timer1.Interval = 1000;
                }
                else
                {
                    timer1.Interval = 100;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Open() error: " + ex.Message);
            }

            //timer1.Enabled = true;
            timer1.Start();
        }

        public CommandReturn SetLotData(string CtrlName, LotInformation_SM LData, int TimeOutLimit = 5000)
        {
            if (GetProcessCtrl(CtrlName) == false)
            {
                return CommandReturn.CR_Waitting;
            }

            if (sCmdExeCode == "" && iCmdExeTask == -1 && crCmdRetReturn == CommandReturn.CR_IDLE)   //交握程序未使用中
            {
                //初始化
                LotData = LData;


                //啟動
                sCmdExeCode = "SetLotData";
                iCmdExeTask = 0;
                crCmdRetReturn = CommandReturn.CR_Waitting;
                iCmdErrorCode = 0;
                sCmdErrorMessage = "";
                iCommTimeOutTickLimit = TimeOutLimit;

            }
            return crCmdRetReturn;
        }

        public CommandReturn StartLot(string CtrlName, int TimeOutLimit = 10000)
        {
            if (GetProcessCtrl(CtrlName) == false)
            {
                return CommandReturn.CR_Waitting;
            }

            if (sCmdExeCode == "" && iCmdExeTask == -1 && crCmdRetReturn == CommandReturn.CR_IDLE)   //交握程序未使用中
            {
                //啟動
                sCmdExeCode = "1";   //1: StartLot 開批
                iCmdExeTask = 0;
                crCmdRetReturn = CommandReturn.CR_Waitting;
                iCmdErrorCode = 0;
                sCmdErrorMessage = "";
                iCommTimeOutTickLimit = TimeOutLimit;

            }
            return crCmdRetReturn;
        }

        public CommandReturn EndLot(string CtrlName, int TimeOutLimit = 10000)
        {
            if (GetProcessCtrl(CtrlName) == false)
            {
                return CommandReturn.CR_Waitting;
            }

            if (sCmdExeCode == "" && iCmdExeTask == -1 && crCmdRetReturn == CommandReturn.CR_IDLE)   //交握程序未使用中
            {
                //啟動
                sCmdExeCode = "2";   //2: EndLot 結批
                iCmdExeTask = 0;
                crCmdRetReturn = CommandReturn.CR_Waitting;
                iCmdErrorCode = 0;
                sCmdErrorMessage = "";
                iCommTimeOutTickLimit = TimeOutLimit;

            }
            return crCmdRetReturn;
        }

        public CommandReturn BeforeInsert(string CtrlName, string BoardID, ref string BoardMap,ref string LotNo, int TimeOutLimit = 5000)
        {
            if (GetProcessCtrl(CtrlName) == false)
            {
                return CommandReturn.CR_Waitting;
            }

            if (sCmdExeCode == "" && iCmdExeTask == -1 && crCmdRetReturn == CommandReturn.CR_IDLE)   //交握程序未使用中
            {
                //初始化
                BoardData.BoardID = BoardID;
                //啟動
                sCmdExeCode = "3";   //3: BeforeInsert 上板前
                iCmdExeTask = 0;
                crCmdRetReturn = CommandReturn.CR_Waitting;
                iCmdErrorCode = 0;
                sCmdErrorMessage = "";
                iCommTimeOutTickLimit = TimeOutLimit;

            }

            if (crCmdRetReturn == CommandReturn.CR_OK)
            {
                BoardMap = BoardData.BoardMap;
                LotNo = BoardData.LotNo;
            }
            else
            {
                BoardMap = "";
            }
            return crCmdRetReturn;
        }

        public CommandReturn AfterInsert(string CtrlName, string BoardID, ref string BoardMap, string BoardIDMap = "", int TimeOutLimit = 5000, bool Update = false)
        {
            if (GetProcessCtrl(CtrlName) == false)
            {
                return CommandReturn.CR_Waitting;
            }

            if (sCmdExeCode == "" && iCmdExeTask == -1 && crCmdRetReturn == CommandReturn.CR_IDLE)   //交握程序未使用中
            {
                //初始化
                BoardData.BoardID = BoardID;
                BoardData.BoardMap = BoardMap;
                BoardData.BoardIDMap = BoardIDMap;
                //啟動
                sCmdExeCode = "4";   //4: AfterInsert 上板後
                bUpdate = Update;
                iCmdExeTask = 0;
                crCmdRetReturn = CommandReturn.CR_Waitting;
                iCmdErrorCode = 0;
                sCmdErrorMessage = "";
                iCommTimeOutTickLimit = TimeOutLimit;

            }
            return crCmdRetReturn;
        }

        public CommandReturn BeforeRemove(string CtrlName, string BoardID, ref string BoardMap,ref string LotNo, int TimeOutLimit = 5000)
        {
            if (GetProcessCtrl(CtrlName) == false)
            {
                return CommandReturn.CR_Waitting;
            }

            if (sCmdExeCode == "" && iCmdExeTask == -1 && crCmdRetReturn == CommandReturn.CR_IDLE)   //交握程序未使用中
            {
                //初始化
                BoardData.BoardID = BoardID;

                //啟動
                sCmdExeCode = "5";   //5: BeforeRemove 卸板前
                iCmdExeTask = 0;
                crCmdRetReturn = CommandReturn.CR_Waitting;
                iCmdErrorCode = 0;
                sCmdErrorMessage = "";
                iCommTimeOutTickLimit = TimeOutLimit;

            }

            if (crCmdRetReturn == CommandReturn.CR_OK)
            {
                BoardMap = BoardData.BoardMap;
                LotNo = BoardData.LotNo;
            }
            else
            {
                BoardMap = "";
            }
            return crCmdRetReturn;
        }

        public CommandReturn AfterRemove(string CtrlName, string BoardID, ref string BoardMap, int TimeOutLimit = 5000)
        {
            if (GetProcessCtrl(CtrlName) == false)
            {
                return CommandReturn.CR_Waitting;
            }

            if (sCmdExeCode == "" && iCmdExeTask == -1 && crCmdRetReturn == CommandReturn.CR_IDLE)   //交握程序未使用中
            {
                //初始化
                BoardData.BoardID = BoardID;
                BoardData.BoardMap = BoardMap;

                //啟動
                sCmdExeCode = "6";   //6: AfterRemove 卸板後
                iCmdExeTask = 0;
                crCmdRetReturn = CommandReturn.CR_Waitting;
                iCmdErrorCode = 0;
                sCmdErrorMessage = "";
                iCommTimeOutTickLimit = TimeOutLimit;

            }
            return crCmdRetReturn;
        }

        public void GetErrorMessage(ref int ErrorCode, ref string ErrorMsg)
        {
            ErrorCode = iCmdErrorCode;
            ErrorMsg = sCmdErrorMessage;
        }

        public bool ReleaseCommandProcess(string CtrlName)
        {
            if (ReleaseProcessCtrl(CtrlName))
            {
                sCmdExeCode = "";
                iCmdExeTask = -1;
                crCmdRetReturn = CommandReturn.CR_IDLE;
                return true;
            }
            return false;
        }
    }
}
