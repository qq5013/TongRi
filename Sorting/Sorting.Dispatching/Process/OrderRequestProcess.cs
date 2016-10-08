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
                if (stateItem.ItemName == "abc")
                    return;

                string channelType = stateItem.ItemName.Substring(12, 1);



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

            int AFlag = int.Parse(oa[0].ToString());
            int BFlag = int.Parse(ob[0].ToString());

            //如果补空位数为0,不处理
            if (AFlag > 0 || BFlag > 0)
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

                    string maxSortNo = orderDao.FindMaxSortNo(channelType);

                    //查询订单明细                                
                    DataTable detailTableA = orderDao.FindSortDetail(sortNo, channelType, 0);

                    int[] orderDataA = new int[250];
                    int indexA = 0;

                    if (detailTableA.Rows.Count > 0)
                    {
                        for (int i = 0; i < detailTableA.Rows.Count; i++)
                        {
                            //仓位地址
                            int channelAddressA = Convert.ToInt32(detailTableA.Rows[i]["CHANNELADDRESS"]);
                            int lastAddressA = Convert.ToInt32(detailTableA.Rows[i]["LASTCHANNELADDRESS"]);
                            orderDataA[indexA] = channelAddressA;
                            //仓位数量
                            orderDataA[indexA++] = Convert.ToInt32(detailTableA.Rows[i]["QUANTITY"]);
                            //订单序号
                            orderDataA[indexA++] = Convert.ToInt32(detailTableA.Rows[i]["SORTNO"]);
                            //订单数量
                            orderDataA[indexA++] = Convert.ToInt32(detailTableA.Rows[i]["ORDERQUANTITY"]);
                            //结束标志
                            if (maxSortNo == sortNo && channelAddressA == lastAddressA)
                                orderDataA[indexA++] = 1;
                            else
                                orderDataA[indexA++] = 0;
                        }
                    }

                    if (WriteToService("SortPLC", "ASortOrder", orderDataA))
                    {
                        orderDao.UpdateOrderStatus(sortNo, "1", channelType);
                        Logger.Info(string.Format("A线下发订单数据成功,分拣订单号[{0}]。", sortNo));

                    }

                    //查询订单明细                                
                    DataTable detailTableB = orderDao.FindSortDetail(sortNo, channelType, 1);

                    int[] orderDataB = new int[250];
                    int indexB = 0;

                    if (detailTableB.Rows.Count > 0)
                    {
                        for (int i = 0; i < detailTableB.Rows.Count; i++)
                        {
                            //仓位地址
                            int channelAddressB = Convert.ToInt32(detailTableA.Rows[i]["CHANNELADDRESS"]);
                            int lastAddressB = Convert.ToInt32(detailTableA.Rows[i]["LASTCHANNELADDRESS"]);
                            orderDataB[indexB] = channelAddressB;
                            //仓位数量
                            orderDataB[indexB++] = Convert.ToInt32(detailTableB.Rows[i]["QUANTITY"]);
                            //订单序号
                            orderDataB[indexB++] = Convert.ToInt32(detailTableB.Rows[i]["SORTNO"]);
                            //订单数量
                            orderDataB[indexB++] = Convert.ToInt32(detailTableB.Rows[i]["ORDERQUANTITY"]);
                            //结束标志
                            if (maxSortNo == sortNo && channelAddressB == lastAddressB)
                                orderDataB[indexA++] = 1;
                            else
                                orderDataB[indexA++] = 0;
                        }
                    }

                    if (WriteToService("SortPLC", "BSortOrder", orderDataB))
                    {
                        orderDao.UpdateOrderStatus(sortNo, "1", channelType);
                        Logger.Info(string.Format("B线下发订单数据成功,分拣订单号[{0}]。", sortNo));

                    }

                }
            }
        }
    }
}
