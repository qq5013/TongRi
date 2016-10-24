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
                Logger.Error(string.Format("CurrentOrderProcess ��ʼ��ʧ�ܣ�ԭ��{0}�� {1}", e.Message, "CurrentOrderProcess.cs �кţ�26��"));
            }

        }
        
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                //string channelType = stateItem.ItemName.Substring(11, 1);
                string channelType = "2";

                object o = ObjectUtil.GetObject(stateItem.State);
                if (o == null)
                    return;

                string sortNo = o.ToString();
                //ˢ�·ּ�״̬                    
                Refresh(sortNo, channelType);

                if (int.Parse(sortNo) > 0)
                {
                    //�жϴ���ˮ���Ƿ�ּ����
                    
                    string channelName = channelType == "3" ? "ͨ����" : "��ʽ��";
                    Logger.Info("�������[" + sortNo + "]�����");
                }
            }

            catch (Exception e)
            {
                Logger.Error(string.Format("��ɶ�����Ϣ����ʧ�ܣ�ԭ��{0}�� {1}", e.Message, "CurrentOrderProcess.cs �кţ�53��"));
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
                    
                    //�������ʱ��
                    string maxSortNo = orderDao.FindLastSortNo();
                    orderDao.UpdateFinishTime(sortNo, channelType, maxSortNo);           

                    //ˢ��������ּ�״̬
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
                    //refreshData.Average = orderDao.FindDispatchingAverage();

                    WriteToProcess("SortingStatus", "RefreshData", refreshData);                    
                }
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("���·ּ���Ϣ����ʧ�ܣ�ԭ��{0}�� {1}", e.Message, "CurrentOrderProcess.cs �кţ�97��"));
            }
        }
    }
}
