using ProVLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaeLibProVSDKEx
{
    /// <summary>
    /// TrayData Bin 值共用定義列舉
    /// <para>0~39 : for Device</para>
    /// <para>40~49 : for Socket</para>
    /// <para>50~59 : For Nozzle</para>
    /// </summary>
    public enum BinDefine
    {
        /// <summary>
        /// 空的 '0'
        /// </summary>
        Empty = 0,

        /// <summary>
        /// Untest, Pass, Good Bin1
        /// </summary>
        Bin1 = 1,

        /// <summary>
        /// Fail Bin2
        /// </summary>
        Bin2 = 2,

        /// <summary>
        /// Fail Bin3
        /// </summary>
        Bin3 = 3,

        /// <summary>
        /// Fail Bin4
        /// </summary>
        Bin4 = 4,

        /// <summary>
        /// Fail Bin5
        /// </summary>
        Bin5 = 5,

        /// <summary>
        /// Fail Bin6
        /// </summary>
        Bin6 = 6,

        /// <summary>
        /// Fail Bin7
        /// </summary>
        Bin7 = 7,

        /// <summary>
        /// Fail Bin8
        /// </summary>
        Bin8 = 8,

        /// <summary>
        /// Fail Bin9
        /// </summary>
        Bin9 = 9,

        /// <summary>
        /// Fail Bin10
        /// </summary>
        Bin10 = 10,

        /// <summary>
        /// Fail Bin11
        /// </summary>
        Bin11 = 11,

        /// <summary>
        /// Fail Bin12
        /// </summary>
        Bin12 = 12,

        /// <summary>
        /// Fail Bin13
        /// </summary>
        Bin13 = 13,

        /// <summary>
        /// Fail Bin14
        /// </summary>
        Bin14 = 14,

        /// <summary>
        /// Fail Bin15
        /// </summary>
        Bin15 = 15,

        /// <summary>
        /// Fail Bin16
        /// </summary>
        Bin16 = 16,
        Bin17 = 17,
        Bin18 = 18,
        Bin19 = 19,
        Bin20 = 20,
        Bin21 = 21,
        Bin22 = 22,
        Bin23 = 23,
        Bin24 = 24,
        Bin25 = 25,
        Bin26 = 26,
        Bin27 = 27,
        Bin28 = 28,
        Bin29 = 29,
        Bin30 = 30,
        Bin31 = 31,
        Bin32 = 32,

        /// <summary>
        /// 'M' Masked Socket
        /// </summary>
        MaskedSocket = 40,

        /// <summary>
        /// 'X' Bad Socket
        /// </summary>
        BadSocket = 41,

        /// <summary>
        /// 'R' Repair Socket
        /// </summary>
        RepairSocket = 42,

        /// <summary>
        /// '-' 停用
        /// </summary>
        Disabled = 43,

        /// <summary>
        /// 取放中
        /// </summary>
        PnPing = 50,

        /// <summary>
        /// 吸取失敗
        /// </summary>
        PnPFail = 51,

        /// <summary>
        /// 移動掉落
        /// </summary>
        PnPLost = 52,

        /// <summary>
        /// 吸嘴暫停使用
        /// </summary>
        PnPAvoid = 53,

        /// <summary>
        /// 吸嘴禁止使用
        /// </summary>
        PnPDisabled = 54,

        //Record用
        Untested = 99,
        //Record用
        Tested=100,
        //整盤Bin別,
        //Bin1S=101,
        //Bin2S=102,
        //Bin3S =103,
        //Bin4S = 104,
        //Bin5S = 105,
        //Bin6S = 106,
        //Bin7S = 107,
        //Bin8S = 108,
        //Bin9S = 109,
        //Bin10S = 110,
        //Bin11S = 111,
        //Bin12S = 112,
        //Bin13S = 113,
        //Bin14S = 114,
        //Bin15S = 115,
        //Bin16S = 116,
        //SortEmpty=150,
        Bin101 = 101,
        Bin102 = 102,
        Bin103 = 103,
        Bin104 = 104,
        Bin105 = 105,
        Bin106 = 106,
        Bin107 = 107,
        Bin108 = 108,
        Bin109 = 109,
        Bin110 = 110,
        Bin111 = 111,
        Bin112 = 112,
        Bin113 = 113,
        Bin114 = 114,
        Bin115 = 115,
        Bin116 = 116,
        Bin117 = 117,
        Bin118 = 118,
        Bin119 = 119,
        Bin120 = 120,
        Bin121 = 121,
        Bin122 = 122,
        Bin123 = 123,
        Bin124 = 124,
        Bin125 = 125,
        Bin126 = 126,
        Bin127 = 127,
        Bin128 = 128,
        Bin129 = 129,
        Bin130 = 130,
        Bin131 = 131,
        Bin132 = 132,
        Bin133 = 133,
        Bin134 = 134,
        Bin135 = 135,
        Bin136 = 136,
        Bin137 = 137,
        Bin138 = 138,
        Bin139 = 139,
        Bin140 = 140,
        Bin141 = 141,
        Bin142 = 142,
        Bin143 = 143,
        Bin144 = 144,
        Bin145 = 145,
        Bin146 = 146,
        Bin147 = 147,
        Bin148 = 148,
        Bin149 = 149,
        Bin150 = 150,
        Bin151 = 151,
        Bin152 = 152,
        Bin153 = 153,
        Bin154 = 154,
        Bin155 = 155,
        Bin156 = 156,
        Bin157 = 157,
        Bin158 = 158,
        Bin159 = 159,
        Bin160 = 160,
        Bin161 = 161,
        Bin162 = 162,
        Bin163 = 163,
        Bin164 = 164,
        Bin165 = 165,
        Bin166 = 166,
        Bin167 = 167,
        Bin168 = 168,
        Bin169 = 169,
        Bin170 = 170,
        Bin171 = 171,
        Bin172 = 172,
        Bin173 = 173,
        Bin174 = 174,
        Bin175 = 175,
        Bin176 = 176,
        Bin177 = 177,
        Bin178 = 178,
        Bin179 = 179,
        Bin180 = 180,
        Bin181 = 181,
        Bin182 = 182,
        Bin183 = 183,
        Bin184 = 184,
        Bin185 = 185,
        Bin186 = 186,
        Bin187 = 187,
        Bin188 = 188,
        Bin189 = 189,
        Bin190 = 190,
        Bin191 = 191,
        Bin192 = 192,
        Bin193 = 193,
        Bin194 = 194,
        Bin195 = 195,
        Bin196 = 196,
        Bin197 = 197,
        Bin198 = 198,
        Bin199 = 199,


        /// <summary>
        /// 未定義 (255)
        /// </summary>
        None = 255
    }

    /// <summary>
    /// <c>取放頭點位計算</c>類別
    /// <para>主要應用於 MainFlow 中計算取放點位後，將吸嘴狀態與點位傳給 Module 執行 PnP 流程</para>
    /// </summary>
    public class PnPCalculator
    {
        #region 私有欄位

        /// <summary>
        /// 取放手臂吸嘴(X,Y)跳支數
        /// </summary>
        private Point NozzleBypass = new Point(0, 0);

        /// <summary>
        /// 吸嘴是否有 X 軸
        /// <para>2020-12-11 v1.5 Jay Tsao Added</para>
        /// </summary>
        private bool mIsHeadHasX = true;

        /// <summary>
        /// 吸嘴是否有 Y 軸
        /// <para>2020-12-11 v1.5 Jay Tsao Added</para>
        /// </summary>
        private bool mIsHeadHasY = true;

        /// <summary>
        /// 取放手臂中心與基準位置的(X,Y) Offset (若吸嘴的基準在中心則為 0)
        /// </summary>
        private Point HeadCenterOffset = new Point(0, 0);

        /// <summary>
        /// TrayData 計算出來的取料(X,Y)點位(相對於基準點位置)
        /// </summary>
        private Point mRelativePos = new Point(0, 0);

        /// <summary>
        /// TrayData 計算出來的取料(X,Y)點位 Fixed (相對於基準點位置)
        /// <para>ProVSDK v1.1.2.0版已修正 TrayData 點位計算錯誤問題，所以此屬性已無需要</para>
        /// <para>注意！！ 在 TrayData 物件使用 GetCell() 函數時，參數 PPCellType PPType 的 PosType 設定，務必設定為 TrayPosType.Pos_LeftTop，不可隨 TrayZero 方向而改變</para>
        /// </summary>
        private Point mRelativePosFixed = new Point(0, 0);

        /// <summary>
        /// 實際取放手臂的取料點位(直接移動至此位置進行取料)
        /// </summary>
        private Point mAbsolutePos = new Point(0, 0);

        /// <summary>
        /// TrayData 計算出來的取料 Head Block 座標
        /// </summary>
        private Point mHeadBlockCoordinate = new Point(0, 0);

        /// <summary>
        /// TrayData 計算出來的取料 Head Cell 座標
        /// </summary>
        private Point mHeadCellCoordinate = new Point(0, 0);

        /// <summary>
        /// TrayData 計算出來的取料 Tray Block 座標
        /// </summary>
        private Point mTrayBlockCoordinate = new Point(0, 0);

        /// <summary>
        /// TrayData 計算出來的取料 Tray Cell 座標
        /// </summary>
        private Point mTrayCellCoordinate = new Point(0, 0);

        #endregion 私有欄位

        #region 公有屬性

        /// <summary>
        /// 吸嘴 TrayDataEx 物件
        /// </summary>
        /// <value>
        /// TrayDataEx 物件
        /// </value>
        public TrayDataEx Nozzles { get; set; }

        /// <summary>
        /// 取放手臂吸嘴最大 Pitch (因應測試程式需求而使用公開欄位來開放設定)
        /// </summary>
        /// <value>
        /// 吸嘴最大 Pitch (X,Y)
        /// </value>
        public Point HeadPitchMax = new Point(0, 0);

        /// <summary>
        /// 取放手臂吸嘴最小 Pitch (因應測試程式需求而使用公開欄位來開放設定)
        /// </summary>
        /// <value>
        /// 吸嘴最小 Pitch (X,Y)
        /// </value>
        public Point HeadPitchMin = new Point(0, 0);

        /// <summary>
        /// TrayData 計算出來的取放點位(相對於基準點位置) (ReadOnly)
        /// </summary>
        /// <value>
        /// 相對於基準點位置的取放點位 (為系統內私有欄位值的副本，外部無法直接修改其 X, Y 值，因為 Point 是結構，是值型別)
        /// </value>
        public Point RelativePos { get { return mRelativePos; } }

        /// <summary>
        /// 修正後的 TrayData 計算出來的取放點位 (相對於基準點位置) (ReadOnly)
        /// <para>TrayData點位計算錯誤問題已於v1.1.2.0版修正，所以ProVSDK v1.1.2.0版之後不再需要使用此屬性</para>
        /// </summary>
        /// <value>
        /// 修正後的相對於基準點位置的取放點位 (為系統內私有欄位值的副本，外部無法直接修改其 X, Y 值，因為 Point 是結構，是值型別)
        /// </value>
        public Point RelativePosFixed { get { return mRelativePosFixed; } }

        /// <summary>
        /// 手臂真正的取放點位(有加入基準點與偏移量，直接移動至此位置進行取放) (ReadOnly)
        /// </summary>
        /// <value>
        /// 真正的取放點位
        /// </value>
        public Point AbsolutePos { get { return mAbsolutePos; } }

        /// <summary>
        /// TrayData 計算出來的取放 Head Block 行列座標 (ReadOnly)
        /// </summary>
        /// <value>
        /// Head Block 行列座標
        /// </value>
        public Point HeadBlockCoordinate { get { return mHeadBlockCoordinate; } }

        /// <summary>
        /// TrayData 計算出來的取放 Head Cell 行列座標 (ReadOnly)
        /// </summary>
        /// <value>
        /// Head Cell 行列座標
        /// </value>
        public Point HeadCellCoordinate { get { return mHeadCellCoordinate; } }

        /// <summary>
        /// TrayData 計算出來的取放 Tray Block 行列座標 (ReadOnly)
        /// </summary>
        /// <value>
        /// Tray Block 行列座標
        /// </value>
        public Point TrayBlockCoordinate { get { return mTrayBlockCoordinate; } }

        /// <summary>
        /// TrayData 計算出來的取放 Tray Cell 行列座標 (ReadOnly)
        /// </summary>
        /// <value>
        /// Tray Cell 行列座標
        /// </value>
        public Point TrayCellCoordinate { get { return mTrayCellCoordinate; } }

        #endregion 公有屬性

        #region 建構子

        /// <summary>
        /// 初始化 PnPCalculator 類別的新物件，並使用指定的 TrayDataEx 物件與 Head X/Y Max./Min. Pitch 值作初始化。
        /// <para>初始化吸嘴、來源端與目的端物件</para>
        /// <para>強制必須使用 TrayDataEx 進行初始化</para>
        /// </summary>
        /// <param name="nozzles">吸嘴 TrayDataEx 物件</param>
        /// <param name="max_head_xp">取放手臂吸嘴最大 X Pitch</param>
        /// <param name="min_head_xp">取放手臂吸嘴最小 X Pitch</param>
        /// <param name="max_head_yp">取放手臂吸嘴最大 Y Pitch</param>
        /// <param name="min_head_yp">取放手臂吸嘴最小 Y Pitch</param>
        /// <param name="head_has_x">吸嘴是否有 X 軸</param>
        /// <param name="head_has_y">吸嘴是否有 Y 軸</param>
        public PnPCalculator(TrayDataEx nozzles, int max_head_xp = int.MaxValue, int min_head_xp = int.MinValue,
            int max_head_yp = int.MaxValue, int min_head_yp = int.MinValue, bool head_has_x = true, bool head_has_y = true)
        {
            this.SetNozzles(nozzles, max_head_xp, min_head_xp, max_head_yp, min_head_yp, head_has_x, head_has_y);
        }

        /// <summary>
        /// 初始化 PnPCalculator 類別的新物件，並使用空的 TrayDataEx 物件與整數的最大最小值作初始化。
        /// </summary>
        public PnPCalculator()
            : this(null)
        {
        }

        #endregion 建構子

        #region 私有函式

        /// <summary>
        /// 設定取放手臂的 TrayDataEx 物件
        /// </summary>
        /// <param name="nozzles">取放手臂的 TrayDataEx 物件</param>
        /// <param name="max_head_xp">取放手臂吸嘴最大 X Pitch</param>
        /// <param name="min_head_xp">取放手臂吸嘴最小 X Pitch</param>
        /// <param name="max_head_yp">取放手臂吸嘴最大 Y Pitch</param>
        /// <param name="min_head_yp">取放手臂吸嘴最小 Y Pitch</param>
        /// <param name="head_has_x">吸嘴是否有 X 軸</param>
        /// <param name="head_has_y">吸嘴是否有 Y 軸</param>
        /// <returns></returns>
        private bool SetNozzles(TrayDataEx nozzles, int max_head_xp, int min_head_xp,
            int max_head_yp, int min_head_yp, bool head_has_x, bool head_has_y)
        {
            if (nozzles != null)
            {
                this.Nozzles = nozzles;

                if (nozzles.Cols > 1)
                {
                    this.HeadPitchMax.X = Math.Max(max_head_xp, min_head_xp);
                    this.HeadPitchMin.X = Math.Min(max_head_xp, min_head_xp);
                }
                else
                {
                    this.HeadPitchMax.X = int.MaxValue;
                    this.HeadPitchMin.X = int.MinValue;
                }
                if (nozzles.Rows > 1)
                {
                    this.HeadPitchMax.Y = Math.Max(max_head_yp, min_head_yp);
                    this.HeadPitchMin.Y = Math.Min(max_head_yp, min_head_yp);
                }
                else
                {
                    this.HeadPitchMax.Y = int.MaxValue;
                    this.HeadPitchMin.Y = int.MinValue;
                }

                this.mIsHeadHasX = head_has_x;
                this.mIsHeadHasY = head_has_y;

                this.NozzleBypass.X = 1;
                this.NozzleBypass.Y = 1;

                return true;
            }
            return false;
        }

        /// <summary>
        /// 計算 PnP Head 取放點位
        /// </summary>
        /// <param name="tdTray">PnP Head 取放目標</param>
        /// <param name="head_bin">PnP Head 吸嘴上的 Bin 值</param>
        /// <param name="tray_bin">取放盤上的 Bin 值</param>
        /// <param name="pos">TrayData 計算出來的取放點位</param>
        /// <param name="PnPCount">設定取放數量，0 代表不限制</param>
        /// <returns></returns>
        private bool CalcPos(TrayDataEx tdxTray, byte[] head_bin, byte[] tray_bin, ref Point pos,
            ref Point coordinateTrayBlock, ref Point coordinateTrayCell, ref Point coordinateHeadBlock, ref Point coordinateHeadCell,
            bool ColFirst = true, int PnPCount = 0)
        {
            bool bRet = false;
            if (tdxTray != null)
            {
                Nozzles.SetTarget(tdxTray);
                Nozzles.PnPStartingCorner = tdxTray.PnPStartingCorner;

                //*****************************************************************************************************
                // 2020-09-01 Jay Cao Added
                //計算盤跳格數
                int tbcX = 1;   //盤 X 跳格數
                int tbcY = 1;   //盤 Y 跳格數
                if (this.Nozzles.Cols > 1)
                {
                    if (tdxTray.ColPitch < (this.HeadPitchMin.X * this.NozzleBypass.X))
                    {
                        tbcX = Convert.ToInt32(Math.Ceiling((this.HeadPitchMin.X * this.NozzleBypass.X) / (double)tdxTray.ColPitch));
                    }
                }
                if (this.Nozzles.Rows > 1)
                {
                    if (tdxTray.RowPitch < (this.HeadPitchMin.Y * this.NozzleBypass.Y))
                    {
                        tbcY = Convert.ToInt32(Math.Ceiling((this.HeadPitchMin.Y * this.NozzleBypass.Y) / (double)tdxTray.RowPitch));
                    }
                }

                int nozzleXP = Convert.ToInt32((tdxTray.ColPitch * tbcX) / this.NozzleBypass.X);
                int nozzleYP = Convert.ToInt32((tdxTray.RowPitch * tbcY) / this.NozzleBypass.Y);

                //設定吸嘴 X Pitch
                if (nozzleXP > this.HeadPitchMax.X)
                {
                    this.Nozzles.ColPitch = this.HeadPitchMax.X;
                }
                else if (nozzleXP < this.HeadPitchMin.X)
                {
                    this.Nozzles.ColPitch = this.HeadPitchMin.X;
                }
                else
                {
                    this.Nozzles.ColPitch = Convert.ToInt32((tdxTray.ColPitch * tbcX) / this.NozzleBypass.X);
                }
                //設定吸嘴 Y Pitch
                if (nozzleYP > this.HeadPitchMax.Y)
                {
                    this.Nozzles.RowPitch = this.HeadPitchMax.Y;
                }
                else if (nozzleYP < this.HeadPitchMin.Y)
                {
                    this.Nozzles.RowPitch = this.HeadPitchMin.Y;
                }
                else
                {
                    this.Nozzles.RowPitch = Convert.ToInt32((tdxTray.RowPitch * tbcY) / this.NozzleBypass.Y);
                }
                // 2020-09-01 Jay Cao Added
                //*****************************************************************************************************

                if (PnPCount > 0)
                {
                    //依 PnP 需求數量決定使用吸嘴數
                    int NozzleUsingCount = 0;
                    for (int x = 0; x < Nozzles.Cols; x++)
                    {
                        for (int y = 0; y < Nozzles.Rows; y++) 
                        {
                            int StartRows = 0;
                            if (((int)Nozzles.PnPStartingCorner).Equals(1) || ((int)Nozzles.PnPStartingCorner).Equals(3))
                            {
                                StartRows = Nozzles.Rows - 1;
                            }
                            byte bin = Nozzles.GetBin(0, 0, x,Math.Abs(StartRows-y), true);
                            if (bin != (byte)BinDefine.Disabled && !tray_bin.Contains(bin))//Poyao 修改吸滿吸嘴例外狀況
                            {
                                NozzleUsingCount++;
                            }
                            if (NozzleUsingCount > PnPCount)
                            {
                                Nozzles.SetBin(0, 0, x, Math.Abs(StartRows - y), (byte)BinDefine.Disabled, true);     //吸嘴停用
                            }
                        }
                    }
                }
                bRet = Nozzles.CalcPos(head_bin, tray_bin, ref pos, ref coordinateTrayBlock, ref coordinateTrayCell,
                    ref coordinateHeadBlock, ref coordinateHeadCell, ColFirst);
            }
            return bRet;
            //return false;
        }

        /// <summary>
        /// 計算 PnP Head 吸嘴中心基準點與第一支取放吸嘴的(X,Y)偏移量，具有方向性，右正左負，上正下負。
        /// <para>此處計算的偏移量值，雖具有方向性，但是以 Head 具有 X/Y 軸的角度來作計算</para>
        /// </summary>
        /// <param name="trayPnP">Tray的取放起始角落位置</param>
        /// <returns></returns>
        private Point CalcNozzleOffset(TrayCorner trayPnP)
        {
            Point retPos = new Point(0, 0);

            int nxo = (int)(((Nozzles.Cols - 1) * Nozzles.ColPitch) / 2);
            int nyo = (int)(((Nozzles.Rows - 1) * Nozzles.RowPitch) / 2);

            //Tray的取放起始角落位置
            switch (trayPnP)
            {
                //左上
                case TrayCorner.UpperLeft: { retPos.X = (nxo); retPos.Y = (-nyo); } break;
                //左下
                case TrayCorner.LowerLeft: { retPos.X = (nxo); retPos.Y = (nyo); } break;
                //右上
                case TrayCorner.UpperRight: { retPos.X = (-nxo); retPos.Y = (-nyo); } break;
                //右下
                case TrayCorner.LowerRight: { retPos.X = (-nxo); retPos.Y = (nyo); } break;
            }
            return retPos;
        }

        /// <summary>
        /// 修正 TrayData 計算點位錯誤的問題 (ProVSDK v1.1.2.0以上版本已修正此問題)
        /// </summary>
        /// <param name="PnPTray">取放 Tray</param>
        /// <returns></returns>
        private Point RelativePosFix(TrayDataEx PnPTray)
        {
            Point TrayBlockSize = new Point();
            TrayBlockSize.X = (((PnPTray.Cols - 1) * PnPTray.ColPitch) + PnPTray.BlockColPitch);
            TrayBlockSize.Y = (((PnPTray.Rows - 1) * PnPTray.RowPitch) + PnPTray.BlockRowPitch);

            Point HeadBlockSize = new Point();
            HeadBlockSize.X = (((this.Nozzles.Cols - 1) * this.Nozzles.ColPitch) + this.Nozzles.BlockColPitch);
            HeadBlockSize.Y = (((this.Nozzles.Rows - 1) * this.Nozzles.RowPitch) + this.Nozzles.BlockRowPitch);

            Point RelativePosFixed = new Point();
            RelativePosFixed.X = (TrayBlockSize.X * mTrayBlockCoordinate.X) +
                (PnPTray.ColPitch * mTrayCellCoordinate.X) -
                (HeadBlockSize.X * mHeadBlockCoordinate.X) -
                (this.Nozzles.ColPitch * mHeadCellCoordinate.X);
            RelativePosFixed.Y = (TrayBlockSize.Y * mTrayBlockCoordinate.Y) +
                (PnPTray.RowPitch * mTrayCellCoordinate.Y) -
                (HeadBlockSize.Y * mHeadBlockCoordinate.Y) -
                (this.Nozzles.RowPitch * mHeadCellCoordinate.Y);

            switch (PnPTray.PnPStartingCorner)
            {
                case TrayCorner.LowerRight:
                    {
                        RelativePosFixed.X = (-RelativePosFixed.X);
                    }
                    break;

                case TrayCorner.UpperLeft:
                    {
                        RelativePosFixed.Y = (-RelativePosFixed.Y);
                    }
                    break;

                case TrayCorner.UpperRight:
                    {
                        RelativePosFixed.X = (-RelativePosFixed.X);
                        RelativePosFixed.Y = (-RelativePosFixed.Y);
                    }
                    break;
            }
            return RelativePosFixed;
        }

        #endregion 私有函式

        #region 公有函式

        /// <summary>
        /// 取得吸嘴狀態陣列
        /// </summary>
        /// <returns>吸嘴狀態陣列</returns>
        public NozzleState[,] GetNozzleState()
        {
            NozzleState[,] nozzleState = null;
            if (Nozzles != null)
            {
                nozzleState = new NozzleState[Nozzles.Cols, Nozzles.Rows];
                for (int i = 0; i < Nozzles.Cols; i++)
                {
                    for (int j = 0; j < Nozzles.Rows; j++)
                    {
                        nozzleState[i, j] = NozzleState.Unused;
                    }
                }
                for (int i = 0; i < Nozzles.Cols; i++)
                {
                    for (int j = 0; j < Nozzles.Rows; j++)
                    {
                        //使用慣用座標(原點固定為左上角)來檢查每支吸嘴的狀態
                        byte state = Nozzles.GetState(0, 0, i, j);
                        if (state.Equals(1))
                        {
                            nozzleState[i, j] = NozzleState.InUsing;
                        }
                    }
                }
            }
            return nozzleState;
        }

        /// <summary>
        /// 重新計算吸嘴跳支數
        /// </summary>
        /// <param name="src">取料盤 TrayDataEx 物件</param>
        /// <param name="trg">放料盤 TrayDataEx 物件</param>
        /// <returns>
        /// <para>true : 依計算得出的跳支數來切換吸嘴 Pitch 並未小於吸嘴的最小 Pitch</para>
        /// <para>false : 依計算得出的跳支數來切換吸嘴 Pitch 但卻小於吸嘴的最小 Pitch (僅供提醒，若繼續則使用吸嘴最小 Pitch)</para>
        /// </returns>
        public bool ResetNozzleBypass(TrayDataEx src = null, TrayDataEx trg = null)
        {
            bool bRetX = true;
            bool bRetY = true;

            double _maxTrayXP = Math.Max(((src != null) ? src.ColPitch : 0), ((trg != null) ? trg.ColPitch : 0));
            double _maxTrayYP = Math.Max(((src != null) ? src.RowPitch : 0), ((trg != null) ? trg.RowPitch : 0));

            //吸嘴跳支數重置為 1 (跳支數 1 代表沒有跳支)
            this.NozzleBypass.X = 1;
            this.NozzleBypass.Y = 1;

            if (this.HeadPitchMax.X > this.HeadPitchMin.X)  //若兩者想等則代表是固定 Pitch
            {
                //計算 X Pitch
                if ((this.Nozzles.Cols > 1) && (_maxTrayXP > 0))
                {
                    if (_maxTrayXP > this.HeadPitchMax.X)
                    {
                        //吸嘴跳支 (採無條件進入法)
                        this.NozzleBypass.X = Convert.ToInt32(Math.Ceiling(_maxTrayXP / this.HeadPitchMax.X));
                        if ((_maxTrayXP / this.NozzleBypass.X) < this.HeadPitchMin.X)
                        {
                            //吸嘴跳支後的 pitch 小於最小 pitch，無法使用
                            bRetX = false;
                        }
                    }
                }
            }

            if (this.HeadPitchMax.Y > this.HeadPitchMin.Y)  //若兩者想等則代表是固定 Pitch
            {
                //計算 Y Pitch
                if ((this.Nozzles.Rows > 1) && (_maxTrayYP > 0))
                {
                    if (_maxTrayYP > this.HeadPitchMax.Y)
                    {
                        //吸嘴跳支 (採無條件進入法)
                        this.NozzleBypass.Y = Convert.ToInt32(Math.Ceiling(_maxTrayYP / this.HeadPitchMax.Y));
                        if ((_maxTrayYP / this.NozzleBypass.Y) < this.HeadPitchMin.Y)
                        {
                            //吸嘴跳支後的 pitch 小於最小 pitch，無法使用
                            bRetY = false;
                        }
                    }
                }
            }

            //將吸嘴 Bin 值清空
            this.Nozzles.ResetBin(new byte[] { (byte)BinDefine.Empty });

            //若吸嘴有跳支，要將吸嘴停用
            for (int x = 0; x < this.Nozzles.Cols; x++)
            {
                for (int y = 0; y < this.Nozzles.Rows; y++)
                {
                    if ((x % this.NozzleBypass.X) != 0 || (y % this.NozzleBypass.Y) != 0)
                    {
                        this.Nozzles.SetBin(0, 0, x, y, (byte)BinDefine.Disabled, false);
                    }
                }
            }
            this.Nozzles.Refresh();
            return (bRetX && bRetY);
        }

        /// <summary>
        /// 計算 PnP Head 取放點位
        /// </summary>
        /// <param name="PnPTray">取放目標 TrayDataEx 物件</param>
        /// <param name="head_bin">吸嘴上的 Bin 資料</param>
        /// <param name="tray_bin">取放目標盤上的 Bin 資料</param>
        /// <param name="ColFirst">true : 行(Y方向)優先取放，false : 列(X方向)優先取放</param>
        /// <param name="PnPCount">設定取放數量，0 代表不限制</param>
        /// <returns>
        /// <c>true</c> 取放點位計算完成，<c>false</c> 取放點位計算失敗，可能 Tray 或 Head 無符合條件的 Bin 值
        /// </returns>
        public bool CalcPnPPos(TrayDataEx PnPTray, byte[] head_bin, byte[] tray_bin, bool ColFirst = true, int PnPCount = 0)
        {
            bool bRet = false;
            if (PnPTray != null)
            {
                if (CalcPos(PnPTray, head_bin, tray_bin, ref mRelativePos, ref mTrayBlockCoordinate,
                    ref mTrayCellCoordinate, ref mHeadBlockCoordinate, ref mHeadCellCoordinate, ColFirst, PnPCount))
                {
                    // 修正 TrayData 計算點位錯誤的問題 (ProVSDK v1.1.2.0以上版本已修正此問題)
                    mRelativePosFixed = RelativePosFix(PnPTray);
                    // 取得 Tray Offset (此處偏移量值，雖具有方向性，但都是以 Head 具有 X/Y 軸的角度來作計算)
                    Point TO = PnPTray.GetTrayOffset();
                    // 計算 Nozzle Offset (此處偏移量值，雖具有方向性，但都是以 Head 具有 X/Y 軸的角度來作計算)
                    Point NO = CalcNozzleOffset(PnPTray.PnPStartingCorner);

                    // 計算取放絕對位置 (此處需判斷 Head 是否具有 X/Y 來計算取放絕對位置) - 2020/12/11 v1.5 Jay Tsao Modified.
                    if (this.mIsHeadHasX)
                    {
                        // Head 有 X 軸
                        mAbsolutePos.X = PnPTray.BasePos.X + (TO.X + NO.X + mRelativePos.X);
                    }
                    else
                    {
                        // Head 無 X 軸 (Tray 有 X 軸)
                        mAbsolutePos.X = PnPTray.BasePos.X - (TO.X + NO.X + mRelativePos.X);
                    }
                    if (this.mIsHeadHasY)
                    {
                        // Head 有 Y 軸
                        mAbsolutePos.Y = PnPTray.BasePos.Y + (TO.Y + NO.Y + mRelativePos.Y);
                    }
                    else
                    {
                        // Head 無 Y 軸 (Tray 有 Y 軸)
                        mAbsolutePos.Y = PnPTray.BasePos.Y - (TO.Y + NO.Y + mRelativePos.Y);
                    }
                    bRet = true;
                }
            }
            return bRet;
        }

        /// <summary>
        /// PnP Head 與目標 Tray 資料交換
        /// </summary>
        public void DataExchange()
        {
            Nozzles.DataExchange();
        }

        #endregion 公有函式
    }
}