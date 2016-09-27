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
    public partial class frmAreaEdit : Form
    {
        public string AreaCode
        {
            get { return this.txtAreaCode.Text; }
        }

        public string AreaName
        {
            get { return this.txtAreaName.Text; }
        }

        public string SortId
        {
            get { return this.txtSortId.Text.Trim(); }
        }


        public frmAreaEdit()
        {
            InitializeComponent();
        }
        public frmAreaEdit(string areaCode, string areaName, string sortId)
        {
            InitializeComponent();
            this.txtAreaCode.Text = areaCode;
            this.txtAreaName.Text = areaName;
            this.txtSortId.Text = sortId;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtAreaCode.Text.Trim().Length == 0)
            {
                GridUtil.ShowInfo("线路代码不能为空。");
                this.txtAreaCode.Focus();
                return;
            }

            if (txtAreaName.Text.Trim().Length == 0)
            {
                GridUtil.ShowInfo("线路名称不能为空。");
                txtAreaName.Focus();
                return;
            }
            int SortId = 0;
            int.TryParse(this.txtSortId.Text.Trim(), out SortId);
            if (SortId == 0)
            {
                GridUtil.ShowInfo("配送顺序不正确，请输入正确的数字。");
                this.txtSortId.Focus();
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
