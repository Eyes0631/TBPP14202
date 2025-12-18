using System;
using PaeLibProVSDKEx;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;
using PaeLibGeneral;

namespace CommonObj
{
    public class BoardBookingSystem
    {
        /// <summary>
        /// KIT 上的 Socket 數量常數
        /// </summary>
        private const int KitBlockCount = 1;
        private const int KitCols = 4;
        private const int KitRows = 4;

        /// <summary>
        ///  Socket 取放位置預約清單
        /// </summary>
        //private Dictionary<BIBStageID, List<BookingInfo>> BoardBookingList = null;
        private List<BookingInfo> BoardBookingList = null;
        //private List<BookingInfo> StageABookingList = null;
        //private List<BookingInfo> StageBBookingList = null;

        private List<BookingInfo> WorkStageBookingList = null;

        public BookingInfo CurrentWorkInfo = null;

        /// <summary>
        /// Kit Shuttle 取放預約位置清單
        /// </summary>
        private Dictionary<KitShuttleID, List<BookingInfo>> KitShuttleBookingList = null;

        public BookingInfo CurrentKitBlockInfo = null;


        /// <summary>
        /// Socket X 數量
        /// </summary>
        private int SocketCols;

        /// <summary>
        /// Socket Y 數量
        /// </summary>
        private int SocketRows;

        public bool IsLastBlock = false;

        /// <summary>
        /// 建構子
        /// </summary>
        public BoardBookingSystem()
        {
            this.BoardBookingList = new List<BookingInfo>();
            //this.BoardBookingList = new Dictionary<BIBStageID, List<BookingInfo>> 
            //{
            //    {BIBStageID.BIBStageA, new List<BookingInfo>()},
            //    {BIBStageID.BIBStageB, new List<BookingInfo>()}
            //};
            this.KitShuttleBookingList = new Dictionary<KitShuttleID, List<BookingInfo>>
            {
                {KitShuttleID.TransferShuttleA, new List<BookingInfo>()},
                {KitShuttleID.TransferShuttleB, new List<BookingInfo>()}
            };

            this.WorkStageBookingList = new List<BookingInfo>();
        }

        public void ClearBookingList()
        {
            this.BoardBookingList.Clear();
        }

        public bool CreateBoardBookingList(PRODUCTION_MODE mode, BoardMapInfo board_map_info, BIBStageID StageID, int BookingCount = -1)
        {
            bool bRet = false;
            if (this.BoardBookingList != null)
            {
                //清空
                //this.BoardBookingList.Clear();
                //改為另外呼叫Clear

                //map 分割
                string board_map = board_map_info.BoardMap;
                this.SocketCols = board_map_info.SocketCols;
                this.SocketRows = board_map_info.SocketRows;
                int board_map_len = board_map.Length;
                int semicolon_idx = board_map.LastIndexOf(';');
                if ((board_map_len - semicolon_idx).Equals(1))
                {
                    //最後多出一個;需移除
                    board_map = board_map.Remove(semicolon_idx);
                }
                int iii = board_map.LastIndexOf(',');
                if (board_map.LastIndexOf(',').Equals(board_map_len-1))
                {
                    board_map = board_map.Remove(board_map_len -1);
                }

                string[] Map_array = board_map.Split(',');
                int m_BookingCount = 0;

                if (Map_array.Length == board_map_info.SocketCols * board_map_info.SocketRows)
                {
                    for (int i = 0; i < Map_array.Length; i++)
                    {
                        Point socket_point = board_map_info.SocketIndexToPoint(i);

                        string socket_map = Map_array[socket_point.X * board_map_info.SocketCols + socket_point.Y];
                        if (mode.HasFlag(PRODUCTION_MODE.LOAD))
                        {
                            socket_map = Regex.Replace(socket_map, "[-XM]", "X", RegexOptions.IgnoreCase);
                            socket_map = Regex.Replace(socket_map, "[0-9]", "0");
                        }
                        else //UNLOAD
                        {
                            socket_map = Regex.Replace(socket_map, "[-XM]", "X", RegexOptions.IgnoreCase);
                            //socket_map = Regex.Replace(socket_map, "[0]", "X");
                            //socket_map = Regex.Replace(socket_map, "[2-9]", "2");
                        }

                        //if (socket_map.Contains("0") || socket_map.Contains("1") || socket_map.Contains("2"))
                        {
                            if (mode.HasFlag(PRODUCTION_MODE.LOAD))
                            { 
                                this.BoardBookingList.Add(new BookingInfo(socket_point.X, socket_point.Y, socket_map, VehicleState.UNTESTED_EMPTY, StageID));
                            }
                            if (mode.HasFlag(PRODUCTION_MODE.UNLOAD))
                            {
                                this.BoardBookingList.Add(new BookingInfo(socket_point.X, socket_point.Y, socket_map, VehicleState.TESTED_FULL, StageID));
                            }
                        }
                    }
                    bRet = true;
                }
            }
            return bRet;
        }

