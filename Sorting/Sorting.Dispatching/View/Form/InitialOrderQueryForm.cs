using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sorting.Dispatching.Dal;

namespace Sorting.Dispatching.View
{
    public partial class InitialOrderQueryForm : OpView.View.ToolbarForm
    {
        private OrderDal orderDal = new OrderDal();

        public InitialOrderQueryForm()
        {
            InitializeComponent();
            Util.GridUtil.EnableFilter(dgvDetail);
            this.dgvDetail.AutoGenerateColumns = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (bsMaster.DataSource == null)
            {
                bsMaster.DataSource = orderDal.FindDownloadMaster();
            }
            else
            {
                DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dgvMaster);
            }
        }

        private void dgvMaster_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgvDetail.DataSource = orderDal.FindDownloadDetail(dgvMaster.Rows[e.RowIndex].Cells[5].Value.ToString());
        }
    }
}

