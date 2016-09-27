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
        /// ���ָ����������
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

                //AS_ORDER_DETAIL��AS_ORDER_MASTER
                OrderDao orderDao = new OrderDao();
                orderDao.DeleteOrder(batchNo);

                //SC_STOCKMIXCHANNEL
                //StockChannelDao scDao = new StockChannelDao();
                //scDao.DeleteSchedule(batchNo);

                //SC_SUPPLY
                //SupplyDao supplyDao = new SupplyDao();
                //supplyDao.DeleteSchedule(batchNo);

                //SC_HANDLESUPPLY
                //HandleSupplyDao handleSupplyDao = new HandleSupplyDao();
                //handleSupplyDao.DeleteHandleSupply(batchNo);
            }
        }

        /// <summary>
        /// �����ʷ���ݣ����������ݡ�
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
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 1, 14));

                            //SC_CHANNELUSED
                            ChannelScheduleDao csDao = new ChannelScheduleDao();
                            //csDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 2, 14));

                            //SC_LINE
                            LineScheduleDao lsDao = new LineScheduleDao();
                            //lsDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 3, 14));

                            //SC_PALLETMASTER ,SC_ORDER
                            OrderScheduleDao osDao = new OrderScheduleDao();
                            //osDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 4, 14));

                            //SC_I_ORDERMASTER,SC_I_ORDERDETAIL,
                            OrderDao orderDao = new OrderDao();
                            //orderDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 5, 14));

                            //SC_STOCKMIXCHANNEL
                            StockChannelDao scDao = new StockChannelDao();
                            //scDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 6, 14));

                            //SC_SUPPLY
                            SupplyDao supplyDao = new SupplyDao();
                            //supplyDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 7, 14));

                            //SC_HANDLESUPPLY
                            HandleSupplyDao handleSupplyDao = new HandleSupplyDao();
                            //handleSupplyDao.DeleteHistory(historyDate);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 8, 14));

                            //ClearSchedule(orderDate, batchNo);

                            //////////////////////////////////////////////////////////////////////////

                            //���������
                            AreaDao areaDao = new AreaDao();
                            DataTable areaTable = ssDao.FindArea();
                            areaDao.SynchronizeArea(areaTable);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 9, 14));

                            //����������·��
                            RouteDao routeDao = new RouteDao();
                            DataTable routeTable = ssDao.FindRoute();
                            routeDao.SynchronizeRoute(routeTable);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 10, 14));

                            //���ؿͻ���
                            CustomerDao customerDao = new CustomerDao();
                            DataTable customerTable = ssDao.FindCustomer(dtOrder);
                            customerDao.SynchronizeCustomer(customerTable);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 11, 14));

                            //���ؾ��̱� ����ͬ��
                            CigaretteDao cigaretteDao = new CigaretteDao();
                            DataTable cigaretteTable = ssDao.FindCigarette(dtOrder);
                            cigaretteDao.SynchronizeCigarette(cigaretteTable);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 12, 14));

                            //��ѯ���Ż�������·���Խ����ų���
                            string routes = lsDao.FindRoutes(orderDate);

                            //���ض�������
                            DataTable masterTable = ssDao.FindOrderMaster(dtOrder, batchNo, routes);
                            orderDao.BatchInsertMaster(masterTable);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 13, 14));

                            //���ض�����ϸ
                            DataTable detailTable = ssDao.FindOrderDetail(dtOrder, batchNo, routes);
                            orderDao.BatchInsertDetail(detailTable);
                            if (OnSchedule != null)
                                OnSchedule(this, new ScheduleEventArgs(1, "�������������", 14, 14));

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
        /// ��ʼ�Ż�
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenSchedule(string orderDate, string batchNo)
        {
            try
            {
                //�������
                ClearSchedule(orderDate, batchNo);
                //�ּ����Ż�
                //����������·�߼�˳��ͳ������(������������)�����ȷֲ������ּ�����
                GenLineSchedule(orderDate, batchNo);

                //�ּ����̵��Ż�
                //�Ż������ݷŵ���SC_CHANNELUSED
                GenChannelSchedule(orderDate, batchNo);

                //��ֶ���
                GenSplitOrder(orderDate, batchNo);

                //�����Ż�
                //GenOrderSchedule(orderDate, batchNo);

                //�����Ż�1
                GenOrderSchedule2(orderDate, batchNo);

                //�����̵��Ż�
                GenStockChannelSchedule(orderDate, batchNo);

                //�����Ż�
                GenSupplySchedule(orderDate, batchNo);

                //�ֹ������Ż�
                GenHandleSupplySchedule(orderDate, batchNo);

                //����Ϊ���Ż�
                using (PersistentManager pm = new PersistentManager())
                {
                    BatchDao batchDao = new BatchDao();
                    if (!batchDao.CheckOrder(orderDate, batchNo))
                    {
                        throw new Exception("�Ż����̳������飡");
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



        #region  �Ż�����

        /// <summary>
        /// �������Ż�  2008-12-11�޸� 
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

                //�ҳ��������ڣ�������·�߼�˳��ͳ�ƶ�������(������������)
                DataTable routeTable = detailDao.FindRouteQuantity(batchNo).Tables[0];
                //�ҳ�����������
                DataTable lineTable = lineDao.GetAvailabeLine("2").Tables[0];

                //���浽��SC_LINE
                DataTable scLineTable = new DataTable();
                if (lineTable.Rows.Count > 0)
                {
                    scLineTable = lineSchedule.Optimize(routeTable, lineTable, orderDate, batchNo);
                    lineScDao.SaveLineSchedule(scLineTable);
                }
                else
                    throw new Exception("û�п��õķּ��ߣ�");


                routeTable = detailDao.FindRouteQuantity(orderDate, batchNo).Tables[0];
                lineTable = lineDao.GetAvailabeLine("3").Tables[0];
                if (lineTable.Rows.Count > 0)
                {
                    scLineTable = lineSchedule.Optimize(routeTable, lineTable, orderDate, batchNo);
                    lineScDao.SaveLineSchedule(scLineTable);
                }

                if (OnSchedule != null)
                    OnSchedule(this, new ScheduleEventArgs(2, "�������Ż�", 1, 1));
            }
        }

        /// <summary>
        /// �������̵��Ż�
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenChannelSchedule(string orderDate, string batchNo)
        {
            using (DB.Util.PersistentManager pm = new DB.Util.PersistentManager())
            {
                //�̵�
                ChannelDao channelDao = new ChannelDao();
                //����
                OrderDao detailDao = new OrderDao();
                //�������豸
                LineDeviceDao deviceDao = new LineDeviceDao();
                ChannelOptimize channelSchedule = new ChannelOptimize();

                Sorting.Dispatching.Dao.SysParameterDao parameterDao = new SysParameterDao();
                //�����ֵ�
                Dictionary<string, string> parameter = parameterDao.FindParameters();

                //�����߱�
                LineScheduleDao lineDao = new LineScheduleDao();
                DataTable lineTable = lineDao.FindAllLine(batchNo).Tables[0];

                LineInfoDao lineDao1 = new LineInfoDao();
                DataTable lineTable1 = lineDao1.GetAvailabeLine("3").Tables[0];

                //�Ƿ�ʹ������(������)�ּ���
                bool isUseWholePiecesSortLine = lineTable1.Rows.Count > 0;

                int currentCount = 0;
                int totalCount = lineTable.Rows.Count;

                foreach (DataRow lineRow in lineTable.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    //DataTable dtNoChannel = detailDao.FindNoCigaretteChannel(batchNo);
                    //for (int i = 0; i < dtNoChannel.Rows.Count; i++)
                    //{
                    //    Logger.Info((i + 1).ToString() + ". " + dtNoChannel.Rows[i]["CIGARETTENAME"].ToString() + "��û�й̶��̲�!");
                    //}
                    //if (dtNoChannel.Rows.Count > 0)
                    //    throw new Exception("��Ʒ�ƻ�û�й̶��̲֣����飡"); ;

                    //��ѯ�ּ����̵���
                    DataTable channelTable = channelDao.FindAvailableChannel(lineCode).Tables[0];
                    //��ѯ�ּ����豸������            
                    DataTable deviceTable = deviceDao.FindLineDevice(lineCode).Tables[0];

                    //ͨ���������ּ���Ʒ�Ʊ���һ�£���ʽ�������ּ�������Ҫһ��
                    //ȡ���ж���Ʒ�Ƽ�������
                    DataTable orderTable = detailDao.FindAllCigaretteQuantity(batchNo, isUseWholePiecesSortLine).Tables[0];

                    //��鶩�����Ƿ���δ�̶����̵�

                    channelSchedule.Optimize(orderTable, channelTable, deviceTable, parameter);

                    channelDao.SaveChannelSchedule(channelTable, orderDate, batchNo);
                    
                    //���¾�������Ϊ���
                    channelDao.UpdateCigaretteName(batchNo);

                    if (OnSchedule != null)
                        OnSchedule(this, new ScheduleEventArgs(3, "�����Ż�" + lineRow["LINECODE"].ToString() + "�ּ����̵�", ++currentCount, totalCount));
                }

                currentCount = 0;
                totalCount = lineTable1.Rows.Count;

                foreach (DataRow lineRow in lineTable1.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    //��ѯ�ּ����̵���
                    DataTable channelTable = channelDao.FindAvailableChannel(lineCode).Tables[0];
                    channelDao.SaveChannelSchedule(channelTable, orderDate, batchNo);


                    if (OnSchedule != null)
                        OnSchedule(this, new ScheduleEventArgs(3, "�����Ż�" + lineRow["LINECODE"].ToString() + "�ּ����̵�", ++currentCount, totalCount));
                }
            }
        }

        /// <summary>
        /// �������2010-07-05
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

                //��ѯ�ּ��߱�
                DataTable lineTable = lineDao.FindAllLine(batchNo).Tables[0];

                //������ʱ��
                //SC_TMP_PALLETMASTER
                //SC_TMP_ORDER"
                orderScheduleDao.ClearSplitOrder();

                LineInfoDao lineDao1 = new LineInfoDao();
                DataTable lineTable1 = lineDao1.GetAvailabeLine("3").Tables[0];
                bool isUseWholePiecesSortLine = lineTable1.Rows.Count > 0;

                foreach (DataRow lineRow in lineTable.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    //��ѯ�̵���Ϣ�� SC_CHANNELUSED��Order by CHANNELORDER
                    DataTable channelTable = channelDao.FindChannelSchedule(batchNo, lineCode).Tables[0];

                    //��ѯ�������� SC_I_ORDERMASTER ORDER BY ROW_NUMBER() over (ORDER BY AREA.SORTID,ROUTE.SORTID,ROUTECODE, SORTID) SORTNO
                    //DataTable masterTable = orderDao.FindOrderMaster(orderDate, batchNo, lineCode).Tables[0];
                    DataTable masterTable = orderDao.GetOrderMain(orderDate, batchNo, lineCode);

                    //��ȡһ��Ʒ�ƶ�ͨ������5�ı�����������ı�
                    DataTable dtQuantity5 = orderDao.GetOrderQuantity5(batchNo);

                    //��ѯ������ϸ�� SC_I_ORDERDETAIL ORDER BY ORDERID,CHANNELCODE
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
                        //    OnSchedule(this, new ScheduleEventArgs(4, "�����Ż�" + lineRow["LINECODE"].ToString() + "�ּ��߶���", ++currentCount, totalCount));
                    }
                    orderDao.UpdateQuantity(orderDate, batchNo);
                    channelDao.UpdateQuantity(channelTable, Convert.ToBoolean(parameter["IsUseBalance"]));


                }
            }
        }
        /// <summary>
        /// ���㶩��֮��ʱ��
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
                        //��������
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
                            //�׸������ڵ�ַ��
                            int address = (channelAddress - preChannelAddress);
                            //��������,����˶�������ͨ���Ĳ�������
                            //dicSupplyCount[channelCode];
                            //�˵��̵�Ƥ������
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
        /// �����Ż�2010-07-07
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

                    //��ѯ��������
                    DataTable masterTable = orderDao.FindTmpMaster(orderDate, batchNo, lineCode);
                    //��ѯ������ϸ��
                    DataTable orderTable = orderDao.FindTmpDetail(orderDate, batchNo, lineCode);
                    //��ѯ�ּ��̵���
                    DataTable channelTable = channelDao.FindChannelSchedule(orderDate, batchNo, lineCode, Convert.ToInt32(parameter["RemainCount"])).Tables[0];

                    int sortNo = 1;
                    int currentCount = 0;
                    int totalCount = masterTable.Rows.Count;

                    foreach (DataRow masterRow in masterTable.Rows)
                    {
                        DataRow[] orderRows = null;

                        //��ѯ��ǰ������ϸ
                        orderRows = orderTable.Select(string.Format("SORTNO = '{0}'", masterRow["SORTNO"]), "CHANNELGROUP, CHANNELCODE");
                        DataSet ds = orderSchedule.Optimize(masterRow, orderRows, channelTable, ref sortNo);

                        //orderScheduleDao.SaveOrder(ds);
                        supplyDao.InsertSupply(ds.Tables["SUPPLY"], lineCode, orderDate, batchNo);

                        if (OnSchedule != null)
                            OnSchedule(this, new ScheduleEventArgs(5, "�����Ż�" + lineRow["LINECODE"].ToString() + "�ּ��߶���", ++currentCount, totalCount));
                    }

                    channelDao.Update(channelTable);
                }

                LineInfoDao lineDao1 = new LineInfoDao();
                DataTable lineTable1 = lineDao1.GetAvailabeLine("3").Tables[0];

                foreach (DataRow lineRow in lineTable1.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    //��ѯ�̵���Ϣ��
                    DataTable channelTable = channelDao.FindChannelSchedule(orderDate, batchNo, lineCode, Convert.ToInt32(parameter["RemainCount"])).Tables[0];
                    //��ѯ��������
                    DataTable masterTable = orderDao.FindOrderMaster(orderDate, batchNo, lineCode).Tables[0];
                    //��ѯ������ϸ��
                    DataTable orderTable = orderDao.FindOrderDetail(orderDate, batchNo, lineCode).Tables[0];

                    int sortNo = 1;
                    int currentCount = 0;
                    int totalCount = masterTable.Rows.Count;

                    foreach (DataRow masterRow in masterTable.Rows)
                    {
                        DataRow[] orderRows = null;
                        //��ѯ��ǰ������ϸ
                        orderRows = orderTable.Select(string.Format("ORDERID = '{0}'", masterRow["ORDERID"]), "CIGARETTECODE");
                        DataSet ds = orderSchedule.OptimizeUseWholePiecesSortLine(masterRow, orderRows, channelTable, ref sortNo);
                        orderScheduleDao.SaveOrder(ds);

                        if (OnSchedule != null)
                            OnSchedule(this, new ScheduleEventArgs(5, "�����Ż�" + lineRow["LINECODE"].ToString() + "�ּ��߶���", ++currentCount, totalCount));
                    }

                    channelDao.UpdateQuantity(channelTable, false);
                }
            }
        }
        /// <summary>
        /// �����Ż�2010-07-07
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

                //��ѯ��������
                DataTable masterTable = orderDao.FindTmpMaster(orderDate, batchNo, lineCode);
                //��ѯ������ϸ��
                DataTable orderTable = orderDao.FindTmpDetail(orderDate, batchNo, lineCode);
                //��ѯ�ּ��̵���
                DataTable channelTable = channelDao.FindChannelSchedule(orderDate, batchNo, lineCode).Tables[0];

                int sortNo = 1;
                int currentCount = 0;
                int totalCount = masterTable.Rows.Count;

                foreach (DataRow masterRow in masterTable.Rows)
                {
                    DataRow[] orderRows = null;

                    //��ѯ��ǰ������ϸ
                    orderRows = orderTable.Select(string.Format("SORTNO = '{0}'", masterRow["SORTNO"]), "CHANNELGROUP, CHANNELCODE");
                    DataSet ds = orderSchedule.Optimize(masterRow, orderRows, channelTable, ref sortNo);

                    //orderScheduleDao.SaveOrder(ds);
                    //supplyDao.InsertSupply(ds.Tables["SUPPLY"], orderDate, batchNo);

                    if (OnSchedule != null)
                        OnSchedule(this, new ScheduleEventArgs(5, "�����Ż��ּ��߶���", ++currentCount, totalCount));
                }

                channelDao.Update(channelTable);

            }
        }
        /// <summary>
        /// �����̵��Ż�2010-07-08
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

                //ÿ��ּ�����󱸻��̵��Ƿ�Ϊ��
                if (parameter["ClearStockChannel"] == "1")
                    schannelDao.ClearCigarette();

                //��ѯ�����̵��� CMD_STOCKCHANNEL ORDER BY ORDERNO
                DataTable channelTable = schannelDao.FindChannel();
                //��ѯͨ��������������Ϣ��
                //DataTable orderCTable = orderDao.FindCigaretteQuantityFromChannelUsed(orderDate, batchNo, "3");
                DataTable orderCTable = orderDao.FindCigaretteQuantityFromChannelUsed(orderDate, batchNo);
                //��ѯ��ʽ������������Ϣ��Ӧ���ϻ���̵����⣩
                DataTable orderTTable = orderDao.FindCigaretteQuantityFromChannelUsed(orderDate, batchNo, "2");
                //ȡ���ж���Ʒ�Ƽ�������
                DataTable orderTable = detailDao.FindAllCigaretteQuantity(orderDate, batchNo, false).Tables[0];

                StockOptimize stockOptimize = new StockOptimize();

                //bool isUseSynchronizeOptimize = Convert.ToBoolean(parameter["IsUseSynchronizeOptimize"]);
                //DataTable mixTable = stockOptimize.Optimize(isUseSynchronizeOptimize, channelTable, isUseSynchronizeOptimize ? orderCTable : orderTable, isUseSynchronizeOptimize ? orderTTable : orderTable, orderDate, batchNo);

                stockOptimize.Optimize(channelTable, orderCTable);

                schannelDao.UpdateChannel(channelTable);
                schannelDao.InsertStockChannelUsed(orderDate, batchNo, channelTable);
                //schannelDao.InsertMixChannel(mixTable);

                if (OnSchedule != null)
                    OnSchedule(this, new ScheduleEventArgs(6, "�����̵��Ż�", 1, 1));
            }
        }

        /// <summary>
        /// �����Ż�2010-04-19
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
                        OnSchedule(this, new ScheduleEventArgs(7, "�����Ż�" + lineRow["LINECODE"].ToString() + "�����ƻ�", currentCount++, totalCount));
                }

                LineInfoDao lineDao1 = new LineInfoDao();
                DataTable lineTable1 = lineDao1.GetAvailabeLine("3").Tables[0];

                foreach (DataRow lineRow in lineTable1.Rows)
                {
                    string lineCode = lineRow["LINECODE"].ToString();

                    //��ѯ�̵���Ϣ��
                    DataTable channelTable = channelDao.FindChannelSchedule(orderDate, batchNo, lineCode, Convert.ToInt32(parameter["RemainCount"])).Tables[0];
                    //��ѯ��������
                    DataTable masterTable = orderDao.FindOrderMaster(orderDate, batchNo, lineCode).Tables[0];
                    //��ѯ������ϸ��
                    DataTable orderTable = orderDao.FindOrderDetail(orderDate, batchNo, lineCode).Tables[0];

                    int serialNo = 1;
                    currentCount = 0;
                    totalCount = masterTable.Rows.Count;

                    foreach (DataRow masterRow in masterTable.Rows)
                    {
                        DataRow[] orderRows = null;
                        //��ѯ��ǰ������ϸ
                        orderRows = orderTable.Select(string.Format("ORDERID = '{0}'", masterRow["ORDERID"]), "CIGARETTECODE");
                        DataTable supplyTable = supplyOptimize.Optimize(channelTable, orderRows, ref serialNo);
                        supplyDao.InsertSupply(supplyTable, true);

                        if (OnSchedule != null)
                            OnSchedule(this, new ScheduleEventArgs(7, "�����Ż�" + lineRow["LINECODE"].ToString() + "�ּ��߶���������", ++currentCount, totalCount));
                    }
                }
            }
        }

        /// <summary>
        /// �ֹ������Ż�
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

                    //�����µ��ֹ�����������
                    DataTable newSupplyOrders = handleSupplyOptimize.Optimize(handSupplyOrders, multiBrandChannel);

                    //�����̵��ղ���ҵ��SortNo
                    channelDao.Update(multiBrandChannel, orderDate, batchNo);

                    //ɾ��sc_orderԭ�����ֹ���������
                    scOrderDao.DeleteOldSupplyOrders(orderDate, batchNo, lineCode);
                    //��sc_order�в����µ��ֹ���������
                    scOrderDao.InsertNewSupplyOrders(newSupplyOrders);


                    //��SC_HANDLESUPPLY�в����µ��ֹ���������
                    scOrderDao.InsertHandSupplyOrders(newSupplyOrders);

                    if (OnSchedule != null)
                        OnSchedule(this, new ScheduleEventArgs(8, "�����Ż�" + lineRow["LINECODE"].ToString() + "�ּ����ֹ�������������", ++currentCount, totalCount));
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
        /// �����Ż�2010-04-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void GenSupplySchedule1(string orderDate, string batchNo)
        {
            int batch = int.Parse(batchNo.Substring(10));
            using (PersistentManager pm = new PersistentManager())
            {
                BatchDao batchDao = new BatchDao();
                OrderDao orderDao = new OrderDao();
                ChannelDao channelDao = new ChannelDao();
                LineScheduleDao lineDao = new LineScheduleDao();
                SupplyDao supplyDao = new SupplyDao();
                SupplyOptimize supplyOptimize = new SupplyOptimize();

                //��������
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

                    //������������
                    supplyDao.InsertBalanceSupply(lineCode, orderDate, batchNo);

                    int bno = int.Parse(batchNo.Substring(10, 3));
                    string firstBatchNo = batchNo.Substring(0, 10) + "001";
                    //��ȡ��һ���ε����κ�
                    string preBatchNo = batchDao.GetPreBatchNo(batchNo);

                    channelDao.InsertChannelBalance(bno, firstBatchNo, batchNo, preBatchNo);
                    //��һ���ֿ���������
                    //����ڶ���������Ʒ�ƣ�Ҫ����QUANTITY,QUANTITY1��Ϊ�ֿ���������

                    //������������
                    channelDao.UpdateChannelBalance(bno, firstBatchNo);


                    //ͨ����β��Ӧ�ò���ǰ��
                    //���������Ƿ��������粻���貹�����̣��ټ�������
                    DataTable channelTable = channelDao.FindChannelBalance3(orderDate, firstBatchNo).Tables[0];
                    DataTable supplyTable = supplyOptimize.Optimize3(channelTable, batchNo, bno, preBatchNo);
                    supplyDao.InsertSupply(supplyTable, Convert.ToBoolean(parameter["IsSupplyOrderbyCigaretteCode"]));
                    channelDao.UpdateChannelBalance(channelTable, bno);

                    //��ȡͨ����������
                    channelTable = channelDao.FindChannel3(orderDate, batchNo, firstBatchNo, bno - 1).Tables[0];
                    DataTable orderDetail = orderDao.GetChannelOrderDetail(orderDate, batchNo, lineCode);
                    DataRow[] orderRows = orderDetail.Select("CHANNELTYPE=3", "SORTNO,CHANNELCODE");
                    //DataTable supplyTable = supplyOptimize.Optimize(channelTable, orderRows);
                    //supplyTable = supplyOptimize.Optimize1(channelTable, orderRows, false);
                    //2015-04-07�޸Ĳ����Ż�
                    //�������OrderCount<=1,��ô����֮ǰ�Ĳ����Ż��������Զ���OrderCount������Ϊһ���Ĳ����Ż�
                    int OrderCount = int.Parse(parameter["OrderCount"]);
                    //DataTable supplyTable;
                    if(OrderCount<=1)
                        supplyTable = supplyOptimize.Optimize2(channelTable, orderRows);
                    else
                        supplyTable = supplyOptimize.Optimize6(channelTable, orderRows, OrderCount);

                    supplyDao.InsertSupply(supplyTable, Convert.ToBoolean(parameter["IsSupplyOrderbyCigaretteCode"]));

                    //ͨ��������ͨ��˳��
                    //DataTable channelTable = channelDao.FindChannel31(orderDate, batchNo).Tables[0];
                    //DataTable supplyTable = supplyOptimize.Optimize5(channelTable);
                    //supplyDao.InsertSupply(supplyTable, Convert.ToBoolean(parameter["IsSupplyOrderbyCigaretteCode"]));

                    
                    //���������Ƿ��������粻���貹�����̣��ټ�������
                    //channelTable = channelDao.FindChannelBalance3(orderDate, firstBatchNo).Tables[0];
                    //supplyTable = supplyOptimize.Optimize3(channelTable, batchNo, bno, preBatchNo);
                    //supplyDao.InsertSupply(supplyTable, Convert.ToBoolean(parameter["IsSupplyOrderbyCigaretteCode"]));
                    //channelDao.UpdateChannelBalance(channelTable, bno);

                    //��ʽ�������Ż�
                    channelTable = channelDao.FindChannel2(orderDate, batchNo, firstBatchNo, bno - 1).Tables[0];
                    //��ʽ���Ķ�����Ʒ�����ͳ��
                    orderDetail = orderDao.GetChannelOrderDetail2(orderDate, batchNo, lineCode);
                    //orderRows = orderDetail.Select("CHANNELTYPE=2", "SORTNO,CHANNELADDRESS");
                    orderRows = orderDetail.Select("", "SORTNO,CHANNELCODE");
                    //supplyTable2 = supplyOptimize.Optimize1(channelTable, orderRows, false);
                    DataTable supplyTable2 = supplyOptimize.Optimize4(channelTable, orderRows);
                    supplyDao.InsertSupply2(supplyTable2, Convert.ToBoolean(parameter["IsSupplyOrderbyCigaretteCode"]));

                    //20151111�޸ģ����ݰ������������ʽ�������������ԣ�ֻ������������


                    ////20150323�޸�
                    //channelTable = channelDao.FindChannel21(orderDate, batchNo).Tables[0];

                    //DataTable supplyTable2 = supplyOptimize.Optimize5(channelTable);
                    //supplyDao.InsertSupply2(supplyTable2, Convert.ToBoolean(parameter["IsSupplyOrderbyCigaretteCode"]));

                    //��ʽ��
                    channelTable = channelDao.FindChannelBalance2(orderDate, firstBatchNo).Tables[0];
                    supplyTable2 = supplyOptimize.Optimize3(channelTable, batchNo, bno, preBatchNo);
                    supplyDao.InsertSupply2(supplyTable2, Convert.ToBoolean(parameter["IsSupplyOrderbyCigaretteCode"]));
                    channelDao.UpdateChannelBalance(channelTable, bno);

                    if (OnSchedule != null)
                        OnSchedule(this, new ScheduleEventArgs(7, "�����Ż�" + lineRow["LINECODE"].ToString() + "�����ƻ�", currentCount++, totalCount));
                }

            }
        }

        /// <summary>
        /// �ֹ������Ż�
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

                    //�����µ��ֹ�����������
                    DataTable newSupplyOrders = handleSupplyOptimize.Optimize(handSupplyOrders, multiBrandChannel);

                    //�����̵��ղ���ҵ��SortNo
                    channelDao.Update(multiBrandChannel, orderDate, batchNo);

                    //ɾ��sc_orderԭ�����ֹ���������
                    scOrderDao.DeleteOldSupplyOrders(orderDate, batchNo, lineCode);
                    //��sc_order�в����µ��ֹ���������
                    scOrderDao.InsertNewSupplyOrders(newSupplyOrders);


                    //��SC_HANDLESUPPLY�в����µ��ֹ���������
                    scOrderDao.InsertHandSupplyOrders(newSupplyOrders);

                    if (OnSchedule != null)
                        OnSchedule(this, new ScheduleEventArgs(8, "�����Ż�" + lineRow["LINECODE"].ToString() + "�ּ����ֹ�������������", ++currentCount, totalCount));
                }
            }
        }
        #endregion
    }
}