using System;
using System.Collections.Generic;
using System.Text;
using MCP;
using DB.Util;
using Sorting.Dispatching.Dao;


namespace Sorting.Dispatching.Process
{
    public class BreakSortingProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                string channelType = "";
                string readItem = "";
                string writeItem = "";

                switch (stateItem.ItemName)
                {
                    case "BreakSorting3":
                        channelType = "3";
                        readItem = "OrderFinish3";
                        writeItem = "BreakSorting31";
                        break;
                    case "BreakSorting2":
                        channelType = "2";
                        readItem = "OrderFinish2";
                        writeItem = "BreakSorting21";
                        break;
                    default:
                        return;
                }
                object o = ObjectUtil.GetObject(stateItem.State);
                if (o == null)
                    return;
                if (o.ToString() == "0")
                    return;

                //读取完成订单序号
                object[] obj = ObjectUtil.GetObjects(WriteToService("SortPLC", readItem));
                //object sortNo = ObjectUtil.GetObject(stateItem.State);
                if (obj == null)
                    return;
                
                string sortNo = obj[0].ToString();
                //校正订单
                if (sortNo != null)
                {
                    //if (sortNo.ToString() != "0")
                    //{
                        using (PersistentManager pm = new PersistentManager())
                        {
                            OrderDao orderDao = new OrderDao();
                            orderDao.UpdateMissOrderStatus(sortNo.ToString(), channelType);
                            //dispatcher.WriteToService("SortPLC", "UpdateMissOrder" + channelGroup, 1);
                            Logger.Info(channelType + " 线 校正定单" + sortNo.ToString() + "成功！");
                            WriteToService("SortPLC", writeItem , 1);
                            Logger.Info(writeItem + "已写入1");
                        }
                    //}
                }
            }
            catch (Exception e)
            {
                Logger.Error("校正定单失败！原因：" + e.Message);
            }
        }
    }
}
