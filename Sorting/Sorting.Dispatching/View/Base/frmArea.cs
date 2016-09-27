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
    public partial class frmArea : OpView.View.ToolbarForm
    {
        AreaDal dal;
        public frmArea()
        {
            InitializeComponent();
            dal = new AreaDal();
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
                MessageBox.Show("读取配送区域信息失败，原因" + exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bsMain_PositionChanged(object sender, EventArgs e)
        {

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
            //        MessageBox.Show("新增配送区域信息失败，原因" + exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dgvMain.SelectedRows.Count != 0)
            {
                DataGridViewRow row = dgvMain.SelectedRows[0];

                frmAreaEdit f = new frmAreaEdit(row.Cells[0].Value.ToString(),row.Cells[1].Value.ToString(),row.Cells[2].Value.ToString());
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        dal.Save(f.SortId, f.AreaCode);
                        bsMain.DataSource = dal.GetAll();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("修改配送区域信息失败，原因" + exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            //        MessageBox.Show("删除配送区域信息失败，原因" + exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}

