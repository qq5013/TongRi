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
    public class MergeBreakRequestProcess : AbstractProcess
    {

        public override void Initialize(Context context)
        {
            try
            {
                base.Initialize(context);
            }
            catch (Exception e)
            {
                Logger.Error("MergeRequestProcess 初始化失败！原因：" + e.Message);
            }

        }

        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {

                object o = ObjectUtil.GetObject(stateItem.State);
                if (o == null)
                    return;

                if (o.ToString() == "1")
                {
                    using (PersistentManager pm = new PersistentManager())
                    {
                        OrderDao orderDao = new OrderDao();

                        object[] obj = ObjectUtil.GetObjects(WriteToService("SortPLC", "MergeBreakSortNo"));
                        object[] obj3 = ObjectUtil.GetObjects(WriteToService("SortPLC", "MergeBreakCount3"));
                        object[] obj2 = ObjectUtil.GetObjects(WriteToService("SortPLC", "MergeBreakCount2"));
                        int sortNo = int.Parse(obj[0].ToString()) + 1;
                        int MergeBreakCount3 = int.Parse(obj3[0].ToString());
                        int MergeBreakCount2 = int.Parse(obj2[0].ToString());

                        DataTable masterTable = orderDao.FindMergeSortMaster(sortNo);

                        if (masterTable.Rows.Count != 0)
                        {
                            int[] merge = new int[11];
                            merge[0] = int.Parse(masterTable.Rows[0]["QUANTITY"].ToString()) - MergeBreakCount3;
                            merge[1] = int.Parse(masterTable.Rows[0]["QUANTITY1"].ToString()) - MergeBreakCount2;
                            merge[10] = sortNo;

                            if (WriteToService("SortPLC", "MergeOrderData1", merge))
                            {
                                orderDao.UpdateMergeOrderStatus(sortNo.ToString(), "1");
                                WriteToService("SortPLC", "MergeBreakFinish", 1);
                                
                                Logger.Info(string.Format("合单中断数据写入成功,分拣订单号[{0}]。", sortNo));                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("合单数据写入失败！原因：{0}！ {1}", ex.Message, "MergeRequestProcess.cs 行号：78！"));
            }
        }
    }
}
