namespace TBPP14200
{
    partial class MainF1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainF1));
            this.pnlControl = new System.Windows.Forms.Panel();
            this.LightLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ledBlue = new KCSDK.ThreeColorLight();
            this.ledRed = new KCSDK.ThreeColorLight();
            this.ledYellow = new KCSDK.ThreeColorLight();
            this.ledGreen = new KCSDK.ThreeColorLight();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnOption = new System.Windows.Forms.Button();
            this.btnAuto = new System.Windows.Forms.Button();
            this.lbNowTime = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPackage = new System.Windows.Forms.Button();
            this.btnManual = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnLang = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lbRunState = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlPackage = new System.Windows.Forms.Panel();
            this.lbPackage = new System.Windows.Forms.Label();
            this.lbProjectName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbPackageName = new System.Windows.Forms.Label();
            this.pnlUser = new System.Windows.Forms.Panel();
            this.btnChangeUser = new System.Windows.Forms.Button();
            this.lbUserType = new System.Windows.Forms.Label();
            this.pnlAlarmGrid = new System.Windows.Forms.Panel();
            this.lvArmGrid = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.tabMachineState = new System.Windows.Forms.TabControl();
            this.tpProductionState = new System.Windows.Forms.TabPage();
            this.lb_UPH = new KCSDK.NumLabel();
            this.lb_BibCount = new KCSDK.NumLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this.tv_RightLoadUnload = new ProVLib.TrayView();
            this.lb_LoadCompletedQty = new KCSDK.NumLabel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.tv_LeftLoadUnload = new ProVLib.TrayView();
            this.lb_LoadProcessQty = new KCSDK.NumLabel();
            this.button2 = new System.Windows.Forms.Button();
            this.lb_LoadMissingQty = new KCSDK.NumLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel22 = new System.Windows.Forms.Panel();
            this.panel28 = new System.Windows.Forms.Panel();
            this.lb_RunMode = new System.Windows.Forms.Label();
            this.panel27 = new System.Windows.Forms.Panel();
            this.lb_LotID = new System.Windows.Forms.Label();
            this.panel26 = new System.Windows.Forms.Panel();
            this.panel25 = new System.Windows.Forms.Panel();
            this.lb_RBoardID = new System.Windows.Forms.Label();
            this.panel24 = new System.Windows.Forms.Panel();
            this.panel23 = new System.Windows.Forms.Panel();
            this.lb_LBoardID = new System.Windows.Forms.Label();
            this.lb_UnloadProcessQty = new KCSDK.NumLabel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.tv_Transfer = new ProVLib.TrayView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.tv_ClamSheel = new ProVLib.TrayView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.tv_LeftBoard = new ProVLib.TrayView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.tv_RightBoard = new ProVLib.TrayView();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tv_HDT_BottomBoard = new ProVLib.TrayView();
            this.tv_HDT_TopBoard = new ProVLib.TrayView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.tv_RightKit = new ProVLib.TrayView();
            this.tv_LeftKit = new ProVLib.TrayView();
            this.pnl異常通知 = new System.Windows.Forms.Panel();
            this.lb異常訊息 = new System.Windows.Forms.Label();
            this.tpKernalData = new System.Windows.Forms.TabPage();
            this.lb嫁動率 = new KCSDK.NumLabel();
            this.TB_授權有效性 = new KCSDK.TextLabel();
            this.TB_授權模式 = new KCSDK.TextLabel();
            this.btnResetMaintenance = new System.Windows.Forms.Button();
            this.btnResetManual = new System.Windows.Forms.Button();
            this.btnResetHome = new System.Windows.Forms.Button();
            this.btnResetIDLE = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbMaintenanceTM = new KCSDK.TextLabel();
            this.lbManualTM = new KCSDK.TextLabel();
            this.lbHomeTM = new KCSDK.TextLabel();
            this.lbIdleTM = new KCSDK.TextLabel();
            this.lbPauseTM = new KCSDK.TextLabel();
            this.lbRunTM = new KCSDK.TextLabel();
            this.lbOperationEndTM = new KCSDK.TextLabel();
            this.lbOperationStartTM = new KCSDK.TextLabel();
            this.lbScanCNT = new KCSDK.NumLabel();
            this.lbScanTM = new KCSDK.NumLabel();
            this.tpMSS = new System.Windows.Forms.TabPage();
            this.tpMainFlow = new System.Windows.Forms.TabPage();
            this.tpCommunication = new System.Windows.Forms.TabPage();
            this.tmUIRefresh = new System.Windows.Forms.Timer(this.components);
            this.tv_LeftTray = new ProVLib.TrayView();
            this.label36 = new System.Windows.Forms.Label();
            this.tv_RightTray = new ProVLib.TrayView();
            this.label37 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tv_HDT_TR = new ProVLib.TrayView();
            this.label5 = new System.Windows.Forms.Label();
            this.numLabel1 = new KCSDK.NumLabel();
            this.numLabel2 = new KCSDK.NumLabel();
            this.pnlControl.SuspendLayout();
            this.LightLayoutPanel.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlPackage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlUser.SuspendLayout();
            this.pnlAlarmGrid.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.tabMachineState.SuspendLayout();
            this.tpProductionState.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panel22.SuspendLayout();
            this.panel28.SuspendLayout();
            this.panel27.SuspendLayout();
            this.panel25.SuspendLayout();
            this.panel23.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnl異常通知.SuspendLayout();
            this.tpKernalData.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlControl
            // 
            this.pnlControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(42)))), ((int)(((byte)(50)))));
            this.pnlControl.Controls.Add(this.LightLayoutPanel);
            this.pnlControl.Controls.Add(this.btnHome);
            this.pnlControl.Controls.Add(this.btnOption);
            this.pnlControl.Controls.Add(this.btnAuto);
            this.pnlControl.Controls.Add(this.lbNowTime);
            this.pnlControl.Controls.Add(this.lbVersion);
            this.pnlControl.Controls.Add(this.btnExit);
            this.pnlControl.Controls.Add(this.btnPackage);
            this.pnlControl.Controls.Add(this.btnManual);
            this.pnlControl.Controls.Add(this.btnHistory);
            this.pnlControl.Controls.Add(this.btnLang);
            this.pnlControl.Controls.Add(this.btnReset);
            this.pnlControl.Controls.Add(this.btnPause);
            this.pnlControl.Controls.Add(this.btnStart);
            this.pnlControl.Controls.Add(this.lbRunState);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControl.Location = new System.Drawing.Point(1301, 2);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(200, 984);
            this.pnlControl.TabIndex = 0;
            // 
            // LightLayoutPanel
            // 
            this.LightLayoutPanel.ColumnCount = 4;
            this.LightLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.LightLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.LightLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.LightLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.LightLayoutPanel.Controls.Add(this.ledBlue, 3, 0);
            this.LightLayoutPanel.Controls.Add(this.ledRed, 0, 0);
            this.LightLayoutPanel.Controls.Add(this.ledYellow, 1, 0);
            this.LightLayoutPanel.Controls.Add(this.ledGreen, 2, 0);
            this.LightLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.LightLayoutPanel.Location = new System.Drawing.Point(0, 63);
            this.LightLayoutPanel.Name = "LightLayoutPanel";
            this.LightLayoutPanel.RowCount = 1;
            this.LightLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LightLayoutPanel.Size = new System.Drawing.Size(200, 53);
            this.LightLayoutPanel.TabIndex = 38;
            // 
            // ledBlue
            // 
            this.ledBlue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ledBlue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ledBlue.BackgroundImage")));
            this.ledBlue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ledBlue.Location = new System.Drawing.Point(155, 6);
            this.ledBlue.Name = "ledBlue";
            this.ledBlue.Size = new System.Drawing.Size(40, 40);
            this.ledBlue.TabIndex = 35;
            this.ledBlue.Value = KCSDK.ThreeColorLight.ColorLightType.BlueLight;
            // 
            // ledRed
            // 
            this.ledRed.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ledRed.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ledRed.BackgroundImage")));
            this.ledRed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ledRed.Location = new System.Drawing.Point(5, 6);
            this.ledRed.Name = "ledRed";
            this.ledRed.Size = new System.Drawing.Size(40, 40);
            this.ledRed.TabIndex = 32;
            this.ledRed.Value = KCSDK.ThreeColorLight.ColorLightType.RedLight;
            // 
            // ledYellow
            // 
            this.ledYellow.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ledYellow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ledYellow.BackgroundImage")));
            this.ledYellow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ledYellow.Location = new System.Drawing.Point(55, 6);
            this.ledYellow.Name = "ledYellow";
            this.ledYellow.Size = new System.Drawing.Size(40, 40);
            this.ledYellow.TabIndex = 33;
            this.ledYellow.Value = KCSDK.ThreeColorLight.ColorLightType.YellowLight;
            // 
            // ledGreen
            // 
            this.ledGreen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ledGreen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ledGreen.BackgroundImage")));
            this.ledGreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ledGreen.Location = new System.Drawing.Point(105, 6);
            this.ledGreen.Name = "ledGreen";
            this.ledGreen.Size = new System.Drawing.Size(40, 40);
            this.ledGreen.TabIndex = 34;
            this.ledGreen.Value = KCSDK.ThreeColorLight.ColorLightType.GreenLight;
            // 
            // btnHome
            // 
            this.btnHome.AutoSize = true;
            this.btnHome.BackgroundImage = global::TBPP14200.Properties.Resources.btnHome_tw;
            this.btnHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnHome.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHome.Location = new System.Drawing.Point(2, 328);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(197, 45);
            this.btnHome.TabIndex = 36;
            this.btnHome.Tag = "1";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnOption
            // 
            this.btnOption.AutoSize = true;
            this.btnOption.BackgroundImage = global::TBPP14200.Properties.Resources.btnOption_tw;
            this.btnOption.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnOption.FlatAppearance.BorderSize = 0;
            this.btnOption.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOption.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.btnOption.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOption.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOption.Location = new System.Drawing.Point(2, 528);
            this.btnOption.Name = "btnOption";
            this.btnOption.Size = new System.Drawing.Size(197, 45);
            this.btnOption.TabIndex = 35;
            this.btnOption.Tag = "4";
            this.btnOption.UseVisualStyleBackColor = true;
            this.btnOption.Click += new System.EventHandler(this.PageChange);
            // 
            // btnAuto
            // 
            this.btnAuto.AutoSize = true;
            this.btnAuto.BackgroundImage = global::TBPP14200.Properties.Resources.btnAuto_tw;
            this.btnAuto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAuto.FlatAppearance.BorderSize = 0;
            this.btnAuto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAuto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.btnAuto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAuto.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAuto.Location = new System.Drawing.Point(2, 378);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(197, 45);
            this.btnAuto.TabIndex = 31;
            this.btnAuto.Tag = "1";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Click += new System.EventHandler(this.PageChange);
            // 
            // lbNowTime
            // 
            this.lbNowTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(42)))), ((int)(((byte)(50)))));
            this.lbNowTime.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbNowTime.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbNowTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbNowTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbNowTime.Location = new System.Drawing.Point(0, 938);
            this.lbNowTime.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lbNowTime.Name = "lbNowTime";
            this.lbNowTime.Size = new System.Drawing.Size(200, 23);
            this.lbNowTime.TabIndex = 29;
            this.lbNowTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbVersion
            // 
            this.lbVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(42)))), ((int)(((byte)(50)))));
            this.lbVersion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbVersion.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbVersion.ForeColor = System.Drawing.Color.Silver;
            this.lbVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbVersion.Location = new System.Drawing.Point(0, 961);
            this.lbVersion.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(200, 23);
            this.lbVersion.TabIndex = 30;
            this.lbVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
            this.btnExit.AutoSize = true;
            this.btnExit.BackgroundImage = global::TBPP14200.Properties.Resources.btnExit_tw;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExit.Location = new System.Drawing.Point(2, 748);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(197, 45);
            this.btnExit.TabIndex = 25;
            this.btnExit.Tag = "5";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPackage
            // 
            this.btnPackage.AutoSize = true;
            this.btnPackage.BackgroundImage = global::TBPP14200.Properties.Resources.btnPackage_tw;
            this.btnPackage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPackage.FlatAppearance.BorderSize = 0;
            this.btnPackage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPackage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.btnPackage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPackage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPackage.Location = new System.Drawing.Point(2, 478);
            this.btnPackage.Name = "btnPackage";
            this.btnPackage.Size = new System.Drawing.Size(197, 45);
            this.btnPackage.TabIndex = 24;
            this.btnPackage.Tag = "3";
            this.btnPackage.UseVisualStyleBackColor = true;
            this.btnPackage.Click += new System.EventHandler(this.PageChange);
            // 
            // btnManual
            // 
            this.btnManual.AutoSize = true;
            this.btnManual.BackgroundImage = global::TBPP14200.Properties.Resources.btnManual_tw;
            this.btnManual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnManual.FlatAppearance.BorderSize = 0;
            this.btnManual.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnManual.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.btnManual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManual.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnManual.Location = new System.Drawing.Point(2, 428);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(197, 45);
            this.btnManual.TabIndex = 23;
            this.btnManual.Tag = "2";
            this.btnManual.UseVisualStyleBackColor = true;
            this.btnManual.Click += new System.EventHandler(this.PageChange);
            // 
            // btnHistory
            // 
            this.btnHistory.AutoSize = true;
            this.btnHistory.BackgroundImage = global::TBPP14200.Properties.Resources.btnHistory_tw;
            this.btnHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHistory.FlatAppearance.BorderSize = 0;
            this.btnHistory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHistory.Location = new System.Drawing.Point(2, 624);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(197, 45);
            this.btnHistory.TabIndex = 22;
            this.btnHistory.Tag = "6";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.PageChange);
            // 
            // btnLang
            // 
            this.btnLang.AutoSize = true;
            this.btnLang.BackgroundImage = global::TBPP14200.Properties.Resources.btnLang_tw;
            this.btnLang.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLang.FlatAppearance.BorderSize = 0;
            this.btnLang.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLang.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.btnLang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLang.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLang.Location = new System.Drawing.Point(2, 574);
            this.btnLang.Name = "btnLang";
            this.btnLang.Size = new System.Drawing.Size(197, 45);
            this.btnLang.TabIndex = 21;
            this.btnLang.Tag = "5";
            this.btnLang.UseVisualStyleBackColor = true;
            this.btnLang.Click += new System.EventHandler(this.PageChange);
            // 
            // btnReset
            // 
            this.btnReset.AutoSize = true;
            this.btnReset.BackgroundImage = global::TBPP14200.Properties.Resources.btnReset_tw;
            this.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnReset.Location = new System.Drawing.Point(2, 250);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(197, 64);
            this.btnReset.TabIndex = 20;
            this.btnReset.Tag = "";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnPause
            // 
            this.btnPause.AutoEllipsis = true;
            this.btnPause.BackgroundImage = global::TBPP14200.Properties.Resources.btnPause_tw;
            this.btnPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPause.FlatAppearance.BorderSize = 0;
            this.btnPause.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPause.Location = new System.Drawing.Point(2, 186);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(197, 64);
            this.btnPause.TabIndex = 19;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackgroundImage = global::TBPP14200.Properties.Resources.btnStart_tw;
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStart.Location = new System.Drawing.Point(2, 122);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(197, 64);
            this.btnStart.TabIndex = 18;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lbRunState
            // 
            this.lbRunState.BackColor = System.Drawing.Color.Yellow;
            this.lbRunState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbRunState.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbRunState.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.lbRunState.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbRunState.Location = new System.Drawing.Point(0, 0);
            this.lbRunState.Name = "lbRunState";
            this.lbRunState.Size = new System.Drawing.Size(200, 63);
            this.lbRunState.TabIndex = 1;
            this.lbRunState.Text = "IDLE";
            this.lbRunState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.pnlTop.Controls.Add(this.pnlPackage);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(2, 2);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1299, 63);
            this.pnlTop.TabIndex = 3;
            // 
            // pnlPackage
            // 
            this.pnlPackage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.pnlPackage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlPackage.Controls.Add(this.lbPackage);
            this.pnlPackage.Controls.Add(this.lbProjectName);
            this.pnlPackage.Controls.Add(this.pictureBox1);
            this.pnlPackage.Controls.Add(this.lbPackageName);
            this.pnlPackage.Controls.Add(this.pnlUser);
            this.pnlPackage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPackage.Location = new System.Drawing.Point(0, 0);
            this.pnlPackage.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.pnlPackage.Name = "pnlPackage";
            this.pnlPackage.Size = new System.Drawing.Size(1299, 63);
            this.pnlPackage.TabIndex = 4;
            // 
            // lbPackage
            // 
            this.lbPackage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbPackage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPackage.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbPackage.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold);
            this.lbPackage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbPackage.Location = new System.Drawing.Point(402, 0);
            this.lbPackage.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lbPackage.Name = "lbPackage";
            this.lbPackage.Size = new System.Drawing.Size(145, 59);
            this.lbPackage.TabIndex = 8;
            this.lbPackage.Text = "生產料號";
            this.lbPackage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbProjectName
            // 
            this.lbProjectName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbProjectName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbProjectName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbProjectName.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbProjectName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbProjectName.Location = new System.Drawing.Point(241, 0);
            this.lbProjectName.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lbProjectName.Name = "lbProjectName";
            this.lbProjectName.Size = new System.Drawing.Size(161, 59);
            this.lbProjectName.TabIndex = 2;
            this.lbProjectName.Text = "專案名稱";
            this.lbProjectName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::TBPP14200.Properties.Resources.provlogo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(241, 59);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // lbPackageName
            // 
            this.lbPackageName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPackageName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbPackageName.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.lbPackageName.Location = new System.Drawing.Point(549, 10);
            this.lbPackageName.Name = "lbPackageName";
            this.lbPackageName.Size = new System.Drawing.Size(516, 40);
            this.lbPackageName.TabIndex = 6;
            this.lbPackageName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlUser
            // 
            this.pnlUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.pnlUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUser.Controls.Add(this.btnChangeUser);
            this.pnlUser.Controls.Add(this.lbUserType);
            this.pnlUser.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlUser.Location = new System.Drawing.Point(1068, 0);
            this.pnlUser.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(227, 59);
            this.pnlUser.TabIndex = 5;
            // 
            // btnChangeUser
            // 
            this.btnChangeUser.BackgroundImage = global::TBPP14200.Properties.Resources.btnChangeUser_tw;
            this.btnChangeUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChangeUser.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnChangeUser.Location = new System.Drawing.Point(2, 1);
            this.btnChangeUser.Name = "btnChangeUser";
            this.btnChangeUser.Size = new System.Drawing.Size(55, 55);
            this.btnChangeUser.TabIndex = 3;
            this.btnChangeUser.UseVisualStyleBackColor = true;
            this.btnChangeUser.Click += new System.EventHandler(this.btnChangeUser_Click);
            // 
            // lbUserType
            // 
            this.lbUserType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbUserType.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbUserType.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.lbUserType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbUserType.Location = new System.Drawing.Point(58, 0);
            this.lbUserType.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lbUserType.Name = "lbUserType";
            this.lbUserType.Size = new System.Drawing.Size(167, 57);
            this.lbUserType.TabIndex = 2;
            this.lbUserType.Text = "Not Login";
            this.lbUserType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlAlarmGrid
            // 
            this.pnlAlarmGrid.Controls.Add(this.lvArmGrid);
            this.pnlAlarmGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAlarmGrid.Location = new System.Drawing.Point(2, 896);
            this.pnlAlarmGrid.Name = "pnlAlarmGrid";
            this.pnlAlarmGrid.Size = new System.Drawing.Size(1299, 90);
            this.pnlAlarmGrid.TabIndex = 7;
            // 
            // lvArmGrid
            // 
            this.lvArmGrid.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvArmGrid.BackColor = System.Drawing.Color.White;
            this.lvArmGrid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lvArmGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvArmGrid.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.lvArmGrid.FullRowSelect = true;
            this.lvArmGrid.GridLines = true;
            this.lvArmGrid.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvArmGrid.HideSelection = false;
            this.lvArmGrid.Location = new System.Drawing.Point(0, 0);
            this.lvArmGrid.MultiSelect = false;
            this.lvArmGrid.Name = "lvArmGrid";
            this.lvArmGrid.Size = new System.Drawing.Size(1299, 90);
            this.lvArmGrid.TabIndex = 0;
            this.lvArmGrid.UseCompatibleStateImageBehavior = false;
            this.lvArmGrid.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 5;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "日期時間";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 250;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "模組/元件";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "錯誤碼";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "錯誤說明";
            this.columnHeader5.Width = 730;
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.tabMachineState);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(2, 65);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(1299, 831);
            this.pnlContainer.TabIndex = 8;
            // 
            // tabMachineState
            // 
            this.tabMachineState.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabMachineState.Controls.Add(this.tpProductionState);
            this.tabMachineState.Controls.Add(this.tpKernalData);
            this.tabMachineState.Controls.Add(this.tpMSS);
            this.tabMachineState.Controls.Add(this.tpMainFlow);
            this.tabMachineState.Controls.Add(this.tpCommunication);
            this.tabMachineState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMachineState.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.tabMachineState.ItemSize = new System.Drawing.Size(220, 40);
            this.tabMachineState.Location = new System.Drawing.Point(0, 0);
            this.tabMachineState.Name = "tabMachineState";
            this.tabMachineState.SelectedIndex = 0;
            this.tabMachineState.Size = new System.Drawing.Size(1299, 831);
            this.tabMachineState.TabIndex = 0;
            // 
            // tpProductionState
            // 
            this.tpProductionState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.tpProductionState.Controls.Add(this.numLabel2);
            this.tpProductionState.Controls.Add(this.numLabel1);
            this.tpProductionState.Controls.Add(this.tv_HDT_TR);
            this.tpProductionState.Controls.Add(this.label5);
            this.tpProductionState.Controls.Add(this.panel18);
            this.tpProductionState.Controls.Add(this.tv_LeftTray);
            this.tpProductionState.Controls.Add(this.panel16);
            this.tpProductionState.Controls.Add(this.label36);
            this.tpProductionState.Controls.Add(this.tv_RightTray);
            this.tpProductionState.Controls.Add(this.label37);
            this.tpProductionState.Controls.Add(this.lb_UPH);
            this.tpProductionState.Controls.Add(this.lb_BibCount);
            this.tpProductionState.Controls.Add(this.label10);
            this.tpProductionState.Controls.Add(this.lb_LoadCompletedQty);
            this.tpProductionState.Controls.Add(this.lb_LoadProcessQty);
            this.tpProductionState.Controls.Add(this.button2);
            this.tpProductionState.Controls.Add(this.lb_LoadMissingQty);
            this.tpProductionState.Controls.Add(this.button1);
            this.tpProductionState.Controls.Add(this.panel22);
            this.tpProductionState.Controls.Add(this.lb_UnloadProcessQty);
            this.tpProductionState.Controls.Add(this.panel20);
            this.tpProductionState.Controls.Add(this.panel5);
            this.tpProductionState.Controls.Add(this.panel1);
            this.tpProductionState.Controls.Add(this.panel2);
            this.tpProductionState.Controls.Add(this.panel7);
            this.tpProductionState.Controls.Add(this.panel3);
            this.tpProductionState.Controls.Add(this.pnl異常通知);
            this.tpProductionState.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.tpProductionState.Location = new System.Drawing.Point(4, 44);
            this.tpProductionState.Name = "tpProductionState";
            this.tpProductionState.Padding = new System.Windows.Forms.Padding(3);
            this.tpProductionState.Size = new System.Drawing.Size(1291, 783);
            this.tpProductionState.TabIndex = 0;
            this.tpProductionState.Text = "生 產 狀 態";
            // 
            // lb_UPH
            // 
            this.lb_UPH.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lb_UPH.Caption = "UPH";
            this.lb_UPH.CaptionColor = System.Drawing.Color.Black;
            this.lb_UPH.CaptionFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.lb_UPH.CaptionWidth = 150;
            this.lb_UPH.CheckLimit = false;
            this.lb_UPH.Location = new System.Drawing.Point(509, 705);
            this.lb_UPH.LowerLimit = 0D;
            this.lb_UPH.Name = "lb_UPH";
            this.lb_UPH.OverLimitColor = System.Drawing.Color.Red;
            this.lb_UPH.Size = new System.Drawing.Size(490, 30);
            this.lb_UPH.TabIndex = 121;
            this.lb_UPH.Unit = "K/H";
            this.lb_UPH.UnitFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.lb_UPH.UnitWidth = 50;
            this.lb_UPH.UpperLimit = 0D;
            this.lb_UPH.Value = "0";
            this.lb_UPH.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.lb_UPH.ValueFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            // 
            // lb_BibCount
            // 
            this.lb_BibCount.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lb_BibCount.Caption = "Board Count";
            this.lb_BibCount.CaptionColor = System.Drawing.Color.Black;
            this.lb_BibCount.CaptionFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.lb_BibCount.CaptionWidth = 150;
            this.lb_BibCount.CheckLimit = false;
            this.lb_BibCount.Location = new System.Drawing.Point(13, 705);
            this.lb_BibCount.LowerLimit = 0D;
            this.lb_BibCount.Name = "lb_BibCount";
            this.lb_BibCount.OverLimitColor = System.Drawing.Color.Red;
            this.lb_BibCount.Size = new System.Drawing.Size(490, 30);
            this.lb_BibCount.TabIndex = 120;
            this.lb_BibCount.Unit = "pcs";
            this.lb_BibCount.UnitFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.lb_BibCount.UnitWidth = 50;
            this.lb_BibCount.UpperLimit = 0D;
            this.lb_BibCount.Value = "0";
            this.lb_BibCount.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.lb_BibCount.ValueFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label10.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.Green;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(13, 579);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(986, 30);
            this.label10.TabIndex = 119;
            this.label10.Text = "Production Information";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel16
            // 
            this.panel16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel16.Controls.Add(this.label4);
            this.panel16.Controls.Add(this.tv_RightLoadUnload);
            this.panel16.Location = new System.Drawing.Point(273, 379);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(258, 196);
            this.panel16.TabIndex = 112;
            // 
            // tv_RightLoadUnload
            // 
            this.tv_RightLoadUnload.BinColorBuf = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("tv_RightLoadUnload.BinColorBuf")));
            this.tv_RightLoadUnload.CellColorGradient = 0;
            this.tv_RightLoadUnload.ColorCount = 27;
            this.tv_RightLoadUnload.DataVCL = null;
            this.tv_RightLoadUnload.IsDoubleBufferdOn = true;
            this.tv_RightLoadUnload.Location = new System.Drawing.Point(3, 30);
            this.tv_RightLoadUnload.Name = "tv_RightLoadUnload";
            this.tv_RightLoadUnload.ShowText = false;
            this.tv_RightLoadUnload.Size = new System.Drawing.Size(246, 159);
            this.tv_RightLoadUnload.UseSelectBesideBinArray = false;
            // 
            // lb_LoadCompletedQty
            // 
            this.lb_LoadCompletedQty.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lb_LoadCompletedQty.Caption = "Load Completed Qty";
            this.lb_LoadCompletedQty.CaptionColor = System.Drawing.Color.Black;
            this.lb_LoadCompletedQty.CaptionFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.lb_LoadCompletedQty.CaptionWidth = 220;
            this.lb_LoadCompletedQty.CheckLimit = false;
            this.lb_LoadCompletedQty.Location = new System.Drawing.Point(13, 643);
            this.lb_LoadCompletedQty.LowerLimit = 0D;
            this.lb_LoadCompletedQty.Name = "lb_LoadCompletedQty";
            this.lb_LoadCompletedQty.OverLimitColor = System.Drawing.Color.Red;
            this.lb_LoadCompletedQty.Size = new System.Drawing.Size(490, 30);
            this.lb_LoadCompletedQty.TabIndex = 115;
            this.lb_LoadCompletedQty.Unit = "pcs";
            this.lb_LoadCompletedQty.UnitFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.lb_LoadCompletedQty.UnitWidth = 50;
            this.lb_LoadCompletedQty.UpperLimit = 0D;
            this.lb_LoadCompletedQty.Value = "0";
            this.lb_LoadCompletedQty.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.lb_LoadCompletedQty.ValueFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            // 
            // panel18
            // 
            this.panel18.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel18.Controls.Add(this.label3);
            this.panel18.Controls.Add(this.tv_LeftLoadUnload);
            this.panel18.Location = new System.Drawing.Point(8, 379);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(253, 196);
            this.panel18.TabIndex = 111;
            // 
            // tv_LeftLoadUnload
            // 
            this.tv_LeftLoadUnload.BinColorBuf = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("tv_LeftLoadUnload.BinColorBuf")));
            this.tv_LeftLoadUnload.CellColorGradient = 0;
            this.tv_LeftLoadUnload.ColorCount = 27;
            this.tv_LeftLoadUnload.DataVCL = null;
            this.tv_LeftLoadUnload.IsDoubleBufferdOn = true;
            this.tv_LeftLoadUnload.Location = new System.Drawing.Point(3, 30);
            this.tv_LeftLoadUnload.Name = "tv_LeftLoadUnload";
            this.tv_LeftLoadUnload.ShowText = false;
            this.tv_LeftLoadUnload.Size = new System.Drawing.Size(244, 160);
            this.tv_LeftLoadUnload.UseSelectBesideBinArray = false;
            // 
            // lb_LoadProcessQty
            // 
            this.lb_LoadProcessQty.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lb_LoadProcessQty.Caption = "Load Process Qty";
            this.lb_LoadProcessQty.CaptionColor = System.Drawing.Color.Black;
            this.lb_LoadProcessQty.CaptionFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.lb_LoadProcessQty.CaptionWidth = 220;
            this.lb_LoadProcessQty.CheckLimit = false;
            this.lb_LoadProcessQty.Location = new System.Drawing.Point(13, 612);
            this.lb_LoadProcessQty.LowerLimit = 0D;
            this.lb_LoadProcessQty.Name = "lb_LoadProcessQty";
            this.lb_LoadProcessQty.OverLimitColor = System.Drawing.Color.Red;
            this.lb_LoadProcessQty.Size = new System.Drawing.Size(490, 30);
            this.lb_LoadProcessQty.TabIndex = 113;
            this.lb_LoadProcessQty.Unit = "pcs";
            this.lb_LoadProcessQty.UnitFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.lb_LoadProcessQty.UnitWidth = 50;
            this.lb_LoadProcessQty.UpperLimit = 0D;
            this.lb_LoadProcessQty.Value = "0";
            this.lb_LoadProcessQty.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.lb_LoadProcessQty.ValueFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 345);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(251, 34);
            this.button2.TabIndex = 110;
            this.button2.Text = "Reject Left Borad";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // lb_LoadMissingQty
            // 
            this.lb_LoadMissingQty.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lb_LoadMissingQty.Caption = "Load Missing Qty";
            this.lb_LoadMissingQty.CaptionColor = System.Drawing.Color.Black;
            this.lb_LoadMissingQty.CaptionFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.lb_LoadMissingQty.CaptionWidth = 220;
            this.lb_LoadMissingQty.CheckLimit = true;
            this.lb_LoadMissingQty.Location = new System.Drawing.Point(13, 674);
            this.lb_LoadMissingQty.LowerLimit = 0D;
            this.lb_LoadMissingQty.Name = "lb_LoadMissingQty";
            this.lb_LoadMissingQty.OverLimitColor = System.Drawing.Color.Red;
            this.lb_LoadMissingQty.Size = new System.Drawing.Size(490, 30);
            this.lb_LoadMissingQty.TabIndex = 116;
            this.lb_LoadMissingQty.Unit = "pcs";
            this.lb_LoadMissingQty.UnitFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.lb_LoadMissingQty.UnitWidth = 50;
            this.lb_LoadMissingQty.UpperLimit = 0D;
            this.lb_LoadMissingQty.Value = "0";
            this.lb_LoadMissingQty.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.lb_LoadMissingQty.ValueFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(278, 345);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(251, 34);
            this.button1.TabIndex = 109;
            this.button1.Text = "Reject Right Board";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // panel22
            // 
            this.panel22.BackColor = System.Drawing.Color.Black;
            this.panel22.Controls.Add(this.panel28);
            this.panel22.Controls.Add(this.panel27);
            this.panel22.Controls.Add(this.panel26);
            this.panel22.Controls.Add(this.panel25);
            this.panel22.Controls.Add(this.panel24);
            this.panel22.Controls.Add(this.panel23);
            this.panel22.Location = new System.Drawing.Point(6, 13);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(994, 36);
            this.panel22.TabIndex = 108;
            // 
            // panel28
            // 
            this.panel28.Controls.Add(this.lb_RunMode);
            this.panel28.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel28.Location = new System.Drawing.Point(794, 0);
            this.panel28.Name = "panel28";
            this.panel28.Size = new System.Drawing.Size(202, 36);
            this.panel28.TabIndex = 5;
            // 
            // lb_RunMode
            // 
            this.lb_RunMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_RunMode.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_RunMode.ForeColor = System.Drawing.Color.Cyan;
            this.lb_RunMode.Location = new System.Drawing.Point(0, 0);
            this.lb_RunMode.Name = "lb_RunMode";
            this.lb_RunMode.Size = new System.Drawing.Size(202, 36);
            this.lb_RunMode.TabIndex = 1;
            this.lb_RunMode.Text = "Mode";
            this.lb_RunMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_RunMode.Click += new System.EventHandler(this.lb_RunMode_Click);
            // 
            // panel27
            // 
            this.panel27.Controls.Add(this.lb_LotID);
            this.panel27.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel27.Location = new System.Drawing.Point(533, 0);
            this.panel27.Name = "panel27";
            this.panel27.Size = new System.Drawing.Size(261, 36);
            this.panel27.TabIndex = 4;
            // 
            // lb_LotID
            // 
            this.lb_LotID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_LotID.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_LotID.ForeColor = System.Drawing.Color.Yellow;
            this.lb_LotID.Location = new System.Drawing.Point(0, 0);
            this.lb_LotID.Name = "lb_LotID";
            this.lb_LotID.Size = new System.Drawing.Size(261, 36);
            this.lb_LotID.TabIndex = 1;
            this.lb_LotID.Text = "Lot ID";
            this.lb_LotID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel26
            // 
            this.panel26.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel26.Location = new System.Drawing.Point(522, 0);
            this.panel26.Name = "panel26";
            this.panel26.Size = new System.Drawing.Size(11, 36);
            this.panel26.TabIndex = 3;
            // 
            // panel25
            // 
            this.panel25.Controls.Add(this.lb_RBoardID);
            this.panel25.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel25.Location = new System.Drawing.Point(267, 0);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(255, 36);
            this.panel25.TabIndex = 2;
            // 
            // lb_RBoardID
            // 
            this.lb_RBoardID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_RBoardID.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_RBoardID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lb_RBoardID.Location = new System.Drawing.Point(0, 0);
            this.lb_RBoardID.Name = "lb_RBoardID";
            this.lb_RBoardID.Size = new System.Drawing.Size(255, 36);
            this.lb_RBoardID.TabIndex = 1;
            this.lb_RBoardID.Text = "Right Board ID";
            this.lb_RBoardID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel24
            // 
            this.panel24.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel24.Location = new System.Drawing.Point(257, 0);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(10, 36);
            this.panel24.TabIndex = 1;
            // 
            // panel23
            // 
            this.panel23.Controls.Add(this.lb_LBoardID);
            this.panel23.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel23.Location = new System.Drawing.Point(0, 0);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(257, 36);
            this.panel23.TabIndex = 0;
            // 
            // lb_LBoardID
            // 
            this.lb_LBoardID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_LBoardID.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_LBoardID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lb_LBoardID.Location = new System.Drawing.Point(0, 0);
            this.lb_LBoardID.Name = "lb_LBoardID";
            this.lb_LBoardID.Size = new System.Drawing.Size(257, 36);
            this.lb_LBoardID.TabIndex = 0;
            this.lb_LBoardID.Text = "Left Board ID";
            this.lb_LBoardID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_UnloadProcessQty
            // 
            this.lb_UnloadProcessQty.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lb_UnloadProcessQty.Caption = "Unload Process Qty";
            this.lb_UnloadProcessQty.CaptionColor = System.Drawing.Color.Black;
            this.lb_UnloadProcessQty.CaptionFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.lb_UnloadProcessQty.CaptionWidth = 220;
            this.lb_UnloadProcessQty.CheckLimit = false;
            this.lb_UnloadProcessQty.Location = new System.Drawing.Point(509, 612);
            this.lb_UnloadProcessQty.LowerLimit = 0D;
            this.lb_UnloadProcessQty.Name = "lb_UnloadProcessQty";
            this.lb_UnloadProcessQty.OverLimitColor = System.Drawing.Color.Red;
            this.lb_UnloadProcessQty.Size = new System.Drawing.Size(490, 30);
            this.lb_UnloadProcessQty.TabIndex = 114;
            this.lb_UnloadProcessQty.Unit = "pcs";
            this.lb_UnloadProcessQty.UnitFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.lb_UnloadProcessQty.UnitWidth = 50;
            this.lb_UnloadProcessQty.UpperLimit = 0D;
            this.lb_UnloadProcessQty.Value = "0";
            this.lb_UnloadProcessQty.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.lb_UnloadProcessQty.ValueFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            // 
            // panel20
            // 
            this.panel20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel20.Controls.Add(this.label2);
            this.panel20.Controls.Add(this.tv_Transfer);
            this.panel20.Location = new System.Drawing.Point(535, 348);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(202, 227);
            this.panel20.TabIndex = 107;
            // 
            // tv_Transfer
            // 
            this.tv_Transfer.BinColorBuf = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("tv_Transfer.BinColorBuf")));
            this.tv_Transfer.CellColorGradient = 0;
            this.tv_Transfer.ColorCount = 27;
            this.tv_Transfer.DataVCL = null;
            this.tv_Transfer.IsDoubleBufferdOn = true;
            this.tv_Transfer.Location = new System.Drawing.Point(5, 30);
            this.tv_Transfer.Name = "tv_Transfer";
            this.tv_Transfer.ShowText = false;
            this.tv_Transfer.Size = new System.Drawing.Size(189, 189);
            this.tv_Transfer.UseSelectBesideBinArray = false;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.panel9);
            this.panel5.Controls.Add(this.tv_ClamSheel);
            this.panel5.Location = new System.Drawing.Point(743, 346);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(258, 229);
            this.panel5.TabIndex = 105;
            // 
            // panel9
            // 
            this.panel9.BackgroundImage = global::TBPP14200.Properties.Resources.Clamsheel;
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(254, 32);
            this.panel9.TabIndex = 5;
            // 
            // tv_ClamSheel
            // 
            this.tv_ClamSheel.BinColorBuf = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("tv_ClamSheel.BinColorBuf")));
            this.tv_ClamSheel.CellColorGradient = 0;
            this.tv_ClamSheel.ColorCount = 27;
            this.tv_ClamSheel.DataVCL = null;
            this.tv_ClamSheel.IsDoubleBufferdOn = true;
            this.tv_ClamSheel.Location = new System.Drawing.Point(2, 35);
            this.tv_ClamSheel.Name = "tv_ClamSheel";
            this.tv_ClamSheel.ShowText = false;
            this.tv_ClamSheel.Size = new System.Drawing.Size(247, 188);
            this.tv_ClamSheel.UseSelectBesideBinArray = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel12);
            this.panel1.Controls.Add(this.tv_LeftBoard);
            this.panel1.Location = new System.Drawing.Point(6, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 290);
            this.panel1.TabIndex = 103;
            // 
            // panel12
            // 
            this.panel12.BackgroundImage = global::TBPP14200.Properties.Resources.Left_Board;
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(0, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(256, 32);
            this.panel12.TabIndex = 5;
            // 
            // tv_LeftBoard
            // 
            this.tv_LeftBoard.BinColorBuf = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("tv_LeftBoard.BinColorBuf")));
            this.tv_LeftBoard.CellColorGradient = 0;
            this.tv_LeftBoard.ColorCount = 200;
            this.tv_LeftBoard.DataVCL = null;
            this.tv_LeftBoard.IsDoubleBufferdOn = true;
            this.tv_LeftBoard.Location = new System.Drawing.Point(5, 35);
            this.tv_LeftBoard.Name = "tv_LeftBoard";
            this.tv_LeftBoard.ShowText = false;
            this.tv_LeftBoard.Size = new System.Drawing.Size(250, 250);
            this.tv_LeftBoard.UseSelectBesideBinArray = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.panel8);
            this.panel2.Controls.Add(this.tv_RightBoard);
            this.panel2.Location = new System.Drawing.Point(271, 52);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(260, 290);
            this.panel2.TabIndex = 102;
            // 
            // panel8
            // 
            this.panel8.BackgroundImage = global::TBPP14200.Properties.Resources.Right_Board;
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(256, 32);
            this.panel8.TabIndex = 4;
            // 
            // tv_RightBoard
            // 
            this.tv_RightBoard.BinColorBuf = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("tv_RightBoard.BinColorBuf")));
            this.tv_RightBoard.CellColorGradient = 0;
            this.tv_RightBoard.ColorCount = 48;
            this.tv_RightBoard.DataVCL = null;
            this.tv_RightBoard.IsDoubleBufferdOn = true;
            this.tv_RightBoard.Location = new System.Drawing.Point(5, 35);
            this.tv_RightBoard.Name = "tv_RightBoard";
            this.tv_RightBoard.ShowText = false;
            this.tv_RightBoard.Size = new System.Drawing.Size(250, 250);
            this.tv_RightBoard.UseSelectBesideBinArray = false;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.panel6);
            this.panel7.Controls.Add(this.tv_HDT_BottomBoard);
            this.panel7.Controls.Add(this.tv_HDT_TopBoard);
            this.panel7.Location = new System.Drawing.Point(535, 52);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(75, 287);
            this.panel7.TabIndex = 101;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel6.BackgroundImage = global::TBPP14200.Properties.Resources.未命名;
            this.panel6.Location = new System.Drawing.Point(5, 71);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(66, 142);
            this.panel6.TabIndex = 6;
            // 
            // tv_HDT_BottomBoard
            // 
            this.tv_HDT_BottomBoard.BinColorBuf = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("tv_HDT_BottomBoard.BinColorBuf")));
            this.tv_HDT_BottomBoard.CellColorGradient = 0;
            this.tv_HDT_BottomBoard.ColorCount = 10;
            this.tv_HDT_BottomBoard.DataVCL = null;
            this.tv_HDT_BottomBoard.IsDoubleBufferdOn = true;
            this.tv_HDT_BottomBoard.Location = new System.Drawing.Point(5, 215);
            this.tv_HDT_BottomBoard.Name = "tv_HDT_BottomBoard";
            this.tv_HDT_BottomBoard.ShowText = false;
            this.tv_HDT_BottomBoard.Size = new System.Drawing.Size(67, 65);
            this.tv_HDT_BottomBoard.UseSelectBesideBinArray = false;
            // 
            // tv_HDT_TopBoard
            // 
            this.tv_HDT_TopBoard.BinColorBuf = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("tv_HDT_TopBoard.BinColorBuf")));
            this.tv_HDT_TopBoard.CellColorGradient = 0;
            this.tv_HDT_TopBoard.ColorCount = 10;
            this.tv_HDT_TopBoard.DataVCL = null;
            this.tv_HDT_TopBoard.IsDoubleBufferdOn = true;
            this.tv_HDT_TopBoard.Location = new System.Drawing.Point(5, 5);
            this.tv_HDT_TopBoard.Name = "tv_HDT_TopBoard";
            this.tv_HDT_TopBoard.ShowText = false;
            this.tv_HDT_TopBoard.Size = new System.Drawing.Size(67, 65);
            this.tv_HDT_TopBoard.UseSelectBesideBinArray = false;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.panel11);
            this.panel3.Controls.Add(this.panel10);
            this.panel3.Controls.Add(this.tv_RightKit);
            this.panel3.Controls.Add(this.tv_LeftKit);
            this.panel3.Location = new System.Drawing.Point(616, 52);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(175, 290);
            this.panel3.TabIndex = 100;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel11.BackgroundImage = global::TBPP14200.Properties.Resources.Right_Kit_Shuttle;
            this.panel11.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel11.Location = new System.Drawing.Point(140, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(31, 286);
            this.panel11.TabIndex = 4;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel10.BackgroundImage = global::TBPP14200.Properties.Resources.Left_Kit_Shuttle;
            this.panel10.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(30, 286);
            this.panel10.TabIndex = 3;
            // 
            // tv_RightKit
            // 
            this.tv_RightKit.BinColorBuf = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("tv_RightKit.BinColorBuf")));
            this.tv_RightKit.CellColorGradient = 0;
            this.tv_RightKit.ColorCount = 10;
            this.tv_RightKit.DataVCL = null;
            this.tv_RightKit.IsDoubleBufferdOn = true;
            this.tv_RightKit.Location = new System.Drawing.Point(89, 5);
            this.tv_RightKit.Name = "tv_RightKit";
            this.tv_RightKit.ShowText = false;
            this.tv_RightKit.Size = new System.Drawing.Size(49, 280);
            this.tv_RightKit.UseSelectBesideBinArray = false;
            // 
            // tv_LeftKit
            // 
            this.tv_LeftKit.BinColorBuf = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("tv_LeftKit.BinColorBuf")));
            this.tv_LeftKit.CellColorGradient = 0;
            this.tv_LeftKit.ColorCount = 10;
            this.tv_LeftKit.DataVCL = null;
            this.tv_LeftKit.IsDoubleBufferdOn = true;
            this.tv_LeftKit.Location = new System.Drawing.Point(35, 5);
            this.tv_LeftKit.Name = "tv_LeftKit";
            this.tv_LeftKit.ShowText = false;
            this.tv_LeftKit.Size = new System.Drawing.Size(49, 280);
            this.tv_LeftKit.UseSelectBesideBinArray = false;
            // 
            // pnl異常通知
            // 
            this.pnl異常通知.BackColor = System.Drawing.Color.Red;
            this.pnl異常通知.Controls.Add(this.lb異常訊息);
            this.pnl異常通知.Location = new System.Drawing.Point(1091, 3);
            this.pnl異常通知.Name = "pnl異常通知";
            this.pnl異常通知.Size = new System.Drawing.Size(131, 114);
            this.pnl異常通知.TabIndex = 5;
            this.pnl異常通知.Visible = false;
            this.pnl異常通知.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lb異常訊息_MouseDown);
            this.pnl異常通知.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lb異常訊息_MouseMove);
            this.pnl異常通知.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lb異常訊息_MouseUp);
            // 
            // lb異常訊息
            // 
            this.lb異常訊息.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb異常訊息.Font = new System.Drawing.Font("微軟正黑體", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb異常訊息.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lb異常訊息.Location = new System.Drawing.Point(0, 0);
            this.lb異常訊息.Name = "lb異常訊息";
            this.lb異常訊息.Size = new System.Drawing.Size(131, 114);
            this.lb異常訊息.TabIndex = 0;
            this.lb異常訊息.Text = "緊停異常通知\r\nEMG Stop";
            this.lb異常訊息.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tpKernalData
            // 
            this.tpKernalData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.tpKernalData.Controls.Add(this.lb嫁動率);
            this.tpKernalData.Controls.Add(this.TB_授權有效性);
            this.tpKernalData.Controls.Add(this.TB_授權模式);
            this.tpKernalData.Controls.Add(this.btnResetMaintenance);
            this.tpKernalData.Controls.Add(this.btnResetManual);
            this.tpKernalData.Controls.Add(this.btnResetHome);
            this.tpKernalData.Controls.Add(this.btnResetIDLE);
            this.tpKernalData.Controls.Add(this.label1);
            this.tpKernalData.Controls.Add(this.lbMaintenanceTM);
            this.tpKernalData.Controls.Add(this.lbManualTM);
            this.tpKernalData.Controls.Add(this.lbHomeTM);
            this.tpKernalData.Controls.Add(this.lbIdleTM);
            this.tpKernalData.Controls.Add(this.lbPauseTM);
            this.tpKernalData.Controls.Add(this.lbRunTM);
            this.tpKernalData.Controls.Add(this.lbOperationEndTM);
            this.tpKernalData.Controls.Add(this.lbOperationStartTM);
            this.tpKernalData.Controls.Add(this.lbScanCNT);
            this.tpKernalData.Controls.Add(this.lbScanTM);
            this.tpKernalData.Font = new System.Drawing.Font("微軟正黑體", 15.75F);
            this.tpKernalData.Location = new System.Drawing.Point(4, 44);
            this.tpKernalData.Name = "tpKernalData";
            this.tpKernalData.Padding = new System.Windows.Forms.Padding(3);
            this.tpKernalData.Size = new System.Drawing.Size(1150, 536);
            this.tpKernalData.TabIndex = 1;
            this.tpKernalData.Text = "核心資料/生產數據";
            // 
            // lb嫁動率
            // 
            this.lb嫁動率.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lb嫁動率.Caption = "當日嫁動率";
            this.lb嫁動率.CaptionColor = System.Drawing.Color.Black;
            this.lb嫁動率.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb嫁動率.CaptionWidth = 150;
            this.lb嫁動率.CheckLimit = false;
            this.lb嫁動率.Location = new System.Drawing.Point(20, 423);
            this.lb嫁動率.LowerLimit = 0D;
            this.lb嫁動率.Name = "lb嫁動率";
            this.lb嫁動率.OverLimitColor = System.Drawing.Color.Red;
            this.lb嫁動率.Size = new System.Drawing.Size(370, 30);
            this.lb嫁動率.TabIndex = 75;
            this.lb嫁動率.Unit = "%";
            this.lb嫁動率.UnitFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.lb嫁動率.UnitWidth = 57;
            this.lb嫁動率.UpperLimit = 0D;
            this.lb嫁動率.Value = "0";
            this.lb嫁動率.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.lb嫁動率.ValueFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            // 
            // TB_授權有效性
            // 
            this.TB_授權有效性.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.TB_授權有效性.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.TB_授權有效性.Caption = "授權有效性";
            this.TB_授權有效性.CaptionColor = System.Drawing.Color.Black;
            this.TB_授權有效性.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TB_授權有效性.CaptionWidth = 150;
            this.TB_授權有效性.Location = new System.Drawing.Point(20, 392);
            this.TB_授權有效性.Name = "TB_授權有效性";
            this.TB_授權有效性.Size = new System.Drawing.Size(370, 30);
            this.TB_授權有效性.TabIndex = 74;
            this.TB_授權有效性.Value = "";
            this.TB_授權有效性.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.TB_授權有效性.ValueFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TB_授權有效性.Visible = false;
            // 
            // TB_授權模式
            // 
            this.TB_授權模式.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.TB_授權模式.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.TB_授權模式.Caption = "授權模式";
            this.TB_授權模式.CaptionColor = System.Drawing.Color.Black;
            this.TB_授權模式.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TB_授權模式.CaptionWidth = 150;
            this.TB_授權模式.Location = new System.Drawing.Point(20, 361);
            this.TB_授權模式.Name = "TB_授權模式";
            this.TB_授權模式.Size = new System.Drawing.Size(370, 30);
            this.TB_授權模式.TabIndex = 73;
            this.TB_授權模式.Value = "";
            this.TB_授權模式.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.TB_授權模式.ValueFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TB_授權模式.Visible = false;
            // 
            // btnResetMaintenance
            // 
            this.btnResetMaintenance.Location = new System.Drawing.Point(393, 144);
            this.btnResetMaintenance.Name = "btnResetMaintenance";
            this.btnResetMaintenance.Size = new System.Drawing.Size(36, 30);
            this.btnResetMaintenance.TabIndex = 72;
            this.btnResetMaintenance.Text = "Z";
            this.btnResetMaintenance.UseVisualStyleBackColor = true;
            this.btnResetMaintenance.Click += new System.EventHandler(this.btnResetMaintenance_Click);
            // 
            // btnResetManual
            // 
            this.btnResetManual.Location = new System.Drawing.Point(393, 113);
            this.btnResetManual.Name = "btnResetManual";
            this.btnResetManual.Size = new System.Drawing.Size(36, 30);
            this.btnResetManual.TabIndex = 72;
            this.btnResetManual.Text = "Z";
            this.btnResetManual.UseVisualStyleBackColor = true;
            this.btnResetManual.Click += new System.EventHandler(this.btnResetManual_Click);
            // 
            // btnResetHome
            // 
            this.btnResetHome.Location = new System.Drawing.Point(393, 82);
            this.btnResetHome.Name = "btnResetHome";
            this.btnResetHome.Size = new System.Drawing.Size(36, 30);
            this.btnResetHome.TabIndex = 71;
            this.btnResetHome.Text = "Z";
            this.btnResetHome.UseVisualStyleBackColor = true;
            this.btnResetHome.Click += new System.EventHandler(this.btnResetHome_Click);
            // 
            // btnResetIDLE
            // 
            this.btnResetIDLE.Location = new System.Drawing.Point(393, 51);
            this.btnResetIDLE.Name = "btnResetIDLE";
            this.btnResetIDLE.Size = new System.Drawing.Size(36, 30);
            this.btnResetIDLE.TabIndex = 70;
            this.btnResetIDLE.Text = "Z";
            this.btnResetIDLE.UseVisualStyleBackColor = true;
            this.btnResetIDLE.Click += new System.EventHandler(this.btnResetIDLE_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(20, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(370, 30);
            this.label1.TabIndex = 44;
            this.label1.Text = "系統資訊";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbMaintenanceTM
            // 
            this.lbMaintenanceTM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbMaintenanceTM.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbMaintenanceTM.Caption = "維修累計時間";
            this.lbMaintenanceTM.CaptionColor = System.Drawing.Color.Black;
            this.lbMaintenanceTM.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbMaintenanceTM.CaptionWidth = 150;
            this.lbMaintenanceTM.Location = new System.Drawing.Point(20, 144);
            this.lbMaintenanceTM.Name = "lbMaintenanceTM";
            this.lbMaintenanceTM.Size = new System.Drawing.Size(370, 30);
            this.lbMaintenanceTM.TabIndex = 69;
            this.lbMaintenanceTM.Value = "";
            this.lbMaintenanceTM.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.lbMaintenanceTM.ValueFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            // 
            // lbManualTM
            // 
            this.lbManualTM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbManualTM.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbManualTM.Caption = "手動操作累計時間";
            this.lbManualTM.CaptionColor = System.Drawing.Color.Black;
            this.lbManualTM.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbManualTM.CaptionWidth = 150;
            this.lbManualTM.Location = new System.Drawing.Point(20, 113);
            this.lbManualTM.Name = "lbManualTM";
            this.lbManualTM.Size = new System.Drawing.Size(370, 30);
            this.lbManualTM.TabIndex = 69;
            this.lbManualTM.Value = "";
            this.lbManualTM.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.lbManualTM.ValueFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            // 
            // lbHomeTM
            // 
            this.lbHomeTM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbHomeTM.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbHomeTM.Caption = "歸零累計時間";
            this.lbHomeTM.CaptionColor = System.Drawing.Color.Black;
            this.lbHomeTM.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbHomeTM.CaptionWidth = 150;
            this.lbHomeTM.Location = new System.Drawing.Point(20, 82);
            this.lbHomeTM.Name = "lbHomeTM";
            this.lbHomeTM.Size = new System.Drawing.Size(370, 30);
            this.lbHomeTM.TabIndex = 68;
            this.lbHomeTM.Value = "";
            this.lbHomeTM.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.lbHomeTM.ValueFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            // 
            // lbIdleTM
            // 
            this.lbIdleTM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbIdleTM.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbIdleTM.Caption = "IDLE累計時間";
            this.lbIdleTM.CaptionColor = System.Drawing.Color.Black;
            this.lbIdleTM.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbIdleTM.CaptionWidth = 150;
            this.lbIdleTM.Location = new System.Drawing.Point(20, 51);
            this.lbIdleTM.Name = "lbIdleTM";
            this.lbIdleTM.Size = new System.Drawing.Size(370, 30);
            this.lbIdleTM.TabIndex = 67;
            this.lbIdleTM.Value = "";
            this.lbIdleTM.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.lbIdleTM.ValueFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            // 
            // lbPauseTM
            // 
            this.lbPauseTM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbPauseTM.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbPauseTM.Caption = "機台暫停時間";
            this.lbPauseTM.CaptionColor = System.Drawing.Color.Black;
            this.lbPauseTM.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbPauseTM.CaptionWidth = 150;
            this.lbPauseTM.Location = new System.Drawing.Point(20, 268);
            this.lbPauseTM.Name = "lbPauseTM";
            this.lbPauseTM.Size = new System.Drawing.Size(370, 30);
            this.lbPauseTM.TabIndex = 64;
            this.lbPauseTM.Value = "";
            this.lbPauseTM.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.lbPauseTM.ValueFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            // 
            // lbRunTM
            // 
            this.lbRunTM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbRunTM.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbRunTM.Caption = "機台運行時間";
            this.lbRunTM.CaptionColor = System.Drawing.Color.Black;
            this.lbRunTM.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbRunTM.CaptionWidth = 150;
            this.lbRunTM.Location = new System.Drawing.Point(20, 237);
            this.lbRunTM.Name = "lbRunTM";
            this.lbRunTM.Size = new System.Drawing.Size(370, 30);
            this.lbRunTM.TabIndex = 63;
            this.lbRunTM.Value = "";
            this.lbRunTM.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.lbRunTM.ValueFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            // 
            // lbOperationEndTM
            // 
            this.lbOperationEndTM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbOperationEndTM.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbOperationEndTM.Caption = "作業結束時間";
            this.lbOperationEndTM.CaptionColor = System.Drawing.Color.Black;
            this.lbOperationEndTM.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbOperationEndTM.CaptionWidth = 150;
            this.lbOperationEndTM.Location = new System.Drawing.Point(20, 206);
            this.lbOperationEndTM.Name = "lbOperationEndTM";
            this.lbOperationEndTM.Size = new System.Drawing.Size(370, 30);
            this.lbOperationEndTM.TabIndex = 62;
            this.lbOperationEndTM.Value = "";
            this.lbOperationEndTM.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.lbOperationEndTM.ValueFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            // 
            // lbOperationStartTM
            // 
            this.lbOperationStartTM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbOperationStartTM.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbOperationStartTM.Caption = "開始作業時間";
            this.lbOperationStartTM.CaptionColor = System.Drawing.Color.Black;
            this.lbOperationStartTM.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbOperationStartTM.CaptionWidth = 150;
            this.lbOperationStartTM.Location = new System.Drawing.Point(20, 175);
            this.lbOperationStartTM.Name = "lbOperationStartTM";
            this.lbOperationStartTM.Size = new System.Drawing.Size(370, 30);
            this.lbOperationStartTM.TabIndex = 61;
            this.lbOperationStartTM.Value = "";
            this.lbOperationStartTM.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.lbOperationStartTM.ValueFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            // 
            // lbScanCNT
            // 
            this.lbScanCNT.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbScanCNT.Caption = "Scan Count";
            this.lbScanCNT.CaptionColor = System.Drawing.Color.Black;
            this.lbScanCNT.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbScanCNT.CaptionWidth = 150;
            this.lbScanCNT.CheckLimit = false;
            this.lbScanCNT.Location = new System.Drawing.Point(20, 330);
            this.lbScanCNT.LowerLimit = 0D;
            this.lbScanCNT.Name = "lbScanCNT";
            this.lbScanCNT.OverLimitColor = System.Drawing.Color.Red;
            this.lbScanCNT.Size = new System.Drawing.Size(370, 30);
            this.lbScanCNT.TabIndex = 66;
            this.lbScanCNT.Unit = "t/s";
            this.lbScanCNT.UnitFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.lbScanCNT.UnitWidth = 57;
            this.lbScanCNT.UpperLimit = 0D;
            this.lbScanCNT.Value = "0";
            this.lbScanCNT.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.lbScanCNT.ValueFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            // 
            // lbScanTM
            // 
            this.lbScanTM.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lbScanTM.Caption = "Scan Time";
            this.lbScanTM.CaptionColor = System.Drawing.Color.Black;
            this.lbScanTM.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbScanTM.CaptionWidth = 150;
            this.lbScanTM.CheckLimit = false;
            this.lbScanTM.Location = new System.Drawing.Point(20, 299);
            this.lbScanTM.LowerLimit = 0D;
            this.lbScanTM.Name = "lbScanTM";
            this.lbScanTM.OverLimitColor = System.Drawing.Color.Red;
            this.lbScanTM.Size = new System.Drawing.Size(370, 30);
            this.lbScanTM.TabIndex = 65;
            this.lbScanTM.Unit = "ms";
            this.lbScanTM.UnitFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.lbScanTM.UnitWidth = 57;
            this.lbScanTM.UpperLimit = 0D;
            this.lbScanTM.Value = "0";
            this.lbScanTM.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.lbScanTM.ValueFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            // 
            // tpMSS
            // 
            this.tpMSS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.tpMSS.Location = new System.Drawing.Point(4, 44);
            this.tpMSS.Name = "tpMSS";
            this.tpMSS.Padding = new System.Windows.Forms.Padding(3);
            this.tpMSS.Size = new System.Drawing.Size(1150, 536);
            this.tpMSS.TabIndex = 2;
            this.tpMSS.Text = "MSS";
            // 
            // tpMainFlow
            // 
            this.tpMainFlow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.tpMainFlow.Location = new System.Drawing.Point(4, 44);
            this.tpMainFlow.Name = "tpMainFlow";
            this.tpMainFlow.Padding = new System.Windows.Forms.Padding(3);
            this.tpMainFlow.Size = new System.Drawing.Size(1150, 536);
            this.tpMainFlow.TabIndex = 3;
            this.tpMainFlow.Text = "主控流程";
            // 
            // tpCommunication
            // 
            this.tpCommunication.Location = new System.Drawing.Point(4, 44);
            this.tpCommunication.Name = "tpCommunication";
            this.tpCommunication.Size = new System.Drawing.Size(1150, 536);
            this.tpCommunication.TabIndex = 4;
            this.tpCommunication.Text = "通訊相關";
            this.tpCommunication.UseVisualStyleBackColor = true;
            // 
            // tmUIRefresh
            // 
            this.tmUIRefresh.Tick += new System.EventHandler(this.tmUIRefresh_Tick);
            // 
            // tv_LeftTray
            // 
            this.tv_LeftTray.BinColorBuf = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("tv_LeftTray.BinColorBuf")));
            this.tv_LeftTray.CellColorGradient = 0;
            this.tv_LeftTray.ColorCount = 10;
            this.tv_LeftTray.DataVCL = null;
            this.tv_LeftTray.IsDoubleBufferdOn = true;
            this.tv_LeftTray.Location = new System.Drawing.Point(794, 144);
            this.tv_LeftTray.Name = "tv_LeftTray";
            this.tv_LeftTray.ShowText = false;
            this.tv_LeftTray.Size = new System.Drawing.Size(100, 195);
            this.tv_LeftTray.UseSelectBesideBinArray = false;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.Blue;
            this.label36.ForeColor = System.Drawing.SystemColors.Control;
            this.label36.Location = new System.Drawing.Point(794, 118);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(100, 25);
            this.label36.TabIndex = 233;
            this.label36.Text = "L-Tray";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tv_RightTray
            // 
            this.tv_RightTray.BinColorBuf = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("tv_RightTray.BinColorBuf")));
            this.tv_RightTray.CellColorGradient = 0;
            this.tv_RightTray.ColorCount = 10;
            this.tv_RightTray.DataVCL = null;
            this.tv_RightTray.IsDoubleBufferdOn = true;
            this.tv_RightTray.Location = new System.Drawing.Point(900, 144);
            this.tv_RightTray.Name = "tv_RightTray";
            this.tv_RightTray.ShowText = false;
            this.tv_RightTray.Size = new System.Drawing.Size(100, 195);
            this.tv_RightTray.UseSelectBesideBinArray = false;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.Blue;
            this.label37.ForeColor = System.Drawing.SystemColors.Control;
            this.label37.Location = new System.Drawing.Point(900, 118);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(100, 25);
            this.label37.TabIndex = 232;
            this.label37.Text = "R-Tray";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Blue;
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(4, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 25);
            this.label2.TabIndex = 234;
            this.label2.Text = "Transfer";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Blue;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(3, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(244, 25);
            this.label3.TabIndex = 235;
            this.label3.Text = "Load Port 1";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Blue;
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(2, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(248, 25);
            this.label4.TabIndex = 236;
            this.label4.Text = "Load Port 2";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tv_HDT_TR
            // 
            this.tv_HDT_TR.BinColorBuf = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("tv_HDT_TR.BinColorBuf")));
            this.tv_HDT_TR.CellColorGradient = 0;
            this.tv_HDT_TR.ColorCount = 10;
            this.tv_HDT_TR.DataVCL = null;
            this.tv_HDT_TR.IsDoubleBufferdOn = true;
            this.tv_HDT_TR.Location = new System.Drawing.Point(793, 78);
            this.tv_HDT_TR.Name = "tv_HDT_TR";
            this.tv_HDT_TR.ShowText = false;
            this.tv_HDT_TR.Size = new System.Drawing.Size(206, 38);
            this.tv_HDT_TR.UseSelectBesideBinArray = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Blue;
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(792, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(208, 25);
            this.label5.TabIndex = 236;
            this.label5.Text = "Tray Head";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numLabel1
            // 
            this.numLabel1.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.numLabel1.Caption = "Load Completed Qty";
            this.numLabel1.CaptionColor = System.Drawing.Color.Black;
            this.numLabel1.CaptionFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.numLabel1.CaptionWidth = 220;
            this.numLabel1.CheckLimit = false;
            this.numLabel1.Location = new System.Drawing.Point(509, 643);
            this.numLabel1.LowerLimit = 0D;
            this.numLabel1.Name = "numLabel1";
            this.numLabel1.OverLimitColor = System.Drawing.Color.Red;
            this.numLabel1.Size = new System.Drawing.Size(490, 30);
            this.numLabel1.TabIndex = 238;
            this.numLabel1.Unit = "pcs";
            this.numLabel1.UnitFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.numLabel1.UnitWidth = 50;
            this.numLabel1.UpperLimit = 0D;
            this.numLabel1.Value = "0";
            this.numLabel1.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.numLabel1.ValueFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            // 
            // numLabel2
            // 
            this.numLabel2.BGColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.numLabel2.Caption = "Load Missing Qty";
            this.numLabel2.CaptionColor = System.Drawing.Color.Black;
            this.numLabel2.CaptionFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.numLabel2.CaptionWidth = 220;
            this.numLabel2.CheckLimit = true;
            this.numLabel2.Location = new System.Drawing.Point(509, 674);
            this.numLabel2.LowerLimit = 0D;
            this.numLabel2.Name = "numLabel2";
            this.numLabel2.OverLimitColor = System.Drawing.Color.Red;
            this.numLabel2.Size = new System.Drawing.Size(490, 30);
            this.numLabel2.TabIndex = 239;
            this.numLabel2.Unit = "pcs";
            this.numLabel2.UnitFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.numLabel2.UnitWidth = 50;
            this.numLabel2.UpperLimit = 0D;
            this.numLabel2.Value = "0";
            this.numLabel2.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(195)))), ((int)(((byte)(97)))));
            this.numLabel2.ValueFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            // 
            // MainF1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1503, 988);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.pnlAlarmGrid);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlControl);
            this.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "MainF1";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "主頁";
            this.Load += new System.EventHandler(this.MainF1_Load);
            this.Shown += new System.EventHandler(this.MainF1_Shown);
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            this.LightLayoutPanel.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlPackage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlUser.ResumeLayout(false);
            this.pnlAlarmGrid.ResumeLayout(false);
            this.pnlContainer.ResumeLayout(false);
            this.tabMachineState.ResumeLayout(false);
            this.tpProductionState.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel18.ResumeLayout(false);
            this.panel22.ResumeLayout(false);
            this.panel28.ResumeLayout(false);
            this.panel27.ResumeLayout(false);
            this.panel25.ResumeLayout(false);
            this.panel23.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnl異常通知.ResumeLayout(false);
            this.tpKernalData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlPackage;
        private System.Windows.Forms.Panel pnlUser;
        private System.Windows.Forms.Button btnChangeUser;
        private System.Windows.Forms.Label lbUserType;
        private System.Windows.Forms.Label lbProjectName;
        private System.Windows.Forms.Label lbPackageName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlAlarmGrid;
        public System.Windows.Forms.ListView lvArmGrid;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label lbPackage;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.TabControl tabMachineState;
        private System.Windows.Forms.TabPage tpProductionState;
        private System.Windows.Forms.TabPage tpKernalData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbRunState;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPackage;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnLang;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.Label lbNowTime;
        private System.Windows.Forms.Label lbVersion;
        private KCSDK.NumLabel lbScanCNT;
        private KCSDK.NumLabel lbScanTM;
        private KCSDK.TextLabel lbPauseTM;
        private KCSDK.TextLabel lbRunTM;
        private KCSDK.TextLabel lbOperationEndTM;
        private KCSDK.TextLabel lbOperationStartTM;
        private System.Windows.Forms.Timer tmUIRefresh;
        private System.Windows.Forms.TabPage tpMSS;
        private System.Windows.Forms.Button btnOption;
        private System.Windows.Forms.TabPage tpMainFlow;
        private System.Windows.Forms.Button btnHome;
        private KCSDK.TextLabel lbManualTM;
        private KCSDK.TextLabel lbHomeTM;
        private KCSDK.TextLabel lbIdleTM;
        private System.Windows.Forms.Button btnResetManual;
        private System.Windows.Forms.Button btnResetHome;
        private System.Windows.Forms.Button btnResetIDLE;
        private KCSDK.TextLabel TB_授權有效性;
        private KCSDK.TextLabel TB_授權模式;
        private System.Windows.Forms.TabPage tpCommunication;
        public System.Windows.Forms.Panel pnl異常通知;
        private System.Windows.Forms.Label lb異常訊息;
        private System.Windows.Forms.Button btnResetMaintenance;
        private KCSDK.TextLabel lbMaintenanceTM;
        private KCSDK.NumLabel lb嫁動率;
        private System.Windows.Forms.TableLayoutPanel LightLayoutPanel;
        private KCSDK.ThreeColorLight ledBlue;
        private KCSDK.ThreeColorLight ledRed;
        private KCSDK.ThreeColorLight ledYellow;
        private KCSDK.ThreeColorLight ledGreen;
        private KCSDK.NumLabel lb_UPH;
        private KCSDK.NumLabel lb_BibCount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel16;
        public ProVLib.TrayView tv_RightLoadUnload;
        private KCSDK.NumLabel lb_LoadCompletedQty;
        private System.Windows.Forms.Panel panel18;
        public ProVLib.TrayView tv_LeftLoadUnload;
        private KCSDK.NumLabel lb_LoadProcessQty;
        private System.Windows.Forms.Button button2;
        private KCSDK.NumLabel lb_LoadMissingQty;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.Panel panel28;
        private System.Windows.Forms.Label lb_RunMode;
        private System.Windows.Forms.Panel panel27;
        private System.Windows.Forms.Label lb_LotID;
        private System.Windows.Forms.Panel panel26;
        private System.Windows.Forms.Panel panel25;
        private System.Windows.Forms.Label lb_RBoardID;
        private System.Windows.Forms.Panel panel24;
        private System.Windows.Forms.Panel panel23;
        private System.Windows.Forms.Label lb_LBoardID;
        private KCSDK.NumLabel lb_UnloadProcessQty;
        private System.Windows.Forms.Panel panel20;
        public ProVLib.TrayView tv_Transfer;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel9;
        public ProVLib.TrayView tv_ClamSheel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel12;
        public ProVLib.TrayView tv_LeftBoard;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel8;
        public ProVLib.TrayView tv_RightBoard;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private ProVLib.TrayView tv_HDT_BottomBoard;
        private ProVLib.TrayView tv_HDT_TopBoard;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel10;
        private ProVLib.TrayView tv_RightKit;
        private ProVLib.TrayView tv_LeftKit;
        private ProVLib.TrayView tv_LeftTray;
        private System.Windows.Forms.Label label36;
        private ProVLib.TrayView tv_RightTray;
        private System.Windows.Forms.Label label37;
        private KCSDK.NumLabel numLabel2;
        private KCSDK.NumLabel numLabel1;
        private ProVLib.TrayView tv_HDT_TR;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}

