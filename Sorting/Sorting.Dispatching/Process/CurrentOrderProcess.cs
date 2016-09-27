using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MCP;
using Sorting.Dispatching.Util;
using Sorting.Dispatching.Dao;
using Sorting.Dispatching.View;
using DB.Util;

namespace Sorting.Dispatching.Process
{
    public class CurrentOrderProcess: AbstractProcess
    {

        public override void Initialize(Context context)
        {
            try
            {
                base.Initialize(context);
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("CurrentOrderProcess 初始化失败！原因：{0}！ {1}", e.Message, "CurrentOrderProcess.cs 行号：26！"));
            }

        }
        
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                string channelType = stateItem.ItemName.Substring(11, 1);

                object o = ObjectUtil.GetObject(stateItem.State);
                if (o == null)
                    return;

                string sortNo = o.ToString();
                //刷新分拣状态                    
                Refresh(sortNo, channelType);

                if (int.Parse(sortNo) > 0)
                {
                    //判断此流水号是否分拣完成
                    
                    string channelName = channelType == "3" ? "通道机" : "立式机";
                    Logger.Info("订单序号" + sortNo + channelName + "已完成");
                }
            }

            catch (Exception e)
            {
                Logger.Error(string.Format("完成订单信息处理失败！原因：{0}！ {1}", e.Message, "CurrentOrderProcess.cs 行号：53！"));
            }
        }

        private void Refresh(string sortNo, string channelType)
        {
            try
            {
                using (PersistentManager pm = new PersistentManager())
                {
                    OrderDao orderDao = new OrderDao();
                    if (sortNo == null)
                        sortNo = orderDao.FindLastSortNo(channelType);
                    
                    //更新完成时间
                    string maxSortNo = orderDao.FindLastSortNo();
                    orderDao.UpdateFinishTime(sortNo, channelType, maxSortNo);

                    
                    //if (sortNo !="-1")
                    //{
                    //    if (Convert.ToInt32(sortNo) >= Convert.ToInt32(orderDao.FindLastCustomerSortNo(channelType)))
                    //    {
                    //        orderDao.UpdateAllFinishTime(sortNo, channelType);
                    //    }
                    //}                    

                    //刷新主界面分拣状态
                    DataTable infoTable = orderDao.FindOrderInfo(null);
                    if (infoTable.Rows.Count <= 0)
                        return;
                    RefreshData refreshData = new RefreshData();
                    string batchNo = infoTable.Rows[0]["BATCHNO"].ToString();
                    refreshData.BatchNo = batchNo;
                    refreshData.TotalCustomer = Convert.ToInt32(infoTable.Rows[0]["CUSTOMERNUM"]);
                    refreshData.TotalRoute = Convert.ToInt32(infoTable.Rows[0]["ROUTENUM"]);
                    refreshData.TotalQuantity = Convert.ToInt32(infoTable.Rows[0]["QUANTITY"]) + Convert.ToInt32(infoTable.Rows[0]["QUANTITY1"]);

                    infoTable = orderDao.FindOrderInfo(sortNo);
                    refreshData.CompleteCustomer = Convert.ToInt32(infoTable.Rows[0]["CUSTOMERNUM"]);
                    refreshData.CompleteRoute = Convert.ToInt32(infoTable.Rows[0]["ROUTENUM"]);
                    refreshData.CompleteQuantity = Convert.ToInt32(infoTable.Rows[0]["QUANTITY"]) + Convert.ToInt32(infoTable.Rows[0]["QUANTITY1"]);
                    refreshData.Average = orderDao.FindDispatchingAverage();

                    WriteToProcess("SortingStatus", "RefreshData", refreshData);
                    //messageUtil.SendToSortLed(sortNo, refreshData);
                    //上报物流系统
                    //判断当前流水号是否都已完成
                    //if (orderDao.SortNoFinished(sortNo) > 0)
                    //{
                        DataSet ds = orderDao.FindOrderProcess(batchNo, sortNo);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            try
                            {
                                string url = Context.Attributes["LogisticsUrl"].ToString();
                                DRProxy.LmsSortDataServiceService client = new DRProxy.LmsSortDataServiceService(url);
                                string xml = XmlDatasetConvert.ConvertDataTableToXml(ds.Tables[0]);
                                string returnXml = client.transSortProcess(xml, "0");
                                if (returnXml.Substring(0, 1) == "Y")
                                {
                                    Logger.Info(string.Format("上传物流订单进度成功,流水号:{0}", sortNo));
                                }
                                else
                                {
                                    Logger.Info(string.Format("上传物流订单进度失败,流水号:{0},返回信息{1}", sortNo, returnXml));
                                }
                            }
                            catch (Exception e)
                            {
                                Logger.Error(string.Format("订单流水号[{0}]完成上报物流系统时出错，原因:{1}",sortNo, e.Message));
                            }
                            
                        }
                    //}
                }
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("更新分拣信息处理失败！原因：{0}！ {1}", e.Message, "CurrentOrderProcess.cs 行号：97！"));
            }
        }
    }
}
