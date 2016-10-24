using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sorting.Dispatching.Util;

namespace Sorting.Dispatching.View.Base
{
    public partial class frmProductEdit : Form
    {
        public string ProductCode
        {
            get { return this.txtProductCode.Text; }
        }

        public string ProductName
        {
            get { return this.txtProductName.Text; }
        }
        public string ShowName
        {
            get { return this.txtShowName.Text; }
        }
        public string IsAbnormity
        {
            get { return this.cmbIsAbnormity.SelectedIndex.ToString(); }
        }
        public string Barcode
        {
            get { return this.txtBarcode.Text.Trim(); }
        }

        public frmProductEdit()
        {
            InitializeComponent();
        }
        public frmProductEdit(string cigaretteCode, string cigaretteName, string shortName,string isAbnormity,string barcode)
        {
            InitializeComponent();
            this.txtProductCode.Text = cigaretteCode;
            this.txtProductName.Text = cigaretteName;
            this.txtShowName.Text = shortName;
            this.cmbIsAbnormity.SelectedIndex = int.Parse(isAbnormity);
            this.txtBarcode.Text = barcode;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtProductCode.Text.Trim().Length == 0)
            {
                GridUtil.ShowInfo("产品编码不能为空。");
                this.txtProductCode.Focus();
                return;
            }

            if (txtProductName.Text.Trim().Length == 0)
            {
                GridUtil.ShowInfo("产品名称不能为空。");
                txtProductName.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

