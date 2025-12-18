using ProVLib;
using System;
using System.Drawing;
using System.Linq;

namespace PaeLibProVSDKEx
{
    /// <summary>
    /// 盤角落列舉
    /// </summary>
    public enum TrayCorner
    {
        /// <summary>
        /// 左上
        /// </summary>
        UpperLeft,

        /// <summary>
        /// 左下
        /// </summary>
        LowerLeft,

        /// <summary>
        /// 右上
        /// </summary>
        UpperRight,

        /// <summary>
        /// 右下
        /// </summary>
        LowerRight,
    }

    /// <summary>
    /// ProVSDK TrayData 增強版 (必須搭配 ProVLib 的 TrayData 物件使用)
    /// </summary>
    public class TrayDataEx
    {
        #region 私有變數

        //TrayData 座標資料結構
        private struct TrayDataCoordinate
        {
            public int BlockX { get; set; }
            public int BlockY { get; set; }
            public int CellX { get; set; }
            public int CellY { get; set; }
        }

        private TrayData tdTray = null;     //ProVSDK TrayData 物件
        private Point mOffset = new Point(0, 0);    //第一個 Cell 相對於基準點的偏移量(注意！！TrayData的第一個Cell位置會隨TrayZero而改變)
        private Point mBasePos = new Point(0, 0);   //基準點座標
        private TrayCorner mBaseCoener = TrayCorner.UpperLeft;   //基準點的角落位置
        private TrayCorner mStartingCorner = TrayCorner.UpperLeft;    //取放的起始角落位置

        #endregion 私有變數

        #region 內部屬性

        /// <summary>
        /// TrayData 物件 (存取限於目前組件)
        /// </summary>
        //internal TrayData Tray { get { return tdTray; } }

        #endregion 內部屬性

        #region 公開屬性

        /// <summary>
        /// 基準點位(X,Y)值 (唯讀)
        /// </summary>
        /// <value>
        /// 基準點位(X,Y)值
        /// </value>
        public Point BasePos
        {
            get
            {
                return mBasePos;
                //建立 mBasePos 副本，避免 User 不小心改到 mBasePos 的值
                //Point pos = new Point(mBasePos.X, mBasePos.Y);
                //return pos;
            }
        }

        /// <summary>
        /// 盤取放起始角落位置(此設定值會改變 TrayData 的 TrayZero 屬性值)
        /// </summary>
        /// <value>
        /// <para>盤取放起始角落位置 TrayCorner 列舉值</para>
        /// <para>TrayCorner.UpperLeft : 左上</para>
        /// <para>TrayCorner.LowerLeft : 左下</para>
        /// <para>TrayCorner.UpperRight : 右上</para>
        /// <para>TrayCorner.LowerRight : 右下</para>
        /// </value>
        public TrayCorner PnPStartingCorner
        {
            get
            {
                return mStartingCorner;
            }
            set
            {
                mStartingCorner = value;
                switch (mStartingCorner)
                {
                    case TrayCorner.UpperLeft: { tdTray.TrayZero = TrayZeroType.Zero_LeftTop; } break;  //左上角
                    case TrayCorner.LowerLeft: { tdTray.TrayZero = TrayZeroType.Zero_LeftBottom; } break;   //左下角
                    case TrayCorner.UpperRight: { tdTray.TrayZero = TrayZeroType.Zero_RightTop; } break;    //右上角
                    case TrayCorner.LowerRight: { tdTray.TrayZero = TrayZeroType.Zero_RightBottom; } break; //右下角
                }
            }
        }

        /// <summary>
        /// Tray Block 行數 (X Number)
        /// </summary>
        /// <value>
        /// Tray Block 行數
        /// </value>
        public int BlockCols { get { return tdTray.BlockXN; } private set { tdTray.BlockXN = value; } }

        /// <summary>
        /// Tray Block 列數 (Y Number)
        /// </summary>
        /// <value>
        /// Tray Block 列數
        /// </value>
        public int BlockRows { get { return tdTray.BlockYN; } private set { tdTray.BlockYN = value; } }

