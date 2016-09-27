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
    public partial class frmRouteEdit : Form
    {
        public string RouteCode
        {
            get { return this.txtRouteCode.Text; }
        }

        public string RouteName
        {
            get { return this.txtRouteName.Text; }
        }

        public string SortId
        {
            get { return this.txtSortId.Text.Trim(); }
        }
        public string IsSort
        {
            get { return this.cmbIsSort.SelectedIndex.ToString(); }
        }
        public frmRouteEdit()
        {
            InitializeComponent();
        }
        public frmRouteEdit(string routeCode, string routeName, string sortId,string isSort)
        {
            InitializeComponent();
            this.txtRouteCode.Text = routeCode;
            this.txtRouteName.Text = routeName;
            this.txtSortId.Text = sortId;
            this.cmbIsSort.SelectedIndex = int.Parse(isSort);
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtRouteCode.Text.Trim().Length == 0)
            {
                GridUtil.ShowInfo("线路代码不能为空。");
                this.txtRouteCode.Focus();
                return;
            }

            if (txtRouteName.Text.Trim().Length == 0)
            {
                GridUtil.ShowInfo("线路名称不能为空。");
                txtRouteName.Focus();
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

