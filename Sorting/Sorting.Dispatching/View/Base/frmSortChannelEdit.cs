using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sorting.Dispatching.Util;
using Sorting.Dispatching.Dal;

namespace Sorting.Dispatching.View.Base
{
    public partial class frmSortChannelEdit : Form
    {
        private Dictionary<string, string> ProductFields = new Dictionary<string, string>();
        public string ChannelCode
        {
            get { return this.txtChannelCode.Text.Trim(); }
        }
        public string ProductCode
        {
            get { return this.txtProductCode.Text; }
        }

        public string ProductName
        {
            get { return this.txtProductName.Text; }
        }
        public string ChannelOrder
        {
            get { return this.txtChannelOrder.Text; }
        }
        public string Status
        {
            get { return this.cmbStatus.SelectedIndex.ToString(); }
        }
        
        public frmSortChannelEdit()
        {
            InitializeComponent();
        }
        public frmSortChannelEdit(string channelcode,string channelName,string ProductCode, string ProductName, string channelOrder,string status)
        {
            InitializeComponent();

            ProductFields.Add("ProductCODE", "产品编号");
            ProductFields.Add("ProductNAME", "产品名称");
            ProductFields.Add("FactoryName", "供应商");
            ProductFields.Add("Width", "宽度");
            ProductFields.Add("BARCODE", "条形码");

            this.txtChannelCode.Text = channelcode;
            this.txtChannelName.Text = channelName;
            this.txtProductCode.Text = ProductCode;
            this.txtProductName.Text = ProductName;
            this.txtChannelOrder.Text = channelOrder;
            this.cmbStatus.SelectedIndex = int.Parse(status);
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            int channelOrder;
            int.TryParse(this.txtChannelOrder.Text.Trim(), out channelOrder);
            if (channelOrder <= 0)
            {
                GridUtil.ShowInfo("排列顺序请输入正确的数字！");
                this.txtChannelOrder.Focus();
                return;
            }
            //if (txtProductCode.Text.Trim().Length == 0)
            //{
            //    GridUtil.ShowInfo("卷烟代码不能为空。");
            //    this.txtProductCode.Focus();
            //    return;
            //}

            if (txtProductCode.Text.Trim().Length > 0)
            {
                if (txtProductName.Text.Trim().Length == 0)
                {
                    GridUtil.ShowInfo("产品名称不能为空。");
                    txtProductName.Focus();
                    return;
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnProductCode_Click(object sender, EventArgs e)
        {
            ProductDal dal = new ProductDal();

            DataTable dtProduct = dal.GetAll();

            SelectDialog selectDialog = new SelectDialog(dtProduct, ProductFields, false);
            if (selectDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtProductCode.Text = selectDialog["ProductCODE"];
                this.txtProductName.Text = selectDialog["ProductNAME"];
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtProductCode.Text = "";
            this.txtProductName.Text = "";
        }

        private void txtProductCode_TextChanged(object sender, EventArgs e)
        {
            ProductDal dal = new ProductDal();
            string filter = string.Format("ProductCODE='{0}'", this.txtProductCode.Text.Trim());
            DataTable dt = dal.GetAll(filter);
            if (dt.Rows.Count > 0)
                this.txtProductName.Text = dt.Rows[0]["ProductNAME"].ToString();
            else
                this.txtProductName.Text = "";
        }
    }
}


