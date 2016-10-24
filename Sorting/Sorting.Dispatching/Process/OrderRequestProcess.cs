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
                Logger.Error("OrderRequestProcess ��ʼ��ʧ�ܣ�ԭ��" + e.Message);
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
                Logger.Error(string.Format("�ּ𶩵��������ʧ�ܣ�ԭ��{0}�� {1}", ex.Message, "OrderRequestProcess.cs �кţ�108��"));
            }
        }
        private void tmWorker(object sender, ElapsedEventArgs e)
        {
            try
            {
                tmWorkTimer.Stop();

                //��������Ƿ�����
                if (bSort)
                {
                    //��ʽ���ּ�
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
            //����״̬
            object[] os = ObjectUtil.GetObjects(WriteToService("SortPLC", "DeviceRunState"));
            if (ob == null)
                return;

            int AFlag = int.Parse(oa[0].ToString());
            int BFlag = int.Parse(ob[0].ToString());
            int State = int.Parse(os[0].ToString());

            //����״̬
            //if (State != 1)
            //    return;
            //�������λ��Ϊ0,������
            if (AFlag > 0 || BFlag > 0 )
                return;

            string channelType = "2";

            using (PersistentManager pm = new PersistentManager())
            {
                OrderDao orderDao = new OrderDao();
                DataTable masterTable = orderDao.FindSortMaster(channelType);

                if (masterTable.Rows.Count != 0)
                {
                    //��ǰ��ˮ��
                    string sortNo = masterTable.Rows[0]["SORTNO"].ToString();

                    //���㵱ǰ��ˮ��ʣ�¶������Լ�ͨ��ʣ����

                    DataTable dtMax = orderDao.FindMaxSortNoChannelAddress();
                    if (dtMax.Rows.Count <= 0)
                        return;
                    //string maxSortNo = orderDao.FindMaxSortNo(channelType);

                    string maxSortNo = dtMax.Rows[0]["SortNo"].ToString();
                    //���ηּ��������AB�����Ĳ�λ��ַ���жϷּ�����ı�־,δ�����һ�ʶ����ŵı�־λ��Ϊ0
                    int channelAddress = int.Parse(dtMax.Rows[0]["ChannelAddress"].ToString());

                    //��ѯ������ϸ                                
                    DataTable detailTableA = orderDao.FindSortDetail(sortNo, channelType, 0);

                    int[] orderDataA = new int[250];
                    int indexA = 0;

                    for (int i = 0; i < detailTableA.Rows.Count; i++)
                    {
                        //��λ��ַ
                        int channelAddressA = Convert.ToInt32(detailTableA.Rows[i]["CHANNELADDRESS"]);
                        int lastAddressA = Convert.ToInt32(detailTableA.Rows[i]["LASTCHANNELADDRESS"]);
                        orderDataA[indexA++] = channelAddressA;
                        //��λ����
                        orderDataA[indexA++] = Convert.ToInt32(detailTableA.Rows[i]["QUANTITY"]);
                        //�������
                        orderDataA[indexA++] = int.Parse(sortNo);
                        //��������
                        orderDataA[indexA++] = Convert.ToInt32(detailTableA.Rows[i]["ORDERQUANTITY"]);
                        //������־
                        if (maxSortNo == sortNo && channelAddressA == channelAddress)
                            orderDataA[indexA++] = 1;
                        else
                            orderDataA[indexA++] = 0;
                    }
                    WriteToService("SortPLC", "ASortOrder", orderDataA);

                    Logger.Info(string.Format("A���·��������ݳɹ�,�ּ𶩵���[{0}]��", sortNo));


                    //��ѯ������ϸ                                
                    DataTable detailTableB = orderDao.FindSortDetail(sortNo, channelType, 1);

                    int[] orderDataB = new int[250];
                    int indexB = 0;

                    for (int i = 0; i < detailTableB.Rows.Count; i++)
                    {
                        //��λ��ַ
                        int channelAddressB = Convert.ToInt32(detailTableB.Rows[i]["CHANNELADDRESS"]);
                        int lastAddressB = Convert.ToInt32(detailTableB.Rows[i]["LASTCHANNELADDRESS"]);
                        orderDataB[indexB++] = channelAddressB;
                        //��λ����
                        orderDataB[indexB++] = Convert.ToInt32(detailTableB.Rows[i]["QUANTITY"]);
                        //�������
                        orderDataB[indexB++] = int.Parse(sortNo);
                        //��������
                        orderDataB[indexB++] = Convert.ToInt32(detailTableB.Rows[i]["ORDERQUANTITY"]);
                        //������־
                        if (maxSortNo == sortNo && channelAddressB == channelAddress)
                            orderDataB[indexB++] = 1;
                        else
                            orderDataB[indexB++] = 0;
                    }

                    WriteToService("SortPLC", "BSortOrder", orderDataB);
                    Logger.Info(string.Format("B���·��������ݳɹ�,�ּ𶩵���[{0}]��", sortNo));

                    if (WriteToService("SortPLC", "ASortOrderFlag", 1) && WriteToService("SortPLC", "BSortOrderFlag", 1))
                    {
                        orderDao.UpdateOrderStatus(sortNo, "1", channelType);
                        //Logger.Info(string.Format("�·��������ݳɹ�,�ּ𶩵���[{0}]��", sortNo));
                    }
                }
            }
        }
    }
}
