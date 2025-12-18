namespace KSM
{
    partial class KSM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KSM));
            ProVLib.MotorParam motorParam1 = new ProVLib.MotorParam();
            ProVLib.MotorParam motorParam2 = new ProVLib.MotorParam();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.MT_KSM_ShuttleA = new ProVLib.Motor();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MT_KSM_ShuttleB = new ProVLib.Motor();
            this.button2 = new System.Windows.Forms.Button();
            this.FC_KSM_HOME = new ProVLib.FlowChart();
            this.flowChart1 = new ProVLib.FlowChart();
            this.flowChart2 = new ProVLib.FlowChart();
            this.flowChart3 = new ProVLib.FlowChart();
            this.FC_KSM_ShuttleA_AUTORUN = new ProVLib.FlowChart();
            this.flowChart4 = new ProVLib.FlowChart();
            this.flowChart5 = new ProVLib.FlowChart();
            this.flowChart6 = new ProVLib.FlowChart();
            this.flowChart10 = new ProVLib.FlowChart();
            this.FC_KSM_ShuttleB_AUTORUN = new ProVLib.FlowChart();
            this.flowChart7 = new ProVLib.FlowChart();
            this.flowChart8 = new ProVLib.FlowChart();
            this.flowChart9 = new ProVLib.FlowChart();
            this.flowChart11 = new ProVLib.FlowChart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dPosEdit3 = new KCSDK.DPosEdit();
            this.dPosEdit2 = new KCSDK.DPosEdit();
            this.dPosEdit1 = new KCSDK.DPosEdit();
            this.dPosEdit10 = new KCSDK.DPosEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dPosEdit4 = new KCSDK.DPosEdit();
            this.dPosEdit5 = new KCSDK.DPosEdit();
            this.dPosEdit6 = new KCSDK.DPosEdit();
            this.dPosEdit7 = new KCSDK.DPosEdit();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.dFieldEdit4 = new KCSDK.DFieldEdit();
            this.dFieldEdit9 = new KCSDK.DFieldEdit();
            this.dFieldEdit10 = new KCSDK.DFieldEdit();
            this.dFieldEdit11 = new KCSDK.DFieldEdit();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.dCheckBox2 = new KCSDK.DCheckBox();
            this.dCheckBox1 = new KCSDK.DCheckBox();
            this.tabMain.SuspendLayout();
            this.tpControl.SuspendLayout();
            this.tpPosition.SuspendLayout();
            this.tpSetting.SuspendLayout();
            this.tpFlow.SuspendLayout();
            this.tpSuperSetting.SuspendLayout();
            this.TabFlow.SuspendLayout();
            this.tpHome.SuspendLayout();
            this.tpAuto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.Images.SetKeyName(0, "position.png");
            this.imgList.Images.SetKeyName(1, "motor.png");
            this.imgList.Images.SetKeyName(2, "setting.png");
            this.imgList.Images.SetKeyName(3, "checklist.png");
            this.imgList.Images.SetKeyName(4, "edit.png");
            this.imgList.Images.SetKeyName(5, "save.png");
            this.imgList.Images.SetKeyName(6, "cancel.png");
            this.imgList.Images.SetKeyName(7, "addrow.png");
            this.imgList.Images.SetKeyName(8, "delrow1.png");
            // 
            // tabMain
            // 
            this.tabMain.Size = new System.Drawing.Size(1084, 741);
            // 
            // tpControl
            // 
            this.tpControl.Controls.Add(this.pictureBox1);
            this.tpControl.Controls.Add(this.panel2);
            this.tpControl.Size = new System.Drawing.Size(1076, 673);
            // 
            // tpPosition
            // 
            this.tpPosition.Controls.Add(this.groupBox2);
            this.tpPosition.Controls.Add(this.groupBox1);
            this.tpPosition.Controls.SetChildIndex(this.groupBox1, 0);
            this.tpPosition.Controls.SetChildIndex(this.groupBox2, 0);
            // 
            // tpSetting
            // 
            this.tpSetting.Controls.Add(this.groupBox12);
            this.tpSetting.Controls.SetChildIndex(this.groupBox12, 0);
            // 
            // tpFlow
            // 
            this.tpFlow.Size = new System.Drawing.Size(1076, 673);
            // 
            // tpSuperSetting
            // 
            this.tpSuperSetting.Controls.Add(this.groupBox11);
            this.tpSuperSetting.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tpSuperSetting.Controls.SetChildIndex(this.groupBox11, 0);
            // 
            // TabFlow
            // 
            this.TabFlow.Size = new System.Drawing.Size(1072, 669);
            // 
            // tpHome
            // 
            this.tpHome.Controls.Add(this.flowChart1);
            this.tpHome.Controls.Add(this.flowChart2);
            this.tpHome.Controls.Add(this.flowChart3);
            this.tpHome.Controls.Add(this.FC_KSM_HOME);
            this.tpHome.Size = new System.Drawing.Size(1064, 627);
            // 
            // tpAuto
            // 
            this.tpAuto.Controls.Add(this.flowChart4);
            this.tpAuto.Controls.Add(this.flowChart5);
            this.tpAuto.Controls.Add(this.flowChart6);
            this.tpAuto.Controls.Add(this.flowChart10);
            this.tpAuto.Controls.Add(this.FC_KSM_ShuttleB_AUTORUN);
            this.tpAuto.Controls.Add(this.flowChart7);
            this.tpAuto.Controls.Add(this.flowChart8);
            this.tpAuto.Controls.Add(this.flowChart9);
            this.tpAuto.Controls.Add(this.flowChart11);
            this.tpAuto.Controls.Add(this.FC_KSM_ShuttleA_AUTORUN);
            this.tpAuto.Location = new System.Drawing.Point(4, 38);
            this.tpAuto.Size = new System.Drawing.Size(1064, 627);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Location = new System.Drawing.Point(717, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(355, 569);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 110;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SeaShell;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 569);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1072, 100);
            this.panel2.TabIndex = 109;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(117, 49);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 30);
            this.button3.TabIndex = 103;
            this.button3.Text = "servo Off";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 49);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 30);
            this.button4.TabIndex = 102;
            this.button4.Text = "servo ON";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.button1);
            this.panel7.Controls.Add(this.MT_KSM_ShuttleA);
            this.panel7.Location = new System.Drawing.Point(6, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(310, 40);
            this.panel7.TabIndex = 100;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(255, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 30);
            this.button1.TabIndex = 24;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MT_KSM_ShuttleA
            // 
            this.MT_KSM_ShuttleA.Acceleration = 300000;
            this.MT_KSM_ShuttleA.AcceptDiffRange = ((uint)(0u));
            this.MT_KSM_ShuttleA.AddressID = 0;
            this.MT_KSM_ShuttleA.AlarmPolarity = ProVLib.ALMPOLARITY.ACTIVELOW;
            this.MT_KSM_ShuttleA.BackColor = System.Drawing.Color.RoyalBlue;
            this.MT_KSM_ShuttleA.BasePulseCount = 0;
            this.MT_KSM_ShuttleA.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MT_KSM_ShuttleA.CMPSRC = ProVLib.ECMPSRC.CMPSRC_1;
            this.MT_KSM_ShuttleA.D2 = ((byte)(0));
            this.MT_KSM_ShuttleA.D4 = ((byte)(0));
            this.MT_KSM_ShuttleA.Deceleration = 300000;
            this.MT_KSM_ShuttleA.DelayTime = 0D;
            this.MT_KSM_ShuttleA.Direction = true;
            this.MT_KSM_ShuttleA.DownZ1 = 0;
            this.MT_KSM_ShuttleA.DownZ2 = 0;
            this.MT_KSM_ShuttleA.DownZ3 = 0;
            this.MT_KSM_ShuttleA.EncGearRatio = 2D;
            this.MT_KSM_ShuttleA.EndX = 0;
            this.MT_KSM_ShuttleA.ErrID = 0;
            this.MT_KSM_ShuttleA.GearRatio = 2D;
            this.MT_KSM_ShuttleA.GroupNo = ((short)(0));
            this.MT_KSM_ShuttleA.HomeBeforeGoto = false;
            this.MT_KSM_ShuttleA.HomeDirection = true;
            this.MT_KSM_ShuttleA.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            this.MT_KSM_ShuttleA.HomeOK = false;
            this.MT_KSM_ShuttleA.HomePos = 100;
            this.MT_KSM_ShuttleA.InAlarm = false;
            this.MT_KSM_ShuttleA.InitialPosition = 0;
            this.MT_KSM_ShuttleA.InitSpeed = 100;
            this.MT_KSM_ShuttleA.InPosOn = false;
            this.MT_KSM_ShuttleA.InposRange = 50;
            this.MT_KSM_ShuttleA.IOPort = "011";
            this.MT_KSM_ShuttleA.IsELSensorB = true;
            this.MT_KSM_ShuttleA.IsSensorB = true;
            this.MT_KSM_ShuttleA.IsUseMileage = false;
            this.MT_KSM_ShuttleA.IsUseSoftLimit = false;
            this.MT_KSM_ShuttleA.JogHighSpeed = 30000;
            this.MT_KSM_ShuttleA.JogLowSpeed = 1000;
            this.MT_KSM_ShuttleA.LimitX = 0;
            this.MT_KSM_ShuttleA.LimitZ = 0;
            this.MT_KSM_ShuttleA.LineID = ((uint)(0u));
            this.MT_KSM_ShuttleA.Location = new System.Drawing.Point(5, 5);
            this.MT_KSM_ShuttleA.LockUI = false;
            this.MT_KSM_ShuttleA.Message = null;
            this.MT_KSM_ShuttleA.Mileage = 0F;
            this.MT_KSM_ShuttleA.MNETSpeed = ProVLib.EMNETSPEED.MNET10M;
            motorParam1.Acceleration = 300000;
            motorParam1.AddressID = 0;
            motorParam1.AlarmPolarity = ProVLib.ALMPOLARITY.ACTIVELOW;
            motorParam1.BasePulseCount = 0;
            motorParam1.CMPSRC = ProVLib.ECMPSRC.CMPSRC_1;
            motorParam1.Deceleration = 300000;
            motorParam1.DelayT = 0D;
            motorParam1.Direction = true;
            motorParam1.DownZ1 = 0;
            motorParam1.DownZ2 = 0;
            motorParam1.DownZ3 = 0;
            motorParam1.EncGearRatio = 2D;
            motorParam1.EndX = 0;
            motorParam1.GearRatio = 2D;
            motorParam1.GroupNo = ((short)(0));
            motorParam1.HomeBeforeGoto = false;
            motorParam1.HomeDirection = true;
            motorParam1.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            motorParam1.HomePos = 100;
            motorParam1.IDZ = ((short)(0));
            motorParam1.InitSpeed = 100;
            motorParam1.InPosOn = false;
            motorParam1.InposRange = 50;
            motorParam1.IOPort = "011";
            motorParam1.IsBusy = false;
            motorParam1.IsELSensorB = true;
            motorParam1.IsSensorB = true;
            motorParam1.IsUseSoftLimit = false;
            motorParam1.JogHighSpeed = 30000;
            motorParam1.JogLowSpeed = 1000;
            motorParam1.LimitX = 0;
            motorParam1.LimitZ = 0;
            motorParam1.MNETSpeed = ProVLib.EMNETSPEED.MNET10M;
            motorParam1.MotionCard = ProVLib.EMOTIONCARD.MCMNET;
            motorParam1.MotorType = ProVLib.MOTORTYPE.NORMAL;
            motorParam1.ObjType = 0;
            motorParam1.Owner = null;
            motorParam1.PitchCOMEnable = false;
            motorParam1.PulseCtrlMode = ProVLib.EPULSEMODE.CWCCW_2;
            motorParam1.SerialPortName = "COM1";
            motorParam1.ServoAlarmOn = false;
            motorParam1.ServoOnPolarity = ProVLib.SVONPOLARITY.ACTIVELOW;
            motorParam1.SlaveIOPort = "000";
            motorParam1.SoftLimitN = -9999999;
            motorParam1.SoftLimitP = 9999999;
            motorParam1.Speed = 50000;
            motorParam1.TRGSRC = ProVLib.ETRIGGERSRC.TRGENC;
            motorParam1.TriAxis = null;
            motorParam1.UpZ = 0;
            motorParam1.ZPhaseLogic = ProVLib.ZPHASELOGIC.FALLINGEDGE;
            this.MT_KSM_ShuttleA.MotorParameter = motorParam1;
            this.MT_KSM_ShuttleA.MoverSize = ((uint)(0u));
            this.MT_KSM_ShuttleA.MsgID = 0;
            this.MT_KSM_ShuttleA.Name = "MT_KSM_ShuttleA";
            this.MT_KSM_ShuttleA.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.MT_KSM_ShuttleA.PitchCOMEnable = false;
            this.MT_KSM_ShuttleA.PriorityHigh = ((uint)(0u));
            this.MT_KSM_ShuttleA.PriorityLow = ((uint)(0u));
            this.MT_KSM_ShuttleA.PulseCtrlMode = ProVLib.EPULSEMODE.CWCCW_2;
            this.MT_KSM_ShuttleA.RelCurrentPos = 0;
            this.MT_KSM_ShuttleA.RelTargetPos = 0;
            this.MT_KSM_ShuttleA.Running = false;
            this.MT_KSM_ShuttleA.SafeDistance = ((uint)(0u));
            this.MT_KSM_ShuttleA.SerialPortName = "COM1";
            this.MT_KSM_ShuttleA.ServoAlarmOn = false;
            this.MT_KSM_ShuttleA.ServoOnPolarity = ProVLib.SVONPOLARITY.ACTIVELOW;
            this.MT_KSM_ShuttleA.Size = new System.Drawing.Size(250, 30);
            this.MT_KSM_ShuttleA.SlaveIOPort = "000";
            this.MT_KSM_ShuttleA.SoftLimitN = -9999999;
            this.MT_KSM_ShuttleA.SoftLimitP = 9999999;
            this.MT_KSM_ShuttleA.Speed = 50000;
            this.MT_KSM_ShuttleA.Text = "(M20) ShuttleA Y";
            this.MT_KSM_ShuttleA.TRGSRC = ProVLib.ETRIGGERSRC.TRGENC;
            this.MT_KSM_ShuttleA.UpZ = 0;
            this.MT_KSM_ShuttleA.ZPhaseLogic = ProVLib.ZPHASELOGIC.FALLINGEDGE;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.MT_KSM_ShuttleB);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(322, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(310, 40);
            this.panel1.TabIndex = 101;
            // 
            // MT_KSM_ShuttleB
            // 
            this.MT_KSM_ShuttleB.Acceleration = 300000;
            this.MT_KSM_ShuttleB.AcceptDiffRange = ((uint)(0u));
            this.MT_KSM_ShuttleB.AddressID = 0;
            this.MT_KSM_ShuttleB.AlarmPolarity = ProVLib.ALMPOLARITY.ACTIVELOW;
            this.MT_KSM_ShuttleB.BackColor = System.Drawing.Color.RoyalBlue;
            this.MT_KSM_ShuttleB.BasePulseCount = 0;
            this.MT_KSM_ShuttleB.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MT_KSM_ShuttleB.CMPSRC = ProVLib.ECMPSRC.CMPSRC_1;
            this.MT_KSM_ShuttleB.D2 = ((byte)(0));
            this.MT_KSM_ShuttleB.D4 = ((byte)(0));
            this.MT_KSM_ShuttleB.Deceleration = 300000;
            this.MT_KSM_ShuttleB.DelayTime = 0D;
            this.MT_KSM_ShuttleB.Direction = true;
            this.MT_KSM_ShuttleB.DownZ1 = 0;
            this.MT_KSM_ShuttleB.DownZ2 = 0;
            this.MT_KSM_ShuttleB.DownZ3 = 0;
            this.MT_KSM_ShuttleB.EncGearRatio = 2D;
            this.MT_KSM_ShuttleB.EndX = 0;
            this.MT_KSM_ShuttleB.ErrID = 0;
            this.MT_KSM_ShuttleB.GearRatio = 2D;
            this.MT_KSM_ShuttleB.GroupNo = ((short)(0));
            this.MT_KSM_ShuttleB.HomeBeforeGoto = false;
            this.MT_KSM_ShuttleB.HomeDirection = true;
            this.MT_KSM_ShuttleB.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            this.MT_KSM_ShuttleB.HomeOK = false;
            this.MT_KSM_ShuttleB.HomePos = 100;
            this.MT_KSM_ShuttleB.InAlarm = false;
            this.MT_KSM_ShuttleB.InitialPosition = 0;
            this.MT_KSM_ShuttleB.InitSpeed = 100;
            this.MT_KSM_ShuttleB.InPosOn = false;
            this.MT_KSM_ShuttleB.InposRange = 50;
            this.MT_KSM_ShuttleB.IOPort = "012";
            this.MT_KSM_ShuttleB.IsELSensorB = true;
            this.MT_KSM_ShuttleB.IsSensorB = true;
            this.MT_KSM_ShuttleB.IsUseMileage = false;
            this.MT_KSM_ShuttleB.IsUseSoftLimit = false;
            this.MT_KSM_ShuttleB.JogHighSpeed = 30000;
            this.MT_KSM_ShuttleB.JogLowSpeed = 1000;
            this.MT_KSM_ShuttleB.LimitX = 0;
            this.MT_KSM_ShuttleB.LimitZ = 0;
            this.MT_KSM_ShuttleB.LineID = ((uint)(0u));
            this.MT_KSM_ShuttleB.Location = new System.Drawing.Point(5, 5);
            this.MT_KSM_ShuttleB.LockUI = false;
            this.MT_KSM_ShuttleB.Message = null;
            this.MT_KSM_ShuttleB.Mileage = 0F;
            this.MT_KSM_ShuttleB.MNETSpeed = ProVLib.EMNETSPEED.MNET10M;
            motorParam2.Acceleration = 300000;
            motorParam2.AddressID = 0;
            motorParam2.AlarmPolarity = ProVLib.ALMPOLARITY.ACTIVELOW;
            motorParam2.BasePulseCount = 0;
            motorParam2.CMPSRC = ProVLib.ECMPSRC.CMPSRC_1;
            motorParam2.Deceleration = 300000;
            motorParam2.DelayT = 0D;
            motorParam2.Direction = true;
            motorParam2.DownZ1 = 0;
            motorParam2.DownZ2 = 0;
            motorParam2.DownZ3 = 0;
            motorParam2.EncGearRatio = 2D;
            motorParam2.EndX = 0;
            motorParam2.GearRatio = 2D;
            motorParam2.GroupNo = ((short)(0));
            motorParam2.HomeBeforeGoto = false;
            motorParam2.HomeDirection = true;
            motorParam2.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            motorParam2.HomePos = 100;
            motorParam2.IDZ = ((short)(0));
            motorParam2.InitSpeed = 100;
            motorParam2.InPosOn = false;
            motorParam2.InposRange = 50;
            motorParam2.IOPort = "012";
            motorParam2.IsBusy = false;
            motorParam2.IsELSensorB = true;
            motorParam2.IsSensorB = true;
            motorParam2.IsUseSoftLimit = false;
            motorParam2.JogHighSpeed = 30000;
            motorParam2.JogLowSpeed = 1000;
            motorParam2.LimitX = 0;
            motorParam2.LimitZ = 0;
            motorParam2.MNETSpeed = ProVLib.EMNETSPEED.MNET10M;
            motorParam2.MotionCard = ProVLib.EMOTIONCARD.MCMNET;
            motorParam2.MotorType = ProVLib.MOTORTYPE.NORMAL;
            motorParam2.ObjType = 0;
            motorParam2.Owner = null;
            motorParam2.PitchCOMEnable = false;
            motorParam2.PulseCtrlMode = ProVLib.EPULSEMODE.CWCCW_2;
            motorParam2.SerialPortName = "COM1";
            motorParam2.ServoAlarmOn = false;
            motorParam2.ServoOnPolarity = ProVLib.SVONPOLARITY.ACTIVELOW;
            motorParam2.SlaveIOPort = "000";
            motorParam2.SoftLimitN = -9999999;
            motorParam2.SoftLimitP = 9999999;
            motorParam2.Speed = 50000;
            motorParam2.TRGSRC = ProVLib.ETRIGGERSRC.TRGENC;
            motorParam2.TriAxis = null;
            motorParam2.UpZ = 0;
            motorParam2.ZPhaseLogic = ProVLib.ZPHASELOGIC.FALLINGEDGE;
            this.MT_KSM_ShuttleB.MotorParameter = motorParam2;
            this.MT_KSM_ShuttleB.MoverSize = ((uint)(0u));
            this.MT_KSM_ShuttleB.MsgID = 0;
            this.MT_KSM_ShuttleB.Name = "MT_KSM_ShuttleB";
            this.MT_KSM_ShuttleB.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.MT_KSM_ShuttleB.PitchCOMEnable = false;
            this.MT_KSM_ShuttleB.PriorityHigh = ((uint)(0u));
            this.MT_KSM_ShuttleB.PriorityLow = ((uint)(0u));
            this.MT_KSM_ShuttleB.PulseCtrlMode = ProVLib.EPULSEMODE.CWCCW_2;
            this.MT_KSM_ShuttleB.RelCurrentPos = 0;
            this.MT_KSM_ShuttleB.RelTargetPos = 0;
            this.MT_KSM_ShuttleB.Running = false;
            this.MT_KSM_ShuttleB.SafeDistance = ((uint)(0u));
            this.MT_KSM_ShuttleB.SerialPortName = "COM1";
            this.MT_KSM_ShuttleB.ServoAlarmOn = false;
            this.MT_KSM_ShuttleB.ServoOnPolarity = ProVLib.SVONPOLARITY.ACTIVELOW;
            this.MT_KSM_ShuttleB.Size = new System.Drawing.Size(250, 30);
            this.MT_KSM_ShuttleB.SlaveIOPort = "000";
            this.MT_KSM_ShuttleB.SoftLimitN = -9999999;
            this.MT_KSM_ShuttleB.SoftLimitP = 9999999;
            this.MT_KSM_ShuttleB.Speed = 50000;
            this.MT_KSM_ShuttleB.Text = "(M24) ShuttleB Y";
            this.MT_KSM_ShuttleB.TRGSRC = ProVLib.ETRIGGERSRC.TRGENC;
            this.MT_KSM_ShuttleB.UpZ = 0;
            this.MT_KSM_ShuttleB.ZPhaseLogic = ProVLib.ZPHASELOGIC.FALLINGEDGE;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(255, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 30);
            this.button2.TabIndex = 24;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FC_KSM_HOME
            // 
            this.FC_KSM_HOME.BackColor = System.Drawing.Color.RoyalBlue;
            this.FC_KSM_HOME.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.FC_KSM_HOME.CASE1 = null;
            this.FC_KSM_HOME.CASE2 = null;
            this.FC_KSM_HOME.CASE3 = null;
            this.FC_KSM_HOME.CASE4 = null;
            this.FC_KSM_HOME.ContinueRun = false;
            this.FC_KSM_HOME.EndFC = null;
            this.FC_KSM_HOME.ErrID = 0;
            this.FC_KSM_HOME.InAlarm = false;
            this.FC_KSM_HOME.IsFlowHead = false;
            this.FC_KSM_HOME.Location = new System.Drawing.Point(92, 64);
            this.FC_KSM_HOME.LockUI = false;
            this.FC_KSM_HOME.Message = null;
            this.FC_KSM_HOME.MsgID = 0;
            this.FC_KSM_HOME.Name = "FC_KSM_HOME";
            this.FC_KSM_HOME.NEXT = this.flowChart1;
            this.FC_KSM_HOME.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.FC_KSM_HOME.OrgLocation = new System.Drawing.Point(0, 0);
            this.FC_KSM_HOME.OrgSize = new System.Drawing.Size(0, 0);
            this.FC_KSM_HOME.OverTimeSpec = 100;
            this.FC_KSM_HOME.Running = false;
            this.FC_KSM_HOME.Size = new System.Drawing.Size(200, 30);
            this.FC_KSM_HOME.SlowRunCycle = -1;
            this.FC_KSM_HOME.StartFC = null;
            this.FC_KSM_HOME.Text = "Start Home";
            this.FC_KSM_HOME.Run += new ProVLib.FlowChart.RunEventHandler(this.FC_KSM_HOME_Run);
            // 
            // flowChart1
            // 
            this.flowChart1.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart1.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart1.CASE1 = null;
            this.flowChart1.CASE2 = null;
            this.flowChart1.CASE3 = null;
            this.flowChart1.CASE4 = null;
            this.flowChart1.ContinueRun = false;
            this.flowChart1.EndFC = null;
            this.flowChart1.ErrID = 0;
            this.flowChart1.InAlarm = false;
            this.flowChart1.IsFlowHead = false;
            this.flowChart1.Location = new System.Drawing.Point(92, 114);
            this.flowChart1.LockUI = false;
            this.flowChart1.Message = null;
            this.flowChart1.MsgID = 0;
            this.flowChart1.Name = "flowChart1";
            this.flowChart1.NEXT = this.flowChart2;
            this.flowChart1.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart1.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart1.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart1.OverTimeSpec = 100;
            this.flowChart1.Running = false;
            this.flowChart1.Size = new System.Drawing.Size(200, 30);
            this.flowChart1.SlowRunCycle = -1;
            this.flowChart1.StartFC = null;
            this.flowChart1.Text = "Can Home ?";
            this.flowChart1.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart1_Run);
            // 
            // flowChart2
            // 
            this.flowChart2.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart2.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart2.CASE1 = null;
            this.flowChart2.CASE2 = null;
            this.flowChart2.CASE3 = null;
            this.flowChart2.CASE4 = null;
            this.flowChart2.ContinueRun = false;
            this.flowChart2.EndFC = null;
            this.flowChart2.ErrID = 0;
            this.flowChart2.InAlarm = false;
            this.flowChart2.IsFlowHead = false;
            this.flowChart2.Location = new System.Drawing.Point(92, 163);
            this.flowChart2.LockUI = false;
            this.flowChart2.Message = null;
            this.flowChart2.MsgID = 0;
            this.flowChart2.Name = "flowChart2";
            this.flowChart2.NEXT = this.flowChart3;
            this.flowChart2.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart2.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart2.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart2.OverTimeSpec = 100;
            this.flowChart2.Running = false;
            this.flowChart2.Size = new System.Drawing.Size(200, 30);
            this.flowChart2.SlowRunCycle = -1;
            this.flowChart2.StartFC = null;
            this.flowChart2.Text = "Shuttle A、B Home";
            this.flowChart2.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart2_Run);
            // 
            // flowChart3
            // 
            this.flowChart3.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart3.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart3.CASE1 = null;
            this.flowChart3.CASE2 = null;
            this.flowChart3.CASE3 = null;
            this.flowChart3.CASE4 = null;
            this.flowChart3.ContinueRun = false;
            this.flowChart3.EndFC = null;
            this.flowChart3.ErrID = 0;
            this.flowChart3.InAlarm = false;
            this.flowChart3.IsFlowHead = false;
            this.flowChart3.Location = new System.Drawing.Point(92, 212);
            this.flowChart3.LockUI = false;
            this.flowChart3.Message = null;
            this.flowChart3.MsgID = 0;
            this.flowChart3.Name = "flowChart3";
            this.flowChart3.NEXT = null;
            this.flowChart3.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart3.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart3.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart3.OverTimeSpec = 100;
            this.flowChart3.Running = false;
            this.flowChart3.Size = new System.Drawing.Size(200, 30);
            this.flowChart3.SlowRunCycle = -1;
            this.flowChart3.StartFC = null;
            this.flowChart3.Text = "Done";
            this.flowChart3.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart3_Run);
            // 
            // FC_KSM_ShuttleA_AUTORUN
            // 
            this.FC_KSM_ShuttleA_AUTORUN.BackColor = System.Drawing.Color.RoyalBlue;
            this.FC_KSM_ShuttleA_AUTORUN.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.FC_KSM_ShuttleA_AUTORUN.CASE1 = null;
            this.FC_KSM_ShuttleA_AUTORUN.CASE2 = null;
            this.FC_KSM_ShuttleA_AUTORUN.CASE3 = null;
            this.FC_KSM_ShuttleA_AUTORUN.CASE4 = null;
            this.FC_KSM_ShuttleA_AUTORUN.ContinueRun = false;
            this.FC_KSM_ShuttleA_AUTORUN.EndFC = null;
            this.FC_KSM_ShuttleA_AUTORUN.ErrID = 0;
            this.FC_KSM_ShuttleA_AUTORUN.InAlarm = false;
            this.FC_KSM_ShuttleA_AUTORUN.IsFlowHead = false;
            this.FC_KSM_ShuttleA_AUTORUN.Location = new System.Drawing.Point(84, 79);
            this.FC_KSM_ShuttleA_AUTORUN.LockUI = false;
            this.FC_KSM_ShuttleA_AUTORUN.Message = null;
            this.FC_KSM_ShuttleA_AUTORUN.MsgID = 0;
            this.FC_KSM_ShuttleA_AUTORUN.Name = "FC_KSM_ShuttleA_AUTORUN";
            this.FC_KSM_ShuttleA_AUTORUN.NEXT = this.flowChart4;
            this.FC_KSM_ShuttleA_AUTORUN.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.FC_KSM_ShuttleA_AUTORUN.OrgLocation = new System.Drawing.Point(0, 0);
            this.FC_KSM_ShuttleA_AUTORUN.OrgSize = new System.Drawing.Size(0, 0);
            this.FC_KSM_ShuttleA_AUTORUN.OverTimeSpec = 100;
            this.FC_KSM_ShuttleA_AUTORUN.Running = false;
            this.FC_KSM_ShuttleA_AUTORUN.Size = new System.Drawing.Size(200, 30);
            this.FC_KSM_ShuttleA_AUTORUN.SlowRunCycle = -1;
            this.FC_KSM_ShuttleA_AUTORUN.StartFC = null;
            this.FC_KSM_ShuttleA_AUTORUN.Text = "Start Shuttle A Move";
            this.FC_KSM_ShuttleA_AUTORUN.Run += new ProVLib.FlowChart.RunEventHandler(this.FC_KSM_ShuttleA_AUTORUN_Run);
            // 
            // flowChart4
            // 
            this.flowChart4.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart4.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart4.CASE1 = null;
            this.flowChart4.CASE2 = null;
            this.flowChart4.CASE3 = null;
            this.flowChart4.CASE4 = null;
            this.flowChart4.ContinueRun = false;
            this.flowChart4.EndFC = null;
            this.flowChart4.ErrID = 0;
            this.flowChart4.InAlarm = false;
            this.flowChart4.IsFlowHead = false;
            this.flowChart4.Location = new System.Drawing.Point(84, 127);
            this.flowChart4.LockUI = false;
            this.flowChart4.Message = null;
            this.flowChart4.MsgID = 0;
            this.flowChart4.Name = "flowChart4";
            this.flowChart4.NEXT = this.flowChart5;
            this.flowChart4.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart4.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart4.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart4.OverTimeSpec = 100;
            this.flowChart4.Running = false;
            this.flowChart4.Size = new System.Drawing.Size(200, 30);
            this.flowChart4.SlowRunCycle = -1;
            this.flowChart4.StartFC = null;
            this.flowChart4.Text = "Dose Shuttle Need Move?";
            this.flowChart4.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart4_Run);
            // 
            // flowChart5
            // 
            this.flowChart5.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart5.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart5.CASE1 = null;
            this.flowChart5.CASE2 = null;
            this.flowChart5.CASE3 = null;
            this.flowChart5.CASE4 = null;
            this.flowChart5.ContinueRun = false;
            this.flowChart5.EndFC = null;
            this.flowChart5.ErrID = 0;
            this.flowChart5.InAlarm = false;
            this.flowChart5.IsFlowHead = false;
            this.flowChart5.Location = new System.Drawing.Point(84, 176);
            this.flowChart5.LockUI = false;
            this.flowChart5.Message = null;
            this.flowChart5.MsgID = 0;
            this.flowChart5.Name = "flowChart5";
            this.flowChart5.NEXT = this.flowChart6;
            this.flowChart5.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart5.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart5.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart5.OverTimeSpec = 100;
            this.flowChart5.Running = false;
            this.flowChart5.Size = new System.Drawing.Size(200, 30);
            this.flowChart5.SlowRunCycle = -1;
            this.flowChart5.StartFC = null;
            this.flowChart5.Text = "Shuttle Move";
            this.flowChart5.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart5_Run);
            // 
            // flowChart6
            // 
            this.flowChart6.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart6.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart6.CASE1 = null;
            this.flowChart6.CASE2 = null;
            this.flowChart6.CASE3 = null;
            this.flowChart6.CASE4 = null;
            this.flowChart6.ContinueRun = false;
            this.flowChart6.EndFC = null;
            this.flowChart6.ErrID = 0;
            this.flowChart6.InAlarm = false;
            this.flowChart6.IsFlowHead = false;
            this.flowChart6.Location = new System.Drawing.Point(302, 176);
            this.flowChart6.LockUI = false;
            this.flowChart6.Message = null;
            this.flowChart6.MsgID = 0;
            this.flowChart6.Name = "flowChart6";
            this.flowChart6.NEXT = this.flowChart10;
            this.flowChart6.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart6.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart6.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart6.OverTimeSpec = 100;
            this.flowChart6.Running = false;
            this.flowChart6.Size = new System.Drawing.Size(50, 30);
            this.flowChart6.SlowRunCycle = -1;
            this.flowChart6.StartFC = null;
            this.flowChart6.Text = "Done";
            this.flowChart6.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart6_Run);
            // 
            // flowChart10
            // 
            this.flowChart10.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart10.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart10.CASE1 = null;
            this.flowChart10.CASE2 = null;
            this.flowChart10.CASE3 = null;
            this.flowChart10.CASE4 = null;
            this.flowChart10.ContinueRun = false;
            this.flowChart10.EndFC = null;
            this.flowChart10.ErrID = 0;
            this.flowChart10.InAlarm = false;
            this.flowChart10.IsFlowHead = false;
            this.flowChart10.Location = new System.Drawing.Point(302, 127);
            this.flowChart10.LockUI = false;
            this.flowChart10.Message = null;
            this.flowChart10.MsgID = 0;
            this.flowChart10.Name = "flowChart10";
            this.flowChart10.NEXT = this.flowChart4;
            this.flowChart10.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart10.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart10.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart10.OverTimeSpec = 100;
            this.flowChart10.Running = false;
            this.flowChart10.Size = new System.Drawing.Size(50, 30);
            this.flowChart10.SlowRunCycle = -1;
            this.flowChart10.StartFC = null;
            this.flowChart10.Text = "Next";
            this.flowChart10.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart10_Run);
            // 
            // FC_KSM_ShuttleB_AUTORUN
            // 
            this.FC_KSM_ShuttleB_AUTORUN.BackColor = System.Drawing.Color.RoyalBlue;
            this.FC_KSM_ShuttleB_AUTORUN.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.FC_KSM_ShuttleB_AUTORUN.CASE1 = null;
            this.FC_KSM_ShuttleB_AUTORUN.CASE2 = null;
            this.FC_KSM_ShuttleB_AUTORUN.CASE3 = null;
            this.FC_KSM_ShuttleB_AUTORUN.CASE4 = null;
            this.FC_KSM_ShuttleB_AUTORUN.ContinueRun = false;
            this.FC_KSM_ShuttleB_AUTORUN.EndFC = null;
            this.FC_KSM_ShuttleB_AUTORUN.ErrID = 0;
            this.FC_KSM_ShuttleB_AUTORUN.InAlarm = false;
            this.FC_KSM_ShuttleB_AUTORUN.IsFlowHead = false;
            this.FC_KSM_ShuttleB_AUTORUN.Location = new System.Drawing.Point(467, 79);
            this.FC_KSM_ShuttleB_AUTORUN.LockUI = false;
            this.FC_KSM_ShuttleB_AUTORUN.Message = null;
            this.FC_KSM_ShuttleB_AUTORUN.MsgID = 0;
            this.FC_KSM_ShuttleB_AUTORUN.Name = "FC_KSM_ShuttleB_AUTORUN";
            this.FC_KSM_ShuttleB_AUTORUN.NEXT = this.flowChart7;
            this.FC_KSM_ShuttleB_AUTORUN.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.FC_KSM_ShuttleB_AUTORUN.OrgLocation = new System.Drawing.Point(0, 0);
            this.FC_KSM_ShuttleB_AUTORUN.OrgSize = new System.Drawing.Size(0, 0);
            this.FC_KSM_ShuttleB_AUTORUN.OverTimeSpec = 100;
            this.FC_KSM_ShuttleB_AUTORUN.Running = false;
            this.FC_KSM_ShuttleB_AUTORUN.Size = new System.Drawing.Size(200, 30);
            this.FC_KSM_ShuttleB_AUTORUN.SlowRunCycle = -1;
            this.FC_KSM_ShuttleB_AUTORUN.StartFC = null;
            this.FC_KSM_ShuttleB_AUTORUN.Text = "Start Shuttle B Move";
            this.FC_KSM_ShuttleB_AUTORUN.Run += new ProVLib.FlowChart.RunEventHandler(this.FC_KSM_ShuttleB_AUTORUN_Run);
            // 
            // flowChart7
            // 
            this.flowChart7.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart7.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart7.CASE1 = null;
            this.flowChart7.CASE2 = null;
            this.flowChart7.CASE3 = null;
            this.flowChart7.CASE4 = null;
            this.flowChart7.ContinueRun = false;
            this.flowChart7.EndFC = null;
            this.flowChart7.ErrID = 0;
            this.flowChart7.InAlarm = false;
            this.flowChart7.IsFlowHead = false;
            this.flowChart7.Location = new System.Drawing.Point(467, 127);
            this.flowChart7.LockUI = false;
            this.flowChart7.Message = null;
            this.flowChart7.MsgID = 0;
            this.flowChart7.Name = "flowChart7";
            this.flowChart7.NEXT = this.flowChart8;
            this.flowChart7.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart7.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart7.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart7.OverTimeSpec = 100;
            this.flowChart7.Running = false;
            this.flowChart7.Size = new System.Drawing.Size(200, 30);
            this.flowChart7.SlowRunCycle = -1;
            this.flowChart7.StartFC = null;
            this.flowChart7.Text = "Dose Shuttle Need Move?";
            this.flowChart7.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart7_Run);
            // 
            // flowChart8
            // 
            this.flowChart8.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart8.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart8.CASE1 = null;
            this.flowChart8.CASE2 = null;
            this.flowChart8.CASE3 = null;
            this.flowChart8.CASE4 = null;
            this.flowChart8.ContinueRun = false;
            this.flowChart8.EndFC = null;
            this.flowChart8.ErrID = 0;
            this.flowChart8.InAlarm = false;
            this.flowChart8.IsFlowHead = false;
            this.flowChart8.Location = new System.Drawing.Point(467, 176);
            this.flowChart8.LockUI = false;
            this.flowChart8.Message = null;
            this.flowChart8.MsgID = 0;
            this.flowChart8.Name = "flowChart8";
            this.flowChart8.NEXT = this.flowChart9;
            this.flowChart8.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart8.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart8.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart8.OverTimeSpec = 100;
            this.flowChart8.Running = false;
            this.flowChart8.Size = new System.Drawing.Size(200, 30);
            this.flowChart8.SlowRunCycle = -1;
            this.flowChart8.StartFC = null;
            this.flowChart8.Text = "Shuttle Move";
            this.flowChart8.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart8_Run);
            // 
            // flowChart9
            // 
            this.flowChart9.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart9.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart9.CASE1 = null;
            this.flowChart9.CASE2 = null;
            this.flowChart9.CASE3 = null;
            this.flowChart9.CASE4 = null;
            this.flowChart9.ContinueRun = false;
            this.flowChart9.EndFC = null;
            this.flowChart9.ErrID = 0;
            this.flowChart9.InAlarm = false;
            this.flowChart9.IsFlowHead = false;
            this.flowChart9.Location = new System.Drawing.Point(687, 176);
            this.flowChart9.LockUI = false;
            this.flowChart9.Message = null;
            this.flowChart9.MsgID = 0;
            this.flowChart9.Name = "flowChart9";
            this.flowChart9.NEXT = this.flowChart11;
            this.flowChart9.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart9.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart9.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart9.OverTimeSpec = 100;
            this.flowChart9.Running = false;
            this.flowChart9.Size = new System.Drawing.Size(50, 30);
            this.flowChart9.SlowRunCycle = -1;
            this.flowChart9.StartFC = null;
            this.flowChart9.Text = "Done";
            this.flowChart9.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart9_Run);
            // 
            // flowChart11
            // 
            this.flowChart11.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart11.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart11.CASE1 = null;
            this.flowChart11.CASE2 = null;
            this.flowChart11.CASE3 = null;
            this.flowChart11.CASE4 = null;
            this.flowChart11.ContinueRun = false;
            this.flowChart11.EndFC = null;
            this.flowChart11.ErrID = 0;
            this.flowChart11.InAlarm = false;
            this.flowChart11.IsFlowHead = false;
            this.flowChart11.Location = new System.Drawing.Point(687, 127);
            this.flowChart11.LockUI = false;
            this.flowChart11.Message = null;
            this.flowChart11.MsgID = 0;
            this.flowChart11.Name = "flowChart11";
            this.flowChart11.NEXT = this.flowChart7;
            this.flowChart11.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart11.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart11.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart11.OverTimeSpec = 100;
            this.flowChart11.Running = false;
            this.flowChart11.Size = new System.Drawing.Size(50, 30);
            this.flowChart11.SlowRunCycle = -1;
            this.flowChart11.StartFC = null;
            this.flowChart11.Text = "Next";
            this.flowChart11.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart11_Run);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dPosEdit3);
            this.groupBox1.Controls.Add(this.dPosEdit2);
            this.groupBox1.Controls.Add(this.dPosEdit1);
            this.groupBox1.Controls.Add(this.dPosEdit10);
            this.groupBox1.Location = new System.Drawing.Point(16, 82);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 164);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transfer ShuttleA";
            // 
            // dPosEdit3
            // 
            this.dPosEdit3.AutoFocus = false;
            this.dPosEdit3.Caption = "BowlFeeder HeadB Y";
            this.dPosEdit3.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit3.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit3.DataName = "Pos_ShuttleA_BFM_B_Y";
            this.dPosEdit3.DataSource = this.SetDS;
            this.dPosEdit3.DefaultValue = null;
            this.dPosEdit3.EditColor = System.Drawing.Color.Black;
            this.dPosEdit3.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit3.EditWidth = 100;
            this.dPosEdit3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit3.IsModified = false;
            this.dPosEdit3.Location = new System.Drawing.Point(17, 111);
            this.dPosEdit3.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit3.MaxValue = 9999999D;
            this.dPosEdit3.MinValue = -9999999D;
            this.dPosEdit3.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit3.MotorP = this.MT_KSM_ShuttleA;
            this.dPosEdit3.Name = "dPosEdit3";
            this.dPosEdit3.NoChangeInAuto = false;
            this.dPosEdit3.PosValue = "";
            this.dPosEdit3.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit3.StepValue = 0D;
            this.dPosEdit3.TabIndex = 42;
            this.dPosEdit3.Unit = "um";
            this.dPosEdit3.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit3.UnitWidth = 40;
            this.dPosEdit3.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit2
            // 
            this.dPosEdit2.AutoFocus = false;
            this.dPosEdit2.Caption = "BowlFeeder HeadA Y";
            this.dPosEdit2.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit2.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit2.DataName = "Pos_ShuttleA_BFM_A_Y";
            this.dPosEdit2.DataSource = this.SetDS;
            this.dPosEdit2.DefaultValue = null;
            this.dPosEdit2.EditColor = System.Drawing.Color.Black;
            this.dPosEdit2.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit2.EditWidth = 100;
            this.dPosEdit2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit2.IsModified = false;
            this.dPosEdit2.Location = new System.Drawing.Point(17, 86);
            this.dPosEdit2.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit2.MaxValue = 9999999D;
            this.dPosEdit2.MinValue = -9999999D;
            this.dPosEdit2.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit2.MotorP = this.MT_KSM_ShuttleA;
            this.dPosEdit2.Name = "dPosEdit2";
            this.dPosEdit2.NoChangeInAuto = false;
            this.dPosEdit2.PosValue = "";
            this.dPosEdit2.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit2.StepValue = 0D;
            this.dPosEdit2.TabIndex = 41;
            this.dPosEdit2.Unit = "um";
            this.dPosEdit2.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit2.UnitWidth = 40;
            this.dPosEdit2.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit1
            // 
            this.dPosEdit1.AutoFocus = false;
            this.dPosEdit1.Caption = "Board HeadB Y";
            this.dPosEdit1.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit1.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit1.DataName = "Pos_ShuttleA_HDT_B_Y";
            this.dPosEdit1.DataSource = this.SetDS;
            this.dPosEdit1.DefaultValue = null;
            this.dPosEdit1.EditColor = System.Drawing.Color.Black;
            this.dPosEdit1.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit1.EditWidth = 100;
            this.dPosEdit1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit1.IsModified = false;
            this.dPosEdit1.Location = new System.Drawing.Point(17, 61);
            this.dPosEdit1.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit1.MaxValue = 9999999D;
            this.dPosEdit1.MinValue = -9999999D;
            this.dPosEdit1.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit1.MotorP = this.MT_KSM_ShuttleA;
            this.dPosEdit1.Name = "dPosEdit1";
            this.dPosEdit1.NoChangeInAuto = false;
            this.dPosEdit1.PosValue = "";
            this.dPosEdit1.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit1.StepValue = 0D;
            this.dPosEdit1.TabIndex = 40;
            this.dPosEdit1.Unit = "um";
            this.dPosEdit1.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit1.UnitWidth = 40;
            this.dPosEdit1.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit10
            // 
            this.dPosEdit10.AutoFocus = false;
            this.dPosEdit10.Caption = "Board HeadA Y";
            this.dPosEdit10.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit10.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit10.DataName = "Pos_ShuttleA_HDT_A_Y";
            this.dPosEdit10.DataSource = this.SetDS;
            this.dPosEdit10.DefaultValue = null;
            this.dPosEdit10.EditColor = System.Drawing.Color.Black;
            this.dPosEdit10.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit10.EditWidth = 100;
            this.dPosEdit10.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit10.IsModified = false;
            this.dPosEdit10.Location = new System.Drawing.Point(17, 36);
            this.dPosEdit10.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit10.MaxValue = 9999999D;
            this.dPosEdit10.MinValue = -9999999D;
            this.dPosEdit10.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit10.MotorP = this.MT_KSM_ShuttleA;
            this.dPosEdit10.Name = "dPosEdit10";
            this.dPosEdit10.NoChangeInAuto = false;
            this.dPosEdit10.PosValue = "";
            this.dPosEdit10.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit10.StepValue = 0D;
            this.dPosEdit10.TabIndex = 39;
            this.dPosEdit10.Unit = "um";
            this.dPosEdit10.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit10.UnitWidth = 40;
            this.dPosEdit10.ValueType = KCSDK.ValueDataType.Int;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dPosEdit4);
            this.groupBox2.Controls.Add(this.dPosEdit5);
            this.groupBox2.Controls.Add(this.dPosEdit6);
            this.groupBox2.Controls.Add(this.dPosEdit7);
            this.groupBox2.Location = new System.Drawing.Point(465, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(381, 164);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Transfer ShuttleB";
            // 
            // dPosEdit4
            // 
            this.dPosEdit4.AutoFocus = false;
            this.dPosEdit4.Caption = "BowlFeeder HeadB Y";
            this.dPosEdit4.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit4.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit4.DataName = "Pos_ShuttleB_BFM_B_Y";
            this.dPosEdit4.DataSource = this.SetDS;
            this.dPosEdit4.DefaultValue = null;
            this.dPosEdit4.EditColor = System.Drawing.Color.Black;
            this.dPosEdit4.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit4.EditWidth = 100;
            this.dPosEdit4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit4.IsModified = false;
            this.dPosEdit4.Location = new System.Drawing.Point(17, 111);
            this.dPosEdit4.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit4.MaxValue = 9999999D;
            this.dPosEdit4.MinValue = -9999999D;
            this.dPosEdit4.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit4.MotorP = this.MT_KSM_ShuttleB;
            this.dPosEdit4.Name = "dPosEdit4";
            this.dPosEdit4.NoChangeInAuto = false;
            this.dPosEdit4.PosValue = "";
            this.dPosEdit4.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit4.StepValue = 0D;
            this.dPosEdit4.TabIndex = 42;
            this.dPosEdit4.Unit = "um";
            this.dPosEdit4.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit4.UnitWidth = 40;
            this.dPosEdit4.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit5
            // 
            this.dPosEdit5.AutoFocus = false;
            this.dPosEdit5.Caption = "BowlFeeder HeadA Y";
            this.dPosEdit5.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit5.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit5.DataName = "Pos_ShuttleB_BFM_A_Y";
            this.dPosEdit5.DataSource = this.SetDS;
            this.dPosEdit5.DefaultValue = null;
            this.dPosEdit5.EditColor = System.Drawing.Color.Black;
            this.dPosEdit5.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit5.EditWidth = 100;
            this.dPosEdit5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit5.IsModified = false;
            this.dPosEdit5.Location = new System.Drawing.Point(17, 86);
            this.dPosEdit5.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit5.MaxValue = 9999999D;
            this.dPosEdit5.MinValue = -9999999D;
            this.dPosEdit5.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit5.MotorP = this.MT_KSM_ShuttleB;
            this.dPosEdit5.Name = "dPosEdit5";
            this.dPosEdit5.NoChangeInAuto = false;
            this.dPosEdit5.PosValue = "";
            this.dPosEdit5.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit5.StepValue = 0D;
            this.dPosEdit5.TabIndex = 41;
            this.dPosEdit5.Unit = "um";
            this.dPosEdit5.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit5.UnitWidth = 40;
            this.dPosEdit5.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit6
            // 
            this.dPosEdit6.AutoFocus = false;
            this.dPosEdit6.Caption = "Board HeadB Y";
            this.dPosEdit6.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit6.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit6.DataName = "Pos_ShuttleB_HDT_B_Y";
            this.dPosEdit6.DataSource = this.SetDS;
            this.dPosEdit6.DefaultValue = null;
            this.dPosEdit6.EditColor = System.Drawing.Color.Black;
            this.dPosEdit6.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit6.EditWidth = 100;
            this.dPosEdit6.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit6.IsModified = false;
            this.dPosEdit6.Location = new System.Drawing.Point(17, 61);
            this.dPosEdit6.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit6.MaxValue = 9999999D;
            this.dPosEdit6.MinValue = -9999999D;
            this.dPosEdit6.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit6.MotorP = this.MT_KSM_ShuttleB;
            this.dPosEdit6.Name = "dPosEdit6";
            this.dPosEdit6.NoChangeInAuto = false;
            this.dPosEdit6.PosValue = "";
            this.dPosEdit6.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit6.StepValue = 0D;
            this.dPosEdit6.TabIndex = 40;
            this.dPosEdit6.Unit = "um";
            this.dPosEdit6.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit6.UnitWidth = 40;
            this.dPosEdit6.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit7
            // 
            this.dPosEdit7.AutoFocus = false;
            this.dPosEdit7.Caption = "Board HeadA Y";
            this.dPosEdit7.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit7.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit7.DataName = "Pos_ShuttleB_HDT_A_Y";
            this.dPosEdit7.DataSource = this.SetDS;
            this.dPosEdit7.DefaultValue = null;
            this.dPosEdit7.EditColor = System.Drawing.Color.Black;
            this.dPosEdit7.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit7.EditWidth = 100;
            this.dPosEdit7.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit7.IsModified = false;
            this.dPosEdit7.Location = new System.Drawing.Point(17, 36);
            this.dPosEdit7.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit7.MaxValue = 9999999D;
            this.dPosEdit7.MinValue = -9999999D;
            this.dPosEdit7.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit7.MotorP = this.MT_KSM_ShuttleB;
            this.dPosEdit7.Name = "dPosEdit7";
            this.dPosEdit7.NoChangeInAuto = false;
            this.dPosEdit7.PosValue = "";
            this.dPosEdit7.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit7.StepValue = 0D;
            this.dPosEdit7.TabIndex = 39;
            this.dPosEdit7.Unit = "um";
            this.dPosEdit7.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit7.UnitWidth = 40;
            this.dPosEdit7.ValueType = KCSDK.ValueDataType.Int;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.dFieldEdit4);
            this.groupBox11.Controls.Add(this.dFieldEdit9);
            this.groupBox11.Controls.Add(this.dFieldEdit10);
            this.groupBox11.Controls.Add(this.dFieldEdit11);
            this.groupBox11.Location = new System.Drawing.Point(31, 92);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(627, 218);
            this.groupBox11.TabIndex = 14;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Motor Speed and Acc/Dec";
            // 
            // dFieldEdit4
            // 
            this.dFieldEdit4.AutoFocus = false;
            this.dFieldEdit4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dFieldEdit4.Caption = "ShuttleB AxisY Acc/Dec =";
            this.dFieldEdit4.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit4.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit4.DataName = "MT_KSM_ShuttleB_Y_ACC_MULTIPLE";
            this.dFieldEdit4.DataSource = this.SetDS;
            this.dFieldEdit4.DefaultValue = null;
            this.dFieldEdit4.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit4.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit4.EditWidth = 100;
            this.dFieldEdit4.FieldValue = "";
            this.dFieldEdit4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit4.IsModified = false;
            this.dFieldEdit4.Location = new System.Drawing.Point(7, 122);
            this.dFieldEdit4.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit4.MaxValue = 20D;
            this.dFieldEdit4.MinValue = 1D;
            this.dFieldEdit4.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit4.Name = "dFieldEdit4";
            this.dFieldEdit4.NoChangeInAuto = false;
            this.dFieldEdit4.Size = new System.Drawing.Size(480, 29);
            this.dFieldEdit4.StepValue = 0D;
            this.dFieldEdit4.TabIndex = 3;
            this.dFieldEdit4.Unit = "times of speed";
            this.dFieldEdit4.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit4.UnitWidth = 150;
            this.dFieldEdit4.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit9
            // 
            this.dFieldEdit9.AutoFocus = false;
            this.dFieldEdit9.Caption = "ShuttleB AxisY Speed =";
            this.dFieldEdit9.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit9.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit9.DataName = "MT_KSM_ShuttleB_Y_SPEED";
            this.dFieldEdit9.DataSource = this.SetDS;
            this.dFieldEdit9.DefaultValue = null;
            this.dFieldEdit9.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit9.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit9.EditWidth = 100;
            this.dFieldEdit9.FieldValue = "";
            this.dFieldEdit9.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit9.IsModified = false;
            this.dFieldEdit9.Location = new System.Drawing.Point(7, 93);
            this.dFieldEdit9.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit9.MaxValue = 2000000D;
            this.dFieldEdit9.MinValue = 10000D;
            this.dFieldEdit9.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit9.Name = "dFieldEdit9";
            this.dFieldEdit9.NoChangeInAuto = false;
            this.dFieldEdit9.Size = new System.Drawing.Size(389, 29);
            this.dFieldEdit9.StepValue = 0D;
            this.dFieldEdit9.TabIndex = 2;
            this.dFieldEdit9.Unit = "um/s";
            this.dFieldEdit9.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit9.UnitWidth = 60;
            this.dFieldEdit9.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit10
            // 
            this.dFieldEdit10.AutoFocus = false;
            this.dFieldEdit10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dFieldEdit10.Caption = "ShuttleA AxisY Acc/Dec =";
            this.dFieldEdit10.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit10.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit10.DataName = "MT_KSM_ShuttleA_Y_ACC_MULTIPLE";
            this.dFieldEdit10.DataSource = this.SetDS;
            this.dFieldEdit10.DefaultValue = null;
            this.dFieldEdit10.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit10.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit10.EditWidth = 100;
            this.dFieldEdit10.FieldValue = "";
            this.dFieldEdit10.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit10.IsModified = false;
            this.dFieldEdit10.Location = new System.Drawing.Point(7, 64);
            this.dFieldEdit10.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit10.MaxValue = 20D;
            this.dFieldEdit10.MinValue = 1D;
            this.dFieldEdit10.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit10.Name = "dFieldEdit10";
            this.dFieldEdit10.NoChangeInAuto = false;
            this.dFieldEdit10.Size = new System.Drawing.Size(480, 29);
            this.dFieldEdit10.StepValue = 0D;
            this.dFieldEdit10.TabIndex = 1;
            this.dFieldEdit10.Unit = "times of speed";
            this.dFieldEdit10.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit10.UnitWidth = 150;
            this.dFieldEdit10.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit11
            // 
            this.dFieldEdit11.AutoFocus = false;
            this.dFieldEdit11.Caption = "ShuttleA AxisY Speed =";
            this.dFieldEdit11.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit11.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit11.DataName = "MT_KSM_ShuttleA_Y_SPEED";
            this.dFieldEdit11.DataSource = this.SetDS;
            this.dFieldEdit11.DefaultValue = null;
            this.dFieldEdit11.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit11.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit11.EditWidth = 100;
            this.dFieldEdit11.FieldValue = "";
            this.dFieldEdit11.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit11.IsModified = false;
            this.dFieldEdit11.Location = new System.Drawing.Point(7, 35);
            this.dFieldEdit11.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit11.MaxValue = 2000000D;
            this.dFieldEdit11.MinValue = 10000D;
            this.dFieldEdit11.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit11.Name = "dFieldEdit11";
            this.dFieldEdit11.NoChangeInAuto = false;
            this.dFieldEdit11.Size = new System.Drawing.Size(389, 29);
            this.dFieldEdit11.StepValue = 0D;
            this.dFieldEdit11.TabIndex = 0;
            this.dFieldEdit11.Unit = "um/s";
            this.dFieldEdit11.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit11.UnitWidth = 60;
            this.dFieldEdit11.ValueType = KCSDK.ValueDataType.Int;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.dCheckBox2);
            this.groupBox12.Controls.Add(this.dCheckBox1);
            this.groupBox12.Location = new System.Drawing.Point(6, 64);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(400, 170);
            this.groupBox12.TabIndex = 22;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Option";
            // 
            // dCheckBox2
            // 
            this.dCheckBox2.AutoSize = true;
            this.dCheckBox2.BackColor = System.Drawing.SystemColors.Control;
            this.dCheckBox2.DataName = "DoNotUseRightShuttle";
            this.dCheckBox2.DataSource = this.SetDS;
            this.dCheckBox2.DefaultValue = false;
            this.dCheckBox2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dCheckBox2.IsModified = false;
            this.dCheckBox2.Location = new System.Drawing.Point(6, 65);
            this.dCheckBox2.ModifiedColor = System.Drawing.Color.Aqua;
            this.dCheckBox2.Name = "dCheckBox2";
            this.dCheckBox2.NoChangeInAuto = false;
            this.dCheckBox2.Size = new System.Drawing.Size(215, 25);
            this.dCheckBox2.TabIndex = 19;
            this.dCheckBox2.Text = "Do not use Right Shuttle";
            this.dCheckBox2.UseVisualStyleBackColor = true;
            // 
            // dCheckBox1
            // 
            this.dCheckBox1.AutoSize = true;
            this.dCheckBox1.BackColor = System.Drawing.SystemColors.Control;
            this.dCheckBox1.DataName = "DoNotUseLeftShuttle";
            this.dCheckBox1.DataSource = this.SetDS;
            this.dCheckBox1.DefaultValue = false;
            this.dCheckBox1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dCheckBox1.IsModified = false;
            this.dCheckBox1.Location = new System.Drawing.Point(6, 34);
            this.dCheckBox1.ModifiedColor = System.Drawing.Color.Aqua;
            this.dCheckBox1.Name = "dCheckBox1";
            this.dCheckBox1.NoChangeInAuto = false;
            this.dCheckBox1.Size = new System.Drawing.Size(203, 25);
            this.dCheckBox1.TabIndex = 18;
            this.dCheckBox1.Text = "Do not use Left Shuttle";
            this.dCheckBox1.UseVisualStyleBackColor = true;
            // 
            // KSM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 741);
            this.Name = "KSM";
            this.Text = "KSM";
            this.tabMain.ResumeLayout(false);
            this.tpControl.ResumeLayout(false);
            this.tpPosition.ResumeLayout(false);
            this.tpSetting.ResumeLayout(false);
            this.tpFlow.ResumeLayout(false);
            this.tpSuperSetting.ResumeLayout(false);
            this.TabFlow.ResumeLayout(false);
            this.tpHome.ResumeLayout(false);
            this.tpAuto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button button1;
        private ProVLib.Motor MT_KSM_ShuttleA;
        private System.Windows.Forms.Panel panel1;
        private ProVLib.Motor MT_KSM_ShuttleB;
        private System.Windows.Forms.Button button2;
        private ProVLib.FlowChart flowChart3;
        private ProVLib.FlowChart flowChart2;
        private ProVLib.FlowChart flowChart1;
        private ProVLib.FlowChart FC_KSM_HOME;
        private ProVLib.FlowChart FC_KSM_ShuttleB_AUTORUN;
        private ProVLib.FlowChart FC_KSM_ShuttleA_AUTORUN;
        private ProVLib.FlowChart flowChart11;
        private ProVLib.FlowChart flowChart7;
        private ProVLib.FlowChart flowChart8;
        private ProVLib.FlowChart flowChart9;
        private ProVLib.FlowChart flowChart10;
        private ProVLib.FlowChart flowChart4;
        private ProVLib.FlowChart flowChart5;
        private ProVLib.FlowChart flowChart6;
        private System.Windows.Forms.GroupBox groupBox1;
        private KCSDK.DPosEdit dPosEdit3;
        private KCSDK.DPosEdit dPosEdit2;
        private KCSDK.DPosEdit dPosEdit1;
        private KCSDK.DPosEdit dPosEdit10;
        private System.Windows.Forms.GroupBox groupBox2;
        private KCSDK.DPosEdit dPosEdit4;
        private KCSDK.DPosEdit dPosEdit5;
        private KCSDK.DPosEdit dPosEdit6;
        private KCSDK.DPosEdit dPosEdit7;
        private System.Windows.Forms.GroupBox groupBox11;
        private KCSDK.DFieldEdit dFieldEdit4;
        private KCSDK.DFieldEdit dFieldEdit9;
        private KCSDK.DFieldEdit dFieldEdit10;
        private KCSDK.DFieldEdit dFieldEdit11;
        private System.Windows.Forms.GroupBox groupBox12;
        private KCSDK.DCheckBox dCheckBox2;
        private KCSDK.DCheckBox dCheckBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;



    }
}