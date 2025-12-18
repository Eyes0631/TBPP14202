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
using ProVTool;
using ProVIFM;

namespace TBPP14200
{
    //使用者修改 (產品設定參數放置處)
    public partial class PackageF : BasePackageF
    {
        public PackageF()
        {
            InitializeComponent();

            //定義表格名稱，會影響Log記錄內的DataSource名稱
            ModuleName = "Package";

            FolderPath = SYSPara.SysDir + @"\LocalData\Package\";
        }

        #region 架構使用 (Public)

        //產品切換後觸發此事件
        public override void SelectChange()
        {
        }

        //產品儲存後觸發此事件
        public override void AfterSave()
        {
        }

        //更新 TFieldCB 顯示連結按鈕於開啟, 儲存, 取消事件
        public override void RefreshFormObjs() //Jimmy Add
        {
            tBoard.HasEdit = true;
            tDevice.HasEdit = true;
            tTransferKit.HasEdit = true;
            
            if (SYSPara.IsAutoMode)
            {
                //指向對應項目
                FormSet.mBoardTable.FileName = SYSPara.PReadValue("BoardTable").ToString();
                FormSet.mDeviceTable.FileName = SYSPara.PReadValue("DeviceTable").ToString();
                FormSet.mKitTable.FileName = SYSPara.PReadValue("KitTable").ToString();
            }
            else
            {
                if (FormSet.mPackage != null)
                {
                    FormSet.mPackage.SetFileName(FileName, "", false); //建立連結才能讀取Package 的內容

                    string subpkg1 = SYSPara.PReadValue("BoardTable").ToString();
                    if (string.IsNullOrEmpty(subpkg1))
                    {
                        FormSet.mBoardTable.FileName_Auto = "";
                        FormSet.mBoardTable.FolderName_Auto = "";
                        FormSet.mBoardTable.AutoEnd();
                    }
                    else
                    {
                        FormSet.mBoardTable.SetFileName(subpkg1, "", true); //對應子項目顯示明顯標記
                    }

                    string subpkg2 = SYSPara.PReadValue("DeviceTable").ToString();
                    if (string.IsNullOrEmpty(subpkg2))
                    {
                        FormSet.mDeviceTable.FileName_Auto = "";
                        FormSet.mDeviceTable.FolderName_Auto = "";
                        FormSet.mDeviceTable.AutoEnd();
                    }
                    else
                    {
                        FormSet.mDeviceTable.SetFileName(subpkg2, "", true); //對應子項目顯示明顯標記
                        //FormSet.mDeviceParamTable.FileName = SYSPara.PReadValue("DeviceParamTable").ToString(); //指向對應項目
                    }

                    string subpkg4 = SYSPara.PReadValue("KitTable").ToString();
                    if (string.IsNullOrEmpty(subpkg4))
                    {
                        FormSet.mKitTable.FileName_Auto = "";
                        FormSet.mKitTable.FolderName_Auto = "";
                        FormSet.mKitTable.AutoEnd();
                    }
                    else
                    {
                        FormSet.mKitTable.SetFileName(subpkg4, "", true); //對應子項目顯示明顯標記
                        //FormSet.mDeviceParamTable.FileName = SYSPara.PReadValue("DeviceParamTable").ToString(); //指向對應項目
                    }
                }
            }
        }

        //關聯式資料庫使用
        public override void InitialSubPackage()
        {
            #region 使用者修改 (關聯式資料庫，將相關表格加入這個主表格內)
            //tFieldCB1.EditForm = FormSet.mToolF;
            //FormSet.mToolF.Tag = this;
            //if (SYSPara.IsAutoMode)
            //    FormSet.mToolF.SetFileName(SYSPara.PReadValue("Tool").ToString(), "", true);
            tBoard.EditForm = FormSet.mBoardTable;
            tBoard.FolderPath = FormSet.mBoardTable.FolderPath;
            tBoard.HasEdit = true;
            FormSet.mBoardTable.Tag = this;
            if (SYSPara.IsAutoMode)
            {
                FormSet.mBoardTable.SetFileName(SYSPara.PReadValue("BoardTable").ToString(), "", true);
            }

            tTransferKit.EditForm = FormSet.mKitTable;
            tTransferKit.FolderPath = FormSet.mKitTable.FolderPath;
            tTransferKit.HasEdit = true;
            FormSet.mKitTable.Tag = this;
            if (SYSPara.IsAutoMode)
            {
                FormSet.mKitTable.SetFileName(SYSPara.PReadValue("KitTable").ToString(), "", true);
            }

            tDevice.EditForm = FormSet.mDeviceTable;
            tDevice.FolderPath = FormSet.mDeviceTable.FolderPath;
            tDevice.HasEdit = true;
            FormSet.mDeviceTable.Tag = this;
            if (SYSPara.IsAutoMode)
            {
                FormSet.mDeviceTable.SetFileName(SYSPara.PReadValue("DeviceTable").ToString(), "", true);
            }

            #endregion
        }

        #endregion

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        #region 架構使用 (Private)

        #endregion

    }
}
