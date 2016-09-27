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
    public partial class OrderQueryForm : OpView.View.ToolbarForm
    {
        private OrderDal orderDal = new OrderDal();

        public OrderQueryForm()
        {
            InitializeComponent();
            this.ORDERDATE.FilteringEnabled = true;
            this.BATCHNO.FilteringEnabled = true;
            this.MAINORDERID.FilteringEnabled = true;
            this.MAINQUANTITY.FilteringEnabled = true;
            this.MAINSORTNO.FilteringEnabled = true;
            this.CUSTOMERCODE.FilteringEnabled = true;
            this.CUSTOMERNAME.FilteringEnabled = true;
            this.ROUTECODE.FilteringEnabled = true;
            this.ROUTENAME.FilteringEnabled = true;
            this.STATUS1.FilteringEnabled = true;
            this.dgvDetail.AutoGenerateColumns = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //if (bsMaster.DataSource == null)
            //{
                bsMaster.DataSource = orderDal.GetOrderMaster();
            //}
            //else
            //{
            //    DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dgvMaster);
            //}
        }

        private void dgvMaster_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgvDetail.DataSource = orderDal.GetOrderDetail(dgvMaster.Rows[e.RowIndex].Cells[3].Value.ToString());
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            OrderSortNoDialog sortnoDialog = new OrderSortNoDialog();
            if (sortnoDialog.ShowDialog() == DialogResult.OK)
            {
                if (MessageBox.Show("是否确定要修正大于此流水号订单的状态为未下达", "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string sortNo = sortnoDialog.SortNo;
                    string channeltype = "";
                    if (sortnoDialog.ChannelType == "通道机")
                        channeltype = "3";
                    else if (sortnoDialog.ChannelType == "立式机")
                        channeltype = "2";

                    if (channeltype.Length > 0)
                        orderDal.UpdateMissOrderStatus(sortNo, channeltype);
                    else
                        orderDal.UpdateMissOrderStatus(sortNo);
                    bsMaster.DataSource = orderDal.GetOrderMaster();
                }
            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            orderDal.UpdateOrderState();
            bsMaster.DataSource = orderDal.GetOrderMaster();
        }        
    }
}

