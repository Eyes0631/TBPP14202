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
    //使用者修改 (產品設定參數放置處)
    public partial class PackageExF : BasePackageExF
    {
        public PackageExF()
        {
            InitializeComponent();

            //定義表格名稱，會影響Log記錄內的DataSource名稱
            ModuleName = "Package";

            FolderPath = SYSPara.SysDir + @"\LocalData\Package\";
        }

        public override void InitialSubPackage()
        {
            #region 使用者修改 (關聯式資料庫，將相關表格加入這個主表格內)
            //tFieldCB1.EditForm = FormSet.mToolF;
            //FormSet.mToolF.Tag = this;
            //if (SYSPara.IsAutoMode)
            //    FormSet.mToolF.SetFileName(SYSPara.PReadValue("Tool").ToString(), "", true);
            #endregion
        }
    }
}
