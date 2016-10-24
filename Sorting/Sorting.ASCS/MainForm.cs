using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MCP;
using System.IO;
using System.Diagnostics;
using System.Timers;

namespace Sorting.ASCS
{
    public partial class MainForm : Form
    {
        private Rectangle tabArea;
        private RectangleF tabTextArea;
        private Context context = null;
        private System.Timers.Timer tmWorkTimer = new System.Timers.Timer();
        
        public MainForm()
        {            
            InitializeComponent();
        }

        private void CreateDirectory(string directoryName)
        {
            if (!System.IO.Directory.Exists(directoryName))
                System.IO.Directory.CreateDirectory(directoryName);
        }

        private void WriteLoggerFile(string text)
        {
            try
            {
                string path = "";
                CreateDirectory("日志");
                path = "日志";
                path = path + @"/" + DateTime.Now.ToString().Substring(0, 4).Trim();
                CreateDirectory(path);
                path = path + @"/" + DateTime.Now.ToString("yyyy-MM-dd").Substring(0, 7).Trim();
                path = path.TrimEnd(new char[] { '-' });
                CreateDirectory(path);
                path = path + @"/" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                StreamWriter sw = File.AppendText(path);
                sw.WriteLine(string.Format("{0} {1}", DateTime.Now, text));
                sw.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        void Logger_OnLog(MCP.LogEventArgs args)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new LogEventHandler(Logger_OnLog), args);
            }
            else
            {
                lock (lbLog)
                {
                    string msg = string.Format("[{0}] {1} {2}", args.LogLevel, DateTime.Now, args.Message);
                    lbLog.Items.Insert(0, msg);
                    WriteLoggerFile(msg);
                }
            }
        }
        void Order_OnLog(MCP.OrderEventArgs args)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new OrderEventHandler(Order_OnLog), args);
            }
            else
            {
                lock (this.dgvDetail1)
                {
                    DataSet ds = args.dsOrder;
                    if (ds.Tables.Count > 1)
                    {
                        DataTable dt1 = ds.Tables[0];
                        //int SortNo = int.Parse(dt1.Rows[0]["SORTNO"].ToString());
                        
                        if (dt1.Rows.Count > 0)
                        {
                            if (args.RequestNo == "1")
                            {
                                this.txtCustomerName1.Text = dt1.Rows[0]["CUSTOMERNAME"].ToString();
                                this.txtRouteName1.Text = dt1.Rows[0]["ROUTENAME"].ToString();
                                this.txtOrderID1.Text = dt1.Rows[0]["ORDERID"].ToString();
                                this.txtSortNo1.Text = dt1.Rows[0]["SORTNO"].ToString();
                                this.txtAreaName1.Text = dt1.Rows[0]["AREANAME"].ToString();
                                this.txtQty11.Text = dt1.Rows[0]["QUANTITY1"].ToString();
                                this.bsDetail1.DataSource = ds.Tables[1];
                            }
                            else if (args.RequestNo == "2")
                            {
                                this.txtCustomerName2.Text = dt1.Rows[0]["CUSTOMERNAME"].ToString();
                                this.txtRouteName2.Text = dt1.Rows[0]["ROUTENAME"].ToString();
                                this.txtOrderID2.Text = dt1.Rows[0]["ORDERID"].ToString();
                                this.txtSortNo2.Text = dt1.Rows[0]["SORTNO"].ToString();
                                this.txtAreaName2.Text = dt1.Rows[0]["AREANAME"].ToString();
                                this.txtQty21.Text = dt1.Rows[0]["QUANTITY1"].ToString();
                                this.bsDetail2.DataSource = ds.Tables[1];
                            }
                            else if (args.RequestNo == "3")
                            {
                                this.txtCustomerName3.Text = dt1.Rows[0]["CUSTOMERNAME"].ToString();
                                this.txtRouteName3.Text = dt1.Rows[0]["ROUTENAME"].ToString();
                                this.txtOrderID3.Text = dt1.Rows[0]["ORDERID"].ToString();
                                this.txtSortNo3.Text = dt1.Rows[0]["SORTNO"].ToString();
                                this.txtAreaName3.Text = dt1.Rows[0]["AREANAME"].ToString();
                                this.txtQty31.Text = dt1.Rows[0]["QUANTITY1"].ToString();

                                this.bsDetail3.DataSource = ds.Tables[1];
                            }
                            else if (args.RequestNo == "4")
                            {
                                this.txtCustomerName4.Text = dt1.Rows[0]["CUSTOMERNAME"].ToString();
                                this.txtRouteName4.Text = dt1.Rows[0]["ROUTENAME"].ToString();
                                this.txtOrderID4.Text = dt1.Rows[0]["ORDERID"].ToString();
                                this.txtSortNo4.Text = dt1.Rows[0]["SORTNO"].ToString();
                                this.txtAreaName4.Text = dt1.Rows[0]["AREANAME"].ToString();
                                this.txtQty41.Text = dt1.Rows[0]["QUANTITY1"].ToString();
                                this.bsDetail4.DataSource = ds.Tables[1];
                            }
                        }                        
                    }
                }
            }
        }
        /// <summary>
        /// 自绘TabControl控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void tabLeft_DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    tabArea = tabLeft.GetTabRect(e.Index);
        //    tabTextArea = tabArea;
        //    Graphics g = e.Graphics;
        //    StringFormat sf = new StringFormat();
        //    sf.LineAlignment = StringAlignment.Center;
        //    sf.Alignment = StringAlignment.Center;
        //    Font font = this.tabLeft.Font;
        //    SolidBrush brush = new SolidBrush(Color.Black);
        //    g.DrawString(((TabControl)(sender)).TabPages[e.Index].Text, font, brush, tabTextArea, sf);  
        //}

        private void MainForm_Load(object sender, EventArgs e)
        {
            //tabLeft.DrawMode = TabDrawMode.OwnerDrawFixed;

            try
            {               
                Logger.OnLog += new LogEventHandler(Logger_OnLog);
                Order.OnOrder += new OrderEventHandler(Order_OnLog);
                if (Init())
                {
                    context = new Context();
                    context.RegisterProcessControl(sortingStatus);

                    ContextInitialize initialize = new ContextInitialize();
                    initialize.InitializeContext(context);

                    context.RegisterProcessControl(monitorView);
                    context.RegisterProcessControl(buttonArea);
                    context.ProcessDispatcher.WriteToProcess("CurrentOrderProcess", "OrderFinish2", new int[] { -1 });

                    tmWorkTimer.Interval = 3000;
                    tmWorkTimer.Elapsed += new ElapsedEventHandler(tmWorker);
                    tmWorkTimer.Start();
                }

            }
            catch (Exception ex)
            {
                Logger.Error("初始化处理失败请检查配置，原因：" + ex.Message);
            }
        }
        private void tmWorker(object sender, ElapsedEventArgs e)
        {
            try
            {
                tmWorkTimer.Stop();

                Sorting.Dispatching.Dal.OrderDal orderDal = new Dispatching.Dal.OrderDal();
                DataTable dt = orderDal.GetSortingOrder().Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string requestNo = (i + 1).ToString();
                    string batchNo = dt.Rows[i]["BATCHNO"].ToString();
                    string SortNo = dt.Rows[i]["SORTNO"].ToString();
                    DataSet ds = orderDal.GetOrder(batchNo,SortNo);
                    Order.OrderInfo(requestNo,ds);
                }
            }
            finally
            {
                tmWorkTimer.Start();
            }
        }        
        
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (context != null)
            {
                context.Release();
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseReason abc = e.CloseReason;
            if (abc == CloseReason.UserClosing)
                e.Cancel = true;
        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            //lblTitle.Left = (pnlTitle.Width - lblTitle.Width) / 2;
        }

        #region  程序运行控制只允许一个进程运行。

        string appName = "Sorting.ASCS";

        private bool Init()
        {
            if (System.Diagnostics.Process.GetProcessesByName(appName).Length > 1)
            {
                if (MessageBox.Show("程序已启动，将自动退出本程序！", appName , MessageBoxButtons.OK).ToString() == "OK")
                {
                    Application.Exit();
                    return false;
                }
            }
            return true;
        }

        #endregion        
    }
}