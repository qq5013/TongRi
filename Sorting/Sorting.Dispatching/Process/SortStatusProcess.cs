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
    public class SortStatusProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                object o = ObjectUtil.GetObject(stateItem.State);
                if (o != null)
                {
                    string sortStatusTag = o.ToString();

                    using (PersistentManager pm = new PersistentManager())
                    {
                        SortStatusDao sortStatusDao = new SortStatusDao();
                        sortStatusDao.UpdateSortStatus(sortStatusTag);
                        sortStatusDao.InsertEfficiency();
                    }
                }
            }
            catch (Exception e)
            {                
                Logger.Error("分拣设备运行状态信息处理失败！原因：" + e.Message);
            }
        }
    }
}
