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
    public partial class frmHandSortEdit : Form
    {
        public string OrderDate = "";
        public string OrderId = "";
        private OrderDal orderDal = new OrderDal();
        private Dictionary<string, string> OrderFields = new Dictionary<string, string>();

        public string BatchNo = "";

        public frmHandSortEdit()
        {
            InitializeComponent();
        }
        public frmHandSortEdit(string batchNo)
        {            
            InitializeComponent();
            OrderFields.Add("ORDERDATE", "订单日期");
            OrderFields.Add("BATCHNO", "批次号");
            OrderFields.Add("ORDERID", "订单编号");
            OrderFields.Add("SORTID", "配送顺序");
            OrderFields.Add("ROUTECODE", "线路编码");
            OrderFields.Add("ROUTENAME", "线路名称");
            OrderFields.Add("CUSTOMERCODE", "客户编号");
            OrderFields.Add("CUSTOMERDESC", "客户姓名");
            OrderFields.Add("AMOUNT", "数量");
            this.BatchNo = batchNo;
            this.dgvDetail.AutoGenerateColumns = false;
        }

        private void btnOrderId_Click(object sender, EventArgs e)
        {
            DataTable dtOrder = orderDal.FindDownloadMaster(BatchNo);

            SelectDialog selectDialog = new SelectDialog(dtOrder, OrderFields, true);
            if (selectDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtBatchNo.Text = selectDialog["BATCHNO"];
                this.txtOrderDate.Text = selectDialog["ORDERDATE"];
                this.txtOrderId.Text = selectDialog["ORDERID"];
            }
        }

        private void txtOrderId_TextChanged(object sender, EventArgs e)
        {
            dgvDetail.DataSource = orderDal.FindDownloadDetail(BatchNo, this.txtOrderId.Text);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.OrderId = this.txtOrderId.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
