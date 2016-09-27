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
    public class MergeRequestProcess : AbstractProcess
    {
        private bool IsBreakMerge = false;
        public override void Initialize(Context context)
        {
            try
            {
                base.Initialize(context);
            }
            catch (Exception e)
            {
                Logger.Error("MergeRequestProcess ��ʼ��ʧ�ܣ�ԭ��" + e.Message);
            }

        }

        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                object o = ObjectUtil.GetObject(stateItem.State);
                if (o == null)
                    return;

                if (o.ToString() == "0")
                    return;

                using (PersistentManager pm = new PersistentManager())
                {
                    OrderDao orderDao = new OrderDao();
                    {
                        try
                        {
                            if (stateItem.ItemName == "MergeBreakSortNo")
                            {
                                //��ɺϲ�����
                                string sortNo = o.ToString();
                                //�ж��ǲ���������ɵ��ţ��������д����ɱ�־
                                string maxSortNo = orderDao.FindMaxSortNo();
                                if (sortNo == maxSortNo)
                                {
                                    WriteToService("SortPLC", "MergeFinish", 1);
                                    Logger.Info(string.Format("�ϵ������,�ּ𶩵���[{0}]��", sortNo));
                                }
                            }
                            else if(o.ToString() == "1")
                            {
                                object[] obj = ObjectUtil.GetObjects(WriteToService("SortPLC", "MergeBreakSortNo"));
                                object[] obj3 = ObjectUtil.GetObjects(WriteToService("SortPLC", "MergeBreakCount3"));
                                object[] obj2 = ObjectUtil.GetObjects(WriteToService("SortPLC", "MergeBreakCount2"));

                                if (stateItem.ItemName == "MergeBreakRequest")
                                {
                                    IsBreakMerge = true;
                                    string sortNo = obj[0].ToString();
                                    orderDao.UpdateBreakMergeOrderStatus(sortNo);

                                    WriteToService("SortPLC", "MergeBreakFinish", 1);

                                    Logger.Info(string.Format("�ϵ��ж�����д��ɹ�,�ּ𶩵���[{0}]��", sortNo));
                                }

                                else
                                {
                                    DataTable masterTable = orderDao.FindMergeSortMaster();

                                    if (masterTable.Rows.Count != 0)
                                    {
                                        string sortNo = masterTable.Rows[0]["SORTNO"].ToString();
                                        int MergeBreakCount3 = int.Parse(obj3[0].ToString());
                                        int MergeBreakCount2 = int.Parse(obj2[0].ToString());

                                        int[] merge = new int[11];
                                        if (IsBreakMerge)
                                        {
                                            merge[0] = int.Parse(masterTable.Rows[0]["QUANTITY"].ToString()) - MergeBreakCount3;
                                            merge[1] = int.Parse(masterTable.Rows[0]["QUANTITY1"].ToString()) - MergeBreakCount2;
                                        }
                                        else
                                        {
                                            merge[0] = int.Parse(masterTable.Rows[0]["QUANTITY"].ToString());
                                            merge[1] = int.Parse(masterTable.Rows[0]["QUANTITY1"].ToString());
                                        }
                                        merge[10] = int.Parse(sortNo);


                                        if (WriteToService("SortPLC", "MergeOrderData1", merge))
                                        {
                                            orderDao.UpdateMergeOrderStatus(sortNo, "1");
                                            WriteToService("SortPLC", "MergeOrderData2", 1);
                                            IsBreakMerge = false;
                                            Logger.Info(string.Format("�ϵ�����д��ɹ�,�ּ𶩵���[{0}]��", sortNo));
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Error(string.Format("�ϵ�����д��ʧ�ܣ�ԭ��{0}�� {1}", e.Message, "MergeRequestProcess.cs �кţ�72��"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("�ϵ�����д��ʧ�ܣ�ԭ��{0}�� {1}", ex.Message, "MergeRequestProcess.cs �кţ�78��"));
            }
        }
    }
}
