using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using KCSDK;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace TBPP14200
{
    public partial class UserManagment : Form
    {
        public UserManagment()
        {
            InitializeComponent();

            cbUserRight.Items.Clear();
            for (int i = 0; i < SYSPara.mSecret.SecretList.Count; i++)
            {
                if (SYSPara.mSecret.NowUser.Level < SYSPara.mSecret.SecretList[i].Level)
                    continue;

                if (!SYSPara.mSecret.SecretList[i].IsDisplay)
                    continue;

                cbUserRight.Items.Add(SYSPara.mSecret.SecretList[i].DisplayName);
            }

            if (cbUserRight.Items.Count > 0)
                cbUserRight.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        //讀入所有的使用者
        public void LoadUser()
        {
            lvUserList.Items.Clear();
            for (int i = 0; i < SYSPara.mSecret.UserList.Count; i++)
            {
                if (SYSPara.mSecret.NowUser.Level < SYSPara.mSecret.UserList[i].Level)
                    continue;

                Secret.UserObj data = SYSPara.mSecret.UserList[i];
                Secret.SecretLevel level = SYSPara.mSecret.SecretList.Find(x => x.Level == data.Level);
                if (level != null)
                {
                    if (!level.IsDisplay)
                        continue;

                    ListViewItem item = new ListViewItem();
                    item.Text = data.Name;
                    item.SubItems.Add(data.PW);
                    item.SubItems.Add(level.DisplayName);
                    lvUserList.Items.Add(item);
                }
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbUserID.Text))
                MessageBox.Show("請輸入員工編號");
            else if (cbUserRight.SelectedIndex == -1)
                MessageBox.Show("請選擇權限層級");
            else
            {
                if (MessageBox.Show(string.Format("是否新增使用者【{0}】?", tbUserID), "確認", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                    return;

                Secret.UserObj user = SYSPara.mSecret.UserList.Find(x => x.Name == tbUserID.Text);
                if (user != null)
                {
                    MessageBox.Show("人員編號重覆");
                    return;
                }

                user = new Secret.UserObj();

                user.PW = tbUserPW.Text;

                Secret.SecretLevel level = SYSPara.mSecret.SecretList.Find(x => x.DisplayName == cbUserRight.Text);
                if (level == null)
                {
                    MessageBox.Show("權限層級錯誤");
                    return;
                }

                user.Level = level.Level;

                SYSPara.mSecret.UserList.Add(user);

                LoadUser();
                if (WriteToXLS())
                    MessageBox.Show("新增使用者完成");
                else
                    MessageBox.Show("存檔使用者檔案失敗");
            }     
        }

        private bool WriteToXLS()
        {
            string fname = string.Format(@"{0}\localdata\userlist.xls", SYSPara.SysDir);

            if (File.Exists(fname))
                File.Delete(fname);

            try
            {
                using (FileStream fs = new FileStream(fname, FileMode.CreateNew, FileAccess.Write))
                {
                    //建立Excel 2003檔案
                    IWorkbook workbook = new HSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("使用者管理");

                    //寫入表頭
                    int LineCNT = 1;
                    sheet.CreateRow(LineCNT);
                    sheet.GetRow(LineCNT).CreateCell(1).SetCellValue("人員編號");
                    sheet.GetRow(LineCNT).CreateCell(2).SetCellValue("人員密碼");
                    sheet.GetRow(LineCNT).CreateCell(3).SetCellValue("權限層級");
                    LineCNT++;

                    foreach (Secret.UserObj user in SYSPara.mSecret.UserList)
                    {
                        sheet.CreateRow(LineCNT);
                        sheet.GetRow(LineCNT).CreateCell(1).SetCellValue(user.Name);
                        sheet.GetRow(LineCNT).CreateCell(2).SetCellValue(user.PW);
                        sheet.GetRow(LineCNT).CreateCell(3).SetCellValue(user.Level);
                        LineCNT++;
                    }

                    for (int i = 1; i <= 3; i++)
                        sheet.AutoSizeColumn(i);

                    workbook.Write(fs);
                    fs.Close();
                    workbook = null;
                }
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }

        private void lvUserList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvUserList.SelectedItems.Count > 0)
            {
                ListViewItem item = lvUserList.SelectedItems[0];
                if (item != null)
                {
                    tbUserID.Text = item.SubItems[0].Text;
                    tbUserPW.Text = item.SubItems[1].Text;

                    if (cbUserRight.Items.Contains(item.SubItems[2].Text))
                        cbUserRight.Text = item.SubItems[2].Text;
                    else
                        cbUserRight.SelectedIndex = -1;
                }
            }
        }

        private void btnChangeUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbUserID.Text))
                MessageBox.Show("請輸入員工編號");
            else if (cbUserRight.SelectedIndex == -1)
                MessageBox.Show("請選擇權限層級");
            else
            {
                if (MessageBox.Show("是否修改?", "確認", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                    return;

                Secret.UserObj user = SYSPara.mSecret.UserList.Find(x => x.Name == tbUserID.Text);
                if (user == null)
                {
                    MessageBox.Show("人員編號錯誤");
                    return;
                }

                user.PW = tbUserPW.Text;

                Secret.SecretLevel level = SYSPara.mSecret.SecretList.Find(x=>x.DisplayName == cbUserRight.Text);
                if (level==null)
                {
                    MessageBox.Show("權限層級錯誤");
                    return;
                }

                user.Level = level.Level;

                LoadUser();

                if (WriteToXLS())
                    MessageBox.Show("修改完成");
                else
                    MessageBox.Show("存檔使用者檔案失敗");
            }
        }

        private void btnDelUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbUserID.Text))
                MessageBox.Show("請輸入員工編號");
            else if (cbUserRight.SelectedIndex == -1)
                MessageBox.Show("請選擇權限層級");
            else
            {
                if (MessageBox.Show(string.Format("是否刪除使用者【{0}】?",tbUserID), "確認", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                    return;

                Secret.UserObj user = SYSPara.mSecret.UserList.Find(x => x.Name == tbUserID.Text);
                if (user == null)
                {
                    MessageBox.Show("人員編號錯誤");
                    return;
                }

                SYSPara.mSecret.UserList.Remove(user);

                LoadUser();

                if (WriteToXLS())
                    MessageBox.Show("刪除完成");
                else
                    MessageBox.Show("存檔使用者檔案失敗");
            }
        }
    }
}
