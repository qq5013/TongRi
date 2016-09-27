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

                //��ȡ��ɶ������
                object[] obj = ObjectUtil.GetObjects(WriteToService("SortPLC", readItem));
                //object sortNo = ObjectUtil.GetObject(stateItem.State);
                if (obj == null)
                    return;
                
                string sortNo = obj[0].ToString();
                //У������
                if (sortNo != null)
                {
                    //if (sortNo.ToString() != "0")
                    //{
                        using (PersistentManager pm = new PersistentManager())
                        {
                            OrderDao orderDao = new OrderDao();
                            orderDao.UpdateMissOrderStatus(sortNo.ToString(), channelType);
                            //dispatcher.WriteToService("SortPLC", "UpdateMissOrder" + channelGroup, 1);
                            Logger.Info(channelType + " �� У������" + sortNo.ToString() + "�ɹ���");
                            WriteToService("SortPLC", writeItem , 1);
                            Logger.Info(writeItem + "��д��1");
                        }
                    //}
                }
            }
            catch (Exception e)
            {
                Logger.Error("У������ʧ�ܣ�ԭ��" + e.Message);
            }
        }
    }
}
