using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MCP;
using Sorting.Dispatching.Dao;
using DB.Util;
using Sorting.Dispatching.Util;

namespace Sorting.Dispatching.Process
{
    public class OrderRequestProcess : AbstractProcess
    {
        int MinBalance = 50;
        public override void Initialize(Context context)
        {
            try
            {
                base.Initialize(context);
                MinBalance = int.Parse(context.Attributes["MinBalance"].ToString());
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
                //string requestNo = stateItem.ItemName.Substring(13, 1);                

                //读取是否中断分拣
                //object[] obj = ObjectUtil.GetObjects(WriteToService("SortPLC", "BreakSorting" + channelType));
                //if (obj == null)
                //    return;
                //if (obj[0] == "1")
                //    return;
                //读取A线订单明细并写给PLC
                object o = ObjectUtil.GetObject(stateItem.State);
                if (o == null)
                    return;
                //Logger.Info("请求类型:" + channelType + ",请求值:" + o.ToString());
                if (o != null && o.ToString() == "1")
                {
                    using (PersistentManager pm = new PersistentManager())
                    {
                        OrderDao orderDao = new OrderDao();
                        try
                        {
                            //要根据分拣线组查询数据
                            //string rno = requestNo;
                            //if (requestNo == "4")
                            //    rno = "0";
                            DataTable masterTable = orderDao.FindSortMaster(channelType);

                            if (masterTable.Rows.Count != 0)
                            { 
                                //当前流水号
                                string sortNo = masterTable.Rows[0]["SORTNO"].ToString();
                                
                                //计算当前流水号剩下订单量以及通道剩余量

                                string maxSortNo = orderDao.FindMaxSortNo(channelType);
                                
                                //if (int.Parse(sortNo) > int.Parse(maxSortNo))
                                //    return;
                                //查询订单明细                                
                                DataTable detailTable = orderDao.FindSortDetail(sortNo, channelType);
                                //查询本分拣线组最后流水号，判断是否结束
                                //string endSortNo = orderDao.FindEndSortNoForChannelGroup(channelType);
                                //int exportNo = Convert.ToInt32(masterTable.Rows[0]["EXPORTNO"]);
                                int[] orderData = new int[27];
                                if(channelType=="2")
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

                                //分拣包号
                                //orderData[89] = Convert.ToInt32(packNo);
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
                                    Logger.Info(string.Format((channelType=="3"?"通道机":"立式机") + "下发订单数据成功,分拣订单号[{0}]。", sortNo));

                                    //写入每个通道机最后一订单号
                                    //DataTable dt = orderDao.FindChannelMaxSortNo();

                                    DataTable dt = orderDao.FindChannelMaxSortNo(MinBalance);
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
                                //else
                                //{
                                //    string batchNo = masterTable.Rows[0]["BATCHNO"].ToString();
                                //    int bNo = int.Parse(batchNo.Substring(10, 3));
                                //    DataTable dtBalance = orderDao.GetChannelBalance(sortNo, bNo, batchNo);
                                //}
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Error(string.Format((channelType == "3" ? "通道机" : "立式机") + " 线 下发订单数据失败，原因：{0}！ {1}", e.Message, "OrderRequestProcess.cs 行号：100！"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("分拣订单请求操作失败！原因：{0}！ {1}", ex.Message, "OrderRequestProcess.cs 行号：108！"));
            }
        }
    }
}
