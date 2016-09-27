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
    public partial class frmSupplyChannelEdit : Form
    {

    private Dictionary<string, string> CigaretteFields = new Dictionary<string, string>();
        public string ChannelCode
        {
            get { return this.txtChannelCode.Text.Trim(); }
        }
        public string CigaretteCode
        {
            get { return this.txtCigaretteCode.Text; }
        }

        public string CigaretteName
        {
            get { return this.txtCigaretteName.Text; }
        }
        public string ChannelOrder
        {
            get { return this.txtChannelOrder.Text; }
        }
        public string Status
        {
            get { return this.cmbStatus.SelectedIndex.ToString(); }
        }
        
        public frmSupplyChannelEdit()
        {
            InitializeComponent();
        }
        public frmSupplyChannelEdit(string channelcode, string channelName, string cigaretteCode, string cigaretteName, string channelOrder, string status)
        {
            InitializeComponent();

            CigaretteFields.Add("CIGARETTECODE", "卷烟代号");
            CigaretteFields.Add("CIGARETTENAME", "卷烟名称");
            CigaretteFields.Add("ISABNORMITY", "异形烟");
            CigaretteFields.Add("BARCODE", "条形码");

            this.txtChannelCode.Text = channelcode;
            this.txtChannelName.Text = channelName;
            this.txtCigaretteCode.Text = cigaretteCode;
            this.txtCigaretteName.Text = cigaretteName;
            this.txtChannelOrder.Text = channelOrder;
            this.cmbStatus.SelectedIndex = int.Parse(status);
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            int channelOrder;
            int.TryParse(this.txtChannelOrder.Text.Trim(), out channelOrder);
            if (channelOrder <= 0)
            {
                GridUtil.ShowInfo("排烟顺序请输入正确的数字！");
                this.txtChannelOrder.Focus();
                return;
            }
            //if (txtCigaretteCode.Text.Trim().Length == 0)
            //{
            //    GridUtil.ShowInfo("卷烟代码不能为空。");
            //    this.txtCigaretteCode.Focus();
            //    return;
            //}

            //if (txtCigaretteName.Text.Trim().Length == 0)
            //{
            //    GridUtil.ShowInfo("卷烟名称不能为空。");
            //    txtCigaretteName.Focus();
            //    return;
            //}

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnCigaretteCode_Click(object sender, EventArgs e)
        {
            ProductDal dal = new ProductDal();

            DataTable dtCigarette = dal.GetAll();

            SelectDialog selectDialog = new SelectDialog(dtCigarette, CigaretteFields, false);
            if (selectDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtCigaretteCode.Text = selectDialog["CIGARETTECODE"];
                this.txtCigaretteName.Text = selectDialog["CIGARETTENAME"];
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtCigaretteCode.Text = "";
            this.txtCigaretteName.Text = "";
        }
    }
}
