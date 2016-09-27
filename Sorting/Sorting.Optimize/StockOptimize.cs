using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sorting.Optimize
{
    public class StockOptimize
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isUseSynchronizeOptimize"></param>
        /// <param name="channelTable">补货烟道表</param>
        /// <param name="orderCTable">通道机品规数量</param>
        /// <param name="orderTTable">立式机品规数量</param>
        /// <param name="orderDate">订单日期</param>
        /// <param name="batchNo">批次</param>
        /// <returns></returns>
        public void Optimize(DataTable channelTable, DataTable orderCTable)
        {
            ////通道机
            //foreach (DataRow row in orderCTable.Rows)
            //{
            //    if (channelTable.Select(String.Format("CIGARETTECODE='{0}'", row["CIGARETTECODE"])).Length == 0)
            //    {
            //        DataRow[] channelRows = channelTable.Select("(CHANNELTYPE = '1' OR CHANNELTYPE ='2')AND LEN(TRIM(CIGARETTECODE)) = 0", "ORDERNO");
            //        if (channelRows.Length != 0)
            //        {
            //            channelRows[0]["CIGARETTECODE"] = row["CIGARETTECODE"];
            //            channelRows[0]["CIGARETTENAME"] = row["CIGARETTENAME"];
            //            channelRows[0]["QUANTITY"] = row["QUANTITY"];                        
            //        }
            //        else
            //            break;
            //    }
            //    else
            //        continue;
            //}

            int channelCount = orderCTable.Rows.Count;
            int productCount = 0;
            if (orderCTable.Rows.Count > 0)
                productCount = int.Parse(orderCTable.Rows[0]["PRODUCTCOUNT"].ToString());
            //通道机
            foreach (DataRow row in orderCTable.Rows)
            {

                DataRow[] channelRows = channelTable.Select("(CHANNELTYPE = '1' OR CHANNELTYPE ='2')AND LEN(TRIM(PRODUCTCODE)) = 0", "ORDERNO");
                if (channelRows.Length != 0)
                {
                    channelRows[0]["PRODUCTCODE"] = row["PRODUCTCODE"];
                    channelRows[0]["PRODUCTNAME"] = row["PRODUCTNAME"];
                    channelRows[0]["QUANTITY"] = row["QUANTITY"];
                    channelRows[0]["TOCHANNELCODE"] = row["CHANNELCODE"];
                }
                else
                    break;


            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isUseSynchronizeOptimize"></param>
        /// <param name="channelTable">补货烟道表</param>
        /// <param name="orderCTable">通道机品规数量</param>
        /// <param name="orderTTable">立式机品规数量</param>
        /// <param name="orderDate">订单日期</param>
        /// <param name="batchNo">批次</param>
        /// <returns></returns>
        public DataTable Optimize(bool isUseSynchronizeOptimize, DataTable channelTable, DataTable orderCTable, DataTable orderTTable, String orderDate, string batchNo)
        {
            //通道机
            foreach (DataRow row in orderCTable.Rows)
            {
                if (channelTable.Select(String.Format("CIGARETTECODE='{0}'", row["CIGARETTECODE"])).Length == 0)
                {
                    DataRow[] channelRows = channelTable.Select("(CHANNELTYPE = '1' OR CHANNELTYPE ='2')AND LEN(TRIM(CIGARETTECODE)) = 0", "ORDERNO");
                    if (channelRows.Length != 0)
                    {
                        channelRows[0]["CIGARETTECODE"] = row["CIGARETTECODE"];
                        channelRows[0]["CIGARETTENAME"] = row["CIGARETTENAME"];
                        if(isUseSynchronizeOptimize)
                            channelRows[0]["QUANTITY"] = row["QUANTITY"];
                        else
                            channelRows[0]["QUANTITY"] = row["TOTAL_CIGARETTE_QUANTITY"];
                    }
                    else if(isUseSynchronizeOptimize)
                        throw new Exception("通道机分拣卷烟品牌数大于件烟缓存道数量，请减少在通道机上进行分拣的卷烟品牌。");
                    else
                        break ;  
                }
                else
                    continue;                

            }

            DataTable mixTable = GetMixTable();
            //立式机
            foreach (DataRow row in orderTTable.Rows)
            {
                if (channelTable.Select(String.Format("CIGARETTECODE='{0}'", row["CIGARETTECODE"])).Length == 0)
                {
                    DataRow[] channelRows = channelTable.Select("CHANNELTYPE = '3'", "QUANTITY ASC");
                    if (channelRows.Length != 0)
                    {
                        mixTable.Rows.Add(new object[] { orderDate, batchNo, channelRows[0]["CHANNELCODE"], row["CIGARETTECODE"], row["CIGARETTENAME"] });

                        channelRows[0]["QUANTITY"] = Convert.ToInt32(channelRows[0]["QUANTITY"]) + Convert.ToInt32(row["QUANTITY"]);
                    }
                }
                else
                    continue;
            }
            
            return mixTable;
        }

        public DataTable GetMixTable()
        {
            DataTable table = new DataTable("MIX");
            table.Columns.Add("ORDERDATE");
            table.Columns.Add("BATCHNO");
            table.Columns.Add("CHANNELCODE");
            table.Columns.Add("CIGARETTECODE");
            table.Columns.Add("CIGARETTENAME");

            return table;
        }
    }
}
