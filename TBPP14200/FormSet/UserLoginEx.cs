using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using KCSDK;

namespace TBPP14200
{
    public partial class UserLoginEx : Form
    {
        public string PMUserName = "";
        private bool m_PMSet = false;

        public UserLoginEx()
        {
            InitializeComponent();
            m_PMSet = false;
        }

        public UserLoginEx(bool bPMSet)
        {
            InitializeComponent();
            m_PMSet = bPMSet;

            lbUserPW.Visible = false;
            tbUserPW.Visible = false;
        }

        private void UserLoginEx_Activated(object sender, EventArgs e)
        {
            tbUserID.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string username = tbUserID.Text;
            if (m_PMSet)
            {
                this.DialogResult = DialogResult.OK;
                PMUserName = tbUserID.Text ;
                Close();
            }
            else
            {
                bool bresult = SYSPara.mSecret.CheckPW(username, tbUserPW.Text);
                if (bresult)
                {
                    this.DialogResult = DialogResult.OK;
                    SYSPara.mSecret.LoginUser(username);

                    Close();
                }
                else
                    MessageBox.Show("Error Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }       
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void tbUserID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                tbUserPW.Focus();
            }
        }

        private void tbUserPW_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnOK_Click(sender, e);
            }
        }

        private void UserLoginEx_Load(object sender, EventArgs e)
        {
            tbUserID.Text = "";
            tbUserPW.Text = "";
        }
    }
}
