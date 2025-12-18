using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaeLibProVSDKEx;


namespace CommonObj
{
    /// <summary>
    /// Board Head 取放導航類別
    /// </summary>
    public class BoardHeadPnPNavigator
    {
        /// <summary>
        /// Board Socket X Pitch
        /// </summary>
        public int SocketXPitch { get; set; }

        /// <summary>
        /// Board Socket Y Pitch
        /// </summary>
        public int SocketYPitch { get; set; }

        /// <summary>
        /// KIT Block Y Pitch
        /// </summary>
        public int BlockYPitch { get; set; }

        /// <summary>
        /// DiePakHeadPnPNavigator 建構子
        /// </summary>
        public BoardHeadPnPNavigator()
        {
        }

        /// <summary>
        /// 計算 Board 取放位置
        /// <para>以 Board 左上角 Socket 座標 (0, 0) 為原點來計算相對位置</para>
        /// </summary>
        /// <param name="socket_info">預約的取放資訊</param>
        /// <returns>回傳相對於 Board Socket 原點的位置偏移量 (X, Y)，單位:um</returns>
        public Point CalcBoardPnPPos(BookingInfo socket_info)
        {
            Point pos = new Point();
            if (socket_info != null)
            {
                pos.X = (socket_info.SocketX * this.SocketXPitch);
                pos.Y = (socket_info.SocketY * this.SocketYPitch);
            }
            return pos;
        }

        /// <summary>
        /// 計算 DiePak 取放位置
        /// <para>以 DiePak 左上角 Socket 座標 (0, 0) 為原點來計算相對位置</para>
        /// </summary>
        /// <param name="head">DiePak Head TrayDataEx 物件</param>
        /// <param name="Board">Board TrayDataEx 物件</param>
        /// <param name="socket_info">預約的取放資訊</param>
        /// <returns>回傳相對於 DiePak Socket 原點的位置偏移量 (X, Y)，單位:um</returns>
        public Point CalcBoardPnPPos(TrayDataEx head, TrayDataEx board, BookingInfo socket_info)
        {
            Point pos = new Point();
            if ((head != null) && (board != null) && (socket_info != null))
            {
                pos.X = (socket_info.SocketX * this.SocketXPitch);
                pos.Y = (socket_info.SocketY * this.SocketYPitch);

                byte[] target_cell_bin = GlobalDefine.ResetBin;
                byte[] head_cell_bin = GlobalDefine.ResetBin;
                // 判斷 Socket 狀態來確認目前 DiePak Head 的生產模式
                switch (socket_info.State)
                {
                    case VehicleState.UNTESTED:
                        {
                            // 上板
                            target_cell_bin = GlobalDefine.EmptyBin;
                            head_cell_bin = GlobalDefine.PassBin;
                        }
                        break;

                    case VehicleState.EMPTY:
                        {
                            // 下板
                            target_cell_bin = GlobalDefine.AllBin;
                            head_cell_bin = GlobalDefine.EmptyBin;
                        }
                        break;
                }
                //設定 Socket 與 Head 的 cell state
                for (int i = 0; i < head.Cols; ++i)
                {
                    for (int j = 0; j < head.Rows; ++j)
                    {
                        byte cell_state = 1;
                        byte cell_bin = board.GetBin(0, 0, socket_info.SocketX, socket_info.SocketY);
                        byte head_bin = head.GetBin(0, 0, i, j, false);
                        if (target_cell_bin.Contains(cell_bin) && head_cell_bin.Contains(head_bin))
                        {
                            board.SetState(0, 0, socket_info.SocketX, socket_info.SocketY, cell_state, false);
                            head.SetState(0, 0, i, j, cell_state, false);
                        }
                    }
                }
                board.Refresh();
                head.Refresh();
            }
            return pos;
        }

        /// <summary>
        /// 計算 KIT 取放位置
        /// <para>以 KIT top block 為原點來計算相對位置</para>
        /// </summary>
        /// <param name="block_info">預約的取放資訊</param>
        /// <returns>回傳相對於 KIT top block 原點的位置偏移量 (X, Y)，單位:um</returns>
        public Point CalcKitPnPPos(BookingInfo block_info)
        {
            Point pos = new Point();
            if (block_info != null)
            {
                pos.X = 0;
                switch (block_info.BlockID)
                {
                    case KitBlockID.TOP_KIT_BLOCK:
                        {
                            pos.Y = 0;
                        }
                        break;

                    case KitBlockID.BOTTOM_KIT_BLOCK:
                        {
                            pos.Y = this.BlockYPitch;
                        }
                        break;
                }
            }
            return pos;
        }

        private int current_kitX = 0;
        private int current_kitY = 0;

