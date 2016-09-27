using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sorting.Dispatching.Dal;

namespace Sorting.Dispatching.View
{
    public partial class frmOrderDateDialog : Form
    {
        public string OrderDate;
        public string BatchNo;
        private int Flag;        

        public frmOrderDateDialog()
        {
            InitializeComponent();
            GetBatchNo();
        }
        public frmOrderDateDialog(int Flag, string orderDate)
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
            this.Flag = Flag;
            if(orderDate.Length>0)
                this.dateTimePicker1.Value = DateTime.Parse(orderDate);

            if (Flag == 1)
                this.cbDownloaded.Text = "含已下载";
            else if (Flag == 2 || Flag == 3 || Flag == 4)
                this.cbDownloaded.Text = "含已优化";
            else if (Flag == 5)
                this.cbDownloaded.Text = "含已上传";
            GetBatchNo();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //this.BatchNo = this.cmbBatchNo.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            GetBatchNo();
        }
        private void GetBatchNo()
        {
            OrderDate = this.dateTimePicker1.Value.ToString("yyyy-MM-dd");
            BatchDal batchDal = new BatchDal();
            //cmbBatchNo.Items.Clear();
            string filter = "1=1";

            if (!this.cbDownloaded.Checked)
            {
                if (Flag == 1)
                    filter = "ISDOWNLOAD='0'";
                else if (Flag == 2 || Flag == 3)
                    filter = "ISDOWNLOAD='1' AND ISVALID='0'";
                else if (Flag == 4)
                    filter = "ISDOWNLOAD='1' AND ISVALID='0'";
            }
            DataTable table = batchDal.FindBatchByFilter(OrderDate, filter);
            this.dataGridView1.DataSource = table.DefaultView;
            //bool hasNoSchedul = false;
            //foreach (DataRow row in table.Rows)
            //{
            //    cmbBatchNo.Items.Add(row["BATCHNO"].ToString().Trim());
            //    if (row["ISVALID"].ToString() == "0")
            //        hasNoSchedul = true;
            //}
            //if (!hasNoSchedul)
            //{
            //    batchDal.AddBatch(OrderDate, table.Rows.Count + 1);
            //    cmbBatchNo.Items.Add(Convert.ToString(table.Rows.Count + 1));
            //}
            //if(this.cmbBatchNo.Items.Count>0)
            //    this.cmbBatchNo.SelectedIndex = 0;
        }

        private void cbDownloaded_CheckedChanged(object sender, EventArgs e)
        {
            GetBatchNo();
        }
    }
}
