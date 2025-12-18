using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProVIFM;

namespace TBPP14200
{
    public partial class InitialForm : Form
    {
        #region 使用者修改 (定義各模組指標)

        //private BaseModuleInterface mTray = null;
        private BaseModuleInterface mTRM = null;
        private BaseModuleInterface mCHM = null;
        private BaseModuleInterface mBSM = null;
        private BaseModuleInterface mHDT = null;
        private BaseModuleInterface mKSM = null;
        private BaseModuleInterface mBFM = null;

        #endregion

        public InitialForm()
        {
            InitializeComponent();
            TopLevel = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            #region 使用者修改 (取得各模組指標)

            //mTray = (BaseModuleInterface)FormSet.mMSS.GetModule("Tray");
            mTRM = (BaseModuleInterface)FormSet.mMSS.GetModule("TRM");
            mCHM = (BaseModuleInterface)FormSet.mMSS.GetModule("CHM");
            mBSM  = (BaseModuleInterface)FormSet.mMSS.GetModule("BSM");
            mHDT = (BaseModuleInterface)FormSet.mMSS.GetModule("HDT");
            mKSM  = (BaseModuleInterface)FormSet.mMSS.GetModule("KSM");
            mBFM = (BaseModuleInterface)FormSet.mMSS.GetModule("BFM");
            #endregion
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SYSPara.LogSay(EnLoggerType.EnLog_OP, "User Click「Homing cancel」");

            tmRefresh.Enabled = false;
            SYSPara.SysState = StateMode.SM_ABORT;
            Parent = null;
        }

        private void InitialForm_Load(object sender, EventArgs e)
        {
            btnCancel.Top = 0;
            btnCancel.Left = FormSet.mInitialF.Width - FormSet.mInitialF.btnCancel.Width;
        }

        public void Reset()
        {
            tmRefresh.Enabled = true;
        }

        private void tmRefresh_Tick(object sender, EventArgs e)
        {
            tmRefresh.Enabled = false;

            #region 使用者修改 (各模組歸零狀態的顯示)

            //if (mTray != null)
            //    lbTray.Value = mTray.mHomeOk;
            //else
            //    lbTray.BackColor = Color.Red;

            if (mTRM != null) lb_TRM.Value = mTRM.mHomeOk; 
            else lb_TRM.BackColor = Color.Red;
            if (mCHM != null) lbChM.Value = mCHM.mHomeOk;
            else lbChM.BackColor = Color.Red;
            if (mBSM != null) lbBSM.Value = mBSM.mHomeOk; 
            else lbBSM.BackColor = Color.Red;
            if (mHDT != null) lbHDT.Value = mHDT.mHomeOk;
            else lbHDT.BackColor = Color.Red;
            if (mKSM != null) lbKSM.Value = mKSM.mHomeOk;
            else lbKSM.BackColor = Color.Red;
            if (mBFM != null) lbBFM.Value = mBFM.mHomeOk;
            else lbBFM.BackColor = Color.Red;

            #endregion

            tmRefresh.Enabled = true;
        }
    }
}
