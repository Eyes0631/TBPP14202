namespace HDT
{
    partial class HDT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HDT));
            ProVLib.MotorParam motorParam1 = new ProVLib.MotorParam();
            ProVLib.MotorParam motorParam2 = new ProVLib.MotorParam();
            ProVLib.MotorParam motorParam3 = new ProVLib.MotorParam();
            ProVLib.MotorParam motorParam4 = new ProVLib.MotorParam();
            ProVLib.MotorParam motorParam5 = new ProVLib.MotorParam();
            ProVLib.MotorParam motorParam6 = new ProVLib.MotorParam();
            ProVLib.MotorParam motorParam7 = new ProVLib.MotorParam();
            ProVLib.MotorParam motorParam8 = new ProVLib.MotorParam();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.IO_HDT_A = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button9 = new System.Windows.Forms.Button();
            this.HDT_A_ClientSocket = new ProVTool.ProVClientSocket();
            this.button10 = new System.Windows.Forms.Button();
            this.panel13 = new System.Windows.Forms.Panel();
            this.OB_HDT_A_Vacuum_1 = new ProVLib.OutBit();
            this.IB_HDT_A_VacDetect_4 = new ProVLib.InBit();
            this.OB_HDT_A_Vacuum_3 = new ProVLib.OutBit();
            this.IB_HDT_A_VacDetect_2 = new ProVLib.InBit();
            this.OB_HDT_A_Destroy_1 = new ProVLib.OutBit();
            this.OB_HDT_A_Destroy_4 = new ProVLib.OutBit();
            this.OB_HDT_A_Destroy_3 = new ProVLib.OutBit();
            this.OB_HDT_A_Destroy_2 = new ProVLib.OutBit();
            this.IB_HDT_A_VacDetect_1 = new ProVLib.InBit();
            this.OB_HDT_A_Vacuum_4 = new ProVLib.OutBit();
            this.IB_HDT_A_VacDetect_3 = new ProVLib.InBit();
            this.OB_HDT_A_Vacuum_2 = new ProVLib.OutBit();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.MT_CCD_A_AxisZ = new ProVLib.Motor();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.MT_HDT_A_AxisZ = new ProVLib.Motor();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MT_HDT_A_AxisX = new ProVLib.Motor();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.MT_HDT_A_AxisU = new ProVLib.Motor();
            this.IO_HDT_B = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button11 = new System.Windows.Forms.Button();
            this.HDT_B_ClientSocket = new ProVTool.ProVClientSocket();
            this.button12 = new System.Windows.Forms.Button();
            this.panel14 = new System.Windows.Forms.Panel();
            this.OB_HDT_B_Vacuum_1 = new ProVLib.OutBit();
            this.IB_HDT_B_VacDetect_4 = new ProVLib.InBit();
            this.OB_HDT_B_Vacuum_3 = new ProVLib.OutBit();
            this.IB_HDT_B_VacDetect_2 = new ProVLib.InBit();
            this.OB_HDT_B_Destroy_1 = new ProVLib.OutBit();
            this.OB_HDT_B_Destroy_4 = new ProVLib.OutBit();
            this.OB_HDT_B_Destroy_3 = new ProVLib.OutBit();
            this.OB_HDT_B_Destroy_2 = new ProVLib.OutBit();
            this.IB_HDT_B_VacDetect_1 = new ProVLib.InBit();
            this.OB_HDT_B_Vacuum_4 = new ProVLib.OutBit();
            this.IB_HDT_B_VacDetect_3 = new ProVLib.InBit();
            this.OB_HDT_B_Vacuum_2 = new ProVLib.OutBit();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.MT_CCD_B_AxisZ = new ProVLib.Motor();
            this.panel10 = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.MT_HDT_B_AxisZ = new ProVLib.Motor();
            this.panel11 = new System.Windows.Forms.Panel();
            this.MT_HDT_B_AxisX = new ProVLib.Motor();
            this.button7 = new System.Windows.Forms.Button();
            this.panel12 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.MT_HDT_B_AxisU = new ProVLib.Motor();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.FC_HDT_A = new System.Windows.Forms.TabPage();
            this.flowChart88 = new ProVLib.FlowChart();
            this.flowChart13 = new ProVLib.FlowChart();
            this.flowChart14 = new ProVLib.FlowChart();
            this.flowChart15 = new ProVLib.FlowChart();
            this.flowChart16 = new ProVLib.FlowChart();
            this.flowChart18 = new ProVLib.FlowChart();
            this.flowChart37 = new ProVLib.FlowChart();
            this.FC_HDT_A_AUTO_Pick = new ProVLib.FlowChart();
            this.flowChart84 = new ProVLib.FlowChart();
            this.flowChart10 = new ProVLib.FlowChart();
            this.flowChart11 = new ProVLib.FlowChart();
            this.flowChart12 = new ProVLib.FlowChart();
            this.flowChart86 = new ProVLib.FlowChart();
            this.flowChart40 = new ProVLib.FlowChart();
            this.flowChart41 = new ProVLib.FlowChart();
            this.flowChart42 = new ProVLib.FlowChart();
            this.flowChart43 = new ProVLib.FlowChart();
            this.flowChart44 = new ProVLib.FlowChart();
            this.flowChart45 = new ProVLib.FlowChart();
            this.flowChart39 = new ProVLib.FlowChart();
            this.flowChart38 = new ProVLib.FlowChart();
            this.FC_HDT_A_AUTO_Place = new ProVLib.FlowChart();
            this.flowChart19 = new ProVLib.FlowChart();
            this.flowChart20 = new ProVLib.FlowChart();
            this.flowChart21 = new ProVLib.FlowChart();
            this.flowChart22 = new ProVLib.FlowChart();
            this.flowChart23 = new ProVLib.FlowChart();
            this.flowChart24 = new ProVLib.FlowChart();
            this.flowChart25 = new ProVLib.FlowChart();
            this.flowChart29 = new ProVLib.FlowChart();
            this.flowChart30 = new ProVLib.FlowChart();
            this.flowChart31 = new ProVLib.FlowChart();
            this.flowChart32 = new ProVLib.FlowChart();
            this.flowChart33 = new ProVLib.FlowChart();
            this.flowChart34 = new ProVLib.FlowChart();
            this.flowChart35 = new ProVLib.FlowChart();
            this.flowChart36 = new ProVLib.FlowChart();
            this.FC_HDT_A_AUTO_Inspection = new ProVLib.FlowChart();
            this.flowChart17 = new ProVLib.FlowChart();
            this.flowChart26 = new ProVLib.FlowChart();
            this.flowChart27 = new ProVLib.FlowChart();
            this.flowChart28 = new ProVLib.FlowChart();
            this.FC_HDT_A_AUTO_Move = new ProVLib.FlowChart();
            this.FC_HDT_B = new System.Windows.Forms.TabPage();
            this.flowChart89 = new ProVLib.FlowChart();
            this.flowChart75 = new ProVLib.FlowChart();
            this.flowChart76 = new ProVLib.FlowChart();
            this.flowChart77 = new ProVLib.FlowChart();
            this.flowChart78 = new ProVLib.FlowChart();
            this.flowChart79 = new ProVLib.FlowChart();
            this.flowChart70 = new ProVLib.FlowChart();
            this.FC_HDT_B_AUTO_Pick = new ProVLib.FlowChart();
            this.flowChart85 = new ProVLib.FlowChart();
            this.flowChart72 = new ProVLib.FlowChart();
            this.flowChart73 = new ProVLib.FlowChart();
            this.flowChart74 = new ProVLib.FlowChart();
            this.flowChart87 = new ProVLib.FlowChart();
            this.flowChart48 = new ProVLib.FlowChart();
            this.flowChart49 = new ProVLib.FlowChart();
            this.flowChart50 = new ProVLib.FlowChart();
            this.flowChart51 = new ProVLib.FlowChart();
            this.flowChart52 = new ProVLib.FlowChart();
            this.flowChart46 = new ProVLib.FlowChart();
            this.flowChart47 = new ProVLib.FlowChart();
            this.flowChart53 = new ProVLib.FlowChart();
            this.FC_HDT_B_AUTO_Place = new ProVLib.FlowChart();
            this.flowChart55 = new ProVLib.FlowChart();
            this.flowChart56 = new ProVLib.FlowChart();
            this.flowChart57 = new ProVLib.FlowChart();
            this.flowChart58 = new ProVLib.FlowChart();
            this.flowChart59 = new ProVLib.FlowChart();
            this.flowChart60 = new ProVLib.FlowChart();
            this.flowChart61 = new ProVLib.FlowChart();
            this.flowChart62 = new ProVLib.FlowChart();
            this.flowChart63 = new ProVLib.FlowChart();
            this.flowChart64 = new ProVLib.FlowChart();
            this.flowChart65 = new ProVLib.FlowChart();
            this.flowChart66 = new ProVLib.FlowChart();
            this.flowChart67 = new ProVLib.FlowChart();
            this.flowChart68 = new ProVLib.FlowChart();
            this.flowChart69 = new ProVLib.FlowChart();
            this.FC_HDT_B_AUTO_Inspection = new ProVLib.FlowChart();
            this.flowChart81 = new ProVLib.FlowChart();
            this.flowChart54 = new ProVLib.FlowChart();
            this.flowChart71 = new ProVLib.FlowChart();
            this.flowChart80 = new ProVLib.FlowChart();
            this.FC_HDT_B_AUTO_Move = new ProVLib.FlowChart();
            this.FC_HDT_HOME = new ProVLib.FlowChart();
            this.flowChart1 = new ProVLib.FlowChart();
            this.flowChart2 = new ProVLib.FlowChart();
            this.flowChart3 = new ProVLib.FlowChart();
            this.flowChart4 = new ProVLib.FlowChart();
            this.flowChart5 = new ProVLib.FlowChart();
            this.flowChart6 = new ProVLib.FlowChart();
            this.flowChart7 = new ProVLib.FlowChart();
            this.flowChart83 = new ProVLib.FlowChart();
            this.flowChart8 = new ProVLib.FlowChart();
            this.flowChart82 = new ProVLib.FlowChart();
            this.flowChart9 = new ProVLib.FlowChart();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dPosEdit47 = new KCSDK.DPosEdit();
            this.dPosEdit30 = new KCSDK.DPosEdit();
            this.dPosEdit11 = new KCSDK.DPosEdit();
            this.dPosEdit12 = new KCSDK.DPosEdit();
            this.dPosEdit15 = new KCSDK.DPosEdit();
            this.dPosEdit10 = new KCSDK.DPosEdit();
            this.dPosEdit14 = new KCSDK.DPosEdit();
            this.dPosEdit4 = new KCSDK.DPosEdit();
            this.dPosEdit7 = new KCSDK.DPosEdit();
            this.dPosEdit8 = new KCSDK.DPosEdit();
            this.dPosEdit6 = new KCSDK.DPosEdit();
            this.dPosEdit5 = new KCSDK.DPosEdit();
            this.dPosEdit2 = new KCSDK.DPosEdit();
            this.dPosEdit3 = new KCSDK.DPosEdit();
            this.dPosEdit9 = new KCSDK.DPosEdit();
            this.dPosEdit1 = new KCSDK.DPosEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dPosEdit48 = new KCSDK.DPosEdit();
            this.dPosEdit29 = new KCSDK.DPosEdit();
            this.dPosEdit13 = new KCSDK.DPosEdit();
            this.dPosEdit16 = new KCSDK.DPosEdit();
            this.dPosEdit17 = new KCSDK.DPosEdit();
            this.dPosEdit18 = new KCSDK.DPosEdit();
            this.dPosEdit19 = new KCSDK.DPosEdit();
            this.dPosEdit20 = new KCSDK.DPosEdit();
            this.dPosEdit21 = new KCSDK.DPosEdit();
            this.dPosEdit22 = new KCSDK.DPosEdit();
            this.dPosEdit23 = new KCSDK.DPosEdit();
            this.dPosEdit24 = new KCSDK.DPosEdit();
            this.dPosEdit25 = new KCSDK.DPosEdit();
            this.dPosEdit26 = new KCSDK.DPosEdit();
            this.dPosEdit27 = new KCSDK.DPosEdit();
            this.dPosEdit28 = new KCSDK.DPosEdit();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.dFieldEdit7 = new KCSDK.DFieldEdit();
            this.dFieldEdit8 = new KCSDK.DFieldEdit();
            this.dFieldEdit9 = new KCSDK.DFieldEdit();
            this.dFieldEdit12 = new KCSDK.DFieldEdit();
            this.dFieldEdit13 = new KCSDK.DFieldEdit();
            this.dFieldEdit14 = new KCSDK.DFieldEdit();
            this.dFieldEdit15 = new KCSDK.DFieldEdit();
            this.dFieldEdit16 = new KCSDK.DFieldEdit();
            this.dFieldEdit5 = new KCSDK.DFieldEdit();
            this.dFieldEdit6 = new KCSDK.DFieldEdit();
            this.dFieldEdit3 = new KCSDK.DFieldEdit();
            this.dFieldEdit4 = new KCSDK.DFieldEdit();
            this.dFieldEdit1 = new KCSDK.DFieldEdit();
            this.dFieldEdit2 = new KCSDK.DFieldEdit();
            this.dFieldEdit10 = new KCSDK.DFieldEdit();
            this.dFieldEdit11 = new KCSDK.DFieldEdit();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dPosEdit31 = new KCSDK.DPosEdit();
            this.dPosEdit32 = new KCSDK.DPosEdit();
            this.dPosEdit33 = new KCSDK.DPosEdit();
            this.dPosEdit34 = new KCSDK.DPosEdit();
            this.dPosEdit35 = new KCSDK.DPosEdit();
            this.dPosEdit36 = new KCSDK.DPosEdit();
            this.dPosEdit43 = new KCSDK.DPosEdit();
            this.dPosEdit44 = new KCSDK.DPosEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dPosEdit37 = new KCSDK.DPosEdit();
            this.dPosEdit38 = new KCSDK.DPosEdit();
            this.dPosEdit39 = new KCSDK.DPosEdit();
            this.dPosEdit40 = new KCSDK.DPosEdit();
            this.dPosEdit41 = new KCSDK.DPosEdit();
            this.dPosEdit42 = new KCSDK.DPosEdit();
            this.dPosEdit45 = new KCSDK.DPosEdit();
            this.dPosEdit46 = new KCSDK.DPosEdit();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.dFieldEdit19 = new KCSDK.DFieldEdit();
            this.dFieldEdit20 = new KCSDK.DFieldEdit();
            this.dFieldEdit18 = new KCSDK.DFieldEdit();
            this.dFieldEdit17 = new KCSDK.DFieldEdit();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.dCheckBox4 = new KCSDK.DCheckBox(this.components);
            this.dCheckBox3 = new KCSDK.DCheckBox(this.components);
            this.dCheckBox2 = new KCSDK.DCheckBox(this.components);
            this.dCheckBox1 = new KCSDK.DCheckBox(this.components);
            this.tabMain.SuspendLayout();
            this.tpControl.SuspendLayout();
            this.tpPosition.SuspendLayout();
            this.tpSetting.SuspendLayout();
            this.tpFlow.SuspendLayout();
            this.tpSuperSetting.SuspendLayout();
            this.TabFlow.SuspendLayout();
            this.tpHome.SuspendLayout();
            this.tpAuto.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.IO_HDT_A.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.IO_HDT_B.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.panel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel12.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.FC_HDT_A.SuspendLayout();
            this.FC_HDT_B.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox17.SuspendLayout();
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
            this.tabMain.Size = new System.Drawing.Size(1084, 733);
            // 
            // tpControl
            // 
            this.tpControl.Controls.Add(this.tabControl1);
            this.tpControl.Size = new System.Drawing.Size(1076, 665);
            // 
            // tpPosition
            // 
            this.tpPosition.Controls.Add(this.tabControl3);
            this.tpPosition.Controls.SetChildIndex(this.tabControl3, 0);
            // 
            // tpSetting
            // 
            this.tpSetting.Controls.Add(this.groupBox12);
            this.tpSetting.Controls.SetChildIndex(this.groupBox12, 0);
            // 
            // tpFlow
            // 
            this.tpFlow.Size = new System.Drawing.Size(1076, 665);
            // 
            // tpSuperSetting
            // 
            this.tpSuperSetting.Controls.Add(this.groupBox17);
            this.tpSuperSetting.Controls.Add(this.groupBox11);
            this.tpSuperSetting.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tpSuperSetting.Controls.SetChildIndex(this.groupBox11, 0);
            this.tpSuperSetting.Controls.SetChildIndex(this.groupBox17, 0);
            // 
            // TabFlow
            // 
            this.TabFlow.Size = new System.Drawing.Size(1072, 661);
            // 
            // tpHome
            // 
            this.tpHome.Controls.Add(this.flowChart83);
            this.tpHome.Controls.Add(this.flowChart1);
            this.tpHome.Controls.Add(this.flowChart2);
            this.tpHome.Controls.Add(this.flowChart3);
            this.tpHome.Controls.Add(this.flowChart4);
            this.tpHome.Controls.Add(this.flowChart5);
            this.tpHome.Controls.Add(this.flowChart6);
            this.tpHome.Controls.Add(this.flowChart7);
            this.tpHome.Controls.Add(this.flowChart8);
            this.tpHome.Controls.Add(this.flowChart82);
            this.tpHome.Controls.Add(this.flowChart9);
            this.tpHome.Controls.Add(this.FC_HDT_HOME);
            this.tpHome.Size = new System.Drawing.Size(1064, 619);
            // 
            // tpAuto
            // 
            this.tpAuto.Controls.Add(this.tabControl2);
            this.tpAuto.Location = new System.Drawing.Point(4, 38);
            this.tpAuto.Size = new System.Drawing.Size(1064, 619);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.IO_HDT_A);
            this.tabControl1.Controls.Add(this.IO_HDT_B);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1071, 671);
            this.tabControl1.TabIndex = 0;
            // 
            // IO_HDT_A
            // 
            this.IO_HDT_A.Controls.Add(this.groupBox5);
            this.IO_HDT_A.Controls.Add(this.panel13);
            this.IO_HDT_A.Controls.Add(this.pictureBox1);
            this.IO_HDT_A.Controls.Add(this.panel8);
            this.IO_HDT_A.Location = new System.Drawing.Point(4, 30);
            this.IO_HDT_A.Name = "IO_HDT_A";
            this.IO_HDT_A.Padding = new System.Windows.Forms.Padding(3);
            this.IO_HDT_A.Size = new System.Drawing.Size(1063, 637);
            this.IO_HDT_A.TabIndex = 0;
            this.IO_HDT_A.Text = "HDT_A";
            this.IO_HDT_A.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button9);
            this.groupBox5.Controls.Add(this.HDT_A_ClientSocket);
            this.groupBox5.Controls.Add(this.button10);
            this.groupBox5.Location = new System.Drawing.Point(28, 200);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(225, 146);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "CCD Socket";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(138, 75);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 50);
            this.button9.TabIndex = 5;
            this.button9.Text = "斷線";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // HDT_A_ClientSocket
            // 
            this.HDT_A_ClientSocket.IP = null;
            this.HDT_A_ClientSocket.Location = new System.Drawing.Point(13, 29);
            this.HDT_A_ClientSocket.Name = "HDT_A_ClientSocket";
            this.HDT_A_ClientSocket.Port = 0;
            this.HDT_A_ClientSocket.Size = new System.Drawing.Size(200, 30);
            this.HDT_A_ClientSocket.Text = "CCD Client Socket";
            this.HDT_A_ClientSocket.OnConnect += new ProVTool.ProVClientSocket.SocketNotifyHandler(this.HDT_A_ClientSocket_OnConnect);
            this.HDT_A_ClientSocket.OnDisconnect += new ProVTool.ProVClientSocket.SocketDisconnectNotifyHandler(this.HDT_A_ClientSocket_OnDisconnect);
            this.HDT_A_ClientSocket.OnRead += new ProVTool.ProVClientSocket.SocketReadNotifyHandler(this.HDT_A_ClientSocket_OnRead);
            this.HDT_A_ClientSocket.OnError += new ProVTool.ProVClientSocket.ErrorNotifyHandler(this.HDT_A_ClientSocket_OnError);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(13, 75);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 50);
            this.button10.TabIndex = 4;
            this.button10.Text = "連線";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // panel13
            // 
            this.panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel13.Controls.Add(this.OB_HDT_A_Vacuum_1);
            this.panel13.Controls.Add(this.IB_HDT_A_VacDetect_4);
            this.panel13.Controls.Add(this.OB_HDT_A_Vacuum_3);
            this.panel13.Controls.Add(this.IB_HDT_A_VacDetect_2);
            this.panel13.Controls.Add(this.OB_HDT_A_Destroy_1);
            this.panel13.Controls.Add(this.OB_HDT_A_Destroy_4);
            this.panel13.Controls.Add(this.OB_HDT_A_Destroy_3);
            this.panel13.Controls.Add(this.OB_HDT_A_Destroy_2);
            this.panel13.Controls.Add(this.IB_HDT_A_VacDetect_1);
            this.panel13.Controls.Add(this.OB_HDT_A_Vacuum_4);
            this.panel13.Controls.Add(this.IB_HDT_A_VacDetect_3);
            this.panel13.Controls.Add(this.OB_HDT_A_Vacuum_2);
            this.panel13.Location = new System.Drawing.Point(28, 22);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(509, 158);
            this.panel13.TabIndex = 150;
            // 
            // OB_HDT_A_Vacuum_1
            // 
            this.OB_HDT_A_Vacuum_1.ActionCount = 0;
            this.OB_HDT_A_Vacuum_1.BackColor = System.Drawing.Color.RoyalBlue;
            this.OB_HDT_A_Vacuum_1.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OB_HDT_A_Vacuum_1.ErrID = 0;
            this.OB_HDT_A_Vacuum_1.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.OB_HDT_A_Vacuum_1.InAlarm = false;
            this.OB_HDT_A_Vacuum_1.IOPort = "2308";
            this.OB_HDT_A_Vacuum_1.IOType = ProVLib.EIOType.IOHSL;
            this.OB_HDT_A_Vacuum_1.IsUseActionCount = false;
            this.OB_HDT_A_Vacuum_1.Location = new System.Drawing.Point(173, 119);
            this.OB_HDT_A_Vacuum_1.LockUI = false;
            this.OB_HDT_A_Vacuum_1.Message = null;
            this.OB_HDT_A_Vacuum_1.MsgID = 0;
            this.OB_HDT_A_Vacuum_1.Name = "OB_HDT_A_Vacuum_1";
            this.OB_HDT_A_Vacuum_1.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.OB_HDT_A_Vacuum_1.RetryCount = 10;
            this.OB_HDT_A_Vacuum_1.Running = false;
            this.OB_HDT_A_Vacuum_1.Size = new System.Drawing.Size(160, 30);
            this.OB_HDT_A_Vacuum_1.Text = "Vaccum - 1";
            this.OB_HDT_A_Vacuum_1.Value = false;
            // 
            // IB_HDT_A_VacDetect_4
            // 
            this.IB_HDT_A_VacDetect_4.BackColor = System.Drawing.Color.RoyalBlue;
            this.IB_HDT_A_VacDetect_4.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.IB_HDT_A_VacDetect_4.ErrID = 0;
            this.IB_HDT_A_VacDetect_4.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.IB_HDT_A_VacDetect_4.InAlarm = false;
            this.IB_HDT_A_VacDetect_4.IOPort = "050F";
            this.IB_HDT_A_VacDetect_4.IOType = ProVLib.EIOType.IOHSL;
            this.IB_HDT_A_VacDetect_4.Location = new System.Drawing.Point(12, 5);
            this.IB_HDT_A_VacDetect_4.LockUI = false;
            this.IB_HDT_A_VacDetect_4.Message = null;
            this.IB_HDT_A_VacDetect_4.MsgID = 0;
            this.IB_HDT_A_VacDetect_4.Name = "IB_HDT_A_VacDetect_4";
            this.IB_HDT_A_VacDetect_4.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.IB_HDT_A_VacDetect_4.Running = false;
            this.IB_HDT_A_VacDetect_4.Simu_Mode = ProVLib.SIMULATION_MODE.S_RANDOM;
            this.IB_HDT_A_VacDetect_4.Simu_OnOffCondition = ProVLib.SIMULATION_ONOFFCONDITION.SRR_KEEP;
            this.IB_HDT_A_VacDetect_4.Simu_OutPort1 = null;
            this.IB_HDT_A_VacDetect_4.Simu_OutPort2 = null;
            this.IB_HDT_A_VacDetect_4.Simu_RandomNum = 2;
            this.IB_HDT_A_VacDetect_4.Simu_RandomTime = 100;
            this.IB_HDT_A_VacDetect_4.Simu_ReflectDelayTm = 100;
            this.IB_HDT_A_VacDetect_4.Simu_ReflectRule = ProVLib.SIMULATION_REFLECTRULE.SRR_ON_OFF;
            this.IB_HDT_A_VacDetect_4.Simu_Reverse = false;
            this.IB_HDT_A_VacDetect_4.Size = new System.Drawing.Size(160, 30);
            this.IB_HDT_A_VacDetect_4.Text = "Vac Detect - 4";
            // 
            // OB_HDT_A_Vacuum_3
            // 
            this.OB_HDT_A_Vacuum_3.ActionCount = 0;
            this.OB_HDT_A_Vacuum_3.BackColor = System.Drawing.Color.RoyalBlue;
            this.OB_HDT_A_Vacuum_3.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OB_HDT_A_Vacuum_3.ErrID = 0;
            this.OB_HDT_A_Vacuum_3.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.OB_HDT_A_Vacuum_3.InAlarm = false;
            this.OB_HDT_A_Vacuum_3.IOPort = "230C";
            this.OB_HDT_A_Vacuum_3.IOType = ProVLib.EIOType.IOHSL;
            this.OB_HDT_A_Vacuum_3.IsUseActionCount = false;
            this.OB_HDT_A_Vacuum_3.Location = new System.Drawing.Point(173, 43);
            this.OB_HDT_A_Vacuum_3.LockUI = false;
            this.OB_HDT_A_Vacuum_3.Message = null;
            this.OB_HDT_A_Vacuum_3.MsgID = 0;
            this.OB_HDT_A_Vacuum_3.Name = "OB_HDT_A_Vacuum_3";
            this.OB_HDT_A_Vacuum_3.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.OB_HDT_A_Vacuum_3.RetryCount = 10;
            this.OB_HDT_A_Vacuum_3.Running = false;
            this.OB_HDT_A_Vacuum_3.Size = new System.Drawing.Size(160, 30);
            this.OB_HDT_A_Vacuum_3.Text = "Vaccum - 3";
            this.OB_HDT_A_Vacuum_3.Value = false;
            // 
            // IB_HDT_A_VacDetect_2
            // 
            this.IB_HDT_A_VacDetect_2.BackColor = System.Drawing.Color.RoyalBlue;
            this.IB_HDT_A_VacDetect_2.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.IB_HDT_A_VacDetect_2.ErrID = 0;
            this.IB_HDT_A_VacDetect_2.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.IB_HDT_A_VacDetect_2.InAlarm = false;
            this.IB_HDT_A_VacDetect_2.IOPort = "050D";
            this.IB_HDT_A_VacDetect_2.IOType = ProVLib.EIOType.IOHSL;
            this.IB_HDT_A_VacDetect_2.Location = new System.Drawing.Point(12, 81);
            this.IB_HDT_A_VacDetect_2.LockUI = false;
            this.IB_HDT_A_VacDetect_2.Message = null;
            this.IB_HDT_A_VacDetect_2.MsgID = 0;
            this.IB_HDT_A_VacDetect_2.Name = "IB_HDT_A_VacDetect_2";
            this.IB_HDT_A_VacDetect_2.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.IB_HDT_A_VacDetect_2.Running = false;
            this.IB_HDT_A_VacDetect_2.Simu_Mode = ProVLib.SIMULATION_MODE.S_RANDOM;
            this.IB_HDT_A_VacDetect_2.Simu_OnOffCondition = ProVLib.SIMULATION_ONOFFCONDITION.SRR_KEEP;
            this.IB_HDT_A_VacDetect_2.Simu_OutPort1 = null;
            this.IB_HDT_A_VacDetect_2.Simu_OutPort2 = null;
            this.IB_HDT_A_VacDetect_2.Simu_RandomNum = 2;
            this.IB_HDT_A_VacDetect_2.Simu_RandomTime = 100;
            this.IB_HDT_A_VacDetect_2.Simu_ReflectDelayTm = 100;
            this.IB_HDT_A_VacDetect_2.Simu_ReflectRule = ProVLib.SIMULATION_REFLECTRULE.SRR_ON_OFF;
            this.IB_HDT_A_VacDetect_2.Simu_Reverse = false;
            this.IB_HDT_A_VacDetect_2.Size = new System.Drawing.Size(160, 30);
            this.IB_HDT_A_VacDetect_2.Text = "Vac Detect - 2";
            // 
            // OB_HDT_A_Destroy_1
            // 
            this.OB_HDT_A_Destroy_1.ActionCount = 0;
            this.OB_HDT_A_Destroy_1.BackColor = System.Drawing.Color.RoyalBlue;
            this.OB_HDT_A_Destroy_1.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OB_HDT_A_Destroy_1.ErrID = 0;
            this.OB_HDT_A_Destroy_1.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.OB_HDT_A_Destroy_1.InAlarm = false;
            this.OB_HDT_A_Destroy_1.IOPort = "2309";
            this.OB_HDT_A_Destroy_1.IOType = ProVLib.EIOType.IOHSL;
            this.OB_HDT_A_Destroy_1.IsUseActionCount = false;
            this.OB_HDT_A_Destroy_1.Location = new System.Drawing.Point(334, 119);
            this.OB_HDT_A_Destroy_1.LockUI = false;
            this.OB_HDT_A_Destroy_1.Message = null;
            this.OB_HDT_A_Destroy_1.MsgID = 0;
            this.OB_HDT_A_Destroy_1.Name = "OB_HDT_A_Destroy_1";
            this.OB_HDT_A_Destroy_1.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.OB_HDT_A_Destroy_1.RetryCount = 10;
            this.OB_HDT_A_Destroy_1.Running = false;
            this.OB_HDT_A_Destroy_1.Size = new System.Drawing.Size(160, 30);
            this.OB_HDT_A_Destroy_1.Text = "Destory - 1";
            this.OB_HDT_A_Destroy_1.Value = false;
            // 
            // OB_HDT_A_Destroy_4
            // 
            this.OB_HDT_A_Destroy_4.ActionCount = 0;
            this.OB_HDT_A_Destroy_4.BackColor = System.Drawing.Color.RoyalBlue;
            this.OB_HDT_A_Destroy_4.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OB_HDT_A_Destroy_4.ErrID = 0;
            this.OB_HDT_A_Destroy_4.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.OB_HDT_A_Destroy_4.InAlarm = false;
            this.OB_HDT_A_Destroy_4.IOPort = "230F";
            this.OB_HDT_A_Destroy_4.IOType = ProVLib.EIOType.IOHSL;
            this.OB_HDT_A_Destroy_4.IsUseActionCount = false;
            this.OB_HDT_A_Destroy_4.Location = new System.Drawing.Point(334, 5);
            this.OB_HDT_A_Destroy_4.LockUI = false;
            this.OB_HDT_A_Destroy_4.Message = null;
            this.OB_HDT_A_Destroy_4.MsgID = 0;
            this.OB_HDT_A_Destroy_4.Name = "OB_HDT_A_Destroy_4";
            this.OB_HDT_A_Destroy_4.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.OB_HDT_A_Destroy_4.RetryCount = 10;
            this.OB_HDT_A_Destroy_4.Running = false;
            this.OB_HDT_A_Destroy_4.Size = new System.Drawing.Size(160, 30);
            this.OB_HDT_A_Destroy_4.Text = "Destory - 4";
            this.OB_HDT_A_Destroy_4.Value = false;
            // 
            // OB_HDT_A_Destroy_3
            // 
            this.OB_HDT_A_Destroy_3.ActionCount = 0;
            this.OB_HDT_A_Destroy_3.BackColor = System.Drawing.Color.RoyalBlue;
            this.OB_HDT_A_Destroy_3.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OB_HDT_A_Destroy_3.ErrID = 0;
            this.OB_HDT_A_Destroy_3.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.OB_HDT_A_Destroy_3.InAlarm = false;
            this.OB_HDT_A_Destroy_3.IOPort = "230D";
            this.OB_HDT_A_Destroy_3.IOType = ProVLib.EIOType.IOHSL;
            this.OB_HDT_A_Destroy_3.IsUseActionCount = false;
            this.OB_HDT_A_Destroy_3.Location = new System.Drawing.Point(334, 43);
            this.OB_HDT_A_Destroy_3.LockUI = false;
            this.OB_HDT_A_Destroy_3.Message = null;
            this.OB_HDT_A_Destroy_3.MsgID = 0;
            this.OB_HDT_A_Destroy_3.Name = "OB_HDT_A_Destroy_3";
            this.OB_HDT_A_Destroy_3.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.OB_HDT_A_Destroy_3.RetryCount = 10;
            this.OB_HDT_A_Destroy_3.Running = false;
            this.OB_HDT_A_Destroy_3.Size = new System.Drawing.Size(160, 30);
            this.OB_HDT_A_Destroy_3.Text = "Destory - 3";
            this.OB_HDT_A_Destroy_3.Value = false;
            // 
            // OB_HDT_A_Destroy_2
            // 
            this.OB_HDT_A_Destroy_2.ActionCount = 0;
            this.OB_HDT_A_Destroy_2.BackColor = System.Drawing.Color.RoyalBlue;
            this.OB_HDT_A_Destroy_2.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OB_HDT_A_Destroy_2.ErrID = 0;
            this.OB_HDT_A_Destroy_2.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.OB_HDT_A_Destroy_2.InAlarm = false;
            this.OB_HDT_A_Destroy_2.IOPort = "230B";
            this.OB_HDT_A_Destroy_2.IOType = ProVLib.EIOType.IOHSL;
            this.OB_HDT_A_Destroy_2.IsUseActionCount = false;
            this.OB_HDT_A_Destroy_2.Location = new System.Drawing.Point(334, 81);
            this.OB_HDT_A_Destroy_2.LockUI = false;
            this.OB_HDT_A_Destroy_2.Message = null;
            this.OB_HDT_A_Destroy_2.MsgID = 0;
            this.OB_HDT_A_Destroy_2.Name = "OB_HDT_A_Destroy_2";
            this.OB_HDT_A_Destroy_2.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.OB_HDT_A_Destroy_2.RetryCount = 10;
            this.OB_HDT_A_Destroy_2.Running = false;
            this.OB_HDT_A_Destroy_2.Size = new System.Drawing.Size(160, 30);
            this.OB_HDT_A_Destroy_2.Text = "Destory - 2";
            this.OB_HDT_A_Destroy_2.Value = false;
            // 
            // IB_HDT_A_VacDetect_1
            // 
            this.IB_HDT_A_VacDetect_1.BackColor = System.Drawing.Color.RoyalBlue;
            this.IB_HDT_A_VacDetect_1.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.IB_HDT_A_VacDetect_1.ErrID = 0;
            this.IB_HDT_A_VacDetect_1.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.IB_HDT_A_VacDetect_1.InAlarm = false;
            this.IB_HDT_A_VacDetect_1.IOPort = "050C";
            this.IB_HDT_A_VacDetect_1.IOType = ProVLib.EIOType.IOHSL;
            this.IB_HDT_A_VacDetect_1.Location = new System.Drawing.Point(12, 119);
            this.IB_HDT_A_VacDetect_1.LockUI = false;
            this.IB_HDT_A_VacDetect_1.Message = null;
            this.IB_HDT_A_VacDetect_1.MsgID = 0;
            this.IB_HDT_A_VacDetect_1.Name = "IB_HDT_A_VacDetect_1";
            this.IB_HDT_A_VacDetect_1.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.IB_HDT_A_VacDetect_1.Running = false;
            this.IB_HDT_A_VacDetect_1.Simu_Mode = ProVLib.SIMULATION_MODE.S_RANDOM;
            this.IB_HDT_A_VacDetect_1.Simu_OnOffCondition = ProVLib.SIMULATION_ONOFFCONDITION.SRR_KEEP;
            this.IB_HDT_A_VacDetect_1.Simu_OutPort1 = null;
            this.IB_HDT_A_VacDetect_1.Simu_OutPort2 = null;
            this.IB_HDT_A_VacDetect_1.Simu_RandomNum = 2;
            this.IB_HDT_A_VacDetect_1.Simu_RandomTime = 100;
            this.IB_HDT_A_VacDetect_1.Simu_ReflectDelayTm = 100;
            this.IB_HDT_A_VacDetect_1.Simu_ReflectRule = ProVLib.SIMULATION_REFLECTRULE.SRR_ON_OFF;
            this.IB_HDT_A_VacDetect_1.Simu_Reverse = false;
            this.IB_HDT_A_VacDetect_1.Size = new System.Drawing.Size(160, 30);
            this.IB_HDT_A_VacDetect_1.Text = "Vac Detect - 1";
            // 
            // OB_HDT_A_Vacuum_4
            // 
            this.OB_HDT_A_Vacuum_4.ActionCount = 0;
            this.OB_HDT_A_Vacuum_4.BackColor = System.Drawing.Color.RoyalBlue;
            this.OB_HDT_A_Vacuum_4.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OB_HDT_A_Vacuum_4.ErrID = 0;
            this.OB_HDT_A_Vacuum_4.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.OB_HDT_A_Vacuum_4.InAlarm = false;
            this.OB_HDT_A_Vacuum_4.IOPort = "230E";
            this.OB_HDT_A_Vacuum_4.IOType = ProVLib.EIOType.IOHSL;
            this.OB_HDT_A_Vacuum_4.IsUseActionCount = false;
            this.OB_HDT_A_Vacuum_4.Location = new System.Drawing.Point(173, 5);
            this.OB_HDT_A_Vacuum_4.LockUI = false;
            this.OB_HDT_A_Vacuum_4.Message = null;
            this.OB_HDT_A_Vacuum_4.MsgID = 0;
            this.OB_HDT_A_Vacuum_4.Name = "OB_HDT_A_Vacuum_4";
            this.OB_HDT_A_Vacuum_4.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.OB_HDT_A_Vacuum_4.RetryCount = 10;
            this.OB_HDT_A_Vacuum_4.Running = false;
            this.OB_HDT_A_Vacuum_4.Size = new System.Drawing.Size(160, 30);
            this.OB_HDT_A_Vacuum_4.Text = "Vaccum - 4";
            this.OB_HDT_A_Vacuum_4.Value = false;
            // 
            // IB_HDT_A_VacDetect_3
            // 
            this.IB_HDT_A_VacDetect_3.BackColor = System.Drawing.Color.RoyalBlue;
            this.IB_HDT_A_VacDetect_3.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.IB_HDT_A_VacDetect_3.ErrID = 0;
            this.IB_HDT_A_VacDetect_3.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.IB_HDT_A_VacDetect_3.InAlarm = false;
            this.IB_HDT_A_VacDetect_3.IOPort = "050E";
            this.IB_HDT_A_VacDetect_3.IOType = ProVLib.EIOType.IOHSL;
            this.IB_HDT_A_VacDetect_3.Location = new System.Drawing.Point(12, 43);
            this.IB_HDT_A_VacDetect_3.LockUI = false;
            this.IB_HDT_A_VacDetect_3.Message = null;
            this.IB_HDT_A_VacDetect_3.MsgID = 0;
            this.IB_HDT_A_VacDetect_3.Name = "IB_HDT_A_VacDetect_3";
            this.IB_HDT_A_VacDetect_3.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.IB_HDT_A_VacDetect_3.Running = false;
            this.IB_HDT_A_VacDetect_3.Simu_Mode = ProVLib.SIMULATION_MODE.S_RANDOM;
            this.IB_HDT_A_VacDetect_3.Simu_OnOffCondition = ProVLib.SIMULATION_ONOFFCONDITION.SRR_KEEP;
            this.IB_HDT_A_VacDetect_3.Simu_OutPort1 = null;
            this.IB_HDT_A_VacDetect_3.Simu_OutPort2 = null;
            this.IB_HDT_A_VacDetect_3.Simu_RandomNum = 2;
            this.IB_HDT_A_VacDetect_3.Simu_RandomTime = 100;
            this.IB_HDT_A_VacDetect_3.Simu_ReflectDelayTm = 100;
            this.IB_HDT_A_VacDetect_3.Simu_ReflectRule = ProVLib.SIMULATION_REFLECTRULE.SRR_ON_OFF;
            this.IB_HDT_A_VacDetect_3.Simu_Reverse = false;
            this.IB_HDT_A_VacDetect_3.Size = new System.Drawing.Size(160, 30);
            this.IB_HDT_A_VacDetect_3.Text = "Vac Detect - 3";
            // 
            // OB_HDT_A_Vacuum_2
            // 
            this.OB_HDT_A_Vacuum_2.ActionCount = 0;
            this.OB_HDT_A_Vacuum_2.BackColor = System.Drawing.Color.RoyalBlue;
            this.OB_HDT_A_Vacuum_2.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OB_HDT_A_Vacuum_2.ErrID = 0;
            this.OB_HDT_A_Vacuum_2.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.OB_HDT_A_Vacuum_2.InAlarm = false;
            this.OB_HDT_A_Vacuum_2.IOPort = "230A";
            this.OB_HDT_A_Vacuum_2.IOType = ProVLib.EIOType.IOHSL;
            this.OB_HDT_A_Vacuum_2.IsUseActionCount = false;
            this.OB_HDT_A_Vacuum_2.Location = new System.Drawing.Point(173, 81);
            this.OB_HDT_A_Vacuum_2.LockUI = false;
            this.OB_HDT_A_Vacuum_2.Message = null;
            this.OB_HDT_A_Vacuum_2.MsgID = 0;
            this.OB_HDT_A_Vacuum_2.Name = "OB_HDT_A_Vacuum_2";
            this.OB_HDT_A_Vacuum_2.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.OB_HDT_A_Vacuum_2.RetryCount = 10;
            this.OB_HDT_A_Vacuum_2.Running = false;
            this.OB_HDT_A_Vacuum_2.Size = new System.Drawing.Size(160, 30);
            this.OB_HDT_A_Vacuum_2.Text = "Vaccum - 2";
            this.OB_HDT_A_Vacuum_2.Value = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Location = new System.Drawing.Point(705, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(355, 531);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 149;
            this.pictureBox1.TabStop = false;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.SeaShell;
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.button13);
            this.panel8.Controls.Add(this.button14);
            this.panel8.Controls.Add(this.panel6);
            this.panel8.Controls.Add(this.panel4);
            this.panel8.Controls.Add(this.panel1);
            this.panel8.Controls.Add(this.panel2);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(3, 534);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1057, 100);
            this.panel8.TabIndex = 148;
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(643, 59);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(95, 30);
            this.button13.TabIndex = 100;
            this.button13.Text = "servo Off";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(643, 17);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(95, 30);
            this.button14.TabIndex = 99;
            this.button14.Text = "servo ON";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.button4);
            this.panel6.Controls.Add(this.MT_CCD_A_AxisZ);
            this.panel6.Location = new System.Drawing.Point(319, 53);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(310, 40);
            this.panel6.TabIndex = 98;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(256, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(50, 30);
            this.button4.TabIndex = 95;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // MT_CCD_A_AxisZ
            // 
            this.MT_CCD_A_AxisZ.Acceleration = 300000;
            this.MT_CCD_A_AxisZ.AcceptDiffRange = ((uint)(0u));
            this.MT_CCD_A_AxisZ.AddressID = 0;
            this.MT_CCD_A_AxisZ.AlarmPolarity = ProVLib.ALMPOLARITY.ACTIVELOW;
            this.MT_CCD_A_AxisZ.BackColor = System.Drawing.Color.RoyalBlue;
            this.MT_CCD_A_AxisZ.BasePulseCount = 0;
            this.MT_CCD_A_AxisZ.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MT_CCD_A_AxisZ.CMPSRC = ProVLib.ECMPSRC.CMPSRC_1;
            this.MT_CCD_A_AxisZ.D2 = ((byte)(0));
            this.MT_CCD_A_AxisZ.D4 = ((byte)(0));
            this.MT_CCD_A_AxisZ.Deceleration = 300000;
            this.MT_CCD_A_AxisZ.DelayTime = 0D;
            this.MT_CCD_A_AxisZ.Direction = true;
            this.MT_CCD_A_AxisZ.DownZ1 = 0;
            this.MT_CCD_A_AxisZ.DownZ2 = 0;
            this.MT_CCD_A_AxisZ.DownZ3 = 0;
            this.MT_CCD_A_AxisZ.EncGearRatio = 1D;
            this.MT_CCD_A_AxisZ.EndX = 0;
            this.MT_CCD_A_AxisZ.ErrID = 0;
            this.MT_CCD_A_AxisZ.GearRatio = 1D;
            this.MT_CCD_A_AxisZ.GroupNo = ((short)(0));
            this.MT_CCD_A_AxisZ.HomeBeforeGoto = false;
            this.MT_CCD_A_AxisZ.HomeDirection = true;
            this.MT_CCD_A_AxisZ.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            this.MT_CCD_A_AxisZ.HomeOK = false;
            this.MT_CCD_A_AxisZ.HomePos = 100;
            this.MT_CCD_A_AxisZ.InAlarm = false;
            this.MT_CCD_A_AxisZ.InitialPosition = 0;
            this.MT_CCD_A_AxisZ.InitSpeed = 100;
            this.MT_CCD_A_AxisZ.InPosOn = false;
            this.MT_CCD_A_AxisZ.InposRange = 50;
            this.MT_CCD_A_AxisZ.IOPort = "014";
            this.MT_CCD_A_AxisZ.IsELSensorB = true;
            this.MT_CCD_A_AxisZ.IsSensorB = true;
            this.MT_CCD_A_AxisZ.IsUseMileage = false;
            this.MT_CCD_A_AxisZ.IsUseSoftLimit = false;
            this.MT_CCD_A_AxisZ.JogHighSpeed = 30000;
            this.MT_CCD_A_AxisZ.JogLowSpeed = 1000;
            this.MT_CCD_A_AxisZ.LimitX = 0;
            this.MT_CCD_A_AxisZ.LimitZ = 0;
            this.MT_CCD_A_AxisZ.LineID = ((uint)(0u));
            this.MT_CCD_A_AxisZ.Location = new System.Drawing.Point(5, 5);
            this.MT_CCD_A_AxisZ.LockUI = false;
            this.MT_CCD_A_AxisZ.Message = null;
            this.MT_CCD_A_AxisZ.Mileage = 0F;
            this.MT_CCD_A_AxisZ.MNETSpeed = ProVLib.EMNETSPEED.MNET10M;
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
            motorParam1.EncGearRatio = 1D;
            motorParam1.EndX = 0;
            motorParam1.GearRatio = 1D;
            motorParam1.GroupNo = ((short)(0));
            motorParam1.HomeBeforeGoto = false;
            motorParam1.HomeDirection = true;
            motorParam1.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            motorParam1.HomePos = 100;
            motorParam1.IDZ = ((short)(0));
            motorParam1.InitSpeed = 100;
            motorParam1.InPosOn = false;
            motorParam1.InposRange = 50;
            motorParam1.IOPort = "014";
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
            this.MT_CCD_A_AxisZ.MotorParameter = motorParam1;
            this.MT_CCD_A_AxisZ.MoverSize = ((uint)(0u));
            this.MT_CCD_A_AxisZ.MsgID = 0;
            this.MT_CCD_A_AxisZ.Name = "MT_CCD_A_AxisZ";
            this.MT_CCD_A_AxisZ.ObjType = ProVLib.EObjType.AXIS_Y;
            this.MT_CCD_A_AxisZ.PitchCOMEnable = false;
            this.MT_CCD_A_AxisZ.PriorityHigh = ((uint)(0u));
            this.MT_CCD_A_AxisZ.PriorityLow = ((uint)(0u));
            this.MT_CCD_A_AxisZ.PulseCtrlMode = ProVLib.EPULSEMODE.CWCCW_2;
            this.MT_CCD_A_AxisZ.RelCurrentPos = 0;
            this.MT_CCD_A_AxisZ.RelTargetPos = 0;
            this.MT_CCD_A_AxisZ.Running = false;
            this.MT_CCD_A_AxisZ.SafeDistance = ((uint)(0u));
            this.MT_CCD_A_AxisZ.SerialPortName = "COM1";
            this.MT_CCD_A_AxisZ.ServoAlarmOn = false;
            this.MT_CCD_A_AxisZ.ServoOnPolarity = ProVLib.SVONPOLARITY.ACTIVELOW;
            this.MT_CCD_A_AxisZ.Size = new System.Drawing.Size(250, 30);
            this.MT_CCD_A_AxisZ.SlaveIOPort = "000";
            this.MT_CCD_A_AxisZ.SoftLimitN = -9999999;
            this.MT_CCD_A_AxisZ.SoftLimitP = 9999999;
            this.MT_CCD_A_AxisZ.Speed = 50000;
            this.MT_CCD_A_AxisZ.Text = "(M25) Board CCDA Z";
            this.MT_CCD_A_AxisZ.TRGSRC = ProVLib.ETRIGGERSRC.TRGENC;
            this.MT_CCD_A_AxisZ.UpZ = 0;
            this.MT_CCD_A_AxisZ.ZPhaseLogic = ProVLib.ZPHASELOGIC.FALLINGEDGE;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.button3);
            this.panel4.Controls.Add(this.MT_HDT_A_AxisZ);
            this.panel4.Location = new System.Drawing.Point(3, 53);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(310, 40);
            this.panel4.TabIndex = 97;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(256, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(50, 30);
            this.button3.TabIndex = 95;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // MT_HDT_A_AxisZ
            // 
            this.MT_HDT_A_AxisZ.Acceleration = 300000;
            this.MT_HDT_A_AxisZ.AcceptDiffRange = ((uint)(0u));
            this.MT_HDT_A_AxisZ.AddressID = 0;
            this.MT_HDT_A_AxisZ.AlarmPolarity = ProVLib.ALMPOLARITY.ACTIVELOW;
            this.MT_HDT_A_AxisZ.BackColor = System.Drawing.Color.RoyalBlue;
            this.MT_HDT_A_AxisZ.BasePulseCount = 0;
            this.MT_HDT_A_AxisZ.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MT_HDT_A_AxisZ.CMPSRC = ProVLib.ECMPSRC.CMPSRC_1;
            this.MT_HDT_A_AxisZ.D2 = ((byte)(0));
            this.MT_HDT_A_AxisZ.D4 = ((byte)(0));
            this.MT_HDT_A_AxisZ.Deceleration = 300000;
            this.MT_HDT_A_AxisZ.DelayTime = 0D;
            this.MT_HDT_A_AxisZ.Direction = true;
            this.MT_HDT_A_AxisZ.DownZ1 = 0;
            this.MT_HDT_A_AxisZ.DownZ2 = 0;
            this.MT_HDT_A_AxisZ.DownZ3 = 0;
            this.MT_HDT_A_AxisZ.EncGearRatio = 1D;
            this.MT_HDT_A_AxisZ.EndX = 0;
            this.MT_HDT_A_AxisZ.ErrID = 0;
            this.MT_HDT_A_AxisZ.GearRatio = 1D;
            this.MT_HDT_A_AxisZ.GroupNo = ((short)(0));
            this.MT_HDT_A_AxisZ.HomeBeforeGoto = false;
            this.MT_HDT_A_AxisZ.HomeDirection = true;
            this.MT_HDT_A_AxisZ.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            this.MT_HDT_A_AxisZ.HomeOK = false;
            this.MT_HDT_A_AxisZ.HomePos = 100;
            this.MT_HDT_A_AxisZ.InAlarm = false;
            this.MT_HDT_A_AxisZ.InitialPosition = 0;
            this.MT_HDT_A_AxisZ.InitSpeed = 100;
            this.MT_HDT_A_AxisZ.InPosOn = false;
            this.MT_HDT_A_AxisZ.InposRange = 50;
            this.MT_HDT_A_AxisZ.IOPort = "014";
            this.MT_HDT_A_AxisZ.IsELSensorB = true;
            this.MT_HDT_A_AxisZ.IsSensorB = true;
            this.MT_HDT_A_AxisZ.IsUseMileage = false;
            this.MT_HDT_A_AxisZ.IsUseSoftLimit = false;
            this.MT_HDT_A_AxisZ.JogHighSpeed = 30000;
            this.MT_HDT_A_AxisZ.JogLowSpeed = 1000;
            this.MT_HDT_A_AxisZ.LimitX = 0;
            this.MT_HDT_A_AxisZ.LimitZ = 0;
            this.MT_HDT_A_AxisZ.LineID = ((uint)(0u));
            this.MT_HDT_A_AxisZ.Location = new System.Drawing.Point(5, 5);
            this.MT_HDT_A_AxisZ.LockUI = false;
            this.MT_HDT_A_AxisZ.Message = null;
            this.MT_HDT_A_AxisZ.Mileage = 0F;
            this.MT_HDT_A_AxisZ.MNETSpeed = ProVLib.EMNETSPEED.MNET10M;
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
            motorParam2.EncGearRatio = 1D;
            motorParam2.EndX = 0;
            motorParam2.GearRatio = 1D;
            motorParam2.GroupNo = ((short)(0));
            motorParam2.HomeBeforeGoto = false;
            motorParam2.HomeDirection = true;
            motorParam2.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            motorParam2.HomePos = 100;
            motorParam2.IDZ = ((short)(0));
            motorParam2.InitSpeed = 100;
            motorParam2.InPosOn = false;
            motorParam2.InposRange = 50;
            motorParam2.IOPort = "014";
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
            this.MT_HDT_A_AxisZ.MotorParameter = motorParam2;
            this.MT_HDT_A_AxisZ.MoverSize = ((uint)(0u));
            this.MT_HDT_A_AxisZ.MsgID = 0;
            this.MT_HDT_A_AxisZ.Name = "MT_HDT_A_AxisZ";
            this.MT_HDT_A_AxisZ.ObjType = ProVLib.EObjType.AXIS_Y;
            this.MT_HDT_A_AxisZ.PitchCOMEnable = false;
            this.MT_HDT_A_AxisZ.PriorityHigh = ((uint)(0u));
            this.MT_HDT_A_AxisZ.PriorityLow = ((uint)(0u));
            this.MT_HDT_A_AxisZ.PulseCtrlMode = ProVLib.EPULSEMODE.CWCCW_2;
            this.MT_HDT_A_AxisZ.RelCurrentPos = 0;
            this.MT_HDT_A_AxisZ.RelTargetPos = 0;
            this.MT_HDT_A_AxisZ.Running = false;
            this.MT_HDT_A_AxisZ.SafeDistance = ((uint)(0u));
            this.MT_HDT_A_AxisZ.SerialPortName = "COM1";
            this.MT_HDT_A_AxisZ.ServoAlarmOn = false;
            this.MT_HDT_A_AxisZ.ServoOnPolarity = ProVLib.SVONPOLARITY.ACTIVELOW;
            this.MT_HDT_A_AxisZ.Size = new System.Drawing.Size(250, 30);
            this.MT_HDT_A_AxisZ.SlaveIOPort = "000";
            this.MT_HDT_A_AxisZ.SoftLimitN = -9999999;
            this.MT_HDT_A_AxisZ.SoftLimitP = 9999999;
            this.MT_HDT_A_AxisZ.Speed = 50000;
            this.MT_HDT_A_AxisZ.Text = "(M10) Board HeadA Z";
            this.MT_HDT_A_AxisZ.TRGSRC = ProVLib.ETRIGGERSRC.TRGENC;
            this.MT_HDT_A_AxisZ.UpZ = 0;
            this.MT_HDT_A_AxisZ.ZPhaseLogic = ProVLib.ZPHASELOGIC.FALLINGEDGE;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.MT_HDT_A_AxisX);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(3, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(310, 40);
            this.panel1.TabIndex = 95;
            // 
            // MT_HDT_A_AxisX
            // 
            this.MT_HDT_A_AxisX.Acceleration = 300000;
            this.MT_HDT_A_AxisX.AcceptDiffRange = ((uint)(0u));
            this.MT_HDT_A_AxisX.AddressID = 0;
            this.MT_HDT_A_AxisX.AlarmPolarity = ProVLib.ALMPOLARITY.ACTIVELOW;
            this.MT_HDT_A_AxisX.BackColor = System.Drawing.Color.RoyalBlue;
            this.MT_HDT_A_AxisX.BasePulseCount = 0;
            this.MT_HDT_A_AxisX.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MT_HDT_A_AxisX.CMPSRC = ProVLib.ECMPSRC.CMPSRC_1;
            this.MT_HDT_A_AxisX.D2 = ((byte)(0));
            this.MT_HDT_A_AxisX.D4 = ((byte)(0));
            this.MT_HDT_A_AxisX.Deceleration = 300000;
            this.MT_HDT_A_AxisX.DelayTime = 0D;
            this.MT_HDT_A_AxisX.Direction = true;
            this.MT_HDT_A_AxisX.DownZ1 = 0;
            this.MT_HDT_A_AxisX.DownZ2 = 0;
            this.MT_HDT_A_AxisX.DownZ3 = 0;
            this.MT_HDT_A_AxisX.EncGearRatio = 1D;
            this.MT_HDT_A_AxisX.EndX = 0;
            this.MT_HDT_A_AxisX.ErrID = 0;
            this.MT_HDT_A_AxisX.GearRatio = 1D;
            this.MT_HDT_A_AxisX.GroupNo = ((short)(0));
            this.MT_HDT_A_AxisX.HomeBeforeGoto = false;
            this.MT_HDT_A_AxisX.HomeDirection = true;
            this.MT_HDT_A_AxisX.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            this.MT_HDT_A_AxisX.HomeOK = false;
            this.MT_HDT_A_AxisX.HomePos = 100;
            this.MT_HDT_A_AxisX.InAlarm = false;
            this.MT_HDT_A_AxisX.InitialPosition = 0;
            this.MT_HDT_A_AxisX.InitSpeed = 100;
            this.MT_HDT_A_AxisX.InPosOn = false;
            this.MT_HDT_A_AxisX.InposRange = 50;
            this.MT_HDT_A_AxisX.IOPort = "011";
            this.MT_HDT_A_AxisX.IsELSensorB = true;
            this.MT_HDT_A_AxisX.IsSensorB = true;
            this.MT_HDT_A_AxisX.IsUseMileage = false;
            this.MT_HDT_A_AxisX.IsUseSoftLimit = false;
            this.MT_HDT_A_AxisX.JogHighSpeed = 30000;
            this.MT_HDT_A_AxisX.JogLowSpeed = 1000;
            this.MT_HDT_A_AxisX.LimitX = 0;
            this.MT_HDT_A_AxisX.LimitZ = 0;
            this.MT_HDT_A_AxisX.LineID = ((uint)(0u));
            this.MT_HDT_A_AxisX.Location = new System.Drawing.Point(5, 5);
            this.MT_HDT_A_AxisX.LockUI = false;
            this.MT_HDT_A_AxisX.Message = null;
            this.MT_HDT_A_AxisX.Mileage = 0F;
            this.MT_HDT_A_AxisX.MNETSpeed = ProVLib.EMNETSPEED.MNET10M;
            motorParam3.Acceleration = 300000;
            motorParam3.AddressID = 0;
            motorParam3.AlarmPolarity = ProVLib.ALMPOLARITY.ACTIVELOW;
            motorParam3.BasePulseCount = 0;
            motorParam3.CMPSRC = ProVLib.ECMPSRC.CMPSRC_1;
            motorParam3.Deceleration = 300000;
            motorParam3.DelayT = 0D;
            motorParam3.Direction = true;
            motorParam3.DownZ1 = 0;
            motorParam3.DownZ2 = 0;
            motorParam3.DownZ3 = 0;
            motorParam3.EncGearRatio = 1D;
            motorParam3.EndX = 0;
            motorParam3.GearRatio = 1D;
            motorParam3.GroupNo = ((short)(0));
            motorParam3.HomeBeforeGoto = false;
            motorParam3.HomeDirection = true;
            motorParam3.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            motorParam3.HomePos = 100;
            motorParam3.IDZ = ((short)(0));
            motorParam3.InitSpeed = 100;
            motorParam3.InPosOn = false;
            motorParam3.InposRange = 50;
            motorParam3.IOPort = "011";
            motorParam3.IsBusy = false;
            motorParam3.IsELSensorB = true;
            motorParam3.IsSensorB = true;
            motorParam3.IsUseSoftLimit = false;
            motorParam3.JogHighSpeed = 30000;
            motorParam3.JogLowSpeed = 1000;
            motorParam3.LimitX = 0;
            motorParam3.LimitZ = 0;
            motorParam3.MNETSpeed = ProVLib.EMNETSPEED.MNET10M;
            motorParam3.MotionCard = ProVLib.EMOTIONCARD.MCMNET;
            motorParam3.MotorType = ProVLib.MOTORTYPE.NORMAL;
            motorParam3.ObjType = 0;
            motorParam3.Owner = null;
            motorParam3.PitchCOMEnable = false;
            motorParam3.PulseCtrlMode = ProVLib.EPULSEMODE.CWCCW_2;
            motorParam3.SerialPortName = "COM1";
            motorParam3.ServoAlarmOn = false;
            motorParam3.ServoOnPolarity = ProVLib.SVONPOLARITY.ACTIVELOW;
            motorParam3.SlaveIOPort = "000";
            motorParam3.SoftLimitN = -9999999;
            motorParam3.SoftLimitP = 9999999;
            motorParam3.Speed = 50000;
            motorParam3.TRGSRC = ProVLib.ETRIGGERSRC.TRGENC;
            motorParam3.TriAxis = null;
            motorParam3.UpZ = 0;
            motorParam3.ZPhaseLogic = ProVLib.ZPHASELOGIC.FALLINGEDGE;
            this.MT_HDT_A_AxisX.MotorParameter = motorParam3;
            this.MT_HDT_A_AxisX.MoverSize = ((uint)(0u));
            this.MT_HDT_A_AxisX.MsgID = 0;
            this.MT_HDT_A_AxisX.Name = "MT_HDT_A_AxisX";
            this.MT_HDT_A_AxisX.ObjType = ProVLib.EObjType.AXIS_Y;
            this.MT_HDT_A_AxisX.PitchCOMEnable = false;
            this.MT_HDT_A_AxisX.PriorityHigh = ((uint)(0u));
            this.MT_HDT_A_AxisX.PriorityLow = ((uint)(0u));
            this.MT_HDT_A_AxisX.PulseCtrlMode = ProVLib.EPULSEMODE.CWCCW_2;
            this.MT_HDT_A_AxisX.RelCurrentPos = 0;
            this.MT_HDT_A_AxisX.RelTargetPos = 0;
            this.MT_HDT_A_AxisX.Running = false;
            this.MT_HDT_A_AxisX.SafeDistance = ((uint)(0u));
            this.MT_HDT_A_AxisX.SerialPortName = "COM1";
            this.MT_HDT_A_AxisX.ServoAlarmOn = false;
            this.MT_HDT_A_AxisX.ServoOnPolarity = ProVLib.SVONPOLARITY.ACTIVELOW;
            this.MT_HDT_A_AxisX.Size = new System.Drawing.Size(250, 30);
            this.MT_HDT_A_AxisX.SlaveIOPort = "000";
            this.MT_HDT_A_AxisX.SoftLimitN = -9999999;
            this.MT_HDT_A_AxisX.SoftLimitP = 9999999;
            this.MT_HDT_A_AxisX.Speed = 50000;
            this.MT_HDT_A_AxisX.Text = "(M9) Board HeadA X";
            this.MT_HDT_A_AxisX.TRGSRC = ProVLib.ETRIGGERSRC.TRGENC;
            this.MT_HDT_A_AxisX.UpZ = 0;
            this.MT_HDT_A_AxisX.ZPhaseLogic = ProVLib.ZPHASELOGIC.FALLINGEDGE;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(257, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 30);
            this.button1.TabIndex = 96;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.MT_HDT_A_AxisU);
            this.panel2.Location = new System.Drawing.Point(319, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(310, 40);
            this.panel2.TabIndex = 96;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(256, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 30);
            this.button2.TabIndex = 95;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MT_HDT_A_AxisU
            // 
            this.MT_HDT_A_AxisU.Acceleration = 300000;
            this.MT_HDT_A_AxisU.AcceptDiffRange = ((uint)(0u));
            this.MT_HDT_A_AxisU.AddressID = 0;
            this.MT_HDT_A_AxisU.AlarmPolarity = ProVLib.ALMPOLARITY.ACTIVELOW;
            this.MT_HDT_A_AxisU.BackColor = System.Drawing.Color.RoyalBlue;
            this.MT_HDT_A_AxisU.BasePulseCount = 0;
            this.MT_HDT_A_AxisU.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MT_HDT_A_AxisU.CMPSRC = ProVLib.ECMPSRC.CMPSRC_1;
            this.MT_HDT_A_AxisU.D2 = ((byte)(0));
            this.MT_HDT_A_AxisU.D4 = ((byte)(0));
            this.MT_HDT_A_AxisU.Deceleration = 300000;
            this.MT_HDT_A_AxisU.DelayTime = 0D;
            this.MT_HDT_A_AxisU.Direction = true;
            this.MT_HDT_A_AxisU.DownZ1 = 0;
            this.MT_HDT_A_AxisU.DownZ2 = 0;
            this.MT_HDT_A_AxisU.DownZ3 = 0;
            this.MT_HDT_A_AxisU.EncGearRatio = 1D;
            this.MT_HDT_A_AxisU.EndX = 0;
            this.MT_HDT_A_AxisU.ErrID = 0;
            this.MT_HDT_A_AxisU.GearRatio = 1D;
            this.MT_HDT_A_AxisU.GroupNo = ((short)(0));
            this.MT_HDT_A_AxisU.HomeBeforeGoto = false;
            this.MT_HDT_A_AxisU.HomeDirection = true;
            this.MT_HDT_A_AxisU.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            this.MT_HDT_A_AxisU.HomeOK = false;
            this.MT_HDT_A_AxisU.HomePos = 100;
            this.MT_HDT_A_AxisU.InAlarm = false;
            this.MT_HDT_A_AxisU.InitialPosition = 0;
            this.MT_HDT_A_AxisU.InitSpeed = 100;
            this.MT_HDT_A_AxisU.InPosOn = false;
            this.MT_HDT_A_AxisU.InposRange = 50;
            this.MT_HDT_A_AxisU.IOPort = "014";
            this.MT_HDT_A_AxisU.IsELSensorB = true;
            this.MT_HDT_A_AxisU.IsSensorB = true;
            this.MT_HDT_A_AxisU.IsUseMileage = false;
            this.MT_HDT_A_AxisU.IsUseSoftLimit = false;
            this.MT_HDT_A_AxisU.JogHighSpeed = 30000;
            this.MT_HDT_A_AxisU.JogLowSpeed = 1000;
            this.MT_HDT_A_AxisU.LimitX = 0;
            this.MT_HDT_A_AxisU.LimitZ = 0;
            this.MT_HDT_A_AxisU.LineID = ((uint)(0u));
            this.MT_HDT_A_AxisU.Location = new System.Drawing.Point(5, 5);
            this.MT_HDT_A_AxisU.LockUI = false;
            this.MT_HDT_A_AxisU.Message = null;
            this.MT_HDT_A_AxisU.Mileage = 0F;
            this.MT_HDT_A_AxisU.MNETSpeed = ProVLib.EMNETSPEED.MNET10M;
            motorParam4.Acceleration = 300000;
            motorParam4.AddressID = 0;
            motorParam4.AlarmPolarity = ProVLib.ALMPOLARITY.ACTIVELOW;
            motorParam4.BasePulseCount = 0;
            motorParam4.CMPSRC = ProVLib.ECMPSRC.CMPSRC_1;
            motorParam4.Deceleration = 300000;
            motorParam4.DelayT = 0D;
            motorParam4.Direction = true;
            motorParam4.DownZ1 = 0;
            motorParam4.DownZ2 = 0;
            motorParam4.DownZ3 = 0;
            motorParam4.EncGearRatio = 1D;
            motorParam4.EndX = 0;
            motorParam4.GearRatio = 1D;
            motorParam4.GroupNo = ((short)(0));
            motorParam4.HomeBeforeGoto = false;
            motorParam4.HomeDirection = true;
            motorParam4.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            motorParam4.HomePos = 100;
            motorParam4.IDZ = ((short)(0));
            motorParam4.InitSpeed = 100;
            motorParam4.InPosOn = false;
            motorParam4.InposRange = 50;
            motorParam4.IOPort = "014";
            motorParam4.IsBusy = false;
            motorParam4.IsELSensorB = true;
            motorParam4.IsSensorB = true;
            motorParam4.IsUseSoftLimit = false;
            motorParam4.JogHighSpeed = 30000;
            motorParam4.JogLowSpeed = 1000;
            motorParam4.LimitX = 0;
            motorParam4.LimitZ = 0;
            motorParam4.MNETSpeed = ProVLib.EMNETSPEED.MNET10M;
            motorParam4.MotionCard = ProVLib.EMOTIONCARD.MCMNET;
            motorParam4.MotorType = ProVLib.MOTORTYPE.NORMAL;
            motorParam4.ObjType = 0;
            motorParam4.Owner = null;
            motorParam4.PitchCOMEnable = false;
            motorParam4.PulseCtrlMode = ProVLib.EPULSEMODE.CWCCW_2;
            motorParam4.SerialPortName = "COM1";
            motorParam4.ServoAlarmOn = false;
            motorParam4.ServoOnPolarity = ProVLib.SVONPOLARITY.ACTIVELOW;
            motorParam4.SlaveIOPort = "000";
            motorParam4.SoftLimitN = -9999999;
            motorParam4.SoftLimitP = 9999999;
            motorParam4.Speed = 50000;
            motorParam4.TRGSRC = ProVLib.ETRIGGERSRC.TRGENC;
            motorParam4.TriAxis = null;
            motorParam4.UpZ = 0;
            motorParam4.ZPhaseLogic = ProVLib.ZPHASELOGIC.FALLINGEDGE;
            this.MT_HDT_A_AxisU.MotorParameter = motorParam4;
            this.MT_HDT_A_AxisU.MoverSize = ((uint)(0u));
            this.MT_HDT_A_AxisU.MsgID = 0;
            this.MT_HDT_A_AxisU.Name = "MT_HDT_A_AxisU";
            this.MT_HDT_A_AxisU.ObjType = ProVLib.EObjType.AXIS_Y;
            this.MT_HDT_A_AxisU.PitchCOMEnable = false;
            this.MT_HDT_A_AxisU.PriorityHigh = ((uint)(0u));
            this.MT_HDT_A_AxisU.PriorityLow = ((uint)(0u));
            this.MT_HDT_A_AxisU.PulseCtrlMode = ProVLib.EPULSEMODE.CWCCW_2;
            this.MT_HDT_A_AxisU.RelCurrentPos = 0;
            this.MT_HDT_A_AxisU.RelTargetPos = 0;
            this.MT_HDT_A_AxisU.Running = false;
            this.MT_HDT_A_AxisU.SafeDistance = ((uint)(0u));
            this.MT_HDT_A_AxisU.SerialPortName = "COM1";
            this.MT_HDT_A_AxisU.ServoAlarmOn = false;
            this.MT_HDT_A_AxisU.ServoOnPolarity = ProVLib.SVONPOLARITY.ACTIVELOW;
            this.MT_HDT_A_AxisU.Size = new System.Drawing.Size(250, 30);
            this.MT_HDT_A_AxisU.SlaveIOPort = "000";
            this.MT_HDT_A_AxisU.SoftLimitN = -9999999;
            this.MT_HDT_A_AxisU.SoftLimitP = 9999999;
            this.MT_HDT_A_AxisU.Speed = 50000;
            this.MT_HDT_A_AxisU.Text = "(M11) Board HeadA U";
            this.MT_HDT_A_AxisU.TRGSRC = ProVLib.ETRIGGERSRC.TRGENC;
            this.MT_HDT_A_AxisU.UpZ = 0;
            this.MT_HDT_A_AxisU.ZPhaseLogic = ProVLib.ZPHASELOGIC.FALLINGEDGE;
            // 
            // IO_HDT_B
            // 
            this.IO_HDT_B.Controls.Add(this.groupBox6);
            this.IO_HDT_B.Controls.Add(this.panel14);
            this.IO_HDT_B.Controls.Add(this.pictureBox2);
            this.IO_HDT_B.Controls.Add(this.panel7);
            this.IO_HDT_B.Location = new System.Drawing.Point(4, 30);
            this.IO_HDT_B.Name = "IO_HDT_B";
            this.IO_HDT_B.Padding = new System.Windows.Forms.Padding(3);
            this.IO_HDT_B.Size = new System.Drawing.Size(1063, 637);
            this.IO_HDT_B.TabIndex = 1;
            this.IO_HDT_B.Text = "HDT_B";
            this.IO_HDT_B.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button11);
            this.groupBox6.Controls.Add(this.HDT_B_ClientSocket);
            this.groupBox6.Controls.Add(this.button12);
            this.groupBox6.Location = new System.Drawing.Point(28, 200);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(225, 146);
            this.groupBox6.TabIndex = 153;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "CCD Socket";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(138, 75);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 50);
            this.button11.TabIndex = 5;
            this.button11.Text = "斷線";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // HDT_B_ClientSocket
            // 
            this.HDT_B_ClientSocket.IP = null;
            this.HDT_B_ClientSocket.Location = new System.Drawing.Point(13, 29);
            this.HDT_B_ClientSocket.Name = "HDT_B_ClientSocket";
            this.HDT_B_ClientSocket.Port = 0;
            this.HDT_B_ClientSocket.Size = new System.Drawing.Size(200, 30);
            this.HDT_B_ClientSocket.Text = "CCD Client Socket";
            this.HDT_B_ClientSocket.OnConnect += new ProVTool.ProVClientSocket.SocketNotifyHandler(this.HDT_B_ClientSocket_OnConnect);
            this.HDT_B_ClientSocket.OnDisconnect += new ProVTool.ProVClientSocket.SocketDisconnectNotifyHandler(this.HDT_B_ClientSocket_OnDisconnect);
            this.HDT_B_ClientSocket.OnRead += new ProVTool.ProVClientSocket.SocketReadNotifyHandler(this.HDT_B_ClientSocket_OnRead);
            this.HDT_B_ClientSocket.OnError += new ProVTool.ProVClientSocket.ErrorNotifyHandler(this.HDT_B_ClientSocket_OnError);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(13, 75);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(75, 50);
            this.button12.TabIndex = 4;
            this.button12.Text = "連線";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // panel14
            // 
            this.panel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel14.Controls.Add(this.OB_HDT_B_Vacuum_1);
            this.panel14.Controls.Add(this.IB_HDT_B_VacDetect_4);
            this.panel14.Controls.Add(this.OB_HDT_B_Vacuum_3);
            this.panel14.Controls.Add(this.IB_HDT_B_VacDetect_2);
            this.panel14.Controls.Add(this.OB_HDT_B_Destroy_1);
            this.panel14.Controls.Add(this.OB_HDT_B_Destroy_4);
            this.panel14.Controls.Add(this.OB_HDT_B_Destroy_3);
            this.panel14.Controls.Add(this.OB_HDT_B_Destroy_2);
            this.panel14.Controls.Add(this.IB_HDT_B_VacDetect_1);
            this.panel14.Controls.Add(this.OB_HDT_B_Vacuum_4);
            this.panel14.Controls.Add(this.IB_HDT_B_VacDetect_3);
            this.panel14.Controls.Add(this.OB_HDT_B_Vacuum_2);
            this.panel14.Location = new System.Drawing.Point(28, 22);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(509, 158);
            this.panel14.TabIndex = 152;
            // 
            // OB_HDT_B_Vacuum_1
            // 
            this.OB_HDT_B_Vacuum_1.ActionCount = 0;
            this.OB_HDT_B_Vacuum_1.BackColor = System.Drawing.Color.RoyalBlue;
            this.OB_HDT_B_Vacuum_1.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OB_HDT_B_Vacuum_1.ErrID = 0;
            this.OB_HDT_B_Vacuum_1.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.OB_HDT_B_Vacuum_1.InAlarm = false;
            this.OB_HDT_B_Vacuum_1.IOPort = "2310";
            this.OB_HDT_B_Vacuum_1.IOType = ProVLib.EIOType.IOHSL;
            this.OB_HDT_B_Vacuum_1.IsUseActionCount = false;
            this.OB_HDT_B_Vacuum_1.Location = new System.Drawing.Point(174, 119);
            this.OB_HDT_B_Vacuum_1.LockUI = false;
            this.OB_HDT_B_Vacuum_1.Message = null;
            this.OB_HDT_B_Vacuum_1.MsgID = 0;
            this.OB_HDT_B_Vacuum_1.Name = "OB_HDT_B_Vacuum_1";
            this.OB_HDT_B_Vacuum_1.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.OB_HDT_B_Vacuum_1.RetryCount = 10;
            this.OB_HDT_B_Vacuum_1.Running = false;
            this.OB_HDT_B_Vacuum_1.Size = new System.Drawing.Size(160, 30);
            this.OB_HDT_B_Vacuum_1.Text = "Vaccum - 1";
            this.OB_HDT_B_Vacuum_1.Value = false;
            // 
            // IB_HDT_B_VacDetect_4
            // 
            this.IB_HDT_B_VacDetect_4.BackColor = System.Drawing.Color.RoyalBlue;
            this.IB_HDT_B_VacDetect_4.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.IB_HDT_B_VacDetect_4.ErrID = 0;
            this.IB_HDT_B_VacDetect_4.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.IB_HDT_B_VacDetect_4.InAlarm = false;
            this.IB_HDT_B_VacDetect_4.IOPort = "051F";
            this.IB_HDT_B_VacDetect_4.IOType = ProVLib.EIOType.IOHSL;
            this.IB_HDT_B_VacDetect_4.Location = new System.Drawing.Point(13, 5);
            this.IB_HDT_B_VacDetect_4.LockUI = false;
            this.IB_HDT_B_VacDetect_4.Message = null;
            this.IB_HDT_B_VacDetect_4.MsgID = 0;
            this.IB_HDT_B_VacDetect_4.Name = "IB_HDT_B_VacDetect_4";
            this.IB_HDT_B_VacDetect_4.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.IB_HDT_B_VacDetect_4.Running = false;
            this.IB_HDT_B_VacDetect_4.Simu_Mode = ProVLib.SIMULATION_MODE.S_RANDOM;
            this.IB_HDT_B_VacDetect_4.Simu_OnOffCondition = ProVLib.SIMULATION_ONOFFCONDITION.SRR_KEEP;
            this.IB_HDT_B_VacDetect_4.Simu_OutPort1 = null;
            this.IB_HDT_B_VacDetect_4.Simu_OutPort2 = null;
            this.IB_HDT_B_VacDetect_4.Simu_RandomNum = 2;
            this.IB_HDT_B_VacDetect_4.Simu_RandomTime = 100;
            this.IB_HDT_B_VacDetect_4.Simu_ReflectDelayTm = 100;
            this.IB_HDT_B_VacDetect_4.Simu_ReflectRule = ProVLib.SIMULATION_REFLECTRULE.SRR_ON_OFF;
            this.IB_HDT_B_VacDetect_4.Simu_Reverse = false;
            this.IB_HDT_B_VacDetect_4.Size = new System.Drawing.Size(160, 30);
            this.IB_HDT_B_VacDetect_4.Text = "Vac Detect - 4";
            // 
            // OB_HDT_B_Vacuum_3
            // 
            this.OB_HDT_B_Vacuum_3.ActionCount = 0;
            this.OB_HDT_B_Vacuum_3.BackColor = System.Drawing.Color.RoyalBlue;
            this.OB_HDT_B_Vacuum_3.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OB_HDT_B_Vacuum_3.ErrID = 0;
            this.OB_HDT_B_Vacuum_3.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.OB_HDT_B_Vacuum_3.InAlarm = false;
            this.OB_HDT_B_Vacuum_3.IOPort = "2314";
            this.OB_HDT_B_Vacuum_3.IOType = ProVLib.EIOType.IOHSL;
            this.OB_HDT_B_Vacuum_3.IsUseActionCount = false;
            this.OB_HDT_B_Vacuum_3.Location = new System.Drawing.Point(174, 43);
            this.OB_HDT_B_Vacuum_3.LockUI = false;
            this.OB_HDT_B_Vacuum_3.Message = null;
            this.OB_HDT_B_Vacuum_3.MsgID = 0;
            this.OB_HDT_B_Vacuum_3.Name = "OB_HDT_B_Vacuum_3";
            this.OB_HDT_B_Vacuum_3.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.OB_HDT_B_Vacuum_3.RetryCount = 10;
            this.OB_HDT_B_Vacuum_3.Running = false;
            this.OB_HDT_B_Vacuum_3.Size = new System.Drawing.Size(160, 30);
            this.OB_HDT_B_Vacuum_3.Text = "Vaccum - 3";
            this.OB_HDT_B_Vacuum_3.Value = false;
            // 
            // IB_HDT_B_VacDetect_2
            // 
            this.IB_HDT_B_VacDetect_2.BackColor = System.Drawing.Color.RoyalBlue;
            this.IB_HDT_B_VacDetect_2.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.IB_HDT_B_VacDetect_2.ErrID = 0;
            this.IB_HDT_B_VacDetect_2.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.IB_HDT_B_VacDetect_2.InAlarm = false;
            this.IB_HDT_B_VacDetect_2.IOPort = "051D";
            this.IB_HDT_B_VacDetect_2.IOType = ProVLib.EIOType.IOHSL;
            this.IB_HDT_B_VacDetect_2.Location = new System.Drawing.Point(13, 81);
            this.IB_HDT_B_VacDetect_2.LockUI = false;
            this.IB_HDT_B_VacDetect_2.Message = null;
            this.IB_HDT_B_VacDetect_2.MsgID = 0;
            this.IB_HDT_B_VacDetect_2.Name = "IB_HDT_B_VacDetect_2";
            this.IB_HDT_B_VacDetect_2.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.IB_HDT_B_VacDetect_2.Running = false;
            this.IB_HDT_B_VacDetect_2.Simu_Mode = ProVLib.SIMULATION_MODE.S_RANDOM;
            this.IB_HDT_B_VacDetect_2.Simu_OnOffCondition = ProVLib.SIMULATION_ONOFFCONDITION.SRR_KEEP;
            this.IB_HDT_B_VacDetect_2.Simu_OutPort1 = null;
            this.IB_HDT_B_VacDetect_2.Simu_OutPort2 = null;
            this.IB_HDT_B_VacDetect_2.Simu_RandomNum = 2;
            this.IB_HDT_B_VacDetect_2.Simu_RandomTime = 100;
            this.IB_HDT_B_VacDetect_2.Simu_ReflectDelayTm = 100;
            this.IB_HDT_B_VacDetect_2.Simu_ReflectRule = ProVLib.SIMULATION_REFLECTRULE.SRR_ON_OFF;
            this.IB_HDT_B_VacDetect_2.Simu_Reverse = false;
            this.IB_HDT_B_VacDetect_2.Size = new System.Drawing.Size(160, 30);
            this.IB_HDT_B_VacDetect_2.Text = "Vac Detect - 2";
            // 
            // OB_HDT_B_Destroy_1
            // 
            this.OB_HDT_B_Destroy_1.ActionCount = 0;
            this.OB_HDT_B_Destroy_1.BackColor = System.Drawing.Color.RoyalBlue;
            this.OB_HDT_B_Destroy_1.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OB_HDT_B_Destroy_1.ErrID = 0;
            this.OB_HDT_B_Destroy_1.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.OB_HDT_B_Destroy_1.InAlarm = false;
            this.OB_HDT_B_Destroy_1.IOPort = "2311";
            this.OB_HDT_B_Destroy_1.IOType = ProVLib.EIOType.IOHSL;
            this.OB_HDT_B_Destroy_1.IsUseActionCount = false;
            this.OB_HDT_B_Destroy_1.Location = new System.Drawing.Point(335, 119);
            this.OB_HDT_B_Destroy_1.LockUI = false;
            this.OB_HDT_B_Destroy_1.Message = null;
            this.OB_HDT_B_Destroy_1.MsgID = 0;
            this.OB_HDT_B_Destroy_1.Name = "OB_HDT_B_Destroy_1";
            this.OB_HDT_B_Destroy_1.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.OB_HDT_B_Destroy_1.RetryCount = 10;
            this.OB_HDT_B_Destroy_1.Running = false;
            this.OB_HDT_B_Destroy_1.Size = new System.Drawing.Size(160, 30);
            this.OB_HDT_B_Destroy_1.Text = "Destory - 1";
            this.OB_HDT_B_Destroy_1.Value = false;
            // 
            // OB_HDT_B_Destroy_4
            // 
            this.OB_HDT_B_Destroy_4.ActionCount = 0;
            this.OB_HDT_B_Destroy_4.BackColor = System.Drawing.Color.RoyalBlue;
            this.OB_HDT_B_Destroy_4.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OB_HDT_B_Destroy_4.ErrID = 0;
            this.OB_HDT_B_Destroy_4.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.OB_HDT_B_Destroy_4.InAlarm = false;
            this.OB_HDT_B_Destroy_4.IOPort = "2317";
            this.OB_HDT_B_Destroy_4.IOType = ProVLib.EIOType.IOHSL;
            this.OB_HDT_B_Destroy_4.IsUseActionCount = false;
            this.OB_HDT_B_Destroy_4.Location = new System.Drawing.Point(335, 5);
            this.OB_HDT_B_Destroy_4.LockUI = false;
            this.OB_HDT_B_Destroy_4.Message = null;
            this.OB_HDT_B_Destroy_4.MsgID = 0;
            this.OB_HDT_B_Destroy_4.Name = "OB_HDT_B_Destroy_4";
            this.OB_HDT_B_Destroy_4.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.OB_HDT_B_Destroy_4.RetryCount = 10;
            this.OB_HDT_B_Destroy_4.Running = false;
            this.OB_HDT_B_Destroy_4.Size = new System.Drawing.Size(160, 30);
            this.OB_HDT_B_Destroy_4.Text = "Destory - 4";
            this.OB_HDT_B_Destroy_4.Value = false;
            // 
            // OB_HDT_B_Destroy_3
            // 
            this.OB_HDT_B_Destroy_3.ActionCount = 0;
            this.OB_HDT_B_Destroy_3.BackColor = System.Drawing.Color.RoyalBlue;
            this.OB_HDT_B_Destroy_3.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OB_HDT_B_Destroy_3.ErrID = 0;
            this.OB_HDT_B_Destroy_3.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.OB_HDT_B_Destroy_3.InAlarm = false;
            this.OB_HDT_B_Destroy_3.IOPort = "2315";
            this.OB_HDT_B_Destroy_3.IOType = ProVLib.EIOType.IOHSL;
            this.OB_HDT_B_Destroy_3.IsUseActionCount = false;
            this.OB_HDT_B_Destroy_3.Location = new System.Drawing.Point(335, 43);
            this.OB_HDT_B_Destroy_3.LockUI = false;
            this.OB_HDT_B_Destroy_3.Message = null;
            this.OB_HDT_B_Destroy_3.MsgID = 0;
            this.OB_HDT_B_Destroy_3.Name = "OB_HDT_B_Destroy_3";
            this.OB_HDT_B_Destroy_3.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.OB_HDT_B_Destroy_3.RetryCount = 10;
            this.OB_HDT_B_Destroy_3.Running = false;
            this.OB_HDT_B_Destroy_3.Size = new System.Drawing.Size(160, 30);
            this.OB_HDT_B_Destroy_3.Text = "Destory - 3";
            this.OB_HDT_B_Destroy_3.Value = false;
            // 
            // OB_HDT_B_Destroy_2
            // 
            this.OB_HDT_B_Destroy_2.ActionCount = 0;
            this.OB_HDT_B_Destroy_2.BackColor = System.Drawing.Color.RoyalBlue;
            this.OB_HDT_B_Destroy_2.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OB_HDT_B_Destroy_2.ErrID = 0;
            this.OB_HDT_B_Destroy_2.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.OB_HDT_B_Destroy_2.InAlarm = false;
            this.OB_HDT_B_Destroy_2.IOPort = "2313";
            this.OB_HDT_B_Destroy_2.IOType = ProVLib.EIOType.IOHSL;
            this.OB_HDT_B_Destroy_2.IsUseActionCount = false;
            this.OB_HDT_B_Destroy_2.Location = new System.Drawing.Point(335, 81);
            this.OB_HDT_B_Destroy_2.LockUI = false;
            this.OB_HDT_B_Destroy_2.Message = null;
            this.OB_HDT_B_Destroy_2.MsgID = 0;
            this.OB_HDT_B_Destroy_2.Name = "OB_HDT_B_Destroy_2";
            this.OB_HDT_B_Destroy_2.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.OB_HDT_B_Destroy_2.RetryCount = 10;
            this.OB_HDT_B_Destroy_2.Running = false;
            this.OB_HDT_B_Destroy_2.Size = new System.Drawing.Size(160, 30);
            this.OB_HDT_B_Destroy_2.Text = "Destory - 2";
            this.OB_HDT_B_Destroy_2.Value = false;
            // 
            // IB_HDT_B_VacDetect_1
            // 
            this.IB_HDT_B_VacDetect_1.BackColor = System.Drawing.Color.RoyalBlue;
            this.IB_HDT_B_VacDetect_1.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.IB_HDT_B_VacDetect_1.ErrID = 0;
            this.IB_HDT_B_VacDetect_1.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.IB_HDT_B_VacDetect_1.InAlarm = false;
            this.IB_HDT_B_VacDetect_1.IOPort = "051C";
            this.IB_HDT_B_VacDetect_1.IOType = ProVLib.EIOType.IOHSL;
            this.IB_HDT_B_VacDetect_1.Location = new System.Drawing.Point(13, 119);
            this.IB_HDT_B_VacDetect_1.LockUI = false;
            this.IB_HDT_B_VacDetect_1.Message = null;
            this.IB_HDT_B_VacDetect_1.MsgID = 0;
            this.IB_HDT_B_VacDetect_1.Name = "IB_HDT_B_VacDetect_1";
            this.IB_HDT_B_VacDetect_1.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.IB_HDT_B_VacDetect_1.Running = false;
            this.IB_HDT_B_VacDetect_1.Simu_Mode = ProVLib.SIMULATION_MODE.S_RANDOM;
            this.IB_HDT_B_VacDetect_1.Simu_OnOffCondition = ProVLib.SIMULATION_ONOFFCONDITION.SRR_KEEP;
            this.IB_HDT_B_VacDetect_1.Simu_OutPort1 = null;
            this.IB_HDT_B_VacDetect_1.Simu_OutPort2 = null;
            this.IB_HDT_B_VacDetect_1.Simu_RandomNum = 2;
            this.IB_HDT_B_VacDetect_1.Simu_RandomTime = 100;
            this.IB_HDT_B_VacDetect_1.Simu_ReflectDelayTm = 100;
            this.IB_HDT_B_VacDetect_1.Simu_ReflectRule = ProVLib.SIMULATION_REFLECTRULE.SRR_ON_OFF;
            this.IB_HDT_B_VacDetect_1.Simu_Reverse = false;
            this.IB_HDT_B_VacDetect_1.Size = new System.Drawing.Size(160, 30);
            this.IB_HDT_B_VacDetect_1.Text = "Vac Detect - 1";
            // 
            // OB_HDT_B_Vacuum_4
            // 
            this.OB_HDT_B_Vacuum_4.ActionCount = 0;
            this.OB_HDT_B_Vacuum_4.BackColor = System.Drawing.Color.RoyalBlue;
            this.OB_HDT_B_Vacuum_4.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OB_HDT_B_Vacuum_4.ErrID = 0;
            this.OB_HDT_B_Vacuum_4.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.OB_HDT_B_Vacuum_4.InAlarm = false;
            this.OB_HDT_B_Vacuum_4.IOPort = "2316";
            this.OB_HDT_B_Vacuum_4.IOType = ProVLib.EIOType.IOHSL;
            this.OB_HDT_B_Vacuum_4.IsUseActionCount = false;
            this.OB_HDT_B_Vacuum_4.Location = new System.Drawing.Point(174, 5);
            this.OB_HDT_B_Vacuum_4.LockUI = false;
            this.OB_HDT_B_Vacuum_4.Message = null;
            this.OB_HDT_B_Vacuum_4.MsgID = 0;
            this.OB_HDT_B_Vacuum_4.Name = "OB_HDT_B_Vacuum_4";
            this.OB_HDT_B_Vacuum_4.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.OB_HDT_B_Vacuum_4.RetryCount = 10;
            this.OB_HDT_B_Vacuum_4.Running = false;
            this.OB_HDT_B_Vacuum_4.Size = new System.Drawing.Size(160, 30);
            this.OB_HDT_B_Vacuum_4.Text = "Vaccum - 4";
            this.OB_HDT_B_Vacuum_4.Value = false;
            // 
            // IB_HDT_B_VacDetect_3
            // 
            this.IB_HDT_B_VacDetect_3.BackColor = System.Drawing.Color.RoyalBlue;
            this.IB_HDT_B_VacDetect_3.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.IB_HDT_B_VacDetect_3.ErrID = 0;
            this.IB_HDT_B_VacDetect_3.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.IB_HDT_B_VacDetect_3.InAlarm = false;
            this.IB_HDT_B_VacDetect_3.IOPort = "051E";
            this.IB_HDT_B_VacDetect_3.IOType = ProVLib.EIOType.IOHSL;
            this.IB_HDT_B_VacDetect_3.Location = new System.Drawing.Point(13, 43);
            this.IB_HDT_B_VacDetect_3.LockUI = false;
            this.IB_HDT_B_VacDetect_3.Message = null;
            this.IB_HDT_B_VacDetect_3.MsgID = 0;
            this.IB_HDT_B_VacDetect_3.Name = "IB_HDT_B_VacDetect_3";
            this.IB_HDT_B_VacDetect_3.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.IB_HDT_B_VacDetect_3.Running = false;
            this.IB_HDT_B_VacDetect_3.Simu_Mode = ProVLib.SIMULATION_MODE.S_RANDOM;
            this.IB_HDT_B_VacDetect_3.Simu_OnOffCondition = ProVLib.SIMULATION_ONOFFCONDITION.SRR_KEEP;
            this.IB_HDT_B_VacDetect_3.Simu_OutPort1 = null;
            this.IB_HDT_B_VacDetect_3.Simu_OutPort2 = null;
            this.IB_HDT_B_VacDetect_3.Simu_RandomNum = 2;
            this.IB_HDT_B_VacDetect_3.Simu_RandomTime = 100;
            this.IB_HDT_B_VacDetect_3.Simu_ReflectDelayTm = 100;
            this.IB_HDT_B_VacDetect_3.Simu_ReflectRule = ProVLib.SIMULATION_REFLECTRULE.SRR_ON_OFF;
            this.IB_HDT_B_VacDetect_3.Simu_Reverse = false;
            this.IB_HDT_B_VacDetect_3.Size = new System.Drawing.Size(160, 30);
            this.IB_HDT_B_VacDetect_3.Text = "Vac Detect - 3";
            // 
            // OB_HDT_B_Vacuum_2
            // 
            this.OB_HDT_B_Vacuum_2.ActionCount = 0;
            this.OB_HDT_B_Vacuum_2.BackColor = System.Drawing.Color.RoyalBlue;
            this.OB_HDT_B_Vacuum_2.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OB_HDT_B_Vacuum_2.ErrID = 0;
            this.OB_HDT_B_Vacuum_2.HSLSpeed = ProVLib.EHSLSPEED.HSL6M;
            this.OB_HDT_B_Vacuum_2.InAlarm = false;
            this.OB_HDT_B_Vacuum_2.IOPort = "2312";
            this.OB_HDT_B_Vacuum_2.IOType = ProVLib.EIOType.IOHSL;
            this.OB_HDT_B_Vacuum_2.IsUseActionCount = false;
            this.OB_HDT_B_Vacuum_2.Location = new System.Drawing.Point(174, 81);
            this.OB_HDT_B_Vacuum_2.LockUI = false;
            this.OB_HDT_B_Vacuum_2.Message = null;
            this.OB_HDT_B_Vacuum_2.MsgID = 0;
            this.OB_HDT_B_Vacuum_2.Name = "OB_HDT_B_Vacuum_2";
            this.OB_HDT_B_Vacuum_2.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.OB_HDT_B_Vacuum_2.RetryCount = 10;
            this.OB_HDT_B_Vacuum_2.Running = false;
            this.OB_HDT_B_Vacuum_2.Size = new System.Drawing.Size(160, 30);
            this.OB_HDT_B_Vacuum_2.Text = "Vaccum - 2";
            this.OB_HDT_B_Vacuum_2.Value = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox2.Location = new System.Drawing.Point(705, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(355, 531);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 151;
            this.pictureBox2.TabStop = false;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.SeaShell;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.button15);
            this.panel7.Controls.Add(this.button16);
            this.panel7.Controls.Add(this.panel9);
            this.panel7.Controls.Add(this.panel10);
            this.panel7.Controls.Add(this.panel11);
            this.panel7.Controls.Add(this.panel12);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(3, 534);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1057, 100);
            this.panel7.TabIndex = 150;
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(649, 55);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(95, 30);
            this.button15.TabIndex = 102;
            this.button15.Text = "servo Off";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(649, 13);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(95, 30);
            this.button16.TabIndex = 101;
            this.button16.Text = "servo ON";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.button5);
            this.panel9.Controls.Add(this.MT_CCD_B_AxisZ);
            this.panel9.Location = new System.Drawing.Point(319, 53);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(310, 40);
            this.panel9.TabIndex = 98;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.Location = new System.Drawing.Point(256, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(50, 30);
            this.button5.TabIndex = 95;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // MT_CCD_B_AxisZ
            // 
            this.MT_CCD_B_AxisZ.Acceleration = 300000;
            this.MT_CCD_B_AxisZ.AcceptDiffRange = ((uint)(0u));
            this.MT_CCD_B_AxisZ.AddressID = 0;
            this.MT_CCD_B_AxisZ.AlarmPolarity = ProVLib.ALMPOLARITY.ACTIVELOW;
            this.MT_CCD_B_AxisZ.BackColor = System.Drawing.Color.RoyalBlue;
            this.MT_CCD_B_AxisZ.BasePulseCount = 0;
            this.MT_CCD_B_AxisZ.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MT_CCD_B_AxisZ.CMPSRC = ProVLib.ECMPSRC.CMPSRC_1;
            this.MT_CCD_B_AxisZ.D2 = ((byte)(0));
            this.MT_CCD_B_AxisZ.D4 = ((byte)(0));
            this.MT_CCD_B_AxisZ.Deceleration = 300000;
            this.MT_CCD_B_AxisZ.DelayTime = 0D;
            this.MT_CCD_B_AxisZ.Direction = true;
            this.MT_CCD_B_AxisZ.DownZ1 = 0;
            this.MT_CCD_B_AxisZ.DownZ2 = 0;
            this.MT_CCD_B_AxisZ.DownZ3 = 0;
            this.MT_CCD_B_AxisZ.EncGearRatio = 1D;
            this.MT_CCD_B_AxisZ.EndX = 0;
            this.MT_CCD_B_AxisZ.ErrID = 0;
            this.MT_CCD_B_AxisZ.GearRatio = 1D;
            this.MT_CCD_B_AxisZ.GroupNo = ((short)(0));
            this.MT_CCD_B_AxisZ.HomeBeforeGoto = false;
            this.MT_CCD_B_AxisZ.HomeDirection = true;
            this.MT_CCD_B_AxisZ.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            this.MT_CCD_B_AxisZ.HomeOK = false;
            this.MT_CCD_B_AxisZ.HomePos = 100;
            this.MT_CCD_B_AxisZ.InAlarm = false;
            this.MT_CCD_B_AxisZ.InitialPosition = 0;
            this.MT_CCD_B_AxisZ.InitSpeed = 100;
            this.MT_CCD_B_AxisZ.InPosOn = false;
            this.MT_CCD_B_AxisZ.InposRange = 50;
            this.MT_CCD_B_AxisZ.IOPort = "014";
            this.MT_CCD_B_AxisZ.IsELSensorB = true;
            this.MT_CCD_B_AxisZ.IsSensorB = true;
            this.MT_CCD_B_AxisZ.IsUseMileage = false;
            this.MT_CCD_B_AxisZ.IsUseSoftLimit = false;
            this.MT_CCD_B_AxisZ.JogHighSpeed = 30000;
            this.MT_CCD_B_AxisZ.JogLowSpeed = 1000;
            this.MT_CCD_B_AxisZ.LimitX = 0;
            this.MT_CCD_B_AxisZ.LimitZ = 0;
            this.MT_CCD_B_AxisZ.LineID = ((uint)(0u));
            this.MT_CCD_B_AxisZ.Location = new System.Drawing.Point(5, 5);
            this.MT_CCD_B_AxisZ.LockUI = false;
            this.MT_CCD_B_AxisZ.Message = null;
            this.MT_CCD_B_AxisZ.Mileage = 0F;
            this.MT_CCD_B_AxisZ.MNETSpeed = ProVLib.EMNETSPEED.MNET10M;
            motorParam5.Acceleration = 300000;
            motorParam5.AddressID = 0;
            motorParam5.AlarmPolarity = ProVLib.ALMPOLARITY.ACTIVELOW;
            motorParam5.BasePulseCount = 0;
            motorParam5.CMPSRC = ProVLib.ECMPSRC.CMPSRC_1;
            motorParam5.Deceleration = 300000;
            motorParam5.DelayT = 0D;
            motorParam5.Direction = true;
            motorParam5.DownZ1 = 0;
            motorParam5.DownZ2 = 0;
            motorParam5.DownZ3 = 0;
            motorParam5.EncGearRatio = 1D;
            motorParam5.EndX = 0;
            motorParam5.GearRatio = 1D;
            motorParam5.GroupNo = ((short)(0));
            motorParam5.HomeBeforeGoto = false;
            motorParam5.HomeDirection = true;
            motorParam5.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            motorParam5.HomePos = 100;
            motorParam5.IDZ = ((short)(0));
            motorParam5.InitSpeed = 100;
            motorParam5.InPosOn = false;
            motorParam5.InposRange = 50;
            motorParam5.IOPort = "014";
            motorParam5.IsBusy = false;
            motorParam5.IsELSensorB = true;
            motorParam5.IsSensorB = true;
            motorParam5.IsUseSoftLimit = false;
            motorParam5.JogHighSpeed = 30000;
            motorParam5.JogLowSpeed = 1000;
            motorParam5.LimitX = 0;
            motorParam5.LimitZ = 0;
            motorParam5.MNETSpeed = ProVLib.EMNETSPEED.MNET10M;
            motorParam5.MotionCard = ProVLib.EMOTIONCARD.MCMNET;
            motorParam5.MotorType = ProVLib.MOTORTYPE.NORMAL;
            motorParam5.ObjType = 0;
            motorParam5.Owner = null;
            motorParam5.PitchCOMEnable = false;
            motorParam5.PulseCtrlMode = ProVLib.EPULSEMODE.CWCCW_2;
            motorParam5.SerialPortName = "COM1";
            motorParam5.ServoAlarmOn = false;
            motorParam5.ServoOnPolarity = ProVLib.SVONPOLARITY.ACTIVELOW;
            motorParam5.SlaveIOPort = "000";
            motorParam5.SoftLimitN = -9999999;
            motorParam5.SoftLimitP = 9999999;
            motorParam5.Speed = 50000;
            motorParam5.TRGSRC = ProVLib.ETRIGGERSRC.TRGENC;
            motorParam5.TriAxis = null;
            motorParam5.UpZ = 0;
            motorParam5.ZPhaseLogic = ProVLib.ZPHASELOGIC.FALLINGEDGE;
            this.MT_CCD_B_AxisZ.MotorParameter = motorParam5;
            this.MT_CCD_B_AxisZ.MoverSize = ((uint)(0u));
            this.MT_CCD_B_AxisZ.MsgID = 0;
            this.MT_CCD_B_AxisZ.Name = "MT_CCD_B_AxisZ";
            this.MT_CCD_B_AxisZ.ObjType = ProVLib.EObjType.AXIS_Y;
            this.MT_CCD_B_AxisZ.PitchCOMEnable = false;
            this.MT_CCD_B_AxisZ.PriorityHigh = ((uint)(0u));
            this.MT_CCD_B_AxisZ.PriorityLow = ((uint)(0u));
            this.MT_CCD_B_AxisZ.PulseCtrlMode = ProVLib.EPULSEMODE.CWCCW_2;
            this.MT_CCD_B_AxisZ.RelCurrentPos = 0;
            this.MT_CCD_B_AxisZ.RelTargetPos = 0;
            this.MT_CCD_B_AxisZ.Running = false;
            this.MT_CCD_B_AxisZ.SafeDistance = ((uint)(0u));
            this.MT_CCD_B_AxisZ.SerialPortName = "COM1";
            this.MT_CCD_B_AxisZ.ServoAlarmOn = false;
            this.MT_CCD_B_AxisZ.ServoOnPolarity = ProVLib.SVONPOLARITY.ACTIVELOW;
            this.MT_CCD_B_AxisZ.Size = new System.Drawing.Size(250, 30);
            this.MT_CCD_B_AxisZ.SlaveIOPort = "000";
            this.MT_CCD_B_AxisZ.SoftLimitN = -9999999;
            this.MT_CCD_B_AxisZ.SoftLimitP = 9999999;
            this.MT_CCD_B_AxisZ.Speed = 50000;
            this.MT_CCD_B_AxisZ.Text = "(M26) Board CCDB Z";
            this.MT_CCD_B_AxisZ.TRGSRC = ProVLib.ETRIGGERSRC.TRGENC;
            this.MT_CCD_B_AxisZ.UpZ = 0;
            this.MT_CCD_B_AxisZ.ZPhaseLogic = ProVLib.ZPHASELOGIC.FALLINGEDGE;
            // 
            // panel10
            // 
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Controls.Add(this.button6);
            this.panel10.Controls.Add(this.MT_HDT_B_AxisZ);
            this.panel10.Location = new System.Drawing.Point(3, 53);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(310, 40);
            this.panel10.TabIndex = 97;
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.Location = new System.Drawing.Point(256, 5);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(50, 30);
            this.button6.TabIndex = 95;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // MT_HDT_B_AxisZ
            // 
            this.MT_HDT_B_AxisZ.Acceleration = 300000;
            this.MT_HDT_B_AxisZ.AcceptDiffRange = ((uint)(0u));
            this.MT_HDT_B_AxisZ.AddressID = 0;
            this.MT_HDT_B_AxisZ.AlarmPolarity = ProVLib.ALMPOLARITY.ACTIVELOW;
            this.MT_HDT_B_AxisZ.BackColor = System.Drawing.Color.RoyalBlue;
            this.MT_HDT_B_AxisZ.BasePulseCount = 0;
            this.MT_HDT_B_AxisZ.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MT_HDT_B_AxisZ.CMPSRC = ProVLib.ECMPSRC.CMPSRC_1;
            this.MT_HDT_B_AxisZ.D2 = ((byte)(0));
            this.MT_HDT_B_AxisZ.D4 = ((byte)(0));
            this.MT_HDT_B_AxisZ.Deceleration = 300000;
            this.MT_HDT_B_AxisZ.DelayTime = 0D;
            this.MT_HDT_B_AxisZ.Direction = true;
            this.MT_HDT_B_AxisZ.DownZ1 = 0;
            this.MT_HDT_B_AxisZ.DownZ2 = 0;
            this.MT_HDT_B_AxisZ.DownZ3 = 0;
            this.MT_HDT_B_AxisZ.EncGearRatio = 1D;
            this.MT_HDT_B_AxisZ.EndX = 0;
            this.MT_HDT_B_AxisZ.ErrID = 0;
            this.MT_HDT_B_AxisZ.GearRatio = 1D;
            this.MT_HDT_B_AxisZ.GroupNo = ((short)(0));
            this.MT_HDT_B_AxisZ.HomeBeforeGoto = false;
            this.MT_HDT_B_AxisZ.HomeDirection = true;
            this.MT_HDT_B_AxisZ.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            this.MT_HDT_B_AxisZ.HomeOK = false;
            this.MT_HDT_B_AxisZ.HomePos = 100;
            this.MT_HDT_B_AxisZ.InAlarm = false;
            this.MT_HDT_B_AxisZ.InitialPosition = 0;
            this.MT_HDT_B_AxisZ.InitSpeed = 100;
            this.MT_HDT_B_AxisZ.InPosOn = false;
            this.MT_HDT_B_AxisZ.InposRange = 50;
            this.MT_HDT_B_AxisZ.IOPort = "014";
            this.MT_HDT_B_AxisZ.IsELSensorB = true;
            this.MT_HDT_B_AxisZ.IsSensorB = true;
            this.MT_HDT_B_AxisZ.IsUseMileage = false;
            this.MT_HDT_B_AxisZ.IsUseSoftLimit = false;
            this.MT_HDT_B_AxisZ.JogHighSpeed = 30000;
            this.MT_HDT_B_AxisZ.JogLowSpeed = 1000;
            this.MT_HDT_B_AxisZ.LimitX = 0;
            this.MT_HDT_B_AxisZ.LimitZ = 0;
            this.MT_HDT_B_AxisZ.LineID = ((uint)(0u));
            this.MT_HDT_B_AxisZ.Location = new System.Drawing.Point(5, 5);
            this.MT_HDT_B_AxisZ.LockUI = false;
            this.MT_HDT_B_AxisZ.Message = null;
            this.MT_HDT_B_AxisZ.Mileage = 0F;
            this.MT_HDT_B_AxisZ.MNETSpeed = ProVLib.EMNETSPEED.MNET10M;
            motorParam6.Acceleration = 300000;
            motorParam6.AddressID = 0;
            motorParam6.AlarmPolarity = ProVLib.ALMPOLARITY.ACTIVELOW;
            motorParam6.BasePulseCount = 0;
            motorParam6.CMPSRC = ProVLib.ECMPSRC.CMPSRC_1;
            motorParam6.Deceleration = 300000;
            motorParam6.DelayT = 0D;
            motorParam6.Direction = true;
            motorParam6.DownZ1 = 0;
            motorParam6.DownZ2 = 0;
            motorParam6.DownZ3 = 0;
            motorParam6.EncGearRatio = 1D;
            motorParam6.EndX = 0;
            motorParam6.GearRatio = 1D;
            motorParam6.GroupNo = ((short)(0));
            motorParam6.HomeBeforeGoto = false;
            motorParam6.HomeDirection = true;
            motorParam6.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            motorParam6.HomePos = 100;
            motorParam6.IDZ = ((short)(0));
            motorParam6.InitSpeed = 100;
            motorParam6.InPosOn = false;
            motorParam6.InposRange = 50;
            motorParam6.IOPort = "014";
            motorParam6.IsBusy = false;
            motorParam6.IsELSensorB = true;
            motorParam6.IsSensorB = true;
            motorParam6.IsUseSoftLimit = false;
            motorParam6.JogHighSpeed = 30000;
            motorParam6.JogLowSpeed = 1000;
            motorParam6.LimitX = 0;
            motorParam6.LimitZ = 0;
            motorParam6.MNETSpeed = ProVLib.EMNETSPEED.MNET10M;
            motorParam6.MotionCard = ProVLib.EMOTIONCARD.MCMNET;
            motorParam6.MotorType = ProVLib.MOTORTYPE.NORMAL;
            motorParam6.ObjType = 0;
            motorParam6.Owner = null;
            motorParam6.PitchCOMEnable = false;
            motorParam6.PulseCtrlMode = ProVLib.EPULSEMODE.CWCCW_2;
            motorParam6.SerialPortName = "COM1";
            motorParam6.ServoAlarmOn = false;
            motorParam6.ServoOnPolarity = ProVLib.SVONPOLARITY.ACTIVELOW;
            motorParam6.SlaveIOPort = "000";
            motorParam6.SoftLimitN = -9999999;
            motorParam6.SoftLimitP = 9999999;
            motorParam6.Speed = 50000;
            motorParam6.TRGSRC = ProVLib.ETRIGGERSRC.TRGENC;
            motorParam6.TriAxis = null;
            motorParam6.UpZ = 0;
            motorParam6.ZPhaseLogic = ProVLib.ZPHASELOGIC.FALLINGEDGE;
            this.MT_HDT_B_AxisZ.MotorParameter = motorParam6;
            this.MT_HDT_B_AxisZ.MoverSize = ((uint)(0u));
            this.MT_HDT_B_AxisZ.MsgID = 0;
            this.MT_HDT_B_AxisZ.Name = "MT_HDT_B_AxisZ";
            this.MT_HDT_B_AxisZ.ObjType = ProVLib.EObjType.AXIS_Y;
            this.MT_HDT_B_AxisZ.PitchCOMEnable = false;
            this.MT_HDT_B_AxisZ.PriorityHigh = ((uint)(0u));
            this.MT_HDT_B_AxisZ.PriorityLow = ((uint)(0u));
            this.MT_HDT_B_AxisZ.PulseCtrlMode = ProVLib.EPULSEMODE.CWCCW_2;
            this.MT_HDT_B_AxisZ.RelCurrentPos = 0;
            this.MT_HDT_B_AxisZ.RelTargetPos = 0;
            this.MT_HDT_B_AxisZ.Running = false;
            this.MT_HDT_B_AxisZ.SafeDistance = ((uint)(0u));
            this.MT_HDT_B_AxisZ.SerialPortName = "COM1";
            this.MT_HDT_B_AxisZ.ServoAlarmOn = false;
            this.MT_HDT_B_AxisZ.ServoOnPolarity = ProVLib.SVONPOLARITY.ACTIVELOW;
            this.MT_HDT_B_AxisZ.Size = new System.Drawing.Size(250, 30);
            this.MT_HDT_B_AxisZ.SlaveIOPort = "000";
            this.MT_HDT_B_AxisZ.SoftLimitN = -9999999;
            this.MT_HDT_B_AxisZ.SoftLimitP = 9999999;
            this.MT_HDT_B_AxisZ.Speed = 50000;
            this.MT_HDT_B_AxisZ.Text = "(M14) Board HeadB Z";
            this.MT_HDT_B_AxisZ.TRGSRC = ProVLib.ETRIGGERSRC.TRGENC;
            this.MT_HDT_B_AxisZ.UpZ = 0;
            this.MT_HDT_B_AxisZ.ZPhaseLogic = ProVLib.ZPHASELOGIC.FALLINGEDGE;
            // 
            // panel11
            // 
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel11.Controls.Add(this.MT_HDT_B_AxisX);
            this.panel11.Controls.Add(this.button7);
            this.panel11.Location = new System.Drawing.Point(3, 7);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(310, 40);
            this.panel11.TabIndex = 95;
            // 
            // MT_HDT_B_AxisX
            // 
            this.MT_HDT_B_AxisX.Acceleration = 300000;
            this.MT_HDT_B_AxisX.AcceptDiffRange = ((uint)(0u));
            this.MT_HDT_B_AxisX.AddressID = 0;
            this.MT_HDT_B_AxisX.AlarmPolarity = ProVLib.ALMPOLARITY.ACTIVELOW;
            this.MT_HDT_B_AxisX.BackColor = System.Drawing.Color.RoyalBlue;
            this.MT_HDT_B_AxisX.BasePulseCount = 0;
            this.MT_HDT_B_AxisX.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MT_HDT_B_AxisX.CMPSRC = ProVLib.ECMPSRC.CMPSRC_1;
            this.MT_HDT_B_AxisX.D2 = ((byte)(0));
            this.MT_HDT_B_AxisX.D4 = ((byte)(0));
            this.MT_HDT_B_AxisX.Deceleration = 300000;
            this.MT_HDT_B_AxisX.DelayTime = 0D;
            this.MT_HDT_B_AxisX.Direction = true;
            this.MT_HDT_B_AxisX.DownZ1 = 0;
            this.MT_HDT_B_AxisX.DownZ2 = 0;
            this.MT_HDT_B_AxisX.DownZ3 = 0;
            this.MT_HDT_B_AxisX.EncGearRatio = 1D;
            this.MT_HDT_B_AxisX.EndX = 0;
            this.MT_HDT_B_AxisX.ErrID = 0;
            this.MT_HDT_B_AxisX.GearRatio = 1D;
            this.MT_HDT_B_AxisX.GroupNo = ((short)(0));
            this.MT_HDT_B_AxisX.HomeBeforeGoto = false;
            this.MT_HDT_B_AxisX.HomeDirection = true;
            this.MT_HDT_B_AxisX.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            this.MT_HDT_B_AxisX.HomeOK = false;
            this.MT_HDT_B_AxisX.HomePos = 100;
            this.MT_HDT_B_AxisX.InAlarm = false;
            this.MT_HDT_B_AxisX.InitialPosition = 0;
            this.MT_HDT_B_AxisX.InitSpeed = 100;
            this.MT_HDT_B_AxisX.InPosOn = false;
            this.MT_HDT_B_AxisX.InposRange = 50;
            this.MT_HDT_B_AxisX.IOPort = "011";
            this.MT_HDT_B_AxisX.IsELSensorB = true;
            this.MT_HDT_B_AxisX.IsSensorB = true;
            this.MT_HDT_B_AxisX.IsUseMileage = false;
            this.MT_HDT_B_AxisX.IsUseSoftLimit = false;
            this.MT_HDT_B_AxisX.JogHighSpeed = 30000;
            this.MT_HDT_B_AxisX.JogLowSpeed = 1000;
            this.MT_HDT_B_AxisX.LimitX = 0;
            this.MT_HDT_B_AxisX.LimitZ = 0;
            this.MT_HDT_B_AxisX.LineID = ((uint)(0u));
            this.MT_HDT_B_AxisX.Location = new System.Drawing.Point(5, 5);
            this.MT_HDT_B_AxisX.LockUI = false;
            this.MT_HDT_B_AxisX.Message = null;
            this.MT_HDT_B_AxisX.Mileage = 0F;
            this.MT_HDT_B_AxisX.MNETSpeed = ProVLib.EMNETSPEED.MNET10M;
            motorParam7.Acceleration = 300000;
            motorParam7.AddressID = 0;
            motorParam7.AlarmPolarity = ProVLib.ALMPOLARITY.ACTIVELOW;
            motorParam7.BasePulseCount = 0;
            motorParam7.CMPSRC = ProVLib.ECMPSRC.CMPSRC_1;
            motorParam7.Deceleration = 300000;
            motorParam7.DelayT = 0D;
            motorParam7.Direction = true;
            motorParam7.DownZ1 = 0;
            motorParam7.DownZ2 = 0;
            motorParam7.DownZ3 = 0;
            motorParam7.EncGearRatio = 1D;
            motorParam7.EndX = 0;
            motorParam7.GearRatio = 1D;
            motorParam7.GroupNo = ((short)(0));
            motorParam7.HomeBeforeGoto = false;
            motorParam7.HomeDirection = true;
            motorParam7.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            motorParam7.HomePos = 100;
            motorParam7.IDZ = ((short)(0));
            motorParam7.InitSpeed = 100;
            motorParam7.InPosOn = false;
            motorParam7.InposRange = 50;
            motorParam7.IOPort = "011";
            motorParam7.IsBusy = false;
            motorParam7.IsELSensorB = true;
            motorParam7.IsSensorB = true;
            motorParam7.IsUseSoftLimit = false;
            motorParam7.JogHighSpeed = 30000;
            motorParam7.JogLowSpeed = 1000;
            motorParam7.LimitX = 0;
            motorParam7.LimitZ = 0;
            motorParam7.MNETSpeed = ProVLib.EMNETSPEED.MNET10M;
            motorParam7.MotionCard = ProVLib.EMOTIONCARD.MCMNET;
            motorParam7.MotorType = ProVLib.MOTORTYPE.NORMAL;
            motorParam7.ObjType = 0;
            motorParam7.Owner = null;
            motorParam7.PitchCOMEnable = false;
            motorParam7.PulseCtrlMode = ProVLib.EPULSEMODE.CWCCW_2;
            motorParam7.SerialPortName = "COM1";
            motorParam7.ServoAlarmOn = false;
            motorParam7.ServoOnPolarity = ProVLib.SVONPOLARITY.ACTIVELOW;
            motorParam7.SlaveIOPort = "000";
            motorParam7.SoftLimitN = -9999999;
            motorParam7.SoftLimitP = 9999999;
            motorParam7.Speed = 50000;
            motorParam7.TRGSRC = ProVLib.ETRIGGERSRC.TRGENC;
            motorParam7.TriAxis = null;
            motorParam7.UpZ = 0;
            motorParam7.ZPhaseLogic = ProVLib.ZPHASELOGIC.FALLINGEDGE;
            this.MT_HDT_B_AxisX.MotorParameter = motorParam7;
            this.MT_HDT_B_AxisX.MoverSize = ((uint)(0u));
            this.MT_HDT_B_AxisX.MsgID = 0;
            this.MT_HDT_B_AxisX.Name = "MT_HDT_B_AxisX";
            this.MT_HDT_B_AxisX.ObjType = ProVLib.EObjType.AXIS_Y;
            this.MT_HDT_B_AxisX.PitchCOMEnable = false;
            this.MT_HDT_B_AxisX.PriorityHigh = ((uint)(0u));
            this.MT_HDT_B_AxisX.PriorityLow = ((uint)(0u));
            this.MT_HDT_B_AxisX.PulseCtrlMode = ProVLib.EPULSEMODE.CWCCW_2;
            this.MT_HDT_B_AxisX.RelCurrentPos = 0;
            this.MT_HDT_B_AxisX.RelTargetPos = 0;
            this.MT_HDT_B_AxisX.Running = false;
            this.MT_HDT_B_AxisX.SafeDistance = ((uint)(0u));
            this.MT_HDT_B_AxisX.SerialPortName = "COM1";
            this.MT_HDT_B_AxisX.ServoAlarmOn = false;
            this.MT_HDT_B_AxisX.ServoOnPolarity = ProVLib.SVONPOLARITY.ACTIVELOW;
            this.MT_HDT_B_AxisX.Size = new System.Drawing.Size(250, 30);
            this.MT_HDT_B_AxisX.SlaveIOPort = "000";
            this.MT_HDT_B_AxisX.SoftLimitN = -9999999;
            this.MT_HDT_B_AxisX.SoftLimitP = 9999999;
            this.MT_HDT_B_AxisX.Speed = 50000;
            this.MT_HDT_B_AxisX.Text = "(M13) Board HeadB X";
            this.MT_HDT_B_AxisX.TRGSRC = ProVLib.ETRIGGERSRC.TRGENC;
            this.MT_HDT_B_AxisX.UpZ = 0;
            this.MT_HDT_B_AxisX.ZPhaseLogic = ProVLib.ZPHASELOGIC.FALLINGEDGE;
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.Location = new System.Drawing.Point(257, 5);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(50, 30);
            this.button7.TabIndex = 96;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // panel12
            // 
            this.panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel12.Controls.Add(this.button8);
            this.panel12.Controls.Add(this.MT_HDT_B_AxisU);
            this.panel12.Location = new System.Drawing.Point(319, 7);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(310, 40);
            this.panel12.TabIndex = 96;
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button8.Image = ((System.Drawing.Image)(resources.GetObject("button8.Image")));
            this.button8.Location = new System.Drawing.Point(256, 5);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(50, 30);
            this.button8.TabIndex = 95;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // MT_HDT_B_AxisU
            // 
            this.MT_HDT_B_AxisU.Acceleration = 300000;
            this.MT_HDT_B_AxisU.AcceptDiffRange = ((uint)(0u));
            this.MT_HDT_B_AxisU.AddressID = 0;
            this.MT_HDT_B_AxisU.AlarmPolarity = ProVLib.ALMPOLARITY.ACTIVELOW;
            this.MT_HDT_B_AxisU.BackColor = System.Drawing.Color.RoyalBlue;
            this.MT_HDT_B_AxisU.BasePulseCount = 0;
            this.MT_HDT_B_AxisU.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MT_HDT_B_AxisU.CMPSRC = ProVLib.ECMPSRC.CMPSRC_1;
            this.MT_HDT_B_AxisU.D2 = ((byte)(0));
            this.MT_HDT_B_AxisU.D4 = ((byte)(0));
            this.MT_HDT_B_AxisU.Deceleration = 300000;
            this.MT_HDT_B_AxisU.DelayTime = 0D;
            this.MT_HDT_B_AxisU.Direction = true;
            this.MT_HDT_B_AxisU.DownZ1 = 0;
            this.MT_HDT_B_AxisU.DownZ2 = 0;
            this.MT_HDT_B_AxisU.DownZ3 = 0;
            this.MT_HDT_B_AxisU.EncGearRatio = 1D;
            this.MT_HDT_B_AxisU.EndX = 0;
            this.MT_HDT_B_AxisU.ErrID = 0;
            this.MT_HDT_B_AxisU.GearRatio = 1D;
            this.MT_HDT_B_AxisU.GroupNo = ((short)(0));
            this.MT_HDT_B_AxisU.HomeBeforeGoto = false;
            this.MT_HDT_B_AxisU.HomeDirection = true;
            this.MT_HDT_B_AxisU.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            this.MT_HDT_B_AxisU.HomeOK = false;
            this.MT_HDT_B_AxisU.HomePos = 100;
            this.MT_HDT_B_AxisU.InAlarm = false;
            this.MT_HDT_B_AxisU.InitialPosition = 0;
            this.MT_HDT_B_AxisU.InitSpeed = 100;
            this.MT_HDT_B_AxisU.InPosOn = false;
            this.MT_HDT_B_AxisU.InposRange = 50;
            this.MT_HDT_B_AxisU.IOPort = "014";
            this.MT_HDT_B_AxisU.IsELSensorB = true;
            this.MT_HDT_B_AxisU.IsSensorB = true;
            this.MT_HDT_B_AxisU.IsUseMileage = false;
            this.MT_HDT_B_AxisU.IsUseSoftLimit = false;
            this.MT_HDT_B_AxisU.JogHighSpeed = 30000;
            this.MT_HDT_B_AxisU.JogLowSpeed = 1000;
            this.MT_HDT_B_AxisU.LimitX = 0;
            this.MT_HDT_B_AxisU.LimitZ = 0;
            this.MT_HDT_B_AxisU.LineID = ((uint)(0u));
            this.MT_HDT_B_AxisU.Location = new System.Drawing.Point(5, 5);
            this.MT_HDT_B_AxisU.LockUI = false;
            this.MT_HDT_B_AxisU.Message = null;
            this.MT_HDT_B_AxisU.Mileage = 0F;
            this.MT_HDT_B_AxisU.MNETSpeed = ProVLib.EMNETSPEED.MNET10M;
            motorParam8.Acceleration = 300000;
            motorParam8.AddressID = 0;
            motorParam8.AlarmPolarity = ProVLib.ALMPOLARITY.ACTIVELOW;
            motorParam8.BasePulseCount = 0;
            motorParam8.CMPSRC = ProVLib.ECMPSRC.CMPSRC_1;
            motorParam8.Deceleration = 300000;
            motorParam8.DelayT = 0D;
            motorParam8.Direction = true;
            motorParam8.DownZ1 = 0;
            motorParam8.DownZ2 = 0;
            motorParam8.DownZ3 = 0;
            motorParam8.EncGearRatio = 1D;
            motorParam8.EndX = 0;
            motorParam8.GearRatio = 1D;
            motorParam8.GroupNo = ((short)(0));
            motorParam8.HomeBeforeGoto = false;
            motorParam8.HomeDirection = true;
            motorParam8.HomeMode = ProVLib.HOMEMODE.LIMITSNR;
            motorParam8.HomePos = 100;
            motorParam8.IDZ = ((short)(0));
            motorParam8.InitSpeed = 100;
            motorParam8.InPosOn = false;
            motorParam8.InposRange = 50;
            motorParam8.IOPort = "014";
            motorParam8.IsBusy = false;
            motorParam8.IsELSensorB = true;
            motorParam8.IsSensorB = true;
            motorParam8.IsUseSoftLimit = false;
            motorParam8.JogHighSpeed = 30000;
            motorParam8.JogLowSpeed = 1000;
            motorParam8.LimitX = 0;
            motorParam8.LimitZ = 0;
            motorParam8.MNETSpeed = ProVLib.EMNETSPEED.MNET10M;
            motorParam8.MotionCard = ProVLib.EMOTIONCARD.MCMNET;
            motorParam8.MotorType = ProVLib.MOTORTYPE.NORMAL;
            motorParam8.ObjType = 0;
            motorParam8.Owner = null;
            motorParam8.PitchCOMEnable = false;
            motorParam8.PulseCtrlMode = ProVLib.EPULSEMODE.CWCCW_2;
            motorParam8.SerialPortName = "COM1";
            motorParam8.ServoAlarmOn = false;
            motorParam8.ServoOnPolarity = ProVLib.SVONPOLARITY.ACTIVELOW;
            motorParam8.SlaveIOPort = "000";
            motorParam8.SoftLimitN = -9999999;
            motorParam8.SoftLimitP = 9999999;
            motorParam8.Speed = 50000;
            motorParam8.TRGSRC = ProVLib.ETRIGGERSRC.TRGENC;
            motorParam8.TriAxis = null;
            motorParam8.UpZ = 0;
            motorParam8.ZPhaseLogic = ProVLib.ZPHASELOGIC.FALLINGEDGE;
            this.MT_HDT_B_AxisU.MotorParameter = motorParam8;
            this.MT_HDT_B_AxisU.MoverSize = ((uint)(0u));
            this.MT_HDT_B_AxisU.MsgID = 0;
            this.MT_HDT_B_AxisU.Name = "MT_HDT_B_AxisU";
            this.MT_HDT_B_AxisU.ObjType = ProVLib.EObjType.AXIS_Y;
            this.MT_HDT_B_AxisU.PitchCOMEnable = false;
            this.MT_HDT_B_AxisU.PriorityHigh = ((uint)(0u));
            this.MT_HDT_B_AxisU.PriorityLow = ((uint)(0u));
            this.MT_HDT_B_AxisU.PulseCtrlMode = ProVLib.EPULSEMODE.CWCCW_2;
            this.MT_HDT_B_AxisU.RelCurrentPos = 0;
            this.MT_HDT_B_AxisU.RelTargetPos = 0;
            this.MT_HDT_B_AxisU.Running = false;
            this.MT_HDT_B_AxisU.SafeDistance = ((uint)(0u));
            this.MT_HDT_B_AxisU.SerialPortName = "COM1";
            this.MT_HDT_B_AxisU.ServoAlarmOn = false;
            this.MT_HDT_B_AxisU.ServoOnPolarity = ProVLib.SVONPOLARITY.ACTIVELOW;
            this.MT_HDT_B_AxisU.Size = new System.Drawing.Size(250, 30);
            this.MT_HDT_B_AxisU.SlaveIOPort = "000";
            this.MT_HDT_B_AxisU.SoftLimitN = -9999999;
            this.MT_HDT_B_AxisU.SoftLimitP = 9999999;
            this.MT_HDT_B_AxisU.Speed = 50000;
            this.MT_HDT_B_AxisU.Text = "(M15) Board HeadB U";
            this.MT_HDT_B_AxisU.TRGSRC = ProVLib.ETRIGGERSRC.TRGENC;
            this.MT_HDT_B_AxisU.UpZ = 0;
            this.MT_HDT_B_AxisU.ZPhaseLogic = ProVLib.ZPHASELOGIC.FALLINGEDGE;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.FC_HDT_A);
            this.tabControl2.Controls.Add(this.FC_HDT_B);
            this.tabControl2.Location = new System.Drawing.Point(0, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1061, 624);
            this.tabControl2.TabIndex = 1;
            // 
            // FC_HDT_A
            // 
            this.FC_HDT_A.Controls.Add(this.flowChart88);
            this.FC_HDT_A.Controls.Add(this.flowChart86);
            this.FC_HDT_A.Controls.Add(this.flowChart84);
            this.FC_HDT_A.Controls.Add(this.flowChart45);
            this.FC_HDT_A.Controls.Add(this.flowChart44);
            this.FC_HDT_A.Controls.Add(this.flowChart43);
            this.FC_HDT_A.Controls.Add(this.flowChart42);
            this.FC_HDT_A.Controls.Add(this.flowChart41);
            this.FC_HDT_A.Controls.Add(this.flowChart40);
            this.FC_HDT_A.Controls.Add(this.flowChart39);
            this.FC_HDT_A.Controls.Add(this.flowChart38);
            this.FC_HDT_A.Controls.Add(this.flowChart37);
            this.FC_HDT_A.Controls.Add(this.flowChart36);
            this.FC_HDT_A.Controls.Add(this.flowChart35);
            this.FC_HDT_A.Controls.Add(this.flowChart34);
            this.FC_HDT_A.Controls.Add(this.flowChart33);
            this.FC_HDT_A.Controls.Add(this.flowChart32);
            this.FC_HDT_A.Controls.Add(this.flowChart31);
            this.FC_HDT_A.Controls.Add(this.flowChart30);
            this.FC_HDT_A.Controls.Add(this.flowChart29);
            this.FC_HDT_A.Controls.Add(this.flowChart25);
            this.FC_HDT_A.Controls.Add(this.flowChart24);
            this.FC_HDT_A.Controls.Add(this.flowChart23);
            this.FC_HDT_A.Controls.Add(this.flowChart22);
            this.FC_HDT_A.Controls.Add(this.flowChart21);
            this.FC_HDT_A.Controls.Add(this.flowChart20);
            this.FC_HDT_A.Controls.Add(this.flowChart19);
            this.FC_HDT_A.Controls.Add(this.FC_HDT_A_AUTO_Inspection);
            this.FC_HDT_A.Controls.Add(this.FC_HDT_A_AUTO_Place);
            this.FC_HDT_A.Controls.Add(this.flowChart18);
            this.FC_HDT_A.Controls.Add(this.flowChart16);
            this.FC_HDT_A.Controls.Add(this.flowChart15);
            this.FC_HDT_A.Controls.Add(this.flowChart14);
            this.FC_HDT_A.Controls.Add(this.flowChart13);
            this.FC_HDT_A.Controls.Add(this.flowChart12);
            this.FC_HDT_A.Controls.Add(this.flowChart11);
            this.FC_HDT_A.Controls.Add(this.flowChart10);
            this.FC_HDT_A.Controls.Add(this.FC_HDT_A_AUTO_Pick);
            this.FC_HDT_A.Controls.Add(this.flowChart17);
            this.FC_HDT_A.Controls.Add(this.flowChart28);
            this.FC_HDT_A.Controls.Add(this.flowChart27);
            this.FC_HDT_A.Controls.Add(this.flowChart26);
            this.FC_HDT_A.Controls.Add(this.FC_HDT_A_AUTO_Move);
            this.FC_HDT_A.Location = new System.Drawing.Point(4, 35);
            this.FC_HDT_A.Name = "FC_HDT_A";
            this.FC_HDT_A.Padding = new System.Windows.Forms.Padding(3);
            this.FC_HDT_A.Size = new System.Drawing.Size(1053, 585);
            this.FC_HDT_A.TabIndex = 0;
            this.FC_HDT_A.Text = "HDT_A";
            this.FC_HDT_A.UseVisualStyleBackColor = true;
            // 
            // flowChart88
            // 
            this.flowChart88.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart88.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart88.CASE1 = null;
            this.flowChart88.CASE2 = null;
            this.flowChart88.CASE3 = null;
            this.flowChart88.CASE4 = null;
            this.flowChart88.ContinueRun = false;
            this.flowChart88.EndFC = null;
            this.flowChart88.ErrID = 0;
            this.flowChart88.InAlarm = false;
            this.flowChart88.IsFlowHead = false;
            this.flowChart88.Location = new System.Drawing.Point(49, 211);
            this.flowChart88.LockUI = false;
            this.flowChart88.Message = null;
            this.flowChart88.MsgID = 0;
            this.flowChart88.Name = "flowChart88";
            this.flowChart88.NEXT = this.flowChart13;
            this.flowChart88.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart88.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart88.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart88.OverTimeSpec = 100;
            this.flowChart88.Running = false;
            this.flowChart88.Size = new System.Drawing.Size(200, 30);
            this.flowChart88.SlowRunCycle = -1;
            this.flowChart88.StartFC = null;
            this.flowChart88.Text = "Axis Z to PickPos + Offset";
            this.flowChart88.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart88_Run);
            // 
            // flowChart13
            // 
            this.flowChart13.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart13.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart13.CASE1 = null;
            this.flowChart13.CASE2 = null;
            this.flowChart13.CASE3 = null;
            this.flowChart13.CASE4 = null;
            this.flowChart13.ContinueRun = false;
            this.flowChart13.EndFC = null;
            this.flowChart13.ErrID = 0;
            this.flowChart13.InAlarm = false;
            this.flowChart13.IsFlowHead = false;
            this.flowChart13.Location = new System.Drawing.Point(49, 249);
            this.flowChart13.LockUI = false;
            this.flowChart13.Message = null;
            this.flowChart13.MsgID = 0;
            this.flowChart13.Name = "flowChart13";
            this.flowChart13.NEXT = this.flowChart14;
            this.flowChart13.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart13.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart13.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart13.OverTimeSpec = 100;
            this.flowChart13.Running = false;
            this.flowChart13.Size = new System.Drawing.Size(200, 30);
            this.flowChart13.SlowRunCycle = -1;
            this.flowChart13.StartFC = null;
            this.flowChart13.Text = "Axis Z to Safety";
            this.flowChart13.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart13_Run);
            // 
            // flowChart14
            // 
            this.flowChart14.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart14.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart14.CASE1 = this.flowChart15;
            this.flowChart14.CASE2 = null;
            this.flowChart14.CASE3 = null;
            this.flowChart14.CASE4 = null;
            this.flowChart14.ContinueRun = false;
            this.flowChart14.EndFC = null;
            this.flowChart14.ErrID = 0;
            this.flowChart14.InAlarm = false;
            this.flowChart14.IsFlowHead = false;
            this.flowChart14.Location = new System.Drawing.Point(49, 287);
            this.flowChart14.LockUI = false;
            this.flowChart14.Message = null;
            this.flowChart14.MsgID = 0;
            this.flowChart14.Name = "flowChart14";
            this.flowChart14.NEXT = this.flowChart16;
            this.flowChart14.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart14.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart14.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart14.OverTimeSpec = 100;
            this.flowChart14.Running = false;
            this.flowChart14.Size = new System.Drawing.Size(200, 30);
            this.flowChart14.SlowRunCycle = -1;
            this.flowChart14.StartFC = null;
            this.flowChart14.Text = "Check Using Vacuum Value";
            this.flowChart14.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart14_Run);
            // 
            // flowChart15
            // 
            this.flowChart15.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart15.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart15.CASE1 = this.flowChart16;
            this.flowChart15.CASE2 = null;
            this.flowChart15.CASE3 = null;
            this.flowChart15.CASE4 = null;
            this.flowChart15.ContinueRun = false;
            this.flowChart15.EndFC = null;
            this.flowChart15.ErrID = 0;
            this.flowChart15.InAlarm = false;
            this.flowChart15.IsFlowHead = false;
            this.flowChart15.Location = new System.Drawing.Point(264, 287);
            this.flowChart15.LockUI = false;
            this.flowChart15.Message = null;
            this.flowChart15.MsgID = 0;
            this.flowChart15.Name = "flowChart15";
            this.flowChart15.NEXT = this.flowChart10;
            this.flowChart15.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart15.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart15.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart15.OverTimeSpec = 100;
            this.flowChart15.Running = false;
            this.flowChart15.Size = new System.Drawing.Size(114, 30);
            this.flowChart15.SlowRunCycle = -1;
            this.flowChart15.StartFC = null;
            this.flowChart15.Text = "Retry or Ignore";
            this.flowChart15.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart15_Run);
            // 
            // flowChart16
            // 
            this.flowChart16.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart16.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart16.CASE1 = null;
            this.flowChart16.CASE2 = null;
            this.flowChart16.CASE3 = null;
            this.flowChart16.CASE4 = null;
            this.flowChart16.ContinueRun = false;
            this.flowChart16.EndFC = null;
            this.flowChart16.ErrID = 0;
            this.flowChart16.InAlarm = false;
            this.flowChart16.IsFlowHead = false;
            this.flowChart16.Location = new System.Drawing.Point(49, 325);
            this.flowChart16.LockUI = false;
            this.flowChart16.Message = null;
            this.flowChart16.MsgID = 0;
            this.flowChart16.Name = "flowChart16";
            this.flowChart16.NEXT = this.flowChart18;
            this.flowChart16.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart16.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart16.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart16.OverTimeSpec = 100;
            this.flowChart16.Running = false;
            this.flowChart16.Size = new System.Drawing.Size(200, 30);
            this.flowChart16.SlowRunCycle = -1;
            this.flowChart16.StartFC = null;
            this.flowChart16.Text = "Vac. Failure Vac. Off";
            this.flowChart16.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart16_Run);
            // 
            // flowChart18
            // 
            this.flowChart18.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart18.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart18.CASE1 = null;
            this.flowChart18.CASE2 = null;
            this.flowChart18.CASE3 = null;
            this.flowChart18.CASE4 = null;
            this.flowChart18.ContinueRun = false;
            this.flowChart18.EndFC = null;
            this.flowChart18.ErrID = 0;
            this.flowChart18.InAlarm = false;
            this.flowChart18.IsFlowHead = false;
            this.flowChart18.Location = new System.Drawing.Point(49, 363);
            this.flowChart18.LockUI = false;
            this.flowChart18.Message = null;
            this.flowChart18.MsgID = 0;
            this.flowChart18.Name = "flowChart18";
            this.flowChart18.NEXT = this.flowChart37;
            this.flowChart18.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart18.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart18.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart18.OverTimeSpec = 100;
            this.flowChart18.Running = false;
            this.flowChart18.Size = new System.Drawing.Size(200, 30);
            this.flowChart18.SlowRunCycle = -1;
            this.flowChart18.StartFC = null;
            this.flowChart18.Text = "Pick Done";
            this.flowChart18.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart18_Run);
            // 
            // flowChart37
            // 
            this.flowChart37.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart37.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart37.CASE1 = null;
            this.flowChart37.CASE2 = null;
            this.flowChart37.CASE3 = null;
            this.flowChart37.CASE4 = null;
            this.flowChart37.ContinueRun = false;
            this.flowChart37.EndFC = null;
            this.flowChart37.ErrID = 0;
            this.flowChart37.InAlarm = false;
            this.flowChart37.IsFlowHead = false;
            this.flowChart37.Location = new System.Drawing.Point(6, 173);
            this.flowChart37.LockUI = false;
            this.flowChart37.Message = null;
            this.flowChart37.MsgID = 0;
            this.flowChart37.Name = "flowChart37";
            this.flowChart37.NEXT = this.FC_HDT_A_AUTO_Pick;
            this.flowChart37.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart37.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart37.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart37.OverTimeSpec = 100;
            this.flowChart37.Running = false;
            this.flowChart37.Size = new System.Drawing.Size(37, 30);
            this.flowChart37.SlowRunCycle = -1;
            this.flowChart37.StartFC = null;
            this.flowChart37.Text = "Next";
            this.flowChart37.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart37_Run);
            // 
            // FC_HDT_A_AUTO_Pick
            // 
            this.FC_HDT_A_AUTO_Pick.BackColor = System.Drawing.Color.RoyalBlue;
            this.FC_HDT_A_AUTO_Pick.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.FC_HDT_A_AUTO_Pick.CASE1 = null;
            this.FC_HDT_A_AUTO_Pick.CASE2 = null;
            this.FC_HDT_A_AUTO_Pick.CASE3 = null;
            this.FC_HDT_A_AUTO_Pick.CASE4 = null;
            this.FC_HDT_A_AUTO_Pick.ContinueRun = false;
            this.FC_HDT_A_AUTO_Pick.EndFC = null;
            this.FC_HDT_A_AUTO_Pick.ErrID = 0;
            this.FC_HDT_A_AUTO_Pick.InAlarm = false;
            this.FC_HDT_A_AUTO_Pick.IsFlowHead = false;
            this.FC_HDT_A_AUTO_Pick.Location = new System.Drawing.Point(49, 21);
            this.FC_HDT_A_AUTO_Pick.LockUI = false;
            this.FC_HDT_A_AUTO_Pick.Message = null;
            this.FC_HDT_A_AUTO_Pick.MsgID = 0;
            this.FC_HDT_A_AUTO_Pick.Name = "FC_HDT_A_AUTO_Pick";
            this.FC_HDT_A_AUTO_Pick.NEXT = this.flowChart84;
            this.FC_HDT_A_AUTO_Pick.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.FC_HDT_A_AUTO_Pick.OrgLocation = new System.Drawing.Point(0, 0);
            this.FC_HDT_A_AUTO_Pick.OrgSize = new System.Drawing.Size(0, 0);
            this.FC_HDT_A_AUTO_Pick.OverTimeSpec = 100;
            this.FC_HDT_A_AUTO_Pick.Running = false;
            this.FC_HDT_A_AUTO_Pick.Size = new System.Drawing.Size(200, 30);
            this.FC_HDT_A_AUTO_Pick.SlowRunCycle = -1;
            this.FC_HDT_A_AUTO_Pick.StartFC = null;
            this.FC_HDT_A_AUTO_Pick.Text = "Start Pick";
            this.FC_HDT_A_AUTO_Pick.Run += new ProVLib.FlowChart.RunEventHandler(this.FC_HDT_A_AUTO_Pick_Run);
            // 
            // flowChart84
            // 
            this.flowChart84.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart84.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart84.CASE1 = null;
            this.flowChart84.CASE2 = null;
            this.flowChart84.CASE3 = null;
            this.flowChart84.CASE4 = null;
            this.flowChart84.ContinueRun = false;
            this.flowChart84.EndFC = null;
            this.flowChart84.ErrID = 0;
            this.flowChart84.InAlarm = false;
            this.flowChart84.IsFlowHead = false;
            this.flowChart84.Location = new System.Drawing.Point(49, 59);
            this.flowChart84.LockUI = false;
            this.flowChart84.Message = null;
            this.flowChart84.MsgID = 0;
            this.flowChart84.Name = "flowChart84";
            this.flowChart84.NEXT = this.flowChart10;
            this.flowChart84.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart84.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart84.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart84.OverTimeSpec = 100;
            this.flowChart84.Running = false;
            this.flowChart84.Size = new System.Drawing.Size(200, 30);
            this.flowChart84.SlowRunCycle = -1;
            this.flowChart84.StartFC = null;
            this.flowChart84.Text = "Move U to Zero";
            this.flowChart84.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart84_Run);
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
            this.flowChart10.Location = new System.Drawing.Point(49, 97);
            this.flowChart10.LockUI = false;
            this.flowChart10.Message = null;
            this.flowChart10.MsgID = 0;
            this.flowChart10.Name = "flowChart10";
            this.flowChart10.NEXT = this.flowChart11;
            this.flowChart10.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart10.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart10.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart10.OverTimeSpec = 100;
            this.flowChart10.Running = false;
            this.flowChart10.Size = new System.Drawing.Size(200, 30);
            this.flowChart10.SlowRunCycle = -1;
            this.flowChart10.StartFC = null;
            this.flowChart10.Text = "Axis Z to PickPos + Offset";
            this.flowChart10.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart10_Run);
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
            this.flowChart11.Location = new System.Drawing.Point(49, 135);
            this.flowChart11.LockUI = false;
            this.flowChart11.Message = null;
            this.flowChart11.MsgID = 0;
            this.flowChart11.Name = "flowChart11";
            this.flowChart11.NEXT = this.flowChart12;
            this.flowChart11.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart11.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart11.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart11.OverTimeSpec = 100;
            this.flowChart11.Running = false;
            this.flowChart11.Size = new System.Drawing.Size(200, 30);
            this.flowChart11.SlowRunCycle = -1;
            this.flowChart11.StartFC = null;
            this.flowChart11.Text = "Axis Z to PickPos ";
            this.flowChart11.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart11_Run);
            // 
            // flowChart12
            // 
            this.flowChart12.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart12.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart12.CASE1 = null;
            this.flowChart12.CASE2 = null;
            this.flowChart12.CASE3 = null;
            this.flowChart12.CASE4 = null;
            this.flowChart12.ContinueRun = false;
            this.flowChart12.EndFC = null;
            this.flowChart12.ErrID = 0;
            this.flowChart12.InAlarm = false;
            this.flowChart12.IsFlowHead = false;
            this.flowChart12.Location = new System.Drawing.Point(49, 173);
            this.flowChart12.LockUI = false;
            this.flowChart12.Message = null;
            this.flowChart12.MsgID = 0;
            this.flowChart12.Name = "flowChart12";
            this.flowChart12.NEXT = this.flowChart88;
            this.flowChart12.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart12.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart12.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart12.OverTimeSpec = 100;
            this.flowChart12.Running = false;
            this.flowChart12.Size = new System.Drawing.Size(200, 30);
            this.flowChart12.SlowRunCycle = -1;
            this.flowChart12.StartFC = null;
            this.flowChart12.Text = "Using Vacuum On";
            this.flowChart12.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart12_Run);
            // 
            // flowChart86
            // 
            this.flowChart86.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart86.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart86.CASE1 = this.flowChart40;
            this.flowChart86.CASE2 = null;
            this.flowChart86.CASE3 = null;
            this.flowChart86.CASE4 = null;
            this.flowChart86.ContinueRun = false;
            this.flowChart86.EndFC = null;
            this.flowChart86.ErrID = 0;
            this.flowChart86.InAlarm = false;
            this.flowChart86.IsFlowHead = false;
            this.flowChart86.Location = new System.Drawing.Point(652, 439);
            this.flowChart86.LockUI = false;
            this.flowChart86.Message = null;
            this.flowChart86.MsgID = 0;
            this.flowChart86.Name = "flowChart86";
            this.flowChart86.NEXT = this.flowChart43;
            this.flowChart86.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart86.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart86.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart86.OverTimeSpec = 100;
            this.flowChart86.Running = false;
            this.flowChart86.Size = new System.Drawing.Size(114, 30);
            this.flowChart86.SlowRunCycle = -1;
            this.flowChart86.StartFC = null;
            this.flowChart86.Text = "Inspect OK?";
            this.flowChart86.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart86_Run);
            // 
            // flowChart40
            // 
            this.flowChart40.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart40.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart40.CASE1 = null;
            this.flowChart40.CASE2 = null;
            this.flowChart40.CASE3 = null;
            this.flowChart40.CASE4 = null;
            this.flowChart40.ContinueRun = false;
            this.flowChart40.EndFC = null;
            this.flowChart40.ErrID = 0;
            this.flowChart40.InAlarm = false;
            this.flowChart40.IsFlowHead = false;
            this.flowChart40.Location = new System.Drawing.Point(783, 401);
            this.flowChart40.LockUI = false;
            this.flowChart40.Message = null;
            this.flowChart40.MsgID = 0;
            this.flowChart40.Name = "flowChart40";
            this.flowChart40.NEXT = this.flowChart41;
            this.flowChart40.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart40.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart40.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart40.OverTimeSpec = 100;
            this.flowChart40.Running = false;
            this.flowChart40.Size = new System.Drawing.Size(200, 30);
            this.flowChart40.SlowRunCycle = -1;
            this.flowChart40.StartFC = null;
            this.flowChart40.Text = "Reset Status";
            this.flowChart40.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart40_Run);
            // 
            // flowChart41
            // 
            this.flowChart41.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart41.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart41.CASE1 = null;
            this.flowChart41.CASE2 = null;
            this.flowChart41.CASE3 = null;
            this.flowChart41.CASE4 = null;
            this.flowChart41.ContinueRun = false;
            this.flowChart41.EndFC = null;
            this.flowChart41.ErrID = 0;
            this.flowChart41.InAlarm = false;
            this.flowChart41.IsFlowHead = false;
            this.flowChart41.Location = new System.Drawing.Point(783, 439);
            this.flowChart41.LockUI = false;
            this.flowChart41.Message = null;
            this.flowChart41.MsgID = 0;
            this.flowChart41.Name = "flowChart41";
            this.flowChart41.NEXT = this.flowChart42;
            this.flowChart41.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart41.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart41.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart41.OverTimeSpec = 100;
            this.flowChart41.Running = false;
            this.flowChart41.Size = new System.Drawing.Size(200, 30);
            this.flowChart41.SlowRunCycle = -1;
            this.flowChart41.StartFC = null;
            this.flowChart41.Text = "Set command";
            this.flowChart41.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart41_Run);
            // 
            // flowChart42
            // 
            this.flowChart42.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart42.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart42.CASE1 = this.flowChart40;
            this.flowChart42.CASE2 = null;
            this.flowChart42.CASE3 = null;
            this.flowChart42.CASE4 = null;
            this.flowChart42.ContinueRun = false;
            this.flowChart42.EndFC = null;
            this.flowChart42.ErrID = 0;
            this.flowChart42.InAlarm = false;
            this.flowChart42.IsFlowHead = false;
            this.flowChart42.Location = new System.Drawing.Point(652, 401);
            this.flowChart42.LockUI = false;
            this.flowChart42.Message = null;
            this.flowChart42.MsgID = 0;
            this.flowChart42.Name = "flowChart42";
            this.flowChart42.NEXT = this.flowChart86;
            this.flowChart42.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart42.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart42.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart42.OverTimeSpec = 100;
            this.flowChart42.Running = false;
            this.flowChart42.Size = new System.Drawing.Size(114, 30);
            this.flowChart42.SlowRunCycle = -1;
            this.flowChart42.StartFC = null;
            this.flowChart42.Text = "Wait Status";
            this.flowChart42.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart42_Run);
            // 
            // flowChart43
            // 
            this.flowChart43.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart43.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart43.CASE1 = this.flowChart40;
            this.flowChart43.CASE2 = null;
            this.flowChart43.CASE3 = null;
            this.flowChart43.CASE4 = null;
            this.flowChart43.ContinueRun = false;
            this.flowChart43.EndFC = null;
            this.flowChart43.ErrID = 0;
            this.flowChart43.InAlarm = false;
            this.flowChart43.IsFlowHead = false;
            this.flowChart43.Location = new System.Drawing.Point(652, 477);
            this.flowChart43.LockUI = false;
            this.flowChart43.Message = null;
            this.flowChart43.MsgID = 0;
            this.flowChart43.Name = "flowChart43";
            this.flowChart43.NEXT = this.flowChart44;
            this.flowChart43.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart43.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart43.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart43.OverTimeSpec = 100;
            this.flowChart43.Running = false;
            this.flowChart43.Size = new System.Drawing.Size(114, 30);
            this.flowChart43.SlowRunCycle = -1;
            this.flowChart43.StartFC = null;
            this.flowChart43.Text = "Get Result";
            this.flowChart43.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart43_Run);
            // 
            // flowChart44
            // 
            this.flowChart44.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart44.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart44.CASE1 = null;
            this.flowChart44.CASE2 = null;
            this.flowChart44.CASE3 = null;
            this.flowChart44.CASE4 = null;
            this.flowChart44.ContinueRun = false;
            this.flowChart44.EndFC = null;
            this.flowChart44.ErrID = 0;
            this.flowChart44.InAlarm = false;
            this.flowChart44.IsFlowHead = false;
            this.flowChart44.Location = new System.Drawing.Point(783, 477);
            this.flowChart44.LockUI = false;
            this.flowChart44.Message = null;
            this.flowChart44.MsgID = 0;
            this.flowChart44.Name = "flowChart44";
            this.flowChart44.NEXT = this.flowChart45;
            this.flowChart44.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart44.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart44.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart44.OverTimeSpec = 100;
            this.flowChart44.Running = false;
            this.flowChart44.Size = new System.Drawing.Size(200, 30);
            this.flowChart44.SlowRunCycle = -1;
            this.flowChart44.StartFC = null;
            this.flowChart44.Text = "Inspection Finished";
            this.flowChart44.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart44_Run);
            // 
            // flowChart45
            // 
            this.flowChart45.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart45.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart45.CASE1 = null;
            this.flowChart45.CASE2 = null;
            this.flowChart45.CASE3 = null;
            this.flowChart45.CASE4 = null;
            this.flowChart45.ContinueRun = false;
            this.flowChart45.EndFC = null;
            this.flowChart45.ErrID = 0;
            this.flowChart45.InAlarm = false;
            this.flowChart45.IsFlowHead = false;
            this.flowChart45.Location = new System.Drawing.Point(989, 363);
            this.flowChart45.LockUI = false;
            this.flowChart45.Message = null;
            this.flowChart45.MsgID = 0;
            this.flowChart45.Name = "flowChart45";
            this.flowChart45.NEXT = this.flowChart39;
            this.flowChart45.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart45.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart45.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart45.OverTimeSpec = 100;
            this.flowChart45.Running = false;
            this.flowChart45.Size = new System.Drawing.Size(37, 30);
            this.flowChart45.SlowRunCycle = -1;
            this.flowChart45.StartFC = null;
            this.flowChart45.Text = "Next";
            this.flowChart45.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart45_Run);
            // 
            // flowChart39
            // 
            this.flowChart39.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart39.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart39.CASE1 = null;
            this.flowChart39.CASE2 = null;
            this.flowChart39.CASE3 = null;
            this.flowChart39.CASE4 = null;
            this.flowChart39.ContinueRun = false;
            this.flowChart39.EndFC = null;
            this.flowChart39.ErrID = 0;
            this.flowChart39.InAlarm = false;
            this.flowChart39.IsFlowHead = false;
            this.flowChart39.Location = new System.Drawing.Point(783, 363);
            this.flowChart39.LockUI = false;
            this.flowChart39.Message = null;
            this.flowChart39.MsgID = 0;
            this.flowChart39.Name = "flowChart39";
            this.flowChart39.NEXT = this.flowChart40;
            this.flowChart39.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart39.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart39.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart39.OverTimeSpec = 100;
            this.flowChart39.Running = false;
            this.flowChart39.Size = new System.Drawing.Size(200, 30);
            this.flowChart39.SlowRunCycle = -1;
            this.flowChart39.StartFC = null;
            this.flowChart39.Text = "Wait for command";
            this.flowChart39.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart39_Run);
            // 
            // flowChart38
            // 
            this.flowChart38.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart38.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart38.CASE1 = null;
            this.flowChart38.CASE2 = null;
            this.flowChart38.CASE3 = null;
            this.flowChart38.CASE4 = null;
            this.flowChart38.ContinueRun = false;
            this.flowChart38.EndFC = null;
            this.flowChart38.ErrID = 0;
            this.flowChart38.InAlarm = false;
            this.flowChart38.IsFlowHead = false;
            this.flowChart38.Location = new System.Drawing.Point(384, 249);
            this.flowChart38.LockUI = false;
            this.flowChart38.Message = null;
            this.flowChart38.MsgID = 0;
            this.flowChart38.Name = "flowChart38";
            this.flowChart38.NEXT = this.FC_HDT_A_AUTO_Place;
            this.flowChart38.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart38.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart38.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart38.OverTimeSpec = 100;
            this.flowChart38.Running = false;
            this.flowChart38.Size = new System.Drawing.Size(37, 30);
            this.flowChart38.SlowRunCycle = -1;
            this.flowChart38.StartFC = null;
            this.flowChart38.Text = "Next";
            this.flowChart38.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart38_Run);
            // 
            // FC_HDT_A_AUTO_Place
            // 
            this.FC_HDT_A_AUTO_Place.BackColor = System.Drawing.Color.RoyalBlue;
            this.FC_HDT_A_AUTO_Place.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.FC_HDT_A_AUTO_Place.CASE1 = null;
            this.FC_HDT_A_AUTO_Place.CASE2 = null;
            this.FC_HDT_A_AUTO_Place.CASE3 = null;
            this.FC_HDT_A_AUTO_Place.CASE4 = null;
            this.FC_HDT_A_AUTO_Place.ContinueRun = false;
            this.FC_HDT_A_AUTO_Place.EndFC = null;
            this.FC_HDT_A_AUTO_Place.ErrID = 0;
            this.FC_HDT_A_AUTO_Place.InAlarm = false;
            this.FC_HDT_A_AUTO_Place.IsFlowHead = false;
            this.FC_HDT_A_AUTO_Place.Location = new System.Drawing.Point(427, 21);
            this.FC_HDT_A_AUTO_Place.LockUI = false;
            this.FC_HDT_A_AUTO_Place.Message = null;
            this.FC_HDT_A_AUTO_Place.MsgID = 0;
            this.FC_HDT_A_AUTO_Place.Name = "FC_HDT_A_AUTO_Place";
            this.FC_HDT_A_AUTO_Place.NEXT = this.flowChart19;
            this.FC_HDT_A_AUTO_Place.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.FC_HDT_A_AUTO_Place.OrgLocation = new System.Drawing.Point(0, 0);
            this.FC_HDT_A_AUTO_Place.OrgSize = new System.Drawing.Size(0, 0);
            this.FC_HDT_A_AUTO_Place.OverTimeSpec = 100;
            this.FC_HDT_A_AUTO_Place.Running = false;
            this.FC_HDT_A_AUTO_Place.Size = new System.Drawing.Size(200, 30);
            this.FC_HDT_A_AUTO_Place.SlowRunCycle = -1;
            this.FC_HDT_A_AUTO_Place.StartFC = null;
            this.FC_HDT_A_AUTO_Place.Text = "Start Place";
            this.FC_HDT_A_AUTO_Place.Run += new ProVLib.FlowChart.RunEventHandler(this.FC_HDT_A_AUTO_Place_Run);
            // 
            // flowChart19
            // 
            this.flowChart19.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart19.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart19.CASE1 = this.flowChart20;
            this.flowChart19.CASE2 = null;
            this.flowChart19.CASE3 = null;
            this.flowChart19.CASE4 = null;
            this.flowChart19.ContinueRun = false;
            this.flowChart19.EndFC = null;
            this.flowChart19.ErrID = 0;
            this.flowChart19.InAlarm = false;
            this.flowChart19.IsFlowHead = false;
            this.flowChart19.Location = new System.Drawing.Point(427, 59);
            this.flowChart19.LockUI = false;
            this.flowChart19.Message = null;
            this.flowChart19.MsgID = 0;
            this.flowChart19.Name = "flowChart19";
            this.flowChart19.NEXT = this.flowChart23;
            this.flowChart19.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart19.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart19.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart19.OverTimeSpec = 100;
            this.flowChart19.Running = false;
            this.flowChart19.Size = new System.Drawing.Size(200, 30);
            this.flowChart19.SlowRunCycle = -1;
            this.flowChart19.StartFC = null;
            this.flowChart19.Text = "Check Using Vac.";
            this.flowChart19.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart19_Run);
            // 
            // flowChart20
            // 
            this.flowChart20.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart20.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart20.CASE1 = this.flowChart21;
            this.flowChart20.CASE2 = null;
            this.flowChart20.CASE3 = null;
            this.flowChart20.CASE4 = null;
            this.flowChart20.ContinueRun = false;
            this.flowChart20.EndFC = null;
            this.flowChart20.ErrID = 0;
            this.flowChart20.InAlarm = false;
            this.flowChart20.IsFlowHead = false;
            this.flowChart20.Location = new System.Drawing.Point(642, 59);
            this.flowChart20.LockUI = false;
            this.flowChart20.Message = null;
            this.flowChart20.MsgID = 0;
            this.flowChart20.Name = "flowChart20";
            this.flowChart20.NEXT = this.flowChart22;
            this.flowChart20.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart20.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart20.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart20.OverTimeSpec = 100;
            this.flowChart20.Running = false;
            this.flowChart20.Size = new System.Drawing.Size(114, 30);
            this.flowChart20.SlowRunCycle = -1;
            this.flowChart20.StartFC = null;
            this.flowChart20.Text = "Retry or Ignore";
            this.flowChart20.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart20_Run);
            // 
            // flowChart21
            // 
            this.flowChart21.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart21.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart21.CASE1 = null;
            this.flowChart21.CASE2 = null;
            this.flowChart21.CASE3 = null;
            this.flowChart21.CASE4 = null;
            this.flowChart21.ContinueRun = false;
            this.flowChart21.EndFC = null;
            this.flowChart21.ErrID = 0;
            this.flowChart21.InAlarm = false;
            this.flowChart21.IsFlowHead = false;
            this.flowChart21.Location = new System.Drawing.Point(642, 21);
            this.flowChart21.LockUI = false;
            this.flowChart21.Message = null;
            this.flowChart21.MsgID = 0;
            this.flowChart21.Name = "flowChart21";
            this.flowChart21.NEXT = this.flowChart19;
            this.flowChart21.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart21.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart21.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart21.OverTimeSpec = 100;
            this.flowChart21.Running = false;
            this.flowChart21.Size = new System.Drawing.Size(114, 30);
            this.flowChart21.SlowRunCycle = -1;
            this.flowChart21.StartFC = null;
            this.flowChart21.Text = "Set Nozzle state";
            this.flowChart21.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart21_Run);
            // 
            // flowChart22
            // 
            this.flowChart22.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart22.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart22.CASE1 = null;
            this.flowChart22.CASE2 = null;
            this.flowChart22.CASE3 = null;
            this.flowChart22.CASE4 = null;
            this.flowChart22.ContinueRun = false;
            this.flowChart22.EndFC = null;
            this.flowChart22.ErrID = 0;
            this.flowChart22.InAlarm = false;
            this.flowChart22.IsFlowHead = false;
            this.flowChart22.Location = new System.Drawing.Point(642, 97);
            this.flowChart22.LockUI = false;
            this.flowChart22.Message = null;
            this.flowChart22.MsgID = 0;
            this.flowChart22.Name = "flowChart22";
            this.flowChart22.NEXT = this.flowChart19;
            this.flowChart22.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart22.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart22.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart22.OverTimeSpec = 100;
            this.flowChart22.Running = false;
            this.flowChart22.Size = new System.Drawing.Size(114, 30);
            this.flowChart22.SlowRunCycle = -1;
            this.flowChart22.StartFC = null;
            this.flowChart22.Text = "Retry";
            this.flowChart22.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart22_Run);
            // 
            // flowChart23
            // 
            this.flowChart23.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart23.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart23.CASE1 = null;
            this.flowChart23.CASE2 = null;
            this.flowChart23.CASE3 = null;
            this.flowChart23.CASE4 = null;
            this.flowChart23.ContinueRun = false;
            this.flowChart23.EndFC = null;
            this.flowChart23.ErrID = 0;
            this.flowChart23.InAlarm = false;
            this.flowChart23.IsFlowHead = false;
            this.flowChart23.Location = new System.Drawing.Point(427, 97);
            this.flowChart23.LockUI = false;
            this.flowChart23.Message = null;
            this.flowChart23.MsgID = 0;
            this.flowChart23.Name = "flowChart23";
            this.flowChart23.NEXT = this.flowChart24;
            this.flowChart23.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart23.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart23.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart23.OverTimeSpec = 100;
            this.flowChart23.Running = false;
            this.flowChart23.Size = new System.Drawing.Size(200, 30);
            this.flowChart23.SlowRunCycle = -1;
            this.flowChart23.StartFC = null;
            this.flowChart23.Text = "Axis Z to PlacePos + Offset";
            this.flowChart23.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart23_Run);
            // 
            // flowChart24
            // 
            this.flowChart24.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart24.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart24.CASE1 = null;
            this.flowChart24.CASE2 = null;
            this.flowChart24.CASE3 = null;
            this.flowChart24.CASE4 = null;
            this.flowChart24.ContinueRun = false;
            this.flowChart24.EndFC = null;
            this.flowChart24.ErrID = 0;
            this.flowChart24.InAlarm = false;
            this.flowChart24.IsFlowHead = false;
            this.flowChart24.Location = new System.Drawing.Point(427, 135);
            this.flowChart24.LockUI = false;
            this.flowChart24.Message = null;
            this.flowChart24.MsgID = 0;
            this.flowChart24.Name = "flowChart24";
            this.flowChart24.NEXT = this.flowChart25;
            this.flowChart24.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart24.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart24.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart24.OverTimeSpec = 100;
            this.flowChart24.Running = false;
            this.flowChart24.Size = new System.Drawing.Size(200, 30);
            this.flowChart24.SlowRunCycle = -1;
            this.flowChart24.StartFC = null;
            this.flowChart24.Text = "Axis Z to PlacePos ";
            this.flowChart24.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart24_Run);
            // 
            // flowChart25
            // 
            this.flowChart25.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart25.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart25.CASE1 = null;
            this.flowChart25.CASE2 = null;
            this.flowChart25.CASE3 = null;
            this.flowChart25.CASE4 = null;
            this.flowChart25.ContinueRun = false;
            this.flowChart25.EndFC = null;
            this.flowChart25.ErrID = 0;
            this.flowChart25.InAlarm = false;
            this.flowChart25.IsFlowHead = false;
            this.flowChart25.Location = new System.Drawing.Point(427, 173);
            this.flowChart25.LockUI = false;
            this.flowChart25.Message = null;
            this.flowChart25.MsgID = 0;
            this.flowChart25.Name = "flowChart25";
            this.flowChart25.NEXT = this.flowChart29;
            this.flowChart25.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart25.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart25.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart25.OverTimeSpec = 100;
            this.flowChart25.Running = false;
            this.flowChart25.Size = new System.Drawing.Size(200, 30);
            this.flowChart25.SlowRunCycle = -1;
            this.flowChart25.StartFC = null;
            this.flowChart25.Text = "Before destory delay";
            this.flowChart25.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart25_Run);
            // 
            // flowChart29
            // 
            this.flowChart29.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart29.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart29.CASE1 = null;
            this.flowChart29.CASE2 = null;
            this.flowChart29.CASE3 = null;
            this.flowChart29.CASE4 = null;
            this.flowChart29.ContinueRun = false;
            this.flowChart29.EndFC = null;
            this.flowChart29.ErrID = 0;
            this.flowChart29.InAlarm = false;
            this.flowChart29.IsFlowHead = false;
            this.flowChart29.Location = new System.Drawing.Point(427, 211);
            this.flowChart29.LockUI = false;
            this.flowChart29.Message = null;
            this.flowChart29.MsgID = 0;
            this.flowChart29.Name = "flowChart29";
            this.flowChart29.NEXT = this.flowChart30;
            this.flowChart29.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart29.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart29.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart29.OverTimeSpec = 100;
            this.flowChart29.Running = false;
            this.flowChart29.Size = new System.Drawing.Size(200, 30);
            this.flowChart29.SlowRunCycle = -1;
            this.flowChart29.StartFC = null;
            this.flowChart29.Text = "Destory (on -> delay -> off)";
            this.flowChart29.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart29_Run);
            // 
            // flowChart30
            // 
            this.flowChart30.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart30.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart30.CASE1 = null;
            this.flowChart30.CASE2 = null;
            this.flowChart30.CASE3 = null;
            this.flowChart30.CASE4 = null;
            this.flowChart30.ContinueRun = false;
            this.flowChart30.EndFC = null;
            this.flowChart30.ErrID = 0;
            this.flowChart30.InAlarm = false;
            this.flowChart30.IsFlowHead = false;
            this.flowChart30.Location = new System.Drawing.Point(427, 249);
            this.flowChart30.LockUI = false;
            this.flowChart30.Message = null;
            this.flowChart30.MsgID = 0;
            this.flowChart30.Name = "flowChart30";
            this.flowChart30.NEXT = this.flowChart31;
            this.flowChart30.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart30.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart30.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart30.OverTimeSpec = 100;
            this.flowChart30.Running = false;
            this.flowChart30.Size = new System.Drawing.Size(200, 30);
            this.flowChart30.SlowRunCycle = -1;
            this.flowChart30.StartFC = null;
            this.flowChart30.Text = "After destory delay";
            this.flowChart30.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart30_Run);
            // 
            // flowChart31
            // 
            this.flowChart31.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart31.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart31.CASE1 = null;
            this.flowChart31.CASE2 = null;
            this.flowChart31.CASE3 = null;
            this.flowChart31.CASE4 = null;
            this.flowChart31.ContinueRun = false;
            this.flowChart31.EndFC = null;
            this.flowChart31.ErrID = 0;
            this.flowChart31.InAlarm = false;
            this.flowChart31.IsFlowHead = false;
            this.flowChart31.Location = new System.Drawing.Point(427, 287);
            this.flowChart31.LockUI = false;
            this.flowChart31.Message = null;
            this.flowChart31.MsgID = 0;
            this.flowChart31.Name = "flowChart31";
            this.flowChart31.NEXT = this.flowChart32;
            this.flowChart31.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart31.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart31.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart31.OverTimeSpec = 100;
            this.flowChart31.Running = false;
            this.flowChart31.Size = new System.Drawing.Size(200, 30);
            this.flowChart31.SlowRunCycle = -1;
            this.flowChart31.StartFC = null;
            this.flowChart31.Text = "Axis Z to PlacePos + Offset";
            this.flowChart31.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart31_Run);
            // 
            // flowChart32
            // 
            this.flowChart32.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart32.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart32.CASE1 = null;
            this.flowChart32.CASE2 = null;
            this.flowChart32.CASE3 = null;
            this.flowChart32.CASE4 = null;
            this.flowChart32.ContinueRun = false;
            this.flowChart32.EndFC = null;
            this.flowChart32.ErrID = 0;
            this.flowChart32.InAlarm = false;
            this.flowChart32.IsFlowHead = false;
            this.flowChart32.Location = new System.Drawing.Point(427, 325);
            this.flowChart32.LockUI = false;
            this.flowChart32.Message = null;
            this.flowChart32.MsgID = 0;
            this.flowChart32.Name = "flowChart32";
            this.flowChart32.NEXT = this.flowChart33;
            this.flowChart32.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart32.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart32.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart32.OverTimeSpec = 100;
            this.flowChart32.Running = false;
            this.flowChart32.Size = new System.Drawing.Size(200, 30);
            this.flowChart32.SlowRunCycle = -1;
            this.flowChart32.StartFC = null;
            this.flowChart32.Text = "Axis Z to Safety";
            this.flowChart32.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart32_Run);
            // 
            // flowChart33
            // 
            this.flowChart33.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart33.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart33.CASE1 = null;
            this.flowChart33.CASE2 = null;
            this.flowChart33.CASE3 = null;
            this.flowChart33.CASE4 = null;
            this.flowChart33.ContinueRun = false;
            this.flowChart33.EndFC = null;
            this.flowChart33.ErrID = 0;
            this.flowChart33.InAlarm = false;
            this.flowChart33.IsFlowHead = false;
            this.flowChart33.Location = new System.Drawing.Point(427, 363);
            this.flowChart33.LockUI = false;
            this.flowChart33.Message = null;
            this.flowChart33.MsgID = 0;
            this.flowChart33.Name = "flowChart33";
            this.flowChart33.NEXT = this.flowChart34;
            this.flowChart33.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart33.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart33.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart33.OverTimeSpec = 100;
            this.flowChart33.Running = false;
            this.flowChart33.Size = new System.Drawing.Size(200, 30);
            this.flowChart33.SlowRunCycle = -1;
            this.flowChart33.StartFC = null;
            this.flowChart33.Text = "Open Vac.";
            this.flowChart33.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart33_Run);
            // 
            // flowChart34
            // 
            this.flowChart34.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart34.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart34.CASE1 = null;
            this.flowChart34.CASE2 = null;
            this.flowChart34.CASE3 = null;
            this.flowChart34.CASE4 = null;
            this.flowChart34.ContinueRun = false;
            this.flowChart34.EndFC = null;
            this.flowChart34.ErrID = 0;
            this.flowChart34.InAlarm = false;
            this.flowChart34.IsFlowHead = false;
            this.flowChart34.Location = new System.Drawing.Point(427, 401);
            this.flowChart34.LockUI = false;
            this.flowChart34.Message = null;
            this.flowChart34.MsgID = 0;
            this.flowChart34.Name = "flowChart34";
            this.flowChart34.NEXT = this.flowChart35;
            this.flowChart34.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart34.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart34.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart34.OverTimeSpec = 100;
            this.flowChart34.Running = false;
            this.flowChart34.Size = new System.Drawing.Size(200, 30);
            this.flowChart34.SlowRunCycle = -1;
            this.flowChart34.StartFC = null;
            this.flowChart34.Text = "Check Vac.";
            this.flowChart34.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart34_Run);
            // 
            // flowChart35
            // 
            this.flowChart35.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart35.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart35.CASE1 = null;
            this.flowChart35.CASE2 = null;
            this.flowChart35.CASE3 = null;
            this.flowChart35.CASE4 = null;
            this.flowChart35.ContinueRun = false;
            this.flowChart35.EndFC = null;
            this.flowChart35.ErrID = 0;
            this.flowChart35.InAlarm = false;
            this.flowChart35.IsFlowHead = false;
            this.flowChart35.Location = new System.Drawing.Point(427, 439);
            this.flowChart35.LockUI = false;
            this.flowChart35.Message = null;
            this.flowChart35.MsgID = 0;
            this.flowChart35.Name = "flowChart35";
            this.flowChart35.NEXT = this.flowChart36;
            this.flowChart35.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart35.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart35.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart35.OverTimeSpec = 100;
            this.flowChart35.Running = false;
            this.flowChart35.Size = new System.Drawing.Size(200, 30);
            this.flowChart35.SlowRunCycle = -1;
            this.flowChart35.StartFC = null;
            this.flowChart35.Text = "Close Vac.";
            this.flowChart35.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart35_Run);
            // 
            // flowChart36
            // 
            this.flowChart36.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart36.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart36.CASE1 = null;
            this.flowChart36.CASE2 = null;
            this.flowChart36.CASE3 = null;
            this.flowChart36.CASE4 = null;
            this.flowChart36.ContinueRun = false;
            this.flowChart36.EndFC = null;
            this.flowChart36.ErrID = 0;
            this.flowChart36.InAlarm = false;
            this.flowChart36.IsFlowHead = false;
            this.flowChart36.Location = new System.Drawing.Point(427, 477);
            this.flowChart36.LockUI = false;
            this.flowChart36.Message = null;
            this.flowChart36.MsgID = 0;
            this.flowChart36.Name = "flowChart36";
            this.flowChart36.NEXT = this.flowChart38;
            this.flowChart36.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart36.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart36.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart36.OverTimeSpec = 100;
            this.flowChart36.Running = false;
            this.flowChart36.Size = new System.Drawing.Size(200, 30);
            this.flowChart36.SlowRunCycle = -1;
            this.flowChart36.StartFC = null;
            this.flowChart36.Text = "Place Done";
            this.flowChart36.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart36_Run);
            // 
            // FC_HDT_A_AUTO_Inspection
            // 
            this.FC_HDT_A_AUTO_Inspection.BackColor = System.Drawing.Color.RoyalBlue;
            this.FC_HDT_A_AUTO_Inspection.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.FC_HDT_A_AUTO_Inspection.CASE1 = null;
            this.FC_HDT_A_AUTO_Inspection.CASE2 = null;
            this.FC_HDT_A_AUTO_Inspection.CASE3 = null;
            this.FC_HDT_A_AUTO_Inspection.CASE4 = null;
            this.FC_HDT_A_AUTO_Inspection.ContinueRun = false;
            this.FC_HDT_A_AUTO_Inspection.EndFC = null;
            this.FC_HDT_A_AUTO_Inspection.ErrID = 0;
            this.FC_HDT_A_AUTO_Inspection.InAlarm = false;
            this.FC_HDT_A_AUTO_Inspection.IsFlowHead = false;
            this.FC_HDT_A_AUTO_Inspection.Location = new System.Drawing.Point(783, 325);
            this.FC_HDT_A_AUTO_Inspection.LockUI = false;
            this.FC_HDT_A_AUTO_Inspection.Message = null;
            this.FC_HDT_A_AUTO_Inspection.MsgID = 0;
            this.FC_HDT_A_AUTO_Inspection.Name = "FC_HDT_A_AUTO_Inspection";
            this.FC_HDT_A_AUTO_Inspection.NEXT = this.flowChart39;
            this.FC_HDT_A_AUTO_Inspection.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.FC_HDT_A_AUTO_Inspection.OrgLocation = new System.Drawing.Point(0, 0);
            this.FC_HDT_A_AUTO_Inspection.OrgSize = new System.Drawing.Size(0, 0);
            this.FC_HDT_A_AUTO_Inspection.OverTimeSpec = 100;
            this.FC_HDT_A_AUTO_Inspection.Running = false;
            this.FC_HDT_A_AUTO_Inspection.Size = new System.Drawing.Size(200, 30);
            this.FC_HDT_A_AUTO_Inspection.SlowRunCycle = -1;
            this.FC_HDT_A_AUTO_Inspection.StartFC = null;
            this.FC_HDT_A_AUTO_Inspection.Text = "Start Inspection";
            this.FC_HDT_A_AUTO_Inspection.Run += new ProVLib.FlowChart.RunEventHandler(this.FC_HDT_A_AUTO_Inspection_Run);
            // 
            // flowChart17
            // 
            this.flowChart17.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart17.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart17.CASE1 = null;
            this.flowChart17.CASE2 = null;
            this.flowChart17.CASE3 = null;
            this.flowChart17.CASE4 = null;
            this.flowChart17.ContinueRun = false;
            this.flowChart17.EndFC = null;
            this.flowChart17.ErrID = 0;
            this.flowChart17.InAlarm = false;
            this.flowChart17.IsFlowHead = false;
            this.flowChart17.Location = new System.Drawing.Point(255, 439);
            this.flowChart17.LockUI = false;
            this.flowChart17.Message = null;
            this.flowChart17.MsgID = 0;
            this.flowChart17.Name = "flowChart17";
            this.flowChart17.NEXT = this.flowChart26;
            this.flowChart17.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart17.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart17.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart17.OverTimeSpec = 100;
            this.flowChart17.Running = false;
            this.flowChart17.Size = new System.Drawing.Size(50, 30);
            this.flowChart17.SlowRunCycle = -1;
            this.flowChart17.StartFC = null;
            this.flowChart17.Text = "Next";
            this.flowChart17.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart17_Run);
            // 
            // flowChart26
            // 
            this.flowChart26.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart26.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart26.CASE1 = null;
            this.flowChart26.CASE2 = null;
            this.flowChart26.CASE3 = null;
            this.flowChart26.CASE4 = null;
            this.flowChart26.ContinueRun = false;
            this.flowChart26.EndFC = null;
            this.flowChart26.ErrID = 0;
            this.flowChart26.InAlarm = false;
            this.flowChart26.IsFlowHead = false;
            this.flowChart26.Location = new System.Drawing.Point(49, 439);
            this.flowChart26.LockUI = false;
            this.flowChart26.Message = null;
            this.flowChart26.MsgID = 0;
            this.flowChart26.Name = "flowChart26";
            this.flowChart26.NEXT = this.flowChart27;
            this.flowChart26.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart26.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart26.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart26.OverTimeSpec = 100;
            this.flowChart26.Running = false;
            this.flowChart26.Size = new System.Drawing.Size(200, 30);
            this.flowChart26.SlowRunCycle = -1;
            this.flowChart26.StartFC = null;
            this.flowChart26.Text = "Need Move?";
            this.flowChart26.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart26_Run);
            // 
            // flowChart27
            // 
            this.flowChart27.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart27.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart27.CASE1 = null;
            this.flowChart27.CASE2 = null;
            this.flowChart27.CASE3 = null;
            this.flowChart27.CASE4 = null;
            this.flowChart27.ContinueRun = false;
            this.flowChart27.EndFC = null;
            this.flowChart27.ErrID = 0;
            this.flowChart27.InAlarm = false;
            this.flowChart27.IsFlowHead = false;
            this.flowChart27.Location = new System.Drawing.Point(49, 477);
            this.flowChart27.LockUI = false;
            this.flowChart27.Message = null;
            this.flowChart27.MsgID = 0;
            this.flowChart27.Name = "flowChart27";
            this.flowChart27.NEXT = this.flowChart28;
            this.flowChart27.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart27.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart27.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart27.OverTimeSpec = 100;
            this.flowChart27.Running = false;
            this.flowChart27.Size = new System.Drawing.Size(200, 30);
            this.flowChart27.SlowRunCycle = -1;
            this.flowChart27.StartFC = null;
            this.flowChart27.Text = "Move to Position (X & U)";
            this.flowChart27.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart27_Run);
            // 
            // flowChart28
            // 
            this.flowChart28.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart28.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart28.CASE1 = null;
            this.flowChart28.CASE2 = null;
            this.flowChart28.CASE3 = null;
            this.flowChart28.CASE4 = null;
            this.flowChart28.ContinueRun = false;
            this.flowChart28.EndFC = null;
            this.flowChart28.ErrID = 0;
            this.flowChart28.InAlarm = false;
            this.flowChart28.IsFlowHead = false;
            this.flowChart28.Location = new System.Drawing.Point(255, 477);
            this.flowChart28.LockUI = false;
            this.flowChart28.Message = null;
            this.flowChart28.MsgID = 0;
            this.flowChart28.Name = "flowChart28";
            this.flowChart28.NEXT = this.flowChart17;
            this.flowChart28.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart28.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart28.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart28.OverTimeSpec = 100;
            this.flowChart28.Running = false;
            this.flowChart28.Size = new System.Drawing.Size(50, 30);
            this.flowChart28.SlowRunCycle = -1;
            this.flowChart28.StartFC = null;
            this.flowChart28.Text = "Done";
            this.flowChart28.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart28_Run);
            // 
            // FC_HDT_A_AUTO_Move
            // 
            this.FC_HDT_A_AUTO_Move.BackColor = System.Drawing.Color.RoyalBlue;
            this.FC_HDT_A_AUTO_Move.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.FC_HDT_A_AUTO_Move.CASE1 = null;
            this.FC_HDT_A_AUTO_Move.CASE2 = null;
            this.FC_HDT_A_AUTO_Move.CASE3 = null;
            this.FC_HDT_A_AUTO_Move.CASE4 = null;
            this.FC_HDT_A_AUTO_Move.ContinueRun = false;
            this.FC_HDT_A_AUTO_Move.EndFC = null;
            this.FC_HDT_A_AUTO_Move.ErrID = 0;
            this.FC_HDT_A_AUTO_Move.InAlarm = false;
            this.FC_HDT_A_AUTO_Move.IsFlowHead = false;
            this.FC_HDT_A_AUTO_Move.Location = new System.Drawing.Point(49, 401);
            this.FC_HDT_A_AUTO_Move.LockUI = false;
            this.FC_HDT_A_AUTO_Move.Message = null;
            this.FC_HDT_A_AUTO_Move.MsgID = 0;
            this.FC_HDT_A_AUTO_Move.Name = "FC_HDT_A_AUTO_Move";
            this.FC_HDT_A_AUTO_Move.NEXT = this.flowChart26;
            this.FC_HDT_A_AUTO_Move.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.FC_HDT_A_AUTO_Move.OrgLocation = new System.Drawing.Point(0, 0);
            this.FC_HDT_A_AUTO_Move.OrgSize = new System.Drawing.Size(0, 0);
            this.FC_HDT_A_AUTO_Move.OverTimeSpec = 100;
            this.FC_HDT_A_AUTO_Move.Running = false;
            this.FC_HDT_A_AUTO_Move.Size = new System.Drawing.Size(200, 30);
            this.FC_HDT_A_AUTO_Move.SlowRunCycle = -1;
            this.FC_HDT_A_AUTO_Move.StartFC = null;
            this.FC_HDT_A_AUTO_Move.Text = "Start Move";
            this.FC_HDT_A_AUTO_Move.Run += new ProVLib.FlowChart.RunEventHandler(this.FC_HDT_A_AUTO_Move_Run);
            // 
            // FC_HDT_B
            // 
            this.FC_HDT_B.Controls.Add(this.flowChart89);
            this.FC_HDT_B.Controls.Add(this.flowChart87);
            this.FC_HDT_B.Controls.Add(this.flowChart85);
            this.FC_HDT_B.Controls.Add(this.flowChart46);
            this.FC_HDT_B.Controls.Add(this.flowChart52);
            this.FC_HDT_B.Controls.Add(this.flowChart51);
            this.FC_HDT_B.Controls.Add(this.flowChart50);
            this.FC_HDT_B.Controls.Add(this.flowChart49);
            this.FC_HDT_B.Controls.Add(this.flowChart48);
            this.FC_HDT_B.Controls.Add(this.flowChart47);
            this.FC_HDT_B.Controls.Add(this.flowChart53);
            this.FC_HDT_B.Controls.Add(this.flowChart70);
            this.FC_HDT_B.Controls.Add(this.flowChart69);
            this.FC_HDT_B.Controls.Add(this.flowChart68);
            this.FC_HDT_B.Controls.Add(this.flowChart67);
            this.FC_HDT_B.Controls.Add(this.flowChart66);
            this.FC_HDT_B.Controls.Add(this.flowChart65);
            this.FC_HDT_B.Controls.Add(this.flowChart64);
            this.FC_HDT_B.Controls.Add(this.flowChart63);
            this.FC_HDT_B.Controls.Add(this.flowChart62);
            this.FC_HDT_B.Controls.Add(this.flowChart61);
            this.FC_HDT_B.Controls.Add(this.flowChart60);
            this.FC_HDT_B.Controls.Add(this.flowChart59);
            this.FC_HDT_B.Controls.Add(this.flowChart58);
            this.FC_HDT_B.Controls.Add(this.flowChart57);
            this.FC_HDT_B.Controls.Add(this.flowChart56);
            this.FC_HDT_B.Controls.Add(this.flowChart55);
            this.FC_HDT_B.Controls.Add(this.FC_HDT_B_AUTO_Inspection);
            this.FC_HDT_B.Controls.Add(this.FC_HDT_B_AUTO_Place);
            this.FC_HDT_B.Controls.Add(this.flowChart79);
            this.FC_HDT_B.Controls.Add(this.flowChart78);
            this.FC_HDT_B.Controls.Add(this.flowChart77);
            this.FC_HDT_B.Controls.Add(this.flowChart76);
            this.FC_HDT_B.Controls.Add(this.flowChart75);
            this.FC_HDT_B.Controls.Add(this.flowChart74);
            this.FC_HDT_B.Controls.Add(this.flowChart73);
            this.FC_HDT_B.Controls.Add(this.flowChart72);
            this.FC_HDT_B.Controls.Add(this.FC_HDT_B_AUTO_Pick);
            this.FC_HDT_B.Controls.Add(this.flowChart81);
            this.FC_HDT_B.Controls.Add(this.flowChart80);
            this.FC_HDT_B.Controls.Add(this.flowChart71);
            this.FC_HDT_B.Controls.Add(this.flowChart54);
            this.FC_HDT_B.Controls.Add(this.FC_HDT_B_AUTO_Move);
            this.FC_HDT_B.Location = new System.Drawing.Point(4, 35);
            this.FC_HDT_B.Name = "FC_HDT_B";
            this.FC_HDT_B.Padding = new System.Windows.Forms.Padding(3);
            this.FC_HDT_B.Size = new System.Drawing.Size(1053, 585);
            this.FC_HDT_B.TabIndex = 1;
            this.FC_HDT_B.Text = "HDT_B";
            this.FC_HDT_B.UseVisualStyleBackColor = true;
            // 
            // flowChart89
            // 
            this.flowChart89.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart89.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart89.CASE1 = null;
            this.flowChart89.CASE2 = null;
            this.flowChart89.CASE3 = null;
            this.flowChart89.CASE4 = null;
            this.flowChart89.ContinueRun = false;
            this.flowChart89.EndFC = null;
            this.flowChart89.ErrID = 0;
            this.flowChart89.InAlarm = false;
            this.flowChart89.IsFlowHead = false;
            this.flowChart89.Location = new System.Drawing.Point(49, 211);
            this.flowChart89.LockUI = false;
            this.flowChart89.Message = null;
            this.flowChart89.MsgID = 0;
            this.flowChart89.Name = "flowChart89";
            this.flowChart89.NEXT = this.flowChart75;
            this.flowChart89.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart89.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart89.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart89.OverTimeSpec = 100;
            this.flowChart89.Running = false;
            this.flowChart89.Size = new System.Drawing.Size(200, 30);
            this.flowChart89.SlowRunCycle = -1;
            this.flowChart89.StartFC = null;
            this.flowChart89.Text = "Axis Z to PickPos + Offset";
            this.flowChart89.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart89_Run);
            // 
            // flowChart75
            // 
            this.flowChart75.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart75.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart75.CASE1 = null;
            this.flowChart75.CASE2 = null;
            this.flowChart75.CASE3 = null;
            this.flowChart75.CASE4 = null;
            this.flowChart75.ContinueRun = false;
            this.flowChart75.EndFC = null;
            this.flowChart75.ErrID = 0;
            this.flowChart75.InAlarm = false;
            this.flowChart75.IsFlowHead = false;
            this.flowChart75.Location = new System.Drawing.Point(49, 249);
            this.flowChart75.LockUI = false;
            this.flowChart75.Message = null;
            this.flowChart75.MsgID = 0;
            this.flowChart75.Name = "flowChart75";
            this.flowChart75.NEXT = this.flowChart76;
            this.flowChart75.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart75.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart75.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart75.OverTimeSpec = 100;
            this.flowChart75.Running = false;
            this.flowChart75.Size = new System.Drawing.Size(200, 30);
            this.flowChart75.SlowRunCycle = -1;
            this.flowChart75.StartFC = null;
            this.flowChart75.Text = "Axis Z to Safety";
            this.flowChart75.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart75_Run);
            // 
            // flowChart76
            // 
            this.flowChart76.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart76.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart76.CASE1 = this.flowChart77;
            this.flowChart76.CASE2 = null;
            this.flowChart76.CASE3 = null;
            this.flowChart76.CASE4 = null;
            this.flowChart76.ContinueRun = false;
            this.flowChart76.EndFC = null;
            this.flowChart76.ErrID = 0;
            this.flowChart76.InAlarm = false;
            this.flowChart76.IsFlowHead = false;
            this.flowChart76.Location = new System.Drawing.Point(49, 287);
            this.flowChart76.LockUI = false;
            this.flowChart76.Message = null;
            this.flowChart76.MsgID = 0;
            this.flowChart76.Name = "flowChart76";
            this.flowChart76.NEXT = this.flowChart78;
            this.flowChart76.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart76.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart76.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart76.OverTimeSpec = 100;
            this.flowChart76.Running = false;
            this.flowChart76.Size = new System.Drawing.Size(200, 30);
            this.flowChart76.SlowRunCycle = -1;
            this.flowChart76.StartFC = null;
            this.flowChart76.Text = "Check Using Vacuum Value";
            this.flowChart76.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart76_Run);
            // 
            // flowChart77
            // 
            this.flowChart77.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart77.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart77.CASE1 = this.flowChart78;
            this.flowChart77.CASE2 = null;
            this.flowChart77.CASE3 = null;
            this.flowChart77.CASE4 = null;
            this.flowChart77.ContinueRun = false;
            this.flowChart77.EndFC = null;
            this.flowChart77.ErrID = 0;
            this.flowChart77.InAlarm = false;
            this.flowChart77.IsFlowHead = false;
            this.flowChart77.Location = new System.Drawing.Point(264, 287);
            this.flowChart77.LockUI = false;
            this.flowChart77.Message = null;
            this.flowChart77.MsgID = 0;
            this.flowChart77.Name = "flowChart77";
            this.flowChart77.NEXT = this.flowChart72;
            this.flowChart77.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart77.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart77.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart77.OverTimeSpec = 100;
            this.flowChart77.Running = false;
            this.flowChart77.Size = new System.Drawing.Size(114, 30);
            this.flowChart77.SlowRunCycle = -1;
            this.flowChart77.StartFC = null;
            this.flowChart77.Text = "Retry or Ignore";
            this.flowChart77.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart77_Run);
            // 
            // flowChart78
            // 
            this.flowChart78.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart78.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart78.CASE1 = null;
            this.flowChart78.CASE2 = null;
            this.flowChart78.CASE3 = null;
            this.flowChart78.CASE4 = null;
            this.flowChart78.ContinueRun = false;
            this.flowChart78.EndFC = null;
            this.flowChart78.ErrID = 0;
            this.flowChart78.InAlarm = false;
            this.flowChart78.IsFlowHead = false;
            this.flowChart78.Location = new System.Drawing.Point(49, 325);
            this.flowChart78.LockUI = false;
            this.flowChart78.Message = null;
            this.flowChart78.MsgID = 0;
            this.flowChart78.Name = "flowChart78";
            this.flowChart78.NEXT = this.flowChart79;
            this.flowChart78.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart78.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart78.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart78.OverTimeSpec = 100;
            this.flowChart78.Running = false;
            this.flowChart78.Size = new System.Drawing.Size(200, 30);
            this.flowChart78.SlowRunCycle = -1;
            this.flowChart78.StartFC = null;
            this.flowChart78.Text = "Vac. Failure Vac. Off";
            this.flowChart78.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart78_Run);
            // 
            // flowChart79
            // 
            this.flowChart79.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart79.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart79.CASE1 = null;
            this.flowChart79.CASE2 = null;
            this.flowChart79.CASE3 = null;
            this.flowChart79.CASE4 = null;
            this.flowChart79.ContinueRun = false;
            this.flowChart79.EndFC = null;
            this.flowChart79.ErrID = 0;
            this.flowChart79.InAlarm = false;
            this.flowChart79.IsFlowHead = false;
            this.flowChart79.Location = new System.Drawing.Point(49, 363);
            this.flowChart79.LockUI = false;
            this.flowChart79.Message = null;
            this.flowChart79.MsgID = 0;
            this.flowChart79.Name = "flowChart79";
            this.flowChart79.NEXT = this.flowChart70;
            this.flowChart79.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart79.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart79.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart79.OverTimeSpec = 100;
            this.flowChart79.Running = false;
            this.flowChart79.Size = new System.Drawing.Size(200, 30);
            this.flowChart79.SlowRunCycle = -1;
            this.flowChart79.StartFC = null;
            this.flowChart79.Text = "Pick Done";
            this.flowChart79.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart79_Run);
            // 
            // flowChart70
            // 
            this.flowChart70.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart70.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart70.CASE1 = null;
            this.flowChart70.CASE2 = null;
            this.flowChart70.CASE3 = null;
            this.flowChart70.CASE4 = null;
            this.flowChart70.ContinueRun = false;
            this.flowChart70.EndFC = null;
            this.flowChart70.ErrID = 0;
            this.flowChart70.InAlarm = false;
            this.flowChart70.IsFlowHead = false;
            this.flowChart70.Location = new System.Drawing.Point(6, 173);
            this.flowChart70.LockUI = false;
            this.flowChart70.Message = null;
            this.flowChart70.MsgID = 0;
            this.flowChart70.Name = "flowChart70";
            this.flowChart70.NEXT = this.FC_HDT_B_AUTO_Pick;
            this.flowChart70.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart70.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart70.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart70.OverTimeSpec = 100;
            this.flowChart70.Running = false;
            this.flowChart70.Size = new System.Drawing.Size(37, 30);
            this.flowChart70.SlowRunCycle = -1;
            this.flowChart70.StartFC = null;
            this.flowChart70.Text = "Next";
            this.flowChart70.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart70_Run);
            // 
            // FC_HDT_B_AUTO_Pick
            // 
            this.FC_HDT_B_AUTO_Pick.BackColor = System.Drawing.Color.RoyalBlue;
            this.FC_HDT_B_AUTO_Pick.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.FC_HDT_B_AUTO_Pick.CASE1 = null;
            this.FC_HDT_B_AUTO_Pick.CASE2 = null;
            this.FC_HDT_B_AUTO_Pick.CASE3 = null;
            this.FC_HDT_B_AUTO_Pick.CASE4 = null;
            this.FC_HDT_B_AUTO_Pick.ContinueRun = false;
            this.FC_HDT_B_AUTO_Pick.EndFC = null;
            this.FC_HDT_B_AUTO_Pick.ErrID = 0;
            this.FC_HDT_B_AUTO_Pick.InAlarm = false;
            this.FC_HDT_B_AUTO_Pick.IsFlowHead = false;
            this.FC_HDT_B_AUTO_Pick.Location = new System.Drawing.Point(49, 21);
            this.FC_HDT_B_AUTO_Pick.LockUI = false;
            this.FC_HDT_B_AUTO_Pick.Message = null;
            this.FC_HDT_B_AUTO_Pick.MsgID = 0;
            this.FC_HDT_B_AUTO_Pick.Name = "FC_HDT_B_AUTO_Pick";
            this.FC_HDT_B_AUTO_Pick.NEXT = this.flowChart85;
            this.FC_HDT_B_AUTO_Pick.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.FC_HDT_B_AUTO_Pick.OrgLocation = new System.Drawing.Point(0, 0);
            this.FC_HDT_B_AUTO_Pick.OrgSize = new System.Drawing.Size(0, 0);
            this.FC_HDT_B_AUTO_Pick.OverTimeSpec = 100;
            this.FC_HDT_B_AUTO_Pick.Running = false;
            this.FC_HDT_B_AUTO_Pick.Size = new System.Drawing.Size(200, 30);
            this.FC_HDT_B_AUTO_Pick.SlowRunCycle = -1;
            this.FC_HDT_B_AUTO_Pick.StartFC = null;
            this.FC_HDT_B_AUTO_Pick.Text = "Start Pick";
            this.FC_HDT_B_AUTO_Pick.Run += new ProVLib.FlowChart.RunEventHandler(this.FC_HDT_B_AUTO_Pick_Run);
            // 
            // flowChart85
            // 
            this.flowChart85.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart85.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart85.CASE1 = null;
            this.flowChart85.CASE2 = null;
            this.flowChart85.CASE3 = null;
            this.flowChart85.CASE4 = null;
            this.flowChart85.ContinueRun = false;
            this.flowChart85.EndFC = null;
            this.flowChart85.ErrID = 0;
            this.flowChart85.InAlarm = false;
            this.flowChart85.IsFlowHead = false;
            this.flowChart85.Location = new System.Drawing.Point(49, 59);
            this.flowChart85.LockUI = false;
            this.flowChart85.Message = null;
            this.flowChart85.MsgID = 0;
            this.flowChart85.Name = "flowChart85";
            this.flowChart85.NEXT = this.flowChart72;
            this.flowChart85.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart85.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart85.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart85.OverTimeSpec = 100;
            this.flowChart85.Running = false;
            this.flowChart85.Size = new System.Drawing.Size(200, 30);
            this.flowChart85.SlowRunCycle = -1;
            this.flowChart85.StartFC = null;
            this.flowChart85.Text = "Move U to Zero";
            this.flowChart85.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart85_Run);
            // 
            // flowChart72
            // 
            this.flowChart72.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart72.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart72.CASE1 = null;
            this.flowChart72.CASE2 = null;
            this.flowChart72.CASE3 = null;
            this.flowChart72.CASE4 = null;
            this.flowChart72.ContinueRun = false;
            this.flowChart72.EndFC = null;
            this.flowChart72.ErrID = 0;
            this.flowChart72.InAlarm = false;
            this.flowChart72.IsFlowHead = false;
            this.flowChart72.Location = new System.Drawing.Point(49, 97);
            this.flowChart72.LockUI = false;
            this.flowChart72.Message = null;
            this.flowChart72.MsgID = 0;
            this.flowChart72.Name = "flowChart72";
            this.flowChart72.NEXT = this.flowChart73;
            this.flowChart72.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart72.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart72.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart72.OverTimeSpec = 100;
            this.flowChart72.Running = false;
            this.flowChart72.Size = new System.Drawing.Size(200, 30);
            this.flowChart72.SlowRunCycle = -1;
            this.flowChart72.StartFC = null;
            this.flowChart72.Text = "Axis Z to PickPos + Offset";
            this.flowChart72.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart72_Run);
            // 
            // flowChart73
            // 
            this.flowChart73.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart73.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart73.CASE1 = null;
            this.flowChart73.CASE2 = null;
            this.flowChart73.CASE3 = null;
            this.flowChart73.CASE4 = null;
            this.flowChart73.ContinueRun = false;
            this.flowChart73.EndFC = null;
            this.flowChart73.ErrID = 0;
            this.flowChart73.InAlarm = false;
            this.flowChart73.IsFlowHead = false;
            this.flowChart73.Location = new System.Drawing.Point(49, 135);
            this.flowChart73.LockUI = false;
            this.flowChart73.Message = null;
            this.flowChart73.MsgID = 0;
            this.flowChart73.Name = "flowChart73";
            this.flowChart73.NEXT = this.flowChart74;
            this.flowChart73.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart73.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart73.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart73.OverTimeSpec = 100;
            this.flowChart73.Running = false;
            this.flowChart73.Size = new System.Drawing.Size(200, 30);
            this.flowChart73.SlowRunCycle = -1;
            this.flowChart73.StartFC = null;
            this.flowChart73.Text = "Axis Z to PickPos ";
            this.flowChart73.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart73_Run);
            // 
            // flowChart74
            // 
            this.flowChart74.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart74.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart74.CASE1 = null;
            this.flowChart74.CASE2 = null;
            this.flowChart74.CASE3 = null;
            this.flowChart74.CASE4 = null;
            this.flowChart74.ContinueRun = false;
            this.flowChart74.EndFC = null;
            this.flowChart74.ErrID = 0;
            this.flowChart74.InAlarm = false;
            this.flowChart74.IsFlowHead = false;
            this.flowChart74.Location = new System.Drawing.Point(49, 173);
            this.flowChart74.LockUI = false;
            this.flowChart74.Message = null;
            this.flowChart74.MsgID = 0;
            this.flowChart74.Name = "flowChart74";
            this.flowChart74.NEXT = this.flowChart89;
            this.flowChart74.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart74.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart74.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart74.OverTimeSpec = 100;
            this.flowChart74.Running = false;
            this.flowChart74.Size = new System.Drawing.Size(200, 30);
            this.flowChart74.SlowRunCycle = -1;
            this.flowChart74.StartFC = null;
            this.flowChart74.Text = "Using Vacuum On";
            this.flowChart74.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart74_Run);
            // 
            // flowChart87
            // 
            this.flowChart87.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart87.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart87.CASE1 = this.flowChart48;
            this.flowChart87.CASE2 = null;
            this.flowChart87.CASE3 = null;
            this.flowChart87.CASE4 = null;
            this.flowChart87.ContinueRun = false;
            this.flowChart87.EndFC = null;
            this.flowChart87.ErrID = 0;
            this.flowChart87.InAlarm = false;
            this.flowChart87.IsFlowHead = false;
            this.flowChart87.Location = new System.Drawing.Point(652, 439);
            this.flowChart87.LockUI = false;
            this.flowChart87.Message = null;
            this.flowChart87.MsgID = 0;
            this.flowChart87.Name = "flowChart87";
            this.flowChart87.NEXT = this.flowChart51;
            this.flowChart87.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart87.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart87.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart87.OverTimeSpec = 100;
            this.flowChart87.Running = false;
            this.flowChart87.Size = new System.Drawing.Size(114, 30);
            this.flowChart87.SlowRunCycle = -1;
            this.flowChart87.StartFC = null;
            this.flowChart87.Text = "Inspect OK?";
            this.flowChart87.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart87_Run);
            // 
            // flowChart48
            // 
            this.flowChart48.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart48.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart48.CASE1 = null;
            this.flowChart48.CASE2 = null;
            this.flowChart48.CASE3 = null;
            this.flowChart48.CASE4 = null;
            this.flowChart48.ContinueRun = false;
            this.flowChart48.EndFC = null;
            this.flowChart48.ErrID = 0;
            this.flowChart48.InAlarm = false;
            this.flowChart48.IsFlowHead = false;
            this.flowChart48.Location = new System.Drawing.Point(783, 401);
            this.flowChart48.LockUI = false;
            this.flowChart48.Message = null;
            this.flowChart48.MsgID = 0;
            this.flowChart48.Name = "flowChart48";
            this.flowChart48.NEXT = this.flowChart49;
            this.flowChart48.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart48.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart48.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart48.OverTimeSpec = 100;
            this.flowChart48.Running = false;
            this.flowChart48.Size = new System.Drawing.Size(200, 30);
            this.flowChart48.SlowRunCycle = -1;
            this.flowChart48.StartFC = null;
            this.flowChart48.Text = "Reset Status";
            this.flowChart48.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart48_Run);
            // 
            // flowChart49
            // 
            this.flowChart49.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart49.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart49.CASE1 = null;
            this.flowChart49.CASE2 = null;
            this.flowChart49.CASE3 = null;
            this.flowChart49.CASE4 = null;
            this.flowChart49.ContinueRun = false;
            this.flowChart49.EndFC = null;
            this.flowChart49.ErrID = 0;
            this.flowChart49.InAlarm = false;
            this.flowChart49.IsFlowHead = false;
            this.flowChart49.Location = new System.Drawing.Point(783, 439);
            this.flowChart49.LockUI = false;
            this.flowChart49.Message = null;
            this.flowChart49.MsgID = 0;
            this.flowChart49.Name = "flowChart49";
            this.flowChart49.NEXT = this.flowChart50;
            this.flowChart49.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart49.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart49.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart49.OverTimeSpec = 100;
            this.flowChart49.Running = false;
            this.flowChart49.Size = new System.Drawing.Size(200, 30);
            this.flowChart49.SlowRunCycle = -1;
            this.flowChart49.StartFC = null;
            this.flowChart49.Text = "Set command";
            this.flowChart49.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart49_Run);
            // 
            // flowChart50
            // 
            this.flowChart50.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart50.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart50.CASE1 = this.flowChart48;
            this.flowChart50.CASE2 = null;
            this.flowChart50.CASE3 = null;
            this.flowChart50.CASE4 = null;
            this.flowChart50.ContinueRun = false;
            this.flowChart50.EndFC = null;
            this.flowChart50.ErrID = 0;
            this.flowChart50.InAlarm = false;
            this.flowChart50.IsFlowHead = false;
            this.flowChart50.Location = new System.Drawing.Point(652, 401);
            this.flowChart50.LockUI = false;
            this.flowChart50.Message = null;
            this.flowChart50.MsgID = 0;
            this.flowChart50.Name = "flowChart50";
            this.flowChart50.NEXT = this.flowChart87;
            this.flowChart50.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart50.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart50.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart50.OverTimeSpec = 100;
            this.flowChart50.Running = false;
            this.flowChart50.Size = new System.Drawing.Size(114, 30);
            this.flowChart50.SlowRunCycle = -1;
            this.flowChart50.StartFC = null;
            this.flowChart50.Text = "Wait Status";
            this.flowChart50.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart50_Run);
            // 
            // flowChart51
            // 
            this.flowChart51.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart51.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart51.CASE1 = this.flowChart48;
            this.flowChart51.CASE2 = null;
            this.flowChart51.CASE3 = null;
            this.flowChart51.CASE4 = null;
            this.flowChart51.ContinueRun = false;
            this.flowChart51.EndFC = null;
            this.flowChart51.ErrID = 0;
            this.flowChart51.InAlarm = false;
            this.flowChart51.IsFlowHead = false;
            this.flowChart51.Location = new System.Drawing.Point(652, 477);
            this.flowChart51.LockUI = false;
            this.flowChart51.Message = null;
            this.flowChart51.MsgID = 0;
            this.flowChart51.Name = "flowChart51";
            this.flowChart51.NEXT = this.flowChart52;
            this.flowChart51.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart51.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart51.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart51.OverTimeSpec = 100;
            this.flowChart51.Running = false;
            this.flowChart51.Size = new System.Drawing.Size(114, 30);
            this.flowChart51.SlowRunCycle = -1;
            this.flowChart51.StartFC = null;
            this.flowChart51.Text = "Get Result";
            this.flowChart51.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart51_Run);
            // 
            // flowChart52
            // 
            this.flowChart52.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart52.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart52.CASE1 = null;
            this.flowChart52.CASE2 = null;
            this.flowChart52.CASE3 = null;
            this.flowChart52.CASE4 = null;
            this.flowChart52.ContinueRun = false;
            this.flowChart52.EndFC = null;
            this.flowChart52.ErrID = 0;
            this.flowChart52.InAlarm = false;
            this.flowChart52.IsFlowHead = false;
            this.flowChart52.Location = new System.Drawing.Point(783, 477);
            this.flowChart52.LockUI = false;
            this.flowChart52.Message = null;
            this.flowChart52.MsgID = 0;
            this.flowChart52.Name = "flowChart52";
            this.flowChart52.NEXT = this.flowChart46;
            this.flowChart52.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart52.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart52.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart52.OverTimeSpec = 100;
            this.flowChart52.Running = false;
            this.flowChart52.Size = new System.Drawing.Size(200, 30);
            this.flowChart52.SlowRunCycle = -1;
            this.flowChart52.StartFC = null;
            this.flowChart52.Text = "Inspection Finished";
            this.flowChart52.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart52_Run);
            // 
            // flowChart46
            // 
            this.flowChart46.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart46.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart46.CASE1 = null;
            this.flowChart46.CASE2 = null;
            this.flowChart46.CASE3 = null;
            this.flowChart46.CASE4 = null;
            this.flowChart46.ContinueRun = false;
            this.flowChart46.EndFC = null;
            this.flowChart46.ErrID = 0;
            this.flowChart46.InAlarm = false;
            this.flowChart46.IsFlowHead = false;
            this.flowChart46.Location = new System.Drawing.Point(989, 363);
            this.flowChart46.LockUI = false;
            this.flowChart46.Message = null;
            this.flowChart46.MsgID = 0;
            this.flowChart46.Name = "flowChart46";
            this.flowChart46.NEXT = this.flowChart47;
            this.flowChart46.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart46.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart46.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart46.OverTimeSpec = 100;
            this.flowChart46.Running = false;
            this.flowChart46.Size = new System.Drawing.Size(37, 30);
            this.flowChart46.SlowRunCycle = -1;
            this.flowChart46.StartFC = null;
            this.flowChart46.Text = "Next";
            this.flowChart46.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart46_Run);
            // 
            // flowChart47
            // 
            this.flowChart47.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart47.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart47.CASE1 = null;
            this.flowChart47.CASE2 = null;
            this.flowChart47.CASE3 = null;
            this.flowChart47.CASE4 = null;
            this.flowChart47.ContinueRun = false;
            this.flowChart47.EndFC = null;
            this.flowChart47.ErrID = 0;
            this.flowChart47.InAlarm = false;
            this.flowChart47.IsFlowHead = false;
            this.flowChart47.Location = new System.Drawing.Point(783, 363);
            this.flowChart47.LockUI = false;
            this.flowChart47.Message = null;
            this.flowChart47.MsgID = 0;
            this.flowChart47.Name = "flowChart47";
            this.flowChart47.NEXT = this.flowChart48;
            this.flowChart47.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart47.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart47.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart47.OverTimeSpec = 100;
            this.flowChart47.Running = false;
            this.flowChart47.Size = new System.Drawing.Size(200, 30);
            this.flowChart47.SlowRunCycle = -1;
            this.flowChart47.StartFC = null;
            this.flowChart47.Text = "Wait for command";
            this.flowChart47.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart47_Run);
            // 
            // flowChart53
            // 
            this.flowChart53.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart53.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart53.CASE1 = null;
            this.flowChart53.CASE2 = null;
            this.flowChart53.CASE3 = null;
            this.flowChart53.CASE4 = null;
            this.flowChart53.ContinueRun = false;
            this.flowChart53.EndFC = null;
            this.flowChart53.ErrID = 0;
            this.flowChart53.InAlarm = false;
            this.flowChart53.IsFlowHead = false;
            this.flowChart53.Location = new System.Drawing.Point(384, 249);
            this.flowChart53.LockUI = false;
            this.flowChart53.Message = null;
            this.flowChart53.MsgID = 0;
            this.flowChart53.Name = "flowChart53";
            this.flowChart53.NEXT = this.FC_HDT_B_AUTO_Place;
            this.flowChart53.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart53.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart53.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart53.OverTimeSpec = 100;
            this.flowChart53.Running = false;
            this.flowChart53.Size = new System.Drawing.Size(37, 30);
            this.flowChart53.SlowRunCycle = -1;
            this.flowChart53.StartFC = null;
            this.flowChart53.Text = "Next";
            this.flowChart53.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart53_Run);
            // 
            // FC_HDT_B_AUTO_Place
            // 
            this.FC_HDT_B_AUTO_Place.BackColor = System.Drawing.Color.RoyalBlue;
            this.FC_HDT_B_AUTO_Place.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.FC_HDT_B_AUTO_Place.CASE1 = null;
            this.FC_HDT_B_AUTO_Place.CASE2 = null;
            this.FC_HDT_B_AUTO_Place.CASE3 = null;
            this.FC_HDT_B_AUTO_Place.CASE4 = null;
            this.FC_HDT_B_AUTO_Place.ContinueRun = false;
            this.FC_HDT_B_AUTO_Place.EndFC = null;
            this.FC_HDT_B_AUTO_Place.ErrID = 0;
            this.FC_HDT_B_AUTO_Place.InAlarm = false;
            this.FC_HDT_B_AUTO_Place.IsFlowHead = false;
            this.FC_HDT_B_AUTO_Place.Location = new System.Drawing.Point(427, 21);
            this.FC_HDT_B_AUTO_Place.LockUI = false;
            this.FC_HDT_B_AUTO_Place.Message = null;
            this.FC_HDT_B_AUTO_Place.MsgID = 0;
            this.FC_HDT_B_AUTO_Place.Name = "FC_HDT_B_AUTO_Place";
            this.FC_HDT_B_AUTO_Place.NEXT = this.flowChart55;
            this.FC_HDT_B_AUTO_Place.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.FC_HDT_B_AUTO_Place.OrgLocation = new System.Drawing.Point(0, 0);
            this.FC_HDT_B_AUTO_Place.OrgSize = new System.Drawing.Size(0, 0);
            this.FC_HDT_B_AUTO_Place.OverTimeSpec = 100;
            this.FC_HDT_B_AUTO_Place.Running = false;
            this.FC_HDT_B_AUTO_Place.Size = new System.Drawing.Size(200, 30);
            this.FC_HDT_B_AUTO_Place.SlowRunCycle = -1;
            this.FC_HDT_B_AUTO_Place.StartFC = null;
            this.FC_HDT_B_AUTO_Place.Text = "Start Place";
            this.FC_HDT_B_AUTO_Place.Run += new ProVLib.FlowChart.RunEventHandler(this.FC_HDT_B_AUTO_Place_Run);
            // 
            // flowChart55
            // 
            this.flowChart55.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart55.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart55.CASE1 = this.flowChart56;
            this.flowChart55.CASE2 = null;
            this.flowChart55.CASE3 = null;
            this.flowChart55.CASE4 = null;
            this.flowChart55.ContinueRun = false;
            this.flowChart55.EndFC = null;
            this.flowChart55.ErrID = 0;
            this.flowChart55.InAlarm = false;
            this.flowChart55.IsFlowHead = false;
            this.flowChart55.Location = new System.Drawing.Point(427, 59);
            this.flowChart55.LockUI = false;
            this.flowChart55.Message = null;
            this.flowChart55.MsgID = 0;
            this.flowChart55.Name = "flowChart55";
            this.flowChart55.NEXT = this.flowChart59;
            this.flowChart55.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart55.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart55.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart55.OverTimeSpec = 100;
            this.flowChart55.Running = false;
            this.flowChart55.Size = new System.Drawing.Size(200, 30);
            this.flowChart55.SlowRunCycle = -1;
            this.flowChart55.StartFC = null;
            this.flowChart55.Text = "Check Using Vac.";
            this.flowChart55.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart55_Run);
            // 
            // flowChart56
            // 
            this.flowChart56.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart56.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart56.CASE1 = this.flowChart57;
            this.flowChart56.CASE2 = null;
            this.flowChart56.CASE3 = null;
            this.flowChart56.CASE4 = null;
            this.flowChart56.ContinueRun = false;
            this.flowChart56.EndFC = null;
            this.flowChart56.ErrID = 0;
            this.flowChart56.InAlarm = false;
            this.flowChart56.IsFlowHead = false;
            this.flowChart56.Location = new System.Drawing.Point(642, 59);
            this.flowChart56.LockUI = false;
            this.flowChart56.Message = null;
            this.flowChart56.MsgID = 0;
            this.flowChart56.Name = "flowChart56";
            this.flowChart56.NEXT = this.flowChart58;
            this.flowChart56.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart56.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart56.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart56.OverTimeSpec = 100;
            this.flowChart56.Running = false;
            this.flowChart56.Size = new System.Drawing.Size(114, 30);
            this.flowChart56.SlowRunCycle = -1;
            this.flowChart56.StartFC = null;
            this.flowChart56.Text = "Retry or Ignore";
            this.flowChart56.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart56_Run);
            // 
            // flowChart57
            // 
            this.flowChart57.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart57.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart57.CASE1 = null;
            this.flowChart57.CASE2 = null;
            this.flowChart57.CASE3 = null;
            this.flowChart57.CASE4 = null;
            this.flowChart57.ContinueRun = false;
            this.flowChart57.EndFC = null;
            this.flowChart57.ErrID = 0;
            this.flowChart57.InAlarm = false;
            this.flowChart57.IsFlowHead = false;
            this.flowChart57.Location = new System.Drawing.Point(642, 21);
            this.flowChart57.LockUI = false;
            this.flowChart57.Message = null;
            this.flowChart57.MsgID = 0;
            this.flowChart57.Name = "flowChart57";
            this.flowChart57.NEXT = this.flowChart55;
            this.flowChart57.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart57.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart57.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart57.OverTimeSpec = 100;
            this.flowChart57.Running = false;
            this.flowChart57.Size = new System.Drawing.Size(114, 30);
            this.flowChart57.SlowRunCycle = -1;
            this.flowChart57.StartFC = null;
            this.flowChart57.Text = "Set Nozzle state";
            this.flowChart57.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart57_Run);
            // 
            // flowChart58
            // 
            this.flowChart58.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart58.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart58.CASE1 = null;
            this.flowChart58.CASE2 = null;
            this.flowChart58.CASE3 = null;
            this.flowChart58.CASE4 = null;
            this.flowChart58.ContinueRun = false;
            this.flowChart58.EndFC = null;
            this.flowChart58.ErrID = 0;
            this.flowChart58.InAlarm = false;
            this.flowChart58.IsFlowHead = false;
            this.flowChart58.Location = new System.Drawing.Point(642, 97);
            this.flowChart58.LockUI = false;
            this.flowChart58.Message = null;
            this.flowChart58.MsgID = 0;
            this.flowChart58.Name = "flowChart58";
            this.flowChart58.NEXT = this.flowChart55;
            this.flowChart58.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart58.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart58.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart58.OverTimeSpec = 100;
            this.flowChart58.Running = false;
            this.flowChart58.Size = new System.Drawing.Size(114, 30);
            this.flowChart58.SlowRunCycle = -1;
            this.flowChart58.StartFC = null;
            this.flowChart58.Text = "Retry";
            this.flowChart58.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart58_Run);
            // 
            // flowChart59
            // 
            this.flowChart59.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart59.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart59.CASE1 = null;
            this.flowChart59.CASE2 = null;
            this.flowChart59.CASE3 = null;
            this.flowChart59.CASE4 = null;
            this.flowChart59.ContinueRun = false;
            this.flowChart59.EndFC = null;
            this.flowChart59.ErrID = 0;
            this.flowChart59.InAlarm = false;
            this.flowChart59.IsFlowHead = false;
            this.flowChart59.Location = new System.Drawing.Point(427, 97);
            this.flowChart59.LockUI = false;
            this.flowChart59.Message = null;
            this.flowChart59.MsgID = 0;
            this.flowChart59.Name = "flowChart59";
            this.flowChart59.NEXT = this.flowChart60;
            this.flowChart59.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart59.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart59.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart59.OverTimeSpec = 100;
            this.flowChart59.Running = false;
            this.flowChart59.Size = new System.Drawing.Size(200, 30);
            this.flowChart59.SlowRunCycle = -1;
            this.flowChart59.StartFC = null;
            this.flowChart59.Text = "Axis Z to PlacePos + Offset";
            this.flowChart59.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart59_Run);
            // 
            // flowChart60
            // 
            this.flowChart60.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart60.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart60.CASE1 = null;
            this.flowChart60.CASE2 = null;
            this.flowChart60.CASE3 = null;
            this.flowChart60.CASE4 = null;
            this.flowChart60.ContinueRun = false;
            this.flowChart60.EndFC = null;
            this.flowChart60.ErrID = 0;
            this.flowChart60.InAlarm = false;
            this.flowChart60.IsFlowHead = false;
            this.flowChart60.Location = new System.Drawing.Point(427, 135);
            this.flowChart60.LockUI = false;
            this.flowChart60.Message = null;
            this.flowChart60.MsgID = 0;
            this.flowChart60.Name = "flowChart60";
            this.flowChart60.NEXT = this.flowChart61;
            this.flowChart60.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart60.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart60.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart60.OverTimeSpec = 100;
            this.flowChart60.Running = false;
            this.flowChart60.Size = new System.Drawing.Size(200, 30);
            this.flowChart60.SlowRunCycle = -1;
            this.flowChart60.StartFC = null;
            this.flowChart60.Text = "Axis Z to PlacePos ";
            this.flowChart60.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart60_Run);
            // 
            // flowChart61
            // 
            this.flowChart61.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart61.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart61.CASE1 = null;
            this.flowChart61.CASE2 = null;
            this.flowChart61.CASE3 = null;
            this.flowChart61.CASE4 = null;
            this.flowChart61.ContinueRun = false;
            this.flowChart61.EndFC = null;
            this.flowChart61.ErrID = 0;
            this.flowChart61.InAlarm = false;
            this.flowChart61.IsFlowHead = false;
            this.flowChart61.Location = new System.Drawing.Point(427, 173);
            this.flowChart61.LockUI = false;
            this.flowChart61.Message = null;
            this.flowChart61.MsgID = 0;
            this.flowChart61.Name = "flowChart61";
            this.flowChart61.NEXT = this.flowChart62;
            this.flowChart61.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart61.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart61.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart61.OverTimeSpec = 100;
            this.flowChart61.Running = false;
            this.flowChart61.Size = new System.Drawing.Size(200, 30);
            this.flowChart61.SlowRunCycle = -1;
            this.flowChart61.StartFC = null;
            this.flowChart61.Text = "Before destory delay";
            this.flowChart61.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart61_Run);
            // 
            // flowChart62
            // 
            this.flowChart62.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart62.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart62.CASE1 = null;
            this.flowChart62.CASE2 = null;
            this.flowChart62.CASE3 = null;
            this.flowChart62.CASE4 = null;
            this.flowChart62.ContinueRun = false;
            this.flowChart62.EndFC = null;
            this.flowChart62.ErrID = 0;
            this.flowChart62.InAlarm = false;
            this.flowChart62.IsFlowHead = false;
            this.flowChart62.Location = new System.Drawing.Point(427, 211);
            this.flowChart62.LockUI = false;
            this.flowChart62.Message = null;
            this.flowChart62.MsgID = 0;
            this.flowChart62.Name = "flowChart62";
            this.flowChart62.NEXT = this.flowChart63;
            this.flowChart62.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart62.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart62.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart62.OverTimeSpec = 100;
            this.flowChart62.Running = false;
            this.flowChart62.Size = new System.Drawing.Size(200, 30);
            this.flowChart62.SlowRunCycle = -1;
            this.flowChart62.StartFC = null;
            this.flowChart62.Text = "Destory (on -> delay -> off)";
            this.flowChart62.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart62_Run);
            // 
            // flowChart63
            // 
            this.flowChart63.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart63.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart63.CASE1 = null;
            this.flowChart63.CASE2 = null;
            this.flowChart63.CASE3 = null;
            this.flowChart63.CASE4 = null;
            this.flowChart63.ContinueRun = false;
            this.flowChart63.EndFC = null;
            this.flowChart63.ErrID = 0;
            this.flowChart63.InAlarm = false;
            this.flowChart63.IsFlowHead = false;
            this.flowChart63.Location = new System.Drawing.Point(427, 249);
            this.flowChart63.LockUI = false;
            this.flowChart63.Message = null;
            this.flowChart63.MsgID = 0;
            this.flowChart63.Name = "flowChart63";
            this.flowChart63.NEXT = this.flowChart64;
            this.flowChart63.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart63.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart63.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart63.OverTimeSpec = 100;
            this.flowChart63.Running = false;
            this.flowChart63.Size = new System.Drawing.Size(200, 30);
            this.flowChart63.SlowRunCycle = -1;
            this.flowChart63.StartFC = null;
            this.flowChart63.Text = "After destory delay";
            this.flowChart63.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart63_Run);
            // 
            // flowChart64
            // 
            this.flowChart64.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart64.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart64.CASE1 = null;
            this.flowChart64.CASE2 = null;
            this.flowChart64.CASE3 = null;
            this.flowChart64.CASE4 = null;
            this.flowChart64.ContinueRun = false;
            this.flowChart64.EndFC = null;
            this.flowChart64.ErrID = 0;
            this.flowChart64.InAlarm = false;
            this.flowChart64.IsFlowHead = false;
            this.flowChart64.Location = new System.Drawing.Point(427, 287);
            this.flowChart64.LockUI = false;
            this.flowChart64.Message = null;
            this.flowChart64.MsgID = 0;
            this.flowChart64.Name = "flowChart64";
            this.flowChart64.NEXT = this.flowChart65;
            this.flowChart64.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart64.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart64.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart64.OverTimeSpec = 100;
            this.flowChart64.Running = false;
            this.flowChart64.Size = new System.Drawing.Size(200, 30);
            this.flowChart64.SlowRunCycle = -1;
            this.flowChart64.StartFC = null;
            this.flowChart64.Text = "Axis Z to PlacePos + Offset";
            this.flowChart64.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart64_Run);
            // 
            // flowChart65
            // 
            this.flowChart65.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart65.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart65.CASE1 = null;
            this.flowChart65.CASE2 = null;
            this.flowChart65.CASE3 = null;
            this.flowChart65.CASE4 = null;
            this.flowChart65.ContinueRun = false;
            this.flowChart65.EndFC = null;
            this.flowChart65.ErrID = 0;
            this.flowChart65.InAlarm = false;
            this.flowChart65.IsFlowHead = false;
            this.flowChart65.Location = new System.Drawing.Point(427, 325);
            this.flowChart65.LockUI = false;
            this.flowChart65.Message = null;
            this.flowChart65.MsgID = 0;
            this.flowChart65.Name = "flowChart65";
            this.flowChart65.NEXT = this.flowChart66;
            this.flowChart65.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart65.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart65.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart65.OverTimeSpec = 100;
            this.flowChart65.Running = false;
            this.flowChart65.Size = new System.Drawing.Size(200, 30);
            this.flowChart65.SlowRunCycle = -1;
            this.flowChart65.StartFC = null;
            this.flowChart65.Text = "Axis Z to Safety";
            this.flowChart65.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart65_Run);
            // 
            // flowChart66
            // 
            this.flowChart66.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart66.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart66.CASE1 = null;
            this.flowChart66.CASE2 = null;
            this.flowChart66.CASE3 = null;
            this.flowChart66.CASE4 = null;
            this.flowChart66.ContinueRun = false;
            this.flowChart66.EndFC = null;
            this.flowChart66.ErrID = 0;
            this.flowChart66.InAlarm = false;
            this.flowChart66.IsFlowHead = false;
            this.flowChart66.Location = new System.Drawing.Point(427, 363);
            this.flowChart66.LockUI = false;
            this.flowChart66.Message = null;
            this.flowChart66.MsgID = 0;
            this.flowChart66.Name = "flowChart66";
            this.flowChart66.NEXT = this.flowChart67;
            this.flowChart66.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart66.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart66.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart66.OverTimeSpec = 100;
            this.flowChart66.Running = false;
            this.flowChart66.Size = new System.Drawing.Size(200, 30);
            this.flowChart66.SlowRunCycle = -1;
            this.flowChart66.StartFC = null;
            this.flowChart66.Text = "Open Vac.";
            this.flowChart66.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart66_Run);
            // 
            // flowChart67
            // 
            this.flowChart67.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart67.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart67.CASE1 = null;
            this.flowChart67.CASE2 = null;
            this.flowChart67.CASE3 = null;
            this.flowChart67.CASE4 = null;
            this.flowChart67.ContinueRun = false;
            this.flowChart67.EndFC = null;
            this.flowChart67.ErrID = 0;
            this.flowChart67.InAlarm = false;
            this.flowChart67.IsFlowHead = false;
            this.flowChart67.Location = new System.Drawing.Point(427, 401);
            this.flowChart67.LockUI = false;
            this.flowChart67.Message = null;
            this.flowChart67.MsgID = 0;
            this.flowChart67.Name = "flowChart67";
            this.flowChart67.NEXT = this.flowChart68;
            this.flowChart67.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart67.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart67.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart67.OverTimeSpec = 100;
            this.flowChart67.Running = false;
            this.flowChart67.Size = new System.Drawing.Size(200, 30);
            this.flowChart67.SlowRunCycle = -1;
            this.flowChart67.StartFC = null;
            this.flowChart67.Text = "Check Vac.";
            this.flowChart67.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart67_Run);
            // 
            // flowChart68
            // 
            this.flowChart68.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart68.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart68.CASE1 = null;
            this.flowChart68.CASE2 = null;
            this.flowChart68.CASE3 = null;
            this.flowChart68.CASE4 = null;
            this.flowChart68.ContinueRun = false;
            this.flowChart68.EndFC = null;
            this.flowChart68.ErrID = 0;
            this.flowChart68.InAlarm = false;
            this.flowChart68.IsFlowHead = false;
            this.flowChart68.Location = new System.Drawing.Point(427, 439);
            this.flowChart68.LockUI = false;
            this.flowChart68.Message = null;
            this.flowChart68.MsgID = 0;
            this.flowChart68.Name = "flowChart68";
            this.flowChart68.NEXT = this.flowChart69;
            this.flowChart68.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart68.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart68.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart68.OverTimeSpec = 100;
            this.flowChart68.Running = false;
            this.flowChart68.Size = new System.Drawing.Size(200, 30);
            this.flowChart68.SlowRunCycle = -1;
            this.flowChart68.StartFC = null;
            this.flowChart68.Text = "Close Vac.";
            this.flowChart68.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart68_Run);
            // 
            // flowChart69
            // 
            this.flowChart69.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart69.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart69.CASE1 = null;
            this.flowChart69.CASE2 = null;
            this.flowChart69.CASE3 = null;
            this.flowChart69.CASE4 = null;
            this.flowChart69.ContinueRun = false;
            this.flowChart69.EndFC = null;
            this.flowChart69.ErrID = 0;
            this.flowChart69.InAlarm = false;
            this.flowChart69.IsFlowHead = false;
            this.flowChart69.Location = new System.Drawing.Point(427, 477);
            this.flowChart69.LockUI = false;
            this.flowChart69.Message = null;
            this.flowChart69.MsgID = 0;
            this.flowChart69.Name = "flowChart69";
            this.flowChart69.NEXT = this.flowChart53;
            this.flowChart69.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart69.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart69.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart69.OverTimeSpec = 100;
            this.flowChart69.Running = false;
            this.flowChart69.Size = new System.Drawing.Size(200, 30);
            this.flowChart69.SlowRunCycle = -1;
            this.flowChart69.StartFC = null;
            this.flowChart69.Text = "Place Done";
            this.flowChart69.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart69_Run);
            // 
            // FC_HDT_B_AUTO_Inspection
            // 
            this.FC_HDT_B_AUTO_Inspection.BackColor = System.Drawing.Color.RoyalBlue;
            this.FC_HDT_B_AUTO_Inspection.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.FC_HDT_B_AUTO_Inspection.CASE1 = null;
            this.FC_HDT_B_AUTO_Inspection.CASE2 = null;
            this.FC_HDT_B_AUTO_Inspection.CASE3 = null;
            this.FC_HDT_B_AUTO_Inspection.CASE4 = null;
            this.FC_HDT_B_AUTO_Inspection.ContinueRun = false;
            this.FC_HDT_B_AUTO_Inspection.EndFC = null;
            this.FC_HDT_B_AUTO_Inspection.ErrID = 0;
            this.FC_HDT_B_AUTO_Inspection.InAlarm = false;
            this.FC_HDT_B_AUTO_Inspection.IsFlowHead = false;
            this.FC_HDT_B_AUTO_Inspection.Location = new System.Drawing.Point(783, 325);
            this.FC_HDT_B_AUTO_Inspection.LockUI = false;
            this.FC_HDT_B_AUTO_Inspection.Message = null;
            this.FC_HDT_B_AUTO_Inspection.MsgID = 0;
            this.FC_HDT_B_AUTO_Inspection.Name = "FC_HDT_B_AUTO_Inspection";
            this.FC_HDT_B_AUTO_Inspection.NEXT = this.flowChart47;
            this.FC_HDT_B_AUTO_Inspection.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.FC_HDT_B_AUTO_Inspection.OrgLocation = new System.Drawing.Point(0, 0);
            this.FC_HDT_B_AUTO_Inspection.OrgSize = new System.Drawing.Size(0, 0);
            this.FC_HDT_B_AUTO_Inspection.OverTimeSpec = 100;
            this.FC_HDT_B_AUTO_Inspection.Running = false;
            this.FC_HDT_B_AUTO_Inspection.Size = new System.Drawing.Size(200, 30);
            this.FC_HDT_B_AUTO_Inspection.SlowRunCycle = -1;
            this.FC_HDT_B_AUTO_Inspection.StartFC = null;
            this.FC_HDT_B_AUTO_Inspection.Text = "Start Inspection";
            this.FC_HDT_B_AUTO_Inspection.Run += new ProVLib.FlowChart.RunEventHandler(this.FC_HDT_B_AUTO_Inspection_Run);
            // 
            // flowChart81
            // 
            this.flowChart81.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart81.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart81.CASE1 = null;
            this.flowChart81.CASE2 = null;
            this.flowChart81.CASE3 = null;
            this.flowChart81.CASE4 = null;
            this.flowChart81.ContinueRun = false;
            this.flowChart81.EndFC = null;
            this.flowChart81.ErrID = 0;
            this.flowChart81.InAlarm = false;
            this.flowChart81.IsFlowHead = false;
            this.flowChart81.Location = new System.Drawing.Point(255, 439);
            this.flowChart81.LockUI = false;
            this.flowChart81.Message = null;
            this.flowChart81.MsgID = 0;
            this.flowChart81.Name = "flowChart81";
            this.flowChart81.NEXT = this.flowChart54;
            this.flowChart81.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart81.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart81.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart81.OverTimeSpec = 100;
            this.flowChart81.Running = false;
            this.flowChart81.Size = new System.Drawing.Size(50, 30);
            this.flowChart81.SlowRunCycle = -1;
            this.flowChart81.StartFC = null;
            this.flowChart81.Text = "Next";
            this.flowChart81.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart81_Run);
            // 
            // flowChart54
            // 
            this.flowChart54.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart54.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart54.CASE1 = null;
            this.flowChart54.CASE2 = null;
            this.flowChart54.CASE3 = null;
            this.flowChart54.CASE4 = null;
            this.flowChart54.ContinueRun = false;
            this.flowChart54.EndFC = null;
            this.flowChart54.ErrID = 0;
            this.flowChart54.InAlarm = false;
            this.flowChart54.IsFlowHead = false;
            this.flowChart54.Location = new System.Drawing.Point(49, 439);
            this.flowChart54.LockUI = false;
            this.flowChart54.Message = null;
            this.flowChart54.MsgID = 0;
            this.flowChart54.Name = "flowChart54";
            this.flowChart54.NEXT = this.flowChart71;
            this.flowChart54.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart54.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart54.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart54.OverTimeSpec = 100;
            this.flowChart54.Running = false;
            this.flowChart54.Size = new System.Drawing.Size(200, 30);
            this.flowChart54.SlowRunCycle = -1;
            this.flowChart54.StartFC = null;
            this.flowChart54.Text = "Need Move?";
            this.flowChart54.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart54_Run);
            // 
            // flowChart71
            // 
            this.flowChart71.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart71.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart71.CASE1 = null;
            this.flowChart71.CASE2 = null;
            this.flowChart71.CASE3 = null;
            this.flowChart71.CASE4 = null;
            this.flowChart71.ContinueRun = false;
            this.flowChart71.EndFC = null;
            this.flowChart71.ErrID = 0;
            this.flowChart71.InAlarm = false;
            this.flowChart71.IsFlowHead = false;
            this.flowChart71.Location = new System.Drawing.Point(49, 477);
            this.flowChart71.LockUI = false;
            this.flowChart71.Message = null;
            this.flowChart71.MsgID = 0;
            this.flowChart71.Name = "flowChart71";
            this.flowChart71.NEXT = this.flowChart80;
            this.flowChart71.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart71.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart71.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart71.OverTimeSpec = 100;
            this.flowChart71.Running = false;
            this.flowChart71.Size = new System.Drawing.Size(200, 30);
            this.flowChart71.SlowRunCycle = -1;
            this.flowChart71.StartFC = null;
            this.flowChart71.Text = "Move to Position (X & U)";
            this.flowChart71.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart71_Run);
            // 
            // flowChart80
            // 
            this.flowChart80.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart80.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart80.CASE1 = null;
            this.flowChart80.CASE2 = null;
            this.flowChart80.CASE3 = null;
            this.flowChart80.CASE4 = null;
            this.flowChart80.ContinueRun = false;
            this.flowChart80.EndFC = null;
            this.flowChart80.ErrID = 0;
            this.flowChart80.InAlarm = false;
            this.flowChart80.IsFlowHead = false;
            this.flowChart80.Location = new System.Drawing.Point(255, 477);
            this.flowChart80.LockUI = false;
            this.flowChart80.Message = null;
            this.flowChart80.MsgID = 0;
            this.flowChart80.Name = "flowChart80";
            this.flowChart80.NEXT = this.flowChart81;
            this.flowChart80.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart80.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart80.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart80.OverTimeSpec = 100;
            this.flowChart80.Running = false;
            this.flowChart80.Size = new System.Drawing.Size(50, 30);
            this.flowChart80.SlowRunCycle = -1;
            this.flowChart80.StartFC = null;
            this.flowChart80.Text = "Done";
            this.flowChart80.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart80_Run);
            // 
            // FC_HDT_B_AUTO_Move
            // 
            this.FC_HDT_B_AUTO_Move.BackColor = System.Drawing.Color.RoyalBlue;
            this.FC_HDT_B_AUTO_Move.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.FC_HDT_B_AUTO_Move.CASE1 = null;
            this.FC_HDT_B_AUTO_Move.CASE2 = null;
            this.FC_HDT_B_AUTO_Move.CASE3 = null;
            this.FC_HDT_B_AUTO_Move.CASE4 = null;
            this.FC_HDT_B_AUTO_Move.ContinueRun = false;
            this.FC_HDT_B_AUTO_Move.EndFC = null;
            this.FC_HDT_B_AUTO_Move.ErrID = 0;
            this.FC_HDT_B_AUTO_Move.InAlarm = false;
            this.FC_HDT_B_AUTO_Move.IsFlowHead = false;
            this.FC_HDT_B_AUTO_Move.Location = new System.Drawing.Point(49, 401);
            this.FC_HDT_B_AUTO_Move.LockUI = false;
            this.FC_HDT_B_AUTO_Move.Message = null;
            this.FC_HDT_B_AUTO_Move.MsgID = 0;
            this.FC_HDT_B_AUTO_Move.Name = "FC_HDT_B_AUTO_Move";
            this.FC_HDT_B_AUTO_Move.NEXT = this.flowChart54;
            this.FC_HDT_B_AUTO_Move.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.FC_HDT_B_AUTO_Move.OrgLocation = new System.Drawing.Point(0, 0);
            this.FC_HDT_B_AUTO_Move.OrgSize = new System.Drawing.Size(0, 0);
            this.FC_HDT_B_AUTO_Move.OverTimeSpec = 100;
            this.FC_HDT_B_AUTO_Move.Running = false;
            this.FC_HDT_B_AUTO_Move.Size = new System.Drawing.Size(200, 30);
            this.FC_HDT_B_AUTO_Move.SlowRunCycle = -1;
            this.FC_HDT_B_AUTO_Move.StartFC = null;
            this.FC_HDT_B_AUTO_Move.Text = "Start Move";
            this.FC_HDT_B_AUTO_Move.Run += new ProVLib.FlowChart.RunEventHandler(this.FC_HDT_B_AUTO_Move_Run);
            // 
            // FC_HDT_HOME
            // 
            this.FC_HDT_HOME.BackColor = System.Drawing.Color.RoyalBlue;
            this.FC_HDT_HOME.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.FC_HDT_HOME.CASE1 = null;
            this.FC_HDT_HOME.CASE2 = null;
            this.FC_HDT_HOME.CASE3 = null;
            this.FC_HDT_HOME.CASE4 = null;
            this.FC_HDT_HOME.ContinueRun = false;
            this.FC_HDT_HOME.EndFC = null;
            this.FC_HDT_HOME.ErrID = 0;
            this.FC_HDT_HOME.InAlarm = false;
            this.FC_HDT_HOME.IsFlowHead = false;
            this.FC_HDT_HOME.Location = new System.Drawing.Point(149, 41);
            this.FC_HDT_HOME.LockUI = false;
            this.FC_HDT_HOME.Message = null;
            this.FC_HDT_HOME.MsgID = 0;
            this.FC_HDT_HOME.Name = "FC_HDT_HOME";
            this.FC_HDT_HOME.NEXT = this.flowChart1;
            this.FC_HDT_HOME.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.FC_HDT_HOME.OrgLocation = new System.Drawing.Point(0, 0);
            this.FC_HDT_HOME.OrgSize = new System.Drawing.Size(0, 0);
            this.FC_HDT_HOME.OverTimeSpec = 100;
            this.FC_HDT_HOME.Running = false;
            this.FC_HDT_HOME.Size = new System.Drawing.Size(200, 30);
            this.FC_HDT_HOME.SlowRunCycle = -1;
            this.FC_HDT_HOME.StartFC = null;
            this.FC_HDT_HOME.Text = "Start Home";
            this.FC_HDT_HOME.Run += new ProVLib.FlowChart.RunEventHandler(this.FC_HDT_HOME_Run);
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
            this.flowChart1.Location = new System.Drawing.Point(149, 79);
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
            this.flowChart1.Text = "Z Home (Include CCD Z)";
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
            this.flowChart2.Location = new System.Drawing.Point(149, 117);
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
            this.flowChart2.Text = "X & U Home";
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
            this.flowChart3.Location = new System.Drawing.Point(149, 155);
            this.flowChart3.LockUI = false;
            this.flowChart3.Message = null;
            this.flowChart3.MsgID = 0;
            this.flowChart3.Name = "flowChart3";
            this.flowChart3.NEXT = this.flowChart4;
            this.flowChart3.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart3.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart3.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart3.OverTimeSpec = 100;
            this.flowChart3.Running = false;
            this.flowChart3.Size = new System.Drawing.Size(200, 30);
            this.flowChart3.SlowRunCycle = -1;
            this.flowChart3.StartFC = null;
            this.flowChart3.Text = "Vacuum On";
            this.flowChart3.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart3_Run);
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
            this.flowChart4.Location = new System.Drawing.Point(149, 193);
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
            this.flowChart4.Text = "Detect IC Remain";
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
            this.flowChart5.Location = new System.Drawing.Point(149, 231);
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
            this.flowChart5.Text = "Vacuum Off";
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
            this.flowChart6.Location = new System.Drawing.Point(149, 269);
            this.flowChart6.LockUI = false;
            this.flowChart6.Message = null;
            this.flowChart6.MsgID = 0;
            this.flowChart6.Name = "flowChart6";
            this.flowChart6.NEXT = this.flowChart7;
            this.flowChart6.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart6.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart6.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart6.OverTimeSpec = 100;
            this.flowChart6.Running = false;
            this.flowChart6.Size = new System.Drawing.Size(200, 30);
            this.flowChart6.SlowRunCycle = -1;
            this.flowChart6.StartFC = null;
            this.flowChart6.Text = "Destory On";
            this.flowChart6.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart6_Run);
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
            this.flowChart7.Location = new System.Drawing.Point(149, 307);
            this.flowChart7.LockUI = false;
            this.flowChart7.Message = null;
            this.flowChart7.MsgID = 0;
            this.flowChart7.Name = "flowChart7";
            this.flowChart7.NEXT = this.flowChart83;
            this.flowChart7.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart7.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart7.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart7.OverTimeSpec = 100;
            this.flowChart7.Running = false;
            this.flowChart7.Size = new System.Drawing.Size(200, 30);
            this.flowChart7.SlowRunCycle = -1;
            this.flowChart7.StartFC = null;
            this.flowChart7.Text = "Destory Off";
            this.flowChart7.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart7_Run);
            // 
            // flowChart83
            // 
            this.flowChart83.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart83.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart83.CASE1 = null;
            this.flowChart83.CASE2 = null;
            this.flowChart83.CASE3 = null;
            this.flowChart83.CASE4 = null;
            this.flowChart83.ContinueRun = false;
            this.flowChart83.EndFC = null;
            this.flowChart83.ErrID = 0;
            this.flowChart83.InAlarm = false;
            this.flowChart83.IsFlowHead = false;
            this.flowChart83.Location = new System.Drawing.Point(149, 345);
            this.flowChart83.LockUI = false;
            this.flowChart83.Message = null;
            this.flowChart83.MsgID = 0;
            this.flowChart83.Name = "flowChart83";
            this.flowChart83.NEXT = this.flowChart8;
            this.flowChart83.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart83.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart83.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart83.OverTimeSpec = 100;
            this.flowChart83.Running = false;
            this.flowChart83.Size = new System.Drawing.Size(200, 30);
            this.flowChart83.SlowRunCycle = -1;
            this.flowChart83.StartFC = null;
            this.flowChart83.Text = "Mve U to Ready";
            this.flowChart83.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart83_Run);
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
            this.flowChart8.Location = new System.Drawing.Point(376, 345);
            this.flowChart8.LockUI = false;
            this.flowChart8.Message = null;
            this.flowChart8.MsgID = 0;
            this.flowChart8.Name = "flowChart8";
            this.flowChart8.NEXT = this.flowChart82;
            this.flowChart8.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart8.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart8.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart8.OverTimeSpec = 100;
            this.flowChart8.Running = false;
            this.flowChart8.Size = new System.Drawing.Size(200, 30);
            this.flowChart8.SlowRunCycle = -1;
            this.flowChart8.StartFC = null;
            this.flowChart8.Text = "Vision Set CCD goto ready";
            this.flowChart8.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart8_Run);
            // 
            // flowChart82
            // 
            this.flowChart82.BackColor = System.Drawing.Color.RoyalBlue;
            this.flowChart82.CaptionFont = new System.Drawing.Font("微軟正黑體", 10F);
            this.flowChart82.CASE1 = null;
            this.flowChart82.CASE2 = null;
            this.flowChart82.CASE3 = null;
            this.flowChart82.CASE4 = null;
            this.flowChart82.ContinueRun = false;
            this.flowChart82.EndFC = null;
            this.flowChart82.ErrID = 0;
            this.flowChart82.InAlarm = false;
            this.flowChart82.IsFlowHead = false;
            this.flowChart82.Location = new System.Drawing.Point(376, 383);
            this.flowChart82.LockUI = false;
            this.flowChart82.Message = null;
            this.flowChart82.MsgID = 0;
            this.flowChart82.Name = "flowChart82";
            this.flowChart82.NEXT = this.flowChart9;
            this.flowChart82.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart82.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart82.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart82.OverTimeSpec = 100;
            this.flowChart82.Running = false;
            this.flowChart82.Size = new System.Drawing.Size(200, 30);
            this.flowChart82.SlowRunCycle = -1;
            this.flowChart82.StartFC = null;
            this.flowChart82.Text = "Check Connection state";
            this.flowChart82.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart82_Run);
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
            this.flowChart9.Location = new System.Drawing.Point(149, 383);
            this.flowChart9.LockUI = false;
            this.flowChart9.Message = null;
            this.flowChart9.MsgID = 0;
            this.flowChart9.Name = "flowChart9";
            this.flowChart9.NEXT = null;
            this.flowChart9.ObjType = ProVLib.EObjType.VOID_TYPE;
            this.flowChart9.OrgLocation = new System.Drawing.Point(0, 0);
            this.flowChart9.OrgSize = new System.Drawing.Size(0, 0);
            this.flowChart9.OverTimeSpec = 100;
            this.flowChart9.Running = false;
            this.flowChart9.Size = new System.Drawing.Size(200, 30);
            this.flowChart9.SlowRunCycle = -1;
            this.flowChart9.StartFC = null;
            this.flowChart9.Text = "Done";
            this.flowChart9.Run += new ProVLib.FlowChart.RunEventHandler(this.flowChart9_Run);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dPosEdit47);
            this.groupBox3.Controls.Add(this.dPosEdit30);
            this.groupBox3.Controls.Add(this.dPosEdit11);
            this.groupBox3.Controls.Add(this.dPosEdit12);
            this.groupBox3.Controls.Add(this.dPosEdit15);
            this.groupBox3.Controls.Add(this.dPosEdit10);
            this.groupBox3.Controls.Add(this.dPosEdit14);
            this.groupBox3.Controls.Add(this.dPosEdit4);
            this.groupBox3.Controls.Add(this.dPosEdit7);
            this.groupBox3.Controls.Add(this.dPosEdit8);
            this.groupBox3.Controls.Add(this.dPosEdit6);
            this.groupBox3.Controls.Add(this.dPosEdit5);
            this.groupBox3.Controls.Add(this.dPosEdit2);
            this.groupBox3.Controls.Add(this.dPosEdit3);
            this.groupBox3.Controls.Add(this.dPosEdit9);
            this.groupBox3.Controls.Add(this.dPosEdit1);
            this.groupBox3.Location = new System.Drawing.Point(18, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(381, 546);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Board HeadA";
            // 
            // dPosEdit47
            // 
            this.dPosEdit47.AutoFocus = false;
            this.dPosEdit47.Caption = "Nozzle and CCD Offset Y";
            this.dPosEdit47.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit47.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit47.DataName = "Pos_HDT_A_Nozzle_CCD_OffsetY";
            this.dPosEdit47.DataSource = this.SetDS;
            this.dPosEdit47.DefaultValue = null;
            this.dPosEdit47.EditColor = System.Drawing.Color.Black;
            this.dPosEdit47.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit47.EditWidth = 100;
            this.dPosEdit47.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit47.IsModified = false;
            this.dPosEdit47.Location = new System.Drawing.Point(14, 96);
            this.dPosEdit47.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit47.MaxValue = 9999999D;
            this.dPosEdit47.MinValue = -9999999D;
            this.dPosEdit47.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit47.MotorP = null;
            this.dPosEdit47.Name = "dPosEdit47";
            this.dPosEdit47.NoChangeInAuto = false;
            this.dPosEdit47.PosValue = "";
            this.dPosEdit47.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit47.StepValue = 0D;
            this.dPosEdit47.TabIndex = 77;
            this.dPosEdit47.Unit = "um";
            this.dPosEdit47.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit47.UnitWidth = 40;
            this.dPosEdit47.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit30
            // 
            this.dPosEdit30.AutoFocus = false;
            this.dPosEdit30.Caption = "Rotate Base Position";
            this.dPosEdit30.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit30.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit30.DataName = "Pos_HDT_A_U_Ready";
            this.dPosEdit30.DataSource = this.SetDS;
            this.dPosEdit30.DefaultValue = null;
            this.dPosEdit30.EditColor = System.Drawing.Color.Black;
            this.dPosEdit30.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit30.EditWidth = 100;
            this.dPosEdit30.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit30.IsModified = false;
            this.dPosEdit30.Location = new System.Drawing.Point(14, 516);
            this.dPosEdit30.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit30.MaxValue = 9999999D;
            this.dPosEdit30.MinValue = -9999999D;
            this.dPosEdit30.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit30.MotorP = this.MT_HDT_A_AxisU;
            this.dPosEdit30.Name = "dPosEdit30";
            this.dPosEdit30.NoChangeInAuto = false;
            this.dPosEdit30.PosValue = "";
            this.dPosEdit30.Size = new System.Drawing.Size(350, 26);
            this.dPosEdit30.StepValue = 0D;
            this.dPosEdit30.TabIndex = 76;
            this.dPosEdit30.Unit = "um";
            this.dPosEdit30.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit30.UnitWidth = 40;
            this.dPosEdit30.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit11
            // 
            this.dPosEdit11.AutoFocus = false;
            this.dPosEdit11.Caption = "SortBox Place Z";
            this.dPosEdit11.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit11.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit11.DataName = "Pos_HDT_A_SortBox_Z";
            this.dPosEdit11.DataSource = this.SetDS;
            this.dPosEdit11.DefaultValue = null;
            this.dPosEdit11.EditColor = System.Drawing.Color.Black;
            this.dPosEdit11.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit11.EditWidth = 100;
            this.dPosEdit11.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit11.IsModified = false;
            this.dPosEdit11.Location = new System.Drawing.Point(14, 441);
            this.dPosEdit11.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit11.MaxValue = 9999999D;
            this.dPosEdit11.MinValue = -9999999D;
            this.dPosEdit11.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit11.MotorP = this.MT_HDT_A_AxisZ;
            this.dPosEdit11.Name = "dPosEdit11";
            this.dPosEdit11.NoChangeInAuto = false;
            this.dPosEdit11.PosValue = "";
            this.dPosEdit11.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit11.StepValue = 0D;
            this.dPosEdit11.TabIndex = 75;
            this.dPosEdit11.Unit = "um";
            this.dPosEdit11.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit11.UnitWidth = 40;
            this.dPosEdit11.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit12
            // 
            this.dPosEdit12.AutoFocus = false;
            this.dPosEdit12.Caption = "SortBox_Fail X";
            this.dPosEdit12.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit12.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit12.DataName = "Pos_HDT_A_SortBox_Fail_X";
            this.dPosEdit12.DataSource = this.SetDS;
            this.dPosEdit12.DefaultValue = null;
            this.dPosEdit12.EditColor = System.Drawing.Color.Black;
            this.dPosEdit12.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit12.EditWidth = 100;
            this.dPosEdit12.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit12.IsModified = false;
            this.dPosEdit12.Location = new System.Drawing.Point(14, 416);
            this.dPosEdit12.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit12.MaxValue = 9999999D;
            this.dPosEdit12.MinValue = -9999999D;
            this.dPosEdit12.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit12.MotorP = this.MT_HDT_A_AxisX;
            this.dPosEdit12.Name = "dPosEdit12";
            this.dPosEdit12.NoChangeInAuto = false;
            this.dPosEdit12.PosValue = "";
            this.dPosEdit12.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit12.StepValue = 0D;
            this.dPosEdit12.TabIndex = 74;
            this.dPosEdit12.Unit = "um";
            this.dPosEdit12.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit12.UnitWidth = 40;
            this.dPosEdit12.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit15
            // 
            this.dPosEdit15.AutoFocus = false;
            this.dPosEdit15.Caption = "SortBox_Pass X";
            this.dPosEdit15.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit15.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit15.DataName = "Pos_HDT_A_SortBox_Pass_X";
            this.dPosEdit15.DataSource = this.SetDS;
            this.dPosEdit15.DefaultValue = null;
            this.dPosEdit15.EditColor = System.Drawing.Color.Black;
            this.dPosEdit15.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit15.EditWidth = 100;
            this.dPosEdit15.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit15.IsModified = false;
            this.dPosEdit15.Location = new System.Drawing.Point(14, 391);
            this.dPosEdit15.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit15.MaxValue = 9999999D;
            this.dPosEdit15.MinValue = -9999999D;
            this.dPosEdit15.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit15.MotorP = this.MT_HDT_A_AxisX;
            this.dPosEdit15.Name = "dPosEdit15";
            this.dPosEdit15.NoChangeInAuto = false;
            this.dPosEdit15.PosValue = "";
            this.dPosEdit15.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit15.StepValue = 0D;
            this.dPosEdit15.TabIndex = 72;
            this.dPosEdit15.Unit = "um";
            this.dPosEdit15.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit15.UnitWidth = 40;
            this.dPosEdit15.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit10
            // 
            this.dPosEdit10.AutoFocus = false;
            this.dPosEdit10.Caption = "Nozzle and CCD Offset X";
            this.dPosEdit10.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit10.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit10.DataName = "Pos_HDT_A_Nozzle_CCD_Offset";
            this.dPosEdit10.DataSource = this.SetDS;
            this.dPosEdit10.DefaultValue = null;
            this.dPosEdit10.EditColor = System.Drawing.Color.Black;
            this.dPosEdit10.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit10.EditWidth = 100;
            this.dPosEdit10.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit10.IsModified = false;
            this.dPosEdit10.Location = new System.Drawing.Point(14, 71);
            this.dPosEdit10.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit10.MaxValue = 9999999D;
            this.dPosEdit10.MinValue = -9999999D;
            this.dPosEdit10.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit10.MotorP = this.MT_HDT_A_AxisX;
            this.dPosEdit10.Name = "dPosEdit10";
            this.dPosEdit10.NoChangeInAuto = false;
            this.dPosEdit10.PosValue = "";
            this.dPosEdit10.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit10.StepValue = 0D;
            this.dPosEdit10.TabIndex = 71;
            this.dPosEdit10.Unit = "um";
            this.dPosEdit10.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit10.UnitWidth = 40;
            this.dPosEdit10.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit14
            // 
            this.dPosEdit14.AutoFocus = false;
            this.dPosEdit14.Caption = "Rotate Resolution(360°)";
            this.dPosEdit14.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit14.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit14.DataName = "Pos_HDT_A_RotateResolution";
            this.dPosEdit14.DataSource = this.SetDS;
            this.dPosEdit14.DefaultValue = null;
            this.dPosEdit14.EditColor = System.Drawing.Color.Black;
            this.dPosEdit14.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit14.EditWidth = 100;
            this.dPosEdit14.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit14.IsModified = false;
            this.dPosEdit14.Location = new System.Drawing.Point(14, 490);
            this.dPosEdit14.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit14.MaxValue = 9999999D;
            this.dPosEdit14.MinValue = -9999999D;
            this.dPosEdit14.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit14.MotorP = this.MT_HDT_A_AxisU;
            this.dPosEdit14.Name = "dPosEdit14";
            this.dPosEdit14.NoChangeInAuto = false;
            this.dPosEdit14.PosValue = "";
            this.dPosEdit14.Size = new System.Drawing.Size(350, 26);
            this.dPosEdit14.StepValue = 0D;
            this.dPosEdit14.TabIndex = 70;
            this.dPosEdit14.Unit = "pls";
            this.dPosEdit14.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit14.UnitWidth = 40;
            this.dPosEdit14.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit4
            // 
            this.dPosEdit4.AutoFocus = false;
            this.dPosEdit4.Caption = "Axis Z Safty Height";
            this.dPosEdit4.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit4.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit4.DataName = "Pos_HDT_A_Safe_Z";
            this.dPosEdit4.DataSource = this.SetDS;
            this.dPosEdit4.DefaultValue = null;
            this.dPosEdit4.EditColor = System.Drawing.Color.Black;
            this.dPosEdit4.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit4.EditWidth = 100;
            this.dPosEdit4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit4.IsModified = false;
            this.dPosEdit4.Location = new System.Drawing.Point(14, 46);
            this.dPosEdit4.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit4.MaxValue = 9999999D;
            this.dPosEdit4.MinValue = -9999999D;
            this.dPosEdit4.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit4.MotorP = this.MT_HDT_A_AxisZ;
            this.dPosEdit4.Name = "dPosEdit4";
            this.dPosEdit4.NoChangeInAuto = false;
            this.dPosEdit4.PosValue = "";
            this.dPosEdit4.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit4.StepValue = 0D;
            this.dPosEdit4.TabIndex = 48;
            this.dPosEdit4.Unit = "um";
            this.dPosEdit4.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit4.UnitWidth = 40;
            this.dPosEdit4.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit7
            // 
            this.dPosEdit7.AutoFocus = false;
            this.dPosEdit7.Caption = "Transfer ShuttleB Z";
            this.dPosEdit7.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit7.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit7.DataName = "Pos_HDT_A_TransferShuttleB_Z";
            this.dPosEdit7.DataSource = this.SetDS;
            this.dPosEdit7.DefaultValue = null;
            this.dPosEdit7.EditColor = System.Drawing.Color.Black;
            this.dPosEdit7.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit7.EditWidth = 100;
            this.dPosEdit7.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit7.IsModified = false;
            this.dPosEdit7.Location = new System.Drawing.Point(14, 342);
            this.dPosEdit7.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit7.MaxValue = 9999999D;
            this.dPosEdit7.MinValue = -9999999D;
            this.dPosEdit7.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit7.MotorP = this.MT_HDT_A_AxisZ;
            this.dPosEdit7.Name = "dPosEdit7";
            this.dPosEdit7.NoChangeInAuto = false;
            this.dPosEdit7.PosValue = "";
            this.dPosEdit7.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit7.StepValue = 0D;
            this.dPosEdit7.TabIndex = 47;
            this.dPosEdit7.Unit = "um";
            this.dPosEdit7.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit7.UnitWidth = 40;
            this.dPosEdit7.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit8
            // 
            this.dPosEdit8.AutoFocus = false;
            this.dPosEdit8.Caption = "Transfer ShuttleB X";
            this.dPosEdit8.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit8.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit8.DataName = "Pos_HDT_A_TransferShuttleB_X";
            this.dPosEdit8.DataSource = this.SetDS;
            this.dPosEdit8.DefaultValue = null;
            this.dPosEdit8.EditColor = System.Drawing.Color.Black;
            this.dPosEdit8.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit8.EditWidth = 100;
            this.dPosEdit8.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit8.IsModified = false;
            this.dPosEdit8.Location = new System.Drawing.Point(14, 317);
            this.dPosEdit8.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit8.MaxValue = 9999999D;
            this.dPosEdit8.MinValue = -9999999D;
            this.dPosEdit8.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit8.MotorP = this.MT_HDT_A_AxisX;
            this.dPosEdit8.Name = "dPosEdit8";
            this.dPosEdit8.NoChangeInAuto = false;
            this.dPosEdit8.PosValue = "";
            this.dPosEdit8.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit8.StepValue = 0D;
            this.dPosEdit8.TabIndex = 46;
            this.dPosEdit8.Unit = "um";
            this.dPosEdit8.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit8.UnitWidth = 40;
            this.dPosEdit8.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit6
            // 
            this.dPosEdit6.AutoFocus = false;
            this.dPosEdit6.Caption = "Transfer ShuttleA Z";
            this.dPosEdit6.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit6.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit6.DataName = "Pos_HDT_A_TransferShuttleA_Z";
            this.dPosEdit6.DataSource = this.SetDS;
            this.dPosEdit6.DefaultValue = null;
            this.dPosEdit6.EditColor = System.Drawing.Color.Black;
            this.dPosEdit6.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit6.EditWidth = 100;
            this.dPosEdit6.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit6.IsModified = false;
            this.dPosEdit6.Location = new System.Drawing.Point(14, 292);
            this.dPosEdit6.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit6.MaxValue = 9999999D;
            this.dPosEdit6.MinValue = -9999999D;
            this.dPosEdit6.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit6.MotorP = this.MT_HDT_A_AxisZ;
            this.dPosEdit6.Name = "dPosEdit6";
            this.dPosEdit6.NoChangeInAuto = false;
            this.dPosEdit6.PosValue = "";
            this.dPosEdit6.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit6.StepValue = 0D;
            this.dPosEdit6.TabIndex = 45;
            this.dPosEdit6.Unit = "um";
            this.dPosEdit6.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit6.UnitWidth = 40;
            this.dPosEdit6.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit5
            // 
            this.dPosEdit5.AutoFocus = false;
            this.dPosEdit5.Caption = "Transfer ShuttleA X";
            this.dPosEdit5.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit5.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit5.DataName = "Pos_HDT_A_TransferShuttleA_X";
            this.dPosEdit5.DataSource = this.SetDS;
            this.dPosEdit5.DefaultValue = null;
            this.dPosEdit5.EditColor = System.Drawing.Color.Black;
            this.dPosEdit5.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit5.EditWidth = 100;
            this.dPosEdit5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit5.IsModified = false;
            this.dPosEdit5.Location = new System.Drawing.Point(14, 267);
            this.dPosEdit5.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit5.MaxValue = 9999999D;
            this.dPosEdit5.MinValue = -9999999D;
            this.dPosEdit5.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit5.MotorP = this.MT_HDT_A_AxisX;
            this.dPosEdit5.Name = "dPosEdit5";
            this.dPosEdit5.NoChangeInAuto = false;
            this.dPosEdit5.PosValue = "";
            this.dPosEdit5.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit5.StepValue = 0D;
            this.dPosEdit5.TabIndex = 44;
            this.dPosEdit5.Unit = "um";
            this.dPosEdit5.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit5.UnitWidth = 40;
            this.dPosEdit5.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit2
            // 
            this.dPosEdit2.AutoFocus = false;
            this.dPosEdit2.Caption = "Board StageB Z";
            this.dPosEdit2.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit2.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit2.DataName = "Pos_HDT_A_BoardStageB_Z";
            this.dPosEdit2.DataSource = this.SetDS;
            this.dPosEdit2.DefaultValue = null;
            this.dPosEdit2.EditColor = System.Drawing.Color.Black;
            this.dPosEdit2.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit2.EditWidth = 100;
            this.dPosEdit2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit2.IsModified = false;
            this.dPosEdit2.Location = new System.Drawing.Point(14, 219);
            this.dPosEdit2.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit2.MaxValue = 9999999D;
            this.dPosEdit2.MinValue = -9999999D;
            this.dPosEdit2.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit2.MotorP = this.MT_HDT_A_AxisZ;
            this.dPosEdit2.Name = "dPosEdit2";
            this.dPosEdit2.NoChangeInAuto = false;
            this.dPosEdit2.PosValue = "";
            this.dPosEdit2.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit2.StepValue = 0D;
            this.dPosEdit2.TabIndex = 43;
            this.dPosEdit2.Unit = "um";
            this.dPosEdit2.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit2.UnitWidth = 40;
            this.dPosEdit2.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit3
            // 
            this.dPosEdit3.AutoFocus = false;
            this.dPosEdit3.Caption = "Board StageB X";
            this.dPosEdit3.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit3.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit3.DataName = "Pos_HDT_A_BoardStageB_X";
            this.dPosEdit3.DataSource = this.SetDS;
            this.dPosEdit3.DefaultValue = null;
            this.dPosEdit3.EditColor = System.Drawing.Color.Black;
            this.dPosEdit3.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit3.EditWidth = 100;
            this.dPosEdit3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit3.IsModified = false;
            this.dPosEdit3.Location = new System.Drawing.Point(14, 194);
            this.dPosEdit3.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit3.MaxValue = 9999999D;
            this.dPosEdit3.MinValue = -9999999D;
            this.dPosEdit3.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit3.MotorP = this.MT_HDT_A_AxisX;
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
            // dPosEdit9
            // 
            this.dPosEdit9.AutoFocus = false;
            this.dPosEdit9.Caption = "Board StageA Z";
            this.dPosEdit9.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit9.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit9.DataName = "Pos_HDT_A_BoardStageA_Z";
            this.dPosEdit9.DataSource = this.SetDS;
            this.dPosEdit9.DefaultValue = null;
            this.dPosEdit9.EditColor = System.Drawing.Color.Black;
            this.dPosEdit9.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit9.EditWidth = 100;
            this.dPosEdit9.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit9.IsModified = false;
            this.dPosEdit9.Location = new System.Drawing.Point(14, 169);
            this.dPosEdit9.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit9.MaxValue = 9999999D;
            this.dPosEdit9.MinValue = -9999999D;
            this.dPosEdit9.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit9.MotorP = this.MT_HDT_A_AxisZ;
            this.dPosEdit9.Name = "dPosEdit9";
            this.dPosEdit9.NoChangeInAuto = false;
            this.dPosEdit9.PosValue = "";
            this.dPosEdit9.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit9.StepValue = 0D;
            this.dPosEdit9.TabIndex = 41;
            this.dPosEdit9.Unit = "um";
            this.dPosEdit9.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit9.UnitWidth = 40;
            this.dPosEdit9.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit1
            // 
            this.dPosEdit1.AutoFocus = false;
            this.dPosEdit1.Caption = "Board StageA X";
            this.dPosEdit1.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit1.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit1.DataName = "Pos_HDT_A_BoardStageA_X";
            this.dPosEdit1.DataSource = this.SetDS;
            this.dPosEdit1.DefaultValue = null;
            this.dPosEdit1.EditColor = System.Drawing.Color.Black;
            this.dPosEdit1.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit1.EditWidth = 100;
            this.dPosEdit1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit1.IsModified = false;
            this.dPosEdit1.Location = new System.Drawing.Point(14, 144);
            this.dPosEdit1.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit1.MaxValue = 9999999D;
            this.dPosEdit1.MinValue = -9999999D;
            this.dPosEdit1.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit1.MotorP = this.MT_HDT_A_AxisX;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dPosEdit48);
            this.groupBox1.Controls.Add(this.dPosEdit29);
            this.groupBox1.Controls.Add(this.dPosEdit13);
            this.groupBox1.Controls.Add(this.dPosEdit16);
            this.groupBox1.Controls.Add(this.dPosEdit17);
            this.groupBox1.Controls.Add(this.dPosEdit18);
            this.groupBox1.Controls.Add(this.dPosEdit19);
            this.groupBox1.Controls.Add(this.dPosEdit20);
            this.groupBox1.Controls.Add(this.dPosEdit21);
            this.groupBox1.Controls.Add(this.dPosEdit22);
            this.groupBox1.Controls.Add(this.dPosEdit23);
            this.groupBox1.Controls.Add(this.dPosEdit24);
            this.groupBox1.Controls.Add(this.dPosEdit25);
            this.groupBox1.Controls.Add(this.dPosEdit26);
            this.groupBox1.Controls.Add(this.dPosEdit27);
            this.groupBox1.Controls.Add(this.dPosEdit28);
            this.groupBox1.Location = new System.Drawing.Point(441, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 546);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Board HeadB";
            // 
            // dPosEdit48
            // 
            this.dPosEdit48.AutoFocus = false;
            this.dPosEdit48.Caption = "Nozzle and CCD Offset Y";
            this.dPosEdit48.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit48.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit48.DataName = "Pos_HDT_B_Nozzle_CCD_OffsetY";
            this.dPosEdit48.DataSource = this.SetDS;
            this.dPosEdit48.DefaultValue = null;
            this.dPosEdit48.EditColor = System.Drawing.Color.Black;
            this.dPosEdit48.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit48.EditWidth = 100;
            this.dPosEdit48.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit48.IsModified = false;
            this.dPosEdit48.Location = new System.Drawing.Point(14, 96);
            this.dPosEdit48.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit48.MaxValue = 9999999D;
            this.dPosEdit48.MinValue = -9999999D;
            this.dPosEdit48.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit48.MotorP = null;
            this.dPosEdit48.Name = "dPosEdit48";
            this.dPosEdit48.NoChangeInAuto = false;
            this.dPosEdit48.PosValue = "";
            this.dPosEdit48.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit48.StepValue = 0D;
            this.dPosEdit48.TabIndex = 77;
            this.dPosEdit48.Unit = "um";
            this.dPosEdit48.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit48.UnitWidth = 40;
            this.dPosEdit48.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit29
            // 
            this.dPosEdit29.AutoFocus = false;
            this.dPosEdit29.Caption = "Rotate Base Position";
            this.dPosEdit29.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit29.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit29.DataName = "Pos_HDT_B_U_Ready";
            this.dPosEdit29.DataSource = this.SetDS;
            this.dPosEdit29.DefaultValue = null;
            this.dPosEdit29.EditColor = System.Drawing.Color.Black;
            this.dPosEdit29.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit29.EditWidth = 100;
            this.dPosEdit29.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit29.IsModified = false;
            this.dPosEdit29.Location = new System.Drawing.Point(14, 515);
            this.dPosEdit29.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit29.MaxValue = 9999999D;
            this.dPosEdit29.MinValue = -9999999D;
            this.dPosEdit29.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit29.MotorP = this.MT_HDT_B_AxisU;
            this.dPosEdit29.Name = "dPosEdit29";
            this.dPosEdit29.NoChangeInAuto = false;
            this.dPosEdit29.PosValue = "";
            this.dPosEdit29.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit29.StepValue = 0D;
            this.dPosEdit29.TabIndex = 76;
            this.dPosEdit29.Unit = "um";
            this.dPosEdit29.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit29.UnitWidth = 40;
            this.dPosEdit29.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit13
            // 
            this.dPosEdit13.AutoFocus = false;
            this.dPosEdit13.Caption = "SortBox Place Z";
            this.dPosEdit13.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit13.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit13.DataName = "Pos_HDT_B_SortBox_Z";
            this.dPosEdit13.DataSource = this.SetDS;
            this.dPosEdit13.DefaultValue = null;
            this.dPosEdit13.EditColor = System.Drawing.Color.Black;
            this.dPosEdit13.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit13.EditWidth = 100;
            this.dPosEdit13.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit13.IsModified = false;
            this.dPosEdit13.Location = new System.Drawing.Point(14, 441);
            this.dPosEdit13.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit13.MaxValue = 9999999D;
            this.dPosEdit13.MinValue = -9999999D;
            this.dPosEdit13.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit13.MotorP = this.MT_HDT_B_AxisZ;
            this.dPosEdit13.Name = "dPosEdit13";
            this.dPosEdit13.NoChangeInAuto = false;
            this.dPosEdit13.PosValue = "";
            this.dPosEdit13.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit13.StepValue = 0D;
            this.dPosEdit13.TabIndex = 75;
            this.dPosEdit13.Unit = "um";
            this.dPosEdit13.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit13.UnitWidth = 40;
            this.dPosEdit13.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit16
            // 
            this.dPosEdit16.AutoFocus = false;
            this.dPosEdit16.Caption = "SortBox_Fail X";
            this.dPosEdit16.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit16.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit16.DataName = "Pos_HDT_B_SortBox_Fail_X";
            this.dPosEdit16.DataSource = this.SetDS;
            this.dPosEdit16.DefaultValue = null;
            this.dPosEdit16.EditColor = System.Drawing.Color.Black;
            this.dPosEdit16.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit16.EditWidth = 100;
            this.dPosEdit16.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit16.IsModified = false;
            this.dPosEdit16.Location = new System.Drawing.Point(14, 416);
            this.dPosEdit16.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit16.MaxValue = 9999999D;
            this.dPosEdit16.MinValue = -9999999D;
            this.dPosEdit16.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit16.MotorP = this.MT_HDT_B_AxisX;
            this.dPosEdit16.Name = "dPosEdit16";
            this.dPosEdit16.NoChangeInAuto = false;
            this.dPosEdit16.PosValue = "";
            this.dPosEdit16.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit16.StepValue = 0D;
            this.dPosEdit16.TabIndex = 74;
            this.dPosEdit16.Unit = "um";
            this.dPosEdit16.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit16.UnitWidth = 40;
            this.dPosEdit16.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit17
            // 
            this.dPosEdit17.AutoFocus = false;
            this.dPosEdit17.Caption = "SortBox_Pass X";
            this.dPosEdit17.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit17.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit17.DataName = "Pos_HDT_B_SortBox_Pass_X";
            this.dPosEdit17.DataSource = this.SetDS;
            this.dPosEdit17.DefaultValue = null;
            this.dPosEdit17.EditColor = System.Drawing.Color.Black;
            this.dPosEdit17.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit17.EditWidth = 100;
            this.dPosEdit17.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit17.IsModified = false;
            this.dPosEdit17.Location = new System.Drawing.Point(14, 391);
            this.dPosEdit17.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit17.MaxValue = 9999999D;
            this.dPosEdit17.MinValue = -9999999D;
            this.dPosEdit17.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit17.MotorP = this.MT_HDT_B_AxisX;
            this.dPosEdit17.Name = "dPosEdit17";
            this.dPosEdit17.NoChangeInAuto = false;
            this.dPosEdit17.PosValue = "";
            this.dPosEdit17.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit17.StepValue = 0D;
            this.dPosEdit17.TabIndex = 72;
            this.dPosEdit17.Unit = "um";
            this.dPosEdit17.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit17.UnitWidth = 40;
            this.dPosEdit17.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit18
            // 
            this.dPosEdit18.AutoFocus = false;
            this.dPosEdit18.Caption = "Nozzle and CCD Offset X";
            this.dPosEdit18.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit18.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit18.DataName = "Pos_HDT_B_Nozzle_CCD_Offset";
            this.dPosEdit18.DataSource = this.SetDS;
            this.dPosEdit18.DefaultValue = null;
            this.dPosEdit18.EditColor = System.Drawing.Color.Black;
            this.dPosEdit18.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit18.EditWidth = 100;
            this.dPosEdit18.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit18.IsModified = false;
            this.dPosEdit18.Location = new System.Drawing.Point(14, 71);
            this.dPosEdit18.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit18.MaxValue = 9999999D;
            this.dPosEdit18.MinValue = -9999999D;
            this.dPosEdit18.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit18.MotorP = this.MT_HDT_B_AxisX;
            this.dPosEdit18.Name = "dPosEdit18";
            this.dPosEdit18.NoChangeInAuto = false;
            this.dPosEdit18.PosValue = "";
            this.dPosEdit18.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit18.StepValue = 0D;
            this.dPosEdit18.TabIndex = 71;
            this.dPosEdit18.Unit = "um";
            this.dPosEdit18.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit18.UnitWidth = 40;
            this.dPosEdit18.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit19
            // 
            this.dPosEdit19.AutoFocus = false;
            this.dPosEdit19.Caption = "Rotate Resolution(360°)";
            this.dPosEdit19.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit19.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit19.DataName = "Pos_HDT_B_RotateResolution";
            this.dPosEdit19.DataSource = this.SetDS;
            this.dPosEdit19.DefaultValue = null;
            this.dPosEdit19.EditColor = System.Drawing.Color.Black;
            this.dPosEdit19.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit19.EditWidth = 100;
            this.dPosEdit19.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit19.IsModified = false;
            this.dPosEdit19.Location = new System.Drawing.Point(14, 490);
            this.dPosEdit19.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit19.MaxValue = 9999999D;
            this.dPosEdit19.MinValue = -9999999D;
            this.dPosEdit19.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit19.MotorP = this.MT_HDT_B_AxisU;
            this.dPosEdit19.Name = "dPosEdit19";
            this.dPosEdit19.NoChangeInAuto = false;
            this.dPosEdit19.PosValue = "";
            this.dPosEdit19.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit19.StepValue = 0D;
            this.dPosEdit19.TabIndex = 70;
            this.dPosEdit19.Unit = "pls";
            this.dPosEdit19.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit19.UnitWidth = 40;
            this.dPosEdit19.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit20
            // 
            this.dPosEdit20.AutoFocus = false;
            this.dPosEdit20.Caption = "Axis Z Safty Height";
            this.dPosEdit20.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit20.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit20.DataName = "Pos_HDT_B_Safe_Z";
            this.dPosEdit20.DataSource = this.SetDS;
            this.dPosEdit20.DefaultValue = null;
            this.dPosEdit20.EditColor = System.Drawing.Color.Black;
            this.dPosEdit20.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit20.EditWidth = 100;
            this.dPosEdit20.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit20.IsModified = false;
            this.dPosEdit20.Location = new System.Drawing.Point(14, 46);
            this.dPosEdit20.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit20.MaxValue = 9999999D;
            this.dPosEdit20.MinValue = -9999999D;
            this.dPosEdit20.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit20.MotorP = this.MT_HDT_B_AxisZ;
            this.dPosEdit20.Name = "dPosEdit20";
            this.dPosEdit20.NoChangeInAuto = false;
            this.dPosEdit20.PosValue = "";
            this.dPosEdit20.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit20.StepValue = 0D;
            this.dPosEdit20.TabIndex = 48;
            this.dPosEdit20.Unit = "um";
            this.dPosEdit20.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit20.UnitWidth = 40;
            this.dPosEdit20.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit21
            // 
            this.dPosEdit21.AutoFocus = false;
            this.dPosEdit21.Caption = "Transfer ShuttleB Z";
            this.dPosEdit21.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit21.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit21.DataName = "Pos_HDT_B_TransferShuttleB_Z";
            this.dPosEdit21.DataSource = this.SetDS;
            this.dPosEdit21.DefaultValue = null;
            this.dPosEdit21.EditColor = System.Drawing.Color.Black;
            this.dPosEdit21.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit21.EditWidth = 100;
            this.dPosEdit21.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit21.IsModified = false;
            this.dPosEdit21.Location = new System.Drawing.Point(14, 342);
            this.dPosEdit21.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit21.MaxValue = 9999999D;
            this.dPosEdit21.MinValue = -9999999D;
            this.dPosEdit21.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit21.MotorP = this.MT_HDT_B_AxisZ;
            this.dPosEdit21.Name = "dPosEdit21";
            this.dPosEdit21.NoChangeInAuto = false;
            this.dPosEdit21.PosValue = "";
            this.dPosEdit21.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit21.StepValue = 0D;
            this.dPosEdit21.TabIndex = 47;
            this.dPosEdit21.Unit = "um";
            this.dPosEdit21.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit21.UnitWidth = 40;
            this.dPosEdit21.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit22
            // 
            this.dPosEdit22.AutoFocus = false;
            this.dPosEdit22.Caption = "Transfer ShuttleB X";
            this.dPosEdit22.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit22.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit22.DataName = "Pos_HDT_B_TransferShuttleB_X";
            this.dPosEdit22.DataSource = this.SetDS;
            this.dPosEdit22.DefaultValue = null;
            this.dPosEdit22.EditColor = System.Drawing.Color.Black;
            this.dPosEdit22.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit22.EditWidth = 100;
            this.dPosEdit22.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit22.IsModified = false;
            this.dPosEdit22.Location = new System.Drawing.Point(14, 317);
            this.dPosEdit22.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit22.MaxValue = 9999999D;
            this.dPosEdit22.MinValue = -9999999D;
            this.dPosEdit22.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit22.MotorP = this.MT_HDT_B_AxisX;
            this.dPosEdit22.Name = "dPosEdit22";
            this.dPosEdit22.NoChangeInAuto = false;
            this.dPosEdit22.PosValue = "";
            this.dPosEdit22.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit22.StepValue = 0D;
            this.dPosEdit22.TabIndex = 46;
            this.dPosEdit22.Unit = "um";
            this.dPosEdit22.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit22.UnitWidth = 40;
            this.dPosEdit22.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit23
            // 
            this.dPosEdit23.AutoFocus = false;
            this.dPosEdit23.Caption = "Transfer ShuttleA Z";
            this.dPosEdit23.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit23.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit23.DataName = "Pos_HDT_B_TransferShuttleA_Z";
            this.dPosEdit23.DataSource = this.SetDS;
            this.dPosEdit23.DefaultValue = null;
            this.dPosEdit23.EditColor = System.Drawing.Color.Black;
            this.dPosEdit23.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit23.EditWidth = 100;
            this.dPosEdit23.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit23.IsModified = false;
            this.dPosEdit23.Location = new System.Drawing.Point(14, 292);
            this.dPosEdit23.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit23.MaxValue = 9999999D;
            this.dPosEdit23.MinValue = -9999999D;
            this.dPosEdit23.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit23.MotorP = this.MT_HDT_B_AxisZ;
            this.dPosEdit23.Name = "dPosEdit23";
            this.dPosEdit23.NoChangeInAuto = false;
            this.dPosEdit23.PosValue = "";
            this.dPosEdit23.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit23.StepValue = 0D;
            this.dPosEdit23.TabIndex = 45;
            this.dPosEdit23.Unit = "um";
            this.dPosEdit23.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit23.UnitWidth = 40;
            this.dPosEdit23.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit24
            // 
            this.dPosEdit24.AutoFocus = false;
            this.dPosEdit24.Caption = "Transfer ShuttleA X";
            this.dPosEdit24.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit24.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit24.DataName = "Pos_HDT_B_TransferShuttleA_X";
            this.dPosEdit24.DataSource = this.SetDS;
            this.dPosEdit24.DefaultValue = null;
            this.dPosEdit24.EditColor = System.Drawing.Color.Black;
            this.dPosEdit24.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit24.EditWidth = 100;
            this.dPosEdit24.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit24.IsModified = false;
            this.dPosEdit24.Location = new System.Drawing.Point(14, 267);
            this.dPosEdit24.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit24.MaxValue = 9999999D;
            this.dPosEdit24.MinValue = -9999999D;
            this.dPosEdit24.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit24.MotorP = this.MT_HDT_B_AxisX;
            this.dPosEdit24.Name = "dPosEdit24";
            this.dPosEdit24.NoChangeInAuto = false;
            this.dPosEdit24.PosValue = "";
            this.dPosEdit24.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit24.StepValue = 0D;
            this.dPosEdit24.TabIndex = 44;
            this.dPosEdit24.Unit = "um";
            this.dPosEdit24.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit24.UnitWidth = 40;
            this.dPosEdit24.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit25
            // 
            this.dPosEdit25.AutoFocus = false;
            this.dPosEdit25.Caption = "Board StageB Z";
            this.dPosEdit25.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit25.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit25.DataName = "Pos_HDT_B_BoardStageB_Z";
            this.dPosEdit25.DataSource = this.SetDS;
            this.dPosEdit25.DefaultValue = null;
            this.dPosEdit25.EditColor = System.Drawing.Color.Black;
            this.dPosEdit25.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit25.EditWidth = 100;
            this.dPosEdit25.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit25.IsModified = false;
            this.dPosEdit25.Location = new System.Drawing.Point(14, 219);
            this.dPosEdit25.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit25.MaxValue = 9999999D;
            this.dPosEdit25.MinValue = -9999999D;
            this.dPosEdit25.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit25.MotorP = this.MT_HDT_B_AxisZ;
            this.dPosEdit25.Name = "dPosEdit25";
            this.dPosEdit25.NoChangeInAuto = false;
            this.dPosEdit25.PosValue = "";
            this.dPosEdit25.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit25.StepValue = 0D;
            this.dPosEdit25.TabIndex = 43;
            this.dPosEdit25.Unit = "um";
            this.dPosEdit25.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit25.UnitWidth = 40;
            this.dPosEdit25.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit26
            // 
            this.dPosEdit26.AutoFocus = false;
            this.dPosEdit26.Caption = "Board StageB X";
            this.dPosEdit26.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit26.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit26.DataName = "Pos_HDT_B_BoardStageB_X";
            this.dPosEdit26.DataSource = this.SetDS;
            this.dPosEdit26.DefaultValue = null;
            this.dPosEdit26.EditColor = System.Drawing.Color.Black;
            this.dPosEdit26.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit26.EditWidth = 100;
            this.dPosEdit26.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit26.IsModified = false;
            this.dPosEdit26.Location = new System.Drawing.Point(14, 194);
            this.dPosEdit26.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit26.MaxValue = 9999999D;
            this.dPosEdit26.MinValue = -9999999D;
            this.dPosEdit26.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit26.MotorP = this.MT_HDT_B_AxisX;
            this.dPosEdit26.Name = "dPosEdit26";
            this.dPosEdit26.NoChangeInAuto = false;
            this.dPosEdit26.PosValue = "";
            this.dPosEdit26.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit26.StepValue = 0D;
            this.dPosEdit26.TabIndex = 42;
            this.dPosEdit26.Unit = "um";
            this.dPosEdit26.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit26.UnitWidth = 40;
            this.dPosEdit26.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit27
            // 
            this.dPosEdit27.AutoFocus = false;
            this.dPosEdit27.Caption = "Board StageA Z";
            this.dPosEdit27.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit27.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit27.DataName = "Pos_HDT_B_BoardStageA_Z";
            this.dPosEdit27.DataSource = this.SetDS;
            this.dPosEdit27.DefaultValue = null;
            this.dPosEdit27.EditColor = System.Drawing.Color.Black;
            this.dPosEdit27.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit27.EditWidth = 100;
            this.dPosEdit27.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit27.IsModified = false;
            this.dPosEdit27.Location = new System.Drawing.Point(14, 169);
            this.dPosEdit27.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit27.MaxValue = 9999999D;
            this.dPosEdit27.MinValue = -9999999D;
            this.dPosEdit27.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit27.MotorP = this.MT_HDT_B_AxisZ;
            this.dPosEdit27.Name = "dPosEdit27";
            this.dPosEdit27.NoChangeInAuto = false;
            this.dPosEdit27.PosValue = "";
            this.dPosEdit27.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit27.StepValue = 0D;
            this.dPosEdit27.TabIndex = 41;
            this.dPosEdit27.Unit = "um";
            this.dPosEdit27.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit27.UnitWidth = 40;
            this.dPosEdit27.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit28
            // 
            this.dPosEdit28.AutoFocus = false;
            this.dPosEdit28.Caption = "Board StageA X";
            this.dPosEdit28.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit28.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit28.DataName = "Pos_HDT_B_BoardStageA_X";
            this.dPosEdit28.DataSource = this.SetDS;
            this.dPosEdit28.DefaultValue = null;
            this.dPosEdit28.EditColor = System.Drawing.Color.Black;
            this.dPosEdit28.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit28.EditWidth = 100;
            this.dPosEdit28.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit28.IsModified = false;
            this.dPosEdit28.Location = new System.Drawing.Point(14, 144);
            this.dPosEdit28.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit28.MaxValue = 9999999D;
            this.dPosEdit28.MinValue = -9999999D;
            this.dPosEdit28.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit28.MotorP = this.MT_HDT_B_AxisX;
            this.dPosEdit28.Name = "dPosEdit28";
            this.dPosEdit28.NoChangeInAuto = false;
            this.dPosEdit28.PosValue = "";
            this.dPosEdit28.Size = new System.Drawing.Size(350, 25);
            this.dPosEdit28.StepValue = 0D;
            this.dPosEdit28.TabIndex = 40;
            this.dPosEdit28.Unit = "um";
            this.dPosEdit28.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit28.UnitWidth = 40;
            this.dPosEdit28.ValueType = KCSDK.ValueDataType.Int;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.dFieldEdit7);
            this.groupBox11.Controls.Add(this.dFieldEdit8);
            this.groupBox11.Controls.Add(this.dFieldEdit9);
            this.groupBox11.Controls.Add(this.dFieldEdit12);
            this.groupBox11.Controls.Add(this.dFieldEdit13);
            this.groupBox11.Controls.Add(this.dFieldEdit14);
            this.groupBox11.Controls.Add(this.dFieldEdit15);
            this.groupBox11.Controls.Add(this.dFieldEdit16);
            this.groupBox11.Controls.Add(this.dFieldEdit5);
            this.groupBox11.Controls.Add(this.dFieldEdit6);
            this.groupBox11.Controls.Add(this.dFieldEdit3);
            this.groupBox11.Controls.Add(this.dFieldEdit4);
            this.groupBox11.Controls.Add(this.dFieldEdit1);
            this.groupBox11.Controls.Add(this.dFieldEdit2);
            this.groupBox11.Controls.Add(this.dFieldEdit10);
            this.groupBox11.Controls.Add(this.dFieldEdit11);
            this.groupBox11.Location = new System.Drawing.Point(18, 74);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(1048, 294);
            this.groupBox11.TabIndex = 16;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Motor Speed and Acc/Dec";
            // 
            // dFieldEdit7
            // 
            this.dFieldEdit7.AutoFocus = false;
            this.dFieldEdit7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dFieldEdit7.Caption = "CCDB AxisZ Acc/Dec =";
            this.dFieldEdit7.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit7.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit7.DataName = "MT_HDT_CCDB_Z_ACC_MULTIPLE";
            this.dFieldEdit7.DataSource = this.SetDS;
            this.dFieldEdit7.DefaultValue = null;
            this.dFieldEdit7.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit7.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit7.EditWidth = 100;
            this.dFieldEdit7.FieldValue = "";
            this.dFieldEdit7.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit7.IsModified = false;
            this.dFieldEdit7.Location = new System.Drawing.Point(548, 243);
            this.dFieldEdit7.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit7.MaxValue = 20D;
            this.dFieldEdit7.MinValue = 1D;
            this.dFieldEdit7.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit7.Name = "dFieldEdit7";
            this.dFieldEdit7.NoChangeInAuto = false;
            this.dFieldEdit7.Size = new System.Drawing.Size(480, 29);
            this.dFieldEdit7.StepValue = 0D;
            this.dFieldEdit7.TabIndex = 15;
            this.dFieldEdit7.Unit = "times of speed";
            this.dFieldEdit7.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit7.UnitWidth = 150;
            this.dFieldEdit7.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit8
            // 
            this.dFieldEdit8.AutoFocus = false;
            this.dFieldEdit8.Caption = "CCDB AxisZ Speed =";
            this.dFieldEdit8.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit8.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit8.DataName = "MT_HDT_CCDB_Z_SPEED";
            this.dFieldEdit8.DataSource = this.SetDS;
            this.dFieldEdit8.DefaultValue = null;
            this.dFieldEdit8.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit8.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit8.EditWidth = 100;
            this.dFieldEdit8.FieldValue = "";
            this.dFieldEdit8.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit8.IsModified = false;
            this.dFieldEdit8.Location = new System.Drawing.Point(548, 214);
            this.dFieldEdit8.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit8.MaxValue = 2000000D;
            this.dFieldEdit8.MinValue = 10000D;
            this.dFieldEdit8.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit8.Name = "dFieldEdit8";
            this.dFieldEdit8.NoChangeInAuto = false;
            this.dFieldEdit8.Size = new System.Drawing.Size(389, 29);
            this.dFieldEdit8.StepValue = 0D;
            this.dFieldEdit8.TabIndex = 14;
            this.dFieldEdit8.Unit = "um/s";
            this.dFieldEdit8.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit8.UnitWidth = 60;
            this.dFieldEdit8.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit9
            // 
            this.dFieldEdit9.AutoFocus = false;
            this.dFieldEdit9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dFieldEdit9.Caption = "HeadB AxisZ Acc/Dec =";
            this.dFieldEdit9.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit9.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit9.DataName = "MT_HDT_HeadB_Z_ACC_MULTIPLE";
            this.dFieldEdit9.DataSource = this.SetDS;
            this.dFieldEdit9.DefaultValue = null;
            this.dFieldEdit9.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit9.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit9.EditWidth = 100;
            this.dFieldEdit9.FieldValue = "";
            this.dFieldEdit9.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit9.IsModified = false;
            this.dFieldEdit9.Location = new System.Drawing.Point(548, 185);
            this.dFieldEdit9.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit9.MaxValue = 20D;
            this.dFieldEdit9.MinValue = 1D;
            this.dFieldEdit9.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit9.Name = "dFieldEdit9";
            this.dFieldEdit9.NoChangeInAuto = false;
            this.dFieldEdit9.Size = new System.Drawing.Size(480, 29);
            this.dFieldEdit9.StepValue = 0D;
            this.dFieldEdit9.TabIndex = 13;
            this.dFieldEdit9.Unit = "times of speed";
            this.dFieldEdit9.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit9.UnitWidth = 150;
            this.dFieldEdit9.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit12
            // 
            this.dFieldEdit12.AutoFocus = false;
            this.dFieldEdit12.Caption = "HeadB AxisZ Speed =";
            this.dFieldEdit12.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit12.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit12.DataName = "MT_HDT_HeadB_Z_SPEED";
            this.dFieldEdit12.DataSource = this.SetDS;
            this.dFieldEdit12.DefaultValue = null;
            this.dFieldEdit12.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit12.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit12.EditWidth = 100;
            this.dFieldEdit12.FieldValue = "";
            this.dFieldEdit12.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit12.IsModified = false;
            this.dFieldEdit12.Location = new System.Drawing.Point(548, 156);
            this.dFieldEdit12.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit12.MaxValue = 2000000D;
            this.dFieldEdit12.MinValue = 10000D;
            this.dFieldEdit12.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit12.Name = "dFieldEdit12";
            this.dFieldEdit12.NoChangeInAuto = false;
            this.dFieldEdit12.Size = new System.Drawing.Size(389, 29);
            this.dFieldEdit12.StepValue = 0D;
            this.dFieldEdit12.TabIndex = 12;
            this.dFieldEdit12.Unit = "um/s";
            this.dFieldEdit12.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit12.UnitWidth = 60;
            this.dFieldEdit12.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit13
            // 
            this.dFieldEdit13.AutoFocus = false;
            this.dFieldEdit13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dFieldEdit13.Caption = "HeadB AxisU Acc/Dec =";
            this.dFieldEdit13.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit13.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit13.DataName = "MT_HDT_HeadB_U_ACC_MULTIPLE";
            this.dFieldEdit13.DataSource = this.SetDS;
            this.dFieldEdit13.DefaultValue = null;
            this.dFieldEdit13.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit13.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit13.EditWidth = 100;
            this.dFieldEdit13.FieldValue = "";
            this.dFieldEdit13.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit13.IsModified = false;
            this.dFieldEdit13.Location = new System.Drawing.Point(548, 127);
            this.dFieldEdit13.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit13.MaxValue = 20D;
            this.dFieldEdit13.MinValue = 1D;
            this.dFieldEdit13.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit13.Name = "dFieldEdit13";
            this.dFieldEdit13.NoChangeInAuto = false;
            this.dFieldEdit13.Size = new System.Drawing.Size(480, 29);
            this.dFieldEdit13.StepValue = 0D;
            this.dFieldEdit13.TabIndex = 11;
            this.dFieldEdit13.Unit = "times of speed";
            this.dFieldEdit13.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit13.UnitWidth = 150;
            this.dFieldEdit13.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit14
            // 
            this.dFieldEdit14.AutoFocus = false;
            this.dFieldEdit14.Caption = "HeadB AxisU Speed =";
            this.dFieldEdit14.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit14.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit14.DataName = "MT_HDT_HeadB_U_SPEED";
            this.dFieldEdit14.DataSource = this.SetDS;
            this.dFieldEdit14.DefaultValue = null;
            this.dFieldEdit14.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit14.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit14.EditWidth = 100;
            this.dFieldEdit14.FieldValue = "";
            this.dFieldEdit14.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit14.IsModified = false;
            this.dFieldEdit14.Location = new System.Drawing.Point(548, 98);
            this.dFieldEdit14.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit14.MaxValue = 2000000D;
            this.dFieldEdit14.MinValue = 10000D;
            this.dFieldEdit14.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit14.Name = "dFieldEdit14";
            this.dFieldEdit14.NoChangeInAuto = false;
            this.dFieldEdit14.Size = new System.Drawing.Size(389, 29);
            this.dFieldEdit14.StepValue = 0D;
            this.dFieldEdit14.TabIndex = 10;
            this.dFieldEdit14.Unit = "um/s";
            this.dFieldEdit14.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit14.UnitWidth = 60;
            this.dFieldEdit14.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit15
            // 
            this.dFieldEdit15.AutoFocus = false;
            this.dFieldEdit15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dFieldEdit15.Caption = "HeadB AxisX Acc/Dec =";
            this.dFieldEdit15.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit15.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit15.DataName = "MT_HDT_HeadB_X_ACC_MULTIPLE";
            this.dFieldEdit15.DataSource = this.SetDS;
            this.dFieldEdit15.DefaultValue = null;
            this.dFieldEdit15.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit15.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit15.EditWidth = 100;
            this.dFieldEdit15.FieldValue = "";
            this.dFieldEdit15.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit15.IsModified = false;
            this.dFieldEdit15.Location = new System.Drawing.Point(548, 69);
            this.dFieldEdit15.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit15.MaxValue = 20D;
            this.dFieldEdit15.MinValue = 1D;
            this.dFieldEdit15.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit15.Name = "dFieldEdit15";
            this.dFieldEdit15.NoChangeInAuto = false;
            this.dFieldEdit15.Size = new System.Drawing.Size(480, 29);
            this.dFieldEdit15.StepValue = 0D;
            this.dFieldEdit15.TabIndex = 9;
            this.dFieldEdit15.Unit = "times of speed";
            this.dFieldEdit15.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit15.UnitWidth = 150;
            this.dFieldEdit15.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit16
            // 
            this.dFieldEdit16.AutoFocus = false;
            this.dFieldEdit16.Caption = "HeadB AxisX Speed =";
            this.dFieldEdit16.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit16.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit16.DataName = "MT_HDT_HeadB_X_SPEED";
            this.dFieldEdit16.DataSource = this.SetDS;
            this.dFieldEdit16.DefaultValue = null;
            this.dFieldEdit16.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit16.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit16.EditWidth = 100;
            this.dFieldEdit16.FieldValue = "";
            this.dFieldEdit16.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit16.IsModified = false;
            this.dFieldEdit16.Location = new System.Drawing.Point(548, 40);
            this.dFieldEdit16.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit16.MaxValue = 2000000D;
            this.dFieldEdit16.MinValue = 10000D;
            this.dFieldEdit16.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit16.Name = "dFieldEdit16";
            this.dFieldEdit16.NoChangeInAuto = false;
            this.dFieldEdit16.Size = new System.Drawing.Size(389, 29);
            this.dFieldEdit16.StepValue = 0D;
            this.dFieldEdit16.TabIndex = 8;
            this.dFieldEdit16.Unit = "um/s";
            this.dFieldEdit16.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit16.UnitWidth = 60;
            this.dFieldEdit16.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit5
            // 
            this.dFieldEdit5.AutoFocus = false;
            this.dFieldEdit5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dFieldEdit5.Caption = "CCDA AxisZ Acc/Dec =";
            this.dFieldEdit5.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit5.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit5.DataName = "MT_HDT_CCDA_Z_ACC_MULTIPLE";
            this.dFieldEdit5.DataSource = this.SetDS;
            this.dFieldEdit5.DefaultValue = null;
            this.dFieldEdit5.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit5.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit5.EditWidth = 100;
            this.dFieldEdit5.FieldValue = "";
            this.dFieldEdit5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit5.IsModified = false;
            this.dFieldEdit5.Location = new System.Drawing.Point(22, 243);
            this.dFieldEdit5.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit5.MaxValue = 20D;
            this.dFieldEdit5.MinValue = 1D;
            this.dFieldEdit5.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit5.Name = "dFieldEdit5";
            this.dFieldEdit5.NoChangeInAuto = false;
            this.dFieldEdit5.Size = new System.Drawing.Size(480, 29);
            this.dFieldEdit5.StepValue = 0D;
            this.dFieldEdit5.TabIndex = 7;
            this.dFieldEdit5.Unit = "times of speed";
            this.dFieldEdit5.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit5.UnitWidth = 150;
            this.dFieldEdit5.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit6
            // 
            this.dFieldEdit6.AutoFocus = false;
            this.dFieldEdit6.Caption = "CCDA AxisZ Speed =";
            this.dFieldEdit6.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit6.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit6.DataName = "MT_HDT_CCDA_Z_SPEED";
            this.dFieldEdit6.DataSource = this.SetDS;
            this.dFieldEdit6.DefaultValue = null;
            this.dFieldEdit6.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit6.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit6.EditWidth = 100;
            this.dFieldEdit6.FieldValue = "";
            this.dFieldEdit6.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit6.IsModified = false;
            this.dFieldEdit6.Location = new System.Drawing.Point(22, 214);
            this.dFieldEdit6.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit6.MaxValue = 2000000D;
            this.dFieldEdit6.MinValue = 10000D;
            this.dFieldEdit6.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit6.Name = "dFieldEdit6";
            this.dFieldEdit6.NoChangeInAuto = false;
            this.dFieldEdit6.Size = new System.Drawing.Size(389, 29);
            this.dFieldEdit6.StepValue = 0D;
            this.dFieldEdit6.TabIndex = 6;
            this.dFieldEdit6.Unit = "um/s";
            this.dFieldEdit6.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit6.UnitWidth = 60;
            this.dFieldEdit6.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit3
            // 
            this.dFieldEdit3.AutoFocus = false;
            this.dFieldEdit3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dFieldEdit3.Caption = "HeadA AxisZ Acc/Dec =";
            this.dFieldEdit3.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit3.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit3.DataName = "MT_HDT_HeadA_Z_ACC_MULTIPLE";
            this.dFieldEdit3.DataSource = this.SetDS;
            this.dFieldEdit3.DefaultValue = null;
            this.dFieldEdit3.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit3.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit3.EditWidth = 100;
            this.dFieldEdit3.FieldValue = "";
            this.dFieldEdit3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit3.IsModified = false;
            this.dFieldEdit3.Location = new System.Drawing.Point(22, 185);
            this.dFieldEdit3.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit3.MaxValue = 20D;
            this.dFieldEdit3.MinValue = 1D;
            this.dFieldEdit3.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit3.Name = "dFieldEdit3";
            this.dFieldEdit3.NoChangeInAuto = false;
            this.dFieldEdit3.Size = new System.Drawing.Size(480, 29);
            this.dFieldEdit3.StepValue = 0D;
            this.dFieldEdit3.TabIndex = 5;
            this.dFieldEdit3.Unit = "times of speed";
            this.dFieldEdit3.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit3.UnitWidth = 150;
            this.dFieldEdit3.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit4
            // 
            this.dFieldEdit4.AutoFocus = false;
            this.dFieldEdit4.Caption = "HeadA AxisZ Speed =";
            this.dFieldEdit4.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit4.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit4.DataName = "MT_HDT_HeadA_Z_SPEED";
            this.dFieldEdit4.DataSource = this.SetDS;
            this.dFieldEdit4.DefaultValue = null;
            this.dFieldEdit4.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit4.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit4.EditWidth = 100;
            this.dFieldEdit4.FieldValue = "";
            this.dFieldEdit4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit4.IsModified = false;
            this.dFieldEdit4.Location = new System.Drawing.Point(22, 156);
            this.dFieldEdit4.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit4.MaxValue = 2000000D;
            this.dFieldEdit4.MinValue = 10000D;
            this.dFieldEdit4.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit4.Name = "dFieldEdit4";
            this.dFieldEdit4.NoChangeInAuto = false;
            this.dFieldEdit4.Size = new System.Drawing.Size(389, 29);
            this.dFieldEdit4.StepValue = 0D;
            this.dFieldEdit4.TabIndex = 4;
            this.dFieldEdit4.Unit = "um/s";
            this.dFieldEdit4.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit4.UnitWidth = 60;
            this.dFieldEdit4.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit1
            // 
            this.dFieldEdit1.AutoFocus = false;
            this.dFieldEdit1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dFieldEdit1.Caption = "HeadA AxisU Acc/Dec =";
            this.dFieldEdit1.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit1.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit1.DataName = "MT_HDT_HeadA_U_ACC_MULTIPLE";
            this.dFieldEdit1.DataSource = this.SetDS;
            this.dFieldEdit1.DefaultValue = null;
            this.dFieldEdit1.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit1.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit1.EditWidth = 100;
            this.dFieldEdit1.FieldValue = "";
            this.dFieldEdit1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit1.IsModified = false;
            this.dFieldEdit1.Location = new System.Drawing.Point(22, 127);
            this.dFieldEdit1.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit1.MaxValue = 20D;
            this.dFieldEdit1.MinValue = 1D;
            this.dFieldEdit1.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit1.Name = "dFieldEdit1";
            this.dFieldEdit1.NoChangeInAuto = false;
            this.dFieldEdit1.Size = new System.Drawing.Size(480, 29);
            this.dFieldEdit1.StepValue = 0D;
            this.dFieldEdit1.TabIndex = 3;
            this.dFieldEdit1.Unit = "times of speed";
            this.dFieldEdit1.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit1.UnitWidth = 150;
            this.dFieldEdit1.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit2
            // 
            this.dFieldEdit2.AutoFocus = false;
            this.dFieldEdit2.Caption = "HeadA AxisU Speed =";
            this.dFieldEdit2.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit2.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit2.DataName = "MT_HDT_HeadA_U_SPEED";
            this.dFieldEdit2.DataSource = this.SetDS;
            this.dFieldEdit2.DefaultValue = null;
            this.dFieldEdit2.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit2.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit2.EditWidth = 100;
            this.dFieldEdit2.FieldValue = "";
            this.dFieldEdit2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit2.IsModified = false;
            this.dFieldEdit2.Location = new System.Drawing.Point(22, 98);
            this.dFieldEdit2.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit2.MaxValue = 2000000D;
            this.dFieldEdit2.MinValue = 10000D;
            this.dFieldEdit2.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit2.Name = "dFieldEdit2";
            this.dFieldEdit2.NoChangeInAuto = false;
            this.dFieldEdit2.Size = new System.Drawing.Size(389, 29);
            this.dFieldEdit2.StepValue = 0D;
            this.dFieldEdit2.TabIndex = 2;
            this.dFieldEdit2.Unit = "um/s";
            this.dFieldEdit2.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit2.UnitWidth = 60;
            this.dFieldEdit2.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit10
            // 
            this.dFieldEdit10.AutoFocus = false;
            this.dFieldEdit10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dFieldEdit10.Caption = "HeadA AxisX Acc/Dec =";
            this.dFieldEdit10.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit10.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit10.DataName = "MT_HDT_HeadA_X_ACC_MULTIPLE";
            this.dFieldEdit10.DataSource = this.SetDS;
            this.dFieldEdit10.DefaultValue = null;
            this.dFieldEdit10.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit10.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit10.EditWidth = 100;
            this.dFieldEdit10.FieldValue = "";
            this.dFieldEdit10.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit10.IsModified = false;
            this.dFieldEdit10.Location = new System.Drawing.Point(22, 69);
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
            this.dFieldEdit11.Caption = "HeadA AxisX Speed =";
            this.dFieldEdit11.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit11.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit11.DataName = "MT_HDT_HeadA_X_SPEED";
            this.dFieldEdit11.DataSource = this.SetDS;
            this.dFieldEdit11.DefaultValue = null;
            this.dFieldEdit11.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit11.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit11.EditWidth = 100;
            this.dFieldEdit11.FieldValue = "";
            this.dFieldEdit11.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit11.IsModified = false;
            this.dFieldEdit11.Location = new System.Drawing.Point(22, 40);
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
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage1);
            this.tabControl3.Controls.Add(this.tabPage2);
            this.tabControl3.Location = new System.Drawing.Point(6, 60);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(1066, 611);
            this.tabControl3.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 35);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1058, 572);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Position";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 35);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1058, 572);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Bound";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dPosEdit31);
            this.groupBox4.Controls.Add(this.dPosEdit32);
            this.groupBox4.Controls.Add(this.dPosEdit33);
            this.groupBox4.Controls.Add(this.dPosEdit34);
            this.groupBox4.Controls.Add(this.dPosEdit35);
            this.groupBox4.Controls.Add(this.dPosEdit36);
            this.groupBox4.Controls.Add(this.dPosEdit43);
            this.groupBox4.Controls.Add(this.dPosEdit44);
            this.groupBox4.Location = new System.Drawing.Point(500, 25);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(418, 392);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Board HeadB";
            // 
            // dPosEdit31
            // 
            this.dPosEdit31.AutoFocus = false;
            this.dPosEdit31.Caption = "Transfer ShuttleB Right Bound";
            this.dPosEdit31.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit31.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit31.DataName = "Pos_HDT_B_TransferShuttleB_RightBound";
            this.dPosEdit31.DataSource = this.SetDS;
            this.dPosEdit31.DefaultValue = null;
            this.dPosEdit31.EditColor = System.Drawing.Color.Black;
            this.dPosEdit31.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit31.EditWidth = 100;
            this.dPosEdit31.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit31.IsModified = false;
            this.dPosEdit31.Location = new System.Drawing.Point(14, 295);
            this.dPosEdit31.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit31.MaxValue = 9999999D;
            this.dPosEdit31.MinValue = -9999999D;
            this.dPosEdit31.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit31.MotorP = this.MT_HDT_B_AxisX;
            this.dPosEdit31.Name = "dPosEdit31";
            this.dPosEdit31.NoChangeInAuto = false;
            this.dPosEdit31.PosValue = "";
            this.dPosEdit31.Size = new System.Drawing.Size(386, 25);
            this.dPosEdit31.StepValue = 0D;
            this.dPosEdit31.TabIndex = 47;
            this.dPosEdit31.Unit = "um";
            this.dPosEdit31.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit31.UnitWidth = 40;
            this.dPosEdit31.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit32
            // 
            this.dPosEdit32.AutoFocus = false;
            this.dPosEdit32.Caption = "Transfer ShuttleB Left Bound";
            this.dPosEdit32.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit32.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit32.DataName = "Pos_HDT_B_TransferShuttleB_LeftBound";
            this.dPosEdit32.DataSource = this.SetDS;
            this.dPosEdit32.DefaultValue = null;
            this.dPosEdit32.EditColor = System.Drawing.Color.Black;
            this.dPosEdit32.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit32.EditWidth = 100;
            this.dPosEdit32.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit32.IsModified = false;
            this.dPosEdit32.Location = new System.Drawing.Point(14, 270);
            this.dPosEdit32.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit32.MaxValue = 9999999D;
            this.dPosEdit32.MinValue = -9999999D;
            this.dPosEdit32.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit32.MotorP = this.MT_HDT_B_AxisX;
            this.dPosEdit32.Name = "dPosEdit32";
            this.dPosEdit32.NoChangeInAuto = false;
            this.dPosEdit32.PosValue = "";
            this.dPosEdit32.Size = new System.Drawing.Size(386, 25);
            this.dPosEdit32.StepValue = 0D;
            this.dPosEdit32.TabIndex = 46;
            this.dPosEdit32.Unit = "um";
            this.dPosEdit32.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit32.UnitWidth = 40;
            this.dPosEdit32.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit33
            // 
            this.dPosEdit33.AutoFocus = false;
            this.dPosEdit33.Caption = "Transfer ShuttleA Right Bound";
            this.dPosEdit33.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit33.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit33.DataName = "Pos_HDT_B_TransferShuttleA_RightBound";
            this.dPosEdit33.DataSource = this.SetDS;
            this.dPosEdit33.DefaultValue = null;
            this.dPosEdit33.EditColor = System.Drawing.Color.Black;
            this.dPosEdit33.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit33.EditWidth = 100;
            this.dPosEdit33.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit33.IsModified = false;
            this.dPosEdit33.Location = new System.Drawing.Point(14, 221);
            this.dPosEdit33.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit33.MaxValue = 9999999D;
            this.dPosEdit33.MinValue = -9999999D;
            this.dPosEdit33.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit33.MotorP = this.MT_HDT_B_AxisX;
            this.dPosEdit33.Name = "dPosEdit33";
            this.dPosEdit33.NoChangeInAuto = false;
            this.dPosEdit33.PosValue = "";
            this.dPosEdit33.Size = new System.Drawing.Size(386, 25);
            this.dPosEdit33.StepValue = 0D;
            this.dPosEdit33.TabIndex = 45;
            this.dPosEdit33.Unit = "um";
            this.dPosEdit33.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit33.UnitWidth = 40;
            this.dPosEdit33.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit34
            // 
            this.dPosEdit34.AutoFocus = false;
            this.dPosEdit34.Caption = "Transfer ShuttleA Left Bound";
            this.dPosEdit34.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit34.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit34.DataName = "Pos_HDT_B_TransferShuttleA_LeftBound";
            this.dPosEdit34.DataSource = this.SetDS;
            this.dPosEdit34.DefaultValue = null;
            this.dPosEdit34.EditColor = System.Drawing.Color.Black;
            this.dPosEdit34.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit34.EditWidth = 100;
            this.dPosEdit34.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit34.IsModified = false;
            this.dPosEdit34.Location = new System.Drawing.Point(14, 196);
            this.dPosEdit34.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit34.MaxValue = 9999999D;
            this.dPosEdit34.MinValue = -9999999D;
            this.dPosEdit34.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit34.MotorP = this.MT_HDT_B_AxisX;
            this.dPosEdit34.Name = "dPosEdit34";
            this.dPosEdit34.NoChangeInAuto = false;
            this.dPosEdit34.PosValue = "";
            this.dPosEdit34.Size = new System.Drawing.Size(386, 25);
            this.dPosEdit34.StepValue = 0D;
            this.dPosEdit34.TabIndex = 44;
            this.dPosEdit34.Unit = "um";
            this.dPosEdit34.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit34.UnitWidth = 40;
            this.dPosEdit34.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit35
            // 
            this.dPosEdit35.AutoFocus = false;
            this.dPosEdit35.Caption = "Board StageB Right Bound";
            this.dPosEdit35.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit35.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit35.DataName = "Pos_HDT_B_BoardStageB_RightBound";
            this.dPosEdit35.DataSource = this.SetDS;
            this.dPosEdit35.DefaultValue = null;
            this.dPosEdit35.EditColor = System.Drawing.Color.Black;
            this.dPosEdit35.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit35.EditWidth = 100;
            this.dPosEdit35.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit35.IsModified = false;
            this.dPosEdit35.Location = new System.Drawing.Point(14, 148);
            this.dPosEdit35.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit35.MaxValue = 9999999D;
            this.dPosEdit35.MinValue = -9999999D;
            this.dPosEdit35.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit35.MotorP = this.MT_HDT_B_AxisX;
            this.dPosEdit35.Name = "dPosEdit35";
            this.dPosEdit35.NoChangeInAuto = false;
            this.dPosEdit35.PosValue = "";
            this.dPosEdit35.Size = new System.Drawing.Size(386, 25);
            this.dPosEdit35.StepValue = 0D;
            this.dPosEdit35.TabIndex = 43;
            this.dPosEdit35.Unit = "um";
            this.dPosEdit35.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit35.UnitWidth = 40;
            this.dPosEdit35.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit36
            // 
            this.dPosEdit36.AutoFocus = false;
            this.dPosEdit36.Caption = "Board StageB Left Bound";
            this.dPosEdit36.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit36.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit36.DataName = "Pos_HDT_B_BoardStageB_LeftBound";
            this.dPosEdit36.DataSource = this.SetDS;
            this.dPosEdit36.DefaultValue = null;
            this.dPosEdit36.EditColor = System.Drawing.Color.Black;
            this.dPosEdit36.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit36.EditWidth = 100;
            this.dPosEdit36.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit36.IsModified = false;
            this.dPosEdit36.Location = new System.Drawing.Point(14, 123);
            this.dPosEdit36.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit36.MaxValue = 9999999D;
            this.dPosEdit36.MinValue = -9999999D;
            this.dPosEdit36.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit36.MotorP = this.MT_HDT_B_AxisX;
            this.dPosEdit36.Name = "dPosEdit36";
            this.dPosEdit36.NoChangeInAuto = false;
            this.dPosEdit36.PosValue = "";
            this.dPosEdit36.Size = new System.Drawing.Size(386, 25);
            this.dPosEdit36.StepValue = 0D;
            this.dPosEdit36.TabIndex = 42;
            this.dPosEdit36.Unit = "um";
            this.dPosEdit36.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit36.UnitWidth = 40;
            this.dPosEdit36.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit43
            // 
            this.dPosEdit43.AutoFocus = false;
            this.dPosEdit43.Caption = "Board StageA Right Bound";
            this.dPosEdit43.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit43.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit43.DataName = "Pos_HDT_B_BoardStageA_RightBound";
            this.dPosEdit43.DataSource = this.SetDS;
            this.dPosEdit43.DefaultValue = null;
            this.dPosEdit43.EditColor = System.Drawing.Color.Black;
            this.dPosEdit43.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit43.EditWidth = 100;
            this.dPosEdit43.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit43.IsModified = false;
            this.dPosEdit43.Location = new System.Drawing.Point(14, 76);
            this.dPosEdit43.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit43.MaxValue = 9999999D;
            this.dPosEdit43.MinValue = -9999999D;
            this.dPosEdit43.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit43.MotorP = this.MT_HDT_B_AxisX;
            this.dPosEdit43.Name = "dPosEdit43";
            this.dPosEdit43.NoChangeInAuto = false;
            this.dPosEdit43.PosValue = "";
            this.dPosEdit43.Size = new System.Drawing.Size(386, 25);
            this.dPosEdit43.StepValue = 0D;
            this.dPosEdit43.TabIndex = 41;
            this.dPosEdit43.Unit = "um";
            this.dPosEdit43.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit43.UnitWidth = 40;
            this.dPosEdit43.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit44
            // 
            this.dPosEdit44.AutoFocus = false;
            this.dPosEdit44.Caption = "Board StageA Left Bound";
            this.dPosEdit44.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit44.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit44.DataName = "Pos_HDT_B_BoardStageA_LeftBound";
            this.dPosEdit44.DataSource = this.SetDS;
            this.dPosEdit44.DefaultValue = null;
            this.dPosEdit44.EditColor = System.Drawing.Color.Black;
            this.dPosEdit44.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit44.EditWidth = 100;
            this.dPosEdit44.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit44.IsModified = false;
            this.dPosEdit44.Location = new System.Drawing.Point(14, 51);
            this.dPosEdit44.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit44.MaxValue = 9999999D;
            this.dPosEdit44.MinValue = -9999999D;
            this.dPosEdit44.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit44.MotorP = this.MT_HDT_B_AxisX;
            this.dPosEdit44.Name = "dPosEdit44";
            this.dPosEdit44.NoChangeInAuto = false;
            this.dPosEdit44.PosValue = "";
            this.dPosEdit44.Size = new System.Drawing.Size(386, 25);
            this.dPosEdit44.StepValue = 0D;
            this.dPosEdit44.TabIndex = 40;
            this.dPosEdit44.Unit = "um";
            this.dPosEdit44.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit44.UnitWidth = 40;
            this.dPosEdit44.ValueType = KCSDK.ValueDataType.Int;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dPosEdit37);
            this.groupBox2.Controls.Add(this.dPosEdit38);
            this.groupBox2.Controls.Add(this.dPosEdit39);
            this.groupBox2.Controls.Add(this.dPosEdit40);
            this.groupBox2.Controls.Add(this.dPosEdit41);
            this.groupBox2.Controls.Add(this.dPosEdit42);
            this.groupBox2.Controls.Add(this.dPosEdit45);
            this.groupBox2.Controls.Add(this.dPosEdit46);
            this.groupBox2.Location = new System.Drawing.Point(32, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(418, 392);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Board HeadA";
            // 
            // dPosEdit37
            // 
            this.dPosEdit37.AutoFocus = false;
            this.dPosEdit37.Caption = "Transfer ShuttleB Right Bound";
            this.dPosEdit37.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit37.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit37.DataName = "Pos_HDT_A_TransferShuttleB_RightBound";
            this.dPosEdit37.DataSource = this.SetDS;
            this.dPosEdit37.DefaultValue = null;
            this.dPosEdit37.EditColor = System.Drawing.Color.Black;
            this.dPosEdit37.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit37.EditWidth = 100;
            this.dPosEdit37.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit37.IsModified = false;
            this.dPosEdit37.Location = new System.Drawing.Point(14, 295);
            this.dPosEdit37.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit37.MaxValue = 9999999D;
            this.dPosEdit37.MinValue = -9999999D;
            this.dPosEdit37.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit37.MotorP = this.MT_HDT_A_AxisX;
            this.dPosEdit37.Name = "dPosEdit37";
            this.dPosEdit37.NoChangeInAuto = false;
            this.dPosEdit37.PosValue = "";
            this.dPosEdit37.Size = new System.Drawing.Size(386, 25);
            this.dPosEdit37.StepValue = 0D;
            this.dPosEdit37.TabIndex = 47;
            this.dPosEdit37.Unit = "um";
            this.dPosEdit37.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit37.UnitWidth = 40;
            this.dPosEdit37.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit38
            // 
            this.dPosEdit38.AutoFocus = false;
            this.dPosEdit38.Caption = "Transfer ShuttleB Left Bound";
            this.dPosEdit38.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit38.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit38.DataName = "Pos_HDT_A_TransferShuttleB_LeftBound";
            this.dPosEdit38.DataSource = this.SetDS;
            this.dPosEdit38.DefaultValue = null;
            this.dPosEdit38.EditColor = System.Drawing.Color.Black;
            this.dPosEdit38.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit38.EditWidth = 100;
            this.dPosEdit38.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit38.IsModified = false;
            this.dPosEdit38.Location = new System.Drawing.Point(14, 270);
            this.dPosEdit38.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit38.MaxValue = 9999999D;
            this.dPosEdit38.MinValue = -9999999D;
            this.dPosEdit38.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit38.MotorP = this.MT_HDT_A_AxisX;
            this.dPosEdit38.Name = "dPosEdit38";
            this.dPosEdit38.NoChangeInAuto = false;
            this.dPosEdit38.PosValue = "";
            this.dPosEdit38.Size = new System.Drawing.Size(386, 25);
            this.dPosEdit38.StepValue = 0D;
            this.dPosEdit38.TabIndex = 46;
            this.dPosEdit38.Unit = "um";
            this.dPosEdit38.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit38.UnitWidth = 40;
            this.dPosEdit38.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit39
            // 
            this.dPosEdit39.AutoFocus = false;
            this.dPosEdit39.Caption = "Transfer ShuttleA Right Bound";
            this.dPosEdit39.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit39.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit39.DataName = "Pos_HDT_A_TransferShuttleA_RightBound";
            this.dPosEdit39.DataSource = this.SetDS;
            this.dPosEdit39.DefaultValue = null;
            this.dPosEdit39.EditColor = System.Drawing.Color.Black;
            this.dPosEdit39.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit39.EditWidth = 100;
            this.dPosEdit39.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit39.IsModified = false;
            this.dPosEdit39.Location = new System.Drawing.Point(14, 221);
            this.dPosEdit39.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit39.MaxValue = 9999999D;
            this.dPosEdit39.MinValue = -9999999D;
            this.dPosEdit39.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit39.MotorP = this.MT_HDT_A_AxisX;
            this.dPosEdit39.Name = "dPosEdit39";
            this.dPosEdit39.NoChangeInAuto = false;
            this.dPosEdit39.PosValue = "";
            this.dPosEdit39.Size = new System.Drawing.Size(386, 25);
            this.dPosEdit39.StepValue = 0D;
            this.dPosEdit39.TabIndex = 45;
            this.dPosEdit39.Unit = "um";
            this.dPosEdit39.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit39.UnitWidth = 40;
            this.dPosEdit39.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit40
            // 
            this.dPosEdit40.AutoFocus = false;
            this.dPosEdit40.Caption = "Transfer ShuttleA Left Bound";
            this.dPosEdit40.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit40.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit40.DataName = "Pos_HDT_A_TransferShuttleA_LeftBound";
            this.dPosEdit40.DataSource = this.SetDS;
            this.dPosEdit40.DefaultValue = null;
            this.dPosEdit40.EditColor = System.Drawing.Color.Black;
            this.dPosEdit40.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit40.EditWidth = 100;
            this.dPosEdit40.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit40.IsModified = false;
            this.dPosEdit40.Location = new System.Drawing.Point(14, 196);
            this.dPosEdit40.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit40.MaxValue = 9999999D;
            this.dPosEdit40.MinValue = -9999999D;
            this.dPosEdit40.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit40.MotorP = this.MT_HDT_A_AxisX;
            this.dPosEdit40.Name = "dPosEdit40";
            this.dPosEdit40.NoChangeInAuto = false;
            this.dPosEdit40.PosValue = "";
            this.dPosEdit40.Size = new System.Drawing.Size(386, 25);
            this.dPosEdit40.StepValue = 0D;
            this.dPosEdit40.TabIndex = 44;
            this.dPosEdit40.Unit = "um";
            this.dPosEdit40.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit40.UnitWidth = 40;
            this.dPosEdit40.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit41
            // 
            this.dPosEdit41.AutoFocus = false;
            this.dPosEdit41.Caption = "Board StageB Right Bound";
            this.dPosEdit41.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit41.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit41.DataName = "Pos_HDT_A_BoardStageB_RightBound";
            this.dPosEdit41.DataSource = this.SetDS;
            this.dPosEdit41.DefaultValue = null;
            this.dPosEdit41.EditColor = System.Drawing.Color.Black;
            this.dPosEdit41.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit41.EditWidth = 100;
            this.dPosEdit41.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit41.IsModified = false;
            this.dPosEdit41.Location = new System.Drawing.Point(14, 148);
            this.dPosEdit41.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit41.MaxValue = 9999999D;
            this.dPosEdit41.MinValue = -9999999D;
            this.dPosEdit41.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit41.MotorP = this.MT_HDT_A_AxisX;
            this.dPosEdit41.Name = "dPosEdit41";
            this.dPosEdit41.NoChangeInAuto = false;
            this.dPosEdit41.PosValue = "";
            this.dPosEdit41.Size = new System.Drawing.Size(386, 25);
            this.dPosEdit41.StepValue = 0D;
            this.dPosEdit41.TabIndex = 43;
            this.dPosEdit41.Unit = "um";
            this.dPosEdit41.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit41.UnitWidth = 40;
            this.dPosEdit41.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit42
            // 
            this.dPosEdit42.AutoFocus = false;
            this.dPosEdit42.Caption = "Board StageB Left Bound";
            this.dPosEdit42.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit42.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit42.DataName = "Pos_HDT_A_BoardStageB_LeftBound";
            this.dPosEdit42.DataSource = this.SetDS;
            this.dPosEdit42.DefaultValue = null;
            this.dPosEdit42.EditColor = System.Drawing.Color.Black;
            this.dPosEdit42.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit42.EditWidth = 100;
            this.dPosEdit42.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit42.IsModified = false;
            this.dPosEdit42.Location = new System.Drawing.Point(14, 123);
            this.dPosEdit42.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit42.MaxValue = 9999999D;
            this.dPosEdit42.MinValue = -9999999D;
            this.dPosEdit42.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit42.MotorP = this.MT_HDT_A_AxisX;
            this.dPosEdit42.Name = "dPosEdit42";
            this.dPosEdit42.NoChangeInAuto = false;
            this.dPosEdit42.PosValue = "";
            this.dPosEdit42.Size = new System.Drawing.Size(386, 25);
            this.dPosEdit42.StepValue = 0D;
            this.dPosEdit42.TabIndex = 42;
            this.dPosEdit42.Unit = "um";
            this.dPosEdit42.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit42.UnitWidth = 40;
            this.dPosEdit42.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit45
            // 
            this.dPosEdit45.AutoFocus = false;
            this.dPosEdit45.Caption = "Board StageA Right Bound";
            this.dPosEdit45.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit45.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit45.DataName = "Pos_HDT_A_BoardStageA_RightBound";
            this.dPosEdit45.DataSource = this.SetDS;
            this.dPosEdit45.DefaultValue = null;
            this.dPosEdit45.EditColor = System.Drawing.Color.Black;
            this.dPosEdit45.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit45.EditWidth = 100;
            this.dPosEdit45.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit45.IsModified = false;
            this.dPosEdit45.Location = new System.Drawing.Point(14, 76);
            this.dPosEdit45.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit45.MaxValue = 9999999D;
            this.dPosEdit45.MinValue = -9999999D;
            this.dPosEdit45.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit45.MotorP = this.MT_HDT_A_AxisX;
            this.dPosEdit45.Name = "dPosEdit45";
            this.dPosEdit45.NoChangeInAuto = false;
            this.dPosEdit45.PosValue = "";
            this.dPosEdit45.Size = new System.Drawing.Size(386, 25);
            this.dPosEdit45.StepValue = 0D;
            this.dPosEdit45.TabIndex = 41;
            this.dPosEdit45.Unit = "um";
            this.dPosEdit45.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit45.UnitWidth = 40;
            this.dPosEdit45.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dPosEdit46
            // 
            this.dPosEdit46.AutoFocus = false;
            this.dPosEdit46.Caption = "Board StageA Left Bound";
            this.dPosEdit46.CaptionColor = System.Drawing.Color.Black;
            this.dPosEdit46.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit46.DataName = "Pos_HDT_A_BoardStageA_LeftBound";
            this.dPosEdit46.DataSource = this.SetDS;
            this.dPosEdit46.DefaultValue = null;
            this.dPosEdit46.EditColor = System.Drawing.Color.Black;
            this.dPosEdit46.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit46.EditWidth = 100;
            this.dPosEdit46.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dPosEdit46.IsModified = false;
            this.dPosEdit46.Location = new System.Drawing.Point(14, 51);
            this.dPosEdit46.Margin = new System.Windows.Forms.Padding(0);
            this.dPosEdit46.MaxValue = 9999999D;
            this.dPosEdit46.MinValue = -9999999D;
            this.dPosEdit46.ModifiedColor = System.Drawing.Color.Aqua;
            this.dPosEdit46.MotorP = this.MT_HDT_A_AxisX;
            this.dPosEdit46.Name = "dPosEdit46";
            this.dPosEdit46.NoChangeInAuto = false;
            this.dPosEdit46.PosValue = "";
            this.dPosEdit46.Size = new System.Drawing.Size(386, 25);
            this.dPosEdit46.StepValue = 0D;
            this.dPosEdit46.TabIndex = 40;
            this.dPosEdit46.Unit = "um";
            this.dPosEdit46.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dPosEdit46.UnitWidth = 40;
            this.dPosEdit46.ValueType = KCSDK.ValueDataType.Int;
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.dFieldEdit19);
            this.groupBox17.Controls.Add(this.dFieldEdit20);
            this.groupBox17.Controls.Add(this.dFieldEdit18);
            this.groupBox17.Controls.Add(this.dFieldEdit17);
            this.groupBox17.Location = new System.Drawing.Point(18, 374);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(1048, 107);
            this.groupBox17.TabIndex = 18;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Socket Setting";
            // 
            // dFieldEdit19
            // 
            this.dFieldEdit19.AutoFocus = false;
            this.dFieldEdit19.Caption = "CCDB Port NO";
            this.dFieldEdit19.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit19.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit19.DataName = "HDTB_CcdPortNo";
            this.dFieldEdit19.DataSource = this.SetDS;
            this.dFieldEdit19.DefaultValue = null;
            this.dFieldEdit19.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit19.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit19.EditWidth = 200;
            this.dFieldEdit19.FieldValue = "";
            this.dFieldEdit19.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit19.IsModified = false;
            this.dFieldEdit19.Location = new System.Drawing.Point(548, 60);
            this.dFieldEdit19.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit19.MaxValue = 999999D;
            this.dFieldEdit19.MinValue = -9999999D;
            this.dFieldEdit19.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit19.Name = "dFieldEdit19";
            this.dFieldEdit19.NoChangeInAuto = false;
            this.dFieldEdit19.Size = new System.Drawing.Size(480, 29);
            this.dFieldEdit19.StepValue = 0D;
            this.dFieldEdit19.TabIndex = 3;
            this.dFieldEdit19.Unit = "";
            this.dFieldEdit19.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit19.UnitWidth = 0;
            this.dFieldEdit19.ValueType = KCSDK.ValueDataType.String;
            // 
            // dFieldEdit20
            // 
            this.dFieldEdit20.AutoFocus = false;
            this.dFieldEdit20.Caption = "CCDB IP Address";
            this.dFieldEdit20.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit20.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit20.DataName = "HDTB_CcdIP";
            this.dFieldEdit20.DataSource = this.SetDS;
            this.dFieldEdit20.DefaultValue = null;
            this.dFieldEdit20.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit20.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit20.EditWidth = 200;
            this.dFieldEdit20.FieldValue = "";
            this.dFieldEdit20.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit20.IsModified = false;
            this.dFieldEdit20.Location = new System.Drawing.Point(548, 31);
            this.dFieldEdit20.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit20.MaxValue = 999999D;
            this.dFieldEdit20.MinValue = -9999999D;
            this.dFieldEdit20.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit20.Name = "dFieldEdit20";
            this.dFieldEdit20.NoChangeInAuto = false;
            this.dFieldEdit20.Size = new System.Drawing.Size(480, 29);
            this.dFieldEdit20.StepValue = 0D;
            this.dFieldEdit20.TabIndex = 2;
            this.dFieldEdit20.Unit = "";
            this.dFieldEdit20.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit20.UnitWidth = 0;
            this.dFieldEdit20.ValueType = KCSDK.ValueDataType.String;
            // 
            // dFieldEdit18
            // 
            this.dFieldEdit18.AutoFocus = false;
            this.dFieldEdit18.Caption = "CCDA Port NO";
            this.dFieldEdit18.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit18.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit18.DataName = "HDTA_CcdPortNo";
            this.dFieldEdit18.DataSource = this.SetDS;
            this.dFieldEdit18.DefaultValue = null;
            this.dFieldEdit18.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit18.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit18.EditWidth = 200;
            this.dFieldEdit18.FieldValue = "";
            this.dFieldEdit18.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit18.IsModified = false;
            this.dFieldEdit18.Location = new System.Drawing.Point(18, 60);
            this.dFieldEdit18.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit18.MaxValue = 999999D;
            this.dFieldEdit18.MinValue = -9999999D;
            this.dFieldEdit18.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit18.Name = "dFieldEdit18";
            this.dFieldEdit18.NoChangeInAuto = false;
            this.dFieldEdit18.Size = new System.Drawing.Size(484, 29);
            this.dFieldEdit18.StepValue = 0D;
            this.dFieldEdit18.TabIndex = 1;
            this.dFieldEdit18.Unit = "";
            this.dFieldEdit18.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit18.UnitWidth = 0;
            this.dFieldEdit18.ValueType = KCSDK.ValueDataType.String;
            // 
            // dFieldEdit17
            // 
            this.dFieldEdit17.AutoFocus = false;
            this.dFieldEdit17.Caption = "CCDA IP Address";
            this.dFieldEdit17.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit17.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit17.DataName = "HDTA_CcdIP";
            this.dFieldEdit17.DataSource = this.SetDS;
            this.dFieldEdit17.DefaultValue = null;
            this.dFieldEdit17.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit17.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit17.EditWidth = 200;
            this.dFieldEdit17.FieldValue = "";
            this.dFieldEdit17.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit17.IsModified = false;
            this.dFieldEdit17.Location = new System.Drawing.Point(18, 31);
            this.dFieldEdit17.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit17.MaxValue = 999999D;
            this.dFieldEdit17.MinValue = -9999999D;
            this.dFieldEdit17.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit17.Name = "dFieldEdit17";
            this.dFieldEdit17.NoChangeInAuto = false;
            this.dFieldEdit17.Size = new System.Drawing.Size(484, 29);
            this.dFieldEdit17.StepValue = 0D;
            this.dFieldEdit17.TabIndex = 0;
            this.dFieldEdit17.Unit = "";
            this.dFieldEdit17.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit17.UnitWidth = 0;
            this.dFieldEdit17.ValueType = KCSDK.ValueDataType.String;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.dCheckBox4);
            this.groupBox12.Controls.Add(this.dCheckBox3);
            this.groupBox12.Controls.Add(this.dCheckBox2);
            this.groupBox12.Controls.Add(this.dCheckBox1);
            this.groupBox12.Location = new System.Drawing.Point(6, 64);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(400, 170);
            this.groupBox12.TabIndex = 20;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Option";
            // 
            // dCheckBox4
            // 
            this.dCheckBox4.AutoSize = true;
            this.dCheckBox4.BackColor = System.Drawing.SystemColors.Control;
            this.dCheckBox4.DataName = "EnableBottomHeadCCD";
            this.dCheckBox4.DataSource = this.SetDS;
            this.dCheckBox4.DefaultValue = false;
            this.dCheckBox4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dCheckBox4.IsModified = false;
            this.dCheckBox4.Location = new System.Drawing.Point(6, 127);
            this.dCheckBox4.ModifiedColor = System.Drawing.Color.Aqua;
            this.dCheckBox4.Name = "dCheckBox4";
            this.dCheckBox4.NoChangeInAuto = false;
            this.dCheckBox4.Size = new System.Drawing.Size(224, 25);
            this.dCheckBox4.TabIndex = 21;
            this.dCheckBox4.Text = "Enable Bottom Head CCD";
            this.dCheckBox4.UseVisualStyleBackColor = true;
            // 
            // dCheckBox3
            // 
            this.dCheckBox3.AutoSize = true;
            this.dCheckBox3.BackColor = System.Drawing.SystemColors.Control;
            this.dCheckBox3.DataName = "EnableTopHeadCCD";
            this.dCheckBox3.DataSource = this.SetDS;
            this.dCheckBox3.DefaultValue = false;
            this.dCheckBox3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dCheckBox3.IsModified = false;
            this.dCheckBox3.Location = new System.Drawing.Point(6, 96);
            this.dCheckBox3.ModifiedColor = System.Drawing.Color.Aqua;
            this.dCheckBox3.Name = "dCheckBox3";
            this.dCheckBox3.NoChangeInAuto = false;
            this.dCheckBox3.Size = new System.Drawing.Size(196, 25);
            this.dCheckBox3.TabIndex = 20;
            this.dCheckBox3.Text = "Enable Top Head CCD";
            this.dCheckBox3.UseVisualStyleBackColor = true;
            // 
            // dCheckBox2
            // 
            this.dCheckBox2.AutoSize = true;
            this.dCheckBox2.BackColor = System.Drawing.SystemColors.Control;
            this.dCheckBox2.DataName = "DoNotUseBottomHead";
            this.dCheckBox2.DataSource = this.SetDS;
            this.dCheckBox2.DefaultValue = false;
            this.dCheckBox2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dCheckBox2.IsModified = false;
            this.dCheckBox2.Location = new System.Drawing.Point(6, 65);
            this.dCheckBox2.ModifiedColor = System.Drawing.Color.Aqua;
            this.dCheckBox2.Name = "dCheckBox2";
            this.dCheckBox2.NoChangeInAuto = false;
            this.dCheckBox2.Size = new System.Drawing.Size(218, 25);
            this.dCheckBox2.TabIndex = 19;
            this.dCheckBox2.Text = "Do not use Bottom Head";
            this.dCheckBox2.UseVisualStyleBackColor = true;
            // 
            // dCheckBox1
            // 
            this.dCheckBox1.AutoSize = true;
            this.dCheckBox1.BackColor = System.Drawing.SystemColors.Control;
            this.dCheckBox1.DataName = "DoNotUseTopHead";
            this.dCheckBox1.DataSource = this.SetDS;
            this.dCheckBox1.DefaultValue = false;
            this.dCheckBox1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dCheckBox1.IsModified = false;
            this.dCheckBox1.Location = new System.Drawing.Point(6, 34);
            this.dCheckBox1.ModifiedColor = System.Drawing.Color.Aqua;
            this.dCheckBox1.Name = "dCheckBox1";
            this.dCheckBox1.NoChangeInAuto = false;
            this.dCheckBox1.Size = new System.Drawing.Size(190, 25);
            this.dCheckBox1.TabIndex = 18;
            this.dCheckBox1.Text = "Do not use Top Head";
            this.dCheckBox1.UseVisualStyleBackColor = true;
            // 
            // HDT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 733);
            this.Name = "HDT";
            this.Text = "HDT";
            this.tabMain.ResumeLayout(false);
            this.tpControl.ResumeLayout(false);
            this.tpPosition.ResumeLayout(false);
            this.tpSetting.ResumeLayout(false);
            this.tpFlow.ResumeLayout(false);
            this.tpSuperSetting.ResumeLayout(false);
            this.TabFlow.ResumeLayout(false);
            this.tpHome.ResumeLayout(false);
            this.tpAuto.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.IO_HDT_A.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.IO_HDT_B.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.FC_HDT_A.ResumeLayout(false);
            this.FC_HDT_B.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox17.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage IO_HDT_A;
        private System.Windows.Forms.TabPage IO_HDT_B;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage FC_HDT_A;
        private System.Windows.Forms.TabPage FC_HDT_B;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button button4;
        private ProVLib.Motor MT_CCD_A_AxisZ;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button3;
        private ProVLib.Motor MT_HDT_A_AxisZ;
        private System.Windows.Forms.Panel panel1;
        private ProVLib.Motor MT_HDT_A_AxisX;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private ProVLib.Motor MT_HDT_A_AxisU;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button button5;
        private ProVLib.Motor MT_CCD_B_AxisZ;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Button button6;
        private ProVLib.Motor MT_HDT_B_AxisZ;
        private System.Windows.Forms.Panel panel11;
        private ProVLib.Motor MT_HDT_B_AxisX;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Button button8;
        private ProVLib.Motor MT_HDT_B_AxisU;
        private System.Windows.Forms.Panel panel13;
        private ProVLib.OutBit OB_HDT_A_Vacuum_1;
        private ProVLib.InBit IB_HDT_A_VacDetect_4;
        private ProVLib.OutBit OB_HDT_A_Vacuum_3;
        private ProVLib.InBit IB_HDT_A_VacDetect_2;
        private ProVLib.OutBit OB_HDT_A_Destroy_1;
        private ProVLib.OutBit OB_HDT_A_Destroy_4;
        private ProVLib.OutBit OB_HDT_A_Destroy_3;
        private ProVLib.OutBit OB_HDT_A_Destroy_2;
        private ProVLib.InBit IB_HDT_A_VacDetect_1;
        private ProVLib.OutBit OB_HDT_A_Vacuum_4;
        private ProVLib.InBit IB_HDT_A_VacDetect_3;
        private ProVLib.OutBit OB_HDT_A_Vacuum_2;
        private System.Windows.Forms.Panel panel14;
        private ProVLib.OutBit OB_HDT_B_Vacuum_1;
        private ProVLib.InBit IB_HDT_B_VacDetect_4;
        private ProVLib.OutBit OB_HDT_B_Vacuum_3;
        private ProVLib.InBit IB_HDT_B_VacDetect_2;
        private ProVLib.OutBit OB_HDT_B_Destroy_1;
        private ProVLib.OutBit OB_HDT_B_Destroy_4;
        private ProVLib.OutBit OB_HDT_B_Destroy_3;
        private ProVLib.OutBit OB_HDT_B_Destroy_2;
        private ProVLib.InBit IB_HDT_B_VacDetect_1;
        private ProVLib.OutBit OB_HDT_B_Vacuum_4;
        private ProVLib.InBit IB_HDT_B_VacDetect_3;
        private ProVLib.OutBit OB_HDT_B_Vacuum_2;
        private ProVLib.FlowChart flowChart4;
        private ProVLib.FlowChart flowChart3;
        private ProVLib.FlowChart flowChart2;
        private ProVLib.FlowChart flowChart1;
        private ProVLib.FlowChart FC_HDT_HOME;
        private ProVLib.FlowChart flowChart9;
        private ProVLib.FlowChart flowChart8;
        private ProVLib.FlowChart flowChart7;
        private ProVLib.FlowChart flowChart6;
        private ProVLib.FlowChart flowChart5;
        private ProVLib.FlowChart flowChart15;
        private ProVLib.FlowChart flowChart14;
        private ProVLib.FlowChart flowChart13;
        private ProVLib.FlowChart flowChart12;
        private ProVLib.FlowChart flowChart11;
        private ProVLib.FlowChart flowChart10;
        private ProVLib.FlowChart FC_HDT_A_AUTO_Pick;
        private ProVLib.FlowChart flowChart17;
        private ProVLib.FlowChart flowChart26;
        private ProVLib.FlowChart flowChart27;
        private ProVLib.FlowChart flowChart28;
        private ProVLib.FlowChart FC_HDT_A_AUTO_Move;
        private ProVLib.FlowChart FC_HDT_A_AUTO_Inspection;
        private ProVLib.FlowChart FC_HDT_A_AUTO_Place;
        private ProVLib.FlowChart flowChart18;
        private ProVLib.FlowChart flowChart16;
        private ProVLib.FlowChart flowChart23;
        private ProVLib.FlowChart flowChart22;
        private ProVLib.FlowChart flowChart21;
        private ProVLib.FlowChart flowChart20;
        private ProVLib.FlowChart flowChart19;
        private ProVLib.FlowChart flowChart36;
        private ProVLib.FlowChart flowChart35;
        private ProVLib.FlowChart flowChart34;
        private ProVLib.FlowChart flowChart33;
        private ProVLib.FlowChart flowChart32;
        private ProVLib.FlowChart flowChart31;
        private ProVLib.FlowChart flowChart30;
        private ProVLib.FlowChart flowChart29;
        private ProVLib.FlowChart flowChart25;
        private ProVLib.FlowChart flowChart24;
        private ProVLib.FlowChart flowChart45;
        private ProVLib.FlowChart flowChart39;
        private ProVLib.FlowChart flowChart40;
        private ProVLib.FlowChart flowChart41;
        private ProVLib.FlowChart flowChart44;
        private ProVLib.FlowChart flowChart43;
        private ProVLib.FlowChart flowChart42;
        private ProVLib.FlowChart flowChart38;
        private ProVLib.FlowChart flowChart37;
        private ProVLib.FlowChart flowChart46;
        private ProVLib.FlowChart flowChart47;
        private ProVLib.FlowChart flowChart48;
        private ProVLib.FlowChart flowChart49;
        private ProVLib.FlowChart flowChart50;
        private ProVLib.FlowChart flowChart51;
        private ProVLib.FlowChart flowChart52;
        private ProVLib.FlowChart flowChart53;
        private ProVLib.FlowChart FC_HDT_B_AUTO_Place;
        private ProVLib.FlowChart flowChart55;
        private ProVLib.FlowChart flowChart56;
        private ProVLib.FlowChart flowChart57;
        private ProVLib.FlowChart flowChart58;
        private ProVLib.FlowChart flowChart59;
        private ProVLib.FlowChart flowChart60;
        private ProVLib.FlowChart flowChart61;
        private ProVLib.FlowChart flowChart62;
        private ProVLib.FlowChart flowChart63;
        private ProVLib.FlowChart flowChart64;
        private ProVLib.FlowChart flowChart65;
        private ProVLib.FlowChart flowChart66;
        private ProVLib.FlowChart flowChart67;
        private ProVLib.FlowChart flowChart68;
        private ProVLib.FlowChart flowChart69;
        private ProVLib.FlowChart flowChart70;
        private ProVLib.FlowChart FC_HDT_B_AUTO_Pick;
        private ProVLib.FlowChart flowChart72;
        private ProVLib.FlowChart flowChart73;
        private ProVLib.FlowChart flowChart74;
        private ProVLib.FlowChart flowChart75;
        private ProVLib.FlowChart flowChart76;
        private ProVLib.FlowChart flowChart77;
        private ProVLib.FlowChart flowChart78;
        private ProVLib.FlowChart flowChart79;
        private ProVLib.FlowChart FC_HDT_B_AUTO_Inspection;
        private ProVLib.FlowChart flowChart81;
        private ProVLib.FlowChart flowChart54;
        private ProVLib.FlowChart flowChart71;
        private ProVLib.FlowChart flowChart80;
        private ProVLib.FlowChart FC_HDT_B_AUTO_Move;
        private System.Windows.Forms.GroupBox groupBox3;
        private KCSDK.DPosEdit dPosEdit9;
        private KCSDK.DPosEdit dPosEdit1;
        private KCSDK.DPosEdit dPosEdit2;
        private KCSDK.DPosEdit dPosEdit3;
        private KCSDK.DPosEdit dPosEdit14;
        private KCSDK.DPosEdit dPosEdit4;
        private KCSDK.DPosEdit dPosEdit7;
        private KCSDK.DPosEdit dPosEdit8;
        private KCSDK.DPosEdit dPosEdit6;
        private KCSDK.DPosEdit dPosEdit5;
        private KCSDK.DPosEdit dPosEdit10;
        private KCSDK.DPosEdit dPosEdit11;
        private KCSDK.DPosEdit dPosEdit12;
        private KCSDK.DPosEdit dPosEdit15;
        private System.Windows.Forms.GroupBox groupBox1;
        private KCSDK.DPosEdit dPosEdit13;
        private KCSDK.DPosEdit dPosEdit16;
        private KCSDK.DPosEdit dPosEdit17;
        private KCSDK.DPosEdit dPosEdit18;
        private KCSDK.DPosEdit dPosEdit19;
        private KCSDK.DPosEdit dPosEdit20;
        private KCSDK.DPosEdit dPosEdit21;
        private KCSDK.DPosEdit dPosEdit22;
        private KCSDK.DPosEdit dPosEdit23;
        private KCSDK.DPosEdit dPosEdit24;
        private KCSDK.DPosEdit dPosEdit25;
        private KCSDK.DPosEdit dPosEdit26;
        private KCSDK.DPosEdit dPosEdit27;
        private KCSDK.DPosEdit dPosEdit28;
        private System.Windows.Forms.GroupBox groupBox11;
        private KCSDK.DFieldEdit dFieldEdit5;
        private KCSDK.DFieldEdit dFieldEdit6;
        private KCSDK.DFieldEdit dFieldEdit3;
        private KCSDK.DFieldEdit dFieldEdit4;
        private KCSDK.DFieldEdit dFieldEdit1;
        private KCSDK.DFieldEdit dFieldEdit2;
        private KCSDK.DFieldEdit dFieldEdit10;
        private KCSDK.DFieldEdit dFieldEdit11;
        private KCSDK.DFieldEdit dFieldEdit7;
        private KCSDK.DFieldEdit dFieldEdit8;
        private KCSDK.DFieldEdit dFieldEdit9;
        private KCSDK.DFieldEdit dFieldEdit12;
        private KCSDK.DFieldEdit dFieldEdit13;
        private KCSDK.DFieldEdit dFieldEdit14;
        private KCSDK.DFieldEdit dFieldEdit15;
        private KCSDK.DFieldEdit dFieldEdit16;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private ProVTool.ProVClientSocket HDT_A_ClientSocket;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button11;
        private ProVTool.ProVClientSocket HDT_B_ClientSocket;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.GroupBox groupBox17;
        private KCSDK.DFieldEdit dFieldEdit19;
        private KCSDK.DFieldEdit dFieldEdit20;
        private KCSDK.DFieldEdit dFieldEdit18;
        private KCSDK.DFieldEdit dFieldEdit17;
        private ProVLib.FlowChart flowChart82;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.GroupBox groupBox12;
        private KCSDK.DCheckBox dCheckBox2;
        private KCSDK.DCheckBox dCheckBox1;
        private ProVLib.FlowChart flowChart83;
        private ProVLib.FlowChart flowChart84;
        private ProVLib.FlowChart flowChart85;
        private KCSDK.DPosEdit dPosEdit30;
        private KCSDK.DPosEdit dPosEdit29;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox4;
        private KCSDK.DPosEdit dPosEdit31;
        private KCSDK.DPosEdit dPosEdit32;
        private KCSDK.DPosEdit dPosEdit33;
        private KCSDK.DPosEdit dPosEdit34;
        private KCSDK.DPosEdit dPosEdit35;
        private KCSDK.DPosEdit dPosEdit36;
        private KCSDK.DPosEdit dPosEdit43;
        private KCSDK.DPosEdit dPosEdit44;
        private System.Windows.Forms.GroupBox groupBox2;
        private KCSDK.DPosEdit dPosEdit37;
        private KCSDK.DPosEdit dPosEdit38;
        private KCSDK.DPosEdit dPosEdit39;
        private KCSDK.DPosEdit dPosEdit40;
        private KCSDK.DPosEdit dPosEdit41;
        private KCSDK.DPosEdit dPosEdit42;
        private KCSDK.DPosEdit dPosEdit45;
        private KCSDK.DPosEdit dPosEdit46;
        private KCSDK.DCheckBox dCheckBox4;
        private KCSDK.DCheckBox dCheckBox3;
        private ProVLib.FlowChart flowChart86;
        private ProVLib.FlowChart flowChart87;
        private KCSDK.DPosEdit dPosEdit47;
        private KCSDK.DPosEdit dPosEdit48;
        private ProVLib.FlowChart flowChart88;
        private ProVLib.FlowChart flowChart89;


    }
}