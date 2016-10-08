using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DB.Util;

namespace Sorting.Dispatching.Dao
{
    public class SupplyDao: BaseDao
    {
        string lineCode = "";
        int serialNo = 0;

        public void DeleteSupply()
        {
            string sql = "TRUNCATE TABLE SC_SUPPLY";
            ExecuteNonQuery(sql);
        }
        public void UpdateChannel(string sourceChannel, string targetChannel)
        {
            string sql = string.Format("UPDATE SC_SUPPLY SET CHANNELCODE='{0}' WHERE CHANNELCODE='{1}'", targetChannel, sourceChannel);
            ExecuteNonQuery(sql);
            sql = string.Format("UPDATE SC_SUPPLY2 SET CHANNELCODE='{0}' WHERE CHANNELCODE='{1}'", targetChannel, sourceChannel);
            ExecuteNonQuery(sql);
        }
        public void UpdateChannel(string batchNo,string sourceChannel, string targetChannel)
        {
            string sql = string.Format("UPDATE SC_SUPPLY SET CHANNELCODE='{0}' WHERE CHANNELCODE='{1}' AND BATCHNO='{2}'", targetChannel, sourceChannel, batchNo);
            ExecuteNonQuery(sql);
            sql = string.Format("UPDATE SC_SUPPLY2 SET CHANNELCODE='{0}' WHERE CHANNELCODE='{1}' AND BATCHNO='{2}'", targetChannel, sourceChannel, batchNo);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="supplyTable"></param>
        public void InsertSupply(DataTable supplyTable,bool isUseSerialNo)
        {
            DataRow[] rows = supplyTable.Select("", "BATCHNO,SERIALNO");
            int serialNo = 1;
            foreach (DataRow row in rows)
            {
                SqlCreate sqlCreate = new SqlCreate("SC_SUPPLY", SqlType.INSERT);
                sqlCreate.AppendQuote("ORDERDATE", row["ORDERDATE"]);
                sqlCreate.AppendQuote("BATCHNO", row["BATCHNO"]);
                sqlCreate.AppendQuote("LINECODE", row["LINECODE"]);
                sqlCreate.Append("SERIALNO", isUseSerialNo ? row["SERIALNO"] : serialNo++);
                
                sqlCreate.Append("ORIGINALSORTNO", row["SORTNO"]);
                sqlCreate.Append("SORTNO", row["SORTNO"]);

                sqlCreate.AppendQuote("ProductCode", row["ProductCode"]);
                sqlCreate.AppendQuote("ProductName", row["ProductName"]);

                sqlCreate.AppendQuote("CHANNELCODE", row["CHANNELCODE"]);
                sqlCreate.Append("CHANNELGROUP", row["CHANNELGROUP"]);
                //sqlCreate.Append("GROUPNO", row["GROUPNO"]);
                sqlCreate.AppendQuote("BARCODE", row["BARCODE"]);
                sqlCreate.AppendQuote("ISBALANCE", row["ISBALANCE"]);
                ExecuteNonQuery(sqlCreate.GetSQL());
            }
        }
        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="supplyTable"></param>
        public void InsertSupply2(DataTable supplyTable, bool isUseSerialNo)
        {
            DataRow[] rows = supplyTable.Select("", "BATCHNO,SERIALNO");
            int serialNo = 1;
            foreach (DataRow row in rows)
            {
                SqlCreate sqlCreate = new SqlCreate("SC_SUPPLY2", SqlType.INSERT);
                sqlCreate.AppendQuote("ORDERDATE", row["ORDERDATE"]);
                sqlCreate.AppendQuote("BATCHNO", row["BATCHNO"]);
                sqlCreate.AppendQuote("LINECODE", row["LINECODE"]);
                sqlCreate.Append("SERIALNO", isUseSerialNo ? row["SERIALNO"] : serialNo++);

                sqlCreate.Append("ORIGINALSORTNO", row["SORTNO"]);
                sqlCreate.Append("SORTNO", row["SORTNO"]);

                sqlCreate.AppendQuote("ProductCode", row["ProductCode"]);
                sqlCreate.AppendQuote("ProductName", row["ProductName"]);

                sqlCreate.AppendQuote("CHANNELCODE", row["CHANNELCODE"]);
                sqlCreate.Append("CHANNELGROUP", row["CHANNELGROUP"]);
                //sqlCreate.Append("GROUPNO", row["GROUPNO"]);
                sqlCreate.AppendQuote("BARCODE", row["BARCODE"]);
                sqlCreate.AppendQuote("ISBALANCE", row["ISBALANCE"]);

                ExecuteNonQuery(sqlCreate.GetSQL());
            }
        }
        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="supplyTable"></param>
        /// <param name="orderDate"></param>
        /// <param name="lineCode"></param>
        public void InsertSupply(DataTable supplyTable, string orderDate, string lineCode)
        {
            if (this.lineCode != lineCode)
            {
                this.lineCode = lineCode;
                string sql = string.Format("SELECT CASE WHEN MAX(SERIALNO) IS NULL THEN 1000 ELSE MAX(SERIALNO) END  FROM SC_SUPPLY WHERE ORDERDATE='{0}' AND LINECODE='{1}'", orderDate, lineCode);
                serialNo = Convert.ToInt32(ExecuteScalar(sql));
            }

            foreach (DataRow row in supplyTable.Rows)
            {
                SqlCreate sqlCreate = new SqlCreate("SC_SUPPLY", SqlType.INSERT);
                sqlCreate.AppendQuote("ORDERDATE", row["ORDERDATE"]);
                sqlCreate.Append("BATCHNO", row["BATCHNO"]);
                sqlCreate.AppendQuote("LINECODE", row["LINECODE"]);
                sqlCreate.Append("SORTNO", row["SORTNO"]);

                sqlCreate.Append("SERIALNO", serialNo++);                
                sqlCreate.Append("ORIGINALSORTNO", row["SORTNO"]);               
                
                sqlCreate.AppendQuote("ProductCode", row["ProductCode"]);
                sqlCreate.AppendQuote("ProductName", row["ProductName"]);

                sqlCreate.AppendQuote("CHANNELCODE", row["CHANNELCODE"]);
                sqlCreate.Append("CHANNELGROUP", row["CHANNELGROUP"]);
                sqlCreate.Append("GROUPNO", row["GROUPNO"]);

                ExecuteNonQuery(sqlCreate.GetSQL());
            }
        }

        public void InsertSupply(DataTable supplyTable, string lineCode, string orderDate, string batchNo)
        {
            if (this.lineCode != lineCode)
            {
                this.lineCode = lineCode;
                string sql = string.Format("SELECT CASE WHEN MAX(SERIALNO) IS NULL THEN 1000 ELSE MAX(SERIALNO) END  FROM SC_SUPPLY WHERE LINECODE='{0}' AND ORDERDATE = '{1}' AND BATCHNO = '{2}' ", lineCode, orderDate, batchNo);
                serialNo = Convert.ToInt32(ExecuteScalar(sql));
            }

            foreach (DataRow row in supplyTable.Rows)
            {
                SqlCreate sqlCreate = new SqlCreate("SC_SUPPLY", SqlType.INSERT);
                sqlCreate.AppendQuote("ORDERDATE", row["ORDERDATE"]);
                sqlCreate.AppendQuote("BATCHNO", row["BATCHNO"]);
                sqlCreate.Append("SERIALNO", serialNo++);
                sqlCreate.AppendQuote("LINECODE", row["LINECODE"]);
                sqlCreate.Append("ORIGINALSORTNO", row["SORTNO"]);
                sqlCreate.Append("SORTNO", row["SORTNO"]);
                sqlCreate.Append("GROUPNO", row["GROUPNO"]);
                sqlCreate.Append("CHANNELGROUP", row["CHANNELGROUP"]);
                sqlCreate.AppendQuote("CHANNELCODE", row["CHANNELCODE"]);
                sqlCreate.AppendQuote("ProductCode", row["ProductCode"]);
                sqlCreate.AppendQuote("ProductName", row["ProductName"]);
                ExecuteNonQuery(sqlCreate.GetSQL());
            }
        }
        public void InsertBalanceSupply(string lineCode, string orderDate, string batchNo)
        {
            //插入立式机零条
            string sql = @"INSERT INTO SC_BALANCE(ORDERDATE, BATCHNO, LINECODE, CHANNELID, CHANNELCODE, CHANNELNAME, CHANNELTYPE, 
                           CHANNELORDER, ProductCode,ProductName, QUANTITY, BALANCE,BARCODE)
                           SELECT  B2.ORDERDATE, B2.BATCHNO, B2.LINECODE, C2.CHANNELID,B2.CHANNELCODE,C2.CHANNELNAME, '2' CHANNELTYPE,B2.CHANNELORDER,
                           B2.ProductCode, B2.ProductName, B2.QUANTITY, B2.BALANCE, B2.BARCODE
                           FROM V_ORDER_BALANCE2 B2 INNER JOIN CMD_CHANNEL C2 ON B2.CHANNELCODE=C2.CHANNELCODE
                           WHERE B2.ORDERDATE='{0}' AND B2.BATCHNO='{1}' AND B2.LINECODE='{2}' AND B2.BALANCE>0";
            sql = string.Format(sql, orderDate, batchNo, lineCode);
            ExecuteNonQuery(sql);

            //插入通道机零条
            sql = @"INSERT INTO SC_BALANCE(ORDERDATE, BATCHNO, LINECODE, CHANNELID, CHANNELCODE, CHANNELNAME, CHANNELTYPE, 
                    CHANNELORDER, ProductCode,ProductName, QUANTITY, BALANCE,BARCODE)
                    SELECT  B3.ORDERDATE, B3.BATCHNO, B3.LINECODE, C3.CHANNELID,B3.CHANNELCODE,C3.CHANNELNAME, '3' CHANNELTYPE,B3.CHANNELORDER,
                    B3.ProductCode, B3.ProductName, B3.QUANTITY, B3.BALANCE, B3.BARCODE
                    FROM V_ORDER_BALANCE3 B3 INNER JOIN CMD_CHANNEL C3 ON B3.CHANNELCODE=C3.CHANNELCODE
                    WHERE B3.ORDERDATE='{0}' AND B3.BATCHNO='{1}' AND B3.LINECODE='{2}' AND B3.BALANCE>0";
            sql = string.Format(sql, orderDate, batchNo, lineCode);
            ExecuteNonQuery(sql);
        }
        public void AdjustSortNo(string lineCode, int aheadCount,string orderDate, string batchNo)
        {
            //aheadCount = aheadCount + 1000;
            //string sql = string.Format("UPDATE SC_SUPPLY SET SORTNO = CASE WHEN SERIALNO <= {0} THEN 1 ELSE ORIGINALSORTNO - (SELECT MAX(ORIGINALSORTNO) FROM SC_SUPPLY WHERE SERIALNO <= {0} AND LINECODE='{1}' AND ORDERDATE = '{2}' AND BATCHNO = {3}) + 1 END WHERE LINECODE='{1}' AND ORDERDATE = '{2}' AND BATCHNO = {3}", aheadCount, lineCode, orderDate, batchNo);
        }

