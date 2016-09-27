using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sorting.Dispatching.Dal;

namespace Sorting.Dispatching.View.Dialog
{
    public partial class OrderBatchEdit : Form
    {        
        public OrderBatchEdit()
        {
            InitializeComponent();
        }
        public OrderBatchEdit(string orderDate,string batchNo,string download,string valid,string uploadNo1)
        {
            InitializeComponent();

            this.txtOrderDate.Text = orderDate;
            this.txtBatchNo.Text = batchNo;
            this.ckbDownload.Checked = download == "是" ? true : false;

            this.ckbValid.Checked = valid == "是" ? true : false;
            this.ckbUploadNo1.Checked = uploadNo1 == "是" ? true : false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            BatchDal dal = new BatchDal();
            if (this.ckbDownload.Checked)
                dal.UpdateDownload(this.txtBatchNo.Text,"1");
            else
                dal.UpdateDownload(this.txtBatchNo.Text, "0");

            if (this.ckbValid.Checked)
                dal.UpdateIsValid(this.txtBatchNo.Text, "1");
            else
                dal.UpdateIsValid(this.txtBatchNo.Text, "0");

            if (this.ckbUploadNo1.Checked)
                dal.UpdateNoOnePro(this.txtBatchNo.Text, "1");
            else
                dal.UpdateNoOnePro(this.txtBatchNo.Text, "0");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

    }
}