        public Point PreCalcKitPnPPos(TrayDataEx head, TrayDataEx kit)
        {
            Point pos = new Point();

            byte[] target_cell_bin = GlobalDefine.EmptyBin;
            byte[] head_cell_bin = GlobalDefine.AllBin;

            for (int i = 0; i < head.Cols; ++i)
            {
                for (int j = 0; j < head.Rows; ++j)
                {
                    byte cell_state = 1;
                    byte cell_bin = 0;

                    bool Flag_break = false;
                    for (current_kitY = 0; current_kitY < kit.Rows; current_kitY++)
                    {
                        for (current_kitX = 0; current_kitX < kit.Cols; current_kitX++)
                        {
                            cell_bin = kit.GetBin(0, 0, current_kitX, current_kitY);
                            if (target_cell_bin.Contains(cell_bin))
                            {
                                Flag_break = true;
                                break;
                            }
                        }
                        if (Flag_break) break;
                    }

                    //byte head_bin = head.GetBin(0, 0, i, j, false);
                    //if (target_cell_bin.Contains(cell_bin) && head_cell_bin.Contains(head_bin))
                    //{
                    //    kit.SetState(0, 0, current_kitX, current_kitY, cell_state, false);
                    //    head.SetState(0, 0, i, j, cell_state, false);
                    //}
                }
            }
            kit.Refresh();
            head.Refresh();

            pos.X = current_kitX;
            pos.Y = current_kitY;

            return pos;
        }

        public Point CalcKitPnPPos(TrayDataEx head, TrayDataEx kit)
        {
            Point pos = new Point();

            byte[] target_cell_bin = GlobalDefine.EmptyBin;
            byte[] head_cell_bin = GlobalDefine.AllBin;

            for (int i = 0; i < head.Cols; ++i)
            {
                for (int j = 0; j < head.Rows; ++j)
                {
                    byte cell_state = 1;
                    byte cell_bin = 0;
                        
                    bool Flag_break = false;
                    for (current_kitY = 0; current_kitY < kit.Rows; current_kitY++)
                    {
                        for (current_kitX = 0; current_kitX < kit.Cols; current_kitX++)
                        {
                            cell_bin = kit.GetBin(0, 0, current_kitX, current_kitY);
                            if (target_cell_bin.Contains(cell_bin))
                            {
                                Flag_break = true;
                                break;
                            }
                        }
                        if (Flag_break) break;
                    }
                        
                    byte head_bin = head.GetBin(0, 0, i, j, false);
                    if (target_cell_bin.Contains(cell_bin) && head_cell_bin.Contains(head_bin))
                    {
                        kit.SetState(0, 0, current_kitX, current_kitY, cell_state, false);
                        head.SetState(0, 0, i, j, cell_state, false);
                    }
                }
            }
            kit.Refresh();
            head.Refresh();
            
            pos.X = current_kitX;
            pos.Y = current_kitY;

            return pos;
        }