        public void AdjustSortNo(string lineCode, int aheadCount)
        {
            string sql = string.Format("UPDATE SC_SUPPLY SET SORTNO=CASE WHEN ORIGINALSORTNO<={0} THEN 1 ELSE ORIGINALSORTNO - {0} END WHERE LINECODE='{1}'", aheadCount, lineCode);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void DeleteSchedule(string batchNo)
        {
            string sql = string.Format("DELETE FROM SC_SUPPLY WHERE BATCHNO='{0}';DELETE FROM SC_SUPPLY2 WHERE BATCHNO='{0}'", batchNo);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void DeleteSchedule(string orderDate, string batchNo)
        {
            string sql = string.Format("DELETE FROM SC_SUPPLY WHERE ORDERDATE = '{0}' AND BATCHNO='{1}'", orderDate, batchNo);
            ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        public void DeleteHistory(string orderDate)
        {
            string sql = string.Format("DELETE FROM SC_SUPPLY WHERE ORDERDATE < '{0}'", orderDate);
            ExecuteNonQuery(sql);
        }


        internal void AdjustSortNo1(string orderDate, string batchNo, string lineCode, int aheadCount1, int aheadCount2)
        {
            string sql = "UPDATE SC_SUPPLY SET SORTNO = 1 " +
                            " WHERE ORDERDATE= '{0}' AND BATCHNO = '{1}' AND LINECODE = '{2}'" +
                            " AND SERIALNO IN " +
                            " (" +
                            " 	SELECT TOP {3} SERIALNO FROM SC_SUPPLY A" +
                            " 	LEFT JOIN CMD_CHANNEL B " +
                            " 	ON A.LINECODE = B.LINECODE" +
                            " 	AND A.CHANNELCODE = B.CHANNELCODE" +
                            " 	WHERE A.ORDERDATE= '{0}' AND A.BATCHNO = '{1}' AND A.LINECODE = '{2}'" +
                            " 	AND A.ORIGINALSORTNO != 0	" +
                            " 	AND A.CHANNELGROUP = '{4}'	" +
                            " 	AND B.CHANNELTYPE = '{5}'" +
                            " 	ORDER BY SERIALNO" +
                            " )  " +

                            " UPDATE SC_SUPPLY SET SORTNO = ORIGINALSORTNO + 1 -" +
                            " (" +
                            " 	SELECT MAX(ORIGINALSORTNO) FROM SC_SUPPLY" +
                            " 	WHERE ORDERDATE= '{0}' AND BATCHNO = '{1}' AND LINECODE = '{2}' " +
                            " 	AND SERIALNO IN " +
                            " 	(" +
                            " 		SELECT TOP {3} SERIALNO FROM SC_SUPPLY A" +
                            " 		LEFT JOIN CMD_CHANNEL B " +
                            " 		ON A.LINECODE = B.LINECODE" +
                            " 		AND A.CHANNELCODE = B.CHANNELCODE" +
                            " 		WHERE A.ORDERDATE= '{0}' AND A.BATCHNO = '{1}' AND A.LINECODE = '{2}'" +
                            " 		AND A.ORIGINALSORTNO != 0" +
                            " 		AND A.CHANNELGROUP = '{4}'" +
                            " 		AND B.CHANNELTYPE = '{5}'" +
                            " 		ORDER BY SERIALNO" +
                            " 	)" +
                            " )" +
                            " WHERE ORDERDATE= '{0}' AND BATCHNO = '{1}' AND LINECODE = '{2}'" +
                            " AND SERIALNO IN " +
                            " (" +
                            " 	SELECT SERIALNO " +
                            " 	FROM SC_SUPPLY A " +
                            " 	LEFT JOIN CMD_CHANNEL B " +
                            " 	ON A.LINECODE = B.LINECODE " +
                            " 	AND A.CHANNELCODE = B.CHANNELCODE" +
                            " 	WHERE A.ORDERDATE= '{0}' AND A.BATCHNO = '{1}' AND A.LINECODE = '{2}'" +
                            " 	AND A.ORIGINALSORTNO != 0 " +
                            " 	AND A.SORTNO != 1 	" +
                            " 	AND A.CHANNELGROUP = '{4}'" +
                            " 	AND B.CHANNELTYPE = '{5}'" +
                            " )";

            ExecuteNonQuery(string.Format(sql, orderDate, batchNo, lineCode, aheadCount1, 1, 2));
            ExecuteNonQuery(string.Format(sql, orderDate, batchNo, lineCode, aheadCount2, 1, 3));
            ExecuteNonQuery(string.Format(sql, orderDate, batchNo, lineCode, aheadCount1, 2, 2));
            ExecuteNonQuery(string.Format(sql, orderDate, batchNo, lineCode, aheadCount2, 2, 3));
        }

        internal void AdjustSortNo(string orderDate, string batchNo, string lineCode, int channelGroup, int channelType, int aheadCount)
        {
            string sql = "UPDATE SC_SUPPLY SET SORTNO = 0 " +
                " WHERE ORDERDATE= '{0}' AND BATCHNO = '{1}' AND LINECODE = '{2}'" +
                " AND SERIALNO IN " +
                " (" +
                " 	SELECT TOP {3} SERIALNO FROM SC_SUPPLY A" +
                " 	LEFT JOIN CMD_CHANNEL B " +
                " 	ON A.LINECODE = B.LINECODE" +
                " 	AND A.CHANNELCODE = B.CHANNELCODE" +
                " 	WHERE A.ORDERDATE= '{0}' AND A.BATCHNO = '{1}' AND A.LINECODE = '{2}'" +
                " 	AND A.ORIGINALSORTNO != 0	" +
                " 	AND A.CHANNELGROUP = '{4}'	" +
                " 	AND B.CHANNELTYPE = '{5}'" +
                " 	ORDER BY SERIALNO" +
                " )  " +

                " UPDATE SC_SUPPLY SET SORTNO = ORIGINALSORTNO + 1 -" +
                " (" +
                " 	SELECT MAX(ORIGINALSORTNO) FROM SC_SUPPLY" +
                " 	WHERE ORDERDATE= '{0}' AND BATCHNO = '{1}' AND LINECODE = '{2}' " +
                " 	AND SERIALNO IN " +
                " 	(" +
                " 		SELECT TOP {3} SERIALNO FROM SC_SUPPLY A" +
                " 		LEFT JOIN CMD_CHANNEL B " +
                " 		ON A.LINECODE = B.LINECODE" +
                " 		AND A.CHANNELCODE = B.CHANNELCODE" +
                " 		WHERE A.ORDERDATE= '{0}' AND A.BATCHNO = '{1}' AND A.LINECODE = '{2}'" +
                " 		AND A.ORIGINALSORTNO != 0" +
                " 		AND A.CHANNELGROUP = '{4}'" +
                " 		AND B.CHANNELTYPE = '{5}'" +
                " 		ORDER BY SERIALNO" +
                " 	)" +
                " )" +
                " WHERE ORDERDATE= '{0}' AND BATCHNO = '{1}' AND LINECODE = '{2}'" +
                " AND SERIALNO IN " +
                " (" +
                " 	SELECT SERIALNO " +
                " 	FROM SC_SUPPLY A " +
                " 	LEFT JOIN CMD_CHANNEL B " +
                " 	ON A.LINECODE = B.LINECODE " +
                " 	AND A.CHANNELCODE = B.CHANNELCODE" +
                " 	WHERE A.ORDERDATE= '{0}' AND A.BATCHNO = '{1}' AND A.LINECODE = '{2}'" +
                " 	AND A.ORIGINALSORTNO != 0 " +
                " 	AND A.SORTNO != 0 " +
                " 	AND A.CHANNELGROUP = '{4}'" +
                " 	AND B.CHANNELTYPE = '{5}'" +
                " 	AND B.CHANNELTYPE = '2'" +
                " )";

            ExecuteNonQuery(string.Format(sql, orderDate, batchNo, lineCode, aheadCount, channelGroup, channelType));
        }
        public void ClearSupply()
        {
            ExecuteNonQuery("DELETE FROM SC_SUPPLYH WHERE BATCHNO IN(SELECT DISTINCT BATCHNO FROM SC_SUPPLY)");
            ExecuteNonQuery("DELETE FROM SC_SUPPLY2H WHERE BATCHNO IN(SELECT DISTINCT BATCHNO FROM SC_SUPPLY2)");
            ExecuteNonQuery("DELETE FROM SC_BALANCEH WHERE BATCHNO IN(SELECT DISTINCT BATCHNO FROM SC_BALANCE)");

            ExecuteNonQuery("INSERT INTO SC_SUPPLYH SELECT * FROM SC_SUPPLY");
            ExecuteNonQuery("INSERT INTO SC_SUPPLY2H SELECT * FROM SC_SUPPLY2");
            ExecuteNonQuery("INSERT INTO SC_BALANCEH SELECT * FROM SC_BALANCE");

            ExecuteNonQuery("TRUNCATE TABLE SC_SUPPLY");
            ExecuteNonQuery("TRUNCATE TABLE SC_SUPPLY2");
            ExecuteNonQuery("TRUNCATE TABLE SC_BALANCE");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public int GetMaxSerialNO(string batchNo)
        {
            string sql = string.Format("SELECT TOP 1 SERIALNO FROM SC_SUPPLY WHERE BATCHNO='{0}' ORDER BY SERIALNO DESC", batchNo);
            return int.Parse(ExecuteScalar(sql).ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public int GetMaxSerialNO2(string batchNo)
        {
            string sql = string.Format("SELECT TOP 1 SERIALNO FROM SC_SUPPLY2 WHERE BATCHNO='{0}' ORDER BY SERIALNO DESC", batchNo);
            return int.Parse(ExecuteScalar(sql).ToString());
        }
        public void UpdateStockChannelUsed(string batchNo, string sourceChannel, string targetChannel)
        {
            string sql = string.Format("UPDATE SC_STOCKCHANNELUSED SET TOCHANNELCODE='{0}' WHERE TOCHANNELCODE='{1}' AND BATCHNO='{2}'", targetChannel, sourceChannel, batchNo);
            ExecuteNonQuery(sql);
            
        }
        public void UpdateStockChannel(string batchNo, string sourceChannel, string targetChannel)
        {
            string sql = string.Format("UPDATE CMD_STOCKCHANNEL SET TOCHANNELCODE='{0}' WHERE TOCHANNELCODE='{1}'", targetChannel, sourceChannel);
            ExecuteNonQuery(sql);

        }
    }
}
