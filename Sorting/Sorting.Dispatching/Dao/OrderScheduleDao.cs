using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DB.Util;

namespace Sorting.Dispatching.Dao
{
    public class OrderScheduleDao : BaseDao
    {
        public DataTable FindMasterAll(int startRecord, int pageSize, string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT * FROM SC_PALLETMASTER " + where;
            sql += " ORDER BY ORDERDATE,BATCHNO,LINECODE,SORTNO";
            return ExecuteQuery(sql, "SC_PALLETMASTER", startRecord, pageSize).Tables[0];
        }

        public int FindMasterCount(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT COUNT(*) FROM SC_PALLETMASTER " + where;
            return (int)ExecuteScalar(sql);
        }

        public DataTable FindDetailAll(int startRecord, int pageSize, string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT * FROM SC_ORDER " + where;
            sql += " ORDER BY CHANNELCODE";
            return ExecuteQuery(sql, "SC_ORDER", startRecord, pageSize).Tables[0];
        }

        public int FindDetailCount(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT COUNT(*) FROM SC_ORDER " + where;
            return (int)ExecuteScalar(sql);
        }

        public DataTable FindLineAll(int startRecord, int pageSize, string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            string sql = "SELECT ORDERDATE,BATCHNO,LINECODE,CIGARETTECODE,CIGARETTENAME, SUM(QUANTITY) QUANTITY, SUM(QUANTITY)/50 JQUANTITY, SUM(QUANTITY)%50 TQUANTITY FROM SC_ORDER " + where;
            sql += " GROUP BY ORDERDATE,BATCHNO,LINECODE,CIGARETTECODE,CIGARETTENAME ORDER BY ORDERDATE,BATCHNO,LINECODE";
            return ExecuteQuery(sql, "SC_ORDER", startRecord, pageSize).Tables[0];
        }

        public int FindLineCount(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT COUNT(*) FROM "+
                "(SELECT ORDERDATE,BATCHNO,LINECODE,CIGARETTECODE,CIGARETTENAME, SUM(QUANTITY) QUANTITY, SUM(QUANTITY)/50 JQUANTITY, SUM(QUANTITY)%50 TQUANTITY FROM SC_ORDER " +
                "GROUP BY ORDERDATE,BATCHNO,LINECODE,CIGARETTECODE,CIGARETTENAME) A" + where;
            return (int)ExecuteScalar(sql);
        }

        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="ds"></param>
        public void SaveOrder(DataSet ds)
        {
            SaveOrderMaster(ds.Tables["MASTER"], "SC_PALLETMASTER");
            SaveOrderSchedule(ds.Tables["DETAIL"], "SC_ORDER");
        }

        //public void SaveOrder(DataTable orderTable)
        //{
        //    foreach (DataRow orderRow in orderTable.Rows)
        //    {
        //        SqlCreate sql = new SqlCreate("SC_ORDER", SqlType.INSERT);
        //        sql.AppendQuote("ORDERDATE", orderRow["ORDERDATE"]);
        //        sql.Append("BATCHNO", orderRow["BATCHNO"]);
        //        sql.AppendQuote("LINECODE", orderRow["LINECODE"]);
        //        sql.Append("SORTNO", orderRow["SORTNO"]);
        //        sql.Append("PALLETNO", orderRow["PALLETNO"]);

        //        sql.AppendQuote("CHANNELCODE", orderRow["CHANNELCODE"]);
        //        sql.AppendQuote("CIGARETTECODE", orderRow["CIGARETTECODE"]);

        //        sql.AppendQuote("CIGARETTENAME", orderRow["CIGARETTENAME"]);
        //        sql.Append("QUANTITY", orderRow["QUANTITY"]);

        //        ExecuteNonQuery(sql.GetSQL());
        //    }
        //}

