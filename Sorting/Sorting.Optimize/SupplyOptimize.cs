using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sorting.Optimize
{
    public class SupplyOptimize
    {
        public DataTable Optimize(DataTable channelTable)
        {
            DataTable supplyTable = GetEmptySupply();

            int serialNo = 1;

            foreach (DataRow row in channelTable.Rows)
            {
                //int quantity = Convert.ToInt32(row["QUANTITY"]) / 50;
                int supplyQuantity1 = Convert.ToInt32(row["SUPPLYQUANTITY"])/50;
                int supplyQuantity2 = Convert.ToInt32(row["PIECE"]);

                if (supplyQuantity1 >= 1 && row["CHANNELTYPE"].ToString() != "5")
                {
                    int count = 1;
                    if (row["CHANNELTYPE"].ToString() == "3")
                    {
                        count = supplyQuantity1 - (supplyQuantity2 > 0?supplyQuantity2:0) ;
                    }

                    for (int i = 0; i < count; i++)
                    {
                        DataRow supplyRow = supplyTable.NewRow();
                        supplyRow["ORDERDATE"] = row["ORDERDATE"];
                        supplyRow["BATCHNO"] = row["BATCHNO"];
                        supplyRow["LINECODE"] = row["LINECODE"];
                        supplyRow["SERIALNO"] = serialNo++;

                        supplyRow["SORTNO"] = 0;

                        supplyRow["CIGARETTECODE"] = row["CIGARETTECODE"];
                        supplyRow["CIGARETTENAME"] = row["CIGARETTENAME"];

                        supplyRow["CHANNELCODE"] = row["CHANNELCODE"];
                        supplyRow["CHANNELGROUP"] = row["CHANNELGROUP"]; 
                        supplyRow["GROUPNO"] = row["GROUPNO"];
                        supplyRow["BATCH"] = i;

                        supplyTable.Rows.Add(supplyRow);
                    }
                }
            }
            return supplyTable;
        }

        public DataTable Optimize(DataTable channelTable, DataRow[] orderRows,ref int serialNo)
        {
            DataTable supplyTable = GetEmptySupply();
            DataRow channelRow = channelTable.Rows[0];
            foreach (DataRow row in orderRows)
            {
                int quantity = Convert.ToInt32(row["QUANTITY"]) / 50;

                if (quantity > 0)
                {
                    for (int i = 0; i < quantity; i++)
                    {
                        DataRow supplyRow = supplyTable.NewRow();
                        supplyRow["ORDERDATE"] = row["ORDERDATE"];
                        supplyRow["BATCHNO"] = channelRow["BATCHNO"];
                        supplyRow["LINECODE"] = channelRow["LINECODE"];

                        supplyRow["SERIALNO"] = serialNo;
                        supplyRow["SORTNO"] = serialNo;

                        supplyRow["CIGARETTECODE"] = row["CIGARETTECODE"];
                        supplyRow["CIGARETTENAME"] = row["CIGARETTENAME"];

                        supplyRow["CHANNELCODE"] = channelRow["CHANNELCODE"];
                        supplyRow["CHANNELGROUP"] = channelRow["CHANNELGROUP"];
                        supplyRow["GROUPNO"] = channelRow["GROUPNO"];
                        supplyRow["BATCH"] = 1;

                        supplyTable.Rows.Add(supplyRow);
                        serialNo++;
                    }                    
                }
            }
            return supplyTable;
        }
        public DataTable Optimize(DataTable channelTable, DataRow[] orderRows)
        {
            DataTable supplyTable = GetEmptySupply();
            //DataRow channelRow = channelTable.Rows[0];
            Dictionary<string, int> dicChannel = new Dictionary<string, int>();
            
            int serialNo = 1;
            foreach (DataRow row in orderRows)
            {
                string channelCode = row["CHANNELCODE"].ToString();
                //此分拣烟道已经分配了多少件烟
                DataRow[] rows = supplyTable.Select(string.Format("CHANNELCODE='{0}'", channelCode));

                int quantity = Convert.ToInt32(row["QUANTITY"]);
                
                if (!dicChannel.ContainsKey(channelCode))
                    dicChannel.Add(channelCode, quantity);
                else
                    dicChannel[channelCode] += quantity;
                //总需要件数
                int boxTotal = dicChannel[channelCode] / 50;
                //+ 尾数整件
                boxTotal +=  + dicChannel[channelCode] % 50 > 0 ? 1 : 0;
                int box = boxTotal - rows.Length;

                for (int i = 0; i < box; i++)
                {
                    DataRow supplyRow = supplyTable.NewRow();
                    supplyRow["ORDERDATE"] = row["ORDERDATE"];
                    supplyRow["BATCHNO"] = row["BATCHNO"];
                    supplyRow["LINECODE"] = row["LINECODE"];

                    supplyRow["SERIALNO"] = serialNo;
                    supplyRow["SORTNO"] = row["SORTNO"];

                    supplyRow["CIGARETTECODE"] = row["CIGARETTECODE"];
                    supplyRow["CIGARETTENAME"] = row["CIGARETTENAME"];

                    supplyRow["CHANNELCODE"] = row["CHANNELCODE"];
                    supplyRow["CHANNELGROUP"] = row["CHANNELGROUP"];
                    //supplyRow["GROUPNO"] = row["GROUPNO"];
                    supplyRow["BATCH"] = 1;
                    supplyRow["BARCODE"] = row["BARCODEPACK"];

                    supplyTable.Rows.Add(supplyRow);
                    serialNo++;
                }

            }
            return supplyTable;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelTable"></param>
        /// <param name="orderRows"></param>
        /// <param name="haveBalance">是否出整件补尾数</param>
        /// <returns></returns>
        public DataTable Optimize1(DataTable channelTable, DataRow[] orderRows,bool haveBalance)
        {
            DataTable supplyTable = GetEmptySupply();
            //DataRow channelRow = channelTable.Rows[0];
            Dictionary<string, int> dicChannel = new Dictionary<string, int>();

            int serialNo = 0;
            if (channelTable.Rows.Count > 0)
                serialNo = int.Parse(channelTable.Rows[0]["serialNo"].ToString());
            foreach (DataRow row in orderRows)
            {
                string str = row["CIGARETTECODE"].ToString();

                //string sddd="1";
                //if (str == "0107")
                //    sddd = "2";
                string channelCode = row["CHANNELCODE"].ToString();
                string filter = string.Format("CHANNELCODE='{0}'",channelCode);
                int channelBoxes = 0;
                DataRow[] dr = channelTable.Select(filter);
                if(dr.Length>0)
                    channelBoxes = int.Parse(dr[0]["BOXES"].ToString());
                //此分拣烟道已经分配了多少件烟
                DataRow[] rows = supplyTable.Select(string.Format("CHANNELCODE='{0}'", channelCode));

                //如果补货不补零条的话的处理
                if (!haveBalance)
                {
                    if (channelBoxes == rows.Length)
                        continue;
                }
                int quantity = Convert.ToInt32(row["QUANTITY"]);

                if (!dicChannel.ContainsKey(channelCode))
                    dicChannel.Add(channelCode, quantity);
                else
                    dicChannel[channelCode] += quantity;
                //总需要件数
                int boxTotal = dicChannel[channelCode] / 50;
                //+ 尾数整件
                boxTotal += +dicChannel[channelCode] % 50 > 0 ? 1 : 0;
                int box = boxTotal - rows.Length;

                for (int i = 0; i < box; i++)
                {
                    serialNo++;
                    DataRow supplyRow = supplyTable.NewRow();
                    supplyRow["ORDERDATE"] = row["ORDERDATE"];
                    supplyRow["BATCHNO"] = row["BATCHNO"];
                    supplyRow["LINECODE"] = row["LINECODE"];

                    supplyRow["SERIALNO"] = serialNo;
                    supplyRow["SORTNO"] = row["SORTNO"];

                    supplyRow["CIGARETTECODE"] = row["CIGARETTECODE"];
                    supplyRow["CIGARETTENAME"] = row["CIGARETTENAME"];

                    supplyRow["CHANNELCODE"] = row["CHANNELCODE"];
                    supplyRow["CHANNELGROUP"] = row["CHANNELGROUP"];
                    //supplyRow["GROUPNO"] = row["GROUPNO"];
                    supplyRow["BATCH"] = 1;
                    supplyRow["BARCODE"] = row["BARCODEPACK"];
                    supplyRow["ISBALANCE"] = 0;
                    supplyTable.Rows.Add(supplyRow);
                    
                }

            }
            return supplyTable;
        }
        /// <summary>
        /// 通道机补货优化
        /// </summary>
        /// <param name="channelTable"></param>
        /// <param name="orderRows"></param>
        /// <returns></returns>
        public DataTable Optimize2(DataTable channelTable, DataRow[] orderRows)
        {
            DataTable supplyTable = GetEmptySupply();
            //DataRow channelRow = channelTable.Rows[0];
            Dictionary<string, int> dicChannel = new Dictionary<string, int>();
            Dictionary<string, int> dicBoxes = new Dictionary<string, int>();
            Dictionary<string, int> dicPacketQty = new Dictionary<string, int>();
            foreach (DataRow row in channelTable.Rows)
            {
                //dicChannel.Add(row["CHANNELCODE"].ToString(), -int.Parse(row["PBALANCE"].ToString()));
                dicChannel.Add(row["CHANNELCODE"].ToString(), 0);
                dicBoxes.Add(row["CHANNELCODE"].ToString(), int.Parse(row["BOXES"].ToString()));
                dicPacketQty.Add(row["CHANNELCODE"].ToString(), int.Parse(row["PACKAGENUM"].ToString()));
            }

            int serialNo = 1;
            if (channelTable.Rows.Count > 0)
                serialNo = int.Parse(channelTable.Rows[0]["serialNo"].ToString()) + 1;

            foreach (DataRow row in orderRows)
            {
                string channelCode = row["CHANNELCODE"].ToString();
                //此分拣烟道已经分配了多少件烟
                DataRow[] rows = supplyTable.Select(string.Format("CHANNELCODE='{0}'", channelCode));
                if (rows.Length >= dicBoxes[channelCode])
                    continue;

                int quantity = Convert.ToInt32(row["QUANTITY"]);

                if (!dicChannel.ContainsKey(channelCode))
                    dicChannel.Add(channelCode, quantity);
                else
                    dicChannel[channelCode] += quantity;

                //判断需要补烟数量
                if (dicChannel[channelCode] <= 0)
                    continue;
                //总需要件数
                int boxTotal = dicChannel[channelCode] / dicPacketQty[channelCode];
                //+ 尾数整件
                boxTotal += dicChannel[channelCode] % dicPacketQty[channelCode] > 0 ? 1 : 0;
                int box = boxTotal - rows.Length;

                for (int i = 0; i < box; i++)
                {
                    DataRow supplyRow = supplyTable.NewRow();
                    supplyRow["ORDERDATE"] = row["ORDERDATE"];
                    supplyRow["BATCHNO"] = row["BATCHNO"];
                    supplyRow["LINECODE"] = row["LINECODE"];

                    supplyRow["SERIALNO"] = serialNo;
                    supplyRow["SORTNO"] = row["SORTNO"];

                    supplyRow["CIGARETTECODE"] = row["CIGARETTECODE"];
                    supplyRow["CIGARETTENAME"] = row["CIGARETTENAME"];

                    supplyRow["CHANNELCODE"] = row["CHANNELCODE"];
                    supplyRow["CHANNELGROUP"] = row["CHANNELGROUP"];
                    //supplyRow["GROUPNO"] = row["GROUPNO"];
                    supplyRow["BATCH"] = 1;
                    supplyRow["BARCODE"] = row["BARCODEPACK"];
                    supplyRow["ISBALANCE"] = 0;

                    supplyTable.Rows.Add(supplyRow);
                    serialNo++;
                }

            }
            return supplyTable;
        }
        // <summary>
        /// 
        /// </summary>
        /// <param name="channelTable"></param>
        /// <param name="orderRows"></param>
        /// <param name="haveBalance">是否出整件补尾数</param>
        /// <returns></returns>
        public DataTable Optimize3(DataTable channelTable,string batchNo,int bNo)
        {
            DataTable supplyTable = GetEmptySupply();
            Dictionary<string, int> dicChannel = new Dictionary<string, int>();

            int serialNo = 0;
            
            if(channelTable.Rows.Count>0)
                serialNo = int.Parse(channelTable.Rows[0]["serialNo"].ToString());

            foreach (DataRow row in channelTable.Rows)
            {
                string pre = "";
                if (bNo - 1 > 0)
                    pre = (bNo - 1).ToString();

                string cigarettecode = row["CIGARETTECODE"].ToString();
                if (cigarettecode.Trim().Length <= 0)
                    continue;
                int packetQty = int.Parse(row["PACKAGENUM"].ToString());
                int margin = int.Parse(row["QUANTITY" + pre].ToString());
                int balance = int.Parse(row["QUANTITY" + bNo.ToString()].ToString());

                if (balance > margin)
                {
                    serialNo++;
                    //需要补一件烟
                    DataRow supplyRow = supplyTable.NewRow();
                    supplyRow["ORDERDATE"] = row["ORDERDATE"];
                    supplyRow["BATCHNO"] = batchNo;
                    supplyRow["LINECODE"] = row["LINECODE"];

                    supplyRow["SERIALNO"] = serialNo;
                    supplyRow["SORTNO"] = row["SORTNO"]; ;

                    supplyRow["CIGARETTECODE"] = row["CIGARETTECODE"];
                    supplyRow["CIGARETTENAME"] = row["CIGARETTENAME"];

                    supplyRow["CHANNELCODE"] = row["CHANNELCODE"];
                    supplyRow["CHANNELGROUP"] = 1;
                    //supplyRow["GROUPNO"] = row["GROUPNO"];
                    supplyRow["BATCH"] = 1;
                    supplyRow["BARCODE"] = row["BARCODEPACK"];
                    supplyRow["ISBALANCE"] = 1;
                    supplyTable.Rows.Add(supplyRow);
                    //计算余量
                    //row["QUANTITY" + bNo.ToString()] = 50 + margin - balance;
                    row["QUANTITY" + bNo.ToString()] = packetQty + margin - balance;
                }             
                else
                    //计算余量
                    row["QUANTITY" + bNo.ToString()] = margin - balance;
            }
            return supplyTable;
        }
        /// <summary>
        /// 立式机补货优化
        /// </summary>
        /// <param name="channelTable"></param>
        /// <param name="orderRows"></param>
        /// <returns></returns>
        public DataTable Optimize4(DataTable channelTable, DataRow[] orderRows)
        {
            DataTable supplyTable = GetEmptySupply();
            //DataRow channelRow = channelTable.Rows[0];
            Dictionary<string, int> dicCigarette = new Dictionary<string, int>();
            Dictionary<string, int> dicBoxes = new Dictionary<string, int>();
            Dictionary<string, int> dicPacketQty = new Dictionary<string, int>();

            foreach (DataRow row in channelTable.Rows)
            {
                //if(!dicCigarette.ContainsKey(row["CIGARETTECODE"].ToString()))
                //    dicCigarette.Add(row["CIGARETTECODE"].ToString(), -int.Parse(row["PBALANCE"].ToString()));
                //else
                //    dicCigarette[row["CIGARETTECODE"].ToString()] -= int.Parse(row["PBALANCE"].ToString());

                if (!dicCigarette.ContainsKey(row["CIGARETTECODE"].ToString()))
                    dicCigarette.Add(row["CIGARETTECODE"].ToString(), 0);

                if (!dicBoxes.ContainsKey(row["CIGARETTECODE"].ToString()))
                    dicBoxes.Add(row["CIGARETTECODE"].ToString(), int.Parse(row["BOXES"].ToString()));

                if (!dicPacketQty.ContainsKey(row["CIGARETTECODE"].ToString()))
                    dicPacketQty.Add(row["CIGARETTECODE"].ToString(), int.Parse(row["PACKAGENUM"].ToString()));
            }

            int serialNo = 1;
            int ii = 0;
            foreach (DataRow row in orderRows)
            {
                ii++;
                string cigaretteCode = row["CIGARETTECODE"].ToString();
                //此品规已经分配了多少件烟
                DataRow[] rows = supplyTable.Select(string.Format("CIGARETTECODE='{0}'", cigaretteCode));

                if (rows.Length >= dicBoxes[cigaretteCode] && rows.Length>0)
                    continue;

                string s;
                if(cigaretteCode=="0304")
                    s = "1";
                int quantity = Convert.ToInt32(row["QUANTITY"]);

                if (!dicCigarette.ContainsKey(cigaretteCode))
                    dicCigarette.Add(cigaretteCode, quantity);
                else
                    dicCigarette[cigaretteCode] += quantity;

                //判断需要补烟数量
                if (dicCigarette[cigaretteCode] <= 0)
                    continue;
                //总需要件数
                int boxTotal = dicCigarette[cigaretteCode] / dicPacketQty[cigaretteCode];
                if (cigaretteCode == "0304")
                {
                    //System.Diagnostics.Debug.Print(row["SORTNO"].ToString() + "_" + quantity.ToString() + "_" + dicCigarette[cigaretteCode].ToString());

                }
                System.Diagnostics.Debug.Print(row["SORTNO"].ToString() + "_" + cigaretteCode + "_" + quantity.ToString() + "_" + dicCigarette[cigaretteCode].ToString());
                //+ 尾数整件
                //boxTotal += dicCigarette[cigaretteCode] % dicPacketQty[cigaretteCode] > 0 ? 1 : 0;
                int box = boxTotal - rows.Length;

                for (int i = 0; i < box; i++)
                {
                    if (cigaretteCode == "0304")
                        s = "1";
                    DataRow supplyRow = supplyTable.NewRow();
                    supplyRow["ORDERDATE"] = row["ORDERDATE"];
                    supplyRow["BATCHNO"] = row["BATCHNO"];
                    supplyRow["LINECODE"] = row["LINECODE"];

                    supplyRow["SERIALNO"] = serialNo;
                    supplyRow["SORTNO"] = row["SORTNO"];

                    supplyRow["CIGARETTECODE"] = row["CIGARETTECODE"];
                    supplyRow["CIGARETTENAME"] = row["CIGARETTENAME"];

                    supplyRow["CHANNELCODE"] = row["CHANNELCODE"];
                    supplyRow["CHANNELGROUP"] = row["CHANNELGROUP"];
                    //supplyRow["GROUPNO"] = row["GROUPNO"];
                    supplyRow["BATCH"] = 1;
                    supplyRow["BARCODE"] = row["BARCODEPACK"];
                    supplyRow["ISBALANCE"] = 0;

                    supplyTable.Rows.Add(supplyRow);
                    serialNo++;
                }

            }
            return supplyTable;
        }
        /// <summary>
        /// 通道机补货优化
        /// </summary>
        /// <param name="channelTable"></param>
        /// <param name="orderRows"></param>
        /// <returns></returns>
        public DataTable Optimize5(DataTable channelTable)
        {
            DataTable supplyTable = GetEmptySupply();
            //DataRow channelRow = channelTable.Rows[0];
            Dictionary<string, int> dicChannel = new Dictionary<string, int>();
            Dictionary<string, int> dicPacketQty = new Dictionary<string, int>();
            foreach (DataRow row in channelTable.Rows)
            {
                dicChannel.Add(row["CHANNELCODE"].ToString(), int.Parse(row["BOXES"].ToString()));
                dicPacketQty.Add(row["CHANNELCODE"].ToString(), int.Parse(row["PACKAGENUM"].ToString()));
            }

            int serialNo = 1;

            for (int i = 0; i < 1000; i++)
            {
                foreach (DataRow row in channelTable.Rows)
                {
                    string channelCode = row["CHANNELCODE"].ToString();
                    if (dicChannel[channelCode] > 0)
                    {
                        DataRow supplyRow = supplyTable.NewRow();
                        supplyRow["ORDERDATE"] = row["ORDERDATE"];
                        supplyRow["BATCHNO"] = row["BATCHNO"];
                        supplyRow["LINECODE"] = row["LINECODE"];

                        supplyRow["SERIALNO"] = serialNo;
                        supplyRow["SORTNO"] = serialNo;

                        supplyRow["CIGARETTECODE"] = row["CIGARETTECODE"];
                        supplyRow["CIGARETTENAME"] = row["CIGARETTENAME"];

                        supplyRow["CHANNELCODE"] = row["CHANNELCODE"];
                        supplyRow["CHANNELGROUP"] = row["CHANNELGROUP"];
                        //supplyRow["GROUPNO"] = row["GROUPNO"];
                        supplyRow["BATCH"] = 1;
                        supplyRow["BARCODE"] = row["BARCODEPACK"];
                        supplyRow["ISBALANCE"] = 0;

                        supplyTable.Rows.Add(supplyRow);
                        serialNo++;
                        dicChannel[channelCode]--;
                    }
                    else
                        continue;
                }
            }
            return supplyTable;
        }
        // <summary>
        /// 
        /// </summary>
        /// <param name="channelTable"></param>
        /// <param name="orderRows"></param>
        /// <param name="haveBalance">是否出整件补尾数</param>
        /// <returns></returns>
        public DataTable Optimize3(DataTable channelTable, string batchNo, int bNo,string preBatchNo)
        {
            DataTable supplyTable = GetEmptySupply();
            Dictionary<string, int> dicChannel = new Dictionary<string, int>();

            int serialNo = 0;

            if (channelTable.Rows.Count > 0)
                serialNo = int.Parse(channelTable.Rows[0]["serialNo"].ToString());

            foreach (DataRow row in channelTable.Rows)
            {
                string pre = "";
                if (preBatchNo.Length > 0)
                    pre = (int.Parse(preBatchNo.Substring(10, 3))).ToString();

                string cigarettecode = row["CIGARETTECODE"].ToString();
                if (cigarettecode.Trim().Length <= 0)
                    continue;
                int packetQty = int.Parse(row["PACKAGENUM"].ToString());
                int margin = int.Parse(row["QUANTITY" + pre].ToString());
                int balance = int.Parse(row["QUANTITY" + bNo.ToString()].ToString());

                if (balance > margin)
                {
                    serialNo++;
                    //需要补一件烟
                    DataRow supplyRow = supplyTable.NewRow();
                    supplyRow["ORDERDATE"] = row["ORDERDATE"];
                    supplyRow["BATCHNO"] = batchNo;
                    supplyRow["LINECODE"] = row["LINECODE"];

                    supplyRow["SERIALNO"] = serialNo;
                    supplyRow["SORTNO"] = row["SORTNO"]; ;

                    supplyRow["CIGARETTECODE"] = row["CIGARETTECODE"];
                    supplyRow["CIGARETTENAME"] = row["CIGARETTENAME"];

                    supplyRow["CHANNELCODE"] = row["CHANNELCODE"];
                    supplyRow["CHANNELGROUP"] = 1;
                    //supplyRow["GROUPNO"] = row["GROUPNO"];
                    supplyRow["BATCH"] = 1;
                    supplyRow["BARCODE"] = row["BARCODEPACK"];
                    supplyRow["ISBALANCE"] = 1;
                    supplyTable.Rows.Add(supplyRow);
                    //计算余量
                    //row["QUANTITY" + bNo.ToString()] = 50 + margin - balance;
                    row["QUANTITY" + bNo.ToString()] = packetQty + margin - balance;
                }
                else
                    //计算余量
                    row["QUANTITY" + bNo.ToString()] = margin - balance;
            }
            return supplyTable;
        }
        /// <summary>
        /// 通道机补货优化2015-04-07
        /// </summary>
        /// <param name="channelTable"></param>
        /// <param name="orderRows"></param>
        /// <returns></returns>
        public DataTable Optimize6(DataTable channelTable, DataRow[] orderRows,int orderCount)
        {
            DataTable supplyTable = GetEmptySupply();
            
            Dictionary<string, int> dicChannel = new Dictionary<string, int>();
            Dictionary<string, int> dicBoxes = new Dictionary<string, int>();
            Dictionary<string, int> dicPacketQty = new Dictionary<string, int>();
            foreach (DataRow row in channelTable.Rows)
            {
                dicChannel.Add(row["CHANNELCODE"].ToString(), 0);
                dicBoxes.Add(row["CHANNELCODE"].ToString(), int.Parse(row["BOXES"].ToString()));
                dicPacketQty.Add(row["CHANNELCODE"].ToString(), int.Parse(row["PACKAGENUM"].ToString()));
            }

            int serialNo = 1;
            if (channelTable.Rows.Count > 0)
                serialNo = int.Parse(channelTable.Rows[0]["serialNo"].ToString()) + 1;

            DataRow[] drs = channelTable.Select("", "CHANNELCODE");
            int preSortNo = 0;
            bool isSupply = false;
            int s = 0;
            foreach (DataRow row in orderRows)
            {
                int sortNo = int.Parse(row["SORTNO"].ToString());
                string channelCode = row["CHANNELCODE"].ToString();
                int quantity = Convert.ToInt32(row["QUANTITY"]);
                
                if (sortNo == 419)
                    s = 0;
                if (sortNo % orderCount > 0)
                {
                    if (isSupply)
                    {
                        //循环补货
                        for (int i = 0; i < 100; i++)
                        {
                            foreach (DataRow rowChannel in drs)
                            {
                                string currentChannel = rowChannel["CHANNELCODE"].ToString();
                                //此分拣烟道已经分配了多少件烟
                                DataRow[] rows = supplyTable.Select(string.Format("CHANNELCODE='{0}'", currentChannel));
                                if (rows.Length >= dicBoxes[currentChannel])
                                    continue;

                                //判断需要补烟数量
                                if (dicChannel[currentChannel] <= 0)
                                    continue;
                                //总需要件数
                                int boxTotal = dicChannel[currentChannel] / dicPacketQty[currentChannel];
                                //+ 尾数整件
                                boxTotal += dicChannel[currentChannel] % dicPacketQty[currentChannel] > 0 ? 1 : 0;
                                int box = boxTotal - rows.Length;

                                if (box > 0)
                                {
                                    DataRow supplyRow = supplyTable.NewRow();
                                    supplyRow["ORDERDATE"] = rowChannel["ORDERDATE"];
                                    supplyRow["BATCHNO"] = rowChannel["BATCHNO"];
                                    supplyRow["LINECODE"] = rowChannel["LINECODE"];

                                    supplyRow["SERIALNO"] = serialNo;
                                    supplyRow["SORTNO"] = serialNo;

                                    supplyRow["CIGARETTECODE"] = rowChannel["CIGARETTECODE"];
                                    supplyRow["CIGARETTENAME"] = rowChannel["CIGARETTENAME"];

                                    supplyRow["CHANNELCODE"] = rowChannel["CHANNELCODE"];
                                    supplyRow["CHANNELGROUP"] = rowChannel["CHANNELGROUP"];
                                    //supplyRow["GROUPNO"] = row["GROUPNO"];
                                    supplyRow["BATCH"] = 1;
                                    supplyRow["BARCODE"] = rowChannel["BARCODEPACK"];
                                    supplyRow["ISBALANCE"] = 0;

                                    supplyTable.Rows.Add(supplyRow);
                                    serialNo++;
                                }
                            }
                        }
                        isSupply = false;
                    }
                    dicChannel[channelCode] += quantity;
                    preSortNo = sortNo;
                    continue;
                }
                else
                {
                    dicChannel[channelCode] += quantity;
                    if (sortNo >= preSortNo)
                    {
                        preSortNo = sortNo;
                        isSupply = true;
                        continue;
                    }
                }
                
            }
            //剩下还没有分配的件数处理
            for (int i = 0; i < 1000; i++)
            {
                foreach (DataRow rowChannel in drs)
                {
                    string currentChannel = rowChannel["CHANNELCODE"].ToString();
                    //此分拣烟道已经分配了多少件烟
                    DataRow[] rows = supplyTable.Select(string.Format("CHANNELCODE='{0}'", currentChannel));
                    if (rows.Length >= dicBoxes[currentChannel])
                        continue;

                    DataRow supplyRow = supplyTable.NewRow();
                    supplyRow["ORDERDATE"] = rowChannel["ORDERDATE"];
                    supplyRow["BATCHNO"] = rowChannel["BATCHNO"];
                    supplyRow["LINECODE"] = rowChannel["LINECODE"];

                    supplyRow["SERIALNO"] = serialNo;
                    supplyRow["SORTNO"] = serialNo;

                    supplyRow["CIGARETTECODE"] = rowChannel["CIGARETTECODE"];
                    supplyRow["CIGARETTENAME"] = rowChannel["CIGARETTENAME"];

                    supplyRow["CHANNELCODE"] = rowChannel["CHANNELCODE"];
                    supplyRow["CHANNELGROUP"] = rowChannel["CHANNELGROUP"];
                    //supplyRow["GROUPNO"] = row["GROUPNO"];
                    supplyRow["BATCH"] = 1;
                    supplyRow["BARCODE"] = rowChannel["BARCODEPACK"];
                    supplyRow["ISBALANCE"] = 0;

                    supplyTable.Rows.Add(supplyRow);
                    serialNo++;
                }
            }
            return supplyTable;
        }
        private DataTable GetEmptySupply()
        {
            DataTable table = new DataTable("SUPPLY");

            table.Columns.Add("ORDERDATE");
            table.Columns.Add("BATCHNO");
            table.Columns.Add("LINECODE");
            table.Columns.Add("SERIALNO", typeof(Int32));
            
            table.Columns.Add("SORTNO");

            table.Columns.Add("CIGARETTECODE");
            table.Columns.Add("CIGARETTENAME");
            
            table.Columns.Add("CHANNELCODE");
            table.Columns.Add("CHANNELGROUP");
            table.Columns.Add("GROUPNO");
            table.Columns.Add("BATCH");
            table.Columns.Add("BARCODE");
            table.Columns.Add("ISBALANCE");
            return table;
        }
    }
}
