using System.Windows.Forms;

namespace HDT
{
    public partial class PPFailureActionSelectForm : Form
    {
        const int BTN_W = 250;
        //const int BTN_H = 138;

        private PP_FAILURE_ACTION_TYPE BtnResult = PP_FAILURE_ACTION_TYPE.ppNone;

        public PPFailureActionSelectForm()
        {
            InitializeComponent();
        }

        public void ShowPPFASelectForm(PP_FAILURE_ACTION_TYPE type)
        {
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
                    //btnRetry.Width = BTN_W;
                    //btnRetry.Height = BTN_H;
                    //btnRetry.Left = (12 + BtnCount * (BTN_W + 6));
                    btnRetry.Visible = true;
                    BtnCount++;
                }
                if ((type & PP_FAILURE_ACTION_TYPE.ppIgnore) == PP_FAILURE_ACTION_TYPE.ppIgnore)
                {
                    //顯示包含 Ignore 按鈕
                    //btnIgnore.Width = BTN_W;
                    //btnIgnore.Height = BTN_H;
                    //btnIgnore.Left = (12 + BtnCount * (BTN_W + 6));
                    btnIgnore.Visible = true;
                    BtnCount++;
                }
                if ((type & PP_FAILURE_ACTION_TYPE.ppSearch) == PP_FAILURE_ACTION_TYPE.ppSearch)
                {
                    //顯示包含 Search 按鈕
                    //btnSearch.Width = BTN_W;
                    //btnSearch.Height = BTN_H;
                    //btnSearch.Left = (12 + BtnCount * (BTN_W + 6));
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

        private void button1_Click(object sender, System.EventArgs e)
        {
            BtnResult = PP_FAILURE_ACTION_TYPE.ppRetry;
            Hide();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            BtnResult = PP_FAILURE_ACTION_TYPE.ppIgnore;
            Hide();
        }

        private void button3_Click(object sender, System.EventArgs e)
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