        public void SaveOrderMaster(DataTable masterTable)
        {
            foreach (DataRow orderRow in masterTable.Rows)
            {
                SqlCreate sql = new SqlCreate("SC_PALLETMASTER", SqlType.INSERT);
                sql.AppendQuote("ORDERDATE", orderRow["ORDERDATE"]);
                sql.Append("BATCHNO", orderRow["BATCHNO"]);
                sql.AppendQuote("LINECODE", orderRow["LINECODE"]);
                sql.Append("SORTNO", orderRow["SORTNO"]);
                sql.AppendQuote("ORDERID", orderRow["ORDERID"]);

                sql.AppendQuote("AREACODE", orderRow["AREACODE"]);
                sql.AppendQuote("AREANAME", orderRow["AREANAME"]);

                sql.AppendQuote("ROUTECODE", orderRow["ROUTECODE"]);
                sql.AppendQuote("ROUTENAME", orderRow["ROUTENAME"]);

                sql.AppendQuote("CUSTOMERCODE", orderRow["CUSTOMERCODE"]);
                sql.AppendQuote("CUSTOMERNAME", orderRow["CUSTOMERNAME"]);

                sql.AppendQuote("ADDRESS", orderRow["ADDRESS"]);
                sql.AppendQuote("ORDERNO", orderRow["ORDERNO"]);

                sql.Append("QUANTITY", orderRow["QUANTITY"]);
                sql.Append("ABNORMITY_QUANTITY", orderRow["ABNORMITY_QUANTITY"]);

                ExecuteNonQuery(sql.GetSQL());
            }
        }

        public void SaveOrderSchedule(DataTable orderTable)
        {
            foreach (DataRow orderRow in orderTable.Rows)
            {
                SqlCreate sql = new SqlCreate("SC_ORDER", SqlType.INSERT);
                sql.Append("SORTNO", orderRow["SORTNO"]);
                sql.AppendQuote("LINECODE", orderRow["LINECODE"]);
                sql.AppendQuote("BATCHNO", orderRow["BATCHNO"]);
                sql.AppendQuote("ORDERID", orderRow["ORDERID"]);
                sql.Append("ORDERNO", 1);
                sql.AppendQuote("ORDERDATE", orderRow["ORDERDATE"]);
                sql.AppendQuote("CIGARETTECODE", orderRow["CIGARETTECODE"]);
                sql.AppendQuote("CIGARETTENAME", orderRow["CIGARETTENAME"]);
                sql.AppendQuote("CHANNELCODE", orderRow["CHANNELCODE"]);
                sql.Append("QUANTITY", orderRow["QUANTITY"]);
                ExecuteNonQuery(sql.GetSQL());
            }
        }

        /// <summary>
        /// 2010-11-21 todo
        /// </summary>
        /// <param name="masterTable"></param>
        /// <param name="tableName"></param>
        public void SaveOrderMaster(DataTable masterTable, string tableName)
        {
            foreach (DataRow orderRow in masterTable.Rows)
            {
                SqlCreate sql = new SqlCreate(tableName, SqlType.INSERT);

                sql.AppendQuote("ORDERDATE", orderRow["ORDERDATE"]);
                sql.Append("BATCHNO", orderRow["BATCHNO"]);
                sql.AppendQuote("LINECODE", orderRow["LINECODE"]);
                sql.Append("SORTNO", orderRow["SORTNO"]);

                sql.AppendQuote("ORDERID", orderRow["ORDERID"]);
                sql.AppendQuote("AREACODE", orderRow["AREACODE"]);
                sql.AppendQuote("AREANAME", orderRow["AREANAME"]);
                sql.AppendQuote("ROUTECODE", orderRow["ROUTECODE"]);
                sql.AppendQuote("ROUTENAME", orderRow["ROUTENAME"]);
                sql.AppendQuote("CUSTOMERCODE", orderRow["CUSTOMERCODE"]);
                sql.AppendQuote("CUSTOMERNAME", orderRow["CUSTOMERNAME"]);

                sql.AppendQuote("LICENSENO", orderRow["LICENSENO"]);
                sql.AppendQuote("ADDRESS", orderRow["ADDRESS"]);
                sql.AppendQuote("CUSTOMERSORTNO", orderRow["CUSTOMERSORTNO"]);
                sql.AppendQuote("ORDERNO", orderRow["ORDERNO"]);

                sql.Append("QUANTITY", orderRow["QUANTITY"]);
                sql.Append("QUANTITY1", orderRow["QUANTITY1"]);

                sql.Append("ABNORMITY_QUANTITY", orderRow["ABNORMITY_QUANTITY"]);

                sql.Append("EXPORTNO", orderRow["EXPORTNO"]);
                sql.Append("EXPORTNO1", orderRow["EXPORTNO1"]);
                sql.AppendQuote("PACKNO", orderRow["PACKNO"]);
                sql.AppendQuote("PACKNO1", orderRow["PACKNO1"]);

                ExecuteNonQuery(sql.GetSQL());
            }
        }

