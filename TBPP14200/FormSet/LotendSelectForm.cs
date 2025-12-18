using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TBPP14200
{
    public partial class LotendSelectForm : Form
    {
        public LotendSelectForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SYSPara.LogSay(EnLoggerType.EnLog_OP, "User Click「Lot End」");

            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SYSPara.LogSay(EnLoggerType.EnLog_OP, "User Click「Enforce End」");

            this.DialogResult = DialogResult.Abort;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SYSPara.LogSay(EnLoggerType.EnLog_OP, "User Click「Cancel」");

            this.DialogResult = DialogResult.Cancel;
        }
    }
}
