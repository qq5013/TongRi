using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Sorting.Dispatching.Schedule;
using Sorting.Dispatching.Dao;
using DB.Util;
using MCP;
using Sorting.Optimize;
using System.Threading;

namespace Sorting.Dispatching.Schedule
{
    public class AutoSchedule
    {
        int batchCount = 0;
        public event ScheduleEventHandler OnSchedule = null;

        /// <summary>
        /// 清除指定批次数据
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void ClearSchedule(string orderDate, string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                //CMD_BATCH
                BatchDao batchDao = new BatchDao();
                batchDao.UpdateExecuter("0", "0", batchNo);
                batchDao.UpdateIsValid(batchNo, "0");

                //SC_CHANNELUSED
                ChannelScheduleDao csDao = new ChannelScheduleDao();
                //csDao.DeleteSchedule(batchNo);
                csDao.DeleteSchedule();

                //SC_LINE
                LineScheduleDao lsDao = new LineScheduleDao();
                lsDao.DeleteSchedule(batchNo);

                //SC_PALLETMASTER,SC_ORDER
                OrderScheduleDao osDao = new OrderScheduleDao();
                osDao.DeleteSchedule(batchNo);

                //AS_ORDER_DETAIL，AS_ORDER_MASTER
                OrderDao orderDao = new OrderDao();
                orderDao.DeleteOrder(batchNo);

                //SC_STOCKMIXCHANNEL
                StockChannelDao scDao = new StockChannelDao();
                scDao.DeleteSchedule(batchNo);

                //SC_SUPPLY
                //SupplyDao supplyDao = new SupplyDao();
                //supplyDao.DeleteSchedule(batchNo);

                //SC_HANDLESUPPLY
                //HandleSupplyDao handleSupplyDao = new HandleSupplyDao();
                //handleSupplyDao.DeleteHandleSupply(batchNo);
            }
        }