        public bool CancelBoardBookingList()
        {
            bool bRet = false;
            if (BoardBookingList != null)
            {
                foreach (BookingInfo booking_ingo in this.BoardBookingList)
                {
                    if (booking_ingo.State.Equals(VehicleState.TESTED))
                    {
                        booking_ingo.State = VehicleState.TESTED_EMPTY;
                    }

                    if (booking_ingo.State.Equals(VehicleState.UNTESTED_EMPTY))
                    {
                        booking_ingo.State = VehicleState.UNTESTED_FULL;
                    }

                    if (booking_ingo.State.Equals(VehicleState.UNTESTED_EMPTY_BOOKED))
                    {
                        booking_ingo.State = VehicleState.UNTESTED_FULL;
                    }
                }
            }
            return bRet;
        }

        /// <summary>
        /// 取得List未完成數量
        /// </summary>
        /// <returns></returns>
        public int BoardBookingListCount(BIBStageID id = BIBStageID.NONE)
        {
            int iRet = -1;
            //List<BookingInfo> socket_list = this.BoardBookingList.FindAll(x => ((x.State != VehicleState.UNTESTED_FULL) && (x.State != VehicleState.TESTED_EMPTY)));
            List<BookingInfo> socket_list = null;
            if (id.Equals(BIBStageID.NONE))
            {
                socket_list = this.BoardBookingList.FindAll(x => ((x.State != VehicleState.UNTESTED_FULL) && (x.State != VehicleState.TESTED_EMPTY) && (x.Map != "X")));
            }
            else
            {
                socket_list = this.BoardBookingList.FindAll(x => ((x.State != VehicleState.UNTESTED_FULL) && (x.State != VehicleState.TESTED_EMPTY) && (x.Map != "X") && (x.StageID.Equals(id))));
            }

            List<BookingInfo> uncompleted_socket_list = new List<BookingInfo>();
            uncompleted_socket_list.Clear();
            foreach (BookingInfo info in socket_list)
            {
                int idx = uncompleted_socket_list.FindIndex(x => (x.SocketX.Equals(info.SocketX) & x.SocketY.Equals(info.SocketY)));
                if (idx < 0)
                {
                    uncompleted_socket_list.Add(info);
                }
            }
            iRet = uncompleted_socket_list.Count;
            return iRet;
        }

