namespace TBPP14200
{
    partial class BoardTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoardTable));
            this.dFieldEdit4 = new KCSDK.DFieldEdit();
            this.dFieldEdit5 = new KCSDK.DFieldEdit();
            this.dFieldEdit1 = new KCSDK.DFieldEdit();
            this.dFieldEdit3 = new KCSDK.DFieldEdit();
            this.dFieldEdit16 = new KCSDK.DFieldEdit();
            this.dFieldEdit13 = new KCSDK.DFieldEdit();
            this.dFieldEdit11 = new KCSDK.DFieldEdit();
            this.dFieldEdit2 = new KCSDK.DFieldEdit();
            this.dFieldEdit8 = new KCSDK.DFieldEdit();
            this.dFieldEdit7 = new KCSDK.DFieldEdit();
            this.dFieldEdit12 = new KCSDK.DFieldEdit();
            this.dComboBox1 = new KCSDK.DComboBox();
            this.dComboBox2 = new KCSDK.DComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PackageContainer)).BeginInit();
            this.PackageContainer.Panel1.SuspendLayout();
            this.PackageContainer.Panel2.SuspendLayout();
            this.PackageContainer.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.pnlControl.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
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
            this.imgList.Images.SetKeyName(9, "closebtn.png");
            // 
            // PackageContainer
            // 
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.panel3);
            this.pnlControl.Controls.Add(this.panel1);
            this.pnlControl.Controls.Add(this.dComboBox2);
            this.pnlControl.Controls.Add(this.dComboBox1);
            this.pnlControl.Controls.Add(this.dFieldEdit4);
            this.pnlControl.Controls.Add(this.dFieldEdit5);
            this.pnlControl.Controls.Add(this.dFieldEdit1);
            this.pnlControl.Controls.Add(this.dFieldEdit3);
            this.pnlControl.Controls.Add(this.dFieldEdit16);
            this.pnlControl.Controls.Add(this.dFieldEdit13);
            this.pnlControl.Controls.Add(this.dFieldEdit11);
            this.pnlControl.Controls.Add(this.dFieldEdit2);
            this.pnlControl.Controls.Add(this.dFieldEdit8);
            this.pnlControl.Controls.Add(this.dFieldEdit7);
            this.pnlControl.Controls.Add(this.dFieldEdit12);
            // 
            // dFieldEdit4
            // 
            this.dFieldEdit4.AutoFocus = false;
            this.dFieldEdit4.Caption = "Board Length";
            this.dFieldEdit4.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit4.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit4.DataName = "Board_Length";
            this.dFieldEdit4.DataSource = this.PreloadPackageDS;
            this.dFieldEdit4.DefaultValue = null;
            this.dFieldEdit4.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit4.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit4.EditWidth = 100;
            this.dFieldEdit4.FieldValue = "";
            this.dFieldEdit4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit4.IsModified = false;
            this.dFieldEdit4.Location = new System.Drawing.Point(4, 188);
            this.dFieldEdit4.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit4.MaxValue = 9999999D;
            this.dFieldEdit4.MinValue = 0D;
            this.dFieldEdit4.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit4.Name = "dFieldEdit4";
            this.dFieldEdit4.NoChangeInAuto = false;
            this.dFieldEdit4.Size = new System.Drawing.Size(300, 29);
            this.dFieldEdit4.StepValue = 0D;
            this.dFieldEdit4.TabIndex = 128;
            this.dFieldEdit4.Unit = "um";
            this.dFieldEdit4.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit4.UnitWidth = 40;
            this.dFieldEdit4.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit5
            // 
            this.dFieldEdit5.AutoFocus = false;
            this.dFieldEdit5.Caption = "Board Width";
            this.dFieldEdit5.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit5.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit5.DataName = "Board_Width";
            this.dFieldEdit5.DataSource = this.PreloadPackageDS;
            this.dFieldEdit5.DefaultValue = null;
            this.dFieldEdit5.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit5.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit5.EditWidth = 100;
            this.dFieldEdit5.FieldValue = "";
            this.dFieldEdit5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit5.IsModified = false;
            this.dFieldEdit5.Location = new System.Drawing.Point(4, 218);
            this.dFieldEdit5.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit5.MaxValue = 9999999D;
            this.dFieldEdit5.MinValue = 0D;
            this.dFieldEdit5.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit5.Name = "dFieldEdit5";
            this.dFieldEdit5.NoChangeInAuto = false;
            this.dFieldEdit5.Size = new System.Drawing.Size(300, 29);
            this.dFieldEdit5.StepValue = 0D;
            this.dFieldEdit5.TabIndex = 129;
            this.dFieldEdit5.Unit = "um";
            this.dFieldEdit5.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit5.UnitWidth = 40;
            this.dFieldEdit5.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit1
            // 
            this.dFieldEdit1.AutoFocus = false;
            this.dFieldEdit1.Caption = "Board Offset X";
            this.dFieldEdit1.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit1.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit1.DataName = "Board_XShift";
            this.dFieldEdit1.DataSource = this.PreloadPackageDS;
            this.dFieldEdit1.DefaultValue = null;
            this.dFieldEdit1.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit1.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit1.EditWidth = 100;
            this.dFieldEdit1.FieldValue = "";
            this.dFieldEdit1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit1.IsModified = false;
            this.dFieldEdit1.Location = new System.Drawing.Point(4, 128);
            this.dFieldEdit1.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit1.MaxValue = 9999999D;
            this.dFieldEdit1.MinValue = 0D;
            this.dFieldEdit1.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit1.Name = "dFieldEdit1";
            this.dFieldEdit1.NoChangeInAuto = false;
            this.dFieldEdit1.Size = new System.Drawing.Size(300, 29);
            this.dFieldEdit1.StepValue = 0D;
            this.dFieldEdit1.TabIndex = 126;
            this.dFieldEdit1.Unit = "um";
            this.dFieldEdit1.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit1.UnitWidth = 40;
            this.dFieldEdit1.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit3
            // 
            this.dFieldEdit3.AutoFocus = false;
            this.dFieldEdit3.Caption = "Board Offset Y";
            this.dFieldEdit3.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit3.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit3.DataName = "Board_YShift";
            this.dFieldEdit3.DataSource = this.PreloadPackageDS;
            this.dFieldEdit3.DefaultValue = null;
            this.dFieldEdit3.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit3.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit3.EditWidth = 100;
            this.dFieldEdit3.FieldValue = "";
            this.dFieldEdit3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit3.IsModified = false;
            this.dFieldEdit3.Location = new System.Drawing.Point(4, 158);
            this.dFieldEdit3.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit3.MaxValue = 9999999D;
            this.dFieldEdit3.MinValue = 0D;
            this.dFieldEdit3.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit3.Name = "dFieldEdit3";
            this.dFieldEdit3.NoChangeInAuto = false;
            this.dFieldEdit3.Size = new System.Drawing.Size(300, 29);
            this.dFieldEdit3.StepValue = 0D;
            this.dFieldEdit3.TabIndex = 127;
            this.dFieldEdit3.Unit = "um";
            this.dFieldEdit3.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit3.UnitWidth = 40;
            this.dFieldEdit3.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit16
            // 
            this.dFieldEdit16.AutoFocus = false;
            this.dFieldEdit16.Caption = "Socket Depth";
            this.dFieldEdit16.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit16.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit16.DataName = "Board_Depth";
            this.dFieldEdit16.DataSource = this.PreloadPackageDS;
            this.dFieldEdit16.DefaultValue = null;
            this.dFieldEdit16.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit16.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit16.EditWidth = 100;
            this.dFieldEdit16.FieldValue = "";
            this.dFieldEdit16.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit16.IsModified = false;
            this.dFieldEdit16.Location = new System.Drawing.Point(4, 308);
            this.dFieldEdit16.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit16.MaxValue = 9999999D;
            this.dFieldEdit16.MinValue = 0D;
            this.dFieldEdit16.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit16.Name = "dFieldEdit16";
            this.dFieldEdit16.NoChangeInAuto = false;
            this.dFieldEdit16.Size = new System.Drawing.Size(300, 29);
            this.dFieldEdit16.StepValue = 0D;
            this.dFieldEdit16.TabIndex = 125;
            this.dFieldEdit16.Unit = "um";
            this.dFieldEdit16.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit16.UnitWidth = 40;
            this.dFieldEdit16.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit13
            // 
            this.dFieldEdit13.AutoFocus = false;
            this.dFieldEdit13.Caption = "Board XN";
            this.dFieldEdit13.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit13.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit13.DataName = "Board_XN";
            this.dFieldEdit13.DataSource = this.PreloadPackageDS;
            this.dFieldEdit13.DefaultValue = null;
            this.dFieldEdit13.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit13.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit13.EditWidth = 100;
            this.dFieldEdit13.FieldValue = "";
            this.dFieldEdit13.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit13.IsModified = false;
            this.dFieldEdit13.Location = new System.Drawing.Point(4, 8);
            this.dFieldEdit13.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit13.MaxValue = 100D;
            this.dFieldEdit13.MinValue = 0D;
            this.dFieldEdit13.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit13.Name = "dFieldEdit13";
            this.dFieldEdit13.NoChangeInAuto = false;
            this.dFieldEdit13.Size = new System.Drawing.Size(300, 29);
            this.dFieldEdit13.StepValue = 0D;
            this.dFieldEdit13.TabIndex = 119;
            this.dFieldEdit13.Unit = "pcs";
            this.dFieldEdit13.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit13.UnitWidth = 40;
            this.dFieldEdit13.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit11
            // 
            this.dFieldEdit11.AutoFocus = false;
            this.dFieldEdit11.Caption = "Board Pitch X";
            this.dFieldEdit11.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit11.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit11.DataName = "Board_XPitch";
            this.dFieldEdit11.DataSource = this.PreloadPackageDS;
            this.dFieldEdit11.DefaultValue = null;
            this.dFieldEdit11.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit11.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit11.EditWidth = 100;
            this.dFieldEdit11.FieldValue = "";
            this.dFieldEdit11.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit11.IsModified = false;
            this.dFieldEdit11.Location = new System.Drawing.Point(4, 68);
            this.dFieldEdit11.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit11.MaxValue = 9999999D;
            this.dFieldEdit11.MinValue = 0D;
            this.dFieldEdit11.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit11.Name = "dFieldEdit11";
            this.dFieldEdit11.NoChangeInAuto = false;
            this.dFieldEdit11.Size = new System.Drawing.Size(300, 29);
            this.dFieldEdit11.StepValue = 0D;
            this.dFieldEdit11.TabIndex = 121;
            this.dFieldEdit11.Unit = "um";
            this.dFieldEdit11.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit11.UnitWidth = 40;
            this.dFieldEdit11.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit2
            // 
            this.dFieldEdit2.AutoFocus = false;
            this.dFieldEdit2.Caption = "Socket Thickness";
            this.dFieldEdit2.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit2.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit2.DataName = "Board_Thickness";
            this.dFieldEdit2.DataSource = this.PreloadPackageDS;
            this.dFieldEdit2.DefaultValue = null;
            this.dFieldEdit2.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit2.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit2.EditWidth = 100;
            this.dFieldEdit2.FieldValue = "";
            this.dFieldEdit2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit2.IsModified = false;
            this.dFieldEdit2.Location = new System.Drawing.Point(4, 248);
            this.dFieldEdit2.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit2.MaxValue = 9999999D;
            this.dFieldEdit2.MinValue = 0D;
            this.dFieldEdit2.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit2.Name = "dFieldEdit2";
            this.dFieldEdit2.NoChangeInAuto = false;
            this.dFieldEdit2.Size = new System.Drawing.Size(300, 29);
            this.dFieldEdit2.StepValue = 0D;
            this.dFieldEdit2.TabIndex = 117;
            this.dFieldEdit2.Unit = "um";
            this.dFieldEdit2.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit2.UnitWidth = 40;
            this.dFieldEdit2.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit8
            // 
            this.dFieldEdit8.AutoFocus = false;
            this.dFieldEdit8.Caption = "Board YN";
            this.dFieldEdit8.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit8.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit8.DataName = "Board_YN";
            this.dFieldEdit8.DataSource = this.PreloadPackageDS;
            this.dFieldEdit8.DefaultValue = null;
            this.dFieldEdit8.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit8.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit8.EditWidth = 100;
            this.dFieldEdit8.FieldValue = "";
            this.dFieldEdit8.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit8.IsModified = false;
            this.dFieldEdit8.Location = new System.Drawing.Point(4, 38);
            this.dFieldEdit8.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit8.MaxValue = 100D;
            this.dFieldEdit8.MinValue = 0D;
            this.dFieldEdit8.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit8.Name = "dFieldEdit8";
            this.dFieldEdit8.NoChangeInAuto = false;
            this.dFieldEdit8.Size = new System.Drawing.Size(300, 29);
            this.dFieldEdit8.StepValue = 0D;
            this.dFieldEdit8.TabIndex = 120;
            this.dFieldEdit8.Unit = "pcs";
            this.dFieldEdit8.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit8.UnitWidth = 40;
            this.dFieldEdit8.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit7
            // 
            this.dFieldEdit7.AutoFocus = false;
            this.dFieldEdit7.Caption = "Socket Height";
            this.dFieldEdit7.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit7.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit7.DataName = "Board_Height";
            this.dFieldEdit7.DataSource = this.PreloadPackageDS;
            this.dFieldEdit7.DefaultValue = null;
            this.dFieldEdit7.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit7.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit7.EditWidth = 100;
            this.dFieldEdit7.FieldValue = "";
            this.dFieldEdit7.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit7.IsModified = false;
            this.dFieldEdit7.Location = new System.Drawing.Point(4, 278);
            this.dFieldEdit7.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit7.MaxValue = 9999999D;
            this.dFieldEdit7.MinValue = 0D;
            this.dFieldEdit7.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit7.Name = "dFieldEdit7";
            this.dFieldEdit7.NoChangeInAuto = false;
            this.dFieldEdit7.Size = new System.Drawing.Size(300, 29);
            this.dFieldEdit7.StepValue = 0D;
            this.dFieldEdit7.TabIndex = 118;
            this.dFieldEdit7.Unit = "um";
            this.dFieldEdit7.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit7.UnitWidth = 40;
            this.dFieldEdit7.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit12
            // 
            this.dFieldEdit12.AutoFocus = false;
            this.dFieldEdit12.Caption = "Board Pitch Y";
            this.dFieldEdit12.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit12.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit12.DataName = "Board_YPitch";
            this.dFieldEdit12.DataSource = this.PreloadPackageDS;
            this.dFieldEdit12.DefaultValue = null;
            this.dFieldEdit12.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit12.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit12.EditWidth = 100;
            this.dFieldEdit12.FieldValue = "";
            this.dFieldEdit12.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit12.IsModified = false;
            this.dFieldEdit12.Location = new System.Drawing.Point(4, 98);
            this.dFieldEdit12.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit12.MaxValue = 9999999D;
            this.dFieldEdit12.MinValue = 0D;
            this.dFieldEdit12.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit12.Name = "dFieldEdit12";
            this.dFieldEdit12.NoChangeInAuto = false;
            this.dFieldEdit12.Size = new System.Drawing.Size(300, 29);
            this.dFieldEdit12.StepValue = 0D;
            this.dFieldEdit12.TabIndex = 122;
            this.dFieldEdit12.Unit = "um";
            this.dFieldEdit12.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit12.UnitWidth = 40;
            this.dFieldEdit12.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dComboBox1
            // 
            this.dComboBox1.BackColor = System.Drawing.SystemColors.Window;
            this.dComboBox1.DataName = "ClamsheelDir";
            this.dComboBox1.DataSource = this.PreloadPackageDS;
            this.dComboBox1.DefaultValue = 0;
            this.dComboBox1.FormattingEnabled = true;
            this.dComboBox1.IsModified = false;
            this.dComboBox1.Items.AddRange(new object[] {
            "Top",
            "Bottom",
            "Left",
            "Right"});
            this.dComboBox1.Location = new System.Drawing.Point(162, 354);
            this.dComboBox1.ModifiedColor = System.Drawing.Color.Aqua;
            this.dComboBox1.Name = "dComboBox1";
            this.dComboBox1.NoChangeInAuto = false;
            this.dComboBox1.Size = new System.Drawing.Size(142, 35);
            this.dComboBox1.TabIndex = 130;
            // 
            // dComboBox2
            // 
            this.dComboBox2.BackColor = System.Drawing.SystemColors.Window;
            this.dComboBox2.DataName = "PinDIr";
            this.dComboBox2.DataSource = this.PreloadPackageDS;
            this.dComboBox2.DefaultValue = 0;
            this.dComboBox2.FormattingEnabled = true;
            this.dComboBox2.IsModified = false;
            this.dComboBox2.Items.AddRange(new object[] {
            "Top",
            "Bottom",
            "Left",
            "Right"});
            this.dComboBox2.Location = new System.Drawing.Point(162, 395);
            this.dComboBox2.ModifiedColor = System.Drawing.Color.Aqua;
            this.dComboBox2.Name = "dComboBox2";
            this.dComboBox2.NoChangeInAuto = false;
            this.dComboBox2.Size = new System.Drawing.Size(142, 35);
            this.dComboBox2.TabIndex = 131;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(5, 354);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(157, 35);
            this.panel1.TabIndex = 132;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Clamsheel Dir";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(5, 395);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(157, 35);
            this.panel3.TabIndex = 133;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 27);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pin Dir";
            // 
            // BoardTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 561);
            this.Name = "BoardTable";
            this.Text = "BoardTable";
            this.PackageContainer.Panel1.ResumeLayout(false);
            this.PackageContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PackageContainer)).EndInit();
            this.PackageContainer.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            this.pnlControl.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KCSDK.DFieldEdit dFieldEdit4;
        private KCSDK.DFieldEdit dFieldEdit5;
        private KCSDK.DFieldEdit dFieldEdit1;
        private KCSDK.DFieldEdit dFieldEdit3;
        private KCSDK.DFieldEdit dFieldEdit16;
        private KCSDK.DFieldEdit dFieldEdit13;
        private KCSDK.DFieldEdit dFieldEdit11;
        private KCSDK.DFieldEdit dFieldEdit2;
        private KCSDK.DFieldEdit dFieldEdit8;
        private KCSDK.DFieldEdit dFieldEdit7;
        private KCSDK.DFieldEdit dFieldEdit12;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private KCSDK.DComboBox dComboBox2;
        private KCSDK.DComboBox dComboBox1;
    }
}