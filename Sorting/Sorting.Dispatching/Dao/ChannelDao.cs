using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DB.Util;

namespace Sorting.Dispatching.Dao
{
    public class ChannelDao: BaseDao
    {
        public DataTable FindAll()
        {
            string sql = "SELECT * FROM V_CMD_Channel ";
            return ExecuteQuery(sql, "CMD_CHANNEL").Tables[0];
        }
        public DataTable FindAll(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT * FROM V_CMD_Channel " + where;
            return ExecuteQuery(sql, "CMD_CHANNEL").Tables[0];
        }
        public DataTable FindAll(int startRecord, int pageSize, string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT * FROM V_CMD_Channel " + where;
            return ExecuteQuery(sql, "CMD_CHANNEL", startRecord, pageSize).Tables[0];
        }

        public int FindCount(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT COUNT(*) FROM V_CMD_Channel " + where;
            return (int)ExecuteScalar(sql);
        }

        /// <summary>
        /// 2010-11-19 todo
        /// </summary>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        public DataSet FindAvailableChannel(string lineCode)
        {
            string sql = string.Format("SELECT * ,ROW_NUMBER() OVER (ORDER BY CASE WHEN GROUPNO > 8 THEN ABS(GROUPNO - 17) ELSE GROUPNO END,GROUPNO) AS CHANNELINDEX FROM CMD_CHANNEL WHERE LINECODE = '{0}' ", lineCode);
            sql = string.Format("SELECT *  FROM CMD_CHANNEL WHERE LINECODE = '{0}' ", lineCode);
            return ExecuteQuery(sql);
        }

