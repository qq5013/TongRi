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
        public string CigaretteCode
        {
            get { return this.txtCigaretteCode.Text; }
        }

        public string CigaretteName
        {
            get { return this.txtCigaretteName.Text; }
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
        public int PackageNum
        {
            get { return int.Parse(this.txtPackageNum.Text.Trim()); }
        }
        public frmProductEdit()
        {
            InitializeComponent();
        }
        public frmProductEdit(string cigaretteCode, string cigaretteName, string shortName,string isAbnormity,string barcode,string packNum)
        {
            InitializeComponent();
            this.txtCigaretteCode.Text = cigaretteCode;
            this.txtCigaretteName.Text = cigaretteName;
            this.txtShowName.Text = shortName;
            this.cmbIsAbnormity.SelectedIndex = int.Parse(isAbnormity);
            this.txtBarcode.Text = barcode;
            this.txtPackageNum.Text = packNum;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtCigaretteCode.Text.Trim().Length == 0)
            {
                GridUtil.ShowInfo("卷烟代码不能为空。");
                this.txtCigaretteCode.Focus();
                return;
            }

            if (txtCigaretteName.Text.Trim().Length == 0)
            {
                GridUtil.ShowInfo("卷烟名称不能为空。");
                txtCigaretteName.Focus();
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

