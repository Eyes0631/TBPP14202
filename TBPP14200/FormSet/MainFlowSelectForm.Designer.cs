namespace TBPP14200
{
    partial class MainFlowSelectForm
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
            this.lb_Message = new System.Windows.Forms.Label();
            this.pnl_BtnContainer = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_Message
            // 
            this.lb_Message.BackColor = System.Drawing.Color.Black;
            this.lb_Message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_Message.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Message.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Message.ForeColor = System.Drawing.Color.Red;
            this.lb_Message.Location = new System.Drawing.Point(121, 0);
            this.lb_Message.Name = "lb_Message";
            this.lb_Message.Size = new System.Drawing.Size(620, 126);
            this.lb_Message.TabIndex = 10;
            this.lb_Message.Text = "Some Messages ...";
            this.lb_Message.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lb_Message.Click += new System.EventHandler(this.lb_Message_Click);
            // 
            // pnl_BtnContainer
            // 
            this.pnl_BtnContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_BtnContainer.Location = new System.Drawing.Point(0, 126);
            this.pnl_BtnContainer.Name = "pnl_BtnContainer";
            this.pnl_BtnContainer.Size = new System.Drawing.Size(741, 140);
            this.pnl_BtnContainer.TabIndex = 8;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(121, 126);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // MainFlowSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 266);
            this.ControlBox = false;
            this.Controls.Add(this.lb_Message);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pnl_BtnContainer);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainFlowSelectForm";
            this.Text = "MainFlowSelectForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainFlowSelectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lb_Message;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnl_BtnContainer;

    }
}