        public DataSet FindChannelSchedule(string orderDate, string batchNo, string lineCode, bool IsAdvancedSupply)
        {
            string sql = "SELECT *,CASE WHEN CHANNELTYPE='3' THEN 50 ELSE 40 END REMAINQUANTITY,CASE WHEN CHANNELTYPE='3' THEN QUANTITY / 50 - 3 ELSE QUANTITY / 50 -1 END PIECE " +
                "FROM SC_CHANNELUSED WHERE LINECODE = '{0}' AND BATCHNO = '{1}' AND ORDERDATE = '{2}'";
            if (IsAdvancedSupply)
            {
                sql = "SELECT *, CASE WHEN CHANNELTYPE='3' THEN 50 ELSE 50 END REMAINQUANTITY,CASE WHEN CHANNELTYPE='3' THEN QUANTITY / 50 ELSE QUANTITY / 50 -1 END PIECE " +
                "FROM SC_CHANNELUSED WHERE LINECODE = '{0}' AND BATCHNO = '{1}' AND ORDERDATE = '{2}'";
            }
            return ExecuteQuery(string.Format(sql, lineCode, batchNo, orderDate));
        }
        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>22
        public DataSet FindChannelSchedule(string orderDate, string batchNo, string lineCode)
        {
            string sql = "SELECT A.*,D.CIGARETTECODE AS D_CIGARETTECODE,A.QUANTITY,C.BARCODE,C.BARCODEPACK,A.QUANTITY/50 BOXES,A.QUANTITY%50 BALANCE " + 
                    " FROM SC_CHANNELUSED A" +                    
                    " LEFT JOIN CMD_CHANNEL D ON A.CHANNELID = D.CHANNELID " +
                    " LEFT JOIN CMD_Product C ON A.CIGARETTECODE=C.CIGARETTECODE " +
                    " WHERE A.LINECODE = '{0}' AND A.BATCHNO = '{1}' AND A.ORDERDATE = '{2}' " +
                    " ORDER BY A.CHANNELORDER";
            return ExecuteQuery(string.Format(sql, lineCode, batchNo, orderDate));
        }
        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>22
        public DataSet FindChannel3(string orderDate, string batchNo)
        {
            string sql = "SELECT A.*,D.CIGARETTECODE AS D_CIGARETTECODE,A.QUANTITY,C.BARCODE,C.BARCODEPACK,A.QUANTITY/50 BOXES,A.QUANTITY%50 BALANCE,(SELECT ISNULL(MAX(SERIALNO),0) FROM SC_SUPPLY) SERIALNO " +
                         "FROM SC_CHANNELUSED A " +
                         "LEFT JOIN CMD_CHANNEL D ON A.CHANNELID = D.CHANNELID " +
                         "LEFT JOIN CMD_Product C ON A.CIGARETTECODE=C.CIGARETTECODE " +
                         "WHERE A.CHANNELTYPE= '3' AND A.BATCHNO = '{0}' AND A.ORDERDATE = '{1}' " +
                         "ORDER BY A.CHANNELORDER";
            return ExecuteQuery(string.Format(sql, batchNo, orderDate));
        }
        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>22
        public DataSet FindChannel3(string orderDate, string batchNo,string firstBatchNo,int pNo)
        {
            string sql = "SELECT A.*,D.CIGARETTECODE AS D_CIGARETTECODE,A.QUANTITY,C.BARCODE,C.BARCODEPACK,A.QUANTITY/50 BOXES,A.QUANTITY%50 BALANCE," +
                         "(SELECT ISNULL(MAX(SERIALNO),0) FROM SC_SUPPLY) SERIALNO,ISNULL(B.QUANTITY{3},0) PBALANCE,ISNULL(C.PACKAGENUM,50) PACKAGENUM " +
                         "FROM SC_CHANNELUSED A " +
                         "LEFT JOIN SC_CHANNELBALANCE B ON A.CHANNELID = B.CHANNELID AND B.BATCHNO='{2}' " +
                         "LEFT JOIN CMD_CHANNEL D ON A.CHANNELID = D.CHANNELID " +
                         "LEFT JOIN CMD_Product C ON A.CIGARETTECODE=C.CIGARETTECODE " +
                         "WHERE A.CHANNELTYPE= '3' AND A.BATCHNO = '{0}' AND A.ORDERDATE = '{1}' " +
                         "ORDER BY A.CHANNELORDER";

            sql = string.Format(sql, batchNo, orderDate, firstBatchNo, pNo == 0 ? "" : pNo.ToString());
            return ExecuteQuery(sql);
        }
        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>22
        public DataSet FindChannel31(string orderDate, string batchNo)
        {
            string sql = "SELECT A.*,D.CIGARETTECODE AS D_CIGARETTECODE,A.QUANTITY,C.BARCODE,C.BARCODEPACK,A.QUANTITY/50 BOXES,A.QUANTITY%50 BALANCE," +
                         "(SELECT ISNULL(MAX(SERIALNO),0) FROM SC_SUPPLY) SERIALNO,ISNULL(C.PACKAGENUM,50) PACKAGENUM " +
                         "FROM SC_CHANNELUSED A " +
                         "LEFT JOIN CMD_CHANNEL D ON A.CHANNELID = D.CHANNELID " +
                         "LEFT JOIN CMD_Product C ON A.CIGARETTECODE=C.CIGARETTECODE " +
                         "WHERE A.CHANNELTYPE= '3' AND A.BATCHNO = '{0}' AND A.ORDERDATE = '{1}' " +
                         "ORDER BY A.CHANNELADDRESS";

            sql = string.Format(sql, batchNo, orderDate);
            return ExecuteQuery(sql);
        }
        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>22
        public DataSet FindChannelBalance3(string orderDate, string batchNo)
        {
            string sql = "SELECT A.ORDERDATE, A.BATCHNO, A.CHANNELID, A.LINECODE, A.CHANNELTYPE, A.CHANNELCODE, A.CHANNELNAME, A.CIGARETTECODE, A.CIGARETTENAME, " +
                         "A.QUANTITY,A.QUANTITY1, A.QUANTITY2, A.QUANTITY3, A.QUANTITY4, A.QUANTITY5, A.QUANTITY6, A.QUANTITY7, A.QUANTITY8, A.QUANTITY9," +
                         "C.BARCODEPACK,MAX(B.SORTNO) SORTNO,(SELECT ISNULL(MAX(SERIALNO),0) FROM SC_SUPPLY) SERIALNO,C.PACKAGENUM " +
                         "FROM SC_CHANNELBALANCE A " +
                         "LEFT JOIN SC_ORDER_DETAIL B ON A.CHANNELCODE=B.CHANNELCODE " +
                         "LEFT JOIN CMD_Product C ON A.CIGARETTECODE=C.CIGARETTECODE " +
                         "WHERE A.CHANNELTYPE= '3' AND A.BATCHNO = '{0}' AND A.ORDERDATE = '{1}' " + 
                         "GROUP BY A.ORDERDATE, A.BATCHNO, A.CHANNELID, A.LINECODE, A.CHANNELTYPE, A.CHANNELCODE, A.CHANNELNAME, A.CIGARETTECODE, A.CIGARETTENAME, " +
                         "A.QUANTITY,A.QUANTITY1, A.QUANTITY2, A.QUANTITY3, A.QUANTITY4, A.QUANTITY5, A.QUANTITY6, A.QUANTITY7, A.QUANTITY8, A.QUANTITY9,C.BARCODEPACK,C.PACKAGENUM " +
                         "ORDER BY A.CHANNELCODE ";
            return ExecuteQuery(string.Format(sql, batchNo, orderDate));
        }
        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>22
        public DataSet FindChannelBalance2(string orderDate, string batchNo)
        {
            string sql = "SELECT A.ORDERDATE, A.BATCHNO, A.CHANNELID, A.LINECODE, A.CHANNELTYPE, A.CHANNELCODE, A.CHANNELNAME, A.CIGARETTECODE, A.CIGARETTENAME, " +
                         "A.QUANTITY,A.QUANTITY1, A.QUANTITY2, A.QUANTITY3, A.QUANTITY4, A.QUANTITY5, A.QUANTITY6, A.QUANTITY7, A.QUANTITY8, A.QUANTITY9," +
                         "C.BARCODEPACK,MAX(B.SORTNO) SORTNO,(SELECT ISNULL(MAX(SERIALNO),0) FROM SC_SUPPLY2) SERIALNO,C.PACKAGENUM " +
                         "FROM SC_CHANNELBALANCE A " +
                         "LEFT JOIN SC_ORDER_DETAIL B ON A.CHANNELCODE=B.CHANNELCODE " +
                         "LEFT JOIN CMD_Product C ON A.CIGARETTECODE=C.CIGARETTECODE " +
                         "WHERE A.CHANNELTYPE= '2' AND A.BATCHNO = '{0}' AND A.ORDERDATE = '{1}' " +
                         "GROUP BY A.ORDERDATE, A.BATCHNO, A.CHANNELID, A.LINECODE, A.CHANNELTYPE, A.CHANNELCODE, A.CHANNELNAME, A.CIGARETTECODE, A.CIGARETTENAME, " +
                         "A.QUANTITY,A.QUANTITY1, A.QUANTITY2, A.QUANTITY3, A.QUANTITY4, A.QUANTITY5, A.QUANTITY6, A.QUANTITY7, A.QUANTITY8, A.QUANTITY9,C.BARCODEPACK,C.PACKAGENUM " +
                         "ORDER BY A.CHANNELCODE ";
            return ExecuteQuery(string.Format(sql, batchNo, orderDate));
        }
        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>22
        public DataSet FindChannel2(string orderDate, string batchNo, string firstBatchNo, int pNo)
        {
            string sql = "SELECT A.ORDERDATE, A.BATCHNO, A.LINECODE, MIN(A.CHANNELCODE) CHANNELCODE, A.CHANNELTYPE,A.CHANNELGROUP,A.CIGARETTECODE, A.CIGARETTENAME," +
                         "SUM(A.QUANTITY) QUANTITY,D.CIGARETTECODE AS D_CIGARETTECODE,C.BARCODE,C.BARCODEPACK,SUM(A.QUANTITY)/ISNULL(C.PACKAGENUM,50) BOXES,SUM(A.QUANTITY)%ISNULL(C.PACKAGENUM,50) BALANCE, " +
                         "(SELECT ISNULL(MAX(SERIALNO),0) FROM SC_SUPPLY2) SERIALNO,ISNULL(SUM(B.QUANTITY{3}),0) PBALANCE,ISNULL(C.PACKAGENUM,50) PACKAGENUM " +
                         "FROM SC_CHANNELUSED A " +
                         "LEFT JOIN SC_CHANNELBALANCE B ON A.CHANNELID = B.CHANNELID AND B.BATCHNO='{2}' " +
                         "LEFT JOIN CMD_CHANNEL D ON A.CHANNELID = D.CHANNELID " +
                         "LEFT JOIN CMD_Product C ON A.CIGARETTECODE=C.CIGARETTECODE " +
                         "WHERE A.CHANNELTYPE= '2' AND A.BATCHNO = '{0}' AND A.ORDERDATE = '{1}' AND A.QUANTITY>0 " +
                         "GROUP BY A.ORDERDATE, A.BATCHNO, A.LINECODE,A.CHANNELTYPE,A.CHANNELGROUP,A.CIGARETTECODE, A.CIGARETTENAME, D.CIGARETTECODE,C.BARCODE,C.BARCODEPACK,C.PACKAGENUM " +
                         "ORDER BY MIN(A.CHANNELCODE)";
            
            sql = string.Format(sql, batchNo, orderDate, firstBatchNo, pNo == 0 ? "" : pNo.ToString());
            return ExecuteQuery(sql);
        }
        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>22
        public DataSet FindChannel21(string orderDate, string batchNo)
        {
            string sql = "SELECT A.ORDERDATE, A.BATCHNO, A.LINECODE, MIN(A.CHANNELCODE) CHANNELCODE, A.CHANNELTYPE,A.CHANNELGROUP,A.CIGARETTECODE, A.CIGARETTENAME," +
                         "SUM(A.QUANTITY) QUANTITY,D.CIGARETTECODE AS D_CIGARETTECODE,C.BARCODE,C.BARCODEPACK,SUM(A.QUANTITY)/ISNULL(C.PACKAGENUM,50) BOXES,SUM(A.QUANTITY)%ISNULL(C.PACKAGENUM,50) BALANCE, " +
                         "(SELECT ISNULL(MAX(SERIALNO),0) FROM SC_SUPPLY2) SERIALNO,ISNULL(C.PACKAGENUM,50) PACKAGENUM " +
                         "FROM SC_CHANNELUSED A " +
                         "LEFT JOIN CMD_CHANNEL D ON A.CHANNELID = D.CHANNELID " +
                         "LEFT JOIN CMD_Product C ON A.CIGARETTECODE=C.CIGARETTECODE " +
                         "WHERE A.CHANNELTYPE= '2' AND A.BATCHNO = '{0}' AND A.ORDERDATE = '{1}' AND A.QUANTITY>0 " +
                         "GROUP BY A.ORDERDATE, A.BATCHNO, A.LINECODE,A.CHANNELTYPE,A.CHANNELGROUP,A.CIGARETTECODE, A.CIGARETTENAME, D.CIGARETTECODE,C.BARCODE,C.BARCODEPACK,C.PACKAGENUM " +
                         "ORDER BY MIN(A.CHANNELCODE)";

            sql = string.Format(sql, batchNo, orderDate);
            return ExecuteQuery(sql);
        }
        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>22
        public DataSet FindChannel2(string orderDate, string batchNo)
        {
            string sql = "SELECT A.ORDERDATE, A.BATCHNO, A.LINECODE, MIN(A.CHANNELCODE) CHANNELCODE, A.CHANNELTYPE,A.CHANNELGROUP,A.CIGARETTECODE, A.CIGARETTENAME," +
                         "SUM(A.QUANTITY) QUANTITY,D.CIGARETTECODE AS D_CIGARETTECODE,C.BARCODE,C.BARCODEPACK,SUM(A.QUANTITY)/50 BOXES,SUM(A.QUANTITY)%50 BALANCE, " +
                         "(SELECT ISNULL(MAX(SERIALNO),0) FROM SC_SUPPLY2) SERIALNO " +
                         "FROM SC_CHANNELUSED A " +
                         "LEFT JOIN CMD_CHANNEL D ON A.CHANNELID = D.CHANNELID " +
                         "LEFT JOIN CMD_Product C ON A.CIGARETTECODE=C.CIGARETTECODE " +
                         "WHERE A.CHANNELTYPE= '2' AND A.BATCHNO = '{0}' AND A.ORDERDATE = '{1}' AND A.QUANTITY>0 " +
                         "GROUP BY A.ORDERDATE, A.BATCHNO, A.LINECODE,A.CHANNELTYPE,A.CHANNELGROUP,A.CIGARETTECODE, A.CIGARETTENAME, D.CIGARETTECODE,C.BARCODE,C.BARCODEPACK " +
                         "ORDER BY MIN(A.CHANNELCODE)";
            return ExecuteQuery(string.Format(sql, batchNo, orderDate));
        }
        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>22
        public DataSet FindChannelSchedule(string batchNo, string lineCode)
        {
            string sql = @"SELECT *, 50 REMAINQUANTITY,CASE WHEN CHANNELTYPE='3' THEN QUANTITY / 50 - 3 ELSE QUANTITY / 50 - 1 END PIECE,ISNULL(tmp.FChannelQty,0) FChannelQty,ISNULL(tmp.SChannelQty,0) SChannelQty
                        FROM SC_CHANNELUSED A 
                        LEFT JOIN (select batchno,Productcode,Productname,
                        case when channelCount=2 or channelCount=3 then
                        sum(eachquantity5*5+(case when channelCount=2 and eachquantity51=1 then 5 else 0 end)) else 0 end FChannelQty,
                        case when channelCount=3 then
                        sum(eachquantity5*5+(case when channelCount=3 and eachquantity51=2 then 5 else 0 end)) else 0 end SChannelQty
                        from V_ORDER_DETAIL5 group by batchno,Productcode,Productname,channelCount) tmp ON A.batchno=tmp.batchno and A.Productcode=tmp.Productcode
                        WHERE A.LINECODE = '{0}' AND A.BATCHNO = '{1}' ORDER BY A.CHANNELORDER";
            
            return ExecuteQuery(string.Format(sql, lineCode, batchNo));
        }
        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>22
        public DataSet FindChannelSchedule(string orderDate, string batchNo, string lineCode, int remainCount)
        {
            string sql = "SELECT *, 50 REMAINQUANTITY,CASE WHEN CHANNELTYPE='3' THEN QUANTITY / 50 - 3 ELSE QUANTITY / 50 - 1 END PIECE " +
                         "FROM SC_CHANNELUSED WHERE LINECODE = '{0}' AND BATCHNO = '{1}' AND ORDERDATE = '{2}' ORDER BY CHANNELORDER";

            sql = "SELECT A.*,D.CIGARETTECODE  AS D_CIGARETTECODE," +
                    " 	CASE WHEN (A.QUANTITY + ISNULL(B.QUANTITY,0))%50 > 16 " +
                    " 		    THEN (A.QUANTITY + ISNULL(B.QUANTITY,0))%50 - 16" +
                    "       WHEN (ISNULL(C.QUANTITY,0) - ISNULL(B.QUANTITY,0)) > 16 " +
                    " 		    THEN (ISNULL(C.QUANTITY,0) - ISNULL(B.QUANTITY,0)) - 16" +
                    " 		ELSE 50 + (A.QUANTITY + ISNULL(B.QUANTITY,0))%50 + (ISNULL(C.QUANTITY,0) - ISNULL(B.QUANTITY,0)) - 16" +
                    " 	END REMAINQUANTITY, " +
                    " 	CASE WHEN A.CHANNELTYPE='3' " +
                    " 		THEN " +
                    " 	        CASE WHEN (A.QUANTITY + ISNULL(B.QUANTITY,0))%50 > 16 " +
                    " 		            THEN (A.QUANTITY + ISNULL(B.QUANTITY,0))/ 50 - {3} " +
                    "               WHEN (ISNULL(C.QUANTITY,0) - ISNULL(B.QUANTITY,0)) > 16 " +
                    " 		            THEN (A.QUANTITY + ISNULL(B.QUANTITY,0))/ 50 - {3} " +
                    " 		        ELSE (A.QUANTITY + ISNULL(B.QUANTITY,0))/ 50 - ({3} + 1 ) " +
                    " 	        END " +
                    " 		ELSE (A.QUANTITY + ISNULL(B.QUANTITY,0))/ 50 - 1 " +
                    " 	END PIECE, " +
                    "   ISNULL(C.QUANTITY,0) AS BALANCE, " +
                    "   (A.QUANTITY + ISNULL(B.QUANTITY,0)) AS SUPPLYQUANTITY" +
                    " FROM SC_CHANNELUSED A" +
                    " LEFT JOIN (SELECT CHANNELID,LINECODE,CHANNELCODE,CHANNELNAME," +
                    " 	CIGARETTECODE,CIGARETTENAME,SUM(QUANTITY) AS QUANTITY " +
                    " 	FROM SC_BALANCE" +
                    " 	WHERE LINECODE = '{0}' " +
                    " 	AND BATCHNO = '{1}' " +
                    " 	AND ORDERDATE = '{2}' " +
                    " 	GROUP BY CHANNELID,LINECODE,CHANNELCODE,CHANNELNAME," +
                    " 	CIGARETTECODE,CIGARETTENAME) B " +
                    " ON A.CHANNELID = B.CHANNELID" +
                    " LEFT JOIN (SELECT CHANNELID,LINECODE,CHANNELCODE,CHANNELNAME," +
                    " 	CIGARETTECODE,CIGARETTENAME,SUM(QUANTITY) AS QUANTITY " +
                    " 	FROM SC_BALANCE " +
                    " 	GROUP BY CHANNELID,LINECODE,CHANNELCODE,CHANNELNAME," +
                    " 	CIGARETTECODE,CIGARETTENAME) C" +
                    " ON A.CHANNELID = C.CHANNELID" +
                    " LEFT JOIN CMD_CHANNEL D ON A.CHANNELID = D.CHANNELID " +
                    " WHERE A.LINECODE = '{0}' " +
                    " AND A.BATCHNO = '{1}' " +
                    " AND A.ORDERDATE = '{2}' " +
                    " ORDER BY A.CHANNELORDER";
            return ExecuteQuery(string.Format(sql, lineCode, batchNo, orderDate, remainCount));
        }