        /// <summary>
        /// Tray Block 行間距 (X Pitch) (因應 Head Auto Pitch 需求而開放 set 功能)
        /// </summary>
        /// <value>
        /// Tray Block 行間距
        /// </value>
        public int BlockColPitch { get { return Convert.ToInt32(tdTray.BlockXPitch); } set { tdTray.BlockXPitch = value; } }

        /// <summary>
        /// Tray Block 列間距 (Y Pitch) (因應 Head Auto Pitch 需求而開放 set 功能)
        /// </summary>
        /// <value>
        /// Tray Block 列間距
        /// </value>
        public int BlockRowPitch { get { return Convert.ToInt32(tdTray.BlockYPitch); } set { tdTray.BlockYPitch = value; } }

        /// <summary>
        /// Tray 行數 (X Number)
        /// </summary>
        /// <value>
        /// Tray 行數
        /// </value>
        public int Cols { get { return tdTray.XN; } private set { tdTray.XN = value; } }

        /// <summary>
        /// Tray 列數 (Y Number)
        /// </summary>
        /// <value>
        /// Tray 列數
        /// </value>
        public int Rows { get { return tdTray.YN; } private set { tdTray.YN = value; } }

        /// <summary>
        /// Tray 行間距 (X Pitch) (因應 Head Auto Pitch 需求而開放 set 功能)
        /// </summary>
        /// <value>
        /// Tray 行間距
        /// </value>
        public int ColPitch { get { return Convert.ToInt32(tdTray.XPitch); } set { tdTray.XPitch = value; } }

        /// <summary>
        /// Tray 列間距 (Y Pitch) (因應 Head Auto Pitch 需求而開放 set 功能)
        /// </summary>
        /// <value>
        /// Tray 列間距
        /// </value>
        public int RowPitch { get { return Convert.ToInt32(tdTray.YPitch); } set { tdTray.YPitch = value; } }

        #endregion 公開屬性

        #region 建構子

        /// <summary>
        /// 初始化 TrayDataEx 類別的新物件，並使用指定的 TrayData 物件與 TrayCorner 列舉值作初始化。
        /// </summary>
        /// <param name="td">TrayData 物件</param>
        /// <param name="baseCorner">Tray 基準點角落位置</param>
        public TrayDataEx(TrayData td, TrayCorner baseCorner = TrayCorner.UpperLeft)
        {
            if (td != null)
            {
                td.ZeroPosX = 0;
                td.ZeroPosY = 0;
                SetTrayData(td, baseCorner);
            }
        }

        #endregion 建構子

        #region 私有函式

        /// <summary>
        /// 設定TrayData物件資料
        /// </summary>
        /// <param name="td">TrayData 物件</param>
        /// <param name="baseCorner">Tray 基準點角落位置</param>
        private void SetTrayData(TrayData td, TrayCorner baseCorner)
        {
            tdTray = td;
            mBaseCoener = baseCorner;
            this.PnPStartingCorner = baseCorner;
        }

        /// <summary>
        /// 座標轉換，在慣用座標(原點固定為左上角)與TrayData座標(原點會依TrayZero值而改變)之間做轉換
        /// </summary>
        /// <param name="bx">Block X 座標</param>
        /// <param name="by">Block Y 座標</param>
        /// <param name="cx">Cell X 座標</param>
        /// <param name="cy">Cell Y 座標</param>
        /// <returns>TrayDataCoordinate資料結構(BlockX, BlockY, CellX, CellY)</returns>
        private TrayDataCoordinate CoordinatesConverter(int bx, int by, int cx, int cy)
        {
            TrayDataCoordinate coordinate = new TrayDataCoordinate();
            switch (this.PnPStartingCorner)
            {
                //取放起始位置
                case TrayCorner.UpperLeft:
                    {
                        coordinate.BlockX = bx;
                        coordinate.BlockY = by;
                        coordinate.CellX = cx;
                        coordinate.CellY = cy;
                    }
                    break;

                case TrayCorner.LowerLeft:
                    {
                        coordinate.BlockX = bx;
                        coordinate.BlockY = (tdTray.BlockYN - by - 1);
                        coordinate.CellX = cx;
                        coordinate.CellY = (tdTray.YN - cy - 1);
                    }
                    break;

                case TrayCorner.UpperRight:
                    {
                        coordinate.BlockX = (tdTray.BlockXN - bx - 1);
                        coordinate.BlockY = by;
                        coordinate.CellX = (tdTray.XN - cx - 1);
                        coordinate.CellY = cy;
                    }
                    break;

                case TrayCorner.LowerRight:
                    {
                        coordinate.BlockX = (tdTray.BlockXN - bx - 1);
                        coordinate.BlockY = (tdTray.BlockYN - by - 1);
                        coordinate.CellX = (tdTray.XN - cx - 1);
                        coordinate.CellY = (tdTray.YN - cy - 1);
                    }
                    break;
            }
            return coordinate;
        }

