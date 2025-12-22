namespace TBPP14200
{
    partial class CassetteTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CassetteTable));
            this.dFieldEdit35 = new KCSDK.DFieldEdit();
            this.dFieldEdit36 = new KCSDK.DFieldEdit();
            this.dFieldEdit1 = new KCSDK.DFieldEdit();
            this.dFieldEdit2 = new KCSDK.DFieldEdit();
            ((System.ComponentModel.ISupportInitialize)(this.PackageContainer)).BeginInit();
            this.PackageContainer.Panel1.SuspendLayout();
            this.PackageContainer.Panel2.SuspendLayout();
            this.PackageContainer.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.pnlControl.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.pnlControl.Controls.Add(this.dFieldEdit2);
            this.pnlControl.Controls.Add(this.dFieldEdit1);
            this.pnlControl.Controls.Add(this.dFieldEdit35);
            this.pnlControl.Controls.Add(this.dFieldEdit36);
            // 
            // dFieldEdit35
            // 
            this.dFieldEdit35.AutoFocus = false;
            this.dFieldEdit35.Caption = "Cassette Num";
            this.dFieldEdit35.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit35.CaptionFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit35.DataName = "Cassette_Num";
            this.dFieldEdit35.DataSource = this.PreloadPackageDS;
            this.dFieldEdit35.DefaultValue = "0";
            this.dFieldEdit35.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit35.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit35.EditWidth = 120;
            this.dFieldEdit35.FieldValue = "";
            this.dFieldEdit35.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit35.IsModified = false;
            this.dFieldEdit35.Location = new System.Drawing.Point(15, 37);
            this.dFieldEdit35.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit35.MaxValue = 9999999D;
            this.dFieldEdit35.MinValue = 0D;
            this.dFieldEdit35.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit35.Name = "dFieldEdit35";
            this.dFieldEdit35.NoChangeInAuto = false;
            this.dFieldEdit35.Size = new System.Drawing.Size(350, 29);
            this.dFieldEdit35.StepValue = 0D;
            this.dFieldEdit35.TabIndex = 131;
            this.dFieldEdit35.Unit = "ea";
            this.dFieldEdit35.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit35.UnitWidth = 40;
            this.dFieldEdit35.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit36
            // 
            this.dFieldEdit36.AutoFocus = false;
            this.dFieldEdit36.Caption = "Cassette Pitch";
            this.dFieldEdit36.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit36.CaptionFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit36.DataName = "Cassette_Pitch";
            this.dFieldEdit36.DataSource = this.PreloadPackageDS;
            this.dFieldEdit36.DefaultValue = "0";
            this.dFieldEdit36.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit36.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit36.EditWidth = 120;
            this.dFieldEdit36.FieldValue = "";
            this.dFieldEdit36.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit36.IsModified = false;
            this.dFieldEdit36.Location = new System.Drawing.Point(15, 8);
            this.dFieldEdit36.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit36.MaxValue = 9999999D;
            this.dFieldEdit36.MinValue = 0D;
            this.dFieldEdit36.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit36.Name = "dFieldEdit36";
            this.dFieldEdit36.NoChangeInAuto = false;
            this.dFieldEdit36.Size = new System.Drawing.Size(350, 29);
            this.dFieldEdit36.StepValue = 0D;
            this.dFieldEdit36.TabIndex = 130;
            this.dFieldEdit36.Unit = "um";
            this.dFieldEdit36.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit36.UnitWidth = 40;
            this.dFieldEdit36.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit1
            // 
            this.dFieldEdit1.AutoFocus = false;
            this.dFieldEdit1.Caption = "Left First Slot Z";
            this.dFieldEdit1.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit1.CaptionFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit1.DataName = "Cassette_LeftFirstSlotZ";
            this.dFieldEdit1.DataSource = this.PreloadPackageDS;
            this.dFieldEdit1.DefaultValue = "0";
            this.dFieldEdit1.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit1.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit1.EditWidth = 120;
            this.dFieldEdit1.FieldValue = "";
            this.dFieldEdit1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit1.IsModified = false;
            this.dFieldEdit1.Location = new System.Drawing.Point(15, 83);
            this.dFieldEdit1.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit1.MaxValue = 9999999D;
            this.dFieldEdit1.MinValue = 0D;
            this.dFieldEdit1.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit1.Name = "dFieldEdit1";
            this.dFieldEdit1.NoChangeInAuto = false;
            this.dFieldEdit1.Size = new System.Drawing.Size(350, 29);
            this.dFieldEdit1.StepValue = 0D;
            this.dFieldEdit1.TabIndex = 132;
            this.dFieldEdit1.Unit = "ea";
            this.dFieldEdit1.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit1.UnitWidth = 40;
            this.dFieldEdit1.ValueType = KCSDK.ValueDataType.Int;
            // 
            // dFieldEdit2
            // 
            this.dFieldEdit2.AutoFocus = false;
            this.dFieldEdit2.Caption = "Right First Slot Z";
            this.dFieldEdit2.CaptionColor = System.Drawing.Color.Black;
            this.dFieldEdit2.CaptionFont = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit2.DataName = "Cassette_RightFirstSlotZ";
            this.dFieldEdit2.DataSource = this.PreloadPackageDS;
            this.dFieldEdit2.DefaultValue = "0";
            this.dFieldEdit2.EditColor = System.Drawing.Color.Black;
            this.dFieldEdit2.EditFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit2.EditWidth = 120;
            this.dFieldEdit2.FieldValue = "";
            this.dFieldEdit2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dFieldEdit2.IsModified = false;
            this.dFieldEdit2.Location = new System.Drawing.Point(379, 83);
            this.dFieldEdit2.Margin = new System.Windows.Forms.Padding(0);
            this.dFieldEdit2.MaxValue = 9999999D;
            this.dFieldEdit2.MinValue = 0D;
            this.dFieldEdit2.ModifiedColor = System.Drawing.Color.Aqua;
            this.dFieldEdit2.Name = "dFieldEdit2";
            this.dFieldEdit2.NoChangeInAuto = false;
            this.dFieldEdit2.Size = new System.Drawing.Size(350, 29);
            this.dFieldEdit2.StepValue = 0D;
            this.dFieldEdit2.TabIndex = 133;
            this.dFieldEdit2.Unit = "ea";
            this.dFieldEdit2.UnitFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.dFieldEdit2.UnitWidth = 40;
            this.dFieldEdit2.ValueType = KCSDK.ValueDataType.Int;
            // 
            // CassetteTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 561);
            this.Name = "CassetteTable";
            this.Text = "CassetteTable";
            this.PackageContainer.Panel1.ResumeLayout(false);
            this.PackageContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PackageContainer)).EndInit();
            this.PackageContainer.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            this.pnlControl.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KCSDK.DFieldEdit dFieldEdit35;
        private KCSDK.DFieldEdit dFieldEdit36;
        private KCSDK.DFieldEdit dFieldEdit2;
        private KCSDK.DFieldEdit dFieldEdit1;
    }
}