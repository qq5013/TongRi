using System;
using System.Data;
using DB.Util;
namespace Sorting.Dispatching.Dao
{
    public class BatchDao : BaseDao
    {
        public DataTable FindAll()
        {

            String sql = "SELECT A.ORDERDATE, A.BATCHNO, (CASE WHEN A.ISDOWNLOAD='0' THEN '否' ELSE '是' END) ISDOWNLOAD, " +
                         "(CASE WHEN A.ISUPLOAD='0' THEN '否' ELSE '是' END) ISUPLOAD, " +
                         "(CASE WHEN A.ISVALID='0' THEN '否' ELSE '是' END) ISVALID, " +
                         "(CASE WHEN A.ISUPTONOONEPRO='0' THEN '否' ELSE '是' END) ISUPTONOONEPRO, " +
                         "CONVERT(VARCHAR(30),A.BEGINSORTTIME,120) BEGINSORTTIME, CONVERT(VARCHAR(30),A.ENDSORTTIME,120) ENDSORTTIME,B.AMOUNT,B.TAMOUNT,B.LAMOUNT," +
                         "convert(decimal(10,2),(datediff(minute, BEGINSORTTIME, ENDSORTTIME)-ISNULL((SELECT SUM(DATEDIFF(minute,BEGINTIME,ENDTIME)) FROM CMD_BatchDetail WHERE BREAKTYPE IN ('01','02','03','04','05') AND BATCHNO=A.BATCHNO AND BEGINTIME IS NOT NULL AND ENDTIME IS NOT NULL),0)) /60.0) SORTTIME," +
                         "CEILING(B.AMOUNT/convert(decimal(10,2),(datediff(minute, BEGINSORTTIME, ENDSORTTIME)-ISNULL((SELECT SUM(DATEDIFF(minute,BEGINTIME,ENDTIME)) FROM CMD_BatchDetail WHERE BREAKTYPE IN ('01','02','03','04','05') AND BATCHNO=A.BATCHNO AND BEGINTIME IS NOT NULL AND ENDTIME IS NOT NULL),0)) /60.0)) EFFICIENCY," +
                         "(SELECT COUNT(*) FROM CMD_BatchDetail WHERE BATCHNO=A.BATCHNO) BREAKCOUNT," +
                         "ISNULL((SELECT SUM(DATEDIFF(minute,BEGINTIME,ENDTIME)) FROM CMD_BatchDetail WHERE BATCHNO=A.BATCHNO AND BEGINTIME IS NOT NULL AND ENDTIME IS NOT NULL),0) BREAKTIME " +
                         "FROM CMD_BATCH A " +
                         "LEFT JOIN V_SC_ORDER_MASTER B ON A.BATCHNO=B.BATCHNO " +
                         "ORDER BY ORDERDATE DESC,BATCHNO";

            return ExecuteQuery(sql).Tables[0];
        }
        public DataTable FindAll(int startRecord, int pageSize, string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);
            String sql = "SELECT *,ISNULL(BATCHNO_ONEPRO,BATCHNO) AS NO1BATCH FROM CMD_BATCH" + where + "ORDER BY ORDERDATE DESC,BATCHNO DESC";
            return ExecuteQuery(sql, "CMD_BATCH", startRecord, pageSize).Tables[0];
        }

        public int FindCount(string filter)
        {
            string where = " ";
            if (filter != null)
                where += (" WHERE " + filter);

            string sql = "SELECT COUNT(*) FROM CMD_BATCH " + where;
            return (int)ExecuteScalar(sql);
        }
        public DataTable FindBatch()
        {
            string sql = "SELECT * FROM CMD_BATCH WHERE ISDOWNLOAD=0 ORDER BY BATCHNO";
            return ExecuteQuery(sql).Tables[0];
        }
        public DataTable FindBatch(string orderDate)
        {
            string sql = string.Format("SELECT * FROM CMD_BATCH WHERE ORDERDATE='{0}' AND ISVALID='1' ORDER BY BATCHNO", orderDate);
            return ExecuteQuery(sql).Tables[0];
        }
        public DataTable FindBatchByFilter(string orderDate,string filter)
        {
            string sql = string.Format("SELECT * FROM CMD_BATCH WHERE ORDERDATE='{0}' AND {1} ORDER BY BATCHNO", orderDate,filter);
            return ExecuteQuery(sql).Tables[0];
        }
        public DataTable FindBatch(string orderDate, string batchNo)
        {
            string sql = string.Format("SELECT * FROM CMD_BATCH WHERE ORDERDATE='{0}' AND BATCHNO='{1}'", orderDate, batchNo);
            return ExecuteQuery(sql).Tables[0];
        }