        /// <summary>
        /// 2010-11-19 todo
        /// </summary>
        /// <param name="channelTable"></param>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void SaveChannelSchedule(DataTable channelTable, string orderDate, string batchNo)
        {
            foreach (DataRow channelRow in channelTable.Rows)
            {
                SqlCreate sql = new SqlCreate("SC_CHANNELUSED", SqlType.INSERT);
                sql.AppendQuote("ORDERDATE", orderDate);
                sql.AppendQuote("BATCHNO", batchNo);
                sql.AppendQuote("CHANNELID", channelRow["CHANNELID"]);
                sql.AppendQuote("LINECODE", channelRow["LINECODE"]);

                sql.AppendQuote("CHANNELCODE", channelRow["CHANNELCODE"]);
                sql.AppendQuote("CHANNELNAME", channelRow["CHANNELNAME"]);
                sql.AppendQuote("CHANNELTYPE", channelRow["CHANNELTYPE"]);

                if (channelRow["STATUS"].ToString() == "1")
                {
                    sql.AppendQuote("ProductCode", channelRow["ProductCode"]);
                    sql.AppendQuote("ProductName", channelRow["ProductName"]);
                }
                sql.Append("QUANTITY", channelRow["QUANTITY"]);

                sql.AppendQuote("STATUS", channelRow["STATUS"]);

                sql.AppendQuote("LEDGROUP", channelRow["LEDGROUP"]);
                sql.AppendQuote("LEDNO", channelRow["LEDNO"]);
                sql.AppendQuote("LEDX", channelRow["LEDX"]);
                sql.AppendQuote("LEDY", channelRow["LEDY"]);
                sql.AppendQuote("LEDWIDTH", channelRow["LEDWIDTH"]);
                sql.AppendQuote("LEDHEIGHT", channelRow["LEDHEIGHT"]);
                sql.AppendQuote("GroupTotal", channelRow["GroupTotal"]);
                sql.AppendQuote("GROUPNO", channelRow["GROUPNO"]);
                sql.AppendQuote("CHANNELGROUP", channelRow["CHANNELGROUP"]);
                sql.AppendQuote("CHANNELORDER", channelRow["CHANNELORDER"]);
                sql.AppendQuote("CHANNELADDRESS", channelRow["CHANNELADDRESS"]);
                sql.AppendQuote("SUPPLYADDRESS", channelRow["SUPPLYADDRESS"]);

                ExecuteNonQuery(sql.GetSQL());
            }
        }

        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="channelTable"></param>
        public void Update(DataTable channelTable)
        {
            foreach (DataRow channelRow in channelTable.Rows)
            {
                string sql = string.Format("UPDATE SC_CHANNELUSED SET SORTNO={0} WHERE ORDERDATE='{1}' AND BATCHNO='{2}' AND LINECODE='{3}' AND CHANNELCODE='{4}'",
                    channelRow["SORTNO"], channelRow["ORDERDATE"], channelRow["BATCHNO"].ToString().Trim(), channelRow["LINECODE"], channelRow["CHANNELCODE"]);
                ExecuteNonQuery(sql);
            }
        }

