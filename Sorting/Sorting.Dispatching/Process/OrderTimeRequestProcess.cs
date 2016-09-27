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
                Logger.Error("OrderRequestProcess ��ʼ��ʧ�ܣ�ԭ��" + e.Message);
            }

        }
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                switch (stateItem.ItemName)
                {
                    //��ʼ�ּ��������á�
                    case "OrderRequest":
                        bRequest = (int)stateItem.State == 1;
                        break;
                }
            }

            catch (Exception e)
            {
                Logger.Error("�����������ʧ�ܣ�ԭ��" + e.Message);
            }
        }
        private void tmWorker(object sender, ElapsedEventArgs e)
        {
            try
            {
                tmWorkTimer.Stop();

                //��������Ƿ�����
                if (bRequest)
                {
                    //ͨ��������
                    Worker3();
                    //��ʽ������
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

            //�������λ��Ϊ0,������
            if (RequestCount <= 0)
                return;

            object[] obj = ObjectUtil.GetObjects(WriteToService("SortPLC", "OrderData3"));
            if (obj == null)
                return;
            int reqeustNo = int.Parse(obj[26].ToString());
            //�����ɱ�־��Ϊ0��������
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

            //�������λΪ0,������
            if (RequestCount <= 0)
                return;

            object[] obj = ObjectUtil.GetObjects(WriteToService("SortPLC", "OrderData2"));
            if (obj == null)
                return;
            int reqeustNo = int.Parse(obj[81].ToString());
            //�����ȡ����ˮ�Ų�Ϊ0��������
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
                        //��ǰ��ˮ��
                        string sortNo = masterTable.Rows[0]["SORTNO"].ToString();

                        string maxSortNo = orderDao.FindMaxSortNo(channelType);

                        //��ѯ������ϸ                                
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
                            //��������
                            orderData[25] = quantity;
                            //�ּ���ˮ��
                            orderData[26] = Convert.ToInt32(sortNo);
                        }
                        else
                        {
                            //��������
                            orderData[80] = quantity;
                            //�ּ���ˮ��
                            orderData[81] = Convert.ToInt32(sortNo);
                        }

                        if (WriteToService("SortPLC", "OrderData" + channelType, orderData))
                        {
                            orderDao.UpdateOrderStatus(sortNo, "1", channelType);
                            DataSet ds = orderDao.GetOrder(sortNo);
                            //Order.OrderInfo(ds);
                            Logger.Info(string.Format((channelType == "3" ? "ͨ����" : "��ʽ��") + "�·��������ݳɹ�,�ּ𶩵���[{0}]��", sortNo));

                            //д��ÿ��ͨ�������һ������
                            DataTable dt = orderDao.FindChannelMaxSortNo();
                            int[] channelSortNo = new int[dt.Rows.Count];
                            for (int i = 0; i < dt.Rows.Count; i++)
                                channelSortNo[i] = int.Parse(dt.Rows[i]["SORTNO"].ToString());
                            WriteToService("SortPLC", "LastChannelSortNo", channelSortNo);
                        }
                        //�Ƿ����һ��
                        if (sortNo == maxSortNo)
                        {
                            WriteToService("SortPLC", "LastOrder" + channelType, 1);
                            Logger.Info(string.Format((channelType == "3" ? "ͨ����" : "��ʽ��") + "��ɱ�־��д��,�ּ𶩵���[{0}]��", sortNo));
                            //���������ˮ��֮������Ϊ0�ĵ���
                            orderDao.UpdateOrderStatus(channelType);
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(string.Format((channelType == "3" ? "ͨ����" : "��ʽ��") + " �� �·���������ʧ�ܣ�ԭ��{0}�� {1}", e.Message, "OrderRequestProcess.cs �кţ�100��"));
                }
            }
        }        
    }
}
