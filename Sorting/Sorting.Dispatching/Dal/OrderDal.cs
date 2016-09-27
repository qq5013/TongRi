using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Sorting.Dispatching.Dao;
using DB.Util;

namespace Sorting.Dispatching.Dal
{
    public class OrderDal
    {
        public DataTable GetRouteAll(int pageIndex, int pageSize, string filter)
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                table = orderDao.FindRouteAll((pageIndex - 1) * pageSize, pageSize, filter);
            }
            return table;
        }

        public int GetRouteCount(string filter)
        {
            int count = 0;
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                count = orderDao.FindRouteCount(filter);
            }
            return count;
        }

        public int GetMasterCount(string filter)
        {
            int count = 0;
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                count = orderDao.FindMasterCount(filter);
            }
            return count;
        }

        public DataTable GetMasterAll(int pageIndex, int pageSize, string filter)
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                table = orderDao.FindMasterAll((pageIndex - 1) * pageSize, pageSize, filter);
            }
            return table;
        }

        public int GetDetailCount(string filter)
        {
            int count = 0;
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                count = orderDao.FindDetailCount(filter);
            }
            return count;
        }

        public DataTable GetDetailAll(int pageIndex, int pageSize, string filter)
        {
            DataTable table = null;
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                table = orderDao.FindDetailAll((pageIndex - 1) * pageSize, pageSize, filter);
            }
            return table;
        }

        public DataTable GetOrderRoute(string orderDate, string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindOrderRoute(orderDate, batchNo);
            }
        }

        public void DeleteNoUseOrder(string orderDate, string batchNo, string routes)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                orderDao.DeleteNoUseOrder(orderDate, batchNo, routes);
            }
        }
        public DataTable GetOrderMaster()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindMaster();
            }
        }
        public DataTable FindDetail(string sortNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindDetail(sortNo);
            }
        }
        public int FindUnsortCount()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindUnsortCount();
            }
        }
        /// <summary>
        /// 矫正订单，更改订单的状态
        /// </summary>
        /// <param name="sortNo">PLC发来要矫正的流水号</param>
        /// <param name="channelGroup">A线或B线</param>
        public void UpdateMissOrderStatus(string sortNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                orderDao.UpdateMissOrderStatus(sortNo);
            }
        }
        /// <summary>
        /// 矫正订单，更改订单的状态
        /// </summary>
        /// <param name="sortNo">PLC发来要矫正的流水号</param>
        /// <param name="channelGroup">A线或B线</param>
        public void UpdateMissOrderStatus(string sortNo, string channelType)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                orderDao.UpdateMissOrderStatus(sortNo, channelType);
            }
        }
        public DataTable GetOrderDetail(string OrderId)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.GetOrderDetail(OrderId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        public DataSet GetSortingOrder()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.GetSortingOrder();
            }
        }
        public DataTable GetSortingOrderDetail(string SortNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.GetSortingOrderDetail(SortNo);
            }
        }
        public DataTable GetSortingOrderDetail(string batchNo,string SortNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.GetSortingOrderDetail(batchNo,SortNo);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        public DataSet GetOrder(string SortNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.GetOrder(SortNo);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        public DataSet GetOrder(string batchNo,string SortNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.GetOrder(batchNo,SortNo);
            }
        }
        public DataTable GetCigarettes()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindCigarettes();
            }
        }

        public DataTable GetOrderWithCigarette(string cigaretteCode)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindOrderWithCigarette(cigaretteCode);
            }
        }

        public void ClearPackage(string orderID)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                orderDao.ClearPackQuantity(orderID);
            }
        }

        public void SetPackage(string orderID)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                orderDao.UpdatePackQuantity(orderID);
            }
        }

        public DataTable GetPackMaster()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindPackMaster();
            }
        }

        public DataTable GetPackMaster(string [] filter)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindPackMaster(filter);
            }
        }

        public DataTable GetPackDetail()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindPackDetail();
            }
        }

        public void UpdateQuantity(string sortNo, string orderId, string channelName, string cigaretteCode,int quantity)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                pm.BeginTransaction();
                try
                {
                    OrderDao orderDao = new OrderDao();
                    orderDao.UpdateQuantity(sortNo, orderId, channelName, cigaretteCode, quantity);
                    pm.Commit();
                }
                catch (Exception)
                {
                    pm.Rollback();
                }
            }
        }
         
        /// <summary>
        ///  多沟带数据显示DAL方法       多沟带数据显示DAL
        /// </summary>
        /// <param name="channelGroup"></param>
        /// <param name="sortNoStart"></param>
        /// <returns></returns>
        public DataTable GetAllOrderDetailForCacheOrderQuery(int channelGroup, int sortNoStart)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                DataTable table = orderDao.FindStartNoForCacheOrderQuery(channelGroup, sortNoStart);
                if (table.Rows.Count != 0)
                {
                    int sortNo = Convert.ToInt32(table.Rows[0]["SORTNO"]);
                    return orderDao.FindDetailForCacheOrderQuery(channelGroup, sortNo);
                }
                return (new DataTable());
            }
        }

        public DataTable GetOrderDetailForCacheOrderQuery(int channelGroup, int sortNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                DataTable table = orderDao.FindOrderIDAndOrderNoForCacheOrderQuery(channelGroup, sortNo);

                if (table.Rows.Count != 0)
                {
                    string orderId = table.Rows[0]["ORDERID"].ToString();
                    int orderNo = Convert.ToInt32(table.Rows[0]["ORDERNO"]);
                    return orderDao.FindDetailForCacheOrderQuery(orderId, orderNo, channelGroup);
                }
                return (new DataTable());
            }
        }
        
        public DataTable GetOrderDetailForCacheOrderQuery(string packMode, int exportNo, int sortNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindDetailForCacheOrderQuery(exportNo, sortNo, packMode);
            }
        }

        public DataTable GetPackDataOrder(int exportNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindPackDataOrder(exportNo);
            }
        }
        /// <summary>
        /// 获取下载订单统计表
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable FindDownloadTotal()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindDownloadTotal();
            }
        }
        /// <summary>
        /// 获取下载订单的主表
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable FindDownloadMaster()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindDownloadMaster();
            }
        }
        /// <summary>
        /// 获取下载订单明细
        /// </summary>
        /// <param name="batchNo"></param>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public DataTable FindDownloadDetail(string OrderId)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindDownloadDetail(OrderId);
            }
        }
        /// <summary>
        /// 获取下载订单的主表
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable FindDownloadMaster(string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindDownloadMaster(batchNo);
            }
        }
        /// <summary>
        /// 获取下载订单明细
        /// </summary>
        /// <param name="batchNo"></param>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public DataTable FindDownloadDetail(string batchNo, string OrderId)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindDownloadDetail(batchNo, OrderId);
            }
        }
        /// <summary>
        /// 获取手工分拣订单的主表
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable FindHandMaster()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindHandMaster();
            }
        }
        /// <summary>
        /// 获取手工分拣订单的主表
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable FindHandMaster(string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindHandMaster(batchNo);
            }
        }
        /// <summary>
        /// 获取手工分拣订单明细
        /// </summary>
        /// <param name="batchNo"></param>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public DataTable FindHandDetail(string batchNo, string OrderId)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindHandDetail(batchNo, OrderId);
            }
        }
        /// <summary>
        /// 获取下载订单的主表
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable FindAbnormityOrder()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindAbnormityOrder();
            }
        }
        /// <summary>
        /// 获取下载订单的主表
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable FindAbnormityOrder(string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindAbnormityOrder(batchNo);
            }
        }
        /// <summary>
        /// 获取下载订单的主表
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable FindAbnormityTotal()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindAbnormityTotal();
            }
        }
        /// <summary>
        /// 获取下载订单的主表
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable FindAbnormityTotal(string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindAbnormityTotal(batchNo);
            }
        }
        public DataSet FindOrderStatus()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindOrderStatus();
            }
        }
        public DataSet FindOrderStatus(string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.FindOrderStatus(batchNo);
            }
        }
        /// <summary>
        /// 获取订单剩余量已经补货剩余量
        /// </summary>
        /// <param name="sortNo"></param>
        /// <param name="bNo"></param>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable GetChannelBalance(string sortNo, int bNo, string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                return orderDao.GetChannelBalance(sortNo, bNo, batchNo);
            }
        }
        /// <summary>
        /// 更新分拣订单结束
        /// </summary>
        /// <param name="sortNo"></param>
        /// <param name="bNo"></param>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public void UpdateOrderState()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                orderDao.UpdateOrderState();
            }
        }
    }
}