        /// <summary>
        /// 設定 Kit Shuttle 的 TaryData Mask Map
        /// <para>Mask Map 的 bin 值只有 Bad Socket 與 Empty 兩種</para>
        /// </summary>
        /// <param name="td_kit_shuttle">Kit Shuttle 的 TrayDataEx 物件</param>
        /// <param name="kit_block_id">Kit Shuttle 的 Block ID</param>
        /// <param name="socket_mask_map">Kit Shuttle 的 Block Map 字串</param>
        /// <returns></returns>
        private bool SetKitShuttleMaskMap(TrayDataEx td_kit_shuttle, int Socket_idx, string socket_mask_map)
        {
            bool bRet = false;

            if (td_kit_shuttle != null)
            {
                if (string.IsNullOrWhiteSpace(socket_mask_map))
                {
                    //int cell_x = Socket_idx / KitCols;
                    //int cell_y = Socket_idx % KitRows;
                    int cell_x = Socket_idx % KitRows;
                    int cell_y = Socket_idx / KitCols;
                    td_kit_shuttle.SetBin(0, 0, cell_x, cell_y, (byte)BinDefine.MaskedSocket, false);
                }
                else
                {
                    int cell_x = Socket_idx % KitRows;
                    int cell_y = Socket_idx / KitCols;
                    if (socket_mask_map.Equals("X"))
                    {
                        td_kit_shuttle.SetBin(0, 0, cell_x, cell_y, (byte)BinDefine.BadSocket, false);
                    }
                    else
                    {
                        td_kit_shuttle.SetBin(0, 0, cell_x, cell_y, (byte)BinDefine.Empty, false);
                    }
                }
                bRet = true;
                //if (string.IsNullOrWhiteSpace(socket_mask_map))
                //{
                //    // 若 Socket Mask Map 為空字串，則設定整個 Kit Block 為 Bad Socket
                //    int cell_count = (td_kit_shuttle.Cols * td_kit_shuttle.Cols);
                //    int kit_block_y = -1;
                //    switch (kit_block_id)
                //    {
                //        case KitBlockID.TOP_KIT_BLOCK: { kit_block_y = 0; } break;
                //        case KitBlockID.BOTTOM_KIT_BLOCK: { kit_block_y = 1; } break;
                //    }
                //    if (kit_block_y >= 0)
                //    {
                //        for (int i = 0; i < cell_count; ++i)
                //        {
                //            int cell_x = (i % td_kit_shuttle.Cols);
                //            int cell_y = (i / td_kit_shuttle.Cols);
                //            //Bad Socket
                //            td_kit_shuttle.SetBin(0, kit_block_y, cell_x, cell_y, (byte)BinDefine.MaskedSocket, false);
                //        }
                //        bRet = false;
                //    }
                //}
                //else
                //{
                //    // Socket Mask Map 字串分割
                //    int board_map_len = socket_mask_map.Length;
                //    int board_map_last_semicolon_idx = socket_mask_map.LastIndexOf(',');
                //    if ((board_map_len - board_map_last_semicolon_idx).Equals(1)) // 判斷字串結尾是否為逗號(,)
                //    {
                //        // 移除字串結尾的逗號(,)，避免字串分割時，多一個空字串的陣列元素
                //        socket_mask_map = socket_mask_map.Remove(board_map_last_semicolon_idx);
                //    }
                //    char[] separator = { ',' };
                //    string[] socket_mask_map_array = socket_mask_map.Split(separator);

                //    // 計算 socket 內 cell 數量
                //    int cell_count = (td_kit_shuttle.Cols * td_kit_shuttle.Cols);
                //    // 檢查 map 長度與 cell 數量是否相符
                //    if (socket_mask_map_array.Length.Equals(cell_count))
                //    {
                //        int kit_block_y = -1;
                //        switch (kit_block_id)
                //        {
                //            case KitBlockID.TOP_KIT_BLOCK: { kit_block_y = 0; } break;
                //            case KitBlockID.BOTTOM_KIT_BLOCK: { kit_block_y = 1; } break;
                //        }
                //        if (kit_block_y >= 0)
                //        {
                //            for (int i = 0; i < cell_count; ++i)
                //            {
                //                int cell_x = (i % td_kit_shuttle.Cols);
                //                int cell_y = (i / td_kit_shuttle.Cols);
                //                if (socket_mask_map_array[i].Equals("X"))
                //                {
                //                    //Bad Socket
                //                    td_kit_shuttle.SetBin(0, kit_block_y, cell_x, cell_y, (byte)BinDefine.MaskedSocket, false);
                //                }
                //                else
                //                {
                //                    //Good Socket
                //                    td_kit_shuttle.SetBin(0, kit_block_y, cell_x, cell_y, (byte)BinDefine.Empty, false);
                //                }
                //            }
                //            bRet = true;
                //        }
                //    }
                //}
            }
            td_kit_shuttle.Refresh();
            return bRet;
        }

