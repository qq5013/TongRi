using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sorting.Dispatching.Dal;

namespace Sorting.Dispatching.View.Base
{
    public partial class frmSupplyChannel : OpView.View.ToolbarForm
    {
        StockChannelDal dal;
        public frmSupplyChannel()
        {
            InitializeComponent();
            dal = new StockChannelDal();
            Util.GridUtil.EnableFilter(dgvMain);
        }
        private void pnlMain_ParentChanged(object sender, EventArgs e)
        {
            try
            {
                bsMain.DataSource = dal.GetAll();
            }
            catch (Exception exp)
            {
                MessageBox.Show("读取补货烟道信息失败，原因" + exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bsMain_PositionChanged(object sender, EventArgs e)
        {
            if (bsMain.Current != null)
            {
                DataRowView row = (DataRowView)bsMain.Current;
                btnDelete.Enabled = !(row["CHANNELTYPE"].ToString() == "3");
                btnModify.Enabled = btnDelete.Enabled;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //frmAreaEdit f = new frmAreaEdit();
            //if (f.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        AreaDal dal = new AreaDal();
            //        dal.Save(billTypeDialog.BillCode,
            //                                    billTypeDialog.BillName,
            //                                    billTypeDialog.BillType,
            //                                    billTypeDialog.TaskLevel,
            //                                    billTypeDialog.Memo);
            //        bsMain.DataSource = billTypeDal.GetBillType();
            //    }
            //    catch (Exception exp)
            //    {
            //        MessageBox.Show("新增补货烟道信息失败，原因" + exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dgvMain.SelectedRows.Count != 0)
            {
                DataGridViewRow row = dgvMain.SelectedRows[0];

                frmSupplyChannelEdit f = new frmSupplyChannelEdit(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString(), row.Cells[6].Value.ToString(), row.Cells[5].Value.ToString());
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        dal.Save(f.ChannelCode, f.CigaretteCode, f.CigaretteName, f.ChannelOrder, f.Status);
                        bsMain.DataSource = dal.GetAll();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("修改卷烟信息失败，原因" + exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //if (dgvMain.SelectedRows.Count != 0)
            //{
            //    DataGridViewRow row = dgvMain.SelectedRows[0];
            //    try
            //    {
            //        BillTypeDal billTypeDal = new BillTypeDal();
            //        billTypeDal.DeleteBillType(row.Cells[0].Value.ToString());
            //        bsMain.DataSource = billTypeDal.GetBillType();
            //    }
            //    catch (Exception exp)
            //    {
            //        MessageBox.Show("删除补货烟道信息失败，原因" + exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }

            //}
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            bsMain.DataSource = dal.GetAll();
        }
        private void dgvMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (e.Value.ToString() == "3")
                    e.Value = "混合烟道";
                else
                    e.Value = "单一烟道";
            }
            if (e.ColumnIndex == 5)
            {
                if (e.Value.ToString() == "0")
                    e.Value = "禁用";
                else
                    e.Value = "启用";
            }
        }
    }
}



