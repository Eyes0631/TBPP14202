using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KCSDK;
using PaeLibGeneral;
using PaeLibProVSDKEx;
using System.IO;
using System.Diagnostics;



namespace CommonObj
{
    #region struct
    //各模組點位傳遞struct
    //public struct BasePosInfo
    //{
    //    public int X { get; set; }
    //    public int Y { get; set; }
    //    public int Z { get; set; }
    //    public int U { get; set; }
    //}

    //各模組回傳基準點位Struct
    public struct BasePosInfo
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int U { get; set; }

        public TrayCorner Dir { get; set; }
        
        //視覺用
        public int RowIndex { get; set; }
        public int ColIndex { get; set; }
        public int BoardCount { get; set; }
    }

    #endregion

    #region enum
    public enum TrayID
    { 
        NONE = -1,
        LEFT = 0,
        RIGHT = 1
    }

    public enum CommandReturn
    {
        CR_IDLE,
        CR_Waitting,
        CR_OK,
        CR_Error
    }

    public enum InspectionState
    {
        Unknow,
        Success,
        Fail,
        Error,
        Wait,
    }

    public enum CassetteID
    { 
        NONE = 0,
        LEFT = 1,
        RIGHT = 2,
    }

    /// <summary>
    /// 治具區塊編號
    /// </summary>
    public enum KitBlockID
    {
        /// <summary>
        /// 未知
        /// </summary>
        NONE = 0,

        /// <summary>
        /// 上區塊
        /// </summary>
        TOP_KIT_BLOCK,

        /// <summary>
        /// 下區塊
        /// </summary>
        BOTTOM_KIT_BLOCK,
    }

    public enum EOnOffState
    {
        ON,
        OFF
    }

    public enum ACTIONMODE
    { 
        NONE,
        MOVE,
        GET,
        PUT,
        LOCK,
        UNLOCK,
        OPEN,
        CLOSE,
        OPEN_DETECT,
        PICKUP,
        PLACEMENT,
        GRAB_CCD,
        GRAB_CCD_AF,
        READ,
        SCAN,   //新增
    }

    public enum CommonDirection
    {
        None = 0,
        LEFT_TOP = 1,         
        LEFT_BOTTOM = 2,  
        RIGHT_TOP = 3,      
        RIGHT_BOTTOM = 4,
    }

    public enum PnPStation
    {
        NONE = 0,
        BOARD_A = 1,
        BOARD_B = 2,
        KIT_SHUTTLE_A =3,
        KIT_SHUTTLE_B = 4,
        BOWLFEEDER_A=5,
        BOWLFEEDER_B = 6,
        PassBox = 7,
        BinBox=8,
        WAITING = 99,
    }

    public enum CH_CHECKMODE
    {
        NONE = 0,
        //關蓋前
        BEFORECLOSE = 1,
        //關蓋後
        AFTERCLOSE = 2,
        //開蓋前
        BEFOREOPEN = 3,
        //開蓋後
        AFTEROPEN = 4,
    }

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
        LOAD_UNLOAD = (LOAD | UNLOAD),
        //全部
        //ALL = (NONE | LOAD | UNLOAD)
    }

    /// <summary>
    /// 板台車使用者
    /// </summary>
    public enum BIBStageModuleOwner
    {
        NONE = 0,
        HDT_BIB_A,
        HDT_BIB_B,
        HDT_BIB_A_CCD,
        HDT_BIB_B_CCD,
        TRANSFER,
    }

    /// <summary>
    /// 板台車車號
    /// </summary>
    public enum BIBStageID
    {
        NONE = 0,
        //左台車
        BIBStageA = 1,
        //右台車
        BIBStageB = 2,
        //Unknow
        Unknow = 4,
    }

    /// <summary>
    /// board stage state
    /// </summary>
    public enum BIBStageSate
    { 
        NONE,
        IDLE,
        TRANSFER,
        READY,
        WORKING,
    }

    /// <summary>
    /// 板手臂ID
    /// </summary>
    public enum BoardHeadID
    { 
        NONE = 0,
        HDT_A = 1,
        HDT_B = 2,
        Unknow= 4,
    }

    /// <summary>
    /// BowlFeeder 手臂ID
    /// </summary>
    public enum BowlFeederID
    { 
        NONE = 0,
        BF_A = 1,
        BF_B = 2,
        Unknow = 4,
    }

    /// <summary>
    /// Transfer 工作站別
    /// </summary>
    [Flags]
    public enum TRMStation
    { 
        NONE = 0,                              //初始
        STAGE_A = 1,                        //Left Board Stage
        STAGE_B = 2,                        //Right Board Stage
        LOAD_A = 4,                         //Left Load/Unload
        LOAD_B = 8,                         //Right Load/Unload
        READBOARCODE = 16,          //Read Barcode Station
        STAGE_CH = 32,                   //Clamsheel Stage
        WAITING = 64,                     //Waiting Station
        DIRECT_TOP = (STAGE_A | STAGE_B),
        DIRECT_DOWN = (LOAD_A | LOAD_B | READBOARCODE),
        DORECT_RIGHT = (STAGE_CH | WAITING),
    }

    public enum TRMState
    { 
        NONE = 0,
        IDLE = 1,
        WORKING = 2,
    }

    [Flags]
    public enum VehicleState
    { 
        NONE  = 0,
        UNTESTED = 1,
        TESTED = 2,
        EMPTY = 4,
        FULL = 8,
        CHECKED = 16,
        BOOKED = 32,
        PASS = 64,
        FAIL = 128,
        TESTED_EMPTY = (TESTED | EMPTY),
        TESTED_EMPTY_CHECKED = (TESTED | EMPTY | CHECKED),
        TESTED_EMPTY_BOOKED = (TESTED | EMPTY | BOOKED),
        TESTED_FULL_BOOKED = (TESTED | FULL | BOOKED),
        TESTED_FULL = (TESTED | FULL),
        UNTESTED_EMPTY = (UNTESTED | EMPTY),
        UNTESTED_EMPTY_CHECKED = (UNTESTED | EMPTY | CHECKED),
        UNTESTED_EMPTY_BOOKED = (UNTESTED | EMPTY | BOOKED),
        UNTESTED_FULL = (UNTESTED | FULL),
        TEST_EMPTY_BOOKED = (TESTED | EMPTY | BOOKED),
        TEST_FULL_BOOKED = (TESTED | FULL | BOOKED),
    }

    /// <summary>
    /// 確認手臂位子進行安全保護
    /// </summary>
    public enum HDTLocation
    { 
        LEFTBOARD,
        RIGHTBOARD,
        LEFTKIT,
        RIGHTKIT,
        MIDDLEKIT,
        SAFE,
        LEFTTRAY,
        RIGHTTRAY,
    }

    public enum LinkMode
    { 
        OFFLINE,
        Saunders,
    }
    #endregion

    #region class
    //v2.0.0.1 add 
    public class BoardInfo
    {
        public string BID { get; set; }
        public int Slot { get; set; }
        public string BeforeMap { get; set; }
        public string AfterMap { get; set; }
        public TRMStation NowStation { get; set; }
        public JActionFlag State {get; set; }

        public BoardInfo()
        {
            //建構
            this.BID = string.Empty;
            this.Slot = -1;
            this.BeforeMap = string.Empty;
            this.AfterMap = string.Empty;
            this.NowStation = TRMStation.NONE;
            this.State = new JActionFlag();
        }

        public void Reset()
        {
            this.BID = string.Empty;
            this.Slot = -1;
            this.BeforeMap = string.Empty;
            this.AfterMap = string.Empty;
            this.NowStation = TRMStation.NONE;
            this.State.Reset();
        }

        public BoardInfo Clone()
        {
            BoardInfo info = new BoardInfo();
            info.BID = this.BID;
            info.Slot = this.Slot;
            info.BeforeMap = this.BeforeMap;
            info.AfterMap = this.AfterMap;
            info.NowStation = this.NowStation;
            info.State = new JActionFlag();
            if (this.State.IsDoIt()) info.State.DoIt();
            else if (this.State.IsDoing()) info.State.Doing();
            else if (this.State.IsDone()) info.State.Done();

            return info;
        }
    }

    public class CassetteInfo
    {
        public CassetteID ID { get; set; }
        public JActionFlag State { get; set; }
        public List<BoardInfo> BIBlist;

        public CassetteInfo(CassetteID id)
        {
            this.ID = id;
            this.State = new JActionFlag();
            BIBlist = new List<BoardInfo>();

            for (int i = 0; i < 25; i++)    //先假設最大值為25
            {
                BIBlist.Add(new BoardInfo() { Slot = i });
            }
        }

        public void Reset()
        {
            this.State.Reset();
            BIBlist.Clear();
        }

        public void SetBoardInfo(BoardInfo info, int slot)
        {
            if (info == null) return;
            if (slot < 0 || slot >= BIBlist.Count) return;
            BIBlist[slot] = info.Clone();
            BIBlist[slot].Slot = slot;
        }
    }

    public class BoardMapInfo
    {
        /// <summary>
        /// Board Block X數量
        /// </summary>
        public int BlockCols { get; private set; }

        /// <summary>
        /// Board Block Y數量
        /// </summary>
        public int BlockRows { get; private set; }

        /// <summary>
        /// Socket X 數量
        /// </summary>
        public int SocketCols { get; private set; }

        /// <summary>
        /// Socket Y數量
        /// </summary>
        public int SocketRows { get; private set; }

        /// <summary>
        /// Board Map 字串
        /// <para>ex:"1,2,3,4,5,6,7,8,9,0,......"</para>>
        /// </summary>
        public string BoardMap { get; private set; }

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="bx">Block X 數量</param>
        /// <param name="by">Block Y 數量</param>
        /// <param name="xn">Socket X 數量</param>
        /// <param name="yn">Socket Y 數量</param>
        /// <param name="map">Board Map 字串</param>
        public BoardMapInfo(int bx, int by, int xn, int yn, string map)
        {
            this.BlockCols = bx;
            this.BlockRows = by;
            this.SocketCols = xn;
            this.SocketRows = yn;
            this.BoardMap = map;
        }

        /// <summary>
        /// 建構子無參數
        /// </summary>
        public BoardMapInfo()
            : this(0, 0, 0, 0, "")
        { 
        }

        public Point SocketIndexToPoint(int socket_idx)
        {
            Point xy = new Point();
            //xy.X = (socket_idx % this.SocketCols);
            //xy.Y = (socket_idx / this.SocketCols);
            xy.X = (socket_idx / this.SocketCols);
            xy.Y = (socket_idx % this.SocketCols);
            return xy;
        }
    }

    public class GlobalDefine
    {
        public static byte[] EmptyBin = new byte[] { (byte)BinDefine.Empty };
        public static byte[] PassBin = new byte[] { (byte)BinDefine.Bin1 };
        public static byte[] FailBin = new byte[] { (byte)BinDefine.Bin2, (byte)BinDefine.Bin3, (byte)BinDefine.Bin4, (byte)BinDefine.Bin5, (byte)BinDefine.Bin6, (byte)BinDefine.Bin7, (byte)BinDefine.Bin8, (byte)BinDefine.Bin9, (byte)BinDefine.Bin10, (byte)BinDefine.Bin11, (byte)BinDefine.Bin12, (byte)BinDefine.Bin13, (byte)BinDefine.Bin14, (byte)BinDefine.Bin15, (byte)BinDefine.Bin16 };
        public static byte[] AllBin = new byte[] { (byte)BinDefine.Bin1, (byte)BinDefine.Bin2, (byte)BinDefine.Bin3, (byte)BinDefine.Bin4, (byte)BinDefine.Bin5, (byte)BinDefine.Bin6, (byte)BinDefine.Bin7, (byte)BinDefine.Bin8, (byte)BinDefine.Bin9, (byte)BinDefine.Bin10, (byte)BinDefine.Bin11, (byte)BinDefine.Bin12, (byte)BinDefine.Bin13, (byte)BinDefine.Bin14, (byte)BinDefine.Bin15, (byte)BinDefine.Bin16 };
        public static byte[] ResetBin = new byte[] { (byte)BinDefine.None };
        public static byte[] BadSocketBin = new byte[] { (byte)BinDefine.BadSocket };
        //public static byte[] CHMOpen = new byte[] {(byte)BinDefine.Bin1S};
        //public static byte[] CHMClose = new byte[] {(byte)BinDefine.Bin2S};

        public static string BinToStr(byte symbol)
        {
            switch ((BinDefine)symbol)
            {
                case BinDefine.Disabled:
                    {
                        return "-";
                    }
                    break;
                case BinDefine.BadSocket:
                    {
                        return "X";
                    }
                    break;
                case BinDefine.MaskedSocket:
                    {
                        return "M";
                    }
                    break;
                case BinDefine.Empty:
                    {
                        return "0";
                    }
                    break;
                case BinDefine.Bin1:
                    {
                        return "1";
                    }
                    break;
                case BinDefine.Bin2:
                    {
                        return "2";
                    }
                    break;
                case BinDefine.Bin3:
                    {
                        return "3";
                    }
                    break;
                case BinDefine.Bin4:
                    {

                        return "4";
                    }
                    break;
                case BinDefine.Bin5:
                    {

                        return "5";
                    }
                    break;
                case BinDefine.Bin6:
                    {

                        return "6";
                    }
                    break;
                case BinDefine.Bin7:
                    {
                        return "7";
                    }
                    break;
                case BinDefine.Bin8:
                    {

                        return "8";
                    }
                    break;
                case BinDefine.Bin9:
                    {

                        return "9";
                    }
                    break;
                case BinDefine.Bin10:
                    {

                        return "10";
                    }
                    break;
                case BinDefine.Bin11:
                    {

                        return "11";
                    }
                    break;
                case BinDefine.Bin12:
                    {

                        return "12";
                    }
                    break;
                case BinDefine.Bin13:
                    {

                        return "13";
                    }
                    break;
                case BinDefine.Bin14:
                    {

                        return "14";
                    }
                    break;
                case BinDefine.Bin15:
                    {

                        return "15";
                    }
                    break;
                case BinDefine.Bin16: { return "16"; } break;
                case BinDefine.Bin17: { return "17"; } break;
                case BinDefine.Bin18: { return "18"; } break;
                case BinDefine.Bin19: { return "19"; } break;
                case BinDefine.Bin20: { return "20"; } break;
                case BinDefine.Bin21: { return "21"; } break;
                case BinDefine.Bin22: { return "22"; } break;
                case BinDefine.Bin23: { return "23"; } break;
                case BinDefine.Bin24: { return "24"; } break;
                case BinDefine.Bin25: { return "25"; } break;
                case BinDefine.Bin26: { return "26"; } break;
                case BinDefine.Bin27: { return "27"; } break;
                case BinDefine.Bin28: { return "28"; } break;
                case BinDefine.Bin29: { return "29"; } break;
                case BinDefine.Bin30: { return "30"; } break;
                case BinDefine.Bin31: { return "31"; } break;
                case BinDefine.Bin32: { return "32"; } break;
                //case BinDefine.Bin133: { return "133"; } break;
                //case BinDefine.Bin134: { return "134"; } break;
                //case BinDefine.Bin135: { return "135"; } break;
                //case BinDefine.Bin136: { return "136"; } break;
                //case BinDefine.Bin137: { return "137"; } break;
                //case BinDefine.Bin138: { return "138"; } break;
                //case BinDefine.Bin139: { return "139"; } break;
                //case BinDefine.Bin140: { return "140"; } break;
                //case BinDefine.Bin141: { return "141"; } break;
                //case BinDefine.Bin142: { return "142"; } break;
                //case BinDefine.Bin143: { return "143"; } break;
                //case BinDefine.Bin144: { return "144"; } break;
                //case BinDefine.Bin145: { return "145"; } break;
                //case BinDefine.Bin146: { return "146"; } break;
                //case BinDefine.Bin147: { return "147"; } break;
                //case BinDefine.Bin148: { return "148"; } break;
                //case BinDefine.Bin149: { return "149"; } break;
                //case BinDefine.Bin150: { return "150"; } break;
                //case BinDefine.Bin151: { return "151"; } break;
                //case BinDefine.Bin152: { return "152"; } break;
                //case BinDefine.Bin153: { return "153"; } break;
                //case BinDefine.Bin154: { return "154"; } break;
                //case BinDefine.Bin155: { return "155"; } break;
                //case BinDefine.Bin156: { return "156"; } break;
                //case BinDefine.Bin157: { return "157"; } break;
                //case BinDefine.Bin158: { return "158"; } break;
                //case BinDefine.Bin159: { return "159"; } break;
                //case BinDefine.Bin160: { return "160"; } break;
                //case BinDefine.Bin161: { return "161"; } break;
                //case BinDefine.Bin162: { return "162"; } break;
                //case BinDefine.Bin163: { return "163"; } break;
                //case BinDefine.Bin164: { return "164"; } break;
                //case BinDefine.Bin165: { return "165"; } break;
                //case BinDefine.Bin166: { return "166"; } break;
                //case BinDefine.Bin167: { return "167"; } break;
                //case BinDefine.Bin168: { return "168"; } break;
                //case BinDefine.Bin169: { return "169"; } break;
                //case BinDefine.Bin170: { return "170"; } break;
                //case BinDefine.Bin171: { return "171"; } break;
                //case BinDefine.Bin172: { return "172"; } break;
                //case BinDefine.Bin173: { return "173"; } break;
                //case BinDefine.Bin174: { return "174"; } break;
                //case BinDefine.Bin175: { return "175"; } break;
                //case BinDefine.Bin176: { return "176"; } break;
                //case BinDefine.Bin177: { return "177"; } break;
                //case BinDefine.Bin178: { return "178"; } break;
                //case BinDefine.Bin179: { return "179"; } break;
                //case BinDefine.Bin180: { return "180"; } break;
                //case BinDefine.Bin181: { return "181"; } break;
                //case BinDefine.Bin182: { return "182"; } break;
                //case BinDefine.Bin183: { return "183"; } break;
                //case BinDefine.Bin184: { return "184"; } break;
                //case BinDefine.Bin185: { return "185"; } break;
                //case BinDefine.Bin186: { return "186"; } break;
                //case BinDefine.Bin187: { return "187"; } break;
                //case BinDefine.Bin188: { return "188"; } break;
                //case BinDefine.Bin189: { return "189"; } break;
                //case BinDefine.Bin190: { return "190"; } break;
                //case BinDefine.Bin191: { return "191"; } break;
                //case BinDefine.Bin192: { return "192"; } break;
                //case BinDefine.Bin193: { return "193"; } break;
                //case BinDefine.Bin194: { return "194"; } break;
                //case BinDefine.Bin195: { return "195"; } break;
                //case BinDefine.Bin196: { return "196"; } break;
                //case BinDefine.Bin197: { return "197"; } break;
                //case BinDefine.Bin198: { return "198"; } break;
                //case BinDefine.Bin199: { return "199"; } break;

            }
            return "-";
        }
        public static void RunFile(string exePath, string argument)
        {
            string filename = Path.GetFileNameWithoutExtension(exePath);
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = exePath;
            // if (!FileAlreadyExist(exePath))
            {
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //process.StartInfo.RedirectStandardOutput = true;
                //process.StartInfo.RedirectStandardInput = true;
                process.Start();
                //process.StandardInput.AutoFlush = true;
            }
        }

        public static IndexTime IndexBF_PickIndexTime;
        public static IndexTime IndexBF_PlaceIndexTime;
        public static IndexTime IndexBF_MoveIndexTime;
        public static IndexTime IndexBIB_HDT_PickIndexTime;
        public static IndexTime IndexBIB_HDT_PlaceIndexTime;
        public static IndexTime IndexBIB_HDT_MoveIndexTime;

        //委派
        public delegate void ShowAlarmDelegate(string AlarmLevel, int ErrorCode, params object[] args);
        public delegate bool LotEndDelegate();
        public delegate void LotStartDelegate(string Lotno, int Lotquantuty);
        public static LotEndDelegate m_LotEndDelegate = null;
        public static LotStartDelegate m_LotStartDelegate = null;
        public static ShowAlarmDelegate m_ShowAlarm = null;
        public static PRODUCTION_MODE m_ProductionMode = PRODUCTION_MODE.NONE;

        public static string BoardDataLinkPath = "D:\\BoardDataLink\\BoardDataLink\\BoardDataLink.exe";

        public static JOEESYS OEE = new JOEESYS();
    }

    public class LotInformation_SM
    {
        //客戶編號(ProV/...)
        public int CustomerID { get; set; }
        //連線樣式(OffLine/...)
        public int LinkType { get; set; }
        //生產模式(None/Load/Unload)
        public int ProductionMode { get; set; }
        //批號
        public string LotNo { get; set; }
        //材料數量
        public int Quantity { get; set; }
        //產品名稱
        public string ProductName { get; set; }
        //板樣式
        public string BoardType { get; set; }
        //Block Col 數量 (BXN)
        public int BlockColCount { get; set; }
        //Block Row 數量 (BYN)
        public int BlockRowCount { get; set; }
        //Dut Col 數量 (DXN)
        public int DutColCount { get; set; }
        //Dut Row 數量 (DYN)
        public int DutRowCount { get; set; }
        //板號
        public string BoardID { get; set; }
        //OP ID
        public string OpID { get; set; }
        //Machine ID

        public string SocketName { get; set; }

        public string BoardQty { get; set; }

        public string ICWorkQty { get; set; }

        public string GoodDeviceQty { get; set; }

        public string StartTime { get; set; }//start lot time 

        public string RunningTime { get; set; }//running time 

        public string Running { get; set; }//running time 

        public string JamRate { get; set; }

        public string JamTotal { get; set; }

        public string MTBF { get; set; }

        public string MTBA { get; set; }

        public string ForceUnload { get; set; }

        public string SortBinQty { get; set; }//Format 1,2,3,4,....,34

        public string MachineNo { get; set; }

        public string MachineState { get; set; }

        public string UPH { get; set; }

        public string ConnectCk { get; set; }

        public string Reserve1 { get; set; }//PTI Setting

        public string Reserve2 { get; set; }//Aher Setting TestProgram Name

        public string Reserve3 { get; set; }

        public string Reserve4 { get; set; }

        public string Reserve5 { get; set; }

        public string Reserve6 { get; set; }

        public string Reserve7 { get; set; }

        public string Reserve8 { get; set; }

        public string Reserve9 { get; set; }

        public string Reserve10 { get; set; }

        public LotInformation_SM()
        {
            CustomerID = 0;
            LinkType = 0;
            ProductionMode = 0;
            LotNo = "";
            Quantity = 0;
            ProductName = "";
            BoardType = "";
            BlockColCount = 0;
            BlockRowCount = 0;
            DutColCount = 0;
            DutRowCount = 0;
            BoardID = "";
            OpID = "";

            SocketName = "";

            BoardQty = "";

            ICWorkQty = "";

            GoodDeviceQty = "";

            StartTime = "";

            RunningTime = "";

            Running = "";

            JamRate = "";

            JamTotal = "";

            MTBF = "";

            MTBA = "";

            ForceUnload = "";

            SortBinQty = "";

            MachineNo = "";

            MachineState = "";

            UPH = "";

            ConnectCk = "";

            Reserve1 = "";

            Reserve2 = "";

            Reserve3 = "";

            Reserve4 = "";

            Reserve5 = "";

            Reserve6 = "";

            Reserve7 = "";

            Reserve8 = "";

            Reserve9 = "";

            Reserve10 = "";

        }
        //深層複製類別
        public LotInformation_SM Clone()
        {
            LotInformation_SM lotInfo = new LotInformation_SM();

            lotInfo.CustomerID = this.CustomerID;
            lotInfo.LinkType = this.LinkType;
            lotInfo.ProductionMode = this.ProductionMode;
            lotInfo.LotNo = this.LotNo;
            lotInfo.Quantity = this.Quantity;
            lotInfo.ProductName = this.ProductName;
            lotInfo.BoardType = this.BoardType;
            lotInfo.BlockColCount = this.BlockColCount;
            lotInfo.BlockRowCount = this.BlockRowCount;
            lotInfo.DutColCount = this.DutColCount;
            lotInfo.DutRowCount = this.DutRowCount;
            lotInfo.BoardID = this.BoardID;
            lotInfo.OpID = this.OpID;
            lotInfo.SocketName = this.SocketName;
            lotInfo.BoardQty = this.BoardQty;
            lotInfo.ICWorkQty = this.ICWorkQty;
            lotInfo.GoodDeviceQty = this.GoodDeviceQty;
            lotInfo.StartTime = this.StartTime;
            lotInfo.RunningTime = this.RunningTime;
            lotInfo.Running = this.Running;
            lotInfo.JamRate = this.JamRate;
            lotInfo.JamTotal = this.JamTotal;
            lotInfo.MTBA = this.MTBA;
            lotInfo.MTBF = this.MTBF;
            lotInfo.ForceUnload = this.ForceUnload;
            lotInfo.SortBinQty = this.SortBinQty;
            lotInfo.MachineNo = this.MachineNo;
            lotInfo.MachineState = this.MachineState;
            lotInfo.UPH = this.UPH;
            lotInfo.ConnectCk = this.ConnectCk;
            lotInfo.Reserve1 = this.Reserve1;
            lotInfo.Reserve2 = this.Reserve2;
            lotInfo.Reserve3 = this.Reserve3;
            lotInfo.Reserve4 = this.Reserve4;
            lotInfo.Reserve5 = this.Reserve5;
            lotInfo.Reserve6 = this.Reserve6;
            lotInfo.Reserve7 = this.Reserve7;
            lotInfo.Reserve8 = this.Reserve8;
            lotInfo.Reserve9 = this.Reserve9;
            lotInfo.Reserve10 = this.Reserve10;


            return lotInfo;
        }

    }

    public class IndexTime
    {
        private double m_starttime;
        private double m_endtime;
        private double m_indextime;
        private double m_maxtime;
        private double m_mintime;
        private double m_average;
        private double m_cycletime;
        private double m_maxcycletime;
        private double m_mincycletime;
        private double m_averagecycletime;
        private double m_cycletime1;
        private double m_cycletime2;
        private bool state;
        private GetTickCountEx m_tick;
        public IndexTime()
        {
            m_tick = new GetTickCountEx();
            m_endtime = 0;
            m_starttime = 0;
            m_indextime = 0;
            m_maxtime = 0;
            m_mintime = 999999;
            m_average = 0;
            m_cycletime = 0;
            m_cycletime1 = 0;
            m_cycletime2 = 0;
            m_maxcycletime = 0;
            m_mincycletime = 999999;
            m_averagecycletime = 0;
            state = false;
        }
        public void start()
        {
            m_starttime = m_tick.Value;
            if (!state)
            {
                m_cycletime1 = m_starttime;
            }
            else
            {
                m_cycletime2 = m_starttime;
            }

            if (m_cycletime1 > 0 && m_cycletime2 > 0)
            {
                m_cycletime = Math.Abs(m_cycletime1 - m_cycletime2) / 1000;
                if (m_cycletime > m_maxcycletime)
                {
                    m_maxcycletime = m_cycletime;
                }
                if (m_cycletime < m_mincycletime)
                {
                    m_mincycletime = m_cycletime;
                }
                if (m_averagecycletime <= 0)
                {
                    m_averagecycletime = m_cycletime;
                }
                else
                {
                    m_averagecycletime += m_cycletime;
                    m_averagecycletime /= 2;
                }
            }
            state = !state;
        }
        public void end()
        {
            if (m_starttime > 0)
            {
                m_endtime = m_tick.Value;
                m_indextime = Math.Abs(m_endtime - m_starttime);
                if (m_indextime > m_maxtime)
                {
                    m_maxtime = m_indextime;
                }
                if (m_indextime < m_mintime)
                {
                    m_mintime = m_indextime;
                }
                if (m_indextime > 0)
                {
                    if (m_average <= 0)
                    {
                        m_average = m_indextime;
                    }
                    else
                    {
                        m_average += m_indextime;
                        m_average /= (double)2;
                    }

                }
                m_starttime = 0;
            }
        }
        public double MaxTime
        {
            get
            {
                return m_maxtime;
            }
        }
        public double MinTime
        {
            get
            {
                return m_mintime;
            }
        }
        public double AverageTime
        {
            get
            {
                return m_average;
            }
        }
        public double Time
        {
            get
            {
                return m_indextime;
            }
        }
        public double CycleTime
        {
            get
            {
                return m_cycletime;
            }
        }
        public double CycleMaxTime
        {
            get
            {
                return m_maxcycletime;
            }
        }
        public double CycleMinTime
        {
            get
            {
                return m_mincycletime;
            }
        }
        public double CycleAverageTime
        {
            get
            {
                return m_averagecycletime;
            }
        }

    }

    public class BookingInfo
    {
        public BIBStageID StageID { get; set; }
        /// <summary>
        /// DiePak Socket X 座標 (Col, 行數)
        /// </summary>
        public int SocketX { get; set; }

        /// <summary>
        /// DiePak Socket Y 座標 (Row, 列數)
        /// </summary>
        public int SocketY { get; set; }

        /// <summary>
        /// KIT Block ID
        /// </summary>
        public KitBlockID BlockID { get; set; }

        public int KitID { get; set; }

        /// <summary>
        /// DiePak Socket / KIT Shuttle Block Map
        /// </summary>
        public string Map { get; set; }

        /// <summary>
        /// DiePak Socket / KIT Shuttle Block 狀態
        /// </summary>
        public VehicleState State { get; set; }

        /// <summary>
        /// BookingInfo 建構子(初始化 KIT Shuttle Block Booking Info 物件)
        /// </summary>
        /// <param name="x">初始化 DiePak Socket X 座標 (Col, 行數)</param>
        /// <param name="y">初始化 DiePak Socket Y 座標 (Row, 列數)</param>
        /// <param name="bid">初始化 Kit Block ID</param>
        /// <param name="map">初始化 DiePak Socket / Kit Block Map</param>
        /// <param name="state">初始化 DiePak Socket / Kit Block 狀態</param>
        public BookingInfo(int x, int y, KitBlockID bid, string map, VehicleState state, BIBStageID stageid)
        {
            this.SocketX = x;
            this.SocketY = y;
            //this.ShuttleID = sid;
            this.BlockID = bid;
            this.Map = map;
            this.State = state;
            this.StageID = stageid;
        }

        /// <summary>
        /// BookingInfo 建構子(初始化 DiePak Socket Booking Info 物件)
        /// </summary>
        public BookingInfo(int x, int y, string map, VehicleState state, BIBStageID stageid)
            : this(x, y, KitBlockID.NONE, map, state, stageid)
        {
        }

        /// <summary>
        /// BookingInfo 建構子(無參數)
        /// </summary>
        public BookingInfo()
            : this(-1, -1, KitBlockID.NONE, "", VehicleState.NONE, BIBStageID.NONE)
        {
        }
    }

    /// <summary>
    /// 生產統計
    /// </summary>
    public class QtyCounter
    {
        private ulong m_count = 0;

        public QtyCounter()
        {
            m_count = 0;
        }

        public ulong Qty { get { return m_count; } }

        public void Reset()
        {
            m_count = 0;
        }

        public ulong Add(ulong count)
        {
            m_count += count;
            return m_count;
        }

    }

    public class CommonObj
    {
    }
    #endregion

    #region Interface
    public interface TRM_interface
    {
        /// <summary>
        /// 設定是否可已開始進行歸零
        /// </summary>
        void SetCanRunHome();

        //void SetCanLotEnd();

        void DataReset();

        bool IsTRMSafety();

        TRMState GetTransferState();

        ThreeValued SetTransferState(TRMState sate);

        //ThreeValued SetActionCommand_TRM(TRMStation station, ACTIONMODE mode, BasePosInfo Pos);
        ThreeValued SetActionCommand_TRM(TRMStation station, ACTIONMODE mode);

        ThreeValued GetActionResult_TRM();
    }

    public interface CHM_interface
    {
        /// <summary>
        /// 設定是否可已開始進行歸零
        /// </summary>
        void SetCanRunHome();

        //void SetCanLotEnd();

        void DataReset();

        //void SetCHMState(string[] state);

        //string GetCHMState();

        ThreeValued IsAxisZHomeOK();

        ThreeValued SetRobotSafety();

        ThreeValued SetActionCommand_CHM(ACTIONMODE mode);//, CH_CHECKMODE CKMode, BasePosInfo pos);

        ThreeValued GetActionResult_CHM();

        //2022/01/20 新增
        void Protection_CHM(bool Val);
    }

    public interface BSM_interface
    {
        /// <summary>
        /// 設定是否可已開始進行歸零
        /// </summary>
        void SetCanRunHome();

        //void SetCanLotEnd();

        void DataReset();

        BIBStageID IsBIBStageReady(BIBStageSate state);

        BIBStageSate GetBoardStageState(BIBStageID id);

        ThreeValued SetBoardStageState(BIBStageID id, BIBStageSate state);

        ThreeValued LockBIBStage(BIBStageModuleOwner owner, BIBStageID id);

        ThreeValued UnlockBIBStage(BIBStageModuleOwner owner, BIBStageID id);

        ThreeValued SetActionCommand_BSM(BIBStageModuleOwner owner, BIBStageID id, ACTIONMODE mode, BasePosInfo pos);

        ThreeValued GetActionResult_BSM(BIBStageID id);
    }

    public interface DHT_interface
    {
        /// <summary>
        /// 設定是否可已開始進行歸零
        /// </summary>
        void SetCanRunHome();

        //void SetCanLotEnd();

        void DataReset();

        ThreeValued IsAxisZHomeOK();

        /// <summary>
        /// 判斷Z軸是否安全
        /// /<returns></returns>
        bool IsAxisZSafety(BoardHeadID id);

        BasePosInfo GetBasicInfo(PnPStation station, BoardHeadID id);

        ThreeValued SetActionCommand_HDT(BoardHeadID id, ACTIONMODE mode, BasePosInfo pos);

        ThreeValued GetActionResult_HDT(BoardHeadID id);
    }

    public interface KSM_interface
    {
        /// <summary>
        /// 設定是否可已開始進行歸零
        /// </summary>
        void SetCanRunHome();

        //void SetCanLotEnd();

        void DataReset();

        KitShuttleID IsTransferShuttleReady(VehicleState state);

        ThreeValued LockTransferShuttle(KitShuttleOwner owner, KitShuttleID id);

        ThreeValued UnlockTransferShuttle(KitShuttleOwner owner, KitShuttleID id);

        ThreeValued IsTransferShuttleLocked(KitShuttleID id);

        ThreeValued SetTransferShuttleState(KitShuttleID id, VehicleState state);

        ThreeValued SetActionCommand_KSM(KitShuttleOwner owner, KitShuttleID id, ACTIONMODE mode, BasePosInfo pos);

        ThreeValued GetActionResult_KSM(KitShuttleOwner owner, KitShuttleID id);
    }

    public interface BFM_interface
    {
        /// <summary>
        /// 設定是否可已開始進行歸零
        /// </summary>
        void SetCanRunHome();

        //void SetCanLotEnd();

        void DataReset();

        ThreeValued IsAxisZHomeOK(BowlFeederID ID);

        /// <summary>
        /// 判斷Z軸是否安全
        /// /<returns></returns>
        bool IsAxisZSafety(BowlFeederID id);

        ThreeValued SetActionCommand_BFM(BowlFeederID id, ACTIONMODE mode, BasePosInfo pos);

        ThreeValued GetActionResult_BFM(BowlFeederID id);

    }
    #endregion
}
