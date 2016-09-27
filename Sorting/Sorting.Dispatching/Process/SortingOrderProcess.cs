using System;
using System.Collections.Generic;
using System.Text;
using MCP;
using Sorting.Dispatching.Dao;
using DB.Util;
using Sorting.Dispatching.Util;

namespace Sorting.Dispatching.Process
{
    class SortingOrderProcess : AbstractProcess
    { 
        [Serializable]
        public class OrderInfo
        {
            public string orderDate = "";
            public string batchNo = "";
        }

        private bool isInit = true;
        private OrderInfo orderInfo = new OrderInfo();

        public override void Initialize(Context context)
        {
            try
            {
                base.Initialize(context);
                isInit = true;
                orderInfo = Util.SerializableUtil.Deserialize<OrderInfo>(true, @".\orderInfo.sl");
            }
            catch (Exception e)
            {
                Logger.Error("DispatchingOrderProcess 初始化失败！原因：" + e.Message);
            }

        }

        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {
                //获取将要分拣的流水号发给补货系统
                string channelGroup = "";
                object o = null;

                switch (stateItem.ItemName)
                {
                    case "OrderInfo":
                        o = stateItem.State;
                        if (o is Array)
                        {
                            Array array = (Array)o;
                            if (array.Length == 2)
                            {
                                string[] orderinfo = new string[2];
                                array.CopyTo(orderinfo, 0);
                                orderInfo.orderDate = orderinfo[0];
                                orderInfo.batchNo = orderinfo[1];
                            }
                        }

                        Util.SerializableUtil.Serialize(true, @".\orderInfo.sl", orderInfo);
                        return;
                        break;
                    case "DispatchingOrderA":
                        channelGroup = "A";
                        break;
                    case "DispatchingOrderB":
                        channelGroup = "B";
                        break;
                    default:
                        return;
                }

                o = ObjectUtil.GetObject(stateItem.State);
                if (o != null)
                {
                    string sortNo = o.ToString();
                    if (Convert.ToInt32(sortNo) > 0)
                    {
                        if (isInit)
                        {
                            isInit = false;
                        }
                        else
                        {
                            //messageUtil.SendToSupply(orderInfo.orderDate, orderInfo.batchNo, sortNo, channelGroup);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("开始分拣订单信息处理失败！原因：" + e.Message);
            }
        }
    }
}
