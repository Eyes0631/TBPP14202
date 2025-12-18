using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KCSDK;

namespace TBPP14200
{
    public partial class UserLoginF : Form
    {
        ////目前使用者
        //private Secret.UserObj mCurrentUser = null;
        //public Secret.UserObj CurrentUser { get { return mCurrentUser; } }

        public UserLoginF()
        {
            InitializeComponent();

            cbUserName.Items.Clear();
            foreach (Secret.UserObj user in SYSPara.mSecret.UserList)
                cbUserName.Items.Add(user.Name);

            if (cbUserName.Items.Count > 0)
                cbUserName.SelectedIndex = 0;
        }

        private void cbUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbUserPW.Focus();
        }

        private void UserLoginF_Activated(object sender, EventArgs e)
        {
            tbUserPW.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string username = cbUserName.Items[cbUserName.SelectedIndex].ToString();
            bool bresult = SYSPara.mSecret.CheckPW(username, tbUserPW.Text);
            if (bresult)
            {
                this.DialogResult = DialogResult.OK;
                SYSPara.mSecret.LoginUser(username);
                //mCurrentUser = SYSPara.mSecret.NowUser;

                Close();
            }
            else
                MessageBox.Show("Error Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void tbUserPW_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnOk_Click(sender, e);
            }
        }

        private void tbUserPW_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("" + System.Environment.SystemDirectory + "/osk.exe");
        }

        private void UserLoginF_Load(object sender, EventArgs e)
        {
            if (cbUserName.Items.Count > 0)
                cbUserName.SelectedIndex = 0;
            tbUserPW.Text = "";
        }
    }
}
