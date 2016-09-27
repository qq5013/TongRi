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
                Logger.Error("OrderRequestProcess ��ʼ��ʧ�ܣ�ԭ��" + e.Message);
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

                //��ȡ�Ƿ��жϷּ�
                //object[] obj = ObjectUtil.GetObjects(WriteToService("SortPLC", "BreakSorting" + channelType));
                //if (obj == null)
                //    return;
                //if (obj[0] == "1")
                //    return;
                //��ȡA�߶�����ϸ��д��PLC
                object o = ObjectUtil.GetObject(stateItem.State);
                if (o == null)
                    return;
                //Logger.Info("��������:" + channelType + ",����ֵ:" + o.ToString());
                if (o != null && o.ToString() == "1")
                {
                    using (PersistentManager pm = new PersistentManager())
                    {
                        OrderDao orderDao = new OrderDao();
                        try
                        {
                            //Ҫ���ݷּ������ѯ����
                            //string rno = requestNo;
                            //if (requestNo == "4")
                            //    rno = "0";
                            DataTable masterTable = orderDao.FindSortMaster(channelType);

                            if (masterTable.Rows.Count != 0)
                            { 
                                //��ǰ��ˮ��
                                string sortNo = masterTable.Rows[0]["SORTNO"].ToString();
                                
                                //���㵱ǰ��ˮ��ʣ�¶������Լ�ͨ��ʣ����

                                string maxSortNo = orderDao.FindMaxSortNo(channelType);
                                
                                //if (int.Parse(sortNo) > int.Parse(maxSortNo))
                                //    return;
                                //��ѯ������ϸ                                
                                DataTable detailTable = orderDao.FindSortDetail(sortNo, channelType);
                                //��ѯ���ּ����������ˮ�ţ��ж��Ƿ����
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

                                //�ּ����
                                //orderData[89] = Convert.ToInt32(packNo);
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
                                    Logger.Info(string.Format((channelType=="3"?"ͨ����":"��ʽ��") + "�·��������ݳɹ�,�ּ𶩵���[{0}]��", sortNo));

                                    //д��ÿ��ͨ�������һ������
                                    //DataTable dt = orderDao.FindChannelMaxSortNo();

                                    DataTable dt = orderDao.FindChannelMaxSortNo(MinBalance);
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
                            Logger.Error(string.Format((channelType == "3" ? "ͨ����" : "��ʽ��") + " �� �·���������ʧ�ܣ�ԭ��{0}�� {1}", e.Message, "OrderRequestProcess.cs �кţ�100��"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("�ּ𶩵��������ʧ�ܣ�ԭ��{0}�� {1}", ex.Message, "OrderRequestProcess.cs �кţ�108��"));
            }
        }
    }
}
