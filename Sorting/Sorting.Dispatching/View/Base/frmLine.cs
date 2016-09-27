using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sorting.Dispatching.Dal;

namespace Sorting.Dispatching.View.Base
{
    public partial class frmLine : OpView.View.ToolbarForm
    {
        LineInfoDal dal;
        public frmLine()
        {
            InitializeComponent();
            dal = new LineInfoDal();
            Util.GridUtil.EnableFilter(dgvMain);
        }
        private void pnlMain_ParentChanged(object sender, EventArgs e)
        {
            try
            {
                bsMain.DataSource = dal.FindAll();
            }
            catch (Exception exp)
            {
                MessageBox.Show("读取配送区域信息失败，原因" + exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bsMain_PositionChanged(object sender, EventArgs e)
        {

        }
        

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            bsMain.DataSource = dal.FindAll();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            string url = this.mainFrame.Context.Attributes["LogisticsUrl"].ToString();
            DRProxy.LmsSortDataServiceService client = new DRProxy.LmsSortDataServiceService(url);

            DataSet ds = dal.GetAll();
            ds.DataSetName = "DATASETS";
            ds.Tables[0].TableName = "DATASET";
            //string xml = XmlDatasetConvert.ConvertDataSetToXML(ds);
            //string xml = XmlDatasetConvert.table2xml(ds.Tables[0]);
            string xml = XmlDatasetConvert.ConvertDataTableToXml(ds.Tables[0]);
            //xml = "<?xml version=\"1.0\" encoding=\"gb2312\"?><DATASETS><DATASET><AREA_CODE>-1</AREA_CODE><SORTLINE_CODE>3</SORTLINE_CODE><SORTLINE_NAME>中山创达半自动分拣线</SORTLINE_NAME><SORTLINE_TYPE>2</SORTLINE_TYPE><ABILITY>12000</ABILITY><ProductTotal>75</ProductTotal><PackCapacity>25</PackCapacity><STATUS>1</STATUS></DATASET></DATASETS>";
            string strxml = client.transSortLine(xml, "0");
            if(strxml.Substring(0,1)=="Y")
                MessageBox.Show("上报成功,返回信息" + strxml, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("上报失败,返回信息" + strxml, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

