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
    public partial class frmHandSort : OpView.View.ToolbarForm
    {
        private OrderDal orderDal = new OrderDal();
        private string BatchNo = "";

        public frmHandSort()
        {
            InitializeComponent();
        }
        public frmHandSort(string batchNo)
        {
            InitializeComponent();
            this.dgvMaster.AutoGenerateColumns = false;
            this.dgvDetail.AutoGenerateColumns = false;
            this.BatchNo = batchNo;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            bsMaster.DataSource = orderDal.FindHandMaster(BatchNo);
        }

        private void dgvMaster_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgvDetail.DataSource = orderDal.FindHandDetail(BatchNo,dgvMaster.Rows[e.RowIndex].Cells[2].Value.ToString());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmHandSortEdit f = new frmHandSortEdit(BatchNo);
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                HandleSortOrderDal handleSortOrderDal = new HandleSortOrderDal();
                handleSortOrderDal.Insert(f.OrderDate,f.BatchNo, f.OrderId);

                bsMaster.DataSource = orderDal.FindHandMaster(BatchNo);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvMaster.CurrentRow != null)
            {
                HandleSortOrderDal handleSortOrderDal = new HandleSortOrderDal();
                handleSortOrderDal.Delete(BatchNo, dgvMaster.Rows[this.dgvMaster.CurrentRow.Index].Cells[2].Value.ToString());

                bsMaster.DataSource = orderDal.FindHandMaster(BatchNo);
                if (this.dgvMaster.CurrentRow == null)
                    dgvDetail.DataSource = orderDal.FindHandDetail(BatchNo, "");
            }
        }
    }
}
