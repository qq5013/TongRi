using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.IO;
using MCP;
using MCP.View;
using DB.Util;
using Sorting.Dispatching.Dao;
using Sorting.Dispatching.Dal;
using System.Timers;

namespace Sorting.Dispatching.View
{
    public partial class ButtonArea : ProcessControl
    {
        string OrderDate = "";
        string BatchNo = "";
        string lineCode = "";
        private System.Timers.Timer tmWorkTimer = new System.Timers.Timer();

        public ButtonArea()
        {
            InitializeComponent();
            tmWorkTimer.Interval = 300000;
            tmWorkTimer.Elapsed += new ElapsedEventHandler(tmWorker);            
        }
        private void tmWorker(object sender, ElapsedEventArgs e)
        {
            try
            {
                tmWorkTimer.Stop();

                OrderDal dal = new OrderDal();
                string url = Context.Attributes["LogisticsUrl"].ToString();
                DRProxy.LmsSortDataServiceService client = new DRProxy.LmsSortDataServiceService(url);

                DataSet ds = dal.FindOrderStatus();
                ds.DataSetName = "DATASETS";
                ds.Tables[0].TableName = "DATASET";

                string xml = XmlDatasetConvert.ConvertDataTableToXml(ds.Tables[0]);

                string strxml = client.transOrderStatus(xml, "0");

                if (strxml.Substring(0, 1) == "Y")
                    Logger.Info("上传物流系统订单状态成功,返回信息" + strxml);
                else
                    Logger.Info("上传物流系统订单状态失败,返回信息" + strxml);
            }
            catch (Exception ex)
            {
                Logger.Error("上传物流系统订单状态时发生错误,原因:" + ex.Message);
            }
            finally
            {
                
                tmWorkTimer.Start();
            }
        }        
        private void btnExit_Click(object sender, EventArgs e)
        {

            if (btnStop.Enabled)
            {
                MessageBox.Show("先停止分拣作业才能退出系统。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (DialogResult.Yes == MessageBox.Show("您确定要退出分拣监控系统吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Util.LogFile.DeleteFile();
                System.Environment.Exit(0);
            }
        }

        private void btnOperate_Click(object sender, EventArgs e)
        {
            try
            {
                OpView.Config config = new OpView.Config();
                OpView.View.MainFrame mainFrame = new OpView.View.MainFrame(config);
                mainFrame.ShowInTaskbar = false;
                mainFrame.Icon = new Icon(@"./App.ico");
                mainFrame.ShowIcon = true;
                mainFrame.StartPosition = FormStartPosition.CenterScreen;
                mainFrame.WindowState = FormWindowState.Maximized;
                mainFrame.Context = Context;
                mainFrame.ShowDialog();
            }
            catch (Exception ee)
            {
                Logger.Error("操作作业失败！原因：" + ee.Message);
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            lock (this)
            {
                if (btnDownload.Enabled == true)
                {
                    UploadDataDal udal = new UploadDataDal();
                    DataTable sortTable = udal.GetSortUploadData("1");
                    if (sortTable == null || sortTable.Rows.Count == 0)
                    {
                        btnDownload.Enabled = false;

                        DownloadData();
                        btnDownload.Enabled = true;
                    }
                    else
                        MessageBox.Show("分拣情况表数据没有上报！请确认上报数据，再下载！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void btnHandSort_Click(object sender, EventArgs e)
        {
            OrderDal dal = new OrderDal();
            //是否还有未分拣的数据
            if (dal.FindUnsortCount() != 0)
            {
                MessageBox.Show("还有未分拣的数据，您先处理未分拣的数据!", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int Flag = 2;
            if (!ShowDialog(Flag))
                return;
            
            Base.frmHandSort f = new Base.frmHandSort(BatchNo);
            f.ShowDialog();
        }
        private void btnPackData_Click(object sender, EventArgs e)
        {
            int Flag = 4;
            if (!ShowDialog(Flag))
                return;
            Context.ProcessDispatcher.WriteToProcess("CreatePackAndPrintDataProcess", "NewData", BatchNo);
            //btnPackData.Enabled = false;
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                int Flag = 5;
                if (!ShowDialog(Flag))
                    return;

                Sorting.Dispatching.Schedule.UploadData uploadData = new Sorting.Dispatching.Schedule.UploadData();
                Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("创建文件", 5, 1));
                if (uploadData.CreateDataFile(OrderDate, BatchNo))
                {
                    Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("压缩文件", 5, 2));
                    uploadData.CreateZipFile();

                    Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("上传文件", 5, 3));
                    uploadData.SendZipFile();
                }

                Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("更新状态", 5, 4));
                uploadData.SaveUploadStatus(BatchNo);

                Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("删除文件", 5, 5));
                uploadData.DeleteFiles();

                //Sorting.Dispatching.Schedule.UploadData uploadData1 = new Sorting.Dispatching.Schedule.UploadData(true);
                //Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("创建文件", 5, 1));
                //if (uploadData1.CreateDataFile(OrderDate, BatchNo))
                //{

                //    Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("压缩文件", 5, 2));
                //    uploadData1.CreateZipFile();

                //    Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("上传文件", 5, 3));
                //    uploadData1.SendZipFile();
                //}

                //Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("更新状态", 5, 4));
                //uploadData1.SaveUploadStatus(BatchNo);

                //Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("删除文件", 5, 5));
                //uploadData1.DeleteFiles();

                Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState());

                //更新上传状态
                BatchDal dal = new BatchDal();
                dal.UpdateNoOnePro(BatchNo, "Admin");
            }
            catch (Exception ex)
            {
                Logger.Error("上传一号工程时发生错误,原因:" + ex.Message);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Context.ProcessDispatcher.WriteToProcess("LEDProcess", "Start", null);
            if (Context.Processes["OrderRequestProcess"] != null)
            {
                Context.Processes["OrderRequestProcess"].Resume();
            }

            if (Context.Processes["MergeRequestProcess"] != null)
            {
                Context.Processes["MergeRequestProcess"].Resume();
            }

            //if (Context.Processes["BreakRecordProcess"] != null)
            //{
            //    Context.Processes["BreakRecordProcess"].Resume();
            //}
            //Context.ProcessDispatcher.WriteToProcess("OrderRequestProcess", "OrderRequest3", 0);
            //Context.ProcessDispatcher.WriteToProcess("OrderRequestProcess", "OrderRequest2", 0);
            //Context.ProcessDispatcher.WriteToProcess("MergeRequestProcess", "MergeRequest", 0);

            //Context.ProcessDispatcher.WriteToProcess("OrderTimeRequestProcess", "OrderRequest", 1);
            
            //给PLC结束标志
            Context.ProcessDispatcher.WriteToService("SortPLC", "StopOrderRequest", 0);
            SwitchStatus(true);
            tmWorkTimer.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (Context.Processes["OrderRequestProcess"] != null)
            {
                Context.Processes["OrderRequestProcess"].Suspend();
            }

            if (Context.Processes["MergeRequestProcess"] != null)
            {
                Context.Processes["MergeRequestProcess"].Suspend();
            }
            //if (Context.Processes["BreakRecordProcess"] != null)
            //{
            //    Context.Processes["BreakRecordProcess"].Suspend();
            //}
            //给PLC结束标志
            Context.ProcessDispatcher.WriteToService("SortPLC", "StopOrderRequest", 1);
            Context.ProcessDispatcher.WriteToProcess("OrderTimeRequestProcess", "OrderRequest", 0);
            SwitchStatus(false);
            tmWorkTimer.Stop();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            Context.Processes["LEDProcess"].Resume();
            Context.ProcessDispatcher.WriteToProcess("LEDProcess", "Check", null);
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "help.chm");
        }

        private void SwitchStatus(bool isStart)
        {
            btnDownload.Enabled = !isStart;
            this.btnUploadLogistics.Enabled = !isStart;
            //btnHandSort.Enabled = !isStart;
            btnUpload.Enabled = !isStart;
            btnStart.Enabled = !isStart;
            btnOptimize.Enabled = !isStart;
            btnStop.Enabled = isStart;
        }
        private bool ShowDialog(int Flag)
        {
            frmOrderDialog f = new frmOrderDialog(Flag, OrderDate);
            if (f.ShowDialog() == DialogResult.OK)
            {
                OrderDate = f.OrderDate;
                BatchNo = f.BatchNo;
                return true;
            }
            return false;
        }
        private void DownloadData()
        {
            string[] batchNo;
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                //是否还有未分拣的数据                
                if (orderDao.FindUnsortCount() != 0)
                {
                    if (orderDao.FindSortedCount()>0)
                    {
                        MessageBox.Show("还有未分拣的数据，您先处理未分拣的数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                int Flag = 1;
                frmOrderDateDialog f = new frmOrderDateDialog(Flag, OrderDate);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    batchNo = new string[f.dataGridView1.Rows.Count];
                    if (batchNo.Length <= 0)
                        return;
                    else
                    {
                        OrderDate = f.OrderDate;
                        for (int i = 0; i < f.dataGridView1.Rows.Count; i++)
                        {
                            if (f.dataGridView1.Rows[i].Cells[0].Value == null)
                                batchNo[i] = "";
                            else
                            {
                                if (bool.Parse(f.dataGridView1.Rows[i].Cells[0].Value.ToString()))
                                    batchNo[i] = f.dataGridView1.Rows[i].Cells[1].Value.ToString();
                                else
                                    batchNo[i] = "";
                            }
                        }
                    }
                }
                else
                    return;
                //if (!ShowDialog(Flag))
                //    return;

                BatchDao batchDao = new BatchDao();
                //DataTable batchTable = batchDao.FindBatch(OrderDate, BatchNo);
                DataTable batchTable = batchDao.FindBatch(OrderDate);
                if (batchTable.Rows.Count > 0)
                {
                    if (MessageBox.Show("该日期有已经优化的数据，是否继续下载。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        return;
                }
                //是否已优化
                //if (batchTable.Rows[0]["ISVALID"].ToString().Trim() == "1")
                //{
                //    if (MessageBox.Show("该批数据已优化，请选择未优化批次。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                //        return;
                //}
                for (int i = 0; i < batchNo.Length; i++)
                {
                    BatchNo = batchNo[i];

                    if (BatchNo.Length <= 0)
                        continue;
                    //ServerDao serverDao = new ServerDao();
                    //DataTable table = serverDao.FindBatch(lineCode);
                    //if (table.Rows.Count == 0)
                    //{
                    //    MessageBox.Show("没有需要分拣的订单数据。", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}

                    DateTime dtOrder = DateTime.Parse(OrderDate);
                    string historyDate = dtOrder.AddDays(-30).ToShortDateString();
                    DataTable detailTable;
                    string result = "";
                    DRProxy.LmsSortDataServiceService client;

                    try
                    {
                        //client = new DRProxy.LmsSortDataServiceService();
                        string url = Context.Attributes["LogisticsUrl"].ToString();
                        client = new DRProxy.LmsSortDataServiceService(url);

                        //下载订单明细
                        Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("批量转入订单[" + BatchNo + "]", 14, 1));
                        result = client.transSortOrder(BatchNo);
                        detailTable = GetDataSet(result);
                        if (detailTable == null)
                        {
                            MessageBox.Show("该批次无订单数据,请核查", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState());
                            return;
                        }

                        orderDao.BatchInsertOrder(detailTable, BatchNo);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("批量转入订单数据失败！原因：" + ex.Message);
                        return;
                    }

                    try
                    {
                        pm.BeginTransaction();

                        //更改状态
                        batchDao.UpdateIsValid(OrderDate, BatchNo, "0");
                        batchDao.UpdateExecuter("", "", OrderDate, BatchNo);
                        batchDao.UpdateEntity(OrderDate, BatchNo, "", "0");
                        //清除优化数据
                        orderDao.DeleteHandSort(BatchNo);

                        //CMD_BATCH
                        batchDao.DeleteHistory(historyDate);
                        Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("删除历史批次数据", 14, 2));

                        //SC_CHANNELUSED
                        ChannelScheduleDao csDao = new ChannelScheduleDao();
                        csDao.DeleteHistory(historyDate);
                        Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("删除历史烟道数据", 14, 3));

                        //SC_LINE
                        LineScheduleDao lsDao = new LineScheduleDao();
                        lsDao.DeleteHistory(historyDate);
                        Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("删除分拣线历史数据", 14, 4));

                        //SC_PALLETMASTER ,SC_ORDER
                        OrderScheduleDao osDao = new OrderScheduleDao();
                        osDao.DeleteHistory(historyDate);
                        Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("删除分拣订单历史数据", 14, 5));

                        //SC_I_ORDERMASTER,SC_I_ORDERDETAIL,
                        orderDao.DeleteHistory(historyDate);
                        Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("删除下载订单历史数据", 14, 6));

                        //SC_STOCKMIXCHANNEL
                        StockChannelDao scDao = new StockChannelDao();
                        scDao.DeleteHistory(historyDate);
                        Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("删除补货烟道历史数据", 14, 7));

                        //SC_SUPPLY
                        SupplyDao supplyDao = new SupplyDao();
                        supplyDao.DeleteHistory(historyDate);
                        Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("删除补货历史数据", 14, 8));

