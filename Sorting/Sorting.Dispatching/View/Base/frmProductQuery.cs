using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sorting.Dispatching.View.Base
{
    public partial class frmProductQuery : Form
    {
        public string filter = "1=1";
        public frmProductQuery()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtCigaretteCode.Text.Trim().Length > 0)
                filter += string.Format(" AND CIGARETTECODE LIKE '%{0}%'", this.txtCigaretteCode.Text.Trim());
            if(this.txtCigaretteName.Text.Trim().Length>0)
                filter += string.Format(" AND CIGARETTENAME LIKE '%{0}%'", this.txtCigaretteName.Text.Trim());
            if(this.cmbIsAbnormity.SelectedIndex==1)
                filter += " AND ISABNORMITY='0'";
            if (this.cmbIsAbnormity.SelectedIndex == 2)
                filter += " AND ISABNORMITY='1'";
            if (this.txtBarcode.Text.Trim().Length > 0)
                filter += string.Format(" AND BARCODE LIKE '%{0}%'", this.txtBarcode.Text.Trim());

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
