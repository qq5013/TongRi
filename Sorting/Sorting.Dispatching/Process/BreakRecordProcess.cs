using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using MCP;
using DB.Util;
using Sorting.Dispatching.Dal;


namespace Sorting.Dispatching.Process
{
    public class BreakRecordProcess : AbstractProcess
    {
        private Timer tmWorkTimer = new Timer();
        public override void Initialize(Context context)
        {
            try
            {
                base.Initialize(context);

                tmWorkTimer.Interval = 1000;
                tmWorkTimer.Elapsed += new ElapsedEventHandler(tmWorker);
            }
            catch (Exception e)
            {
                Logger.Error("OrderRequestProcess 初始化失败！原因：" + e.Message);
            }

        }
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                

                
            }
            catch (Exception e)
            {
                Logger.Error("故障处理失败！原因：" + e.Message);
            }
        }
        private void tmWorker(object sender, ElapsedEventArgs e)
        {
            try
            {
                tmWorkTimer.Stop();

                Worker();
            }
            finally
            {
                tmWorkTimer.Start();
            }
        }
        private void Worker()
        {
            //return;
            object[] oa = ObjectUtil.GetObjects(WriteToService("SortPLC", "FaultAlarmInfo"));
            if (oa == null)
                return;
            //运行状态
            object[] os = ObjectUtil.GetObjects(WriteToService("SortPLC", "DeviceRunState"));
            if (os == null)
                return;

            
            int State = int.Parse(os[0].ToString());

            //运行状态
            if (State != 1)
                return;

            BatchDal dal = new BatchDal();
            string AlarmNoAll = "''";
            for (int i = 0; i < oa.Length; i++)
            {
                long FaultAlarmInfo = long.Parse(oa[i].ToString());
                if (FaultAlarmInfo <= 0)
                    break;

                string AlarmNo = FaultAlarmInfo.ToString();
                if (AlarmNoAll.IndexOf(AlarmNo) <= 0)
                    AlarmNoAll += ",'" + AlarmNo + "'";

                string ChannelNo = AlarmNo.Substring(AlarmNo.Length - 4, 2);
                string GroupNo = AlarmNo.Substring(AlarmNo.Length - 4, 1);
                string BreakType = (100 + int.Parse(AlarmNo.Substring(0, AlarmNo.Length - 4))).ToString().Substring(1, 2);
                string ChannelName = GroupNo == "0" ? "A" : "B" + ChannelNo;
                dal.InsertBatchDetail(AlarmNo,BreakType, ChannelName);
            }
            dal.UpdateBatchDetail(AlarmNoAll);
        }
    }
}