        private void FindFirstState(byte state, ref Point coordinateHeadBlock, ref Point coordinateHeadCell, bool ColFirst = true)
        {
            if (ColFirst)
            {
                for (int bx = 0; bx < this.BlockCols; ++bx)
                {
                    for (int by = 0; by < this.BlockRows; ++by)
                    {
                        for (int cx = 0; cx < this.Cols; ++cx)
                        {
                            for (int cy = 0; cy < this.Rows; ++cy)
                            {
                                byte sta = this.GetState(bx, by, cx, cy, true);
                                if (sta.Equals(state))
                                {
                                    coordinateHeadBlock.X = bx;
                                    coordinateHeadBlock.Y = by;
                                    coordinateHeadCell.X = cx;
                                    coordinateHeadCell.Y = cy;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                for (int by = 0; by < this.BlockRows; ++by)
                {
                    for (int bx = 0; bx < this.BlockCols; ++bx)
                    {
                        for (int cy = 0; cy < this.Rows; ++cy)
                        {
                            for (int cx = 0; cx < this.Cols; ++cx)
                            {
                                byte sta = this.GetState(bx, by, cx, cy, true);
                                if (sta.Equals(state))
                                {
                                    coordinateHeadBlock.X = bx;
                                    coordinateHeadBlock.Y = by;
                                    coordinateHeadCell.X = cx;
                                    coordinateHeadCell.Y = cy;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion 私有函式

        #region 內部函式

        /// <summary>
        /// 設定 TrayData 的 In TrayData 物件
        /// </summary>
        /// <param name="tdxTarget"></param>
        internal void SetTarget(TrayDataEx tdxTarget)
        {
            this.tdTray.In = tdxTarget.tdTray;
        }

        /// <summary>
        /// 計算取放點位
        /// </summary>
        /// <param name="head_bin">PnP Head Bin</param>
        /// <param name="tray_bin">Target Tray Bin</param>
        /// <param name="pos">取放點位</param>
        /// <param name="coordinateBlock">取放 Block 座標</param>
        /// <param name="coordinateCell">取放 Cell 座標</param>
        /// <param name="ColFirst">是否 Y 優先</param>
        /// <returns></returns>
        internal bool CalcPos(byte[] head_bin, byte[] tray_bin, ref Point pos, ref Point coordinateTrayBlock,
            ref Point coordinateTrayCell, ref Point coordinateHeadBlock, ref Point coordinateHeadCell, bool ColFirst = true)
        {
            PPCellType type = new PPCellType();
            type.IsYFirst = ColFirst;   //設定是否行(Y方向)優先放滿，false則列(X方向)優先放滿
            type.bChangeBin = true;

            //2020-10-23 Jay Tsao Modified : PosType 固定為 LeftTop (避免點位計算錯誤)
            type.PosType = TrayPosType.Pos_LeftTop;
            //switch (this.PnPStartingCorner)
            //{
            //    case TrayCorner.LowerLeft: { type.PosType = TrayPosType.Pos_LeftBottom; } break;
            //    case TrayCorner.UpperLeft: { type.PosType = TrayPosType.Pos_LeftTop; } break;
            //    case TrayCorner.LowerRight: { type.PosType = TrayPosType.Pos_RightBottom; } break;
            //    case TrayCorner.UpperRight: { type.PosType = TrayPosType.Pos_RightTop; } break;
            //}

            type.SortType = SORTTYPE.SORT_SEQUENCE;
            type.bPPLimit = false;
            type.ActionBin = string.Join(",", head_bin);    //Head 取料前的 Bin
            type.FinishedBin = "";
            type.GetBin = string.Join(",", tray_bin);   //來源盤要取料 Bin

            //Point posB = new Point();       //盤區塊索引座標
            //Point posT = new Point();       //盤位置索引座標
            TRAYRESULT result = this.tdTray.GetCell(type, ref pos, ref coordinateTrayBlock, ref coordinateTrayCell);
            FindFirstState(1, ref coordinateHeadBlock, ref coordinateHeadCell, ColFirst);
            if (result == TRAYRESULT.RESULT_ACTION)
            {
                return true;
            }
            return false;
        }

        internal void DataExchange()
        {
            //盤資料交換
            this.tdTray.In.CellStateChangeToTarget(1);
            //吸嘴資料交換
            this.tdTray.CellStateChangeToTarget(1);
        }

        #endregion 內部函式

        #region 公開函式
        /// <summary>
        /// 設定 Basic Pos 基本資訊
        /// </summary>
        /// <param name="xbp">X Base Position</param>
        /// <param name="ybp">Y Base Position</param>
        public void SetBasicPos(int xbp, int ybp)
        {
            mBasePos.X = xbp;
            mBasePos.Y = ybp;
        }
        /// <summary>
        /// 設定 TrayData 基本資訊
        /// </summary>
        /// <param name="bxn">Block X Number</param>
        /// <param name="byn">Block Y Number</param>
        /// <param name="bxp">Block X Pitch</param>
        /// <param name="byp">Block Y Pitch</param>
        /// <param name="xn">Unit X Number per Block</param>
        /// <param name="yn">Unit Y Number per Block</param>
        /// <param name="xp">Unit X Pitch</param>
        /// <param name="yp">Unit Y Pitch</param>
        /// <param name="xo">X Offset (The Center of First Col to the Edge)</param>
        /// <param name="yo">Y Offset (The Center of First Row to the Edge)</param>
        /// <param name="xbp">X Base Position</param>
        /// <param name="ybp">Y Base Position</param>
        /// <param name="bc">基準點的角落位置</param>
        /// <param name="sc">取放的起始角落位置</param>
        public void SetInfo(int bxn, int byn, int bxp, int byp, int xn, int yn, int xp, int yp,
            int xo, int yo, int xbp, int ybp, TrayCorner bc = TrayCorner.UpperLeft, TrayCorner sc = TrayCorner.UpperLeft)
        {
            tdTray.BlockXN = bxn;
            tdTray.BlockYN = byn;
            tdTray.BlockXPitch = bxp;
            tdTray.BlockYPitch = byp;
            tdTray.XN = xn;
            tdTray.YN = yn;
            tdTray.XPitch = xp;
            tdTray.YPitch = yp;
            tdTray.ZeroPosX = 0;
            tdTray.ZeroPosY = 0;

            mOffset.X = xo;
            mOffset.Y = yo;
            mBasePos.X = xbp;
            mBasePos.Y = ybp;
            mBaseCoener = bc;
            this.PnPStartingCorner = sc;
        }

        /// <summary>
        /// 取得基準點與取放起始點的(X,Y)偏移量，具有方向性，右正左負，上正下負。
        /// <para>此處計算的偏移量值，雖具有方向性，但是以 Head 具有 X/Y 軸的角度來作計算</para>
        /// </summary>
        /// <returns>
        /// 基準點與取放起始點的(X,Y)偏移量
        /// </returns>
        public Point GetTrayOffset()
        {
            Point retPos = new Point(0, 0);

            int txo = mOffset.X;
            int tyo = mOffset.Y;
            int txw = (int)(((tdTray.XN*tdTray.BlockXN) - 1) * tdTray.XPitch);
            int tyh = (int)(((tdTray.YN*tdTray.BlockYN) - 1) * tdTray.YPitch);

            switch (mBaseCoener)
            {
                //基準點位置
                case TrayCorner.UpperLeft:
                    {
                        switch (this.PnPStartingCorner)
                        {
                            //取放起始位置
                            case TrayCorner.UpperLeft: { retPos.X = txo; retPos.Y = (-tyo); } break;
                            case TrayCorner.LowerLeft: { retPos.X = txo; retPos.Y = (-tyo - tyh); } break;
                            case TrayCorner.UpperRight: { retPos.X = (txo + txw); retPos.Y = (-tyo); } break;
                            case TrayCorner.LowerRight: { retPos.X = (txo + txw); retPos.Y = (-tyo - tyh); } break;
                        }
                    }
                    break;

                case TrayCorner.LowerLeft:
                    {
                        switch (this.PnPStartingCorner)
                        {
                            //取放起始位置
                            case TrayCorner.UpperLeft: { retPos.X = (txo); retPos.Y = (tyo + tyh); } break;
                            case TrayCorner.LowerLeft: { retPos.X = (txo); retPos.Y = (tyo); } break;
                            case TrayCorner.UpperRight: { retPos.X = (txo + txw); retPos.Y = (tyo + tyh); } break;
                            case TrayCorner.LowerRight: { retPos.X = (txo + txw); retPos.Y = (tyo); } break;
                        }
                    }
                    break;

                case TrayCorner.UpperRight:
                    {
                        switch (this.PnPStartingCorner)
                        {
                            //取放起始位置
                            case TrayCorner.UpperLeft: { retPos.X = (-txo - txw); retPos.Y = (-tyo); } break;
                            case TrayCorner.LowerLeft: { retPos.X = (-txo - txw); retPos.Y = (-tyo - tyh); } break;
                            case TrayCorner.UpperRight: { retPos.X = (-txo); retPos.Y = (-tyo); } break;
                            case TrayCorner.LowerRight: { retPos.X = (-txo); retPos.Y = (-tyo - tyh); } break;
                        }
                    }
                    break;

                case TrayCorner.LowerRight:
                    {
                        switch (this.PnPStartingCorner)
                        {
                            //取放起始位置
                            case TrayCorner.UpperLeft: { retPos.X = (-txo - txw); retPos.Y = (tyo + tyh); } break;
                            case TrayCorner.LowerLeft: { retPos.X = (-txo - txw); retPos.Y = (tyo); } break;
                            case TrayCorner.UpperRight: { retPos.X = (-txo); retPos.Y = (tyo + tyh); } break;
                            case TrayCorner.LowerRight: { retPos.X = (-txo); retPos.Y = (tyo); } break;
                        }
                    }
                    break;
            }
            return retPos;
        }

        /// <summary>
        /// 要求立即更新 TrayDataView 畫面資訊
        /// <para>當 SetBin() 或 SetState() 的 refresh 參數設定為 false 時(預設為 true)，須由 User 呼叫此函式進行畫面更新</para>
        /// </summary>
        public void Refresh()
        {
            this.tdTray.OnResize();
        }

        /// <summary>
        /// 設定指定行列位置的 bin 值
        /// </summary>
        /// <param name="bx">Block 行座標(BX)</param>
        /// <param name="by">Block 列座標(BY)</param>
        /// <param name="cx">Cell 行座標(CX)</param>
        /// <param name="cy">Cell 列座標(CY)</param>
        /// <param name="bin">欲設定的 bin 值</param>
        /// <param name="original">
        /// <para>true : 使用 TrayData 座標(原點會依TrayZero值而改變)</para>
        /// <para>false : 使用慣用座標(原點固定為左上角)</para>
        /// </param>
        public void SetBin(int bx, int by, int cx, int cy, byte bin, bool refresh = true, bool original = false)
        {
            byte state = GetState(bx, by, cx, cy);
            if (original)
            {
                //TrayData 的原始 BinMap 字串(原點會依TrayZero值而改變)
                tdTray.CellSet(bx, by, cx, cy, bin, state, refresh);
            }
            else
            {
                //經過座標轉換的 BinMap 字串(原點固定在左上角)
                TrayDataCoordinate tdc = CoordinatesConverter(bx, by, cx, cy);
                tdTray.CellSet(tdc.BlockX, tdc.BlockY, tdc.CellX, tdc.CellY, bin, state, refresh);
            }
        }

        /// <summary>
        /// 設定指定行列位置的 state 值
        /// <para>2020-12-14 Jay Tsao Added.</para>
        /// </summary>
        /// <param name="bx">Block 行座標(BX)</param>
        /// <param name="by">Block 列座標(BY)</param>
        /// <param name="cx">Cell 行座標(CX)</param>
        /// <param name="cy">Cell 列座標(CY)</param>
        /// <param name="state">欲設定的 state 值</param>
        /// <param name="original">
        /// <para>true : 使用 TrayData 座標(原點會依TrayZero值而改變)</para>
        /// <para>false : 使用慣用座標(原點固定為左上角)</para>
        /// </param>
        public void SetState(int bx, int by, int cx, int cy, byte state, bool refresh = true, bool original = false)
        {
            byte bin = GetBin(bx, by, cx, cy);
            if (original)
            {
                //TrayData 的原始 BinMap 字串(原點會依TrayZero值而改變)
                tdTray.CellSet(bx, by, cx, cy, bin, state, refresh);
            }
            else
            {
                //經過座標轉換的 BinMap 字串(原點固定在左上角)
                TrayDataCoordinate tdc = CoordinatesConverter(bx, by, cx, cy);
                tdTray.CellSet(tdc.BlockX, tdc.BlockY, tdc.CellX, tdc.CellY, bin, state, refresh);
            }
        }

        /// <summary>
        /// 重新設定 Bin Map
        /// </summary>
        /// <param name="bins">Bin Array</param>
        /// <param name="type">設定方式，0:隨機, 1:依序</param>
        /// <param name="original">
        /// <para>true : 使用 TrayData 座標(原點會依TrayZero值而改變)</para>
        /// <para>false : 使用慣用座標(原點固定為左上角)</para>
        /// </param>
        public void ResetBin(byte[] bins, int type = 0, bool original = false)
        {

            for (int by = 0; by < tdTray.BlockYN; ++by)
            {
                for (int bx = 0; bx < tdTray.BlockXN; ++bx)
                {
                    for (int cy = 0; cy < tdTray.YN; ++cy)
                    {
                        for (int cx = 0; cx < tdTray.XN; ++cx)
                        {
                            int cell_count = 0;
                            switch (type)
                            {
                                case 0: //隨機
                                    {
                                        Random rand = new Random(Guid.NewGuid().GetHashCode());
                                        cell_count = rand.Next();
                                    }
                                    break;

                                case 1: //依序
                                    {
                                        cell_count = (((tdTray.BlockXN * by) + bx) * (tdTray.XN * tdTray.YN)) + ((tdTray.XN * cy) + cx);
                                    }
                                    break;
                            }
                            int idx = (cell_count % bins.Length);
                            SetBin(bx, by, cx, cy, bins[idx], false, original);
                        }
                    }
                }
            }
            tdTray.OnResize();
        }
        /// Reset State
        public void ResetState()
        {

            for (int by = 0; by < tdTray.BlockYN; ++by)
            {
                for (int bx = 0; bx < tdTray.BlockXN; ++bx)
                {
                    for (int cy = 0; cy < tdTray.YN; ++cy)
                    {
                        for (int cx = 0; cx < tdTray.XN; ++cx)
                        {

                            SetState(bx, by, cx, cy, (byte)0, false);

                        }
                    }
                }
            }
            tdTray.OnResize();
        }



        /// <summary>
        /// 設定 Bin Map，依指定的 Map 字串內容做設定，Map 格式為 ProV 標準格式(1,2,3,4,5,6;7,8,9,0,1,2;...)
        /// </summary>
        /// <param name="map">指定的 Map 字串內容，Map 格式為 ProV 標準格式(1,2,3,4,5,6;7,8,9,0,1,2;...)</param>
        /// <returns>
        /// <c>true</c> 設定成功，<c>false</c> 設定失敗，可能是 map 格式錯誤或資料長度不符。
        /// </returns>
        public bool SetBinMap(string map)
        {
            bool bRet = false;
            //解析 map 內容
            string[] sMapArray = map.Split(new char[] { ',', ';' });    //使用','(逗號)與';'(分號)來分割字串
            sMapArray = sMapArray.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();  //去除空字串元素
            //檢查字串元素數量與 cell 數是否相同
            if (sMapArray.Length.Equals(this.BlockCols * this.BlockRows * this.Cols * this.Rows))
            {
                //設定 map
                try
                {
                    byte[] bMapArray = sMapArray.Select(byte.Parse).ToArray();
                    ResetBin(bMapArray, 1);
                    bRet = true;
                }
                catch (Exception e)
                {
                    //...
                }
            }
            return bRet;
        }

        /// <summary>
        /// 取得指定行列位置的 bin 值
        /// </summary>
        /// <param name="bx">Block 行座標(BX)</param>
        /// <param name="by">Block 列座標(BY)</param>
        /// <param name="cx">Cell 行座標(CX)</param>
        /// <param name="cy">Cell 列座標(CY)</param>
        /// <param name="original">
        /// <para>true : 使用 TrayData 座標(原點會依TrayZero值而改變)</para>
        /// <para>false : 使用慣用座標(原點固定為左上角)</para>
        /// </param>
        /// <returns>指定行列位置的 bin 值</returns>
        public byte GetBin(int bx, int by, int cx, int cy, bool original = false)
        {
            byte bin = 0;
            byte state = 0;
            if (original)
            {
                //TrayData 的原始 BinMap 字串(原點會依TrayZero值而改變)
                tdTray.CellGet(bx, by, cx, cy, ref bin, ref state);
            }
            else
            {
                //經過座標轉換的 BinMap 字串(原點固定在左上角)
                TrayDataCoordinate tdc = CoordinatesConverter(bx, by, cx, cy);
                tdTray.CellGet(tdc.BlockX, tdc.BlockY, tdc.CellX, tdc.CellY, ref bin, ref state);
            }
            return bin;
        }

        /// <summary>
        /// 取得指定行列位置的 state 值
        /// </summary>
        /// <param name="bx">Block 行座標(BX)</param>
        /// <param name="by">Block 列座標(BY)</param>
        /// <param name="cx">Cell 行座標(CX)</param>
        /// <param name="cy">Cell 列座標(CY)</param>
        /// <param name="original">
        /// <para>true : 使用 TrayData 座標(原點會依TrayZero值而改變)</para>
        /// <para>false : 使用慣用座標(原點固定為左上角)</para>
        /// </param>
        /// <returns>指定行列位置的 state 值</returns>
        public byte GetState(int bx, int by, int cx, int cy, bool original = false)
        {
            byte bin = 0;
            byte state = 0;
            if (original)
            {
                //TrayData 的原始 BinMap 字串(原點會依TrayZero值而改變)
                tdTray.CellGet(bx, by, cx, cy, ref bin, ref state);
            }
            else
            {
                //經過座標轉換的 BinMap 字串(原點固定在左上角)
                TrayDataCoordinate tdc = CoordinatesConverter(bx, by, cx, cy);
                tdTray.CellGet(tdc.BlockX, tdc.BlockY, tdc.CellX, tdc.CellY, ref bin, ref state);
            }
            return state;
        }

        /// <summary>
        /// 取得 Bin Map 字串
        /// </summary>
        /// <param name="original">
        /// <para>true : 使用 TrayData 座標(原點會依TrayZero值而改變)</para>
        /// <para>false : 使用慣用座標(原點固定為左上角)</para>
        /// </param>
        /// <returns>Bin Map String</returns>
        public string GetBinMap(bool original = false)
        {
            string sMap = string.Empty;
            for (int by = 0; by < tdTray.BlockYN; ++by)
            {
                for (int bx = 0; bx < tdTray.BlockXN; ++bx)
                {
                    string sBlockMap = string.Empty;
                    for (int cy = 0; cy < tdTray.YN; ++cy)
                    {
                        for (int cx = 0; cx < tdTray.XN; ++cx)
                        {
                            byte bin = GetBin(bx, by, cx, cy, original);
                            if (string.IsNullOrEmpty(sBlockMap))
                            {
                                sBlockMap = string.Format("{0}", bin);
                            }
                            else
                            {
                                sBlockMap = string.Format("{0},{1}", sBlockMap, bin);
                            }
                        }
                    }
                    sBlockMap += ";";
                    sMap += sBlockMap;
                }
            }
            return sMap;
        }

        /// <summary>
        /// 是否全部的 Bin 都包含在指定的 Bin Array 中
        /// </summary>
        /// <param name="bins">指定的 Bin Array</param>
        /// <returns><c>true</c> 全部的 Bin 都包含在指定的 Bin Array 中，<c>false</c> 有部分的 Bin 不包含在 Bin Array 中。</returns>
        public bool IsFull(byte[] bins)
        {
            for (int x = 0; x < tdTray.BlockXN; x++)
            {
                for (int y = 0; y < tdTray.BlockYN; y++)
                {
                    for (int c = 0; c < tdTray.XN; c++)
                    {
                        for (int r = 0; r < tdTray.YN; r++)
                        {
                            byte currentBin = 0;
                            byte currentState = 0;
                            if (tdTray.CellGet(x, y, c, r, ref currentBin, ref currentState))
                            {
                                if (bins.Contains(currentBin) == false)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 是否全部的 Bin 都不包含在指定的 Bin Array 中
        /// </summary>
        /// <param name="bins">指定的 Bin Array</param>
        /// <returns><c>true</c> 全部的 Bin 都不包含在指定的 Bin Array 中，<c>false</c> 有部分的 Bin 包含在 Bin Array 中。</returns>
        public bool IsEmpty(byte[] bins)
        {
            for (int x = 0; x < tdTray.BlockXN; x++)
            {
                for (int y = 0; y < tdTray.BlockYN; y++)
                {
                    for (int c = 0; c < tdTray.XN; c++)
                    {
                        for (int r = 0; r < tdTray.YN; r++)
                        {
                            byte currentBin = 0;
                            byte currentState = 0;
                            if (tdTray.CellGet(x, y, c, r, ref currentBin, ref currentState))
                            {
                                if (bins.Contains(currentBin))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 是否有包含指定的 Bin Array 中的任一個 Bin
        /// </summary>
        /// <param name="bins">指定的 Bin Array</param>
        /// <returns><c>true</c> 有包含指定的 Bin Array 中的某一個 Bin，<c>false</c> 完全不包含指定的 Bin Array 中的任一個 Bin。</returns>
        public bool IsContain(byte[] bins)
        {
            for (int x = 0; x < tdTray.BlockXN; x++)
            {
                for (int y = 0; y < tdTray.BlockYN; y++)
                {
                    for (int c = 0; c < tdTray.XN; c++)
                    {
                        for (int r = 0; r < tdTray.YN; r++)
                        {
                            byte currentBin = 0;
                            byte currentState = 0;
                            if (tdTray.CellGet(x, y, c, r, ref currentBin, ref currentState))
                            {
                                if (bins.Contains(currentBin))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 取得包含在指定的 Bin Array 中 Bin 的數量
        /// </summary>
        /// <param name="bins">指定的 Bin Array</param>
        /// <returns>包含在指定的 Bin Array 中 Bin 的數量</returns>
        public int BinCount(byte[] bins)
        {
            int count = 0;
            for (int x = 0; x < tdTray.BlockXN; x++)
            {
                for (int y = 0; y < tdTray.BlockYN; y++)
                {
                    for (int c = 0; c < tdTray.XN; c++)
                    {
                        for (int r = 0; r < tdTray.YN; r++)
                        {
                            byte currentBin = 0;
                            byte currentState = 0;
                            if (tdTray.CellGet(x, y, c, r, ref currentBin, ref currentState))
                            {
                                if (bins.Contains(currentBin))
                                {
                                    count++;
                                }
                            }
                        }
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// 替換 Bin 資料，將 oldBin 以 newBin 取代
        /// </summary>
        /// <param name="oldBin">原始 Bin 資料(被取代)</param>
        /// <param name="newBin">新的 Bin 資料(用來取代)</param>
        public void BinReplace(byte oldBin, byte newBin)
        {
            tdTray.CellBinChange(oldBin, newBin);
        }

        /// <summary>
        /// Clear Tray Bin
        /// </summary>
        /// <param name="oldBin">原始 Bin 資料(被取代)</param>
        /// <param name="newBin">新的 Bin 資料(用來取代)</param>
        public void Clear(byte Bin)
        {
            tdTray.CellClear(Bin);
        }

        #endregion 公開函式
    }
}