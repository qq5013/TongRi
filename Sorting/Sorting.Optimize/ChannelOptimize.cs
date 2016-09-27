using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sorting.Optimize
{
    public class ChannelOptimize
    {     
        /// <summary>
        /// orderTable 所有订单数量
        /// </summary>
        /// <param name="orderTable">所有订单数量</param>
        /// <param name="lineOrderTable">指定线的订单数量</param>
        /// <param name="channelTable">烟道表</param>
        /// <param name="deviceTable">生产线烟道类型数量表</param>
        /// <param name="param">参数设定表</param>
        public void Optimize(DataTable orderTable, DataTable lineOrderTable, DataTable channelTable, DataTable deviceTable, Dictionary<string, string> param)
        {
            //允许占用2个通道机的品牌数量
            int ocupyCount = Convert.ToInt32(param["OcupyCount"]);
            int ocupyCount1 = Convert.ToInt32(param["OcupyCount1"]);
            List<string> splitProduct = new List<string>();
            DataTable tmpTable = GenerateTmpTable();

            //查找占用两个通道机的品牌
            if (ocupyCount != 0)
            {
                foreach (DataRow channelRow in channelTable.Rows)
                {
                    string productCode = channelRow["CIGARETTECODE"].ToString();
                    if (productCode != "")
                    {
                        string filter = string.Format("STATUS = '1' AND CHANNELTYPE = '3' AND CIGARETTECODE = '{0}'", productCode);
                        int count = Convert.ToInt32(channelTable.Compute("COUNT(CIGARETTECODE)", filter));

                        if (count >= 2 && !splitProduct.Contains(productCode))
                        {
                            ocupyCount--;
                            splitProduct.Add(productCode);
                        }
                    }
                }
            }

            //固定烟道分配
            foreach (DataRow deviceRow in deviceTable.Rows)
            {
                string channelType = deviceRow["CHANNELTYPE"].ToString();
                SetFixedChannel(lineOrderTable, channelTable, channelType);
            }

            //非固定烟道分配
            foreach (DataRow deviceRow in deviceTable.Rows)
            {
                string channelType = deviceRow["CHANNELTYPE"].ToString();

                switch (channelType)
                {
                    case "2": //立式机 
                        SetTowerChannel(lineOrderTable, channelTable, tmpTable, channelType, ocupyCount1);
                        break;

                    case "3": //通道机
                        SetChannel(orderTable,lineOrderTable,channelTable, tmpTable, channelType, ocupyCount);
                        break;
                }
            }
        }
        /// <summary>
        /// orderTable 所有订单数量
        /// </summary>
        /// <param name="orderTable">所有订单数量</param>
        /// <param name="lineOrderTable">指定线的订单数量</param>
        /// <param name="channelTable">烟道表</param>
        /// <param name="deviceTable">生产线烟道类型数量表</param>
        /// <param name="param">参数设定表</param>
        public void Optimize(DataTable orderTable,DataTable channelTable, DataTable deviceTable, Dictionary<string, string> param)
        {
            //允许占用2个通道机的品牌数量
            int ocupyCount = Convert.ToInt32(param["OcupyCount"]);
            int ocupyCount1 = Convert.ToInt32(param["OcupyCount1"]);

            //当订单量所占百分比大于或等于设定值时
            string[] OrderQtyTPercent1 = param["OrderQtyTPercent1"].Split(',');
            float tp1 = 0;
            int tpQty1 = 1;
            if (OrderQtyTPercent1.Length > 1)
            {
                tp1 = float.Parse(OrderQtyTPercent1[0]);
                tpQty1 = int.Parse(OrderQtyTPercent1[1]);
            }
            //当订单量所占百分比大于或等于设定值时
            string[] OrderQtyTPercent2 = param["OrderQtyTPercent2"].Split(',');
            float tp2 = 0;
            int tpQty2 = 1;
            if (OrderQtyTPercent2.Length > 1)
            {
                tp2 = float.Parse(OrderQtyTPercent2[0]);
                tpQty2 = int.Parse(OrderQtyTPercent2[1]);
            }

            string[] OrderQtyLPercent1 = param["OrderQtyLPercent1"].Split(',');
            float lp1 = 0;
            int lpQty1 = 1;
            if (OrderQtyLPercent1.Length > 1)
            {
                lp1 = float.Parse(OrderQtyLPercent1[0]);
                lpQty1 = int.Parse(OrderQtyLPercent1[1]);
            }
            //
            string[] OrderQtyLPercent2 = param["OrderQtyLPercent2"].Split(',');
            float lp2 = 0;
            int lpQty2 = 1;
            if (OrderQtyLPercent2.Length > 1)
            {
                lp2 = float.Parse(OrderQtyLPercent2[0]);
                lpQty2 = int.Parse(OrderQtyLPercent2[1]);
            }

            List<string> splitProduct = new List<string>();
            DataTable tmpTable = GenerateTmpTable();

            //查找占用两个通道机的品牌
            if (ocupyCount != 0)
            {
                foreach (DataRow channelRow in channelTable.Rows)
                {
                    string productCode = channelRow["productCode"].ToString();
                    if (productCode != "")
                    {
                        string filter = string.Format("STATUS = '1' AND CHANNELTYPE = '3' AND ProductCode = '{0}'", productCode);
                        int count = Convert.ToInt32(channelTable.Compute("COUNT(productCode)", filter));

                        if (count >= 2 && !splitProduct.Contains(productCode))
                        {
                            ocupyCount--;
                            splitProduct.Add(productCode);
                        }
                    }
                }
            }

            //固定烟道分配
            foreach (DataRow deviceRow in deviceTable.Rows)
            {
                string channelType = deviceRow["CHANNELTYPE"].ToString();
                SetFixedChannel(orderTable, channelTable, channelType);
            }

            //非固定烟道分配
            foreach (DataRow deviceRow in deviceTable.Rows)
            {
                string channelType = deviceRow["CHANNELTYPE"].ToString();

                switch (channelType)
                {
                    case "2": //立式机 
                        //SetTowerChannel(orderTable, channelTable, tmpTable, channelType, ocupyCount1);
                        SetTowerChannel1(orderTable, channelTable, tmpTable, channelType, lp1, lpQty1, lp2, lpQty2);
                        break;

                    case "3": //通道机
                        //SetChannel(orderTable, orderTable, channelTable, tmpTable, channelType, ocupyCount);
                        SetChannel1(orderTable, channelTable, tmpTable, channelType, tp1, tpQty1, tp2, tpQty2);
                        break;
                }
            }
        }
        /// <summary>
        /// 固定烟道分拣
        /// </summary>
        /// <param name="orderTable"></param>
        /// <param name="channelTable"></param>
        /// <param name="channelType"></param>
        private void SetFixedChannel(DataTable orderTable, DataTable channelTable, string channelType)
        {
            //固定烟道品牌
            foreach (DataRow orderRow in orderTable.Rows)
            {
                //取当前品牌固定烟道
                DataRow[] channelRows = channelTable.Select(string.Format("CHANNELTYPE = '{0}' AND ProductCode = '{1}'  AND STATUS='1'", channelType, orderRow["ProductCode"]), "CHANNELORDER");

                if (channelRows.Length > 1)//占用多少烟道
                {
                    foreach (DataRow channelRow in channelRows)
                    {
                        channelRow["ProductCode"] = orderRow["ProductCode"];
                        channelRow["ProductName"] = orderRow["ProductName"];
                        //++q                       
                        channelRow["GroupTotal"] = orderRow["TOTAL_PRODUCT_QUANTITY"];
                        //++q
                    }
                    orderRow["TOTAL_PRODUCT_QUANTITY"] = 0;
                }
                else if (channelRows.Length == 1) //只占用一个烟道
                {
                    channelRows[0]["ProductCode"] = orderRow["ProductCode"];
                    channelRows[0]["ProductName"] = orderRow["ProductName"];
                    channelRows[0]["GroupTotal"] = orderRow["TOTAL_PRODUCT_QUANTITY"];//++q
                    orderRow["TOTAL_PRODUCT_QUANTITY"] = 0;
                }
            }
        }

        /// <summary>
        /// 分配通道机非固定烟道
        /// </summary>
        /// <param name="orderTable"></param>
        /// <param name="channelTable"></param>
        /// <param name="tmpTable"></param>
        /// <param name="channelType"></param>
        /// <param name="sortSplitProduct"></param>
        private void SetChannel(DataTable orderTable, DataTable lineOrderTable, DataTable channelTable, DataTable tmpTable, string channelType, int sortSplitProduct)
        {

            //非固定通道机品牌
            DataRow[] orderRows = orderTable.Select("QUANTITY > 0", "QUANTITY DESC");
            foreach (DataRow orderRow in orderRows)
            {
                //取未被占用通道机烟道
                int count = Convert.ToInt32(channelTable.Compute("COUNT(ProductCode)", string.Format("STATUS = '1' AND ProductCode='{0}'", orderRow["ProductCode"])));
                DataRow[] channelRows = channelTable.Select(string.Format("CHANNELTYPE = '{0}' AND ProductCode=''  AND STATUS='1'", channelType), "CHANNELORDER");

                if (count == 0)
                {
                    if (channelRows.Length != 0)
                    {
                        if (sortSplitProduct-- > 0 && channelRows.Length >= 2)
                        {
                            channelRows[0]["ProductCode"] = orderRow["ProductCode"];
                            channelRows[0]["ProductName"] = orderRow["ProductName"];

                            channelRows[1]["ProductCode"] = orderRow["ProductCode"];
                            channelRows[1]["ProductName"] = orderRow["ProductName"];

                            //++q       
                            object objQuantity = lineOrderTable.Compute("SUM(QUANTITY)", string.Format("ProductCode='{0}'", orderRow["ProductCode"]));
                            int lineOrderQuantity = Convert.ToInt32(Convert.IsDBNull(objQuantity) ? 0 : objQuantity);
                            channelRows[0]["GroupTotal"] = lineOrderQuantity;
                            channelRows[1]["GroupTotal"] = lineOrderQuantity;
                            //++q
                        }
                        else
                        {
                            DataRow[] tmpRows = tmpTable.Select("", "QUANTITY");
                            foreach (DataRow tmpRow in tmpRows)
                            {
                                //在当前组里查找是否还有烟道，如果没有则在另外一组里进行查询
                                DataRow[] rows = channelTable.Select(string.Format("ProductCode='' AND CHANNELTYPE='{0}'  AND STATUS='1' AND CHANNELGROUP = {1}", channelType, tmpRow["GROUPNO"]), "CHANNELORDER");
                                if (rows.Length != 0)
                                {
                                    rows[0]["ProductCode"] = orderRow["ProductCode"];
                                    rows[0]["ProductName"] = orderRow["ProductName"];
                                    //++q
                                    object objQuantity = lineOrderTable.Compute("SUM(QUANTITY)", string.Format("ProductCode='{0}'", orderRow["ProductCode"]));
                                    int lineOrderQuantity = Convert.ToInt32(Convert.IsDBNull(objQuantity) ? 0 : objQuantity);
                                    rows[0]["GroupTotal"] = lineOrderQuantity;
                                    //++q
                                    tmpRow["QUANTITY"] = Convert.ToInt32(tmpRow["QUANTITY"]) + Convert.ToInt32(orderRow["QUANTITY"]);
                                    break;
                                }
                            }
                        }

                        orderRow["QUANTITY"] = 0;
                    }
                    else
                        break;
                }
            }
        }
        /// <summary>
        /// 分配通道机非固定烟道
        /// </summary>
        /// <param name="orderTable"></param>
        /// <param name="channelTable"></param>
        /// <param name="tmpTable"></param>
        /// <param name="channelType"></param>
        /// <param name="sortSplitProduct"></param>
        private void SetChannel1(DataTable orderTable, DataTable channelTable, DataTable tmpTable, string channelType,float tp1,int tpQty1,float tp2,int tpQty2)
        {

            //非固定通道机品牌
            DataRow[] orderRows = orderTable.Select("TOTAL_PRODUCT_QUANTITY > 0", "TOTAL_PRODUCT_QUANTITY DESC,PRODUCTCODE");
            foreach (DataRow orderRow in orderRows)
            {
                int tpQty = 1;
                float orderPercent = float.Parse(orderRow["PRODUCT_PERCENT"].ToString());
                if (orderPercent >= tp1)
                    tpQty = tpQty1;
                else if (orderPercent < tp1 && orderPercent >= tp2)
                    tpQty = tpQty2;


                //取未被占用通道机烟道
                int count = Convert.ToInt32(channelTable.Compute("COUNT(PRODUCTCODE)", string.Format("STATUS = '1' AND PRODUCTCODE='{0}'", orderRow["PRODUCTCODE"])));
                DataRow[] channelRows = channelTable.Select(string.Format("CHANNELTYPE = '{0}' AND PRODUCTCODE=''  AND STATUS='1'", channelType), "CHANNELORDER");

                //通道机预留一个备用
                if (channelRows.Length <= 1)
                    break;
                if(tpQty>channelRows.Length)
                    tpQty = channelRows.Length;


                if (count == 0)
                {
                    if (channelRows.Length != 0)
                    {
                        for(int i=0;i<tpQty;i++)
                        {
                            channelRows[i]["PRODUCTCODE"] = orderRow["PRODUCTCODE"];
                            channelRows[i]["PRODUCTNAME"] = orderRow["PRODUCTNAME"];
                            channelRows[i]["GroupTotal"] = orderRow["TOTAL_PRODUCT_QUANTITY"];                            
                        }

                        orderRow["TOTAL_PRODUCT_QUANTITY"] = 0;
                    }
                    else
                        break;
                }
            }
        }
        /// <summary>
        /// 分配立式机非固定烟道
        /// </summary>
        /// <param name="orderTable"></param>
        /// <param name="channelTable"></param>
        /// <param name="tmpTable"></param>
        /// <param name="channelType"></param>
        private void SetTowerChannel(DataTable lineOrderTable, DataTable channelTable, DataTable tmpTable, string channelType, int sortSplitProduct)
        {
            DataRow[] orderRows = lineOrderTable.Select("QUANTITY > 0", "QUANTITY DESC");            

            foreach (DataRow orderRow in orderRows)
            {
                int count = Convert.ToInt32(channelTable.Compute("COUNT(ProductCode)", string.Format("STATUS = '1' AND ProductCode='{0}'", orderRow["ProductCode"])));
                if (count == 0)
                {
                    //如果有未被占用的立式机
                    if ((int)channelTable.Compute("COUNT(ProductCode)", string.Format("ProductCode='' AND CHANNELTYPE='{0}' AND STATUS='1'", channelType)) != 0)
                    {
                        if (sortSplitProduct-- > 0)
                        {
                            DataRow[] channelRows = channelTable.Select(string.Format("ProductCode='' AND CHANNELTYPE='{0}'  AND STATUS='1' AND CHANNELGROUP = {1}", channelType, 1), "CHANNELORDER");
                            if (channelRows.Length != 0)
                            {
                                channelRows[0]["ProductCode"] = orderRow["ProductCode"];
                                channelRows[0]["ProductName"] = orderRow["ProductName"];
                                channelRows[0]["GroupTotal"] = orderRow["QUANTITY"];//++q                                
                            }

                            channelRows = channelTable.Select(string.Format("CIGARETTECODE='' AND CHANNELTYPE='{0}'  AND STATUS='1' AND CHANNELGROUP = {1}", channelType, 2), "CHANNELORDER");
                            if (channelRows.Length != 0)
                            {
                                channelRows[0]["ProductCode"] = orderRow["ProductCode"];
                                channelRows[0]["ProductName"] = orderRow["ProductName"];
                                channelRows[0]["GroupTotal"] = orderRow["QUANTITY"];//++q                                
                            }

                            orderRow["QUANTITY"] = 0;
                        }
                        else
                        {
                            DataRow[] tmpRows = tmpTable.Select("", "QUANTITY");
                            foreach (DataRow tmpRow in tmpRows)
                            {
                                //在当前组里查找是否还有烟道，如果没有则在另外一组里进行查询
                                DataRow[] channelRows = channelTable.Select(string.Format("CIGARETTECODE='' AND CHANNELTYPE='{0}'  AND STATUS='1' AND CHANNELGROUP = {1}", channelType, tmpRow["GROUPNO"]), "CHANNELORDER");
                                if (channelRows.Length != 0)
                                {
                                    channelRows[0]["ProductCode"] = orderRow["ProductCode"];
                                    channelRows[0]["ProductName"] = orderRow["ProductName"];
                                    channelRows[0]["GroupTotal"] = orderRow["QUANTITY"];//++q

                                    tmpRow["QUANTITY"] = Convert.ToInt32(tmpRow["QUANTITY"]) + Convert.ToInt32(orderRow["QUANTITY"]);
                                    orderRow["QUANTITY"] = 0;
                                    break;
                                }
                            }
                        }
                    }
                    else if (0 == Convert.ToInt32(channelTable.Compute("COUNT(CHANNELCODE)", "STATUS = '1' AND CHANNELTYPE='5'")))
                    {
                        throw new Exception("还有卷烟品牌未分配烟道,请调整烟道设置。");
                    }
                }
            }
        }
        /// <summary>
        /// 分配立式机非固定烟道
        /// </summary>
        /// <param name="orderTable"></param>
        /// <param name="channelTable"></param>
        /// <param name="tmpTable"></param>
        /// <param name="channelType"></param>
        private void SetTowerChannel1(DataTable lineOrderTable, DataTable channelTable, DataTable tmpTable, string channelType, float lp1, int lpQty1, float lp2, int lpQty2)
        {
            DataRow[] orderRows = lineOrderTable.Select("TOTAL_PRODUCT_QUANTITY > 0", "TOTAL_PRODUCT_QUANTITY DESC,PRODUCTCODE");

            foreach (DataRow orderRow in orderRows)
            {
                int lpQty = 1;
                float orderPercent = float.Parse(orderRow["PRODUCT_PERCENT"].ToString());
                if (orderPercent >= lp1)
                    lpQty = lpQty1;
                else if (orderPercent < lp1 && orderPercent >= lp2)
                    lpQty = lpQty2;

                int count = Convert.ToInt32(channelTable.Compute("COUNT(PRODUCTCODE)", string.Format("STATUS = '1' AND PRODUCTCODE='{0}'", orderRow["PRODUCTCODE"])));
                DataRow[] channelRows = channelTable.Select(string.Format("CHANNELTYPE = '{0}' AND PRODUCTCODE=''  AND STATUS='1'", channelType), "CHANNELORDER");

                if (lpQty > channelRows.Length)
                    lpQty = channelRows.Length;
                if (count == 0)
                {
                    for (int i = 0; i < lpQty; i++)
                    {
                        channelRows[i]["PRODUCTCODE"] = orderRow["PRODUCTCODE"];
                        channelRows[i]["PRODUCTNAME"] = orderRow["PRODUCTNAME"];
                        channelRows[i]["GroupTotal"] = orderRow["TOTAL_PRODUCT_QUANTITY"];
                    }

                    orderRow["TOTAL_PRODUCT_QUANTITY"] = 0;


                    if (lpQty == 0)
                    {
                        throw new Exception("还有产品未分配仓位,请调整货仓设置。");
                    }
                }
            }
        }
        private DataTable GenerateTmpTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("GroupTotal");
            table.Columns.Add("QUANTITY", typeof(Int32));
            for (int i = 1; i <= 2; i++)
            {
                table.Rows.Add(i, 0);
            }
            return table;
        }
    }
}
