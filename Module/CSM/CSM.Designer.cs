namespace CSM
{
    partial class CSM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CSM));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.IB_CassetteBackDetect_B = new ProVLib.InBit();
            this.IB_CassetteFrontDetect_B = new ProVLib.InBit();
            this.panel6 = new System.Windows.Forms.Panel();
            this.IB_BIBSafeDetect_B = new ProVLib.InBit();
            this.IB_HaveBIBDetect_B = new ProVLib.InBit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.IB_CassetteBackDetect_A = new ProVLib.InBit();
            this.IB_CassetteFrontDetect_A = new ProVLib.InBit();
            this.panel11 = new System.Windows.Forms.Panel();
            this.IB_BIBSafeDetect_A = new ProVLib.InBit();
            this.IB_HaveBIBDetect_A = new ProVLib.InBit();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.dCheckBox3 = new KCSDK.DCheckBox(this.components);
            this.dCheckBox2 = new KCSDK.DCheckBox(this.components);
            this.dCheckBox1 = new KCSDK.DCheckBox(this.components);
            this.tabMain.SuspendLayout();
            this.tpControl.SuspendLayout();
            this.tpSetting.SuspendLayout();
            this.tpFlow.SuspendLayout();
            this.TabFlow.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel11.SuspendLayout();
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
            this.tabMain.Size = new System.Drawing.Size(909, 539);
            // 
            // tpControl
            // 
            this.tpControl.Controls.Add(this.groupBox2);
            this.tpControl.Controls.Add(this.groupBox1);
            this.tpControl.Size = new System.Drawing.Size(901, 471);
            // 
            // tpSetting
            // 
            this.tpSetting.Controls.Add(this.groupBox12);
            this.tpSetting.Controls.SetChildIndex(this.groupBox12, 0);
            // 
            // tpFlow
            // 
            this.tpFlow.Size = new System.Drawing.Size(901, 471);
            // 
            // tpSuperSetting
            // 
            this.tpSuperSetting.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // TabFlow
            // 
            this.TabFlow.Size = new System.Drawing.Size(897, 467);
            // 
            // tpHome
            // 
            this.tpHome.Size = new System.Drawing.Size(889, 425);
            // 
            // tpAuto
            // 
            this.tpAuto.Location = new System.Drawing.Point(4, 38);
            this.tpAuto.Size = new System.Drawing.Size(889, 425);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.panel6);
            this.groupBox2.Location = new System.Drawing.Point(315, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(240, 217);
            this.groupBox2.TabIndex = 116;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Right Load/Unload";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.IB_CassetteBackDetect_B);
            this.panel2.Controls.Add(this.IB_CassetteFrontDetect_B);
            this.panel2.Location = new System.Drawing.Point(11, 122);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(213, 79);
            this.panel2.TabIndex = 111;
            // 
            // IB_CassetteBackDetect_B
            // 
            this.IB_CassetteBackDetect_B.BackColor = System.Drawing.Color.RoyalBlue;
            this.IB_CassetteBackDetect_B.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.IB_CassetteBackDetect_B.ErrID = 0;
            this.IB_CassetteBackDetect_B.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.IB_CassetteBackDetect_B.InAlarm = false;
            this.IB_CassetteBackDetect_B.IOPort = "0117";
            this.IB_CassetteBackDetect_B.IOType = ProVLib.EIOType.IOHSL;
            this.IB_CassetteBackDetect_B.Location = new System.Drawing.Point(5, 40);
            this.IB_CassetteBackDetect_B.LockUI = false;
            this.IB_CassetteBackDetect_B.Message = null;
            this.IB_CassetteBackDetect_B.MsgID = 0;
            this.IB_CassetteBackDetect_B.Name = "IB_CassetteBackDetect_B";
            this.IB_CassetteBackDetect_B.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.IB_CassetteBackDetect_B.Running = false;
            this.IB_CassetteBackDetect_B.Simu_Mode = ProVLib.SIMULATION_MODE.S_RANDOM;
            this.IB_CassetteBackDetect_B.Simu_OnOffCondition = ProVLib.SIMULATION_ONOFFCONDITION.SRR_KEEP;
            this.IB_CassetteBackDetect_B.Simu_OutPort1 = null;
            this.IB_CassetteBackDetect_B.Simu_OutPort2 = null;
            this.IB_CassetteBackDetect_B.Simu_RandomNum = 2;
            this.IB_CassetteBackDetect_B.Simu_RandomTime = 100;
            this.IB_CassetteBackDetect_B.Simu_ReflectDelayTm = 100;
            this.IB_CassetteBackDetect_B.Simu_ReflectRule = ProVLib.SIMULATION_REFLECTRULE.SRR_ON_OFF;
            this.IB_CassetteBackDetect_B.Simu_Reverse = false;
            this.IB_CassetteBackDetect_B.Size = new System.Drawing.Size(200, 30);
            this.IB_CassetteBackDetect_B.Text = "Cassette Detect(Back)";
            // 
            // IB_CassetteFrontDetect_B
            // 
            this.IB_CassetteFrontDetect_B.BackColor = System.Drawing.Color.RoyalBlue;
            this.IB_CassetteFrontDetect_B.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.IB_CassetteFrontDetect_B.ErrID = 0;
            this.IB_CassetteFrontDetect_B.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.IB_CassetteFrontDetect_B.InAlarm = false;
            this.IB_CassetteFrontDetect_B.IOPort = "0115";
            this.IB_CassetteFrontDetect_B.IOType = ProVLib.EIOType.IOHSL;
            this.IB_CassetteFrontDetect_B.Location = new System.Drawing.Point(5, 6);
            this.IB_CassetteFrontDetect_B.LockUI = false;
            this.IB_CassetteFrontDetect_B.Message = null;
            this.IB_CassetteFrontDetect_B.MsgID = 0;
            this.IB_CassetteFrontDetect_B.Name = "IB_CassetteFrontDetect_B";
            this.IB_CassetteFrontDetect_B.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.IB_CassetteFrontDetect_B.Running = false;
            this.IB_CassetteFrontDetect_B.Simu_Mode = ProVLib.SIMULATION_MODE.S_RANDOM;
            this.IB_CassetteFrontDetect_B.Simu_OnOffCondition = ProVLib.SIMULATION_ONOFFCONDITION.SRR_KEEP;
            this.IB_CassetteFrontDetect_B.Simu_OutPort1 = null;
            this.IB_CassetteFrontDetect_B.Simu_OutPort2 = null;
            this.IB_CassetteFrontDetect_B.Simu_RandomNum = 2;
            this.IB_CassetteFrontDetect_B.Simu_RandomTime = 100;
            this.IB_CassetteFrontDetect_B.Simu_ReflectDelayTm = 100;
            this.IB_CassetteFrontDetect_B.Simu_ReflectRule = ProVLib.SIMULATION_REFLECTRULE.SRR_ON_OFF;
            this.IB_CassetteFrontDetect_B.Simu_Reverse = false;
            this.IB_CassetteFrontDetect_B.Size = new System.Drawing.Size(200, 30);
            this.IB_CassetteFrontDetect_B.Text = "Cassette Detect(Front)";
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.IB_BIBSafeDetect_B);
            this.panel6.Controls.Add(this.IB_HaveBIBDetect_B);
            this.panel6.Location = new System.Drawing.Point(11, 33);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(213, 79);
            this.panel6.TabIndex = 109;
            // 
            // IB_BIBSafeDetect_B
            // 
            this.IB_BIBSafeDetect_B.BackColor = System.Drawing.Color.RoyalBlue;
            this.IB_BIBSafeDetect_B.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.IB_BIBSafeDetect_B.ErrID = 0;
            this.IB_BIBSafeDetect_B.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.IB_BIBSafeDetect_B.InAlarm = false;
            this.IB_BIBSafeDetect_B.IOPort = "0113";
            this.IB_BIBSafeDetect_B.IOType = ProVLib.EIOType.IOHSL;
            this.IB_BIBSafeDetect_B.Location = new System.Drawing.Point(5, 40);
            this.IB_BIBSafeDetect_B.LockUI = false;
            this.IB_BIBSafeDetect_B.Message = null;
            this.IB_BIBSafeDetect_B.MsgID = 0;
            this.IB_BIBSafeDetect_B.Name = "IB_BIBSafeDetect_B";
            this.IB_BIBSafeDetect_B.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.IB_BIBSafeDetect_B.Running = false;
            this.IB_BIBSafeDetect_B.Simu_Mode = ProVLib.SIMULATION_MODE.S_RANDOM;
            this.IB_BIBSafeDetect_B.Simu_OnOffCondition = ProVLib.SIMULATION_ONOFFCONDITION.SRR_KEEP;
            this.IB_BIBSafeDetect_B.Simu_OutPort1 = null;
            this.IB_BIBSafeDetect_B.Simu_OutPort2 = null;
            this.IB_BIBSafeDetect_B.Simu_RandomNum = 2;
            this.IB_BIBSafeDetect_B.Simu_RandomTime = 100;
            this.IB_BIBSafeDetect_B.Simu_ReflectDelayTm = 100;
            this.IB_BIBSafeDetect_B.Simu_ReflectRule = ProVLib.SIMULATION_REFLECTRULE.SRR_ON_OFF;
            this.IB_BIBSafeDetect_B.Simu_Reverse = false;
            this.IB_BIBSafeDetect_B.Size = new System.Drawing.Size(200, 30);
            this.IB_BIBSafeDetect_B.Text = "BIB Safe Detect";
            // 
            // IB_HaveBIBDetect_B
            // 
            this.IB_HaveBIBDetect_B.BackColor = System.Drawing.Color.RoyalBlue;
            this.IB_HaveBIBDetect_B.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.IB_HaveBIBDetect_B.ErrID = 0;
            this.IB_HaveBIBDetect_B.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.IB_HaveBIBDetect_B.InAlarm = false;
            this.IB_HaveBIBDetect_B.IOPort = "0111";
            this.IB_HaveBIBDetect_B.IOType = ProVLib.EIOType.IOHSL;
            this.IB_HaveBIBDetect_B.Location = new System.Drawing.Point(5, 6);
            this.IB_HaveBIBDetect_B.LockUI = false;
            this.IB_HaveBIBDetect_B.Message = null;
            this.IB_HaveBIBDetect_B.MsgID = 0;
            this.IB_HaveBIBDetect_B.Name = "IB_HaveBIBDetect_B";
            this.IB_HaveBIBDetect_B.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.IB_HaveBIBDetect_B.Running = false;
            this.IB_HaveBIBDetect_B.Simu_Mode = ProVLib.SIMULATION_MODE.S_RANDOM;
            this.IB_HaveBIBDetect_B.Simu_OnOffCondition = ProVLib.SIMULATION_ONOFFCONDITION.SRR_KEEP;
            this.IB_HaveBIBDetect_B.Simu_OutPort1 = null;
            this.IB_HaveBIBDetect_B.Simu_OutPort2 = null;
            this.IB_HaveBIBDetect_B.Simu_RandomNum = 2;
            this.IB_HaveBIBDetect_B.Simu_RandomTime = 100;
            this.IB_HaveBIBDetect_B.Simu_ReflectDelayTm = 100;
            this.IB_HaveBIBDetect_B.Simu_ReflectRule = ProVLib.SIMULATION_REFLECTRULE.SRR_ON_OFF;
            this.IB_HaveBIBDetect_B.Simu_Reverse = false;
            this.IB_HaveBIBDetect_B.Size = new System.Drawing.Size(200, 30);
            this.IB_HaveBIBDetect_B.Text = "Have BIB Detect";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.panel11);
            this.groupBox1.Location = new System.Drawing.Point(30, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 217);
            this.groupBox1.TabIndex = 115;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Left Load/Unload";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.IB_CassetteBackDetect_A);
            this.panel1.Controls.Add(this.IB_CassetteFrontDetect_A);
            this.panel1.Location = new System.Drawing.Point(11, 123);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(213, 79);
            this.panel1.TabIndex = 110;
            // 
            // IB_CassetteBackDetect_A
            // 
            this.IB_CassetteBackDetect_A.BackColor = System.Drawing.Color.RoyalBlue;
            this.IB_CassetteBackDetect_A.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.IB_CassetteBackDetect_A.ErrID = 0;
            this.IB_CassetteBackDetect_A.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.IB_CassetteBackDetect_A.InAlarm = false;
            this.IB_CassetteBackDetect_A.IOPort = "0116";
            this.IB_CassetteBackDetect_A.IOType = ProVLib.EIOType.IOHSL;
            this.IB_CassetteBackDetect_A.Location = new System.Drawing.Point(5, 40);
            this.IB_CassetteBackDetect_A.LockUI = false;
            this.IB_CassetteBackDetect_A.Message = null;
            this.IB_CassetteBackDetect_A.MsgID = 0;
            this.IB_CassetteBackDetect_A.Name = "IB_CassetteBackDetect_A";
            this.IB_CassetteBackDetect_A.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.IB_CassetteBackDetect_A.Running = false;
            this.IB_CassetteBackDetect_A.Simu_Mode = ProVLib.SIMULATION_MODE.S_RANDOM;
            this.IB_CassetteBackDetect_A.Simu_OnOffCondition = ProVLib.SIMULATION_ONOFFCONDITION.SRR_KEEP;
            this.IB_CassetteBackDetect_A.Simu_OutPort1 = null;
            this.IB_CassetteBackDetect_A.Simu_OutPort2 = null;
            this.IB_CassetteBackDetect_A.Simu_RandomNum = 2;
            this.IB_CassetteBackDetect_A.Simu_RandomTime = 100;
            this.IB_CassetteBackDetect_A.Simu_ReflectDelayTm = 100;
            this.IB_CassetteBackDetect_A.Simu_ReflectRule = ProVLib.SIMULATION_REFLECTRULE.SRR_ON_OFF;
            this.IB_CassetteBackDetect_A.Simu_Reverse = false;
            this.IB_CassetteBackDetect_A.Size = new System.Drawing.Size(200, 30);
            this.IB_CassetteBackDetect_A.Text = "Cassette Detect(Back)";
            // 
            // IB_CassetteFrontDetect_A
            // 
            this.IB_CassetteFrontDetect_A.BackColor = System.Drawing.Color.RoyalBlue;
            this.IB_CassetteFrontDetect_A.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.IB_CassetteFrontDetect_A.ErrID = 0;
            this.IB_CassetteFrontDetect_A.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.IB_CassetteFrontDetect_A.InAlarm = false;
            this.IB_CassetteFrontDetect_A.IOPort = "0114";
            this.IB_CassetteFrontDetect_A.IOType = ProVLib.EIOType.IOHSL;
            this.IB_CassetteFrontDetect_A.Location = new System.Drawing.Point(5, 6);
            this.IB_CassetteFrontDetect_A.LockUI = false;
            this.IB_CassetteFrontDetect_A.Message = null;
            this.IB_CassetteFrontDetect_A.MsgID = 0;
            this.IB_CassetteFrontDetect_A.Name = "IB_CassetteFrontDetect_A";
            this.IB_CassetteFrontDetect_A.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.IB_CassetteFrontDetect_A.Running = false;
            this.IB_CassetteFrontDetect_A.Simu_Mode = ProVLib.SIMULATION_MODE.S_RANDOM;
            this.IB_CassetteFrontDetect_A.Simu_OnOffCondition = ProVLib.SIMULATION_ONOFFCONDITION.SRR_KEEP;
            this.IB_CassetteFrontDetect_A.Simu_OutPort1 = null;
            this.IB_CassetteFrontDetect_A.Simu_OutPort2 = null;
            this.IB_CassetteFrontDetect_A.Simu_RandomNum = 2;
            this.IB_CassetteFrontDetect_A.Simu_RandomTime = 100;
            this.IB_CassetteFrontDetect_A.Simu_ReflectDelayTm = 100;
            this.IB_CassetteFrontDetect_A.Simu_ReflectRule = ProVLib.SIMULATION_REFLECTRULE.SRR_ON_OFF;
            this.IB_CassetteFrontDetect_A.Simu_Reverse = false;
            this.IB_CassetteFrontDetect_A.Size = new System.Drawing.Size(200, 30);
            this.IB_CassetteFrontDetect_A.Text = "Cassette Detect(Front)";
            // 
            // panel11
            // 
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel11.Controls.Add(this.IB_BIBSafeDetect_A);
            this.panel11.Controls.Add(this.IB_HaveBIBDetect_A);
            this.panel11.Location = new System.Drawing.Point(11, 33);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(213, 79);
            this.panel11.TabIndex = 109;
            // 
            // IB_BIBSafeDetect_A
            // 
            this.IB_BIBSafeDetect_A.BackColor = System.Drawing.Color.RoyalBlue;
            this.IB_BIBSafeDetect_A.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.IB_BIBSafeDetect_A.ErrID = 0;
            this.IB_BIBSafeDetect_A.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.IB_BIBSafeDetect_A.InAlarm = false;
            this.IB_BIBSafeDetect_A.IOPort = "0112";
            this.IB_BIBSafeDetect_A.IOType = ProVLib.EIOType.IOHSL;
            this.IB_BIBSafeDetect_A.Location = new System.Drawing.Point(5, 40);
            this.IB_BIBSafeDetect_A.LockUI = false;
            this.IB_BIBSafeDetect_A.Message = null;
            this.IB_BIBSafeDetect_A.MsgID = 0;
            this.IB_BIBSafeDetect_A.Name = "IB_BIBSafeDetect_A";
            this.IB_BIBSafeDetect_A.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.IB_BIBSafeDetect_A.Running = false;
            this.IB_BIBSafeDetect_A.Simu_Mode = ProVLib.SIMULATION_MODE.S_RANDOM;
            this.IB_BIBSafeDetect_A.Simu_OnOffCondition = ProVLib.SIMULATION_ONOFFCONDITION.SRR_KEEP;
            this.IB_BIBSafeDetect_A.Simu_OutPort1 = null;
            this.IB_BIBSafeDetect_A.Simu_OutPort2 = null;
            this.IB_BIBSafeDetect_A.Simu_RandomNum = 2;
            this.IB_BIBSafeDetect_A.Simu_RandomTime = 100;
            this.IB_BIBSafeDetect_A.Simu_ReflectDelayTm = 100;
            this.IB_BIBSafeDetect_A.Simu_ReflectRule = ProVLib.SIMULATION_REFLECTRULE.SRR_ON_OFF;
            this.IB_BIBSafeDetect_A.Simu_Reverse = false;
            this.IB_BIBSafeDetect_A.Size = new System.Drawing.Size(200, 30);
            this.IB_BIBSafeDetect_A.Text = "BIB Safe Detect";
            // 
            // IB_HaveBIBDetect_A
            // 
            this.IB_HaveBIBDetect_A.BackColor = System.Drawing.Color.RoyalBlue;
            this.IB_HaveBIBDetect_A.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.IB_HaveBIBDetect_A.ErrID = 0;
            this.IB_HaveBIBDetect_A.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.IB_HaveBIBDetect_A.InAlarm = false;
            this.IB_HaveBIBDetect_A.IOPort = "0110";
            this.IB_HaveBIBDetect_A.IOType = ProVLib.EIOType.IOHSL;
            this.IB_HaveBIBDetect_A.Location = new System.Drawing.Point(5, 6);
            this.IB_HaveBIBDetect_A.LockUI = false;
            this.IB_HaveBIBDetect_A.Message = null;
            this.IB_HaveBIBDetect_A.MsgID = 0;
            this.IB_HaveBIBDetect_A.Name = "IB_HaveBIBDetect_A";
            this.IB_HaveBIBDetect_A.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.IB_HaveBIBDetect_A.Running = false;
            this.IB_HaveBIBDetect_A.Simu_Mode = ProVLib.SIMULATION_MODE.S_RANDOM;
            this.IB_HaveBIBDetect_A.Simu_OnOffCondition = ProVLib.SIMULATION_ONOFFCONDITION.SRR_KEEP;
            this.IB_HaveBIBDetect_A.Simu_OutPort1 = null;
            this.IB_HaveBIBDetect_A.Simu_OutPort2 = null;
            this.IB_HaveBIBDetect_A.Simu_RandomNum = 2;
            this.IB_HaveBIBDetect_A.Simu_RandomTime = 100;
            this.IB_HaveBIBDetect_A.Simu_ReflectDelayTm = 100;
            this.IB_HaveBIBDetect_A.Simu_ReflectRule = ProVLib.SIMULATION_REFLECTRULE.SRR_ON_OFF;
            this.IB_HaveBIBDetect_A.Simu_Reverse = false;
            this.IB_HaveBIBDetect_A.Size = new System.Drawing.Size(200, 30);
            this.IB_HaveBIBDetect_A.Text = "Have BIB Detect";
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.dCheckBox3);
            this.groupBox12.Controls.Add(this.dCheckBox2);
            this.groupBox12.Controls.Add(this.dCheckBox1);
            this.groupBox12.Location = new System.Drawing.Point(6, 64);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(400, 170);
            this.groupBox12.TabIndex = 23;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Option";
            // 
            // dCheckBox3
            // 
            this.dCheckBox3.AutoSize = true;
            this.dCheckBox3.BackColor = System.Drawing.SystemColors.Control;
            this.dCheckBox3.DataName = "RightInLeftOut";
            this.dCheckBox3.DataSource = this.SetDS;
            this.dCheckBox3.DefaultValue = false;
            this.dCheckBox3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dCheckBox3.IsModified = false;
            this.dCheckBox3.Location = new System.Drawing.Point(6, 109);
            this.dCheckBox3.ModifiedColor = System.Drawing.Color.Aqua;
            this.dCheckBox3.Name = "dCheckBox3";
            this.dCheckBox3.NoChangeInAuto = false;
            this.dCheckBox3.Size = new System.Drawing.Size(184, 25);
            this.dCheckBox3.TabIndex = 20;
            this.dCheckBox3.Text = "Right in and Left out";
            this.dCheckBox3.UseVisualStyleBackColor = true;
            // 
            // dCheckBox2
            // 
            this.dCheckBox2.AutoSize = true;
            this.dCheckBox2.BackColor = System.Drawing.SystemColors.Control;
            this.dCheckBox2.DataName = "DoNotUseRightCassette";
            this.dCheckBox2.DataSource = this.SetDS;
            this.dCheckBox2.DefaultValue = false;
            this.dCheckBox2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dCheckBox2.IsModified = false;
            this.dCheckBox2.Location = new System.Drawing.Point(6, 65);
            this.dCheckBox2.ModifiedColor = System.Drawing.Color.Aqua;
            this.dCheckBox2.Name = "dCheckBox2";
            this.dCheckBox2.NoChangeInAuto = false;
            this.dCheckBox2.Size = new System.Drawing.Size(227, 25);
            this.dCheckBox2.TabIndex = 19;
            this.dCheckBox2.Text = "Do not use Right Cassette";
            this.dCheckBox2.UseVisualStyleBackColor = true;
            // 
            // dCheckBox1
            // 
            this.dCheckBox1.AutoSize = true;
            this.dCheckBox1.BackColor = System.Drawing.SystemColors.Control;
            this.dCheckBox1.DataName = "DoNotUseLeftCassette";
            this.dCheckBox1.DataSource = this.SetDS;
            this.dCheckBox1.DefaultValue = false;
            this.dCheckBox1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dCheckBox1.IsModified = false;
            this.dCheckBox1.Location = new System.Drawing.Point(6, 34);
            this.dCheckBox1.ModifiedColor = System.Drawing.Color.Aqua;
            this.dCheckBox1.Name = "dCheckBox1";
            this.dCheckBox1.NoChangeInAuto = false;
            this.dCheckBox1.Size = new System.Drawing.Size(215, 25);
            this.dCheckBox1.TabIndex = 18;
            this.dCheckBox1.Text = "Do not use Left Cassette";
            this.dCheckBox1.UseVisualStyleBackColor = true;
            // 
            // CSM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 539);
            this.Name = "CSM";
            this.Text = "CSM";
            this.tabMain.ResumeLayout(false);
            this.tpControl.ResumeLayout(false);
            this.tpSetting.ResumeLayout(false);
            this.tpFlow.ResumeLayout(false);
            this.TabFlow.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel6;
        private ProVLib.InBit IB_HaveBIBDetect_B;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel11;
        private ProVLib.InBit IB_BIBSafeDetect_A;
        private ProVLib.InBit IB_HaveBIBDetect_A;
        private ProVLib.InBit IB_BIBSafeDetect_B;
        private System.Windows.Forms.Panel panel2;
        private ProVLib.InBit IB_CassetteBackDetect_B;
        private ProVLib.InBit IB_CassetteFrontDetect_B;
        private System.Windows.Forms.Panel panel1;
        private ProVLib.InBit IB_CassetteBackDetect_A;
        private ProVLib.InBit IB_CassetteFrontDetect_A;
        private System.Windows.Forms.GroupBox groupBox12;
        private KCSDK.DCheckBox dCheckBox3;
        private KCSDK.DCheckBox dCheckBox2;
        private KCSDK.DCheckBox dCheckBox1;


    }
}