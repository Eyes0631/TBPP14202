namespace TBPP14200
{
    partial class DeviceTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceTable));
            this.dFieldEdit9 = new KCSDK.DFieldEdit();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dFieldEdit8 = new KCSDK.DFieldEdit();
            this.dFieldEdit1 = new KCSDK.DFieldEdit();
            ((System.ComponentModel.ISupportInitialize)(this.PackageContainer)).BeginInit();
            this.PackageContainer.Panel1.SuspendLayout();
            this.PackageContainer.Panel2.SuspendLayout();
            this.PackageContainer.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.pnlControl.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.pnlControl.Controls.Add(this.dFieldEdit1);
            this.pnlControl.Controls.Add(this.dFieldEdit9);
            this.pnlControl.Controls.Add(this.pictureBox1);
            this.pnlControl.Controls.Add(this.dFieldEdit8);
            // 
            // dFieldEdit9
            // 
            this.dFieldEdit9.AutoFocus = false;
            this.dFieldEdit9.Caption = "Device Width";
            this.dFieldEdit9.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit9.CaptionFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit9.DataName = "Device_Width";
            this.dFieldEdit9.DataSource = this.PreloadPackageDS;
            this.dFieldEdit9.DefaultValue = null;
            this.dFieldEdit9.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit9.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit9.EditWidth = 120;
            this.dFieldEdit9.FieldValue = "";
            this.dFieldEdit9.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit9.IsModified = false;
            this.dFieldEdit9.Location = new System.Drawing.Point(24, 37);
            this.dFieldEdit9.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit9.MaxValue = 9999999D;
            this.dFieldEdit9.MinValue = 0D;
            this.dFieldEdit9.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit9.Name = "dFieldEdit9";
            this.dFieldEdit9.NoChangeInAuto = false;
            this.dFieldEdit9.Size = new System.Drawing.Size(350, 29);
            this.dFieldEdit9.StepValue = 0D;
            this.dFieldEdit9.TabIndex = 117;
            this.dFieldEdit9.Unit = "um";
            this.dFieldEdit9.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit9.UnitWidth = 40;
            this.dFieldEdit9.ValueType = KCSDK.ValueDataType.Int;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(386, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(350, 87);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 116;
            this.pictureBox1.TabStop = false;
            // 
            // dFieldEdit8
            // 
            this.dFieldEdit8.AutoFocus = false;
            this.dFieldEdit8.Caption = "Device Thickness";
            this.dFieldEdit8.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit8.CaptionFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit8.DataName = "Device_Thickness";
            this.dFieldEdit8.DataSource = this.PreloadPackageDS;
            this.dFieldEdit8.DefaultValue = null;
            this.dFieldEdit8.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit8.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit8.EditWidth = 120;
            this.dFieldEdit8.FieldValue = "";
            this.dFieldEdit8.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit8.IsModified = false;
            this.dFieldEdit8.Location = new System.Drawing.Point(24, 8);
            this.dFieldEdit8.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit8.MaxValue = 9999999D;
            this.dFieldEdit8.MinValue = 0D;
            this.dFieldEdit8.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit8.Name = "dFieldEdit8";
            this.dFieldEdit8.NoChangeInAuto = false;
            this.dFieldEdit8.Size = new System.Drawing.Size(350, 29);
            this.dFieldEdit8.StepValue = 0D;
            this.dFieldEdit8.TabIndex = 115;
            this.dFieldEdit8.Unit = "um";
            this.dFieldEdit8.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit8.UnitWidth = 40;
            this.dFieldEdit8.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit1
            // 
            this.dFieldEdit1.AutoFocus = false;
            this.dFieldEdit1.Caption = "Device Length";
            this.dFieldEdit1.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit1.CaptionFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit1.DataName = "Device_Length";
            this.dFieldEdit1.DataSource = this.PreloadPackageDS;
            this.dFieldEdit1.DefaultValue = null;
            this.dFieldEdit1.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit1.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit1.EditWidth = 120;
            this.dFieldEdit1.FieldValue = "";
            this.dFieldEdit1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit1.IsModified = false;
            this.dFieldEdit1.Location = new System.Drawing.Point(24, 66);
            this.dFieldEdit1.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit1.MaxValue = 9999999D;
            this.dFieldEdit1.MinValue = 0D;
            this.dFieldEdit1.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit1.Name = "dFieldEdit1";
            this.dFieldEdit1.NoChangeInAuto = false;
            this.dFieldEdit1.Size = new System.Drawing.Size(350, 29);
            this.dFieldEdit1.StepValue = 0D;
            this.dFieldEdit1.TabIndex = 118;
            this.dFieldEdit1.Unit = "um";
            this.dFieldEdit1.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit1.UnitWidth = 40;
            this.dFieldEdit1.ValueType = KCSDK.ValueDataType.Int;
            // 
            // DeviceTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 561);
            this.Name = "DeviceTable";
            this.Text = "DeviceTable";
            this.PackageContainer.Panel1.ResumeLayout(false);
            this.PackageContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PackageContainer)).EndInit();
            this.PackageContainer.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            this.pnlControl.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KCSDK.DFieldEdit dFieldEdit9;
        private System.Windows.Forms.PictureBox pictureBox1;
        private KCSDK.DFieldEdit dFieldEdit8;
        private KCSDK.DFieldEdit dFieldEdit1;
    }
}