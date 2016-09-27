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
    public partial class CheckQueryForm : OpView.View.ToolbarForm
    {
        private ChannelDal channelDal = new ChannelDal();
        private string channelGroup = "A";

        public CheckQueryForm()
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
            
            SortNoDialog sortnoDialog = new SortNoDialog();
            if (sortnoDialog.ShowDialog() == DialogResult.OK)
            {
                bsMain.DataSource = channelDal.GetChannel(sortnoDialog.SortNo);                
            }
            else
            {
                using (PersistentManager pm = new PersistentManager())
                {

                    OrderDao orderDao = new OrderDao();
                    string sortNo = orderDao.FindMaxSortedMaster();

                    ChannelDao channelDao = new ChannelDao();
                    bsMain.DataSource = channelDao.FindAllChannelQuantity(sortNo);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (bsMain.DataSource == null)
                return;
            //OpView.ExcelHelper.DataTabletoExcel((DataTable)bsMain.DataSource, @"C\ss.xls");
            OpView.ExcelHelper.DoExport((DataTable)bsMain.DataSource);
        }
    }
}

