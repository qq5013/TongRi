using System;
using System.Collections.Generic;
using System.Text;
using MCP;
using DB.Util;
using Sorting.Dispatching.Dal;


namespace Sorting.Dispatching.Process
{
    public class BreakRecordProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                string BreakType = stateItem.ItemName.Substring(12,2);

                object o = ObjectUtil.GetObject(stateItem.State);
                if (o == null)
                    return;

                string Flag = o.ToString();

                //��ȡ��ɶ������
                object[] obj = ObjectUtil.GetObjects(WriteToService("SortPLC", "SortingFlag"));                
                if (obj == null)
                    return;
                string SortFlag = obj[0].ToString();
                if (SortFlag == "0")
                    return;

                BatchDal dal = new BatchDal();
                dal.InsertBatchDetail(BreakType, Flag);
            }
            catch (Exception e)
            {
                Logger.Error("���ϴ���ʧ�ܣ�ԭ��" + e.Message);
            }
        }
    }
}
