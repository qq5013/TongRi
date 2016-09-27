using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sorting.Dispatching.Dal;
using Sorting.Dispatching.Util;

namespace Sorting.Dispatching.View.Base
{
    public partial class frmChannelParameter : Form
    {
        public frmChannelParameter()
        {
            InitializeComponent();
            
        }

        private void frmChannelParameter_Load(object sender, EventArgs e)
        {
            ParameterDal dal = new ParameterDal();
            Dictionary<string, string> param = dal.FindParameter();

            //通道机1
            string[] OrderQtyTPercent1 = param["OrderQtyTPercent1"].Split(',');
            
            if (OrderQtyTPercent1.Length > 1)
            {
                this.txtOrderQtyTPercent1.Text = OrderQtyTPercent1[0];                
                this.cbTQty1.Text = OrderQtyTPercent1[1];                
            }

            //通道机2
            string[] OrderQtyTPercent2 = param["OrderQtyTPercent2"].Split(',');

            if (OrderQtyTPercent2.Length > 1)
            {
                this.txtOrderQtyTPercent2.Text = OrderQtyTPercent2[0];
                this.cbTQty2.Text = OrderQtyTPercent2[1];
            }

            //立式机1
            string[] OrderQtyLPercent1 = param["OrderQtyLPercent1"].Split(',');

            if (OrderQtyLPercent1.Length > 1)
            {
                this.txtOrderQtyLPercent1.Text = OrderQtyLPercent1[0];
                this.cbLQty1.Text = OrderQtyLPercent1[1];
            }

            //立式机2
            string[] OrderQtyLPercent2 = param["OrderQtyLPercent2"].Split(',');

            if (OrderQtyLPercent2.Length > 1)
            {
                this.txtOrderQtyLPercent2.Text = OrderQtyLPercent2[0];
                this.cbLQty2.Text = OrderQtyLPercent2[1];
            }            
        }

        private void txtOrderQtyTPercent1_TextChanged(object sender, EventArgs e)
        {
            this.txtOrderQtyTPercent11.Text = this.txtOrderQtyTPercent1.Text;
        }

        private void txtOrderQtyTPercent2_TextChanged(object sender, EventArgs e)
        {
            txtOrderQtyTPercent21.Text = this.txtOrderQtyTPercent2.Text;
        }

        private void txtOrderQtyLPercent1_TextChanged(object sender, EventArgs e)
        {
            txtOrderQtyLPercent11.Text = this.txtOrderQtyLPercent1.Text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            float Tp1 = 0;
            float.TryParse(this.txtOrderQtyTPercent1.Text.Trim(), out Tp1);
            if (Tp1 == 0)
            {
                GridUtil.ShowInfo("通道机订单品牌百分比1输入错误！");
                this.txtOrderQtyTPercent1.Focus();
                return;
            }
            
            float Tp2 = 0;
            float.TryParse(this.txtOrderQtyTPercent2.Text.Trim(), out Tp2);
            if (Tp2 == 0)
            {
                GridUtil.ShowInfo("通道机订单品牌百分比2输入错误！");
                this.txtOrderQtyTPercent2.Focus();
                return;
            }
            //百分比大小校对
            if (Tp1 <= Tp2)
            {
                GridUtil.ShowInfo("通道机订单品牌百分比1不可小于等于通道机订单品牌百分比2！");
                this.txtOrderQtyTPercent2.Focus();
                return;
            }
            float Lp1 = 0;
            float.TryParse(this.txtOrderQtyLPercent1.Text.Trim(), out Lp1);
            if (Lp1 == 0)
            {
                GridUtil.ShowInfo("立式机订单品牌百分比1输入错误！");
                this.txtOrderQtyLPercent1.Focus();
                return;
            }
            float Lp2 = 0;
            float.TryParse(this.txtOrderQtyLPercent2.Text.Trim(), out Lp2);
            if (Lp2 == 0)
            {
                GridUtil.ShowInfo("立式机订单品牌百分比2输入错误！");
                this.txtOrderQtyLPercent2.Focus();
                return;
            }
            //百分比大小校对
            if (Lp1 <= Lp2)
            {
                GridUtil.ShowInfo("立式机订单品牌百分比1不可小于等于立式机订单品牌百分比2！");
                this.txtOrderQtyLPercent2.Focus();
                return;
            }

            string OrderQtyTPercent1 = Tp1.ToString() + "," + this.cbTQty1.Text;
            string OrderQtyTPercent2 = Tp2.ToString() + "," + this.cbTQty2.Text;
            string OrderQtyLPercent1 = Lp1.ToString() + "," + this.cbLQty1.Text;
            string OrderQtyLPercent2 = Lp2.ToString() + "," + this.cbLQty2.Text;

            ParameterDal dal = new ParameterDal();
            dal.UpdateParameter(OrderQtyTPercent1, "OrderQtyTPercent1");
            dal.UpdateParameter(OrderQtyTPercent2, "OrderQtyTPercent2");
            dal.UpdateParameter(OrderQtyLPercent1, "OrderQtyLPercent1");
            dal.UpdateParameter(OrderQtyLPercent2, "OrderQtyLPercent2");

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