        public bool BatchNoExists(string orderDate, string batchNo)
        {
            string sql = string.Format("SELECT COUNT(*) FROM CMD_BATCH WHERE ORDERDATE='{0}' AND BATCHNO='{1}'", orderDate, batchNo);
            return Convert.ToBoolean(ExecuteScalar(sql));
        }

        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        public void DeleteHistory(string orderDate)
        {
            string sql = string.Format("DELETE FROM CMD_BATCH WHERE ORDERDATE < '{0}'", orderDate);            
            ExecuteNonQuery(sql);
        }
        public void InsertEntity(string batchNo)
        {
            string OrderDate = batchNo.Substring(2, 4) + "-" + batchNo.Substring(6, 2) + "-" + batchNo.Substring(8, 2);
            DateTime SCDATE = DateTime.Parse(OrderDate);
            //DateTime SCDATE = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            SqlCreate sqlCreate = new SqlCreate("CMD_BATCH", SqlType.INSERT);
            sqlCreate.AppendQuote("BATCHNO", batchNo);
            sqlCreate.AppendQuote("BATCHNAME", batchNo);
            sqlCreate.AppendQuote("ORDERDATE", OrderDate);
            sqlCreate.AppendQuote("ISVALID", 0);
            sqlCreate.AppendQuote("EXECUTEUSER", "");
            sqlCreate.AppendQuote("EXECUTEIP", "127.0.0.1");
            sqlCreate.AppendQuote("ISUPTONOONEPRO", 0);
            sqlCreate.AppendQuote("SCDATE", DateTime.Now.ToShortDateString());
            ExecuteNonQuery(sqlCreate.GetSQL());
        }
        public void InsertEntity(string orderDate, string batchNo)
        {
            DateTime SCDATE = DateTime.Parse(orderDate);

            SqlCreate sqlCreate = new SqlCreate("CMD_BATCH", SqlType.INSERT);
            sqlCreate.Append("BATCHNO", batchNo);
            sqlCreate.AppendQuote("BATCHNAME", string.Format("{0}第{1}批次", orderDate, batchNo));
            sqlCreate.AppendQuote("ORDERDATE", orderDate);
            sqlCreate.AppendQuote("ISVALID", 0);
            sqlCreate.AppendQuote("EXECUTEUSER", 0);
            sqlCreate.AppendQuote("EXECUTEIP", 0);
            sqlCreate.AppendQuote("ISUPTONOONEPRO", 0);
            sqlCreate.AppendQuote("SCDATE", DateTime.Now.ToShortDateString());
            ExecuteNonQuery(sqlCreate.GetSQL());
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ip"></param>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void UpdateExecuter(string user, string ip, string batchNo)
        {
            string sql = string.Format("UPDATE CMD_BATCH SET EXECUTEUSER='{0}',EXECUTEIP='{1}' " +
                "WHERE BATCHNO='{2}'", user, ip, batchNo);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ip"></param>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        public void UpdateExecuter(string user, string ip, string orderDate, string batchNo)
        {
            string sql = string.Format("UPDATE CMD_BATCH SET EXECUTEUSER='{0}',EXECUTEIP='{1}' " +
                "WHERE ORDERDATE='{2}' AND BATCHNO='{3}'", user, ip, orderDate, batchNo);
            ExecuteNonQuery(sql);
        }
        public void UpdateDownload(string batchNo)
        {
            string sql = string.Format("UPDATE CMD_BATCH SET ISDOWNLOAD='1' WHERE BATCHNO='{0}'", batchNo);
            ExecuteNonQuery(sql);
        }
        public void UpdateDownload(string batchNo,string status)
        {
            string sql = string.Format("UPDATE CMD_BATCH SET ISDOWNLOAD='{1}' WHERE BATCHNO='{0}'", batchNo,status);
            ExecuteNonQuery(sql);
        }
        public void UpdateNoOnePro(string batchNo, string status)
        {
            string sql = string.Format("UPDATE CMD_BATCH SET ISUPTONOONEPRO='{0}' " +
                "WHERE BATCHNO='{1}'", status, batchNo);
            ExecuteNonQuery(sql);
        }
        public void UpdateNoOnePro(string orderDate, string batchNo, string user)
        {
            string sql = string.Format("UPDATE CMD_BATCH SET ISUPTONOONEPRO='1',SENDNOONEUSER='{0}' " +
                "WHERE ORDERDATE='{1}' AND BATCHNO='{2}'", user, orderDate, batchNo);
            ExecuteNonQuery(sql);
        }
        public void UpdateUpload(string batchNo, string status)
        {
            string sql = string.Format("UPDATE CMD_BATCH SET ISUPLOAD='{0}' " +
                "WHERE BATCHNO='{1}'", status, batchNo);
            ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="status"></param>
        public void UpdateIsValid(string batchNo, string status)
        {
            string sql = "UPDATE CMD_BATCH SET ISVALID='{0}' WHERE BATCHNO='{1}'";
            ExecuteNonQuery(string.Format(sql, status, batchNo));
        }
        /// <summary>
        /// 2010-11-19
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <param name="status"></param>
        public void UpdateIsValid(string orderDate, string batchNo, string status)
        {
            string sql = "UPDATE CMD_BATCH SET ISVALID='{0}' WHERE ORDERDATE='{1}' AND BATCHNO='{2}'";
            if(status=="1")
                sql = "UPDATE CMD_BATCH SET ISVALID='{0}',VALIDTIME=GETDATE() WHERE ORDERDATE='{1}' AND BATCHNO='{2}'";
            else
                sql = "UPDATE CMD_BATCH SET ISVALID='{0}',VALIDTIME=NULL WHERE ORDERDATE='{1}' AND BATCHNO='{2}'";
            ExecuteNonQuery(string.Format(sql,status, orderDate, batchNo));
        }

        internal void UpdateEntity(string orderDate, string sortBatch, string no1Batch,string no1UpLoadState)
        {
            string sql = "UPDATE CMD_BATCH SET BATCHNO_ONEPRO = '{0}',ISUPTONOONEPRO='{3}' WHERE ORDERDATE='{1}' AND BATCHNO='{2}'";
            ExecuteNonQuery(string.Format(sql, no1Batch, orderDate, sortBatch, no1UpLoadState));
        }

        internal void SelectBalanceIntoHistory(string orderDate, string batchNo)
        {
            string sql = "DELETE SC_BALANCE_HISTORY WHERE ORDERDATE = '{0}' AND BATCHNO = '{1}' ";
            ExecuteNonQuery(string.Format(sql, orderDate, batchNo));

            sql = "INSERT INTO SC_BALANCE_HISTORY " +
                    " SELECT '{0}','{1}',LINECODE,CHANNELCODE,CHANNELNAME,CIGARETTECODE, CIGARETTENAME, SUM(QUANTITY) AS QUANTITY " +
                    " FROM SC_BALANCE " +
                    " GROUP BY LINECODE,CHANNELCODE,CHANNELNAME,CIGARETTECODE, CIGARETTENAME";
            ExecuteNonQuery(string.Format(sql, orderDate, batchNo));
        }

        internal bool CheckOrder(string orderDate, string batchNo)
        {
            string sql = @"SELECT A.ORDERID,A.ROUTECODE,A.CUSTOMERCODE,B.CIGARETTENAME,
	                            ISNULL(SUM(B.QUANTITY),0),
	                            (SELECT ISNULL(SUM(QUANTITY),0) FROM SC_ORDER   
		                            WHERE ORDERID = A.ORDERID AND CIGARETTECODE = B.CIGARETTECODE),   
	                            ISNULL(SUM(B.QUANTITY),0)-
	                            (SELECT ISNULL(SUM(QUANTITY),0) FROM SC_ORDER   
		                            WHERE ORDERID = A.ORDERID AND CIGARETTECODE = B.CIGARETTECODE ),  
	                            D.CUSTOMERNAME  
	                            FROM SC_I_ORDERMASTER A  
	                            LEFT JOIN SC_I_ORDERDETAIL B ON A.ORDERID = B.ORDERID  
	                            LEFT JOIN CMD_CUSTOMER D ON A.CUSTOMERCODE = D.CUSTOMERCODE  
	                            LEFT JOIN CMD_Product E ON B.CIGARETTECODE = E.CIGARETTECODE   
	                            WHERE  A.ORDERDATE ='{0}' AND A.BATCHNO = '{1}'  AND E.ISABNORMITY = 0 
	                            AND A.ORDERID NOT IN (SELECT ORDERID FROM SC_HANDLE_SORT_ORDER WHERE ORDERDATE = '{0}')   
	                            GROUP BY A.ORDERID,A.ROUTECODE,A.CUSTOMERCODE,B.CIGARETTECODE,B.CIGARETTENAME,D.CUSTOMERNAME   
	                            HAVING ISNULL(SUM(B.QUANTITY),0) != 
	                            (SELECT ISNULL(SUM(QUANTITY),0) FROM SC_ORDER_DETAIL
		                            WHERE ORDERID = A.ORDERID AND CIGARETTECODE = B.CIGARETTECODE)  
	                            ORDER BY A.ROUTECODE ";

            sql = @"SELECT A.ORDERID,A.ROUTECODE,A.CUSTOMERCODE,B.ProductName,
	                            ISNULL(SUM(B.QUANTITY),0),
	                            (SELECT ISNULL(SUM(QUANTITY),0) FROM SC_ORDER   
		                            WHERE ORDERID = A.ORDERID AND ProductCode = B.ProductCode),   
	                            ISNULL(SUM(B.QUANTITY),0)-
	                            (SELECT ISNULL(SUM(QUANTITY),0) FROM SC_ORDER   
		                            WHERE ORDERID = A.ORDERID AND ProductCode = B.ProductCode ),  
	                            D.CUSTOMERNAME  
	                            FROM SC_I_ORDERMASTER A  
	                            LEFT JOIN SC_I_ORDERDETAIL B ON A.ORDERID = B.ORDERID  
	                            LEFT JOIN CMD_CUSTOMER D ON A.CUSTOMERCODE = D.CUSTOMERCODE  
	                            LEFT JOIN CMD_Product E ON B.ProductCode = E.ProductCode   
	                            WHERE  A.ORDERDATE ='{0}' AND A.BATCHNO = '{1}'  AND E.ISABNORMITY = 0 
	                            AND A.ORDERID NOT IN (SELECT ORDERID FROM SC_HANDLE_SORT_ORDER WHERE BATCHNO = '{1}')
                                AND A.ROUTECODE IN(SELECT ROUTECODE FROM CMD_ROUTE WHERE ISSORT ='1')  
	                            GROUP BY A.ORDERID,A.ROUTECODE,A.CUSTOMERCODE,B.ProductCode,B.ProductName,D.CUSTOMERNAME   
	                            HAVING ISNULL(SUM(B.QUANTITY),0) != 
	                            (SELECT ISNULL(SUM(QUANTITY),0) FROM SC_ORDER_DETAIL
		                            WHERE ORDERID = A.ORDERID AND ProductCode = B.ProductCode)  
	                            ORDER BY A.ROUTECODE ";
            return (ExecuteQuery(string.Format(sql, orderDate, batchNo)).Tables[0].Rows.Count == 0);
        }
        /// <summary>
        /// 查询本次优化的总数量
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public int BatchCount(string batchNo)
        {
            string sql = @"SELECT SUM(ORDER_QUANTITY) FROM DBO.SC_I_ORDERDETAIL WHERE BATCHNO='{1}'";
            return Convert.ToInt32(ExecuteQuery(string.Format(sql, batchNo)).Tables[0].Rows[0][0]);
        }
        /// <summary>
        /// 查询本次优化的总数量
        /// </summary>
        /// <param name="orderDate"></param>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public int BatchCount(string orderDate, string batchNo)
        {
            string sql = @"SELECT ISNULL(SUM(QUANTITY),0) FROM DBO.SC_I_ORDERDETAIL WHERE ORDERDATE='{0}' AND BATCHNO='{1}'";
            return Convert.ToInt32(ExecuteQuery(string.Format( sql,orderDate,batchNo)).Tables[0].Rows[0][0]);
        }
        /// <summary>
        /// 判断是否有正在分拣的数据
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public bool IsExistsNoSorting()
        {
            bool isExists;
            int count, count1;
            string sql = @"SELECT COUNT(*),(SELECT COUNT(*) FROM SC_ORDER_MASTER) TOTALCOUNT FROM SC_ORDER_MASTER WHERE STATUS='0' AND STATUS1='0'";
            DataTable dt = ExecuteQuery(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                count = int.Parse(dt.Rows[0][0].ToString());
                count1 = int.Parse(dt.Rows[0][1].ToString());
                if (count < count1 && count > 0)
                    isExists= true;
                else
                    isExists= false;
            }
            else
                isExists= false;
            return isExists;
        }
        /// <summary>
        /// 判断是否有正在补货的数据
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public bool IsExistsStartSupply()
        {
            string sql = @"SELECT STATUS FROM SC_SUPPLY GROUP BY STATUS";
            DataTable dt = ExecuteQuery(sql).Tables[0];
            if (dt.Rows.Count > 1)
                return true;
            else
                return false;            
        }
        internal void InsertBatchDetail(string BreakType, string Flag)
        {
            string sql;

            if (Flag == "1")
                sql = string.Format("INSERT INTO CMD_BatchDetail(BATCHNO, BREAKTYPE, BREAKNAME, BEGINTIME) " +
                             "SELECT ISNULL((SELECT TOP 1 BATCHNO FROM SC_ORDER_MASTER),''),'{0}',(SELECT BREAKNAME FROM CMD_BREAKTYPE WHERE BREAKTYPE='{0}'),GETDATE()", BreakType);

            else
                sql = string.Format("UPDATE CMD_BatchDetail SET ENDTIME=GETDATE() WHERE BREAKTYPE='{0}' AND ENDTIME IS NULL AND BATCHNO IN(SELECT TOP 1 BATCHNO FROM SC_ORDER_MASTER)", BreakType);
            ExecuteNonQuery(sql);
        }
        internal void InsertBatchDetail(string AlarmNo,string BreakType, string ChannelName)
        {
            string sql = string.Format("INSERT INTO CMD_BatchDetail(BatchNo, AlarmNo,BreakType, BreakName,ChannelCode,ChannelName, BeginTime) " +
                             "SELECT ISNULL((SELECT TOP 1 BATCHNO FROM SC_ORDER_MASTER),''),'{0}','{1}',ISNULL((SELECT BREAKNAME FROM CMD_BREAKTYPE WHERE BREAKTYPE='{1}'),'')," +
                             "ISNULL((SELECT ChannelCode FROM CMD_Channel WHERE ChannelName='{2}'),''),'{2}',GETDATE() " + 
                             "Where Not exists(select * from CMD_BatchDetail where AlarmNo='{0}' AND Status='0' AND " +
                             "ENDTIME IS NULL AND BATCHNO IN(SELECT TOP 1 BATCHNO FROM SC_ORDER_MASTER))", AlarmNo,BreakType, ChannelName);

            ExecuteNonQuery(sql);
        }
        internal void UpdateBatchDetail()
        {
            string sql = string.Format("UPDATE CMD_BatchDetail SET Status='1', ENDTIME=GETDATE() WHERE Status='0' AND ENDTIME IS NULL AND BATCHNO IN(SELECT TOP 1 BATCHNO FROM SC_ORDER_MASTER)");
            ExecuteNonQuery(sql);
        }
        internal void UpdateBatchDetail(string AlarmNo)
        {
            string sql = string.Format("UPDATE CMD_BatchDetail SET Status='1', ENDTIME=GETDATE() WHERE Status='0' AND AlarmNo not in('{0}') AND ENDTIME IS NULL AND BATCHNO IN(SELECT TOP 1 BATCHNO FROM SC_ORDER_MASTER)", AlarmNo);
            ExecuteNonQuery(sql);
        }
        internal DataTable GetBatchDetail(string BatchNo)
        {
            string sql = string.Format("SELECT ROW_NUMBER() OVER (ORDER BY BATCHNO) ROWID,BATCHNO, BREAKTYPE, BREAKNAME, CONVERT(VARCHAR(30),BEGINTIME,120) BEGINTIME, CONVERT(VARCHAR(30),ENDTIME,120) ENDTIME,DATEDIFF(minute, BEGINTIME, ENDTIME) BREAKTIME FROM CMD_BatchDetail WHERE BATCHNO='{0}'", BatchNo);

            DataTable dt = ExecuteQuery(sql).Tables[0];
            return dt;
        }
        internal DataTable GetBreakTotal(string BatchNo)
        {
            string sql = string.Format("SELECT ROW_NUMBER() OVER (ORDER BY BREAKTYPE) ROWID,BATCHNO,BREAKTYPE,BREAKNAME,COUNT(BREAKTYPE) BREAKCOUNT," +
                                       "SUM(DATEDIFF(MINUTE,BEGINTIME,ENDTIME)) BREAKTIME " +
                                       "FROM CMD_BatchDetail " +
                                       "WHERE BATCHNO='{0}' " +
                                       "GROUP BY BATCHNO,BREAKTYPE,BREAKNAME ", BatchNo);

            DataTable dt = ExecuteQuery(sql).Tables[0];
            return dt;
        }
        internal string GetPreBatchNo(string BatchNo)
        {
            string preBatchNo = BatchNo.Substring(0, 6);
            string sql = string.Format("SELECT BATCHNO FROM CMD_BATCH WHERE BATCHNO LIKE '{0}%' AND BATCHNO<'{1}' AND ISVALID='1' ORDER BY VALIDTIME DESC", preBatchNo, BatchNo);

            DataTable dt = ExecuteQuery(sql).Tables[0];
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";
        }
        internal void DeleteBatchData(string BatchNo)
        {
            string sql = string.Format("DELETE FROM SC_I_ORDER WHERE BATCH_NO = '{0}'", BatchNo);
            ExecuteNonQuery(sql);

            sql = string.Format("DELETE FROM SC_I_ORDERMASTER WHERE BATCHNO = '{0}'", BatchNo);
            ExecuteNonQuery(sql);

            sql = string.Format("DELETE FROM SC_I_ORDERDETAIL WHERE BATCHNO = '{0}'", BatchNo);
            ExecuteNonQuery(sql);

            sql = string.Format("DELETE FROM SC_ORDER_MASTER WHERE BATCHNO = '{0}'", BatchNo);
            ExecuteNonQuery(sql);

            sql = string.Format("DELETE FROM SC_ORDER_DETAIL WHERE BATCHNO = '{0}'", BatchNo);
            ExecuteNonQuery(sql);
        }
        internal void ClearBatchData()
        {
            string sql = @"USE [master]
                            ALTER DATABASE SortSupplyDB SET RECOVERY SIMPLE WITH NO_WAIT
                            ALTER DATABASE SortSupplyDB SET RECOVERY SIMPLE   --简单模式
                            USE SortSupplyDB 
                            DBCC SHRINKFILE (N'THOK_Logistics_Data_log' , 1)
                            USE[master]
                            ALTER DATABASE SortSupplyDB SET RECOVERY FULL WITH NO_WAIT
                            ALTER DATABASE SortSupplyDB SET RECOVERY FULL
                            USE SortSupplyDB";
            ExecuteNonQuery(sql);

            sql = "delete from SC_SUPPLY2H where orderdate<CONVERT(varchar(10),getdate(),120)";
            ExecuteNonQuery(sql);
            sql = "delete from SC_SUPPLY2 where orderdate<CONVERT(varchar(10),getdate(),120)";
            ExecuteNonQuery(sql);
            sql = "delete from SC_SUPPLYH where orderdate<CONVERT(varchar(10),getdate(),120)";
            ExecuteNonQuery(sql);
            sql = "delete from SC_SUPPLY where orderdate<CONVERT(varchar(10),getdate(),120)";
            ExecuteNonQuery(sql);
            sql = "delete from SC_CHANNELUSEDH where orderdate<CONVERT(varchar(10),getdate(),120)";
            ExecuteNonQuery(sql);
            sql = "delete from SC_CHANNELBALANCE where orderdate<CONVERT(varchar(10),getdate(),120)";
            ExecuteNonQuery(sql);
            sql = "delete from SC_I_ORDERMASTER where orderdate<CONVERT(varchar(10),getdate(),120)";
            ExecuteNonQuery(sql);
            sql = "delete from SC_I_ORDERDETAIL where orderdate<CONVERT(varchar(10),getdate(),120)";
            ExecuteNonQuery(sql);
            sql = "truncate table SC_I_ORDER";
            ExecuteNonQuery(sql);
            sql = "delete from SC_BALANCEH where orderdate<CONVERT(varchar(10),getdate(),120)";
            ExecuteNonQuery(sql);
            sql = "delete from SC_BALANCE where orderdate<CONVERT(varchar(10),getdate(),120)";
            ExecuteNonQuery(sql);
            sql = "delete from SC_ORDER_MASTERH where orderdate<CONVERT(varchar(10),getdate(),120)";
            ExecuteNonQuery(sql);
            sql = "delete from SC_ORDER_MASTER where orderdate<CONVERT(varchar(10),getdate(),120)";
            ExecuteNonQuery(sql);
            sql = "delete from SC_ORDER_DETAILH where orderdate<CONVERT(varchar(10),getdate(),120)";
            ExecuteNonQuery(sql);
            sql = "delete from SC_ORDER_DETAIL where orderdate<CONVERT(varchar(10),getdate(),120)";
            ExecuteNonQuery(sql);
        }
    }
}