        public void Update(DataTable channelTable, string orderDate, string batchNo)
        {
            foreach (DataRow channelRow in channelTable.Rows)
            {
                string sql = string.Format("UPDATE SC_CHANNELUSED SET SORTNO={0} ,QUANTITY = '{1}' WHERE ORDERDATE='{2}' AND BATCHNO='{3}' AND LINECODE='{4}' AND CHANNELCODE='{5}'",
                    channelRow["SORTNO"], channelRow["QUANTITY"], orderDate, batchNo, channelRow["LINECODE"], channelRow["CHANNELCODE"]);
                ExecuteNonQuery(sql);
            }
        }

        public string GetEnableChannel()
        {
            string strSql = "SELECT COUNT(CHANNELID) FROM CMD_CHANNEL WHERE CHANNELTYPE ='2' AND STATUS='1'";
            return ExecuteQuery(strSql).Tables[0].Rows[0][0].ToString();
        }

        public DataTable FindMultiBrandChannel(string lineCode)
        {
            string strSql = string.Format("SELECT * ,0 SORTNO FROM CMD_CHANNEL WHERE CHANNELTYPE ='5' AND STATUS='1' AND LINECODE='{0}' ", lineCode);
            return ExecuteQuery(strSql).Tables[0];
        }

        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="channelTable"></param>
        public void UpdateQuantity(DataTable channelTable, bool isUseBalance)
        {
            foreach (DataRow channelRow in channelTable.Rows)
            {
                string sql = "UPDATE SC_CHANNELUSED SET PRODUCTCODE = '{0}',PRODUCTNAME = '{1}',QUANTITY={2} " +
                                " WHERE ORDERDATE='{3}' AND BATCHNO='{4}' AND LINECODE='{5}' AND CHANNELCODE='{6}'";
                ExecuteNonQuery(string.Format(sql, channelRow["PRODUCTCODE"], channelRow["PRODUCTNAME"], channelRow["QUANTITY"],
                    channelRow["ORDERDATE"], channelRow["BATCHNO"].ToString().Trim(), channelRow["LINECODE"], channelRow["CHANNELCODE"]));

                //if (isUseBalance && channelRow["CHANNELTYPE"].ToString() == "3" && channelRow["CIGARETTECODE"].ToString() == channelRow["D_CIGARETTECODE"].ToString())
                if (isUseBalance && channelRow["CHANNELTYPE"].ToString() == "3")
                {
                    SqlCreate sqlCreate = new SqlCreate("SC_BALANCE", SqlType.INSERT);

                    sqlCreate.AppendQuote("ORDERDATE", channelRow["ORDERDATE"]);
                    sqlCreate.Append("BATCHNO", channelRow["BATCHNO"]);
                    sqlCreate.AppendQuote("CHANNELID", channelRow["CHANNELID"]);
                    sqlCreate.AppendQuote("LINECODE", channelRow["LINECODE"]);
                    sqlCreate.AppendQuote("CHANNELCODE", channelRow["CHANNELCODE"]);
                    sqlCreate.AppendQuote("CHANNELNAME", channelRow["CHANNELNAME"]);
                    sqlCreate.AppendQuote("CIGARETTECODE", channelRow["CIGARETTECODE"]);
                    sqlCreate.AppendQuote("CIGARETTENAME", channelRow["CIGARETTENAME"]);

                    int quantity = Convert.ToInt32(channelRow["QUANTITY"]) % 50;
                    int balance = Convert.ToInt32(channelRow["BALANCE"]);

                    sqlCreate.Append("QUANTITY", balance >= quantity ? 0 - quantity : 50 - quantity);

                    ExecuteNonQuery(sqlCreate.GetSQL());
                }
                else
                {
                    ExecuteNonQuery(string.Format("DELETE FROM SC_BALANCE WHERE CHANNELID = '{0}'", channelRow["CHANNELID"]));
                }
            }
        }