        public bool SetStageBookingInfo()
        {
            bool bRet = false;
            if (this.WorkStageBookingList != null)
            {
                this.WorkStageBookingList.Clear();

                if (this.BoardBookingList != null)
                {
                    for (int j = 0; j < SocketRows; j++)
                    {
                        for (int i = 0; i < SocketCols; i++)
                        {
                            // 指定 BoardBookingList 搜尋的狀態
                            VehicleState query_state = VehicleState.TESTED_FULL;
                            // 指定要設定給 work 的狀態
                            VehicleState work_info_state = VehicleState.EMPTY;
                            // 指定 BoardBookingList 搜尋出來的元素要修改的狀態
                            VehicleState socket_info_state = VehicleState.TESTED_FULL_BOOKED;

                            if (query_state.Equals(VehicleState.NONE) || work_info_state.Equals(VehicleState.NONE) || socket_info_state.Equals(VehicleState.NONE))
                            {
                                // 清除此治具台車中的所有項目
                                this.WorkStageBookingList.Clear();
                                return false;   //此處必須直接 return false，不可只用 break，否則離開迴圈後還是會回傳 true
                            }
                            else
                            {
                                // 從 BoardBookingList 取出符合 state 狀態之元素索引值
                                int idx = this.BoardBookingList.FindIndex(x => x.State.Equals(query_state));
                                if (idx >= 0)
                                {
                                    // 依索引值從 BoardBookingList 中取得 DiePakBookingInfo 物件資料(深複製)
                                    BookingInfo work_info = JGeneralTools.DeepCopy(this.BoardBookingList[idx]);
                                    // 設定 Block ID
                                    //kit_block_info.BlockID = kit_block_id;
                                    work_info.KitID = SocketRows * j + i + 1;
                                    // 修改 kit_block_info Block 的狀態改
                                    work_info.State = work_info_state;

                                    // 修改 BoardBookingList 中 Socket 的狀態改
                                    this.BoardBookingList[idx].State = socket_info_state;

                                    this.WorkStageBookingList.Add(work_info);
                                    bRet |= true;
                                    
                                }
                                else
                                {
                                    this.WorkStageBookingList.Add(new BookingInfo());
                                }
                            }                            
                        }
                    }
                }
            }
            return bRet;
        }

