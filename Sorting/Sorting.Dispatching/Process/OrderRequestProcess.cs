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
    public class OrderRequestProcess : AbstractProcess
    {
        int OrderCount = 25;
        private Timer tmWorkTimer = new Timer();
        private bool bSort = false;

        public override void Initialize(Context context)
        {
            try
            {
                base.Initialize(context);
                OrderCount = int.Parse(context.Attributes["OrderCount"].ToString());

                tmWorkTimer.Interval = 1000;
                tmWorkTimer.Elapsed += new ElapsedEventHandler(tmWorker);
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
                if (stateItem.ItemName == "abc")
                    return;

                if (stateItem.ItemName == "Start")
                {
                    bSort = true;
                    tmWorkTimer.Start();
                }
                if (stateItem.ItemName == "Stop")
                {
                    bSort = false;
                    tmWorkTimer.Stop();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("分拣订单请求操作失败！原因：{0}！ {1}", ex.Message, "OrderRequestProcess.cs 行号：108！"));
            }
        }
        private void tmWorker(object sender, ElapsedEventArgs e)
        {
            try
            {
                tmWorkTimer.Stop();

                //检查条件是否满足
                if (bSort)
                {
                    //立式机分拣
                    Worker2();
                }
            }
            finally
            {
                tmWorkTimer.Start();
            }
        }
        private void Worker2()
        {
            //return;
            object[] oa = ObjectUtil.GetObjects(WriteToService("SortPLC", "ASortOrderFlag"));
            if (oa == null)
                return;
            object[] ob = ObjectUtil.GetObjects(WriteToService("SortPLC", "BSortOrderFlag"));
            if (ob == null)
                return;
            //运行状态
            object[] os = ObjectUtil.GetObjects(WriteToService("SortPLC", "DeviceRunState"));
            if (ob == null)
                return;

            int AFlag = int.Parse(oa[0].ToString());
            int BFlag = int.Parse(ob[0].ToString());
            int State = int.Parse(os[0].ToString());

            //运行状态
            //if (State != 1)
            //    return;
            //如果补空位数为0,不处理
            if (AFlag > 0 || BFlag > 0 )
                return;

            string channelType = "2";

            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                DataTable masterTable = orderDao.FindSortMaster(channelType);

                if (masterTable.Rows.Count != 0)
                {
                    //当前流水号
                    string sortNo = masterTable.Rows[0]["SORTNO"].ToString();

                    //计算当前流水号剩下订单量以及通道剩余量

                    DataTable dtMax = orderDao.FindMaxSortNoChannelAddress();
                    if (dtMax.Rows.Count <= 0)
                        return;
                    //string maxSortNo = orderDao.FindMaxSortNo(channelType);

                    string maxSortNo = dtMax.Rows[0]["SortNo"].ToString();
                    //批次分拣结束，找AB仓最大的仓位地址，判断分拣结束的标志,未到最后一笔订单号的标志位都为0
                    int channelAddress = int.Parse(dtMax.Rows[0]["ChannelAddress"].ToString());

                    //查询订单明细                                
                    DataTable detailTableA = orderDao.FindSortDetail(sortNo, channelType, 0);

                    int[] orderDataA = new int[250];
                    int indexA = 0;

                    for (int i = 0; i < detailTableA.Rows.Count; i++)
                    {
                        //仓位地址
                        int channelAddressA = Convert.ToInt32(detailTableA.Rows[i]["CHANNELADDRESS"]);
                        int lastAddressA = Convert.ToInt32(detailTableA.Rows[i]["LASTCHANNELADDRESS"]);
                        orderDataA[indexA++] = channelAddressA;
                        //仓位数量
                        orderDataA[indexA++] = Convert.ToInt32(detailTableA.Rows[i]["QUANTITY"]);
                        //订单序号
                        orderDataA[indexA++] = int.Parse(sortNo);
                        //订单数量
                        orderDataA[indexA++] = Convert.ToInt32(detailTableA.Rows[i]["ORDERQUANTITY"]);
                        //结束标志
                        if (maxSortNo == sortNo && channelAddressA == channelAddress)
                            orderDataA[indexA++] = 1;
                        else
                            orderDataA[indexA++] = 0;
                    }
                    WriteToService("SortPLC", "ASortOrder", orderDataA);

                    Logger.Info(string.Format("A线下发订单数据成功,分拣订单号[{0}]。", sortNo));


                    //查询订单明细                                
                    DataTable detailTableB = orderDao.FindSortDetail(sortNo, channelType, 1);

                    int[] orderDataB = new int[250];
                    int indexB = 0;

                    for (int i = 0; i < detailTableB.Rows.Count; i++)
                    {
                        //仓位地址
                        int channelAddressB = Convert.ToInt32(detailTableB.Rows[i]["CHANNELADDRESS"]);
                        int lastAddressB = Convert.ToInt32(detailTableB.Rows[i]["LASTCHANNELADDRESS"]);
                        orderDataB[indexB++] = channelAddressB;
                        //仓位数量
                        orderDataB[indexB++] = Convert.ToInt32(detailTableB.Rows[i]["QUANTITY"]);
                        //订单序号
                        orderDataB[indexB++] = int.Parse(sortNo);
                        //订单数量
                        orderDataB[indexB++] = Convert.ToInt32(detailTableB.Rows[i]["ORDERQUANTITY"]);
                        //结束标志
                        if (maxSortNo == sortNo && channelAddressB == channelAddress)
                            orderDataB[indexB++] = 1;
                        else
                            orderDataB[indexB++] = 0;
                    }

                    WriteToService("SortPLC", "BSortOrder", orderDataB);
                    Logger.Info(string.Format("B线下发订单数据成功,分拣订单号[{0}]。", sortNo));

                    if (WriteToService("SortPLC", "ASortOrderFlag", 1) && WriteToService("SortPLC", "BSortOrderFlag", 1))
                    {
                        orderDao.UpdateOrderStatus(sortNo, "1", channelType);
                        //Logger.Info(string.Format("下发订单数据成功,分拣订单号[{0}]。", sortNo));
                    }
                }
            }
        }
    }
}
