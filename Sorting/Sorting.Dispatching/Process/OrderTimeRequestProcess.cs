using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MCP;
using Sorting.Dispatching.Dao;
using DB.Util;
using Sorting.Dispatching.Util;
using System.Timers;

namespace Sorting.Dispatching.Process
{
    public class OrderTimeRequestProcess : AbstractProcess
    {
        private Timer tmWorkTimer = new Timer();
        private bool bRequest = false;
        public override void Initialize(Context context)
        {
            try
            {
                base.Initialize(context);
                tmWorkTimer.Interval = 1000;
                tmWorkTimer.Elapsed += new ElapsedEventHandler(tmWorker);
                tmWorkTimer.Start();
            }
            catch (Exception e)
            {
                Logger.Error("OrderRequestProcess 初始化失败！原因：" + e.Message);
            }

        }
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                switch (stateItem.ItemName)
                {
                    //开始分拣，主动调用。
                    case "OrderRequest":
                        bRequest = (int)stateItem.State == 1;
                        break;
                }
            }

            catch (Exception e)
            {
                Logger.Error("处理出库数据失败，原因：" + e.Message);
            }
        }
        private void tmWorker(object sender, ElapsedEventArgs e)
        {
            try
            {
                tmWorkTimer.Stop();

                //检查条件是否满足
                if (bRequest)
                {
                    //通道机补货
                    Worker3();
                    //立式机补货
                    Worker2();
                }
            }
            finally
            {
                tmWorkTimer.Start();
            }
        }
        private void Worker3()
        {
            object[] o = ObjectUtil.GetObjects(WriteToService("SortPLC", "OrderRequest3"));
            if (o == null)
                return;

            int RequestCount = int.Parse(o[0].ToString());

            //如果补空位数为0,不处理
            if (RequestCount <= 0)
                return;

            object[] obj = ObjectUtil.GetObjects(WriteToService("SortPLC", "OrderData3"));
            if (obj == null)
                return;
            int reqeustNo = int.Parse(obj[26].ToString());
            //如果完成标志不为0，不处理
            if (reqeustNo > 0)
                return;

            Write2PLC("3");
        }
        private void Worker2()
        {
            object[] o = ObjectUtil.GetObjects(WriteToService("SortPLC", "OrderRequest2"));
            if (o == null)
                return;

            int RequestCount = int.Parse(o[0].ToString());

            //如果请求位为0,不处理
            if (RequestCount <= 0)
                return;

            object[] obj = ObjectUtil.GetObjects(WriteToService("SortPLC", "OrderData2"));
            if (obj == null)
                return;
            int reqeustNo = int.Parse(obj[81].ToString());
            //如果读取到流水号不为0，不处理
            if (reqeustNo > 0)
                return;

            Write2PLC("2");
        }
        private void Write2PLC(string channelType)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                try
                {
                    DataTable masterTable = orderDao.FindSortMaster(channelType);

                    if (masterTable.Rows.Count != 0)
                    {
                        //当前流水号
                        string sortNo = masterTable.Rows[0]["SORTNO"].ToString();

                        string maxSortNo = orderDao.FindMaxSortNo(channelType);

                        //查询订单明细                                
                        DataTable detailTable = orderDao.FindSortDetail(sortNo, channelType);

                        int[] orderData = new int[27];
                        if (channelType == "2")
                            orderData = new int[82];

                        int quantity = 0;
                        if (detailTable.Rows.Count > 0)
                        {
                            for (int i = 0; i < detailTable.Rows.Count; i++)
                            {
                                orderData[Convert.ToInt32(detailTable.Rows[i]["CHANNELADDRESS"]) - 1] = Convert.ToInt32(detailTable.Rows[i]["QUANTITY"]);
                                quantity += Convert.ToInt32(detailTable.Rows[i]["QUANTITY"]);
                            }
                        }

                        if (channelType == "3")
                        {
                            //订单数量
                            orderData[25] = quantity;
                            //分拣流水号
                            orderData[26] = Convert.ToInt32(sortNo);
                        }
                        else
                        {
                            //订单数量
                            orderData[80] = quantity;
                            //分拣流水号
                            orderData[81] = Convert.ToInt32(sortNo);
                        }

                        if (WriteToService("SortPLC", "OrderData" + channelType, orderData))
                        {
                            orderDao.UpdateOrderStatus(sortNo, "1", channelType);
                            DataSet ds = orderDao.GetOrder(sortNo);
                            //Order.OrderInfo(ds);
                            Logger.Info(string.Format((channelType == "3" ? "通道机" : "立式机") + "下发订单数据成功,分拣订单号[{0}]。", sortNo));

                            //写入每个通道机最后一订单号
                            DataTable dt = orderDao.FindChannelMaxSortNo();
                            int[] channelSortNo = new int[dt.Rows.Count];
                            for (int i = 0; i < dt.Rows.Count; i++)
                                channelSortNo[i] = int.Parse(dt.Rows[i]["SORTNO"].ToString());
                            WriteToService("SortPLC", "LastChannelSortNo", channelSortNo);
                        }
                        //是否到最后一单
                        if (sortNo == maxSortNo)
                        {
                            WriteToService("SortPLC", "LastOrder" + channelType, 1);
                            Logger.Info(string.Format((channelType == "3" ? "通道机" : "立式机") + "完成标志已写入,分拣订单号[{0}]。", sortNo));
                            //更新最大流水号之后数量为0的单据
                            orderDao.UpdateOrderStatus(channelType);
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(string.Format((channelType == "3" ? "通道机" : "立式机") + " 线 下发订单数据失败，原因：{0}！ {1}", e.Message, "OrderRequestProcess.cs 行号：100！"));
                }
            }
        }        
    }
}