        internal void UpdateEntity(string channelCode, string cigaretteCode, string cigaretteName, string status)
        {
            SqlCreate sqlCreate = new SqlCreate("CMD_CHANNEL", SqlType.UPDATE);
            sqlCreate.AppendQuote("CIGARETTECODE", cigaretteCode.Trim());
            sqlCreate.AppendQuote("CIGARETTENAME", cigaretteName.Trim());
            sqlCreate.AppendQuote("STATUS", status);
            sqlCreate.AppendWhereQuote("CHANNELCODE", channelCode);
            ExecuteNonQuery(sqlCreate.GetSQL());

            string sql = "INSERT INTO SC_BALANCE_OUT" +
                            " SELECT GETDATE(),LINECODE,CHANNELCODE,CHANNELNAME,CIGARETTECODE, CIGARETTENAME, " +
                            " SUM(QUANTITY) AS QUANTITY,0" +
                            " FROM SC_BALANCE" +
                            " WHERE CHANNELCODE = '{0}'" +
                            " GROUP BY LINECODE,CHANNELCODE,CHANNELNAME,CIGARETTECODE, CIGARETTENAME" +
                            " HAVING ISNULL(SUM(QUANTITY),0) > 0 ";
            ExecuteNonQuery(string.Format(sql, channelCode));

            sql = "DELETE FROM SC_BALANCE WHERE CHANNELCODE = '{0}'";
            ExecuteNonQuery(string.Format(sql, channelCode));
        }
        internal void UpdateEntity(string channelCode, string productCode, string productName, string channelOrder, string status)
        {
            SqlCreate sqlCreate = new SqlCreate("CMD_CHANNEL", SqlType.UPDATE);
            sqlCreate.AppendQuote("ProductCode", productCode.Trim());
            sqlCreate.AppendQuote("ProductName", productName.Trim());
            sqlCreate.Append("CHANNELORDER", channelOrder);
            sqlCreate.AppendQuote("STATUS", status);
            sqlCreate.AppendWhereQuote("CHANNELCODE", channelCode);
            ExecuteNonQuery(sqlCreate.GetSQL());

            //string sql = "INSERT INTO SC_BALANCE_OUT" +
            //                " SELECT GETDATE(),LINECODE,CHANNELCODE,CHANNELNAME,CIGARETTECODE, CIGARETTENAME, " +
            //                " SUM(QUANTITY) AS QUANTITY,0" +
            //                " FROM SC_BALANCE" +
            //                " WHERE CHANNELCODE = '{0}'" +
            //                " GROUP BY LINECODE,CHANNELCODE,CHANNELNAME,CIGARETTECODE, CIGARETTENAME" +
            //                " HAVING ISNULL(SUM(QUANTITY),0) > 0 ";
            //ExecuteNonQuery(string.Format(sql, channelCode));

            //sql = "DELETE FROM SC_BALANCE WHERE CHANNELCODE = '{0}'";
            //ExecuteNonQuery(string.Format(sql, channelCode));
        }
        public DataTable FindChannel()
        {
            string sql = "SELECT A.BATCHNO,A.CHANNELCODE, A.CHANNELNAME, " +
                         "CASE A.CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE, " +
                         "A.LINECODE, A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY,CASE A.CHANNELTYPE WHEN '3' THEN A.QUANTITY/50 ELSE " +
                         "CASE WHEN (SELECT COUNT(*) FROM SC_CHANNELUSED WHERE CIGARETTECODE=A.CIGARETTECODE AND BATCHNO=A.BATCHNO)>1 THEN ISNULL(B.QUANTITY,0)/50 " +
                         "ELSE A.QUANTITY/50 END END BOXES,ISNULL(B.BALANCE,0) BALANCE " +
                         "FROM SC_CHANNELUSED A " +
                         "LEFT JOIN SC_BALANCE B ON A.CHANNELCODE=B.CHANNELCODE " +
                         "WHERE A.BATCHNO IN(SELECT TOP 1 BATCHNO FROM SC_BALANCE) " +
                         "ORDER BY A.BATCHNO,A.CHANNELCODE,A.CHANNELNAME";
            return ExecuteQuery(sql).Tables[0];
        }
        public DataSet FindChannels()
        {
            string sql = "SELECT A.BATCHNO,A.CHANNELCODE, A.CHANNELNAME, " +
                         "CASE A.CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE, " +
                         "A.LINECODE, A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY,CASE A.CHANNELTYPE WHEN '3' THEN A.QUANTITY/50 ELSE " +
                         "CASE WHEN (SELECT COUNT(*) FROM SC_CHANNELUSED WHERE CIGARETTECODE=A.CIGARETTECODE AND BATCHNO=A.BATCHNO)>1 THEN ISNULL(B.QUANTITY,0)/50 " +
                         "ELSE A.QUANTITY/50 END END BOXES,ISNULL(B.BALANCE,0) BALANCE " +
                         "FROM SC_CHANNELUSED A " +
                         "LEFT JOIN SC_BALANCE B ON A.CHANNELCODE=B.CHANNELCODE " +
                         "WHERE A.BATCHNO IN(SELECT TOP 1 BATCHNO FROM SC_BALANCE) " +
                         "ORDER BY A.BATCHNO,A.CHANNELCODE,A.CHANNELNAME";
            return ExecuteQuery(sql);
        }
        public DataTable FindChannelBalance()
        {
            string sql = "SELECT ORDERDATE, BATCHNO, CHANNELID, LINECODE, CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE, " +
                         "CHANNELCODE, CHANNELNAME, CIGARETTECODE, CIGARETTENAME, QUANTITY, QUANTITY1, QUANTITY2, QUANTITY3, QUANTITY4, QUANTITY5, QUANTITY6, QUANTITY7, QUANTITY8, QUANTITY9, BNO " +
                         "FROM SC_CHANNELBALANCE " +
                         "WHERE BATCHNO1 IN(SELECT TOP 1 BATCHNO FROM SC_ORDER_MASTER) " +
                         "ORDER BY BATCHNO,CHANNELCODE,CHANNELNAME";
            return ExecuteQuery(sql).Tables[0];
        }
        public DataTable FindAllChannelBalance()
        {
            string sql = @"SELECT CMD_CHANNEL.CHANNELCODE,(SELECT TOP 1 BATCHNO FROM SC_ORDER_MASTER) BATCHNO,ISNULL(TMP.QUANTITY,0) QUANTITY, ISNULL(QUANTITY1,0) QUANTITY1, ISNULL(QUANTITY2,0) QUANTITY2, ISNULL(QUANTITY3,0) QUANTITY3, 
                           ISNULL(QUANTITY4,0) QUANTITY4, ISNULL(QUANTITY5,0) QUANTITY5, ISNULL(QUANTITY6,0) QUANTITY6, ISNULL(QUANTITY7,0) QUANTITY7, 
                           ISNULL(QUANTITY8,0) QUANTITY8, ISNULL(QUANTITY9,0) QUANTITY9 
                           FROM CMD_CHANNEL LEFT JOIN
                            (SELECT BATCHNO,CHANNELCODE, CHANNELNAME, CIGARETTECODE, CIGARETTENAME, QUANTITY, 
                            QUANTITY1, QUANTITY2, QUANTITY3, 
                            QUANTITY4, QUANTITY5, QUANTITY6, QUANTITY7, QUANTITY8, QUANTITY9, BNO  
                            FROM SC_CHANNELBALANCE  
                            WHERE BATCHNO1 IN(SELECT TOP 1 BATCHNO FROM SC_ORDER_MASTER)) TMP ON CMD_CHANNEL.CHANNELCODE=TMP.CHANNELCODE  
                         WHERE CMD_CHANNEL.CHANNELTYPE='3'  
                         ORDER BY CHANNELCODE";
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 根据烟道代码查询烟道类型和烟道组号
        /// </summary>
        /// <param name="channelcode">烟道代码</param>
        /// <returns>返回烟道类型和烟道组号</returns>
        public DataTable FindChannelCode(string channelcode)
        {
            string sql = string.Format("SELECT CHANNELNAME,CHANNELADDRESS,CHANNELTYPE,CHANNELGROUP,CIGARETTECODE,CIGARETTENAME,LEDNO FROM SC_CHANNELUSED WHERE CHANNELCODE='{0}'", channelcode);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 根据烟道代码查询烟道类型和烟道组号
        /// </summary>
        /// <param name="channelcode">烟道代码</param>
        /// <returns>返回烟道类型和烟道组号</returns>
        public DataTable FindChannelCode(string channelType,int channelAddress)
        {
            string sql = string.Format("SELECT * FROM SC_CHANNELUSED WHERE CHANNELTYPE='{0}' AND CHANNELADDRESS={1}", channelType, channelAddress);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 根据流水号查询烟道信息
        /// </summary>
        /// <param name="sortNo">流水号</param>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回查询到的烟道信息表</returns>
        public DataTable FindChannelQuantity(string sortNo)
        {
            string sql = string.Format("SELECT *, REMAINQUANTITY / 50 BOXQUANTITY, REMAINQUANTITY % 50 ITEMQUANTITY " +
                                        " FROM (SELECT A.BATCHNO,A.CHANNELCODE,A.CHANNELNAME, A.LEDGROUP, A.LEDNO, " +
                                                " CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE," +
                                                " A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY,ISNULL(B.QUANTITY,0) SORTQUANTITY," +
                                                " A.QUANTITY - ISNULL(B.QUANTITY,0) REMAINQUANTITY," +
                                                " A.LED_X,A.LED_Y,A.LED_WIDTH,A.LED_HEIGHT,A.CHANNELADDRESS,A.CHANNELGROUP " +
                                                " FROM SC_CHANNELUSED A " +
                                                " LEFT JOIN(SELECT CHANNELCODE, SUM(QUANTITY) QUANTITY " +
                                                            " FROM SC_ORDER_DETAIL WHERE SORTNO <= '{0}' GROUP BY CHANNELCODE) B " +
                                                            " ON A.CHANNELCODE = B.CHANNELCODE WHERE A.BATCHNO IN (SELECT TOP 1 BATCHNO FROM SC_ORDER_DETAIL)) C  " +
                                                            " ORDER BY CHANNELCODE", sortNo);

            //sql = string.Format("SELECT *, REMAINQUANTITY / 50 BOXQUANTITY, REMAINQUANTITY % 50 ITEMQUANTITY " +
            //                            " FROM (SELECT A.BATCHNO,A.CHANNELCODE,A.CHANNELNAME, " +
            //                                    " CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE," +
            //                                    " A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY,ISNULL(B.QUANTITY,0) SORTQUANTITY," +
            //                                    " A.QUANTITY - ISNULL(B.QUANTITY,0) REMAINQUANTITY " +
            //                                    " FROM SC_CHANNELUSED A " +
            //                                    " LEFT JOIN(SELECT CHANNELCODE, SUM(QUANTITY) QUANTITY " +
            //                                                " FROM SC_ORDER_DETAIL WHERE SORTNO <= '{0}' GROUP BY CHANNELCODE) B " +
            //                                                " ON A.CHANNELCODE = B.CHANNELCODE WHERE A.BATCHNO IN (SELECT TOP 1 BATCHNO FROM SC_ORDER_DETAIL)) C  " +
            //                                                " ORDER BY CHANNELCODE", sortNo);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 根据流水号查询烟道信息
        /// </summary>
        /// <param name="sortNo">流水号</param>
        /// <param name="channelGroup">A线或者B线</param>
        /// <returns>返回查询到的烟道信息表</returns>
        public DataTable FindChannelQuantity(string sortNo, string channelType)
        {
            string sql = string.Format("SELECT *, REMAINQUANTITY / 50 BOXQUANTITY, REMAINQUANTITY % 50 ITEMQUANTITY "+
                                        " FROM (SELECT A.CHANNELNAME, A.LEDGROUP, A.LEDNO, "+
                                                " CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE," +
                                                " A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY,ISNULL(B.QUANTITY,0) SORTQUANTITY,"+
                                                " A.QUANTITY - ISNULL(B.QUANTITY,0) REMAINQUANTITY,"+
                                                " A.LED_X,A.LED_Y,A.LED_WIDTH,A.LED_HEIGHT,A.CHANNELADDRESS,A.CHANNELGROUP "+
                                                " FROM SC_CHANNELUSED A " +
                                                " LEFT JOIN(SELECT CHANNELCODE, SUM(QUANTITY) QUANTITY "+
                                                            " FROM SC_ORDER_DETAIL WHERE SORTNO <= '{0}' GROUP BY CHANNELCODE) B " +
                                                            " ON A.CHANNELCODE = B.CHANNELCODE WHERE A.BATCHNO IN (SELECT TOP 1 BATCHNO FROM SC_ORDER_DETAIL)) C  " +
                                                            " WHERE CHANNELTYPE = '{1}' ORDER BY CHANNELNAME ", sortNo, channelType=="2"?"立式机":"通道机");

            //sql = string.Format("SELECT *, REMAINQUANTITY / 50 BOXQUANTITY, REMAINQUANTITY % 50 ITEMQUANTITY " +
            //                            " FROM (SELECT A.CHANNELNAME, " +
            //                                    " CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE," +
            //                                    " A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY,ISNULL(B.QUANTITY,0) SORTQUANTITY," +
            //                                    " A.QUANTITY - ISNULL(B.QUANTITY,0) REMAINQUANTITY " +
            //                                    " FROM SC_CHANNELUSED A " +
            //                                    " LEFT JOIN(SELECT CHANNELCODE, SUM(QUANTITY) QUANTITY " +
            //                                                " FROM SC_ORDER_DETAIL WHERE SORTNO <= '{0}' GROUP BY CHANNELCODE) B " +
            //                                                " ON A.CHANNELCODE = B.CHANNELCODE WHERE A.BATCHNO IN (SELECT TOP 1 BATCHNO FROM SC_ORDER_DETAIL)) C  " +
            //                                                " WHERE CHANNELTYPE = '{1}' ORDER BY CHANNELNAME ", sortNo, channelType == "2" ? "立式机" : "通道机");
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 根据流水号查询烟道信息
        /// </summary>
        /// <param name="sortNo">A线流水号</param>
        /// <param name="channelGroup">B线流水号</param>
        /// <returns>返回查询到的烟道信息表</returns>
        public DataTable FindAllChannelQuantity(string sortNo)
        {
            string sql = string.Format(@"SELECT *, REMAINQUANTITY / 50 BOXQUANTITY, REMAINQUANTITY % 50 ITEMQUANTITY    
                                          FROM (SELECT A.BATCHNO,A.CHANNELCODE,A.CHANNELNAME, A.LEDGROUP, A.LEDNO,    
                                                  CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE,   
                                                  A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY,ISNULL(B.QUANTITY,0) SORTQUANTITY,   
                                                  A.QUANTITY - ISNULL(B.QUANTITY,0) REMAINQUANTITY,   
                                                  A.LED_X,A.LED_Y,A.LED_WIDTH,A.LED_HEIGHT,A.CHANNELADDRESS,A.CHANNELGROUP    
                                                  FROM SC_CHANNELUSED A    
                                                  LEFT JOIN(SELECT CHANNELCODE, SUM(QUANTITY) QUANTITY    
                                                              FROM SC_ORDER_DETAIL WHERE SORTNO <= '{0}' GROUP BY CHANNELCODE) B    
                                                              ON A.CHANNELCODE = B.CHANNELCODE WHERE A.BATCHNO IN (SELECT TOP 1 BATCHNO FROM SC_ORDER_DETAIL)) C ", sortNo);

//            sql = string.Format(@"SELECT *, REMAINQUANTITY / 50 BOXQUANTITY, REMAINQUANTITY % 50 ITEMQUANTITY    
//                                          FROM (SELECT A.BATCHNO,A.CHANNELCODE,A.CHANNELNAME,    
//                                                  CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE,   
//                                                  A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY,ISNULL(B.QUANTITY,0) SORTQUANTITY,   
//                                                  A.QUANTITY - ISNULL(B.QUANTITY,0) REMAINQUANTITY   
//                                                  FROM SC_CHANNELUSED A    
//                                                  LEFT JOIN(SELECT CHANNELCODE, SUM(QUANTITY) QUANTITY    
//                                                              FROM SC_ORDER_DETAIL WHERE SORTNO <= '{0}' GROUP BY CHANNELCODE) B    
//                                                              ON A.CHANNELCODE = B.CHANNELCODE WHERE A.BATCHNO IN (SELECT TOP 1 BATCHNO FROM SC_ORDER_DETAIL)) C ", sortNo);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 根据流水号查询烟道信息
        /// </summary>
        /// <param name="sortNo">A线流水号</param>
        /// <param name="channelGroup">B线流水号</param>
        /// <returns>返回查询到的烟道信息表</returns>
        public DataTable FindAllChannelQuantity(string sortNo_A, string sortNo_B)
        {
            string sql = string.Format(@"SELECT *, REMAINQUANTITY / 50 BOXQUANTITY, REMAINQUANTITY % 50 ITEMQUANTITY    
                                          FROM (SELECT A.BATCHNO,A.CHANNELCODE,A.CHANNELNAME, A.LEDGROUP, A.LEDNO,    
                                                  CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE,   
                                                  A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY,ISNULL(B.QUANTITY,0) SORTQUANTITY,   
                                                  A.QUANTITY - ISNULL(B.QUANTITY,0) REMAINQUANTITY,   
                                                  A.LED_X,A.LED_Y,A.LED_WIDTH,A.LED_HEIGHT,A.CHANNELADDRESS,A.CHANNELGROUP    
                                                  FROM SC_CHANNELUSED A    
                                                  LEFT JOIN(SELECT CHANNELCODE, SUM(QUANTITY) QUANTITY    
                                                              FROM SC_ORDER WHERE SORTNO <= '{0}' GROUP BY CHANNELCODE) B    
                                                              ON A.CHANNELCODE = B.CHANNELCODE) C     
                                          WHERE CHANNELGROUP =1
                                       union all
                                        SELECT *, REMAINQUANTITY / 50 BOXQUANTITY, REMAINQUANTITY % 50 ITEMQUANTITY    
                                          FROM (SELECT A.BATCHNO,A.CHANNELCODE,A.CHANNELNAME, A.LEDGROUP, A.LEDNO,    
                                                  CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE,   
                                                  A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY,ISNULL(B.QUANTITY,0) SORTQUANTITY,   
                                                  A.QUANTITY - ISNULL(B.QUANTITY,0) REMAINQUANTITY,   
                                                  A.LED_X,A.LED_Y,A.LED_WIDTH,A.LED_HEIGHT,A.CHANNELADDRESS,A.CHANNELGROUP    
                                                  FROM SC_CHANNELUSED A    
                                                  LEFT JOIN(SELECT CHANNELCODE, SUM(QUANTITY) QUANTITY    
                                                              FROM SC_ORDER WHERE SORTNO <= '{1}' GROUP BY CHANNELCODE) B    
                                                              ON A.CHANNELCODE = B.CHANNELCODE) C     
                                          WHERE CHANNELGROUP =2 ORDER BY BATCHNO,CHANNELCODE,CHANNELNAME", sortNo_A, sortNo_B);
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// 实时查询分拣烟道内所剩的卷烟数量
        /// </summary>
        /// <param name="sortNo">分拣流水号</param>
        /// <param name="channelGroup">烟道组号</param>
        /// <returns>返回查询到分拣烟道信息表</returns>
        public DataTable FindChannelRealtimeQuantity(string sortNo, string channelGroup)
        {
            string sql = string.Format(@"SELECT C.*,   
                                             (QUANTITY - SORTQUANTITY) AS NOSORTQUANTITY,            
                                             ( ISNULL(D.OUTQUANTITY,0) + HANDLESUPPLYQUANTITY) ALLQUANTITY,
                                             ((ISNULL(D.OUTQUANTITY,0) + HANDLESUPPLYQUANTITY) - SORTQUANTITY) AS REMAINQUANTITY,                                             
                                             ((ISNULL(D.OUTQUANTITY,0) + HANDLESUPPLYQUANTITY) - SORTQUANTITY) / 50 BOXQUANTITY, 
                                             ((ISNULL(D.OUTQUANTITY,0) + HANDLESUPPLYQUANTITY) - SORTQUANTITY) % 50 ITEMQUANTITY		                                     
                                        FROM (SELECT A.LINECODE,A.CHANNELGROUP,A.CHANNELCODE,A.CHANNELNAME,A.CHANNELADDRESS,
                                                CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机' ELSE '通道机' END CHANNELTYPE,
                                                CASE CHANNELTYPE WHEN '2' THEN A.QUANTITY % 50 WHEN '5' THEN A.QUANTITY ELSE A.QUANTITY % 50 END HANDLESUPPLYQUANTITY,
                                                A.CIGARETTECODE, A.CIGARETTENAME, A.QUANTITY,ISNULL(B.QUANTITY,0) SORTQUANTITY                                                
                                              FROM SC_CHANNELUSED A 
                                                LEFT JOIN(SELECT CHANNELCODE, SUM(QUANTITY) QUANTITY FROM SC_ORDER 
                                                            WHERE SORTNO <= '{0}' GROUP BY CHANNELCODE) B 
                                                ON A.CHANNELCODE = B.CHANNELCODE) C  
                                        LEFT JOIN CMD_CHANNEL_CHECK D ON C.CHANNELCODE=D.CHANNELCODE
                                        WHERE C.CHANNELGROUP = '{1}' ORDER BY C.CHANNELNAME", sortNo, channelGroup);
            return ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        ///  根据烟道组号和烟道类型查询出烟道信息表
        /// </summary>
        /// <param name="channelType">烟道类型</param>
        /// <param name="channelGroup">烟道组号</param>
        /// <returns>返回烟道信息表</returns>
        public DataTable FindEmptyChannel(string channelCode,string channelType,int channelGroup)
        {
            string sql = string.Format("SELECT CHANNELCODE, "+
                                        " CHANNELNAME + ' ' + CASE CHANNELTYPE WHEN '2' THEN '立式机' WHEN '5' THEN '立式机'  ELSE '通道机' END CHANNELNAME " +
                                        " FROM SC_CHANNELUSED "+
                                        " WHERE CHANNELTYPE IN ('{0}') AND CHANNELTYPE != '5' AND CHANNELGROUP = {1} AND CHANNELCODE != {2} " +
                                        " ORDER BY CHANNELNAME", channelType, channelGroup, channelCode);
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable FindChannel(string channelCode)
        {
            string sql = string.Format("SELECT * FROM SC_CHANNELUSED WHERE CHANNELCODE='{0}'", channelCode);
            return ExecuteQuery(sql).Tables[0];
        }
        public DataTable FindChannel(string batchNo,string channelCode)
        {
            string sql = string.Format("SELECT * FROM SC_CHANNELUSED WHERE BATCHNO='{0}' AND CHANNELCODE='{1}'", batchNo,channelCode);
            return ExecuteQuery(sql).Tables[0];
        }
        /// <summary>
        /// 查询空仓烟道信息
        /// </summary>
        /// <returns>返回空仓信息表</returns>
        public DataTable FindLastSortNo(int channelgroup)
        {
            string sql = string.Format("SELECT CHANNELADDRESS,CHANNELCODE,SORTNO FROM SC_CHANNELUSED WHERE CHANNELGROUP={0} ORDER BY CHANNELGROUP,CHANNELCODE",channelgroup);
            return ExecuteQuery(sql).Tables[0];
        }

        public void UpdateChannel(string channelCode, string cigaretteCode, string cigaretteName, int quantity, string sortNo)
        {
            string sql = string.Format("UPDATE SC_CHANNELUSED SET CIGARETTECODE='{0}', CIGARETTENAME='{1}', QUANTITY={2}, SORTNO={3} WHERE CHANNELCODE='{4}'",
                cigaretteCode, cigaretteName, quantity, sortNo, channelCode);
            ExecuteNonQuery(sql);
        }
        public void UpdateChannel(string channelCode, string cigaretteCode, string cigaretteName, int quantity, int groupNo,string sortNo)
        {
            string sql = string.Format("UPDATE SC_CHANNELUSED SET CIGARETTECODE='{0}', CIGARETTENAME='{1}', QUANTITY={2},QUANTITY={3},  SORTNO={4} WHERE CHANNELCODE='{5}'",
                cigaretteCode, cigaretteName, quantity, groupNo,sortNo, channelCode);
            ExecuteNonQuery(sql);
        }
        public void UpdateChannel(string batchNo,string channelCode, string cigaretteCode, string cigaretteName, int quantity, int groupNo, string sortNo)
        {
            string sql = string.Format("UPDATE SC_CHANNELUSED SET CIGARETTECODE='{0}', CIGARETTENAME='{1}', QUANTITY={2},GROUPNO={3},SORTNO={4} WHERE CHANNELCODE='{5}' AND BATCHNO='{6}'",
                cigaretteCode, cigaretteName, quantity, groupNo, sortNo, channelCode, batchNo);
            ExecuteNonQuery(sql);
        }
        public void InsertChannel(DataTable channelTable)
        {
            ExecuteQuery("TRUNCATE TABLE SC_SORT_STATUS");
            ExecuteQuery("TRUNCATE TABLE SC_SORT_EFFICIENCY");
            ExecuteQuery("TRUNCATE TABLE SC_CHANNELUSED");
            BatchInsert(channelTable, "SC_CHANNELUSED");
        }

        /// <summary>
        /// 将补货监控系统的出库数据插入分拣监控系统（已经过2号扫码器和已经过3号扫码器的卷烟）
        /// </summary>
        /// <param name="channelCheckTable"></param>
        public void InsertChannelCheck(DataTable channelCheckTable)
        {
            ExecuteQuery("TRUNCATE TABLE CMD_CHANNEL_CHECK");
            BatchInsert(channelCheckTable, "CMD_CHANNEL_CHECK");
        }
        public void InsertChannelBalance(string batchNo)
        {
            string sql = string.Format("DELETE FROM SC_CHANNELBALANCE WHERE BATCHNO='{0}'",batchNo);
            ExecuteNonQuery(sql);

            sql = string.Format("INSERT INTO SC_CHANNELBALANCE (ORDERDATE, BATCHNO, CHANNELID, LINECODE, CHANNELTYPE, CHANNELCODE, CHANNELNAME, CIGARETTECODE, CIGARETTENAME) " +
                                "SELECT ORDERDATE, BATCHNO, CHANNELID, LINECODE, CHANNELTYPE, CHANNELCODE, CHANNELNAME, CIGARETTECODE, CIGARETTENAME " +
                                "FROM SC_CHANNELUSED " +
                                "WHERE BATCHNO='{0}' AND ", batchNo);
            ExecuteNonQuery(sql);
        }
        public void InsertChannelBalance(int bNo,string firstBatchNo,string batchNo,string preBatchNo)
        {
            string sql = string.Format("DELETE FROM SC_CHANNELBALANCE WHERE BATCHNO='{0}'", firstBatchNo);
            //if(bNo==1)
            //    ExecuteNonQuery(sql);

            if (preBatchNo.Length<=0)
                ExecuteNonQuery(sql);

            sql = string.Format("INSERT INTO SC_CHANNELBALANCE (ORDERDATE, BATCHNO, CHANNELID, LINECODE, CHANNELTYPE, CHANNELCODE, CHANNELNAME, CIGARETTECODE, CIGARETTENAME,QUANTITY{0},BNO) " +
                                "SELECT ORDERDATE, '{1}', CHANNELID, LINECODE, CHANNELTYPE, CHANNELCODE, CHANNELNAME, CIGARETTECODE, CIGARETTENAME,BALANCE,{0} " +
                                "FROM SC_BALANCE " +
                                "WHERE BATCHNO='{2}' AND NOT EXISTS(SELECT * FROM SC_CHANNELBALANCE WHERE BATCHNO='{1}' AND CHANNELCODE=SC_BALANCE.CHANNELCODE)", bNo, firstBatchNo,batchNo);
            ExecuteNonQuery(sql);

            //如果有通道换了品牌也要更新


            sql = string.Format("UPDATE SC_CHANNELBALANCE SET QUANTITY{0}=0,BATCHNO1='{1}', CIGARETTECODE=SC_CHANNELUSED.CIGARETTECODE,CIGARETTENAME=SC_CHANNELUSED.CIGARETTENAME " +
                                "FROM SC_CHANNELBALANCE " +
                                "INNER JOIN SC_CHANNELUSED ON SC_CHANNELBALANCE.CHANNELCODE=SC_CHANNELUSED.CHANNELCODE " +
                                "WHERE SC_CHANNELBALANCE.BATCHNO='{2}' AND SC_CHANNELUSED.BATCHNO='{1}' ", bNo, batchNo, firstBatchNo);
            ExecuteNonQuery(sql);
            //更新新增加品规的余量
            //if (bNo > 1)
            //{
            //    int pno = bNo-1;
            //    //如果有新增加品牌,更新为仓库零条数量
            //    sql = string.Format("UPDATE SC_CHANNELBALANCE QUANTITY{0}=0 WHERE BNO={1}", pno, bNo);

            //    //处理立式机零条，假设仓库零条足够
            //    sql = string.Format("UPDATE SC_CHANNELBALANCE SET QUANTITY{0}=V_ORDER_BALANCE2.BALANCE " +
            //                        "FROM SC_CHANNELBALANCE " +
            //                        "INNER JOIN V_ORDER_BALANCE2 ON SC_CHANNELBALANCE.BATCHNO1=V_ORDER_BALANCE2.BATCHNO AND SC_CHANNELBALANCE.CHANNELCODE=V_ORDER_BALANCE2.CHANNELCODE " +
            //                        "WHERE SC_CHANNELBALANCE.BATCHNO1='{1}'", pno, batchNo);

            //    //更新上一批的余量，如果批次优化有不连续的情况，所以只能处理上一批的余量，而不能用pno

            //    ExecuteNonQuery(sql);
            //}
            //else if (bNo == 1)
            //{
            //    //处理立式机零条，假设仓库零条足够
            //    sql = string.Format("UPDATE SC_CHANNELBALANCE SET QUANTITY=V_ORDER_BALANCE2.BALANCE " +
            //                        "FROM SC_CHANNELBALANCE " +
            //                        "INNER JOIN V_ORDER_BALANCE2 ON SC_CHANNELBALANCE.BATCHNO1=V_ORDER_BALANCE2.BATCHNO AND SC_CHANNELBALANCE.CHANNELCODE=V_ORDER_BALANCE2.CHANNELCODE " +
            //                        "WHERE SC_CHANNELBALANCE.BATCHNO1='{0}'", batchNo);

            //    ExecuteNonQuery(sql);
            //}
            if (preBatchNo.Length > 0)
            {

                int pno = int.Parse(preBatchNo.Substring(10, 3));

                //处理立式机零条，假设仓库零条足够
                sql = string.Format("UPDATE SC_CHANNELBALANCE SET QUANTITY{0}=V_ORDER_BALANCE2.BALANCE " +
                                    "FROM SC_CHANNELBALANCE " +
                                    "INNER JOIN V_ORDER_BALANCE2 ON SC_CHANNELBALANCE.BATCHNO1=V_ORDER_BALANCE2.BATCHNO AND SC_CHANNELBALANCE.CHANNELCODE=V_ORDER_BALANCE2.CHANNELCODE " +
                                    "WHERE SC_CHANNELBALANCE.BATCHNO1='{1}'", pno, batchNo);

                //更新上一批的余量，如果批次优化有不连续的情况，所以只能处理上一批的余量，而不能用pno

                ExecuteNonQuery(sql);
            }
            else
            {
                //处理立式机零条，假设仓库零条足够
                sql = string.Format("UPDATE SC_CHANNELBALANCE SET QUANTITY=V_ORDER_BALANCE2.BALANCE " +
                                    "FROM SC_CHANNELBALANCE " +
                                    "INNER JOIN V_ORDER_BALANCE2 ON SC_CHANNELBALANCE.BATCHNO1=V_ORDER_BALANCE2.BATCHNO AND SC_CHANNELBALANCE.CHANNELCODE=V_ORDER_BALANCE2.CHANNELCODE " +
                                    "WHERE SC_CHANNELBALANCE.BATCHNO1='{0}'", batchNo);

                ExecuteNonQuery(sql);
            }           

        }
        public void UpdateChannelBalance(int bNo,string batchNo)
        {
            string sql = string.Format("UPDATE SC_CHANNELBALANCE SET QUANTITY{0} = B.BALANCE " +
                                       "FROM SC_CHANNELBALANCE A " +
                                       "INNER JOIN SC_BALANCE B ON A.ORDERDATE=B.ORDERDATE AND A.CHANNELCODE=B.CHANNELCODE " +
                                       "WHERE A.BATCHNO='{1}'", bNo,batchNo);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="channelTable"></param>
        public void UpdateChannelBalance(DataTable channelTable,int bNo)
        {
            foreach (DataRow channelRow in channelTable.Rows)
            {
                string sql = string.Format("UPDATE SC_CHANNELBALANCE SET QUANTITY{0}={1} WHERE CHANNELCODE='{2}'", bNo, channelRow["QUANTITY" + bNo.ToString()], channelRow["CHANNELCODE"]);
                ExecuteNonQuery(sql);                
            }
        }
        public void UpdateCigaretteName(string batchNo)
        {
            string sql = string.Format("UPDATE SC_CHANNELUSED SET PRODUCTNAME =CMD_Product.SHORTNAME " +
                                       "FROM SC_CHANNELUSED " +
                                       "INNER JOIN CMD_Product ON SC_CHANNELUSED.PRODUCTCODE=CMD_Product.PRODUCTCODE " +
                                       "WHERE BATCHNO='{0}'",batchNo);
            ExecuteNonQuery(sql);
        }
        public void ClearChannelBalance(int bNo, string batchNo)
        {
            string sql = string.Format("UPDATE SC_CHANNELBALANCE SET QUANTITY{0} = 0 " +
                                       "FROM SC_CHANNELBALANCE A " +                                       
                                       "WHERE A.BATCHNO='{1}'", bNo, batchNo);
            ExecuteNonQuery(sql);
        }
    }
}
