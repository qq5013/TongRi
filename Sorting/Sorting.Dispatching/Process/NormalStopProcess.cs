using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using MCP;
using DB.Util;
using Sorting.Dispatching.Dal;


namespace Sorting.Dispatching.Process
{
    public class NormalStopProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {

                object o = ObjectUtil.GetObject(stateItem.State);
                
                if (o == null)
                    return;
                string state = o.ToString();

                //如果不是在分拣过程，不记录
                BatchDal batchDal = new BatchDal();
                if (!batchDal.IsExistsNoSorting())
                    return;

                if (state == "0")
                {
                    batchDal.UpdateBatchDetail();
                    batchDal.InsertBatchDetail("992000", "99", "");
                }
                else if (state == "1")
                    batchDal.UpdateBatchDetail();
                else
                    Worker();
            }
            catch (Exception e)
            {
                Logger.Error("故障处理失败！原因：" + e.Message);
            }
        }
        private void Worker()
        {
            //return;
            object[] oa = ObjectUtil.GetObjects(WriteToService("SortPLC", "FaultAlarmInfo"));
            if (oa == null)
                return;

            BatchDal dal = new BatchDal();
            for (int i = 0; i < oa.Length; i++)
            {
                long FaultAlarmInfo = long.Parse(oa[i].ToString());
                if (FaultAlarmInfo <= 0)
                    break;

                string AlarmNo = FaultAlarmInfo.ToString();
                string ChannelNo = AlarmNo.Substring(AlarmNo.Length - 2, 2);
                string GroupNo = AlarmNo.Substring(AlarmNo.Length - 4, 1);
                string BreakType = (100 + int.Parse(AlarmNo.Substring(0, AlarmNo.Length - 4))).ToString().Substring(1, 2);
                string ChannelName = (GroupNo == "0" ? "A" : "B") + ChannelNo;
                dal.InsertBatchDetail(AlarmNo, BreakType, ChannelName);
            }
        }
    }
}
