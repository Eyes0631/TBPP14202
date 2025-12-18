namespace TBPP14200
{
    partial class OptionF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionF));
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.pnlButton = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel2 = new System.Windows.Forms.Button();
            this.btnSave2 = new System.Windows.Forms.Button();
            this.btnEdit2 = new System.Windows.Forms.Button();
            this.OptionDS = new KCSDK.DataManagement(this.components);
            this.dFieldEdit2 = new KCSDK.DFieldEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dCheckBox4 = new KCSDK.DCheckBox(this.components);
            this.dCheckBox3 = new KCSDK.DCheckBox(this.components);
            this.dCheckBox2 = new KCSDK.DCheckBox(this.components);
            this.dCheckBox1 = new KCSDK.DCheckBox(this.components);
            this.dCheckBox5 = new KCSDK.DCheckBox(this.components);
            this.pnlButton.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.White;
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
            // pnlButton
            // 
            this.pnlButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlButton.Controls.Add(this.btnClose);
            this.pnlButton.Controls.Add(this.label1);
            this.pnlButton.Controls.Add(this.btnCancel2);
            this.pnlButton.Controls.Add(this.btnSave2);
            this.pnlButton.Controls.Add(this.btnEdit2);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButton.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.pnlButton.Location = new System.Drawing.Point(0, 0);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(1229, 58);
            this.pnlButton.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.ImageKey = "closebtn.png";
            this.btnClose.ImageList = this.imgList;
            this.btnClose.Location = new System.Drawing.Point(550, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 50);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "結束";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 56);
            this.label1.TabIndex = 3;
            this.label1.Text = "通用設定";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel2
            // 
            this.btnCancel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel2.ImageKey = "cancel.png";
            this.btnCancel2.ImageList = this.imgList;
            this.btnCancel2.Location = new System.Drawing.Point(444, 3);
            this.btnCancel2.Name = "btnCancel2";
            this.btnCancel2.Size = new System.Drawing.Size(100, 50);
            this.btnCancel2.TabIndex = 2;
            this.btnCancel2.Text = "取消";
            this.btnCancel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel2.UseVisualStyleBackColor = true;
            this.btnCancel2.Click += new System.EventHandler(this.btnCancel2_Click);
            // 
            // btnSave2
            // 
            this.btnSave2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave2.ImageKey = "save.png";
            this.btnSave2.ImageList = this.imgList;
            this.btnSave2.Location = new System.Drawing.Point(338, 3);
            this.btnSave2.Name = "btnSave2";
            this.btnSave2.Size = new System.Drawing.Size(100, 50);
            this.btnSave2.TabIndex = 1;
            this.btnSave2.Text = "儲存";
            this.btnSave2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave2.UseVisualStyleBackColor = true;
            this.btnSave2.Click += new System.EventHandler(this.btnSave2_Click);
            // 
            // btnEdit2
            // 
            this.btnEdit2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit2.ImageKey = "edit.png";
            this.btnEdit2.ImageList = this.imgList;
            this.btnEdit2.Location = new System.Drawing.Point(232, 3);
            this.btnEdit2.Name = "btnEdit2";
            this.btnEdit2.Size = new System.Drawing.Size(100, 50);
            this.btnEdit2.TabIndex = 0;
            this.btnEdit2.Text = "編輯";
            this.btnEdit2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit2.UseVisualStyleBackColor = true;
            this.btnEdit2.Click += new System.EventHandler(this.btnEdit2_Click);
            // 
            // OptionDS
            // 
            this.OptionDS.ModifiedLog = true;
            this.OptionDS.ModifiedLogToDB = true;
            // 
            // dFieldEdit2
            // 
            this.dFieldEdit2.AutoFocus = false;
            this.dFieldEdit2.Caption = "機台速率";
            this.dFieldEdit2.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit2.CaptionFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit2.DataName = "機台速率";
            this.dFieldEdit2.DataSource = this.OptionDS;
            this.dFieldEdit2.DefaultValue = null;
            this.dFieldEdit2.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit2.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit2.EditWidth = 100;
            this.dFieldEdit2.FieldValue = "0";
            this.dFieldEdit2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit2.IsModified = false;
            this.dFieldEdit2.Location = new System.Drawing.Point(34, 75);
            this.dFieldEdit2.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit2.MaxValue = 100D;
            this.dFieldEdit2.MinValue = 10D;
            this.dFieldEdit2.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit2.Name = "dFieldEdit2";
            this.dFieldEdit2.NoChangeInAuto = false;
            this.dFieldEdit2.Size = new System.Drawing.Size(309, 29);
            this.dFieldEdit2.StepValue = 0D;
            this.dFieldEdit2.TabIndex = 7;
            this.dFieldEdit2.Unit = "%";
            this.dFieldEdit2.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit2.UnitWidth = 40;
            this.dFieldEdit2.ValueType = KCSDK.ValueDataType.Int;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dCheckBox4);
            this.groupBox1.Controls.Add(this.dCheckBox3);
            this.groupBox1.Controls.Add(this.dCheckBox2);
            this.groupBox1.Controls.Add(this.dCheckBox1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(34, 147);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 170);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dry Run Setting";
            // 
            // dCheckBox4
            // 
            this.dCheckBox4.AutoSize = true;
            this.dCheckBox4.BackColor = System.Drawing.SystemColors.Control;
            this.dCheckBox4.DataName = "NoUseBoardID";
            this.dCheckBox4.DataSource = this.OptionDS;
            this.dCheckBox4.DefaultValue = false;
            this.dCheckBox4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dCheckBox4.IsModified = false;
            this.dCheckBox4.Location = new System.Drawing.Point(31, 128);
            this.dCheckBox4.ModifiedColor = System.Drawing.Color.Aqua;
            this.dCheckBox4.Name = "dCheckBox4";
            this.dCheckBox4.NoChangeInAuto = true;
            this.dCheckBox4.Size = new System.Drawing.Size(153, 24);
            this.dCheckBox4.TabIndex = 14;
            this.dCheckBox4.Text = "No Use Board ID";
            this.dCheckBox4.UseVisualStyleBackColor = true;
            // 
            // dCheckBox3
            // 
            this.dCheckBox3.AutoSize = true;
            this.dCheckBox3.BackColor = System.Drawing.SystemColors.Control;
            this.dCheckBox3.DataName = "DryRun_NoDevice";
            this.dCheckBox3.DataSource = this.OptionDS;
            this.dCheckBox3.DefaultValue = false;
            this.dCheckBox3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dCheckBox3.IsModified = false;
            this.dCheckBox3.Location = new System.Drawing.Point(49, 88);
            this.dCheckBox3.ModifiedColor = System.Drawing.Color.Aqua;
            this.dCheckBox3.Name = "dCheckBox3";
            this.dCheckBox3.NoChangeInAuto = true;
            this.dCheckBox3.Size = new System.Drawing.Size(175, 24);
            this.dCheckBox3.TabIndex = 9;
            this.dCheckBox3.Text = "No Device Free Run";
            this.dCheckBox3.UseVisualStyleBackColor = true;
            // 
            // dCheckBox2
            // 
            this.dCheckBox2.AutoSize = true;
            this.dCheckBox2.BackColor = System.Drawing.SystemColors.Control;
            this.dCheckBox2.DataName = "DryRun_NoBoard";
            this.dCheckBox2.DataSource = this.OptionDS;
            this.dCheckBox2.DefaultValue = false;
            this.dCheckBox2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dCheckBox2.IsModified = false;
            this.dCheckBox2.Location = new System.Drawing.Point(49, 58);
            this.dCheckBox2.ModifiedColor = System.Drawing.Color.Aqua;
            this.dCheckBox2.Name = "dCheckBox2";
            this.dCheckBox2.NoChangeInAuto = true;
            this.dCheckBox2.Size = new System.Drawing.Size(170, 24);
            this.dCheckBox2.TabIndex = 8;
            this.dCheckBox2.Text = "No Board Free Run";
            this.dCheckBox2.UseVisualStyleBackColor = true;
            // 
            // dCheckBox1
            // 
            this.dCheckBox1.AutoSize = true;
            this.dCheckBox1.BackColor = System.Drawing.SystemColors.Control;
            this.dCheckBox1.DataName = "DryRun";
            this.dCheckBox1.DataSource = this.OptionDS;
            this.dCheckBox1.DefaultValue = false;
            this.dCheckBox1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dCheckBox1.IsModified = false;
            this.dCheckBox1.Location = new System.Drawing.Point(31, 28);
            this.dCheckBox1.ModifiedColor = System.Drawing.Color.Aqua;
            this.dCheckBox1.Name = "dCheckBox1";
            this.dCheckBox1.NoChangeInAuto = true;
            this.dCheckBox1.Size = new System.Drawing.Size(88, 24);
            this.dCheckBox1.TabIndex = 7;
            this.dCheckBox1.Text = "Dry Run";
            this.dCheckBox1.UseVisualStyleBackColor = true;
            // 
            // dCheckBox5
            // 
            this.dCheckBox5.AutoSize = true;
            this.dCheckBox5.BackColor = System.Drawing.SystemColors.Control;
            this.dCheckBox5.DataName = "UseBoardIDKeyin";
            this.dCheckBox5.DataSource = this.OptionDS;
            this.dCheckBox5.DefaultValue = false;
            this.dCheckBox5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dCheckBox5.IsModified = false;
            this.dCheckBox5.Location = new System.Drawing.Point(65, 323);
            this.dCheckBox5.ModifiedColor = System.Drawing.Color.Aqua;
            this.dCheckBox5.Name = "dCheckBox5";
            this.dCheckBox5.NoChangeInAuto = true;
            this.dCheckBox5.Size = new System.Drawing.Size(171, 24);
            this.dCheckBox5.TabIndex = 15;
            this.dCheckBox5.Text = "Use Board ID Keyin";
            this.dCheckBox5.UseVisualStyleBackColor = true;
            // 
            // OptionF
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.ClientSize = new System.Drawing.Size(1229, 730);
            this.Controls.Add(this.dCheckBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlButton);
            this.Controls.Add(this.dFieldEdit2);
            this.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "OptionF";
            this.Text = "OptionF";
            this.pnlButton.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.Button btnCancel2;
        private System.Windows.Forms.Button btnSave2;
        private System.Windows.Forms.Button btnEdit2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        public KCSDK.DataManagement OptionDS;
        private KCSDK.DFieldEdit dFieldEdit2;
        private System.Windows.Forms.GroupBox groupBox1;
        private KCSDK.DCheckBox dCheckBox3;
        private KCSDK.DCheckBox dCheckBox2;
        private KCSDK.DCheckBox dCheckBox1;
        private KCSDK.DCheckBox dCheckBox4;
        private KCSDK.DCheckBox dCheckBox5;
    }
}