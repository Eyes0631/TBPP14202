namespace TBPP14200
{
    partial class InitialForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InitialForm));
            this.imglist = new System.Windows.Forms.ImageList(this.components);
            this.lbMachineState = new System.Windows.Forms.Label();
            this.lbKSM = new KCSDK.LEDLabel();
            this.tmRefresh = new System.Windows.Forms.Timer(this.components);
            this.picMachine = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lb_TRM = new KCSDK.LEDLabel();
            this.lbChM = new KCSDK.LEDLabel();
            this.lbBFM = new KCSDK.LEDLabel();
            this.lbHDT = new KCSDK.LEDLabel();
            this.lbBSM = new KCSDK.LEDLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picMachine)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imglist
            // 
            this.imglist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglist.ImageStream")));
            this.imglist.TransparentColor = System.Drawing.Color.White;
            this.imglist.Images.SetKeyName(0, "cancel.png");
            // 
            // lbMachineState
            // 
            this.lbMachineState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbMachineState.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbMachineState.Font = new System.Drawing.Font("微軟正黑體", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbMachineState.ForeColor = System.Drawing.Color.Black;
            this.lbMachineState.Location = new System.Drawing.Point(0, 0);
            this.lbMachineState.Name = "lbMachineState";
            this.lbMachineState.Size = new System.Drawing.Size(1121, 86);
            this.lbMachineState.TabIndex = 6;
            this.lbMachineState.Text = "機台初始化中";
            this.lbMachineState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbKSM
            // 
            this.lbKSM.BackColor = System.Drawing.Color.Black;
            this.lbKSM.Caption = "KSM";
            this.lbKSM.CaptionFont = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbKSM.CpationColor = System.Drawing.Color.Yellow;
            this.lbKSM.Location = new System.Drawing.Point(14, 273);
            this.lbKSM.Name = "lbKSM";
            this.lbKSM.Size = new System.Drawing.Size(170, 40);
            this.lbKSM.TabIndex = 9;
            this.lbKSM.Value = false;
            // 
            // tmRefresh
            // 
            this.tmRefresh.Interval = 40;
            this.tmRefresh.Tick += new System.EventHandler(this.tmRefresh_Tick);
            // 
            // picMachine
            // 
            this.picMachine.BackgroundImage = global::TBPP14200.Properties.Resources.top_view;
            this.picMachine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMachine.Location = new System.Drawing.Point(0, 86);
            this.picMachine.Name = "picMachine";
            this.picMachine.Size = new System.Drawing.Size(1121, 638);
            this.picMachine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picMachine.TabIndex = 8;
            this.picMachine.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("微軟正黑體", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.ImageKey = "cancel.png";
            this.btnCancel.ImageList = this.imglist;
            this.btnCancel.Location = new System.Drawing.Point(834, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(127, 84);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取  消";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lb_TRM
            // 
            this.lb_TRM.BackColor = System.Drawing.Color.Black;
            this.lb_TRM.Caption = "TRM";
            this.lb_TRM.CaptionFont = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_TRM.CpationColor = System.Drawing.Color.Yellow;
            this.lb_TRM.Location = new System.Drawing.Point(14, 439);
            this.lb_TRM.Name = "lb_TRM";
            this.lb_TRM.Size = new System.Drawing.Size(170, 40);
            this.lb_TRM.TabIndex = 10;
            this.lb_TRM.Value = false;
            // 
            // lbChM
            // 
            this.lbChM.BackColor = System.Drawing.Color.Black;
            this.lbChM.Caption = "CHM";
            this.lbChM.CaptionFont = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbChM.CpationColor = System.Drawing.Color.Yellow;
            this.lbChM.Location = new System.Drawing.Point(14, 398);
            this.lbChM.Name = "lbChM";
            this.lbChM.Size = new System.Drawing.Size(170, 40);
            this.lbChM.TabIndex = 11;
            this.lbChM.Value = false;
            // 
            // lbBFM
            // 
            this.lbBFM.BackColor = System.Drawing.Color.Black;
            this.lbBFM.Caption = "BFM";
            this.lbBFM.CaptionFont = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbBFM.CpationColor = System.Drawing.Color.Yellow;
            this.lbBFM.Location = new System.Drawing.Point(14, 231);
            this.lbBFM.Name = "lbBFM";
            this.lbBFM.Size = new System.Drawing.Size(170, 40);
            this.lbBFM.TabIndex = 12;
            this.lbBFM.Value = false;
            // 
            // lbHDT
            // 
            this.lbHDT.BackColor = System.Drawing.Color.Black;
            this.lbHDT.Caption = "HDT";
            this.lbHDT.CaptionFont = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbHDT.CpationColor = System.Drawing.Color.Yellow;
            this.lbHDT.Location = new System.Drawing.Point(14, 315);
            this.lbHDT.Name = "lbHDT";
            this.lbHDT.Size = new System.Drawing.Size(170, 40);
            this.lbHDT.TabIndex = 13;
            this.lbHDT.Value = false;
            // 
            // lbBSM
            // 
            this.lbBSM.BackColor = System.Drawing.Color.Black;
            this.lbBSM.Caption = "BSM";
            this.lbBSM.CaptionFont = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbBSM.CpationColor = System.Drawing.Color.Yellow;
            this.lbBSM.Location = new System.Drawing.Point(14, 357);
            this.lbBSM.Name = "lbBSM";
            this.lbBSM.Size = new System.Drawing.Size(170, 40);
            this.lbBSM.TabIndex = 14;
            this.lbBSM.Value = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbHDT);
            this.panel1.Controls.Add(this.lbKSM);
            this.panel1.Controls.Add(this.lbBSM);
            this.panel1.Controls.Add(this.lb_TRM);
            this.panel1.Controls.Add(this.lbBFM);
            this.panel1.Controls.Add(this.lbChM);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(825, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(296, 638);
            this.panel1.TabIndex = 16;
            // 
            // InitialForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.ClientSize = new System.Drawing.Size(1121, 724);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.picMachine);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lbMachineState);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "InitialForm";
            this.Text = "初始化";
            this.Load += new System.EventHandler(this.InitialForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picMachine)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbMachineState;
        private System.Windows.Forms.ImageList imglist;
        private System.Windows.Forms.PictureBox picMachine;
        private System.Windows.Forms.Button btnCancel;
        private KCSDK.LEDLabel lbKSM;
        private System.Windows.Forms.Timer tmRefresh;
        private KCSDK.LEDLabel lb_TRM;
        private KCSDK.LEDLabel lbChM;
        private KCSDK.LEDLabel lbBFM;
        private KCSDK.LEDLabel lbHDT;
        private KCSDK.LEDLabel lbBSM;
        private System.Windows.Forms.Panel panel1;
    }
}