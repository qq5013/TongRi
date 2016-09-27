using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using Sorting.Dispatching.Dao;
using DB.Util;

namespace Sorting.Dispatching.Schedule
{
    public class DownLoadData
    {
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
                batchDao.UpdateExecuter("0", "0", orderDate, batchNo);
                batchDao.UpdateIsValid(orderDate, batchNo, "0");

                //SC_CHANNELUSED
                ChannelScheduleDao csDao = new ChannelScheduleDao();
                csDao.DeleteSchedule(batchNo);

                //SC_LINE
                LineScheduleDao lsDao = new LineScheduleDao();
                lsDao.DeleteSchedule(orderDate, batchNo);

                //SC_PALLETMASTER,SC_ORDER
                OrderScheduleDao osDao = new OrderScheduleDao();
                osDao.DeleteSchedule(batchNo);

                //SC_I_ORDERDETAIL��SC_I_ORDERMASTER
                OrderDao orderDao = new OrderDao();
                orderDao.DeleteOrder(batchNo);

                //SC_STOCKMIXCHANNEL
                StockChannelDao scDao = new StockChannelDao();
                scDao.DeleteSchedule(batchNo);

                //SC_SUPPLY
                SupplyDao supplyDao = new SupplyDao();
                supplyDao.DeleteSchedule(batchNo);

                //SC_HANDLESUPPLY
                HandleSupplyDao handleSupplyDao = new HandleSupplyDao();
                handleSupplyDao.DeleteHandleSupply(batchNo);

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
                ProcessState.Status = "PROCESSING";
                ProcessState.TotalCount = 15;
                ProcessState.Step = 1;

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
                            ProcessState.CompleteCount = 1;

                            //SC_CHANNELUSED
                            ChannelScheduleDao csDao = new ChannelScheduleDao();
                            //csDao.DeleteHistory(historyDate);
                            ProcessState.CompleteCount = 2;

                            //SC_LINE
                            LineScheduleDao lsDao = new LineScheduleDao();
                            //lsDao.DeleteHistory(historyDate);
                            ProcessState.CompleteCount = 3;

                            //SC_PALLETMASTER ,SC_ORDER
                            OrderScheduleDao osDao = new OrderScheduleDao();
                            //osDao.DeleteHistory(historyDate);
                            ProcessState.CompleteCount = 4;

                            //SC_I_ORDERMASTER,SC_I_ORDERDETAIL,
                            OrderDao orderDao = new OrderDao();
                            //orderDao.DeleteHistory(historyDate);
                            ProcessState.CompleteCount = 5;

                            //SC_STOCKMIXCHANNEL
                            StockChannelDao scDao = new StockChannelDao();
                            //scDao.DeleteHistory(historyDate);
                            ProcessState.CompleteCount = 6;

                            //SC_SUPPLY
                            SupplyDao supplyDao = new SupplyDao();
                            //supplyDao.DeleteHistory(historyDate);
                            ProcessState.CompleteCount = 7;

                            //SC_HANDLESUPPLY
                            HandleSupplyDao handleSupplyDao = new HandleSupplyDao();
                            //handleSupplyDao.DeleteHistory(historyDate);
                            ProcessState.CompleteCount = 8;

                            //ClearSchedule(orderDate, batchNo);

                            //////////////////////////////////////////////////////////////////////////

                            //���������
                            AreaDao areaDao = new AreaDao();
                            DataTable areaTable = ssDao.FindArea();
                            areaDao.SynchronizeArea(areaTable);
                            ProcessState.CompleteCount = 9;

                            //����������·��
                            RouteDao routeDao = new RouteDao();
                            DataTable routeTable = ssDao.FindRoute();
                            routeDao.SynchronizeRoute(routeTable);
                            ProcessState.CompleteCount = 10;

                            //���ؿͻ���
                            CustomerDao customerDao = new CustomerDao();
                            DataTable customerTable = ssDao.FindCustomer(dtOrder);
                            customerDao.SynchronizeCustomer(customerTable);
                            ProcessState.CompleteCount = 11;

                            //���ؾ��̱� ����ͬ��
                            CigaretteDao cigaretteDao = new CigaretteDao();
                            DataTable cigaretteTable = ssDao.FindCigarette(dtOrder);
                            cigaretteDao.SynchronizeCigarette(cigaretteTable);
                            ProcessState.CompleteCount = 12;

                            //��ѯ���Ż�������·���Խ����ų���
                            string routes = lsDao.FindRoutes(orderDate);

                            //���ض�������
                            DataTable masterTable = ssDao.FindOrderMaster(dtOrder, batchNo, routes);
                            orderDao.BatchInsertMaster(masterTable);
                            ProcessState.CompleteCount = 13;

                            //���ض�����ϸ
                            DataTable detailTable = ssDao.FindOrderDetail(dtOrder, batchNo, routes);
                            orderDao.BatchInsertDetail(detailTable);
                            ProcessState.CompleteCount = 14;

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
                ProcessState.Status = "ERROR";
                ProcessState.Message = ee.Message;
            }
        }
    }
}
