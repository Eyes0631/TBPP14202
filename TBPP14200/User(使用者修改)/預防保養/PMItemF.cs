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
    public partial class PMItemF : Form
    {
        public PMItem m_Item;
        private bool m_Reset = false;

        public PMItemF()
        {
            InitializeComponent();
        }

        public PMItemF(ref PMItem Item, bool bReset = false)
        {
            InitializeComponent();
            m_Item = Item;
            cboPMCycle.SelectedIndex = 0;
            m_Reset = bReset;
            if (m_Reset)
            {
                this.Size = new System.Drawing.Size(330, 157);
                btnAdd.Text = "確定";
                btnCancel.Visible = false;
                btnAdd.Location = new Point(110, 90);
                txtItem.Text = m_Item.ItemName;

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (m_Reset)
            {
                m_Item.ItemName = txtItem.Text;
                m_Item.PMCycle = (ePMCycle)cboPMCycle.SelectedIndex;
                m_Item.PMDate = dtPickerPM.Value;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;

            }
            else
            {
                if (!String.IsNullOrEmpty(txtItem.Text) && m_Item != null)
                {
                    m_Item.ItemName = txtItem.Text;
                    m_Item.PMCycle = (ePMCycle)cboPMCycle.SelectedIndex;
                    if (cboPMCycle.SelectedIndex == 3) //Mileage Selected
                    {
                        try
                        {
                            ProVLib.Motor mtr = ProVLib.MotorList.Motors.Find(m => m.Name == m_Item.ItemName);
                            mtr.IsUseMileage = true;
                            mtr.ResetMileage();
                            m_Item.PMMile = Convert.ToInt32(txtMileage.Text);
                            m_Item.PMHint = txtHint.Text;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        m_Item.PMDate = dtPickerPM.Value;
                    }
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
        }

        private void cboPMCycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPMCycle.SelectedIndex == 3) //Mileage Selected
            {
                cboMotorList.Location = new Point(12, 30);
                txtMileage.Location = new Point(162, 30);
                lblItem.Text = "馬達列表";
                lblBaseDate.Text = "保養里程(m)";
                txtItem.Visible = false;
                dtPickerPM.Visible = false;
                cboMotorList.Visible = true;
                txtMileage.Visible = true;
                lblHint.Visible = true;
                txtHint.Visible = true;
                cboMotorList.Items.Clear();
                foreach (ProVLib.Motor mtr in ProVLib.MotorList.Motors)
                {
                    cboMotorList.Items.Add(mtr.Name);
                }

                if (cboMotorList.Items.Count > 0)
                {
                    cboMotorList.SelectedIndex = 0;
                    txtItem.Text = cboMotorList.Text;
                }
            }
            else
            {
                lblItem.Text = "保養項目";
                lblBaseDate.Text = "保養基準日";
                txtItem.Visible = true;
                dtPickerPM.Visible = true;
                cboMotorList.Visible = false;
                txtMileage.Visible = false;
                lblHint.Visible = false;
                txtHint.Visible = false;
                txtItem.Text = "";
            }
        }
    }
}
