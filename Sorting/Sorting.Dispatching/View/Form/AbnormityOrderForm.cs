using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sorting.Dispatching.Dal;
using System.IO;

namespace Sorting.Dispatching.View
{
    public partial class AbnormityOrderForm : OpView.View.ToolbarForm
    {
        private OrderDal orderDal = new OrderDal();
        private string BatchNo = "";
        public AbnormityOrderForm()
        {
            InitializeComponent();
            //this.dgvDetail.AutoGenerateColumns = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (bsMaster.DataSource == null)
            {
                bsMaster.DataSource = orderDal.FindAbnormityOrder();
            }
            else
            {
                DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dgvMaster);
            }
        }

        private void btnToExcel_Click(object sender, EventArgs e)
        {
            if (bsMaster.DataSource == null)
                bsMaster.DataSource = orderDal.FindAbnormityOrder();
            //ExcelHelper.DoExport((DataTable)bsMaster.DataSource);
            DataTable dt = (DataTable)bsMaster.DataSource;
            if (dt.Rows.Count > 0)
            {
                BatchNo = dt.Rows[0]["BATCHNO"].ToString();
                DataView dv = dt.DefaultView;
                dv.RowFilter = string.Format("BATCHNO='{0}'",BatchNo);
                dv.Sort = "SORTNO";
                cExportExcel(dv);
            }
        }
        public void cExportExcel(DataView dv)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt(*.txt)|*.txt";
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.CreatePrompt = true;
            saveFileDialog1.Title = "导出txt文件到 ";

            DateTime now = DateTime.Now;
            //saveFileDialog1.FileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            saveFileDialog1.FileName = BatchNo;
            //now.Year.ToString().PadLeft(2)+now.Month.ToString().PadLeft(2, '0 ') +now.Day.ToString().PadLeft(2, '0 ')+ "_ " +now.Hour.ToString().PadLeft(2, '0 ') +now.Minute.ToString().PadLeft(2, '0 ') +

            saveFileDialog1.ShowDialog();

            Stream myStream;
            myStream = saveFileDialog1.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));

            String str = "";
            //写标题
            //for (int i = 0; i < dv.Table.Columns.Count; i++)
            //{
            //    if (i > 0)
            //    {
            //        str += "\t ";
            //    }
            //    str += dv.Table.Columns[i].ColumnName;
            //}
            //sw.WriteLine(str);

            //写内容
            for (int rowNo = 0; rowNo < dv.Count; rowNo++)
            {
                String tempstr = "";
                for (int columnNo = 0; columnNo < dv.Table.Columns.Count; columnNo++)
                {
                    if (columnNo > 0)
                    {
                        tempstr += "\t";
                    }

                    //tempstr+=dg.Rows[rowNo,columnNo].ToString();
                    tempstr += dv.Table.Rows[rowNo][columnNo].ToString();
                }
                sw.WriteLine(tempstr);
            }
            sw.Close();
            myStream.Close();
        }
        
    }
}

