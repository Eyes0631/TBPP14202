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
using CommonObj;

namespace TBPP14200
{
    public partial class 內參設定 : BaseOptionF
    {
        private List<Button> btnlist = new List<Button>();

        public 內參設定()
            : base("Setup")
        {
            InitializeComponent();

            TopLevel = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            ControlBox = false;
            StartPosition = FormStartPosition.CenterScreen;
        }

        public override void Initial()
        {
            DataSnippet.ControlEnable(this, false);
            DataSnippet.DataGetAll(this);
            ButtonEnabled(false);
        }

        public override void ChangeUser()
        {
        }

        //關聯式子表格相關設定
        public override void InitialSubPackage()
        {
            #region 使用者修改 (關聯式資料庫，將相關表格加入這個主表格內)
            //tFieldCB1.EditForm = FormSet.mToolF;
            //FormSet.mToolF.Tag = this;
            #endregion
        }

        private void ButtonEnabled(bool sw)
        {
            foreach (Button btn in btnlist)
                btn.Enabled = sw;
        }

        private void SetOpenFilePath(DFieldEdit dEdit)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fname = openFileDialog1.FileName;
                if (!string.IsNullOrEmpty(fname))
                    dEdit.FieldValue = fname;
            }
        }

        private void SetOpenFolderPath(DFieldEdit dEdit)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fname = folderBrowserDialog1.SelectedPath;
                if (!string.IsNullOrEmpty(fname))
                    dEdit.FieldValue = fname;
            }
        }

        private void btnEdit2_Click(object sender, EventArgs e)
        {
            ButtonEnabled(true);
        }

        private void btnSave2_Click(object sender, EventArgs e)
        {
            ButtonEnabled(false);
        }

        private void btnCancel2_Click(object sender, EventArgs e)
        {
            ButtonEnabled(false);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ButtonEnabled(false);
            Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dEdit1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DGV_SPCSet.Rows.Add();
            DGV_SPCSet.Rows[DGV_SPCSet.Rows.Count - 1].Height = DGV_SPCSet.RowHeight;
            DGV_SPCSet.Rows[DGV_SPCSet.Rows.Count - 1].Cells[0].Value = DGV_SPCSet.Rows.Count;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (DGV_SPCSet.SelectedCells.Count == 1)
            {
                DGV_SPCSet.Rows.RemoveAt(DGV_SPCSet.SelectedCells[0].RowIndex);
            }
        }

        private void btnLoadPWRCurve_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML files (*.xml)|*.xml";

            LogAnalyzer.LaserPowerCurveData pwrData;
            List<LogAnalyzer.LaserPowerCurveData> lstPWR = new List<LogAnalyzer.LaserPowerCurveData>();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ds.ReadXml(ofd.FileName);
                DataTable dt = ds.Tables[0];


                foreach (DataRow row in dt.Rows)
                {
                    pwrData = new LogAnalyzer.LaserPowerCurveData();
                    pwrData.Current = (float)row[0];
                    pwrData.Frequency = (float)row[1];
                    pwrData.Power = (float)row[2];

                    lstPWR.Add(pwrData);
                }

                SYSPara.logDB.WriteLaserPowerCurve(lstPWR);

            }
        }
    }
}