        /// <summary>
        /// 清除历史数据，并下载数据。
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void DownloadData(string orderDate, string batchNo, string dataBase)
        {
            try
            {
                DateTime dtOrder = DateTime.Parse(orderDate);
                string historyDate = dtOrder.AddDays(-7).ToShortDateString();
                using (PersistentManager pm = new PersistentManager())
                {
                    BatchDao batchDao = new BatchDao();
                    using (PersistentManager ssPM = new PersistentManager(dataBase))
                    {
                        SalesSystemDao ssDao = new SalesSystemDao();
                        ssDao.SetPersistentManager(ssPM);
                        try
                        {
                            pm.BeginTransaction();

                            //CMD_BATCH
                            //batchDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "数据清除与下载", 1, 14));

                            //SC_CHANNELUSED
                            ChannelScheduleDao csDao = new ChannelScheduleDao();
                            //csDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "数据清除与下载", 2, 14));

                            //SC_LINE
                            LineScheduleDao lsDao = new LineScheduleDao();
                            //lsDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "数据清除与下载", 3, 14));

                            //SC_PALLETMASTER ,SC_ORDER
                            OrderScheduleDao osDao = new OrderScheduleDao();
                            //osDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "数据清除与下载", 4, 14));

                            //SC_I_ORDERMASTER,SC_I_ORDERDETAIL,
                            OrderDao orderDao = new OrderDao();
                            //orderDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "数据清除与下载", 5, 14));

                            //SC_STOCKMIXCHANNEL
                            StockChannelDao scDao = new StockChannelDao();
                            //scDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "数据清除与下载", 6, 14));

                            //SC_SUPPLY
                            SupplyDao supplyDao = new SupplyDao();
                            //supplyDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "数据清除与下载", 7, 14));

                            //SC_HANDLESUPPLY
                            HandleSupplyDao handleSupplyDao = new HandleSupplyDao();
                            //handleSupplyDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "数据清除与下载", 8, 14));

                            //ClearSchedule(orderDate, batchNo);

                            //////////////////////////////////////////////////////////////////////////

                            //下载区域表
                            AreaDao areaDao = new AreaDao();
                            DataTable areaTable = ssDao.FindArea();
                            areaDao.SynchronizeArea(areaTable);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "数据清除与下载", 9, 14));

                            //下载配送线路表
                            RouteDao routeDao = new RouteDao();
                            DataTable routeTable = ssDao.FindRoute();
                            routeDao.SynchronizeRoute(routeTable);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "数据清除与下载", 10, 14));

                            //下载客户表
                            CustomerDao customerDao = new CustomerDao();
                            DataTable customerTable = ssDao.FindCustomer(dtOrder);
                            customerDao.SynchronizeCustomer(customerTable);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "数据清除与下载", 11, 14));

                            //下载卷烟表 进行同步
                            CigaretteDao cigaretteDao = new CigaretteDao();
                            DataTable cigaretteTable = ssDao.FindCigarette(dtOrder);
                            cigaretteDao.SynchronizeCigarette(cigaretteTable);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "数据清除与下载", 12, 14));

                            //查询已优化过的线路，以进行排除。
                            string routes = lsDao.FindRoutes(orderDate);

                            //下载订单主表
                            DataTable masterTable = ssDao.FindOrderMaster(dtOrder, batchNo, routes);
                            orderDao.BatchInsertMaster(masterTable);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "数据清除与下载", 13, 14));

                            //下载订单明细
                            DataTable detailTable = ssDao.FindOrderDetail(dtOrder, batchNo, routes);
                            orderDao.BatchInsertDetail(detailTable);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "数据清除与下载", 14, 14));

                            pm.Commit();
                        }
                        catch (Exception e)
                        {
                            pm.Rollback();
                            throw e;
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                if (OnSchedule != null)
                    OnSchedule(this, new ScheduleEventArgs(OptimizeStatus.ERROR, ee.Message));
                throw ee;
            }
        }

        /// <summary>
        /// 开始优化
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenSchedule(string orderDate, string batchNo)
        {
            try
            {
                //数据清除
                ClearSchedule(orderDate, batchNo);
                //分拣线优化
                //订单按配送路线及顺序统计数量(不包括异形烟)，均匀分布到各分拣线上
                GenLineSchedule(orderDate, batchNo);

                //分拣线货仓优化
                //优化的数据放到表SC_CHANNELUSED
                GenChannelSchedule(orderDate, batchNo);

                //拆分订单
                GenSplitOrder(orderDate, batchNo);

                //订单优化
                //GenOrderSchedule(orderDate, batchNo);

                //订单优化1
                GenOrderSchedule2(orderDate, batchNo);

                //备货货仓优化
                GenStockChannelSchedule(orderDate, batchNo);

                //补货优化
                GenSupplySchedule(orderDate, batchNo);

                //手工补货优化
                GenHandleSupplySchedule(orderDate, batchNo);

                //更新为已优化
                using (PersistentManager pm = new PersistentManager())
                {
                    BatchDao batchDao = new BatchDao();
                    if (!batchDao.CheckOrder(orderDate, batchNo))
                    {
                        throw new Exception("优化过程出错，请检查！");
                    }
                    batchDao.SelectBalanceIntoHistory(orderDate, batchNo);
                    batchDao.UpdateIsValid(orderDate, batchNo, "1");
                    batchCount = batchDao.BatchCount(batchNo);
                }

                if (OnSchedule != null)
                    OnSchedule(this, new ScheduleEventArgs(OptimizeStatus.COMPLETE, Convert.ToString(batchCount)));

            }
            catch (Exception e)
            {
                ClearSchedule(orderDate, batchNo);
                if (OnSchedule != null)
                    OnSchedule(this, new ScheduleEventArgs(OptimizeStatus.ERROR, "[" + e.TargetSite + "]\n" + e.Message));
            }
        }



        #region  优化方法

        /// <summary>
        /// 生产线优化  2008-12-11修改 
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenLineSchedule(string orderDate, string batchNo)
        {
            using (DB.Util.PersistentManager pm = new DB.Util.PersistentManager())
            {
                LineInfoDao lineDao = new LineInfoDao();
                OrderDao detailDao = new OrderDao();
                LineScheduleDao lineScDao = new LineScheduleDao();
                Sorting.Optimize.LineOptimize lineSchedule = new Sorting.Optimize.LineOptimize();

                //找出订单日期，按配送路线及顺序统计订单数量(不包括异形烟)
                DataTable routeTable = detailDao.FindRouteQuantity(batchNo).Tables[0];
                //找出可用生产线
                DataTable lineTable = lineDao.GetAvailabeLine("2").Tables[0];

                //保存到表SC_LINE
                DataTable scLineTable = new DataTable();
                if (lineTable.Rows.Count > 0)
                {
                    scLineTable = lineSchedule.Optimize(routeTable, lineTable, orderDate, batchNo);
                    lineScDao.SaveLineSchedule(scLineTable);
                }
                else
                    throw new Exception("没有可用的分拣线！");


                routeTable = detailDao.FindRouteQuantity(orderDate, batchNo).Tables[0];
                lineTable = lineDao.GetAvailabeLine("3").Tables[0];
                if (lineTable.Rows.Count > 0)
                {
                    scLineTable = lineSchedule.Optimize(routeTable, lineTable, orderDate, batchNo);
                    lineScDao.SaveLineSchedule(scLineTable);
                }

                if (OnSchedule != null)
                    OnSchedule(this, new ScheduleEventArgs(2, "生产线优化", 1, 1));
            }
        }

        /// <summary>
        /// 生产线货仓优化
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenChannelSchedule(string orderDate, string batchNo)
        {
            using (DB.Util.PersistentManager pm = new DB.Util.PersistentManager())
            {
                //货仓
                ChannelDao channelDao = new ChannelDao();
                //订单
                OrderDao detailDao = new OrderDao();
                //生产线设备
                LineDeviceDao deviceDao = new LineDeviceDao();
                ChannelOptimize channelSchedule = new ChannelOptimize();

                Sorting.Dispatching.Dao.SysParameterDao parameterDao = new SysParameterDao();
                //参数字典
                Dictionary<string, string> parameter = parameterDao.FindParameters();

                //生产线表
                LineScheduleDao lineDao = new LineScheduleDao();
                DataTable lineTable = lineDao.FindAllLine(batchNo).Tables[0];

                LineInfoDao lineDao1 = new LineInfoDao();
                DataTable lineTable1 = lineDao1.GetAvailabeLine("3").Tables[0];

                //是否使用整件(不拆箱)分拣线
                bool isUseWholePiecesSortLine = lineTable1.Rows.Count > 0;

                int currentCount = 0;
                int totalCount = lineTable.Rows.Count;

                foreach (DataRow lineRow in lineTable.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    //DataTable dtNoChannel = detailDao.FindNoCigaretteChannel(batchNo);
                    //for (int i = 0; i < dtNoChannel.Rows.Count; i++)
                    //{
                    //    Logger.Info((i + 1).ToString() + ". " + dtNoChannel.Rows[i]["CIGARETTENAME"].ToString() + "还没有固定烟仓!");
                    //}
                    //if (dtNoChannel.Rows.Count > 0)
                    //    throw new Exception("有品牌还没有固定烟仓，请检查！"); ;

                    //查询分拣线货仓表
                    DataTable channelTable = channelDao.FindAvailableChannel(lineCode).Tables[0];
                    //查询分拣线设备参数表            
                    DataTable deviceTable = deviceDao.FindLineDevice(lineCode).Tables[0];

                    //通道机两条分拣线品牌必须一致，立式机两条分拣线则不需要一致
                    //取所有订单品牌及总数量
                    DataTable orderTable = detailDao.FindAllCigaretteQuantity(batchNo, isUseWholePiecesSortLine).Tables[0];

                    //检查订单中是否有未固定的货仓

                    channelSchedule.Optimize(orderTable, channelTable, deviceTable, parameter);

                    channelDao.SaveChannelSchedule(channelTable, orderDate, batchNo);
                    
                    //更新卷烟名称为简称
                    channelDao.UpdateProductName(batchNo);

                    if (OnSchedule != null)
                        OnSchedule(this, new ScheduleEventArgs(3, "正在优化" + lineRow["LINECODE"].ToString() + "分拣线货仓", ++currentCount, totalCount));
                }

                currentCount = 0;
                totalCount = lineTable1.Rows.Count;

                foreach (DataRow lineRow in lineTable1.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    //查询分拣线货仓表
                    DataTable channelTable = channelDao.FindAvailableChannel(lineCode).Tables[0];
                    channelDao.SaveChannelSchedule(channelTable, orderDate, batchNo);


                    if (OnSchedule != null)
                        OnSchedule(this, new ScheduleEventArgs(3, "正在优化" + lineRow["LINECODE"].ToString() + "分拣线货仓", ++currentCount, totalCount));
                }
            }
        }

        /// <summary>
        /// 订单拆分2010-07-05
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenSplitOrder(string orderDate, string batchNo)
        {
            using (DB.Util.PersistentManager pm = new DB.Util.PersistentManager())
            {

                OrderDao orderDao = new OrderDao();
                ChannelDao channelDao = new ChannelDao();
                OrderScheduleDao orderScheduleDao = new OrderScheduleDao();
                LineScheduleDao lineDao = new LineScheduleDao();

                OrderSplitOptimize orderSchedule = new OrderSplitOptimize();

                Sorting.Dispatching.Dao.SysParameterDao parameterDao = new SysParameterDao();
                Dictionary<string, string> parameter = parameterDao.FindParameters();

                //查询分拣线表
                DataTable lineTable = lineDao.FindAllLine(batchNo).Tables[0];

                //清理临时表
                //SC_TMP_PALLETMASTER
                //SC_TMP_ORDER"
                orderScheduleDao.ClearSplitOrder();

                LineInfoDao lineDao1 = new LineInfoDao();
                DataTable lineTable1 = lineDao1.GetAvailabeLine("3").Tables[0];
                bool isUseWholePiecesSortLine = lineTable1.Rows.Count > 0;

                foreach (DataRow lineRow in lineTable.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    //查询货仓信息表 SC_CHANNELUSED，Order by CHANNELORDER
                    DataTable channelTable = channelDao.FindChannelSchedule(batchNo, lineCode).Tables[0];

                    //查询订单主表 SC_I_ORDERMASTER ORDER BY ROW_NUMBER() over (ORDER BY AREA.SORTID,ROUTE.SORTID,ROUTECODE, SORTID) SORTNO
                    //DataTable masterTable = orderDao.FindOrderMaster(orderDate, batchNo, lineCode).Tables[0];
                    DataTable masterTable = orderDao.GetOrderMain(orderDate, batchNo, lineCode);

                    //获取一个品牌多通道订单5的倍数的最大量的表
                    DataTable dtQuantity5 = orderDao.GetOrderQuantity5(batchNo);

                    //查询订单明细表 SC_I_ORDERDETAIL ORDER BY ORDERID,CHANNELCODE
                    DataTable orderTable = orderDao.FindOrderDetail(orderDate, batchNo, lineCode).Tables[0];

                    DataTable orderDetail = null;
                    HashTableHandle hashTableHandle = new HashTableHandle(orderTable);

                    int sortNo = 1;
                    int currentCount = 0;
                    int totalCount = masterTable.Rows.Count;
                    orderSchedule.moveToMixChannelProducts.Clear();

                    foreach (DataRow masterRow in masterTable.Rows)
                    {
                        sortNo = int.Parse(masterRow["SortNo"].ToString());
                        DataRow[] orderRows = null;

                        orderDetail = hashTableHandle.Select("ORDERID", masterRow["ORDERID"]);
                        //orderRows = orderDetail.Select(string.Format("ORDERID = '{0}'", masterRow["ORDERID"]), "Quantity");
                        orderRows = orderTable.Select(string.Format("ORDERID = '{0}'", masterRow["ORDERID"]), "Quantity");

                        DataSet ds = orderSchedule.Optimize1(masterRow, orderRows, channelTable, dtQuantity5,lineCode, sortNo, parameter);
                        orderScheduleDao.SaveSplitOrder(ds);

                        //if (OnSchedule != null)
                        //    OnSchedule(this, new ScheduleEventArgs(4, "正在优化" + lineRow["LINECODE"].ToString() + "分拣线订单", ++currentCount, totalCount));
                    }
                    orderDao.UpdateQuantity(orderDate, batchNo);
                    channelDao.UpdateQuantity(channelTable, Convert.ToBoolean(parameter["IsUseBalance"]));


                }
            }
        }
        /// <summary>
        /// 计算订单之间时间
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenOrderSpace(string orderDate, string batchNo)
        {
            using (DB.Util.PersistentManager pm = new DB.Util.PersistentManager())
            {

                OrderDao orderDao = new OrderDao();
                ChannelDao channelDao = new ChannelDao();
                OrderScheduleDao orderScheduleDao = new OrderScheduleDao();
                LineScheduleDao lineDao = new LineScheduleDao();

                OrderSplitOptimize orderSchedule = new OrderSplitOptimize();

                Sorting.Dispatching.Dao.SysParameterDao parameterDao = new SysParameterDao();
                Dictionary<string, string> parameter = parameterDao.FindParameters();
                Dictionary<string, int> dicBalance = new Dictionary<string, int>();
                Dictionary<string, int> dicSupplyCount = new Dictionary<string, int>();
                Dictionary<string, int> dicFirstAddress = new Dictionary<string, int>();
                DataTable dt = orderDao.GetOrderDetail();
                int quantityTotal = 0;
                int preSortNo = 0;
                int preChannelAddress = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int sortNo = int.Parse(dt.Rows[i]["SORTNO"].ToString());

                    int channelAddress = int.Parse(dt.Rows[i]["CHANNELNAME"].ToString());
                    string channelCode = dt.Rows[i]["CHANNELCODE"].ToString();
                    int balance = int.Parse(dt.Rows[i]["BALANCE"].ToString());
                    int quantity = int.Parse(dt.Rows[i]["QUANTITY"].ToString());
                    quantityTotal += quantity;
                    if (balance <= 0)
                        balance = 25;

                    if (!dicBalance.ContainsKey(channelCode))
                        dicBalance.Add(channelCode, balance);

                    if (dicBalance[channelCode] - quantity < 0)
                    {
                        int supplyCount = (quantity - dicBalance[channelCode]) / 25 + ((quantity - dicBalance[channelCode]) % 25) > 0 ? 1 : 0;
                        //补货次数
                        if (!dicSupplyCount.ContainsKey(channelCode))
                            dicSupplyCount.Add(channelCode, supplyCount);
                        else
                            dicSupplyCount[channelCode] = supplyCount;

                        dicBalance[channelCode] = supplyCount * 25 + dicBalance[channelCode] - quantity;
                    }
                    else
                    {
                        dicBalance[channelCode] = dicBalance[channelCode] - quantity;
                    }

                    if (sortNo != preSortNo)
                    {
                        if (sortNo > 1)
                        {
                            //首个出道口地址差
                            int address = (channelAddress - preChannelAddress);
                            //补货次数,计算此订单所有通道的补货次数
                            //dicSupplyCount[channelCode];
                            //此单烟的皮带长度
                            //quantityTotal
                        }
                        preSortNo = sortNo;
                        preChannelAddress = int.Parse(dt.Rows[i]["CHANNELNAME"].ToString());
                        dicSupplyCount[channelCode] = 0;
                        quantityTotal = 0;
                    }
                }
            }
        }
        /// <summary>
        /// 订单优化2010-07-07
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenOrderSchedule(string orderDate, string batchNo)
        {
            using (DB.Util.PersistentManager pm = new DB.Util.PersistentManager())
            {

                OrderDao orderDao = new OrderDao();
                ChannelDao channelDao = new ChannelDao();
                OrderScheduleDao orderScheduleDao = new OrderScheduleDao();
                SupplyDao supplyDao = new SupplyDao();
                OrderOptimize orderSchedule = new OrderOptimize();

                SysParameterDao parameterDao = new SysParameterDao();
                Dictionary<string, string> parameter = parameterDao.FindParameters();

                LineScheduleDao lineDao = new LineScheduleDao();
                DataTable lineTable = lineDao.FindAllLine(orderDate, batchNo).Tables[0];

                foreach (DataRow lineRow in lineTable.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    //查询订单主表
                    DataTable masterTable = orderDao.FindTmpMaster(orderDate, batchNo, lineCode);
                    //查询订单明细表
                    DataTable orderTable = orderDao.FindTmpDetail(orderDate, batchNo, lineCode);
                    //查询分拣货仓表
                    DataTable channelTable = channelDao.FindChannelSchedule(orderDate, batchNo, lineCode, Convert.ToInt32(parameter["RemainCount"])).Tables[0];

                    int sortNo = 1;
                    int currentCount = 0;
                    int totalCount = masterTable.Rows.Count;

                    foreach (DataRow masterRow in masterTable.Rows)
                    {
                        DataRow[] orderRows = null;

                        //查询当前订单明细
                        orderRows = orderTable.Select(string.Format("SORTNO = '{0}'", masterRow["SORTNO"]), "CHANNELGROUP, CHANNELCODE");
                        DataSet ds = orderSchedule.Optimize(masterRow, orderRows, channelTable, ref sortNo);

                        //orderScheduleDao.SaveOrder(ds);
                        supplyDao.InsertSupply(ds.Tables["SUPPLY"], lineCode, orderDate, batchNo);

                        if (OnSchedule != null)
                            OnSchedule(this, new ScheduleEventArgs(5, "正在优化" + lineRow["LINECODE"].ToString() + "分拣线订单", ++currentCount, totalCount));
                    }

                    channelDao.Update(channelTable);
                }

                LineInfoDao lineDao1 = new LineInfoDao();
                DataTable lineTable1 = lineDao1.GetAvailabeLine("3").Tables[0];

                foreach (DataRow lineRow in lineTable1.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    //查询货仓信息表
                    DataTable channelTable = channelDao.FindChannelSchedule(orderDate, batchNo, lineCode, Convert.ToInt32(parameter["RemainCount"])).Tables[0];
                    //查询订单主表
                    DataTable masterTable = orderDao.FindOrderMaster(orderDate, batchNo, lineCode).Tables[0];
                    //查询订单明细表
                    DataTable orderTable = orderDao.FindOrderDetail(orderDate, batchNo, lineCode).Tables[0];

                    int sortNo = 1;
                    int currentCount = 0;
                    int totalCount = masterTable.Rows.Count;

                    foreach (DataRow masterRow in masterTable.Rows)
                    {
                        DataRow[] orderRows = null;
                        //查询当前订单明细
                        orderRows = orderTable.Select(string.Format("ORDERID = '{0}'", masterRow["ORDERID"]), "CIGARETTECODE");
                        DataSet ds = orderSchedule.OptimizeUseWholePiecesSortLine(masterRow, orderRows, channelTable, ref sortNo);
                        orderScheduleDao.SaveOrder(ds);

                        if (OnSchedule != null)
                            OnSchedule(this, new ScheduleEventArgs(5, "正在优化" + lineRow["LINECODE"].ToString() + "分拣线订单", ++currentCount, totalCount));
                    }

                    channelDao.UpdateQuantity(channelTable, false);
                }
            }
        }
        /// <summary>
        /// 订单优化2010-07-07
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenOrderSchedule2(string orderDate, string batchNo)
        {
            using (DB.Util.PersistentManager pm = new DB.Util.PersistentManager())
            {

                OrderDao orderDao = new OrderDao();
                ChannelDao channelDao = new ChannelDao();
                OrderScheduleDao orderScheduleDao = new OrderScheduleDao();
                SupplyDao supplyDao = new SupplyDao();
                OrderOptimize orderSchedule = new OrderOptimize();

                SysParameterDao parameterDao = new SysParameterDao();
                Dictionary<string, string> parameter = parameterDao.FindParameters();

                string lineCode = "01";

                //查询订单主表
                DataTable masterTable = orderDao.FindTmpMaster(orderDate, batchNo, lineCode);
                //查询订单明细表
                DataTable orderTable = orderDao.FindTmpDetail(orderDate, batchNo, lineCode);
                //查询分拣货仓表
                DataTable channelTable = channelDao.FindChannelSchedule(orderDate, batchNo, lineCode).Tables[0];

                int sortNo = 1;
                int currentCount = 0;
                int totalCount = masterTable.Rows.Count;

                foreach (DataRow masterRow in masterTable.Rows)
                {
                    DataRow[] orderRows = null;

                    //查询当前订单明细
                    orderRows = orderTable.Select(string.Format("SORTNO = '{0}'", masterRow["SORTNO"]), "CHANNELGROUP, CHANNELCODE");
                    DataSet ds = orderSchedule.Optimize(masterRow, orderRows, channelTable, ref sortNo);

                    //orderScheduleDao.SaveOrder(ds);
                    //supplyDao.InsertSupply(ds.Tables["SUPPLY"], orderDate, batchNo);

                    if (OnSchedule != null)
                        OnSchedule(this, new ScheduleEventArgs(5, "正在优化分拣线订单", ++currentCount, totalCount));
                }

                channelDao.Update(channelTable);

            }
        }
        /// <summary>
        /// 备货货仓优化2010-07-08
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenStockChannelSchedule(string orderDate, string batchNo)
        {
            using (DB.Util.PersistentManager pm = new DB.Util.PersistentManager())
            {
                StockChannelDao schannelDao = new StockChannelDao();
                OrderDao orderDao = new OrderDao();
                OrderDao detailDao = new OrderDao();

                SysParameterDao parameterDao = new SysParameterDao();
                Dictionary<string, string> parameter = parameterDao.FindParameters();

                //每天分拣结束后备货货仓是否为空
                if (parameter["ClearStockChannel"] == "1")
                    schannelDao.ClearCigarette();

                //查询补货货仓表 CMD_STOCKCHANNEL ORDER BY ORDERNO
                DataTable channelTable = schannelDao.FindChannel();
                //查询通道机卷烟数量信息表
                //DataTable orderCTable = orderDao.FindCigaretteQuantityFromChannelUsed(orderDate, batchNo, "3");
                DataTable orderCTable = orderDao.FindCigaretteQuantityFromChannelUsed(orderDate, batchNo);
                //查询立式机卷烟数量信息表（应加上混合货仓问题）
                DataTable orderTTable = orderDao.FindCigaretteQuantityFromChannelUsed(orderDate, batchNo, "2");
                //取所有订单品牌及总数量
                DataTable orderTable = detailDao.FindAllCigaretteQuantity(orderDate, batchNo, false).Tables[0];

                StockOptimize stockOptimize = new StockOptimize();

                //bool isUseSynchronizeOptimize = Convert.ToBoolean(parameter["IsUseSynchronizeOptimize"]);
                //DataTable mixTable = stockOptimize.Optimize(isUseSynchronizeOptimize, channelTable, isUseSynchronizeOptimize ? orderCTable : orderTable, isUseSynchronizeOptimize ? orderTTable : orderTable, orderDate, batchNo);

                stockOptimize.Optimize(channelTable, orderCTable);

                schannelDao.UpdateChannel(channelTable);
                schannelDao.InsertStockChannelUsed(orderDate, batchNo, channelTable);
                //schannelDao.InsertMixChannel(mixTable);

                if (OnSchedule != null)
                    OnSchedule(this, new ScheduleEventArgs(6, "备货货仓优化", 1, 1));
            }
        }

        /// <summary>
        /// 补货优化2010-04-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenSupplySchedule(string orderDate, string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                ChannelDao channelDao = new ChannelDao();
                LineScheduleDao lineDao = new LineScheduleDao();
                SupplyDao supplyDao = new SupplyDao();
                SupplyOptimize supplyOptimize = new SupplyOptimize();

                SysParameterDao parameterDao = new SysParameterDao();
                Dictionary<string, string> parameter = parameterDao.FindParameters();

                DataTable lineTable = lineDao.FindAllLine(batchNo).Tables[0];

                int currentCount = 0;
                int totalCount = lineTable.Rows.Count;

                foreach (DataRow lineRow in lineTable.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    int channelGroup = 1;
                    int channelType = 2;
                    int aheadCount = Convert.ToInt32(parameter[string.Format("SupplyAheadCount-{0}-{1}-{2}", lineCode, channelGroup, channelType)]);
                    supplyDao.AdjustSortNo(orderDate, batchNo, lineCode, channelGroup, channelType, aheadCount);

                    channelGroup = 1;
                    channelType = 3;
                    aheadCount = Convert.ToInt32(parameter[string.Format("SupplyAheadCount-{0}-{1}-{2}", lineCode, channelGroup, channelType)]);
                    supplyDao.AdjustSortNo(orderDate, batchNo, lineCode, channelGroup, channelType, aheadCount);

                    //channelGroup = 2;
                    //channelType = 2;
                    //aheadCount = Convert.ToInt32(parameter[string.Format("SupplyAheadCount-{0}-{1}-{2}", lineCode, channelGroup, channelType)]);       
                    //supplyDao.AdjustSortNo(orderDate, batchNo, lineCode, channelGroup, channelType, aheadCount);

                    //channelGroup = 2;
                    //channelType = 3;
                    //aheadCount = Convert.ToInt32(parameter[string.Format("SupplyAheadCount-{0}-{1}-{2}", lineCode, channelGroup, channelType)]);      
                    //supplyDao.AdjustSortNo(orderDate, batchNo, lineCode, channelGroup, channelType, aheadCount);

                    DataTable channelTable = channelDao.FindChannelSchedule(orderDate, batchNo, lineCode, Convert.ToInt32(parameter["RemainCount"])).Tables[0];
                    DataTable supplyTable = supplyOptimize.Optimize(channelTable);
                    supplyDao.InsertSupply(supplyTable, Convert.ToBoolean(parameter["IsSupplyOrderbyCigaretteCode"]));

                    if (OnSchedule != null)
                        OnSchedule(this, new ScheduleEventArgs(7, "正在优化" + lineRow["LINECODE"].ToString() + "补货计划", currentCount++, totalCount));
                }

                LineInfoDao lineDao1 = new LineInfoDao();
                DataTable lineTable1 = lineDao1.GetAvailabeLine("3").Tables[0];

                foreach (DataRow lineRow in lineTable1.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    //查询货仓信息表
                    DataTable channelTable = channelDao.FindChannelSchedule(orderDate, batchNo, lineCode, Convert.ToInt32(parameter["RemainCount"])).Tables[0];
                    //查询订单主表
                    DataTable masterTable = orderDao.FindOrderMaster(orderDate, batchNo, lineCode).Tables[0];
                    //查询订单明细表
                    DataTable orderTable = orderDao.FindOrderDetail(orderDate, batchNo, lineCode).Tables[0];

                    int serialNo = 1;
                    currentCount = 0;
                    totalCount = masterTable.Rows.Count;

                    foreach (DataRow masterRow in masterTable.Rows)
                    {
                        DataRow[] orderRows = null;
                        //查询当前订单明细
                        orderRows = orderTable.Select(string.Format("ORDERID = '{0}'", masterRow["ORDERID"]), "CIGARETTECODE");
                        DataTable supplyTable = supplyOptimize.Optimize(channelTable, orderRows, ref serialNo);
                        supplyDao.InsertSupply(supplyTable, true);

                        if (OnSchedule != null)
                            OnSchedule(this, new ScheduleEventArgs(7, "正在优化" + lineRow["LINECODE"].ToString() + "分拣线订单补货！", ++currentCount, totalCount));
                    }
                }
            }
        }

        /// <summary>
        /// 手工补货优化
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenHandleSupplySchedule(string orderDate, string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {

                HandleSupplyOptimize handleSupplyOptimize = new Sorting.Optimize.HandleSupplyOptimize();
                ScOrderDao scOrderDao = new ScOrderDao();
                ChannelDao channelDao = new ChannelDao();

                Dao.LineScheduleDao lineDao = new LineScheduleDao();
                DataTable lineTable = lineDao.FindAllLine(batchNo).Tables[0];

                int currentCount = 0;
                int totalCount = lineTable.Rows.Count;

                foreach (DataRow lineRow in lineTable.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    DataTable handSupplyOrders = scOrderDao.FindHandleSupplyOrder(orderDate, batchNo, lineCode);
                    DataTable multiBrandChannel = channelDao.FindMultiBrandChannel(lineCode);

                    AddColumnForChannelTable(multiBrandChannel, multiBrandChannel.Rows.Count);

                    //返回新的手工补货订单表
                    DataTable newSupplyOrders = handleSupplyOptimize.Optimize(handSupplyOrders, multiBrandChannel);

                    //保存货仓空仓作业的SortNo
                    channelDao.Update(multiBrandChannel, orderDate, batchNo);

                    //删除sc_order原来的手工补货定单
                    scOrderDao.DeleteOldSupplyOrders(orderDate, batchNo, lineCode);
                    //在sc_order中插入新的手工补货定单
                    scOrderDao.InsertNewSupplyOrders(newSupplyOrders);


                    //在SC_HANDLESUPPLY中插入新的手工补货定单
                    scOrderDao.InsertHandSupplyOrders(newSupplyOrders);

                    if (OnSchedule != null)
                        OnSchedule(this, new ScheduleEventArgs(8, "正在优化" + lineRow["LINECODE"].ToString() + "分拣线手工补货定单订单", ++currentCount, totalCount));
                }
            }
        }

        private DataTable AddColumnForChannelTable(DataTable channel, int channelCount)
        {
            for (int i = 0; i < channelCount; i++)
            {
                channel.Rows[i]["QUANTITY"] = 0;
            }

            return channel;
        }
        /// <summary>
        /// 补货优化2010-04-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenSupplySchedule1(string orderDate, string batchNo)
        {
            int batch = int.Parse(batchNo.Substring(6,3));
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                OrderDao orderDao = new OrderDao();
                ChannelDao channelDao = new ChannelDao();
                LineScheduleDao lineDao = new LineScheduleDao();
                SupplyDao supplyDao = new SupplyDao();
                SupplyOptimize supplyOptimize = new SupplyOptimize();

                //清理数据
                supplyDao.ClearSupply();

                SysParameterDao parameterDao = new SysParameterDao();
                Dictionary<string, string> parameter = parameterDao.FindParameters();

                DataTable lineTable = lineDao.FindAllLine(orderDate, batchNo).Tables[0];

                int currentCount = 0;
                int totalCount = lineTable.Rows.Count;

                foreach (DataRow lineRow in lineTable.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    //DataTable channelTable = channelDao.FindChannelSchedule(orderDate, batchNo, lineCode).Tables[0];

                    //处理批次零条
                    supplyDao.InsertBalanceSupply(lineCode, orderDate, batchNo);

                    int bno = int.Parse(batchNo.Substring(10, 3));
                    string firstBatchNo = batchNo.Substring(0, 10) + "001";
                    //获取上一批次的批次号
                    string preBatchNo = batchDao.GetPreBatchNo(batchNo);

                    channelDao.InsertChannelBalance(bno, firstBatchNo, batchNo, preBatchNo);
                    //第一批仓库余量更新
                    //如果第二批有新增品牌，要更新QUANTITY,QUANTITY1都为仓库零条余量

                    //更新零条数量
                    channelDao.UpdateChannelBalance(bno, firstBatchNo);


                    //通道机尾数应该补最前面
                    //计算余量是否够零条，如不够需补整件烟，再计算余量
                    DataTable channelTable = channelDao.FindChannelBalance3(orderDate, firstBatchNo).Tables[0];
                    DataTable supplyTable = supplyOptimize.Optimize3(channelTable, batchNo, bno, preBatchNo);
                    supplyDao.InsertSupply(supplyTable, Convert.ToBoolean(parameter["IsSupplyOrderbyCigaretteCode"]));
                    channelDao.UpdateChannelBalance(channelTable, bno);

                    //获取通道机的数据
                    channelTable = channelDao.FindChannel3(orderDate, batchNo, firstBatchNo, bno - 1).Tables[0];
                    DataTable orderDetail = orderDao.GetChannelOrderDetail(orderDate, batchNo, lineCode);
                    DataRow[] orderRows = orderDetail.Select("CHANNELTYPE=3", "SORTNO,CHANNELCODE");
                    //DataTable supplyTable = supplyOptimize.Optimize(channelTable, orderRows);
                    //supplyTable = supplyOptimize.Optimize1(channelTable, orderRows, false);
                    //2015-04-07修改补货优化
                    //如果参数OrderCount<=1,那么还用之前的补货优化，否则以订单OrderCount数量作为一批的补货优化
                    int OrderCount = int.Parse(parameter["OrderCount"]);
                    //DataTable supplyTable;
                    if(OrderCount<=1)
                        supplyTable = supplyOptimize.Optimize2(channelTable, orderRows);
                    else
                        supplyTable = supplyOptimize.Optimize6(channelTable, orderRows, OrderCount);

                    supplyDao.InsertSupply(supplyTable, Convert.ToBoolean(parameter["IsSupplyOrderbyCigaretteCode"]));

                    //通道机按照通道顺序补
                    //DataTable channelTable = channelDao.FindChannel31(orderDate, batchNo).Tables[0];
                    //DataTable supplyTable = supplyOptimize.Optimize5(channelTable);
                    //supplyDao.InsertSupply(supplyTable, Convert.ToBoolean(parameter["IsSupplyOrderbyCigaretteCode"]));

                    
                    //计算余量是否够零条，如不够需补整件烟，再计算余量
                    //channelTable = channelDao.FindChannelBalance3(orderDate, firstBatchNo).Tables[0];
                    //supplyTable = supplyOptimize.Optimize3(channelTable, batchNo, bno, preBatchNo);
                    //supplyDao.InsertSupply(supplyTable, Convert.ToBoolean(parameter["IsSupplyOrderbyCigaretteCode"]));
                    //channelDao.UpdateChannelBalance(channelTable, bno);

                    //立式机补货优化
                    channelTable = channelDao.FindChannel2(orderDate, batchNo, firstBatchNo, bno - 1).Tables[0];
                    //立式机的订单按品规进行统计
                    orderDetail = orderDao.GetChannelOrderDetail2(orderDate, batchNo, lineCode);
                    //orderRows = orderDetail.Select("CHANNELTYPE=2", "SORTNO,CHANNELADDRESS");
                    orderRows = orderDetail.Select("", "SORTNO,CHANNELCODE");
                    //supplyTable2 = supplyOptimize.Optimize1(channelTable, orderRows, false);
                    DataTable supplyTable2 = supplyOptimize.Optimize4(channelTable, orderRows);
                    supplyDao.InsertSupply2(supplyTable2, Convert.ToBoolean(parameter["IsSupplyOrderbyCigaretteCode"]));

                    //20151111修改，根据阿宽的描述，立式机补货件数不对，只计算整件即可


                    ////20150323修改
                    //channelTable = channelDao.FindChannel21(orderDate, batchNo).Tables[0];

                    //DataTable supplyTable2 = supplyOptimize.Optimize5(channelTable);
                    //supplyDao.InsertSupply2(supplyTable2, Convert.ToBoolean(parameter["IsSupplyOrderbyCigaretteCode"]));

                    //立式机
                    channelTable = channelDao.FindChannelBalance2(orderDate, firstBatchNo).Tables[0];
                    supplyTable2 = supplyOptimize.Optimize3(channelTable, batchNo, bno, preBatchNo);
                    supplyDao.InsertSupply2(supplyTable2, Convert.ToBoolean(parameter["IsSupplyOrderbyCigaretteCode"]));
                    channelDao.UpdateChannelBalance(channelTable, bno);

                    if (OnSchedule != null)
                        OnSchedule(this, new ScheduleEventArgs(7, "正在优化" + lineRow["LINECODE"].ToString() + "补货计划", currentCount++, totalCount));
                }

            }
        }

        /// <summary>
        /// 手工补货优化
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenHandleSupplySchedule1(string orderDate, string batchNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {

                HandleSupplyOptimize handleSupplyOptimize = new Sorting.Optimize.HandleSupplyOptimize();
                ScOrderDao scOrderDao = new ScOrderDao();
                ChannelDao channelDao = new ChannelDao();

                Dao.LineScheduleDao lineDao = new LineScheduleDao();
                DataTable lineTable = lineDao.FindAllLine(orderDate, batchNo).Tables[0];

                int currentCount = 0;
                int totalCount = lineTable.Rows.Count;

                foreach (DataRow lineRow in lineTable.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    DataTable handSupplyOrders = scOrderDao.FindHandleSupplyOrder(orderDate, batchNo, lineCode);
                    DataTable multiBrandChannel = channelDao.FindMultiBrandChannel(lineCode);

                    AddColumnForChannelTable(multiBrandChannel, multiBrandChannel.Rows.Count);

                    //返回新的手工补货订单表
                    DataTable newSupplyOrders = handleSupplyOptimize.Optimize(handSupplyOrders, multiBrandChannel);

                    //保存货仓空仓作业的SortNo
                    channelDao.Update(multiBrandChannel, orderDate, batchNo);

                    //删除sc_order原来的手工补货定单
                    scOrderDao.DeleteOldSupplyOrders(orderDate, batchNo, lineCode);
                    //在sc_order中插入新的手工补货定单
                    scOrderDao.InsertNewSupplyOrders(newSupplyOrders);


                    //在SC_HANDLESUPPLY中插入新的手工补货定单
                    scOrderDao.InsertHandSupplyOrders(newSupplyOrders);

                    if (OnSchedule != null)
                        OnSchedule(this, new ScheduleEventArgs(8, "正在优化" + lineRow["LINECODE"].ToString() + "分拣线手工补货定单订单", ++currentCount, totalCount));
                }
            }
        }
        #endregion
    }
}