        /// <summary>
        /// 2010-11-21 todo
        /// </summary>
        /// <param name="orderTable"></param>
        /// <param name="tableName"></param>
        public void SaveOrderSchedule(DataTable orderTable, string tableName)
        {
            foreach (DataRow orderRow in orderTable.Rows)
            {
                SqlCreate sql = new SqlCreate(tableName, SqlType.INSERT);
                sql.Append("SORTNO", orderRow["SORTNO"]);
                sql.AppendQuote("LINECODE", orderRow["LINECODE"]);
                sql.AppendQuote("BATCHNO", orderRow["BATCHNO"]);
                sql.AppendQuote("ORDERID", orderRow["ORDERID"]);
                sql.Append("ORDERNO", orderRow["ORDERNO"]);
                sql.AppendQuote("ORDERDATE", orderRow["ORDERDATE"]);
                sql.AppendQuote("CIGARETTECODE", orderRow["CIGARETTECODE"]);
                sql.AppendQuote("CIGARETTENAME", orderRow["CIGARETTENAME"]);
                sql.AppendQuote("CHANNELCODE", orderRow["CHANNELCODE"]);
                sql.Append("QUANTITY", orderRow["QUANTITY"]);
                sql.Append("CHANNELGROUP", orderRow["CHANNELGROUP"]);
                sql.Append("CHANNELORDER", orderRow["CHANNELORDER"]);
                sql.Append("EXPORTNO", orderRow["EXPORTNO"]);
                sql.AppendQuote("PACKNO", orderRow["PACKNO"]);
                ExecuteNonQuery(sql.GetSQL());
            }
        }
        /// <summary>
        /// 2010-11-21 todo
        /// </summary>
        /// <param name="orderTable"></param>
        /// <param name="tableName"></param>
        public void SaveOrderDetail(DataTable orderTable, string tableName)
        {
            foreach (DataRow orderRow in orderTable.Rows)
            {
                SqlCreate sql = new SqlCreate(tableName, SqlType.INSERT);
                sql.Append("SORTNO", orderRow["SORTNO"]);
                sql.AppendQuote("LINECODE", orderRow["LINECODE"]);
                sql.AppendQuote("BATCHNO", orderRow["BATCHNO"]);
                sql.AppendQuote("ORDERID", orderRow["ORDERID"]);
                sql.Append("ORDERNO", orderRow["ORDERNO"]);
                sql.AppendQuote("ORDERDATE", orderRow["ORDERDATE"]);
                sql.AppendQuote("PRODUCTCODE", orderRow["PRODUCTCODE"]);
                sql.AppendQuote("PRODUCTNAME", orderRow["PRODUCTNAME"]);
                sql.AppendQuote("CHANNELCODE", orderRow["CHANNELCODE"]);
                sql.Append("QUANTITY", orderRow["QUANTITY"]);
                sql.Append("CHANNELGROUP", orderRow["CHANNELGROUP"]);
                sql.Append("CHANNELORDER", orderRow["CHANNELORDER"]);
                sql.Append("EXPORTNO", orderRow["EXPORTNO"]);
                sql.AppendQuote("PACKNO", orderRow["PACKNO"]);
                ExecuteNonQuery(sql.GetSQL());
            }
        }
        public DataSet FindOrder(string orderDate, string batchNo, string lineCode)
        {
            string sql = "SELECT * FROM SC_ORDER WHERE ORDERDATE = '{0}' AND BATCHNO = '{1}' AND LINECODE = '{2}' ORDER BY CHANNELCODE";
            return ExecuteQuery(string.Format(sql, orderDate, batchNo, lineCode));
        }