        /// <summary>
        /// 設定 KIT Shuttle 取放預約位置資訊 (2 Blocks)
        /// </summary>
        /// <param name="shuttle_id">指定的治具台車編號</param>
        /// <param name="shuttle_state">指定的治具台車狀態</param>
        /// <param name="td_kit_shuttle">Kit Shuttle 的 TrayDataEx 物件</param>
        /// <returns>true:完成，false:失敗</returns>
        public bool SetKitShuttleBookingInfo(KitShuttleID shuttle_id, VehicleState shuttle_state, TrayDataEx td_kit_shuttle = null)
        {
            bool bRet = false;
            if (this.KitShuttleBookingList != null)
            {
                // 檢查此治具台車 ID 是否存在於清單中
                if (this.KitShuttleBookingList.ContainsKey(shuttle_id))
                {
                    if (this.KitShuttleBookingList[shuttle_id] != null)
                    {
                        //if (shuttle_state.Equals(VehicleState.UNTESTED_EMPTY_CHECKED))
                        //{
                        //    if (this.KitShuttleBookingList[shuttle_id].FindIndex(x => x.State == (VehicleState.UNTESTED)) >= 0)//20210527 poyao 
                        //    {
                        //        return false;
                        //    }
                        //}
                        //else
                        //{
                        //    if (this.KitShuttleBookingList[shuttle_id].FindIndex(x => x.State == (VehicleState.EMPTY)) >= 0)//20210527 poyao 
                        //    {
                        //        return false;
                        //    }
                        //}
                        // 清除此治具台車中的所有項目
                        this.KitShuttleBookingList[shuttle_id].Clear();

                        if (this.BoardBookingList != null)
                        {
                            //foreach (KitBlockID kit_block_id in Enum.GetValues(typeof(KitBlockID)))
                            for (int i = 0; i < KitCols * KitRows; i++)
                            {
                                //if (kit_block_id != KitBlockID.NONE)
                                {
                                    // 指定 BoardBookingList 搜尋的狀態
                                    VehicleState query_state = VehicleState.NONE;
                                    // 指定要設定給 KIT Block Info 的狀態
                                    VehicleState block_info_state = VehicleState.NONE;
                                    // 指定 BoardBookingList 搜尋出來的元素要修改的狀態
                                    VehicleState socket_info_state = VehicleState.NONE;
                                    switch (shuttle_state)
                                    {
                                        case VehicleState.TESTED_EMPTY:  //Unloading
                                            {
                                                query_state = VehicleState.TESTED_FULL;
                                                block_info_state = VehicleState.EMPTY;
                                                socket_info_state = VehicleState.TESTED_FULL_BOOKED;
                                            }
                                            break;

                                        case VehicleState.UNTESTED_EMPTY:    //Loading
                                            {
                                                query_state = VehicleState.UNTESTED_EMPTY;
                                                block_info_state = VehicleState.UNTESTED;
                                                socket_info_state = VehicleState.UNTESTED_EMPTY_BOOKED;
                                            }
                                            break;
                                    }
                                    if (query_state.Equals(VehicleState.NONE) || block_info_state.Equals(VehicleState.NONE) || socket_info_state.Equals(VehicleState.NONE))
                                    {
                                        // 清除此治具台車中的所有項目
                                        this.KitShuttleBookingList[shuttle_id].Clear();
                                        return false;   //此處必須直接 return false，不可只用 break，否則離開迴圈後還是會回傳 true
                                    }
                                    else
                                    {
                                        // 從 BoardBookingList 取出符合 state 狀態之元素索引值
                                        int idx = this.BoardBookingList.FindIndex(x => x.State.Equals(query_state));
                                        if (idx >= 0)
                                        {
                                            // 依索引值從 BoardBookingList 中取得 DiePakBookingInfo 物件資料(深複製)
                                            BookingInfo kit_block_info = JGeneralTools.DeepCopy(this.BoardBookingList[idx]);
                                            // 設定 Block ID
                                            //kit_block_info.BlockID = kit_block_id;
                                            kit_block_info.KitID = i + 1;
                                            // 修改 kit_block_info Block 的狀態改
                                            kit_block_info.State = block_info_state;

                                            // 修改 BoardBookingList 中 Socket 的狀態改
                                            this.BoardBookingList[idx].State = socket_info_state;

                                            // 設定 Kit Shuttle TrayData Map
                                            if (SetKitShuttleMaskMap(td_kit_shuttle, i, kit_block_info.Map))
                                            {
                                                // 將 kit_block_info 加入 KitShuttleBookingList 中
                                                this.KitShuttleBookingList[shuttle_id].Add(kit_block_info);
                                                bRet |= true;
                                            }
                                            else
                                            {
                                                // 清除此治具台車中的所有項目
                                                if (!bRet)
                                                {
                                                    this.KitShuttleBookingList[shuttle_id].Clear();
                                                    return false;   //此處必須直接 return false，不可只用 break，否則離開迴圈後還是會回傳 true
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // 設定 Kit Shuttle TrayData Map - All Bad Socket
                                            if (SetKitShuttleMaskMap(td_kit_shuttle, i, ""))
                                            {
                                                // Disabled this Block of KIT
                                                this.KitShuttleBookingList[shuttle_id].Add(new BookingInfo());
                                            }
                                            else
                                            {
                                                // 清除此治具台車中的所有項目
                                                if (!bRet)
                                                {
                                                    if (this.KitShuttleBookingList[shuttle_id].Count <= 0)
                                                    {
                                                        this.KitShuttleBookingList[shuttle_id].Clear();
                                                    }
                                                    return false;   //此處必須直接 return false，不可只用 break，否則離開迴圈後還是會回傳 true
                                                }
                                            }
                                        }
                                    }
                                }
                            }   //End of foreach() loop
                            // bRet = true;
                        }
                    }
                }
            }
            return bRet;
        }

        public bool GetWorkStageBlockInfo()
        {
            IsLastBlock = false;
            if (this.WorkStageBookingList != null)
            {
                foreach (KitBlockID stage_block_id in Enum.GetValues(typeof(KitBlockID)))
                {
                    int idx = this.WorkStageBookingList.FindIndex(x => (x.BlockID.Equals(stage_block_id) && x.State.Equals(VehicleState.EMPTY)));
                    if (idx >= 0)
                    {
                        // 檢查 Block Map 是否包含 Good Socket (若全是 Bad Socket 則直接將狀態改為 Done)
                        // 正常來說可以 Get 到 idx的項目，應該不會是全 bad socket 的 map
                        string block_map = this.WorkStageBookingList[idx].Map;
                        if (block_map.Contains("M") || block_map.Contains("X"))
                        {
                            // 將 CurrentKitBlockInfo 清空並將該位置 Kit Block 狀態改為 NONE (其實沒改應該也沒差，因為沒有對應的 Socket ，所以不用改 Socket 狀態)
                            // 會進入這裡的表示 Socket List 的數量為奇數，在 SetKitShuttleBookingInfo() 中會塞一個空的資料(new BookingInfo()) 進來
                            // 但是因為空資料的狀態為 NONE，所以應該 get 不到，idx 應該是 -1，所以正常來說應該不會進到這裡
                            //CurrentKitBlockInfo = null;
                            //this.KitShuttleBookingList[shuttle_id][idx].State = VehicleState.NONE;
                            CurrentWorkInfo = null;
                            this.WorkStageBookingList[idx].State = VehicleState.NONE;
                            
                        }
                        else
                        {
                            // 將搜尋到符合條件的治具 Block 資訊深複製一分至 CurrentKitBlockInfo (避免使用者使用時修改到原始資料)
                            //CurrentKitBlockInfo = JGeneralTools.DeepCopy(this.KitShuttleBookingList[shuttle_id][idx]);  //深複製
                            //if (this.KitShuttleBookingList[shuttle_id].Count.Equals(idx + 1))
                            CurrentWorkInfo = JGeneralTools.DeepCopy(this.WorkStageBookingList[idx]);
                            {
                                IsLastBlock = true;
                            }
                            return true;    //有找到就回傳 true
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 取得 KIT Block Infomation
        /// </summary>
        /// <param name="shuttle_id">指定的治具台車編號</param>
        /// <param name="shuttle_state">指定的治具台車狀態</param>
        /// <returns></returns>
        public bool GetKitBlockInfo(KitShuttleID shuttle_id, VehicleState shuttle_state)
        {
            IsLastBlock = false;
            if (this.KitShuttleBookingList != null)
            {
                // 檢查此治具台車 ID 是否存在於清單中
                if (this.KitShuttleBookingList.ContainsKey(shuttle_id))
                {
                    // 搜尋治具的每一個 Block 資料，檢查是否有符合指定狀態條件的
                    foreach (KitBlockID kit_block_id in Enum.GetValues(typeof(KitBlockID)))
                    {
                        // 略過治具 Block ID 為 NONE 的
                        //if (kit_block_id != KitBlockID.NONE)
                        {
                            int idx = -1;
                            // 依治具台車狀態來決定要搜尋的治具 Block 狀態
                            switch (shuttle_state)
                            {
                                case VehicleState.TESTED_EMPTY_BOOKED:  //Unloading
                                    {
                                        idx = this.KitShuttleBookingList[shuttle_id].FindIndex(x => (x.BlockID.Equals(kit_block_id) && x.State.Equals(VehicleState.EMPTY)));
                                    }
                                    break;

                                case VehicleState.UNTESTED_FULL:    //Loading
                                    {
                                        idx = this.KitShuttleBookingList[shuttle_id].FindIndex(x => (x.BlockID.Equals(kit_block_id) && x.State.Equals(VehicleState.UNTESTED)));
                                        if (idx >= 0)
                                        {
                                            // 檢查 Booking List 中預計要放 IC 的 Socket 狀態是否有包含 TESTED_FULL，若有則表示下板中，不可上板
                                            BookingInfo socket_info = this.KitShuttleBookingList[shuttle_id][idx];
                                            List<BookingInfo> socket_list = this.BoardBookingList.FindAll(x => (x.SocketX.Equals(socket_info.SocketX) && x.SocketY.Equals(socket_info.SocketY)));
                                            int unloading_socket_index = socket_list.FindIndex(x => x.State.HasFlag(VehicleState.TESTED_FULL));
                                            if (unloading_socket_index >= 0)
                                            {
                                                idx = -1;   //將 idx 改為 -1，避免進行上板
                                                //  CurrentKitBlockInfo = null;
                                            }
                                        }
                                    }
                                    break;
                            }
                            if (idx >= 0)
                            {
                                // 檢查 Block Map 是否包含 Good Socket (若全是 Bad Socket 則直接將狀態改為 Done)
                                // 正常來說可以 Get 到 idx的項目，應該不會是全 bad socket 的 map
                                string block_map = this.KitShuttleBookingList[shuttle_id][idx].Map;
                                if (block_map.Contains("0"))
                                {
                                    // 將搜尋到符合條件的治具 Block 資訊深複製一分至 CurrentKitBlockInfo (避免使用者使用時修改到原始資料)
                                    CurrentKitBlockInfo = JGeneralTools.DeepCopy(this.KitShuttleBookingList[shuttle_id][idx]);  //深複製
                                    if (this.KitShuttleBookingList[shuttle_id].Count.Equals(idx + 1))
                                    {
                                        IsLastBlock = true;
                                    }
                                    return true;    //有找到就回傳 true
                                }
                                else
                                {
                                    // 將 CurrentKitBlockInfo 清空並將該位置 Kit Block 狀態改為 NONE (其實沒改應該也沒差，因為沒有對應的 Socket ，所以不用改 Socket 狀態)
                                    // 會進入這裡的表示 Socket List 的數量為奇數，在 SetKitShuttleBookingInfo() 中會塞一個空的資料(new BookingInfo()) 進來
                                    // 但是因為空資料的狀態為 NONE，所以應該 get 不到，idx 應該是 -1，所以正常來說應該不會進到這裡
                                    CurrentKitBlockInfo = null;
                                    this.KitShuttleBookingList[shuttle_id][idx].State = VehicleState.NONE;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public Point GetKITBlockIndex(KitShuttleID kitid, KitBlockID blockid)
        {
            Point index = new Point();
            try
            {
                index.X = -1;
                index.Y = -1;
                if (this.KitShuttleBookingList != null)
                {
                    int find = KitShuttleBookingList[kitid].FindIndex(x => x.BlockID.Equals(blockid));
                    if (find >= 0)
                    {
                        index.X = KitShuttleBookingList[kitid][find].SocketX;
                        index.Y = KitShuttleBookingList[kitid][find].SocketY;
                    }

                }
            }
            catch (Exception EX)
            {
                index.X = -1;
                index.Y = -1;
            }
            return index;
        }

        public List<BookingInfo> GetBoardBookingList()
        {
            return JGeneralTools.DeepCopy(BoardBookingList);
        }

        public bool WorkBlockDone()
        {
            bool bRet = false;
            if ((this.BoardBookingList != null) && (this.WorkStageBookingList != null))
            {
                // 指定 BoardBookingList 搜尋的狀態
                VehicleState query_state = VehicleState.TESTED_FULL_BOOKED;
                // 指定要設定給 KIT Block Info 的狀態
                VehicleState block_info_state = VehicleState.TESTED;
                // 指定 BoardBookingList 搜尋出來的元素要修改的狀態
                VehicleState socket_info_state = VehicleState.TESTED_EMPTY;

                // 尋找 BoardBookingList 與 KitShuttleBookingList 中對應 CurrentKitBlockInfo 的項目
                int socket_idx = this.BoardBookingList.FindIndex(x => (x.SocketX.Equals(CurrentWorkInfo.SocketX) &&
                    x.SocketY.Equals(CurrentWorkInfo.SocketY) && x.State.Equals(query_state)));
                int idx = this.WorkStageBookingList.FindIndex(x => x.KitID.Equals(CurrentWorkInfo.KitID));
                // 清除 CurrentKitBlockInfo 的參照，避免使用到舊資料
                CurrentWorkInfo = null;
                // 確認是否都有找到 BoardBookingList 與 KitShuttleBookingList 中對應 CurrentKitBlockInfo 的項目
                //if ((socket_idx >= 0) && (block_idx >= 0))
                if ((socket_idx >= 0) && (idx >= 0))
                {
                    //this.KitShuttleBookingList[shuttle_id][id].State = block_info_state;
                    this.WorkStageBookingList[idx].State = block_info_state;
                    this.BoardBookingList[socket_idx].State = socket_info_state;
                    bRet = true;
                }
            }
            return bRet;
        }

        /// <summary>
        /// 設定 Kit Block 狀態
        /// </summary>
        /// <param name="shuttle_id">指定的治具台車編號</param>
        /// <param name="shuttle_state">指定的治具台車狀態</param>
        /// <returns></returns>
        public bool SocketBlockDone(KitShuttleID shuttle_id, VehicleState shuttle_state)
        {
            bool bRet = false;
            if ((this.BoardBookingList != null) && (this.KitShuttleBookingList != null))
            {
                // 檢查此治具台車 ID 是否存在於清單中
                if (this.KitShuttleBookingList.ContainsKey(shuttle_id))
                {
                    // 指定 BoardBookingList 搜尋的狀態
                    VehicleState query_state = VehicleState.NONE;
                    // 指定要設定給 KIT Block Info 的狀態
                    VehicleState block_info_state = VehicleState.NONE;
                    // 指定 BoardBookingList 搜尋出來的元素要修改的狀態
                    VehicleState socket_info_state = VehicleState.NONE;
                    // 依治具台車狀態來設定 BoardBookingList 與 KitShuttleBookingList 中對應項目的狀態
                    switch (shuttle_state)
                    {
                        case VehicleState.TESTED_EMPTY_BOOKED:  //Unloading
                            {
                                query_state = VehicleState.TESTED_FULL_BOOKED;
                                block_info_state = VehicleState.TESTED;
                                socket_info_state = VehicleState.TESTED_EMPTY;
                            }
                            break;

                        case VehicleState.UNTESTED_FULL:    //Loading
                            {
                                query_state = VehicleState.UNTESTED_EMPTY_BOOKED;
                                block_info_state = VehicleState.EMPTY;
                                socket_info_state = VehicleState.UNTESTED_FULL;
                            }
                            break;
                    }

                    // 尋找 BoardBookingList 與 KitShuttleBookingList 中對應 CurrentKitBlockInfo 的項目
                    int socket_idx = this.BoardBookingList.FindIndex(x => (x.SocketX.Equals(CurrentKitBlockInfo.SocketX) &&
                        x.SocketY.Equals(CurrentKitBlockInfo.SocketY) && x.State.Equals(query_state)));
                    //int block_idx = this.KitShuttleBookingList[shuttle_id].FindIndex(x => x.BlockID.Equals(CurrentKitBlockInfo.BlockID));
                    int id = this.KitShuttleBookingList[shuttle_id].FindIndex(x => x.KitID.Equals(CurrentKitBlockInfo.KitID));
                    // 清除 CurrentKitBlockInfo 的參照，避免使用到舊資料
                    CurrentKitBlockInfo = null;
                    // 確認是否都有找到 BoardBookingList 與 KitShuttleBookingList 中對應 CurrentKitBlockInfo 的項目
                    //if ((socket_idx >= 0) && (block_idx >= 0))
                    if ((socket_idx >= 0) && (id >= 0))
                    {
                        this.KitShuttleBookingList[shuttle_id][id].State = block_info_state;
                        this.BoardBookingList[socket_idx].State = socket_info_state;
                        bRet = true;
                    }
                }
            }
            return bRet;
        }

    }
}
