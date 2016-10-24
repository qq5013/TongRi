using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sorting.Dispatching.Dal;
using Sorting.Dispatching.Dao;
using MCP;
using DB.Util;

namespace Sorting.Dispatching.View
{
    public partial class OrderBatchForm : OpView.View.ToolbarForm
    {
        private BatchDal dal = new BatchDal();
        private string channelGroup = "A";

        public OrderBatchForm()
        {
            InitializeComponent();
            this.dgvMain.AutoGenerateColumns = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dgvMain.DataSource = dal.GetAll();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dgvMain.SelectedRows.Count != 0)
            {
                int RowIndex = this.dgvMain.CurrentRow.Index;
                DataGridViewRow row = dgvMain.SelectedRows[0];
                Dialog.OrderBatchEdit f = new Dialog.OrderBatchEdit(row.Cells[1].Value.ToString(), row.Cells[0].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString());
                if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    dgvMain.DataSource = dal.GetAll();
                    if(this.dgvMain.Rows.Count-1>=RowIndex)
                        dgvMain.CurrentCell = dgvMain.Rows[RowIndex].Cells[0];

                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvMain.DataSource == null)
                dgvMain.DataSource = dal.GetAll();
            //ExcelHelper.DataTabletoExcel((DataTable)bsMain.DataSource, @"C\ss.xls");
            ExcelHelper.DoExport((DataTable)dgvMain.DataSource);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvMain.CurrentRow == null)
                return;
            string batchNo = this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].Cells[0].Value.ToString();
            if (MessageBox.Show(string.Format("是否确定要删除批次号[{0}]",batchNo), "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                dal.DeleteBatchData(batchNo);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {            
            if (MessageBox.Show("此清理功能会把小于当前日期的数据全部删除，请操作前最好备份数据库", "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                dal.ClearBatchData();
                MessageBox.Show("清理完成","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
    }
}

