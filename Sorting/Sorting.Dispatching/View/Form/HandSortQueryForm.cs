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
    public partial class HandSortQueryForm : OpView.View.ToolbarForm
    {
        private OrderDal orderDal = new OrderDal();

        public HandSortQueryForm()
        {
            InitializeComponent();
            this.Column2.FilteringEnabled = true;
            this.Column5.FilteringEnabled = true;
            this.Column6.FilteringEnabled = true;
            this.Column7.FilteringEnabled = true;
            this.Column8.FilteringEnabled = true;
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
                bsMaster.DataSource = orderDal.FindHandMaster();
            }
            else
            {
                DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dgvMaster);
            }
        }

        private void dgvMaster_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgvDetail.DataSource = orderDal.FindDownloadDetail(dgvMaster.Rows[e.RowIndex].Cells[1].Value.ToString(),dgvMaster.Rows[e.RowIndex].Cells[3].Value.ToString());
        }
        
    }
}

