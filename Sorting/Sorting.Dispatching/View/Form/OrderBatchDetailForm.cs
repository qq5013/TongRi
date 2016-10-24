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
    public partial class OrderBatchDetailForm : OpView.View.ToolbarForm
    {
        private BatchDal dal = new BatchDal();

        public OrderBatchDetailForm()
        {
            InitializeComponent();
            this.dgvMain.AutoGenerateColumns = false;
            this.dgvDetail.AutoGenerateColumns = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dgvMain.DataSource = dal.GetAll();
        }
        
        private void dgvMaster_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgvDetail.DataSource = dal.GetBatchDetail(dgvMain.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvDetail.DataSource == null)
                return;
            //ExcelHelper.DataTabletoExcel((DataTable)bsMain.DataSource, @"C\ss.xls");
            ExcelHelper.DoExport((DataTable)dgvDetail.DataSource);
        }
    }
}