        /// <summary>
        /// 計算 KIT 取放位置
        /// <para>以 KIT top block 為原點來計算相對位置</para>
        /// </summary>
        /// <param name="head">DiePak Head TrayDataEx 物件</param>
        /// <param name="kit">KIT TrayDataEx 物件</param>
        /// <param name="block_info">預約的取放資訊</param>
        /// <returns>回傳相對於 KIT top block 原點的位置偏移量 (X, Y)，單位:um</returns>
        public Point CalcKitPnPPos(TrayDataEx head, TrayDataEx kit, BookingInfo block_info)
        {
            Point pos = new Point();
            if (block_info != null)
            {
                pos.X = 0;
                byte[] target_cell_bin = GlobalDefine.ResetBin;
                byte[] head_cell_bin = GlobalDefine.ResetBin;
                // 判斷 Socket 狀態來確認目前 DiePak Head 的生產模式
                switch (block_info.State)
                {
                    case VehicleState.UNTESTED:
                        {
                            // 上板
                            target_cell_bin = GlobalDefine.PassBin;
                            head_cell_bin = GlobalDefine.EmptyBin;
                        }
                        break;

                    case VehicleState.EMPTY:
                        {
                            // 下板
                            target_cell_bin = GlobalDefine.EmptyBin;
                            head_cell_bin = GlobalDefine.AllBin;
                        }
                        break;
                }
                //target_cell_bin = GlobalDefine.PassBin;
                //head_cell_bin = GlobalDefine.EmptyBin;
                //設定 Kit Block 與 Head 的 cell state
                for (int i = 0; i < head.Cols; ++i)
                {
                    for (int j = 0; j < head.Rows; ++j)
                    {
                        // 2020-12-16 Jay Tsao fixed - 修正 Kit State 設定錯誤位置的問題
                        //int kit_block_id = -1;
                        
                        byte cell_state = 1;
                        //byte cell_bin = kit.GetBin(0, 0, i, j);
                        byte cell_bin = 0;
                        
                        bool Flag_break = false;
                        for (current_kitY = 0; current_kitY < kit.Rows; current_kitY++)
                        {
                            for (current_kitX = 0; current_kitX < kit.Cols; current_kitX++)
                            {
                                cell_bin = kit.GetBin(0, 0, current_kitX, current_kitY);
                                if (target_cell_bin.Contains(cell_bin))
                                {
                                    Flag_break = true;
                                    break;
                                }
                            }
                            if (Flag_break) break;
                        }
                        
                        //byte cell_bin = kit.GetBin(0, 0, block_info.SocketX, block_info.SocketY);
                        byte head_bin = head.GetBin(0, 0, i, j, false);
                        if (target_cell_bin.Contains(cell_bin) && head_cell_bin.Contains(head_bin))
                        {
                            // 2020-12-16 Jay Tsao commented - 修正 Kit State 設定錯誤位置的問題
                            //kit.SetState(block_info.SocketX, block_info.SocketY, i, j, cell_state);


                            //if (kit_block_id >= 0)
                            //{
                            //    kit.SetState(0, kit_block_id, i, j, cell_state, false);
                            //}
                            // 2020-12-16 Jay Tsao fixed - 修正 Kit State 設定錯誤位置的問題
                            //kit.SetState(0, 0, block_info.SocketX, block_info.SocketY, cell_state, false);
                            kit.SetState(0, 0, current_kitX, current_kitY, cell_state, false);
                            head.SetState(0, 0, i, j, cell_state, false);
                        }
                    }
                }
                kit.Refresh();
                head.Refresh();
            }
            pos.X = current_kitX;
            pos.Y = current_kitY;
            return pos;
        }

        public Point CalcBoxPnPPos(TrayDataEx head, TrayDataEx box)
        {
            Point pos = new Point();
            pos.X = 0;
            byte[] target_cell_bin = GlobalDefine.EmptyBin;
            byte[] head_cell_bin = GlobalDefine.AllBin;
            
            for (int i = 0; i < head.Cols; ++i)
            {
                for (int j = 0; j < head.Rows; ++j)
                {
                    byte cell_state = 1;
                    
                    byte cell_bin = 0;

                    cell_bin = box.GetBin(0, 0, 0, 0);

                    byte head_bin = head.GetBin(0, 0, i, j, false);
                    if (target_cell_bin.Contains(cell_bin) && head_cell_bin.Contains(head_bin))
                    {
                        box.SetState(0, 0, 0, 0, cell_state, false);
                        head.SetState(0, 0, i, j, cell_state, false);
                    }
                }
            }
                box.Refresh();
                head.Refresh();
            
            pos.X = 0;
            pos.Y = 0;
            return pos;
        }

        /// <summary>
        /// 取放完後 Board Head 與 Board 資料交換
        /// </summary>
        /// <param name="head">Board Head TrayDataEx 物件</param>
        /// <param name="board">Board TrayDataEx 物件</param>
        /// <param name="socket_info">預約的取放資訊</param>
        /// <param name="state">Head 吸嘴狀態，true:有IC, false:無IC</param>
        /// <returns></returns>
        public bool DiePakDataExchange(TrayDataEx head, TrayDataEx board, BookingInfo socket_info, bool[,] state)
        {
            bool bRet = false;
            if ((head != null) && (board != null) && (socket_info != null))
            {
                if (state.GetLength(0).Equals(head.Cols) && state.GetLength(1).Equals(head.Rows))
                {
                    for (int i = 0; i < head.Cols; ++i)
                    {
                        for (int j = 0; j < head.Rows; ++j)
                        {
                            // 判斷 Head 是否有 IC
                            if (state[i, j])
                            {
                                //資料交換 (Unloading : DiePak -> Head, Loading : Head -> DiePak)
                                byte head_bin = head.GetBin(0, 0, i, j);
                                byte socket_bin = board.GetBin(0, 0, socket_info.SocketX, socket_info.SocketY);
                                head.SetBin(0, 0, i, j, socket_bin, false);
                                board.SetBin(0, 0, socket_info.SocketX, socket_info.SocketY, head_bin, false);
                            }
                            else
                            {
                                //DiePak & Head 的資料都清空
                                byte socket_bin = board.GetBin(0, 0, socket_info.SocketX, socket_info.SocketY);
                                if (GlobalDefine.AllBin.Contains(socket_bin))
                                {
                                    board.SetBin(0, 0, socket_info.SocketX, socket_info.SocketY, 0);
                                }
                                head.SetBin(0, 0, i, j, 0, false);

                            }
                            //Reset Socket & head cell state
                            head.SetState(0, 0, i, j, 0, false);
                            board.SetState(0, 0, socket_info.SocketX, socket_info.SocketY, 0, false);
                        }
                    }
                    bRet = true;
                    board.Refresh();
                    head.Refresh();
                }
            }
            return bRet;
        }

