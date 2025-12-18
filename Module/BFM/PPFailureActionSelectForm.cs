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
    public partial class PPFailureActionSelectForm : Form
    {
        const int BTN_W = 250;

        private PP_FAILURE_ACTION_TYPE BtnResult = PP_FAILURE_ACTION_TYPE.ppNone;

        public PPFailureActionSelectForm()
        {
            InitializeComponent();
        }

        public void ShowPPFASelectForm(PP_FAILURE_ACTION_TYPE type)
        {
            this.TopLevel = true;
            if (type != PP_FAILURE_ACTION_TYPE.ppNone)
            {
                BtnResult = PP_FAILURE_ACTION_TYPE.ppNone;
                btnRetry.Visible = false;
                btnIgnore.Visible = false;
                btnSearch.Visible = false;

                int BtnCount = 0;
                if ((type & PP_FAILURE_ACTION_TYPE.ppRetry) == PP_FAILURE_ACTION_TYPE.ppRetry)
                {
                    //顯示包含 Retry 按鈕
                    btnRetry.Visible = true;
                    BtnCount++;
                }
                if ((type & PP_FAILURE_ACTION_TYPE.ppIgnore) == PP_FAILURE_ACTION_TYPE.ppIgnore)
                {
                    //顯示包含 Ignore 按鈕
                    btnIgnore.Visible = true;
                    BtnCount++;
                }
                if ((type & PP_FAILURE_ACTION_TYPE.ppSearch) == PP_FAILURE_ACTION_TYPE.ppSearch)
                {
                    //顯示包含 Search 按鈕
                    btnSearch.Visible = true;
                    BtnCount++;
                }
                this.Width = 18 + (BtnCount * BTN_W);
                this.Height = 300;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.ShowDialog();
            }
        }

        public void ShowPPFASelectForm(PP_FAILURE_ACTION_TYPE type, string message)
        {
            if (type != PP_FAILURE_ACTION_TYPE.ppNone)
            {
                BtnResult = PP_FAILURE_ACTION_TYPE.ppNone;
                btnRetry.Visible = false;
                btnIgnore.Visible = false;
                btnSearch.Visible = false;
                label1.Text = message;

                int BtnCount = 0;
                if ((type & PP_FAILURE_ACTION_TYPE.ppRetry) == PP_FAILURE_ACTION_TYPE.ppRetry)
                {
                    //顯示包含 Retry 按鈕
                    btnRetry.Visible = true;
                    BtnCount++;
                }
                if ((type & PP_FAILURE_ACTION_TYPE.ppIgnore) == PP_FAILURE_ACTION_TYPE.ppIgnore)
                {
                    //顯示包含 Ignore 按鈕
                    btnIgnore.Visible = true;
                    BtnCount++;
                }
                if ((type & PP_FAILURE_ACTION_TYPE.ppSearch) == PP_FAILURE_ACTION_TYPE.ppSearch)
                {
                    //顯示包含 Search 按鈕
                    btnSearch.Visible = true;
                    BtnCount++;
                }
                this.Width = 18 + (BtnCount * BTN_W);
                this.Height = 300;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.ShowDialog();
            }
        }

        public PP_FAILURE_ACTION_TYPE PPFailureActionResult { get { return BtnResult; } }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            BtnResult = PP_FAILURE_ACTION_TYPE.ppRetry;
            Hide();
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {
            BtnResult = PP_FAILURE_ACTION_TYPE.ppIgnore;
            Hide();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BtnResult = PP_FAILURE_ACTION_TYPE.ppSearch;
            Hide();
        }

        private void PPFailureActionSelectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BtnResult == PP_FAILURE_ACTION_TYPE.ppNone)
            {
                e.Cancel = true;
            }
        }

    }
}
