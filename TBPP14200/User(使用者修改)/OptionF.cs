using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KCSDK;

namespace TBPP14200
{
    //使用者修改 (設備通用參數放置處)
    public partial class OptionF : Form
    {
        public OptionF()
        {
            InitializeComponent();

            #region 架構使用

            this.TopLevel = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            RefreshAllData();

            #endregion
        }

        public void RefreshAllData()
        {            
            OptionDS.Initial(SYSPara.SysDir + @"\LocalData\MachineSet.xml", "SYS");
            DataSnippet.DataGetAll(this);
            DataSnippet.ControlEnable(this, false);
        }

        public void Initial()
        {
        }

        ////使用者權限切換後的頁面變化控制
        //public void ChangeUser()
        //{
        //    switch (SYSPara.LoginUser)
        //    {
        //        case UserType.utNone:
        //            break;
        //        case UserType.utOperator:
        //            break;
        //        case UserType.utEngineer:
        //            break;
        //        case UserType.utAdministrator:
        //            break;
        //    }
        //}

        #region 架構使用

        public bool IsLogExist()
        {
            return OptionDS.IsLogExist();
        }

        public string GetLog()
        {
            return OptionDS.GetLog();
        }

        public void ExitClick()
        {
            DataSnippet.ControlEnable(this, false);
            Parent = null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            SYSPara.LogSay(EnLoggerType.EnLog_OP, "User Click「Finish」-[Option]");

            DataSnippet.ControlEnable(this, false);
            Parent = null;
        }

        private void btnEdit2_Click(object sender, EventArgs e)
        {
            SYSPara.LogSay(EnLoggerType.EnLog_OP, "User Click「Edit」-[Option]");
            DataSnippet.ControlEnable(this, true, SYSPara.IsAutoMode);
        }

        private void btnSave2_Click(object sender, EventArgs e)
        {
            SYSPara.LogSay(EnLoggerType.EnLog_OP, "User Click「Save」-[Option]");

            DataSnippet.ControlEnable(this, false);
            DataSnippet.DataSetAll(this);
            OptionDS.SaveFile();
        }

        private void btnCancel2_Click(object sender, EventArgs e)
        {
            SYSPara.LogSay(EnLoggerType.EnLog_OP, "User Click「Cancel」-[Option]");

            DataSnippet.DataGetAll(this);
            DataSnippet.ControlEnable(this, false);
        }

        //關聯式子表格相關設定
        public void InitialSubPackage()
        {
            #region 使用者修改 (關聯式資料庫，將相關表格加入這個主表格內)
            //tFieldCB1.EditForm = FormSet.mToolF;
            //FormSet.mToolF.Tag = this;
            #endregion
        }

        #endregion
    }
}
