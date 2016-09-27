using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sorting.Dispatching.Dal;
using System.IO;

namespace Sorting.Dispatching.View
{
    public partial class InitialOrderTotalForm : OpView.View.ToolbarForm
    {
        private OrderDal orderDal = new OrderDal();

        public InitialOrderTotalForm()
        {
            InitializeComponent();
            //this.dgvDetail.AutoGenerateColumns = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (bsMaster.DataSource == null)
            {
                bsMaster.DataSource = orderDal.FindDownloadTotal();
            }
            else
            {
                DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dgvMaster);
            }
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (bsMaster.DataSource == null)
                bsMaster.DataSource = orderDal.FindDownloadTotal();
            //OpView.ExcelHelper.DataTabletoExcel((DataTable)bsMain.DataSource, @"C\ss.xls");
            OpView.ExcelHelper.DoExport((DataTable)bsMaster.DataSource);
        }
    }
}

