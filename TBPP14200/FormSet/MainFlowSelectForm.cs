using CommonObj;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBPP14200
{
    public enum DialogFormType
    {
        None = 0,
        BinTrayExchange,
        DiePakICCheck,
        DiePakICCheckInTrolley,
        InputDiePakID,
        KitICCheck,
        DiePakICCheckReject,
    }

    public enum DialogFormReturn
    {
        //None = 0,
        //BinTrayExchangeOK,
        RETURN_NONE = 0,
        RETURN_OK,
        RETURN_IGNORE,
        RETURN_RETRY,
        RETURN_CHECK,
    }
    public partial class MainFlowSelectForm : Form
    {

        private DialogFormType mActionType = DialogFormType.None;
        private DialogFormReturn m_ActionResult = DialogFormReturn.RETURN_NONE;
        private string m_InputResult = string.Empty;
        private Button btnOK = null;    //Jay Tsao 2017-09-12 ###Added - MainFlowDialogForm OK 按鈕
        private Button btnRetry = null;    //Jay Tsao 2017-09-12 ###Added - MainFlowDialogForm Retry 按鈕
        private Button btnIgnore = null;    //Jay Tsao 2017-09-12 ###Added - MainFlowDialogForm Ignore 按鈕
        private Button btnCheck = null;    //Jay Tsao 2017-09-12 ###Added - MainFlowDialogForm Ignore 按鈕
        public DialogFormReturn ActionResult { get { return m_ActionResult; } }
        public string InputResult { get { return m_InputResult; } }
        public MainFlowSelectForm()
        {
            InitializeComponent();
            //創建 OK 按鈕
            btnOK = new Button();
            btnOK.Font = new Font("Cambria", 36, FontStyle.Bold);
            btnOK.Dock = DockStyle.Left;
            btnOK.ForeColor = Color.Black;
            btnOK.BackColor = Color.FromArgb(243, 243, 243);
            btnOK.Width = 750;
            btnOK.Height = 138;
            btnOK.Text = "OK";
            btnOK.Tag = DialogFormReturn.RETURN_OK;
            btnOK.Click += new System.EventHandler(ButtonClick);

            //創建 Retry 按鈕
            btnRetry = new Button();
            btnRetry.Font = new Font("Cambria", 36, FontStyle.Bold);
            btnRetry.Dock = DockStyle.Left;
            btnRetry.ForeColor = Color.Black;
            btnRetry.BackColor = Color.FromArgb(243, 243, 243);
            btnRetry.Width = 375;
            btnRetry.Height = 138;
            btnRetry.Text = "Retry";
            btnRetry.Tag = DialogFormReturn.RETURN_RETRY;
            btnRetry.Click += new System.EventHandler(ButtonClick);

            //創建 Ignore 按鈕
            btnIgnore = new Button();
            btnIgnore.Font = new Font("Cambria", 36, FontStyle.Bold);
            btnIgnore.Dock = DockStyle.Left;
            btnIgnore.ForeColor = Color.Red;
            btnIgnore.BackColor = Color.FromArgb(243, 243, 243);
            btnIgnore.Width = 375;
            btnIgnore.Height = 138;
            btnIgnore.Text = "Ignore";
            btnIgnore.Tag = DialogFormReturn.RETURN_IGNORE;
            btnIgnore.Click += new System.EventHandler(ButtonClick);

            btnCheck = new Button();
            btnCheck.Font = new Font("Cambria", 36, FontStyle.Bold);
            btnCheck.Dock = DockStyle.Left;
            btnCheck.ForeColor = Color.Red;
            btnCheck.BackColor = Color.FromArgb(243, 243, 243);
            btnCheck.Width = 375;
            btnCheck.Height = 138;
            btnCheck.Text = "Check";
            btnCheck.Tag = DialogFormReturn.RETURN_CHECK;
            btnCheck.Click += new System.EventHandler(ButtonClick);
            
        }
        //Jay Tsao 2017-09-12 ###Added - 以委派的方式在主畫面切換使用者權限時，設定 MainFlowDialogForm 的 Ignore 按鈕顯示與否
        public void RefreshDialog()
        {
            if (mActionType.Equals(DialogFormType.DiePakICCheckReject) || mActionType.Equals(DialogFormType.DiePakICCheck) || mActionType.Equals(DialogFormType.KitICCheck))
            {
               // if (SYSPara.LoginUser >= UserType.utEngineer)
                {
                    btnRetry.Width = 250;
                    btnIgnore.Width = 250;
                    btnCheck.Width = 250;
                    btnIgnore.Visible = true;
                    //if (SYSPara.ProductionLinkMode.Equals(LinkMode.AHER) || mActionType.Equals(DialogFormType.KitICCheck))
                    if (true)
                    {
                        btnCheck.Visible = true;
                    }
                    else
                    {
                        btnCheck.Visible = false;
                    }
                }
                //else
                //{
                //    btnRetry.Width = 375;
                //    btnCheck.Width = 375;
                //    btnIgnore.Width = 0;
                //    btnIgnore.Visible = false;
                //    btnCheck.Visible = false;
                //}
            }
        }
        public void ShowDialogForm(DialogFormType type, string message)
        {
            mActionType = type;
            if (mActionType.Equals(DialogFormType.None))
            {
                return;
            }

            m_ActionResult = DialogFormReturn.RETURN_NONE;
            m_InputResult = string.Empty;
            lb_Message.Text = message;

            pnl_BtnContainer.Controls.Clear();
            switch (mActionType)
            {
                case DialogFormType.BinTrayExchange:
                    {
                        pnl_BtnContainer.Controls.Add(btnOK);

                        //If bin tray has been replaced, please click here to continue.
                        //OK Button
                        //AddButton("OK", DialogFormReturn.RETURN_OK, 750, Color.Black);
                    }
                    break;
                case DialogFormType.DiePakICCheck:
                    {
                        RefreshDialog();
                        pnl_BtnContainer.Controls.Add(btnIgnore);
                        pnl_BtnContainer.Controls.Add(btnRetry);
                        //pnl_BtnContainer.Controls.Add(btnCheck);    //2021-04-24 Jay Tsao Added.
                    }
                    break;
                case DialogFormType.DiePakICCheckReject:
                    {
                        RefreshDialog();
                        pnl_BtnContainer.Controls.Add(btnIgnore);
                        pnl_BtnContainer.Controls.Add(btnRetry);
                        pnl_BtnContainer.Controls.Add(btnCheck);    //2021-04-24 Jay Tsao Added.
                    }
                    break;
                case DialogFormType.KitICCheck:
                    {
                        RefreshDialog();
                        pnl_BtnContainer.Controls.Add(btnIgnore);
                        pnl_BtnContainer.Controls.Add(btnRetry);
                        pnl_BtnContainer.Controls.Add(btnCheck);
                    }
                    break;
                case DialogFormType.DiePakICCheckInTrolley: //2021-04-24 Jay Tsao Added.
                    {
                        pnl_BtnContainer.Controls.Add(btnOK);
                    }
                    break;
            }

            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowDialog();
        }

        private void ButtonClick(object sender, System.EventArgs e)
        {
            object obj = ((Button)sender).Tag;
            m_ActionResult = (DialogFormReturn)obj;

            //Jay Tsao 2017-09-03 ###Changed - 移除於產生按鈕時判斷使用者權限，改於按下按鈕時做判斷
            if (m_ActionResult.Equals(DialogFormReturn.RETURN_IGNORE))
            {
                //if (SYSPara.LoginUser <= UserType.utOperator)
                //{
                //    m_ActionResult = DialogFormReturn.RETURN_NONE;
                //    MessageBox.Show("You do not have permission to ignore!", "No Permission", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
            }
            mActionType = DialogFormType.None;
            SYSPara.LogSay(EnLoggerType.EnLog_OP, string.Format("MainFlowSelectForm:User Click-[{0}]", m_ActionResult.ToString()));
            Hide();
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                string sRet = ((TextBox)sender).Text;
                if (!string.IsNullOrEmpty(sRet) && !string.IsNullOrWhiteSpace(sRet))
                {
                    m_InputResult = sRet;
                    m_ActionResult = DialogFormReturn.RETURN_OK;
                    mActionType = DialogFormType.None;
                    SYSPara.LogSay(EnLoggerType.EnLog_OP, string.Format("MainFlowSelectForm:User enter-[{0}]", m_InputResult.ToString()));
                    Hide();
                }
            }
        }

        private void MainFlowDialogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_ActionResult.Equals(DialogFormReturn.RETURN_NONE))
            {
                e.Cancel = true;
            }
        }

        private void MainFlowSelectForm_Load(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void lb_Message_Click(object sender, EventArgs e)
        {

        }
    }
}