                        //SC_HANDLESUPPLY
                        HandleSupplyDao handleSupplyDao = new HandleSupplyDao();
                        handleSupplyDao.DeleteHistory(historyDate);
                        Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("删除手动补货历史数据", 14, 9));

                        //ClearSchedule(orderDate, batchNo);

                        //////////////////////////////////////////////////////////////////////////

                        //下载区域表
                        //AreaDao areaDao = new AreaDao();
                        //DataTable areaTable = ssDao.FindArea();
                        //areaDao.SynchronizeArea(areaTable);
                        //Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("下载烟道表", 14, 10));                    

                        //下载配送线路表
                        Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("下载配送路线[" + BatchNo + "]", 14, 11));
                        RouteDao routeDao = new RouteDao();
                        //DataTable routeTable = ssDao.FindRoute();
                        result = client.transRoute(BatchNo);
                        DataTable routeTable = GetDataSet(result);
                        routeDao.SynchronizeRoute(routeTable);
                        routeDao.UpdateRouteSortID(BatchNo);

                        //下载客户表
                        Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("下载客户资料[" + BatchNo + "]", 14, 12));
                        CustomerDao customerDao = new CustomerDao();
                        result = client.transCustomer(BatchNo);
                        DataTable customerTable = GetDataSet(result);
                        customerDao.SynchronizeCustomer(customerTable, BatchNo);

                        //下载卷烟表 进行同步
                        Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("下载卷烟资料[" + BatchNo + "]", 14, 13));
                        CigaretteDao cigaretteDao = new CigaretteDao();
                        result = client.transProduct();
                        DataTable cigaretteTable = GetDataSet(result);
                        cigaretteDao.SynchronizeCigarette(cigaretteTable);

                        //下载订单资料
                        Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("下载订单资料[" + BatchNo + "]", 14, 14));
                        orderDao.BatchInsertOrder(BatchNo);

                        pm.Commit();

                        Logger.Info("批次号:" + BatchNo + "数据下载完成");
                        batchDao.UpdateDownload(BatchNo);
                        Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState());
                    }
                    catch (Exception e)
                    {
                        Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState());
                        pm.Rollback();
                        Logger.Error("批次号:" + BatchNo + "下载数据失败！原因：" + e.Message);
                        throw e;
                    }
                }
            }
        }
        private DataTable GetDataSet(string result)
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(new StringReader(result));
                reader.WhitespaceHandling = WhitespaceHandling.None;
                DataSet ds = new DataSet();
                ds.ReadXml(reader);
                if (ds.Tables.Count <= 0)
                    return null;
                DataTable dt = ds.Tables[0];                
                reader.Close();
                ds.Dispose();
                return dt;
            }
            catch (Exception err)
            {
                throw new Exception("GetDataSet方法异常：" + err.Message);
            }
        }
        private void btnOptimize_Click(object sender, EventArgs e)
        {
            BatchDal batchDal = new BatchDal();
            if (batchDal.IsExistsNoSorting())
            {
                MessageBox.Show("该批数据正在分拣，不可重新优化。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }
            //看是否正在补货           
            if (batchDal.IsExistsStartSupply())
            {
                if (MessageBox.Show("该批数据已开始补货，是否要重新优化,如重新优化请停止补货，且在补货操作台删除已下发的数据。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;
            }
            int Flag = 3;
            if (!ShowDialog(Flag))
                return;

            lineCode = "3";
            //lineCode = Context.Attributes["LineCode"].ToString();

            
            DataTable batchTable = batchDal.GetBatch(OrderDate, BatchNo);

            //是否已优化
            if (batchTable.Rows[0]["ISVALID"].ToString().Trim() == "1")
            {
                if (MessageBox.Show("该批数据已优化，是否要重新优化。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;
            }
            this.btnOptimize.Enabled = false;
            Logger.Info("数据优化开始");

            try
            {
                //查找上个优化分拣批次
                string preBatchNo = batchDal.GetPreBatchNo(BatchNo);
                if (preBatchNo.Length > 0)
                {
                    if (MessageBox.Show("是否要清除上批的余量?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int bno = int.Parse(preBatchNo.Substring(10, 3));
                        string firstBatchNo = BatchNo.Substring(0, 10) + "001";
                        ChannelDal cdal = new ChannelDal();
                        cdal.ClearChannelBalance(bno, firstBatchNo);
                    }
                }

                //int bno = int.Parse(BatchNo.Substring(10, 3));
                //string firstBatchNo = BatchNo.Substring(0, 10) + "001";
                //if (bno > 1)
                //{
                //    if (MessageBox.Show("是否要清除上批的余量?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //    {
                //        ChannelDal cdal = new ChannelDal();
                //        cdal.ClearChannelBalance(bno-1, firstBatchNo);
                //    }
                //}

                
                Optimize();

                using (PersistentManager pm = new PersistentManager())
                {
                    BatchDao batchDao = new BatchDao();
                    if (!batchDao.CheckOrder(OrderDate, BatchNo))
                    {
                        throw new Exception("优化过程出错，请检查！");
                    }
                    //更新已优化
                    batchDao.UpdateIsValid(OrderDate, BatchNo, "1");
                    string ip = "127.0.0.1"; //Dns.GetHostEntry(Dns.GetHostName()).AddressList[2].ToString();
                    batchDao.UpdateExecuter("Admin", ip, OrderDate, BatchNo);
                    int batchCount = batchDao.BatchCount(OrderDate, BatchNo);

                    //ServerDao serverDao = new ServerDao();
                    ////选择日期的时候有插入记录
                    //DataTable table = serverDao.FindBatch(lineCode);
                    //if (table.Rows.Count != 0)
                    //{
                    //    string batchID = table.Rows[0]["BATCHID"].ToString();
                    //    serverDao.UpdateBatchStatus(batchID, lineCode);                        
                    //}
                    //else
                    //{
                    //    //MessageBox.Show("没有需要分拣的订单数据。", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    //return;
                    //}
                }

                Logger.Info("数据优化完成");

                Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState());
                Context.ProcessDispatcher.WriteToProcess("LEDProcess", "NewData", null);
                //Context.ProcessDispatcher.WriteToProcess("CreatePackAndPrintDataProcess", "NewData", null);
                Context.ProcessDispatcher.WriteToProcess("CurrentOrderProcess", "OrderFinish3", new int[] { -1 });
                Context.ProcessDispatcher.WriteToProcess("SortingOrderProcess", "OrderInfo", new string[] { OrderDate, BatchNo });
            }
            catch (Exception ex)
            {
                Logger.Info("优化过程出现错误，信息:" + ex.Message);
                Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState());
            }
            this.btnOptimize.Enabled = true;
        }
        private void Optimize()
        {
            Sorting.Dispatching.Schedule.AutoSchedule schedule = new Sorting.Dispatching.Schedule.AutoSchedule();

            //临时表清除
            schedule.ClearSchedule(OrderDate, BatchNo);

            //Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("分拣线优化", 7, 1));
            schedule.GenLineSchedule(OrderDate, BatchNo);

            //Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("分拣货仓优化", 7, 2));
            schedule.GenChannelSchedule(OrderDate, BatchNo);

            //Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("拆分订单", 7, 3));
            schedule.GenSplitOrder(OrderDate, BatchNo);

            //Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("订单优化", 7, 4));
            //schedule.GenOrderSchedule(OrderDate, BatchNo);

            //Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("备货站台优化", 7, 5));
            schedule.GenStockChannelSchedule(OrderDate, BatchNo);

            //Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("补货优化", 7, 6));
            schedule.GenSupplySchedule1(OrderDate, BatchNo);

            //Context.ProcessDispatcher.WriteToProcess("monitorView", "ProgressState", new ProgressState("手工补货优化", 7, 7));
            schedule.GenHandleSupplySchedule1(OrderDate, BatchNo);
        }

        private void btnUploadLogistics_Click(object sender, EventArgs e)
        {
            int Flag = 6;
            if (!ShowDialog(Flag))
                return;

            OrderDal dal = new OrderDal();

            string url = Context.Attributes["LogisticsUrl"].ToString();
            DRProxy.LmsSortDataServiceService client = new DRProxy.LmsSortDataServiceService(url);

            DataSet ds = dal.FindOrderStatus(BatchNo);
            ds.DataSetName = "DATASETS";
            ds.Tables[0].TableName = "DATASET";

            string xml = XmlDatasetConvert.ConvertDataTableToXml(ds.Tables[0]);
            try
            {
                string strxml = client.transOrderStatus(xml, "0");
                if (strxml.Substring(0, 1) == "Y")
                {                    
                    //更新上传状态
                    BatchDal bdal = new BatchDal();
                    bdal.UpdateUpload(BatchNo, "1");
                    MessageBox.Show("上传物流系统订单状态成功,返回信息" + strxml, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.Info("上传物流系统订单状态成功,返回信息" + strxml);
                }
                else
                {
                    MessageBox.Show("上传物流系统订单状态失败,返回信息" + strxml, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.Info("上传物流系统订单状态失败,返回信息" + strxml);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("上传物流系统订单状态时发生错误,原因:" + ex.Message);
            }
        }
    }
}