        public DataSet FindOrder2(string orderDate, string batchNo, string lineCode)
        {
            string sql = "SELECT * FROM SC_ORDER WHERE ORDERDATE = '{0}' AND BATCHNO = '{1}' AND LINECODE = '{2}' ORDER BY SORTNO";
            return ExecuteQuery(string.Format(sql, orderDate, batchNo, lineCode));
        }

        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        public void DeleteHistory(string orderDate)
        {
            string sql = string.Format("DELETE FROM SC_ORDER_DETAIL WHERE ORDERDATE < '{0}'", orderDate);
            ExecuteNonQuery(sql);

            sql = string.Format("DELETE FROM SC_ORDER_MASTER WHERE ORDERDATE < '{0}'", orderDate);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void DeleteSchedule(string batchNo)
        {
            string sql = string.Format("DELETE FROM SC_ORDER_DETAIL WHERE BATCHNO = '{0}'", batchNo);
            ExecuteNonQuery(sql);

            sql = string.Format("DELETE FROM SC_ORDER_MASTER WHERE BATCHNO = '{0}'", batchNo);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void DeleteSchedule(string orderDate, string batchNo)
        {
            string sql = string.Format("DELETE FROM SC_ORDER WHERE ORDERDATE = '{0}' AND BATCHNO = '{1}'", orderDate, batchNo);
            ExecuteNonQuery(sql);

            sql = string.Format("DELETE FROM SC_PALLETMASTER WHERE ORDERDATE = '{0}' AND BATCHNO = '{1}'", orderDate, batchNo);
            ExecuteNonQuery(sql);
        }

        //正常分拣打码
        public DataTable FindOrder(string orderDate, string batchNo)
        {
            string sql = @"SELECT MIN(A.SORTNO) SORTNO,
	                        A.ORDERID,D.N_CUST_CODE,A.CUSTOMERNAME,B.CIGARETTECODE,B.CIGARETTENAME,
	                        SUM(B.QUANTITY) QUANTITY,   
	                        ISNULL(Z.BATCHNO_ONEPRO,CAST(SUBSTRING(Z.BATCHNO,11,3) AS INT)) BATCHNO,  
	                        MIN(A.SORTNO) ORDERNO,
	                        A.ROUTECODE,A.ROUTENAME,
	                        CONVERT(NVARCHAR(10),A.ORDERDATE,120) ORDERDATE,   
	                        CONVERT(NVARCHAR(10),GETDATE(),120) SCDATE,  
	                        (SELECT NO1LINECODE FROM CMD_LINEINFO WHERE LINECODE = A.LINECODE) LINECODE ,  
	                        '1' AS ZZBS    
	                        FROM SC_ORDER_MASTER A
	                        LEFT JOIN SC_ORDER_DETAIL B ON A.ORDERDATE = B.ORDERDATE AND A.BATCHNO = B.BATCHNO AND A.LINECODE=B.LINECODE AND A.SORTNO=B.SORTNO   
	                        LEFT JOIN CMD_BATCH Z ON A.ORDERDATE = Z.ORDERDATE AND A.BATCHNO = Z.BATCHNO   
	                        LEFT JOIN CMD_LINEINFO C ON A.LINECODE = C.LINECODE
                            LEFT JOIN CMD_CUSTOMER D ON A.CUSTOMERCODE = D.CUSTOMERCODE 
	                        WHERE A.ORDERDATE='{0}' AND A.BATCHNO='{1}' AND C.LINETYPE = '2'   
	                        GROUP BY A.ORDERDATE,Z.BATCHNO_ONEPRO,Z.BATCHNO,A.LINECODE,A.ROUTECODE,A.ROUTENAME,A.ORDERID,A.CUSTOMERCODE,D.N_CUST_CODE,A.CUSTOMERNAME,B.CIGARETTECODE,B.CIGARETTENAME
	                        ORDER BY A.LINECODE,A.SORTNO,B.CIGARETTECODE ";

            sql = @"SELECT ORDERID + SUBSTRING(CAST(1000+ROWID AS VARCHAR),2,3) ID, ORDERID, N_CUST_CODE, CUSTOMERNAME, CIGARETTECODE, CIGARETTENAME, " +
                   "QUANTITY, BATCHNO, ORDERNO, ROUTECODE, ROUTENAME, ORDERDATE, SCDATE, LINECODE, ZZBS " +
                   "FROM V_ORDER_TOONE1 A " +
                   "WHERE A.ORDERDATE='{0}' AND A.BATCHNO1='{1}' " +
	               "ORDER BY A.LINECODE,A.SORTNO ";
            sql = string.Format(sql, orderDate, batchNo);
            return ExecuteQuery(sql).Tables[0];
        }

        //手工分拣打码
        internal DataTable FindOrderForHandleSort(string orderDate, string batchNo,string handleSortLineCode)
        {
            string sql = @"SELECT ORDERID + SUBSTRING(CAST(1000+SORTNO AS VARCHAR),2,3) ID,
                            ORDERID,N_CUST_CODE,CUSTOMERNAME,CIGARETTECODE,CIGARETTENAME,QUANTITY,BATCHNO,
                            ORDERNO,ROUTECODE,ROUTENAME,
                            ORDERDATE,SCDATE,'{0}' LINECODE,ZZBS 
                            FROM V_ORDER_TOONE2 
                            WHERE ORDERDATE='{1}' AND BATCHNO1='{2}'";
            sql = string.Format(sql, handleSortLineCode,orderDate, batchNo);
            return ExecuteQuery(sql).Tables[0];
        }

        //整件分拣打码
        internal DataTable FindOrderForWholePieces(string orderDate, string batchNo, string wholePiecesSortLineCode)
        {
            //
            //string sql = "SELECT ROW_NUMBER() OVER (ORDER BY D.SORTID,C.SORTID,A.ORDERID ,B.CIGARETTECODE) AS SORTNO, " +
            //                " A.ORDERID,C.CUSTOMERCODE,C.CUSTOMERNAME," +
            //                " B.CIGARETTECODE,B.CIGARETTENAME,(B.QUANTITY/50)*50 QUANTITY, " +
            //                " ISNULL(Z.BATCHNO_ONEPRO,Z.BATCHNO) BATCHNO," +
            //                " ROW_NUMBER() OVER (ORDER BY D.SORTID,C.SORTID,A.ORDERID ,B.CIGARETTECODE) ORDERNO," +
            //                " D.ROUTECODE,D.ROUTENAME," +
            //                " CONVERT(NVARCHAR(10),A.ORDERDATE,120) ORDERDATE," +
            //                " CONVERT(NVARCHAR(10),GETDATE(),120) SCDATE," +
            //                " '{0}' AS LINECODE,'1' AS ZZBS  " +
            //                " FROM SC_I_ORDERMASTER A " +
            //                " LEFT JOIN SC_I_ORDERDETAIL B " +
            //                " ON A.ORDERID = B.ORDERID " +
            //                " LEFT JOIN CMD_BATCH Z " +
            //                " ON A.ORDERDATE = Z.ORDERDATE AND A.BATCHNO = Z.BATCHNO " +
            //                " LEFT JOIN CMD_CUSTOMER C " +
            //                " ON A.CUSTOMERCODE = C.CUSTOMERCODE" +
            //                " LEFT JOIN CMD_ROUTE D" +
            //                " ON A.ROUTECODE = D.ROUTECODE " +
            //                " LEFT JOIN CMD_Product E" +
            //                " ON B.CIGARETTECODE = E.CIGARETTECODE" +
            //                " WHERE A.ORDERDATE='{1}' AND A.BATCHNO='{2}' AND B.QUANTITY/50 > 0 AND E.ISABNORMITY != '1'" +
            //                " ORDER BY LINECODE,SORTNO,CIGARETTECODE";
            //return ExecuteQuery(string.Format(sql, wholePiecesSortLineCode, orderDate, batchNo)).Tables[0];

            string sql = @"SELECT A.SORTNO SORTNO,
	                        A.SORTNO ORDERID,D.N_CUST_CODE,A.CUSTOMERNAME,B.CIGARETTECODE,B.CIGARETTENAME,
	                        SUM(B.QUANTITY) QUANTITY,   
	                        ISNULL(Z.BATCHNO_ONEPRO,Z.BATCHNO) BATCHNO,  
	                        MIN(A.SORTNO) ORDERNO,
	                        A.ROUTECODE,A.ROUTENAME,
	                        CONVERT(NVARCHAR(10),A.ORDERDATE,120) ORDERDATE,   
	                        CONVERT(NVARCHAR(10),GETDATE(),120) SCDATE,  
	                        (SELECT NO1LINECODE FROM CMD_LINEINFO WHERE LINECODE = A.LINECODE) LINECODE ,  
	                        '0' AS ZZBS    
	                        FROM SC_ORDER_MASTER A   
	                        LEFT JOIN SC_ORDER_DETAIL B ON A.ORDERDATE = B.ORDERDATE AND A.BATCHNO = B.BATCHNO AND A.LINECODE=B.LINECODE AND A.SORTNO=B.SORTNO   
	                        LEFT JOIN CMD_BATCH Z ON A.ORDERDATE = Z.ORDERDATE AND A.BATCHNO = Z.BATCHNO  
                            LEFT JOIN CMD_LINEINFO C ON A.LINECODE = C.LINECODE 
                            LEFT JOIN CMD_CUSTOMER D ON A.CUSTOMERCODE = D.CUSTOMERCODE
	                        WHERE A.ORDERDATE='{0}' AND A.BATCHNO='{1}' AND C.LINETYPE = '3'  
	                        GROUP BY A.ORDERDATE,Z.BATCHNO_ONEPRO,Z.BATCHNO,A.LINECODE,A.ROUTECODE,A.ROUTENAME,A.ORDERID,A.CUSTOMERCODE,D.N_CUST_CODE,A.CUSTOMERNAME,A.SORTNO,B.CIGARETTECODE,B.CIGARETTENAME
	                        ORDER BY A.LINECODE,SORTNO,B.CIGARETTECODE ";

            sql = string.Format(sql, orderDate, batchNo);
            return ExecuteQuery(sql).Tables[0];
        }

        //异形分拣打码
        public DataTable FindOrderForAbnormity(string orderDate, string batchNo, string abnormitySortLineCode)
        {
            //按订单打
            string sql = "SELECT ROW_NUMBER() OVER (ORDER BY D.SORTID,C.SORTID,A.ORDERID ,B.CIGARETTECODE) AS SORTNO, " +
                            " A.ORDERID,C.N_CUST_CODE,C.CUSTOMERNAME," +
                            " B.CIGARETTECODE,B.CIGARETTENAME,B.QUANTITY, " +
                            " ISNULL(Z.BATCHNO_ONEPRO,Z.BATCHNO) BATCHNO," +
                            " ROW_NUMBER() OVER (ORDER BY D.SORTID,C.SORTID,A.ORDERID ,B.CIGARETTECODE) ORDERNO," +
                            " D.ROUTECODE,D.ROUTENAME," +
                            " CONVERT(NVARCHAR(10),A.ORDERDATE,120) ORDERDATE," +
                            " CONVERT(NVARCHAR(10),GETDATE(),120) SCDATE," +
                            " '{0}' AS LINECODE,'1' AS ZZBS  " +
                            " FROM SC_I_ORDERMASTER A " +
                            " LEFT JOIN SC_I_ORDERDETAIL B " +
                            " ON A.ORDERID = B.ORDERID " +
                            " LEFT JOIN CMD_BATCH Z " +
                            " ON A.ORDERDATE = Z.ORDERDATE AND A.BATCHNO = Z.BATCHNO " +
                            " LEFT JOIN CMD_CUSTOMER C " +
                            " ON A.CUSTOMERCODE = C.CUSTOMERCODE" +
                            " LEFT JOIN CMD_ROUTE D" +
                            " ON A.ROUTECODE = D.ROUTECODE " +
                            " LEFT JOIN CMD_Product E" +
                            " ON B.CIGARETTECODE = E.CIGARETTECODE" +
                            " WHERE A.ORDERDATE='{1}' AND A.BATCHNO='{2}' AND B.QUANTITY IS NOT NULL AND E.ISABNORMITY = '1'" +
                            " ORDER BY LINECODE,SORTNO,CIGARETTECODE";
            //按品牌打
            sql = @"SELECT ROW_NUMBER() OVER (ORDER BY B.CIGARETTECODE,A.SORTID) AS SORTNO,   
                      A.ORDERID,C.N_CUST_CODE,C.CUSTOMERNAME,  
                      B.CIGARETTECODE,B.CIGARETTENAME,B.QUANTITY,   
                      ISNULL(Z.BATCHNO_ONEPRO,CAST(SUBSTRING(Z.BATCHNO,11,3) AS INT)) BATCHNO,  
                      ROW_NUMBER() OVER (ORDER BY A.SORTID ,B.CIGARETTECODE) ORDERNO,  
                      D.ROUTECODE,D.ROUTENAME,  
                      CONVERT(NVARCHAR(10),A.ORDERDATE,120) ORDERDATE,  
                      CONVERT(NVARCHAR(10),GETDATE(),120) SCDATE,  
                      '{0}' AS LINECODE,'1' AS ZZBS    
                      FROM SC_I_ORDERMASTER A   
                      LEFT JOIN SC_I_ORDERDETAIL B   
                      ON A.ORDERID = B.ORDERID   
                      LEFT JOIN CMD_BATCH Z   
                      ON A.ORDERDATE = Z.ORDERDATE AND A.BATCHNO = Z.BATCHNO   
                      LEFT JOIN CMD_CUSTOMER C   
                      ON A.CUSTOMERCODE = C.CUSTOMERCODE  
                      LEFT JOIN CMD_ROUTE D  
                      ON A.ROUTECODE = D.ROUTECODE   
                      LEFT JOIN CMD_Product E  
                      ON B.CIGARETTECODE = E.CIGARETTECODE  
                      WHERE A.ORDERDATE='{1}' AND A.BATCHNO='{2}' AND B.QUANTITY IS NOT NULL AND E.ISABNORMITY = '1'  
                      ORDER BY SORTNO,CIGARETTECODE,LINECODE";

            sql = @"SELECT ORDERID + SUBSTRING(CAST(1000+SORTNO AS VARCHAR),2,3) ID,
                            ORDERID,N_CUST_CODE,CUSTOMERNAME,CIGARETTECODE,CIGARETTENAME,QUANTITY,BATCHNO,
                            ORDERNO,ROUTECODE,ROUTENAME,
                            ORDERDATE,SCDATE,'{0}' LINECODE,ZZBS 
                            FROM V_ORDER_TOONE4 
                            WHERE ORDERDATE='{1}' AND BATCHNO1='{2}'";
            sql = string.Format(sql, abnormitySortLineCode, orderDate, batchNo);

            return ExecuteQuery(sql).Tables[0];
        }

        public void ClearSplitOrder()
        {
            ExecuteNonQuery("DELETE FROM SC_ORDER_MASTERH WHERE BATCHNO IN(SELECT DISTINCT BATCHNO FROM SC_ORDER_MASTER)");
            ExecuteNonQuery("DELETE FROM SC_ORDER_DETAILH WHERE BATCHNO IN(SELECT DISTINCT BATCHNO FROM SC_ORDER_MASTER)");
            ExecuteNonQuery("INSERT INTO SC_ORDER_MASTERH SELECT * FROM SC_ORDER_MASTER");
            ExecuteNonQuery("INSERT INTO SC_ORDER_DETAILH SELECT * FROM SC_ORDER_DETAIL");

            ExecuteNonQuery("TRUNCATE TABLE SC_ORDER_MASTER");
            ExecuteNonQuery("TRUNCATE TABLE SC_ORDER_DETAIL");
        }

        /// <summary>
        /// 2010-11-21
        /// </summary>
        /// <param name="ds"></param>
        public void SaveSplitOrder(DataSet ds)
        {
            //SaveOrderMaster(ds.Tables["MASTER"], "SC_TMP_PALLETMASTER");
            //SaveOrderSchedule(ds.Tables["DETAIL"], "SC_TMP_ORDER");
            SaveOrderDetail(ds.Tables["DETAIL"], "SC_ORDER_DETAIL");
        }
    }
}
