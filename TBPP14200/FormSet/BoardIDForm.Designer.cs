namespace TBPP14200
{
    partial class BoardIDForm
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.text_BoardID = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.text_BoardID);
            this.panel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(735, 59);
            this.panel2.TabIndex = 3;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(310, 349);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(230, 40);
            this.button4.TabIndex = 16;
            this.button4.Text = "開批(Lot Start)";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Khaki;
            this.textBox2.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox2.Enabled = false;
            this.textBox2.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox2.ForeColor = System.Drawing.Color.Black;
            this.textBox2.Location = new System.Drawing.Point(10, 7);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.MaxLength = 0;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(165, 39);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "BoardID";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // text_BoardID
            // 
            this.text_BoardID.Cursor = System.Windows.Forms.Cursors.Default;
            this.text_BoardID.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.text_BoardID.Location = new System.Drawing.Point(172, 7);
            this.text_BoardID.Margin = new System.Windows.Forms.Padding(2);
            this.text_BoardID.Name = "text_BoardID";
            this.text_BoardID.Size = new System.Drawing.Size(558, 39);
            this.text_BoardID.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Default;
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.Font = new System.Drawing.Font("Calibri", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(735, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(176, 66);
            this.button1.TabIndex = 4;
            this.button1.Text = "LOAD";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BoardIDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 59);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Name = "BoardIDForm";
            this.Text = "BoardIDForm";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox text_BoardID;
        private System.Windows.Forms.Button button1;

    }
}