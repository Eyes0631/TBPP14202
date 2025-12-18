using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PaeLibComponent
{
    /// <summary>
    /// JAC 基礎控制項，所有的控制項接繼承於此
    /// </summary>
    [ToolboxItem(false)]    //工具箱中不顯示此控制項
    [Designer(typeof(JBasicViewDesigner))]     //設定那些屬性要在子類別中顯示
    public partial class JRoundRectBase : UserControl
    {
        /// <summary>
        /// 框線寬度
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public int EdgeWitch { get; set; }

        /// <summary>
        /// 圓弧角半徑
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public int CornerRadius { get; set; }

        /// <summary>
        /// 邊框顏色
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public Color EdgeColor { get; set; }

        /// <summary>
        /// 是否填滿背景
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool FillBackground { get; set; }

        /// <summary>
        /// 填滿顏色
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public Color FillColr { get; set; }

        /// <summary>
        /// 是否顯示標題
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool ShowCaption { get; set; }

        /// <summary>
        /// 標題內容
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string Caption { get; set; }

        /// <summary>
        /// 標題顏色
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public Color CaptionColor { get; set; }

        /// <summary>
        /// 標題寬度
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public int CaptionWidth { get; set; }

        /// <summary>
        /// 標圖高度
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public int CaptionHeight { get; set; }

        /// <summary>
        /// 標題置中
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool CaptionAlignCenter { get; set; }

        //提示訊息類別
        protected ToolTip tt = null;

        /// <summary>
        /// 建構子
        /// </summary>
        public JRoundRectBase()
        {
            InitializeComponent();

            // Create the ToolTip and associate with the Form container.
            tt = new ToolTip();
            // Set up the delays for the ToolTip.
            tt.AutoPopDelay = 5000;
            tt.InitialDelay = 500;
            tt.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            tt.ShowAlways = true;

            //CaptionParameter = new CaptionPara();
            this.EdgeWitch = 2;
            this.CornerRadius = 5;
            this.EdgeColor = Color.Black;

            this.FillBackground = false;
            this.FillColr = Color.White;

            this.ShowCaption = false;
            this.Caption = string.Empty;
            this.CaptionWidth = this.Width;
            this.CaptionHeight = this.Height;
            this.CaptionAlignCenter = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //int EdgeWidth = Math.Max(1, this.Width / 20);
            //EdgeWidth = Math.Min(5, EdgeWidth);
            //int EdgeHeight = Math.Max(1, this.Height / 20);
            //EdgeHeight = Math.Min(5, EdgeHeight);

            //int EdgeWidth = 2;
            //CaptionParameter.Width = Math.Min((this.Width - (EdgeWidth * 2)), CaptionParameter.Width);
            //CaptionParameter.Height = Math.Min((this.Height - (EdgeWidth * 2)), CaptionParameter.Height);

            //設定控制項背景顏色為透明
            this.BackColor = Color.Transparent;
            //繪製邊框
            DrawRoundRect(e.Graphics, new Rectangle(0, 0, this.Width, this.Height), this.EdgeWitch, this.CornerRadius, this.EdgeColor);
            //繪製底色
            if (this.FillBackground)
            {
                int X = (int)(this.EdgeWitch * 1);
                int Y = (int)(this.EdgeWitch * 1);
                int W = this.Width - (this.EdgeWitch * 3);
                int H = this.Height - (this.EdgeWitch * 3);
                FillRoundRect(e.Graphics, new Rectangle(X, Y, W, H), this.CornerRadius, this.FillColr);
            }
            //繪製文字
            if (this.ShowCaption)
            {
                int X = (int)(this.EdgeWitch * 1);
                int Y = (int)(this.EdgeWitch * 1);
                int W = this.Width - (this.EdgeWitch * 3);
                int H = this.Height - (this.EdgeWitch * 3);
                DrawCaption(e.Graphics, new Rectangle(X, Y, W, H), this.Caption, this.CaptionColor, true, this.CaptionAlignCenter);
            }
            //繪製元件自定義內容
            CustomPaint(e.Graphics);
        }

        //private void JAC_BaseUC_Paint(object sender, PaintEventArgs e)
        //{
        //}

        /// <summary>
        /// 繪製圓角矩形函式
        /// </summary>
        /// <param name="rect">矩形參數(左上角位置X, Y與寬、高)</param>
        /// <param name="g">繪圖物件</param>
        /// <param name="fillColor">矩形內填滿的顏色</param>
        protected void DrawRoundRect(Graphics g, Rectangle rect, int edgeWidth, int cornerRadius, Color edgeColor)
        {
            int X = rect.X + edgeWidth / 2;
            int Y = rect.Y + edgeWidth / 2;
            int R = rect.Right - edgeWidth;
            int B = rect.Bottom - edgeWidth;
            int r = cornerRadius;// Math.Max(1, Math.Min(R, B) / 5);         //半徑
            int d = (r * 2);    //直徑

            GraphicsPath roundedRectPath = new GraphicsPath();
            //畫左上角1/4圓弧
            roundedRectPath.AddArc(X, Y, d, d, 180, 90);
            //畫上邊
            roundedRectPath.AddLine(X + r, Y, R - r, Y);
            //畫右上角1/4圓弧
            roundedRectPath.AddArc(R - d, Y, d, d, 270, 90);
            //畫右邊
            roundedRectPath.AddLine(R, Y + r, R, B - r);
            //畫右下角1/4圓弧
            roundedRectPath.AddArc(R - d, B - d, d, d, 0, 90);
            //畫下邊
            roundedRectPath.AddLine(R - r, B, X + r, B);
            //畫左下角1/4圓弧
            roundedRectPath.AddArc(X, B - d, d, d, 90, 90);
            //畫左邊
            roundedRectPath.AddLine(X, B - d, X, Y + r);

            g.DrawPath(new Pen(edgeColor, edgeWidth), roundedRectPath);

            g.SmoothingMode = SmoothingMode.AntiAlias;      //指定反鋸齒呈現
        }

        /// <summary>
        /// 繪製圓角矩形函式
        /// </summary>
        /// <param name="rect">矩形參數(左上角位置X, Y與寬、高)</param>
        /// <param name="g">繪圖物件</param>
        /// <param name="fillColor">矩形內填滿的顏色</param>
        protected void FillRoundRect(Graphics g, Rectangle rect, int cornerRadius, Color fillColor)
        {
            //int edgeWidth = 0;
            int X = rect.X;// +edgeWidth / 2;
            int Y = rect.Y;// +edgeWidth / 2;
            int R = rect.Right;// -edgeWidth;
            int B = rect.Bottom;// -edgeWidth;
            int r = cornerRadius;// Math.Max(1, Math.Min(R, B) / 5);         //半徑
            int d = (r * 2);    //直徑

            GraphicsPath roundedRectPath = new GraphicsPath();
            //畫左上角1/4圓弧
            roundedRectPath.AddArc(X, Y, d, d, 180, 90);
            //畫上邊
            roundedRectPath.AddLine(X + r, Y, R - r, Y);
            //畫右上角1/4圓弧
            roundedRectPath.AddArc(R - d, Y, d, d, 270, 90);
            //畫右邊
            roundedRectPath.AddLine(R, Y + r, R, B - r);
            //畫右下角1/4圓弧
            roundedRectPath.AddArc(R - d, B - d, d, d, 0, 90);
            //畫下邊
            roundedRectPath.AddLine(R - r, B, X + r, B);
            //畫左下角1/4圓弧
            roundedRectPath.AddArc(X, B - d, d, d, 90, 90);
            //畫左邊
            roundedRectPath.AddLine(X, B - d, X, Y + r);

            g.FillPath(new SolidBrush(fillColor), roundedRectPath);

            g.SmoothingMode = SmoothingMode.AntiAlias;      //指定反鋸齒呈現
        }

        /// <summary>
        /// 繪製文字標題
        /// </summary>
        /// <param name="rect">文字標題矩形參數(左上角位置X, Y與寬、高)</param>
        /// <param name="g">繪圖物件</param>
        /// <param name="caption">文字標題內容</param>
        /// <param name="brush">文字標題顏色</param>
        /// <param name="bold">文字標題是否粗體</param>
        /// <param name="center">文字標題是否水平置中</param>
        protected void DrawCaption(Graphics g, Rectangle rect, string caption, Color brush, bool bold, bool center)
        {
            //Set Font of caption.
            Font drawFont;
            if (bold)
            {
                drawFont = new Font(this.Font, FontStyle.Bold);
            }
            else
            {
                drawFont = new Font(this.Font, FontStyle.Regular);
            }

            //自動換行
            List<string> textList = new List<string>();
            int captionTextH = (int)g.MeasureString(caption, drawFont).Height;
            int textStartIndex = 0;
            for (int i = 0; i < caption.Length; i++)
            {
                // i 代表 textEndIndex
                string currentText = caption.Substring(textStartIndex, (i - textStartIndex + 1));
                if (g.MeasureString(currentText, drawFont).Width > rect.Width)
                {
                    textList.Add(caption.Substring(textStartIndex, (i - textStartIndex)));
                    textStartIndex = i;
                }
            }
            textList.Add(caption.Substring(textStartIndex, (caption.Length - textStartIndex)));

            //Set brush of caption
            SolidBrush brushCaption = new SolidBrush(brush);
            // Set format of caption.
            StringFormat drawFormat = new StringFormat();
            if (center)
            {
                drawFormat.Alignment = StringAlignment.Center;      //文字水平置中
            }
            else
            {
                drawFormat.Alignment = StringAlignment.Near;      //文字水平靠左
            }
            drawFormat.LineAlignment = StringAlignment.Center;      //文字垂直置中
            g.TextContrast = 0;

            //Draw the string of caption
            //g.DrawString(caption, drawFont, brushCaption, rect, drawFormat);      //不支援自動換行

            //文字自動換行
            int textTotalH = captionTextH * textList.Count;
            int textTop = rect.Top + ((rect.Height - textTotalH) / 2);
            for (int i = 0; i < textList.Count; i++)
            {
                Rectangle rectText = new Rectangle(rect.Left, textTop + (captionTextH * i), rect.Width, captionTextH);
                g.DrawString(textList[i], drawFont, brushCaption, rectText, drawFormat);
            }
        }

        /// <summary>
        /// 虛擬函式 - 自定義繪製內容
        /// </summary>
        /// <param name="g"></param>
        protected virtual void CustomPaint(Graphics g)
        {
        }

        /// <summary>
        /// 虛擬函式 - 更新 JAC_BaseUC 控制項 UI 顯示的狀態 (在主行程中呼叫，不可在執行緒中呼叫)
        /// </summary>
        //public virtual void RefreshStatus()
        //{
        //}

        /// <summary>
        /// 虛擬函式 - 更新 JAC_BaseUC 控制項的內部狀態值  (在執行緒中呼叫 - AlwayseRun中)
        /// </summary>
        //public virtual void UpdateStatus()
        //{
        //}
    }
}