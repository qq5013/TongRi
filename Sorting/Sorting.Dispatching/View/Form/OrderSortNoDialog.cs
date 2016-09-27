using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sorting.Dispatching.View
{
    public partial class OrderSortNoDialog : Form
    {
        public string SortNo
        {
            get { return txtSortNo.Text; }
        }
        public string ChannelType
        {
            get { return this.cbChannelType.Text; }
        }
        public OrderSortNoDialog()
        {
            InitializeComponent();
        }


        private void txtSortNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar) || e.KeyChar == '\b'))
                e.Handled = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}