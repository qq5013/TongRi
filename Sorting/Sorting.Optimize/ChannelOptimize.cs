using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sorting.Optimize
{
    public class ChannelOptimize
    {     
        /// <summary>
        /// orderTable ���ж�������
        /// </summary>
        /// <param name="orderTable">���ж�������</param>
        /// <param name="lineOrderTable">ָ���ߵĶ�������</param>
        /// <param name="channelTable">�̵���</param>
        /// <param name="deviceTable">�������̵�����������</param>
        /// <param name="param">�����趨��</param>
        public void Optimize(DataTable orderTable, DataTable lineOrderTable, DataTable channelTable, DataTable deviceTable, Dictionary<string, string> param)
        {
            //����ռ��2��ͨ������Ʒ������
            int ocupyCount = Convert.ToInt32(param["OcupyCount"]);
            int ocupyCount1 = Convert.ToInt32(param["OcupyCount1"]);
            List<string> splitProduct = new List<string>();
            DataTable tmpTable = GenerateTmpTable();

            //����ռ������ͨ������Ʒ��
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

            //�̶��̵�����
            foreach (DataRow deviceRow in deviceTable.Rows)
            {
                string channelType = deviceRow["CHANNELTYPE"].ToString();
                SetFixedChannel(lineOrderTable, channelTable, channelType);
            }

            //�ǹ̶��̵�����
            foreach (DataRow deviceRow in deviceTable.Rows)
            {
                string channelType = deviceRow["CHANNELTYPE"].ToString();

                switch (channelType)
                {
                    case "2": //��ʽ�� 
                        SetTowerChannel(lineOrderTable, channelTable, tmpTable, channelType, ocupyCount1);
                        break;

                    case "3": //ͨ����
                        SetChannel(orderTable,lineOrderTable,channelTable, tmpTable, channelType, ocupyCount);
                        break;
                }
            }
        }
        /// <summary>
        /// orderTable ���ж�������
        /// </summary>
        /// <param name="orderTable">���ж�������</param>
        /// <param name="lineOrderTable">ָ���ߵĶ�������</param>
        /// <param name="channelTable">�̵���</param>
        /// <param name="deviceTable">�������̵�����������</param>
        /// <param name="param">�����趨��</param>
        public void Optimize(DataTable orderTable,DataTable channelTable, DataTable deviceTable, Dictionary<string, string> param)
        {
            //����ռ��2��ͨ������Ʒ������
            int ocupyCount = Convert.ToInt32(param["OcupyCount"]);
            int ocupyCount1 = Convert.ToInt32(param["OcupyCount1"]);

            //����������ռ�ٷֱȴ��ڻ�����趨ֵʱ
            string[] OrderQtyTPercent1 = param["OrderQtyTPercent1"].Split(',');
            float tp1 = 0;
            int tpQty1 = 1;
            if (OrderQtyTPercent1.Length > 1)
            {
                tp1 = float.Parse(OrderQtyTPercent1[0]);
                tpQty1 = int.Parse(OrderQtyTPercent1[1]);
            }
            //����������ռ�ٷֱȴ��ڻ�����趨ֵʱ
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

            //����ռ������ͨ������Ʒ��
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

            //�̶��̵�����
            foreach (DataRow deviceRow in deviceTable.Rows)
            {
                string channelType = deviceRow["CHANNELTYPE"].ToString();
                SetFixedChannel(orderTable, channelTable, channelType);
            }

            //�ǹ̶��̵�����
            foreach (DataRow deviceRow in deviceTable.Rows)
            {
                string channelType = deviceRow["CHANNELTYPE"].ToString();

                switch (channelType)
                {
                    case "2": //��ʽ�� 
                        //SetTowerChannel(orderTable, channelTable, tmpTable, channelType, ocupyCount1);
                        SetTowerChannel1(orderTable, channelTable, tmpTable, channelType, lp1, lpQty1, lp2, lpQty2);
                        break;

                    case "3": //ͨ����
                        //SetChannel(orderTable, orderTable, channelTable, tmpTable, channelType, ocupyCount);
                        SetChannel1(orderTable, channelTable, tmpTable, channelType, tp1, tpQty1, tp2, tpQty2);
                        break;
                }
            }
        }
        /// <summary>
        /// �̶��̵��ּ�
        /// </summary>
        /// <param name="orderTable"></param>
        /// <param name="channelTable"></param>
        /// <param name="channelType"></param>
        private void SetFixedChannel(DataTable orderTable, DataTable channelTable, string channelType)
        {
            //�̶��̵�Ʒ��
            foreach (DataRow orderRow in orderTable.Rows)
            {
                //ȡ��ǰƷ�ƹ̶��̵�
                DataRow[] channelRows = channelTable.Select(string.Format("CHANNELTYPE = '{0}' AND ProductCode = '{1}'  AND STATUS='1'", channelType, orderRow["ProductCode"]), "CHANNELORDER");

                if (channelRows.Length > 1)//ռ�ö����̵�
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
                else if (channelRows.Length == 1) //ֻռ��һ���̵�
                {
                    channelRows[0]["ProductCode"] = orderRow["ProductCode"];
                    channelRows[0]["ProductName"] = orderRow["ProductName"];
                    channelRows[0]["GroupTotal"] = orderRow["TOTAL_PRODUCT_QUANTITY"];//++q
                    orderRow["TOTAL_PRODUCT_QUANTITY"] = 0;
                }
            }
        }

        /// <summary>
        /// ����ͨ�����ǹ̶��̵�
        /// </summary>
        /// <param name="orderTable"></param>
        /// <param name="channelTable"></param>
        /// <param name="tmpTable"></param>
        /// <param name="channelType"></param>
        /// <param name="sortSplitProduct"></param>
        private void SetChannel(DataTable orderTable, DataTable lineOrderTable, DataTable channelTable, DataTable tmpTable, string channelType, int sortSplitProduct)
        {

            //�ǹ̶�ͨ����Ʒ��
            DataRow[] orderRows = orderTable.Select("QUANTITY > 0", "QUANTITY DESC");
            foreach (DataRow orderRow in orderRows)
            {
                //ȡδ��ռ��ͨ�����̵�
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
                                //�ڵ�ǰ��������Ƿ����̵������û����������һ������в�ѯ
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
        /// ����ͨ�����ǹ̶��̵�
        /// </summary>
        /// <param name="orderTable"></param>
        /// <param name="channelTable"></param>
        /// <param name="tmpTable"></param>
        /// <param name="channelType"></param>
        /// <param name="sortSplitProduct"></param>
        private void SetChannel1(DataTable orderTable, DataTable channelTable, DataTable tmpTable, string channelType,float tp1,int tpQty1,float tp2,int tpQty2)
        {

            //�ǹ̶�ͨ����Ʒ��
            DataRow[] orderRows = orderTable.Select("TOTAL_PRODUCT_QUANTITY > 0", "TOTAL_PRODUCT_QUANTITY DESC,PRODUCTCODE");
            foreach (DataRow orderRow in orderRows)
            {
                int tpQty = 1;
                float orderPercent = float.Parse(orderRow["PRODUCT_PERCENT"].ToString());
                if (orderPercent >= tp1)
                    tpQty = tpQty1;
                else if (orderPercent < tp1 && orderPercent >= tp2)
                    tpQty = tpQty2;


                //ȡδ��ռ��ͨ�����̵�
                int count = Convert.ToInt32(channelTable.Compute("COUNT(PRODUCTCODE)", string.Format("STATUS = '1' AND PRODUCTCODE='{0}'", orderRow["PRODUCTCODE"])));
                DataRow[] channelRows = channelTable.Select(string.Format("CHANNELTYPE = '{0}' AND PRODUCTCODE=''  AND STATUS='1'", channelType), "CHANNELORDER");

                //ͨ����Ԥ��һ������
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
        /// ������ʽ���ǹ̶��̵�
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
                    //�����δ��ռ�õ���ʽ��
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
                                //�ڵ�ǰ��������Ƿ����̵������û����������һ������в�ѯ
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
                        throw new Exception("���о���Ʒ��δ�����̵�,������̵����á�");
                    }
                }
            }
        }
        /// <summary>
        /// ������ʽ���ǹ̶��̵�
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
                        throw new Exception("���в�Ʒδ�����λ,������������á�");
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