        public bool KitDataExchange(TrayDataEx head, TrayDataEx kit, bool[,] state)
        {
            bool bRet = false;
            if ((head != null) && (kit != null))
            {
                if (state.GetLength(0).Equals(head.Cols) && state.GetLength(1).Equals(head.Rows))
                {
                    for (int i = 0; i < head.Cols; ++i)
                    {
                        for (int j = 0; j < head.Rows; ++j)
                        {
                            if (state[i, j])
                            {
                                //資料交換 (Unloading : Head -> KIT, Loading : KIT -> Head)
                                byte head_bin = head.GetBin(0, 0, i, j);
                                byte socket_bin = kit.GetBin(0, 0, current_kitX, current_kitY);
                                head.SetBin(0, 0, i, j, socket_bin, false);
                                kit.SetBin(0, 0, current_kitX, current_kitY, head_bin, false);
                            }
                            else
                            {
                                //DiePak & Head 的資料都清空
                                byte socket_bin = kit.GetBin(0, 0, current_kitX, current_kitY);
                                if (GlobalDefine.PassBin.Contains(socket_bin))
                                {
                                    kit.SetBin(0, 0, current_kitX, current_kitY, (byte)BinDefine.Empty, false);
                                }
                                head.SetBin(0, 0, i, j, 0, false);
                            }
                            //Reset Socket & head cell state
                            head.SetState(0, 0, i, j, 0, false);
                            kit.SetState(0, 0, current_kitX, current_kitY, 0, false);
                        }
                    }
                    head.Refresh();
                    kit.Refresh();
                    bRet = true;
                }
            }
            return bRet;
        }

        /// <summary>
        /// 取放完後 DiePak Head 與 KIT 資料交換
        /// </summary>
        /// <param name="head">DiePak Head TrayDataEx 物件</param>
        /// <param name="kit">KIT TrayDataEx 物件</param>
        /// <param name="block_info">預約的取放資訊</param>
        /// <param name="state">Head 吸嘴狀態，true:有IC, false:無IC</param>
        /// <returns></returns>
        public bool KitDataExchange(TrayDataEx head, TrayDataEx kit, BookingInfo block_info, bool[,] state)
        {
            bool bRet = false;
            if ((head != null) && (kit != null) && (block_info != null))
            {
                if (state.GetLength(0).Equals(head.Cols) && state.GetLength(1).Equals(head.Rows))
                {
                    for (int i = 0; i < head.Cols; ++i)
                    {
                        for (int j = 0; j < head.Rows; ++j)
                        {
                            // 判斷 Head 是否有 IC
                            if (state[i, j])
                            {
                                //資料交換 (Unloading : Head -> KIT, Loading : KIT -> Head)
                                byte head_bin = head.GetBin(0, 0, i, j);
                                //byte socket_bin = kit.GetBin(0, (block_info.BlockID.Equals(KitBlockID.TOP_KIT_BLOCK) ? 0 : 1), i, j);
                                byte socket_bin = kit.GetBin(0, 0, current_kitX, current_kitY);
                                head.SetBin(0, 0, i, j, socket_bin, false);
                                kit.SetBin(0, 0, current_kitX, current_kitY, head_bin, false);
                            }
                            else
                            {
                                //DiePak & Head 的資料都清空
                                byte socket_bin = kit.GetBin(0, 0, current_kitX, current_kitY);
                                if (GlobalDefine.PassBin.Contains(socket_bin))
                                {
                                    kit.SetBin(0, 0, current_kitX, current_kitY, (byte)BinDefine.Empty, false);
                                }
                                head.SetBin(0, 0, i, j, 0, false);
                                // kit.SetBin(0, (block_info.BlockID.Equals(KitBlockID.TOP_KIT_BLOCK) ? 0 : 1), i, j, 0,false);
                            }
                            //Reset Socket & head cell state
                            head.SetState(0, 0, i, j, 0, false);
                            kit.SetState(0, 0, current_kitX, current_kitY, 0, false);
                        }
                    }
                    head.Refresh();
                    kit.Refresh();
                    bRet = true;
                }
            }
            return bRet;
        }
    }
}
