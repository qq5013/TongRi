using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sorting.Dispatching.Dal;

namespace Sorting.Dispatching.View
{
    public partial class ChannelBalanceForm : OpView.View.ToolbarForm
    {
        private ChannelDal channelDal = new ChannelDal();

        public ChannelBalanceForm()
        {
            InitializeComponent();
            this.Column1.FilteringEnabled = true;
            this.Column2.FilteringEnabled = true;
            this.Column5.FilteringEnabled = true;
            this.Column6.FilteringEnabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (bsMain.DataSource == null)
                bsMain.DataSource = channelDal.GetChannelBalance();
            else
                DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dgvMain);
        }       

        

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (bsMain.DataSource == null)
                bsMain.DataSource = channelDal.GetChannelBalance();
            //OpView.ExcelHelper.DataTabletoExcel((DataTable)bsMain.DataSource, @"C\ss.xls");
            OpView.ExcelHelper.DoExport((DataTable)bsMain.DataSource);
        }

        private void btnClearBalance_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("清仓动作只有在分拣结束后，才可执行，确定要执行吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            DataTable dt = channelDal.FindAllChannelBalance();
            
            int[] balance3 = new int[15];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string batchNo = dt.Rows[i]["BATCHNO"].ToString();
                int bno = int.Parse(batchNo.Substring(10, 3));
                balance3[i] = int.Parse(dt.Rows[i]["QUANTITY" + bno.ToString()].ToString());
            }
            this.mainFrame.Context.ProcessDispatcher.WriteToService("SortPLC", "ClearBalance31", balance3);
            this.mainFrame.Context.ProcessDispatcher.WriteToService("SortPLC", "ClearBalance32", 1);

            //drs = dt.Select("CHANNELTYPE='通道机'");
            //int[] balance2 = new int[70];
            //for (int i = 0; i < drs.Length; i++)
            //{
            //    string batchNo = drs[i]["BATCHNO"].ToString();
            //    int bno = int.Parse(batchNo.Substring(10, 3));
            //    balance3[i] = int.Parse(drs[i]["QUANTITY" + bno.ToString()].ToString());
            //}
            //this.mainFrame.Context.ProcessDispatcher.WriteToService("SortPLC", "ClearBalance31", balance3);
            //this.mainFrame.Context.ProcessDispatcher.WriteToService("SortPLC", "ClearBalance32", 1);
        }
    }
}

