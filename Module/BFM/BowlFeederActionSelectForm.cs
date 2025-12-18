using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BFM
{
    public partial class BowlFeederActionSelectForm : Form
    {
        const int BTN_W = 250;

        private BF_FAILURE_ACTION_TYPE BtnResult = BF_FAILURE_ACTION_TYPE.bfNone;

        public BowlFeederActionSelectForm()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        public void ShowBFASelectForm(BF_FAILURE_ACTION_TYPE type, string message)
        {
            if (type != BF_FAILURE_ACTION_TYPE.bfNone)
            {
                BtnResult = BF_FAILURE_ACTION_TYPE.bfNone;
                btnWait.Visible = false;
                btnFinish.Visible = false;
                label1.Text = message;

                int BtnCount = 0;
                if ((type & BF_FAILURE_ACTION_TYPE.bfWait) == BF_FAILURE_ACTION_TYPE.bfWait)
                {
                    //顯示包含 Retry 按鈕
                    btnWait.Visible = true;
                    BtnCount++;
                }
                if ((type & BF_FAILURE_ACTION_TYPE.bfFinish) == BF_FAILURE_ACTION_TYPE.bfFinish)
                {
                    //顯示包含 Ignore 按鈕
                    btnFinish.Visible = true;
                    BtnCount++;
                }

                this.Width = 18 + (BtnCount * BTN_W);
                this.Height = 300;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.ShowDialog();
            }
        }


        public BF_FAILURE_ACTION_TYPE BFFailureActionResult { get { return BtnResult; } }

        private void btnWait_Click(object sender, EventArgs e)
        {
            BtnResult = BF_FAILURE_ACTION_TYPE.bfWait;
            Hide();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            BtnResult = BF_FAILURE_ACTION_TYPE.bfFinish;
            Hide();
        }

        private void BowlFeederActionSelectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BtnResult == BF_FAILURE_ACTION_TYPE.bfNone)
            {
                e.Cancel = true;
            }
        }


    }
}
