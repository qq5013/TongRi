using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sorting.Dispatching.View.Report
{
    public partial class frmReport : Form
    {
        private string reportName;
        private DataSet FDataSet;
        public frmReport()
        {
            InitializeComponent();
        }
        public frmReport(DataSet ds, string reportName)
        {
            InitializeComponent();
            this.FDataSet = ds;
            this.reportName = reportName;
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            FastReport.Report FReport = new FastReport.Report();
            FReport.Preview = preview1;            
            
            FReport.Load(reportName);
            FReport.RegisterData(FDataSet);
            FReport.Prepare();
            FReport.ShowPrepared();
        }
    }
}
